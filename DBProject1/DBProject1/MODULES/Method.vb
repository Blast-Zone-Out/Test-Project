Imports MySql.Data.MySqlClient



Module Method
    Dim connectionString As String = "server=localhost;user=root;database=sample;Convert Zero Datetime=True;"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Public Sub AddQuery(ByVal productNo As String, ByVal productName As String, ByVal price As String, ByVal category As String, ByVal expiryDate As String, ByVal stocks As String)
        Try
            Dim AddQuery As String = "INSERT INTO `products` (`product_no`, `product_name`, `price`, `category`, `expire_date`, `stocks`) " &
                                     "VALUES ('" & productNo & "', '" & productName & "', '" & price & "', '" & category & "', '" & expiryDate & "', '" & stocks & "')"

            connection.Open()

            Dim Command As New MySqlCommand(AddQuery, connection)
            Dim result As Integer = Command.ExecuteNonQuery()

            If result > 0 Then
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MAINFORM.Show_Products()
            Else
                MessageBox.Show("Failed to add the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Sub

    ' Helper method to clear fields
    Private Sub ClearFields(ByVal controls As Control())
        For Each ctrl In controls
            If TypeOf ctrl Is TextBox Then
                ctrl.Text = String.Empty
            ElseIf TypeOf ctrl Is DateTimePicker Then
                CType(ctrl, DateTimePicker).Value = DateTime.Now
            End If
        Next
    End Sub

    '' If MAINFORM.dgvProducts.SelectedRows.Count > 0 Then
    'Dim selectedRow As DataGridViewRow = MAINFORM.dgvProducts.SelectedRows(0)
    '            txtProduct.Text = MAINFORM.dgvProducts.CurrentRow.Cells(1).Value
    '            txtProductName.Text = MAINFORM.dgvProducts.CurrentRow.Cells(2).Value
    '            txtPrice.Text = MAINFORM.dgvProducts.CurrentRow.Cells(3).Value
    '            txtCategory.Text = MAINFORM.dgvProducts.CurrentRow.Cells(4).Value
    '            txtExpiryDate.Value = MAINFORM.dgvProducts.CurrentRow.Cells(5).Value
    '            txtStocks.Text = MAINFORM.dgvProducts.CurrentRow.Cells(6).Value

    Public Sub UpdateQuery(ByVal productNo As String, ByVal productName As String, ByVal price As String, ByVal category As String, ByVal expiryDate As String, ByVal stocks As String)
        If MessageBox.Show("Are you sure you want to update the selected product?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim query As String = "UPDATE `products` SET " &
                                      "`product_no`='" & productNo & "', " &
                                      "`product_name`='" & productName & "', " &
                                      "`price`='" & price & "', " &
                                      "`category`='" & category & "', " &
                                      "`expire_date`='" & expiryDate & "', " &
                                      "`stocks`='" & stocks & "' " &
                                      "WHERE `product_no`='" & productNo & "'"

                connection.Open()

                Dim command As New MySqlCommand(query, connection)
                Dim result As Integer = command.ExecuteNonQuery()

                If result > 0 Then
                    MessageBox.Show("Product updated successfully!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MAINFORM.Show_Products()
                Else
                    MessageBox.Show("Failed to update the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try
        End If
    End Sub

    

    Public Sub DeleteQuery()
        If MAINFORM.dgvProducts.SelectedRows.Count > 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = MAINFORM.dgvProducts.SelectedRows(0)

            ' Get the value of product_no from the selected row
            Dim productNo As String = selectedRow.Cells("PRODUCT NO").Value.ToString()

            If MessageBox.Show("Are you sure you want to delete the selected product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    connection.Open()
                    ' Construct and execute the DELETE query
                    Dim query As String = "DELETE FROM products WHERE product_no = " & productNo
                    Dim command As New MySqlCommand(query, connection)
                    Dim result As Integer = command.ExecuteNonQuery()

                    If result > 0 Then
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        MAINFORM.Show_Products()
                    Else
                        MessageBox.Show("No rows were affected. Please check the product number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    ' Close the connection
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End If
        Else
            ' Inform the user that no row is selected
            MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If


    End Sub


End Module
