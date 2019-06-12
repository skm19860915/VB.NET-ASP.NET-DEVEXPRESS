<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ShowBook" title="Untitled Page" Codebehind="ShowBook.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:FormView ID="BookFormView" runat="server" DataKeyNames="BooksID" DataSourceID="BookObjectDataSource" Width="100%">
       <ItemTemplate>
    <asp:MultiView ID="ShowBookMultiView" runat="server" ActiveViewIndex='<%# Eval("DisplayVerticle") %>' EnableTheming="False">
        <asp:View ID="HorzView" runat="server">
            <table style="width: 100%; text-align:justify" align="center" cellpadding="4" class="basefont">
            <tr><td colspan="2" class="showbookName">
                        <asp:Label ID="HorzNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: top;width: 1px;"><asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Images/Books/Medium/" & Eval("FileName").ToString %>' /></td>
                    <td valign="bottom" style="width:600px;text-align:right;">
                        $<asp:Label ID="HorzPriceLabel" runat="server" Text='<%# Eval("Price") %>'></asp:Label><br />
                        <asp:Label ID="HorzPriceDescLabel" runat="server" Text='<%# Eval("PriceDesc") %>'></asp:Label><br />
                        <strong>Quantity: </strong>
                        <asp:TextBox ID="HorzQuantityTextBox" runat="server" Width="32px" OnPreRender="PopulateQuantity"></asp:TextBox><br />
                        <asp:Button ID="HorzAddCartButton" runat="server" Text="Add to Cart" cssclass="ButtonClass" OnClick="AddCartButton_Click" />
                    </td>
                    </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="HorzDescLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        </td>
                </tr>
            </table><br />
        </asp:View>
        <asp:View ID="VertView" runat="server">
            <table style="width: 570px; text-align:justify" align="center" cellpadding="4" class="basefont">
            <tr><td colspan="2" class="showbookName">
                        <asp:Label ID="VertNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                    <asp:Image ID="VertImage" runat="server" ImageUrl='<%# "~/Images/Books/Medium/" & Eval("FileName").ToString %>' />
                    <br />
                        $<asp:Label ID="VertPriceLabel" runat="server" Text='<%# Eval("Price") %>'></asp:Label><br />
                        <asp:Label ID="VertPriceDescLabel" runat="server" Text='<%# Eval("PriceDesc") %>'></asp:Label><br />
                        <strong>Quantity: </strong>
                        <asp:TextBox ID="VertQuantityTextBox" runat="server" Width="32px" OnPreRender="PopulateQuantity"></asp:TextBox><br />
                        <asp:Button ID="VertAddCartButton" runat="server" cssclass="ButtonClass" Text="Add to Cart" OnClick="AddCartButton_Click" /></td>
                    <td valign="bottom" style="text-align:left;vertical-align:top;">
                        <asp:Label ID="VertDescLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
            </table><br />
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField id="AuthorHiddenField" runat="server" Value='<%# Eval("Author") %>'>
        </asp:HiddenField><asp:HiddenField id="WeightHiddenField" runat="server" Value='<%# Eval("Weight") %>'></asp:HiddenField>
            <asp:HiddenField ID="BookIDHiddenField" runat="server" Value='<%# Eval("BooksID") %>' />
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="BookObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBook" TypeName="Klemcoll.DAL.KlemCollDataSetTableAdapters.BooksTableAdapter">
        <SelectParameters>
            <asp:RouteParameter Name="BooksID" RouteKey="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

