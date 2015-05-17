namespace LocalTweet
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
			this.ButtonMain = new System.Windows.Forms.Button();
			this.TextBoxMain = new System.Windows.Forms.TextBox();
			this.TextBoxInput = new System.Windows.Forms.TextBox();
			this.BackgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
			this.TextBoxSince = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButtonMain
			// 
			this.ButtonMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonMain.Location = new System.Drawing.Point(279, 247);
			this.ButtonMain.Name = "ButtonMain";
			this.ButtonMain.Size = new System.Drawing.Size(75, 23);
			this.ButtonMain.TabIndex = 0;
			this.ButtonMain.Text = "Go";
			this.ButtonMain.UseVisualStyleBackColor = true;
			this.ButtonMain.Click += new System.EventHandler(this.ButtonMain_Click);
			// 
			// TextBoxMain
			// 
			this.TextBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxMain.Location = new System.Drawing.Point(12, 12);
			this.TextBoxMain.Multiline = true;
			this.TextBoxMain.Name = "TextBoxMain";
			this.TextBoxMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextBoxMain.Size = new System.Drawing.Size(342, 195);
			this.TextBoxMain.TabIndex = 1;
			// 
			// TextBoxInput
			// 
			this.TextBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TextBoxInput.Location = new System.Drawing.Point(89, 213);
			this.TextBoxInput.Name = "TextBoxInput";
			this.TextBoxInput.Size = new System.Drawing.Size(60, 19);
			this.TextBoxInput.TabIndex = 2;
			this.TextBoxInput.Text = "yamashiro";
			// 
			// BackgroundWorkerMain
			// 
			this.BackgroundWorkerMain.WorkerReportsProgress = true;
			this.BackgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerMain_DoWork);
			this.BackgroundWorkerMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerMain_ProgressChanged);
			this.BackgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerMain_RunWorkerCompleted);
			// 
			// TextBoxSince
			// 
			this.TextBoxSince.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSince.Location = new System.Drawing.Point(196, 213);
			this.TextBoxSince.Name = "TextBoxSince";
			this.TextBoxSince.Size = new System.Drawing.Size(158, 19);
			this.TextBoxSince.TabIndex = 3;
			this.TextBoxSince.Text = "0";
			this.TextBoxSince.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 216);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "ScreenName:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(155, 216);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "Since:";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(366, 282);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TextBoxSince);
			this.Controls.Add(this.TextBoxInput);
			this.Controls.Add(this.TextBoxMain);
			this.Controls.Add(this.ButtonMain);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.Text = "Tweet All";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonMain;
		private System.Windows.Forms.TextBox TextBoxMain;
		private System.Windows.Forms.TextBox TextBoxInput;
		private System.ComponentModel.BackgroundWorker BackgroundWorkerMain;
		private System.Windows.Forms.TextBox TextBoxSince;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

