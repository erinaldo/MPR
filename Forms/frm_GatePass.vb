Imports MMSPlus.GatePass
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text.RegularExpressions
Public Class frm_GatePass

    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New cls_GatePass_master
    Dim prpty As cls_GatePass_prop
    Dim flag As String
    Dim GatePassId As Int16
    Dim NEWCUST As Int16 = 0
    Dim dtable_Item_List As DataTable
    Dim gstnoRegex As New Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")
    Dim mobileRegex As New Regex("^\d{10}$")
    Dim timeRegex As New Regex("^([0]?[1-9]|[1][0-2]):([0-5][0-9]|[1-9]) [AP]M$")
    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub fill_grid(Optional ByVal condition As String = "")
        Try

            Dim strsql As String

            strsql = "SELECT * FROM (SELECT GatePassId," &
            "GatePassNo," &
            " dbo.fn_Format(GatePassDate) AS GatePassDate," &
            " VehicalNo,ACC_NAME AS Customer FROM dbo.GatePass_Master " &
            "JOIN dbo.ACCOUNT_MASTER ON ACCOUNT_MASTER.ACC_ID=dbo.GatePass_Master.Acc_id)tb " &
            "WHERE (CAST(GatePassId AS varchar) +GatePassNo+GatePassDate+VehicalNo+Customer) LIKE '%" & condition & "%'  order by 1"

            Dim dt As DataTable = obj.Fill_DataSet(strsql).Tables(0)

            grdGatePass.DataSource = dt

            grdGatePass.Columns(0).Width = 90
            grdGatePass.Columns(1).Width = 150
            grdGatePass.Columns(2).Width = 150
            grdGatePass.Columns(3).Width = 150
            grdGatePass.Columns(4).Width = 300


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Private Sub frm_GatePass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            obj.FormatGrid(grdGatePass)
            'obj.FormatGrid(flxList)

            ' table_style()
            clsObj.ComboBind(cmbBillNo, "Select SI_ID,(SI_CODE+CAST(SI_NO AS VARCHAR)) AS BillNo from dbo.SALE_INVOICE_MASTER WHERE SI_ID NOT IN (SELECT DISTINCT SI_ID FROM GatePass_Master)", "BillNo", "SI_ID", True)
            new_initilization()
            fill_grid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading_Error)
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initilization()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        Dim cmd As SqlCommand
        Try

            If Validation() = False Then
                Exit Sub
            End If

            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If

                prpty = New cls_GatePass_prop

                Dim ds1 As DataSet = obj.FillDataSet("Select isnull(max(GatePassId),0) + 1 from dbo.GatePass_Master")
                GatePassId = Convert.ToInt32(ds1.Tables(0).Rows(0)(0))
                prpty.GatePassId = GatePassId

                Dim ds As New DataSet()
                ds = obj.fill_Data_set("GET_GATEPASS_NO", "@DIV_ID", v_the_current_division_id)
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("GatePass series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                    ds.Dispose()
                    Exit Sub
                Else
                    If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                        MsgBox("GatePass series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                        MsgBox("GatePass series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    Else
                        ds.Dispose()
                    End If
                End If

                prpty.GatePassNo = lbl_GPNo.Text
                prpty.GatePassDate = Now
                prpty.BillNo = cmbBillNo.SelectedText
                prpty.BillDate = Convert.ToDateTime(lblBillDate.Text)
                prpty.SI_ID = cmbBillNo.SelectedValue
                prpty.Acc_id = Convert.ToInt32(lblAccId.Text)
                prpty.Remarks = txtRemarks.Text
                prpty.VehicalNo = lblvehicleNo.Text
                prpty.EntryDate = Convert.ToDateTime(dtpGateInDate.Text)
                prpty.InTime = mtxtInTime.Text
                prpty.OutTime = mtxtOutTime.Text
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.DIVISION_ID = v_the_current_division_id

                clsObj.Insert_GatePass_MASTER(prpty)

            End If


            If flag = "save" Then
                If MsgBox("GatePass information has been Saved." & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptInvoicePrint, "GatePassId", CStr(prpty.GatePassId), CStr(enmDataType.D_int))
                End If
            Else
                MsgBox("You Can't edit this.")
            End If
            fill_grid()
            new_initilization()
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                If grdGatePass.SelectedRows.Count > 0 Then

                    obj.RptShow(enmReportName.RptInvoicePrint, "GatePassId", CStr(grdGatePass("GatePassId", grdGatePass.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptInvoicePrint, "GatePassId", CStr(GatePassId), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub new_initilization()

        lbl_GPDate.Text = Now.ToString("dd-MMM-yyyy")
        txtCustomer.Text = ""
        cmbBillNo.SelectedIndex = 0
        txtRemarks.Text = ""
        mtxtInTime.Text = ""
        mtxtOutTime.Text = ""
        lblBillDate.Text = ""
        lblvehicleNo.Text = ""

        Dim ds As New DataSet()
        ds = obj.fill_Data_set("GET_GATEPASS_NO", "@DIV_ID", v_the_current_division_id)
        If ds.Tables(0).Rows.Count = 0 Then
            lbl_GPNo.Text = "GatePass series does not exists"
            ds.Dispose()
            Exit Sub
        Else
            If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                lbl_GPNo.Text = "GatePass series does not exists"
                ds.Dispose()
                Exit Sub
            ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                lbl_GPNo.Text = "GatePass series has been completed"
                ds.Dispose()
                Exit Sub
            Else
                lbl_GPNo.Text = ds.Tables(0).Rows(0)(0).ToString() & (Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1).ToString
                ds.Dispose()
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''TO GET INV NO'''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        flag = "save"
    End Sub
    Private Function Validation() As Boolean

        If cmbBillNo.SelectedIndex = 0 Then
            MsgBox("Select BillNo to Gate Pass.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If

        If Not String.IsNullOrEmpty(mtxtInTime.Text.Trim) Then
            If Not timeRegex.IsMatch(mtxtInTime.Text) Then
                MsgBox("In Time is not valid. Try again after entering valid Time.", MsgBoxStyle.Information, "Invalid Phone Format!!!")
                Return False
            End If
        End If

        If Not String.IsNullOrEmpty(mtxtOutTime.Text.Trim) Then
            If Not timeRegex.IsMatch(mtxtOutTime.Text) Then
                MsgBox("Out Time is not valid. Try again after entering valid Time.", MsgBoxStyle.Information, "Invalid GST Format!!!")
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub cmbBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBillNo.SelectedIndexChanged
        Dim strSql As String

        If cmbBillNo.SelectedValue <> -1 Then
            strSql = "SELECT ACCOUNT_MASTER.ACC_NAME, dbo.fn_Format(SALE_INVOICE_MASTER.SI_DATE) AS SI_DATE, VEHICLE_NO, ACCOUNT_MASTER.ACC_ID"
            strSql = strSql & " FROM dbo.SALE_INVOICE_MASTER INNER JOIN"
            strSql = strSql & " ACCOUNT_MASTER ON ACCOUNT_MASTER.ACC_ID = dbo.SALE_INVOICE_MASTER.CUST_ID"
            strSql = strSql & " WHERE SALE_INVOICE_MASTER.SI_ID = " & cmbBillNo.SelectedValue

            txtCustomer.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(0)
            lblBillDate.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(1)
            lblvehicleNo.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(2)
            lblAccId.Text = obj.Fill_DataSet(strSql).Tables(0).Rows(0)(3)
        End If
    End Sub
End Class
