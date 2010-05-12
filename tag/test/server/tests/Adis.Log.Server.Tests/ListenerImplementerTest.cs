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
		public void InitialiseLink_Successfully_Adds_New_Listener_To_List()
		{
			ListenerImplementer target = new ListenerImplementer();
			RequestFilter requestFilter = new RequestFilter();
			Uri remoteAddressUri = new Uri("http://www.dummyUri.com");
			IContextChannel channelMock = new Moq.Mock<IContextChannel>(Moq.MockBehavior.Loose).Object;
			IListenerCallbackContract callbackMock = new Moq.Mock<IListenerCallbackContract>().Object;

			ListenerImplementer.ListenerInfo newListener = new ListenerImplementer.ListenerInfo()
			{
				callback = callbackMock,
				filter = requestFilter,
				remoteAddress = remoteAddressUri
			};

			Moq.Mock<IList<ListenerImplementer.ListenerInfo>> listenerListMock = new Moq.Mock<IList<ListenerImplementer.ListenerInfo>>();
			listenerListMock.Expect(list => list.Add(newListener)).AtMostOnce();

			bool expected = true;
			bool actual;
			actual = target.InitialiseLink(requestFilter, callbackMock, remoteAddressUri, channelMock, listenerListMock.Object);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for NotifyListeners
		///</summary>
		[TestMethod()]
		public void NotifyListeners_Calls_Notify_Once()
		{
			LogTransportObject logObject = new LogTransportObject();

			Moq.Mock<IListenerCallbackContract> callbackMock = new Moq.Mock<IListenerCallbackContract>();
			callbackMock.Expect(callback => callback.Notify(logObject)).AtMostOnce();

			//set up a dummy listener that we can notify
			List<ListenerImplementer.ListenerInfo> listenerList = new List<ListenerImplementer.ListenerInfo>();
			listenerList.Add(new ListenerImplementer.ListenerInfo()
			{
				remoteAddress = new Uri("http://www.dummyUri.com"),
				callback = callbackMock.Object,
				filter = new RequestFilter()
			});

			ListenerImplementer.NotifyListeners(logObject, listenerList);
		}

		[TestMethod]
		public void RemoveBadListeners_Removes_One_Listener_Only()
		{
			List<ListenerImplementer.ListenerInfo> listToRemove = new List<ListenerImplementer.ListenerInfo>();
			List<ListenerImplementer.ListenerInfo> masterList = new List<ListenerImplementer.ListenerInfo>();

			IListenerCallbackContract callback = new Moq.Mock<IListenerCallbackContract>().Object;

			//populate the master list with 2 listeners
			masterList.Add(new ListenerImplementer.ListenerInfo()
			{
				remoteAddress = new Uri("http://www.dummyUriOne.com"),
				callback = callback,
				filter = new RequestFilter()
			});
			masterList.Add(new ListenerImplementer.ListenerInfo()
			{
				remoteAddress = new Uri("http://www.dummyUriTwo.com"),
				callback = callback,
				filter = new RequestFilter()
			});

			//add one of the master's listeners in the remove list
			listToRemove.Add(masterList[0]);

			ListenerImplementer_Accessor.RemoveBadListeners(masterList, listToRemove);

			Assert.AreEqual(1, masterList.Count);


		}
	}
}
