'This web part generates a script block that looks like this
'
'<script type="text/javascript">
'<!--
'code
'// -->
'</script>

Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Namespace SPScriptBlock

    Public Class Block
        Inherits WebPart

        Private strScriptBlock As String = ""
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

        'Script property
        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(True), WebDisplayName("Script"), _
        WebDescription("The JavaScript to insert in the page.")> _
        Public Property Script() As String
            Get
                Return strScriptBlock
            End Get
            Set(ByVal value As String)
                strScriptBlock = value
            End Set
        End Property

        Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
            MyBase.OnPreRender(e)
            If Not (strScriptBlock = "") _
            AndAlso _
            Not Page.ClientScript.IsClientScriptBlockRegistered(strScriptKey) Then
                Page.ClientScript.RegisterClientScriptBlock( _
                GetType(String), strScriptKey, strScriptBlock, True)
            End If
        End Sub

    End Class

End Namespace
