using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using VolatileTweetLibrary;


namespace LearningTweet
{
	public partial class FormMain : Form
	{
		EstimateManager _Learner;
		//VolatileManager _Volatile;
		GenerateManager _Generate;

		public FormMain()
		{
			InitializeComponent();
		}

		private void ButtonNetwork_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(TextBoxNetwork.Text) && Directory.Exists(TextBoxNetwork.Text)) FolderBrowserDialogMain.SelectedPath = TextBoxNetwork.Text;
			if (FolderBrowserDialogMain.ShowDialog() != DialogResult.OK) return;
			TextBoxNetwork.Text = FolderBrowserDialogMain.SelectedPath;
		}

		private void ButtonDictionary_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(TextBoxDictionary.Text) && Directory.Exists(TextBoxDictionary.Text)) FolderBrowserDialogMain.SelectedPath = TextBoxDictionary.Text;
			if (FolderBrowserDialogMain.ShowDialog() != DialogResult.OK) return;
			TextBoxDictionary.Text = FolderBrowserDialogMain.SelectedPath;
		}

		private void ButtonInput_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(TextBoxInput.Text) && Directory.Exists(TextBoxInput.Text)) FolderBrowserDialogMain.SelectedPath = TextBoxInput.Text;
			if (FolderBrowserDialogMain.ShowDialog() != DialogResult.OK) return;
			TextBoxInput.Text = FolderBrowserDialogMain.SelectedPath;
		}

		private void ButtonPrepare_Click(object sender, EventArgs e)
		{
			EnableButtons(false);

			Properties.Settings.Default.Save();
			Log.Instance.Clear();
			PrepareForLearner();
			VolatileTask task = new VolatileTask() { Task = VolatileTask.TaskType.PREPARE, NetworkDir = TextBoxNetwork.Text, InputDir = TextBoxInput.Text };
			BackgroundWorkerLearning.RunWorkerAsync(task);
		}

		private void ButtonLearn_Click(object sender, EventArgs e)
		{
			EnableButtons(false);

			Properties.Settings.Default.Save();
			Log.Instance.Clear();
			PrepareForLearner();
			VolatileTask task = new VolatileTask() { Task = VolatileTask.TaskType.LEARN_WORD, NetworkDir = TextBoxNetwork.Text, InputDir = TextBoxInput.Text };
			BackgroundWorkerLearning.RunWorkerAsync(task);
		}

		private void ButtonRun_Click(object sender, EventArgs e)
		{
			EnableButtons(false);

			Log.Instance.Clear();
			PrepareForLearner();
			VolatileTask task = new VolatileTask() { Task = VolatileTask.TaskType.ESTIMATE, NetworkDir = TextBoxNetwork.Text, InputDir = TextBoxInput.Text };
			task.Input = TextBoxEstimate.Text;
			BackgroundWorkerLearning.RunWorkerAsync(task);
		}

		private void PrepareForLearner()
		{
			MorphemeManager.Instance.Initialize(TextBoxDictionary.Text);

			if (_Learner == null) _Learner = new EstimateManager(TextBoxNetwork.Text);
		}

		private void BackgroundWorkerLearning_DoWork(object sender, DoWorkEventArgs e)
		{
			VolatileTask task = e.Argument as VolatileTask;
			if (task == null) return;

			IEnumerator enumerator = null;
			if (task.Task == VolatileTask.TaskType.PREPARE) enumerator = _Learner.Prepare(task.InputDir);
			else if (task.Task == VolatileTask.TaskType.LEARN_WORD) enumerator = _Learner.Learn(task.InputDir);
			else if (task.Task == VolatileTask.TaskType.ESTIMATE) _Learner.Compute("shokos", task.Input);
			if (task == null || enumerator == null) return;

			while (enumerator.MoveNext()) BackgroundWorkerLearning.ReportProgress(0);
		}
		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			TextBoxResult.Text = Log.Instance.Get();
		}

		private void ButtonVolatile_Click(object sender, EventArgs e)
		{
			EnableButtons(false);

			var task = new VolatileTask() { Task = VolatileTask.TaskType.LEARN_VOLATILE, ScreenName = "shokos" };
			task.InputDir = TextBoxInput.Text;
			task.NetworkDir = TextBoxNetwork.Text;

			Log.Instance.Clear();
			PrepareForLearner();
			BackgroundWorkerVolatile.RunWorkerAsync(task);
		}

		private void ButtonTweet_Click(object sender, EventArgs e)
		{
			var task = new VolatileTask() { Task = VolatileTask.TaskType.TWEET, ScreenName = "shokos" };
			task.InputDir = TextBoxInput.Text;
			task.NetworkDir = TextBoxNetwork.Text;

			Log.Instance.Clear();
			PrepareForLearner();
			BackgroundWorkerVolatile.RunWorkerAsync(task);
		}

		private void BackgroundWorkerVolatile_DoWork(object sender, DoWorkEventArgs e)
		{
			VolatileTask task = e.Argument as VolatileTask;
			if (task == null) return;

			_Generate = new GenerateManager(task.NetworkDir, task.ScreenName, Define.CONSUMER, Define.CONSUMER_SECRET, Define.ACCESS, Define.ACCESS_SECRET);
			if (task.Task == VolatileTask.TaskType.LEARN_VOLATILE)
			{
				IEnumerator enumerator = _Generate.LearnByLocal(task.InputDir);
				while (enumerator.MoveNext()) BackgroundWorkerVolatile.ReportProgress(0);
				_Generate.Save();
			}
			else if(task.Task == VolatileTask.TaskType.TWEET)
			{
				_Generate.PublishTweet();
			}
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			EnableButtons(true);
			TextBoxResult.Text = Log.Instance.Get();
		}

		private void EnableButtons(bool enable)
		{
			ButtonPrepare.Enabled = enable;
			ButtonLearn.Enabled = enable;
			ButtonRun.Enabled = enable;
			ButtonVolatile.Enabled = enable;
			ButtonTweet.Enabled = enable;
		}
	}
}
