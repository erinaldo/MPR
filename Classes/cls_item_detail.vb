Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace item_detail

    Public Class cls_item_detail_prop

        Dim _ITEM_ID As Decimal
        Dim _DIV_ID As Int32
        Dim _RE_ORDER_LEVEL As Decimal
        Dim _RE_ORDER_QTY As Decimal
        Dim _PURCHASE_VAT_ID As Int32
        Dim _SALE_VAT_ID As Int32
        Dim _OPENING_STOCK As Decimal
        Dim _CURRENT_STOCK As Decimal
        Dim _IS_EXTERNAL As Int32
        Dim _TRANSFER_RATE As Decimal
        Dim _OPENING_RATE As Decimal
        Dim _AVERAGE_RATE As Decimal
        Dim _IS_STOCKABLE As Boolean
        Dim _IS_active As Boolean
        Dim _BATCH_NO As String
        Dim _EXPIRY_DATE As DateTime


        Public Property ITEM_ID() As Decimal
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Decimal)
                _ITEM_ID = value
            End Set
        End Property

        Public Property DIV_ID() As Int32
            Get
                DIV_ID = _DIV_ID
            End Get
            Set(ByVal value As Int32)
                _DIV_ID = value
            End Set
        End Property

        Public Property RE_ORDER_LEVEL() As Decimal
            Get
                RE_ORDER_LEVEL = _RE_ORDER_LEVEL
            End Get
            Set(ByVal value As Decimal)
                _RE_ORDER_LEVEL = value
            End Set
        End Property

        Public Property RE_ORDER_QTY() As Decimal
            Get
                RE_ORDER_QTY = _RE_ORDER_QTY
            End Get
            Set(ByVal value As Decimal)
                _RE_ORDER_QTY = value
            End Set
        End Property

        Public Property PURCHASE_VAT_ID() As Int32
            Get
                PURCHASE_VAT_ID = _PURCHASE_VAT_ID
            End Get
            Set(ByVal value As Int32)
                _PURCHASE_VAT_ID = value
            End Set
        End Property

        Public Property SALE_VAT_ID() As Int32
            Get
                SALE_VAT_ID = _SALE_VAT_ID
            End Get
            Set(ByVal value As Int32)
                _SALE_VAT_ID = value
            End Set
        End Property

        Public Property OPENING_STOCK() As Decimal
            Get
                OPENING_STOCK = _OPENING_STOCK
            End Get
            Set(ByVal value As Decimal)
                _OPENING_STOCK = value
            End Set
        End Property

        Public Property CURRENT_STOCK() As Decimal
            Get
                CURRENT_STOCK = _CURRENT_STOCK
            End Get
            Set(ByVal value As Decimal)
                _CURRENT_STOCK = value
            End Set
        End Property

        Public Property IS_EXTERNAL() As Int32
            Get
                IS_EXTERNAL = _IS_EXTERNAL
            End Get
            Set(ByVal value As Int32)
                _IS_EXTERNAL = value
            End Set
        End Property

        Public Property TRANSFER_RATE() As Decimal
            Get
                TRANSFER_RATE = _TRANSFER_RATE
            End Get
            Set(ByVal value As Decimal)
                _TRANSFER_RATE = value
            End Set
        End Property
        Public Property OPENING_RATE() As Double
            Get
                OPENING_RATE = _OPENING_RATE
            End Get
            Set(ByVal value As Double)
                _OPENING_RATE = value
            End Set
        End Property

        Public Property AVERAGE_RATE() As Decimal
            Get
                AVERAGE_RATE = _AVERAGE_RATE
            End Get
            Set(ByVal value As Decimal)
                _AVERAGE_RATE = value
            End Set
        End Property

        Public Property IS_STOCKABLE() As Boolean
            Get
                IS_STOCKABLE = _IS_STOCKABLE
            End Get
            Set(ByVal value As Boolean)
                _IS_STOCKABLE = value
            End Set
        End Property

        Public Property IS_active() As Boolean
            Get
                IS_active = _IS_active
            End Get
            Set(ByVal value As Boolean)
                _IS_active = value
            End Set
        End Property

        Public Property BATCH_NO() As String
            Get
                BATCH_NO = _BATCH_NO
            End Get
            Set(ByVal value As String)
                _BATCH_NO = value
            End Set
        End Property

        Public Property EXPIRY_DATE() As DateTime
            Get
                EXPIRY_DATE = _EXPIRY_DATE
            End Get
            Set(ByVal value As DateTime)
                _EXPIRY_DATE = value
            End Set
        End Property

    End Class

    Public Class cls_item_detail
        Inherits CommonClass

        Public Sub insert_ITEM_DETAIL(ByVal clsObj As cls_item_detail_prop)
            Dim trans As SqlTransaction
            Try
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                trans = con.BeginTransaction

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_ITEM_DETAIL"

                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                cmd.Parameters.AddWithValue("@V_DIV_ID", clsObj.DIV_ID)
                cmd.Parameters.AddWithValue("@V_RE_ORDER_LEVEL", clsObj.RE_ORDER_LEVEL)
                cmd.Parameters.AddWithValue("@V_RE_ORDER_QTY", clsObj.RE_ORDER_QTY)
                cmd.Parameters.AddWithValue("@V_PURCHASE_VAT_ID", clsObj.PURCHASE_VAT_ID)
                cmd.Parameters.AddWithValue("@V_SALE_VAT_ID", clsObj.SALE_VAT_ID)
                cmd.Parameters.AddWithValue("@V_OPENING_STOCK", clsObj.OPENING_STOCK)
                cmd.Parameters.AddWithValue("@V_CURRENT_STOCK", clsObj.CURRENT_STOCK)
                cmd.Parameters.AddWithValue("@V_IS_EXTERNAL", clsObj.IS_EXTERNAL)
                cmd.Parameters.AddWithValue("@V_TRANSFER_RATE", clsObj.TRANSFER_RATE)
                cmd.Parameters.AddWithValue("@V_OPENING_RATE", clsObj.OPENING_RATE)
                cmd.Parameters.AddWithValue("@V_AVERAGE_RATE", clsObj.AVERAGE_RATE)
                cmd.Parameters.AddWithValue("@V_IS_STOCKABLE", clsObj.IS_STOCKABLE)
                cmd.Parameters.AddWithValue("@v_IS_ACTIVE", clsObj.IS_active)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                cmd.Parameters.AddWithValue("@v_Batch_no", clsObj.BATCH_NO)
                cmd.Parameters.AddWithValue("@v_Expiry_Date", clsObj.EXPIRY_DATE)
                cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.OpeningStock)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()


                trans.Commit()
                con.Close()
                '*******************************************************
            Catch ex As Exception
                trans.Rollback()

                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Save_rec clsItemDetail")
            End Try
        End Sub

        Public Sub update_ITEM_DETAIL(ByVal clsObj As cls_item_detail_prop)
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_ITEM_DETAIL"

                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                cmd.Parameters.AddWithValue("@V_DIV_ID", clsObj.DIV_ID)
                cmd.Parameters.AddWithValue("@V_RE_ORDER_LEVEL", clsObj.RE_ORDER_LEVEL)
                cmd.Parameters.AddWithValue("@V_RE_ORDER_QTY", clsObj.RE_ORDER_QTY)
                cmd.Parameters.AddWithValue("@V_PURCHASE_VAT_ID", clsObj.PURCHASE_VAT_ID)
                cmd.Parameters.AddWithValue("@V_SALE_VAT_ID", clsObj.SALE_VAT_ID)
                cmd.Parameters.AddWithValue("@V_OPENING_STOCK", clsObj.OPENING_STOCK)
                cmd.Parameters.AddWithValue("@V_CURRENT_STOCK", clsObj.CURRENT_STOCK)
                cmd.Parameters.AddWithValue("@V_IS_EXTERNAL", clsObj.IS_EXTERNAL)
                cmd.Parameters.AddWithValue("@V_TRANSFER_RATE", clsObj.TRANSFER_RATE)
                cmd.Parameters.AddWithValue("@V_AVERAGE_RATE", clsObj.AVERAGE_RATE)
                cmd.Parameters.AddWithValue("@V_OPENING_RATE", clsObj.OPENING_RATE)
                cmd.Parameters.AddWithValue("@V_IS_STOCKABLE", clsObj.IS_STOCKABLE)
                cmd.Parameters.AddWithValue("@v_IS_ACTIVE", clsObj.IS_active)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
                cmd.Parameters.AddWithValue("@v_Batch_no", clsObj.BATCH_NO)
                cmd.Parameters.AddWithValue("@v_Expiry_Date", clsObj.EXPIRY_DATE)
                cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.OpeningStock)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()


                '    '################################
                '    'UPDATE IN GLOBAL DATABASE
                '    '################################
                '    Dim conGBL As New SqlConnection(gblDNS_Online)
                '    cmd = New SqlClient.SqlCommand
                '    cmd.Connection = conGBL
                '    cmd.CommandType = CommandType.StoredProcedure
                '    cmd.CommandText = "PROC_ITEM_DETAIL"

                '    cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                '    cmd.Parameters.AddWithValue("@V_DIV_ID", clsObj.DIV_ID)
                '    cmd.Parameters.AddWithValue("@V_RE_ORDER_LEVEL", clsObj.RE_ORDER_LEVEL)
                '    cmd.Parameters.AddWithValue("@V_RE_ORDER_QTY", clsObj.RE_ORDER_QTY)
                '    cmd.Parameters.AddWithValue("@V_PURCHASE_VAT_ID", clsObj.PURCHASE_VAT_ID)
                '    cmd.Parameters.AddWithValue("@V_SALE_VAT_ID", clsObj.SALE_VAT_ID)
                '    cmd.Parameters.AddWithValue("@V_OPENING_STOCK", clsObj.OPENING_STOCK)
                '    cmd.Parameters.AddWithValue("@V_CURRENT_STOCK", clsObj.CURRENT_STOCK)
                '    cmd.Parameters.AddWithValue("@V_IS_EXTERNAL", clsObj.IS_EXTERNAL)
                '    cmd.Parameters.AddWithValue("@V_TRANSFER_RATE", clsObj.TRANSFER_RATE)
                '    cmd.Parameters.AddWithValue("@V_AVERAGE_RATE", clsObj.AVERAGE_RATE)
                '    cmd.Parameters.AddWithValue("@V_IS_STOCKABLE", clsObj.IS_STOCKABLE)
                '    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
                '    If conGBL.State = ConnectionState.Closed Then conGBL.Open()
                '    cmd.ExecuteNonQuery()
                '    cmd.Dispose()
                '    conGBL.Close()
                '    conGBL.Dispose()
                '    '**** Comment by Ajinder 
                '    '****if connection close here it will give connection error while selecting new item from grid after updation
                '    'con.Close()
                '    'con.Dispose()
                '    '**************************************
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update_rec clsItemDetail")
            End Try
        End Sub

        Public Sub delete_ITEM_DETAIL(ByVal clsObj As cls_item_detail_prop)
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_ITEM_DETAIL"

                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.ITEM_ID)
                'cmd.Parameters.AddWithValue("@V_DIV_ID", clsObj.DIV_ID)
                'cmd.Parameters.AddWithValue("@V_RE_ORDER_LEVEL", clsObj.RE_ORDER_LEVEL)
                'cmd.Parameters.AddWithValue("@V_RE_ORDER_QTY", clsObj.RE_ORDER_QTY)
                'cmd.Parameters.AddWithValue("@V_PURCHASE_VAT_ID", clsObj.PURCHASE_VAT_ID)
                'cmd.Parameters.AddWithValue("@V_SALE_VAT_ID", clsObj.SALE_VAT_ID)
                'cmd.Parameters.AddWithValue("@V_OPENING_STOCK", clsObj.OPENING_STOCK)
                'cmd.Parameters.AddWithValue("@V_CURRENT_STOCK", clsObj.CURRENT_STOCK)
                'cmd.Parameters.AddWithValue("@V_IS_EXTERNAL", clsObj.IS_EXTERNAL)
                'cmd.Parameters.AddWithValue("@V_TRANSFER_RATE", clsObj.TRANSFER_RATE)
                'cmd.Parameters.AddWithValue("@V_AVERAGE_RATE", clsObj.AVERAGE_RATE)
                'cmd.Parameters.AddWithValue("@V_IS_STOCKABLE", clsObj.IS_STOCKABLE)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Delete_rec clsItemDetail")
            End Try
        End Sub

        Public Sub get_ITEM_MASTER(ByVal clsObj As cls_item_detail_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_ITEM_DETAIL"
            cmd.Parameters.AddWithValue("@ITEM_ID", clsObj.ITEM_ID)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        End Sub

        Public Sub update_item_transfer_rate(ByVal clsobj As cls_item_detail_prop)
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "update_item_transfer_rate"

                cmd.Parameters.AddWithValue("@item_id", clsobj.ITEM_ID)
                cmd.Parameters.AddWithValue("@item_rate", clsobj.TRANSFER_RATE)

                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

    End Class

End Namespace
