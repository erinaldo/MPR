Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace SemiFinished_Recipe_Master

    Public Class cls_SemiFinished_Recipe_Master_Prop
        Dim _SemifinishedRecipeid As Integer
        Dim _SemifinishedRecipeName As String
        Dim _SemiFinishedRecipeCode As String
        Dim _SemiFinishedRecipeDesc As String
        Dim _SemiFinishedRecipeuom As Integer
        Dim _Item_id As Integer
        Dim _Item_uom As Integer
        Dim _Item_qty As Double
        Dim _item_yield_qty As Double
        Dim _CreationDate As Date
        Dim _CreatedBy As String
        Dim _ModificationDate As Date
        Dim _ModifiedBy As String
        Dim _SemiFinishedItem_id As Integer
        Dim _SemiFinishedItem_qty As Double


        Public Property Item_id() As Integer
            Get
                Return _Item_id
            End Get
            Set(ByVal value As Integer)
                _Item_id = value
            End Set
        End Property

        Public Property Item_uom() As Integer
            Get
                Return _Item_uom
            End Get
            Set(ByVal value As Integer)
                _Item_uom = value
            End Set
        End Property

        Public Property Item_qty() As Double
            Get
                Return _Item_qty
            End Get
            Set(ByVal value As Double)
                _Item_qty = value
            End Set
        End Property

        Public Property item_yield_qty() As Double
            Get
                Return _item_yield_qty
            End Get
            Set(ByVal value As Double)
                _item_yield_qty = value
            End Set
        End Property

        Public Property SemifinishedRecipeid() As Integer
            Get
                Return _SemifinishedRecipeid
            End Get
            Set(ByVal value As Integer)
                _SemifinishedRecipeid = value
            End Set
        End Property

        Public Property SemifinishedRecipeName() As String
            Get
                Return _SemifinishedRecipeName
            End Get
            Set(ByVal value As String)
                _SemifinishedRecipeName = value
            End Set
        End Property

        Public Property SemiFinishedRecipeCode() As String
            Get
                Return _SemiFinishedRecipeCode
            End Get
            Set(ByVal value As String)
                _SemiFinishedRecipeCode = value
            End Set
        End Property

        Public Property SemiFinishedRecipeDesc() As String
            Get
                Return _SemiFinishedRecipeDesc
            End Get
            Set(ByVal value As String)
                _SemiFinishedRecipeDesc = value
            End Set
        End Property

        Public Property SemiFinishedRecipeuom() As Integer
            Get
                Return _SemiFinishedRecipeuom
            End Get
            Set(ByVal value As Integer)
                _SemiFinishedRecipeuom = value
            End Set
        End Property


        Public Property CreationDate() As Date
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Date)
                _CreationDate = value
            End Set
        End Property

        Public Property ModificationDate() As Date
            Get
                Return _ModificationDate
            End Get
            Set(ByVal value As Date)
                _ModificationDate = value
            End Set
        End Property

        Public Property CreatedBy() As String
            Get
                Return _CreatedBy
            End Get
            Set(ByVal value As String)
                _CreatedBy = value
            End Set
        End Property

        Public Property ModifiedBy() As String
            Get
                Return _ModifiedBy
            End Get
            Set(ByVal value As String)
                _ModifiedBy = value
            End Set
        End Property

        Public Property SemiFinishedItem_id() As Integer
            Get
                Return _SemiFinishedItem_id
            End Get
            Set(ByVal value As Integer)
                _SemiFinishedItem_id = value
            End Set
        End Property

        Public Property SemiFinishedItem_qty() As Double
            Get
                Return _SemiFinishedItem_qty
            End Get
            Set(ByVal value As Double)
                _SemiFinishedItem_qty = value
            End Set
        End Property
    End Class

    Public Class cls_SemiFinished_Recipe_Master
        Inherits CommonClass

        Public Sub insert_SemiFinished_Recipe_Master(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insert_SemifinishedRecipe_Master"

                cmd.Parameters.AddWithValue("@Semifinisheditem_id", obj.SemifinishedRecipeid)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_name", obj.SemifinishedRecipeName)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_Code", obj.SemiFinishedRecipeCode)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_uom", obj.SemiFinishedRecipeuom)
                cmd.Parameters.AddWithValue("@Creation_Date", obj.CreationDate)
                cmd.Parameters.AddWithValue("@Created_By", obj.CreatedBy)
                cmd.Parameters.AddWithValue("@Modified_By", obj.ModifiedBy)
                cmd.Parameters.AddWithValue("@Modification_Date", obj.ModificationDate)
                cmd.Parameters.AddWithValue("@Pro_Type", "I")

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub

        Public Sub insert_SemiFinished_Recipe_Detail(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insert_SemifinishedRecipe_Detail"

                cmd.Parameters.AddWithValue("@Semifinisheditem_id", obj.SemifinishedRecipeid)
                cmd.Parameters.AddWithValue("@Item_id", obj.Item_id)
                cmd.Parameters.AddWithValue("@Item_uom", obj.Item_uom)
                cmd.Parameters.AddWithValue("@item_qty", obj.Item_qty)
                cmd.Parameters.AddWithValue("@item_yield_qty", obj.item_yield_qty)
                cmd.Parameters.AddWithValue("@Creation_Date", obj.CreationDate)
                cmd.Parameters.AddWithValue("@Created_By", obj.CreatedBy)
                cmd.Parameters.AddWithValue("@Modified_By", obj.ModifiedBy)
                cmd.Parameters.AddWithValue("@Modification_Date", obj.ModificationDate)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub

        Public Sub Delete_SemiFinished_Recipe_Detail(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_delete_SemifinishedRecipe_Detail"

                cmd.Parameters.AddWithValue("@Semifinisheditem_id", obj.SemifinishedRecipeid)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub

        Public Sub Delete_SemiFinished_Recipe_Master(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_delete_SemifinishedRecipe_Master"

                cmd.Parameters.AddWithValue("@Semifinisheditem_id", obj.SemifinishedRecipeid)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub

        Public Sub Update_SemiFinished_Recipe_Master(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insert_SemifinishedRecipe_Master"

                cmd.Parameters.AddWithValue("@Semifinisheditem_id", obj.SemifinishedRecipeid)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_name", obj.SemifinishedRecipeName)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_Code", obj.SemiFinishedRecipeCode)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_uom", obj.SemiFinishedRecipeuom)
                cmd.Parameters.AddWithValue("@Creation_Date", obj.CreationDate)
                cmd.Parameters.AddWithValue("@Created_By", obj.CreatedBy)
                cmd.Parameters.AddWithValue("@Modified_By", obj.ModifiedBy)
                cmd.Parameters.AddWithValue("@Modification_Date", obj.ModificationDate)
                cmd.Parameters.AddWithValue("@Pro_Type", "U")

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub

        Public Sub insert_SemiFinishedItems_in_SemiFinishedRecipe(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insert_SemiFinishedItems_in_SemiFinishedRecipe"

                cmd.Parameters.AddWithValue("@SemifinishedRecipe_id", obj.SemifinishedRecipeid)
                cmd.Parameters.AddWithValue("@SemiFinishedItem_id", obj.SemiFinishedItem_id)
                cmd.Parameters.AddWithValue("@SemifinishedItem_qty", obj.SemiFinishedItem_qty)
                cmd.Parameters.AddWithValue("@Creation_Date", obj.CreationDate)
                cmd.Parameters.AddWithValue("@Created_By", obj.CreatedBy)
                cmd.Parameters.AddWithValue("@Modified_By", obj.ModifiedBy)
                cmd.Parameters.AddWithValue("@Modification_Date", obj.ModificationDate)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub

        Public Sub Delete_SemiFinishedItems_in_SemifinishedRecipe(ByVal obj As cls_SemiFinished_Recipe_Master_Prop)
            Dim trans As SqlTransaction

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_delete_SemiFinishedItems_in_SemifinishedRecipe"

                cmd.Parameters.AddWithValue("@SemifinishedRecipe_id", obj.SemifinishedRecipeid)

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                trans.Commit()
                con.Close()
            Catch ex As Exception
                trans.Rollback()
                con.Close()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->")
            End Try
        End Sub
    End Class
End Namespace