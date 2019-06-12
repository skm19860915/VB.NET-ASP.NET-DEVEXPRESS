
Partial Class Secure_TheGalleryImages
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("TheGalleryImages.aspx")
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim strSelectStatement As String
        Dim hasSomething As Boolean = False
        strSelectStatement = "Select * from Images"
        If Not ddPhotoType.SelectedIndex = 0 And Not ddPhotoType.SelectedIndex = -1 Then
            If hasSomething Then
                strSelectStatement = strSelectStatement & " AND PhotoType = '" & ddPhotoType.SelectedValue & "'"
            Else
                strSelectStatement = strSelectStatement & " WHERE PhotoType = '" & ddPhotoType.SelectedValue & "'"
                hasSomething = True
            End If
        End If
        If Not txtName.Text = "" Then
            If hasSomething Then
                strSelectStatement = strSelectStatement & " AND Name LIKE '%" & txtName.Text & "%'"
            Else
                strSelectStatement = strSelectStatement & " WHERE Name LIKE '%" & txtName.Text & "%'"
                hasSomething = True
            End If
        End If
        If Not txtRefid.Text = "" Then
            If hasSomething Then
                strSelectStatement = strSelectStatement & " AND refid LIKE '%" & txtRefid.Text & "%'"
            Else
                strSelectStatement = strSelectStatement & " WHERE refid LIKE '%" & txtRefid.Text & "%'"
                hasSomething = True
            End If
        End If
        If Not txtPhotographer.Text = "" Then
            If hasSomething Then
                strSelectStatement = strSelectStatement & " AND Photographerlastname LIKE '%" & txtPhotographer.Text & "%'"
            Else
                strSelectStatement = strSelectStatement & " WHERE Photographerlastname LIKE '%" & txtPhotographer.Text & "%'"
                hasSomething = True
            End If
        End If
        If Not txtDriver.Text = "" Then
            If hasSomething Then
                strSelectStatement = strSelectStatement & " AND Driverlastname LIKE '%" & txtDriver.Text & "%'"
            Else
                strSelectStatement = strSelectStatement & " WHERE Driverlastname LIKE '%" & txtDriver.Text & "%'"
                hasSomething = True
            End If
        End If
        SqlDataSource1.SelectCommand = strSelectStatement
        GridView1.DataBind()
    End Sub

    Protected Sub ddPhotoType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddPhotoType.DataBound
        ddPhotoType.Items.Insert(0, New ListItem("All", "All"))
    End Sub
End Class
