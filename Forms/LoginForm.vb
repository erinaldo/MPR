Public Class LoginForm

    Dim obj As New CommonClass

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    '

    Public Sub New()

        Dim command_line_argument() As String
        command_line_argument = Environment.GetCommandLineArgs()
        If command_line_argument.Length > 1 Then
            v_logged_in_as_admin = True
        End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try

            Dim result As Integer
            If Not rdoCostCenter.Checked Then
                result = obj.get_record_count("select * from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "' and user_role = 'MS' ")
            Else
                result = obj.get_record_count("select * from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "' and user_role = 'CC' and CostCenter_Id=" & cmbCostCenter.SelectedValue)
            End If
            If result > 0 Then
                result = obj.get_record_count("select * from DIVISION_SETTINGS")
                If result > 0 Then

                    v_the_current_division_id = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
                    v_the_current_selected_division = "MMS+ -- " & Convert.ToString(obj.ExecuteScalar("select DIVISION_NAME from DIVISION_SETTINGS"))
                    v_the_current_logged_in_user_name = UsernameTextBox.Text
                    v_the_current_logged_in_user_id = Convert.ToInt32(obj.ExecuteScalar("select user_id from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "'")) ' and comp_id = " & v_the_current_division_id))
                    v_the_current_logged_in_user_role = Convert.ToString(obj.ExecuteScalar("select user_role from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "'")) ' and comp_id = " & v_the_current_division_id))
                    'We will not use v_the_current_ServerDate as transaction date becouse
                    'our entries are time based also.
                    'v_the_current_ServerDate = Now

                    If Not rdoCostCenter.Visible Then
                        ' MDI_Warehouse.Show()
                        MDIMain.MDIMain_Load(Nothing, Nothing)
                        MDIMain.Show()
                    ElseIf rdoCostCenter.Checked Then
                        v_the_current_Selected_CostCenter_id = Convert.ToInt32(cmbCostCenter.SelectedValue)
                        MDICostCenter.MDICostCenter_Load(Nothing, Nothing)
                        MDICostCenter.Show()
                    Else
                        'MDIMain.MDIMain_Load(Nothing, Nothing)
                        'MDIMain.Show()

                        MDI_Warehouse.MDIMain_Load(Nothing, Nothing)
                        MDI_Warehouse.Show()
                    End If
                    Me.Hide()
                Else
                    Dim prp As New Form_Rights
                    prp.allow_trans = "Y"
                    prp.allow_view = "Y"
                    Dim obj As New frm_DivisionSettings(True, prp)
                    obj.ShowDialog()
                End If
            Else
                MsgBox("Wrong User Name or Password!", MsgBoxStyle.Information, "Login")
            End If

            'If rdoCostCenter.Checked Then
            '    v_the_current_division_id = cmbCostCenter.SelectedValue
            '    v_the_current_selected_division = "AFBSL MMS-- " & cmbCostCenter.Text
            'Else
            '    v_the_current_division_id = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
            '    v_the_current_selected_division = "AFBSL MMS-- " & Convert.ToString(obj.ExecuteScalar("select DIVISION_NAME from DIVISION_SETTINGS"))
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.InnerException.Message)
        End Try

        '' ''Try
        '' ''    Dim result As Integer
        '' ''    'If cmbCostCenter.Items.Count = 0 Then
        '' ''    '    Me.Hide()
        '' ''    '    MDIMain.Show()
        '' ''    '    Exit Sub
        '' ''    'End If
        '' ''    If rdoCostCenter.Checked Then
        '' ''        result = obj.get_record_count("select * from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "' and user_role='" & gblCostCenter_ROLE & "'") ' and division_id = " & v_the_current_division_id)
        '' ''        v_the_current_division_id = cmbCostCenter.SelectedValue
        '' ''        v_the_current_selected_CC_MS = "AFBSL MMS-- " & cmbCostCenter.Text
        '' ''    Else
        '' ''        v_the_current_division_id = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
        '' ''        v_the_current_selected_CC_MS = "AFBSL MMS-- " & Convert.ToString(obj.ExecuteScalar("select DIVISION_NAME from DIVISION_SETTINGS"))
        '' ''        result = obj.get_record_count("select * from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "' and user_role='" & gblMainStore_ROLE & "'") ' and division_id = " & v_the_current_division_id)
        '' ''    End If
        '' ''    If result > 0 Then
        '' ''        v_the_current_logged_in_user_name = UsernameTextBox.Text
        '' ''        v_the_current_logged_in_user_id = Convert.ToInt32(obj.ExecuteScalar("select user_id from USER_MASTER where user_name = '" & UsernameTextBox.Text & "' and password = '" & PasswordTextBox.Text & "'")) ' and comp_id = " & v_the_current_division_id))
        '' ''        v_the_current_Selected_CostCenter_id = Convert.ToInt32(cmbCostCenter.SelectedValue)
        '' ''        now = Now
        '' ''        Me.Hide()
        '' ''        ''''''load function is called to change name of the company show on the top of the mdi form
        '' ''        ''''''and also to update the birthday and anniversary list
        '' ''        If rdoCostCenter.Checked Then
        '' ''            'MDICostCenter.MDICostCenter_Load(Nothing, Nothing)
        '' ''            MDICostCenter.Show()
        '' ''        Else
        '' ''            'MDIMain.MDIMain_Load(Nothing, Nothing)
        '' ''            MDIMain.Show()
        '' ''        End If
        '' ''        'MDIMain.CloseAllToolStripMenuItem_Click(Nothing, Nothing)
        '' ''    Else
        '' ''        MsgBox("Wrong User Name or Password !", MsgBoxStyle.Information, "Login")
        '' ''    End If
        '' ''Catch ex As Exception
        '' ''    MsgBox(ex.Message)
        '' ''    MsgBox(ex.InnerException.Message)
        '' ''End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub rdoCostCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCostCenter.CheckedChanged
        'lbl.Visible = rdoCostCenter.Checked
        'cmbCostCenter.Visible = rdoCostCenter.Checked
        cmbCostCenter.Enabled = rdoCostCenter.Checked
    End Sub

    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'lbl.Visible = rdoCostCenter.Checked
        'cmbCostCenter.Visible = rdoCostCenter.Checked
        'cmbCostCenter.Enabled = rdoCostCenter.Checked
        Dim year As New DateTime(DateTime.Now.Year, 4, 1)

        If v_division_type = division_type.Warehouse Then
            lbl.Visible = False
            rdoCostCenter.Visible = False
            rdoMainKitchen.Visible = False
            cmbCostCenter.Visible = False
            ' picAlchemist.Visible = True
            ' lblAlchemist.Visible = True
        End If

        If v_logged_in_as_admin Then
            PasswordTextBox.Text = "admin"
        End If
        UsernameTextBox.Focus()
        'frm_DivisionSettings.new_mode = True
        'Dim commObj As New CommonClass
        'Dim con As New SqlConnection(DNS)
        'Dim ds As DataSet
        'Dim cmd As SqlCommand
        'con.Open()
        'cmd = New SqlCommand("select * from DIVISION_SETTINGS", con)
        'da = New SqlDataAdapter("select * from DIVISION_SETTINGS", con)
        'ds = New DataSet
        'da.Fill(ds)
        'MsgBox(Fill_DataSet("select * from DIVISION_SETTINGS").Tables(0).Rows.Count)
        'If obj.get_record_count("select * from DIVISION_SETTINGS") = 0 Then
        ' frm_DivisionSettings.new_mode = True
        ' frm_DivisionSettings.ShowDialog()
        ' Else
        '       
        Dim cnt As Integer = obj.get_record_count("SELECT CostCenter_Id,CostCenter_Name + SPACE(2) + '(' + CostCenter_Code + ')' AS cost_center FROM COST_CENTER_MASTER ORDER BY CostCenter_Name")
        Dim ds As DataSet
        Try
            obj.ComboBind(cmbCostCenter, "SELECT CostCenter_Id,CostCenter_Name + SPACE(2) + '(' + CostCenter_Code + ')' AS cost_center FROM COST_CENTER_MASTER ORDER BY CostCenter_Name", "Cost_Center", "CostCenter_Id")

            ds = obj.Fill_DataSet("select * from DIVISION_SETTINGS")
            If ds.Tables(0).Rows.Count > 0 Then

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading_Error)
        End Try
        If cmbCostCenter.Items.Count > 0 Then
            cmbCostCenter.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoginForm_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        obj.ComboBind(cmbCostCenter, "select CostCenter_Id,CostCenter_Name + space(2) + '(' + CostCenter_Code + ')' as cost_center from COST_CENTER_MASTER order by CostCenter_Name ", "cost_center", "CostCenter_Id")
    End Sub

    Private Sub LoginForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim clsRequest As System.Net.FtpWebRequest =
                   DirectCast(System.Net.WebRequest.Create("ftp://syncsolz.com/1.txt"), System.Net.FtpWebRequest)
        clsRequest.Credentials = New System.Net.NetworkCredential("syncsolz@junifilms.com", "$ync$olz@123")
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile

        'read in file...
        Dim bFile() As Byte = System.IO.File.ReadAllBytes("e:\1.txt")

        ' upload file...
        Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
        clsStream.Write(bFile, 0, bFile.Length)
        clsStream.Close()
        clsStream.Dispose()
    End Sub
End Class
