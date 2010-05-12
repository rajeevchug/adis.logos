using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Adis.Log.Contract
{
	[DataContract(Name="Log")]
	public class LogTransportObject
	{
		//The unique DB identifier of the logevent. Never populate it when logging an event.
		//It will only be populated for reporting or listening services
		[DataMember(Name="EventId", Order=1)]
		public int Id { get; set; }

		//this is the application 'root' name eg "editorial tools" or "feeds"
		[DataMember(Name="Category", Order=2)]
		public String Category { get; set; }

		//The name of the application. eg "RPS.Mylan"
		[DataMember(Name="Application", Order=3)]
		public String Application { get; set; }

		//This can be used to identify the thread or session name
		[DataMember(Name="Instance", Order=4)]
		public String Instance { get; set; }

		//The user associated with the event. For a service it may be the login contect that 
		//the service is running under. For an asp.net app it could be the name of the user associated 
		//with the current session
		[DataMember(Name="User", Order=5)]
		public String User { get; set; }

		//The computer that the application is running on
		[DataMember(Name="Machine", Order=6)]
		public String Machine { get; set; }

		//Debug, Warning, Error, Fatal, Info
		[DataMember(Name="Severity", Order=7)]
		public String Severity { get; set; }

		//The time the event happened
		[DataMember(Name="Time", Order=9)]
		public DateTime Time { get; set; }

		//A short message describing the event being logged
		[DataMember(Name="Message", Order=9)]
		public String Message { get; set; }

		//This really needs to be a string because it's not likely that the server side will have 
		//enough knowledge of the object passed back to serialise it.
		[DataMember(Name="ExtraInfo", Order=10)]
		public String ExtraInfo { get; set; }

	}
}
