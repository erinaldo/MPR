Imports MMSPlus

Public Class frm_Account_Payment
    Implements IForm
    Dim obj As New CommonClass
    Public entryType As Int32 = PaymentType.Journal
    Public entryTypeName As String = PaymentType.Journal.ToString
    Dim PaymentId As Int16
    Dim flag As String
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
            query += " where AG_ID IN( " + Convert.ToString(AccountGroups.Bank_Accounts) + ", " + Convert.ToString(AccountGroups.Cash_in_hand) + " )"



            lblGSTNature.Visible = False
            cmbGSTNature.Visible = False
            chk_GSTApplicable.Visible = False
            chk_GSTApplicable_BankId.Visible = False
            lblGSTPercentage.Visible = False
            lblGSTPercentageValue.Visible = False

        ElseIf entryType = PaymentType.Expense Then
            'query += " where AG_ID = " + Convert.ToString(AccountGroups.Bank_Accounts)
            query += " where AG_ID in (" + Convert.ToString(AccountGroups.Expenses_Direct_Mfg) + ", " + Convert.ToString(AccountGroups.Expenses_Indirect_Admn) + ")"
        End If
        query += " Order by ACC_NAME"
        clsObj.ComboBindForPayment(cmbAccountToDebit, query, "ACC_NAME", "ACC_ID", True)


        query = "Select [PaymentTypeId], PaymentTypeName from [PaymentTypeMaster] WHERE [IsActive_bit] = 1 and IsApprovalRequired_bit=0"
        clsObj.ComboBind(cmbPaymentType, query, "PaymentTypeName", "PaymentTypeId", True)





        query = "Select ID, Type from GST_Nature where Is_Active = 1"
        clsObj.ComboBind(cmbGSTNature, query, "Type", "ID", True)

        fill_ListPaymentgrid()
        ClearControls()
    End Sub


    Dim GSTTypeCalculation As DataTable = Nothing
    Dim GSTPercentageCalculation As DataTable = Nothing

    Dim GSTTypeCalculationBankId As DataTable = Nothing
    Dim GSTPercentageCalculationBankId As DataTable = Nothing

    Private Sub cmbAccountToDebit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccountToDebit.SelectedIndexChanged




        query = "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER where ACC_ID <> " + cmbAccountToDebit.SelectedValue.ToString
        If entryType = PaymentType.Contra Then
            query += " and AG_ID IN(" + Convert.ToString(AccountGroups.Bank_Accounts) + ", " + Convert.ToString(AccountGroups.Cash_in_hand) + " )"
        ElseIf entryType = PaymentType.Expense Then
            query += " and AG_ID in (" + Convert.ToString(AccountGroups.Expenses_Direct_Mfg) + ", " + Convert.ToString(AccountGroups.Expenses_Indirect_Admn) + ")"
        End If
        query += " Order by ACC_NAME"
        clsObj.ComboBindForPayment(cmbAccountToCredit, query, "ACC_NAME", "ACC_ID", True)


        chk_GSTApplicable.Checked = False
        txtAmount.Text = ""
        lblGSTPercentageValue.Text = "0.00"

        query = "Select isnull(FK_GST_TYPE_ID,0) As FK_GST_TYPE_ID, isnull(fk_GST_ID,0) As fk_GST_ID, Isnull(Fk_HSN_ID,0) As Fk_HSN_ID from ACCOUNT_MASTER where AG_ID in (10,11,12) and ACC_ID = " + cmbAccountToDebit.SelectedValue.ToString
        GSTTypeCalculation = clsObj.FillDataSet(query).Tables(0)
        If (GSTTypeCalculation.Rows.Count > 0) Then
            If (GSTTypeCalculation.Rows(0)("FK_GST_TYPE_ID").ToString = "2") Then
                chk_GSTApplicable.Checked = True
                chk_GSTApplicable.ForeColor = Color.White
            End If
        End If

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
        cmbGSTNature.SelectedIndex = 0
        flag = "save"
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
            prpty.PaymentTransactionId = PaymentId
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

            If chk_GSTApplicable.Checked Then
                'query = "Select isnull(FK_GST_TYPE_ID,0) As FK_GST_TYPE_ID, isnull(fk_GST_ID,0) As fk_GST_ID, Isnull(Fk_HSN_ID,0) As Fk_HSN_ID from ACCOUNT_MASTER where AG_ID in (10,11,12) and ACC_ID = " + cmbAccountToDebit.SelectedValue.ToString
                'GSTTypeCalculation = clsObj.FillDataSet(query).Tables(0)

                prpty.FK_GST_TYPE_ID = GSTTypeCalculation.Rows(0)("FK_GST_TYPE_ID")
                prpty.fk_GST_ID = GSTTypeCalculation.Rows(0)("fk_GST_ID")
                prpty.Fk_HSN_ID = GSTTypeCalculation.Rows(0)("Fk_HSN_ID")
                prpty.GSTPerAmt = lblGSTPercentageValue.Text
                prpty.GST_Applicable_Acc = "Dr."


            ElseIf chk_GSTApplicable_BankId.Checked Then
                prpty.FK_GST_TYPE_ID = GSTTypeCalculationBankId.Rows(0)("FK_GST_TYPE_ID_BankId")
                prpty.fk_GST_ID = GSTTypeCalculationBankId.Rows(0)("fk_GST_ID_BankId")
                prpty.Fk_HSN_ID = GSTTypeCalculationBankId.Rows(0)("Fk_HSN_ID_BankId")
                prpty.GSTPerAmt = lblGSTPercentageValue.Text
                prpty.GST_Applicable_Acc = "Cr."

            Else
                prpty.FK_GST_TYPE_ID = 0
                prpty.fk_GST_ID = 0
                prpty.Fk_HSN_ID = 0
                prpty.GSTPerAmt = 0.00
                prpty.GST_Applicable_Acc = ""

            End If

            prpty.Fk_GSTNature_ID = cmbGSTNature.SelectedValue

            If (flag = "save") Then
                prpty.Proctype = 1
            Else
                prpty.Proctype = 2
            End If

            If (entryTypeName = "Journal") Then
                prpty.TransactionId = Transaction_Type.Journal
            ElseIf (entryTypeName = "Contra") Then
                prpty.TransactionId = Transaction_Type.Contra
            ElseIf (entryTypeName = "Expense") Then
                prpty.TransactionId = Transaction_Type.Expense
            End If


            clsObj.insert_Invoice_Settlement(prpty)

            If flag = "save" Then
                MsgBox(entryTypeName + " has been Saved.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            Else
                MsgBox(entryTypeName + " has been Updated.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            End If

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

        If chk_GSTApplicable.Checked And chk_GSTApplicable_BankId.Checked Then
            MsgBox("Only one GST Applicable Account can be selected.", vbExclamation, gblMessageHeading)
            cmbAccountToDebit.Focus()
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

            strsql = "SELECT * FROM (SELECT  pt.PaymentTransactionId as PaymentID , PaymentTransactionNo AS VoucherNo ,CONVERT(VARCHAR(20), PaymentDate, 106) AS Date, " &
            " AM.ACC_NAME AS Account ,ChequeDraftNo AS ChequeNo,CONVERT(VARCHAR(20), ChequeDraftDate, 106) AS ChequeDate ,BK.ACC_NAME AS Bank, " &
            " TotalAmountReceived AS Amount,CASE WHEN StatusId =1 THEN 'InProcess'  WHEN StatusId =2 THEN 'Approved' WHEN StatusId =3 THEN 'Cancelled'  WHEN StatusId = 4 THEN 'Bounced' END AS Status,ptm.PaymentTypeName AS PaymentType,  PT.StatusId " &
            " FROM    dbo.PaymentTransaction PT JOIN dbo.ACCOUNT_MASTER AM ON pt.AccountId = AM.ACC_ID JOIN dbo.PaymentTypeMaster PTM ON PTM.PaymentTypeId = PT.PaymentTypeId " &
            " JOIN dbo.ACCOUNT_MASTER BK ON BK.ACC_ID= PT.BankId and PM_Type=" & entryType & ")tb Where VoucherNo + Date + Account + ChequeNo " &
            "+ ChequeDate + Bank +CAST(Amount AS VARCHAR(50))+ PaymentType+Status LIKE '%" & condition & "%' order by 1"

            Dim dt As DataTable = clsObj.Fill_DataSet(strsql).Tables(0)

            flxList.DataSource = dt

            flxList.Columns(0).Visible = False
            flxList.Columns(1).Width = 120
            flxList.Columns(2).Width = 70
            flxList.Columns(3).Width = 220
            flxList.Columns(4).Width = 70
            flxList.Columns(5).Width = 70
            flxList.Columns(6).Width = 70
            flxList.Columns(7).Width = 70
            flxList.Columns(8).Width = 60
            flxList.Columns(9).Width = 100
            flxList.Columns(10).Visible = False

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Public Sub CloseClick(sender As Object, e As EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(sender As Object, e As EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub ViewClick(sender As Object, e As EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                If flxList.SelectedRows.Count > 0 Then
                    obj.RptShow(enmReportName.RptAccPaymentPrint, "PaymentId", CStr(flxList("PaymentId", flxList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptAccPaymentPrint, "PaymentId", CStr(PaymentId), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub RefreshClick(sender As Object, e As EventArgs) Implements IForm.RefreshClick

    End Sub

    Private Sub flxList_DoubleClick(sender As Object, e As EventArgs) Handles flxList.DoubleClick
        If _rights.allow_edit = "N" Then
            RightsMsg()
            Exit Sub
        End If

        flag = "update"
        PaymentId = Convert.ToInt32(flxList("PaymentId", flxList.CurrentCell.RowIndex).Value())
        Dim result As Integer = MessageBox.Show("Are you sure you want to edit this Voucher ?", "Edit Voucher", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            FillPaymentDetails(PaymentId)
        End If
    End Sub

    Public Sub FillPaymentDetails(PaymentId As Int16)
        Dim dt As DataTable
        dt = clsObj.fill_Data_set("Proc_GETPaymentDetailByID_Edit", "@PaymentId", PaymentId).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim Status As String
            Status = dr("StatusId")
            If Status = "3" Then
                MessageBox.Show("Cancelled voucher can't be edited.")
                Return
            End If

            TabControl1.SelectedIndex = 1
            cmbAccountToDebit.SelectedValue = dr("AccountId")
            cmbPaymentType.SelectedValue = dr("PaymentTypeId")
            txtReferenceNo.Text = dr("ChequeDraftNo")
            cmbAccountToCredit.SelectedValue = dr("BankId")
            txtAmount.Text = dr("TotalAmountReceived")
            txtRemarks.Text = dr("Remarks")
            dtpPaymentDate.Value = dr("PaymentDate")
            dtpBankDate.Value = dr("BankDate")
            dtpReferenceDate.Value = dr("ChequeDraftDate")
            lblGSTPercentageValue.Text = dr("GSTPerAmt")
            cmbGSTNature.SelectedValue = dr("Fk_GSTNature_ID")

        End If
    End Sub

    Private Sub BtnCancelInv_Click(sender As Object, e As EventArgs) Handles BtnCancelInv.Click
        If _rights.allow_cancel = "N" Then
            RightsMsg()
            Exit Sub
        End If

        Dim result As Integer = MessageBox.Show("Are you sure you want to cancel this Voucher ?", "Cancel Voucher", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then

            Dim Status As String
            Status = flxList.SelectedRows(0).Cells("Status").Value
            If Status = "Cancelled" Then
                MessageBox.Show("This entry is already cancelled")
                Return
            End If

            Dim transtype As Int32

            If (entryTypeName = "Journal") Then
                transtype = Transaction_Type.Journal
            ElseIf (entryTypeName = "Contra") Then
                transtype = Transaction_Type.Contra
            ElseIf (entryTypeName = "Expense") Then
                transtype = Transaction_Type.Expense
            End If

            clsObj.Cancel_PaymentEntries(Convert.ToInt32(flxList("PaymentId", flxList.SelectedRows(0).Index).Value()), PaymentStatus.Cancelled, 0, entryType, transtype)
            MsgBox("Selected entry cancelled successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            fill_ListPaymentgrid()

        End If
    End Sub

    Private Sub cmbAccountToDebit_Enter(sender As Object, e As EventArgs) Handles cmbAccountToDebit.Enter

        If (cmbAccountToDebit.Text.Length) > 0 And cmbAccountToDebit.SelectedIndex > 0 Then
            If Not cmbAccountToDebit.DroppedDown Then
                cmbAccountToDebit.DroppedDown = True
            End If
        End If
    End Sub

    Private Sub cmbAccountToCredit_Enter(sender As Object, e As EventArgs) Handles cmbAccountToCredit.Enter
        If Not cmbAccountToCredit.DroppedDown Then
            cmbAccountToCredit.DroppedDown = True
        End If
    End Sub

    Private Sub cmbPaymentType_Enter(sender As Object, e As EventArgs) Handles cmbPaymentType.Enter
        If Not cmbPaymentType.DroppedDown Then
            cmbPaymentType.DroppedDown = True
        End If
    End Sub

    Private Sub txtAmount_Leave(sender As Object, e As EventArgs) Handles txtAmount.Leave

        'query = "Select isnull(FK_GST_TYPE_ID,0) As FK_GST_TYPE_ID, isnull(fk_GST_ID,0) As fk_GST_ID, Isnull(Fk_HSN_ID,0) As Fk_HSN_ID from ACCOUNT_MASTER where AG_ID in (10,11,12) and ACC_ID = " + cmbAccountToDebit.SelectedValue.ToString
        'GSTTypeCalculation = clsObj.FillDataSet(query).Tables(0)
        If String.IsNullOrEmpty(txtAmount.Text) Then
            txtAmount.Text = 0
        End If
        'If (GSTTypeCalculation.Rows.Count > 0) Then
        '    If (GSTTypeCalculation.Rows(0)("FK_GST_TYPE_ID") = "2") Then

        '        query = "SELECT VAT_PERCENTAGE FROM dbo.VAT_MASTER WHERE VAT_ID = " + GSTTypeCalculation.Rows(0)("fk_GST_ID").ToString
        '        GSTPercentageCalculation = clsObj.FillDataSet(query).Tables(0)

        '        If (GSTPercentageCalculation.Rows.Count > 0) Then
        '            lblGSTPercentageValue.Text = Math.Round((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(GSTPercentageCalculation.Rows(0)("VAT_PERCENTAGE"))) / 100, 2)
        '        End If

        '    End If
        'End If

        If (GSTTypeCalculation.Rows.Count > 0) Then
            If (GSTTypeCalculation.Rows(0)("FK_GST_TYPE_ID") = "2") And chk_GSTApplicable.Checked Then

                query = "SELECT VAT_PERCENTAGE FROM dbo.VAT_MASTER WHERE VAT_ID = " + GSTTypeCalculation.Rows(0)("fk_GST_ID").ToString
                GSTPercentageCalculation = clsObj.FillDataSet(query).Tables(0)

                If (GSTPercentageCalculation.Rows.Count > 0) Then
                    lblGSTPercentageValue.Text = Math.Round((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(GSTPercentageCalculation.Rows(0)("VAT_PERCENTAGE"))) / 100, 2)
                End If

            End If
        ElseIf (GSTTypeCalculationBankId.Rows.Count > 0) Then
            If (GSTTypeCalculationBankId.Rows(0)("FK_GST_TYPE_ID_BankId") = "2") And chk_GSTApplicable_BankId.Checked Then

                query = "SELECT VAT_PERCENTAGE FROM dbo.VAT_MASTER WHERE VAT_ID = " + GSTTypeCalculationBankId.Rows(0)("fk_GST_ID_BankId").ToString
                GSTPercentageCalculationBankId = clsObj.FillDataSet(query).Tables(0)

                If (GSTPercentageCalculationBankId.Rows.Count > 0) Then
                    lblGSTPercentageValue.Text = Math.Round((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(GSTPercentageCalculationBankId.Rows(0)("VAT_PERCENTAGE"))) / 100, 2)
                End If

            End If
        Else
            lblGSTPercentageValue.Text = ""
        End If


    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged

        If String.IsNullOrEmpty(txtAmount.Text) Then
            txtAmount.Text = 0
        End If

        'If (GSTTypeCalculation.Rows.Count > 0) Then
        '    If (GSTTypeCalculation.Rows(0)("FK_GST_TYPE_ID") = "2") Then

        '        query = "SELECT VAT_PERCENTAGE FROM dbo.VAT_MASTER WHERE VAT_ID = " + GSTTypeCalculation.Rows(0)("fk_GST_ID").ToString
        '        GSTPercentageCalculation = clsObj.FillDataSet(query).Tables(0)

        '        If (GSTPercentageCalculation.Rows.Count > 0) Then
        '            lblGSTPercentageValue.Text = Math.Round((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(GSTPercentageCalculation.Rows(0)("VAT_PERCENTAGE"))) / 100, 2)
        '        End If

        '    End If

        'End If

        If (GSTTypeCalculation.Rows.Count > 0) Then
            If (GSTTypeCalculation.Rows(0)("FK_GST_TYPE_ID") = "2") And chk_GSTApplicable.Checked Then

                query = "SELECT VAT_PERCENTAGE FROM dbo.VAT_MASTER WHERE VAT_ID = " + GSTTypeCalculation.Rows(0)("fk_GST_ID").ToString
                GSTPercentageCalculation = clsObj.FillDataSet(query).Tables(0)

                If (GSTPercentageCalculation.Rows.Count > 0) Then
                    lblGSTPercentageValue.Text = Math.Round((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(GSTPercentageCalculation.Rows(0)("VAT_PERCENTAGE"))) / 100, 2)
                End If

            End If
        ElseIf (GSTTypeCalculationBankId.Rows.Count > 0) Then
            If (GSTTypeCalculationBankId.Rows(0)("FK_GST_TYPE_ID_BankId") = "2") And chk_GSTApplicable_BankId.Checked Then

                query = "SELECT VAT_PERCENTAGE FROM dbo.VAT_MASTER WHERE VAT_ID = " + GSTTypeCalculationBankId.Rows(0)("fk_GST_ID_BankId").ToString
                GSTPercentageCalculationBankId = clsObj.FillDataSet(query).Tables(0)

                If (GSTPercentageCalculationBankId.Rows.Count > 0) Then
                    lblGSTPercentageValue.Text = Math.Round((Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(GSTPercentageCalculationBankId.Rows(0)("VAT_PERCENTAGE"))) / 100, 2)
                End If

            End If
        Else
            lblGSTPercentageValue.Text = ""
        End If

    End Sub

    Private Sub cmbAccountToCredit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccountToCredit.SelectedIndexChanged
        chk_GSTApplicable_BankId.Checked = False
        txtAmount.Text = ""
        lblGSTPercentageValue.Text = "0.00"

        query = "Select isnull(FK_GST_TYPE_ID,0) As FK_GST_TYPE_ID_BankId, isnull(fk_GST_ID,0) As fk_GST_ID_BankId, Isnull(Fk_HSN_ID,0) As Fk_HSN_ID_BankId from ACCOUNT_MASTER where AG_ID in (13,14) and ACC_ID = " + cmbAccountToCredit.SelectedValue.ToString
        GSTTypeCalculationBankId = clsObj.FillDataSet(query).Tables(0)

        If (GSTTypeCalculationBankId.Rows.Count > 0) Then
            If (GSTTypeCalculationBankId.Rows(0)("FK_GST_TYPE_ID_BankId").ToString = "2") Then
                chk_GSTApplicable_BankId.Checked = True
                chk_GSTApplicable_BankId.ForeColor = Color.White
            End If
        End If
    End Sub

    Private Sub cmbAccountToDebit_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbAccountToDebit.KeyUp
        'If cmbAccountToDebit.SelectedIndex <> 0 Then
        '    If Not cmbAccountToDebit.DroppedDown Then
        '        cmbAccountToDebit.DroppedDown = True

        '    End If
        'End If

    End Sub


End Class
