'This part creates the include directive
'<script src="{js file}" type="text/javascript"></script>

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Namespace SPScriptInclude

    Public Class Loader
        Inherits WebPart
        Private strScriptFile As String = ""
        Private strScriptKey As String = "scriptKey"

        'Key property
        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(True), WebDisplayName("Script Key"), _
        WebDescription("A unique key for the script.")> _
        Public Property ScriptKey() As String
            Get
                Return strScriptKey
            End Get
            Set(ByVal value As String)
                strScriptKey = value
            End Set
        End Property

        'File property
        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(True), WebDisplayName("Script File"), _
        WebDescription("The name of the JavaScript file to insert in the page.")> _
        Public Property ScriptFile() As String
            Get
                Return strScriptFile
            End Get
            Set(ByVal value As String)
                strScriptFile = value
            End Set
        End Property

        Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
            MyBase.OnPreRender(e)
            If Not (strScriptFile = "") _
            AndAlso _
            Not Page.ClientScript.IsClientScriptIncludeRegistered(strScriptKey) Then
                Page.ClientScript.RegisterClientScriptInclude( _
                strScriptKey, Page.ResolveClientUrl(strScriptFile))
            End If
        End Sub

    End Class

End Namespace
