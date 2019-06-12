<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.PrivacyPolicy" title="Untitled Page" Codebehind="PrivacyPolicy.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="text-align: left; width: 100%;" cellpadding="4" cellspacing="0" border="0">
    <tr><td>
        <% Response.WriteFile("ContentPages/PrivacyPolicy.htm")%>
    </td></tr>
</table>
</asp:Content>

