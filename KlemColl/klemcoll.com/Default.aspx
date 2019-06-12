<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website._Default" Codebehind="Default.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="text-align: left; width: 100%; height: 100%" cellpadding="4" cellspacing="0" border="0">
    <tr><td>
        <% Response.WriteFile("~/ContentPages/Homepage.htm")%>
    </td></tr>
</table>
</asp:Content>

