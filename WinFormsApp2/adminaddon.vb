Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.IO
Imports Guna.UI2.WinForms

Public Class adminaddon
    ' ✅ Connection String (Ensure it's correct)
    Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

    Private Sub adminaddon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.BringToFront()
        Guna2TextBox1.SendToBack()
        HideAllUpdateButtons()
        ' ✅ Automatically load CPU list and pre-select CPU button
        Cpu.PerformClick()

        ' ✅ Remove selection outline from ListBox

        Me.ActiveControl = Label8 ' Moves focus to another control

        ' ✅ Initialize DataGridView with columns (keeps structure even when empty)


    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ClearTable()
    End Sub
    Private Sub HideAllUpdateButtons()

    End Sub

    ' ✅ Function to Setup DataGridView Columns


    ' ✅ Logout button functionality
    Private Sub Logoutbtn_Click(sender As Object, e As EventArgs) Handles Logoutbtn.Click
        Hide()
        Dim newForm As New Form1
        newForm.Show()
    End Sub

    ' ✅ CPU Button Click - Loads CPU List
    Private Sub Cpu_Click(sender As Object, e As EventArgs) Handles Cpu.Click
        rating.Location = New Point(667, 524)
        Label14.Location = New Point(620, 536)
        SetActiveButton(Cpu)
        HideAllUpdateButtons()
        Updte.Visible = True
        Updte.Enabled = True

        Label3.Text = "CPU Name"
        Label4.Text = "Cores"
        Label4.Location = New Point(499, 232)
        Label6.Text = "Threads"
        Label6.Location = New Point(40, 309)
        Label7.Text = "Base Speed"
        Label7.Location = New Point(481, 309)
        Label11.Text = "Want to delete an item? Enter the name of CPU : "
        Label5.Visible = True
        Label3.Visible = True
        Label4.Visible = True
        Label6.Visible = True
        Label7.Visible = True
        Label9.Visible = True
        Label9.Text = "Turbo Speed"
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = True
        TextBox6.Visible = True
        LoadData()
        LoadNamesToDeleteBox()
    End Sub

    ' ✅ Other category button click handlers
    Private Sub Gpu_Click(sender As Object, e As EventArgs) Handles Gpu.Click
        rating.Location = New Point(671, 378)
        Label14.Location = New Point(621, 390)
        SetActiveButton(Gpu)
        HideAllUpdateButtons()

        Label3.Text = "GPU Name"
        Label4.Text = "Memory (GB)"
        Label4.Location = New Point(470, 232)
        Label6.Text = "Base Clock (MHz)"
        Label6.Location = New Point(26, 309)
        Label7.Text = "Clock Speed (MHz)"
        Label7.Location = New Point(450, 309)
        Label11.Text = "Want to delete an item? Enter the name of GPU : "
        Label3.Visible = True
        Label4.Visible = True
        Label6.Visible = True
        Label7.Visible = True
        Label5.Visible = False
        Label9.Visible = False
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = False
        TextBox6.Visible = False
        LoadData()
        LoadNamesToDeleteBox()
    End Sub

    Private Sub Storage_Click(sender As Object, e As EventArgs) Handles Storage.Click
        rating.Location = New Point(671, 378)
        Label14.Location = New Point(621, 390)
        SetActiveButton(Storage)
        HideAllUpdateButtons()

        Label3.Text = "Storage Name"
        Label4.Text = "Capacity (TB)"
        Label4.Location = New Point(470, 232)
        Label6.Text = "Cache (MB)"
        Label6.Location = New Point(40, 309)
        Label11.Text = "Want to delete an item? Enter the name of STORAGE : "
        Label3.Visible = True
        Label4.Visible = True
        Label6.Visible = True
        Label5.Visible = False
        Label7.Visible = False
        Label9.Visible = False
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox5.Visible = False
        TextBox4.Visible = False
        TextBox6.Visible = False
        LoadData()
        LoadNamesToDeleteBox()
    End Sub

    Private Sub Ram_Click(sender As Object, e As EventArgs) Handles Ram.Click
        rating.Location = New Point(671, 378)
        Label14.Location = New Point(621, 390)
        SetActiveButton(Ram)
        HideAllUpdateButtons()
        Label3.Text = "RAM Name"
        Label4.Text = "RAM Size"
        Label4.Location = New Point(470, 232)
        Label6.Text = "RAM Type"
        Label6.Location = New Point(40, 309)
        Label7.Text = "Quantity"
        Label7.Location = New Point(470, 309)
        Label9.Visible = True
        Label9.Text = "RAM Speed (MHz)"
        Label9.Location = New Point(25, 390)
        Label11.Text = "Want to delete an item? Enter the name of RAM: "
        Label3.Visible = True
        Label4.Visible = True
        Label6.Visible = True
        Label7.Visible = True
        Label9.Visible = True
        Label5.Visible = False
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = True
        TextBox6.Visible = False
        LoadData()
        LoadNamesToDeleteBox()
    End Sub

    Private Sub Cabinet_Click(sender As Object, e As EventArgs) Handles Cabinet.Click
        rating.Location = New Point(671, 378)
        Label14.Location = New Point(621, 390)
        SetActiveButton(Cabinet)
        HideAllUpdateButtons()

        Label3.Text = "Cabinet Name"
        Label4.Text = "Side Panel"
        Label4.Location = New Point(470, 232)
        Label6.Text = "Cabinet Type"
        Label6.Location = New Point(35, 309)
        Label7.Text = "Colour"
        Label7.Location = New Point(480, 309)
        Label11.Text = "Want to delete an item? Enter the name of CABINET : "
        Label3.Visible = True
        Label4.Visible = True
        Label6.Visible = True
        Label7.Visible = True
        Label5.Visible = False
        Label9.Visible = False
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = False
        TextBox6.Visible = False
        LoadData()
        LoadNamesToDeleteBox()
    End Sub

    ' ✅ Helper function to reset UI selections
    Private Sub SetActiveButton(selectedButton As Object)
        ' Reset all buttons to gray
        Cpu.FillColor = Color.Gray
        Gpu.FillColor = Color.Gray
        Ram.FillColor = Color.Gray
        Storage.FillColor = Color.Gray
        Cabinet.FillColor = Color.Gray

        ' Highlight the selected button
        selectedButton.FillColor = Color.Black
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        rating.Text = ""

    End Sub

    Private Sub LoadData()
        TextBox1.BringToFront()
        Guna2TextBox1.SendToBack()
        Dim query As String = ""

        If Cpu.FillColor = Color.Black Then
            query = "SELECT cpu FROM cpuinfo"
        ElseIf Gpu.FillColor = Color.Black Then
            query = "SELECT gpu FROM gpuinfo"
        ElseIf Ram.FillColor = Color.Black Then
            query = "SELECT ramname FROM raminfo"
        ElseIf Storage.FillColor = Color.Black Then
            query = "SELECT sname FROM storageinfo"
        ElseIf Cabinet.FillColor = Color.Black Then
            query = "SELECT Brand FROM cabinetinfo"
        End If

        If query <> "" Then
            TextBox1.Items.Clear()
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            TextBox1.Items.Add(reader(0).ToString())
                        End While
                    End Using
                End Using
            End Using
        End If
    End Sub



    Private Sub update_Click(sender As Object, e As EventArgs) Handles Updte.Click
        Dim topTextboxName As String
        If Me.Controls.GetChildIndex(Guna2TextBox1) < Me.Controls.GetChildIndex(TextBox1) Then
            topTextboxName = Guna2TextBox1.Text
        Else
            topTextboxName = TextBox1.Text
        End If

        ' Shared function to get image bytes


        ' Unified handler
        Dim tableName As String = ""
        Dim keyColumn As String = ""
        Dim keyValue As String = topTextboxName.Trim()
        Dim updateFields As New List(Of String)
        Dim parameters As New List(Of SqlParameter)

        If Cpu.FillColor = Color.Black Then
            tableName = "cpuinfo" : keyColumn = "cpu"
            If String.IsNullOrWhiteSpace(keyValue) Then
                MessageBox.Show("Please enter the CPU Name to update or insert.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not String.IsNullOrWhiteSpace(TextBox2.Text) Then updateFields.Add("cores = @Cores") : parameters.Add(New SqlParameter("@Cores", Integer.Parse(TextBox2.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then updateFields.Add("threads = @Threads") : parameters.Add(New SqlParameter("@Threads", Integer.Parse(TextBox3.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox4.Text) Then updateFields.Add("basespeed = @BaseSpeed") : parameters.Add(New SqlParameter("@BaseSpeed", Decimal.Parse(TextBox4.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox5.Text) Then updateFields.Add("turbospeed = @TurboSpeed") : parameters.Add(New SqlParameter("@TurboSpeed", Decimal.Parse(TextBox5.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox6.Text) Then updateFields.Add("memory = @MemoryType") : parameters.Add(New SqlParameter("@MemoryType", TextBox6.Text))
            If Not String.IsNullOrWhiteSpace(TextBox9.Text) Then updateFields.Add("price = @Price") : parameters.Add(New SqlParameter("@Price", TextBox9.Text))
            If Not String.IsNullOrWhiteSpace(rating.Text) Then updateFields.Add("rating = @rating") : parameters.Add(New SqlParameter("@rating", rating.Text))
        ElseIf Ram.FillColor = Color.Black Then
            tableName = "raminfo" : keyColumn = "ramname"
            If String.IsNullOrWhiteSpace(keyValue) Then
                MessageBox.Show("Please enter the RAM Name to update or insert.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not String.IsNullOrWhiteSpace(TextBox2.Text) Then updateFields.Add("ramsize = @Size") : parameters.Add(New SqlParameter("@Size", TextBox2.Text))
            If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then updateFields.Add("ramtype = @Type") : parameters.Add(New SqlParameter("@Type", TextBox3.Text))
            If Not String.IsNullOrWhiteSpace(TextBox4.Text) Then updateFields.Add("quantity = @Qty") : parameters.Add(New SqlParameter("@Qty", Integer.Parse(TextBox4.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox5.Text) Then updateFields.Add("ramspeed = @Speed") : parameters.Add(New SqlParameter("@Speed", Integer.Parse(TextBox5.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox9.Text) Then updateFields.Add("price = @Price") : parameters.Add(New SqlParameter("@Price", TextBox9.Text))
            If Not String.IsNullOrWhiteSpace(rating.Text) Then updateFields.Add("rating = @rating") : parameters.Add(New SqlParameter("@rating", rating.Text))
        ElseIf Gpu.FillColor = Color.Black Then
            tableName = "gpuinfo" : keyColumn = "gpu"
            If String.IsNullOrWhiteSpace(keyValue) Then
                MessageBox.Show("Please enter the GPU Name to update or insert.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not String.IsNullOrWhiteSpace(TextBox2.Text) Then updateFields.Add("memory = @Memory") : parameters.Add(New SqlParameter("@Memory", Integer.Parse(TextBox2.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then updateFields.Add("baseclock = @Base") : parameters.Add(New SqlParameter("@Base", Integer.Parse(TextBox3.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox4.Text) Then updateFields.Add("clockspeed = @Clock") : parameters.Add(New SqlParameter("@Clock", Decimal.Parse(TextBox4.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox9.Text) Then updateFields.Add("price = @Price") : parameters.Add(New SqlParameter("@Price", TextBox9.Text))
            If Not String.IsNullOrWhiteSpace(rating.Text) Then updateFields.Add("rating = @rating") : parameters.Add(New SqlParameter("@rating", rating.Text))
        ElseIf Storage.FillColor = Color.Black Then
            tableName = "storageinfo" : keyColumn = "sname"
            If String.IsNullOrWhiteSpace(keyValue) Then
                MessageBox.Show("Please enter the Storage Name to update or insert.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not String.IsNullOrWhiteSpace(TextBox2.Text) Then updateFields.Add("capacity = @Capacity") : parameters.Add(New SqlParameter("@Capacity", Integer.Parse(TextBox2.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then updateFields.Add("cache = @Cache") : parameters.Add(New SqlParameter("@Cache", Integer.Parse(TextBox3.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox9.Text) Then updateFields.Add("price = @Price") : parameters.Add(New SqlParameter("@Price", TextBox9.Text))
            If Not String.IsNullOrWhiteSpace(rating.Text) Then updateFields.Add("rating = @rating") : parameters.Add(New SqlParameter("@rating", rating.Text))
        ElseIf Cabinet.FillColor = Color.Black Then
            tableName = "cabinetinfo" : keyColumn = "Brand"
            If String.IsNullOrWhiteSpace(keyValue) Then
                MessageBox.Show("Please enter the Cabinet Name to update or insert.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not String.IsNullOrWhiteSpace(TextBox2.Text) Then updateFields.Add("panel = @Panel") : parameters.Add(New SqlParameter("@Panel", Integer.Parse(TextBox2.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then updateFields.Add("ctype = @Type") : parameters.Add(New SqlParameter("@Type", Integer.Parse(TextBox3.Text)))
            If Not String.IsNullOrWhiteSpace(TextBox4.Text) Then updateFields.Add("color = @Color") : parameters.Add(New SqlParameter("@Color", TextBox4.Text))
            If Not String.IsNullOrWhiteSpace(TextBox9.Text) Then updateFields.Add("price = @Price") : parameters.Add(New SqlParameter("@Price", TextBox9.Text))
            If Not String.IsNullOrWhiteSpace(rating.Text) Then updateFields.Add("rating = @rating") : parameters.Add(New SqlParameter("@rating", rating.Text))
        Else
            MessageBox.Show("No category selected for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Handle Image (shared)
        If Not String.IsNullOrWhiteSpace(TextBox8.Text) Then
            Dim imageBytes = GetImageBytes(TextBox8.Text)
            If imageBytes IsNot Nothing Then
                updateFields.Add("imgpath = @ImageData")
                parameters.Add(New SqlParameter("@ImageData", SqlDbType.VarBinary) With {.Value = imageBytes})
            Else
                Exit Sub
            End If
        End If

        If updateFields.Count = 0 Then
            MessageBox.Show("No fields to update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Final query & execution
        Dim query = $"UPDATE {tableName} SET {String.Join(", ", updateFields)} WHERE {keyColumn} = @KeyValue"
        parameters.Add(New SqlParameter("@KeyValue", keyValue))

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddRange(parameters.ToArray())
                Try
                    conn.Open()
                    Dim rows = cmd.ExecuteNonQuery()
                    If rows > 0 Then
                        MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        ' INSERT fallback if no update
                        Dim insertCols = String.Join(", ", {keyColumn}.Concat(updateFields.Select(Function(x) x.Split("="c)(0).Trim())))
                        Dim insertParams = String.Join(", ", {"@KeyValue"}.Concat(parameters.Take(parameters.Count - 1).Select(Function(p) p.ParameterName)))
                        Dim insertQuery = $"INSERT INTO {tableName} ({insertCols}) VALUES ({insertParams})"
                        Using insertCmd As New SqlCommand(insertQuery, conn)
                            For Each p In parameters
                                insertCmd.Parameters.Add(DirectCast(CType(p, ICloneable).Clone(), SqlParameter))
                            Next

                            insertCmd.ExecuteNonQuery()
                            MessageBox.Show("Record inserted successfully!", "Insert Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Using
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Function GetImageBytes(filePath As String) As Byte()
        If Not File.Exists(filePath) Then
            MessageBox.Show("File not found! Please enter a valid file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If
        Return File.ReadAllBytes(filePath)
    End Function

    Private Sub BrowseButton_Click(sender As Object, e As EventArgs) Handles BrowseButton.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "All Files|*.*" ' Customize filter if needed

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox8.Text = openFileDialog.FileName ' Set the selected file path
        End If
    End Sub

    Private Sub dltbtn_Click(sender As Object, e As EventArgs) Handles dltbtn.Click
        If Cpu.FillColor = Color.Black Then
            Dim cpuName As String = TextBox7.SelectedItem
            Dim query As String = "DELETE FROM cpuinfo where cpu=@CPUName"
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CPUName", cpuName)
                    Try
                        conn.Open()
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Record not updated! Try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        ElseIf Gpu.FillColor = Color.Black Then
            Dim gpuName As String = TextBox7.SelectedItem
            Dim query As String = "DELETE FROM gpuinfo where gpu=@GPUName"
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@GPUName", gpuName)
                    Try
                        conn.Open()
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Record not updated! Try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        ElseIf Ram.FillColor = Color.Black Then
            Dim ramName As String = TextBox7.SelectedItem
            Dim query As String = "DELETE FROM raminfo where ramname=@ramName"
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ramName", ramName)
                    Try
                        conn.Open()
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Record not updated! Try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        ElseIf Storage.FillColor = Color.Black Then
            Dim gpuName As String = TextBox7.SelectedItem
            Dim query As String = "DELETE FROM storageinfo where sname=@sName"
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@sName", gpuName)
                    Try
                        conn.Open()
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Record not updated! Try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        ElseIf Cabinet.FillColor = Color.Black Then
            Dim gpuName As String = TextBox7.SelectedItem
            Dim query As String = "DELETE FROM cabinetinfo where Brand=@cabName"
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cabName", gpuName)
                    Try
                        conn.Open()
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Record not updated! Try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        End If

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        rating.Text = ""

    End Sub

    Private Sub TextBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TextBox1.SelectedIndexChanged
        If Cpu.FillColor = Color.Black Then
            LoadCPUDetails(TextBox1.SelectedItem.ToString())
        ElseIf Gpu.FillColor = Color.Black Then
            LoadGPUDetails(TextBox1.SelectedItem.ToString())
        ElseIf Ram.FillColor = Color.Black Then
            LoadRAMDetails(TextBox1.SelectedItem.ToString())
        ElseIf Storage.FillColor = Color.Black Then
            LoadStorageDetails(TextBox1.SelectedItem.ToString())
        ElseIf Cabinet.FillColor = Color.Black Then
            LoadCabinetDetails(TextBox1.SelectedItem.ToString())
        End If
    End Sub
    Private Sub LoadCPUDetails(cpuName As String)
        Dim query As String = "SELECT * FROM cpuinfo WHERE cpu = @name"
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", cpuName)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox2.Text = reader("cores").ToString()
                        TextBox3.Text = reader("threads").ToString()
                        TextBox4.Text = reader("basespeed").ToString()
                        TextBox5.Text = reader("turbospeed").ToString()
                        TextBox6.Text = reader("memory").ToString()
                        TextBox9.Text = reader("price").ToString()
                        rating.Text = reader("rating").ToString
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadGPUDetails(gpuName As String)
        Dim query As String = "SELECT * FROM gpuinfo WHERE gpu = @name"
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", gpuName)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox2.Text = reader("memory").ToString()
                        TextBox3.Text = reader("baseclock").ToString()
                        TextBox4.Text = reader("clockspeed").ToString()
                        TextBox9.Text = reader("price").ToString()
                        rating.Text = reader("rating").ToString
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadRAMDetails(ramName As String)
        Dim query As String = "SELECT * FROM raminfo WHERE ramname = @name"
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", ramName)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox2.Text = reader("ramsize").ToString()
                        TextBox3.Text = reader("ramtype").ToString()
                        TextBox4.Text = reader("quantity").ToString()
                        TextBox5.Text = reader("ramspeed").ToString()
                        TextBox9.Text = reader("price").ToString()
                        rating.Text = reader("rating").ToString
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadStorageDetails(sname As String)
        Dim query As String = "SELECT * FROM storageinfo WHERE sname = @name"
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", sname)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox2.Text = reader("capacity").ToString()
                        TextBox3.Text = reader("cache").ToString()
                        TextBox9.Text = reader("price").ToString()
                        rating.Text = reader("rating").ToString
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadCabinetDetails(brand As String)
        Dim query As String = "SELECT * FROM cabinetinfo WHERE Brand = @name"
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", brand)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox2.Text = reader("panel").ToString()
                        TextBox3.Text = reader("ctype").ToString()
                        TextBox4.Text = reader("color").ToString()
                        TextBox9.Text = reader("price").ToString()
                        rating.Text = reader("rating").ToString
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadNamesToDeleteBox()
        Dim query As String = ""

        If Cpu.FillColor = Color.Black Then
            query = "SELECT cpu FROM cpuinfo"
        ElseIf Gpu.FillColor = Color.Black Then
            query = "SELECT gpu FROM gpuinfo"
        ElseIf Ram.FillColor = Color.Black Then
            query = "SELECT ramname FROM raminfo"
        ElseIf Storage.FillColor = Color.Black Then
            query = "SELECT sname FROM storageinfo"
        ElseIf Cabinet.FillColor = Color.Black Then
            query = "SELECT Brand FROM cabinetinfo"
        End If

        If query <> "" Then
            TextBox7.Items.Clear()
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            TextBox7.Items.Add(reader(0).ToString())
                        End While
                    End Using
                End Using
            End Using
        End If
    End Sub

    Private Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        Guna2TextBox1.BringToFront()
        TextBox1.SendToBack()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        rating.Text = ""
    End Sub

    Private Sub refbtn_Click(sender As Object, e As EventArgs) Handles refbtn.Click
        TextBox1.BringToFront()
        Guna2TextBox1.SendToBack()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        rating.Text = ""
        Guna2TextBox1.Text = ""
        LoadData()
    End Sub
End Class
