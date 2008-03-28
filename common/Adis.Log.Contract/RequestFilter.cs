using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Adis.Log.Contract
{
	[DataContract]
	public class RequestFilter
	{
		public RequestFilter()
		{
			CategoryExactMatch = false;
			ApplicationExactMatch = false;
			InstanceExactMatch = false;
			UserExactMatch = false;
			MachineExactMatch = false;
			StartTime = null;
			EndTime = null;
		}

		[DataMember]
		public int? Id { get; set; }

		[DataMember]
		public String Category { get; set; }

		[DataMember]
		public String Application { get; set; }

		[DataMember]
		public String Instance { get; set; }

		[DataMember]
		public String User { get; set; }

		[DataMember]
		public String Machine { get; set; }

		[DataMember]
		public String Severity { get; set; }

		[DataMember]
		public String Message { get; set; }

		[DataMember]
		public DateTime? StartTime { get; set; }

		[DataMember]
		public DateTime? EndTime { get; set; }

		[DataMember]
		public bool CategoryExactMatch { get; set; }

		[DataMember]
		public bool ApplicationExactMatch { get; set; }

		[DataMember]
		public bool InstanceExactMatch { get; set; }

		[DataMember]
		public bool UserExactMatch { get; set; }

		[DataMember]
		public bool MachineExactMatch { get; set; }
	}
}