<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableViewState="false" EnableViewStateMac="false" Debug="false" EnableTheming="false"%>
<%@ Import Namespace="InterAKT.WebControls" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        KtmlServiceProvider sp = new KtmlServiceProvider(this.Context);
        Response.Write(sp.Execute());
        Response.End();
    }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>KTML4 Service Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div></div>
    </form>
</body>
</html>
