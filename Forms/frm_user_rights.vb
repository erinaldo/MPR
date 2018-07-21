Public Class frm_user_rights

    Implements IForm



    Dim rights As Form_Rights
    Dim v_form_name As String
    Dim v_view, transactions As String
    Dim FormName(500) As String
    Dim FormRights(500, 3) As String
    Dim Selected_Node As TreeNode
    Dim arr_index As Integer = 0
    Dim obj As New CommonClass
    Dim selected_user_id As Integer
    Dim LastParent As String = ""
    Dim Checking_UnCheckingStart As Boolean = False
    Dim AnotherChildSel As Boolean = False

    Dim _user_role As String
    Dim _form_rights As Form_Rights
    Dim _MenuStrip1 As MenuStrip

    Public Sub New(ByVal user_role As String, ByVal mStrip As MenuStrip, ByVal rights As Form_Rights)
        _user_role = user_role
        _form_rights = rights
        _MenuStrip1 = mStrip
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
        Try
          

            Dim i As Integer = 0
            Dim j As Integer = 0
            If _form_rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If
            obj.ExecuteNonQuery("delete from user_rights where user_id = " & selected_user_id)
            Save_Checked_Nodes(trvForms.Nodes)
            MsgBox("Group Rights updated !", MsgBoxStyle.Information, "Group Rights")
            LastParent = ""
            Call lstUsers_SelectedIndexChanged(sender, e)




        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_hr_user_rights")
        End Try
    End Sub

    Private Sub Save_Checked_Nodes(ByVal n As TreeNodeCollection)
        Dim insQry As String
        LastParent = ""
        For Each n1 As TreeNode In n
            If n1.Nodes.Count() > 0 Then
                Save_Checked_Nodes(n1.Nodes)
            ElseIf LastParent <> n1.Parent.Name Then
                If n1.Checked = True Then
                    LastParent = n1.Parent.Name
                    v_form_name = n1.Parent.Name

                    v_view = IIf(n1.Parent.Nodes("vw").Checked = True, "Y", "N")

                    If Not (n1.Parent.Nodes("tr") Is Nothing) Then
                        transactions = IIf(n1.Parent.Nodes("tr").Checked = True, "Y", "N")
                    Else
                        transactions = "N"
                    End If
                    'If Not (n1.Parent.Nodes("d") Is Nothing) Then
                    '    deletion = IIf(n1.Parent.Nodes("d").Checked = True, "Y", "N")
                    'Else
                    '    deletion = "N"
                    'End If
                    'If Not (n1.Parent.Nodes("u") Is Nothing) Then
                    '    updation = IIf(n1.Parent.Nodes("u").Checked = True, "Y", "N")
                    'Else
                    '    updation = "N"
                    'End If
                    insQry = "insert into user_rights(user_id,form_name,allow_trans,allow_view) values("
                    insQry += selected_user_id & ",'" & v_form_name & "','" & transactions & "','" & v_view & "')"
                    obj.ExecuteNonQuery(insQry)
                End If
            End If
        Next
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub


    Private Sub frm_user_rights_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rights = obj.Get_Form_Rights(Me.Name)
            fill_user()
            Fill_Tree()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> frm_hr_user_rights_Load")
        End Try
    End Sub

    Private Sub fill_user()
        Try
            Dim ds1 As DataSet
            lstUsers.Clear()
            lstUsers.View = View.Details
            lstUsers.Columns.Add("user_name", "", 202)
            lstUsers.Columns.Add("user_id", "state_id", 0)
            ds1 = obj.FillDataSet("select user_id ,UPPER(user_name) AS user_name ,password ,user_role ,division_id ,CostCenter_Id from user_master where user_role='" & _user_role & "'")
            For Each drow As DataRow In ds1.Tables(0).Rows
                Dim strArr As String() = {drow(1), drow(0)}
                lstUsers.Items.Add(New ListViewItem(strArr))
            Next
        Catch ex As Exception

        End Try
    End Sub
        Private Sub Fill_Tree()
        Try
            Dim node As TreeNode
            Dim T1 As ToolStripMenuItem
            trvForms.BeginUpdate()
            For Each T1 In _MenuStrip1.Items 'MDIMain.MenuStrip1.Items
                '' this if condition is used for those menu which are not to be shown in user rights trees
                If T1.Name = "UtilitiesToolStripMenuItem" Or T1.Name = "TerminateToolStripMenuItem" Then
                    Continue For
                End If
                If T1.Visible = False Then
                    Continue For
                End If
                '''''''''''''''''''''''''''''

                If T1.Name <> "logo" And T1.Name <> "Home" Then
                    node = trvForms.Nodes.Add(T1.Name, T1.Text)
                    node.ForeColor = Color.White

                    If T1.HasDropDownItems Then
                        If T1.Name = "Reports" Then
                            Nodes_under_reports(T1, node)
                        Else
                            add_sub_menu(T1, node)
                        End If
                    End If
                End If

                
            Next

            trvForms.EndUpdate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Nodes_under_reports(ByVal t1 As ToolStripMenuItem, ByVal n As TreeNode)
        '' this function add only view node under every report
        Try
            Dim t_temp As ToolStripMenuItem
            Dim node As TreeNode
            For Each obj As Object In t1.DropDownItems
                If TypeOf obj Is ToolStripMenuItem Then
                    t_temp = CType(obj, ToolStripMenuItem)
                    node = n.Nodes.Add(t_temp.Name, t_temp.Text)
                    If t_temp.HasDropDownItems Then
                        ' node.ForeColor = Color.Maroon
                        Nodes_under_reports(t_temp, node)
                    Else
                        ' node.ForeColor = Color.Maroon
                        node.Nodes.Add("vw", "View")
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub add_sub_menu(ByVal t1 As ToolStripMenuItem, ByVal n As TreeNode)
        Try
            Dim t_temp As ToolStripMenuItem
            Dim node As TreeNode
            For Each obj As Object In t1.DropDownItems
                If TypeOf obj Is ToolStripMenuItem Then
                    t_temp = CType(obj, ToolStripMenuItem)
                    If t_temp.Enabled Then
                        node = n.Nodes.Add(t_temp.Name, t_temp.Text)
                        If t_temp.HasDropDownItems Then
                            ' node.ForeColor = Color.Maroon
                            add_sub_menu(t_temp, node)
                        Else
                            '  node.ForeColor = Color.Maroon
                            Add_Nodes_of_Rigths(node)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Add_Nodes_of_Rigths(ByVal n As TreeNode)
        Try
            n.Nodes.Add("tr", "Transactional")
            n.Nodes.Add("vw", "View")
            'n.Nodes.Add("a", "Add")
            'n.Nodes.Add("u", "Update")
            'n.Nodes.Add("d", "Delete")
        Catch ex As Exception

        End Try
    End Sub


    Private Sub lstUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUsers.SelectedIndexChanged
        Try

            Dim ds As DataSet
            Dim i As Integer = 0
            Dim drv As DataRowView
            Dim lst As New ListViewItem
            Dim selitm As ListView.SelectedListViewItemCollection
            selitm = lstUsers.SelectedItems
            For Each lst In selitm
                selected_user_id = lst.SubItems(1).Text
            Next
            deselect_all(trvForms.Nodes)
            LastParent = ""
            ds = obj.FillDataSet("select * from user_rights where user_id =" & selected_user_id & " ")
            While i < ds.Tables(0).Rows.Count()
                drv = ds.Tables(0).DefaultView(i)
                Select_Node(trvForms.Nodes, drv("form_name"), drv("allow_trans"), drv("allow_view"))
                i += 1
            End While

            'For Each n1 As TreeNode In trvForms.Nodes
            '    n1.Expand()
            'Next


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> lstGroups_SelectedIndexChanged")
        End Try
    End Sub

    'Private Sub Select_Node(ByVal n As TreeNodeCollection, ByVal form_name As String, ByVal allow_add As String, ByVal allow_delete As String, ByVal allow_update As String, ByVal allow_view As String)
    '    Try
    '        Dim chk As Boolean = True
    '        For Each n1 As TreeNode In n
    '            If n1.Nodes.Count() > 0 Then
    '                '' Recursive call means --- call  this function till it reaches at last node of every menu (e.g to reach at add,delete,update node)
    '                Select_Node(n1.Nodes, form_name, allow_add, allow_delete, allow_update, allow_view)
    '            Else
    '                If LastParent <> n1.Parent.Name Then
    '                    If n1.Parent.Name = form_name Then ' if saved form_name is equal to the parent node of current node
    '                        LastParent = n1.Parent.Name
    '                        n1.Parent.Checked = True
    '                        For Each n2 As TreeNode In n1.Parent.Nodes
    '                            If n2.Name = "a" Then
    '                                If allow_add.ToUpper.Trim = "Y" Then
    '                                    n2.Checked = True
    '                                Else : n2.Checked = False
    '                                End If
    '                            ElseIf n2.Name = "d" Then
    '                                If allow_delete.ToUpper.Trim = "Y" Then
    '                                    n2.Checked = True
    '                                Else : n2.Checked = False
    '                                End If
    '                            ElseIf n2.Name = "u" Then
    '                                If allow_update.ToUpper.Trim = "Y" Then
    '                                    n2.Checked = True
    '                                Else : n2.Checked = False
    '                                End If
    '                            ElseIf n2.Name = "vw" Then
    '                                If allow_view.ToUpper.Trim = "Y" Then
    '                                    n2.Checked = True
    '                                Else : n2.Checked = False
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Select_Node")
    '    End Try
    'End Sub

    Private Sub Select_Node(ByVal n As TreeNodeCollection, ByVal form_name As String, ByVal allow_trans As String, ByVal allow_view As String)
        Try
            Dim chk As Boolean = True
            For Each n1 As TreeNode In n
                If n1.Nodes.Count() > 0 Then
                    '' Recursive call means --- call  this function till it reaches at last node of every menu (e.g to reach at add,delete,update node)
                    Select_Node(n1.Nodes, form_name, allow_trans, allow_view)
                Else
                    If LastParent <> n1.Parent.Name Then
                        If n1.Parent.Name = form_name Then ' if saved form_name is equal to the parent node of current node
                            LastParent = n1.Parent.Name
                            n1.Parent.Checked = True
                            For Each n2 As TreeNode In n1.Parent.Nodes
                                If n2.Name = "tr" Then
                                    If allow_trans.ToUpper.Trim = "Y" Then
                                        n2.Checked = True
                                    Else : n2.Checked = False
                                    End If
                                ElseIf n2.Name = "vw" Then
                                    If allow_view.ToUpper.Trim = "Y" Then
                                        n2.Checked = True
                                    Else : n2.Checked = False
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Select_Node")
        End Try
    End Sub

    Private Sub deselect_all(ByVal n As TreeNodeCollection)
        Try
            For Each n1 As TreeNode In n
                n1.Checked = False
                If n1.Nodes.Count() > 0 Then
                    deselect_all(n1.Nodes)
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub trvForms_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvForms.AfterCheck
        Try
            RemoveHandler trvForms.AfterCheck, AddressOf trvForms_AfterCheck

            Dim chk As Boolean
            chk = e.Node.Checked

            If e.Node.Nodes.Count = 0 Then
                If e.Node.Checked = True Then
                    If Not (e.Node.Parent Is Nothing) Then
                        e.Node.Parent.Checked = True
                    End If
                Else
                    If Not (e.Node.Parent Is Nothing) Then
                        For Each n As TreeNode In e.Node.Parent.Nodes
                            If n.Checked = False Then
                                chk = False
                            Else
                                chk = True
                                Exit For
                            End If
                        Next
                        e.Node.Parent.Checked = chk
                    End If
                End If
            Else
                Call Checkin_UnCheckingStart(e.Node, e.Node.Checked)
            End If
            AnotherChildSel = False
            Test_All_Parents(e.Node, e.Node.Checked)
            If AnotherChildSel = False Then
                Call UnCheckAllParent(e.Node) '' Un check all parents if none of the child is checked
            Else
                Call CheckAllParent(e.Node) '' Check all parents if any child of the selected parent checked
            End If
            AddHandler trvForms.AfterCheck, AddressOf trvForms_AfterCheck
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error -> trvForms_AfterCheck")
        End Try
    End Sub

    Private Sub Checkin_UnCheckingStart(ByVal node As TreeNode, ByVal Check As Boolean)
        Try
            For Each n As TreeNode In node.Nodes
                If n.Nodes.Count > 0 Then
                    Checkin_UnCheckingStart(n, Check)
                End If
                n.Checked = Check
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Checkin_UnCheckingStart")
        End Try
    End Sub

    Private Sub Test_All_Parents(ByVal node As TreeNode, ByVal Check As Boolean)
        '' this method is used to check is there any child of selected node is checked or not ?????????
        Try
            If Not (node.Parent Is Nothing) Then
                For Each n As TreeNode In node.Parent.Nodes
                    If Not AnotherChildSel Then
                        For Each n1 As TreeNode In n.Nodes
                            If n1.Checked = True Then
                                AnotherChildSel = True
                                Exit Sub
                            End If
                        Next
                        Test_All_Parents(n.Parent, Check)
                    End If
                Next
            Else
                AnotherChildSel = Check
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Test_All_Parents")
        End Try
    End Sub

    Private Sub UnCheckAllParent(ByVal node As TreeNode)
        Try
            If Not (node.Parent Is Nothing) Then
                UnCheckAllParent(node.Parent)
            Else
                node.Checked = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> UnCheckAllParent")
        End Try
    End Sub

    Private Sub CheckAllParent(ByVal node As TreeNode)
        Try
            If Not (node.Parent Is Nothing) Then
                CheckAllParent(node.Parent)
            Else
                node.Checked = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> CheckAllParent")
        End Try
    End Sub

    Private Sub ExpandAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandAllToolStripMenuItem.Click
        Try
            
            trvForms.ExpandAll()
            
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> ExpandAllToolStripMenuItem_Click")
        End Try
    End Sub

    Private Sub CollapseAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollapseAllToolStripMenuItem.Click
        Try
           
            trvForms.CollapseAll()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        Try
            trvForms.ExpandAll()
            select_all(trvForms.Nodes)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> SelectAllToolStripMenuItem_Click")
        End Try
    End Sub

    Private Sub DeselectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeselectAllToolStripMenuItem.Click
        Try
            trvForms.CollapseAll()
            deselect_all(trvForms.Nodes)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> DeselectAllToolStripMenuItem_Click")
        End Try

    End Sub

    Private Sub select_all(ByVal n As TreeNodeCollection)
        Try
            For Each n1 As TreeNode In n
                n1.Checked = True
                If n1.Nodes.Count() > 0 Then
                    select_all(n1.Nodes)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

End Class
