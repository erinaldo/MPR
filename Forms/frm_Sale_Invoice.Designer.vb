<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Sale_Invoice
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Sale_Invoice))
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
        Me.lnkCalculatePOAmt = New System.Windows.Forms.LinkLabel()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblVatAmount = New System.Windows.Forms.Label()
        Me.lblItemValue = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.flxItems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GBDCMASTER = New System.Windows.Forms.GroupBox()
        Me.cmbinvtype = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtvechicle_no = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_LRNO = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblCap2 = New System.Windows.Forms.Label()
        Me.cmbSupplier = New System.Windows.Forms.ComboBox()
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
        Me.GBItemInfo.Location = New System.Drawing.Point(3, 161)
        Me.GBItemInfo.Name = "GBItemInfo"
        Me.GBItemInfo.Size = New System.Drawing.Size(896, 436)
        Me.GBItemInfo.TabIndex = 8
        Me.GBItemInfo.TabStop = False
        Me.GBItemInfo.Text = "List of Items"
        '
        'lnkCalculatePOAmt
        '
        Me.lnkCalculatePOAmt.AutoSize = True
        Me.lnkCalculatePOAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCalculatePOAmt.LinkColor = System.Drawing.Color.DarkOrange
        Me.lnkCalculatePOAmt.Location = New System.Drawing.Point(747, 369)
        Me.lnkCalculatePOAmt.Name = "lnkCalculatePOAmt"
        Me.lnkCalculatePOAmt.Size = New System.Drawing.Size(110, 15)
        Me.lnkCalculatePOAmt.TabIndex = 9
        Me.lnkCalculatePOAmt.TabStop = True
        Me.lnkCalculatePOAmt.Text = "Calculate  Amount"
        '
        'lblNetAmount
        '
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblNetAmount.Location = New System.Drawing.Point(721, 397)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.Size = New System.Drawing.Size(118, 20)
        Me.lblNetAmount.TabIndex = 3
        Me.lblNetAmount.Text = "0.00"
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(609, 401)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Net Amount :"
        '
        'lblVatAmount
        '
        Me.lblVatAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVatAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblVatAmount.Location = New System.Drawing.Point(446, 396)
        Me.lblVatAmount.Name = "lblVatAmount"
        Me.lblVatAmount.Size = New System.Drawing.Size(118, 20)
        Me.lblVatAmount.TabIndex = 5
        Me.lblVatAmount.Text = "0.00"
        Me.lblVatAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItemValue
        '
        Me.lblItemValue.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemValue.ForeColor = System.Drawing.Color.Orange
        Me.lblItemValue.Location = New System.Drawing.Point(142, 397)
        Me.lblItemValue.Name = "lblItemValue"
        Me.lblItemValue.Size = New System.Drawing.Size(118, 20)
        Me.lblItemValue.TabIndex = 6
        Me.lblItemValue.Text = "0.00"
        Me.lblItemValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(328, 401)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 15)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "GST  Amount :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 402)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 15)
        Me.Label7.TabIndex = 8
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
        Me.flxItems.Location = New System.Drawing.Point(2, 20)
        Me.flxItems.Name = "flxItems"
        Me.flxItems.Rows.Count = 2
        Me.flxItems.Rows.DefaultSize = 18
        Me.flxItems.Size = New System.Drawing.Size(890, 345)
        Me.flxItems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxItems.Styles"))
        Me.flxItems.TabIndex = 2
        '
        'GBDCMASTER
        '
        Me.GBDCMASTER.Controls.Add(Me.cmbinvtype)
        Me.GBDCMASTER.Controls.Add(Me.Label4)
        Me.GBDCMASTER.Controls.Add(Me.txtvechicle_no)
        Me.GBDCMASTER.Controls.Add(Me.Label2)
        Me.GBDCMASTER.Controls.Add(Me.txt_LRNO)
        Me.GBDCMASTER.Controls.Add(Me.Label3)
        Me.GBDCMASTER.Controls.Add(Me.lblAddress)
        Me.GBDCMASTER.Controls.Add(Me.lblCap2)
        Me.GBDCMASTER.Controls.Add(Me.cmbSupplier)
        Me.GBDCMASTER.Controls.Add(Me.lblCap1)
        Me.GBDCMASTER.Controls.Add(Me.lbl_TransferDate)
        Me.GBDCMASTER.Controls.Add(Me.lbl_INVNo)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSDate)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSCode)
        Me.GBDCMASTER.Controls.Add(Me.lblFormHeading)
        Me.GBDCMASTER.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBDCMASTER.Location = New System.Drawing.Point(3, 3)
        Me.GBDCMASTER.Name = "GBDCMASTER"
        Me.GBDCMASTER.Size = New System.Drawing.Size(896, 152)
        Me.GBDCMASTER.TabIndex = 0
        Me.GBDCMASTER.TabStop = False
        '
        'cmbinvtype
        '
        Me.cmbinvtype.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbinvtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbinvtype.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbinvtype.ForeColor = System.Drawing.Color.White
        Me.cmbinvtype.FormattingEnabled = True
        Me.cmbinvtype.Items.AddRange(New Object() {"---Select---", "SCGST", "IGST", "UGST"})
        Me.cmbinvtype.Location = New System.Drawing.Point(517, 95)
        Me.cmbinvtype.Name = "cmbinvtype"
        Me.cmbinvtype.Size = New System.Drawing.Size(180, 23)
        Me.cmbinvtype.TabIndex = 47
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(431, 99)
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
        Me.txtvechicle_no.Location = New System.Drawing.Point(517, 57)
        Me.txtvechicle_no.MaxLength = 0
        Me.txtvechicle_no.Name = "txtvechicle_no"
        Me.txtvechicle_no.Size = New System.Drawing.Size(180, 19)
        Me.txtvechicle_no.TabIndex = 45
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(431, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 15)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Vehicle NO :"
        '
        'txt_LRNO
        '
        Me.txt_LRNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_LRNO.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_LRNO.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LRNO.ForeColor = System.Drawing.Color.White
        Me.txt_LRNO.Location = New System.Drawing.Point(517, 22)
        Me.txt_LRNO.MaxLength = 0
        Me.txt_LRNO.Name = "txt_LRNO"
        Me.txt_LRNO.Size = New System.Drawing.Size(180, 19)
        Me.txt_LRNO.TabIndex = 43
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(431, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 15)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "LR NO :"
        '
        'lblAddress
        '
        Me.lblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(82, 96)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(303, 42)
        Me.lblAddress.TabIndex = 13
        '
        'lblCap2
        '
        Me.lblCap2.AutoSize = True
        Me.lblCap2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap2.Location = New System.Drawing.Point(17, 98)
        Me.lblCap2.Name = "lblCap2"
        Me.lblCap2.Size = New System.Drawing.Size(59, 15)
        Me.lblCap2.TabIndex = 12
        Me.lblCap2.Text = "Address :"
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.ForeColor = System.Drawing.Color.White
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(90, 57)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(295, 23)
        Me.cmbSupplier.TabIndex = 11
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(16, 60)
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
        Me.lbl_TransferDate.Location = New System.Drawing.Point(311, 26)
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
        Me.lbl_INVNo.Location = New System.Drawing.Point(87, 26)
        Me.lbl_INVNo.Name = "lbl_INVNo"
        Me.lbl_INVNo.Size = New System.Drawing.Size(45, 15)
        Me.lbl_INVNo.TabIndex = 3
        Me.lbl_INVNo.Text = "INV No"
        '
        'lblMRSDate
        '
        Me.lblMRSDate.AutoSize = True
        Me.lblMRSDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSDate.Location = New System.Drawing.Point(211, 26)
        Me.lblMRSDate.Name = "lblMRSDate"
        Me.lblMRSDate.Size = New System.Drawing.Size(80, 15)
        Me.lblMRSDate.TabIndex = 7
        Me.lblMRSDate.Text = "Invoice Date :"
        '
        'lblMRSCode
        '
        Me.lblMRSCode.AutoSize = True
        Me.lblMRSCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSCode.Location = New System.Drawing.Point(17, 26)
        Me.lblMRSCode.Name = "lblMRSCode"
        Me.lblMRSCode.Size = New System.Drawing.Size(53, 15)
        Me.lblMRSCode.TabIndex = 5
        Me.lblMRSCode.Text = "INV NO :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(719, 10)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(150, 25)
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
        'frm_Sale_Invoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Sale_Invoice"
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
    Friend WithEvents cmbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents lblCap1 As System.Windows.Forms.Label
    Private WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblCap2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtvechicle_no As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_LRNO As System.Windows.Forms.TextBox
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
End Class
