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
<%--	<link href="<%=AppHelper.CssUrl("jquery-ui-1.7.2.custom.css") %>" rel="stylesheet" type="text/css" />--%>
	<link href="<%=AppHelper.ScriptUrl("datepicker.css")%>" rel="stylesheet" type="text/css"  />
	<script src="<%=AppHelper.ScriptUrl("jquery-1.3.2.min.js") %>" type="text/javascript"></script>
	<script src="<%=AppHelper.ScriptUrl("main.js") %>" type="text/javascript"></script>
<%--	<script src="<%=AppHelper.ScriptUrl("jquery-ui-1.7.2.custom.min.js") %>" type="text/javascript"></script>--%>
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
		<div>
			<div style="float: left; padding:3px;">
					<a href="#" onclick="logging.main.SetPageNumberAndSubmit(1)">Apply Filter</a>
					<!--<a href="#" onclick="logging.main.ResetFilterCookies()">Reset Filters</a>-->
			</div>
			<div style="float: right;">
				<%Html.RenderPartial("Pager"); %>
			</div>
			<div style="float: right; padding:3px;">
				<span class="swatch severity_DEBUG">Debug</span>
				<span class="swatch severity_INFO">Info</span> 
				<span class="swatch severity_WARN">Warning</span> 
				<span class="swatch severity_ERROR">Error</span> 
				<span class="swatch severity_FATAL">Fatal</span>
			</div>
			<br class="brclear" />
		</div>
	</div>
	<div class="main_section">
		<%foreach (var log in logList)
		{
			Html.RenderPartial("LogView", log);
		} %>
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
