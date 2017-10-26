<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Gate_Pass
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tb_list = New System.Windows.Forms.TabPage
        Me.tb_detail = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TabControl1.SuspendLayout()
        Me.tb_detail.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tb_list)
        Me.TabControl1.Controls.Add(Me.tb_detail)
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(877, 603)
        Me.TabControl1.TabIndex = 0
        '
        'tb_list
        '
        Me.tb_list.Location = New System.Drawing.Point(4, 22)
        Me.tb_list.Name = "tb_list"
        Me.tb_list.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_list.Size = New System.Drawing.Size(869, 577)
        Me.tb_list.TabIndex = 0
        Me.tb_list.Text = "List"
        Me.tb_list.UseVisualStyleBackColor = True
        '
        'tb_detail
        '
        Me.tb_detail.Controls.Add(Me.GroupBox1)
        Me.tb_detail.Location = New System.Drawing.Point(4, 22)
        Me.tb_detail.Name = "tb_detail"
        Me.tb_detail.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_detail.Size = New System.Drawing.Size(869, 577)
        Me.tb_detail.TabIndex = 1
        Me.tb_detail.Text = "Detail"
        Me.tb_detail.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(857, 215)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Gate Pass Detail"
        '
        'frm_Gate_Pass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Gate_Pass"
        Me.Size = New System.Drawing.Size(900, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.tb_detail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tb_list As System.Windows.Forms.TabPage
    Friend WithEvents tb_detail As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
