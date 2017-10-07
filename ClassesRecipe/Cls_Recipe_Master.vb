Imports System
Imports System.Data
Imports System.Data.SqlClient

Namespace Recipe_Master

    Public Class cls_Recipe_Master_Prop
        Dim _Recipe_id As Integer
        Dim _Menu_id As Integer
        Dim _Item_id As Integer
        Dim _Item_Uom As Integer
        Dim _Item_Qty As Double
        Dim _Item_Yield_Qty As Double
        Dim _Creation_Date As Date
        Dim _Created_By As String
        Dim _Modification_Date As Date
        Dim _Modified_By As String
        Dim _SemiFinishedRecipe_id As Integer
        Dim _SemiFinishedRecipe_qty As Double

        Public Property Recipe_id() As Integer
            Get
                Return _Recipe_id
            End Get
            Set(ByVal value As Integer)
                _Recipe_id = value
            End Set
        End Property

        Public Property Menu_id() As Integer
            Get
                Return _Menu_id
            End Get
            Set(ByVal value As Integer)
                _Menu_id = value
            End Set
        End Property

        Public Property Item_id() As Integer
            Get
                Return _Item_id
            End Get
            Set(ByVal value As Integer)
                _Item_id = value
            End Set
        End Property

        Public Property Item_Uom() As Integer
            Get
                Return _Item_Uom
            End Get
            Set(ByVal value As Integer)
                _Item_Uom = value
            End Set
        End Property

        Public Property Item_Qty() As Double
            Get
                Return _Item_Qty
            End Get
            Set(ByVal value As Double)
                _Item_Qty = value
            End Set
        End Property

        Public Property Item_Yield_Qty() As Double
            Get
                Return _Item_Yield_Qty
            End Get
            Set(ByVal value As Double)
                _Item_Yield_Qty = value
            End Set
        End Property


        Public Property Creation_Date() As Date
            Get
                Return _Creation_Date
            End Get
            Set(ByVal value As Date)
                _Creation_Date = value
            End Set
        End Property

        Public Property Created_By() As String
            Get
                Return _Created_By
            End Get
            Set(ByVal value As String)
                _Created_By = value
            End Set
        End Property

        Public Property Modified_By() As String
            Get
                Return _Modified_By
            End Get
            Set(ByVal value As String)
                _Modified_By = value
            End Set
        End Property

        Public Property Modification_Date() As Date
            Get
                Return _Modification_Date
            End Get
            Set(ByVal value As Date)
                _Modification_Date = value
            End Set
        End Property

        Public Property SemifinishedRecipe_id() As Integer
            Get
                Return _SemiFinishedRecipe_id
            End Get
            Set(ByVal value As Integer)
                _SemiFinishedRecipe_id = value
            End Set
        End Property

        Public Property SemifinishedRecipe_qty() As Double
            Get
                Return _SemiFinishedRecipe_qty
            End Get
            Set(ByVal value As Double)
                _SemiFinishedRecipe_qty = value
            End Set
        End Property
    End Class

    Public Class Cls_Recipe_Master
        Inherits CommonClass

        Public Sub insert_Recipe_Master(ByVal obj As cls_Recipe_Master_Prop)
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
                cmd.CommandText = "Pro_insert_Recipe_Master"

                cmd.Parameters.AddWithValue("@Menu_id", obj.Menu_id)
                cmd.Parameters.AddWithValue("@Item_id", obj.Item_id)
                cmd.Parameters.AddWithValue("@Item_Uom", obj.Item_Uom)
                cmd.Parameters.AddWithValue("@Item_qty", obj.Item_Qty)
                cmd.Parameters.AddWithValue("@Item_yield_qty", obj.Item_Yield_Qty)
                cmd.Parameters.AddWithValue("@Creation_Date", obj.Creation_Date)
                cmd.Parameters.AddWithValue("@Created_By", obj.Created_By)
                cmd.Parameters.AddWithValue("@Modified_By", obj.Modified_By)
                cmd.Parameters.AddWithValue("@Modification_Date", obj.Modification_Date)

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


        Public Sub Delete_Recipe_Master(ByVal obj As cls_Recipe_Master_Prop)
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
                cmd.CommandText = "Pro_Delete_Recipe_Master"

                cmd.Parameters.AddWithValue("@Menu_id", obj.Menu_id)

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


        Public Sub insert_SemiFinishedItemsinRecipe_maping(ByVal obj As cls_Recipe_Master_Prop)
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
                cmd.CommandText = "Pro_insert_SemiFinishedItemsinRecipe_maping"

                cmd.Parameters.AddWithValue("@Menu_id", obj.Menu_id)
                cmd.Parameters.AddWithValue("@Semifinishedrecipe_id", obj.SemifinishedRecipe_id)
                cmd.Parameters.AddWithValue("@Semifinishedrecipe_qty", obj.SemifinishedRecipe_qty)
                cmd.Parameters.AddWithValue("@Creation_Date", obj.Creation_Date)
                cmd.Parameters.AddWithValue("@Created_By", obj.Created_By)
                cmd.Parameters.AddWithValue("@Modified_By", obj.Modified_By)
                cmd.Parameters.AddWithValue("@Modification_Date", obj.Modification_Date)

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


        Public Sub Delete_SemiFinisheditemsinRecipe_Mapping(ByVal obj As cls_Recipe_Master_Prop)
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
                cmd.CommandText = "Pro_Delete_SemiFinisheditemsinRecipe_Mapping"

                cmd.Parameters.AddWithValue("@Menu_id", obj.Menu_id)

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

