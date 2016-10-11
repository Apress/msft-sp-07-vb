Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Namespace SPDataPart

    Public Class Adventure
        Inherits WebPart

        Private objGrid As DataGrid
        Private lblMessages As Label

        Protected strConnection As String

        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(True), WebDisplayName("Connection String"), _
        WebDescription("The connection string for the AdventureWorks database")> _
        Public Property Connection() As String
            Get
                Return strConnection
            End Get
            Set(ByVal value As String)
                strConnection = value
            End Set
        End Property

        'Filter
        Private strLastName As String = Nothing

        <Personalizable(PersonalizationScope.Shared), _
        WebBrowsable(False), WebDisplayName("Last Name"), _
        WebDescription("The last name to use as a filter")> _
        Public Property LastName() As String
            Get
                Return strLastName
            End Get
            Set(ByVal value As String)
                strLastName = value
            End Set
        End Property

        <ConnectionConsumer("Last Name")> _
        Public Sub GetConnectionInterface(ByVal providerPart As IWebPartField)
            Dim callback As FieldCallback = AddressOf ReceiveField
            providerPart.GetFieldValue(callback)
        End Sub

        Public Sub ReceiveField(ByVal field As Object)
            If Not (field Is Nothing) Then
                LastName = field.ToString
            End If
        End Sub

        Protected Overloads Overrides Sub CreateChildControls()

            'Add Grid
            objGrid = New DataGrid
            objGrid.AutoGenerateColumns = False
            objGrid.Width = Unit.Percentage(100)
            objGrid.GridLines = GridLines.Horizontal
            objGrid.HeaderStyle.CssClass = "ms-vh2"
            objGrid.CellPadding = 2

            'Add columns
            Dim column As BoundColumn = New BoundColumn
            column.DataField = "FullName"
            column.HeaderText = "Associate"
            objGrid.Columns.Add(column)

            column = New BoundColumn
            column.DataField = "Title"
            column.HeaderText = "Title"
            objGrid.Columns.Add(column)

            column = New BoundColumn
            column.DataField = "SalesTerritory"
            column.HeaderText = "Territory"
            objGrid.Columns.Add(column)

            column = New BoundColumn
            column.DataField = "2002"
            column.HeaderText = "2002"
            column.DataFormatString = "{0:C}"
            column.ItemStyle.BackColor = Color.Wheat
            objGrid.Columns.Add(column)

            column = New BoundColumn
            column.DataField = "2003"
            column.HeaderText = "2003"
            column.DataFormatString = "{0:C}"
            objGrid.Columns.Add(column)

            column = New BoundColumn
            column.DataField = "2004"
            column.HeaderText = "2004"
            column.DataFormatString = "{0:C}"
            column.ItemStyle.BackColor = Color.Wheat
            objGrid.Columns.Add(column)
            Controls.Add(objGrid)

            lblMessages = New Label
            Controls.Add(lblMessages)

        End Sub

        Protected Overloads Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)

            Dim objDataSet As DataSet = Nothing
            Dim strSQL As String = "SELECT FullName, Title, SalesTerritory, " + "[2002], [2003], [2004] FROM Sales.vSalesPersonSalesByFiscalYears " + "WHERE FullName LIKE '%" + LastName + "'"

            Using objConn As SqlConnection = New SqlConnection(Connection)

                'Get Data
                Try
                    objConn.Open()
                    Dim objAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, objConn)
                    objDataSet = New DataSet("root")
                    objAdapter.Fill(objDataSet, "sales")
                Catch x As SqlException
                    lblMessages.Text = x.Message
                Catch x As Exception
                    lblMessages.Text += x.Message
                End Try

                'Bind Data
                Try
                    objGrid.DataSource = objDataSet
                    objGrid.DataMember = "sales"
                    DataBind()
                Catch x As Exception
                    lblMessages.Text += x.Message
                End Try

            End Using

            'Display Data
            writer.Write("<table border=""0"" width=""100%"">")
            writer.Write("<tr><td>")
            objGrid.RenderControl(writer)
            writer.Write("</td></tr>")
            writer.Write("<tr><td>")
            lblMessages.RenderControl(writer)
            writer.Write("</td></tr>")
            writer.Write("</table>")

        End Sub

    End Class

End Namespace
