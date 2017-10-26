Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace GatePass

    Public Class cls_GatePass_prop

        Dim _GatePassId As Int32
        Dim _GatePassNo As String
        Dim _BillNo As String
        Dim _BillDate As DateTime
        Dim _GatePassDate As DateTime
        Dim _Acc_id As Int32
        Dim _SI_ID As Int32
        Dim _Remarks As String
        Dim _VehicalNo As String
        Dim _EntryDate As DateTime
        Dim _InTime As String
        Dim _OutTime As String
        Dim _CREATED_BY As String
        Dim _DIVISION_ID As Integer

        Dim _MODE As Integer
        Dim _dtable_Item_List As DataTable
        Dim _DataTable As DataTable

        Public Property GatePassId() As Int32
            Get
                GatePassId = _GatePassId
            End Get
            Set(ByVal value As Int32)
                _GatePassId = value
            End Set
        End Property
        Public Property SI_ID() As Int32
            Get
                SI_ID = _SI_ID
            End Get
            Set(ByVal value As Int32)
                _SI_ID = value
            End Set
        End Property
        Public Property GatePassNo() As String
            Get
                GatePassNo = _GatePassNo
            End Get
            Set(ByVal value As String)
                _GatePassNo = value
            End Set
        End Property
        Public Property BillNo() As String
            Get
                BillNo = _BillNo
            End Get
            Set(ByVal value As String)
                _BillNo = value
            End Set
        End Property
        Public Property BillDate() As DateTime
            Get
                BillDate = _BillDate
            End Get
            Set(ByVal value As DateTime)
                _BillDate = value
            End Set
        End Property
        Public Property GatePassDate() As DateTime
            Get
                GatePassDate = _GatePassDate
            End Get
            Set(ByVal value As DateTime)
                _GatePassDate = value
            End Set
        End Property
        Public Property Acc_id() As Int32
            Get
                Acc_id = _Acc_id
            End Get
            Set(ByVal value As Int32)
                _Acc_id = value
            End Set
        End Property
        Public Property Remarks() As String
            Get
                Remarks = _Remarks
            End Get
            Set(ByVal value As String)
                _Remarks = value
            End Set
        End Property
        Public Property VehicalNo() As String
            Get
                VehicalNo = _VehicalNo
            End Get
            Set(ByVal value As String)
                _VehicalNo = value
            End Set
        End Property
        Public Property EntryDate() As DateTime
            Get
                EntryDate = _EntryDate
            End Get
            Set(ByVal value As DateTime)
                _EntryDate = value
            End Set
        End Property
        Public Property InTime() As String
            Get
                InTime = _InTime
            End Get
            Set(ByVal value As String)
                _InTime = value
            End Set
        End Property
        Public Property OutTime() As String
            Get
                OutTime = _OutTime
            End Get
            Set(ByVal value As String)
                _OutTime = value
            End Set
        End Property
        Public Property MODE() As Integer
            Get
                MODE = _MODE
            End Get
            Set(ByVal value As Integer)
                _MODE = value
            End Set
        End Property
        Public Property dtable_Item_List() As DataTable
            Get
                dtable_Item_List = _dtable_Item_List
            End Get
            Set(ByVal value As DataTable)
                _dtable_Item_List = value
            End Set
        End Property
        Public Property Data_Table() As DataTable
            Get
                Data_Table = _DataTable
            End Get
            Set(ByVal value As DataTable)
                _DataTable = value
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
        Public Property DIVISION_ID() As Int32
            Get
                DIVISION_ID = _DIVISION_ID
            End Get
            Set(ByVal value As Int32)
                _DIVISION_ID = value
            End Set
        End Property

    End Class

    Public Class cls_GatePass_master
        Inherits CommonClass

        Public Sub Insert_GatePass_MASTER(ByVal clsobj As cls_GatePass_prop)
            Try
                ' Dim tran As SqlTransaction
                If con.State = ConnectionState.Closed Then con.Open()
                ' tran = con.BeginTransaction()
                cmd = New SqlCommand
                cmd.Connection = con
                ' cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_GATEPASS_MASTER"
                cmd.Parameters.AddWithValue("@GatePassId", clsobj.GatePassId)
                cmd.Parameters.AddWithValue("@GatePassNo", clsobj.GatePassNo)
                cmd.Parameters.AddWithValue("@GatePassDate", clsobj.GatePassDate)
                cmd.Parameters.AddWithValue("@BillNo", clsobj.BillNo)
                cmd.Parameters.AddWithValue("@BillDate", clsobj.BillDate)
                cmd.Parameters.AddWithValue("@SI_ID", clsobj.SI_ID)
                cmd.Parameters.AddWithValue("@Acc_id", clsobj.Acc_id)
                cmd.Parameters.AddWithValue("@VehicalNo", clsobj.VehicalNo)
                cmd.Parameters.AddWithValue("@EntryDate", clsobj.EntryDate)
                cmd.Parameters.AddWithValue("@InTime", clsobj.InTime)
                cmd.Parameters.AddWithValue("@OutTime", clsobj.OutTime)
                cmd.Parameters.AddWithValue("@Remarks", clsobj.Remarks)
                cmd.Parameters.AddWithValue("@CreatedBy", clsobj.CREATED_BY)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cmd = New SqlCommand
                cmd.Parameters.Clear()
                cmd.Connection = con
                ' cmd.Transaction = tran
                cmd.CommandText = "update GATEPASS_SERIES set current_used=current_used + 1 where div_id=" & clsobj.DIVISION_ID
                cmd.ExecuteNonQuery()

                cmd.Dispose()

                '  tran.Commit()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

        Public Function GetDCDetail_remote(ByVal qry As String) As DataSet
            Try
                Dim con_gbl As New SqlConnection(gblDNS_Online)
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
    End Class

End Namespace