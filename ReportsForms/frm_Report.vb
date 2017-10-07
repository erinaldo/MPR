Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frm_Report

    Dim obj As New CommonClass
    Dim clsObj As New material_recieved_without_po_master.cls_material_recieved_without_po_master
    Dim prpty As New material_recieved_without_po_master.cls_material_recieved_without_po_master_prop
    Dim prop As New material_rec_against_PO.cls_Material_rec_Against_PO_Prop
    Dim Master As New material_rec_against_PO.cls_material_recieved_against_po_master

    Public Mrn_id As Int32
    Public formName As String

    Private Sub TsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsbExport.Click

        cryViewer.ExportReport()
    End Sub

    Private Sub TsbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsbPrint.Click
        Dim cmd As New SqlCommand
        cryViewer.PrintReport()

        If formName = "MRN_WithoutPO" Then
            If Mrn_id > 0 Then
                prpty.Received_ID = Mrn_id
                prpty.IsPrinted = 1
                clsObj.Update_PrintStatus_MRN(prpty, cmd)
            End If
        End If

        If formName = "MRN_AgainstPO" Then
            If Mrn_id > 0 Then
                prop.Receipt_ID = Mrn_id
                prop.IsPrinted = 1
                Master.Update_PrintStatus(prop)
            End If
        End If
    End Sub

    Private Sub TsbFirstPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsbFirstPage.Click
        cryViewer.ShowFirstPage()
    End Sub

    Private Sub Tsbprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tsbprevious.Click
        cryViewer.ShowPreviousPage()
    End Sub

    Private Sub Tspnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tspnext.Click
        cryViewer.ShowNextPage()
    End Sub

    Private Sub Tsblast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tsblast.Click
        cryViewer.ShowLastPage()
    End Sub
End Class