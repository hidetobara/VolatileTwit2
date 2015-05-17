using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tweetinvi;
using Tweetinvi.Core;


namespace LocalTweet
{
	class TwitterManager
	{
		public void Initialize()
		{
			TwitterCredentials.SetCredentials(Define.ACCESS, Define.ACCESS_SECRET, Define.CONSUMER, Define.CONSUMER_SECRET);
		}

		public string GetMyTimeline()
		{
			string log = "";
			foreach(var t in Timeline.GetHomeTimeline())
			{
				log += t.Creator.ScreenName + ":" + t.Text + Environment.NewLine;
			}
			return log;
		}

		public IEnumerator GetUserTimeline(string name, long sinceid)
		{
			StreamWriter writer = null;
			using (writer = new StreamWriter("../" + name + "." + sinceid + ".log"))
			{
				RequestParameters p = new RequestParameters();
				p.MaximumNumberOfTweetsToRetrieve = 200;
				p.ExcludeReplies = true;
				p.IncludeRTS = false;

				long interval = 100000000;
				foreach (var t in Timeline.GetUserTimeline(name, 100))
				{
					if (t.Id > p.MaxId) p.MaxId = t.Id;
					if (p.SinceId == 0 || t.Id < p.SinceId) p.SinceId = t.Id;
				}
				if (p.MaxId - p.SinceId > interval) interval = p.MaxId - p.SinceId;

				var user = User.GetUserFromScreenName(name);
				if (user == null) yield return "NO USER";
				p.UserIdentifier = user.UserIdentifier;

				//p.MaxId = 400249525299724288;	// 注意
				if (sinceid > 0) p.MaxId = sinceid;

				for (int life = 10; life > 0; life--)
				{
					System.Threading.Thread.Sleep(15000);

					p.SinceId = p.MaxId - interval;
					var timelineTweets = user.GetUserTimeline(p);
					p.MaxId = p.SinceId - 1;
					if (timelineTweets == null) continue;

					string log = "";
					DateTime created = DateTime.Now;
					foreach (var t in timelineTweets)
					{
						string line = t.CreatedAt + "\t" + t.Text.Replace("\n", " ") + Environment.NewLine;
						writer.Write(line);
						//log += line;
						created = t.CreatedAt;
					}
					string mark = "#sinceid=" + p.SinceId + ",created" + created + Environment.NewLine;
					writer.Write(mark);
					log += mark;
					yield return log;
				}

				if (writer != null) writer.Close();
				writer = null;
			}
		}

		class RequestParameters : Tweetinvi.Core.Interfaces.Models.Parameters.IUserTimelineRequestParameters
		{
			public Tweetinvi.Core.Interfaces.Models.IUserIdentifier UserIdentifier { get; set; }

			public int MaximumNumberOfTweetsToRetrieve { get; set; }
			public bool TrimUser { get; set; }
			public bool IncludeEntities { get; set; }

			public bool IncludeRTS { get; set; }
			public bool ExcludeReplies { get; set; }
			public bool IncludeContributorDetails { get; set; }

			public long SinceId { get; set; }
			public long MaxId { get; set; }
		}
	}
}
