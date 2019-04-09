<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_SetEcommerce_Vendor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_SetEcommerce_Vendor))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSupplier = New MMSPlus.AutoCompleteCombo()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Ecommerce Vendor:"
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.ForeColor = System.Drawing.Color.White
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(152, 17)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.ResetOnClear = False
        Me.cmbSupplier.Size = New System.Drawing.Size(386, 24)
        Me.cmbSupplier.TabIndex = 1
        '
        'btn_Save
        '
        Me.btn_Save.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Save.Location = New System.Drawing.Point(337, 47)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(120, 27)
        Me.btn_Save.TabIndex = 2
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = False
        '
        'btn_Cancel
        '
        Me.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Location = New System.Drawing.Point(463, 47)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 27)
        Me.btn_Cancel.TabIndex = 3
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = False
        '
        'frm_SetEcommerce_Vendor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(541, 103)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btn_Save)
        Me.Controls.Add(Me.cmbSupplier)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_SetEcommerce_Vendor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSupplier As AutoCompleteCombo
    Friend WithEvents btn_Save As Button
    Friend WithEvents btn_Cancel As Button
End Class
