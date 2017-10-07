<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ItemStockable_CC_List
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ItemStockable_CC_List))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.FLXGRID_CC = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.FLXGRID_CC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btn_Save)
        Me.Panel1.Controls.Add(Me.FLXGRID_CC)
        Me.Panel1.Controls.Add(Me.btn_Cancel)
        Me.Panel1.Location = New System.Drawing.Point(3, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(629, 286)
        Me.Panel1.TabIndex = 3
        '
        'btn_Save
        '
        Me.btn_Save.Location = New System.Drawing.Point(464, 252)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(75, 23)
        Me.btn_Save.TabIndex = 2
        Me.btn_Save.Text = "OK"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'FLXGRID_CC
        '
        Me.FLXGRID_CC.BackColor = System.Drawing.Color.Silver
        Me.FLXGRID_CC.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.FLXGRID_CC.Location = New System.Drawing.Point(6, 8)
        Me.FLXGRID_CC.Name = "FLXGRID_CC"
        Me.FLXGRID_CC.Rows.Count = 1
        Me.FLXGRID_CC.Rows.DefaultSize = 17
        Me.FLXGRID_CC.Size = New System.Drawing.Size(612, 238)
        Me.FLXGRID_CC.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLXGRID_CC.Styles"))
        Me.FLXGRID_CC.TabIndex = 1
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Location = New System.Drawing.Point(545, 252)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_Cancel.TabIndex = 0
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frm_ItemStockable_CC_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(636, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frm_ItemStockable_CC_List"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        CType(Me.FLXGRID_CC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents FLXGRID_CC As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btn_Save As System.Windows.Forms.Button
End Class
