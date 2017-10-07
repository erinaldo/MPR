Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace cost_center_master
    Public Class cls_cost_center_master_prop
        Dim _CostCenter_Id As Int32
        Dim _CostCenter_Code As String
        Dim _CostCenter_Name As String
        Dim _CostCenter_Description As String
        Dim _CostCenter_Status As Boolean
        Dim _Division_Id As Int32

        Public Property CostCenter_Id() As Int32
            Get
                CostCenter_Id = _CostCenter_Id
            End Get
            Set(ByVal value As Int32)
                _CostCenter_Id = value
            End Set
        End Property

        Public Property CostCenter_Code() As String
            Get
                CostCenter_Code = _CostCenter_Code
            End Get
            Set(ByVal value As String)
                _CostCenter_Code = value
            End Set
        End Property

        Public Property CostCenter_Name() As String
            Get
                CostCenter_Name = _CostCenter_Name
            End Get
            Set(ByVal value As String)
                _CostCenter_Name = value
            End Set
        End Property

        Public Property CostCenter_Description() As String
            Get
                CostCenter_Description = _CostCenter_Description
            End Get
            Set(ByVal value As String)
                _CostCenter_Description = value
            End Set
        End Property

        Public Property CostCenter_Status() As Boolean
            Get
                CostCenter_Status = _CostCenter_Status
            End Get
            Set(ByVal value As Boolean)
                _CostCenter_Status = value

            End Set
        End Property
        Public Property Division_Id() As Int32
            Get
                Division_Id = _Division_Id
            End Get
            Set(ByVal value As Int32)
                _Division_Id = value
            End Set
        End Property

    End Class

    Public Class cls_cost_center_master
        Inherits CommonClass
        Public Sub Insert_COST_CENTER_MASTER(ByVal clsObj As cls_cost_center_master_prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_COST_CENTER_MASTER"
            cmd.Parameters.AddWithValue("@V_CostCenter_Id", clsObj.CostCenter_Id)
            cmd.Parameters.AddWithValue("@V_CostCenter_Code", clsObj.CostCenter_Code)
            cmd.Parameters.AddWithValue("@V_CostCenter_Name", clsObj.CostCenter_Name)
            cmd.Parameters.AddWithValue("@V_CostCenter_Description", clsObj.CostCenter_Description)
            cmd.Parameters.AddWithValue("@v_CostCenter_Status", clsObj.CostCenter_Status)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_Id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()
        End Sub

        Public Sub Update_COST_CENTER_MASTER(ByVal clsObj As cls_cost_center_master_prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_COST_CENTER_MASTER"
            cmd.Parameters.AddWithValue("@V_CostCenter_Id", clsObj.CostCenter_Id)
            cmd.Parameters.AddWithValue("@V_CostCenter_Code", clsObj.CostCenter_Code)
            cmd.Parameters.AddWithValue("@V_CostCenter_Name", clsObj.CostCenter_Name)
            cmd.Parameters.AddWithValue("@V_CostCenter_Description", clsObj.CostCenter_Description)
            cmd.Parameters.AddWithValue("@v_CostCenter_Status", clsObj.CostCenter_Status)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_Id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub GET_COST_CENTER_MASTER()
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_COST_CENTER_MASTER"
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 4)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()
        End Sub
    End Class

End Namespace
