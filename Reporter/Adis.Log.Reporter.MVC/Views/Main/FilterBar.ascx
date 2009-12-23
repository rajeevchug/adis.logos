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
	<% using (var form = Html.BeginForm("ViewList", "Main"))
		{%>
	<table cellspacing="0" cellpadding="5" border="0">
		<tr>
			<td>
				<label>Category</label>
			</td>
			<td>
				<%=Html.DropDownList("category", categories) %>
			</td>
			<td>
				<label>Instance</label>
			</td>
			<td>
				<%=Html.TextBox("instance", requestFilter.Instance)%>
			</td>
			<td>
				<%=Html.CheckBox("instanceExact", requestFilter.InstanceExactMatch)%>
				exact
			</td>
			<td>
				<label>End time</label>
			</td>
			<td>
				<%=Html.TextBox("endTime", requestFilter.EndTime)%>
			</td>
		</tr>
		<tr>
			<td>
				<label>Application</label>
			</td>
			<td>
				<%=Html.DropDownList("application", applications)%>
			</td>
			<td>
				<label>User</label>
			</td>
			<td>
				<%=Html.TextBox("user", requestFilter.User)%>
			</td>
			<td>
				<%=Html.CheckBox("userExact", requestFilter.UserExactMatch)%>
				exact
			</td>
			<td>
				<label>Start time</label>
			</td>
			<td>
				<%=Html.TextBox("startTime", requestFilter.StartTime)%>
			</td>
		</tr>
		<tr>
			<td>
				<label>Max Severity</label>
			</td>
			<td>
				<%=Html.DropDownList("severity", severityOptions)%>
			</td>
			<td>
				<label>Machine</label>
			</td>
			<td>
				<%=Html.TextBox("machine", requestFilter.Machine)%>
			</td>
			<td>
				<%=Html.CheckBox("machineExact", requestFilter.MachineExactMatch)%>
				exact
			</td>
			<td>
			<label>Log Server</label>
			</td>
			<td>
				<%=Html.DropDownList("logServer", logServers)%>
			</td>
		</tr>
		<tr>
			<td>
				<label>Message</label>
			</td>
			<td colspan="4">
				<%=Html.TextBox("Message", requestFilter.Message, new { style = "width:340px" })%>
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

		
</div>
