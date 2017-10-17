<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_rpt_Open_Purchase_Order
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
        Me.cmb_PO_ID = New System.Windows.Forms.ComboBox()
        Me.lbl_PO_ID = New System.Windows.Forms.Label()
        Me.SuspendLayout()
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
        Me.cryMain.TabIndex = 30
        Me.cryMain.ViewTimeSelectionFormula = ""
        '
        'cmb_PO_ID
        '
        Me.cmb_PO_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_PO_ID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_PO_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_PO_ID.ForeColor = System.Drawing.Color.White
        Me.cmb_PO_ID.FormattingEnabled = True
        Me.cmb_PO_ID.Location = New System.Drawing.Point(104, 9)
        Me.cmb_PO_ID.Name = "cmb_PO_ID"
        Me.cmb_PO_ID.Size = New System.Drawing.Size(196, 23)
        Me.cmb_PO_ID.TabIndex = 33
        '
        'lbl_PO_ID
        '
        Me.lbl_PO_ID.AutoSize = True
        Me.lbl_PO_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PO_ID.ForeColor = System.Drawing.Color.White
        Me.lbl_PO_ID.Location = New System.Drawing.Point(14, 13)
        Me.lbl_PO_ID.Name = "lbl_PO_ID"
        Me.lbl_PO_ID.Size = New System.Drawing.Size(82, 15)
        Me.lbl_PO_ID.TabIndex = 32
        Me.lbl_PO_ID.Text = "Select PO ID :"
        '
        'Frm_rpt_Open_Purchase_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.cmb_PO_ID)
        Me.Controls.Add(Me.lbl_PO_ID)
        Me.Controls.Add(Me.cryMain)
        Me.Name = "Frm_rpt_Open_Purchase_Order"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmb_PO_ID As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_PO_ID As System.Windows.Forms.Label
    Private WithEvents cryMain As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
