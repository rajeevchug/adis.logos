using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Client;

namespace SampleApp
{

	class Program
	{
		static void Main(string[] args)
		{
			//sets up the logging
			LogManager.Configure();
			System.Threading.Thread.CurrentThread.Name = "main";

			LogPart1();

			LogPart2();

			Console.Write("\n\n\nPress a key to exit...");
			Console.ReadKey();
		}

		static void LogPart1()
		{
			//gets the default logos logger
			ILog log = LogManager.GetLog();


			//add properties that will be added into Adis.Log logger
			LogManager.Application = "Adis.Log.Client.SampleApp";
			LogManager.Category = "Adis.Log Test Applications";
			LogManager.User = "adisnz\\jg3";
			//The LogMananger.Instance field has not been set so it defaults to the current 
			//thread name

			//Log some information
			log.Debug("1 debug");
			Console.Write("1");
			log.Info("2 info");
			Console.Write("1");
		}

		static void LogPart2()
		{
			LogManager.Instance = "new instance name";

			//will use the "sample1" logger defined in the app.config
			ILog log = LogManager.GetLog("sample1");

			log.Debug("2.5 debug. This shouldn't get logged");

			//Log a message with an "extra Info" object
			log.Error("3 info", typeof(UserHelper));
			Console.Write("1");

			try
			{
				throw new Exception("an exception that we have thrown");
			}
			catch (Exception e)
			{
				log.Fatal("4 fatal", e);
			}

			log.WarnFormat("5 {0} {1}", "warnformat", typeof(UserHelper));
			Console.Write("1");
		}
	}
}
