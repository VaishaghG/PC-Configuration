Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Data.SqlClient

Public Class Cart
    Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ActiveControl = Label1
        Label1.Focus()
        LoadData()

        ' ✅ Remove blue selection from the first cell
        DataGridView1.ClearSelection()

        ' ✅ Ensure form resizes but does not reposition when maximized
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimumSize = Me.Size
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        ClearTable()

    End Sub

    Private Sub LoadData()
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                Dim query As String = "SELECT cpu, gpu, storage, ram, cabinet FROM cart"
                Dim adapter As New Microsoft.Data.SqlClient.SqlDataAdapter(query, conn)
                Dim dt As New DataTable()

                conn.Open()
                adapter.Fill(dt)

                If dt.Rows.Count > 0 Then
                    DataGridView1.AutoGenerateColumns = False
                    DataGridView1.DataSource = dt
                    DataGridView1.ClearSelection()

                    ' ✅ Extract selected item names
                    Dim cpu As String = dt.Rows(0)("cpu").ToString()
                    Dim gpu As String = dt.Rows(0)("gpu").ToString()
                    Dim storage As String = dt.Rows(0)("storage").ToString()
                    Dim ram As String = dt.Rows(0)("ram").ToString()
                    Dim cabinet As String = dt.Rows(0)("cabinet").ToString()

                    ' ✅ Fetch and assign prices dynamically
                    Dim cpuPrice As Decimal = GetItemPrice("cpuinfo", "cpu", cpu)
                    Dim gpuPrice As Decimal = GetItemPrice("gpuinfo", "gpu", gpu)
                    Dim storagePrice As Decimal = GetItemPrice("storageinfo", "sname", storage)
                    Dim ramPrice As Decimal = GetItemPrice("raminfo", "ramname", ram)
                    Dim cabinetPrice As Decimal = GetItemPrice("cabinetinfo", "Brand", cabinet)

                    ' ✅ Update labels with prices
                    LabelCPUPrice.Text = "₹" & cpuPrice.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN"))
                    LabelGPUPrice.Text = "₹" & gpuPrice.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN"))
                    LabelStoragePrice.Text = "₹" & storagePrice.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN"))
                    LabelRAMPrice.Text = "₹" & ramPrice.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN"))
                    LabelCabinetPrice.Text = "₹" & cabinetPrice.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN"))

                    ' ✅ Calculate and display subtotal
                    Dim totalPrice As Decimal = cpuPrice + gpuPrice + storagePrice + ramPrice + cabinetPrice
                    LabelTotalPrice.Text = "₹" & totalPrice.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN"))
                End If

                If dt.Rows.Count > 0 Then
                    Dim cabinetName As String = dt.Rows(0)("cabinet").ToString()
                    LoadCabinetImage(cabinetName)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCabinetImage(cabinetName As String)
        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                Dim query As String = "SELECT imgpath FROM cabinetinfo WHERE Brand = @cabinetName"
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cabinetName", cabinetName)

                    conn.Open()
                    Dim imageData As Object = cmd.ExecuteScalar()

                    If imageData IsNot Nothing AndAlso Not DBNull.Value.Equals(imageData) Then
                        Dim imgBytes As Byte() = DirectCast(imageData, Byte())
                        Using ms As New MemoryStream(imgBytes)
                            Dim img As Image = Image.FromStream(ms)

                            ' ✅ Resize Image to Fit PictureBox
                            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                            PictureBox1.Image = img
                        End Using
                    Else
                        MessageBox.Show("No image found for the selected cabinet.", "Image Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.Hide()
                        Form3.Show()
                        Exit Sub
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading cabinet image: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Hide()
            Form3.Show()
        End Try
    End Sub

    Private Function GetItemPrice(tableName As String, columnName As String, itemName As String) As Decimal
        Dim price As Decimal = 0
        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT price FROM " & tableName & " WHERE " & columnName & " = @item"
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@item", itemName)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then price = Convert.ToDecimal(result)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching price for " & itemName & ": " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return price
    End Function

    Private Sub rtnbtn_Click(sender As Object, e As EventArgs) Handles rtnbtn.Click
        Me.Hide()
        Dim newForm As New Form3()
        newForm.Show()
    End Sub

    Private Sub clrbtn_Click(sender As Object, e As EventArgs) Handles clrbtn.Click
        Try
            ' ✅ Clear database entries
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "DELETE FROM cart"
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' ✅ Clear DataGridView Rows (Preserve Column Headers)
            Dim dt As DataTable = CType(DataGridView1.DataSource, DataTable)
            If dt IsNot Nothing Then dt.Rows.Clear()
            DataGridView1.ClearSelection()

            ' ✅ Reset Labels
            LabelCPUPrice.Text = "₹0"
            LabelGPUPrice.Text = "₹0"
            LabelStoragePrice.Text = "₹0"
            LabelRAMPrice.Text = "₹0"
            LabelCabinetPrice.Text = "₹0"
            LabelTotalPrice.Text = "₹0"

            ' ✅ Reset Image
            PictureBox1.Image = Nothing

            MessageBox.Show("Cart cleared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim newForm As New Payment
        If Not AreAllCellsFilled(DataGridView1) Then
            MessageBox.Show("Please ensure all the components are selected before proceeding.", "Incomplete Order", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Proceed to the payment form
        Dim paymentForm As New Payment()
        paymentForm.Show()
        Me.Hide()
    End Sub

    ' Function to check if all cells are filled
    Private Function AreAllCellsFilled(dgv As DataGridView) As Boolean
        ' Check if there are any rows first
        If dgv.Rows.Count = 0 Then
            Return False
        End If

        ' Iterate through each cell to check if they are empty
        For Each row As DataGridViewRow In dgv.Rows
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value Is Nothing OrElse String.IsNullOrWhiteSpace(cell.Value.ToString()) Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function

End Class
