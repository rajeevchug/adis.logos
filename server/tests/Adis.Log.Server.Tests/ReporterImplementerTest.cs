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


		/// <summary>
		///A test for GetRecords
		///</summary>
		[TestMethod()]
		public void GetRecordsTest()
		{
			IRepository repository = new RepositoryMock();
			Uri remoteAddress = new Uri("http://www.DummyUri.com"); ;
			//Try to get the 1st 10 records should give us 5 since that's all there are
			{
				RequestFilter filter = new RequestFilter();
				List<LogTransportObject> actual;
				actual = ReporterImplementer.GetRecords(filter, 0, 10, repository, remoteAddress, false);
				Assert.AreEqual(5, actual.Count, "Getting 10 records should give us the complete set (5 records)");
			}
			//Try to get 3 records, skipping the first 3 should give us 2
			{
				RequestFilter filter = new RequestFilter();
				List<LogTransportObject> actual;
				actual = ReporterImplementer.GetRecords(filter, 3, 3, repository, remoteAddress, false);
				Assert.AreEqual(2, actual.Count, "Try to get 3 records, skipping the first 3 should give us 2");
			}
			//filter on application starts with "123" should give us 3 records
			{
				RequestFilter filter = new RequestFilter() { Application = "123", ApplicationExactMatch=false };
				List<LogTransportObject> actual;
				actual = ReporterImplementer.GetRecords(filter, 0, 10, repository, remoteAddress, false);
				Assert.AreEqual(3, actual.Count, "filter on application starts with \"123\" should give us 3 records");
			}
			//filter on application starts with "123" with an exact match should give us 3 records
			{
				RequestFilter filter = new RequestFilter() { Application = "123", ApplicationExactMatch=true };
				List<LogTransportObject> actual;
				actual = ReporterImplementer.GetRecords(filter, 0, 10, repository, remoteAddress, false);

				Assert.AreEqual(1, actual.Count, "filter on application starts with \"123\" with exact match should give us 3 records");
			}

			//Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for AddUserFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddUserFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			RequestFilter filter = new RequestFilter() { User = "1234" };
			
			{
				//first test: "1234" and exactMatch=false should return 2 records ("1234" + "12345")
				filter.UserExactMatch = false;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddUserFilter(filter, logEvents);

				Assert.AreEqual(2, actual.Count(), "User=1234 should return 2 records");
			}
			{
				//second test: "1234" and exactMatch=true should return 1 records ("1234")
				filter.UserExactMatch = true;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddUserFilter(filter, logEvents);

				Assert.AreEqual(1, actual.Count(), "User=1234 EXACT should return 1 records");
			}
		}

		/// <summary>
		///A test for AddEventTimeFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddEventTimeFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			//filter.StartTime < [first record's EventTime] so should be getting all records
			{
				RequestFilter filter = new RequestFilter() { StartTime = RepositoryMock._PointInTime };
				IQueryable<LogEvent> actual = ReporterImplementer_Accessor.AddEventTimeFilter(filter, logEvents);
				Assert.AreEqual(5, actual.Count(), "filter.StartTime < [first record's EventTime] so should be getting all records");
			}
			//filter.StartTime == [3rd record's EventTime] so should be getting 3 records
			{
				RequestFilter filter = new RequestFilter() { StartTime = RepositoryMock._PointInTime.AddMinutes(3) };
				IQueryable<LogEvent> actual = ReporterImplementer_Accessor.AddEventTimeFilter(filter, logEvents);
				Assert.AreEqual(3, actual.Count(), "filter.StartTime == [3rd record's EventTime] so should be getting 3 records");
			}
			//filter.StartTime < [first record's EventTime], filter.EndTime < [4th records EventTime] so should return 1st to 3rd records
			{
				RequestFilter filter = new RequestFilter()
				{
					StartTime = RepositoryMock._PointInTime,
					EndTime = RepositoryMock._PointInTime.AddSeconds(210) //3.5 minutes 
				};
				IQueryable<LogEvent> actual = ReporterImplementer_Accessor.AddEventTimeFilter(filter, logEvents);
				Assert.AreEqual(3, actual.Count(), "filter.StartTime < [first record's EventTime], filter.EndTime < [4th records EventTime] so should return 1st to 3rd records");
			}
		}

		/// <summary>
		///A test for AddEventTimeFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddTimeLoggedFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			//filter.StartTime < [first record's TimeLogged] so should be getting all records
			{
				RequestFilter filter = new RequestFilter() { StartTime = RepositoryMock._PointInTime };
				IQueryable<LogEvent> actual = ReporterImplementer_Accessor.AddTimeLoggedFilter(filter, logEvents);
				Assert.AreEqual(5, actual.Count(), "filter.StartTime < [first record's TimeLogged] so should be getting all records");
			}
			//filter.StartTime == [3rd record's TimeLogged] so should be getting 3 records
			{
				RequestFilter filter = new RequestFilter() { StartTime = RepositoryMock._PointInTime.AddMinutes(3) };
				IQueryable<LogEvent> actual = ReporterImplementer_Accessor.AddTimeLoggedFilter(filter, logEvents);
				Assert.AreEqual(3, actual.Count(), "filter.StartTime == [3rd record's TimeLogged] so should be getting 3 records");
			}
			//filter.StartTime < [first record's TimeLogged], filter.EndTime < [4th records TimeLogged] so should return 1st to 3rd records
			{
				RequestFilter filter = new RequestFilter()
				{
					StartTime = RepositoryMock._PointInTime,
					EndTime = RepositoryMock._PointInTime.AddSeconds(210) //3.5 minutes 
				};
				IQueryable<LogEvent> actual = ReporterImplementer_Accessor.AddTimeLoggedFilter(filter, logEvents);
				Assert.AreEqual(3, actual.Count(), "filter.StartTime < [first record's TimeLogged], filter.EndTime < [4th records TimeLogged] so should return 1st to 3rd records");
			}
		}

		/// <summary>
		///A test for AddMachineFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddMachineFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			RequestFilter filter = new RequestFilter() { Machine = "1234" };

			{
				//first test: "1234" and exactMatch=false should return 2 records ("1234" + "12345")
				filter.MachineExactMatch = false;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddMachineFilter(filter, logEvents);

				Assert.AreEqual(2, actual.Count(), "Machine=1234 should return 2 records");
			}
			{
				//second test: "1234" and exactMatch=true should return 1 records ("1234")
				filter.MachineExactMatch = true;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddMachineFilter(filter, logEvents);

				Assert.AreEqual(1, actual.Count(), "Machine=1234 EXACT should return 1 records");
			}
		}

		/// <summary>
		///A test for AddInstanceFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddInstanceFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			RequestFilter filter = new RequestFilter() { Instance = "1234" };

			{
				//first test: "1234" and exactMatch=false should return 2 records ("1234" + "12345")
				filter.InstanceExactMatch = false;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddInstanceFilter(filter, logEvents);

				Assert.AreEqual(2, actual.Count(), "Instance=1234 should return 2 records");
			}
			{
				//second test: "1234" and exactMatch=true should return 1 records ("1234")
				filter.InstanceExactMatch = true;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddInstanceFilter(filter, logEvents);

				Assert.AreEqual(1, actual.Count(), "Instance=1234 EXACT should return 1 records");
			}
		}

		/// <summary>
		///A test for AddCategoryFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddCategoryFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			RequestFilter filter = new RequestFilter() { Category = "1234" };

			{
				//first test: "1234" and exactMatch=false should return 2 records ("1234" + "12345")
				filter.CategoryExactMatch = false;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddCategoryFilter(filter, logEvents);

				Assert.AreEqual(2, actual.Count(), "Category=1234 should return 2 records");
			}
			{
				//second test: "1234" and exactMatch=true should return 1 records ("1234")
				filter.CategoryExactMatch = true;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddCategoryFilter(filter, logEvents);

				Assert.AreEqual(1, actual.Count(), "Category=1234 EXACT should return 1 records");
			}
		}

		/// <summary>
		///A test for AddApplicationFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Server.dll")]
		public void AddApplicationFilterTest()
		{
			IRepository repository = new RepositoryMock();
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			RequestFilter filter = new RequestFilter() { Application = "1234" };

			{
				//first test: "1234" and exactMatch=false should return 2 records ("1234" + "12345")
				filter.ApplicationExactMatch = false;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddApplicationFilter(filter, logEvents);

				Assert.AreEqual(2, actual.Count(), "Application=1234 should return 2 records");
			}
			{
				//second test: "1234" and exactMatch=true should return 1 records ("1234")
				filter.ApplicationExactMatch = true;
				IQueryable<LogEvent> actual;
				actual = ReporterImplementer_Accessor.AddApplicationFilter(filter, logEvents);

				Assert.AreEqual(1, actual.Count(), "Application=1234 EXACT should return 1 records");
			}
		}
	}
}
