'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Namespace Klemcoll
    Public Class ConnectionHelper
        Const ConnectionString As String = "XpoProvider=MSSqlServer;data source=localhost;integrated security=SSPI;initial catalog=Klemcoll"
        Public Shared Sub Connect(ByVal autoCreationOption As DB.AutoCreateOption)
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnectionString, autoCreationOption)
            XpoDefault.Session = Nothing
        End Sub
        Public Shared Function GetConnectionProvider(ByVal autoCreationOption As DB.AutoCreateOption) As DB.IDataStore
            Return XpoDefault.GetConnectionProvider(ConnectionString, autoCreationOption)
        End Function
        Public Shared Function GetConnectionProvider(ByVal autoCreationOption As DB.AutoCreateOption, ByRef objectsToDisposeOnDisconnect() As IDisposable) As DB.IDataStore
            Return XpoDefault.GetConnectionProvider(ConnectionString, autoCreationOption, objectsToDisposeOnDisconnect)
        End Function
        Public Shared Function GetDataLayer(ByVal autoCreationOption As DB.AutoCreateOption) As IDataLayer
            Return XpoDefault.GetDataLayer(ConnectionString, autoCreationOption)
        End Function
    End Class

End Namespace
