<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockValue
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
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbItemCatId = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmd_Show = New System.Windows.Forms.Button()
        Me.chkCalculateAllData = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chk_NotZeroQty = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb_subCategory = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpDate
        '
        Me.dtpDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(102, 161)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(137, 21)
        Me.dtpDate.TabIndex = 26
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 165)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 15)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Select Date"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.Location = New System.Drawing.Point(320, 120)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(198, 18)
        Me.txtItemName.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(245, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 15)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Item Name"
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.Location = New System.Drawing.Point(102, 120)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(137, 18)
        Me.txtItemCode.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 15)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Item Code:"
        '
        'cmbItemCatId
        '
        Me.cmbItemCatId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbItemCatId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemCatId.ForeColor = System.Drawing.Color.White
        Me.cmbItemCatId.FormattingEnabled = True
        Me.cmbItemCatId.Location = New System.Drawing.Point(102, 29)
        Me.cmbItemCatId.Name = "cmbItemCatId"
        Me.cmbItemCatId.Size = New System.Drawing.Size(416, 23)
        Me.cmbItemCatId.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 15)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Category Head:"
        '
        'cmd_Show
        '
        Me.cmd_Show.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Show.Location = New System.Drawing.Point(556, 114)
        Me.cmd_Show.Name = "cmd_Show"
        Me.cmd_Show.Size = New System.Drawing.Size(98, 31)
        Me.cmd_Show.TabIndex = 27
        Me.cmd_Show.Text = "Show"
        Me.cmd_Show.UseVisualStyleBackColor = True
        '
        'chkCalculateAllData
        '
        Me.chkCalculateAllData.AutoSize = True
        Me.chkCalculateAllData.Location = New System.Drawing.Point(248, 161)
        Me.chkCalculateAllData.Name = "chkCalculateAllData"
        Me.chkCalculateAllData.Size = New System.Drawing.Size(122, 19)
        Me.chkCalculateAllData.TabIndex = 28
        Me.chkCalculateAllData.Text = "Calculate All Data"
        Me.chkCalculateAllData.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chk_NotZeroQty)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmb_subCategory)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkCalculateAllData)
        Me.GroupBox1.Controls.Add(Me.cmbItemCatId)
        Me.GroupBox1.Controls.Add(Me.cmd_Show)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpDate)
        Me.GroupBox1.Controls.Add(Me.txtItemCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtItemName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(22, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(868, 237)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter options"
        '
        'chk_NotZeroQty
        '
        Me.chk_NotZeroQty.AutoSize = True
        Me.chk_NotZeroQty.Location = New System.Drawing.Point(381, 161)
        Me.chk_NotZeroQty.Name = "chk_NotZeroQty"
        Me.chk_NotZeroQty.Size = New System.Drawing.Size(137, 19)
        Me.chk_NotZeroQty.TabIndex = 31
        Me.chk_NotZeroQty.Text = "With zero stock Data"
        Me.chk_NotZeroQty.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 15)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Sub Category:"
        '
        'cmb_subCategory
        '
        Me.cmb_subCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_subCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_subCategory.Enabled = False
        Me.cmb_subCategory.ForeColor = System.Drawing.Color.White
        Me.cmb_subCategory.FormattingEnabled = True
        Me.cmb_subCategory.Location = New System.Drawing.Point(102, 72)
        Me.cmb_subCategory.Name = "cmb_subCategory"
        Me.cmb_subCategory.Size = New System.Drawing.Size(416, 23)
        Me.cmb_subCategory.TabIndex = 30
        '
        'frmStockValue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmStockValue"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbItemCatId As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_Show As System.Windows.Forms.Button
    Friend WithEvents chkCalculateAllData As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_subCategory As System.Windows.Forms.ComboBox
    Friend WithEvents chk_NotZeroQty As System.Windows.Forms.CheckBox

End Class
