using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Adis.Log.Contract;

namespace Adis.Log.Listener.WinForms
{
	public partial class MainForm : Form
	{

		public LogObjectList ListOfLogObjects;

		public MainForm()
		{
			InitializeComponent();
			ListOfLogObjects = new LogObjectList();

			listView1.VirtualMode = true;
			listView1.VirtualListSize = ListOfLogObjects.Count;
			listView1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);

			filterSeverity.Items.AddRange(RequestFilter.SeverityRanks.Keys.ToArray());

		}

		private void ConnectToListener()
		{
			ClearListView();

			progressBar1.Style = ProgressBarStyle.Marquee;
			statusLabel.ForeColor = Color.Blue;
			statusLabel.Text = "Attempting to connect to the listener service.";
			ListenerManager listenerManager = new ListenerManager();

			listenerManager.SetRequestFilter(
				filterCategory.Enabled ? filterCategory.Text : null, filterCategoryExactMatch.Checked,
				filterApplication.Enabled ? filterApplication.Text : null, filterApplicationExactMatch.Checked,
				filterInstance.Enabled ? filterInstance.Text : null, filterInstanceExactMatch.Checked,
				filterMachine.Enabled ? filterMachine.Text : null, filterMachineExactMatch.Checked,
				filterUser.Enabled ? filterUser.Text : null, filterUserExactMatch.Checked,
				filterSeverity.Enabled ? filterSeverity.Text : null, 
				filterStartTime.Enabled ? (DateTime?)filterStartTime.Value : null,
				filterMessage.Enabled ? filterMessage.Text : null, filterMessageExactMatch.Checked);

			listenerManager.StartServiceCompleted += new EventHandler<StartServiceEventArgs>(listenerManager_StartServiceCompleted);
			listenerManager.StartServiceAsync();
		}

		protected override void OnClosed(EventArgs e)
		{
			KeepConnectionAlive.StopKeepAliveThread();
			base.OnClosed(e);
		}

		void listenerManager_StartServiceCompleted(object sender, StartServiceEventArgs e)
		{
			progressBar1.Style = ProgressBarStyle.Continuous;
			progressBar1.Value = progressBar1.Maximum;
			if (e.Succeeded)
			{
				statusLabel.ForeColor = Color.Black;
				statusLabel.Text = "Listener connected successfully";
			}
			else
			{
				statusLabel.ForeColor = Color.Red;
				statusLabel.Text = "Listener failed to connect sucessfully.";
				ErrorDetails.Text = ((Object)e.Exception ?? "").ToString();
			}
		}

		private void btnReconnect_Click(object sender, EventArgs e)
		{
			ErrorDetails.Text = "";
			ConnectToListener();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
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
			if (checkBoxAutoScroll.Checked && ListOfLogObjects.Count < 0)
			{
				listView1.EnsureVisible(ListOfLogObjects.Count - 1);
			}
		}

		public String StatusText
		{
			get { return statusLabel.Text; }
			set { statusLabel.Text = value; }

		}
		delegate void UpdateListView();

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

		private void CategoryLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterCategory, filterCategoryExactMatch);
		}

		private void ApplicationLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterApplication, filterApplicationExactMatch);
		}

		private void InstanceLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterInstance, filterInstanceExactMatch);
		}

		private void MachineLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterMachine, filterMachineExactMatch);
		}

		private void UserLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterUser, filterUserExactMatch);
		}

		private void SeverityLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled( filterSeverity, null);
		}

		private void StartTimeLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterStartTime, null);
		}

		private void MessageLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ToggleFilterGroupEnabled(filterMessage, filterMessageExactMatch);
		}

		private void ToggleFilterGroupEnabled(Control textbox, Control checkbox)
		{
			textbox.Enabled = !textbox.Enabled;
			//in case they might get out of sync, make the textbox the master
			if (checkbox != null)
			{
				checkbox.Enabled = textbox.Enabled;
			}
		}

		private void btnClearList_Click(object sender, EventArgs e)
		{
			ClearListView();
		}

		private void ClearListView()
		{
			ListOfLogObjects.Clear();
			ResetVirtualListSize();
		}
	}

}
