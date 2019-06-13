
Partial Class Downloads
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'CType(Me.Master.FindControl("DownloadsButton"), msWebControlsLibrary.ExImageButton).Enabled = False
    End Sub
End Class
