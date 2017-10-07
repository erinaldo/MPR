<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Item_Ledger
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
        Me.GBRMWPM = New System.Windows.Forms.GroupBox()
        Me.btInverseSelect = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.grdItemMaster = New System.Windows.Forms.DataGridView()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.dt_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dt_FromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GBRMWPM.SuspendLayout()
        CType(Me.grdItemMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GBRMWPM
        '
        Me.GBRMWPM.Controls.Add(Me.btInverseSelect)
        Me.GBRMWPM.Controls.Add(Me.btnDeselectAll)
        Me.GBRMWPM.Controls.Add(Me.btnSelectAll)
        Me.GBRMWPM.Controls.Add(Me.grdItemMaster)
        Me.GBRMWPM.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBRMWPM.ForeColor = System.Drawing.Color.White
        Me.GBRMWPM.Location = New System.Drawing.Point(23, 102)
        Me.GBRMWPM.Name = "GBRMWPM"
        Me.GBRMWPM.Size = New System.Drawing.Size(867, 506)
        Me.GBRMWPM.TabIndex = 21
        Me.GBRMWPM.TabStop = False
        Me.GBRMWPM.Text = "List Reverse Material without PO"
        '
        'btInverseSelect
        '
        Me.btInverseSelect.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btInverseSelect.Location = New System.Drawing.Point(724, 469)
        Me.btInverseSelect.Name = "btInverseSelect"
        Me.btInverseSelect.Size = New System.Drawing.Size(135, 29)
        Me.btInverseSelect.TabIndex = 18
        Me.btInverseSelect.Text = "Inverse Select"
        Me.btInverseSelect.UseVisualStyleBackColor = True
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeselectAll.Location = New System.Drawing.Point(583, 469)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(135, 29)
        Me.btnDeselectAll.TabIndex = 19
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(442, 469)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(135, 29)
        Me.btnSelectAll.TabIndex = 16
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'grdItemMaster
        '
        Me.grdItemMaster.AllowUserToAddRows = False
        Me.grdItemMaster.AllowUserToDeleteRows = False
        Me.grdItemMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemMaster.Location = New System.Drawing.Point(3, 17)
        Me.grdItemMaster.Name = "grdItemMaster"
        Me.grdItemMaster.RowHeadersVisible = False
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.OrangeRed
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdItemMaster.Size = New System.Drawing.Size(861, 447)
        Me.grdItemMaster.TabIndex = 15
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.dt_ToDate)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.dt_FromDate)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.txtSearch)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Location = New System.Drawing.Point(23, 11)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(867, 85)
        Me.GroupBox5.TabIndex = 20
        Me.GroupBox5.TabStop = False
        '
        'dt_ToDate
        '
        Me.dt_ToDate.CalendarForeColor = System.Drawing.Color.White
        Me.dt_ToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dt_ToDate.CustomFormat = "dd-MMM-yyyy"
        Me.dt_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dt_ToDate.Location = New System.Drawing.Point(372, 19)
        Me.dt_ToDate.Name = "dt_ToDate"
        Me.dt_ToDate.Size = New System.Drawing.Size(125, 20)
        Me.dt_ToDate.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(297, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "To Date :"
        '
        'dt_FromDate
        '
        Me.dt_FromDate.CalendarForeColor = System.Drawing.Color.White
        Me.dt_FromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dt_FromDate.CustomFormat = "dd-MMM-yyyy"
        Me.dt_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dt_FromDate.Location = New System.Drawing.Point(124, 19)
        Me.dt_FromDate.Name = "dt_FromDate"
        Me.dt_FromDate.Size = New System.Drawing.Size(125, 20)
        Me.dt_FromDate.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(36, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "From Date :"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(124, 52)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(693, 19)
        Me.txtSearch.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(36, 53)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 15)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Search By :"
        '
        'frm_Item_Ledger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.GBRMWPM)
        Me.Controls.Add(Me.GroupBox5)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "frm_Item_Ledger"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.GBRMWPM.ResumeLayout(False)
        CType(Me.grdItemMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GBRMWPM As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dt_FromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dt_ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdItemMaster As System.Windows.Forms.DataGridView
    Friend WithEvents btInverseSelect As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button

End Class
