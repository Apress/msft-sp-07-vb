Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.Workflow
Imports Microsoft.SharePoint.WorkflowActions
Imports Microsoft.Office.Workflow.Utility

'Added Code
Imports System.Diagnostics
Imports System.Xml.Serialization
Imports System.Xml
'End Added Code

Public Class Workflow1
    Inherits SharePointSequentialWorkflowActivity

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    'Added Code
    Public workflowID As System.Guid = Nothing
    Public managerUsername As String = ""
    Public managerFullname As String = ""
    Public reviewType As String = ""
    Public reviewComments As String = ""
    'End Added Code

    Public workflowProperties As SPWorkflowActivationProperties = _
    New Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties

    Private Sub onWorkflowActivated1_Invoked( _
    ByVal sender As System.Object, _
    ByVal e As System.Workflow.Activities.ExternalDataEventArgs)

        'Added Code
        Try

            'Save the Workflow ID
            workflowID = workflowProperties.WorkflowId

            'Save data from the Initialization form
            Dim objDocument As New XmlDocument
            objDocument.LoadXml(workflowProperties.InitiationData)

            Dim objManager As New XmlNamespaceManager(objDocument.NameTable)
            objManager.AddNamespace("my", _
            "http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-12-05T20:04:17")

            managerUsername = objDocument.SelectSingleNode( _
            "/my:flowFields/my:managerUsername", objManager).InnerText

            managerFullname = objDocument.SelectSingleNode( _
            "/my:flowFields/my:managerFullname", objManager).InnerText

            reviewType = objDocument.SelectSingleNode( _
            "/my:flowFields/my:reviewType", objManager).InnerText

            reviewComments = objDocument.SelectSingleNode( _
            "/my:flowFields/my:reviewComments", objManager).InnerText

        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try

        'End Added Code

    End Sub

    Public taskId As System.Guid = Nothing
    Public taskProperties As SPWorkflowTaskProperties = _
    New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties

    Private Sub createTask1_MethodInvoking( _
    ByVal sender As System.Object, _
    ByVal e As System.EventArgs)

        'Added Code
        Try

            taskId = Guid.NewGuid
            taskProperties.AssignedTo = managerUsername
            taskProperties.Description = reviewComments & vbCrLf

            taskProperties.Title = _
            "Perform employee review [" & reviewType & "]"

            taskProperties.ExtendedProperties("reviewComments") = _
            reviewComments

        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try
        'End Added Code

    End Sub

    'Added Code
    Private Complete As Boolean = False
    'End Added Code

    Private Sub notComplete( _
    ByVal sender As System.Object, _
    ByVal e As System.Workflow.Activities.ConditionalEventArgs)

        'Added Code
        e.Result = Not Complete
        'End Added Code

    End Sub

    Public afterProperties As SPWorkflowTaskProperties = _
    New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties
    Public beforeProperties As SPWorkflowTaskProperties = _
    New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties

    Private Sub onTaskChanged1_Invoked( _
    ByVal sender As System.Object, _
    ByVal e As System.Workflow.Activities.ExternalDataEventArgs)

        'Added Code
        Try
            Complete = Boolean.Parse(afterProperties.ExtendedProperties("reviewCompleted").ToString())
        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try
        'End Added Code

    End Sub

    Private Sub completeTask1_MethodInvoking( _
    ByVal sender As System.Object, _
    ByVal e As System.EventArgs)

        'Added Code
        Try

            afterProperties.Description &= _
            afterProperties.ExtendedProperties("reviewNotes").ToString()

            afterProperties.PercentComplete = 100

        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try
        'End Added Code

    End Sub

    Private Sub LogMessage(ByVal strMessage As String, ByVal enmType As EventLogEntryType)
        If Not EventLog.SourceExists("SharePoint Workflow") Then
            EventLog.CreateEventSource("SharePoint Workflow", "Application")
        End If
        EventLog.WriteEntry("SharePoint Workflow", strMessage, enmType)
    End Sub

End Class
