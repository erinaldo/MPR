Imports MMSPlus

Public Class frm_EwayBill
    Implements IForm

    Dim obj As New CommonClass

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_EwayBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fill_grid(dpBillDate.Value)
    End Sub

    Private Sub fill_grid(Optional ByVal condition As String = "")
        Try

            Dim strsql As String

            strsql = "SELECT *, CAST(0 as bit) as Action FROM (SELECT SI_ID," &
            "(SI_CODE+CAST(SI_NO AS VARCHAR)) AS InvNo,('DC/'+CAST(DC_GST_NO AS VARCHAR) ) AS [DC NO]," &
            " dbo.fn_Format(dbo.SALE_INVOICE_MASTER.CREATION_DATE) AS [INV DATE]," &
            " NET_AMOUNT AS Amount,ACC_NAME Customer,CASE WHEN INVOICE_STATUS =1 THEN 'Fresh'  WHEN INVOICE_STATUS =2 THEN 'Pending' WHEN INVOICE_STATUS =3 THEN 'Clear'  WHEN INVOICE_STATUS =4 THEN 'Cancel' END AS Status FROM dbo.SALE_INVOICE_MASTER " &
            "JOIN dbo.ACCOUNT_MASTER ON ACCOUNT_MASTER.ACC_ID=dbo.SALE_INVOICE_MASTER.CUST_ID WHERE INVOICE_STATUS <> 4 and FLAG=0)tb " &
            "WHERE  (CAST(SI_ID AS varchar) ='" & condition & "'  order by 1"

            Dim dt As DataTable = obj.Fill_DataSet(strsql).Tables(0)

            flxList.DataSource = dt

            flxList.Columns(0).Width = 40
            flxList.Columns(1).Width = 130
            flxList.Columns(2).Width = 100
            flxList.Columns(3).Width = 80
            flxList.Columns(4).Width = 100
            flxList.Columns(5).Width = 250
            flxList.Columns(6).Width = 70

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Public Sub NewClick(sender As Object, e As EventArgs) Implements IForm.NewClick
        frm_EwayBill_Load(Nothing, Nothing)
    End Sub

    Public Sub SaveClick(sender As Object, e As EventArgs) Implements IForm.SaveClick

    End Sub

    Public Sub CloseClick(sender As Object, e As EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(sender As Object, e As EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub ViewClick(sender As Object, e As EventArgs) Implements IForm.ViewClick

    End Sub

    Public Sub RefreshClick(sender As Object, e As EventArgs) Implements IForm.RefreshClick
        frm_EwayBill_Load(Nothing, Nothing)
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim selectedInvIds As New List(Of Int32)
        For Each row As DataGridViewRow In flxList.Rows
            If row.Cells(7).Value Then
                selectedInvIds.Add(row.Cells(0).Value)
            End If
        Next
        If selectedInvIds.Count > 0 Then
            Dim eWayBill As New EWayBill
            Dim jsonResult As String = eWayBill.GetEWayBillInJson(selectedInvIds)
            Dim svd As New SaveFileDialog
            svd.DefaultExt = "txt"
            svd.Filter =
            "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            svd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If svd.ShowDialog() = DialogResult.OK Then
                Dim fileStream As IO.FileStream = svd.OpenFile()
                Dim byteData() As Byte
                byteData = System.Text.Encoding.ASCII.GetBytes(jsonResult)

                fileStream.Write(byteData, 0, byteData.Length)
                fileStream.Close()
            End If
        End If

    End Sub
End Class
