using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Adis.Log.Contract;
using System.Threading;

namespace WinFormsListener
{
	public partial class MainForm : Form
	{

		public LogObjectList ListOfLogObjects;
		private Thread bgThread;

		public MainForm()
		{
			InitializeComponent();
			ListOfLogObjects = new LogObjectList();

			listView1.VirtualMode = true;
			listView1.VirtualListSize = ListOfLogObjects.Count;
			listView1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);

			bgThread = new Thread(new ThreadStart(BgThreadStart));
			//bgThread.Start();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			KillBgThread();
			base.OnClosing(e);
		}

		void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if (e.ItemIndex < ListOfLogObjects.Count)
			{
				e.Item = LogObjectToListItem(ListOfLogObjects[e.ItemIndex]);
			}
			else
			{
				e.Item = new ListViewItem();
				e.Item.SubItems.Add("");
				e.Item.SubItems.Add("");
				e.Item.SubItems.Add("");
				e.Item.SubItems.Add("");
				e.Item.SubItems.Add("");
			}
		}

		private ListViewItem LogObjectToListItem(LogTransportObject logObject)
		{
			ListViewItem item = new ListViewItem();
			item.Text = logObject.Application;
			item.SubItems.Add(logObject.Category);
			item.SubItems.Add(logObject.Time.ToString());
			item.SubItems.Add(logObject.User);
			item.SubItems.Add(logObject.Machine);
			item.SubItems.Add(logObject.Severity);
			item.SubItems.Add(logObject.Message);
			item.SubItems.Add(logObject.ExtraInfo);
			return item;
		}

		public void ResetVirtualListSize()
		{
			listView1.VirtualListSize = ListOfLogObjects.Count;
			if (checkBoxAutoScroll.Checked)
			{
				listView1.EnsureVisible(ListOfLogObjects.Count - 1);
			}
		}

		public String StatusText
		{
			get { return label1.Text; }
			set { label1.Text = value; }

		}
		delegate void UpdateListView();

		private void BgThreadStart()
		{
			for (int i = 0; i < 1000; i++)
			{
				Thread.Sleep(50);
				lock (ListOfLogObjects)
				{
					LogTransportObject logObject = new LogTransportObject()
					{
						Application = "Application",
						Category = "Category",
						Severity = "Severity",
						ExtraInfo = "ExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfoExtraInfo",
						Message = "MessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessage",
						Time = DateTime.Now,
						Instance = "Instance",
						Machine = "Machine",
						User = "User"
					};

					ListOfLogObjects.Add(logObject);

				}
			}


		}


		public void KillBgThread()
		{
			bgThread.Abort();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewItem selected = listView1.GetItemAt(e.X, e.Y);
			if (selected != null)
			{
				Form Details = new DetailsView(
					selected.SubItems[0].Text,
					selected.SubItems[1].Text,
					selected.SubItems[2].Text,
					selected.SubItems[3].Text,
					selected.SubItems[4].Text,
					selected.SubItems[5].Text,
					selected.SubItems[6].Text,
					selected.SubItems[7].Text
					);
				Details.Show();
			}
		}

	}

}
