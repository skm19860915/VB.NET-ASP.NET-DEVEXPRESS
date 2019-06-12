<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ViewCart" title="Untitled Page" Explicit="true" Strict="true" Codebehind="ViewCart.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="font-size: larger; text-align: center; width: 539px; padding-top: 20px;" class="HeaderTextCell">
                <asp:Label ID="NoItemsLabel" runat="server" Text="You do not have any items in your Shopping Cart"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 620px">
                <asp:DataList ID="ImagesDataList" runat="server" BackColor="#EEEEEE" CssClass="viewcartbookstable">
                    <ItemTemplate>
                        <table style="width: 100%;" class="viewcartitemtable">
                            <tr>
                                <td style="width: 226px;" class="viewcartsubtitle">
                                    Image Reference Number</td>
                                <td style="width: 300px;">
                                    <asp:Label ID="RefidLabel" runat="server" Text='<%# Eval("Refid") %>'></asp:Label></td>
                                <td style="width: 60px" class="viewcartsubtitle">
                                    Quantity</td>
                                <td style="width: 72px" class="viewcartsubtitle">
                                    Price</td>
                            </tr>
                            <tr>
                                <td class="viewcartsubtitle" style="width: 226px">
                                    Paper Size</td>
                                <td>
                                    <asp:Label ID="PaperSizeLabel" runat="server" Text='<%# Eval("PaperSize") %>'></asp:Label></td>
                                <td style="text-align: center">
                                    <asp:Label ID="QuantityLabel" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></td>
                                <td style="width: 72px">
                                    <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("SubTotal") %>' OnPreRender="PriceLabel_PreRender"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="viewcartsubtitle" style="width: 226px">
                                    Paper Type</td>
                                <td>
                                    <asp:Label ID="PaperTypeLabel" runat="server" Text='<%# Eval("PaperType") %>'></asp:Label></td>
                                <td>
                                </td>
                                <td style="width: 72px">
                                </td>
                            </tr>
                            <tr>
                                <td class="viewcartsubtitle" style="width: 226px">
                                    Border</td>
                                <td>
                                    <asp:Label ID="BorderLabel" runat="server" Text='<%# Eval("Border") %>'></asp:Label></td>
                                <td colspan="2" style="text-align: right">
                                    <asp:Button ID="EditImageButton" runat="server" CssClass="ButtonClass" PostBackUrl='<%# "~/AddEditImage.aspx?ImagesID=" & Eval("SessionImagesID").ToString %>'
                                        Text="Edit Item" Width="104px" Visible="True"/></td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; vertical-align: top; width: 226px;">
                                    Toning</td>
                                <td>
                                    <asp:Label ID="ToningLabel" runat="server" Text='<%# Eval("Toning") %>'></asp:Label></td>
                                <td colspan="2" style="text-align: right; vertical-align: top;">
                                    <asp:Button ID="RemoveItemButton" runat="server" CssClass="ButtonClass" Text="Remove Item" Width="104px" CommandArgument='<%# Eval("SessionImagesID") %>' OnCommand="RemoveItemButton_Command" /></td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; vertical-align: top; width: 226px;">
                                    Special Instructions / Memo</td>
                                <td>
                                    <asp:Label ID="MemoLabel" runat="server" Text='<%# Eval("Memo") %>' OnPreRender="MemoLabel_PreRender"></asp:Label></td>
                                <td colspan="2" style="text-align: right; vertical-align: top;"></td>
                            </tr>
                            <tr>
                                <td style="width: 226px">
                                </td><td></td><td></td>
                                <td style="width: 72px">
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <table cellpadding="4" cellspacing="0" class="viewcartTitle" width="100%"><tr><td class="viewcartTitle">
                        Images in your cart</td></tr></table>
                    </HeaderTemplate>
                </asp:DataList></td>
        </tr>
        <tr>
            <td style="width: 620px">
                <asp:DataList ID="BooksDataList" runat="server" CssClass="viewcartbookstable">
                    <ItemTemplate>
                        <table style="height: 100%; width: 100%;" class="viewcartitemtable">
                            <tr>
                                <td style="width: 50px" class="viewcartsubtitle">Title:</td>
                                <td style="width: 450px;" class="viewcartdata"><asp:Label ID="BookNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                <td style="width: 60px" class="viewcartsubtitle">Quantity</td>
                                <td style="width: 60px" class="viewcartsubtitle">Price</td>
                            </tr>
                            <tr>
                                <td class="viewcartsubtitle">Author:</td>
                                <td class="viewcartdata"><asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("Author") %>'></asp:Label></td>
                                <td class="viewcartdata"><asp:Label ID="BookQuantityLabel" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></td>
                                <td class="viewcartdata">$<asp:Label ID="BookPriceLabel" runat="server" Text='<%# Eval("SubTotal") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="1" style="text-align: right"></td>
                                <td colspan="2" style="text-align: right">
                                    <asp:Button ID="EditImageButton" runat="server" CssClass="ButtonClass" PostBackUrl='<%# GetRouteUrl("Publications", New With {.id = Eval("BooksID")}) %>'
                                    Text="Edit Item" Width="104px"/>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="1" style="text-align: right"></td>
                                <td colspan="2" style="text-align: right">
                                    <asp:Button ID="RemoveBookButton" runat="server" CssClass="ButtonClass" Text="Remove Item" Width="104px" CommandArgument='<%# Eval("BooksID") %>' OnCommand="RemoveBookButton_Command" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <table cellpadding="4" cellspacing="0" class="viewcartTitle" width="100%"><tr><td class="viewcartTitle">
                        Books in your Cart</td></tr></table>
                    </HeaderTemplate>
                </asp:DataList></td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr runat="server" id="TotalRow">
                        <td style="font-weight: bold; width: 570px; text-align: right">
                            Subtotal = <asp:Label ID="SubtotalLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 570px;">
                <asp:Button ID="CheckoutButton" runat="server" CssClass="ButtonClass" Text="Check Out" Width="128px" /><br />
                <br />
                <asp:Label ID="Label1" runat="server" CssClass="basefontbold" Text="To select another image from your last search, click on The Gallery and reload your search. To select another book, click on Special Publications."></asp:Label>
                </td>
        </tr>
    </table>
    <asp:HiddenField ID="SessionIDHiddenField" runat="server" />
</asp:Content>

