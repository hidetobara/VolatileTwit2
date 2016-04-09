using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Web6.Controllers
{
    public class ServiceLineController : Controller
    {
		private string LogDir { get { return HttpContext.Server.MapPath("~/App_Data/line/logs/"); } }

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Callback()
		{
			try
			{
				string log = "";
				foreach (string key in Request.Headers.Keys) { log += key + "=" + Request.Headers[key] + Environment.NewLine; }
				log += Environment.NewLine;

				string body = new StreamReader(Request.InputStream).ReadToEnd();
				log += body + Environment.NewLine;

				DateTime date = DateTime.Now;
				string localdir = date.ToString("yyyyMM/dd/");
				string filename = date.ToString("HHmmss") + ".log";

				string dir = Path.Combine(LogDir, localdir);
				if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

				string path = Path.Combine(dir, filename);
				System.IO.File.WriteAllText(path, log);
				ViewBag.Message = "OK";
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message + "@" + ex.StackTrace;
				ViewBag.Error = ex.Message;
			}
			return View();
		}
    }
}