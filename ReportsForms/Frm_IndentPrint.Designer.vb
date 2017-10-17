<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IndentPrint
    Inherits System.Windows.Forms.UserControl

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
        Me.BtnShow = New System.Windows.Forms.Button()
        Me.lbl_Item = New System.Windows.Forms.Label()
        Me.cmb_Item = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'BtnShow
        '
        Me.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnShow.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShow.ForeColor = System.Drawing.Color.White
        Me.BtnShow.Location = New System.Drawing.Point(497, 11)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(108, 31)
        Me.BtnShow.TabIndex = 19
        Me.BtnShow.Text = "Show"
        Me.BtnShow.UseVisualStyleBackColor = True
        '
        'lbl_Item
        '
        Me.lbl_Item.AutoSize = True
        Me.lbl_Item.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Item.ForeColor = System.Drawing.Color.White
        Me.lbl_Item.Location = New System.Drawing.Point(64, 16)
        Me.lbl_Item.Name = "lbl_Item"
        Me.lbl_Item.Size = New System.Drawing.Size(61, 16)
        Me.lbl_Item.TabIndex = 15
        Me.lbl_Item.Text = "Select Item :"
        '
        'cmb_Item
        '
        Me.cmb_Item.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_Item.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_Item.ForeColor = System.Drawing.Color.White
        Me.cmb_Item.FormattingEnabled = True
        Me.cmb_Item.Location = New System.Drawing.Point(160, 13)
        Me.cmb_Item.Name = "cmb_Item"
        Me.cmb_Item.Size = New System.Drawing.Size(278, 24)
        Me.cmb_Item.TabIndex = 20
        '
        'Frm_IndentPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.cmb_Item)
        Me.Controls.Add(Me.BtnShow)
        Me.Controls.Add(Me.lbl_Item)
        Me.Name = "Frm_IndentPrint"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnShow As System.Windows.Forms.Button
    Friend WithEvents lbl_Item As System.Windows.Forms.Label
    Friend WithEvents cmb_Item As System.Windows.Forms.ComboBox
End Class
