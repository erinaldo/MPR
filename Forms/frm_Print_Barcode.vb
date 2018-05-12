Imports MMSPlus.Adjustment_master
Imports System.Data.SqlClient
Imports System.IO

Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Print_Barcode
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim Item_obj As New item_detail.cls_item_detail
    Dim prpty As New item_detail.cls_item_detail_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iAdjustmentId As Int32
    Dim objComm As New CommonClass
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

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        SearchItem()
    End Sub
    Private Sub SearchItem()
        dgvPrintBarcode.DataSource = Nothing

        Dim Query As String
        Query = "SELECT top 250  ROW_NUMBER() OVER ( ORDER BY ITEM_ID ) AS SNO ," &
            " Item_Code ," &
            " Item_Name, BarCode_vch," &
            " MRP_num as MRP ," &
            " sale_rate," &
            " 0 AS [Current Stock]" &
            " From dbo.Item_Master mm" &
            " where 1=1 "

        If Not String.IsNullOrEmpty(txtItemCode.Text.Trim) Then
            Query += " and Item_Code Like '%" & txtItemCode.Text & "%'"
        End If
        If Not String.IsNullOrEmpty(txtItemName.Text.Trim) Then
            Query += " and Item_Name Like '%" & txtItemName.Text & "%'"
        End If
        If Not String.IsNullOrEmpty(txtItemBarcode.Text.Trim) Then
            Query += " and BarCode_vch  Like '%" & txtItemBarcode.Text & "%'"
        End If

        Dim itemDetailTable As DataTable = obj.FillDataSet(Query).Tables(0)

        dgvPrintBarcode.DataSource = Nothing
        dgvPrintBarcode.RowCount = 0
        For Each row As DataRow In itemDetailTable.Rows

            'For Each Grow As DataGridViewRow In dgvPrintBarcode.Rows
            '    If Grow.Cells(1).Value = row("ItemCode_num") Then
            '        MsgBox("Item you have search already exist.", "Item Already Exist!!!")
            '        Exit Sub
            '    End If
            'Next

            Dim index As Int32 = dgvPrintBarcode.RowCount
            dgvPrintBarcode.RowCount += 1
            dgvPrintBarcode.Rows(index).Cells(0).Value = row("SNO")
            dgvPrintBarcode.Rows(index).Cells(1).Value = row("Item_Code")
            dgvPrintBarcode.Rows(index).Cells(2).Value = row("Item_Name")
            dgvPrintBarcode.Rows(index).Cells(3).Value = row("MRP")
            dgvPrintBarcode.Rows(index).Cells(4).Value = row("BarCode_vch")
            dgvPrintBarcode.Rows(index).Cells(5).Value = row("sale_rate")
            dgvPrintBarcode.Rows(index).Cells(6).Value = ""
        Next
    End Sub
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub
    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        'FillGrid()
    End Sub
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub
    Private Sub new_initilization()

    End Sub
    Private Sub dgvPrintBarcode_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrintBarcode.SelectionChanged
        If dgvPrintBarcode.SelectedRows.Count > 0 Then
            Dim cell As DataGridViewCell = dgvPrintBarcode.SelectedRows(0).Cells(6)
            dgvPrintBarcode.CurrentCell = cell
            dgvPrintBarcode.BeginEdit(True)
        End If
    End Sub

    Private Sub dgvPrintBarcode_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvPrintBarcode.RowPrePaint
        dgvPrintBarcode.Rows(e.RowIndex).Cells(7).Value = "Print Barcode"
    End Sub
    Private Sub dgvPrintBarcode_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPrintBarcode.CellContentClick
        If e.RowIndex >= 0 And e.ColumnIndex = 7 Then
            PrintBarcode(e.RowIndex)
            dgvPrintBarcode.Rows(e.RowIndex).Cells(6).Value = ""
        End If
    End Sub
    Private Sub PrintBarcode(RowIndex As Int32)
        Dim itemcode As String = dgvPrintBarcode.Rows(RowIndex).Cells(1).Value
        Dim Query As String

        Query = " SELECT TOP(1) Item_Name," &
            " MRP_num ," &
            " sale_rate ," &
            " BarCode_vch" &
            " From dbo.item_master " &
            " where Item_Code = '" & itemcode & "'"

        Dim itemDataTable As DataTable = obj.FillDataSet(Query).Tables(0)
        If itemDataTable.Rows.Count > 0 Then
            Dim noOfCopies As Integer

            If Not String.IsNullOrEmpty(dgvPrintBarcode.Rows(RowIndex).Cells(6).Value) Then
                noOfCopies = dgvPrintBarcode.Rows(RowIndex).Cells(6).Value
            Else
                noOfCopies = 1
            End If

            Dim loopCount As Int32 = (noOfCopies \ 2) + (noOfCopies Mod 2)

            For index As Integer = 1 To loopCount
                Dim template As String = "\BarcodeTemplate_double.prn"

                If index = loopCount And (noOfCopies Mod 2) = 1 Then
                    template = "\BarcodeTemplate_single.prn"
                End If

                Dim path As String = Application.StartupPath & template
                Dim fs As String = File.ReadAllText(path)
                Dim dr As DataRow = itemDataTable.Rows(0)
                'Dim barcode As String = dr("BarCode_vch")
                'If (barcode.Length Mod 2) <> 0 Then
                '    barcode = barcode.Insert(barcode.Length - 1, "!100")
                'End If


                Dim length As Int32 = dr("Item_Name").ToString().Length
                If length > 27 Then
                    length = 27
                Else
                    length = length
                End If
                ''fill values in parameters in template file
                ''fs = fs.Replace("!105111-barcode-111", String.Format("!105{0}", barcode))
                fs = fs.Replace("Barcode", dr("BarCode_vch"))
                fs = fs.Replace("Company Name", "")

                fs = fs.Replace("Item Name", dr("Item_Name").ToString().Substring(0, length) + "..")
                Dim rateInfo As String = "MRP " & dr("MRP_num").ToString()
                If chkPrintOurPrice.Checked Then
                    rateInfo += "  Our Price " & dr("sale_rate")
                End If

                fs = fs.Replace("Rate Info", rateInfo)

                ''write file
                path = Application.StartupPath & String.Format("\Barcode{0}.prn", DateTime.Now.ToString("ddMMyyyyhhmmss"))
                Dim fileStream As FileStream = System.IO.File.Create(path)
                fileStream.Close()

                Dim newfile As System.IO.StreamWriter
                newfile = My.Computer.FileSystem.OpenTextFileWriter(path, False)
                newfile.Write(fs)
                newfile.Close()

                ''print file
                Dim processPrint As New Process
                Dim command As String = "/c copy /b """ & path & """  \\127.0.0.1\tsc"
                Dim printProcess As Process = New Process()
                printProcess.StartInfo.FileName = "cmd"
                printProcess.StartInfo.Arguments = command
                printProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                printProcess.Start()
                printProcess.WaitForExit()

                ''delete file
                File.Delete(path)
            Next
        End If
    End Sub
End Class
