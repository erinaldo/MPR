Imports C1.Win.C1FlexGrid

Public Class frm_Purchase_Order
    Implements IForm
    Dim Obj As New CommonClass
    Dim clsObj As New Purchase_Order.cls_Purchase_Order_Master
    Dim clsPropObj As New Purchase_Order.cls_Purchase_Order_Prop
    Dim clsindobj As New indent_master.cls_indent_master
    Dim clsindpropobj As New indent_master.cls_indent_master_prop
    Dim dtable_Item_List As DataTable
    Dim flag As String
    Dim _po_id As Integer
    'Dim edit_mode As Boolean = False
    Dim _rights As Form_Rights

    Dim vatperid() As String
    Dim vatper() As String
    Dim vatpername() As String
    Dim cessperid() As String
    Dim cessper() As String
    Dim cesspername() As String
    Dim poqty() As String
    Dim itemvalue() As String
    Dim itemrate() As String

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initialization()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Function GetIndentCode() As String
        ' Try
        Dim Pre As String ' Store indent prefix 
        Pre = Obj.getPrefixCode("INDENT_PREFIX", "DIVISION_SETTINGS")
        Return Pre
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        '
        'End Try
    End Function

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        Dim dt As DataTable
        Dim i As Integer
        Dim VatTotalAmt As String
        Dim Result As String

        If cmbPOType.SelectedIndex <= 0 Then
            erp.SetError(cmbPOType, "Please Select Purchase Order type first.")
            Exit Sub
        Else
            erp.Clear()
        End If
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)

        If cmbSupplier.SelectedIndex <= 0 Then
            erp.SetError(cmbSupplier, "Please Select supplier first.")
            Exit Sub
        Else
            erp.Clear()
        End If

        If flxItemList.Rows.Count <= 0 Then
            MsgBox("Please choose atleast one item for this purchase order.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        Else
            erp.Clear()
        End If

        dt = flxItemList.DataSource
        For i = 0 To dt.Rows.Count - 1
            If Convert.ToDouble(dt.Rows(i)("item_rate")) <= 0 Then
                MsgBox("""" & dt.Rows(i)("item_name") & """ has zero rate." & vbCrLf & "It should not be blank/zero.", MsgBoxStyle.Information, gblMessageHeading)
                Exit Sub
            End If
        Next

        Dim ds As New DataSet()
        ds = clsObj.fill_Data_set("GET_PO_NO", "@DIV_ID", v_the_current_division_id)
        If ds.Tables(0).Rows.Count = 0 Then
            MsgBox("Purchase Order series does not exists", MsgBoxStyle.Information, gblMessageHeading)
            ds.Dispose()
            Exit Sub
        Else
            If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                MsgBox("Purchase Order series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                ds.Dispose()
                Exit Sub
            ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                MsgBox("Purchase Order series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                ds.Dispose()
                Exit Sub
            Else
                clsPropObj.PO_CODE = ds.Tables(0).Rows(0)(0).ToString()
                clsPropObj.PO_NO = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                ds.Dispose()
            End If
        End If

        Try
            VatTotalAmt = CalculateAmount()
            If validate() = True Then

                ''Indent Master
                'clsindpropobj.INDENT_ID = Convert.ToInt32(Obj.getMaxValue("INDENT_ID", "INDENT_MASTER"))
                'clsindpropobj.INDENT_CODE = GetIndentCode()
                'clsindpropobj.INDENT_NO = Convert.ToInt32(Obj.getMaxValue("INDENT_ID", "INDENT_MASTER"))
                'clsindpropobj.INDENT_DATE = Convert.ToDateTime(dtpPODate.Value.ToString("dd-MMM-yyyy"))
                'clsindpropobj.REQUIRED_DATE = Convert.ToDateTime(dtpPODate.Value.ToString("dd-MMM-yyyy"))
                'clsindpropobj.INDENT_REMARKS = "Auto Generated from Purchase Order."
                'clsindpropobj.INDENT_STATUS = 2
                'clsindpropobj.CREATED_BY = v_the_current_logged_in_user_name
                'clsindpropobj.CREATION_DATE = Now
                'clsindpropobj.MODIFIED_BY = v_the_current_logged_in_user_name
                'clsindpropobj.MODIFIED_DATE = Now
                'clsindpropobj.DIVISION_ID = v_the_current_division_id


                Dim GrossTotalVat As [String]() = (VatTotalAmt.Split(","c))
                clsPropObj.PO_ID = Obj.getMaxValue("PO_ID", "PO_MASTER")
                clsPropObj.PO_DATE = Convert.ToDateTime(dtpPODate.Value.ToString("dd-MMM-yyyy"))
                clsPropObj.PO_START_DATE = Convert.ToDateTime(dtpStartDate.Value.ToString("dd-MMM-yyyy"))
                clsPropObj.PO_END_DATE = Convert.ToDateTime(dtpEndDate.Value.ToString("dd-MMM-yyyy"))
                clsPropObj.PO_REMARKS = txtPORemarks.Text.ToUpper()
                cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
                clsPropObj.PO_SUPP_ID = Convert.ToInt32(cmbSupplier.SelectedValue)
                clsPropObj.PO_QUALITY_ID = Convert.ToInt32(cmbQualityRate.SelectedValue)
                clsPropObj.PO_DELIVERY_ID = Convert.ToInt32(cmbDeliveryRate.SelectedValue)
                clsPropObj.PO_STATUS = Convert.ToInt32(POStatus.Fresh)
                clsPropObj.PATMENT_TERMS = txtPaymentTerms.Text.ToUpper()
                clsPropObj.TRANSPORT_MODE = dtpTransMode.Text
                clsPropObj.TOTAL_AMOUNT = Convert.ToDecimal(GrossTotalVat(0).ToString())
                clsPropObj.VAT_AMOUNT = Convert.ToDecimal(GrossTotalVat(1).ToString())
                clsPropObj.CESS_AMOUNT = Convert.ToDecimal(GrossTotalVat(2).ToString())
                clsPropObj.NET_AMOUNT = Convert.ToDecimal(GrossTotalVat(3).ToString())
                clsPropObj.EXICE_AMOUNT = Convert.ToDecimal(GrossTotalVat(4).ToString())
                clsPropObj.PO_TYPE = cmbPOType.SelectedValue
                clsPropObj.OCTROI = dtpOctroi.Text
                clsPropObj.PRICE_BASIS = dtpPriceBasis.Text

                clsPropObj.FRIEGHT = Convert.ToInt32(dtpFreight.SelectedValue)

                clsPropObj.OTHER_CHARGES = Convert.ToDecimal(txtOtherCharges.Text)
                'clsPropObj.CESS = Convert.ToDecimal("0.0".ToString())
                clsPropObj.DISCOUNT_AMOUNT = Convert.ToDouble(txtDiscountAmount.Text)
                clsPropObj.CREATED_BY = v_the_current_logged_in_user_name
                clsPropObj.CREATION_DATE = Now
                clsPropObj.MODIFIED_BY = ""
                clsPropObj.MODIFIED_DATE = NULL_DATE
                clsPropObj.DIVISION_ID = v_the_current_division_id
                If chk_VatCal.Checked = True Then
                    clsPropObj.VAT_ON_EXICE = 1
                Else
                    clsPropObj.VAT_ON_EXICE = 0
                End If

                If CHK_OPEN_PO_QTY.Checked = True Then
                    clsPropObj.OPEN_PO_QTY = 1
                Else
                    clsPropObj.OPEN_PO_QTY = 0
                End If

                If chk_Composition.Checked = True Then
                    clsPropObj.Special_Scheme = "Composite"
                Else
                    clsPropObj.Special_Scheme = "Nill"
                End If

                clsPropObj.PO_Items = flxItemList.DataSource

                If flag = "save" Then
                    Result = ""
                    Result = clsObj.Insert_PO_MASTER(clsPropObj)
                    'MsgBox(Result, MsgBoxStyle.Information, gblMessageHeading)
                    If MsgBox(Result & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                        Obj.RptShow(enmReportName.RptPurchaseOrderPrint, "PO_ID", CStr(clsPropObj.PO_ID), CStr(enmDataType.D_int))
                    End If
                Else
                    Result = ""
                    clsPropObj.PO_NO = Convert.ToDecimal(txtPONO.Text)
                    clsPropObj.PO_CODE = txtPOPrefix.Text
                    clsPropObj.PO_ID = _po_id
                    Result = clsObj.update_PO_MASTER(clsPropObj)
                    MsgBox(Result, MsgBoxStyle.Information, gblMessageHeading)
                    If MsgBox(Result & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                        Obj.RptShow(enmReportName.RptPurchaseOrderPrint, "PO_ID", CStr(clsPropObj.PO_ID), CStr(enmDataType.D_int))
                    End If
                End If
                Call new_initialization()
                'grdPOList.SelectedIndex = -1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function validate() As Boolean
        Dim iRow As Int32
        validate = True
        Dim blnRecExist As Boolean
        blnRecExist = False
        txtPORemarks.Focus()

        For iRow = 1 To flxItemList.Rows.Count - 1
            If Convert.ToDecimal(flxItemList.Item(iRow, ("Po_Qty"))) > 0 Then
                blnRecExist = True
                Exit For
            End If
        Next iRow


        If blnRecExist = True Then
            validate = True
            Exit Function
        Else
            validate = False
            MsgBox("Select aleast one valid item  to save Purchase Order information", vbExclamation, gblMessageHeading)
            Exit Function
        End If
    End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                Obj.RptShow(enmReportName.RptPurchaseOrderPrint, "PO_ID", CStr(flxPOList.Rows(flxPOList.CursorCell.r1).Item("po_id").ToString()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    Obj.RptShow(enmReportName.RptPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub new_initialization()
        ' edit_mode = False
        cmbPOType.SelectedIndex = 0
        cmbSupplier.SelectedIndex = 0
        dtpoFromDate.Value = Now.AddDays(-10)
        cmbQualityRate.SelectedIndex = 0
        cmbDeliveryRate.SelectedIndex = 0
        lblAddress.Text = ""
        lblStatus.Text = POStatus.Fresh.ToString
        dtable_Item_List.Rows.Clear()
        txtPORemarks.Text = ""
        txtPaymentTerms.Text = ""
        lblItemValue.Text = "0.00"
        lblVatAmount.Text = "0.00"
        lblCESSAmount.Text = "0.00"
        txtOtherCharges.Text = "0.00"
        txtDiscountAmount.Text = "0.00"
        lblNetAmount.Text = "0.00"
        TabControl1.SelectTab(1)
        'cmbPOType.Focus()
        flag = "save"
        SetGstLabels()

    End Sub

    Private Sub frm_Purchase_Order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            clsObj.ComboBind(cmbPOType, "Select PO_TYPE_ID,PO_TYPE_NAME from PO_TYPE_MASTER", "PO_TYPE_NAME", "PO_TYPE_ID", True)
            clsObj.ComboBind(cmbSupplier, "Select ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ ISNULL(VAT_NO,'')) AS ACC_NAME from ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID in (1,2,3,6) Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
            clsObj.ComboBind(cmbQualityRate, "Select QR_ID,QR_CODE from QUALITY_RATING_MASTER", "QR_CODE", "QR_ID", True)
            clsObj.ComboBind(cmbDeliveryRate, "Select DR_ID,DR_CODE from DELIVERY_RATING_MASTER", "DR_CODE", "DR_ID", True)
            clsObj.ComboBind(cmbFilterSupp, "Select ACC_ID,LTRIM(ACC_NAME +'  '+ CASE WHEN AG_ID=1 THEN 'Dr ' ELSE CASE WHEN AG_ID=2 THEN 'Cr ' ELSE '' END END +'  '+ ISNULL(VAT_NO,'')) AS ACC_NAME from ACCOUNT_MASTER WHERE AG_ID in (1,2,3,6) Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
            clsObj.ComboBind_Enum(cmdPoStatus, New POStatus)
            ' clsObj.FormatGrid(flxItemList)
            ' clsObj.FormatGrid(flxPOList)
            table_style()
            new_initialization()
            flxPOList.SelectionMode = SelectionModeEnum.Row
            Call btnShow_Click(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gblMessageHeading)
        End Try
    End Sub

    Private Sub table_style()
        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()

        dtable_Item_List = New DataTable()
        dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
        dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("Req_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("PO_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_Rate", GetType(System.Double))
        dtable_Item_List.Columns.Add("DType", GetType(System.String))
        dtable_Item_List.Columns.Add("DISC", GetType(System.Decimal))
        dtable_Item_List.Columns.Add("Vat_Id", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Vat_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("Exice_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("Vat_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_Value", GetType(System.Decimal))
        dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Indent_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("UM_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Cess_Id", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Cess_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("Cess_Per", GetType(System.Double))

        flxItemList.DataSource = dtable_Item_List

        format_grid()


    End Sub

    Private Sub format_grid()

        flxItemList.Cols(0).Width = 10
        flxItemList.Cols("UM_ID").Visible = False
        flxItemList.Cols("Indent_ID").Visible = False
        flxItemList.Cols("Item_ID").Visible = False
        flxItemList.Cols("Vat_Id").Visible = False
        flxItemList.Cols("Cess_Id").Visible = False

        flxItemList.Cols("Item_Code").Caption = "BarCode"
        flxItemList.Cols("Item_Name").Caption = "Item Name"
        flxItemList.Cols("UM_Name").Caption = "UOM"
        flxItemList.Cols("Req_Qty").Caption = "Req.Qty"
        flxItemList.Cols("PO_Qty").Caption = "PO Qty"
        flxItemList.Cols("Item_Rate").Caption = "Item Rate"
        flxItemList.Cols("DType").Caption = "DType"
        flxItemList.Cols("DISC").Caption = "Disc"

        flxItemList.Cols("Vat_Name").Caption = "Gst Name"
        flxItemList.Cols("Vat_Per").Caption = "GST%"
        flxItemList.Cols("Cess_Name").Caption = "Cess Name"
        flxItemList.Cols("Cess_Per").Caption = "CESS%"
        flxItemList.Cols("Item_Value").Caption = "Item Value"

        flxItemList.Cols("Item_Code").AllowEditing = False
        flxItemList.Cols("Item_Name").AllowEditing = False
        flxItemList.Cols("UM_Name").AllowEditing = False
        flxItemList.Cols("Req_Qty").AllowEditing = False
        flxItemList.Cols("PO_Qty").AllowEditing = True
        flxItemList.Cols("Item_Rate").AllowEditing = True
        flxItemList.Cols("DType").AllowEditing = True
        flxItemList.Cols("DType").ComboList = "P|A"
        flxItemList.Cols("DISC").AllowEditing = True

        flxItemList.Cols("Vat_Name").AllowEditing = False
        flxItemList.Cols("Vat_Per").AllowEditing = False
        flxItemList.Cols("Cess_Name").AllowEditing = False
        flxItemList.Cols("Cess_Per").AllowEditing = False
        flxItemList.Cols("Item_Value").AllowEditing = False


        flxItemList.Cols("Item_Code").Width = 100
        flxItemList.Cols("Item_Name").Width = 255
        flxItemList.Cols("UM_Name").Width = 35
        flxItemList.Cols("Req_Qty").Width = 60
        flxItemList.Cols("PO_Qty").Width = 60
        flxItemList.Cols("Exice_Per").Width = 30
        flxItemList.Cols("Item_Rate").Width = 70
        flxItemList.Cols("DType").Width = 45
        flxItemList.Cols("DISC").Width = 45
        flxItemList.Cols("Vat_Name").Width = 60
        'flxItemList.Cols("Vat_Per").Width = 20
        flxItemList.Cols("Cess_Name").Width = 70
        'flxItemList.Cols("Cess_Per").Width = 20
        flxItemList.Cols("Item_Value").Width = 75

        flxItemList.Cols("Exice_Per").Visible = False
        flxItemList.Cols("Vat_Id").Visible = False
        flxItemList.Cols("Vat_Per").Visible = False
        flxItemList.Cols("Cess_Id").Visible = False
        flxItemList.Cols("Cess_Per").Visible = False

    End Sub

    Private Sub lnkSelectItems_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectItems.LinkClicked
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
        If cmbSupplier.SelectedIndex > 0 Then
            'dtable_Item_List.Rows.Clear()
            frm_Indent_Items.v_supp_id = cmbSupplier.SelectedValue
            frm_Indent_Items.dTable_POItems = flxItemList.DataSource
            frm_Indent_Items.flag = flag
            frm_Indent_Items._po_id = _po_id
            RemoveHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh
            frm_Indent_Items.ShowDialog()
            AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh
            generate_tree()
        Else
            erp.SetError(cmbSupplier, "Please select supplier first.")
        End If
    End Sub

    Private Sub flxItemList_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles flxItemList.AfterDataRefresh
        Try
            generate_tree()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub generate_tree()

        If flxItemList.Rows.Count > 1 Then
            'Dim strSort As String = flxItemList.Cols(1).Name + ", " + flxItemList.Cols(2).Name + ", " + flxItemList.Cols(3).Name
            'Dim dt As DataTable = CType(flxItemList.DataSource, DataTable)
            'If Not dt Is Nothing Then
            '    dt.DefaultView.Sort = strSort
            'End If
            'strSort = ""

            'flxItemList.Tree.Style = TreeStyleFlags.CompleteLeaf
            'flxItemList.Tree.Column = 1
            'flxItemList.AllowMerging = AllowMergingEnum.None

            'Dim totalOn As Integer = flxItemList.Cols("Req_Qty").SafeIndex
            'flxItemList.Subtotal(AggregateEnum.Sum, 0, 2, totalOn)
            'totalOn = flxItemList.Cols("PO_Qty").SafeIndex
            'flxItemList.Subtotal(AggregateEnum.Sum, 0, 2, totalOn)
            'totalOn = flxItemList.Cols("item_value").SafeIndex
            'flxItemList.Subtotal(AggregateEnum.Sum, 0, 2, totalOn)
            'totalOn = flxItemList.Cols("item_rate").SafeIndex
            'flxItemList.Subtotal(AggregateEnum.Max, 0, 2, totalOn)
            'totalOn = flxItemList.Cols("vat_per").SafeIndex
            'flxItemList.Subtotal(AggregateEnum.Max, 0, 2, totalOn)
            'totalOn = flxItemList.Cols("vat_name").SafeIndex
            'flxItemList.Subtotal(AggregateEnum.None, 0, 2, totalOn)
        End If

        Dim cs As C1.Win.C1FlexGrid.CellStyle

        cs = Me.flxItemList.Styles.Add("PO_QTY")
        'cs.ForeColor = Color.White
        cs.BackColor = Color.LimeGreen
        cs.Border.Style = BorderStyleEnum.Raised

        Dim cs1 As C1.Win.C1FlexGrid.CellStyle
        cs1 = Me.flxItemList.Styles.Add("item_rate")
        'cs1.ForeColor = Color.White
        cs1.BackColor = Color.Orange
        cs1.Border.Style = BorderStyleEnum.Raised

        Dim cs2 As C1.Win.C1FlexGrid.CellStyle
        cs2 = Me.flxItemList.Styles.Add("DISC")
        'cs2.ForeColor = Color.White
        cs2.BackColor = Color.Gold
        cs2.Border.Style = BorderStyleEnum.Raised

        Dim cs3 As C1.Win.C1FlexGrid.CellStyle
        cs3 = Me.flxItemList.Styles.Add("DType")
        'cs3.ForeColor = Color.White
        cs3.BackColor = Color.Gold
        cs3.Border.Style = BorderStyleEnum.Raised

        Dim i As Integer
        For i = 1 To flxItemList.Rows.Count - 1
            If Not flxItemList.Rows(i).IsNode Then
                flxItemList.SetCellStyle(i, 5, cs)
                flxItemList.SetCellStyle(i, flxItemList.Cols("item_rate").SafeIndex, cs1)
                flxItemList.SetCellStyle(i, flxItemList.Cols("DISC").SafeIndex, cs2)
                flxItemList.SetCellStyle(i, flxItemList.Cols("DType").SafeIndex, cs3)
            End If
        Next

        CalculateAmount()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        generate_tree()
    End Sub

    Private Sub flxItemList_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles flxItemList.KeyPressEdit
        e.Handled = flxItemList.Rows(flxItemList.CursorCell.r1).IsNode
    End Sub

    Private Sub flxItemList_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles flxItemList.AfterEdit
        If Not IsNumeric(flxItemList.Rows(e.Row).Item("po_qty")) Then
            flxItemList.Rows(e.Row).Item("po_qty") = 0
        End If
        If flxItemList.Rows(e.Row).Item("req_qty") < flxItemList.Rows(e.Row).Item("po_qty") And Not flxItemList.Rows(flxItemList.CursorCell.r1).IsNode And flxItemList.Rows(e.Row).Item("indent_id") <> -1 Then
            flxItemList.Rows(e.Row).Item("po_qty") = 0
        End If
        CalculateAmount()
    End Sub

    Private Function CalculateAmount() As String
        Try

            Dim i As Integer
            Dim Str As String


            Dim total_item_value As Decimal
            Dim item_value As Decimal
            Dim total_vat_amount As Decimal
            Dim total_cess_amount As Decimal
            Dim total_exice_amount As Decimal
            Dim tot_amt As Decimal
            total_exice_amount = 0.00
            total_item_value = 0.00
            total_vat_amount = 0.00
            total_cess_amount = 0.00
            tot_amt = 0.00
            item_value = 0.00

            If Not IsNumeric(txtOtherCharges.Text) Then
                txtOtherCharges.Text = 0.0
            End If
            If Not IsNumeric(txtDiscountAmount.Text) Then
                txtDiscountAmount.Text = 0.0

            End If

            For i = 1 To flxItemList.Rows.Count - 1
                If flxItemList.Rows(i).IsNode Then
                    tot_amt = tot_amt + (flxItemList.Rows(i).Item("PO_Qty") * flxItemList.Rows(i).Item("Item_rate"))
                End If
            Next

            Dim itmVal As Decimal = 0
            Dim exice_per As Decimal = 0.0
            Dim discamt As Decimal = 0.0
            Dim totdiscamt As Decimal = 0.0

            RemoveHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            For i = 1 To flxItemList.Rows.Count - 1
                With flxItemList.Rows(i)
                    If Not .IsNode Then

                        If (.Item("DType")) = "P" Then
                            discamt = Math.Round(((.Item("PO_Qty") * .Item("Item_Rate")) * .Item("DISC") / 100), 2)
                        Else
                            discamt = Math.Round(.Item("DISC"), 2)
                        End If


                        .Item("Item_Value") = Math.Round((.Item("PO_Qty") * .Item("Item_Rate")) - discamt, 2)

                        itmVal = Math.Round((.Item("PO_Qty") * .Item("Item_Rate")) - discamt, 2)

                        total_item_value = total_item_value + itmVal

                        totdiscamt = totdiscamt + discamt

                        exice_per = IIf((.Item("Exice_Per")) Is DBNull.Value, 0, .Item("Exice_Per"))
                        exice_per = exice_per / 100
                        total_exice_amount = total_exice_amount + itmVal * exice_per
                        total_vat_amount = total_vat_amount + (((.Item("PO_Qty") * .Item("Item_Rate")) - discamt) * (.Item("Vat_per")) / 100) '((((.Item("PO_Qty") * .Item("Item_Rate")) - discamt) * .Item("Vat_Per")) / 100)
                        total_cess_amount = total_cess_amount + ((((.Item("PO_Qty") * .Item("Item_Rate")) - discamt) * .Item("Cess_Per")) / 100)
                    End If
                End With
            Next

            AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            txtDiscountAmount.Text = totdiscamt.ToString("#0.00")
            lblItemValue.Text = total_item_value.ToString("#0.00")
            lblVatAmount.Text = total_vat_amount.ToString("#0.00")
            lblCESSAmount.Text = total_cess_amount.ToString("#0.00")


            lblNetAmount.Text = (total_item_value + total_vat_amount + total_cess_amount + total_exice_amount + txtOtherCharges.Text).ToString("#0.00") '- txtDiscountAmount.Text).ToString("#0.00")
            'Return lblItemValue.Text + "," + lblVatAmount.Text + "," + lblNetAmount.Text + "," + lblNetAmount.Text + "," + ExiceGross
            Str = total_item_value.ToString("#0.00") + "," + total_vat_amount.ToString("#0.00") + "," + total_cess_amount.ToString("#0.00") + "," + lblNetAmount.Text + "," + total_exice_amount.ToString()

            SetGstLabels()

            Return Str
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return ""
    End Function

    Private Sub txtOtherCharges_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOtherCharges.TextChanged, txtDiscountAmount.TextChanged
        If Not IsNumeric(CType(sender, TextBox).Text) Then CType(sender, TextBox).Text = 0
        CalculateAmount()
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            Dim param As String, val As String
            param = ""
            val = ""

            param = "@v_supp_id,@v_po_number,@v_po_date_from,@v_po_date_to,@v_po_status,@v_div_id"

            cmbFilterSupp.SelectedIndex = cmbFilterSupp.FindStringExact(cmbFilterSupp.Text)

            If cmbFilterSupp.SelectedIndex > 0 Then
                val += Convert.ToString(cmbFilterSupp.SelectedValue)
            Else
                val += "-1"
            End If



            If txtPONumber.Text.Trim() <> "" Then
                val += "," & txtPONumber.Text
            Else
                val += "," + "-1"
            End If

            If dtpoFromDate.Text.Trim() <> "" Then
                val += "," & Convert.ToString(dtpoFromDate.Text)
            Else
                val += ","
            End If

            If dtpoToDate.Text.Trim() <> "" Then
                val += "," & Convert.ToString(dtpoToDate.Text)
            Else
                val += ","
            End If

            '***************Commented for the time being, as Fresh status has Index value 0 *********
            '' ''If cmdPoStatus.SelectedIndex <> 0 Then
            '' ''    val += "," & cmdPoStatus.SelectedValue
            '' ''Else
            '' ''    val += ",2"
            '' ''End If
            '***************Commented for the time being, as Fresh status has Index value 0 *********

            val += "," & cmdPoStatus.SelectedValue
            val += "," & v_the_current_division_id

            Dim ds As New DataSet()
            ds = clsObj.fill_Data_set("GET_FILTER_PO_LIST", param, val)
            flxPOList.DataSource = ds.Tables(0)


            flxPOList.Cols("PO_ID").Visible = False
            flxPOList.Cols("PO_SUPP_ID").Visible = False
            flxPOList.Cols("po_number").Caption = "PO Number"
            flxPOList.Cols("PO_DATE").Caption = "PO Date"
            flxPOList.Cols("ACC_NAME").Caption = "Supplier Name"
            flxPOList.Cols("PO_START_DATE").Caption = "Start Date"
            flxPOList.Cols("PO_END_DATE").Caption = "End Date"
            flxPOList.Cols("PO_STATUS").Caption = "PO Status"



            flxPOList.Cols(0).Width = 20
            flxPOList.Cols("po_number").Width = 100
            flxPOList.Cols("PO_DATE").Width = 100
            flxPOList.Cols("ACC_NAME").Width = 355
            flxPOList.Cols("PO_START_DATE").Width = 100
            flxPOList.Cols("PO_END_DATE").Width = 100
            flxPOList.Cols("PO_STATUS").Width = 100

            flxPOList.Select()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSupplier.SelectedIndexChanged

        Dim dt As DataTable
        Dim ds As DataSet
        Dim i As Integer
        Dim strSql As String
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)

        If cmbSupplier.SelectedValue > 0 Then
            strSql = "SELECT ACCOUNT_MASTER.ADDRESS_PRIM + ' - {'  + ISNULL(CITY_MASTER.CITY_NAME,'') + '}'"
            strSql = strSql & " FROM ACCOUNT_MASTER LEFT OUTER JOIN"
            strSql = strSql & " CITY_MASTER ON ACCOUNT_MASTER.CITY_ID = CITY_MASTER.CITY_ID"
            strSql = strSql & " WHERE ACCOUNT_MASTER.ACC_ID = " & cmbSupplier.SelectedValue
            lblAddress.Text = clsObj.Fill_DataSet(strSql).Tables(0).Rows(0)(0)

            Dim NewstrSql As String
            Dim dsdata As DataSet
            NewstrSql = "SELECT STATE_ID,isUT_bit FROM dbo.STATE_MASTER WHERE STATE_ID IN(SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT CITY_ID FROM dbo.DIVISION_SETTINGS))"
            NewstrSql = NewstrSql & " SELECT STATE_ID,isUT_bit FROM dbo.STATE_MASTER WHERE STATE_ID IN(SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT CITY_ID FROM dbo.ACCOUNT_MASTER WHERE ACC_ID=" & cmbSupplier.SelectedValue & "))"
            dsdata = clsObj.Fill_DataSet(NewstrSql)


            'SCGST
            'IGST
            'UGST
            If dsdata.Tables(0).Rows(0)(0) <> dsdata.Tables(1).Rows(0)(0) Then
                cmbPOType.Text = "IGST"
            Else
                If dsdata.Tables(0).Rows(0)(1) = True Then
                    cmbPOType.Text = "UGST"
                Else
                    cmbPOType.Text = "SGST"
                End If
            End If


        End If
        If Not flxItemList.DataSource Is Nothing Then

            dt = flxItemList.DataSource

            For i = 0 To dt.Rows.Count - 1
                strSql = "DECLARE @rate NUMERIC (18,2);"
                strSql = strSql & " SELECT @rate=SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE FROM "
                strSql = strSql & " SUPPLIER_RATE_LIST INNER JOIN SUPPLIER_RATE_LIST_DETAIL ON SUPPLIER_RATE_LIST.SRL_ID = SUPPLIER_RATE_LIST_DETAIL.SRL_ID "
                strSql = strSql & " WHERE SUPPLIER_RATE_LIST_DETAIL.ITEM_ID = " & dt.Rows(i)("ITEM_ID").ToString() & "  AND (SUPPLIER_RATE_LIST.SUPP_ID = " & cmbSupplier.SelectedValue & ") AND (SUPPLIER_RATE_LIST.ACTIVE = 1);"
                strSql = strSql & " SELECT ISNULL(@rate,0);"
                strSql = strSql & " SELECT ITEM_DETAIL.PURCHASE_VAT_ID, VAT_MASTER.VAT_PERCENTAGE, VAT_MASTER.VAT_NAME FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID "
                strSql = strSql & " WHERE (ITEM_DETAIL.ITEM_ID = " & dt.Rows(i)("ITEM_ID").ToString & " )"
                ds = clsObj.Fill_DataSet(strSql)
                dt.Rows(i)("ITEM_RATE") = ds.Tables(0).Rows(0)(0)
                dt.Rows(i)("VAT_NAME") = ds.Tables(1).Rows(0)("VAT_NAME")
                dt.Rows(i)("VAT_PER") = ds.Tables(1).Rows(0)("VAT_PERCENTAGE")
                dt.Rows(i)("Vat_Id") = ds.Tables(1).Rows(0)("PURCHASE_VAT_ID")
                dt.Rows(i)("Item_Value") = (dt.Rows(i)("PO_Qty") * dt.Rows(i)("Item_Rate")) + ((dt.Rows(i)("PO_Qty") * dt.Rows(i)("Item_Rate") * dt.Rows(i)("Vat_Per")) / 100)
            Next
            SetGstLabels()
        End If

    End Sub

    Private Sub lnkCalculatePOAmt_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCalculatePOAmt.LinkClicked
        CalculateAmount()
    End Sub

    Private Function validation() As Boolean
        Dim dtpTest As DateTime
        ' Dim lblItem As Label
        Try
            dtpTest = Convert.ToDateTime(dtpPODate.Value)
        Catch
            MsgBox("Not a valid PO Date.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End Try
        Try
            dtpTest = Convert.ToDateTime(dtpoToDate.Value)
        Catch
            MsgBox("Not a valid To Date.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End Try
        If flxItemList.Rows.Count < 1 Then
            MsgBox("Enter atleast one item in PO.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If

        If flxItemList.Visible = False Then
            MsgBox("Enter atleast one item in PO.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        Else
            Return True
        End If

        'lblItem = DirectCast((grdItemList.FooterRow.FindControl("lblTotal")), Label)
        'If lblItem.Text = "" Then
        '    lblMsg.Text = "There may be no Item in the PO or the Item Qty/Rate is Zero."
        '    updpnlMain.Update()
        '    Return False
        'ElseIf Convert.ToDouble(lblItem.Text) <= 0 Then
        '    lblMsg.Text = "There may be no Item in the PO or the Item Qty/Rate is Zero."
        '    updpnlMain.Update()
        '    Return False
        'Else
        '    Return True
        'End If

    End Function

    Private Sub flxPOList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxPOList.DoubleClick
        Try
            If _rights.allow_edit = "N" Then
                RightsMsg()
                Exit Sub
            End If

            new_initialization()

            If flxPOList.Rows(flxPOList.CursorCell.r1)("PO_STATUS").ToString().ToUpper = POStatus.Fresh.ToString().ToUpper() Then
                flag = "update"
                _po_id = Convert.ToInt32(flxPOList.Rows(flxPOList.CursorCell.r1)("PO_ID"))
                fill_PODetail(_po_id)
            Else
                flag = "view"
                MsgBox("You can't edit this PO." & vbCrLf & "Because this PO is approved/cleared/canceled.", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub fill_PODetail(ByVal strPOID As Integer)


        Try
            Dim dt As DataTable

            dt = clsObj.fill_Data_set("GET_PO_DETAIL", "@V_PO_ID", strPOID.ToString()).Tables(0)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                dtpStartDate.Value = Convert.ToDateTime(dr("PO_START_DATE")).ToShortDateString()
                dtpEndDate.Value = Convert.ToDateTime(dr("PO_END_DATE")).ToShortDateString()
                txtPaymentTerms.Text = Convert.ToString(dr("PATMENT_TERMS"))
                dtpPODate.Value = Convert.ToDateTime(dr("PO_DATE")).ToShortDateString()
                txtPONO.Text = Convert.ToString(dr("PO_NO"))
                txtPOPrefix.Text = Convert.ToString(dr("PO_CODE"))

                'txtStartDate.Text = Convert.ToDateTime(dr("PO_START_DATE")).ToShortDateString()
                cmbDeliveryRate.SelectedValue = Convert.ToInt32(dr("PO_DELIVERY_ID"))
                cmbQualityRate.SelectedValue = Convert.ToInt32(dr("PO_QUALITY_ID"))
                cmbSupplier.SelectedValue = Convert.ToInt32(dr("PO_SUPP_ID"))
                'dblTotalAmount = Convert.ToDouble(dr("NET_AMOUNT"))

                dtpPriceBasis.Text = Convert.ToString(dr("PRICE_BASIS"))
                dtpFreight.Text = Convert.ToString(dr("FRIEGHT"))
                dtpOctroi.Text = Convert.ToString(dr("OCTROI"))
                dtpTransMode.Text = Convert.ToString(dr("TRANSPORT_MODE"))

                txtOtherCharges.Text = Convert.ToString(dr("OTHER_CHARGES"))
                'txtCessPer.Text = Convert.ToString(dr("CESS_PER"))
                txtDiscountAmount.Text = Convert.ToString(dr("DISCOUNT_AMOUNT"))
                cmbPOType.SelectedValue = Convert.ToString(dr("po_type"))
                CHK_OPEN_PO_QTY.Checked = dr("OPEN_PO_QTY")


                If (Convert.ToString(dr("SpecialSchemeFlag")).Trim() = "Composite") Then
                    chk_Composition.Checked = True
                Else
                    chk_Composition.Checked = False
                End If

                If Convert.ToInt32(dr("PO_STATUS").ToString()) = Convert.ToInt32(POStatus.Fresh) Then
                    lblStatus.Text = POStatus.Fresh.ToString()
                ElseIf Convert.ToInt32(dr("PO_STATUS").ToString()) = Convert.ToInt32(POStatus.Pending) Then
                    lblStatus.Text = POStatus.Pending.ToString()
                ElseIf Convert.ToInt32(dr("PO_STATUS").ToString()) = Convert.ToInt32(POStatus.Cancel) Then
                    lblStatus.Text = POStatus.Cancel.ToString()
                End If
                dtable_Item_List = clsObj.fill_Data_set("GET_PO_ITEM_DETAILS", "@V_PO_ID", strPOID).Tables(0)

                RemoveHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh
                flxItemList.DataSource = dtable_Item_List
                CalculateAmount()
                format_grid()
                'table_style()
                generate_tree()
                AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

                txtPORemarks.Text = Convert.ToString(dr("PO_REMARKS"))
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub flxItemList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles flxItemList.KeyDown
        If e.KeyCode = Keys.Delete Then

            RemoveHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            Dim result As Integer
            Dim item_code As String
            result = MsgBox("Do you want to remove """ & flxItemList.Rows(flxItemList.CursorCell.r1).Item("item_name") & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
            item_code = flxItemList.Rows(flxItemList.CursorCell.r1).Item("item_code")
            If result = MsgBoxResult.Yes Then
restart:
                Dim dt As DataTable
                dt = TryCast(flxItemList.DataSource, DataTable)
                If Not dt Is Nothing Then

                    For Each dr As DataRow In dt.Rows
                        If Convert.ToString(dr("item_code")) = item_code Then
                            dr.Delete()
                            dt.AcceptChanges()
                            GoTo restart
                        End If
                    Next

                End If
            End If
            e.Handled = True
            AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh
            generate_tree()
        End If
    End Sub

    Private Sub lnkSelectItemswithoutIndent_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectItemswithoutIndent.LinkClicked
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
        If cmbSupplier.SelectedIndex > 0 Then

            'frm_Show_search.qry = "SELECT IM.ITEM_ID,IM.ITEM_CODE," & _
            '                   " IM.ITEM_NAME,UM.UM_Name,CM.ITEM_CAT_NAME," & _
            '                   " IM.DIVISION_ID, 0.00 as Quantity " & _
            '                   " FROM ITEM_MASTER  AS IM INNER JOIN " & _
            '                   " ITEM_DETAIL AS ID ON IM.ITEM_ID = ID.ITEM_ID  INNER JOIN  UNIT_MASTER AS UM " & _
            '                   " ON IM.UM_ID = UM.UM_ID INNER JOIN ITEM_CATEGORY AS CM ON " & _
            '                   " IM.ITEM_CATEGORY_ID = CM.ITEM_CAT_ID" & _
            '                   " INNER JOIN dbo.SUPPLIER_RATE_LIST_DETAIL AS SRLD ON SRLD.ITEM_ID = IM.ITEM_ID" & _
            '                   " INNER JOIN dbo.SUPPLIER_RATE_LIST AS SRL ON SRL.SRL_ID=SRLD.SRL_ID" & _
            '                   " where srl.supp_id = " & cmbSupplier.SelectedValue & " AND srl.active = 1 "


            'frm_Show_search.column_name = "Item_Name"
            'frm_Indent_Items.dTable_POItems = flxItemList.DataSource
            ''frm_Show_search.extra_condition = ""
            'frm_Show_search.ret_column = "Item_ID"
            'frm_Show_search.item_rate_column = ""
            'frm_Show_search.cols_no_for_width = "1,2,3"
            'frm_Show_search.cols_width = "60,350,50"
            '' frm_Show_search.ret_column = "division_id"

            frm_Show_Search_RateList.qry = " SELECT  top 50 im.ITEM_ID ,
		                                ISNULL(im.BarCode_vch, '') AS BARCODE ,
                                        im.ITEM_NAME AS [ITEM NAME] ,
                                        im.MRP_Num AS MRP ,
                                        CAST(im.sale_rate AS NUMERIC(18, 2)) AS RATE ,
                                        ISNULL(litems.LabelItemName_vch, '') AS BRAND ,
                                        ic.ITEM_CAT_NAME AS CATEGORY
                                        FROM Item_master im
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
                                        INNER JOIN dbo.SUPPLIER_RATE_LIST_DETAIL As SRLD On SRLD.ITEM_ID = IM.ITEM_ID
                                        INNER JOIN dbo.SUPPLIER_RATE_LIST As SRL On SRL.SRL_ID=SRLD.SRL_ID 
                                        where srl.supp_id = " & cmbSupplier.SelectedValue & " And srl.active = 1 and  id.Is_active = 1 AND SRL.SRL_ID NOT IN(SELECT SRL_ID FROM dbo.CUSTOMER_RATE_LIST_MAPPING)"


            frm_Indent_Items.dTable_POItems = flxItemList.DataSource
            frm_Indent_Items.dTable_POItems_Copy = flxItemList.DataSource
            frm_Show_Search_RateList.column_name = "BARCODE_VCH"
            frm_Show_Search_RateList.column_name1 = "ITEM_NAME"
            frm_Show_Search_RateList.column_name2 = "MRP_Num"
            frm_Show_Search_RateList.column_name3 = "SALE_RATE"
            frm_Show_Search_RateList.column_name4 = "LABELITEMNAME_VCH"
            frm_Show_Search_RateList.column_name5 = "ITEM_CAT_NAME"
            frm_Show_Search_RateList.cols_no_for_width = "1,2,3,4,5,6,7"
            frm_Show_Search_RateList.cols_width = "10,100,320,70,70,90,105"
            frm_Show_Search_RateList.extra_condition = ""
            frm_Show_Search_RateList.ret_column = "ITEM_ID"
            frm_Show_Search_RateList.item_rate_column = ""
            'frm_Show_search.ShowDialog()

            RemoveHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            frm_Show_Search_RateList.ShowDialog()
            'MsgBox(frm_Show_search.search_result)

            If frm_Show_Search_RateList.search_result = -1 Then
                Exit Sub
            End If


            If frm_Show_Search_RateList.search_result <> -1 Then

                Dim stringSeparators() As String = {","}
                Dim result() As String

                result = frm_Show_Search_RateList.search_result.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)

                For Each element As String In result
                    Dim dr As DataRow
                    Dim drItem As DataRow
                    dr = frm_Indent_Items.dTable_POItems.NewRow
                    drItem = frm_Indent_Items.dTable_POItems_Copy.NewRow

                    Dim ds As New DataSet

                    ds = Obj.Fill_DataSet("SELECT ITEM_ID,BARCODE_VCH as ITEM_CODE, ITEM_NAME, UM_Name, UNIT_MASTER.UM_ID, ISNULL(pk_CessId_num,0) AS Cess_Id, ISNULL(CessName_vch,'0%') + ' CESS'  AS Cess_Name, ISNULL(CessPercentage_num,0.00) AS CessPercentage_num  FROM item_master" &
                                    " INNER JOIN dbo.UNIT_MASTER ON dbo.UNIT_MASTER .UM_ID = dbo.ITEM_MASTER .UM_ID Left JOIN dbo.CessMaster ON dbo.CessMaster.pk_CessId_num = dbo.ITEM_MASTER.fk_CessId_num " &
                                    " WHERE ITEM_ID=" & Convert.ToInt32(element) & "")


                    Dim dv_Items As DataView
                    Dim tempdt As DataTable
                    tempdt = frm_Indent_Items.dTable_POItems.Copy
                    dv_Items = tempdt.DefaultView


                    If dv_Items.Count > 0 Then
                        dv_Items.RowFilter = "item_id=" & ds.Tables(0).Rows(0)("item_id").ToString() & ""
                        tempdt = dv_Items.ToTable()
                    End If


                    For i As Integer = 0 To frm_Indent_Items.dTable_POItems.Rows.Count - 1
                        If tempdt.Rows.Count() > 0 Then
                            MsgBox("Item already exists.")
                            Exit Sub
                        End If
                    Next

                    If chk_Composition.Checked = True Then

                        If frm_Indent_Items.dTable_POItems.Rows.Count = 0 Then
                            AddCessPerId(ds.Tables(0).Rows(0)("Cess_Id"), 0)
                            AddCessPer(ds.Tables(0).Rows(0)("Cess_Per"), 0)
                            AddCessPerName(ds.Tables(0).Rows(0)("Cess_Name"), 0)
                        Else
                            AddCessPerId(ds.Tables(0).Rows(0)("Cess_Id"), cessperid.Length)
                            AddCessPer(ds.Tables(0).Rows(0)("Cess_Per"), cessper.Length)
                            AddCessPerName(ds.Tables(0).Rows(0)("Cess_Name"), cesspername.Length)
                        End If

                        dr("Cess_Per") = 0
                        dr("Cess_Id") = 0
                        dr("Cess_Name") = "0% CESS"
                    Else
                        dr("Cess_Per") = ds.Tables(0).Rows(0)("CessPercentage_num")
                        dr("Cess_Id") = ds.Tables(0).Rows(0)("Cess_Id")
                        dr("Cess_Name") = ds.Tables(0).Rows(0)("Cess_Name")
                    End If



                    dr("Item_id") = ds.Tables(0).Rows(0)("ITEM_ID").ToString
                    dr("Item_Code") = ds.Tables(0).Rows(0)("ITEM_CODE").ToString
                    dr("Item_Name") = ds.Tables(0).Rows(0)("ITEM_NAME").ToString
                    dr("UM_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString
                    'dr("Cess_Id") = ds.Tables(0).Rows(0)("Cess_Id")
                    'dr("Cess_Name") = ds.Tables(0).Rows(0)("Cess_Name")
                    'dr("Cess_Per") = ds.Tables(0).Rows(0)("Cess_Per")
                    'dr("UM_Id") = ds.Tables(0).Rows(0)("UM_ID").ToString
                    ds = Obj.Fill_DataSet("DECLARE @rate NUMERIC (18,2);SELECT @rate=SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE FROM SUPPLIER_RATE_LIST INNER JOIN SUPPLIER_RATE_LIST_DETAIL ON SUPPLIER_RATE_LIST.SRL_ID = SUPPLIER_RATE_LIST_DETAIL.SRL_ID WHERE (SUPPLIER_RATE_LIST_DETAIL.ITEM_ID = " & Convert.ToInt32(element) & " ) AND (SUPPLIER_RATE_LIST.SUPP_ID = " & Convert.ToInt32(cmbSupplier.SelectedValue) & ") AND (SUPPLIER_RATE_LIST.ACTIVE = 1);SELECT ISNULL(@rate,0);SELECT     ITEM_DETAIL.PURCHASE_VAT_ID as PURCHASE_VAT_ID, VAT_MASTER.VAT_PERCENTAGE, VAT_MASTER.VAT_NAME FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID WHERE (ITEM_DETAIL.ITEM_ID = " & Convert.ToInt32(element) & " )")
                    dr("Item_Rate") = ds.Tables(0).Rows(0)(0)
                    'dr("Vat_Id") = ds.Tables(1).Rows(0)("PURCHASE_VAT_ID")
                    'dr("Vat_Name") = ds.Tables(1).Rows(0)("VAT_NAME")
                    dr("Req_Qty") = 0.0
                    dr("Po_Qty") = 0.0
                    dr("DType") = "P"
                    dr("Disc") = 0.0

                    If chk_Composition.Checked = True Then

                        If frm_Indent_Items.dTable_POItems.Rows.Count = 0 Then
                            AddVatPerId(ds.Tables(1).Rows(0)("PURCHASE_VAT_ID"), 0)
                            AddVatPer(ds.Tables(1).Rows(0)("VAT_PERCENTAGE"), 0)
                            AddVatPerName(ds.Tables(1).Rows(0)("VAT_NAME"), 0)
                        Else
                            AddVatPerId(ds.Tables(1).Rows(0)("PURCHASE_VAT_ID"), vatperid.Length)
                            AddVatPer(ds.Tables(1).Rows(0)("VAT_PERCENTAGE"), vatper.Length)
                            AddVatPerName(ds.Tables(1).Rows(0)("VAT_NAME"), vatpername.Length)
                        End If

                        dr("Vat_Per") = 0
                        dr("Vat_Id") = 0
                        dr("Vat_Name") = "0% GST"
                    Else
                        dr("Vat_Per") = ds.Tables(1).Rows(0)("VAT_PERCENTAGE")
                        dr("Vat_Id") = ds.Tables(1).Rows(0)("PURCHASE_VAT_ID")
                        dr("Vat_Name") = ds.Tables(1).Rows(0)("VAT_NAME")

                    End If

                    'dr("Exice_Per") = 0
                    'dr("Vat_per") = ds.Tables(1).Rows(0)("VAT_PERCENTAGE")
                    dr("Item_value") = (dr("PO_Qty") * dr("Item_Rate")) + ((dr("PO_Qty") * dr("Item_Rate") * dr("Vat_Per")) / 100)
                    dr("Indent_Id") = -1

                    frm_Indent_Items.dTable_POItems.Rows.Add(dr)

                Next

                dtable_Item_List = frm_Indent_Items.dTable_POItems

                generate_tree()
                AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh
                frm_Show_Search_RateList.Close()

                Dim Index As Int32 = flxItemList.Rows.Count - 1
                flxItemList.Row = Index
                flxItemList.RowSel = Index
                flxItemList.Col = 5
                flxItemList.ColSel = 5

                flxItemList.Select()

            End If
        Else
            erp.SetError(cmbSupplier, "Please select supplier first.")
        End If
    End Sub

    Public Sub get_row(ByVal item_id As Integer)
        Dim dr As DataRow
        'Dim drItemCopy As DataRow
        Dim ds As DataSet
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)

        frm_Indent_Items.dTable_POItems = flxItemList.DataSource

        'frm_Indent_Items.dTable_POItems_Copy = flxItemList.DataSource

        dr = frm_Indent_Items.dTable_POItems.NewRow

        'drItemCopy = frm_Indent_Items.dTable_POItems_Copy.NewRow

        ds = Obj.Fill_DataSet("SELECT ITEM_ID,BarCode_vch as ITEM_CODE, ITEM_NAME, UM_Name, UNIT_MASTER.UM_ID, ISNULL(pk_CessId_num,0) AS Cess_Id, ISNULL(CessName_vch,'0%') + ' CESS'  AS Cess_Name, ISNULL(CessPercentage_num,0.00) AS Cess_Per  FROM item_master" &
                                " INNER JOIN dbo.UNIT_MASTER ON dbo.UNIT_MASTER .UM_ID = dbo.ITEM_MASTER .UM_ID Left JOIN dbo.CessMaster ON dbo.CessMaster.pk_CessId_num = dbo.ITEM_MASTER.fk_CessId_num " &
                                " WHERE ITEM_ID=" & Convert.ToInt32(item_id) & "")

        Dim dv_Items As DataView
        Dim tempdt As DataTable
        tempdt = frm_Indent_Items.dTable_POItems.Copy
        dv_Items = tempdt.DefaultView

        dv_Items.RowFilter = "item_id=" & ds.Tables(0).Rows(0)("item_id").ToString() & ""

        tempdt = dv_Items.ToTable()

        For i As Integer = 0 To frm_Indent_Items.dTable_POItems.Rows.Count - 1
            If tempdt.Rows.Count() > 0 Then
                MsgBox("Item already exists.")
                Exit Sub
            End If
        Next

        dr("Item_id") = ds.Tables(0).Rows(0)("ITEM_ID").ToString
        dr("Item_Code") = ds.Tables(0).Rows(0)("ITEM_CODE").ToString
        dr("Item_Name") = ds.Tables(0).Rows(0)("ITEM_NAME").ToString
        dr("UM_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString
        dr("Cess_Id") = ds.Tables(0).Rows(0)("Cess_Id")
        dr("Cess_Name") = ds.Tables(0).Rows(0)("Cess_Name")
        dr("Cess_Per") = ds.Tables(0).Rows(0)("Cess_Per")
        'dr("UM_Id") = ds.Tables(0).Rows(0)("UM_ID").ToString
        ds = Obj.Fill_DataSet("DECLARE @rate NUMERIC (18,2);SELECT @rate=SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE FROM SUPPLIER_RATE_LIST INNER JOIN SUPPLIER_RATE_LIST_DETAIL ON SUPPLIER_RATE_LIST.SRL_ID = SUPPLIER_RATE_LIST_DETAIL.SRL_ID WHERE (SUPPLIER_RATE_LIST_DETAIL.ITEM_ID = " & Convert.ToInt32(item_id) & " ) AND (SUPPLIER_RATE_LIST.SUPP_ID = " & Convert.ToInt32(cmbSupplier.SelectedValue) & ") AND (SUPPLIER_RATE_LIST.ACTIVE = 1);SELECT ISNULL(@rate,0);SELECT     ITEM_DETAIL.PURCHASE_VAT_ID as PURCHASE_VAT_ID, VAT_MASTER.VAT_PERCENTAGE, VAT_MASTER.VAT_NAME FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID WHERE (ITEM_DETAIL.ITEM_ID = " & Convert.ToInt32(item_id) & " )")
        dr("Item_Rate") = ds.Tables(0).Rows(0)(0)
        dr("Vat_Id") = ds.Tables(1).Rows(0)("PURCHASE_VAT_ID")
        dr("Vat_Name") = ds.Tables(1).Rows(0)("VAT_NAME")
        dr("Req_Qty") = 0.0
        dr("Po_Qty") = 0.0
        dr("DType") = "P"
        dr("Disc") = 0.0
        'dr("Exice_Per") = 0
        dr("Vat_per") = ds.Tables(1).Rows(0)("VAT_PERCENTAGE")
        dr("Item_value") = (dr("PO_Qty") * dr("Item_Rate")) + ((dr("PO_Qty") * dr("Item_Rate") * dr("Vat_Per")) / 100)
        dr("Indent_Id") = -1

        frm_Indent_Items.dTable_POItems.Rows.Add(dr)

        'drItemCopy("Item_id") = ds.Tables(0).Rows(0)("ITEM_ID").ToString
        'drItemCopy("Item_Code") = ds.Tables(0).Rows(0)("ITEM_CODE").ToString
        'drItemCopy("Item_Name") = ds.Tables(0).Rows(0)("ITEM_NAME").ToString
        'drItemCopy("UM_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString
        'drItemCopy("Cess_Id") = ds.Tables(0).Rows(0)("Cess_Id")
        'drItemCopy("Cess_Name") = ds.Tables(0).Rows(0)("Cess_Name")
        'drItemCopy("Cess_Per") = ds.Tables(0).Rows(0)("Cess_Per")
        ''dr("UM_Id") = ds.Tables(0).Rows(0)("UM_ID").ToString
        'ds = Obj.Fill_DataSet("DECLARE @rate NUMERIC (18,2);SELECT @rate=SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE FROM SUPPLIER_RATE_LIST INNER JOIN SUPPLIER_RATE_LIST_DETAIL ON SUPPLIER_RATE_LIST.SRL_ID = SUPPLIER_RATE_LIST_DETAIL.SRL_ID WHERE (SUPPLIER_RATE_LIST_DETAIL.ITEM_ID = " & Convert.ToInt32(item_id) & " ) AND (SUPPLIER_RATE_LIST.SUPP_ID = " & Convert.ToInt32(cmbSupplier.SelectedValue) & ") AND (SUPPLIER_RATE_LIST.ACTIVE = 1);SELECT ISNULL(@rate,0);SELECT     ITEM_DETAIL.PURCHASE_VAT_ID as PURCHASE_VAT_ID, VAT_MASTER.VAT_PERCENTAGE, VAT_MASTER.VAT_NAME FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID WHERE (ITEM_DETAIL.ITEM_ID = " & Convert.ToInt32(item_id) & " )")
        'drItemCopy("Item_Rate") = ds.Tables(0).Rows(0)(0)
        'drItemCopy("Vat_Id") = ds.Tables(1).Rows(0)("PURCHASE_VAT_ID")
        'drItemCopy("Vat_Name") = ds.Tables(1).Rows(0)("VAT_NAME")
        'drItemCopy("Req_Qty") = 0.0
        'drItemCopy("Po_Qty") = 0.0
        'drItemCopy("DType") = "P"
        'dr("Disc") = 0.0
        ''dr("Exice_Per") = 0
        'drItemCopy("Vat_per") = ds.Tables(1).Rows(0)("VAT_PERCENTAGE")
        'drItemCopy("Item_value") = (drItemCopy("PO_Qty") * drItemCopy("Item_Rate")) + ((drItemCopy("PO_Qty") * drItemCopy("Item_Rate") * drItemCopy("Vat_Per")) / 100)
        'drItemCopy("Indent_Id") = -1

        'frm_Indent_Items.dTable_POItems_Copy.Rows.Add(drItemCopy)

        generate_tree()
        AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh


    End Sub

    Private Sub chk_VatCal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_VatCal.CheckedChanged
        CalculateAmount()
    End Sub

    Private Sub txtBarcodeSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcodeSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
            If Not String.IsNullOrEmpty(txtBarcodeSearch.Text) Then

                Dim qry As String = "SELECT  IM.ITEM_ID,BarCode_vch FROM    ITEM_MASTER AS IM INNER JOIN dbo.SUPPLIER_RATE_LIST_DETAIL AS SRLD ON SRLD.ITEM_ID = IM.ITEM_ID INNER JOIN dbo.SUPPLIER_RATE_LIST AS  srl ON srl.SRL_ID = SRLD.SRL_ID  WHERE srl.active = 1 AND srl.supp_id =" + cmbSupplier.SelectedValue.ToString() + " And   Barcode_vch = '" + txtBarcodeSearch.Text + "'"
                Dim id As Int32 = clsObj.ExecuteScalar(qry)

                If id > 0 Then


                    'If Not check_item_exist(id) Then
                    get_row(id)
                    'End If
                End If
                txtBarcodeSearch.Text = ""
                txtBarcodeSearch.Focus()
            End If
        End If
    End Sub

    Private Sub flxPOList_KeyDown(sender As Object, e As KeyEventArgs) Handles flxPOList.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                new_initialization()
                If flxPOList.Rows(flxPOList.CursorCell.r1)("PO_STATUS").ToString().ToUpper = POStatus.Fresh.ToString().ToUpper() Then
                    flag = "update"
                    _po_id = Convert.ToInt32(flxPOList.Rows(flxPOList.CursorCell.r1)("PO_ID"))
                    fill_PODetail(_po_id)
                Else
                    flag = "view"
                    MsgBox("You can't edit this PO." & vbCrLf & "Because this PO is approved/cleared/canceled.", MsgBoxStyle.Information, gblMessageHeading)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

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

        For iRow = 1 To flxItemList.Rows.Count - 1

            Dim totalAmount As Decimal = flxItemList.Item(iRow, "PO_QTY") * flxItemList.Item(iRow, "item_rate")

            If flxItemList.Item(iRow, "DType") = "P" Then
                totalAmount -= Math.Round((totalAmount * flxItemList.Item(iRow, "DISC") / 100), 2)
            Else
                totalAmount -= Math.Round(flxItemList.Item(iRow, "DISC"), 2)
            End If

            'If flxItemList.Item(iRow, "GPAID") = "Y" Then
            '    totalAmount -= (totalAmount - (totalAmount / (1 + (flxItemList.Item(iRow, "vat_per") / 100))))
            'End If

            Tax = totalAmount * flxItemList.Item(iRow, "vat_per") / 100

            GSTTaxTotal += Tax

            Select Case flxItemList.Item(iRow, "vat_per")
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
        If cmbPOType.SelectedValue = 0 Then
            lblGSTDetail.Text = String.Format("Total GST - {0}", TotalGst)
            lblGSTDetail.Tag = Math.Round(TotalGst, 2)
        ElseIf cmbPOType.SelectedValue = 3 Then
            lblGSTDetail.Text = String.Format("UTGST - {0}{1}CGST - {0}", Math.Round(PartialGst, 2), Environment.NewLine)
            lblGSTDetail.Tag = Math.Round(PartialGst, 2)
        ElseIf cmbPOType.SelectedValue = 1 Then
            lblGSTDetail.Text = String.Format("SGST - {0}{1}CGST - {0}", Math.Round(PartialGst, 2), Environment.NewLine)
            lblGSTDetail.Tag = Math.Round(PartialGst, 2)
        ElseIf cmbPOType.SelectedValue = 2 Then
            lblGSTDetail.Text = String.Format("IGST - {0}", Math.Round(TotalGst, 2))
            lblGSTDetail.Tag = Math.Round(TotalGst, 2)
        End If

    End Sub

    Private Sub chk_Composition_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Composition.CheckedChanged
        If chk_Composition.Checked = True Then

            Dim i As Integer
            For i = 1 To flxItemList.Rows.Count - 1

                AddVatPerId(flxItemList.Item(i, "Vat_Id"), (i - 1))
                AddVatPer(flxItemList.Item(i, "vat_per"), (i - 1))
                AddVatPerName(flxItemList.Item(i, "Vat_Name"), (i - 1))
                AddCessPerId(flxItemList.Item(i, "Cess_Id"), (i - 1))
                AddCessPer(flxItemList.Item(i, "Cess_Per"), (i - 1))
                AddCessPerName(flxItemList.Item(i, "Cess_Name"), (i - 1))
                'AddQty(flxItemList.Item(i, "PO_Qty"), (i - 1))
                'AddAmount(flxItemList.Item(i, "Item_Value"), (i - 1))
                'AddItemRate(flxItemList.Item(i, "Item_Rate"), (i - 1))

                flxItemList.Item(i, "vat_per") = 0
                flxItemList.Rows(i).Item("Cess_Per") = 0
                flxItemList.Item(i, "Vat_Id") = 0
                flxItemList.Item(i, "Vat_Name") = "0% GST"
                flxItemList.Item(i, "Cess_Id") = 0
                flxItemList.Item(i, "Cess_Name") = "0% CESS"
                'flxItemList.Item(i, "Item_Value") = 0.00
                'flxItemList.Item(i, "Item_Rate") = 0.00

            Next
        Else

            Dim i As Integer
            For i = 1 To flxItemList.Rows.Count - 1
                dtable_Item_List.Rows(i - 1)("Item_ID") = flxItemList.Item(i, "Item_ID")
                dtable_Item_List.Rows(i - 1)("Item_Code") = flxItemList.Item(i, "Item_Code")
                dtable_Item_List.Rows(i - 1)("Item_Name") = flxItemList.Item(i, "Item_Name")
                dtable_Item_List.Rows(i - 1)("UM_Name") = flxItemList.Item(i, "UM_Name")
                dtable_Item_List.Rows(i - 1)("Req_Qty") = flxItemList.Item(i, "Req_Qty")
                dtable_Item_List.Rows(i - 1)("PO_Qty") = flxItemList.Item(i, "PO_Qty")
                dtable_Item_List.Rows(i - 1)("DType") = flxItemList.Item(i, "DType")
                dtable_Item_List.Rows(i - 1)("DISC") = flxItemList.Item(i, "DISC")
                dtable_Item_List.Rows(i - 1)("Exice_Per") = flxItemList.Item(i, "Exice_Per")
                dtable_Item_List.Rows(i - 1)("Item_Value") = flxItemList.Item(i, "Item_Value")
                dtable_Item_List.Rows(i - 1)("Indent_ID") = flxItemList.Item(i, "Indent_ID")
                dtable_Item_List.Rows(i - 1)("UM_ID") = flxItemList.Item(i, "UM_ID")
                dtable_Item_List.Rows(i - 1)("Item_Value") = flxItemList.Item(i, "Item_Value")
                dtable_Item_List.Rows(i - 1)("Item_Rate") = flxItemList.Item(i, "Item_Rate")
                dtable_Item_List.Rows(i - 1)("PO_Qty") = flxItemList.Item(i, "PO_Qty")

                dtable_Item_List.Rows(i - 1)("Vat_Id") = vatperid(i - 1)
                dtable_Item_List.Rows(i - 1)("Vat_Name") = vatpername(i - 1)
                dtable_Item_List.Rows(i - 1)("Vat_Per") = vatper(i - 1)
                dtable_Item_List.Rows(i - 1)("Cess_Id") = cessperid(i - 1)
                dtable_Item_List.Rows(i - 1)("Cess_Name") = cesspername(i - 1)
                dtable_Item_List.Rows(i - 1)("Cess_Per") = cessper(i - 1)

                'If (flag = "save") Then
                '    dtable_Item_List.Rows(i - 1)("Vat_Id") = vatperid(i - 1)
                '    dtable_Item_List.Rows(i - 1)("Vat_Name") = vatpername(i - 1)
                '    dtable_Item_List.Rows(i - 1)("Vat_Per") = vatper(i - 1)
                '    dtable_Item_List.Rows(i - 1)("Cess_Id") = cessperid(i - 1)
                '    dtable_Item_List.Rows(i - 1)("Cess_Name") = cesspername(i - 1)
                '    dtable_Item_List.Rows(i - 1)("Cess_Per") = cessper(i - 1)
                'Else
                '    dtable_Item_List.Rows(i - 1)("Vat_Id") = dtable_Item_List.Rows(i - 1)("Vat_Id")
                '    dtable_Item_List.Rows(i - 1)("Vat_Name") = dtable_Item_List.Rows(i - 1)("Vat_Name")
                '    dtable_Item_List.Rows(i - 1)("Vat_Per") = dtable_Item_List.Rows(i - 1)("Vat_Per")
                '    dtable_Item_List.Rows(i - 1)("Cess_Id") = dtable_Item_List.Rows(i - 1)("Cess_Id")
                '    dtable_Item_List.Rows(i - 1)("Cess_Name") = dtable_Item_List.Rows(i - 1)("Cess_Name")
                '    dtable_Item_List.Rows(i - 1)("Cess_Per") = dtable_Item_List.Rows(i - 1)("Cess_Per")
                'End If
            Next

            dtable_Item_List.AcceptChanges()
            generate_tree()
            flxItemList.DataSource = dtable_Item_List
            format_grid()
        End If
        CalculateAmount()
    End Sub

    Public Sub AddVatPerId(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve vatperid(i)
        vatperid(i) = stringToAdd
    End Sub

    Public Sub AddVatPer(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve vatper(i)
        vatper(i) = stringToAdd
    End Sub

    Public Sub AddVatPerName(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve vatpername(i)
        vatpername(i) = stringToAdd
    End Sub

    Public Sub AddCessPerId(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve cessperid(i)
        cessperid(i) = stringToAdd
    End Sub

    Public Sub AddCessPer(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve cessper(i)
        cessper(i) = stringToAdd
    End Sub

    Public Sub AddCessPerName(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve cesspername(i)
        cesspername(i) = stringToAdd
    End Sub

    Public Sub AddQty(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve poqty(i)
        poqty(i) = stringToAdd
    End Sub

    Public Sub AddAmount(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve itemvalue(i)
        itemvalue(i) = stringToAdd
    End Sub

    Public Sub AddItemRate(ByVal stringToAdd As String, ByVal i As Integer)
        ReDim Preserve itemrate(i)
        itemrate(i) = stringToAdd
    End Sub


End Class
