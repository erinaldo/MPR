<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MRNDetails
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
        Me.cryMain = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.dtp_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtp_FromDate = New System.Windows.Forms.DateTimePicker()
        Me.lbl_ToDate = New System.Windows.Forms.Label()
        Me.lbl_FromDate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cryMain
        '
        Me.cryMain.ActiveViewIndex = -1
        Me.cryMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cryMain.DisplayGroupTree = False
        Me.cryMain.EnableDrillDown = False
        Me.cryMain.Location = New System.Drawing.Point(14, 46)
        Me.cryMain.Name = "cryMain"
        Me.cryMain.SelectionFormula = ""
        Me.cryMain.ShowGroupTreeButton = False
        Me.cryMain.ShowRefreshButton = False
        Me.cryMain.Size = New System.Drawing.Size(873, 549)
        Me.cryMain.TabIndex = 13
        Me.cryMain.ViewTimeSelectionFormula = ""
        '
        'dtp_ToDate
        '
        Me.dtp_ToDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_ToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_ToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_ToDate.Location = New System.Drawing.Point(521, 14)
        Me.dtp_ToDate.Name = "dtp_ToDate"
        Me.dtp_ToDate.Size = New System.Drawing.Size(106, 20)
        Me.dtp_ToDate.TabIndex = 11
        '
        'dtp_FromDate
        '
        Me.dtp_FromDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_FromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_FromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_FromDate.Location = New System.Drawing.Point(305, 14)
        Me.dtp_FromDate.Name = "dtp_FromDate"
        Me.dtp_FromDate.Size = New System.Drawing.Size(106, 20)
        Me.dtp_FromDate.TabIndex = 10
        '
        'lbl_ToDate
        '
        Me.lbl_ToDate.AutoSize = True
        Me.lbl_ToDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ToDate.ForeColor = System.Drawing.Color.White
        Me.lbl_ToDate.Location = New System.Drawing.Point(456, 18)
        Me.lbl_ToDate.Name = "lbl_ToDate"
        Me.lbl_ToDate.Size = New System.Drawing.Size(49, 15)
        Me.lbl_ToDate.TabIndex = 8
        Me.lbl_ToDate.Text = "To Date"
        '
        'lbl_FromDate
        '
        Me.lbl_FromDate.AutoSize = True
        Me.lbl_FromDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FromDate.ForeColor = System.Drawing.Color.White
        Me.lbl_FromDate.Location = New System.Drawing.Point(231, 18)
        Me.lbl_FromDate.Name = "lbl_FromDate"
        Me.lbl_FromDate.Size = New System.Drawing.Size(65, 15)
        Me.lbl_FromDate.TabIndex = 7
        Me.lbl_FromDate.Text = "From Date"
        '
        'frm_MRNDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.cryMain)
        Me.Controls.Add(Me.dtp_ToDate)
        Me.Controls.Add(Me.dtp_FromDate)
        Me.Controls.Add(Me.lbl_ToDate)
        Me.Controls.Add(Me.lbl_FromDate)
        Me.Name = "frm_MRNDetails"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtp_ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_FromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_ToDate As System.Windows.Forms.Label
    Friend WithEvents lbl_FromDate As System.Windows.Forms.Label
    Private WithEvents cryMain As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
