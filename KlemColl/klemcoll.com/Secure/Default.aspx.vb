Imports System.IO
Imports System.Configuration
Imports Klemcoll.DAL
Imports Klemcoll.DAL.KlemCollDataSetTableAdapters

Partial Class ContentManager
    Inherits System.Web.UI.Page
    Dim sr As StreamReader
    Dim SpecialPubAdapter As New SpecialPublicationsTableAdapter
    Dim SpecialPubTable As New KlemCollDataSet.SpecialPublicationsDataTable
    Dim PhotographersAdapter As New OurPhotographersTableAdapter
    Dim PhotographersTable As New KlemCollDataSet.OurPhotographersDataTable
    Dim GalleryAdapter As New GalleryImagesTableAdapter
    Dim GalleryTable As New KlemCollDataSet.GalleryImagesDataTable

    Protected Sub lnkLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogOut.Click
        FormsAuthentication.SignOut()
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        Dim filepath, content As String
        Dim root As String
        content = ""
        If TreeView1.SelectedNode.Depth = 1 Then
            If TreeView1.SelectedNode.Text = "GalleryImages" Then
                Response.Redirect("TheGalleryImages.aspx")
            Else
                root = Request.PhysicalApplicationPath() & "ContentPages\"
                filepath = root & TreeView1.SelectedValue
                Try
                    sr = File.OpenText(filepath)
                    content = sr.ReadToEnd()
                    sr.Close()
                Catch FileNotFoundException As Exception
                    content = ""
                End Try
            End If
        Else
            If TreeView1.SelectedNode.Parent IsNot Nothing Then
                Select Case TreeView1.SelectedNode.Parent.Text
                    Case "SpecialPublications"
                        SpecialPubAdapter.FillByBookName(SpecialPubTable, TreeView1.SelectedNode.Text)
                        If SpecialPubTable.Count > 0 Then
                            Dim SPRow As KlemCollDataSet.SpecialPublicationsRow = SpecialPubTable(0)
                            content = SPRow.Description
                        End If
                    Case "Photographers"
                        PhotographersAdapter.FillByPhotographerName(PhotographersTable, TreeView1.SelectedNode.Text)
                        If PhotographersTable.Count > 0 Then
                            Dim PhotoRow As KlemCollDataSet.OurPhotographersRow = PhotographersTable(0)
                            content = PhotoRow.listinfo
                        End If
                End Select
            End If
        End If
        KtmlContent.Text = content
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        Dim filepath As String
        If Request.QueryString("GalleryImage") = Nothing Then
            If TreeView1.SelectedNode.Depth = 1 Then

                filepath = Request.PhysicalApplicationPath() & "ContentPages\" & TreeView1.SelectedValue
                Try
                    File.WriteAllText(filepath, KtmlContent.Text)
                Catch FileNotFoundException As Exception

                End Try
            Else
                Select Case TreeView1.SelectedNode.Parent.Text
                    Case "SpecialPublications"
                        SpecialPubAdapter.FillByBookName(SpecialPubTable, TreeView1.SelectedNode.Text)
                        If SpecialPubTable.Count > 0 Then
                            Dim SPRow As KlemCollDataSet.SpecialPublicationsRow = SpecialPubTable(0)
                            SPRow.Description = KtmlContent.Text
                            SpecialPubAdapter.Update(SpecialPubTable)
                        End If
                    Case "Photographers"
                        PhotographersAdapter.FillByPhotographerName(PhotographersTable, TreeView1.SelectedNode.Text)
                        If PhotographersTable.Count > 0 Then
                            Dim PhotoRow As KlemCollDataSet.OurPhotographersRow = PhotographersTable(0)
                            PhotoRow.listinfo = KtmlContent.Text
                            PhotographersAdapter.Update(PhotographersTable)
                        End If
                End Select
            End If
        Else
            GalleryAdapter.FillByID(GalleryTable, CInt(Request.QueryString("GalleryImage")))
            If GalleryTable.Count > 0 Then
                Dim GalleryRow As KlemCollDataSet.GalleryImagesRow = GalleryTable(0)
                GalleryRow.Description = KtmlContent.Text
                GalleryAdapter.Update(GalleryTable)
                Response.Redirect("Default.aspx?GalleryImage=" & Request.QueryString("GalleryImage"))
            End If
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Request.QueryString("GalleryImage") = Nothing Then
            GalleryAdapter.FillByID(GalleryTable, CInt(Request.QueryString("GalleryImage")))
            If GalleryTable.Count > 0 Then
                Dim GalleryRow As KlemCollDataSet.GalleryImagesRow = GalleryTable(0)
                KtmlContent.Text = GalleryRow.Description
           End If
        End If
    End Sub
End Class
