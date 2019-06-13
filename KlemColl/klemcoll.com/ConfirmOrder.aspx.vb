Option Explicit On
Option Strict On
Imports Klemcoll.DAL
Imports Klemcoll.DAL.KlemCollDataSetTableAdapters
Imports System.Configuration
Imports SendGrid
Imports SendGrid.Helpers.Mail

Partial Class ConfirmOrder
    Inherits System.Web.UI.Page


    Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitButton.Click
        Dim EmailBody As New StringBuilder
        Dim streamBody As New System.IO.StringWriter(EmailBody)
        Dim htmlWriter As New System.Web.UI.HtmlTextWriter(streamBody)
        Dim strEmailBody As String
        Dim PaymentMethod As String = ""

        Dim OrdersTA As New OrdersTableAdapter
        Dim OrderedBooksTA As New OrderedBooksTableAdapter
        Dim OrderedBooksDT As New KlemCollDataSet.OrderedBooksDataTable
        Dim OrderedImagesTA As New OrderedImagesTableAdapter
        Dim OrderedImagesDT As New KlemCollDataSet.OrderedImagesDataTable

        Dim OrdersDT As New KlemCollDataSet.OrdersDataTable
        Dim OrderDR As KlemCollDataSet.OrdersRow = OrdersDT.NewOrdersRow

        Dim FromEmailAddress As String = Nothing
        Dim ToEmailAddress As String = Nothing

        OrderDR.Name = Session("FirstName").ToString & " " & Session("LastName").ToString
        OrderDR.Address = Session("MailingAddress1").ToString & "</BR>" & Session("MailingAddress2").ToString
        OrderDR.City = Session("MailingCity").ToString
        OrderDR.email = Session("Email").ToString
        OrderDR.GrandTotal = CDec(Session("GrandTotal"))
        OrderDR.ShipAddress = Session("ShippingAddress1").ToString & "</BR>" & Session("ShippingAddress2").ToString
        OrderDR.Shipping = CDec(Session("ShippingPrice"))
        OrderDR.ShippingMethod = IIf(Session("ShippingMethod") Is Nothing, "Will Call", CStr(Session("ShippingMethod"))).ToString()
        OrderDR.ShipStateProv = Session("ShippingState").ToString
        OrderDR.ShipZipPostal = Session("ShippingZip").ToString
        OrderDR.StateProv = Session("MailingState").ToString
        OrderDR.Subtotal = CDec(Session("SubTotalPrice"))
        OrderDR.ZipPostal = Session("MailingZip").ToString
        OrderDR.Country = Session("MailingCountry").ToString
        OrderDR.ShipCountry = Session("ShippingCountry").ToString
        OrderDR.ShipCity = Session("ShippingCity").ToString
        If Session("MailingDayPhone") IsNot Nothing AndAlso Session("MailingDayPhone").ToString() <> "" Then
            OrderDR.Phone = Session("MailingDayPhone").ToString
        ElseIf Session("PayPalPhone") IsNot Nothing AndAlso Session("PayPalPhone").ToString() <> "" Then
            OrderDR.Phone = Session("PayPalPhone").ToString
        End If

        If Session("PaymentType").ToString <> "PayPal" Then
            'If Session("CCNumber") IsNot Nothing Then
            OrderDR.CCNumber = Session("CCNumber").ToString
            OrderDR.CCExpDate = Session("CCExpDate").ToString
            OrderDR.NameOnCard = Session("NameOnCard").ToString
            OrderDR.CVV = Session("CVV").ToString
        End If


        OrdersDT.AddOrdersRow(OrderDR)

        OrdersTA.Update(OrdersDT)

        Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)

        OrderedBooksDT.OrderIDColumn.DefaultValue = OrderDR.OrderID
        OrderedImagesDT.OrderIDColumn.DefaultValue = OrderDR.OrderID

        Session("OrderID") = OrderDR.OrderID

        If OrderedBooksDT.Columns.Contains("SubTotal") Then OrderedBooksDT.Columns.Remove("SubTotal")
        If OrderedImagesDT.Columns.Contains("SubTotal") Then OrderedImagesDT.Columns.Remove("SubTotal")

        OrderedBooksDT.Merge(CartDS.SessionBooks, False, MissingSchemaAction.Ignore)

        OrderedImagesDT.Merge(CartDS.SessionImages, False, MissingSchemaAction.Ignore)

        For Each DR As KlemCollDataSet.OrderedBooksRow In OrderedBooksDT.Rows
            DR.SetAdded()
        Next
        For Each DR As KlemCollDataSet.OrderedImagesRow In OrderedImagesDT.Rows
            DR.SetAdded()
        Next

        OrderedBooksTA.Update(OrderedBooksDT)
        OrderedImagesTA.Update(OrderedImagesDT)

        Me.ShowOrder1.RenderControl(htmlWriter)

        strEmailBody = PaymentMethod & "</BR>" & EmailBody.ToString

        '' Send the customer email.
        'Dim SMTPMessage As New System.Net.Mail.MailMessage("noreply@klemcoll.com", OrderDR.email, "Confirm Order", strEmailBody)
        'SMTPMessage.IsBodyHtml = True
        'Dim SMTP As New System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings("SMTPServer"))
        'SMTP.Send(SMTPMessage)

        '' Send the company email.
        '' Add a link to view the order if its not a PayPal order.
        'If Session("PaymentType").ToString <> "PayPal" Then
        '    strEmailBody = String.Format("</BR></BR><A HREF='{0}Secure/ViewOrder.aspx?id={1}'>Click Here to view the order</A>", System.Configuration.ConfigurationManager.AppSettings("SecureBaseAddress"), OrderDR.OrderID) & strEmailBody
        'End If
        'SMTPMessage = New System.Net.Mail.MailMessage("noreply@klemcoll.com", System.Configuration.ConfigurationManager.AppSettings("KlemcollEmail"), "Klemcoll Order", strEmailBody)
        'SMTPMessage.IsBodyHtml = True
        'SMTP.Send(SMTPMessage)


        ' Send the customer email.
        FromEmailAddress = ConfigurationManager.AppSettings("FromEmail")
        ToEmailAddress = OrderDR.email
        SendEmailByUsingSendGridService(FromEmailAddress, ToEmailAddress, "Confirm Order", strEmailBody, True)

        ' Send the company email.
        If Session("PaymentType").ToString <> "PayPal" Then
            strEmailBody = String.Format("</BR></BR><A HREF='{0}Secure/ViewOrder.aspx?id={1}'>Click Here to view the order</A>", ConfigurationManager.AppSettings("SecureBaseAddress"), OrderDR.OrderID) & strEmailBody
        End If
        ToEmailAddress = ConfigurationManager.AppSettings("KlemcollEmail")
        SendEmailByUsingSendGridService(FromEmailAddress, ToEmailAddress, "Klemcoll Order", strEmailBody, True)

        If Session("PaymentType").ToString() = "PayPal" Then
            Response.Redirect("PayPalTransfer.aspx", False)
        Else
            Response.Redirect("OrderCompleted.aspx", False)
        End If
    End Sub

    Private Sub SendEmailByUsingSendGridService(fromAddress As String, toAddress As String, subject As String, body As String, isBodyHtml As Boolean)
        Dim ApiKey As String = Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings("SendGridApiKey"))
        Dim Client As SendGridClient = New SendGridClient(ApiKey)
        Dim FromEmail As EmailAddress = New EmailAddress(fromAddress, "")
        Dim ToEmail As New EmailAddress(toAddress, "")
        Dim Message As SendGridMessage = MailHelper.CreateSingleEmail(FromEmail, ToEmail, subject, String.Empty, body)
        If isBodyHtml = True Then
            Message.HtmlContent = body
        End If
        Dim Response As Response = Client.SendEmailAsync(Message).Result
    End Sub
End Class
