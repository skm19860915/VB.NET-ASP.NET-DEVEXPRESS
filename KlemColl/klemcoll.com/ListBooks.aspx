<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ListBooks" title="Untitled Page" Codebehind="ListBooks.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
            <% Response.WriteFile("~/ContentPages/ListBooks.htm")%>
            <asp:DataList ID="DataList1" runat="server" DataKeyField="BooksID" DataSourceID="BooksObjectDataSource"
                RepeatColumns="4" RepeatDirection="Horizontal" HorizontalAlign="Center">
                <ItemTemplate>
                    <table cellpadding="4" cellspacing="2">
                        <tr>
                            <td style="text-align: center; vertical-align: bottom; height: 92px;">
                                <asp:ImageButton ID="Image10" runat="server" ImageUrl='<%# "~/Images/Books/Small/" & Eval("FileName").ToString %>' 
                                PostBackUrl='<%# GetRouteUrl("Publications", New With {.id = Eval("BooksID")}) %>' CssClass="listbooksimage" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: top;" class="listbooksname">
                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("Name") %>' Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EditItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:DataList></td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="BooksObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" 
        TypeName="Klemcoll.DAL.KlemCollDataSetTableAdapters.BooksTableAdapter">
    </asp:ObjectDataSource>
</asp:Content>

