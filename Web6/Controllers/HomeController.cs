using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

using Web6.Models;
using Tweetinvi;
using Tweetinvi.Core;
using VolatileTweetLibrary;


namespace Web6.Controllers
{
	public class HomeController : Controller
	{
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

				MorphemeManager manager = MorphemeManager.Instance.Initialize(HttpContext.Server.MapPath("~/App_Data/ipadic/"));
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
				TwitterManager manager = new TwitterManager();
				manager.Initialize(db.Users.ToList());
				long last = db.SelectLastTweetId();
				db.Insert(manager.GetMyTimeline(last));
				db.UpdateOrInsert(manager.Users.Values.ToList());
				manager.DownloadUserImages(HttpContext.Server.MapPath("~/App_Data/icons/"));
				ViewBag.Message = "Last id is " + db.SelectLastTweetId();
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message + "@" + ex.StackTrace;
			}
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public struct Usage
		{
			public string Origin;
			public int Count;
		}
	}
}