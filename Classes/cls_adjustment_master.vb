Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace Adjustment_master
    Public Class cls_adjustment_master_prop
        Dim _adjustment_ID As Double
        Dim _adjustment_Code As String
        Dim _adjustment_No As Double
        Dim _adjustment_Date As DateTime
        Dim _adjustment_Remarks As String
        Dim _Created_BY As String
        Dim _Creation_Date As DateTime
        Dim _Modified_BY As String
        Dim _Modified_Date As DateTime
        Dim _Division_ID As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Balance_Qty As Double
        Dim _Stock_Detail_Id As Double
        Dim _adjustmentItem As DataTable

        Public Property Stock_Detail_Id() As Double
            Get
                Stock_Detail_Id = _Stock_Detail_Id
            End Get
            Set(ByVal value As Double)
                _Stock_Detail_Id = value
            End Set
        End Property
        Public Property adjustment_ID() As Double
            Get
                adjustment_ID = _adjustment_ID
            End Get
            Set(ByVal value As Double)
                _adjustment_ID = value
            End Set
        End Property

        Public Property adjustment_Code() As String
            Get
                adjustment_Code = _adjustment_Code
            End Get
            Set(ByVal value As String)
                _adjustment_Code = value
            End Set
        End Property

        Public Property adjustment_No() As Double
            Get
                adjustment_No = _adjustment_No
            End Get
            Set(ByVal value As Double)
                _adjustment_No = value
            End Set
        End Property

        Public Property adjustment_Date() As DateTime
            Get
                adjustment_Date = _adjustment_Date
            End Get
            Set(ByVal value As DateTime)
                _adjustment_Date = value
            End Set
        End Property

        Public Property adjustment_Remarks() As String
            Get
                adjustment_Remarks = _adjustment_Remarks
            End Get
            Set(ByVal value As String)
                _adjustment_Remarks = value
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
        Public Property adjustmentItem() As DataTable
            Get
                Return _adjustmentItem
            End Get

            Set(ByVal value As DataTable)
                _adjustmentItem = value

            End Set
        End Property
    End Class

    Public Class cls_adjustment_master
        Inherits CommonClass

        Public Function Insert_adjustment_MasterTran(ByVal clsObj As cls_adjustment_master_prop) As String
            Dim Trans As SqlTransaction
            Dim adjustment_Code As String = ""
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Trans = con.BeginTransaction
            Try
                ' <summary>
                ' Sieps for Saving adjustment Item
                ' 
                ' 1. Insert in adjustment_Master Table with transaction.
                ' 2. Insert in adjustment_Detail Table with Transaction.
                ' 3. Update in STOCK_DETAIL Table with  Transaction.
                ' 4. Insert in Transaction_Log Table with  Transaction.
                ' </summary>
                '   
                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = Trans
                adjustment_Code = clsObj.adjustment_Code
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_ADJUSTMENT_MASTER"
                cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
                cmd.Parameters.AddWithValue("@V_adjustment_Code", clsObj.adjustment_Code)
                cmd.Parameters.AddWithValue("@V_adjustment_No", clsObj.adjustment_No)
                cmd.Parameters.AddWithValue("@V_adjustment_Date", clsObj.adjustment_Date)
                cmd.Parameters.AddWithValue("@V_adjustment_Remarks", clsObj.adjustment_Remarks)
                cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
                cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
                cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
                cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
                cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                cmd.ExecuteNonQuery()

                Dim dt As DataTable
                Dim iRow As Int32
                dt = clsObj.adjustmentItem
                For iRow = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToDouble(dt.Rows(iRow)("adjustment_Qty") <> 0.0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_ADJUSTMENT_DETAIL"
                        cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
                        cmd.Parameters.AddWithValue("@V_Item_ID", dt.Rows(iRow)("Item_id"))
                        cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@V_Item_Qty", dt.Rows(iRow)("adjustment_Qty"))
                        ''NEW fIELD
                        cmd.Parameters.AddWithValue("@v_Item_Rate", dt.Rows(iRow)("Item_Rate"))
                        '' NEW FIELD
                        cmd.Parameters.AddWithValue("@v_Balance_Qty", dt.Rows(iRow)("adjustment_Qty"))
                        cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
                        cmd.Parameters.AddWithValue("@V_Creation_Date", Now)
                        cmd.Parameters.AddWithValue("@V_Modified_By", v_the_current_logged_in_user_name)
                        cmd.Parameters.AddWithValue("@V_Modified_Date", NULL_DATE)
                        cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                        cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                        cmd.ExecuteNonQuery()
                    End If

                Next iRow

                cmd.Parameters.Clear()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_ADJUSTMENT_DETAIL"
                cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
                cmd.Parameters.AddWithValue("@V_Item_ID", dt.Rows(0)("Item_id"))
                cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", dt.Rows(0)("Stock_Detail_Id"))
                cmd.Parameters.AddWithValue("@V_Item_Qty", dt.Rows(0)("adjustment_Qty"))
                ''NEW fIELD
                cmd.Parameters.AddWithValue("@v_Item_Rate", dt.Rows(0)("Item_Rate"))
                '' NEW FIELD
                cmd.Parameters.AddWithValue("@v_Balance_Qty", dt.Rows(0)("adjustment_Qty"))
                cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
                cmd.Parameters.AddWithValue("@V_Creation_Date", Now)
                cmd.Parameters.AddWithValue("@V_Modified_By", v_the_current_logged_in_user_name)
                cmd.Parameters.AddWithValue("@V_Modified_Date", NULL_DATE)
                cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
                cmd.ExecuteNonQuery()


                Dim iRowSD As Int32
                dt = clsObj.adjustmentItem
                For iRowSD = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToDouble(dt.Rows(iRowSD)("adjustment_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "insert_Stock_Detail_Adjustment"
                        cmd.Parameters.AddWithValue("@item_ID", dt.Rows(iRowSD)("Item_Id"))
                        cmd.Parameters.AddWithValue("@Batch_no", dt.Rows(iRowSD)("Batch_no"))
                        cmd.Parameters.AddWithValue("@Expiry_Date", dt.Rows(iRowSD)("Expiry_date"))
                        cmd.Parameters.AddWithValue("@Item_Qty", dt.Rows(iRowSD)("adjustment_Qty"))
                        cmd.Parameters.AddWithValue("@Doc_ID", 1)
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.adjustment_ID)
                        cmd.ExecuteNonQuery()
                    End If
                    If Convert.ToDouble(dt.Rows(iRowSD)("adjustment_Qty")) < 0 Then
                        'If Math.Abs(Convert.ToDouble(dt.Rows(iRowSD)("adjustment_Qty"))) < Convert.ToDouble(dt.Rows(iRowSD)("Batch_Qty")) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "Delete_from_stock_detail_Adjustment"
                        cmd.Parameters.AddWithValue("@stock_detail_id", dt.Rows(iRowSD)("stock_detail_id"))
                        cmd.Parameters.AddWithValue("@item_id", dt.Rows(iRowSD)("Item_Id"))
                        cmd.Parameters.AddWithValue("@adjustment_qty", dt.Rows(iRowSD)("adjustment_qty"))
                        cmd.ExecuteNonQuery()
                        'Else
                        '    Trans.Rollback()
                        '    Return "Adjustment Qty. Cannot be greater than Batch Qty"
                        '    Exit Function
                        'End If

                    End If
                Next iRowSD

                Dim iRowTL As Int32
                dt = clsObj.adjustmentItem
                For iRowTL = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToDouble(dt.Rows(iRowTL)("adjustment_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.adjustment_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(iRowTL)("Item_id"))
                        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.Adjustment)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRowTL)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@Quantity", dt.Rows(iRowTL)("adjustment_Qty"))
                        cmd.Parameters.AddWithValue("@Transaction_Date", Now)
                        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        cmd.ExecuteNonQuery()
                    End If
                Next iRowTL

                Trans.Commit()
                Return "Record saved successfully with code ( " + adjustment_Code + Convert.ToString(clsObj.adjustment_No) + "  )"
                cmd.Dispose()
            Catch ex As Exception
                Trans.Rollback()
                Return ex.Message
            End Try
        End Function

        Public Sub Insert_adjustment_Master(ByVal clsObj As cls_adjustment_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_adjustment_MASTER"
            cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
            cmd.Parameters.AddWithValue("@V_adjustment_Code", clsObj.adjustment_Code)
            cmd.Parameters.AddWithValue("@V_adjustment_No", clsObj.adjustment_No)
            cmd.Parameters.AddWithValue("@V_adjustment_Date", clsObj.adjustment_Date)
            cmd.Parameters.AddWithValue("@V_adjustment_Remarks", clsObj.adjustment_Remarks)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

        Public Sub Insert_adjustment_Detail(ByVal clsObj As cls_adjustment_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ADJUSTMENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", clsObj.Stock_Detail_Id)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub Update_adjustment_Master(ByVal clsObj As cls_adjustment_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ADJUSTMENT_MASTER"
            cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
            cmd.Parameters.AddWithValue("@V_adjustment_Code", clsObj.adjustment_Code)
            cmd.Parameters.AddWithValue("@V_adjustment_No", clsObj.adjustment_No)
            cmd.Parameters.AddWithValue("@V_adjustment_Date", clsObj.adjustment_Date)
            cmd.Parameters.AddWithValue("@V_adjustment_Remarks", clsObj.adjustment_Remarks)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub Delete_adjustment_MASTER(ByVal clsObj As cls_adjustment_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ADJUSTMENT_MASTER"
            cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
            cmd.Parameters.AddWithValue("@V_adjustment_Code", clsObj.adjustment_Code)
            cmd.Parameters.AddWithValue("@V_adjustment_No", clsObj.adjustment_No)
            cmd.Parameters.AddWithValue("@V_adjustment_Date", clsObj.adjustment_Date)
            cmd.Parameters.AddWithValue("@V_adjustment_Remarks", clsObj.adjustment_Remarks)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()

        End Sub

        Public Sub Delete_adjustment_Detail(ByVal clsObj As cls_adjustment_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_ADJUSTMENT_DETAIL"
            cmd.Parameters.AddWithValue("@V_adjustment_ID", clsObj.adjustment_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub GET_adjustment_MASTER(ByVal clsObj As cls_adjustment_master_prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_adjustment_MASTER"
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

    End Class
End Namespace
