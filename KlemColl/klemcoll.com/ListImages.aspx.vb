Option Explicit On
Option Strict On

Imports Klemcoll.DAL

Partial Class ListImages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Filter As String = ""
        Dim ReferingFile As String = ""
        Dim ReferringPage As String = Request.RawUrl.ToLower.ToString
        If Request.UrlReferrer IsNot Nothing Then
            ReferingFile = IO.Path.GetFileName(Request.UrlReferrer.ToString).ToLower()
        End If

        If ReferingFile.StartsWith("thegallery.aspx") And Request("vf") Is Nothing Then
            Dim Photographer1 As String = ""
            Dim Photographer2 As String = ""
            Dim PhotoType As String = ""
            Dim RaceType As String = ""
            Dim MakeOfCar1 As String = ""
            Dim MakeOfCar2 As String = ""
            Dim PhotoYear As String = ""
            Dim Driver1 As String = ""
            Dim Driver2 As String = ""

            For Each str As String In Request.Form
                If str.Contains("PhotographerDropDownList1") Then
                    Photographer1 = Request(str)
                ElseIf str.Contains("PhotographerDropDownList2") Then
                    Photographer2 = Request(str)
                ElseIf str.Contains("PhotoTypeDropDownList") Then
                    PhotoType = Request(str)
                ElseIf str.Contains("RaceTypeDropDownList") Then
                    RaceType = Request(str)
                ElseIf str.Contains("MakeOfCarDropDownList1") Then
                    MakeOfCar1 = Request(str)
                ElseIf str.Contains("MakeOfCarDropDownList2") Then
                    MakeOfCar2 = Request(str)
                ElseIf str.Contains("YearDropDownList") Then
                    PhotoYear = Request(str).Replace("all", "")
                ElseIf str.Contains("DriversDropDownList1") Then
                    Driver1 = Request(str)
                ElseIf str.Contains("DriversDropDownList2") Then
                    Driver2 = Request(str)
                End If
            Next


            ' Save the state to a session variable for the gallery to save its state.
            Session("Photographer1") = Photographer1
            Session("Photographer2") = Photographer2
            Session("PhotoType") = PhotoType
            Session("RaceType") = RaceType
            Session("MakeOfCar1") = MakeOfCar1
            Session("MakeOfCar2") = MakeOfCar2
            Session("PhotoYear") = PhotoYear
            Session("Driver1") = Driver1
            Session("Driver2") = Driver2

            If Photographer1 > "" And Photographer2 = "" Then
                Filter &= String.Format("PhotographerLastName = '{0}'", Photographer1)
            ElseIf Photographer1 > "" And Photographer2 > "" Then
                Filter &= String.Format("(PhotographerLastName = '{0}' OR PhotographerLastName = '{1}')", Photographer1, Photographer2)
            ElseIf Photographer1 = "" And Photographer2 > "" Then
                Filter &= String.Format("PhotographerLastName = '{0}'", Photographer2)
            End If

            If PhotoType > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("PhotoType = '{0}'", PhotoType)
            End If

            If RaceType > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("RaceType = '{0}'", RaceType)
            End If

            If MakeOfCar1 > "" And MakeOfCar2 = "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("MakeOfCar = '{0}'", MakeOfCar1)
            ElseIf MakeOfCar1 > "" And MakeOfCar2 > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("(MakeOfCar = '{0}' OR MakeOfCar = '{1}')", MakeOfCar1, MakeOfCar2)
            ElseIf MakeOfCar1 = "" And MakeOfCar2 > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("MakeOfCar = '{0}'", MakeOfCar2)
            End If
            If Driver1.Contains("-1") Then
                Driver1 = Driver1.Replace("-1", "') And (DriverLastName='")
            End If
            If Driver2.Contains("-1") Then
                Driver2 = Driver2.Replace("-1", "') And (DriverLastName='")
            End If
            If Driver1 > "" And Driver2 = "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("(DriverFirstName = '{0}')", Driver1)
            ElseIf Driver1 > "" And Driver2 > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("((DriverFirstName = '{0}') OR (DriverFirstName = '{1}'))", Driver1, Driver2)
            ElseIf Driver1 = "" And Driver2 > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("(DriverFirstName = '{0}')", Driver2)
            End If

            If PhotoYear > "" Then
                CheckForAnd(Filter)
                Filter &= String.Format("PhotoYear = '{0}'", PhotoYear)
            End If

            Session("ListImages") = Filter

            'Response.Write(Filter)
            'Response.End()

        ElseIf Request("vf") IsNot Nothing AndAlso Request("vf") = "1" Then
            ' Load the favorites.
            If Request.Cookies("Favorites") IsNot Nothing Then
                ' Cookie exists.
                Dim MyFavorites As New Favorites(Request, Response)
                Dim col As Collection = MyFavorites.GetFavorites
                Dim i As Integer = 0
                If col.Count > 0 Then
                    For Each fave As String In col
                        i += 1
                        If fave.Length > 0 Then
                            Filter &= "(Refid = '" & fave & "') "
                            If i < col.Count Then
                                Filter &= "OR "
                            End If
                        End If
                    Next
                Else
                    Filter = "Refid = 'XXXXXXXXX'"
                End If

            Else
                ' Cookie does not exist yet.
                Filter = "Refid = 'XXXXXXXXX'"
            End If
            Me.ClearFavoritesButton.Visible = True
        ElseIf ReferringPage.Contains("showimage.aspx") And Session("ListImages") IsNot Nothing Then    ' ReferingFile was replaced with ReferringPage by Emanuel 06/26/2007 because referring file returned an image name as an aspx file so the session states were not loaded
            Filter = Session("ListImages").ToString
        ElseIf ReferringPage.Contains("listimages.aspx") And Session("ListImages") IsNot Nothing Then ' ReferingFile was replaced with ReferringPage by Emanuel 06/26/2007 because referring file returned an image name as an aspx file so the session states were not loaded
            Filter = Session("ListImages").ToString
        Else
            Filter = ""
        End If

        If Request("vf") Is Nothing Then
            CheckForAnd(Filter)
            ' Only add this filter requested by Peter if people arent viewing their favorites.
            Filter &= "Listitems = -1"
        End If


        Me.ImagesObjectDataSource.FilterExpression = Filter
        Me.ImagesCollectionPager.DataSource = Me.ImagesObjectDataSource.Select
        Me.ImagesCollectionPager.BindToControl = Me.ImagesDataList
        Me.ImagesDataList.DataSourceID = ""
        Me.ImagesDataList.DataSource = Me.ImagesCollectionPager.DataSourcePaged

        'CollectionPager1.DataSource = SampleDataSet.Tables[0].DefaultView;

        '	//Let the Pager know what Control it needs to DataBind during the PreRender	
        '	CollectionPager1.BindToControl = Repeater1;

        '		//Set the DataSource of the Repeater to the PagedData coming from the Pager.
        '	Repeater1.DataSource = CollectionPager1.DataSourcePaged;
        If Me.ImagesCollectionPager.DataSourcePaged.Count > 0 Then
            Me.HeaderLabel.Text = "Click on the image for a more detailed description and a larger view, to place an order for a print, or to remove the image from your Favorites.<br /><br />If you wish to have access to your Favorites on a subsequent visit to us, please do not disable or erase cookies on your computer."
        Else
            If Request("vf") IsNot Nothing AndAlso Request("vf") = "1" Then
                Me.HeaderLabel.Text = "You have no favorites in your list."
            Else
                Me.HeaderLabel.Text = "Sorry, no Gallery images exist for your search criteria."
            End If

        End If

    End Sub

    Private Sub CheckForAnd(ByRef Filter As String)
        If Filter.Length > 0 Then
            Filter &= " AND "
        End If
    End Sub

    Protected Sub ClearFavoritesButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearFavoritesButton.Click
        ' Delete the Favorites cookie.
        Dim Favorites As New HttpCookie("Favorites", Nothing)
        Favorites.Expires = DateAdd(DateInterval.Day, -1, Now)
        Response.Cookies.Set(Favorites)
        Response.Redirect("Default.aspx", False)
    End Sub
End Class
