Imports System
Imports System.Data.SqlClient
Imports System.Net
Imports System.xml

Public Class Form1

    Private Sub Form1_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strConnection As String = "Integrated Security=SSPI;Initial Catalog=WSS_Content;Data Source=VSSQL"
        Dim strSql As String = "SELECT Title, FullUrl FROM dbo.Webs WHERE (ParentWebId IS NULL) AND (FullUrl <> '') AND (FullUrl IS NOT NULL) ORDER BY Title"

        Try

            Using objConnection As SqlConnection = New SqlConnection(strConnection)

                'Connect to content database
                Dim objAdapter As SqlDataAdapter = New SqlDataAdapter
                Dim objDataSet As DataSet = New DataSet("root")
                objAdapter.SelectCommand = New SqlCommand(strSql, objConnection)
                objAdapter.Fill(objDataSet, "Sites")

                'Get all top-level sites
                Dim objSiteRows As DataRowCollection = objDataSet.Tables("Sites").Rows

                'Show each site
                For Each objSiteRow As DataRow In objSiteRows
                    Dim objTreeNode As TreeNode = New TreeNode
                    objTreeNode.Text = objSiteRow("Title").ToString
                    objTreeNode.Tag = "http://localhost/" + objSiteRow("FullUrl").ToString
                    TreeView1.Nodes.Add(objTreeNode)
                    FillTree(objTreeNode)
                Next

            End Using

        Catch x As Exception
            MessageBox.Show(x.Message)
        End Try

    End Sub

    Private Sub FillTree(ByVal objParentNode As TreeNode)

        'Connect to web service
        Dim objService As MOSSService.Webs = New MOSSService.Webs
        objService.Url = objParentNode.Tag.ToString + "/_vti_bin/Webs.asmx"
        objService.Credentials = CredentialCache.DefaultCredentials

        'Get each sub site
        Dim objNodes As XmlNode = objService.GetWebCollection

        For Each objNode As XmlNode In objNodes
            Dim objChild As TreeNode = New TreeNode
            objChild.Text = objNode.Attributes("Title").Value
            objChild.Tag = objNode.Attributes("Url").Value
            objParentNode.Nodes.Add(objChild)

            'Recurse site collection
            FillTree(objChild)

        Next
    End Sub

End Class
