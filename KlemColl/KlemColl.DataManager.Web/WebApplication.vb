Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
'using DevExpress.ExpressApp.Security;

' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWebWebApplicationMembersTopicAll
Partial Public Class DataManagerAspNetApplication
    Inherits WebApplication
    Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
    Friend WithEvents SecurityModule1 As DevExpress.ExpressApp.Security.SecurityModule
    Friend WithEvents SecurityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
    Friend WithEvents AuthenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    Private module3 As KlemColl.DataManager.Module.DataManagerModule

    Public Sub New()
        InitializeComponent()
    End Sub
    
    Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
        args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection, True)
    End Sub

    '    Private Sub DataManagerAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
    '#If EASYTEST Then
    '        e.Updater.Update()
    '        e.Handled = True
    '#Else
    '        If System.Diagnostics.Debugger.IsAttached Then
    '            e.Updater.Update()
    '            e.Handled = True
    '        Else
    '            Dim message As String = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & Constants.vbCrLf & _
    '                "This error occurred  because the automatic database update was disabled when the application was started without debugging." & Constants.vbCrLf & _
    '                "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & _
    '                "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & _
    '                "or manually create a database using the 'DBUpdater' tool." & Constants.vbCrLf & _
    '                "Anyway, refer to the following help topics for more detailed information:" & Constants.vbCrLf & _
    '                "'Update Application and Database Versions' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm" & Constants.vbCrLf & _
    '                "'Database Security References' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument3237.htm" & Constants.vbCrLf & _
    '                "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/"

    '            If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
    '                message &= Constants.vbCrLf & Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
    '            End If
    '            Throw New InvalidOperationException(message)
    '        End If
    '#End If
    '    End Sub

    Private Sub InitializeComponent()
        Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
        Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
        Me.SecurityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
        Me.module3 = New KlemColl.DataManager.[Module].DataManagerModule()
        Me.SecurityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
        Me.AuthenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'SecurityStrategyComplex1
        '
        Me.SecurityStrategyComplex1.Authentication = Me.AuthenticationStandard1
        Me.SecurityStrategyComplex1.RoleType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole)
        Me.SecurityStrategyComplex1.UserType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)
        '
        'AuthenticationStandard1
        '
        Me.AuthenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
        '
        'DataManagerAspNetApplication
        '
        Me.ApplicationName = "KlemColl.DataManager"
        Me.Modules.Add(Me.module1)
        Me.Modules.Add(Me.module2)
        Me.Modules.Add(Me.SecurityModule1)
        Me.Modules.Add(Me.module3)
        Me.Security = Me.SecurityStrategyComplex1
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
End Class

