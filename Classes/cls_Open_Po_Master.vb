Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace open_po_master

    Public Class cls_open_po_master_prop

        Dim _PO_ID As Double
        Dim _PO_CODE As String
        Dim _PO_NO As String
        Dim _PO_TYPE As Int32
        Dim _PO_DATE As DateTime
        Dim _PO_START_DATE As DateTime
        Dim _PO_END_DATE As DateTime
        Dim _PO_REMARKS As String
        Dim _PO_SUPP_ID As Int32
        Dim _PO_QUALITY_ID As Int32
        Dim _PO_DELIVERY_ID As Int32
        Dim _PO_STATUS As Int32
        Dim _PATMENT_TERMS As String
        Dim _TRANSPORT_MODE As String
        Dim _TOTAL_AMOUNT As Double
        Dim _VAT_AMOUNT As Double
        Dim _NET_AMOUNT As Double
        Dim _EXICE_AMOUNT As Double
        'Dim _OCTROI As Int32
        'Dim _PRICE_BASIS As Double
        'Dim _FRIEGHT As Double
        Dim _OCTROI As Int32
        Dim _PRICE_BASIS As Int32
        Dim _FRIEGHT As Int32
        Dim _OTHER_CHARGES As Double
        Dim _CESS_PER As Double
        Dim _ALREADY_RECVD As Boolean
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _ITEM_NAME As String
        Dim _UOM As Double
        Dim _ITEM_QTY As Double
        Dim _VAT_PER As Double
        Dim _EXICE_PER As Double
        Dim _ITEM_RATE As Double
        Dim _AMOUNT As Double
        Dim _Trans As SqlTransaction

#Region "Properties"

        Public Property PO_ID() As Integer
            Get
                PO_ID = _PO_ID
            End Get
            Set(ByVal value As Integer)
                _PO_ID = value
            End Set
        End Property

        Public Property PO_CODE() As String
            Get
                PO_CODE = _PO_CODE
            End Get
            Set(ByVal value As String)
                _PO_CODE = value
            End Set
        End Property

        Public Property PO_NO() As String
            Get
                PO_NO = _PO_NO
            End Get
            Set(ByVal value As String)
                _PO_NO = value
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

        Public Property PO_DATE() As DateTime
            Get
                PO_DATE = _PO_DATE
            End Get
            Set(ByVal value As DateTime)
                _PO_DATE = value
            End Set
        End Property

        Public Property PO_START_DATE() As DateTime
            Get
                PO_START_DATE = _PO_START_DATE
            End Get
            Set(ByVal value As DateTime)
                _PO_START_DATE = value
            End Set
        End Property

        Public Property PO_END_DATE() As DateTime
            Get
                PO_END_DATE = _PO_END_DATE
            End Get
            Set(ByVal value As DateTime)
                _PO_END_DATE = value
            End Set
        End Property

        Public Property PO_REMARKS() As String
            Get
                PO_REMARKS = _PO_REMARKS
            End Get
            Set(ByVal value As String)
                _PO_REMARKS = value
            End Set
        End Property

        Public Property PO_SUPP_ID() As Int32
            Get
                PO_SUPP_ID = _PO_SUPP_ID
            End Get
            Set(ByVal value As Int32)
                _PO_SUPP_ID = value
            End Set
        End Property

        Public Property PO_QUALITY_ID() As Int32
            Get
                PO_QUALITY_ID = _PO_QUALITY_ID
            End Get
            Set(ByVal value As Int32)
                _PO_QUALITY_ID = value
            End Set
        End Property

        Public Property PO_DELIVERY_ID() As Int32
            Get
                PO_DELIVERY_ID = _PO_DELIVERY_ID
            End Get
            Set(ByVal value As Int32)
                _PO_DELIVERY_ID = value
            End Set
        End Property

        Public Property PO_STATUS() As Int32
            Get
                PO_STATUS = _PO_STATUS
            End Get
            Set(ByVal value As Int32)
                _PO_STATUS = value
            End Set
        End Property

        Public Property PATMENT_TERMS() As String
            Get
                PATMENT_TERMS = _PATMENT_TERMS
            End Get
            Set(ByVal value As String)
                _PATMENT_TERMS = value
            End Set
        End Property

        Public Property TRANSPORT_MODE() As String
            Get
                TRANSPORT_MODE = _TRANSPORT_MODE
            End Get
            Set(ByVal value As String)
                _TRANSPORT_MODE = value
            End Set
        End Property

        Public Property TOTAL_AMOUNT() As Double
            Get
                TOTAL_AMOUNT = _TOTAL_AMOUNT
            End Get
            Set(ByVal value As Double)
                _TOTAL_AMOUNT = value
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

        Public Property EXICE_AMOUNT() As Double
            Get
                EXICE_AMOUNT = _EXICE_AMOUNT
            End Get
            Set(ByVal value As Double)
                _EXICE_AMOUNT = value
            End Set
        End Property

        'Public Property OCTROI() As Double
        '    Get
        '        OCTROI = _OCTROI
        '    End Get
        '    Set(ByVal value As Double)
        '        _OCTROI = value
        '    End Set
        'End Property

        'Public Property PRICE_BASIS() As Double
        '    Get
        '        PRICE_BASIS = _PRICE_BASIS
        '    End Get
        '    Set(ByVal value As Double)
        '        _PRICE_BASIS = value
        '    End Set
        'End Property

        'Public Property FRIEGHT() As Double
        '    Get
        '        FRIEGHT = _FRIEGHT
        '    End Get
        '    Set(ByVal value As Double)
        '        _FRIEGHT = value
        '    End Set
        'End Property

        Public Property OCTROI() As Int32
            Get
                OCTROI = _OCTROI
            End Get
            Set(ByVal value As Int32)
                _OCTROI = value
            End Set
        End Property

        Public Property PRICE_BASIS() As Int32
            Get
                PRICE_BASIS = _PRICE_BASIS
            End Get
            Set(ByVal value As Int32)
                _PRICE_BASIS = value
            End Set
        End Property

        Public Property FRIEGHT() As Int32
            Get
                FRIEGHT = _FRIEGHT
            End Get
            Set(ByVal value As Int32)
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

        Public Property CESS_PER() As Double
            Get
                CESS_PER = _CESS_PER
            End Get
            Set(ByVal value As Double)
                _CESS_PER = value
            End Set
        End Property

        Public Property ALREADY_RECVD() As Boolean
            Get
                ALREADY_RECVD = _ALREADY_RECVD
            End Get
            Set(ByVal value As Boolean)
                _ALREADY_RECVD = value
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

        Public Property ITEM_NAME() As String
            Get
                ITEM_NAME = _ITEM_NAME
            End Get
            Set(ByVal value As String)
                _ITEM_NAME = value
            End Set
        End Property

        Public Property UOM() As Integer
            Get
                UOM = _UOM
            End Get
            Set(ByVal value As Integer)
                _UOM = value
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

        Public Property VAT_PER() As Double
            Get
                VAT_PER = _VAT_PER
            End Get
            Set(ByVal value As Double)
                _VAT_PER = value
            End Set
        End Property

        Public Property EXICE_PER() As Double
            Get
                EXICE_PER = _EXICE_PER
            End Get
            Set(ByVal value As Double)
                _EXICE_PER = value
            End Set
        End Property

        Public Property ITEM_RATE() As Double
            Get
                ITEM_RATE = _ITEM_RATE
            End Get
            Set(ByVal value As Double)
                _ITEM_RATE = value
            End Set
        End Property

        Public Property AMOUNT() As Double
            Get
                AMOUNT = _AMOUNT
            End Get
            Set(ByVal value As Double)
                _AMOUNT = value
            End Set
        End Property

#End Region

    End Class

    Public Class cls_open_po_master
        Inherits CommonClass

        'Public Sub insert_OPEN_PO_MASTER(ByVal clsObj As cls_open_po_master_prop)

        '    cmd = New SqlClient.SqlCommand
        '    cmd.Connection = con
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.CommandText = "PROC_OPEN_PO_MASTER"

        '    cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_CODE", clsObj.PO_CODE)
        '    cmd.Parameters.AddWithValue("@V_PO_NO", clsObj.PO_NO)
        '    cmd.Parameters.AddWithValue("@V_PO_TYPE", clsObj.PO_TYPE)
        '    cmd.Parameters.AddWithValue("@V_PO_DATE", clsObj.PO_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsObj.PO_START_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsObj.PO_END_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsObj.PO_REMARKS)
        '    cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsObj.PO_SUPP_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsObj.PO_QUALITY_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsObj.PO_DELIVERY_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_STATUS", clsObj.PO_STATUS)
        '    cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsObj.PATMENT_TERMS)
        '    cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsObj.TRANSPORT_MODE)
        '    cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsObj.VAT_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsObj.NET_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_EXICE_AMOUNT", clsObj.EXICE_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_OCTROI", clsObj.OCTROI)
        '    cmd.Parameters.AddWithValue("@V_PRICE_BASIS", clsObj.PRICE_BASIS)
        '    cmd.Parameters.AddWithValue("@V_FRIEGHT", clsObj.FRIEGHT)
        '    cmd.Parameters.AddWithValue("@V_OTHER_CHARGES", clsObj.OTHER_CHARGES)
        '    cmd.Parameters.AddWithValue("@V_CESS_PER", clsObj.CESS_PER)
        '    cmd.Parameters.AddWithValue("@V_ALREADY_RECVD", clsObj.ALREADY_RECVD)
        '    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
        '    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
        '    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
        '    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

        '    cmd.ExecuteNonQuery()
        '    cmd.Dispose()
        '    'con.Close()
        '    'con.Dispose()

        'End Sub

        Public Sub insert_OPEN_PO_MASTERTrans(ByVal clsObj As cls_open_po_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_OPEN_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
            cmd.Parameters.AddWithValue("@V_PO_CODE", clsObj.PO_CODE)
            cmd.Parameters.AddWithValue("@V_PO_NO", clsObj.PO_NO)
            cmd.Parameters.AddWithValue("@V_PO_TYPE", clsObj.PO_TYPE)
            cmd.Parameters.AddWithValue("@V_PO_DATE", clsObj.PO_DATE)
            cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsObj.PO_START_DATE)
            cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsObj.PO_END_DATE)
            cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsObj.PO_REMARKS)
            cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsObj.PO_SUPP_ID)
            cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsObj.PO_QUALITY_ID)
            cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsObj.PO_DELIVERY_ID)
            cmd.Parameters.AddWithValue("@V_PO_STATUS", clsObj.PO_STATUS)
            cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsObj.PATMENT_TERMS)
            cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsObj.TRANSPORT_MODE)
            cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
            cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsObj.VAT_AMOUNT)
            cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsObj.NET_AMOUNT)
            cmd.Parameters.AddWithValue("@V_EXICE_AMOUNT", clsObj.EXICE_AMOUNT)
            cmd.Parameters.AddWithValue("@V_OCTROI", clsObj.OCTROI)
            cmd.Parameters.AddWithValue("@V_PRICE_BASIS", clsObj.PRICE_BASIS)
            cmd.Parameters.AddWithValue("@V_FRIEGHT", clsObj.FRIEGHT)
            cmd.Parameters.AddWithValue("@V_OTHER_CHARGES", clsObj.OTHER_CHARGES)
            cmd.Parameters.AddWithValue("@V_CESS_PER", clsObj.CESS_PER)
            cmd.Parameters.AddWithValue("@V_ALREADY_RECVD", clsObj.ALREADY_RECVD)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            'cmd.Parameters.Clear()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub insert_OPEN_PO_DETAILTrans(ByVal clsObj As cls_open_po_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'cmd.Connection = con
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_OPEN_PO_DETAIL"

            cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
            cmd.Parameters.AddWithValue("@V_UOM", clsObj.UOM)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsObj.ITEM_QTY)
            cmd.Parameters.AddWithValue("@V_VAT_PER", clsObj.VAT_PER)
            cmd.Parameters.AddWithValue("@V_EXICE_PER", clsObj.EXICE_PER)
            cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
            cmd.Parameters.AddWithValue("@V_AMOUNT", clsObj.AMOUNT)
            cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            Dim S As Integer
            S = cmd.Parameters.Count()
            'If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()

            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub update_OPEN_PO_MASTERTrans(ByVal clsObj As cls_open_po_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_OPEN_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
            cmd.Parameters.AddWithValue("@V_PO_CODE", clsObj.PO_CODE)
            cmd.Parameters.AddWithValue("@V_PO_NO", clsObj.PO_NO)
            cmd.Parameters.AddWithValue("@V_PO_TYPE", clsObj.PO_TYPE)
            cmd.Parameters.AddWithValue("@V_PO_DATE", clsObj.PO_DATE)
            cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsObj.PO_START_DATE)
            cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsObj.PO_END_DATE)
            cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsObj.PO_REMARKS)
            cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsObj.PO_SUPP_ID)
            cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsObj.PO_QUALITY_ID)
            cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsObj.PO_DELIVERY_ID)
            cmd.Parameters.AddWithValue("@V_PO_STATUS", clsObj.PO_STATUS)
            cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsObj.PATMENT_TERMS)
            cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsObj.TRANSPORT_MODE)
            cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
            cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsObj.VAT_AMOUNT)
            cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsObj.NET_AMOUNT)
            cmd.Parameters.AddWithValue("@V_EXICE_AMOUNT", clsObj.EXICE_AMOUNT)
            cmd.Parameters.AddWithValue("@V_OCTROI", clsObj.OCTROI)
            cmd.Parameters.AddWithValue("@V_PRICE_BASIS", clsObj.PRICE_BASIS)
            cmd.Parameters.AddWithValue("@V_FRIEGHT", clsObj.FRIEGHT)
            cmd.Parameters.AddWithValue("@V_OTHER_CHARGES", clsObj.OTHER_CHARGES)
            cmd.Parameters.AddWithValue("@V_CESS_PER", clsObj.CESS_PER)
            cmd.Parameters.AddWithValue("@V_ALREADY_RECVD", clsObj.ALREADY_RECVD)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            'cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub update_OPEN_PO_DETAILTrans(ByVal clsObj As cls_open_po_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            'cmd.Connection = con
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_OPEN_PO_DETAIL"

            cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
            cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
            cmd.Parameters.AddWithValue("@V_UOM", clsObj.UOM)
            cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsObj.ITEM_QTY)
            cmd.Parameters.AddWithValue("@V_VAT_PER", clsObj.VAT_PER)
            cmd.Parameters.AddWithValue("@V_EXICE_PER", clsObj.EXICE_PER)
            cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
            cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            'If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            'cmd.Parameters.Clear()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub


        Public Sub delete_OPEN_PO_DETAIL(ByVal clsObj As cls_open_po_master_prop, ByVal cmd As SqlCommand)

            'cmd = New SqlClient.SqlCommand
            ' cmd.Connection = con
            Try
                cmd.Parameters.Clear()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_OPEN_PO_DETAIL"

                cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
                cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
                cmd.Parameters.AddWithValue("@V_UOM", clsObj.UOM)
                cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsObj.ITEM_QTY)
                cmd.Parameters.AddWithValue("@V_VAT_PER", clsObj.VAT_PER)
                cmd.Parameters.AddWithValue("@V_EXICE_PER", clsObj.EXICE_PER)
                cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
                cmd.Parameters.AddWithValue("@V_AMOUNT", clsObj.AMOUNT)
                cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                'con.Close()
                'con.Dispose()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update_rec clsLocationMaster")
            End Try

        End Sub
        Public Sub delete_OPEN_PO_MASTER(ByVal clsObj As cls_open_po_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_OPEN_PO_MASTER"

            cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
            cmd.Parameters.AddWithValue("@V_PO_CODE", clsObj.PO_CODE)
            cmd.Parameters.AddWithValue("@V_PO_NO", clsObj.PO_NO)
            cmd.Parameters.AddWithValue("@V_PO_DATE", clsObj.PO_DATE)
            cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsObj.PO_START_DATE)
            cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsObj.PO_END_DATE)
            cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsObj.PO_REMARKS)
            cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsObj.PO_SUPP_ID)
            cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsObj.PO_QUALITY_ID)
            cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsObj.PO_DELIVERY_ID)
            cmd.Parameters.AddWithValue("@V_PO_STATUS", clsObj.PO_STATUS)
            cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsObj.PATMENT_TERMS)
            cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsObj.TRANSPORT_MODE)
            cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
            cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsObj.VAT_AMOUNT)
            cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsObj.NET_AMOUNT)
            cmd.Parameters.AddWithValue("@V_EXICE_AMOUNT", clsObj.EXICE_AMOUNT)
            cmd.Parameters.AddWithValue("@V_OCTROI", clsObj.OCTROI)
            cmd.Parameters.AddWithValue("@V_PRICE_BASIS", clsObj.PRICE_BASIS)
            cmd.Parameters.AddWithValue("@V_FRIEGHT", clsObj.FRIEGHT)
            cmd.Parameters.AddWithValue("@V_OTHER_CHARGES", clsObj.OTHER_CHARGES)
            cmd.Parameters.AddWithValue("@V_CESS_PER", clsObj.CESS_PER)
            cmd.Parameters.AddWithValue("@V_ALREADY_RECVD", clsObj.ALREADY_RECVD)
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

        'Public Sub insert_OPEN_PO_MASTER(ByVal clsObj As cls_open_po_master_prop)

        '    cmd = New SqlClient.SqlCommand
        '    cmd.Connection = con
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.CommandText = "PROC_OPEN_PO_MASTER"

        '    cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_CODE", clsObj.PO_CODE)
        '    cmd.Parameters.AddWithValue("@V_PO_NO", clsObj.PO_NO)
        '    cmd.Parameters.AddWithValue("@V_PO_TYPE", clsObj.PO_TYPE)
        '    cmd.Parameters.AddWithValue("@V_PO_DATE", clsObj.PO_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsObj.PO_START_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsObj.PO_END_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsObj.PO_REMARKS)
        '    cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsObj.PO_SUPP_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsObj.PO_QUALITY_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsObj.PO_DELIVERY_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_STATUS", clsObj.PO_STATUS)
        '    cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsObj.PATMENT_TERMS)
        '    cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsObj.TRANSPORT_MODE)
        '    cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsObj.VAT_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsObj.NET_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_EXICE_AMOUNT", clsObj.EXICE_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_OCTROI", clsObj.OCTROI)
        '    cmd.Parameters.AddWithValue("@V_PRICE_BASIS", clsObj.PRICE_BASIS)
        '    cmd.Parameters.AddWithValue("@V_FRIEGHT", clsObj.FRIEGHT)
        '    cmd.Parameters.AddWithValue("@V_OTHER_CHARGES", clsObj.OTHER_CHARGES)
        '    cmd.Parameters.AddWithValue("@V_CESS_PER", clsObj.CESS_PER)
        '    cmd.Parameters.AddWithValue("@V_ALREADY_RECVD", clsObj.ALREADY_RECVD)
        '    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
        '    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
        '    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
        '    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

        '    cmd.ExecuteNonQuery()
        '    cmd.Dispose()
        '    'con.Close()
        '    'con.Dispose()
        'End Sub

        'Public Sub insert_OPEN_PO_DETAIL(ByVal clsObj As cls_open_po_master_prop)

        '    cmd = New SqlClient.SqlCommand
        '    cmd.Connection = con
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.CommandText = "PROC_OPEN_PO_DETAIL"

        '    cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
        '    cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
        '    cmd.Parameters.AddWithValue("@V_UOM", clsObj.UOM)
        '    cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsObj.ITEM_QTY)
        '    cmd.Parameters.AddWithValue("@V_VAT_PER", clsObj.VAT_PER)
        '    cmd.Parameters.AddWithValue("@V_EXICE_PER", clsObj.EXICE_PER)
        '    cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
        '    cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
        '    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
        '    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
        '    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
        '    If con.State = ConnectionState.Closed Then con.Open()
        '    cmd.ExecuteNonQuery()
        '    cmd.Dispose()
        '    'con.Close()
        '    'con.Dispose()
        'End Sub

        'Public Sub update_OPEN_PO_MASTER(ByVal clsObj As cls_open_po_master_prop)

        '    cmd = New SqlClient.SqlCommand
        '    cmd.Connection = con
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.CommandText = "PROC_OPEN_PO_MASTER"

        '    cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_CODE", clsObj.PO_CODE)
        '    cmd.Parameters.AddWithValue("@V_PO_NO", clsObj.PO_NO)
        '    cmd.Parameters.AddWithValue("@V_PO_TYPE", clsObj.PO_TYPE)
        '    cmd.Parameters.AddWithValue("@V_PO_DATE", clsObj.PO_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_START_DATE", clsObj.PO_START_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_END_DATE", clsObj.PO_END_DATE)
        '    cmd.Parameters.AddWithValue("@V_PO_REMARKS", clsObj.PO_REMARKS)
        '    cmd.Parameters.AddWithValue("@V_PO_SUPP_ID", clsObj.PO_SUPP_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_QUALITY_ID", clsObj.PO_QUALITY_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_DELIVERY_ID", clsObj.PO_DELIVERY_ID)
        '    cmd.Parameters.AddWithValue("@V_PO_STATUS", clsObj.PO_STATUS)
        '    cmd.Parameters.AddWithValue("@V_PATMENT_TERMS", clsObj.PATMENT_TERMS)
        '    cmd.Parameters.AddWithValue("@V_TRANSPORT_MODE", clsObj.TRANSPORT_MODE)
        '    cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_VAT_AMOUNT", clsObj.VAT_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_NET_AMOUNT", clsObj.NET_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_EXICE_AMOUNT", clsObj.EXICE_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_OCTROI", clsObj.OCTROI)
        '    cmd.Parameters.AddWithValue("@V_PRICE_BASIS", clsObj.PRICE_BASIS)
        '    cmd.Parameters.AddWithValue("@V_FRIEGHT", clsObj.FRIEGHT)
        '    cmd.Parameters.AddWithValue("@V_OTHER_CHARGES", clsObj.OTHER_CHARGES)
        '    cmd.Parameters.AddWithValue("@V_CESS_PER", clsObj.CESS_PER)
        '    cmd.Parameters.AddWithValue("@V_ALREADY_RECVD", clsObj.ALREADY_RECVD)
        '    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
        '    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
        '    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
        '    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

        '    cmd.ExecuteNonQuery()
        '    cmd.Dispose()
        '    'con.Close()
        '    'con.Dispose()

        'End Sub

        'Public Sub update_OPEN_PO_DETAIL(ByVal clsObj As cls_open_po_master_prop)

        '    cmd = New SqlClient.SqlCommand
        '    cmd.Connection = con
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.CommandText = "PROC_OPEN_PO_DETAIL"

        '    cmd.Parameters.AddWithValue("@V_PO_ID", clsObj.PO_ID)
        '    cmd.Parameters.AddWithValue("@V_ITEM_NAME", clsObj.ITEM_NAME)
        '    cmd.Parameters.AddWithValue("@V_UOM", clsObj.UOM)
        '    cmd.Parameters.AddWithValue("@V_ITEM_QTY", clsObj.ITEM_QTY)
        '    cmd.Parameters.AddWithValue("@V_VAT_PER", clsObj.VAT_PER)
        '    cmd.Parameters.AddWithValue("@V_EXICE_PER", clsObj.EXICE_PER)
        '    cmd.Parameters.AddWithValue("@V_ITEM_RATE", clsObj.ITEM_RATE)
        '    cmd.Parameters.AddWithValue("@V_TOTAL_AMOUNT", clsObj.TOTAL_AMOUNT)
        '    cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
        '    cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
        '    cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
        '    cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
        '    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
        '    If con.State = ConnectionState.Closed Then con.Open()
        '    cmd.ExecuteNonQuery()
        '    cmd.Dispose()
        '    'con.Close()
        '    'con.Dispose()

        'End Sub




        Public Function get_OPEN_PO_date_wise(ByVal supp_id As String, ByVal po_number As String, ByVal Sdate As String, ByVal Edate As String, ByVal mode As Integer, ByVal status As Integer, ByVal division_id As Integer) As DataSet
            Dim adp As SqlDataAdapter = New SqlDataAdapter("GET_FILTER_OPEN_PO_LIST", con)
            Dim ds As New DataSet

            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@v_supp_id", supp_id)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_code", po_number)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_date_from", Sdate)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_date_to", Edate)
            adp.SelectCommand.Parameters.AddWithValue("@v_po_status", status)
            adp.SelectCommand.Parameters.AddWithValue("@v_div_id", division_id)

            adp.Fill(ds)
            Return ds
        End Function

        Public Sub OPEN_po_status_change(ByVal po_ids As String, ByVal status As Integer)
            cmd = New SqlCommand()
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.Connection = con
            cmd.CommandText = "PROC_OPEN_PO_STATUS_CHANGE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@PO_ids", po_ids)
            cmd.Parameters.AddWithValue("@status", status)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()

        End Sub



    End Class
End Namespace
