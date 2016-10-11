Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SharePoint
Imports System.Diagnostics

Public Class Receiver
    Inherits SPFeatureReceiver


    Public Overrides Sub FeatureActivated( _
    ByVal properties As Microsoft.SharePoint.SPFeatureReceiverProperties)

        Try
            Dim objSite As SPWeb = DirectCast(properties.Feature.Parent, SPWeb)

            LogMessage(objSite.ServerRelativeUrl & _
            "_catalogs/masterpage/minimal.master", _
            EventLogEntryType.Information)

            objSite.MasterUrl = objSite.ServerRelativeUrl & _
            "_catalogs/masterpage/minimal.master"

            objSite.CustomMasterUrl = objSite.ServerRelativeUrl & _
            "_catalogs/masterpage/minimal.master"

            objSite.Update()

        Catch x As SPException
            LogMessage(x.Message, EventLogEntryType.Error)
        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try

    End Sub

    Public Overrides Sub FeatureDeactivating( _
    ByVal properties As Microsoft.SharePoint.SPFeatureReceiverProperties)

        Try
            Dim objSite As SPWeb = DirectCast(properties.Feature.Parent, SPWeb)

            LogMessage(objSite.ServerRelativeUrl & _
            "_catalogs/masterpage/default.master", _
            EventLogEntryType.Information)

            objSite.MasterUrl = objSite.ServerRelativeUrl & _
            "_catalogs/masterpage/default.master"

            objSite.CustomMasterUrl = objSite.ServerRelativeUrl & _
            "_catalogs/masterpage/default.master"

            objSite.Update()

        Catch x As SPException
            LogMessage(x.Message, EventLogEntryType.Error)
        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try

    End Sub

    Public Overrides Sub FeatureInstalled( _
    ByVal properties As Microsoft.SharePoint.SPFeatureReceiverProperties)
        LogMessage("MinimalMaster Feature installed", EventLogEntryType.SuccessAudit)
    End Sub

    Public Overrides Sub FeatureUninstalling( _
    ByVal properties As Microsoft.SharePoint.SPFeatureReceiverProperties)
        LogMessage("MinimalMaster Feature uninstalling", EventLogEntryType.Information)
    End Sub

    Private Sub LogMessage(ByVal strMessage As String, _
    ByVal enmType As EventLogEntryType)
        If Not EventLog.SourceExists("SharePoint Features") Then
            EventLog.CreateEventSource("SharePoint Features", "Application")
        End If
        EventLog.WriteEntry("SharePoint Features", strMessage, enmType)
    End Sub

End Class
