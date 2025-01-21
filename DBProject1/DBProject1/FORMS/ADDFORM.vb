Imports MySql.Data.MySqlClient
Public Class ADDFORM
    Dim connectionString As String = "server=localhost;user=root;database=sample"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click

        Dim RequiredInfo() As Control = {txtProduct, txtProductName, txtPrice, txtCategory, txtExpiryDate, txtStocks}
        For Each ctrl In RequiredInfo
            If String.IsNullOrWhiteSpace(ctrl.Text) Then
                MsgBox("Fill all boxes", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next
        Dim ConvertedExpDate As String = txtExpiryDate.Value.ToString("yyyy-MM-dd")

        AddQuery(txtProduct.Text, txtProductName.Text, txtPrice.Text, txtCategory.Text, ConvertedExpDate, txtStocks.Text)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        Dim ClearInfo() As Control = {txtProduct, txtProductName, txtPrice, txtCategory, txtExpiryDate, txtStocks}
        FillUpClear(ClearInfo)
    End Sub
End Class