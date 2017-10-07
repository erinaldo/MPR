<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_LedgerSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LedgerSummary))
        Me.lbl_FromDate = New System.Windows.Forms.Label()
        Me.lbl_ToDate = New System.Windows.Forms.Label()
        Me.lbl_Item = New System.Windows.Forms.Label()
        Me.dtp_FromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtp_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.cmb_Item = New System.Windows.Forms.ComboBox()
        Me.cmd_Show = New System.Windows.Forms.Button()
        Me.cmb_Indent_ID = New System.Windows.Forms.ComboBox()
        Me.lbl_Indent_ID = New System.Windows.Forms.Label()
        Me.cmb_PO_ID = New System.Windows.Forms.ComboBox()
        Me.lbl_PO_ID = New System.Windows.Forms.Label()
        'Me.AxVLEHost1 = New AxVLELib.AxVLEHost()
        'CType(Me.AxVLEHost1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_FromDate
        '
        Me.lbl_FromDate.AutoSize = True
        Me.lbl_FromDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FromDate.ForeColor = System.Drawing.Color.White
        Me.lbl_FromDate.Location = New System.Drawing.Point(25, 129)
        Me.lbl_FromDate.Name = "lbl_FromDate"
        Me.lbl_FromDate.Size = New System.Drawing.Size(65, 15)
        Me.lbl_FromDate.TabIndex = 0
        Me.lbl_FromDate.Text = "From Date"
        '
        'lbl_ToDate
        '
        Me.lbl_ToDate.AutoSize = True
        Me.lbl_ToDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ToDate.ForeColor = System.Drawing.Color.White
        Me.lbl_ToDate.Location = New System.Drawing.Point(263, 129)
        Me.lbl_ToDate.Name = "lbl_ToDate"
        Me.lbl_ToDate.Size = New System.Drawing.Size(49, 15)
        Me.lbl_ToDate.TabIndex = 1
        Me.lbl_ToDate.Text = "To Date"
        '
        'lbl_Item
        '
        Me.lbl_Item.AutoSize = True
        Me.lbl_Item.Location = New System.Drawing.Point(20, 170)
        Me.lbl_Item.Name = "lbl_Item"
        Me.lbl_Item.Size = New System.Drawing.Size(60, 13)
        Me.lbl_Item.TabIndex = 2
        Me.lbl_Item.Text = "Select Item"
        '
        'dtp_FromDate
        '
        Me.dtp_FromDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_FromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_FromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_FromDate.Location = New System.Drawing.Point(116, 125)
        Me.dtp_FromDate.Name = "dtp_FromDate"
        Me.dtp_FromDate.Size = New System.Drawing.Size(122, 20)
        Me.dtp_FromDate.TabIndex = 3
        '
        'dtp_ToDate
        '
        Me.dtp_ToDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_ToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_ToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_ToDate.Location = New System.Drawing.Point(325, 125)
        Me.dtp_ToDate.Name = "dtp_ToDate"
        Me.dtp_ToDate.Size = New System.Drawing.Size(130, 20)
        Me.dtp_ToDate.TabIndex = 4
        '
        'cmb_Item
        '
        Me.cmb_Item.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_Item.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_Item.ForeColor = System.Drawing.Color.White
        Me.cmb_Item.FormattingEnabled = True
        Me.cmb_Item.Location = New System.Drawing.Point(113, 168)
        Me.cmb_Item.Name = "cmb_Item"
        Me.cmb_Item.Size = New System.Drawing.Size(340, 23)
        Me.cmb_Item.TabIndex = 5
        '
        'cmd_Show
        '
        Me.cmd_Show.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Show.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Show.ForeColor = System.Drawing.Color.White
        Me.cmd_Show.Location = New System.Drawing.Point(522, 164)
        Me.cmd_Show.Name = "cmd_Show"
        Me.cmd_Show.Size = New System.Drawing.Size(128, 31)
        Me.cmd_Show.TabIndex = 7
        Me.cmd_Show.Text = "Show"
        Me.cmd_Show.UseVisualStyleBackColor = True
        '
        'cmb_Indent_ID
        '
        Me.cmb_Indent_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_Indent_ID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_Indent_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_Indent_ID.ForeColor = System.Drawing.Color.White
        Me.cmb_Indent_ID.FormattingEnabled = True
        Me.cmb_Indent_ID.Location = New System.Drawing.Point(113, 168)
        Me.cmb_Indent_ID.Name = "cmb_Indent_ID"
        Me.cmb_Indent_ID.Size = New System.Drawing.Size(180, 23)
        Me.cmb_Indent_ID.TabIndex = 6
        '
        'lbl_Indent_ID
        '
        Me.lbl_Indent_ID.AutoSize = True
        Me.lbl_Indent_ID.Location = New System.Drawing.Point(25, 170)
        Me.lbl_Indent_ID.Name = "lbl_Indent_ID"
        Me.lbl_Indent_ID.Size = New System.Drawing.Size(84, 13)
        Me.lbl_Indent_ID.TabIndex = 8
        Me.lbl_Indent_ID.Text = "Select Indent ID"
        '
        'cmb_PO_ID
        '
        Me.cmb_PO_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_PO_ID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_PO_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_PO_ID.ForeColor = System.Drawing.Color.White
        Me.cmb_PO_ID.FormattingEnabled = True
        Me.cmb_PO_ID.Location = New System.Drawing.Point(113, 168)
        Me.cmb_PO_ID.Name = "cmb_PO_ID"
        Me.cmb_PO_ID.Size = New System.Drawing.Size(180, 23)
        Me.cmb_PO_ID.TabIndex = 9
        '
        'lbl_PO_ID
        '
        Me.lbl_PO_ID.AutoSize = True
        Me.lbl_PO_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PO_ID.ForeColor = System.Drawing.Color.White
        Me.lbl_PO_ID.Location = New System.Drawing.Point(25, 170)
        Me.lbl_PO_ID.Name = "lbl_PO_ID"
        Me.lbl_PO_ID.Size = New System.Drawing.Size(82, 15)
        Me.lbl_PO_ID.TabIndex = 8
        Me.lbl_PO_ID.Text = "Select PO ID :"
        '
        'AxVLEHost1
        '
        'Me.AxVLEHost1.Enabled = True
        'Me.AxVLEHost1.Location = New System.Drawing.Point(705, 405)
        'Me.AxVLEHost1.Name = "AxVLEHost1"
        'Me.AxVLEHost1.OcxState = CType(resources.GetObject("AxVLEHost1.OcxState"), System.Windows.Forms.AxHost.State)
        'Me.AxVLEHost1.Size = New System.Drawing.Size(192, 192)
        'Me.AxVLEHost1.TabIndex = 10
        '
        'frm_LedgerSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        'Me.Controls.Add(Me.AxVLEHost1)
        Me.Controls.Add(Me.cmd_Show)
        Me.Controls.Add(Me.cmb_Item)
        Me.Controls.Add(Me.cmb_Indent_ID)
        Me.Controls.Add(Me.cmb_PO_ID)
        Me.Controls.Add(Me.lbl_PO_ID)
        Me.Controls.Add(Me.dtp_ToDate)
        Me.Controls.Add(Me.dtp_FromDate)
        Me.Controls.Add(Me.lbl_Item)
        Me.Controls.Add(Me.lbl_ToDate)
        Me.Controls.Add(Me.lbl_FromDate)
        Me.Controls.Add(Me.lbl_Indent_ID)
        Me.Name = "frm_LedgerSummary"
        Me.Size = New System.Drawing.Size(910, 630)
        'CType(Me.AxVLEHost1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_FromDate As System.Windows.Forms.Label
    Friend WithEvents lbl_ToDate As System.Windows.Forms.Label
    Friend WithEvents lbl_Item As System.Windows.Forms.Label
    Friend WithEvents dtp_FromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmb_Item As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_Show As System.Windows.Forms.Button
    Friend WithEvents cmb_Indent_ID As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_Indent_ID As System.Windows.Forms.Label
    Friend WithEvents cmb_PO_ID As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_PO_ID As System.Windows.Forms.Label
    'Friend WithEvents AxVLEHost1 As AxVLELib.AxVLEHost

End Class
