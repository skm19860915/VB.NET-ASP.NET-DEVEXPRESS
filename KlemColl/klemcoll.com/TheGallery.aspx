<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false"
    Inherits="Klemcoll.Website.TheGallery" Title="The Gallery" Codebehind="TheGallery.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%; height: 100%" class="basefontbold">
        <tr>
            <td class="HeaderTextCell">
                <% Response.WriteFile("ContentPages/TheGallery.htm")%>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; height: 100%" class="galleryoptions" cellpadding="4" cellspacing="0">
                    <tr class="greybackground">
                        <td class="GalleryLabel">
                            Photographer</td>
                        <td>
                            <asp:DropDownList ID="PhotographerDropDownList1" runat="server" Width="200px" DataSourceID="PhotographersSqlDataSource"
                                DataTextField="Photographer" DataValueField="PhotographerLastName">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="PhotographerDropDownList2" runat="server" Width="192px" DataSourceID="PhotographersSqlDataSource"
                                DataTextField="Photographer" DataValueField="PhotographerLastName">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="GalleryLabel">
                            Photograph Type</td>
                        <td>
                            <asp:DropDownList ID="PhotoTypeDropDownList" runat="server" Width="200px" DataSourceID="PhotoTypeSqlDataSource"
                                DataTextField="PhotoType" DataValueField="PhotoType">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr class="greybackground">
                        <td class="GalleryLabel">
                            Type of Race</td>
                        <td>
                            <asp:DropDownList ID="RaceTypeDropDownList" runat="server" Width="200px" DataSourceID="RaceTypeSqlDataSource"
                                DataTextField="RaceType" DataValueField="RaceType">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="GalleryLabel">
                            Make of Car</td>
                        <td>
                            <asp:DropDownList ID="MakeOfCarDropDownList1" runat="server" Width="200px" DataSourceID="MakeOfCarSqlDataSource"
                                DataTextField="MakeOfCar" DataValueField="MakeOfCar">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="MakeOfCarDropDownList2" runat="server" Width="192px" DataSourceID="MakeOfCarSqlDataSource"
                                DataTextField="MakeOfCar" DataValueField="MakeOfCar">
                            </asp:DropDownList></td>
                    </tr>
                    <tr class="greybackground">
                        <td class="GalleryLabel">
                            Drivers</td>
                        <td>
                            <asp:DropDownList ID="DriversDropDownList1" runat="server" Width="200px" DataSourceID="DriverSqlDataSource"
                                DataTextField="Driver" DataValueField="DriverQry">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="DriversDropDownList2" runat="server" Width="192px" DataSourceID="DriverSqlDataSource"
                                DataTextField="Driver" DataValueField="DriverQry">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="GalleryLabel">
                            Year</td>
                        <td>
                            <asp:DropDownList ID="YearDropDownList" runat="server" Width="200px">
                                <asp:ListItem Selected="True" Value="all">&lt;&lt;&lt; Select All &gt;&gt;&gt;</asp:ListItem>
                                <asp:ListItem Value="R1">1900 - 1939</asp:ListItem>
                                <asp:ListItem Value="R2">1940 - 1949</asp:ListItem>
                                <asp:ListItem Value="R3">1950 - 1959</asp:ListItem>
                                <asp:ListItem Value="R4">1960 - 1969</asp:ListItem>
                                <asp:ListItem Value="R5">1970 - 1979</asp:ListItem>
                                <asp:ListItem Value="R6">1980 - Later</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; padding-top: 10px" align="center">
                <asp:Button ID="SearchButton" runat="server" Text="Search" Width="80px" PostBackUrl="~/ListImages.aspx"
                    CssClass="ButtonClass" /></td>
        </tr>
    </table>
    <asp:SqlDataSource ID="PhotographersSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
        SelectCommand="SELECT DISTINCT 
	RTRIM(LTRIM(PhotographerFirstName)) + ' ' + RTRIM(LTRIM(PhotographerLastName)) AS Photographer, 
	RTRIM(LTRIM(PhotographerLastName)) AS PhotographerLastName,
	PhotographerSort = Case RTRIM(LTRIM(PhotographerLastName))
		When 'Klemantaski' then '____'
		Else RTRIM(LTRIM(PhotographerLastName))
	End
FROM Images 
ORDER BY PhotographerSort">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="PhotoTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
        SelectCommand="SELECT DISTINCT RTRIM(LTRIM(PhotoType)) AS PhotoType FROM [Images] WHERE ([PhotoType] IS NOT NULL)">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="RaceTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
        SelectCommand="SELECT DISTINCT RTRIM(LTRIM(RaceType)) AS RaceType FROM [Images] WHERE ([RaceType] IS NOT NULL) ORDER BY RaceType">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="MakeOfCarSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
        SelectCommand="SELECT DISTINCT RTRIM(LTRIM(MakeOfCar)) AS MakeOfCar FROM [Images] WHERE ([MakeOfCar] IS NOT NULL) ORDER BY MakeOfCar">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DriverSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Klemcoll.DAL.My.MySettings.KlemCollConnectionString %>"
        SelectCommand="SELECT DISTINCT LTRIM(RTRIM(DriverFirstName)) + ' ' + LTRIM(RTRIM(DriverLastName)) AS Driver, LTRIM(RTRIM(DriverFirstName)) + '-1' + LTRIM(RTRIM(DriverLastName)) AS DriverQry, LTRIM(RTRIM(DriverFirstName)) AS Expr1, LTRIM(RTRIM(DriverLastName)) AS Expr2 FROM Images WHERE (LTRIM(RTRIM(DriverLastName)) <> '') ORDER BY Expr2">
    </asp:SqlDataSource>
</asp:Content>
