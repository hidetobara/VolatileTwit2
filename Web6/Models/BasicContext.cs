using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Web6.Models
{
	public class BasicContext : DbContext
	{
		public DbSet<Tweet> Tweets { set; get; }
		public DbSet<User> Users { set; get; }

		public long SelectLastTweetId()
		{
			if (Tweets.Count() == 0) return 0;
			return Tweets.Max(record => record.TweetId);
		}
		
		public DateTime SelectLastTweet()
		{
			if (Tweets.Count() == 0) return DateTime.MinValue;
			return Tweets.Max(record => record.Date);
		}

		public List<Tweet> SelectRecentTweets(int count = 30)
		{
			var list = Tweets.OrderByDescending(record => record.Date).Take(count);
			return new List<Tweet>(list);
		}

		public List<Tweet> Select1DayTweets(int limit = 30)
		{
			DateTime now = DateTime.Now;
			DateTime day1ago = now.Subtract(new TimeSpan(1, 0, 0, 0));
			var list = Tweets.Where(record => day1ago < record.Date).Take(limit);
			return new List<Tweet>(list);
		}

		public void Insert(List<Tweet> tweets)
		{
			Tweets.AddRange(tweets);
			SaveChanges();
		}

		public void UpdateOrInsert(List<User> users)
		{
			bool isChanged = false;
			foreach (var user in users)
			{
				var q = (from r in Users where r.ScreenName == user.ScreenName select r).FirstOrDefault();
				if(q == null)
				{
					Users.Add(user);
					isChanged = true;
				}
				else if (q.Name != user.Name || q.ImageUrl != user.ImageUrl)
				{
					q.Name = user.Name;
					q.ImageUrl = user.ImageUrl;
					isChanged = true;
				}
			}
			if (isChanged) SaveChanges();
		}
	}
}