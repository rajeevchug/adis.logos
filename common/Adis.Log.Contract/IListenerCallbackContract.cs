using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adis.Log.Contract
{
	public interface IListenerCallbackContract
	{
		[System.ServiceModel.OperationContract]
		void Notify(LogTransportObject logObject);
	}
}
