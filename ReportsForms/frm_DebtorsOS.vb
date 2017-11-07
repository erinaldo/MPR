Imports System.Data.SqlClient
Public Class frm_DebtorsOS
    'Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New cls_Supplier_Invoice_Settlement
    Dim _rights As Form_Rights
    Dim PaymentId As Int16
    Dim flag As String
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub
End Class
