using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Adis.Log.Contract
{
	[DataContract]
	public class RequestFilter
	{
		public RequestFilter()
		{
			CategoryExactMatch = false;
			ApplicationExactMatch = false;
			InstanceExactMatch = false;
			UserExactMatch = false;
			MachineExactMatch = false;
			StartTime = null;
			EndTime = null;
		}

		#region Data Members
		[DataMember]
		public int? Id { get; set; }

		[DataMember]
		public String Category { get; set; }

		[DataMember]
		public String Application { get; set; }

		[DataMember]
		public String Instance { get; set; }

		[DataMember]
		public String User { get; set; }

		[DataMember]
		public String Machine { get; set; }

		[DataMember]
		public String Severity { get; set; }

		[DataMember]
		public String Message { get; set; }

		[DataMember]
		public DateTime? StartTime { get; set; }

		[DataMember]
		public DateTime? EndTime { get; set; }

		[DataMember]
		public bool CategoryExactMatch { get; set; }

		[DataMember]
		public bool ApplicationExactMatch { get; set; }

		[DataMember]
		public bool InstanceExactMatch { get; set; }

		[DataMember]
		public bool UserExactMatch { get; set; }

		[DataMember]
		public bool MachineExactMatch { get; set; }
		#endregion

		#region non DataMemeber operations

		private static Dictionary<string, int> severityRanks = new Dictionary<string, int>() {
			{"debug", 5},
			{"info", 4},
			{"warn", 3},
			{"error", 2},
			{"fatal", 1}
		};

		public static Dictionary<string, int> SeverityRanks
		{
			get { return severityRanks; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="severity"></param>
		/// <returns></returns>
		public static IEnumerable<String> GetValidSeverities(String severityToCheck)
		{
			IEnumerable<String> validSeverities = new List<String>();
			
			//if the severity we are checking is blank or null then there are no valid severities
			if (String.IsNullOrEmpty(severityToCheck))
			{
				return validSeverities;
			}

			String lowerSeverityToCheck = severityToCheck.ToLower();

			//If the severity we are checking isn't one we recognise then there are no valid severities
			if (!severityRanks.Keys.Contains(lowerSeverityToCheck))
			{
				return validSeverities;
			}

			//We now know we have a valid severity that we are checking against
			int levelOfSeverityToCheck = severityRanks[lowerSeverityToCheck];

			return severityRanks.Keys.Where(severity => severityRanks[severity] <= levelOfSeverityToCheck);
		}

		/// <summary>
		/// Checks that a LogTransportObject satisfies a RequestFilter.
		/// </summary>
		/// <remarks>
		/// Note: we don't filter on the Id field here because that would only allow us to ever have one log entry. 
		/// The Id is more useful for adhoc reporting
		/// </remarks>
		/// <param name="requestFilter"></param>
		/// <param name="transportObject"></param>
		/// <returns></returns>
		public static bool SatisfiesFilter(RequestFilter requestFilter, LogTransportObject transportObject)
		{
			bool success = true;
			success = success && DoesStringMatch(transportObject.Category, requestFilter.Category, requestFilter.CategoryExactMatch);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: {2} Category {0} != {1}", transportObject.Category, requestFilter.Category, requestFilter.CategoryExactMatch ? "EXACT" : ""));
#endif
			success = success && DoesStringMatch(transportObject.Application, requestFilter.Application, requestFilter.ApplicationExactMatch);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: {2} Application {0} != {1}", transportObject.Application, 
					requestFilter.Application, requestFilter.ApplicationExactMatch ? "EXACT" : ""));
#endif
			success = success && DoesStringMatch(transportObject.Instance, requestFilter.Instance, requestFilter.InstanceExactMatch);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: {2} Instance {0} != {1}", transportObject.Instance, 
					requestFilter.Instance, requestFilter.InstanceExactMatch ? "EXACT" : ""));
#endif
			success = success && DoesStringMatch(transportObject.Machine, requestFilter.Machine, requestFilter.MachineExactMatch);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: {2} Machine {0} != {1}", transportObject.Machine, 
					requestFilter.Machine, requestFilter.MachineExactMatch ? "EXACT" : ""));
#endif
			success = success && DoesStringMatch(transportObject.User, requestFilter.User, requestFilter.UserExactMatch);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: {2} User {0} != {1}", transportObject.User, 
					requestFilter.User, requestFilter.UserExactMatch ? "EXACT" : ""));
#endif
			success = success && DoesStringMatch(transportObject.Message, requestFilter.Message, false);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: Message {0} != {1}", transportObject.Message,
					requestFilter.Message));
#endif

			success = success && DoesSeveritySatisfyFilter(transportObject.Severity, requestFilter.Severity);
#if DEBUG
			if (!success)
				System.Diagnostics.Debug.WriteLine(String.Format("SatisfiesFilter: Severity {0} != {1}", transportObject.Severity,
					requestFilter.Severity));
#endif

			return success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="success"></param>
		/// <returns></returns>
		private static bool DoesSeveritySatisfyFilter(String logSeverity, String filter)
		{
			bool success = true;
			if (String.IsNullOrEmpty(filter))
			{
				//we haven't got any filter for the severity so we have to pass.
				return true;
			}
			if (logSeverity == null)
			{
				return false;
			}
			String lowerCaseSeverity = logSeverity.ToLower();
			String lowerCaseFilter = filter.ToLower();
			//if either the log's or filter's severity isn't one that we recognise then we will fail
			success = success && severityRanks.Keys.Contains(lowerCaseSeverity);
			success = success && severityRanks.Keys.Contains(lowerCaseFilter);

			//now that we know that both the log and filter severity have known ranks we can compare them
			success = success && (severityRanks[lowerCaseSeverity] >= severityRanks[lowerCaseFilter]);
			return success;
		}

		/// <summary>
		/// Decides whether a string from a LogObject matches a filter string
		/// </summary>
		/// <param name="logString"></param>
		/// <param name="filter"></param>
		/// <param name="exactMatch"></param>
		/// <returns></returns>
		private static bool DoesStringMatch(String logString, String filter, bool exactMatch)
		{
			//if filter string is null or blank then it will *always* match
			if (String.IsNullOrEmpty(filter))
			{
				return true;
			}
			//by now we have verified there is something in the filter. If the logString is null
			//then it automatically doesn't match the filter.
			if (logString == null)
			{
				return false;
			}

			if (exactMatch)
			{
				if (logString != filter)
				{
					return false;
				}
			}
			else
			{
				if (!logString.StartsWith(filter))
				{
					return false;
				}
			}
			return true;
		}

		#endregion
	}

}