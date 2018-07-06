Public Class MyMenuStrip
    Inherits MenuStrip

    Protected Overrides Sub WndProc(ByRef m As Message)
        '' Set focus on WM_MOUSEACTIVATE message
        If m.Msg = &H21 AndAlso Me.CanFocus AndAlso Not Me.Focused Then
            Me.Focus()
        End If
        MyBase.WndProc(m)
    End Sub

End Class
