Imports Microsoft.Office.Interop
Imports MMSPlus
Imports System.Linq
Imports System.Data.DataSetExtensions
Public Class frm_Stock_Analysis
    Implements IForm
    Dim objCommFunction As New CommonClass

    Dim Query As String
    Dim _rights As Form_Rights
    Dim dtBrand As DataTable
    Dim dtColor As DataTable
    Dim dtSize As DataTable
    Dim dtCompany As DataTable
    Dim dtDepartment As DataTable
    Dim dtType As DataTable
    Dim dtCategory As DataTable
    Dim dt As DataTable

    Dim allBrandFlag As Boolean
    Dim BrandIds As String = ""
    Dim allColorFlag As Boolean
    Dim ColorIds As String = ""
    Dim allSizeFlag As Boolean
    Dim SizeIds As String = ""
    Dim allCompanyFlag As Boolean
    Dim CompanyIds As String = ""
    Dim allDepartmentFlag As Boolean
    Dim DepartmentIds As String = ""
    Dim allTypeFlag As Boolean
    Dim TypeIds As String = ""
    Dim allCategoryFlag As Boolean
    Dim CategoryIds As String = ""
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub
    Dim itemId As Integer = 0
    Private Sub txtSearchedItem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchedItem.KeyDown

        If e.KeyCode = Keys.Enter Then
            Dim dr() As DataRow = itemTable.Select("Name = '" & txtSearchedItem.Text.Replace("'", "''") & "'")
            If dr.Length > 0 Then
                itemId = dr(0)("Id")
                'SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub BindData()

        Dim Filter As String = ""
        Cursor.Current = Cursors.WaitCursor

        Dim FROMDATE As String = dtpFromDate.Value.ToString("dd-MMM-yyyy")
        Dim TODATE As String = dtpToDate.Value.ToString("dd-MMM-yyyy")

        If BrandIds <> "" Then
            BrandIds = BrandIds.Substring(0, BrandIds.Length() - 1)
            If BrandIds.Length > 0 Then
                Filter = Filter & " AND BrandId IN(" & BrandIds & ") "
            End If
            BrandIds = BrandIds + ","
        End If

        If ColorIds <> "" Then
            ColorIds = ColorIds.Substring(0, ColorIds.Length() - 1)
            If ColorIds.Length > 0 Then
                Filter = Filter & " AND ColorId IN(" & ColorIds & ") "
            End If
            ColorIds = ColorIds + ","
        End If

        If SizeIds <> "" Then
            SizeIds = SizeIds.Substring(0, SizeIds.Length() - 1)
            If SizeIds.Length > 0 Then
                Filter = Filter & " AND SizeId IN(" & SizeIds & ") "
            End If
            SizeIds = SizeIds + ","
        End If

        If CompanyIds <> "" Then
            CompanyIds = CompanyIds.Substring(0, CompanyIds.Length() - 1)
            If CompanyIds.Length > 0 Then
                Filter = Filter & " AND CompanyId IN(" & CompanyIds & ") "
            End If
            CompanyIds = CompanyIds + ","
        End If

        If DepartmentIds <> "" Then
            DepartmentIds = DepartmentIds.Substring(0, DepartmentIds.Length() - 1)
            If DepartmentIds.Length > 0 Then
                Filter = Filter & " AND DepartmentId IN(" & DepartmentIds & ") "
            End If
            DepartmentIds = DepartmentIds + ","
        End If

        If TypeIds <> "" Then
            TypeIds = TypeIds.Substring(0, TypeIds.Length() - 1)
            If TypeIds.Length > 0 Then
                Filter = Filter & " AND TypeId IN(" & TypeIds & ") "
            End If
            TypeIds = TypeIds + ","
        End If

        If CategoryIds <> "" Then
            CategoryIds = CategoryIds.Substring(0, CategoryIds.Length() - 1)
            If CategoryIds.Length > 0 Then
                Filter = Filter & " AND fk_CategoryID_num IN(" & CategoryIds & ") "
            End If
            CategoryIds = CategoryIds + ","
        End If

        If Not String.IsNullOrEmpty(txtSearchedItem.Text) Then
            If itemId <> 0 Then
                Filter = Filter & " AND pk_ItemID_num IN(" & itemId & ") "
            End If
        End If



        If rbItemWise.Checked = True Then
            Query = "SELECT ROW_NUMBER() OVER ( ORDER BY tb.ItemName_vch ) AS SrNo , tb.BarCode_vch AS Barcode ,
        tb.ItemName_vch AS Item ,
        tb.UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(tb.pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(tb.pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(tb.pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(tb.pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(tb.pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,UM_Name,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
        GROUP BY pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
        ORDER BY ItemName_vch"

        End If

        If rbCategoryWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY CategoryName_vch, ItemName_vch ) AS SrNo ,
        CategoryName_vch AS Category ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,        
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    CategoryName_vch ,
                    fk_CategoryID_num ,
                    UM_ID ,
                    um_name ,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
       
GROUP BY fk_CategoryID_num ,
        CategoryName_vch ,
        pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
ORDER BY CategoryName_vch ,
        ItemName_vch"

        End If

        If rbBrandWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY BrandName, ItemName_vch ) AS SrNo ,
        BrandName AS Brand ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,
                    um_name ,
                    BrandId ,
                    BrandName ,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
       
GROUP BY tb.BrandId ,
        tb.BrandName ,
        pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
ORDER BY BrandName ,
        ItemName_vch "

        End If

        If rbColorWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY ColorName, ItemName_vch ) AS SrNo ,
        ColorName AS Color ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,
                    UM_Name ,
                    ColorId ,
                    ColorName ,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
GROUP BY tb.ColorId ,
        tb.ColorName ,
        pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
ORDER BY ColorName ,
        ItemName_vch"

        End If

        If rbSizeWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY SizeName, ItemName_vch ) AS SrNo ,
        SizeName AS Size ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,
                    UM_Name ,
                    SizeId ,
                    SizeName ,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb 
GROUP BY tb.SizeId ,
       tb.SizeName ,
        pk_ItemID_num ,
        BarCode_vch ,
       ItemName_vch ,
        UM_Name
ORDER BY tb.SizeName ,
        ItemName_vch"

        End If

        If rbCompanyWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY CompanyName, ItemName_vch ) AS SrNo ,
        CompanyName AS Company ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,
                    UM_Name ,
                    CompanyId ,
                    CompanyName ,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
GROUP BY CompanyId ,
        CompanyName ,
        pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
ORDER BY CompanyName ,
        ItemName_vch"

        End If

        If rbDepartmentWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY DepartmentName, ItemName_vch ) AS SrNo ,
        DepartmentName AS Department ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,
                    UM_Name ,
                    DepartmentId ,
                    DepartmentName ,
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
GROUP BY DepartmentId ,
        DepartmentName ,
        pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
ORDER BY DepartmentName ,
        ItemName_vch"

        End If

        If rbTypeWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY TypeName, ItemName_vch ) AS SrNo ,
        TypeName AS Type ,
        BarCode_vch AS Barcode ,
        ItemName_vch AS Item ,
        UM_Name AS UOM ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format(DATEADD(day, -1,
                                                            '" & FROMDATE & "')),1) AS NUMERIC(18,
                                                              2)) AS Opening ,
        CAST(ISNULL(SUM(Purchase), 0) AS NUMERIC(18, 2)) AS Purchase ,
        CAST(ISNULL(SUM([Purc.Return]), 0) AS NUMERIC(18, 2)) AS [Purc.Return] ,
        CAST(ISNULL(SUM(Received), 0) AS NUMERIC(18, 2)) AS Received ,
        CAST(ISNULL(SUM(Transferred), 0) AS NUMERIC(18, 2)) AS Transferred ,
        CAST(ISNULL(SUM(sale), 0) AS NUMERIC(18, 2)) AS Sale ,
        CAST(ISNULL(SUM([Sale.Return]), 0) AS NUMERIC(18, 2)) AS [Sale.Return] ,
        CAST(ISNULL(SUM(Adjustment), 0) AS NUMERIC(18, 2)) AS Adjustment ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1) AS NUMERIC(18,
                                                              2)) AS Closing ,
        CAST(dbo.Get_Stock_as_on_date(pk_ItemID_num," & v_the_current_division_id & ",
                                      dbo.fn_Format('" & TODATE & "'),1)
        * CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num, '" & TODATE & "',
                                               0, 1) AS DECIMAL(18, 2)) AS NUMERIC(18,
                                                              2)) AS ClosingValue ,
        CAST(CAST(dbo.Get_Average_Rate_as_on_date(pk_ItemID_num,
                                                  '" & TODATE & "', 0, 1) AS DECIMAL(18,
                                                              2)) AS NUMERIC(18,
                                                              2)) AS [Avg.Rate]
FROM    ( SELECT    pk_ItemID_num ,
                    BarCode_vch ,
                    ItemName_vch ,
                    UM_ID ,
                    UM_Name ,
                    TypeId ,
                    TypeName ,   
                    ( ISNULL(CASE WHEN TRANSACTION_TYPE = 'Purc' THEN Received
                                  ELSE 0
                             END, 0) ) AS Purchase ,
                    CASE WHEN TRANSACTION_TYPE = 'DrNt'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS [Purc.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'StkRc'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS Received ,
                    CASE WHEN TRANSACTION_TYPE = 'StkTr'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Transferred ,
                    CASE WHEN TRANSACTION_TYPE = 'Sale'
                         THEN ( ISNULL(Issue, 0) )
                         ELSE 0
                    END AS Sale ,
                    CASE WHEN TRANSACTION_TYPE = 'CrNt'
                         THEN ( ISNULL(Received, 0) )
                         ELSE 0
                    END AS [Sale.Return] ,
                    CASE WHEN TRANSACTION_TYPE = 'Adj'
                         THEN ( ISNULL(Received, 0) - ( ISNULL(Issue, 0) ) )
                         ELSE 0
                    END AS Adjustment
          FROM      dbo.Item_Analysis
          WHERE     CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME) " & Filter &
        ") tb
GROUP BY TypeId ,
        TypeName ,
        pk_ItemID_num ,
        BarCode_vch ,
        ItemName_vch ,
        UM_Name
       ORDER BY TypeName ,
        ItemName_vch"

        End If

        dt = objCommFunction.FillDataSet(Query).Tables(0)
        dgvReport.Columns.Clear()
        dgvReport.DataSource = dt

        If dt.Rows.Count > 0 Then

            Label33.Visible = True
            txtSearch.Visible = True

            If rbItemWise.Checked = True Then

                If cbxSingleLedger.Checked = True Then

                    If String.IsNullOrEmpty(txtSearchedItem.Text.Trim) Then
                        MessageBox.Show("Must select Item to view Single tranaction ledger.")
                        Exit Sub
                    End If

                    Query = "SELECT  CONVERT(VARCHAR(20), ROW_NUMBER() OVER ( ORDER BY TRANSACTION_DATE )) AS SrNo ,
        Transaction_No AS [Trans./Bill No] ,
        CONVERT(VARCHAR(20), TRANSACTION_DATE, 106) AS [Date] ,
        TRANSACTION_TYPE AS [Trans.Type] ,
        ACC_NAME AS [Account] ,
        CONVERT(VARCHAR(30), CAST(ISNULL(Received, 0) AS NUMERIC(18, 2))) AS [Qty.In] ,
        CONVERT(VARCHAR(30), CAST(ISNULL(Issue, 0) AS NUMERIC(18, 2))) AS [Qty.Out] ,
        CONVERT(VARCHAR(30), CAST(ISNULL(BasePrice, 0) AS NUMERIC(18, 2))) [Base Price] ,
        CONVERT(VARCHAR(30), CAST(ABS(ISNULL(Received, 0) - ISNULL(Issue, 0))
        * ISNULL(BasePrice, 0) AS NUMERIC(18, 2))) AS Amount
INTO    #dt
FROM    dbo.Item_Analysis
WHERE   pk_ItemID_num = " & itemId & " AND CAST(TRANSACTION_DATE AS DATETIME) BETWEEN CAST('" & FROMDATE & "' AS DATETIME)
                                                       AND    CAST('" & TODATE & "' AS DATETIME)
ORDER BY TRANSACTION_DATE

SELECT  '' ,
        'From: " & FROMDATE & "  To: " & TODATE & "' ,
        '' ,
        '' ,
        'ITEM LEDGER' ,
        '' ,
        '' ,
        '' ,
        ''
        UNION ALL
SELECT TOP ( 1 )
        '' ,
        ItemName_vch ,
        '' ,
        '' ,
        'Opening' ,
        'Qty. :' ,
        CONVERT(VARCHAR(30), CAST(ISNULL(dbo.Get_Stock_as_on_date(" & itemId & "," & v_the_current_division_id & ",
                                                              dbo.fn_Format(DATEADD(day,
                                                              -1, '" & FROMDATE & "')),1),
                                         0) AS NUMERIC(18, 2))) ,
        'Amt. :' ,
        CONVERT(VARCHAR(30), CAST(( ISNULL(dbo.Get_Stock_as_on_date(" & itemId & "," & v_the_current_division_id & ",
                                                              dbo.fn_Format(DATEADD(day,
                                                              -1, '" & FROMDATE & "')),1),
                                           0)
                                    * ISNULL(dbo.Get_Average_Rate_as_on_date(" & itemId & ",
                                                              '" & FROMDATE & "', 0,
                                                              1), 0) ) AS NUMERIC(18,
                                                              2)))
FROM    dbo.Item_Analysis
WHERE   pk_ItemID_num = " & itemId & "
UNION ALL
SELECT  '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        ''
UNION ALL
SELECT  'SrNo.' ,
        'Trans./Bill No' ,
        'Date' ,
        'Trn.Type' ,
        'Account' ,
        'Qty.In' ,
        'Qty.Out' ,
        'BasePrice' ,
        'Amount'
UNION ALL
SELECT  *
FROM    #dt
UNION ALL
SELECT  '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        '' ,
        ''
UNION ALL
SELECT TOP ( 1 )
        '' ,
        '' ,
        '' ,
        '' ,
        'Closing' ,
        'Qty. :' ,
        CONVERT(VARCHAR(30), CAST(ISNULL(dbo.Get_Stock_as_on_date(" & itemId & "," & v_the_current_division_id & ",
                                                              dbo.fn_Format('" & TODATE & "'),1),
                                         0) AS NUMERIC(18, 2))) ,
        'Amt. :' ,
        CONVERT(VARCHAR(30), CAST(( ISNULL(dbo.Get_Stock_as_on_date(" & itemId & "," & v_the_current_division_id & ",
                                                              dbo.fn_Format('" & TODATE & "'),1),
                                           0)
                                    * ISNULL(dbo.Get_Average_Rate_as_on_date(" & itemId & ",
                                                              '" & TODATE & "', 0,
                                                              1), 0) ) AS NUMERIC(18,
                                                              2)))
FROM    dbo.Item_Analysis
WHERE   pk_ItemID_num = " & itemId & "

DROP TABLE #dt"

                    dt = objCommFunction.FillDataSet(Query).Tables(0)

                    dgvReport.Columns.Clear()
                    dgvReport.ColumnHeadersVisible = False
                    dgvReport.DataSource = dt

                    dgvReport.Columns(0).Width = 40
                    dgvReport.Columns(1).Width = 230
                    dgvReport.Columns(2).Width = 80
                    dgvReport.Columns(3).Width = 60
                    dgvReport.Columns(4).Width = 200
                    dgvReport.Columns(5).Width = 50
                    dgvReport.Columns(6).Width = 52
                    dgvReport.Columns(7).Width = 70
                    dgvReport.Columns(8).Width = 85

                    dgvReport.AlternatingRowsDefaultCellStyle.BackColor = dgvReport.BackgroundColor
                    dgvReport.RowsDefaultCellStyle.BackColor = dgvReport.BackgroundColor

                    dgvReport.Rows(1).DefaultCellStyle.Font = New Font("Tahoma", 8.2, FontStyle.Bold)
                    dgvReport.Rows(1).DefaultCellStyle.ForeColor = Color.Lime

                    dgvReport.Rows(3).DefaultCellStyle.Font = New Font("Tahoma", 8.2, FontStyle.Bold)
                    ' dgvReport.Rows(2).DefaultCellStyle.ForeColor = Color.DarkOrange
                    dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.Font = New Font("Tahoma", 8.2, FontStyle.Bold)
                    dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Lime

                    dgvReport.GridColor = Color.Silver
                    dgvReport.BorderStyle = BorderStyle.FixedSingle
                    Label33.Visible = False
                    txtSearch.Visible = False

                Else

                    Dim TotalOpnQty As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Opening")))
                    Dim TotalClsQty As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Closing")))
                    Dim TotalPurchase As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Purchase")))

                    Dim TotalPurchaseReturn As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Purc.Return"))))
                    Dim TotalReceived As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Received"))))
                    Dim TotalTransferred As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Transferred"))))

                    Dim TotalSale As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Sale"))))
                    Dim TotalSaleReturn As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Sale.Return"))))
                    Dim TotalAdjustment As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Adjustment"))))
                    Dim TotalClosingValue As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("ClosingValue"))))


                    Dim footerRow As DataRow
                    footerRow = dt.NewRow
                    footerRow(2) = "Total"
                    footerRow(4) = TotalOpnQty
                    footerRow(5) = TotalPurchase
                    footerRow(6) = TotalPurchaseReturn
                    footerRow(7) = TotalReceived
                    footerRow(8) = TotalTransferred
                    footerRow(9) = TotalSale
                    footerRow(10) = TotalSaleReturn
                    footerRow(11) = TotalAdjustment
                    footerRow(12) = TotalClsQty
                    footerRow(13) = TotalClosingValue

                    dt.Rows.Add(footerRow)

                    dgvReport.Columns(0).Width = 50
                    dgvReport.Columns(1).Width = 100
                    dgvReport.Columns(2).Width = 280
                    dgvReport.Columns(3).Width = 40
                    dgvReport.Columns(4).Width = 80
                    dgvReport.Columns(5).Width = 80
                    dgvReport.Columns(6).Width = 80
                    dgvReport.Columns(7).Width = 80
                    dgvReport.Columns(8).Width = 80
                    dgvReport.Columns(9).Width = 80
                    dgvReport.Columns(10).Width = 80
                    dgvReport.Columns(11).Width = 80

                    dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Lime
                    dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)
                End If

            Else

                Dim TotalOpnQty As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Opening")))
                Dim TotalClsQty As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Closing")))
                Dim TotalPurchase As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Purchase")))

                Dim TotalPurchaseReturn As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Purc.Return"))))
                Dim TotalReceived As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Received"))))
                Dim TotalTransferred As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Transferred"))))

                Dim TotalSale As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Sale"))))
                Dim TotalSaleReturn As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Sale.Return"))))
                Dim TotalAdjustment As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Adjustment"))))
                Dim TotalClosingValue As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("ClosingValue"))))



                Dim footerRow As DataRow
                footerRow = dt.NewRow
                footerRow(1) = "Total"
                footerRow(5) = TotalOpnQty
                footerRow(6) = TotalPurchase
                footerRow(7) = TotalPurchaseReturn
                footerRow(8) = TotalReceived
                footerRow(9) = TotalTransferred
                footerRow(10) = TotalSale
                footerRow(11) = TotalSaleReturn
                footerRow(12) = TotalAdjustment
                footerRow(13) = TotalClsQty
                footerRow(14) = TotalClosingValue

                dt.Rows.Add(footerRow)

                dgvReport.Columns(0).Width = 50
                dgvReport.Columns(1).Width = 150
                dgvReport.Columns(2).Width = 80
                dgvReport.Columns(3).Width = 280
                dgvReport.Columns(4).Width = 40
                dgvReport.Columns(5).Width = 80
                dgvReport.Columns(6).Width = 80
                dgvReport.Columns(7).Width = 80
                dgvReport.Columns(8).Width = 80
                dgvReport.Columns(9).Width = 80
                dgvReport.Columns(10).Width = 80
                dgvReport.Columns(11).Width = 80

                dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Lime
                dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)

            End If

        End If

        pnlShow.Visible = True
        pnlShow.BringToFront()

        Cursor.Current = Cursors.Default


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

    Private Sub frm_Sale_Analysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'pdsTaxReportDates.FromDateDTP = dtpFromDate
        'pdsTaxReportDates.ToDateDTP = dtpToDate
        FillBrand()
        FillColor()
        FillSize()
        FillCompany()
        FillDepartment()
        FillType()
        FillCategory()
        BindItemAutoCompleteTextBox()
        dgvReport.Columns.Clear()
    End Sub


    Private Sub FillBrand()
        dtBrand = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As BrandId,LabelItemName_vch As Brand FROM dbo.Label_Items WHERE fk_LabelId_num=1" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtBrand.Rows.Count > 0 Then
            ChkdLB_Brand.DataSource = dtBrand
            ChkdLB_Brand.DisplayMember = "Brand"
            ChkdLB_Brand.ValueMember = "BrandId"
        End If
    End Sub

    Private Sub FillCategory()
        dtCategory = objCommFunction.FillDataSet("SELECT pk_CategoryID_num As CategoryId,CategoryName_vch As Category FROM dbo.CategoryMaster" &
                    " order by CategoryName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtCategory.Rows.Count > 0 Then
            ChkdLB_Category.DataSource = dtCategory
            ChkdLB_Category.DisplayMember = "Category"
            ChkdLB_Category.ValueMember = "CategoryId"
        End If
    End Sub



    Dim itemTable As DataTable = Nothing
    Private Sub BindItemAutoCompleteTextBox()
        Query = " select ITEM_CODE + ' - ' + ITEM_NAME + ' MRP - ' + cast(MRP_num as varchar(20)) + ' - ' + cast(BasePrice_num as varchar(20)) + ' - ' + BarCode_vch  as name, ITEM_ID as Id, 'MM' as Type from ITEM_MASTER ORDER BY ITEM_NAME "
        itemTable = objCommFunction.FillDataSet(Query).Tables(0)
        Try

            Dim MyCollection As AutoCompleteStringCollection = New AutoCompleteStringCollection()

            Dim source As New List(Of String)
            For Each row As DataRow In itemTable.Rows
                source.Add(row("Name"))
                MyCollection.Add(row("Name"))
            Next
            txtSearchedItem.AutoCompleteList = source

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillColor()
        dtColor = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As ColorId,LabelItemName_vch As Color FROM dbo.Label_Items WHERE fk_LabelId_num=3" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtColor.Rows.Count > 0 Then
            ChkdLB_Color.DataSource = dtColor
            ChkdLB_Color.DisplayMember = "Color"
            ChkdLB_Color.ValueMember = "ColorId"
        End If
    End Sub


    Private Sub FillSize()
        dtSize = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As SizeId,LabelItemName_vch As Size FROM dbo.Label_Items WHERE fk_LabelId_num=2" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtSize.Rows.Count > 0 Then
            ChkdLB_Size.DataSource = dtSize
            ChkdLB_Size.DisplayMember = "Size"
            ChkdLB_Size.ValueMember = "SizeId"
        End If
    End Sub

    Private Sub FillCompany()
        dtCompany = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As CompanyId,LabelItemName_vch As Company FROM dbo.Label_Items WHERE fk_LabelId_num=4" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtCompany.Rows.Count > 0 Then
            ChkdLB_Company.DataSource = dtCompany
            ChkdLB_Company.DisplayMember = "Company"
            ChkdLB_Company.ValueMember = "CompanyId"
        End If
    End Sub

    Private Sub FillDepartment()
        dtDepartment = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As DepartmentId,LabelItemName_vch As Department FROM dbo.Label_Items WHERE fk_LabelId_num=5" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtDepartment.Rows.Count > 0 Then
            ChkdLB_Department.DataSource = dtDepartment
            ChkdLB_Department.DisplayMember = "Department"
            ChkdLB_Department.ValueMember = "DepartmentId"
        End If
    End Sub

    Private Sub FillType()
        dtType = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As TypeId,LabelItemName_vch As Type FROM dbo.Label_Items WHERE fk_LabelId_num=6" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtType.Rows.Count > 0 Then
            ChkdLB_Type.DataSource = dtType
            ChkdLB_Type.DisplayMember = "Type"
            ChkdLB_Type.ValueMember = "TypeId"
        End If
    End Sub


    Private Sub btn_AllBrand_Click(sender As Object, e As EventArgs) Handles btn_AllBrand.Click
        Dim u As Integer
        If btn_AllBrand.Text = "Select All" Then
            For u = 0 To ChkdLB_Brand.Items.Count() - 1
                ChkdLB_Brand.SetItemChecked(u, True)
                btn_AllBrand.Text = "UnSelect"
            Next u
            allBrandFlag = True
        ElseIf btn_AllBrand.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Brand.Items.Count() - 1
                ChkdLB_Brand.SetItemChecked(u, False)
                btn_AllBrand.Text = "Select All"
            Next
            allBrandFlag = False
            BrandIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Brand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Brand.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView
        For i = 0 To ChkdLB_Brand.Items.Count - 1
            If ChkdLB_Brand.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Brand.Items(i)
                BrandIds = BrandIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                BrandIds &= "'" & drv(0).ToString().Trim & "',"



            Else
                If BrandIds <> "" Then
                    drv = ChkdLB_Brand.Items(i)
                    BrandIds = BrandIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If

            End If
        Next
    End Sub

    Private Sub btn_AllColor_Click(sender As Object, e As EventArgs) Handles btn_AllColor.Click
        Dim u As Integer
        If btn_AllColor.Text = "Select All" Then
            For u = 0 To ChkdLB_Color.Items.Count() - 1
                ChkdLB_Color.SetItemChecked(u, True)
                btn_AllColor.Text = "UnSelect"
            Next u
            allColorFlag = True
        ElseIf btn_AllColor.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Color.Items.Count() - 1
                ChkdLB_Color.SetItemChecked(u, False)
                btn_AllColor.Text = "Select All"
            Next
            allColorFlag = False
            ColorIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Color_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Color.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Color.Items.Count - 1
            If ChkdLB_Color.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Color.Items(i)
                ColorIds = ColorIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                ColorIds &= "'" & drv(0).ToString().Trim & "',"
            Else
                If ColorIds <> "" Then
                    drv = ChkdLB_Color.Items(i)
                    ColorIds = ColorIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next

    End Sub

    Private Sub btn_AllSize_Click(sender As Object, e As EventArgs) Handles btn_AllSize.Click
        Dim u As Integer
        If btn_AllSize.Text = "Select All" Then
            For u = 0 To ChkdLB_Size.Items.Count() - 1
                ChkdLB_Size.SetItemChecked(u, True)
                btn_AllSize.Text = "UnSelect"
            Next u
            allSizeFlag = True
        ElseIf btn_AllSize.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Size.Items.Count() - 1
                ChkdLB_Size.SetItemChecked(u, False)
                btn_AllSize.Text = "Select All"
            Next
            allSizeFlag = False
            SizeIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Size_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Size.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Size.Items.Count - 1
            If ChkdLB_Size.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Size.Items(i)
                SizeIds = SizeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                SizeIds &= "'" & drv(0).ToString().Trim & "',"

            Else
                If SizeIds <> "" Then
                    drv = ChkdLB_Size.Items(i)
                    SizeIds = SizeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub btn_AllCompany_Click(sender As Object, e As EventArgs) Handles btn_AllCompany.Click
        Dim u As Integer
        If btn_AllCompany.Text = "Select All" Then
            For u = 0 To ChkdLB_Company.Items.Count() - 1
                ChkdLB_Company.SetItemChecked(u, True)
                btn_AllCompany.Text = "UnSelect"
            Next u
            allCompanyFlag = True
        ElseIf btn_AllCompany.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Company.Items.Count() - 1
                ChkdLB_Company.SetItemChecked(u, False)
                btn_AllCompany.Text = "Select All"
            Next
            allCompanyFlag = False
            CompanyIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Company_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Company.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Company.Items.Count - 1
            If ChkdLB_Company.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Company.Items(i)

                CompanyIds = CompanyIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                CompanyIds &= "'" & drv(0).ToString().Trim & "',"


            Else
                If CompanyIds <> "" Then
                    drv = ChkdLB_Company.Items(i)
                    CompanyIds = CompanyIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub btn_AllDepartment_Click(sender As Object, e As EventArgs) Handles btn_AllDepartment.Click
        Dim u As Integer
        If btn_AllDepartment.Text = "Select All" Then
            For u = 0 To ChkdLB_Department.Items.Count() - 1
                ChkdLB_Department.SetItemChecked(u, True)
                btn_AllDepartment.Text = "UnSelect"
            Next u
            allDepartmentFlag = True
        ElseIf btn_AllDepartment.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Department.Items.Count() - 1
                ChkdLB_Department.SetItemChecked(u, False)
                btn_AllDepartment.Text = "Select All"
            Next
            allDepartmentFlag = False
            DepartmentIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Department_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Department.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Department.Items.Count - 1
            If ChkdLB_Department.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Department.Items(i)

                DepartmentIds = DepartmentIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                DepartmentIds &= "'" & drv(0).ToString().Trim & "',"

            Else
                If DepartmentIds <> "" Then
                    drv = ChkdLB_Department.Items(i)
                    DepartmentIds = DepartmentIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub btn_AllType_Click(sender As Object, e As EventArgs) Handles btn_AllType.Click
        Dim u As Integer
        If btn_AllType.Text = "Select All" Then
            For u = 0 To ChkdLB_Type.Items.Count() - 1
                ChkdLB_Type.SetItemChecked(u, True)
                btn_AllType.Text = "UnSelect"
            Next u
            allTypeFlag = True
        ElseIf btn_AllType.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Type.Items.Count() - 1
                ChkdLB_Type.SetItemChecked(u, False)
                btn_AllType.Text = "Select All"
            Next
            allTypeFlag = False
            TypeIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Type.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Type.Items.Count - 1
            If ChkdLB_Type.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Type.Items(i)

                TypeIds = TypeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                TypeIds &= "'" & drv(0).ToString().Trim & "',"

            Else
                If TypeIds <> "" Then
                    drv = ChkdLB_Type.Items(i)
                    TypeIds = TypeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub txtBrand_TextChanged(sender As Object, e As EventArgs) Handles txtBrand.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvBrand As DataView = dtBrand.DefaultView
        dvBrand.RowFilter = String.Format("Brand LIKE '{0}%'", txtBrand.Text)


        If BrandIds <> "" Then
            Dim NewBrandIds As String = BrandIds.Substring(0, BrandIds.Length() - 1)
            Dim CheckedBrandId() As String = NewBrandIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedBrandId
                For i = 0 To ChkdLB_Brand.Items.Count - 1
                    drv = ChkdLB_Brand.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Brand.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If

    End Sub

    Private Sub btn_AllCategory_Click(sender As Object, e As EventArgs) Handles btn_AllCategory.Click
        Dim u As Integer
        If btn_AllCategory.Text = "Select All" Then
            For u = 0 To ChkdLB_Category.Items.Count() - 1
                ChkdLB_Category.SetItemChecked(u, True)
                btn_AllCategory.Text = "UnSelect"
            Next u
            allCategoryFlag = True
        ElseIf btn_AllCategory.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Category.Items.Count() - 1
                ChkdLB_Category.SetItemChecked(u, False)
                btn_AllCategory.Text = "Select All"
            Next
            allCategoryFlag = False
            CategoryIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Category.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Category.Items.Count - 1
            If ChkdLB_Category.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Category.Items(i)

                CategoryIds = CategoryIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                CategoryIds &= "'" & drv(0).ToString().Trim & "',"
            Else
                If CategoryIds <> "" Then
                    drv = ChkdLB_Category.Items(i)
                    CategoryIds = CategoryIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub txtColor_TextChanged(sender As Object, e As EventArgs) Handles txtColor.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvColor As DataView = dtColor.DefaultView
        dvColor.RowFilter = String.Format("Color LIKE '{0}%'", txtColor.Text)


        If ColorIds <> "" Then
            Dim NewColorIds As String = ColorIds.Substring(0, ColorIds.Length() - 1)
            Dim CheckedColorId() As String = NewColorIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedColorId
                For i = 0 To ChkdLB_Color.Items.Count - 1
                    drv = ChkdLB_Color.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Color.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If

    End Sub

    Private Sub txtSize_TextChanged(sender As Object, e As EventArgs) Handles txtSize.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvSize As DataView = dtSize.DefaultView
        dvSize.RowFilter = String.Format("Size LIKE '{0}%'", txtSize.Text)


        If SizeIds <> "" Then
            Dim NewSizeIds As String = SizeIds.Substring(0, SizeIds.Length() - 1)
            Dim CheckedSizeId() As String = NewSizeIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedSizeId
                For i = 0 To ChkdLB_Size.Items.Count - 1
                    drv = ChkdLB_Size.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Size.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub txtCompany_TextChanged(sender As Object, e As EventArgs) Handles txtCompany.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvCompany As DataView = dtCompany.DefaultView
        dvCompany.RowFilter = String.Format("Company LIKE '{0}%'", txtCompany.Text)


        If CompanyIds <> "" Then
            Dim NewCompanyIds As String = CompanyIds.Substring(0, CompanyIds.Length() - 1)
            Dim CheckedCompanyId() As String = NewCompanyIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedCompanyId
                For i = 0 To ChkdLB_Company.Items.Count - 1
                    drv = ChkdLB_Company.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Company.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub txtDepartment_TextChanged(sender As Object, e As EventArgs) Handles txtDepartment.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvDepartment As DataView = dtDepartment.DefaultView
        dvDepartment.RowFilter = String.Format("Department LIKE '{0}%'", txtDepartment.Text)

        If DepartmentIds <> "" Then
            Dim NewDepartmentIds As String = DepartmentIds.Substring(0, DepartmentIds.Length() - 1)
            Dim CheckedDepartmentId() As String = NewDepartmentIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedDepartmentId
                For i = 0 To ChkdLB_Department.Items.Count - 1
                    drv = ChkdLB_Department.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Department.SetItemChecked(i, True)
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub txtType_TextChanged(sender As Object, e As EventArgs) Handles txtType.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvType As DataView = dtType.DefaultView
        dvType.RowFilter = String.Format("Type LIKE '{0}%'", txtType.Text)


        If TypeIds <> "" Then
            Dim NewTypeIds As String = TypeIds.Substring(0, TypeIds.Length() - 1)
            Dim CheckedTypeId() As String = NewTypeIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedTypeId
                For i = 0 To ChkdLB_Type.Items.Count - 1
                    drv = ChkdLB_Type.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Type.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub txtCategory_TextChanged(sender As Object, e As EventArgs) Handles txtCategory.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvCategory As DataView = dtCategory.DefaultView
        dvCategory.RowFilter = String.Format("Category LIKE '{0}%'", txtCategory.Text)


        If CategoryIds <> "" Then
            Dim NewCategoryIds As String = CategoryIds.Substring(0, CategoryIds.Length() - 1)
            Dim CheckedCategoryId() As String = NewCategoryIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedCategoryId
                For i = 0 To ChkdLB_Category.Items.Count - 1
                    drv = ChkdLB_Category.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Category.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If cbxSingleLedger.Checked = True Then
            TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("[Bill Date] LIKE '{0}%' OR [Account Name] LIKE '{0}%' OR [Brand] LIKE '{0}%' OR [Category] LIKE '{0}%' OR [Item] LIKE '{0}%' OR [Color] LIKE '{0}%' OR [Size] LIKE '{0}%' OR [GST No.] LIKE '{0}%' OR [Address] LIKE '{0}%' OR [Bill No.] LIKE '{0}%' OR [BAR Code] LIKE '{0}%' OR [HSN Code] LIKE '{0}%' OR [Make] LIKE '{0}%' OR [Department] LIKE '{0}%' OR [Type] LIKE '{0}%' OR [Mobile No.] LIKE '{0}%'", txtSearch.Text)
        Else
            If rbItemWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbCategoryWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Category LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbBrandWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Brand LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbColorWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Color LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbSizeWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Size LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbCompanyWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Company LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbDepartmentWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Department LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

            If rbTypeWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("Type LIKE '{0}%' OR Item LIKE '{0}%' OR Barcode LIKE '{0}%'", txtSearch.Text)
            End If

        End If
    End Sub

    Private Sub dgvReport_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReport.CellClick
        txtSearch.Focus()
    End Sub

    Private Sub dgvReport_SelectionChanged(sender As Object, e As EventArgs) Handles dgvReport.SelectionChanged
        txtSearch.Focus()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If dgvReport.Rows.Count > 0 Then
            If dgvReport.SelectedRows.Count = 0 Then
                dgvReport.Rows(0).Selected = True
            Else
                Dim selectedRowIndex As Int32 = dgvReport.SelectedRows(0).Index

                If (e.KeyCode = Keys.Up) Then
                    If selectedRowIndex > 0 Then
                        dgvReport.Rows(selectedRowIndex - 1).Selected = True
                        e.Handled = True
                    End If
                End If

                If (e.KeyCode = Keys.Down) Then
                    If selectedRowIndex < dgvReport.Rows.Count - 1 Then
                        dgvReport.Rows(selectedRowIndex + 1).Selected = True
                        e.Handled = True
                    End If
                End If
                selectedRowIndex = dgvReport.SelectedRows(0).Index
                dgvReport.CurrentCell = dgvReport.Rows(selectedRowIndex).Cells(1)
            End If
        End If
    End Sub
    Dim n As Integer = 0

    Private Sub txtMrpFrom_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))

    End Sub

    Private Sub txtHSNcode_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    Private Sub btnShowBack_Click(sender As Object, e As EventArgs) Handles btnShowBack.Click
        pnlShow.Visible = False
        pnlShow.SendToBack()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        objCommFunction.ExportGridToExcel(dgvReport)
        MsgBox("Sale Analysis export successfully.", MsgBoxStyle.Information, gblMessageHeading)
    End Sub

    Private Sub btnLoadReport_Click(sender As Object, e As EventArgs) Handles btnLoadReport.Click
        BindData()
    End Sub


End Class
