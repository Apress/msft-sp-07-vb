Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Text.RegularExpressions

Namespace EditorPartDemo

    Public Class PhoneEditor
        Inherits EditorPart

        Private txtProperty As TextBox = Nothing
        Private lblMessages As Label = Nothing

        Protected Overloads Overrides Sub CreateChildControls()
            txtProperty = New TextBox
            Controls.Add(txtProperty)
            lblMessages = New Label
            Controls.Add(lblMessages)
        End Sub

        Protected Overloads Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)
            txtProperty.RenderControl(writer)
            writer.Write("<br/>")
            lblMessages.RenderControl(writer)
        End Sub

        Public Overloads Overrides Function ApplyChanges() As Boolean
            Try
                Dim expression As Regex = New Regex("\(\d\d\d\)\s\d\d\d-\d\d\d\d")
                Dim match As Match = expression.Match(txtProperty.Text)
                If match.Success = True Then
                    CType(WebPartToEdit, PhoneLabel).Phone = txtProperty.Text
                    lblMessages.Text = ""
                Else
                    CType(WebPartToEdit, PhoneLabel).Phone = "Invalid phone number"
                End If
            Catch x As Exception
                lblMessages.Text += x.Message
            End Try
            Return True
        End Function

        Public Overloads Overrides Sub SyncChanges()

            Try
                txtProperty.Text = CType(WebPartToEdit, PhoneLabel).Phone
            Catch
            End Try

        End Sub

    End Class

    Public Class PhoneLabel
        Inherits WebPart

        Protected strPhone As String

        <Personalizable(PersonalizationScope.Shared), WebBrowsable(False), WebDisplayName("Phone"), WebDescription("Phone number")> _
        Public Property Phone() As String
            Get
                Return strPhone
            End Get
            Set(ByVal value As String)
                strPhone = value
            End Set
        End Property

        Public Overloads Overrides Function CreateEditorParts() As EditorPartCollection

            Dim partsArray As ArrayList = New ArrayList
            Dim phonePart As PhoneEditor = New PhoneEditor
            phonePart.ID = Me.ID + "_editorPart1"
            phonePart.Title = "Phone Number"
            phonePart.GroupingText = "(xxx) xxx-xxxx"
            partsArray.Add(phonePart)
            Dim parts As EditorPartCollection = New EditorPartCollection(partsArray)
            Return parts

        End Function

        Protected Overloads Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)
            writer.Write("<p>" + strPhone + "</p>")
        End Sub

    End Class

End Namespace
