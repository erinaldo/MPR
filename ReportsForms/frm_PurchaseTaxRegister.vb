Imports Microsoft.Office.Interop
Imports MMSPlus

Public Class frm_PurchaseTaxRegister
    Implements IForm
    Dim objCommFunction As New CommonClass

    Dim Qry As String
    Dim _rights As Form_Rights


    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub BindData()

        'Qry = "SELECT ROW_NUMBER()OVER(ORDER BY [Bill Date])AS SrNo, * FROM dbo.VWPurchaseRegister
        '        WHERE  MONTH([Bill Date])= " + txtFromDate.Value.Month.ToString() & "
        '        And Year([Bill Date]) = " + txtFromDate.Value.Year.ToString() &
        '        " Order by [Bill Date]"

        Qry = "SELECT ROW_NUMBER()OVER(ORDER BY [Bill Date])AS SrNo, * FROM dbo.VWPurchaseRegister
                WHERE  cast([Bill Date] AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
                " Order by [Bill Date]"

        Dim dt As DataTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        grdTaxReport.DataSource = dt


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


    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        objCommFunction.ExportGridToExcel(grdTaxReport)
    End Sub

    Private Sub btnGenerate_Click_1(sender As Object, e As EventArgs) Handles btnGenerate.Click
        BindData()
    End Sub
End Class
