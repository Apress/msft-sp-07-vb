Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Namespace MyNameSpace

    Public Class MyWebPart
        Inherits WebPart

        Dim strConnection As String

        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(True), _
        WebDisplayName("Connection String"), _
        WebDescription("The connection string for the database.")> _
        Public Property Connection() As String
            Get
                Return strConnection
            End Get
            Set(ByVal value As String)
                strConnection = value
            End Set
        End Property

        Protected txtDisplay As TextBox
        Protected WithEvents btnGo As Button

        Protected Overrides Sub CreateChildControls()

            Me.Controls.Add(btnGo)

            txtDisplay.Width = Unit.Percentage(100)
            Me.Controls.Add(txtDisplay)

        End Sub

        Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
            txtDisplay.Text = "Button clicked!"
        End Sub

        Protected Overrides Sub RenderContents(ByVal writer As System.Web.UI.HtmlTextWriter)
            writer.Write("<table border=""0"" width=""100%"">")
            writer.Write("<tr><td>")
            btnGo.RenderControl(writer)
            writer.Write("</td></tr>")
            writer.Write("<tr><td>")
            txtDisplay.RenderControl(writer)
            writer.Write("</td></tr>")
            writer.Write("</table>")
        End Sub

    End Class

End Namespace
