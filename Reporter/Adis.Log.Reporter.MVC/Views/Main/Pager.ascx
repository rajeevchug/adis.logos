﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ListViewData>" %>
<%
	var minPage = 1;
	var maxPage = Model.MaxPage;

	var minPageNumToDisplay = Model.Page - 3 < minPage ? minPage : Model.Page - 3;
	var maxPageNumToDisplay = minPageNumToDisplay + 6 > maxPage ? maxPage : minPageNumToDisplay + 6;
	
	 %>
<div class="pager">
<%if (Model.Page != minPage)
	{ %>
		 <a href="#" class='pager_item' onclick="logging.main.SetPageNumberAndSubmit(<%=minPage %>)">|&lt;&lt;</a>
		 <a href="#" class='pager_item' onclick="logging.main.SetPageNumberAndSubmit(<%=Model.Page-1 %>)">&lt;</a>
		 <%if (minPageNumToDisplay > minPage)
		 { %>
		 <a href="#" class='pager_item' onclick="logging.main.SetPageNumberAndSubmit(<%=minPage %>)"><%=minPage%></a>
		 <span class='pager_item'>...</span>
		 <%} 
	}
	else
	{ %>
		 <span class='pager_item'>|&lt;&lt;</span>
		 <span class='pager_item'>&lt;</span>
<%} %>
<%for (int i = minPageNumToDisplay; i <= maxPageNumToDisplay; i++)
	{
		if (i == Model.Page)
		{
			Response.Write("<span class='current_page'>" + i + "</span>");
		}
		else
		{
			Response.Write("<a href='#' class='pager_item'  onclick=\"logging.main.SetPageNumberAndSubmit(" + i + ")\">" + i + "</a>");
		}
	}
		 %>
<%if (Model.Page != maxPage)
	{ 
		 if (maxPageNumToDisplay < maxPage)
		 { %>
			<span class='pager_item'>...</span>
		 <a href="#" class='pager_item' onclick="logging.main.SetPageNumberAndSubmit(<%=maxPage %>)"><%=maxPage%></a>
		 <%} %>
		 <a href="#" class='pager_item' onclick="logging.main.SetPageNumberAndSubmit(<%=Model.Page+1 %>)">&gt;</a>
		 <a href="#" class='pager_item' onclick="logging.main.SetPageNumberAndSubmit(<%=maxPage %>)">&gt;&gt;|</a>
<%}
	else
	{ %>
		 <span class='pager_item'>&gt;</span>
		 <span class='pager_item'>&gt;&gt;|</span>
<%} %>
</div>
