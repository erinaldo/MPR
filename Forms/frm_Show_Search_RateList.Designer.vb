<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Show_Search_RateList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Show_Search_RateList))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gpAdvanceSearch = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.txtMRP = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblMRP = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.grdSearch = New System.Windows.Forms.DataGridView()
        Me.chkBxSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmbCategory = New MMSPlus.AutoCompleteCombo()
        Me.cmbBrand = New MMSPlus.AutoCompleteCombo()
        Me.Panel1.SuspendLayout()
        Me.gpAdvanceSearch.SuspendLayout()
        CType(Me.grdSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.gpAdvanceSearch)
        Me.Panel1.Controls.Add(Me.grdSearch)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 100.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(10, Byte))
        Me.Panel1.Location = New System.Drawing.Point(-1, -2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(808, 431)
        Me.Panel1.TabIndex = 3
        '
        'gpAdvanceSearch
        '
        Me.gpAdvanceSearch.BackColor = System.Drawing.Color.DarkGray
        Me.gpAdvanceSearch.Controls.Add(Me.cmbCategory)
        Me.gpAdvanceSearch.Controls.Add(Me.cmbBrand)
        Me.gpAdvanceSearch.Controls.Add(Me.btnExit)
        Me.gpAdvanceSearch.Controls.Add(Me.lblItem)
        Me.gpAdvanceSearch.Controls.Add(Me.txtRate)
        Me.gpAdvanceSearch.Controls.Add(Me.lblRate)
        Me.gpAdvanceSearch.Controls.Add(Me.txtMRP)
        Me.gpAdvanceSearch.Controls.Add(Me.txtSearch)
        Me.gpAdvanceSearch.Controls.Add(Me.lblMRP)
        Me.gpAdvanceSearch.Controls.Add(Me.lblCategory)
        Me.gpAdvanceSearch.Controls.Add(Me.lblBrand)
        Me.gpAdvanceSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpAdvanceSearch.ForeColor = System.Drawing.Color.White
        Me.gpAdvanceSearch.Location = New System.Drawing.Point(-1, -1)
        Me.gpAdvanceSearch.Name = "gpAdvanceSearch"
        Me.gpAdvanceSearch.Size = New System.Drawing.Size(808, 175)
        Me.gpAdvanceSearch.TabIndex = 0
        Me.gpAdvanceSearch.TabStop = False
        Me.gpAdvanceSearch.Text = "Filter Options"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Red
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(777, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(25, 23)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "X"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblItem.Location = New System.Drawing.Point(28, 142)
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
        Me.txtRate.Location = New System.Drawing.Point(499, 104)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(250, 18)
        Me.txtRate.TabIndex = 4
        '
        'lblRate
        '
        Me.lblRate.AutoSize = True
        Me.lblRate.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblRate.Location = New System.Drawing.Point(437, 107)
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
        Me.txtMRP.Location = New System.Drawing.Point(162, 105)
        Me.txtMRP.Name = "txtMRP"
        Me.txtMRP.Size = New System.Drawing.Size(250, 18)
        Me.txtMRP.TabIndex = 3
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(162, 140)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(587, 18)
        Me.txtSearch.TabIndex = 0
        '
        'lblMRP
        '
        Me.lblMRP.AutoSize = True
        Me.lblMRP.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblMRP.Location = New System.Drawing.Point(28, 107)
        Me.lblMRP.Name = "lblMRP"
        Me.lblMRP.Size = New System.Drawing.Size(39, 15)
        Me.lblMRP.TabIndex = 4
        Me.lblMRP.Text = "MRP :"
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblCategory.Location = New System.Drawing.Point(28, 69)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(62, 15)
        Me.lblCategory.TabIndex = 3
        Me.lblCategory.Text = "Category :"
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblBrand.Location = New System.Drawing.Point(29, 31)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(46, 15)
        Me.lblBrand.TabIndex = 1
        Me.lblBrand.Text = "Brand :"
        '
        'grdSearch
        '
        Me.grdSearch.AllowUserToAddRows = False
        Me.grdSearch.AllowUserToDeleteRows = False
        Me.grdSearch.BackgroundColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 100.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(10, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSearch.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSearch.ColumnHeadersHeight = 25
        Me.grdSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdSearch.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkBxSelect})
        Me.grdSearch.GridColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdSearch.Location = New System.Drawing.Point(-1, 176)
        Me.grdSearch.Name = "grdSearch"
        Me.grdSearch.RowHeadersVisible = False
        Me.grdSearch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdSearch.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.25!)
        Me.grdSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.grdSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.grdSearch.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.grdSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSearch.Size = New System.Drawing.Size(806, 245)
        Me.grdSearch.TabIndex = 1
        '
        'chkBxSelect
        '
        Me.chkBxSelect.HeaderText = ""
        Me.chkBxSelect.Name = "chkBxSelect"
        Me.chkBxSelect.Width = 25
        '
        'cmbCategory
        '
        Me.cmbCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.ForeColor = System.Drawing.Color.White
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(162, 65)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.ResetOnClear = False
        Me.cmbCategory.Size = New System.Drawing.Size(587, 24)
        Me.cmbCategory.TabIndex = 2
        '
        'cmbBrand
        '
        Me.cmbBrand.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBrand.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBrand.ForeColor = System.Drawing.Color.White
        Me.cmbBrand.FormattingEnabled = True
        Me.cmbBrand.Location = New System.Drawing.Point(162, 27)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.ResetOnClear = False
        Me.cmbBrand.Size = New System.Drawing.Size(587, 24)
        Me.cmbBrand.TabIndex = 1
        '
        'frm_Show_Search_RateList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(804, 425)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frm_Show_Search_RateList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.gpAdvanceSearch.ResumeLayout(False)
        Me.gpAdvanceSearch.PerformLayout()
        CType(Me.grdSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents gpAdvanceSearch As GroupBox
    Friend WithEvents btnExit As Button
    Friend WithEvents lblItem As Label
    Friend WithEvents txtRate As TextBox
    Friend WithEvents lblRate As Label
    Friend WithEvents txtMRP As TextBox
    Public WithEvents txtSearch As TextBox
    Friend WithEvents lblMRP As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents lblBrand As Label
    Private WithEvents grdSearch As DataGridView
    Friend WithEvents chkBxSelect As DataGridViewCheckBoxColumn
    Friend WithEvents cmbBrand As AutoCompleteCombo
    Friend WithEvents cmbCategory As AutoCompleteCombo
End Class
