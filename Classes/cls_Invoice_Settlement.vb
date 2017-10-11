Public Class cls_Invoice_Settlement_prop
    Public PaymentTransactionId As Int32
    Public PaymentTransactionCode As Int32
    Public PaymentTypeId As Int32
    Public AccountId As Int32
    Public PaymentDate As DateTime
    Public ReferenceNo As String
    Public ReferenceDate As DateTime
    Public BankId As Int32
    Public Remarks As String
    Public TotalAmountReceived As Decimal
    Public BalanceTotalAmount As Decimal
    Public StatusId As Int32
    Public ReceiveChequeBounceAmount As Decimal
    Public BankDate As DateTime
    Public PdcPaymentTransactionId As Int32
    Public CreatedBy As String
    Public DivisionId As Int32
End Class

Public Class cls_Invoice_Settlement
    Inherits CommonClass

    Public Sub insert_Invoice_Settlement(ByVal clsObj As cls_Invoice_Settlement_prop)

        cmd = New SqlClient.SqlCommand
        cmd.Connection = con
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ProcPaymentTransactionDetail_Insert"

        cmd.Parameters.AddWithValue("@PaymentTransactionId", clsObj.PaymentTransactionId)
        cmd.Parameters.AddWithValue("@StatusId", clsObj.StatusId)
        cmd.Parameters.AddWithValue("@PaymentTransactionCode", clsObj.PaymentTransactionCode)
        cmd.Parameters.AddWithValue("@PaymentTypeId", clsObj.PaymentTypeId)
        cmd.Parameters.AddWithValue("@AccountId", clsObj.AccountId)
        cmd.Parameters.AddWithValue("@PaymentDate", clsObj.PaymentDate)
        cmd.Parameters.AddWithValue("@ChequeDraftNo", clsObj.ReferenceNo)
        cmd.Parameters.AddWithValue("@ChequeDraftDate", clsObj.ReferenceDate)
        cmd.Parameters.AddWithValue("@BankId", clsObj.BankId)
        cmd.Parameters.AddWithValue("@BankDate", clsObj.BankDate)
        cmd.Parameters.AddWithValue("@Remarks", clsObj.Remarks)
        cmd.Parameters.AddWithValue("@TotalamountReceived", clsObj.TotalAmountReceived)
        cmd.Parameters.AddWithValue("@BalanceTotalAmount", clsObj.BalanceTotalAmount)
        cmd.Parameters.AddWithValue("@CreatedBy", clsObj.CreatedBy)
        cmd.Parameters.AddWithValue("@DivisionId", clsObj.DivisionId)
        cmd.Parameters.AddWithValue("@ProcedureStatus", 1)

        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

End Class
