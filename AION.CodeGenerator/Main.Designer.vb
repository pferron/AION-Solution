<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbc_IstCodeGenerator = New System.Windows.Forms.TabControl()
        Me.tab_SqlServerConnection = New System.Windows.Forms.TabPage()
        Me.chk_WindowsAuthentication = New System.Windows.Forms.CheckBox()
        Me.btn_Connect = New System.Windows.Forms.Button()
        Me.lbl_SqlServer = New System.Windows.Forms.Label()
        Me.txt_Password = New System.Windows.Forms.TextBox()
        Me.lbl_Username = New System.Windows.Forms.Label()
        Me.txt_Username = New System.Windows.Forms.TextBox()
        Me.lbl_Password = New System.Windows.Forms.Label()
        Me.txt_SqlServer = New System.Windows.Forms.TextBox()
        Me.tab_CodeGenerator = New System.Windows.Forms.TabPage()
        Me.chk_C = New System.Windows.Forms.CheckBox()
        Me.chk_Vb = New System.Windows.Forms.CheckBox()
        Me.txt_Output = New System.Windows.Forms.TextBox()
        Me.btn_BrowseDirectory = New System.Windows.Forms.Button()
        Me.btn_Generate = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgv_Columns = New System.Windows.Forms.DataGridView()
        Me.txt_SectionName = New System.Windows.Forms.TextBox()
        Me.txt_ObjectName = New System.Windows.Forms.TextBox()
        Me.lbl_SectionName = New System.Windows.Forms.Label()
        Me.lbl_ObjectName = New System.Windows.Forms.Label()
        Me.cmb_Table = New System.Windows.Forms.ComboBox()
        Me.txt_ProjectName = New System.Windows.Forms.TextBox()
        Me.lbl_Table = New System.Windows.Forms.Label()
        Me.lbl_Database = New System.Windows.Forms.Label()
        Me.lbl_ProjectName = New System.Windows.Forms.Label()
        Me.fbd_BrowseDirectory = New System.Windows.Forms.FolderBrowserDialog()
        Me.lbl_DefaultDatabase = New System.Windows.Forms.Label()
        Me.txt_DefaultDatabase = New System.Windows.Forms.TextBox()
        Me.txt_Database = New System.Windows.Forms.TextBox()
        Me.tbc_IstCodeGenerator.SuspendLayout()
        Me.tab_SqlServerConnection.SuspendLayout()
        Me.tab_CodeGenerator.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgv_Columns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbc_IstCodeGenerator
        '
        Me.tbc_IstCodeGenerator.Controls.Add(Me.tab_SqlServerConnection)
        Me.tbc_IstCodeGenerator.Controls.Add(Me.tab_CodeGenerator)
        Me.tbc_IstCodeGenerator.Location = New System.Drawing.Point(11, 10)
        Me.tbc_IstCodeGenerator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbc_IstCodeGenerator.Name = "tbc_IstCodeGenerator"
        Me.tbc_IstCodeGenerator.SelectedIndex = 0
        Me.tbc_IstCodeGenerator.Size = New System.Drawing.Size(896, 492)
        Me.tbc_IstCodeGenerator.TabIndex = 18
        '
        'tab_SqlServerConnection
        '
        Me.tab_SqlServerConnection.Controls.Add(Me.txt_DefaultDatabase)
        Me.tab_SqlServerConnection.Controls.Add(Me.lbl_DefaultDatabase)
        Me.tab_SqlServerConnection.Controls.Add(Me.chk_WindowsAuthentication)
        Me.tab_SqlServerConnection.Controls.Add(Me.btn_Connect)
        Me.tab_SqlServerConnection.Controls.Add(Me.lbl_SqlServer)
        Me.tab_SqlServerConnection.Controls.Add(Me.txt_Password)
        Me.tab_SqlServerConnection.Controls.Add(Me.lbl_Username)
        Me.tab_SqlServerConnection.Controls.Add(Me.txt_Username)
        Me.tab_SqlServerConnection.Controls.Add(Me.lbl_Password)
        Me.tab_SqlServerConnection.Controls.Add(Me.txt_SqlServer)
        Me.tab_SqlServerConnection.Location = New System.Drawing.Point(4, 25)
        Me.tab_SqlServerConnection.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tab_SqlServerConnection.Name = "tab_SqlServerConnection"
        Me.tab_SqlServerConnection.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tab_SqlServerConnection.Size = New System.Drawing.Size(888, 463)
        Me.tab_SqlServerConnection.TabIndex = 0
        Me.tab_SqlServerConnection.Text = "SQL Server Connection"
        Me.tab_SqlServerConnection.UseVisualStyleBackColor = True
        '
        'chk_WindowsAuthentication
        '
        Me.chk_WindowsAuthentication.AutoSize = True
        Me.chk_WindowsAuthentication.Location = New System.Drawing.Point(15, 42)
        Me.chk_WindowsAuthentication.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_WindowsAuthentication.Name = "chk_WindowsAuthentication"
        Me.chk_WindowsAuthentication.Size = New System.Drawing.Size(180, 21)
        Me.chk_WindowsAuthentication.TabIndex = 25
        Me.chk_WindowsAuthentication.Text = "Windows Authentication"
        Me.chk_WindowsAuthentication.UseVisualStyleBackColor = True
        '
        'btn_Connect
        '
        Me.btn_Connect.Location = New System.Drawing.Point(779, 423)
        Me.btn_Connect.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Connect.Name = "btn_Connect"
        Me.btn_Connect.Size = New System.Drawing.Size(100, 27)
        Me.btn_Connect.TabIndex = 24
        Me.btn_Connect.Text = "Connect"
        Me.btn_Connect.UseVisualStyleBackColor = True
        '
        'lbl_SqlServer
        '
        Me.lbl_SqlServer.Location = New System.Drawing.Point(11, 10)
        Me.lbl_SqlServer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_SqlServer.Name = "lbl_SqlServer"
        Me.lbl_SqlServer.Size = New System.Drawing.Size(87, 18)
        Me.lbl_SqlServer.TabIndex = 18
        Me.lbl_SqlServer.Text = "SQL Server:"
        Me.lbl_SqlServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Password
        '
        Me.txt_Password.Location = New System.Drawing.Point(107, 96)
        Me.txt_Password.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Password.Name = "txt_Password"
        Me.txt_Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_Password.Size = New System.Drawing.Size(132, 22)
        Me.txt_Password.TabIndex = 23
        '
        'lbl_Username
        '
        Me.lbl_Username.Location = New System.Drawing.Point(11, 70)
        Me.lbl_Username.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Username.Name = "lbl_Username"
        Me.lbl_Username.Size = New System.Drawing.Size(87, 18)
        Me.lbl_Username.TabIndex = 19
        Me.lbl_Username.Text = "Username:"
        Me.lbl_Username.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Username
        '
        Me.txt_Username.Location = New System.Drawing.Point(107, 64)
        Me.txt_Username.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Username.Name = "txt_Username"
        Me.txt_Username.Size = New System.Drawing.Size(132, 22)
        Me.txt_Username.TabIndex = 22
        '
        'lbl_Password
        '
        Me.lbl_Password.Location = New System.Drawing.Point(11, 102)
        Me.lbl_Password.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Password.Name = "lbl_Password"
        Me.lbl_Password.Size = New System.Drawing.Size(87, 18)
        Me.lbl_Password.TabIndex = 20
        Me.lbl_Password.Text = "Password:"
        Me.lbl_Password.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SqlServer
        '
        Me.txt_SqlServer.Location = New System.Drawing.Point(107, 10)
        Me.txt_SqlServer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_SqlServer.Name = "txt_SqlServer"
        Me.txt_SqlServer.Size = New System.Drawing.Size(332, 22)
        Me.txt_SqlServer.TabIndex = 21
        '
        'tab_CodeGenerator
        '
        Me.tab_CodeGenerator.Controls.Add(Me.txt_Database)
        Me.tab_CodeGenerator.Controls.Add(Me.chk_C)
        Me.tab_CodeGenerator.Controls.Add(Me.chk_Vb)
        Me.tab_CodeGenerator.Controls.Add(Me.txt_Output)
        Me.tab_CodeGenerator.Controls.Add(Me.btn_BrowseDirectory)
        Me.tab_CodeGenerator.Controls.Add(Me.btn_Generate)
        Me.tab_CodeGenerator.Controls.Add(Me.Panel1)
        Me.tab_CodeGenerator.Controls.Add(Me.txt_SectionName)
        Me.tab_CodeGenerator.Controls.Add(Me.txt_ObjectName)
        Me.tab_CodeGenerator.Controls.Add(Me.lbl_SectionName)
        Me.tab_CodeGenerator.Controls.Add(Me.lbl_ObjectName)
        Me.tab_CodeGenerator.Controls.Add(Me.cmb_Table)
        Me.tab_CodeGenerator.Controls.Add(Me.txt_ProjectName)
        Me.tab_CodeGenerator.Controls.Add(Me.lbl_Table)
        Me.tab_CodeGenerator.Controls.Add(Me.lbl_Database)
        Me.tab_CodeGenerator.Controls.Add(Me.lbl_ProjectName)
        Me.tab_CodeGenerator.Location = New System.Drawing.Point(4, 25)
        Me.tab_CodeGenerator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tab_CodeGenerator.Name = "tab_CodeGenerator"
        Me.tab_CodeGenerator.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tab_CodeGenerator.Size = New System.Drawing.Size(888, 463)
        Me.tab_CodeGenerator.TabIndex = 1
        Me.tab_CodeGenerator.Text = "Code Generator"
        Me.tab_CodeGenerator.UseVisualStyleBackColor = True
        '
        'chk_C
        '
        Me.chk_C.AutoSize = True
        Me.chk_C.Location = New System.Drawing.Point(723, 74)
        Me.chk_C.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_C.Name = "chk_C"
        Me.chk_C.Size = New System.Drawing.Size(47, 21)
        Me.chk_C.TabIndex = 34
        Me.chk_C.Text = "C#"
        Me.chk_C.UseVisualStyleBackColor = True
        '
        'chk_Vb
        '
        Me.chk_Vb.AutoSize = True
        Me.chk_Vb.Location = New System.Drawing.Point(635, 74)
        Me.chk_Vb.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_Vb.Name = "chk_Vb"
        Me.chk_Vb.Size = New System.Drawing.Size(74, 21)
        Me.chk_Vb.TabIndex = 33
        Me.chk_Vb.Text = "VB.Net"
        Me.chk_Vb.UseVisualStyleBackColor = True
        '
        'txt_Output
        '
        Me.txt_Output.Location = New System.Drawing.Point(11, 423)
        Me.txt_Output.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Output.Name = "txt_Output"
        Me.txt_Output.Size = New System.Drawing.Size(621, 22)
        Me.txt_Output.TabIndex = 32
        '
        'btn_BrowseDirectory
        '
        Me.btn_BrowseDirectory.Location = New System.Drawing.Point(641, 421)
        Me.btn_BrowseDirectory.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_BrowseDirectory.Name = "btn_BrowseDirectory"
        Me.btn_BrowseDirectory.Size = New System.Drawing.Size(47, 27)
        Me.btn_BrowseDirectory.TabIndex = 31
        Me.btn_BrowseDirectory.Text = "..."
        Me.btn_BrowseDirectory.UseVisualStyleBackColor = True
        '
        'btn_Generate
        '
        Me.btn_Generate.Location = New System.Drawing.Point(696, 421)
        Me.btn_Generate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Generate.Name = "btn_Generate"
        Me.btn_Generate.Size = New System.Drawing.Size(179, 27)
        Me.btn_Generate.TabIndex = 30
        Me.btn_Generate.Text = "Generate Selected Rows"
        Me.btn_Generate.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.dgv_Columns)
        Me.Panel1.Location = New System.Drawing.Point(11, 108)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(864, 305)
        Me.Panel1.TabIndex = 29
        '
        'dgv_Columns
        '
        Me.dgv_Columns.AllowUserToAddRows = False
        Me.dgv_Columns.AllowUserToDeleteRows = False
        Me.dgv_Columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Columns.Location = New System.Drawing.Point(0, 0)
        Me.dgv_Columns.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgv_Columns.Name = "dgv_Columns"
        Me.dgv_Columns.Size = New System.Drawing.Size(864, 305)
        Me.dgv_Columns.TabIndex = 0
        '
        'txt_SectionName
        '
        Me.txt_SectionName.Location = New System.Drawing.Point(576, 39)
        Me.txt_SectionName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_SectionName.Name = "txt_SectionName"
        Me.txt_SectionName.Size = New System.Drawing.Size(199, 22)
        Me.txt_SectionName.TabIndex = 28
        '
        'txt_ObjectName
        '
        Me.txt_ObjectName.Location = New System.Drawing.Point(576, 10)
        Me.txt_ObjectName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_ObjectName.Name = "txt_ObjectName"
        Me.txt_ObjectName.Size = New System.Drawing.Size(199, 22)
        Me.txt_ObjectName.TabIndex = 27
        '
        'lbl_SectionName
        '
        Me.lbl_SectionName.Location = New System.Drawing.Point(459, 39)
        Me.lbl_SectionName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_SectionName.Name = "lbl_SectionName"
        Me.lbl_SectionName.Size = New System.Drawing.Size(113, 18)
        Me.lbl_SectionName.TabIndex = 26
        Me.lbl_SectionName.Text = "Section Name:"
        Me.lbl_SectionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ObjectName
        '
        Me.lbl_ObjectName.Location = New System.Drawing.Point(459, 10)
        Me.lbl_ObjectName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ObjectName.Name = "lbl_ObjectName"
        Me.lbl_ObjectName.Size = New System.Drawing.Size(113, 18)
        Me.lbl_ObjectName.TabIndex = 25
        Me.lbl_ObjectName.Text = "Object Name:"
        Me.lbl_ObjectName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_Table
        '
        Me.cmb_Table.FormattingEnabled = True
        Me.cmb_Table.Location = New System.Drawing.Point(128, 69)
        Me.cmb_Table.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_Table.Name = "cmb_Table"
        Me.cmb_Table.Size = New System.Drawing.Size(332, 24)
        Me.cmb_Table.TabIndex = 24
        '
        'txt_ProjectName
        '
        Me.txt_ProjectName.Location = New System.Drawing.Point(128, 10)
        Me.txt_ProjectName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_ProjectName.Name = "txt_ProjectName"
        Me.txt_ProjectName.Size = New System.Drawing.Size(159, 22)
        Me.txt_ProjectName.TabIndex = 22
        '
        'lbl_Table
        '
        Me.lbl_Table.Location = New System.Drawing.Point(11, 69)
        Me.lbl_Table.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Table.Name = "lbl_Table"
        Me.lbl_Table.Size = New System.Drawing.Size(113, 18)
        Me.lbl_Table.TabIndex = 21
        Me.lbl_Table.Text = "Table:"
        Me.lbl_Table.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Database
        '
        Me.lbl_Database.Location = New System.Drawing.Point(11, 39)
        Me.lbl_Database.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Database.Name = "lbl_Database"
        Me.lbl_Database.Size = New System.Drawing.Size(113, 18)
        Me.lbl_Database.TabIndex = 20
        Me.lbl_Database.Text = "Database:"
        Me.lbl_Database.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ProjectName
        '
        Me.lbl_ProjectName.Location = New System.Drawing.Point(11, 10)
        Me.lbl_ProjectName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProjectName.Name = "lbl_ProjectName"
        Me.lbl_ProjectName.Size = New System.Drawing.Size(113, 18)
        Me.lbl_ProjectName.TabIndex = 19
        Me.lbl_ProjectName.Text = "Project Name:"
        Me.lbl_ProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_DefaultDatabase
        '
        Me.lbl_DefaultDatabase.AutoSize = True
        Me.lbl_DefaultDatabase.Location = New System.Drawing.Point(476, 14)
        Me.lbl_DefaultDatabase.Name = "lbl_DefaultDatabase"
        Me.lbl_DefaultDatabase.Size = New System.Drawing.Size(73, 17)
        Me.lbl_DefaultDatabase.TabIndex = 26
        Me.lbl_DefaultDatabase.Text = "Database:"
        '
        'txt_DefaultDatabase
        '
        Me.txt_DefaultDatabase.Location = New System.Drawing.Point(555, 11)
        Me.txt_DefaultDatabase.Name = "txt_DefaultDatabase"
        Me.txt_DefaultDatabase.Size = New System.Drawing.Size(150, 22)
        Me.txt_DefaultDatabase.TabIndex = 27
        '
        'txt_Database
        '
        Me.txt_Database.Location = New System.Drawing.Point(128, 40)
        Me.txt_Database.Name = "txt_Database"
        Me.txt_Database.Size = New System.Drawing.Size(159, 22)
        Me.txt_Database.TabIndex = 35
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(912, 507)
        Me.Controls.Add(Me.tbc_IstCodeGenerator)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DaySheets Code Generator"
        Me.tbc_IstCodeGenerator.ResumeLayout(False)
        Me.tab_SqlServerConnection.ResumeLayout(False)
        Me.tab_SqlServerConnection.PerformLayout()
        Me.tab_CodeGenerator.ResumeLayout(False)
        Me.tab_CodeGenerator.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgv_Columns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbc_IstCodeGenerator As System.Windows.Forms.TabControl
    Friend WithEvents tab_SqlServerConnection As System.Windows.Forms.TabPage
    Friend WithEvents tab_CodeGenerator As System.Windows.Forms.TabPage
    Friend WithEvents btn_Connect As System.Windows.Forms.Button
    Friend WithEvents txt_Password As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Username As System.Windows.Forms.Label
    Friend WithEvents txt_Username As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Password As System.Windows.Forms.Label
    Friend WithEvents txt_SqlServer As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SqlServer As System.Windows.Forms.Label
    Friend WithEvents lbl_ProjectName As System.Windows.Forms.Label
    Friend WithEvents lbl_Database As System.Windows.Forms.Label
    Friend WithEvents lbl_ObjectName As System.Windows.Forms.Label
    Friend WithEvents cmb_Table As System.Windows.Forms.ComboBox
    Friend WithEvents txt_ProjectName As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Table As System.Windows.Forms.Label
    Friend WithEvents txt_Output As System.Windows.Forms.TextBox
    Friend WithEvents btn_BrowseDirectory As System.Windows.Forms.Button
    Friend WithEvents btn_Generate As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txt_SectionName As System.Windows.Forms.TextBox
    Friend WithEvents txt_ObjectName As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SectionName As System.Windows.Forms.Label
    Friend WithEvents dgv_Columns As System.Windows.Forms.DataGridView
    Friend WithEvents fbd_BrowseDirectory As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chk_WindowsAuthentication As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Vb As System.Windows.Forms.CheckBox
    Friend WithEvents chk_C As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_DefaultDatabase As Label
    Friend WithEvents txt_DefaultDatabase As TextBox
    Friend WithEvents txt_Database As TextBox
End Class
