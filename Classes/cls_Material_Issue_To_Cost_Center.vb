Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace material_issue_to_cost_center_master

    Public Class cls_material_issue_to_cost_center_master_prop


        Dim _MIO_ID As Decimal
        Dim _MRS_ID As Decimal
        Dim _ITEM_ID As Decimal
        Dim _ACCEPTED_QTY As Decimal
        Dim _MIO_CODE As String
        Dim _MIO_NO As Int32
        Dim _MIO_DATE As DateTime
        Dim _CS_ID As Int32
        Dim _MIO_REMARKS As String
        Dim _MIO_ACCEPT_DATE As DateTime
        Dim _MIO_STATUS As Int32
        Dim _CREATED_BY As String
        Dim _CREATION_DATE As DateTime
        Dim _MODIFIED_BY As String
        Dim _MODIFIED_DATE As DateTime
        Dim _DIVISION_ID As Int32
        Dim _REQ_QTY As Int32
        Dim _ISSUED_QTY As Int32
        Dim _RETURNED_QTY As Int32
        Dim _ITEM_RATE As Int32
        Dim _BalIssued_Qty As Int32
        Dim _MaterialIssueItem As DataTable






        Public Property MIO_ID() As Decimal
            Get
                MIO_ID = _MIO_ID
            End Get
            Set(ByVal value As Decimal)
                _MIO_ID = value
            End Set
        End Property


        Public Property ACCEPTED_QTY() As Decimal
            Get
                ACCEPTED_QTY = _ACCEPTED_QTY
            End Get
            Set(ByVal value As Decimal)
                _ACCEPTED_QTY = value
            End Set
        End Property
        Public Property MRS_ID() As Decimal
            Get
                MRS_ID = _MRS_ID
            End Get
            Set(ByVal value As Decimal)
                _MRS_ID = value
            End Set
        End Property
        Public Property ITEM_ID() As Decimal
            Get
                ITEM_ID = _ITEM_ID
            End Get
            Set(ByVal value As Decimal)
                _ITEM_ID = value
            End Set
        End Property



        Public Property MIO_CODE() As String
            Get
                MIO_CODE = _MIO_CODE
            End Get
            Set(ByVal value As String)
                _MIO_CODE = value
            End Set
        End Property

        Public Property MIO_NO() As Int32
            Get
                MIO_NO = _MIO_NO
            End Get
            Set(ByVal value As Int32)
                _MIO_NO = value
            End Set
        End Property

        Public Property MIO_DATE() As DateTime
            Get
                MIO_DATE = _MIO_DATE
            End Get
            Set(ByVal value As DateTime)
                _MIO_DATE = value
            End Set
        End Property

        Public Property CS_ID() As Int32
            Get
                CS_ID = _CS_ID
            End Get
            Set(ByVal value As Int32)
                _CS_ID = value
            End Set
        End Property

        Public Property MIO_REMARKS() As String
            Get
                MIO_REMARKS = _MIO_REMARKS
            End Get
            Set(ByVal value As String)
                _MIO_REMARKS = value
            End Set
        End Property

        Public Property MIO_ACCEPT_DATE() As DateTime
            Get
                MIO_ACCEPT_DATE = _MIO_ACCEPT_DATE
            End Get
            Set(ByVal value As DateTime)
                _MIO_ACCEPT_DATE = value
            End Set
        End Property

        Public Property BalIssued_Qty() As Int32
            Get
                BalIssued_Qty = _BalIssued_Qty
            End Get
            Set(ByVal value As Int32)
                _BalIssued_Qty = value
            End Set
        End Property

        Public Property MIO_STATUS() As Int32
            Get
                MIO_STATUS = _MIO_STATUS
            End Get
            Set(ByVal value As Int32)
                _MIO_STATUS = value
            End Set
        End Property
        Public Property CREATED_BY() As String
            Get
                CREATED_BY = _CREATED_BY
            End Get
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
        End Property

        Public Property CREATION_DATE() As DateTime
            Get
                CREATION_DATE = _CREATION_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATION_DATE = value
            End Set
        End Property

        Public Property MODIFIED_BY() As String
            Get
                MODIFIED_BY = _MODIFIED_BY
            End Get
            Set(ByVal value As String)
                _MODIFIED_BY = value
            End Set
        End Property

        Public Property MODIFIED_DATE() As DateTime
            Get
                MODIFIED_DATE = _MODIFIED_DATE
            End Get
            Set(ByVal value As DateTime)
                _MODIFIED_DATE = value
            End Set
        End Property

        Public Property DIVISION_ID() As Int32
            Get
                DIVISION_ID = _DIVISION_ID
            End Get
            Set(ByVal value As Int32)
                _DIVISION_ID = value
            End Set
        End Property

        Public Property REQ_QTY() As Int32
            Get
                REQ_QTY = _REQ_QTY
            End Get
            Set(ByVal value As Int32)
                _REQ_QTY = value
            End Set
        End Property

        Public Property ISSUED_QTY() As Int32
            Get
                ISSUED_QTY = _ISSUED_QTY
            End Get
            Set(ByVal value As Int32)
                _ISSUED_QTY = value
            End Set
        End Property

        Public Property RETURNED_QTY() As Int32
            Get
                RETURNED_QTY = _RETURNED_QTY
            End Get
            Set(ByVal value As Int32)
                _RETURNED_QTY = value
            End Set
        End Property

        Public Property ITEM_RATE() As Int32
            Get
                ITEM_RATE = _ITEM_RATE
            End Get
            Set(ByVal value As Int32)
                _ITEM_RATE = value
            End Set
        End Property
        Public Property MaterialIssueItem() As DataTable
            Get
                Return _MaterialIssueItem
            End Get

            Set(ByVal value As DataTable)
                _MaterialIssueItem = value

            End Set
        End Property
    End Class

    Public Class cls_material_issue_to_cost_center_master
        Inherits CommonClass
        Public Function Insert_Material_Issue_to_CostCenter(ByVal clsObj As cls_material_issue_to_cost_center_master_prop) As String
            Dim commObj As New CommonClass()
            Dim Trans As SqlTransaction
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim MIO_CODE As String = ""
            Trans = con.BeginTransaction
            Try
                ' <summary>
                ' Steps for Saving Wastage Item
                ' 1. Insert in MATERIAL_ISSUE_TO_COST_CENTER_MASTER Table with transaction.
                ' 2. Insert in MATERIAL_ISSUE_TO_COST_CENTER_DETAIL Table with Transaction.
                ' 3. Update in Stock_Detail Table with  Transaction
                ' 4. Insert in Transaction_Log Table with  Transaction
                ' </summary>
                '   
                cmd = New SqlCommand()
                cmd.CommandTimeout = 0
                cmd.Connection = con
                cmd.Transaction = Trans
                MIO_CODE = clsObj.MIO_CODE + Convert.ToString(clsObj.MIO_NO)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PROC_MATERIAL_ISSUE_TO_COST_CENTER_MASTER"
                cmd.Parameters.AddWithValue("@V_MIO_ID", clsObj.MIO_ID)
                cmd.Parameters.AddWithValue("@V_MIO_CODE", clsObj.MIO_CODE)
                cmd.Parameters.AddWithValue("@V_MIO_NO", clsObj.MIO_NO)
                cmd.Parameters.AddWithValue("@V_MIO_DATE", clsObj.MIO_DATE)
                cmd.Parameters.AddWithValue("@V_CS_ID", clsObj.CS_ID)
                cmd.Parameters.AddWithValue("@V_MIO_REMARKS", clsObj.MIO_REMARKS)
                cmd.Parameters.AddWithValue("@V_MIO_ACCEPT_DATE", clsObj.MIO_ACCEPT_DATE)
                cmd.Parameters.AddWithValue("@V_MIO_STATUS", clsObj.MIO_STATUS)
                cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
                cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
                cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
                cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
                cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
                cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                cmd.ExecuteNonQuery()

                Dim dt As DataTable
                Dim iRow As Int32
                dt = clsObj.MaterialIssueItem
                For iRow = 0 To dt.Rows.Count - 1
                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("issue_qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PROC_MATERIAL_ISSUE_TO_COST_CENTER_DETAIL"
                        cmd.Parameters.AddWithValue("@v_MIO_ID", clsObj.MIO_ID)
                        cmd.Parameters.AddWithValue("@v_MRS_ID", clsObj.MRS_ID)
                        cmd.Parameters.AddWithValue("@v_ITEM_ID", Convert.ToInt32(dt.Rows(iRow)("ITEM_ID").ToString()))
                        cmd.Parameters.AddWithValue("@v_REQ_QTY", Convert.ToDouble(dt.Rows(iRow)("Req_QTY").ToString()))
                        cmd.Parameters.AddWithValue("@v_ISSUED_QTY", Convert.ToDouble(dt.Rows(iRow)("issue_qty").ToString()))
                        cmd.Parameters.AddWithValue("@v_ACCEPTED_QTY", Convert.ToDouble(dt.Rows(iRow)("issue_qty").ToString()))
                        cmd.Parameters.AddWithValue("@v_RETURNED_QTY", 0)
                        cmd.Parameters.AddWithValue("@v_BalIssued_Qty", Convert.ToDouble(dt.Rows(iRow)("issue_qty").ToString()))
                        cmd.Parameters.AddWithValue("@v_IS_WASTAGE", False)
                        cmd.Parameters.AddWithValue("@v_ITEM_RATE", Convert.ToDouble(dt.Rows(iRow)("item_rate").ToString()))
                        cmd.Parameters.AddWithValue("@v_STOCK_DETAIL_ID", Convert.ToDouble(dt.Rows(iRow)("Stock_Detail_Id").ToString()))
                        cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
                        cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
                        cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                        cmd.ExecuteNonQuery()
                    End If

                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("issue_qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "UPDATE_STOCK_DETAIL_ISSUE"
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", dt.Rows(iRow)("issue_qty"))
                        cmd.ExecuteNonQuery()
                    End If

                    cmd.Parameters.Clear()
                    If Convert.ToInt32(dt.Rows(iRow)("issue_qty") > 0) Then
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.MIO_ID)
                        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(iRow)("Item_id"))
                        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.MaterialIssuetoCostCenter)
                        cmd.Parameters.AddWithValue("@Quantity", dt.Rows(iRow)("issue_qty"))
                        cmd.Parameters.AddWithValue("@Transaction_Date", Now)
                        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRow)("Stock_Detail_Id"))
                        cmd.ExecuteNonQuery()
                    End If
                Next iRow

                cmd.Parameters.Clear()

                'For iRow = 0 To dt.Rows.Count - 1
                '    cmd.Parameters.Clear()
                '    If Convert.ToInt32(dt.Rows(iRow)("issue_qty") > 0) Then
                '        cmd.CommandType = CommandType.StoredProcedure
                '        cmd.CommandText = "INSERT_TRANSACTION_LOG"
                '        cmd.Parameters.AddWithValue("@Transaction_ID", clsObj.MIO_ID)
                '        cmd.Parameters.AddWithValue("@Item_ID", dt.Rows(iRow)("Item_id"))
                '        cmd.Parameters.AddWithValue("@Transaction_Type", Transaction_Type.MaterialIssuetoCostCenter)
                '        cmd.Parameters.AddWithValue("@Quantity", dt.Rows(iRow)("issue_qty"))
                '        cmd.Parameters.AddWithValue("@Transaction_Date", now)
                '        cmd.Parameters.AddWithValue("@Current_Stock", 0)
                '        cmd.Parameters.AddWithValue("@STOCK_DETAIL_ID", dt.Rows(iRow)("Stock_Detail_Id"))
                '        cmd.ExecuteNonQuery()
                '    End If
                'Next
                Trans.Commit()
                Return "Record saved successfully with code ( " + MIO_CODE + "  )"
                cmd.Dispose()
            Catch ex As Exception
                Trans.Rollback()
                Return ex.Message
            End Try
        End Function

        Public Sub insert_MATERIAL_ISSUE_TO_COST_CENTER_MASTER(ByVal clsObj As cls_material_issue_to_cost_center_master_prop, ByVal dtItems As DataTable)
            Dim intRetMISNo As Int64

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_ISSUE_TO_COST_CENTER_MASTER"
            cmd.Parameters.AddWithValue("@V_MIO_ID", clsObj.MIO_ID)
            cmd.Parameters.AddWithValue("@V_MIO_CODE", clsObj.MIO_CODE)
            cmd.Parameters.AddWithValue("@V_MIO_NO", clsObj.MIO_NO)
            cmd.Parameters.AddWithValue("@V_MIO_DATE", clsObj.MIO_DATE)
            cmd.Parameters.AddWithValue("@V_CS_ID", clsObj.CS_ID)
            cmd.Parameters.AddWithValue("@V_MIO_REMARKS", clsObj.MIO_REMARKS)
            cmd.Parameters.AddWithValue("@V_MIO_ACCEPT_DATE", clsObj.MIO_ACCEPT_DATE)
            cmd.Parameters.AddWithValue("@V_MIO_STATUS", clsObj.MIO_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)

            Dim retMISNo As New SqlParameter("@ret", SqlDbType.BigInt)
            retMISNo.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(retMISNo)

            Dim sqlTrans As SqlTransaction
            sqlTrans = con.BeginTransaction()
            Try
                cmd.Transaction = sqlTrans
                cmd.ExecuteNonQuery()
                intRetMISNo = Convert.ToInt64(cmd.Parameters("@ret").Value)
                cmd.Dispose()

                Dim intLoop As Int32
                For intLoop = 0 To dtItems.Rows.Count - 1
                    cmd = New SqlCommand()
                    cmd.Connection = con
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "PROC_MATERIAL_ISSUE_TO_COST_CENTER_DETAIL"
                    cmd.Parameters.AddWithValue("@v_MIO_ID", clsObj.MIO_ID)
                    cmd.Parameters.AddWithValue("@v_MRS_ID", Convert.ToDouble(dtItems.Rows(intLoop)("MRS_ID").ToString()))
                    cmd.Parameters.AddWithValue("@v_ITEM_ID", Convert.ToInt32(dtItems.Rows(intLoop)("ITEM_ID").ToString()))
                    cmd.Parameters.AddWithValue("@v_REQ_QTY", Convert.ToDouble(dtItems.Rows(intLoop)("ITEM_QTY").ToString()))
                    cmd.Parameters.AddWithValue("@v_ISSUED_QTY", Convert.ToDouble(dtItems.Rows(intLoop)("Issued_QTY").ToString()))
                    cmd.Parameters.AddWithValue("@v_ACCEPTED_QTY", 0)
                    cmd.Parameters.AddWithValue("@v_RETURNED_QTY", 0)
                    cmd.Parameters.AddWithValue("@v_IS_WASTAGE", False)
                    cmd.Parameters.AddWithValue("@v_ITEM_RATE", Convert.ToDouble(dtItems.Rows(intLoop)("ITEM_RATE").ToString()))
                    cmd.Parameters.AddWithValue("@V_PROC_TYPE", 1)
                    cmd.Transaction = sqlTrans
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                Next
            Catch ex As Exception
            End Try
            sqlTrans.Commit()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        End Sub

        Public Sub update_MATERIAL_ISSUE_TO_COST_CENTER_MASTER(ByVal clsObj As cls_material_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_ISSUE_TO_COST_CENTER_MASTER"
            cmd.Parameters.AddWithValue("@V_MIO_ID", clsObj.MIO_ID)
            cmd.Parameters.AddWithValue("@V_MIO_CODE", clsObj.MIO_CODE)
            cmd.Parameters.AddWithValue("@V_MIO_NO", clsObj.MIO_NO)
            cmd.Parameters.AddWithValue("@V_MIO_DATE", clsObj.MIO_DATE)
            cmd.Parameters.AddWithValue("@V_CS_ID", clsObj.CS_ID)
            cmd.Parameters.AddWithValue("@V_MIO_REMARKS", clsObj.MIO_REMARKS)
            cmd.Parameters.AddWithValue("@V_MIO_ACCEPT_DATE", clsObj.MIO_ACCEPT_DATE)
            cmd.Parameters.AddWithValue("@V_MIO_STATUS", clsObj.MIO_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        End Sub

        Public Sub UPDATE_MATERIAL_ISSUE_TO_COST_CENTER_DETAIL(ByVal clsObj As cls_material_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MIO_ISSUE_ACCEPTED"
            cmd.Parameters.AddWithValue("@MIO_ID", clsObj.MIO_ID)
            cmd.Parameters.AddWithValue("@MRS_ID", clsObj.MRS_ID)
            cmd.Parameters.AddWithValue("@ITEM_ID", clsObj.ITEM_ID)
            cmd.Parameters.AddWithValue("@ACCEPT_DATE", clsObj.ACCEPTED_QTY)
            cmd.Parameters.AddWithValue("@ACCEPTED_QTY", clsObj.ACCEPTED_QTY)
            cmd.Parameters.AddWithValue("@MODE", 2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            'con.Close()
            'con.Dispose()

        End Sub

        Public Sub delete_MATERIAL_ISSUE_TO_COST_CENTER_MASTER(ByVal clsObj As cls_material_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PROC_MATERIAL_ISSUE_TO_COST_CENTER_MASTER"

            cmd.Parameters.AddWithValue("@V_MIO_ID", clsObj.MIO_ID)
            cmd.Parameters.AddWithValue("@V_MIO_CODE", clsObj.MIO_CODE)
            cmd.Parameters.AddWithValue("@V_MIO_NO", clsObj.MIO_NO)
            cmd.Parameters.AddWithValue("@V_MIO_DATE", clsObj.MIO_DATE)
            cmd.Parameters.AddWithValue("@V_CS_ID", clsObj.CS_ID)
            cmd.Parameters.AddWithValue("@V_MIO_REMARKS", clsObj.MIO_REMARKS)
            cmd.Parameters.AddWithValue("@V_MIO_ACCEPT_DATE", clsObj.MIO_ACCEPT_DATE)
            cmd.Parameters.AddWithValue("@V_MIO_STATUS", clsObj.MIO_STATUS)
            cmd.Parameters.AddWithValue("@V_CREATED_BY", clsObj.CREATED_BY)
            cmd.Parameters.AddWithValue("@V_CREATION_DATE", clsObj.CREATION_DATE)
            cmd.Parameters.AddWithValue("@V_MODIFIED_BY", clsObj.MODIFIED_BY)
            cmd.Parameters.AddWithValue("@V_MODIFIED_DATE", clsObj.MODIFIED_DATE)
            cmd.Parameters.AddWithValue("@V_DIVISION_ID", clsObj.DIVISION_ID)
            cmd.Parameters.AddWithValue("@V_PROC_TYPE", 3)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub
        Public Sub GET_MATERIAL_ISSUE(ByVal clsObj As cls_material_issue_to_cost_center_master_prop)

            cmd = New SqlClient.SqlCommand
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_MATERIAL_ISSUE"
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            con.Dispose()

        End Sub
        '[GET_MATERIAL_ISSUE_TO_COST_CENTER_MASTER]

    End Class
End Namespace
