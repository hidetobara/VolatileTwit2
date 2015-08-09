using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Threading;

using Web6.Models;
using Tweetinvi;
using Tweetinvi.Core;
using VolatileTweetLibrary;


namespace Web6.Controllers
{
	public class HomeController : Controller
	{
		private string IpadicDir { get { return HttpContext.Server.MapPath("~/App_Data/ipadic/"); } }
		private string IconsDir { get { return HttpContext.Server.MapPath("~/App_Data/icons/"); } }
		private string NetworkDir { get { return HttpContext.Server.MapPath("~/App_Data/"); } }

		BasicContext db = new BasicContext();

		public ActionResult Index()
		{
			ViewBag.Message = "Total " + db.Tweets.Count() + " Tweets.";
			return View();
		}

		public ActionResult Analyze()
		{
			try
			{
				ViewBag.Message = "Analyzing...";

				var tweets = db.Select1DayTweets(200);
				string input = "";
				foreach (var t in tweets) input += t.Text + Environment.NewLine;

				MorphemeManager manager = MorphemeManager.Instance.Initialize(IpadicDir);
				List<MorphemeManager.Item> items = manager.Parse(input);

				Dictionary<string, int> table = new Dictionary<string, int>();
				foreach (MorphemeManager.Item item in items)
				{
					if (item.Type != MorphemeManager.MorphemeType.NOUN) continue;
					if (string.IsNullOrEmpty(item.Origin)) continue;
					if (!table.ContainsKey(item.Origin)) table[item.Origin] = 1;
					else table[item.Origin]++;
				}
				List<Usage> list = new List<Usage>();
				foreach (var pair in table)
				{
					if (pair.Value <= 1) continue;
					list.Add(new Usage() { Origin = pair.Key, Count = pair.Value });
				}

				return View(list.OrderByDescending(u => u.Count).Take(30));
			}
			catch(Exception ex)
			{
				ViewBag.Error = ex.Message + "@" + ex.StackTrace;
				return View(new List<Usage>());
			}
		}

		public ActionResult Timeline()
		{
			try
			{
				TwitterManager manager = new TwitterManager(Define.BARA_ACCESS_KEY, Define.BARA_ACCESS_SECRET);
				manager.Initialize(db.Users.ToList());
				long last = db.SelectLastTweetId();
				db.Insert(manager.GetMyTimeline(last));
				db.UpdateOrInsert(manager.Users.Values.ToList());
				manager.DownloadUserImages(IconsDir);
				ViewBag.Message = "Last id is " + db.SelectLastTweetId();

				new Thread(
					() =>
					{
						EstimateLearn();
					})
					.Start();
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message + "@" + ex.StackTrace;
			}
			return View();
		}

		public ActionResult Volatile()
		{
			try
			{
				MorphemeManager.Instance.Initialize(IpadicDir);
				GenerateManager m = new GenerateManager(NetworkDir, "shokos",
					Define.CONSUMER, Define.CONSUMER_SECRET, Define.SHOKOS_ACCESS, Define.SHOKOS_ACCESS_SECRET);
				m.PublishTweet();
				ViewBag.Message = "OK";
			}
			catch(Exception ex)
			{
				ViewBag.Error = ex.Message + "@" + ex.StackTrace;
			}
			return View();
		}

		public ActionResult Learn()
		{
			try
			{
				var estimate = EstimateLearn();

				// 評価
				List<StripEstimated> valued = new List<StripEstimated>();
				valued.Add(estimate.Compute("shokos", "ついったー上でもセクハラするなんて、腐さんは本当にひどい"));
				valued.Add(estimate.Compute("shokos", "せーざんさんがハンターED見ながら、「一番人気なのはヒソカなのかな？」って言ったので、一応腐的な意味でか聞いた。"));
				valued.Add(estimate.Compute("hidetobara", "夜の０時になっても、ガラスのお腹は元に戻らないんですよー。"));
				return View(valued);
			}
			catch(Exception ex)
			{
				ViewBag.Error = ex.Message + "@" + ex.StackTrace;
				return View(new List<StripEstimated>());
			}
		}

		// 学習
		private EstimateManager EstimateLearn(int TweetLimit = 15)
		{
			List<Tweet> tweets = new List<Tweet>();
			tweets.AddRange(db.SelectRecentTweets(TweetLimit));
			foreach (var name in new string[] { Define.NAME_SHOKOS, Define.NAME_BARA, Define.NAME_SAYAKAME, Define.NAME_KUSIGAHAMA })
			{
				try { tweets.AddRange(db.SelectRecentTweets(name, TweetLimit)); }
				catch (Exception ex) { ViewBag.Error += ex.Message + "@" + ex.StackTrace + "<hr />"; }
			}

			List<Strip> strips = new List<Strip>();
			foreach (var t in tweets.OrderBy(tweet => Guid.NewGuid()))
			{
				strips.Add(new Strip() { ScreenName = t.ScreenName, Text = t.Text });
			}

			MorphemeManager.Instance.Initialize(IpadicDir);
			EstimateManager estimate = new EstimateManager(NetworkDir);

			IEnumerator enumerator = estimate.Learn(strips);
			while (enumerator.MoveNext()) { }

			return estimate;
		}

		public struct Usage
		{
			public string Origin;
			public int Count;
		}
	}
}