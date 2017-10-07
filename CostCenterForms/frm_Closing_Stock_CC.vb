Imports MMSPlus.Adjustment_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Closing_Stock_CC
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim clsObj As New Closing_master.cls_Closing_master
    Dim prpty As New Closing_master.cls_Closing_master_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iAdjustmentId As Int32
    Dim objComm As New CommonClass
    Dim iclosingid As Integer
    Dim ds1 As DataSet
    Dim DtItem As New DataTable

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmgrdWastageItem
        rowid = 0
        ItemId = 1
        ItemCode = 2
        ItemName = 3
        ItemUOM = 4
        Batch_N0 = 5
        Expiry_Date = 6
        Batch_Qty = 7
        Stock_Detail_Id = 8
        Adjustment_Qty = 10
    End Enum

    Private Enum enmstatus
        Fresh = 1
        Freeze = 2
    End Enum

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            flag = "save"
            new_initilization()
            

           
            TBCWastageMaster.SelectTab(1)

            iclosingid = Convert.ToInt32(obj.getMaxValue("Closing_ID", "Closing_CC_Master"))
            lbl_Closing_code.Text = Convert.ToString(obj.getPrefixCode("CLOSING_PREFIX", "DIVISION_SETTINGS")) + Convert.ToString(iclosingid)
            'lbl_Closing_Date.Text = Now.ToString("dd-MMM-yyyy")
            dtpClosingdt.Value = Now()
            lblstatus.Text = "Fresh"
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Wastwge_Master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGridMaster()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
       
        'DGVClstkitems.EndEdit()

        Dim val As String = TryCast(DGVClstkitems.Rows(0)("item_code"), String)
        If String.IsNullOrEmpty(val) Then
            MsgBox("Please Enter Atleast One Record", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If
        Dim dt As New DataTable
        dt = DGVClstkitems.DataSource

        For rowcount As Integer = 0 To dt.Rows.Count - 1
            If Not IsDBNull(dt.Rows(rowcount)("ClosingStockRecp")) And IsNumeric(dt.Rows(rowcount)("ClosingStockRecp")) Then
                If Convert.ToDouble(dt.Rows(rowcount)("ClosingStockRecp")) < 0 Then
                    MsgBox("Stock cannot be negative.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            If IsNumeric(dt.Rows(rowcount)("Avg_Rate")) And IsNumeric(dt.Rows(rowcount)("Consumption")) Then
                If Convert.ToDouble(dt.Rows(rowcount)("Avg_Rate")) = 0 And Convert.ToDouble(dt.Rows(rowcount)("Consumption")) < 0 Then
                    MsgBox("Rate Cannot be zero in case of negative consumption.", MsgBoxStyle.Critical)
                    Exit Sub
                End If

            End If
        Next

        Dim ds As New DataSet
        'Dim closing_date As String = dtpClosingdt.Value.Date + Convert.ToDateTime("11:59:59").TimeOfDay
        Dim closing_date As String = Convert.ToDateTime(dtpClosingdt.Value.Date).ToString("MM/dd/yyyy")

        ds = obj.fill_Data_set("Pro_GetClosingDtlCC", "@Closing_Date,@CostCenter_id", closing_date + "," + Convert.ToString(v_the_current_Selected_CostCenter_id))

        If ds.Tables(0).Rows.Count > 0 Then
            For index As Integer = 0 To dt.Rows.Count - 1
                For rowcount As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(dt.Rows(index)("Item_Id")) Then
                        If Convert.ToInt32(dt.Rows(index)("Item_Id")) = Convert.ToInt32(ds.Tables(0).Rows(rowcount)("Item_id")) Then
                            MsgBox("Closing of the same item cannot be done twice a day.", MsgBoxStyle.Information)
                            Exit Sub
                        End If
                    End If
                Next
            Next
        End If

        Dim cmd As SqlCommand
        cmd = obj.MyCon_BeginTransaction
        Try

            If flag = "save" Then
                Dim Mdate As DateTime = Today()

                iclosingid = Convert.ToInt32(obj.getMaxValue("Closing_ID", "Closing_CC_Master"))
                prpty.Closing_ID = iclosingid
                prpty.Closing_Code = obj.getPrefixCode("CLOSING_PREFIX", "DIVISION_SETTINGS")
                prpty.Closing_No = iclosingid
                prpty.Closing_Date = dtpClosingdt.Value.Date + Convert.ToDateTime("11:59:59 PM").TimeOfDay
                prpty.CostCenter_ID = v_the_current_Selected_CostCenter_id
                prpty.Closing_Status = Convert.ToInt32(enmstatus.Freeze)
                prpty.Closing_Remarks = txtAdjustmentRemarks.Text
                prpty.Created_BY = v_the_current_logged_in_user_name
                prpty.Creation_Date = Now
                prpty.Modified_BY = ""
                prpty.Modified_Date = NULL_DATE
                prpty.Division_ID = v_the_current_division_id
                clsObj.Insert_Closing_Master(prpty, cmd)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRow = 0
                iRowCount = dt.Rows.Count
                For iRow = 0 To iRowCount - 1

                    If Not IsDBNull(dt.Rows(iRow)("Quantity")) And IsNumeric(dt.Rows(iRow)("Quantity")) Then
                        'If Convert.ToInt32(dt.Rows(iRow)("check")) = 1 Then
                        prpty.Closing_ID = iclosingid
                        prpty.Division_ID = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
                        prpty.CostCenter_ID = v_the_current_Selected_CostCenter_id
                        prpty.Item_ID = Convert.ToInt32(dt.Rows(iRow)("item_id"))
                        prpty.Item_Qty = Convert.ToDouble(dt.Rows(iRow)("Quantity"))
                        prpty.Item_Rate = Convert.ToDouble(dt.Rows(iRow)("Avg_Rate"))
                        prpty.Current_Stock = Convert.ToDouble(dt.Rows(iRow)("Current_Stock"))
                        prpty.Consumption = Convert.ToDouble(dt.Rows(iRow)("Consumption"))
                        prpty.Created_BY = v_the_current_logged_in_user_name
                        prpty.Creation_Date = Now
                        prpty.Modified_BY = ""
                        prpty.Modified_Date = NULL_DATE
                        prpty.Conv_factor = Convert.ToDouble(dt.Rows(iRow)("convfact"))
                        prpty.Closing_Stock_recp = Convert.ToDouble(dt.Rows(iRow)("ClosingStockrecp"))
                        clsObj.Insert_Closing_Detail(prpty, cmd)
                        ' End If
                        'End If
                    End If
                Next iRow

            Else
            End If
            obj.MyCon_CommitTransaction(cmd)
            If flag = "save" Then
                If MsgBox("Closing Stock Has Been Saved Successfully." & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptClosingStockPrint, "closing_id", CStr(iclosingid), CStr(enmDataType.D_int))
                End If
            End If

            DtItem.Rows.Clear()
            DtItem.Columns.Clear()
            new_initilization()
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try


    End Sub



    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TBCWastageMaster.SelectedIndex = 0 Then
                objComm.RptShow(enmReportName.RptClosingStockPrint, "closing_id", CStr(DGVMasters("closing_id", DGVMasters.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    objComm.RptShow(enmReportName.RptClosingStockPrint, "closing_id", CStr(DGVMasters("closing_id", DGVMasters.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub get_row(ByVal item_id As String)
        'Dim drv As DataRowView
        Dim IsInsert As Boolean
        Dim ds As DataSet
        ds = obj.fill_Data_set("GET_ITEM_BY_ID_CC", "@V_ITEM_ID,@V_Div_id,@V_Todate", item_id & "," & v_the_current_Selected_CostCenter_id & "," & dtpClosingdt.Value.ToString("dd-MMM-yyyy"))
        Dim iRowCount As Int32
        Dim iRow As Int32
        iRowCount = DGVClstkitems.Rows.Count
        IsInsert = True
        'DGVClstkitems_Style()
        For iRow = 0 To iRowCount - 2
            If DGVClstkitems.Item(0, iRow).Value = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "Closing Stock Item")
                IsInsert = False
                Exit For
            End If
        Next iRow
        Dim datatbl As New DataTable
        datatbl = ds.Tables(0)
        'DGVClstkitems.DataSource

        Dim trans_date = obj.ExecuteScalar("select dbo.fn_format(trans_date) from vw_itemrecieveissue_details_cc" & _
                                        " where item_id=" & item_id & " and" & _
                                    " cast(dbo.fn_format(trans_date) as datetime) > cast(dbo.fn_format('" & dtpClosingdt.Value.Date & "') as datetime) and entrytype='I' and cs_id=" & v_the_current_Selected_CostCenter_id)

        If Not trans_date Is Nothing Then
            MsgBox("This Item is already issued/closing punched on """ & trans_date & """ date" & vbCrLf & "So you can not add this item's closing stock.")
            Exit Sub
        End If

        If IsInsert = True Then
            Dim introw As Integer
            'For Each dr As DataRow In datatbl.Rows
            ' MsgBox(DGVClstkitems.Rows.Count)
            introw = DGVClstkitems.Rows.Count - 1
            DGVClstkitems.Rows.Insert(introw)
            DGVClstkitems.Rows(introw)("Item_ID") = ds.Tables(0).Rows(0)(0)
            DGVClstkitems.Rows(introw)("Item_CODE") = ds.Tables(0).Rows(0)("item_Code").ToString()
            DGVClstkitems.Rows(introw)("Item_Name") = ds.Tables(0).Rows(0)("item_Name").ToString()
            'DGVClstkitems.Rows(introw).Cells("item_cat_name").Value = ds.Tables(0).Rows(0)("item_cat_name").ToString()
            DGVClstkitems.Rows(introw)("UM_Name") = ds.Tables(0).Rows(0)("UM_NAME").ToString()
            DGVClstkitems.Rows(introw)("Convfact") = ds.Tables(0).Rows(0)("Conv_fac_recp").ToString()
            ''New Column Added 
            DGVClstkitems.Rows(introw)("Current_Stock") = ds.Tables(0).Rows(0)("Current_Stock").ToString()
            DGVClstkitems.Rows(introw)("Conv_Current_Stock") = ds.Tables(0).Rows(0)("Conv_Current_Stock").ToString()
            DGVClstkitems.Rows(introw)("Avg_Rate") = ds.Tables(0).Rows(0)("Avg_Rate").ToString()
            ''New Column Added 
            DGVClstkitems.Rows(introw)("Quantity") = ds.Tables(0).Rows(0)("Current_Stock").ToString()
            DGVClstkitems.Rows(introw)("ClosingStockRecp") = ds.Tables(0).Rows(0)("Conv_Current_Stock").ToString()
            'DGVClstkitems.Rows.Add()


            'If Me.DGVClstkitems.Rows.Count > 0 Then DGVClstkitems.Rows.Add()
            ' If Me.DGVClstkitems.Rows.Count > 0 Then DGVClstkitems.Rows.Add(1)
            'Next
        End If
        'ds = obj.
    End Sub



    Public Function GetClosingCode() As String
        ' Try
        Dim Pre As String
        Dim Closing_id As String
        Dim Closing_Code As String
        Pre = obj.getPrefixCode("CLOSING_PREFIX", "DIVISION_SETTINGS")
        Closing_id = obj.getMaxValue("Closing_ID", "Closing_cc_Master")
        Closing_Code = Pre & "" & Closing_id
        Return Closing_Code
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        'End Try
    End Function


    Private Sub FillGridMaster()
        Try
            obj.Bind_GridBind_Val(DGVMasters, "GET_CLOSING_CC_MASTER", "@v_cc_id", "", v_the_current_Selected_CostCenter_id, "")
            DGVMasters.Columns(0).Visible = False                'Closing_ID
            DGVMasters.Columns(1).HeaderText = "Closing Code"        'Closing_CODE
            DGVMasters.Columns(1).Width = 150
            DGVMasters.Columns(2).HeaderText = "Closing Date"        'Closing_DATE
            DGVMasters.Columns(2).Width = 180
            DGVMasters.Columns(3).HeaderText = "Closing Remarks"     'Closing_REMARKS
            DGVMasters.Columns(3).Width = 430
            DGVMasters.Columns(4).HeaderText = "STATUS"          'Closing_STATUS
            DGVMasters.Columns(4).Width = 100
            DGVMasters.Columns(5).Visible = False                'DIVISION_ID
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub


    Public Sub clear_all()
        lbl_Closing_code.Text = ""
        dtpClosingdt.Value = Now()
        lblstatus.Text = ""
        txtAdjustmentRemarks.Text = ""
    End Sub

    Private Sub new_initilization()
        clear_all()
        'CreateTable()
        DGVClstkitems.DataSource = Nothing
        DGVClstkitems_Style()
        txtAdjustmentRemarks.Text = ""
        FillGridMaster()
        TBCWastageMaster.SelectTab(0)
        flag = "save"

        iclosingid = Convert.ToInt32(obj.getMaxValue("Closing_ID", "Closing_CC_Master"))
        lbl_Closing_code.Text = Convert.ToString(obj.getPrefixCode("CLOSING_PREFIX", "DIVISION_SETTINGS")) + Convert.ToString(iclosingid)
        'lbl_Closing_Date.Text = Now.ToString("dd-MMM-yyyy")
        dtpClosingdt.Value = Now()
        lblstatus.Text = "Fresh"

        obj.ComboBind(ddl_category, "SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER", "ITEM_CAT_Head_NAME", "ITEM_CAT_Head_ID", True)

        txtAdjustmentRemarks.Enabled = True
        txtAdjustmentRemarks.ReadOnly = False
        DGVClstkitems.Enabled = True
        'DGVClstkitems.ReadOnly = False
        dtpClosingdt.Enabled = True
    End Sub


    Public Sub DGVClstkitems_Style()
        Try
            If Not DtItem Is Nothing Then DtItem.Dispose()
            DtItem = New DataTable()
            DtItem.Columns.Add("Item_Id", Type.GetType("System.String"))
            DtItem.Columns.Add("Item_Code", Type.GetType("System.String"))
            DtItem.Columns.Add("Item_Name", Type.GetType("System.String"))
            DtItem.Columns.Add("um_name", Type.GetType("System.String"))
            DtItem.Columns.Add("current_stock", Type.GetType("System.String"))
            DtItem.Columns.Add("avg_rate", Type.GetType("System.String"))
            DtItem.Columns.Add("Convfact", Type.GetType("System.String"))
            DtItem.Columns.Add("Quantity", Type.GetType("System.String"))
            DtItem.Columns.Add("consumption", Type.GetType("System.String"))
            DtItem.Columns.Add("Conv_current_stock", Type.GetType("System.String"))
            DtItem.Columns.Add("Closingstockrecp", Type.GetType("System.String"))
            DtItem.Columns.Add("viewconsumption", Type.GetType("System.String"))
            DtItem.Columns.Add("check", Type.GetType("System.String"))

            DGVClstkitems.DataSource = DtItem
            DGVClstkitems.Rows.Add()

            DGVClstkitems.Cols(0).Visible = False
            DGVClstkitems.Cols(1).Visible = False

            DGVClstkitems.Cols("Item_Id").Caption = "Item Id"
            DGVClstkitems.Cols("Item_Code").Caption = "Item Code"
            DGVClstkitems.Cols("Item_Name").Caption = "Item Name"
            DGVClstkitems.Cols("UM_Name").Caption = "UOM"
            DGVClstkitems.Cols("Current_Stock").Caption = "Current Stock1"
            DGVClstkitems.Cols("Conv_Current_Stock").Caption = "Current Stock"
            DGVClstkitems.Cols("Avg_Rate").Caption = "Rate"
            DGVClstkitems.Cols("Convfact").Caption = "Convfact"
            DGVClstkitems.Cols("Quantity").Caption = "Closing Stock1"
            DGVClstkitems.Cols("ClosingStockRecp").Caption = "Closing Stock"
            DGVClstkitems.Cols("Consumption").Caption = "Consumption1"
            DGVClstkitems.Cols("ViewConsumption").Caption = "Consumption"
            DGVClstkitems.Cols("check").Caption = "check"

            DGVClstkitems.Cols("Item_Id").Width = 10
            DGVClstkitems.Cols("Item_Code").Width = 90
            DGVClstkitems.Cols("Item_Name").Width = 250
            DGVClstkitems.Cols("UM_Name").Width = 70
            DGVClstkitems.Cols("Current_Stock").Width = 100
            DGVClstkitems.Cols("Conv_Current_Stock").Width = 120
            DGVClstkitems.Cols("Avg_Rate").Width = 90
            DGVClstkitems.Cols("Convfact").Width = 90
            DGVClstkitems.Cols("Quantity").Width = 90
            DGVClstkitems.Cols("ClosingStockRecp").Width = 120
            DGVClstkitems.Cols("Consumption").Width = 90
            DGVClstkitems.Cols("ViewConsumption").Width = 120
            DGVClstkitems.Cols("check").Width = 20

            DGVClstkitems.Cols("Item_Id").Visible = False
            DGVClstkitems.Cols("Item_Code").Visible = True
            DGVClstkitems.Cols("Item_Name").Visible = True
            DGVClstkitems.Cols("UM_Name").Visible = True
            DGVClstkitems.Cols("Current_Stock").Visible = False
            DGVClstkitems.Cols("Conv_Current_Stock").Visible = True
            DGVClstkitems.Cols("Avg_Rate").Visible = True
            DGVClstkitems.Cols("Convfact").Visible = False
            DGVClstkitems.Cols("Quantity").Visible = False
            DGVClstkitems.Cols("ClosingStockRecp").Visible = True
            DGVClstkitems.Cols("Consumption").Visible = False
            DGVClstkitems.Cols("ViewConsumption").Visible = True
            DGVClstkitems.Cols("check").Visible = False

            DGVClstkitems.Cols("Item_Id").AllowEditing = False
            DGVClstkitems.Cols("Item_Code").AllowEditing = False
            DGVClstkitems.Cols("Item_Name").AllowEditing = False
            DGVClstkitems.Cols("UM_Name").AllowEditing = False
            DGVClstkitems.Cols("Current_Stock").AllowEditing = False
            DGVClstkitems.Cols("Conv_Current_Stock").AllowEditing = False
            DGVClstkitems.Cols("Avg_Rate").AllowEditing = True
            DGVClstkitems.Cols("Convfact").AllowEditing = False
            DGVClstkitems.Cols("Quantity").AllowEditing = False
            DGVClstkitems.Cols("ClosingStockRecp").AllowEditing = True
            DGVClstkitems.Cols("Consumption").AllowEditing = False
            DGVClstkitems.Cols("ViewConsumption").AllowEditing = True
            DGVClstkitems.Cols("check").AllowEditing = False


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub grid_format()
        Try
            DGVClstkitems.Cols("Item_Id").Caption = "Item Id"
            DGVClstkitems.Cols("Item_Code").Caption = "Item Code"
            DGVClstkitems.Cols("Item_Name").Caption = "Item Name"
            DGVClstkitems.Cols("UM_Name").Caption = "UOM"
            DGVClstkitems.Cols("Current_Stock").Caption = "Current Stock1"
            DGVClstkitems.Cols("Conv_Current_Stock").Caption = "Current Stock"
            DGVClstkitems.Cols("Avg_Rate").Caption = "Rate"
            DGVClstkitems.Cols("Convfact").Caption = "Convfact"
            DGVClstkitems.Cols("Quantity").Caption = "Closing Stock1"
            DGVClstkitems.Cols("ClosingStockRecp").Caption = "Closing Stock"
            DGVClstkitems.Cols("Consumption").Caption = "Consumption1"
            DGVClstkitems.Cols("ViewConsumption").Caption = "Consumption"
            DGVClstkitems.Cols("check").Caption = "check"

            DGVClstkitems.Cols("Item_Id").Width = 10
            DGVClstkitems.Cols("Item_Code").Width = 90
            DGVClstkitems.Cols("Item_Name").Width = 250
            DGVClstkitems.Cols("UM_Name").Width = 70
            DGVClstkitems.Cols("Current_Stock").Width = 100
            DGVClstkitems.Cols("Conv_Current_Stock").Width = 120
            DGVClstkitems.Cols("Avg_Rate").Width = 90
            DGVClstkitems.Cols("Convfact").Width = 90
            DGVClstkitems.Cols("Quantity").Width = 90
            DGVClstkitems.Cols("ClosingStockRecp").Width = 120
            DGVClstkitems.Cols("Consumption").Width = 90
            DGVClstkitems.Cols("ViewConsumption").Width = 120
            DGVClstkitems.Cols("check").Width = 20

            DGVClstkitems.Cols("Item_Id").Visible = False
            DGVClstkitems.Cols("Item_Code").Visible = True
            DGVClstkitems.Cols("Item_Name").Visible = True
            DGVClstkitems.Cols("UM_Name").Visible = True
            DGVClstkitems.Cols("Current_Stock").Visible = False
            DGVClstkitems.Cols("Conv_Current_Stock").Visible = True
            DGVClstkitems.Cols("Avg_Rate").Visible = True
            DGVClstkitems.Cols("Convfact").Visible = False
            DGVClstkitems.Cols("Quantity").Visible = False
            DGVClstkitems.Cols("ClosingStockRecp").Visible = True
            DGVClstkitems.Cols("Consumption").Visible = False
            DGVClstkitems.Cols("ViewConsumption").Visible = True
            DGVClstkitems.Cols("check").Visible = False

            DGVClstkitems.Cols("Item_Id").AllowEditing = False
            DGVClstkitems.Cols("Item_Code").AllowEditing = False
            DGVClstkitems.Cols("Item_Name").AllowEditing = False
            DGVClstkitems.Cols("UM_Name").AllowEditing = False
            DGVClstkitems.Cols("Current_Stock").AllowEditing = False
            DGVClstkitems.Cols("Conv_Current_Stock").AllowEditing = False
            DGVClstkitems.Cols("Avg_Rate").AllowEditing = True
            DGVClstkitems.Cols("Convfact").AllowEditing = False
            DGVClstkitems.Cols("Quantity").AllowEditing = False
            DGVClstkitems.Cols("ClosingStockRecp").AllowEditing = True
            DGVClstkitems.Cols("Consumption").AllowEditing = False
            DGVClstkitems.Cols("ViewConsumption").AllowEditing = True
            DGVClstkitems.Cols("check").AllowEditing = False

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVClstkitems_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            'Dim iRowindex As Int32
            ''If flag = "save" Then
            '    If e.KeyCode = Keys.Space Then
            '        iRowindex = DGVClstkitems.CurrentRow.Index
            '        frm_Show_search.qry = "Select Item_master.ITEM_ID,Item_master.ITEM_CODE as [Item Code],Item_master.ITEM_NAME  as [Item Name] from Item_master inner join item_detail on item_master.item_id = item_detail.item_id " 'where item_detail.div_id = '" + Convert.ToString(v_the_curre) + "'"
            '        frm_Show_search.extra_condition = ""
            '        frm_Show_search.ret_column = "Item_ID"
            '        frm_Show_search.column_name = "ITEM_NAME"
            '        frm_Show_search.cols_width = "80,300"
            '        frm_Show_search.cols_no_for_width = "1,2"
            '        frm_Show_search.ShowDialog()
            '   get_row(frm_Show_search.search_result)
            '    dtpClosingdt.Enabled = False
            'End If
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGVClstkitems_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim introwCount = DGVClstkitems.Rows.Count - 1
    End Sub

    Private Sub frm_Closing_Stock_CC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TBCWastageMaster.SelectTab(0)
            ' obj.FormatGrid(DGVClstkitems)
            'obj.FormatGrid(DGVMasters)
            'rights = clsObj.Get_Form_Rights(Me.Name)
            new_initilization()
            'DGVClstkitems_Style()

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVMasters_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVMasters.DoubleClick
        Try
            'Dim ds As DataSet
            'Dim dtclosing As New DataTable
            'Dim dtclosingdtl As New DataTable
            'iclosingid = Convert.ToInt32(DGVMasters.SelectedRows.Item(0).Cells(0).Value)
            'flag = "update"
            'ds = obj.fill_Data_set("GET_Closing_DETAIL", "@V_CLOSING_ID", iclosingid)
            'If ds.Tables.Count > 0 Then
            '    'Bind the MRS Infromation
            '    dtclosing = ds.Tables(0)
            '    If (Convert.ToString(dtclosing.Rows(0)("CLOSING_STATUS")) = "Fresh") Then
            '        txtAdjustmentRemarks.ReadOnly = False
            '        DGVClstkitems.Enabled = True
            '    Else
            '        txtAdjustmentRemarks.ReadOnly = True
            '        DGVClstkitems.Enabled = False
            '        dtpClosingdt.Enabled = False
            '    End If
            '    iclosingid = Convert.ToString(dtclosing.Rows(0)("closing_id"))

            '    lbl_Closing_code.Text = Convert.ToString(dtclosing.Rows(0)("closing_code"))
            '    'lbl_Closing_Date.Text = Convert.ToString(dtclosing.Rows(0)("closing_date"))
            '    dtpClosingdt.Value = Convert.ToDateTime(dtclosing.Rows(0)("closing_date"))
            '    lblstatus.Text = Convert.ToString(dtclosing.Rows(0)("CLOSING_STATUS"))
            '    txtAdjustmentRemarks.Text = Convert.ToString(dtclosing.Rows(0)("CLOSING_REMARKS"))
            '    'Bind the MRS Item Grid
            '    'DGVMRSItem_style()
            '    DGVClstkitems_Style()
            '    dtclosingdtl = ds.Tables(1)
            '    Dim iRowCount As Int32
            '    Dim iRow As Int32
            '    iRowCount = dtclosingdtl.Rows.Count
            '    For iRow = 0 To iRowCount - 1
            '        If dtclosingdtl.Rows.Count > 0 Then

            '            Dim rowindex As Integer = DGVClstkitems.Rows.Add()
            '            DGVClstkitems.Rows(rowindex).Cells(0).Value = Convert.ToInt32(dtclosingdtl.Rows(iRow)("ITEM_ID"))
            '            DGVClstkitems.Rows(rowindex).Cells("Item_Code").Value = Convert.ToString(dtclosingdtl.Rows(iRow)("ITEM_CODE"))
            '            DGVClstkitems.Rows(rowindex).Cells("Item_name").Value = Convert.ToString(dtclosingdtl.Rows(iRow)("ITEM_Name"))
            '            'DGVClstkitems.Rows(rowindex).Cells(3).Value = Convert.ToString(dtclosingdtl.Rows(iRow)("item_cat_name"))
            '            DGVClstkitems.Rows(rowindex).Cells("Um_name").Value = Convert.ToString(dtclosingdtl.Rows(iRow)("UM_Name"))
            '            DGVClstkitems.Rows(rowindex).Cells("Current_Stock").Value = Convert.ToString(dtclosingdtl.Rows(iRow)("Current_Stock"))
            '            DGVClstkitems.Rows(rowindex).Cells("Avg_Rate").Value = Convert.ToString(dtclosingdtl.Rows(iRow)("Avg_Rate"))
            '            DGVClstkitems.Rows(rowindex).Cells("Quantity").Value = Convert.ToInt32(dtclosingdtl.Rows(iRow)("Quantity"))
            '            DGVClstkitems.Rows(rowindex).Cells("Conv_Current_Stock").Value = Convert.ToInt32(dtclosingdtl.Rows(iRow)("Conv_Current_Stock"))
            '            DGVClstkitems.Rows(rowindex).Cells("Convfact").Value = Convert.ToInt32(dtclosingdtl.Rows(iRow)("Conv_factor"))
            '            DGVClstkitems.Rows(rowindex).Cells("ClosingStockRecp").Value = Convert.ToInt32(dtclosingdtl.Rows(iRow)("closing_stock_recp"))
            '        End If
            '    Next iRow
            '    TBCWastageMaster.SelectTab(1)
            'End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVClstkitems_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

       

    End Sub


    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim qry As String = ""
        qry = "SELECT  CLOSING_ID," & _
                " (CLOSING_CODE + CAST(CLOSING_NO AS VARCHAR)) AS CLOSING_CODE," & _
                " REPLACE(CONVERT(VARCHAR, CLOSING_DATE, 106), ' ', '-') AS CLOSING_DATE," & _
                " CLOSING_REMARKS," & _
                " CASE WHEN CLOSING_STATUS = 1 THEN 'Fresh'" & _
                     " WHEN CLOSING_STATUS = 2 THEN 'Freeze'" & _
                " END CLOSING_STATUS," & _
        " COSTCENTER_ID " & _
        " FROM CLOSING_CC_MASTER " & _
       " WHERE (CLOSING_CODE + dbo.fn_Format(CLOSING_DATE) + CLOSING_REMARKS + CAST(CLOSING_STATUS AS VARCHAR)) LIKE '%" & txtSearch.Text & "%'" & _
        " and CostCenter_ID = " & v_the_current_Selected_CostCenter_id & ""

        obj.GridBind(DGVMasters, qry)
    End Sub
    Private Sub ddl_category_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddl_category.SelectedIndexChanged

        If ddl_category.SelectedIndex > 0 Then

            Dim txtsearch As String
            txtsearch = "%%"

            DGVClstkitems_Style()
            ds1 = obj.fill_Data_set("Proc_get_items_catwise", "@cat_id,@cc_id,@date,@txtsearch", ddl_category.SelectedValue & "," & v_the_current_Selected_CostCenter_id & "," & dtpClosingdt.Value.ToString("dd-MMM-yyyy") & "," & txtsearch)

            If ds1.Tables(0).Rows.Count - 1 > 0 Then
                For irow As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = DtItem.NewRow()
                    dr("Item_Id") = Convert.ToString(ds1.Tables(0).Rows(irow)("Item_Id"))
                    dr("Item_Code") = Convert.ToString(ds1.Tables(0).Rows(irow)("Item_Code"))
                    dr("Item_Name") = Convert.ToString((ds1.Tables(0).Rows(irow)("Item_Name")))
                    dr("Um_name") = ds1.Tables(0).Rows(irow)("um_name")
                    dr("current_stock") = ds1.Tables(0).Rows(irow)("current_stock")
                    dr("avg_rate") = ds1.Tables(0).Rows(irow)("avg_rate")
                    dr("Convfact") = ds1.Tables(0).Rows(irow)("Conv_fac_recp")
                    dr("quantity") = ds1.Tables(0).Rows(irow)("current_stock")
                    dr("consumption") = 0
                    dr("Conv_current_stock") = Convert.ToDouble(ds1.Tables(0).Rows(irow)("Conv_fac_recp")) * Convert.ToDouble(ds1.Tables(0).Rows(irow)("current_stock"))
                    dr("Closingstockrecp") = Convert.ToDouble(ds1.Tables(0).Rows(irow)("Conv_fac_recp")) * Convert.ToDouble(ds1.Tables(0).Rows(irow)("current_stock"))
                    dr("viewconsumption") = 0
                    dr("check") = 0
                    DtItem.Rows.Add(dr)
                Next
            End If

            DGVClstkitems.DataSource = DtItem
            DGVClstkitems.Rows.Fixed = 1
            grid_format()

        End If
    End Sub

    Private Sub DGVClstkitems_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub



    Private Sub CreateTable()
     
    End Sub

    Private Sub DGVClstkitems_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
        'DGVClstkitems_Style()
    End Sub

    Private Sub DGVClstkitems_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles DGVClstkitems.AfterEdit

        If e.Col = 11 Then
            If Not IsDBNull(DGVClstkitems.Rows(e.Row)("Item_id")) Then
                If IsNumeric(DGVClstkitems.Rows(e.Row)("ClosingStockRecp")) And IsNumeric(DGVClstkitems.Rows(e.Row)("Viewconsumption")) Then

                    If (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("ClosingStockRecp"))) > (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Conv_current_stock"))) Then
                        DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = 0
                    End If

                    If IsNumeric(DGVClstkitems.Rows(e.Row)("Conv_current_stock")) Then
                        DGVClstkitems.Rows(e.Row)("Quantity") = (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("ClosingStockRecp"))) / (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("convfact")))
                        DGVClstkitems.Rows(e.Row)("consumption") = (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("current_stock")) - Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Quantity")))
                        DGVClstkitems.Rows(e.Row)("Viewconsumption") = (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Conv_current_stock")) - Convert.ToDouble(DGVClstkitems.Rows(e.Row)("ClosingStockRecp")))
                    Else
                        DGVClstkitems.Rows(e.Row)("current_stock") = 0.0
                        DGVClstkitems.Rows(e.Row)("Viewconsumption") = 0.0
                    End If
                Else
                    MsgBox("Please enter numeric value.", MsgBoxStyle.Information)
                    DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Conv_current_stock"))
                    DGVClstkitems.Rows(e.Row)("Viewconsumption") = 0
                End If
            Else
                DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = ""
                DGVClstkitems.Rows(e.Row)("Viewconsumption") = ""
            End If
        End If

        If e.Col = 12 Then
            If Not IsDBNull(DGVClstkitems.Rows(e.Row)("Item_id")) Then


                If IsNumeric(DGVClstkitems.Rows(e.Row)("ClosingStockRecp")) And IsNumeric(DGVClstkitems.Rows(e.Row)("Viewconsumption")) Then


                    If (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("ClosingStockRecp"))) > (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Conv_current_stock"))) Then
                        DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = 0
                    End If

                    If IsNumeric(DGVClstkitems.Rows(e.Row)("Conv_current_stock")) Then
                        DGVClstkitems.Rows(e.Row)("consumption") = (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("viewconsumption"))) / (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("convfact")))
                        DGVClstkitems.Rows(e.Row)("Quantity") = (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("current_stock")) - Convert.ToDouble(DGVClstkitems.Rows(e.Row)("consumption")))
                        DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Conv_current_stock")) - Convert.ToDouble(DGVClstkitems.Rows(e.Row)("viewconsumption")))
                    Else
                        DGVClstkitems.Rows(e.Row)("current_stock") = 0.0
                        DGVClstkitems.Rows(e.Row)("Viewconsumption") = 0.0
                    End If
                Else
                    MsgBox("Please enter numeric value.", MsgBoxStyle.Information)
                    DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Conv_current_stock"))
                    DGVClstkitems.Rows(e.Row)("Viewconsumption") = 0
                End If
            Else
                DGVClstkitems.Rows(e.Row)("ClosingStockRecp") = ""
                DGVClstkitems.Rows(e.Row)("Viewconsumption") = ""
            End If
        End If

        'If (Convert.ToDouble(DGVClstkitems.Rows(DGVClstkitems.RowSel)("Viewconsumption"))) < 0 Then
        '    DGVClstkitems.Rows(DGVClstkitems.RowSel)("Avg_Rate").r = False
        'Else
        '    DGVClstkitems.Item("Avg_Rate", Row).ReadOnly = True
        'End If

        If IsNumeric(DGVClstkitems.Rows(e.Row)("Viewconsumption")) Then
            If (Convert.ToDouble(DGVClstkitems.Rows(e.Row)("Viewconsumption"))) <> 0 Then
                DGVClstkitems.Rows(e.Row)("check") = 1
            End If
        End If

    End Sub


   
End Class
