Imports System.Data.SqlClient

Public Class frm_RateList_Mapping
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
           "  Select srl_id,srl_name from supplier_rate_list " &
           "  WHERE SUPP_ID IN(SELECT ACC_ID FROM dbo.ACCOUNT_MASTER WHERE AG_ID=1) order by srl_name "

        obj.ComboBind(cmbratelist, query, "srl_name", "srl_id")
        obj.ComboBind(cmbSupplier, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=1 Order by ACC_NAME ", "ACC_NAME", "ACC_ID")

    End Sub
End Class