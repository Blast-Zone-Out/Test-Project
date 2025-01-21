Imports MySql.Data.MySqlClient

Public Class MAINFORM

    Dim connectionString As String = "server=localhost;user=root;database=sample;Convert Zero Datetime=True;"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Public Sub Show_Products()
        Try
            ' Open the database connection
            connection.Open()

            ' SQL query to retrieve data from the table
            Dim query As String = "SELECT product_no AS `PRODUCT NO`, product_name AS `PRODUCT NAME`, price AS `PRICE`, category AS `CATEGORY`, expire_date AS `EXPIRY DATE`, stocks AS `STOCKS` FROM products"
            Dim command As MySqlCommand = New MySqlCommand(query, connection)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(command)
            Dim table As DataTable = New DataTable()

            ' Fill the DataTable with data from the database
            adapter.Fill(table)

            ' Bind DataTable to the DataGridView
            dgvProducts.DataSource = table

        Catch ex As MySqlException
            ' Display any error message
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Close the database connection
            connection.Close()
        End Try
    End Sub

    ' Call Show_Products when the form loads
    Private Sub MAINFORM_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        txtExpiryDate.Value = DateTime.Now
    End Sub

    Private Sub MAINFORM_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Show_Products()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim a As New ADDFORM
        a.ShowDialog()
        'Try
        '    ' Correct SQL query using string concatenation
        '    Dim AddQuery As String = "INSERT INTO `products` (`product_no`, `product_name`, `price`, `category`, `expire_date`, `stocks`) " &
        '                             "VALUES ('" & txtProduct.Text & "', '" & txtProductName.Text & "', '" & txtPrice.Text & "', '" &
        '                             txtCategory.Text & "', '" & txtExpiryDate.Text & "', '" & txtStocks.Text & "')"

        '    ' Open the database connection
        '    connection.Open()

        '    ' Execute the query
        '    Dim Command As New MySqlCommand(AddQuery, connection)
        '    Dim result As Integer = Command.ExecuteNonQuery()

        '    ' Notify the user of the result
        '    If result > 0 Then
        '        MessageBox.Show("Product added successfully!")
        '    Else
        '        MessageBox.Show("Failed to add the product.")
        '    End If

        'Catch ex As Exception
        '    ' Display the exception message
        '    MessageBox.Show("An error occurred: " & ex.Message)
        'Finally
        '    ' Ensure the connection is closed
        '    If connection.State = ConnectionState.Open Then
        '        connection.Close()
        '    End If
        'End Try
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        DeleteQuery()
    End Sub

    Private Sub dgvProducts_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProducts.CellClick
        If e.RowIndex >= 0 AndAlso e.RowIndex < dgvProducts.Rows.Count Then
            ' Enable Edit and Delete buttons
            'Dim selectedRow As DataGridViewRow = dgvProducts.Rows(e.RowIndex)
            'Dim colProductNo As String = selectedRow.Cells("product_no").Value.ToString()
            'Dim colProductName As String = selectedRow.Cells("product_name").Value.ToString()
            'Dim colProductPrice As String = selectedRow.Cells("price").Value.ToString()
            'Dim colProductCategory As String = selectedRow.Cells("category").Value.ToString()
            'Dim colProductExpDate As String = selectedRow.Cells("expire_date").Value.ToString()
            'Dim colProductStocks As String = selectedRow.Cells("stocks").Value.ToString()


            txtProduct.Text = dgvProducts.CurrentRow.Cells(0).Value
            txtProductName.Text = dgvProducts.CurrentRow.Cells(1).Value
            txtPrice.Text = dgvProducts.CurrentRow.Cells(2).Value
            txtCategory.Text = dgvProducts.CurrentRow.Cells(3).Value
            txtExpiryDate.Text = dgvProducts.CurrentRow.Cells(4).Value
            txtStocks.Text = dgvProducts.CurrentRow.Cells(5).Value
        End If
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Dim editForm As New EDITFORM(dgvProducts)
        If editForm.ShowDialog() = DialogResult.OK Then

        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text <> "" Then
            connection.Open()
            Try
                ' Construct the query using concatenation
                Dim query As String = "SELECT * FROM products WHERE product_no LIKE '%" & txtSearch.Text & "%' " &
                                      "OR product_name LIKE '%" & txtSearch.Text & "%' " &
                                      "OR price LIKE '%" & txtSearch.Text & "%' " &
                                      "OR category LIKE '%" & txtSearch.Text & "%' " &
                                      "OR expire_date LIKE '%" & txtSearch.Text & "%' " &
                                      "OR stocks LIKE '%" & txtSearch.Text & "%'"

                ' Execute the query
                Dim command As New MySqlCommand(query, connection)
                Dim adapter As New MySqlDataAdapter(command)
                Dim table As New DataTable()
                adapter.Fill(table)

                ' Bind the DataTable to the DataGridView
                dgvProducts.DataSource = Nothing ' Unbind any existing data source
                dgvProducts.Rows.Clear() ' Clear rows if not data-bound
                dgvProducts.DataSource = table ' Set the new data source

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                connection.Close()

            End Try

        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Show_Products()
        txtSearch.Text = ""
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Me.Close()
            LOGINFORM.Show()

        End If

        
    End Sub
End Class