Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace Reverse_wastage_master
    Public Class cls_Reverse_wastage_master_prop
        Dim _Wastage_ID As Double
        Dim _ReverseWastage_ID As Double
        Dim _Wastage_Code As String
        Dim _Wastage_No As Double
        Dim _Wastage_Date As DateTime
        Dim _Wastage_Remarks As String
        Dim _Created_BY As String
        Dim _Creation_Date As DateTime
        Dim _Modified_BY As String
        Dim _Modified_Date As DateTime
        Dim _Division_ID As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Stock_Detail_Id As Double
        Dim _WastageItem As DataTable
        Dim _Return_Qty As Double

        Public Property Stock_Detail_Id() As Double
            Get
                Stock_Detail_Id = _Stock_Detail_Id
            End Get
            Set(ByVal value As Double)
                _Stock_Detail_Id = value
            End Set
        End Property
        Public Property Wastage_ID() As Double
            Get
                Wastage_ID = _Wastage_ID
            End Get
            Set(ByVal value As Double)
                _Wastage_ID = value
            End Set
        End Property
        Public Property ReverseWastage_ID() As Double
            Get
                ReverseWastage_ID = _ReverseWastage_ID
            End Get
            Set(ByVal value As Double)
                _ReverseWastage_ID = value
            End Set
        End Property

        Public Property Wastage_Code() As String
            Get
                Wastage_Code = _Wastage_Code
            End Get
            Set(ByVal value As String)
                _Wastage_Code = value
            End Set
        End Property

        Public Property Wastage_No() As Double
            Get
                Wastage_No = _Wastage_No
            End Get
            Set(ByVal value As Double)
                _Wastage_No = value
            End Set
        End Property

        Public Property Wastage_Date() As DateTime
            Get
                Wastage_Date = _Wastage_Date
            End Get
            Set(ByVal value As DateTime)
                _Wastage_Date = value
            End Set
        End Property

        Public Property Wastage_Remarks() As String
            Get
                Wastage_Remarks = _Wastage_Remarks
            End Get
            Set(ByVal value As String)
                _Wastage_Remarks = value
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
        Public Property WastageItem() As DataTable
            Get
                Return _WastageItem
            End Get

            Set(ByVal value As DataTable)
                _WastageItem = value

            End Set
        End Property
        Public Property Return_Qty() As Double
            Get
                Return_Qty = _Return_Qty
            End Get
            Set(ByVal value As Double)
                _Return_Qty = value
            End Set
        End Property
    End Class
    Public Class cls_Reverse_wastage_master
        Inherits CommonClass
        Public Function Insert_Reverse_Wastage_Master(ByVal clsObj As cls_Reverse_wastage_master_prop) As String
            Dim _PRW_ID As Double
            _PRW_ID = 0
            Dim commObj As New CommonClass()
            _PRW_ID = commObj.getMaxValue("p_ReverseWastage_ID", "ReverseWASTAGE_DETAIL")
            Dim Trans As SqlTransaction
            Dim Wastage_Code As String = ""
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Trans = con.BeginTransaction
            Try
                ' <summary>
                ' Sieps for Saving Wastage Item
                ' 
                ' 1. Insert in ReverseWastage_Master Table with transaction.
                ' 2. Insert in ReverseWastage_Detail Table with Transaction.
                ' 3. Update in Wastage_Detail Table (Balance_Qty) with Transaction .
                ' 3. Update in Stock_Detail Table with  Transaction
                ' 4. Insert in Transaction_Log Table with  Transaction
                ' </summary>
                '   
                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = Trans
                Wastage_Code = clsObj.Wastage_Code + Convert.ToString(clsObj.Wastage_No)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_Reverse_WASTAGE_MASTER"
                cmd.Parameters.AddWithValue("@V_Reverse_Wastage_ID", clsObj.ReverseWastage_ID)
                cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
                cmd.Parameters.AddWithValue("@V_Reverse_Wastage_Code", clsObj.Wastage_Code)
                cmd.Parameters.AddWithValue("@V_Reverse_Wastage_No", clsObj.Wastage_No)
                cmd.Parameters.AddWithValue("@V_Reverse_Wastage_Date", clsObj.Wastage_Date)
                cmd.Parameters.AddWithValue("@V_Reverse_Wastage_Remarks", clsObj.Wastage_Remarks)
                cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
                cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
                cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
                cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
                cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                cmd.ExecuteNonQuery()

                Dim dt As DataTable
                Dim iRow As Int32
                dt = clsObj.WastageItem
                For iRow = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("Actual_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_Reverse_WASTAGE_DETAIL"
                        cmd.Parameters.AddWithValue("@v_ReverseWastage_ID", _PRW_ID)
                        cmd.Parameters.AddWithValue("@v_fk_ReverseWastage_ID", clsObj.ReverseWastage_ID)
                        cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
                        cmd.Parameters.AddWithValue("@V_Item_ID", dt.Rows(iRow)("Item_id"))
                        cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@V_Item_Qty", dt.Rows(iRow)("Wastage_Qty"))
                        ''new''
                        cmd.Parameters.AddWithValue("@v_Item_Rate", dt.Rows(iRow)("Item_Rate"))
                        ''New
                        cmd.Parameters.AddWithValue("@v_Actual_Qty", dt.Rows(iRow)("Actual_Qty"))
                        cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
                        cmd.Parameters.AddWithValue("@V_Creation_Date", now)
                        cmd.Parameters.AddWithValue("@V_Modified_By", v_the_current_logged_in_user_name)
                        cmd.Parameters.AddWithValue("@V_Modified_Date", NULL_DATE)
                        cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                        cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                        cmd.ExecuteNonQuery()
                    End If

                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("Actual_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_WASTAGE_DETAIL"
                        cmd.Parameters.AddWithValue("@v_Wastage_ID", clsObj.Wastage_ID)
                        cmd.Parameters.AddWithValue("@v_Item_ID", dt.Rows(iRow)("Item_id"))
                        cmd.Parameters.AddWithValue("@v_Stock_Detail_Id", dt.Rows(iRow)("Stock_Detail_Id"))

                        cmd.Parameters.AddWithValue("@v_Actual_Qty", dt.Rows(iRow)("Actual_Qty"))
                        cmd.ExecuteNonQuery()
                    End If

                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("Actual_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_STOCK_DETAIL_RECIEVE"
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", dt.Rows(iRow)("wastage_Qty") - dt.Rows(iRow)("Actual_Qty"))
                        cmd.ExecuteNonQuery()
                    End If

                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("Actual_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.ReverseWastage_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(iRow)("Item_id"))
                        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.ReverseWastage)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@Quantity", dt.Rows(iRow)("wastage_Qty") - dt.Rows(iRow)("Actual_Qty"))
                        cmd.Parameters.AddWithValue("@Transaction_Date", now)
                        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        cmd.ExecuteNonQuery()
                    End If
                    _PRW_ID += 1
                Next iRow

                Trans.Commit()
                Return "Record saved successfully with code ( " + Wastage_Code + "  )"
                cmd.Dispose()
            Catch ex As Exception
                Trans.Rollback()
                Return ex.Message
            End Try
        End Function
        Public Sub GET_WASTAGE_MASTER(ByVal clsObj As cls_Reverse_wastage_master_prop)
            cmd = New SqlClient.SqlCommand
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_WASTAGE_MASTER"
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub
    End Class
End Namespace

