<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Show_search
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Show_search))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grdSearch = New System.Windows.Forms.DataGridView()
        Me.gpAdvanceSearch = New System.Windows.Forms.GroupBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.txtMRP = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblMRP = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.cmbBrand = New System.Windows.Forms.ComboBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.grdSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpAdvanceSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.grdSearch)
        Me.Panel1.Controls.Add(Me.gpAdvanceSearch)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 100.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(10, Byte))
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(810, 438)
        Me.Panel1.TabIndex = 3
        '
        'grdSearch
        '
        Me.grdSearch.AllowUserToAddRows = False
        Me.grdSearch.AllowUserToDeleteRows = False
        Me.grdSearch.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSearch.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSearch.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSearch.Location = New System.Drawing.Point(4, 184)
        Me.grdSearch.Name = "grdSearch"
        Me.grdSearch.ReadOnly = True
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSearch.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdSearch.RowHeadersVisible = False
        Me.grdSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSearch.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.grdSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdSearch.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.grdSearch.Size = New System.Drawing.Size(790, 237)
        Me.grdSearch.TabIndex = 2
        '
        'gpAdvanceSearch
        '
        Me.gpAdvanceSearch.Controls.Add(Me.btnExit)
        Me.gpAdvanceSearch.Controls.Add(Me.lblItem)
        Me.gpAdvanceSearch.Controls.Add(Me.txtRate)
        Me.gpAdvanceSearch.Controls.Add(Me.lblRate)
        Me.gpAdvanceSearch.Controls.Add(Me.txtMRP)
        Me.gpAdvanceSearch.Controls.Add(Me.txtSearch)
        Me.gpAdvanceSearch.Controls.Add(Me.lblMRP)
        Me.gpAdvanceSearch.Controls.Add(Me.lblCategory)
        Me.gpAdvanceSearch.Controls.Add(Me.cmbCategory)
        Me.gpAdvanceSearch.Controls.Add(Me.lblBrand)
        Me.gpAdvanceSearch.Controls.Add(Me.cmbBrand)
        Me.gpAdvanceSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpAdvanceSearch.ForeColor = System.Drawing.Color.White
        Me.gpAdvanceSearch.Location = New System.Drawing.Point(5, 3)
        Me.gpAdvanceSearch.Name = "gpAdvanceSearch"
        Me.gpAdvanceSearch.Size = New System.Drawing.Size(789, 173)
        Me.gpAdvanceSearch.TabIndex = 4
        Me.gpAdvanceSearch.TabStop = False
        Me.gpAdvanceSearch.Text = "Filter Options"
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblItem.Location = New System.Drawing.Point(132, 131)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(134, 15)
        Me.lblItem.TabIndex = 8
        Me.lblItem.Text = "Search Item / Barcode :"
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRate.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtRate.ForeColor = System.Drawing.Color.White
        Me.txtRate.Location = New System.Drawing.Point(474, 96)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(127, 18)
        Me.txtRate.TabIndex = 7
        '
        'lblRate
        '
        Me.lblRate.AutoSize = True
        Me.lblRate.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblRate.Location = New System.Drawing.Point(407, 99)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(39, 15)
        Me.lblRate.TabIndex = 6
        Me.lblRate.Text = "Rate :"
        '
        'txtMRP
        '
        Me.txtMRP.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtMRP.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMRP.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtMRP.ForeColor = System.Drawing.Color.White
        Me.txtMRP.Location = New System.Drawing.Point(272, 96)
        Me.txtMRP.Name = "txtMRP"
        Me.txtMRP.Size = New System.Drawing.Size(120, 18)
        Me.txtMRP.TabIndex = 5
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(272, 129)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(329, 18)
        Me.txtSearch.TabIndex = 0
        '
        'lblMRP
        '
        Me.lblMRP.AutoSize = True
        Me.lblMRP.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblMRP.Location = New System.Drawing.Point(191, 99)
        Me.lblMRP.Name = "lblMRP"
        Me.lblMRP.Size = New System.Drawing.Size(39, 15)
        Me.lblMRP.TabIndex = 4
        Me.lblMRP.Text = "MRP :"
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblCategory.Location = New System.Drawing.Point(191, 64)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(62, 15)
        Me.lblCategory.TabIndex = 3
        Me.lblCategory.Text = "Category :"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(272, 58)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(329, 21)
        Me.cmbCategory.TabIndex = 2
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblBrand.Location = New System.Drawing.Point(191, 26)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(46, 15)
        Me.lblBrand.TabIndex = 1
        Me.lblBrand.Text = "Brand :"
        '
        'cmbBrand
        '
        Me.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBrand.FormattingEnabled = True
        Me.cmbBrand.Location = New System.Drawing.Point(272, 20)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.Size = New System.Drawing.Size(329, 21)
        Me.cmbBrand.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Red
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Black
        Me.btnExit.Location = New System.Drawing.Point(758, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(20, 23)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "X"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'frm_Show_search
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(811, 439)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frm_Show_search"
        Me.Padding = New System.Windows.Forms.Padding(20)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpAdvanceSearch.ResumeLayout(False)
        Me.gpAdvanceSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents grdSearch As DataGridView
    Friend WithEvents gpAdvanceSearch As GroupBox
    Friend WithEvents lblItem As Label
    Friend WithEvents txtRate As TextBox
    Friend WithEvents lblRate As Label
    Friend WithEvents txtMRP As TextBox
    Public WithEvents txtSearch As TextBox
    Friend WithEvents lblMRP As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents lblBrand As Label
    Friend WithEvents cmbBrand As ComboBox
    Friend WithEvents btnExit As Button
End Class
