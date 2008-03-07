using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsListener
{
	static class Program
	{
		public static Form1 mainForm;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			mainForm = new Form1();
			ListenerManager.StartService();
			Application.Run(mainForm);

			mainForm.KillBgThread();

		}
	}
}
