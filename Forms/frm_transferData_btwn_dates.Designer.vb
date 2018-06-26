<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_transferData_btwn_dates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_transferData_btwn_dates))
        Me.btnDataTransfer = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstTablesName = New System.Windows.Forms.ListBox()
        Me.pbardatatransfer = New System.Windows.Forms.ProgressBar()
        Me.lblstdate = New System.Windows.Forms.Label()
        Me.dtpfrmdate = New System.Windows.Forms.DateTimePicker()
        Me.lbltodate = New System.Windows.Forms.Label()
        Me.dtptodate = New System.Windows.Forms.DateTimePicker()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btnQuickDataTransfer = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDataTransfer
        '
        Me.btnDataTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDataTransfer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDataTransfer.Location = New System.Drawing.Point(272, 276)
        Me.btnDataTransfer.Name = "btnDataTransfer"
        Me.btnDataTransfer.Size = New System.Drawing.Size(93, 31)
        Me.btnDataTransfer.TabIndex = 1
        Me.btnDataTransfer.Text = "Data Transfer"
        Me.btnDataTransfer.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstTablesName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(15, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(486, 151)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transfered Tables"
        '
        'lstTablesName
        '
        Me.lstTablesName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lstTablesName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTablesName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTablesName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTablesName.ForeColor = System.Drawing.Color.White
        Me.lstTablesName.FormattingEnabled = True
        Me.lstTablesName.ItemHeight = 15
        Me.lstTablesName.Location = New System.Drawing.Point(3, 17)
        Me.lstTablesName.Name = "lstTablesName"
        Me.lstTablesName.Size = New System.Drawing.Size(480, 131)
        Me.lstTablesName.TabIndex = 0
        '
        'pbardatatransfer
        '
        Me.pbardatatransfer.Location = New System.Drawing.Point(13, 239)
        Me.pbardatatransfer.Name = "pbardatatransfer"
        Me.pbardatatransfer.Size = New System.Drawing.Size(489, 23)
        Me.pbardatatransfer.TabIndex = 3
        '
        'lblstdate
        '
        Me.lblstdate.AutoSize = True
        Me.lblstdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstdate.Location = New System.Drawing.Point(12, 24)
        Me.lblstdate.Name = "lblstdate"
        Me.lblstdate.Size = New System.Drawing.Size(65, 15)
        Me.lblstdate.TabIndex = 4
        Me.lblstdate.Text = "From Date"
        '
        'dtpfrmdate
        '
        Me.dtpfrmdate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpfrmdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpfrmdate.Location = New System.Drawing.Point(97, 19)
        Me.dtpfrmdate.Name = "dtpfrmdate"
        Me.dtpfrmdate.Size = New System.Drawing.Size(200, 20)
        Me.dtpfrmdate.TabIndex = 5
        '
        'lbltodate
        '
        Me.lbltodate.AutoSize = True
        Me.lbltodate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltodate.Location = New System.Drawing.Point(12, 57)
        Me.lbltodate.Name = "lbltodate"
        Me.lbltodate.Size = New System.Drawing.Size(49, 15)
        Me.lbltodate.TabIndex = 4
        Me.lbltodate.Text = "To Date"
        '
        'dtptodate
        '
        Me.dtptodate.CalendarForeColor = System.Drawing.Color.White
        Me.dtptodate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtptodate.Location = New System.Drawing.Point(97, 52)
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.Size = New System.Drawing.Size(200, 20)
        Me.dtptodate.TabIndex = 5
        '
        'btn_Cancel
        '
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.White
        Me.btn_Cancel.Location = New System.Drawing.Point(191, 276)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 31)
        Me.btn_Cancel.TabIndex = 7
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'btnQuickDataTransfer
        '
        Me.btnQuickDataTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuickDataTransfer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuickDataTransfer.Location = New System.Drawing.Point(368, 276)
        Me.btnQuickDataTransfer.Name = "btnQuickDataTransfer"
        Me.btnQuickDataTransfer.Size = New System.Drawing.Size(133, 31)
        Me.btnQuickDataTransfer.TabIndex = 8
        Me.btnQuickDataTransfer.Text = "Quick Data Transfer"
        Me.btnQuickDataTransfer.UseVisualStyleBackColor = True
        '
        'frm_transferData_btwn_dates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(514, 319)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnQuickDataTransfer)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.dtptodate)
        Me.Controls.Add(Me.lbltodate)
        Me.Controls.Add(Me.dtpfrmdate)
        Me.Controls.Add(Me.lblstdate)
        Me.Controls.Add(Me.pbardatatransfer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnDataTransfer)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_transferData_btwn_dates"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDataTransfer As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstTablesName As System.Windows.Forms.ListBox
    Friend WithEvents pbardatatransfer As System.Windows.Forms.ProgressBar
    Friend WithEvents lblstdate As System.Windows.Forms.Label
    Friend WithEvents dtpfrmdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbltodate As System.Windows.Forms.Label
    Friend WithEvents dtptodate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btn_Cancel As Button
    Friend WithEvents btnQuickDataTransfer As Button
End Class
