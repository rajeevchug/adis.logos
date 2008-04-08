using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adis.Log.Server
{
	public interface IRepository
	{
		IQueryable<LogEvent> GetAllLogLogEvents();
		void InsertIntoDatabase(LogEvent entity);
	}
}
