using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Tweetinvi;
using Tweetinvi.Core;


namespace Web6
{
	public class TwitterManager
	{
		public Dictionary<string, User> Users { get; private set; }

		public void Initialize(List<User> users)
		{
			Users = new Dictionary<string, User>();
			foreach (var user in users) Users[user.ScreenName] = user;

			TwitterCredentials.SetCredentials(Define.ACCESS, Define.ACCESS_SECRET, Define.CONSUMER, Define.CONSUMER_SECRET);
		}

		public List<Web6.Tweet> GetMyTimeline(long since)
		{
			RequestParameters p = new RequestParameters();
			p.MaximumNumberOfTweetsToRetrieve = 200;
			p.ExcludeReplies = true;
			p.IncludeEntities = false;
			p.SinceId = since;

			List<Web6.Tweet> list = new List<Web6.Tweet>();
			foreach (var t in Timeline.GetHomeTimeline(200))
			{
				if (t.Id <= since) continue;
				list.Add(new Tweet() { TweetId = t.Id, Date = t.CreatedAt, ScreenName = t.Creator.ScreenName, Text = t.Text });
				Users[t.Creator.ScreenName] = new User() { UserId = t.Creator.Id, ScreenName = t.Creator.ScreenName, Name = t.Creator.Name, ImageUrl = t.Creator.ProfileImageUrl };
			}
			return list;
		}

		public void DownloadUserImages(string dir)
		{
			int life = 5;
			foreach(var user in Users.Values)
			{
				if (life <= 0) break;
				if (string.IsNullOrEmpty(user.ImageUrl)) continue;

				string path = GetUserImagePath(dir, user);
				if (File.Exists(path)) continue;
				WebClient client = new WebClient();
				client.DownloadFile(user.ImageUrl, path);
				life--;
			}
		}
		private string GetUserImagePath(string dir, User user)
		{
			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
			return Path.Combine(dir, user.ScreenName + Path.GetExtension(user.ImageUrl));
		}

		class RequestParameters : Tweetinvi.Core.Interfaces.Models.Parameters.IHomeTimelineRequestParameters
		{
			public int MaximumNumberOfTweetsToRetrieve { get; set; }

			public long SinceId { get; set; }
			public long MaxId { get; set; }

			public bool TrimUser { get; set; }
			public bool IncludeEntities { get; set; }

			public bool ExcludeReplies { get; set; }
			public bool IncludeContributorDetails { get; set; }
		}
	}
}