Option Explicit On
Option Strict On

Imports Klemcoll.DAL
Imports Klemcoll.DAL.KlemCollDataSetTableAdapters

Partial Class ShowOrder
    Inherits System.Web.UI.UserControl

    Const CTSalesTax As Decimal = CDec(0.0635)

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Request("id") IsNot Nothing Then
            ' Load a specific order.
            Dim OrderID As Integer = CInt(Request("id"))

            Dim OrdersTA As New OrdersTableAdapter
            Dim OrderedBooksTA As New OrderedBooksTableAdapter
            Dim OrderedImagesTA As New OrderedImagesTableAdapter
            Dim OrderedImagesDT As KlemCollDataSet.OrderedImagesDataTable = OrderedImagesTA.GetDataByOrderID(OrderID)

            Dim OrdersDT As KlemCollDataSet.OrdersDataTable = OrdersTA.GetDataByOrderID(OrderID)
            Dim OrderedBooksDT As KlemCollDataSet.OrderedBooksDataTable = OrderedBooksTA.GetDataByOrderID(OrderID)

            If OrdersDT.Rows.Count > 0 Then
                ' Order Found.
                Dim OrderDR As KlemCollDataSet.OrdersRow = OrdersDT(0)

                Me.CustomerNameLabel.Text = OrderDR.Name
                Me.MailingAddress1Label.Text = OrderDR.Address
                Me.MailingCityStateCountryLabel.Text = OrderDR.City & " " & OrderDR.StateProv & " " & OrderDR.ZipPostal & " " & OrderDR.Country

                If Not OrderDR.IsPhoneNull Then
                    Me.MailingPhoneLabel.Text = OrderDR.Phone
                End If

                Me.CustomerEmailLabel.Text = OrderDR.email
                Me.ShippingAddress1Label.Text = OrderDR.ShipAddress
                Me.ShippingCityStateCountryLabel.Text = OrderDR.ShipCity & " " & OrderDR.ShipStateProv & " " & OrderDR.ShipZipPostal & " " & OrderDR.ShipCountry

                ' Fill the Payment Type.
                If OrderDR.IsCCNumberNull = False Then
                    Me.PaymentMethodLabel.Text = "<B>Payment Type:</B> Credit Card<BR/>" & _
                        "Card Number: " & OrderDR.CCNumber & "<BR/>" & _
                        "Exp Date: " & OrderDR.CCExpDate & "<BR/>" & _
                        "Name on Card: " & OrderDR.NameOnCard & "<BR/>" & _
                        "CVV: " & OrderDR.CVV & "<BR/>"
                Else
                    Me.PaymentMethodLabel.Text = "<B>Payment Type:</B> PayPal"
                End If

                ' Fill the shipping method.
                Me.ShipMethodLabel.Text = "<B>Shipping Method:</B> " & OrderDR.ShippingMethod

                Me.ImagesRepeater.DataSource = OrderedImagesDT
                Me.BooksRepeater.DataSource = OrderedBooksDT
                Me.DataBind()

                Dim TotalPrice As Decimal = 0
                Dim SubTotalPrice As Decimal = 0
                Dim SalesTax As Decimal = 0
                Dim ShippingPrice As Decimal = 0
                Dim PrintsTotal As Decimal = 0
                Dim BooksTotal As Decimal = 0

                If OrderedBooksDT.Rows.Count > 0 Then
                    BooksTotal = CDec(OrderedBooksDT.Compute("Sum(SubTotal)", ""))
                End If
                If OrderedImagesDT.Rows.Count > 0 Then
                    PrintsTotal = CDec(OrderedImagesDT.Compute("Sum(SubTotal)", ""))
                End If

                SubTotalPrice = BooksTotal + PrintsTotal

                
                ShippingPrice = OrderDR.Shipping

                ' In CT, tax is calculated w/shipping included
                If OrderDR.ShipStateProv.ToLower = "ct" Then
                    SalesTax = (SubTotalPrice + ShippingPrice) * CTSalesTax
                Else
                    SalesTax = 0
                End If

                TotalPrice = SubTotalPrice + ShippingPrice + SalesTax

                For Each BookControl As Control In Me.BooksRepeater.Controls
                    For Each oControl As Control In BookControl.Controls
                        If oControl.ClientID.Contains("TotalLabel") Then
                            CType(oControl, Label).Text = CheckForZero(TotalPrice)
                        ElseIf oControl.ClientID.Contains("SubtotalLabel") Then
                            CType(oControl, Label).Text = CheckForZero(SubTotalPrice)
                        ElseIf oControl.ClientID.Contains("ShippingLabel") Then
                            CType(oControl, Label).Text = CheckForZero(ShippingPrice)
                        ElseIf oControl.ClientID.Contains("SalesTaxLabel") Then
                            CType(oControl, Label).Text = Format(SalesTax, "c")
                        End If
                    Next
                Next
            End If


        Else
            ' Load information from form variables.
            Dim FirstName As String = "", MI As String = "", LastName As String = "", Email As String = "", Organization As String = "", MailingDayPhone As String = "", MailingFax As String = ""
            Dim MailingAddress1 As String = "", MailingAddress2 As String = "", MailingCity As String = "", MailingState As String = "", MailingZip As String = "", MailingCountry As String = ""
            Dim ShippingAddress1 As String = "", ShippingAddress2 As String = "", ShippingCity As String = "", ShippingState As String = "", ShippingZip As String = "", ShippingCountry As String = "", ShippingDayPhone As String = "", ShippingFax As String = ""
            Dim PaymentType As String = "", CCNumber As String = "", CCExpDate As String = "", NameOnCard As String = "", CVV As String = "", MailingCounty As String = "", ShippingCounty As String = ""
            Dim ShippingPrice As Decimal = 0, ShippingMethod As String = "", PayPalPhone As String = Nothing

            For Each str As String In Request.Form
                If str.Contains("FirstName") Then
                    FirstName = Request.Form(str)
                    Session("FirstName") = FirstName
                ElseIf str.Contains("LastName") Then
                    LastName = Request(str)
                    Session("LastName") = LastName
                ElseIf str.Contains("MiddleInitial") Then
                    MI = " " & Request(str) & " "
                    Session("MI") = MI
                ElseIf str.Contains("MailingAddress1") Then
                    MailingAddress1 = Request(str)
                    Session("MailingAddress1") = MailingAddress1
                ElseIf str.Contains("MailingAddress2") Then
                    MailingAddress2 = Request(str)
                    Session("MailingAddress2") = MailingAddress2
                ElseIf str.Contains("MailingCity") Then
                    MailingCity = Request(str)
                    Session("MailingCity") = MailingCity
                ElseIf str.Contains("MailingState") Then
                    MailingState = Request(str)
                    Session("MailingState") = MailingState
                ElseIf str.Contains("MailingZip") Then
                    MailingZip = Request(str)
                    Session("MailingZip") = MailingZip
                ElseIf str.Contains("MailingCountry") Then
                    MailingCountry = Request(str)
                    Session("MailingCountry") = MailingCountry
                ElseIf str.Contains("ShippingAddress1") Then
                    ShippingAddress1 = Request(str)
                    Session("ShippingAddress1") = ShippingAddress1
                ElseIf str.Contains("ShippingAddress2") Then
                    ShippingAddress2 = Request(str)
                    Session("ShippingAddress2") = ShippingAddress2
                ElseIf str.Contains("ShippingCity") Then
                    ShippingCity = Request(str)
                    Session("ShippingCity") = ShippingCity
                ElseIf str.Contains("ShippingState") Then
                    ShippingState = Request(str)
                    Session("ShippingState") = ShippingState
                ElseIf str.Contains("ShippingZip") Then
                    ShippingZip = Request(str)
                    Session("ShippingZip") = ShippingZip
                ElseIf str.Contains("ShippingCountry") Then
                    ShippingCountry = Request(str)
                    Session("ShippingCountry") = ShippingCountry
                ElseIf str.Contains("PaymentType") Then
                    PaymentType = Request(str)
                    Session("PaymentType") = PaymentType
                ElseIf str.Contains("CCNumber") Then
                    CCNumber = Request(str)
                    Session("CCNumber") = CCNumber
                ElseIf str.Contains("ExpDate") Then
                    CCExpDate = Request(str)
                    Session("CCExpDate") = CCExpDate
                ElseIf str.Contains("NameOnCard") Then
                    NameOnCard = Request(str)
                    Session("NameOnCard") = NameOnCard
                ElseIf str.Contains("CVV") Then
                    CVV = Request(str)
                    Session("CVV") = CVV
                ElseIf str.Contains("Email") Then
                    Email = Request(str)
                    Session("Email") = Email
                ElseIf str.Contains("Organization") Then
                    Organization = Request(str)
                    Session("Organization") = Organization
                ElseIf str.Contains("MailingDayPhone") Then
                    MailingDayPhone = Request(str)
                    Session("MailingDayPhone") = MailingDayPhone
                ElseIf str.Contains("MailingDayPhone2") Then
                    PayPalPhone = Request(str)
                    Session("PayPalPhone") = PayPalPhone
                ElseIf str.Contains("MailingFax") Then
                    MailingFax = Request(str)
                    Session("MailingFax") = MailingFax
                ElseIf str.Contains("MailingCounty") Then
                    MailingCounty = Request(str)
                    Session("MailingCounty") = MailingCounty
                ElseIf str.Contains("ShippingCounty") Then
                    ShippingCounty = Request(str)
                    Session("ShippingCounty") = ShippingCounty
                    'ElseIf str.Contains("ShippingDayPhone") Then
                    '    ShippingDayPhone = Request(str)
                    '    Session("ShippingDayPhone") = ShippingDayPhone
                    'ElseIf str.Contains("ShippingFax") Then
                    '    ShippingFax = Request(str)
                ElseIf str.Contains("ShippingMethod") Then
                    If Request(str).Contains("$") Then
                        ' Parse out the shipping method and price.
                        Dim DelimPos As Integer
                        DelimPos = Request(str).LastIndexOf("$")
                        ShippingMethod = Request(str).Substring(0, DelimPos).Replace("(", "").Trim()
                        ShippingPrice = CDec(ParseNumber(Request(str).Substring(DelimPos + 1)))
                        Session("ShippingMethod") = ShippingMethod
                        Session("ShippingPrice") = ShippingPrice
                    End If
                End If
            Next

            Me.CustomerNameLabel.Text = FirstName & MI & LastName
            If Organization <> "" Then Me.CompanyLabel.Text = "<br />" & Organization
            Me.MailingAddress1Label.Text = MailingAddress1 & "<br />" & MailingAddress2
            If MailingState.ToLower <> "not specified" Then
                ' Domestic address.
                Me.MailingCityStateCountryLabel.Text = MailingCity & " " & MailingState & " " & MailingZip & " " & MailingCountry
            Else
                ' International.
                Me.MailingCityStateCountryLabel.Text = MailingCity & " " & MailingZip & " " & MailingCountry
            End If
            Me.MailingPhoneLabel.Text = MailingDayPhone
            If MailingFax <> "" Then Me.MailingFaxLabel.Text = "<br />" & MailingFax
            Me.CustomerEmailLabel.Text = Email

            Me.ShippingAddress1Label.Text = ShippingAddress1
            Me.ShippingAddress2Label.Text = ShippingAddress2
            If ShippingState.ToLower <> "not specified" Then
                ' Domestic address.
                Me.ShippingCityStateCountryLabel.Text = ShippingCity & " " & ShippingState & " " & ShippingZip & " " & ShippingCountry
            Else
                ' International.
                Me.ShippingCityStateCountryLabel.Text = ShippingCity & " " & ShippingZip & " " & ShippingCountry
            End If

            ' Fill the Payment Type.
            If CCNumber.Length > 0 Then
                Me.PaymentMethodLabel.Text = "<B>Payment Type:</B> Credit Card"
            Else
                Me.PaymentMethodLabel.Text = "<B>Payment Type:</B> PayPal"
            End If

            ' Fill the shipping method.
            Me.ShipMethodLabel.Text = "<B>Shipping Method:</B> " & ShippingMethod

            Dim CartDS As CartDataSet = CType(Session("CartDS"), CartDataSet)

            Me.ImagesRepeater.DataSource = CartDS.SessionImages
            Me.BooksRepeater.DataSource = CartDS.SessionBooks

            Me.DataBind()

            Dim TotalPrice As Decimal = 0
            Dim SubTotalPrice As Decimal = 0
            Dim SalesTax As Decimal = 0
            Dim PrintsTotal As Decimal = 0
            Dim BooksTotal As Decimal = 0

            If CartDS.SessionBooks.Rows.Count > 0 Then
                BooksTotal = CDec(CartDS.SessionBooks.Compute("Sum(SubTotal)", ""))
            End If
            If CartDS.SessionImages.Rows.Count > 0 Then
                PrintsTotal = CDec(CartDS.SessionImages.Compute("Sum(SubTotal)", ""))
            End If

            SubTotalPrice = BooksTotal + PrintsTotal
            Session("SubTotal") = SubTotalPrice

            Session("SalesTax") = SalesTax

            ' In CT, shipping is calculated w/shipping included
            If ShippingState.ToLower = "ct" Then
                SalesTax = (SubTotalPrice + ShippingPrice) * CTSalesTax
            Else
                SalesTax = 0
            End If

            TotalPrice = SubTotalPrice + ShippingPrice + SalesTax
            If Session("GrandTotal") Is Nothing Then
                Session("GrandTotal") = TotalPrice
            End If

            For Each BookControl As Control In Me.BooksRepeater.Controls
                For Each oControl As Control In BookControl.Controls
                    If oControl.ClientID.Contains("TotalLabel") Then
                        CType(oControl, Label).Text = CheckForZero(TotalPrice)
                    ElseIf oControl.ClientID.Contains("SubtotalLabel") Then
                        CType(oControl, Label).Text = CheckForZero(SubTotalPrice)
                    ElseIf oControl.ClientID.Contains("ShippingLabel") Then
                        CType(oControl, Label).Text = CheckForZero(ShippingPrice)
                    ElseIf oControl.ClientID.Contains("SalesTaxLabel") Then
                        CType(oControl, Label).Text = Format(SalesTax, "c")
                    End If
                Next
            Next

        End If


    End Sub

    Private Function ParseNumber(Input As String) As String
        Dim returnString As String = ""
        For Each c As Char In Input.ToCharArray()
            If Char.IsDigit(c) Or c.ToString() = "." Then
                returnString += c.ToString()
            Else
                Return returnString
            End If
        Next
        Return returnString
    End Function

    Private Function CheckForZero(ByVal PriceToCheck As Decimal) As String
        If PriceToCheck > 0 Then
            Return Format(PriceToCheck, "c")
        Else
            Return "Will Quote"
        End If
    End Function
End Class
