
Imports C1.Win.C1FlexGrid

Public Class frm_Indent_Items

    Dim dTable_IndentItems As DataTable
    Public dTable_POItems As DataTable
    Public v_supp_id As Integer
    Dim strItemIDs, strIndentIDs As String
    Dim dtable_Item_List As DataTable
    Dim commObj As New CommonClass
    Public flag As String
    Public _po_id As Integer

    Private Sub frm_Indent_Items_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtpFromDate.Value = Now.AddDays(-30)
            fillGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub fillGrid()
        Dim strIndentItem, strFromDate, strToDate As String
        Dim i As Integer
        strFromDate = ""
        strToDate = ""
        strIndentItem = ""
        Dim strItemID, strIndentID As String
        If Not dTable_POItems Is Nothing Then
            For i = 0 To dTable_POItems.Rows.Count - 1
                strItemID = dTable_POItems.Rows(i)("Item_ID").ToString()
                strIndentID = dTable_POItems.Rows(i)("Indent_ID").ToString()
                If strIndentItem = "" Then
                    strIndentItem = strItemID + "A" + strIndentID
                Else
                    strIndentItem = strIndentItem + "," + strItemID + "A" + strIndentID
                End If
            Next
        End If

        If strIndentItem = "" Then
            strIndentItem = "-1A-1,"
        Else
            strIndentItem = strIndentItem + ","
        End If



        strFromDate = dtpFromDate.Value.ToString("dd-MMM-yyyy")
        strToDate = dtpToDate.Value.ToString("dd-MMM-yyyy")



        Dim ds As New DataSet()
       
        If flag = "save" Then
            dTable_IndentItems = commObj.fill_Data_set_val("FILL_PENDING_INDENTS", "@ITEMID_INDENTID", "@Div_ID", "@From", "@To", "", strIndentItem, v_the_current_division_id, strFromDate, strToDate, "").Tables(0)
        Else
            dTable_IndentItems = commObj.fill_Data_set_val("FILL_PENDING_INDENTS_FOR_EDIT", "@ITEMID_INDENTID", "@Div_ID", "@PO_ID", "@From", "@To", strIndentItem, v_the_current_division_id, _po_id, strFromDate, strToDate).Tables(0)
        End If
      


        grdIndentItems.DataSource = dTable_IndentItems

        grdIndentItems.Cols(0).Width = 20

        grdIndentItems.Cols("INDENT_NO").AllowDragging = True
        grdIndentItems.Cols("ITEM_CODE").AllowDragging = True
        grdIndentItems.Cols("INDENT_ID").AllowDragging = False
        grdIndentItems.Cols("ITEM_ID").AllowDragging = False
        grdIndentItems.Cols("INDENT_DATE").AllowDragging = False
        grdIndentItems.Cols("ITEM_NAME").AllowDragging = False
        grdIndentItems.Cols("UOM").AllowDragging = False
        grdIndentItems.Cols("REQUIRED_QTY").AllowDragging = False
        grdIndentItems.Cols("ORDER_QTY").AllowDragging = False




        grdIndentItems.Cols("INDENT_ID").Visible = False
        grdIndentItems.Cols("ITEM_ID").Visible = False


        grdIndentItems.Cols("INDENT_NO").Caption = "Indent No"
        grdIndentItems.Cols("ITEM_CODE").Caption = "Item Code"
        grdIndentItems.Cols("INDENT_ID").Caption = "Indent Id"
        grdIndentItems.Cols("ITEM_ID").Caption = "Item ID"
        grdIndentItems.Cols("INDENT_DATE").Caption = "Indent Date"
        grdIndentItems.Cols("ITEM_NAME").Caption = "Item Name"
        grdIndentItems.Cols("UOM").Caption = "UOM"
        grdIndentItems.Cols("REQUIRED_QTY").Caption = "Required Qty"
        grdIndentItems.Cols("ORDER_QTY").Caption = "Order Qty"


        grdIndentItems.Cols("INDENT_NO").AllowEditing = False
        grdIndentItems.Cols("ITEM_CODE").AllowEditing = False
        grdIndentItems.Cols("INDENT_ID").AllowEditing = False
        grdIndentItems.Cols("ITEM_ID").AllowEditing = False
        grdIndentItems.Cols("INDENT_DATE").AllowEditing = False
        grdIndentItems.Cols("ITEM_NAME").AllowEditing = False
        grdIndentItems.Cols("UOM").AllowEditing = False
        grdIndentItems.Cols("REQUIRED_QTY").AllowEditing = False
        grdIndentItems.Cols("ORDER_QTY").AllowEditing = True


    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        fillGrid()
    End Sub

    Private Sub generate_tree()

        Dim strSort As String = grdIndentItems.Cols(1).Name + ", " + grdIndentItems.Cols(2).Name + ", " + grdIndentItems.Cols(3).Name
        Dim dt As DataTable = CType(grdIndentItems.DataSource, DataTable)
        If Not dt Is Nothing Then
            dt.DefaultView.Sort = strSort
        End If

        grdIndentItems.Tree.Style = TreeStyleFlags.Simple
        grdIndentItems.Tree.Column = 1
        grdIndentItems.AllowMerging = AllowMergingEnum.Nodes

        Dim totalOn As Integer = grdIndentItems.Cols("Required_Qty").SafeIndex
        grdIndentItems.Subtotal(AggregateEnum.Sum, 0, 1, totalOn)
    End Sub

    Private Sub grdIndentItems_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles grdIndentItems.AfterDataRefresh
        Try
            generate_tree()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdIndentItems_AfterDragColumn(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.DragRowColEventArgs) Handles grdIndentItems.AfterDragColumn
        generate_tree()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        
        Dim i As Integer
        Dim dr As DataRow
        Dim ds As DataSet

        For i = 1 To grdIndentItems.Rows.Count - 1
            If Not grdIndentItems.Rows(i).IsNode Then
                If Convert.ToDouble(grdIndentItems.Rows(i)("Order_Qty")) > 0 Then
                    dr = dTable_POItems.NewRow
                    dr("Item_ID") = grdIndentItems.Rows(i)("ITEM_ID").ToString
                    dr("Indent_ID") = grdIndentItems.Rows(i)("INDENT_ID").ToString
                    dr("Item_Code") = grdIndentItems.Rows(i)("ITEM_CODE").ToString
                    dr("Item_Name") = grdIndentItems.Rows(i)("ITEM_NAME").ToString
                    dr("UM_Name") = grdIndentItems.Rows(i)("UOM").ToString
                    dr("Req_Qty") = grdIndentItems.Rows(i)("REQUIRED_QTY").ToString
                    dr("PO_Qty") = grdIndentItems.Rows(i)("Order_Qty").ToString
                    ds = commObj.Fill_DataSet("DECLARE @rate NUMERIC (18,2);SELECT @rate=SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE FROM SUPPLIER_RATE_LIST INNER JOIN SUPPLIER_RATE_LIST_DETAIL ON SUPPLIER_RATE_LIST.SRL_ID = SUPPLIER_RATE_LIST_DETAIL.SRL_ID WHERE (SUPPLIER_RATE_LIST_DETAIL.ITEM_ID = " & grdIndentItems.Rows(i)("ITEM_ID").ToString() & " ) AND (SUPPLIER_RATE_LIST.SUPP_ID = " & v_supp_id & ") AND (SUPPLIER_RATE_LIST.ACTIVE = 1);SELECT ISNULL(@rate,0);SELECT     ITEM_DETAIL.PURCHASE_VAT_ID as PURCHASE_VAT_ID, VAT_MASTER.VAT_PERCENTAGE, VAT_MASTER.VAT_NAME FROM ITEM_DETAIL INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID WHERE (ITEM_DETAIL.ITEM_ID = " & grdIndentItems.Rows(i)("ITEM_ID").ToString & " )")
                    dr("Item_Rate") = ds.Tables(0).Rows(0)(0)
                    dr("Vat_Id") = ds.Tables(1).Rows(0)("PURCHASE_VAT_ID")
                    dr("Vat_Name") = ds.Tables(1).Rows(0)("VAT_NAME")
                    dr("Vat_Per") = ds.Tables(1).Rows(0)("VAT_PERCENTAGE")
                    dr("Item_Value") = (dr("PO_Qty") * dr("Item_Rate")) + ((dr("PO_Qty") * dr("Item_Rate") * dr("Vat_Per")) / 100)
                    dTable_POItems.Rows.Add(dr)
                End If
            End If
        Next
        Me.Close()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub grdIndentItems_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdIndentItems.AfterEdit
        If grdIndentItems.Rows(e.Row).Item("REQUIRED_QTY") < grdIndentItems.Rows(e.Row).Item("Order_Qty") And Not grdIndentItems.Rows(grdIndentItems.CursorCell.r1).IsNode Then
            grdIndentItems.Rows(e.Row).Item("Order_Qty") = 0
        End If
    End Sub

    Private Sub grdIndentItems_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles grdIndentItems.KeyPressEdit
        e.Handled = grdIndentItems.Rows(grdIndentItems.CursorCell.r1).IsNode
    End Sub

End Class