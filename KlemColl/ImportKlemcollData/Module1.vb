Option Explicit On
Option Strict On

Imports System.IO
Imports Klemcoll.DAL
Imports DataStreams.Csv

Module Module1

    Sub Main()
        'ImportCSV("D:\WRavenFiles\Documents\Work Projects\Klemcoll\MASTERImages 91914.csv")

        TestShipping()


        Console.WriteLine("End of line")
        Console.ReadKey()
    End Sub

    Private Function GetInt(input As Object) As Integer
        If IsNumeric(input) Then
            Return CInt(input)
        Else
            Return 0
        End If

    End Function

    Private Sub TestShipping()
        Dim ship As New Klemcoll.DAL.Shipping()

        Dim prints As New CartDataSet

        prints.ReadXml("d:\tempcrap\prints.xml")

        Dim dt As DataTable = ship.CalculatePrintsShipping(prints.SessionImages, False)

        Dim stopHere As String = ""

    End Sub

    Private Sub ImportCSV(SourceFilePath As String)
        If Not File.Exists(SourceFilePath) Then
            Throw New FileNotFoundException("Cannot find source file: " & SourceFilePath)
        End If

        Dim csv As New CsvReader(SourceFilePath)

        Try
            Dim images As DataTable = csv.ReadToEnd()
            csv.ReadHeaders()
            Dim imagesTA As New KlemCollDataSetTableAdapters.ImagesTableAdapter()
            imagesTA.DeleteAll()

            Dim imagesDT As New KlemCollDataSet.ImagesDataTable()

            For Each row As DataRow In images.Rows
                Dim newRow As KlemCollDataSet.ImagesRow = imagesDT.NewImagesRow()

                With newRow
                    .ImagesID = CInt(row("ImagesID"))
                    .Refid = CStr(row("refid"))
                    .ListItems = GetInt(row("ListItems"))
                    .CropVersion = GetInt(row("CropVersion"))
                    .SignedAvailable = GetInt(row("SignedAvailable"))
                    .Sequence = CStr(row("Sequence"))
                    .Name = CStr(row("Name"))
                    .Description = CStr(row("Description"))
                    .PhotographerFirstName = CStr(row("PhotographerFirstName"))
                    .PhotographerLastName = CStr(row("PhotographerLastName"))
                    .PhotoType = CStr(row("PhotoType"))
                    .RaceType = CStr(row("RaceType"))
                    .MakeOfCar = CStr(row("MakeOfCar"))
                    .PhotoYear = CStr(row("PhotoYear"))
                    .DriverFirstName = CStr(row("DriverFirstName"))
                    .DriverLastName = CStr(row("DriverLastName"))
                End With

                imagesDT.Rows.Add(newRow)
            Next

            Console.WriteLine("Updated " & imagesTA.Update(imagesDT).ToString() & " Records")

        Finally
            csv.Close()

        End Try



    End Sub


End Module
