Imports System.Data.SqlClient

Module SyncronizeData

    Dim GlobalTables As DataSet
    Dim con As SqlConnection
    Dim cmd As SqlCommand

    Public Sub CollectData(ByVal outlet_id As Integer, Optional ByVal NEW_OUTLET As Boolean = False, Optional ByVal lblStatus As Label = Nothing, Optional ByVal pbardatatransfer As ProgressBar = Nothing)

        GlobalTables = New DataSet
        GlobalTables.Tables.Clear()
        Dim Table As DataTable
        Dim Query As String
        ''1) city_master
        Table = New DataTable()
        Query = " Select pk_CityId_num as CITY_ID,'C' as CITY_CODE,CityName_vch as CITY_NAME,'' as CITY_DESC, fk_StateId_num as STATE_ID,fk_CreatedBy_num as CREATED_BY,CreatedDate_dt as CREATION_DATE,fk_ModifiedBy_num as MODIFIED_BY,ModifiedDate_dt MODIFIED_DATE, " & outlet_id & " as DIVISION_ID from cityMaster"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "city_master"
        GlobalTables.Tables.Add(Table.Copy())

        ''2) Item_Master
        Query = " Select ITEM_ID ,ITEM_CODE ,ITEM_NAME ,ITEM_DESC, UM_ID ,ISSUE_UM_ID,RECP_UM_ID,ITEM_CATEGORY_ID,CONV_FAC_ISSUE,CONV_FAC_RECP,RE_ORDER_LEVEL,RE_ORDER_QTY,TRANSFER_RATE,SALE_RATE,PURCHASE_RATE,VAT_ID,EXCISE_ID,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,0 as division_id, IS_STOCKABLE,fk_HsnID_num from ITEM_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Item_Master"
        GlobalTables.Tables.Add(Table.Copy())

        ''3) ACCOUNT_GROUP
        Query = "Select * from ACCOUNT_GROUPS"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ACCOUNT_GROUPS"
        GlobalTables.Tables.Add(Table.Copy())

        ''4) ACCOUNT_MASTER
        Query = "Select * from ACCOUNT_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ACCOUNT_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''6) COST_CENTER_MASTER
        Query = "Select * from COST_CENTER_MASTER where Division_Id = " & outlet_id
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "COST_CENTER_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        '7) COUNTRY_MASTER
        Query = "Select * from COUNTRY_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "COUNTRY_MASTER"
        GlobalTables.Tables.Add(Table.Copy())



        ''8) DELIVERY_RATING_MASTER
        Query = "Select * from DELIVERY_RATING_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "DELIVERY_RATING_MASTER"
        GlobalTables.Tables.Add(Table.Copy())


        ''9) EXCISE_MASTER
        Query = "Select * from EXCISE_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "EXCISE_MASTER"
        GlobalTables.Tables.Add(Table.Copy())


        ''11) ITEM_CATEGORY
        Query = "Select * from ITEM_CATEGORY"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ITEM_CATEGORY"
        GlobalTables.Tables.Add(Table.Copy())

        ''11) ITEM_CATEGORY
        Query = "Select * from ITEM_CATEGORY_HEAD_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ITEM_CATEGORY_HEAD_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''15) PO_TYPE_MASTER
        Query = "Select * from PO_TYPE_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_TYPE_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''16) PurchaseType_Master
        Query = "Select * from PurchaseType_Master"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PurchaseType_Master"
        GlobalTables.Tables.Add(Table.Copy())

        ''18) QUALITY_RATING_MASTER
        Query = "Select * from QUALITY_RATING_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "QUALITY_RATING_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''19) UNIT_MASTER
        Query = "Select * from UNIT_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "UNIT_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''20) USER_MASTER
        Query = "Select * from USER_MASTER_MMS where division_id=" & outlet_id
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "USER_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''21) VAT_MASTER
        Query = "Select *  from VAT_MASTER"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "VAT_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''22) 
        Query = " Select pk_StateId_num as STATE_ID ,STATECODE_vch as STATE_CODE,StateName_vch as STATE_NAME,'' as STATE_DESC,fk_CreatedBy_num as CREATED_BY,CreatedDate_dt as CREATION_DATE,fk_ModifiedBy_num as MODIFIED_BY,ModifiedDate_dt MODIFIED_DATE,IsUT_Bit " & outlet_id & " as DIVISION_ID from StateMaster"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STATE_MASTER"
        GlobalTables.Tables.Add(Table.Copy())

        ''23)
        Query = "select * from CategoryMaster"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CategoryMaster"
        GlobalTables.Tables.Add(Table.Copy())

        ''24)
        Query = "select * from MenuMaster"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MenuMaster"
        GlobalTables.Tables.Add(Table.Copy())

        ''24)
        Query = "select * from MenuMapping"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MenuMapping"
        GlobalTables.Tables.Add(Table.Copy())

        ''24)
        Query = "select * from OutletMaster"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "OutletMaster"
        GlobalTables.Tables.Add(Table.Copy())

        ''25)
        Query = "select * from Hsncode_master"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Hsncode_master"
        GlobalTables.Tables.Add(Table.Copy())

        If NEW_OUTLET Then

            '5) CN_SERIES
            Query = "Select * from CN_SERIES where DIV_ID = " & outlet_id
            Table = Get_Remote_DataSet(Query).Tables(0)
            Table.TableName = "CN_SERIES"
            GlobalTables.Tables.Add(Table.Copy())

            '14) MRN_SERIES
            Query = "Select * from MRN_SERIES where DIV_ID = " & outlet_id
            Table = Get_Remote_DataSet(Query).Tables(0)
            Table.TableName = "MRN_SERIES"
            GlobalTables.Tables.Add(Table.Copy())


            '10) INVOICE_SERIES
            Query = "Select * from INVOICE_SERIES where DIV_ID = " & outlet_id
            Table = Get_Remote_DataSet(Query).Tables(0)
            Table.TableName = "INVOICE_SERIES"
            GlobalTables.Tables.Add(Table.Copy())


            '9) DN_SERIES
            Query = "Select * from DN_SERIES where DIV_ID = " & outlet_id
            Table = Get_Remote_DataSet(Query).Tables(0)
            Table.TableName = "DN_SERIES"
            GlobalTables.Tables.Add(Table.Copy())

            '8) DC_SERIES
            Query = "Select * from DC_SERIES where DIV_ID = " & outlet_id
            Table = Get_Remote_DataSet(Query).Tables(0)
            Table.TableName = "DC_SERIES"
            GlobalTables.Tables.Add(Table.Copy())
        End If
        Bulk_Copy(GlobalTables, lblStatus, pbardatatransfer)


    End Sub

    Public Function Get_Remote_DataSet(ByVal qry As String) As DataSet
        cmd = New SqlCommand()
        Dim ds_new As New DataSet()
        Try
            con = New SqlConnection(gblDNS_Online)
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Class Connection")
        End Try
        cmd.Connection = con
        'cmd.CommandText = "SELECT *,CityMaster.CityName_vch FROM OutletMaster INNER JOIN outletSettings ON OutletMaster.Pk_OutletId_num = outletSettings.Outlet_id INNER JOIN dbo.CityMaster ON OutletMaster.fk_CityId_num = dbo.CityMaster.pk_CityId_num WHERE outletSettings.Outlet_id = " + outlet_id.ToString()
        cmd.CommandText = qry
        cmd.CommandType = CommandType.Text
        Dim adp As New SqlDataAdapter(cmd)

        adp.Fill(ds_new)

        Return ds_new
    End Function

    Public Sub Bulk_Copy(ByVal _Table As DataSet, Optional ByVal lblStatus As Label = Nothing, Optional ByVal pbardatatransfer As ProgressBar = Nothing)

        Dim Table1 As DataTable = Nothing

        con = New SqlConnection(DNS)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()

        Dim tran As SqlTransaction
        tran = con.BeginTransaction
        Try
            ''********************************************************************''
            ''Synchronize local tables with global tables
            ''********************************************************************''
            'CollectData() ''collect data from global tables

            Dim cmd As SqlCommand

            'Using _TransactionScope As New Transactions.TransactionScope()
            ''we can open only one connection with the single server under TransactionScope object.
            ''So create a common command object to execute delete queries with the same connection
            ''object which is used by Sql Bulk Copy object.

            ''delete data from local tables
            ''DeleteData(CommandObject)

            ''Synchronize data




            cmd = New SqlCommand("delete from ACCOUNT_GROUPS")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ACCOUNT_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from CITY_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from CN_SERIES")
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from COST_CENTER_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from COUNTRY_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from DC_SERIES")
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from DELIVERY_RATING_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from DIVISION_SETTINGS")
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from DN_SERIES")
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from EXCISE_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from INVOICE_SERIES")
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ITEM_CATEGORY")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ITEM_CATEGORY_HEAD_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from ITEM_DETAIL")
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ITEM_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from MRN_SERIES")
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from PO_TYPE_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from PurchaseType_Master")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from QUALITY_RATING_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from UNIT_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from USER_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from VAT_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from STATE_MASTER")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from CategoryMaster")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MenuMaster")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MenuMapping")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from OutletMaster")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from HsnCode_Master")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            Dim BulkCopy As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)

            'AddHandler BulkCopy.SqlRowsCopied, AddressOf BulkCopy_SqlRowsCopied
            'BulkCopy.NotifyAfter = 100
            If Not IsNothing(pbardatatransfer) Then
                pbardatatransfer.Value = 0
                pbardatatransfer.Minimum = 0
                pbardatatransfer.Maximum = _Table.Tables.Count

            End If


            For Each Table1 In _Table.Tables
                BulkCopy.DestinationTableName = Table1.TableName
                BulkCopy.WriteToServer(Table1)
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text += Environment.NewLine & "Transferring Table " & _
                    Table1.TableName & "....."
                    lblStatus.Text += "Done"
                    pbardatatransfer.Value += 1
                    'System.Threading.Thread.Sleep(1000) 
                    System.Windows.Forms.Application.DoEvents()
                End If
            Next
            If Not IsNothing(pbardatatransfer) Then
                pbardatatransfer.Value = pbardatatransfer.Maximum
            End If

            BulkCopy.Close()
            tran.Commit()
            con.Close()

            MsgBox("Database updated successfully.")

            ''********************************************************************''

            ''********************************************************************''  


        Catch ex As Exception
            tran.Rollback()
            MsgBox(ex.Message & vbCrLf & Table1.TableName)
        End Try
    End Sub
End Module
