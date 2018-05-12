Module GlobalModule

    'Public v_the_current_selected_division_id As Integer

    Public v_logged_in_as_admin As Boolean = False

    Public v_the_current_division_id As Integer
    Public v_the_current_selected_division As String
    Public v_the_current_logged_in_user_name As String
    Public v_the_current_logged_in_user_role As String

    Public v_the_current_logged_in_user_id As Integer
    'Public v_the_current_ServerDate As DateTime
    Public v_the_current_Selected_CostCenter_id As Integer


    Public NULL_DATE As Date = New Date(1900, 1, 1)

    Public gblMessageHeading As String = "MMS PLUS"
    'Public gblMessageHeading_Error As String = "Error"
    Public gblMessageHeading_confirm As String = "Confirmation"
    Public gblMessageHeading_delete As String = "Delete Message"
    Public gblMessageHeading_Error As String = "The System is unable to process your request at the moment. Please try again later."
    Public gblMessage_ContactInfo As String = "For further support Contact IT Dept at mailto:mmsplus@SyncSolz.com or http://SyncSolz.com/support"

    Public gblDataBaseServer_Name As String = "afblmms"

    Public gblDataBase_UserName As String = ""
    Public gblDataBase_Password As String = ""
    Public gblDataBase_Name As String = ""

    Public v_division_type As division_type

    'Inderjeet: No need of these variables.
    'Public gblMainStore_ROLE As String = "MS"
    'Public gblCostCenter_ROLE As String = "CC"
    Public gblDNS_Online As String '= "Data Source=republicofchicken.com\sql2005;Initial Catalog=AFBL_CentralizePos;User Id=afbl_cp; Password=afbl_cp; Connection Timeout = 100"
    '*****Ajinder
    Public gblSelectedReportName As Integer
    Public Const gblMaxValue = 999999999.999999
    Public G_MyConTransaction As Boolean
    '************************
    'Form Objects

    Public frm_Item_Master_obj As frm_Item_Master



    Public Enum division_type As Integer
        Warehouse = 1
        Resturant = 2
    End Enum

    Public Enum enmReportName
        RptItemLedgerSummary = 1
        RptMrnDetail = 2
        RptIssueDetail = 3
        RptIndentDetail = 4
        RptIndentPrintDetail = 5
        RptMRSMainStorePrint = 6
        RptMrsItemList = 7
        RptMrsdetailList = 8
        RptOpenPurchaseOrderPrint = 9
        RptItemwiseMRS = 10
        RptStockValue = 11
        RptItemwiseMrsMstore = 12
        RptMrsDetailMStore = 13
        RptMrsItemListMStore = 14
        RptStockValueCategoryWise = 15
        RptPurchaseOrderPrint = 16
        RptWastageDetailList = 17
        RptItemwiseWastage = 18
        RptWastagePrint = 19
        RptIndentDetailPrint = 20
        RptRevWastagePrint = 21
        RptRevMICostCenterPrint = 22
        RptListofIndent = 23
        RptItemWiseIndentDetail = 24
        RptMatIssueToCostCenterPrint = 25
        ''''MRN WITHOUT PO
        RptListofMRNWithOutPO = 26
        RptItemWiseMRNWithOutPO = 27
        RptStockValueBatchWise = 28
        RptListofMRNDetailWithOutPO = 29
        RptMRNWithoutPOPrint = 30

        ''''MRN WITH(Against) PO
        RptMaterialRecAgainstPOPrintRevised = 31
        RptListMRNWithPO = 32
        RptDetailMRNListWithPO = 33
        RptItemWiseMRNWithPO = 34
        RptReverseMaterialWithOutPO = 35
        RptSupplierRateList = 36
        RptReverseMaterialAgainstPO = 37
        RptMRNActualWithoutPOPrint = 38
        'RptReverseMaterialwithOutPOMaster = 36
        RptMatIssueItemWiseToCostCenterPrint = 39
        RptIndentDetailCategoryHeadWisePrint = 40
        RptMrsItemListMStoreCategoryHeadWisePrint = 41
        RptItemwiseWastageCategoryHeadWisePrint = 42
        RptCategoryHeadWiseIssue = 43
        RptListofMRNWithOutPO_WithoutSupplier = 44
        RptListofMRNWithOutPO_ItemWiseSuppliers = 45
        RptMRNWithoutPOPrint_NonAFBL = 46
        RptClosingStockPrint = 47
        RptStockValueCC = 48
        RptMaterialRecAgainstPOPrint = 49
        RptStockTransferCCPrint = 50
        RptAcceptStockCCPrint = 51
        RptInterKitchenTransferList = 52
        RptWastagePrint_cc = 53
        RptWastageDetail_ItemWise_cc = 54
        RptIkt_ItemWise_cc = 55
        RptConsumption_ItemWise_cc = 56
        RptStock_Adjustment = 57
        RptListofMRNWithpo_SupplierWise = 58
        RptListofMRNWithPO_ItemWiseSuppliers = 59
        RptCatheadWise_Consumption_cc = 60
        RpttotPurchase_catwise = 61
        RptLastpurchaserate = 62
        RptAllPurchaseRate = 63
        RptNonMovingItemList = 64
        'Invoice 
        RptInvoicePrint = 65
        RptDCInvoicePrint = 66
        RptSalesummary = 67
        RptSalesummaryList = 68
        RptCustomerRateList = 69
        RptDebitNotePrint = 70
        RptCreditNotePrint = 71
        RptGatePassPrint = 72
        RptPaymentPrint = 73
        RptSuppPaymentPrint = 74
        RptDebtorsOSPrint = 75
        RptAccPaymentPrint = 76
        RptDeliveryNotePrint = 77
        RptStockValueBrandWise = 78
        RptDebitNoteWOItemPrint = 79
        RptCreditNoteWOItemPrint = 80
        RptBrandWiseSale = 81

    End Enum
    Public Enum IndentStatus As Integer
        Fresh = 1
        Pending = 2
        Clear = 3
        Cancel = 4
    End Enum
    Public Enum MRSStatus As Integer
        Fresh = 1
        Pending = 2
        Clear = 3
        Cancel = 4
    End Enum

    Public Enum POStatus As Integer
        Fresh = 1
        Pending = 2
        Clear = 3
        Cancel = 4
    End Enum
    Public Enum MIOStatus As Integer
        Fresh = 1
        Pending = 2
        Clear = 3
        Cancel = 4
    End Enum

    Public Enum InvoiceStatus As Integer
        Fresh = 1
        Pending = 2
        Clear = 3
        Cancel = 4
    End Enum

    Public Enum TRANSFER_STATUS As Integer
        Fresh = 1
        ''''''''''''Added by Aman'''''''''''''''
        Accepted = 2
        Freezed = 3
        ''''''''''''''''''''''''''''''''''''''''
        'Pending = 2
        'Clear = 3
        'Cancel = 4
    End Enum
    Public Enum MRNStatus As Integer
        normal = 1
        cancel = 2
        clear = 3
    End Enum
    Public Enum AccountGroups As Integer
        Sundry_Debtors = 1
        Sundry_Creditors = 2
        Bank_Accounts = 3
        Bank_O_D_Account = 4
        Capital_Account = 5
        Cash_in_hand = 6
        Current_Assets = 7
        Current_Liabilities = 8
        Duties_and_Taxes = 9
        Expenses_Direct_Mfg = 10
        Expenses_Indirect_Admn = 11
        Fixed_Assets = 12
        Income_Direct_Opr = 13
        Income_Indirect = 14
        Investments = 15
        Loans_and_Advances_Asset = 16
        Loans_Liability = 17
        Pre_Operative_Expenses = 18
        Profit_and_Loss = 19
        Provisions_Expenses_Payable = 20
        Purchase = 21
        Reserves_and_Surplus = 22
        Revenue_Accounts = 23
        Sale = 24
        Secured_Loans = 25
        Securities_and_Deposits_Asset = 26
        Stock_in_hand = 27
        Suspense_Account = 28
        Unsecured_Loans = 29

    End Enum

    Public Enum Transaction_Type As Integer
        Wastage = 1
        MaterialReceivedWithoutPO = 2
        MaterialRecievedAgainstPO = 3
        ReverseWastage = 4
        MaterialReturnAgainstIssue = 5
        MaterialIssuetoCostCenter = 6
        DebitNote = 7
        ReverseMaterialRecievedWithoutPO = 8
        ReverseMaterialRecievedAgainstPO = 9
        OpeningStock = 10
        Adjustment = 11
        Stock_Transfer_to_other_outlet = 15
        Stock_Transfer_accepted_other_outlet = 15
        Sale_Invoice = 16
        CreditNote = 17
        Invoice_Settlement = 18
        Supplier_Invoice_Settlement = 19
        Opening_Balance = 20
        Depreciation = 25

    End Enum

    Public Enum PaymentType
        Receipt = 1
        Payment = 2
        Journal = 3
        Contra = 4
        Expense = 5
    End Enum

    ''********************************************************************''
    ''Property to get ReportFilePath
    ''********************************************************************''
    Dim _ReportFilePath As String
    Public ReadOnly Property ReportFilePath() As String
        Get
            _ReportFilePath = Application.StartupPath
            ''After publishing application, property "Application.StartupPath"
            ''exclude bin\debug folder.
            If _ReportFilePath.Contains("bin\Debug") Then
                Dim Index As Int32 = _ReportFilePath.IndexOf("\bin\Debug")
                _ReportFilePath = _ReportFilePath.Substring(0, Index)
            End If
            _ReportFilePath = _ReportFilePath & "\Reports\"
            Return _ReportFilePath
        End Get
    End Property
    ''********************************************************************''
    ''********************************************************************''

    Public Enum enmDataType
        D_int = 0
        D_String = 1
        D_Date = 3
    End Enum


    Public Function Encrypt(ByVal sSTR As System.String) As String
        Dim sTmp As System.String
        Dim sResult As System.String
        Dim iCnt As System.Int32

        sTmp = StrReverse(sSTR)
        sResult = ""
        For iCnt = 1 To Len(sTmp)
            sResult = sResult & Chr(Asc(Mid(sTmp, iCnt, 1)) + Asc("g"))
        Next
        Encrypt = sResult
    End Function

    Public Function Decrypt(ByVal sSTR As String) As String

        Dim sTmp As String
        Dim sResult As String
        Dim icnt As Integer

        sTmp = StrReverse(sSTR)
        sResult = ""
        For icnt = 1 To Len(sTmp)
            sResult = sResult & Chr(Asc(Mid(sTmp, icnt, 1)) - Asc("g"))
        Next
        Decrypt = sResult
    End Function

    Public Sub main()

    End Sub

End Module
