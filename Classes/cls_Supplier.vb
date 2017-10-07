Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace supplier_rate_list

    Public Class cls_supplier_rate_list_prop

        Dim _SRL_ID As Decimal
        Dim _SRL_NAME As String
        Dim _SRL_DATE As DateTime
        Dim _SRL_DESC As String
        Dim _SUPP_ID As Int32
        Dim _ACTIVE As Boolean
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _ITEM_ID As Int32
        Dim _ITEM_RATE As Decimal
        Dim _DEL_QTY As Decimal
        Dim _DEL_DAYS As String
        Dim _grdDetail As DataGridView

        Public Property SRL_ID() As Decimal
            Get
                SRL_ID = _SRL_ID
            End Get
            Set(ByVal value As Decimal)
                _SRL_ID = value
            End Set
        End Property

        Public Property SRL_NAME() As String
            Get
                SRL_NAME = _SRL_NAME
            End Get
            Set(ByVal value As String)
                _SRL_NAME = value
            End Set
        End Property

        Public Property SRL_DATE() As DateTime
            Get
                SRL_DATE = _SRL_DATE
            End Get
            Set(ByVal value As DateTime)
                _SRL_DATE = value
            End Set
        End Property

        Public Property SRL_DESC() As String
            Get
                SRL_DESC = _SRL_DESC
            End Get
            Set(ByVal value As String)
                _SRL_DESC = value
            End Set
        End Property

        Public Property SUPP_ID() As Int32
            Get
                SUPP_ID = _SUPP_ID
            End Get
            Set(ByVal value As Int32)
                _SUPP_ID = value
            End Set
        End Property

        Public Property ACTIVE() As Boolean
            Get
                ACTIVE = _ACTIVE
            End Get
            Set(ByVal value As Boolean)
                _ACTIVE = value
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

        Public Property ITEM_RATE() As Decimal
            Get
                ITEM_RATE = _ITEM_RATE
            End Get
            Set(ByVal value As Decimal)
                _ITEM_RATE = value
            End Set
        End Property

        Public Property DEL_QTY() As Decimal
            Get
                DEL_QTY = _DEL_QTY
            End Get
            Set(ByVal value As Decimal)
                _DEL_QTY = value
            End Set
        End Property

        Public Property DEL_DAYS() As String
            Get
                DEL_DAYS = _DEL_DAYS
            End Get
            Set(ByVal value As String)
                _DEL_DAYS = value
            End Set
        End Property

        Public Property grdDetail() As DataGridView
            Get
                grdDetail = _grdDetail
            End Get
            Set(ByVal value As DataGridView)
                _grdDetail = value
            End Set
        End Property

    End Class

    Public Class cls_supplier_rate_list
        Inherits CommonClass

        Public Sub insert_SUPPLIER_RATE_LIST(ByVal clsObj As cls_supplier_rate_list_prop, ByVal cmd As SqlCommand)
            Try
                'cmd = New SqlClient.SqlCommand
                'cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_SUPPLIER_RATE_LIST"

                cmd.Parameters.AddWithValue("@V_SRL_ID", clsObj.SRL_ID)
                cmd.Parameters.AddWithValue("@V_SRL_NAME", clsObj.SRL_NAME)
                cmd.Parameters.AddWithValue("@V_SRL_DATE", clsObj.SRL_DATE)
                cmd.Parameters.AddWithValue("@V_SRL_DESC", clsObj.SRL_DESC)
                cmd.Parameters.AddWithValue("@V_SUPP_ID", clsObj.SUPP_ID)
                cmd.Parameters.AddWithValue("@V_ACTIVE", clsObj.ACTIVE)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                'If con.State = ConnectionState.Closed Then con.Open()

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                'con.Close()
                ' con.Dispose()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update_rec clsLocationMaster")
            End Try

        End Sub

        Public Sub insert_SUPPLIER_DETAIL_RATE_LIST(ByVal clsObj As cls_supplier_rate_list_prop, ByVal cmd As SqlCommand)
            Try
                cmd.Parameters.Clear()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_SUPPLIER_RATE_LIST_DETAIL"

                cmd.Parameters.AddWithValue("@V_SRL_ID", clsObj.SRL_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
                cmd.Parameters.AddWithValue("@V_DEL_QTY", clsObj.DEL_QTY)
                cmd.Parameters.AddWithValue("@V_DEL_DAYS", clsObj.DEL_DAYS)
                'cmd.Parameters.AddWithValue("@V_ACTIVE", clsObj.ACTIVE)
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
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update_rec clsLocationMaster")
            End Try
        End Sub

        Public Sub update_SUPPLIER_RATE_LIST(ByVal clsObj As cls_supplier_rate_list_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandText = "PROC_SUPPLIER_RATE_LIST"
            cmd.Parameters.AddWithValue("@V_SRL_ID", clsObj.SRL_ID)
            cmd.Parameters.AddWithValue("@V_SRL_NAME", clsObj.SRL_NAME)
            cmd.Parameters.AddWithValue("@V_SRL_DATE", clsObj.SRL_DATE)
            cmd.Parameters.AddWithValue("@V_SRL_DESC", clsObj.SRL_DESC)
            cmd.Parameters.AddWithValue("@V_SUPP_ID", clsObj.SUPP_ID)
            cmd.Parameters.AddWithValue("@V_ACTIVE", clsObj.ACTIVE)
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


        'Public Sub Insert_RATE_LIST_Mapping(ByVal custid As Int32, ByVal RateListid As Int32, ByVal cmd As SqlCommand)
        '    cmd.Parameters.Clear()
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.CommandText = "Proc_RateListMapping"
        '    cmd.Parameters.AddWithValue("@CustId", custid)
        '    cmd.Parameters.AddWithValue("@RateListId", RateListid)
        '    cmd.ExecuteNonQuery()
        '    cmd.Dispose()
        '    'con.Close()
        '    'con.Dispose()

        'End Sub


        Public Sub update_SUPPLIER_DETAIL_RATE_LIST(ByVal clsObj As cls_supplier_rate_list_prop, ByVal cmd As SqlCommand)

            Try
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_SUPPLIER_RATE_LIST_DETAIL"
                cmd.Parameters.AddWithValue("@V_SRL_ID", clsObj.SRL_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
                cmd.Parameters.AddWithValue("@V_DEL_QTY", clsObj.DEL_QTY)
                cmd.Parameters.AddWithValue("@V_DEL_DAYS", clsObj.DEL_DAYS)
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
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update_rec clsLocationMaster")
            End Try

        End Sub

        Public Sub Delete_SUPPLIER_DETAIL_RATE_LIST(ByVal clsObj As cls_supplier_rate_list_prop, ByVal cmd As SqlCommand)

            Try
                cmd.Parameters.Clear()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_SUPPLIER_RATE_LIST_DETAIL"
                cmd.Parameters.AddWithValue("@V_SRL_ID", clsObj.SRL_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
                cmd.Parameters.AddWithValue("@V_DEL_QTY", clsObj.DEL_QTY)
                cmd.Parameters.AddWithValue("@V_DEL_DAYS", clsObj.DEL_DAYS)
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
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update_rec clsLocationMaster")
            End Try

        End Sub

        Public Sub delete_SUPPLIER_RATE_LIST(ByVal clsObj As cls_supplier_rate_list_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_SUPPLIER_RATE_LIST"

            cmd.Parameters.AddWithValue("@V_SRL_ID", clsObj.SRL_ID)
            cmd.Parameters.AddWithValue("@V_SRL_NAME", clsObj.SRL_NAME)
            cmd.Parameters.AddWithValue("@V_SRL_DATE", clsObj.SRL_DATE)
            cmd.Parameters.AddWithValue("@V_SRL_DESC", clsObj.SRL_DESC)
            cmd.Parameters.AddWithValue("@V_SUPP_ID", clsObj.SUPP_ID)
            cmd.Parameters.AddWithValue("@V_ACTIVE", clsObj.ACTIVE)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

    End Class

End Namespace
