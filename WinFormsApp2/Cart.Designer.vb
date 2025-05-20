<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cart))
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        rtnbtn = New Guna.UI2.WinForms.Guna2Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label10 = New Label()
        Label11 = New Label()
        LabelCPUPrice = New Label()
        LabelGPUPrice = New Label()
        LabelStoragePrice = New Label()
        LabelRAMPrice = New Label()
        LabelCabinetPrice = New Label()
        Label17 = New Label()
        Label18 = New Label()
        LabelTotalPrice = New Label()
        Label20 = New Label()
        Label5 = New Label()
        Label9 = New Label()
        PictureBox1 = New PictureBox()
        clrbtn = New Guna.UI2.WinForms.Guna2Button()
        Label12 = New Label()
        PictureBox2 = New PictureBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.BackgroundColor = Color.White
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = Color.White
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.Black
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3, Column4, Column5})
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9.0F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        DataGridView1.DefaultCellStyle = DataGridViewCellStyle5
        DataGridView1.GridColor = Color.Black
        DataGridView1.Location = New Point(130, 101)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = SystemColors.Control
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 9.0F)
        DataGridViewCellStyle6.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.True
        DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        DataGridView1.RowHeadersVisible = False
        DataGridView1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.RowTemplate.Height = 70
        DataGridView1.RowTemplate.ReadOnly = True
        DataGridView1.Size = New Size(625, 92)
        DataGridView1.TabIndex = 0
        ' 
        ' Column1
        ' 
        Column1.DataPropertyName = "cpu"
        Column1.HeaderText = "CPU"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.DataPropertyName = "gpu"
        Column2.HeaderText = "GPU"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.DataPropertyName = "storage"
        Column3.HeaderText = "STORAGE"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        ' 
        ' Column4
        ' 
        Column4.DataPropertyName = "ram"
        Column4.HeaderText = "RAM"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        ' 
        ' Column5
        ' 
        Column5.DataPropertyName = "cabinet"
        Column5.HeaderText = "CABINET"
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        ' 
        ' Guna2Button1
        ' 
        Guna2Button1.AutoRoundedCorners = True
        Guna2Button1.BackColor = Color.Transparent
        Guna2Button1.CustomizableEdges = CustomizableEdges7
        Guna2Button1.DisabledState.BorderColor = Color.DarkGray
        Guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray
        Guna2Button1.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        Guna2Button1.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        Guna2Button1.FillColor = Color.Black
        Guna2Button1.Font = New Font("Segoe UI", 9F)
        Guna2Button1.ForeColor = Color.White
        Guna2Button1.Location = New Point(644, 456)
        Guna2Button1.Name = "Guna2Button1"
        Guna2Button1.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        Guna2Button1.Size = New Size(147, 36)
        Guna2Button1.TabIndex = 1
        Guna2Button1.Text = "CHECKOUT"
        ' 
        ' rtnbtn
        ' 
        rtnbtn.AutoRoundedCorners = True
        rtnbtn.BackColor = Color.Transparent
        rtnbtn.CustomizableEdges = CustomizableEdges9
        rtnbtn.DisabledState.BorderColor = Color.DarkGray
        rtnbtn.DisabledState.CustomBorderColor = Color.DarkGray
        rtnbtn.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        rtnbtn.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        rtnbtn.FillColor = Color.White
        rtnbtn.Font = New Font("Segoe UI", 9F)
        rtnbtn.ForeColor = Color.Black
        rtnbtn.Image = CType(resources.GetObject("rtnbtn.Image"), Image)
        rtnbtn.ImageSize = New Size(25, 25)
        rtnbtn.Location = New Point(707, 25)
        rtnbtn.Name = "rtnbtn"
        rtnbtn.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        rtnbtn.Size = New Size(179, 36)
        rtnbtn.TabIndex = 1
        rtnbtn.Text = "RETURN TO PRODUTS"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 495)
        Label1.Name = "Label1"
        Label1.Size = New Size(41, 15)
        Label1.TabIndex = 2
        Label1.Text = "Label1"
        Label1.Visible = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(158, 199)
        Label2.Name = "Label2"
        Label2.Size = New Size(175, 30)
        Label2.TabIndex = 2
        Label2.Text = "Order Summary "
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(169, 247)
        Label3.Name = "Label3"
        Label3.Size = New Size(39, 17)
        Label3.TabIndex = 2
        Label3.Text = "ITEM"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(263, 247)
        Label4.Name = "Label4"
        Label4.Size = New Size(60, 17)
        Label4.TabIndex = 2
        Label4.Text = "PRICE(₹)"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Location = New Point(169, 279)
        Label6.Name = "Label6"
        Label6.Size = New Size(30, 15)
        Label6.TabIndex = 2
        Label6.Text = "CPU"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.BackColor = Color.Transparent
        Label7.Location = New Point(169, 303)
        Label7.Name = "Label7"
        Label7.Size = New Size(30, 15)
        Label7.TabIndex = 2
        Label7.Text = "GPU"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.BackColor = Color.Transparent
        Label8.Location = New Point(158, 329)
        Label8.Name = "Label8"
        Label8.Size = New Size(57, 15)
        Label8.TabIndex = 2
        Label8.Text = "STORAGE"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.Location = New Point(169, 354)
        Label10.Name = "Label10"
        Label10.Size = New Size(33, 15)
        Label10.TabIndex = 2
        Label10.Text = "RAM"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.BackColor = Color.Transparent
        Label11.Location = New Point(158, 377)
        Label11.Name = "Label11"
        Label11.Size = New Size(55, 15)
        Label11.TabIndex = 2
        Label11.Text = "CABINET"
        ' 
        ' LabelCPUPrice
        ' 
        LabelCPUPrice.AutoSize = True
        LabelCPUPrice.BackColor = Color.Transparent
        LabelCPUPrice.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelCPUPrice.Location = New Point(278, 279)
        LabelCPUPrice.Name = "LabelCPUPrice"
        LabelCPUPrice.Size = New Size(21, 15)
        LabelCPUPrice.TabIndex = 2
        LabelCPUPrice.Text = "₹0"
        ' 
        ' LabelGPUPrice
        ' 
        LabelGPUPrice.AutoSize = True
        LabelGPUPrice.BackColor = Color.Transparent
        LabelGPUPrice.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelGPUPrice.Location = New Point(278, 303)
        LabelGPUPrice.Name = "LabelGPUPrice"
        LabelGPUPrice.Size = New Size(21, 15)
        LabelGPUPrice.TabIndex = 2
        LabelGPUPrice.Text = "₹0"
        ' 
        ' LabelStoragePrice
        ' 
        LabelStoragePrice.AutoSize = True
        LabelStoragePrice.BackColor = Color.Transparent
        LabelStoragePrice.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelStoragePrice.Location = New Point(278, 329)
        LabelStoragePrice.Name = "LabelStoragePrice"
        LabelStoragePrice.Size = New Size(21, 15)
        LabelStoragePrice.TabIndex = 2
        LabelStoragePrice.Text = "₹0"
        ' 
        ' LabelRAMPrice
        ' 
        LabelRAMPrice.AutoSize = True
        LabelRAMPrice.BackColor = Color.Transparent
        LabelRAMPrice.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelRAMPrice.Location = New Point(278, 354)
        LabelRAMPrice.Name = "LabelRAMPrice"
        LabelRAMPrice.Size = New Size(21, 15)
        LabelRAMPrice.TabIndex = 2
        LabelRAMPrice.Text = "₹0"
        ' 
        ' LabelCabinetPrice
        ' 
        LabelCabinetPrice.AutoSize = True
        LabelCabinetPrice.BackColor = Color.Transparent
        LabelCabinetPrice.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelCabinetPrice.Location = New Point(278, 377)
        LabelCabinetPrice.Name = "LabelCabinetPrice"
        LabelCabinetPrice.Size = New Size(21, 15)
        LabelCabinetPrice.TabIndex = 2
        LabelCabinetPrice.Text = "₹0"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.BackColor = Color.Transparent
        Label17.Location = New Point(134, 392)
        Label17.Name = "Label17"
        Label17.Size = New Size(227, 15)
        Label17.TabIndex = 2
        Label17.Text = "--------------------------------------------"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.BackColor = Color.Transparent
        Label18.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label18.Location = New Point(146, 407)
        Label18.Name = "Label18"
        Label18.Size = New Size(90, 21)
        Label18.TabIndex = 2
        Label18.Text = "SUB TOTAL"
        ' 
        ' LabelTotalPrice
        ' 
        LabelTotalPrice.AutoSize = True
        LabelTotalPrice.BackColor = Color.Transparent
        LabelTotalPrice.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelTotalPrice.Location = New Point(275, 407)
        LabelTotalPrice.Name = "LabelTotalPrice"
        LabelTotalPrice.Size = New Size(28, 21)
        LabelTotalPrice.TabIndex = 2
        LabelTotalPrice.Text = "₹0"
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.BackColor = Color.Transparent
        Label20.Location = New Point(134, 428)
        Label20.Name = "Label20"
        Label20.Size = New Size(227, 15)
        Label20.TabIndex = 2
        Label20.Text = "--------------------------------------------"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Location = New Point(134, 264)
        Label5.Name = "Label5"
        Label5.Size = New Size(227, 15)
        Label5.TabIndex = 2
        Label5.Text = "--------------------------------------------"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.BackColor = Color.Transparent
        Label9.Location = New Point(134, 229)
        Label9.Name = "Label9"
        Label9.Size = New Size(227, 15)
        Label9.TabIndex = 2
        Label9.Text = "--------------------------------------------"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Location = New Point(518, 199)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(237, 237)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 3
        PictureBox1.TabStop = False
        ' 
        ' clrbtn
        ' 
        clrbtn.AutoRoundedCorners = True
        clrbtn.BackColor = Color.Transparent
        clrbtn.CustomizableEdges = CustomizableEdges11
        clrbtn.DisabledState.BorderColor = Color.DarkGray
        clrbtn.DisabledState.CustomBorderColor = Color.DarkGray
        clrbtn.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        clrbtn.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        clrbtn.FillColor = Color.Black
        clrbtn.Font = New Font("Segoe UI", 9F)
        clrbtn.ForeColor = Color.White
        clrbtn.Location = New Point(481, 456)
        clrbtn.Name = "clrbtn"
        clrbtn.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        clrbtn.Size = New Size(147, 36)
        clrbtn.TabIndex = 1
        clrbtn.Text = "CLEAR CART"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.BackColor = Color.Transparent
        Label12.Font = New Font("EurostileUnicaseLTW04-Rg", 26.2499962F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label12.ForeColor = Color.White
        Label12.Image = CType(resources.GetObject("Label12.Image"), Image)
        Label12.ImageAlign = ContentAlignment.MiddleRight
        Label12.Location = New Point(81, 25)
        Label12.Name = "Label12"
        Label12.Size = New Size(169, 43)
        Label12.TabIndex = 2
        Label12.Text = "CART."
        ' 
        ' PictureBox2
        ' 
        PictureBox2.BackColor = Color.Transparent
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.InitialImage = CType(resources.GetObject("PictureBox2.InitialImage"), Image)
        PictureBox2.Location = New Point(32, 18)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(55, 56)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 4
        PictureBox2.TabStop = False
        ' 
        ' Cart
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(919, 519)
        Controls.Add(PictureBox2)
        Controls.Add(PictureBox1)
        Controls.Add(Label12)
        Controls.Add(Label2)
        Controls.Add(LabelTotalPrice)
        Controls.Add(LabelCabinetPrice)
        Controls.Add(LabelRAMPrice)
        Controls.Add(LabelStoragePrice)
        Controls.Add(LabelGPUPrice)
        Controls.Add(LabelCPUPrice)
        Controls.Add(Label4)
        Controls.Add(Label11)
        Controls.Add(Label10)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label20)
        Controls.Add(Label9)
        Controls.Add(Label5)
        Controls.Add(Label17)
        Controls.Add(Label18)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(rtnbtn)
        Controls.Add(clrbtn)
        Controls.Add(Guna2Button1)
        Controls.Add(DataGridView1)
        Name = "Cart"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Cart"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents rtnbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents LabelCPUPrice As Label
    Friend WithEvents LabelGPUPrice As Label
    Friend WithEvents LabelStoragePrice As Label
    Friend WithEvents LabelRAMPrice As Label
    Friend WithEvents LabelCabinetPrice As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents LabelTotalPrice As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents clrbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label12 As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
