using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Server;
using System.Reflection;
using System.Runtime.Serialization;

namespace Adis.Log.server.Tests
{
	class RepositoryMock : IRepository
	{
		/// <summary>
		/// A method to allow us to create a IQueryable List(of T). The class exists in the framework 
		/// but has been marked as internal.
		/// </summary>
		/// <remarks>Courtousy of Ayende @ Rahien</remarks>
		private static IQueryable<T> CreateQueryable<T>(IEnumerable<T> inner)
		{
			Type queryable = typeof(IQueryable).Assembly.GetType("System.Linq.EnumerableQuery`1").MakeGenericType(typeof(T));
			ConstructorInfo constructor = queryable.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(IEnumerable<T>) }, new ParameterModifier[0]);
			object instance = FormatterServices.GetSafeUninitializedObject(queryable);
			constructor.Invoke(instance, new object[] { inner });
			return (IQueryable<T>)instance;
		}

		public List<LogEvent> _LogEventList;
		private int _NextEventId;

		//used as a point in time that is used to create the records. Can then be used in the filtering.
		public static DateTime _PointInTime = DateTime.Now;

		public RepositoryMock()
		{
			_NextEventId = 0;
			_LogEventList = new List<LogEvent>();
			//populate the mock with 5 default eventLog records
			_LogEventList.Add(GenerateLogEvent("1", "debug", _PointInTime.AddMinutes(1)));
			_LogEventList.Add(GenerateLogEvent("12", "debug", _PointInTime.AddMinutes(2)));
			_LogEventList.Add(GenerateLogEvent("123", "debug", _PointInTime.AddMinutes(3)));
			_LogEventList.Add(GenerateLogEvent("1234", "debug", _PointInTime.AddMinutes(4)));
			_LogEventList.Add(GenerateLogEvent("12345", "debug", _PointInTime.AddMinutes(5)));
		}

		private LogEvent GenerateLogEvent(String dataForEachProperty, String severity, DateTime time)
		{
			LogEvent newLogEvent = new LogEvent()
			{
				Application = dataForEachProperty,
				Category = dataForEachProperty,
				EventID = _NextEventId,
				ExtraInfo = dataForEachProperty,
				Instance = dataForEachProperty,
				Machine = dataForEachProperty,
				Message = dataForEachProperty,
				Severity = severity,
				User = dataForEachProperty,
				EventTime = time,
				TimeLogged = time
			};
			_NextEventId++;
			return newLogEvent;
		}

		#region IRepository Members

		public IQueryable<LogEvent> GetAllLogLogEvents()
		{
			IQueryable<LogEvent> allLogEvents = CreateQueryable(_LogEventList);
			return allLogEvents;
		}

		public void InsertIntoDatabase(LogEvent entity)
		{
			entity.EventID = _NextEventId;
			_NextEventId++;
			_LogEventList.Add(entity);
		}

		#endregion
	}
}
