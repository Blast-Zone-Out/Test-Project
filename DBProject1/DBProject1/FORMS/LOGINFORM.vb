Imports ADODB
Imports MySql.Data.MySqlClient

Public Class LOGINFORM
    Dim connectionString As String = "server=localhost;user=root;database=sample;"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            connection.Open()
            MsgBox("Connection successful!", MsgBoxStyle.Information, "DATABASE CONNECTION")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim connection As MySqlConnection = New MySqlConnection(connectionString)

        Try
            Dim QueryAccount As String = "SELECT * FROM `user` WHERE `username`='" & txtUsername.Text & "' AND `password`='" & txtPassword.Text & "' LIMIT 1"
            Dim Command As New MySqlCommand(QueryAccount, connection)
            connection.Open()
            Dim Reader As MySqlDataReader = Command.ExecuteReader()
            If Reader.HasRows Then
                MsgBox("Successfully logged in", MsgBoxStyle.Information, "LOG IN SUCCESS")
                Me.txtUsername.Clear()
                Me.txtPassword.Clear()
                Me.Hide()

                MAINFORM.Show()

            Else
                MsgBox("Invalid username or password", MsgBoxStyle.Exclamation, "INVALID LOG IN")
                Me.txtUsername.Clear()
                Me.txtPassword.Clear()
            End If
            Reader.Close()
            connection.Close()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message.ToString)
        End Try
    End Sub

    
    
    Private Sub Terminate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Terminate.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to terminate the system?", "Confirm Termination", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
