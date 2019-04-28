<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Licence
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Licence))
        Me.lblLicnceSMS = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRenewSettings = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblExpireDate = New System.Windows.Forms.Label()
        Me.pnlRenew = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRenew = New System.Windows.Forms.Button()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pnlRenew.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblLicnceSMS
        '
        Me.lblLicnceSMS.AutoSize = True
        Me.lblLicnceSMS.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicnceSMS.ForeColor = System.Drawing.Color.White
        Me.lblLicnceSMS.Location = New System.Drawing.Point(31, 25)
        Me.lblLicnceSMS.Name = "lblLicnceSMS"
        Me.lblLicnceSMS.Size = New System.Drawing.Size(443, 25)
        Me.lblLicnceSMS.TabIndex = 0
        Me.lblLicnceSMS.Text = "Your Softwares License Will Expire Soon"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(35, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(381, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "You need to activate Softwares License in RENEW settings"
        '
        'btnRenewSettings
        '
        Me.btnRenewSettings.BackColor = System.Drawing.Color.DimGray
        Me.btnRenewSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime
        Me.btnRenewSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRenewSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRenewSettings.ForeColor = System.Drawing.Color.White
        Me.btnRenewSettings.Location = New System.Drawing.Point(506, 126)
        Me.btnRenewSettings.Name = "btnRenewSettings"
        Me.btnRenewSettings.Size = New System.Drawing.Size(160, 28)
        Me.btnRenewSettings.TabIndex = 2
        Me.btnRenewSettings.Text = "Go to RENEW Settings"
        Me.btnRenewSettings.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(685, 126)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 28)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblExpireDate
        '
        Me.lblExpireDate.AutoSize = True
        Me.lblExpireDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpireDate.ForeColor = System.Drawing.Color.White
        Me.lblExpireDate.Location = New System.Drawing.Point(35, 66)
        Me.lblExpireDate.Name = "lblExpireDate"
        Me.lblExpireDate.Size = New System.Drawing.Size(208, 18)
        Me.lblExpireDate.TabIndex = 4
        Me.lblExpireDate.Text = "Expire On : 30 - Apr - 2019"
        '
        'pnlRenew
        '
        Me.pnlRenew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRenew.Controls.Add(Me.Label4)
        Me.pnlRenew.Controls.Add(Me.Label3)
        Me.pnlRenew.Controls.Add(Me.btnExit)
        Me.pnlRenew.Controls.Add(Me.btnRenew)
        Me.pnlRenew.Controls.Add(Me.txtKey)
        Me.pnlRenew.Controls.Add(Me.Label1)
        Me.pnlRenew.Location = New System.Drawing.Point(12, 12)
        Me.pnlRenew.Name = "pnlRenew"
        Me.pnlRenew.Size = New System.Drawing.Size(776, 151)
        Me.pnlRenew.TabIndex = 5
        Me.pnlRenew.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(16, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(319, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Activation will register the product key to this software"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(16, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Product Key:"
        '
        'btnExit
        '
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(679, 115)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 26)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnRenew
        '
        Me.btnRenew.BackColor = System.Drawing.Color.Gray
        Me.btnRenew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime
        Me.btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRenew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRenew.ForeColor = System.Drawing.Color.White
        Me.btnRenew.Location = New System.Drawing.Point(545, 115)
        Me.btnRenew.Name = "btnRenew"
        Me.btnRenew.Size = New System.Drawing.Size(120, 26)
        Me.btnRenew.TabIndex = 2
        Me.btnRenew.Text = "Renew Licence"
        Me.btnRenew.UseVisualStyleBackColor = False
        '
        'txtKey
        '
        Me.txtKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKey.Location = New System.Drawing.Point(122, 82)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(387, 22)
        Me.txtKey.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Type your product key"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button1.BackgroundImage = Global.MMSPlus.My.Resources.Resources.mms_
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Enabled = False
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(685, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 40)
        Me.Button1.TabIndex = 6
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Licence
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(800, 175)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pnlRenew)
        Me.Controls.Add(Me.lblExpireDate)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnRenewSettings)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblLicnceSMS)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Licence"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Licence"
        Me.TopMost = True
        Me.pnlRenew.ResumeLayout(False)
        Me.pnlRenew.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblLicnceSMS As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnRenewSettings As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents lblExpireDate As Label
    Friend WithEvents pnlRenew As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents btnRenew As Button
    Friend WithEvents txtKey As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
End Class
