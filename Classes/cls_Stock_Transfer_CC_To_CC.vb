Imports System
Imports System.Data
Imports System.Data.SqlClient




Namespace Stock_Transfer


    Public Class cls_Stock_Transfer_CC_To_CC_Prop

        Dim _Transfer_ID As Double
        Dim _Transfer_Code As String
        Dim _Transfer_No As Double
        Dim _Transfer_Date As DateTime
        Dim _Transfer_CC_Id As Integer
        Dim _Transfer_Remarks As String
        Dim _Transfer_Status As Int16
        Dim _Received_Date As DateTime
        Dim _Created_By As String
        Dim _Creation_Date As DateTime
        Dim _Modified_By As String
        Dim _Modification_Date As DateTime
        Dim _Division_ID As Int32
        Dim _CostCenter_Id As Int32
        Dim _Item_ID As Int16
        Dim _Item_Qty As Double
        Dim _Accepted_Qty As Double
        Dim _Returned_Qty As Double
        Dim _Transfer_Rate As Double

        Public Property Transfer_ID() As Integer
            Get
                Transfer_ID = _Transfer_ID
            End Get
            Set(ByVal value As Integer)
                _Transfer_ID = value
            End Set
        End Property

        Public Property Transfer_Code() As String
            Get
                Transfer_Code = _Transfer_Code
            End Get
            Set(ByVal value As String)
                _Transfer_Code = value
            End Set
        End Property

        Public Property Transfer_No() As Double
            Get
                Transfer_No = _Transfer_No
            End Get
            Set(ByVal value As Double)
                _Transfer_No = value
            End Set
        End Property

        Public Property Transfer_Date() As DateTime
            Get
                Transfer_Date = _Transfer_Date
            End Get
            Set(ByVal value As DateTime)
                _Transfer_Date = value
            End Set
        End Property



        Public Property Transfer_CC_ID() As Int32
            Get
                Transfer_CC_ID = _Transfer_CC_Id
            End Get
            Set(ByVal value As Int32)
                _Transfer_CC_Id = value
            End Set
        End Property

        Public Property Transfer_Remarks() As String
            Get
                Transfer_Remarks = _Transfer_Remarks
            End Get
            Set(ByVal value As String)
                _Transfer_Remarks = value
            End Set
        End Property
        Public Property Transfer_Status() As Int16
            Get
                Transfer_Status = _Transfer_Status
            End Get
            Set(ByVal value As Int16)
                _Transfer_Status = value
            End Set
        End Property
        Public Property Received_Date() As DateTime
            Get
                Received_Date = _Received_Date
            End Get
            Set(ByVal value As DateTime)
                _Received_Date = value
            End Set
        End Property
        Public Property Created_By() As String
            Get
                Created_By = _Created_By
            End Get
            Set(ByVal value As String)
                _Created_By = value
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

        Public Property Modified_By() As String
            Get
                Modified_By = _Modified_By
            End Get
            Set(ByVal value As String)
                _Modified_By = value
            End Set
        End Property

        Public Property Modification_Date() As DateTime
            Get
                Modification_Date = _Modification_Date
            End Get
            Set(ByVal value As DateTime)
                _Modification_Date = value
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

        Public Property CostCenter_Id() As Int32
            Get
                CostCenter_Id = _CostCenter_Id
            End Get
            Set(ByVal value As Int32)
                _CostCenter_Id = value
            End Set
        End Property

        Public Property Item_ID() As Int32
            Get
                Item_ID = _Item_ID
            End Get
            Set(ByVal value As Int32)
                _Item_ID = value
            End Set
        End Property
        Public Property Accepted_Qty() As Double
            Get
                Accepted_Qty = _Accepted_Qty
            End Get
            Set(ByVal value As Double)
                _Accepted_Qty = value
            End Set
        End Property
        Public Property Returned_Qty() As Double
            Get
                Returned_Qty = _Returned_Qty
            End Get
            Set(ByVal value As Double)
                _Returned_Qty = value
            End Set
        End Property
        Public Property Transfer_Rate() As Double
            Get
                Transfer_Rate = _Transfer_Rate
            End Get
            Set(ByVal value As Double)
                _Transfer_Rate = value
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
    End Class

    Public Class cls_Stock_Transfer_CC_To_CC
        Inherits CommonClass
        Public Sub INSERT_STOCK_TRANSFER_CC(ByVal clsObj As cls_Stock_Transfer_CC_To_CC_Prop, ByVal cmd As SqlCommand)

            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_STOCK_TRANSER_CC_MASTER"

            cmd.Parameters.AddWithValue("@V_Transfer_ID", clsObj.Transfer_ID)
            cmd.Parameters.AddWithValue("@V_Transfer_Code", clsObj.Transfer_Code)
            cmd.Parameters.AddWithValue("@V_Transfer_No", clsObj.Transfer_No)
            cmd.Parameters.AddWithValue("@V_Transfer_Date", clsObj.Transfer_Date)
            cmd.Parameters.AddWithValue("@V_Transfer_Remarks", clsObj.Transfer_Remarks)
            cmd.Parameters.AddWithValue("@v_Transfer_CC_Id", clsObj.Transfer_CC_ID)
            cmd.Parameters.AddWithValue("@v_Received_Date", clsObj.Received_Date)
            cmd.Parameters.AddWithValue("@v_Transfer_Status_Id", clsObj.Transfer_Status)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_CostCenter_Id", clsObj.CostCenter_Id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub
        Public Sub INSERT_STOCK_TRANSFER_CC_DETAIL(ByVal clsObj As cls_Stock_Transfer_CC_To_CC_Prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_STOCK_TRANSER_CC_DETAIL"

            cmd.Parameters.AddWithValue("@v_Transfer_ID", clsObj.Transfer_ID)
            cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_ACCEPTED_QTY", clsObj.Accepted_Qty)
            cmd.Parameters.AddWithValue("@V_RETURNED_QTY", clsObj.Returned_Qty)
            cmd.Parameters.AddWithValue("@V_TRANSFER_RATE", clsObj.Transfer_Rate)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_CostCenter_Id", clsObj.CostCenter_Id)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub update_DebitNote(ByVal clsObj As cls_Stock_Transfer_CC_To_CC_Prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_STOCK_TRANSER_CC_MASTER"

            cmd.Parameters.AddWithValue("@V_Transfer_ID", clsObj.Transfer_ID)
            cmd.Parameters.AddWithValue("@V_Transfer_Code", clsObj.Transfer_Code)
            cmd.Parameters.AddWithValue("@V_Transfer_No", clsObj.Transfer_No)
            cmd.Parameters.AddWithValue("@V_Transfer_Date", clsObj.Transfer_Date)
            cmd.Parameters.AddWithValue("@V_Transfer_Remarks", clsObj.Transfer_Remarks)
            cmd.Parameters.AddWithValue("@v_Transfer_CC_Id", clsObj.Transfer_CC_ID)
            cmd.Parameters.AddWithValue("@v_Received_Date", clsObj.Received_Date)
            cmd.Parameters.AddWithValue("@v_Transfer_Status_Id", clsObj.Transfer_Status)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub

        Public Sub delete_DebitNote(ByVal clsObj As cls_Stock_Transfer_CC_To_CC_Prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_STOCK_TRANSER_CC_MASTER"

            cmd.Parameters.AddWithValue("@V_Transfer_ID", clsObj.Transfer_ID)
            cmd.Parameters.AddWithValue("@V_Transfer_Code", clsObj.Transfer_Code)
            cmd.Parameters.AddWithValue("@V_Transfer_No", clsObj.Transfer_No)
            cmd.Parameters.AddWithValue("@V_Transfer_Date", clsObj.Transfer_Date)
            cmd.Parameters.AddWithValue("@V_Transfer_Remarks", clsObj.Transfer_Remarks)
            ' cmd.Parameters.AddWithValue("@V_received_ID", clsObj.received_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub

        Public Function get_stocktransfered_date_wise(ByVal Sdate As Date, ByVal Edate As Date, ByVal CC_ID As Integer, ByVal status As Integer) As DataSet
            Dim adp As SqlDataAdapter = New SqlDataAdapter("GET_STOCKTRANSFER_MASTER", con)
            Dim ds As New DataSet

            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.Add("@SDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Sdate)
            adp.SelectCommand.Parameters.Add("@EDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(Edate)
            adp.SelectCommand.Parameters.Add("@CC_ID", SqlDbType.Int).Value = CC_ID
            adp.SelectCommand.Parameters.Add("@STATUS", SqlDbType.Int).Value = status
            adp.Fill(ds)
            Return ds
        End Function

        Public Sub Accept_Stock(ByVal clsobj As cls_Stock_Transfer_CC_To_CC_Prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Accept_Stock"
            cmd.Parameters.AddWithValue("@id", clsobj.Transfer_ID)
            cmd.Parameters.AddWithValue("@Rec_date", clsobj.Received_Date)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub
    End Class
End Namespace
