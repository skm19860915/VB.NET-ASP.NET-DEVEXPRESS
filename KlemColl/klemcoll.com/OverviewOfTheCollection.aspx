<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.OverviewOfTheCollection" title="Overview of the Collection" Codebehind="OverviewOfTheCollection.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="text-align: left; width: 100%;" cellpadding="4" cellspacing="0" border="0">
    <tr><td>
        <% Response.WriteFile("ContentPages/OverviewOfTheCollection.htm")%>
    </td></tr>
</table>
</asp:Content>

