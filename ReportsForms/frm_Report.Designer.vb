<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Report))
        Me.cryViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.Ts_report = New System.Windows.Forms.ToolStrip
        Me.TsbExport = New System.Windows.Forms.ToolStripButton
        Me.TsbPrint = New System.Windows.Forms.ToolStripButton
        Me.TsbFirstPage = New System.Windows.Forms.ToolStripButton
        Me.Tsbprevious = New System.Windows.Forms.ToolStripButton
        Me.Tspnext = New System.Windows.Forms.ToolStripButton
        Me.Tsblast = New System.Windows.Forms.ToolStripButton
        Me.Ts_report.SuspendLayout()
        Me.SuspendLayout()
        '
        'cryViewer
        '
        Me.cryViewer.ActiveViewIndex = -1
        Me.cryViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cryViewer.DisplayGroupTree = False
        Me.cryViewer.DisplayToolbar = False
        Me.cryViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cryViewer.Location = New System.Drawing.Point(0, 0)
        Me.cryViewer.Name = "cryViewer"
        'Me.cryViewer.SelectionFormula = ""
        Me.cryViewer.Size = New System.Drawing.Size(892, 516)
        Me.cryViewer.TabIndex = 0
        'Me.cryViewer.ViewTimeSelectionFormula = ""
        '
        'Ts_report
        '
        Me.Ts_report.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsbExport, Me.TsbPrint, Me.TsbFirstPage, Me.Tsbprevious, Me.Tspnext, Me.Tsblast})
        Me.Ts_report.Location = New System.Drawing.Point(0, 0)
        Me.Ts_report.Name = "Ts_report"
        Me.Ts_report.Size = New System.Drawing.Size(892, 25)
        Me.Ts_report.TabIndex = 1
        Me.Ts_report.Text = "ToolStrip1"
        '
        'TsbExport
        '
        Me.TsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbExport.Image = CType(resources.GetObject("TsbExport.Image"), System.Drawing.Image)
        Me.TsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbExport.Name = "TsbExport"
        Me.TsbExport.Size = New System.Drawing.Size(23, 22)
        Me.TsbExport.Text = "Export"
        Me.TsbExport.ToolTipText = "Export"
        '
        'TsbPrint
        '
        Me.TsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbPrint.Image = CType(resources.GetObject("TsbPrint.Image"), System.Drawing.Image)
        Me.TsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbPrint.Name = "TsbPrint"
        Me.TsbPrint.Size = New System.Drawing.Size(23, 22)
        Me.TsbPrint.Text = "Print"
        '
        'TsbFirstPage
        '
        Me.TsbFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbFirstPage.Image = CType(resources.GetObject("TsbFirstPage.Image"), System.Drawing.Image)
        Me.TsbFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbFirstPage.Name = "TsbFirstPage"
        Me.TsbFirstPage.Size = New System.Drawing.Size(23, 22)
        Me.TsbFirstPage.Text = "First Page"
        '
        'Tsbprevious
        '
        Me.Tsbprevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Tsbprevious.Image = CType(resources.GetObject("Tsbprevious.Image"), System.Drawing.Image)
        Me.Tsbprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsbprevious.Name = "Tsbprevious"
        Me.Tsbprevious.Size = New System.Drawing.Size(23, 22)
        Me.Tsbprevious.Text = "Previous"
        '
        'Tspnext
        '
        Me.Tspnext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Tspnext.Image = CType(resources.GetObject("Tspnext.Image"), System.Drawing.Image)
        Me.Tspnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tspnext.Name = "Tspnext"
        Me.Tspnext.Size = New System.Drawing.Size(23, 22)
        Me.Tspnext.Text = "Next"
        '
        'Tsblast
        '
        Me.Tsblast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Tsblast.Image = CType(resources.GetObject("Tsblast.Image"), System.Drawing.Image)
        Me.Tsblast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsblast.Name = "Tsblast"
        Me.Tsblast.Size = New System.Drawing.Size(23, 22)
        Me.Tsblast.Text = "Last Page"
        '
        'frm_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 516)
        Me.Controls.Add(Me.Ts_report)
        Me.Controls.Add(Me.cryViewer)
        Me.Name = "frm_Report"
        Me.Text = "Report"
        Me.Ts_report.ResumeLayout(False)
        Me.Ts_report.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cryViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Ts_report As System.Windows.Forms.ToolStrip
    Friend WithEvents TsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsbFirstPage As System.Windows.Forms.ToolStripButton
    Friend WithEvents Tsbprevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents Tspnext As System.Windows.Forms.ToolStripButton
    Friend WithEvents Tsblast As System.Windows.Forms.ToolStripButton
End Class
