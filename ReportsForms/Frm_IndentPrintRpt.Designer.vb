<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IndentPrintRpt
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
        Me.cmb_Indent_ID = New System.Windows.Forms.ComboBox()
        Me.cryMain = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.lbl_FromDate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmb_Indent_ID
        '
        Me.cmb_Indent_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_Indent_ID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_Indent_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_Indent_ID.ForeColor = System.Drawing.Color.White
        Me.cmb_Indent_ID.FormattingEnabled = True
        Me.cmb_Indent_ID.Location = New System.Drawing.Point(115, 9)
        Me.cmb_Indent_ID.Name = "cmb_Indent_ID"
        Me.cmb_Indent_ID.Size = New System.Drawing.Size(196, 23)
        Me.cmb_Indent_ID.TabIndex = 31
        '
        'cryMain
        '
        Me.cryMain.ActiveViewIndex = -1
        Me.cryMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cryMain.DisplayGroupTree = False
        Me.cryMain.Location = New System.Drawing.Point(14, 37)
        Me.cryMain.Name = "cryMain"
        Me.cryMain.SelectionFormula = ""
        Me.cryMain.ShowGroupTreeButton = False
        Me.cryMain.ShowRefreshButton = False
        Me.cryMain.Size = New System.Drawing.Size(873, 549)
        Me.cryMain.TabIndex = 29
        Me.cryMain.ViewTimeSelectionFormula = ""
        '
        'lbl_FromDate
        '
        Me.lbl_FromDate.AutoSize = True
        Me.lbl_FromDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FromDate.Location = New System.Drawing.Point(14, 13)
        Me.lbl_FromDate.Name = "lbl_FromDate"
        Me.lbl_FromDate.Size = New System.Drawing.Size(99, 15)
        Me.lbl_FromDate.TabIndex = 30
        Me.lbl_FromDate.Text = "Select Indent ID :"
        '
        'Frm_IndentPrintRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.cmb_Indent_ID)
        Me.Controls.Add(Me.cryMain)
        Me.Controls.Add(Me.lbl_FromDate)
        Me.Name = "Frm_IndentPrintRpt"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmb_Indent_ID As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_FromDate As System.Windows.Forms.Label
    Private WithEvents cryMain As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
