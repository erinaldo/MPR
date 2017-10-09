Imports System.Data.SqlClient

Public Class frm_Invoice_Settlement
    Implements IForm

    Dim clsObj As New cls_Invoice_Settlement
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Invoice_Settlement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeControls()
    End Sub

    Private Sub InitializeControls()
        clsObj.ComboBind(cmbCustomer, "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" &
                 AccountGroups.Customers & " Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
        clsObj.ComboBind(cmbPaymentType, "Select [PaymentTypeId], [PaymentTypeName] from [PaymentTypeMaster] WHERE [IsActive_bit] = 1",
                          "PaymentTypeName", "PaymentTypeId", True)
        GetPMCode()
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
        lblUndistributedAmount.Text = "0.00"
        txtRemarks.Text = ""
    End Sub

    Private Sub cmbPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPaymentType.SelectedIndexChanged
        clsObj.ComboBind(cmbBank, "select BankID, BankAccountNo FROM dbo.BankMaster where PaymentTypeID = " & cmbPaymentType.SelectedValue,
                          "BankAccountNo", "BankID", True)
    End Sub

    Dim PM_Code As String
    Dim PM_No As Integer

    Private Sub GetPMCode()
        PM_Code = ""
        PM_No = 0
        Dim ds As New DataSet()
        ds = clsObj.fill_Data_set("GET_PaymentModule_No", "@DIV_ID", v_the_current_division_id)
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

            prpty = New cls_Invoice_Settlement_prop
            prpty.PaymentTransactionCode = PM_No
            prpty.PaymentTypeId = cmbPaymentType.SelectedValue
            prpty.AccountId = cmbCustomer.SelectedValue
            prpty.PaymentDate = dtpPaymentDate.Value
            prpty.ReferenceNo = txtReferenceNo.Text
            prpty.ReferenceDate = dtpReferenceDate.Value
            prpty.BankId = cmbBank.SelectedValue
            prpty.Remarks = txtRemarks.Text
            prpty.TotalAmountReceived = txtAmount.Text
            prpty.BalanceTotalAmount = txtAmount.Text
            prpty.StatusId = 0
            prpty.ReceiveChequeBounceAmount = 0
            prpty.BankDate = dtpBankDate.Value
            prpty.PdcPaymentTransactionId = 0
            prpty.CreatedBy = v_the_current_logged_in_user_name
            prpty.DivisionId = v_the_current_division_id

            clsObj.insert_Invoice_Settlement(prpty)

            MsgBox("Invoice Settlement has been Saved.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gblMessageHeading)
            InitializeControls()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Function Validation() As Boolean

        If cmbCustomer.SelectedIndex <= 0 Then
            MsgBox("Select Customer to enter payment.", vbExclamation, gblMessageHeading)
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
        Return True
    End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

End Class
