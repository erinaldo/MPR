Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace Closing_master
    Public Class cls_Closing_master_prop
        Dim _Closing_ID As Double
        Dim _Closing_Code As String
        Dim _Closing_No As Double
        Dim _Closing_Date As DateTime
        Dim _Closing_Remarks As String
        Dim _Closing_Status As Integer
        Dim _Created_BY As String
        Dim _Creation_Date As DateTime
        Dim _Modified_BY As String
        Dim _Modified_Date As DateTime
        Dim _CostCenter_ID As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Balance_Qty As Double
        Dim _Stock_Detail_Id As Double
        Dim _Item_Rate As Double
        Dim _ClosingItem As DataTable
        Dim _current_Stock As Double
        Dim _Consumption As Double
        Dim _Conv_factor As Double
        Dim _Closing_Stock_recp As Double
        Dim _Division_ID As Integer

        Public Property Conv_factor() As Double
            Get
                Conv_factor = _Conv_factor
            End Get
            Set(ByVal value As Double)
                _Conv_factor = value
            End Set
        End Property

        Public Property Closing_Stock_recp() As Double
            Get
                Closing_Stock_recp = _Closing_Stock_recp
            End Get
            Set(ByVal value As Double)
                _Closing_Stock_recp = value
            End Set
        End Property

        Public Property Stock_Detail_Id() As Double
            Get
                Stock_Detail_Id = _Stock_Detail_Id
            End Get
            Set(ByVal value As Double)
                _Stock_Detail_Id = value
            End Set
        End Property

        Public Property Closing_ID() As Double
            Get
                Closing_ID = _Closing_ID
            End Get
            Set(ByVal value As Double)
                _Closing_ID = value
            End Set
        End Property

        Public Property Closing_Code() As String
            Get
                Closing_Code = _Closing_Code
            End Get
            Set(ByVal value As String)
                _Closing_Code = value
            End Set
        End Property

        Public Property Closing_No() As Double
            Get
                Closing_No = _Closing_No
            End Get
            Set(ByVal value As Double)
                _Closing_No = value
            End Set
        End Property

        Public Property Closing_Date() As DateTime
            Get
                Closing_Date = _Closing_Date
            End Get
            Set(ByVal value As DateTime)
                _Closing_Date = value
            End Set
        End Property

        Public Property Closing_Remarks() As String
            Get
                Closing_Remarks = _Closing_Remarks
            End Get
            Set(ByVal value As String)
                _Closing_Remarks = value
            End Set
        End Property

        Public Property Closing_Status() As Integer
            Get
                Closing_Status = _Closing_Status
            End Get
            Set(ByVal value As Integer)
                _Closing_Status = value
            End Set
        End Property


        Public Property Created_BY() As String
            Get
                Created_BY = _Created_BY
            End Get
            Set(ByVal value As String)
                _Created_BY = value
            End Set
        End Property

        Public Property Creation_Date() As DateTime
            Get
                Creation_Date = _Creation_Date
            End Get
            Set(ByVal value As DateTime)
                _Creation_Date = value
            End Set
        End Property

        Public Property Modified_BY() As String
            Get
                Modified_BY = _Modified_BY
            End Get
            Set(ByVal value As String)
                _Modified_BY = value
            End Set
        End Property

        Public Property Modified_Date() As DateTime
            Get
                Modified_Date = _Modified_Date
            End Get
            Set(ByVal value As DateTime)
                _Modified_Date = value
            End Set
        End Property

        Public Property CostCenter_ID() As Int32
            Get
                CostCenter_ID = _CostCenter_ID
            End Get
            Set(ByVal value As Int32)
                _CostCenter_ID = value
            End Set
        End Property


        Public Property Division_ID() As Int32
            Get
                Division_ID = _Division_ID
            End Get
            Set(ByVal value As Int32)
                _Division_ID = value
            End Set
        End Property

        Public Property Item_ID() As Double
            Get
                Item_ID = _Item_ID
            End Get
            Set(ByVal value As Double)
                _Item_ID = value
            End Set
        End Property

        Public Property Item_Qty() As Double
            Get
                Item_Qty = _Item_Qty
            End Get
            Set(ByVal value As Double)
                _Item_Qty = value
            End Set
        End Property
        Public Property Balance_Qty() As Double
            Get
                Balance_Qty = _Balance_Qty
            End Get
            Set(ByVal value As Double)
                _Balance_Qty = value
            End Set
        End Property
        Public Property ClosingItem() As DataTable
            Get
                Return _ClosingItem
            End Get

            Set(ByVal value As DataTable)
                _ClosingItem = value

            End Set
        End Property

        Public Property Item_Rate() As Double
            Get
                Item_Rate = _Item_Rate
            End Get
            Set(ByVal value As Double)
                _Item_Rate = value
            End Set
        End Property

        Public Property Current_Stock() As Double
            Get
                Current_Stock = _current_Stock
            End Get
            Set(ByVal value As Double)
                _current_Stock = value
            End Set
        End Property

        Public Property Consumption() As Double
            Get
                Consumption = _Consumption
            End Get
            Set(ByVal value As Double)
                _Consumption = value
            End Set
        End Property

    End Class

    Public Class cls_Closing_master
        Inherits CommonClass

        Public Sub Freeze_Closing_Stock(ByVal clsobj As cls_Closing_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_Closing_CC_MASTER"
            cmd.Parameters.AddWithValue("@id", clsobj.Closing_ID)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

        Public Sub Insert_Closing_Master(ByVal clsObj As cls_Closing_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_Closing_CC_MASTER"
            cmd.Parameters.AddWithValue("@V_Closing_ID", clsObj.Closing_ID)
            cmd.Parameters.AddWithValue("@V_Closing_Code", clsObj.Closing_Code)
            cmd.Parameters.AddWithValue("@V_Closing_No", clsObj.Closing_No)
            cmd.Parameters.AddWithValue("@V_Closing_Date", clsObj.Closing_Date)
            cmd.Parameters.AddWithValue("@V_Closing_Remarks", clsObj.Closing_Remarks)
            cmd.Parameters.AddWithValue("@V_Closing_Status", clsObj.Closing_Status)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_CostCenter_ID", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

        Public Sub Insert_Closing_Detail(ByVal clsObj As cls_Closing_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_Closing_CC_DETAIL"
            cmd.Parameters.AddWithValue("@V_Closing_ID", clsObj.Closing_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@V_Current_Stock", clsObj.Current_Stock)
            cmd.Parameters.AddWithValue("@V_Consumption", clsObj.Consumption)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_CostCenter_ID", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Conv_factore", clsObj.Conv_factor)
            cmd.Parameters.AddWithValue("@V_Closing_Stock_recp", clsObj.Closing_Stock_recp)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub Update_Closing_Master(ByVal clsObj As cls_Closing_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_Closing_CC_MASTER"
            cmd.Parameters.AddWithValue("@V_Closing_ID", clsObj.Closing_ID)
            cmd.Parameters.AddWithValue("@V_Closing_Code", clsObj.Closing_Code)
            cmd.Parameters.AddWithValue("@V_Closing_No", clsObj.Closing_No)
            cmd.Parameters.AddWithValue("@V_Closing_Date", clsObj.Closing_Date)
            cmd.Parameters.AddWithValue("@V_Closing_Remarks", clsObj.Closing_Remarks)
            cmd.Parameters.AddWithValue("@V_Closing_Status", clsObj.Closing_Status)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_CostCenter_ID", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub Delete_Closing_MASTER(ByVal clsObj As cls_Closing_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_Closing_CC_MASTER"
            cmd.Parameters.AddWithValue("@V_Closing_ID", clsObj.Closing_ID)
            cmd.Parameters.AddWithValue("@V_Closing_Code", clsObj.Closing_Code)
            cmd.Parameters.AddWithValue("@V_Closing_No", clsObj.Closing_No)
            cmd.Parameters.AddWithValue("@V_Closing_Date", clsObj.Closing_Date)
            cmd.Parameters.AddWithValue("@V_Closing_Remarks", clsObj.Closing_Remarks)
            cmd.Parameters.AddWithValue("@V_Closing_Status", clsObj.Closing_Status)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_CostCenter_ID", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()

        End Sub

        Public Sub Delete_Closing_Detail(ByVal clsObj As cls_Closing_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_Closing_CC_DETAIL"
            cmd.Parameters.AddWithValue("@V_Closing_ID", clsObj.Closing_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@V_Current_Stock", clsObj.Current_Stock)
            cmd.Parameters.AddWithValue("@V_Consumption", clsObj.Consumption)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_CostCenter_ID", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Conv_factore", clsObj.Conv_factor)
            cmd.Parameters.AddWithValue("@V_Closing_Stock_recp", clsObj.Closing_Stock_recp)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub GET_Closing_MASTER(ByVal clsObj As cls_Closing_master_prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_Closing_CC_MASTER"
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

        Public Function GET_CLOSING_MASTER_DATEWISE(ByVal Sdate As Date, ByVal Edate As Date, ByVal Costcenter_id As Integer) As DataSet
            Dim adp As SqlDataAdapter = New SqlDataAdapter("GET_CLOSING_MASTER_DATEWISE", con)
            Dim ds As New DataSet

            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.Add("@SDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Sdate)
            adp.SelectCommand.Parameters.Add("@EDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Edate)
            adp.SelectCommand.Parameters.Add("@CC_ID", SqlDbType.Int).Value = Costcenter_id

            adp.Fill(ds)
            Return ds
        End Function

        Public Sub Freeze_Closing_Stock(ByVal clsobj As cls_Closing_master_prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Freeze_Closing_Stock"
            cmd.Parameters.AddWithValue("@id", clsobj.Closing_ID)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub
    End Class
End Namespace
