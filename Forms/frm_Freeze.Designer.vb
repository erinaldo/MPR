<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Freeze
    Inherits System.Windows.Forms.Form

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Freeze))
        Me.lblProgressDetail = New System.Windows.Forms.Label()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbllastFreezeDate = New System.Windows.Forms.Label()
        Me.dtp_Date = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnFreeze = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblProgressDetail
        '
        Me.lblProgressDetail.AutoSize = True
        Me.lblProgressDetail.Location = New System.Drawing.Point(12, 101)
        Me.lblProgressDetail.Name = "lblProgressDetail"
        Me.lblProgressDetail.Size = New System.Drawing.Size(0, 13)
        Me.lblProgressDetail.TabIndex = 1
        '
        'btn_Cancel
        '
        Me.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.White
        Me.btn_Cancel.Location = New System.Drawing.Point(233, 317)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(88, 30)
        Me.btn_Cancel.TabIndex = 5
        Me.btn_Cancel.Text = "Close"
        Me.btn_Cancel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Consolas", 20.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(58, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(210, 32)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Freeze System"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(12, 233)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(308, 56)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "NOTE :- Once the Freeze date is defined " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "after that System will not allow any ki" &
    "nd " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "of alteration to the existing tranactional " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "data."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbllastFreezeDate
        '
        Me.lbllastFreezeDate.AutoSize = True
        Me.lbllastFreezeDate.BackColor = System.Drawing.Color.Transparent
        Me.lbllastFreezeDate.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllastFreezeDate.ForeColor = System.Drawing.Color.Honeydew
        Me.lbllastFreezeDate.Location = New System.Drawing.Point(61, 125)
        Me.lbllastFreezeDate.Name = "lbllastFreezeDate"
        Me.lbllastFreezeDate.Size = New System.Drawing.Size(96, 17)
        Me.lbllastFreezeDate.TabIndex = 34
        Me.lbllastFreezeDate.Text = "Freeze Date"
        '
        'dtp_Date
        '
        Me.dtp_Date.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_Date.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_Date.CustomFormat = "dd-MMM-yyyy"
        Me.dtp_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dtp_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_Date.Location = New System.Drawing.Point(127, 84)
        Me.dtp_Date.Name = "dtp_Date"
        Me.dtp_Date.Size = New System.Drawing.Size(115, 23)
        Me.dtp_Date.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Georgia", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Honeydew
        Me.Label3.Location = New System.Drawing.Point(61, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 17)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Set Date"
        '
        'btnFreeze
        '
        Me.btnFreeze.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFreeze.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFreeze.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.btnFreeze.FlatAppearance.BorderSize = 0
        Me.btnFreeze.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnFreeze.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFreeze.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFreeze.ForeColor = System.Drawing.Color.White
        Me.btnFreeze.Location = New System.Drawing.Point(19, 159)
        Me.btnFreeze.Name = "btnFreeze"
        Me.btnFreeze.Size = New System.Drawing.Size(292, 54)
        Me.btnFreeze.TabIndex = 31
        Me.btnFreeze.Text = "Freeze"
        Me.btnFreeze.UseVisualStyleBackColor = False
        '
        'frm_Freeze
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(333, 357)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbllastFreezeDate)
        Me.Controls.Add(Me.dtp_Date)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnFreeze)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.lblProgressDetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_Freeze"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblProgressDetail As System.Windows.Forms.Label
    Friend WithEvents btn_Cancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbllastFreezeDate As Label
    Friend WithEvents dtp_Date As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btnFreeze As Button
End Class
