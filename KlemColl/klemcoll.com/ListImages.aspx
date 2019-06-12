<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ListImages" title="The Gallery" MaintainScrollPositionOnPostback="true" Codebehind="ListImages.aspx.vb" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<%@ PreviousPageType VirtualPath="~/TheGallery.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 100%"->
        <tr>
            <td class="HeaderTextCell">
                <asp:Label ID="HeaderLabel" runat="server"></asp:Label>
                <br />
                </td>
        </tr>
        <tr>
            <td class="HeaderTextCell" style="text-align: center">
                <asp:Button ID="ClearFavoritesButton" runat="server" Text="Clear All Favorites" Visible="False" OnClientClick="return confirm('Are you sure you want to clear all of your favorite images?');" Width="128px" /></td>
        </tr>
        <tr>
            <td class="FootTextCellBlack" align="center">
                <asp:DataList ID="ImagesDataList" runat="server" DataKeyField="Refid" DataSourceID="ImagesObjectDataSource"
                    RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# "~/images/small/" & Eval("Refid").tostring & ".jpg" %>' PostBackUrl='<%# GetRouteUrl("Image", New With {.Refid = Eval("Refid")}) & "?page=" & request("page") %>' />
                    </ItemTemplate>
                    <ItemStyle CssClass="ListImagesItemStyle" VerticalAlign="Bottom" Width="25%" />
                </asp:DataList><asp:ObjectDataSource ID="ImagesObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="Klemcoll.DAL.KlemCollDataSetTableAdapters.ImagesTableAdapter">
                </asp:ObjectDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <cc1:CollectionPager ID="ImagesCollectionPager" runat="server" BackNextButtonStyle=""
                    BackNextDisplay="HyperLinks" BackNextLinkSeparator="--" BackNextLocation="Split"
                    BackNextStyle="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" BackText="« Back" NextText="Next »"
                    PageNumbersDisplay="Numbers" PageSize="16" PagingMode="QueryString" MaxPages="200" ShowFirstLast="True" SectionPadding="10">
                </cc1:CollectionPager>
            </td>
        </tr>
    </table>
</asp:Content>

