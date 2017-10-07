Public Class frm_ChangePassword

    Dim obj As New CommonClass


    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_ChangePassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txt_UserName.Text = v_the_current_logged_in_user_name
    End Sub
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_Change_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Change.Click
        Dim qry As String = "select password from User_master where user_name = '" + txt_UserName.Text + "'"
        Dim CurrentPassword As String = obj.ExecuteScalar(qry).ToString()
        If CurrentPassword <> txt_CurrentPassword.Text Then
            MsgBox("Current Password not Correct")
        Else
            If txt_NewPassword.Text <> txt_ConfirmPassword.Text Then
                MsgBox("New Password and Confirm Password do not Match")
            Else
                Dim updatepass As String = "Update USER_MASTER set password = '" + txt_ConfirmPassword.Text + "' where user_id = " + v_the_current_logged_in_user_id.ToString()
                obj.ExecuteNonQuery(updatepass)
                MsgBox("Password Changed Successfully")
            End If
        End If
    End Sub


End Class