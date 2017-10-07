<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDICostCenter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDICostCenter))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.logo = New System.Windows.Forms.ToolStripMenuItem()
        Me.Home = New System.Windows.Forms.ToolStripMenuItem()
        Me.MasterSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_MRS_MainStore = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Approve_MRS = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Cancek_MRS = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Material_Received = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Change_Password = New System.Windows.Forms.ToolStripMenuItem()
        Me.StockOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_material_return = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_stock_transfer_CC_To_CC = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Accept_Stock = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Closing_Stock_CC = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_Freeze_ClosingStock = New System.Windows.Forms.ToolStripMenuItem()
        Me.Wastage_Master = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_stock_value = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mrs_item = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItemWiseMRS = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mrsdetail = New System.Windows.Forms.ToolStripMenuItem()
        Me.Wastagedetail_itemwise = New System.Windows.Forms.ToolStripMenuItem()
        Me.IKT_ItemWise = New System.Windows.Forms.ToolStripMenuItem()
        Me.Consumption_ItemWise = New System.Windows.Forms.ToolStripMenuItem()
        Me.CategoryHeadWiseConsumptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserManagmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_user_master = New System.Windows.Forms.ToolStripMenuItem()
        Me.frm_user_rights = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerminateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogOffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.toolbar = New System.Windows.Forms.ToolStrip()
        Me.btnViewRpt = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.btClose = New System.Windows.Forms.ToolStripButton()
        Me.btDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.btRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.btSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.btNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuStrip1.SuspendLayout()
        Me.toolbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.BackColor = System.Drawing.Color.OrangeRed
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.MenuStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.logo, Me.Home, Me.MasterSetupToolStripMenuItem, Me.StockOutToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.UserManagmentToolStripMenuItem, Me.TerminateToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(105, 686)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'logo
        '
        Me.logo.AutoSize = False
        Me.logo.BackColor = System.Drawing.Color.Black
        Me.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.logo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.logo.Font = New System.Drawing.Font("Agency FB", 17.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.logo.ForeColor = System.Drawing.Color.Orange
        Me.logo.Image = Global.MMSPlus.My.Resources.Resources.mms_3
        Me.logo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.logo.Margin = New System.Windows.Forms.Padding(5)
        Me.logo.Name = "logo"
        Me.logo.Size = New System.Drawing.Size(115, 60)
        '
        'Home
        '
        Me.Home.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(11, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.Home.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Home.ForeColor = System.Drawing.Color.White
        Me.Home.Image = Global.MMSPlus.My.Resources.Resources.go_home2
        Me.Home.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.Home.Name = "Home"
        Me.Home.Size = New System.Drawing.Size(98, 66)
        Me.Home.Text = "Home"
        Me.Home.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'MasterSetupToolStripMenuItem
        '
        Me.MasterSetupToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.MasterSetupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_MRS_MainStore, Me.frm_Approve_MRS, Me.frm_Cancek_MRS, Me.frm_Material_Received, Me.frm_Change_Password})
        Me.MasterSetupToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MasterSetupToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.MasterSetupToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Paste_clipboard_edit_document
        Me.MasterSetupToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MasterSetupToolStripMenuItem.Name = "MasterSetupToolStripMenuItem"
        Me.MasterSetupToolStripMenuItem.Size = New System.Drawing.Size(98, 66)
        Me.MasterSetupToolStripMenuItem.Text = "MRS"
        Me.MasterSetupToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frm_MRS_MainStore
        '
        Me.frm_MRS_MainStore.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_MRS_MainStore.ForeColor = System.Drawing.Color.White
        Me.frm_MRS_MainStore.Image = Global.MMSPlus.My.Resources.Resources.Purchase_Order
        Me.frm_MRS_MainStore.Name = "frm_MRS_MainStore"
        Me.frm_MRS_MainStore.Size = New System.Drawing.Size(175, 22)
        Me.frm_MRS_MainStore.Text = "MRS Master"
        '
        'frm_Approve_MRS
        '
        Me.frm_Approve_MRS.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Approve_MRS.ForeColor = System.Drawing.Color.White
        Me.frm_Approve_MRS.Image = Global.MMSPlus.My.Resources.Resources.Approve_PO
        Me.frm_Approve_MRS.Name = "frm_Approve_MRS"
        Me.frm_Approve_MRS.Size = New System.Drawing.Size(175, 22)
        Me.frm_Approve_MRS.Text = "Approve MRS"
        '
        'frm_Cancek_MRS
        '
        Me.frm_Cancek_MRS.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Cancek_MRS.ForeColor = System.Drawing.Color.White
        Me.frm_Cancek_MRS.Image = Global.MMSPlus.My.Resources.Resources.Cancel_PO
        Me.frm_Cancek_MRS.Name = "frm_Cancek_MRS"
        Me.frm_Cancek_MRS.Size = New System.Drawing.Size(175, 22)
        Me.frm_Cancek_MRS.Text = "Cancel MRS"
        '
        'frm_Material_Received
        '
        Me.frm_Material_Received.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Material_Received.ForeColor = System.Drawing.Color.White
        Me.frm_Material_Received.Image = Global.MMSPlus.My.Resources.Resources.Stock_In
        Me.frm_Material_Received.Name = "frm_Material_Received"
        Me.frm_Material_Received.Size = New System.Drawing.Size(175, 22)
        Me.frm_Material_Received.Text = "Material Received"
        Me.frm_Material_Received.Visible = False
        '
        'frm_Change_Password
        '
        Me.frm_Change_Password.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Change_Password.ForeColor = System.Drawing.Color.White
        Me.frm_Change_Password.Image = Global.MMSPlus.My.Resources.Resources.Psichologist_doctor_psychologist2
        Me.frm_Change_Password.Name = "frm_Change_Password"
        Me.frm_Change_Password.Size = New System.Drawing.Size(175, 22)
        Me.frm_Change_Password.Text = "Change Password"
        '
        'StockOutToolStripMenuItem
        '
        Me.StockOutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.StockOutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_material_return, Me.frm_stock_transfer_CC_To_CC, Me.frm_Accept_Stock, Me.frm_Closing_Stock_CC, Me.frm_Freeze_ClosingStock, Me.Wastage_Master})
        Me.StockOutToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StockOutToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.StockOutToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Data_transmission_database1
        Me.StockOutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.StockOutToolStripMenuItem.Name = "StockOutToolStripMenuItem"
        Me.StockOutToolStripMenuItem.Size = New System.Drawing.Size(98, 66)
        Me.StockOutToolStripMenuItem.Text = "Transactions"
        Me.StockOutToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frm_material_return
        '
        Me.frm_material_return.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_material_return.ForeColor = System.Drawing.Color.White
        Me.frm_material_return.Image = Global.MMSPlus.My.Resources.Resources.Stock_Out
        Me.frm_material_return.Name = "frm_material_return"
        Me.frm_material_return.Size = New System.Drawing.Size(235, 22)
        Me.frm_material_return.Text = "Material Return To Store"
        Me.frm_material_return.Visible = False
        '
        'frm_stock_transfer_CC_To_CC
        '
        Me.frm_stock_transfer_CC_To_CC.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_stock_transfer_CC_To_CC.ForeColor = System.Drawing.Color.White
        Me.frm_stock_transfer_CC_To_CC.Image = Global.MMSPlus.My.Resources.Resources.Stock_Out
        Me.frm_stock_transfer_CC_To_CC.Name = "frm_stock_transfer_CC_To_CC"
        Me.frm_stock_transfer_CC_To_CC.Size = New System.Drawing.Size(235, 22)
        Me.frm_stock_transfer_CC_To_CC.Text = "Inter Kichen Transfer"
        '
        'frm_Accept_Stock
        '
        Me.frm_Accept_Stock.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Accept_Stock.ForeColor = System.Drawing.Color.White
        Me.frm_Accept_Stock.Image = Global.MMSPlus.My.Resources.Resources.Stock_In
        Me.frm_Accept_Stock.Name = "frm_Accept_Stock"
        Me.frm_Accept_Stock.Size = New System.Drawing.Size(235, 22)
        Me.frm_Accept_Stock.Text = "Accept Inter Kitchen Transfer"
        '
        'frm_Closing_Stock_CC
        '
        Me.frm_Closing_Stock_CC.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Closing_Stock_CC.ForeColor = System.Drawing.Color.White
        Me.frm_Closing_Stock_CC.Image = Global.MMSPlus.My.Resources.Resources.Unloading_web_vector_uploading1
        Me.frm_Closing_Stock_CC.Name = "frm_Closing_Stock_CC"
        Me.frm_Closing_Stock_CC.Size = New System.Drawing.Size(235, 22)
        Me.frm_Closing_Stock_CC.Text = "Closing Stock/Consumption"
        '
        'frm_Freeze_ClosingStock
        '
        Me.frm_Freeze_ClosingStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_Freeze_ClosingStock.ForeColor = System.Drawing.Color.White
        Me.frm_Freeze_ClosingStock.Image = Global.MMSPlus.My.Resources.Resources.box_download
        Me.frm_Freeze_ClosingStock.Name = "frm_Freeze_ClosingStock"
        Me.frm_Freeze_ClosingStock.Size = New System.Drawing.Size(235, 22)
        Me.frm_Freeze_ClosingStock.Text = "Freeze Closing Stock"
        Me.frm_Freeze_ClosingStock.Visible = False
        '
        'Wastage_Master
        '
        Me.Wastage_Master.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Wastage_Master.ForeColor = System.Drawing.Color.White
        Me.Wastage_Master.Image = Global.MMSPlus.My.Resources.Resources.Wastage
        Me.Wastage_Master.Name = "Wastage_Master"
        Me.Wastage_Master.Size = New System.Drawing.Size(235, 22)
        Me.Wastage_Master.Text = "Wastage Master"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_stock_value, Me.Mrs_item, Me.ListOfMaterialRequisitionSlipsToolStripMenuItem, Me.ItemWiseMRS, Me.Mrsdetail, Me.Wastagedetail_itemwise, Me.IKT_ItemWise, Me.Consumption_ItemWise, Me.CategoryHeadWiseConsumptionToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportsToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ReportsToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Market_report1
        Me.ReportsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(98, 66)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        Me.ReportsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frm_stock_value
        '
        Me.frm_stock_value.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_stock_value.ForeColor = System.Drawing.Color.White
        Me.frm_stock_value.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_stock_value.Name = "frm_stock_value"
        Me.frm_stock_value.Size = New System.Drawing.Size(262, 22)
        Me.frm_stock_value.Text = "Stock Value"
        '
        'Mrs_item
        '
        Me.Mrs_item.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Mrs_item.ForeColor = System.Drawing.Color.White
        Me.Mrs_item.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.Mrs_item.Name = "Mrs_item"
        Me.Mrs_item.Size = New System.Drawing.Size(262, 22)
        Me.Mrs_item.Text = "MRS List"
        '
        'ListOfMaterialRequisitionSlipsToolStripMenuItem
        '
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem.Name = "ListOfMaterialRequisitionSlipsToolStripMenuItem"
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.ListOfMaterialRequisitionSlipsToolStripMenuItem.Text = "List of Material Requisition Slips"
        '
        'ItemWiseMRS
        '
        Me.ItemWiseMRS.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ItemWiseMRS.ForeColor = System.Drawing.Color.White
        Me.ItemWiseMRS.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.ItemWiseMRS.Name = "ItemWiseMRS"
        Me.ItemWiseMRS.Size = New System.Drawing.Size(262, 22)
        Me.ItemWiseMRS.Text = "Item wise MRS"
        '
        'Mrsdetail
        '
        Me.Mrsdetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Mrsdetail.ForeColor = System.Drawing.Color.White
        Me.Mrsdetail.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.Mrsdetail.Name = "Mrsdetail"
        Me.Mrsdetail.Size = New System.Drawing.Size(262, 22)
        Me.Mrsdetail.Text = "MRS Detail"
        '
        'Wastagedetail_itemwise
        '
        Me.Wastagedetail_itemwise.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Wastagedetail_itemwise.ForeColor = System.Drawing.Color.White
        Me.Wastagedetail_itemwise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.Wastagedetail_itemwise.Name = "Wastagedetail_itemwise"
        Me.Wastagedetail_itemwise.Size = New System.Drawing.Size(262, 22)
        Me.Wastagedetail_itemwise.Text = "Item Wise Wastage Detail"
        '
        'IKT_ItemWise
        '
        Me.IKT_ItemWise.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.IKT_ItemWise.ForeColor = System.Drawing.Color.White
        Me.IKT_ItemWise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.IKT_ItemWise.Name = "IKT_ItemWise"
        Me.IKT_ItemWise.Size = New System.Drawing.Size(262, 22)
        Me.IKT_ItemWise.Text = "Item Wise Inter Kitchen Transfer"
        '
        'Consumption_ItemWise
        '
        Me.Consumption_ItemWise.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Consumption_ItemWise.ForeColor = System.Drawing.Color.White
        Me.Consumption_ItemWise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.Consumption_ItemWise.Name = "Consumption_ItemWise"
        Me.Consumption_ItemWise.Size = New System.Drawing.Size(262, 22)
        Me.Consumption_ItemWise.Text = "Item Wise Consumption"
        '
        'CategoryHeadWiseConsumptionToolStripMenuItem
        '
        Me.CategoryHeadWiseConsumptionToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CategoryHeadWiseConsumptionToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.CategoryHeadWiseConsumptionToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.CategoryHeadWiseConsumptionToolStripMenuItem.Name = "CategoryHeadWiseConsumptionToolStripMenuItem"
        Me.CategoryHeadWiseConsumptionToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.CategoryHeadWiseConsumptionToolStripMenuItem.Text = "Category Head Wise Consumption"
        '
        'UserManagmentToolStripMenuItem
        '
        Me.UserManagmentToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.UserManagmentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_user_master, Me.frm_user_rights})
        Me.UserManagmentToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserManagmentToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.UserManagmentToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.User_Card1
        Me.UserManagmentToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UserManagmentToolStripMenuItem.Name = "UserManagmentToolStripMenuItem"
        Me.UserManagmentToolStripMenuItem.Size = New System.Drawing.Size(98, 66)
        Me.UserManagmentToolStripMenuItem.Text = "User Settings"
        Me.UserManagmentToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frm_user_master
        '
        Me.frm_user_master.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_user_master.Enabled = False
        Me.frm_user_master.ForeColor = System.Drawing.Color.White
        Me.frm_user_master.Image = Global.MMSPlus.My.Resources.Resources.Psichologist_doctor_psychologist3
        Me.frm_user_master.Name = "frm_user_master"
        Me.frm_user_master.Size = New System.Drawing.Size(142, 22)
        Me.frm_user_master.Text = "User Master"
        Me.frm_user_master.Visible = False
        '
        'frm_user_rights
        '
        Me.frm_user_rights.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.frm_user_rights.ForeColor = System.Drawing.Color.White
        Me.frm_user_rights.Image = Global.MMSPlus.My.Resources.Resources.Client_list_text2
        Me.frm_user_rights.Name = "frm_user_rights"
        Me.frm_user_rights.Size = New System.Drawing.Size(142, 22)
        Me.frm_user_rights.Text = "User Rights"
        '
        'TerminateToolStripMenuItem
        '
        Me.TerminateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TerminateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogOffToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.TerminateToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TerminateToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.TerminateToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.SignOff2
        Me.TerminateToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TerminateToolStripMenuItem.Name = "TerminateToolStripMenuItem"
        Me.TerminateToolStripMenuItem.Size = New System.Drawing.Size(98, 66)
        Me.TerminateToolStripMenuItem.Text = "Quit"
        Me.TerminateToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'LogOffToolStripMenuItem
        '
        Me.LogOffToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LogOffToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.LogOffToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.User_logout_man3
        Me.LogOffToolStripMenuItem.Name = "LogOffToolStripMenuItem"
        Me.LogOffToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LogOffToolStripMenuItem.Text = "Log off"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ExitToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ExitToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Logout_user_login_account2
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'TabControl2
        '
        Me.TabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl2.Location = New System.Drawing.Point(106, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(918, 647)
        Me.TabControl2.TabIndex = 4
        '
        'toolbar
        '
        Me.toolbar.BackColor = System.Drawing.SystemColors.ControlDark
        Me.toolbar.CanOverflow = False
        Me.toolbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnViewRpt, Me.ToolStripSeparator11, Me.btClose, Me.btDelete, Me.ToolStripSeparator17, Me.btRefresh, Me.ToolStripSeparator7, Me.btSave, Me.ToolStripSeparator18, Me.btNew, Me.ToolStripSeparator19})
        Me.toolbar.Location = New System.Drawing.Point(105, 650)
        Me.toolbar.Name = "toolbar"
        Me.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolbar.Size = New System.Drawing.Size(917, 36)
        Me.toolbar.TabIndex = 5
        Me.toolbar.Text = "ggggg"
        '
        'btnViewRpt
        '
        Me.btnViewRpt.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnViewRpt.AutoSize = False
        Me.btnViewRpt.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btnViewRpt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnViewRpt.ForeColor = System.Drawing.Color.White
        Me.btnViewRpt.Image = Global.MMSPlus.My.Resources.Resources.Printer_print_3d_vector_symbol
        Me.btnViewRpt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewRpt.Name = "btnViewRpt"
        Me.btnViewRpt.Size = New System.Drawing.Size(150, 33)
        Me.btnViewRpt.Text = "Print"
        Me.btnViewRpt.ToolTipText = "View Report Ctrl+W"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 36)
        '
        'btClose
        '
        Me.btClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btClose.AutoSize = False
        Me.btClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btClose.ForeColor = System.Drawing.Color.White
        Me.btClose.Image = Global.MMSPlus.My.Resources.Resources.Close_delete_remove_exit_cross
        Me.btClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(150, 33)
        Me.btClose.Text = "Close"
        Me.btClose.ToolTipText = "Close Ctrl+O"
        '
        'btDelete
        '
        Me.btDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btDelete.AutoSize = False
        Me.btDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDelete.ForeColor = System.Drawing.Color.White
        Me.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(150, 33)
        Me.btDelete.Text = "Erase"
        Me.btDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btDelete.ToolTipText = "Delete Ctrl+D"
        Me.btDelete.Visible = False
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(6, 36)
        '
        'btRefresh
        '
        Me.btRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btRefresh.AutoSize = False
        Me.btRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btRefresh.ForeColor = System.Drawing.Color.White
        Me.btRefresh.Image = Global.MMSPlus.My.Resources.Resources.PC_Web_synchronization
        Me.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btRefresh.Name = "btRefresh"
        Me.btRefresh.Size = New System.Drawing.Size(150, 33)
        Me.btRefresh.Text = "Refresh"
        Me.btRefresh.ToolTipText = "Refresh F5"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 36)
        '
        'btSave
        '
        Me.btSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btSave.AutoSize = False
        Me.btSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btSave.ForeColor = System.Drawing.Color.White
        Me.btSave.Image = Global.MMSPlus.My.Resources.Resources.Save_all_download
        Me.btSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(150, 33)
        Me.btSave.Text = "Save"
        Me.btSave.ToolTipText = "Save Ctrl+S"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(6, 36)
        '
        'btNew
        '
        Me.btNew.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btNew.AutoSize = False
        Me.btNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btNew.ForeColor = System.Drawing.Color.White
        Me.btNew.Image = Global.MMSPlus.My.Resources.Resources.Table_excel_row_document_teach
        Me.btNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btNew.Name = "btNew"
        Me.btNew.Size = New System.Drawing.Size(150, 33)
        Me.btNew.Text = "New"
        Me.btNew.ToolTipText = "New Ctrl+N"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator19.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(6, 36)
        '
        'MDICostCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.DimGray
        Me.BackgroundImage = Global.MMSPlus.My.Resources.Resources.whicon2
        Me.ClientSize = New System.Drawing.Size(1022, 686)
        Me.ControlBox = False
        Me.Controls.Add(Me.toolbar)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.TabControl2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MDICostCenter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.toolbar.ResumeLayout(False)
        Me.toolbar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MasterSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Material_Received As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents frm_MRS_MainStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Mrs_item As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListOfMaterialRequisitionSlipsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemWiseMRS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserManagmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_user_master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_user_rights As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Mrsdetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Approve_MRS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Cancek_MRS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Change_Password As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_material_return As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_stock_transfer_CC_To_CC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Accept_Stock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Closing_Stock_CC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Freeze_ClosingStock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_stock_value As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Wastage_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Wastagedetail_itemwise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IKT_ItemWise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Consumption_ItemWise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CategoryHeadWiseConsumptionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents logo As ToolStripMenuItem
    Friend WithEvents Home As ToolStripMenuItem
    Friend WithEvents toolbar As ToolStrip
    Friend WithEvents btnViewRpt As ToolStripButton
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents btClose As ToolStripButton
    Friend WithEvents btDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator17 As ToolStripSeparator
    Friend WithEvents btRefresh As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents btSave As ToolStripButton
    Friend WithEvents ToolStripSeparator18 As ToolStripSeparator
    Friend WithEvents btNew As ToolStripButton
    Friend WithEvents ToolStripSeparator19 As ToolStripSeparator
    Friend WithEvents TerminateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogOffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
