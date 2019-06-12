Option Explicit On
Option Strict On

Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Partial Class Secure_ListOrders
    Inherits System.Web.UI.Page

    Protected Sub Label1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As Label = CType(sender, Label)
        If lbl.Text.Length >= 4 Then
            lbl.Text = Microsoft.VisualBasic.Right(lbl.Text, 4)
        End If
    End Sub

    Protected Sub DeleteAllButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteAllButton.Click
        Try
            Dim ordersTA As New DAL.KlemCollDataSetTableAdapters.OrdersTableAdapter()
            ordersTA.DeleteAllOrders()
            OrdersGridView.DataBind()
        Catch ex As Exception
            Response.Write("Error trying to delete all Orders: " & ex.Message)
        End Try
    End Sub

    Protected Sub OrdersGridView_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles OrdersGridView.RowDeleting
        Dim DelButton As Button = CType(OrdersGridView.Rows(e.RowIndex).FindControl("DeleteButton"), Button)
        Dim IDtoRemove As Integer = CInt(DelButton.CommandArgument)
        Try
            Dim ordersTA As New DAL.KlemCollDataSetTableAdapters.OrdersTableAdapter()
            ordersTA.DeleteOrder(IDtoRemove)
            OrdersGridView.DataBind()
        Catch ex As Exception
            Response.Write("Error trying to delete Order number " & IDtoRemove & ": " & ex.Message)
        End Try
    End Sub
End Class
