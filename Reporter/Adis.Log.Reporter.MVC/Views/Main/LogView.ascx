<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Adis.Log.Contract.LogTransportObject>" %>
<%
	var log = Model;
	;
	 %>
<div class="log_item severity_<%=log.Severity %> <%=string.IsNullOrEmpty(log.ExtraInfo) ? "" : "has_extra_info" %>" >
<div class="log_wrapper">
<table style="table-layout:fixed">
	<%--<colgroup><col width="20%" /><col width="20%" /><col width="20%" /><col width="40%" /></colgroup>--%>
	<tbody>
		<tr>
			<td valign="top" class="message">
				<%=Html.Encode(log.Message) %>
			<%if (!string.IsNullOrEmpty(log.ExtraInfo))
			{ %>
				<a class="show_extra_info_button" href="#">
					More...
				</a>
			<%} %>
			</td>
			<td width="12%" class="secondary_info">
				<div title="Time Logged"><%=Html.Encode(log.Time) %></div>
				<div title="Instance"><%=Html.Encode(log.Instance)%></div>
				<div title="User"><%=Html.Encode(log.User)%></div>
				<div title="Machine"><%=Html.Encode(log.Machine)%></div>
			</td>
		</tr>
		</tbody>
	</table>
		<%if (!string.IsNullOrEmpty(log.ExtraInfo))
		{ %>
				<div style="display:none;" class="extra_info" title="Extra Info">
					<div ><pre ><%=log.ExtraInfo%></pre></div>
				</div>
		<%} %>
</div>
</div>
