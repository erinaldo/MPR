Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DivisionSettings
    Implements IForm
    Dim obj As New CommonClass
    Dim con As SqlConnection

    'con = New SqlConnection()
    Dim cmd As SqlCommand

    Dim clsobj_prop As New Division_Settings.cls_Division_Settings_Prop
    Dim clsDivision_Obj As New Division_Settings.cls_Division_Settings

    Dim GlobalTables As DataSet
    Dim new_mode As Boolean

    Dim _rights As Form_Rights
    Public Sub New(ByVal mode As Boolean, ByVal rights As Form_Rights)
        new_mode = mode
        _rights = rights
        InitializeComponent()
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub frm_DivisionSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        

        ''********************************************************************''
        ''Check whether the settings are already present. 
        ''If yes then show the local settings and disable the Dropdown
        '' Else get the data from AFBL_CentralizePos
        ''********************************************************************''

        Dim ds As DataSet
        ds = obj.Fill_DataSet("Select dbo.DIVISION_SETTINGS.*,dbo.CITY_MASTER.CITY_NAME from DIVISION_SETTINGS INNER JOIN dbo.CITY_MASTER ON dbo.DIVISION_SETTINGS.CITY_ID = dbo.CITY_MASTER.CITY_ID")
        If (ds.Tables.Count > 0) Then
            If (ds.Tables(0).Rows.Count > 0) Then
                Dim dr As DataRow
                dr = ds.Tables(0).Rows(0)
                GroupBox1.Visible = False
                btn_Save.Visible = False
                lbl_Caption.Visible = True

                btn_Save.Text = "Update"
                lblOutletId.Text = dr("DIV_ID").ToString()
                lbl_OutletName.Text = dr("DIVISION_NAME").ToString()
                lbl_OutletAddress.Text = dr("DIVISION_ADDRESS").ToString()
                lbl_OutletTinNo.Text = dr("TIN_NO").ToString()
                lbl_PhoneNo.Text = dr("PHONE1").ToString()
                lbl_ZipCode.Text = dr("ZIP_CODE").ToString()
                lbl_mailAddress.Text = dr("MAIL_ADD").ToString()
                lbl_City.Text = dr("CITY_NAME").ToString()
                lbl_DivPrefix.Text = dr("COST_CENTER_PREFIX").ToString()
                lbl_IndentPrefix.Text = dr("INDENT_PREFIX").ToString()
                lbl_WastagePrefix.Text = dr("WASTAGE_PREFIX").ToString()
                lbl_RevWastagePrefix.Text = dr("REV_WASTAGE_PREFIX").ToString()
                lbl_ReceiptPrefix.Text = dr("RECEIPT_PREFIX").ToString()
                lbl_MioPrefix.Text = dr("MIO_PREFIX").ToString()
                lbl_RevMioPrefix.Text = dr("RMIO_PREFIX").ToString()
                lbl_MRSPrefix.Text = dr("MRSMainStorePREFIX").ToString()
                lbl_RevMRNPrefix.Text = dr("RMRN_PREFIX").ToString()
                lbl_RevPOMRNPrefix.Text = dr("RPMRN_PREFIX").ToString()
            Else
                
                cmd = New SqlCommand()
                ddl_outlets.Enabled = True

                GroupBox1.Visible = True
                btn_Save.Visible = True

                Try
                    If con Is Nothing Then
                        con = New SqlConnection(gblDNS_Online)
                    End If
                    If con.State = ConnectionState.Open Then con.Close()
                    con.Open()

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Class Connection")
                End Try
                cmd.Connection = con
                cmd.CommandText = "Select 0 as Pk_OutletId_num,'---Select---' as OutletName_vch union all Select Pk_DivisionId_num,DivisionName_vch from DivisionMaster where Is_MMS_Used=0"
                cmd.CommandType = CommandType.Text
                Dim adp As New SqlDataAdapter(cmd)
                Dim ds_new As New DataSet()
                adp.Fill(ds_new)
                ddl_outlets.DataSource = ds_new.Tables(0)
                ddl_outlets.DisplayMember = "OutletName_vch"
                ddl_outlets.ValueMember = "Pk_OutletId_num"
                'ddl_outlets.Items.Insert(0, "--Select--")
                ddl_outlets.SelectedIndex = 0
            End If
        
        End If

        AddHandler ddl_outlets.SelectedIndexChanged, AddressOf ddl_outlets_SelectedIndexChanged
    End Sub

    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        If new_mode Then
            Application.Exit()
        Else
            Me.Close()
        End If
    End Sub

    Public Sub Clear_All()

        lbl_OutletName.Text = String.Empty
        lbl_OutletAddress.Text = String.Empty
        lbl_OutletTinNo.Text = String.Empty
        lbl_PhoneNo.Text = String.Empty
        lbl_ZipCode.Text = String.Empty
        lbl_mailAddress.Text = String.Empty
        lbl_City.Text = String.Empty
        lbl_DivPrefix.Text = String.Empty
        lbl_IndentPrefix.Text = String.Empty
        lbl_WastagePrefix.Text = String.Empty
        lbl_RevWastagePrefix.Text = String.Empty
        lbl_ReceiptPrefix.Text = String.Empty
        lbl_MioPrefix.Text = String.Empty
        lbl_RevMioPrefix.Text = String.Empty
        lbl_MRSPrefix.Text = String.Empty
        lbl_RevMRNPrefix.Text = String.Empty
        lbl_RevPOMRNPrefix.Text = String.Empty
    End Sub

    Private Sub ddl_outlets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim outlet_id As Int16
        outlet_id = Convert.ToInt16(ddl_outlets.SelectedValue)

        ddl_outlets.Enabled = True
        Dim ds_new As DataSet
        Dim Query As String
        Query = "SELECT * FROM divisionmaster,CityMaster WHERE CityMaster.pk_CityId_num= divisionmaster.fk_CityId_num AND pk_divisionid_num= " & outlet_id.ToString()
        ds_new = Get_Remote_DataSet(Query)
        Dim dr As DataRow
        If (ds_new.Tables.Count > 0) Then
            If (ds_new.Tables(0).Rows.Count > 0) Then
                dr = ds_new.Tables(0).Rows(0)
                lbl_OutletName.Text = dr("DivisionName_vch").ToString()
                lbl_OutletAddress.Text = dr("Address_vch").ToString()
                lbl_OutletTinNo.Text = dr("TinNo_vch").ToString()
                lbl_PhoneNo.Text = dr("Phone1_vch").ToString()
                lbl_PhoneNo_2.Text = dr("Phone2_vch").ToString()
                lbl_ZipCode.Text = "" 'dr("Zip_Code").ToString()
                lbl_mailAddress.Text = dr("Address_vch").ToString()
                lbl_City.Text = dr("CityName_vch").ToString()
                lbl_cityID.Text = dr("fk_CityId_num").ToString()

                lbl_DivPrefix.Text = dr("Division_prefix").ToString()
                lbl_IndentPrefix.Text = dr("INDENT_PREFIX").ToString()
                lbl_WastagePrefix.Text = dr("WASTAGE_PREFIX").ToString()
                lbl_RevWastagePrefix.Text = dr("REV_WASTAGE_PREFIX").ToString()
                lbl_ReceiptPrefix.Text = dr("RECEIPT_PREFIX").ToString()
                lbl_MioPrefix.Text = dr("MIO_PREFIX").ToString()
                lbl_RevMioPrefix.Text = dr("RMIO_PREFIX").ToString()
                lbl_MRSPrefix.Text = dr("MRSMainStorePREFIX").ToString()
                lbl_RevMRNPrefix.Text = dr("RMRN_PREFIX").ToString()
                lbl_RevPOMRNPrefix.Text = dr("RPMRN_PREFIX").ToString()


                lbl_Adjustment_prefix.Text = dr("ADJUSTMENT_PREFIX").ToString()
                lbl_Transfer_prefix.Text = dr("TRANSFER_PREFIX").ToString()
                lbl_Closing_prefix.Text = dr("CLOSING_PREFIX").ToString()
                lbl_wastage_prefix_CC.Text = dr("WASTAGE_PREFIX_CC").ToString()
                lbl_Revwastage_prefix_CC.Text = dr("REV_WASTAGE_PREFIX_CC").ToString()
                lbl_Bank_name.Text = dr("BANK_NAME").ToString()
                lbl_account_no.Text = dr("ACCOUNT_NO").ToString()
                lbl_Branch_Address.Text = dr("BRANCH_ADDRESS").ToString()
                lbl_Ifsc_Code.Text = dr("IFSC_CODE").ToString()
                lbl_auth_signatory.Text = dr("AUTH_SIGNATORY").ToString()
                lbl_Company_Id.Text = dr("fk_CompanyId_num").ToString()


            Else
                Clear_All()
            End If
        End If
    End Sub

    Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        If new_mode Then
            clsobj_prop.DIV_ID = Convert.ToInt16(ddl_outlets.SelectedValue)
            clsobj_prop.DIVISION_NAME = lbl_OutletName.Text
            clsobj_prop.DIVISION_ADDRESS = lbl_OutletAddress.Text
            clsobj_prop.TIN_NO = lbl_OutletTinNo.Text
            clsobj_prop.PHONE1 = lbl_PhoneNo.Text
            clsobj_prop.PHONE2 = lbl_PhoneNo_2.Text
            clsobj_prop.ZIP_CODE = lbl_ZipCode.Text
            clsobj_prop.MAIL_ADD = lbl_mailAddress.Text
            clsobj_prop.CITY_ID = Convert.ToInt32(lbl_cityID.Text)
            clsobj_prop.COST_CENTER_PREFIX = lbl_DivPrefix.Text
            clsobj_prop.INDENT_PREFIX = lbl_IndentPrefix.Text
            clsobj_prop.WASTAGE_PREFIX = lbl_WastagePrefix.Text
            clsobj_prop.REV_WASTAGE_PREFIX = lbl_RevWastagePrefix.Text
            clsobj_prop.RECEIPT_PREFIX = lbl_ReceiptPrefix.Text
            clsobj_prop.MIO_PREFIX = lbl_MioPrefix.Text
            clsobj_prop.RMIO_PREFIX = lbl_RevMioPrefix.Text
            clsobj_prop.MRSMainStorePREFIX = lbl_MRSPrefix.Text
            clsobj_prop.RMRN_PREFIX = lbl_RevMRNPrefix.Text
            clsobj_prop.RPMRN_PREFIX = lbl_RevPOMRNPrefix.Text
            clsobj_prop.ADJUSTMENT_PREFIX = lbl_Adjustment_prefix.Text
            clsobj_prop.TRANSFER_PREFIX = lbl_Transfer_prefix.Text
            clsobj_prop.CLOSING_PREFIX = lbl_Closing_prefix.Text
            clsobj_prop.WASTAGE_PREFIX_CC = lbl_wastage_prefix_CC.Text
            clsobj_prop.REV_WASTAGE_PREFIX_CC = lbl_Revwastage_prefix_CC.Text
            clsobj_prop.BANK_NAME = lbl_Bank_name.Text
            clsobj_prop.ACCOUNT_NO = lbl_account_no.Text
            clsobj_prop.BRANCH_ADDRESS = lbl_Branch_Address.Text
            clsobj_prop.IFSC_CODE = lbl_Ifsc_Code.Text
            clsobj_prop.AUTH_SIGNATORY = lbl_auth_signatory.Text
            clsobj_prop.fk_CompanyId_num = Convert.ToInt32(lbl_Company_Id.Text)
            clsobj_prop.TYPE = 1

            lbl_msg.Text = clsDivision_Obj.INSERT_DIVISION_SETTINGS(clsobj_prop)

            CollectData(ddl_outlets.SelectedValue, True)
            Application.Restart()

        Else
            'Dim dt As DataTable

            'obj.ExecuteNonQuery("delete from DIVISION_SETTINGS")


            'dt = obj.Fill_DataSet("SELECT *,CityMaster.CityName_vch FROM OutletMaster INNER JOIN outletSettings ON OutletMaster.Pk_OutletId_num = outletSettings.Outlet_id INNER JOIN dbo.CityMaster ON OutletMaster.fk_CityId_num = dbo.CityMaster.pk_CityId_num WHERE outletSettings.Outlet_id = " & lblOutletId.Text).Tables(0)


        End If


    End Sub



    'Private Sub CollectData()

    '    GlobalTables = New DataSet
    '    GlobalTables.Tables.Clear()
    '    Dim Table As DataTable
    '    Dim Query As String
    '    ''1) city_master
    '    Table = New DataTable()
    '    Query = " Select pk_CityId_num as CITY_ID,'C' as CITY_CODE,CityName_vch as CITY_NAME,'' as CITY_DESC, fk_StateId_num as STATE_ID,fk_CreatedBy_num as CREATED_BY,CreatedDate_dt as CREATION_DATE,fk_ModifiedBy_num as MODIFIED_BY,ModifiedDate_dt MODIFIED_DATE, " & ddl_outlets.SelectedValue & " as DIVISION_ID from cityMaster"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "city_master"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''2) Item_Master
    '    Query = " Select ITEM_ID as ITEM_ID,ITEM_CODE as ITEM_CODE,ITEM_NAME as ITEM_NAME,ITEM_DESC as ITEM_DESC, UM_ID as UM_ID,ITEM_CATEGORY_ID as ITEM_CATEGORY_ID,0 as RELATED_ITEM_ID,NULL as GMS_SPEC,IS_STOCKABLE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, " & ddl_outlets.SelectedValue & " as DIVISION_ID from ITEM_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "Item_Master"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''3) ACCOUNT_GROUP
    '    Query = "Select * from ACCOUNT_GROUPS"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "ACCOUNT_GROUPS"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''4) ACCOUNT_MASTER
    '    Query = "Select * from ACCOUNT_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "ACCOUNT_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''5) CN_SERIES
    '    Query = "Select * from CN_SERIES where DIV_ID = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "CN_SERIES"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''6) COST_CENTER_MASTER
    '    Query = "Select * from COST_CENTER_MASTER where Division_Id = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "COST_CENTER_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    '7) COUNTRY_MASTER
    '    Query = "Select * from COUNTRY_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "COUNTRY_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''8) DC_SERIES
    '    Query = "Select * from DC_SERIES where DIV_ID = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "DC_SERIES"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''8) DELIVERY_RATING_MASTER
    '    Query = "Select * from DELIVERY_RATING_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "DELIVERY_RATING_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''9) DN_SERIES
    '    Query = "Select * from DN_SERIES where DIV_ID = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "DN_SERIES"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''9) EXCISE_MASTER
    '    Query = "Select * from EXCISE_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "EXCISE_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''10) INVOICE_SERIES
    '    Query = "Select * from INVOICE_SERIES where DIV_ID = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "INVOICE_SERIES"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''11) ITEM_CATEGORY
    '    Query = "Select * from ITEM_CATEGORY"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "ITEM_CATEGORY"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''13) ITEM_DETAIL
    '    Query = "Select ITEM_ID,DIV_ID,RE_ORDER_LEVEL,RE_ORDER_QTY,PURCHASE_VAT_ID,SALE_VAT_ID,OPENING_STOCK,CURRENT_STOCK,IS_EXTERNAL,TRANSFER_RATE,AVERAGE_RATE,IS_STOCKABLE from ITEM_DETAIL where DIV_ID = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "ITEM_DETAIL"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''14) MRN_SERIES
    '    Query = "Select * from MRN_SERIES where DIV_ID = " & ddl_outlets.SelectedValue
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "MRN_SERIES"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''15) PO_TYPE_MASTER
    '    Query = "Select * from PO_TYPE_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "PO_TYPE_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''16) PurchaseType_Master
    '    Query = "Select * from PurchaseType_Master"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "PurchaseType_Master"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''18) QUALITY_RATING_MASTER
    '    Query = "Select * from QUALITY_RATING_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "QUALITY_RATING_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''19) UNIT_MASTER
    '    Query = "Select * from UNIT_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "UNIT_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''20) USER_MASTER
    '    Query = "Select * from USER_MASTER_MMS"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "USER_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())

    '    ''21) VAT_MASTER
    '    Query = "Select *  from VAT_MASTER"
    '    Table = Get_Remote_DataSet(Query).Tables(0)
    '    Table.TableName = "VAT_MASTER"
    '    GlobalTables.Tables.Add(Table.Copy())



    '    Bulk_Copy(GlobalTables)


    'End Sub

    'Public Sub Bulk_Copy(ByVal _Table As DataSet)
    '    con = New SqlConnection(DNS)

    '    If con.State = ConnectionState.Open Then con.Close()
    '    con.Open()

    '    Dim tran As SqlTransaction
    '    tran = con.BeginTransaction
    '    Try
    '        ''********************************************************************''
    '        ''Synchronize local tables with global tables
    '        ''********************************************************************''
    '        'CollectData() ''collect data from global tables

    '        Dim cmd As SqlCommand

    '        'Using _TransactionScope As New Transactions.TransactionScope()
    '        ''we can open only one connection with the single server under TransactionScope object.
    '        ''So create a common command object to execute delete queries with the same connection
    '        ''object which is used by Sql Bulk Copy object.

    '        ''delete data from local tables
    '        ''DeleteData(CommandObject)

    '        ''Synchronize data




    '        cmd = New SqlCommand("delete from ACCOUNT_GROUPS")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from ACCOUNT_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from CITY_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from CN_SERIES")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from COST_CENTER_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from COUNTRY_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from DC_SERIES")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from DELIVERY_RATING_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        'cmd = New SqlCommand("delete from DIVISION_SETTINGS")
    '        'cmd.Connection = con
    '        'cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from DN_SERIES")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from EXCISE_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from INVOICE_SERIES")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from ITEM_CATEGORY")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from ITEM_DETAIL")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from ITEM_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from MRN_SERIES")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from PO_TYPE_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from PurchaseType_Master")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from QUALITY_RATING_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from UNIT_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from USER_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()

    '        cmd = New SqlCommand("delete from VAT_MASTER")
    '        cmd.Transaction = tran
    '        cmd.Connection = con
    '        cmd.ExecuteNonQuery()



    '        Dim BulkCopy As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)

    '        AddHandler BulkCopy.SqlRowsCopied, AddressOf BulkCopy_SqlRowsCopied
    '        BulkCopy.NotifyAfter = 100
    '        For Each Table1 As DataTable In _Table.Tables

    '            BulkCopy.DestinationTableName = Table1.TableName
    '            BulkCopy.WriteToServer(Table1)
    '        Next
    '        BulkCopy.Close()
    '        tran.Commit()
    '        con.Close()


    '        ''********************************************************************''
    '        ''********************************************************************''  


    '    Catch ex As Exception
    '        tran.Rollback()
    '    End Try
    'End Sub

    


    'Private Function Get_Remote_DataSet(ByVal qry As String) As DataSet
    '    cmd = New SqlCommand()
    '    Dim ds_new As New DataSet()
    '    Try
    '        If con Is Nothing Then
    '            con = New SqlConnection(gblDNS_Online)
    '        End If
    '        If con.State = ConnectionState.Open Then con.Close()
    '        con.Open()

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Class Connection")
    '    End Try
    '    cmd.Connection = con
    '    'cmd.CommandText = "SELECT *,CityMaster.CityName_vch FROM OutletMaster INNER JOIN outletSettings ON OutletMaster.Pk_OutletId_num = outletSettings.Outlet_id INNER JOIN dbo.CityMaster ON OutletMaster.fk_CityId_num = dbo.CityMaster.pk_CityId_num WHERE outletSettings.Outlet_id = " + outlet_id.ToString()
    '    cmd.CommandText = qry
    '    cmd.CommandType = CommandType.Text
    '    Dim adp As New SqlDataAdapter(cmd)

    '    adp.Fill(ds_new)

    '    Return ds_new
    'End Function

End Class