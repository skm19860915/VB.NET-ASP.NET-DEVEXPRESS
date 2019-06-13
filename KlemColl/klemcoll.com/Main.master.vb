Option Explicit On
Option Strict On

Imports Klemcoll.DAL

Public Class Main
    Inherits System.Web.UI.MasterPage

    Protected Sub KlemcollCss_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles KlemcollCss.PreRender
        Dim link As System.Web.UI.HtmlControls.HtmlLink = DirectCast(sender, System.Web.UI.HtmlControls.HtmlLink)
        link.Href = System.Web.HttpContext.Current.Request.ApplicationPath & link.Href
        link.Href = link.Href.Replace("//", "/")
    End Sub
    'Protected Sub OverviewOfTheCollectionButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OverviewOfTheCollectionButton.Click
    '    Response.Redirect("OverviewOfTheCollection.aspx", False)
    'End Sub

    'Protected Sub OurLeadingPhotographersButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OurLeadingPhotographersButton.Click
    '    Response.Redirect("OurLeadingPhotographers.aspx", False)
    'End Sub

    'Protected Sub PhotographsForCollectorsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhotographsForCollectorsButton.Click
    '    Response.Redirect("PhotographsForCollectors.aspx", False)
    'End Sub

    'Protected Sub TheGalleryButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TheGalleryButton.Click
    '    Response.Redirect("TheGallery.aspx", False)
    'End Sub

    'Protected Sub SpecialPublicationsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SpecialPublicationsButton.Click
    '    Response.Redirect("ListBooks.aspx", False)
    'End Sub

    'Protected Sub InfoForAuthorsPublishersButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles InfoForAuthorsPublishersButton.Click
    '    Response.Redirect("InfoForAuthorsPublishers.aspx", False)
    'End Sub

    'Protected Sub DownloadsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DownloadsButton.Click
    '    Response.Redirect("Downloads.aspx", False)
    'End Sub

    'Protected Sub ContactUsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContactUsButton.Click
    '    Response.Redirect("ContactUs.aspx", False)
    'End Sub

    'Protected Sub OtherInterestingWebsitesButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OtherInterestingWebsitesButton.Click
    '    Response.Redirect("OtherInterestingWebsites.aspx", False)
    'End Sub

    'Protected Sub ViewShoppingCartButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewShoppingCartButton.Click
    '    Response.Redirect("ViewCart.aspx", False)
    'End Sub

    'Protected Sub ViewFavoritesButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewFavoritesButton.Click
    '    Response.Redirect("ListImages.aspx?vf=1", False)
    'End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ' Check if this user has favorites.

        Dim MyFavorites As New Favorites(Request, Response)
        If MyFavorites.GetFavorites.Count = 0 Then
            Me.ViewFavoritesButton.Visible = False
        Else
            Me.ViewFavoritesButton.Visible = True
        End If
        'If Request.Cookies("Favorites") Is Nothing OrElse Request.Cookies("Favorites").Value.Length = 0 Then
        '    Me.ViewFavoritesButton.Visible = False
        'Else
        '    Me.ViewFavoritesButton.Visible = True
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ' Check if this user has any thing in their cart.
        Dim CartDS As CartDataSet = DirectCast(Session("CartDS"), CartDataSet)
        'Me.OverviewOfTheCollectionButton.CausesValidation = False
        'Me.OurLeadingPhotographersButton.CausesValidation = False
        'Me.PhotographsForCollectorsButton.CausesValidation = False
        'Me.TheGalleryButton.CausesValidation = False
        'Me.SpecialPublicationsButton.CausesValidation = False
        'Me.InfoForAuthorsPublishersButton.CausesValidation = False
        'Me.DownloadsButton.CausesValidation = False
        'Me.ContactUsButton.CausesValidation = False
        'Me.OtherInterestingWebsitesButton.CausesValidation = False
        'Me.ViewShoppingCartButton.CausesValidation = False
        'Me.ViewFavoritesButton.CausesValidation = False
        Dim CartTotal As Decimal = 0
        Dim WillQuote As Boolean = False

        If CartDS Is Nothing OrElse CartDS.SessionBooks.Rows.Count = 0 And CartDS.SessionImages.Rows.Count = 0 Then
            Me.ViewShoppingCartButton.Visible = False
            Me.CartImageButton.Visible = False
            Me.ViewCartHyperLink.Visible = False
        ElseIf CartDS.SessionBooks.Rows.Count > 0 Or CartDS.SessionImages.Rows.Count > 0 Then
            Me.ViewShoppingCartButton.Visible = True
            Me.CartImageButton.Visible = True
            Me.ViewCartHyperLink.Visible = True
            Dim CartCount As Integer = CartDS.SessionBooks.Rows.Count + CartDS.SessionImages.Rows.Count
            If CartDS.SessionBooks.Rows.Count > 0 AndAlso CDec(CartDS.SessionBooks.Compute("MIN(Subtotal)", "")) = 0 Then WillQuote = True
            If CartDS.SessionImages.Rows.Count > 0 AndAlso CDec(CartDS.SessionImages.Compute("MIN(Subtotal)", "")) = 0 Then WillQuote = True
            Dim BooksTotal As Object = CartDS.SessionImages.Compute("SUM(SubTotal)", "")
            Dim ImagesTotal As Object = CartDS.SessionBooks.Compute("SUM(SubTotal)", "")
            If IsDBNull(BooksTotal) = False Then
                CartTotal = CDec(BooksTotal)
            End If
            If IsDBNull(ImagesTotal) = False Then
                CartTotal += CDec(ImagesTotal)
            End If
            Dim Plural As String = ""
            If CartCount > 1 Then Plural = "s"
            If WillQuote Then
                Me.ViewCartHyperLink.Text = String.Format("You have {0} item{1}<br />", CartCount, Plural)
            Else
                Me.ViewCartHyperLink.Text = String.Format("You have {0} item{1}<br />{2}", CartCount, Plural, Format(CartTotal, "c"))
            End If

        End If

    End Sub
End Class

