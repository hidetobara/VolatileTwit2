using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalTweet
{
	public partial class FormMain : Form
	{
		TwitterManager _Manager = new TwitterManager();

		public FormMain()
		{
			InitializeComponent();
			_Manager.Initialize();
		}

		private void ButtonMain_Click(object sender, EventArgs e)
		{
			long since = 0;
			long.TryParse(TextBoxSince.Text, out since);
			BackgroundWorkerMain.RunWorkerAsync(new TaskBackground() { Name = TextBoxInput.Text, Since = since });
		}

		private string _Log;
		private void BackgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
		{
			TaskBackground t = e.Argument as TaskBackground;
			if (t == null) return;

			IEnumerator enumrator = _Manager.GetUserTimeline(t.Name, t.Since);
			while(enumrator.MoveNext())
			{
				_Log += enumrator.Current as string;
				BackgroundWorkerMain.ReportProgress(0);
			}
		}

		private void BackgroundWorkerMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			TextBoxMain.Text = _Log;
		}

		private void BackgroundWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			TextBoxMain.Text = _Log + "#Done";
		}

		private class TaskBackground
		{
			public string Name;
			public long Since;
		}

	}
}
