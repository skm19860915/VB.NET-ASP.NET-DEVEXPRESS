Imports Microsoft.VisualBasic
Imports System

Partial Public Class DataManagerModule
    ''' <summary> 
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary> 
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (Not components Is Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Component Designer generated code"

    ''' <summary> 
    ''' Required method for Designer support - do not modify 
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        '
        'DataManagerModule
        '
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ChangePasswordOnLogonParameters))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ChangePasswordParameters))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ClientServer.MemberMatrixItem))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ClientServer.MemberListDetailViewObject))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemTypePermissionsObjectBase))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemObjectPermissionsObject))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemMemberPermissionsObject))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemTypePermissionsObjectOwner))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemTypePermissionObject))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRoleBase))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUserBase))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.Strategy.PermissionMatrix.TypePermissionMatrixItem))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.PermissionData))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.TypePermissionData))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.TypeOperationPermissionData))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.MemberOperationPermissionData))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ObjectOperationPermissionData))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ModelOperationPermissionData))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.MemberOperationPermissionDescriptor))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ObjectOperationPermissionDescriptor))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.TypeOperationPermissionDescriptor))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.TypePermissionDetails))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.PermissionsContainer))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityStrategyRole))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityRole))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.HierarchicalRole))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityUserBase))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityUserWithRolesBase))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityUser))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.SecuritySimpleUser))
        Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.ResetPasswordParameters))
        Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.SystemModule.SystemModule))

    End Sub

#End Region
End Class
