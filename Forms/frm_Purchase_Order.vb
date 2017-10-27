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
                clsPropObj.PO_SUPP_ID = Convert.ToInt32(cmbSupplier.SelectedValue)
                clsPropObj.PO_QUALITY_ID = Convert.ToInt32(cmbQualityRate.SelectedValue)
                clsPropObj.PO_DELIVERY_ID = Convert.ToInt32(cmbDeliveryRate.SelectedValue)
                clsPropObj.PO_STATUS = Convert.ToInt32(POStatus.Fresh)
                clsPropObj.PATMENT_TERMS = txtPaymentTerms.Text.ToUpper()
                clsPropObj.TRANSPORT_MODE = dtpTransMode.Text
                clsPropObj.TOTAL_AMOUNT = Convert.ToDecimal(GrossTotalVat(0).ToString())
                clsPropObj.VAT_AMOUNT = Convert.ToDecimal(GrossTotalVat(1).ToString())
                clsPropObj.NET_AMOUNT = Convert.ToDecimal(GrossTotalVat(2).ToString())
                clsPropObj.EXICE_AMOUNT = Convert.ToDecimal(GrossTotalVat(3).ToString())
                clsPropObj.PO_TYPE = cmbPOType.SelectedValue
                clsPropObj.OCTROI = dtpOctroi.Text
                clsPropObj.PRICE_BASIS = dtpPriceBasis.Text
                clsPropObj.FRIEGHT = Convert.ToInt32(dtpFreight.SelectedValue)
                clsPropObj.OTHER_CHARGES = Convert.ToDecimal(txtOtherCharges.Text)
                clsPropObj.CESS = Convert.ToDecimal("0.0".ToString())
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

        For iRow = 1 To flxItemList.Rows.Count - 2
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
        txtOtherCharges.Text = "0.00"
        txtDiscountAmount.Text = "0.00"
        lblNetAmount.Text = "0.00"
        TabControl1.SelectTab(1)
        'cmbPOType.Focus()
        flag = "save"

    End Sub

    Private Sub frm_Purchase_Order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            clsObj.ComboBind(cmbPOType, "Select PO_TYPE_ID,PO_TYPE_NAME from PO_TYPE_MASTER", "PO_TYPE_NAME", "PO_TYPE_ID", True)
            clsObj.ComboBind(cmbSupplier, "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" & AccountGroups.Supplier & " Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
            clsObj.ComboBind(cmbQualityRate, "Select QR_ID,QR_CODE from QUALITY_RATING_MASTER", "QR_CODE", "QR_ID", True)
            clsObj.ComboBind(cmbDeliveryRate, "Select DR_ID,DR_CODE from DELIVERY_RATING_MASTER", "DR_CODE", "DR_ID", True)
            clsObj.ComboBind(cmbFilterSupp, "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" & AccountGroups.Supplier & " Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
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
        dtable_Item_List.Columns.Add("Item_Value", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Indent_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("UM_ID", GetType(System.Int32))

        flxItemList.DataSource = dtable_Item_List

        format_grid()


    End Sub
    Private Sub format_grid()


        flxItemList.Cols(0).Width = 10
        flxItemList.Cols("UM_ID").Visible = False
        flxItemList.Cols("Indent_ID").Visible = False
        flxItemList.Cols("Item_ID").Visible = False
        flxItemList.Cols("Vat_Id").Visible = False


        flxItemList.Cols("Item_Code").Caption = "Item Code"
        flxItemList.Cols("Item_Name").Caption = "Item Name"
        flxItemList.Cols("UM_Name").Caption = "UOM"
        flxItemList.Cols("Req_Qty").Caption = "Req.Qty"
        flxItemList.Cols("PO_Qty").Caption = "PO Qty"
        flxItemList.Cols("Item_Rate").Caption = "Item Rate"
        flxItemList.Cols("DType").Caption = "DType"
        flxItemList.Cols("DISC").Caption = "Disc"

        flxItemList.Cols("Vat_Name").Caption = "Gst Name"
        flxItemList.Cols("Vat_Per").Caption = "GST%"
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
        flxItemList.Cols("Item_Value").AllowEditing = False


        flxItemList.Cols("Item_Code").Width = 90
        flxItemList.Cols("Item_Name").Width = 320
        flxItemList.Cols("UM_Name").Width = 40
        flxItemList.Cols("Req_Qty").Width = 60
        flxItemList.Cols("PO_Qty").Width = 60
        flxItemList.Cols("Exice_Per").Width = 30
        flxItemList.Cols("Item_Rate").Width = 70

        flxItemList.Cols("DType").Width = 45
        flxItemList.Cols("DISC").Width = 45

        flxItemList.Cols("Vat_Name").Width = 60
        flxItemList.Cols("Vat_Per").Width = 20
        flxItemList.Cols("Item_Value").Width = 80

        flxItemList.Cols("Exice_Per").Visible = False
        flxItemList.Cols("Vat_Id").Visible = False
        flxItemList.Cols("Vat_Per").Visible = False

    End Sub

    Private Sub lnkSelectItems_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectItems.LinkClicked
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

            flxItemList.Tree.Style = TreeStyleFlags.CompleteLeaf
            flxItemList.Tree.Column = 1
            flxItemList.AllowMerging = AllowMergingEnum.None

            Dim totalOn As Integer = flxItemList.Cols("Req_Qty").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Sum, 0, 2, totalOn)
            totalOn = flxItemList.Cols("PO_Qty").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Sum, 0, 2, totalOn)
            totalOn = flxItemList.Cols("item_value").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Sum, 0, 2, totalOn)
            totalOn = flxItemList.Cols("item_rate").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Max, 0, 2, totalOn)
            totalOn = flxItemList.Cols("vat_per").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Max, 0, 2, totalOn)
            totalOn = flxItemList.Cols("vat_name").SafeIndex
            flxItemList.Subtotal(AggregateEnum.None, 0, 2, totalOn)
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


            Dim total_item_value As Double
            Dim item_value As Double
            Dim total_vat_amount As Double
            Dim total_exice_amount As Double
            Dim tot_amt As Double
            total_exice_amount = 0
            total_item_value = 0
            total_vat_amount = 0
            tot_amt = 0
            item_value = 0

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

            Dim exice_per As Double = 0.0
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

                        .Item("Item_Value") = Math.Round((.Item("PO_Qty") * .Item("Item_Rate")), 2) - discamt
                        total_item_value = total_item_value + (.Item("Item_Value"))

                        totdiscamt = totdiscamt + discamt

                        exice_per = IIf((.Item("Exice_Per")) Is DBNull.Value, 0, .Item("Exice_Per"))
                        exice_per = exice_per / 100
                        total_exice_amount = total_exice_amount + (.Item("Item_Value") * exice_per)
                        total_vat_amount = total_vat_amount + ((((.Item("PO_Qty") * .Item("Item_Rate")) - discamt) * .Item("Vat_Per")) / 100)
                    End If
                End With
            Next

            AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            txtDiscountAmount.Text = totdiscamt.ToString("#0.00")
            lblItemValue.Text = total_item_value.ToString("#0.00")
            lblVatAmount.Text = total_vat_amount.ToString("#0.00")
            lblNetAmount.Text = (total_item_value + total_vat_amount + total_exice_amount + txtOtherCharges.Text).ToString("#0.00") '- txtDiscountAmount.Text).ToString("#0.00")
            'Return lblItemValue.Text + "," + lblVatAmount.Text + "," + lblNetAmount.Text + "," + lblNetAmount.Text + "," + ExiceGross
            Str = total_item_value.ToString("#0.00") + "," + total_vat_amount.ToString("#0.00") + "," + lblNetAmount.Text + "," + total_exice_amount.ToString()
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


            If cmbFilterSupp.SelectedIndex <> -1 Then
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


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSupplier.SelectedIndexChanged

        Dim dt As DataTable
        Dim ds As DataSet
        Dim i As Integer
        Dim strSql As String

        If cmbSupplier.SelectedValue <> -1 Then
            strSql = "SELECT ACCOUNT_MASTER.ADDRESS_PRIM + ' - {'  + ISNULL(CITY_MASTER.CITY_NAME,'') + '}'"
            strSql = strSql & " FROM ACCOUNT_MASTER LEFT OUTER JOIN"
            strSql = strSql & " CITY_MASTER ON ACCOUNT_MASTER.CITY_ID = CITY_MASTER.CITY_ID"
            strSql = strSql & " WHERE ACCOUNT_MASTER.ACC_ID = " & cmbSupplier.SelectedValue
            lblAddress.Text = clsObj.Fill_DataSet(strSql).Tables(0).Rows(0)(0)
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
        If cmbSupplier.SelectedIndex > 0 Then
            Dim dr As DataRow
            Dim ds As DataSet

            frm_Show_search.qry = "SELECT IM.ITEM_ID,IM.ITEM_CODE," & _
                               " IM.ITEM_NAME,UM.UM_Name,CM.ITEM_CAT_NAME," & _
                               " IM.DIVISION_ID, 0.00 as Quantity " & _
                               " FROM ITEM_MASTER  AS IM INNER JOIN " & _
                               " ITEM_DETAIL AS ID ON IM.ITEM_ID = ID.ITEM_ID  INNER JOIN  UNIT_MASTER AS UM " & _
                               " ON IM.UM_ID = UM.UM_ID INNER JOIN ITEM_CATEGORY AS CM ON " & _
                               " IM.ITEM_CATEGORY_ID = CM.ITEM_CAT_ID" & _
                               " INNER JOIN dbo.SUPPLIER_RATE_LIST_DETAIL AS SRLD ON SRLD.ITEM_ID = IM.ITEM_ID" & _
                               " INNER JOIN dbo.SUPPLIER_RATE_LIST AS SRL ON SRL.SRL_ID=SRLD.SRL_ID" & _
                               " where srl.supp_id = " & cmbSupplier.SelectedValue & " AND srl.active = 1 "


            frm_Show_search.column_name = "Item_Name"
            frm_Indent_Items.dTable_POItems = flxItemList.DataSource
            'frm_Show_search.extra_condition = ""
            frm_Show_search.ret_column = "Item_ID"

            frm_Show_search.cols_no_for_width = "1,2,3"
            frm_Show_search.cols_width = "60,350,50"
            ' frm_Show_search.ret_column = "division_id"


            RemoveHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            frm_Show_search.ShowDialog()
            'MsgBox(frm_Show_search.search_result)

            If frm_Show_search.search_result = -1 Then
                Exit Sub
            End If

            dr = frm_Indent_Items.dTable_POItems.NewRow


            ds = Obj.Fill_DataSet("SELECT ITEM_ID, ITEM_CODE, ITEM_NAME, UM_Name, UNIT_MASTER.UM_ID  FROM item_master" & _
                                    " INNER JOIN dbo.UNIT_MASTER ON dbo.UNIT_MASTER .UM_ID =dbo.ITEM_MASTER .UM_ID" & _
                                    " WHERE ITEM_ID=" & Convert.ToInt32(frm_Show_search.search_result) & "")

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
            'dr("UM_Id") = ds.Tables(0).Rows(0)("UM_ID").ToString
            ds = Obj.Fill_DataSet("DECLARE @rate NUMERIC (18,2);SELECT @rate=SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE FROM SUPPLIER_RATE_LIST INNER JOIN SUPPLIER_RATE_LIST_DETAIL ON SUPPLIER_RATE_LIST.SRL_ID = SUPPLIER_RATE_LIST_DETAIL.SRL_ID WHERE (SUPPLIER_RATE_LIST_DETAIL.ITEM_ID = " & Convert.ToInt32(frm_Show_search.search_result) & " ) AND (SUPPLIER_RATE_LIST.SUPP_ID = " & Convert.ToInt32(cmbSupplier.SelectedValue) & ") AND (SUPPLIER_RATE_LIST.ACTIVE = 1);SELECT ISNULL(@rate,0);SELECT     ITEM_DETAIL.PURCHASE_VAT_ID as PURCHASE_VAT_ID, VAT_MASTER.VAT_PERCENTAGE, VAT_MASTER.VAT_NAME FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID WHERE (ITEM_DETAIL.ITEM_ID = " & Convert.ToInt32(frm_Show_search.search_result) & " )")
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
            generate_tree()
            AddHandler flxItemList.AfterDataRefresh, AddressOf flxItemList_AfterDataRefresh

            frm_Show_search.Close()
        Else
            erp.SetError(cmbSupplier, "Please select supplier first.")
        End If
    End Sub

    Private Sub chk_VatCal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_VatCal.CheckedChanged
        CalculateAmount()
    End Sub


End Class
