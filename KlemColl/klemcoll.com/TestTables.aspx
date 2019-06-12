<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.TestTables" Codebehind="TestTables.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 600px">
            <tr>
                <td style="width: 198px">
                    Image Reference Number</td>
                <td>
                    refid</td>
                <td style="width: 54px; text-align: center">
                    Quantity</td>
                <td style="width: 47px; text-align: center">
                    Each</td>
                <td style="width: 47px; text-align: center">
                    Price</td>
            </tr>
            <tr>
                <td style="width: 198px">
                    Paper Size</td>
                <td>
                    papersize</td>
                <td style="width: 54px">
                    quantity</td>
                <td style="width: 47px">
                    price</td>
                <td style="width: 47px">
                    subtotal</td>
            </tr>
            <tr>
                <td style="width: 198px">
                    B&amp;W or Color</td>
                <td>
                    color</td>
                <td style="width: 54px">
                </td>
                <td style="width: 47px">
                </td>
                <td style="width: 47px">
                </td>
            </tr>
            <tr>
                <td style="width: 198px">
                    Paper Type</td>
                <td>
                    papertype</td>
                <td style="width: 54px">
                </td>
                <td style="width: 47px">
                </td>
                <td style="width: 47px">
                </td>
            </tr>
            <tr>
                <td style="width: 198px">
                    Border</td>
                <td>
                    border</td>
                <td style="width: 54px">
                </td>
                <td style="width: 47px">
                </td>
                <td style="width: 47px">
                </td>
            </tr>
            <tr>
                <td style="width: 198px">
                    Special Instructions / Memo</td>
                <td>
                    Memo</td>
                <td style="width: 54px">
                </td>
                <td style="width: 47px">
                </td>
                <td style="width: 47px">
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 600px">
            <tr>
                <td style="width: 198px">
                    Title</td>
                <td>
                    <%'#DataBinder.Eval(Container.DataItem, "Name")%></td>
                <td style="width: 54px; text-align: center">
                    Quantity</td>
                <td style="width: 47px; text-align: center">
                    Each</td>
                <td style="width: 47px; text-align: center">
                    Price</td>
            </tr>
            <tr>
                <td style="width: 198px">
                    Author</td>
                <td>
                    <%'#DataBinder.Eval(Container.DataItem, "Author")%></td>
                <td style="width: 54px">
                    quantity</td>
                <td style="width: 47px">
                    price</td>
                <td style="width: 47px">
                    subtotal</td>
            </tr>
        </table>
    
    </div>
        <br />
        <table style="width: 600px">
            <tr>
                <td colspan="4" style="text-align: right">
                    Subtotal =</td>
                <td style="width: 47px; text-align: left">
                    subtotal</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right">
                    Shipping =</td>
                <td style="width: 47px; text-align: left">
                    shipping</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right">
                    Sales Tax =</td>
                <td style="width: 47px; text-align: left">
                    salestax</td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    <hr color="#000000" />
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold; text-align: right;" colspan="4">
                    Total =</td>
                <td style="width: 47px; text-align: left;">
                    total</td>
            </tr>
        </table>
    </form>
</body>
</html>
