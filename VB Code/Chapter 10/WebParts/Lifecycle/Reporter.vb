Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Namespace SPLifecycle

    Public Class Reporter
        Inherits WebPart

        'Variable for reporting events
        Private strReport As String = ""

        'UI controls
        Private objButton As Button
        Private objText As TextBox

        Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs)

            strReport += "OnInit<br/>"
            MyBase.OnInit(e)

        End Sub

        Protected Overloads Overrides Sub LoadViewState(ByVal savedState As Object)

            strReport += "LoadViewState<br/>"

            Dim viewState As Object() = Nothing

            If Not (savedState Is Nothing) Then
                viewState = CType(savedState, Object())
                MyBase.LoadViewState(viewState(0))
                strReport += CType(viewState(1), String) + "<br/>"
            End If

        End Sub

        Protected Overloads Overrides Sub CreateChildControls()

            strReport += "CreateChildControls<br/>"

            objButton = New Button
            objButton.Text = "Push Me!"

            AddHandler objButton.Click, AddressOf m_button_Click
            Controls.Add(objButton)
            objText = New TextBox
            Controls.Add(objText)

        End Sub

        Protected Overloads Overrides Sub OnLoad(ByVal e As EventArgs)

            strReport += "OnLoad<br/>"
            MyBase.OnLoad(e)

        End Sub

        Sub m_button_Click(ByVal sender As Object, ByVal e As EventArgs)

            strReport += "Button Click<br/>"

        End Sub

        Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)

            strReport += "OnPreRender<br/>"
            MyBase.OnPreRender(e)

        End Sub

        Protected Overloads Overrides Function SaveViewState() As Object

            strReport += "SaveViewState<br/>"

            Dim viewState(2) As Object
            viewState(0) = MyBase.SaveViewState
            viewState(1) = "myData"
            Return viewState

        End Function

        Protected Overloads Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)

            strReport += "RenderContents<br/>"

            writer.Write(strReport)
            objText.RenderControl(writer)
            objButton.RenderControl(writer)

        End Sub

        Public Overloads Overrides Sub Dispose()
            MyBase.Dispose()
        End Sub

        Protected Overloads Overrides Sub OnUnload(ByVal e As EventArgs)
            MyBase.OnUnload(e)
        End Sub


    End Class

End Namespace