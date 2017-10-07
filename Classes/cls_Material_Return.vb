Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace material_return_master

    Public Class cls_material_return_master_prop

        Dim _Return_ID As Double
        Dim _Return_Code As String
        Dim _Return_No As Double
        Dim _Return_Date As DateTime
        Dim _Return_Warehouse_ID As Int16
        Dim _Return_status As Int32
        Dim _Return_Remarks As String
        Dim _Created_By As String
        Dim _Creation_Date As DateTime
        Dim _Received_Date As DateTime
        Dim _Freezed_Date As DateTime
        Dim _Modified_By As String
        Dim _Modified_Date As DateTime
        Dim _Division_ID As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Accepted_Qty As Double
        Dim _Returned_Qty As Double
        Dim _Transfer_Rate As Double
        Dim _MIO_ID As Int16
        Dim _Batch_no As String
        Dim _Expiry_Date As Date



        Public Property Return_ID() As Integer
            Get
                Return_ID = _Return_ID
            End Get
            Set(ByVal value As Integer)
                _Return_ID = value
            End Set
        End Property

        Public Property Return_Code() As String
            Get
                Return_Code = _Return_Code
            End Get
            Set(ByVal value As String)
                _Return_Code = value
            End Set
        End Property

        Public Property Return_No() As Double
            Get
                Return_No = _Return_No
            End Get
            Set(ByVal value As Double)
                _Return_No = value
            End Set
        End Property

        Public Property Return_Date() As DateTime
            Get
                Return_Date = _Return_Date
            End Get
            Set(ByVal value As DateTime)
                _Return_Date = value
            End Set
        End Property

        Public Property Return_Warehouse_ID() As Int16
            Get
                Return_Warehouse_ID = _Return_Warehouse_ID
            End Get
            Set(ByVal value As Int16)
                _Return_Warehouse_ID = value
            End Set
        End Property

        Public Property Return_Status() As Int16
            Get
                Return_Status = _Return_status
            End Get
            Set(ByVal value As Int16)
                _Return_status = value
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

        Public Property Freezed_date() As DateTime
            Get
                Freezed_date = _Freezed_Date
            End Get
            Set(ByVal value As DateTime)
                _Freezed_Date = value
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

        Public Property Modified_Date() As DateTime
            Get
                Modified_Date = _Modified_Date
            End Get
            Set(ByVal value As DateTime)
                _Modified_Date = value
            End Set
        End Property

        Public Property Remarks() As String
            Get
                Remarks = _Return_Remarks
            End Get
            Set(ByVal value As String)
                _Return_Remarks = value
            End Set
        End Property
        
        Public Property Batch_No() As String
            Get
                Batch_No = _Batch_no
            End Get
            Set(ByVal value As String)
                _Batch_no = value
            End Set
        End Property

        Public Property Expiry_Date() As Date
            Get
                Expiry_Date = _Expiry_Date
            End Get
            Set(ByVal value As Date)
                _Expiry_Date = value
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

        Public Property Modified_By() As String
            Get
                Modified_By = _Modified_By
            End Get
            Set(ByVal value As String)
                _Modified_By = value
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

        Public Property MIO_ID() As Int16
            Get
                MIO_ID = _MIO_ID
            End Get
            Set(ByVal value As Int16)
                _MIO_ID = value
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

        Public Property Accepted_qty() As Double
            Get
                Accepted_qty = _Accepted_Qty
            End Get
            Set(ByVal value As Double)
                _Accepted_Qty = value
            End Set
        End Property

        Public Property Returned_qty() As Double
            Get
                Returned_qty = _Returned_Qty
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


    End Class

    Public Class cls_material_return_master
        Inherits CommonClass

        Public Sub insert_MATERIAL_RETURN_MASTER(ByVal clsObj As cls_material_return_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RETURN_MASTER"

            cmd.Parameters.AddWithValue("@v_Return_ID", clsObj.Return_ID)
            cmd.Parameters.AddWithValue("@v_Return_Code", clsObj.Return_Code)
            cmd.Parameters.AddWithValue("@v_Return_No", clsObj.Return_No)
            cmd.Parameters.AddWithValue("@v_Return_Date", clsObj.Return_Date)
            cmd.Parameters.AddWithValue("@v_Return_Warehouse_ID", clsObj.Return_Warehouse_ID)
            cmd.Parameters.AddWithValue("@v_Return_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@v_Received_Date", clsObj.Received_Date)
            cmd.Parameters.AddWithValue("@v_Freezed_Date", clsObj.Freezed_date)
            cmd.Parameters.AddWithValue("@v_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@@v_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@v_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@v_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@v_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Return_status", clsObj.Return_Status)
            cmd.Parameters.AddWithValue("@v_mio_id", clsObj.MIO_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub insert_MATERIAL_RETURN_DETAIL(ByVal clsObj As cls_material_return_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL"

            'cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
            'cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            'cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            'cmd.Parameters.AddWithValue("@V_Item_Rate", clsObj.Item_Rate)
            'cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            'cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            'cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            'cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            'cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            'cmd.Parameters.AddWithValue("@V_Item_vat", clsObj.Item_vat)
            'cmd.Parameters.AddWithValue("@V_Item_exice", clsObj.Item_exice)
            'cmd.Parameters.AddWithValue("@V_Batch_No", clsObj.Batch_No)
            'cmd.Parameters.AddWithValue("@V_Expiry_Date", clsObj.Expiry_Date)
            'cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.MaterialReceivedWithoutPO)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub update_MATERIAL_RETURN_MASTER(ByVal clsObj As cls_material_return_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER"

            cmd.Parameters.AddWithValue("@v_Return_ID", clsObj.Return_ID)
            cmd.Parameters.AddWithValue("@v_Return_Code", clsObj.Return_Code)
            cmd.Parameters.AddWithValue("@v_Return_No", clsObj.Return_No)
            cmd.Parameters.AddWithValue("@v_Return_Date", clsObj.Return_Date)
            cmd.Parameters.AddWithValue("@v_Return_Warehouse_ID", clsObj.Return_Warehouse_ID)
            cmd.Parameters.AddWithValue("@v_Return_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@v_Received_Date", clsObj.Received_Date)
            cmd.Parameters.AddWithValue("@v_Freezed_Date", clsObj.Freezed_date)
            cmd.Parameters.AddWithValue("@v_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@@v_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@v_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@v_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@v_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Return_status", clsObj.Return_Status)
            cmd.Parameters.AddWithValue("@v_mio_id", clsObj.MIO_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub delete_MATERIAL_RETURN_MASTER(ByVal clsObj As cls_material_return_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER"

            'cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
            'cmd.Parameters.AddWithValue("@V_Received_Code", clsObj.Received_Code)
            'cmd.Parameters.AddWithValue("@V_Received_No", clsObj.Received_No)
            'cmd.Parameters.AddWithValue("@V_Received_Date", clsObj.Received_Date)
            'cmd.Parameters.AddWithValue("@V_Purchase_Type", clsObj.Purchase_Type)
            'cmd.Parameters.AddWithValue("@V_Vendor_ID", clsObj.Vendor_ID)
            'cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            'cmd.Parameters.AddWithValue("@V_Po_ID", clsObj.Po_ID)
            'cmd.Parameters.AddWithValue("@V_MRN_PREFIX", clsObj.MRN_PREFIX)
            'cmd.Parameters.AddWithValue("@V_MRN_NO", clsObj.MRN_NO)
            'cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            'cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            'cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            'cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            'cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            'cmd.Parameters.AddWithValue("@V_mrn_status", clsObj.mrn_status)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub
    End Class
End Namespace

