Imports System.Xml

Public Class frm_create_connection_file

    Private Sub Submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit.Click
        Try

            If System.IO.File.Exists(Application.StartupPath & "\connection.xml") Then _
                System.IO.File.Delete(Application.StartupPath & "\connection.xml")


            Dim textWriter As XmlTextWriter = New XmlTextWriter(Application.StartupPath & "\connection.xml", Nothing)
            textWriter.WriteStartDocument()
            ' Write comments
            textWriter.WriteComment("Connection String for Local and remote Database ")

            'Start of LocalConnecttion
            textWriter.WriteStartElement("ConnectionStrings")
            textWriter.WriteStartElement("LocalConnection")

            ' Write Local Server Name
            textWriter.WriteStartAttribute("ServerName", "")
            textWriter.WriteValue(cmbLocalServer.Text)
            textWriter.WriteEndAttribute()

            ' Write Local Database Name
            textWriter.WriteStartAttribute("DataBase", "")
            textWriter.WriteValue(txtLocalDatabase.Text)
            textWriter.WriteEndAttribute()

            ' Write Local User Name
            textWriter.WriteStartAttribute("UserName", "")
            textWriter.WriteValue(txtLocalUserName.Text)
            textWriter.WriteEndAttribute()

            ' Write Local password
            textWriter.WriteStartAttribute("Password", "")
            textWriter.WriteValue(Encrypt(txtLocalPassword.Text))
            textWriter.WriteEndAttribute()

            ' Write timeout
            textWriter.WriteStartAttribute("Timeout", "")
            textWriter.WriteValue(txtLocalTimeOut.Text)
            textWriter.WriteEndAttribute()


            textWriter.WriteEndElement()
            'End of LocalConnecttion	



            'Start of Centralise_database
            textWriter.WriteStartElement("CentraliseConnection")

            ' Write Local Server Name
            textWriter.WriteStartAttribute("ServerName", "")
            textWriter.WriteValue(txtOnlineServer.Text)
            textWriter.WriteEndAttribute()

            ' Write Local Database Name
            textWriter.WriteStartAttribute("DataBase", "")
            textWriter.WriteValue(txtOnlineDatabase.Text)
            textWriter.WriteEndAttribute()

            ' Write Local User Name
            textWriter.WriteStartAttribute("UserName", "")
            textWriter.WriteValue(txtOnlineUserName.Text)
            textWriter.WriteEndAttribute()

            ' Write Local password
            textWriter.WriteStartAttribute("Password", "")
            textWriter.WriteValue(Encrypt(txtOnlinePassword.Text))
            textWriter.WriteEndAttribute()

            ' Write timeout
            textWriter.WriteStartAttribute("Timeout", "")
            textWriter.WriteValue(txtOnlineTimeOut.Text)
            textWriter.WriteEndAttribute()


            textWriter.WriteEndElement()
            textWriter.WriteEndElement()
            'End of LocalConnecttion

            ' Ends the document.
            textWriter.WriteEndDocument()
            ' close writer
            textWriter.Close()
            Application.Restart()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
       
    End Sub


    Private Sub validate_form()
        'If Text Then
    End Sub

    Private Sub frm_hr_create_connection_file_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim sqlServers As SQLDMO.NameList
        'Dim sqlServer As String

        'sqlServers = New SQLDMO.Application().ListAvailableSQLServers
        'For Each sqlServer In sqlServers
        '    cmbLocalServer.Items.Add(sqlServer)
        'Next

    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub
End Class