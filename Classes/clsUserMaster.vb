Imports System
Imports System.Data
Imports System.Data.SqlClient




Namespace user_master


    Public Class cls_user_master_prop


        Dim _user_id As Int32
        Dim _user_name As String
        Dim _password As String
        Dim _user_role As String
        Dim _division_id As Int32




        Public Property user_id() As Int32
            Get
                user_id = _user_id
            End Get
            Set(ByVal value As Int32)
                _user_id = value
            End Set
        End Property

        Public Property user_name() As String
            Get
                user_name = _user_name
            End Get
            Set(ByVal value As String)
                _user_name = value
            End Set
        End Property

        Public Property password() As String
            Get
                password = _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        Public Property user_role() As String
            Get
                user_role = _user_role
            End Get
            Set(ByVal value As String)
                _user_role = value
            End Set
        End Property

        Public Property division_id() As Int32
            Get
                division_id = _division_id
            End Get
            Set(ByVal value As Int32)
                _division_id = value
            End Set
        End Property

    End Class

    Public Class cls_user_master
        Inherits CommonClass

        Public Sub insert_USER_MASTER(ByVal clsObj As cls_user_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_USER_MASTER"

            cmd.Parameters.AddWithValue("@V_user_id", clsObj.user_id)
            cmd.Parameters.AddWithValue("@V_user_name", clsObj.user_name)
            cmd.Parameters.AddWithValue("@V_password", clsObj.password)
            cmd.Parameters.AddWithValue("@V_user_role", clsObj.user_role)
            cmd.Parameters.AddWithValue("@V_division_id", clsObj.division_id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)


            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()

        End Sub

        Public Sub update_USER_MASTER(ByVal clsObj As cls_user_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_USER_MASTER"

            cmd.Parameters.AddWithValue("@V_user_id", clsObj.user_id)
            cmd.Parameters.AddWithValue("@V_user_name", clsObj.user_name)
            cmd.Parameters.AddWithValue("@V_password", clsObj.password)
            cmd.Parameters.AddWithValue("@V_user_role", clsObj.user_role)
            cmd.Parameters.AddWithValue("@V_division_id", clsObj.division_id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)


            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()

        End Sub

        Public Sub delete_USER_MASTER(ByVal clsObj As cls_user_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_USER_MASTER"

            cmd.Parameters.AddWithValue("@V_user_id", clsObj.user_id)
            cmd.Parameters.AddWithValue("@V_user_name", clsObj.user_name)
            cmd.Parameters.AddWithValue("@V_password", clsObj.password)
            cmd.Parameters.AddWithValue("@V_user_role", clsObj.user_role)
            cmd.Parameters.AddWithValue("@V_division_id", clsObj.division_id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)


            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()


        End Sub

    End Class
End Namespace
