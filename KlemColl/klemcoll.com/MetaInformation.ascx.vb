Imports System.Web.Routing
Imports System.IO

Partial Class MetaInformation
    Inherits System.Web.UI.UserControl
    Public Location As String

    Protected Sub Default_head(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Need to make some custom scenarios to account for URL rewriting.

        Dim rules As New List(Of String)
        Dim FileName As String

        If Request.Path.ToLower().Contains("photographers/") Then
            FileName = "showphotographer" & Page.RouteData.Values("id").ToString() & "-MetaInfo.htm"
        ElseIf Request.Path.ToLower().Contains("publications/") Then
            FileName = "showbook" & Page.RouteData.Values("id").ToString() & "-MetaInfo.htm"
            'ElseIf Request.Path.ToLower().Contains("image/") Then
            '    FileName = "showbook" & Page.RouteData.Values("id").ToString() & "-MetaInfo.htm"
        Else
            Dim FileNameRegEx As New Regex(".*/([^/]*).aspx$")
            FileName = FileNameRegEx.Replace(Request.Path, "$1") & Request("id") & "-MetaInfo.htm"
        End If

        Location = Request.MapPath("~/ContentPages/MetaInformation/" + FileName)
        Dim FolderPath As String = Request.MapPath("~/ContentPages/MetaInformation/")

        If File.Exists(Location) Then
            Location = Location
        Else
            Location = FolderPath + "PageMissing-MetaInfo.htm"
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Page.Header.Title = Nothing
    End Sub
End Class
