<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FilterBarViewData>" %>
<%
	var requestFilter = Model.RequestFilter;
	var minPage = 1;

	var severityOptions = new SelectList(
		Enum.GetValues(typeof(Severity)).Cast<int>()
		.Select(c => new { Value = c.ToString(), Text = ((Severity)c).ToString() }),
		"Value", "Text"
		);

	Func<IEnumerable<string>, string, SelectList> PrefixEmptyEntry = (collection, currentFilter) =>
		{
			if (!collection.Contains(""))
			{
				var temp = collection.ToList();
				temp.Insert(0, "");
				collection = temp;
			}
			return new SelectList(collection, currentFilter);
		};
	
	var categories = PrefixEmptyEntry(Model.Categories, requestFilter.Category);
	var applications = PrefixEmptyEntry(Model.Applications, requestFilter.Application);
	var logServers = PrefixEmptyEntry(Model.LogServers, Model.LogServer);	
	
%>
<div class="filter_bar">
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
			</td>
		</tr>
	</table>
	<input type="hidden" name="pageNumber" value="-1" />
	<%}
		 %>
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
