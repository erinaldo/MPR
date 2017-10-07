Public Class MDICostCenter
    Dim cls_obj As New CommonClass
    Dim prpty_form_rights As New Form_Rights
    Public Sub MDICostCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub MDICostCenter_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If MsgBox("Are you sure to exit from MMS", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            RemoveHandler Me.FormClosing, AddressOf MDICostCenter_FormClosing
            Application.Exit()
        Else
            e.Cancel = True
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
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> CloseTab")
        End Try
    End Sub

    Private Sub CloseAllTab()
        Try

            If TabControl2.TabPages.Count > 0 Then

                For index As Integer = 0 To TabControl2.TabPages.Count - 1
                    TabControl2.TabPages.Remove(Me.TabControl2.TabPages(0))
                Next



            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> CloseTab")
        End Try
    End Sub



    Private Sub TabControl2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TabControl2_Click(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "TabControl2_SelectedIndexChanged")
        End Try
    End Sub




    Private Sub MDICostCenter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> MDIMain_KeyDown")
        End Try
    End Sub

    
    Private Sub Menu_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frm_MRS_MainStore.Click, frm_Material_Received.Click, frm_Approve_MRS.Click, frm_Cancek_MRS.Click, frm_material_return.Click, frm_stock_transfer_CC_To_CC.Click, frm_Closing_Stock_CC.Click, frm_Freeze_ClosingStock.Click, frm_stock_value.Click, frm_Accept_Stock.Click, Mrs_item.Click, Mrsdetail.Click, ItemWiseMRS.Click, frm_user_rights.Click, Wastage_Master.Click, Wastagedetail_itemwise.Click, IKT_ItemWise.Click, Consumption_ItemWise.Click, CategoryHeadWiseConsumptionToolStripMenuItem.Click
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
                    Case UCase("frm_MRS_MainStore")
                        tbp.Text = "MRS Master"
                        tbp.Controls.Add(New frm_MRS_MainStore(prpty_form_rights))
                    Case UCase("frm_Material_Received")
                        tbp.Text = "Material Received"
                        tbp.Controls.Add(New frm_Material_Received(prpty_form_rights))
                    Case UCase("frm_Approve_MRS")
                        tbp.Text = "Approve MRS"
                        tbp.Controls.Add(New frm_Approve_MRS("Approve MRS", MRSStatus.Fresh, MRSStatus.Pending, prpty_form_rights))
                    Case UCase("frm_Cancek_MRS")
                        tbp.Text = "Cancel MRS"
                        tbp.Controls.Add(New frm_Approve_MRS("Cancel MRS", MRSStatus.Pending, MRSStatus.Cancel, prpty_form_rights))
                    Case UCase("frm_material_return")
                        tbp.Text = "Material Return"
                        tbp.Controls.Add(New frm_material_return(prpty_form_rights))
                    Case UCase("frm_Closing_Stock_CC")
                        tbp.Text = "Closing Master/Consumption"
                        tbp.Controls.Add(New frm_Closing_Stock_CC(prpty_form_rights))
                    Case UCase("frm_Freeze_ClosingStock")
                        tbp.Text = "Freeze Closing Stock"
                        tbp.Controls.Add(New frm_Freeze_ClosingStock(prpty_form_rights))
                    Case UCase("frm_stock_value")
                        tbp.Text = "Stock Value"
                        gblSelectedReportName = enmReportName.RptStockValueCC
                        tbp.Controls.Add(New frmStockValue(enmReportName.RptStockValueCC, prpty_form_rights))
                    Case UCase("frm_stock_transfer_CC_To_CC")
                        tbp.Text = "Inter Kitchen Transfer"
                        tbp.Controls.Add(New frm_Stock_Transfer_CC_To_CC(prpty_form_rights))
                    Case UCase("frm_Accept_Stock")
                        tbp.Text = "Accept Inter Kitchen Transfer"
                        tbp.Controls.Add(New frm_Accept_Stock(prpty_form_rights))
                    Case UCase("Mrs_item")
                        tbp.Text = "MRS Item List"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptMrsItemList, prpty_form_rights))
                    Case UCase("IKT_ItemWise")
                        tbp.Text = "Item Wise Inter Kitchen Transfer"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptIkt_ItemWise_cc, prpty_form_rights))
                    Case UCase("Consumption_ItemWise")
                        tbp.Text = "Item Wise Consumption"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptConsumption_ItemWise_cc, prpty_form_rights))
                    Case UCase("CategoryHeadWiseConsumptionToolStripMenuItem")
                        tbp.Text = "Category Head Wise Consumption"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptCatheadWise_Consumption_cc, prpty_form_rights))
                    Case UCase("Mrsdetail")
                        tbp.Text = "MRS Detail List"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptMrsdetailList, prpty_form_rights))
                    Case UCase("Wastagedetail_itemwise")
                        tbp.Text = "Item Wise Wastage Detail"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptWastageDetail_ItemWise_cc, prpty_form_rights))
                    Case UCase("ItemWiseMrs")
                        tbp.Text = "Item Wise MRS"
                        tbp.Controls.Add(New frm_ReportInput(enmReportName.RptItemwiseMRS, prpty_form_rights))
                    Case UCase("frm_Closing_Stock_CC")
                        tbp.Text = "Closing Stock"
                        tbp.Controls.Add(New frm_Closing_Stock_CC(prpty_form_rights))
                    Case UCase("Wastage_Master")
                        tbp.Text = "Wastage Master"
                        tbp.Controls.Add(New frm_Wastage_Master_cc(prpty_form_rights))
                    Case UCase("frm_user_rights")
                        tbp.Text = "User Rights"
                        tbp.Controls.Add(New frm_user_rights("CC", MenuStrip1, prpty_form_rights))

                    Case Else
                        MsgBox("Form not found!!!", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, gblMessageHeading)
                        Return
                End Select
                SetEventHandlers(tbp.Controls(0))
                TabControl2.TabPages.Add(tbp)
                TabControl2.SelectTab(TabControl2.TabPages.Count - 1)
            End If
        Else
            RightsMsg()
        End If
    End Sub
 
  

    Private Sub LogOffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOffToolStripMenuItem.Click
        Application.Restart()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub frm_Change_Password_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frm_Change_Password.Click
        Dim menuItem As New ToolStripMenuItem
        If TypeOf sender Is ToolStripMenuItem Then
            menuItem = CType(sender, ToolStripMenuItem)
        End If
        'prpty_form_rights = cls_obj.Get_Form_Rights(menuItem.Name)
        'If prpty_form_rights.allow_view = "Y" Then
        Dim frm_ChangePassword As New frm_ChangePassword(prpty_form_rights)
        frm_ChangePassword.ShowDialog()
        'Else
        'RightsMsg()
        'End If
    End Sub

    Private Sub Home_Click(sender As Object, e As EventArgs) Handles Home.Click
        CloseAllTab()
        
    End Sub
End Class