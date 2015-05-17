using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace VolatileTweetLibrary
{
	public class Strip
	{
		public string ScreenName;
		public string Text;
	}

	public class StripEstimated : Strip
	{
		public double Value;
	}

	public class StripManager
	{
		public List<Strip> LoadStrips(string path)
		{
			List<Strip> strips = new List<Strip>();
			StreamReader reader = null;
			try
			{
				reader = new StreamReader(path);
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					string[] blocks = line.Split(',');
					Strip s = new Strip();
					for (int i = 0; i < blocks.Length; i++)
					{
						string block = blocks[i];
						if (string.IsNullOrEmpty(block)) continue;
						string[] cells = block.Split('=');
						if (cells.Length < 2) continue;
						if (cells[0] == "user_screen_name") s.ScreenName = cells[1];
						if (cells[0] == "text") s.Text = cells[1];
					}
					if (!string.IsNullOrEmpty(s.Text)) strips.Add(s);
				}
			}
			catch (Exception ex)
			{
				Log.Instance.Error(ex.Message + "@" + ex.StackTrace);
			}
			finally
			{
				if (reader != null) reader.Close();
			}
			return strips;
		}
	}
}
