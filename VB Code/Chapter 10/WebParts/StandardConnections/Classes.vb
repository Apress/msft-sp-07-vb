Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.ComponentModel

Namespace StandardConnections

    Public Class TextProvider
        Inherits WebPart
        Implements IWebPartField

        Protected objButton As Button = Nothing
        Protected objText As TextBox = Nothing
        Private strData As String = Nothing

        'Text Property
        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(False), WebDisplayName("Text"), _
        WebDescription("The text to send")> _
        Public Property Text() As String
            Get
                Return strData
            End Get
            Set(ByVal value As String)
                strData = value
            End Set
        End Property

        'Child Controls
        Protected Overloads Overrides Sub CreateChildControls()

            objButton = New Button
            objButton.Text = "Send Data"
            AddHandler objButton.Click, AddressOf button_Click
            Controls.Add(objButton)

            objText = New TextBox
            Controls.Add(objText)

        End Sub

        'Show UI
        Protected Overloads Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)
            objButton.RenderControl(writer)
            objText.RenderControl(writer)
        End Sub

        'Connection description
        <ConnectionProvider("Text")> _
        Public Function ConnectionInterface() As IWebPartField
            Return Me
        End Function

        'Callback object
        Public Sub GetFieldValue(ByVal callback As FieldCallback) Implements System.Web.UI.WebControls.WebParts.IWebPartField.GetFieldValue
            callback.Invoke(objText.Text)
        End Sub

        'Publish schema
        Public ReadOnly Property Schema() As PropertyDescriptor Implements System.Web.UI.WebControls.WebParts.IWebPartField.Schema
            Get
                Return TypeDescriptor.GetProperties(Me)("Text")
            End Get
        End Property

        Sub button_Click(ByVal sender As Object, ByVal e As EventArgs)
            strData = objText.Text
        End Sub

    End Class

    Public Class TextConsumer
        Inherits WebPart

        Private strData As String

        <ConnectionConsumer("Text")> _
        Public Sub GetConnectionInterface(ByVal providerPart As IWebPartField)
            Dim callback As FieldCallback = AddressOf ReceiveField
            providerPart.GetFieldValue(callback)
        End Sub

        Public Sub ReceiveField(ByVal field As Object)
            strData = CType(field, String)
        End Sub

        Protected Overloads Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)
            Try
                writer.Write(strData)
            Catch
                writer.Write("No connection.")
            End Try
        End Sub

    End Class

End Namespace
