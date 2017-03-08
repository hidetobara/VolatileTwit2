using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Tweetinvi;
using Tweetinvi.Core;

using VolatileTweetLibrary;


namespace Web6
{
	public class TwitterManager
	{
		public Dictionary<string, User> Users { get; private set; }

		public TwitterManager(string key, string secret)
		{
			Auth.SetUserCredentials(Define.CONSUMER, Define.CONSUMER_SECRET, key, secret);
		}

		public void Initialize(List<User> users)
		{
			Users = new Dictionary<string, User>();
			foreach (var user in users) Users[user.ScreenName] = user;
		}

		public List<Web6.Tweet> GetMyTimeline(long since)
		{
			List<Web6.Tweet> list = new List<Web6.Tweet>();
			foreach (var t in Timeline.GetHomeTimeline(200))
			{
				if (t.Id <= since) continue;
				list.Add(new Tweet() { TweetId = t.Id, Date = t.CreatedAt, ScreenName = t.CreatedBy.ScreenName, Text = t.Text });
				Users[t.CreatedBy.ScreenName] = new User() { UserId = t.CreatedBy.Id, ScreenName = t.CreatedBy.ScreenName, Name = t.CreatedBy.Name, ImageUrl = t.CreatedBy.ProfileImageUrl };
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

				try
				{
					string path = GetUserImagePath(dir, user);
					if (File.Exists(path)) continue;
					WebClient client = new WebClient();
					client.DownloadFile(user.ImageUrl, path);
				}
				catch(Exception ex)
				{
					Log.Instance.Error(ex.Message + "@" + ex.StackTrace);	
				}
				life--;
			}
		}
		private string GetUserImagePath(string dir, User user)
		{
			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
			return Path.Combine(dir, user.ScreenName + Path.GetExtension(user.ImageUrl));
		}
	}
}