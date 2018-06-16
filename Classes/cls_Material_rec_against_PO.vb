Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace material_rec_against_PO

    Public Class cls_Material_rec_Against_PO_Prop

        Dim _Receipt_ID As Double
        Dim _Receipt_Code As String
        Dim _Receipt_No As Double
        Dim _Receipt_Date As DateTime
        Dim _Purchase_Type As Int32
        Dim _Vendor_ID As Int32
        Dim _Indent_ID As Int32
        Dim _Remarks As String
        Dim _Po_ID As Int32
        Dim _MRN_PREFIX As String
        Dim _MRN_NO As Double
        Dim _Created_By As String
        Dim _Creation_Date As DateTime
        Dim _Modified_By As String
        Dim _Modification_Date As DateTime
        Dim _Division_ID As Int32
        Dim _mrn_status As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Item_Rate As Double
        Dim _Item_Vat As Double
        Dim _Item_Exice As Double
        Dim _MRNCompanies_ID As Int16
        Dim _Invoice_No As String
        Dim _Invoice_Date As DateTime
        Dim _freight As Double
        Dim _Other_charges As Double
        Dim _Discount_amt As Double
        Dim _GROSS_AMOUNT As Double
        Dim _GST_AMOUNT As Double
        Dim _CESS_AMOUNT As Double
        Dim _NET_AMOUNT As Double
        Dim _MRN_TYPE As Int32
        Dim _VAT_ON_EXICE As Int32
        Dim _IsPrinted As Int32
        Dim _CUST_ID As Int32


        Public Property Receipt_ID() As Int32
            Get
                Receipt_ID = _Receipt_ID
            End Get
            Set(ByVal value As Int32)
                _Receipt_ID = value
            End Set
        End Property


        Public Property CUST_ID() As Integer
            Get
                CUST_ID = _CUST_ID
            End Get
            Set(ByVal value As Integer)
                _CUST_ID = value
            End Set
        End Property

        Public Property Invoice_No() As String
            Get
                Invoice_No = _Invoice_No
            End Get
            Set(ByVal value As String)
                _Invoice_No = value
            End Set
        End Property
        Public Property Invoice_Date() As DateTime
            Get
                Invoice_Date = _Invoice_Date
            End Get
            Set(ByVal value As DateTime)
                _Invoice_Date = value
            End Set
        End Property
        Public Property Receipt_Code() As String
            Get
                Receipt_Code = _Receipt_Code
            End Get
            Set(ByVal value As String)
                _Receipt_Code = value
            End Set
        End Property
        Public Property Receipt_No() As Double
            Get
                Receipt_No = _Receipt_No
            End Get
            Set(ByVal value As Double)
                _Receipt_No = value
            End Set
        End Property
        Public Property Receipt_Date() As DateTime
            Get
                Receipt_Date = _Receipt_Date
            End Get
            Set(ByVal value As DateTime)
                _Receipt_Date = value
            End Set
        End Property
        Public Property Purchase_Type() As Int32
            Get
                Purchase_Type = _Purchase_Type
            End Get
            Set(ByVal value As Int32)
                _Purchase_Type = value
            End Set
        End Property
        Public Property Indent_ID() As Int32
            Get
                Indent_ID = _Indent_ID
            End Get
            Set(ByVal value As Int32)
                _Indent_ID = value
            End Set
        End Property
        Public Property Vendor_ID() As Int32
            Get
                Vendor_ID = _Vendor_ID
            End Get
            Set(ByVal value As Int32)
                _Vendor_ID = value
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
        Public Property Po_ID() As Int32
            Get
                Po_ID = _Po_ID
            End Get
            Set(ByVal value As Int32)
                _Po_ID = value
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
        Public Property MRN_NO() As Double
            Get
                MRN_NO = _MRN_NO
            End Get
            Set(ByVal value As Double)
                _MRN_NO = value
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
        Public Property mrn_status() As Int32
            Get
                mrn_status = _mrn_status
            End Get
            Set(ByVal value As Int32)
                _mrn_status = value
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
        Public Property Item_Rate() As Double
            Get
                Item_Rate = _Item_Rate
            End Get
            Set(ByVal value As Double)
                _Item_Rate = value
            End Set
        End Property
        Public Property Item_vat() As Double
            Get
                Item_vat = _Item_Vat
            End Get
            Set(ByVal value As Double)
                _Item_Vat = value
            End Set
        End Property
        Public Property Item_exice() As Double
            Get
                Item_exice = _Item_Exice
            End Get
            Set(ByVal value As Double)
                _Item_Exice = value
            End Set
        End Property
        Public Property MRNCompanies_ID() As Int32
            Get
                MRNCompanies_ID = _MRNCompanies_ID
            End Get
            Set(ByVal value As Int32)
                _MRNCompanies_ID = value
            End Set
        End Property
        Public Property freight() As Double
            Get
                freight = _freight
            End Get
            Set(ByVal value As Double)
                _freight = value
            End Set
        End Property
        Public Property Other_Charges() As Double
            Get
                Other_Charges = _Other_charges
            End Get
            Set(ByVal value As Double)
                _Other_charges = value
            End Set
        End Property
        Public Property Discount_amt() As Double
            Get
                Discount_amt = _Discount_amt
            End Get
            Set(ByVal value As Double)
                _Discount_amt = value
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
        Public Property GST_AMOUNT() As Double
            Get
                GST_AMOUNT = _GST_AMOUNT
            End Get
            Set(ByVal value As Double)
                _GST_AMOUNT = value
            End Set
        End Property

        Public Property CESS_AMOUNT() As Double
            Get
                CESS_AMOUNT = _CESS_AMOUNT
            End Get
            Set(ByVal value As Double)
                _CESS_AMOUNT = value
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
        Public Property MRN_TYPE() As Int32
            Get
                MRN_TYPE = _MRN_TYPE
            End Get
            Set(ByVal value As Int32)
                _MRN_TYPE = value
            End Set
        End Property
        Public Property VAT_ON_EXICE() As Int32
            Get
                VAT_ON_EXICE = _VAT_ON_EXICE
            End Get
            Set(ByVal value As Int32)
                _VAT_ON_EXICE = value
            End Set
        End Property
        Public Property IsPrinted() As Int32
            Get
                IsPrinted = _IsPrinted
            End Get
            Set(ByVal value As Int32)
                _IsPrinted = value
            End Set
        End Property

    End Class

    Public Class cls_material_recieved_against_po_master
        Inherits CommonClass

        ''' <summary>
        ''' 1. Insert into Material_received_Against_PO_Master
        ''' 2. Insert into Material_received_Against_PO_Detail
        ''' 3. PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL updates the PO_STATUS table also.
        ''' 4. Insert into STOCK_DETAIL table
        ''' 5. Insert into Transaction_Log table
        ''' </summary>
        ''' <param name="clsObj"></param>
        ''' <param name="cmd"></param>
        ''' <param name="FlexGrid"></param>
        ''' <remarks></remarks>
        ''' 

        Public Sub insert_MATERIAL_RECIEVED_AGAINST_PO_MASTER(ByVal clsObj As cls_Material_rec_Against_PO_Prop, ByVal cmd As SqlCommand, ByVal FlexGrid As DataTable, ByVal FlexGrid_NonStockableItems As DataTable)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con


            cmd.CommandText = "PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER"

            cmd.Parameters.AddWithValue("@v_Receipt_ID", clsObj.Receipt_ID)
            cmd.Parameters.AddWithValue("@v_Receipt_No", clsObj.Receipt_No)
            cmd.Parameters.AddWithValue("@v_Receipt_Code", clsObj.Receipt_Code)
            cmd.Parameters.AddWithValue("@v_PO_ID", clsObj.Po_ID)
            cmd.Parameters.AddWithValue("@v_Receipt_Date", clsObj.Receipt_Date)
            cmd.Parameters.AddWithValue("@v_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@v_MRN_NO", clsObj.MRN_NO)
            cmd.Parameters.AddWithValue("@v_MRN_PREFIX", clsObj.MRN_PREFIX)
            cmd.Parameters.AddWithValue("@v_Created_BY", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_mrn_status", clsObj.mrn_status)
            cmd.Parameters.AddWithValue("@v_MRNCompanies_ID", clsObj.MRNCompanies_ID)
            cmd.Parameters.AddWithValue("@v_Invoice_No", clsObj.Invoice_No)
            cmd.Parameters.AddWithValue("@v_Invoice_Date", clsObj.Invoice_Date)
            cmd.Parameters.AddWithValue("@v_freight", clsObj.freight)
            cmd.Parameters.AddWithValue("@v_Other_charges", clsObj.Other_Charges)
            cmd.Parameters.AddWithValue("@v_Discount_amt", clsObj.Discount_amt)
            cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsObj.VAT_ON_EXICE)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.Parameters.AddWithValue("@v_GROSS_AMOUNT", clsObj.GROSS_AMOUNT)
            cmd.Parameters.AddWithValue("@v_GST_AMOUNT", clsObj.GST_AMOUNT)
            cmd.Parameters.AddWithValue("@v_CESS_AMOUNT", clsObj.CESS_AMOUNT)
            cmd.Parameters.AddWithValue("@v_NET_AMOUNT", clsObj.NET_AMOUNT)
            cmd.Parameters.AddWithValue("@V_MRN_TYPE", clsObj.MRN_TYPE)
            cmd.Parameters.AddWithValue("@V_CUST_ID", clsObj.CUST_ID)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()




            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = FlexGrid.Rows.Count - 1
            For iRow = 0 To iRowCount
                If FlexGrid.Rows(iRow)("BATCH_QTY") > 0 Then
                    clsObj.Item_ID = FlexGrid.Rows(iRow)("Item_ID")
                    'clsObj.Po_ID = FlexGrid.Item(iRow, 2)
                    clsObj.Created_By = v_the_current_logged_in_user_name
                    clsObj.Creation_Date = now
                    clsObj.Modified_By = v_the_current_logged_in_user_name
                    clsObj.Modification_Date = now
                    clsObj.Division_ID = v_the_current_division_id
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL"
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@v_PO_ID", clsObj.Po_ID)
                    cmd.Parameters.AddWithValue("@v_DIV_ID", clsObj.Division_ID)
                    cmd.Parameters.AddWithValue("@v_Receipt_ID", clsObj.Receipt_ID)
                    cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
                    cmd.Parameters.AddWithValue("@v_Item_Rate", FlexGrid.Rows(iRow)("Item_Rate"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Batch_no", FlexGrid.Rows(iRow)("BATCH_NO"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Expiry_Date", FlexGrid.Rows(iRow)("EXPIRY_DATE"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Creation_Date", clsObj.Creation_Date)
                    cmd.Parameters.AddWithValue("@v_Item_Qty", FlexGrid.Rows(iRow)("BATCH_QTY"))
                    cmd.Parameters.AddWithValue("@v_Item_vat_per", FlexGrid.Rows(iRow)("Vat_Per"))
                    cmd.Parameters.AddWithValue("@v_Item_cess_per", FlexGrid.Rows(iRow)("Cess_Per"))
                    cmd.Parameters.AddWithValue("@v_Item_Exice_per", FlexGrid.Rows(iRow)("EXICE_Per"))
                    cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.MaterialRecievedAgainstPO)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.Parameters.AddWithValue("@v_DType", FlexGrid.Rows(iRow)("DType"))
                    cmd.Parameters.AddWithValue("@v_DiscountValue", FlexGrid.Rows(iRow)("DISC"))

                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End If
            Next iRow
            '***************************************************************************************
            ' Insert items into NOn Stockable Items table Against PO 
            '***************************************************************************************
            Dim iRowCount_NonSTK As Int32
            Dim iRow_NonSTK As Int32
            If FlexGrid_NonStockableItems Is Nothing Then
                iRowCount_NonSTK = -1
            Else
                iRowCount_NonSTK = FlexGrid_NonStockableItems.Rows.Count - 1
            End If

            For iRow_NonSTK = 0 To iRowCount_NonSTK
                If FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("BATCH_QTY") > 0 Then
                    clsObj.Item_ID = FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("Item_ID")
                    'clsObj.Po_ID = FlexGrid_NonStockableItems.Item(iRow_NonSTK, 2)
                    clsObj.Division_ID = v_the_current_division_id

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL_NON_STOCKABLE"
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@v_Receipt_ID", clsObj.Receipt_ID)
                    cmd.Parameters.AddWithValue("@v_PO_ID", clsObj.Po_ID)
                    cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
                    cmd.Parameters.AddWithValue("@v_CostCenter_ID", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("CostCenter_ID"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Item_Qty", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("BATCH_QTY"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Item_Vat", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("Vat_Per"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Item_Exice", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("EXICE_Per"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Batch_no", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("BATCH_NO"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Expiry_Date", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("EXPIRY_DATE"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Item_Rate", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("Item_Rate"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Bal_Item_Qty", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("BATCH_QTY"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Bal_Item_Rate", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("Item_Rate"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Bal_Item_Vat", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("Vat_Per"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_Bal_Item_Exice", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("EXICE_Per"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_DIV_ID", clsObj.Division_ID)   ''''  new added
                    cmd.Parameters.AddWithValue("@v_DType", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("DType"))   ''''  new added
                    cmd.Parameters.AddWithValue("@v_DiscountValue", FlexGrid_NonStockableItems.Rows(iRow_NonSTK)("DISC"))   ''''  new added
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End If

            Next iRow_NonSTK


            '***************************************************************************************
            'Inderjeet
            'Entries in these table should be done through PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL
            'take a referance from recieved without po.
            '***************************************************************************************

            'cmd.Parameters.Clear()
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "INSERT_STOCK_DETAIL"

            'For iRow = 1 To iRowCount

            '    If FlexGrid.Item(iRow, 13) > 0 Then
            '        cmd.Parameters.AddWithValue("@ITEM_ID", FlexGrid.Item(iRow, 1))
            '        cmd.Parameters.AddWithValue("@BATCH_NO", FlexGrid.Item(iRow, "BATCH_NO"))
            '        cmd.Parameters.AddWithValue("@EXPIRY_DATE", FlexGrid.Item(iRow, "expiry_date"))
            '        cmd.Parameters.AddWithValue("@ITEM_QTY", FlexGrid.Item(iRow, 13))
            '        cmd.Parameters.AddWithValue("@DOC_ID", clsObj.Receipt_ID)
            '        cmd.Parameters.AddWithValue("@TRANSACTION_ID", Transaction_Type.MaterialRecievedAgainstPO)
            '        cmd.ExecuteNonQuery()
            '        cmd.Parameters.Clear()
            '    End If
            'Next iRow

            'cmd.Parameters.Clear()
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "INSERT_TRANSACTION_LOG"

            'For iRow = 1 To iRowCount

            '    If FlexGrid.Item(iRow, 13) > 0 Then
            '        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.Receipt_ID)
            '        cmd.Parameters.AddWithValue("@Item_ID", FlexGrid.Item(iRow, 1))
            '        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.MaterialRecievedAgainstPO)
            '        cmd.Parameters.AddWithValue("@Quantity", FlexGrid.Item(iRow, 13))
            '        cmd.Parameters.AddWithValue("@Transaction_Date", now)
            '        cmd.Parameters.AddWithValue("@Current_Stock", 0)
            '        cmd.ExecuteNonQuery()
            '        cmd.Parameters.Clear()
            '    End If
            'Next iRow


        End Sub

        Public Sub insert_MATERIAL_RECEIVED_AGAINST_PO_DETAIL(ByVal clsObj As cls_Material_rec_Against_PO_Prop)



        End Sub

        Public Sub Update_PrintStatus(ByVal clsObj As cls_Material_rec_Against_PO_Prop)
            Dim cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Proc_UpdatePrintStatus_AgainstPO"

            cmd.Parameters.AddWithValue("@v_Receipt_ID", clsObj.Receipt_ID)
            cmd.Parameters.AddWithValue("@IsPrinted", clsObj.IsPrinted)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
        End Sub

        Public Sub update_MATERIAL_RECIEVED_AGAINST_PO_MASTER(ByVal clsObj As cls_Material_rec_Against_PO_Prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Receipt_ID", clsObj.Receipt_ID)
            cmd.Parameters.AddWithValue("@V_Receipt_Code", clsObj.Receipt_Code)
            cmd.Parameters.AddWithValue("@V_Receipt_No", clsObj.Receipt_No)
            cmd.Parameters.AddWithValue("@V_Receipt_Date", clsObj.Receipt_Date)
            cmd.Parameters.AddWithValue("@V_Purchase_Type", clsObj.Purchase_Type)
            cmd.Parameters.AddWithValue("@V_Vendor_ID", clsObj.Vendor_ID)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_Po_ID", clsObj.Po_ID)
            cmd.Parameters.AddWithValue("@V_MRN_PREFIX", clsObj.MRN_PREFIX)
            cmd.Parameters.AddWithValue("@V_MRN_NO", clsObj.MRN_NO)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_mrn_status", clsObj.mrn_status)
            cmd.Parameters.AddWithValue("@v_freight", clsObj.freight)
            cmd.Parameters.AddWithValue("@v_Other_charges", clsObj.Other_Charges)
            cmd.Parameters.AddWithValue("@v_Discount_amt", clsObj.Discount_amt)
            cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsObj.VAT_ON_EXICE)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub delete_MATERIAL_RECIEVED_AGAINST_PO_MASTER(ByVal clsObj As cls_Material_rec_Against_PO_Prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Receipt_ID", clsObj.Receipt_ID)
            cmd.Parameters.AddWithValue("@V_Receipt_Code", clsObj.Receipt_Code)
            cmd.Parameters.AddWithValue("@V_Receipt_No", clsObj.Receipt_No)
            cmd.Parameters.AddWithValue("@V_Receipt_Date", clsObj.Receipt_Date)
            cmd.Parameters.AddWithValue("@V_Purchase_Type", clsObj.Purchase_Type)
            cmd.Parameters.AddWithValue("@V_Vendor_ID", clsObj.Vendor_ID)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_Po_ID", clsObj.Po_ID)
            cmd.Parameters.AddWithValue("@V_MRN_PREFIX", clsObj.MRN_PREFIX)
            cmd.Parameters.AddWithValue("@V_MRN_NO", clsObj.MRN_NO)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_mrn_status", clsObj.mrn_status)
            cmd.Parameters.AddWithValue("@v_freight", clsObj.freight)
            cmd.Parameters.AddWithValue("@v_Other_charges", clsObj.Other_Charges)
            cmd.Parameters.AddWithValue("@v_Discount_amt", clsObj.Discount_amt)
            cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsObj.VAT_ON_EXICE)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()

            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

    End Class
End Namespace
