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
		currentOtherFilter = "EndDate";
		currentOtherFilterValue = requestFilter.EndTime.Value.ToString("dd/MM/yyyy");
	} 
	
	var categories = new SelectList(Model.Categories, requestFilter.Category);
	var applications = new SelectList(Model.Applications, requestFilter.Application);
	var logServers = new SelectList((new string[] { "" }).Concat(Model.LogServers), Model.LogServer);
	var otherFilters = new SelectList(
		new Dictionary<string, string>() { { "", ""}, {"Machine","Machine"}, {"Instance","Instance"}, {"User","User"}, {"EndDate" ,"EndDate" }}, 
		"Key", "Value", currentOtherFilter);
	
%>
<div class="filter_bar" >
	<div id="filterForm" class="filter_section">
	<% 
		using (var form = Html.BeginForm("ViewList", "Main"))
		{%>
		<div class="filter_line">
			<span>
				<label>Log Server:</label>
				<%=Html.DropDownList("logServer", logServers)%>
			</span>
			<span>
				<label>Category:</label>
				<%=Html.DropDownList("category", categories) %>
			</span>
			<span>
				<label>Application:</label>
				<%=Html.DropDownList("application", applications)%>
			</span>
		</div>
		<div class="filter_line">
			<span>
				<label>Min Severity:</label>
				<%=Html.DropDownList("severity", severityOptions)%>
			</span>
			<span>
				<label>Start Date:</label>
				<%=Html.TextBox("startTime", requestFilter.StartTime, new { style = "width:80px;" })%>
				
			</span>
			<span>
				<label>Message:</label>
				<%=Html.TextBox("message", requestFilter.Message, new { style = "width:90px;" })%>
			</span>
			<span>
				<label>Other:</label>
				<span>
					<%=Html.DropDownList("otherFiltersOption", otherFilters)%>
					<%=Html.TextBox("otherFiltersValue", currentOtherFilterValue, new { style = "width:80px;" })%>
				</span>
			</span>
		</div>
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
