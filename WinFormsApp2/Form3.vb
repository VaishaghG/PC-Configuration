Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Data.SqlClient
Imports System.IO
Imports System.Drawing.Imaging
Public Class Form3
    ' ✅ Connection String (Ensure it's correct)
    Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.TextBox1.Text = CurrentCustomerName
        ' Ensures all UI updates are processed

        ' Debugging: Print the latest TextBox1 value
        Debug.WriteLine("Opening OrderForm for customer: " & Form1.TextBox1.Text)
        ' ✅ Automatically load CPU list and pre-select CPU button
        Cpu.PerformClick()

        ' ✅ Remove selection outline from ListBox
        ListBox1.ClearSelected()
        Me.ActiveControl = Label8 ' Moves focus to another control

        ' ✅ Initialize DataGridView with columns (keeps structure even when empty)

        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 5
        ProgressBar1.Value = 0
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ClearTable()
    End Sub

    Private Sub ListBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox1.DrawItem
        If e.Index < 0 Then Exit Sub

        e.DrawBackground()
        Dim itemText As String = ListBox1.Items(e.Index).ToString()
        Dim textSize As SizeF = e.Graphics.MeasureString(itemText, e.Font)

        ' ✅ Calculate X position to center text
        Dim x As Integer = (e.Bounds.Width - textSize.Width) / 2
        Dim y As Integer = e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2

        ' ✅ Draw centered text
        TextRenderer.DrawText(e.Graphics, itemText, e.Font, New Point(x, y), e.ForeColor)

        e.DrawFocusRectangle()

        If e.Index < 0 Then Exit Sub

        Dim g As Graphics = e.Graphics

        Dim bgColor As Color
        Dim textColor As Color

        ' ✅ Check if the item is selected
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            bgColor = Color.Black ' Custom selection background color
        Else
            bgColor = ListBox1.BackColor ' Default background color
        End If

        ' ✅ Invert the color of the text based on the background
        textColor = Color.FromArgb(255 - bgColor.R, 255 - bgColor.G, 255 - bgColor.B)

        ' Fill background
        g.FillRectangle(New SolidBrush(bgColor), e.Bounds)

        ' Draw text with the inverted color
        TextRenderer.DrawText(g, itemText, e.Font, e.Bounds, textColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

        e.DrawFocusRectangle()
    End Sub

    ' ✅ Function to Setup DataGridView Columns
    Private Sub SetupDataGridView1()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns.Clear()

        ' Define Columns (So DataGridView Structure is always visible)
        Dim col1 As New DataGridViewTextBoxColumn With {.Name = "cores", .HeaderText = "Cores"}
        Dim col2 As New DataGridViewTextBoxColumn With {.Name = "threads", .HeaderText = "Threads"}
        Dim col3 As New DataGridViewTextBoxColumn With {.Name = "basespeed", .HeaderText = "Base Speed (GHz)"}
        Dim col4 As New DataGridViewTextBoxColumn With {.Name = "turbospeed", .HeaderText = "Turbo Speed (GHz)"}
        Dim col5 As New DataGridViewTextBoxColumn With {.Name = "memory", .HeaderText = "Memory Type"}

        ' Add columns to DataGridView
        DataGridView1.Columns.AddRange({col1, col2, col3, col4, col5})

        ' ✅ Disable selection highlight
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.ClearSelection()
    End Sub
    Private Sub SetupDataGridView2()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns.Clear()

        ' Define Columns (So DataGridView Structure is always visible)
        Dim col1 As New DataGridViewTextBoxColumn With {.Name = "memory", .HeaderText = "Memory"}
        Dim col2 As New DataGridViewTextBoxColumn With {.Name = "baseclock", .HeaderText = "Base Clock (Mhz)"}
        Dim col3 As New DataGridViewTextBoxColumn With {.Name = "clockspeed", .HeaderText = "Clock Speed (MHz)"}

        ' Add columns to DataGridView
        DataGridView1.Columns.AddRange({col1, col2, col3})

        ' ✅ Disable selection highlight
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.ClearSelection()
    End Sub
    Private Sub SetupDataGridView3()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns.Clear()

        ' Define Columns (So DataGridView Structure is always visible)
        Dim col1 As New DataGridViewTextBoxColumn With {.Name = "ramsize", .HeaderText = "Ram Size (GB)"}
        Dim col2 As New DataGridViewTextBoxColumn With {.Name = "ramtype", .HeaderText = "Ram Type"}
        Dim col3 As New DataGridViewTextBoxColumn With {.Name = "quantity", .HeaderText = "Quantity"}
        Dim col4 As New DataGridViewTextBoxColumn With {.Name = "ramspeed", .HeaderText = "Ram Speed (MHz)"}

        ' Add columns to DataGridView
        DataGridView1.Columns.AddRange({col1, col2, col3, col4})

        ' ✅ Disable selection highlight
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.ClearSelection()
    End Sub
    Private Sub SetupDataGridView4()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns.Clear()

        ' Define Columns (So DataGridView Structure is always visible)
        Dim col1 As New DataGridViewTextBoxColumn With {.Name = "capacity", .HeaderText = "Capacity"}
        Dim col2 As New DataGridViewTextBoxColumn With {.Name = "cache", .HeaderText = "Cache Memory (MB)"}

        ' Add columns to DataGridView
        DataGridView1.Columns.AddRange({col1, col2})

        ' ✅ Disable selection highlight
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.ClearSelection()
    End Sub
    Private Sub SetupDataGridView5()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns.Clear()

        ' Define Columns (So DataGridView Structure is always visible)
        Dim col1 As New DataGridViewTextBoxColumn With {.Name = "panel", .HeaderText = "Side Panel"}
        Dim col2 As New DataGridViewTextBoxColumn With {.Name = "ctype", .HeaderText = "Cabinet Type"}
        Dim col3 As New DataGridViewTextBoxColumn With {.Name = "color", .HeaderText = "Colour"}
        ' Add columns to DataGridView
        DataGridView1.Columns.AddRange({col1, col2, col3})

        ' ✅ Disable selection highlight
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.ClearSelection()
    End Sub

    ' ✅ Logout button functionality
    Private Sub Logoutbtn_Click(sender As Object, e As EventArgs) Handles Logoutbtn.Click
        Me.Hide()
        Dim newForm As New Form1()
        newForm.Show()
    End Sub

    ' ✅ CPU Button Click - Loads CPU List
    Private Sub Cpu_Click(sender As Object, e As EventArgs) Handles Cpu.Click
        SetActiveButton(Cpu)
        SetupDataGridView1()


        ' Populate ListBox with CPU names
        ListBox1.Items.Clear()
        Dim query As String = "SELECT cpu FROM cpuinfo" ' Change to your table & column name

        ' Create connection and command
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    ' Loop through the data and add it to the ListBox
                    While reader.Read()
                        ListBox1.Items.Add(reader("cpu").ToString())
                    End While

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using

        ' Clear text & DataGridView rows (but keep headers)
        TextBox1.Text = ""
        DataGridView1.Rows.Clear()

    End Sub

    ' ✅ Other category button click handlers
    Private Sub Gpu_Click(sender As Object, e As EventArgs) Handles Gpu.Click
        SetActiveButton(Gpu)
        SetupDataGridView2()
        ' Populate ListBox with GPU names
        ListBox1.Items.Clear()
        Dim query As String = "SELECT gpu FROM gpuinfo" ' Change to your table & column name

        ' Create connection and command
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    ' Loop through the data and add it to the ListBox
                    While reader.Read()
                        ListBox1.Items.Add(reader("gpu").ToString())
                    End While

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using

        ' Clear text & DataGridView rows (but keep headers)
        TextBox1.Text = ""
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Storage_Click(sender As Object, e As EventArgs) Handles Storage.Click
        SetActiveButton(Storage)
        SetupDataGridView4()
        ' Populate ListBox with STORAGE names
        ListBox1.Items.Clear()
        Dim query As String = "SELECT sname FROM storageinfo" ' Change to your table & column name

        ' Create connection and command
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    ' Loop through the data and add it to the ListBox
                    While reader.Read()
                        ListBox1.Items.Add(reader("sname").ToString())
                    End While

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using

        ' Clear text & DataGridView rows (but keep headers)
        TextBox1.Text = ""
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Ram_Click(sender As Object, e As EventArgs) Handles Ram.Click
        SetActiveButton(Ram)
        SetupDataGridView3()
        ' Populate ListBox with RAM names
        ListBox1.Items.Clear()
        Dim query As String = "SELECT ramname FROM raminfo" ' Change to your table & column name

        ' Create connection and command
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    ' Loop through the data and add it to the ListBox
                    While reader.Read()
                        ListBox1.Items.Add(reader("ramname").ToString())
                    End While

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using

        ' Clear text & DataGridView rows (but keep headers)
        TextBox1.Text = ""
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Cabinet_Click(sender As Object, e As EventArgs) Handles Cabinet.Click
        SetActiveButton(Cabinet)
        SetupDataGridView5()
        ' Populate ListBox with RAM names
        ListBox1.Items.Clear()
        Dim query As String = "SELECT Brand FROM cabinetinfo" ' Change to your table & column name

        ' Create connection and command
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    ' Loop through the data and add it to the ListBox
                    While reader.Read()
                        ListBox1.Items.Add(reader("Brand").ToString())
                    End While

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using

        ' Clear text & DataGridView rows (but keep headers)
        TextBox1.Text = ""
        DataGridView1.Rows.Clear()
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

        ' ✅ Clear ListBox, TextBox, and DataGridView rows
        ListBox1.Items.Clear()
        TextBox1.Text = ""
        DataGridView1.Rows.Clear() ' Only clears rows, not columns
        ProgressBar1.Value = 0
        Label3.Text = "0/0"
        PictureBox1.Image = Nothing
        Label9.Text = "PRICE : ₹0.00"
    End Sub

    ' ✅ Load Data into DataGridView when an item is selected from ListBox
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If Cpu.FillColor = Color.Black Then
            If ListBox1.SelectedItem Is Nothing Then Exit Sub

            ' Disable row selection
            DataGridView1.ClearSelection()
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.CurrentCell = Nothing

            ' Display selected CPU in TextBox
            TextBox1.Text = ListBox1.SelectedItem.ToString()
            Dim selectedCPU As String = ListBox1.SelectedItem.ToString().Trim()

            ' ✅ SQL Query to fetch CPU details including image binary data
            Dim query As String = "SELECT cores, threads, basespeed, turbospeed, memory, imgpath, rating, price FROM cpuinfo WHERE cpu = @cpu"

            ' ✅ Database Connection
            Using connection As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@cpu", selectedCPU)

                Try
                    ' ✅ Open Connection
                    connection.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' ✅ Populate DataGridView (Without Auto-Selection)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.Add(reader("cores"), reader("threads"), reader("basespeed"), reader("turbospeed"), reader("memory"))

                        ' Clear Selection Again
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = Nothing

                        ' ✅ Fetch and convert binary image data
                        If Not reader.IsDBNull(reader.GetOrdinal("imgpath")) Then
                            Dim imgData As Byte() = DirectCast(reader("imgpath"), Byte())

                            If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
                                Using ms As New MemoryStream(imgData)
                                    PictureBox1.Image = Image.FromStream(ms)
                                End Using
                            Else
                                PictureBox1.Image = Nothing ' Default to no image
                            End If
                        Else
                            PictureBox1.Image = Nothing ' No image in database
                        End If
                        Dim rating As Decimal = 0
                        If Not reader.IsDBNull(reader.GetOrdinal("rating")) Then
                            rating = Convert.ToDecimal(reader("rating"))
                            Label3.Text = rating.ToString() & "/5"
                        Else
                            Label3.Text = "N/A"
                        End If

                        ' ✅ Set ProgressBar based on Rating (Convert rating out of 5 to percentage)
                        ProgressBar1.Maximum = 100 ' Set max value to 100
                        ProgressBar1.Value = CInt((rating / 5) * 100) ' Convert 5-star rating to percentage

                        ' ✅ Fetch price and format it for Label9
                        If Not reader.IsDBNull(reader.GetOrdinal("price")) Then
                            Dim price As Decimal = Convert.ToDecimal(reader("price"))
                            Label9.Text = "PRICE : ₹" & price.ToString("N0", Globalization.CultureInfo.CreateSpecificCulture("en-IN")) ' Formats as ₹xx,xxx.xx
                        Else
                            Label9.Text = "PRICE : ₹0.00"
                        End If
                    Else
                        MessageBox.Show("CPU not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("❌ Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
            'gpu code

        ElseIf Gpu.FillColor = Color.Black Then
            If ListBox1.SelectedItem Is Nothing Then Exit Sub

            ' Disable row selection
            DataGridView1.ClearSelection()
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.CurrentCell = Nothing

            ' Display selected CPU in TextBox
            TextBox1.Text = ListBox1.SelectedItem.ToString()
            Dim selectedGPU As String = ListBox1.SelectedItem.ToString().Trim()

            ' ✅ SQL Query to fetch CPU details including image binary data
            Dim query As String = "SELECT memory, baseclock, clockspeed, imgpath, price, rating FROM gpuinfo WHERE gpu = @gpu"

            ' ✅ Database Connection
            Using connection As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@gpu", selectedGPU)

                Try
                    ' ✅ Open Connection
                    connection.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' ✅ Populate DataGridView (Without Auto-Selection)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.Add(reader("memory"), reader("baseclock"), reader("clockspeed"), reader("imgpath"))

                        ' Clear Selection Again
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = Nothing

                        ' ✅ Fetch and convert binary image data
                        If Not reader.IsDBNull(reader.GetOrdinal("imgpath")) Then
                            Dim imgData As Byte() = DirectCast(reader("imgpath"), Byte())

                            If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
                                Using ms As New MemoryStream(imgData)
                                    PictureBox1.Image = Image.FromStream(ms)
                                End Using
                            Else
                                PictureBox1.Image = Nothing ' Default to no image
                            End If
                        Else
                            PictureBox1.Image = Nothing ' No image in database
                        End If
                        Dim rating As Decimal = 0
                        If Not reader.IsDBNull(reader.GetOrdinal("rating")) Then
                            rating = Convert.ToDecimal(reader("rating"))
                            Label3.Text = rating.ToString() & "/5"
                        Else
                            Label3.Text = "N/A"
                        End If

                        ' ✅ Set ProgressBar based on Rating (Convert rating out of 5 to percentage)
                        ProgressBar1.Maximum = 100 ' Set max value to 100
                        ProgressBar1.Value = CInt((rating / 5) * 100) ' Convert 5-star rating to percentage

                        ' ✅ Fetch price and format it for Label9
                        If Not reader.IsDBNull(reader.GetOrdinal("price")) Then
                            Dim price As Decimal = Convert.ToDecimal(reader("price"))
                            Label9.Text = "PRICE : ₹" & price.ToString("N2") ' Formats as ₹xx,xxx.xx
                        Else
                            Label9.Text = "PRICE : ₹0.00"
                        End If
                    Else
                        MessageBox.Show("GPU not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("❌ Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using

        ElseIf Ram.FillColor = Color.Black Then
            If ListBox1.SelectedItem Is Nothing Then Exit Sub

            ' Disable row selection
            DataGridView1.ClearSelection()
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.CurrentCell = Nothing

            ' Display selected CPU in TextBox
            TextBox1.Text = ListBox1.SelectedItem.ToString()
            Dim selectedRAM As String = ListBox1.SelectedItem.ToString().Trim()

            ' ✅ SQL Query to fetch CPU details including image binary data
            Dim query As String = "SELECT ramsize, ramtype, quantity, ramspeed, imgpath, price, rating FROM raminfo WHERE ramname = @ramname"

            ' ✅ Database Connection
            Using connection As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ramname", selectedRAM)

                Try
                    ' ✅ Open Connection
                    connection.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' ✅ Populate DataGridView (Without Auto-Selection)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.Add(reader("ramsize"), reader("ramtype"), reader("quantity"), reader("ramspeed"), reader("imgpath"))

                        ' Clear Selection Again
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = Nothing

                        ' ✅ Fetch and convert binary image data
                        If Not reader.IsDBNull(reader.GetOrdinal("imgpath")) Then
                            Dim imgData As Byte() = DirectCast(reader("imgpath"), Byte())

                            If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
                                Using ms As New MemoryStream(imgData)
                                    PictureBox1.Image = Image.FromStream(ms)
                                End Using
                            Else
                                PictureBox1.Image = Nothing ' Default to no image
                            End If
                        Else
                            PictureBox1.Image = Nothing ' No image in database
                        End If
                        Dim rating As Decimal = 0
                        If Not reader.IsDBNull(reader.GetOrdinal("rating")) Then
                            rating = Convert.ToDecimal(reader("rating"))
                            Label3.Text = rating.ToString() & "/5"
                        Else
                            Label3.Text = "N/A"
                        End If

                        ' ✅ Set ProgressBar based on Rating (Convert rating out of 5 to percentage)
                        ProgressBar1.Maximum = 100 ' Set max value to 100
                        ProgressBar1.Value = CInt((rating / 5) * 100) ' Convert 5-star rating to percentage

                        ' ✅ Fetch price and format it for Label9
                        If Not reader.IsDBNull(reader.GetOrdinal("price")) Then
                            Dim price As Decimal = Convert.ToDecimal(reader("price"))
                            Label9.Text = "PRICE : ₹" & price.ToString("N2") ' Formats as ₹xx,xxx.xx
                        Else
                            Label9.Text = "PRICE : ₹0.00"
                        End If
                    Else
                        MessageBox.Show("RAM Name not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("❌ Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using

        ElseIf Storage.FillColor = Color.Black Then
            If ListBox1.SelectedItem Is Nothing Then Exit Sub

            ' Disable row selection
            DataGridView1.ClearSelection()
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.CurrentCell = Nothing

            ' Display selected CPU in TextBox
            TextBox1.Text = ListBox1.SelectedItem.ToString()
            Dim selectedSTORAGE As String = ListBox1.SelectedItem.ToString().Trim()

            ' ✅ SQL Query to fetch CPU details including image binary data
            Dim query As String = "SELECT capacity, cache, imgpath, price, rating FROM storageinfo WHERE sname = @name"

            ' ✅ Database Connection
            Using connection As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@name", selectedSTORAGE)

                Try
                    ' ✅ Open Connection
                    connection.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' ✅ Populate DataGridView (Without Auto-Selection)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.Add(reader("capacity"), reader("cache"), reader("imgpath"))

                        ' Clear Selection Again
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = Nothing

                        ' ✅ Fetch and convert binary image data
                        If Not reader.IsDBNull(reader.GetOrdinal("imgpath")) Then
                            Dim imgData As Byte() = DirectCast(reader("imgpath"), Byte())

                            If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
                                Using ms As New MemoryStream(imgData)
                                    PictureBox1.Image = Image.FromStream(ms)
                                End Using
                            Else
                                PictureBox1.Image = Nothing ' Default to no image
                            End If
                        Else
                            PictureBox1.Image = Nothing ' No image in database
                        End If
                        Dim rating As Decimal = 0
                        If Not reader.IsDBNull(reader.GetOrdinal("rating")) Then
                            rating = Convert.ToDecimal(reader("rating"))
                            Label3.Text = rating.ToString() & "/5"
                        Else
                            Label3.Text = "N/A"
                        End If

                        ' ✅ Set ProgressBar based on Rating (Convert rating out of 5 to percentage)
                        ProgressBar1.Maximum = 100 ' Set max value to 100
                        ProgressBar1.Value = CInt((rating / 5) * 100) ' Convert 5-star rating to percentage

                        ' ✅ Fetch price and format it for Label9
                        If Not reader.IsDBNull(reader.GetOrdinal("price")) Then
                            Dim price As Decimal = Convert.ToDecimal(reader("price"))
                            Label9.Text = "PRICE : ₹" & price.ToString("N2") ' Formats as ₹xx,xxx.xx
                        Else
                            Label9.Text = "PRICE : ₹0.00"
                        End If
                    Else
                        MessageBox.Show("Storage Name not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("❌ Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using

        ElseIf Cabinet.FillColor = Color.Black Then
            If ListBox1.SelectedItem Is Nothing Then Exit Sub

            ' Disable row selection
            DataGridView1.ClearSelection()
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.CurrentCell = Nothing

            ' Display selected CPU in TextBox
            TextBox1.Text = ListBox1.SelectedItem.ToString()
            Dim selectedCABINET As String = ListBox1.SelectedItem.ToString().Trim()

            ' ✅ SQL Query to fetch CPU details including image binary data
            Dim query As String = "SELECT panel, ctype, color, imgpath, price, rating FROM cabinetinfo WHERE Brand = @brand"

            ' ✅ Database Connection
            Using connection As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@brand", selectedCABINET)

                Try
                    ' ✅ Open Connection
                    connection.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' ✅ Populate DataGridView (Without Auto-Selection)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.Add(reader("panel"), reader("ctype"), reader("color"), reader("imgpath"))

                        ' Clear Selection Again
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = Nothing

                        ' ✅ Fetch and convert binary image data
                        If Not reader.IsDBNull(reader.GetOrdinal("imgpath")) Then
                            Dim imgData As Byte() = DirectCast(reader("imgpath"), Byte())

                            If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
                                Using ms As New MemoryStream(imgData)
                                    PictureBox1.Image = Image.FromStream(ms)
                                End Using
                            Else
                                PictureBox1.Image = Nothing ' Default to no image
                            End If
                        Else
                            PictureBox1.Image = Nothing ' No image in database
                        End If
                        Dim rating As Decimal = 0
                        If Not reader.IsDBNull(reader.GetOrdinal("rating")) Then
                            rating = Convert.ToDecimal(reader("rating"))
                            Label3.Text = rating.ToString() & "/5"
                        Else
                            Label3.Text = "N/A"
                        End If

                        ' ✅ Set ProgressBar based on Rating (Convert rating out of 5 to percentage)
                        ProgressBar1.Maximum = 100 ' Set max value to 100
                        ProgressBar1.Value = CInt((rating / 5) * 100) ' Convert 5-star rating to percentage

                        ' ✅ Fetch price and format it for Label9
                        If Not reader.IsDBNull(reader.GetOrdinal("price")) Then
                            Dim price As Decimal = Convert.ToDecimal(reader("price"))
                            Label9.Text = "PRICE : ₹" & price.ToString("N2") ' Formats as ₹xx,xxx.xx
                        Else
                            Label9.Text = "PRICE : ₹0.00"
                        End If
                    Else
                        MessageBox.Show("GPU not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("❌ Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
        ' Ensure an item is selected


    End Sub
    Private Sub Orderbtn_Click(sender As Object, e As EventArgs) Handles Orderbtn.Click
        ' ✅ Ensure that TextBox1 is not empty
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Select an item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' ✅ Assign values only if the respective button is selected
        Dim cpuValue As String = If(Cpu.FillColor = Color.Black, TextBox1.Text.Trim(), Nothing)
        Dim gpuValue As String = If(Gpu.FillColor = Color.Black, TextBox1.Text.Trim(), Nothing)
        Dim storageValue As String = If(Storage.FillColor = Color.Black, TextBox1.Text.Trim(), Nothing)
        Dim ramValue As String = If(Ram.FillColor = Color.Black, TextBox1.Text.Trim(), Nothing)
        Dim cabinetValue As String = If(Cabinet.FillColor = Color.Black, TextBox1.Text.Trim(), Nothing)

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' ✅ Check if an order row already exists
            Dim checkQuery As String = "SELECT COUNT(*) FROM cart"
            Dim commandCheck As New SqlCommand(checkQuery, connection)
            Dim orderExists As Boolean = Convert.ToInt32(commandCheck.ExecuteScalar()) > 0

            If orderExists Then
                ' ✅ Update existing row, keeping previous values intact
                Dim updateQuery As String = "UPDATE cart SET 
                    cpu = COALESCE(@cpu, cpu), 
                    gpu = COALESCE(@gpu, gpu), 
                    storage = COALESCE(@storage, storage), 
                    ram = COALESCE(@ram, ram), 
                    cabinet = COALESCE(@cabinet, cabinet)"

                Dim commandUpdate As New SqlCommand(updateQuery, connection)
                commandUpdate.Parameters.AddWithValue("@cpu", If(cpuValue, DBNull.Value))
                commandUpdate.Parameters.AddWithValue("@gpu", If(gpuValue, DBNull.Value))
                commandUpdate.Parameters.AddWithValue("@storage", If(storageValue, DBNull.Value))
                commandUpdate.Parameters.AddWithValue("@ram", If(ramValue, DBNull.Value))
                commandUpdate.Parameters.AddWithValue("@cabinet", If(cabinetValue, DBNull.Value))
                commandUpdate.ExecuteNonQuery()

            Else
                ' ✅ Insert new row
                Dim insertQuery As String = "INSERT INTO cart (cpu, gpu, storage, ram, cabinet) VALUES (@cpu, @gpu, @storage, @ram, @cabinet)"
                Dim commandInsert As New SqlCommand(insertQuery, connection)
                commandInsert.Parameters.AddWithValue("@cpu", If(cpuValue, DBNull.Value))
                commandInsert.Parameters.AddWithValue("@gpu", If(gpuValue, DBNull.Value))
                commandInsert.Parameters.AddWithValue("@storage", If(storageValue, DBNull.Value))
                commandInsert.Parameters.AddWithValue("@ram", If(ramValue, DBNull.Value))
                commandInsert.Parameters.AddWithValue("@cabinet", If(cabinetValue, DBNull.Value))
                commandInsert.ExecuteNonQuery()
            End If

            MessageBox.Show("Item added to Cart successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None)
        End Using
    End Sub

    Private Sub Cartbtn_Click(sender As Object, e As EventArgs) Handles Cartbtn.Click
        If IsCabinetSelected() Then
            Hide()
            Dim newForm As New Cart
            newForm.Show()
        Else
            MessageBox.Show("Please add a cabinet to cart before proceeding.", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' 🔥 Function to check if cabinet is selected
    Private Function IsCabinetSelected() As Boolean
        Dim isSelected As Boolean = False
        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                Dim query As String = "SELECT COUNT(*) FROM cart WHERE cabinet IS NOT NULL AND cabinet <> ''"
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    conn.Open()
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        isSelected = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking cabinet selection: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return isSelected
    End Function

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.Hide()
        Dim newForm As New Orders
        newForm.Show()
        Debug.WriteLine("Opening OrderForm for customer: " & Form1.TextBox1.Text)
    End Sub
End Class
