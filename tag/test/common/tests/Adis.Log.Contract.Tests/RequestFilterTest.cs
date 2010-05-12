using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adis.Log.Contract;

namespace Adis.Log.Contract.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class RequestFilterTest
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
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void ValidSeveritiesFailsForNullOrBlank()
		{
			Assert.AreEqual(0, RequestFilter.GetValidSeverities(null).Count());
			Assert.AreEqual(0, RequestFilter.GetValidSeverities("").Count());
		}

		[TestMethod]
		public void ValidSeveritiesForFatal()
		{
			String expectedSeveritiesReturned = "fatal";
			String actualSeveritiesReturned = ConcatReturnedSeverities("fatal");

			Assert.AreEqual(expectedSeveritiesReturned, actualSeveritiesReturned);
		}

		[TestMethod]
		public void ValidSeveritiesForDebug()
		{
			String expectedSeveritiesReturned = "debugerrorfatalinfowarn";
			String actualSeveritiesReturned = ConcatReturnedSeverities("debug");
			
			Assert.AreEqual(expectedSeveritiesReturned, actualSeveritiesReturned);
		}

		/// <summary>
		/// helper function for GetValidSeverities tests
		/// </summary>
		/// <param name="severity"></param>
		/// <returns></returns>
		private static String  ConcatReturnedSeverities(String severity)
		{
			string actualSeveritiesReturned = "";
			IEnumerable<String> valuesFromGetValidSeverities = RequestFilter.GetValidSeverities(severity);

			foreach (String validSeverity in valuesFromGetValidSeverities.OrderBy(s => s))
			{
				actualSeveritiesReturned += validSeverity;
			}

			return actualSeveritiesReturned;
		}

		[TestMethod]
		public void ValidSeveritiesIsCaseInsensitive()
		{
			Assert.AreEqual(1, RequestFilter.GetValidSeverities("Fatal").Count());
			Assert.AreEqual(1, RequestFilter.GetValidSeverities("FATAL").Count());
		}

		/// <summary>
		///A test for SatisfiesFilter
		///</summary>
		[TestMethod()]
		public void SatisfiesFilterTest()
		{
			//check that RequestFilter.Application is taken into account
			{
				bool expected = true;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Application = "1234" };
				LogTransportObject transportObject = new LogTransportObject { Application = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.Application");
			}
			//check that RequestFilter.ApplicationExactMatch is taken into account
			{
				bool expected = false;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Application = "1234", ApplicationExactMatch = true };
				LogTransportObject transportObject = new LogTransportObject { Application = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.ApplicationExactMatch");
			}
			//check that RequestFilter.Category is taken into account
			{
				bool expected = true;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Category = "1234" };
				LogTransportObject transportObject = new LogTransportObject { Category = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.Category");
			}
			//check that RequestFilter.CategoryExactMatch is taken into account
			{
				bool expected = false;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Category = "1234", CategoryExactMatch = true };
				LogTransportObject transportObject = new LogTransportObject { Category = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.CategoryExactMatch");
			}
			//check that RequestFilter.Instance is taken into account
			{
				bool expected = true;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Instance = "1234" };
				LogTransportObject transportObject = new LogTransportObject { Instance = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.Instance");
			}
			//check that RequestFilter.InstanceExactMatch is taken into account
			{
				bool expected = false;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Instance = "1234", InstanceExactMatch = true };
				LogTransportObject transportObject = new LogTransportObject { Instance = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.InstanceExactMatch");
			}
			//check that RequestFilter.Machine is taken into account
			{
				bool expected = true;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Machine = "1234" };
				LogTransportObject transportObject = new LogTransportObject { Machine = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.Machine");
			}
			//check that RequestFilter.MachineExactMatch is taken into account
			{
				bool expected = false;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Machine = "1234", MachineExactMatch = true };
				LogTransportObject transportObject = new LogTransportObject { Machine = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.MachineExactMatch");
			}
			//check that RequestFilter.User is taken into account
			{
				bool expected = true;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { User = "1234" };
				LogTransportObject transportObject = new LogTransportObject { User = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.User");
			}
			//check that RequestFilter.UserExactMatch is taken into account
			{
				bool expected = false;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { User = "1234", UserExactMatch = true };
				LogTransportObject transportObject = new LogTransportObject { User = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.UserExactMatch");
			}
			//check that RequestFilter.Message is taken into account
			{
				bool expected = true;
				bool actual;
				RequestFilter requestFilter = new RequestFilter() { Message = "1234" };
				LogTransportObject transportObject = new LogTransportObject { Message = "12345678" };
				actual = RequestFilter.SatisfiesFilter(requestFilter, transportObject);
				Assert.AreEqual(expected, actual, "RequestFilter.Message");
			}
		}

		/// <summary>
		///A test for DoesStringMatch
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Contract.dll")]
		public void DoesStringMatchTest()
		{
			//null filter should always return true
			{
				string logString = "1234"; 
				string filter = null;
				bool expected = true; 
				bool actual;
				actual = RequestFilter_Accessor.DoesStringMatch(logString, filter, false);
				Assert.AreEqual(expected, actual, "null filter should always return true");
			}
			// filter that is superset of logString fails
			{
				string logString = "1234";
				string filter = "12345678";
				bool exactMatch = false;
				bool expected = false;
				bool actual;
				actual = RequestFilter_Accessor.DoesStringMatch(logString, filter, exactMatch);
				Assert.AreEqual(expected, actual, "filter that is superset of logString fails");
			}
			// filter that is subset of logString succeeds when exactMatch is false
			{
				string logString = "12345678";
				string filter = "1234";
				bool exactMatch = false;
				bool expected = true;
				bool actual;
				actual = RequestFilter_Accessor.DoesStringMatch(logString, filter, exactMatch);
				Assert.AreEqual(expected, actual, "filter that is subset of logString succeeds when exactMatch is false");
			}
			// filter that is subset of logString fails when exactMatch is true
			{
				string logString = "12345678";
				string filter = "1234";
				bool exactMatch = true;
				bool expected = false;
				bool actual;
				actual = RequestFilter_Accessor.DoesStringMatch(logString, filter, exactMatch);
				Assert.AreEqual(expected, actual, "filter that is subset of logString fails when exactMatch is true");
			}

		}

		/// <summary>
		///A test for DoesSeveritySatisfyFilter
		///</summary>
		[TestMethod()]
		[DeploymentItem("Adis.Log.Contract.dll")]
		public void DoesSeveritySatisfyFilterTest()
		{
			//debug should always match debug
			{
				string logSeverity = "debug";
				string filter = "debug";
				bool expected = true;
				bool actual;
				actual = RequestFilter_Accessor.DoesSeveritySatisfyFilter(logSeverity, filter);
				Assert.AreEqual(expected, actual, "debug should always match debug");
			}
			//info shouldn't match debug
			{
				string logSeverity = "info";
				string filter = "debug";
				bool expected = false;
				bool actual;
				actual = RequestFilter_Accessor.DoesSeveritySatisfyFilter(logSeverity, filter);
				Assert.AreEqual(expected, actual, "info shouldn't match debug");
			}
			//"wibble" shouldn't match any severity
			{
				string logSeverity = "wibble";
				string filter = "fatal";
				bool expected = false;
				bool actual;
				actual = RequestFilter_Accessor.DoesSeveritySatisfyFilter(logSeverity, filter);
				Assert.AreEqual(expected, actual, "\"wibble\" shouldn't match any severity");
			}
		}
	}
}
