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
Imports System.Drawing
Imports Windows.Win32.UI.Input
Imports iTextSharp.text.pdf.qrcode

' Required for Indian Number Formatting

Public Class Orders

    ' Define the connection string to connect to the database
    Dim connString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"

    Private Sub DisplayRecords()
        ' Clear DataGridView before fetching new record

        DataGridView1.Rows.Clear()
        Form1.TextBox1.Text = CurrentCustomerName

        ' Get the latest customer name from Form1's TextBox1
        Dim customerName As String = Form1.TextBox1.Text


        ' Debugging: Check if customerName updates correctly


        ' If customer name is empty, clear DataGridView and exit
        If String.IsNullOrEmpty(customerName) Then
            Debug.WriteLine("No customer name found. Exiting...")
            Exit Sub
        End If

        ' SQL query to fetch records only for the given customer
        Dim query As String = "SELECT co.cpu, co.gpu, co.storage, co.ram, co.cabinet, ci.imgpath, 
                              cpu.price AS cpu_price, gpu.price AS gpu_price, 
                              storage.price AS storage_price, ram.price AS ram_price, 
                              cabinet.price AS cabinet_price
                           FROM custorder co
                           INNER JOIN cabinetinfo ci ON co.cabinet = ci.Brand 
                           INNER JOIN cpuinfo cpu ON co.cpu = cpu.cpu
                           INNER JOIN gpuinfo gpu ON co.gpu = gpu.gpu
                           INNER JOIN storageinfo storage ON co.storage = storage.sname
                           INNER JOIN raminfo ram ON co.ram = ram.ramname
                           INNER JOIN cabinetinfo cabinet ON co.cabinet = cabinet.Brand
                           WHERE co.custname = @custname
                           ORDER BY co.row_num DESC;"

        ' Open database connection
        Using conn As New Microsoft.Data.SqlClient.SqlConnection(connString)
            Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@custname", customerName) ' Use dynamic value

                conn.Open()

                Using reader As Microsoft.Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
                    ' If no records found, clear DataGridView and exit
                    If Not reader.HasRows Then
                        Debug.WriteLine("No orders found for: " & customerName)
                        DataGridView1.Rows.Clear()
                        Exit Sub
                    End If

                    ' Read and populate DataGridView
                    While reader.Read()
                        Dim cpu As String = reader("cpu").ToString()
                        Dim gpu As String = reader("gpu").ToString()
                        Dim storage As String = reader("storage").ToString()
                        Dim ram As String = reader("ram").ToString()
                        Dim cabinet As String = reader("cabinet").ToString()

                        ' Handle image conversion
                        Dim imageData As Byte() = If(IsDBNull(reader("imgpath")), Nothing, CType(reader("imgpath"), Byte()))
                        Dim img As System.Drawing.Image = ConvertBinaryToImage(imageData)
                        Dim resizedImg As System.Drawing.Image = ResizeImageToFitWidth(img, 100, 100)

                        ' Read and store prices
                        Dim cpuPrice As Integer = If(IsDBNull(reader("cpu_price")), 0, reader("cpu_price"))
                        Dim gpuPrice As Integer = If(IsDBNull(reader("gpu_price")), 0, reader("gpu_price"))
                        Dim storagePrice As Integer = If(IsDBNull(reader("storage_price")), 0, reader("storage_price"))
                        Dim ramPrice As Integer = If(IsDBNull(reader("ram_price")), 0, reader("ram_price"))
                        Dim cabinetPrice As Integer = If(IsDBNull(reader("cabinet_price")), 0, reader("cabinet_price"))

                        ' Add row to DataGridView
                        Dim rowIndex As Integer = DataGridView1.Rows.Add()
                        DataGridView1.Rows(rowIndex).Cells(0).Value = resizedImg
                        DataGridView1.Rows(rowIndex).Cells(1).Value = cpu
                        DataGridView1.Rows(rowIndex).Cells(2).Value = gpu
                        DataGridView1.Rows(rowIndex).Cells(3).Value = storage
                        DataGridView1.Rows(rowIndex).Cells(4).Value = ram
                        DataGridView1.Rows(rowIndex).Cells(5).Value = cabinet

                        ' Store prices in Tag property for later use
                        DataGridView1.Rows(rowIndex).Tag = New Integer() {cpuPrice, gpuPrice, storagePrice, ramPrice, cabinetPrice}

                    End While
                End Using
            End Using
        End Using

        ' Ensure DataGridView is refreshed
        UpdateGridLayout()
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.Refresh()
        DataGridView1.ClearSelection()

    End Sub






    ' Convert binary data to an Image
    Function ConvertBinaryToImage(ByVal byteArray As Byte()) As System.Drawing.Image
        Try
            If byteArray Is Nothing OrElse byteArray.Length = 0 Then
                Return Nothing
            End If
            Using ms As New MemoryStream(byteArray)
                Return System.Drawing.Image.FromStream(ms)
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Resize image while maintaining aspect ratio
    Private Function ResizeImageToFitWidth(ByVal img As System.Drawing.Image, ByVal cellWidth As Integer, ByVal cellHeight As Integer) As System.Drawing.Image
        If img Is Nothing Then Return Nothing

        Dim newWidth As Integer = cellWidth
        Dim newHeight As Integer = CInt(img.Height * (newWidth / img.Width))

        ' Ensure the image does not exceed the cell height
        If newHeight > cellHeight Then
            newHeight = cellHeight
            newWidth = CInt(img.Width * (newHeight / img.Height))
        End If

        ' Create a high-resolution resized image
        Dim resizedImg As New Bitmap(newWidth, newHeight, Imaging.PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(resizedImg)
            ' Enable high-quality rendering
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

            ' Draw image
            g.DrawImage(img, New System.Drawing.Rectangle(0, 0, newWidth, newHeight))
        End Using

        Return resizedImg
    End Function


    ' Function to update the DataGridView layout dynamically
    Private Sub UpdateGridLayout()
        DataGridView1.Refresh()
        Dim totalHeight As Integer = DataGridView1.Height
        Dim rowHeight As Integer = totalHeight \ 2 ' Set row height to exactly half

        ' Apply row height to all rows
        DataGridView1.RowTemplate.Height = rowHeight
        For Each row As DataGridViewRow In DataGridView1.Rows
            row.Height = rowHeight
        Next

        ' Adjust column widths
        Dim imgColWidth As Integer = DataGridView1.Width \ 3
        Dim remainingWidth As Integer = DataGridView1.Width - imgColWidth
        Dim otherColWidth As Integer = remainingWidth \ 5

        DataGridView1.Columns(0).Width = imgColWidth
        For i As Integer = 1 To 5
            DataGridView1.Columns(i).Width = otherColWidth
            DataGridView1.Columns(i).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Next

        ' Resize images to fit the new row height
        ResizeImagesInGrid(rowHeight)
    End Sub

    ' Function to resize images in the DataGridView
    Private Sub ResizeImagesInGrid(ByVal rowHeight As Integer)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso TypeOf row.Cells(0).Value Is System.Drawing.Image Then
                Dim img As System.Drawing.Image = CType(row.Cells(0).Value, System.Drawing.Image)
                Dim cellWidth As Integer = DataGridView1.Columns(0).Width
                row.Cells(0).Value = ResizeImageToFitWidth(img, cellWidth, rowHeight)
            End If
        Next
    End Sub

    ' Call this function when the form loads or DataGridView is resized
    Private Sub OrdersForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Refresh()
        Me.Refresh()
        Me.Invalidate()
        DisplayRecords() ' Set up the grid
    End Sub

    ' Call this function when DataGridView is resized
    Private Sub DataGridView1_SizeChanged(sender As Object, e As EventArgs) Handles DataGridView1.SizeChanged
        UpdateGridLayout()
    End Sub



    ' Handle row selection to update price labels
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged


        DataGridView1.Refresh()
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            DeleteButton.Visible = True


            ' Ensure the row has enough columns before accessing cells
            If selectedRow.Cells.Count >= 6 Then
                If selectedRow.Tag IsNot Nothing Then
                    Dim prices As Integer() = CType(selectedRow.Tag, Integer())

                    ' Ensure prices array has at least 5 elements
                    If prices.Length >= 5 Then
                        Label12.Text = FormatIndianCurrency(prices(0)) ' CPU Price
                        Label13.Text = FormatIndianCurrency(prices(1)) ' GPU Price
                        Label14.Text = FormatIndianCurrency(prices(2)) ' Storage Price
                        Label15.Text = FormatIndianCurrency(prices(3)) ' RAM Price
                        Label16.Text = FormatIndianCurrency(prices(4)) ' Cabinet Price
                        Label19.Text = FormatIndianCurrency(prices.Sum()) ' Total Price
                        Label12.Location = New Point(919, 220)
                        Label13.Location = New Point(919, 247)
                        Label14.Location = New Point(919, 274)
                        Label15.Location = New Point(919, 303)
                        Label16.Location = New Point(919, 331)
                        Label19.Location = New Point(919, 372)
                    Else
                        ResetLabels()
                    End If
                Else
                    ResetLabels()
                End If
            Else
                ResetLabels()
            End If
        Else
            DeleteButton.Visible = False
        End If

    End Sub

    Private Sub LoadData()
        DataGridView1.Refresh()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso TypeOf row.Cells(0).Value Is System.Drawing.Image Then
                Dim img As System.Drawing.Image = CType(row.Cells(0).Value, System.Drawing.Image)
                Dim cellWidth As Integer = DataGridView1.Columns(0).Width
                Dim cellHeight As Integer = row.Height
                row.Cells(0).Value = ResizeImageToFitWidth(img, cellWidth, cellHeight)
            End If
        Next
    End Sub
    Private Function FormatIndianCurrency(amount As Integer) As String
        Dim culture As New CultureInfo("en-IN")
        Return "₹ " & amount.ToString("N2", culture)
    End Function

    ' Ensure row height and column widths update when form resizes
    Private Sub DataGridView1_Resize(sender As Object, e As EventArgs) Handles DataGridView1.Resize
        UpdateGridLayout()
    End Sub

    ' Reset labels to avoid displaying incorrect data
    Private Sub ResetLabels()
        Label12.Text = "₹0"
        Label13.Text = "₹0"
        Label14.Text = "₹0"
        Label15.Text = "₹0"
        Label16.Text = "₹0"
        Label19.Text = "₹0"
        Label12.Location = New Point(933, 220)
        Label13.Location = New Point(933, 247)
        Label14.Location = New Point(933, 274)
        Label15.Location = New Point(933, 303)
        Label16.Location = New Point(933, 331)
        Label19.Location = New Point(933, 372)
    End Sub

    ' Load data on form load
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Refresh()
        ResizeRowHeight()
        DisplayRecords()
        UpdateGridLayout()
        LoadData()

        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGray
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        Label12.Location = New Point(933, 220)
        Label13.Location = New Point(933, 247)
        Label14.Location = New Point(933, 274)
        Label15.Location = New Point(933, 303)
        Label16.Location = New Point(933, 331)
        Label19.Location = New Point(933, 372)
        DeleteButton.Visible = False
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        ' Check if there is a valid current row (since selection is disabled for column 1)
        If DataGridView1.CurrentRow Is Nothing OrElse DataGridView1.CurrentRow.Index < 0 Then
            MessageBox.Show("Please select a valid row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm deletion
        If MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        ' Get the current row (since selection is limited)
        Dim currentRow As DataGridViewRow = DataGridView1.CurrentRow

        ' Retrieve values safely
        Dim selectedCPU As String = If(currentRow.Cells(1).Value IsNot Nothing, currentRow.Cells(1).Value.ToString(), "")
        Dim selectedGPU As String = If(currentRow.Cells(2).Value IsNot Nothing, currentRow.Cells(2).Value.ToString(), "")
        Dim selectedStorage As String = If(currentRow.Cells(3).Value IsNot Nothing, currentRow.Cells(3).Value.ToString(), "")
        Dim selectedRAM As String = If(currentRow.Cells(4).Value IsNot Nothing, currentRow.Cells(4).Value.ToString(), "")
        Dim selectedCabinet As String = If(currentRow.Cells(5).Value IsNot Nothing, currentRow.Cells(5).Value.ToString(), "")

        ' Ensure valid data
        If String.IsNullOrEmpty(selectedCPU) OrElse String.IsNullOrEmpty(selectedGPU) OrElse
       String.IsNullOrEmpty(selectedStorage) OrElse String.IsNullOrEmpty(selectedRAM) OrElse
       String.IsNullOrEmpty(selectedCabinet) Then
            MessageBox.Show("Selected row contains invalid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' SQL query to delete the record
        Dim deleteQuery As String = "DELETE FROM custorder WHERE cpu = @cpu AND gpu = @gpu AND storage = @storage AND ram = @ram AND cabinet = @cabinet"

        Try
            ' Execute deletion query
            Using conn As New Microsoft.Data.SqlClient.SqlConnection(connString)
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(deleteQuery, conn)
                    cmd.Parameters.AddWithValue("@cpu", selectedCPU)
                    cmd.Parameters.AddWithValue("@gpu", selectedGPU)
                    cmd.Parameters.AddWithValue("@storage", selectedStorage)
                    cmd.Parameters.AddWithValue("@ram", selectedRAM)
                    cmd.Parameters.AddWithValue("@cabinet", selectedCabinet)

                    conn.Open()
                    Dim rowsAffected = cmd.ExecuteNonQuery()

                    ' Remove from DataGridView only if deletion was successful
                    If rowsAffected > 0 AndAlso currentRow.Index >= 0 Then
                        DataGridView1.Rows.RemoveAt(currentRow.Index)
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Record not found in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error deleting record: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True;"

        ' Check if any row is selected
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an item to generate bill", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

        Dim cpuName As String = selectedRow.Cells("Column2").Value.ToString()
        Dim gpuName As String = selectedRow.Cells("Column3").Value.ToString()
        Dim storageName As String = selectedRow.Cells("Column4").Value.ToString()
        Dim ramName As String = selectedRow.Cells("Column5").Value.ToString()
        Dim cabinetName As String = selectedRow.Cells("Column6").Value.ToString()

        Dim cpuPrice As Decimal = GetPrice("cpuinfo", "cpu", cpuName, connectionString)
        Dim gpuPrice As Decimal = GetPrice("gpuinfo", "gpu", gpuName, connectionString)
        Dim ramPrice As Decimal = GetPrice("raminfo", "ramname", ramName, connectionString)
        Dim storagePrice As Decimal = GetPrice("storageinfo", "sname", storageName, connectionString)
        Dim cabinetPrice As Decimal = GetPrice("cabinetinfo", "Brand", cabinetName, connectionString)

        Dim subTotal As Decimal = cpuPrice + gpuPrice + ramPrice + storagePrice + cabinetPrice

        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "PDF Files|*.pdf"
        saveFileDialog.Title = "Save Bill As"
        saveFileDialog.FileName = "Generated_Bill.pdf"

        If saveFileDialog.ShowDialog() <> DialogResult.OK Then Exit Sub

        Dim savePath As String = saveFileDialog.FileName

        Try
            Using fs As New FileStream(savePath, FileMode.Create)
                Using doc As New iTextSharp.text.Document(PageSize.A4)
                    Dim writer As PdfWriter = PdfWriter.GetInstance(doc, fs)
                    doc.Open()

                    ' Load custom Eurostile font
                    Dim baseFontPath As String = "C:\Users\vaisa\Downloads\OnlineWebFonts_COM_579f63f160bc040279b4436ec011a50d\Eurostile Unicase LT W04 Rg\Eurostile Unicase LT W04 Rg.ttf"
                    Dim baseFont As BaseFont = BaseFont.CreateFont(baseFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    Dim titleFont As New iTextSharp.text.Font(baseFont, 18, Font.Bold)

                    Dim labelFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)
                    Dim tableFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12)

                    ' Title (centered, custom font)
                    Dim titlePara As New Paragraph("PC CONFIG.", titleFont)
                    titlePara.Alignment = Element.ALIGN_CENTER
                    doc.Add(titlePara)

                    ' Add spacing
                    doc.Add(New Paragraph(Environment.NewLine))

                    ' Date and Customer
                    Dim customerName As String = GlobalVariables.CurrentCustomerName
                    Dim currentDate As String = DateTime.Now.ToString("dd-MM-yyyy")
                    Dim infoPara As New Paragraph("Date: " & currentDate & vbCrLf & "Customer Name: " & customerName, labelFont)
                    infoPara.Alignment = Element.ALIGN_LEFT
                    doc.Add(infoPara)

                    doc.Add(New Paragraph(Environment.NewLine))

                    ' Table
                    ' Order Summary Title (Helvetica-Bold, size 20)
                    Dim orderSummaryFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20)
                    Dim orderSummaryPara As New Paragraph("Order Summary", orderSummaryFont)
                    orderSummaryPara.Alignment = Element.ALIGN_CENTER
                    doc.Add(orderSummaryPara)

                    doc.Add(New Paragraph(Environment.NewLine))

                    ' Table
                    Dim table As New PdfPTable(2)

                    table.WidthPercentage = 70
                    table.SetWidths(New Single() {3, 2})

                    ' Header
                    table.AddCell(New PdfPCell(New Phrase("ITEM", labelFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                    table.AddCell(New PdfPCell(New Phrase("PRICE (Rs.)", labelFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})

                    Dim items As String() = {cpuName, gpuName, storageName, ramName, cabinetName}
                    Dim prices As String() = {cpuPrice, gpuPrice, storagePrice, ramPrice, cabinetPrice}

                    For i As Integer = 0 To items.Length - 1
                        table.AddCell(New PdfPCell(New Phrase(items(i), tableFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                        table.AddCell(New PdfPCell(New Phrase(FormatIndianCurrency(prices(i)), tableFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                    Next

                    ' Subtotal
                    table.AddCell(New PdfPCell(New Phrase("SUB TOTAL", labelFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
                    table.AddCell(New PdfPCell(New Phrase(FormatIndianCurrency(subTotal), labelFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})

                    doc.Add(table)
                    doc.Close()
                End Using
            End Using

            MessageBox.Show("Bill saved at: " & savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error generating bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Function GetPrice(tableName As String, columnName As String, itemValue As String, connectionString As String) As Decimal
        Dim price As Decimal = 0
        Dim query As String = $"SELECT price FROM {tableName} WHERE {columnName} = @itemValue"

        ' Display the SQL command for debugging
        Debug.WriteLine("Executing SQL: " & query)
        Debug.WriteLine("With Parameter: @itemValue = " & itemValue)

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
    Private Sub ResizeRowHeight()
        Dim dgvHeight As Integer = DataGridView1.Height
        Dim rowCount As Integer = DataGridView1.RowCount

        If rowCount > 0 Then
            Dim rowHeight As Integer = dgvHeight \ (rowCount * 2) ' Half of the DataGridView height
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Height = rowHeight
            Next
        End If
    End Sub

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If e.RowIndex >= 0 AndAlso e.ColumnIndex > 0 Then
            DataGridView1.ClearSelection()
            DataGridView1.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class
