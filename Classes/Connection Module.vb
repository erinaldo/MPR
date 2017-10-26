Imports System.IO
Imports System.Data.SqlClient

Module Connection_Module
    'Public mycon As Odbc.OdbcConnection
    'Public mycon As SqlConnection
    Public constr As String
    Public stp As String
    'Public DNS As String = "dsn=pay_master"
    Public DNS As String
    Public mnth As Integer
    Public yr As Integer
    Public OldUserControl As IForm = Nothing
    Public NewUserControl As IForm = Nothing

    'Public Sub OpenConnection()
    '    Try
    '        If mycon Is Nothing Then
    '            'mycon = New Odbc.OdbcConnection(DNS)
    '            mycon = New SqlConnection(DNS)
    '        End If
    '        If mycon.State = ConnectionState.Open Then mycon.Close()
    '        mycon.Open()
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> OpenConnection")
    '    End Try
    'End Sub

    Private Function get_f_year() As Integer
        If mnth = 1 Or mnth = 2 Or mnth = 3 Then
            get_f_year = yr - 1
        Else
            get_f_year = yr
        End If
    End Function

    Public Function NofDays(ByVal month As Integer, ByVal year As Integer) As Integer
        'calculating no of days in a month for sal calculation
        If month = 1 Or month = 3 Or month = 5 Or month = 7 Or month = 8 Or month = 10 Or month = 12 Then
            NofDays = 31
        ElseIf month = 2 Then
            If (year Mod 4 = 0 And year Mod 100 <> 0) Or year Mod 400 = 0 Then
                NofDays = 29
            Else
                NofDays = 28
            End If
        Else
            NofDays = 30
        End If
    End Function

    Public Function Month_Name(ByVal mnth_no) As String
        Select Case mnth_no
            Case 1
                Month_Name = "JANUARY"
                Exit Function
            Case 2
                Month_Name = "FEBRUARY"
                Exit Function
            Case 3
                Month_Name = "MARCH"
                Exit Function
            Case 4
                Month_Name = "APRIL"
                Exit Function
            Case 5
                Month_Name = "MAY"
                Exit Function
            Case 6
                Month_Name = "JUNE"
                Exit Function
            Case 7
                Month_Name = "JULY"
                Exit Function
            Case 8
                Month_Name = "AUGUST"
                Exit Function
            Case 9
                Month_Name = "SEPTEMBER"
                Exit Function
            Case 10
                Month_Name = "OCTOBER"
                Exit Function
            Case 11
                Month_Name = "NOVEMBER"
                Exit Function
            Case 12
                Month_Name = "DECEMBER"
                Exit Function
        End Select
        Return ""
    End Function

    Public Function Month_No(ByVal mnth_nm) As Integer
        mnth_nm = UCase(mnth_nm)
        Select Case mnth_nm
            Case "JANUARY"
                Month_No = 1
                Exit Function
            Case "FEBRUARY"
                Month_No = 2
                Exit Function
            Case "MARCH"
                Month_No = 3
                Exit Function
            Case "APRIL"
                Month_No = 4
                Exit Function
            Case "MAY"
                Month_No = 5
                Exit Function
            Case "JUNE"
                Month_No = 6
                Exit Function
            Case "JULY"
                Month_No = 7
                Exit Function
            Case "AUGUST"
                Month_No = 8
                Exit Function
            Case "SEPTEMBER"
                Month_No = 9
                Exit Function
            Case "OCTOBER"
                Month_No = 10
                Exit Function
            Case "NOVEMBER"
                Month_No = 11
                Exit Function
            Case "DECEMBER"
                Month_No = 12
                Exit Function
        End Select
    End Function

    Public Function daily_wages_days(ByVal mnth_name As Integer, ByVal year_name As Integer) As Integer
        Dim i As Integer
        Try
            Dim tot_dy As Integer = DateTime.DaysInMonth(year_name, mnth_name)

            Dim cnt As Integer
            For i = 1 To tot_dy
                If Convert.ToInt32(DatePart(DateInterval.Weekday, Convert.ToDateTime("" + i.ToString + "-" + mnth_name.ToString + "-" + year_name.ToString + ""))) = 1 Then
                    cnt = cnt + 1
                End If
            Next
            daily_wages_days = tot_dy - cnt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Pay Master")
        End Try
    End Function

    Public Sub RightsMsg()
        Try
            MsgBox("You are not authorized for this operation !", MsgBoxStyle.Information, "User Rights")
        Catch ex As Exception
        End Try
    End Sub

End Module
