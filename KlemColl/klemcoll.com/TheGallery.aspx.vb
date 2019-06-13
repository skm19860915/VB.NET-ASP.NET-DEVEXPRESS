
Partial Class TheGallery
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Photographer1") IsNot Nothing AndAlso Session("Photographer1").ToString.Length > 0 Then Me.PhotographerDropDownList1.SelectedValue = CStr(Session("Photographer1"))
        If Session("Photographer2") IsNot Nothing AndAlso Session("Photographer2").ToString.Length > 0 Then Me.PhotographerDropDownList2.SelectedValue = CStr(Session("Photographer2"))
        If Session("PhotoType") IsNot Nothing AndAlso Session("PhotoType").ToString.Length > 0 Then Me.PhotoTypeDropDownList.SelectedValue = CStr(Session("PhotoType"))
        If Session("RaceType") IsNot Nothing AndAlso Session("RaceType").ToString.Length > 0 Then Me.RaceTypeDropDownList.SelectedValue = CStr(Session("RaceType"))
        If Session("MakeOfCar1") IsNot Nothing AndAlso Session("MakeOfCar1").ToString.Length > 0 Then Me.MakeOfCarDropDownList1.SelectedValue = CStr(Session("MakeOfCar1"))
        If Session("MakeOfCar2") IsNot Nothing AndAlso Session("MakeOfCar2").ToString.Length > 0 Then Me.MakeOfCarDropDownList2.SelectedValue = CStr(Session("MakeOfCar2"))
        If Session("PhotoYear") IsNot Nothing AndAlso Session("PhotoYear").ToString.Length > 0 Then Me.YearDropDownList.SelectedValue = CStr(Session("PhotoYear"))
        If Session("Driver1") IsNot Nothing AndAlso Session("Driver1").ToString.Length > 0 Then Me.DriversDropDownList1.SelectedValue = CStr(Session("Driver1"))
        If Session("Driver2") IsNot Nothing AndAlso Session("Driver2").ToString.Length > 0 Then Me.DriversDropDownList2.SelectedValue = CStr(Session("Driver2"))
        If Session("PhotoYear") IsNot Nothing AndAlso Session("PhotoYear").ToString.Length > 0 Then Me.YearDropDownList.SelectedValue = CStr(Session("PhotoYear"))
    End Sub


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ' Set the left nav button to selected.
        'CType(Me.Master.FindControl("TheGalleryButton"), msWebControlsLibrary.ExImageButton).Enabled = False
    End Sub

    '#Region "Public properties to expose the search dropdownlists"
    '    Public ReadOnly Property Photographer1() As String
    '        Get
    '            Return Me.PhotographerDropDownList1.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property Photographer2() As String
    '        Get
    '            Return Me.PhotographerDropDownList2.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property Driver1() As String
    '        Get
    '            Return Me.DriversDropDownList1.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property Driver2() As String
    '        Get
    '            Return Me.DriversDropDownList2.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property MakeOfCar1() As String
    '        Get
    '            Return Me.MakeOfCarDropDownList1.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property MakeOfCar2() As String
    '        Get
    '            Return Me.MakeOfCarDropDownList2.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property PhotoType() As String
    '        Get
    '            Return Me.PhotoTypeDropDownList.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property RaceType() As String
    '        Get
    '            Return Me.RaceTypeDropDownList.SelectedValue
    '        End Get
    '    End Property

    '    Public ReadOnly Property PhotoYear() As String
    '        Get
    '            Return Me.YearDropDownList.SelectedValue
    '        End Get
    '    End Property

    '#End Region

#Region "Add Select All to drop downs"
    Private Sub AddSelectAll(ByRef DDL As Object)
        CType(DDL, DropDownList).Items.Insert(0, New ListItem("<<< Select All >>>", ""))
    End Sub

    Protected Sub PhotographerDropDownList1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhotographerDropDownList1.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub PhotographerDropDownList2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhotographerDropDownList2.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub DriversDropDownList1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DriversDropDownList1.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub DriversDropDownList2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DriversDropDownList2.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub MakeOfCarDropDownList1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles MakeOfCarDropDownList1.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub MakeOfCarDropDownList2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles MakeOfCarDropDownList2.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub PhotoTypeDropDownList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhotoTypeDropDownList.DataBound
        AddSelectAll(sender)
    End Sub

    Protected Sub RaceTypeDropDownList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles RaceTypeDropDownList.DataBound
        AddSelectAll(sender)
    End Sub

#End Region

End Class
