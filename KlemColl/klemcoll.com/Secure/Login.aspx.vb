Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports Klemcoll.DAL
Imports Klemcoll.DAL.KlemCollDataSetTableAdapters

Partial Class Login
    Inherits System.Web.UI.Page
    Dim SpecialPubAdapter As New SpecialPublicationsTableAdapter
    Dim SpecialPubTable As New KlemCollDataSet.SpecialPublicationsDataTable
    Dim PhotographersAdapter As New OurPhotographersTableAdapter
    Dim PhotographersTable As New KlemCollDataSet.OurPhotographersDataTable

    Protected Sub ProcessLogin(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click

        If FormsAuthentication.Authenticate(txtUser.Text, txtPassword.Text) Then
            If Request("id") Is Nothing Then
                Dim filepath As String
                Dim sitemap As String
                Dim root As String = Request.PhysicalApplicationPath() & "ContentPages\"
                Dim files() As String = Directory.GetFiles(root)
                Dim f As String

                sitemap = "<Website>" & vbCrLf
                For Each f In files
                    Dim filename As String = Path.GetFileName(f)
                    If InStr(filename, ".htm") > 0 Then
                        sitemap = sitemap & "<" & filename.ToString() & " />" & vbCrLf
                    End If
                Next
                sitemap = sitemap & "<SpecialPublications>" & vbCrLf
                SpecialPubAdapter.Fill(SpecialPubTable)
                If SpecialPubTable.Count > 0 Then
                    For Each SPRow As KlemCollDataSet.SpecialPublicationsRow In SpecialPubTable
                        sitemap = sitemap & "<SpecialPublication>" & SPRow.Name & "</SpecialPublication>" & vbCrLf
                    Next
                End If
                sitemap = sitemap & "</SpecialPublications>" & vbCrLf

                sitemap = sitemap & "<Photographers>" & vbCrLf
                PhotographersAdapter.Fill(PhotographersTable)
                If PhotographersTable.Count > 0 Then
                    For Each PhotoRow As KlemCollDataSet.OurPhotographersRow In PhotographersTable
                        sitemap = sitemap & "<Photographer>" & PhotoRow.photographername & "</Photographer>" & vbCrLf
                    Next
                End If
                sitemap = sitemap & "</Photographers>" & vbCrLf
                sitemap = sitemap & "<GalleryImages />" & vbCrLf
                sitemap = sitemap & "</Website>"

                filepath = Request.PhysicalApplicationPath() & "Secure\ContentManagerSiteMap.xml"

                Try
                    File.WriteAllText(filepath, sitemap)
                Catch FileNotFoundException As Exception

                End Try
                FormsAuthentication.RedirectFromLoginPage(txtUser.Text, chkPersistLogin.Checked)
            Else
                FormsAuthentication.RedirectFromLoginPage(txtUser.Text, chkPersistLogin.Checked)
                'Response.Redirect("ViewOrder.aspx?id=" & Request("id"))
            End If
            
        Else
            ErrorMessage.InnerHtml = "<b>Wrong Combination...</b> please re-enter your credentials..."
        End If

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtUser.Focus()
    End Sub
End Class
