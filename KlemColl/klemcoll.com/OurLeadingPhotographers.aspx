<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.OurLeadingPhotographers" title="Our Leading Photographers" Codebehind="OurLeadingPhotographers.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left" class="basefontbold">Click on the image for a more detailed description and a larger view.<br />
            <asp:DataList ID="DataList1" runat="server" DataKeyField="ID" DataSourceID="OurPhotographersDataSource"
                RepeatColumns="4" RepeatDirection="Horizontal">
                <ItemTemplate>
                <div style="height: 150px;">
                    <table cellpadding="4" cellspacing="2" style="width: 140px;">
                    <tr>
                    <td style="text-align: center; height: 92px; vertical-align: bottom;">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl='<%# Eval("PicturePathSmall","~{0}") %>' 
                            PostBackUrl='<%# GetRouteUrl("Photographers", New With {.id = Eval("id")}) %>' 
                            CssClass="listbooksimage" AlternateText='<%# Eval("alttext") %>' />
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align: center;">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("photographername") %>' Font-Bold="True" Font-Size="14pt"></asp:Label>
                    </td>
                    </tr>
                    </table>
                    </div>
                </ItemTemplate>
                <EditItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:DataList></td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="OurPhotographersDataSource" runat="server" 
        SelectMethod="GetData" 
        TypeName="Klemcoll.DAL.KlemCollDataSetTableAdapters.OurPhotographersTableAdapter" 
        OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
</asp:Content>

