using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Adis.Log.Listener.WinForms
{
	static class Program
	{
		public static MainForm mainForm;

		public static int UiThreadId { get; set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			UiThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			mainForm = new MainForm();
			Application.Run(mainForm);

		}
	}
}
