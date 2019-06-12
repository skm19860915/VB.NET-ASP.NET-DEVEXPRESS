<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ShowPhotographer" title="Untitled Page" Codebehind="ShowPhotographer.aspx.vb" %>
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
<asp:FormView ID="PhotographersFormView" runat="server" DataKeyNames="id" DataSourceID="OurPhotographersDataSource" Width="100%">
        <EditItemTemplate />
        <InsertItemTemplate />
        <ItemTemplate>
            <asp:MultiView ID="ShowPhotographerMultiView" runat="server" ActiveViewIndex='<%# Eval("columnsdisplay") %>' EnableTheming="False">
                <asp:View ID="HorzView" runat="server">
                         <table style="width: 570px; text-align:justify" align="center" cellpadding="4" class="showbooktable">
                        <tr>
                            <td style="vertical-align: top" class="showbookpricetext">
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("picturepathlarge","~{0}") %>' /></td></tr>
                                <tr>
                            <td valign="bottom" style="text-align:left;vertical-align:top;" class="showbookdesc">
                                <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("namepicturepath","~{0}") %>' /><br />
                                <br />
                                <!--
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("dateplace") %>'></asp:Label><br />
                                <br />
                                Reference#
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("reference") %>'></asp:Label><br />
                                <br />
                                -->
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("listinfo") %>'></asp:Label><br />
                                <br />
                                <a href="OurLeadingPhotographers.aspx">Back to Our Leading Photographers</a></td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                    </table><br />
                </asp:View>
                <asp:View ID="VertView" runat="server">
                    <table style="width: 100%; text-align:justify" align="center" cellpadding="4" class="showbooktable">
                        <tr>
                            <td style="vertical-align: top; width: 1px;" class="showbookpricetext">
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("picturepathlarge","~{0}") %>' /></td>
                            <td valign="bottom" style="text-align:right;vertical-align:bottom;width: 600px;" class="showbookdesc">
                                <span style="font-weight: bold;"><asp:Label ID="Label7" runat="server" Text='<%# Eval("photographername") %>'></asp:Label></span>
                                <br />
                                <!--
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("dateplace") %>'></asp:Label>
                                <br />
                                Reference#
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("reference") %>'></asp:Label>
                                -->
                                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("listinfo") %>'></asp:Label><br />
                                <br />
                                <asp:HyperLink ID="BackHyperLink" runat="server" NavigateUrl="~/OurLeadingPhotographers.aspx">Back to Our Leading Photographers</asp:HyperLink>
                             </td>
                        </tr>
                    </table><br />
                </asp:View>
            </asp:MultiView>
        </ItemTemplate>
    </asp:FormView>
    
    <asp:ObjectDataSource ID="OurPhotographersDataSource" runat="server" 
    SelectMethod="GetDataById" 
    TypeName="Klemcoll.DAL.KlemCollDataSetTableAdapters.OurPhotographersTableAdapter" 
    OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:RouteParameter Name="id" RouteKey="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

