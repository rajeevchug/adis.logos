using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Adis.Log.Contract;
using System.Runtime.Serialization;

namespace Adis.Log.Contract
{
	[ServiceContract]
	public interface IReporterContract
	{
		[OperationContract]
		List<LogTransportObject> GetRecords(RequestFilter filter,  int skipFirst, int maxRecords);

	}

}
