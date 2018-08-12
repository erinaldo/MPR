<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Synchronization
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Synchronization))
        Me.btnSynchronize = New System.Windows.Forms.Button()
        Me.lblProgressDetail = New System.Windows.Forms.Label()
        Me.TmrForProgressBar = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PbarDataTransfer = New System.Windows.Forms.ProgressBar()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btnQuickSync = New System.Windows.Forms.Button()
        Me.txtBxProgressDetail = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnSynchronize
        '
        Me.btnSynchronize.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSynchronize.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSynchronize.ForeColor = System.Drawing.Color.White
        Me.btnSynchronize.Location = New System.Drawing.Point(12, 22)
        Me.btnSynchronize.Name = "btnSynchronize"
        Me.btnSynchronize.Size = New System.Drawing.Size(312, 50)
        Me.btnSynchronize.TabIndex = 0
        Me.btnSynchronize.Text = "Synchronize All..."
        Me.btnSynchronize.UseVisualStyleBackColor = False
        '
        'lblProgressDetail
        '
        Me.lblProgressDetail.AutoSize = True
        Me.lblProgressDetail.Location = New System.Drawing.Point(12, 101)
        Me.lblProgressDetail.Name = "lblProgressDetail"
        Me.lblProgressDetail.Size = New System.Drawing.Size(0, 13)
        Me.lblProgressDetail.TabIndex = 1
        '
        'TmrForProgressBar
        '
        Me.TmrForProgressBar.Enabled = True
        Me.TmrForProgressBar.Interval = 1000
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'PbarDataTransfer
        '
        Me.PbarDataTransfer.Location = New System.Drawing.Point(11, 78)
        Me.PbarDataTransfer.Name = "PbarDataTransfer"
        Me.PbarDataTransfer.Size = New System.Drawing.Size(312, 38)
        Me.PbarDataTransfer.TabIndex = 2
        '
        'btn_Cancel
        '
        Me.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.White
        Me.btn_Cancel.Location = New System.Drawing.Point(235, 322)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(86, 25)
        Me.btn_Cancel.TabIndex = 5
        Me.btn_Cancel.Text = "Close"
        Me.btn_Cancel.UseVisualStyleBackColor = False
        '
        'btnQuickSync
        '
        Me.btnQuickSync.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnQuickSync.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuickSync.ForeColor = System.Drawing.Color.White
        Me.btnQuickSync.Location = New System.Drawing.Point(15, 79)
        Me.btnQuickSync.Name = "btnQuickSync"
        Me.btnQuickSync.Size = New System.Drawing.Size(309, 36)
        Me.btnQuickSync.TabIndex = 6
        Me.btnQuickSync.Text = "Quick Synchronize..."
        Me.btnQuickSync.UseVisualStyleBackColor = False
        Me.btnQuickSync.Visible = False
        '
        'txtBxProgressDetail
        '
        Me.txtBxProgressDetail.Location = New System.Drawing.Point(12, 122)
        Me.txtBxProgressDetail.Multiline = True
        Me.txtBxProgressDetail.Name = "txtBxProgressDetail"
        Me.txtBxProgressDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBxProgressDetail.Size = New System.Drawing.Size(309, 192)
        Me.txtBxProgressDetail.TabIndex = 7
        '
        'frm_Synchronization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(333, 357)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtBxProgressDetail)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.PbarDataTransfer)
        Me.Controls.Add(Me.lblProgressDetail)
        Me.Controls.Add(Me.btnSynchronize)
        Me.Controls.Add(Me.btnQuickSync)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_Synchronization"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSynchronize As System.Windows.Forms.Button
    Friend WithEvents lblProgressDetail As System.Windows.Forms.Label
    Friend WithEvents TmrForProgressBar As System.Windows.Forms.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PbarDataTransfer As System.Windows.Forms.ProgressBar
    Friend WithEvents btn_Cancel As Button
    Friend WithEvents btnQuickSync As Button
    Friend WithEvents txtBxProgressDetail As TextBox
End Class
