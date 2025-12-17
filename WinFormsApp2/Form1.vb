Imports Guna.UI2.WinForms
Imports Microsoft.Data.SqlClient

Public Class Form1
    Private connectionString As String = "Server=JARVIS;Database=mydb;Integrated Security=True;TrustServerCertificate=True"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LinkLabel1.Visible = True
        TextBox3.Visible = False
        Guna2ImageCheckBox2.Visible = False
        Me.ActiveControl = Label3
        Guna2ComboBox1.Items.Add("Admin")
        Guna2ComboBox1.Items.Add("Customer")
        Guna2ComboBox1.SelectedItem = "Customer"
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        ClearTable()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim pass As String = TextBox2.Text.Trim()

        ' Check for empty or placeholder values
        If String.IsNullOrWhiteSpace(username) OrElse username.ToLower() = "username" OrElse
       String.IsNullOrWhiteSpace(pass) OrElse pass.ToLower() = "password" Then

            MessageBox.Show("Username and Password cannot be empty or default values!")
            Exit Sub
        End If

        Dim userType As String = Guna2ComboBox1.SelectedItem.ToString()

        If AuthenticateUser(username, pass) Then
            CurrentCustomerName = username
            Me.Hide()

            If userType = "Customer" Then
                Application.DoEvents()
                Debug.WriteLine("Opening OrderForm for customer: " & username)
                Dim newForm As New Form3()
                newForm.Show()

            ElseIf userType = "Admin" Then
                Dim newForm As New adminaddon()
                newForm.Show()
            End If
        Else
            MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    Private Function AuthenticateUser(username As String, password As String) As Boolean
        Dim query As String
        If Guna2ComboBox1.SelectedItem = "Admin" Then
            query = "SELECT COUNT(1) FROM admin WHERE adname=@adname AND adpass=@adpass"
        Else
            query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND pass=@Password"
        End If

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@adname", username)
                cmd.Parameters.AddWithValue("@adpass", password)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", password)

                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Dim newForm As New Form2()
        newForm.Show()
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        Dim exampleText As String = "username"
        Dim exampleText2 As String = "password"
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
        If Guna2ComboBox1.SelectedItem = "Admin" Then
            Label2.Visible = False
            Button2.Visible = False
            LinkLabel1.Visible = True
            TextBox3.Visible = False
            Guna2ImageCheckBox2.Visible = False
        Else
            Label2.Visible = True
            Button2.Visible = True
            LinkLabel1.Visible = True
            TextBox3.Visible = False
            Guna2ImageCheckBox2.Visible = False
        End If
    End Sub

    Private Sub Guna2ImageCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ImageCheckBox1.CheckedChanged
        If Guna2ImageCheckBox1.Checked Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim username As String = TextBox1.Text.Trim()

        ' Show warning only if empty or default placeholder
        If String.IsNullOrWhiteSpace(username) OrElse username.ToLower() = "username" Then
            MessageBox.Show("Enter Username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim query As String

        ' Choose query based on role
        If Guna2ComboBox1.SelectedItem = "Admin" Then
            query = "SELECT adpass FROM admin WHERE adname=@Username"
        Else
            query = "SELECT pass FROM users WHERE username=@Username"
        End If

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Username", username)

                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                conn.Close()

                If result Is Nothing OrElse result Is DBNull.Value Then
                    MessageBox.Show("Username does not Exsist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Show controls and autofill password
                LinkLabel1.Visible = False
                TextBox3.Visible = True
                Guna2ImageCheckBox2.Visible = True
                TextBox3.Text = result.ToString()
            End Using
        End Using
    End Sub



    Private Sub Guna2ImageCheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ImageCheckBox2.CheckedChanged
        If Guna2ImageCheckBox2.Checked Then
            TextBox3.PasswordChar = ""
        Else
            TextBox3.PasswordChar = "*"
        End If

    End Sub
End Class
