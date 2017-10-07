Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class frm_Material_Issue_To_CostCentre_Item_Wise
    Implements IForm
    Dim clsObj As New material_issue_to_cost_center_master.cls_material_issue_to_cost_center_master
    Dim Query As String
    Dim objCommFunction As New CommonClass
    Dim _call_type As Integer
    Dim _report_name As String
    Dim _report_call_type As Integer = 0

    Dim _form_rights As Form_Rights
    Public Sub New(ByVal call_type As Integer, ByVal rights As Form_Rights)
        InitializeComponent()
        _report_call_type = call_type
        _form_rights = rights
    End Sub
    Private Sub frm_Material_Issue_To_CostCentre_Item_Wise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If _report_call_type = 1 Then
            Label1.Text = "Cost Centre"
            Label4.Visible = True
            cmbCostCenter.Visible = True
            cmbCategoryHead.Visible = True
            cmb_subCategory.Visible = True
            BindCostCenterCombo()
            BindCatHeadNameCombo()
        End If
        If _report_call_type = 2 Then
            Label1.Visible = True
            Label4.Visible = True
            Label4.Text = "Category Head:"
            cmbCostCenter.Visible = True
            cmbCategoryHead.Visible = True
            cmb_subCategory.Visible = True
            BindCostCenterCombo()
            BindCatHeadNameCombo()
        End If
        If _report_call_type = 3 Then
            Label1.Text = "Category Head:"
            BindCatHeadNameCombo()
            cmb_subCategory.Enabled = True
            RBtnDetails.Visible = False
            RBtnSummary.Visible = False
        End If
    End Sub
    Private Sub BindCatHeadNameCombo()
        ''********************************************************************''
        ''Fill Category Head Drop Down
        ''********************************************************************''
        Dim Dt As New DataTable
        Dim DtRow As DataRow
        Query = "Select ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME,ITEM_CAT_Head_NAME + '-' + ITEM_CAT_Head_CODE as CATEGORY_HEAD_NAME From ITEM_CATEGORY_HEAD_MASTER"
        Dt = clsObj.Fill_DataSet(Query).Tables(0)
        DtRow = Dt.NewRow
        DtRow("ITEM_CAT_Head_ID") = -1
        DtRow("ITEM_CAT_Head_NAME") = "--All Category Head--"
        Dt.Rows.InsertAt(DtRow, 0)
        cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
        cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
        cmbCategoryHead.DataSource = Dt
        cmbCategoryHead.SelectedIndex = 0

        cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
        cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
        cmbCategoryHead.DataSource = Dt
        cmbCategoryHead.SelectedIndex = 0
    End Sub

    Private Sub BindCostCenterCombo()
        ''********************************************************************''
        ''Fill cost center Drop Down
        ''********************************************************************''
        Dim Dt As New DataTable
        Dim DtRow As DataRow

        Query = "Select COSTCENTER_ID,COSTCENTER_NAME + '-' + CostCenter_Code as COSTCENTER_NAME" & _
            " from COST_CENTER_MASTER" & _
            " where CostCenter_Status = 1 and Division_Id = " & v_the_current_division_id
        Dt = clsObj.Fill_DataSet(Query).Tables(0)
        DtRow = Dt.NewRow
        DtRow("COSTCENTER_ID") = -1
        DtRow("COSTCENTER_NAME") = "--All Cost Center--"
        Dt.Rows.InsertAt(DtRow, 0)
        cmbCostCenter.DisplayMember = "COSTCENTER_NAME"
        cmbCostCenter.ValueMember = "COSTCENTER_ID"
        cmbCostCenter.DataSource = Dt
        cmbCostCenter.SelectedIndex = 0

        cmbCostCenter.DisplayMember = "COSTCENTER_NAME"
        cmbCostCenter.ValueMember = "COSTCENTER_ID"
        cmbCostCenter.DataSource = Dt
        cmbCostCenter.SelectedIndex = 0
    End Sub

    Private Sub cmd_Show_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Show.Click
        'If RBtnSummary.Checked = False And RBtnDetails.Checked = False Then
        '    MsgBox("Please Select Category 'Summary' OR 'Detail'.")
        'Else
        Dim DateFrom As New Date()
        Dim DateTo As New Date()
        Dim CBStr1 As String
        Dim CBStr2 As String
        Dim CBStr3 As String

        If DateTimePicker1.Text.Trim() <> "" Then
            DateFrom = Convert.ToString(DateTimePicker1.Value.Date)
        End If
        If DateTimePicker2.Text.Trim() <> "" Then
            DateTo = "," & Convert.ToString(DateTimePicker2.Value.Date)
        End If

        If cmbCostCenter.SelectedIndex = 0 Then
            CBStr1 = "%"
        Else
            CBStr1 = Convert.ToString(cmbCostCenter.SelectedValue)
        End If

        If cmbCategoryHead.SelectedIndex = 0 Then
            CBStr2 = "%"
        Else
            CBStr2 = Convert.ToString(cmbCategoryHead.SelectedValue)
        End If

        If cmb_subCategory.SelectedIndex = 0 Then
            CBStr3 = "%"
        Else
            CBStr3 = Convert.ToString(cmb_subCategory.SelectedValue)
        End If

        Try
            Dim ds As New DataSet
            Dim filepath As String = ""
            Dim dsSubQry As New DataSet
            Dim rep As New ReportDocument()
            If RBtnSummary.Checked = True And _report_call_type = 1 Then
                filepath = ReportFilePath & "cryMaterialIssuetoCostCenterItemwise.rpt"
                rep.Load(filepath)
            End If
            If RBtnDetails.Checked = True And _report_call_type = 1 Then
                filepath = ReportFilePath & "CryItemWiseMICostCenter.rpt"
                rep.Load(filepath)

            End If
            If RBtnDetails.Checked = True And _report_call_type = 2 Then
                filepath = ReportFilePath & "cryMaterialIssuetoCostCenter Itemwise HEAD WISE DETAILED.rpt"
                rep.Load(filepath)

            End If
            If RBtnSummary.Checked = True And _report_call_type = 2 Then
                filepath = ReportFilePath & "cryMaterialIssuetoCostCenter Itemwise HEAD WISE.rpt"
                rep.Load(filepath)
            End If
            If _report_call_type = 3 Then
                filepath = ReportFilePath & "CyItemWiseIndentsCategoryHeadWise.rpt"
                rep.Load(filepath)

            End If
            'Dim rep As New MyReportDocument(filepath)


            Dim CRXParamDefs As CrystalDecisions.Shared.ParameterFields
            Dim CRXParamDef As CrystalDecisions.Shared.ParameterField
            CRXParamDefs = rep.ParameterFields
            For Each CRXParamDef In CRXParamDefs
                With CRXParamDef
                    Select Case .ParameterFieldName
                        Case "@FromDate"
                            rep.SetParameterValue("@FromDate", Convert.ToDateTime(DateTimePicker1.Value.Date.ToString("dd-MMM-yyyy")))
                        Case "@ToDate"
                            rep.SetParameterValue("@ToDate", Convert.ToDateTime(DateTimePicker2.Value.Date.ToString("dd-MMM-yyyy")))
                        Case "@costid"
                            rep.SetParameterValue("@costid", CBStr1)
                        Case "@CatHeadId"
                            rep.SetParameterValue("@CatHeadId", CBStr2)
                        Case "@CatID"
                            rep.SetParameterValue("@CatID", CBStr3)
                    End Select
                End With
            Next
            'rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "afbl_mms")
            Dim connection As New ConnectionInfo()
            connection.DatabaseName = gblDataBase_Name 'myDataBase
            connection.ServerName = gblDataBaseServer_Name '127.0.0.1
            connection.UserID = gblDataBase_UserName 'root
            connection.Password = gblDataBase_Password '12345
            ' First we assign the connection to all tables in the main report
            '
            For Each table As CrystalDecisions.CrystalReports.Engine.Table In rep.Database.Tables
                AssignTableConnection(table, connection)
            Next


            frm_Report.CryViewer.ReportSource = rep
            frm_Report.Show()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
        'End If
    End Sub

    Private Sub AssignTableConnection(ByVal table As CrystalDecisions.CrystalReports.Engine.Table, ByVal connection As ConnectionInfo)
        ' Cache the logon info block
        Dim logOnInfo As TableLogOnInfo = table.LogOnInfo

        connection.Type = logOnInfo.ConnectionInfo.Type

        ' Set the connection
        logOnInfo.ConnectionInfo = connection

        ' Apply the connection to the table!

        table.LogOnInfo.ConnectionInfo.DatabaseName = connection.DatabaseName
        table.LogOnInfo.ConnectionInfo.ServerName = connection.ServerName
        table.LogOnInfo.ConnectionInfo.UserID = connection.UserID
        table.LogOnInfo.ConnectionInfo.Password = connection.Password
        table.LogOnInfo.ConnectionInfo.Type = connection.Type
        table.ApplyLogOnInfo(logOnInfo)
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    'Private Sub cmbCostCenter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenter.SelectedIndexChanged
    '    Dim Cat_head_id As String
    '    If (cmbCostCenter.SelectedValue.ToString() <> "%" And cmbCostCenter.SelectedValue.ToString() <> "0") Then
    '        Cat_head_id = cmbCostCenter.SelectedValue.ToString()
    '        cmb_subCategory.Enabled = True
    '        objCommFunction.ComboBind(cmb_subCategory, "select  '%' as ITEM_CAT_ID,'--All Categories--' as ITEM_CAT_NAME union select  cast(ITEM_CAT_ID as varchar), ITEM_CAT_NAME from ITEM_CATEGORY where fk_ITEM_CAT_Head_ID = " & Cat_head_id & "ORDER BY ITEM_CAT_ID", "ITEM_CAT_NAME", "ITEM_CAT_ID")
    '    End If
    'End Sub
    Private Sub cmbCategoryHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategoryHead.SelectedIndexChanged
        Dim Cat_head_id As String
        If (cmbCategoryHead.SelectedValue.ToString() <> "%" And cmbCategoryHead.SelectedValue.ToString() <> "0") Then
            Cat_head_id = cmbCategoryHead.SelectedValue.ToString()
            cmb_subCategory.Enabled = True
            objCommFunction.ComboBind(cmb_subCategory, "select  '%' as ITEM_CAT_ID,'--All Categories--' as ITEM_CAT_NAME union select  cast(ITEM_CAT_ID as varchar), ITEM_CAT_NAME from ITEM_CATEGORY where fk_ITEM_CAT_Head_ID = " & Cat_head_id & "ORDER BY ITEM_CAT_ID", "ITEM_CAT_NAME", "ITEM_CAT_ID")
        End If
    End Sub
End Class
