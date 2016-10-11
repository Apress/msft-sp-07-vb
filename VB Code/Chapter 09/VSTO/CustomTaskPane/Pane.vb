Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class NamePane

    Private Sub Pane_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lstItems.Items.Add("Microsoft SharePoint")
        lstItems.Items.Add("Microsoft Exchange")
        lstItems.Items.Add("Microsoft Windows")
        lstItems.Items.Add("Microsoft Vista")

        'Try

        '    Dim connString As String = _
        '    "Data Source=win2k3template;Initial Catalog=Adventureworks;Integrated Security=SSPI;"
        '    Dim sqlString As String = _
        '    "Select Name + ', ' + ProductNumber as FullProduct FROM Production.Product ORDER BY Name"

        '    Using objConnection As New SqlConnection(connString)

        '        objConnection.Open()

        '        Dim objCommand As New SqlCommand
        '        With objCommand
        '            .CommandText = sqlString
        '            .CommandType = CommandType.Text
        '            .Connection = objConnection
        '        End With

        '        Dim objreader As SqlDataReader = objCommand.ExecuteReader()

        '        If objreader.HasRows Then
        '            While objreader.Read()
        '                lstItems.Items.Add(objreader.GetString(0))
        '            End While
        '        End If

        '        objConnection.Close()
        '    End Using

        'Catch x As Exception
        '    MessageBox.Show(x.Message)
        'End Try


    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim objRange As Word.Range = Globals.ThisAddIn.Application.Selection.Range
        objRange.Text = lstItems.SelectedItem.ToString
    End Sub

End Class
