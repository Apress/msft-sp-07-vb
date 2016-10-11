Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.IO.Packaging

Module Module1

    Sub Main(ByVal args() As String)
        Const wordSpace As String = _
        "http://schemas.openxmlformats.org/wordprocessingml/2006/main"

        Try
            Using objPackage As Package = _
            objPackage.Open(args(0), FileMode.Open, FileAccess.ReadWrite)

                'Get the document part and load it into XML document
                Dim objUri As Uri = _
                New Uri("/word/document.xml", UriKind.Relative)
                Dim objPart As PackagePart = objPackage.GetPart(objUri)
                Dim objStream As Stream = _
                objPackage.GetPart(objUri).GetStream( _
                FileMode.Open, FileAccess.ReadWrite)
                Dim objNameTable As NameTable = New NameTable
                Dim objManager As XmlNamespaceManager = _
                New XmlNamespaceManager(objNameTable)

                objManager.AddNamespace("w", wordSpace)

                Dim objXMLDoc As XmlDocument = New XmlDocument(objNameTable)
                objXMLDoc.Load(objStream)

                'Show the text
                Dim objNodes As XmlNodeList = _
                objXMLDoc.SelectNodes("//w:t", objManager)
                For Each objNode As XmlNode In objNodes
                    Console.WriteLine(objNode.InnerText)
                Next

            End Using

        Catch x As Exception
            Console.WriteLine(x.Message)
        End Try

    End Sub

End Module
