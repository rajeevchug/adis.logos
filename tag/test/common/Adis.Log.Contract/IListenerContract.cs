using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Adis.Log.Contract
{
	[ServiceContract(CallbackContract = typeof(IListenerCallbackContract))]
	public interface IListenerContract : IReporterContract
	{
		[OperationContract]
		bool InitialiseLink(RequestFilter filter);

		[OperationContract(IsOneWay=true)]
		void KeepAlive();
	}
}
