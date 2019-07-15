Imports System.Data.SqlClient
Public Class frm_Invoice_Settlement
    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New cls_Invoice_Settlement
    Dim _rights As Form_Rights
    Dim PaymentId As Int16
    Dim flag As String
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Invoice_Settlement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeControls()
    End Sub

    Dim _paymentStatus As PaymentStatus

    Private Sub InitializeControls()
        clsObj.ComboBindForPayment(cmbCustomer, "Select ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ ISNULL(VAT_NO,'')) AS ACC_NAME from ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID in (1,2,3,6) Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
        clsObj.ComboBindForPayment(cmbCustomerApprovePayment, "Select ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ ISNULL(VAT_NO,'')) AS ACC_NAME from ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID in (1,2,3,6) Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
        clsObj.ComboBindForPayment(cmbCustomerSettleInvoice, "Select ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ ISNULL(VAT_NO,'')) AS ACC_NAME from ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID in (1,2,3,6) Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)

        clsObj.ComboBind(cmbPaymentType, "Select [PaymentTypeId], [PaymentTypeName] + CASE WHEN IsApprovalRequired_bit=1" &
                         " THEN ' - Approval Required' ELSE ' - Approval Not Required' END AS PaymentTypeName from [PaymentTypeMaster] WHERE [IsActive_bit] = 1",
                          "PaymentTypeName", "PaymentTypeId", True)

        clsObj.ComboBind(cmbBank, "SELECT ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ ISNULL(VAT_NO,'')) AS ACC_NAME FROM dbo.ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID IN(" & AccountGroups.Bank_Accounts & "," & AccountGroups.Cash_in_hand & ")",
                          "ACC_NAME", "ACC_ID", True)
        GetPMCode()
        fill_ListPaymentgrid()
        ClearControls()
    End Sub

    Private Sub fill_ListPaymentgrid(Optional ByVal condition As String = "")
        Try

            Dim strsql As String

            strsql = "SELECT * FROM (SELECT  pt.PaymentTransactionId as PaymentID ,PaymentTransactionNo AS VoucherNo ,CONVERT(VARCHAR(20), PaymentDate, 106) AS Date, " &
            " AM.ACC_NAME AS Account ,ChequeDraftNo AS ChequeNo,CONVERT(VARCHAR(20), ChequeDraftDate, 106) AS ChequeDate ,BK.ACC_NAME AS Bank, " &
            " TotalAmountReceived AS Amount,CASE WHEN StatusId =1 THEN 'InProcess'  WHEN StatusId =2 THEN 'Approved' WHEN StatusId =3 THEN 'Cancelled'  WHEN StatusId =4 THEN 'Bounced' END AS Status,ptm.PaymentTypeName AS PaymentType,  PT.StatusId" &
            " FROM    dbo.PaymentTransaction PT JOIN dbo.ACCOUNT_MASTER AM ON pt.AccountId = AM.ACC_ID JOIN dbo.PaymentTypeMaster PTM ON PTM.PaymentTypeId = PT.PaymentTypeId " &
            " JOIN dbo.ACCOUNT_MASTER BK ON BK.ACC_ID= PT.BankId and PM_Type=" & PaymentType.Receipt & ")tb Where VoucherNo + Date + Account + ChequeNo " &
            "+ ChequeDate + Bank +CAST(Amount AS VARCHAR(50))+ PaymentType+Status LIKE '%" & condition & "%' order by 1"

            Dim dt As DataTable = clsObj.Fill_DataSet(strsql).Tables(0)

            flxList.DataSource = dt

            flxList.Columns(0).Visible = False
            flxList.Columns(1).Width = 120
            flxList.Columns(2).Width = 70
            flxList.Columns(3).Width = 210
            flxList.Columns(4).Width = 70
            flxList.Columns(5).Width = 70
            flxList.Columns(6).Width = 100
            flxList.Columns(7).Width = 70
            flxList.Columns(8).Width = 60
            flxList.Columns(9).Width = 80
            flxList.Columns(10).Visible = False

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Private Sub ClearControls()
        cmbCustomer.SelectedIndex = 0
        lblPendingAmount.Text = "0.00"
        lblAdvanceAmount.Text = "0.00"
        dtpPaymentDate.Value = DateTime.Now
        cmbPaymentType.SelectedIndex = 0
        cmbBank.SelectedIndex = 0
        txtReferenceNo.Text = ""
        dtpReferenceDate.Value = DateTime.Now
        dtpBankDate.Value = DateTime.Now
        txtAmount.Text = ""
        txtRemarks.Text = ""
        TabControl1.SelectedIndex = 1
        flag = "save"
    End Sub

    Private Sub cmbPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPaymentType.SelectedIndexChanged
        If cmbPaymentType.SelectedIndex > 0 Then
            If cmbPaymentType.Text.Contains("Approval Required") Then
                chkBoxDistributeAmount.Enabled = False
                _paymentStatus = PaymentStatus.InProcess
            Else
                chkBoxDistributeAmount.Enabled = True
                _paymentStatus = PaymentStatus.Approved
            End If
        End If
    End Sub

    Dim PM_Code As String
    Dim PM_No As Integer

    Private Sub GetPMCode()

        PM_Code = ""
        PM_No = 0
        Dim ds As New DataSet()
        ds = clsObj.fill_Data_set_val("GET_PaymentModule_No", "@DIV_ID", "@PM_TYPE", v_the_current_division_id, PaymentType.Receipt)
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

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        InitializeControls()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

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

            cmbCustomer.SelectedIndex = cmbCustomer.FindStringExact(cmbCustomer.Text)

            prpty = New cls_Invoice_Settlement_prop
            prpty.PaymentTransactionId = PaymentId
            prpty.PaymentTransactionCode = PM_Code & PM_No
            prpty.PaymentTypeId = cmbPaymentType.SelectedValue
            prpty.AccountId = cmbCustomer.SelectedValue
            prpty.PaymentDate = dtpPaymentDate.Value
            prpty.ReferenceNo = txtReferenceNo.Text
            prpty.ReferenceDate = dtpReferenceDate.Value
            prpty.BankId = cmbBank.SelectedValue
            prpty.Remarks = txtRemarks.Text
            prpty.TotalAmountReceived = txtAmount.Text
            prpty.BalanceTotalAmount = txtAmount.Text
            prpty.StatusId = _paymentStatus
            prpty.CancellationCharges = 0
            prpty.BankDate = dtpBankDate.Value
            prpty.PdcPaymentTransactionId = 0
            prpty.CreatedBy = v_the_current_logged_in_user_name
            prpty.DivisionId = v_the_current_division_id
            prpty.PM_Type = PaymentType.Receipt

            If (flag = "save") Then
                prpty.Proctype = 1
            Else
                prpty.Proctype = 2
            End If

            prpty.TransactionId = Transaction_Type.Invoice_Settlement

            clsObj.insert_Invoice_Settlement(prpty)

            If flag = "save" Then
                MsgBox("Payment received sucessfully!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            Else
                MsgBox("Payment received updated sucessfully!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            End If

            If chkBoxDistributeAmount.Checked And chkBoxDistributeAmount.Enabled Then
                cmbCustomerSettleInvoice.SelectedValue = cmbCustomer.SelectedValue
                btnDistributeAmount_Click(Nothing, Nothing)
                btnSettleInvoice_Click(Nothing, Nothing)
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

        cmbCustomer.SelectedIndex = cmbCustomer.FindStringExact(cmbCustomer.Text)

        If cmbCustomer.SelectedIndex <= 0 Then
            MsgBox("Select Account to enter payment.", vbExclamation, gblMessageHeading)
            cmbCustomer.Focus()
            Return False
            Exit Function
        End If

        If cmbPaymentType.SelectedIndex <= 0 Then
            MsgBox("Select Payment Type to enter payment.", vbExclamation, gblMessageHeading)
            cmbPaymentType.Focus()
            Return False
            Exit Function
        End If

        If cmbBank.SelectedIndex <= 0 Then
            MsgBox("Select Bank to enter payment.", vbExclamation, gblMessageHeading)
            cmbBank.Focus()
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

        If Convert.ToDateTime(dtpPaymentDate.Text) > Now Then
            MsgBox("Payment date can't be greater than to current date", vbExclamation, gblMessageHeading)
            dtpPaymentDate.Focus()
            Return False
            Exit Function
        End If

        If Convert.ToDateTime(dtpPaymentDate.Text) < v_the_current_financial_year Then
            MsgBox("Payment date can't be Less than to current finicial year date", vbExclamation, gblMessageHeading)
            dtpPaymentDate.Focus()
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

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                If flxList.SelectedRows.Count > 0 Then

                    obj.RptShow(enmReportName.RptPaymentPrint, "PaymentId", CStr(flxList("PaymentId", flxList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptPaymentPrint, "PaymentId", CStr(PaymentId), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tabTakePayment_Click(sender As Object, e As EventArgs) Handles tabTakePayment.Click

    End Sub

    Private Sub tabApprovePayment_Click(sender As Object, e As EventArgs) Handles tabApprovePayment.Click

    End Sub

    Private Sub cmbPendingPayment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPendingPayment.SelectedIndexChanged
        Dim query As String = " SELECT PaymentTransactionNo, PaymentDate, ChequeDraftno, ChequeDraftDate, Remarks," &
            " TotalAmountReceived, pt.CreatedBy, PaymentTypeName , bm.ACC_NAME AS  BankName, BankDate " &
            " FROM dbo.PaymentTransaction pt INNER JOIN dbo.PaymentTypeMaster ptm ON ptm.PaymentTypeId = pt.PaymentTypeId" &
            " INNER JOIN dbo.ACCOUNT_MASTER bm ON bm.ACC_ID  = pt.BankId WHERE PaymentTransactionId =  " & cmbPendingPayment.SelectedValue

        Dim ds As DataSet = clsObj.FillDataSet(query)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            lblPaymentDate.Text = dr("PaymentDate")
            lblPaymentType.Text = dr("PaymentTypeName")
            lblAmount.Text = dr("TotalAmountReceived")
            lblBankName.Text = dr("BankName")
            lblBankDate.Text = dr("BankDate")
            lblReferenceDate.Text = dr("ChequeDraftDate")
            lblReferenceNo.Text = dr("ChequeDraftno")
            lblRemarks.Text = dr("Remarks")
            lblReceivedBy.Text = dr("CreatedBy")
        Else
            ClearInfoLabels()
        End If
    End Sub

    Private Sub cmbCustomerApprovePayment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomerApprovePayment.SelectedIndexChanged

        cmbCustomerApprovePayment.SelectedIndex = cmbCustomerApprovePayment.FindStringExact(cmbCustomerApprovePayment.Text)

        If cmbCustomerApprovePayment.SelectedIndex > 0 Then
            Dim query As String = "SELECT PaymentTransactionID, PaymentTransactionNo + ' - ' + CONVERT(VARCHAR(20), PaymentDate, 107) as PaymentTransactionNo" &
            " FROM dbo.PaymentTransaction where StatusId = 1 AND AccountId = " & cmbCustomerApprovePayment.SelectedValue
            clsObj.ComboBind(cmbPendingPayment, query, "PaymentTransactionNo", "PaymentTransactionID", True)
        End If

    End Sub

    Private Sub btnApproved_Click(sender As Object, e As EventArgs) Handles btnApproved.Click
        UpdatePaymentStatus(PaymentStatus.Approved)
    End Sub

    Private Sub btnBounce_Click(sender As Object, e As EventArgs) Handles btnBounce.Click
        UpdatePaymentStatus(PaymentStatus.Bounced)
    End Sub

    Private Sub UpdatePaymentStatus(_paymentApprovalStatus As PaymentStatus)

        If ApprovalValidation() = False Then
            Exit Sub
        End If

        cmbCustomerApprovePayment.SelectedIndex = cmbCustomerApprovePayment.FindStringExact(cmbCustomerApprovePayment.Text)

        prpty = New cls_Invoice_Settlement_prop
        prpty.PaymentTransactionId = cmbPendingPayment.SelectedValue
        prpty.StatusId = _paymentApprovalStatus
        prpty.CancellationCharges = txtCancellationCharges.Text
        prpty.PM_Type = PaymentType.Receipt
        prpty.TransactionId = Transaction_Type.Invoice_Settlement

        clsObj.Update_Payment_Status(prpty)

        MsgBox("Payment status has been updated.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)

        If chkBoxDistributeAmountApprove.Checked And chkBoxDistributeAmountApprove.Enabled Then
            cmbCustomerSettleInvoice.SelectedValue = cmbCustomerApprovePayment.SelectedValue
            btnDistributeAmount_Click(Nothing, Nothing)
            btnSettleInvoice_Click(Nothing, Nothing)
        End If

        ClearApprovalControls()

    End Sub

    Private Sub ClearApprovalControls()
        cmbCustomerApprovePayment.SelectedIndex = 0
        txtCancellationCharges.Text = ""
        ClearInfoLabels()
    End Sub

    Private Sub ClearInfoLabels()
        lblPaymentDate.Text = ""
        lblPaymentType.Text = ""
        lblAmount.Text = ""
        lblReferenceDate.Text = ""
        lblReferenceNo.Text = ""
        lblRemarks.Text = ""
        lblBankName.Text = ""
        lblBankDate.Text = ""
        lblReceivedBy.Text = ""
    End Sub

    Private Function ApprovalValidation() As Boolean

        cmbCustomerApprovePayment.SelectedIndex = cmbCustomerApprovePayment.FindStringExact(cmbCustomerApprovePayment.Text)

        If cmbCustomerApprovePayment.SelectedIndex <= 0 Then
            MsgBox("Select Account to approve/disapprove payment.", vbExclamation, gblMessageHeading)
            cmbCustomerApprovePayment.Focus()
            Return False
            Exit Function
        End If

        If cmbPendingPayment.SelectedIndex <= 0 Then
            MsgBox("Select pending payment to approve/disapprove payment.", vbExclamation, gblMessageHeading)
            cmbPendingPayment.Focus()
            Return False
            Exit Function
        End If

        If String.IsNullOrEmpty(txtCancellationCharges.Text) Then
            txtCancellationCharges.Text = "0"
        End If

        Dim amount As Decimal
        If Not Decimal.TryParse(txtCancellationCharges.Text, amount) Then
            MsgBox("Amount is not valid.", vbExclamation, gblMessageHeading)
            txtCancellationCharges.Focus()
            Return False
            Exit Function
        End If
        Return True
    End Function

    Private Sub tabDistributePayment_Click(sender As Object, e As EventArgs) Handles tabDistributePayment.Click

    End Sub
    Dim SettleInvoiceCustomerID = 0
    Private Sub cmbCustomerSettleInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomerSettleInvoice.SelectedIndexChanged

        cmbCustomerSettleInvoice.SelectedIndex = cmbCustomerSettleInvoice.FindStringExact(cmbCustomerSettleInvoice.Text)
        SettleInvoiceCustomerID = cmbCustomerSettleInvoice.SelectedValue
        If cmbCustomerSettleInvoice.SelectedIndex > 0 Then
            FillGrid()
            SetUndistributedAmount()
        End If
    End Sub

    Dim UndistributedAmount As Decimal
    Dim OpenCrAmount As Decimal
    Private Sub SetUndistributedAmount()

        'cmbCustomerSettleInvoice.SelectedIndex = cmbCustomerSettleInvoice.FindStringExact(cmbCustomerSettleInvoice.Text)

        ' If cmbCustomerSettleInvoice.SelectedIndex > 0 Then
        Dim query As String = " DECLARE @UndistributedAmount DECIMAL(18, 2) SELECT @UndistributedAmount=isnull(SUM(UndistributedAmount), 0) FROM dbo.PaymentTransaction WHERE StatusId =2 AND AccountId=" & SettleInvoiceCustomerID &
       " SET @UndistributedAmount=ISNULL(@UndistributedAmount,0)+ISNULL((SELECT ISNULL(MAX(OpeningAmount),0)- ISNULL(sum(AmountSettled),0) FROM dbo.OpeningBalance left JOIN dbo.SettlementDetail" &
       " ON OpeningBalanceId=PaymentTransactionId WHERE TYPE=2 AND FkAccountId=" & SettleInvoiceCustomerID & " ),0) SELECT @UndistributedAmount"
        UndistributedAmount = clsObj.ExecuteScalar(query)

        OpenCrAmount = clsObj.ExecuteScalar("SELECT ISNULL(SUM(OpenCrAmount), 0) FROM (SELECT ( ISNULL(SUM(CN_Amount), 0) )- ( SELECT  ISNULL(SUM(OpenCrAmount), 0) FROM  SettlementDetail WHERE   OpenCrNo = CAST(CreditNote_No AS VARCHAR(20)))AS  OpenCrAmount FROM dbo.CreditNote_Master WHERE INVId<=0 AND CN_CustId= " & SettleInvoiceCustomerID & " GROUP BY CreditNote_No )TB")
        If OpenCrAmount > 0 Then
            UndistributedAmount = (UndistributedAmount + OpenCrAmount)
            lblOpenCrAmount.Text = OpenCrAmount.ToString("0.00")
        Else
            lblOpenCrAmount.Text = 0.00
        End If

        lblUndistributedAmount.Text = UndistributedAmount.ToString("0.00")
        ' End If
    End Sub

    Private Sub FillGrid()
        'cmbCustomerSettleInvoice.SelectedIndex = cmbCustomerSettleInvoice.FindStringExact(cmbCustomerSettleInvoice.Text)
        'If cmbCustomerSettleInvoice.SelectedIndex > 0 Then
        'If SettleInvoiceCustomerID > 0 Then

        Dim query As String = " SELECT SI_ID, SI_CODE ,SI_NO, CONVERT(VARCHAR(20), SI_DATE, 106) as SI_DATE, NET_AMOUNT, " &
                "ISNULL((SELECT SUM(AmountSettled) FROM dbo.SettlementDetail JOIN dbo.PaymentTransaction  ON dbo.PaymentTransaction.PaymentTransactionId = dbo.SettlementDetail.PaymentTransactionId WHERE InvoiceId = SI_ID AND AccountId=" & SettleInvoiceCustomerID & "),0) +" &
                " ISNULL((SELECT SUM(AmountSettled) FROM dbo.SettlementDetail JOIN dbo.OpeningBalance  ON dbo.OpeningBalance.OpeningBalanceId = dbo.SettlementDetail.PaymentTransactionId WHERE InvoiceId = SI_ID AND fkAccountId=" & SettleInvoiceCustomerID & "),0)" &
                "  + ISNULL(( SELECT   SUM(AmountSettled)  FROM     ( SELECT DISTINCT  ISNULL(AmountSettled, 0) AS AmountSettled ,CreditNote_No ,InvoiceId FROM      dbo.SettlementDetail  JOIN dbo.CreditNote_Master ON Cast(dbo.CreditNote_Master.CreditNote_No AS VARCHAR(20))= dbo.SettlementDetail.OpenCrNo  WHERE     InvoiceId = SI_ID AND SettlementDetail.PaymentTransactionId <= 0  AND CN_CustId=" & SettleInvoiceCustomerID & " ) tb  ), 0)  AS ReceivedAmount ,iSNULL(cn_amount,0) AS CnAmount" &
                " FROM dbo.SALE_INVOICE_MASTER  LEFT JOIN dbo.CreditNote_Master ON INVId = SI_ID WHERE  INVOICE_STATUS <> 4 AND CUST_ID = " & SettleInvoiceCustomerID &
                " UNION SELECT OpeningBalanceId,'Opening Balance',OpeningBalanceId,CONVERT(VARCHAR(20), OpeningDate, 106) AS OpeningDate ,OpeningAmount, ISNULL(( SELECT SUM(AmountSettled)FROM   dbo.SettlementDetail JOIN dbo.PaymentTransaction " &
                " ON dbo.PaymentTransaction.PaymentTransactionId = dbo.SettlementDetail.PaymentTransactionId  WHERE  InvoiceId = OpeningBalanceId  AND AccountId = " & SettleInvoiceCustomerID &
                " ), 0) AS ReceivedAmount ,0 FROM dbo.OpeningBalance WHERE  TYPE=1 AND FkAccountId=" & SettleInvoiceCustomerID

        Dim dt As DataTable = clsObj.Fill_DataSet(query).Tables(0)
        dgvInvoiceToSettle.RowCount = 0

        Dim index As Int16 = 0
        For Each dr As DataRow In dt.Rows
            If (dr("NET_AMOUNT") - dr("ReceivedAmount") - dr("CnAmount")) = 0 Then
                Continue For
            End If
            dgvInvoiceToSettle.RowCount += 1
            dgvInvoiceToSettle.Rows(index).Cells("InvoiceId").Value = dr("SI_ID")
            dgvInvoiceToSettle.Rows(index).Cells("InvoiceNo").Value = dr("SI_CODE") & dr("SI_No")
            dgvInvoiceToSettle.Rows(index).Cells("InvoiceDate").Value = dr("SI_DATE")
            dgvInvoiceToSettle.Rows(index).Cells("InvoiceAmount").Value = dr("NET_AMOUNT")
            dgvInvoiceToSettle.Rows(index).Cells("ReceivedAmount").Value = dr("ReceivedAmount")
            dgvInvoiceToSettle.Rows(index).Cells("CreditedAmount").Value = dr("CnAmount")
            dgvInvoiceToSettle.Rows(index).Cells("PendingAmount").Value = dr("NET_AMOUNT") - dr("ReceivedAmount") - dr("CnAmount")
            dgvInvoiceToSettle.Rows(index).Cells("AmountToReceive").Value = 0
            index = index + 1
        Next
        ' End If
    End Sub

    Private Sub btnDistributeAmount_Click(sender As Object, e As EventArgs) Handles btnDistributeAmount.Click
        dgvInvoiceToSettle.Sort(InvoiceDate, System.ComponentModel.ListSortDirection.Ascending)
        SetUndistributedAmount()

        For Each row As DataGridViewRow In dgvInvoiceToSettle.Rows
            row.Cells("AmountToReceive").Value = 0
            Dim pendingAmount As Decimal = row.Cells("PendingAmount").Value
            If pendingAmount <= UndistributedAmount Then
                row.Cells("AmountToReceive").Value = pendingAmount
                UndistributedAmount = UndistributedAmount - pendingAmount
            ElseIf UndistributedAmount > 0 Then
                row.Cells("AmountToReceive").Value = UndistributedAmount
                UndistributedAmount = 0
            End If
        Next
        lblUndistributedAmount.Text = UndistributedAmount.ToString("0.00")
    End Sub

    Private Sub btnSettleInvoice_Click(sender As Object, e As EventArgs) Handles btnSettleInvoice.Click

        ' cmbCustomerSettleInvoice.SelectedIndex = cmbCustomerSettleInvoice.FindStringExact(cmbCustomerSettleInvoice.Text)

        ' If cmbCustomerSettleInvoice.SelectedIndex <= 0 Then
        If SettleInvoiceCustomerID <= 0 Then
            MsgBox("Select Account to Settle payment.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If
        Dim query As String = "SELECT PaymentTransactionId, UndistributedAmount, PaymentTransactionNo FROM dbo.PaymentTransaction" &
            " WHERE StatusId =2 AND UndistributedAmount > 0 AND AccountId=" & SettleInvoiceCustomerID &
            " UNION ALL SELECT  0 AS ID , ( ISNULL(SUM(CN_Amount), 0) )  - ( SELECT  ISNULL(SUM(OpenCrAmount), 0) FROM    SettlementDetail WHERE   OpenCrNo =  CAST(CreditNote_No AS VARCHAR(50) )) AS UndistributedAmount , CAST(CreditNote_No AS VARCHAR(50) ) FROM    dbo.CreditNote_Master WHERE   INVId <= 0 AND CN_CustId = " & SettleInvoiceCustomerID & " GROUP BY CreditNote_No" &
            " UNION ALL SELECT OpeningBalanceId,ISNULL(MAX(OpeningAmount), 0) - ISNULL(SUM(AmountSettled), 0) AS OpeningAmount, 'OPBL'  FROM dbo.OpeningBalance LEFT JOIN dbo.SettlementDetail ON OpeningBalanceId = PaymentTransactionId WHERE TYPE=2 AND FkAccountId= " & SettleInvoiceCustomerID &
            " GROUP BY OpeningBalanceId ORDER BY PaymentTransactionId ASC"

        Dim undistributedAmountTable As DataTable = clsObj.Fill_DataSet(query).Tables(0)

        Dim prop As New cls_Invoice_Settlement_prop
        For Each row As DataGridViewRow In dgvInvoiceToSettle.Rows

            Dim amountToSettle As Decimal = row.Cells("AmountToReceive").Value
            If amountToSettle = 0 Then
                Continue For
            End If

            Dim index As Int32 = 0
            While amountToSettle > 0 And index < undistributedAmountTable.Rows.Count

                Dim amountAvailable As Decimal = undistributedAmountTable.Rows(index)("UndistributedAmount")
                Dim AmountSettled As Decimal = 0

                If amountAvailable = 0 Then
                    index += 1
                    Continue While
                End If

                If amountToSettle <= amountAvailable Then
                    AmountSettled = amountToSettle
                    amountToSettle = 0
                Else
                    AmountSettled = amountAvailable
                    amountToSettle -= amountAvailable
                End If

                undistributedAmountTable.Rows(index)("UndistributedAmount") = amountAvailable - AmountSettled

                prop.PaymentTransactionId = undistributedAmountTable.Rows(index)("PaymentTransactionId")
                prop.PaymentId = undistributedAmountTable.Rows(index)("PaymentTransactionId")
                prop.InvoiceId = row.Cells("InvoiceId").Value
                prop.Remarks = String.Format("Rs. {0} settled for invoice {1} against payment {2}",
                                             AmountSettled, row.Cells("InvoiceNo").Value, undistributedAmountTable.Rows(index)("PaymentTransactionNo"))
                prop.AmountSettled = AmountSettled
                prop.OpenCrNo = undistributedAmountTable.Rows(index)("PaymentTransactionNo")
                prop.CreatedBy = v_the_current_logged_in_user_name
                prop.DivisionId = v_the_current_division_id
                clsObj.Update_Undistributed_Amount(prop)
                index += 1
            End While
        Next
        MsgBox("Invoice settled successfully against payment(s).", vbExclamation, gblMessageHeading)
        cmbCustomerSettleInvoice.SelectedIndex = 0
        SettleInvoiceCustomerID = 0
        FillGrid()
        SetUndistributedAmount()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        For Each row As DataGridViewRow In dgvInvoiceToSettle.Rows
            row.Cells("AmountToReceive").Value = 0
        Next
        SetUndistributedAmount()
    End Sub

    Private Sub dgvInvoiceToSettle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoiceToSettle.CellEndEdit

    End Sub

    Private Sub dgvInvoiceToSettle_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvInvoiceToSettle.CellValidating
        If e.RowIndex >= 0 Then
            If dgvInvoiceToSettle.Columns(e.ColumnIndex).Name = "AmountToReceive" Then
                Dim result As Decimal = 0
                Dim resultBool As Boolean = False
                resultBool = Decimal.TryParse(e.FormattedValue, result)

                If Not resultBool Then
                    e.Cancel = True
                    MsgBox("Please enter valid value.", vbExclamation, gblMessageHeading)
                    Exit Sub
                End If

                If result > dgvInvoiceToSettle.Rows(e.RowIndex).Cells("PendingAmount").Value Then
                    e.Cancel = True
                    MsgBox("Amount To Receive cannot be greater than pending amount.", vbExclamation, gblMessageHeading)
                    Exit Sub
                End If

                Dim lastValue As Decimal = dgvInvoiceToSettle.Rows(e.RowIndex).Cells("AmountToReceive").Value
                If result > (UndistributedAmount + lastValue) Then
                    e.Cancel = True
                    MsgBox("Amount To Receive cannot be greater than available undistributed amount.", vbExclamation, gblMessageHeading)
                    Exit Sub
                End If

                UndistributedAmount = UndistributedAmount - result + lastValue
                lblUndistributedAmount.Text = UndistributedAmount.ToString("0.00")
            End If
        End If
    End Sub

    Private Function GetInvoiceSettledAmount() As Decimal
        Dim InvoiceSettledAmount As Decimal = 0
        For Each row As DataGridViewRow In dgvInvoiceToSettle.Rows
            InvoiceSettledAmount = InvoiceSettledAmount + row.Cells("AmountToReceive").Value
        Next
        Return InvoiceSettledAmount
    End Function

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        fill_ListPaymentgrid(txtSearch.Text)
    End Sub

    Private Sub cmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomer.SelectedIndexChanged

        cmbCustomer.SelectedIndex = cmbCustomer.FindStringExact(cmbCustomer.Text)

        If cmbCustomer.SelectedIndex > 0 Then

            Dim query As String = "DECLARE @AmountInHand DECIMAL(18,2) DECLARE @UndistributedAmount DECIMAL(18,2) SELECT @AmountInHand= ISNULL(SUM(CashIn), 0)-ISNULL(SUM(CashOut), 0) FROM dbo.LedgerMaster JOIN dbo.LedgerDetail ON dbo.LedgerDetail.LedgerId = dbo.LedgerMaster.LedgerId WHERE AccountId=" & cmbCustomer.SelectedValue &
       " SELECT  @UndistributedAmount=isnull(SUM(UndistributedAmount), 0) FROM dbo.PaymentTransaction WHERE StatusId =2 AND AccountId=" & cmbCustomer.SelectedValue &
       " SET @UndistributedAmount=ISNULL(@UndistributedAmount,0)+ISNULL((SELECT ISNULL(MAX(OpeningAmount),0)- ISNULL(sum(AmountSettled),0) FROM dbo.OpeningBalance left JOIN dbo.SettlementDetail" &
       " ON OpeningBalanceId=PaymentTransactionId WHERE TYPE=2 AND FkAccountId=" & cmbCustomer.SelectedValue & " ),0)" &
       " SELECT @AmountInHand AS AmountInHand,@UndistributedAmount AS UndistributedAmount"

            Dim dt As DataTable = clsObj.Fill_DataSet(query).Tables(0)

            If (dt.Rows(0)(0) < 0) Then
                lblPendingAmount.Text = (-dt.Rows(0)(0)).ToString() + " Dr."
                'lblAdvanceAmount.Text = "0.00"
            Else
                lblPendingAmount.Text = dt.Rows(0)(0).ToString() + " Cr."
                'lblAdvanceAmount.Text = dt.Rows(0)(0)
            End If
            lblUnDistributeAmount.Text = dt.Rows(0)(1)
        End If
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
            cmbCustomer.SelectedValue = dr("AccountId")
            cmbPaymentType.SelectedValue = dr("PaymentTypeId")
            txtReferenceNo.Text = dr("ChequeDraftNo")
            cmbBank.SelectedValue = dr("BankId")
            txtAmount.Text = dr("TotalAmountReceived")
            txtRemarks.Text = dr("Remarks")
            dtpPaymentDate.Value = dr("PaymentDate")
            dtpBankDate.Value = dr("BankDate")
            dtpReferenceDate.Value = dr("ChequeDraftDate")

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
                MessageBox.Show("This payment is already cancelled")
                Return
            End If

            clsObj.Cancel_PaymentEntries(Convert.ToInt32(flxList("PaymentId", flxList.SelectedRows(0).Index).Value()), PaymentStatus.Cancelled, 0, PaymentType.Receipt, Transaction_Type.Invoice_Settlement)
            MsgBox("Payment status has been updated.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            fill_ListPaymentgrid()

        End If

    End Sub


    Private Sub cmbPaymentType_Enter(sender As Object, e As EventArgs) Handles cmbPaymentType.Enter
        If Not cmbPaymentType.DroppedDown Then
            cmbPaymentType.DroppedDown = True
        End If
    End Sub

    Private Sub cmbBank_Enter(sender As Object, e As EventArgs) Handles cmbBank.Enter
        If Not cmbBank.DroppedDown Then
            cmbBank.DroppedDown = True
        End If
    End Sub

    Private Sub cmbPendingPayment_Enter(sender As Object, e As EventArgs) Handles cmbPendingPayment.Enter
        If Not cmbPendingPayment.DroppedDown Then
            cmbPendingPayment.DroppedDown = True
        End If
    End Sub

End Class
