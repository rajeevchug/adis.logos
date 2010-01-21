using System.Linq;

namespace Adis.Log.Server
{
	public class Repository : IRepository
	{
		private static string _ConnectionString;
		private static IRepository _RepositoryInstance = new Repository();
		
		public static IRepository RepositoryInstance
		{
			get {	return _RepositoryInstance ; }
		}

		private Repository()
		{
			_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataSourceConnectionString"] == null ?
				"" : System.Configuration.ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ConnectionString;
		}

		public Repository(string connectionString)
		{
			_ConnectionString = connectionString;
		}

		#region IRepository Members

		public IQueryable<LogEvent> GetAllLogLogEvents()
		{
			LoggerDataContext dbContext = new LoggerDataContext(_ConnectionString);
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
			using (LoggerDataContext dbContext = new LoggerDataContext(_ConnectionString))
			{
				dbContext.LogEvents.InsertOnSubmit(entity);
				dbContext.SubmitChanges();
			}
		}

		#endregion

	}
}
