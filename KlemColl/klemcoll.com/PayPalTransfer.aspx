<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.PayPalTransfer" Codebehind="PayPalTransfer.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 846px">
            <tr>
                <td colspan="1" style="height: 186px">
                    <img alt="The Klemantaski Collection, a library of motorsport photography" src="Images/Head_Main.jpg" /></td>
            </tr>
            <tr>
                <td style="vertical-align: top;background-color:#FFFFFF;">
<br />
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/paypal.gif" /><br />
    <br />
    You are now leaving Klemcoll.com<br />
    Click Continue to be transfered to paypal<br />
    <br />

    <asp:HiddenField ID="cmd" Value="_xclick" runat="server" />
    <asp:HiddenField ID="business" runat="server" />
    <asp:HiddenField ID="item_name" runat="server" />
    <asp:HiddenField ID="invoice" runat="server" />
    <asp:HiddenField ID="custom" runat="server" />
    <asp:HiddenField ID="amount" runat="server" />
    <asp:HiddenField ID="shipping" runat="server" />
    <asp:HiddenField ID="image_url" runat="server" />
    <asp:HiddenField ID="no_shipping" Value="1" runat="server" />
    <asp:HiddenField ID="currency_code" Value="USD" runat="server" />
    <asp:Button ID="ContButton" runat="server" Text="Continue" PostBackUrl="https://www.paypal.com/cgi-bin/webscr" CssClass="ButtonClass" Height="24px" Width="96px" /><br /><br />


                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: center;">
                    </td>
            </tr>
        </table>
            </div>
    </form>
</body>
</html>
