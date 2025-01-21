Imports System.Reflection
Module CommonFuncVar
    Public SelRow As ListViewItem

    'Clear Fill up forms
    Public Sub FillUpClear(ByVal ClearInfo() As Control)
        For i = 0 To ClearInfo.Length - 1
            ClearInfo(i).Text = ""
        Next i
    End Sub

    'DoubleBuffer controls
    Public Sub EnableDoubleBuffered(ByVal inControl() As Control)

        For i = 0 To inControl.Length - 1
            Dim ControlType As Type = inControl(i).[GetType]()
            Dim pi As PropertyInfo = ControlType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
            pi.SetValue(inControl(i), True, Nothing)
        Next i
    End Sub

    'Try Middle name
    Public Function MiddleInitial(ByVal InputMN As String, Optional ByVal AdditionalChar As String = "") As String
        If InputMN.Length > 0 Then
            Return InputMN.Substring(0, 1) + AdditionalChar
        Else
            Return ""
        End If
    End Function
End Module
