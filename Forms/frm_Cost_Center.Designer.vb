<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Cost_Center
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Cost_Center))
        Me.GBCostCenterEntry = New System.Windows.Forms.GroupBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.lbl_CostCenterStatus = New System.Windows.Forms.Label()
        Me.txt_CostCenter_Discription = New System.Windows.Forms.TextBox()
        Me.txt_CostCenter_Name = New System.Windows.Forms.TextBox()
        Me.lbl_CC_Code = New System.Windows.Forms.Label()
        Me.lblCostCenterDiscription = New System.Windows.Forms.Label()
        Me.lblCostCenterName = New System.Windows.Forms.Label()
        Me.lblCostCenterCode = New System.Windows.Forms.Label()
        Me.DgvCostCenterMaster = New System.Windows.Forms.DataGridView()
        Me.GBCostcenterMaster = New System.Windows.Forms.GroupBox()
        Me.TBCCostCenter = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ErrorProviderCC = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GBCostCenterEntry.SuspendLayout()
        CType(Me.DgvCostCenterMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBCostcenterMaster.SuspendLayout()
        Me.TBCCostCenter.SuspendLayout()
        Me.List.SuspendLayout()
        Me.Detail.SuspendLayout()
        CType(Me.ErrorProviderCC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GBCostCenterEntry
        '
        Me.GBCostCenterEntry.BackColor = System.Drawing.Color.Transparent
        Me.GBCostCenterEntry.Controls.Add(Me.lblFormHeading)
        Me.GBCostCenterEntry.Controls.Add(Me.chkStatus)
        Me.GBCostCenterEntry.Controls.Add(Me.lbl_CostCenterStatus)
        Me.GBCostCenterEntry.Controls.Add(Me.txt_CostCenter_Discription)
        Me.GBCostCenterEntry.Controls.Add(Me.txt_CostCenter_Name)
        Me.GBCostCenterEntry.Controls.Add(Me.lbl_CC_Code)
        Me.GBCostCenterEntry.Controls.Add(Me.lblCostCenterDiscription)
        Me.GBCostCenterEntry.Controls.Add(Me.lblCostCenterName)
        Me.GBCostCenterEntry.Controls.Add(Me.lblCostCenterCode)
        Me.GBCostCenterEntry.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBCostCenterEntry.ForeColor = System.Drawing.Color.White
        Me.GBCostCenterEntry.Location = New System.Drawing.Point(36, 26)
        Me.GBCostCenterEntry.Name = "GBCostCenterEntry"
        Me.GBCostCenterEntry.Size = New System.Drawing.Size(816, 536)
        Me.GBCostCenterEntry.TabIndex = 3
        Me.GBCostCenterEntry.TabStop = False
        Me.GBCostCenterEntry.Text = "Cost Center Master"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(584, 20)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(224, 25)
        Me.lblFormHeading.TabIndex = 30
        Me.lblFormHeading.Text = "Cost Center Master"
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.Location = New System.Drawing.Point(195, 165)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(114, 17)
        Me.chkStatus.TabIndex = 15
        Me.chkStatus.Text = "Active / De Active"
        Me.chkStatus.UseVisualStyleBackColor = True
        '
        'lbl_CostCenterStatus
        '
        Me.lbl_CostCenterStatus.AutoSize = True
        Me.lbl_CostCenterStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CostCenterStatus.Location = New System.Drawing.Point(33, 165)
        Me.lbl_CostCenterStatus.Name = "lbl_CostCenterStatus"
        Me.lbl_CostCenterStatus.Size = New System.Drawing.Size(117, 15)
        Me.lbl_CostCenterStatus.TabIndex = 14
        Me.lbl_CostCenterStatus.Text = "Cost Center Status :"
        '
        'txt_CostCenter_Discription
        '
        Me.txt_CostCenter_Discription.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_CostCenter_Discription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_CostCenter_Discription.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_CostCenter_Discription.ForeColor = System.Drawing.Color.White
        Me.txt_CostCenter_Discription.Location = New System.Drawing.Point(195, 304)
        Me.txt_CostCenter_Discription.Multiline = True
        Me.txt_CostCenter_Discription.Name = "txt_CostCenter_Discription"
        Me.txt_CostCenter_Discription.Size = New System.Drawing.Size(590, 198)
        Me.txt_CostCenter_Discription.TabIndex = 13
        '
        'txt_CostCenter_Name
        '
        Me.txt_CostCenter_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_CostCenter_Name.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_CostCenter_Name.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_CostCenter_Name.ForeColor = System.Drawing.Color.White
        Me.txt_CostCenter_Name.Location = New System.Drawing.Point(195, 234)
        Me.txt_CostCenter_Name.MaxLength = 500
        Me.txt_CostCenter_Name.Name = "txt_CostCenter_Name"
        Me.txt_CostCenter_Name.Size = New System.Drawing.Size(590, 19)
        Me.txt_CostCenter_Name.TabIndex = 12
        '
        'lbl_CC_Code
        '
        Me.lbl_CC_Code.AutoSize = True
        Me.lbl_CC_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CC_Code.Location = New System.Drawing.Point(192, 102)
        Me.lbl_CC_Code.Name = "lbl_CC_Code"
        Me.lbl_CC_Code.Size = New System.Drawing.Size(90, 13)
        Me.lbl_CC_Code.TabIndex = 11
        Me.lbl_CC_Code.Text = "Cost Center Code"
        '
        'lblCostCenterDiscription
        '
        Me.lblCostCenterDiscription.AutoSize = True
        Me.lblCostCenterDiscription.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenterDiscription.Location = New System.Drawing.Point(33, 304)
        Me.lblCostCenterDiscription.Name = "lblCostCenterDiscription"
        Me.lblCostCenterDiscription.Size = New System.Drawing.Size(141, 15)
        Me.lblCostCenterDiscription.TabIndex = 7
        Me.lblCostCenterDiscription.Text = "Cost Center Discription :"
        '
        'lblCostCenterName
        '
        Me.lblCostCenterName.AutoSize = True
        Me.lblCostCenterName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenterName.Location = New System.Drawing.Point(33, 234)
        Me.lblCostCenterName.Name = "lblCostCenterName"
        Me.lblCostCenterName.Size = New System.Drawing.Size(116, 15)
        Me.lblCostCenterName.TabIndex = 6
        Me.lblCostCenterName.Text = "Cost Center Name :"
        '
        'lblCostCenterCode
        '
        Me.lblCostCenterCode.AutoSize = True
        Me.lblCostCenterCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenterCode.Location = New System.Drawing.Point(33, 101)
        Me.lblCostCenterCode.Name = "lblCostCenterCode"
        Me.lblCostCenterCode.Size = New System.Drawing.Size(112, 15)
        Me.lblCostCenterCode.TabIndex = 5
        Me.lblCostCenterCode.Text = "Cost Center Code :"
        '
        'DgvCostCenterMaster
        '
        Me.DgvCostCenterMaster.AllowUserToAddRows = False
        Me.DgvCostCenterMaster.AllowUserToDeleteRows = False
        Me.DgvCostCenterMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DgvCostCenterMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCostCenterMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DgvCostCenterMaster.Location = New System.Drawing.Point(26, 32)
        Me.DgvCostCenterMaster.Name = "DgvCostCenterMaster"
        Me.DgvCostCenterMaster.ReadOnly = True
        Me.DgvCostCenterMaster.RowHeadersVisible = False
        Me.DgvCostCenterMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DgvCostCenterMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvCostCenterMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DgvCostCenterMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.DgvCostCenterMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DgvCostCenterMaster.Size = New System.Drawing.Size(763, 495)
        Me.DgvCostCenterMaster.TabIndex = 4
        '
        'GBCostcenterMaster
        '
        Me.GBCostcenterMaster.Controls.Add(Me.DgvCostCenterMaster)
        Me.GBCostcenterMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBCostcenterMaster.ForeColor = System.Drawing.Color.White
        Me.GBCostcenterMaster.Location = New System.Drawing.Point(37, 10)
        Me.GBCostcenterMaster.Name = "GBCostcenterMaster"
        Me.GBCostcenterMaster.Size = New System.Drawing.Size(816, 554)
        Me.GBCostcenterMaster.TabIndex = 5
        Me.GBCostcenterMaster.TabStop = False
        Me.GBCostcenterMaster.Text = "Cost Center Master"
        '
        'TBCCostCenter
        '
        Me.TBCCostCenter.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TBCCostCenter.Controls.Add(Me.List)
        Me.TBCCostCenter.Controls.Add(Me.Detail)
        Me.TBCCostCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TBCCostCenter.ImageList = Me.ImageList1
        Me.TBCCostCenter.Location = New System.Drawing.Point(0, 0)
        Me.TBCCostCenter.Name = "TBCCostCenter"
        Me.TBCCostCenter.SelectedIndex = 0
        Me.TBCCostCenter.Size = New System.Drawing.Size(900, 630)
        Me.TBCCostCenter.TabIndex = 6
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.GBCostcenterMaster)
        Me.List.ForeColor = System.Drawing.Color.White
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(892, 600)
        Me.List.TabIndex = 0
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.GBCostCenterEntry)
        Me.Detail.ForeColor = System.Drawing.Color.White
        Me.Detail.ImageIndex = 1
        Me.Detail.Location = New System.Drawing.Point(4, 26)
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New System.Windows.Forms.Padding(3)
        Me.Detail.Size = New System.Drawing.Size(892, 600)
        Me.Detail.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'ErrorProviderCC
        '
        Me.ErrorProviderCC.ContainerControl = Me
        '
        'frm_Cost_Center
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TBCCostCenter)
        Me.Name = "frm_Cost_Center"
        Me.Size = New System.Drawing.Size(900, 630)
        Me.GBCostCenterEntry.ResumeLayout(False)
        Me.GBCostCenterEntry.PerformLayout()
        CType(Me.DgvCostCenterMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBCostcenterMaster.ResumeLayout(False)
        Me.TBCCostCenter.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.Detail.ResumeLayout(False)
        CType(Me.ErrorProviderCC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GBCostCenterEntry As System.Windows.Forms.GroupBox
    Friend WithEvents lblCostCenterDiscription As System.Windows.Forms.Label
    Friend WithEvents lblCostCenterName As System.Windows.Forms.Label
    Friend WithEvents lblCostCenterCode As System.Windows.Forms.Label
    Friend WithEvents lbl_CC_Code As System.Windows.Forms.Label
    Friend WithEvents DgvCostCenterMaster As System.Windows.Forms.DataGridView
    Friend WithEvents txt_CostCenter_Discription As System.Windows.Forms.TextBox
    Friend WithEvents txt_CostCenter_Name As System.Windows.Forms.TextBox
    Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_CostCenterStatus As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents GBCostcenterMaster As System.Windows.Forms.GroupBox
    Friend WithEvents TBCCostCenter As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents ErrorProviderCC As System.Windows.Forms.ErrorProvider
    Friend WithEvents ImageList1 As ImageList

End Class
