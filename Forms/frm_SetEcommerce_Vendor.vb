Public Class frm_SetEcommerce_Vendor
    Public Vendor_ID As Int16 = 0
    Public PVendor_ID As Int16 = 0
    Public Vendor_Name As String = ""
    Dim obj As New CommonClass

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frm_SetEcommerce_Vendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CustomerBind()
    End Sub

    Public Sub CustomerBind()
        obj.ComboBindConsumer(cmbSupplier, "Select ACC_ID,LTRIM(ACC_NAME) AS ACC_NAME from ACCOUNT_MASTER WHERE Is_Active=1 And AG_ID in (1,2,3,6) Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
    End Sub


    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Me.Close()
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        cmbSupplier.SelectedIndex = cmbSupplier.FindStringExact(cmbSupplier.Text)
        If (cmbSupplier.SelectedValue <> -1) Then
            Vendor_ID = cmbSupplier.SelectedValue
            Vendor_Name = cmbSupplier.Text
        Else
            If PVendor_ID = 0 Then
                Vendor_ID = 0
                Vendor_Name = ""
            Else
                cmbSupplier.SelectedValue = PVendor_ID
                Vendor_ID = cmbSupplier.SelectedValue
                Vendor_Name = cmbSupplier.Text
                PVendor_ID = 0
            End If


        End If


    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub
End Class