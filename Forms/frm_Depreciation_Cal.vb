Imports MMSPlus

Public Class frm_Depreciation_Cal
    Implements IForm

    Dim obj As New CommonClass
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Depreciation_Cal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeControls()
    End Sub

    Private Sub InitializeControls()
        obj.FormatGrid(dgvDepreciation)
        SetFYText()
        BindGrid()
    End Sub

    Dim dataTable As DataTable

    Private Sub BindGrid()
        Dim qry As String = "SELECT ACC_ID as [Acc. Id], ACC_NAME as [Acc. Name], OPENING_BAL as [Opening Bal.]" &
             " , SUM(CASE WHEN cast(ld.TransactionDate as date) <= '" + semiYearDate + "' Then CashIn Else 0 End) As [1st Half]" &
             " , SUM(CASE WHEN cast(ld.TransactionDate as date) > '" + semiYearDate + "' THEN CashIn ELSE 0 END) AS [2nd Half], SUM(ISNULL(CashOut,0)) AS Sold" &
             " FROM dbo.ACCOUNT_MASTER am LEFT OUTER JOIN dbo.LedgerMaster lm On lm.AccountId = am.ACC_ID " &
             " LEFT OUTER JOIN dbo.LedgerDetail ld ON ld.LedgerId=lm.LedgerId And (ld.TransactionId > 0 Or ld.TransactionId Is NULL)" &
             " WHERE AG_ID=" + Convert.ToString(AccountGroups.Fixed_Assets) &
             " GROUP BY ACC_ID, ACC_NAME, OPENING_BAL"
        dataTable = obj.FillDataSet(qry).Tables(0)
        dataTable.Columns.Add("Final Value")
        dataTable.Columns.Add("Depreciation")
        dataTable.Columns.Add("WDV")
        CalculateDepreciation()
        dgvDepreciation.DataSource = dataTable
    End Sub

    Private Sub CalculateDepreciation()
        Dim rod As Decimal = 10
        For Each row As DataRow In dataTable.Rows
            row("Final Value") = row("Opening Bal.") + row("1st Half") + row("2nd Half") - row("Sold")
            Dim firstDep As Decimal = rod * row("1st Half") / 100
            Dim secondDep As Decimal = rod * row("2nd Half") / 100 / 2
            Dim lastYearDep As Decimal = rod * (row("Opening Bal.") - row("Sold")) / 100
            lastYearDep = IIf(lastYearDep >= 0, lastYearDep, 0)
            row("Depreciation") = firstDep + secondDep + lastYearDep
            row("WDV") = row("Final Value") - row("Depreciation")
        Next
    End Sub

    Dim semiYearDate As Date
    Dim fyBeginDate As Date
    Private Sub SetFYText()

        Dim currentYear As Int32 = Date.Now.Year
        Dim currentMonth As Int32 = Date.Now.Month
        If currentMonth >= 1 And currentMonth <= 3 Then
            currentYear -= 1
        End If

        lblFY.Text = currentYear.ToString + "-" + (currentYear + 1).ToString
        semiYearDate = New DateTime(currentYear, 10, 3)
        fyBeginDate = New DateTime(currentYear, 4, 1)
        If DateTime.IsLeapYear(currentYear) Then
            semiYearDate = semiYearDate.AddDays(1)
        End If
    End Sub


    Public Sub CloseClick(sender As Object, e As EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(sender As Object, e As EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(sender As Object, e As EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(sender As Object, e As EventArgs) Implements IForm.RefreshClick
        BindGrid()
    End Sub

    Public Sub SaveClick(sender As Object, e As EventArgs) Implements IForm.SaveClick
        ''delete old enteries
        DeleteRecord()
        For Each row As DataRow In dataTable.Rows
            If row("Depreciation") > 0 Then
                InsertRecord(row)
            End If
        Next
        MsgBox("Depreciation information has been Saved.")
    End Sub

    Dim qry As String
    Private Sub DeleteRecord()

        ''delete depreciation detail
        qry = "select DepreciationId, AccountId, DepreciationAmount from DepreciationDetail WHERE FYBeginDate ='" + fyBeginDate + "'"
        Dim deleteRows As DataTable = obj.FillDataSet(qry).Tables(0)

        For Each delRow As DataRow In deleteRows.Rows
            qry = "DELETE FROM DepreciationDetail WHERE DepreciationId = " + delRow("DepreciationId").ToString
            obj.ExecuteNonQuery(qry)

            qry = "UPDATE dbo.LedgerMaster SET AmountInHand = AmountInHand + " + delRow("DepreciationAmount").ToString + " where AccountId=" + delRow("AccountId").ToString
            obj.ExecuteNonQuery(qry)

            qry = "DELETE FROM dbo.LedgerDetail WHERE TransactionId = " + delRow("DepreciationId").ToString + " AND TransactionTypeId = " + Convert.ToString(Transaction_Type.Depreciation)
            obj.ExecuteNonQuery(qry)
        Next

    End Sub

    Private Sub InsertRecord(row As DataRow)

        ''insert depreciation detail
        qry = "SELECT ISNULL(MAX(DepreciationId),0) + 1 from dbo.DepreciationDetail"
        Dim id As Int32 = obj.ExecuteScalar(qry)


        qry = "INSERT INTO dbo.DepreciationDetail " &
            "(DepreciationId, " &
            "AccountId , " &
            "Rate , " &
            "OpeningBalance , " &
            "Add1stHalf , " &
            "Add2ndHalf , " &
            "SoldValue , " &
            "DepreciationAmount , " &
            "FYBeginDate " &
            ") " &
            "VALUES  ( " + id.ToString + " , " &
            row("Acc. Id").ToString + " , " &
            10.ToString + ", " &
            row("Opening Bal.").ToString + ", " &
            row("1st Half").ToString + ", " &
            row("2nd Half").ToString + ", " &
            row("Sold").ToString + ", " &
            row("Depreciation").ToString + ", " &
            "'" + fyBeginDate + "'  " &
            ")"
        obj.ExecuteNonQuery(qry)

        qry = "EXECUTE Proc_Ledger_Insert " + row("Acc. Id").ToString + ", 0, " + row("Depreciation").ToString +
            ", 'Depreciation', " + v_the_current_division_id.ToString + ", " + id.ToString + ", " +
            Convert.ToString(Transaction_Type.Depreciation) + ", '" + v_the_current_logged_in_user_name + "'"
        obj.ExecuteNonQuery(qry)
    End Sub

    Public Sub ViewClick(sender As Object, e As EventArgs) Implements IForm.ViewClick

    End Sub
End Class
