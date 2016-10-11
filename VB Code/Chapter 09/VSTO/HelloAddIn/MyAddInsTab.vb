Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Office = Microsoft.Office.Core

' TODO:
' This is an override of the RequestService method in the ThisAddIn class.
' To hook up your custom ribbon uncomment this code.
Partial Public Class ThisAddIn

    Private ribbon As MyAddInsTab

    Protected Overrides Function RequestService(ByVal serviceGuid As Guid) As Object
        If serviceGuid = GetType(Office.IRibbonExtensibility).GUID Then
            If ribbon Is Nothing Then
                ribbon = New MyAddInsTab()
            End If
            Return ribbon
        End If

        Return MyBase.RequestService(serviceGuid)
    End Function

End Class

<ComVisible(True)> _
    Public Class MyAddInsTab
    Implements Office.IRibbonExtensibility

    Private ribbon As Office.IRibbonUI

    Public Sub New()
    End Sub

    Public Function GetCustomUI(ByVal ribbonID As String) As String Implements Office.IRibbonExtensibility.GetCustomUI
        Return GetResourceText("HelloAddIn.MyAddInsTab.xml")
    End Function

#Region "Ribbon Callbacks"

    Public Sub OnLoad(ByVal ribbonUI As Office.IRibbonUI)
        Me.ribbon = ribbonUI
    End Sub

    Public Sub HelloButton_Click(ByVal control As Office.IRibbonControl)
        Dim objRange As Microsoft.Office.Interop.Word.Range = _
        Globals.ThisAddIn.Application.Selection.Range
        objRange.Text = "Hello, World!"
    End Sub

#End Region

#Region "Helpers"

    Private Shared Function GetResourceText(ByVal resourceName As String) As String
        Dim asm As Assembly = Assembly.GetExecutingAssembly()
        Dim resourceNames() As String = asm.GetManifestResourceNames()
        For i As Integer = 0 To resourceNames.Length - 1
            If String.Compare(resourceName, resourceNames(i), StringComparison.OrdinalIgnoreCase) = 0 Then
                Using resourceReader As StreamReader = New StreamReader(asm.GetManifestResourceStream(resourceNames(i)))
                    If resourceReader IsNot Nothing Then
                        Return resourceReader.ReadToEnd()
                    End If
                End Using
            End If
        Next
        Return Nothing
    End Function

#End Region

End Class
