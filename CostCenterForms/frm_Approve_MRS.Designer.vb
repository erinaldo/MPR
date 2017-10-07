<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Approve_MRS
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rdoRequiredDate = New System.Windows.Forms.RadioButton()
        Me.rdoMRSDate = New System.Windows.Forms.RadioButton()
        Me.dtpMRSTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpMRSFrom = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdMRSList = New System.Windows.Forms.DataGridView()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.btInverseSelect = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdMRSList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnShow)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.rdoRequiredDate)
        Me.GroupBox1.Controls.Add(Me.rdoMRSDate)
        Me.GroupBox1.Controls.Add(Me.dtpMRSTo)
        Me.GroupBox1.Controls.Add(Me.dtpMRSFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(894, 108)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnShow
        '
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(437, 57)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(127, 29)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Show MRS"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(236, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "To :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "From :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(730, 9)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(162, 25)
        Me.lblFormHeading.TabIndex = 3
        Me.lblFormHeading.Text = "Approve MRS"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Search By:"
        '
        'rdoRequiredDate
        '
        Me.rdoRequiredDate.AutoSize = True
        Me.rdoRequiredDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoRequiredDate.Location = New System.Drawing.Point(197, 20)
        Me.rdoRequiredDate.Name = "rdoRequiredDate"
        Me.rdoRequiredDate.Size = New System.Drawing.Size(105, 19)
        Me.rdoRequiredDate.TabIndex = 2
        Me.rdoRequiredDate.Text = "Required Date"
        Me.rdoRequiredDate.UseVisualStyleBackColor = True
        '
        'rdoMRSDate
        '
        Me.rdoMRSDate.AutoSize = True
        Me.rdoMRSDate.Checked = True
        Me.rdoMRSDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoMRSDate.Location = New System.Drawing.Point(95, 20)
        Me.rdoMRSDate.Name = "rdoMRSDate"
        Me.rdoMRSDate.Size = New System.Drawing.Size(80, 19)
        Me.rdoMRSDate.TabIndex = 2
        Me.rdoMRSDate.TabStop = True
        Me.rdoMRSDate.Text = "MRS Date"
        Me.rdoMRSDate.UseVisualStyleBackColor = True
        '
        'dtpMRSTo
        '
        Me.dtpMRSTo.CalendarForeColor = System.Drawing.Color.White
        Me.dtpMRSTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpMRSTo.CalendarTitleForeColor = System.Drawing.Color.Black
        Me.dtpMRSTo.CustomFormat = "dd-MMM-yyyy"
        Me.dtpMRSTo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMRSTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMRSTo.Location = New System.Drawing.Point(281, 62)
        Me.dtpMRSTo.Name = "dtpMRSTo"
        Me.dtpMRSTo.Size = New System.Drawing.Size(134, 21)
        Me.dtpMRSTo.TabIndex = 1
        '
        'dtpMRSFrom
        '
        Me.dtpMRSFrom.CalendarForeColor = System.Drawing.Color.White
        Me.dtpMRSFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpMRSFrom.CalendarTitleForeColor = System.Drawing.Color.Black
        Me.dtpMRSFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dtpMRSFrom.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMRSFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMRSFrom.Location = New System.Drawing.Point(96, 62)
        Me.dtpMRSFrom.Name = "dtpMRSFrom"
        Me.dtpMRSFrom.Size = New System.Drawing.Size(134, 21)
        Me.dtpMRSFrom.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdMRSList)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 113)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(894, 459)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'grdMRSList
        '
        Me.grdMRSList.AllowUserToAddRows = False
        Me.grdMRSList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdMRSList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMRSList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdMRSList.Location = New System.Drawing.Point(6, 16)
        Me.grdMRSList.Name = "grdMRSList"
        Me.grdMRSList.RowHeadersVisible = False
        Me.grdMRSList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdMRSList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMRSList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdMRSList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdMRSList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdMRSList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdMRSList.Size = New System.Drawing.Size(882, 435)
        Me.grdMRSList.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(341, 581)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(135, 29)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "Update MRS"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(482, 581)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(135, 29)
        Me.btnSelectAll.TabIndex = 2
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAll.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeselectAll.Location = New System.Drawing.Point(623, 581)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(135, 29)
        Me.btnDeselectAll.TabIndex = 2
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'btInverseSelect
        '
        Me.btInverseSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btInverseSelect.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btInverseSelect.Location = New System.Drawing.Point(764, 581)
        Me.btInverseSelect.Name = "btInverseSelect"
        Me.btInverseSelect.Size = New System.Drawing.Size(135, 29)
        Me.btInverseSelect.TabIndex = 2
        Me.btInverseSelect.Text = "Inverse Select"
        Me.btInverseSelect.UseVisualStyleBackColor = True
        '
        'frm_Approve_MRS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.btInverseSelect)
        Me.Controls.Add(Me.btnDeselectAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "frm_Approve_MRS"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdMRSList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdoRequiredDate As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMRSDate As System.Windows.Forms.RadioButton
    Friend WithEvents dtpMRSTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpMRSFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdMRSList As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btInverseSelect As System.Windows.Forms.Button
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label

End Class
