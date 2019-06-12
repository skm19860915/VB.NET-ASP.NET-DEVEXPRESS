Imports System.Xml
Imports System.Xml.Linq
Imports Klemcoll.DAL
Imports Klemcoll.DAL.KlemCollDataSetTableAdapters

Module Program

    Const BaseURL As String = "http://www.klemcoll.com/"
    Dim linkCounter As Integer = 0

    Sub Main()
        Console.WriteLine("Generating...")

        Try
            GenerateSitemap()
            Console.WriteLine("Links created: " & linkCounter.ToString())

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.WriteLine("End of line")
        Console.ReadKey()

    End Sub

    Private Sub GenerateSitemap()
        Dim doc As New XDocument(New XDeclaration("1.0", "utf-8", "yes"))
        Dim root As New XElement("urlset")
        doc.Add(root)

        Dim booksTA As New BooksTableAdapter()
        Dim photographersTA As New OurPhotographersTableAdapter()
        Dim imagesTA As New GalleryImagesTableAdapter()

        Dim photographersDT As KlemCollDataSet.OurPhotographersDataTable = photographersTA.GetData()
        Dim booksDT As KlemCollDataSet.BooksDataTable = booksTA.GetData()
        Dim imagesDT As KlemCollDataSet.GalleryImagesDataTable = imagesTA.GetData()

        For Each photographer As KlemCollDataSet.OurPhotographersRow In photographersDT
            root.Add(CreateURL(BaseURL & "photographers/" & photographer.id.ToString() & ".aspx"))
        Next

        For Each book As KlemCollDataSet.BooksRow In booksDT
            root.Add(CreateURL(BaseURL & "publications/" & book.BooksID.ToString() & ".aspx"))
        Next

        For Each image As KlemCollDataSet.GalleryImagesRow In imagesDT
            root.Add(CreateURL(BaseURL & "image/" & image.Refid & ".aspx"))
        Next

        doc.Save("C:\TempCrap\KlemCollSiteMap.xml")

        ' Gallery images
        '"image/0A-22.aspx?page=
    End Sub

    Private Function CreateURL(URL As String) As XElement
        linkCounter += 1
        Return New XElement("url", New XElement("loc", URL))

    End Function



End Module
