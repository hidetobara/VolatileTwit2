namespace LearningTweet
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.ButtonNetwork = new System.Windows.Forms.Button();
			this.ButtonDictionary = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ButtonInput = new System.Windows.Forms.Button();
			this.TextBoxInput = new System.Windows.Forms.TextBox();
			this.RadioButtonCsv = new System.Windows.Forms.RadioButton();
			this.RadioButtonKeyValueCsv = new System.Windows.Forms.RadioButton();
			this.ButtonLearn = new System.Windows.Forms.Button();
			this.TextBoxEstimate = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ButtonRun = new System.Windows.Forms.Button();
			this.FolderBrowserDialogMain = new System.Windows.Forms.FolderBrowserDialog();
			this.TextBoxResult = new System.Windows.Forms.TextBox();
			this.ButtonPrepare = new System.Windows.Forms.Button();
			this.TextBoxDictionary = new System.Windows.Forms.TextBox();
			this.TextBoxNetwork = new System.Windows.Forms.TextBox();
			this.BackgroundWorkerLearning = new System.ComponentModel.BackgroundWorker();
			this.ButtonVolatile = new System.Windows.Forms.Button();
			this.BackgroundWorkerVolatile = new System.ComponentModel.BackgroundWorker();
			this.ButtonTweet = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Network Directory";
			// 
			// ButtonNetwork
			// 
			this.ButtonNetwork.Location = new System.Drawing.Point(297, 12);
			this.ButtonNetwork.Name = "ButtonNetwork";
			this.ButtonNetwork.Size = new System.Drawing.Size(75, 23);
			this.ButtonNetwork.TabIndex = 2;
			this.ButtonNetwork.Text = "Directory";
			this.ButtonNetwork.UseVisualStyleBackColor = true;
			this.ButtonNetwork.Click += new System.EventHandler(this.ButtonNetwork_Click);
			// 
			// ButtonDictionary
			// 
			this.ButtonDictionary.Location = new System.Drawing.Point(297, 41);
			this.ButtonDictionary.Name = "ButtonDictionary";
			this.ButtonDictionary.Size = new System.Drawing.Size(75, 23);
			this.ButtonDictionary.TabIndex = 5;
			this.ButtonDictionary.Text = "Directory";
			this.ButtonDictionary.UseVisualStyleBackColor = true;
			this.ButtonDictionary.Click += new System.EventHandler(this.ButtonDictionary_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "Dictionary";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ButtonInput);
			this.groupBox1.Controls.Add(this.TextBoxInput);
			this.groupBox1.Controls.Add(this.RadioButtonCsv);
			this.groupBox1.Controls.Add(this.RadioButtonKeyValueCsv);
			this.groupBox1.Location = new System.Drawing.Point(14, 68);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(358, 77);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Input Type";
			// 
			// ButtonInput
			// 
			this.ButtonInput.Location = new System.Drawing.Point(277, 38);
			this.ButtonInput.Name = "ButtonInput";
			this.ButtonInput.Size = new System.Drawing.Size(75, 23);
			this.ButtonInput.TabIndex = 4;
			this.ButtonInput.Text = "Directory";
			this.ButtonInput.UseVisualStyleBackColor = true;
			this.ButtonInput.Click += new System.EventHandler(this.ButtonInput_Click);
			// 
			// TextBoxInput
			// 
			this.TextBoxInput.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LearningTweet.Properties.Settings.Default, "InputDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.TextBoxInput.Location = new System.Drawing.Point(6, 40);
			this.TextBoxInput.Name = "TextBoxInput";
			this.TextBoxInput.Size = new System.Drawing.Size(265, 19);
			this.TextBoxInput.TabIndex = 3;
			this.TextBoxInput.Text = global::LearningTweet.Properties.Settings.Default.InputDirectory;
			// 
			// RadioButtonCsv
			// 
			this.RadioButtonCsv.AutoSize = true;
			this.RadioButtonCsv.Location = new System.Drawing.Point(111, 18);
			this.RadioButtonCsv.Name = "RadioButtonCsv";
			this.RadioButtonCsv.Size = new System.Drawing.Size(81, 16);
			this.RadioButtonCsv.TabIndex = 1;
			this.RadioButtonCsv.Text = "Legacy csv";
			this.RadioButtonCsv.UseVisualStyleBackColor = true;
			// 
			// RadioButtonKeyValueCsv
			// 
			this.RadioButtonKeyValueCsv.AutoSize = true;
			this.RadioButtonKeyValueCsv.Checked = true;
			this.RadioButtonKeyValueCsv.Location = new System.Drawing.Point(6, 18);
			this.RadioButtonKeyValueCsv.Name = "RadioButtonKeyValueCsv";
			this.RadioButtonKeyValueCsv.Size = new System.Drawing.Size(99, 16);
			this.RadioButtonKeyValueCsv.TabIndex = 0;
			this.RadioButtonKeyValueCsv.TabStop = true;
			this.RadioButtonKeyValueCsv.Text = "Key-Value csv";
			this.RadioButtonKeyValueCsv.UseVisualStyleBackColor = true;
			// 
			// ButtonLearn
			// 
			this.ButtonLearn.Location = new System.Drawing.Point(153, 151);
			this.ButtonLearn.Name = "ButtonLearn";
			this.ButtonLearn.Size = new System.Drawing.Size(75, 23);
			this.ButtonLearn.TabIndex = 7;
			this.ButtonLearn.Text = "Learn";
			this.ButtonLearn.UseVisualStyleBackColor = true;
			this.ButtonLearn.Click += new System.EventHandler(this.ButtonLearn_Click);
			// 
			// TextBoxEstimate
			// 
			this.TextBoxEstimate.Location = new System.Drawing.Point(116, 180);
			this.TextBoxEstimate.Name = "TextBoxEstimate";
			this.TextBoxEstimate.Size = new System.Drawing.Size(175, 19);
			this.TextBoxEstimate.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 183);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 12);
			this.label3.TabIndex = 9;
			this.label3.Text = "Estimate";
			// 
			// ButtonRun
			// 
			this.ButtonRun.Location = new System.Drawing.Point(297, 178);
			this.ButtonRun.Name = "ButtonRun";
			this.ButtonRun.Size = new System.Drawing.Size(75, 23);
			this.ButtonRun.TabIndex = 10;
			this.ButtonRun.Text = "Run";
			this.ButtonRun.UseVisualStyleBackColor = true;
			this.ButtonRun.Click += new System.EventHandler(this.ButtonRun_Click);
			// 
			// TextBoxResult
			// 
			this.TextBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxResult.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.TextBoxResult.Location = new System.Drawing.Point(12, 241);
			this.TextBoxResult.Multiline = true;
			this.TextBoxResult.Name = "TextBoxResult";
			this.TextBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextBoxResult.Size = new System.Drawing.Size(360, 209);
			this.TextBoxResult.TabIndex = 11;
			// 
			// ButtonPrepare
			// 
			this.ButtonPrepare.Location = new System.Drawing.Point(72, 151);
			this.ButtonPrepare.Name = "ButtonPrepare";
			this.ButtonPrepare.Size = new System.Drawing.Size(75, 23);
			this.ButtonPrepare.TabIndex = 12;
			this.ButtonPrepare.Text = "Prepare";
			this.ButtonPrepare.UseVisualStyleBackColor = true;
			this.ButtonPrepare.Click += new System.EventHandler(this.ButtonPrepare_Click);
			// 
			// TextBoxDictionary
			// 
			this.TextBoxDictionary.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LearningTweet.Properties.Settings.Default, "DictionaryDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.TextBoxDictionary.Location = new System.Drawing.Point(116, 43);
			this.TextBoxDictionary.Name = "TextBoxDictionary";
			this.TextBoxDictionary.Size = new System.Drawing.Size(175, 19);
			this.TextBoxDictionary.TabIndex = 4;
			this.TextBoxDictionary.Text = global::LearningTweet.Properties.Settings.Default.DictionaryDirectory;
			// 
			// TextBoxNetwork
			// 
			this.TextBoxNetwork.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LearningTweet.Properties.Settings.Default, "NetworkDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.TextBoxNetwork.Location = new System.Drawing.Point(116, 14);
			this.TextBoxNetwork.Name = "TextBoxNetwork";
			this.TextBoxNetwork.Size = new System.Drawing.Size(175, 19);
			this.TextBoxNetwork.TabIndex = 1;
			this.TextBoxNetwork.Text = global::LearningTweet.Properties.Settings.Default.NetworkDirectory;
			// 
			// BackgroundWorkerLearning
			// 
			this.BackgroundWorkerLearning.WorkerReportsProgress = true;
			this.BackgroundWorkerLearning.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerLearning_DoWork);
			this.BackgroundWorkerLearning.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
			this.BackgroundWorkerLearning.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
			// 
			// ButtonVolatile
			// 
			this.ButtonVolatile.Location = new System.Drawing.Point(72, 205);
			this.ButtonVolatile.Name = "ButtonVolatile";
			this.ButtonVolatile.Size = new System.Drawing.Size(75, 23);
			this.ButtonVolatile.TabIndex = 13;
			this.ButtonVolatile.Text = "Volatile";
			this.ButtonVolatile.UseVisualStyleBackColor = true;
			this.ButtonVolatile.Click += new System.EventHandler(this.ButtonVolatile_Click);
			// 
			// BackgroundWorkerVolatile
			// 
			this.BackgroundWorkerVolatile.WorkerReportsProgress = true;
			this.BackgroundWorkerVolatile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerVolatile_DoWork);
			this.BackgroundWorkerVolatile.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
			this.BackgroundWorkerVolatile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
			// 
			// ButtonTweet
			// 
			this.ButtonTweet.Location = new System.Drawing.Point(153, 205);
			this.ButtonTweet.Name = "ButtonTweet";
			this.ButtonTweet.Size = new System.Drawing.Size(75, 23);
			this.ButtonTweet.TabIndex = 14;
			this.ButtonTweet.Text = "Tweet";
			this.ButtonTweet.UseVisualStyleBackColor = true;
			this.ButtonTweet.Click += new System.EventHandler(this.ButtonTweet_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 462);
			this.Controls.Add(this.ButtonTweet);
			this.Controls.Add(this.ButtonVolatile);
			this.Controls.Add(this.ButtonPrepare);
			this.Controls.Add(this.TextBoxResult);
			this.Controls.Add(this.ButtonRun);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TextBoxEstimate);
			this.Controls.Add(this.ButtonLearn);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ButtonDictionary);
			this.Controls.Add(this.TextBoxDictionary);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ButtonNetwork);
			this.Controls.Add(this.TextBoxNetwork);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.Text = "Learning";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TextBoxNetwork;
		private System.Windows.Forms.Button ButtonNetwork;
		private System.Windows.Forms.Button ButtonDictionary;
		private System.Windows.Forms.TextBox TextBoxDictionary;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button ButtonInput;
		private System.Windows.Forms.TextBox TextBoxInput;
		private System.Windows.Forms.RadioButton RadioButtonCsv;
		private System.Windows.Forms.RadioButton RadioButtonKeyValueCsv;
		private System.Windows.Forms.Button ButtonLearn;
		private System.Windows.Forms.TextBox TextBoxEstimate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button ButtonRun;
		private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialogMain;
		private System.Windows.Forms.TextBox TextBoxResult;
		private System.Windows.Forms.Button ButtonPrepare;
		private System.ComponentModel.BackgroundWorker BackgroundWorkerLearning;
		private System.Windows.Forms.Button ButtonVolatile;
		private System.ComponentModel.BackgroundWorker BackgroundWorkerVolatile;
		private System.Windows.Forms.Button ButtonTweet;
	}
}

