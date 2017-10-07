Public Class frm_ItemStockable_CC_List
    Dim dTable_IndentItems As DataTable
    Dim dtable_Item_List As DataTable
    Dim obj As New CommonClass

    Private Sub Grid_styles()
        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()


        dtable_Item_List = New DataTable()
        dtable_Item_List.Columns.Add("CostCenter_Id", GetType(System.Int32))
        dtable_Item_List.Columns.Add("CostCenter_Code", GetType(System.String))
        dtable_Item_List.Columns.Add("CostCenter_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("Qty", GetType(System.Double))


        FLXGRID_CC.DataSource = dtable_Item_List

        dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)
        FLXGRID_CC.Cols(0).Width = 10

        SetGridSettingValues()


    End Sub
    Private Sub SetGridSettingValues()
        FLXGRID_CC.Cols(0).Visible = False
        FLXGRID_CC.Cols("CostCenter_Id").Visible = False
        FLXGRID_CC.Cols("CostCenter_Code").Caption = "CC Code"
        FLXGRID_CC.Cols("CostCenter_Name").Caption = "CC Name"
        FLXGRID_CC.Cols("Qty").Caption = "Quantity"

        FLXGRID_CC.Cols("CostCenter_Code").AllowEditing = False
        FLXGRID_CC.Cols("CostCenter_Name").AllowEditing = False
        FLXGRID_CC.Cols("Qty").AllowEditing = True

        FLXGRID_CC.Cols("CostCenter_Code").Width = 100
        FLXGRID_CC.Cols("CostCenter_Name").Width = 200
        FLXGRID_CC.Cols("Qty").Width = 75

    End Sub
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Me.Close()
    End Sub

    Private Sub frm_ItemStockable_CC_List_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ds As DataSet
            ds = obj.Fill_DataSet("SELECT dbo.COST_CENTER_MASTER.CostCenter_Id,dbo.COST_CENTER_MASTER.CostCenter_Code,dbo.COST_CENTER_MASTER.CostCenter_Name FROM dbo.COST_CENTER_MASTER WHERE Costcenter_IsActive = 1")
            If Not ds Is Nothing Then
                'Dim drItem As DataRow
                'drItem = dtable_Item_List.NewRow
                'drItem("Item_Id") = ds.Tables(0).Rows(0)(0)
                'drItem("Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                'drItem("Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                'drItem("um_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString()
                FLXGRID_CC.DataSource = ds.Tables(0)
                'Grid_styles()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class