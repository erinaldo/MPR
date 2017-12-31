Imports System
Imports System.Data
Imports System.Data.SqlClient




Namespace CreditNote


    Public Class cls_Credit_note_Prop

        Dim _CreditNote_ID As Double
        Dim _CreditNote_Code As String
        Dim _CreditNote_No As Double
        Dim _CreditNote_Date As DateTime
        Dim _INV_Id As Integer
        Dim _Remarks As String
        Dim _Created_By As String
        Dim _Creation_Date As DateTime
        Dim _Modified_By As String
        Dim _Modification_Date As DateTime
        Dim _Division_ID As Int32
        Dim _Item_ID As Double
        Dim _Item_Qty As Double
        Dim _Item_rate As Double
        Dim _Item_Tax As Double
        Dim _CN_Amount As Double
        Dim _CN_CustId As Int32
        Dim _Stock_Detail_Id As Double
        Dim _INV_No As String
        Dim _INV_Date As DateTime
        Dim _CN_ItemValue As Double
        Dim _CN_ItemTax As Double

        Public Property CreditNote_ID() As Integer
            Get
                CreditNote_ID = _CreditNote_ID
            End Get
            Set(ByVal value As Integer)
                _CreditNote_ID = value
            End Set
        End Property

        Public Property CreditNote_Code() As String
            Get
                CreditNote_Code = _CreditNote_Code
            End Get
            Set(ByVal value As String)
                _CreditNote_Code = value
            End Set
        End Property

        Public Property CreditNote_No() As Double
            Get
                CreditNote_No = _CreditNote_No
            End Get
            Set(ByVal value As Double)
                _CreditNote_No = value
            End Set
        End Property

        Public Property CreditNote_Date() As DateTime
            Get
                CreditNote_Date = _CreditNote_Date
            End Get
            Set(ByVal value As DateTime)
                _CreditNote_Date = value
            End Set
        End Property

        Public Property INV_ID() As Int32
            Get
                INV_ID = _INV_Id
            End Get
            Set(ByVal value As Int32)
                _INV_Id = value
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
                Item_Rate = _Item_rate
            End Get
            Set(ByVal value As Double)
                _Item_rate = value
            End Set
        End Property

        Public Property Item_Tax() As Double
            Get
                Item_Tax = _Item_Tax
            End Get
            Set(ByVal value As Double)
                _Item_Tax = value
            End Set
        End Property

        Public Property Cn_Amount() As Double
            Get
                Cn_Amount = _CN_Amount
            End Get
            Set(ByVal value As Double)
                _CN_Amount = value
            End Set
        End Property

        Public Property CN_CustId() As Integer
            Get
                CN_CustId = _CN_CustId
            End Get
            Set(ByVal value As Integer)
                _CN_CustId = value
            End Set
        End Property

        Public Property Stock_Detail_ID() As Integer
            Get
                Stock_Detail_ID = _Stock_Detail_Id
            End Get
            Set(ByVal value As Integer)
                _Stock_Detail_Id = value
            End Set
        End Property

        Public Property INV_No() As String
            Get
                INV_No = _INV_No
            End Get
            Set(ByVal value As String)
                _INV_No = value
            End Set
        End Property

        Public Property INV_Date() As DateTime
            Get
                INV_Date = _INV_Date
            End Get
            Set(ByVal value As DateTime)
                _INV_Date = value
            End Set
        End Property

        Public Property CN_ItemValue() As Double
            Get
                CN_ItemValue = _CN_ItemValue
            End Get
            Set(ByVal value As Double)
                _CN_ItemValue = value
            End Set
        End Property

        Public Property CN_ItemTax() As Double
            Get
                CN_ItemTax = _CN_ItemTax
            End Get
            Set(ByVal value As Double)
                _CN_ItemTax = value
            End Set
        End Property

    End Class

    Public Class cls_Credit_note_Master
        Inherits CommonClass
        Public Sub insert_CreditNote_MASTER(ByVal clsObj As cls_Credit_note_Prop, ByVal cmd As SqlCommand)

            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_CreditNote_MASTER"

            cmd.Parameters.AddWithValue("@V_CreditNote_ID", clsObj.CreditNote_ID)
            cmd.Parameters.AddWithValue("@V_CreditNote_Code", clsObj.CreditNote_Code)
            cmd.Parameters.AddWithValue("@V_CreditNote_No", clsObj.CreditNote_No)
            cmd.Parameters.AddWithValue("@V_CreditNote_Date", clsObj.CreditNote_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@v_INV_Id", clsObj.INV_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_CN_Amount", clsObj.Cn_Amount)
            cmd.Parameters.AddWithValue("@v_CN_CustId", clsObj.CN_CustId)
            cmd.Parameters.AddWithValue("@v_INV_No", clsObj.INV_No)
            cmd.Parameters.AddWithValue("@v_INV_Date", clsObj.INV_Date)

            cmd.Parameters.AddWithValue("@v_CN_ItemValue", clsObj.CN_ItemValue)
            cmd.Parameters.AddWithValue("@v_CN_ItemTax", clsObj.CN_ItemTax)

            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub
        Public Sub insert_CreditNote_DETAIL(ByVal clsObj As cls_Credit_note_Prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_CreditNote_DETAIL"

            cmd.Parameters.AddWithValue("@v_CreditNote_ID", clsObj.CreditNote_ID)
            cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@v_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@v_Item_Tax", clsObj.Item_Tax)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Stock_Detail_Id", clsObj.Stock_Detail_ID)
            cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.CreditNote)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub update_CreditNote(ByVal clsObj As cls_Credit_note_Prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_CreditNote_MASTER"

            cmd.Parameters.AddWithValue("@V_CreditNote_ID", clsObj.CreditNote_ID)
            cmd.Parameters.AddWithValue("@V_CreditNote_Code", clsObj.CreditNote_Code)
            cmd.Parameters.AddWithValue("@V_CreditNote_No", clsObj.CreditNote_No)
            cmd.Parameters.AddWithValue("@V_CreditNote_Date", clsObj.CreditNote_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_CN_Amount", clsObj.Cn_Amount)
            cmd.Parameters.AddWithValue("@v_CN_CustId", clsObj.CN_CustId)
            cmd.Parameters.AddWithValue("@v_INV_No", clsObj.INV_No)
            cmd.Parameters.AddWithValue("@v_INV_Date", clsObj.INV_Date)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub

        Public Sub delete_CreditNote(ByVal clsObj As cls_Credit_note_Prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_CreditNote_MASTER"

            cmd.Parameters.AddWithValue("@V_CreditNote_ID", clsObj.CreditNote_ID)
            cmd.Parameters.AddWithValue("@V_CreditNote_Code", clsObj.CreditNote_Code)
            cmd.Parameters.AddWithValue("@V_CreditNote_No", clsObj.CreditNote_No)
            cmd.Parameters.AddWithValue("@V_CreditNote_Date", clsObj.CreditNote_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_CN_Amount", clsObj.Cn_Amount)
            cmd.Parameters.AddWithValue("@v_CN_CustId", clsObj.CN_CustId)
            cmd.Parameters.AddWithValue("@v_INV_No", clsObj.INV_No)
            cmd.Parameters.AddWithValue("@v_INV_Date", clsObj.INV_Date)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub

    End Class
End Namespace
