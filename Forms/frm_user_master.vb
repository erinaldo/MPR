Imports MMSPlus.user_master

Public Class frm_user_master

    Implements IForm
    Dim _user_role As String


    Dim Obj As New cls_user_master
    Dim prpty As cls_user_master_prop
    Dim flag As String
    Dim _rights As Form_Rights

    Public Sub New(ByVal user_role As String, ByVal rights As Form_Rights)
        _user_role = user_role
        _rights = rights
        InitializeComponent()
    End Sub

   
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
    'Private Sub ComboBind_Enum(ByVal cmb As ComboBox, ByVal enm As [Enum])
    '    Dim Names() As String = [Enum].GetNames(enm.GetType())
    '    Dim Values1 As Array = [Enum].GetValues(enm.GetType())
    '    '     Dim dr As DataRow
    '    'Dim mylist As New List(Of Array)


    '    cmb.Items.Clear()
    '    'Values1.Resize(
    '    'ReDim Preserve Values1(5)
    '    'cmb.DataSource = Values1
    '    '      Dim dt As DataTable = CType(cmb.DataSource, DataTable)
    '    '       cmb.Items.Add("AAAA")

    '    'Values1.Resize(Values1, Values1.Length + 1)
    '    'cmb.SelectedItem = "Select"
    '    'cmb.SelectedIndex = "-1"
    '    'cmb.SelectedIndex = 1
    '    'MsgBox(cmb.SelectedValue & "    -     " & cmb.Text)
    'End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick
        Try
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If
            If grdUsers.SelectedRows.Count = 0 Then
                Exit Sub
            End If

            If grdUsers.SelectedRows.Count > 0 And MsgBox("Are you sure to Delete it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading_delete) = MsgBoxResult.Yes Then
                prpty = New cls_user_master_prop
                prpty.user_id = grdUsers.SelectedRows.Item(0).Cells("user_id").Value
                prpty.user_name = ""
                prpty.password = ""
                prpty.user_role = ""
                prpty.division_id = 0

                Obj.delete_USER_MASTER(prpty)
                FillGrid()
                ClearControls()
                MsgBox("Record Deleted")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error deleteClick --> frm_hr_location_master")
        End Try
    End Sub

    Private Sub ClearControls()
        Obj.Clear_All_TextBox(Me.GroupBox1.Controls)
        txtUserName.Enabled = True
        chkChangePwd.Checked = False
        chkChangePwd.Visible = False

        Label3.Visible = True
        Label4.Visible = True
        txtPassword.Visible = True
        txtConf_Password.Visible = True
        txtUserName.Focus()
    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            flag = "save"
            ClearControls()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_hr_user_master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGrid()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                If txtPassword.Text <> txtConf_Password.Text Then
                    MsgBox("Password does not match !", MsgBoxStyle.Critical, "Save User")
                Else
                    If Obj.get_record_count("select * from user_master where user_name ='" & txtUserName.Text & "'") > 0 Then
                        MsgBox("User already exsist !", MsgBoxStyle.Information, "Save User")
                    Else
                        prpty = New cls_user_master_prop
                        prpty.user_name = txtUserName.Text
                        prpty.password = txtPassword.Text
                        prpty.user_role = _user_role
                        prpty.division_id = v_the_current_division_id
                        Obj.insert_USER_MASTER(prpty)
                        ClearControls()
                        MsgBox("Record Saved", MsgBoxStyle.Information, "Save User")
                    End If
                End If
            Else
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                If chkChangePwd.Checked = True Then
                    If txtOldPassword.Text <> grdUsers.SelectedRows.Item(0).Cells(2).Value Then
                        MsgBox("Wrong Password !", MsgBoxStyle.Information, "Change Password")
                    Else
                        prpty = New cls_user_master_prop
                        prpty.user_id = grdUsers.SelectedRows.Item(0).Cells(0).Value
                        prpty.user_name = txtUserName.Text
                        prpty.password = txtNewPassword.Text
                        prpty.user_role = _user_role
                        prpty.division_id = v_the_current_division_id
                        Obj.update_USER_MASTER(prpty)
                        ClearControls()
                        MsgBox("Record Updated", MsgBoxStyle.Information, "Updated User")
                        flag = "save"
                    End If
                End If
            End If
            FillGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_hr_user_master")
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub frm_user_master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'ComboBind_Enum(New ComboBox, New POStatus)
            Obj.FormatGrid(grdUsers)
            flag = "save"
            FillGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frm_hr_user_master_Load")
        End Try

    End Sub

    Private Sub FillGrid()
        Try
            Obj.GridBind(grdUsers, "select user_id,user_name,password,user_role,division_id from user_master where USER_ROLE = '" & _user_role & "'")
            grdUsers.Columns(0).Visible = False 'user id
            grdUsers.Columns(1).HeaderText = "User Name"
            grdUsers.Columns(1).Width = 400
            grdUsers.Columns(2).Visible = False 'password
            grdUsers.Columns(3).Visible = False 'user_role
            grdUsers.Columns(4).Visible = False 'DIVISION_ID
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub


    Private Sub grdUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdUsers.Click
        Try
            txtUserName.Text = grdUsers.SelectedRows.Item(0).Cells(1).Value
            txtUserName.Enabled = False
            chkChangePwd.Visible = True

            Label3.Visible = False
            Label4.Visible = False
            txtPassword.Visible = False
            txtConf_Password.Visible = False

            flag = "update"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->grdUsers_Click")
        End Try

    End Sub

    Private Sub chkChangePwd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChangePwd.CheckedChanged
        Try
            If chkChangePwd.Checked = True Then
                lblOldPwd.Visible = True
                lblNewPwd.Visible = True
                txtOldPassword.Visible = True
                txtNewPassword.Visible = True
                lblOldPwd.Left = Label3.Left
                lblNewPwd.Left = Label4.Left
                txtOldPassword.Left = txtPassword.Left
                txtNewPassword.Left = txtConf_Password.Left
                txtOldPassword.Focus()
            Else
                lblOldPwd.Visible = False
                lblNewPwd.Visible = False
                txtOldPassword.Visible = False
                txtNewPassword.Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -->chkChangePwd_CheckedChanged")
        End Try
    End Sub

End Class
