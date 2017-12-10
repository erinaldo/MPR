Imports MMSPlus

Public Class frm_Account_Payment
    Implements IForm

    Public entryType As Int32 = PaymentType.Journal
    Public entryTypeName As String = PaymentType.Journal.ToString

    Dim clsObj As New cls_Invoice_Settlement
    Dim _rights As Form_Rights
    Dim query As String

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Account_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeControls()
    End Sub

    Private Sub InitializeControls()
        entryTypeName = CType(entryType, PaymentType).ToString
        lblFormHeading.Text = entryTypeName + " Entry"
        query = "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER "
        If entryType = PaymentType.Contra Then
            query += " where AG_ID = " + Convert.ToString(AccountGroups.Bank_Accounts)
        ElseIf entryType = PaymentType.Expense Then
            query += " where AG_ID = " + Convert.ToString(AccountGroups.Bank_Accounts)
        End If
        query += " Order by ACC_NAME"
        clsObj.ComboBind(cmbAccountToDebit, query, "ACC_NAME", "ACC_ID", True)

        query = "Select [PaymentTypeId], PaymentTypeName from [PaymentTypeMaster] WHERE [IsActive_bit] = 1 and IsApprovalRequired_bit=0"
        clsObj.ComboBind(cmbPaymentType, query, "PaymentTypeName", "PaymentTypeId", True)

        fill_ListPaymentgrid()
    End Sub

    Private Sub cmbAccountToDebit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccountToDebit.SelectedIndexChanged
        query = "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER where ACC_ID <> " + cmbAccountToDebit.SelectedValue.ToString
        If entryType = PaymentType.Contra Then
            query += " and AG_ID = " + Convert.ToString(AccountGroups.Bank_Accounts)
        ElseIf entryType = PaymentType.Expense Then
            query += " and AG_ID in (" + Convert.ToString(AccountGroups.Expenses_Direct_Mfg) + ", " + Convert.ToString(AccountGroups.Expenses_Indirect_Admn) + ")"
        End If
        query += " Order by ACC_NAME"
        clsObj.ComboBind(cmbAccountToCredit, query, "ACC_NAME", "ACC_ID", True)
    End Sub

    Private Sub ClearControls()
        cmbAccountToDebit.SelectedIndex = 0
        dtpPaymentDate.Value = DateTime.Now
        cmbPaymentType.SelectedIndex = 0
        txtReferenceNo.Text = ""
        dtpReferenceDate.Value = DateTime.Now
        dtpBankDate.Value = DateTime.Now
        txtAmount.Text = ""
        txtRemarks.Text = ""
    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        ClearControls()
    End Sub

    Dim prpty As cls_Invoice_Settlement_prop
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            If Validation() = False Then
                Exit Sub
            End If

            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If

            prpty = New cls_Invoice_Settlement_prop
            prpty.PaymentTransactionCode = PM_Code & PM_No
            prpty.PaymentTypeId = cmbPaymentType.SelectedValue
            prpty.AccountId = cmbAccountToDebit.SelectedValue
            prpty.PaymentDate = dtpPaymentDate.Value
            prpty.ReferenceNo = txtReferenceNo.Text
            prpty.ReferenceDate = dtpReferenceDate.Value
            prpty.BankId = cmbAccountToCredit.SelectedValue
            prpty.Remarks = txtRemarks.Text
            prpty.TotalAmountReceived = txtAmount.Text
            prpty.BalanceTotalAmount = txtAmount.Text
            prpty.StatusId = PaymentStatus.Approved
            prpty.CancellationCharges = 0
            prpty.BankDate = dtpBankDate.Value
            prpty.PdcPaymentTransactionId = 0
            prpty.CreatedBy = v_the_current_logged_in_user_name
            prpty.DivisionId = v_the_current_division_id
            prpty.PM_Type = entryType
            clsObj.insert_Invoice_Settlement(prpty)
            MsgBox(entryTypeName + " has been Saved.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)

            ClearControls()
            fill_ListPaymentgrid()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Function Validation() As Boolean

        GetPMCode()

        If PM_No = 0 Then
            ''messages already displayed in getPMCode method
            Return False
            Exit Function
        End If

        If cmbAccountToDebit.SelectedIndex <= 0 Then
            MsgBox("Select Account to Debit.", vbExclamation, gblMessageHeading)
            cmbAccountToDebit.Focus()
            Return False
            Exit Function
        End If

        If cmbAccountToCredit.SelectedIndex <= 0 Then
            MsgBox("Select Account to Credit.", vbExclamation, gblMessageHeading)
            cmbAccountToCredit.Focus()
            Return False
            Exit Function
        End If

        If cmbPaymentType.SelectedIndex <= 0 Then
            MsgBox("Select Payment Type to enter payment.", vbExclamation, gblMessageHeading)
            cmbPaymentType.Focus()
            Return False
            Exit Function
        End If

        If String.IsNullOrEmpty(txtReferenceNo.Text) Then
            MsgBox("Please fill the Cheque/Reference No.", vbExclamation, gblMessageHeading)
            txtReferenceNo.Focus()
            Return False
            Exit Function
        End If

        If String.IsNullOrEmpty(txtAmount.Text) Then
            MsgBox("Please fill the Amount.", vbExclamation, gblMessageHeading)
            txtAmount.Focus()
            Return False
            Exit Function
        End If

        Dim amount As Decimal
        If Not Decimal.TryParse(txtAmount.Text, amount) Then
            MsgBox("Amount is not valid.", vbExclamation, gblMessageHeading)
            txtAmount.Focus()
            Return False
            Exit Function
        End If
        Return True
    End Function

    Dim PM_Code As String
    Dim PM_No As Integer

    Private Sub GetPMCode()

        PM_Code = ""
        PM_No = 0
        Dim ds As New DataSet()
        ds = clsObj.fill_Data_set_val("GET_PaymentModule_No", "@DIV_ID", "@PM_TYPE", v_the_current_division_id, entryType)
        If ds.Tables(0).Rows.Count = 0 Then
            MsgBox("Payment Module series does not exists", MsgBoxStyle.Information, gblMessageHeading)
            ds.Dispose()
            Exit Sub
        Else
            If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                MsgBox("Payment Module series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                ds.Dispose()
                Exit Sub
            ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                MsgBox("Payment Module series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                ds.Dispose()
                Exit Sub
            Else
                PM_Code = ds.Tables(0).Rows(0)(0).ToString()
                PM_No = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                ds.Dispose()
            End If
        End If

    End Sub

    Private Sub fill_ListPaymentgrid(Optional ByVal condition As String = "")
        Try

            Dim strsql As String

            strsql = "SELECT * FROM (SELECT  pt.PaymentTransactionId as PaymentID ,PaymentTransactionNo AS PaymentCode ,CONVERT(VARCHAR(20), PaymentDate, 106) AS PaymentDate, " &
            " AM.ACC_NAME AS Account ,ChequeDraftNo AS ChequeNo,CONVERT(VARCHAR(20), ChequeDraftDate, 106) AS ChequeDate ,BK.ACC_NAME AS Bank, " &
            " TotalAmountReceived AS Amount,CASE WHEN StatusId =1 THEN 'InProcess'  WHEN StatusId =2 THEN 'Approved' WHEN StatusId =3 THEN 'Cancelled'  WHEN StatusId =4 THEN 'Bounced' END AS Status,ptm.PaymentTypeName AS PaymentType" &
            " FROM    dbo.PaymentTransaction PT JOIN dbo.ACCOUNT_MASTER AM ON pt.AccountId = AM.ACC_ID JOIN dbo.PaymentTypeMaster PTM ON PTM.PaymentTypeId = PT.PaymentTypeId " &
            " JOIN dbo.ACCOUNT_MASTER BK ON BK.ACC_ID= PT.BankId and PM_Type=" & entryType & ")tb WHERE   PaymentCode + PaymentDate + Account + ChequeNo " &
            "+ ChequeDate + Bank +CAST(Amount AS VARCHAR(50))+ PaymentType+Status LIKE '%" & condition & "%' order by 1"

            Dim dt As DataTable = clsObj.Fill_DataSet(strsql).Tables(0)

            flxList.DataSource = dt

            flxList.Columns(0).Visible = False
            flxList.Columns(1).Width = 120
            flxList.Columns(2).Width = 70
            flxList.Columns(3).Width = 230
            flxList.Columns(4).Width = 70
            flxList.Columns(5).Width = 70
            flxList.Columns(6).Width = 70
            flxList.Columns(7).Width = 70
            flxList.Columns(8).Width = 60
            flxList.Columns(9).Width = 115

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

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
