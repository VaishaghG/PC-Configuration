Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class CreditDebit
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MessageBox.Show("Payment Successfull! Return to payments page to generate bill", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Dim username As String = Form1.TextBox1.Text.Trim()
        SaveLoginInfo(username)

        Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

        ' Query to fetch the latest item from cart
        Dim querySelect As String = "SELECT TOP 1 cpu, gpu, ram, storage, cabinet FROM cart"

        ' Query to update the last record with NULL values (without using id)
        Dim queryUpdate As String = "
        UPDATE custorder 
        SET 
            cpu = COALESCE(cpu, @cpu),
            gpu = COALESCE(gpu, @gpu),
            ram = COALESCE(ram, @ram),
            storage = COALESCE(storage, @storage),
            cabinet = COALESCE(cabinet, @cabinet)
        WHERE cpu IS NULL OR gpu IS NULL OR ram IS NULL OR storage IS NULL OR cabinet IS NULL;
    "

        Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
            conn.Open()

            ' Fetch values from cart
            Using cmdSelect As New Microsoft.Data.SqlClient.SqlCommand(querySelect, conn)
                Using reader As Microsoft.Data.SqlClient.SqlDataReader = cmdSelect.ExecuteReader()
                    If reader.Read() Then
                        Dim cpu As Object = If(IsDBNull(reader("cpu")), DBNull.Value, reader("cpu"))
                        Dim gpu As Object = If(IsDBNull(reader("gpu")), DBNull.Value, reader("gpu"))
                        Dim ram As Object = If(IsDBNull(reader("ram")), DBNull.Value, reader("ram"))
                        Dim storage As Object = If(IsDBNull(reader("storage")), DBNull.Value, reader("storage"))
                        Dim cabinet As Object = If(IsDBNull(reader("cabinet")), DBNull.Value, reader("cabinet"))

                        reader.Close()

                        ' Update only the latest NULL record in custorder
                        Using cmdUpdate As New Microsoft.Data.SqlClient.SqlCommand(queryUpdate, conn)
                            cmdUpdate.Parameters.AddWithValue("@cpu", cpu)
                            cmdUpdate.Parameters.AddWithValue("@gpu", gpu)
                            cmdUpdate.Parameters.AddWithValue("@ram", ram)
                            cmdUpdate.Parameters.AddWithValue("@storage", storage)
                            cmdUpdate.Parameters.AddWithValue("@cabinet", cabinet)

                            Dim rowsAffected As Integer = cmdUpdate.ExecuteNonQuery()
                            If rowsAffected = 0 Then
                                MessageBox.Show("No record found to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End Using
                    Else
                        MessageBox.Show("No items found in cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub SaveLoginInfo(username As String)
        Dim query As String = "INSERT INTO custorder (custname, orderdate) VALUES (@custname, @orderdate)"
        Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"
        Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
            Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@custname", username)
                cmd.Parameters.AddWithValue("@orderdate", DateTime.Now)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show("Error saving login info: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
    Private Sub CreditDebit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ClearTable()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click, Label2.Click, Label3.Click, Label4.Click, Label5.Click

    End Sub

    Private Sub CreditDebit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2TextBox2.Text = GlobalVariables.CurrentCustomerName
        Dim exampleText As String = "MM/YY"
        Dim exampleText2 As String = "123"
        TextBox1.ForeColor = Color.Gray
        TextBox1.Text = exampleText
        AddHandler TextBox1.Enter, Sub()
                                       If TextBox1.Text = exampleText Then
                                           TextBox1.Text = ""
                                           TextBox1.ForeColor = Color.Black
                                       End If
                                   End Sub

        AddHandler TextBox1.Leave, Sub()
                                       If TextBox1.Text = "" Then
                                           TextBox1.Text = exampleText
                                           TextBox1.ForeColor = Color.Gray
                                       End If
                                   End Sub
        TextBox2.ForeColor = Color.Gray
        TextBox2.Text = exampleText2

        AddHandler TextBox2.Enter, Sub()
                                       If TextBox2.Text = exampleText2 Then
                                           TextBox2.Text = ""
                                           TextBox2.ForeColor = Color.Black
                                       End If
                                   End Sub

        AddHandler TextBox2.Leave, Sub()
                                       If TextBox2.Text = "" Then
                                           TextBox2.Text = exampleText2
                                           TextBox2.ForeColor = Color.Gray
                                       End If
                                   End Sub
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim txt As String = TextBox3.Text.Replace(" ", "") ' Remove existing spaces

        ' Ensure only up to 16 digits are allowed
        If txt.Length > 16 Then
            txt = txt.Substring(0, 16)
        End If

        ' Format with spaces after every 4 digits
        Dim formattedText As String = ""
        For i As Integer = 0 To txt.Length - 1
            formattedText &= txt(i)
            If (i + 1) Mod 4 = 0 AndAlso (i + 1) < txt.Length Then
                formattedText &= " " ' Add space after every 4th digit
            End If
        Next

        ' Prevent looping issue
        If TextBox3.Text <> formattedText Then
            TextBox3.Text = formattedText
            TextBox3.SelectionStart = TextBox3.Text.Length ' Keep cursor at the end
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Hide()
        Dim newForm As New Cart
        newForm.Show()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Hide()
        Dim newForm As New Payment
        newForm.Show()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim txt = TextBox1.Text.Replace("/", "") ' Remove any existing slash

        ' Ensure only digits are processed (max 4 digits)
        If txt.Length > 4 Then
            txt = txt.Substring(0, 4) ' Trim to max 4 digits
        End If

        ' Insert '/' after 2 digits and make sure it remains visible
        If txt.Length > 2 Then
            txt = txt.Insert(2, "/")
        ElseIf txt.Length = 2 Then
            txt &= "/" ' Add the slash after two digits
        End If

        ' Prevent looping issue
        If TextBox1.Text <> txt Then
            TextBox1.Text = txt
            TextBox1.SelectionStart = TextBox1.Text.Length ' Keep cursor at the end
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.Length > 3 Then
            TextBox2.Text = TextBox2.Text.Substring(0, 3)
            TextBox2.SelectionStart = TextBox2.Text.Length ' Keep cursor at the end
        End If
    End Sub
End Class