using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Web6
{
	public class Tweet
	{
		[Key]
		public int Id { set; get; }
		[Index]
		public long TweetId { set; get; }
		[Index]
		public DateTime Date { set; get; }
		public string ScreenName { set; get; }
		public string Text { set; get; }
	}

	public class User
	{
		[Key]
		public string ScreenName { set; get; }
		[Index]
		public long UserId { set; get; }
		public string Name { set; get; }
		public string ImageUrl { set; get; }
	}
}