Imports System.Data

' Klemcoll's crazy shipping
' The requirements for Klemcoll's shipping are very specific and has a bunch of logic tied to it.
' Because of this, this class is full of magic numbers and strings.
Public Class Shipping

    Const shipFedInt As String = "FedEx International Priority"
    Const shipGEM As String = "International Express Mail"
    Const shipIPM As String = "International Priority Mail"
    Const shipFirstClass As String = "First Class Mail"
    Const shipFedUps As String = "Fed Ex / UPS Ground"
    Const shipPri As String = "Priority Mail"
    Const shipExp As String = "Express Mail"
    Const shipWillQuote As String = "Will Quote"
    Const shipIntFirstClass As String = "International First Class Mail"
    Const shipMediaMail As String = "Media Mail"
    'Const shipFirstClassIntNonIns As String = "First Class Mail International (not insurable)"
    Private excludeFirstClass As Boolean

    Public Sub New()
        excludeFirstClass = False
    End Sub

    Public Function CalculateShipping(ByVal Books As CartDataSet.SessionBooksDataTable, ByVal Prints As CartDataSet.SessionImagesDataTable, ByVal International As Boolean) As DataTable

        Dim PrintsShipping As DataTable = Nothing
        Dim BooksShipping As DataTable = Nothing
        Dim Result As New DataTable
        Result.Columns.Add("Method", GetType(String))
        Result.Columns.Add("Price", GetType(Decimal))
        Result.Columns.Add("DisplayOrder", GetType(Integer))

        Dim willQuote As Boolean = False

        If Prints.Count > 0 Then
            PrintsShipping = CalculatePrintsShipping(Prints, International)
            For Each row As DataRow In PrintsShipping.Rows
                If CStr(row(0)) = "Will Quote" Then
                    willQuote = True
                End If
                Result.Rows.Add(row(0), row(1))
            Next
        End If

        If Books.Count > 0 And Not willQuote Then
            BooksShipping = CalculateBooksShipping(Books, International)
            For Each row As DataRow In BooksShipping.Rows
                Dim found As DataRow() = Result.Select("Method = '" & CStr(row("Method")) & "'")
                If found.Length > 0 Then
                    found(0)("Price") = CDbl(found(0)("Price")) + CDbl(row("Price"))
                Else
                    Result.Rows.Add(row(0), row(1))
                End If
            Next
        End If

        ' Check if both First Class and Media Mail have been combined. (Rule 9)
        Dim findExclusions As DataRow() = Result.Select("Method = '" & shipFirstClass & "' OR Method = '" & shipMediaMail & "'")
        If findExclusions.Length >= 2 Then
            For Each row As DataRow In findExclusions
                Result.Rows.Remove(row)
            Next
        End If

        ' Remove any "First Class" items if certain size images are being shipped.
        If excludeFirstClass Then
            findExclusions = Result.Select("Method = '" + shipFirstClass + "'")
            For Each row As DataRow In findExclusions
                Result.Rows.Remove(row)
            Next
        End If

        For Each row As DataRow In Result.Rows
            If CDbl(row("Price")) > 0 Then
                row("Method") = CStr(row("Method")) & " (" & FormatCurrency(row("Price")) & ")"
                row("DisplayOrder") = CDbl(row("Price"))
            ElseIf CStr(row("Method")) <> "Will Quote" Then
                row("Method") = CStr(row("Method")) & " (Will Quote)"
                ' Make sure "Will Quote" options are always on the bottom.
                row("DisplayOrder") = 999999
            End If
        Next

        Result.DefaultView.Sort = "DisplayOrder"
        Return Result
    End Function

    Public Function CalculatePrintsShipping(ByVal Prints As CartDataSet.SessionImagesDataTable, ByVal International As Boolean) As DataTable
        Dim Result As New DataTable
        Result.Columns.Add("Method", GetType(String))
        Result.Columns.Add("Price", GetType(Decimal))

        ' IF order includes any print larger than 16x20 - shipping cost should appear as: Will Quote.
        If Prints.Select("PaperSize = 'Other'").Length > 0 Or Prints.Select("PaperType = 'Pearl Fiber' AND PaperSize <> '8 x 10'").Length > 0 Then
            Result.Rows.Add(shipWillQuote, 0)
            excludeFirstClass = True
            Return Result

        End If

        If Not International And Prints.Select("PaperSize <> '8 x 10'").Length > 0 Then
            excludeFirstClass = True
        End If

        Dim firstClass As Integer
        Dim priority As Integer
        Dim express As Integer
        Dim fedExUps As Integer
        Dim intFirstClass As Integer
        Dim intPriority As Integer
        Dim intExpress As Integer
        Dim intFedExUps As Integer

        Dim largest As ImageSizes

        ' Find the largest size and base prices on that size for all.
        If Prints.Select("PaperSize = 'Other'").Length > 0 Then

        ElseIf Prints.Select("PaperSize = '16 x 20'").Length > 0 Then
            largest = ImageSizes.SixteenByTwenty
            intFirstClass = 39
            intPriority = 61
            intExpress = 78
            intFedExUps = 211
            firstClass = 0
            priority = 28
            express = 68
            fedExUps = 24
        ElseIf Prints.Select("PaperSize = '11 x 14'").Length > 0 Then
            largest = ImageSizes.ElevenByFourteen
            intFirstClass = 32
            intPriority = 57
            intExpress = 72
            intFedExUps = 170
            firstClass = 0
            priority = 24
            express = 61
            fedExUps = 22
        ElseIf Prints.Select("PaperSize = '8 x 10'").Length > 0 Then
            largest = ImageSizes.EightByTen
            intFirstClass = 27
            intPriority = 52
            intExpress = 66
            intFedExUps = 140
            firstClass = 11
            priority = 15
            express = 43
            fedExUps = 18
        End If

        Dim Mult As Integer
        Dim PrintCount As Integer = CInt(Prints.Compute("Sum(Quantity)", ""))

        If PrintCount < 1 Then
            Throw New Exception("No prints found. Please contact Expert Data Solutions for support.")
        End If

        If International Then
            ' International First Class
            Select Case largest
                Case ImageSizes.EightByTen
                    Mult = CalcMultiplier(2, PrintCount)
                Case Else
                    Mult = CalcMultiplier(3, PrintCount)
            End Select
            Result.Rows.Add(shipIntFirstClass, MarkupShipping(intFirstClass * Mult))

            Mult = CalcMultiplier(2, PrintCount)

            ' International Priority Mail
            Result.Rows.Add(shipIPM, MarkupShipping(intPriority * Mult))
            ' International Express Mail
            Result.Rows.Add(shipGEM, MarkupShipping(intExpress * Mult))

            If largest = ImageSizes.EightByTen Then
                If PrintCount <= 20 Then
                    Mult = 1
                ElseIf PrintCount >= 21 And PrintCount <= 40 Then
                    Mult = 1
                    intFedExUps = 180
                Else
                    ' TODO will quote
                End If
            Else
                Mult = CalcMultiplier(2, PrintCount)
            End If
            ' FedEx International Priority
            Result.Rows.Add(shipFedInt, MarkupShipping(intFedExUps * Mult))
        Else
            If firstClass > 0 And PrintCount <= 5 Then
                'First Class Mail 5 or less - $10
                Result.Rows.Add(shipFirstClass, MarkupShipping(10))
            End If

            Mult = CalcMultiplier(2, PrintCount)

            'Priority Mail
            Result.Rows.Add(shipPri, MarkupShipping(priority * Mult))
            'Express Mail
            Result.Rows.Add(shipExp, MarkupShipping(express * Mult))
            'FedEx/UPS Ground
            Result.Rows.Add(shipFedUps, MarkupShipping(fedExUps * Mult))

        End If

        Return Result
    End Function

    Private Function CalcMultiplier(RuleNumber As Integer, Count As Integer) As Integer
        Select Case RuleNumber
            Case 2
                Return Convert.ToInt32(Math.Ceiling(Count / 10))
            Case 3
                If Count <= 5 Then
                    Return 1
                ElseIf Count >= 6 And Count <= 10 Then
                    Return 2
                Else
                    Return Convert.ToInt32(Math.Ceiling(Count / 10))
                End If
            Case 4
                If Count <= 20 Then
                    Return 1
                ElseIf Count >= 21 And Count <= 40 Then

                End If
            Case 5
                Return Convert.ToInt32(Math.Ceiling(Count / 3))
            Case 6
                Return Convert.ToInt32(Math.Ceiling(Count / 4))
        End Select

    End Function

    Private Enum ImageSizes
        EightByTen
        ElevenByFourteen
        SixteenByTwenty
    End Enum

    Public Function CalculateBooksShipping(ByVal Books As CartDataSet.SessionBooksDataTable, ByVal International As Boolean) As DataTable

        Dim Result As New DataTable
        Result.Columns.Add("Method", GetType(String))
        Result.Columns.Add("Price", GetType(Double))

        ' For the purpose of this method, -1 and -2 are special values for the following total variables.
        ' 0 = Do nothing
        ' -1 = "N/A"
        ' -2 = "Will Quote"
        Const NotAvailable As Integer = -1
        Const WillQuote As Integer = -2

        Dim firstClassTotal As Double = 0
        Dim priorityTotal As Double = 0
        Dim expressTotal As Double = 0
        Dim FedExUpsTotal As Double = 0

        Dim totalCount As Integer = CInt(Books.Compute("SUM(Quantity)", ""))

        ' Take care of special publications.
        Dim countObject As Object = Books.Compute("SUM(Quantity)", "Name = 'The Eye of Klemantaski' OR Name = 'The Golden Age'")
        Dim specialPubCount As Integer = 0
        If countObject IsNot Nothing AndAlso Not IsDBNull(countObject) Then
            specialPubCount = CInt(countObject)
            If specialPubCount > 0 Then
                If specialPubCount = totalCount Then
                    ' Only special pubs being sent.
                    If Not International Then
                        firstClassTotal += 3 * specialPubCount
                        priorityTotal += 11 * CalcMultiplier(5, specialPubCount)
                        expressTotal += 43 * CalcMultiplier(6, specialPubCount)
                        FedExUpsTotal += 17 * CalcMultiplier(6, specialPubCount)

                    Else
                        firstClassTotal += 6 * specialPubCount
                        priorityTotal += 26 * CalcMultiplier(6, specialPubCount)
                        expressTotal += 51 * CalcMultiplier(6, specialPubCount)
                        FedExUpsTotal = WillQuote
                    End If


                Else
                    ' Order contains special pubs, but mixed with other books.
                    If Not International Then
                        firstClassTotal += 3 * specialPubCount
                        priorityTotal += 3 * specialPubCount
                        expressTotal += 3 * specialPubCount
                        FedExUpsTotal += 3 * specialPubCount
                    Else
                        firstClassTotal += 6 * specialPubCount
                        priorityTotal += 6 * specialPubCount
                        expressTotal += 6 * specialPubCount
                        FedExUpsTotal = WillQuote
                    End If

                End If
            End If

        End If

        Dim weightObject As Object = Books.Compute("SUM(TotalWeight)", "Name <> 'The Eye of Klemantaski' AND Name <> 'The Golden Age'")
        If weightObject IsNot Nothing AndAlso Not IsDBNull(weightObject) Then
            ' Get the total weight of the non-special books.
            Dim totalWeight As Integer = CInt(Math.Ceiling(CDec(weightObject) + 1.5))

            If totalWeight > 0 Then
                If Not International Then
                    ' I know this select seems crazy, but these values have to take up lines of code somewhere, might as well be here.
                    Select Case totalWeight
                        Case Is <= 2
                            firstClassTotal += 8
                            priorityTotal += 18.5
                            expressTotal += 54
                            FedExUpsTotal += 15
                        Case Is <= 3
                            firstClassTotal += 8.75
                            priorityTotal += 23
                            expressTotal += 61
                            FedExUpsTotal += 17
                        Case Is <= 4
                            firstClassTotal += 9.25
                            priorityTotal += 27
                            expressTotal += 68
                            FedExUpsTotal += 18.5
                        Case Is <= 5
                            firstClassTotal += 9.75
                            priorityTotal += 30
                            expressTotal += 75
                            FedExUpsTotal += 20
                        Case Is <= 6
                            firstClassTotal += 10.25
                            priorityTotal += 34
                            expressTotal += 83
                            FedExUpsTotal += 21
                        Case Is <= 7
                            firstClassTotal += 10.75
                            priorityTotal += 37
                            expressTotal += 90
                            FedExUpsTotal += 22
                        Case Is <= 8
                            firstClassTotal += 11
                            priorityTotal += 41
                            expressTotal += 97
                            FedExUpsTotal += 23
                        Case Is <= 9
                            firstClassTotal += 11.75
                            priorityTotal += 45
                            expressTotal += 104
                            FedExUpsTotal += 24
                        Case Is <= 10
                            firstClassTotal += 12.25
                            priorityTotal += 49
                            expressTotal += 109
                            FedExUpsTotal += 25
                        Case Else
                            firstClassTotal += CalcOverTenPounds(12.25, totalWeight)
                            priorityTotal += CalcOverTenPounds(49, totalWeight)
                            expressTotal += CalcOverTenPounds(109, totalWeight)
                            FedExUpsTotal += CalcOverTenPounds(25, totalWeight)

                    End Select
                Else
                    Select Case totalWeight
                        Case Is <= 2
                            firstClassTotal += 30
                            priorityTotal += 51
                            expressTotal += 65
                            FedExUpsTotal = WillQuote
                        Case Is <= 3
                            firstClassTotal += 37
                            priorityTotal += 56
                            expressTotal += 71
                            FedExUpsTotal = WillQuote
                        Case Is <= 4
                            firstClassTotal += 45
                            priorityTotal += 61
                            expressTotal += 76
                            FedExUpsTotal = WillQuote
                        Case Is <= 5
                            firstClassTotal = NotAvailable
                            priorityTotal += 65
                            expressTotal += 81
                            FedExUpsTotal = WillQuote
                        Case Is <= 6
                            firstClassTotal = NotAvailable
                            priorityTotal += 71
                            expressTotal += 86
                            FedExUpsTotal = WillQuote
                        Case Is <= 7
                            firstClassTotal = NotAvailable
                            priorityTotal += 75
                            expressTotal += 92
                            FedExUpsTotal = WillQuote
                        Case Is <= 8
                            firstClassTotal = NotAvailable
                            priorityTotal += 81
                            expressTotal += 98
                            FedExUpsTotal = WillQuote
                        Case Is <= 9
                            firstClassTotal = NotAvailable
                            priorityTotal += 86
                            expressTotal += 103
                            FedExUpsTotal = WillQuote
                        Case Is <= 10
                            firstClassTotal = NotAvailable
                            priorityTotal += 90
                            expressTotal += 110
                            FedExUpsTotal = WillQuote
                        Case Else
                            firstClassTotal = NotAvailable
                            priorityTotal += CalcOverTenPounds(90, totalWeight)
                            expressTotal += CalcOverTenPounds(110, totalWeight)
                            FedExUpsTotal = WillQuote
                    End Select
                End If

            End If
        End If

        If firstClassTotal > 0 Then
            If Not International Then
                If specialPubCount = totalCount Then
                    Result.Rows.Add(shipFirstClass, firstClassTotal)
                Else
                    Result.Rows.Add(shipMediaMail, firstClassTotal)
                End If
            Else
                Result.Rows.Add(shipIntFirstClass, firstClassTotal)
            End If
        End If

        If priorityTotal > 0 Then
            If Not International Then
                Result.Rows.Add(shipPri, priorityTotal)
            Else
                Result.Rows.Add(shipIPM, priorityTotal)
            End If
        End If

        If expressTotal > 0 Then
            If Not International Then
                Result.Rows.Add(shipExp, expressTotal)
            Else
                Result.Rows.Add(shipGEM, expressTotal)
            End If
        End If

        If FedExUpsTotal > 0 Then
            Result.Rows.Add(IIf(International, shipFedInt, shipFedUps), FedExUpsTotal)
        ElseIf FedExUpsTotal = WillQuote Then
            Result.Rows.Add(IIf(International, shipFedInt, shipFedUps), 0)
        End If

        Return Result
    End Function

    Private Function CalcOverTenPounds(BaseCost As Double, TotalWeight As Integer) As Double
        If TotalWeight <= 10 Then
            Throw New ArgumentOutOfRangeException("TotalWeight", "Weight must be over 10 lbs to use this function.")
        End If
        Return BaseCost + (BaseCost * 0.07) * (TotalWeight - 10)
    End Function

    Private Function MarkupShipping(input As Decimal) As Decimal
        ' Marking up by 15% per Peter's request on 1/23/2012.
        'Dim markup As Decimal = (input * 15) / 100
        'Return input + markup
        Return input
    End Function

End Class