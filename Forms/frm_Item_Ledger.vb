Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class frm_Item_Ledger
    Implements IForm
    Dim Obj As New item_detail.cls_item_detail


    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Item_Ledger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillGrid()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Call Obj.GridBind(grdItemMaster, "SELECT ITEM_MASTER.ITEM_ID,ITEM_MASTER.ITEM_CODE," _
              & " ITEM_MASTER.ITEM_NAME,UNIT_MASTER.UM_Name,ITEM_CATEGORY.ITEM_CAT_NAME, CAST(0 AS BIT) AS [SELECT]  FROM ITEM_MASTER " _
              & " INNER JOIN  UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID INNER JOIN ITEM_CATEGORY " _
              & " ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID where (item_master.item_code + " _
            & " item_master.item_name + ITEM_CATEGORY.item_cat_name + UNIT_MASTER.um_name ) " _
            & " like '%" & txtSearch.Text & "%'")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")

        End Try
    End Sub


    Private Sub FillGrid()
        Try
            Call Obj.GridBind(grdItemMaster, "SELECT ITEM_MASTER.ITEM_ID,ITEM_MASTER.ITEM_CODE," _
                & " ITEM_MASTER.ITEM_NAME,UNIT_MASTER.UM_Name,ITEM_CATEGORY.ITEM_CAT_NAME, CAST(0 AS BIT) AS [SELECT] FROM ITEM_MASTER " _
                & " INNER JOIN  UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID INNER JOIN ITEM_CATEGORY " _
                & " ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID inner join item_detail on item_master.item_id = item_detail.item_id order by Item_Master.Item_Code ")
            grdItemMaster.Columns(0).Visible = False 'Item Master id
            grdItemMaster.Columns(0).HeaderText = "Item ID"
            grdItemMaster.Columns(0).Width = 0
            grdItemMaster.Columns(1).HeaderText = "Item Code"
            grdItemMaster.Columns(1).Width = 100
            grdItemMaster.Columns(2).HeaderText = "Item Name"
            grdItemMaster.Columns(2).Width = 400
            grdItemMaster.Columns(3).HeaderText = "Item Unit"
            grdItemMaster.Columns(3).Width = 100

            grdItemMaster.Columns(4).HeaderText = "Item Category Name"
            grdItemMaster.Columns(4).Width = 200
            grdItemMaster.Columns(5).Width = 55

            grdItemMaster.Columns(1).ReadOnly = True
            grdItemMaster.Columns(2).ReadOnly = True
            grdItemMaster.Columns(3).ReadOnly = True
            grdItemMaster.Columns(4).ReadOnly = True
            grdItemMaster.Columns(5).ReadOnly = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Dim filepath As String = ""
        Dim ITEM_IDS As String = "-1"

        grdItemMaster.EndEdit()

        For i As Integer = 0 To grdItemMaster.Rows.Count - 1
            If grdItemMaster.Rows(i).Cells(5).Value = True Then ITEM_IDS = ITEM_IDS & "," & grdItemMaster.Rows(i).Cells("item_id").Value
        Next

        Dim rep As New ReportDocument()
        Dim selectionFormnula As String = ""
        filepath = ReportFilePath & "Item Ledger.rpt"
        ' Dim rep As New MyReportDocument(filepath)

        rep.Load(filepath)

        rep.SetParameterValue("@Item_ID", ITEM_IDS)
        rep.SetParameterValue("@Div_ID", v_the_current_division_id)
        rep.SetParameterValue("@FromDate", dt_FromDate.Value.Date)
        rep.SetParameterValue("@ToDate", dt_ToDate.Value.Date)


        ' rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "afbl_mms")
        Dim connection As New ConnectionInfo()


        connection.DatabaseName = gblDataBase_Name 'myDataBase
        connection.ServerName = gblDataBaseServer_Name '127.0.0.1
        connection.UserID = gblDataBase_UserName 'root
        connection.Password = gblDataBase_Password '12345



        ' First we assign the connection to all tables in the main report
        '
        For Each table As CrystalDecisions.CrystalReports.Engine.Table In rep.Database.Tables
            AssignTableConnection(table, connection)
        Next

        frm_Report.cryViewer.ReportSource = rep
        frm_Report.Show()

    End Sub

    Private Sub AssignTableConnection(ByVal table As CrystalDecisions.CrystalReports.Engine.Table, ByVal connection As ConnectionInfo)

        ' Cache the logon info block

        Dim logOnInfo As TableLogOnInfo = table.LogOnInfo
        connection.Type = logOnInfo.ConnectionInfo.Type
        logOnInfo.ConnectionInfo = connection
        table.LogOnInfo.ConnectionInfo.DatabaseName = connection.DatabaseName
        table.LogOnInfo.ConnectionInfo.ServerName = connection.ServerName
        table.LogOnInfo.ConnectionInfo.UserID = connection.UserID
        table.LogOnInfo.ConnectionInfo.Password = connection.Password
        table.LogOnInfo.ConnectionInfo.Type = connection.Type
        table.ApplyLogOnInfo(logOnInfo)

    End Sub


    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        select_deselectAll(True)
    End Sub

    Private Sub select_deselectAll(ByVal true_false As Boolean)
        Dim i As Integer
        For i = 0 To grdItemMaster.Rows.Count - 1
            grdItemMaster.Rows(i).Cells(5).Value = true_false
        Next
    End Sub

    Private Sub btnDeselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAll.Click
        select_deselectAll(False)
    End Sub

    Private Sub btInverseSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btInverseSelect.Click
        Dim i As Integer
        For i = 0 To grdItemMaster.Rows.Count - 1
            grdItemMaster.Rows(i).Cells(5).Value = Not grdItemMaster.Rows(i).Cells(5).Value
        Next
    End Sub
End Class
