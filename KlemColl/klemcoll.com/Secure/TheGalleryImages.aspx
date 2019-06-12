<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.Secure_TheGalleryImages" Codebehind="TheGalleryImages.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
            <tr>
                <td align="right" style="font-weight: bold; width: 35%;">
                    Refid:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtRefid" runat="server" Width="75px"></asp:TextBox></td>
                <td align="right" style="font-weight: bold">
                    PhotoType:</td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddPhotoType" runat="server" DataSourceID="SqlDataSource2" DataTextField="PhotoType" DataValueField="PhotoType">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold; width: 35%;">
                    Name:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold; width: 35%;">
                    Photographer Last Name:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPhotographer" runat="server" Width="100px"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold; width: 35%;">
                    Driver Last Name:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtDriver" runat="server" Width="100px"></asp:TextBox></td>
                <td colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="Search" />
                    <asp:Button ID="Button2" runat="server" Text="Reset" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="0px" CellPadding="5" DataKeyNames="ImagesID"
            DataSourceID="SqlDataSource1" Width="100%" PageSize="20">
            <Columns>
                <asp:BoundField DataField="Refid" HeaderText="Refid" SortExpression="Refid" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Sequence" HeaderText="Sequence" SortExpression="Sequence" />
                <asp:BoundField DataField="PhotographerLastName" HeaderText="PhotographerLastName"
                    SortExpression="PhotographerLastName" />
                <asp:BoundField DataField="PhotoType" HeaderText="PhotoType" SortExpression="PhotoType" />
                <asp:HyperLinkField DataNavigateUrlFields="ImagesId" DataNavigateUrlFormatString="Default.aspx?GalleryImage={0}"
                    Text="Edit" />
            </Columns>
            <HeaderStyle BackColor="#990000" ForeColor="White" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
            SelectCommand="SELECT * FROM [Images]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
            SelectCommand="SELECT DISTINCT [PhotoType] FROM [Images]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
