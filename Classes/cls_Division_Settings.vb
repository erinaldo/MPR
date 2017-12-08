Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace Division_Settings

    Public Class cls_Division_Settings_Prop

        Dim _DIV_ID As Decimal
        Dim _DIVISION_NAME As String
        Dim _DIVISION_ADDRESS As String
        Dim _TIN_NO As String
        Dim _PHONE1 As String
        Dim _PHONE2 As String
        Dim _ZIP_CODE As String
        Dim _MAIL_ADD As String
        Dim _CITY_ID As Int32
        Dim _COST_CENTER_PREFIX As String
        Dim _INDENT_PREFIX As String
        Dim _WASTAGE_PREFIX As String
        Dim _REV_WASTAGE_PREFIX As String
        Dim _RECEIPT_PREFIX As String
        Dim _MIO_PREFIX As String
        Dim _RMIO_PREFIX As String
        Dim _MRSMainStorePREFIX As String
        Dim _RMRN_PREFIX As String
        Dim _RPMRN_PREFIX As String


        Dim _ADJUSTMENT_PREFIX As String
        Dim _TRANSFER_PREFIX As String
        Dim _CLOSING_PREFIX As String
        Dim _WASTAGE_PREFIX_CC As String
        Dim _REV_WASTAGE_PREFIX_CC As String
        Dim _BANK_NAME As String
        Dim _ACCOUNT_NO As String
        Dim _BRANCH_ADDRESS As String
        Dim _IFSC_CODE As String
        Dim _AUTH_SIGNATORY As String
        Dim _fk_CompanyId_num As Int32

        Dim _TYPE As Int32




        Public Property DIV_ID() As Decimal
            Get
                DIV_ID = _DIV_ID
            End Get
            Set(ByVal value As Decimal)
                _DIV_ID = value
            End Set
        End Property

        Public Property DIVISION_NAME() As String
            Get
                DIVISION_NAME = _DIVISION_NAME
            End Get
            Set(ByVal value As String)
                _DIVISION_NAME = value
            End Set
        End Property

        Public Property DIVISION_ADDRESS() As String
            Get
                DIVISION_ADDRESS = _DIVISION_ADDRESS
            End Get
            Set(ByVal value As String)
                _DIVISION_ADDRESS = value
            End Set
        End Property

        Public Property TIN_NO() As String
            Get
                TIN_NO = _TIN_NO
            End Get
            Set(ByVal value As String)
                _TIN_NO = value
            End Set
        End Property

        Public Property PHONE1() As String
            Get
                PHONE1 = _PHONE1
            End Get
            Set(ByVal value As String)
                _PHONE1 = value
            End Set
        End Property

        Public Property PHONE2() As String
            Get
                PHONE2 = _PHONE2
            End Get
            Set(ByVal value As String)
                _PHONE2 = value
            End Set
        End Property

        Public Property ZIP_CODE() As String
            Get
                ZIP_CODE = _ZIP_CODE
            End Get
            Set(ByVal value As String)
                _ZIP_CODE = value
            End Set
        End Property

        Public Property MAIL_ADD() As String
            Get
                MAIL_ADD = _MAIL_ADD
            End Get
            Set(ByVal value As String)
                _MAIL_ADD = value
            End Set
        End Property


        Public Property CITY_ID() As Int32
            Get
                CITY_ID = _CITY_ID
            End Get
            Set(ByVal value As Int32)
                _CITY_ID = value
            End Set
        End Property

        Public Property COST_CENTER_PREFIX() As String
            Get
                COST_CENTER_PREFIX = _COST_CENTER_PREFIX
            End Get
            Set(ByVal value As String)
                _COST_CENTER_PREFIX = value
            End Set
        End Property

        Public Property INDENT_PREFIX() As String
            Get
                INDENT_PREFIX = _INDENT_PREFIX
            End Get
            Set(ByVal value As String)
                _INDENT_PREFIX = value
            End Set
        End Property

        Public Property WASTAGE_PREFIX() As String
            Get
                WASTAGE_PREFIX = _WASTAGE_PREFIX
            End Get
            Set(ByVal value As String)
                _WASTAGE_PREFIX = value
            End Set
        End Property

        Public Property REV_WASTAGE_PREFIX() As String
            Get
                REV_WASTAGE_PREFIX = _REV_WASTAGE_PREFIX
            End Get
            Set(ByVal value As String)
                _REV_WASTAGE_PREFIX = value
            End Set
        End Property

        Public Property RECEIPT_PREFIX() As String
            Get
                RECEIPT_PREFIX = _RECEIPT_PREFIX
            End Get
            Set(ByVal value As String)
                _RECEIPT_PREFIX = value
            End Set
        End Property

        Public Property MIO_PREFIX() As String
            Get
                MIO_PREFIX = _MIO_PREFIX
            End Get
            Set(ByVal value As String)
                _MIO_PREFIX = value
            End Set
        End Property

        Public Property RMIO_PREFIX() As String
            Get
                RMIO_PREFIX = _RMIO_PREFIX
            End Get
            Set(ByVal value As String)
                _RMIO_PREFIX = value
            End Set
        End Property


        Public Property MRSMainStorePREFIX() As String
            Get
                MRSMainStorePREFIX = _MRSMainStorePREFIX
            End Get
            Set(ByVal value As String)
                _MRSMainStorePREFIX = value
            End Set
        End Property

        Public Property RMRN_PREFIX() As String
            Get
                RMRN_PREFIX = _RMRN_PREFIX
            End Get
            Set(ByVal value As String)
                _RMRN_PREFIX = value
            End Set
        End Property

        Public Property RPMRN_PREFIX() As String
            Get
                RPMRN_PREFIX = _RPMRN_PREFIX
            End Get
            Set(ByVal value As String)
                _RPMRN_PREFIX = value
            End Set
        End Property


        Public Property ADJUSTMENT_PREFIX() As String
            Get
                ADJUSTMENT_PREFIX = _ADJUSTMENT_PREFIX
            End Get
            Set(ByVal value As String)
                _ADJUSTMENT_PREFIX = value
            End Set
        End Property

        Public Property TRANSFER_PREFIX() As String
            Get
                TRANSFER_PREFIX = _TRANSFER_PREFIX
            End Get
            Set(ByVal value As String)
                _TRANSFER_PREFIX = value
            End Set
        End Property

        Public Property CLOSING_PREFIX() As String
            Get
                CLOSING_PREFIX = _CLOSING_PREFIX
            End Get
            Set(ByVal value As String)
                _CLOSING_PREFIX = value
            End Set
        End Property

        Public Property WASTAGE_PREFIX_CC() As String
            Get
                WASTAGE_PREFIX_CC = _WASTAGE_PREFIX_CC
            End Get
            Set(ByVal value As String)
                _WASTAGE_PREFIX_CC = value
            End Set
        End Property

        Public Property REV_WASTAGE_PREFIX_CC() As String
            Get
                REV_WASTAGE_PREFIX_CC = _REV_WASTAGE_PREFIX_CC
            End Get
            Set(ByVal value As String)
                _REV_WASTAGE_PREFIX_CC = value
            End Set
        End Property

        Public Property BANK_NAME() As String
            Get
                BANK_NAME = _BANK_NAME
            End Get
            Set(ByVal value As String)
                _BANK_NAME = value
            End Set
        End Property

        Public Property ACCOUNT_NO() As String
            Get
                ACCOUNT_NO = _ACCOUNT_NO
            End Get
            Set(ByVal value As String)
                _ACCOUNT_NO = value
            End Set
        End Property

        Public Property BRANCH_ADDRESS() As String
            Get
                BRANCH_ADDRESS = _BRANCH_ADDRESS
            End Get
            Set(ByVal value As String)
                _BRANCH_ADDRESS = value
            End Set
        End Property

        Public Property IFSC_CODE() As String
            Get
                IFSC_CODE = _IFSC_CODE
            End Get
            Set(ByVal value As String)
                _IFSC_CODE = value
            End Set
        End Property

        Public Property AUTH_SIGNATORY() As String
            Get
                AUTH_SIGNATORY = _AUTH_SIGNATORY
            End Get
            Set(ByVal value As String)
                _AUTH_SIGNATORY = value
            End Set
        End Property

        Public Property fk_CompanyId_num() As Int32
            Get
                fk_CompanyId_num = _fk_CompanyId_num
            End Get
            Set(ByVal value As Int32)
                _fk_CompanyId_num = value
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
    Public Class cls_Division_Settings
        Inherits CommonClass
        Public Function INSERT_DIVISION_SETTINGS(ByVal clsObj As cls_Division_Settings_Prop) As [String]

            ''IF TYpe = 1 then insert else Update 
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "INSERT_DIVISION_SETTINGS"

                cmd.Parameters.AddWithValue("@DIV_ID", clsObj.DIV_ID)
                cmd.Parameters.AddWithValue("@DIVISION_NAME", clsObj.DIVISION_NAME)
                cmd.Parameters.AddWithValue("@DIVISION_ADDRESS", clsObj.DIVISION_ADDRESS)
                cmd.Parameters.AddWithValue("@TIN_NO", clsObj.TIN_NO)
                cmd.Parameters.AddWithValue("@PHONE1", clsObj.PHONE1)
                cmd.Parameters.AddWithValue("@PHONE2", clsObj.PHONE2)
                cmd.Parameters.AddWithValue("@ZIP_CODE", clsObj.ZIP_CODE)
                cmd.Parameters.AddWithValue("@MAIL_ADD", clsObj.MAIL_ADD)
                cmd.Parameters.AddWithValue("@CITY_ID", clsObj.CITY_ID)
                cmd.Parameters.AddWithValue("@COST_CENTER_PREFIX", clsObj.COST_CENTER_PREFIX)
                cmd.Parameters.AddWithValue("@INDENT_PREFIX", clsObj.INDENT_PREFIX)
                cmd.Parameters.AddWithValue("@WASTAGE_PREFIX", clsObj.WASTAGE_PREFIX)
                cmd.Parameters.AddWithValue("@REV_WASTAGE_PREFIX", clsObj.REV_WASTAGE_PREFIX)
                cmd.Parameters.AddWithValue("@RECEIPT_PREFIX", clsObj.RECEIPT_PREFIX)
                cmd.Parameters.AddWithValue("@MIO_PREFIX", clsObj.MIO_PREFIX)
                cmd.Parameters.AddWithValue("@RMIO_PREFIX", clsObj.RMIO_PREFIX)
                cmd.Parameters.AddWithValue("@MRSMainStorePREFIX", clsObj.MRSMainStorePREFIX)
                cmd.Parameters.AddWithValue("@RMRN_PREFIX", clsObj.RMRN_PREFIX)
                cmd.Parameters.AddWithValue("@RPMRN_PREFIX", clsObj.RPMRN_PREFIX)
                cmd.Parameters.AddWithValue("@ADJUSTMENT_PREFIX", clsObj.ADJUSTMENT_PREFIX)
                cmd.Parameters.AddWithValue("TRANSFER_PREFIX", clsObj.TRANSFER_PREFIX)
                cmd.Parameters.AddWithValue("CLOSING_PREFIX", clsObj.CLOSING_PREFIX)
                cmd.Parameters.AddWithValue("WASTAGE_PREFIX_CC", clsObj.WASTAGE_PREFIX_CC)
                cmd.Parameters.AddWithValue("REV_WASTAGE_PREFIX_CC", clsObj.REV_WASTAGE_PREFIX_CC)
                cmd.Parameters.AddWithValue("BANK_NAME", clsObj.BANK_NAME)
                cmd.Parameters.AddWithValue("ACCOUNT_NO", clsObj.ACCOUNT_NO)
                cmd.Parameters.AddWithValue("BRANCH_ADDRESS", clsObj.BRANCH_ADDRESS)
                cmd.Parameters.AddWithValue("IFSC_CODE", clsObj.IFSC_CODE)
                cmd.Parameters.AddWithValue("AUTH_SIGNATORY", clsObj.AUTH_SIGNATORY)
                cmd.Parameters.AddWithValue("fk_CompanyId_num", clsObj.fk_CompanyId_num)


                cmd.Parameters.AddWithValue("@TYPE", clsObj.TYPE)

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()
                con.Dispose()
                Return ("Record Inserted Successfully")
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function
    End Class


End Namespace
