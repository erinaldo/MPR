Imports System.Data.SqlClient
Imports MMSPlus.CommonClass
Public Class frm_RateList_Mapping
    Inherits System.Windows.Forms.Form

    Dim obj As New CommonClass

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub


    Dim clsObj As New supplier_rate_list.cls_supplier_rate_list
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If cmbratelist.SelectedValue > 0 And cmbSupplier.SelectedValue > 0 Then

            Dim Query As String = String.Format("exec UpdateCustomerRateListMapping {0},{1}", cmbSupplier.SelectedValue, cmbratelist.SelectedValue)
            If obj.ExecuteNonQuery(Query) > 0 Then
                MessageBox.Show("record added sucessfully.")
                Me.Close()
            Else
                MessageBox.Show("this customer already mapped with ratelist.")
            End If
        Else
            MessageBox.Show("Please Select RateList/Customer.")
        End If
    End Sub

    Private Sub frm_RateList_Mapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim query As String = " select 0 as srl_id,'--Select--' as Srl_name union  " &
           "  Select supplier_rate_list.srl_id,srl_name from supplier_rate_list JOIN dbo.CUSTOMER_RATE_LIST_MAPPING ON dbo.CUSTOMER_RATE_LIST_MAPPING.SRL_ID = dbo.SUPPLIER_RATE_LIST.SRL_ID " &
           "  WHERE SUPPLIER_RATE_LIST.SUPP_ID IN(SELECT ACC_ID FROM dbo.ACCOUNT_MASTER WHERE AG_ID in (1,2,3,6)) order by srl_name "

        obj.ComboBind(cmbratelist, query, "srl_name", "srl_id")
        obj.ComboBind(cmbSupplier, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID in (1,2,3,6) Order by ACC_NAME ", "ACC_NAME", "ACC_ID")

    End Sub

    Private Sub cmbratelist_Enter(sender As Object, e As EventArgs) Handles cmbratelist.Enter
        If Not cmbratelist.DroppedDown Then
            cmbratelist.DroppedDown = True
        End If
    End Sub

    Private Sub cmbSupplier_Enter(sender As Object, e As EventArgs) Handles cmbSupplier.Enter
        If Not cmbSupplier.DroppedDown Then
            cmbSupplier.DroppedDown = True
        End If
    End Sub

    Private Sub frm_RateList_Mapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class