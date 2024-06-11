<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCodeChallenge
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCodeChallenge))
        pbSplash = New PictureBox()
        bg1 = New ComponentModel.BackgroundWorker()
        pnlTop = New Panel()
        Label2 = New Label()
        btnChooseFile = New Button()
        txtCSVFile = New TextBox()
        Label1 = New Label()
        Panel2 = New Panel()
        pbLoading = New PictureBox()
        lblStatus = New Label()
        lstResults = New ListView()
        CType(pbSplash, ComponentModel.ISupportInitialize).BeginInit()
        pnlTop.SuspendLayout()
        Panel2.SuspendLayout()
        CType(pbLoading, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbSplash
        ' 
        pbSplash.BackgroundImage = My.Resources.Resources.CodeChallengeLogo
        pbSplash.BackgroundImageLayout = ImageLayout.Stretch
        pbSplash.Location = New Point(0, 0)
        pbSplash.Margin = New Padding(3, 4, 3, 4)
        pbSplash.Name = "pbSplash"
        pbSplash.Size = New Size(686, 104)
        pbSplash.TabIndex = 0
        pbSplash.TabStop = False
        ' 
        ' bg1
        ' 
        bg1.WorkerReportsProgress = True
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = Color.White
        pnlTop.Controls.Add(Label2)
        pnlTop.Controls.Add(btnChooseFile)
        pnlTop.Controls.Add(txtCSVFile)
        pnlTop.Controls.Add(Label1)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Margin = New Padding(3, 4, 3, 4)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(686, 104)
        pnlTop.TabIndex = 1
        pnlTop.Visible = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(0), CByte(108), CByte(255))
        Label2.Location = New Point(124, 2)
        Label2.Name = "Label2"
        Label2.Size = New Size(189, 37)
        Label2.TabIndex = 3
        Label2.Text = "Import Loans"
        ' 
        ' btnChooseFile
        ' 
        btnChooseFile.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnChooseFile.Location = New Point(647, 48)
        btnChooseFile.Name = "btnChooseFile"
        btnChooseFile.Size = New Size(27, 27)
        btnChooseFile.TabIndex = 2
        btnChooseFile.Text = "..."
        btnChooseFile.UseVisualStyleBackColor = True
        ' 
        ' txtCSVFile
        ' 
        txtCSVFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtCSVFile.Location = New Point(124, 48)
        txtCSVFile.Name = "txtCSVFile"
        txtCSVFile.ReadOnly = True
        txtCSVFile.Size = New Size(505, 27)
        txtCSVFile.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(8, 55)
        Label1.Name = "Label1"
        Label1.Size = New Size(116, 20)
        Label1.TabIndex = 0
        Label1.Text = "Select your file:"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(pbLoading)
        Panel2.Controls.Add(lblStatus)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(0, 520)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(686, 51)
        Panel2.TabIndex = 3
        ' 
        ' pbLoading
        ' 
        pbLoading.BackgroundImageLayout = ImageLayout.Stretch
        pbLoading.Image = My.Resources.Resources.LoadingImage
        pbLoading.Location = New Point(13, 8)
        pbLoading.Name = "pbLoading"
        pbLoading.Size = New Size(40, 40)
        pbLoading.SizeMode = PictureBoxSizeMode.StretchImage
        pbLoading.TabIndex = 4
        pbLoading.TabStop = False
        pbLoading.Visible = False
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(81, 16)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 20)
        lblStatus.TabIndex = 3
        ' 
        ' lstResults
        ' 
        lstResults.BackColor = Color.AliceBlue
        lstResults.Dock = DockStyle.Fill
        lstResults.Font = New Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lstResults.FullRowSelect = True
        lstResults.Location = New Point(0, 104)
        lstResults.Name = "lstResults"
        lstResults.Size = New Size(686, 416)
        lstResults.TabIndex = 4
        lstResults.UseCompatibleStateImageBehavior = False
        lstResults.View = View.Details
        ' 
        ' frmCodeChallenge
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(686, 571)
        Controls.Add(lstResults)
        Controls.Add(Panel2)
        Controls.Add(pnlTop)
        Controls.Add(pbSplash)
        Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 4, 3, 4)
        Name = "frmCodeChallenge"
        Opacity = 0R
        Text = "Code Challenge - Self Help Credit Union"
        CType(pbSplash, ComponentModel.ISupportInitialize).EndInit()
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(pbLoading, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbSplash As PictureBox
    Friend WithEvents bg1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents pnlTop As Panel
    Friend WithEvents txtCSVFile As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnChooseFile As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblStatus As Label
    Friend WithEvents lstResults As ListView
    Friend WithEvents pbLoading As PictureBox

End Class
