namespace WinFormsListener
{
	partial class DetailsView
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tbExtraInfo = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.lblApplication = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblMachine = new System.Windows.Forms.Label();
			this.lblUser = new System.Windows.Forms.Label();
			this.lblSeverity = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tbExtraInfo);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(353, 99);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Extra Info";
			// 
			// tbExtraInfo
			// 
			this.tbExtraInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbExtraInfo.Location = new System.Drawing.Point(3, 16);
			this.tbExtraInfo.Multiline = true;
			this.tbExtraInfo.Name = "tbExtraInfo";
			this.tbExtraInfo.ReadOnly = true;
			this.tbExtraInfo.Size = new System.Drawing.Size(347, 80);
			this.tbExtraInfo.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 170);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(353, 205);
			this.splitContainer1.SplitterDistance = 102;
			this.splitContainer1.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tbMessage);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(353, 102);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Message";
			// 
			// tbMessage
			// 
			this.tbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbMessage.Location = new System.Drawing.Point(3, 16);
			this.tbMessage.Multiline = true;
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.ReadOnly = true;
			this.tbMessage.Size = new System.Drawing.Size(347, 83);
			this.tbMessage.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Category";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Application";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Time";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Machine";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 114);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "User";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 140);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Severity";
			// 
			// lblCategory
			// 
			this.lblCategory.AutoSize = true;
			this.lblCategory.Location = new System.Drawing.Point(90, 9);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(35, 13);
			this.lblCategory.TabIndex = 3;
			this.lblCategory.Text = "label7";
			// 
			// lblApplication
			// 
			this.lblApplication.AutoSize = true;
			this.lblApplication.Location = new System.Drawing.Point(90, 37);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(35, 13);
			this.lblApplication.TabIndex = 3;
			this.lblApplication.Text = "label7";
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(90, 62);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(35, 13);
			this.lblTime.TabIndex = 3;
			this.lblTime.Text = "label7";
			// 
			// lblMachine
			// 
			this.lblMachine.AutoSize = true;
			this.lblMachine.Location = new System.Drawing.Point(90, 89);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(35, 13);
			this.lblMachine.TabIndex = 3;
			this.lblMachine.Text = "label7";
			// 
			// lblUser
			// 
			this.lblUser.AutoSize = true;
			this.lblUser.Location = new System.Drawing.Point(90, 114);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(35, 13);
			this.lblUser.TabIndex = 3;
			this.lblUser.Text = "label7";
			// 
			// lblSeverity
			// 
			this.lblSeverity.AutoSize = true;
			this.lblSeverity.Location = new System.Drawing.Point(90, 140);
			this.lblSeverity.Name = "lblSeverity";
			this.lblSeverity.Size = new System.Drawing.Size(35, 13);
			this.lblSeverity.TabIndex = 3;
			this.lblSeverity.Text = "label7";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(290, 381);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Ok";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// DetailsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(377, 416);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lblSeverity);
			this.Controls.Add(this.lblUser);
			this.Controls.Add(this.lblMachine);
			this.Controls.Add(this.lblTime);
			this.Controls.Add(this.lblApplication);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.splitContainer1);
			this.Name = "DetailsView";
			this.Text = "DetailsView";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbExtraInfo;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Label lblMachine;
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.Label lblSeverity;
		private System.Windows.Forms.Button button1;
	}
}