Imports C1.Win.C1FlexGrid

Public Class frm_MRN_ITEMS

    Dim dTable_MrnItems As DataTable

    Dim v_dTable_POItems As DataTable

    '    Dim v_frm_purchase_master As New frm_Purchase_Order()

    Dim strItemIDs, strmrnIDs As String

    Dim dtable_Item_List As DataTable

    Dim commObj As New CommonClass

    Private Sub frm_MRN_ITEMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillGrid()
    End Sub

    Private Sub InitilizeGrid()
        dTable_MrnItems = New DataTable("PO_Items")
        dTable_MrnItems.Columns.Add("item_id", GetType(System.Int32))
        dTable_MrnItems.Columns.Add("mrn_id", GetType(System.Int32))
        dTable_MrnItems.Columns.Add("indent_no", GetType(System.String))
        dTable_MrnItems.Columns.Add("item_code", GetType(System.String))
        dTable_MrnItems.Columns.Add("item_name", GetType(System.String))
        dTable_MrnItems.Columns.Add("UOM", GetType(System.String))
        dTable_MrnItems.Columns.Add("Req_qty", GetType(System.Double))
    End Sub

    Private Sub fillGrid()
        Dim strMrnItem As String = ""
        Dim strFromDate As String = ""
        Dim strToDate As String = ""
        'DataTable dt = (DataTable)Session["dTable_POItems"];
        'Dim i As Integer

        strFromDate = ""
        strToDate = ""




        If strMrnItem = "" Then
            strMrnItem = "-1A-1,"
        Else
            strMrnItem = strMrnItem + ","
        End If


        strFromDate = dtpFromDate.Value.ToString("dd-MMM-yyyy")
        strToDate = dtpToDate.Value.ToString("dd-MMM-yyyy")




        dTable_MrnItems = commObj.fill_Data_set("FILL_MRN_MASTER_DETAIL", "@ITEMID_MRNID,@Div_ID,@From,@To", strMrnItem & v_the_current_division_id & "," & strFromDate & "," & strToDate).Tables(0)


        grdMRNItems.DataSource = dTable_MrnItems
        grdMRNItems.Cols(0).Width = 20

        grdMRNItems.Cols(4).AllowDragging = False
        grdMRNItems.Cols(5).AllowDragging = False
        grdMRNItems.Cols(6).AllowDragging = False
        grdMRNItems.Cols(7).AllowDragging = False


        grdMRNItems.Cols(5).Visible = True




        grdMRNItems.Cols(0).AllowEditing = False
        grdMRNItems.Cols(1).AllowEditing = False
        grdMRNItems.Cols(2).AllowEditing = False
        grdMRNItems.Cols(3).AllowEditing = False
        grdMRNItems.Cols(4).AllowEditing = False
        grdMRNItems.Cols(5).AllowEditing = False
        grdMRNItems.Cols(6).AllowEditing = False

        'Dim strSort As String = grdIndentItems.Cols(1).Name + ", " + grdIndentItems.Cols(2).Name + ", " + grdIndentItems.Cols(3).Name
        'Dim dt As DataTable = CType(grdIndentItems.DataSource, DataTable)
        'If Not dt Is Nothing Then
        '    dt.DefaultView.Sort = strSort
        'End If


        'grdItems.DataSource = ds;
        'grdItems.DataBind();
        'ds.Dispose();
        'grdIndents.Visible = false;
        'grdItems.Visible = true;
        'grdOutlets.Visible = false;
    End Sub

    Private Sub generate_tree()

        Dim strSort As String = grdMRNItems.Cols(1).Name + ", " + grdMRNItems.Cols(2).Name + ", " + grdMRNItems.Cols(3).Name
        Dim dt As DataTable = CType(grdMRNItems.DataSource, DataTable)
        If Not dt Is Nothing Then
            dt.DefaultView.Sort = strSort
        End If

        grdMRNItems.Tree.Style = TreeStyleFlags.Simple

        grdMRNItems.Tree.Column = 1
        grdMRNItems.AllowMerging = AllowMergingEnum.Nodes

        Dim totalOn As Integer = grdMRNItems.Cols("Required_Qty").SafeIndex
        grdMRNItems.Subtotal(AggregateEnum.Sum, 0, 1, totalOn)
        'grdIndentItems.Subtotal(AggregateEnum.Sum, 1, 2, totalOn)
        'grdIndentItems.Subtotal(AggregateEnum.Sum, 2, 3, totalOn)
        'grdIndentItems.AutoSizeCols(1, 1, 1000, 3, 30, AutoSizeFlags.IgnoreHidden)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Label3.Text = ""
    End Sub

    Private Sub btnShow_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        fillGrid()
    End Sub

    Private Sub grdMRNItems_AfterDataRefresh_1(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles grdMRNItems.AfterDataRefresh
        Try

            generate_tree()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMRNItems_AfterDragColumn_1(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.DragRowColEventArgs) Handles grdMRNItems.AfterDragColumn
        generate_tree()
    End Sub

    Private Sub btnOk_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim i As Int32
        dtable_Item_List = New DataTable()
        dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Mrn_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
        dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("UM_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Req_Qty", GetType(System.Double))

        grdMRNItems.SelectionMode = SelectionModeEnum.RowRange
        'For i = 0 To dTable_IndentItems.Rows.Count - 1 Step 1
        Dim startrow As Int16
        Dim endrow As Int16
        startrow = grdMRNItems.Row
        endrow = grdMRNItems.RowSel
        For i = startrow To endrow Step 1
            Label3.Text += i.ToString() + ","
        Next
        'grdIndentItems.Rows(1)
        'Next
        'Me.Close()
    End Sub
End Class
