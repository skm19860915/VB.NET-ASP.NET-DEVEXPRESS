<%@ Control Language="VB" AutoEventWireup="true" Inherits="Klemcoll.Website.ShowOrder" Codebehind="ShowOrder.ascx.vb" %>
<table id="ConfirmOrderTable" runat="server" class="showordertable">
    <tr>
        <td colspan="2" style="text-align: left">
        </td>
    </tr>
    <tr>
        <td rowspan="3" style="text-align:left;">
            The Klemantaski Collection<br />
            PO Box 8204<br />
            Stamford, CT. 06905-8204 
            <br />
            U.S.A.<br />
            Telephone: (203) 461-9804<br />
            Fax: (203) 968-2970<br />
            E-mail: <asp:HyperLink ID="EmailHyperLink" runat="server" NavigateUrl="mailto:info@klemcoll.com">info@klemcoll.com</asp:HyperLink><br />
            Website: <a href="http://www.klemcoll.com">www.klemcoll.com</a></td>
        <td style="text-align:left;">
            <asp:Label ID="CustomerNameLabel" runat="server"></asp:Label>
            <asp:Label ID="CompanyLabel" runat="server"></asp:Label><br />
            <asp:Label ID="CustomerEmailLabel" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:left;">
            <strong>Billing Address:</strong><br />
            <asp:Label ID="MailingAddress1Label" runat="server"></asp:Label><br />
            <asp:Label ID="MailingCityStateCountryLabel" runat="server"></asp:Label><br />
            <asp:Label ID="MailingPhoneLabel" runat="server"></asp:Label>
            <asp:Label ID="MailingFaxLabel" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td rowspan="3" style="text-align:left;">
            <strong>Shipping Address:</strong><br />
            <asp:Label ID="ShippingAddress1Label" runat="server"></asp:Label><br />
            <asp:Label ID="ShippingAddress2Label" runat="server"></asp:Label><br />
            <asp:Label ID="ShippingCityStateCountryLabel" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td rowspan="2" style="text-align:left;">
            <asp:Label ID="PaymentMethodLabel" runat="server"></asp:Label><br />
            <br />
            <asp:Label ID="ShipMethodLabel" runat="server"></asp:Label></td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td colspan="2" style="padding-top: 15px">
            <asp:Repeater ID="ImagesRepeater" runat="server">
                <HeaderTemplate>
                    <table style="width: 600px" class="showorderlist">
                </HeaderTemplate>
                <ItemTemplate>
                    <!--This row is for images-->
                    <!--<tr runat="server" id="ImageRow">
                        <td>-->
                    <tr>
                        <td style="width: 198px" class="showordersubtitle">
                            Image Reference Number</td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "Refid")%></td>
                        <td style="width: 60px" class="showordersubtitle">
                            Quantity</td>
                        <td style="width: 47px" class="showordersubtitle">
                            Each</td>
                        <td style="width: 47px" class="showordersubtitle">
                            Price</td>
                    </tr>
                    <tr>
                        <td style="background-color: #EEEEEE;width: 198px" class="showordersubtitle">
                            Paper Size</td>
                        <td style="background-color: #EEEEEE;">
                            <%#DataBinder.Eval(Container.DataItem, "Papersize")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 60px">
                            <%#DataBinder.Eval(Container.DataItem, "Quantity")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                            <%#DataBinder.Eval(Container.DataItem, "Price")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                            <%#DataBinder.Eval(Container.DataItem, "SubTotal")%>
                        </td>
                    </tr>
                    <!--<tr>
                        <td style="width: 198px" class="showordersubtitle">
                            B&amp;W or Color</td>
                        <td>
                            <#DataBinder.Eval(Container.DataItem, "Color")%>
                        </td>
                        <td style="width: 54px">
                        </td>
                        <td style="width: 47px">
                        </td>
                        <td style="width: 47px">
                        </td>
                    </tr>-->
                    <tr>
                        <td style="background-color: #EEEEEE;width: 198px" class="showordersubtitle">
                            Paper Type</td>
                        <td style="background-color: #EEEEEE;">
                            <%# DataBinder.Eval(Container.DataItem, "Papertype")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 54px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #EEEEEE;width: 198px" class="showordersubtitle">
                            Border</td>
                        <td style="background-color: #EEEEEE;">
                            <%#DataBinder.Eval(Container.DataItem, "Border")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 54px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #EEEEEE;width: 198px" class="showordersubtitle">
                            Toning</td>
                        <td style="background-color: #EEEEEE;">
                            <%#DataBinder.Eval(Container.DataItem, "Toning")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 54px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #EEEEEE;width: 198px; vertical-align: top;" class="showordersubtitle">
                            Special Instructions / Memo</td>
                        <td style="background-color: #EEEEEE;">
                            
                        </td>
                        <td style="background-color: #EEEEEE;width: 54px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                        <td style="background-color: #EEEEEE;width: 47px">
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="vertical-align: top;" colspan="5">
                            <%#DataBinder.Eval(Container.DataItem, "Memo")%>
                            </td>
                        
                    </tr>
                    <tr>
                    <td colspan="5">
                    <hr style="color: #cccccc;" />
                    </td>
                    </tr>
                    <!--</td>
                    </tr>-->
                    <!--End image row-->
                    <!--This row is for books-->
                    <!--<tr runat="server" id="BookRow">
                        <td>-->
                    <!--</td>
                    </tr>-->
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Repeater ID="BooksRepeater" runat="server">
                <HeaderTemplate>
                    <!--<tr>
                                        <td colspan="5">
                                            Books</td>
                                    </tr>-->
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="background-color: #EEEEEE;width: 198px" class="showordersubtitle">
                            Title</td>
                        <td style="background-color: #EEEEEE;">
                            <%#DataBinder.Eval(Container.DataItem, "Name")%>
                        </td>
                        <td style="background-color: #EEEEEE;width: 54px" class="showordersubtitle">
                            Quantity</td>
                        <td style="background-color: #EEEEEE;width: 47px" class="showordersubtitle">
                            Each</td>
                        <td style="background-color: #EEEEEE;width: 47px" class="showordersubtitle">
                            Price</td>
                    </tr>
                    <tr>
                        <td style="width: 198px" class="showordersubtitle">
                            Author</td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "Author")%>
                        </td>
                        <td style="width: 54px">
                            <%#DataBinder.Eval(Container.DataItem, "Quantity")%>
                        </td>
                        <td style="width: 47px">
                            <%#DataBinder.Eval(Container.DataItem, "Price")%>
                        </td>
                        <td style="width: 47px">
                            <%#DataBinder.Eval(Container.DataItem, "SubTotal")%>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="5">
                    <hr style="color: #cccccc;" />
                    </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="4" style="text-align: right" class="showordersubtitle">
                            Subtotal =</td>
                        <td style="width: 47px; text-align: left">
                            <asp:Label ID="SubtotalLabel" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right" class="showordersubtitle">
                            Shipping =</td>
                        <td style="width: 47px; text-align: left">
                            <asp:Label ID="ShippingLabel" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right" class="showordersubtitle">
                            Sales Tax =</td>
                        <td style="width: 47px; text-align: left">
                            <asp:Label ID="SalesTaxLabel" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <hr style="color: #cccccc;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="font-size: 14px; font-weight: bold; text-align: right; padding: 4px;" class="showordersubtitle">
                            Total =</td>
                        <td style="font-size: 14px; width: 47px; text-align: left; font-weight: bold; padding: 4px;">
                            <asp:Label ID="TotalLabel" runat="server" Text=""></asp:Label></td>
                    </tr>
                    </table></FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
