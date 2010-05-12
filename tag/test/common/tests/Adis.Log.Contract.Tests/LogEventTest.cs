using Adis.Log.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Adis.Log.Contract;
using System;

namespace Adis.Log.Contract.Tests
{
    
    
    /// <summary>
    ///This is a test class for LogEventTest and is intended
    ///to contain all LogEventTest Unit Tests
    ///</summary>
	[TestClass()]
	public class LogEventTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for ToLogTransportObject
		///</summary>
		[TestMethod()]
		public void ToLogTransportObjectTest()
		{
			LogEvent target = new LogEvent()
			{
				Application = "Application",
				Category = "category",
				EventID = 1,
				EventTime = DateTime.Now,
				ExtraInfo = "extrainfo",
				Instance = "instance",
				Machine = "machine",
				Message = "message",
				Severity = "severity",
				TimeLogged = DateTime.Now,
				User = "user"
			};
			LogTransportObject expected = null; 
			LogTransportObject actual;
			actual = target.ToLogTransportObject();
			Assert.AreEqual(target.Application, actual.Application);
			Assert.AreEqual(target.Category, actual.Category);
			Assert.AreEqual(target.EventID, actual.Id);
			Assert.AreEqual(target.EventTime, actual.Time);
			Assert.AreEqual(target.ExtraInfo, actual.ExtraInfo);
			Assert.AreEqual(target.Instance, actual.Instance);
			Assert.AreEqual(target.Machine, actual.Machine);
			Assert.AreEqual(target.Message, actual.Message);
			Assert.AreEqual(target.Severity, actual.Severity);
			//TimeLogged isn't used in LogTransportObject
			Assert.AreEqual(target.User, actual.User);
		}

		/// <summary>
		///A test for op_Explicit
		///</summary>
		[TestMethod()]
		public void op_ExplicitTest()
		{
			LogTransportObject target = new LogTransportObject
			{
				Application = "Application",
				Category = "category",
				Id = 1,
				Time = DateTime.Now,
				ExtraInfo = "extrainfo",
				Instance = "instance",
				Machine = "machine",
				Message = "message",
				Severity = "debug",
				User = "user"
			};
			LogEvent actual;
			actual = ((LogEvent)(target));
			Assert.AreEqual(target.Application, actual.Application);
			Assert.AreEqual(target.Category, actual.Category);
			//the Id is specifically not converted
			//Assert.AreEqual(target.Id, actual.EventID);
			
			Assert.AreEqual(target.Time, actual.EventTime);
			Assert.AreEqual(target.ExtraInfo, actual.ExtraInfo);
			Assert.AreEqual(target.Instance, actual.Instance);
			Assert.AreEqual(target.Machine, actual.Machine);
			Assert.AreEqual(target.Message, actual.Message);
			Assert.AreEqual(target.Severity, actual.Severity);
		}
	}
}
