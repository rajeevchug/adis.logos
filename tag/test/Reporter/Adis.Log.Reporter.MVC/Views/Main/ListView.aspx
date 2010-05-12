<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<ListViewData>" %>

<%
	var requestFilter = Model.RequestFilter;
	var logList = Model.Logs;
	
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Logging Reporter</title>
	<link href="<%=AppHelper.CssUrl("ListView.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%=AppHelper.CssUrl("SquareButton.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%=AppHelper.CssUrl("datepicker.css")%>" rel="stylesheet" type="text/css"  />
	<script src="<%=AppHelper.ScriptUrl("jquery-1.3.2.min.js") %>" type="text/javascript"></script>
	<script src="<%=AppHelper.ScriptUrl("main.js") %>" type="text/javascript"></script>
	<script type="text/javascript" src="<%=AppHelper.ScriptUrl("jquery.bgiframe.js")%>"></script>
	<script type="text/javascript" src="<%=AppHelper.ScriptUrl("jquery.datePicker.js")%>"></script>
	<script type="text/javascript" src="<%=AppHelper.ScriptUrl("date.js")%>"></script>
</head>
<body>
	<div class="wrapper">
	<div id="loadingImage"><img src="<%=AppHelper.ImageUrl("loading.large.gif") %>" alt="loading" /></div>
	<div class="error_message"><%=Model.ErrorMessage %></div>
	<div class="top_section">
		<div class="page_header">Log Reporter for <%=Model.LogServer %></div>
		<%Html.RenderPartial("FilterBar", new FilterBarViewData()
	 {
		 RequestFilter = requestFilter,
		 SelectedCategory = Model.SelectedCategory,
		 Categories = Model.Categories,
		 Applications = Model.Applications,
		 LogServers = Model.LogServers,
		 LogServer = Model.LogServer
	 }); %>
		<div class="last_line">
			<div id="submitButton">
				<%=Html.ActionButton("logging.main.SetPageNumberAndSubmit(1)", "Apply Filters", ButtonCategories.Highlight) %>
				<%=Html.ActionButton("logging.main.ResetFilters()", "Clear", ButtonCategories.Cancel)%>
			</div>
			<div id="pager">
				<%Html.RenderPartial("Pager"); %>
			</div>
			<div id="severityLegend">
				<span class="swatch severity_DEBUG" title="Debug">D</span>
				<span class="swatch severity_INFO" title="Info">I</span> 
				<span class="swatch severity_WARN" title="Warning">W</span> 
				<span class="swatch severity_ERROR" title="Error">E</span> 
				<span class="swatch severity_FATAL" title="Fatal">F</span>
			</div>
			<br class="brclear" />
		</div>
	</div>
	<div class="main_section">
		<%
			if (logList.Count() > 0)
			{
				foreach (var log in logList)
				{
					Html.RenderPartial("LogView", log);
				}
			}
			else
			{
				%>
				<h3>No Results Found</h3>
				<%
			}
			%>
	</div>
	</div>
</body>
<script type="text/javascript">
	var urls =  
{
	Applications: '<%=Url.Action("Applications", "Main", new {  server = "__server__", category = "__category__" }) %>',
	Categories: '<%=Url.Action("Categories", "Main", new { server = "__server__" }) %>',
	Images: '<%= Url.Content("Images") %>'
};
</script>
</html>
