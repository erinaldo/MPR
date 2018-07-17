Imports System.Data.SqlClient
Imports MMSPlus.CommonClass

Public Class frm_Show_Search_RateList
    Inherits System.Windows.Forms.Form

    Dim comFun As New CommonClass
    Public qry As String
    Public extra_condition As String = ""
    Public checkbox_column_name As String
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
    Dim TotalCheckBoxes As Integer = 0
    Dim TotalCheckedCheckBoxes As Integer = 0
    Dim HeaderCheckBox As CheckBox = Nothing
    Dim IsHeaderCheckBoxClicked As Boolean = False

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
            cmbBrand.DropDownStyle = ComboBoxStyle.DropDown
            cmbBrand.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbBrand.AutoCompleteSource = AutoCompleteSource.ListItems
            cmbBrand.AllowDrop = True
            cmbBrand.DataSource = Dt
            cmbBrand.SelectedIndex = 0

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
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDown
            cmbCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbCategory.AutoCompleteSource = AutoCompleteSource.ListItems
            cmbCategory.AllowDrop = True
            cmbCategory.DataSource = DtCat
            cmbCategory.SelectedIndex = 0

            txtSearch.Text = ""
            'GroupBox1.Width = Me.Width - 20
            grdSearch.Width = 794
            grdSearch.ScrollBars = ScrollBars.Vertical
            FormatGrid(grdSearch)
            'comFun.GridBind(grdSearch, qry + extra_condition)

            AddHeaderCheckBox()


            'AddHandler HeaderCheckBox.KeyUp, AddressOf HeaderCheckBox_KeyUp
            AddHandler HeaderCheckBox.MouseClick, AddressOf HeaderCheckBox_MouseClick
            AddHandler HeaderCheckBox.Click, AddressOf HeaderCheckBox_Clicked

            GridBindCheckBox(grdSearch, qry + extra_condition)
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
                        i += 1
                    Next
                End If
            End If

            'txtSearch.Focus()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub AddHeaderCheckBox()
        HeaderCheckBox = New CheckBox()
        HeaderCheckBox.Size = New Size(15, 15)
        grdSearch.Controls.Add(HeaderCheckBox)
        ResetHeaderCheckBoxLocation(0, -1)
    End Sub

    Private Sub HeaderCheckBox_Clicked(ByVal sender As Object, ByVal e As EventArgs)
        'Necessary to end the edit mode of the Cell.
        grdSearch.EndEdit()

        'Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
        For Each row As DataGridViewRow In grdSearch.Rows
            Dim checkBox As DataGridViewCheckBoxCell = (TryCast(row.Cells("chkBxSelect"), DataGridViewCheckBoxCell))
            checkBox.Value = HeaderCheckBox.Checked
        Next
    End Sub

    Public Sub GridBindCheckBox(ByVal cnt As DataGridView, ByVal qry As String)
        '' Common Function to Bind a DataGrid
        Try
            Dim ds1 As DataSet
            ds1 = comFun.FillDataSet(qry)
            'GetDataSource(ds1.Tables(0))
            cnt.DataSource = ds1.Tables(0)
            'cnt.DataSource = GetDataSource(ds1)

            TotalCheckBoxes = cnt.RowCount
            TotalCheckedCheckBoxes = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error GridBind")
        End Try
    End Sub

    Private Function GetDataSource(ds1 As DataSet) As DataTable
        Dim dTable As DataTable = New DataTable()
        Dim dRow As DataRow = Nothing
        Dim rnd As Random = New Random()
        dTable.Columns.Add("IsChecked", System.Type.[GetType]("System.Boolean"))
        dTable.Columns.Add("BARCODE")
        dTable.Columns.Add("ITEM NAME")
        dTable.Columns.Add("MRP")
        dTable.Columns.Add("RATE")
        dTable.Columns.Add("BRAND")
        dTable.Columns.Add("CATEGORY")

        For n As Integer = 0 To ds1.Tables(0).Rows.Count - 1
            dRow = dTable.NewRow()
            dRow("IsChecked") = "false"
            dRow("BARCODE") = ds1.Tables(0).Rows(n)("BARCODE")
            dRow("ITEM NAME") = ds1.Tables(0).Rows(n)("ITEM NAME")
            dRow("MRP") = ds1.Tables(0).Rows(n)("MRP")
            dRow("RATE") = ds1.Tables(0).Rows(n)("RATE")
            dRow("BRAND") = ds1.Tables(0).Rows(n)("BRAND")
            dRow("CATEGORY") = ds1.Tables(0).Rows(n)("CATEGORY")
            dTable.Rows.Add(dRow)
            dTable.AcceptChanges()
        Next

        Return dTable
    End Function

    Private Sub HeaderCheckBox_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
        HeaderCheckBoxClick(CType(sender, CheckBox))
    End Sub

    Private Sub HeaderCheckBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Space Then HeaderCheckBoxClick(CType(sender, CheckBox))
    End Sub

    Private Sub HeaderCheckBoxClick(ByVal HCheckBox As CheckBox)
        IsHeaderCheckBoxClicked = True

        For Each Row As DataGridViewRow In grdSearch.Rows
            CType(Row.Cells("chkBxSelect"), DataGridViewCheckBoxCell).Value = HCheckBox.Checked
        Next

        grdSearch.RefreshEdit()
        TotalCheckedCheckBoxes = If(HCheckBox.Checked, TotalCheckBoxes, 0)
        IsHeaderCheckBoxClicked = False
    End Sub

    Private Sub grdSearch_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles grdSearch.CellClick
        'grdSearch.EditMode = True

        'Check to ensure that the row CheckBox is clicked.
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then
            RowCheckBoxClick(CType(grdSearch(e.ColumnIndex, e.RowIndex), DataGridViewCheckBoxCell))

            'Loop to verify whether all row CheckBoxes are checked or not.
            'Dim isChecked As Boolean = False
            'For Each row As DataGridViewRow In grdSearch.Rows
            '    If Convert.ToBoolean(row.Cells("chkBxSelect").Value) = True Then
            '        If (TotalCheckedCheckBoxes <> grdSearch.Rows.Count) Then
            '            RowCheckBoxClick(CType(grdSearch(e.ColumnIndex, e.RowIndex), DataGridViewCheckBoxCell))
            '        End If
            '        Exit For
            '    ElseIf Convert.ToBoolean(row.Cells("chkBxSelect").Value) = False Then
            '        RowCheckBoxClick(CType(grdSearch(e.ColumnIndex, e.RowIndex), DataGridViewCheckBoxCell))
            '        Exit For
            '    End If
            'Next
            'HeaderCheckBox.Checked = isChecked
        End If
        'grdSearch.EditMode = False

    End Sub

    Public Sub FormatGrid(ByVal grd As DataGridView)
        grd.BackgroundColor = Color.White
        grd.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
        grd.RowsDefaultCellStyle.SelectionForeColor = Color.Blue
        grd.RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray
        grd.RowHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue
        grd.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black
        grd.RowHeadersWidth = 20
        grd.ColumnHeadersHeight = 20
        grd.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        grd.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue
        grd.AllowUserToResizeColumns = False
        grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grd.MultiSelect = True
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

        For Each Row As DataGridViewRow In grdSearch.Rows
            If CType(Row.Cells("chkBxSelect"), DataGridViewCheckBoxCell).Value = True Then
                search_result = search_result & Row.Cells(ret_column).Value & ","
                If Not String.IsNullOrEmpty(item_rate_column) Then
                    item_rate = Row.Cells(item_rate_column).Value
                Else
                    item_rate = 0
                End If
            End If
        Next

        If String.IsNullOrEmpty(search_result) Then
            If grdSearch.SelectedRows.Count > 0 Then
                search_result = grdSearch.SelectedRows.Item(0).Cells(ret_column).Value
                If Not String.IsNullOrEmpty(item_rate_column) Then
                    item_rate = grdSearch.SelectedRows.Item(0).Cells(item_rate_column).Value
                Else
                    item_rate = 0
                End If
                Me.Close()
            Else
                search_result = -1
                Me.Close()
            End If
        Else
            Me.Close()
        End If

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
            GridBindCheckBox(grdSearch, search_qry)
            'comFun.GridBind(grdSearch, search_qry)
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
            If e.KeyCode = Keys.Enter AndAlso TotalCheckedCheckBoxes = grdSearch.RowCount Then
                SelectItemAndCloseForm()
            Else
                SelectItemAndCloseForm()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter AndAlso TotalCheckedCheckBoxes = grdSearch.RowCount Then
                'Dim abc As Integer = grdSearch.Rows.Count - 1
                'grdSearch.Rows(abc).Cells(0).Selected = True
                'grdSearch.Select()

                SelectItemAndCloseForm()
            Else
                SelectItemAndCloseForm()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try
    End Sub

    Private Sub grdSearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSearch.DoubleClick
        Try
            'SelectItemAndCloseForm()
            If grdSearch.SelectedRows.Count > 0 Then
                search_result = grdSearch.SelectedRows.Item(0).Cells(ret_column).Value
                If Not String.IsNullOrEmpty(item_rate_column) Then
                    item_rate = grdSearch.SelectedRows.Item(0).Cells(item_rate_column).Value
                Else
                    item_rate = 0
                End If
                Me.Close()
            Else
                search_result = -1
                Me.Close()
            End If

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
            GridBindCheckBox(grdSearch, search_qry)
            'comFun.GridBind(grdSearch, search_qry)
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
            GridBindCheckBox(grdSearch, search_qry)
            'comFun.GridBind(grdSearch, search_qry)
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
            GridBindCheckBox(grdSearch, search_qry)
            'comFun.GridBind(grdSearch, search_qry)
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
            GridBindCheckBox(grdSearch, search_qry)
            'comFun.GridBind(grdSearch, search_qry)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frmCommon_Search_Load")

        End Try
    End Sub

    Private Sub grdSearch_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdSearch.CellValueChanged
        'If Not IsHeaderCheckBoxClicked Then RowCheckBoxClick(CType(grdSearch(e.ColumnIndex, e.RowIndex), DataGridViewCheckBoxCell))
    End Sub

    Private Sub grdSearch_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles grdSearch.CurrentCellDirtyStateChanged
        If TypeOf grdSearch.CurrentCell Is DataGridViewCheckBoxCell Then grdSearch.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub grdSearch_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles grdSearch.CellPainting
        If e.RowIndex = -1 AndAlso e.ColumnIndex = 0 Then ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex)
    End Sub

    Private Sub ResetHeaderCheckBoxLocation(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim oRectangle As Rectangle = grdSearch.GetCellDisplayRectangle(ColumnIndex, RowIndex, True)
        Dim oPoint As Point = New Point()
        oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1
        oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1
        HeaderCheckBox.Location = oPoint
    End Sub

    Private Sub RowCheckBoxClick(ByVal RCheckBox As DataGridViewCheckBoxCell)
        If RCheckBox IsNot Nothing Then
            If CBool(RCheckBox.Value) AndAlso TotalCheckedCheckBoxes < TotalCheckBoxes Then
                TotalCheckedCheckBoxes += 1
            ElseIf CBool(RCheckBox.Value) = False AndAlso TotalCheckedCheckBoxes < TotalCheckBoxes Then
                TotalCheckedCheckBoxes -= 1
            ElseIf TotalCheckedCheckBoxes > 0 Then
                TotalCheckedCheckBoxes -= 1
            End If

            If TotalCheckedCheckBoxes < TotalCheckBoxes Then
                HeaderCheckBox.Checked = False
            ElseIf TotalCheckedCheckBoxes = TotalCheckBoxes Then
                HeaderCheckBox.Checked = True
            End If
        End If
    End Sub

    'Private Sub grdSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSearch.KeyPress
    '    Try
    '        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '            SendKeys.Send("{TAB}")
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message())
    '    End Try
    'End Sub

    Private Sub grdSearch_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Dim index As Integer = grdSearch.CurrentRow.Index
            If index < grdSearch.Rows.Count - 1 Then
                grdSearch.CurrentCell = grdSearch.Rows(index + 1).Cells(0)
            Else
                grdSearch.CurrentCell = grdSearch.Rows(0).Cells(0)
            End If
        End If
    End Sub

End Class