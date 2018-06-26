<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_transferData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_transferData))
        Me.btnDataTransfer = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstTablesName = New System.Windows.Forms.ListBox()
        Me.pbardatatransfer = New System.Windows.Forms.ProgressBar()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDataTransfer
        '
        Me.btnDataTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDataTransfer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDataTransfer.Location = New System.Drawing.Point(322, 272)
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
        Me.GroupBox1.Location = New System.Drawing.Point(25, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(390, 151)
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
        Me.lstTablesName.Size = New System.Drawing.Size(384, 131)
        Me.lstTablesName.TabIndex = 0
        '
        'pbardatatransfer
        '
        Me.pbardatatransfer.Location = New System.Drawing.Point(25, 222)
        Me.pbardatatransfer.Name = "pbardatatransfer"
        Me.pbardatatransfer.Size = New System.Drawing.Size(390, 23)
        Me.pbardatatransfer.TabIndex = 3
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(25, 31)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(70, 15)
        Me.lblDate.TabIndex = 4
        Me.lblDate.Text = "Select Date"
        '
        'dtpDate
        '
        Me.dtpDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpDate.Location = New System.Drawing.Point(136, 26)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpDate.TabIndex = 5
        '
        'btn_Cancel
        '
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.White
        Me.btn_Cancel.Location = New System.Drawing.Point(241, 272)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 31)
        Me.btn_Cancel.TabIndex = 6
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frm_transferData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(441, 320)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.pbardatatransfer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnDataTransfer)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_transferData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDataTransfer As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstTablesName As System.Windows.Forms.ListBox
    Friend WithEvents pbardatatransfer As System.Windows.Forms.ProgressBar
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btn_Cancel As Button
End Class
