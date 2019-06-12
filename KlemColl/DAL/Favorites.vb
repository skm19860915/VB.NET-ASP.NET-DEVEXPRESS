Imports System.Web

Public Class Favorites

    Dim request As HttpRequest
    Dim response As HttpResponse

    Public Sub New(ByVal oRequest As HttpRequest, ByVal oResponse As HttpResponse)
        request = oRequest
        response = oResponse
    End Sub

    Public Function GetFavorites() As Collection
        Dim Favorites As New Collection

        If request.Cookies("Favorites") IsNot Nothing Then
            Dim arrFavorites() As String = HttpUtility.UrlDecode(request.Cookies("Favorites").Value).Split(CChar(","))
            For Each Favorite As String In arrFavorites
                If Favorite.Length > 0 AndAlso Favorites.Contains(Favorite) = False Then
                    Favorites.Add(Favorite, Favorite)
                End If
            Next
        End If
        Return Favorites
    End Function

    Private Sub SetFavorites(ByVal Favorites As Collection)
        Dim strFavorites As String = ""
        For Each Favorite As String In Favorites
            strFavorites &= Favorite & ","
        Next

        ' Encoding the string because Safari won't store some characters into session values.
        strFavorites = HttpUtility.UrlEncode(strFavorites)

        Dim FavoritesCookie As New HttpCookie("Favorites", strFavorites)
        'FavoritesCookie.Expires = CDate(Format(DateAdd(DateInterval.Year, 1, Now), "d"))
        If request.Cookies("Favorites") Is Nothing Then
            response.AppendCookie(FavoritesCookie)
        Else
            response.SetCookie(FavoritesCookie)
        End If
    End Sub

    Public Sub AddFavorite(ByVal RefId As String)
        Dim Favorites As Collection = GetFavorites()
        If Favorites.Contains(RefId) = False Then
            Favorites.Add(RefId, RefId)
        End If
        SetFavorites(Favorites)
    End Sub

    Public Sub RemoveFavorite(ByVal RefId As String)
        Dim Favorites As Collection = GetFavorites()
        If Favorites.Contains(RefId) = True Then
            Favorites.Remove(RefId)
            SetFavorites(Favorites)
        End If
    End Sub
End Class