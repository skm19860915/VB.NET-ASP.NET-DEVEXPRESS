
Partial Class OrderCompleted
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Me.EmailLabel.Text = CStr(Session("email"))
    End Sub
End Class
