<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.Secure_ViewOrder" Codebehind="ViewOrder.aspx.vb" %>

<%@ Register Src="../ShowOrder.ascx" TagName="ShowOrder" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ShowOrder ID="ShowOrder1" runat="server" />
        <asp:HiddenField ID="SecureHiddenField" runat="server" />
    </div>
    </form>
</body>
</html>
