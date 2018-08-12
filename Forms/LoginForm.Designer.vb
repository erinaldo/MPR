<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.rdoMainKitchen = New System.Windows.Forms.RadioButton()
        Me.rdoCostCenter = New System.Windows.Forms.RadioButton()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.OK = New System.Windows.Forms.Button()
        Me.UsernameTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'rdoMainKitchen
        '
        Me.rdoMainKitchen.BackColor = System.Drawing.Color.Transparent
        Me.rdoMainKitchen.Checked = True
        Me.rdoMainKitchen.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed
        Me.rdoMainKitchen.FlatAppearance.BorderSize = 10
        Me.rdoMainKitchen.FlatAppearance.CheckedBackColor = System.Drawing.Color.OrangeRed
        Me.rdoMainKitchen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black
        Me.rdoMainKitchen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rdoMainKitchen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoMainKitchen.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoMainKitchen.ForeColor = System.Drawing.Color.Tomato
        Me.rdoMainKitchen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rdoMainKitchen.Location = New System.Drawing.Point(273, 182)
        Me.rdoMainKitchen.Name = "rdoMainKitchen"
        Me.rdoMainKitchen.Size = New System.Drawing.Size(235, 50)
        Me.rdoMainKitchen.TabIndex = 8
        Me.rdoMainKitchen.TabStop = True
        Me.rdoMainKitchen.Text = "Main Store"
        Me.rdoMainKitchen.UseVisualStyleBackColor = False
        Me.rdoMainKitchen.Visible = False
        '
        'rdoCostCenter
        '
        Me.rdoCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.rdoCostCenter.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed
        Me.rdoCostCenter.FlatAppearance.BorderSize = 10
        Me.rdoCostCenter.FlatAppearance.CheckedBackColor = System.Drawing.Color.OrangeRed
        Me.rdoCostCenter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black
        Me.rdoCostCenter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rdoCostCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoCostCenter.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoCostCenter.ForeColor = System.Drawing.Color.Tomato
        Me.rdoCostCenter.Location = New System.Drawing.Point(606, 187)
        Me.rdoCostCenter.Name = "rdoCostCenter"
        Me.rdoCostCenter.Size = New System.Drawing.Size(227, 40)
        Me.rdoCostCenter.TabIndex = 9
        Me.rdoCostCenter.Text = "Cost Center"
        Me.rdoCostCenter.UseVisualStyleBackColor = False
        Me.rdoCostCenter.Visible = False
        '
        'Cancel
        '
        Me.Cancel.BackColor = System.Drawing.Color.Transparent
        Me.Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Cancel.Image = Global.MMSPlus.My.Resources.Resources.logout
        Me.Cancel.Location = New System.Drawing.Point(973, 7)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(40, 38)
        Me.Cancel.TabIndex = 5
        Me.Cancel.UseVisualStyleBackColor = False
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.cmbCostCenter.Enabled = False
        Me.cmbCostCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCostCenter.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCostCenter.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.cmbCostCenter.Location = New System.Drawing.Point(476, 238)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(282, 35)
        Me.cmbCostCenter.TabIndex = 10
        Me.cmbCostCenter.Visible = False
        '
        'UsernameLabel
        '
        Me.UsernameLabel.BackColor = System.Drawing.Color.Transparent
        Me.UsernameLabel.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.ForeColor = System.Drawing.Color.Silver
        Me.UsernameLabel.Location = New System.Drawing.Point(269, 279)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(153, 23)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "&User name"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordLabel
        '
        Me.PasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.PasswordLabel.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.ForeColor = System.Drawing.Color.Silver
        Me.PasswordLabel.Location = New System.Drawing.Point(269, 388)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(142, 23)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Password"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OK
        '
        Me.OK.BackColor = System.Drawing.Color.Silver
        Me.OK.BackgroundImage = Global.MMSPlus.My.Resources.Resources.MMS_Login_New_17
        Me.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.OK.Location = New System.Drawing.Point(273, 515)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(485, 59)
        Me.OK.TabIndex = 4
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.OK.UseVisualStyleBackColor = False
        '
        'UsernameTextBox
        '
        Me.UsernameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.UsernameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UsernameTextBox.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameTextBox.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.UsernameTextBox.Location = New System.Drawing.Point(273, 317)
        Me.UsernameTextBox.Multiline = True
        Me.UsernameTextBox.Name = "UsernameTextBox"
        Me.UsernameTextBox.Size = New System.Drawing.Size(485, 59)
        Me.UsernameTextBox.TabIndex = 1
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PasswordTextBox.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordTextBox.ForeColor = System.Drawing.Color.White
        Me.PasswordTextBox.Location = New System.Drawing.Point(273, 426)
        Me.PasswordTextBox.Multiline = True
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(485, 59)
        Me.PasswordTextBox.TabIndex = 3
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.Silver
        Me.lbl.Location = New System.Drawing.Point(268, 235)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(141, 23)
        Me.lbl.TabIndex = 7
        Me.lbl.Text = "Cost Center"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl.Visible = False
        '
        'LoginForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.MMSPlus.My.Resources.Resources.MMS_Login_background
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(1018, 762)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbCostCenter)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UsernameTextBox)
        Me.Controls.Add(Me.rdoCostCenter)
        Me.Controls.Add(Me.rdoMainKitchen)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoMainKitchen As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCostCenter As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents lbl As System.Windows.Forms.Label

End Class
