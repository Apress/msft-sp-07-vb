Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualStudio.Tools.Applications.Runtime
Imports Word = Microsoft.Office.Interop.Word
Imports Office = Microsoft.Office.Core

public class ThisAddIn

    Dim objPane As NamePane

    Private Sub ThisAddIn_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup
        objPane = New NamePane()
        Dim objTaskPane As Microsoft.Office.Tools.CustomTaskPane = _
        Me.CustomTaskPanes.Add(objPane, "Names")
        objTaskPane.Visible = True
    End Sub

    Private Sub ThisAddIn_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

End class
