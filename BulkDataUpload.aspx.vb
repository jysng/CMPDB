Imports System.IO

Public Class BulkDataUpload
    Inherits System.Web.UI.Page
    Dim xtblMasterData = "CMPDB_tblMasterData"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()
        If Not IsPostBack Then
            ' CreateGrid("")
        End If
    End Sub

    Private Sub FillDropDown(dt As DataTable)
        ' PopulateDDFromDataTable(ddlTemplateType, dt)
    End Sub

    Protected Sub btnUploadTemplate_Click(sender As Object, e As EventArgs) Handles btnUploadTemplate.Click
        '' ReadFromDB()
        ''FromExcelToTable(ddlTemplateType.SelectedItem.Text, GetFileObject, True)
        'Dim mFileObj = GetFileObject()
        'Dim mSheetName = ddlTemplateType.SelectedItem.Text
        'Dim mPlantText = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "A15")

        'Dim mProjectName = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "B15")
        'Dim mStartupName = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "C15")
        'Dim mAllInChange = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "D15")
        'Dim mBU = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "G15")
        'Dim mPlatform = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "H15")
        'Dim mLeadingDepartment = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "K15")
        'Dim mProductionLine = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "L15")
        'Dim mProjectType = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "M15")
        'Dim mChangeType = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "N15")
        'Dim mCBN = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "O15")
        'Dim mSUL = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "Q15")
        'Dim mProjectLevelComplexity = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "T15")
        'Dim mQualificationLevel = ReadCellValueFromWorkSheetName(mFileObj, mSheetName, "U15")

        ''If ExistsInTable(mPlantText) Then

        ''End If
        'Dim str As New StringBuilder


        'mPlantText = ExistsInTable(mPlantText, xTblPlants, "Plant_ID", "Plant")
        'str.Append(mPlantText)
        'str.Append(",")
        'str.Append(mStartupName)
        'str.Append(",")

        'lblMessage.Text = sBtr.ToString
        'ExistsInTable(mPlantText, xTblPlants, "Plant_ID", "Plant")
        ' Dim file = FileUpload1.FileBytes
        'SaveToDatabase()
        'FromExcelToTable("temp_proj", GetFileObject, True)
        'FromExcelToTable("temp_Practioner", GetFileObject, True)
        'ExecuteProc("CMPDB_sp_InsertBulkDataPractionerMain")
        'ExecuteProc("CMPDB_sp_InsertBulkDataProjectMain")

        FromExcelToTable("temp_proj", FileUpload1.FileBytes, True)
        FromExcelToTable("temp_Practioner", FileUpload1.FileBytes, True)
        ExecuteProc("CMPDB_sp_InsertBulkDataPractionerMain")
        ExecuteProc("CMPDB_sp_InsertBulkDataProjectMain")
    End Sub

    Private Function ExistsInTable(mPlantText As String, mTableName As String, mColumnName As String, mColumnNameWhere As String) As String
        Dim mValue = GetSingleValue($"select {mColumnName} from {mTableName} where {mColumnNameWhere}='{mPlantText}'")
        If mValue = "" Then
            AddUpdateRecordsListControls(mTableName + ",'" + mPlantText + "',I")
            mValue = GetSingleValue($"select IDENT_CURRENT('{mTableName}')")
        End If
        Return mValue
    End Function

    'Private Sub UpdateReportConfigTable()
    '    Dim dt As DataTable = GetDataTableFromSQL("select max(lib_ref_id) from CMPDB_tblreport_template_lib")
    '    Dim strLibID As String = dt.Rows(0)(0)
    '    Dim strQry = "update CMPDB_tblsystem_generated_report_processing_configurations set report_template_id=" + strLibID + ",Output_File_Extension='" + System.IO.Path.GetExtension(FileUpload1.FileName).Remove(0, 1) + "' where report_code='" + ddlTemplateType.SelectedItem.Text + "'"
    '    RunSQLQuery(strQry)
    '    MessageBox("Template Uploaded Successfully.")
    'End Sub

    ''Upload File to Database
    ''Public Sub UploadToDB(FileUpload1 As FileUpload)
    ''    Dim filename As String = IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
    ''    Dim contentType As String = FileUpload1.PostedFile.ContentType
    ''    Using fs As IO.Stream = FileUpload1.PostedFile.InputStream
    ''        Using br As New IO.BinaryReader(fs)
    ''            Dim bytes As Byte() = br.ReadBytes(fs.Length)

    ''            Using con As New SqlConnection(strConnectionString)
    ''                Dim query As String = $"Update  {ddlTemplateType.Text} set FileObject=@FileObject,FileName=@FileName,FileUploaded=getdate() Where BLOBFile_ID={gvGrid.SelectedRow.Cells(1).Text}"
    ''                Using cmd As New SqlCommand(query)
    ''                    cmd.Connection = con
    ''                    cmd.Parameters.AddWithValue("@FileName", filename)
    ''                    cmd.Parameters.AddWithValue("@FileObject", bytes)
    ''                    con.Open()
    ''                    cmd.ExecuteNonQuery()
    ''                    con.Close()
    ''                End Using
    ''            End Using
    ''        End Using
    ''    End Using
    ''End Sub

    'Private Function HasTemplate() As Boolean
    '    Dim con As SqlConnection = New SqlConnection(strConnectionString)
    '    Dim cmd As SqlCommand = New SqlCommand("select count(*) from CMPDB_tblreport_template_lib", con)
    '    Dim mTemplateCount As String
    '    con.Open()
    '    mTemplateCount = cmd.ExecuteScalar().ToString()
    '    con.Close()
    '    If mTemplateCount = "0" Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Protected Sub ddlTemplateType_SelectedIndexChanged(sender As Object, e As EventArgs)
        'CreateGrid("")
        'txtSearch.Text = String.Empty
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        '  ddlTemplateType.SelectedIndex = 0
        lblMessage.Text = ""
        'lblReportDesc.Text = ""
    End Sub

    Private Function CreateGrid(key As String) As DataTable
        Dim dt As DataTable
        Try
            dt = GetDataTableFromSQL($"select * from {xtblMasterData} ")
            gvGrid.DataSource = dt
            gvGrid.DataBind()
        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Function

    Protected Sub gvGrid_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        gvGrid.RowStyle.BackColor = Drawing.Color.FromName("#f9f9f9")
        If e.CommandName <> "Select" Then
            Dim bytes As Byte()
            Dim dt = GetDataTableFromSQL($"select * from CMPDB_tblMasterData where BLOBFile_ID= {e.CommandArgument}")
            bytes = DirectCast(dt.Rows(0)("FileObject"), Byte())
            Dim filename = DirectCast(dt.Rows(0)("FileName"), String)
            'Put excel as bytes array into memory stream
            Dim ms As New MemoryStream(bytes)

            DownloadFileFromMemoryStream(ms, filename)
        End If

    End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    '    ' Verifies that the control is rendered
    'End Sub

    Protected Sub gvGrid_SelectedIndexChanged(sender As Object, e As EventArgs)
        'lblReportDesc.Text = $"Upload New File For : {gvGrid.SelectedRow.Cells(2).Text}"
        If btnUploadTemplate.Enabled = False Then
            btnUploadTemplate.Enabled = True
        End If
        FillDropDown(GetSheets(GetFileObject()))
    End Sub

    Private Function GetFileObject()
        Dim dt As DataTable = GetDataTableFromSQL($"Select * from {xtblMasterData} where BLOBFile_ID={gvGrid.SelectedRow.Cells(1).Text}")
        Dim FileObj = dt.Rows(0)("FileObject")
        Return FileObj
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        'CreateGrid(txtSearch.Text)
    End Sub

    Protected Sub btnLoadTables_Click(sender As Object, e As EventArgs)
        'FillDropDown(GetSheets(FileUpload1.FileBytes))
        'SaveToDatabase()
        'CreateGrid("")
    End Sub

    Private Sub SaveToDatabase()
        'Dim params As New List(Of SqlParameter)
        'params.Add(New SqlParameter("BLOBFile", FileUpload1.FileBytes))
        'params.Add(New SqlParameter("FileName", FileUpload1.FileName))
        'ExecuteProcedure("CMPDB_sp_MasterData", params)
    End Sub
End Class