<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Adis.Log.Contract.LogTransportObject>" %>
<%
	var log = Model;
	;
	 %>
<div class="log_item severity_<%=log.Severity %> <%=string.IsNullOrEmpty(log.ExtraInfo) ? "" : "has_extra_info" %>" 
	>
<table style="table-layout:fixed">
	<%--<colgroup><col width="20%" /><col width="20%" /><col width="20%" /><col width="40%" /></colgroup>--%>
	<tbody>
		<tr>
			<td colspan="4" class="message">
				<%=Html.Encode(log.Message) %>
			<%if (!string.IsNullOrEmpty(log.ExtraInfo))
			{ %>
				<a class="show_extra_info_button" href="#">
					More...
				</a>
			<%} %>
			</td>
		</tr>
		<tr>
			<td title="Time Logged"><%=Html.Encode(log.Time) %></td>
			<td title="Instance"><%=Html.Encode(log.Instance)%></td>
			<td title="User"><%=Html.Encode(log.User)%></td>
			<td title="Machine"><%=Html.Encode(log.Machine)%></td>
		</tr>
		<tr>
		</tr>
		<%if (!string.IsNullOrEmpty(log.ExtraInfo))
		{ %>
		<tr>
			<td colspan="4">
				<div style="display:none;" class="extra_info" title="Extra Info">
					<div ><pre ><%=log.ExtraInfo%></pre></div>
				</div>
			</td>
		</tr>
		<%} %>
		</tbody>
	</table>
</div>
