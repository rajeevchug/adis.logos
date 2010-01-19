<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ListViewData>" %>
<%
	var minPage = 1;
	var maxPage = Model.MaxPage;

	var minPageNumToDisplay = Model.Page - 3 < minPage ? minPage : Model.Page - 3;
	var maxPageNumToDisplay = minPageNumToDisplay + 6 > maxPage ? maxPage : minPageNumToDisplay + 6;
	
	 %>
<div class="pager">
<%if (Model.Page != minPage)
	{ %>
		 <%=Html.ActionButton("logging.main.SetPageNumberAndSubmit("+minPage+")", "|&lt;&lt;", ButtonCategories.Normal)%>
		 <%=Html.ActionButton("logging.main.SetPageNumberAndSubmit("+(Model.Page-1)+")", "&lt;", ButtonCategories.Normal)%>
		 <%if (minPageNumToDisplay > minPage)
		 { %>
		 <%=Html.ActionButton("logging.main.SetPageNumberAndSubmit("+minPage+")", minPage.ToString(), ButtonCategories.Normal)%>
		 <%=Html.ActionButton("", "...", ButtonCategories.None)%>
		 <%} 
	}
	else
	{ %>
		 <%=Html.ActionButton("", "|&lt;&lt;", ButtonCategories.Disabled)%>
		 <%=Html.ActionButton("", "&lt;", ButtonCategories.Disabled)%>
<%} %>
<%for (int i = minPageNumToDisplay; i <= maxPageNumToDisplay; i++)
	{
		if (i == Model.Page)
		{
			Response.Write(Html.ActionButton("", i.ToString(), ButtonCategories.None));
		}
		else
		{
			Response.Write(Html.ActionButton("logging.main.SetPageNumberAndSubmit(" + i + ")", i.ToString(), ButtonCategories.Normal));
		}
	}
		 %>
<%if (Model.Page < maxPage)
	{ 
		 if (maxPageNumToDisplay < maxPage)
		 { %>
		 <%=Html.ActionButton("", "...", ButtonCategories.None)%>
			<%=Html.ActionButton("logging.main.SetPageNumberAndSubmit(" + maxPage + ")", maxPage.ToString(), ButtonCategories.Normal) %>
		 <%} %>
			<%=Html.ActionButton("logging.main.SetPageNumberAndSubmit(" + Model.Page + 1 + ")", "&gt;", ButtonCategories.Normal)%>
			<%=Html.ActionButton("logging.main.SetPageNumberAndSubmit(" + maxPage + ")", "&gt;&gt;|", ButtonCategories.Normal) %>
<%}
	else
	{ %>
		 <%=Html.ActionButton("", "&gt;", ButtonCategories.Disabled)%>
		 <%=Html.ActionButton("", "&gt;&gt;|", ButtonCategories.Disabled)%>
<%} %>
</div>
