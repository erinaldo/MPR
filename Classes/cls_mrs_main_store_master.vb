Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace mrs_main_store_master

    Public Class cls_mrs_main_store_master_prop
        Dim _MRS_ID As Double
        Dim _MRS_CODE As String
        Dim _MRS_NO As Double
        Dim _MRS_DATE As DateTime
        Dim _CC_ID As Int32
        Dim _REQ_DATE As DateTime
        Dim _MRS_REMARKS As String
        Dim _MRS_STATUS As Int32
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _APPROVE_DATETIME As DateTime
        Dim _ITEM_ID As Double
        Dim _ITEM_QTY As Double
        Dim _Issue_QTY As Double
        Dim _Bal_QTY As Double

        Public Property MRS_ID() As Double
            Get
                MRS_ID = _MRS_ID
            End Get
            Set(ByVal value As Double)
                _MRS_ID = value
            End Set
        End Property

        Public Property MRS_CODE() As String
            Get
                MRS_CODE = _MRS_CODE
            End Get
            Set(ByVal value As String)
                _MRS_CODE = value
            End Set
        End Property

        Public Property MRS_NO() As Double
            Get
                MRS_NO = _MRS_NO
            End Get
            Set(ByVal value As Double)
                _MRS_NO = value
            End Set
        End Property

        Public Property MRS_DATE() As DateTime
            Get
                MRS_DATE = _MRS_DATE
            End Get
            Set(ByVal value As DateTime)
                _MRS_DATE = value
            End Set
        End Property

        Public Property CC_ID() As Int32
            Get
                CC_ID = _CC_ID
            End Get
            Set(ByVal value As Int32)
                _CC_ID = value
            End Set
        End Property

        Public Property REQ_DATE() As DateTime
            Get
                REQ_DATE = _REQ_DATE
            End Get
            Set(ByVal value As DateTime)
                _REQ_DATE = value
            End Set
        End Property

        Public Property MRS_REMARKS() As String
            Get
                MRS_REMARKS = _MRS_REMARKS
            End Get
            Set(ByVal value As String)
                _MRS_REMARKS = value
            End Set
        End Property

        Public Property MRS_STATUS() As Int32
            Get
                MRS_STATUS = _MRS_STATUS
            End Get
            Set(ByVal value As Int32)
                _MRS_STATUS = value
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

        Public Property APPROVE_DATETIME() As DateTime
            Get
                APPROVE_DATETIME = _APPROVE_DATETIME
            End Get
            Set(ByVal value As DateTime)
                _APPROVE_DATETIME = value
            End Set
        End Property
        Public Property ITEM_ID() As Double
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Double)
                _ITEM_ID = value
            End Set
        End Property

        Public Property ITEM_QTY() As Double
            Get
                ITEM_QTY = _ITEM_QTY
            End Get
            Set(ByVal value As Double)
                _ITEM_QTY = value
            End Set
        End Property
        Public Property Issue_QTY() As Double
            Get
                Issue_QTY = _Issue_QTY
            End Get
            Set(ByVal value As Double)
                _Issue_QTY = value
            End Set
        End Property

        Public Property Bal_QTY() As Double
            Get
                Bal_QTY = _Bal_QTY
            End Get
            Set(ByVal value As Double)
                _Bal_QTY = value
            End Set
        End Property

    End Class

    Public Class cls_mrs_main_store_master
        Inherits CommonClass
        Public Sub Insert_MRS_MAIN_STORE_MASTER(ByVal clsobj As cls_mrs_main_store_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MRS_MAIN_STORE_MASTER"
            cmd.Parameters.AddWithValue("@V_MRS_ID", clsobj.MRS_ID)
            cmd.Parameters.AddWithValue("@V_MRS_CODE", clsobj.MRS_CODE)
            cmd.Parameters.AddWithValue("@V_MRS_NO", clsobj.MRS_NO)
            cmd.Parameters.AddWithValue("@V_MRS_DATE", clsobj.MRS_DATE)
            cmd.Parameters.AddWithValue("@V_CC_ID", clsobj.CC_ID)
            cmd.Parameters.AddWithValue("@V_REQ_DATE", clsobj.REQ_DATE)
            cmd.Parameters.AddWithValue("@V_MRS_REMARKS", clsobj.MRS_REMARKS)
            cmd.Parameters.AddWithValue("@V_MRS_STATUS", clsobj.MRS_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_APPROVE_DATETIME", clsobj.APPROVE_DATETIME)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub Insert_MRS_MAIN_STORE_DETAIL(ByVal clsobj As cls_mrs_main_store_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MRS_MAIN_STORE_DETAIL"
            cmd.Parameters.AddWithValue("@V_MRS_ID", clsobj.MRS_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsobj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsobj.ITEM_QTY)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_Issue_QTY", clsobj.Issue_QTY)
            cmd.Parameters.AddWithValue("@V_Bal_QTY", clsobj.Bal_QTY)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

        Public Sub Update_MRS_MAIN_STORE_MASTER(ByVal clsobj As cls_mrs_main_store_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MRS_MAIN_STORE_MASTER"
            cmd.Parameters.AddWithValue("@V_MRS_ID", clsobj.MRS_ID)
            cmd.Parameters.AddWithValue("@V_MRS_CODE", clsobj.MRS_CODE)
            cmd.Parameters.AddWithValue("@V_MRS_NO", clsobj.MRS_NO)
            cmd.Parameters.AddWithValue("@V_MRS_DATE", clsobj.MRS_DATE)
            cmd.Parameters.AddWithValue("@V_CC_ID", clsobj.CC_ID)
            cmd.Parameters.AddWithValue("@V_REQ_DATE", clsobj.REQ_DATE)
            cmd.Parameters.AddWithValue("@V_MRS_REMARKS", clsobj.MRS_REMARKS)
            cmd.Parameters.AddWithValue("@V_MRS_STATUS", clsobj.MRS_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_APPROVE_DATETIME", clsobj.APPROVE_DATETIME)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub Delete_MRS_MAIN_STORE_MASTER(ByVal clsobj As cls_mrs_main_store_master_prop)
            'cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MRS_MAIN_STORE_MASTER"

            cmd.Parameters.AddWithValue("@V_MRS_ID", clsobj.MRS_ID)
            cmd.Parameters.AddWithValue("@V_MRS_CODE", clsobj.MRS_CODE)
            cmd.Parameters.AddWithValue("@V_MRS_NO", clsobj.MRS_NO)
            cmd.Parameters.AddWithValue("@V_MRS_DATE", clsobj.MRS_DATE)
            cmd.Parameters.AddWithValue("@V_CC_ID", clsobj.CC_ID)
            cmd.Parameters.AddWithValue("@V_REQ_DATE", clsobj.REQ_DATE)
            cmd.Parameters.AddWithValue("@V_MRS_REMARKS", clsobj.MRS_REMARKS)
            cmd.Parameters.AddWithValue("@V_MRS_STATUS", clsobj.MRS_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_APPROVE_DATETIME", clsobj.APPROVE_DATETIME)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub Delete_MRS_MAIN_STORE_DETAIL(ByVal clsobj As cls_mrs_main_store_master_prop, ByVal cmd As SqlCommand)

           cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MRS_MAIN_STORE_DETAIL"
            cmd.Parameters.AddWithValue("@V_MRS_ID", clsobj.MRS_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsobj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsobj.ITEM_QTY)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsobj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsobj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsobj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsobj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsobj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_Issue_QTY", clsobj.Issue_QTY)
            cmd.Parameters.AddWithValue("@V_Bal_QTY", clsobj.Bal_QTY)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Function get_mrs_date_wise(ByVal Sdate As Date, ByVal Edate As Date, ByVal mode As Integer, ByVal status As Integer, ByVal division_id As Integer) As DataSet
            Dim adp As SqlDataAdapter = New SqlDataAdapter("GET_FILTER_MRS_LIST", con)
            Dim ds As New DataSet

            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.Add("@SDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Sdate)
            adp.SelectCommand.Parameters.Add("@EDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Edate)
            adp.SelectCommand.Parameters.Add("@MODE", SqlDbType.Int).Value = mode
            adp.SelectCommand.Parameters.Add("@STATUS", SqlDbType.Int).Value = status
            adp.SelectCommand.Parameters.Add("@DIV_ID", SqlDbType.Int).Value = division_id

            adp.Fill(ds)
            Return ds
        End Function

        Public Sub MRS_status_change(ByVal indent_ids As String, ByVal status As Integer)
            cmd = New SqlCommand()
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandText = "PROC_MRS_STATUS_CHANGE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@mrsid", indent_ids)
            cmd.Parameters.AddWithValue("@status", status)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()

        End Sub
    End Class
End Namespace
