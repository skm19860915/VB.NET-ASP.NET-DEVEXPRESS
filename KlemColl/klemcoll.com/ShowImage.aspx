<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ShowImage" title="View Image Details" Codebehind="ShowImage.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
document.onmousedown=disableclick
status="Right Click Disabled"
function disableclick(e) {
if( event.button==2 )
alert("Right Click has been disabled");
return false;
}
</script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%" class="ShowImageTable">
                <tr>
                <td class="ShowImageTitle"><asp:Label ID="NameLabel" runat="server"></asp:Label></td>
                </tr>
        <tr>
            <td colspan="2" style="height: 100%; width: 100%; text-align: left;">
            <table border="0" cellpadding="0" cellspacing="2" style="width: 100%; height: 100%">
                            <tr>
                                <td rowspan="5" style="font-size: 13px; text-align:center; padding-right: 5px; padding-left: 5px; vertical-align: top; padding-top: 5px;">
                                    <div id="Image1" runat="server">
                                    </div>
                                        <asp:Label ID="Signed" runat="server">
                                            <span class="Apple-tab-span" style="white-space: pre"></span><font class="Apple-style-span"
                                                color="#000000">Prints signed by the photographer are available.</font>
                                            <asp:HyperLink ID="ContactUsHyperLink" runat="server" NavigateUrl="contactus.aspx">Contact Us</asp:HyperLink>
                                        </asp:Label>
                                        <%--<asp:MultiView ID="Signed" runat="server"><asp:View ID="SignedOriginal" runat="server">
                                            </asp:View></asp:MultiView>--%>
                                    <asp:HiddenField ID="SignedAvailableHF" runat="server" />
                                </td>
                                <td style="text-align: left; width: 500px;">
                                    </td>
                            </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-style: italic;" class="basefontbold">Reference #: </span><asp:Label ID="RefidLabel" runat="server" CssClass="basefontbold"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            </td>
                    </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: bottom;">
                                    <br />
                                    <table style="width: 150px">
                                        <tr>
                                            <td style="width: 180px; text-align: center;"><strong><span style="text-decoration: underline; font-size: 13px"><asp:Label ID="OtherVariationsLabel" runat="server" Text="Other Variations"></asp:Label></span></strong></td>
                                        </tr>
                                        <tr>
                                            <td rowspan="4" style="width: 180px">
                                                <asp:Button ID="ViewBWButton" runat="server" Text="View B&W Full Frame" Width="180px" CssClass="ButtonClass" /><asp:Button ID="ViewBWCroppedButton" runat="server" Text="View B&W Cropped" CssClass="ButtonClass" Width="180px" /><asp:Button ID="ViewColorButton" runat="server" Text="View Color Full Frame" CssClass="ButtonClass" Width="180px" /><asp:Button ID="ViewColorCroppedButton" runat="server" Text="View Color Cropped" CssClass="ButtonClass" Width="180px" /></td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px"><asp:MultiView ID="FavoritesMultiView" runat="server">
                                                <asp:View ID="AddFavoriteView" runat="server">
                                                    <asp:Button ID="AddToFavoritesButton" runat="server" Text="Add to Favorites" CssClass="ButtonClass" Width="180px" />&nbsp;
                                                </asp:View>
                                                <asp:View ID="RemoveFavoriteView" runat="server">
                                                    <asp:Button ID="RemoveFromFavoritesButton" runat="server" Text="Remove From Favorites" CssClass="ButtonClass" Width="180px" />&nbsp;
                                                </asp:View>
                                            </asp:MultiView></td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 180px"><br /><asp:Button ID="AddToCartButton" runat="server" Text="Add to Shopping Cart" CssClass="ButtonClass" Width="180px" PostBackUrl="~/AddEditImage.aspx" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px"><asp:Button ID="BackButton" runat="server" CssClass="ButtonClass" Text="Back to Search" Width="180px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                
                
            </td>
        </tr>
        <tr>
            
            
            <td colspan="2" style="text-align: justify; height: 24px;padding: 10px;"><asp:Label CssClass="basefontbold" ID="DescLabel" runat="server"></asp:Label></td>
        </tr>
    </table>
    <asp:HiddenField ID="RefidHiddenField" runat="server" />
    <asp:Label ID="CookieStatusLabel" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>

