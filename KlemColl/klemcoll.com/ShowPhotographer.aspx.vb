Imports System.Web.Routing

Partial Class ShowPhotographer
    Inherits System.Web.UI.Page

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.Title = Page.RouteData.Values("id").ToString()

    End Sub
End Class
