<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.Login" Codebehind="Login.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Content Manager</title>
</head>
<body>
    <form id="secureorder" runat="server">
        <div>
            <table align="center" border="0" cellpadding="3" cellspacing="0" style="border-right: #990000 1px solid;
                border-top: #990000 1px solid; border-left: #990000 1px solid; border-bottom: #990000 1px solid;
                background-color: #eee" width="400">
                <tr>
                    <td colspan="2" style="font-weight: bold; color: white; height: 19px; background-color: #990000">
                        Login</td>
                </tr>
                <tr>
                    <td align="right" width="80">
                        Username :
                    </td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" Width="150"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                        Password :
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150"></asp:TextBox></td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkPersistLogin" runat="server" />Remember my credentials<br />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="cmdLogin" runat="server" OnClick="ProcessLogin" Text="Login" /></td>
                </tr>
            </table>
            <div id="ErrorMessage" runat="server" />
        </div>
    </form>
</body>
</html>
