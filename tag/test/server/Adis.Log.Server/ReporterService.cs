using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using Adis.Log.Contract;
using log4net;
using System.ServiceModel;
using System;

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
		public IEnumerable<LogTransportObject> GetRecords(RequestFilter filter, int skipFirst, int maxRecords)
		{
			return HandleMethodSetup(uri=> ReporterImplementer.GetRecords(filter, skipFirst, maxRecords, Repository.RepositoryInstance, uri, false),
				"ReporterService.GetRecords() got an error");
		}


    /// <summary>
    /// Returns number of Log events from the database according to the filtering required
    /// </summary>
    /// <param name="filter">allows you to filter the results returned to just those you are interested in</param>
    /// <returns>an int with the number of matching events</returns>
    public int GetCount(RequestFilter filter)
    {
			return HandleMethodSetup(uri => ReporterImplementer.GetCount(filter, Repository.RepositoryInstance, uri, false),
				"ReporterService.GetCount() got an error");
    }

		public IEnumerable<string> GetCategoryList()
		{
			return HandleMethodSetup(uri => ReporterImplementer.GetCategoryList(Repository.RepositoryInstance, uri),
				"ReporterService.GetCount() got an error");
		}

		public Dictionary<string, IEnumerable<string>> GetApplicationList()
		{
			return HandleMethodSetup(uri => ReporterImplementer.GetApplicationList(Repository.RepositoryInstance, uri),
				"ReporterService.GetCount() got an error");
		}

		#endregion

		private T HandleMethodSetup<T>(Func<Uri, T> implementerMethodToRun, string exceptionMessage)
		{
			ILog internalLog = LogManager.GetLogger(typeof(ReporterImplementer));
			try
			{
				Uri uri = null;
				if (OperationContext.Current != null &&
					OperationContext.Current.Channel != null &&
					OperationContext.Current.Channel.RemoteAddress != null &&
					OperationContext.Current.Channel.RemoteAddress.Uri != null)
				{
					uri = OperationContext.Current.Channel.RemoteAddress.Uri;
				}
				else
				{
					uri = new Uri("http://invalidUri");
				}
				return implementerMethodToRun(uri);
			}
			catch (Exception e)
			{
				internalLog.Error(exceptionMessage, e);
				throw;
			}
		}


	}
}
