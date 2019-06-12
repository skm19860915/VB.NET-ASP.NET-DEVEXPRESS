Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.Threading.Tasks
Imports SendGrid
Imports SendGrid.Helpers.Mail
Imports dotnetSHIP

Partial Class ContactUs
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'CType(Me.Master.FindControl("ContactUsButton"), msWebControlsLibrary.ExImageButton).Enabled = False

        ' Set the email address to the Contact Us link from the Host table.
        'Dim HostTA As New KlemCollDataSetTableAdapters.HostTableAdapter
        'Dim HostDT As KlemCollDataSet.HostDataTable = HostTA.GetData
        'If HostDT.Rows.Count > 0 Then
        '    Dim ContactUsHyperLink As HyperLink = CType(Me.ImageFormView.FindControl("ContactUsHyperLink"), HyperLink)
        '    ContactUsHyperLink.NavigateUrl = "mailto:" & HostDT(0).EmailCompany
        'End If
    End Sub

    Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitButton.Click
        'Dim NewMail As New System.Net.Mail.SmtpClient(AppSettings("SMTPServer"))
        'Dim FromEmail As String = "noreply@klemcoll.com"
        'If Me.EmailTextBox.Text.Length > 0 Then
        '    FromEmail = Me.EmailTextBox.Text
        'End If
        'Dim Body As String = String.Concat("Name: ", Me.NameTextBox.Text, vbCr, "Email: ", Me.EmailTextBox.Text, vbCr, vbCr, "Message: ", vbCr, Me.MessageTextBox.Text)

        'NewMail.Send(FromEmail, System.Configuration.ConfigurationManager.AppSettings("KlemcollEmail"), "Klemcoll.com Contact Us form", Body)
        'Me.ContactUsMultiView.SetActiveView(Me.ThankYouView)

        Dim FromEmailAddress As String = AppSettings("FromEmail")
        Dim ToEmailAddress As String = AppSettings("ToEmail")

        Dim Name As String = NameTextBox.Text
        Dim Message As String = MessageTextBox.Text
        Dim UserEmail As String = EmailTextBox.Text
        Dim Subject As String = "Klemcoll.com Contact Us Form"
        Dim Body As String = String.Concat("Name: ", Name, vbCr, "Email: ", UserEmail, vbCr, vbCr, "Message: ", vbCr, Message)

        Dim Success As Boolean = SendEmail(FromEmailAddress, ToEmailAddress, Subject, Body)
        If Success Then
            Me.ContactUsMultiView.SetActiveView(Me.ThankYouView)
        End If

    End Sub


    Private Function SendEmail(fromAddress As String, toAddress As String, subject As String, body As String) As Boolean
        Dim ApiKey As String = Environment.GetEnvironmentVariable(AppSettings("SendGridApiKey"))
        Dim Client As SendGridClient = New SendGridClient(ApiKey)
        Dim FromEmail As EmailAddress = New EmailAddress(fromAddress, "Klemcoll")
        Dim ToEmail As New EmailAddress(toAddress, "Customer")
        Dim Message As SendGridMessage = MailHelper.CreateSingleEmail(FromEmail, ToEmail, subject, String.Empty, body)
        Dim Response As Response = Client.SendEmailAsync(Message).Result

        If Response.StatusCode = HttpStatusCode.Accepted Then
            Return True
        End If
        Return False
    End Function
End Class

