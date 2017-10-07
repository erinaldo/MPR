Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace SemiFinishedItem

    '**** Comment by Yogesh Chandra Upreti 
    Public Class Cls_Semi_Finished_Item_Master_prop
        Private _item_id As Int16 = 0
        Private _item_code As [String]
        Private _item_name As [String]
        Private _SemItem_Id As Int16
        Private _Semiitem_name As [String]
        Private _ingredientid As Int16
        Private _qty As [Decimal]
        Private _quantity As [String]
        Private _yieldQty As [Decimal]
        Private _uomid As Int16
        Private _UOM As [String]
        Private _createdby As [String]
        Private _cost As [String]
        Private _majorcomponent As Int16
        Private _recipe_Id As Int16
        Private _Category_Id As Int16
        Private _Menu_Item_Id As Int16
        Private _Semi_Finished_Item_Id As Integer
        Private _MenuItem_desc As [String]
        Private _creation_date As Date
        Private _modified_by As String
        Private _modified_date As Date

        Public Property creation_date() As Date
            Get
                Return _creation_date
            End Get
            Set(ByVal value As Date)
                _creation_date = value
            End Set
        End Property

        Public Property modified_date() As Date
            Get
                Return _modified_date
            End Get
            Set(ByVal value As Date)
                _modified_date = value
            End Set
        End Property

        Public Property modified_by() As String
            Get
                Return _modified_by
            End Get
            Set(ByVal value As String)
                _modified_by = value
            End Set
        End Property

        Public Property Menu_Item_Id() As [Int16]
            Get
                Return _Menu_Item_Id
            End Get
            Set(ByVal value As [Int16])
                _Menu_Item_Id = value
            End Set
        End Property
        Public Property Semi_Finished_Item_Id() As Integer
            Get
                Return _Semi_Finished_Item_Id
            End Get
            Set(ByVal value As Integer)
                _Semi_Finished_Item_Id = value
            End Set
        End Property

        Public Property Menuitem_desc() As String
            Get
                Return _MenuItem_desc
            End Get
            Set(ByVal value As String)
                _MenuItem_desc = value
            End Set
        End Property

        Public Property Category_Id() As [Int16]
            Get
                Return _Category_Id
            End Get
            Set(ByVal value As [Int16])
                _Category_Id = value
            End Set
        End Property
        Public Property Item_Id() As Int16
            Get
                Return _item_id
            End Get
            Set(ByVal value As Int16)
                _item_id = value
            End Set
        End Property
        Public Property Item_Code() As [String]
            Get
                Return _item_code
            End Get
            Set(ByVal value As [String])
                _item_code = value
            End Set
        End Property
        Public Property Semi_Item_Qty() As [String]
            Get
                Return _quantity
            End Get
            Set(ByVal value As [String])
                _quantity = value
            End Set
        End Property
        Public Property Item_Name() As [String]
            Get
                Return _item_name
            End Get
            Set(ByVal value As [String])
                _item_name = value
            End Set
        End Property
        Public Property SemItem_Id() As Int16
            Get
                Return _SemItem_Id
            End Get
            Set(ByVal value As Int16)
                _SemItem_Id = value
            End Set
        End Property
        Public Property SemiItem_Name() As [String]
            Get
                Return _Semiitem_name
            End Get
            Set(ByVal value As [String])
                _Semiitem_name = value
            End Set
        End Property
        Public Property ingredientid() As Int16
            Get
                Return _ingredientid
            End Get
            Set(ByVal value As Int16)
                _ingredientid = value
            End Set
        End Property
        Public Property qty() As [Decimal]
            Get
                Return _qty
            End Get
            Set(ByVal value As [Decimal])
                _qty = value
            End Set
        End Property
        Public Property yieldQty() As [Decimal]
            Get
                Return _yieldQty
            End Get
            Set(ByVal value As [Decimal])
                _yieldQty = value
            End Set
        End Property
        Public Property uomid() As Int16
            Get
                Return _uomid
            End Get
            Set(ByVal value As Int16)
                _uomid = value
            End Set
        End Property
        Public Property UOM() As [String]
            Get
                Return _UOM
            End Get
            Set(ByVal value As [String])
                _UOM = value
            End Set
        End Property
        Public Property cost() As [String]
            Get
                Return _cost
            End Get
            Set(ByVal value As [String])
                _cost = value
            End Set
        End Property
        Public Property majorcomponent() As Int16
            Get
                Return _majorcomponent
            End Get
            Set(ByVal value As Int16)
                _majorcomponent = value
            End Set
        End Property
        Public Property createdby() As [String]
            Get
                Return _createdby
            End Get
            Set(ByVal value As [String])
                _createdby = value
            End Set
        End Property
        Public Property Recipe_Id() As Int16
            Get
                Return _recipe_Id
            End Get
            Set(ByVal value As Int16)
                _recipe_Id = value
            End Set
        End Property
    End Class
  

    Public Class Cls_Semi_Finished_Item_Master
        Inherits CommonClass

        '    '################################################################
        '    'Manipulating SemiFinished Items VALUES TO DATABASE
        '    '################################################################

        Public Sub insert_SemiFinishedItem_DETAIL(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Dim trans As SqlTransaction
            '    '################################
            '    'INSERT VALUES TO DATABASE
            '    '################################
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_Insert_SemiFinished_Items"

                cmd.Parameters.AddWithValue("@ITEMCode", clsObj.Item_Code)
                cmd.Parameters.AddWithValue("@ItemName", clsObj.Item_Name)
                cmd.Parameters.AddWithValue("@unit", clsObj.uomid)

                If con.State = ConnectionState.Closed Then con.Open()
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

        Public Sub update_SemiFinishedItem_DETAIL(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Try
                '    '################################
                '    'UPDATE DATABASE
                '    '################################

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_Insert_SemiFinished_Items"

                cmd.Parameters.AddWithValue("@ITEMCode", clsObj.Item_Code)
                cmd.Parameters.AddWithValue("@ItemName", clsObj.Item_Name)
                cmd.Parameters.AddWithValue("@unit", clsObj.uomid)

                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Updating Semifinished Items")
            End Try
        End Sub

        Public Sub delete_SemiFinishedItem_DETAIL(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Try
                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_ITEM_DETAIL"

                cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.Item_Id)

                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Delete_rec clsItemDetail")
            End Try
        End Sub

        Public Sub get_SemiFinished_ITEM_MASTER(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_ITEM_DETAIL"

            cmd.Parameters.AddWithValue("@ITEM_ID", clsObj.Item_Id)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        End Sub
        Public Sub Bind_UOM()

        End Sub
        '    '################################################################
        '    'Manipulating SemiFinished Item Recipe VALUES TO DATABASE
        '    '################################################################
        Public Sub insert_SemiFinishedItemRecipe(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Dim trans As SqlTransaction
            '    '################################################################
            '    'INSERT SemiFinished Item Recipe VALUES TO DATABASE
            '    '################################################################
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Try

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_GetRecipeInsertSemiFinishedItemRecipe"

                cmd.Parameters.AddWithValue("@menuid", clsObj.SemItem_Id)
                cmd.Parameters.AddWithValue("@ingredientid", clsObj.ingredientid)
                cmd.Parameters.AddWithValue("@Qty", clsObj.qty)
                cmd.Parameters.AddWithValue("@yieldQty", clsObj.yieldQty)
                cmd.Parameters.AddWithValue("@uomid", clsObj.uomid)
                cmd.Parameters.AddWithValue("@majorcomponent", "1")
                cmd.Parameters.AddWithValue("@createdby", "Semi Finished Recipe Items")

                If con.State = ConnectionState.Closed Then con.Open()
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

        Public Sub update_SemiFinishedItemRecipe(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Try
                '    '################################
                '    'UPDATE DATABASE
                '    '################################

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_GetRecipeUpdateSemiFinishedItem"

                cmd.Parameters.AddWithValue("@recipeid", clsObj.SemItem_Id)
                cmd.Parameters.AddWithValue("@ingredientid", clsObj.ingredientid)
                cmd.Parameters.AddWithValue("@qty", clsObj.qty)
                cmd.Parameters.AddWithValue("@uomid", clsObj.uomid)
                cmd.Parameters.AddWithValue("@yieldqty", clsObj.yieldQty)

                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->in Updating Items Recipe ")
            End Try
        End Sub

        'Public Sub Get_RecipeItemDetails(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
        '    Try
        '        '    '################################
        '        '    'Getting Recipe Items Details 
        '        '    '################################

        '        cmd = New SqlClient.SqlCommand
        '        cmd.Connection = con
        '        cmd.CommandType = CommandType.StoredProcedure
        '        cmd.CommandText = "Pro_Update_SemiFinished_Item_Recepie"

        '        cmd.Parameters.AddWithValue("@V_ITEM_ID", clsObj.Item_Id)

        '        If con.State = ConnectionState.Closed Then con.Open()
        '        cmd.ExecuteNonQuery()
        '        cmd.Dispose()

        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Update Recipe")
        '    End Try
        'End Sub

        Public Sub Bind_Grid_Val_With_Para(ByVal DGV As DataGridView, ByVal sp As String, ByVal para As String, ByVal parm_val As String)
            Try
                Dim ds1 As DataSet
                ds1 = fill_Data_set(sp, para, parm_val)
                DGV.DataSource = ds1.Tables(0)

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error GridBind")
            Finally
                'cmd.Dispose()
            End Try

        End Sub
        Public Sub ComboBindWithSP_Para(ByVal cnt As ComboBox, ByVal sp As String, ByVal value As String, ByVal text As String, Optional ByVal use_select As Boolean = False)
            '' Common Function to Bind a Combo Box using store Proceduer 

            Try
                Dim ds1 As DataSet
                ds1 = fill_Data_set(sp, "@pro_type", 1)
                cnt.ValueMember = value
                cnt.DisplayMember = text
                Dim dr As DataRow
                If ds1.Tables(0).Rows.Count > 0 Then
                    If use_select Then
                        dr = ds1.Tables(0).NewRow
                        dr(value) = -1
                        dr(text) = "-- Select --"
                        ds1.Tables(0).Rows.InsertAt(dr, 0)
                    End If
                Else
                    dr = ds1.Tables(0).NewRow
                    dr(value) = 0
                    dr(text) = "--No Data Found--"
                    ds1.Tables(0).Rows.Add(dr)
                End If
                cnt.DataSource = ds1.Tables(0)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
            End Try
        End Sub

        Public Sub ComboBindWithSP_Parameter(ByVal cnt As ComboBox, ByVal sp As String, ByVal value As String, ByVal text As String, Optional ByVal use_select As Boolean = False)
            '' Common Function to Bind a Combo Box using store Proceduer 

            Try
                Dim ds1 As DataSet
                ds1 = fill_Data_set(sp, "@menuheadid", 0)
                cnt.ValueMember = value
                cnt.DisplayMember = text
                Dim dr As DataRow
                If ds1.Tables(0).Rows.Count > 0 Then
                    If use_select Then
                        dr = ds1.Tables(0).NewRow
                        dr(value) = -1
                        dr(text) = "-- Select --"
                        ds1.Tables(0).Rows.InsertAt(dr, 0)
                    End If
                Else
                    dr = ds1.Tables(0).NewRow
                    dr(value) = 0
                    dr(text) = "--No Data Found--"
                    ds1.Tables(0).Rows.Add(dr)
                End If
                cnt.DataSource = ds1.Tables(0)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
            End Try
        End Sub


        Public Sub ComboBinding(ByVal cnt As System.Windows.Forms.ComboBox, ByVal qry As String, ByVal text As String, ByVal value As String, Optional ByVal use_select As Boolean = False)
            '' Common Function to Bind a Combo Box
            Try
                Dim ds1 As DataSet
                ds1 = FillDataSet(qry)
                cnt.ValueMember = value
                cnt.DisplayMember = text
                Dim dr As DataRow
                If ds1.Tables(0).Rows.Count > 0 Then
                    cnt.DataSource = ds1.Tables(0)
                Else
                    dr = ds1.Tables(0).NewRow
                    dr(value) = 0
                    dr(text) = "--No Data Found--"
                    ds1.Tables(0).Rows.Add(dr)
                    cnt.DataSource = ds1.Tables(0)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ComboBind")
            End Try
        End Sub

        '    '################################################################
        '    'Manipulating SemiFinished Item Recipe VALUES TO DATABASE
        '    '################################################################
        Public Sub insert_RecipeDetails(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Dim trans As SqlTransaction
            '    '################################################################
            '    'INSERT & Delete Recipe Ingredients
            '    '################################################################
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Try

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_GetRecipeInsertionDeletion"

                cmd.Parameters.AddWithValue("@menuid", clsObj.Menu_Item_Id)
                cmd.Parameters.AddWithValue("@ingredientid", clsObj.ingredientid)
                cmd.Parameters.AddWithValue("@qty", clsObj.qty)
                cmd.Parameters.AddWithValue("@yieldQty", clsObj.yieldQty)
                cmd.Parameters.AddWithValue("@uomid", clsObj.uomid)
                cmd.Parameters.AddWithValue("@cost", 0)
                cmd.Parameters.AddWithValue("@majorcomponent", "1")
                cmd.Parameters.AddWithValue("@createdby", "ADMIN")
                cmd.Parameters.AddWithValue("@flag", "S")
                cmd.Parameters.AddWithValue("@recipeid", 0)

                If con.State = ConnectionState.Closed Then con.Open()
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
        Public Function fill_SemiFinishedItems(ByVal sp As String, ByVal p1 As String, ByVal p2 As String, ByVal v1 As String, ByVal v2 As String) As DataSet
            Try
                Dim adp As New SqlDataAdapter(sp, con)
                adp.SelectCommand.CommandType = CommandType.StoredProcedure
                adp.SelectCommand.CommandTimeout = 0
                If Not String.IsNullOrEmpty(p1.ToString()) Then
                    adp.SelectCommand.Parameters.AddWithValue(p1, v1)
                End If
                If Not String.IsNullOrEmpty(p2.ToString()) Then
                    adp.SelectCommand.Parameters.AddWithValue(p2, v2)
                End If
                Dim ds As New DataSet()
                adp.Fill(ds)
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> Fill DataSet ")
                Return ds
            Finally
                'ds.Dispose()
            End Try
        End Function

        Public Function fill_Menu_Items(ByVal sp As String, ByVal p1 As String, ByVal p2 As String, ByVal p3 As String, ByVal v1 As String, ByVal v2 As String, ByVal v3 As String) As DataSet
            Try
                Dim adp As New SqlDataAdapter(sp, con)
                adp.SelectCommand.CommandType = CommandType.StoredProcedure
                adp.SelectCommand.CommandTimeout = 0
                If Not String.IsNullOrEmpty(p1.ToString()) Then
                    adp.SelectCommand.Parameters.AddWithValue(p1, v1)
                End If
                If Not String.IsNullOrEmpty(p2.ToString()) Then
                    adp.SelectCommand.Parameters.AddWithValue(p2, v2)
                End If
                If Not String.IsNullOrEmpty(p2.ToString()) Then
                    adp.SelectCommand.Parameters.AddWithValue(p3, v3)
                End If
                Dim ds As New DataSet()
                adp.Fill(ds)
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> Fill DataSet ")
                Return ds
            Finally
                'ds.Dispose()
            End Try
        End Function

        Public Sub insert_menuitem_Recipe(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Dim trans As SqlTransaction
            '    '################################################################
            '    'INSERT & Delete Recipe Ingredients
            '    '################################################################
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Try

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insertmenuitem"

                cmd.Parameters.AddWithValue("@menuitem_id", clsObj.Menu_Item_Id)
                cmd.Parameters.AddWithValue("@menuitem_name", clsObj.Item_Name)
                cmd.Parameters.AddWithValue("@menuitem_catid", clsObj.Category_Id)
                cmd.Parameters.AddWithValue("@menuitem_desc", clsObj.Menuitem_desc)
                cmd.Parameters.AddWithValue("@created_by", clsObj.createdby)
                cmd.Parameters.AddWithValue("@creation_date", clsObj.creation_date)
                cmd.Parameters.AddWithValue("@modified_by", clsObj.modified_by)
                cmd.Parameters.AddWithValue("@modified_date", clsObj.modified_date)
                cmd.Parameters.AddWithValue("@flag", "S")


                If con.State = ConnectionState.Closed Then con.Open()
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

        Public Sub update_menuitem_Recipe(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Dim trans As SqlTransaction
            '    '################################################################
            '    'INSERT & Delete Recipe Ingredients
            '    '################################################################
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Try

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insertmenuitem"

                cmd.Parameters.AddWithValue("@menuitem_id", clsObj.Menu_Item_Id)
                cmd.Parameters.AddWithValue("@menuitem_name", clsObj.Item_Name)
                cmd.Parameters.AddWithValue("@menuitem_catid", clsObj.Category_Id)
                cmd.Parameters.AddWithValue("@menuitem_desc", clsObj.Menuitem_desc)
                cmd.Parameters.AddWithValue("@created_by", clsObj.createdby)
                cmd.Parameters.AddWithValue("@creation_date", clsObj.creation_date)
                cmd.Parameters.AddWithValue("@modified_by", clsObj.modified_by)
                cmd.Parameters.AddWithValue("@modified_date", clsObj.modified_date)
                cmd.Parameters.AddWithValue("@flag", "U")


                If con.State = ConnectionState.Closed Then con.Open()
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

        Public Sub Delete_menuitem_Recipe(ByVal clsObj As Cls_Semi_Finished_Item_Master_prop)
            Dim trans As SqlTransaction
            '    '################################################################
            '    'INSERT & Delete Recipe Ingredients
            '    '################################################################
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Try

                cmd = New SqlClient.SqlCommand
                cmd.Connection = con
                cmd.Transaction = trans

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Pro_insertmenuitem"

                cmd.Parameters.AddWithValue("@menuitem_id", clsObj.Menu_Item_Id)
                cmd.Parameters.AddWithValue("@menuitem_name", clsObj.Item_Name)
                cmd.Parameters.AddWithValue("@menuitem_catid", clsObj.Category_Id)
                cmd.Parameters.AddWithValue("@menuitem_desc", clsObj.Menuitem_desc)
                cmd.Parameters.AddWithValue("@created_by", clsObj.createdby)
                cmd.Parameters.AddWithValue("@creation_date", clsObj.creation_date)
                cmd.Parameters.AddWithValue("@modified_by", clsObj.modified_by)
                cmd.Parameters.AddWithValue("@modified_date", clsObj.modified_date)
                cmd.Parameters.AddWithValue("@flag", "D")


                If con.State = ConnectionState.Closed Then con.Open()
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