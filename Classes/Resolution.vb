Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)> _
Public Structure DEVMODE1
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
    Public dmDeviceName As String
    Public dmSpecVersion As Short
    Public dmDriverVersion As Short
    Public dmSize As Short
    Public dmDriverExtra As Short
    Public dmFields As Integer

    Public dmOrientation As Short
    Public dmPaperSize As Short
    Public dmPaperLength As Short
    Public dmPaperWidth As Short

    Public dmScale As Short
    Public dmCopies As Short
    Public dmDefaultSource As Short
    Public dmPrintQuality As Short
    Public dmColor As Short
    Public dmDuplex As Short
    Public dmYResolution As Short
    Public dmTTOption As Short
    Public dmCollate As Short
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
    Public dmFormName As String
    Public dmLogPixels As Short
    Public dmBitsPerPel As Short
    Public dmPelsWidth As Integer
    Public dmPelsHeight As Integer

    Public dmDisplayFlags As Integer
    Public dmDisplayFrequency As Integer

    Public dmICMMethod As Integer
    Public dmICMIntent As Integer
    Public dmMediaType As Integer
    Public dmDitherType As Integer
    Public dmReserved1 As Integer
    Public dmReserved2 As Integer

    Public dmPanningWidth As Integer
    Public dmPanningHeight As Integer
End Structure



Class User_32
    <DllImport("user32.dll")> _
    Public Shared Function EnumDisplaySettings(ByVal deviceName As String, ByVal modeNum As Integer, ByRef devMode As DEVMODE1) As Integer
    End Function
    <DllImport("user32.dll")> _
    Public Shared Function ChangeDisplaySettings(ByRef devMode As DEVMODE1, ByVal flags As Integer) As Integer
    End Function

    Public Const ENUM_CURRENT_SETTINGS As Integer = -1
    Public Const CDS_UPDATEREGISTRY As Integer = &H1
    Public Const CDS_TEST As Integer = &H2
    Public Const DISP_CHANGE_SUCCESSFUL As Integer = 0
    Public Const DISP_CHANGE_RESTART As Integer = 1
    Public Const DISP_CHANGE_FAILED As Integer = -1
End Class


Namespace Resolution
    Class CResolution
        Public Sub New(ByVal a As Integer, ByVal b As Integer)
            Dim screen__1 As Screen = Screen.PrimaryScreen


            Dim iWidth As Integer = a
            Dim iHeight As Integer = b

            Dim dm As New DEVMODE1()
            dm.dmDeviceName = New [String](New Char(31) {})
            dm.dmFormName = New [String](New Char(31) {})
            dm.dmSize = CShort(Marshal.SizeOf(dm))

            If 0 <> User_32.EnumDisplaySettings(Nothing, User_32.ENUM_CURRENT_SETTINGS, dm) Then

                dm.dmPelsWidth = iWidth
                dm.dmPelsHeight = iHeight

                Dim iRet As Integer = User_32.ChangeDisplaySettings(dm, User_32.CDS_TEST)

                If iRet = User_32.DISP_CHANGE_FAILED Then
                    MessageBox.Show("Unable to process your request")
                    MessageBox.Show("Description: Unable To Process Your Request. Sorry For This Inconvenience.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    iRet = User_32.ChangeDisplaySettings(dm, User_32.CDS_UPDATEREGISTRY)

                    Select Case iRet
                        Case User_32.DISP_CHANGE_SUCCESSFUL
                            If True Then
                                Exit Select

                                'successfull change
                            End If
                        Case User_32.DISP_CHANGE_RESTART
                            If True Then

                                MessageBox.Show("Description: You Need To Reboot For The Change To Happen." & vbLf & " If You Feel Any Problem After Rebooting Your Machine" & vbLf & "Then Try To Change Resolution In Safe Mode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Select
                                'windows 9x series you have to restart
                            End If
                        Case Else
                            If True Then

                                MessageBox.Show("Description: Failed To Change The Resolution.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Select
                                'failed to change
                            End If
                    End Select

                End If
            End If
        End Sub
    End Class
End Namespace