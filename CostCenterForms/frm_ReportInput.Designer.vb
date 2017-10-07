<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ReportInput
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ReportInput))
        Me.gbInput = New System.Windows.Forms.GroupBox()
        Me.cmb_subCategory = New System.Windows.Forms.ComboBox()
        Me.lblSubCategory = New System.Windows.Forms.Label()
        Me.btn_ExportToExcel = New System.Windows.Forms.Button()
        Me.cmbCategoryHead = New System.Windows.Forms.ComboBox()
        Me.lblCategoryHead = New System.Windows.Forms.Label()
        Me.rBtnReqDate = New System.Windows.Forms.RadioButton()
        Me.cmbstatus = New System.Windows.Forms.ComboBox()
        Me.lblstatus = New System.Windows.Forms.Label()
        Me.rBtnMRS = New System.Windows.Forms.RadioButton()
        Me.btn_show = New System.Windows.Forms.Button()
        Me.dtp_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_FromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblfrmdate = New System.Windows.Forms.Label()
        Me.FLX_ISSUE_CAT_HEAD_WISE = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.grp_cost_of_issue = New System.Windows.Forms.GroupBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.gbInput.SuspendLayout()
        CType(Me.FLX_ISSUE_CAT_HEAD_WISE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_cost_of_issue.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbInput
        '
        Me.gbInput.Controls.Add(Me.cmb_subCategory)
        Me.gbInput.Controls.Add(Me.lblSubCategory)
        Me.gbInput.Controls.Add(Me.btn_ExportToExcel)
        Me.gbInput.Controls.Add(Me.cmbCategoryHead)
        Me.gbInput.Controls.Add(Me.lblCategoryHead)
        Me.gbInput.Controls.Add(Me.rBtnReqDate)
        Me.gbInput.Controls.Add(Me.cmbstatus)
        Me.gbInput.Controls.Add(Me.lblstatus)
        Me.gbInput.Controls.Add(Me.rBtnMRS)
        Me.gbInput.Controls.Add(Me.btn_show)
        Me.gbInput.Controls.Add(Me.dtp_ToDate)
        Me.gbInput.Controls.Add(Me.Label1)
        Me.gbInput.Controls.Add(Me.dtp_FromDate)
        Me.gbInput.Controls.Add(Me.lblfrmdate)
        Me.gbInput.ForeColor = System.Drawing.Color.White
        Me.gbInput.Location = New System.Drawing.Point(18, 27)
        Me.gbInput.Name = "gbInput"
        Me.gbInput.Size = New System.Drawing.Size(868, 207)
        Me.gbInput.TabIndex = 9
        Me.gbInput.TabStop = False
        Me.gbInput.Text = "Print Options"
        '
        'cmb_subCategory
        '
        Me.cmb_subCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_subCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_subCategory.DropDownWidth = 350
        Me.cmb_subCategory.Enabled = False
        Me.cmb_subCategory.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_subCategory.ForeColor = System.Drawing.Color.White
        Me.cmb_subCategory.FormattingEnabled = True
        Me.cmb_subCategory.Location = New System.Drawing.Point(188, 90)
        Me.cmb_subCategory.Name = "cmb_subCategory"
        Me.cmb_subCategory.Size = New System.Drawing.Size(327, 23)
        Me.cmb_subCategory.TabIndex = 32
        '
        'lblSubCategory
        '
        Me.lblSubCategory.AutoSize = True
        Me.lblSubCategory.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubCategory.Location = New System.Drawing.Point(89, 90)
        Me.lblSubCategory.Name = "lblSubCategory"
        Me.lblSubCategory.Size = New System.Drawing.Size(84, 15)
        Me.lblSubCategory.TabIndex = 31
        Me.lblSubCategory.Text = "Sub Category:"
        '
        'btn_ExportToExcel
        '
        Me.btn_ExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToExcel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToExcel.Location = New System.Drawing.Point(662, 157)
        Me.btn_ExportToExcel.Name = "btn_ExportToExcel"
        Me.btn_ExportToExcel.Size = New System.Drawing.Size(100, 31)
        Me.btn_ExportToExcel.TabIndex = 20
        Me.btn_ExportToExcel.Text = "Export Excel"
        Me.btn_ExportToExcel.UseVisualStyleBackColor = True
        Me.btn_ExportToExcel.Visible = False
        '
        'cmbCategoryHead
        '
        Me.cmbCategoryHead.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCategoryHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategoryHead.DropDownWidth = 350
        Me.cmbCategoryHead.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategoryHead.ForeColor = System.Drawing.Color.White
        Me.cmbCategoryHead.FormattingEnabled = True
        Me.cmbCategoryHead.Location = New System.Drawing.Point(188, 56)
        Me.cmbCategoryHead.Name = "cmbCategoryHead"
        Me.cmbCategoryHead.Size = New System.Drawing.Size(327, 23)
        Me.cmbCategoryHead.TabIndex = 19
        Me.cmbCategoryHead.Visible = False
        '
        'lblCategoryHead
        '
        Me.lblCategoryHead.AutoSize = True
        Me.lblCategoryHead.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategoryHead.Location = New System.Drawing.Point(89, 56)
        Me.lblCategoryHead.Name = "lblCategoryHead"
        Me.lblCategoryHead.Size = New System.Drawing.Size(95, 15)
        Me.lblCategoryHead.TabIndex = 18
        Me.lblCategoryHead.Text = "Category Head :"
        Me.lblCategoryHead.Visible = False
        '
        'rBtnReqDate
        '
        Me.rBtnReqDate.AutoSize = True
        Me.rBtnReqDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rBtnReqDate.Location = New System.Drawing.Point(299, 26)
        Me.rBtnReqDate.Name = "rBtnReqDate"
        Me.rBtnReqDate.Size = New System.Drawing.Size(98, 19)
        Me.rBtnReqDate.TabIndex = 17
        Me.rBtnReqDate.TabStop = True
        Me.rBtnReqDate.Text = "Require Date"
        Me.rBtnReqDate.UseVisualStyleBackColor = True
        '
        'cmbstatus
        '
        Me.cmbstatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbstatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbstatus.ForeColor = System.Drawing.Color.White
        Me.cmbstatus.FormattingEnabled = True
        Me.cmbstatus.Location = New System.Drawing.Point(190, 161)
        Me.cmbstatus.Name = "cmbstatus"
        Me.cmbstatus.Size = New System.Drawing.Size(121, 23)
        Me.cmbstatus.TabIndex = 16
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(89, 164)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(48, 15)
        Me.lblstatus.TabIndex = 15
        Me.lblstatus.Text = "Status :"
        '
        'rBtnMRS
        '
        Me.rBtnMRS.AutoSize = True
        Me.rBtnMRS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rBtnMRS.Location = New System.Drawing.Point(184, 26)
        Me.rBtnMRS.Name = "rBtnMRS"
        Me.rBtnMRS.Size = New System.Drawing.Size(80, 19)
        Me.rBtnMRS.TabIndex = 14
        Me.rBtnMRS.TabStop = True
        Me.rBtnMRS.Text = "MRS Date"
        Me.rBtnMRS.UseVisualStyleBackColor = True
        '
        'btn_show
        '
        Me.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_show.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_show.Location = New System.Drawing.Point(662, 90)
        Me.btn_show.Name = "btn_show"
        Me.btn_show.Size = New System.Drawing.Size(100, 31)
        Me.btn_show.TabIndex = 13
        Me.btn_show.Text = "Show"
        Me.btn_show.UseVisualStyleBackColor = True
        '
        'dtp_ToDate
        '
        Me.dtp_ToDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_ToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_ToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtp_ToDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_ToDate.Location = New System.Drawing.Point(350, 127)
        Me.dtp_ToDate.Name = "dtp_ToDate"
        Me.dtp_ToDate.Size = New System.Drawing.Size(106, 21)
        Me.dtp_ToDate.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(317, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "To "
        '
        'dtp_FromDate
        '
        Me.dtp_FromDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtp_FromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtp_FromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtp_FromDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_FromDate.Location = New System.Drawing.Point(190, 127)
        Me.dtp_FromDate.Name = "dtp_FromDate"
        Me.dtp_FromDate.Size = New System.Drawing.Size(121, 21)
        Me.dtp_FromDate.TabIndex = 11
        '
        'lblfrmdate
        '
        Me.lblfrmdate.AutoSize = True
        Me.lblfrmdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfrmdate.Location = New System.Drawing.Point(89, 127)
        Me.lblfrmdate.Name = "lblfrmdate"
        Me.lblfrmdate.Size = New System.Drawing.Size(42, 15)
        Me.lblfrmdate.TabIndex = 9
        Me.lblfrmdate.Text = "From :"
        '
        'FLX_ISSUE_CAT_HEAD_WISE
        '
        Me.FLX_ISSUE_CAT_HEAD_WISE.AllowEditing = False
        Me.FLX_ISSUE_CAT_HEAD_WISE.BackColor = System.Drawing.Color.DarkGray
        Me.FLX_ISSUE_CAT_HEAD_WISE.ColumnInfo = "10,0,1,0,0,100,Columns:"
        Me.FLX_ISSUE_CAT_HEAD_WISE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLX_ISSUE_CAT_HEAD_WISE.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FLX_ISSUE_CAT_HEAD_WISE.Location = New System.Drawing.Point(3, 17)
        Me.FLX_ISSUE_CAT_HEAD_WISE.Name = "FLX_ISSUE_CAT_HEAD_WISE"
        Me.FLX_ISSUE_CAT_HEAD_WISE.Rows.Count = 2
        Me.FLX_ISSUE_CAT_HEAD_WISE.Rows.DefaultSize = 20
        Me.FLX_ISSUE_CAT_HEAD_WISE.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.FLX_ISSUE_CAT_HEAD_WISE.Size = New System.Drawing.Size(862, 345)
        Me.FLX_ISSUE_CAT_HEAD_WISE.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLX_ISSUE_CAT_HEAD_WISE.Styles"))
        Me.FLX_ISSUE_CAT_HEAD_WISE.TabIndex = 10
        Me.FLX_ISSUE_CAT_HEAD_WISE.Visible = False
        '
        'grp_cost_of_issue
        '
        Me.grp_cost_of_issue.Controls.Add(Me.FLX_ISSUE_CAT_HEAD_WISE)
        Me.grp_cost_of_issue.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_cost_of_issue.ForeColor = System.Drawing.Color.White
        Me.grp_cost_of_issue.Location = New System.Drawing.Point(18, 240)
        Me.grp_cost_of_issue.Name = "grp_cost_of_issue"
        Me.grp_cost_of_issue.Size = New System.Drawing.Size(868, 365)
        Me.grp_cost_of_issue.TabIndex = 12
        Me.grp_cost_of_issue.TabStop = False
        Me.grp_cost_of_issue.Text = "Total Issue Value (both stockable and non stockable)"
        Me.grp_cost_of_issue.Visible = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "xls"
        Me.SaveFileDialog1.Filter = "Excel Files (*.xls)|*.xls"
        '
        'frm_ReportInput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.grp_cost_of_issue)
        Me.Controls.Add(Me.gbInput)
        Me.Name = "frm_ReportInput"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.gbInput.ResumeLayout(False)
        Me.gbInput.PerformLayout()
        CType(Me.FLX_ISSUE_CAT_HEAD_WISE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_cost_of_issue.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbInput As System.Windows.Forms.GroupBox
    Friend WithEvents dtp_ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_FromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblfrmdate As System.Windows.Forms.Label
    Friend WithEvents btn_show As System.Windows.Forms.Button
    Friend WithEvents rBtnMRS As System.Windows.Forms.RadioButton
    Friend WithEvents cmbstatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents rBtnReqDate As System.Windows.Forms.RadioButton
    Friend WithEvents lblCategoryHead As System.Windows.Forms.Label
    Friend WithEvents cmbCategoryHead As System.Windows.Forms.ComboBox
    Friend WithEvents FLX_ISSUE_CAT_HEAD_WISE As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents grp_cost_of_issue As System.Windows.Forms.GroupBox
    Friend WithEvents btn_ExportToExcel As System.Windows.Forms.Button
    Friend WithEvents lblSubCategory As System.Windows.Forms.Label
    Friend WithEvents cmb_subCategory As System.Windows.Forms.ComboBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog

End Class
