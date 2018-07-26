<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DebitNote
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_DebitNote))
        Me.TbRMRN = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GBRMWPM = New System.Windows.Forms.GroupBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblCessAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDebit = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblVatAmount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lnkCalculateDebitAmt = New System.Windows.Forms.LinkLabel()
        Me.FLXGRD_MaterialItem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_INVDate = New System.Windows.Forms.TextBox()
        Me.txt_INVNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbsupplier = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_DNDate = New System.Windows.Forms.Label()
        Me.lblDN_Code = New System.Windows.Forms.Label()
        Me.lblReverseCode = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lbl_Remarks = New System.Windows.Forms.Label()
        Me.lblReceivedDate = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.cmbMRNNo = New System.Windows.Forms.ComboBox()
        Me.lblSelectMRNNO = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblGST3 = New System.Windows.Forms.Label()
        Me.lblGST28 = New System.Windows.Forms.Label()
        Me.lblGST18 = New System.Windows.Forms.Label()
        Me.lblGST12 = New System.Windows.Forms.Label()
        Me.lblGST5 = New System.Windows.Forms.Label()
        Me.lblGST0 = New System.Windows.Forms.Label()
        Me.lblGSTHeader = New System.Windows.Forms.Label()
        Me.lblGSTDetail = New System.Windows.Forms.Label()
        Me.TbRMRN.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GBRMWPM.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.FLXGRD_MaterialItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TbRMRN
        '
        Me.TbRMRN.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TbRMRN.Controls.Add(Me.TabPage1)
        Me.TbRMRN.Controls.Add(Me.TabPage2)
        Me.TbRMRN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TbRMRN.ImageList = Me.ImageList1
        Me.TbRMRN.Location = New System.Drawing.Point(0, 0)
        Me.TbRMRN.Name = "TbRMRN"
        Me.TbRMRN.SelectedIndex = 0
        Me.TbRMRN.Size = New System.Drawing.Size(910, 630)
        Me.TbRMRN.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GBRMWPM)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtSearch)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(864, 76)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
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
        Me.txtSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search By :"
        '
        'GBRMWPM
        '
        Me.GBRMWPM.Controls.Add(Me.dgvList)
        Me.GBRMWPM.ForeColor = System.Drawing.Color.White
        Me.GBRMWPM.Location = New System.Drawing.Point(17, 108)
        Me.GBRMWPM.Name = "GBRMWPM"
        Me.GBRMWPM.Size = New System.Drawing.Size(867, 471)
        Me.GBRMWPM.TabIndex = 1
        Me.GBRMWPM.TabStop = False
        Me.GBRMWPM.Text = "List of Debit Notes"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.Location = New System.Drawing.Point(3, 16)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.dgvList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.dgvList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(861, 452)
        Me.dgvList.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblGSTDetail)
        Me.GroupBox2.Controls.Add(Me.lblGST3)
        Me.GroupBox2.Controls.Add(Me.lblGST28)
        Me.GroupBox2.Controls.Add(Me.lblGST18)
        Me.GroupBox2.Controls.Add(Me.lblGST12)
        Me.GroupBox2.Controls.Add(Me.lblGST5)
        Me.GroupBox2.Controls.Add(Me.lblGST0)
        Me.GroupBox2.Controls.Add(Me.lblGSTHeader)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.Panel13)
        Me.GroupBox2.Controls.Add(Me.lblCessAmount)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.lblDebit)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblVatAmount)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.lblAmount)
        Me.GroupBox2.Controls.Add(Me.lnkCalculateDebitAmt)
        Me.GroupBox2.Controls.Add(Me.FLXGRD_MaterialItem)
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(6, 164)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(890, 427)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Items List"
        '
        'lblCessAmount
        '
        Me.lblCessAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCessAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblCessAmount.Location = New System.Drawing.Point(774, 365)
        Me.lblCessAmount.Name = "lblCessAmount"
        Me.lblCessAmount.Size = New System.Drawing.Size(112, 17)
        Me.lblCessAmount.TabIndex = 18
        Me.lblCessAmount.Text = "0.00"
        Me.lblCessAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(686, 367)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 15)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Cess  Amount :"
        '
        'lblDebit
        '
        Me.lblDebit.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDebit.ForeColor = System.Drawing.Color.Lime
        Me.lblDebit.Location = New System.Drawing.Point(773, 392)
        Me.lblDebit.Name = "lblDebit"
        Me.lblDebit.Size = New System.Drawing.Size(112, 17)
        Me.lblDebit.TabIndex = 7
        Me.lblDebit.Text = "0.00"
        Me.lblDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(686, 394)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Debit Amount :"
        '
        'lblVatAmount
        '
        Me.lblVatAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVatAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblVatAmount.Location = New System.Drawing.Point(775, 301)
        Me.lblVatAmount.Name = "lblVatAmount"
        Me.lblVatAmount.Size = New System.Drawing.Size(112, 17)
        Me.lblVatAmount.TabIndex = 5
        Me.lblVatAmount.Text = "0.00"
        Me.lblVatAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(686, 303)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "GST  Amount :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(686, 277)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Total Amount :"
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblAmount.Location = New System.Drawing.Point(775, 275)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(112, 17)
        Me.lblAmount.TabIndex = 3
        Me.lblAmount.Text = "0.00"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lnkCalculateDebitAmt
        '
        Me.lnkCalculateDebitAmt.ActiveLinkColor = System.Drawing.Color.Lime
        Me.lnkCalculateDebitAmt.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkCalculateDebitAmt.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCalculateDebitAmt.LinkColor = System.Drawing.Color.OrangeRed
        Me.lnkCalculateDebitAmt.Location = New System.Drawing.Point(243, 277)
        Me.lnkCalculateDebitAmt.Name = "lnkCalculateDebitAmt"
        Me.lnkCalculateDebitAmt.Size = New System.Drawing.Size(200, 25)
        Me.lnkCalculateDebitAmt.TabIndex = 8
        Me.lnkCalculateDebitAmt.TabStop = True
        Me.lnkCalculateDebitAmt.Text = "Calculate All"
        Me.lnkCalculateDebitAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLXGRD_MaterialItem
        '
        Me.FLXGRD_MaterialItem.BackColor = System.Drawing.Color.Silver
        Me.FLXGRD_MaterialItem.ColumnInfo = "12,1,0,0,0,85,Columns:"
        Me.FLXGRD_MaterialItem.Location = New System.Drawing.Point(5, 19)
        Me.FLXGRD_MaterialItem.Name = "FLXGRD_MaterialItem"
        Me.FLXGRD_MaterialItem.Rows.Count = 1
        Me.FLXGRD_MaterialItem.Rows.DefaultSize = 17
        Me.FLXGRD_MaterialItem.Size = New System.Drawing.Size(879, 249)
        Me.FLXGRD_MaterialItem.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLXGRD_MaterialItem.Styles"))
        Me.FLXGRD_MaterialItem.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_INVDate)
        Me.GroupBox1.Controls.Add(Me.txt_INVNo)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbsupplier)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lbl_DNDate)
        Me.GroupBox1.Controls.Add(Me.lblDN_Code)
        Me.GroupBox1.Controls.Add(Me.lblReverseCode)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.lbl_Remarks)
        Me.GroupBox1.Controls.Add(Me.lblReceivedDate)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.cmbMRNNo)
        Me.GroupBox1.Controls.Add(Me.lblSelectMRNNO)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 152)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Debit Note Detail"
        '
        'txt_INVDate
        '
        Me.txt_INVDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_INVDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_INVDate.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_INVDate.ForeColor = System.Drawing.Color.White
        Me.txt_INVDate.Location = New System.Drawing.Point(580, 73)
        Me.txt_INVDate.MaxLength = 0
        Me.txt_INVDate.Name = "txt_INVDate"
        Me.txt_INVDate.ReadOnly = True
        Me.txt_INVDate.Size = New System.Drawing.Size(137, 19)
        Me.txt_INVDate.TabIndex = 4
        '
        'txt_INVNo
        '
        Me.txt_INVNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_INVNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_INVNo.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_INVNo.ForeColor = System.Drawing.Color.White
        Me.txt_INVNo.Location = New System.Drawing.Point(400, 74)
        Me.txt_INVNo.MaxLength = 0
        Me.txt_INVNo.Name = "txt_INVNo"
        Me.txt_INVNo.ReadOnly = True
        Me.txt_INVNo.Size = New System.Drawing.Size(110, 19)
        Me.txt_INVNo.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(343, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 15)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "INV No :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(516, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 15)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "INV Date:"
        '
        'cmbsupplier
        '
        Me.cmbsupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbsupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbsupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbsupplier.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsupplier.ForeColor = System.Drawing.Color.White
        Me.cmbsupplier.FormattingEnabled = True
        Me.cmbsupplier.Location = New System.Drawing.Point(145, 40)
        Me.cmbsupplier.Name = "cmbsupplier"
        Me.cmbsupplier.Size = New System.Drawing.Size(572, 23)
        Me.cmbsupplier.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 15)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Select Supplier :"
        '
        'lbl_DNDate
        '
        Me.lbl_DNDate.AutoSize = True
        Me.lbl_DNDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DNDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbl_DNDate.Location = New System.Drawing.Point(298, 17)
        Me.lbl_DNDate.Name = "lbl_DNDate"
        Me.lbl_DNDate.Size = New System.Drawing.Size(84, 13)
        Me.lbl_DNDate.TabIndex = 26
        Me.lbl_DNDate.Text = "Debit Note Date"
        '
        'lblDN_Code
        '
        Me.lblDN_Code.AutoSize = True
        Me.lblDN_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDN_Code.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDN_Code.Location = New System.Drawing.Point(82, 18)
        Me.lblDN_Code.Name = "lblDN_Code"
        Me.lblDN_Code.Size = New System.Drawing.Size(86, 13)
        Me.lblDN_Code.TabIndex = 16
        Me.lblDN_Code.Text = "Debit Note Code"
        '
        'lblReverseCode
        '
        Me.lblReverseCode.AutoSize = True
        Me.lblReverseCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReverseCode.Location = New System.Drawing.Point(28, 16)
        Me.lblReverseCode.Name = "lblReverseCode"
        Me.lblReverseCode.Size = New System.Drawing.Size(50, 15)
        Me.lblReverseCode.TabIndex = 15
        Me.lblReverseCode.Text = "DN No :"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.White
        Me.txtRemarks.Location = New System.Drawing.Point(145, 100)
        Me.txtRemarks.MaxLength = 500
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(572, 46)
        Me.txtRemarks.TabIndex = 5
        '
        'lbl_Remarks
        '
        Me.lbl_Remarks.AutoSize = True
        Me.lbl_Remarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Remarks.Location = New System.Drawing.Point(28, 102)
        Me.lbl_Remarks.Name = "lbl_Remarks"
        Me.lbl_Remarks.Size = New System.Drawing.Size(64, 15)
        Me.lbl_Remarks.TabIndex = 13
        Me.lbl_Remarks.Text = "Remarks :"
        '
        'lblReceivedDate
        '
        Me.lblReceivedDate.AutoSize = True
        Me.lblReceivedDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedDate.Location = New System.Drawing.Point(207, 15)
        Me.lblReceivedDate.Name = "lblReceivedDate"
        Me.lblReceivedDate.Size = New System.Drawing.Size(63, 15)
        Me.lblReceivedDate.TabIndex = 11
        Me.lblReceivedDate.Text = "DN  Date :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(756, 14)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(132, 25)
        Me.lblFormHeading.TabIndex = 10
        Me.lblFormHeading.Text = "Debit Note"
        '
        'cmbMRNNo
        '
        Me.cmbMRNNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbMRNNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMRNNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMRNNo.ForeColor = System.Drawing.Color.White
        Me.cmbMRNNo.FormattingEnabled = True
        Me.cmbMRNNo.Location = New System.Drawing.Point(145, 71)
        Me.cmbMRNNo.Name = "cmbMRNNo"
        Me.cmbMRNNo.Size = New System.Drawing.Size(181, 23)
        Me.cmbMRNNo.TabIndex = 2
        '
        'lblSelectMRNNO
        '
        Me.lblSelectMRNNO.AutoSize = True
        Me.lblSelectMRNNO.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectMRNNO.Location = New System.Drawing.Point(28, 74)
        Me.lblSelectMRNNO.Name = "lblSelectMRNNO"
        Me.lblSelectMRNNO.Size = New System.Drawing.Size(96, 15)
        Me.lblSelectMRNNO.TabIndex = 9
        Me.lblSelectMRNNO.Text = "Select MRN No :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.C1FlexGrid1.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.DefaultSize = 17
        Me.C1FlexGrid1.Size = New System.Drawing.Size(0, 0)
        Me.C1FlexGrid1.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("C1FlexGrid1.Styles"))
        Me.C1FlexGrid1.TabIndex = 0
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.Silver
        Me.Panel13.Location = New System.Drawing.Point(673, 274)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(1, 142)
        Me.Panel13.TabIndex = 268
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.Location = New System.Drawing.Point(450, 274)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1, 142)
        Me.Panel1.TabIndex = 269
        '
        'lblGST3
        '
        Me.lblGST3.AutoSize = True
        Me.lblGST3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST3.ForeColor = System.Drawing.Color.White
        Me.lblGST3.Location = New System.Drawing.Point(460, 317)
        Me.lblGST3.Name = "lblGST3"
        Me.lblGST3.Size = New System.Drawing.Size(107, 14)
        Me.lblGST3.TabIndex = 276
        Me.lblGST3.Text = "3% - Amt @ Tax"
        '
        'lblGST28
        '
        Me.lblGST28.AutoSize = True
        Me.lblGST28.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST28.ForeColor = System.Drawing.Color.White
        Me.lblGST28.Location = New System.Drawing.Point(459, 392)
        Me.lblGST28.Name = "lblGST28"
        Me.lblGST28.Size = New System.Drawing.Size(115, 14)
        Me.lblGST28.TabIndex = 275
        Me.lblGST28.Text = "28% - Amt @ Tax"
        '
        'lblGST18
        '
        Me.lblGST18.AutoSize = True
        Me.lblGST18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST18.ForeColor = System.Drawing.Color.White
        Me.lblGST18.Location = New System.Drawing.Point(459, 373)
        Me.lblGST18.Name = "lblGST18"
        Me.lblGST18.Size = New System.Drawing.Size(115, 14)
        Me.lblGST18.TabIndex = 274
        Me.lblGST18.Text = "18% - Amt @ Tax"
        '
        'lblGST12
        '
        Me.lblGST12.AutoSize = True
        Me.lblGST12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST12.ForeColor = System.Drawing.Color.White
        Me.lblGST12.Location = New System.Drawing.Point(459, 353)
        Me.lblGST12.Name = "lblGST12"
        Me.lblGST12.Size = New System.Drawing.Size(115, 14)
        Me.lblGST12.TabIndex = 273
        Me.lblGST12.Text = "12% - Amt @ Tax"
        '
        'lblGST5
        '
        Me.lblGST5.AutoSize = True
        Me.lblGST5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST5.ForeColor = System.Drawing.Color.White
        Me.lblGST5.Location = New System.Drawing.Point(460, 335)
        Me.lblGST5.Name = "lblGST5"
        Me.lblGST5.Size = New System.Drawing.Size(107, 14)
        Me.lblGST5.TabIndex = 272
        Me.lblGST5.Text = "5% - Amt @ Tax"
        '
        'lblGST0
        '
        Me.lblGST0.AutoSize = True
        Me.lblGST0.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST0.ForeColor = System.Drawing.Color.White
        Me.lblGST0.Location = New System.Drawing.Point(460, 299)
        Me.lblGST0.Name = "lblGST0"
        Me.lblGST0.Size = New System.Drawing.Size(107, 14)
        Me.lblGST0.TabIndex = 271
        Me.lblGST0.Text = "0% - Amt @ Tax"
        '
        'lblGSTHeader
        '
        Me.lblGSTHeader.AutoSize = True
        Me.lblGSTHeader.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTHeader.ForeColor = System.Drawing.Color.White
        Me.lblGSTHeader.Location = New System.Drawing.Point(459, 279)
        Me.lblGSTHeader.Name = "lblGSTHeader"
        Me.lblGSTHeader.Size = New System.Drawing.Size(116, 14)
        Me.lblGSTHeader.TabIndex = 270
        Me.lblGSTHeader.Text = "GST Summary :-"
        '
        'lblGSTDetail
        '
        Me.lblGSTDetail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTDetail.ForeColor = System.Drawing.Color.Orange
        Me.lblGSTDetail.Location = New System.Drawing.Point(704, 323)
        Me.lblGSTDetail.Name = "lblGSTDetail"
        Me.lblGSTDetail.Size = New System.Drawing.Size(182, 37)
        Me.lblGSTDetail.TabIndex = 277
        Me.lblGSTDetail.Text = "UTGST - 0.00" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SGST - 0.00"
        Me.lblGSTDetail.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frm_DebitNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TbRMRN)
        Me.Name = "frm_DebitNote"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TbRMRN.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GBRMWPM.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.FLXGRD_MaterialItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TbRMRN As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbMRNNo As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectMRNNO As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents lbl_Remarks As System.Windows.Forms.Label
    Friend WithEvents lblReceivedDate As System.Windows.Forms.Label
    Friend WithEvents lblDN_Code As System.Windows.Forms.Label
    Friend WithEvents lblReverseCode As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GBRMWPM As System.Windows.Forms.GroupBox
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lbl_DNDate As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents FLXGRD_MaterialItem As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lnkCalculateDebitAmt As System.Windows.Forms.LinkLabel
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDebit As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblVatAmount As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbsupplier As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_INVDate As System.Windows.Forms.TextBox
    Friend WithEvents txt_INVNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCessAmount As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents lblGST3 As Label
    Friend WithEvents lblGST28 As Label
    Friend WithEvents lblGST18 As Label
    Friend WithEvents lblGST12 As Label
    Friend WithEvents lblGST5 As Label
    Friend WithEvents lblGST0 As Label
    Friend WithEvents lblGSTHeader As Label
    Friend WithEvents lblGSTDetail As Label
End Class
