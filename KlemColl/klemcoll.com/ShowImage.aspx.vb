Option Explicit On
Option Strict On

Imports System.IO
Imports Klemcoll.DAL
'Imports Klemcoll.DAL.KlemCollDataSetTableAdapters

Partial Class ShowImage
    Inherits System.Web.UI.Page

    Dim MyFavorites As Favorites

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        MyFavorites = New Favorites(Request, Response)
        If Request.Browser.Cookies = True Then
            Dim RefId As String
            If Page.RouteData.Values("refid") IsNot Nothing Then
                RefId = Page.RouteData.Values("refid").ToString()
            ElseIf Request("refid") <> "" Then
                RefId = Request("refid")
            Else
                Throw New Exception("Cannot find passed refid.")
            End If

            If Request("option") IsNot Nothing Then RefId &= Request("option")
            If Request.Cookies("Favorites") IsNot Nothing Then
                If MyFavorites.GetFavorites.Contains(RefId) = True Then
                    ' This refid was found in the favorites.
                    Me.FavoritesMultiView.SetActiveView(Me.RemoveFavoriteView)
                Else
                    Me.FavoritesMultiView.SetActiveView(Me.AddFavoriteView)
                End If
            Else
                Me.FavoritesMultiView.SetActiveView(Me.AddFavoriteView)
            End If
        Else
            Me.FavoritesMultiView.ActiveViewIndex = -1
        End If

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Page.RouteData.Values("Refid") IsNot Nothing Then
            Dim Refid As String = Page.RouteData.Values("Refid").ToString()
            Dim ImageOption As String = ""
            Dim ColorBase As Boolean = CBool(IIf(Refid.ToLower.StartsWith("c"), True, False))
            If Request("option") IsNot Nothing Then
                ImageOption = Request("option")
            Else
                If Refid.EndsWith("bw") Then
                    ImageOption = "bw"
                    Refid = Refid.Substring(0, Refid.Length - 2)
                ElseIf Refid.EndsWith("bwc") Then
                    ImageOption = "bwc"
                    Refid = Refid.Substring(0, Refid.Length - 3)
                ElseIf Refid.EndsWith("cp") Then
                    ImageOption = "cp"
                    Refid = Refid.Substring(0, Refid.Length - 2)
                End If
            End If

            'If Page.IsPostBack = False Then
            Dim ImageTA As New KlemCollDataSetTableAdapters.ImagesTableAdapter
            Dim ImageDT As KlemCollDataSet.ImagesDataTable = ImageTA.GetByRefid(Refid & ImageOption)
            If ImageDT.Rows.Count > 0 Then
                ' Load the basic data.
                Me.RefidHiddenField.Value = Refid & ImageOption
                'Me.Image2.ImageUrl = "~/Images/Large/" & Refid & ImageOption & ".jpg"
                Me.Image1.Style.Value = "background-image:url(" & Request.ApplicationPath & IIf(Request.ApplicationPath.EndsWith("/"), "", "/").ToString() & "Images/Large/" & Refid & ImageOption & ".jpg);display:block;width:360px;height:360px;background-repeat:no-repeat;background-position:center;"
                'Me.Image1.ImageUrl = "Images/Large/" & Refid & ImageOption & ".jpg"
                'Me.Image1.NavigateUrl = Request.Url.AbsoluteUri.ToString
                Me.NameLabel.Text = ImageDT(0).Name
                Me.RefidLabel.Text = Refid & ImageOption
                Me.DescLabel.Text = ImageDT(0).Description


                Me.SignedAvailableHF.Value = ImageDT(0).SignedAvailable.ToString()
                If ImageDT(0).SignedAvailable = 0 Then
                    Me.Signed.Visible = False
                End If

                'If ImageDT(0).SignedAvailable = 0 Then
                '    Me.Signed.SetActiveView(Me.SignedOriginal)
                'Else
                '    Me.Signed.ActiveViewIndex = -1
                'End If

                ' Display the appropriate buttons depending on the options for this image.
                ' Get the root directory to the images.
                Dim ImagesRoot As String = Server.MapPath("\Images\large\")

                Dim BWCroppedVersion As String = ""
                Dim ColorCroppedVersion As String = ""
                Dim BWVersion As String = ""
                Dim ColorVersion As String = ""

                If ColorBase = True Then
                    'Me.ViewBWButton.PostBackUrl = "ShowImage.aspx?refid=" & Refid & "&option=bw"
                    Me.ViewBWButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = Refid + "bw"})
                    BWVersion = String.Concat(ImagesRoot, Refid, "bw.jpg")
                Else
                    'Me.ViewBWButton.PostBackUrl = "ShowImage.aspx?refid=" & Refid & "&option="
                    Me.ViewBWButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = Refid})
                    BWVersion = String.Concat(ImagesRoot, Refid, ".jpg")
                End If

                If ColorBase = True Then
                    BWCroppedVersion = String.Concat(ImagesRoot, Refid, "bwc.jpg")
                    'Me.ViewBWCroppedButton.PostBackUrl = "ShowImage.aspx?Refid=" & Refid & "&option=bwc"
                    Me.ViewBWCroppedButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = Refid + "bwc"})
                Else
                    BWCroppedVersion = String.Concat(ImagesRoot, Refid, "cp.jpg")
                    'Me.ViewBWCroppedButton.PostBackUrl = "ShowImage.aspx?Refid=" & Refid & "&option=cp"
                    Me.ViewBWCroppedButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = Refid + "cp"})
                End If

                If ColorBase = True Then
                    ColorVersion = String.Concat(ImagesRoot, Refid, ".jpg")
                    'Me.ViewColorButton.PostBackUrl = "ShowImage.aspx?Refid=" & Refid & "&option="
                    Me.ViewColorButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = Refid})
                Else
                    ColorVersion = String.Concat(ImagesRoot, "C", Refid, ".jpg")
                    'Me.ViewColorButton.PostBackUrl = "ShowImage.aspx?Refid=C" & Refid & "&option="
                    Me.ViewColorButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = "C" + Refid})
                End If

                If ColorBase = True Then
                    ColorCroppedVersion = String.Concat(ImagesRoot, Refid, "cp.jpg")
                Else
                    ColorCroppedVersion = String.Concat(ImagesRoot, "C", Refid, "cp.jpg")
                End If

                'Me.ViewColorCroppedButton.PostBackUrl = "ShowImage.aspx?Refid=" & Refid & "&option=cp"
                Me.ViewColorCroppedButton.PostBackUrl = GetRouteUrl("Image", New With {.Refid = Refid + "cp"})

                ' If we are looking at a color image check for B&W and Cropped versions.
                If Refid.ToLower.StartsWith("c") And ImageOption.Contains("bw") = False Then
                    ' Viewing a Color image.

                    ' Check for B&W version.
                    Me.ViewBWButton.Visible = File.Exists(BWVersion)
                    ' Check for a B&W Cropped version.
                    Me.ViewBWCroppedButton.Visible = File.Exists(BWCroppedVersion)
                    ' Check for a Color Cropped version.
                    Me.ViewColorCroppedButton.Visible = File.Exists(ColorCroppedVersion)
                    ' Check for a Color version.
                    Me.ViewColorButton.Visible = File.Exists(ColorVersion)
                Else
                    ' B&W image.
                    ' Check for B&W version.
                    Me.ViewBWButton.Visible = File.Exists(BWVersion)
                    ' Check for a B&W Cropped version.
                    Me.ViewBWCroppedButton.Visible = File.Exists(BWCroppedVersion)
                    ' Check for a Color Cropped version.
                    Me.ViewColorCroppedButton.Visible = File.Exists(ColorCroppedVersion)
                    ' Check for a Color version.
                    Me.ViewColorButton.Visible = File.Exists(ColorVersion)
                End If

                ' Turn off the button for the currently selected image.
                Select Case ImageOption
                    Case ""
                        If ColorBase = True Then
                            Me.ViewColorButton.Visible = False
                        Else
                            Me.ViewBWButton.Visible = False
                        End If
                    Case "bw"
                        Me.ViewBWButton.Visible = False
                    Case "bwc"
                        Me.ViewBWCroppedButton.Visible = False

                    Case "cp"
                        If ColorBase = True Then
                            Me.ViewColorCroppedButton.Visible = False
                        Else
                            Me.ViewBWCroppedButton.Visible = False
                        End If

                End Select

                If Me.ViewColorButton.Visible = False And Me.ViewBWButton.Visible = False And Me.ViewBWCroppedButton.Visible = False And Me.ViewColorCroppedButton.Visible = False Then
                    Me.OtherVariationsLabel.Visible = False
                Else
                    Me.OtherVariationsLabel.Visible = True
                End If

                Me.AddToCartButton.PostBackUrl = "~/AddEditImage.aspx?refid=" & Refid & ImageOption & "&return=ShowImage"
                Me.BackButton.PostBackUrl = "~/ListImages.aspx?page=" & Request("page")
            End If

            ' Check for the existance of other version of this file.

            'End If


        End If
    End Sub

    Private Function CheckForC(ByVal RefidToCheck As String) As String
        If RefidToCheck.ToLower.StartsWith("c") = False Then
            Return "C" & RefidToCheck
        Else
            Return RefidToCheck
        End If
    End Function

    Private Function ParseRefid(ByVal PathToParse As String) As String
        Dim Refid As String = PathToParse.Substring(PathToParse.LastIndexOf("\") + 1)

        Return Refid.Substring(0, Refid.Length - 4)
    End Function

    Protected Sub AddToFavoritesButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddToFavoritesButton.Click
        ' Version 4.
        If Request.Browser.Cookies = True Then
            ' Add the favorite to the cookie.
            MyFavorites.AddFavorite(Me.RefidLabel.Text)

            Me.FavoritesMultiView.SetActiveView(Me.RemoveFavoriteView)
            CType(Me.Master.FindControl("ViewFavoritesButton"), HyperLink).Visible = True

        Else
            Response.Redirect("NoCookies.aspx", False)
        End If
    End Sub

    Protected Sub RemoveFromFavoritesButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveFromFavoritesButton.Click
        ' Get the current Favorites for this user.
        ' Version 4.
        MyFavorites.RemoveFavorite(Me.RefidLabel.Text)
        Me.FavoritesMultiView.SetActiveView(Me.AddFavoriteView)
        If MyFavorites.GetFavorites.Count = 0 Then
            CType(Me.Master.FindControl("ViewFavoritesButton"), HyperLink).Visible = False
        End If

    End Sub

    Protected Sub FavoritesMultiView_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FavoritesMultiView.PreRender
        
    End Sub

End Class
