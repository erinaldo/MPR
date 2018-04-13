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
    Public grid_size As Integer
    Public search_result As String
    Public item_rate As Decimal
    Public ret_column As String
    Public item_rate_column As String

    Public cols_width As String = ""
    Public cols_no_for_width As String = ""


    Private Sub frm_Show_search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            txtSearch.Text = ""
            GroupBox1.Width = Me.Width - 20
            grdSearch.Width = Me.Width - 50
            grdSearch.ScrollBars = ScrollBars.Vertical
            comFun.FormatGrid(grdSearch)
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
                search_qry = qry & extra_condition & " and upper(" & column_name & ") like '%" & txtSearch.Text & "%'"
            Else
                If qry.Contains("where") Then
                    search_qry = qry & " and upper(" & column_name & ") like '%" & txtSearch.Text & "%'"
                Else
                    search_qry = qry & " where upper(" & column_name & ") like '%" & txtSearch.Text & "%'"
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
End Class