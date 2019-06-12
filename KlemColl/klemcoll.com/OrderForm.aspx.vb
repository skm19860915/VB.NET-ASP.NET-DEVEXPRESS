Imports Klemcoll.DAL
Imports Klemcoll.DAL.KlemCollDataSetTableAdapters
Imports Klemcoll.DAL.CartDataSetTableAdapters
Imports System.Data

Partial Class OrderForm
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ' If no session id was passed to lookup redirect to the home page.
        If Request("id") Is Nothing Then
            Response.Redirect("Default.aspx", False)
        End If

    End Sub

    Protected Sub CalcShippingButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalcShippingButton.Click
        Dim RequestedSessionID As String = Request("id")
        If RequestedSessionID IsNot Nothing Then
            ' Create the object used to calculate shipping prices.
            Dim MyShipping As New Shipping

            ' Load the cart data from the session tables.
            Dim SessionBooksTA As New SessionBooksTableAdapter
            Dim sessionimagesta As New SessionImagesTableAdapter
            Dim CartDS As New CartDataSet
            Dim BooksCount As Integer = SessionBooksTA.Fill(CartDS.SessionBooks, Request("id"))
            Dim ImagesCount As Integer = sessionimagesta.Fill(CartDS.SessionImages, Request("id"))

            Dim InternationalShipping As Boolean = False
            If Me.ShippingStateDropDownList.SelectedValue = "Not Specified" Then
                ' No state was specified, which means the order is being shipping outside the US.
                InternationalShipping = True
            End If

            If BooksCount > 0 Or ImagesCount > 0 Then
                Me.ShippingMethodDropDownList.DataSource = MyShipping.CalculateShipping(CartDS.SessionBooks, CartDS.SessionImages, InternationalShipping)
                Me.ShippingMethodDropDownList.DataBind()
            Else
                'Error?
            End If

            Session("CartDS") = CartDS
            Me.ShippingMultiView.SetActiveView(Me.ShippingView1)
        End If


    End Sub

    Protected Sub CopyMailingButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CopyMailingButton.Click
        Me.ShippingAddress1TextBox.Text = Me.MailingAddress1TextBox.Text
        Me.ShippingAddress2TextBox.Text = Me.MailingAddress2TextBox.Text
        Me.ShippingCityTextBox.Text = Me.MailingCityTextBox.Text
        Me.ShippingCountryTextBox.Text = Me.MailingCountryTextBox.Text
        Me.ShippingStateDropDownList.SelectedValue = Me.MailingStateDropDownList.SelectedValue
        Me.ShippingZipPostalCodeTextBox.Text = Me.MailingZipPostalCodeTextBox.Text
        Me.ShippingCountyTextBox.Text = Me.MailingCountyTextBox.Text

    End Sub

    Protected Sub PaymentTypeDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaymentTypeDropDownList.SelectedIndexChanged
        If Me.PaymentTypeDropDownList.SelectedIndex = 0 Then
            Me.PaymentTypeMultiView.SetActiveView(Me.CreditCardView)
            Me.BillingInfoLabel.Visible = True
            'Me.PayPalLogo.Visible = False
        Else
            Me.PaymentTypeMultiView.SetActiveView(Me.PayPalLogo)
            Me.BillingInfoLabel.Visible = False
            'Me.PayPalLogo.Visible = True
        End If
    End Sub

    Protected Sub PaymentTypeMultiView_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaymentTypeMultiView.PreRender
        If Me.PaymentTypeDropDownList.SelectedIndex = 0 Then
            Me.PaymentTypeMultiView.SetActiveView(Me.CreditCardView)
        End If
    End Sub

    Protected Sub MailingStateDropDownList_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MailingStateDropDownList.PreRender
        DirectCast(sender, DropDownList).Attributes.Add("onChange", "fillCountry(this.options[this.selectedIndex].value, '" & MailingCountryTextBox.ClientID & "');Domestic(this.options[this.selectedIndex].value, '" & MailingCountyTextBox.ClientID & "');")
    End Sub

    Protected Sub ShippingStateDropDownList_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShippingStateDropDownList.PreRender
        DirectCast(sender, DropDownList).Attributes.Add("onChange", "fillCountry(this.options[this.selectedIndex].value, '" & ShippingCountryTextBox.ClientID & "');Domestic(this.options[this.selectedIndex].value, '" & ShippingCountyTextBox.ClientID & "');")
    End Sub

    Protected Sub CCnumValidator_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.CCNumberTextBox.Text = "4111111111111111" Then
            Me.RegularExpressionValidator2.IsValid = True
        End If
    End Sub
End Class
