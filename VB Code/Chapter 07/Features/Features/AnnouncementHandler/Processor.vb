Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SharePoint

Public Class Processor
    Inherits SPItemEventReceiver

    Public Overrides Sub ItemAdding( _
    ByVal properties As _
    Microsoft.SharePoint.SPItemEventProperties)
        properties.AfterProperties("Body") &= _
        vbCrLf & "**For internal use only **" & vbCrLf
    End Sub

    Public Overrides Sub ItemDeleting( _
    ByVal properties As _
    Microsoft.SharePoint.SPItemEventProperties)
        properties.Cancel = True
        properties.ErrorMessage = "Items cannot be deleted from this list."
    End Sub

End Class
