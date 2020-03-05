Imports MMSPlus.DebitNote
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_DebitNoteNew
    Implements IForm

    Dim obj As New CommonClass
    Dim DebitNoteId As Int16
    Dim flag As String
    '  Dim group_id As Integer
    Dim dtable_Item_List As DataTable
    Dim dtable As DataTable
    ' Dim dTable_IndentItems As DataTable
    Dim grdMaterial_Rowindex As Int16
    ' Dim RMRSID As Int16
    Dim FLXGRD_PO_Items_Rowindex As Int16
    'Dim intColumnIndex As Integer
    Dim rights As Form_Rights
    Dim Pre As String
    Dim DN_Code As String
    Dim DN_No As Integer
    Dim DN_Id As Integer
    Dim AccountId As Integer
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

        If flag = "save" Then
            GetDNCode()
            flag = "save"
            lbl_DNDate.Text = Now.ToString("dd-MMM-yyyy")
            lblDN_Code.Text = DN_Code & DN_No
            txtRemarks.Text = ""
            txt_INVNo.Text = ""
            txt_INVDate.Text = ""
            lblMRN_TYPE.Text = "0"
            txtRoundOff.Text = 0
            DebitNoteId = 0
        End If

        dtable_Item_List = FLXGRD_MaterialItem.DataSource
        txt_INVNo.Visible = False
        lblAmount.Text = 0
        lblVatAmount.Text = 0
        lblCessAmount.Text = 0
        lblDebit.Text = 0

        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Rows.Clear()
        ' intColumnIndex = -1
        FillGrid()
        SetGstLabels()

        TbRMRN.SelectTab(1)
        'TbRMRN.Focus()
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
            obj.GridBind(dgvList, "SELECT * FROM ( SELECT DebitNote_Id, DebitNote_Code + CAST(DebitNote_No AS VARCHAR(10)) AS DebitNote_No ,dbo.fn_format(DebitNote_Date) AS DebitNote_Date,DN_Amount ,MRNId ,MM.MRN_PREFIX + CAST(MM.MRN_NO AS VARCHAR(10)) AS MRNNo ,ACC_NAME,DM.Remarks,DM.Created_by FROM DebitNote_Master Dm INNER JOIN MATERIAL_RECEIVED_AGAINST_PO_MASTER MM ON DM.MRNId = MM.MRN_NO INNER JOIN dbo.ACCOUNT_MASTER AM ON am.ACC_ID=dm.DN_CustId UNION ALL SELECT    DebitNote_Id ,DebitNote_Code + CAST(DebitNote_No AS VARCHAR(10)) AS DebitNote_No ,dbo.fn_format(DebitNote_Date) AS DebitNote_Date,DN_Amount ,MRNId ,MM.MRN_PREFIX + CAST(MM.MRN_NO AS VARCHAR(10)) AS MRNNo ,ACC_NAME ,DM.Remarks ,DM.Created_by FROM      DebitNote_Master Dm INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM ON DM.MRNId = MM.MRN_NO INNER JOIN dbo.ACCOUNT_MASTER AM ON am.ACC_ID = dm.DN_CustId UNION ALL
          SELECT    DebitNote_Id ,
                    DebitNote_Code + CAST(DebitNote_No AS VARCHAR(10)) AS DebitNote_No ,
                    dbo.fn_format(DebitNote_Date) AS DebitNote_Date ,
                    DN_Amount ,
                    MRNId ,
                    'OPEN' AS MRNNo ,
                    ACC_NAME ,
                    DM.Remarks ,
                    DM.Created_by
          FROM      DebitNote_Master Dm
                    INNER JOIN dbo.ACCOUNT_MASTER AM ON am.ACC_ID = dm.DN_CustId
          WHERE     MRNId <=0) tb  WHERE (tb.DebitNote_No+tb.DebitNote_Date+MRNNo+tb.Remarks+tb.Created_by+ACC_NAME + CAST(DN_Amount as varchar(50)) + tb.ACC_NAME + tb.Created_by)  LIKE '%" & condition & "%' order by tb.DebitNote_Id")
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

        If Not cbSetOpen.Checked Then
            If cmbBillNo.SelectedIndex <= 0 Then
                MsgBox("Select MRN to create debit note.", vbExclamation, gblMessageHeading)
                cmbBillNo.Focus()
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
                DebitNoteId = DN_Id
                prpty.DebitNote_ID = Convert.ToInt32(DN_Id)
                prpty.DebitNote_Code = DN_Code ' GetDebitNoteCode()
                prpty.DebitNote_No = DN_No ' Convert.ToInt32(DebitNoteID)
                prpty.DebitNote_Date = Now 'Convert.ToDateTime(lbl_PODate.Text).ToString()
                prpty.MRN_ID = cmbBillNo.SelectedValue
                prpty.Remarks = txtRemarks.Text
                prpty.Created_By = v_the_current_logged_in_user_name
                prpty.Creation_Date = Now
                prpty.Modified_By = ""
                prpty.Modification_Date = NULL_DATE
                prpty.Division_ID = v_the_current_division_id
                prpty.RoundOff = txtRoundOff.Text
                prpty.Dn_Amount = lblDebit.Text
                'cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
                prpty.DN_CustId = AccountId 'cmbSupplier.SelectedValue
                prpty.INV_No = txt_INVNo.Text
                prpty.INV_Date = txt_INVDate.Text
                prpty.DN_ItemValue = lblAmount.Text
                prpty.DN_ItemTax = lblVatAmount.Text
                prpty.DN_ItemCess = lblCessAmount.Text

                If (cbSetOpen.Checked) Then
                    prpty.DN_Type = "Open"
                Else
                    prpty.DN_Type = ""
                End If

                prpty.Ref_No = ""
                prpty.Ref_Date = NULL_DATE
                prpty.Proctype = 1

                clsObj.insert_DebitNote_MASTER(prpty, cmd)

                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = FLXGRD_MaterialItem.Rows.Count - 1

                For iRow = 1 To iRowCount
                    If FLXGRD_MaterialItem.Item(iRow, "TaxableAmt") > 0 Then
                        prpty.DebitNote_ID = Convert.ToInt32(DN_Id)
                        prpty.Item_ID = FLXGRD_MaterialItem.Item(iRow, "Item_Id")
                        prpty.Item_Qty = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_Qty")).ToString()
                        prpty.Item_Rate = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_rate")).ToString()
                        prpty.Item_Tax = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Vat_Per")).ToString()
                        prpty.Item_Cess = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess_Per")).ToString()
                        prpty.Stock_Detail_ID = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Stock_Detail_Id")).ToString()
                        prpty.TaxableAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "TaxableAmt")).ToString()
                        prpty.TaxAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "GST")).ToString()
                        prpty.CessAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess")).ToString()
                        prpty.DebitAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "TaxableAmt")) + Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "GST")).ToString() + Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess")).ToString()

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

                flag = "save"
                set_new_initilize()
                cmbSupplier.SelectedValue = 0
                cmbBillNo.SelectedValue = 0


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
                prpty.MRN_ID = cmbBillNo.SelectedValue
                prpty.Remarks = txtRemarks.Text
                prpty.Created_By = v_the_current_logged_in_user_name
                prpty.Creation_Date = NULL_DATE
                prpty.Modified_By = v_the_current_logged_in_user_name
                prpty.Modification_Date = Now
                prpty.Division_ID = v_the_current_division_id
                prpty.Dn_Amount = lblDebit.Text
                prpty.RoundOff = txtRoundOff.Text
                ' cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
                prpty.DN_CustId = AccountId 'cmbSupplier.SelectedValue
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
                    If FLXGRD_MaterialItem.Item(iRow, "TaxableAmt") > 0 Then
                        prpty.DebitNote_ID = Convert.ToInt32(DebitNoteId)
                        prpty.Item_ID = FLXGRD_MaterialItem.Item(iRow, "Item_Id")
                        prpty.Item_Qty = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_Qty")).ToString()
                        prpty.Item_Rate = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_rate")).ToString()
                        prpty.Item_Tax = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Vat_Per")).ToString()
                        prpty.Item_Cess = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess_Per")).ToString()
                        prpty.Stock_Detail_ID = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Stock_Detail_Id")).ToString()
                        prpty.TaxableAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "TaxableAmt")).ToString()
                        prpty.TaxAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "GST")).ToString()
                        prpty.CessAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess")).ToString()
                        prpty.DebitAmt = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "TaxableAmt")) + Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "GST")).ToString() + Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Cess")).ToString()

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
                flag = "save"
                set_new_initilize()
                cmbSupplier.SelectedValue = 0
                cmbBillNo.SelectedValue = 0
                cbSetOpen.Checked = False
                DebitNoteId = 0
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
        dtable_Item_List.Columns.Add("TaxableAmt", GetType(System.Double))
        dtable_Item_List.Columns.Add("Amount", GetType(System.Double))
        dtable_Item_List.Columns.Add("GST", GetType(System.Double))
        dtable_Item_List.Columns.Add("Cess", GetType(System.Double))
        dtable_Item_List.Columns.Add("Stock_Detail_Id", GetType(System.Double))
        dtable_Item_List.Columns.Add("Prv_Rate", GetType(System.Double))
        dtable_Item_List.Columns.Add("Prv_Vat_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("Prv_Cess_Per", GetType(System.Double))

        FLXGRD_MaterialItem.DataSource = dtable_Item_List
        dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)
        FLXGRD_MaterialItem.Cols(0).Width = 10
        generate_tree()

    End Sub

    Private Sub SetGridSettingValues()

        FLXGRD_MaterialItem.Cols("Item_ID").Visible = False
        FLXGRD_MaterialItem.Cols("Item_Code").Caption = "BarCode"
        FLXGRD_MaterialItem.Cols("Item_Name").Caption = "Item Name"
        FLXGRD_MaterialItem.Cols("UM_Name").Caption = "UOM"
        FLXGRD_MaterialItem.Cols("Prev_Item_Qty").Caption = "Current Stock"
        FLXGRD_MaterialItem.Cols("MRN_Qty").Caption = "MRN Item Qty"
        FLXGRD_MaterialItem.Cols("Item_Rate").Caption = "Cost Rate"
        FLXGRD_MaterialItem.Cols("Vat_Per").Caption = "Tax %"
        FLXGRD_MaterialItem.Cols("Cess_Per").Caption = "Cess %"
        FLXGRD_MaterialItem.Cols("Amount").Caption = "Amount"
        FLXGRD_MaterialItem.Cols("Item_Qty").Caption = "Return Qty"

        FLXGRD_MaterialItem.Cols("Stock_Detail_Id").Visible = False

        FLXGRD_MaterialItem.Cols("Prv_Rate").Visible = False
        FLXGRD_MaterialItem.Cols("Prv_Vat_Per").Visible = False
        FLXGRD_MaterialItem.Cols("Prv_Cess_Per").Visible = False

        FLXGRD_MaterialItem.Cols("Item_Code").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Item_Name").AllowEditing = False
        FLXGRD_MaterialItem.Cols("UM_Name").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Prev_Item_Qty").AllowEditing = False
        FLXGRD_MaterialItem.Cols("MRN_Qty").Visible = True
        FLXGRD_MaterialItem.Cols("MRN_Qty").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Item_Rate").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Vat_Per").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Cess_Per").AllowEditing = False
        FLXGRD_MaterialItem.Cols("TaxableAmt").AllowEditing = False

        FLXGRD_MaterialItem.Cols("Amount").AllowEditing = False
        FLXGRD_MaterialItem.Cols("GST").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Cess").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Item_Qty").AllowEditing = True

        FLXGRD_MaterialItem.Cols("Amount").Visible = False
        FLXGRD_MaterialItem.Cols("GST").Visible = False
        FLXGRD_MaterialItem.Cols("Cess").Visible = False

        FLXGRD_MaterialItem.Cols("Stock_Detail_Id").AllowEditing = False

        FLXGRD_MaterialItem.Cols("Item_Name").Width = 230

        If cbSetOpen.Checked Then
            FLXGRD_MaterialItem.Cols("Item_Rate").AllowEditing = True
            FLXGRD_MaterialItem.Cols("Vat_Per").AllowEditing = True
            FLXGRD_MaterialItem.Cols("Cess_Per").AllowEditing = True
        Else
            FLXGRD_MaterialItem.Cols("Item_Rate").AllowEditing = False
            FLXGRD_MaterialItem.Cols("Vat_Per").AllowEditing = False
            FLXGRD_MaterialItem.Cols("Cess_Per").AllowEditing = False
        End If

        If cbSetOpen.Checked Then
            FLXGRD_MaterialItem.Cols("MRN_Qty").Visible = False
            FLXGRD_MaterialItem.Cols("Item_Name").Width = 300
        End If

        FLXGRD_MaterialItem.Cols("Item_Code").Width = 100

        FLXGRD_MaterialItem.Cols("UM_Name").Width = 40
        FLXGRD_MaterialItem.Cols("Prev_Item_Qty").Width = 90
        FLXGRD_MaterialItem.Cols("MRN_Qty").Width = 75
        FLXGRD_MaterialItem.Cols("Item_Rate").Width = 70
        FLXGRD_MaterialItem.Cols("Vat_Per").Width = 50
        FLXGRD_MaterialItem.Cols("Cess_Per").Width = 50
        FLXGRD_MaterialItem.Cols("Item_Qty").Width = 70
        FLXGRD_MaterialItem.Cols("TaxableAmt").Width = 90
        FLXGRD_MaterialItem.Cols("Amount").Width = 50
        FLXGRD_MaterialItem.Cols("GST").Width = 50
        FLXGRD_MaterialItem.Cols("Cess").Width = 50
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
    Private Sub generate_tree()
        FLXGRD_MaterialItem.DataSource = Nothing
        FLXGRD_MaterialItem.DataSource = dtable_Item_List
        'format_grid()
        SetGridSettingValues()
        If cbSetOpen.Checked Then
            If FLXGRD_MaterialItem.Rows.Count > 1 Then
                'flxItems.Tree.Style = TreeStyleFlags.CompleteLeaf
                'flxItems.Tree.Column = 2
                'flxItems.AllowMerging = AllowMergingEnum.None
                'Dim totalOn As Integer = flxItems.Cols("Batch_Qty").SafeIndex
                'flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
                'totalOn = flxItems.Cols("transfer_Qty").SafeIndex
                'flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

                Dim cs1 As C1.Win.C1FlexGrid.CellStyle
                cs1 = Me.FLXGRD_MaterialItem.Styles.Add("Item_Rate")
                'cs1.ForeColor = Color.White
                cs1.BackColor = Color.Orange
                cs1.Border.Style = BorderStyleEnum.Raised


                Dim cs2 As C1.Win.C1FlexGrid.CellStyle
                cs2 = Me.FLXGRD_MaterialItem.Styles.Add("Vat_Per")
                'cs2.ForeColor = Color.White
                cs2.BackColor = Color.Gold
                cs2.Border.Style = BorderStyleEnum.Raised


                Dim cs As C1.Win.C1FlexGrid.CellStyle
                cs = Me.FLXGRD_MaterialItem.Styles.Add("Cess_Per")
                'cs.ForeColor = Color.White
                cs.BackColor = Color.Gold
                cs.Border.Style = BorderStyleEnum.Raised

                'Dim cs5 As C1.Win.C1FlexGrid.CellStyle
                'cs5 = Me.FLXGRD_MaterialItem.Styles.Add("ACess")
                ''cs.ForeColor = Color.White
                'cs5.BackColor = Color.Gold
                'cs5.Border.Style = BorderStyleEnum.Raised

                Dim i As Integer
                For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1
                    If Not FLXGRD_MaterialItem.Rows(i).IsNode Then
                        FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("Item_Rate").SafeIndex, cs1)
                        FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("Vat_Per").SafeIndex, cs2)
                        FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("Cess_Per").SafeIndex, cs)
                        'FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("ACess").SafeIndex, cs5)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub FLXGRD_MaterialItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_MaterialItem.AfterEdit
        If IsNumeric(FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty")) = True Then
            If FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") > FLXGRD_MaterialItem.Rows(e.Row)("Prev_Item_Qty") And cbSetOpen.Checked = False Then
                FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") = 0
            Else
                FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty")), 2)

                Dim itemqty As Decimal = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty")), 2)
                Dim itemRate As Decimal = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Item_Rate")), 2)
                Dim Vat As Decimal = Math.Round(Convert.ToDouble(FLXGRD_MaterialItem.Rows(e.Row)("Vat_Per")), 2)

                CalculateAmount()

            End If
        Else
            FLXGRD_MaterialItem.Rows(e.Row)("Item_Qty") = 0
        End If
    End Sub
    Private Sub getMRNDetail(ByVal Receive_ID As Integer)


    End Sub

    Private Sub cmbMRNNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillNo.SelectedIndexChanged

        Try

            If (DebitNoteId < 1) Then
                set_new_initilize()
            End If

            Dim ds As DataSet
            Dim ds1 As DataSet
            Dim MRNNo As Int32
            MRNNo = Convert.ToInt32(cmbBillNo.SelectedValue)
            ds = clsObj.fill_Data_set("Get_MRN_Details_DebitNote", "@V_MRN_NO", MRNNo)
            If ds.Tables(0).Rows.Count > 0 Then
                dtable_Item_List = ds.Tables(0).Copy
                FLXGRD_MaterialItem.DataSource = dtable_Item_List
                generate_tree()
            End If

            Dim Query As String = " Select Invoice_No , Convert(VARCHAR(20), Invoice_date, 106)As Invoice_date, MRN_TYPE FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER WHERE MRN_NO=" & MRNNo & " And Division_ID =   " & v_the_current_division_id &
           "UNION ALL Select Invoice_No , Convert(VARCHAR(20), Invoice_date, 106)As Invoice_date, MRN_TYPE FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER WHERE MRN_NO=" & MRNNo


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
            If (DebitNoteId < 1) Then
                SetGstLabels()
            End If
        End If


    End Sub

    Private Sub frm_DebitNote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        flag = "save"
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

        'cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)

        'If cmbSupplier.SelectedValue > 0 Then
        If AccountId > 0 Then
            Query = " Select MRN_NO As MRN_ID, Invoice_No AS MRN_NO FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER WHERE PO_ID IN (SELECT PO_ID FROM dbo.PO_MASTER WHERE PO_SUPP_ID=" & cmbSupplier.SelectedValue & ") AND Division_ID =   " & v_the_current_division_id &
            "UNION ALL SELECT MRN_NO AS MRN_ID , Invoice_No AS MRN_NO FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER WHERE Vendor_ID=" & cmbSupplier.SelectedValue & " ORDER BY MRN_id"
            Dt = clsObj.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("MRN_ID") = -1
            Dtrow("MRN_NO") = "Select Bill No"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbBillNo.DisplayMember = "MRN_NO"
            cmbBillNo.ValueMember = "MRN_ID"
            cmbBillNo.DataSource = Dt
            cmbBillNo.SelectedIndex = 0
            'cmbMRNNo.Focus()
            cmbBillNo.Visible = False
            txt_INVNo.Visible = False
            If cbSetOpen.Checked Then
                txt_INVNo.Visible = True
            Else
                cmbBillNo.Visible = True
            End If
        End If

    End Sub

    Private Sub BindSupplierCombo()
        obj.ComboBind(cmbSupplier, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ VAT_NO) AS ACC_NAME from ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID in (1,2,3,6) Order by ACC_NAME ", "ACC_NAME", "ACC_ID")

    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        FillGrid(txtSearch.Text.Trim())
    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        AccountId = cmbSupplier.SelectedValue
        BindMRNCombo()
        lblAmount.Text = 0
        lblVatAmount.Text = 0
        lblDebit.Text = 0

        If cbSetOpen.Checked Then
            Dim str As String
            str = "SELECT  dbo.Get_GST_Type(" & cmbSupplier.SelectedValue & ") AS GType "
            lblMRN_TYPE.Text = clsObj.ExecuteScalar(str)
        End If

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

        Dim pqty As Double
        Dim qty As Double
        Dim rate As Double
        Dim pgst As Double
        Dim gst As Double
        Dim pcess As Double
        Dim cess As Double
        Dim amt As Double
        Dim pamt As Double
        Dim famt As Double
        Dim fgst As Double
        Dim fcess As Double
        total_exice_amount = 0
        total_item_value = 0
        total_vat_amount = 0
        total_cess_amount = 0
        tot_amt = 0

        pgst = 0
        gst = 0
        pcess = 0
        cess = 0
        pqty = 1
        amt = 0
        pamt = 0
        famt = 0
        fgst = 0
        fcess = 0
        '--------------------Previous Calculation-------------------

        For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1

            rate = 0
            amt = 0
            gst = 0
            cess = 0
            qty = 1

            'If Not cbSetOpen.Checked Then
            '    If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("Prv_Rate") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("Prv_Rate"))) > 0 Then
            '        If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("Item_Qty"))) > 0 Then
            '            pqty = FLXGRD_MaterialItem.Rows(i).Item("Item_Qty")

            '        ElseIf Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("MRN_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("MRN_Qty"))) > 0 Then
            '            pqty = FLXGRD_MaterialItem.Rows(i).Item("MRN_Qty")
            '        End If
            '        pamt = (pqty * FLXGRD_MaterialItem.Rows(i).Item("Prv_Rate"))
            '        pgst = ((FLXGRD_MaterialItem.Rows(i).Item("Prv_Rate") * pqty) * FLXGRD_MaterialItem.Rows(i).Item("Prv_Vat_Per") / 100)
            '        pcess = ((FLXGRD_MaterialItem.Rows(i).Item("Prv_Rate") * pqty) * FLXGRD_MaterialItem.Rows(i).Item("Prv_Cess_Per") / 100)

            '    Else
            '        FLXGRD_MaterialItem.Rows(i).Item("Amount") = 0.00
            '    End If
            'End If

            If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("item_rate") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("item_rate"))) > 0 Then

                If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("Item_Qty"))) > 0 Then
                    qty = FLXGRD_MaterialItem.Rows(i).Item("Item_Qty")
                    If cbSetOpen.Checked Then
                        rate = FLXGRD_MaterialItem.Rows(i).Item("item_rate")
                        pgst = FLXGRD_MaterialItem.Rows(i).Item("Vat_Per")
                        pcess = FLXGRD_MaterialItem.Rows(i).Item("Cess_Per")
                    End If
                ElseIf Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("MRN_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("MRN_Qty"))) > 0 Then
                    qty = FLXGRD_MaterialItem.Rows(i).Item("MRN_Qty")
                End If

                If FLXGRD_MaterialItem.Rows(i).Item("Prv_Rate") <> FLXGRD_MaterialItem.Rows(i).Item("item_rate") Or FLXGRD_MaterialItem.Rows(i).Item("Prv_Vat_Per") <> FLXGRD_MaterialItem.Rows(i).Item("Vat_Per") Or FLXGRD_MaterialItem.Rows(i).Item("Prv_Cess_Per") <> FLXGRD_MaterialItem.Rows(i).Item("Cess_Per") Then
                    rate = FLXGRD_MaterialItem.Rows(i).Item("item_rate")
                    pgst = FLXGRD_MaterialItem.Rows(i).Item("Vat_Per")
                    pcess = FLXGRD_MaterialItem.Rows(i).Item("Cess_Per")
                End If

                If Not cbSetOpen.Checked Then
                    rate = FLXGRD_MaterialItem.Rows(i).Item("item_rate")
                    pgst = FLXGRD_MaterialItem.Rows(i).Item("Vat_Per")
                    pcess = FLXGRD_MaterialItem.Rows(i).Item("Cess_Per")
                End If


                amt = qty * rate
                If lblMRN_TYPE.Text = 2 Then
                    gst = ((rate * qty) * pgst / 100)
                Else
                    gst = Math.Round(((rate * qty) * (pgst / 2) / 100), 2)
                    gst = Math.Round((gst * 2), 2)
                End If
                cess = ((rate * qty) * pcess / 100)

            Else
                FLXGRD_MaterialItem.Rows(i).Item("Amount") = 0.00
            End If

            If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("Item_Qty"))) <= 0 Then
                If Not cbSetOpen.Checked Then
                    amt = 0
                    gst = 0
                    cess = 0
                End If
            End If

            FLXGRD_MaterialItem.Rows(i).Item("TaxableAmt") = Math.Round(amt, 2)
            FLXGRD_MaterialItem.Rows(i).Item("Amount") = Math.Round(amt, 2)
            FLXGRD_MaterialItem.Rows(i).Item("GST") = Math.Round(gst, 2)
            FLXGRD_MaterialItem.Rows(i).Item("Cess") = Math.Round(cess, 2)

            total_item_value = total_item_value + Math.Round(amt, 2)
            total_vat_amount = total_vat_amount + Math.Round(gst, 2)
            total_cess_amount = total_cess_amount + Math.Round(cess, 2)
        Next



        '-------------------Current Calculation--------------------------

        'For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1
        '    If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Rows(i).Item("Item_Qty"))) > 0 Then

        '        FLXGRD_MaterialItem.Rows(i).Item("Amount") = (FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") * FLXGRD_MaterialItem.Rows(i).Item("item_rate"))
        '        total_item_value = total_item_value + (FLXGRD_MaterialItem.Rows(i).Item("Item_Qty") * FLXGRD_MaterialItem.Rows(i).Item("item_rate"))
        '        total_vat_amount = total_vat_amount + ((FLXGRD_MaterialItem.Rows(i).Item("item_rate") * FLXGRD_MaterialItem.Rows.Item(i)("Item_Qty")) * FLXGRD_MaterialItem.Rows(i).Item("Vat_Per") / 100)
        '        total_cess_amount = total_cess_amount + ((FLXGRD_MaterialItem.Rows(i).Item("item_rate") * FLXGRD_MaterialItem.Rows.Item(i)("Item_Qty")) * FLXGRD_MaterialItem.Rows(i).Item("Cess_Per") / 100)
        '    Else
        '        FLXGRD_MaterialItem.Rows(i).Item("Amount") = 0.00
        '    End If
        'Next



        lblAmount.Text = total_item_value.ToString("#0.00")
        lblVatAmount.Text = total_vat_amount.ToString("#0.00")
        lblCessAmount.Text = total_cess_amount.ToString("#0.00")
        lblDebit.Text = (total_item_value + total_vat_amount + total_cess_amount + Convert.ToDouble(IIf(IsNumeric(txtRoundOff.Text), txtRoundOff.Text, 0))).ToString("#0.00")
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
        Dim totalAmount As Decimal
        Dim Tax As Decimal
        Dim Rate As Decimal
        Dim pgst As Decimal = 0
        Dim pcess As Decimal = 0
        Dim qty As Double = 0
        Dim iRow As Integer = 0

        If FLXGRD_MaterialItem.Rows.Count > 1 Then
            For iRow = 1 To FLXGRD_MaterialItem.Rows.Count - 1
                Rate = 0
                Tax = 0
                totalAmount = 0

                If cbSetOpen.Checked Then
                    qty = 1

                    If FLXGRD_MaterialItem.Rows(iRow).Item("Prv_Rate") <> FLXGRD_MaterialItem.Rows(iRow).Item("item_rate") Or FLXGRD_MaterialItem.Rows(iRow).Item("Prv_Vat_Per") <> FLXGRD_MaterialItem.Rows(iRow).Item("Vat_Per") Or FLXGRD_MaterialItem.Rows(iRow).Item("Prv_Cess_Per") <> FLXGRD_MaterialItem.Rows(iRow).Item("Cess_Per") Then
                        Rate = FLXGRD_MaterialItem.Rows(iRow).Item("item_rate")
                        pgst = FLXGRD_MaterialItem.Rows(iRow).Item("Vat_Per")
                    End If
                Else
                    Rate = FLXGRD_MaterialItem.Rows(iRow).Item("item_rate")
                    pgst = FLXGRD_MaterialItem.Rows(iRow).Item("Vat_Per")
                End If

                If Convert.ToDouble(IIf(FLXGRD_MaterialItem.Item(iRow, "Item_Qty") Is DBNull.Value, 0, FLXGRD_MaterialItem.Item(iRow, "Item_Qty"))) > 0 Then
                    qty = FLXGRD_MaterialItem.Item(iRow, "Item_Qty")
                    If cbSetOpen.Checked Then
                        Rate = FLXGRD_MaterialItem.Rows(iRow).Item("item_rate")
                        pgst = FLXGRD_MaterialItem.Rows(iRow).Item("Vat_Per")
                    End If
                Else
                    If Not cbSetOpen.Checked Then
                        qty = 0
                    End If
                End If

                totalAmount = qty * Rate

                Tax = totalAmount * pgst / 100

                GSTTaxTotal += Tax

                Select Case pgst
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

            Next
        End If


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
        If CDate(dgvList.SelectedRows(0).Cells("DebitNote_Date").Value) > CDate(FreezeDate) Then

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
        Else
            MessageBox.Show("System is freezed for this transaction date so further operation do not allowed.")
            Return
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
            cmbSupplier.SelectedValue = dr("DN_CustId")
            BindMRNCombo()
            cmbBillNo.SelectedValue = dr("MRNo").ToString
            txt_INVNo.Text = dr("InvoiceNo")
            txt_INVDate.Text = dr("InvoiceDate")
            txtRemarks.Text = dr("Remarks")
            lblMRN_TYPE.Text = dr("MRN_TYPE")

            If dr("MRNo").ToString = "0" Then
                cbSetOpen.Checked = True
            Else
                cbSetOpen.Checked = False
            End If
            txtRoundOff.Text = dr("RoundOff")

            Dim ds As DataSet
            ds = clsObj.fill_Data_set("GetDebitNoteDetails", "@DebitNoteId", DebitNoteId)
            If ds.Tables(0).Rows.Count > 0 Then
                dtable_Item_List = ds.Tables(0).Copy
                FLXGRD_MaterialItem.DataSource = dtable_Item_List
                generate_tree()
                CalculateAmount()
            End If

        End If
    End Sub

    Private Sub cbSetOpen_CheckedChanged(sender As Object, e As EventArgs) Handles cbSetOpen.CheckedChanged

        If flag = "save" Then
            BindSupplierCombo()
        End If

        set_new_initilize()
        cmbBillNo.Visible = False
        txt_INVNo.Visible = False

        If cbSetOpen.Checked Then
            txt_INVNo.Visible = True
            If cmbBillNo.Items.Count() > 0 Then
                cmbBillNo.SelectedIndex = 0
            End If
        Else
            cmbBillNo.Visible = True
        End If

    End Sub

    Private Sub FLXGRD_MaterialItem_KeyDown(sender As Object, e As KeyEventArgs) Handles FLXGRD_MaterialItem.KeyDown
        Try
            If Not cbSetOpen.Checked Then
                Exit Sub
            End If

            If e.KeyCode = Keys.Space Then
                grdMaterial_Rowindex = FLXGRD_MaterialItem.Row

                frm_Show_search.qry = " SELECT  top 50 im.ITEM_ID ,
		                                ISNULL(im.BarCode_vch, '') AS BARCODE ,
                                        im.ITEM_NAME AS [ITEM NAME] ,
                                        im.MRP_Num AS MRP ,
                                        CAST(im.sale_rate AS NUMERIC(18, 2)) AS RATE ,
                                        ISNULL(litems.LabelItemName_vch, '') AS BRAND ,
                                        ic.ITEM_CAT_NAME AS CATEGORY
                                        FROM      Item_master im
                                        LEFT OUTER JOIN item_detail id ON im.item_id = id.item_id
                                        LEFT OUTER JOIN dbo.ITEM_CATEGORY ic ON im.ITEM_CATEGORY_ID = ic.ITEM_CAT_ID
                                               LEFT JOIN dbo.LabelItem_Mapping AS LIM ON LIM.Fk_ItemId_Num = IM.ITEM_ID        
                                                  AND LIM.Fk_LabelDetailId IN (
                                                  SELECT    Pk_LabelDetailId_Num
                                                  FROM      dbo.Label_Items
                                                  WHERE     fk_LabelId_num = 1 )
        LEFT JOIN dbo.Label_Items AS litems ON litems.Pk_LabelDetailId_Num = LIM.Fk_LabelDetailId
                                               AND litems.fk_LabelId_num = 1
        LEFT JOIN dbo.Label_Master AS BrandMaster ON BrandMaster.Pk_LabelId_Num = litems.fk_LabelId_num
                                                     AND BrandMaster.Pk_LabelId_Num = 1
                                        WHERE   id.Is_active = 1 "


                frm_Show_search.column_name = "BARCODE_VCH"
                frm_Show_search.column_name1 = "ITEM_NAME"
                frm_Show_search.column_name2 = "MRP_Num"
                frm_Show_search.column_name3 = "SALE_RATE"
                frm_Show_search.column_name4 = "LABELITEMNAME_VCH"
                frm_Show_search.column_name5 = "ITEM_CAT_NAME"
                frm_Show_search.cols_no_for_width = "1,2,3,4,5,6"
                frm_Show_search.cols_width = "100,340,70,70,100,105"
                frm_Show_search.extra_condition = ""
                frm_Show_search.ret_column = "ITEM_ID"
                frm_Show_search.item_rate_column = ""
                frm_Show_search.ShowDialog()

                If Not check_item_exist(frm_Show_search.search_result) Then
                    get_row(frm_Show_search.search_result)
                End If



                frm_Show_search.Close()

            ElseIf e.KeyCode = Keys.Delete Then

                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want To remove """ & FLXGRD_MaterialItem.Rows(FLXGRD_MaterialItem.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = FLXGRD_MaterialItem.Rows(FLXGRD_MaterialItem.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(FLXGRD_MaterialItem.DataSource, DataTable)
                    dt.AcceptChanges()
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
                    CalculateAmount()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Function check_item_exist(ByVal item_id As Integer) As Boolean
        Dim iRow As Int32
        check_item_exist = False
        For iRow = 1 To FLXGRD_MaterialItem.Rows.Count - 1
            If FLXGRD_MaterialItem.Item(iRow, "item_id").ToString() <> "" Then
                If Convert.ToInt32(FLXGRD_MaterialItem.Item(iRow, "item_id")) = item_id Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    check_item_exist = True
                    Exit For
                End If
            Else
                check_item_exist = False
            End If
        Next iRow
    End Function
    Public Sub get_row(ByVal item_id As String)

        Dim ds As DataSet
        Dim drItem As DataRow
        Dim sqlqry As String
        Try
            If item_id <> -1 Then

                sqlqry = "SELECT  " &
                                            " IM.ITEM_ID , " &
                                            " IM.ITEM_CODE , " &
                                            " IM.ITEM_NAME , " &
                                            " UM.UM_Name , " &
                                            " SD.Batch_no , " &
                                            " dbo.fn_Format(SD.Expiry_date) AS Expiry_Date, " &
                                            " 0.00 as Item_Rate," & '" dbo.Get_Average_Rate_as_on_date(IM.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," &
                                            "CAST(dbo.Get_Current_Stock(IM.ITEM_ID) AS NUMERIC(18, 2)) as Balance_Qty, " &
                                            " 0.00  as transfer_qty, " &
                                            " SD.STOCK_DETAIL_ID  ,isnull(vm.VAT_PERCENTAGE,0) as VAT_PERCENTAGE,isnull(cm.CessPercentage_num,0) as CessPercentage_num" &
                                    " FROM " &
                                            " ITEM_MASTER  IM " &
                                             "INNER JOIN VAT_MASTER vm On vm.VAT_ID = IM.vat_id  " &
                                             " Left Join dbo.CessMaster cm On cm.pk_CessId_num = IM.fk_CessId_num " &
                                            " INNER JOIN ITEM_DETAIL ID ON IM.ITEM_ID = ID.ITEM_ID " &
                                            " INNER JOIN STOCK_DETAIL SD ON ID.ITEM_ID = SD.Item_id " &
                                            " INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID" &
                                    " where " &
                                            " IM.ITEM_ID = " & item_id '& " and SD.Balance_Qty > 0"
                ds = clsObj.Fill_DataSet(sqlqry)

                'Dim iRowCount As Int32
                'Dim iRow As Int32
                'iRowCount = FLXGRD_MaterialItem.Rows.Count

                'IsInsert = True
                'For iRow = 1 To iRowCount - 1
                '    If FLXGRD_MaterialItem.Item(iRow, 1) = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                '        MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                '        IsInsert = False
                '        Exit For
                '    End If
                'Next iRow

                Dim i As Integer

                obj.RemoveBlankRow(dtable_Item_List, "item_id")

                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        drItem = dtable_Item_List.NewRow

                        drItem("Item_Id") = ds.Tables(0).Rows(0)(0)
                        drItem("Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                        drItem("Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                        drItem("UM_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString()
                        drItem("Prev_Item_Qty") = ds.Tables(0).Rows(0)("Balance_Qty")
                        drItem("MRN_Qty") = 0.0
                        drItem("item_rate") = ds.Tables(0).Rows(0)("Item_Rate")
                        drItem("Vat_Per") = ds.Tables(0).Rows(0)("VAT_PERCENTAGE")
                        drItem("Cess_Per") = ds.Tables(0).Rows(0)("CessPercentage_num")
                        drItem("Item_Qty") = 0.0
                        drItem("Stock_Detail_Id") = ds.Tables(0).Rows(0)("STOCK_DETAIL_ID")
                        drItem("TaxableAmt") = 0.0
                        drItem("Amount") = 0.0
                        drItem("GST") = 0.0
                        drItem("Cess") = 0.0
                        drItem("Prv_Rate") = ds.Tables(0).Rows(0)("Item_Rate")
                        drItem("Prv_Vat_Per") = ds.Tables(0).Rows(0)("VAT_PERCENTAGE")
                        drItem("Prv_Cess_Per") = ds.Tables(0).Rows(0)("CessPercentage_num")
                        dtable_Item_List.Rows.Add(drItem)
                        dtable_Item_List.AcceptChanges()
                    Next
                End If
            End If
            generate_tree()
            Dim Index As Int32 = FLXGRD_MaterialItem.Rows.Count - 1
            FLXGRD_MaterialItem.Row = Index
            FLXGRD_MaterialItem.RowSel = Index
            FLXGRD_MaterialItem.Col = 7
            FLXGRD_MaterialItem.ColSel = 7

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub txtRoundOff_Leave(sender As Object, e As EventArgs) Handles txtRoundOff.Leave
        If String.IsNullOrEmpty(txtRoundOff.Text) Then
            txtRoundOff.Text = "0.00"
        Else
            CalculateAmount()
        End If
    End Sub
End Class
