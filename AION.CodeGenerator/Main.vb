#Region " Imports "

Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports System.Text.RegularExpressions
Imports Meck.Data

#End Region

Public Class Main

#Region " Private Variables "

    Private LoggedIn As Boolean = False
    Private FilePath As String = String.Empty
    Private StoredProcedureObjectName As StringBuilder
    Private StoredProcedureUserName As String

#End Region

#Region " Form Events "

    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txt_SqlServer.Text = "sus-paas-non.database.windows.net"
        Me.chk_WindowsAuthentication.Checked = False
        Me.txt_Username.Text = "AION_user"
        Me.txt_Password.Text = "JjMHOWhJj9jUvnuBYovU"
        Me.txt_DefaultDatabase.Text = "dev_sustainable"
    End Sub

#End Region

#Region " Tab Events "

    Private Sub tbc_IstCodeGenerator_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbc_IstCodeGenerator.SelectedIndexChanged

        If Me.tbc_IstCodeGenerator.SelectedTab.Name = Me.tab_CodeGenerator.Name And Me.LoggedIn = False Then
            Me.tbc_IstCodeGenerator.SelectTab(0)
            MsgBox("Please log on to a database server prior to attempting to generate code!", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

#End Region

#Region " Button Events "

    Private Sub btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Connect.Click

        If (Me.txt_Username.Text = String.Empty OrElse Me.txt_Password.Text = String.Empty) And Me.chk_WindowsAuthentication.Checked = False Then

            MsgBox("Please enter Username and Password", MsgBoxStyle.Exclamation)

        Else

            Me.ClearCodeGenerationForm()
            Me.LoadDatabases()

        End If

    End Sub

    Private Sub btn_BrowseDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_BrowseDirectory.Click

        If Me.txt_Output.Text <> "" Then
            Me.fbd_BrowseDirectory.SelectedPath = Me.txt_Output.Text
        End If

        If Me.fbd_BrowseDirectory.ShowDialog() = DialogResult.OK Then
            Me.txt_Output.Text = Me.fbd_BrowseDirectory.SelectedPath
        End If

    End Sub

    Private Sub btn_Generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Generate.Click
        If (MsgBox("WARNING! Only Selected rows will be generated. Continue?", MsgBoxStyle.OkCancel, "Continue?") = MsgBoxResult.Cancel) Then
            Return
        End If
        If txt_Output.Text <> "" Then
            If chk_C.Checked <> False Or chk_Vb.Checked <> False Then
                Me.FilePath = Me.txt_Output.Text
                If Me.FilePath.Substring(Len(Me.FilePath) - 1) <> "\" Then
                    Me.FilePath = Me.FilePath + "\"
                End If

                Me.GenerateSQLObjects()

                Me.GenerateBusinessObjects()
            Else
                MsgBox("Select Output Language C# or VB.Net!", MsgBoxStyle.Critical, "Error")
            End If
        Else
            MsgBox("Select Output Directory!", MsgBoxStyle.Critical, "Error")
        End If

    End Sub

#End Region

#Region " ComboBox Events "

    'Private Sub cmb_Database_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)

    '    If cmb_Database.SelectedValue.ToString <> String.Empty Then
    '        Me.LoadTables()
    '    End If

    'End Sub

    Private Sub cmb_Table_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Table.SelectionChangeCommitted

        If cmb_Table.SelectedValue.ToString <> String.Empty Then
            Me.LoadColumns()
        End If

    End Sub

#End Region

#Region " CheckBox Events "

    Private Sub chk_WindowsAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_WindowsAuthentication.CheckedChanged

        If Me.chk_WindowsAuthentication.Checked Then
            Me.txt_Username.Enabled = False
            Me.txt_Password.Enabled = False
        Else
            Me.txt_Username.Enabled = True
            Me.txt_Password.Enabled = True
        End If
    End Sub

#End Region

#Region " Other Methods "

#Region " Database Information "

    Private Sub LoadDatabases()

        Dim SQLServerConnection As String = String.Empty
        'Dim Databases As DataSet

        Try
            If Me.chk_WindowsAuthentication.Checked Then
                SQLServerConnection = "Server=" & Me.txt_SqlServer.Text.Trim & ";Trusted_Connection=Yes;"
            Else
                SQLServerConnection = "Server=" & Me.txt_SqlServer.Text.Trim & ";Persist Security Info=false;Integrated Security=false;User ID=" & Me.txt_Username.Text.Trim & ";Password=" & Me.txt_Password.Text.Trim & ";Database=" & Me.txt_DefaultDatabase.Text.Trim
            End If

            'Databases = DataService.RunSQLReturnDS("SELECT * FROM sys.databases", SQLServerConnection)

            'Me.cmb_Database.ValueMember = "NAME"
            'Me.cmb_Database.DisplayMember = "NAME"
            'Databases.Tables(0).DefaultView.Sort = "NAME ASC"
            'Me.cmb_Database.DataSource = Databases.Tables(0).DefaultView
            'Me.cmb_Database.SelectedValue = -1

            Me.txt_Database.Text = Me.txt_DefaultDatabase.Text
            Me.txt_Database.Enabled = False

            Me.LoadTables()

            Me.LoggedIn = True
            Me.tbc_IstCodeGenerator.SelectTab(1)

        Catch ex As Exception

            MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error")

        End Try

    End Sub

    Private Sub LoadTables()

        Dim SQLServerConnection As String = String.Empty
        Dim Tables As DataSet

        Me.ClearDatabaseDetails()

        Try
            If Me.chk_WindowsAuthentication.Checked Then
                SQLServerConnection = "Server=" & Me.txt_SqlServer.Text.Trim & ";Database=" & Me.txt_DefaultDatabase.Text & ";Trusted_Connection=Yes;"
            Else
                SQLServerConnection = "Server=" & Me.txt_SqlServer.Text.Trim & ";Database=" & Me.txt_DefaultDatabase.Text & ";Persist Security Info=false;Integrated Security=false;User ID=" & Me.txt_Username.Text.Trim & ";Password=" & Me.txt_Password.Text.Trim
            End If

            Tables = SqlWrapper.RunSQLReturnDS("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_SCHEMA", SQLServerConnection)

            Me.cmb_Table.ValueMember = "TABLE_NAME"
            Me.cmb_Table.DisplayMember = "TABLE_NAME"
            Tables.Tables(0).DefaultView.Sort = "TABLE_NAME ASC"
            Me.cmb_Table.DataSource = Tables.Tables(0).DefaultView
            Me.cmb_Table.SelectedValue = -1

        Catch ex As Exception

            MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error")

        End Try

    End Sub

    Private Sub LoadColumns()

        Dim SQLServerConnection As String = String.Empty
        Dim Columns As DataSet
        Dim PrimaryKeys As DataSet
        Dim params(0) As SqlParameter

        Me.ClearColumns()

        Try
            If Me.chk_WindowsAuthentication.Checked Then
                SQLServerConnection = "Server=" & Me.txt_SqlServer.Text.Trim & ";Database=" & Me.txt_DefaultDatabase.Text & ";Trusted_Connection=Yes;"
            Else
                SQLServerConnection = "Server=" & Me.txt_SqlServer.Text.Trim & ";Database=" & Me.txt_DefaultDatabase.Text & ";Persist Security Info=false;Integrated Security=false;User ID=" & Me.txt_Username.Text.Trim & ";Password=" & Me.txt_Password.Text.Trim
            End If

            params(0) = New SqlParameter("@table_name", Me.cmb_Table.SelectedValue.ToString())

            Columns = SqlWrapper.RunSPReturnDS("sp_columns_90", SQLServerConnection, params)

            Columns.Tables(0).Columns.Add(New DataColumn("Primary_Key"))
            Columns.Tables(0).Columns("Primary_Key").DefaultValue = ""

            Columns.Tables(0).Columns.Add(New DataColumn("Property_Name"))
            Columns.Tables(0).Columns("Property_Name").DefaultValue = ""

            PrimaryKeys = SqlWrapper.RunSPReturnDS("sp_pkeys", SQLServerConnection, params)

            For i = 0 To Columns.Tables(0).Rows.Count - 1
                For j = 0 To PrimaryKeys.Tables(0).Rows.Count - 1
                    If Columns.Tables(0).Rows(i).Item("COLUMN_NAME") = PrimaryKeys.Tables(0).Rows(j).Item("COLUMN_NAME") Then
                        Columns.Tables(0).Rows(i).Item("Primary_Key") = "Yes"
                    End If
                Next
            Next

            Me.FormatDataGridView()

            Me.dgv_Columns.DataSource = Columns.Tables(0).DefaultView

            Me.LoadPropertyNames()

        Catch ex As Exception

            MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error")

        End Try

    End Sub

    Private Sub FormatDataGridView()

        Me.dgv_Columns.Columns.Clear()
        Me.dgv_Columns.AutoGenerateColumns = False

        Dim PositionColumn As New DataGridViewTextBoxColumn

        With PositionColumn
            .DataPropertyName = "ORDINAL_POSITION"
            .Name = "No."
            .HeaderText = "No."
            .Width = 35
            .ReadOnly = True
        End With

        Me.dgv_Columns.Columns.Add(PositionColumn)

        Dim PrimaryKeyColumn As New DataGridViewTextBoxColumn

        With PrimaryKeyColumn
            .DataPropertyName = "Primary_Key"
            .Name = "PK"
            .HeaderText = "PK"
            .Width = 35
            .ReadOnly = True
        End With

        Me.dgv_Columns.Columns.Add(PrimaryKeyColumn)

        Dim PropertyNameColumn As New DataGridViewTextBoxColumn

        With PropertyNameColumn
            .DataPropertyName = "Property_Name"
            .Name = "Property Name"
            .HeaderText = "Property Name"
            .Width = 160
            .ReadOnly = False
        End With

        Me.dgv_Columns.Columns.Add(PropertyNameColumn)

        Dim FieldNameColumn As New DataGridViewTextBoxColumn

        With FieldNameColumn
            .DataPropertyName = "COLUMN_NAME"
            .Name = "Field Name"
            .HeaderText = "Field Name"
            .Width = 190
            .ReadOnly = True
        End With

        Me.dgv_Columns.Columns.Add(FieldNameColumn)

        Dim DataTypeColumn As New DataGridViewTextBoxColumn

        With DataTypeColumn
            .DataPropertyName = "TYPE_NAME"
            .Name = "Data Type"
            .HeaderText = "Data Type"
            .Width = 85
            .ReadOnly = True
        End With

        Me.dgv_Columns.Columns.Add(DataTypeColumn)

        Dim LengthColumn As New DataGridViewTextBoxColumn

        With LengthColumn
            .DataPropertyName = "PRECISION"
            .Name = "Length"
            .HeaderText = "Length"
            .Width = 70
            .ReadOnly = True
        End With

        Me.dgv_Columns.Columns.Add(LengthColumn)

        Dim NullableColumn As New DataGridViewTextBoxColumn

        With NullableColumn
            .DataPropertyName = "IS_NULLABLE"
            .Name = "Allow Nulls"
            .HeaderText = "Allow Nulls"
            .Width = 90
            .ReadOnly = True
        End With

        Me.dgv_Columns.Columns.Add(NullableColumn)

    End Sub

    Private Sub LoadPropertyNames()

        For i As Integer = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns.Item("Field Name", i).Value = "WKR_ID_CREATED_TXT" Then
                Me.dgv_Columns.Item("Property Name", i).Value = "CreatedByWkrId"
            End If
            If Me.dgv_Columns.Item("Field Name", i).Value = "CREATED_DTTM" Then
                Me.dgv_Columns.Item("Property Name", i).Value = "CreatedDate"
            End If
            If Me.dgv_Columns.Item("Field Name", i).Value = "WKR_ID_UPDATED_TXT" Then
                Me.dgv_Columns.Item("Property Name", i).Value = "UpdatedByWkrId"
            End If
            If Me.dgv_Columns.Item("Field Name", i).Value = "UPDATED_DTTM" Then
                Me.dgv_Columns.Item("Property Name", i).Value = "UpdatedDate"
            End If
        Next

    End Sub

#End Region

#Region " SQL Objects "

    Private Sub GenerateSQLObjects()

        If Me.txt_Username.Text = String.Empty Then
            StoredProcedureUserName = Environment.UserName
        Else
            StoredProcedureUserName = Me.txt_Username.Text
        End If

        StoredProcedureObjectName = New StringBuilder

        For Each Character As Char In Me.txt_ObjectName.Text.Trim
            If Char.IsUpper(Character) Then
                StoredProcedureObjectName.Append("_" & Char.ToLower(Character))
            Else
                StoredProcedureObjectName.Append(Character)
            End If
        Next

        Me.GenerateInsert()
        Me.GenerateUpdate()
        Me.GenerateDelete()
        Me.GenerateGetByID()
        Me.GenerateGetList()

    End Sub

    Private Sub GenerateInsert()

        Dim StoredProcedureName As String = String.Empty
        Dim StoredProcedure As New StringBuilder()
        Dim StoredProcedureStreamWriter As System.IO.StreamWriter
        Dim IdentityColumnFlag As Boolean = False
        Dim ParameterCount As Integer = 0
        Dim ColumnName As String = String.Empty
        Dim ColumnLength As String = String.Empty

        'Stored Procedure Name
        If Me.txt_SectionName.Text.Trim <> String.Empty Then
            StoredProcedureName = "usp_insert_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_" & Replace(Me.txt_ObjectName.Text.Trim, " ", "_").ToLower()
        Else
            StoredProcedureName = "usp_insert_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString()
        End If

        'Comments
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("/***********************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Object:" & vbTab & StoredProcedureName.ToString & vbCrLf)
        StoredProcedure.Append("* Description:" & vbTab & "Inserts " & Me.txt_ObjectName.Text & " record." & vbCrLf)
        StoredProcedure.Append("* Parameters:" & vbCrLf)

        For i = 0 To Me.dgv_Columns.SelectedRows.Count - 1
            If InStr(Me.dgv_Columns(4, i).Value, "identity") > 0 Then
                'Identity Column
                IdentityColumnFlag = True
            Else
                Select Case Me.dgv_Columns(3, i).Value.ToString
                    Case "CREATED_DTTM", "WKR_ID_CREATED_TXT", "UPDATED_DTTM", "WKR_ID_UPDATED_TXT"
                        'Ignore
                    Case Else
                        StoredProcedure.Append("*" & vbTab & vbTab & "@" & Me.dgv_Columns(3, i).Value.ToString)
                        StoredProcedure.Append(Space(60 - Len(Me.dgv_Columns(3, i).Value.ToString)))
                        Select Case Me.dgv_Columns(4, i).Value.ToString()
                            Case "int identity"
                                StoredProcedure.Append("int")
                            Case "bigint", "int", "ntext", "text", "datetime", "date", "smallint", "decimal", "money", "bit", "tinyint"
                                'Data types that do not require length
                                StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString)
                            Case Else
                                'Others like varchar, char, nvarchar etc.
                                StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString & "(")
                                If (Me.dgv_Columns(4, i).Value.ToString = "0") Then
                                    ColumnLength = "MAX"
                                Else
                                    ColumnLength = Me.dgv_Columns(5, i).Value.ToString
                                End If
                                StoredProcedure.Append(ColumnLength & ")")
                        End Select
                        StoredProcedure.Append(vbCrLf)
                End Select
            End If
        Next

        StoredProcedure.Append("*" & vbTab & vbTab & "@WKR_ID_TXT")
        StoredProcedure.Append(Space(60 - Len("WKR_ID_TXT")))
        StoredProcedure.Append("varchar(100)" & vbCrLf)

        StoredProcedure.Append("*" & vbCrLf)
        If IdentityColumnFlag Then
            StoredProcedure.Append("* Returns:      Identity column of new record." & vbCrLf)
        Else
            StoredProcedure.Append("* Returns:      N/A" & vbCrLf)
        End If
        StoredProcedure.Append("* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted." & vbCrLf)

        StoredProcedure.Append("*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist." & vbCrLf)

        StoredProcedure.Append("* Version:      1.0" & vbCrLf)
        StoredProcedure.Append("* Created by:   " & StoredProcedureUserName & vbCrLf)
        StoredProcedure.Append("* Created:      " & Date.Today & vbCrLf)
        StoredProcedure.Append("************************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Change History: Date, Name, Description" & vbCrLf)
        StoredProcedure.Append("* " & Date.Today & "    " & StoredProcedureUserName & "     Auto-generated" & vbCrLf)
        StoredProcedure.Append("* " & vbCrLf)
        StoredProcedure.Append("***********************************************************************************************************************/" & vbCrLf & vbCrLf)

        'Stored procedure name
        StoredProcedure.Append("CREATE PROCEDURE [" & StoredProcedureName.ToString() & "]" & vbCrLf)

        ParameterCount = 0

        'Building Parameter list
        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                If InStr(Me.dgv_Columns(4, i).Value.ToString, "identity") = 0 Then
                    'Ignore CREATED_DTTM, UPDATED_DTTM, WKR_ID_CREATED_TXT, WKR_ID_UPDATED_TXT
                    Select Case Me.dgv_Columns(3, i).Value.ToString
                        Case "CREATED_DTTM", "UPDATED_DTTM", "WKR_ID_CREATED_TXT", "WKR_ID_UPDATED_TXT"
                            'Ignore
                        Case Else

                            If ParameterCount <> 0 Then
                                StoredProcedure.Append("  , @" & Me.dgv_Columns(3, i).Value.ToString)
                            Else
                                StoredProcedure.Append("    @" & Me.dgv_Columns(3, i).Value.ToString)
                            End If

                            ParameterCount = ParameterCount + 1

                            StoredProcedure.Append(Space(60 - Len(Me.dgv_Columns(3, i).Value.ToString)))
                            Select Case Me.dgv_Columns(4, i).Value.ToString
                                Case "int identity"
                                    StoredProcedure.Append("int")
                                Case "bigint", "int", "ntext", "text", "datetime", "date", "smallint", "decimal", "money", "bit", "tinyint"
                                    'Data types that do not require length
                                    StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString)
                                Case Else
                                    'Others like varchar, char, nvarchar etc.
                                    StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString & "(")
                                    If (Me.dgv_Columns(5, i).Value.ToString = "0") Then
                                        ColumnLength = "MAX"
                                    Else
                                        ColumnLength = Me.dgv_Columns(5, i).Value.ToString
                                    End If
                                    StoredProcedure.Append(ColumnLength & ")")
                            End Select
                            StoredProcedure.Append(vbCrLf)
                    End Select
                End If
            End If
        Next

        StoredProcedure.Append("  , @WKR_ID_TXT")
        StoredProcedure.Append(Space(60 - Len("WKR_ID_TXT")))
        StoredProcedure.Append("varchar(100)" & vbCrLf)

        If IdentityColumnFlag Then
            StoredProcedure.Append("  , @ReturnValue")
            StoredProcedure.Append(Space(60 - Len("ReturnValue")))
            StoredProcedure.Append("int OUTPUT" & vbCrLf)
        End If

        StoredProcedure.Append(vbCrLf & "AS" & vbCrLf & vbCrLf)
        StoredProcedure.Append(Space(5) & "DECLARE @error   int" & vbCrLf & vbCrLf)
        StoredProcedure.Append(Space(5) & "INSERT INTO " & Me.cmb_Table.SelectedValue.ToString & vbCrLf)
        StoredProcedure.Append(Space(10) & "(" & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                If InStr(Me.dgv_Columns(4, i).Value.ToString, "identity") = 0 Then
                    If ParameterCount = 0 Then
                        ColumnName = "  " & Me.dgv_Columns(3, i).Value.ToString
                    Else
                        ColumnName = ", " & Me.dgv_Columns(3, i).Value.ToString
                    End If
                    StoredProcedure.Append(Space(10) & ColumnName & vbCrLf)
                    ParameterCount = ParameterCount + 1
                End If
            End If
        Next

        StoredProcedure.Append(Space(10) & ")" & vbCrLf)
        StoredProcedure.Append(Space(5) & "VALUES" & vbCrLf)
        StoredProcedure.Append(Space(10) & "(" & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                If InStr(Me.dgv_Columns(4, i).Value, "identity") = 0 Then
                    If ParameterCount = 0 Then
                        ColumnName = "  "
                    Else
                        ColumnName = ", "
                    End If

                    Select Case Me.dgv_Columns(3, i).Value.ToString
                        Case "CREATED_DTTM", "UPDATED_DTTM"
                            ColumnName = ColumnName & "GETDATE()"
                        Case "WKR_ID_CREATED_TXT", "WKR_ID_UPDATED_TXT"
                            ColumnName = ColumnName & "@WKR_ID_TXT"
                        Case Else
                            ColumnName = ColumnName & "@" & Me.dgv_Columns(3, i).Value.ToString()
                    End Select

                    StoredProcedure.Append(Space(10) & ColumnName & vbCrLf)

                    ParameterCount = ParameterCount + 1
                End If
            End If
        Next

        StoredProcedure.Append(Space(10) & ")" & vbCrLf & vbCrLf)
        StoredProcedure.Append(Space(5) & "SELECT @error = @@ERROR" & vbCrLf)

        If IdentityColumnFlag Then
            StoredProcedure.Append(Space(10) & ", @ReturnValue = SCOPE_IDENTITY()" & vbCrLf & vbCrLf)
        End If

        StoredProcedure.Append(Space(5) & "IF @error != 0" & vbCrLf)
        StoredProcedure.Append(Space(10) & "RAISERROR('Error adding " & Me.txt_ObjectName.Text & " record.', 18,1)" & vbCrLf & vbCrLf)

        StoredProcedure.Append("RETURN" & vbCrLf)
        StoredProcedure.Append("GO")

        If Me.FileExistsCheck(Me.FilePath + StoredProcedureName + ".sql") Then
            Try

                StoredProcedureStreamWriter = New System.IO.StreamWriter(Me.FilePath + StoredProcedureName + ".sql")
                StoredProcedureStreamWriter.Write(StoredProcedure.ToString)
                StoredProcedureStreamWriter.Close()
                StoredProcedureStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating SQL Insert Stored Procedure")

            End Try
        End If

    End Sub

    Private Sub GenerateUpdate()

        Dim StoredProcedureName As String = String.Empty
        Dim StoredProcedure As New StringBuilder()
        Dim StoredProcedureStreamWriter As System.IO.StreamWriter
        Dim PrimaryKeyColumnFlag As Boolean = False
        Dim PrimaryKeyCount As Integer = 0
        Dim ParameterCount As Integer = 0
        Dim ColumnName As String = String.Empty
        Dim ColumnLength As String = String.Empty

        If Me.txt_SectionName.Text.Trim <> String.Empty Then
            StoredProcedureName = "usp_update_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & "_" & Replace(Me.txt_SectionName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString()
        Else
            StoredProcedureName = "usp_update_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString()
        End If

        'Start of comments
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("/***********************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Object:       " & StoredProcedureName.ToString & vbCrLf)
        StoredProcedure.Append("* Description:  Updates " & Me.txt_ObjectName.Text & " record using supplied parameters." & vbCrLf)
        StoredProcedure.Append("* Parameters:   " & vbCrLf)

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                'Ignore CREATED_DTTM, UPDATED_DTTM, WKR_ID_CREATED_TXT, WKR_ID_UPDATED_TXT
                Select Case Me.dgv_Columns(3, i).Value.ToString
                    Case "CREATED_DTTM", "WKR_ID_CREATED_TXT", "WKR_ID_UPDATED_TXT"
                        'Ignore
                    Case Else
                        StoredProcedure.Append("*               @" & Me.dgv_Columns(3, i).Value.ToString)
                        StoredProcedure.Append(Space(60 - Len(Me.dgv_Columns(3, i).Value.ToString)))
                        Select Case Me.dgv_Columns(4, i).Value.ToString
                            Case "int identity"
                                StoredProcedure.Append("int")
                            Case "bigint", "int", "ntext", "text", "datetime", "date", "smallint", "decimal", "money", "bit", "tinyint"
                                'Data types that do not require length
                                StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString)
                            Case Else
                                'Others like varchar, char, nvarchar etc.
                                StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString & "(")
                                If (Me.dgv_Columns(5, i).Value.ToString = "0") Then
                                    ColumnLength = "MAX"
                                Else
                                    ColumnLength = Me.dgv_Columns(5, i).Value.ToString
                                End If
                                StoredProcedure.Append(ColumnLength & ")")
                        End Select
                        StoredProcedure.Append(vbCrLf)
                End Select
            End If
        Next i

        StoredProcedure.Append("*               @WKR_ID_TXT")
        StoredProcedure.Append(Space(60 - Len("WKR_ID_TXT")))
        StoredProcedure.Append("varchar(100)" & vbCrLf)

        StoredProcedure.Append("*" & vbCrLf)
        StoredProcedure.Append("* Returns:      Number of rows affected." & vbCrLf)
        StoredProcedure.Append("* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data." & vbCrLf)
        StoredProcedure.Append("* Version:      1.0" & vbCrLf)
        StoredProcedure.Append("* Created by:   " & StoredProcedureUserName & vbCrLf)
        StoredProcedure.Append("* Created:      " & Date.Today & vbCrLf)
        StoredProcedure.Append("************************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Change History: Date, Name, Description" & vbCrLf)
        StoredProcedure.Append("* " & Date.Today & "    " & StoredProcedureUserName & "     Auto-generated" & vbCrLf)
        StoredProcedure.Append("* " & vbCrLf)
        StoredProcedure.Append("***********************************************************************************************************************/" & vbCrLf & vbCrLf)

        StoredProcedure.Append("CREATE PROCEDURE [" & StoredProcedureName.ToString() & "]" & vbCrLf & vbCrLf) 'Stored procedure name

        ParameterCount = 0

        'Building Parameter list
        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                'ignore CREATED_DTTM, UPDATED_DTTM, WKR_ID_CREATED_TXT, WKR_ID_UPDATED_TXT
                Select Case Me.dgv_Columns(3, i).Value.ToString
                    Case "CREATED_DTTM", "WKR_ID_CREATED_TXT", "WKR_ID_UPDATED_TXT"
                        'Ignore
                    Case Else
                        If ParameterCount <> 0 Then
                            StoredProcedure.Append("  , @" & Me.dgv_Columns(3, i).Value.ToString)
                        Else
                            StoredProcedure.Append("    @" & Me.dgv_Columns(3, i).Value.ToString)
                        End If

                        ParameterCount = ParameterCount + 1

                        StoredProcedure.Append(Space(60 - Len(Me.dgv_Columns(3, i).Value.ToString)))
                        Select Case Me.dgv_Columns(4, i).Value.ToString
                            Case "int identity"
                                StoredProcedure.Append("int")
                            Case "bigint", "int", "ntext", "text", "datetime", "date", "smallint", "decimal", "money", "bit", "tinyint"
                                'Data types that do not require length
                                StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString)
                            Case Else
                                'Others like varchar, char, nvarchar etc.
                                StoredProcedure.Append(Me.dgv_Columns(4, i).Value.ToString & "(")
                                If (Me.dgv_Columns(5, i).Value.ToString = "0") Then
                                    ColumnLength = "MAX"
                                Else
                                    ColumnLength = Me.dgv_Columns(5, i).Value.ToString
                                End If
                                StoredProcedure.Append(ColumnLength & ")")
                        End Select

                        StoredProcedure.Append(vbCrLf)
                End Select
            End If
        Next

        StoredProcedure.Append("  , @WKR_ID_TXT")
        StoredProcedure.Append(Space(60 - Len("WKR_ID_TXT")))
        StoredProcedure.Append("varchar(100)" & vbCrLf)

        StoredProcedure.Append(vbCrLf & "  , @ReturnValue")
        StoredProcedure.Append(Space(60 - Len("ReturnValue")))
        StoredProcedure.Append("int OUTPUT" & vbCrLf)

        StoredProcedure.Append(vbCrLf & "AS" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(5) & "DECLARE @error   int" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(7) & "UPDATE " & Me.cmb_Table.SelectedValue.ToString & vbCrLf)
        StoredProcedure.Append(Space(7) & "SET" & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then

                ColumnName = Me.dgv_Columns(3, i).Value.ToString
                'Check if selected column is primary key
                PrimaryKeyColumnFlag = False

                If Me.dgv_Columns(1, i).Value.ToString = "Yes" Then
                    PrimaryKeyColumnFlag = True
                    PrimaryKeyCount = PrimaryKeyCount + 1
                End If

                If Not PrimaryKeyColumnFlag Then
                    Select Case Me.dgv_Columns(3, i).Value.ToString
                        Case "CREATED_DTTM", "WKR_ID_CREATED_TXT"
                            ' Ignore
                            ColumnName = String.Empty
                        Case "UPDATED_DTTM"
                            ColumnName = ColumnName & Space(60 - Len(ColumnName)) & " = GETDATE()"
                        Case "WKR_ID_UPDATED_TXT"
                            ColumnName = ColumnName & Space(60 - Len(ColumnName)) & " = @WKR_ID_TXT"
                        Case Else
                            ColumnName = ColumnName & Space(60 - Len(ColumnName)) & " = @" & ColumnName
                    End Select

                    If Len(ColumnName) > 0 Then
                        If ParameterCount <> 0 Then
                            StoredProcedure.Append(Space(10) & ", " & ColumnName)
                        Else
                            StoredProcedure.Append(Space(12) & ColumnName)
                        End If
                        StoredProcedure.Append(vbCrLf)
                        ParameterCount = ParameterCount + 1
                    End If
                End If
            End If
        Next

        StoredProcedure.Append(vbCrLf)

        ParameterCount = 0

        If PrimaryKeyCount > 0 Then

            StoredProcedure.Append(Space(7) & "WHERE" & vbCrLf)

            For i = 0 To Me.dgv_Columns.Rows.Count - 1
                If Me.dgv_Columns(2, i).Selected Then
                    If Me.dgv_Columns(1, i).Value.ToString = "Yes" Then
                        If ParameterCount <> 0 Then StoredProcedure.Append(" AND " & vbCrLf)
                        ColumnName = Me.dgv_Columns(3, i).Value.ToString
                        StoredProcedure.Append(Space(10) & ColumnName & Space(58 - Len(ColumnName)) & "     = @" & ColumnName)
                        ParameterCount = ParameterCount + 1
                    End If
                End If
            Next

        End If

        StoredProcedure.Append(Space(7) & vbCrLf)
        StoredProcedure.Append(Space(7) & "AND " & vbCrLf)
        StoredProcedure.Append(Space(10) & "ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')" & vbCrLf)

        StoredProcedure.Append(Space(10) & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(5) & "SELECT @error = @@ERROR" & vbCrLf)
        StoredProcedure.Append(Space(10) & ", @ReturnValue = @@ROWCOUNT" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(5) & "IF @error != 0" & vbCrLf)
        StoredProcedure.Append(Space(10) & "RAISERROR('Error updating " & Me.txt_ObjectName.Text & " record.', 18,1)" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(5) & "IF @ReturnValue = 0" & vbCrLf)
        StoredProcedure.Append(Space(10) & "RAISERROR('Data was changed/deleted prior to update.', 18,100)" & vbCrLf & vbCrLf)

        StoredProcedure.Append("RETURN" & vbCrLf)
        StoredProcedure.Append("GO")

        If Me.FileExistsCheck(Me.FilePath + StoredProcedureName + ".sql") Then
            Try

                StoredProcedureStreamWriter = New System.IO.StreamWriter(Me.FilePath + StoredProcedureName + ".sql")
                StoredProcedureStreamWriter.Write(StoredProcedure.ToString)
                StoredProcedureStreamWriter.Close()
                StoredProcedureStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating SQL Update Stored Procedure")

            End Try
        End If

    End Sub

    Private Sub GenerateDelete()

        Dim StoredProcedureName As String = String.Empty
        Dim StoredProcedure As New StringBuilder()
        Dim StoredProcedureStreamWriter As System.IO.StreamWriter
        Dim ParameterCount As Integer = 0
        Dim ColumnName As String = String.Empty

        If Me.txt_SectionName.Text.Trim <> String.Empty Then
            StoredProcedureName = "usp_delete_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & "_" & Replace(Me.txt_SectionName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString()
        Else
            StoredProcedureName = "usp_delete_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString()
        End If

        'Start of comments
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("/***********************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Object:       " & StoredProcedureName.ToString & vbCrLf)
        StoredProcedure.Append("* Description:  Deletes " & Me.txt_ObjectName.Text & " record for given key field(s)." & vbCrLf)
        StoredProcedure.Append("* Parameters:   " & vbCrLf)
        StoredProcedure.Append("*               @identity")
        StoredProcedure.Append(Space(60 - Len("identity")))
        StoredProcedure.Append("int")
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("*" & vbCrLf)
        StoredProcedure.Append("* Returns:      Number of rows affected." & vbCrLf)
        StoredProcedure.Append("* Version:      1.0" & vbCrLf)
        StoredProcedure.Append("* Created by:   " & StoredProcedureUserName & vbCrLf)
        StoredProcedure.Append("* Created:      " & Date.Today & vbCrLf)
        StoredProcedure.Append("************************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Change History: Date, Name, Description" & vbCrLf)
        StoredProcedure.Append("* " & Date.Today & "    " & StoredProcedureUserName & "     Auto-generated" & vbCrLf)
        StoredProcedure.Append("* " & vbCrLf)
        StoredProcedure.Append("***********************************************************************************************************************/" & vbCrLf & vbCrLf)
        'End of comments

        StoredProcedure.Append("CREATE PROCEDURE [" & StoredProcedureName.ToString() & "]" & vbCrLf & vbCrLf) 'Stored procedure name

        StoredProcedure.Append("    @identity")
        StoredProcedure.Append(Space(60 - Len("identity")))
        StoredProcedure.Append("int")
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append(vbCrLf & "  , @ReturnValue")
        StoredProcedure.Append(Space(60 - Len("ReturnValue")))
        StoredProcedure.Append("int OUTPUT" & vbCrLf)

        StoredProcedure.Append(vbCrLf & "AS" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(5) & "DECLARE @error   int" & vbCrLf & vbCrLf)

        'Body of the procedure
        StoredProcedure.Append(Space(7) & "DELETE FROM " & Me.cmb_Table.SelectedValue.ToString() & vbCrLf)
        StoredProcedure.Append(Space(7) & "WHERE" & vbCrLf)

        StoredProcedure.Append(Space(7) & " " & vbCrLf)
        StoredProcedure.Append(Space(7) & "-- @TODO:  Correct the following as necessary" & vbCrLf)
        StoredProcedure.Append(Space(7) & " " & vbCrLf)

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                If Me.dgv_Columns(1, i).Value.ToString = "Yes" Then
                    If ParameterCount <> 0 Then StoredProcedure.Append(" AND " & vbCrLf)
                    ColumnName = Me.dgv_Columns(3, i).Value.ToString
                    StoredProcedure.Append(Space(10) & ColumnName & " = @identity" & vbCrLf)
                    ParameterCount = ParameterCount + 1
                End If
            End If
        Next

        StoredProcedure.Append(Space(10) & vbCrLf & vbCrLf)
        StoredProcedure.Append(Space(5) & "SELECT @error = @@ERROR" & vbCrLf)
        StoredProcedure.Append(Space(10) & ", @ReturnValue = @@ROWCOUNT" & vbCrLf & vbCrLf)
        StoredProcedure.Append(Space(5) & "IF @error != 0" & vbCrLf)
        StoredProcedure.Append(Space(10) & "RAISERROR('Error deleting " & Me.txt_ObjectName.Text & " record.', 18,1)" & vbCrLf & vbCrLf)

        StoredProcedure.Append("RETURN" & vbCrLf & vbCrLf)
        StoredProcedure.Append("GO")

        If Me.FileExistsCheck(Me.FilePath + StoredProcedureName + ".sql") Then
            Try

                StoredProcedureStreamWriter = New System.IO.StreamWriter(Me.FilePath + StoredProcedureName + ".sql")
                StoredProcedureStreamWriter.Write(StoredProcedure.ToString)
                StoredProcedureStreamWriter.Close()
                StoredProcedureStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating SQL Delete Stored Procedure")

            End Try
        End If

    End Sub

    Private Sub GenerateGetByID()

        Dim StoredProcedureName As String = String.Empty
        Dim StoredProcedure As New StringBuilder()
        Dim StoredProcedureStreamWriter As System.IO.StreamWriter
        Dim ParameterCount As Integer = 0
        Dim ColumnName As String = String.Empty
        Dim ColumnLength As String = String.Empty

        If Me.txt_SectionName.Text.Trim <> String.Empty Then
            StoredProcedureName = "usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & "_" & Replace(Me.txt_SectionName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_by_id"
        Else
            StoredProcedureName = "usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_by_id"
        End If

        'Start of comments
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("/***********************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Object:       " & StoredProcedureName.ToString & vbCrLf)
        StoredProcedure.Append("* Description:  Retrieves " & Me.txt_ObjectName.Text & " record for given key field(s)." & vbCrLf)
        StoredProcedure.Append("* Parameters:   " & vbCrLf)
        StoredProcedure.Append("*               @identity")
        StoredProcedure.Append(Space(60 - Len("identity")))
        StoredProcedure.Append("int")
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("*" & vbCrLf)
        StoredProcedure.Append("* Returns:      Recordset." & vbCrLf)
        StoredProcedure.Append("* Comments:     Developer may need to manually join to other tables, such as code tables," & vbCrLf)
        StoredProcedure.Append("*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause." & vbCrLf)
        StoredProcedure.Append("* Version:      1.0" & vbCrLf)
        StoredProcedure.Append("* Created by:   " & StoredProcedureUserName & vbCrLf)
        StoredProcedure.Append("* Created:      " & Date.Today & vbCrLf)
        StoredProcedure.Append("************************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Change History: Date, Name, Description" & vbCrLf)
        StoredProcedure.Append("* " & Date.Today & "    " & StoredProcedureUserName & "     Auto-generated" & vbCrLf)
        StoredProcedure.Append("* " & vbCrLf)
        StoredProcedure.Append("***********************************************************************************************************************/" & vbCrLf & vbCrLf)
        'End of comments

        StoredProcedure.Append("CREATE PROCEDURE [" & StoredProcedureName.ToString() & "]" & vbCrLf & vbCrLf) 'Stored procedure name

        StoredProcedure.Append("    @identity")
        StoredProcedure.Append(Space(60 - Len("identity")))
        StoredProcedure.Append("int")
        StoredProcedure.Append(vbCrLf)

        StoredProcedure.Append(vbCrLf & "AS" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(7) & "SELECT " & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                ColumnName = Me.dgv_Columns(3, i).Value.ToString()
                If ParameterCount <> 0 Then
                    StoredProcedure.Append(Space(10) & ", " & ColumnName)
                Else
                    StoredProcedure.Append(Space(10) & "  " & ColumnName)
                End If
                StoredProcedure.Append(vbCrLf)
                ParameterCount = ParameterCount + 1
            End If
        Next

        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append(Space(7) & "FROM " & Me.cmb_Table.SelectedValue.ToString & vbCrLf)
        StoredProcedure.Append(vbCrLf)

        StoredProcedure.Append(Space(7) & "WHERE" & vbCrLf)
        StoredProcedure.Append(Space(7) & " " & vbCrLf)
        StoredProcedure.Append(Space(7) & "-- @TODO:  Correct the following as necessary" & vbCrLf)
        StoredProcedure.Append(Space(7) & " " & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                If Me.dgv_Columns(1, i).Value.ToString = "Yes" Then
                    If ParameterCount <> 0 Then StoredProcedure.Append(" AND " & vbCrLf)
                    ColumnName = Me.dgv_Columns(3, i).Value.ToString
                    StoredProcedure.Append(Space(10) & ColumnName & " = @identity" & vbCrLf)
                    ParameterCount = ParameterCount + 1
                End If
            End If
        Next

        StoredProcedure.Append(Space(10) & vbCrLf & vbCrLf)
        StoredProcedure.Append("RETURN" & vbCrLf & vbCrLf)
        StoredProcedure.Append("GO")

        If Me.FileExistsCheck(Me.FilePath + StoredProcedureName + ".sql") Then
            Try

                StoredProcedureStreamWriter = New System.IO.StreamWriter(Me.FilePath + StoredProcedureName + ".sql")
                StoredProcedureStreamWriter.Write(StoredProcedure.ToString)
                StoredProcedureStreamWriter.Close()
                StoredProcedureStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating SQL GetByID Stored Procedure")

            End Try
        End If

    End Sub

    Private Sub GenerateGetList()

        Dim StoredProcedureName As String = String.Empty
        Dim StoredProcedure As New StringBuilder()
        Dim StoredProcedureStreamWriter As System.IO.StreamWriter
        Dim ParameterCount As Integer = 0
        Dim ColumnName As String = String.Empty

        If Me.txt_SectionName.Text.Trim <> String.Empty Then
            StoredProcedureName = "usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & "_" & Replace(Me.txt_SectionName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_list"
        Else
            StoredProcedureName = "usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_list"
        End If

        'Start of comments
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("/***********************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Object:       " & StoredProcedureName.ToString & vbCrLf)
        StoredProcedure.Append("* Description:  Retrieves " & Me.txt_ObjectName.Text & " list for given parameter(s)." & vbCrLf)
        StoredProcedure.Append("* Parameters:   " & vbCrLf)

        StoredProcedure.Append("*               @identity")
        StoredProcedure.Append(Space(60 - Len("@identity")))
        StoredProcedure.Append("int")
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append("*" & vbCrLf)
        StoredProcedure.Append("* Returns:      Recordset." & vbCrLf)
        StoredProcedure.Append("* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval." & vbCrLf)
        StoredProcedure.Append("*               This proc expects id_person and/or id_file to generate list; modify as necessary." & vbCrLf)
        StoredProcedure.Append("*               Include ORDER BY clause as necessary." & vbCrLf)
        StoredProcedure.Append("* Version:      1.0" & vbCrLf)
        StoredProcedure.Append("* Created by:   " & StoredProcedureUserName & vbCrLf)
        StoredProcedure.Append("* Created:      " & Date.Today & vbCrLf)
        StoredProcedure.Append("************************************************************************************************************************" & vbCrLf)
        StoredProcedure.Append("* Change History: Date, Name, Description" & vbCrLf)
        StoredProcedure.Append("* " & Date.Today & "    " & StoredProcedureUserName & "     Auto-generated" & vbCrLf)
        StoredProcedure.Append("* " & vbCrLf)
        StoredProcedure.Append("***********************************************************************************************************************/" & vbCrLf & vbCrLf)
        'End of comments

        StoredProcedure.Append("CREATE PROCEDURE [" & StoredProcedureName.ToString() & "]" & vbCrLf & vbCrLf) 'Stored procedure name

        StoredProcedure.Append("    @identity")
        StoredProcedure.Append(Space(60 - Len("@identity")))
        StoredProcedure.Append("int")
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append(vbCrLf & "AS" & vbCrLf & vbCrLf)

        StoredProcedure.Append(Space(7) & "SELECT " & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                ColumnName = Me.dgv_Columns(3, i).Value.ToString()

                If ParameterCount <> 0 Then
                    StoredProcedure.Append(Space(10) & ", " & ColumnName)
                Else
                    StoredProcedure.Append(Space(10) & "  " & ColumnName)
                End If
                StoredProcedure.Append(vbCrLf)
                ParameterCount = ParameterCount + 1
            End If
        Next

        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append(Space(7) & "FROM " & Me.cmb_Table.SelectedValue.ToString & vbCrLf)
        StoredProcedure.Append(vbCrLf)
        StoredProcedure.Append(Space(7) & "WHERE" & vbCrLf)

        StoredProcedure.Append(Space(7) & " " & vbCrLf)
        StoredProcedure.Append(Space(7) & "-- @TODO:  Correct the following as necessary" & vbCrLf)
        StoredProcedure.Append(Space(7) & " " & vbCrLf)

        ParameterCount = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then
                If Me.dgv_Columns(1, i).Value.ToString = "Yes" Then
                    If ParameterCount <> 0 Then StoredProcedure.Append(" AND " & vbCrLf)
                    ColumnName = Me.dgv_Columns(3, i).Value.ToString
                    StoredProcedure.Append(Space(10) & ColumnName & " = @identity" & vbCrLf)
                    ParameterCount = ParameterCount + 1
                End If
            End If
        Next

        StoredProcedure.Append(Space(10) & vbCrLf & vbCrLf)
        StoredProcedure.Append("RETURN" & vbCrLf & vbCrLf)
        StoredProcedure.Append("GO")

        If Me.FileExistsCheck(Me.FilePath + StoredProcedureName + ".sql") Then
            Try

                StoredProcedureStreamWriter = New System.IO.StreamWriter(Me.FilePath + StoredProcedureName + ".sql")
                StoredProcedureStreamWriter.Write(StoredProcedure.ToString)
                StoredProcedureStreamWriter.Close()
                StoredProcedureStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating SQL GetList Stored Procedure")

            End Try
        End If

    End Sub

#End Region

#Region " Business Objects "

    Private Sub GenerateBusinessObjects()

        If chk_C.Checked = True Then
            Me.GenerateCSharpBuisnessEntity()
            Me.GenerateCSharpBuisnessObject()
        End If

    End Sub

#Region " C# "

    Private Sub GenerateCSharpBuisnessEntity()

        Dim BusinessEntityFile As String = String.Empty
        Dim BusinessEntity As New StringBuilder()
        Dim BusinessStreamWriter As System.IO.StreamWriter
        Dim PrivateMemberName As String = String.Empty
        Dim PropertyName As String = String.Empty
        Dim SQLDataType As String = String.Empty

        BusinessEntityFile = Me.FilePath & Me.txt_ObjectName.Text.Trim & "BE.cs"

        BusinessEntity.Append("#region Using" & vbCrLf & vbCrLf)
        BusinessEntity.Append("using System;" & vbCrLf)
        BusinessEntity.Append("using System.Runtime.Serialization;" & vbCrLf)
        BusinessEntity.Append("using " & Me.txt_ProjectName.Text.Trim & ".Base;" & vbCrLf & vbCrLf)
        BusinessEntity.Append("#endregion" & vbCrLf & vbCrLf)

        BusinessEntity.Append("namespace " + Me.txt_ProjectName.Text.Trim & ".Engine.BusinessEntities" & vbCrLf)
        BusinessEntity.Append("{" & vbCrLf & vbCrLf)

        BusinessEntity.Append(vbTab & "#region BusinessEntitiy - " & Me.txt_ObjectName.Text.Trim & "BE" & vbCrLf & vbCrLf)
        BusinessEntity.Append(vbTab & "[DataContract]" & vbCrLf)
        BusinessEntity.Append(vbTab & "public class " & Me.txt_ObjectName.Text.Trim & "BE : BaseBE" & vbCrLf)
        BusinessEntity.Append(vbTab & "{" & vbCrLf & vbCrLf)

        BusinessEntity.Append(vbTab & vbTab & "#region Properties" & vbCrLf & vbCrLf)

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then

                PropertyName = Me.dgv_Columns(2, i).Value.ToString
                SQLDataType = Me.dgv_Columns(4, i).Value.ToString

                Select Case PropertyName
                    Case "CreatedDate", "CreatedByWkrId", "UpdatedDate", "UpdatedByWkrId"
                        'Nothing
                    Case Else
                        BusinessEntity.Append(vbTab & vbTab & "[DataMember]" & vbCrLf)
                        BusinessEntity.Append(vbTab & vbTab & "public " & Me.GetCodeCSharpDataType(SQLDataType) & " " & PropertyName & " { get; set; }" & vbCrLf & vbCrLf)
                End Select
            End If
        Next

        BusinessEntity.Append(vbTab & vbTab & "#endregion" & vbCrLf & vbCrLf)

        BusinessEntity.Append(vbTab & "}" & vbCrLf & vbCrLf)
        BusinessEntity.Append(vbTab & "#endregion" & vbCrLf & vbCrLf)
        BusinessEntity.Append("}")

        If Me.FileExistsCheck(BusinessEntityFile) Then
            Try

                BusinessStreamWriter = New System.IO.StreamWriter(BusinessEntityFile)
                BusinessStreamWriter.Write(BusinessEntity.ToString)
                BusinessStreamWriter.Close()
                BusinessStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating Business C# Model File")

            End Try
        End If

    End Sub

    Private Sub GenerateCSharpBuisnessObject()

        Dim BusinessObjectFile As String = String.Empty
        Dim BusinessObject As New StringBuilder()
        Dim BusinessObjectStreamWriter As System.IO.StreamWriter
        Dim PropertyName As String = String.Empty
        Dim SQLFieldName As String = String.Empty
        Dim SQLDataType As String = String.Empty
        Dim ParameterCount As Integer = 0

        BusinessObjectFile = Me.FilePath & Me.txt_ObjectName.Text.Trim & "BO.cs"

        BusinessObject.Append("#region Using" & vbCrLf & vbCrLf)
        BusinessObject.Append("using System;" & vbCrLf)
        BusinessObject.Append("using System.Collections.Generic;" & vbCrLf)
        BusinessObject.Append("using System.Data;" & vbCrLf)
        BusinessObject.Append("using System.Data.SqlClient;" & vbCrLf)
        BusinessObject.Append("using Meck.Data;" & vbCrLf)
        BusinessObject.Append("using " & Me.txt_ProjectName.Text.Trim & "Estimator.Engine.BusinessEntities;" & vbCrLf)
        BusinessObject.Append("using " & Me.txt_ProjectName.Text.Trim & ".Base;" & vbCrLf & vbCrLf)
        BusinessObject.Append("#endregion" & vbCrLf & vbCrLf)

        BusinessObject.Append("namespace " + Me.txt_ProjectName.Text.Trim & "Estimator.Engine.BusinessObjects" & vbCrLf)
        BusinessObject.Append("{" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & "#region BusinessObject - " & Me.txt_ObjectName.Text.Trim & "BO" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & "public class " & Me.txt_ObjectName.Text.Trim & "BO : BaseBO" & vbCrLf)
        BusinessObject.Append(vbTab & "{" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "#region Private Members" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "private string _errorMsg;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "private " & Me.txt_ObjectName.Text.Trim & "BE _" & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "private int _id;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "#endregion" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "#region Public Methods" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "public int Create(" & Me.txt_ObjectName.Text.Trim & "BE " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "int id;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_" & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE = " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (!this.Validate(ActionType.Create))" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw(new Exception(_errorMsg));" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "try" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "SqlParameter[] sqlParameters = new SqlParameter[" & (Me.GetParameterCount("Create") + 2).ToString & "];" & vbCrLf & vbCrLf)
        ParameterCount = 0
        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then

                PropertyName = Me.dgv_Columns(2, i).Value.ToString
                SQLFieldName = Me.dgv_Columns(3, i).Value.ToString
                SQLDataType = Me.dgv_Columns(4, i).Value.ToString

                If InStr(SQLDataType, "identity") = 0 Then
                    Select Case PropertyName
                        Case "CreatedDate", "CreatedByWkrId", "UpdatedDate", "UpdatedByWkrId"
                            'Nothing
                        Case Else
                            BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & ParameterCount.ToString() & "] = new SqlParameter(""@" & SQLFieldName & """, " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE." & PropertyName & ");" & vbCrLf)
                            ParameterCount = ParameterCount + 1
                    End Select
                End If
            End If
        Next
        BusinessObject.Append(vbCrLf & vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & ParameterCount.ToString() & "] = new SqlParameter(""@WKR_ID_TXT""," & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE" & ".UserId);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & (ParameterCount + 1).ToString() & "] = new SqlParameter(""@ReturnValue"",0);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & (ParameterCount + 1).ToString() & "].Direction  = ParameterDirection.Output;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "id = SqlWrapper.RunSPReturnInteger(""usp_insert_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & """, base.ConnectionString, ref sqlParameters);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "catch (Exception ex)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (ex);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return id;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "public int Delete(int id)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "int rows;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_id = id;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (!this.Validate(ActionType.Delete))" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (new Exception(_errorMsg));" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "try" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "SqlParameter[] sqlParameters = new SqlParameter[2];" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[0] = new SqlParameter(""identity"", id);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[1] = new SqlParameter(""@ReturnValue"", 0);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[1].Direction = ParameterDirection.Output;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "rows = SqlWrapper.RunSPReturnInteger(""usp_delete_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & """, base.ConnectionString, ref sqlParameters);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "catch (Exception ex)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (ex);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return rows;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "public " & Me.txt_ObjectName.Text.Trim & "BE " & "GetById(int id)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_id = id;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (!this.Validate(ActionType.GetById))" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (new Exception(_errorMsg));" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & Me.txt_ObjectName.Text.Trim & "BE " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE = new " & Me.txt_ObjectName.Text.Trim & "BE();" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "DataSet dataSet;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "try" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "SqlParameter[] sqlParameters = new SqlParameter[1];" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[0] = new SqlParameter(""@identity"", id);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "dataSet = SqlWrapper.RunSPReturnDS(""usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_by_id"", base.ConnectionString, ref sqlParameters);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "catch (Exception ex)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (ex);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (dataSet.Tables[0].Rows.Count > 0)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "public DataSet GetDataSet(int id)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_id = id;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (!this.Validate(ActionType.GetDataSet))" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (new Exception(_errorMsg));" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "DataSet dataSet;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "try" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "SqlParameter[] sqlParameters = new SqlParameter[1];" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[0] = new SqlParameter(""@identity"", id);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "dataSet = SqlWrapper.RunSPReturnDS(""usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_list"", base.ConnectionString, ref sqlParameters);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "catch (Exception ex)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (ex);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return dataSet;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "public List<" & Me.txt_ObjectName.Text.Trim & "BE> GetList(int id)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_id = id;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (!this.Validate(ActionType.GetList))" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (new Exception(_errorMsg));" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "DataSet dataSet;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "List<" & Me.txt_ObjectName.Text.Trim & "BE> " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BEList = new List<" & Me.txt_ObjectName.Text.Trim & "BE>();" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "try" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "SqlParameter[] sqlParameters = new SqlParameter[1];" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[0] = new SqlParameter(""@identity"", id);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "dataSet = SqlWrapper.RunSPReturnDS(""usp_select_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & "_get_list"", base.ConnectionString, ref sqlParameters);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "foreach (DataRow dataRow in dataSet.Tables[0].Rows)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BEList.Add(this.ConvertDataRowToBE(dataRow));" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "catch (Exception ex)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (ex);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BEList;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "public int Update(" & Me.txt_ObjectName.Text.Trim & "BE " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "int rows;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_" & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE = " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "if (!this.Validate(ActionType.Update))" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "throw (new Exception(_errorMsg));" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "try" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "SqlParameter[] sqlParameters = new SqlParameter[" & (Me.GetParameterCount("Update") + 2).ToString & "];" & vbCrLf & vbCrLf)
        ParameterCount = 0
        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then

                PropertyName = Me.dgv_Columns(2, i).Value.ToString
                SQLFieldName = Me.dgv_Columns(3, i).Value.ToString
                SQLDataType = Me.dgv_Columns(4, i).Value.ToString

                Select Case PropertyName
                    Case "CreatedDate", "CreatedByWkrId", "UpdatedByWkrId"
                        'Nothing
                    Case Else
                        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & ParameterCount.ToString() & "] = new SqlParameter(""@" & SQLFieldName & """, " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE." & PropertyName & ");" & vbCrLf)
                        ParameterCount = ParameterCount + 1
                End Select
            End If
        Next
        BusinessObject.Append(vbCrLf & vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & ParameterCount.ToString() & "] = new SqlParameter(""@WKR_ID_TXT""," & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE" & ".UserId);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & (ParameterCount + 1).ToString() & "] = new SqlParameter(""@ReturnValue"",0);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "sqlParameters[" & (ParameterCount + 1).ToString() & "].Direction  = ParameterDirection.Output;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "rows = SqlWrapper.RunSPReturnInteger(""usp_update_" & Replace(Me.txt_ProjectName.Text.Trim, " ", "_").ToLower() & StoredProcedureObjectName.ToString() & """, base.ConnectionString, ref sqlParameters);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "catch (Exception ex)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "throw (ex);" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return rows;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "#endregion" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "#region Private Methods" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "private bool Validate(ActionType actionType)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "// TODO: Add validation rules as necessary." & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "_errorMsg = String.Empty;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "switch (actionType)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "case ActionType.Create:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "return(_errorMsg == String.Empty);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "case ActionType.Delete:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "return(_errorMsg == String.Empty);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "case ActionType.GetById:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "return(_errorMsg == String.Empty);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "case ActionType.GetDataSet:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "return(_errorMsg == String.Empty);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "case ActionType.GetList:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "return(_errorMsg == String.Empty);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "case ActionType.Update:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "return(_errorMsg == String.Empty);" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & "default:" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & vbTab & vbTab & "break;" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & "return true;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & vbTab & "private " & Me.txt_ObjectName.Text.Trim & "BE ConvertDataRowToBE(DataRow dataRow)" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "{" & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & vbTab & Me.txt_ObjectName.Text.Trim & "BE " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE = new " & Me.txt_ObjectName.Text.Trim & "BE();" & vbCrLf & vbCrLf)
        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then

                PropertyName = Me.dgv_Columns(2, i).Value.ToString
                SQLFieldName = Me.dgv_Columns(3, i).Value.ToString
                SQLDataType = Me.dgv_Columns(4, i).Value.ToString

                BusinessObject.Append(vbTab & vbTab & vbTab & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE." & PropertyName & " = TryToParse<" & Me.GetCodeCSharpDataType(SQLDataType) & ">(dataRow[""" & SQLFieldName & """]);" & vbCrLf)

            End If
        Next
        BusinessObject.Append(vbCrLf & vbTab & vbTab & vbTab & "return " & Me.FirstCharToLower(Me.txt_ObjectName.Text.Trim) & "BE;" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "}" & vbCrLf & vbCrLf)
        BusinessObject.Append(vbTab & vbTab & "#endregion" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & "}" & vbCrLf & vbCrLf)

        BusinessObject.Append(vbTab & "#endregion" & vbCrLf & vbCrLf)
        BusinessObject.Append("}")

        If Me.FileExistsCheck(BusinessObjectFile) Then
            Try

                BusinessObjectStreamWriter = New System.IO.StreamWriter(BusinessObjectFile)
                BusinessObjectStreamWriter.Write(BusinessObject.ToString)
                BusinessObjectStreamWriter.Close()
                BusinessObjectStreamWriter = Nothing

            Catch ex As Exception

                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Generating Business C# Controller File")

            End Try
        End If

    End Sub

#End Region

    Private Function GetCodeCSharpDataType(ByVal SQLDataType As String) As String

        Dim CodeDataType As String = String.Empty

        Select Case SQLDataType
            Case "bigint"
                CodeDataType = "long?"
            Case "numeric", "int", "int identity"
                CodeDataType = "int?"
            Case "nvarchar", "varchar", "ntext", "text", "char"
                CodeDataType = "string"
            Case "money", "decimal"
                CodeDataType = "decimal?"
            Case "smallint", "tinyint"
                CodeDataType = "short?"
            Case "datetime"
                CodeDataType = "DateTime?"
            Case "byte"
                CodeDataType = "byte?"
            Case "bit"
                CodeDataType = "bool?"
            Case "float"
                CodeDataType = "double?"
        End Select

        Return CodeDataType

    End Function

    Private Function GetCodeVbNetDataType(ByVal SQLDataType As String) As String

        Dim CodeDataType As String = String.Empty

        Select Case SQLDataType
            Case "bigint"
                CodeDataType = "Int64"
            Case "numeric", "int", "int identity"
                CodeDataType = "Int32"
            Case "nvarchar", "varchar", "ntext", "text", "char"
                CodeDataType = "String"
            Case "money", "decimal"
                CodeDataType = "Single"
            Case "smallint", "tinyint"
                CodeDataType = "Int16"
            Case "datetime"
                CodeDataType = "DateTime"
            Case "byte"
                CodeDataType = "Byte"
            Case "bit"
                CodeDataType = "Boolean"
            Case "float"
                CodeDataType = "Double"
        End Select

        Return CodeDataType

    End Function

    Private Function GetParameterCount(ByVal ServiceMethod As String) As Integer

        Dim PropertyName As String = String.Empty
        Dim SQLFieldName As String = String.Empty
        Dim SQLDataType As String = String.Empty
        Dim ParameterCount As Integer = 0

        For i = 0 To Me.dgv_Columns.Rows.Count - 1
            If Me.dgv_Columns(2, i).Selected Then

                PropertyName = Me.dgv_Columns(2, i).Value.ToString
                SQLFieldName = Me.dgv_Columns(3, i).Value.ToString
                SQLDataType = Me.dgv_Columns(4, i).Value.ToString

                Select Case PropertyName
                    Case "CreatedDate", "CreatedByWkrId", "UpdatedByWkrId"
                        'Nothing
                    Case Else
                        Select Case ServiceMethod
                            Case "Create"
                                If InStr(SQLDataType, "identity") = 0 Then
                                    If PropertyName <> "UpdatedDate" Then
                                        ParameterCount = ParameterCount + 1
                                    End If
                                End If
                            Case "Update"
                                ParameterCount = ParameterCount + 1
                        End Select
                End Select
            End If
        Next

        Return ParameterCount

    End Function

#End Region

    Private Function FirstCharToLower(ByVal InputString As String)

        If String.IsNullOrEmpty(InputString) Then
            Return InputString
        Else
            Return InputString.First().ToString().ToLower() + String.Join("", InputString.Skip(1))
        End If

    End Function

    Public Function FileExistsCheck(ByVal SourceFile As String) As Boolean

        Dim FileExistsFlag As Boolean = False

        If System.IO.File.Exists(SourceFile) Then
            If MsgBox("A file with the name " & SourceFile & " already exist this this naming convention in this location." & _
                    Chr(13) & Chr(13) & "Do you want to continue?", MsgBoxStyle.YesNo, "Files already exist in this location....") <> MsgBoxResult.Yes Then
                FileExistsFlag = False
            Else
                FileExistsFlag = True
            End If
        Else
            FileExistsFlag = True
        End If

        Return FileExistsFlag

    End Function

    Private Sub ClearCodeGenerationForm()

        Me.txt_ProjectName.Text = "AION"
        Me.txt_ObjectName.Text = ""
        'Me.cmb_Database.DataSource = Nothing
        'Me.cmb_Database.Items.Clear()
        Me.cmb_Table.DataSource = Nothing
        Me.cmb_Table.Items.Clear()
        Me.txt_SectionName.Text = ""
        Me.dgv_Columns.DataSource = Nothing

    End Sub

    Private Sub ClearDatabaseDetails()

        Me.cmb_Table.DataSource = Nothing
        Me.cmb_Table.Items.Clear()
        Me.dgv_Columns.DataSource = Nothing

    End Sub

    Private Sub ClearColumns()

        Me.dgv_Columns.DataSource = Nothing

    End Sub

#End Region

End Class