﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_SaleTaxRegister
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdTaxReport = New System.Windows.Forms.DataGridView()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdTaxReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtToDate)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Controls.Add(Me.txtFromDate)
        Me.GroupBox6.Controls.Add(Me.Label1)
        Me.GroupBox6.Controls.Add(Me.btnGenerate)
        Me.GroupBox6.Controls.Add(Me.btnShow)
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(9, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(876, 113)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        '
        'txtToDate
        '
        Me.txtToDate.CalendarForeColor = System.Drawing.Color.White
        Me.txtToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtToDate.CustomFormat = "dd-MMM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(449, 49)
        Me.txtToDate.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(142, 20)
        Me.txtToDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(341, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 15)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Select To Month"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalendarForeColor = System.Drawing.Color.White
        Me.txtFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtFromDate.CustomFormat = "dd-MMM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(149, 49)
        Me.txtFromDate.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(142, 20)
        Me.txtFromDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 15)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Select From Month"
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.ForeColor = System.Drawing.Color.ForestGreen
        Me.btnGenerate.Location = New System.Drawing.Point(624, 47)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(110, 25)
        Me.btnGenerate.TabIndex = 2
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.ForeColor = System.Drawing.Color.Red
        Me.btnShow.Location = New System.Drawing.Point(740, 47)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(110, 25)
        Me.btnShow.TabIndex = 3
        Me.btnShow.Text = "Export to Excel"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.grdTaxReport)
        Me.GroupBox2.ForeColor = System.Drawing.Color.DarkGray
        Me.GroupBox2.Location = New System.Drawing.Point(9, 125)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(876, 490)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'grdTaxReport
        '
        Me.grdTaxReport.AllowUserToAddRows = False
        Me.grdTaxReport.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.grdTaxReport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTaxReport.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdTaxReport.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkGray
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdTaxReport.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdTaxReport.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdTaxReport.Location = New System.Drawing.Point(3, 8)
        Me.grdTaxReport.Name = "grdTaxReport"
        Me.grdTaxReport.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTaxReport.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdTaxReport.RowHeadersVisible = False
        Me.grdTaxReport.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdTaxReport.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTaxReport.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdTaxReport.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.grdTaxReport.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdTaxReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdTaxReport.Size = New System.Drawing.Size(870, 480)
        Me.grdTaxReport.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer))
        Me.Label22.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label22.Location = New System.Drawing.Point(894, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(15, 225)
        Me.Label22.TabIndex = 58
        Me.Label22.Text = "GST  Sale  Register"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frm_SaleTaxRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox6)
        Me.Name = "frm_SaleTaxRegister"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdTaxReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents btnShow As Button
    Friend WithEvents btnGenerate As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents grdTaxReport As DataGridView
    Friend WithEvents txtToDate As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFromDate As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label22 As Label
End Class
