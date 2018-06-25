Imports MMSPlus.GatePass
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text.RegularExpressions
Public Class frm_OpeningBalance

    Implements IForm
    Dim obj As New CommonClass
    Dim OpeningBalanceId As Int16
    Dim flag As String
    Dim clsObj As New cls_opening_balance
    Dim prpty As cls_opening_balance_prop
    Dim dtable_Item_List As DataTable
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_OpeningBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clsObj.ComboBind(cmbAccountGroup, "select 0 as AG_ID,'--Select--' as AG_NAME union Select AG_ID,AG_NAME from ACCOUNT_GROUPS Order by AG_NAME ", "AG_NAME", "AG_ID")
        BindCustomerCombo()
        cmbopbaltype.Text = "Dr."
        FillGrid()
        ClearControl()
    End Sub

    Private Sub FillGrid(Optional ByVal condition As String = "")
        Dim strsql As String
        Try
            strsql = " SELECT  ROW_NUMBER()OVER(ORDER BY Account)AS [Sr. No],* FROM (SELECT OpeningBalanceId, AG_NAME AS [Group Name] , ACC_NAME AS Account," &
            " OPENINGAmount AS [Opening Balance],CONVERT(VARCHAR(20), OpeningDate, 106) AS [Opening Date]" &
            "FROM    openingbalance JOIN dbo.ACCOUNT_MASTER ON openingbalance.fkAccountId = dbo.ACCOUNT_MASTER.ACC_ID" &
            " JOIN dbo.ACCOUNT_GROUPS ON dbo.ACCOUNT_GROUPS.AG_ID = dbo.ACCOUNT_MASTER.AG_ID )tb WHERE [GROUP Name]+[Account]+CAST([Opening Balance] AS VARCHAR(30))+[Opening Date] LIKE '%" & condition & "%'  order by 1"

            Dim dt As DataTable = obj.Fill_DataSet(strsql).Tables(0)

            grdOpeningBalance.DataSource = dt
            grdOpeningBalance.Columns(0).Width = 150
            'grdOpeningBalance.Columns(1).Width = 150
            grdOpeningBalance.Columns(1).Visible = False
            grdOpeningBalance.Columns(2).Width = 150
            grdOpeningBalance.Columns(3).Width = 150
            grdOpeningBalance.Columns(4).Width = 150
            grdOpeningBalance.Columns(5).Width = 150

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Private Sub BindCustomerCombo()

        clsObj.ComboBind(cmbCustomer, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" & cmbAccountGroup.SelectedValue & " Order by ACC_NAME ", "ACC_NAME", "ACC_ID")

    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initilization()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGrid()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        If cmbCustomer.SelectedIndex = 0 Then
            MsgBox("Select Account  to Opening Balance.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        ElseIf txtAmount.Text.Length <= 0 Then
            MsgBox("Enter Account Opening Balance.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        prpty = New cls_opening_balance_prop

        'Dim ds As DataSet = obj.FillDataSet("SELECT COUNT(*) FROM OPENINGBALANCE WHERE fkAccountId=" & cmbCustomer.SelectedValue)
        'Dim id As Int32 = Convert.ToInt32(ds.Tables(0).Rows(0)(0))
        'If id > 0 Then
        '    MsgBox("Selected Account alreday have  Opening Balance.", MsgBoxStyle.Information, gblMessageHeading)
        '    Exit Sub
        'Else

        prpty.OpeningBalanceId = OpeningBalanceId
        prpty.AccountId = Convert.ToDouble(cmbCustomer.SelectedValue)
        prpty.Amount = Convert.ToDouble(txtAmount.Text)
        prpty.DivisionId = v_the_current_division_id
        prpty.CreatedBy = v_the_current_logged_in_user_name
        prpty.OpeningDate = Convert.ToDateTime(dtpOpeningDate.Value)
        If cmbopbaltype.SelectedItem = "Dr." Then
            prpty.Type = 1
        Else
            prpty.Type = 2
        End If

        If (flag = "save") Then
            prpty.Proctype = 1
        Else
            prpty.Proctype = 2
        End If

        prpty.TransactionId = Transaction_Type.Opening_Balance

        clsObj.Add_Account_OpeningBalance(prpty)

        If flag = "save" Then
            MsgBox("Account Opening Balance has been Saved.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
        Else
            MsgBox("Account Opening Balance has been Updated.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
        End If

        ClearControl()
        FillGrid()

        'End If
    End Sub

    Public Sub ClearControl()
        cmbAccountGroup.SelectedValue = "0"
        txtAmount.Text = ""
        dtpOpeningDate.Value = Now
        flag = "save"
    End Sub

    Private Function Validation() As Boolean

        If cmbCustomer.SelectedIndex = 0 Then
            MsgBox("Select Account  to Opening Balance.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        ElseIf txtAmount.Text.Length <= 0 Then
            MsgBox("Enter Account Opening Balance.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If
    End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub new_initilization()

    End Sub

    Private Sub cmbAccountGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccountGroup.SelectedIndexChanged
        BindCustomerCombo()
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        FillGrid(txtSearch.Text)
    End Sub

    Private Sub grdOpeningBalance_DoubleClick(sender As Object, e As EventArgs) Handles grdOpeningBalance.DoubleClick
        flag = "update"
        OpeningBalanceId = Convert.ToInt32(grdOpeningBalance("OpeningBalanceId", grdOpeningBalance.CurrentCell.RowIndex).Value())
        Dim result As Integer = MessageBox.Show("Are you sure you want to edit this Opening Balance ?", "Edit Opening Balance", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            FillPaymentDetails(OpeningBalanceId)
        End If
    End Sub

    Public Sub FillPaymentDetails(OpeningBalanceId As Int16)
        Dim dt As DataTable
        dt = clsObj.fill_Data_set("Proc_GETOpeningBalanceDetailByID_Edit", "@OpeningBalanceId", OpeningBalanceId).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            TabControl1.SelectedIndex = 1
            cmbAccountGroup.SelectedValue = dr("AG_ID")
            cmbCustomer.SelectedValue = dr("FkAccountId")
            txtAmount.Text = dr("OpeningAmount")

            If (dr("TYPE") = 1) Then
                cmbopbaltype.SelectedItem = "Dr."
            Else
                cmbopbaltype.SelectedItem = "Cr."
            End If

        End If
    End Sub

End Class
