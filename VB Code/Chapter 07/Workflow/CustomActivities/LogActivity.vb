Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Workflow.ComponentModel
Imports System.Workflow.ComponentModel.Compiler
Imports System.Workflow.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing

<Designer(GetType(ActivityDesigner), _
GetType(IDesigner)), _
ToolboxItem(GetType(ActivityToolboxItem)), _
Description("Logging Activity"), _
ActivityValidator(GetType(LogActivityValidator))> _
Public NotInheritable Class LogActivity
    Inherits Activity

    'Name of log
    Public Shared LogNameProperty As DependencyProperty = _
    DependencyProperty.Register( _
    "LogName", GetType(String), GetType(LogActivity))

    Public Property LogName() As String
        Get
            Return CType((MyBase.GetValue( _
            LogActivity.LogNameProperty)), String)
        End Get
        Set(ByVal value As String)
            MyBase.SetValue(LogActivity.LogNameProperty, value)
        End Set
    End Property

    'Message Property
    Public Shared MessageProperty As DependencyProperty = _
    DependencyProperty.Register( _
    "Message", GetType(String), GetType(LogActivity))

    Public Property Message() As String
        Get
            Return CType((MyBase.GetValue( _
            LogActivity.MessageProperty)), String)
        End Get
        Set(ByVal value As String)
            MyBase.SetValue(LogActivity.MessageProperty, value)
        End Set
    End Property

    'Entry type
    Public Shared EntryTypeProperty As DependencyProperty = _
    DependencyProperty.Register( _
    "EntryType", GetType(String), GetType(LogActivity))

    Public Property EntryType() As String
        Get
            Return CType((MyBase.GetValue( _
            LogActivity.EntryTypeProperty)), String)
        End Get
        Set(ByVal value As String)
            MyBase.SetValue(LogActivity.EntryTypeProperty, value)
        End Set
    End Property

    Protected Overloads Overrides Function Execute( _
    ByVal executionContext As ActivityExecutionContext) _
    As ActivityExecutionStatus

        If Not EventLog.SourceExists(LogName) Then
            EventLog.CreateEventSource(LogName, "Application")
        End If

        Select Case EntryType
            Case "Error"
                EventLog.WriteEntry( _
                LogName, Message, EventLogEntryType.Error)
            Case "Failure"
                EventLog.WriteEntry( _
                LogName, Message, EventLogEntryType.FailureAudit)
            Case "Information"
                EventLog.WriteEntry( _
                LogName, Message, EventLogEntryType.Information)
            Case "Success"
                EventLog.WriteEntry( _
                LogName, Message, EventLogEntryType.SuccessAudit)
            Case "Warning"
                EventLog.WriteEntry( _
                LogName, Message, EventLogEntryType.Warning)
        End Select

        Return ActivityExecutionStatus.Closed

    End Function

End Class

Public Class LogActivityValidator
    Inherits ActivityValidator

    Public Overrides Function ValidateProperties( _
    ByVal manager As ValidationManager, ByVal obj As Object) _
    As ValidationErrorCollection

        Dim objErrors As ValidationErrorCollection = _
        New ValidationErrorCollection

        Dim activity As LogActivity = CType(obj, LogActivity)
        If activity Is Nothing Then
            objErrors.Add( _
            New ValidationError("Not a valid activity.", 1))
        Else
            If activity.LogName Is Nothing Then
                objErrors.Add( _
                New ValidationError("Not a valid log name.", 2))
            End If
            If activity.Message Is Nothing Then
                objErrors.Add( _
                New ValidationError("Not a valid message.", 3))
            End If
            If activity.EntryType Is Nothing Then
                objErrors.Add( _
                New ValidationError("Not a valid entry type.", 4))
            End If
        End If

        Return objErrors

    End Function

End Class