Option Explicit On
Option Strict On

Imports Klemcoll.DAL

Partial Class ShowBook
    Inherits System.Web.UI.Page

    Protected Sub AddCartButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Page.RouteData.Values("id") IsNot Nothing Then
            Dim BookID As Integer = CInt(Page.RouteData.Values("id"))

            ' Add the book to the Cart.
            Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)

            If CartDS Is Nothing Then
                CartDS = New CartDataSet
            End If

            Dim SendingButton As Button = CType(sender, Button)
            Dim Layout As String = SendingButton.ID.Substring(0, 4)

            ' Lookup the book by its ID to see if we need to add a new book or increment the quantity.
            Dim BookDR As CartDataSet.SessionBooksRow = CartDS.SessionBooks.FindByBooksID(BookID)
            If BookDR Is Nothing Then
                BookDR = CartDS.SessionBooks.NewSessionBooksRow
                BookDR.Name = CType(Me.BookFormView.FindControl(Layout & "NameLabel"), Label).Text
                BookDR.Author = CType(Me.BookFormView.FindControl("AuthorHiddenField"), HiddenField).Value
                BookDR.Weight = CDec(CType(Me.BookFormView.FindControl("WeightHiddenField"), HiddenField).Value)
                BookDR.Description = CType(Me.BookFormView.FindControl(Layout & "DescLabel"), Label).Text
                BookDR.Price = CDec(CType(Me.BookFormView.FindControl(Layout & "PriceLabel"), Label).Text.Replace("$", ""))
                BookDR.Quantity = CInt(CType(Me.BookFormView.FindControl(Layout & "QuantityTextBox"), TextBox).Text)
                BookDR.BooksID = CInt(CType(Me.BookFormView.FindControl("BookIDHiddenField"), HiddenField).Value)
                BookDR.SessionID = Session.SessionID
                CartDS.SessionBooks.AddSessionBooksRow(BookDR)
            Else
                BookDR.Quantity = CInt(CType(Me.BookFormView.FindControl(Layout & "QuantityTextBox"), TextBox).Text)
            End If

            Session("CartDS") = CartDS
            Response.Redirect("~/ViewCart.aspx", False)
        End If

        

    End Sub

    Protected Sub PopulateQuantity(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)
        Dim tbxQty As TextBox = DirectCast(sender, TextBox)
        If CartDS IsNot Nothing And Request("id") IsNot Nothing Then
            ' A cart exists and a book was kpassed
            If CartDS.SessionBooks.Rows.Count > 0 Then
                ' There are books in the current order
                Dim currentBook As CartDataSet.SessionBooksRow = CartDS.SessionBooks.FindByBooksID(CInt(Request("id")))
                If currentBook IsNot Nothing Then
                    ' The book exists in the order, populate current book quantity
                    tbxQty.Text = currentBook.Quantity.ToString
                End If
            End If
        End If
        If tbxQty.Text.Length = 0 Then
            tbxQty.Text = "1"
        End If
    End Sub
End Class
