using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Adis.Log.Listener.WinForms
{
	static class Program
	{
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

			Application.Run(new MainForm());

		}
	}
}
