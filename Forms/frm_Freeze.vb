Imports System.Data.SqlClient

Public Class frm_Freeze
    Implements IForm

    Dim obj As New CommonClass
    Dim outlet_id As Integer
    Dim GlobalTables As DataSet
    Dim con As SqlConnection
    Dim cmd As SqlCommand

    'Protected con As SqlConnection
    'Protected cmd As SqlCommand

    Dim clsCommonClass As New CommonClass

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
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

    Private Sub frm_Synchronization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lbllastFreezeDate.Text = "Freeze Date: " & FreezeDate.ToString("dd-MMM-yyyy")

    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()

    End Sub

    Private Sub btnFreeze_Click(sender As Object, e As EventArgs) Handles btnFreeze.Click

        Try
            obj.ExecuteNonQuery("INSERT INTO TempFD VALUES('" & dtp_Date.Value.ToString("MM/dd/yyyy") & "','" & v_the_current_logged_in_user_name & "',GETDATE())")

            FreezeDate = dtp_Date.Value

            lbllastFreezeDate.Text = "Freeze Date: " & FreezeDate.ToString("dd-MMM-yyyy")

            MsgBox("System Freeze done sucessfully !!")
        Catch ex As Exception
            MsgBox("Error: Please try again.")
        End Try

    End Sub

End Class
