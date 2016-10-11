Imports MileageCalculator.ExcelWebServices
Imports System.Web.Services.Protocols

Public Class Form1

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim CalcSheet As ExcelService = New ExcelService
        Dim OutStatus As Status()
        Dim objRangeCoordinates As RangeCoordinates = New RangeCoordinates
        Dim strSheetName As String = "Sheet1"
        Dim strTargetWorkbookPath As String = "http://litwareserver:2401/Docs/Expense%20Calculators/MileageCalculator.xlsx"
        CalcSheet.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            Dim id As String = CalcSheet.OpenWorkbook(strTargetWorkbookPath, "en-US", "en-US", OutStatus)
            Dim rateCell As Object = CalcSheet.GetCell(id, strSheetName, 1, 1, True, OutStatus)
            CalcSheet.SetCell(id, strSheetName, 1, 0, numMileage.Value)
            CalcSheet.Calculate(id, strSheetName, objRangeCoordinates)
            Dim reimbursementCell As Object = CalcSheet.GetCell(id, strSheetName, 1, 2, True, OutStatus)
            numRate.Text = rateCell.ToString
            numReimbursement.Text = reimbursementCell.ToString
            CalcSheet.CloseWorkbook(id)
        Catch x As Exception
            MessageBox.Show(x.Message)
        End Try

    End Sub
End Class
