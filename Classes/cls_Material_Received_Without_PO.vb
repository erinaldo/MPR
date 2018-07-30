Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace material_recieved_without_po_master

    Public Class cls_material_recieved_without_po_master_prop

        Dim _Received_ID As Double
        Dim _Received_Code As String
        Dim _Received_No As Double
        Dim _Received_Date As DateTime
        Dim _Purchase_Type As Int32
        Dim _Vendor_ID As Int32
        Dim _Remarks As String
        Dim _Po_ID As Double
        Dim _MRN_PREFIX As String
        Dim _MRN_NO As Double
        Dim _Created_By As String
        Dim _Creation_Date As DateTime
        Dim _Modified_By As String
        Dim _Modification_Date As DateTime
        Dim _Invoice_No As String
        Dim _Invoice_Date As DateTime
        Dim _Division_ID As Int32
        Dim _mrn_status As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Item_Rate As Double
        Dim _Item_Vat As Double
        Dim _DType As String
        Dim _DISC As Double
        Dim _DISC1 As Double
        Dim _GPaid As String
        Dim _Item_Exice As Double
        Dim _Batch_no As String
        Dim _Expiry_Date As Date
        Dim _TransactionType As Int32
        Dim _CostCenter_ID As Int16
        Dim _freight As Double
        Dim _freight_type As String
        Dim _MRNCompanies_ID As String
        Dim _other_charges As Double
        Dim _Discount_amt As Double
        Dim _CashDiscount_amt As Double
        Dim _VAT_ON_EXICE As Int32
        Dim _Isprinted As Int32
        Dim _MRNType As Int32
        Dim _GROSS_AMOUNT As Double
        Dim _GST_AMOUNT As Double
        Dim _CESS_AMOUNT As Double
        Dim _ACESS_AMOUNT As Double
        Dim _NET_AMOUNT As Double
        Dim _Item_Cess As Double
        Dim _A_Cess As Double

        Dim _Special_Scheme As String

        Public Property Special_Scheme() As String
            Get
                Special_Scheme = _Special_Scheme
            End Get
            Set(ByVal value As String)
                _Special_Scheme = value
            End Set
        End Property

        Public Property Received_ID() As Integer
            Get
                Received_ID = _Received_ID
            End Get
            Set(ByVal value As Integer)
                _Received_ID = value
            End Set
        End Property

        Public Property Received_Code() As String
            Get
                Received_Code = _Received_Code
            End Get
            Set(ByVal value As String)
                _Received_Code = value
            End Set
        End Property

        Public Property Received_No() As Double
            Get
                Received_No = _Received_No
            End Get
            Set(ByVal value As Double)
                _Received_No = value
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
        Public Property Invoice_Date() As DateTime
            Get
                Invoice_Date = _Invoice_Date
            End Get
            Set(ByVal value As DateTime)
                _Invoice_Date = value
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
        Public Property Invoice_No() As String
            Get
                Invoice_No = _Invoice_No
            End Get
            Set(ByVal value As String)
                _Invoice_No = value
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

        Public Property Po_ID() As Double
            Get
                Po_ID = _Po_ID
            End Get
            Set(ByVal value As Double)
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
                Other_Charges = _other_charges
            End Get
            Set(ByVal value As Double)
                _other_charges = value
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

        Public Property CashDiscount_amt() As Double
            Get
                CashDiscount_amt = _CashDiscount_amt
            End Get
            Set(ByVal value As Double)
                _CashDiscount_amt = value
            End Set
        End Property

        Public Property freight_type() As String
            Get
                freight_type = _freight_type
            End Get
            Set(ByVal value As String)
                _freight_type = value
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
        Public Property DType() As String
            Get
                DType = _DType
            End Get
            Set(ByVal value As String)
                _DType = value
            End Set
        End Property
        Public Property DISC() As Double
            Get
                DISC = _DISC
            End Get
            Set(ByVal value As Double)
                _DISC = value
            End Set
        End Property

        Public Property DISC1() As Double
            Get
                DISC1 = _DISC1
            End Get
            Set(ByVal value As Double)
                _DISC1 = value
            End Set
        End Property

        Public Property GPaid() As String
            Get
                GPaid = _GPaid
            End Get
            Set(ByVal value As String)
                _GPaid = value
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

        Public Property Item_Cess() As Double
            Get
                Item_Cess = _Item_Cess
            End Get
            Set(ByVal value As Double)
                _Item_Cess = value
            End Set
        End Property

        Public Property A_Cess() As Double
            Get
                A_Cess = _A_Cess
            End Get
            Set(ByVal value As Double)
                _A_Cess = value
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
        Public Property TransactionType() As Int32
            Get
                TransactionType = _TransactionType
            End Get
            Set(ByVal value As Int32)
                _TransactionType = value
            End Set
        End Property
        Public Property CostCenter_ID() As Int32
            Get
                CostCenter_ID = _CostCenter_ID
            End Get
            Set(ByVal value As Int32)
                _CostCenter_ID = value
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
                IsPrinted = _Isprinted
            End Get
            Set(ByVal value As Int32)
                _Isprinted = value
            End Set
        End Property

        Public Property MRNType() As Int32
            Get
                MRNType = _MRNType
            End Get
            Set(ByVal value As Int32)
                _MRNType = value
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

        Public Property ACESS_AMOUNT() As Double
            Get
                ACESS_AMOUNT = _ACESS_AMOUNT
            End Get
            Set(ByVal value As Double)
                _ACESS_AMOUNT = value
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


    End Class

    Public Class cls_material_recieved_without_po_master
        Inherits CommonClass

        Public Sub insert_MATERIAL_RECIEVED_WITHOUT_PO_MASTER(ByVal clsObj As cls_material_recieved_without_po_master_prop, ByVal cmd As SqlCommand)
            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
            cmd.Parameters.AddWithValue("@V_Received_Code", clsObj.Received_Code)
            cmd.Parameters.AddWithValue("@V_Received_No", clsObj.Received_No)
            cmd.Parameters.AddWithValue("@V_Received_Date", clsObj.Received_Date)
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
            cmd.Parameters.AddWithValue("@v_Invoice_No", clsObj.Invoice_No)
            cmd.Parameters.AddWithValue("@v_Invoice_Date", clsObj.Invoice_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_mrn_status", clsObj.mrn_status)
            cmd.Parameters.AddWithValue("@V_freight", clsObj.freight)
            cmd.Parameters.AddWithValue("@V_freight_type", clsObj.freight_type)
            cmd.Parameters.AddWithValue("@v_MRNCompanies_ID", clsObj.MRNCompanies_ID)
            cmd.Parameters.AddWithValue("@v_other_charges", clsObj.Other_Charges)
            cmd.Parameters.AddWithValue("@v_Discount_amt", clsObj.Discount_amt)
            cmd.Parameters.AddWithValue("@v_CashDiscount_amt", clsObj.CashDiscount_amt)
            cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsObj.VAT_ON_EXICE)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.Parameters.AddWithValue("@v_GROSS_AMOUNT", clsObj.GROSS_AMOUNT)
            cmd.Parameters.AddWithValue("@v_GST_AMOUNT", clsObj.GST_AMOUNT)
            cmd.Parameters.AddWithValue("@v_CESS_AMOUNT", clsObj.CESS_AMOUNT)
            cmd.Parameters.AddWithValue("@v_ACESS_AMOUNT", clsObj.ACESS_AMOUNT)
            cmd.Parameters.AddWithValue("@v_NET_AMOUNT", clsObj.NET_AMOUNT)
            cmd.Parameters.AddWithValue("@V_MRN_TYPE", clsObj.MRNType)
            cmd.Parameters.AddWithValue("@V_Special_Scheme", clsObj.Special_Scheme)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub insert_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL(ByVal clsObj As cls_material_recieved_without_po_master_prop, ByVal cmd As SqlCommand)
            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL"

            cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_Item_vat", clsObj.Item_vat)
            cmd.Parameters.AddWithValue("@V_Item_cess", clsObj.Item_Cess)
            cmd.Parameters.AddWithValue("@V_A_cess", clsObj.A_Cess)
            cmd.Parameters.AddWithValue("@V_Item_exice", clsObj.Item_exice)
            cmd.Parameters.AddWithValue("@V_Batch_No", clsObj.Batch_No)
            cmd.Parameters.AddWithValue("@V_Expiry_Date", clsObj.Expiry_Date)
            cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.MaterialReceivedWithoutPO)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.Parameters.AddWithValue("@v_DType", clsObj.DType)
            cmd.Parameters.AddWithValue("@v_DiscountValue", clsObj.DISC)
            cmd.Parameters.AddWithValue("@v_DiscountValue1", clsObj.DISC1)
            cmd.Parameters.AddWithValue("@v_GSTPaid", clsObj.GPaid)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub Update_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL(ByVal clsObj As cls_material_recieved_without_po_master_prop, ByVal cmd As SqlCommand)
            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL"

            cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@V_Item_vat", clsObj.Item_vat)
            cmd.Parameters.AddWithValue("@V_Item_cess", clsObj.Item_Cess)
            cmd.Parameters.AddWithValue("@V_A_cess", clsObj.A_Cess)
            cmd.Parameters.AddWithValue("@V_Item_exice", clsObj.Item_exice)
            cmd.Parameters.AddWithValue("@V_Batch_No", clsObj.Batch_No)
            cmd.Parameters.AddWithValue("@V_Expiry_Date", clsObj.Expiry_Date)
            cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.MaterialReceivedWithoutPO)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.Parameters.AddWithValue("@v_DType", clsObj.DType)
            cmd.Parameters.AddWithValue("@v_DiscountValue", clsObj.DISC)
            cmd.Parameters.AddWithValue("@v_DiscountValue1", clsObj.DISC1)
            cmd.Parameters.AddWithValue("@v_GSTPaid", clsObj.GPaid)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub update_MATERIAL_RECIEVED_WITHOUT_PO_MASTER(ByVal clsObj As cls_material_recieved_without_po_master_prop)
            Try


                cmd = New SqlClient.SqlCommand

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER"

                cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
                cmd.Parameters.AddWithValue("@V_Received_Code", clsObj.Received_Code)
                cmd.Parameters.AddWithValue("@V_Received_No", clsObj.Received_No)
                cmd.Parameters.AddWithValue("@V_Received_Date", clsObj.Received_Date)
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
                cmd.Parameters.AddWithValue("@v_Invoice_No", clsObj.Invoice_No)
                cmd.Parameters.AddWithValue("@v_Invoice_Date", clsObj.Invoice_Date)
                cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
                cmd.Parameters.AddWithValue("@V_mrn_status", clsObj.mrn_status)
                cmd.Parameters.AddWithValue("@V_freight", clsObj.freight)
                cmd.Parameters.AddWithValue("@V_freight_type", clsObj.freight_type)
                cmd.Parameters.AddWithValue("@v_MRNCompanies_ID", clsObj.MRNCompanies_ID)
                cmd.Parameters.AddWithValue("@v_other_charges", clsObj.Other_Charges)
                cmd.Parameters.AddWithValue("@v_Discount_amt", clsObj.Discount_amt)
                cmd.Parameters.AddWithValue("@v_CashDiscount_amt", clsObj.CashDiscount_amt)
                cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsObj.VAT_ON_EXICE)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
                cmd.Parameters.AddWithValue("@v_GROSS_AMOUNT", clsObj.GROSS_AMOUNT)
                cmd.Parameters.AddWithValue("@v_GST_AMOUNT", clsObj.GST_AMOUNT)
                cmd.Parameters.AddWithValue("@v_CESS_AMOUNT", clsObj.CESS_AMOUNT)
                cmd.Parameters.AddWithValue("@v_ACESS_AMOUNT", clsObj.ACESS_AMOUNT)
                cmd.Parameters.AddWithValue("@v_NET_AMOUNT", clsObj.NET_AMOUNT)
                cmd.Parameters.AddWithValue("@V_MRN_TYPE", clsObj.MRNType)
                cmd.Parameters.AddWithValue("@V_Special_Scheme", clsObj.Special_Scheme)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

            Catch ex As Exception
                MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
            End Try

        End Sub

        Public Sub Update_PrintStatus_MRN(ByVal clsobj As cls_material_recieved_without_po_master_prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_UPDATE_PRINT_STATUS_MRN"

            cmd.Parameters.AddWithValue("@V_Received_ID", clsobj.Received_ID)
            cmd.Parameters.AddWithValue("@IsPrinted", clsobj.IsPrinted)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub delete_MATERIAL_RECIEVED_WITHOUT_PO_MASTER(ByVal clsObj As cls_material_recieved_without_po_master_prop)

            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_Received_ID", clsobj.Received_ID)
            cmd.Parameters.AddWithValue("@V_Received_Code", clsobj.Received_Code)
            cmd.Parameters.AddWithValue("@V_Received_No", clsobj.Received_No)
            cmd.Parameters.AddWithValue("@V_Received_Date", clsobj.Received_Date)
            cmd.Parameters.AddWithValue("@V_Purchase_Type", clsobj.Purchase_Type)
            cmd.Parameters.AddWithValue("@V_Vendor_ID", clsobj.Vendor_ID)
            cmd.Parameters.AddWithValue("@V_Remarks", clsobj.Remarks)
            cmd.Parameters.AddWithValue("@V_Po_ID", clsobj.Po_ID)
            cmd.Parameters.AddWithValue("@V_MRN_PREFIX", clsobj.MRN_PREFIX)
            cmd.Parameters.AddWithValue("@V_MRN_NO", clsobj.MRN_NO)
            cmd.Parameters.AddWithValue("@V_Created_By", clsobj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsobj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsobj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsobj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsobj.Division_ID)
            cmd.Parameters.AddWithValue("@V_mrn_status", clsobj.mrn_status)
            cmd.Parameters.AddWithValue("@v_other_charges", clsobj.Other_Charges)
            cmd.Parameters.AddWithValue("@v_Discount_amt", clsobj.Discount_amt)
            cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsobj.VAT_ON_EXICE)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub

        Public Sub Insert_Non_Stockable_Items(ByVal clsObj As cls_material_recieved_without_po_master_prop)
            cmd = New SqlClient.SqlCommand

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_INSERT_NON_STOCKABLE_ITEMS"
            cmd.Parameters.AddWithValue("@V_Received_ID", clsObj.Received_ID)
            cmd.Parameters.AddWithValue("@V_CostCenter_Id", clsObj.CostCenter_ID)
            cmd.Parameters.AddWithValue("@V_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@V_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@v_Item_vat", clsObj.Item_vat)
            cmd.Parameters.AddWithValue("@v_Item_cess", clsObj.Item_Cess)
            cmd.Parameters.AddWithValue("@v_Item_Exice", clsObj.Item_exice)
            cmd.Parameters.AddWithValue("@v_batch_no", clsObj.Batch_No)
            cmd.Parameters.AddWithValue("@v_batch_date", clsObj.Expiry_Date)
            cmd.Parameters.AddWithValue("@v_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@v_DType", clsObj.DType)
            cmd.Parameters.AddWithValue("@v_DiscountValue", clsObj.DISC)
            cmd.Parameters.AddWithValue("@v_DiscountValue1", clsObj.DISC1)
            cmd.Parameters.AddWithValue("@v_GSTPaid", clsObj.GPaid)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        End Sub

    End Class

End Namespace