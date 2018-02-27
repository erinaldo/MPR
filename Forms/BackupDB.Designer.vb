<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BackupDB
    Inherits System.Windows.Forms.UserControl

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BackupDB))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.lblOnlineBackupStatus = New System.Windows.Forms.Label()
        Me.lblCurrentBackupDetail = New System.Windows.Forms.Label()
        Me.lblLastBackupDetail = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btnBackupDb = New System.Windows.Forms.Button()
        Me.FgrdBykMaster = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        CType(Me.FgrdBykMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Controls.Add(Me.FgrdBykMaster)
        Me.TabPage1.Controls.Add(Me.progressBar)
        Me.TabPage1.Controls.Add(Me.btn_Close)
        Me.TabPage1.Controls.Add(Me.btnBackupDb)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.ImageIndex = 1
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(953, 620)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblOnlineBackupStatus)
        Me.GroupBox1.Controls.Add(Me.lblCurrentBackupDetail)
        Me.GroupBox1.Controls.Add(Me.lblLastBackupDetail)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(3, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(946, 326)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(730, 17)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(200, 25)
        Me.lblFormHeading.TabIndex = 29
        Me.lblFormHeading.Text = "SYSTEM BACKUP"
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(961, 650)
        Me.TabControl1.TabIndex = 1
        '
        'lblOnlineBackupStatus
        '
        Me.lblOnlineBackupStatus.AutoSize = True
        Me.lblOnlineBackupStatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOnlineBackupStatus.ForeColor = System.Drawing.Color.White
        Me.lblOnlineBackupStatus.Location = New System.Drawing.Point(67, 36)
        Me.lblOnlineBackupStatus.Name = "lblOnlineBackupStatus"
        Me.lblOnlineBackupStatus.Size = New System.Drawing.Size(212, 13)
        Me.lblOnlineBackupStatus.TabIndex = 32
        Me.lblOnlineBackupStatus.Text = "Online Backup Status : Disabled"
        '
        'lblCurrentBackupDetail
        '
        Me.lblCurrentBackupDetail.AutoSize = True
        Me.lblCurrentBackupDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentBackupDetail.ForeColor = System.Drawing.Color.White
        Me.lblCurrentBackupDetail.Location = New System.Drawing.Point(67, 278)
        Me.lblCurrentBackupDetail.Name = "lblCurrentBackupDetail"
        Me.lblCurrentBackupDetail.Size = New System.Drawing.Size(157, 13)
        Me.lblCurrentBackupDetail.TabIndex = 31
        Me.lblCurrentBackupDetail.Text = "Current Backup Detail :"
        Me.lblCurrentBackupDetail.Visible = False
        '
        'lblLastBackupDetail
        '
        Me.lblLastBackupDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblLastBackupDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBackupDetail.ForeColor = System.Drawing.Color.White
        Me.lblLastBackupDetail.Location = New System.Drawing.Point(67, 64)
        Me.lblLastBackupDetail.Name = "lblLastBackupDetail"
        Me.lblLastBackupDetail.Size = New System.Drawing.Size(812, 208)
        Me.lblLastBackupDetail.TabIndex = 30
        Me.lblLastBackupDetail.Text = "Last Backup Detail :"
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(226, 354)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(519, 33)
        Me.progressBar.TabIndex = 167
        '
        'btn_Close
        '
        Me.btn_Close.BackColor = System.Drawing.Color.Red
        Me.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btn_Close.FlatAppearance.BorderSize = 0
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Close.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.ForeColor = System.Drawing.Color.White
        Me.btn_Close.Location = New System.Drawing.Point(768, 348)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(130, 44)
        Me.btn_Close.TabIndex = 166
        Me.btn_Close.Text = "Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'btnBackupDb
        '
        Me.btnBackupDb.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBackupDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBackupDb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnBackupDb.FlatAppearance.BorderSize = 0
        Me.btnBackupDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackupDb.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackupDb.ForeColor = System.Drawing.Color.White
        Me.btnBackupDb.Location = New System.Drawing.Point(55, 348)
        Me.btnBackupDb.Name = "btnBackupDb"
        Me.btnBackupDb.Size = New System.Drawing.Size(150, 44)
        Me.btnBackupDb.TabIndex = 165
        Me.btnBackupDb.Text = "Start Backup"
        Me.btnBackupDb.UseVisualStyleBackColor = False
        '
        'FgrdBykMaster
        '
        Me.FgrdBykMaster.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.FgrdBykMaster.AllowEditing = False
        Me.FgrdBykMaster.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.FgrdBykMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.FgrdBykMaster.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:25;Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.FgrdBykMaster.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.FgrdBykMaster.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FgrdBykMaster.Location = New System.Drawing.Point(55, 406)
        Me.FgrdBykMaster.Name = "FgrdBykMaster"
        Me.FgrdBykMaster.Rows.Count = 1
        Me.FgrdBykMaster.Rows.DefaultSize = 18
        Me.FgrdBykMaster.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.FgrdBykMaster.ShowCellLabels = True
        Me.FgrdBykMaster.Size = New System.Drawing.Size(843, 199)
        Me.FgrdBykMaster.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FgrdBykMaster.Styles"))
        Me.FgrdBykMaster.TabIndex = 168
        '
        'BackupDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "BackupDB"
        Me.Size = New System.Drawing.Size(961, 650)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        CType(Me.FgrdBykMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblFormHeading As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents lblOnlineBackupStatus As Label
    Friend WithEvents lblCurrentBackupDetail As Label
    Friend WithEvents lblLastBackupDetail As Label
    Friend WithEvents FgrdBykMaster As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents progressBar As ProgressBar
    Friend WithEvents btn_Close As Button
    Friend WithEvents btnBackupDb As Button
End Class
