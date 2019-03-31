Public Class Licence
    Dim obj As New CommonClass
    Dim Division As String
    Dim LicDate As String
    Dim chkDate As Date
    Dim Lockkey As String = "$ync-3sh8-sqoy19"

    Private Sub Licence_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlRenew.Visible = False
        GetLCKey()

    End Sub

    Public Sub GetLCKey()

        Division = obj.ExecuteScalar("SELECT TOP 1 UPPER(DIVISION_NAME) FROM dbo.DIVISION_SETTINGS")
        chkDate = obj.ExecuteScalar("SELECT TOP 1 CAST(SI_DATE AS DATE) FROM dbo.SALE_INVOICE_MASTER ORDER BY SI_ID DESC")

        If chkDate.ToString.Length = 0 Then
            chkDate = Date.Now.ToString("dd-MMM-yyyy")
        End If

        Dim GetKey As String = obj.ExecuteScalar("SELECT D1 FROM templd")

        If GetKey.ToString.Length > 0 Then

            Dim StrKey() As String = Decrypt(GetKey, Lockkey).Split(",")

            LicDate = StrKey(1)

            lblExpireDate.Text = "Expire On : " & LicDate

            If chkDate > Convert.ToDateTime(LicDate).Date Then
                lblLicnceSMS.Text = "Your Softwares License is Expired"
            End If

        Else
            lblExpireDate.Text = "Expire On : " & LicDate
        End If

    End Sub

    Private Sub btnRenew_Click(sender As Object, e As EventArgs) Handles btnRenew.Click

        Cursor.Current = Cursors.WaitCursor

        If txtKey.Text.Trim().Length = 0 Then
            MsgBox("Error: A problem occurred when Systems tried to activate. For a possible resolution, Contact your system administrator or technical support provider for assistance.", MsgBoxStyle.Critical, "Error: Licence Verify")
            Exit Sub
        End If

        Dim StrKey() As String = Decrypt(txtKey.Text, Lockkey).Split(",")

        Dim Company As String = StrKey(0)
        Dim RenewDate As String = StrKey(1)

        If Company = Division And RenewDate > Convert.ToDateTime(LicDate).Date Then
            obj.ExecuteNonQuery("UPDATE templd SET D1='" & Decrypt(txtKey.Text, Lockkey) & "'")
            MsgBox("Congratulations! Your Systems activation completed sucessfully.", MsgBoxStyle.Information, "Licence Verify")
            Me.Close()
            LoginForm.Show()
        Else
            Cursor.Current = Cursors.Default
            MsgBox("Error: A problem occurred when Systems tried to activate. For a possible resolution, Contact your system administrator or technical support provider for assistance.", MsgBoxStyle.Critical, "Error: Licence Verify")
        End If

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnRenewSettings_Click(sender As Object, e As EventArgs) Handles btnRenewSettings.Click
        pnlRenew.Visible = True
        txtKey.Focus()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class