<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_freeze_stock_transfer
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblstatus = New System.Windows.Forms.Label
        Me.rdbFreeze = New System.Windows.Forms.RadioButton
        Me.rdbFresh = New System.Windows.Forms.RadioButton
        Me.btnShow = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblFormHeading = New System.Windows.Forms.Label
        Me.dtptodt = New System.Windows.Forms.DateTimePicker
        Me.dtpfrmdt = New System.Windows.Forms.DateTimePicker
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdstocklist = New System.Windows.Forms.DataGridView
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.btnDeselectAll = New System.Windows.Forms.Button
        Me.btInverseSelect = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdstocklist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblstatus)
        Me.GroupBox1.Controls.Add(Me.rdbFreeze)
        Me.GroupBox1.Controls.Add(Me.rdbFresh)
        Me.GroupBox1.Controls.Add(Me.btnShow)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.dtptodt)
        Me.GroupBox1.Controls.Add(Me.dtpfrmdt)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(894, 108)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Freeze Stock Transfer"
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(33, 30)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(73, 18)
        Me.lblstatus.TabIndex = 6
        Me.lblstatus.Text = "Status :"
        '
        'rdbFreeze
        '
        Me.rdbFreeze.Location = New System.Drawing.Point(206, 32)
        Me.rdbFreeze.Name = "rdbFreeze"
        Me.rdbFreeze.Size = New System.Drawing.Size(73, 17)
        Me.rdbFreeze.TabIndex = 5
        Me.rdbFreeze.Text = "Freeze"
        Me.rdbFreeze.UseVisualStyleBackColor = True
        '
        'rdbFresh
        '
        Me.rdbFresh.AutoSize = True
        Me.rdbFresh.Checked = True
        Me.rdbFresh.Location = New System.Drawing.Point(128, 32)
        Me.rdbFresh.Name = "rdbFresh"
        Me.rdbFresh.Size = New System.Drawing.Size(51, 17)
        Me.rdbFresh.TabIndex = 5
        Me.rdbFresh.TabStop = True
        Me.rdbFresh.Text = "Fresh"
        Me.rdbFresh.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(439, 62)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(127, 29)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(254, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 18)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "To :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 18)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "From :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFormHeading.Location = New System.Drawing.Point(616, 30)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(175, 50)
        Me.lblFormHeading.TabIndex = 3
        Me.lblFormHeading.Text = "Freeze " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Stock Transfer"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtptodt
        '
        Me.dtptodt.CustomFormat = "dd-MMM-yyyy"
        Me.dtptodt.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtptodt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodt.Location = New System.Drawing.Point(299, 63)
        Me.dtptodt.Name = "dtptodt"
        Me.dtptodt.Size = New System.Drawing.Size(134, 26)
        Me.dtptodt.TabIndex = 1
        '
        'dtpfrmdt
        '
        Me.dtpfrmdt.CustomFormat = "dd-MMM-yyyy"
        Me.dtpfrmdt.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfrmdt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfrmdt.Location = New System.Drawing.Point(114, 63)
        Me.dtpfrmdt.Name = "dtpfrmdt"
        Me.dtpfrmdt.Size = New System.Drawing.Size(134, 26)
        Me.dtpfrmdt.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdstocklist)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 117)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(894, 402)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'grdstocklist
        '
        Me.grdstocklist.AllowUserToAddRows = False
        Me.grdstocklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdstocklist.Location = New System.Drawing.Point(3, 16)
        Me.grdstocklist.Name = "grdstocklist"
        Me.grdstocklist.Size = New System.Drawing.Size(888, 380)
        Me.grdstocklist.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(331, 531)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(135, 29)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(472, 531)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(135, 29)
        Me.btnSelectAll.TabIndex = 2
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeselectAll.Location = New System.Drawing.Point(613, 531)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(135, 29)
        Me.btnDeselectAll.TabIndex = 2
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'btInverseSelect
        '
        Me.btInverseSelect.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btInverseSelect.Location = New System.Drawing.Point(754, 531)
        Me.btInverseSelect.Name = "btInverseSelect"
        Me.btInverseSelect.Size = New System.Drawing.Size(135, 29)
        Me.btInverseSelect.TabIndex = 2
        Me.btInverseSelect.Text = "Inverse Select"
        Me.btInverseSelect.UseVisualStyleBackColor = True
        '
        'frm_freeze_stock_transfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btInverseSelect)
        Me.Controls.Add(Me.btnDeselectAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_freeze_stock_transfer"
        Me.Size = New System.Drawing.Size(900, 571)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdstocklist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtptodt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpfrmdt As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdstocklist As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btInverseSelect As System.Windows.Forms.Button
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents rdbFresh As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFreeze As System.Windows.Forms.RadioButton
    Friend WithEvents lblstatus As System.Windows.Forms.Label

End Class
