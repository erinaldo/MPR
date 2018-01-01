<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_openSale_Invoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_openSale_Invoice))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.BtnCancelInv = New System.Windows.Forms.Button()
        Me.BtnInvoice = New System.Windows.Forms.Button()
        Me.BtnDc = New System.Windows.Forms.Button()
        Me.GBMRSDetail = New System.Windows.Forms.GroupBox()
        Me.flxList = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GBItemInfo = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotalDisc = New System.Windows.Forms.Label()
        Me.lnkCalculatePOAmt = New System.Windows.Forms.LinkLabel()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblVatAmount = New System.Windows.Forms.Label()
        Me.lblItemValue = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.flxItems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GBDCMASTER = New System.Windows.Forms.GroupBox()
        Me.cmbCity = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbSupplier = New System.Windows.Forms.ComboBox()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.txtLRNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTransport = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtGstNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rbtn_Cash = New System.Windows.Forms.RadioButton()
        Me.rdbtn_credit = New System.Windows.Forms.RadioButton()
        Me.txtcustomer_name = New System.Windows.Forms.TextBox()
        Me.cmbinvtype = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtvechicle_no = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_txtphoneNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCap2 = New System.Windows.Forms.Label()
        Me.lblCap1 = New System.Windows.Forms.Label()
        Me.lbl_TransferDate = New System.Windows.Forms.Label()
        Me.lbl_INVNo = New System.Windows.Forms.Label()
        Me.lblMRSDate = New System.Windows.Forms.Label()
        Me.lblMRSCode = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GBMRSDetail.SuspendLayout()
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GBItemInfo.SuspendLayout()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TabControl1.TabIndex = 1
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.BtnCancelInv)
        Me.List.Controls.Add(Me.BtnInvoice)
        Me.List.Controls.Add(Me.BtnDc)
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
        Me.BtnCancelInv.BackColor = System.Drawing.Color.LightSalmon
        Me.BtnCancelInv.Location = New System.Drawing.Point(455, 538)
        Me.BtnCancelInv.Name = "BtnCancelInv"
        Me.BtnCancelInv.Size = New System.Drawing.Size(141, 30)
        Me.BtnCancelInv.TabIndex = 9
        Me.BtnCancelInv.Text = "Cancel Invoice"
        Me.BtnCancelInv.UseVisualStyleBackColor = False
        '
        'BtnInvoice
        '
        Me.BtnInvoice.Location = New System.Drawing.Point(615, 538)
        Me.BtnInvoice.Name = "BtnInvoice"
        Me.BtnInvoice.Size = New System.Drawing.Size(128, 30)
        Me.BtnInvoice.TabIndex = 7
        Me.BtnInvoice.Text = "Print Invoice"
        Me.BtnInvoice.UseVisualStyleBackColor = True
        '
        'BtnDc
        '
        Me.BtnDc.Location = New System.Drawing.Point(749, 538)
        Me.BtnDc.Name = "BtnDc"
        Me.BtnDc.Size = New System.Drawing.Size(128, 30)
        Me.BtnDc.TabIndex = 8
        Me.BtnDc.Text = "Print Dc"
        Me.BtnDc.UseVisualStyleBackColor = True
        '
        'GBMRSDetail
        '
        Me.GBMRSDetail.Controls.Add(Me.flxList)
        Me.GBMRSDetail.Location = New System.Drawing.Point(19, 91)
        Me.GBMRSDetail.Name = "GBMRSDetail"
        Me.GBMRSDetail.Size = New System.Drawing.Size(864, 441)
        Me.GBMRSDetail.TabIndex = 6
        Me.GBMRSDetail.TabStop = False
        '
        'flxList
        '
        Me.flxList.AllowUserToAddRows = False
        Me.flxList.AllowUserToDeleteRows = False
        Me.flxList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxList.Location = New System.Drawing.Point(3, 16)
        Me.flxList.Name = "flxList"
        Me.flxList.RowHeadersVisible = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.flxList.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.flxList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxList.Size = New System.Drawing.Size(858, 422)
        Me.flxList.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSearch)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(864, 76)
        Me.GroupBox2.TabIndex = 7
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
        Me.txtSearch.TabIndex = 17
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
        Me.TabPage2.Controls.Add(Me.GBItemInfo)
        Me.TabPage2.Controls.Add(Me.GBDCMASTER)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GBItemInfo
        '
        Me.GBItemInfo.Controls.Add(Me.Label10)
        Me.GBItemInfo.Controls.Add(Me.lblTotalDisc)
        Me.GBItemInfo.Controls.Add(Me.lnkCalculatePOAmt)
        Me.GBItemInfo.Controls.Add(Me.lblNetAmount)
        Me.GBItemInfo.Controls.Add(Me.Label5)
        Me.GBItemInfo.Controls.Add(Me.lblVatAmount)
        Me.GBItemInfo.Controls.Add(Me.lblItemValue)
        Me.GBItemInfo.Controls.Add(Me.Label6)
        Me.GBItemInfo.Controls.Add(Me.Label7)
        Me.GBItemInfo.Controls.Add(Me.flxItems)
        Me.GBItemInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBItemInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBItemInfo.ForeColor = System.Drawing.Color.White
        Me.GBItemInfo.Location = New System.Drawing.Point(3, 171)
        Me.GBItemInfo.Name = "GBItemInfo"
        Me.GBItemInfo.Size = New System.Drawing.Size(896, 426)
        Me.GBItemInfo.TabIndex = 8
        Me.GBItemInfo.TabStop = False
        Me.GBItemInfo.Text = "List of Items"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(250, 393)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 15)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Total Discount :"
        '
        'lblTotalDisc
        '
        Me.lblTotalDisc.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDisc.ForeColor = System.Drawing.Color.Orange
        Me.lblTotalDisc.Location = New System.Drawing.Point(347, 389)
        Me.lblTotalDisc.Name = "lblTotalDisc"
        Me.lblTotalDisc.Size = New System.Drawing.Size(94, 20)
        Me.lblTotalDisc.TabIndex = 0
        Me.lblTotalDisc.Text = "0.00"
        Me.lblTotalDisc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lnkCalculatePOAmt
        '
        Me.lnkCalculatePOAmt.AutoSize = True
        Me.lnkCalculatePOAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCalculatePOAmt.LinkColor = System.Drawing.Color.DarkOrange
        Me.lnkCalculatePOAmt.Location = New System.Drawing.Point(11, 362)
        Me.lnkCalculatePOAmt.Name = "lnkCalculatePOAmt"
        Me.lnkCalculatePOAmt.Size = New System.Drawing.Size(110, 15)
        Me.lnkCalculatePOAmt.TabIndex = 17
        Me.lnkCalculatePOAmt.TabStop = True
        Me.lnkCalculatePOAmt.Text = "Calculate  Amount"
        '
        'lblNetAmount
        '
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblNetAmount.Location = New System.Drawing.Point(759, 390)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.Size = New System.Drawing.Size(118, 20)
        Me.lblNetAmount.TabIndex = 0
        Me.lblNetAmount.Text = "0.00"
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(694, 394)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 15)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Net Amount :"
        '
        'lblVatAmount
        '
        Me.lblVatAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVatAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblVatAmount.Location = New System.Drawing.Point(557, 389)
        Me.lblVatAmount.Name = "lblVatAmount"
        Me.lblVatAmount.Size = New System.Drawing.Size(106, 20)
        Me.lblVatAmount.TabIndex = 0
        Me.lblVatAmount.Text = "0.00"
        Me.lblVatAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItemValue
        '
        Me.lblItemValue.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemValue.ForeColor = System.Drawing.Color.Orange
        Me.lblItemValue.Location = New System.Drawing.Point(109, 389)
        Me.lblItemValue.Name = "lblItemValue"
        Me.lblItemValue.Size = New System.Drawing.Size(107, 20)
        Me.lblItemValue.TabIndex = 0
        Me.lblItemValue.Text = "0.00"
        Me.lblItemValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(467, 393)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 15)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "GST  Amount :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 391)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 15)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Total Item Value :"
        '
        'flxItems
        '
        Me.flxItems.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.flxItems.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.flxItems.BackColor = System.Drawing.Color.Silver
        Me.flxItems.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:26;AllowSorting:False;AllowDragging:False;AllowResiz" & _
    "ing:False;AllowMerging:True;AllowEditing:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flxItems.Location = New System.Drawing.Point(4, 20)
        Me.flxItems.Name = "flxItems"
        Me.flxItems.Rows.Count = 2
        Me.flxItems.Rows.DefaultSize = 18
        Me.flxItems.Size = New System.Drawing.Size(890, 337)
        Me.flxItems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxItems.Styles"))
        Me.flxItems.TabIndex = 16
        '
        'GBDCMASTER
        '
        Me.GBDCMASTER.Controls.Add(Me.cmbCity)
        Me.GBDCMASTER.Controls.Add(Me.Label13)
        Me.GBDCMASTER.Controls.Add(Me.cmbSupplier)
        Me.GBDCMASTER.Controls.Add(Me.btnAddNew)
        Me.GBDCMASTER.Controls.Add(Me.txtLRNO)
        Me.GBDCMASTER.Controls.Add(Me.Label12)
        Me.GBDCMASTER.Controls.Add(Me.txtTransport)
        Me.GBDCMASTER.Controls.Add(Me.Label11)
        Me.GBDCMASTER.Controls.Add(Me.txtGstNo)
        Me.GBDCMASTER.Controls.Add(Me.Label9)
        Me.GBDCMASTER.Controls.Add(Me.txtAddress)
        Me.GBDCMASTER.Controls.Add(Me.Label8)
        Me.GBDCMASTER.Controls.Add(Me.rbtn_Cash)
        Me.GBDCMASTER.Controls.Add(Me.rdbtn_credit)
        Me.GBDCMASTER.Controls.Add(Me.txtcustomer_name)
        Me.GBDCMASTER.Controls.Add(Me.cmbinvtype)
        Me.GBDCMASTER.Controls.Add(Me.Label4)
        Me.GBDCMASTER.Controls.Add(Me.txtvechicle_no)
        Me.GBDCMASTER.Controls.Add(Me.Label2)
        Me.GBDCMASTER.Controls.Add(Me.txt_txtphoneNo)
        Me.GBDCMASTER.Controls.Add(Me.Label3)
        Me.GBDCMASTER.Controls.Add(Me.lblCap2)
        Me.GBDCMASTER.Controls.Add(Me.lblCap1)
        Me.GBDCMASTER.Controls.Add(Me.lbl_TransferDate)
        Me.GBDCMASTER.Controls.Add(Me.lbl_INVNo)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSDate)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSCode)
        Me.GBDCMASTER.Controls.Add(Me.lblFormHeading)
        Me.GBDCMASTER.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBDCMASTER.Location = New System.Drawing.Point(3, 3)
        Me.GBDCMASTER.Name = "GBDCMASTER"
        Me.GBDCMASTER.Size = New System.Drawing.Size(896, 162)
        Me.GBDCMASTER.TabIndex = 0
        Me.GBDCMASTER.TabStop = False
        '
        'cmbCity
        '
        Me.cmbCity.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCity.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCity.ForeColor = System.Drawing.Color.White
        Me.cmbCity.FormattingEnabled = True
        Me.cmbCity.Items.AddRange(New Object() {"---Select---", "SCGST", "IGST", "UGST"})
        Me.cmbCity.Location = New System.Drawing.Point(752, 97)
        Me.cmbCity.Name = "cmbCity"
        Me.cmbCity.Size = New System.Drawing.Size(136, 23)
        Me.cmbCity.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(691, 103)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(33, 15)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "City :"
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.ForeColor = System.Drawing.Color.White
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(89, 39)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(596, 23)
        Me.cmbSupplier.TabIndex = 6
        '
        'btnAddNew
        '
        Me.btnAddNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Location = New System.Drawing.Point(695, 39)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 23)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.UseVisualStyleBackColor = False
        '
        'txtLRNO
        '
        Me.txtLRNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLRNO.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLRNO.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLRNO.ForeColor = System.Drawing.Color.White
        Me.txtLRNO.Location = New System.Drawing.Point(514, 129)
        Me.txtLRNO.MaxLength = 0
        Me.txtLRNO.Name = "txtLRNO"
        Me.txtLRNO.Size = New System.Drawing.Size(171, 19)
        Me.txtLRNO.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(439, 131)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 15)
        Me.Label12.TabIndex = 55
        Me.Label12.Text = "LR. NO. :"
        '
        'txtTransport
        '
        Me.txtTransport.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtTransport.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTransport.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransport.ForeColor = System.Drawing.Color.White
        Me.txtTransport.Location = New System.Drawing.Point(287, 130)
        Me.txtTransport.MaxLength = 0
        Me.txtTransport.Name = "txtTransport"
        Me.txtTransport.Size = New System.Drawing.Size(146, 19)
        Me.txtTransport.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(218, 131)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 15)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Transport :"
        '
        'txtGstNo
        '
        Me.txtGstNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtGstNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGstNo.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGstNo.ForeColor = System.Drawing.Color.White
        Me.txtGstNo.Location = New System.Drawing.Point(515, 101)
        Me.txtGstNo.MaxLength = 0
        Me.txtGstNo.Name = "txtGstNo"
        Me.txtGstNo.Size = New System.Drawing.Size(170, 19)
        Me.txtGstNo.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(439, 102)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 15)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "GST NO :"
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddress.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.White
        Me.txtAddress.Location = New System.Drawing.Point(90, 69)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(343, 51)
        Me.txtAddress.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(439, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 15)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Payment Mode :"
        '
        'rbtn_Cash
        '
        Me.rbtn_Cash.AutoSize = True
        Me.rbtn_Cash.Location = New System.Drawing.Point(541, 16)
        Me.rbtn_Cash.Name = "rbtn_Cash"
        Me.rbtn_Cash.Size = New System.Drawing.Size(54, 17)
        Me.rbtn_Cash.TabIndex = 4
        Me.rbtn_Cash.Text = "CASH"
        Me.rbtn_Cash.UseVisualStyleBackColor = True
        '
        'rdbtn_credit
        '
        Me.rdbtn_credit.AutoSize = True
        Me.rdbtn_credit.Checked = True
        Me.rdbtn_credit.Location = New System.Drawing.Point(626, 15)
        Me.rdbtn_credit.Name = "rdbtn_credit"
        Me.rdbtn_credit.Size = New System.Drawing.Size(65, 17)
        Me.rdbtn_credit.TabIndex = 5
        Me.rdbtn_credit.TabStop = True
        Me.rdbtn_credit.Text = "CREDIT"
        Me.rdbtn_credit.UseVisualStyleBackColor = True
        '
        'txtcustomer_name
        '
        Me.txtcustomer_name.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtcustomer_name.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtcustomer_name.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer_name.ForeColor = System.Drawing.Color.White
        Me.txtcustomer_name.Location = New System.Drawing.Point(90, 42)
        Me.txtcustomer_name.MaxLength = 0
        Me.txtcustomer_name.Name = "txtcustomer_name"
        Me.txtcustomer_name.Size = New System.Drawing.Size(595, 19)
        Me.txtcustomer_name.TabIndex = 7
        '
        'cmbinvtype
        '
        Me.cmbinvtype.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbinvtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbinvtype.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbinvtype.ForeColor = System.Drawing.Color.White
        Me.cmbinvtype.FormattingEnabled = True
        Me.cmbinvtype.Items.AddRange(New Object() {"---Select---", "SGST", "IGST", "UGST"})
        Me.cmbinvtype.Location = New System.Drawing.Point(752, 127)
        Me.cmbinvtype.Name = "cmbinvtype"
        Me.cmbinvtype.Size = New System.Drawing.Size(136, 23)
        Me.cmbinvtype.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(692, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 15)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "INV Type :"
        '
        'txtvechicle_no
        '
        Me.txtvechicle_no.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtvechicle_no.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtvechicle_no.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvechicle_no.ForeColor = System.Drawing.Color.White
        Me.txtvechicle_no.Location = New System.Drawing.Point(90, 129)
        Me.txtvechicle_no.MaxLength = 0
        Me.txtvechicle_no.Name = "txtvechicle_no"
        Me.txtvechicle_no.Size = New System.Drawing.Size(126, 19)
        Me.txtvechicle_no.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 15)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Vehicle NO :"
        '
        'txt_txtphoneNo
        '
        Me.txt_txtphoneNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_txtphoneNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_txtphoneNo.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_txtphoneNo.ForeColor = System.Drawing.Color.White
        Me.txt_txtphoneNo.Location = New System.Drawing.Point(515, 72)
        Me.txt_txtphoneNo.MaxLength = 0
        Me.txt_txtphoneNo.Name = "txt_txtphoneNo"
        Me.txt_txtphoneNo.Size = New System.Drawing.Size(170, 19)
        Me.txt_txtphoneNo.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(439, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 15)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Phone NO :"
        '
        'lblCap2
        '
        Me.lblCap2.AutoSize = True
        Me.lblCap2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap2.Location = New System.Drawing.Point(11, 70)
        Me.lblCap2.Name = "lblCap2"
        Me.lblCap2.Size = New System.Drawing.Size(59, 15)
        Me.lblCap2.TabIndex = 12
        Me.lblCap2.Text = "Address :"
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(11, 43)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.Size = New System.Drawing.Size(68, 15)
        Me.lblCap1.TabIndex = 10
        Me.lblCap1.Text = "Customer :"
        '
        'lbl_TransferDate
        '
        Me.lbl_TransferDate.AutoSize = True
        Me.lbl_TransferDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TransferDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_TransferDate.Location = New System.Drawing.Point(305, 16)
        Me.lbl_TransferDate.Name = "lbl_TransferDate"
        Me.lbl_TransferDate.Size = New System.Drawing.Size(74, 15)
        Me.lbl_TransferDate.TabIndex = 9
        Me.lbl_TransferDate.Text = "Invoice Date"
        '
        'lbl_INVNo
        '
        Me.lbl_INVNo.AutoSize = True
        Me.lbl_INVNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_INVNo.ForeColor = System.Drawing.Color.Orange
        Me.lbl_INVNo.Location = New System.Drawing.Point(87, 16)
        Me.lbl_INVNo.Name = "lbl_INVNo"
        Me.lbl_INVNo.Size = New System.Drawing.Size(45, 15)
        Me.lbl_INVNo.TabIndex = 3
        Me.lbl_INVNo.Text = "INV No"
        '
        'lblMRSDate
        '
        Me.lblMRSDate.AutoSize = True
        Me.lblMRSDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSDate.Location = New System.Drawing.Point(205, 16)
        Me.lblMRSDate.Name = "lblMRSDate"
        Me.lblMRSDate.Size = New System.Drawing.Size(80, 15)
        Me.lblMRSDate.TabIndex = 7
        Me.lblMRSDate.Text = "Invoice Date :"
        '
        'lblMRSCode
        '
        Me.lblMRSCode.AutoSize = True
        Me.lblMRSCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSCode.Location = New System.Drawing.Point(12, 16)
        Me.lblMRSCode.Name = "lblMRSCode"
        Me.lblMRSCode.Size = New System.Drawing.Size(53, 15)
        Me.lblMRSCode.TabIndex = 5
        Me.lblMRSCode.Text = "INV NO :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(790, 20)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(104, 65)
        Me.lblFormHeading.TabIndex = 4
        Me.lblFormHeading.Text = "Sale Invoice"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_openSale_Invoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_openSale_Invoice"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GBMRSDetail.ResumeLayout(False)
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GBItemInfo.ResumeLayout(False)
        Me.GBItemInfo.PerformLayout()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBDCMASTER.ResumeLayout(False)
        Me.GBDCMASTER.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GBDCMASTER As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents GBMRSDetail As System.Windows.Forms.GroupBox
    Friend WithEvents lblMRSCode As System.Windows.Forms.Label
    Friend WithEvents lblMRSDate As System.Windows.Forms.Label
    Friend WithEvents lbl_INVNo As System.Windows.Forms.Label
    Friend WithEvents lbl_TransferDate As System.Windows.Forms.Label
    Friend WithEvents GBItemInfo As System.Windows.Forms.GroupBox
    Friend WithEvents flxList As DataGridView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents lblCap1 As System.Windows.Forms.Label
    Friend WithEvents lblCap2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtvechicle_no As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_txtphoneNo As System.Windows.Forms.TextBox
    Friend WithEvents flxItems As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblNetAmount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblVatAmount As System.Windows.Forms.Label
    Friend WithEvents lblItemValue As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lnkCalculatePOAmt As System.Windows.Forms.LinkLabel
    Friend WithEvents cmbinvtype As System.Windows.Forms.ComboBox
    Friend WithEvents BtnInvoice As System.Windows.Forms.Button
    Friend WithEvents BtnDc As System.Windows.Forms.Button
    Friend WithEvents BtnCancelInv As System.Windows.Forms.Button
    Friend WithEvents txtcustomer_name As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rbtn_Cash As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_credit As System.Windows.Forms.RadioButton
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtGstNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblTotalDisc As Label
    Friend WithEvents txtLRNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtTransport As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btnAddNew As Button
    Friend WithEvents cmbSupplier As ComboBox
    Friend WithEvents cmbCity As ComboBox
    Friend WithEvents Label13 As Label
End Class
