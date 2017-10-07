Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace item_master


    Public Class cls_item_master_prop

        Dim _ITEM_ID As Decimal
        Dim _ITEM_CODE As String
        Dim _ITEM_NAME As String
        Dim _ITEM_DESC As String
        Dim _UM_ID As Int32
        Dim _ITEM_CATEGORY_ID As Int32
        Dim _RELATED_ITEM_ID As Decimal
        Dim _GMS_SPEC As Decimal
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32




        Public Property ITEM_ID() As Decimal
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Decimal)
                _ITEM_ID = value
            End Set
        End Property

        Public Property ITEM_CODE() As String
            Get
                ITEM_CODE = _ITEM_CODE
            End Get
            Set(ByVal value As String)
                _ITEM_CODE = value
            End Set
        End Property

        Public Property ITEM_NAME() As String
            Get
                ITEM_NAME = _ITEM_NAME
            End Get
            Set(ByVal value As String)
                _ITEM_NAME = value
            End Set
        End Property

        Public Property ITEM_DESC() As String
            Get
                ITEM_DESC = _ITEM_DESC
            End Get
            Set(ByVal value As String)
                _ITEM_DESC = value
            End Set
        End Property

        Public Property UM_ID() As Int32
            Get
                UM_ID = _UM_ID
            End Get
            Set(ByVal value As Int32)
                _UM_ID = value
            End Set
        End Property

        Public Property ITEM_CATEGORY_ID() As Int32
            Get
                ITEM_CATEGORY_ID = _ITEM_CATEGORY_ID
            End Get
            Set(ByVal value As Int32)
                _ITEM_CATEGORY_ID = value
            End Set
        End Property

        Public Property RELATED_ITEM_ID() As Decimal
            Get
                RELATED_ITEM_ID = _RELATED_ITEM_ID
            End Get
            Set(ByVal value As Decimal)
                _RELATED_ITEM_ID = value
            End Set
        End Property

        Public Property GMS_SPEC() As Decimal
            Get
                GMS_SPEC = _GMS_SPEC
            End Get
            Set(ByVal value As Decimal)
                _GMS_SPEC = value
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

    End Class

    Public Class cls_item_master
        Inherits CommonClass
        Public Sub insert_ITEM_MASTER(ByVal clsObj As cls_item_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ITEM_MASTER"

            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_CODE", clsObj.ITEM_CODE)
            cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
            cmd.Parameters.AddWithValue("@V_ITEM_DESC", clsObj.ITEM_DESC)
            cmd.Parameters.AddWithValue("@V_UM_ID", clsObj.UM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_CATEGORY_ID", clsObj.ITEM_CATEGORY_ID)
            cmd.Parameters.AddWithValue("@V_RELATED_ITEM_ID", clsObj.RELATED_ITEM_ID)
            cmd.Parameters.AddWithValue("@V_GMS_SPEC", clsObj.GMS_SPEC)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub update_ITEM_MASTER(ByVal clsObj As cls_item_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ITEM_MASTER"

            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_CODE", clsObj.ITEM_CODE)
            cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
            cmd.Parameters.AddWithValue("@V_ITEM_DESC", clsObj.ITEM_DESC)
            cmd.Parameters.AddWithValue("@V_UM_ID", clsObj.UM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_CATEGORY_ID", clsObj.ITEM_CATEGORY_ID)
            cmd.Parameters.AddWithValue("@V_RELATED_ITEM_ID", clsObj.RELATED_ITEM_ID)
            cmd.Parameters.AddWithValue("@V_GMS_SPEC", clsObj.GMS_SPEC)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub delete_ITEM_MASTER(ByVal clsObj As cls_item_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ITEM_MASTER"

            cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_CODE", clsObj.ITEM_CODE)
            cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
            cmd.Parameters.AddWithValue("@V_ITEM_DESC", clsObj.ITEM_DESC)
            cmd.Parameters.AddWithValue("@V_UM_ID", clsObj.UM_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_CATEGORY_ID", clsObj.ITEM_CATEGORY_ID)
            cmd.Parameters.AddWithValue("@V_RELATED_ITEM_ID", clsObj.RELATED_ITEM_ID)
            cmd.Parameters.AddWithValue("@V_GMS_SPEC", clsObj.GMS_SPEC)
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

        Public Sub get_ITEM_MASTER(ByVal clsObj As cls_item_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_ITEM_MASTER"

            cmd.Parameters.AddWithValue("@ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@DIV_ID", clsObj.DIVISION_ID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        End Sub
    End Class

End Namespace
