using Adis.Log.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Adis.Log.Contract;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Adis.Log.server.Tests
{
    
    
    /// <summary>
    ///This is a test class for ReporterImplementerTest and is intended
    ///to contain all ReporterImplementerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ReporterImplementerTest
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


	  [TestMethod]
		public void GetRecords_filters_on_all_fields()
		{
			int result = -1;
			DateTime aFixedTimeStamp = DateTime.Now;
			Moq.Mock<IRepository> repositoryMock = new Moq.Mock<IRepository>();
			Uri remoteAddress = new Uri("http://www.DummyUri.com");
			List<LogEvent> listOfLogEvents = new List<LogEvent>();
			listOfLogEvents.Add(new LogEvent() { Application = "123", EventID=1 });
			listOfLogEvents.Add(new LogEvent() { Category = "123" });
			listOfLogEvents.Add(new LogEvent() { Instance = "123" });
			listOfLogEvents.Add(new LogEvent() { Machine = "123" });
			listOfLogEvents.Add(new LogEvent() { User = "123" });
			listOfLogEvents.Add(new LogEvent() { Severity = "warn" });
			listOfLogEvents.Add(new LogEvent() { Message = "123" });
			listOfLogEvents.Add(new LogEvent() { TimeLogged = aFixedTimeStamp });
			listOfLogEvents.Add(new LogEvent() { EventTime = aFixedTimeStamp });

			repositoryMock.Expect(rep => rep.GetAllLogLogEvents()).Returns(listOfLogEvents.AsQueryable());

			RequestFilter filter = null;
			{
				filter = new RequestFilter() { Application = "123" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on application field failed");
			}
			{
				filter = new RequestFilter() { Category = "123" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on Category field failed");
			}
			{
				filter = new RequestFilter() { Instance = "123" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on Instance field failed");
			}
			{
				filter = new RequestFilter() { Machine = "123" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on Machine field failed");
			}
			{
				filter = new RequestFilter() { User = "123" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on User field failed");
			}
			{
				filter = new RequestFilter() { Message = "123" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on Message field failed");
			}
			{
				filter = new RequestFilter() { Severity = "warn" };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on Severity field failed");
			}
			{
				filter = new RequestFilter() { StartTime = aFixedTimeStamp.AddMinutes(-10) };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, false).Count;
				Assert.AreEqual(1, result, "filter on EventTime field failed");
			}
			{
				filter = new RequestFilter() { StartTime = aFixedTimeStamp.AddMinutes(-10) };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, true).Count;
				Assert.AreEqual(1, result, "filter on TimeLogged field failed");
			}
			{
				filter = new RequestFilter() { Id = 1 };
				result = ReporterImplementer.GetRecords(filter, 0, 10, repositoryMock.Object, remoteAddress, true).Count;
				Assert.AreEqual(1, result, "filter on Id failed");
			}

		}

	}
}
