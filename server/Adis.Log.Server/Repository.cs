using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Adis.Log.Server
{
	public class Repository : IRepository
	{
		private static IRepository _RepositoryInstance = new Repository();
		
		public static IRepository RepositoryInstance
		{
			get {	return _RepositoryInstance ; }
		}

		private Repository()
		{
		}

		#region IRepository Members

		public IQueryable<LogEvent> GetAllLogLogEvents()
		{
			LoggerDataContext dbContext = new LoggerDataContext();
			{
				return from le in dbContext.LogEvents
							 select le;
			}
			//Note, we don't dispose the dbContext here because we will probably still need it in the code that called this 
			//method. eg the code
			//   IQueryable<LogEvent> logEvents = Repository.GetInstance.GetAllLogEvents();
			//   return logEvents.First();
			// The return call is the one that actually accesses the DB and for that it needs the dbcontext
		}


		public void InsertIntoDatabase(LogEvent entity)
		{
			using (LoggerDataContext dbContext = new LoggerDataContext())
			{
				dbContext.LogEvents.InsertOnSubmit(entity);
				dbContext.SubmitChanges();
			}
		}

		#endregion

	}
}
