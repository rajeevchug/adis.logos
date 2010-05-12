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
    ///This is a test class for LoggerImplementerTest and is intended
    ///to contain all LoggerImplementerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class LoggerImplementerTest
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
		///A test for InsertNewLog
		///</summary>
		[TestMethod()]
		public void InsertNewLogTest()
		{
			LoggerImplementer target = new LoggerImplementer();
			LogTransportObject logObject = new LogTransportObject() { Application = "abc", Severity = "debug" };
			IRepository repository = new RepositoryMock();
			bool expected = true;
			bool actual;
			actual = target.InsertNewLog(logObject, repository);
			Assert.AreEqual(expected, actual, "InsertNewLog() failed");
			Assert.AreEqual(6, repository.GetAllLogLogEvents().Count(), "new log hasn't been inserted into repository");
		}

		[TestMethod]
		public void InsertNewLogTestUseMoq()
		{
			DateTime loggedTime = DateTime.Now;
			LoggerImplementer target = new LoggerImplementer();
			LogTransportObject logObject = new LogTransportObject();

			LogEvent logevent = new LogEvent();

			Moq.Mock<IRepository> repositoryMock = new Moq.Mock<IRepository>();
			repositoryMock.Expect(rep => rep.InsertIntoDatabase(logevent));

			bool expected = true;
			bool actual;
			actual = target.InsertNewLog(logObject, repositoryMock.Object);
			Assert.AreEqual(expected, actual, "InsertNewLog() failed");
		}
	}
}
