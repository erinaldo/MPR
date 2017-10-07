Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace wastage_master
    Public Class cls_wastage_master_prop
        Dim _Wastage_ID As Double
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
        Dim _Balance_Qty As Double
        Dim _Stock_Detail_Id As Double
        Dim _WastageItem As DataTable

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
        Public Property Balance_Qty() As Double
            Get
                Balance_Qty = _Balance_Qty
            End Get
            Set(ByVal value As Double)
                _Balance_Qty = value
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
    End Class

    Public Class cls_wastage_master
        Inherits CommonClass

        Public Function Insert_Wastage_MasterTran(ByVal clsObj As cls_wastage_master_prop) As String
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
                ' 1. Insert in Wastage_Master Table with transaction.
                ' 2. Insert in Wastage_Detail Table with Transaction.
                ' 3. Update in STOCK_DETAIL Table with  Transaction
                ' 4. Insert in Transaction_Log Table with  Transaction
                ' </summary>
                '   
                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = Trans
                Wastage_Code = clsObj.Wastage_Code
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_WASTAGE_MASTER"
                cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
                cmd.Parameters.AddWithValue("@V_Wastage_Code", clsObj.Wastage_Code)
                cmd.Parameters.AddWithValue("@V_Wastage_No", clsObj.Wastage_No)
                cmd.Parameters.AddWithValue("@V_Wastage_Date", clsObj.Wastage_Date)
                cmd.Parameters.AddWithValue("@V_Wastage_Remarks", clsObj.Wastage_Remarks)
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
                    If Convert.ToDouble(dt.Rows(iRow)("Wastage_Qty") > 0.0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_WASTAGE_DETAIL"
                        cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
                        cmd.Parameters.AddWithValue("@V_Item_ID", dt.Rows(iRow)("Item_id"))
                        cmd.Parameters.AddWithValue("@V_Stock_Detail_Id", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@V_Item_Qty", dt.Rows(iRow)("Wastage_Qty"))
                        ''NEW fIELD
                        cmd.Parameters.AddWithValue("@v_Item_Rate", dt.Rows(iRow)("Item_Rate"))
                        '' NEW FIELD
                        cmd.Parameters.AddWithValue("@v_Balance_Qty", dt.Rows(iRow)("Wastage_Qty"))
                        cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_BY)
                        cmd.Parameters.AddWithValue("@V_Creation_Date", now)
                        cmd.Parameters.AddWithValue("@V_Modified_By", v_the_current_logged_in_user_name)
                        cmd.Parameters.AddWithValue("@V_Modified_Date", NULL_DATE)
                        cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                        cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                        cmd.ExecuteNonQuery()
                    End If
                Next iRow
                Dim iRowSD As Int32
                dt = clsObj.WastageItem
                For iRowSD = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRowSD)("Wastage_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_STOCK_DETAIL_ISSUE"
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRowSD)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", dt.Rows(iRowSD)("Wastage_Qty"))
                        cmd.ExecuteNonQuery()
                    End If
                Next iRowSD

                Dim iRowTL As Int32
                dt = clsObj.WastageItem
                For iRowTL = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRowTL)("Wastage_Qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.Wastage_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(iRowTL)("Item_id"))
                        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.Wastage)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRowTL)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@Quantity", dt.Rows(iRowTL)("Wastage_Qty"))
                        cmd.Parameters.AddWithValue("@Transaction_Date", now)
                        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        cmd.ExecuteNonQuery()
                    End If
                Next iRowTL

                Trans.Commit()
                Return "Record saved successfully with code ( " + Wastage_Code + "  )"
                cmd.Dispose()
            Catch ex As Exception
                Trans.Rollback()
                Return ex.Message
            End Try
        End Function

        Public Sub Insert_Wastage_Master(ByVal clsObj As cls_wastage_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_WASTAGE_MASTER"
            cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
            cmd.Parameters.AddWithValue("@V_Wastage_Code", clsObj.Wastage_Code)
            cmd.Parameters.AddWithValue("@V_Wastage_No", clsObj.Wastage_No)
            cmd.Parameters.AddWithValue("@V_Wastage_Date", clsObj.Wastage_Date)
            cmd.Parameters.AddWithValue("@V_Wastage_Remarks", clsObj.Wastage_Remarks)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

        Public Sub Insert_Wastage_Detail(ByVal clsObj As cls_wastage_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_WASTAGE_DETAIL"
            cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
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

        Public Sub Update_Wastage_Master(ByVal clsObj As cls_wastage_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_WASTAGE_MASTER"
            cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
            cmd.Parameters.AddWithValue("@V_Wastage_Code", clsObj.Wastage_Code)
            cmd.Parameters.AddWithValue("@V_Wastage_No", clsObj.Wastage_No)
            cmd.Parameters.AddWithValue("@V_Wastage_Date", clsObj.Wastage_Date)
            cmd.Parameters.AddWithValue("@V_Wastage_Remarks", clsObj.Wastage_Remarks)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub Delete_WASTAGE_MASTER(ByVal clsObj As cls_wastage_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_WASTAGE_MASTER"
            cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
            cmd.Parameters.AddWithValue("@V_Wastage_Code", clsObj.Wastage_Code)
            cmd.Parameters.AddWithValue("@V_Wastage_No", clsObj.Wastage_No)
            cmd.Parameters.AddWithValue("@V_Wastage_Date", clsObj.Wastage_Date)
            cmd.Parameters.AddWithValue("@V_Wastage_Remarks", clsObj.Wastage_Remarks)
            cmd.Parameters.AddWithValue("@V_Created_BY", clsObj.Created_BY)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_BY", clsObj.Modified_BY)
            cmd.Parameters.AddWithValue("@V_Modified_Date", clsObj.Modified_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
            cmd.ExecuteNonQuery()

        End Sub

        Public Sub Delete_Wastage_Detail(ByVal clsObj As cls_wastage_master_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_WASTAGE_DETAIL"
            cmd.Parameters.AddWithValue("@V_Wastage_ID", clsObj.Wastage_ID)
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

        Public Sub GET_WASTAGE_MASTER(ByVal clsObj As cls_wastage_master_prop)
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
