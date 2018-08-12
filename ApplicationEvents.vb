Namespace My

    ' The following events are availble for MyApplication:    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        ''********************************************************************''
        ''Start a secondary thread to take backup
        ''********************************************************************''
        Dim PrimaryThread As Threading.Thread
        Dim BackUpThread As New Threading.Thread(AddressOf TakeBackUp)
        Dim TempHeight As Int32 = 1024
        Dim TempWidth As Int32 = 768

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            If Not (System.Diagnostics.Debugger.IsAttached) Then
                Dim ChangeRes As Resolution.CResolution = New Resolution.CResolution(OriginalHeight, OriginalWidth)
            End If
            BackUpThread.Abort()
        End Sub

        Private Sub Application_StartUp(ByVal sender As Object, ByVal e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim Srn As Screen = Screen.PrimaryScreen
            OriginalHeight = Srn.Bounds.Width
            OriginalWidth = Srn.Bounds.Height

            'Dim ChangeRes As Resolution.CResolution = New Resolution.CResolution(TempHeight, TempWidth)
            If Not (System.Diagnostics.Debugger.IsAttached) Then
                Dim ChangeRes As Resolution.CResolution = New Resolution.CResolution(TempHeight, TempWidth)
            End If


            PrimaryThread = Threading.Thread.CurrentThread
            BackUpThread.Start()



        End Sub

        Private Sub TakeBackUp()
            ''run the secondary thread while the primary thread is alive
            'While PrimaryThread.IsAlive
            '    Try
            '        Dim CommonFunction As New CommonClass
            '        Dim FilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)

            '        Dim BackupQuery As String = " BACKUP DATABASE [MMSPlus] TO  DISK = N'" & FilePath & "\MMSPlus" &
            '            DateTime.Today.ToString("_dd_MM_yyyy") & ".bak' " &
            '            " WITH NOFORMAT, NOINIT,  NAME = N'MMSPlus-Full Database Backup', " &
            '            " SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            '        CommonFunction.ExecuteNonQueryWithoutTransaction(BackupQuery)
            '    Catch ex As Exception
            '        ''do nothing
            '    End Try
            '    System.Threading.Thread.Sleep(1800000)
            'End While
        End Sub
        ''********************************************************************''
        ''********************************************************************''
    End Class

End Namespace

