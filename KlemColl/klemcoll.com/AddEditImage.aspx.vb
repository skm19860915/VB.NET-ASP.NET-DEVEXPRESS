Option Explicit On
Option Strict On

Imports Klemcoll.DAL.CartDataSetTableAdapters
Imports Klemcoll.DAL

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If IsNumeric(Me.qty.Text) = False Then
            Me.StatusLabel.Text = "Please enter a valid quantity"
        Else
            ' Save the form information to an object in a session variable.
            Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)
            ' Check if this item is already in memory or not.
            Dim ImageDR As CartDataSet.SessionImagesRow

            If CartDS Is Nothing Then
                ' No cart is stored in the session.
                CartDS = New CartDataSet
                ImageDR = CartDS.SessionImages.NewSessionImagesRow
            ElseIf Request.Params("ImagesId") IsNot Nothing Then
                ImageDR = CartDS.SessionImages.FindBySessionImagesID(CInt(Request("ImagesID")))
            Else
                CartDS.SessionImages.DefaultView.RowFilter = String.Format("(SessionID = '{0}' and RefId = '{1}' and PaperSize = '{2}' and PaperType = '{3}' and Border = '{4}')", _
                    Session.SessionID, Me.ref.Text, Me.size.SelectedValue, Request("papertype"), Me.Border.SelectedValue)
                If CartDS.SessionImages.DefaultView.Count > 0 Then
                    ' It already exists
                    ImageDR = DirectCast(CartDS.SessionImages.DefaultView.Item(0).Row, CartDataSet.SessionImagesRow)
                Else
                    ImageDR = CartDS.SessionImages.NewSessionImagesRow
                End If
                CartDS.SessionImages.DefaultView.RowFilter = ""
                ' Throw error?
            End If


            ' Cart dataset loaded, assign the record if it exists.
            'ImageDR = CartDS.SessionImages.FindByRefid(Me.ref.Text)
            'If ImageDR Is Nothing Then ImageDR = CartDS.SessionImages.NewSessionImagesRow

            ' Fill the newly assigned row.
            ImageDR.Refid = Me.ref.Text
            ImageDR.SessionID = Session.SessionID
            ImageDR.PaperSize = Me.size.SelectedValue
            'ImageDR.PaperType = Request("papertype")
            ImageDR.PaperType = Request("PaperTypeDropDown")
            'ImageDR.Color = CBool(Me.finish.SelectedIndex)
            ImageDR.Border = Me.Border.SelectedValue
            ImageDR.Memo = Me.MemoTextBox.Text
            ImageDR.Quantity = CInt(Me.qty.Text)
            ImageDR.Toning = Me.Toning.SelectedValue

            If IsNumeric(Me.Price.Text) And IsNumeric(Me.qty.Text) Then
                ' Add the price divide by quantity because the price in the textbox reflects the current quantity
                ImageDR.Price = CDec(Me.Price.Text) / CDec(Me.qty.Text)
            Else
                ImageDR.Price = 0
            End If

            ' Add the row to the datatable if its not attached.
            If ImageDR.RowState = Data.DataRowState.Detached Then
                CartDS.SessionImages.AddSessionImagesRow(ImageDR)
            End If

            ' Save the dataset into the session variable.
            Session("CartDS") = CartDS

            Dim SessionImagesTA As New SessionImagesTableAdapter
            Dim SessionBooksTA As New SessionBooksTableAdapter
            Dim Updated As Boolean = False

            SessionImagesTA.DeleteBySessionID(Session.SessionID)
            SessionBooksTA.DeleteBySessionID(Session.SessionID)

            CartDS.AcceptChanges()

            If CartDS.SessionImages IsNot Nothing AndAlso CartDS.SessionImages.Rows.Count > 0 Then
                For Each dr As Data.DataRow In CartDS.SessionImages.Rows
                    dr.SetAdded()
                Next
                SessionImagesTA.Update(CartDS.SessionImages)
                Updated = True
            End If

            Response.Redirect("ViewCart.aspx", False)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            'Dim RefidHiddenField As HiddenField = CType(PreviousPage.Master.FindControl("ContentPlaceHolder1").FindControl("RefidHiddenField"), HiddenField)
            Dim Refid As String = Request.QueryString("refid")
            Dim ImagesID As Integer
            If Request.Params("ImagesID") IsNot Nothing Then
                ImagesID = CInt(Request("ImagesID"))
            End If
            Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)
            Dim ImageDR As CartDataSet.SessionImagesRow

            ' Only pull info from cart if id was passed and NOT refid
            If Not ImagesID = 0 And Refid Is Nothing Then
                'If Request("refid").ToLower.StartsWith("c") = True Then
                '    'Me.finish.SelectedIndex = 1
                'End If
                ' Check if this item is in the Cart already.
                Dim PaperType As String = ""
                Dim Border As String = ""
                Dim Quantity As Integer = 1
                Dim Memo As String = ""
                Dim Toning As String = ""
                If CartDS IsNot Nothing Then
                    ' Cart found in memory, lookup the item.
                    'Me.SaveButton.Text = "Save Changes"
                    ImageDR = CartDS.SessionImages.FindBySessionImagesID(ImagesID)
                    If ImageDR IsNot Nothing Then
                        ' Item found in the cart, load its values.
                        Me.size.SelectedValue = ImageDR.PaperSize
                        ' Known issue: Can't select the correct paper type because the javascript is controlling this.
                        Me.PaperTypeDropDown.SelectedValue = ImageDR.PaperType
                        Me.SelectedPaperType.Value = ImageDR.PaperType
                        Me.Border.SelectedValue = ImageDR.Border
                        Me.qty.Text = ImageDR.Quantity.ToString
                        Me.MemoTextBox.Text = ImageDR.Memo
                        Me.Toning.SelectedValue = ImageDR.Toning
                        Me.ref.Text = ImageDR.Refid.ToString
                        Refid = ImageDR.Refid
                    End If
                End If

                'Dim ImagesTA As New KlemCollDataSetTableAdapters.ImagesTableAdapter
                'Dim ImagesDT As KlemCollDataSet.ImagesDataTable = ImagesTA.GetByRefid(Refid)
                'If ImagesDT.Rows.Count > 0 Then
                Me.PaperTypeDropDown.SelectedValue = PaperType
                'End If
            End If

            Me.ref.Text = Refid
        End If
        'ClientScript.RegisterStartupScript("PriceScript", "PrintPricing.js")
        'Me.PaperSizeDropDownList.Attributes.Add("onChange", "size_pricing()")

    End Sub

    Protected Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        If Request("return") IsNot Nothing Then
            Response.Redirect(GetRouteUrl("Image", New With {.Refid = Request("refid")}), False)
        Else
            Response.Redirect("PhotographsForCollectors.aspx", False)
        End If
    End Sub

    Protected Sub SaveButton_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.PreRender
        If Me.SelectedPaperType.Value.Length > 0 Then
            Me.SaveButton.Text = "Save Changes"
        End If
    End Sub
End Class
