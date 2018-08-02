
Imports System.Data.SqlClient
Imports System.Data

Public Class frm_Open_PO_Master
    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New open_po_master.cls_open_po_master
    Dim prpty As New open_po_master.cls_open_po_master_prop
    Dim flag As String
    Dim PO_ID As Integer
    Dim qry As String
    Dim ds1 As DataSet
    Dim dt As DataTable
    Dim cmbItems As ComboBox
    'Dim int_ColumnIndex As Integer
    Dim txtItemQuanity As TextBox
    Dim int_GrdIndex As Integer
    Dim Int_RowIndex As Integer

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub
    Private Enum enmGrdItem
        ItemName = 0
        ItemRate = 2
        ItemQty = 3
        ExicePer = 5
        VatPer = 7
    End Enum

    Private Sub frm_Open_PO_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            flag = "save"
            clsObj.ComboBind(ddlSupplierSearch, "SELECT '--SELECT--' as Acc_Name,0 as Acc_id union all SELECT ACC_NAME, ACC_ID FROM ACCOUNT_MASTER where AG_ID=2 order by ACC_NAME", "ACC_NAME", "ACC_ID")
            ComboBind_Enum(cmdPOStatusSearch, New POStatus)
            'obj.FormatGrid(grdOpenPOList)
            Fill_Grid()
            '  obj.FormatGrid(grdOpenPoMaster)
            grdOpenPoMaster.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            clsObj.ComboBind(cmbPOType, "Select PO_TYPE_ID,PO_TYPE_NAME from PO_TYPE_MASTER", "PO_TYPE_NAME", "PO_TYPE_ID", True)
            clsObj.ComboBind(cmbSupplier, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" & AccountGroups.Sundry_Creditors, "ACC_NAME", "ACC_ID")
            clsObj.ComboBind(cmbQualityRate, "Select QR_ID,QR_CODE from QUALITY_RATING_MASTER", "QR_CODE", "QR_ID", True)
            clsObj.ComboBind(cmbDeliveryRate, "Select DR_ID,DR_CODE from DELIVERY_RATING_MASTER", "DR_CODE", "DR_ID", True)
            new_initialization()
            setCheckBoxState()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub setCheckBoxState()
        If chkSupplier.Checked = True Then
            ddlSupplierSearch.Enabled = True
        Else
            ddlSupplierSearch.Enabled = False
        End If
        If chkPONumber.Checked = True Then
            txtPONumberSearch.Enabled = True
        Else
            txtPONumberSearch.Enabled = False
        End If
        If chkPODate.Checked = True Then
            txtPODateSearch.Enabled = True
        Else
            txtPODateSearch.Enabled = False
        End If
        If chkStatus.Checked = True Then
            cmdPOStatusSearch.Enabled = True
        Else
            cmdPOStatusSearch.Enabled = False
        End If
    End Sub

    Private Sub ComboBind_Enum(ByVal cmb As ComboBox, ByVal enm As [Enum])
        Dim Names() As String = [Enum].GetNames(enm.GetType())
        Dim Values1 As Array = [Enum].GetValues(enm.GetType())
        '     Dim dr As DataRow
        'Dim mylist As New List(Of Array)

        cmb.Items.Clear()
        'Values1.Resize(
        'ReDim Preserve Values1(5, 2)
        cmb.DataSource = Values1
        '      Dim dt As DataTable = CType(cmb.DataSource, DataTable)
        '       cmb.Items.Add("AAAA")

        'Values1.Resize(Values1, Values1.Length + 1)
        'cmb.SelectedItem = "Select"
        'cmb.SelectedIndex = "-1"
        'cmb.SelectedIndex = 1
        'MsgBox(cmb.SelectedValue & "    -     " & cmb.Text)
    End Sub

    Private Function Validation() As Boolean
        Validation = True
        Dim blnRecExist As Boolean
        blnRecExist = False
        If dtpPODate.Value < now Then
            MsgBox("PO date can not be less than current date.", vbExclamation, gblMessageHeading)
            dtpPODate.Focus()
            Validation = False
            Exit Function
        End If
    End Function
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            new_initialization()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Public Sub new_initialization()
        grid_style()
        obj.Clear_All_TextBox(Me.GroupBox1.Controls)
        obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
        obj.Clear_All_TextBox(Me.GroupBox4.Controls)
        obj.Clear_All_ComoBox(Me.GroupBox4.Controls)
        dtpFreight.Text = 0
        flag = "save"
        TabControl1.SelectTab(1)
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Dim cmd As SqlCommand = Nothing

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

        If Not IsNumeric(txtCess.Text) Then
            txtCess.Text = 0
        End If

        If txtOtherCharges.Text.Length <= 0 Then
            erp.SetError(txtOtherCharges, "Please Select Other Charges first.")
            Exit Sub
        Else
            erp.Clear()
        End If

        If Validation() = False Then
            Exit Sub
        End If

        Try
            If grdOpenPoMaster.Rows.Count >= 1 And Not grdOpenPoMaster.Rows(0).Cells(0).Value = Trim("") Then
                Dim stringArr() As String = New String() {"Item_Name", "Item_Qty", "Item_Rate"}
                If obj.ValidatingDGV(grdOpenPoMaster, stringArr) = False Then
                    MsgBox("Please Enter All The Records In Datagrid", vbExclamation, gblMessageHeading)
                    Exit Sub
                End If

                cmd = obj.MyCon_BeginTransaction
                'Try
                If flag = "save" Then

                    'Dim ds As New DataSet()
                    ds1 = clsObj.fill_Data_set("GET_PO_NO", "@DIV_ID", v_the_current_division_id)

                    If ds1.Tables(0).Rows.Count = 0 Then
                        lblMsg.Text = "Purchase Order series does not exists"
                        ds1.Dispose()
                        'updpnlMain.Update()
                        Return
                    Else
                        If ds1.Tables(0).Rows(0)(0).ToString() = "-1" Then
                            lblMsg.Text = "Purchase Order series does not exists"
                            ds1.Dispose()
                            'updpnlMain.Update()
                            Return
                        ElseIf ds1.Tables(0).Rows(0)(0).ToString() = "-2" Then
                            lblMsg.Text = "Purchase Order series has been completed"
                            ds1.Dispose()
                            'updpnlMain.Update()
                            Return
                        Else
                            prpty.PO_CODE = ds1.Tables(0).Rows(0)(0).ToString()
                            prpty.PO_NO = Convert.ToDecimal(ds1.Tables(0).Rows(0)(1).ToString()) + 1
                            ds1.Dispose()
                        End If
                    End If
                    Dim PID As Integer
                    PID = Convert.ToInt32(obj.getMaxValue("PO_ID", "OPEN_PO_MASTER"))
                    prpty.PO_ID = Convert.ToInt32(PID)
                    prpty.PO_CODE = GetPOCode()
                    prpty.PO_NO = Convert.ToInt32(PID)
                    prpty.PO_TYPE = cmbPOType.SelectedValue
                    prpty.PO_DATE = Convert.ToDateTime(dtpPODate.Text).ToString()
                    prpty.PO_START_DATE = Convert.ToDateTime(dtpStartDate.Text).ToString()
                    prpty.PO_END_DATE = Convert.ToDateTime(dtpEndDate.Text).ToString()
                    prpty.PO_REMARKS = txtPORemarks.Text
                    'prpty.PO_SUPP_ID = Convert.ToInt32(cmbSupplier.SelectedValue)
                    prpty.PO_SUPP_ID = Convert.ToInt32(cmbSupplier.SelectedValue).ToString()
                    prpty.PO_QUALITY_ID = Convert.ToDouble(cmbQualityRate.SelectedValue)
                    prpty.PO_DELIVERY_ID = Convert.ToDouble(cmbDeliveryRate.SelectedValue)
                    prpty.PO_STATUS = Convert.ToInt32(GlobalModule.POStatus.Fresh)
                    prpty.PATMENT_TERMS = txtPaymentTerms.Text
                    prpty.TRANSPORT_MODE = dtpTransMode.Text
                    'prpty.TOTAL_AMOUNT = IIf(txtTotAmt.Text = "", 0, Convert.ToDouble(txtTotAmt.Text))
                    prpty.TOTAL_AMOUNT = IIf(lblItemValue.Text = "", 0, Convert.ToDouble(lblItemValue.Text))
                    prpty.VAT_AMOUNT = Convert.ToDouble(lblVatCst.Text).ToString()
                    'prpty.NET_AMOUNT = Convert.ToDouble(txtNetAmt.Text).ToString()
                    prpty.NET_AMOUNT = Convert.ToDouble(lblNetAmount.Text).ToString()
                    'prpty.EXICE_AMOUNT = Convert.ToDouble(txtExciseAmt.Text).ToString()
                    prpty.EXICE_AMOUNT = Convert.ToDouble(lblExiceAmt.Text).ToString()
                    prpty.OCTROI = Convert.ToInt32(dtpOctroi.SelectedValue)
                    prpty.PRICE_BASIS = Convert.ToInt32(dtpPriceBasis.SelectedValue)
                    prpty.FRIEGHT = Convert.ToInt32(dtpFreight.SelectedValue)
                    prpty.OTHER_CHARGES = Val(obj.NZ(Convert.ToDouble(txtOtherCharges.Text).ToString()))
                    prpty.CESS_PER = Convert.ToDouble(txtCess.Text).ToString()
                    prpty.ALREADY_RECVD = False
                    prpty.CREATED_BY = v_the_current_logged_in_user_name
                    prpty.CREATION_DATE = now
                    prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                    prpty.MODIFIED_DATE = now
                    prpty.DIVISION_ID = v_the_current_division_id
                    clsObj.insert_OPEN_PO_MASTERTrans(prpty, cmd)

                    Dim iRowCount As Int32
                    Dim iRow As Int32
                    iRowCount = grdOpenPoMaster.RowCount - 1
                    For iRow = 0 To iRowCount - 1
                        If grdOpenPoMaster.Item(9, iRow).Value() IsNot Nothing Then
                            If grdOpenPoMaster.Item(9, iRow).Value() > 0 Then
                                prpty.PO_ID = Convert.ToInt32(PID)
                                prpty.ITEM_NAME = grdOpenPoMaster.Item(0, iRow).Value
                                prpty.UOM = grdOpenPoMaster.Item(1, iRow).Value
                                prpty.ITEM_QTY = Convert.ToDouble(grdOpenPoMaster.Item("Item_Qty", iRow).Value)
                                prpty.ITEM_RATE = Convert.ToDouble(grdOpenPoMaster.Item("Item_Rate", iRow).Value)
                                prpty.EXICE_PER = Convert.ToDouble(grdOpenPoMaster.Item("Exice_Per", iRow).Value)
                                prpty.VAT_PER = Convert.ToDouble(grdOpenPoMaster.Item("Vat_Per", iRow).Value)
                                prpty.AMOUNT = Convert.ToDouble(grdOpenPoMaster.Item("Net_Amount", iRow).Value)
                                prpty.TOTAL_AMOUNT = Convert.ToDouble(grdOpenPoMaster.Item("Total_Amount", iRow).Value)
                                prpty.CREATED_BY = v_the_current_logged_in_user_name
                                prpty.CREATION_DATE = now
                                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                                prpty.MODIFIED_DATE = now
                                prpty.DIVISION_ID = v_the_current_division_id
                                clsObj.insert_OPEN_PO_DETAILTrans(prpty, cmd)
                            End If
                        End If
                    Next iRow
                    'MsgBox("Record Saved", MsgBoxStyle.Information, gblMessageHeading)

                    grid_style()
                    obj.Clear_All_TextBox(Me.GroupBox1.Controls)
                    obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
                    obj.Clear_All_TextBox(Me.GroupBox4.Controls)
                    obj.Clear_All_ComoBox(Me.GroupBox4.Controls)
                End If


                qry = "SELECT * FROM OPEN_PO_MASTER WHERE PO_ID = " & PO_ID
                ds1 = obj.FillDataSet(qry)
                dt = ds1.Tables(0)
                If ds1.Tables(0).Rows.Count > 0 Then

                    prpty.PO_ID = PO_ID
                    prpty.PO_CODE = Convert.ToString(txtPONo.Text).ToString
                    prpty.PO_NO = PO_ID
                    prpty.PO_TYPE = cmbPOType.SelectedValue
                    prpty.PO_DATE = Convert.ToDateTime(dtpPODate.Text).ToString()
                    prpty.PO_START_DATE = Convert.ToDateTime(dtpStartDate.Text).ToString()
                    prpty.PO_END_DATE = Convert.ToDateTime(dtpEndDate.Text).ToString()
                    prpty.PO_REMARKS = txtPORemarks.Text
                    prpty.PO_SUPP_ID = Convert.ToInt32(cmbSupplier.SelectedValue)
                    prpty.PO_QUALITY_ID = cmbQualityRate.SelectedValue
                    prpty.PO_DELIVERY_ID = cmbDeliveryRate.SelectedValue
                    prpty.PO_STATUS = Convert.ToInt32(GlobalModule.POStatus.Fresh)
                    prpty.PATMENT_TERMS = txtPaymentTerms.Text
                    prpty.TRANSPORT_MODE = Convert.ToString(dtpTransMode.SelectedValue)
                    'prpty.TOTAL_AMOUNT = Convert.ToDecimal(txtTotAmt.Text).ToString()
                    prpty.TOTAL_AMOUNT = Convert.ToDecimal(lblNetAmount.Text).ToString()
                    prpty.VAT_AMOUNT = Convert.ToDecimal(lblVatCst.Text).ToString()
                    'prpty.NET_AMOUNT = Convert.ToDecimal(txtNetAmt.Text).ToString()
                    prpty.NET_AMOUNT = Convert.ToDecimal(lblItemValue.Text).ToString()
                    'prpty.EXICE_AMOUNT = Convert.ToDecimal(txtExciseAmt.Text).ToString()
                    prpty.EXICE_AMOUNT = Convert.ToDecimal(lblExiceAmt.Text).ToString()
                    prpty.OCTROI = Convert.ToInt32(dtpOctroi.SelectedValue).ToString()
                    prpty.PRICE_BASIS = Convert.ToInt32(dtpPriceBasis.SelectedValue).ToString()
                    prpty.FRIEGHT = Convert.ToInt32(dtpFreight.SelectedValue).ToString()
                    prpty.OTHER_CHARGES = Convert.ToDecimal(txtOtherCharges.Text).ToString()
                    prpty.CESS_PER = Convert.ToDecimal(txtCess.Text).ToString()
                    prpty.ALREADY_RECVD = False
                    prpty.CREATED_BY = v_the_current_logged_in_user_name
                    prpty.CREATION_DATE = now
                    prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                    prpty.MODIFIED_DATE = now
                    prpty.DIVISION_ID = v_the_current_division_id

                    clsObj.update_OPEN_PO_MASTERTrans(prpty, cmd)

                    prpty.PO_ID = Convert.ToInt32(PO_ID)
                    prpty.ITEM_NAME = -1
                    prpty.UOM = -1
                    prpty.ITEM_QTY = -1
                    prpty.ITEM_RATE = -1
                    prpty.EXICE_PER = -1
                    prpty.VAT_PER = -1
                    prpty.TOTAL_AMOUNT = -1
                    prpty.CREATED_BY = -1
                    prpty.CREATION_DATE = now
                    prpty.MODIFIED_BY = -1
                    prpty.MODIFIED_DATE = now
                    prpty.DIVISION_ID = -1
                    clsObj.delete_OPEN_PO_DETAIL(prpty, cmd)

                    'grid_style()
                    'grdOpenPoMaster.Rows(rowindex).Cells(1).Tag = dtOpenPODetail.Rows(iRow)("UM_Id")


                    Dim iRowCount As Int32
                    Dim iRow As Int32
                    iRowCount = grdOpenPoMaster.RowCount
                    For iRow = 0 To iRowCount - 1
                        If grdOpenPoMaster.Item(9, iRow).Value() IsNot Nothing Then
                            If Convert.ToInt32(grdOpenPoMaster.Item(9, iRow).Value()) > 0 Then
                                prpty.PO_ID = PO_ID
                                prpty.ITEM_NAME = grdOpenPoMaster.Item(0, iRow).Value
                                prpty.UOM = grdOpenPoMaster.Item(1, iRow).Value
                                prpty.ITEM_QTY = Convert.ToDouble(grdOpenPoMaster.Item("Item_Qty", iRow).Value)
                                prpty.ITEM_RATE = Convert.ToDouble(grdOpenPoMaster.Item("Item_Rate", iRow).Value)
                                prpty.EXICE_PER = Convert.ToDouble(grdOpenPoMaster.Item("Exice_Per", iRow).Value)
                                prpty.VAT_PER = Convert.ToDouble(grdOpenPoMaster.Item("Vat_Per", iRow).Value)
                                prpty.AMOUNT = Convert.ToDouble(grdOpenPoMaster.Item("Net_Amount", iRow).Value)
                                prpty.TOTAL_AMOUNT = Convert.ToDouble(grdOpenPoMaster.Item("Total_Amount", iRow).Value)
                                prpty.CREATED_BY = v_the_current_logged_in_user_name
                                prpty.CREATION_DATE = now
                                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                                prpty.MODIFIED_DATE = now
                                prpty.DIVISION_ID = v_the_current_division_id
                                clsObj.insert_OPEN_PO_DETAILTrans(prpty, cmd)
                            End If
                        End If
                    Next iRow

                    'MsgBox("Record Updated", MsgBoxStyle.Information, gblMessageHeading)

                End If
                grid_style()
                obj.Clear_All_TextBox(Me.GroupBox1.Controls)
                obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
                obj.Clear_All_TextBox(Me.GroupBox4.Controls)
                obj.Clear_All_ComoBox(Me.GroupBox4.Controls)
            End If
            obj.MyCon_CommitTransaction(cmd)
            Fill_Grid()
            '''''''''''''print report''''''''''''''''''''''''''''''''''''
            If flag = "save" Then
                If MsgBox("Record Saved" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(prpty.PO_ID), CStr(enmDataType.D_int))
                End If
            ElseIf Not flag = "save" Then
                If MsgBox("Record Updated" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(prpty.PO_ID), CStr(enmDataType.D_int))
                End If
            End If
            '''''''''''''Print Report'''''''''''''''''''''''''''''''''''''
            TabControl1.SelectTab(1)

        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(grdOpenPOList("po_id", grdOpenPOList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    '    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
                    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(grdOpenPOList("po_id", grdOpenPOList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grid_style()

        Dim txbCol As DataGridViewTextBoxColumn
        Dim cmbCol As New DataGridViewComboBoxColumn

        grdOpenPoMaster.Columns.Clear()

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Name"
            .Name = "Item_Name"
            .ReadOnly = False
            .Visible = True
            .Width = 280
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        cmbCol = New DataGridViewComboBoxColumn
        With cmbCol
            .HeaderText = "UOM"
            .Name = "UOM"
            .Tag = "UM_Id"
            .ReadOnly = False
            .Visible = True
            .Width = 80
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        cmbCol.DataSource = clsObj.Fill_DataSet("Select * from unit_master").Tables(0)
        cmbCol.DisplayMember = "UM_NAME"
        cmbCol.ValueMember = "UM_Id"
        grdOpenPoMaster.Columns.Add(cmbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Qty"
            .Name = "Item_Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 75
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Rate"
            .Name = "Item_Rate"
            .ReadOnly = False
            .Visible = True
            .Width = 80
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Net Amount"
            .Name = "Net_Amount"
            .ReadOnly = True
            .Visible = True
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Exice %"
            .Name = "Exice_Per"
            .ReadOnly = False
            .Visible = True
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Excise"
            .Name = "Exice_Amount"
            .ReadOnly = False
            .Visible = False
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Vat %"
            .Name = "Vat_Per"
            .ReadOnly = False
            .Visible = True
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Total Vat"
            .Name = "Vat_Amount"
            .ReadOnly = False
            .Visible = False
            .Width = 75
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Total Amt"
            .Name = "Total_Amount"
            .ReadOnly = True
            .Visible = True
            .Width = 80
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        grdOpenPoMaster.Columns.Add(txbCol)
        'grdOpenPoMaster.Rows(0).Cells("Total_Amount").Value = 0
    End Sub

    Private Sub grdOpenPoMaster_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOpenPoMaster.CellEndEdit
        Dim mul As Decimal = 0
        mul = Convert.ToDouble(Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Item_Qty").Value) * (Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Item_Rate").Value)))
        grdOpenPoMaster.Rows(e.RowIndex).Cells("Net_Amount").Value = mul

        Dim exec As Decimal = 0
        exec = (Convert.ToDouble(Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Net_Amount").Value) * (Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Exice_Per").Value)))) / 100
        grdOpenPoMaster.Rows(e.RowIndex).Cells("Exice_Amount").Value = exec

        Dim vat As Decimal = 0
        vat = ((Convert.ToDouble(Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Net_Amount").Value) + (Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Exice_Amount").Value)))) * grdOpenPoMaster.Rows(e.RowIndex).Cells("Vat_Per").Value) / 100
        grdOpenPoMaster.Rows(e.RowIndex).Cells("Vat_Amount").Value = vat

        Dim tot_amt As Decimal = 0
        tot_amt = Convert.ToDouble(Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Net_Amount").Value) + (Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Exice_Amount").Value) + (Convert.ToDouble(grdOpenPoMaster.Rows(e.RowIndex).Cells("Vat_Amount").Value))))
        grdOpenPoMaster.Rows(e.RowIndex).Cells("Total_Amount").Value = tot_amt
    End Sub

    Dim Total As Integer
    Dim NetAmt As Integer
    Dim excise As Integer
    Dim vat As Integer

    Private Sub grdOpenPoMaster_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOpenPoMaster.RowLeave
        GetTotalAmount()
        txtCess.ReadOnly = False
        txtOtherCharges.ReadOnly = False
    End Sub

    Private Sub GetTotalAmount()
        Total = 0
        NetAmt = 0
        excise = 0
        vat = 0
        Dim GrdRowIndex As Integer
        For GrdRowIndex = 0 To grdOpenPoMaster.Rows.Count - 1
            Total = Total + Convert.ToDouble(grdOpenPoMaster.Rows(GrdRowIndex).Cells("Total_Amount").Value)
            NetAmt = NetAmt + Convert.ToDouble(grdOpenPoMaster.Rows(GrdRowIndex).Cells("Net_Amount").Value)
            excise = excise + Convert.ToDouble(grdOpenPoMaster.Rows(GrdRowIndex).Cells("Exice_Amount").Value)
            vat = vat + Convert.ToDouble(grdOpenPoMaster.Rows(GrdRowIndex).Cells("Vat_Amount").Value)
        Next GrdRowIndex
        lblItemValue.Text = NetAmt
        lblNetAmount.Text = Total
        'lblItemValue.Text = NetAmt
        'lblNetAmount.Text = Total
        lblExiceAmt.Text = excise
        lblVatCst.Text = vat
    End Sub

    Private Sub txtCess_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCess.TextChanged, TextBox5.TextChanged
        'Dim cess As Double
        'cess = Val(txtCess.Text) * Val(Total) / 100
        'txtTotAmt.Text = cess
    End Sub

    Private Sub txtOtherCharges_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOtherCharges.TextChanged, txtDiscountAmount.TextChanged, TextBox1.TextChanged, TextBox6.TextChanged
        If txtOtherCharges.Text <> "" Then
            Dim TotAmt As Double
            'TotAmt = Val(txtOtherCharges.Text) + Val(Total)
            TotAmt = Val(txtOtherCharges.Text) + Val(Total)
            lblNetAmount.Text = TotAmt
        Else
            txtOtherCharges.Text = 0
        End If
    End Sub

    Public Function GetPOCode() As String
        Dim Pre As String
        Dim CCID As String
        Dim POCode As String
        Pre = obj.getPrefixCode("PREFIX", "PO_SERIES")
        CCID = obj.getMaxValue("PO_ID", "OPEN_PO_MASTER")
        POCode = Pre & "" & CCID
        Return POCode
    End Function

    Private Sub FillPOInfo()
        txtPONo.Text = GetPOCode()
        'flag = "save"
    End Sub

    Public Sub Fill_Grid()
        obj.Grid_Bind(grdOpenPOList, "GET_OPENPO_LIST_DETAIL")
        grdOpenPOList.Columns(0).Visible = False
        grdOpenPOList.Columns(0).HeaderText = "PO ID"
        grdOpenPOList.Columns(1).HeaderText = "PO code"
        grdOpenPOList.Columns(1).Width = 100
        grdOpenPOList.Columns(2).HeaderText = "PO DATE"
        grdOpenPOList.Columns(3).HeaderText = "START DATE"
        grdOpenPOList.Columns(3).Width = 100
        grdOpenPOList.Columns(4).HeaderText = "END DATE"
        grdOpenPOList.Columns(4).Width = 100
        grdOpenPOList.Columns(5).HeaderText = "OPEN PO STATUS"
        grdOpenPOList.Columns(5).Width = 150
        grdOpenPOList.Columns(6).HeaderText = "ACC ID"
        grdOpenPOList.Columns(6).Visible = False
        grdOpenPOList.Columns(7).HeaderText = "SUPPLIER NAME"
        grdOpenPOList.Columns(7).Width = 300
    End Sub



    Private Sub getOpenPODetail(ByVal PO_ID As Integer)

        If Convert.ToString(grdOpenPOList.SelectedRows.Item(0).Cells(5).Value).ToUpper <> "FRESH" Then
            MsgBox("Open PO with 'FRESH' status can modify only.", vbExclamation, gblMessageHeading)
            grid_style()
            Exit Sub
        End If
        Dim dtOpenPO As New DataTable
        Dim dtOpenPODetail As New DataTable
        flag = "update"
        ds1 = obj.fill_Data_set("GET_OPEN_PO_DETAIL", "@V_PO_ID", PO_ID)
        If ds1.Tables.Count > 0 Then
            If dtOpenPO.Rows.Count > 0 Then dtOpenPO.Rows.Clear()
            dtOpenPO = ds1.Tables(0)
            txtPONo.Text = Convert.ToString(dtOpenPO.Rows(0)("PO_CODE"))
            cmbPOType.SelectedValue = dtOpenPO.Rows(0)("PO_TYPE")
            cmbSupplier.SelectedValue = Convert.ToString(dtOpenPO.Rows(0)("PO_SUPP_ID"))
            dtpPODate.Text = Convert.ToString(dtOpenPO.Rows(0)("PO_DATE"))
            dtpStartDate.Text = Convert.ToDateTime(dtOpenPO.Rows(0)("PO_START_DATE"))
            dtpEndDate.Text = Convert.ToString(dtOpenPO.Rows(0)("PO_END_DATE"))
            cmbQualityRate.SelectedValue = Convert.ToString(dtOpenPO.Rows(0)("PO_QUALITY_ID"))
            cmbDeliveryRate.SelectedValue = Convert.ToString(dtOpenPO.Rows(0)("PO_DELIVERY_ID"))
            dtpOctroi.SelectedValue = Convert.ToInt32(dtOpenPO.Rows(0)("OCTROI"))
            dtpFreight.SelectedValue = Convert.ToInt32(dtOpenPO.Rows(0)("FRIEGHT"))
            dtpTransMode.SelectedValue = Convert.ToString(dtOpenPO.Rows(0)("TRANSPORT_MODE"))
            dtpPriceBasis.SelectedValue = Convert.ToInt32(dtOpenPO.Rows(0)("PRICE_BASIS"))
            lblVatCst.Text = Convert.ToString(dtOpenPO.Rows(0)("VAT_AMOUNT"))
            'txtExciseAmt.Text = Convert.ToString(dtOpenPO.Rows(0)("EXICE_AMOUNT"))
            lblExiceAmt.Text = Convert.ToString(dtOpenPO.Rows(0)("EXICE_AMOUNT"))
            txtCess.Text = Convert.ToString(dtOpenPO.Rows(0)("CESS_PER"))
            txtOtherCharges.Text = Convert.ToString(dtOpenPO.Rows(0)("OTHER_CHARGES"))
            'txtNetAmt.Text = Convert.ToString(dtOpenPO.Rows(0)("NET_AMOUNT"))

            'lblNetAmount.Text = Convert.ToString(dtOpenPO.Rows(0)("NET_AMOUNT"))
            'lblItemValue.Text = Convert.ToString(dtOpenPO.Rows(0)("TOTAL_AMOUNT"))

            lblItemValue.Text = Convert.ToString(dtOpenPO.Rows(0)("TOTAL_AMOUNT"))
            lblNetAmount.Text = dtOpenPO.Rows(0)("NET_AMOUNT")

            txtPORemarks.Text = Convert.ToString(dtOpenPO.Rows(0)("PO_REMARKS"))
            txtPaymentTerms.Text = Convert.ToString(dtOpenPO.Rows(0)("PATMENT_TERMS"))


            'dt = ds1.Tables(1)
            ''If grdOpenPoMaster.ColumnCount > 0 Then grdOpenPoMaster.Columns.Clear()
            ''grdOpenPoMaster.DataSource = Nothing
            'grdOpenPoMaster.AutoGenerateColumns = False '.Columns(0).Visible = False

            'If dt.Rows.Count > 0 Then
            '    grdOpenPoMaster.DataSource = dt
            'End If
            ''Dim i As Integer = 0
            ''Dim cmb As DataGridViewComboBoxColumn = TryCast(Me.grdOpenPoMaster.Columns("UOM"), DataGridViewComboBoxColumn)



            If grdOpenPoMaster.Rows.Count > 0 Then grdOpenPoMaster.Rows.Clear()
            'grid_style()
            If dtOpenPODetail.Rows.Count > 0 Then dtOpenPODetail.Rows.Clear()
            dtOpenPODetail = ds1.Tables(1)
            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = dtOpenPODetail.Rows.Count
            For iRow = 0 To iRowCount - 1
                If dtOpenPODetail.Rows.Count > 0 Then
                    Dim rowindex As Integer = grdOpenPoMaster.Rows.Add()
                    grdOpenPoMaster.Rows(rowindex).Cells(0).Value = Convert.ToString(dtOpenPODetail.Rows(iRow)("Item_Name"))
                    grdOpenPoMaster.Rows(rowindex).Cells(1).Value = dtOpenPODetail.Rows(iRow)("UM_ID")
                    grdOpenPoMaster.Rows(rowindex).Cells(2).Value = Convert.ToInt32(dtOpenPODetail.Rows(iRow)("ITEM_QTY"))
                    grdOpenPoMaster.Rows(rowindex).Cells(3).Value = Convert.ToInt32(dtOpenPODetail.Rows(iRow)("ITEM_RATE"))
                    grdOpenPoMaster.Rows(rowindex).Cells(4).Value = Convert.ToInt32(dtOpenPODetail.Rows(iRow)("AMOUNT"))
                    grdOpenPoMaster.Rows(rowindex).Cells(5).Value = Convert.ToInt32(dtOpenPODetail.Rows(iRow)("EXICE_PER"))
                    grdOpenPoMaster.Rows(rowindex).Cells(7).Value = Convert.ToInt32(dtOpenPODetail.Rows(iRow)("VAT_PER"))
                    grdOpenPoMaster.Rows(rowindex).Cells(9).Value = Convert.ToInt32(dtOpenPODetail.Rows(iRow)("TOTAL_AMOUNT"))
                End If
            Next iRow
            ds1.Dispose()
            TabControl1.SelectTab(1)
        End If
    End Sub

    Private Sub grdOpenPoMaster_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdOpenPoMaster.DataError
        e.ThrowException = False
    End Sub

    Private Sub grdOpenPoMaster_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOpenPoMaster.CellEnter
        int_GrdIndex = e.ColumnIndex
    End Sub

    Private Sub txttxtNumericValid(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCess.KeyPress, txtOtherCharges.KeyPress, TextBox6.KeyPress, TextBox5.KeyPress
        If obj.Valid_Number(Asc(e.KeyChar), sender) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdOpenPoMaster_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdOpenPoMaster.EditingControlShowing
        Try
            RemoveHandler e.Control.KeyPress, AddressOf obj.Valid_NumberGrid
            RemoveHandler e.Control.KeyPress, AddressOf TXTVALUES
            If int_GrdIndex = enmGrdItem.ItemRate Or int_GrdIndex = enmGrdItem.ItemQty Or int_GrdIndex = enmGrdItem.ExicePer Or int_GrdIndex = enmGrdItem.VatPer Then
                AddHandler e.Control.KeyPress, AddressOf obj.Valid_NumberGrid
            Else
                int_GrdIndex = enmGrdItem.ItemName
                AddHandler e.Control.KeyPress, AddressOf TXTVALUES
            End If
        Catch ex As Exception

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Private Sub TXTVALUES(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub

    Private Sub grdOpenPoMaster_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOpenPoMaster.CellLeave

    End Sub

    Private Sub grdOpenPoMaster_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdOpenPoMaster.CellValidating
        'e.Cancel = False

        'If String.IsNullOrEmpty(Convert.ToString(e.FormattedValue)) Then
        '    MsgBox("Enter Value")
        'End If
    End Sub

    Private Sub Handle_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOpenPoMaster.CurrentCellChanged

    End Sub

    'Private Sub grdOpenPOList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOpenPOList.CellDoubleClick

    '    If e.RowIndex <> -1 Then
    '        PO_ID = grdOpenPOList.Rows(e.RowIndex).Cells("PO_ID").Value
    '        getOpenPODetail(PO_ID)
    '        TabControl1.SelectTab(1)
    '    Else
    '        PO_ID = 0
    '    End If
    'End Sub

    Private Sub chkSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSupplier.CheckedChanged
        If chkSupplier.Checked = True Then
            ddlSupplierSearch.Enabled = True
        Else
            ddlSupplierSearch.Enabled = False
            Fill_Grid()
            ddlSupplierSearch.SelectedValue = 0
        End If
    End Sub

    Private Sub chkPONumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPONumber.CheckedChanged
        If chkPONumber.Checked = True Then
            txtPONumberSearch.Enabled = True
        Else
            txtPONumberSearch.Enabled = False
            Fill_Grid()
            txtPONumberSearch.Text = ""
        End If
    End Sub

    Private Sub chkPODate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPODate.CheckedChanged
        If chkPODate.Checked = True Then
            txtPODateSearch.Enabled = True
        Else
            txtPODateSearch.Enabled = False
            Fill_Grid()
        End If
    End Sub

    Private Sub chkStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStatus.CheckedChanged
        If chkStatus.Checked = True Then
            cmdPOStatusSearch.Enabled = True
        Else
            cmdPOStatusSearch.Enabled = False
            Fill_Grid()
        End If
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        fill_PO_List()
    End Sub

    Private Sub fill_PO_List()
        Try
            Dim param As String, val As String
            param = ""
            val = ""

            param = "@v_supp_id,@v_po_code,@v_po_date_from,@v_po_date_to,@v_po_status,@v_div_id"

            If chkSupplier.Checked Then
                If ddlSupplierSearch.SelectedIndex <> 0 Then
                    val += Convert.ToString(ddlSupplierSearch.SelectedValue)
                Else
                    val += "-1"
                End If
            Else
                val += "-1"
            End If

            If chkPONumber.Checked Then
                If txtPONumberSearch.Text.Trim() <> "" Then
                    val += "," & txtPONumberSearch.Text
                Else
                    val += ",-1"
                End If
            Else
                val += ",-1"
            End If

            If chkPODate.Checked Then
                If txtPODateSearch.Text.Trim() <> "" Then
                    val += "," & txtPODateSearch.Text + "," & txtPODateSearch.Text
                Else
                    val += ","
                End If
            Else
                val += ",,"
            End If

            If chkStatus.Checked Then
                If cmdPOStatusSearch.SelectedIndex <> -1 Then
                    val += "," & cmdPOStatusSearch.SelectedValue
                Else
                    val += ",-1"
                End If
            Else
                val += ",-1"
            End If

            val += "," & v_the_current_division_id

            Dim ds As New DataSet()
            ds = clsObj.fill_Data_set("GET_FILTER_OPEN_PO_LIST", param, val)
            If ds.Tables.Count > 0 Then
                grdOpenPOList.DataSource = ds.Tables(0)
            Else
                grdOpenPOList.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub grdOpenPOList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOpenPOList.CellDoubleClick
        obj.Clear_All_TextBox(Me.GroupBox1.Controls)
        obj.Clear_All_TextBox(Me.GroupBox4.Controls)
        lblItemValue.Text = ""
        lblNetAmount.Text = ""
        lblVatCst.Text = ""
        lblExiceAmt.Text = ""
        If e.RowIndex <> -1 Then
            PO_ID = grdOpenPOList.Rows(e.RowIndex).Cells("PO_ID").Value
            getOpenPODetail(PO_ID)
            TabControl1.SelectTab(1)
        Else
            PO_ID = 0
        End If
    End Sub

    Private Sub lblItemValue_Click(sender As Object, e As EventArgs) Handles lblItemValue.Click, Label20.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click, Label26.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click, Label30.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click, Label32.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click, Label33.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click, Label23.Click

    End Sub

    Private Sub lblNetAmount_Click(sender As Object, e As EventArgs) Handles lblNetAmount.Click, Label17.Click

    End Sub

    Private Sub lblExiceAmt_Click(sender As Object, e As EventArgs) Handles lblExiceAmt.Click, Label18.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click, Label28.Click

    End Sub

End Class
