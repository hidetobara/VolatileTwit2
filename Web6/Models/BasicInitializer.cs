using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Web6.Models
{
//	public class BasicInitializer : DropCreateDatabaseIfModelChanges<BasicContext>
	public class BasicInitializer : CreateDatabaseIfNotExists<BasicContext>
	{
		protected override void Seed(BasicContext context)
		{
			base.Seed(context);
			//context.Tweets.Add(new Tweet() { Id = 1, Date = DateTime.Now, ScreenName = "Bob", Text = "Hello World !" });
			context.SaveChanges();
		}

	}
}