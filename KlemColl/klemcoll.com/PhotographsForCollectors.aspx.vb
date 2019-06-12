
Partial Class PhotographsForCollectors
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'CType(Me.Master.FindControl("PhotographsForCollectorsButton"), msWebControlsLibrary.ExImageButton).Enabled = False
    End Sub
End Class
