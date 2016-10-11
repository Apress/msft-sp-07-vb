<%@ Page Language="VB" MasterPageFile="~/_admin/admin.master" %>

<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages.Administration, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.Administration" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Data" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
    Unified Logging Service (ULS) Logs
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="PlaceHolderPageDescription">
    This page allows you to browse the Unified Logging Service logs
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <table border="0" cellspacing="0" cellpadding="0" class="ms-propertysheet" width="100%">
        <tr>
            <td>
                <!-- This section lists the log files that are on the server -->
                <wssuc:InputFormSection Title="Available log files" runat="server">
                    <template_description>
			            <asp:Literal Id="Literal1" runat="server" text="These are the available log files that you can view."/>
		            </template_description>
                    <template_inputformcontrols>
			            <wssuc:InputFormControl runat="server" LabelText="Select a log file to view">
				            <Template_control>
                                <asp:DropDownList ID="listFiles" runat="server" EnableViewState="true" Width="100%"/>
				            </Template_control>
			            </wssuc:InputFormControl>
		            </template_inputformcontrols>
                </wssuc:InputFormSection>
                <!-- This section allows filtering of the logs by category and severity -->
                <wssuc:InputFormSection Title="Log Filtering" runat="server">
                    <template_description>
			            <asp:Literal ID="Literal2" runat="server" text="Select the keywords to use when filtering the logs"/>
		            </template_description>
                    <template_inputformcontrols>
					            <TABLE border="0" cellspacing="0" cellpadding="0" width="100%">
						            <tr><td>
			                        <wssuc:InputFormControl runat="server" LabelText="This category..." LabelAssociatedControlId="listCategories">
				                        <Template_control>
							            <asp:DropDownList Id="listCategories" runat="server" EnableViewState="true">
	                                        <asp:ListItem value="empty" Text=""/>
	                                        <asp:ListItem value="Administration" Text="Administration"/>
	                                        <asp:ListItem value="Backup and Restore" Text="Backup and Restore"/>
	                                        <asp:ListItem value="Backward Compatible" Text="Backward Compatible Administration and Object Model"/>
	                                        <asp:ListItem value="Business Data" Text="Business Data Catalog"/>
	                                        <asp:ListItem value="Communication" Text="Communication"/>
	                                        <asp:ListItem value="Content Deployment" Text="Content Deployment"/>
	                                        <asp:ListItem value="Database" Text="Database"/>
	                                        <asp:ListItem value="Document Management" Text="Document Management"/>
	                                        <asp:ListItem value="E-Mail" Text="E-Mail" />
	                                        <asp:ListItem value="Excel" Text="Excel Services"/>
	                                        <asp:ListItem value="Feature Infrastructure" Text="Feature Infrastructure"/>
	                                        <asp:ListItem value="Fields" Text="Fields"/>
	                                        <asp:ListItem value="Forms Services" Text="Forms Services"/>
	                                        <asp:ListItem value="General" Text="General"/>
	                                        <asp:ListItem value="Information Policy Management" Text="Information Policy Management"/>
	                                        <asp:ListItem value="IRM" Text="Information Rights Management"/>
	                                        <asp:ListItem value="Knowledge Network Server" Text="Knowledge Network Server"/>
	                                        <asp:ListItem value="Launcher Service" Text="Launcher Service"/>
	                                        <asp:ListItem value="Load Balancer Service" Text="Load Balancer Service"/>
	                                        <asp:ListItem value="Long running operation infrastructure" Text="Long running operation infrastructure"/>
	                                        <asp:ListItem value="MCMS 2002 Migration" Text="MCMS 2002 Migration"/>
	                                        <asp:ListItem value="Office Server" Text="MOSS General"/>
	                                        <asp:ListItem value="Shared Services" Text="MOSS Shared Services"/>
	                                        <asp:ListItem value="MS Search" Text="MS Search"/>
	                                        <asp:ListItem value="Project Server" Text="Project Server"/>
	                                        <asp:ListItem value="Publishing" Text="Publishing Features"/>
	                                        <asp:ListItem value="Records Center" Text="Records Center"/>
	                                        <asp:ListItem value="Runtime" Text="Runtime"/>
	                                        <asp:ListItem value="Server Help" Text="Server Help"/>
	                                        <asp:ListItem value="Session State Service" Text="Session State Service"/>
	                                        <asp:ListItem value="Setup and Upgrade" Text="Setup and Upgrade"/>
	                                        <asp:ListItem value="SharePoint Services" Text="SharePoint Services"/>
	                                        <asp:ListItem value="Site Directory" Text="Site Directory"/>
	                                        <asp:ListItem value="Site Management" Text="Site Management"/>
	                                        <asp:ListItem value="SSO" Text="SSO"/>
	                                        <asp:ListItem value="Timer" Text="Timer"/>
	                                        <asp:ListItem value="Topology" Text="Topology"/>
	                                        <asp:ListItem value="Unified Logging Service" Text="Unified Logging Service"/>
	                                        <asp:ListItem value="Upgrade" Text="Upgrade"/>
	                                        <asp:ListItem value="User Profiles" Text="User Profiles"/>
	                                        <asp:ListItem value="Web Controls" Text="Web Controls"/>
	                                        <asp:ListItem value="Web Parts" Text="Web Parts"/>
	                                        <asp:ListItem value="WebParts" Text="WebParts"/>
	                                        <asp:ListItem value="Workflow" Text="Workflow"/>
	                                     </asp:DropDownList>
							            </Template_control>
						            </wssuc:InputFormControl>
						            </td></tr>
						            <tr><td>
						            <wssuc:InputFormControl runat="server" LabelText="AND this event severity..." LabelAssociatedControlId="listEvent" >
							            <Template_control>
								            <asp:DropDownList id="listEvent" runat="server" EnableViewState="true">
									            <asp:ListItem Value="empty" Text=""/>
									            <asp:ListItem Value="Error" Text="Error"/>
									            <asp:ListItem Value="Warning" Text="Warning"/>
									            <asp:ListItem Value="Failure" Text="Failure"/>
									            <asp:ListItem Value="Critical" Text="Critical"/>
									            <asp:ListItem Value="Success" Text="Success"/>
									            <asp:ListItem Value="Information" Text="Information"/>
								            </asp:DropDownList>
							            </Template_control>
						            </wssuc:InputFormControl>
						            </td></tr>
						            <tr><td>
						            <wssuc:InputFormControl runat="server" LabelText="OR this trace severity" LabelAssociatedControlId="listTrace" >
							            <Template_control>
								            <asp:DropDownList id="listTrace" runat="server" EnableViewState="true">
									            <asp:ListItem Value="empty" Text=""/>
									            <asp:ListItem Value="Unexpected" Text="Unexpected"/>
									            <asp:ListItem Value="Monitorable" Text="Monitorable"/>
									            <asp:ListItem Value="High" Text="High"/>
									            <asp:ListItem Value="Medium" Text="Medium"/>
									            <asp:ListItem Value="Verbose" Text="Verbose"/>
								            </asp:DropDownList>
							            </Template_control>
						            </wssuc:InputFormControl>
						            </td></tr>					            
						        </TABLE>
		            </template_inputformcontrols>
                </wssuc:InputFormSection>
                <!-- This section contains a button to show the logs -->
                <wssuc:InputFormSection Title="Show the log" runat="server">
                    <template_description>
			            <asp:Literal Id="Literal3" runat="server" text="Press the button to view the selected log."/><br />
			            <asp:Literal Id="Literal4" runat="server" text="Note: If the log file is large, it can take some time to process and display."/>
		            </template_description>
                    <template_inputformcontrols>
			            <wssuc:InputFormControl runat="server" LabelText="">
				            <Template_control>
                                <asp:Button Id="buttonGo" Text="Go" Width="60px" runat="server" onclick="FillGrid"/>
				            </Template_control>
			            </wssuc:InputFormControl>
		            </template_inputformcontrols>
                </wssuc:InputFormSection>
            </td>
        </tr>
    </table>
    <!-- This table shows the selected log entries -->
    <table border="0" cellspacing="0" cellpadding="0" class="ms-propertysheet" width="100%">
        <tr>
            <td>
                <asp:Label runat="server" ID="message" class="ms-error" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid runat="server" ID="gridLog" Width="100%" class="ms-descriptionText"
                    GridLines="Horizontal" />
            </td>
        </tr>
    </table>
</asp:Content>

<script runat="server">

    Protected Sub Page_Load( _
    ByVal sender As Object, ByVal e As EventArgs)
        
        If Not Page.IsPostBack Then
            
            Try
                'Open the farm
                Dim objFarm As SPFarm = SPFarm.Open("Data Source=LITWARESERVER;Initial Catalog=SharePoint_Config;Integrated Security=SSPI")
                
                'Get reference to ULS
                Dim objDiagnostics As SPDiagnosticsService = _
                New SPDiagnosticsService("log", objFarm)
                Dim strFiles As String() = _
                Directory.GetFiles(objDiagnostics.LogLocation, "*.log")
                
                'Show list of logs
                Dim tblLines As DataTable = New DataTable("Lines")
                tblLines.Columns.Add("Name", GetType(String))
                tblLines.Columns.Add("Path", GetType(String))
                
                Dim i As Integer = strFiles.Length - 1
                
                For i = strFiles.Length - 1 To 0 Step -1
                    If strFiles(i).IndexOf("Diagnostics") = -1 Then
                        Dim strLine As String() = _
                        {strFiles(i).Substring( _
                        strFiles(i).LastIndexOf("\") + 1), strFiles(i)}
                        tblLines.Rows.Add(strLine)
                    End If
                Next

                listFiles.DataSource = tblLines
                listFiles.DataTextField = "Name"
                listFiles.DataValueField = "Path"
                listFiles.DataBind()
                listFiles.Items.Insert(0, "Select a file...")
                
            Catch x As Exception
                message.Text = x.Message
            End Try
            
        End If
        
    End Sub

    Protected Sub FillGrid( _
    ByVal sender As Object, ByVal e As EventArgs)
        
        Try
            
            message.Text = ""
            
            'Create table
            Dim tblEntries As DataTable = New DataTable("Entries")
            tblEntries.Columns.Add("Timestamp", GetType(String))
            tblEntries.Columns.Add("Process", GetType(String))
            tblEntries.Columns.Add("TID", GetType(String))
            tblEntries.Columns.Add("Area", GetType(String))
            tblEntries.Columns.Add("Category", GetType(String))
            tblEntries.Columns.Add("EventID", GetType(String))
            tblEntries.Columns.Add("Level", GetType(String))
            tblEntries.Columns.Add("Message", GetType(String))
            tblEntries.Columns.Add("Correlation", GetType(String))
            
            'Fill table
            Dim objReader As StreamReader = _
            New StreamReader(listFiles.SelectedValue)
            objReader.ReadLine.Split( _
            New Char() {Microsoft.VisualBasic.Chr(9)})
            
            While Not objReader.EndOfStream
                
                Dim strFields As String() = objReader.ReadLine.Split( _
                New Char() {Microsoft.VisualBasic.Chr(9)})
                
                'Category selected
                If Not (listCategories.Text = "empty") Then
                    
                    'Category and level selected
                    If Not (listEvent.Text = "empty") _
                    OrElse Not (listTrace.Text = "empty") Then
                        If _
                        strFields(4).IndexOf(listCategories.Text) >= 0 _
                        AndAlso _
                        (strFields(6).IndexOf(listEvent.Text) >= 0 _
                        OrElse _
                        strFields(6).IndexOf(listTrace.Text) >= 0) Then
                            tblEntries.Rows.Add(strFields)
                        End If
                        'Category, but no level selected
                    Else
                        If _
                        strFields(4).IndexOf(listCategories.Text) >= 0 _
                        Then
                            tblEntries.Rows.Add(strFields)
                        End If
                    End If
                    
                    'Category not selected
                Else
                    
                    'Level selected
                    If Not (listEvent.Text = "empty") _
                    OrElse Not (listTrace.Text = "empty") Then
                        If _
                        strFields(6).IndexOf(listEvent.Text) >= 0 _
                        OrElse _
                        strFields(6).IndexOf(listTrace.Text) >= 0 Then
                            tblEntries.Rows.Add(strFields)
                        End If
                        'Nothing selected
                    Else
                        tblEntries.Rows.Add(strFields)
                    End If
                End If
                
            End While
            
            gridLog.DataSource = tblEntries
            gridLog.DataBind()
            
        Catch x As Exception
            message.Text = x.Message
        End Try
        
    End Sub

</script>
