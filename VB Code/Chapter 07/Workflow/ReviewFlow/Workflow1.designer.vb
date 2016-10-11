<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial class Workflow1

    'NOTE: The following procedure is required by the Workflow Designer
    'It can be modified using the Workflow Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Private Sub InitializeComponent()
        Me.CanModifyActivities = True
        Dim activitybind1 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind2 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim correlationtoken1 As System.Workflow.Runtime.CorrelationToken = New System.Workflow.Runtime.CorrelationToken
        Dim activitybind3 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind4 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim codecondition1 As System.Workflow.Activities.CodeCondition = New System.Workflow.Activities.CodeCondition
        Dim activitybind5 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind6 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim correlationtoken2 As System.Workflow.Runtime.CorrelationToken = New System.Workflow.Runtime.CorrelationToken
        Dim activitybind7 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Me.onTaskChanged1 = New Microsoft.SharePoint.WorkflowActions.OnTaskChanged
        Me.completeTask1 = New Microsoft.SharePoint.WorkflowActions.CompleteTask
        Me.whileActivity1 = New System.Workflow.Activities.WhileActivity
        Me.createTask1 = New Microsoft.SharePoint.WorkflowActions.CreateTask
        Me.onWorkflowActivated1 = New Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated
        '
        'onTaskChanged1
        '
        activitybind1.Name = "Workflow1"
        activitybind1.Path = "afterProperties"
        activitybind2.Name = "Workflow1"
        activitybind2.Path = "beforeProperties"
        correlationtoken1.Name = "taskToken"
        correlationtoken1.OwnerActivityName = "Workflow1"
        Me.onTaskChanged1.CorrelationToken = correlationtoken1
        Me.onTaskChanged1.Executor = Nothing
        Me.onTaskChanged1.Name = "onTaskChanged1"
        activitybind3.Name = "Workflow1"
        activitybind3.Path = "taskId"
        AddHandler Me.onTaskChanged1.Invoked, AddressOf Me.onTaskChanged1_Invoked
        Me.onTaskChanged1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, CType(activitybind1, System.Workflow.ComponentModel.ActivityBind))
        Me.onTaskChanged1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, CType(activitybind2, System.Workflow.ComponentModel.ActivityBind))
        Me.onTaskChanged1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, CType(activitybind3, System.Workflow.ComponentModel.ActivityBind))
        '
        'completeTask1
        '
        Me.completeTask1.CorrelationToken = correlationtoken1
        Me.completeTask1.Name = "completeTask1"
        activitybind4.Name = "Workflow1"
        activitybind4.Path = "taskId"
        Me.completeTask1.TaskOutcome = Nothing
        AddHandler Me.completeTask1.MethodInvoking, AddressOf Me.completeTask1_MethodInvoking
        Me.completeTask1.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, CType(activitybind4, System.Workflow.ComponentModel.ActivityBind))
        '
        'whileActivity1
        '
        Me.whileActivity1.Activities.Add(Me.onTaskChanged1)
        AddHandler codecondition1.Condition, AddressOf Me.notComplete
        Me.whileActivity1.Condition = codecondition1
        Me.whileActivity1.Name = "whileActivity1"
        '
        'createTask1
        '
        Me.createTask1.CorrelationToken = correlationtoken1
        Me.createTask1.ListItemId = -1
        Me.createTask1.Name = "createTask1"
        Me.createTask1.SpecialPermissions = Nothing
        activitybind5.Name = "Workflow1"
        activitybind5.Path = "taskId"
        activitybind6.Name = "Workflow1"
        activitybind6.Path = "taskProperties"
        AddHandler Me.createTask1.MethodInvoking, AddressOf Me.createTask1_MethodInvoking
        Me.createTask1.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTask.TaskIdProperty, CType(activitybind5, System.Workflow.ComponentModel.ActivityBind))
        Me.createTask1.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTask.TaskPropertiesProperty, CType(activitybind6, System.Workflow.ComponentModel.ActivityBind))
        '
        'onWorkflowActivated1
        '
        correlationtoken2.Name = "workflowToken"
        correlationtoken2.OwnerActivityName = "Workflow1"
        Me.onWorkflowActivated1.CorrelationToken = correlationtoken2
        Me.onWorkflowActivated1.EventName = "OnWorkflowActivated"
        Me.onWorkflowActivated1.Name = "onWorkflowActivated1"
        activitybind7.Name = "Workflow1"
        activitybind7.Path = "workflowProperties"
        AddHandler Me.onWorkflowActivated1.Invoked, AddressOf Me.onWorkflowActivated1_Invoked
        Me.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, CType(activitybind7, System.Workflow.ComponentModel.ActivityBind))
        '
        'Workflow1
        '
        Me.Activities.Add(Me.onWorkflowActivated1)
        Me.Activities.Add(Me.createTask1)
        Me.Activities.Add(Me.whileActivity1)
        Me.Activities.Add(Me.completeTask1)
        Me.Name = "Workflow1"
        Me.CanModifyActivities = False

    End Sub
    Private completeTask1 As Microsoft.SharePoint.WorkflowActions.CompleteTask
    Private whileActivity1 As System.Workflow.Activities.WhileActivity
    Private onTaskChanged1 As Microsoft.SharePoint.WorkflowActions.OnTaskChanged
    Private createTask1 As Microsoft.SharePoint.WorkflowActions.CreateTask
    Private onWorkflowActivated1 As Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated

End Class
