Public Class frm_GSTR_1

    Dim Qry As String
    Dim _rights As Form_Rights


    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_GSTR_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        BindData()
        ImportData()

    End Sub

    Dim b2bTable As DataTable
    Dim b2clTable As DataTable
    Dim b2csTable As DataTable

    Private Sub ImportData()
        Qry = "SELECT  VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE MONTH(SI_DATE) =9 And YEAR(SI_DATE)=2017" &
            " GROUP BY VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER"
    End Sub

    Private Sub BindData()
        Qry = "SELECT  VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE MONTH(SI_DATE) =9 And YEAR(SI_DATE)=2017" &
            " GROUP BY VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER"

    End Sub
End Class
