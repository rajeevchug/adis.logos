namespace Adis.Log.Listener.WinForms
{
	partial class MainForm
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
			System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.categoryLabel = new System.Windows.Forms.LinkLabel();
			this.messageLabel = new System.Windows.Forms.LinkLabel();
			this.startTimeLabel = new System.Windows.Forms.LinkLabel();
			this.severityLabel = new System.Windows.Forms.LinkLabel();
			this.userLabel = new System.Windows.Forms.LinkLabel();
			this.machineLabel = new System.Windows.Forms.LinkLabel();
			this.instanceLabel = new System.Windows.Forms.LinkLabel();
			this.applicationLabel = new System.Windows.Forms.LinkLabel();
			this.label10 = new System.Windows.Forms.Label();
			this.ErrorDetails = new System.Windows.Forms.TextBox();
			this.filterSeverity = new System.Windows.Forms.ComboBox();
			this.filterStartTime = new System.Windows.Forms.DateTimePicker();
			this.filterMessage = new System.Windows.Forms.TextBox();
			this.filterMessageExactMatch = new System.Windows.Forms.CheckBox();
			this.filterUser = new System.Windows.Forms.TextBox();
			this.filterUserExactMatch = new System.Windows.Forms.CheckBox();
			this.filterMachine = new System.Windows.Forms.TextBox();
			this.filterMachineExactMatch = new System.Windows.Forms.CheckBox();
			this.filterInstance = new System.Windows.Forms.TextBox();
			this.filterInstanceExactMatch = new System.Windows.Forms.CheckBox();
			this.filterApplication = new System.Windows.Forms.TextBox();
			this.filterApplicationExactMatch = new System.Windows.Forms.CheckBox();
			this.filterCategory = new System.Windows.Forms.TextBox();
			this.filterCategoryExactMatch = new System.Windows.Forms.CheckBox();
			this.btnReconnect = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.statusLabel = new System.Windows.Forms.Label();
			this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
			this.listView1 = new Adis.Log.Listener.WinForms.MyListView();
			this.Application = new System.Windows.Forms.ColumnHeader();
			this.CategoryHeader = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.btnClearList = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.categoryLabel);
			this.groupBox1.Controls.Add(this.messageLabel);
			this.groupBox1.Controls.Add(this.startTimeLabel);
			this.groupBox1.Controls.Add(this.severityLabel);
			this.groupBox1.Controls.Add(this.userLabel);
			this.groupBox1.Controls.Add(this.machineLabel);
			this.groupBox1.Controls.Add(this.instanceLabel);
			this.groupBox1.Controls.Add(this.applicationLabel);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.ErrorDetails);
			this.groupBox1.Controls.Add(this.filterSeverity);
			this.groupBox1.Controls.Add(this.filterStartTime);
			this.groupBox1.Controls.Add(this.filterMessage);
			this.groupBox1.Controls.Add(this.filterMessageExactMatch);
			this.groupBox1.Controls.Add(this.filterUser);
			this.groupBox1.Controls.Add(this.filterUserExactMatch);
			this.groupBox1.Controls.Add(this.filterMachine);
			this.groupBox1.Controls.Add(this.filterMachineExactMatch);
			this.groupBox1.Controls.Add(this.filterInstance);
			this.groupBox1.Controls.Add(this.filterInstanceExactMatch);
			this.groupBox1.Controls.Add(this.filterApplication);
			this.groupBox1.Controls.Add(this.filterApplicationExactMatch);
			this.groupBox1.Controls.Add(this.filterCategory);
			this.groupBox1.Controls.Add(this.filterCategoryExactMatch);
			this.groupBox1.Controls.Add(this.btnReconnect);
			this.groupBox1.Controls.Add(this.progressBar1);
			this.groupBox1.Controls.Add(this.statusLabel);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(895, 277);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Listener Status";
			// 
			// categoryLabel
			// 
			this.categoryLabel.AutoSize = true;
			this.categoryLabel.Location = new System.Drawing.Point(21, 70);
			this.categoryLabel.Name = "categoryLabel";
			this.categoryLabel.Size = new System.Drawing.Size(49, 13);
			this.categoryLabel.TabIndex = 11;
			this.categoryLabel.TabStop = true;
			this.categoryLabel.Text = "Category";
			this.categoryLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CategoryLabel_LinkClicked);
			// 
			// messageLabel
			// 
			this.messageLabel.AutoSize = true;
			this.messageLabel.Location = new System.Drawing.Point(20, 200);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new System.Drawing.Size(50, 13);
			this.messageLabel.TabIndex = 11;
			this.messageLabel.TabStop = true;
			this.messageLabel.Text = "Message";
			this.messageLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MessageLabel_LinkClicked);
			// 
			// startTimeLabel
			// 
			this.startTimeLabel.AutoSize = true;
			this.startTimeLabel.Location = new System.Drawing.Point(15, 224);
			this.startTimeLabel.Name = "startTimeLabel";
			this.startTimeLabel.Size = new System.Drawing.Size(55, 13);
			this.startTimeLabel.TabIndex = 11;
			this.startTimeLabel.TabStop = true;
			this.startTimeLabel.Text = "Start Time";
			this.startTimeLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StartTimeLabel_LinkClicked);
			// 
			// severityLabel
			// 
			this.severityLabel.AutoSize = true;
			this.severityLabel.Location = new System.Drawing.Point(25, 253);
			this.severityLabel.Name = "severityLabel";
			this.severityLabel.Size = new System.Drawing.Size(45, 13);
			this.severityLabel.TabIndex = 11;
			this.severityLabel.TabStop = true;
			this.severityLabel.Text = "Severity";
			this.severityLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SeverityLabel_LinkClicked);
			// 
			// userLabel
			// 
			this.userLabel.AutoSize = true;
			this.userLabel.Location = new System.Drawing.Point(41, 174);
			this.userLabel.Name = "userLabel";
			this.userLabel.Size = new System.Drawing.Size(29, 13);
			this.userLabel.TabIndex = 11;
			this.userLabel.TabStop = true;
			this.userLabel.Text = "User";
			this.userLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UserLabel_LinkClicked);
			// 
			// machineLabel
			// 
			this.machineLabel.AutoSize = true;
			this.machineLabel.Location = new System.Drawing.Point(22, 145);
			this.machineLabel.Name = "machineLabel";
			this.machineLabel.Size = new System.Drawing.Size(48, 13);
			this.machineLabel.TabIndex = 11;
			this.machineLabel.TabStop = true;
			this.machineLabel.Text = "Machine";
			this.machineLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MachineLabel_LinkClicked);
			// 
			// instanceLabel
			// 
			this.instanceLabel.AutoSize = true;
			this.instanceLabel.Location = new System.Drawing.Point(22, 122);
			this.instanceLabel.Name = "instanceLabel";
			this.instanceLabel.Size = new System.Drawing.Size(48, 13);
			this.instanceLabel.TabIndex = 11;
			this.instanceLabel.TabStop = true;
			this.instanceLabel.Text = "Instance";
			this.instanceLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.InstanceLabel_LinkClicked);
			// 
			// applicationLabel
			// 
			this.applicationLabel.AutoSize = true;
			this.applicationLabel.Location = new System.Drawing.Point(11, 96);
			this.applicationLabel.Name = "applicationLabel";
			this.applicationLabel.Size = new System.Drawing.Size(59, 13);
			this.applicationLabel.TabIndex = 11;
			this.applicationLabel.TabStop = true;
			this.applicationLabel.Text = "Application";
			this.applicationLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ApplicationLabel_LinkClicked);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(348, 71);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(121, 13);
			this.label10.TabIndex = 10;
			this.label10.Text = "Connection Error Details";
			// 
			// ErrorDetails
			// 
			this.ErrorDetails.Location = new System.Drawing.Point(348, 93);
			this.ErrorDetails.Multiline = true;
			this.ErrorDetails.Name = "ErrorDetails";
			this.ErrorDetails.Size = new System.Drawing.Size(541, 173);
			this.ErrorDetails.TabIndex = 9;
			// 
			// filterSeverity
			// 
			this.filterSeverity.Enabled = false;
			this.filterSeverity.FormattingEnabled = true;
			this.filterSeverity.Location = new System.Drawing.Point(76, 250);
			this.filterSeverity.Name = "filterSeverity";
			this.filterSeverity.Size = new System.Drawing.Size(100, 21);
			this.filterSeverity.TabIndex = 8;
			// 
			// filterStartTime
			// 
			this.filterStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			this.filterStartTime.Enabled = false;
			this.filterStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.filterStartTime.Location = new System.Drawing.Point(79, 224);
			this.filterStartTime.Name = "filterStartTime";
			this.filterStartTime.Size = new System.Drawing.Size(122, 20);
			this.filterStartTime.TabIndex = 7;
			// 
			// filterMessage
			// 
			this.filterMessage.Enabled = false;
			this.filterMessage.Location = new System.Drawing.Point(76, 197);
			this.filterMessage.Name = "filterMessage";
			this.filterMessage.Size = new System.Drawing.Size(100, 20);
			this.filterMessage.TabIndex = 6;
			// 
			// filterMessageExactMatch
			// 
			this.filterMessageExactMatch.AutoSize = true;
			this.filterMessageExactMatch.Enabled = false;
			this.filterMessageExactMatch.Location = new System.Drawing.Point(182, 200);
			this.filterMessageExactMatch.Name = "filterMessageExactMatch";
			this.filterMessageExactMatch.Size = new System.Drawing.Size(86, 17);
			this.filterMessageExactMatch.TabIndex = 5;
			this.filterMessageExactMatch.Text = "Exact Match";
			this.filterMessageExactMatch.UseVisualStyleBackColor = true;
			// 
			// filterUser
			// 
			this.filterUser.Enabled = false;
			this.filterUser.Location = new System.Drawing.Point(76, 171);
			this.filterUser.Name = "filterUser";
			this.filterUser.Size = new System.Drawing.Size(100, 20);
			this.filterUser.TabIndex = 6;
			// 
			// filterUserExactMatch
			// 
			this.filterUserExactMatch.AutoSize = true;
			this.filterUserExactMatch.Enabled = false;
			this.filterUserExactMatch.Location = new System.Drawing.Point(182, 174);
			this.filterUserExactMatch.Name = "filterUserExactMatch";
			this.filterUserExactMatch.Size = new System.Drawing.Size(86, 17);
			this.filterUserExactMatch.TabIndex = 5;
			this.filterUserExactMatch.Text = "Exact Match";
			this.filterUserExactMatch.UseVisualStyleBackColor = true;
			// 
			// filterMachine
			// 
			this.filterMachine.Enabled = false;
			this.filterMachine.Location = new System.Drawing.Point(76, 145);
			this.filterMachine.Name = "filterMachine";
			this.filterMachine.Size = new System.Drawing.Size(100, 20);
			this.filterMachine.TabIndex = 6;
			// 
			// filterMachineExactMatch
			// 
			this.filterMachineExactMatch.AutoSize = true;
			this.filterMachineExactMatch.Enabled = false;
			this.filterMachineExactMatch.Location = new System.Drawing.Point(182, 148);
			this.filterMachineExactMatch.Name = "filterMachineExactMatch";
			this.filterMachineExactMatch.Size = new System.Drawing.Size(86, 17);
			this.filterMachineExactMatch.TabIndex = 5;
			this.filterMachineExactMatch.Text = "Exact Match";
			this.filterMachineExactMatch.UseVisualStyleBackColor = true;
			// 
			// filterInstance
			// 
			this.filterInstance.Enabled = false;
			this.filterInstance.Location = new System.Drawing.Point(76, 119);
			this.filterInstance.Name = "filterInstance";
			this.filterInstance.Size = new System.Drawing.Size(100, 20);
			this.filterInstance.TabIndex = 6;
			// 
			// filterInstanceExactMatch
			// 
			this.filterInstanceExactMatch.AutoSize = true;
			this.filterInstanceExactMatch.Enabled = false;
			this.filterInstanceExactMatch.Location = new System.Drawing.Point(182, 122);
			this.filterInstanceExactMatch.Name = "filterInstanceExactMatch";
			this.filterInstanceExactMatch.Size = new System.Drawing.Size(86, 17);
			this.filterInstanceExactMatch.TabIndex = 5;
			this.filterInstanceExactMatch.Text = "Exact Match";
			this.filterInstanceExactMatch.UseVisualStyleBackColor = true;
			// 
			// filterApplication
			// 
			this.filterApplication.Enabled = false;
			this.filterApplication.Location = new System.Drawing.Point(76, 93);
			this.filterApplication.Name = "filterApplication";
			this.filterApplication.Size = new System.Drawing.Size(100, 20);
			this.filterApplication.TabIndex = 6;
			// 
			// filterApplicationExactMatch
			// 
			this.filterApplicationExactMatch.AutoSize = true;
			this.filterApplicationExactMatch.Enabled = false;
			this.filterApplicationExactMatch.Location = new System.Drawing.Point(182, 96);
			this.filterApplicationExactMatch.Name = "filterApplicationExactMatch";
			this.filterApplicationExactMatch.Size = new System.Drawing.Size(86, 17);
			this.filterApplicationExactMatch.TabIndex = 5;
			this.filterApplicationExactMatch.Text = "Exact Match";
			this.filterApplicationExactMatch.UseVisualStyleBackColor = true;
			// 
			// filterCategory
			// 
			this.filterCategory.Enabled = false;
			this.filterCategory.Location = new System.Drawing.Point(76, 67);
			this.filterCategory.Name = "filterCategory";
			this.filterCategory.Size = new System.Drawing.Size(100, 20);
			this.filterCategory.TabIndex = 6;
			// 
			// filterCategoryExactMatch
			// 
			this.filterCategoryExactMatch.AutoSize = true;
			this.filterCategoryExactMatch.Enabled = false;
			this.filterCategoryExactMatch.Location = new System.Drawing.Point(182, 70);
			this.filterCategoryExactMatch.Name = "filterCategoryExactMatch";
			this.filterCategoryExactMatch.Size = new System.Drawing.Size(86, 17);
			this.filterCategoryExactMatch.TabIndex = 5;
			this.filterCategoryExactMatch.Text = "Exact Match";
			this.filterCategoryExactMatch.UseVisualStyleBackColor = true;
			// 
			// btnReconnect
			// 
			this.btnReconnect.Location = new System.Drawing.Point(231, 224);
			this.btnReconnect.Name = "btnReconnect";
			this.btnReconnect.Size = new System.Drawing.Size(91, 42);
			this.btnReconnect.TabIndex = 4;
			this.btnReconnect.Text = "Reconnect";
			this.btnReconnect.UseVisualStyleBackColor = true;
			this.btnReconnect.Click += new System.EventHandler(this.btnReconnect_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(10, 36);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(879, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 3;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.ForeColor = System.Drawing.Color.Red;
			this.statusLabel.Location = new System.Drawing.Point(7, 20);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(131, 13);
			this.statusLabel.TabIndex = 0;
			this.statusLabel.Text = "Not connected to a server";
			// 
			// checkBoxAutoScroll
			// 
			this.checkBoxAutoScroll.AutoSize = true;
			this.checkBoxAutoScroll.Checked = true;
			this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxAutoScroll.Location = new System.Drawing.Point(13, 296);
			this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
			this.checkBoxAutoScroll.Size = new System.Drawing.Size(241, 17);
			this.checkBoxAutoScroll.TabIndex = 2;
			this.checkBoxAutoScroll.Text = "Automatically scroll to newly added log entries";
			this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Application,
            this.CategoryHeader,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.listView1.FullRowSelect = true;
			listViewGroup5.Header = "ListViewGroup";
			listViewGroup5.Name = "listViewGroup1";
			listViewGroup6.Header = "ListViewGroup";
			listViewGroup6.Name = "listViewGroup2";
			this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6});
			this.listView1.Location = new System.Drawing.Point(12, 319);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(896, 404);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			// 
			// Application
			// 
			this.Application.Text = "Application";
			this.Application.Width = 116;
			// 
			// CategoryHeader
			// 
			this.CategoryHeader.Text = "Category";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Time";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "User";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Machine";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Severity";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Message";
			this.columnHeader5.Width = 387;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Extra Info";
			this.columnHeader6.Width = 367;
			// 
			// btnClearList
			// 
			this.btnClearList.Location = new System.Drawing.Point(810, 296);
			this.btnClearList.Name = "btnClearList";
			this.btnClearList.Size = new System.Drawing.Size(92, 23);
			this.btnClearList.TabIndex = 4;
			this.btnClearList.Text = "Clear List";
			this.btnClearList.UseVisualStyleBackColor = true;
			this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 735);
			this.Controls.Add(this.checkBoxAutoScroll);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btnClearList);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MyListView listView1;
		private System.Windows.Forms.ColumnHeader Application;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.CheckBox checkBoxAutoScroll;
		private System.Windows.Forms.ColumnHeader CategoryHeader;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button btnReconnect;
		private System.Windows.Forms.TextBox filterCategory;
		private System.Windows.Forms.CheckBox filterCategoryExactMatch;
		private System.Windows.Forms.TextBox filterUser;
		private System.Windows.Forms.CheckBox filterUserExactMatch;
		private System.Windows.Forms.TextBox filterMachine;
		private System.Windows.Forms.CheckBox filterMachineExactMatch;
		private System.Windows.Forms.TextBox filterInstance;
		private System.Windows.Forms.CheckBox filterInstanceExactMatch;
		private System.Windows.Forms.TextBox filterApplication;
		private System.Windows.Forms.CheckBox filterApplicationExactMatch;
		private System.Windows.Forms.ComboBox filterSeverity;
		private System.Windows.Forms.DateTimePicker filterStartTime;
		private System.Windows.Forms.TextBox filterMessage;
		private System.Windows.Forms.CheckBox filterMessageExactMatch;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox ErrorDetails;
		private System.Windows.Forms.LinkLabel categoryLabel;
		private System.Windows.Forms.LinkLabel startTimeLabel;
		private System.Windows.Forms.LinkLabel severityLabel;
		private System.Windows.Forms.LinkLabel userLabel;
		private System.Windows.Forms.LinkLabel machineLabel;
		private System.Windows.Forms.LinkLabel instanceLabel;
		private System.Windows.Forms.LinkLabel applicationLabel;
		private System.Windows.Forms.LinkLabel messageLabel;
		private System.Windows.Forms.Button btnClearList;
	}
}

