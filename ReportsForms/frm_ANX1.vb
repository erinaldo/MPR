Imports Microsoft.Office.Interop
Imports MMSPlus

Public Class frm_ANX1
    Implements IForm
    Dim objCommFunction As New CommonClass
    Private stateId As String
    Private IsInUT As String
    Dim Qry As String
    Dim _rights As Form_Rights


    Dim b2cTableANX1 As DataTable
    Dim b2bTableANX1 As DataTable
    Dim RCMTableANX1 As DataTable
    Dim EcomTableANX1 As DataTable

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_GSTR_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stateId = objCommFunction.ExecuteScalar("SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT TOP 1 CITY_ID FROM dbo.DIVISION_SETTINGS)")
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        BindDataANX1()
        ImportDataANX1()

    End Sub


    Private m_colIndex As Integer
    Public Property colIndex As Integer
        Get
            m_colIndex += 1
            Return m_colIndex
        End Get
        Set(value As Integer)
            m_colIndex = value
        End Set
    End Property

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub



    Private Sub BindDataANX1()

        Qry = " SELECT  STATE_CODE ,
        STATE_NAME ,
        DiffTaxRate ,
        IGST_Act ,
        VAT_PER ,
        SUM(Taxable_Value) AS Taxable_Value ,
        SUM(non_integrated_tax) AS non_integrated_tax ,
        SUM(integrated_tax) AS integrated_tax ,
        SUM(Cess_Amount) AS Cess_Amount ,
        STATE_CODE
FROM    ( SELECT    STATE_CODE ,
                    STATE_NAME ,
                    DiffTaxRate ,
                    IGST_Act ,
                    VAT_PER ,
                    Taxable_Value ,
                    non_integrated_tax ,
                    integrated_tax ,
                    Cess_Amount ,
                    OrderDate
          FROM      VW_ANX1_B2CSTable
          WHERE     CAST(OrderDate AS DATE) BETWEEN CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
                                            AND     CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
        ) tb
GROUP BY STATE_CODE ,
        STATE_NAME ,
        DiffTaxRate ,
        IGST_Act ,
        VAT_PER	ORDER BY tb.STATE_CODE"

        b2cTableANX1 = objCommFunction.Fill_DataSet(Qry).Tables(0)


        Qry = "SELECT  VAT_NO ,
        ACC_NAME ,
        DocumentType ,
        DocumentNo ,
        OrderDate ,
      max(Net_amount) AS Net_amount ,
        STATE_CODE ,
        STATE_NAME ,
        DiffTaxRate ,
        IGST_Act ,
        HsnCode_vch AS HSN ,
        VAT_PER ,
        SUM(Taxable_Value) AS Taxable_Value ,
        SUM(non_integrated_tax) AS non_integrated_tax ,
        SUM(integrated_tax) AS integrated_tax ,
        SUM(Cess_Amount) AS Cess_Amount ,
        STATE_CODE,
  MAX(Net_amount) AS Net_amount
FROM    ( SELECT    VAT_NO ,
                    ACC_NAME ,
                    DocumentType ,
                    DocumentNo ,
                    OrderDate ,
                    STATE_CODE ,
                    STATE_NAME ,                  
                    DiffTaxRate ,
                    IGST_Act ,
                    HsnCode_vch ,
                    VAT_PER ,
                    Taxable_Value ,
                    non_integrated_tax ,
                    integrated_tax ,
                    Cess_Amount,
                    Net_amount
          FROM      VW_ANX1_B2BTable
          WHERE     CAST(OrderDate AS DATE) BETWEEN CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
                                            AND     CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
        ) tb
GROUP BY STATE_CODE ,
        STATE_NAME ,
        DiffTaxRate ,
        IGST_Act ,
        VAT_PER ,
        VAT_NO ,
        ACC_NAME ,
        DocumentType ,
        DocumentNo ,
        tb.HsnCode_vch ,
        OrderDate
ORDER BY tb.OrderDate "
        b2bTableANX1 = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = "SELECT  *
FROM    VW_ANX1_SectionRCMD
WHERE   CAST(Received_Date AS DATE) BETWEEN CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
                                    AND     CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)"
        RCMTableANX1 = objCommFunction.Fill_DataSet(Qry).Tables(0)

    End Sub

    Private Sub ImportDataANX1()
        Dim path As String = Application.StartupPath + "\ExcelTemplate\ANX1_Template.xlsx"

        Dim sfd As New SaveFileDialog
        sfd.CheckFileExists = False
        If sfd.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim xlApp As Object
        Dim xlWorkBook As Object

        xlApp = CreateObject("Excel.Application")

        xlWorkBook = xlApp.Workbooks.Open(path)

        Try
            WriteAnxB2CData(xlWorkBook)
            WriteB2BAnxData(xlWorkBook)
            WriteRCMAnxData(xlWorkBook)
            xlWorkBook.SaveAs(sfd.FileName)
        Finally
            xlWorkBook.Close(SaveChanges:=False)
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            MsgBox("ANX 1 exported successfully", MsgBoxStyle.Information, "POS PLUS")
        End Try
    End Sub
    Private Sub WriteAnxB2CData(xlWorkBook As Object)
        Dim rowIndex As Int32 = 3
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("B2C")
        For Each row As DataRow In b2cTableANX1.Rows
            Dim state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
            xlWorkSheet.Cells(rowIndex, 1) = state
            xlWorkSheet.Cells(rowIndex, 2) = row("DiffTaxRate")
            xlWorkSheet.Cells(rowIndex, 3) = row("IGST_Act")
            xlWorkSheet.Cells(rowIndex, 4) = row("VAT_PER")
            xlWorkSheet.Cells(rowIndex, 5) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 6) = row("integrated_tax")
            xlWorkSheet.Cells(rowIndex, 7) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 8) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 9) = row("Cess_Amount")
            rowIndex += 1
        Next
    End Sub

    Private Sub WriteB2BAnxData(xlWorkBook As Object)
        If b2bTableANX1.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 3
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("B2B")
        For Each row As DataRow In b2bTableANX1.Rows
            Dim state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
            Dim invoiceNo As String = row("DocumentNo")
            xlWorkSheet.Cells(rowIndex, 1) = row("VAT_NO")
            xlWorkSheet.Cells(rowIndex, 2) = row("ACC_NAME")
            xlWorkSheet.Cells(rowIndex, 3) = row("DocumentType")
            xlWorkSheet.Cells(rowIndex, 4) = row("DocumentNo")
            xlWorkSheet.Cells(rowIndex, 5) = Convert.ToDateTime(row("OrderDate")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, 6) = row("NET_AMOUNT")
            xlWorkSheet.Cells(rowIndex, 7) = state
            xlWorkSheet.Cells(rowIndex, 8) = row("DiffTaxRate")
            xlWorkSheet.Cells(rowIndex, 9) = row("IGST_Act")
            xlWorkSheet.Cells(rowIndex, 10) = row("HSN")
            xlWorkSheet.Cells(rowIndex, 11) = row("VAT_PER")
            xlWorkSheet.Cells(rowIndex, 12) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 13) = row("integrated_tax")
            xlWorkSheet.Cells(rowIndex, 14) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 15) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 16) = row("Cess_Amount")

            rowIndex += 1
        Next
    End Sub

    Private Sub WriteRCMAnxData(xlWorkBook As Object)
        If RCMTableANX1.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 3
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("REV")
        For Each row As DataRow In RCMTableANX1.Rows
            Dim state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
            xlWorkSheet.Cells(rowIndex, 1) = row("VAT_NO")
            xlWorkSheet.Cells(rowIndex, 2) = row("ACC_NAME")
            xlWorkSheet.Cells(rowIndex, 3) = state
            xlWorkSheet.Cells(rowIndex, 4) = row("DiffTaxRate")
            xlWorkSheet.Cells(rowIndex, 5) = row("IGST_Act")
            xlWorkSheet.Cells(rowIndex, 6) = row("SuplyType")
            xlWorkSheet.Cells(rowIndex, 7) = row("HSN")
            xlWorkSheet.Cells(rowIndex, 8) = row("VAT_PER")
            xlWorkSheet.Cells(rowIndex, 9) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 10) = row("integrated_tax")
            xlWorkSheet.Cells(rowIndex, 11) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 12) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 13) = row("Cess_Amount")
            rowIndex += 1
        Next
    End Sub


    Public Sub NewClick(sender As Object, e As EventArgs) Implements IForm.NewClick
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
    End Sub

End Class
