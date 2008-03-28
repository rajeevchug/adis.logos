namespace WinFormsListener
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
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
			this.listView1 = new WinFormsListener.MyListView();
			this.Application = new System.Windows.Forms.ColumnHeader();
			this.CategoryHeader = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(531, 48);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Listener Status";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// checkBoxAutoScroll
			// 
			this.checkBoxAutoScroll.AutoSize = true;
			this.checkBoxAutoScroll.Checked = true;
			this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxAutoScroll.Location = new System.Drawing.Point(13, 96);
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
			listViewGroup1.Header = "ListViewGroup";
			listViewGroup1.Name = "listViewGroup1";
			listViewGroup2.Header = "ListViewGroup";
			listViewGroup2.Name = "listViewGroup2";
			this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
			this.listView1.Location = new System.Drawing.Point(12, 119);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(957, 479);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(981, 610);
			this.Controls.Add(this.checkBoxAutoScroll);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listView1);
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxAutoScroll;
		private System.Windows.Forms.ColumnHeader CategoryHeader;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
	}
}

