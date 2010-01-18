<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FilterBarViewData>" %>
<%
	var requestFilter = Model.RequestFilter;

	var severityOptions = new SelectList(
		Enum.GetValues(typeof(Severity)).Cast<int>()
		.Select(c => new { Value = c.ToString(), Text = ((Severity)c).ToString() }),
		"Value", "Text"
		);

	var currentOtherFilter = "";
	var currentOtherFilterValue = "";
	if (!string.IsNullOrEmpty(requestFilter.Machine))
	{
		currentOtherFilter = "Machine";
		currentOtherFilterValue = requestFilter.Machine;
	} else if (!string.IsNullOrEmpty(requestFilter.User))
	{
		currentOtherFilter = "User";
		currentOtherFilterValue = requestFilter.User;
	}
	else if (!string.IsNullOrEmpty(requestFilter.Instance))
	{
		currentOtherFilter = "Instance";
		currentOtherFilterValue = requestFilter.Instance;
	}
	else if (requestFilter.EndTime != null)
	{
		currentOtherFilter = "EndTime";
		currentOtherFilterValue = requestFilter.EndTime.Value.ToString("yyyy-M-dd");
	} 
	
	var categories = new SelectList(Model.Categories, requestFilter.Category);
	var applications = new SelectList(Model.Applications, requestFilter.Application);
	var logServers = new SelectList(Model.LogServers, Model.LogServer);
	var otherFilters = new SelectList(new List<string>() { "", "Machine", "Instance", "User", "EndDate" }, currentOtherFilter);
	
	//var ShowFilterHeader = new Func<string, string, string>((label, value) =>
	//  {
	//    if (string.IsNullOrEmpty(value))
	//    {
	//      return string.Empty;
	//    }
	//    return string.Format("<span class=\"filter\">{0}: <span class=\"filter_value\">{1}</span></span>", label, value);
	//  });
%>
<div class="filter_bar" >
	<%--<div class="filter_header">
		<%=ShowFilterHeader("Server", currentFilter.LogServer) %>
		<%=ShowFilterHeader("Category", currentFilter.Category)%>
		<%=ShowFilterHeader("Application", currentFilter.Application)%>
		<%=ShowFilterHeader("Severity", currentFilter.Severity)%>
		<%=ShowFilterHeader("Machine", currentFilter.Machine)%>
		<%=ShowFilterHeader("Instance", currentFilter.Instance)%>
		<%=ShowFilterHeader("User", currentFilter.User)%>
		<%=ShowFilterHeader("StartTime", currentFilter.StartTime)%>
		<%=ShowFilterHeader("EndTime", currentFilter.EndTime)%>
		<%--<div class="expander"  title="Expand / Collapse"></div>
		<div class="brclear"></div>
	</div>--%>
	<div id="filterForm" class="filter_section">
	<% 
		using (var form = Html.BeginForm("ViewList", "Main"))
		{%>
		<div class="filter_line">
			<div>
				<label>Log Server</label>
				<%=Html.DropDownList("logServer", logServers)%>
			</div>
			<div>
				<label>Category</label>
				<%=Html.DropDownList("category", categories) %>
			</div>
			<div>
				<label>Application</label>
				<%=Html.DropDownList("application", applications)%>
			</div>
			<div>
				<label>Min Severity</label>
				<%=Html.DropDownList("severity", severityOptions)%>
			</div>
			<div>
				<label>Start Date</label>
				<%=Html.TextBox("startTime", requestFilter.StartTime, new { style = "width:80px;" })%>
				
			</div>
			<div>
				<label>Message</label>
				<%=Html.TextBox("message", requestFilter.Message, new { style = "width:90px;" })%>
			</div>
			<div>
				<label>Other</label>
				<div>
					<%=Html.DropDownList("otherFiltersOption", otherFilters)%>
					<%=Html.TextBox("otherFiltersValue", currentOtherFilterValue, new { style = "width:80px;" })%>
				</div>
			</div>
		</div>
		<br class="brclear" />
	<input type="hidden" id="pageNumber" name="pageNumber" value="-1" />
	<input type="hidden" id="instance" name="instance" value="" />
	<input type="hidden" id="instanceExact" name="instanceExact" value="false" />
	<input type="hidden" id="machine" name="machine" value="" />
	<input type="hidden" id="machineExact" name="machineExact" value="false" />
	<input type="hidden" id="user" name="user" value="" />
	<input type="hidden" id="userExact" name="userExact" value="false" />
	<input type="hidden" id="endTime" name="endTime" value="" />
	<%}
		 %>
	</div>
		<script type="text/javascript">
			$(document).ready(function() { logging.main.FilterBarOnReady(); });
		</script>
</div>
