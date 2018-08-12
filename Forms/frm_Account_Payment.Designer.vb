<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Account_Payment
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Account_Payment))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.BtnCancelInv = New System.Windows.Forms.Button()
        Me.GBMRSDetail = New System.Windows.Forms.GroupBox()
        Me.flxList = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GBDCMASTER = New System.Windows.Forms.GroupBox()
        Me.chk_GSTApplicable_BankId = New System.Windows.Forms.CheckBox()
        Me.lblGSTPercentageValue = New System.Windows.Forms.Label()
        Me.lblGSTPercentage = New System.Windows.Forms.Label()
        Me.chk_GSTApplicable = New System.Windows.Forms.CheckBox()
        Me.cmbGSTNature = New System.Windows.Forms.ComboBox()
        Me.lblGSTNature = New System.Windows.Forms.Label()
        Me.lblAdvanceAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPendingAmount = New System.Windows.Forms.Label()
        Me.dtpBankDate = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpReferenceDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbAccountToCredit = New System.Windows.Forms.ComboBox()
        Me.dtpPaymentDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbPaymentType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtReferenceNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbAccountToDebit = New System.Windows.Forms.ComboBox()
        Me.lblCap1 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.testCombo = New MMSPlus.AutoCompleteCombo()
        Me.TabControl1.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GBMRSDetail.SuspendLayout()
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GBDCMASTER.SuspendLayout()
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
        Me.BtnCancelInv.Location = New System.Drawing.Point(766, 544)
        Me.BtnCancelInv.Name = "BtnCancelInv"
        Me.BtnCancelInv.Size = New System.Drawing.Size(127, 30)
        Me.BtnCancelInv.TabIndex = 2
        Me.BtnCancelInv.Text = "Cancel "
        Me.BtnCancelInv.UseVisualStyleBackColor = False
        '
        'GBMRSDetail
        '
        Me.GBMRSDetail.Controls.Add(Me.flxList)
        Me.GBMRSDetail.Location = New System.Drawing.Point(19, 91)
        Me.GBMRSDetail.Name = "GBMRSDetail"
        Me.GBMRSDetail.Size = New System.Drawing.Size(877, 437)
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
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.flxList.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.flxList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxList.Size = New System.Drawing.Size(871, 418)
        Me.flxList.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSearch)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(874, 76)
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
        Me.txtSearch.Size = New System.Drawing.Size(741, 18)
        Me.txtSearch.TabIndex = 0
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
        Me.TabPage2.Controls.Add(Me.GBDCMASTER)
        Me.TabPage2.Controls.Add(Me.lblFormHeading)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GBDCMASTER
        '
        Me.GBDCMASTER.Controls.Add(Me.testCombo)
        Me.GBDCMASTER.Controls.Add(Me.chk_GSTApplicable_BankId)
        Me.GBDCMASTER.Controls.Add(Me.lblGSTPercentageValue)
        Me.GBDCMASTER.Controls.Add(Me.lblGSTPercentage)
        Me.GBDCMASTER.Controls.Add(Me.chk_GSTApplicable)
        Me.GBDCMASTER.Controls.Add(Me.cmbGSTNature)
        Me.GBDCMASTER.Controls.Add(Me.lblGSTNature)
        Me.GBDCMASTER.Controls.Add(Me.lblAdvanceAmount)
        Me.GBDCMASTER.Controls.Add(Me.Label5)
        Me.GBDCMASTER.Controls.Add(Me.lblPendingAmount)
        Me.GBDCMASTER.Controls.Add(Me.dtpBankDate)
        Me.GBDCMASTER.Controls.Add(Me.Label11)
        Me.GBDCMASTER.Controls.Add(Me.txtRemarks)
        Me.GBDCMASTER.Controls.Add(Me.Label10)
        Me.GBDCMASTER.Controls.Add(Me.dtpReferenceDate)
        Me.GBDCMASTER.Controls.Add(Me.Label9)
        Me.GBDCMASTER.Controls.Add(Me.cmbAccountToCredit)
        Me.GBDCMASTER.Controls.Add(Me.dtpPaymentDate)
        Me.GBDCMASTER.Controls.Add(Me.Label12)
        Me.GBDCMASTER.Controls.Add(Me.cmbPaymentType)
        Me.GBDCMASTER.Controls.Add(Me.Label4)
        Me.GBDCMASTER.Controls.Add(Me.txtAmount)
        Me.GBDCMASTER.Controls.Add(Me.Label2)
        Me.GBDCMASTER.Controls.Add(Me.txtReferenceNo)
        Me.GBDCMASTER.Controls.Add(Me.Label3)
        Me.GBDCMASTER.Controls.Add(Me.cmbAccountToDebit)
        Me.GBDCMASTER.Controls.Add(Me.lblCap1)
        Me.GBDCMASTER.Location = New System.Drawing.Point(23, 41)
        Me.GBDCMASTER.Name = "GBDCMASTER"
        Me.GBDCMASTER.Size = New System.Drawing.Size(859, 532)
        Me.GBDCMASTER.TabIndex = 0
        Me.GBDCMASTER.TabStop = False
        '
        'chk_GSTApplicable_BankId
        '
        Me.chk_GSTApplicable_BankId.AutoSize = True
        Me.chk_GSTApplicable_BankId.BackColor = System.Drawing.Color.Transparent
        Me.chk_GSTApplicable_BankId.Enabled = False
        Me.chk_GSTApplicable_BankId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chk_GSTApplicable_BankId.ForeColor = System.Drawing.Color.White
        Me.chk_GSTApplicable_BankId.Location = New System.Drawing.Point(510, 294)
        Me.chk_GSTApplicable_BankId.Name = "chk_GSTApplicable_BankId"
        Me.chk_GSTApplicable_BankId.Size = New System.Drawing.Size(128, 19)
        Me.chk_GSTApplicable_BankId.TabIndex = 70
        Me.chk_GSTApplicable_BankId.Text = "GST Applicable Cr."
        Me.chk_GSTApplicable_BankId.UseVisualStyleBackColor = False
        Me.chk_GSTApplicable_BankId.Visible = False
        '
        'lblGSTPercentageValue
        '
        Me.lblGSTPercentageValue.AutoSize = True
        Me.lblGSTPercentageValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTPercentageValue.ForeColor = System.Drawing.Color.White
        Me.lblGSTPercentageValue.Location = New System.Drawing.Point(761, 276)
        Me.lblGSTPercentageValue.Name = "lblGSTPercentageValue"
        Me.lblGSTPercentageValue.Size = New System.Drawing.Size(31, 15)
        Me.lblGSTPercentageValue.TabIndex = 69
        Me.lblGSTPercentageValue.Text = "0.00"
        '
        'lblGSTPercentage
        '
        Me.lblGSTPercentage.AutoSize = True
        Me.lblGSTPercentage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTPercentage.ForeColor = System.Drawing.Color.White
        Me.lblGSTPercentage.Location = New System.Drawing.Point(653, 276)
        Me.lblGSTPercentage.Name = "lblGSTPercentage"
        Me.lblGSTPercentage.Size = New System.Drawing.Size(82, 15)
        Me.lblGSTPercentage.TabIndex = 68
        Me.lblGSTPercentage.Text = "GST Amount :"
        '
        'chk_GSTApplicable
        '
        Me.chk_GSTApplicable.AutoSize = True
        Me.chk_GSTApplicable.BackColor = System.Drawing.Color.Transparent
        Me.chk_GSTApplicable.Enabled = False
        Me.chk_GSTApplicable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chk_GSTApplicable.ForeColor = System.Drawing.Color.White
        Me.chk_GSTApplicable.Location = New System.Drawing.Point(510, 275)
        Me.chk_GSTApplicable.Name = "chk_GSTApplicable"
        Me.chk_GSTApplicable.Size = New System.Drawing.Size(110, 19)
        Me.chk_GSTApplicable.TabIndex = 67
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
        Me.cmbGSTNature.Location = New System.Drawing.Point(134, 266)
        Me.cmbGSTNature.Name = "cmbGSTNature"
        Me.cmbGSTNature.Size = New System.Drawing.Size(306, 23)
        Me.cmbGSTNature.TabIndex = 62
        '
        'lblGSTNature
        '
        Me.lblGSTNature.AutoSize = True
        Me.lblGSTNature.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTNature.Location = New System.Drawing.Point(34, 268)
        Me.lblGSTNature.Name = "lblGSTNature"
        Me.lblGSTNature.Size = New System.Drawing.Size(77, 15)
        Me.lblGSTNature.TabIndex = 63
        Me.lblGSTNature.Text = "GST Nature :"
        '
        'lblAdvanceAmount
        '
        Me.lblAdvanceAmount.AutoSize = True
        Me.lblAdvanceAmount.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvanceAmount.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblAdvanceAmount.Location = New System.Drawing.Point(89, 80)
        Me.lblAdvanceAmount.Name = "lblAdvanceAmount"
        Me.lblAdvanceAmount.Size = New System.Drawing.Size(28, 18)
        Me.lblAdvanceAmount.TabIndex = 61
        Me.lblAdvanceAmount.Text = "Cr."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(35, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 15)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "Account :"
        '
        'lblPendingAmount
        '
        Me.lblPendingAmount.AutoSize = True
        Me.lblPendingAmount.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPendingAmount.ForeColor = System.Drawing.Color.Red
        Me.lblPendingAmount.Location = New System.Drawing.Point(89, 35)
        Me.lblPendingAmount.Name = "lblPendingAmount"
        Me.lblPendingAmount.Size = New System.Drawing.Size(28, 18)
        Me.lblPendingAmount.TabIndex = 59
        Me.lblPendingAmount.Text = "Dr."
        '
        'dtpBankDate
        '
        Me.dtpBankDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpBankDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpBankDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpBankDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBankDate.Location = New System.Drawing.Point(618, 226)
        Me.dtpBankDate.Name = "dtpBankDate"
        Me.dtpBankDate.Size = New System.Drawing.Size(194, 20)
        Me.dtpBankDate.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(507, 230)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 15)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "Bank Date :"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRemarks.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.White
        Me.txtRemarks.Location = New System.Drawing.Point(134, 319)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(678, 69)
        Me.txtRemarks.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(35, 319)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 15)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Remarks :"
        '
        'dtpReferenceDate
        '
        Me.dtpReferenceDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpReferenceDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpReferenceDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpReferenceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReferenceDate.Location = New System.Drawing.Point(619, 184)
        Me.dtpReferenceDate.Name = "dtpReferenceDate"
        Me.dtpReferenceDate.Size = New System.Drawing.Size(193, 20)
        Me.dtpReferenceDate.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(507, 175)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 30)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Cheque or " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference Date :"
        '
        'cmbAccountToCredit
        '
        Me.cmbAccountToCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbAccountToCredit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAccountToCredit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAccountToCredit.ForeColor = System.Drawing.Color.White
        Me.cmbAccountToCredit.FormattingEnabled = True
        Me.cmbAccountToCredit.Items.AddRange(New Object() {"---Select---", "Post Dated Cheque", "Temporary Receipt", "Credit Payment"})
        Me.cmbAccountToCredit.Location = New System.Drawing.Point(134, 78)
        Me.cmbAccountToCredit.Name = "cmbAccountToCredit"
        Me.cmbAccountToCredit.Size = New System.Drawing.Size(678, 23)
        Me.cmbAccountToCredit.TabIndex = 1
        '
        'dtpPaymentDate
        '
        Me.dtpPaymentDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpPaymentDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpPaymentDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaymentDate.Location = New System.Drawing.Point(619, 145)
        Me.dtpPaymentDate.Name = "dtpPaymentDate"
        Me.dtpPaymentDate.Size = New System.Drawing.Size(193, 20)
        Me.dtpPaymentDate.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(507, 143)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 15)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Payment Date :"
        '
        'cmbPaymentType
        '
        Me.cmbPaymentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaymentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPaymentType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPaymentType.ForeColor = System.Drawing.Color.White
        Me.cmbPaymentType.FormattingEnabled = True
        Me.cmbPaymentType.Items.AddRange(New Object() {"---Select---", "Post Dated Cheque", "Temporary Receipt", "Credit Payment"})
        Me.cmbPaymentType.Location = New System.Drawing.Point(134, 141)
        Me.cmbPaymentType.Name = "cmbPaymentType"
        Me.cmbPaymentType.Size = New System.Drawing.Size(306, 23)
        Me.cmbPaymentType.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(35, 143)
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
        Me.txtAmount.Location = New System.Drawing.Point(134, 223)
        Me.txtAmount.MaxLength = 0
        Me.txtAmount.Multiline = True
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(306, 23)
        Me.txtAmount.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(35, 226)
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
        Me.txtReferenceNo.Location = New System.Drawing.Point(134, 182)
        Me.txtReferenceNo.MaxLength = 0
        Me.txtReferenceNo.Multiline = True
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.Size = New System.Drawing.Size(306, 23)
        Me.txtReferenceNo.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(35, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 30)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Cheque or" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reference No :"
        '
        'cmbAccountToDebit
        '
        Me.cmbAccountToDebit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbAccountToDebit.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbAccountToDebit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAccountToDebit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAccountToDebit.ForeColor = System.Drawing.Color.White
        Me.cmbAccountToDebit.FormattingEnabled = True
        Me.cmbAccountToDebit.Location = New System.Drawing.Point(134, 33)
        Me.cmbAccountToDebit.Name = "cmbAccountToDebit"
        Me.cmbAccountToDebit.Size = New System.Drawing.Size(678, 23)
        Me.cmbAccountToDebit.TabIndex = 0
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(35, 35)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.Size = New System.Drawing.Size(56, 15)
        Me.lblCap1.TabIndex = 10
        Me.lblCap1.Text = "Account :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(351, 11)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(162, 25)
        Me.lblFormHeading.TabIndex = 5
        Me.lblFormHeading.Text = "Journal Entry"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'testCombo
        '
        Me.testCombo.FormattingEnabled = True
        Me.testCombo.Location = New System.Drawing.Point(134, 424)
        Me.testCombo.Name = "testCombo"
        Me.testCombo.ResetOnClear = False
        Me.testCombo.Size = New System.Drawing.Size(337, 21)
        Me.testCombo.TabIndex = 72
        '
        'frm_Account_Payment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Account_Payment"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GBMRSDetail.ResumeLayout(False)
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GBDCMASTER.ResumeLayout(False)
        Me.GBDCMASTER.PerformLayout()
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
    Friend WithEvents GBDCMASTER As GroupBox
    Friend WithEvents dtpBankDate As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents dtpReferenceDate As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents cmbAccountToCredit As ComboBox
    Friend WithEvents dtpPaymentDate As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbPaymentType As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtReferenceNo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbAccountToDebit As ComboBox
    Friend WithEvents lblCap1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblPendingAmount As Label
    Friend WithEvents lblAdvanceAmount As Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents BtnCancelInv As Button
    Friend WithEvents cmbGSTNature As ComboBox
    Friend WithEvents lblGSTNature As Label
    Friend WithEvents lblGSTPercentageValue As Label
    Friend WithEvents lblGSTPercentage As Label
    Friend WithEvents chk_GSTApplicable As CheckBox
    Friend WithEvents chk_GSTApplicable_BankId As CheckBox
    Friend WithEvents testCombo As AutoCompleteCombo
End Class
