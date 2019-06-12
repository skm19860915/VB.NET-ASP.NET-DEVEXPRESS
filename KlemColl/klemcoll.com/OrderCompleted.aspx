<%@ Page Language="VB" MasterPageFile="~/NoLeftNav.master" AutoEventWireup="false" Inherits="Klemcoll.Website.OrderCompleted" title="Untitled Page" Codebehind="OrderCompleted.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Your order has been completed. An email confirming your order has been sent to
                <asp:Label ID="EmailLabel" runat="server"></asp:Label></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl=' <%$ AppSettings:OrderCompleteAddress %> '>Click Here to go back to The Klemantaski Collection</asp:HyperLink></td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

