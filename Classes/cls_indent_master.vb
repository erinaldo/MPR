Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Namespace indent_master
    Public Class cls_indent_master_prop

        Dim _INDENT_ID As Double
        Dim _INDENT_CODE As String
        Dim _INDENT_NO As Integer
        Dim _INDENT_DATE As DateTime
        Dim _REQUIRED_DATE As DateTime
        Dim _INDENT_REMARKS As String
        Dim _INDENT_STATUS As Int32
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _ITEM_ID As Int32
        Dim _ITEM_QTY_REQ As Double
        Dim _ITEM_QTY_PO As Double
        Dim _ITEM_QTY_BAL As Double
        Dim _Trans As SqlTransaction


#Region "Properties"

        Public Property INDENT_ID() As Integer
            Get
                INDENT_ID = _INDENT_ID
            End Get
            Set(ByVal value As Integer)
                _INDENT_ID = value
            End Set
        End Property

        Public Property INDENT_CODE() As String
            Get
                INDENT_CODE = _INDENT_CODE
            End Get
            Set(ByVal value As String)
                _INDENT_CODE = value
            End Set
        End Property

        Public Property INDENT_NO() As Integer
            Get
                INDENT_NO = _INDENT_NO
            End Get
            Set(ByVal value As Integer)
                _INDENT_NO = value
            End Set
        End Property

        Public Property INDENT_DATE() As DateTime
            Get
                INDENT_DATE = _INDENT_DATE
            End Get
            Set(ByVal value As DateTime)
                _INDENT_DATE = value
            End Set
        End Property

        Public Property REQUIRED_DATE() As DateTime
            Get
                REQUIRED_DATE = _REQUIRED_DATE
            End Get
            Set(ByVal value As DateTime)
                _REQUIRED_DATE = value
            End Set
        End Property

        Public Property INDENT_REMARKS() As String
            Get
                INDENT_REMARKS = _INDENT_REMARKS
            End Get
            Set(ByVal value As String)
                _INDENT_REMARKS = value
            End Set
        End Property

        Public Property INDENT_STATUS() As Int32
            Get
                INDENT_STATUS = _INDENT_STATUS
            End Get
            Set(ByVal value As Int32)
                _INDENT_STATUS = value
            End Set
        End Property

        Public Property CREATED_BY() As String
            Get
                CREATED_BY = _CREATED_BY
            End Get
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
        End Property

        Public Property CREATION_DATE() As DateTime
            Get
                CREATION_DATE = _CREATION_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATION_DATE = value
            End Set
        End Property

        Public Property MODIFIED_BY() As String
            Get
                MODIFIED_BY = _MODIFIED_BY
            End Get
            Set(ByVal value As String)
                _MODIFIED_BY = value
            End Set
        End Property

        Public Property MODIFIED_DATE() As DateTime
            Get
                MODIFIED_DATE = _MODIFIED_DATE
            End Get
            Set(ByVal value As DateTime)
                _MODIFIED_DATE = value
            End Set
        End Property

        Public Property DIVISION_ID() As Int32
            Get
                DIVISION_ID = _DIVISION_ID
            End Get
            Set(ByVal value As Int32)
                _DIVISION_ID = value
            End Set
        End Property

        Public Property ITEM_ID() As Int32
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Int32)
                _ITEM_ID = value
            End Set
        End Property

        Public Property ITEM_QTY_REQ() As Double
            Get
                ITEM_QTY_REQ = _ITEM_QTY_REQ
            End Get
            Set(ByVal value As Double)
                _ITEM_QTY_REQ = value
            End Set
        End Property

        Public Property ITEM_QTY_PO() As Double
            Get
                ITEM_QTY_PO = _ITEM_QTY_PO
            End Get
            Set(ByVal value As Double)
                _ITEM_QTY_PO = value
            End Set
        End Property

        Public Property ITEM_QTY_BAL() As Double
            Get
                ITEM_QTY_BAL = _ITEM_QTY_BAL
            End Get
            Set(ByVal value As Double)
                _ITEM_QTY_BAL = value
            End Set
        End Property

#End Region
    End Class

    Public Class cls_indent_master
        Inherits CommonClass
        Public Sub Insert_INDENT_MASTER(ByVal clsObj As cls_indent_master_prop)
            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandText = "PROC_INDENT_MASTER"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_INDENT_CODE", clsObj.INDENT_CODE)
            cmd.Parameters.AddWithValue("@V_INDENT_NO", clsObj.INDENT_NO)
            cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsObj.INDENT_DATE)
            cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsObj.REQUIRED_DATE)
            cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", clsObj.INDENT_REMARKS)
            cmd.Parameters.AddWithValue("@V_INDENT_STATUS", clsObj.INDENT_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Insert_INDENT_MASTERTrans(ByVal clsObj As cls_indent_master_prop, ByVal cmd As SqlCommand)
            'cmd = New SqlClient.SqlCommand

            'If con.State = ConnectionState.Closed Then
            ' con.Open()
            ' End If
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandText = "PROC_INDENT_MASTER"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_INDENT_CODE", clsObj.INDENT_CODE)
            cmd.Parameters.AddWithValue("@V_INDENT_NO", clsObj.INDENT_NO)
            cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsObj.INDENT_DATE)
            cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsObj.REQUIRED_DATE)
            cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", clsObj.INDENT_REMARKS)
            cmd.Parameters.AddWithValue("@V_INDENT_STATUS", clsObj.INDENT_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Insert_INDENT_DETAIL(ByVal clsobj As cls_indent_master_prop)

            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsobj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsobj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", clsobj.ITEM_QTY_REQ)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", clsobj.ITEM_QTY_PO)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", clsobj.ITEM_QTY_BAL)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()

            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Insert_INDENT_DETAILTrans(ByVal clsobj As cls_indent_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand

            'If con.State = ConnectionState.Closed Then
            ' con.Open()
            'End If
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsobj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsobj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", clsobj.ITEM_QTY_REQ)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", clsobj.ITEM_QTY_PO)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", clsobj.ITEM_QTY_BAL)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            ' cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Update_INDENT_MASTER(ByVal clsObj As cls_indent_master_prop)

            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_MASTER"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_INDENT_CODE", clsObj.INDENT_CODE)
            cmd.Parameters.AddWithValue("@V_INDENT_NO", clsObj.INDENT_NO)
            cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsObj.INDENT_DATE)
            cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsObj.REQUIRED_DATE)
            cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", clsObj.INDENT_REMARKS)
            cmd.Parameters.AddWithValue("@V_INDENT_STATUS", clsObj.INDENT_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Update_INDENT_MASTERTrans(ByVal clsObj As cls_indent_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'If con.State = ConnectionState.Closed Then
            ' con.Open()
            ' End If
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_MASTER"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_INDENT_CODE", clsObj.INDENT_CODE)
            cmd.Parameters.AddWithValue("@V_INDENT_NO", clsObj.INDENT_NO)
            cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsObj.INDENT_DATE)
            cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsObj.REQUIRED_DATE)
            cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", clsObj.INDENT_REMARKS)
            cmd.Parameters.AddWithValue("@V_INDENT_STATUS", clsObj.INDENT_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Update_INDENT_DETAIL(ByVal clsobj As cls_indent_master_prop)

            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsobj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsobj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", clsobj.ITEM_QTY_REQ)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", clsobj.ITEM_QTY_PO)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", clsobj.ITEM_QTY_BAL)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub Delete_INDENT_MASTER(ByVal clsObj As cls_indent_master_prop)

            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_MASTER"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_INDENT_CODE", clsObj.INDENT_CODE)
            cmd.Parameters.AddWithValue("@V_INDENT_NO", clsObj.INDENT_NO)
            cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsObj.INDENT_DATE)
            cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsObj.REQUIRED_DATE)
            cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", clsObj.INDENT_REMARKS)
            cmd.Parameters.AddWithValue("@V_INDENT_STATUS", clsObj.INDENT_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Delete_INDENT_MASTERTrans(ByVal clsObj As cls_indent_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'If con.State = ConnectionState.Closed Then
            ' con.Open()
            'End If
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_MASTER"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_INDENT_CODE", clsObj.INDENT_CODE)
            cmd.Parameters.AddWithValue("@V_INDENT_NO", clsObj.INDENT_NO)
            cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsObj.INDENT_DATE)
            cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsObj.REQUIRED_DATE)
            cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", clsObj.INDENT_REMARKS)
            cmd.Parameters.AddWithValue("@V_INDENT_STATUS", clsObj.INDENT_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Delete_INDENT_DETAIL(ByVal clsObj As cls_indent_master_prop)

            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", clsObj.ITEM_QTY_REQ)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", clsObj.ITEM_QTY_PO)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", clsObj.ITEM_QTY_BAL)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Delete_INDENT_DETAILTrans(ByVal clsObj As cls_indent_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'If con.State = ConnectionState.Closed Then
            ' con.Open()
            ' End If
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INDENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_INDENT_ID", clsObj.INDENT_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", clsObj.ITEM_QTY_REQ)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", clsObj.ITEM_QTY_PO)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", clsObj.ITEM_QTY_BAL)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub indent_status_change(ByVal indent_ids As String, ByVal status As Integer)
            cmd = New SqlCommand()
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandText = "PROC_INDENT_STATUS_CHANGE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@Indentno", indent_ids)
            cmd.Parameters.AddWithValue("@status", status)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()

        End Sub

        Public Function get_indent_date_wise(ByVal Sdate As Date, ByVal Edate As Date, ByVal mode As Integer, ByVal status As Integer, ByVal division_id As Integer) As DataSet
            Dim adp As SqlDataAdapter = New SqlDataAdapter("GET_FILTER_INDENT_LIST", con)
            Dim ds As New DataSet

            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.Add("@SDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Sdate)
            adp.SelectCommand.Parameters.Add("@EDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Edate)
            adp.SelectCommand.Parameters.Add("@MODE", SqlDbType.Int).Value = mode
            adp.SelectCommand.Parameters.Add("@STATUS", SqlDbType.Int).Value = status
            adp.SelectCommand.Parameters.Add("@division_id", SqlDbType.Int).Value = division_id

            adp.Fill(ds)
            Return ds
        End Function

        'Public Sub GET_CurrentStock(ByVal item_id As String)
        '    'Dim con As New SqlConnection
        '    'con.ConnectionString = "Data Source=inderjeet;Initial Catalog=afbl_mms;User Id=sa; Password=DataBase; Connection Timeout = 100"
        '    Dim Value As String
        '    If con.State = ConnectionState.Closed Then
        '        con.Open()
        '    End If
        '    Dim str As String = "SELECT dbo.Get_Current_Stock(@Item_ID, @Div_ID, @ToDate)"
        '    Dim cmd As New SqlCommand(str, con)
        '    cmd.Parameters.AddWithValue("@Item_ID", item_id)
        '    cmd.Parameters.AddWithValue("@Div_ID", v_the_current_logged_in_user_id)
        '    cmd.Parameters.AddWithValue("@ToDate", now)
        '    If con.State = ConnectionState.Closed Then
        '        con.Open()
        '    End If
        '    cmd.ExecuteNonQuery()
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    While dr.Read
        '        Value = dr(0)
        '    End While
        '    'Return Value
        '    'VALUE = DirectCast(cmd.ExecuteScalar(), String)
        '    cmd.Dispose()
        '    con.Close()
        '    con.Dispose()
        'End Sub
    End Class
End Namespace

