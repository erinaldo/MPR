Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace Sale_Invoice

    Public Class cls_Sale_Invoice_prop
        Dim _SI_ID As Double
        Dim _SI_CODE As String
        Dim _SI_NO As Double
        Dim _DC_GST_NO As Double
        Dim _SI_DATE As DateTime
        Dim _CUST_ID As Int32
        Dim _INVOICE_STATUS As Int32
        Dim _REMARKS As String
        Dim _PAYMENTS_REMARKS As String
        Dim _SALE_TYPE As String
        Dim _GROSS_AMOUNT As Double
        Dim _VAT_AMOUNT As Double
        Dim _NET_AMOUNT As Double
        Dim _IS_SAMPLE As Integer
        Dim _DELIVERY_NOTE_NO As String
        Dim _VAT_CST_PER As Double
        Dim _SAMPLE_ADDRESS As String
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Integer
        Dim _VEHICLE_NO As String
        Dim _SHIPP_ADD_ID As Integer
        Dim _INV_TYPE As String
        Dim _TRANSPORT As String
        Dim _LR_NO As String
        Dim _MODE As Integer
        Dim _Flag As Integer
        Dim _dtable_Item_List As DataTable
        Dim _DataTable As DataTable

        Public Property SI_ID() As Double
            Get
                SI_ID = _SI_ID
            End Get
            Set(ByVal value As Double)
                _SI_ID = value
            End Set
        End Property
        Public Property SI_CODE() As String
            Get
                SI_CODE = _SI_CODE
            End Get
            Set(ByVal value As String)
                _SI_CODE = value
            End Set
        End Property
        Public Property SI_NO() As Double
            Get
                SI_NO = _SI_NO
            End Get
            Set(ByVal value As Double)
                _SI_NO = value
            End Set
        End Property
        Public Property DC_GST_NO() As Double
            Get
                DC_GST_NO = _DC_GST_NO
            End Get
            Set(ByVal value As Double)
                _DC_GST_NO = value
            End Set
        End Property
        Public Property SI_DATE() As DateTime
            Get
                SI_DATE = _SI_DATE
            End Get
            Set(ByVal value As DateTime)
                _SI_DATE = value
            End Set
        End Property
        Public Property CUST_ID() As Int32
            Get
                CUST_ID = _CUST_ID
            End Get
            Set(ByVal value As Int32)
                _CUST_ID = value
            End Set
        End Property
        Public Property INVOICE_STATUS() As Int32
            Get
                INVOICE_STATUS = _INVOICE_STATUS
            End Get
            Set(ByVal value As Int32)
                _INVOICE_STATUS = value
            End Set
        End Property
        Public Property REMARKS() As String
            Get
                REMARKS = _REMARKS
            End Get
            Set(ByVal value As String)
                _REMARKS = value
            End Set
        End Property
        Public Property PAYMENTS_REMARKS() As String
            Get
                PAYMENTS_REMARKS = _PAYMENTS_REMARKS
            End Get
            Set(ByVal value As String)
                _PAYMENTS_REMARKS = value
            End Set
        End Property
        Public Property SALE_TYPE() As String
            Get
                SALE_TYPE = _SALE_TYPE
            End Get
            Set(ByVal value As String)
                _SALE_TYPE = value
            End Set
        End Property
        Public Property GROSS_AMOUNT() As Double
            Get
                GROSS_AMOUNT = _GROSS_AMOUNT
            End Get
            Set(ByVal value As Double)
                _GROSS_AMOUNT = value
            End Set
        End Property
        Public Property VAT_AMOUNT() As Double
            Get
                VAT_AMOUNT = _VAT_AMOUNT
            End Get
            Set(ByVal value As Double)
                _VAT_AMOUNT = value
            End Set
        End Property
        Public Property NET_AMOUNT() As Double
            Get
                NET_AMOUNT = _NET_AMOUNT
            End Get
            Set(ByVal value As Double)
                _NET_AMOUNT = value
            End Set
        End Property
        Public Property IS_SAMPLE() As Integer
            Get
                IS_SAMPLE = _IS_SAMPLE
            End Get
            Set(ByVal value As Integer)
                _IS_SAMPLE = value
            End Set
        End Property
        Public Property DELIVERY_NOTE_NO() As Double
            Get
                DELIVERY_NOTE_NO = _DELIVERY_NOTE_NO
            End Get
            Set(ByVal value As Double)
                _DELIVERY_NOTE_NO = value
            End Set
        End Property
        Public Property VAT_CST_PER() As Double
            Get
                VAT_CST_PER = _VAT_CST_PER
            End Get
            Set(ByVal value As Double)
                _VAT_CST_PER = value
            End Set
        End Property
        Public Property SAMPLE_ADDRESS() As String
            Get
                SAMPLE_ADDRESS = _SAMPLE_ADDRESS
            End Get
            Set(ByVal value As String)
                _SAMPLE_ADDRESS = value
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
        Public Property VEHICLE_NO() As String
            Get
                VEHICLE_NO = _VEHICLE_NO
            End Get
            Set(ByVal value As String)
                _VEHICLE_NO = value
            End Set
        End Property
        Public Property SHIPP_ADD_ID() As Integer
            Get
                SHIPP_ADD_ID = _SHIPP_ADD_ID
            End Get
            Set(ByVal value As Integer)
                _SHIPP_ADD_ID = value
            End Set
        End Property
        Public Property INV_TYPE() As String
            Get
                INV_TYPE = _INV_TYPE
            End Get
            Set(ByVal value As String)
                _INV_TYPE = value
            End Set
        End Property
        Public Property LR_NO() As String
            Get
                LR_NO = _LR_NO
            End Get
            Set(ByVal value As String)
                _LR_NO = value
            End Set
        End Property
        Public Property TRANSPORT() As String
            Get
                TRANSPORT = _TRANSPORT
            End Get
            Set(ByVal value As String)
                _TRANSPORT = value
            End Set
        End Property
        Public Property MODE() As Integer
            Get
                MODE = _MODE
            End Get
            Set(ByVal value As Integer)
                _MODE = value
            End Set
        End Property
        Public Property Flag() As Integer
            Get
                Flag = _Flag
            End Get
            Set(ByVal value As Integer)
                _Flag = value
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

    End Class

    Public Class cls_Sale_Invoice_master
        Inherits CommonClass

        Public Sub Insert_SALE_INVOICE_MASTER(ByVal clsobj As cls_Sale_Invoice_prop)
            Try
                Dim tran As SqlTransaction
                If con.State = ConnectionState.Closed Then con.Open()
                tran = con.BeginTransaction()
                cmd = New SqlCommand
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_OUTSIDE_SALE_MASTER_SALE_NEW"
                cmd.Parameters.AddWithValue("@v_SI_ID", clsobj.SI_ID)
                cmd.Parameters.AddWithValue("@v_SI_CODE", clsobj.SI_CODE)
                cmd.Parameters.AddWithValue("@v_SI_NO", clsobj.SI_NO)
                cmd.Parameters.AddWithValue("@v_DC_NO", clsobj.DC_GST_NO)
                cmd.Parameters.AddWithValue("@v_SI_DATE", clsobj.SI_DATE)
                cmd.Parameters.AddWithValue("@v_CUST_ID", clsobj.CUST_ID)
                cmd.Parameters.AddWithValue("@V_INVOICE_STATUS", clsobj.INVOICE_STATUS)
                cmd.Parameters.AddWithValue("@v_REMARKS", clsobj.REMARKS)
                cmd.Parameters.AddWithValue("@v_PAYMENTS_REMARKS", clsobj.PAYMENTS_REMARKS)
                cmd.Parameters.AddWithValue("@v_SALE_TYPE", clsobj.SALE_TYPE)
                cmd.Parameters.AddWithValue("@v_GROSS_AMOUNT", clsobj.GROSS_AMOUNT)
                cmd.Parameters.AddWithValue("@v_VAT_AMOUNT", clsobj.VAT_AMOUNT)
                cmd.Parameters.AddWithValue("@v_NET_AMOUNT", clsobj.NET_AMOUNT)
                cmd.Parameters.AddWithValue("@V_IS_SAMPLE", clsobj.IS_SAMPLE)
                cmd.Parameters.AddWithValue("@V_DELIVERY_NOTE_NO", clsobj.DELIVERY_NOTE_NO)
                cmd.Parameters.AddWithValue("@V_VAT_CST_PER", clsobj.VAT_CST_PER)
                cmd.Parameters.AddWithValue("@V_SAMPLE_ADDRESS", clsobj.SAMPLE_ADDRESS)
                cmd.Parameters.AddWithValue("@v_CREATED_BY", clsobj.CREATED_BY)
                cmd.Parameters.AddWithValue("@v_CREATION_DATE", clsobj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@v_MODIFIED_BY", clsobj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@v_MODIFIED_DATE", clsobj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@v_DIVISION_ID", clsobj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_VEHICLE_NO", clsobj.VEHICLE_NO)
                cmd.Parameters.AddWithValue("@V_TRANSPORT", clsobj.TRANSPORT)
                cmd.Parameters.AddWithValue("@v_SHIPP_ADD_ID", clsobj.SHIPP_ADD_ID)
                cmd.Parameters.AddWithValue("@v_INV_TYPE", clsobj.INV_TYPE)
                cmd.Parameters.AddWithValue("@v_LR_NO", clsobj.LR_NO)
                cmd.Parameters.AddWithValue("@V_MODE", 1)
                cmd.Parameters.AddWithValue("@V_Flag", clsobj.Flag)
                cmd.ExecuteNonQuery()
                cmd.Dispose()










                ''   2) insert in stock transfer detail table of local database

                Try




                    'For j As Integer = 0 To dt.Rows.Count
again:
                    For i As Integer = 0 To clsobj.dtable_Item_List.Rows.Count - 1
                        If IsNumeric(clsobj.dtable_Item_List.Rows(i)("transfer_qty")) Then


                            If Convert.ToDouble(clsobj.dtable_Item_List.Rows(i)("transfer_qty")) <= 0 Then
                                clsobj.dtable_Item_List.Rows.RemoveAt(i)

                                GoTo again
                            End If
                        End If
                    Next
                    ' Next



                    Dim Dtitemsnew As DataTable = clsobj.dtable_Item_List.Copy
                    Dtitemsnew.Rows.Clear()




                    For Each items_DataRow As DataRow In clsobj.dtable_Item_List.Rows



                        If (Dtitemsnew.Select("Item_Id=" & items_DataRow("Item_Id")).Length > 0) Then

                            Dim items_row() As DataRow = Dtitemsnew.Select("item_id=" & items_DataRow("item_id"))

                            items_row(0)("TRANSFER_QTY") = (items_row(0)("TRANSFER_QTY") + items_DataRow("TRANSFER_QTY"))
                            items_row(0)("GST_Amount") = (items_row(0)("GST_Amount") + items_DataRow("GST_Amount"))

                            If items_DataRow("DType").ToString() = "A" Then
                                items_row(0)("DISC") = (items_row(0)("DISC") + items_DataRow("DISC"))
                            End If

                        Else
                            Dim OrderDataRow As DataRow = Dtitemsnew.NewRow()
                            OrderDataRow("item_id") = items_DataRow("item_id")
                            OrderDataRow("TRANSFER_QTY") = items_DataRow("TRANSFER_QTY")
                            OrderDataRow("ITEM_RATE") = items_DataRow("ITEM_RATE")
                            OrderDataRow("GST") = items_DataRow("GST")
                            OrderDataRow("GST_Amount") = items_DataRow("GST_Amount")
                            OrderDataRow("HsnCodeId") = items_DataRow("HsnCodeId")
                            OrderDataRow("DType") = items_DataRow("DType")
                            OrderDataRow("DISC") = items_DataRow("DISC")
                            OrderDataRow("Amount") = items_DataRow("Amount")
                            If (clsobj.Flag = 1) Then
                                OrderDataRow("GPAID") = items_DataRow("GPAID")
                            End If

                            Dtitemsnew.Rows.Add(OrderDataRow)
                        End If

                        Dtitemsnew.AcceptChanges()
                    Next








                    cmd = New SqlCommand

                    For i As Integer = 0 To Dtitemsnew.Rows.Count - 1
                        cmd.Parameters.Clear()
                        cmd.Connection = con
                        cmd.Transaction = tran
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_OUTSIDE_SALE_DETAIL_NEW"
                        cmd.Parameters.AddWithValue("@v_SI_ID", clsobj.SI_ID)
                        cmd.Parameters.AddWithValue("@v_ITEM_ID", Dtitemsnew.Rows(i)("item_id"))
                        cmd.Parameters.AddWithValue("@v_ITEM_QTY", Dtitemsnew.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@v_PKT", 0)
                        cmd.Parameters.AddWithValue("@v_ITEM_RATE", Dtitemsnew.Rows(i)("ITEM_RATE"))
                        cmd.Parameters.AddWithValue("@v_ITEM_AMOUNT", Dtitemsnew.Rows(i)("Amount"))
                        cmd.Parameters.AddWithValue("@v_VAT_PER", Dtitemsnew.Rows(i)("GST"))
                        cmd.Parameters.AddWithValue("@v_VAT_AMOUNT", Dtitemsnew.Rows(i)("GST_Amount"))
                        cmd.Parameters.AddWithValue("@v_CREATED_BY", clsobj.CREATED_BY)
                        cmd.Parameters.AddWithValue("@v_CREATION_DATE", clsobj.CREATION_DATE)
                        cmd.Parameters.AddWithValue("@v_MODIFIED_BY", clsobj.MODIFIED_BY)
                        cmd.Parameters.AddWithValue("@v_MODIFIED_DATE", clsobj.MODIFIED_DATE)
                        cmd.Parameters.AddWithValue("@v_DIVISION_ID", clsobj.DIVISION_ID)
                        cmd.Parameters.AddWithValue("@v_TARRIF_ID", Dtitemsnew.Rows(i)("HsnCodeId"))
                        cmd.Parameters.AddWithValue("@v_DISCOUNT_TYPE", Dtitemsnew.Rows(i)("DType"))
                        cmd.Parameters.AddWithValue("@v_DISCOUNT_VALUE", Dtitemsnew.Rows(i)("DISC"))
                        cmd.Parameters.AddWithValue("@V_MODE", 1)
                        If (clsobj.Flag = 1) Then
                            cmd.Parameters.AddWithValue("@v_GSTPAID", Dtitemsnew.Rows(i)("GPAID"))
                        End If

                        cmd.ExecuteNonQuery()
                    Next


                    For i As Integer = 0 To clsobj.dtable_Item_List.Rows.Count - 1

                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_STOCK_DETAIL_ISSUE"
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.dtable_Item_List.Rows(i)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", clsobj.dtable_Item_List.Rows(i)("TRANSFER_QTY"))
                        cmd.ExecuteNonQuery()


                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_SALE_INVOICE_STOCK_DETAIL"
                        cmd.Parameters.AddWithValue("@SI_ID", clsobj.SI_ID)
                        cmd.Parameters.AddWithValue("@ITEM_ID", clsobj.dtable_Item_List.Rows(i)("Item_id"))
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.dtable_Item_List.Rows(i)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ITEM_QTY", clsobj.dtable_Item_List.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@MODE", 1)
                        cmd.ExecuteNonQuery()


                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsobj.SI_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", clsobj.dtable_Item_List.Rows(i)("Item_id"))
                        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.Sale_Invoice)
                        cmd.Parameters.AddWithValue("@Quantity", clsobj.dtable_Item_List.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@Transaction_Date", Now)
                        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.dtable_Item_List.Rows(i)("Stock_Detail_Id"))
                        cmd.ExecuteNonQuery()
                    Next


                    cmd = New SqlCommand
                    cmd.Parameters.Clear()
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandText = "update Invoice_series set current_used=current_used + 1 where div_id=" & clsobj.DIVISION_ID
                    cmd.ExecuteNonQuery()

                    cmd.Dispose()

                    tran.Commit()

                    'trans_global.Commit()


                Catch ex As Exception
                    tran.Rollback()
                    ' trans_global.Rollback()
                    MsgBox(ex.Message)
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

        Public Sub Cancel_SALE_INVOICE_MASTER(SI_ID As Integer, Status As Integer, username As String)
            Try

                If con.State = ConnectionState.Closed Then con.Open()

                cmd = New SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_Cancel_Sale_Invoice"
                cmd.Parameters.AddWithValue("@SI_ID", SI_ID)
                cmd.Parameters.AddWithValue("@status", Status)
                cmd.Parameters.AddWithValue("@userName", username)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Catch ex As Exception


                MsgBox(ex.Message)
            End Try

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

        Public Sub Insert_New_Customer_Remote(CUS_ID As Integer, Customer_Name As String, Address As String, Phone As String, GSTNO As String, City_ID As Integer)


            Dim con_global As New SqlConnection(gblDNS_Online)
            Dim trans_global As SqlTransaction


            If con_global.State <> ConnectionState.Open Then con_global.Open()
            trans_global = con_global.BeginTransaction
            Try
                cmd = New SqlCommand
                cmd.Connection = con_global
                cmd.Transaction = trans_global
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "proc_AccountMasterInsert"
                cmd.Parameters.AddWithValue("@ACC_ID", CUS_ID)
                cmd.Parameters.AddWithValue("@ACC_NAME", Customer_Name)
                cmd.Parameters.AddWithValue("@AG_ID", 1)
                cmd.Parameters.AddWithValue("@ADDRESS_PRIM", Address)
                cmd.Parameters.AddWithValue("@PHONE_PRIM", Phone)
                cmd.Parameters.AddWithValue("@CITY_ID", City_ID)
                cmd.Parameters.AddWithValue("@VAT_NO", GSTNO)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans_global.Commit()
                trans_global.Dispose()

            Catch ex As Exception
                trans_global.Rollback()
                con_global.Close()
                MsgBox(ex.Message)
            End Try
        End Sub

        Public Sub Insert_New_Customer(CUS_ID As Integer, Customer_Name As String, Address As String, Phone As String, GSTNO As String, City_ID As Integer)

            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then con.Open()
            trans = con.BeginTransaction

            Try
                cmd = New SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trans
                cmd.CommandText = "proc_AccountMasterInsert"
                cmd.Parameters.AddWithValue("@ACC_ID", CUS_ID)
                cmd.Parameters.AddWithValue("@ACC_NAME", Customer_Name)
                cmd.Parameters.AddWithValue("@AG_ID", 1)
                cmd.Parameters.AddWithValue("@ADDRESS_PRIM", Address)
                cmd.Parameters.AddWithValue("@PHONE_PRIM", Phone)
                cmd.Parameters.AddWithValue("@CITY_ID", City_ID)
                cmd.Parameters.AddWithValue("@VAT_NO", GSTNO)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                trans.Dispose()

            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message)
            End Try
        End Sub

        Public Sub Update_SALE_INVOICE_MASTER(ByVal clsobj As cls_Sale_Invoice_prop)
            Try


                Dim tran As SqlTransaction
                If con.State = ConnectionState.Closed Then con.Open()
                tran = con.BeginTransaction()

                cmd = New SqlCommand
                cmd.Parameters.Clear()
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ProcReverseInvoiceEntry"
                cmd.Parameters.AddWithValue("@v_SI_ID", clsobj.SI_ID)
                cmd.Parameters.AddWithValue("@V_CUST_ID", clsobj.CUST_ID)
                cmd.ExecuteNonQuery()
                cmd.Dispose()


                cmd = New SqlCommand
                cmd.Connection = con
                cmd.Transaction = tran





                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_OUTSIDE_SALE_MASTER_SALE_NEW"
                cmd.Parameters.AddWithValue("@v_SI_ID", clsobj.SI_ID)
                cmd.Parameters.AddWithValue("@v_SI_CODE", clsobj.SI_CODE)
                cmd.Parameters.AddWithValue("@v_SI_NO", clsobj.SI_NO)
                cmd.Parameters.AddWithValue("@v_DC_NO", clsobj.DC_GST_NO)
                cmd.Parameters.AddWithValue("@v_SI_DATE", clsobj.SI_DATE)
                cmd.Parameters.AddWithValue("@v_CUST_ID", clsobj.CUST_ID)
                cmd.Parameters.AddWithValue("@V_INVOICE_STATUS", clsobj.INVOICE_STATUS)
                cmd.Parameters.AddWithValue("@v_REMARKS", clsobj.REMARKS)
                cmd.Parameters.AddWithValue("@v_PAYMENTS_REMARKS", clsobj.PAYMENTS_REMARKS)
                cmd.Parameters.AddWithValue("@v_SALE_TYPE", clsobj.SALE_TYPE)
                cmd.Parameters.AddWithValue("@v_GROSS_AMOUNT", clsobj.GROSS_AMOUNT)
                cmd.Parameters.AddWithValue("@v_VAT_AMOUNT", clsobj.VAT_AMOUNT)
                cmd.Parameters.AddWithValue("@v_NET_AMOUNT", clsobj.NET_AMOUNT)
                cmd.Parameters.AddWithValue("@V_IS_SAMPLE", clsobj.IS_SAMPLE)
                cmd.Parameters.AddWithValue("@V_DELIVERY_NOTE_NO", clsobj.DELIVERY_NOTE_NO)
                cmd.Parameters.AddWithValue("@V_VAT_CST_PER", clsobj.VAT_CST_PER)
                cmd.Parameters.AddWithValue("@V_SAMPLE_ADDRESS", clsobj.SAMPLE_ADDRESS)
                cmd.Parameters.AddWithValue("@v_CREATED_BY", clsobj.CREATED_BY)
                cmd.Parameters.AddWithValue("@v_CREATION_DATE", clsobj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@v_MODIFIED_BY", clsobj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@v_MODIFIED_DATE", clsobj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@v_DIVISION_ID", clsobj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_VEHICLE_NO", clsobj.VEHICLE_NO)
                cmd.Parameters.AddWithValue("@V_TRANSPORT", clsobj.TRANSPORT)
                cmd.Parameters.AddWithValue("@v_SHIPP_ADD_ID", clsobj.SHIPP_ADD_ID)
                cmd.Parameters.AddWithValue("@v_INV_TYPE", clsobj.INV_TYPE)
                cmd.Parameters.AddWithValue("@v_LR_NO", clsobj.LR_NO)
                cmd.Parameters.AddWithValue("@V_MODE", 2)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                Try






again:
                    For i As Integer = 0 To clsobj.dtable_Item_List.Rows.Count - 1
                        If IsNumeric(clsobj.dtable_Item_List.Rows(i)("transfer_qty")) Then


                            If Convert.ToDouble(clsobj.dtable_Item_List.Rows(i)("transfer_qty")) <= 0 Then
                                clsobj.dtable_Item_List.Rows.RemoveAt(i)

                                GoTo again
                            End If
                        End If
                    Next


                    Dim Dtitemsnew As DataTable = clsobj.dtable_Item_List.Copy
                    Dtitemsnew.Rows.Clear()




                    For Each items_DataRow As DataRow In clsobj.dtable_Item_List.Rows



                        If (Dtitemsnew.Select("Item_Id=" & items_DataRow("Item_Id")).Length > 0) Then

                            Dim items_row() As DataRow = Dtitemsnew.Select("item_id=" & items_DataRow("item_id"))

                            items_row(0)("TRANSFER_QTY") = (items_row(0)("TRANSFER_QTY") + items_DataRow("TRANSFER_QTY"))
                            items_row(0)("GST_Amount") = (items_row(0)("GST_Amount") + items_DataRow("GST_Amount"))

                            If items_DataRow("DType").ToString() = "A" Then
                                items_row(0)("DISC") = (items_row(0)("DISC") + items_DataRow("DISC"))
                            End If

                        Else
                            Dim OrderDataRow As DataRow = Dtitemsnew.NewRow()
                            OrderDataRow("item_id") = items_DataRow("item_id")
                            OrderDataRow("TRANSFER_QTY") = items_DataRow("TRANSFER_QTY")
                            OrderDataRow("ITEM_RATE") = items_DataRow("ITEM_RATE")
                            OrderDataRow("GST") = items_DataRow("GST")
                            OrderDataRow("GST_Amount") = items_DataRow("GST_Amount")
                            OrderDataRow("HsnCodeId") = items_DataRow("HsnCodeId")
                            OrderDataRow("DType") = items_DataRow("DType")
                            OrderDataRow("DISC") = items_DataRow("DISC")
                            If (clsobj.Flag = 1) Then
                                OrderDataRow("GPAID") = items_DataRow("GPAID")
                            End If
                            OrderDataRow("Amount") = items_DataRow("Amount")
                            Dtitemsnew.Rows.Add(OrderDataRow)
                        End If

                        Dtitemsnew.AcceptChanges()
                    Next


                    cmd = New SqlCommand

                    For i As Integer = 0 To Dtitemsnew.Rows.Count - 1
                        cmd.Parameters.Clear()
                        cmd.Connection = con
                        cmd.Transaction = tran
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_OUTSIDE_SALE_DETAIL_NEW"
                        cmd.Parameters.AddWithValue("@v_SI_ID", clsobj.SI_ID)
                        cmd.Parameters.AddWithValue("@v_ITEM_ID", Dtitemsnew.Rows(i)("item_id"))
                        cmd.Parameters.AddWithValue("@v_ITEM_QTY", Dtitemsnew.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@v_PKT", 0)
                        cmd.Parameters.AddWithValue("@v_ITEM_RATE", Dtitemsnew.Rows(i)("ITEM_RATE"))
                        cmd.Parameters.AddWithValue("@v_ITEM_AMOUNT", Dtitemsnew.Rows(i)("Amount"))
                        cmd.Parameters.AddWithValue("@v_VAT_PER", Dtitemsnew.Rows(i)("GST"))
                        cmd.Parameters.AddWithValue("@v_VAT_AMOUNT", Dtitemsnew.Rows(i)("GST_Amount"))
                        cmd.Parameters.AddWithValue("@v_CREATED_BY", clsobj.CREATED_BY)
                        cmd.Parameters.AddWithValue("@v_CREATION_DATE", clsobj.CREATION_DATE)
                        cmd.Parameters.AddWithValue("@v_MODIFIED_BY", clsobj.MODIFIED_BY)
                        cmd.Parameters.AddWithValue("@v_MODIFIED_DATE", clsobj.MODIFIED_DATE)
                        cmd.Parameters.AddWithValue("@v_DIVISION_ID", clsobj.DIVISION_ID)
                        cmd.Parameters.AddWithValue("@v_TARRIF_ID", Dtitemsnew.Rows(i)("HsnCodeId"))
                        cmd.Parameters.AddWithValue("@v_DISCOUNT_TYPE", Dtitemsnew.Rows(i)("DType"))
                        cmd.Parameters.AddWithValue("@v_DISCOUNT_VALUE", Dtitemsnew.Rows(i)("DISC"))
                        cmd.Parameters.AddWithValue("@V_MODE", 1)
                        If (clsobj.Flag = 1) Then
                            cmd.Parameters.AddWithValue("@v_GSTPAID", Dtitemsnew.Rows(i)("GPAID"))
                        End If
                        cmd.ExecuteNonQuery()
                    Next


                    For i As Integer = 0 To clsobj.dtable_Item_List.Rows.Count - 1

                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_STOCK_DETAIL_ISSUE"
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.dtable_Item_List.Rows(i)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", clsobj.dtable_Item_List.Rows(i)("TRANSFER_QTY"))
                        cmd.ExecuteNonQuery()


                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_SALE_INVOICE_STOCK_DETAIL"
                        cmd.Parameters.AddWithValue("@SI_ID", clsobj.SI_ID)
                        cmd.Parameters.AddWithValue("@ITEM_ID", clsobj.dtable_Item_List.Rows(i)("Item_id"))
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", clsobj.dtable_Item_List.Rows(i)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ITEM_QTY", clsobj.dtable_Item_List.Rows(i)("TRANSFER_QTY"))
                        cmd.Parameters.AddWithValue("@MODE", 1)
                        cmd.ExecuteNonQuery()


                        'cmd.Parameters.Clear()
                        'cmd.CommandType = CommandType.StoredProcedure
                        'cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        'cmd.Parameters.AddWithValue("@Transaction_ID", clsobj.SI_ID)
                        'cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(i)("Item_id"))
                        'cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.Sale_Invoice)
                        'cmd.Parameters.AddWithValue("@Quantity", dt.Rows(i)("TRANSFER_QTY"))
                        'cmd.Parameters.AddWithValue("@Transaction_Date", Now)
                        'cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        'cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(i)("Stock_Detail_Id"))
                        'cmd.ExecuteNonQuery()
                    Next




                    cmd.Dispose()

                    tran.Commit()

                    'trans_global.Commit()


                Catch ex As Exception
                    tran.Rollback()
                    ' trans_global.Rollback()
                    MsgBox(ex.Message)
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub


    End Class

End Namespace