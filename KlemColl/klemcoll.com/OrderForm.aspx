<%@ Page Language="VB" MasterPageFile="~/NoLeftNav.master" AutoEventWireup="false" Inherits="Klemcoll.Website.OrderForm" title="Order Form" MaintainScrollPositionOnPostback="true" Codebehind="OrderForm.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fillCountry(value, controlID) {
            if (value == "Not Specified") {
                if (document.getElementById(controlID).value == "US") {
                    document.getElementById(controlID).value = ""
                }
            }
            else {
                document.getElementById(controlID).value = "US"
            }
        }
        
        function Domestic(SelectedValue, CountyControlID) {
            if (SelectedValue != "Not Specified"){
                document.getElementById(CountyControlID).value = ""
                document.getElementById(CountyControlID).disabled = true;
            }
            else {
                document.getElementById(CountyControlID).disabled = false;
            }
        }
    </script>
    <table style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; margin: 5px; padding-top: 5px;" class="OrderFormTable" cellpadding="0">
    <tr><td><table>
        <tr>
            <td colspan="4" class="OrderFormSubTitle">
                Personal Information</td>
        </tr>
        <tr>
            <td colspan="4">
                <span style="text-decoration: underline">All fields marked with an <span style="color: Red;">*</span> are optional</span></td>
        </tr>
        <tr>
            <td class="OrderFormTitle" style="height: 24px; width: 297px;">
                First Name:<asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
            <td class="OrderFormTitle" style="height: 24px;" colspan="2">
                Middle Initial:<asp:TextBox ID="MiddleInitialTextBox" runat="server" Width="40px" CssClass="tbxMargin"></asp:TextBox>
                <span style="color: #ff0000">*</span></td>
            <td class="OrderFormTitle" style="height: 24px;">
                Last Name:<asp:TextBox ID="LastNameTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstNameTextBox"
                    Display="Dynamic" ErrorMessage="First Name is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            <td style="width: 158px">
            </td>
            <td style="width: 114px">
            </td>
            <td style="width: 344px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LastNameTextBox"
                    Display="Dynamic" ErrorMessage="Last Name is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="OrderFormTitle" style="height: 25px;" colspan="2" nowrap="noWrap">
                Organization:<asp:TextBox ID="OrganizationTextBox" runat="server" Columns="35" CssClass="tbxMargin" Width="192px"></asp:TextBox>
                <span style="color: #ff0000">*</span></td>
            <td colspan="2" style="height: 25px" class="OrderFormTitle" nowrap="noWrap">
                Email Address:<asp:TextBox ID="EmailTextBox" runat="server" CssClass="tbxMargin" Columns="40"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" class="OrderFormTitle">
                </td>
            <td class="OrderFormTitle" colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EmailTextBox"
                    ErrorMessage="Email Address is required" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailTextBox"
                        Display="Dynamic" ErrorMessage="Please enter a valid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td colspan="4" class="OrderFormSubTitle">
                Billing Information</td>
        </tr>
        <tr>
            <td class="OrderFormTitle" colspan="4" >
                Payment Type:&nbsp;<asp:DropDownList ID="PaymentTypeDropDownList" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">Credit Card</asp:ListItem>
                    <asp:ListItem>PayPal</asp:ListItem>
                </asp:DropDownList>
                <span style="font-size: 13px">We accept VISA, Mastercard, Discover and American Express credit cards or PayPal.</span></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:MultiView ID="PaymentTypeMultiView" runat="server">
                    <asp:View ID="CreditCardView" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 280px;vertical-align:top;" class="OrderFormTitle">Credit Card Number:<asp:TextBox ID="CCNumberTextBox" runat="server" CssClass="tbxMargin" Columns="16" AutoCompleteType="Disabled"></asp:TextBox><div style="font-size:13px;font-weight:bold;vertical-align:text-top;">(no spaces please)</div></td>
                                <td class="OrderFormTitle" style="width: 260px;vertical-align:top;">
                                    Exp. Date (MM/YY)<asp:TextBox ID="ExpDateTextBox" runat="server" CssClass="tbxMargin" Columns="4" AutoCompleteType="Disabled"></asp:TextBox></td>
                                <td class="OrderFormTitle" style="vertical-align:top;">
                                    CVV Number:<asp:TextBox ID="CVVTextBox" runat="server" MaxLength="4" Columns="3" CssClass="tbxMargin" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Smaller" NavigateUrl="~/CVVInfo.aspx"
                                        Target="_blank">What is this?</asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td style="width: 280px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="CCNumberTextBox"
                                        Display="Dynamic" ErrorMessage="Credit Card Number is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="CCNumberTextBox"
                                        ErrorMessage="Invalid Credit Card Number" SetFocusOnError="True" ValidationExpression="^((4\d{3})|(5[1-5]\d{2})|(6011))-?\d{4}-?\d{4}-?\d{4}|3[4,7]\d{13}$" OnPreRender="CCnumValidator_PreRender"></asp:RegularExpressionValidator></td>
                                <td style="width: 260px;vertical-align:top;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ExpDateTextBox"
                                        Display="Dynamic" ErrorMessage="Exp Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                <td style="vertical-align:top;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="CVVTextBox"
                                        Display="Dynamic" ErrorMessage="CVV Number is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 280px;" class="OrderFormTitle">
                                    Name on Card:<asp:TextBox ID="NameOnCardTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
                                <td class="OrderFormTitle" style="width: 260px;">
                                    Telephone Number:<asp:TextBox ID="MailingDayPhoneTextBox" runat="server" CssClass="tbxMargin" Columns="12"></asp:TextBox></td>
                                <td class="OrderFormTitle">
                                    Fax Number:<asp:TextBox ID="MailingFaxTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox><span style="color: #ff0000">*</span></td>
                            </tr>
                            <tr>
                                <td style="width: 280px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="NameOnCardTextBox"
                                        Display="Dynamic" ErrorMessage="Name on Card is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                <td style="width: 260px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="MailingDayPhoneTextBox"
                    Display="Dynamic" ErrorMessage="Telephone is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="PayPalLogo" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="PayPal" ImageUrl="images/paypal.gif" runat="server" Height="88px" Width="88px" />
                                </td>
                                <td class="OrderFormTitle" style="width: 260px;">
                                    Telephone Number:<asp:TextBox ID="MailingDayPhoneTextBox2" runat="server" CssClass="tbxMargin" Columns="12"></asp:TextBox></td>
                                <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="MailingDayPhoneTextBox2"
                    Display="Dynamic" ErrorMessage="Telephone is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                            
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        </table>
        <table>
        <tr>
            <td colspan="4" class="OrderFormSubTitle">
                Mailing Information 
                <asp:Label ID="BillingInfoLabel" runat="server" Font-Bold="False" Font-Size="Smaller"
                    Text="(must be the billing address of your credit card)"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" class="OrderFormTitle">
                Address:<asp:TextBox ID="MailingAddress1TextBox" runat="server" Columns="50" CssClass="tbxMargin"></asp:TextBox>
                <asp:TextBox ID="MailingAddress2TextBox" runat="server" Columns="50" CssClass="tbxMargin"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="MailingAddress1TextBox"
                    Display="Dynamic" ErrorMessage="Mailing Address is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            <td colspan="1" style="width: 82px">
            </td>
            <td style="width: 344px">
            </td>
        </tr>
        <tr>
            <td class="OrderFormTitle" style="width: 508px">
                State (US Only)<asp:DropDownList ID="MailingStateDropDownList" runat="server" CssClass="tbxMargin">
                    <asp:ListItem Value="Not Specified" selected="True">Not Specified</asp:ListItem>
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="FL">Flordia</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList></td>
            <td class="OrderFormTitle" colspan="2">
                City:<asp:TextBox ID="MailingCityTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
            <td class="OrderFormTitle">
                Zip/Postal Code:<asp:TextBox ID="MailingZipPostalCodeTextBox" runat="server" Width="96px" CssClass="tbxMargin"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 508px;"></td>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="MailingCityTextBox"
                    Display="Dynamic" ErrorMessage="City is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            <td style="width: 344px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="MailingZipPostalCodeTextBox"
                    Display="Dynamic" ErrorMessage="Zip/Postal Code is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="OrderFormTitle" colspan="2">
                County/Province: (Required for outside US only)<asp:TextBox ID="MailingCountyTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
            <td class="OrderFormTitle" colspan="2">
                Country:<asp:TextBox ID="MailingCountryTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 508px"></td>
            <td style="width: 284px"></td>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="MailingCountryTextBox"
                    Display="Dynamic" ErrorMessage="Country is required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        </table>
        <table>
        <tr>
            <td colspan="4" class="OrderFormSubTitle">
                Shipping Information
                <asp:Button ID="CopyMailingButton" runat="server" Text="Same as Mailing" Width="152px" CausesValidation="False" CssClass="ButtonClass" /></td>
        </tr>
        <tr>
            <td colspan="4" class="OrderFormTitle">
                Address:<asp:TextBox ID="ShippingAddress1TextBox" runat="server" ValidationGroup="Shipping" Columns="50" CssClass="tbxMargin"></asp:TextBox>
                <asp:TextBox ID="ShippingAddress2TextBox" runat="server" Columns="50" CssClass="tbxMargin"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ShippingAddress1TextBox"
                    Display="Dynamic" ErrorMessage="Shipping Address is required" SetFocusOnError="True" ValidationGroup="Shipping"></asp:RequiredFieldValidator></td>
            <td colspan="1" style="width: 49px">
            </td>
            <td style="width: 344px">
            </td>
        </tr>
        <tr>
            <td colspan="1" class="OrderFormTitle" style="width: 506px">
                State (US Only)<asp:DropDownList ID="ShippingStateDropDownList" runat="server" CssClass="tbxMargin">
                    <asp:ListItem value="Not Specified" selected="True">Not Specified</asp:ListItem>
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="FL">Flordia</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList></td>
            <td class="OrderFormTitle" colspan="2">
                City:<asp:TextBox ID="ShippingCityTextBox" runat="server" ValidationGroup="Shipping" CssClass="tbxMargin"></asp:TextBox></td>
            <td class="OrderFormTitle" style="width: 344px">
                Zip/Postal Code:<asp:TextBox ID="ShippingZipPostalCodeTextBox" runat="server" Width="96px" ValidationGroup="Shipping" CssClass="tbxMargin"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="1" style="width: 506px;"></td>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ShippingCityTextBox"
                    Display="Dynamic" ErrorMessage="City is required" SetFocusOnError="True" ValidationGroup="Shipping"></asp:RequiredFieldValidator></td>
            <td style="width: 344px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ShippingZipPostalCodeTextBox"
                    Display="Dynamic" ErrorMessage="Zip/Postal Code is required" SetFocusOnError="True"
                    ValidationGroup="Shipping"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" class="OrderFormTitle">
                County/Province: (Required for outside US only)<asp:TextBox ID="ShippingCountyTextBox" runat="server" CssClass="tbxMargin"></asp:TextBox></td>
            <td class="OrderFormTitle" colspan="2">
                Country:<asp:TextBox ID="ShippingCountryTextBox" runat="server" ValidationGroup="Shipping" CssClass="tbxMargin"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="1" style="width: 506px;">
                </td>
            <td style="width: 257px;">
                </td>
            <td style="width: 49px;">
            </td>
            <td style="width: 344px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ShippingCountryTextBox"
                    Display="Dynamic" ErrorMessage="Country is required" SetFocusOnError="True" ValidationGroup="Shipping"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="CalcShippingButton" runat="server" Text="Select Shipping Method" ValidationGroup="Shipping" CssClass="ButtonClass" /></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:MultiView ID="ShippingMultiView" runat="server">
                    <asp:View ID="ShippingView1" runat="server" EnableTheming="False">
                        <table style="width: 100%">
                            <tr>
                                <td class="OrderFormTitle" colspan="2" style="font-size: larger; text-align: center">
                                    Select Shipping Method</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:DropDownList ID="ShippingMethodDropDownList" runat="server" DataTextField="Method" DataValueField="Method">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                
                <asp:Button ID="ContinueCheckoutButton" runat="server" Text="Continue Checkout" PostBackUrl="~/ConfirmOrder.aspx" CssClass="ButtonClass" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        </table>
    </td></tr>
    </table><br />
    
</asp:Content>

