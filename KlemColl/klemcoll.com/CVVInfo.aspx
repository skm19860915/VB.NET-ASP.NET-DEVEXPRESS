<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.CVVInfo" Codebehind="CVVInfo.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 90%">
            <tr>
                <td align="left" class="HeaderTextCell" colspan="2">
                    <strong>CVV</strong> is an anti-fraud security feature to help verify that you are
                    in possession of your credit card. For Visa/Mastercard, the three-digit CVV number
                    is printed on the signature panel on the back of the card immediately after the
                    card's account number For American Express, the four-digit CVV number is printed
                    on the front of the card above the card accountnumber.</td>
            </tr>
            <tr>
                <td align="left" style="width: 50%; height: 28px">
                    &nbsp;</td>
                <td align="left" style="height: 28px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="width: 50%">
                    <strong>Visa/Mastercard</strong></td>
                <td align="left">
                    <strong>American Express</strong></td>
            </tr>
            <tr>
                <td align="left" style="width: 50%">
                    <img src="Images/cvv-visa.gif" /></td>
                <td align="left">
                    <img src="Images/cvv-amex.gif" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 50%">
                    <span style="font-size: 10pt">A 3-digit number in reverse italics<br />
                        on the <b>back</b> of your credit card</span></td>
                <td align="left">
                    <span style="font-size: 10pt">A 4-digit number on the <b>front</b>, just<br />
                        above your credit card number</span></td>
            </tr>
            <tr>
                <td align="left" style="width: 50%; height: 30px">
                    &nbsp;</td>
                <td align="left" style="height: 30px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="Button1" runat="server" CssClass="ButtonClass" OnClientClick="window.close()"
                        Text="Close this window" Width="144px" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
