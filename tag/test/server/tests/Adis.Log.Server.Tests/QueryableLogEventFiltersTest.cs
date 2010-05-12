using Adis.Log.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Adis.Log.Server.Tests
{
    
    
    /// <summary>
    ///This is a test class for QueryableLogEventFiltersTest and is intended
    ///to contain all QueryableLogEventFiltersTest Unit Tests
    ///</summary>
	[TestClass()]
	public class QueryableLogEventFiltersTest
	{
		private IList<LogEvent> LogEvents;
		private DateTime AFixedTimeStamp;

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

		
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		[TestInitialize()]
		public void MyTestInitialize()
		{
			AFixedTimeStamp = DateTime.Now;

			List<LogEvent> listOfLogEvents = new List<LogEvent>();
			listOfLogEvents.Add(new LogEvent() { Application = "123" });
			listOfLogEvents.Add(new LogEvent() { Category = "123" });
			listOfLogEvents.Add(new LogEvent() { Instance = "123" });
			listOfLogEvents.Add(new LogEvent() { Machine = "123" });
			listOfLogEvents.Add(new LogEvent() { User = "123" });
			listOfLogEvents.Add(new LogEvent() { Severity = "warn" });
			listOfLogEvents.Add(new LogEvent() { Severity = "debug" });
			listOfLogEvents.Add(new LogEvent() { Message = "123" });
			listOfLogEvents.Add(new LogEvent() { TimeLogged = AFixedTimeStamp });
			listOfLogEvents.Add(new LogEvent() { EventTime = AFixedTimeStamp });
			listOfLogEvents.Add(new LogEvent() { EventID=1 });

			LogEvents = listOfLogEvents;
		}

		/// <summary>
		///A test for WithUser
		///</summary>
		[TestMethod()]
		public void WithUser_matches_both_exact_and_partial()
		{
			LogEvents.Add(new LogEvent() { User = "123456" });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			string userToFilterOn = "123";
			IQueryable<LogEvent> actual;
			{
				bool matchExactly = false;
				actual = queryableOfLogEvents.WithUser(userToFilterOn, matchExactly);
				Assert.AreEqual(2, actual.Count());
			}
			{
				bool matchExactly = true;
				actual = queryableOfLogEvents.WithUser(userToFilterOn, matchExactly);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithTimeLoggedInRange
		///</summary>
		[TestMethod()]
		public void WithTimeLoggedInRangeTest()
		{
			LogEvents.Add(new LogEvent() { TimeLogged = AFixedTimeStamp.AddMinutes(30) });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();

			DateTime TenMinsAfterFixed = AFixedTimeStamp.AddMinutes(10);
			DateTime TenMinsBeforeFixed = AFixedTimeStamp.AddMinutes(-10);
			IQueryable<LogEvent> actual;

			{
				actual = queryableOfLogEvents.WithTimeLoggedInRange(TenMinsBeforeFixed, null);
				Assert.AreEqual(2, actual.Count(), "Start time with no end time");
			}

			{
				actual = queryableOfLogEvents.WithTimeLoggedInRange(TenMinsBeforeFixed, TenMinsAfterFixed);
				Assert.AreEqual(1, actual.Count(), "start time and end time");
			}

			{
				//TimeLogged is a compulsory field therefore all the LogEvent objects that haven't got TimeLogged explicitely set
				//wil have an TimeLogged of DateTime.MinValue. No start time filtering means that we will get all of those as well.
				//The only one we should filter out here is the one we just added with 30 minutes in the future.
				actual = queryableOfLogEvents.WithTimeLoggedInRange(null, TenMinsAfterFixed);
				Assert.AreEqual(queryableOfLogEvents.Count() - 1, actual.Count(), "end time and no start time");
			}

			{
				actual = queryableOfLogEvents.WithTimeLoggedInRange(TenMinsAfterFixed, null);
				Assert.AreEqual(1, actual.Count(), "start time 10 minutes after 1st record");
			}
		}

		/// <summary>
		///A test for WithMessage
		///</summary>
		[TestMethod()]
		public void WithMessage_matches_both_exact_and_partial()
		{
			LogEvents.Add(new LogEvent() { Message = "123456" });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			string MessageToFilterOn = "123";
			IQueryable<LogEvent> actual;
			{
				bool matchExactly = false;
				actual = queryableOfLogEvents.WithMessage(MessageToFilterOn, matchExactly);
				Assert.AreEqual(2, actual.Count());
			}
			{
				bool matchExactly = true;
				actual = queryableOfLogEvents.WithMessage(MessageToFilterOn, matchExactly);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithMaxSeverity
		///</summary>
		[TestMethod()]
		public void WithMaxSeverity_gets_lower_level_severities()
		{
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();

			IQueryable<LogEvent> actual;

			{
				//debug should get warn and debug
				string severity = "debug";
				actual = queryableOfLogEvents.WithMaxSeverity(severity);
				Assert.AreEqual(2, actual.Count());
			}
			{
				//warn shouldn't get debug
				string severity = "warn";
				actual = queryableOfLogEvents.WithMaxSeverity(severity);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithMachine
		///</summary>
		[TestMethod()]
		public void WithMachine_matches_both_exact_and_partial()
		{
			LogEvents.Add(new LogEvent() { Machine = "123456" });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			string MachineToFilterOn = "123";
			IQueryable<LogEvent> actual;
			{
				bool matchExactly = false;
				actual = queryableOfLogEvents.WithMachine(MachineToFilterOn, matchExactly);
				Assert.AreEqual(2, actual.Count());
			}
			{
				bool matchExactly = true;
				actual = queryableOfLogEvents.WithMachine(MachineToFilterOn, matchExactly);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithInstance
		///</summary>
		[TestMethod()]
		public void WithInstance_matches_both_exact_and_partial()
		{
			LogEvents.Add(new LogEvent() { Instance = "123456" });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			string InstanceToFilterOn = "123";
			IQueryable<LogEvent> actual;
			{
				bool matchExactly = false;
				actual = queryableOfLogEvents.WithInstance(InstanceToFilterOn, matchExactly);
				Assert.AreEqual(2, actual.Count());
			}
			{
				bool matchExactly = true;
				actual = queryableOfLogEvents.WithInstance(InstanceToFilterOn, matchExactly);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithEventTimeInRange
		///</summary>
		[TestMethod()]
		public void WithEventTimeInRangeTest()
		{
			LogEvents.Add(new LogEvent() { EventTime = AFixedTimeStamp.AddMinutes(30) });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();

			DateTime TenMinsAfterFixed = AFixedTimeStamp.AddMinutes(10);
			DateTime TenMinsBeforeFixed = AFixedTimeStamp.AddMinutes(-10);
			IQueryable<LogEvent> actual;

			{
				actual = queryableOfLogEvents.WithEventTimeInRange(TenMinsBeforeFixed, null);
				Assert.AreEqual(2, actual.Count(), "Start time with no end time");
			}

			{
				actual = queryableOfLogEvents.WithEventTimeInRange(TenMinsBeforeFixed, TenMinsAfterFixed);
				Assert.AreEqual(1, actual.Count(), "start time and end time");
			}

			{
				//EventTime is a compulsory field therefore all the LogEvent objects that haven't got EventTime explicitely set
				//wil have an EventTime of DateTime.MinValue. No start time filtering means that we will get all of those as well.
				//The only one we should filter out here is the one we just added with 30 minutes in the future.
				actual = queryableOfLogEvents.WithEventTimeInRange(null, TenMinsAfterFixed);
				Assert.AreEqual(queryableOfLogEvents.Count()-1, actual.Count(), "end time and no start time");
			}

			{
				actual = queryableOfLogEvents.WithEventTimeInRange(TenMinsAfterFixed, null);
				Assert.AreEqual(1, actual.Count(), "start time 10 minutes after 1st record");
			}

		}

		/// <summary>
		///A test for WithCategory
		///</summary>
		[TestMethod()]
		public void WithCategory_matches_both_exact_and_partial()
		{
			LogEvents.Add(new LogEvent() { Category = "123456" });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			string CategoryToFilterOn = "123";
			IQueryable<LogEvent> actual;
			{
				bool matchExactly = false;
				actual = queryableOfLogEvents.WithCategory(CategoryToFilterOn, matchExactly);
				Assert.AreEqual(2, actual.Count());
			}
			{
				bool matchExactly = true;
				actual = queryableOfLogEvents.WithCategory(CategoryToFilterOn, matchExactly);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithApplication
		///</summary>
		[TestMethod()]
		public void WithApplication_matches_both_exact_and_partial()
		{
			LogEvents.Add(new LogEvent() { Application = "123456" });
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			string ApplicationToFilterOn = "123";
			IQueryable<LogEvent> actual;
			{
				bool matchExactly = false;
				actual = queryableOfLogEvents.WithApplication(ApplicationToFilterOn, matchExactly);
				Assert.AreEqual(2, actual.Count());
			}
			{
				bool matchExactly = true;
				actual = queryableOfLogEvents.WithApplication(ApplicationToFilterOn, matchExactly);
				Assert.AreEqual(1, actual.Count());
			}
		}

		/// <summary>
		///A test for WithApplication
		///</summary>
		[TestMethod()]
		public void WithId_gets_only_LogEvents_that_have_same_id()
		{
			IQueryable<LogEvent> queryableOfLogEvents = LogEvents.AsQueryable();
			int IdToFilterOn = 1;
			IQueryable<LogEvent> actual;

			actual = queryableOfLogEvents.WithId(IdToFilterOn);
			Assert.AreEqual(1, actual.Count());
		}
	}
}
