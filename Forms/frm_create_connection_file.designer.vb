<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_create_connection_file
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_create_connection_file))
        Me.Submit = New System.Windows.Forms.Button()
        Me.txtLocalTimeOut = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbLocalServer = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLocalPassword = New System.Windows.Forms.TextBox()
        Me.txtLocalUserName = New System.Windows.Forms.TextBox()
        Me.txtLocalDatabase = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOnlinePassword = New System.Windows.Forms.TextBox()
        Me.txtOnlineUserName = New System.Windows.Forms.TextBox()
        Me.txtOnlineTimeOut = New System.Windows.Forms.TextBox()
        Me.txtOnlineServer = New System.Windows.Forms.TextBox()
        Me.txtOnlineDatabase = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Submit
        '
        Me.Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Submit.Location = New System.Drawing.Point(563, 193)
        Me.Submit.Name = "Submit"
        Me.Submit.Size = New System.Drawing.Size(123, 23)
        Me.Submit.TabIndex = 6
        Me.Submit.Text = "Save Settings"
        Me.Submit.UseVisualStyleBackColor = True
        '
        'txtLocalTimeOut
        '
        Me.txtLocalTimeOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLocalTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocalTimeOut.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalTimeOut.ForeColor = System.Drawing.Color.White
        Me.txtLocalTimeOut.Location = New System.Drawing.Point(90, 85)
        Me.txtLocalTimeOut.Name = "txtLocalTimeOut"
        Me.txtLocalTimeOut.Size = New System.Drawing.Size(225, 15)
        Me.txtLocalTimeOut.TabIndex = 5
        Me.txtLocalTimeOut.Text = "100"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "DataBase :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "TimeOut :"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbLocalServer)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtLocalPassword)
        Me.GroupBox1.Controls.Add(Me.txtLocalUserName)
        Me.GroupBox1.Controls.Add(Me.txtLocalDatabase)
        Me.GroupBox1.Controls.Add(Me.txtLocalTimeOut)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(334, 175)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Connect to Local Database"
        '
        'cmbLocalServer
        '
        Me.cmbLocalServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbLocalServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbLocalServer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLocalServer.ForeColor = System.Drawing.Color.White
        Me.cmbLocalServer.FormattingEnabled = True
        Me.cmbLocalServer.Location = New System.Drawing.Point(90, 26)
        Me.cmbLocalServer.Name = "cmbLocalServer"
        Me.cmbLocalServer.Size = New System.Drawing.Size(225, 24)
        Me.cmbLocalServer.TabIndex = 8
        Me.cmbLocalServer.Text = "inderjeet\sqlexpress"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Password :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "User Name :"
        '
        'txtLocalPassword
        '
        Me.txtLocalPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLocalPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocalPassword.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalPassword.ForeColor = System.Drawing.Color.White
        Me.txtLocalPassword.Location = New System.Drawing.Point(90, 137)
        Me.txtLocalPassword.Name = "txtLocalPassword"
        Me.txtLocalPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtLocalPassword.Size = New System.Drawing.Size(225, 15)
        Me.txtLocalPassword.TabIndex = 5
        Me.txtLocalPassword.Text = "DataBase"
        '
        'txtLocalUserName
        '
        Me.txtLocalUserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLocalUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocalUserName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalUserName.ForeColor = System.Drawing.Color.White
        Me.txtLocalUserName.Location = New System.Drawing.Point(90, 111)
        Me.txtLocalUserName.Name = "txtLocalUserName"
        Me.txtLocalUserName.Size = New System.Drawing.Size(225, 15)
        Me.txtLocalUserName.TabIndex = 5
        Me.txtLocalUserName.Text = "sa"
        '
        'txtLocalDatabase
        '
        Me.txtLocalDatabase.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLocalDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocalDatabase.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalDatabase.ForeColor = System.Drawing.Color.White
        Me.txtLocalDatabase.Location = New System.Drawing.Point(90, 58)
        Me.txtLocalDatabase.Name = "txtLocalDatabase"
        Me.txtLocalDatabase.Size = New System.Drawing.Size(225, 15)
        Me.txtLocalDatabase.TabIndex = 5
        Me.txtLocalDatabase.Text = "afbl_mms"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtOnlinePassword)
        Me.GroupBox2.Controls.Add(Me.txtOnlineUserName)
        Me.GroupBox2.Controls.Add(Me.txtOnlineTimeOut)
        Me.GroupBox2.Controls.Add(Me.txtOnlineServer)
        Me.GroupBox2.Controls.Add(Me.txtOnlineDatabase)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(352, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(334, 175)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Connect to Online Database"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 139)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Password :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(17, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "User Name :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(17, 87)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "TimeOut :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(17, 58)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "DataBase :"
        '
        'txtOnlinePassword
        '
        Me.txtOnlinePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOnlinePassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOnlinePassword.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnlinePassword.ForeColor = System.Drawing.Color.White
        Me.txtOnlinePassword.Location = New System.Drawing.Point(95, 137)
        Me.txtOnlinePassword.Name = "txtOnlinePassword"
        Me.txtOnlinePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOnlinePassword.Size = New System.Drawing.Size(222, 15)
        Me.txtOnlinePassword.TabIndex = 5
        Me.txtOnlinePassword.Text = "afbl_cp"
        '
        'txtOnlineUserName
        '
        Me.txtOnlineUserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOnlineUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOnlineUserName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnlineUserName.ForeColor = System.Drawing.Color.White
        Me.txtOnlineUserName.Location = New System.Drawing.Point(95, 111)
        Me.txtOnlineUserName.Name = "txtOnlineUserName"
        Me.txtOnlineUserName.Size = New System.Drawing.Size(222, 15)
        Me.txtOnlineUserName.TabIndex = 5
        Me.txtOnlineUserName.Text = "afbl_cp"
        '
        'txtOnlineTimeOut
        '
        Me.txtOnlineTimeOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOnlineTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOnlineTimeOut.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnlineTimeOut.ForeColor = System.Drawing.Color.White
        Me.txtOnlineTimeOut.Location = New System.Drawing.Point(95, 85)
        Me.txtOnlineTimeOut.Name = "txtOnlineTimeOut"
        Me.txtOnlineTimeOut.Size = New System.Drawing.Size(222, 15)
        Me.txtOnlineTimeOut.TabIndex = 5
        Me.txtOnlineTimeOut.Text = "100"
        '
        'txtOnlineServer
        '
        Me.txtOnlineServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOnlineServer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOnlineServer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnlineServer.ForeColor = System.Drawing.Color.White
        Me.txtOnlineServer.Location = New System.Drawing.Point(95, 27)
        Me.txtOnlineServer.Name = "txtOnlineServer"
        Me.txtOnlineServer.Size = New System.Drawing.Size(222, 15)
        Me.txtOnlineServer.TabIndex = 5
        Me.txtOnlineServer.Text = "republicofchicken.com\sql2005"
        '
        'txtOnlineDatabase
        '
        Me.txtOnlineDatabase.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOnlineDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOnlineDatabase.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnlineDatabase.ForeColor = System.Drawing.Color.White
        Me.txtOnlineDatabase.Location = New System.Drawing.Point(95, 56)
        Me.txtOnlineDatabase.Name = "txtOnlineDatabase"
        Me.txtOnlineDatabase.Size = New System.Drawing.Size(222, 15)
        Me.txtOnlineDatabase.TabIndex = 5
        Me.txtOnlineDatabase.Text = "AFBL_CentralizePos"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Server Name :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Orange
        Me.Label11.Location = New System.Drawing.Point(35, 198)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(170, 13)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Note : All fields are required."
        '
        'btn_Cancel
        '
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Location = New System.Drawing.Point(482, 193)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_Cancel.TabIndex = 9
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frm_create_connection_file
        '
        Me.AcceptButton = Me.Submit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(698, 225)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Submit)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_create_connection_file"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Submit As System.Windows.Forms.Button
    Friend WithEvents txtLocalTimeOut As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbLocalServer As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLocalUserName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtOnlinePassword As System.Windows.Forms.TextBox
    Friend WithEvents txtOnlineUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtOnlineTimeOut As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLocalPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtOnlineServer As System.Windows.Forms.TextBox
    Friend WithEvents txtOnlineDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btn_Cancel As Button
End Class
