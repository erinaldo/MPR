﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Supplier_Invoice_Settlement
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Supplier_Invoice_Settlement))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.BtnCancelInv = New System.Windows.Forms.Button()
        Me.GBMRSDetail = New System.Windows.Forms.GroupBox()
        Me.flxList = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPaymentControl = New System.Windows.Forms.TabControl()
        Me.tabTakePayment = New System.Windows.Forms.TabPage()
        Me.GBDCMASTER = New System.Windows.Forms.GroupBox()
        Me.lblGSTPercentageValue = New System.Windows.Forms.Label()
        Me.chk_GSTApplicable = New System.Windows.Forms.CheckBox()
        Me.cmbGSTNature = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblUnDistributeAmount = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpPaymentDate = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpBankDate = New System.Windows.Forms.DateTimePicker()
        Me.chkBoxDistributeAmount = New System.Windows.Forms.CheckBox()
        Me.dtpReferenceDate = New System.Windows.Forms.DateTimePicker()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbBank = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbPaymentType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtReferenceNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.lblCap1 = New System.Windows.Forms.Label()
        Me.lblAdvanceAmount = New System.Windows.Forms.Label()
        Me.lblPendingAmount = New System.Windows.Forms.Label()
        Me.lblMRSDate = New System.Windows.Forms.Label()
        Me.lblMRSCode = New System.Windows.Forms.Label()
        Me.tabApprovePayment = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblReceivedBy = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnBounce = New System.Windows.Forms.Button()
        Me.btnApproved = New System.Windows.Forms.Button()
        Me.txtCancellationCharges = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.lblPaymentType = New System.Windows.Forms.Label()
        Me.lblReferenceNo = New System.Windows.Forms.Label()
        Me.lblBankDate = New System.Windows.Forms.Label()
        Me.lblReferenceDate = New System.Windows.Forms.Label()
        Me.lblPaymentDate = New System.Windows.Forms.Label()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.cmbPendingPayment = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkBoxDistributeAmountApprove = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lable23 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbCustomerApprovePayment = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tabDistributePayment = New System.Windows.Forms.TabPage()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSettleInvoice = New System.Windows.Forms.Button()
        Me.dgvInvoiceToSettle = New System.Windows.Forms.DataGridView()
        Me.MrnId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MrnNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MRNDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvoiceAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReceivedAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DebitedAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PendingAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountToReceive = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblUndistributedAmount = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnDistributeAmount = New System.Windows.Forms.Button()
        Me.cmbCustomerSettleInvoice = New System.Windows.Forms.ComboBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GBMRSDetail.SuspendLayout()
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPaymentControl.SuspendLayout()
        Me.tabTakePayment.SuspendLayout()
        Me.GBDCMASTER.SuspendLayout()
        Me.tabApprovePayment.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tabDistributePayment.SuspendLayout()
        CType(Me.dgvInvoiceToSettle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.List)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.TabIndex = 0
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.BtnCancelInv)
        Me.List.Controls.Add(Me.GBMRSDetail)
        Me.List.Controls.Add(Me.GroupBox2)
        Me.List.ForeColor = System.Drawing.Color.White
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(902, 600)
        Me.List.TabIndex = 0
        '
        'BtnCancelInv
        '
        Me.BtnCancelInv.BackColor = System.Drawing.Color.Tomato
        Me.BtnCancelInv.Location = New System.Drawing.Point(749, 554)
        Me.BtnCancelInv.Name = "BtnCancelInv"
        Me.BtnCancelInv.Size = New System.Drawing.Size(141, 30)
        Me.BtnCancelInv.TabIndex = 2
        Me.BtnCancelInv.Text = "Cancel "
        Me.BtnCancelInv.UseVisualStyleBackColor = False
        '
        'GBMRSDetail
        '
        Me.GBMRSDetail.Controls.Add(Me.flxList)
        Me.GBMRSDetail.Location = New System.Drawing.Point(13, 91)
        Me.GBMRSDetail.Name = "GBMRSDetail"
        Me.GBMRSDetail.Size = New System.Drawing.Size(877, 449)
        Me.GBMRSDetail.TabIndex = 1
        Me.GBMRSDetail.TabStop = False
        '
        'flxList
        '
        Me.flxList.AllowUserToAddRows = False
        Me.flxList.AllowUserToDeleteRows = False
        Me.flxList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.flxList.Location = New System.Drawing.Point(3, 16)
        Me.flxList.Name = "flxList"
        Me.flxList.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.flxList.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.flxList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxList.Size = New System.Drawing.Size(871, 430)
        Me.flxList.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSearch)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(877, 76)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(89, 32)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(768, 18)
        Me.txtSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Search By :"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.TabPaymentControl)
        Me.TabPage2.Controls.Add(Me.lblFormHeading)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'TabPaymentControl
        '
        Me.TabPaymentControl.Controls.Add(Me.tabTakePayment)
        Me.TabPaymentControl.Controls.Add(Me.tabApprovePayment)
        Me.TabPaymentControl.Controls.Add(Me.tabDistributePayment)
        Me.TabPaymentControl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabPaymentControl.Location = New System.Drawing.Point(3, 45)
        Me.TabPaymentControl.Name = "TabPaymentControl"
        Me.TabPaymentControl.SelectedIndex = 0
        Me.TabPaymentControl.Size = New System.Drawing.Size(896, 552)
        Me.TabPaymentControl.TabIndex = 0
        '
        'tabTakePayment
        '
        Me.tabTakePayment.BackColor = System.Drawing.Color.DimGray
        Me.tabTakePayment.Controls.Add(Me.GBDCMASTER)
        Me.tabTakePayment.Location = New System.Drawing.Point(4, 22)
        Me.tabTakePayment.Name = "tabTakePayment"
        Me.tabTakePayment.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTakePayment.Size = New System.Drawing.Size(888, 526)
        Me.tabTakePayment.TabIndex = 0
        Me.tabTakePayment.Text = "Make Payments"
        '
        'GBDCMASTER
        '
        Me.GBDCMASTER.Controls.Add(Me.lblGSTPercentageValue)
        Me.GBDCMASTER.Controls.Add(Me.chk_GSTApplicable)
        Me.GBDCMASTER.Controls.Add(Me.cmbGSTNature)
        Me.GBDCMASTER.Controls.Add(Me.Label22)
        Me.GBDCMASTER.Controls.Add(Me.Label10)
        Me.GBDCMASTER.Controls.Add(Me.lblUnDistributeAmount)
        Me.GBDCMASTER.Controls.Add(Me.Label12)
        Me.GBDCMASTER.Controls.Add(Me.dtpPaymentDate)
        Me.GBDCMASTER.Controls.Add(Me.Label25)
        Me.GBDCMASTER.Controls.Add(Me.Label9)
        Me.GBDCMASTER.Controls.Add(Me.Label5)
        Me.GBDCMASTER.Controls.Add(Me.dtpBankDate)
        Me.GBDCMASTER.Controls.Add(Me.chkBoxDistributeAmount)
        Me.GBDCMASTER.Controls.Add(Me.dtpReferenceDate)
        Me.GBDCMASTER.Controls.Add(Me.txtRemarks)
        Me.GBDCMASTER.Controls.Add(Me.Label11)
        Me.GBDCMASTER.Controls.Add(Me.cmbBank)
        Me.GBDCMASTER.Controls.Add(Me.Label8)
        Me.GBDCMASTER.Controls.Add(Me.cmbPaymentType)
        Me.GBDCMASTER.Controls.Add(Me.Label4)
        Me.GBDCMASTER.Controls.Add(Me.txtAmount)
        Me.GBDCMASTER.Controls.Add(Me.Label2)
        Me.GBDCMASTER.Controls.Add(Me.txtReferenceNo)
        Me.GBDCMASTER.Controls.Add(Me.Label3)
        Me.GBDCMASTER.Controls.Add(Me.cmbCustomer)
        Me.GBDCMASTER.Controls.Add(Me.lblCap1)
        Me.GBDCMASTER.Controls.Add(Me.lblAdvanceAmount)
        Me.GBDCMASTER.Controls.Add(Me.lblPendingAmount)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSDate)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSCode)
        Me.GBDCMASTER.Location = New System.Drawing.Point(24, 21)
        Me.GBDCMASTER.Name = "GBDCMASTER"
        Me.GBDCMASTER.Size = New System.Drawing.Size(841, 477)
        Me.GBDCMASTER.TabIndex = 0
        Me.GBDCMASTER.TabStop = False
        '
        'lblGSTPercentageValue
        '
        Me.lblGSTPercentageValue.AutoSize = True
        Me.lblGSTPercentageValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTPercentageValue.ForeColor = System.Drawing.Color.White
        Me.lblGSTPercentageValue.Location = New System.Drawing.Point(777, 302)
        Me.lblGSTPercentageValue.Name = "lblGSTPercentageValue"
        Me.lblGSTPercentageValue.Size = New System.Drawing.Size(31, 15)
        Me.lblGSTPercentageValue.TabIndex = 73
        Me.lblGSTPercentageValue.Text = "0.00"
        '
        'chk_GSTApplicable
        '
        Me.chk_GSTApplicable.AutoSize = True
        Me.chk_GSTApplicable.BackColor = System.Drawing.Color.Transparent
        Me.chk_GSTApplicable.Enabled = False
        Me.chk_GSTApplicable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chk_GSTApplicable.ForeColor = System.Drawing.Color.White
        Me.chk_GSTApplicable.Location = New System.Drawing.Point(533, 292)
        Me.chk_GSTApplicable.Name = "chk_GSTApplicable"
        Me.chk_GSTApplicable.Size = New System.Drawing.Size(110, 19)
        Me.chk_GSTApplicable.TabIndex = 72
        Me.chk_GSTApplicable.Text = "GST Applicable"
        Me.chk_GSTApplicable.UseVisualStyleBackColor = False
        '
        'cmbGSTNature
        '
        Me.cmbGSTNature.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbGSTNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGSTNature.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbGSTNature.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGSTNature.ForeColor = System.Drawing.Color.White
        Me.cmbGSTNature.FormattingEnabled = True
        Me.cmbGSTNature.Items.AddRange(New Object() {"---Select---", "Post Dated Cheque", "Temporary Receipt", "Credit Payment"})
        Me.cmbGSTNature.Location = New System.Drawing.Point(116, 294)
        Me.cmbGSTNature.Name = "cmbGSTNature"
        Me.cmbGSTNature.Size = New System.Drawing.Size(312, 23)
        Me.cmbGSTNature.TabIndex = 70
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(16, 296)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(77, 15)
        Me.Label22.TabIndex = 71
        Me.Label22.Text = "GST Nature :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 320)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 30)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "Remarks or " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Narration :"
        '
        'lblUnDistributeAmount
        '
        Me.lblUnDistributeAmount.BackColor = System.Drawing.Color.Black
        Me.lblUnDistributeAmount.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnDistributeAmount.ForeColor = System.Drawing.Color.Gold
        Me.lblUnDistributeAmount.Location = New System.Drawing.Point(658, 65)
        Me.lblUnDistributeAmount.Name = "lblUnDistributeAmount"
        Me.lblUnDistributeAmount.Size = New System.Drawing.Size(150, 23)
        Me.lblUnDistributeAmount.TabIndex = 3
        Me.lblUnDistributeAmount.Text = "0.00"
        Me.lblUnDistributeAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(532, 128)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 15)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Payment Date :"
        '
        'dtpPaymentDate
        '
        Me.dtpPaymentDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpPaymentDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpPaymentDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaymentDate.Location = New System.Drawing.Point(656, 124)
        Me.dtpPaymentDate.Name = "dtpPaymentDate"
        Me.dtpPaymentDate.Size = New System.Drawing.Size(152, 20)
        Me.dtpPaymentDate.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(533, 68)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(126, 15)
        Me.Label25.TabIndex = 60
        Me.Label25.Text = "UnDistribute Amount :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(530, 164)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 30)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Cheque or " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference Date :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(32, 419)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(343, 15)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "(keep checkbox unchecked if want to settle invoices manually)"
        '
        'dtpBankDate
        '
        Me.dtpBankDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpBankDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpBankDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpBankDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBankDate.Location = New System.Drawing.Point(656, 218)
        Me.dtpBankDate.Name = "dtpBankDate"
        Me.dtpBankDate.Size = New System.Drawing.Size(153, 20)
        Me.dtpBankDate.TabIndex = 6
        '
        'chkBoxDistributeAmount
        '
        Me.chkBoxDistributeAmount.AutoSize = True
        Me.chkBoxDistributeAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxDistributeAmount.Location = New System.Drawing.Point(19, 399)
        Me.chkBoxDistributeAmount.Name = "chkBoxDistributeAmount"
        Me.chkBoxDistributeAmount.Size = New System.Drawing.Size(525, 22)
        Me.chkBoxDistributeAmount.TabIndex = 9
        Me.chkBoxDistributeAmount.Text = "Distribute Released && Advanced Amount among unsettled Invoices"
        Me.chkBoxDistributeAmount.UseVisualStyleBackColor = True
        '
        'dtpReferenceDate
        '
        Me.dtpReferenceDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpReferenceDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpReferenceDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpReferenceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReferenceDate.Location = New System.Drawing.Point(657, 172)
        Me.dtpReferenceDate.Name = "dtpReferenceDate"
        Me.dtpReferenceDate.Size = New System.Drawing.Size(152, 20)
        Me.dtpReferenceDate.TabIndex = 4
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRemarks.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.White
        Me.txtRemarks.Location = New System.Drawing.Point(119, 323)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(689, 47)
        Me.txtRemarks.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(532, 221)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 15)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "Bank Date :"
        '
        'cmbBank
        '
        Me.cmbBank.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBank.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBank.ForeColor = System.Drawing.Color.White
        Me.cmbBank.FormattingEnabled = True
        Me.cmbBank.Location = New System.Drawing.Point(122, 218)
        Me.cmbBank.Name = "cmbBank"
        Me.cmbBank.Size = New System.Drawing.Size(306, 23)
        Me.cmbBank.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 221)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 15)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Bank Name :"
        '
        'cmbPaymentType
        '
        Me.cmbPaymentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaymentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPaymentType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPaymentType.ForeColor = System.Drawing.Color.White
        Me.cmbPaymentType.FormattingEnabled = True
        Me.cmbPaymentType.Location = New System.Drawing.Point(122, 121)
        Me.cmbPaymentType.Name = "cmbPaymentType"
        Me.cmbPaymentType.Size = New System.Drawing.Size(306, 23)
        Me.cmbPaymentType.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 15)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Payment Type :"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmount.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.Color.White
        Me.txtAmount.Location = New System.Drawing.Point(119, 263)
        Me.txtAmount.MaxLength = 0
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(290, 19)
        Me.txtAmount.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 266)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Amount :"
        '
        'txtReferenceNo
        '
        Me.txtReferenceNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReferenceNo.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReferenceNo.ForeColor = System.Drawing.Color.White
        Me.txtReferenceNo.Location = New System.Drawing.Point(122, 169)
        Me.txtReferenceNo.MaxLength = 0
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.Size = New System.Drawing.Size(287, 19)
        Me.txtReferenceNo.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 30)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Cheque or" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference No :"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCustomer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCustomer.ForeColor = System.Drawing.Color.White
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(122, 28)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(686, 23)
        Me.cmbCustomer.TabIndex = 0
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(16, 30)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.Size = New System.Drawing.Size(93, 15)
        Me.lblCap1.TabIndex = 10
        Me.lblCap1.Text = "Account Name :"
        '
        'lblAdvanceAmount
        '
        Me.lblAdvanceAmount.BackColor = System.Drawing.Color.Black
        Me.lblAdvanceAmount.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvanceAmount.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblAdvanceAmount.Location = New System.Drawing.Point(377, 65)
        Me.lblAdvanceAmount.Name = "lblAdvanceAmount"
        Me.lblAdvanceAmount.Size = New System.Drawing.Size(150, 23)
        Me.lblAdvanceAmount.TabIndex = 2
        Me.lblAdvanceAmount.Text = "0.00"
        Me.lblAdvanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPendingAmount
        '
        Me.lblPendingAmount.BackColor = System.Drawing.Color.Black
        Me.lblPendingAmount.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPendingAmount.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblPendingAmount.Location = New System.Drawing.Point(123, 65)
        Me.lblPendingAmount.Name = "lblPendingAmount"
        Me.lblPendingAmount.Size = New System.Drawing.Size(150, 23)
        Me.lblPendingAmount.TabIndex = 1
        Me.lblPendingAmount.Text = "0.00"
        Me.lblPendingAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMRSDate
        '
        Me.lblMRSDate.AutoSize = True
        Me.lblMRSDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSDate.Location = New System.Drawing.Point(274, 67)
        Me.lblMRSDate.Name = "lblMRSDate"
        Me.lblMRSDate.Size = New System.Drawing.Size(104, 15)
        Me.lblMRSDate.TabIndex = 7
        Me.lblMRSDate.Text = "Advance Amount :"
        '
        'lblMRSCode
        '
        Me.lblMRSCode.AutoSize = True
        Me.lblMRSCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSCode.Location = New System.Drawing.Point(16, 66)
        Me.lblMRSCode.Name = "lblMRSCode"
        Me.lblMRSCode.Size = New System.Drawing.Size(104, 15)
        Me.lblMRSCode.TabIndex = 5
        Me.lblMRSCode.Text = "Pending Amount :"
        '
        'tabApprovePayment
        '
        Me.tabApprovePayment.BackColor = System.Drawing.Color.DimGray
        Me.tabApprovePayment.Controls.Add(Me.GroupBox1)
        Me.tabApprovePayment.Location = New System.Drawing.Point(4, 22)
        Me.tabApprovePayment.Name = "tabApprovePayment"
        Me.tabApprovePayment.Padding = New System.Windows.Forms.Padding(3)
        Me.tabApprovePayment.Size = New System.Drawing.Size(888, 526)
        Me.tabApprovePayment.TabIndex = 1
        Me.tabApprovePayment.Text = "Approve Payments"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblReceivedBy)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.btnBounce)
        Me.GroupBox1.Controls.Add(Me.btnApproved)
        Me.GroupBox1.Controls.Add(Me.txtCancellationCharges)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.lblRemarks)
        Me.GroupBox1.Controls.Add(Me.lblBankName)
        Me.GroupBox1.Controls.Add(Me.lblPaymentType)
        Me.GroupBox1.Controls.Add(Me.lblReferenceNo)
        Me.GroupBox1.Controls.Add(Me.lblBankDate)
        Me.GroupBox1.Controls.Add(Me.lblReferenceDate)
        Me.GroupBox1.Controls.Add(Me.lblPaymentDate)
        Me.GroupBox1.Controls.Add(Me.lblAmount)
        Me.GroupBox1.Controls.Add(Me.cmbPendingPayment)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.chkBoxDistributeAmountApprove)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.lable23)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.cmbCustomerApprovePayment)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(847, 479)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblReceivedBy
        '
        Me.lblReceivedBy.AutoSize = True
        Me.lblReceivedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedBy.Location = New System.Drawing.Point(178, 318)
        Me.lblReceivedBy.Name = "lblReceivedBy"
        Me.lblReceivedBy.Size = New System.Drawing.Size(85, 15)
        Me.lblReceivedBy.TabIndex = 75
        Me.lblReceivedBy.Text = "Received By"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(34, 316)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 15)
        Me.Label23.TabIndex = 74
        Me.Label23.Text = "Received By"
        '
        'btnBounce
        '
        Me.btnBounce.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBounce.FlatAppearance.BorderSize = 0
        Me.btnBounce.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBounce.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBounce.ForeColor = System.Drawing.Color.White
        Me.btnBounce.Location = New System.Drawing.Point(627, 436)
        Me.btnBounce.Name = "btnBounce"
        Me.btnBounce.Size = New System.Drawing.Size(86, 23)
        Me.btnBounce.TabIndex = 5
        Me.btnBounce.Text = "Bounced"
        Me.btnBounce.UseVisualStyleBackColor = False
        '
        'btnApproved
        '
        Me.btnApproved.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnApproved.FlatAppearance.BorderSize = 0
        Me.btnApproved.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApproved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApproved.Location = New System.Drawing.Point(722, 436)
        Me.btnApproved.Name = "btnApproved"
        Me.btnApproved.Size = New System.Drawing.Size(86, 23)
        Me.btnApproved.TabIndex = 4
        Me.btnApproved.Text = "Approved"
        Me.btnApproved.UseVisualStyleBackColor = False
        '
        'txtCancellationCharges
        '
        Me.txtCancellationCharges.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCancellationCharges.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCancellationCharges.Enabled = False
        Me.txtCancellationCharges.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCancellationCharges.ForeColor = System.Drawing.Color.White
        Me.txtCancellationCharges.Location = New System.Drawing.Point(529, 315)
        Me.txtCancellationCharges.MaxLength = 0
        Me.txtCancellationCharges.Name = "txtCancellationCharges"
        Me.txtCancellationCharges.Size = New System.Drawing.Size(161, 19)
        Me.txtCancellationCharges.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(401, 315)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(98, 15)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "Bounce Charges"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(526, 280)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(64, 15)
        Me.lblRemarks.TabIndex = 69
        Me.lblRemarks.Text = "Remarks"
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(178, 237)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(81, 15)
        Me.lblBankName.TabIndex = 68
        Me.lblBankName.Text = "Bank Name"
        '
        'lblPaymentType
        '
        Me.lblPaymentType.AutoSize = True
        Me.lblPaymentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentType.Location = New System.Drawing.Point(178, 151)
        Me.lblPaymentType.Name = "lblPaymentType"
        Me.lblPaymentType.Size = New System.Drawing.Size(100, 15)
        Me.lblPaymentType.TabIndex = 67
        Me.lblPaymentType.Text = "Payment Type "
        '
        'lblReferenceNo
        '
        Me.lblReferenceNo.AutoSize = True
        Me.lblReferenceNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReferenceNo.Location = New System.Drawing.Point(178, 181)
        Me.lblReferenceNo.Name = "lblReferenceNo"
        Me.lblReferenceNo.Size = New System.Drawing.Size(95, 30)
        Me.lblReferenceNo.TabIndex = 66
        Me.lblReferenceNo.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference No"
        '
        'lblBankDate
        '
        Me.lblBankDate.AutoSize = True
        Me.lblBankDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDate.Location = New System.Drawing.Point(526, 237)
        Me.lblBankDate.Name = "lblBankDate"
        Me.lblBankDate.Size = New System.Drawing.Size(73, 15)
        Me.lblBankDate.TabIndex = 65
        Me.lblBankDate.Text = "Bank Date"
        '
        'lblReferenceDate
        '
        Me.lblReferenceDate.AutoSize = True
        Me.lblReferenceDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReferenceDate.Location = New System.Drawing.Point(526, 196)
        Me.lblReferenceDate.Name = "lblReferenceDate"
        Me.lblReferenceDate.Size = New System.Drawing.Size(107, 15)
        Me.lblReferenceDate.TabIndex = 64
        Me.lblReferenceDate.Text = "Reference Date"
        '
        'lblPaymentDate
        '
        Me.lblPaymentDate.AutoSize = True
        Me.lblPaymentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDate.Location = New System.Drawing.Point(526, 151)
        Me.lblPaymentDate.Name = "lblPaymentDate"
        Me.lblPaymentDate.Size = New System.Drawing.Size(96, 15)
        Me.lblPaymentDate.TabIndex = 63
        Me.lblPaymentDate.Text = "Payment Date"
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(178, 280)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(59, 15)
        Me.lblAmount.TabIndex = 62
        Me.lblAmount.Text = "Amount "
        '
        'cmbPendingPayment
        '
        Me.cmbPendingPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbPendingPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPendingPayment.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPendingPayment.ForeColor = System.Drawing.Color.White
        Me.cmbPendingPayment.FormattingEnabled = True
        Me.cmbPendingPayment.Location = New System.Drawing.Point(181, 73)
        Me.cmbPendingPayment.Name = "cmbPendingPayment"
        Me.cmbPendingPayment.Size = New System.Drawing.Size(511, 23)
        Me.cmbPendingPayment.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(34, 75)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(123, 15)
        Me.Label21.TabIndex = 61
        Me.Label21.Text = "Payments Inprocess :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(50, 401)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(343, 15)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "(keep checkbox unchecked if want to settle invoices manually)"
        '
        'chkBoxDistributeAmountApprove
        '
        Me.chkBoxDistributeAmountApprove.AutoSize = True
        Me.chkBoxDistributeAmountApprove.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxDistributeAmountApprove.Location = New System.Drawing.Point(37, 381)
        Me.chkBoxDistributeAmountApprove.Name = "chkBoxDistributeAmountApprove"
        Me.chkBoxDistributeAmountApprove.Size = New System.Drawing.Size(525, 22)
        Me.chkBoxDistributeAmountApprove.TabIndex = 3
        Me.chkBoxDistributeAmountApprove.Text = "Distribute Released && Advanced Amount among unsettled Invoices"
        Me.chkBoxDistributeAmountApprove.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(401, 237)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 15)
        Me.Label7.TabIndex = 58
        Me.Label7.Text = "Bank Date :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(401, 282)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(63, 15)
        Me.Label13.TabIndex = 55
        Me.Label13.Text = "Remarks :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(401, 177)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 30)
        Me.Label14.TabIndex = 53
        Me.Label14.Text = "Cheque or " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference Date :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(34, 237)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 15)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "Bank Name :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(401, 151)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(90, 15)
        Me.Label16.TabIndex = 48
        Me.Label16.Text = "Payment Date :"
        '
        'lable23
        '
        Me.lable23.AutoSize = True
        Me.lable23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lable23.Location = New System.Drawing.Point(34, 151)
        Me.lable23.Name = "lable23"
        Me.lable23.Size = New System.Drawing.Size(90, 15)
        Me.lable23.TabIndex = 46
        Me.lable23.Text = "Payment Type :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(34, 280)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 15)
        Me.Label18.TabIndex = 44
        Me.Label18.Text = "Amount :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(34, 181)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(89, 30)
        Me.Label19.TabIndex = 15
        Me.Label19.Text = "Cheque or" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference No :"
        '
        'cmbCustomerApprovePayment
        '
        Me.cmbCustomerApprovePayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCustomerApprovePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCustomerApprovePayment.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCustomerApprovePayment.ForeColor = System.Drawing.Color.White
        Me.cmbCustomerApprovePayment.FormattingEnabled = True
        Me.cmbCustomerApprovePayment.Location = New System.Drawing.Point(181, 30)
        Me.cmbCustomerApprovePayment.Name = "cmbCustomerApprovePayment"
        Me.cmbCustomerApprovePayment.Size = New System.Drawing.Size(627, 23)
        Me.cmbCustomerApprovePayment.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(34, 32)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(93, 15)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "Account Name :"
        '
        'tabDistributePayment
        '
        Me.tabDistributePayment.BackColor = System.Drawing.Color.DimGray
        Me.tabDistributePayment.Controls.Add(Me.btnClear)
        Me.tabDistributePayment.Controls.Add(Me.btnSettleInvoice)
        Me.tabDistributePayment.Controls.Add(Me.dgvInvoiceToSettle)
        Me.tabDistributePayment.Controls.Add(Me.GroupBox3)
        Me.tabDistributePayment.Location = New System.Drawing.Point(4, 22)
        Me.tabDistributePayment.Name = "tabDistributePayment"
        Me.tabDistributePayment.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDistributePayment.Size = New System.Drawing.Size(888, 526)
        Me.tabDistributePayment.TabIndex = 2
        Me.tabDistributePayment.Text = "Settle Invoices"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(672, 488)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSettleInvoice
        '
        Me.btnSettleInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSettleInvoice.FlatAppearance.BorderSize = 0
        Me.btnSettleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSettleInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSettleInvoice.Location = New System.Drawing.Point(778, 488)
        Me.btnSettleInvoice.Name = "btnSettleInvoice"
        Me.btnSettleInvoice.Size = New System.Drawing.Size(100, 25)
        Me.btnSettleInvoice.TabIndex = 2
        Me.btnSettleInvoice.Text = "Settle Invoice"
        Me.btnSettleInvoice.UseVisualStyleBackColor = False
        '
        'dgvInvoiceToSettle
        '
        Me.dgvInvoiceToSettle.AllowUserToAddRows = False
        Me.dgvInvoiceToSettle.AllowUserToDeleteRows = False
        Me.dgvInvoiceToSettle.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvInvoiceToSettle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvoiceToSettle.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MrnId, Me.MrnNo, Me.InvoiceNo, Me.MRNDate, Me.InvoiceAmount, Me.ReceivedAmount, Me.DebitedAmount, Me.PendingAmount, Me.AmountToReceive})
        Me.dgvInvoiceToSettle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvInvoiceToSettle.GridColor = System.Drawing.Color.Gainsboro
        Me.dgvInvoiceToSettle.Location = New System.Drawing.Point(1, 123)
        Me.dgvInvoiceToSettle.Name = "dgvInvoiceToSettle"
        Me.dgvInvoiceToSettle.RowHeadersVisible = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkOrange
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.dgvInvoiceToSettle.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvInvoiceToSettle.Size = New System.Drawing.Size(887, 355)
        Me.dgvInvoiceToSettle.TabIndex = 1
        '
        'MrnId
        '
        Me.MrnId.HeaderText = "MrnId"
        Me.MrnId.Name = "MrnId"
        Me.MrnId.ReadOnly = True
        Me.MrnId.Visible = False
        '
        'MrnNo
        '
        Me.MrnNo.HeaderText = "MrnNo"
        Me.MrnNo.Name = "MrnNo"
        Me.MrnNo.ReadOnly = True
        '
        'InvoiceNo
        '
        Me.InvoiceNo.HeaderText = "Invoice No"
        Me.InvoiceNo.MinimumWidth = 100
        Me.InvoiceNo.Name = "InvoiceNo"
        Me.InvoiceNo.ReadOnly = True
        '
        'MRNDate
        '
        Me.MRNDate.HeaderText = "Mrn Date"
        Me.MRNDate.Name = "MRNDate"
        Me.MRNDate.ReadOnly = True
        Me.MRNDate.Width = 85
        '
        'InvoiceAmount
        '
        Me.InvoiceAmount.HeaderText = "Invoice Amount"
        Me.InvoiceAmount.Name = "InvoiceAmount"
        Me.InvoiceAmount.ReadOnly = True
        Me.InvoiceAmount.Width = 110
        '
        'ReceivedAmount
        '
        Me.ReceivedAmount.HeaderText = "Released Amount"
        Me.ReceivedAmount.Name = "ReceivedAmount"
        Me.ReceivedAmount.ReadOnly = True
        Me.ReceivedAmount.Width = 118
        '
        'DebitedAmount
        '
        Me.DebitedAmount.HeaderText = "Debited Amount"
        Me.DebitedAmount.Name = "DebitedAmount"
        Me.DebitedAmount.ReadOnly = True
        Me.DebitedAmount.Width = 110
        '
        'PendingAmount
        '
        Me.PendingAmount.HeaderText = "Pending Amount"
        Me.PendingAmount.Name = "PendingAmount"
        Me.PendingAmount.ReadOnly = True
        Me.PendingAmount.Width = 110
        '
        'AmountToReceive
        '
        Me.AmountToReceive.HeaderText = "AmountToRelease"
        Me.AmountToReceive.Name = "AmountToReceive"
        Me.AmountToReceive.Width = 125
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblUndistributedAmount)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.btnDistributeAmount)
        Me.GroupBox3.Controls.Add(Me.cmbCustomerSettleInvoice)
        Me.GroupBox3.Controls.Add(Me.Label44)
        Me.GroupBox3.Location = New System.Drawing.Point(2, -3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(885, 117)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'lblUndistributedAmount
        '
        Me.lblUndistributedAmount.BackColor = System.Drawing.Color.Black
        Me.lblUndistributedAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUndistributedAmount.ForeColor = System.Drawing.Color.Gold
        Me.lblUndistributedAmount.Location = New System.Drawing.Point(182, 71)
        Me.lblUndistributedAmount.Name = "lblUndistributedAmount"
        Me.lblUndistributedAmount.Size = New System.Drawing.Size(150, 25)
        Me.lblUndistributedAmount.TabIndex = 1
        Me.lblUndistributedAmount.Text = "0.00"
        Me.lblUndistributedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(35, 75)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(131, 15)
        Me.Label24.TabIndex = 74
        Me.Label24.Text = "Undistributed Amount :"
        '
        'btnDistributeAmount
        '
        Me.btnDistributeAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDistributeAmount.FlatAppearance.BorderSize = 0
        Me.btnDistributeAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDistributeAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDistributeAmount.Location = New System.Drawing.Point(719, 72)
        Me.btnDistributeAmount.Name = "btnDistributeAmount"
        Me.btnDistributeAmount.Size = New System.Drawing.Size(124, 23)
        Me.btnDistributeAmount.TabIndex = 1
        Me.btnDistributeAmount.Text = "Distribute Amount"
        Me.btnDistributeAmount.UseVisualStyleBackColor = False
        '
        'cmbCustomerSettleInvoice
        '
        Me.cmbCustomerSettleInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCustomerSettleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCustomerSettleInvoice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCustomerSettleInvoice.ForeColor = System.Drawing.Color.White
        Me.cmbCustomerSettleInvoice.FormattingEnabled = True
        Me.cmbCustomerSettleInvoice.Location = New System.Drawing.Point(182, 29)
        Me.cmbCustomerSettleInvoice.Name = "cmbCustomerSettleInvoice"
        Me.cmbCustomerSettleInvoice.Size = New System.Drawing.Size(661, 23)
        Me.cmbCustomerSettleInvoice.TabIndex = 0
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(35, 31)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(93, 15)
        Me.Label44.TabIndex = 10
        Me.Label44.Text = "Account Name :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(340, 10)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(198, 25)
        Me.lblFormHeading.TabIndex = 5
        Me.lblFormHeading.Text = "Account Payable"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Supplier_Invoice_Settlement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Supplier_Invoice_Settlement"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GBMRSDetail.ResumeLayout(False)
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPaymentControl.ResumeLayout(False)
        Me.tabTakePayment.ResumeLayout(False)
        Me.GBDCMASTER.ResumeLayout(False)
        Me.GBDCMASTER.PerformLayout()
        Me.tabApprovePayment.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tabDistributePayment.ResumeLayout(False)
        CType(Me.dgvInvoiceToSettle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents GBMRSDetail As System.Windows.Forms.GroupBox
    Friend WithEvents flxList As DataGridView
    Friend WithEvents lblFormHeading As Label
    Friend WithEvents TabPaymentControl As TabControl
    Friend WithEvents tabTakePayment As TabPage
    Friend WithEvents GBDCMASTER As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents chkBoxDistributeAmount As CheckBox
    Friend WithEvents dtpBankDate As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents dtpReferenceDate As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents cmbBank As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents dtpPaymentDate As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbPaymentType As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtReferenceNo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbCustomer As ComboBox
    Friend WithEvents lblCap1 As Label
    Friend WithEvents lblAdvanceAmount As Label
    Friend WithEvents lblPendingAmount As Label
    Friend WithEvents lblMRSDate As Label
    Friend WithEvents lblMRSCode As Label
    Friend WithEvents tabApprovePayment As TabPage
    Friend WithEvents tabDistributePayment As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents chkBoxDistributeAmountApprove As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents lable23 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents cmbCustomerApprovePayment As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents cmbPendingPayment As ComboBox
    Friend WithEvents Label21 As Label
    Friend WithEvents lblBankDate As Label
    Friend WithEvents lblReferenceDate As Label
    Friend WithEvents lblPaymentDate As Label
    Friend WithEvents lblAmount As Label
    Friend WithEvents lblRemarks As Label
    Friend WithEvents lblBankName As Label
    Friend WithEvents lblPaymentType As Label
    Friend WithEvents lblReferenceNo As Label
    Friend WithEvents txtCancellationCharges As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents btnBounce As Button
    Friend WithEvents btnApproved As Button
    Friend WithEvents lblReceivedBy As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnDistributeAmount As Button
    Friend WithEvents cmbCustomerSettleInvoice As ComboBox
    Friend WithEvents Label44 As Label
    Friend WithEvents lblUndistributedAmount As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents dgvInvoiceToSettle As DataGridView
    Friend WithEvents btnSettleInvoice As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lblUnDistributeAmount As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents BtnCancelInv As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents MrnId As DataGridViewTextBoxColumn
    Friend WithEvents MrnNo As DataGridViewTextBoxColumn
    Friend WithEvents InvoiceNo As DataGridViewTextBoxColumn
    Friend WithEvents MRNDate As DataGridViewTextBoxColumn
    Friend WithEvents InvoiceAmount As DataGridViewTextBoxColumn
    Friend WithEvents ReceivedAmount As DataGridViewTextBoxColumn
    Friend WithEvents DebitedAmount As DataGridViewTextBoxColumn
    Friend WithEvents PendingAmount As DataGridViewTextBoxColumn
    Friend WithEvents AmountToReceive As DataGridViewTextBoxColumn
    Friend WithEvents lblGSTPercentageValue As Label
    Friend WithEvents chk_GSTApplicable As CheckBox
    Friend WithEvents cmbGSTNature As ComboBox
    Friend WithEvents Label22 As Label
End Class
