<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Approve_Indent
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
        Me.rdoIndentDate = New System.Windows.Forms.RadioButton()
        Me.dtpIndentTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpIndentFrom = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdIndentList = New System.Windows.Forms.DataGridView()
        Me.btnApproveIndent = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.btInverseSelect = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdIndentList, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Controls.Add(Me.rdoIndentDate)
        Me.GroupBox1.Controls.Add(Me.dtpIndentTo)
        Me.GroupBox1.Controls.Add(Me.dtpIndentFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(904, 125)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnShow.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(472, 71)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(100, 25)
        Me.btnShow.TabIndex = 5
        Me.btnShow.Text = "Show Indents"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(284, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "To :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 15)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "From :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(710, 12)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(188, 25)
        Me.lblFormHeading.TabIndex = 14
        Me.lblFormHeading.Text = "Approve Indent"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Search By:"
        '
        'rdoRequiredDate
        '
        Me.rdoRequiredDate.AutoSize = True
        Me.rdoRequiredDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoRequiredDate.Location = New System.Drawing.Point(287, 27)
        Me.rdoRequiredDate.Name = "rdoRequiredDate"
        Me.rdoRequiredDate.Size = New System.Drawing.Size(105, 19)
        Me.rdoRequiredDate.TabIndex = 2
        Me.rdoRequiredDate.Text = "Required Date"
        Me.rdoRequiredDate.UseVisualStyleBackColor = True
        '
        'rdoIndentDate
        '
        Me.rdoIndentDate.AutoSize = True
        Me.rdoIndentDate.Checked = True
        Me.rdoIndentDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoIndentDate.Location = New System.Drawing.Point(112, 31)
        Me.rdoIndentDate.Name = "rdoIndentDate"
        Me.rdoIndentDate.Size = New System.Drawing.Size(88, 19)
        Me.rdoIndentDate.TabIndex = 1
        Me.rdoIndentDate.TabStop = True
        Me.rdoIndentDate.Text = "Indent Date"
        Me.rdoIndentDate.UseVisualStyleBackColor = True
        '
        'dtpIndentTo
        '
        Me.dtpIndentTo.CalendarForeColor = System.Drawing.Color.White
        Me.dtpIndentTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpIndentTo.CustomFormat = "dd-MMM-yyyy"
        Me.dtpIndentTo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpIndentTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIndentTo.Location = New System.Drawing.Point(328, 72)
        Me.dtpIndentTo.Name = "dtpIndentTo"
        Me.dtpIndentTo.Size = New System.Drawing.Size(114, 21)
        Me.dtpIndentTo.TabIndex = 4
        '
        'dtpIndentFrom
        '
        Me.dtpIndentFrom.CalendarForeColor = System.Drawing.Color.White
        Me.dtpIndentFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpIndentFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dtpIndentFrom.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpIndentFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIndentFrom.Location = New System.Drawing.Point(112, 72)
        Me.dtpIndentFrom.Name = "dtpIndentFrom"
        Me.dtpIndentFrom.Size = New System.Drawing.Size(115, 21)
        Me.dtpIndentFrom.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdIndentList)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 135)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(904, 445)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'grdIndentList
        '
        Me.grdIndentList.AllowUserToAddRows = False
        Me.grdIndentList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdIndentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdIndentList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdIndentList.Location = New System.Drawing.Point(7, 12)
        Me.grdIndentList.Name = "grdIndentList"
        Me.grdIndentList.RowHeadersVisible = False
        Me.grdIndentList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdIndentList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdIndentList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdIndentList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.grdIndentList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdIndentList.Size = New System.Drawing.Size(891, 424)
        Me.grdIndentList.TabIndex = 7
        '
        'btnApproveIndent
        '
        Me.btnApproveIndent.BackColor = System.Drawing.Color.Green
        Me.btnApproveIndent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApproveIndent.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApproveIndent.Location = New System.Drawing.Point(489, 593)
        Me.btnApproveIndent.Name = "btnApproveIndent"
        Me.btnApproveIndent.Size = New System.Drawing.Size(100, 25)
        Me.btnApproveIndent.TabIndex = 8
        Me.btnApproveIndent.Text = "Update Indent"
        Me.btnApproveIndent.UseVisualStyleBackColor = False
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(595, 593)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(100, 25)
        Me.btnSelectAll.TabIndex = 9
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = False
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.BackColor = System.Drawing.Color.Maroon
        Me.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAll.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeselectAll.Location = New System.Drawing.Point(701, 593)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(100, 25)
        Me.btnDeselectAll.TabIndex = 10
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = False
        '
        'btInverseSelect
        '
        Me.btInverseSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btInverseSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btInverseSelect.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btInverseSelect.Location = New System.Drawing.Point(807, 593)
        Me.btInverseSelect.Name = "btInverseSelect"
        Me.btInverseSelect.Size = New System.Drawing.Size(100, 25)
        Me.btInverseSelect.TabIndex = 11
        Me.btInverseSelect.Text = "Inverse Select"
        Me.btInverseSelect.UseVisualStyleBackColor = False
        '
        'frm_Approve_Indent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.btInverseSelect)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnDeselectAll)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnApproveIndent)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "frm_Approve_Indent"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdIndentList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdoRequiredDate As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIndentDate As System.Windows.Forms.RadioButton
    Friend WithEvents dtpIndentTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpIndentFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdIndentList As System.Windows.Forms.DataGridView
    Friend WithEvents btnApproveIndent As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btInverseSelect As System.Windows.Forms.Button
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label

End Class
