using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Tweetinvi;


namespace VolatileTweetLibrary
{
	public class GenerateManager
	{
		string _ScreenName;
		EstimateManager _Estimate;
		VolatileManager _Volatile;

		public GenerateManager(string dir, string screenName, string consumerKey, string consumerSecret, string userKey, string userSecret)
		{
			Auth.SetUserCredentials(consumerKey, consumerSecret, userKey, userSecret);
			_ScreenName = screenName;
			_Estimate = new EstimateManager(dir);
			_Volatile = new VolatileManager(dir, screenName);
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

		public void PublishTweet(int lowerLength = 8, int iterateLimit = 10)
		{
			// 生成
			Dictionary<string, double> texts = new Dictionary<string, double>();
			_Volatile.Load();
			for(int i = 0; i < iterateLimit; i++)
			{
				string text = _Volatile.Tweet();
				if (text.Length < lowerLength) continue;

				StripEstimated s = _Estimate.Compute(_ScreenName, text);
				texts[text] = s.Value;
				if (texts.Count >= 10) break;	// 固定で大丈夫か？
			}
			if (texts.Count == 0) return;
			var top = texts.OrderByDescending(x => x.Value).First();
			// 投稿
			Tweet.PublishTweet(top.Key);
		}
	}
}
