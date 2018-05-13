Imports System.Data.SqlClient
Imports System.Reflection

Public Class BillDetail
    Public Property itemNo As Integer
    Public Property productName As String
    Public Property productDesc As String
    Public Property hsnCode As String
    Public Property quantity As Integer
    Public Property qtyUnit As String
    Public Property taxableAmount As Integer
    Public Property sgstRate As Integer
    Public Property cgstRate As Integer
    Public Property igstRate As Integer
    Public Property cessRate As Integer
End Class

Public Class Bill
    Public Property userGstin As String
    Public Property supplyType As String
    Public Property subSupplyType As Integer
    Public Property docType As String
    Public Property docNo As String
    Public Property docDate As String
    Public Property fromGstin As String
    Public Property fromTrdName As String
    Public Property fromAddr1 As String
    Public Property fromAddr2 As String
    Public Property fromPlace As String
    Public Property fromPincode As Integer
    Public Property fromStateCode As Integer
    Public Property toGstin As String
    Public Property toTrdName As String
    Public Property toAddr1 As String
    Public Property toAddr2 As String
    Public Property toPlace As String
    Public Property toPincode As Integer
    Public Property toStateCode As Integer
    Public Property totalValue As Integer
    Public Property cgstValue As Double
    Public Property sgstValue As Double
    Public Property igstValue As Integer
    Public Property cessValue As Integer
    Public Property transMode As Integer
    Public Property transDistance As Integer
    Public Property transporterName As String
    Public Property transporterId As String
    Public Property transDocNo As String
    Public Property transDocDate As String
    Public Property vehicleNo As String
    Public Property itemList As List(Of BillDetail)
End Class

Public Class EWayBill
    Inherits CommonClass
    Public Property version As String
    Public Property billLists As List(Of Bill)

    Public Function GetEWayBillInJson(orderIds As List(Of Int32)) As String
        version = "1.0.0123"
        billLists = New List(Of Bill)
        For Each orderId As Int32 In orderIds
            Dim ds As DataSet = GetOrderDetail(orderId)
            billLists.Add(TableToObject(ds))
        Next
        Return Newtonsoft.Json.JsonConvert.SerializeObject(Me)
    End Function

    Private Function TableToObject(ds As DataSet) As Bill
        Dim bill As New Bill()

        For Each col As DataColumn In ds.Tables(0).Columns
            Dim row As DataRow = ds.Tables(0).Rows(0)
            Dim prop As PropertyInfo = bill.GetType().GetProperty(col.ColumnName)
            SetPropertyValue(bill, col, row, prop)
        Next
        bill.itemList = New List(Of BillDetail)
        For Each row As DataRow In ds.Tables(1).Rows
            Dim billDetail As New BillDetail
            For Each col As DataColumn In ds.Tables(1).Columns
                Dim prop As PropertyInfo = billDetail.GetType().GetProperty(col.ColumnName)
                SetPropertyValue(billDetail, col, row, prop)
            Next
            bill.itemList.Add(billDetail)
        Next
        Return bill
    End Function

    Private Shared Sub SetPropertyValue(obj As Object, col As DataColumn, row As DataRow, prop As PropertyInfo)
        If prop.PropertyType Is Type.GetType("System.DateTime") Then
            prop.SetValue(obj, Convert.ToDateTime(row(col.ColumnName)), Nothing)
        ElseIf prop.PropertyType Is Type.GetType("System.Int32") Then
            prop.SetValue(obj, Convert.ToInt32(row(col.ColumnName)), Nothing)
        ElseIf prop.PropertyType Is Type.GetType("System.String") Then
            prop.SetValue(obj, Convert.ToString(row(col.ColumnName)), Nothing)
        ElseIf prop.PropertyType Is Type.GetType("System.Double") Then
            prop.SetValue(obj, Convert.ToDouble(row(col.ColumnName)), Nothing)
        End If
    End Sub

    Private Function GetOrderDetail(orderId As Int32) As DataSet
        Dim query As String
        query = "select TIN_NO AS userGstin, 0 AS supplyType, 1 AS subSupplyType, 'INV' AS docType" &
            " from DIVISION_SETTINGS, dbo.SALE_INVOICE_MASTER inv" &
            " WHERE inv.SI_ID = " & orderId & ";"
        query += " SELECT ROW_NUMBER() OVER(ORDER BY ITEM_ID ASC) AS itemNo, '' AS productName, '' AS productDesc," &
            " ITEM_QTY AS quantity  FROM dbo.SALE_INVOICE_DETAIL WHERE SI_ID = " & orderId & ";"
        Return Fill_DataSet(query)
    End Function


End Class
