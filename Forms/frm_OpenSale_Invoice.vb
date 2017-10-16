Imports MMSPlus.Sale_Invoice
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid
Imports System.Text.RegularExpressions
Public Class frm_openSale_Invoice

    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New cls_Sale_Invoice_master
    Dim prpty As cls_Sale_Invoice_prop
    Dim flag As String
    Dim Si_ID As Int16
    Dim NEWCUST As Int16 = 0
    Dim dtable_Item_List As DataTable
    Dim gstnoRegex As New Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")
    Dim mobileRegex As New Regex("^\d{10}$")
    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub


    Private Sub fill_grid(Optional ByVal condition As String = "")
        Try

            Dim strsql As String

            strsql = "SELECT * FROM (SELECT SI_ID," & _
            "(SI_CODE+CAST(SI_NO AS VARCHAR)) AS InvNo,('DC/'+CAST(DC_GST_NO AS VARCHAR) ) AS [DC NO]," & _
            " dbo.fn_Format(dbo.SALE_INVOICE_MASTER.CREATION_DATE) AS [INV DATE]," & _
            " NET_AMOUNT AS Amount,ACC_NAME Customer,CASE WHEN INVOICE_STATUS =1 THEN 'Fresh'  WHEN INVOICE_STATUS =2 THEN 'Pending' WHEN INVOICE_STATUS =3 THEN 'Clear'  WHEN INVOICE_STATUS =4 THEN 'Cancel' END AS Status FROM dbo.SALE_INVOICE_MASTER " & _
            "JOIN dbo.ACCOUNT_MASTER ON ACCOUNT_MASTER.ACC_ID=dbo.SALE_INVOICE_MASTER.CUST_ID)tb " & _
            "WHERE (CAST(SI_ID AS varchar) +InvNo+[DC NO]+[INV DATE]+ CAST(tb.Amount AS VARCHAR)+tb.Customer+tb.Status) LIKE '%" & condition & "%'  order by 1"

            Dim dt As DataTable = obj.Fill_DataSet(strsql).Tables(0)

            flxList.DataSource = dt

            flxList.Columns(0).Width = 40
            flxList.Columns(1).Width = 130
            flxList.Columns(2).Width = 100
            flxList.Columns(3).Width = 80
            flxList.Columns(4).Width = 100
            flxList.Columns(5).Width = 300
            flxList.Columns(6).Width = 70

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Private Sub frm_Sale_Invoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            obj.FormatGrid(flxItems)
            table_style()

            CustomerBind()
            cmbSupplier.Visible = True
            txtcustomer_name.Visible = False
            CityBind()
            new_initilization()
            fill_grid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading_Error)
        End Try
    End Sub
    Public Sub CustomerBind()
        clsObj.ComboBind(cmbSupplier, "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" & AccountGroups.Customers & " Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
    End Sub
    Public Sub CityBind()
        clsObj.ComboBind(cmbCity, "Select CITY_ID,CITY_NAME from CITY_MASTER Order by CITY_NAME", "CITY_NAME", "CITY_ID", True)
    End Sub
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initilization()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        CalculateAmount()
        Dim cmd As SqlCommand
        Try

            If Validation() = False Then
                Exit Sub
            End If

            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If

                prpty = New cls_Sale_Invoice_prop

                Dim ds1 As DataSet = obj.FillDataSet("Select isnull(max(SI_ID),0) + 1 from dbo.SALE_INVOICE_MASTER")
                Si_ID = Convert.ToInt32(ds1.Tables(0).Rows(0)(0))
                prpty.SI_ID = Si_ID

                Dim ds As New DataSet()
                ds = obj.fill_Data_set("GET_INV_NO", "@DIV_ID", v_the_current_division_id)
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("Invoice series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                    ds.Dispose()
                    Exit Sub
                Else
                    If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                        MsgBox("Invoice series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                        MsgBox("Invoice series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    Else
                        prpty.SI_CODE = ds.Tables(0).Rows(0)(0).ToString()
                        prpty.SI_NO = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                        ds.Dispose()
                    End If
                End If

                prpty.DC_GST_NO = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                prpty.SI_DATE = Now

                If NEWCUST = 0 Then
                    prpty.CUST_ID = cmbSupplier.SelectedValue
                Else
                    Dim dscust As DataSet = clsObj.GetDCDetail_remote("Select isnull(max(ACC_ID),0) + 1 from dbo.ACCOUNT_MASTER")
                    prpty.CUST_ID = Convert.ToInt32(dscust.Tables(0).Rows(0)(0))

                    clsObj.Insert_New_Customer_Remote(prpty.CUST_ID, txtcustomer_name.Text, txtAddress.Text, txt_txtphoneNo.Text, txtGstNo.Text, Convert.ToInt32(cmbCity.SelectedValue))
                    clsObj.Insert_New_Customer(prpty.CUST_ID, txtcustomer_name.Text, txtAddress.Text, txt_txtphoneNo.Text, txtGstNo.Text, Convert.ToInt32(cmbCity.SelectedValue))

                End If

                prpty.INVOICE_STATUS = Convert.ToInt32(GlobalModule.InvoiceStatus.Clear)
                prpty.REMARKS = ""
                prpty.PAYMENTS_REMARKS = ""

                If rbtn_Cash.Checked Then
                    prpty.SALE_TYPE = "Cash"
                Else
                    prpty.SALE_TYPE = "Credit"
                End If

                prpty.GROSS_AMOUNT = Convert.ToDouble(lblItemValue.Text)
                prpty.VAT_AMOUNT = Convert.ToDouble(lblVatAmount.Text)
                prpty.NET_AMOUNT = Convert.ToDouble(lblNetAmount.Text)
                prpty.IS_SAMPLE = 0
                prpty.DELIVERY_NOTE_NO = 0
                prpty.VAT_CST_PER = 0
                prpty.SAMPLE_ADDRESS = txtAddress.Text
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.CREATION_DATE = Now
                prpty.MODIFIED_BY = ""
                prpty.MODIFIED_DATE = NULL_DATE
                prpty.DIVISION_ID = v_the_current_division_id
                prpty.TRANSPORT = txtTransport.Text
                prpty.VEHICLE_NO = txtvechicle_no.Text
                prpty.SHIPP_ADD_ID = 0
                prpty.INV_TYPE = cmbinvtype.SelectedItem
                prpty.LR_NO = txtLRNO.Text
                prpty.dtable_Item_List = dtable_Item_List
                'clsObj.Insert_SALE_INVOICE_MASTER(prpty)

            End If

            If flag = "save" Then
                If MsgBox("Invoice information has been Saved." & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptInvoicePrint, "Si_ID", CStr(prpty.SI_ID), CStr(enmDataType.D_int))
                End If
            Else
                MsgBox("You Can't edit this.")
            End If
            fill_grid()

            new_initilization()
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                If flxList.SelectedRows.Count > 0 Then
                    'obj.RptShow(enmReportName.RptInvoicePrint, "Si_ID", CStr(flxList.SelectedRows(0).Cells("Si_id").Value), CStr(enmDataType.D_int))
                    obj.RptShow(enmReportName.RptInvoicePrint, "Si_ID", CStr(flxList("Si_ID", flxList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptInvoicePrint, "Si_ID", CStr(Si_ID), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub new_initilization()


        lbl_TransferDate.Text = Now.ToString("dd-MMM-yyyy")
        txt_txtphoneNo.Text = ""
        cmbSupplier.SelectedIndex = 0
        txtAddress.Text = ""
        txtvechicle_no.Text = ""
        txtGstNo.Text = ""
        txtTransport.Text = ""
        txtLRNO.Text = ""
        txtcustomer_name.Text = ""
        cmbinvtype.SelectedIndex = 0
        cmbCity.SelectedIndex = 0
        NEWCUST = 0
        btnAddNew.Visible = True
        dtable_Item_List.Rows.Clear()
        'dtable_Item_List.Rows.Add()
        TabControl1.SelectTab(1)
        lblItemValue.Text = 0
        lblVatAmount.Text = 0
        lblNetAmount.Text = 0
        lblTotalDisc.Text = 0
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''TO GET Inv NO'''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim ds As New DataSet()
        ds = obj.fill_Data_set("GET_INV_NO", "@DIV_ID", v_the_current_division_id)
        If ds.Tables(0).Rows.Count = 0 Then
            lbl_INVNo.Text = "Invoice series does not exists"
            ds.Dispose()
            Exit Sub
        Else
            If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                lbl_INVNo.Text = "Invoice series does not exists"
                ds.Dispose()
                Exit Sub
            ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                lbl_INVNo.Text = "Invoice series has been completed"
                ds.Dispose()
                Exit Sub
            Else
                lbl_INVNo.Text = ds.Tables(0).Rows(0)(0).ToString() & (Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1).ToString
                ds.Dispose()
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''TO GET INV NO'''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        flag = "save"
    End Sub
    Private Function Validation() As Boolean

        If cmbSupplier.SelectedIndex = 0 And NEWCUST = 0 Then
            MsgBox("Select Customer to Invoice.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        ElseIf txtcustomer_name.Text.Length <= 0 And NEWCUST = 1 Then
            MsgBox("Enter New Customer to Invoice.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If

        If Not String.IsNullOrEmpty(txt_txtphoneNo.Text.Trim) Then
            If Not mobileRegex.IsMatch(txt_txtphoneNo.Text) Then
                MsgBox("Phone number is not valid. Try again after entering valid number.", MsgBoxStyle.Information, "Invalid Phone Format!!!")
                Return False
            End If
        End If

        If Not String.IsNullOrEmpty(txtGstNo.Text.Trim) Then
            If Not gstnoRegex.IsMatch(txtGstNo.Text) Then
                MsgBox("GST No. is not valid. Try again after entering valid GST no.", MsgBoxStyle.Information, "Invalid GST Format!!!")
                Return False
            End If
        End If

        If cmbCity.SelectedIndex = 0 Then
            MsgBox("Select City Name.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If

        If cmbinvtype.SelectedIndex = 0 Then
            MsgBox("Select Invoice Type.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If

        Dim dt As DataTable
        dt = dtable_Item_List.Copy
        For j As Integer = 0 To dt.Rows.Count
            Dim i As Integer
again:
            For i = 0 To dt.Rows.Count - 1
                If IsNumeric(dt.Rows(i)("transfer_qty")) Then
                    If Convert.ToDouble(dt.Rows(i)("transfer_qty")) <= 0 Then
                        dt.Rows.RemoveAt(i)
                        GoTo again
                    End If
                End If
            Next
        Next

        If dt.Rows.Count <= 0 Then
            MsgBox("Please transfer atleast one item.", MsgBoxStyle.Information)
            Return False
        End If

        Return True

    End Function


    Private Sub table_style()
        Try
            If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()
            dtable_Item_List = New DataTable()
            dtable_Item_List.Columns.Add("Item_Id", GetType(System.Int32))
            dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
            dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
            dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
            dtable_Item_List.Columns.Add("Batch_no", GetType(System.String))
            dtable_Item_List.Columns.Add("Batch_Qty", GetType(System.Double))
            dtable_Item_List.Columns.Add("transfer_Qty", GetType(System.Decimal))
            dtable_Item_List.Columns.Add("Expiry_date", GetType(System.String))
            dtable_Item_List.Columns.Add("Stock_Detail_Id", GetType(System.Int32))
            dtable_Item_List.Columns.Add("Item_Rate", GetType(System.Decimal))
            dtable_Item_List.Columns.Add("DType", GetType(System.String))
            dtable_Item_List.Columns.Add("DISC", GetType(System.Decimal))
            dtable_Item_List.Columns.Add("Amount", GetType(System.Decimal))
            dtable_Item_List.Columns.Add("GST", GetType(System.Decimal))
            dtable_Item_List.Columns.Add("GST_Amount", GetType(System.Decimal))
            dtable_Item_List.Columns.Add("HsnCodeId", GetType(System.Int32))
            dtable_Item_List.Columns.Add("LandingAmt", GetType(System.Decimal))

            dtable_Item_List.Rows.Add()
            flxItems.DataSource = dtable_Item_List
            format_grid()

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub


    Private Sub format_grid()

        flxItems.Cols(0).Width = 10
        flxItems.Cols("Item_Id").Visible = False
        flxItems.Cols("Stock_Detail_Id").Visible = True

        flxItems.Cols("Item_Code").Caption = "Code"
        flxItems.Cols("Item_Name").Caption = "Description"
        flxItems.Cols("UM_Name").Caption = "UOM"
        flxItems.Cols("Batch_no").Caption = "Batch No"
        flxItems.Cols("Expiry_date").Caption = "Expiry Date"
        flxItems.Cols("batch_qty").Caption = "Batch Qty"
        flxItems.Cols("transfer_Qty").Caption = "Transfer Qty"
        flxItems.Cols("Item_Rate").Caption = "Rate"
        flxItems.Cols("DType").Caption = "DType"
        flxItems.Cols("DISC").Caption = "DISC"
        flxItems.Cols("GST").Caption = "GST% "
        flxItems.Cols("GST_Amount").Caption = "GST Amt"

        flxItems.Cols("Amount").Caption = "Amount"
        flxItems.Cols("HsnCodeId").Visible = False
        flxItems.Cols("Amount").AllowEditing = False
        flxItems.Cols("DType").AllowEditing = True
        flxItems.Cols("DType").ComboList = "P|A"

        flxItems.Cols("Item_Code").AllowEditing = False
        flxItems.Cols("Item_Name").AllowEditing = False
        flxItems.Cols("UM_Name").AllowEditing = False
        flxItems.Cols("Batch_no").AllowEditing = False
        flxItems.Cols("Expiry_date").AllowEditing = False
        flxItems.Cols("Expiry_date").Visible = False
        flxItems.Cols("batch_qty").AllowEditing = False
        flxItems.Cols("Stock_Detail_Id").AllowEditing = False
        flxItems.Cols("transfer_Qty").AllowEditing = True
        flxItems.Cols("Item_Rate").AllowEditing = True

        flxItems.Cols("DType").AllowEditing = True
        flxItems.Cols("DISC").AllowEditing = True
        flxItems.Cols("GST").AllowEditing = False
        flxItems.Cols("GST_Amount").AllowEditing = False
        flxItems.Cols("LandingAmt").AllowEditing = False


        flxItems.Cols("Item_Id").Width = 40
        flxItems.Cols("Item_Code").Width = 60
        flxItems.Cols("Item_Name").Width = 230
        flxItems.Cols("UM_Name").Width = 35
        flxItems.Cols("Batch_No").Width = 70
        flxItems.Cols("Amount").Width = 60
        flxItems.Cols("Batch_Qty").Width = 55
        flxItems.Cols("Stock_Detail_Id").Width = 60
        flxItems.Cols("transfer_Qty").Width = 70
        flxItems.Cols("Item_Rate").Width = 50
        flxItems.Cols("DType").Width = 40
        flxItems.Cols("DISC").Width = 45
        flxItems.Cols("GST").Width = 40
        flxItems.Cols("GST_Amount").Width = 50
        flxItems.Cols("LandingAmt").Width = 70
        flxItems.Cols("Stock_Detail_Id").Visible = False

    End Sub


    Private Sub flxItems_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles flxItems.AfterDataRefresh

    End Sub
    Private Sub generate_tree()
        flxItems.DataSource = Nothing
        flxItems.DataSource = dtable_Item_List
        format_grid()

        If flxItems.Rows.Count > 1 Then
            flxItems.Tree.Style = TreeStyleFlags.CompleteLeaf
            flxItems.Tree.Column = 2
            flxItems.AllowMerging = AllowMergingEnum.None
            Dim totalOn As Integer = flxItems.Cols("Batch_Qty").SafeIndex
            flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            totalOn = flxItems.Cols("transfer_Qty").SafeIndex
            flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

            Dim cs1 As C1.Win.C1FlexGrid.CellStyle
            cs1 = Me.flxItems.Styles.Add("Item_Rate")
            'cs1.ForeColor = Color.White
            cs1.BackColor = Color.Orange
            cs1.Border.Style = BorderStyleEnum.Raised

            Dim cs2 As C1.Win.C1FlexGrid.CellStyle
            cs2 = Me.flxItems.Styles.Add("DISC")
            'cs2.ForeColor = Color.White
            cs2.BackColor = Color.Gold
            cs2.Border.Style = BorderStyleEnum.Raised

            Dim cs3 As C1.Win.C1FlexGrid.CellStyle
            cs3 = Me.flxItems.Styles.Add("DType")
            'cs3.ForeColor = Color.White
            cs3.BackColor = Color.Gold
            cs3.Border.Style = BorderStyleEnum.Raised

            Dim cs As C1.Win.C1FlexGrid.CellStyle
            cs = Me.flxItems.Styles.Add("transfer_Qty")
            'cs.ForeColor = Color.White
            cs.BackColor = Color.LimeGreen
            cs.Border.Style = BorderStyleEnum.Raised

            Dim i As Integer
            For i = 1 To flxItems.Rows.Count - 1
                If Not flxItems.Rows(i).IsNode Then
                    flxItems.SetCellStyle(i, flxItems.Cols("Item_Rate").SafeIndex, cs1)
                    flxItems.SetCellStyle(i, flxItems.Cols("DISC").SafeIndex, cs2)
                    flxItems.SetCellStyle(i, flxItems.Cols("DType").SafeIndex, cs3)
                    flxItems.SetCellStyle(i, flxItems.Cols("transfer_Qty").SafeIndex, cs)
                End If
            Next
        End If
    End Sub

    Private Sub flxItems_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles flxItems.KeyDown
        Try
            'If cmbSupplier.SelectedIndex > 0 Then

            Dim iRowindex As Int32
            If flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = flxItems.Row

                    'frm_Show_search.qry = " SELECT " & _
                    '                   " ITEM_MASTER.ITEM_ID,   " & _
                    '                   " ITEM_MASTER.ITEM_CODE, " & _
                    '                   " ITEM_MASTER.ITEM_NAME, " & _
                    '                   " ITEM_MASTER.ITEM_DESC, " & _
                    '                   " UNIT_MASTER.UM_Name,   " & _
                    '                   " ITEM_CATEGORY.ITEM_CAT_NAME, " & _
                    '                   " ITEM_MASTER.IS_STOCKABLE " & _
                    '           " FROM " & _
                    '                   " ITEM_MASTER " & _
                    '                   " INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID " & _
                    '                   " INNER JOIN ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID " & _
                    '                    "INNER JOIN ITEM_DETAIL ON ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID "

                    frm_Show_search.qry = "SELECT IM.ITEM_ID,IM.ITEM_CODE," & _
                                       " IM.ITEM_NAME,UM.UM_Name,CM.ITEM_CAT_NAME," & _
                                       " IM.DIVISION_ID, 0.00 as Quantity, Sale_RATE as Rate " & _
                                       " FROM ITEM_MASTER  AS IM INNER JOIN " & _
                                       " ITEM_DETAIL AS ID ON IM.ITEM_ID = ID.ITEM_ID  INNER JOIN  UNIT_MASTER AS UM " & _
                                       " ON IM.UM_ID = UM.UM_ID INNER JOIN ITEM_CATEGORY AS CM ON " & _
                                       " IM.ITEM_CATEGORY_ID = CM.ITEM_CAT_ID"

                    frm_Show_search.column_name = "Item_Name"
                    frm_Show_search.extra_condition = ""
                    frm_Show_search.ret_column = "Item_ID"
                    frm_Show_search.item_rate_column = "Rate"
                    frm_Show_search.ShowDialog()
                    If Not check_item_exist(frm_Show_search.search_result) Then
                        get_row(frm_Show_search.search_result, 0, frm_Show_search.item_rate)
                    End If
                End If

            End If
            If e.KeyCode = Keys.Delete Then

                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want to remove """ & flxItems.Rows(flxItems.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = flxItems.Rows(flxItems.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(flxItems.DataSource, DataTable)
                    If Not dt Is Nothing Then
                        For Each dr As DataRow In dt.Rows
                            If Convert.ToString(dr("item_code")) = item_code Then
                                dr.Delete()
                                dt.AcceptChanges()
                                GoTo restart
                            End If
                        Next
                        '        dt.AcceptChanges()
                    End If
                End If
            End If

            'Else
            '    MsgBox("Please select Customer first.", MsgBoxStyle.Information)
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub get_row(ByVal item_id As Integer, ByVal Wastage_id As Integer, ByVal itemRate As Decimal)
        Try
            Dim ds As DataSet
            Dim ds2 As DataSet
            Dim sqlqry As String
            sqlqry = "SELECT  " & _
                                        " IM.ITEM_ID , " & _
                                        " IM.ITEM_CODE , " & _
                                        " IM.ITEM_NAME , " & _
                                        " UM.UM_Name , " & _
                                        " SD.Batch_no , " & _
                                        " dbo.fn_Format(SD.Expiry_date) AS Expiry_Date, " & _
                                        " dbo.Get_Average_Rate_as_on_date(IM.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," & _
                                        " SD.Balance_Qty, " & _
                                        " 0.00  as transfer_qty, " & _
                                        " SD.STOCK_DETAIL_ID  ,fk_HsnId_num" & _
                                " FROM " & _
                                        " ITEM_MASTER  IM " & _
                                        " INNER JOIN ITEM_DETAIL ID ON IM.ITEM_ID = ID.ITEM_ID " & _
                                        " INNER JOIN STOCK_DETAIL SD ON ID.ITEM_ID = SD.Item_id " & _
                                        " INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID" & _
                                " where " & _
                                        " IM.ITEM_ID = " & item_id & " and SD.Balance_Qty > 0"
            ds = clsObj.Fill_DataSet(sqlqry)

            Dim i As Integer
            Dim dr As DataRow

            If ds.Tables(0).Rows.Count > 0 Then
                'obj.RemoveBlankRow(dtable_Item_List, "item_id")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dtable_Item_List.NewRow
                    dr("Item_Id") = item_id
                    dr("Item_Code") = ds.Tables(0).Rows(i)("ITEM_CODE")
                    dr("Item_Name") = ds.Tables(0).Rows(i)("ITEM_NAME")
                    dr("UM_Name") = ds.Tables(0).Rows(i)("UM_Name")
                    dr("Batch_No") = ds.Tables(0).Rows(i)("Batch_no")
                    dr("Expiry_Date") = ds.Tables(0).Rows(i)("Expiry_Date")
                    dr("HsnCodeId") = ds.Tables(0).Rows(i)("fk_HsnId_num")
                    'dr("Item_Rate") = ds.Tables(0).Rows(0)("Item_Rate")

                    ds2 = obj.Fill_DataSet("SELECT VAT_MASTER.VAT_PERCENTAGE FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID WHERE (ITEM_DETAIL.ITEM_ID = " & Convert.ToInt32(frm_Show_search.search_result) & " )")
                    dr("Item_Rate") = itemRate.ToString("#0.00")
                    dr("Amount") = 0.0
                    dr("DISC") = 0.0
                    dr("LandingAmt") = 0.0
                    dr("DType") = "P"
                    dr("GST") = ds2.Tables(0).Rows(0)("VAT_PERCENTAGE")
                    dr("GSt_Amount") = "0"

                    dr("Batch_Qty") = ds.Tables(0).Rows(i)("Balance_Qty")
                    dr("Stock_Detail_Id") = ds.Tables(0).Rows(i)("STOCK_DETAIL_ID")
                    dr("transfer_Qty") = 0
                    dtable_Item_List.Rows.Add(dr)
                Next
                'Dim strSort As String = flxItems.Cols(1).Name + ", " + flxItems.Cols(2).Name + ", " + flxItems.Cols(3).Name
                'dtable_Item_List.DefaultView.Sort = strSort

                'If dtable_Item_List.Rows.Count = 0 Then dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)

                generate_tree()
            Else
                MsgBox("Stock is not avaialable for this Item.", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Function check_item_exist(ByVal item_id As Integer) As Boolean
        Dim iRow As Int32
        check_item_exist = False
        For iRow = 1 To flxItems.Rows.Count - 1
            If flxItems.Item(iRow, "item_id").ToString() <> "" Then
                If Convert.ToInt32(flxItems.Item(iRow, "item_id")) = item_id Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    check_item_exist = True
                    Exit For
                End If
            Else
                check_item_exist = False
            End If
        Next iRow
    End Function

    Private Sub flxItems_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles flxItems.AfterEdit
        If flxItems.Rows(e.Row).IsNode Then
            Exit Sub
        End If

        If Convert.ToDecimal(flxItems.Rows(e.Row)("transfer_Qty")) > Convert.ToDecimal(flxItems.Rows(e.Row)("Batch_Qty")) Then
            flxItems.Rows(e.Row)("transfer_Qty") = 0.0
            generate_tree()
        Else
            Dim discamt As Decimal = 0.0

            flxItems.Rows(e.Row)("Amount") = Math.Round(flxItems.Rows(e.Row)("transfer_Qty") * (flxItems.Rows(e.Row)("item_rate")), 2)

            Dim i As Integer
            Dim dTY As String = flxItems.Rows(e.Row)("DType")

            For i = 1 To flxItems.Rows.Count - 1
                flxItems.Rows(i).Item("DType") = dTY
            Next

            If (flxItems.Rows(e.Row)("DType")) = "P" Then
                discamt = Math.Round((flxItems.Rows(e.Row)("Amount") * flxItems.Rows(e.Row)("DISC") / 100), 2)
            Else
                discamt = Math.Round((flxItems.Rows(e.Row)("DISC")), 2)
            End If

            flxItems.Rows(e.Row)("GST_Amount") = Math.Round((flxItems.Rows(e.Row)("Amount") - discamt) * (flxItems.Rows(e.Row)("GST") / 100), 2)
            flxItems.Rows(e.Row)("LandingAmt") = Math.Round((flxItems.Rows(e.Row)("Amount") - discamt) + (flxItems.Rows(e.Row)("GST_Amount")), 2)

            generate_tree()

        End If

            CalculateAmount()
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        fill_grid(txtSearch.Text)
    End Sub

    Private Sub flxList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxList.DoubleClick
        MsgBox("You Can't Edit this Invoice." & vbCrLf & "Please click in print to view/print this Invoice/ DC.", MsgBoxStyle.Information)
    End Sub

   
    Private Function CalculateAmount() As String
        Try

            Dim i As Integer
            Dim Str As String

            Dim total_item_value As Double
            Dim total_vat_amount As Double
            Dim total_exice_amount As Double
            Dim tot_amt As Double
            total_exice_amount = 0
            total_item_value = 0
            total_vat_amount = 0
            tot_amt = 0

            Dim discamt As Decimal = 0.0
            Dim totdiscamt As Decimal = 0.0

            For i = 1 To flxItems.Rows.Count - 1
                'If flxItems.Rows(i).IsNode Then
                total_item_value = total_item_value + (flxItems.Rows(i).Item("transfer_Qty") * flxItems.Rows(i).Item("item_rate"))

                If (flxItems.Rows(i).Item("DType")) = "P" Then
                    discamt = Math.Round((flxItems.Rows(i).Item("Amount") * flxItems.Rows(i).Item("DISC") / 100), 2)
                    totdiscamt = totdiscamt + ((flxItems.Rows(i).Item("transfer_Qty") * flxItems.Rows(i).Item("item_rate")) * flxItems.Rows(i)("DISC") / 100)
                    total_vat_amount = total_vat_amount + (((flxItems.Rows(i).Item("transfer_Qty") * flxItems.Rows(i).Item("item_rate")) - ((flxItems.Rows(i).Item("transfer_Qty") * flxItems.Rows(i).Item("item_rate")) * flxItems.Rows(i)("DISC") / 100)) * flxItems.Rows(i)("GST") / 100)
                Else
                    discamt = Math.Round((flxItems.Rows(i).Item("DISC")), 2)
                    totdiscamt = totdiscamt + flxItems.Rows(i)("DISC")
                    total_vat_amount = total_vat_amount + (((flxItems.Rows(i).Item("transfer_Qty") * flxItems.Rows(i).Item("item_rate")) - discamt) * flxItems.Rows(i)("GST") / 100)
                End If

                flxItems.Rows(i).Item("GST_Amount") = Math.Round((flxItems.Rows(i).Item("Amount") - discamt) * (flxItems.Rows(i).Item("GST") / 100), 2)
                flxItems.Rows(i).Item("LandingAmt") = Math.Round((flxItems.Rows(i).Item("Amount") - discamt) + (flxItems.Rows(i).Item("GST_Amount")), 2)

            Next



            RemoveHandler flxItems.AfterDataRefresh, AddressOf flxItems_AfterDataRefresh

            AddHandler flxItems.AfterDataRefresh, AddressOf flxItems_AfterDataRefresh

            lblTotalDisc.Text = totdiscamt.ToString("#0.00")
            lblItemValue.Text = total_item_value.ToString("#0.00")
            lblVatAmount.Text = total_vat_amount.ToString("#0.00")
            lblNetAmount.Text = (total_item_value - totdiscamt + total_vat_amount + total_exice_amount).ToString("#0.00")
            Str = total_item_value.ToString("#0.00") + "," + total_vat_amount.ToString("#0.00") + "," + lblNetAmount.Text + "," + total_exice_amount.ToString()
            Return Str
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return ""
    End Function

    Private Sub lnkCalculatePOAmt_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCalculatePOAmt.LinkClicked
        CalculateAmount()
    End Sub

    Private Sub BtnInvoice_Click(sender As Object, e As EventArgs) Handles BtnInvoice.Click
        If flxList.SelectedRows.Count > 0 Then
            'obj.RptShow(enmReportName.RptInvoicePrint, "Si_ID", CStr(flxList.SelectedRows(0).Cells("Si_id").Value), CStr(enmDataType.D_int))

            obj.RptShow(enmReportName.RptInvoicePrint, "Si_ID", CStr(flxList("Si_ID", flxList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))

        End If
    End Sub

    Private Sub BtnDc_Click(sender As Object, e As EventArgs) Handles BtnDc.Click
        If flxList.SelectedRows.Count > 0 Then
            'obj.RptShow(enmReportName.RptDCInvoicePrint, "Si_ID", CStr(flxList.SelectedRows(0).Cells("Si_id").Value), CStr(enmDataType.D_int))
            obj.RptShow(enmReportName.RptDCInvoicePrint, "Si_ID", CStr(flxList("Si_ID", flxList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
        End If
    End Sub

    Private Sub BtnCancelInv_Click(sender As Object, e As EventArgs) Handles BtnCancelInv.Click
        Dim invId As Integer
        Dim Invdate As Date
        invId = flxList.SelectedRows(0).Cells("Si_id").Value
        Invdate = flxList.SelectedRows(0).Cells("INV DATE").Value
        Dim Status As String
        Status = flxList.SelectedRows(0).Cells("Status").Value
        If Status = "Cancel" Then
            MessageBox.Show("this Invoice is already canceled")
            Return
        End If
        If ((DateTime.Now - Invdate).TotalDays <= 7) Then

            clsObj.Cancel_SALE_INVOICE_MASTER(invId, Convert.ToInt32(GlobalModule.InvoiceStatus.Cancel), GlobalModule.v_the_current_logged_in_user_name)
            MessageBox.Show("Selected Invoice cancel successfully.")
        Else
            MessageBox.Show("You Can't Edit this Invoice.")
        End If
        fill_grid()

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click

        NEWCUST = 1
        cmbSupplier.Visible = False
        txtcustomer_name.Visible = True
        txtcustomer_name.Text = ""
        txtAddress.Text = ""
        txt_txtphoneNo.Text = ""
        txtGstNo.Text = ""
        btnAddNew.Visible = False
        CityBind()
        cmbinvtype.SelectedIndex = 0
        txtcustomer_name.Focus()
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged

        Dim strSql As String
        If cmbSupplier.SelectedValue <> -1 Then
            strSql = "SELECT ACCOUNT_MASTER.ADDRESS_PRIM + ' - {'  + ISNULL(CITY_MASTER.CITY_NAME,'') + '}', PHONE_PRIM, VAT_NO,ACCOUNT_MASTER.CITY_ID"
            strSql = strSql & " FROM ACCOUNT_MASTER LEFT OUTER JOIN"
            strSql = strSql & " CITY_MASTER ON ACCOUNT_MASTER.CITY_ID = CITY_MASTER.CITY_ID"
            strSql = strSql & " WHERE ACCOUNT_MASTER.ACC_ID = " & cmbSupplier.SelectedValue
            txtAddress.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(0)
            txt_txtphoneNo.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(1)
            txtGstNo.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(2)
            cmbCity.SelectedValue = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(3)
            txtvechicle_no.Focus()
        End If
    End Sub
End Class
