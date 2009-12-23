<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Adis.Log.Contract.LogTransportObject>" %>
<%
	var log = Model;
	 %>
<div class="log_item severity_<%=log.Severity %>">
<table style="table-layout:fixed">
	<%--<colgroup><col width="20%" /><col width="20%" /><col width="20%" /><col width="40%" /></colgroup>--%>
	<tbody>
		<tr>
			<td colspan="4" class="message"><%=Html.Encode(log.Message) %></td>
		</tr>
		<tr>
			<td title="Category"><%=Html.Encode(log.Category)%></td>
			<td title="Application"><%=Html.Encode(log.Application)%></td>
			<td title="Instance"><%=Html.Encode(log.Instance)%></td>
			<td title="User"><%=Html.Encode(log.User)%></td>
		</tr>
		<tr>
			<td title="Severity"><%=Html.Encode(log.Severity) %></td>
			<td title="Time"><%=Html.Encode(log.Time) %></td>
			<%if (!string.IsNullOrEmpty(log.ExtraInfo))
			{ %>
			<td colspan="2" align="right"><a class="show_extra_info_button" href="#" onclick="logging.main.ToggleExtraInfo(this)">Show Extra Info</a></td>
			<%} %>
		</tr>
		<%if (!string.IsNullOrEmpty(log.ExtraInfo))
		{ %>
		<tr>
			<td colspan="4">
				<div style="display:none;" class="extra_info" title="Extra Info">
					<div ><pre ><%=log.ExtraInfo%></pre></div>
					<div><a href="#" onclick="javascript:logging.main.ToggleExtraInfo(this)">Hide Extra Info</a></div>
				</div>
			</td>
		</tr>
		<%} %>
		</tbody>
	</table>
</div>
