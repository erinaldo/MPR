Public Class MDI_Warehouse

    Dim cls_obj As New CommonClass
    Dim prpty_form_rights As New Form_Rights

    Public Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MDIMain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                CloseTab()
            ElseIf e.KeyCode = Keys.F5 Then
                btRefresh.PerformClick()
            ElseIf e.Control = True And e.KeyCode = Keys.N Then
                btNew.PerformClick()
            ElseIf e.Control = True And e.KeyCode = Keys.S Then
                btSave.PerformClick()
            ElseIf e.Control = True And e.KeyCode = Keys.D Then
                btDelete.PerformClick()
            ElseIf e.Control = True And e.KeyCode = Keys.O Then
                btClose.PerformClick()
            ElseIf e.Control = True And e.KeyCode = Keys.W Then
                btnViewRpt.PerformClick()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub MDIMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If MsgBox("Are you sure to exit from MMS", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gblMessageHeading) = MsgBoxResult.No Then
            e.Cancel = True
        Else
            RemoveHandler Me.FormClosing, AddressOf MDIMain_FormClosing
            Application.Exit()
        End If
    End Sub

    Private Sub Menu_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DetailMRNwithPO.Click, cmd_ListMRNwithPO.Click, frm_ItemWiseIndent.Click, frm_WastageItemWise.Click, frm_ListIndents.Click, frm_WastageItemDetail.Click, frm_Item_Master.Click, frm_user_master.Click, frm_user_rights.Click, frm_Approve_Indent.Click, frm_Cancel_Indent.Click, frm_Purchase_Order.Click, frm_Supplier_Rate_List_Master.Click, frm_LedgerSummary.Click, frm_MRNDetails.Click, frm_IssueDetail.Click, frm_ListIndentDetail.Click, frm_Approve_PO.Click, frm_Cancel_PO.Click, frm_Indent_Master.Click, frm_Wastage_Master.Click, frm_Material_Received_Without_PO_Master.Click, frm_material_rec_against_PO.Click, frmStockValue.Click, frm_MRSListMStore.Click, frm_ItemWiseMRSMStore.Click, frm_MRSDetailMStore.Click, StockValueCategoryWise.Click, frm_Reverse_Wastage_Master.Click, frm_ReverseMaterial_Received_Without_PO_Master.Click, frm_LedgerSummary.Click, frm_ListMRN_supplierwise.Click, frm_ItemWiseMRN.Click, frmStockValueBatchWise.Click, frm_ListMRNDetail.Click, cmd_ItemWiseMRNwithPO.Click, frm_ReverseMaterial_Received_Against_PO_Master.Click, frm_ReverseMaterial.Click, ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem.Click, ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem.Click, ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem.Click, frm_ListMRN.Click, frm_mrnItemWiseSupplier.Click, frm_Item_Ledger.Click, frm_Stock_Transfer.Click, frm_Accept_Stock_transfer.Click
        Dim menuItem As New ToolStripMenuItem
        If TypeOf sender Is ToolStripMenuItem Then
            menuItem = CType(sender, ToolStripMenuItem)
        End If
        prpty_form_rights = cls_obj.Get_Form_Rights(menuItem.Name)
        If prpty_form_rights.allow_view = "Y" Then
            '''' "or" condition for that time when user login first time and company is not created yet
            If Check_Form_in_tab(menuItem.Name) = False Then
                Dim tbp As New TabPage
                Select Case UCase(menuItem.Name)
                    Case UCase("frm_Item_Master")
                        tbp.Text = "Item Master"
                        tbp.Controls.Add(New frm_Item_Master(prpty_form_rights))
                    Case UCase("frm_Cost_Center")
                        tbp.Text = "Cost Center Master"
                        tbp.Controls.Add(New frm_Cost_Center(prpty_form_rights))
                    Case UCase("frm_StockAdjustment")
                        tbp.Text = "Stock Adjustment Master"
                        tbp.Controls.Add(New frm_StockAdjustment(prpty_form_rights))
                    Case UCase("frm_DivisionSettings")
                        tbp.Text = " Division Settings"
                        tbp.Controls.Add(New frm_DivisionSettings(False, prpty_form_rights))
                    Case UCase("frm_Indent_Master")
                        tbp.Text = "Indent Master"
                        tbp.Controls.Add(New frm_Indent_Master(prpty_form_rights))
                    Case UCase("frm_Wastage_Master")
                        tbp.Text = "Wastage Master"
                        tbp.Controls.Add(New frm_Wastage_Master(prpty_form_rights))
                    Case UCase("frm_user_group_master")
                        tbp.Text = "User Group Master"
                        tbp.Controls.Add(New frm_user_group_master(prpty_form_rights))
                    Case UCase("frm_user_master")
                        tbp.Text = "User Master"
                        tbp.Controls.Add(New frm_user_master("MS", prpty_form_rights))
                    Case UCase("frm_user_rights")
                        tbp.Text = "User Rights"
                        tbp.Controls.Add(New frm_user_rights("MS", MenuStrip1, prpty_form_rights))
                    Case UCase("frm_Approve_Indent")
                        tbp.Text = "Approve Indent"
                        tbp.Controls.Add(New frm_Approve_Indent("Approve Indent", IndentStatus.Fresh, IndentStatus.Pending, prpty_form_rights))
                    Case UCase("frm_Cancel_Indent")
                        tbp.Text = "Cancel Indent"
                        tbp.Controls.Add(New frm_Approve_Indent("Cancel Indent", IndentStatus.Pending, IndentStatus.Cancel, prpty_form_rights))
                    Case UCase("frm_Purchase_Order")
                        tbp.Text = "Purchase Order"
                        tbp.Controls.Add(New frm_Purchase_Order(prpty_form_rights))
                    Case UCase("frm_Approve_PO")
                        tbp.Text = "Approve PO"
                        tbp.Controls.Add(New frm_Approve_PO("Approve PO", POStatus.Fresh, POStatus.Pending, prpty_form_rights))
                    Case UCase("frm_Cancel_PO")
                        tbp.Text = "Cancel PO"
                        tbp.Controls.Add(New frm_Approve_PO("Cancel PO", POStatus.Pending, POStatus.Cancel, prpty_form_rights))
                    Case UCase("frm_Supplier_Rate_List_Master")
                        tbp.Text = "Supplier Rate List"
                        tbp.Controls.Add(New frm_Supplier_Rate_List_Master(prpty_form_rights))
                    Case UCase("frm_Open_PO_Master")
                        tbp.Text = "Open PO Master"
                        tbp.Controls.Add(New frm_Open_PO_Master(prpty_form_rights))
                    Case UCase("frm_Approve_Open_PO")
                        tbp.Text = "Approve Open PO"
                        tbp.Controls.Add(New frm_Approve_OPEN_PO("Approve Open PO", POStatus.Fresh, POStatus.Pending, prpty_form_rights))
                    Case UCase("frm_Cancel_Open_PO")
                        tbp.Text = "Cancel Open PO"
                        tbp.Controls.Add(New frm_Approve_OPEN_PO("Cancel Open PO", POStatus.Pending, POStatus.Cancel, prpty_form_rights))
                    Case UCase("frm_Material_Issue_To_Cost_Center_Master")
                        tbp.Text = "Material Issue To Cost Center"
                        tbp.Controls.Add(New frm_Material_Issue_To_Cost_Center_Master())
                    Case UCase("frm_Material_Received_Without_PO_Master")
                        tbp.Text = "Material Received Without PO Master"
                        tbp.Controls.Add(New frm_Material_Received_Without_PO_Master(prpty_form_rights))
                    Case UCase("frm_LedgerSummary")
                        tbp.Text = "Item Ledger Summary"
                        gblSelectedReportName = enmReportName.RptItemLedgerSummary
                        tbp.Controls.Add(New frm_LedgerSummary(enmReportName.RptItemLedgerSummary, prpty_form_rights))
                    Case UCase("frm_MRNDetails")
                        tbp.Text = "MRN Details"
                        gblSelectedReportName = enmReportName.RptMrnDetail
                        tbp.Controls.Add(New frm_MRNDetails(prpty_form_rights))
                    Case UCase("frm_IssueDetail")
                        tbp.Text = "Issue Details"
                        gblSelectedReportName = enmReportName.RptIssueDetail
                        tbp.Controls.Add(New frm_IssueDetail(prpty_form_rights))
                    Case UCase("frm_ListIndentDetail")
                        tbp.Text = "Indent Details"
                        gblSelectedReportName = enmReportName.RptIndentDetail
                        tbp.Controls.Add(New frm_LedgerSummary(enmReportName.RptIndentDetail, prpty_form_rights))
                    Case UCase("Frm_IndentPrintRpt")
                        tbp.Text = "Indent Print Details"
                        gblSelectedReportName = enmReportName.RptIndentPrintDetail
                        tbp.Controls.Add(New frm_LedgerSummary(enmReportName.RptIndentPrintDetail, prpty_form_rights))
                    Case UCase("frm_material_rec_against_PO")
                        tbp.Text = "Material Received Against PO"
                        tbp.Controls.Add(New frm_material_rec_against_PO(prpty_form_rights))
                    Case UCase("frmStockValue")
                        tbp.Text = "Stock Value"
                        gblSelectedReportName = enmReportName.RptStockValue
                        tbp.Controls.Add(New frmStockValue(enmReportName.RptStockValue, prpty_form_rights))
                    Case UCase("frm_Item_Ledger")
                        tbp.Text = "Item Ledger"
                        tbp.Controls.Add(New frm_Item_Ledger(prpty_form_rights))
                    Case UCase("frmStockValueBatchWise")
                        tbp.Text = "Stock Value Batch Wise"
                        gblSelectedReportName = enmReportName.RptStockValueBatchWise
                        tbp.Controls.Add(New frmStockValue(enmReportName.RptStockValueBatchWise, prpty_form_rights))
                    Case UCase("frm_LedgerSummary")
                        tbp.Text = "List Of Indents"
                        gblSelectedReportName = enmReportName.RptListofIndent
                        tbp.Controls.Add(New frm_LedgerSummary(enmReportName.RptListofIndent, prpty_form_rights))
                    Case UCase("frm_MRSListMStore")
                        tbp.Text = "MRS Item List"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptMrsItemListMStore, prpty_form_rights))
                    Case UCase("frm_MRSDetailMStore")
                        tbp.Text = "MRS Detail List"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptMrsDetailMStore, prpty_form_rights))
                    Case UCase("frm_ItemWiseMRSMStore")
                        tbp.Text = "Item Wise MRS"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemwiseMrsMstore, prpty_form_rights))
                    Case UCase("ItemWiseMRSBetweenDatesCategoryHeadWiseToolStripMenuItem")
                        tbp.Text = "Item Wise MRS Detail Category Head Wise."
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptMrsItemListMStoreCategoryHeadWisePrint, prpty_form_rights))
                    Case UCase("frm_WastageItemDetail")
                        tbp.Text = "Wastage Items Detail"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptWastageDetailList, prpty_form_rights))
                    Case UCase("frm_WastageItemWise")
                        tbp.Text = "Item Wise Wastage Detail"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemwiseWastage, prpty_form_rights))
                    Case UCase("ItemWiseWastagebetweenDatesCategoryHeadWiseToolStripMenuItem")
                        tbp.Text = "Item Wise Wastage Detail Category Head Wise."
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemwiseWastageCategoryHeadWisePrint, prpty_form_rights))
                        ''''''''Wastage Detail Reports''''''''''''''''''''''''''''''''''''''''''
                    Case UCase("StockValueCategoryWise")
                        tbp.Text = "Stock Value Category Wise"
                        gblSelectedReportName = enmReportName.RptStockValueCategoryWise
                        tbp.Controls.Add(New frmStockValue(enmReportName.RptStockValueCategoryWise, prpty_form_rights))
                    Case UCase("frm_Reverse_Wastage_Master")
                        tbp.Text = "Reverse Wastage"
                        tbp.Controls.Add(New frm_Reverse_Wastage_Master(prpty_form_rights))
                    Case UCase("frm_ReverseMaterial_Issue_To_Cost_Center_Master")
                        tbp.Text = " Reverse Material Issue To Cost Center"
                        tbp.Controls.Add(New frm_ReverseMaterial_Issue_To_Cost_Center_Master(prpty_form_rights))
                    Case UCase("frm_ReverseMaterial_Received_Without_PO_Master")
                        tbp.Text = " Reverse Material Receive Without PO"
                        tbp.Controls.Add(New frm_ReverseMaterial_Received_Without_PO_Master(prpty_form_rights))
                    Case UCase("frm_ReverseMaterial_Received_Against_PO_Master")
                        tbp.Text = " Reverse Material Receive Against PO"
                        tbp.Controls.Add(New frm_ReverseMaterial_Received_Against_PO_Master(prpty_form_rights))
                        ''''''''Indent

                    Case UCase("frm_ItemWiseIndent")
                        tbp.Text = " Item Wise Indent Detail"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemWiseIndentDetail, prpty_form_rights))
                    Case UCase("frm_ListIndents")
                        tbp.Text = " List Of Indents"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptListofIndent, prpty_form_rights))
                        '''''''''''''MRN WithOut PO '''''''''''
                    Case UCase("frm_ListMRN_supplierwise")
                        tbp.Text = "List of MRN withou PO (Supplier Wise)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptListofMRNWithOutPO_WithoutSupplier, prpty_form_rights))
                    Case UCase("frm_mrnItemWiseSupplier")
                        tbp.Text = "List of MRN withou PO(Item Wise)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptListofMRNWithOutPO_ItemWiseSuppliers, prpty_form_rights))
                    Case UCase("frm_ListMRN")
                        tbp.Text = "List of MRN(Without PO)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptListofMRNWithOutPO, prpty_form_rights))
                    Case UCase("frm_ItemWiseMRN")
                        tbp.Text = "Item Wise MRN(Without PO)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemWiseMRNWithOutPO, prpty_form_rights))
                    Case UCase("frm_ListMRNDetail")
                        tbp.Text = "List Of MRN Detail(Without PO)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptListofMRNDetailWithOutPO, prpty_form_rights))
                        ''''''''''''''MRN With PO'''''''''''''''
                    Case UCase("cmd_ListMRNwithPO")
                        tbp.Text = "List MRN(With Purchase Order)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptListMRNWithPO, prpty_form_rights))
                    Case UCase("cmd_DetailMRNwithPO")
                        tbp.Text = "Detail MRN List(With Purchase Order)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptDetailMRNListWithPO, prpty_form_rights))
                    Case UCase("cmd_ItemWiseMRNwithPO")
                        tbp.Text = "ItemWiseMRNList(With Purchase Order)"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemWiseMRNWithPO, prpty_form_rights))

                    Case UCase("frm_ReverseMaterial")
                        tbp.Text = "List Of Reverse Material WithOut PO"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptReverseMaterialWithOutPO, prpty_form_rights))
                        ''''''''''''''MRN With PO'''''''''''''''
                    Case UCase("frm_DebitNote")
                        tbp.Text = " Debit Note"
                        tbp.Controls.Add(New frm_DebitNoteNew(prpty_form_rights))

                    Case UCase("ItemWiseMaterialIssueToCostCenterToolStripMenuItem")
                        tbp.Text = "Item Wise Material Issue To Cost Center"
                        tbp.Controls.Add(New frm_Material_Issue_To_CostCentre_Item_Wise(1, prpty_form_rights))
                        'tbp.Controls.Add(New frm_ReportInput(enmReportName.RptMatIssueItemWiseToCostCenterPrint))
                    Case UCase("MaterialIssuedCategoryHeadWiseToolStripMenuItem")
                        tbp.Text = " Material Issue To Cost Center Item Wise and Category Head Wise"
                        tbp.Controls.Add(New frm_Material_Issue_To_CostCentre_Item_Wise(2, prpty_form_rights))
                        'Case UCase("ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem")
                        '    tbp.Text = "Item Wise Indent Detail Category Head Wise."
                        '    tbp.Controls.Add(New frm_ReportInput(enmReportName.RptIndentDetailCategoryHeadWisePrint))
                    Case UCase("ItemWiseMaterialIssueToCostCenterCatHeadWiseToolStripMenuItem")
                        tbp.Text = " Material Issue To Cost Center Item Wise and Category Head Wise"
                        tbp.Controls.Add(New frm_Material_Issue_To_CostCentre_Item_Wise(2, prpty_form_rights))

                    Case UCase("ItemWiseIndentbetweenDatesCategoryHeadWiseToolStripMenuItem")
                        tbp.Text = "Item Wise Indent Detail Category Head Wise."
                        tbp.Controls.Add(New frm_Material_Issue_To_CostCentre_Item_Wise(3, prpty_form_rights))

                    Case UCase("frm_Stock_Transfer")
                        tbp.Text = "Stock Transfer"
                        tbp.Controls.Add(New frm_Stock_Transfer(prpty_form_rights))

                    Case UCase("frm_Accept_Stock_transfer")
                        tbp.Text = "Accept Stock Transfer"
                        tbp.Controls.Add(New frm_Accept_Stock_transfer(prpty_form_rights))

                    Case UCase("CostofIssueReport")
                        tbp.Text = "Cost of Issue Report."
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptCategoryHeadWiseIssue, prpty_form_rights))

                    Case Else
                        MsgBox("Form not found!!!", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, gblMessageHeading)
                        Return
                End Select
                SetEventHandlers(tbp.Controls(0))
                TabControl2.TabPages.Add(tbp)
                TabControl2.SelectTab(TabControl2.TabPages.Count - 1)

                If TabControl2.TabCount = 0 Then
                    picLogo.Visible = True
                Else
                    picLogo.Visible = False
                End If
            End If
        Else
            RightsMsg()
        End If

    End Sub

    Private Sub SetEventHandlers(ByVal FirstControlOfTabPage As Object)
        Try
            OldUserControl = NewUserControl
            NewUserControl = CType(FirstControlOfTabPage, IForm)

            If OldUserControl IsNot Nothing Then
                RemoveHandler btNew.Click, AddressOf OldUserControl.NewClick
                RemoveHandler btSave.Click, AddressOf OldUserControl.SaveClick
                RemoveHandler btClose.Click, AddressOf OldUserControl.CloseClick
                RemoveHandler btDelete.Click, AddressOf OldUserControl.DeleteClick
                RemoveHandler btRefresh.Click, AddressOf OldUserControl.RefreshClick
                RemoveHandler btnViewRpt.Click, AddressOf OldUserControl.ViewClick
            End If

            AddHandler btNew.Click, AddressOf NewUserControl.NewClick
            AddHandler btSave.Click, AddressOf NewUserControl.SaveClick
            AddHandler btClose.Click, AddressOf NewUserControl.CloseClick
            AddHandler btDelete.Click, AddressOf NewUserControl.DeleteClick
            AddHandler btRefresh.Click, AddressOf NewUserControl.RefreshClick
            AddHandler btnViewRpt.Click, AddressOf NewUserControl.ViewClick
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Private Function Check_Form_in_tab(ByVal frm_name As String) As Boolean
        Dim i As Integer
        Dim flag As Boolean
        flag = False
        For i = 1 To TabControl2.TabCount
            If TabControl2.TabPages(i - 1).Controls(0).Name = frm_name Then
                TabControl2.SelectedIndex = i - 1
                flag = True
                Exit For
            End If
        Next
        Return flag
    End Function

    Private Sub TabControl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim tabPage As TabPage
            tabPage = TabControl2.SelectedTab
            If tabPage IsNot Nothing Then
                SetEventHandlers(tabPage.Controls(0)) ''first control (means at zero index) is the user control
                ''of which we have to set handlers
            ElseIf NewUserControl IsNot Nothing Then
                RemoveHandler btNew.Click, AddressOf NewUserControl.NewClick
                RemoveHandler btSave.Click, AddressOf NewUserControl.SaveClick
                RemoveHandler btClose.Click, AddressOf NewUserControl.CloseClick
                RemoveHandler btDelete.Click, AddressOf NewUserControl.DeleteClick
                RemoveHandler btRefresh.Click, AddressOf NewUserControl.RefreshClick
                RemoveHandler btnViewRpt.Click, AddressOf NewUserControl.ViewClick
            End If

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub btClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClose.Click
        Try
            CloseTab()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub CloseTab()
        Try
            'Call RemoveAllHandler()
            If TabControl2.TabPages.Count > 0 Then
                TabControl2.TabPages.Remove(TabControl2.SelectedTab)
            End If
            If TabControl2.TabCount = 0 Then
                picLogo.Visible = True
            Else
                picLogo.Visible = False
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub TabControl2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TabControl2_Click(sender, e)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub



    Private Sub LogOffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOffToolStripMenuItem.Click
        If MsgBox("Are you sure to logoff from MMS", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gblMessageHeading) = MsgBoxResult.Yes Then
            RemoveHandler Me.FormClosing, AddressOf MDIMain_FormClosing
            Application.Restart()
            'AddHandler Me.FormClosing, AddressOf MDIMain_FormClosing
        End If
    End Sub


    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub frm_Division_Settings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frm_Division_Settings.Click
        Dim menuItem As New ToolStripMenuItem
        If TypeOf sender Is ToolStripMenuItem Then
            menuItem = CType(sender, ToolStripMenuItem)
        End If
        prpty_form_rights = cls_obj.Get_Form_Rights(menuItem.Name)
        If prpty_form_rights.allow_view = "Y" Then
            Dim obj As New frm_DivisionSettings(False, prpty_form_rights)
            obj.Show()
        Else
            RightsMsg()
        End If
    End Sub

    Private Sub frm_Synchronization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Synchronization.Click
        Dim menuItem As New ToolStripMenuItem
        If TypeOf sender Is ToolStripMenuItem Then
            menuItem = CType(sender, ToolStripMenuItem)
        End If
        prpty_form_rights = cls_obj.Get_Form_Rights(menuItem.Name)
        If prpty_form_rights.allow_view = "Y" Then
            Dim frmsyn As New frm_Synchronization(prpty_form_rights)
            frmsyn.ShowDialog()
        Else
            RightsMsg()
        End If
    End Sub

    Private Sub frm_transferData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransferDataToolStripMenuItem.Click
        Dim menuItem As New ToolStripMenuItem
        If TypeOf sender Is ToolStripMenuItem Then
            menuItem = CType(sender, ToolStripMenuItem)
        End If
        prpty_form_rights = cls_obj.Get_Form_Rights(menuItem.Name)
        If prpty_form_rights.allow_view = "Y" Then
            Dim frmdatatransfer As New frm_transferData(prpty_form_rights)
            frmdatatransfer.ShowDialog()
        Else
            RightsMsg()
        End If
    End Sub

    Private Sub frm_Change_Password_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frm_Change_Password.Click
        Dim menuItem As New ToolStripMenuItem
        If TypeOf sender Is ToolStripMenuItem Then
            menuItem = CType(sender, ToolStripMenuItem)
        End If
        prpty_form_rights = cls_obj.Get_Form_Rights(menuItem.Name)
        If prpty_form_rights.allow_view = "Y" Then
            Dim frm_ChangePassword As New frm_ChangePassword(prpty_form_rights)
            frm_ChangePassword.ShowDialog()
        Else
            RightsMsg()
        End If
    End Sub

End Class