using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using Adis.Log.Contract;
using log4net;
using System.ServiceModel;

namespace Adis.Log.Server
{
	/// <summary>
	/// 
	/// </summary>
	class ReporterService : IReporterContract
	{
		#region IReporterContract Members

		/// <summary>
		/// Returns Log events from the database according to the filtering required
		/// </summary>
		/// <param name="filter">allows you to filter the results returned to just those you are interested in</param>
		/// <param name="skipFirst">skip the first X records</param>
		/// <param name="maxRecords">Limits the number of recors returned to X</param>
		/// <returns>a List&lt;LogTransportObject&gt;. Each LogTransportObject contains the entire record from the database </returns>
		public List<LogTransportObject> GetRecords(RequestFilter filter, int skipFirst, int maxRecords)
		{
			return ReporterImplementer.GetRecords(filter, skipFirst, maxRecords, Repository.RepositoryInstance, OperationContext.Current.Channel.RemoteAddress.Uri, false);
		}


		#endregion
	}
}
