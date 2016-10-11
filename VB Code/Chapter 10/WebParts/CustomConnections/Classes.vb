Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Namespace CustomConnections

    Public Interface IStringConnection

        ReadOnly Property ProvidedString() As String
    End Interface

    Public Class StringProvider
        Inherits WebPart
        Implements IStringConnection

        Protected strString As String = "Test Data"

        <ConnectionProvider("String Provider")> _
        Public Function ConnectionInterface() As IStringConnection
            Return Me
        End Function

        Public ReadOnly Property ProvidedString() As String _
        Implements IStringConnection.ProvidedString
            Get
                Return strString
            End Get
        End Property

    End Class

    Public Class StringConsumer
        Inherits WebPart
        Private objProviderPart As IStringConnection = Nothing

        <ConnectionConsumer("String Consumer")> _
        Public Sub GetConnectionInterface( _
        ByVal objProviderPart As IStringConnection)
            objProviderPart = objProviderPart
        End Sub

        Protected Overloads Overrides Sub RenderContents( _
        ByVal writer As HtmlTextWriter)
            Try
                writer.Write(objProviderPart.ProvidedString)
            Catch
                writer.Write("No Connection")
            End Try
        End Sub

    End Class

End Namespace
