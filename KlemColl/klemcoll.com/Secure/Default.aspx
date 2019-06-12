<%@ Page Language="VB" AutoEventWireup="false"
    Inherits="Klemcoll.Website.ContentManager" ValidateRequest="false" Codebehind="Default.aspx.vb" %>

<%@ Register Assembly="InterAKT.KTML4" Namespace="InterAKT.WebControls" TagPrefix="iakt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="984">
                <tr>
                    <td>
                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                            <tr bgcolor="#990000">
                                <td style="padding-left: 10px; padding-right: 10px; border-right: solid thin white;">
                                    <asp:LinkButton ID="lnkSave" runat="server" ForeColor="White" Font-Bold="True">Save</asp:LinkButton>
                                </td>
                                <td width="100%" style="text-align: center; font-weight: bold; color: white;">
                                    Expert Data Solutions Content Manager
                                </td>
                                <td style="padding-left: 10px; padding-right: 10px; border-left: solid thin white;">
                                    <asp:LinkButton ID="lnkLogOut" runat="server" ForeColor="white" Font-Bold="True">LogOut</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="100%" valign="top" style="border-left: solid thin DarkGray;border-bottom: solid thin DarkGray;border-top: solid thin DarkGray;">
                                    <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" ImageSet="XPFileExplorer"
                                        NodeIndent="10" ExpandDepth="1">
                                        <ParentNodeStyle Font-Bold="False" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                            VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px" />
                                        <DataBindings>
                                            <asp:TreeNodeBinding DataMember="SiteMapNode" TextField="Title" />
                                            <asp:TreeNodeBinding DataMember="SpecialPublication" TextField="#InnerText"  />
                                            <asp:TreeNodeBinding DataMember="Photographer" TextField="#InnerText"  />
                                        </DataBindings>
                                    </asp:TreeView>
                                    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Secure/ContentManagerSiteMap.xml">
                                    </asp:XmlDataSource>
                                    &nbsp; &nbsp;
                                    &nbsp; &nbsp;
                                </td>
                                <td>
                                    <iakt:Ktml ID="KtmlContent" runat="server" Width="850px" TextMode="MultiLine" TemplatesFolder="~/ContentPages/templates"
                                        MediaFolder="~/ContentPages/Images" Height="700px" DocumentsFolder="~/ContentPages"
                                        AllowedDocumentTypes="*.doc,*.txt,*.pdf,*.csv,*.xls,*.rtf,*.sxw,*.odt,*.htm">
                                    </iakt:Ktml>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
