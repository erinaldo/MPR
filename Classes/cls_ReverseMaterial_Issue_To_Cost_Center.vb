Imports System
Imports System.Data
Imports System.Data.SqlClient




Namespace Reversematerial_issue_to_cost_center_master


    Public Class cls_Reversematerial_issue_to_cost_center_master_prop


        Dim _RMIO_ID As Decimal
        Dim _ITEM_ID As Integer
        Dim _RMIO_CODE As String
        Dim _RMIO_NO As Integer
        Dim _RMIO_DATE As Date
        Dim _Issue_ID As Integer
        Dim _RMIO_REMARKS As String
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As Date
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As Date
        Dim _DIVISION_ID As Int32
        Dim _ITEM_QTY As Double
        Dim _ISSUED_QTY As Double
        Dim _RETURNED_QTY As Int32
        Dim _AVG_RATE As Double
        Dim _StockDetailId As Integer
        Dim _TransDate As Date
        Dim _TransType As Integer






        Public Property RMIO_ID() As Double
            Get
                RMIO_ID = _RMIO_ID
            End Get
            Set(ByVal value As Double)
                _RMIO_ID = value
            End Set
        End Property

        Public Property ITEM_ID() As Integer
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Integer)
                _ITEM_ID = value
            End Set
        End Property



        Public Property RMIO_CODE() As String
            Get
                RMIO_CODE = _RMIO_CODE
            End Get
            Set(ByVal value As String)
                _RMIO_CODE = value
            End Set
        End Property

        Public Property RMIO_NO() As Integer
            Get
                RMIO_NO = _RMIO_NO
            End Get
            Set(ByVal value As Integer)
                _RMIO_NO = value
            End Set
        End Property

        Public Property RMIO_DATE() As Date
            Get
                RMIO_DATE = _RMIO_DATE
            End Get
            Set(ByVal value As Date)
                _RMIO_DATE = value
            End Set
        End Property

        Public Property ISSUE_ID() As Integer
            Get
                ISSUE_ID = _Issue_ID
            End Get
            Set(ByVal value As Integer)
                _Issue_ID = value
            End Set
        End Property

        Public Property RMIO_REMARKS() As String
            Get
                RMIO_REMARKS = _RMIO_REMARKS
            End Get
            Set(ByVal value As String)
                _RMIO_REMARKS = value
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

        Public Property CREATION_DATE() As Date
            Get
                CREATION_DATE = _CREATION_DATE
            End Get
            Set(ByVal value As Date)
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

        Public Property MODIFIED_DATE() As Date
            Get
                MODIFIED_DATE = _MODIFIED_DATE
            End Get
            Set(ByVal value As Date)
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

        Public Property ITEM_QTY() As Double
            Get
                ITEM_QTY = _ITEM_QTY
            End Get
            Set(ByVal value As Double)
                _ITEM_QTY = value
            End Set
        End Property

        Public Property ISSUED_QTY() As Double
            Get
                ISSUED_QTY = _ISSUED_QTY
            End Get
            Set(ByVal value As Double)
                _ISSUED_QTY = value
            End Set
        End Property

        Public Property RETURNED_QTY() As Double
            Get
                RETURNED_QTY = _RETURNED_QTY
            End Get
            Set(ByVal value As Double)
                _RETURNED_QTY = value
            End Set
        End Property

        Public Property AVG_RATE() As Double
            Get
                AVG_RATE = _AVG_RATE
            End Get
            Set(ByVal value As Double)
                _AVG_RATE = value
            End Set
        End Property

        Public Property Stock_Detail_Id() As Integer
            Get
                Stock_Detail_Id = _StockDetailId
            End Get
            Set(ByVal value As Integer)
                _StockDetailId = value
            End Set
        End Property


        Public Property TransDate() As Date
            Get
                TransDate = _TransDate
            End Get
            Set(ByVal value As Date)
                _TransDate = value
            End Set
        End Property


        Public Property TransType() As Integer
            Get
                TransType = _TransType
            End Get
            Set(ByVal value As Integer)
                _TransType = value
            End Set
        End Property
    End Class

    Public Class cls_Reversematerial_issue_to_cost_center_master
        Inherits CommonClass

        Public Sub insert_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER(ByVal clsObj As cls_Reversematerial_issue_to_cost_center_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER"

            cmd.Parameters.AddWithValue("@V_RMIO_ID", clsObj.RMIO_ID)
            cmd.Parameters.AddWithValue("@V_RMIO_CODE", clsObj.RMIO_CODE)
            cmd.Parameters.AddWithValue("@V_RMIO_NO", clsObj.RMIO_NO)
            cmd.Parameters.AddWithValue("@V_RMIO_DATE", clsObj.RMIO_DATE)
            cmd.Parameters.AddWithValue("@V_Issue_ID", clsObj.ISSUE_ID)
            cmd.Parameters.AddWithValue("@V_RMIO_REMARKS", clsObj.RMIO_REMARKS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub
        Public Sub insert_ReverseMATERIAL_ISSUE_TO_COST_CENTER_Detail(ByVal clsObj As cls_Reversematerial_issue_to_cost_center_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_ISSUE_TO_COST_CENTER_Detail"

            cmd.Parameters.AddWithValue("@V_RMIO_ID", clsObj.RMIO_ID)
            cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", clsObj.Stock_Detail_Id)
            cmd.Parameters.AddWithValue("@V_Issue_ID", clsObj.ISSUE_ID)
            cmd.Parameters.AddWithValue("@V_Item_Id", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@V_Avg_Rate", clsObj.AVG_RATE)
            cmd.Parameters.AddWithValue("@v_TransDate", clsObj.TransDate)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.ITEM_QTY)
            cmd.Parameters.AddWithValue("@V_TransactionType", clsObj.TransType)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()

            'cmd.Parameters.Clear()
            'cmd.CommandText = "UPDATE_STOCK_DETAIL_ReturnISSUE"
            'cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", clsObj.Stock_Detail_Id)
            'cmd.Parameters.AddWithValue("@V_Item_Id", clsObj.ITEM_ID)
            'cmd.Parameters.AddWithValue("@V_Avg_Rate", clsObj.AVG_RATE)
            'cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)



            cmd.Dispose()

        End Sub

        Public Sub update_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER(ByVal clsObj As cls_Reversematerial_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER"

            cmd.Parameters.AddWithValue("@V_MIO_ID", clsObj.RMIO_ID)
            cmd.Parameters.AddWithValue("@V_MIO_CODE", clsObj.RMIO_CODE)
            cmd.Parameters.AddWithValue("@V_MIO_NO", clsObj.RMIO_NO)
            cmd.Parameters.AddWithValue("@V_MIO_DATE", clsObj.RMIO_DATE)
            cmd.Parameters.AddWithValue("@V_CS_ID", clsObj.ISSUE_ID)
            cmd.Parameters.AddWithValue("@V_MIO_REMARKS", clsObj.RMIO_REMARKS)
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
        Public Sub UPDATE_ReverseMATERIAL_ISSUE_TO_COST_CENTER_DETAIL(ByVal clsObj As cls_Reversematerial_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MIO_ISSUE_ACCEPTED"
            cmd.Parameters.AddWithValue("@MIO_ID", clsObj.RMIO_ID)
            cmd.Parameters.AddWithValue("@ITEM_ID", clsObj.ITEM_ID)
            
            cmd.Parameters.AddWithValue("@MODE", 2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub delete_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER(ByVal clsObj As cls_Reversematerial_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER"

            cmd.Parameters.AddWithValue("@V_MIO_ID", clsObj.RMIO_ID)
            cmd.Parameters.AddWithValue("@V_MIO_CODE", clsObj.RMIO_CODE)
            cmd.Parameters.AddWithValue("@V_MIO_NO", clsObj.RMIO_NO)
            cmd.Parameters.AddWithValue("@V_MIO_DATE", clsObj.RMIO_DATE)
            cmd.Parameters.AddWithValue("@V_CS_ID", clsObj.ISSUE_ID)
            cmd.Parameters.AddWithValue("@V_MIO_REMARKS", clsObj.RMIO_REMARKS)
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
        Public Sub GET_ReverseMATERIAL_ISSUE(ByVal clsObj As cls_Reversematerial_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_MATERIAL_ISSUE"
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub
        '[GET_MATERIAL_ISSUE_TO_COST_CENTER_MASTER]

    End Class
End Namespace
