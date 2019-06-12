<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.Downloads" title="Downloads" Codebehind="Downloads.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="text-align: left; width: 100%;" cellpadding="4" cellspacing="0" border="0">
    <tr><td>
        <% Response.WriteFile("~/ContentPages/Downloads.htm")%>
    </td></tr>
</table>
</asp:Content>

