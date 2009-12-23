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
	<script src="<%=AppHelper.ScriptUrl("jquery-1.3.2.min.js") %>" type="text/javascript"></script>
	<script src="<%=AppHelper.ScriptUrl("main.js") %>" type="text/javascript"></script>
</head>
<body>
	<%Html.RenderPartial("FilterBar", new FilterBarViewData() 
	 { RequestFilter = requestFilter, 
		 SelectedCategory = Model.SelectedCategory, 
		 Categories = Model.Categories, 
		 Applications = Model.Applications,
		 LogServers = Model.LogServers,
		 LogServer = Model.LogServer
	 }); %>
	 <div class="error_message"><%=Model.ErrorMessage %></div>
	<%Html.RenderPartial("Pager"); %>
	<div>
		<%foreach (var log in logList)
		{
			Html.RenderPartial("LogView", log);
		} %>
	</div>
	<%Html.RenderPartial("Pager", Model); %>
</body>
</html>
