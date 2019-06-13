<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.Secure_ListOrders" MaintainScrollPositionOnPostback="true" Codebehind="ListOrders.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List All Orders</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="DeleteAllButton" runat="server" 
            onclientclick="return confirm('Are you really sure you want to DELETE ALL ORDERS? This can only be undone by restoring the database from a previous backup.');" 
            Text="Delete All Orders" />
        <br />
        <br />
    
        <asp:GridView ID="OrdersGridView" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="OrderID" 
            DataSourceID="OrdersObjectDataSource" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="OrderID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:TemplateField HeaderText="Card Last 4" SortExpression="CCNumber">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CCNumber") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" onprerender="Label1_PreRender" 
                            Text='<%# Bind("CCNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Subtotal" DataFormatString="{0:c}" 
                    HeaderText="Sub Total" SortExpression="Subtotal" />
                <asp:BoundField DataField="Shipping" DataFormatString="{0:c}" 
                    HeaderText="Shipping" SortExpression="Shipping" />
                <asp:BoundField DataField="GrandTotal" DataFormatString="{0:c}" 
                    HeaderText="Grand Total" SortExpression="GrandTotal" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="DetailsButton" runat="server" 
                            PostBackUrl='<%# Eval("OrderID", "ViewOrder.aspx?id={0}") %>' Text="View" 
                            Width="55px" />
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("OrderID") %>' 
                            onclientclick="return confirm('Are you sure you want to delete this order?');" 
                            Text="Delete" Width="55px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                There are no orders in the system.
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:ObjectDataSource ID="OrdersObjectDataSource" runat="server" 
            DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" 
            SelectMethod="GetData" 
            TypeName="Klemcoll.DAL.KlemCollDataSetTableAdapters.OrdersTableAdapter">
            <DeleteParameters>
                <asp:Parameter Name="Original_OrderID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
