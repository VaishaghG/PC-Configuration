Imports FxResources
Imports Microsoft.Identity.Client.Extensions.Msal
Imports Org.BouncyCastle.Asn1
Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection.Metadata
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Reflection
Imports Microsoft.VisualBasic.Constants

Public Class Payment
    Dim hasPaid As Boolean = False
    Public Sub Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PictureBox1.Visible = False
        Button4.Visible = True
        Button3.Visible = True
        Me.ActiveControl = Label2
        Button8.Visible = True
        Button7.Location = New Point(233, 375)
        Button8.Location = New Point(425, 375)
        Button1.PerformClick()

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        ClearTable()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Button7.Location = New Point(329, 353)
        Me.Hide()
        Form3.Show()
        Button8.Visible = True
    End Sub
    Private Sub SetActiveButton(selectedButton As Object)
        ' Reset all buttons to gray
        Button1.FillColor = Color.Gray
        Button2.FillColor = Color.Gray
        Button3.FillColor = Color.Gray
        Button4.FillColor = Color.Gray
        Button8.FillColor = Color.Gray
        Button7.FillColor = Color.Gray
        Guna2Button1.FillColor = Color.Gray
        ' Highlight the selected button
        selectedButton.FillColor = Color.Black
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Stop()
        PictureBox1.Visible = False
        Button4.Visible = True
        Button3.Visible = True
        SetActiveButton(Button1)
        Button7.Location = New Point(233, 375)
        Button8.Location = New Point(425, 375)
        Label3.Visible = True
        Button3.Visible = True
        Button4.Visible = True
        Button7.Visible = True
        Button8.Visible = True

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        hasPaid = True
        SetActiveButton(Button3)
        Me.Hide()
        Dim newFrom As New CreditDebit
        newFrom.Show()
        Button7.Location = New Point(329, 353)
        Button8.Visible = True
    End Sub
    Private Function FormatIndianCurrency(amount As Decimal) As String
        Dim culture As New CultureInfo("en-IN")
        Return "₹" & amount.ToString("N2", culture)
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not hasPaid Then
            MessageBox.Show("Please make payment before proceeding.", "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        SetActiveButton(Button2)
        Label3.Visible = False
        Button3.Visible = False
        Button4.Visible = False
        Button7.Visible = False
        Button8.Visible = False
        Guna2Button1.Visible = False

        Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

        ' Fetch latest cart record
        Dim latestCart As Dictionary(Of String, String) = GetLatestCartRecord(connectionString)
        If latestCart.Count = 0 Then Exit Sub

        ' Prices from respective tables
        Dim cpuPrice As Decimal = GetPrice("cpuinfo", "cpu", latestCart("cpu"), connectionString)
        Dim gpuPrice As Decimal = GetPrice("gpuinfo", "gpu", latestCart("gpu"), connectionString)
        Dim ramPrice As Decimal = GetPrice("raminfo", "ramname", latestCart("ram"), connectionString)
        Dim storagePrice As Decimal = GetPrice("storageinfo", "sname", latestCart("storage"), connectionString)
        Dim cabinetPrice As Decimal = GetPrice("cabinetinfo", "Brand", latestCart("cabinet"), connectionString)

        ' Names
        Dim cpuName As String = latestCart("cpu")
        Dim gpuName As String = latestCart("gpu")
        Dim ramName As String = latestCart("ram")
        Dim storageName As String = latestCart("storage")
        Dim cabinetName As String = latestCart("cabinet")

        ' Subtotal
        Dim subTotal As Decimal = cpuPrice + gpuPrice + ramPrice + storagePrice + cabinetPrice

        ' Save dialog
        Dim saveFileDialog As New SaveFileDialog() With {
        .Filter = "PDF Files|*.pdf",
        .Title = "Save Bill As",
        .FileName = "Generated_Bill.pdf"
    }
        If saveFileDialog.ShowDialog() <> DialogResult.OK Then Exit Sub
        Dim savePath As String = saveFileDialog.FileName

        Try
            Using fs As New FileStream(savePath, FileMode.Create)
                Using doc As New iTextSharp.text.Document(PageSize.A4)
                    Dim writer As PdfWriter = PdfWriter.GetInstance(doc, fs)
                    doc.Open()

                    ' Fonts
                    Dim titleFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18)
                    Dim labelFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 12)
                    Dim headerFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)
                    Dim dataFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 12)
                    Dim topTitleFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)

                    ' Header with customer info and PC CONFIG


                    Dim headerTable As New PdfPTable(2)
                    headerTable.WidthPercentage = 100
                    headerTable.SetWidths(New Single() {70, 30})

                    ' Left cell (Customer Info)


                    ' Right cell (PC CONFIG title)
                    Dim baseFont As BaseFont = BaseFont.CreateFont(
                    BaseFont.HELVETICA,
                    BaseFont.WINANSI,
                    BaseFont.NOT_EMBEDDED
                    )
                    Dim titleFont1 As New iTextSharp.text.Font(baseFont, 18, Font.Bold)
                    Dim titlePara As New Paragraph("PC CONFIG.", titleFont1)
                    titlePara.Alignment = Element.ALIGN_CENTER
                    doc.Add(titlePara)
                    doc.Add(New Paragraph(Environment.NewLine))

                    Dim customerName As String = GlobalVariables.CurrentCustomerName
                    Dim currentDate As String = DateTime.Now.ToString("dd-MM-yyyy")
                    Dim infoPara As New Paragraph("Date: " & currentDate & vbCrLf & "Customer Name: " & customerName, labelFont)
                    infoPara.Alignment = Element.ALIGN_LEFT
                    doc.Add(infoPara)
                    doc.Add(New Paragraph(Environment.NewLine))

                    ' Order Summary Title (Centered, Bold)
                    Dim orderSummaryFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20)
                    Dim orderSummaryPara As New Paragraph("Order Summary", orderSummaryFont)
                    orderSummaryPara.Alignment = Element.ALIGN_CENTER
                    doc.Add(orderSummaryPara)
                    doc.Add(New Paragraph(Environment.NewLine))



                    ' Order Table
                    Dim table As New PdfPTable(2)
                    table.WidthPercentage = 70
                    table.HorizontalAlignment = Element.ALIGN_CENTER
                    table.SetWidths(New Single() {3, 2})

                    ' Table Headers
                    table.AddCell(New PdfPCell(New Phrase("ITEM", headerFont)) With {
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .VerticalAlignment = Element.ALIGN_MIDDLE
                })
                    table.AddCell(New PdfPCell(New Phrase("PRICE (Rs.)", headerFont)) With {
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .VerticalAlignment = Element.ALIGN_MIDDLE
                })

                    ' Table Data
                    Dim items() As String = {cpuName, gpuName, ramName, storageName, cabinetName}
                    Dim prices() As Decimal = {cpuPrice, gpuPrice, ramPrice, storagePrice, cabinetPrice}

                    For i = 0 To items.Length - 1
                        table.AddCell(New PdfPCell(New Phrase(items(i), dataFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                        table.AddCell(New PdfPCell(New Phrase(FormatIndianCurrency(prices(i)), dataFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                    Next

                    ' Subtotal Row
                    table.AddCell(New PdfPCell(New Phrase("SUB TOTAL", headerFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                    table.AddCell(New PdfPCell(New Phrase(FormatIndianCurrency(subTotal), headerFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})

                    doc.Add(table)
                    doc.Close()
                End Using
            End Using

            MessageBox.Show("Bill saved at: " & savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error generating bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





    Private Function GetLatestCartRecord(connectionString As String) As Dictionary(Of String, String)
        Dim latestRecord As New Dictionary(Of String, String)

        Dim query As String = "SELECT TOP 1 cpu, gpu, ram, storage, cabinet FROM cart ORDER BY (SELECT NULL) DESC"

        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                conn.Open()
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    Using reader As Microsoft.Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            latestRecord("cpu") = reader("cpu").ToString()
                            latestRecord("gpu") = reader("gpu").ToString()
                            latestRecord("ram") = reader("ram").ToString()
                            latestRecord("storage") = reader("storage").ToString()
                            latestRecord("cabinet") = reader("cabinet").ToString()
                        Else
                            MessageBox.Show("No records found in cart!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching latest cart record: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return latestRecord
    End Function



    Private Function GetLatestRecord(connectionString As String) As Dictionary(Of String, String)
        Dim latestRecord As New Dictionary(Of String, String)

        Dim query As String = "
        SELECT TOP 1  cpu, gpu, ram, storage, cabinet
        FROM custorder
        ORDER BY (SELECT NULL) DESC"

        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                conn.Open()
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    Using reader As Microsoft.Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            latestRecord("cpu") = reader("cpu").ToString()
                            latestRecord("gpu") = reader("gpu").ToString()
                            latestRecord("ram") = reader("ram").ToString()
                            latestRecord("storage") = reader("storage").ToString()
                            latestRecord("cabinet") = reader("cabinet").ToString()
                        Else
                            MessageBox.Show("No orders found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching latest order: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return latestRecord
    End Function


    Private Function GetPrice(tableName As String, columnName As String, itemValue As String, connectionString As String) As Decimal
        Dim price As Decimal = 0

        Dim query As String = $"SELECT price FROM {tableName} WHERE {columnName} = @itemValue"

        Try
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
                conn.Open()
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@itemValue", itemValue)

                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        price = Convert.ToDecimal(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching price: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return price
    End Function





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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        hasPaid = True
        SetActiveButton(Button4)
        Button8.Visible = True
        Button7.Location = New Point(233, 375)

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim username As String = Form1.TextBox1.Text.Trim()
        SaveLoginInfo(username)
        MessageBox.Show("Payment process complete! Now you can generate bill", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        hasPaid = True
        SetActiveButton(Guna2Button1)
        PictureBox1.Visible = True
        Button4.Visible = False
        Button3.Visible = False
        Button8.Visible = False
        Button7.Location = New Point(333, 375)
        PictureBox1.Image = System.Drawing.Image.FromFile("C:\Users\vaisa\OneDrive\Pictures\Screenshots\testqr.png")
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Timer1.Interval = 8000 ' 5 seconds delay
        Timer1.Start()
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim connString As String = "Server=JARVIS;Database=mydb2;Integrated Security=True;TrustServerCertificate=True"
        Timer1.Stop() ' Stop the timer

        MessageBox.Show("Payment Process Complete!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Dim username As String = Form1.TextBox1.Text.Trim()
        SaveLoginInfo(username)
        MessageBox.Show("Payment process complete! Now you can generate bill", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        hasPaid = False
        ' Stop the timer when Button7 is clicked
        If Timer1.Enabled Then
            Timer1.Stop()
            MessageBox.Show("Payment process canceled!", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Button7.Location = New Point(329, 353)
        Button8.Visible = True
        Me.Hide()
        Cart.Show()
    End Sub
End Class
