Imports MMSPlus.cost_center_master
Public Class frm_Cost_Center
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim clsObj As New cls_cost_center_master
    Dim prpty As cls_cost_center_master_prop
    Dim flag As String

    Dim CostCenter_Code As String

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            flag = "save"
            ClearControls()
            lbl_CC_Code.Text = GetCostCenterCode()
            TBCCostCenter.SelectTab(1)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> Cost Center Master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        ClearControls()
        FillGrid()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                If txt_CostCenter_Name.Text = "" Then
                    ErrorProviderCC.SetError(txt_CostCenter_Name, "Please Enter The CostCenter Name!")
                    Exit Sub
                Else

                    If clsObj.get_record_count("select * from COST_CENTER_MASTER where CostCenter_Name ='" & txt_CostCenter_Name.Text & "'") > 0 Then
                        MsgBox("CostCenter Name already exsist !", MsgBoxStyle.Information, "Save CostCenter")
                    Else
                        prpty = New cls_cost_center_master_prop
                        prpty.CostCenter_Id = Convert.ToInt32(obj.getMaxValue("CostCenter_Id", "COST_CENTER_MASTER"))
                        prpty.CostCenter_Code = GetCostCenterCode()
                        prpty.CostCenter_Name = txt_CostCenter_Name.Text()
                        prpty.CostCenter_Description = txt_CostCenter_Discription.Text()
                        prpty.CostCenter_Status = chkStatus.Checked
                        prpty.Division_Id = v_the_current_division_id
                        clsObj.Insert_COST_CENTER_MASTER(prpty)
                        ClearControls()
                        FillGrid()
                        MsgBox("Record Saved", MsgBoxStyle.Information, "Save Cost Center Info")
                    End If
                End If
            Else
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                If txt_CostCenter_Name.Text = "" Then
                    MsgBox("Please Enter The Cost Center Name", MsgBoxStyle.Information, "Update CostCenter Info")
                Else
                    prpty = New cls_cost_center_master_prop
                    prpty.CostCenter_Id = DgvCostCenterMaster.SelectedRows.Item(0).Cells(0).Value()
                    prpty.CostCenter_Code = lbl_CC_Code.Text
                    prpty.CostCenter_Name = txt_CostCenter_Name.Text()
                    prpty.CostCenter_Description = txt_CostCenter_Discription.Text()
                    prpty.CostCenter_Status = chkStatus.Checked
                    prpty.Division_Id = v_the_current_division_id
                    clsObj.Update_COST_CENTER_MASTER(prpty)
                    ClearControls()
                    MsgBox("Record Updated", MsgBoxStyle.Information, "Updated Cost Center Info")
                End If
            End If
            FillGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_hr_user_master")
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub
    Private Sub ClearControls()
        clsObj.Clear_All_TextBox(Me.GBCostCenterEntry.Controls)
        lbl_CC_Code.Text = ""
        txt_CostCenter_Name.Focus()
    End Sub
    Public Sub Clear_All_TextBox(ByVal cc As Control.ControlCollection)
        For Each C As Control In cc
            If TypeOf C Is TextBox Then
                CType(C, TextBox).Text = ""
            End If
        Next C
    End Sub
    Private Sub FillGrid()
        Try
            clsObj.GridBind(DgvCostCenterMaster, "SELECT  CostCenter_Id, CostCenter_Code, CostCenter_Name, CostCenter_Description,CASE WHEN CostCenter_Status = 1 THEN 'Active' ELSE 'DeActive' End AS CostCenter_Status,Division_Id FROM COST_CENTER_MASTER")
            DgvCostCenterMaster.Columns(0).Visible = False 'Cost Center id
            DgvCostCenterMaster.Columns(1).HeaderText = "Cost Center Code"
            DgvCostCenterMaster.Columns(1).Width = 150
            DgvCostCenterMaster.Columns(2).HeaderText = "Cost Center Name"
            DgvCostCenterMaster.Columns(2).Width = 160
            DgvCostCenterMaster.Columns(3).HeaderText = "Cost Center Discription"
            DgvCostCenterMaster.Columns(3).Width = 300
            DgvCostCenterMaster.Columns(4).HeaderText = "Cost Center Status"
            DgvCostCenterMaster.Columns(4).Width = 150
            DgvCostCenterMaster.Columns(5).Visible = False 'DIVISION_ID
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in  --> Fill Grid Cost Center Master")
        End Try
    End Sub

    Private Sub frm_Cost_Center_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            clsObj.FormatGrid(DgvCostCenterMaster)
            flag = "save"
            FillGrid()
            TBCCostCenter.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in  --> Form Load of Cost Center")
        End Try
    End Sub
  
    Public Function GetCostCenterCode() As String
        Dim Pre As String
        Dim CCID As String
        Dim CostCenterCode As String
        Pre = obj.getPrefixCode("COST_CENTER_PREFIX", "DIVISION_SETTINGS")
        CCID = obj.getMaxValue("CostCenter_Id", "COST_CENTER_MASTER")
        CostCenterCode = Pre & "" & CCID
        Return CostCenterCode
    End Function


    Private Sub DgvCostCenterMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgvCostCenterMaster.DoubleClick
        Try
            lbl_CC_Code.Text = DgvCostCenterMaster.SelectedRows.Item(0).Cells(1).Value
            txt_CostCenter_Name.Text = DgvCostCenterMaster.SelectedRows.Item(0).Cells(2).Value
            txt_CostCenter_Discription.Text = DgvCostCenterMaster.SelectedRows.Item(0).Cells(3).Value
            If DgvCostCenterMaster.SelectedRows.Item(0).Cells(4).Value = "Active" Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
            flag = "update"
            TBCCostCenter.SelectTab(1)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in --> GridviewCostCenterMaster Double Click")
        End Try

    End Sub
End Class
