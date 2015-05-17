using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tweetinvi;
using Tweetinvi.Core;


namespace VolatileTweetLibrary
{
	class TweetManager
	{
		string _ScreenName;
		EstimateManager _Estimate;
		VolatileManager _Volatile;

		public TweetManager(string dir, string screenName, string consumer, string consumerSecret, string user, string userSecret)
		{
			TwitterCredentials.SetCredentials(user, userSecret, consumer, consumerSecret);
			_ScreenName = screenName;
			_Estimate = new EstimateManager(dir);
			_Volatile = new VolatileManager(dir, screenName);
		}

		public void GetTimeline()
		{
			// 取得

			// 計算

		}

		public void PublishTweet()
		{
			// 生成
			Dictionary<string, double> texts = new Dictionary<string, double>();
			_Volatile.Load();
			for(int i = 0; i < 3; i++)
			{
				string text = _Volatile.Tweet();
				StripEstimated s = _Estimate.Compute(_ScreenName, text);
				texts.Add(text, s.Value);
			}
			var top = texts.OrderByDescending(x => x.Value).First();
			// 投稿
			//Tweet.PublishTweet(top.Key);
		}
	}
}
