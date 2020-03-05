Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms

Public Class frm_ANX2
    Implements IForm

    Dim objCommFunction As New CommonClass
    Dim row As DataRow
    Dim rowRCM As DataRow
    Private stateId As String
    Dim Qry As String
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_GSTR_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' stateId = objCommFunction.ExecuteScalar("SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT TOP 1 CITY_ID FROM dbo.DIVISION_SETTINGS)")
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        ImportData()
    End Sub

    Private Sub ImportData()
        Dim path As String = System.Windows.Forms.Application.StartupPath + "\ExcelTemplate\PurchaseRegister_Template.xlsx"

        Dim sfd As New SaveFileDialog
        sfd.CheckFileExists = False
        If sfd.ShowDialog <> System.Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        'Dim xlApp As Excel.Application
        'Dim xlWorkBook As Excel.Workbook
        'xlApp = New Excel.ApplicationClass


        Dim xlApp As Object 'Excel.Application
        Dim xlWorkBook As Object 'Excel.Workbook

        xlApp = CreateObject("Excel.Application")

        xlWorkBook = xlApp.Workbooks.Open(path)

        Try
            WritePurchaseData(xlWorkBook)
            xlWorkBook.SaveAs(sfd.FileName)
        Finally
            xlWorkBook.Close(SaveChanges:=False)
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            MsgBox("Purchase Register exported successfully", MsgBoxStyle.Information, gblMessageHeading)
        End Try
    End Sub

    Private Sub WritePurchaseData(xlWorkBook As Object)
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("Purchase Register")

        xlWorkSheet = AddBasicPurchaseData(xlWorkSheet)
        WritePurchaseRegisterData(xlWorkSheet)

    End Sub

    Private Function AddBasicPurchaseData(xlWorkSheet As Object) As Object
        row = GetBasicData()
        xlWorkSheet.Cells(1, 3) = row("TIN_NO").ToString

        Qry = "SELECT YEAR(financialyear_dt) FROM dbo.Company_Master"
        Dim fydate As Int32 = objCommFunction.FillDataSet(Qry).Tables(0).Rows(0)(0)

        xlWorkSheet.Cells(1, 5) = fydate.ToString & "-" & (fydate + 1).ToString.Substring(2, 2)

        xlWorkSheet.Cells(2, 3) = row("DIVISION_NAME")

        If (txtFromDate.Value.ToString("MMMM") = txtToDate.Value.ToString("MMMM")) Then
            xlWorkSheet.Cells(2, 5) = txtFromDate.Value.ToString("MMMM")
        Else
            xlWorkSheet.Cells(2, 5) = txtFromDate.Value.ToString("MMMM") & "-" & txtToDate.Value.ToString("MMMM")
        End If

        Return xlWorkSheet
    End Function

    Dim PurchaseRegisterTable As DataTable
    Private Sub WritePurchaseRegisterData(xlWorkBook As Object)

        Qry = "  SELECT * FROM VW_GST_PurchaseRegister WHERE CAST(BillDate AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) ORDER BY BillDate, ID"

        PurchaseRegisterTable = objCommFunction.FillDataSet(Qry).Tables(0)

        If PurchaseRegisterTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 6

        For Each row As DataRow In PurchaseRegisterTable.Rows
            xlWorkBook.Cells(rowIndex, 1) = row("VAT_NO")
            xlWorkBook.Cells(rowIndex, 2) = row("ACC_NAME")
            xlWorkBook.Cells(rowIndex, 3) = "B2B" '
            xlWorkBook.Cells(rowIndex, 4) = row("DOC_TYPE")
            xlWorkBook.Cells(rowIndex, 5) = row("BillNumber")
            xlWorkBook.Cells(rowIndex, 6) = Convert.ToDateTime(row("BillDate")).ToString("dd-MM-yyyy")
            xlWorkBook.Cells(rowIndex, 7) = row("Taxable_Value")
            'xlWorkSheet.Cells(rowIndex, 8) = ""
            xlWorkBook.Cells(rowIndex, 9) = row("IGST")

            If row("GST") > 0 Then
                xlWorkBook.Cells(rowIndex, 10) = row("GST") / 2
                xlWorkBook.Cells(rowIndex, 11) = row("GST") / 2
            Else
                xlWorkBook.Cells(rowIndex, 10) = 0
                xlWorkBook.Cells(rowIndex, 11) = 0
            End If

            xlWorkBook.Cells(rowIndex, 12) = row("CESS")
            rowIndex += 1
        Next

    End Sub

    Private Function GetBasicData() As DataRow
        Qry = "SELECT DIVISION_NAME, TIN_NO  FROM dbo.DIVISION_SETTINGS"
        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Sub NewClick(sender As Object, e As EventArgs) Implements IForm.NewClick
    End Sub

    Public Sub SaveClick(sender As Object, e As EventArgs) Implements IForm.SaveClick
    End Sub

    Public Sub CloseClick(sender As Object, e As EventArgs) Implements IForm.CloseClick
    End Sub

    Public Sub DeleteClick(sender As Object, e As EventArgs) Implements IForm.DeleteClick
    End Sub

    Public Sub ViewClick(sender As Object, e As EventArgs) Implements IForm.ViewClick
    End Sub

    Public Sub RefreshClick(sender As Object, e As EventArgs) Implements IForm.RefreshClick
    End Sub

End Class
