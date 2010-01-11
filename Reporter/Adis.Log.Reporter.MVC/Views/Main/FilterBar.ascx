<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FilterBarViewData>" %>
<%
	var requestFilter = Model.RequestFilter;
	var minPage = 1;

	var severityOptions = new SelectList(
		Enum.GetValues(typeof(Severity)).Cast<int>()
		.Select(c => new { Value = c.ToString(), Text = ((Severity)c).ToString() }),
		"Value", "Text"
		);

	var categories = new SelectList(Model.Categories, requestFilter.Category);
	var applications = new SelectList(Model.Applications, requestFilter.Application);
	var logServers = new SelectList(Model.LogServers, Model.LogServer);

	var currentFilter = new
	{
		LogServer = Response.Cookies["LogServer"].Value,
		Category = Response.Cookies["Category"].Value,
		Application = Response.Cookies["Application"].Value,
		Severity = Response.Cookies["Severity"].Value,
		Machine = Response.Cookies["Machine"].Value,
		Instance = Response.Cookies["Instance"].Value,
		User = Response.Cookies["User"].Value,
		StartTime = Response.Cookies["StartTime"].Value,
		EndTime = Response.Cookies["EndTime"].Value,
	};

	var ShowFilterHeader = new Func<string, string, string>((label, value) =>
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return string.Format("<span class=\"filter\">{0}: <span class=\"filter_value\">{1}</span></span>", label, value);
		});
%>
<div class="filter_bar">
	<div class="filter_header">
		<%=ShowFilterHeader("Server", currentFilter.LogServer) %>
		<%=ShowFilterHeader("Category", currentFilter.Category)%>
		<%=ShowFilterHeader("Application", currentFilter.Application)%>
		<%=ShowFilterHeader("Severity", currentFilter.Severity)%>
		<%=ShowFilterHeader("Machine", currentFilter.Machine)%>
		<%=ShowFilterHeader("Instance", currentFilter.Instance)%>
		<%=ShowFilterHeader("User", currentFilter.User)%>
		<%=ShowFilterHeader("StartTime", currentFilter.StartTime)%>
		<%=ShowFilterHeader("EndTime", currentFilter.EndTime)%>
		<%--<div class="expander"  title="Expand / Collapse"></div>--%>
		<div class="brclear"></div>
	</div>
	<div id="filterForm">
	<% 
		using (var form = Html.BeginForm("ViewList", "Main"))
		{%>
	<table cellspacing="0" cellpadding="5" border="0" class="filter_section">
		<tr>
			<td>
				<label>Log Server</label>
			</td>
			<td>
				<%=Html.DropDownList("logServer", logServers)%>
			</td>
			<td>
				<label>Category</label>
			</td>
			<td>
				<%=Html.DropDownList("category", categories) %>
			</td>
			<td>
				<label>Application</label>
			</td>
			<td>
				<%=Html.DropDownList("application", applications)%>
			</td>
		</tr>
	</table>
	<table cellspacing="0" cellpadding="5" border="0" class="filter_section">
		<tr>
			<td>
				<label>User</label>
			</td>
			<td>
				<%=Html.TextBox("user", requestFilter.User)%>
				<%=Html.CheckBox("userExact", requestFilter.UserExactMatch, new { title = "Exact" })%>
			</td>
			<td>
				<label>Machine</label>
			</td>
			<td>
				<%=Html.TextBox("machine", requestFilter.Machine)%>
				<%=Html.CheckBox("machineExact", requestFilter.MachineExactMatch, new { title = "Exact" })%>
			</td>
			<td>
				<label>Instance</label>
			</td>
			<td>
				<%=Html.TextBox("instance", requestFilter.Instance)%>
				<%=Html.CheckBox("instanceExact", requestFilter.InstanceExactMatch, new { title = "Exact" })%>
			</td>
		</tr>
		<tr>
			<td>
				<label>Start time</label>
			</td>
			<td>
				<%=Html.TextBox("startTime", requestFilter.StartTime)%>
			</td>
			<td>
				<label>End time</label>
			</td>
			<td>
				<%=Html.TextBox("endTime", requestFilter.EndTime)%>
			</td>
			<td>
				<label>Max Severity</label>
			</td>
			<td>
				<%=Html.DropDownList("severity", severityOptions)%>
			</td>
		</tr>
		<tr>
			<td>
				<label>Message</label>
			</td>
			<td colspan="4">
				<%=Html.TextBox("Message", requestFilter.Message, new { style = "width:540px" })%>
			</td>
		</tr>
		<tr>
			<td>
				<a href="#" onclick="logging.main.SetPageNumberAndSubmit(<%=minPage %>)">Apply Filter</a>
				<!--<a href="#" onclick="logging.main.ResetFilterCookies()">Reset Filters</a>-->
			</td>
		</tr>
	</table>
	<input type="hidden" id="pageNumber" name="pageNumber" value="-1" />
	<%}
		 %>
	</div>
	<div>
		<span class="swatch severity_DEBUG">Debug</span>
		<span class="swatch severity_INFO">Info</span>
		<span class="swatch severity_WARN">Warning</span>
		<span class="swatch severity_ERROR">Error</span>
		<span class="swatch severity_FATAL">Fatal</span>
	</div>
		<script type="text/javascript">
			$(document).ready(function() { logging.main.FilterBarOnReady(); });
		</script>
</div>
