using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Tweetinvi;
using Tweetinvi.Core;


namespace VolatileTweetLibrary
{
	public class GenerateManager
	{
		string _ScreenName;
		EstimateManager _Estimate;
		VolatileManager _Volatile;

		public GenerateManager(string dir, string screenName, string consumer, string consumerSecret, string user, string userSecret)
		{
			TwitterCredentials.SetCredentials(user, userSecret, consumer, consumerSecret);
			_ScreenName = screenName;
			_Estimate = new EstimateManager(dir);
			_Volatile = new VolatileManager(dir, screenName);
		}

		public void LearnByTimeline()
		{
			// 取得

			// 計算

		}

		public IEnumerator LearnByLocal(string dir)
		{
			_Volatile.Load();
			return _Volatile.LearningMatrix(dir);
		}

		public void Save()
		{
			_Volatile.Save();
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
