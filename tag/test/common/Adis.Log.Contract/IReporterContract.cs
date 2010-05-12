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
		IEnumerable<LogTransportObject> GetRecords(RequestFilter filter,  int skipFirst, int maxRecords);

    [OperationContract]
    int GetCount(RequestFilter filter);

		[OperationContract]
		IEnumerable<string> GetCategoryList();

		[OperationContract]
		Dictionary<string, IEnumerable<string>> GetApplicationList();
		
  }

}
