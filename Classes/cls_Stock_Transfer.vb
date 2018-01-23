
Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace stock_transfer_outlet
    Public Class cls_Stock_Transfer_prop
        Dim _TRANSFER_ID As Double
        Dim _DC_CODE As String
        Dim _DC_NO As Double
        Dim _TRANSFER_DATE As DateTime
        Dim _TRANSFER_OUTLET_ID As Int32
        Dim _TRANSFER_STATUS As Int32
        Dim _TRANSFER_REMARKS As String
        Dim _MRN_REMARKS As String
        Dim _REQ_DATE As DateTime
        Dim _RECEIVED_DATE As DateTime
        Dim _FREEZED_DATE As DateTime
        Dim _MRN_NO As Double
        Dim _MRN_PREFIX As String
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _ITEM_ID As Double
        Dim _ITEM_QTY As Double
        Dim _ACCEPTED_QTY As Double
        Dim _RETURNED_QTY As Double
        Dim _TRANSFER_RATE As Double
        Dim _TYPE As String
        Dim _dtable_Item_List As DataTable
        Dim _DataTable As DataTable

        Public Property TRANSFER_ID() As Double
            Get
                TRANSFER_ID = _TRANSFER_ID
            End Get
            Set(ByVal value As Double)
                _TRANSFER_ID = value
            End Set
        End Property

        Public Property DC_CODE() As String
            Get
                DC_CODE = _DC_CODE
            End Get
            Set(ByVal value As String)
                _DC_CODE = value
            End Set
        End Property
        Public Property DC_NO() As Double
            Get
                DC_NO = _DC_NO
            End Get
            Set(ByVal value As Double)
                _DC_NO = value
            End Set
        End Property
        Public Property TRANSFER_DATE() As DateTime
            Get
                TRANSFER_DATE = _TRANSFER_DATE
            End Get
            Set(ByVal value As DateTime)
                _TRANSFER_DATE = value
            End Set
        End Property

        Public Property TRANSFER_OUTLET_ID() As Int32
            Get
                TRANSFER_OUTLET_ID = _TRANSFER_OUTLET_ID
            End Get
            Set(ByVal value As Int32)
                _TRANSFER_OUTLET_ID = value
            End Set
        End Property

        Public Property TRANSFER_STATUS() As Int32
            Get
                TRANSFER_STATUS = _TRANSFER_STATUS
            End Get
            Set(ByVal value As Int32)
                _TRANSFER_STATUS = value
            End Set
        End Property

        Public Property RECEIVED_DATE() As DateTime
            Get
                RECEIVED_DATE = _RECEIVED_DATE
            End Get
            Set(ByVal value As DateTime)
                _RECEIVED_DATE = value
            End Set
        End Property
        Public Property FREEZED_DATE() As DateTime
            Get
                FREEZED_DATE = _FREEZED_DATE
            End Get
            Set(ByVal value As DateTime)
                _FREEZED_DATE = value
            End Set
        End Property

        Public Property MRN_NO() As Double
            Get
                MRN_NO = _MRN_NO
            End Get
            Set(ByVal value As Double)
                _MRN_NO = value
            End Set
        End Property

        Public Property MRN_PREFIX() As String
            Get
                MRN_PREFIX = _MRN_PREFIX
            End Get
            Set(ByVal value As String)
                _MRN_PREFIX = value
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

        Public Property CREATION_DATE() As DateTime
            Get
                CREATION_DATE = _CREATION_DATE
            End Get
            Set(ByVal value As DateTime)
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

        Public Property MODIFIED_DATE() As DateTime
            Get
                MODIFIED_DATE = _MODIFIED_DATE
            End Get
            Set(ByVal value As DateTime)
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

        Public Property ITEM_ID() As Double
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Double)
                _ITEM_ID = value
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
        Public Property ACCEPTED_QTY() As Double
            Get
                ACCEPTED_QTY = _ACCEPTED_QTY
            End Get
            Set(ByVal value As Double)
                _ACCEPTED_QTY = value
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
        Public Property TRANSFER_RATE() As Double
            Get
                TRANSFER_RATE = _TRANSFER_RATE
            End Get
            Set(ByVal value As Double)
                _TRANSFER_RATE = value
            End Set
        End Property

        Public Property TRANSFER_REMARKS() As String
            Get
                TRANSFER_REMARKS = _TRANSFER_REMARKS
            End Get
            Set(ByVal value As String)
                _TRANSFER_REMARKS = value
            End Set
        End Property

        Public Property MRN_REMARKS() As String
            Get
                MRN_REMARKS = _MRN_REMARKS
            End Get
            Set(ByVal value As String)
                _MRN_REMARKS = value
            End Set
        End Property
        Public Property dtable_Item_List() As DataTable
            Get
                dtable_Item_List = _dtable_Item_List
            End Get
            Set(ByVal value As DataTable)
                _dtable_Item_List = value
            End Set
        End Property

        Public Property Data_Table() As DataTable
            Get
                Data_Table = _DataTable
            End Get
            Set(ByVal value As DataTable)
                _DataTable = value
            End Set
        End Property


        Public Property TYPE() As String
            Get
                TYPE = _TYPE
            End Get
            Set(ByVal value As String)
                _TYPE = value
            End Set
        End Property
    End Class
    Public Class cls_Stock_Transfer_master
        Inherits CommonClass
        ''' <summary>
        '''   1) Insert in Stock Transfwer Master table of local database
        '''   2) insert in stock transfer detail table of local database
        '''   3) Insert in Stock Transfwer Master table of GLOBAL database
        '''   4) insert in stock transfer detail table of GLOBAL database
        '''   5) update dc_series table of local database
        '''
        '''   ALL THESE TRANSACTION MUST BE EXECUTE WITH SINGLE TARSACTION
        ''' </summary>
        ''' <param name="clsobj"></param>
        ''' <remarks></remarks>
        Public Sub Insert_STOCK_TRANSFER(ByVal clsobj As cls_Stock_Transfer_prop)
            Try

                
                Dim tran As SqlTransaction
                Dim con_global As New SqlConnection(gblDNS_Online)
                Dim trans_global As SqlTransaction

                If con_global.State <> ConnectionState.Open Then con_global.Open()
                trans_global = con_global.BeginTransaction

                If con.State = ConnectionState.Closed Then con.Open()
                tran = con.BeginTransaction()
                '   1) Insert in Stock Transfwer Master table of local database

                cmd = New SqlCommand
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_STOCK_TRANSFER_MASTER"
                cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
                cmd.Parameters.AddWithValue("@DC_Code", clsobj.DC_CODE)
                cmd.Parameters.AddWithValue("@DC_No", clsobj.DC_NO)
                cmd.Parameters.AddWithValue("@Transfer_Date", clsobj.TRANSFER_DATE)
                cmd.Parameters.AddWithValue("@Transfer_Outlet_Id", clsobj.TRANSFER_OUTLET_ID)
                cmd.Parameters.AddWithValue("@Transfer_Status_Id", clsobj.TRANSFER_STATUS)
                cmd.Parameters.AddWithValue("@TRANSFER_REMARKS", clsobj.TRANSFER_REMARKS)
                cmd.Parameters.AddWithValue("@Received_Date", clsobj.RECEIVED_DATE)
                cmd.Parameters.AddWithValue("@Freezed_Date", clsobj.FREEZED_DATE)
                cmd.Parameters.AddWithValue("@Created_By", clsobj.CREATED_BY)
                cmd.Parameters.AddWithValue("@Creation_Date", clsobj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@Division_ID", clsobj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@TYPE", clsobj.TYPE)
                cmd.Parameters.AddWithValue("@PROC_TYPE", 1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                ''   2) insert in stock transfer detail table of local database

                Try


                    Dim dt As DataTable = clsobj.dtable_Item_List
                    dt.AcceptChanges()
                    For j As Integer = 0 To dt.Rows.Count
                        Dim i As Integer
again:
                        For i = 0 To dt.Rows.Count - 1
                            If IsNumeric(dt.Rows(i)("transfer_qty")) Then
                                If Convert.ToDouble(dt.Rows(i)("transfer_qty")) <= 0 Then
                                    dt.Rows.RemoveAt(i)
                                    dt.AcceptChanges()
                                    GoTo again
                                End If
                            End If
                        Next
                    Next

                    cmd = New SqlCommand
                    For i As Integer = 0 To dt.Rows.Count - 1


                        cmd.Parameters.Clear()
                        cmd.Connection = con
                        cmd.Transaction = tran
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_STOCK_TRANSER_DETAIL"

                        cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(i)("STOCK_DETAIL_ID"))
                        cmd.Parameters.AddWithValue("@BATCH_NO", dt.Rows(i)("BATCH_NO"))
                        cmd.Parameters.AddWithValue("@EXPIRY_DATE", dt.Rows(i)("EXPIRY_DATE"))
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(i)("item_id"))
                        cmd.Parameters.AddWithValue("@Item_Qty", dt.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@ACCEPTED_QTY", 0)
                        cmd.Parameters.AddWithValue("@RETURNED_QTY", dt.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@TRANSFER_RATE", dt.Rows(i)("ITEM_RATE"))
                        cmd.Parameters.AddWithValue("@Created_By", clsobj.CREATED_BY)
                        cmd.Parameters.AddWithValue("@Creation_Date", clsobj.CREATION_DATE)
                        cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                        cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                        cmd.Parameters.AddWithValue("@Division_Id", clsobj.DIVISION_ID)
                        cmd.Parameters.AddWithValue("@PROC_TYPE", 1)
                        cmd.ExecuteNonQuery()



                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_STOCK_DETAIL_ISSUE"
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(i)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", dt.Rows(i)("TRANSFER_QTY"))
                        cmd.ExecuteNonQuery()
                        
                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsobj.TRANSFER_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(i)("Item_id"))
                        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.Stock_Transfer_to_other_outlet)
                        cmd.Parameters.AddWithValue("@Quantity", dt.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@Transaction_Date", Now)
                        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(i)("Stock_Detail_Id"))
                        cmd.ExecuteNonQuery()
                        

                    Next

                    '3) Insert in Stock Transfwer Master table of GLOBAL database

                    cmd = New SqlCommand
                    cmd.Connection = con_global
                    cmd.Transaction = trans_global
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_STOCK_TRANSFER_MASTER"
                    cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
                    cmd.Parameters.AddWithValue("@DC_Code", clsobj.DC_CODE)
                    cmd.Parameters.AddWithValue("@DC_No", clsobj.DC_NO)
                    cmd.Parameters.AddWithValue("@Transfer_Date", clsobj.TRANSFER_DATE)
                    cmd.Parameters.AddWithValue("@Transfer_Outlet_Id", clsobj.TRANSFER_OUTLET_ID)
                    cmd.Parameters.AddWithValue("@Transfer_Status_Id", clsobj.TRANSFER_STATUS)
                    cmd.Parameters.AddWithValue("@TRANSFER_REMARKS", clsobj.TRANSFER_REMARKS)
                    cmd.Parameters.AddWithValue("@Received_Date", clsobj.RECEIVED_DATE)
                    cmd.Parameters.AddWithValue("@Freezed_Date", clsobj.FREEZED_DATE)
                    cmd.Parameters.AddWithValue("@Created_By", clsobj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@Creation_Date", clsobj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@Division_ID", clsobj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@TYPE", clsobj.TYPE)
                    cmd.Parameters.AddWithValue("@PROC_TYPE", 1)



                    cmd.ExecuteNonQuery()

                    '4) insert in stock transfer detail table of GLOBAL database

                    For i As Integer = 0 To dt.Rows.Count - 1

                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_STOCK_TRANSER_DETAIL"

                        cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(i)("STOCK_DETAIL_ID"))
                        cmd.Parameters.AddWithValue("@BATCH_NO", dt.Rows(i)("BATCH_NO"))
                        cmd.Parameters.AddWithValue("@EXPIRY_DATE", dt.Rows(i)("EXPIRY_DATE"))
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(i)("item_id"))
                        cmd.Parameters.AddWithValue("@Item_Qty", dt.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@ACCEPTED_QTY", 0)
                        cmd.Parameters.AddWithValue("@RETURNED_QTY", dt.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@TRANSFER_RATE", dt.Rows(i)("ITEM_RATE"))
                        cmd.Parameters.AddWithValue("@Created_By", clsobj.CREATED_BY)
                        cmd.Parameters.AddWithValue("@Creation_Date", clsobj.CREATION_DATE)
                        cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                        cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                        cmd.Parameters.AddWithValue("@Division_Id", clsobj.DIVISION_ID)
                        cmd.Parameters.AddWithValue("@PROC_TYPE", 1)
                        cmd.ExecuteNonQuery()

                    Next


                    '5) update dc_series table of local database
                    cmd = New SqlCommand
                    cmd.Parameters.Clear()
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandText = "update dc_series set current_used=current_used + 1 where div_id=" & clsobj.DIVISION_ID

                    cmd.ExecuteNonQuery()

                    cmd.Dispose()

                    tran.Commit()
                    trans_global.Commit()


                Catch ex As Exception
                    tran.Rollback()
                    trans_global.Rollback()
                    MsgBox(ex.Message)
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

        Public Sub INSERT_STOCK_TRANSFER_DETAIL(ByVal clsObj As cls_Stock_Transfer_prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_STOCK_TRANSER_DETAIL"

            cmd.Parameters.AddWithValue("@Transfer_ID", clsObj.TRANSFER_ID)
            cmd.Parameters.AddWithValue("@Item_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@Item_Qty", clsObj.ITEM_QTY)
            cmd.Parameters.AddWithValue("@ACCEPTED_QTY", clsObj.ACCEPTED_QTY)
            cmd.Parameters.AddWithValue("@RETURNED_QTY", clsObj.RETURNED_QTY)
            cmd.Parameters.AddWithValue("@TRANSFER_RATE", clsObj.TRANSFER_RATE)
            cmd.Parameters.AddWithValue("@Created_By", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@Creation_Date", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@Modified_By", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@Modification_Date", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@Division_Id", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Function GetDCDetail_remote(ByVal qry As String) As DataSet
            Try
                Dim con_gbl As New SqlConnection(gblDNS_Online)
                da = New SqlDataAdapter(qry, con_gbl)
                ds = New DataSet
                da.Fill(ds)
                con_gbl.Close()
                con_gbl.Dispose()
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error FillDataSet")
                Return ds
            Finally
                da.Dispose()
            End Try
        End Function

        '''' <summary>
        '''' 1) Update Online Stock transfer Master and Stock Transfer Detail
        '''' 2) Insert in Stock transfer Master table of local database
        '''' 3) Insert in Stock Transfer Detail table of local
        '''' 4) Update MRN_Series table of local database
        '''' </summary>
        '''' <param name="clsobj"></param>
        '''' <remarks></remarks>
        'Public Sub Update_Stock_Transfer(ByVal clsobj As cls_Stock_Transfer_prop)
        '    Dim stock_detail_id As Double
        '    Dim trans_global As SqlTransaction
        '    Dim tran As SqlTransaction
        '    Dim con_global As New SqlConnection(gblDNS_Online)
        '    If con_global.State <> ConnectionState.Open Then con_global.Open()
        '    trans_global = con_global.BeginTransaction
        '    tran = con.BeginTransaction()
        '    Try
        '        '1) Update Online Stock transfer Master and Stock Transfer Detail


        '        cmd = New SqlCommand
        '        cmd.Connection = con_global
        '        cmd.Transaction = trans_global
        '        cmd.CommandType = CommandType.StoredProcedure
        '        cmd.CommandText = "PROC_UPDATE_STOCK_TRANSFER"
        '        cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.Data_Table.Rows(0)("Transfer_ID"))
        '        cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
        '        cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
        '        cmd.Parameters.AddWithValue("@Transfer_Status_Id", Convert.ToInt32(GlobalModule.TRANSFER_STATUS.Accepted))
        '        cmd.Parameters.AddWithValue("@MRN_NO", clsobj.MRN_NO)
        '        cmd.Parameters.AddWithValue("@MRN_PREFIX", clsobj.MRN_PREFIX)
        '        cmd.Parameters.AddWithValue("@TRANSFER_REMARKS", clsobj.TRANSFER_REMARKS)
        '        cmd.Parameters.AddWithValue("@Recieved_Date", clsobj.RECEIVED_DATE)
        '        cmd.ExecuteNonQuery()
        '        cmd.Dispose()

        '        Dim ds As New DataSet
        '        ds = FillDataSet_Remote("SELECT item_id, item_qty, batch_no, expiry_date FROM stock_transfer_detail WHERE transfer_id=" & clsobj.Data_Table.Rows(0)("Transfer_ID") & "")

        '        Dim ds1 As DataSet = FillDataSet_Remote("Select isnull(max(Stock_Detail_ID),0) + 1 from STOCK_DETAIL")
        '        stock_detail_id = Convert.ToInt32(ds1.Tables(0).Rows(0)(0))
        '        stock_detail_id = stock_detail_id + 1

        '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '            cmd = New SqlCommand
        '            cmd.Parameters.Clear()
        '            cmd.Connection = con
        '            cmd.Transaction = tran
        '            cmd.CommandType = CommandType.StoredProcedure
        '            cmd.CommandText = "PROC_UPDATE_STOCK_DETAIL"
        '            cmd.Parameters.AddWithValue("@stock_detail_id", stock_detail_id)
        '            cmd.Parameters.AddWithValue("@Item_id", ds.Tables(0).Rows(i)("item_id"))
        '            cmd.Parameters.AddWithValue("@Batch_No", ds.Tables(0).Rows(i)("Batch_no"))
        '            cmd.Parameters.AddWithValue("@Expiry_Date", ds.Tables(0).Rows(i)("expiry_date"))
        '            cmd.Parameters.AddWithValue("@item_qty", ds.Tables(0).Rows(i)("item_qty"))
        '            cmd.Parameters.AddWithValue("@Issue_qty", 0)
        '            cmd.Parameters.AddWithValue("@Balance_qty", ds.Tables(0).Rows(i)("item_qty"))
        '            cmd.Parameters.AddWithValue("@Doc_id", clsobj.Data_Table.Rows(0)("Transfer_ID"))
        '            cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Type.Stock_Transfer_to_other_outlet)
        '            cmd.ExecuteNonQuery()
        '            cmd.Dispose()

        '            'cmd = New SqlCommand
        '            'cmd.Connection = con_global
        '            'cmd.Transaction = trans_global
        '            'cmd.CommandType = CommandType.StoredProcedure
        '            'cmd.CommandText = "PROC_UPDATE_STOCK_TRANSFER_DETAIL"
        '            'cmd.Parameters.AddWithValue("@Transfer_id", clsobj.Data_Table.Rows(0)("Transfer_ID"))
        '            'cmd.Parameters.AddWithValue("@Item_id", ds.Tables(0).Rows(i)("item_id"))
        '            'cmd.Parameters.AddWithValue("@Accepted_stk_dtl_id", stock_detail_id)
        '            'cmd.ExecuteNonQuery()
        '            'cmd.Dispose()

        '            stock_detail_id = stock_detail_id + 1
        '        Next

        '        'Try

        '        '    '2) Insert in Stock transfer Master table of local database
        '        '    cmd = New SqlCommand
        '        '    cmd.Parameters.Clear()
        '        '    cmd.Connection = con
        '        '    cmd.Transaction = tran
        '        '    cmd.CommandType = CommandType.StoredProcedure
        '        '    cmd.CommandText = "PROC_ACCEPT_STOCK_TRANSFER_MASTER"

        '        '    cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
        '        '    cmd.Parameters.AddWithValue("@DC_Code", clsobj.DC_CODE)
        '        '    cmd.Parameters.AddWithValue("@DC_No", clsobj.DC_NO)
        '        '    cmd.Parameters.AddWithValue("@Transfer_Date", clsobj.TRANSFER_DATE)
        '        '    cmd.Parameters.AddWithValue("@Transfer_Outlet_Id", clsobj.TRANSFER_OUTLET_ID)
        '        '    cmd.Parameters.AddWithValue("@Transfer_Status_Id", Convert.ToInt32(GlobalModule.TRANSFER_STATUS.Accepted))
        '        '    cmd.Parameters.AddWithValue("@TRANSFER_REMARKS", clsobj.TRANSFER_REMARKS)
        '        '    cmd.Parameters.AddWithValue("@Received_Date", clsobj.RECEIVED_DATE)
        '        '    cmd.Parameters.AddWithValue("@Freezed_Date", clsobj.FREEZED_DATE)
        '        '    cmd.Parameters.AddWithValue("@Created_By", clsobj.CREATED_BY)
        '        '    cmd.Parameters.AddWithValue("@Creation_Date", clsobj.CREATION_DATE)
        '        '    cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
        '        '    cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
        '        '    cmd.Parameters.AddWithValue("@Division_ID", clsobj.DIVISION_ID)
        '        '    cmd.Parameters.AddWithValue("@MRN_NO", clsobj.MRN_NO)
        '        '    cmd.Parameters.AddWithValue("@MRN_PREFIX", clsobj.MRN_PREFIX)



        '        '    cmd.ExecuteNonQuery()
        '        '    cmd.Dispose()

        '        '    ' 3) Insert in Stock Transfer Detail table of local

        '        '    For i As Integer = 0 To clsobj.Data_Table.Rows.Count - 1
        '        '        cmd = New SqlCommand
        '        '        cmd.Parameters.Clear()
        '        '        cmd.Connection = con
        '        '        cmd.Transaction = tran
        '        '        cmd.CommandType = CommandType.StoredProcedure
        '        '        cmd.CommandText = "PROC_ACCEPT_STOCK_TRANSFER_DETAIL"

        '        '        cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
        '        '        cmd.Parameters.AddWithValue("@Item_ID", clsobj.Data_Table.Rows(i)("Item_ID"))
        '        '        cmd.Parameters.AddWithValue("@Item_Qty", clsobj.Data_Table.Rows(i)("Item_Qty"))
        '        '        cmd.Parameters.AddWithValue("@ACCEPTED_QTY", clsobj.Data_Table.Rows(i)("Item_Qty"))
        '        '        cmd.Parameters.AddWithValue("@RETURNED_QTY", 0)
        '        '        cmd.Parameters.AddWithValue("@TRANSFER_RATE", clsobj.Data_Table.Rows(i)("TRANSFER_RATE"))
        '        '        cmd.Parameters.AddWithValue("@Created_By", clsobj.Data_Table.Rows(i)("Created_By"))
        '        '        cmd.Parameters.AddWithValue("@Creation_Date", clsobj.Data_Table.Rows(i)("Creation_Date"))
        '        '        cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
        '        '        cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
        '        '        cmd.Parameters.AddWithValue("@Division_Id", clsobj.Data_Table.Rows(i)("Division_Id"))


        '        '        cmd.ExecuteNonQuery()
        '        '        cmd.Dispose()
        '        '    Next
        '        'Catch ex As Exception
        '        '    trans_global.Rollback()
        '        '    tran.Rollback()
        '        '    Exit Sub
        '        'End Try

        '        ' 4) Update MRN_Series table of local database
        '        '

        '        cmd = New SqlCommand
        '        cmd.Parameters.Clear()
        '        cmd.Connection = con
        '        cmd.Transaction = tran
        '        cmd.CommandText = "update MRN_SERIES set CURRENT_USED=CURRENT_USED + 1 where DIV_ID=" & v_the_current_division_id

        '        cmd.ExecuteNonQuery()

        '        cmd.Dispose()


        '        trans_global.Commit()
        '        tran.Commit()

        '    Catch ex As Exception
        '        trans_global.Rollback()
        '        tran.Rollback()
        '        Exit Sub
        '    End Try
        'End Sub

        Public Sub Update_Stock_Transfer(ByVal clsobj As cls_Stock_Transfer_prop)
            Dim stock_detail_id As Double
            Dim tran As SqlTransaction

            Dim con_global As New SqlConnection(gblDNS_Online)
            Dim trans_global As SqlTransaction

            If con_global.State <> ConnectionState.Open Then con_global.Open()
            trans_global = con_global.BeginTransaction


            tran = con.BeginTransaction()
            Try


                Try

                    '''''''''''''''''''''''''''''''''''''''''''''''''''
                    '''''STEPS FOR ACCEPTING THE STOCK TRANSFER '''''''
                    '''''''''''''''''''''''''''''''''''''''''''''''''''

                    '1) UPDATE MASTER ENTRY IN GLOBAL DATABASE
                    '2) INSERT MASTER ENTRY IN LOCAL DATABASE
                    '3) INSERT NEW BATCH IN LOCAL DATABASE AND IN LOCAL DETAIL TABLE
                    '4) IT WIIL RETUN STOCK DETAIL ID
                    '5) UPDATE GLOBAL DATABASE DETAIL TABLE WITH STOCK DETAILID AND ACCEPTED QTY


                    '''''''''''''''''''''''''''''''''''''''''''''''''''
                    '''''STEPS FOR ACCEPTING THE STOCK TRANSFER '''''''
                    '''''''''''''''''''''''''''''''''''''''''''''''''''


                    ''Updating the global database's master entry
                    cmd = New SqlCommand
                    cmd.Connection = con_global
                    cmd.Transaction = trans_global
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_UPDATE_STOCK_TRANSFER"
                    cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.Data_Table.Rows(0)("Transfer_ID"))
                    cmd.Parameters.AddWithValue("@Transfer_Status_Id", Convert.ToInt32(GlobalModule.TRANSFER_STATUS.Accepted))
                    cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@MRN_NO", clsobj.MRN_NO)
                    cmd.Parameters.AddWithValue("@MRN_PREFIX", clsobj.MRN_PREFIX)
                    cmd.Parameters.AddWithValue("@MRN_remarks", clsobj.MRN_REMARKS)
                    cmd.Parameters.AddWithValue("@Recieved_Date", clsobj.RECEIVED_DATE)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()



                    '2) Insert in Stock transfer Master table of local database
                    cmd = New SqlCommand
                    cmd.Parameters.Clear()
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_ACCEPT_STOCK_TRANSFER_MASTER"

                    cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
                    cmd.Parameters.AddWithValue("@DC_Code", clsobj.DC_CODE)
                    cmd.Parameters.AddWithValue("@DC_No", clsobj.DC_NO)
                    cmd.Parameters.AddWithValue("@Transfer_Date", clsobj.TRANSFER_DATE)
                    cmd.Parameters.AddWithValue("@Transfer_Outlet_Id", clsobj.TRANSFER_OUTLET_ID)
                    cmd.Parameters.AddWithValue("@Transfer_Status_Id", Convert.ToInt32(GlobalModule.TRANSFER_STATUS.Accepted))
                    cmd.Parameters.AddWithValue("@TRANSFER_REMARKS", clsobj.TRANSFER_REMARKS)
                    cmd.Parameters.AddWithValue("@MRN_REMARKS", clsobj.MRN_REMARKS)
                    cmd.Parameters.AddWithValue("@Received_Date", Now)
                    cmd.Parameters.AddWithValue("@Freezed_Date", clsobj.FREEZED_DATE)
                    cmd.Parameters.AddWithValue("@Created_By", clsobj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@Creation_Date", clsobj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@Division_ID", clsobj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@MRN_NO", clsobj.MRN_NO)
                    cmd.Parameters.AddWithValue("@MRN_PREFIX", clsobj.MRN_PREFIX)
                    cmd.Parameters.AddWithValue("@TYPE", clsobj.TYPE)


                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                   
                    For i As Integer = 0 To clsobj.Data_Table.Rows.Count - 1

                        ' 3) Insert in Stock Transfer Detail and STOCK DETAIL table of local

                        cmd = New SqlCommand
                        cmd.Parameters.Clear()
                        cmd.Connection = con
                        cmd.Transaction = tran
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_ACCEPT_STOCK_TRANSFER_DETAIL"
                        cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.TRANSFER_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", clsobj.Data_Table.Rows(i)("Item_ID"))
                        cmd.Parameters.AddWithValue("@Item_Qty", clsobj.Data_Table.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@TRANSFER_RATE", clsobj.Data_Table.Rows(i)("ITEM_RATE"))
                        cmd.Parameters.AddWithValue("@Created_By", clsobj.Data_Table.Rows(i)("Created_By"))
                        cmd.Parameters.AddWithValue("@Creation_Date", clsobj.Data_Table.Rows(i)("Creation_Date"))
                        cmd.Parameters.AddWithValue("@Modified_By", clsobj.MODIFIED_BY)
                        cmd.Parameters.AddWithValue("@Modification_Date", clsobj.MODIFIED_DATE)
                        cmd.Parameters.AddWithValue("@Division_Id", clsobj.Data_Table.Rows(i)("Division_Id"))
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.Data_Table.Rows(i)("STOCK_DETAIL_ID"))
                        cmd.Parameters.AddWithValue("@Batch_No", clsobj.Data_Table.Rows(i)("Batch_no"))
                        cmd.Parameters.AddWithValue("@Expiry_Date", clsobj.Data_Table.Rows(i)("Expiry_date"))
                        cmd.Parameters.AddWithValue("@transaction_id", Transaction_Type.Stock_Transfer_accepted_other_outlet)

                        Dim ret_stock_detail_id As SqlParameter = New SqlParameter("@ret", SqlDbType.Int)
                        ret_stock_detail_id.Direction = ParameterDirection.ReturnValue
                        cmd.Parameters.Add(ret_stock_detail_id)


                        cmd.ExecuteNonQuery()
                        cmd.Dispose()

                        stock_detail_id = ret_stock_detail_id.Value

                        ' 3) Update Stock Transfer Detail table of GLOBAL

                        cmd = New SqlCommand
                        cmd.Connection = con_global
                        cmd.Transaction = trans_global
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_UPDATE_STOCK_TRANSFER_DETAIL"
                        cmd.Parameters.AddWithValue("@Transfer_ID", clsobj.Data_Table.Rows(i)("Transfer_ID"))
                        cmd.Parameters.AddWithValue("@ITEM_ID", clsobj.Data_Table.Rows(i)("ITEM_ID"))
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.Data_Table.Rows(i)("STOCK_DETAIL_ID"))
                        cmd.Parameters.AddWithValue("@ACCEPTED_STOCK_DETAIL_ID", stock_detail_id)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()


                    Next
                Catch ex As Exception
                    tran.Rollback()
                    Exit Sub
                End Try

              

                cmd = New SqlCommand
                cmd.Parameters.Clear()
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandText = "update MRN_SERIES set CURRENT_USED=CURRENT_USED + 1 where DIV_ID=" & v_the_current_division_id

                cmd.ExecuteNonQuery()

                cmd.Dispose()



                tran.Commit()
                trans_global.Commit()

            Catch ex As Exception
                tran.Rollback()
                trans_global.Rollback()
                Exit Sub
            End Try
        End Sub

    End Class
End Namespace