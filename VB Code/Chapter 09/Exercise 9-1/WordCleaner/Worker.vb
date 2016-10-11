Imports System
Imports System.Xml
Imports System.IO
Imports System.IO.Packaging

Public Class Worker

    Private Const strWordSpace As String = _
    "http://schemas.openxmlformats.org/wordprocessingml/2006/main"
    Private Const strDocUri As String = "/word/document.xml"

    Public Sub Sanitize(ByVal packagePath As String)
        Try

            Using objPackage As Package = _
            objPackage.Open(packagePath, FileMode.Open, FileAccess.ReadWrite)

                'Get the Document Part
                Dim objUriDocument As Uri = _
                New Uri(strDocUri, UriKind.Relative)

                Dim objDocumentPart As PackagePart = _
                objPackage.GetPart(objUriDocument)

                'Load the Document Part into a Stream
                Dim objPartStream As Stream = _
                objDocumentPart.GetStream(FileMode.Open, FileAccess.ReadWrite)

                'Add the Namespace manager
                Dim objNameTable As NameTable = New NameTable
                Dim objManager As XmlNamespaceManager = _
                New XmlNamespaceManager(objNameTable)

                objManager.AddNamespace("w", strWordSpace)

                'Create a temporary XML document
                'To manipulate the Word document elements
                Dim objTempDoc As XmlDocument = New XmlDocument(objNameTable)
                objTempDoc.Load(objPartStream)

                'Remove deleted text
                Dim objDelNodes As XmlNodeList = _
                objTempDoc.SelectNodes("//w:del", objManager)

                For Each objDelNode As XmlNode In objDelNodes
                    objDelNode.ParentNode.RemoveChild(objDelNode)
                Next

                'Accept changes to inserted text
                Dim objInsNodes As XmlNodeList = _
                objTempDoc.SelectNodes("//w:ins", objManager)

                For Each objInsNode As XmlNode In objInsNodes
                    For Each objChildNode As XmlNode In objInsNode.ChildNodes
                        objInsNode.ParentNode.InsertBefore(objChildNode, objInsNode)
                    Next
                    objInsNode.ParentNode.RemoveChild(objInsNode)
                Next

                'Remove comments from document
                Dim objCommentStartNodes As XmlNodeList = _
                objTempDoc.SelectNodes("//w:commentRangeStart", objManager)

                For Each objCommentStartNode As XmlNode In objCommentStartNodes
                    objCommentStartNode.ParentNode.RemoveChild(objCommentStartNode)
                Next

                Dim objCommentEndNodes As XmlNodeList = _
                objTempDoc.SelectNodes("//w:commentRangeEnd", objManager)

                For Each objCommentEndNode As XmlNode In objCommentEndNodes
                    objCommentEndNode.ParentNode.RemoveChild(objCommentEndNode)
                Next

                Dim objCommentRefNodes As XmlNodeList = _
                objTempDoc.SelectNodes("//w:commentReference", objManager)

                For Each objCommentRefNode As XmlNode In objCommentRefNodes
                    objCommentRefNode.ParentNode.RemoveChild(objCommentRefNode)
                Next

                'Save the document
                objDocumentPart.GetStream.SetLength(0)
                objTempDoc.Save(objDocumentPart.GetStream)

                'Delete relationship woth comment part
                Dim objUriComments As Uri = _
                New Uri("/word/comments.xml", UriKind.Relative)

                Dim objCommentsPart As PackagePart = _
                objPackage.GetPart(objUriComments)

                Dim objRelationships As PackageRelationshipCollection = _
                objDocumentPart.GetRelationships

                For Each objRelationship As PackageRelationship In objRelationships
                    If objRelationship.TargetUri.ToString = "comments.xml" Then
                        objDocumentPart.DeleteRelationship(objRelationship.Id)
                    End If
                Next

                'Delete comments from the package
                objPackage.DeletePart(objUriComments)
                objPackage.Flush()
                objPackage.Close()

            End Using

        Catch x As Exception
            LogMessage(x.Message, EventLogEntryType.Error)
        End Try
    End Sub

    Shared Sub LogMessage(ByVal message As String, ByVal entry As EventLogEntryType)
        If Not EventLog.SourceExists("Word Cleaner") Then
            EventLog.CreateEventSource("Word Cleaner", "Application")
        End If
        EventLog.WriteEntry("Word Cleaner", message, entry)
    End Sub
End Class
