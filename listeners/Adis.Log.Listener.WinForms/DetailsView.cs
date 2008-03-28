using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsListener
{
	public partial class DetailsView : Form
	{
		public DetailsView()
		{
			InitializeComponent();
		}

		public DetailsView(
			String Application,
			String Category,
			String Time,
			String User,
			String Machine,
			String Severity,
			String Message, 
			String ExtraInfo
			)
			: this()
		{
			lblApplication.Text = Application;
			lblCategory.Text = Category;
			lblTime.Text = Time;
			lblUser.Text = User;
			lblMachine.Text = Machine;
			lblSeverity.Text = Severity;

			tbMessage.Text = Message;
			tbExtraInfo.Text = ExtraInfo;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
