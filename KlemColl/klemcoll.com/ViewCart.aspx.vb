Option Explicit On
Option Strict On

Imports Klemcoll.DAL
Imports Klemcoll.DAL.CartDataSetTableAdapters

Partial Class ViewCart
    Inherits System.Web.UI.Page

    Dim CartDS As CartDataSet

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        CartDS = CType(Session("CartDS"), CartDataSet)
        Dim BooksPrice As Decimal = 0
        Dim PrintsPrice As Decimal = 0
        Dim WillQuote As Boolean = False ' show the text "Will Quote" for subtotal?

        If CartDS IsNot Nothing Then
            If CartDS.SessionImages.Rows.Count > 0 Then
                If Not Page.IsPostBack Then
                    Me.ImagesDataList.DataSource = CartDS.SessionImages
                    Me.ImagesDataList.DataBind()
                End If
                PrintsPrice = CDec(CartDS.SessionImages.Compute("Sum(SubTotal)", ""))
                ' If one of the sobtotals is 0 then the item is a "Will Quote" item
                WillQuote = CDec(CartDS.SessionImages.Compute("Min(SubTotal)", "")) = 0
            End If
            If CartDS.SessionBooks.Rows.Count > 0 Then
                If Not Page.IsPostBack Then
                    Me.BooksDataList.DataSource = CartDS.SessionBooks
                    Me.BooksDataList.DataBind()
                End If
                BooksPrice = CDec(CartDS.SessionBooks.Compute("Sum(SubTotal)", ""))
                ' If one of the sobtotals is 0 then the item is a "Will Quote" item
                WillQuote = CDec(CartDS.SessionBooks.Compute("min(Subtotal)", "")) = 0
            End If
        End If

        If CartDS IsNot Nothing AndAlso CartDS.SessionBooks IsNot Nothing Then
            If CartDS.SessionBooks.Rows.Count = 0 And CartDS.SessionImages.Rows.Count = 0 Then
                Me.NoItemsLabel.Visible = True
                Me.CheckoutButton.Visible = False
            Else
                Me.NoItemsLabel.Visible = False
                Me.CheckoutButton.Visible = True
            End If
        Else
            Me.NoItemsLabel.Visible = True
            Me.CheckoutButton.Visible = False
        End If
        

        Dim Subtotal As Decimal = BooksPrice + PrintsPrice
        If WillQuote Then
            Me.SubtotalLabel.Text = "Will Quote"
        Else
            Me.SubtotalLabel.Text = Format(Subtotal, "c")
        End If


        'For Each str As String In Request.Form
        '    If str.Contains("TextBox") = True Then
        '        MsgBox(str)
        '    End If
        'Next
    End Sub


    Protected Sub CheckoutButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckoutButton.Click
        ' Save the cart information to the session tables and redirect to the secure area.
        CartDS = CType(Session("CartDS"), CartDataSet)
        Dim SessionImagesTA As New SessionImagesTableAdapter
        Dim SessionBooksTA As New SessionBooksTableAdapter
        Dim Updated As Boolean = False

        SessionImagesTA.DeleteBySessionID(Session.SessionID)
        SessionBooksTA.DeleteBySessionID(Session.SessionID)

        CartDS.AcceptChanges()

        If CartDS.SessionBooks IsNot Nothing AndAlso CartDS.SessionBooks.Rows.Count > 0 Then
            For Each dr As Data.DataRow In CartDS.SessionBooks.Rows
                dr.SetAdded()
            Next
            SessionBooksTA.Update(CartDS.SessionBooks)
            Updated = True
        End If

        If CartDS.SessionImages IsNot Nothing AndAlso CartDS.SessionImages.Rows.Count > 0 Then
            For Each dr As Data.DataRow In CartDS.SessionImages.Rows
                dr.SetAdded()
            Next
            SessionImagesTA.Update(CartDS.SessionImages)
            Updated = True
        End If

        If Updated = True Then
            'Response.Redirect(ConfigurationManager.AppSettings("SecureBaseAddress").ToString() _
            '& IIf(ConfigurationManager.AppSettings("SecureBaseAddress").ToString().EndsWith("/"), "", "/").ToString() _
            '& "OrderForm.aspx?id=" & Session.SessionID, False)

            Response.Redirect("OrderForm.aspx?id=" & Session.SessionID, False)

            'Response.Redirect("https://buysecured.net/KlemColl.com/OrderForm.aspx?id=" & Session.SessionID, False)
        End If

    End Sub

    Protected Sub RemoveBookButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim BookID As Integer = CInt(e.CommandArgument)

        If BookID > 0 Then
            Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)
            Dim BookToRemove As CartDataSet.SessionBooksRow = CartDS.SessionBooks.FindByBooksID(BookID)
            CartDS.SessionBooks.RemoveSessionBooksRow(BookToRemove)
            Session("CartDS") = CartDS
            If CartDS.SessionBooks.Rows.Count = 0 Then
                Me.BooksDataList.Visible = False
            Else
                Me.BooksDataList.DataSource = CartDS.SessionBooks
                Me.BooksDataList.DataBind()
            End If
            If CartDS.SessionImages.Rows.Count = 0 And CartDS.SessionBooks.Rows.Count = 0 Then
                Me.TotalRow.Visible = False
            Else
                Me.TotalRow.Visible = True
            End If
        End If

    End Sub

    Protected Sub RemoveItemButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim ImagesID As Integer = CInt(e.CommandArgument)
        Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)

        Dim ImageToRemove As CartDataSet.SessionImagesRow = CartDS.SessionImages.FindBySessionImagesID(ImagesID)
        CartDS.SessionImages.RemoveSessionImagesRow(ImageToRemove)
        Session("CartDS") = CartDS
        If CartDS.SessionImages.Rows.Count = 0 Then
            Me.ImagesDataList.Visible = False
        Else
            Me.ImagesDataList.DataSource = CartDS.SessionImages
            Me.ImagesDataList.DataBind()
        End If
        If CartDS.SessionImages.Rows.Count = 0 And CartDS.SessionBooks.Rows.Count = 0 Then
            Me.TotalRow.Visible = False
        Else
            Me.TotalRow.Visible = True
        End If
    End Sub

    'Protected Sub ImagesDataList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ImagesDataList.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        For Each oControl As Control In e.Item.Controls
    '            If oControl.ClientID.Contains("PriceLabel") Then
    '                Dim PriceLabel As Label = CType(oControl, Label)
    '                If PriceLabel.Text.StartsWith("0") Then
    '                    PriceLabel.Text = "Will Quote"
    '                Else
    '                    PriceLabel.Text = Format(CDec(PriceLabel.Text), "c")
    '                End If
    '            ElseIf oControl.ClientID.Contains("MemoLabel") Then
    '                Dim MemoLabel As Label = CType(oControl, Label)
    '                If MemoLabel.Text.Length > 50 Then
    '                    MemoLabel.Text = MemoLabel.Text.Substring(0, 50) & "..."
    '                End If
    '            End If
    '        Next
    '    End If
    'End Sub

    Protected Sub PriceLabel_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim PriceLabel As Label = CType(sender, Label)
        If PriceLabel.Text = "0" Then
            PriceLabel.Text = "Will Quote"
        ElseIf IsNumeric(PriceLabel.Text) Then
            PriceLabel.Text = Format(CDec(PriceLabel.Text), "c")
        End If
    End Sub

    Protected Sub MemoLabel_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MemoLabel As Label = CType(sender, Label)
        If MemoLabel.Text.Length > 50 Then
            MemoLabel.Text = MemoLabel.Text.Substring(0, 50) & "..."
        End If
    End Sub

End Class
