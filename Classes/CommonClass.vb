Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Data
Imports System.Transactions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Xml
Imports System.IO
Imports Microsoft.Office.Interop

Public Class CommonClass
    Inherits Connection

    Public Sub fill_GridCombo(ByVal qry As String, ByVal grdcmb As DataGridViewComboBoxColumn, ByVal vm As String, ByVal dm As String)

        Dim ds As New DataSet()
        'On Error Resume Next
        ds = FillDataSet(qry)
        'If Err.Number = 5 Then
        '   MsgBox("Please check data bindings", MsgBoxStyle.Information)
        'End If
        Dim dt As DataTable
        Dim dr1 As DataRow
        dt = ds.Tables(0)
        dr1 = dt.NewRow()
        'dr1(0) = "(Select)"
        'ds.Tables(0).Rows.InsertAt(dr1(0), 0)
        grdcmb.ValueMember = vm
        grdcmb.DisplayMember = dm
        grdcmb.DataSource = dt
    End Sub

    ' Dim DR As Odbc.OdbcDataReader
    Public Function FillDataSet(ByVal qry As String) As DataSet
        '' Common Function to open a DataSet
        Try
            'da = New OdbcDataAdapter(qry, con)
            da = New SqlDataAdapter(qry, con)
            ds = New DataSet
            da.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error FillDataSet")
            Return ds
        Finally
            da.Dispose()
        End Try
    End Function

    Public Function FillDataSet_Remote(ByVal qry As String) As DataSet
        '' Common Function to open a DataSet
        Try
            Dim con_gbl As New SqlConnection(gblDNS_Online)
            'da = New OdbcDataAdapter(qry, con)
            da = New SqlDataAdapter(qry, con_gbl)
            ds = New DataSet
            da.Fill(ds)
            con_gbl.Close()
            con_gbl.Dispose()
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error FillDataSet")
            Return ds
        Finally
            da.Dispose()
        End Try
    End Function

    Public Sub Valid_NumberGrid(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Valid_Number(Asc(e.KeyChar), sender) = False Then
            e.Handled = True
        End If
    End Sub

    Public Function Valid_Number(ByVal KeyAscii As Integer, ByVal txt As TextBox) As Boolean
        Dim flag As Boolean
        Dim a As Integer
        Dim MaxDigit As Integer
        Dim MaxDecimal As Integer
        Dim MaxLength As Integer

        MaxDigit = 9
        MaxDecimal = 6
        MaxLength = 16

        Valid_Number = True

        '****Stop placing decimal if value greater than maximum value
        Dim ss As String
        If Chr(KeyAscii) = "." Then
            If InStr(txt.Text, ".") > 0 Then
                Valid_Number = False
                Exit Function
            Else

                ss = Left(txt.Text, txt.SelectionStart)
                ss = ss & "."
                ss = ss & Right(txt.Text, Len(txt.Text) - txt.SelectionStart)
                If Convert.ToDecimal(ss) > gblMaxValue Then
                    Valid_Number = False
                    Exit Function
                End If
            End If
        End If
        '***********************
        If KeyAscii = 8 Then
            Exit Function
        End If

        If InStr(txt.Text, ".") > 0 Then
            If Len(txt.Text) >= MaxLength Then
                KeyAscii = 0
                Valid_Number = False
                Exit Function
            End If
            If Len(txt.Text) - InStr(txt.Text, ".") >= MaxDecimal And Len(Left(txt.Text, InStr(txt.Text, ".") - 1)) < MaxDigit And txt.SelectionStart >= InStr(txt.Text, ".") And KeyAscii <> 8 Then
                KeyAscii = 0
                Valid_Number = False
                Exit Function
            End If
            If Len(txt.Text) - InStr(txt.Text, ".") < MaxDecimal And Len(Left(txt.Text, InStr(txt.Text, "."))) > MaxDigit And txt.SelectionStart < InStr(txt.Text, ".") And KeyAscii <> 8 Then
                KeyAscii = 0
                Valid_Number = False
                Exit Function
            End If
        Else
            If Len(txt.Text) >= MaxDigit And Chr(KeyAscii) <> "." And KeyAscii <> 8 Then
                KeyAscii = 0
                Valid_Number = False
            End If
        End If
        a = InStr("1234567890.", Chr(KeyAscii))
        If a <> 0 Then

            'if flag
            If (Chr(KeyAscii) = "." And InStr(txt.Text, ".") <= 0) Or Chr(KeyAscii) <> "." Then
                'txt.Text = txt.Text + Chr(KeyAscii)
                flag = True
                Valid_Number = True
            Else
                KeyAscii = 0
                Valid_Number = False
            End If
        ElseIf KeyAscii <> 8 Then
            KeyAscii = 0
            Valid_Number = False
            ''MsgBox "Keys (0-9) Allowed Only....", vbInformation, "Alchemist Ltd."
        End If

    End Function

    Public Function GridValid_Number(ByVal KeyAscii As Integer, ByVal txt As String) As Boolean
        Dim flag As Boolean
        Dim a As Integer
        Dim MaxDigit As Integer
        Dim MaxDecimal As Integer
        Dim MaxLength As Integer

        MaxDigit = 9
        MaxDecimal = 6
        MaxLength = 16
        GridValid_Number = True

        '****Stop placing decimal if value greater than maximum value
        '' ''Dim ss As String
        '' ''If Chr(KeyAscii) = "." Then
        '' ''    If InStr(txt, ".") > 0 Then
        '' ''        GridValid_Number = False
        '' ''        Exit Function
        '' ''    Else

        '' ''        ss = Left(txt, txt.SelectionStart)
        '' ''        ss = ss & "."
        '' ''        ss = ss & Right(txt.Text, Len(txt.Text) - txt.SelectionStart)
        '' ''        If Convert.ToDecimal(ss) > gblMaxValue Then
        '' ''            GridValid_Number = False
        '' ''            Exit Function
        '' ''        End If
        '' ''    End If
        '' ''End If
        '***********************

        If KeyAscii = 8 Then
            Exit Function
        End If

        If InStr(txt, ".") > 0 Then
            If Len(txt) >= MaxLength Then
                KeyAscii = 0
                GridValid_Number = False
                Exit Function
            End If

            'If Len(txt.Text) - InStr(txt.Text, ".") >= MaxDecimal And Len(Left(txt.Text, InStr(txt.Text, ".") - 1)) < MaxDigit And txt.SelectionStart >= InStr(txt.Text, ".") And KeyAscii <> 8 Then
            '    KeyAscii = 0
            '    GridValid_Number = False
            '    Exit Function
            'End If
            'If Len(txt.Text) - InStr(txt.Text, ".") < MaxDecimal And Len(Left(txt.Text, InStr(txt.Text, "."))) > MaxDigit And txt.SelectionStart < InStr(txt.Text, ".") And KeyAscii <> 8 Then
            '    KeyAscii = 0
            '    GridValid_Number = False
            '    Exit Function
            'End If

        Else
            If Len(txt) >= MaxDigit And Chr(KeyAscii) <> "." And KeyAscii <> 8 Then
                KeyAscii = 0
                GridValid_Number = False
            End If
        End If
        a = InStr("1234567890.", Chr(KeyAscii))
        If a <> 0 Then

            'if flag
            If (Chr(KeyAscii) = "." And InStr(txt, ".") <= 0) Or Chr(KeyAscii) <> "." Then
                'txt.Text = txt.Text + Chr(KeyAscii)
                flag = True
                GridValid_Number = True
            Else
                KeyAscii = 0
                GridValid_Number = False
            End If
        ElseIf KeyAscii <> 8 Then
            KeyAscii = 0
            GridValid_Number = False
            ''MsgBox "Keys (0-9) Allowed Only....", vbInformation, "Alchemist Ltd."
        End If

    End Function

    Public Function FillDataAdaptor(ByVal qry As String) As DataSet
        '' Common Function to open a DataSet
        Try
            da = New SqlDataAdapter(qry, con)
            'ds = New DataSet
            da.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error FillDataSet")
            Return ds
        Finally
            da.Dispose()
        End Try
    End Function

    Public Function getPrefixCode(ByVal fldname As String, ByVal tblname As String) As String
        Dim Prefix As String
        Dim sql As String
        sql = "SELECT " & fldname & " FROM " & tblname
        Prefix = ExecuteScalar(sql)
        'Prefix = Prefix & "" & Convert.ToString(getMaxValue("CostCenter_Id", "COST_CENTER_MASTER"))
        Return Prefix
    End Function

    Public Function get_record_count(ByVal qry As String) As Integer
        Try
            Dim ds_cnt As DataSet
            Dim rec_cnt As Integer
            ds_cnt = FillDataSet(qry)
            If ds_cnt.Tables(0).Rows.Count > 0 Then

                rec_cnt = ds_cnt.Tables(0).Rows.Count
            Else
                rec_cnt = 0
            End If
            get_record_count = rec_cnt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Recordcount")
        End Try
    End Function

    Public Sub ListControlBind(ByVal cnt As ListControl, ByVal qry As String, ByVal text As String, ByVal value As String)
        '' Common Function to Bind a List Control
        Try
            Dim ds As DataSet
            ds = FillDataSet(qry)
            cnt.ValueMember = value
            cnt.DisplayMember = text
            cnt.DataSource = ds.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ListControlBind")
        End Try
    End Sub

    Public Sub ComboBind(ByVal cnt As System.Windows.Forms.ComboBox, ByVal qry As String, ByVal text As String, ByVal value As String, Optional ByVal use_select As Boolean = False)
        '' Common Function to Bind a Combo Box
        Try
            Dim ds1 As DataSet
            ds1 = FillDataSet(qry)
            cnt.ValueMember = value
            cnt.DisplayMember = text
            'cnt.DropDownStyle = ComboBoxStyle.DropDown
            'cnt.AutoCompleteMode = AutoCompleteMode.Suggest
            'cnt.AutoCompleteSource = AutoCompleteSource.ListItems
            Dim dr As DataRow
            If ds1.Tables(0).Rows.Count > 0 Then
                If use_select Then
                    dr = ds1.Tables(0).NewRow
                    dr(value) = -1
                    dr(text) = "--Select--"
                    ds1.Tables(0).Rows.InsertAt(dr, 0)
                End If
            Else
                dr = ds1.Tables(0).NewRow
                dr(value) = 0
                'dr(text) = "--No Data Found--"
                dr(text) = "--Select--"
                ds1.Tables(0).Rows.Add(dr)
            End If
            cnt.DataSource = ds1.Tables(0)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
        End Try
    End Sub

    Public Sub ComboBindForPayment(ByVal cnt As System.Windows.Forms.ComboBox, ByVal qry As String, ByVal text As String, ByVal value As String, Optional ByVal use_select As Boolean = False)
        '' Common Function to Bind a Combo Box
        Try
            Dim ds1 As DataSet
            ds1 = FillDataSet(qry)
            cnt.ValueMember = value
            cnt.DisplayMember = text
            cnt.DropDownStyle = ComboBoxStyle.DropDown
            cnt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cnt.AutoCompleteSource = AutoCompleteSource.ListItems
            cnt.AllowDrop = True
            Dim dr As DataRow
            If ds1.Tables(0).Rows.Count > 0 Then
                If use_select Then
                    dr = ds1.Tables(0).NewRow
                    dr(value) = -1
                    dr(text) = "--Select--"
                    ds1.Tables(0).Rows.InsertAt(dr, 0)
                End If
            Else
                dr = ds1.Tables(0).NewRow
                dr(value) = 0
                dr(text) = "--No Data Found--"
                ds1.Tables(0).Rows.Add(dr)
            End If
            cnt.DataSource = ds1.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
        End Try
    End Sub

    Public Sub ComboBind_Remote(ByVal cnt As System.Windows.Forms.ComboBox, ByVal qry As String, ByVal text As String, ByVal value As String, Optional ByVal use_select As Boolean = False)
        '' Common Function to Bind a Combo Box
        Try
            Dim ds1 As DataSet
            ds1 = FillDataSet_Remote(qry)
            cnt.ValueMember = value
            cnt.DisplayMember = text
            Dim dr As DataRow
            If ds1.Tables(0).Rows.Count > 0 Then
                If use_select Then
                    dr = ds1.Tables(0).NewRow
                    dr(value) = -1
                    dr(text) = "--Select--"
                    ds1.Tables(0).Rows.InsertAt(dr, 0)
                End If
            Else
                dr = ds1.Tables(0).NewRow
                dr(value) = 0
                dr(text) = "--No Data Found--"
                ds1.Tables(0).Rows.Add(dr)
            End If
            cnt.DataSource = ds1.Tables(0)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
        End Try
    End Sub

    'Public Sub ComboBindNew(ByVal cnt As ComboBox, ByVal qry As String, ByVal text As String, ByVal value As String)
    '    '' Common Function to Bind a Combo Box
    '    Try
    '        Dim ds1 As DataSet
    '        ds1 = FillDataSet(qry)
    '        cnt.ValueMember = value
    '        cnt.DisplayMember = text
    '        cnt.DataSource = ds1.Tables(0)
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
    '    End Try
    'End Sub

    Public Sub ComboBindWithSP(ByVal cnt As ComboBox, ByVal sp As String, ByVal value As String, ByVal text As String, Optional ByVal use_select As Boolean = False)
        '' Common Function to Bind a Combo Box using store Proceduer 

        Try
            Dim ds1 As DataSet
            ds1 = fill_Data_sets(sp)
            cnt.ValueMember = value
            cnt.DisplayMember = text
            Dim dr As DataRow
            If ds1.Tables(0).Rows.Count > 0 Then
                If use_select Then
                    dr = ds1.Tables(0).NewRow
                    dr(value) = -1
                    dr(text) = "--Select--"
                    ds1.Tables(0).Rows.InsertAt(dr, 0)
                End If
            Else
                dr = ds1.Tables(0).NewRow
                dr(value) = 0
                dr(text) = "--No Data Found--"
                ds1.Tables(0).Rows.Add(dr)
            End If
            cnt.DataSource = ds1.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
        End Try
    End Sub

    Public Structure ObjectWithValue
        Dim str As String
        Dim strvalue As String
        Public Overrides Function ToString() As String
            Return str.ToString()

        End Function
    End Structure

    Public Sub GridBind(ByVal cnt As DataGridView, ByVal qry As String)
        '' Common Function to Bind a DataGrid
        Try
            Dim ds1 As DataSet
            ds1 = FillDataSet(qry)
            cnt.DataSource = ds1.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error GridBind")
        End Try
    End Sub

    Public Sub Grid_Bind(ByVal DGV As DataGridView, ByVal sp As String)
        '' Common Function to Bind a DataGrid
        Try
            Dim ds1 As DataSet
            ds1 = fill_Data_sets(sp)

            DGV.DataSource = ds1.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error GridBind")
        End Try
    End Sub

    Public Sub Bind_GridBind_Val(ByVal DGV As DataGridView, ByVal sp As String, ByVal para As String, ByVal para1 As String, ByVal parm_val As String, ByVal parm_val1 As String)
        Try
            Dim ds1 As DataSet
            ds1 = fill_Data_set_val(sp, para, para1, parm_val, parm_val1)
            DGV.DataSource = ds1.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error GridBind")
        Finally
            'cmd.Dispose()
        End Try

    End Sub

    Public Function ExecuteNonQuery(ByVal qry As String) As Int32
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trns = con.BeginTransaction
            cmd = New SqlCommand(qry, con)
            cmd.Transaction = trns
            ExecuteNonQuery = cmd.ExecuteNonQuery()
            trns.Commit()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ExecuteNonQuery")
            trns.Rollback()
            ExecuteNonQuery = 0
        Finally
            cmd.Dispose()
        End Try
    End Function

    Public Function ExecuteNonQueryWithoutTransaction(ByVal qry As String) As Int32
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd = New SqlCommand(qry, con)
            ExecuteNonQueryWithoutTransaction = cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ExecuteNonQuery")
            ExecuteNonQueryWithoutTransaction = 0
        Finally
            cmd.Dispose()
        End Try
    End Function

    Public Function ExecuteScalar(ByVal qry As String) As Object
        cmd = New SqlCommand(qry)
        cmd.CommandText = qry
        cmd.Connection = con

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim obj As Object
        obj = cmd.ExecuteScalar()
        con.Close()
        Return obj
    End Function

    Public Function ExecuteScalar(ByVal qry As String, ByVal conObject As SqlConnection) As Object
        cmd = New SqlCommand(qry)
        cmd.CommandText = qry
        cmd.Connection = conObject

        If conObject.State = ConnectionState.Closed Then
            conObject.Open()
        End If
        Dim obj As Object
        obj = cmd.ExecuteScalar()
        conObject.Close()
        Return obj
    End Function

    Public Function ExecuteScalarTrans(ByVal qry As String, ByVal cmd As SqlCommand) As Object
        'cmd = New SqlCommand(qry)
        cmd.CommandText = qry
        'cmd.Connection = con

        'If con.State = ConnectionState.Closed Then
        ' con.Open()
        'End If
        Dim obj As Object
        obj = cmd.ExecuteScalar()
        'con.Close()
        Return obj
    End Function

    Public Function KeyDownInteger(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If e.Shift Then
            Return False
        End If
        If (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or (e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9) Then
            Return True
        ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
            Return True
        End If
        Return False
    End Function

    Public Function KeyDownDecimal(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If e.Shift Then
            Return False
        End If
        If (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or (e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9) Then
            Return True
        ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
            Return True
        ElseIf e.KeyCode = Keys.Decimal Or e.KeyCode = Keys.OemPeriod Then
            If CType(sender, TextBox).Text.Contains(".") Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Public Function KeyDownString(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If e.Shift = True And e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9 Then
            Return True
        End If
        If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Then
            Return True
        ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Space Or e.KeyCode = Keys.Decimal Or e.KeyCode = Keys.OemPeriod Then
            Return True
        End If
    End Function

    Public Function KeyDownAlphabaticsOnly(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean

        If e.Shift = True And e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Then
            Return True
        ElseIf e.Shift = False And e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Then
            Return True
        ElseIf e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Space Then
            Return True
        ElseIf e.Shift = True Then
            If e.KeyValue = 95 Then
                Return True
            End If
        Else
            Return False
        End If

    End Function

    Public Sub TextBoxLeave(ByRef sender As System.Object, ByVal e As System.EventArgs)
        While CType(sender, TextBox).Text.Contains("  ")
            CType(sender, TextBox).Text = CType(sender, TextBox).Text.Replace("  ", " ")
        End While
        CType(sender, TextBox).Text = CType(sender, TextBox).Text.Trim
        CType(sender, TextBox).Text = StrConv(CType(sender, TextBox).Text, VbStrConv.Uppercase)
    End Sub

    Public Function EXECUTEREADER(ByVal S As String)
        Try
            cmd = New SqlCommand(S)
            If con.State = ConnectionState.Closed Then con.Open()
            cmd.Connection = con
            dr = cmd.ExecuteReader()

            Return dr
        Catch ex As Exception
            MsgBox(ex.Message)
            Return dr
        Finally
            cmd.Dispose()
        End Try
    End Function

    Public Sub clearme(ByVal cc As Control.ControlCollection)
        For Each C As Control In cc
            If TypeOf C Is TextBox Then
                CType(C, TextBox).Text = ""
            End If

            If TypeOf C Is ListBox Then
                CType(C, ListBox).Items.Clear()
            End If

            'If TypeOf C Is System.Windows.Forms.ComboBox Then

            '    CType(C, System.Windows.Forms.ComboBox).Items.Clear()
            '    CType(C, System.Windows.Forms.ComboBox).DataBindings.Clear()
            '    CType(C, System.Windows.Forms.ComboBox).DataSource = Nothing
            'End If

            If TypeOf C Is CheckBox Then
                CType(C, CheckBox).Checked = False
            End If

            If TypeOf C Is RadioButton Then
                CType(C, RadioButton).Checked = False
            End If

            If TypeOf C Is DateTimePicker Then
                CType(C, DateTimePicker).Checked = False
            End If
        Next C
    End Sub

    Public Sub Clear_All_TextBox(ByVal cc As Control.ControlCollection)
        For Each C As Control In cc
            If TypeOf C Is TextBox Then
                CType(C, TextBox).Text = ""
            End If
        Next C
    End Sub

    Public Sub Clear_All_CheckBox(ByVal cc As Control.ControlCollection)
        For Each c As Control In cc
            If TypeOf c Is CheckBox Then
                CType(c, CheckBox).Checked = False
            End If
        Next
    End Sub

    Public Sub Clear_All_ListBox(ByVal cc As Control.ControlCollection)
        For Each c As Control In cc
            If TypeOf c Is ListBox Then
                CType(c, ListBox).Items.Clear()
            End If
        Next
    End Sub

    Public Sub Clear_All_DTPicker(ByVal cc As Control.ControlCollection)
        For Each c As Control In cc
            If TypeOf c Is DateTimePicker Then
                CType(c, DateTimePicker).Value = Now.Date
            End If
        Next
    End Sub

    Public Sub Clear_All_ComoBox(ByVal cc As Control.ControlCollection)
        For Each c As Control In cc
            If TypeOf c Is System.Windows.Forms.ComboBox Then
                CType(c, System.Windows.Forms.ComboBox).SelectedIndex = 0
            End If
        Next
    End Sub

    Public Sub disableme(ByVal Cc As Control.ControlCollection)
        For Each c As Control In Cc
            If TypeOf c Is TextBox Then
                CType(c, TextBox).Enabled = False
            End If

            If TypeOf c Is ListBox Then
                CType(c, ListBox).Enabled = False
            End If

            If TypeOf c Is System.Windows.Forms.ComboBox Then
                CType(c, System.Windows.Forms.ComboBox).Enabled = False
            End If

            If TypeOf c Is CheckBox Then
                CType(c, CheckBox).Enabled = False
            End If

            If TypeOf c Is RadioButton Then
                CType(c, RadioButton).Enabled = False
            End If

            If TypeOf c Is DateTimePicker Then
                CType(c, DateTimePicker).Enabled = False
            End If
        Next c
    End Sub

    Public Sub enableme(ByVal cc As Control.ControlCollection)
        For Each c As Control In cc
            If TypeOf c Is TextBox Then
                CType(c, TextBox).Enabled = True
            End If

            If TypeOf c Is ListBox Then
                CType(c, ListBox).Enabled = True
            End If

            If TypeOf c Is System.Windows.Forms.ComboBox Then
                CType(c, System.Windows.Forms.ComboBox).Enabled = True
            End If

            If TypeOf c Is CheckBox Then
                CType(c, CheckBox).Enabled = True
            End If

            If TypeOf c Is RadioButton Then
                CType(c, RadioButton).Enabled = True
            End If

            If TypeOf c Is DateTimePicker Then
                CType(c, DateTimePicker).Enabled = True
            End If
        Next c
    End Sub

    Public Function AllCaps(ByRef KeyAscii As Integer) As Integer
        If (KeyAscii >= 97 And KeyAscii <= 122) Then
            KeyAscii = KeyAscii - 32
        End If

        Return KeyAscii
    End Function

    Public Function Numeric(ByVal str As String) As Boolean
        If IsNumeric(str) = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function NumericOnly(ByRef KeyAscii As Integer) As Integer
        If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 8 And KeyAscii <> 13 And KeyAscii <> 27 Then
            KeyAscii = 0
            Return KeyAscii
        Else
            Return KeyAscii
        End If
    End Function

    Public Function DecimalOnly(ByRef KeyAscii As Integer) As Integer
        If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 8 And KeyAscii <> 13 And KeyAscii <> 27 And KeyAscii <> 46 Then
            KeyAscii = 0
            Return KeyAscii
        Else
            Return KeyAscii
        End If
    End Function

    Public Function phonenumber(ByRef KeyAscii As Integer) As Integer
        If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 8 And KeyAscii <> 43 And KeyAscii <> 45 And KeyAscii <> 47 And KeyAscii <> 13 And KeyAscii <> 27 Then
            KeyAscii = 0
            Return KeyAscii
        Else
            Return KeyAscii
        End If
    End Function

    Public Function Validate_TextBoxes(ByVal cc As Control.ControlCollection) As Boolean
        Dim ret As Boolean
        ret = True
        For Each C As Control In cc
            If TypeOf C Is TextBox Then
                If CType(C, TextBox).Text = "" Then
                    ret = False
                End If
            End If
        Next C
        Return ret
    End Function

    Public Function Get_Form_Rights(ByVal form_name As String) As Form_Rights
        Dim p As New Form_Rights
        Try
            cmd = New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from user_rights where form_name = '" & form_name & "' and USER_id = " & v_the_current_logged_in_user_id
            cmd.Connection = con
            If con.State = ConnectionState.Closed Then con.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                p.Has_rights = "Y"
                p.allow_view = dr("allow_view")
                p.allow_trans = dr("allow_trans")
                p.allow_edit = dr("allow_edit")
                p.allow_cancel = dr("allow_cancel")
                con.Close()
                cmd.Dispose()
            Else
                p.Has_rights = "N"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Get_Form_Rights")
        Finally
            dr.Close()
        End Try
        Return p
    End Function

    Public Sub FormatGrid(ByVal grd As DataGridView)
        grd.BackgroundColor = Color.White
        grd.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
        grd.RowsDefaultCellStyle.SelectionForeColor = Color.Blue
        grd.RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray
        grd.RowHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue
        grd.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black
        grd.RowHeadersWidth = 20
        grd.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        grd.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue
        grd.AllowUserToResizeColumns = True
        grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grd.MultiSelect = False
    End Sub

    Public Sub FormatGrid(ByVal grd As C1.Win.C1FlexGrid.C1FlexGrid)
        grd.BackColor = Color.White
        grd.Styles.EmptyArea.BackColor = Color.DimGray
        grd.Styles.Fixed.BackColor = Color.LightSteelBlue
        grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grd.Tree.LineColor = Color.Blue
        grd.Tree.LineStyle = Drawing2D.DashStyle.Solid
    End Sub

    Public Function CheckTANNo(ByVal tan As String) As Boolean
        ''''''''tan no->ABCD12345E
        Dim regx_tan As New System.Text.RegularExpressions.Regex("([a-zA-Z]{4})([0123456789]{5})([a-zA-Z]{1})")
        Return regx_tan.IsMatch(tan)
    End Function

    Public Function CheckPANNo(ByVal pan As String) As Boolean
        ''''''''pan no->ABCDE1234F
        If (pan = "") Then
            Return True
        Else
            Dim regx_pan As New System.Text.RegularExpressions.Regex("([a-zA-Z]{5})([0123456789]{4})([a-zA-Z]{1})")
            Return regx_pan.IsMatch(pan)
        End If
    End Function

    Public Function CheckPincode(ByVal pincode As String) As Boolean
        ''''''''pincode->123456
        Dim regx_pincode As New System.Text.RegularExpressions.Regex("([0123456789]{6})")
        Return regx_pincode.IsMatch(pincode)
    End Function

    Public Function CheckEmail(ByVal email As String) As Boolean
        ''''''''email->abc@yahoo.com
        Dim regx_email As New System.Text.RegularExpressions.Regex("([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})")
        Return regx_email.IsMatch(email)
    End Function

    Public Function getMaxValue(ByVal fldname As String, ByVal tblname As String) As Integer
        Dim sql As String
        sql = " Select isnull(max(" & fldname & "),0) + 1  from " & tblname
        getMaxValue = Convert.ToInt32(ExecuteScalar(sql))
    End Function

    Public Function getMaxValueTrans(ByVal fldname As String, ByVal tblname As String, ByVal cmd As SqlCommand) As Integer
        Dim sql As String
        sql = " Select isnull(max(" & fldname & "),0) + 1  from " & tblname
        getMaxValueTrans = Convert.ToInt32(ExecuteScalarTrans(sql, cmd))
    End Function

    Public Function Check_Duplicate_Field(ByVal tblname As String, ByVal fldname As String, ByVal valStr As String) As Boolean
        Dim sql As String
        Dim ds_field As DataSet
        Try
            sql = " Select * from " & tblname & " Where " & fldname & "='" & valStr & "' and comp_id = " & v_the_current_division_id
            ds_field = Fill_DataSet(sql)
            If ds_field.Tables(0).Rows.Count > 0 Then
                Check_Duplicate_Field = True
            Else
                Check_Duplicate_Field = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function AutoGenerateCode(ByVal id As String, ByVal tbl_name As String, ByVal CodeName As String) As String
        Dim sql As String
        Dim ds_code As DataSet
        Dim ds_max As DataSet
        sql = " Select codeYesNo from tb_comp_Master Where comp_id = " & v_the_current_division_id
        ds_code = Fill_DataSet(sql)
        If ds_code.Tables(0).Rows.Count > 0 Then
            If ds_code.Tables(0).Rows(0)(0) = "Y" Then
                sql = " select isnull(max(" & id & "),0) + 1 as maxID from " & tbl_name & " Where comp_id = " & v_the_current_division_id
                ds_max = Fill_DataSet(sql)
                AutoGenerateCode = CodeName & "_" & ds.Tables(0).Rows(0)(0)
            Else
                AutoGenerateCode = ""
                Exit Function
            End If
        End If
        AutoGenerateCode = ""
        Exit Function
    End Function
End Class

Public Class Form_Rights

#Region "Property Rights"

    Private pallow_view As System.String

    Public Property allow_view() As System.String
        Get
            Return pallow_view
        End Get
        Set(ByVal value As System.String)
            pallow_view = value
        End Set
    End Property

    Private pallow_trans As System.String
    Public Property allow_trans() As System.String
        Get
            Return pallow_trans
        End Get

        Set(ByVal value As System.String)
            pallow_trans = value
        End Set
    End Property


    Private pallow_edit As System.String
    Public Property allow_edit() As System.String
        Get
            Return pallow_edit
        End Get

        Set(ByVal value As System.String)
            pallow_edit = value
        End Set
    End Property


    Private pallow_cancel As System.String
    Public Property allow_cancel() As System.String
        Get
            Return pallow_cancel
        End Get

        Set(ByVal value As System.String)
            pallow_cancel = value
        End Set
    End Property


    'Private pallow_delete As System.String
    'Public Property allow_delete() As System.String
    '    Get
    '        Return pallow_delete
    '    End Get

    '    Set(ByVal value As System.String)
    '        pallow_delete = value
    '    End Set
    'End Property

    'Private pallow_update As System.String
    'Public Property allow_update() As System.String
    '    Get
    '        Return pallow_update
    '    End Get

    '    Set(ByVal value As System.String)
    '        pallow_update = value
    '    End Set
    'End Property
    Private pHas_rights As System.String
    Public Property Has_rights() As System.String
        Get
            Return pHas_rights
        End Get

        Set(ByVal value As System.String)
            pHas_rights = value
        End Set
    End Property

#End Region
End Class

Public MustInherit Class Connection

    Protected con As SqlConnection
    Protected da As SqlDataAdapter
    Protected ds As DataSet
    Protected dt As DataTable
    Protected cmd As SqlCommand
    'Protected dr As OdbcDataReader
    Protected dr As SqlDataReader
    'Protected trns As OdbcTransaction

    Protected trns As SqlTransaction

    Private Sub OpenConnectionFile()
        Try


            'Dim sr As System.IO.StreamReader
            'Dim str As String
            'str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\afbsl_mms\connection.txt"
            'If System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\connection.txt") Then
            'sr = New System.IO.StreamReader(Application.StartupPath & "\connection.txt")
            '' ''If System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\afbsl_mms\connection.txt") Then
            '' ''    '         MsgBox(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData))
            '' ''    sr = New System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\afbsl_mms\connection.txt")
            '' ''    DNS = sr.ReadToEnd
            '' ''    sr.Close()
            '' ''Else
            '' ''    MsgBox("connection file does not exist")
            '' ''    frm_create_connection_file.ShowDialog()
            '' ''End If

            If System.IO.File.Exists(Application.StartupPath & "\connection.xml") Then
                Dim settings As New XmlReaderSettings()
                settings.IgnoreComments = True

                Dim xmlRdr As XmlReader = XmlReader.Create(Application.StartupPath & "\connection.xml", settings)

                While xmlRdr.Read
                    If xmlRdr.NodeType = XmlNodeType.Element And xmlRdr.Name = "LocalConnection" Then
                        DNS = "Server =" & xmlRdr.GetAttribute("ServerName") & ";Database =" &
                            xmlRdr.GetAttribute("DataBase") & ";User ID =" & xmlRdr.GetAttribute("UserName") & ";Password =" &
                            Decrypt(xmlRdr.GetAttribute("Password")) & ";Connection Timeout =" & xmlRdr.GetAttribute("Timeout")


                        gblDataBase_UserName = xmlRdr.GetAttribute("UserName")
                        gblDataBase_Password = Decrypt(xmlRdr.GetAttribute("Password"))
                        gblDataBase_Name = xmlRdr.GetAttribute("DataBase")

                    ElseIf xmlRdr.NodeType = XmlNodeType.Element And xmlRdr.Name = "CentraliseConnection" Then
                        gblDNS_Online = "Server =" & xmlRdr.GetAttribute("ServerName") & ";Database =" &
                                                    xmlRdr.GetAttribute("DataBase") & ";User ID =" & xmlRdr.GetAttribute("UserName") & ";Password =" &
                                                    Decrypt(xmlRdr.GetAttribute("Password")) & ";Connection Timeout =" & xmlRdr.GetAttribute("Timeout")

                        gblCentraliseServer_Name = xmlRdr.GetAttribute("ServerName")
                        gblCentraliseDataBase_Name = xmlRdr.GetAttribute("DataBase")

                    ElseIf xmlRdr.NodeType = XmlNodeType.Element And xmlRdr.Name = "CentraliseConnectionMMS" Then
                        gblDNS_OnlineMMS = "Server =" & xmlRdr.GetAttribute("ServerName") & ";Database =" &
                                                    xmlRdr.GetAttribute("DataBase") & ";User ID =" & xmlRdr.GetAttribute("UserName") & ";Password =" &
                                                    Decrypt(xmlRdr.GetAttribute("Password")) & ";Connection Timeout =" & xmlRdr.GetAttribute("Timeout")

                    ElseIf xmlRdr.NodeType = XmlNodeType.Element And xmlRdr.Name = "ConnectionStrings" Then
                        If xmlRdr.GetAttribute("DivisionType").ToUpper = "WAREHOUSE" Then
                            v_division_type = division_type.Warehouse
                        Else
                            v_division_type = division_type.Resturant
                        End If
                    End If
                End While

            Else
                frm_create_connection_file.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> OpenConnectionFile")
        End Try
    End Sub

    Public Sub New()
        'creating new instance of connect 
        OpenConnectionFile()
        Try
            If con Is Nothing Then
                con = New SqlConnection(DNS)
            End If
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Class Connection")
        End Try
    End Sub

    Public Function MyCon_BeginTransaction() As SqlCommand
        If G_MyConTransaction = False Then

            OpenConnectionFile()
            Dim conn As New SqlConnection(DNS)
            Dim cmd As New SqlCommand
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.Transaction = conn.BeginTransaction
            cmd.Connection = conn
            cmd.CommandTimeout = 1000
            G_MyConTransaction = True
            Return cmd
        Else
            Return Nothing
        End If
    End Function

    Public Sub MyCon_RollBackTransaction(ByVal cmd As SqlCommand)
        If G_MyConTransaction = True Then
            cmd.Transaction.Rollback()
            cmd.Dispose()
            G_MyConTransaction = False
        End If
    End Sub

    Public Sub MyCon_CommitTransaction(ByVal cmd As SqlCommand)
        If G_MyConTransaction = True Then
            cmd.Transaction.Commit()
            cmd.Dispose()
            G_MyConTransaction = False
        End If
    End Sub

    Public Function Fill_DataSet(ByVal qry As String) As DataSet

        Try
            da = New SqlDataAdapter(qry, con)
            da.SelectCommand.CommandTimeout = 1000
            ds = New DataSet
            da.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> Fill DataSet")
            Return Nothing
        Finally
            da.Dispose()
        End Try

    End Function

    Public Function fill_Data_sets(ByVal sp As String) As DataSet
        Try
            Dim adp As New SqlDataAdapter(sp, con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.CommandTimeout = 0
            Dim ds As New DataSet()
            adp.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> Fill DataSet")
            Return ds
        Finally
            'ds.Dispose()
        End Try
    End Function

    Public Function fill_Data_set(ByVal sp As String, ByVal parameters As String, ByVal values As String) As DataSet
        Try
            Dim adp As New SqlDataAdapter(sp, con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.CommandTimeout = 0
            Dim param As String()
            Dim value As String()
            Dim sepr As Char() = New Char(1) {}

            sepr(0) = ","c
            param = parameters.Split(sepr)
            value = values.Split(sepr)
            Dim i As Int32
            For i = 0 To param.Length - 1
                adp.SelectCommand.Parameters.AddWithValue(param(i), value(i))
            Next i
            Dim ds As New DataSet()
            adp.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
            'Return ds
        Finally
            'ds.Dispose()
        End Try
        Return Nothing
    End Function

    Public Function fill_Data_set_val(ByVal sp As String, ByVal p1 As String, ByVal p2 As String, ByVal v1 As String, ByVal v2 As String) As DataSet
        Try
            Dim adp As New SqlDataAdapter(sp, con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.CommandTimeout = 0
            If Not String.IsNullOrEmpty(p1.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p1, v1)
            End If
            If Not String.IsNullOrEmpty(p2.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p2, v2)
            End If
            Dim ds As New DataSet()
            adp.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> Fill DataSet ")
            Return ds
        Finally
            'ds.Dispose()
        End Try
    End Function

    Public Function fill_Data_set_val(ByVal sp As String, ByVal p1 As String, ByVal p2 As String, ByVal p3 As String, ByVal p4 As String, ByVal p5 As String, ByVal v1 As String, ByVal v2 As String, ByVal v3 As String, ByVal v4 As String, ByVal v5 As String) As DataSet
        Try
            Dim adp As New SqlDataAdapter(sp, con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.CommandTimeout = 0
            If Not String.IsNullOrEmpty(p1.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p1, v1)
            End If
            If Not String.IsNullOrEmpty(p2.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p2, v2)
            End If
            If Not String.IsNullOrEmpty(p3.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p3, v3)
            End If
            If Not String.IsNullOrEmpty(p4.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p4, v4)
            End If
            If Not String.IsNullOrEmpty(p5.ToString()) Then
                adp.SelectCommand.Parameters.AddWithValue(p5, v5)
            End If
            Dim ds As New DataSet()
            adp.Fill(ds)
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> Fill DataSet ")
            Return ds
        Finally
            'ds.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Function AllNums(ByVal x As Integer) As Boolean
        Select Case x
            Case 8, 32, 48 To 57
                AllNums = False
            Case Else
                AllNums = True
        End Select
    End Function


    ''' <summary>
    ''' Function To Check Null Values
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="blnTrim"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Function NZ(ByVal str As Object, Optional ByVal blnTrim As Boolean = True) As String

        If str Is Nothing OrElse IsDBNull(str) Then
            Return ""
        Else
            If blnTrim = False Then
                Return str
            Else
                Return LTrim(RTrim(str))
            End If
        End If

    End Function

    Public Function GetDate(ByVal Dt As Date)
        Dim gDate As Date
        Dim qry As String
        qry = "Select Replace(Convert(varchar," & Dt & ",106),' ','-')"
        Dim cmd As New SqlCommand(qry, con)
        cmd.ExecuteNonQuery()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            gDate = dr(0)
        End If
        Dt = gDate
        Return Dt
    End Function

    Public Sub RemoveRowsFromGrid(ByVal Grd As DataGridView, ByVal Rows As Integer)
        For i As Integer = Rows To 1 Step -1
            Grd.Rows.RemoveAt(i - 1)
        Next
    End Sub

    Public Function ValidatingDGV(ByVal Grd As DataGridView, ByVal Names() As String) As Boolean
        ValidatingDGV = True
        For x As Integer = 0 To Names.Length - 1
            For y As Integer = 0 To Grd.Rows.Count - 2
                Dim val As String = CStr(Grd(Names(x), y).Value)
                If String.IsNullOrEmpty(val) Then
                    Grd.SelectionMode = DataGridViewSelectionMode.CellSelect
                    Grd.CurrentCell = Grd.Item(Names(x), Grd.CurrentCell.RowIndex) ''Column Select 
                    ValidatingDGV = False
                    Exit Function
                End If
            Next
        Next
    End Function

    Public Function Validation(ByVal cc As Control.ControlCollection) As Boolean
        Validation = True
        For Each c As Control In cc
            If TypeOf c Is TextBox Then
                If Trim(CType(c, TextBox).Text) = "" Then
                    Validation = False
                    Exit Function
                End If
            End If
        Next
    End Function

    ''' <summary>
    ''' Validation For Component One FlexGrid  Check Null Values
    ''' </summary>
    ''' <param name="Grd"></param>
    ''' <param name="Names"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 


    Public Function C1ValidatingDGV(ByVal Grd As C1.Win.C1FlexGrid.C1FlexGrid, ByVal Names() As String) As Boolean
        C1ValidatingDGV = True
        For x As Integer = 0 To Names.Length - 1
            For y As Integer = 1 To Grd.Rows.Count - 2
                Dim val As String = CStr(Grd.Item(y, Names(x))) ', y).Value)
                If String.IsNullOrEmpty(val) Then
                    'Grd.SelectionMode = DataGridViewSelectionMode.CellSelect
                    'Grd.CurrentCell = Grd.Item(Names(x), Grd.CurrentCell.RowIndex) ''Column Select 
                    '' Grd(Names(x), y).Style.BackColor = Color.DimGray
                    '' Grd.BeginEdit(True) Highlight Of cursor
                    C1ValidatingDGV = False
                    Exit Function
                End If
            Next
        Next
    End Function

    ''' <summary>
    ''' Report Call Procedure
    ''' </summary>
    ''' <param name="RptNo"></param>
    ''' <param name="Parameters"></param>
    ''' <param name="values"></param>
    ''' <param name="DType"></param>
    ''' <remarks></remarks>
    ''' 

    Public Sub RptShow(ByVal RptNo As Integer, ByVal Parameters As String, ByVal values As String, ByVal DType As String)
        Try
            Dim ds As New DataSet
            Dim filepath As String = ""
            Dim dsSubQry As New DataSet
            Dim ArrIndex As Integer
            Dim PInteger As Integer
            Dim PDate As Date = Now
            Dim PString As String = ""
            Dim rep As New ReportDocument()
            Dim param As String()
            Dim value As String()
            Dim ValueType As String()
            Dim sepr As Char() = New Char(0) {}
            sepr(0) = ","c
            param = Parameters.Split(sepr)
            value = values.Split(sepr)
            ValueType = DType.Split(sepr)
            If RptNo = enmReportName.RptMRSMainStorePrint Then
                filepath = ReportFilePath & "cryMRSMainstorePrint.rpt"
            ElseIf RptNo = enmReportName.RptPurchaseOrderPrint Then
                filepath = ReportFilePath & "cryPurchaseOrderPrint.rpt"
            ElseIf RptNo = enmReportName.RptSupplierRateList Then
                filepath = ReportFilePath & "CrySupplierRateList.rpt"
            ElseIf RptNo = enmReportName.RptWastagePrint Then
                filepath = ReportFilePath & "cryWastagePrint.rpt"
            ElseIf RptNo = enmReportName.RptWastagePrint_cc Then
                filepath = ReportFilePath & "cryWastagePrint_cc.rpt"
            ElseIf RptNo = enmReportName.RptRevWastagePrint Then
                filepath = ReportFilePath & "cryRevWastagePrint.rpt"
            ElseIf RptNo = enmReportName.RptOpenPurchaseOrderPrint Then
                filepath = ReportFilePath & "cryOpenPurchaseOrderPrint.rpt"
            ElseIf RptNo = enmReportName.RptIndentDetailPrint Then
                filepath = ReportFilePath & "cryIndentDetailPrint.rpt"
            ElseIf RptNo = enmReportName.RptRevMICostCenterPrint Then
                filepath = ReportFilePath & "cryRevMICostCenterPrint.rpt"
            ElseIf RptNo = enmReportName.RptMatIssueToCostCenterPrint Then
                filepath = ReportFilePath & "cryMaterialIssueCostCenterPrint.rpt"
            ElseIf RptNo = enmReportName.RptMRNWithoutPOPrint Then
                filepath = ReportFilePath & "cryMRNWithOutPOPrint.rpt"
            ElseIf RptNo = enmReportName.RptMRNWithoutPOPrint_NonAFBL Then
                filepath = ReportFilePath & "cryMRNWithOutPOPrint_NonAFBL.rpt"
            ElseIf RptNo = enmReportName.RptMaterialRecAgainstPOPrintRevised Then
                filepath = ReportFilePath & "cryMaterialRecAgainstPOPrintREvised.rpt"
            ElseIf RptNo = enmReportName.RptMaterialRecAgainstPOPrint Then
                filepath = ReportFilePath & "cryMaterialRecAgainstPOPrint.rpt"
            ElseIf RptNo = enmReportName.RptReverseMaterialWithOutPO Then
                filepath = ReportFilePath & "cryRevMaterialDetail(without_po).rpt"
            ElseIf RptNo = enmReportName.RptMRNActualWithoutPOPrint Then
                filepath = ReportFilePath & "CryMRNNewWithOutPO.rpt"
            ElseIf RptNo = enmReportName.RptClosingStockPrint Then
                filepath = ReportFilePath & "cryClosingStockPrint.rpt"
            ElseIf RptNo = enmReportName.RptStockTransferCCPrint Then
                filepath = ReportFilePath & "cryStockTransferCCPrint.rpt"
            ElseIf RptNo = enmReportName.RptAcceptStockCCPrint Then
                filepath = ReportFilePath & "cryStockAcceptCCPrint.rpt"
            ElseIf RptNo = enmReportName.RptStock_Adjustment Then
                filepath = ReportFilePath & "cryAdjustmentPrint.rpt"
            ElseIf RptNo = enmReportName.RptReverseMaterialAgainstPO Then
                filepath = ReportFilePath & "CryReverseMaterialReceivedAgainstPOMaster.rpt"
            ElseIf RptNo = enmReportName.RptInvoicePrint Then
                filepath = ReportFilePath & "cry_Sale_Invoice.rpt"
            ElseIf RptNo = enmReportName.RptDCInvoicePrint Then
                filepath = ReportFilePath & "cry_DC_Sale_Invoice.rpt"

            ElseIf RptNo = enmReportName.RptCustomerRateList Then
                filepath = ReportFilePath & "CryCustomerrateList.rpt"

            ElseIf RptNo = enmReportName.RptDebitNotePrint Then
                filepath = ReportFilePath & "Cry_Debit_Note.rpt"

            ElseIf RptNo = enmReportName.RptDebitNoteWOItemPrint Then
                filepath = ReportFilePath & "Cry_Debit_Note_WO_Item.rpt"

            ElseIf RptNo = enmReportName.RptCreditNotePrint Then
                filepath = ReportFilePath & "Cry_Credit_Note.rpt"

            ElseIf RptNo = enmReportName.RptCreditNoteWOItemPrint Then
                filepath = ReportFilePath & "Cry_Credit_Note_WO_Item.rpt"

            ElseIf RptNo = enmReportName.RptGatePassPrint Then
                filepath = ReportFilePath & "CryGatePass.rpt"

            ElseIf RptNo = enmReportName.RptPaymentPrint Then
                filepath = ReportFilePath & "PaymentDetailCrReport.rpt"

            ElseIf RptNo = enmReportName.RptDeliveryNotePrint Then
                filepath = ReportFilePath & "Cry_DeliveryNote.rpt"

            ElseIf RptNo = enmReportName.RptSuppPaymentPrint Then
                filepath = ReportFilePath & "SuppPaymentDetailCrReport.rpt"

            ElseIf RptNo = enmReportName.RptAccPaymentPrint Then
                filepath = ReportFilePath & "PaymentVoucher.rpt"

            End If

            Dim callType As Integer = 0

            rep.Load(filepath)
            'Dim rep As New MyReportDocument(filepath)

            For ArrIndex = 0 To param.Length - 1
                If CInt(ValueType(ArrIndex)) = enmDataType.D_int Then
                    If RptNo = enmReportName.RptMRSMainStorePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("mrs_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptPurchaseOrderPrint Or RptNo = enmReportName.RptOpenPurchaseOrderPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("PO_ID", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptReverseMaterialAgainstPO Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Reverse_Id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptWastagePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("wastage_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptWastagePrint_cc Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("wastage_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptRevWastagePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("rwastage_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptIndentDetailPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("indent_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptRevMICostCenterPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("rmio_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptMatIssueToCostCenterPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("mio_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptMaterialRecAgainstPOPrintRevised Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("rec_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptMaterialRecAgainstPOPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("rec_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptMRNWithoutPOPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Received_ID", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptMRNWithoutPOPrint_NonAFBL Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Received_ID", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptMRNActualWithoutPOPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("rec_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptReverseMaterialWithOutPO Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Reverse_ID", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptClosingStockPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("closing_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptStockTransferCCPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Transfer_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptAcceptStockCCPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Transfer_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptStock_Adjustment Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("Adjustment_id", CInt(value(ArrIndex)))
                    ElseIf RptNo = enmReportName.RptSupplierRateList Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("SRL_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptCustomerRateList Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("SRL_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptDebitNotePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("DN_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptDebitNoteWOItemPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("DN_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptCreditNotePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("CN_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptCreditNoteWOItemPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("CN_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptGatePassPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("GatePassId", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptPaymentPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("PaymentId", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptSuppPaymentPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("PaymentId", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptAccPaymentPrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("PaymentId", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptDeliveryNotePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("TRANSFERID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptInvoicePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("SI_ID", CInt(value(ArrIndex)))

                    ElseIf RptNo = enmReportName.RptDCInvoicePrint Then
                        PInteger = CInt(value(ArrIndex))
                        rep.SetParameterValue("SI_ID", CInt(value(ArrIndex)))
                    End If



                    'ElseIf CInt(ValueType(ArrIndex)) = enmDataType.D_int Then
                    '   PInteger = CInt(value(ArrIndex))
                    '  rep.SetParameterValue("PO_ID", CInt(value(ArrIndex)))
                ElseIf CInt(ValueType(ArrIndex)) = enmDataType.D_String Then
                    rep.SetParameterValue(param(ArrIndex), value(ArrIndex))
                ElseIf CInt(ValueType(ArrIndex)) = enmDataType.D_Date Then
                    rep.SetParameterValue(param(ArrIndex), CDate(value(ArrIndex)))
                End If
            Next


            'rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "mmsplus", True)

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
            If RptNo = enmReportName.RptMRSMainStorePrint Then
                frm_Report.Text = "MRS Mainstore"
            ElseIf RptNo = enmReportName.RptPurchaseOrderPrint Then
                frm_Report.Text = "Purchase Order"
            ElseIf RptNo = enmReportName.RptSupplierRateList Then
                frm_Report.Text = "Supplier Rate List"
            ElseIf RptNo = enmReportName.RptWastagePrint Then
                frm_Report.Text = "Wastage Detail"
            ElseIf RptNo = enmReportName.RptRevWastagePrint Then
                frm_Report.Text = "Reverse Wastage Detail"
            ElseIf RptNo = enmReportName.RptOpenPurchaseOrderPrint Then
                frm_Report.Text = "Open Purchase Order"
            ElseIf RptNo = enmReportName.RptIndentDetailPrint Then
                frm_Report.Text = "Indent Detail"
            ElseIf RptNo = enmReportName.RptRevMICostCenterPrint Then
                frm_Report.Text = "Reverse Material Issue To Cost Center Detail"
            ElseIf RptNo = enmReportName.RptMatIssueToCostCenterPrint Then
                frm_Report.Text = "Material Issue To Cost Center Detail"
            ElseIf RptNo = enmReportName.RptMRNWithoutPOPrint Then
                frm_Report.Text = "Material Received Without PO Master"

            ElseIf RptNo = enmReportName.RptMaterialRecAgainstPOPrintRevised Or RptNo = enmReportName.RptMaterialRecAgainstPOPrint Then
                frm_Report.Text = "Material Received Against Purchase Order"
            ElseIf RptNo = enmReportName.RptMRNActualWithoutPOPrint Then
                frm_Report.Text = "Actual Material Received Without PO Master"
            Else
                frm_Report.Text = "Reports"
            End If
            frm_Report.cryViewer.ReportSource = rep
            frm_Report.cryViewer.Refresh()
            'frm_Report.cryViewer.ParameterFieldInfo()

            frm_Report.Show()
            'cryMain.ReportSource = rep
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try 'cryMain.Show()
    End Sub


    Private Sub AssignTableConnection(ByVal table As CrystalDecisions.CrystalReports.Engine.Table, ByVal connection As ConnectionInfo)
        ' Cache the logon info block
        Dim logOnInfo As TableLogOnInfo = table.LogOnInfo

        connection.Type = logOnInfo.ConnectionInfo.Type

        ' Set the connection
        logOnInfo.ConnectionInfo = connection

        ' Apply the connection to the table!

        table.LogOnInfo.ConnectionInfo.DatabaseName = connection.DatabaseName
        table.LogOnInfo.ConnectionInfo.ServerName = connection.ServerName
        table.LogOnInfo.ConnectionInfo.UserID = connection.UserID
        table.LogOnInfo.ConnectionInfo.Password = connection.Password
        table.LogOnInfo.ConnectionInfo.Type = connection.Type
        table.ApplyLogOnInfo(logOnInfo)
    End Sub


    Public Sub RemoveBlankRow(ByRef dtable As DataTable, ByVal fld_name As String)
        Dim i As Integer
again:
        For i = 0 To dtable.Rows.Count - 1
            If dtable.Rows(i)(fld_name).ToString = "" Then
                dtable.Rows.RemoveAt(i)
                GoTo again
            End If
        Next
    End Sub

    Public Sub ComboBind_Enum(ByVal cmb As ComboBox, ByVal enm As [Enum])
        Dim Names() As String = [Enum].GetNames(enm.GetType())
        Dim Values1 As Array = [Enum].GetValues(enm.GetType())
        cmb.Items.Clear()
        cmb.DataSource = Values1
    End Sub

    Public Sub ExportGridToExcel(dgv As DataGridView)

        Dim sfd As New SaveFileDialog
        sfd.CheckFileExists = False
        If sfd.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If


        'Dim APP As New Excel.Application
        'Dim worksheet As Excel.Worksheet
        'Dim workbook As Excel.Workbook

        Dim APP As Object 'Excel.Application
        Dim worksheet As Object 'Excel.Workbook
        Dim workbook As Object 'Excel.Workbook

        APP = CreateObject("Excel.Application")

        Dim excelLocation As String = sfd.FileName

        workbook = APP.Workbooks.Add(System.Reflection.Missing.Value)
        worksheet = workbook.Sheets("sheet1")
        'Excel.Range("A50:I50").EntireColumn.AutoFit()
        With workbook
            .Sheets("Sheet1").Select()
            .Sheets(1).Name = "TAX Report"
        End With

        'Export Header Names Start
        Dim columnsCount As Integer = dgv.Columns.Count
        For Each column As DataGridViewColumn In dgv.Columns
            worksheet.Cells(1, column.Index + 1).Value = column.HeaderText
        Next
        'Export Header Name End


        'Export Each Row Start
        For rowIndex As Integer = 0 To dgv.Rows.Count - 1
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount
                worksheet.Cells(rowIndex + 2, columnIndex + 1).Value =
                   Convert.ToString(dgv.Item(columnIndex, rowIndex).Value)
                columnIndex += 1
            Loop
        Next
        'Export Each Row End

        workbook.SaveAs(excelLocation)
        APP.Workbooks.Open(excelLocation)
        APP.Visible = True
    End Sub

End Class

Public Class CustomComboBox
    Inherits ComboBox

    Protected Overrides Sub OnEnter(ByVal e As System.EventArgs)
        If Not DroppedDown Then
            DroppedDown = True
        End If
    End Sub

End Class