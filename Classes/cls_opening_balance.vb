Imports System.Data.SqlClient

Public Class cls_opening_balance_prop

    Public OpeningBalanceId As Int32
    Public AccountId As Int32
    Public OpeningDate As DateTime
    Public Amount As Decimal
    Public DivisionId As Int32
    Public CreatedBy As String
    Public Type As Int32
    Public Proctype As Int32
    Public TransactionId As Int32

End Class
Public Class cls_opening_balance
    Inherits CommonClass

    Public Sub Add_Account_OpeningBalance(ByVal clsobj As cls_opening_balance_prop)

        'Add_Account_OpeningBalanceRemote(clsobj)

        Dim trans As SqlTransaction

        If con.State = ConnectionState.Closed Then con.Open()
        trans = con.BeginTransaction

        Try
            cmd = New SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trans
            cmd.CommandText = "Proc_AddOpeningBalance"

            cmd.Parameters.AddWithValue("@OpeningBalanceId", clsobj.OpeningBalanceId)
            cmd.Parameters.AddWithValue("@AccountId", clsobj.AccountId)
            cmd.Parameters.AddWithValue("@Amount", clsobj.Amount)
            cmd.Parameters.AddWithValue("@DivisionId", clsobj.DivisionId)
            cmd.Parameters.AddWithValue("@CreatedBy", clsobj.CreatedBy)
            cmd.Parameters.AddWithValue("@OpeningDate", clsobj.OpeningDate)
            cmd.Parameters.AddWithValue("@Type", clsobj.Type)
            cmd.Parameters.AddWithValue("@Proctype", clsobj.Proctype)
            cmd.Parameters.AddWithValue("@TransactionId", clsobj.TransactionId)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            trans.Commit()
            trans.Dispose()

        Catch ex As Exception
            trans.Rollback()
            con.Close()
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Add_Account_OpeningBalanceRemote(ByVal clsobj As cls_opening_balance_prop)
        Dim con_global As New SqlConnection(gblDNS_Online)
        Dim trans_global As SqlTransaction


        If con_global.State <> ConnectionState.Open Then con_global.Open()
        trans_global = con_global.BeginTransaction
        Try
            cmd = New SqlCommand
            cmd.Connection = con_global
            cmd.Transaction = trans_global
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UPDATE dbo.ACCOUNT_MASTER SET OPENING_BAL=" & clsobj.Amount & " WHERE ACC_ID=" & clsobj.AccountId
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            trans_global.Commit()
            trans_global.Dispose()

        Catch ex As Exception
            trans_global.Rollback()
            con_global.Close()
            MsgBox(ex.Message)
        End Try

    End Sub

End Class
