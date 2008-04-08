using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Adis.Log.Listener.WinForms
{
	public partial class MyListView : ListView
	{
		public MyListView()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

	}
}
