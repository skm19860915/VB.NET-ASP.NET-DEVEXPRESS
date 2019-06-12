<%@ Page Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" Inherits="Klemcoll.Website.ContactUs" title="Contact Us" Codebehind="ContactUs.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView ID="ContactUsMultiView" runat="server" ActiveViewIndex="0" EnableTheming="False">
        <asp:View ID="ContactUsView" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 102px; text-align: right">
                    </td>
                    <td style="width: 473px" class="MiddleTextCell">
                        <% Response.WriteFile("~/ContentPages/ContactUs.htm")%>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <% Response.WriteFile("~/ContentPages/ContactUsMiddle.htm")%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 473px; padding-top: 15px; text-align: right" class="MiddleTextCell">
                        Name</td>
                    <td style="width: 473px; padding-top: 15px; text-align: left" class="MiddleTextCell">
                        <asp:TextBox ID="NameTextBox" runat="server" Columns="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                            ErrorMessage="Please enter your name" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td style="width: 473px; padding-top: 15px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 102px; text-align: right" class="MiddleTextCell">
                        Email</td>
                    <td style="width: 473px; text-align: left" class="MiddleTextCell">
                        <asp:TextBox ID="EmailTextBox" runat="server" Columns="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Please enter an email address"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailTextBox"
                            ErrorMessage="Please enter a valid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 102px; text-align: right" class="MiddleTextCell">
                        Message</td>
                    <td style="width: 473px; text-align: left; vertical-align:top;" class="MiddleTextCell">
                        <asp:TextBox ID="MessageTextBox" runat="server" Height="90px" TextMode="MultiLine"
                            Width="472px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 102px; text-align: right">
                    </td>
                    <td style="width: 473px; text-align: left">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="MessageTextBox"
                            ErrorMessage="Please enter a message to send" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 102px; text-align: right">
                    </td>
                    <td style="width: 473px; text-align: left">
                        <asp:Button ID="SubmitButton" runat="server" Text="Submit" Width="72px" /></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 102px; text-align: right">
                    </td>
                    <td style="width: 473px; text-align: left">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ThankYouView" runat="server">
            &nbsp;<table style="width: 100%">
                <tr>
                    <td style="font-size: large; padding-top: 15px">
                        <asp:Label ID="ThankYouLabel" runat="server" Text="Thank you! Your email has been sent."></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-size: medium; padding-top: 10px">
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Default.aspx">Click Here to continue to the home page</asp:LinkButton></td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

