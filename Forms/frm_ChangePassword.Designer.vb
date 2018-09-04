<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ChangePassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ChangePassword))
        Me.txt_CurrentPassword = New System.Windows.Forms.TextBox()
        Me.txt_NewPassword = New System.Windows.Forms.TextBox()
        Me.txt_ConfirmPassword = New System.Windows.Forms.TextBox()
        Me.btn_Change = New System.Windows.Forms.Button()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.txt_UserName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txt_CurrentPassword
        '
        Me.txt_CurrentPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_CurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_CurrentPassword.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_CurrentPassword.ForeColor = System.Drawing.Color.White
        Me.txt_CurrentPassword.Location = New System.Drawing.Point(150, 54)
        Me.txt_CurrentPassword.Name = "txt_CurrentPassword"
        Me.txt_CurrentPassword.Size = New System.Drawing.Size(155, 19)
        Me.txt_CurrentPassword.TabIndex = 0
        '
        'txt_NewPassword
        '
        Me.txt_NewPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_NewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_NewPassword.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NewPassword.ForeColor = System.Drawing.Color.White
        Me.txt_NewPassword.Location = New System.Drawing.Point(150, 90)
        Me.txt_NewPassword.Name = "txt_NewPassword"
        Me.txt_NewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_NewPassword.Size = New System.Drawing.Size(155, 19)
        Me.txt_NewPassword.TabIndex = 1
        '
        'txt_ConfirmPassword
        '
        Me.txt_ConfirmPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_ConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_ConfirmPassword.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ConfirmPassword.ForeColor = System.Drawing.Color.White
        Me.txt_ConfirmPassword.Location = New System.Drawing.Point(150, 125)
        Me.txt_ConfirmPassword.Name = "txt_ConfirmPassword"
        Me.txt_ConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_ConfirmPassword.Size = New System.Drawing.Size(155, 19)
        Me.txt_ConfirmPassword.TabIndex = 2
        '
        'btn_Change
        '
        Me.btn_Change.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_Change.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Change.Location = New System.Drawing.Point(109, 161)
        Me.btn_Change.Name = "btn_Change"
        Me.btn_Change.Size = New System.Drawing.Size(115, 27)
        Me.btn_Change.TabIndex = 3
        Me.btn_Change.Text = "Change Password"
        Me.btn_Change.UseVisualStyleBackColor = False
        '
        'btn_Cancel
        '
        Me.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Location = New System.Drawing.Point(232, 161)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 27)
        Me.btn_Cancel.TabIndex = 4
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = False
        '
        'txt_UserName
        '
        Me.txt_UserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_UserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_UserName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UserName.ForeColor = System.Drawing.Color.White
        Me.txt_UserName.Location = New System.Drawing.Point(150, 17)
        Me.txt_UserName.Name = "txt_UserName"
        Me.txt_UserName.ReadOnly = True
        Me.txt_UserName.Size = New System.Drawing.Size(155, 19)
        Me.txt_UserName.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "User Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Current Password :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "New Password :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(27, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 15)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Confirm Password :"
        '
        'frm_ChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(340, 200)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_UserName)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btn_Change)
        Me.Controls.Add(Me.txt_ConfirmPassword)
        Me.Controls.Add(Me.txt_NewPassword)
        Me.Controls.Add(Me.txt_CurrentPassword)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_ChangePassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_CurrentPassword As System.Windows.Forms.TextBox
    Friend WithEvents txt_NewPassword As System.Windows.Forms.TextBox
    Friend WithEvents txt_ConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents btn_Change As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents txt_UserName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
