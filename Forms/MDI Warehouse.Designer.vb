<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDI_Warehouse
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDI_Warehouse))
        Me.frm_ListMRNDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.MasterSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Item_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Division_Settings = New System.Windows.Forms.ToolStripMenuItem
        Me.Synchronization = New System.Windows.Forms.ToolStripMenuItem
        Me.TransferDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Change_Password = New System.Windows.Forms.ToolStripMenuItem
        Me.PurchaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Supplier_Rate_List_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_Indent_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Approve_Indent = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Cancel_Indent = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_Purchase_Order = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Approve_PO = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Cancel_PO = New System.Windows.Forms.ToolStripMenuItem
        Me.StockINToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Material_Received_Without_PO_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_material_rec_against_PO = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_Reverse_Wastage_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Wastage_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_ReverseMaterial_Received_Against_PO_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_ReverseMaterial_Received_Without_PO_Master = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_Stock_Transfer = New System.Windows.Forms.ToolStripMenuItem
        Me.Reports = New System.Windows.Forms.ToolStripMenuItem
        Me.frmStockValue = New System.Windows.Forms.ToolStripMenuItem
        Me.StockValueCategoryWise = New System.Windows.Forms.ToolStripMenuItem
        Me.frmStockValueBatchWise = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_Item_Ledger = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_LedgerSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_ListIndents = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_ListIndentDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_ItemWiseIndent = New System.Windows.Forms.ToolStripMenuItem
        Me.ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_MRSListMStore = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_MRSDetailMStore = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_ItemWiseMRSMStore = New System.Windows.Forms.ToolStripMenuItem
        Me.ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_WastageItemDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_WastageItemWise = New System.Windows.Forms.ToolStripMenuItem
        Me.ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_MRNDetails = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_IssueDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_ListMRN = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_ListMRN_supplierwise = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_mrnItemWiseSupplier = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_ItemWiseMRN = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.cmd_ListMRNwithPO = New System.Windows.Forms.ToolStripMenuItem
        Me.cmd_DetailMRNwithPO = New System.Windows.Forms.ToolStripMenuItem
        Me.cmd_ItemWiseMRNwithPO = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator
        Me.frm_ReverseMaterial = New System.Windows.Forms.ToolStripMenuItem
        Me.UserManagmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_user_master = New System.Windows.Forms.ToolStripMenuItem
        Me.frm_user_rights = New System.Windows.Forms.ToolStripMenuItem
        Me.TerminateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LogOffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.picLogo = New System.Windows.Forms.PictureBox
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.toolbar = New System.Windows.Forms.ToolStrip
        Me.btNew = New System.Windows.Forms.ToolStripButton
        Me.btSave = New System.Windows.Forms.ToolStripButton
        Me.btRefresh = New System.Windows.Forms.ToolStripButton
        Me.btDelete = New System.Windows.Forms.ToolStripButton
        Me.btClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.btnViewRpt = New System.Windows.Forms.ToolStripButton
        Me.frm_Accept_Stock_transfer = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.toolbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'frm_ListMRNDetail
        '
        Me.frm_ListMRNDetail.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ListMRNDetail.Name = "frm_ListMRNDetail"
        Me.frm_ListMRNDetail.ShowShortcutKeys = False
        Me.frm_ListMRNDetail.Size = New System.Drawing.Size(363, 22)
        Me.frm_ListMRNDetail.Text = "List of MRN with detail (without PO)"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.WhiteSpace
        Me.MenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterSetupToolStripMenuItem, Me.PurchaseToolStripMenuItem, Me.StockINToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.Reports, Me.UserManagmentToolStripMenuItem, Me.TerminateToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1020, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MasterSetupToolStripMenuItem
        '
        Me.MasterSetupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_Item_Master, Me.frm_Division_Settings, Me.Synchronization, Me.TransferDataToolStripMenuItem, Me.frm_Change_Password})
        Me.MasterSetupToolStripMenuItem.Name = "MasterSetupToolStripMenuItem"
        Me.MasterSetupToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.MasterSetupToolStripMenuItem.Text = "Master Setup"
        '
        'frm_Item_Master
        '
        Me.frm_Item_Master.Name = "frm_Item_Master"
        Me.frm_Item_Master.Size = New System.Drawing.Size(171, 22)
        Me.frm_Item_Master.Text = "Item Master"
        '
        'frm_Division_Settings
        '
        Me.frm_Division_Settings.Name = "frm_Division_Settings"
        Me.frm_Division_Settings.Size = New System.Drawing.Size(171, 22)
        Me.frm_Division_Settings.Text = "Division Settings"
        '
        'Synchronization
        '
        Me.Synchronization.Name = "Synchronization"
        Me.Synchronization.Size = New System.Drawing.Size(171, 22)
        Me.Synchronization.Text = "Synchronize Data"
        '
        'TransferDataToolStripMenuItem
        '
        Me.TransferDataToolStripMenuItem.Name = "TransferDataToolStripMenuItem"
        Me.TransferDataToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.TransferDataToolStripMenuItem.Text = "Transfer Data"
        '
        'frm_Change_Password
        '
        Me.frm_Change_Password.Name = "frm_Change_Password"
        Me.frm_Change_Password.Size = New System.Drawing.Size(171, 22)
        Me.frm_Change_Password.Text = "Change Password"
        '
        'PurchaseToolStripMenuItem
        '
        Me.PurchaseToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_Supplier_Rate_List_Master, Me.ToolStripSeparator4, Me.frm_Indent_Master, Me.frm_Approve_Indent, Me.frm_Cancel_Indent, Me.ToolStripSeparator1, Me.frm_Purchase_Order, Me.frm_Approve_PO, Me.frm_Cancel_PO})
        Me.PurchaseToolStripMenuItem.Name = "PurchaseToolStripMenuItem"
        Me.PurchaseToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.PurchaseToolStripMenuItem.Text = "Purchase"
        '
        'frm_Supplier_Rate_List_Master
        '
        Me.frm_Supplier_Rate_List_Master.Image = Global.MMSPlus.My.Resources.Resources.Supplier_Rate_List
        Me.frm_Supplier_Rate_List_Master.Name = "frm_Supplier_Rate_List_Master"
        Me.frm_Supplier_Rate_List_Master.Size = New System.Drawing.Size(204, 22)
        Me.frm_Supplier_Rate_List_Master.Text = "Supplier Rate List"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(201, 6)
        '
        'frm_Indent_Master
        '
        Me.frm_Indent_Master.Image = Global.MMSPlus.My.Resources.Resources.Indent
        Me.frm_Indent_Master.Name = "frm_Indent_Master"
        Me.frm_Indent_Master.Size = New System.Drawing.Size(204, 22)
        Me.frm_Indent_Master.Text = "Indent Master"
        '
        'frm_Approve_Indent
        '
        Me.frm_Approve_Indent.Image = Global.MMSPlus.My.Resources.Resources.Approve_Indent
        Me.frm_Approve_Indent.Name = "frm_Approve_Indent"
        Me.frm_Approve_Indent.Size = New System.Drawing.Size(204, 22)
        Me.frm_Approve_Indent.Text = "Approve Indent"
        '
        'frm_Cancel_Indent
        '
        Me.frm_Cancel_Indent.Image = Global.MMSPlus.My.Resources.Resources.Cancel_Indent
        Me.frm_Cancel_Indent.Name = "frm_Cancel_Indent"
        Me.frm_Cancel_Indent.Size = New System.Drawing.Size(204, 22)
        Me.frm_Cancel_Indent.Text = "Cancel Indent"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(201, 6)
        '
        'frm_Purchase_Order
        '
        Me.frm_Purchase_Order.Image = Global.MMSPlus.My.Resources.Resources.Purchase_Order

        Me.frm_Purchase_Order.Name = "frm_Purchase_Order"
        Me.frm_Purchase_Order.Size = New System.Drawing.Size(204, 22)
        Me.frm_Purchase_Order.Text = "Purchase Order"
        '
        'frm_Approve_PO
        '
        Me.frm_Approve_PO.Image = Global.MMSPlus.My.Resources.Resources.Approve_PO
        Me.frm_Approve_PO.Name = "frm_Approve_PO"
        Me.frm_Approve_PO.Size = New System.Drawing.Size(204, 22)
        Me.frm_Approve_PO.Text = "Approve Purchase Order"
        '
        'frm_Cancel_PO
        '
        Me.frm_Cancel_PO.Image = Global.MMSPlus.My.Resources.Resources.Cancel_PO

        Me.frm_Cancel_PO.Name = "frm_Cancel_PO"
        Me.frm_Cancel_PO.Size = New System.Drawing.Size(204, 22)
        Me.frm_Cancel_PO.Text = "Cancel Purchase Order"
        '
        'StockINToolStripMenuItem
        '
        Me.StockINToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_Material_Received_Without_PO_Master, Me.frm_material_rec_against_PO, Me.ToolStripSeparator3, Me.frm_Reverse_Wastage_Master, Me.frm_Accept_Stock_transfer})
        Me.StockINToolStripMenuItem.Name = "StockINToolStripMenuItem"
        Me.StockINToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.StockINToolStripMenuItem.Text = "Stock IN"
        '
        'frm_Material_Received_Without_PO_Master
        '
        Me.frm_Material_Received_Without_PO_Master.Image = Global.MMSPlus.My.Resources.Resources.Stock_In
        Me.frm_Material_Received_Without_PO_Master.Name = "frm_Material_Received_Without_PO_Master"
        Me.frm_Material_Received_Without_PO_Master.Size = New System.Drawing.Size(264, 22)
        Me.frm_Material_Received_Without_PO_Master.Text = "Material Received Without PO Master"
        '
        'frm_material_rec_against_PO
        '
        Me.frm_material_rec_against_PO.Name = "frm_material_rec_against_PO"
        Me.frm_material_rec_against_PO.Size = New System.Drawing.Size(264, 22)
        Me.frm_material_rec_against_PO.Text = "Material Received Against PO"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(261, 6)
        '
        'frm_Reverse_Wastage_Master
        '
        Me.frm_Reverse_Wastage_Master.Image = Global.MMSPlus.My.Resources.Resources.Reverse_Wastage
        Me.frm_Reverse_Wastage_Master.Name = "frm_Reverse_Wastage_Master"
        Me.frm_Reverse_Wastage_Master.Size = New System.Drawing.Size(264, 22)
        Me.frm_Reverse_Wastage_Master.Text = "Reverse Wastage"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_Wastage_Master, Me.ToolStripSeparator5, Me.frm_ReverseMaterial_Received_Against_PO_Master, Me.frm_ReverseMaterial_Received_Without_PO_Master, Me.ToolStripSeparator2, Me.frm_Stock_Transfer})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.ReportsToolStripMenuItem.Text = "Stock Out"
        '
        'frm_Wastage_Master
        '
        Me.frm_Wastage_Master.Image = CType(resources.GetObject("frm_Wastage_Master.Image"), System.Drawing.Image)
        Me.frm_Wastage_Master.Name = "frm_Wastage_Master"
        Me.frm_Wastage_Master.Size = New System.Drawing.Size(269, 22)
        Me.frm_Wastage_Master.Text = "Wastage Master"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(266, 6)
        '
        'frm_ReverseMaterial_Received_Against_PO_Master
        '
        Me.frm_ReverseMaterial_Received_Against_PO_Master.Name = "frm_ReverseMaterial_Received_Against_PO_Master"
        Me.frm_ReverseMaterial_Received_Against_PO_Master.Size = New System.Drawing.Size(269, 22)
        Me.frm_ReverseMaterial_Received_Against_PO_Master.Text = "Reverse Material Recieved Against PO"
        '
        'frm_ReverseMaterial_Received_Without_PO_Master
        '
        Me.frm_ReverseMaterial_Received_Without_PO_Master.Name = "frm_ReverseMaterial_Received_Without_PO_Master"
        Me.frm_ReverseMaterial_Received_Without_PO_Master.Size = New System.Drawing.Size(269, 22)
        Me.frm_ReverseMaterial_Received_Without_PO_Master.Text = "Reverse Material Recieved without PO"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(266, 6)
        '
        'frm_Stock_Transfer
        '
        Me.frm_Stock_Transfer.Name = "frm_Stock_Transfer"
        Me.frm_Stock_Transfer.Size = New System.Drawing.Size(269, 22)
        Me.frm_Stock_Transfer.Text = "Stock Transfer"
        '
        'Reports
        '
        Me.Reports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frmStockValue, Me.StockValueCategoryWise, Me.frmStockValueBatchWise, Me.frm_Item_Ledger, Me.frm_LedgerSummary, Me.ToolStripSeparator8, Me.frm_ListIndents, Me.frm_ListIndentDetail, Me.frm_ItemWiseIndent, Me.ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem, Me.ToolStripSeparator6, Me.frm_MRSListMStore, Me.frm_MRSDetailMStore, Me.frm_ItemWiseMRSMStore, Me.ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem, Me.ToolStripSeparator7, Me.frm_WastageItemDetail, Me.frm_WastageItemWise, Me.ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem, Me.ToolStripSeparator9, Me.frm_MRNDetails, Me.frm_IssueDetail, Me.ToolStripSeparator10, Me.frm_ListMRN, Me.frm_ListMRNDetail, Me.frm_ListMRN_supplierwise, Me.frm_mrnItemWiseSupplier, Me.frm_ItemWiseMRN, Me.ToolStripSeparator11, Me.cmd_ListMRNwithPO, Me.cmd_DetailMRNwithPO, Me.cmd_ItemWiseMRNwithPO, Me.ToolStripSeparator13, Me.frm_ReverseMaterial})
        Me.Reports.Name = "Reports"
        Me.Reports.Size = New System.Drawing.Size(57, 20)
        Me.Reports.Text = "Reports"
        '
        'frmStockValue
        '
        Me.frmStockValue.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frmStockValue.Name = "frmStockValue"
        Me.frmStockValue.Size = New System.Drawing.Size(363, 22)
        Me.frmStockValue.Text = "Stock Value"
        '
        'StockValueCategoryWise
        '
        Me.StockValueCategoryWise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.StockValueCategoryWise.Name = "StockValueCategoryWise"
        Me.StockValueCategoryWise.Size = New System.Drawing.Size(363, 22)
        Me.StockValueCategoryWise.Text = "Stock Value Category Wise"
        '
        'frmStockValueBatchWise
        '
        Me.frmStockValueBatchWise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frmStockValueBatchWise.Name = "frmStockValueBatchWise"
        Me.frmStockValueBatchWise.Size = New System.Drawing.Size(363, 22)
        Me.frmStockValueBatchWise.Text = "Stock Value Batch Wise"
        '
        'frm_Item_Ledger
        '
        Me.frm_Item_Ledger.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_Item_Ledger.Name = "frm_Item_Ledger"
        Me.frm_Item_Ledger.Size = New System.Drawing.Size(363, 22)
        Me.frm_Item_Ledger.Text = "Item Ledger"
        '
        'frm_LedgerSummary
        '
        Me.frm_LedgerSummary.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_LedgerSummary.Name = "frm_LedgerSummary"
        Me.frm_LedgerSummary.Size = New System.Drawing.Size(363, 22)
        Me.frm_LedgerSummary.Text = "Item Ledger Summary"
        Me.frm_LedgerSummary.Visible = False
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(360, 6)
        '
        'frm_ListIndents
        '
        Me.frm_ListIndents.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ListIndents.Name = "frm_ListIndents"
        Me.frm_ListIndents.Size = New System.Drawing.Size(363, 22)
        Me.frm_ListIndents.Text = "List of Indents"
        '
        'frm_ListIndentDetail
        '
        Me.frm_ListIndentDetail.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ListIndentDetail.Name = "frm_ListIndentDetail"
        Me.frm_ListIndentDetail.Size = New System.Drawing.Size(363, 22)
        Me.frm_ListIndentDetail.Text = "List of Indent (with Detail)"
        '
        'frm_ItemWiseIndent
        '
        Me.frm_ItemWiseIndent.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ItemWiseIndent.Name = "frm_ItemWiseIndent"
        Me.frm_ItemWiseIndent.Size = New System.Drawing.Size(363, 22)
        Me.frm_ItemWiseIndent.Text = "Item Wise Indent (between Dates)"
        '
        'ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem
        '
        Me.ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem.Name = "ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem"
        Me.ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem.Size = New System.Drawing.Size(363, 22)
        Me.ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem.Text = "Item Wise Indent (between Dates Category Head Wise)"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(360, 6)
        '
        'frm_MRSListMStore
        '
        Me.frm_MRSListMStore.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_MRSListMStore.Name = "frm_MRSListMStore"
        Me.frm_MRSListMStore.Size = New System.Drawing.Size(363, 22)
        Me.frm_MRSListMStore.Text = "List of MRS"
        '
        'frm_MRSDetailMStore
        '
        Me.frm_MRSDetailMStore.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_MRSDetailMStore.Name = "frm_MRSDetailMStore"
        Me.frm_MRSDetailMStore.Size = New System.Drawing.Size(363, 22)
        Me.frm_MRSDetailMStore.Text = "List of MRS (with Detail)"
        '
        'frm_ItemWiseMRSMStore
        '
        Me.frm_ItemWiseMRSMStore.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ItemWiseMRSMStore.Name = "frm_ItemWiseMRSMStore"
        Me.frm_ItemWiseMRSMStore.Size = New System.Drawing.Size(363, 22)
        Me.frm_ItemWiseMRSMStore.Text = "Item Wise MRS (Between Dates)"
        '
        'ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem
        '
        Me.ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem.Name = "ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem"
        Me.ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem.Size = New System.Drawing.Size(363, 22)
        Me.ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem.Text = "Item Wise MRS (Between Dates Category Head Wise)"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(360, 6)
        '
        'frm_WastageItemDetail
        '
        Me.frm_WastageItemDetail.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_WastageItemDetail.Name = "frm_WastageItemDetail"
        Me.frm_WastageItemDetail.Size = New System.Drawing.Size(363, 22)
        Me.frm_WastageItemDetail.Text = "List of Wastage (with Detail)"
        '
        'frm_WastageItemWise
        '
        Me.frm_WastageItemWise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_WastageItemWise.Name = "frm_WastageItemWise"
        Me.frm_WastageItemWise.Size = New System.Drawing.Size(363, 22)
        Me.frm_WastageItemWise.Text = "Item wise Wastage (between Dates)"
        '
        'ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem
        '
        Me.ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem.Name = "ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem"
        Me.ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem.Size = New System.Drawing.Size(363, 22)
        Me.ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem.Text = "Item wise Wastage (between Dates Category Head Wise)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(360, 6)
        Me.ToolStripSeparator9.Visible = False
        '
        'frm_MRNDetails
        '
        Me.frm_MRNDetails.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_MRNDetails.Name = "frm_MRNDetails"
        Me.frm_MRNDetails.Size = New System.Drawing.Size(363, 22)
        Me.frm_MRNDetails.Text = "List of MRN (with Detail)"
        Me.frm_MRNDetails.Visible = False
        '
        'frm_IssueDetail
        '
        Me.frm_IssueDetail.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_IssueDetail.Name = "frm_IssueDetail"
        Me.frm_IssueDetail.Size = New System.Drawing.Size(363, 22)
        Me.frm_IssueDetail.Text = "List DC (with Detail)"
        Me.frm_IssueDetail.Visible = False
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(360, 6)
        '
        'frm_ListMRN
        '
        Me.frm_ListMRN.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ListMRN.Name = "frm_ListMRN"
        Me.frm_ListMRN.Size = New System.Drawing.Size(363, 22)
        Me.frm_ListMRN.Text = "List of MRN (without PO)"
        '
        'frm_ListMRN_supplierwise
        '
        Me.frm_ListMRN_supplierwise.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ListMRN_supplierwise.Name = "frm_ListMRN_supplierwise"
        Me.frm_ListMRN_supplierwise.Size = New System.Drawing.Size(363, 22)
        Me.frm_ListMRN_supplierwise.Text = "List of MRN (without PO Supplier Wise)"
        '
        'frm_mrnItemWiseSupplier
        '
        Me.frm_mrnItemWiseSupplier.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_mrnItemWiseSupplier.Name = "frm_mrnItemWiseSupplier"
        Me.frm_mrnItemWiseSupplier.Size = New System.Drawing.Size(363, 22)
        Me.frm_mrnItemWiseSupplier.Text = "MRN without PO Supplier Wise and Items wise"
        '
        'frm_ItemWiseMRN
        '
        Me.frm_ItemWiseMRN.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ItemWiseMRN.Name = "frm_ItemWiseMRN"
        Me.frm_ItemWiseMRN.Size = New System.Drawing.Size(363, 22)
        Me.frm_ItemWiseMRN.Text = "Item Wise MRN between dates (without PO)"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(360, 6)
        '
        'cmd_ListMRNwithPO
        '
        Me.cmd_ListMRNwithPO.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.cmd_ListMRNwithPO.Name = "cmd_ListMRNwithPO"
        Me.cmd_ListMRNwithPO.Size = New System.Drawing.Size(363, 22)
        Me.cmd_ListMRNwithPO.Text = "List of MRN (with PO)"
        '
        'cmd_DetailMRNwithPO
        '
        Me.cmd_DetailMRNwithPO.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.cmd_DetailMRNwithPO.Name = "cmd_DetailMRNwithPO"
        Me.cmd_DetailMRNwithPO.Size = New System.Drawing.Size(363, 22)
        Me.cmd_DetailMRNwithPO.Text = "List of MRN with Detail (with PO)"
        '
        'cmd_ItemWiseMRNwithPO
        '
        Me.cmd_ItemWiseMRNwithPO.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.cmd_ItemWiseMRNwithPO.Name = "cmd_ItemWiseMRNwithPO"
        Me.cmd_ItemWiseMRNwithPO.Size = New System.Drawing.Size(363, 22)
        Me.cmd_ItemWiseMRNwithPO.Text = "Item Wise MRN between dates (with PO)"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(360, 6)
        '
        'frm_ReverseMaterial
        '
        Me.frm_ReverseMaterial.Image = Global.MMSPlus.My.Resources.Resources.Reports
        Me.frm_ReverseMaterial.Name = "frm_ReverseMaterial"
        Me.frm_ReverseMaterial.Size = New System.Drawing.Size(363, 22)
        Me.frm_ReverseMaterial.Text = "List of Reverse Material"
        '
        'UserManagmentToolStripMenuItem
        '
        Me.UserManagmentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.frm_user_master, Me.frm_user_rights})
        Me.UserManagmentToolStripMenuItem.Name = "UserManagmentToolStripMenuItem"
        Me.UserManagmentToolStripMenuItem.Size = New System.Drawing.Size(100, 20)
        Me.UserManagmentToolStripMenuItem.Text = "User Managment"
        '
        'frm_user_master
        '
        Me.frm_user_master.Enabled = False
        Me.frm_user_master.Name = "frm_user_master"
        Me.frm_user_master.Size = New System.Drawing.Size(202, 22)
        Me.frm_user_master.Text = "User Master"
        Me.frm_user_master.Visible = False
        '
        'frm_user_rights
        '
        Me.frm_user_rights.Name = "frm_user_rights"
        Me.frm_user_rights.Size = New System.Drawing.Size(202, 22)
        Me.frm_user_rights.Text = "User Rights (Main Store)"
        '
        'TerminateToolStripMenuItem
        '
        Me.TerminateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogOffToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.TerminateToolStripMenuItem.Name = "TerminateToolStripMenuItem"
        Me.TerminateToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.TerminateToolStripMenuItem.Text = "Terminate"
        '
        'LogOffToolStripMenuItem
        '
        Me.LogOffToolStripMenuItem.Name = "LogOffToolStripMenuItem"
        Me.LogOffToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.LogOffToolStripMenuItem.Text = "Log off"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'picLogo
        '
        'Me.picLogo.Image = Global.MMSPlus.My.Resources.Resources.f_b2
        'Me.picLogo.InitialImage = Global.MMSPlus.My.Resources.Resources.f_b
        Me.picLogo.Location = New System.Drawing.Point(350, 220)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(336, 253)
        Me.picLogo.TabIndex = 8
        Me.picLogo.TabStop = False
        '
        'TabControl2
        '
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.HotTrack = True
        Me.TabControl2.Location = New System.Drawing.Point(117, 6)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(897, 676)
        Me.TabControl2.TabIndex = 7
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.76321!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.23679!))
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBox1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 50)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1020, 688)
        Me.TableLayoutPanel1.TabIndex = 9
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = Global.MMSPlus.My.Resources.Resources.M_M
        Me.PictureBox1.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(102, 676)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'toolbar
        '
        Me.toolbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btNew, Me.btSave, Me.btRefresh, Me.btDelete, Me.btClose, Me.ToolStripLabel1, Me.btnViewRpt})
        Me.toolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.toolbar.Location = New System.Drawing.Point(0, 24)
        Me.toolbar.Name = "toolbar"
        Me.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolbar.Size = New System.Drawing.Size(1020, 25)
        Me.toolbar.TabIndex = 10
        Me.toolbar.Text = "ggggg"
        '
        'btNew
        '
        Me.btNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btNew.Name = "btNew"
        Me.btNew.Size = New System.Drawing.Size(43, 22)
        Me.btNew.Text = "New"
        Me.btNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btNew.ToolTipText = "New Ctrl+N"
        '
        'btSave
        '
        Me.btSave.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(45, 22)
        Me.btSave.Text = "Save"
        Me.btSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btSave.ToolTipText = "Save Ctrl+S"
        '
        'btRefresh
        '
        Me.btRefresh.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btRefresh.Name = "btRefresh"
        Me.btRefresh.Size = New System.Drawing.Size(65, 22)
        Me.btRefresh.Text = "Refresh"
        Me.btRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btRefresh.ToolTipText = "Refresh F5"
        '
        'btDelete
        '
        Me.btDelete.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(50, 22)
        Me.btDelete.Text = "Erase"
        Me.btDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btDelete.ToolTipText = "Delete Ctrl+D"
        Me.btDelete.Visible = False
        '
        'btClose
        '
        Me.btClose.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(49, 22)
        Me.btClose.Text = "Close"
        Me.btClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btClose.ToolTipText = "Close Ctrl+O"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(80, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        Me.ToolStripLabel1.Visible = False
        '
        'btnViewRpt
        '
        Me.btnViewRpt.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewRpt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnViewRpt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewRpt.Name = "btnViewRpt"
        Me.btnViewRpt.Size = New System.Drawing.Size(45, 22)
        Me.btnViewRpt.Text = "Print"
        Me.btnViewRpt.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnViewRpt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnViewRpt.ToolTipText = "View Report Ctrl+W"
        '
        'frm_Accept_Stock_transfer
        '
        Me.frm_Accept_Stock_transfer.Name = "frm_Accept_Stock_transfer"
        Me.frm_Accept_Stock_transfer.Size = New System.Drawing.Size(264, 22)
        Me.frm_Accept_Stock_transfer.Text = "Accept Stock Transfer"
        '
        'MDI_Warehouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 738)
        Me.Controls.Add(Me.toolbar)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "MDI_Warehouse"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Material Management System"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.toolbar.ResumeLayout(False)
        Me.toolbar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents frm_ListMRNDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MasterSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Item_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Division_Settings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Synchronization As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TransferDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Change_Password As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PurchaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Supplier_Rate_List_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_Indent_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Approve_Indent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Cancel_Indent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_Purchase_Order As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Approve_PO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Cancel_PO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockINToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Material_Received_Without_PO_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_material_rec_against_PO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_Reverse_Wastage_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Wastage_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_ReverseMaterial_Received_Against_PO_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_ReverseMaterial_Received_Without_PO_Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Reports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frmStockValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockValueCategoryWise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frmStockValueBatchWise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Item_Ledger As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_LedgerSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_ListIndents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_ListIndentDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_ItemWiseIndent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_MRSListMStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_MRSDetailMStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_ItemWiseMRSMStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_WastageItemDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_WastageItemWise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_MRNDetails As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_IssueDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_ListMRN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_ListMRN_supplierwise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_mrnItemWiseSupplier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_ItemWiseMRN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmd_ListMRNwithPO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_DetailMRNwithPO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_ItemWiseMRNwithPO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_ReverseMaterial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserManagmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_user_master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_user_rights As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TerminateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogOffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents toolbar As System.Windows.Forms.ToolStrip
    Friend WithEvents btNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnViewRpt As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents frm_Stock_Transfer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frm_Accept_Stock_transfer As System.Windows.Forms.ToolStripMenuItem
End Class
