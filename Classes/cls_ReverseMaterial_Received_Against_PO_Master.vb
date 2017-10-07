Imports System
Imports System.Data
Imports System.Data.SqlClient




Namespace ReverseMaterial_Recieved_Against_Po_Master


    Public Class cls_ReverseMaterial_Recieved_Against_Po_Master_Prop


        Dim _Reverse_ID As Double
        Dim _Reverse_Code As String
        Dim _Reverse_No As Double
        Dim _Reverse_Date As DateTime
        Dim _Remarks As String
        Dim _received_ID As Double
        Dim _Created_By As String
        Dim _Creation_Date As DateTime
        Dim _Modified_By As String
        Dim _Modification_Date As DateTime
        Dim _Division_ID As Int32
        Dim _Item_ID As Decimal
        Dim _Prev_Item_Qty As Double
        Dim _Item_Qty As Double
        Dim _Prev_Item_Rate As Double
        Dim _Item_Rate As Double
        Dim _Prev_Item_vat As Double
        Dim _Prev_Item_exice As Double
        Dim _Item_vat As Double
        Dim _Item_exice As Double
        Dim _Batch_No As String
        Dim _Expiry_Date As DateTime
        Dim _StockDetail_Id As Double
        Dim _TransType As Integer
        Dim _MRNId As Integer
        Dim _CostCenter_id As Integer


        Public Property Reverse_ID() As Decimal
            Get
                Reverse_ID = _Reverse_ID
            End Get
            Set(ByVal value As Decimal)
                _Reverse_ID = value
            End Set
        End Property

        Public Property Reverse_Code() As String
            Get
                Reverse_Code = _Reverse_Code
            End Get
            Set(ByVal value As String)
                _Reverse_Code = value
            End Set
        End Property

        Public Property Reverse_No() As Decimal
            Get
                Reverse_No = _Reverse_No
            End Get
            Set(ByVal value As Decimal)
                _Reverse_No = value
            End Set
        End Property

        Public Property Reverse_Date() As DateTime
            Get
                Reverse_Date = _Reverse_Date
            End Get
            Set(ByVal value As DateTime)
                _Reverse_Date = value
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

        Public Property received_ID() As Decimal
            Get
                received_ID = _received_ID
            End Get
            Set(ByVal value As Decimal)
                _received_ID = value
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


        Public Property Item_ID() As Decimal
            Get
                Item_ID = _Item_ID
            End Get
            Set(ByVal value As Decimal)
                _Item_ID = value
            End Set
        End Property

        Public Property Prev_Item_Qty() As Double
            Get
                Prev_Item_Qty = _Prev_Item_Qty
            End Get
            Set(ByVal value As Double)
                _Prev_Item_Qty = value
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

        Public Property Prev_Item_Rate() As Double
            Get
                Prev_Item_Rate = _Prev_Item_Rate
            End Get
            Set(ByVal value As Double)
                _Prev_Item_Rate = value
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




        Public Property Prev_Item_vat() As Double
            Get
                Prev_Item_vat = _Prev_Item_vat
            End Get
            Set(ByVal value As Double)
                _Prev_Item_vat = value
            End Set
        End Property

        Public Property Prev_Item_exice() As Double
            Get
                Prev_Item_exice = _Prev_Item_exice
            End Get
            Set(ByVal value As Double)
                _Prev_Item_exice = value
            End Set
        End Property

        Public Property Item_vat() As Double
            Get
                Item_vat = _Item_vat
            End Get
            Set(ByVal value As Double)
                _Item_vat = value
            End Set
        End Property

        Public Property Item_exice() As Double
            Get
                Item_exice = _Item_exice
            End Get
            Set(ByVal value As Double)
                _Item_exice = value
            End Set
        End Property

        Public Property Batch_No() As String
            Get
                Batch_No = _Batch_No
            End Get
            Set(ByVal value As String)
                _Batch_No = value
            End Set
        End Property

        Public Property Expiry_Date() As DateTime
            Get
                Expiry_Date = _Expiry_Date
            End Get
            Set(ByVal value As DateTime)
                _Expiry_Date = value
            End Set
        End Property

        Public Property StockDetail_Id() As Double
            Get
                StockDetail_Id = _StockDetail_Id
            End Get
            Set(ByVal value As Double)
                _StockDetail_Id = value
            End Set
        End Property
        Public Property TransType() As Double
            Get
                TransType = _TransType
            End Get
            Set(ByVal value As Double)
                _TransType = value
            End Set
        End Property
        Public Property MRNId() As Double
            Get
                MRNId = _MRNId
            End Get
            Set(ByVal value As Double)
                _MRNId = value
            End Set
        End Property
        Public Property CostCenter_ID() As Decimal
            Get
                CostCenter_ID = _CostCenter_id
            End Get
            Set(ByVal value As Decimal)
                _CostCenter_id = value
            End Set
        End Property
    End Class

    Public Class cls_ReverseMaterial_Recieved_Against_Po_Master
        Inherits CommonClass
        Public Sub insert_ReverseMATERIAL_RECIEVED_Against_PO_MASTER(ByVal clsObj As cls_ReverseMaterial_Recieved_Against_Po_Master_Prop, ByVal cmd As SqlCommand)

            ' cmd = New SqlClient.SqlCommand
            ' cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_RECIEVED_Against_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Reverse_ID", clsObj.Reverse_ID)
            cmd.Parameters.AddWithValue("@V_Reverse_Code", clsObj.Reverse_Code)
            cmd.Parameters.AddWithValue("@V_Reverse_No", clsObj.Reverse_No)
            cmd.Parameters.AddWithValue("@V_Reverse_Date", clsObj.Reverse_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_received_ID", clsObj.received_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            ' cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub
        Public Sub insert_ReverseMATERIAL_RECIEVED_Against_PO_Detail(ByVal clsObj As cls_ReverseMaterial_Recieved_Against_Po_Master_Prop, ByVal cmd As SqlCommand)

            ' cmd = New SqlClient.SqlCommand
            ' cmd.Connection = con
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_REVERSEMATERIAL_RECEIVED_Against_PO_DETAIL"

            cmd.Parameters.AddWithValue("@v_Reverse_ID", clsObj.Reverse_ID)
            cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@v_Prev_Item_Qty", clsObj.Prev_Item_Qty)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@v_Prev_Item_Rate", clsObj.Prev_Item_Rate)
            cmd.Parameters.AddWithValue("@v_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@v_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@v_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@v_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@v_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@v_Division_Id", clsObj.Division_ID)

            cmd.Parameters.AddWithValue("@v_Prev_Item_vat", clsObj.Prev_Item_vat)
            cmd.Parameters.AddWithValue("@v_Prev_Item_exice", clsObj.Prev_Item_exice)
            cmd.Parameters.AddWithValue("@v_Item_vat", clsObj.Item_vat)
            cmd.Parameters.AddWithValue("@v_Item_exice", clsObj.Item_exice)
            cmd.Parameters.AddWithValue("@v_Batch_No", clsObj.Batch_No)

            cmd.Parameters.AddWithValue("@v_Expiry_Date", clsObj.Expiry_Date)
            cmd.Parameters.AddWithValue("@v_Stock_Detail_Id", clsObj.StockDetail_Id)

            cmd.Parameters.AddWithValue("@V_TransactionType", clsObj.TransType)
            cmd.Parameters.AddWithValue("@V_MrnId", clsObj.MRNId)

            cmd.Parameters.AddWithValue("@v_Proc_Type", 1)

            cmd.ExecuteNonQuery()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub



        Public Sub update_ReverseMATERIAL_RECIEVED_Against_PO_MASTER(ByVal clsObj As cls_ReverseMaterial_Recieved_Against_Po_Master_Prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_RECIEVED_Against_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Reverse_ID", clsObj.Reverse_ID)
            cmd.Parameters.AddWithValue("@V_Reverse_Code", clsObj.Reverse_Code)
            cmd.Parameters.AddWithValue("@V_Reverse_No", clsObj.Reverse_No)
            cmd.Parameters.AddWithValue("@V_Reverse_Date", clsObj.Reverse_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_received_ID", clsObj.received_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub delete_ReverseMATERIAL_RECIEVED_Against_PO_MASTER(ByVal clsObj As cls_ReverseMaterial_Recieved_Against_Po_Master_Prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_RECIEVED_Against_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Reverse_ID", clsObj.Reverse_ID)
            cmd.Parameters.AddWithValue("@V_Reverse_Code", clsObj.Reverse_Code)
            cmd.Parameters.AddWithValue("@V_Reverse_No", clsObj.Reverse_No)
            cmd.Parameters.AddWithValue("@V_Reverse_Date", clsObj.Reverse_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_received_ID", clsObj.received_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub insert_ReverseNON_STOCKABLE_MATERIAL_RECIEVED_WITHOUT_PO(ByVal clsObj As cls_ReverseMaterial_Recieved_Against_Po_Master_Prop, ByVal cmd As SqlCommand)

            ' cmd = New SqlClient.SqlCommand
            ' cmd.Connection = con
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_REVERSE_NON_STOCKABLE_MATERIAL_RECEIVED_AGAINST_PO"

            cmd.Parameters.AddWithValue("@v_Reverse_ID", clsObj.Reverse_ID)
            cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@v_CostCenter_ID", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@v_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@v_Item_vat", clsObj.Item_vat)
            cmd.Parameters.AddWithValue("@v_Item_exice", clsObj.Item_exice)
            cmd.Parameters.AddWithValue("@v_Prev_Item_Qty", clsObj.Prev_Item_Qty)
            cmd.Parameters.AddWithValue("@v_Prev_Item_Rate", clsObj.Prev_Item_Rate)
            cmd.Parameters.AddWithValue("@v_Prev_Item_vat", clsObj.Prev_Item_vat)
            cmd.Parameters.AddWithValue("@v_Prev_Item_exice", clsObj.Prev_Item_exice)
            cmd.Parameters.AddWithValue("@v_Batch_No", clsObj.Batch_No)
            cmd.Parameters.AddWithValue("@v_Expiry_Date", clsObj.Expiry_Date)
            cmd.Parameters.AddWithValue("@V_MrnId", clsObj.MRNId)

            cmd.ExecuteNonQuery()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

    End Class
End Namespace
