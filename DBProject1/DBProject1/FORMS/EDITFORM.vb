Imports MySql.Data.MySqlClient

Public Class EDITFORM
    Private dgv As DataGridView

    ' Constructor that accepts a DataGridView parameter
    Public Sub New(ByVal dgvProducts As DataGridView)
        ' This call is required by the designer.
        InitializeComponent()

        ' Assign the parameter to the class-level variable
        dgv = dgvProducts
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        Dim ClearInfo() As Control = {txtProduct, txtProductName, txtPrice, txtCategory, txtExpiryDate, txtStocks}
        FillUpClear(ClearInfo)
    End Sub

    Private Sub EDITFORM_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        Try
            If MAINFORM.dgvProducts.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = MAINFORM.dgvProducts.SelectedRows(0)
                txtProduct.Text = MAINFORM.dgvProducts.CurrentRow.Cells(0).Value
                txtProductName.Text = MAINFORM.dgvProducts.CurrentRow.Cells(1).Value
                txtPrice.Text = MAINFORM.dgvProducts.CurrentRow.Cells(2).Value
                txtCategory.Text = MAINFORM.dgvProducts.CurrentRow.Cells(3).Value
                txtExpiryDate.Value = MAINFORM.dgvProducts.CurrentRow.Cells(4).Value
                txtStocks.Text = MAINFORM.dgvProducts.CurrentRow.Cells(5).Value
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
       
        Dim ConvertedExpDate As String = txtExpiryDate.Value.ToString("yyyy-MM-dd")
        UpdateQuery(txtProduct.Text, txtProductName.Text, txtPrice.Text, txtCategory.Text, ConvertedExpDate, txtStocks.Text)

    End Sub
End Class