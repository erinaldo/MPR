Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace DebitNote


    Public Class cls_DebitNote_Prop

        Dim _DebitNote_ID As Double
        Dim _DebitNote_Code As String
        Dim _DebitNote_No As Double
        Dim _DebitNote_Date As DateTime
        Dim _MRN_Id As Integer
        Dim _Indent_ID As Int32
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
        Dim _Item_Cess As Double
        Dim _DN_Amount As Double
        Dim _RoundOff As Double
        Dim _TaxableAmt As Double
        Dim _TaxAmt As Double
        Dim _CessAmt As Double
        Dim _DebitAmt As Double
        Dim _DN_CustId As Int32
        Dim _Stock_Detail_Id As Double
        Dim _INV_No As String
        Dim _INV_Date As DateTime
        Dim _DN_ItemValue As Double
        Dim _DN_ItemTax As Double
        Dim _DN_ItemCess As Double
        Dim _DebitNote_Type As String
        Dim _RefNo As String
        Dim _RefDate_dt As DateTime
        Dim _Tax_num As Double

        Dim _Proctype As Integer
        Public Property Proctype() As Integer
            Get
                Proctype = _Proctype
            End Get
            Set(ByVal value As Integer)
                _Proctype = value
            End Set
        End Property

        Public Property DebitNote_ID() As Integer
            Get
                DebitNote_ID = _DebitNote_ID
            End Get
            Set(ByVal value As Integer)
                _DebitNote_ID = value
            End Set
        End Property

        Public Property DebitNote_Code() As String
            Get
                DebitNote_Code = _DebitNote_Code
            End Get
            Set(ByVal value As String)
                _DebitNote_Code = value
            End Set
        End Property

        Public Property DebitNote_No() As Double
            Get
                DebitNote_No = _DebitNote_No
            End Get
            Set(ByVal value As Double)
                _DebitNote_No = value
            End Set
        End Property

        Public Property DebitNote_Date() As DateTime
            Get
                DebitNote_Date = _DebitNote_Date
            End Get
            Set(ByVal value As DateTime)
                _DebitNote_Date = value
            End Set
        End Property

        Public Property MRN_ID() As Int32
            Get
                MRN_ID = _MRN_Id
            End Get
            Set(ByVal value As Int32)
                _MRN_Id = value
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

        Public Property TaxableAmt() As Double
            Get
                TaxableAmt = _TaxableAmt
            End Get
            Set(ByVal value As Double)
                _TaxableAmt = value
            End Set
        End Property
        Public Property TaxAmt() As Double
            Get
                TaxAmt = _TaxAmt
            End Get
            Set(ByVal value As Double)
                _TaxAmt = value
            End Set
        End Property
        Public Property CessAmt() As Double
            Get
                CessAmt = _CessAmt
            End Get
            Set(ByVal value As Double)
                _CessAmt = value
            End Set
        End Property
        Public Property DebitAmt() As Double
            Get
                DebitAmt = _DebitAmt
            End Get
            Set(ByVal value As Double)
                _DebitAmt = value
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

        Public Property RoundOff() As Double
            Get
                RoundOff = _RoundOff
            End Get
            Set(ByVal value As Double)
                _RoundOff = value
            End Set
        End Property
        Public Property Dn_Amount() As Double
            Get
                Dn_Amount = _DN_Amount
            End Get
            Set(ByVal value As Double)
                _DN_Amount = value
            End Set
        End Property


        Public Property DN_CustId() As Integer
            Get
                DN_CustId = _DN_CustId
            End Get
            Set(ByVal value As Integer)
                _DN_CustId = value
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

        Public Property DN_ItemValue() As Double
            Get
                DN_ItemValue = _DN_ItemValue
            End Get
            Set(ByVal value As Double)
                _DN_ItemValue = value
            End Set
        End Property


        Public Property DN_ItemTax() As Double
            Get
                DN_ItemTax = _DN_ItemTax
            End Get
            Set(ByVal value As Double)
                _DN_ItemTax = value
            End Set
        End Property

        Public Property DN_ItemCess() As Double
            Get
                DN_ItemCess = _DN_ItemCess
            End Get
            Set(ByVal value As Double)
                _DN_ItemCess = value
            End Set
        End Property

        Public Property DN_Type() As String
            Get
                DN_Type = _DebitNote_Type
            End Get
            Set(ByVal value As String)
                _DebitNote_Type = value
            End Set
        End Property

        Public Property Ref_No() As String
            Get
                Ref_No = _RefNo
            End Get
            Set(ByVal value As String)
                _RefNo = value
            End Set
        End Property


        Public Property Ref_Date() As DateTime
            Get
                Ref_Date = _RefDate_dt
            End Get
            Set(ByVal value As DateTime)
                _RefDate_dt = value
            End Set
        End Property

        Public Property Tax_Num() As Double
            Get
                Tax_Num = _Tax_num
            End Get
            Set(ByVal value As Double)
                _Tax_num = value
            End Set
        End Property
    End Class

    Public Class cls_DebitNote_Master
        Inherits CommonClass
        Public Sub insert_DebitNote_MASTER(ByVal clsObj As cls_DebitNote_Prop, ByVal cmd As SqlCommand)

            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_DebitNote_MASTER"
            cmd.Parameters.AddWithValue("@V_DebitNote_ID", clsObj.DebitNote_ID)
            cmd.Parameters.AddWithValue("@V_DebitNote_Code", clsObj.DebitNote_Code)
            cmd.Parameters.AddWithValue("@V_DebitNote_No", clsObj.DebitNote_No)
            cmd.Parameters.AddWithValue("@V_DebitNote_Date", clsObj.DebitNote_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            cmd.Parameters.AddWithValue("@v_MRN_Id", clsObj.MRN_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_DN_Amount", clsObj.Dn_Amount)
            cmd.Parameters.AddWithValue("@v_DN_CustId", clsObj.DN_CustId)
            cmd.Parameters.AddWithValue("@v_INV_No", clsObj.INV_No)
            cmd.Parameters.AddWithValue("@v_INV_Date", clsObj.INV_Date)
            cmd.Parameters.AddWithValue("@v_DN_ItemValue", clsObj.DN_ItemValue)
            cmd.Parameters.AddWithValue("@v_DN_ItemTax", clsObj.DN_ItemTax)
            cmd.Parameters.AddWithValue("@v_DN_ItemCess", clsObj.DN_ItemCess)
            cmd.Parameters.AddWithValue("@v_DebitNote_Type", clsObj.DN_Type)
            cmd.Parameters.AddWithValue("@v_RoundOff", clsObj.RoundOff)
            cmd.Parameters.AddWithValue("@v_RefNo", clsObj.Ref_No)
            cmd.Parameters.AddWithValue("@v_RefDate_dt", clsObj.Ref_Date)
            cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.DebitNote)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", clsObj.Proctype)
            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub
        Public Sub insert_DebitNote_DETAIL(ByVal clsObj As cls_DebitNote_Prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_DebitNote_DETAIL"

            cmd.Parameters.AddWithValue("@v_DebitNote_ID", clsObj.DebitNote_ID)
            cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Stock_Detail_Id", clsObj.Stock_Detail_ID)
            cmd.Parameters.AddWithValue("@v_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@v_Item_Tax", clsObj.Item_Tax)
            cmd.Parameters.AddWithValue("@v_Item_Cess", clsObj.Item_Cess)
            cmd.Parameters.AddWithValue("@v_TaxableAmt", clsObj.TaxableAmt)
            cmd.Parameters.AddWithValue("@v_TaxAmt", clsObj.TaxAmt)
            cmd.Parameters.AddWithValue("@v_CessAmt", clsObj.CessAmt)
            cmd.Parameters.AddWithValue("@v_DebitAmt", clsObj.DebitAmt)
            cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.DebitNote)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", clsObj.Proctype)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub insert_DebitNote_WO_Items_DETAIL(ByVal clsObj As cls_DebitNote_Prop, ByVal cmd As SqlCommand)
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_DebitNote_WO_Items_DETAIL"

            cmd.Parameters.AddWithValue("@v_DebitNote_ID", clsObj.DebitNote_ID)
            cmd.Parameters.AddWithValue("@v_Item_ID", clsObj.Item_ID)
            cmd.Parameters.AddWithValue("@v_Item_Qty", clsObj.Item_Qty)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_Id", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_Stock_Detail_Id", clsObj.Stock_Detail_ID)
            cmd.Parameters.AddWithValue("@v_Item_Rate", clsObj.Item_Rate)
            cmd.Parameters.AddWithValue("@v_Item_Tax", clsObj.Item_Tax)
            cmd.Parameters.AddWithValue("@V_Trans_Type", Transaction_Type.DebitNote)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

        End Sub

        Public Sub update_DebitNote(ByVal clsObj As cls_DebitNote_Prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_DebitNote_MASTER"

            cmd.Parameters.AddWithValue("@V_DebitNote_ID", clsObj.DebitNote_ID)
            cmd.Parameters.AddWithValue("@V_DebitNote_Code", clsObj.DebitNote_Code)
            cmd.Parameters.AddWithValue("@V_DebitNote_No", clsObj.DebitNote_No)
            cmd.Parameters.AddWithValue("@V_DebitNote_Date", clsObj.DebitNote_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            '  cmd.Parameters.AddWithValue("@V_received_ID", clsObj.received_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_DN_Amount", clsObj.Dn_Amount)
            cmd.Parameters.AddWithValue("@v_DN_CustId", clsObj.DN_CustId)
            cmd.Parameters.AddWithValue("@v_RoundOff", clsObj.RoundOff)
            cmd.Parameters.AddWithValue("@v_INV_No", clsObj.INV_No)
            cmd.Parameters.AddWithValue("@v_INV_Date", clsObj.INV_Date)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub

        Public Sub delete_DebitNote(ByVal clsObj As cls_DebitNote_Prop, ByVal cmd As SqlCommand)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_DebitNote_MASTER"

            cmd.Parameters.AddWithValue("@V_DebitNote_ID", clsObj.DebitNote_ID)
            cmd.Parameters.AddWithValue("@V_DebitNote_Code", clsObj.DebitNote_Code)
            cmd.Parameters.AddWithValue("@V_DebitNote_No", clsObj.DebitNote_No)
            cmd.Parameters.AddWithValue("@V_DebitNote_Date", clsObj.DebitNote_Date)
            cmd.Parameters.AddWithValue("@V_Remarks", clsObj.Remarks)
            ' cmd.Parameters.AddWithValue("@V_received_ID", clsObj.received_ID)
            cmd.Parameters.AddWithValue("@V_Created_By", clsObj.Created_By)
            cmd.Parameters.AddWithValue("@V_Creation_Date", clsObj.Creation_Date)
            cmd.Parameters.AddWithValue("@V_Modified_By", clsObj.Modified_By)
            cmd.Parameters.AddWithValue("@V_Modification_Date", clsObj.Modification_Date)
            cmd.Parameters.AddWithValue("@V_Division_ID", clsObj.Division_ID)
            cmd.Parameters.AddWithValue("@v_DN_Amount", clsObj.Dn_Amount)
            cmd.Parameters.AddWithValue("@v_DN_CustId", clsObj.DN_CustId)
            cmd.Parameters.AddWithValue("@v_INV_No", clsObj.INV_No)
            cmd.Parameters.AddWithValue("@v_INV_Date", clsObj.INV_Date)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()


        End Sub

    End Class
End Namespace
