<%@ Page Language="VB" MasterPageFile="~/NoLeftNav.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ConfirmOrder" title="Untitled Page" Codebehind="ConfirmOrder.aspx.vb" %>

<%@ Register Src="ShowOrder.ascx" TagName="ShowOrder" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<table style="width: 90%;">
        <tr>
            <td colspan="2" style="text-align: left" class="basefont">
                Please print this screen and save it for your records. After reviewing your order
                click the "Submit Order" button to complete the transaction.</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <uc1:ShowOrder ID="ShowOrder1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit Order" CssClass="ButtonClass" /></td>
        </tr>
    </table><br /><asp:HiddenField ID="CCNumberHiddenField" runat="server" />
    <asp:HiddenField ID="NameOnCardHiddenField" runat="server" />
    <asp:HiddenField ID="CCExpDateHiddenField" runat="server" />

</asp:Content>

