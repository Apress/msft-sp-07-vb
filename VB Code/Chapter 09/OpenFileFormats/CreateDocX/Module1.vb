Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.IO.Packaging


Module Module1
    Private Const strWordSpace As String = _
    "http://schemas.openxmlformats.org/wordprocessingml/2006/main"
    Private Const strPartType As String = _
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"
    Private Const strPartUri As String = _
    "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"
    Private Const strPartId As String = "rId1"

    Sub Main(ByVal args As String())
        Try
            Using objPackage As Package = _
            objPackage.Open(args(0), FileMode.CreateNew, FileAccess.ReadWrite)

                Dim objDocumentXml As XmlDocument = New XmlDocument
                Dim objDocumentElement As XmlElement = _
                objDocumentXml.CreateElement("w:document", strWordSpace)
                objDocumentXml.AppendChild(objDocumentElement)

                Dim bodyElement As XmlElement = _
                objDocumentXml.CreateElement("w:body", strWordSpace)
                objDocumentElement.AppendChild(bodyElement)

                Dim pElement As XmlElement = _
                objDocumentXml.CreateElement("w:p", strWordSpace)
                bodyElement.AppendChild(pElement)

                Dim rElement As XmlElement = _
                objDocumentXml.CreateElement("w:r", strWordSpace)
                pElement.AppendChild(rElement)

                Dim tElement As XmlElement = _
                objDocumentXml.CreateElement("w:t", strWordSpace)
                rElement.AppendChild(tElement)

                Dim tNode As XmlNode = _
                objDocumentXml.CreateNode(XmlNodeType.Text, "w:t", strWordSpace)
                tNode.Value = args(1)
                tElement.AppendChild(tNode)

                Dim Uri As Uri = _
                New Uri("/word/document.xml", UriKind.Relative)

                Dim objPartDocumentXML As PackagePart = _
                objPackage.CreatePart(Uri, strPartType)

                Dim objStream As StreamWriter = _
                New StreamWriter(objPartDocumentXML.GetStream( _
                FileMode.Create, FileAccess.Write))

                objDocumentXml.Save(objStream)
                objStream.Close()
                objPackage.Flush()

                objPackage.CreateRelationship(Uri, TargetMode.Internal, strPartUri, strPartId)
                objPackage.Flush()
                objPackage.Close()

            End Using
        Catch x As Exception
            Console.WriteLine(x.Message)
        End Try
    End Sub
End Module
