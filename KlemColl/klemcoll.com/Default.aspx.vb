
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Request("clearcart") IsNot Nothing AndAlso Request("clearcart") = "1" Then
            Session("CartDS") = Nothing
        End If
    End Sub
End Class
