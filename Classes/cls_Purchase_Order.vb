Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

' <summary>
' Summary description for cls_Purchase_Order_Master
' </summary>

Namespace Purchase_Order
    Public Class cls_Purchase_Order_Prop
        Dim _PO_ID As Int32
        Dim _PO_TYPE As Int32
        Dim _PO_CODE As [String]
        Dim _PO_NO As [Decimal]
        Dim _PO_DATE As DateTime
        Dim _PO_START_DATE As DateTime
        Dim _PO_END_DATE As DateTime
        Dim _PO_REMARKS As [String]
        Dim _PO_SUPP_ID As Int32
        Dim _PO_QUALITY_ID As Int32
        Dim _PO_DELIVERY_ID As Int32
        Dim _PO_STATUS As Int32
        Dim _PATMENT_TERMS As [String]
        Dim _PRICE_BASIS_REMARKS As [String]
        Dim _TRANSPORT_MODE As [String]
        Dim _TOTAL_AMOUNT As [Decimal]
        Dim _VAT_AMOUNT As [Decimal]
        Dim _NET_AMOUNT As [Decimal]
        Dim _CREATED_BY As [String]
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As [String]
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _PO_Items As DataTable
        Dim _EXICE_AMOUNT As Double
        Dim _OCTROI As String
        Dim _FRIEGHT As Double
        Dim _OTHER_CHARGES As Double
        Dim _PRICE_BASIS As String
        Dim _CESS As Double
        Dim _DISCOUNT_AMOUNT As Double
        Dim _OPEN_PO_QTY As Boolean
        Dim _VAT_ON_EXICE As Int32

        Public Property PO_ID() As Int32
            Get
                Return _PO_ID
            End Get
            Set(ByVal value As Int32)
                _PO_ID = value
            End Set
        End Property
        Public Property CESS() As [Decimal]
            Get
                Return _CESS
            End Get
            Set(ByVal value As [Decimal])
                _CESS = value
            End Set
        End Property


        Public Property PO_CODE() As [String]
            Get
                Return _PO_CODE
            End Get
            Set(ByVal value As [String])
                _PO_CODE = value
            End Set
        End Property

        Public Property PO_NO() As [Decimal]
            Get
                Return _PO_NO
            End Get
            Set(ByVal value As [Decimal])
                _PO_NO = value
            End Set
        End Property

        Public Property PO_DATE() As DateTime
            Get
                Return _PO_DATE
            End Get
            Set(ByVal value As DateTime)
                _PO_DATE = value
            End Set
        End Property

        Public Property PO_START_DATE() As DateTime
            Get
                Return _PO_START_DATE
            End Get
            Set(ByVal value As DateTime)
                _PO_START_DATE = value
            End Set
        End Property

        Public Property PO_END_DATE() As DateTime
            Get
                Return _PO_END_DATE
            End Get
            Set(ByVal value As DateTime)
                _PO_END_DATE = value
            End Set
        End Property

        Public Property PO_REMARKS() As [String]
            Get
                Return _PO_REMARKS
            End Get
            Set(ByVal value As [String])
                _PO_REMARKS = value
            End Set
        End Property

        Public Property PO_SUPP_ID() As Int32
            Get
                Return _PO_SUPP_ID
            End Get
            Set(ByVal value As Int32)
                _PO_SUPP_ID = value
            End Set
        End Property

        Public Property PO_QUALITY_ID() As Int32
            Get
                Return _PO_QUALITY_ID
            End Get
            Set(ByVal value As Int32)
                _PO_QUALITY_ID = value
            End Set
        End Property

        Public Property PO_DELIVERY_ID() As Int32
            Get
                Return _PO_DELIVERY_ID
            End Get
            Set(ByVal value As Int32)
                _PO_DELIVERY_ID = value
            End Set
        End Property

        Public Property PO_STATUS() As Int32
            Get
                Return _PO_STATUS
            End Get
            Set(ByVal value As Int32)
                _PO_STATUS = value
            End Set
        End Property

        Public Property PATMENT_TERMS() As [String]
            Get
                Return _PATMENT_TERMS
            End Get
            Set(ByVal value As [String])
                _PATMENT_TERMS = value
            End Set
        End Property

        Public Property PRICE_BASIS_REMARKS() As [String]
            Get
                Return _PRICE_BASIS_REMARKS
            End Get
            Set(ByVal value As [String])
                _PRICE_BASIS_REMARKS = value
            End Set
        End Property

        Public Property TRANSPORT_MODE() As [String]
            Get
                Return _TRANSPORT_MODE
            End Get
            Set(ByVal value As [String])
                _TRANSPORT_MODE = value
            End Set
        End Property

        Public Property TOTAL_AMOUNT() As [Decimal]
            Get
                Return _TOTAL_AMOUNT
            End Get
            Set(ByVal value As [Decimal])
                _TOTAL_AMOUNT = value
            End Set
        End Property

        Public Property VAT_AMOUNT() As [Decimal]
            Get
                Return _VAT_AMOUNT
            End Get
            Set(ByVal value As [Decimal])
                _VAT_AMOUNT = value
            End Set
        End Property

        Public Property NET_AMOUNT() As [Decimal]
            Get
                Return _NET_AMOUNT
            End Get
            Set(ByVal value As [Decimal])
                _NET_AMOUNT = value
            End Set
        End Property

        Public Property CREATED_BY() As [String]
            Get
                Return _CREATED_BY
            End Get
            Set(ByVal value As [String])
                _CREATED_BY = value
            End Set
        End Property

        Public Property CREATION_DATE() As DateTime
            Get
                Return _CREATION_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATION_DATE = value
            End Set
        End Property

        Public Property MODIFIED_BY() As [String]
            Get
                Return _MODIFIED_BY
            End Get
            Set(ByVal value As [String])
                _MODIFIED_BY = value
            End Set
        End Property

        Public Property MODIFIED_DATE() As DateTime
            Get
                Return _MODIFIED_DATE
            End Get
            Set(ByVal value As DateTime)
                _MODIFIED_DATE = value
            End Set
        End Property

        Public Property DIVISION_ID() As Int32
            Get
                Return _DIVISION_ID
            End Get
            Set(ByVal value As Int32)
                _DIVISION_ID = value
            End Set
        End Property

        Public Property OPEN_PO_QTY() As Int32
            Get
                Return _OPEN_PO_QTY
            End Get
            Set(ByVal value As Int32)
                _OPEN_PO_QTY = value
            End Set
        End Property

        Public Property PO_Items() As DataTable
            Get
                Return _PO_Items
            End Get
            Set(ByVal value As DataTable)
                _PO_Items = value
            End Set
        End Property

        Public Property EXICE_AMOUNT() As Double
            Get
                EXICE_AMOUNT = _EXICE_AMOUNT
            End Get
            Set(ByVal value As Double)
                _EXICE_AMOUNT = value
            End Set
        End Property

        Public Property PO_TYPE() As Int32
            Get
                PO_TYPE = _PO_TYPE
            End Get
            Set(ByVal value As Int32)
                _PO_TYPE = value
            End Set
        End Property

        Public Property OCTROI() As String
            Get
                OCTROI = _OCTROI
            End Get
            Set(ByVal value As String)
                _OCTROI = value
            End Set
        End Property

        Public Property PRICE_BASIS() As String
            Get
                PRICE_BASIS = _PRICE_BASIS
            End Get
            Set(ByVal value As String)
                _PRICE_BASIS = value
            End Set
        End Property

        Public Property FRIEGHT() As Double
            Get
                FRIEGHT = _FRIEGHT
            End Get
            Set(ByVal value As Double)
                _FRIEGHT = value
            End Set
        End Property

        Public Property OTHER_CHARGES() As Double
            Get
                OTHER_CHARGES = _OTHER_CHARGES
            End Get
            Set(ByVal value As Double)
                _OTHER_CHARGES = value
            End Set
        End Property

        Public Property DISCOUNT_AMOUNT() As [Double]
            Get
                Return _DISCOUNT_AMOUNT
            End Get
            Set(ByVal value As [Double])
                _DISCOUNT_AMOUNT = value
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

    End Class

    Public Class cls_Purchase_Order_Master
        Inherits CommonClass

        Public Function Insert_PO_MASTER(ByVal clsPropObj As cls_Purchase_Order_Prop) As [String]

            Dim i As Int32
            Dim _POS_ID As Double
            Dim _Indent_ID As Integer
            Dim _Indent_Code As String

            Dim intRetPOID As Int64

            Dim dv_NonIndentItems As DataView
            Dim tempdt As DataTable
            tempdt = clsPropObj.PO_Items.Copy
            dv_NonIndentItems = tempdt.DefaultView

            dv_NonIndentItems.RowFilter = "indent_id=-1"

            tempdt = dv_NonIndentItems.ToTable()


            Dim commObj As New CommonClass()

            '_POD_ID = commObj.getMaxValue("POD_ID", "PO_DETAIL")
            _POS_ID = commObj.getMaxValue("POS_ID", "PO_STATUS")
            _Indent_ID = commObj.getMaxValue("INDENT_ID", "INDENT_MASTER")
            _Indent_Code = commObj.getPrefixCode("INDENT_PREFIX", "DIVISION_SETTINGS")

            Dim tran As SqlTransaction
            If con.State = ConnectionState.Closed Then con.Open()
            tran = con.BeginTransaction()
            Try
                '<summary>
                ' Seps for Saving Purchase Order
                '   very fist step is to save new indent of non indent items 
                ' 1. Insert in PO_Master Table with transaction.
                ' 2. Insert in PO_Detail Table with Transaction.
                ' 3. Update in PO_SERIES Table  with Transaction.
                ' 4. Insert in PO_Status Table with Transaction.
                ' 5. Update Indent_status Table with Transaction.
                ' 6. Update Indent_Master Table's Status to Clear if all Items are Ordered or Entre in Purchase Order.
                ' </summary>

                '* Code for inserting into Purchase Order Master (PO_Master)  Table 


                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure

                cmd.CommandText = "PROC_INDENT_MASTER"
                cmd.Parameters.AddWithValue("@V_INDENT_ID", _Indent_ID)
                cmd.Parameters.AddWithValue("@V_INDENT_CODE", _Indent_Code)
                cmd.Parameters.AddWithValue("@V_INDENT_NO", _Indent_ID)
                cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsPropObj.PO_DATE)
                cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsPropObj.PO_DATE)
                cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", "Auto generated through Purchase Order." & vbCrLf & vbCrLf & "PO Number is :" & clsPropObj.PO_CODE & clsPropObj.PO_NO.ToString())
                cmd.Parameters.AddWithValue("@V_INDENT_STATUS", IndentStatus.Clear)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()


                For i = 0 To tempdt.Rows.Count - 1

                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.CommandText = "PROC_INDENT_DETAIL"
                    cmd.Parameters.AddWithValue("@V_INDENT_ID", _Indent_ID)
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", tempdt.Rows(i)("item_id"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", tempdt.Rows(i)("po_qty"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", tempdt.Rows(i)("po_qty"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", 0)
                    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.ExecuteNonQuery()

                    cmd.Dispose()
                Next


                tempdt = Nothing


                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_PO_MASTER"
                cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                cmd.Parameters.AddWithValue("@V_PO_CODE", clsPropObj.PO_CODE)
                cmd.Parameters.AddWithValue("@V_PO_NO", clsPropObj.PO_NO)
                cmd.Parameters.AddWithValue("@V_PO_DATE", clsPropObj.PO_DATE)
                cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsPropObj.PO_START_DATE)
                cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsPropObj.PO_END_DATE)
                cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsPropObj.PO_REMARKS)
                cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsPropObj.PO_SUPP_ID)
                cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsPropObj.PO_QUALITY_ID)
                cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsPropObj.PO_DELIVERY_ID)
                cmd.Parameters.AddWithValue("@V_PO_STATUS", clsPropObj.PO_STATUS)
                cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsPropObj.PATMENT_TERMS)
                cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsPropObj.TRANSPORT_MODE)
                cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsPropObj.TOTAL_AMOUNT)
                cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsPropObj.VAT_AMOUNT)
                cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsPropObj.NET_AMOUNT)
                cmd.Parameters.AddWithValue("@v_EXICE_AMOUNT", clsPropObj.EXICE_AMOUNT)
                cmd.Parameters.AddWithValue("@v_PO_TYPE", clsPropObj.PO_TYPE)
                cmd.Parameters.AddWithValue("@v_OCTROI", clsPropObj.OCTROI)
                cmd.Parameters.AddWithValue("@v_PRICE_BASIS", clsPropObj.OCTROI)
                cmd.Parameters.AddWithValue("@v_FRIEGHT", clsPropObj.FRIEGHT)
                cmd.Parameters.AddWithValue("@v_OTHER_CHARGES", clsPropObj.OTHER_CHARGES)
                cmd.Parameters.AddWithValue("@v_CESS", clsPropObj.CESS)
                cmd.Parameters.AddWithValue("@v_DISCOUNT_AMOUNT", clsPropObj.DISCOUNT_AMOUNT)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_OPEN_PO_QTY", clsPropObj.OPEN_PO_QTY)
                cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsPropObj.VAT_ON_EXICE)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                Dim retPOID As New SqlParameter("@PO", SqlDbType.BigInt)
                retPOID.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(retPOID)
                cmd.ExecuteNonQuery()
                intRetPOID = Convert.ToInt64(cmd.Parameters("@PO").Value)
                cmd.Dispose()

                '*  Update  in PO_SERIES Table with Transaction.

                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_PO_SERIES"
                cmd.Parameters.AddWithValue("@V_Div_ID", clsPropObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_CURRENT_USED", clsPropObj.PO_NO)
                cmd.ExecuteNonQuery()
                cmd.Dispose()




                Dim dt As DataTable
                Dim dr As DataRow
                Dim dv As DataView


                dt = clsPropObj.PO_Items.Copy
                dv = dt.DefaultView
                tempdt = dv.ToTable("po_items", True, "item_id")


                dt = New DataTable
                dt.Columns.Add("item_id")
                dt.Columns.Add("item_Qty")
                dt.Columns.Add("vat_per")
                dt.Columns.Add("excise_per")
                dt.Columns.Add("item_rate")
                dt.Columns.Add("DType")
                dt.Columns.Add("DISC")


                For i = 0 To tempdt.Rows.Count - 1
                    dr = dt.NewRow

                    dr("item_id") = tempdt.Rows(i)(0)
                    dr("item_qty") = clsPropObj.PO_Items.Compute("sum(po_qty)", "item_id=" & tempdt.Rows(i)(0))
                    dr("vat_per") = IIf(IsDBNull(clsPropObj.PO_Items.Compute("Max(vat_per)", "item_id=" & tempdt.Rows(i)(0))), 0, clsPropObj.PO_Items.Compute("Max(vat_per)", "item_id=" & tempdt.Rows(i)(0)))
                    dr("excise_per") = IIf(IsDBNull(clsPropObj.PO_Items.Compute("Max(exice_per)", "item_id=" & tempdt.Rows(i)(0))), 0, clsPropObj.PO_Items.Compute("Max(exice_per)", "item_id=" & tempdt.Rows(i)(0)))
                    dr("item_rate") = clsPropObj.PO_Items.Compute("Max(item_rate)", "item_id=" & tempdt.Rows(i)(0))
                    dr("DType") = clsPropObj.PO_Items.Compute("Max(DType)", "item_id=" & tempdt.Rows(i)(0))

                    If clsPropObj.PO_Items.Compute("Max(DType)", "item_id=" & tempdt.Rows(i)(0)).ToString() = "A" Then
                        dr("DISC") = clsPropObj.PO_Items.Compute("sum(DISC)", "item_id=" & tempdt.Rows(i)(0))
                    Else
                        dr("DISC") = clsPropObj.PO_Items.Compute("Max(DISC)", "item_id=" & tempdt.Rows(i)(0))
                    End If

                    dt.Rows.Add(dr)

                Next

                For i = 0 To dt.Rows.Count - 1
                    '* Code for inserting into Purchase Order Detail (PO_Detail)  Table 
                    cmd.Parameters.Clear()
                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_PO_DETAIL"
                    cmd.Parameters.AddWithValue("@V_PO_ID", intRetPOID)
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY", dt.Rows(i)("item_qty"))
                    cmd.Parameters.AddWithValue("@v_VAT_PER", dt.Rows(i)("Vat_Per"))
                    cmd.Parameters.AddWithValue("@v_EXICE_PER", dt.Rows(i)("excise_per"))
                    cmd.Parameters.AddWithValue("@V_ITEM_RATE", dt.Rows(i)("Item_Rate"))

                    cmd.Parameters.AddWithValue("@v_DTYPE", dt.Rows(i)("DType").ToString())
                    cmd.Parameters.AddWithValue("@v_DISC_VALUE", dt.Rows(i)("DISC"))

                    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                Next

                ' '' ''* Code for inserting into Purchase Order Detail (PO_Detail)  Table 
                '' ''cmd.Parameters.Clear()
                '' ''cmd = New SqlCommand()
                '' ''cmd.CommandTimeout = 0
                '' ''cmd.Connection = con
                '' ''cmd.Transaction = tran
                '' ''cmd.CommandType = CommandType.StoredProcedure
                '' ''cmd.CommandText = "PROC_PO_DETAIL"
                '' ''cmd.Parameters.AddWithValue("@V_PO_ID", intRetPOID)
                '' ''cmd.Parameters.AddWithValue("@V_POD_ID", _POD_ID)
                '' ''cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                '' ''cmd.Parameters.AddWithValue("@V_INDENT_ID", dt.Rows(i)("indent_id"))
                '' ''cmd.Parameters.AddWithValue("@V_ITEM_QTY", dt.Rows(i)("PO_Qty"))
                '' ''cmd.Parameters.AddWithValue("@v_VAT_PER", dt.Rows(i)("Vat_Per"))
                '' ''cmd.Parameters.AddWithValue("@v_EXICE_PER", dt.Rows(i)("Exice_Per"))
                '' ''cmd.Parameters.AddWithValue("@V_ITEM_RATE", dt.Rows(i)("Item_Rate"))
                '' ''cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", dt.Rows(i)("Item_Value"))
                '' ''cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                '' ''cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                '' ''cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                '' ''cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                '' ''cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                '' ''cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                '' ''cmd.ExecuteNonQuery()
                '' ''cmd.Dispose()



                dt = clsPropObj.PO_Items
                For i = 0 To dt.Rows.Count - 1

                    'Code for inserting into Purchase Order Status Table
                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_PO_STATUS"
                    cmd.Parameters.AddWithValue("@V_POS_ID", _POS_ID)
                    cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))

                    If dt.Rows(i)("indent_id") = "-1" Then
                        cmd.Parameters.AddWithValue("@V_INDENT_ID", _Indent_ID)
                    Else
                        cmd.Parameters.AddWithValue("@V_INDENT_ID", dt.Rows(i)("indent_id"))
                    End If

                    cmd.Parameters.AddWithValue("@V_REQUIRED_QTY", dt.Rows(i)("PO_Qty"))
                    cmd.Parameters.AddWithValue("@V_RECIEVED_QTY", 0)
                    cmd.Parameters.AddWithValue("@V_BALANCE_QTY", dt.Rows(i)("PO_Qty"))

                    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    _POS_ID += 1

                    'Code for Update Indent Status Table
                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "UPDATE_INDENT_STATUS"
                    cmd.Parameters.AddWithValue("@V_INDENT_ID", dt.Rows(i)("indent_id"))
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                    cmd.Parameters.AddWithValue("@V_PO_QTY", dt.Rows(i)("PO_Qty"))
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    'Code for Update Indent Status Table

                    Dim previous_indent_id As Integer
                    previous_indent_id = 0

                    If previous_indent_id <> Convert.ToInt32(dt.Rows(i)("indent_id")) Then
                        previous_indent_id = Convert.ToInt32(dt.Rows(i)("indent_id"))

                        cmd = New SqlCommand()
                        cmd.CommandTimeout = 0
                        cmd.Connection = con
                        cmd.Transaction = tran
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_INDENT_STATUS_MASTER"
                        cmd.Parameters.AddWithValue("@V_INDENT_ID", previous_indent_id)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                    End If
                Next
                tran.Commit()
                tran.Dispose()
                'con.Close()
                ' con.Dispose()
                Return ("Record saved successfully with code " & clsPropObj.PO_CODE.ToString()) + clsPropObj.PO_NO.ToString()
            Catch ex As Exception
                tran.Rollback()
                con.Close()
                'con.Dispose()
                Return ex.Message

            End Try
        End Function

        Public Function update_PO_MASTER(ByVal clsPropObj As cls_Purchase_Order_Prop) As [String]


            Dim i As Int32
            Dim _POS_ID As Double
            Dim _Indent_ID As Integer
            Dim _Indent_Code As String

            Dim dv_NonIndentItems As DataView
            Dim tempdt As DataTable
            tempdt = clsPropObj.PO_Items.Copy
            dv_NonIndentItems = tempdt.DefaultView

            dv_NonIndentItems.RowFilter = "indent_id=-1"

            tempdt = dv_NonIndentItems.ToTable()


            'Dim intRetPONo As Int64
            Dim commObj As New CommonClass()
            _POS_ID = commObj.getMaxValue("POS_ID", "PO_STATUS")
            _Indent_ID = commObj.getMaxValue("INDENT_ID", "INDENT_MASTER")
            _Indent_Code = commObj.getPrefixCode("INDENT_PREFIX", "DIVISION_SETTINGS")


            Dim tran As SqlTransaction
            tran = con.BeginTransaction()
            Try


                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure

                cmd.CommandText = "PROC_INDENT_MASTER"
                cmd.Parameters.AddWithValue("@V_INDENT_ID", _Indent_ID)
                cmd.Parameters.AddWithValue("@V_INDENT_CODE", _Indent_Code)
                cmd.Parameters.AddWithValue("@V_INDENT_NO", _Indent_ID)
                cmd.Parameters.AddWithValue("@V_INDENT_DATE", clsPropObj.PO_DATE)
                cmd.Parameters.AddWithValue("@V_REQUIRED_DATE", clsPropObj.PO_DATE)
                cmd.Parameters.AddWithValue("@V_INDENT_REMARKS", "Auto generated through Purchase Order." & vbCrLf & vbCrLf & "PO Number is :" & clsPropObj.PO_CODE & clsPropObj.PO_NO.ToString())
                cmd.Parameters.AddWithValue("@V_INDENT_STATUS", IndentStatus.Clear)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()


                For i = 0 To tempDT.Rows.Count - 1

                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.CommandText = "PROC_INDENT_DETAIL"
                    cmd.Parameters.AddWithValue("@V_INDENT_ID", _Indent_ID)
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", tempDT.Rows(i)("item_id"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY_REQ", tempDT.Rows(i)("po_qty"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY_PO", tempDT.Rows(i)("po_qty"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY_BAL", 0)
                    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.ExecuteNonQuery()

                    cmd.Dispose()
                Next


                tempDT = Nothing




                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_PO_MASTER"

                cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                cmd.Parameters.AddWithValue("@V_PO_CODE", clsPropObj.PO_CODE)
                cmd.Parameters.AddWithValue("@V_PO_NO", clsPropObj.PO_NO)
                cmd.Parameters.AddWithValue("@V_PO_DATE", clsPropObj.PO_DATE)
                cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsPropObj.PO_START_DATE)
                cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsPropObj.PO_END_DATE)
                cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsPropObj.PO_REMARKS)
                cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsPropObj.PO_SUPP_ID)
                cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsPropObj.PO_QUALITY_ID)
                cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsPropObj.PO_DELIVERY_ID)
                cmd.Parameters.AddWithValue("@V_PO_STATUS", clsPropObj.PO_STATUS)
                cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsPropObj.PATMENT_TERMS)
                cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsPropObj.TRANSPORT_MODE)
                cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsPropObj.TOTAL_AMOUNT)
                cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsPropObj.VAT_AMOUNT)
                cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsPropObj.NET_AMOUNT)
                cmd.Parameters.AddWithValue("@v_EXICE_AMOUNT", clsPropObj.EXICE_AMOUNT)
                cmd.Parameters.AddWithValue("@v_PO_TYPE", clsPropObj.PO_TYPE)
                cmd.Parameters.AddWithValue("@v_OCTROI", clsPropObj.OCTROI)
                cmd.Parameters.AddWithValue("@v_PRICE_BASIS", clsPropObj.OCTROI)
                cmd.Parameters.AddWithValue("@v_FRIEGHT", clsPropObj.FRIEGHT)
                cmd.Parameters.AddWithValue("@v_OTHER_CHARGES", clsPropObj.OTHER_CHARGES)
                cmd.Parameters.AddWithValue("@v_CESS", clsPropObj.CESS)
                cmd.Parameters.AddWithValue("@v_DISCOUNT_AMOUNT", clsPropObj.DISCOUNT_AMOUNT)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_OPEN_PO_QTY", clsPropObj.OPEN_PO_QTY)
                cmd.Parameters.AddWithValue("@V_VAT_ON_EXICE", clsPropObj.VAT_ON_EXICE)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
                cmd.ExecuteNonQuery()
                cmd.Dispose()


                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = tran
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_PO_MASTER_UPDATE"
                cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                Dim dt As DataTable



                'Dim tempDT As DataTable
                Dim dr As DataRow
                Dim dv As DataView


                dt = clsPropObj.PO_Items.Copy
                dv = dt.DefaultView
                tempDT = dv.ToTable("po_items", True, "item_id")

                dt = New DataTable
                dt.Columns.Add("item_id")
                dt.Columns.Add("item_Qty")
                dt.Columns.Add("vat_per")
                dt.Columns.Add("excise_per")
                dt.Columns.Add("item_rate")
                dt.Columns.Add("DType")
                dt.Columns.Add("DISC")



                For i = 0 To tempDT.Rows.Count - 1
                    dr = dt.NewRow

                    dr("item_id") = tempDT.Rows(i)(0)
                    dr("item_qty") = clsPropObj.PO_Items.Compute("sum(po_qty)", "item_id=" & tempDT.Rows(i)(0))
                    dr("vat_per") = IIf(IsDBNull(clsPropObj.PO_Items.Compute("Max(vat_per)", "item_id=" & tempDT.Rows(i)(0))), 0, clsPropObj.PO_Items.Compute("Max(vat_per)", "item_id=" & tempDT.Rows(i)(0)))
                    dr("excise_per") = IIf(IsDBNull(clsPropObj.PO_Items.Compute("Max(exice_per)", "item_id=" & tempDT.Rows(i)(0))), 0, clsPropObj.PO_Items.Compute("Max(exice_per)", "item_id=" & tempDT.Rows(i)(0)))
                    dr("item_rate") = clsPropObj.PO_Items.Compute("Max(item_rate)", "item_id=" & tempDT.Rows(i)(0))
                    dr("DType") = clsPropObj.PO_Items.Compute("Max(DType)", "item_id=" & tempdt.Rows(i)(0))

                    If clsPropObj.PO_Items.Compute("Max(DType)", "item_id=" & tempdt.Rows(i)(0)).ToString() = "A" Then
                        dr("DISC") = clsPropObj.PO_Items.Compute("sum(DISC)", "item_id=" & tempdt.Rows(i)(0))
                    Else
                        dr("DISC") = clsPropObj.PO_Items.Compute("Max(DISC)", "item_id=" & tempdt.Rows(i)(0))
                    End If
                    dt.Rows.Add(dr)

                Next

                For i = 0 To dt.Rows.Count - 1
                    '* Code for inserting into Purchase Order Detail (PO_Detail)  Table 
                    cmd.Parameters.Clear()
                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_PO_DETAIL"
                    cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                    cmd.Parameters.AddWithValue("@V_ITEM_QTY", dt.Rows(i)("item_qty"))
                    cmd.Parameters.AddWithValue("@v_VAT_PER", dt.Rows(i)("Vat_Per"))
                    cmd.Parameters.AddWithValue("@v_EXICE_PER", dt.Rows(i)("excise_per"))
                    cmd.Parameters.AddWithValue("@V_ITEM_RATE", dt.Rows(i)("Item_Rate"))
                    cmd.Parameters.AddWithValue("@v_DTYPE", dt.Rows(i)("DType").ToString())
                    cmd.Parameters.AddWithValue("@v_DISC_VALUE", dt.Rows(i)("DISC"))
                    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                Next


                dt = clsPropObj.PO_Items

                For i = 0 To dt.Rows.Count - 1
                    'cmd.Parameters.Clear()
                    'cmd = New SqlCommand()
                    'cmd.CommandTimeout = 0
                    'cmd.Connection = con
                    'cmd.Transaction = tran
                    'cmd.CommandType = CommandType.StoredProcedure
                    'cmd.CommandText = "PROC_PO_DETAIL"
                    'cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                    'cmd.Parameters.AddWithValue("@V_POD_ID", _POD_ID)
                    'cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                    'cmd.Parameters.AddWithValue("@V_INDENT_ID", dt.Rows(i)("indent_id"))
                    'cmd.Parameters.AddWithValue("@V_ITEM_QTY", dt.Rows(i)("PO_Qty"))
                    'cmd.Parameters.AddWithValue("@v_VAT_PER", dt.Rows(i)("Vat_Per"))
                    'cmd.Parameters.AddWithValue("@v_EXICE_PER", dt.Rows(i)("Exice_Per"))
                    'cmd.Parameters.AddWithValue("@V_ITEM_RATE", dt.Rows(i)("Item_Rate"))
                    'cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", dt.Rows(i)("Item_Value"))
                    'cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    'cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    'cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    'cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    'cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    'cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    'cmd.ExecuteNonQuery()
                    'cmd.Dispose()


                    'Code for inserting into Purchase Order Status Table
                    cmd = New SqlCommand()
                    cmd.CommandTimeout = 0
                    cmd.Connection = con
                    cmd.Transaction = tran
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_PO_STATUS"
                    cmd.Parameters.AddWithValue("@V_POS_ID", _POS_ID)
                    cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
                    cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                    If dt.Rows(i)("indent_id") = "-1" Then
                        cmd.Parameters.AddWithValue("@V_INDENT_ID", _Indent_ID)
                    Else
                        cmd.Parameters.AddWithValue("@V_INDENT_ID", dt.Rows(i)("indent_id"))
                    End If
                    cmd.Parameters.AddWithValue("@V_REQUIRED_QTY", dt.Rows(i)("PO_Qty"))
                    cmd.Parameters.AddWithValue("@V_RECIEVED_QTY", 0)
                    cmd.Parameters.AddWithValue("@V_BALANCE_QTY", dt.Rows(i)("PO_Qty"))
                    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsPropObj.CREATED_BY)
                    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsPropObj.CREATION_DATE)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsPropObj.MODIFIED_BY)
                    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsPropObj.MODIFIED_DATE)
                    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsPropObj.DIVISION_ID)
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    _POS_ID += 1


                Next


                'tran.Commit()
                'tran.Dispose()

                Try

                    For i = 0 To dt.Rows.Count - 1
                        'Code for Update Indent Status Table

                        cmd = New SqlCommand()
                        cmd.CommandTimeout = 0
                        cmd.Connection = con
                        cmd.Transaction = tran
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_INDENT_STATUS"

                        cmd.Parameters.AddWithValue("@V_INDENT_ID", dt.Rows(i)("indent_id"))
                        cmd.Parameters.AddWithValue("@V_ITEM_ID", dt.Rows(i)("item_id"))
                        cmd.Parameters.AddWithValue("@V_PO_QTY", dt.Rows(i)("PO_QTY"))


                        cmd.ExecuteNonQuery()

                        cmd.Dispose()
                    Next

                    Dim previous_indent_id As Integer
                    previous_indent_id = 0
                    For i = 0 To dt.Rows.Count - 1
                        If previous_indent_id <> Convert.ToInt32(dt.Rows(i)("indent_id")) Then
                            previous_indent_id = Convert.ToInt32(dt.Rows(i)("indent_id"))

                            cmd = New SqlCommand()
                            cmd.CommandTimeout = 0
                            cmd.Transaction = tran
                            cmd.Connection = con
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "UPDATE_INDENT_STATUS_master"

                            cmd.Parameters.AddWithValue("@V_INDENT_ID", previous_indent_id)
                            cmd.ExecuteNonQuery()

                            cmd.Dispose()
                        End If
                    Next
                Catch
                End Try

                tran.Commit()
                tran.Dispose()
                con.Close()
                'con.Dispose()
                Return ("Record updated successfully with code " & clsPropObj.PO_CODE.ToString()) + clsPropObj.PO_NO.ToString()
            Catch ex As Exception

                tran.Rollback()
                con.Close()
                'con.Dispose()
                Return ex.Message

            End Try
        End Function

        'Public Function delete_PO_MASTER(ByVal clsPropObj As cls_PO_Master_Prop) As [String]

        '    Try
        '        cmd = New SqlCommand()
        '        cmd.CommandTimeout = 0
        '        cmd.Connection = con
        '        cmd.CommandType = CommandType.StoredProcedure
        '        cmd.CommandText = "PROC_PO_MASTER_DELETE"
        '        cmd.Parameters.AddWithValue("@V_PO_ID", clsPropObj.PO_ID)
        '        
        '        cmd.ExecuteNonQuery()
        '        cmd.Dispose()
        '        con.Close()
        '        con.Dispose()
        '        Return ""
        '    Catch ex As Exception
        '        con.Close()
        '        con.Dispose()
        '        Return ex.Message

        '    End Try
        'End Function


        Public Function get_PO_date_wise(ByVal supp_id As String, ByVal po_number As String, ByVal Sdate As String, ByVal Edate As String, ByVal mode As Integer, ByVal status As Integer, ByVal division_id As Integer) As DataSet
            Dim adp As SqlDataAdapter = New SqlDataAdapter("GET_FILTER_PO_LIST", con)
            Dim ds As New DataSet

            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@v_supp_id", supp_id)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_number", po_number)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_date_from", Sdate)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_date_to", Edate)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_status", status)
            adp.SelectCommand.Parameters.AddWithValue("@v_div_id", division_id)
            adp.Fill(ds)

            Return ds
        End Function

        Public Sub po_status_change(ByVal indent_ids As String, ByVal status As Integer)
            cmd = New SqlCommand()
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandText = "PROC_PO_STATUS_CHANGE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@PO_ids", indent_ids)
            cmd.Parameters.AddWithValue("@status", status)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()

        End Sub

    End Class
End Namespace
