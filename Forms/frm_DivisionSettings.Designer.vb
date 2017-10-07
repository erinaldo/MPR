<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DivisionSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_DivisionSettings))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddl_outlets = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblOutletId = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lbl_msg = New System.Windows.Forms.Label()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.lbl_cityID = New System.Windows.Forms.Label()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.lbl_RevPOMRNPrefix = New System.Windows.Forms.Label()
        Me.lbl_RevMRNPrefix = New System.Windows.Forms.Label()
        Me.lbl_MRSPrefix = New System.Windows.Forms.Label()
        Me.lbl_RevMioPrefix = New System.Windows.Forms.Label()
        Me.lbl_MioPrefix = New System.Windows.Forms.Label()
        Me.lbl_ReceiptPrefix = New System.Windows.Forms.Label()
        Me.lbl_RevWastagePrefix = New System.Windows.Forms.Label()
        Me.lbl_WastagePrefix = New System.Windows.Forms.Label()
        Me.lbl_IndentPrefix = New System.Windows.Forms.Label()
        Me.lbl_DivPrefix = New System.Windows.Forms.Label()
        Me.lbl_City = New System.Windows.Forms.Label()
        Me.lbl_mailAddress = New System.Windows.Forms.Label()
        Me.lbl_ZipCode = New System.Windows.Forms.Label()
        Me.lbl_PhoneNo = New System.Windows.Forms.Label()
        Me.lbl_OutletTinNo = New System.Windows.Forms.Label()
        Me.lbl_OutletAddress = New System.Windows.Forms.Label()
        Me.lbl_OutletName = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_Caption = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Outlet :"
        '
        'ddl_outlets
        '
        Me.ddl_outlets.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ddl_outlets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddl_outlets.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddl_outlets.ForeColor = System.Drawing.Color.White
        Me.ddl_outlets.FormattingEnabled = True
        Me.ddl_outlets.Location = New System.Drawing.Point(206, 25)
        Me.ddl_outlets.Name = "ddl_outlets"
        Me.ddl_outlets.Size = New System.Drawing.Size(283, 26)
        Me.ddl_outlets.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblOutletId)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ddl_outlets)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GroupBox1.Location = New System.Drawing.Point(14, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(866, 73)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Division"
        '
        'lblOutletId
        '
        Me.lblOutletId.AutoSize = True
        Me.lblOutletId.Location = New System.Drawing.Point(569, 31)
        Me.lblOutletId.Name = "lblOutletId"
        Me.lblOutletId.Size = New System.Drawing.Size(52, 15)
        Me.lblOutletId.TabIndex = 4
        Me.lblOutletId.Text = "Label19"
        Me.lblOutletId.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbl_msg)
        Me.GroupBox2.Controls.Add(Me.btn_Cancel)
        Me.GroupBox2.Controls.Add(Me.lbl_cityID)
        Me.GroupBox2.Controls.Add(Me.btn_Save)
        Me.GroupBox2.Controls.Add(Me.lbl_RevPOMRNPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_RevMRNPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_MRSPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_RevMioPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_MioPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_ReceiptPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_RevWastagePrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_WastagePrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_IndentPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_DivPrefix)
        Me.GroupBox2.Controls.Add(Me.lbl_City)
        Me.GroupBox2.Controls.Add(Me.lbl_mailAddress)
        Me.GroupBox2.Controls.Add(Me.lbl_ZipCode)
        Me.GroupBox2.Controls.Add(Me.lbl_PhoneNo)
        Me.GroupBox2.Controls.Add(Me.lbl_OutletTinNo)
        Me.GroupBox2.Controls.Add(Me.lbl_OutletAddress)
        Me.GroupBox2.Controls.Add(Me.lbl_OutletName)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(14, 101)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(866, 382)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Division Details"
        '
        'lbl_msg
        '
        Me.lbl_msg.ForeColor = System.Drawing.Color.Brown
        Me.lbl_msg.Location = New System.Drawing.Point(286, 333)
        Me.lbl_msg.Name = "lbl_msg"
        Me.lbl_msg.Size = New System.Drawing.Size(293, 23)
        Me.lbl_msg.TabIndex = 35
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Location = New System.Drawing.Point(729, 332)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(87, 27)
        Me.btn_Cancel.TabIndex = 5
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'lbl_cityID
        '
        Me.lbl_cityID.AutoSize = True
        Me.lbl_cityID.Location = New System.Drawing.Point(373, 140)
        Me.lbl_cityID.Name = "lbl_cityID"
        Me.lbl_cityID.Size = New System.Drawing.Size(0, 15)
        Me.lbl_cityID.TabIndex = 34
        Me.lbl_cityID.Visible = False
        '
        'btn_Save
        '
        Me.btn_Save.Location = New System.Drawing.Point(608, 332)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(87, 27)
        Me.btn_Save.TabIndex = 4
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'lbl_RevPOMRNPrefix
        '
        Me.lbl_RevPOMRNPrefix.AutoSize = True
        Me.lbl_RevPOMRNPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RevPOMRNPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_RevPOMRNPrefix.Location = New System.Drawing.Point(763, 283)
        Me.lbl_RevPOMRNPrefix.Name = "lbl_RevPOMRNPrefix"
        Me.lbl_RevPOMRNPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_RevPOMRNPrefix.TabIndex = 33
        Me.lbl_RevPOMRNPrefix.Text = "Label19"
        '
        'lbl_RevMRNPrefix
        '
        Me.lbl_RevMRNPrefix.AutoSize = True
        Me.lbl_RevMRNPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RevMRNPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_RevMRNPrefix.Location = New System.Drawing.Point(454, 283)
        Me.lbl_RevMRNPrefix.Name = "lbl_RevMRNPrefix"
        Me.lbl_RevMRNPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_RevMRNPrefix.TabIndex = 32
        Me.lbl_RevMRNPrefix.Text = "Label19"
        '
        'lbl_MRSPrefix
        '
        Me.lbl_MRSPrefix.AutoSize = True
        Me.lbl_MRSPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MRSPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_MRSPrefix.Location = New System.Drawing.Point(148, 283)
        Me.lbl_MRSPrefix.Name = "lbl_MRSPrefix"
        Me.lbl_MRSPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_MRSPrefix.TabIndex = 31
        Me.lbl_MRSPrefix.Text = "Label19"
        '
        'lbl_RevMioPrefix
        '
        Me.lbl_RevMioPrefix.AutoSize = True
        Me.lbl_RevMioPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RevMioPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_RevMioPrefix.Location = New System.Drawing.Point(763, 234)
        Me.lbl_RevMioPrefix.Name = "lbl_RevMioPrefix"
        Me.lbl_RevMioPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_RevMioPrefix.TabIndex = 30
        Me.lbl_RevMioPrefix.Text = "Label19"
        '
        'lbl_MioPrefix
        '
        Me.lbl_MioPrefix.AutoSize = True
        Me.lbl_MioPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MioPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_MioPrefix.Location = New System.Drawing.Point(454, 234)
        Me.lbl_MioPrefix.Name = "lbl_MioPrefix"
        Me.lbl_MioPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_MioPrefix.TabIndex = 29
        Me.lbl_MioPrefix.Text = "Label19"
        '
        'lbl_ReceiptPrefix
        '
        Me.lbl_ReceiptPrefix.AutoSize = True
        Me.lbl_ReceiptPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ReceiptPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_ReceiptPrefix.Location = New System.Drawing.Point(148, 234)
        Me.lbl_ReceiptPrefix.Name = "lbl_ReceiptPrefix"
        Me.lbl_ReceiptPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_ReceiptPrefix.TabIndex = 28
        Me.lbl_ReceiptPrefix.Text = "Label19"
        '
        'lbl_RevWastagePrefix
        '
        Me.lbl_RevWastagePrefix.AutoSize = True
        Me.lbl_RevWastagePrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RevWastagePrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_RevWastagePrefix.Location = New System.Drawing.Point(763, 183)
        Me.lbl_RevWastagePrefix.Name = "lbl_RevWastagePrefix"
        Me.lbl_RevWastagePrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_RevWastagePrefix.TabIndex = 27
        Me.lbl_RevWastagePrefix.Text = "Label19"
        '
        'lbl_WastagePrefix
        '
        Me.lbl_WastagePrefix.AutoSize = True
        Me.lbl_WastagePrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_WastagePrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_WastagePrefix.Location = New System.Drawing.Point(454, 183)
        Me.lbl_WastagePrefix.Name = "lbl_WastagePrefix"
        Me.lbl_WastagePrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_WastagePrefix.TabIndex = 26
        Me.lbl_WastagePrefix.Text = "Label19"
        '
        'lbl_IndentPrefix
        '
        Me.lbl_IndentPrefix.AutoSize = True
        Me.lbl_IndentPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_IndentPrefix.Location = New System.Drawing.Point(148, 183)
        Me.lbl_IndentPrefix.Name = "lbl_IndentPrefix"
        Me.lbl_IndentPrefix.Size = New System.Drawing.Size(53, 16)
        Me.lbl_IndentPrefix.TabIndex = 25
        Me.lbl_IndentPrefix.Text = "Label19"
        '
        'lbl_DivPrefix
        '
        Me.lbl_DivPrefix.AutoSize = True
        Me.lbl_DivPrefix.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DivPrefix.ForeColor = System.Drawing.Color.Orange
        Me.lbl_DivPrefix.Location = New System.Drawing.Point(763, 37)
        Me.lbl_DivPrefix.Name = "lbl_DivPrefix"
        Me.lbl_DivPrefix.Size = New System.Drawing.Size(52, 15)
        Me.lbl_DivPrefix.TabIndex = 24
        Me.lbl_DivPrefix.Text = "Label19"
        Me.lbl_DivPrefix.Visible = False
        '
        'lbl_City
        '
        Me.lbl_City.AutoSize = True
        Me.lbl_City.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_City.ForeColor = System.Drawing.Color.Orange
        Me.lbl_City.Location = New System.Drawing.Point(454, 140)
        Me.lbl_City.Name = "lbl_City"
        Me.lbl_City.Size = New System.Drawing.Size(53, 16)
        Me.lbl_City.TabIndex = 23
        Me.lbl_City.Text = "Label19"
        '
        'lbl_mailAddress
        '
        Me.lbl_mailAddress.AutoSize = True
        Me.lbl_mailAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_mailAddress.ForeColor = System.Drawing.Color.Orange
        Me.lbl_mailAddress.Location = New System.Drawing.Point(148, 140)
        Me.lbl_mailAddress.Name = "lbl_mailAddress"
        Me.lbl_mailAddress.Size = New System.Drawing.Size(53, 16)
        Me.lbl_mailAddress.TabIndex = 22
        Me.lbl_mailAddress.Text = "Label19"
        '
        'lbl_ZipCode
        '
        Me.lbl_ZipCode.AutoSize = True
        Me.lbl_ZipCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ZipCode.ForeColor = System.Drawing.Color.Orange
        Me.lbl_ZipCode.Location = New System.Drawing.Point(763, 140)
        Me.lbl_ZipCode.Name = "lbl_ZipCode"
        Me.lbl_ZipCode.Size = New System.Drawing.Size(53, 16)
        Me.lbl_ZipCode.TabIndex = 21
        Me.lbl_ZipCode.Text = "Label19"
        '
        'lbl_PhoneNo
        '
        Me.lbl_PhoneNo.AutoSize = True
        Me.lbl_PhoneNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PhoneNo.ForeColor = System.Drawing.Color.Orange
        Me.lbl_PhoneNo.Location = New System.Drawing.Point(454, 106)
        Me.lbl_PhoneNo.Name = "lbl_PhoneNo"
        Me.lbl_PhoneNo.Size = New System.Drawing.Size(53, 16)
        Me.lbl_PhoneNo.TabIndex = 20
        Me.lbl_PhoneNo.Text = "Label19"
        '
        'lbl_OutletTinNo
        '
        Me.lbl_OutletTinNo.AutoSize = True
        Me.lbl_OutletTinNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_OutletTinNo.ForeColor = System.Drawing.Color.Orange
        Me.lbl_OutletTinNo.Location = New System.Drawing.Point(148, 106)
        Me.lbl_OutletTinNo.Name = "lbl_OutletTinNo"
        Me.lbl_OutletTinNo.Size = New System.Drawing.Size(53, 16)
        Me.lbl_OutletTinNo.TabIndex = 19
        Me.lbl_OutletTinNo.Text = "Label19"
        '
        'lbl_OutletAddress
        '
        Me.lbl_OutletAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_OutletAddress.ForeColor = System.Drawing.Color.Orange
        Me.lbl_OutletAddress.Location = New System.Drawing.Point(148, 68)
        Me.lbl_OutletAddress.Name = "lbl_OutletAddress"
        Me.lbl_OutletAddress.Size = New System.Drawing.Size(309, 30)
        Me.lbl_OutletAddress.TabIndex = 18
        Me.lbl_OutletAddress.Text = "Label19"
        '
        'lbl_OutletName
        '
        Me.lbl_OutletName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_OutletName.ForeColor = System.Drawing.Color.Orange
        Me.lbl_OutletName.Location = New System.Drawing.Point(148, 35)
        Me.lbl_OutletName.Name = "lbl_OutletName"
        Me.lbl_OutletName.Size = New System.Drawing.Size(285, 15)
        Me.lbl_OutletName.TabIndex = 17
        Me.lbl_OutletName.Text = "Label19"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(604, 283)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 15)
        Me.Label18.TabIndex = 16
        Me.Label18.Text = "Rev PO MRN Prefix"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(313, 283)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(91, 15)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Rev MRN Prefix"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(20, 283)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 15)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "MRS Prefix"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(604, 234)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 15)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Rev MIO Prefix"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(313, 234)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 15)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "MIO Prefix"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(20, 234)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Receipt Prefix"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(605, 180)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(113, 15)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Rev Wastage Prefix"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(313, 183)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 15)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Wastage Prefix"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 183)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 15)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Indent Prefix"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(605, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 15)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Division Prefix"
        Me.Label9.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(313, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 15)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "City"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 15)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Mail Address"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(605, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Zip Code"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(313, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 15)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Phone No."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "GST No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Address :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Name :"
        '
        'lbl_Caption
        '
        Me.lbl_Caption.AutoSize = True
        Me.lbl_Caption.Font = New System.Drawing.Font("Arial Narrow", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Caption.ForeColor = System.Drawing.Color.Orange
        Me.lbl_Caption.Location = New System.Drawing.Point(29, 35)
        Me.lbl_Caption.Name = "lbl_Caption"
        Me.lbl_Caption.Size = New System.Drawing.Size(177, 31)
        Me.lbl_Caption.TabIndex = 5
        Me.lbl_Caption.Text = "Division Details"
        Me.lbl_Caption.Visible = False
        '
        'frm_DivisionSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(892, 497)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbl_Caption)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_DivisionSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddl_outlets As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lbl_OutletName As System.Windows.Forms.Label
    Friend WithEvents lbl_OutletAddress As System.Windows.Forms.Label
    Friend WithEvents lbl_OutletTinNo As System.Windows.Forms.Label
    Friend WithEvents lbl_PhoneNo As System.Windows.Forms.Label
    Friend WithEvents lbl_ZipCode As System.Windows.Forms.Label
    Friend WithEvents lbl_mailAddress As System.Windows.Forms.Label
    Friend WithEvents lbl_City As System.Windows.Forms.Label
    Friend WithEvents lbl_DivPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_IndentPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_WastagePrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_RevWastagePrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_ReceiptPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_MioPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_RevMioPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_MRSPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_RevMRNPrefix As System.Windows.Forms.Label
    Friend WithEvents lbl_RevPOMRNPrefix As System.Windows.Forms.Label
    Friend WithEvents btn_Save As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents lbl_cityID As System.Windows.Forms.Label
    Friend WithEvents lbl_msg As System.Windows.Forms.Label
    Friend WithEvents lblOutletId As System.Windows.Forms.Label
    Friend WithEvents lbl_Caption As System.Windows.Forms.Label
End Class
