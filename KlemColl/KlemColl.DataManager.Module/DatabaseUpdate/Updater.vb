Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy
'Imports DevExpress.ExpressApp.Reports
'Imports DevExpress.ExpressApp.PivotChart
'Imports KlemColl.DataManager.Module.BusinessObjects

' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
Public Class Updater
    Inherits ModuleUpdater
    Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
        MyBase.New(objectSpace, currentDBVersion)
    End Sub

    Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
        MyBase.UpdateDatabaseAfterUpdateSchema()

        ' Create Admin role if it doesn't exist.
        Dim adminRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", SecurityStrategy.AdministratorRoleName))

        If adminRole Is Nothing Then
            adminRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
            adminRole.Name = SecurityStrategy.AdministratorRoleName
            adminRole.IsAdministrative = True
        End If


        ' Create user role if it doesn't exist.
        Dim userRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "User"))
        If userRole Is Nothing Then
            userRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
            userRole.Name = "User"
            Dim userTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
            userTypePermission.TargetType = GetType(SecuritySystemUser)
            Dim currentUserObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
            currentUserObjectPermission.Criteria = "[Oid] = CurrentUserId()"
            currentUserObjectPermission.AllowNavigate = True
            currentUserObjectPermission.AllowRead = True
            userTypePermission.ObjectPermissions.Add(currentUserObjectPermission)
            userRole.TypePermissions.Add(userTypePermission)
        End If

        ' Create Users.
        Dim user1 As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "wraven"))
        If user1 Is Nothing Then
            user1 = ObjectSpace.CreateObject(Of SecuritySystemUser)()
            user1.UserName = "wraven"
            user1.SetPassword("mustang")
            
        End If
        user1.Roles.Add(adminRole)

        'user1.Save()

        Dim user2 As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "psachs"))
        If user2 Is Nothing Then
            user2 = ObjectSpace.CreateObject(Of SecuritySystemUser)()
            user2.UserName = "psachs"
            user2.SetPassword("P3120872s")
            
        End If

        user1.Roles.Add(adminRole)
        user2.Roles.Add(adminRole)
        'user2.Save()

        'ObjectSpace.CommitChanges()

        'Dim name As String = "MyName"
        'Dim theObject As DomainObject1 = ObjectSpace.FindObject(Of DomainObject1)(CriteriaOperator.Parse("Name=?", name))
        'If (theObject Is Nothing) Then
        '    theObject = ObjectSpace.CreateObject(Of DomainObject1)()
        '    theObject.Name = name
        'End If
    End Sub

    Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
        MyBase.UpdateDatabaseBeforeUpdateSchema()
        'If (CurrentDBVersion < New Version("1.1.0.0") AndAlso CurrentDBVersion > New Version("0.0.0.0")) Then
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName")
        'End If
    End Sub
End Class
