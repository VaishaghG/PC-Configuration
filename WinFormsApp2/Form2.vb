Imports MySqlConnector
Imports MySql.Data.MySqlClient
Imports Microsoft.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions

Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ActiveControl = Label1
        Dim exampleText As String = "Username"
        Dim exampleText2 As String = "Password"
        Dim exampleText3 As String = "Address"
        Dim exampleText4 As String = "PhoneNo"
        txtUsername.ForeColor = Color.Gray
        txtUsername.Text = exampleText
        AddHandler txtUsername.Enter, Sub()
                                          If txtUsername.Text = exampleText Then
                                              txtUsername.Text = ""
                                              txtUsername.ForeColor = Color.Black
                                          End If
                                      End Sub

        AddHandler txtUsername.Leave, Sub()
                                          If txtUsername.Text = "" Then
                                              txtUsername.Text = exampleText
                                              txtUsername.ForeColor = Color.Gray
                                          End If
                                      End Sub
        txtPassword.ForeColor = Color.Gray
        txtPassword.Text = exampleText2

        AddHandler txtPassword.Enter, Sub()
                                          If txtPassword.Text = exampleText2 Then
                                              txtPassword.Text = ""
                                              txtPassword.ForeColor = Color.Black
                                          End If
                                      End Sub

        AddHandler txtPassword.Leave, Sub()
                                          If txtPassword.Text = "" Then
                                              txtPassword.Text = exampleText2
                                              txtPassword.ForeColor = Color.Gray
                                          End If
                                      End Sub
        txtAddress.ForeColor = Color.Gray
        txtAddress.Text = exampleText3

        AddHandler txtAddress.Enter, Sub()
                                         If txtAddress.Text = exampleText3 Then
                                             txtAddress.Text = ""
                                             txtAddress.ForeColor = Color.Black
                                         End If
                                     End Sub

        AddHandler txtAddress.Leave, Sub()
                                         If txtAddress.Text = "" Then
                                             txtAddress.Text = exampleText3
                                             txtAddress.ForeColor = Color.Gray
                                         End If
                                     End Sub
        txtPhone.ForeColor = Color.Gray
        txtPhone.Text = exampleText4

        AddHandler txtPhone.Enter, Sub()
                                       If txtPhone.Text = exampleText4 Then
                                           txtPhone.Text = ""
                                           txtPhone.ForeColor = Color.Black
                                       End If
                                   End Sub

        AddHandler txtPhone.Leave, Sub()
                                       If txtPhone.Text = "" Then
                                           txtPhone.Text = exampleText4
                                           txtPhone.ForeColor = Color.Gray
                                       End If
                                   End Sub
    End Sub

    Private Function UsernameExists(ByVal username As String) As Boolean
        Dim exists As Boolean = False
        Dim query As String = "SELECT COUNT(*) FROM users WHERE LOWER(username) = LOWER(@username)"

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                exists = (count > 0)
            End Using
        End Using

        Return exists
    End Function
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        ClearTable()
    End Sub

    ' Define MySQL connection string
    Private connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

    ' Method to insert data into MySQL database
    Private Sub InsertData()
        Using con As New SqlConnection(connectionString)
            Try
                con.Open()
                Dim query As String = "INSERT INTO Users (Username, pass, address, phoneno) VALUES (@Username, @pass, @address, @phoneno)"

                Using cmd As New SqlCommand(query, con)
                    ' Assign values from textboxes
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text)
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                    cmd.Parameters.AddWithValue("@phoneno", txtPhone.Text)

                    ' Execute Query
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    ' Confirmation message
                    If txtUsername.Text = "Username" Or txtPassword.Text = "Password" Or txtAddress.Text = "Address" Or txtPhone.Text = "PhoneNo" Or txtUsername.Text = "" Or txtPassword.Text = "" Or txtAddress.Text = "" Or txtPhone.Text = "" Or rowsAffected = 0 Then
                        MessageBox.Show("Failed to register user. Please Enter all the fields mentioned!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("User registered successfully! Return to Login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Button Click Event to Trigger Insertion
    Private Sub BtnRegister_Click(sender As Object, e As EventArgs) Handles BtnRegister.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' Password validation
        Dim pattern As String = "^(?=.*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$"
        If Not Regex.IsMatch(password, pattern) Then
            MessageBox.Show("Password must be at least 8 characters long, include at least one letter, one number, and one special symbol.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Exit Sub
        End If

        ' Check if username exists
        If UsernameExists(username) Then
            MessageBox.Show("Username already exists. Please choose a different one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Exit Sub
        End If

        InsertData()
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub txtPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.Hide()
        Dim newForm As New Form1() ' Create a new instance
        newForm.Show()
        Dim exampleText As String = "Username"
        Dim exampleText2 As String = "Password"
        Dim exampleText3 As String = "Address"
        Dim exampleText4 As String = "PhoneNo"
        txtUsername.ForeColor = Color.Gray
        txtUsername.Text = exampleText
        AddHandler txtUsername.Enter, Sub()
                                          If txtUsername.Text = exampleText Then
                                              txtUsername.Text = ""
                                              txtUsername.ForeColor = Color.Black
                                          End If
                                      End Sub

        AddHandler txtUsername.Leave, Sub()
                                          If txtUsername.Text = "" Then
                                              txtUsername.Text = exampleText
                                              txtUsername.ForeColor = Color.Gray
                                          End If
                                      End Sub
        txtPassword.ForeColor = Color.Gray
        txtPassword.Text = exampleText2

        AddHandler txtPassword.Enter, Sub()
                                          If txtPassword.Text = exampleText2 Then
                                              txtPassword.Text = ""
                                              txtPassword.ForeColor = Color.Black
                                          End If
                                      End Sub

        AddHandler txtPassword.Leave, Sub()
                                          If txtPassword.Text = "" Then
                                              txtPassword.Text = exampleText2
                                              txtPassword.ForeColor = Color.Gray
                                          End If
                                      End Sub
        txtAddress.ForeColor = Color.Gray
        txtAddress.Text = exampleText3

        AddHandler txtAddress.Enter, Sub()
                                         If txtAddress.Text = exampleText3 Then
                                             txtAddress.Text = ""
                                             txtAddress.ForeColor = Color.Black
                                         End If
                                     End Sub

        AddHandler txtAddress.Leave, Sub()
                                         If txtAddress.Text = "" Then
                                             txtAddress.Text = exampleText3
                                             txtAddress.ForeColor = Color.Gray
                                         End If
                                     End Sub
        txtPhone.ForeColor = Color.Gray
        txtPhone.Text = exampleText4

        AddHandler txtPhone.Enter, Sub()
                                       If txtPhone.Text = exampleText4 Then
                                           txtPhone.Text = ""
                                           txtPhone.ForeColor = Color.Black
                                       End If
                                   End Sub

        AddHandler txtPhone.Leave, Sub()
                                       If txtPhone.Text = "" Then
                                           txtPhone.Text = exampleText4
                                           txtPhone.ForeColor = Color.Gray
                                       End If
                                   End Sub
    End Sub

    Private Sub txtPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged
        If txtPhone.Text.Length > 10 Then
            MessageBox.Show("Maximum 10 characters allowed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPhone.Text = txtPhone.Text.Substring(0, 10)
            txtPhone.SelectionStart = txtPhone.Text.Length
        End If
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub

    Private Sub Guna2ImageCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ImageCheckBox1.CheckedChanged
        If Guna2ImageCheckBox1.Checked Then
            txtPassword.PasswordChar = "*"
        Else
            txtPassword.PasswordChar = ""
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub
End Class