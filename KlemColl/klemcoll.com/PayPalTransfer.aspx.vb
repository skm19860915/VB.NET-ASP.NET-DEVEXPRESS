Partial Class PayPalTransfer
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Me.business.Value = System.Configuration.ConfigurationManager.AppSettings("PayPalEmail")
        Me.item_name.Value = "Klemcoll Online Order"
        Me.invoice.Value = Session("OrderID").ToString()
        Me.custom.Value = ""
        Me.amount.Value = Session("SubTotal").ToString()
        Me.shipping.Value = Session("ShippingPrice").ToString()
        Me.image_url.Value = ""
    End Sub
End Class
