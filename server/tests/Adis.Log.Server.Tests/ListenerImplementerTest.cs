using Adis.Log.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Adis.Log.Contract;
using System;
using System.ServiceModel;
using System.Collections.Generic;

namespace Adis.Log.server.Tests
{
    
    
    /// <summary>
    ///This is a test class for ListenerImplementerTest and is intended
    ///to contain all ListenerImplementerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ListenerImplementerTest
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
		///A test for InitialiseLink
		///</summary>
		[TestMethod()]
		public void InitialiseLinkTest()
		{
			ListenerImplementer target = new ListenerImplementer(); 
			RequestFilter requestFilter = new RequestFilter();
			Uri remoteAddressUri = new Uri("http://www.dummyUri.com");
			IContextChannel channel = new ContextChannelMock();
			IListenerCallbackContract callbackContract = new ListenerCallbackContractMock();
			List<ListenerImplementer.ListenerInfo> listenerList = new List<ListenerImplementer.ListenerInfo>();
			bool expected = true;
			bool actual;
			actual = target.InitialiseLink(requestFilter, callbackContract, remoteAddressUri, channel, listenerList);
			Assert.AreEqual(expected, actual);
			Assert.AreEqual(1, listenerList.Count, "listenerList contains the wrong number of objects");
		}

		/// <summary>
		///A test for NotifyListeners
		///</summary>
		[TestMethod()]
		public void NotifyListenersTest()
		{
			LogTransportObject logObject = new LogTransportObject(); 

			//set up a dummy listener that we can notify
			List<ListenerImplementer.ListenerInfo> listenerList = new List<ListenerImplementer.ListenerInfo>();
			listenerList.Add(new ListenerImplementer.ListenerInfo()
			{
				remoteAddress = new Uri("http://www.dummyUri.com"),
				callback = new ListenerCallbackContractMock(),
				filter = new RequestFilter()
			});

			ListenerImplementer.NotifyListeners(logObject, listenerList);
			Assert.IsTrue(((ListenerCallbackContractMock)listenerList[0].callback).NotificationComplete, 
				"the listener's Notify() method wasn't called");
		}

	}
}
