Imports System.Data.SqlClient
Imports MMSPlus.CommonClass

Public Class frm_Show_search
    Inherits System.Windows.Forms.Form

    'Dim frm_MRS_MainStore_Obj As New frm_MRS_MainStore
    '

    Dim comFun As New CommonClass
    Public qry As String
    Public extra_condition As String = ""
    Public column_name As String
    Public column_name1 As String
    Public column_name2 As String
    Public column_name3 As String
    Public column_name4 As String
    Public column_name5 As String
    Public grid_size As Integer
    Public search_result As String
    Public item_rate As Decimal
    Public ret_column As String
    Public item_rate_column As String
    Public cols_width As String = ""
    Public cols_no_for_width As String = ""

    Private Sub frm_Show_search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim BrandQuery As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            BrandQuery = " SELECT Pk_LabelDetailId_Num, LabelItemName_vch FROM dbo.Label_Items ORDER BY LabelItemName_vch "
            Dt = comFun.Fill_DataSet(BrandQuery).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("Pk_LabelDetailId_Num") = -1
            Dtrow("LabelItemName_vch") = "--ALL Brand--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbBrand.DisplayMember = "LabelItemName_vch"
            cmbBrand.ValueMember = "LabelItemName_vch"
            'cmbBrand.DropDownStyle = ComboBoxStyle.DropDown
            'cmbBrand.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'cmbBrand.AutoCompleteSource = AutoCompleteSource.ListItems
            'cmbBrand.AllowDrop = True
            cmbBrand.DataSource = Dt
            'cmbBrand.SelectedIndex = 0

            Dim CategoryQuery As String
            Dim DtCat As DataTable
            Dim Dtrowcat As DataRow
            CategoryQuery = " Select  cast(ITEM_CAT_ID as varchar) AS ITEM_CAT_ID, ITEM_CAT_NAME from ITEM_CATEGORY ORDER BY ITEM_CAT_ID"
            DtCat = comFun.Fill_DataSet(CategoryQuery).Tables(0)
            Dtrowcat = DtCat.NewRow
            Dtrowcat("ITEM_CAT_ID") = -1
            Dtrowcat("ITEM_CAT_NAME") = "--All Categories--"
            DtCat.Rows.InsertAt(Dtrowcat, 0)
            cmbCategory.DisplayMember = "ITEM_CAT_NAME"
            cmbCategory.ValueMember = "ITEM_CAT_NAME"
            'cmbCategory.DropDownStyle = ComboBoxStyle.DropDown
            'cmbCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'cmbCategory.AutoCompleteSource = AutoCompleteSource.ListItems
            'cmbCategory.AllowDrop = True
            cmbCategory.DataSource = DtCat
            'cmbCategory.SelectedIndex = 0

            txtSearch.Text = ""
            'GroupBox1.Width = Me.Width - 20
            grdSearch.Width = 806
            grdSearch.ScrollBars = ScrollBars.Vertical
            '  comFun.FormatGrid(grdSearch)
            comFun.GridBind(grdSearch, qry + extra_condition)
            grdSearch.Columns(ret_column).Visible = False

            Dim i As Integer
            Dim col_index As Integer

            If cols_width <> "" Then
                If cols_no_for_width <> "" Then

                    Dim arr_width, arr_cols_no As Array

                    arr_width = Split(cols_width, ",")
                    arr_cols_no = Split(cols_no_for_width, ",")
                    i = 0

                    For Each a As String In arr_width
                        col_index = arr_cols_no(i)
                        grdSearch.Columns(col_index).Width = arr_width(i)
                        grdSearch.Columns(col_index).HeaderCell.Style.Font = New Font("Arial", 9, FontStyle.Bold)
                        i += 1
                    Next
                End If
            End If

            txtSearch.Focus()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SelectItemAndCloseForm()
            Else
                search_result = -1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SelectItemAndCloseForm()
        If grdSearch.SelectedRows.Count > 0 Then
            search_result = grdSearch.SelectedRows.Item(0).Cells(ret_column).Value
            If Not String.IsNullOrEmpty(item_rate_column) Then
                item_rate = grdSearch.SelectedRows.Item(0).Cells(item_rate_column).Value
            Else
                item_rate = 0
            End If
        End If

        Me.Close()
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        Try
            If e.KeyCode = Keys.Down Then
                grdSearch.Rows(0).Selected = True
                grdSearch.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Dim search_qry As String
            If extra_condition <> "" Then
                search_qry = qry & extra_condition & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                If (txtMRP.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                End If
                If (txtRate.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                End If
                If (cmbBrand.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                Else
                    'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                    search_qry = search_qry
                End If
                If (cmbCategory.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                Else
                    search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                End If
            Else
                If qry.ToLower().Contains("where") Then
                    search_qry = qry & extra_condition & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                Else
                    search_qry = qry & extra_condition & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                End If

            End If
            comFun.GridBind(grdSearch, search_qry)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frmCommon_Search_Load")

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'SelectItemAndCloseForm()
            search_result = -1
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdSearch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSearch.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                SelectItemAndCloseForm()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SelectItemAndCloseForm()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdSearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSearch.DoubleClick
        Try
            SelectItemAndCloseForm()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frm_Show_search_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                search_result = -1
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Dim k As Integer
        k = comFun.AllCaps(Asc(e.KeyChar))
        If k = 0 Then
            e.Handled = True
        Else
            e.KeyChar = Chr(k)
        End If
    End Sub

    Private Sub txtMRP_TextChanged(sender As Object, e As EventArgs) Handles txtMRP.TextChanged

        Try
            Dim search_qry As String
            If extra_condition <> "" Then
                search_qry = qry & extra_condition
                If (txtMRP.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                End If
                If (txtRate.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                End If
                If (txtSearch.Text <> "") Then
                    search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                End If
                If (cmbBrand.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                Else
                    'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                    search_qry = search_qry
                End If
                If (cmbCategory.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                Else
                    search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                End If
            Else
                If qry.ToLower().Contains("where") Then
                    search_qry = qry & extra_condition
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                Else
                    search_qry = qry & extra_condition
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                End If

            End If
            comFun.GridBind(grdSearch, search_qry)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frmCommon_Search_Load")

        End Try

    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged

        Try
            Dim search_qry As String
            If extra_condition <> "" Then
                search_qry = qry & extra_condition
                If (txtRate.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                End If
                If (txtMRP.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                End If
                If (txtSearch.Text <> "") Then
                    search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                End If
                If (cmbBrand.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                Else
                    'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                    search_qry = search_qry
                End If
                If (cmbCategory.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                Else
                    search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                End If
            Else
                If qry.ToLower().Contains("where") Then
                    search_qry = qry & extra_condition
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                Else
                    search_qry = qry & extra_condition
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                End If

            End If
            comFun.GridBind(grdSearch, search_qry)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frmCommon_Search_Load")

        End Try

    End Sub

    Private Sub cmbBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBrand.SelectedIndexChanged
        Try
            Dim search_qry As String
            If extra_condition <> "" Then
                If (cmbBrand.SelectedIndex <> 0) Then
                    search_qry = qry & extra_condition & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                Else
                    'search_qry = qry & extra_condition & " and upper(" & column_name4 & ") like '%%'"
                    search_qry = qry & extra_condition
                End If
                If (cmbCategory.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                Else
                    search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                End If
                If (txtMRP.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                End If
                If (txtRate.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                End If
                If (txtSearch.Text <> "") Then
                    search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                End If
            Else
                If qry.ToLower().Contains("where") Then
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = qry & extra_condition & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = qry & extra_condition & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = qry & extra_condition
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                Else
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = qry & extra_condition & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = qry & extra_condition & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = qry & extra_condition
                    End If
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = search_qry & " and upper(" & column_name5 & ") like '%%'"
                    End If
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                End If

            End If
            comFun.GridBind(grdSearch, search_qry)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frmCommon_Search_Load")

        End Try

    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        Try
            Dim search_qry As String
            If extra_condition <> "" Then
                If (cmbCategory.SelectedIndex <> 0) Then
                    search_qry = qry & extra_condition & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                Else
                    search_qry = qry & extra_condition & " and upper(" & column_name5 & ") like '%%'"
                End If
                If (cmbBrand.SelectedIndex <> 0) Then
                    search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                Else
                    'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                    search_qry = search_qry
                End If
                If (txtMRP.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                End If
                If (txtRate.Text <> "") Then
                    search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                End If
                If (txtSearch.Text <> "") Then
                    search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                End If
            Else
                If qry.ToLower().Contains("where") Then
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = qry & extra_condition & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = qry & extra_condition & " and upper(" & column_name5 & ") like '%%'"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                Else
                    If (cmbCategory.SelectedIndex <> 0) Then
                        search_qry = qry & extra_condition & " and upper(" & column_name5 & ") Like '%" & cmbCategory.SelectedValue & "%'"
                    Else
                        search_qry = qry & extra_condition & " and upper(" & column_name5 & ") like '%%'"
                    End If
                    If (cmbBrand.SelectedIndex <> 0) Then
                        search_qry = search_qry & " and upper(" & column_name4 & ") Like '%" & cmbBrand.SelectedValue & "%'"
                    Else
                        'search_qry = search_qry & " and upper(" & column_name4 & ") like '%%'"
                        search_qry = search_qry
                    End If
                    If (txtMRP.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name2 & ") like '%" & txtMRP.Text & "%'"
                    End If
                    If (txtRate.Text <> "") Then
                        search_qry = search_qry & " and  upper(" & column_name3 & ") like '%" & txtRate.Text & "%'"
                    End If
                    If (txtSearch.Text <> "") Then
                        search_qry = search_qry & " and ( upper(" & column_name & ") like '%" & txtSearch.Text & "%'" & " or upper(" & column_name1 & ") like '%" & txtSearch.Text & "%')"
                    End If
                End If

            End If
            comFun.GridBind(grdSearch, search_qry)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frmCommon_Search_Load")

        End Try
    End Sub

    Private Sub cmbBrand_Enter(sender As Object, e As EventArgs) Handles cmbBrand.Enter
        If Not cmbBrand.DroppedDown Then
            cmbBrand.DroppedDown = True
        End If
    End Sub

    Private Sub cmbCategory_Enter(sender As Object, e As EventArgs) Handles cmbCategory.Enter
        If Not cmbCategory.DroppedDown Then
            cmbCategory.DroppedDown = True
        End If
    End Sub
End Class