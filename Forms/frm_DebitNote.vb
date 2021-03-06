Imports MMSPlus.DebitNote
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_DebitNote
    Implements IForm

    Dim obj As New CommonClass
    Dim DebitNoteId As Int16
    Dim flag As String
    '  Dim group_id As Integer
    Dim dtable_Item_List As DataTable
    Dim dtable As DataTable
    ' Dim dTable_IndentItems As DataTable
    ' Dim grdMaterial_Rowindex As Int16save
    ' Dim RMRSID As Int16
    Dim FLXGRD_PO_Items_Rowindex As Int16
    'Dim intColumnIndex As Integer
    Dim rights As Form_Rights
    Dim Pre As String
    Dim DN_Code As String
    Dim DN_No As Integer
    Dim DN_Id As Integer
    Dim clsObj As New DebitNote.cls_DebitNote_Master
    Dim prpty As New DebitNote.cls_DebitNote_Prop
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmPODetail
        ItemId = 0
        ItemCode = 1
        ItemName = 2
        UOM = 3
        ItemRate = 4
        VatPer = 5
        ExePer = 6
        BatchNo = 7
        ExpiryDate = 8
        BatchQty = 9

    End Enum

    Private Sub set_new_initilize()
        lbl_DNDate.Text = Now.ToString("dd-MMM-yyyy")
        GetDNCode()
        lblDN_Code.Text = DN_Code & DN_No
        txtRemarks.Text = ""
        txt_INVNo.Text = ""
        txt_INVDate.Text = ""
        lblMRN_TYPE.Text = "0"
        dtable_Item_List = FLXGRD_MaterialItem.DataSource

        lblAmount.Text = 0
        lblVatAmount.Text = 0
        lblCessAmount.Text = 0
        lblDebit.Text = 0
        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Rows.Clear()
        TbRMRN.SelectTab(1)
        ' intColumnIndex = -1
        FillGrid()
        SetGstLabels()
        flag = "save"
    End Sub

    Private Sub GetDNCode()

        Dim ds As New DataSet()
        ds = clsObj.fill_Data_set("GET_DebitNote_No", "@DIV_ID", v_the_current_division_id)
        If ds.Tables(0).Rows.Count = 0 Then
            MsgBox("Debit note series does not exists", MsgBoxStyle.Information, gblMessageHeading)
            ds.Dispose()
            Exit Sub
        Else
            If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                MsgBox("Debit Note series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                ds.Dispose()
                Exit Sub
            ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                MsgBox("Debit Note series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                ds.Dispose()
                Exit Sub
            Else
                DN_Code = ds.Tables(0).Rows(0)(0).ToString()
                DN_No = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                ds.Dispose()
            End If
        End If

    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            TbRMRN.SelectTab(1)
            FLXGRD_MaterialItem.DataSource = Nothing
            Grid_styles()
            flag = "save"
            set_new_initilize()
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Indent_Master")
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub FillGrid(Optional ByVal condition As String = "")
        Try
            obj.GridBind(dgvList, "SELECT * FROM ( SELECT DebitNote_Id, DebitNote_Code + CAST(DebitNote_No AS VARCHAR(10)) AS DebitNote_No ,dbo.fn_format(DebitNote_Date) AS DebitNote_Date,DN_Amount ,MRNId ,MM.MRN_PREFIX + CAST(MM.MRN_NO AS VARCHAR(10)) AS MRNNo ,ACC_NAME,DM.Remarks,DM.Created_by FROM DebitNote_Master Dm INNER JOIN MATERIAL_RECEIVED_AGAINST_PO_MASTER MM ON DM.MRNId = MM.MRN_NO INNER JOIN dbo.ACCOUNT_MASTER AM ON am.ACC_ID=dm.DN_CustId UNION ALL SELECT    DebitNote_Id ,DebitNote_Code + CAST(DebitNote_No AS VARCHAR(10)) AS DebitNote_No ,dbo.fn_format(DebitNote_Date) AS DebitNote_Date,DN_Amount ,MRNId ,MM.MRN_PREFIX + CAST(MM.MRN_NO AS VARCHAR(10)) AS MRNNo ,ACC_NAME ,DM.Remarks ,DM.Created_by FROM      DebitNote_Master Dm INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM ON DM.MRNId = MM.MRN_NO INNER JOIN dbo.ACCOUNT_MASTER AM ON am.ACC_ID = dm.DN_CustId) tb  WHERE (tb.DebitNote_No+tb.DebitNote_Date+MRNNo+tb.Remarks+tb.Created_by+ACC_NAME + CAST(DN_Amount as varchar(50)) + tb.ACC_NAME + tb.Created_by)  LIKE '%" & condition & "%'")
            ' dgvList.Width = 100
            dgvList.Columns(0).Visible = False 'Reverse_ID
            dgvList.Columns(0).Width = 100
            dgvList.Columns(1).HeaderText = "DebitNote No."
            dgvList.Columns(1).Width = 110
            dgvList.Columns(2).HeaderText = "Date"
            dgvList.Columns(2).Width = 80
            dgvList.Columns(3).HeaderText = "Amount"
            dgvList.Columns(3).Width = 80
            dgvList.Columns(4).HeaderText = "MRN Id"
            dgvList.Columns(4).Width = 10
            dgvList.Columns(4).Visible = False
            dgvList.Columns(5).HeaderText = "MRN No."
            dgvList.Columns(5).Width = 120
            dgvList.Columns(6).HeaderText = "Supplier"
            dgvList.Columns(6).Width = 195
            dgvList.Columns(7).HeaderText = "Remarks"
            dgvList.Columns(7).Width = 195
            dgvList.Columns(8).HeaderText = "User"
            dgvList.Columns(8).Width = 60
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGrid()
        TbRMRN.SelectTab(0)
    End Sub

    Private Function validate_data() As Boolean
        Dim iRow As Int32
        Dim int_RowIndex As Int32
        Dim dtCheck As New DataTable
        Dim blnIsExist As Boolean
        blnIsExist = False

        If cmbSupplier.Text = Nothing Then
            MsgBox("Please Select valid Supplier first.")
            validate_data = False
            Exit Function
        End If

        If String.IsNullOrEmpty(txtRemarks.Text) Then
            MsgBox("Please fill the Remarks", vbExclamation, gblMessageHeading)
            txtRemarks.Focus()
            validate_data = False
            Exit Function
        End If

        If cmbMRNNo.SelectedIndex <= 0 Then
            MsgBox("Select MRN to create debit note.", vbExclamation, gblMessageHeading)
            cmbMRNNo.Focus()
            validate_data = False
            Exit Function
        End If

        dtCheck = FLXGRD_MaterialItem.DataSource
        int_RowIndex = dtCheck.Rows.Count
        For iRow = 0 To int_RowIndex - 1
            If dtCheck.Rows(iRow)("Item_Qty") > 0 Then
                blnIsExist = True
            End If
        Next iRow

        If blnIsExist = False Then
            validate_data = False
            MsgBox("Enter atleast one debit quantity.", vbExclamation, gblMessageHeading)
            Exit Function
        Else
            validate_data = True
        End If
    End Function

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        If _rights.allow_trans = "N" Then
            RightsMsg()
            Exit Sub
        End If

        CalculateAmount()
        Dim cmd As SqlCommand


        'If cmd Is Nothing Then
        '    G_MyConTransaction = False
        '    cmd = obj.MyCon_BeginTransaction
        'End If
        txtRemarks.Focus()
        Try
            If flag = "save" And validate_data() Then
                cmd = obj.MyCon_BeginTransaction
                GetDNCode()
                DN_Id = Convert.ToInt32(obj.getMaxValue("DebitNote_ID", "DebitNote_MASTER"))
                prpty.DebitNote_ID = Convert.ToInt32(DN_Id)
                prpty.DebitNote_Code = DN_Code ' GetDebitNoteCode()
                prpty.DebitNote_No = DN_No ' Convert.ToInt32(DebitNoteID)
                prpty.DebitNote_Date = Now 'Convert.ToDateTime(lbl_PODate.Text).ToString()
                prpty.MRN_ID = cmbMRNNo.SelectedValue
                prpty.Remarks = txtRemarks.Text
                prpty.Created_By = v_the_current_logged_in_user_name
                prpty.Creation_Date = Now
                prpty.Modified_By = ""
                prpty.Modification_Date = NULL_DATE
                prpty.Division_ID = v_the_current_division_id
                prpty.Dn_Amount = lblDebit.Text
                cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
                prpty.DN_CustId = cmbsupplier.SelectedValue
                prpty.INV_No = txt_INVNo.Text
                prpty.INV_Date = txt_INVDate.Text
                prpty.DN_ItemValue = lblAmount.Text
                prpty.DN_ItemTax = lblVatAmount.Text
                prpty.DN_ItemCess = lblCessAmount.Text
                prpty.DN_Type = ""
                prpty.Ref_No = ""
                prpty.Ref_Date = NULL_DATE
                prpty.Proctype = 1

                clsObj.insert_DebitNote_MASTER(prpty, cmd)

                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = FLXGRD_MaterialItem.Rows.Count - 1

                For iRow = 1 To iRowCount
                    If FLXGRD_MaterialItem.Item(iRow, "Item_Qty") > 0 Then
                        prpty.DebitNote_ID = Convert.ToInt32(DN_Id)
                        prpty.Item_ID = FLXGRD_MaterialItem.Item(iRow, "Item_Id")
                        prpty.Item_Qty = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_Qty")).ToString()
                        prpty.Item_Rate = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_rate")).ToString()
                        prpty.Item_Tax = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Vat_Per")).ToString()
                        prpty.Item_Cess = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess_Per")).ToString()
                        prpty.Stock_Detail_ID = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Stock_Detail_Id")).ToString()
                        prpty.Created_By = v_the_current_logged_in_user_name
                        prpty.Creation_Date = Now
                        prpty.Modified_By = v_the_current_logged_in_user_name
                        prpty.Modification_Date = NULL_DATE
                        prpty.Division_ID = v_the_current_division_id
                        prpty.Proctype = 1
                        clsObj.insert_DebitNote_DETAIL(prpty, cmd)
                        'End If
                    End If
                Next iRow

                MsgBox("Debit note saved with No. " & DN_Code & DN_No, MsgBoxStyle.Information, gblMessageHeading)
                obj.MyCon_CommitTransaction(cmd)

                If flag = "save" Then
                    If MsgBox(vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                        obj.RptShow(enmReportName.RptDebitNotePrint, "DN_ID", CStr(prpty.DebitNote_ID), CStr(enmDataType.D_int))
                    End If
                Else
                End If

                set_new_initilize()
                cmbsupplier.SelectedValue = 0
                cmbMRNNo.SelectedValue = 0

            ElseIf flag = "update" And validate_data() Then
                cmd = obj.MyCon_BeginTransaction
                Dim ds As New DataSet()
                ds = clsObj.fill_Data_set("GET_DebitNoteCodeByID", "@DebitNoteId", DebitNoteId)
                If ds.Tables(0).Rows.Count > 0 Then
                    DN_Code = ds.Tables(0).Rows(0)(0).ToString()
                    DN_No = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString())
                    ds.Dispose()
                End If
                prpty.DebitNote_ID = Convert.ToInt32(DebitNoteId)
                prpty.DebitNote_Code = DN_Code ' GetDebitNoteCode()
                prpty.DebitNote_No = DN_No ' Convert.ToInt32(DebitNoteID)
                prpty.DebitNote_Date = lbl_DNDate.Text 'Convert.ToDateTime(lbl_PODate.Text).ToString()
                prpty.MRN_ID = cmbMRNNo.SelectedValue
                prpty.Remarks = txtRemarks.Text
                prpty.Created_By = v_the_current_logged_in_user_name
                prpty.Creation_Date = NULL_DATE
                prpty.Modified_By = v_the_current_logged_in_user_name
                prpty.Modification_Date = Now
                prpty.Division_ID = v_the_current_division_id
                prpty.Dn_Amount = lblDebit.Text
                cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
                prpty.DN_CustId = cmbsupplier.SelectedValue
                prpty.INV_No = txt_INVNo.Text
                prpty.INV_Date = txt_INVDate.Text
                prpty.DN_ItemValue = lblAmount.Text
                prpty.DN_ItemTax = lblVatAmount.Text
                prpty.DN_ItemCess = lblCessAmount.Text
                prpty.DN_Type = ""
                prpty.Ref_No = ""
                prpty.Ref_Date = NULL_DATE
                prpty.Proctype = 2

                clsObj.insert_DebitNote_MASTER(prpty, cmd)

                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = FLXGRD_MaterialItem.Rows.Count - 1

                For iRow = 1 To iRowCount
                    If FLXGRD_MaterialItem.Item(iRow, "Item_Qty") > 0 Then
                        prpty.DebitNote_ID = Convert.ToInt32(DebitNoteId)
                        prpty.Item_ID = FLXGRD_MaterialItem.Item(iRow, "Item_Id")
                        prpty.Item_Qty = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_Qty")).ToString()
                        prpty.Item_Rate = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_rate")).ToString()
                        prpty.Item_Tax = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Vat_Per")).ToString()
                        prpty.Item_Cess = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess_Per")).ToString()
                        prpty.Stock_Detail_ID = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Stock_Detail_Id")).ToString()
                        prpty.Created_By = v_the_current_logged_in_user_name
                        prpty.Creation_Date = Now
                        prpty.Modified_By = v_the_current_logged_in_user_name
                        prpty.Modification_Date = NULL_DATE
                        prpty.Division_ID = v_the_current_division_id
                        prpty.Proctype = 2
                        clsObj.insert_DebitNote_DETAIL(prpty, cmd)
                        'End If
                    End If
                Next iRow

                MsgBox("Debit note updated with No. " & DN_Code & DN_No, MsgBoxStyle.Information, gblMessageHeading)
                obj.MyCon_CommitTransaction(cmd)

                If flag = "update" Then
                    If MsgBox(vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                        obj.RptShow(enmReportName.RptDebitNotePrint, "DN_ID", CStr(prpty.DebitNote_ID), CStr(enmDataType.D_int))
                    End If
                Else
                End If

                set_new_initilize()
                cmbsupplier.SelectedValue = 0
                cmbMRNNo.SelectedValue = 0
            End If
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Public Function GetReceivedCode() As String

        Dim CCID As String
        Dim POCode As String
        Pre = obj.getPrefixCode("RMRN_Prefix", "DIVISION_SETTINGS")
        CCID = obj.getMaxValue("Reverse_ID", "REVERSEMATERIAL_RECIEVED_WITHOUT_PO_MASTER")
        POCode = Pre & "" & CCID
        Return POCode
    End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TbRMRN.SelectedIndex = 0 Then
                If dgvList.SelectedRows.Count > 0 Then

                    obj.RptShow(enmReportName.RptDebitNotePrint, "DN_ID", CStr(dgvList("DebitNote_Id", dgvList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptDebitNotePrint, "DN_ID", CStr(DN_Id), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Grid_styles()
        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()

        dtable_Item_List = New DataTable()
        dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
        dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("Prev_Item_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("MRN_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_Rate", GetType(System.Double))
        dtable_Item_List.Columns.Add("Vat_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("Cess_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("Stock_Detail_Id", GetType(System.Double))


        FLXGRD_MaterialItem.DataSource = dtable_Item_List
        dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)
        FLXGRD_MaterialItem.Cols(0).Width = 10
        SetGridSettingValues()


    End Sub

    Private Sub SetGridSettingValues()

        FLXGRD_MaterialItem.Cols("Item_ID").Visible = False
        FLXGRD_MaterialItem.Cols("Item_Code").Caption = "BarCode"
        FLXGRD_MaterialItem.Cols("Item_Name").Caption = "Item Name"
        FLXGRD_MaterialItem.Cols("UM_Name").Caption = "UOM"
        FLXGRD_MaterialItem.Cols("Prev_Item_Qty").Caption = "Current Stock"
        FLXGRD_MaterialItem.Cols("MRN_Qty").Caption = "MRN Item Qty"
        FLXGRD_MaterialItem.Cols("Item_Rate").Caption = "Item Rate"
        FLXGRD_MaterialItem.Cols("Vat_Per").Caption = "Tax %"
        FLXGRD_MaterialItem.Cols("Cess_Per").Caption = "Cess %"

        FLXGRD_MaterialItem.Cols("Item_Qty").Caption = "Return Qty"

        FLXGRD_MaterialItem.Cols("Stock_Detail_Id").Visible = False

        FLXGRD_MaterialItem.Cols("Item_Code").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Item_Name").AllowEditing = False
        FLXGRD_MaterialItem.Cols("UM_Name").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Prev_Item_Qty").AllowEditing = False
        FLXGRD_MaterialItem.Cols("MRN_Qty").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Item_Rate").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Vat_Per").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Cess_Per").AllowEditing = False

        FLXGRD_MaterialItem.Cols("Item_Qty").AllowEditing = True

        FLXGRD_MaterialItem.Cols("Stock_Detail_Id").AllowEditing = False

        FLXGRD_MaterialItem.Cols("Item_Code").Width = 100
        FLXGRD_MaterialItem.Cols("Item_Name").Width = 300
        FLXGRD_MaterialItem.Cols("UM_Name").Width = 40
        FLXGRD_MaterialItem.Cols("Prev_Item_Qty").Width = 90

        FLXGRD_MaterialItem.Cols("MRN_Qty").Width = 75
        FLXGRD_MaterialItem.Cols("Item_Rate").Width = 70
        FLXGRD_MaterialItem.Cols("Vat_Per").Width = 50
        FLXGRD_MaterialItem.Cols("Cess_Per").Width = 50

        FLXGRD_MaterialItem.Cols("Item_Qty").Width = 90
        FLXGRD_MaterialItem.Cols("Stock_Detail_Id").Width = 10



        Dim cs As C1.Win.C1FlexGrid.CellStyle
        cs = Me.FLXGRD_MaterialItem.Styles.Add("Item_Qty")
        cs.ForeColor = Color.Black
        cs.BackColor = Color.LimeGreen
        cs.Border.Style = BorderStyleEnum.Raised
        cs.TextAlign = TextAlignEnum.RightTop

        Dim i As Integer
        For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1
            FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("Item_Qty").SafeIndex, cs)
        Next

    End Sub

    Private Sub FLXGRD_MaterialItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_MaterialItem.AfterEdit
        If IsNumeric(FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty")) = True Then
            If FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") > FLXGRD_MaterialItem.Rows(e.Row)("Prev_Item_Qty") Then
                FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") = 0
            Else
                FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty")), 4)

                Dim itemqty As Decimal = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty")), 4)
                Dim itemRate As Decimal = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Item_Rate")), 2)
                Dim Vat As Decimal = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Vat_Per")), 2)

                CalculateAmount()

            End If
        Else
            FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") = 0
        End If
    End Sub

    Private Sub FLXGRD_MaterialItem_EnterCell(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FLXGRD_MaterialItem.EnterCell
    End Sub

    Private Sub FLXGRD_MaterialItem_RowColChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FLXGRD_MaterialItem.RowColChange

    End Sub

    Private Sub FLXGRD_MaterialItem_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FLXGRD_MaterialItem.KeyPress

    End Sub

    Private Sub getMRNDetail(ByVal Receive_ID As Integer)


    End Sub

    Private Sub cmbMRNNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMRNNo.SelectedIndexChanged
        If (DebitNoteId < 1) Then
            Try
                set_new_initilize()
                Dim ds As DataSet
                Dim ds1 As DataSet
                Dim MRNNo As Int32
                MRNNo = Convert.ToInt32(cmbMRNNo.SelectedValue)
                ds = clsObj.fill_Data_set("Get_MRN_Details_DebitNote", "@V_MRN_NO", MRNNo)
                If ds.Tables(0).Rows.Count > 0 Then
                    dtable_Item_List = ds.Tables(0).Copy
                    FLXGRD_MaterialItem.DataSource = dtable_Item_List
                    SetGridSettingValues()
                End If

                Dim Query As String = " SELECT Invoice_No ,CONVERT(VARCHAR (20),Invoice_date,106)AS Invoice_date,MRN_TYPE FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER WHERE MRN_NO=" & MRNNo & " AND Division_ID =   " & v_the_current_division_id &
               "UNION ALL SELECT Invoice_No ,CONVERT(VARCHAR (20),Invoice_date,106)AS Invoice_date,MRN_TYPE FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER WHERE MRN_NO=" & MRNNo


                ds1 = clsObj.FillDataSet(Query)
                If ds1.Tables(0).Rows.Count > 0 Then
                    txt_INVNo.Text = ds1.Tables(0).Rows(0)(0)
                    txt_INVDate.Text = ds1.Tables(0).Rows(0)(1)
                    lblMRN_TYPE.Text = ds1.Tables(0).Rows(0)(2)
                End If
                TbRMRN.SelectTab(1)
            Catch ex As Exception
                MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
            End Try
            lblAmount.Text = 0
            lblVatAmount.Text = 0
            lblDebit.Text = 0

            If FLXGRD_MaterialItem.Rows.Count - 1 > 0 Then
                Dim Index As Int32 = 1
                FLXGRD_MaterialItem.Row = Index
                FLXGRD_MaterialItem.RowSel = Index
                FLXGRD_MaterialItem.Col = 10
                FLXGRD_MaterialItem.ColSel = 10
                SetGstLabels()
            End If
        End If

    End Sub

    Private Sub frm_DebitNote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        set_new_initilize()
        BindSupplierCombo()
        'BindMRNCombo()
        Grid_styles()
        FillGrid()

    End Sub

    Private Sub BindMRNCombo()

        Dim Query As String
        Dim Dt As DataTable
        Dim Dtrow As DataRow

        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)

        If cmbSupplier.SelectedValue > 0 Then

            Query = " SELECT MRN_NO AS MRN_ID,MRN_PREFIX+CAST(MRN_NO AS VARCHAR(20))AS MRN_NO FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER WHERE PO_ID IN (SELECT PO_ID FROM dbo.PO_MASTER WHERE PO_SUPP_ID=" & cmbSupplier.SelectedValue & ") AND Division_ID =   " & v_the_current_division_id &
            "UNION ALL SELECT MRN_NO AS MRN_ID , MRN_PREFIX + CAST(MRN_NO AS VARCHAR(20)) AS MRN_NO FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER WHERE Vendor_ID=" & cmbSupplier.SelectedValue & " ORDER BY MRN_id"
            Dt = clsObj.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("MRN_ID") = -1
            Dtrow("MRN_NO") = "Select MRN No"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbMRNNo.DisplayMember = "MRN_NO"
            cmbMRNNo.ValueMember = "MRN_ID"
            cmbMRNNo.DataSource = Dt
            cmbMRNNo.SelectedIndex = 0
            'cmbMRNNo.Focus()
        End If

    End Sub

    Private Sub BindSupplierCombo()
        obj.ComboBind(cmbsupplier, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID in (1,2,3,6) Order by ACC_NAME ", "ACC_NAME", "ACC_ID")

    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        FillGrid(txtSearch.Text.Trim())
    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged

        BindMRNCombo()
        lblAmount.Text = 0
        lblVatAmount.Text = 0
        lblDebit.Text = 0
    End Sub

    Private Sub lnkCalculateDebitAmt_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCalculateDebitAmt.LinkClicked
        CalculateAmount()
    End Sub

    Private Function CalculateAmount() As String
        Dim i As Integer
        Dim Str As String

        Dim total_item_value As Double
        Dim total_vat_amount As Double
        Dim total_cess_amount As Double
        Dim total_exice_amount As Double
        Dim tot_amt As Double
        total_exice_amount = 0
        total_item_value = 0
        total_vat_amount = 0
        total_cess_amount = 0
        tot_amt = 0



        For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1
            If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("Item_Qty"))) > 0 Then

                total_item_value = total_item_value + (FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") * FLXGRD_MaterialItem.Rows(i).Item("item_rate"))
                total_vat_amount = total_vat_amount + ((FLXGRD_MaterialItem.Rows(i).Item("item_rate") * FLXGRD_MaterialItem.Rows.Item(i)("Item_Qty")) * FLXGRD_MaterialItem.Rows(i).Item("Vat_Per") / 100)
                total_cess_amount = total_cess_amount + ((FLXGRD_MaterialItem.Rows(i).Item("item_rate") * FLXGRD_MaterialItem.Rows.Item(i)("Item_Qty")) * FLXGRD_MaterialItem.Rows(i).Item("Cess_Per") / 100)

            End If
        Next



        lblAmount.Text = total_item_value.ToString("#0.00")
        lblVatAmount.Text = total_vat_amount.ToString("#0.00")
        lblCessAmount.Text = total_cess_amount.ToString("#0.00")
        lblDebit.Text = (total_item_value + total_vat_amount + total_cess_amount + total_exice_amount).ToString("#0.00")
        Str = total_item_value.ToString("#0.00") + "," + total_vat_amount.ToString("#0.00") + "," + total_cess_amount.ToString("#0.00") + "," + total_exice_amount.ToString()
        SetGstLabels()
        Return Str

    End Function

    Private Sub SetGstLabels()

        Dim GSTAmount0 As Decimal = 0
        Dim GSTTax0 As Decimal = 0
        Dim GSTAmount3 As Decimal = 0
        Dim GSTTax3 As Decimal = 0
        Dim GSTAmount5 As Decimal = 0
        Dim GSTTax5 As Decimal = 0
        Dim GSTAmount12 As Decimal = 0
        Dim GSTTax12 As Decimal = 0
        Dim GSTAmount18 As Decimal = 0
        Dim GSTTax18 As Decimal = 0
        Dim GSTAmount28 As Decimal = 0
        Dim GSTTax28 As Decimal = 0
        Dim GSTTaxTotal As Decimal = 0
        Dim CessTotal As Decimal = 0
        Dim Tax As Decimal = 0

        Dim iRow As Integer = 0

        For iRow = 1 To FLXGRD_MaterialItem.Rows.Count - 1

            If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Item(iRow, "Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Item(iRow, "Item_Qty"))) > 0 Then


                Dim totalAmount As Decimal = FLXGRD_MaterialItem.Item(iRow, "Item_Qty") * FLXGRD_MaterialItem.Item(iRow, "item_rate")

                'If FLXGRD_MaterialItem.Item(iRow, "DType") = "P" Then
                '    totalAmount -= Math.Round((totalAmount * FLXGRD_MaterialItem.Item(iRow, "DISC") / 100), 2) + Math.Round((FLXGRD_MaterialItem.Item(iRow, "DISC1") / 100), 2)
                'Else
                '    totalAmount -= Math.Round(FLXGRD_MaterialItem.Item(iRow, "DISC"), 2) + Math.Round((FLXGRD_MaterialItem.Item(iRow, "DISC1") / 100), 2)
                'End If

                'If FLXGRD_MaterialItem.Item(iRow, "GPAID") = "Y" Then
                '    totalAmount -= (totalAmount - (totalAmount / (1 + (FLXGRD_MaterialItem.Item(iRow, "vat_per") / 100))))
                'End If

                Tax = totalAmount * FLXGRD_MaterialItem.Item(iRow, "vat_per") / 100

                GSTTaxTotal += Tax

                Select Case FLXGRD_MaterialItem.Item(iRow, "vat_per")
                    Case 0
                        GSTAmount0 += totalAmount
                        GSTTax0 += Tax
                    Case 3
                        GSTAmount3 += totalAmount
                        GSTTax3 += Tax
                    Case 5
                        GSTAmount5 += totalAmount
                        GSTTax5 += Tax
                    Case 12
                        GSTAmount12 += totalAmount
                        GSTTax12 += Tax
                    Case 18
                        GSTAmount18 += totalAmount
                        GSTTax18 += Tax
                    Case 28
                        GSTAmount28 += totalAmount
                        GSTTax28 += Tax
                End Select
            End If
        Next



        lblGST0.Text = String.Format("0% - {0:0.00} @ {1}", Math.Round(GSTAmount0, 2), Math.Round(GSTTax0, 2))
        lblGST3.Text = String.Format("3% - {0:0.00} @ {1}", Math.Round(GSTAmount3, 2), Math.Round(GSTTax3, 2))
        lblGST5.Text = String.Format("5% - {0:0.00} @ {1}", Math.Round(GSTAmount5, 2), Math.Round(GSTTax5, 2))
        lblGST12.Text = String.Format("12% - {0:0.00} @ {1}", Math.Round(GSTAmount12, 2), Math.Round(GSTTax12, 2))
        lblGST18.Text = String.Format("18% - {0:0.00} @ {1}", Math.Round(GSTAmount18, 2), Math.Round(GSTTax18, 2))
        lblGST28.Text = String.Format("28% - {0:0.00} @ {1}", Math.Round(GSTAmount28, 2), Math.Round(GSTTax28, 2))

        SetGSTAndCessHeader(GSTTaxTotal, CessTotal)

    End Sub

    Private Sub SetGSTAndCessHeader(TotalGst As Decimal, TotalCess As Decimal)
        Dim PartialGst As Decimal = Math.Round(TotalGst / 2, 2)
        If lblMRN_TYPE.Text = 0 Then
            lblGSTDetail.Text = String.Format("Total GST - {0}", Math.Round(TotalGst, 2))
            lblGSTDetail.Tag = Math.Round(TotalGst, 2)
        ElseIf lblMRN_TYPE.Text = 3 Then
            lblGSTDetail.Text = String.Format("UTGST - {0}{1}CGST - {0}", Math.Round(PartialGst, 2), Environment.NewLine)
            lblGSTDetail.Tag = Math.Round(PartialGst, 2)
        ElseIf lblMRN_TYPE.Text = 1 Then
            lblGSTDetail.Text = String.Format("SGST - {0}{1}CGST - {0}", Math.Round(PartialGst, 2), Environment.NewLine)
            lblGSTDetail.Tag = Math.Round(PartialGst, 2)
        ElseIf lblMRN_TYPE.Text = 2 Then
            lblGSTDetail.Text = String.Format("IGST - {0}", Math.Round(TotalGst, 2))
            lblGSTDetail.Tag = Math.Round(TotalGst, 2)
        End If

    End Sub

    Private Sub dgvList_DoubleClick(sender As Object, e As EventArgs) Handles dgvList.DoubleClick
        If _rights.allow_edit = "N" Then
            RightsMsg()
            Exit Sub
        End If

        flag = "update"
        DebitNoteId = Convert.ToInt32(dgvList("DebitNote_Id", dgvList.CurrentCell.RowIndex).Value())
        FillPaymentDetails(DebitNoteId)

        If FLXGRD_MaterialItem.Rows.Count - 1 > 0 Then
            Dim Index As Int32 = 1
            FLXGRD_MaterialItem.Row = Index
            FLXGRD_MaterialItem.RowSel = Index
            FLXGRD_MaterialItem.Col = 10
            FLXGRD_MaterialItem.ColSel = 10
        End If

    End Sub

    Public Sub FillPaymentDetails(DebitNoteId As Int16)
        Dim dt As DataTable
        dt = clsObj.fill_Data_set("Proc_GETDebitNoteDetailsByID_Edit", "@DebitNoteId", DebitNoteId).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            TbRMRN.SelectTab(1)
            lblDN_Code.Text = dr("DebitNoteNumber")
            lbl_DNDate.Text = dr("DebitNote_Date")
            cmbsupplier.SelectedValue = dr("DN_CustId")
            BindMRNCombo()
            cmbMRNNo.SelectedValue = dr("MRNo").ToString
            txt_INVNo.Text = dr("InvoiceNo")
            txt_INVDate.Text = dr("InvoiceDate")
            txtRemarks.Text = dr("Remarks")
            lblMRN_TYPE.Text = dr("MRN_TYPE")

            Dim ds As DataSet
            ds = clsObj.fill_Data_set("GetDebitNoteDetails", "@DebitNoteId", DebitNoteId)
            If ds.Tables(0).Rows.Count > 0 Then
                dtable_Item_List = ds.Tables(0).Copy
                FLXGRD_MaterialItem.DataSource = dtable_Item_List
                SetGridSettingValues()
                CalculateAmount()
            End If

        End If
    End Sub

End Class
