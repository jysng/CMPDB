Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail
Imports ClosedXML.Excel
Imports OfficeOpenXml


Module Functions
    Public xTblNameProduction_Types = "CMPDB_tblProduction_Types"
    Public xTblNameBusiness_Unit = "CMPDB_tblBusiness_Unit"
    Public xTblNamePlatforms = "CMPDB_tblPlatforms"
    Public xTblNameProject_Types = "CMPDB_tblProject_Types"
    Public xTblNameChange_Types = "CMPDB_tblChange_Types"
    Public xTblNameSWP = "CMPDB_tblSWP"
    Public xTblNamePractitioner_Roles = "CMPDB_tblPractitioner_Roles"
    Public xTblNameQualification_Level = "CMPDB_tblQualification_Level"
    Public xTblcmpdb_ComplexityLevel = "CMPDB_tblComplexityLevel"
    Public xTblcmpdb_CorporateSources = "CMPDB_tblCorporateSources"
    Public xtblSWPToolName = "CMPDB_tblSWP_Tool_Names"
    Public xTblBLOBFiles = "CMPDB_tblBLOBFiles"
    Public xTblRegion = "CMPDB_tblRegion"
    Public xTblQualification = "CMPDB_tblQualification_Level"
    Public xTblPlants = "CMPDB_tblPlants"
    Public xTblPLantsAssoBU = "CMPDB_tblBUPlant"

    'Public Const strConnectionString As String = "Data Source=BDC-SQLP044\DRNAP4415;Initial Catalog=MSO_Forecast;Persist Security Info=True;User ID=FCS_Admin;Password=t0day@123"
    'Public Const strConnectionString As String = "Data Source=sitecoresql;Initial Catalog=MSO_Forecast;Persist Security Info=True;User ID=FCS_User;Password=t0day@123"
    Public strConnectionString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    'Public strConnectionString As String = ConfigurationManager.ConnectionStrings("csMSO_Forecast").ConnectionString
    Public pRecordsToProcess As Int16 = Convert.ToInt16(ConfigurationManager.AppSettings.Item("pRecordsToProcess"))
    Public Const mApplication As String = "1.0"

    Public Enum SWPToolName As Integer
        IAP
        SPR
        MPC
        MROA
        MRMD
        PRA
        EWP
        RIP
        PRRA
    End Enum

    Public Sub ResetControls(ByVal ParamArray txt As LinkButton())
        For Each t As LinkButton In txt
            t.Text = String.Empty
        Next
    End Sub

    Public Sub ResetControls(ByVal ParamArray txt As CheckBox())
        For Each t As CheckBox In txt
            t.Checked = False
        Next
    End Sub

    Public Function GetUserName() As String
        Dim uid As String
        Dim i As Long

        uid = HttpContext.Current.User.Identity.Name


        i = InStr(1, uid, "\", CompareMethod.Text)

        GetUserName = Right(uid, Len(uid) - i)


    End Function

    Public Function IsMaintenanceModeOn()
        If GetParamsValue("pDatabaseVersion") <> mApplication Or GetParamsValue("pMaintenanceModeFlag") = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function AthuUersByUserID() As Boolean
        If HttpContext.Current.Session("User_ID").ToString = "dlentz" Or HttpContext.Current.Session("User_ID").ToString = "lentz.d" Or HttpContext.Current.Session("User_ID").ToString = "thomas.ds" Then
            'If GetUserRole(HttpContext.Current.Session("User_ID").ToString) = "1" Or GetUserRole(HttpContext.Current.Session("User_ID").ToString) = "2" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function AthuUersByRoleID() As Boolean
        ' If HttpContext.Current.Session("User_ID").ToString = "dlentz" Or HttpContext.Current.Session("User_ID").ToString = "lentz.d" Or HttpContext.Current.Session("User_ID").ToString = "thomas.ds" Then
        'If GetUserRole(HttpContext.Current.Session("User_ID").ToString) = "1" Or GetUserRole(HttpContext.Current.Session("User_ID").ToString) = "2" Then
        If HttpContext.Current.Session("User_ID") IsNot Nothing Then
            If GetUserRole(HttpContext.Current.Session("User_ID").ToString) = "1" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
            HttpContext.Current.Response.Redirect("Default.aspx")

        End If
    End Function

    Function GetUserRole(ByVal UserID As String) As Long

        Dim cn As SqlConnection
        Dim cmd As SqlCommand
        Dim sql As String
        cn = New SqlConnection(strConnectionString)

        sql = "SELECT a.role_id,b.role_desc " +
                "FROM cmpdb_tblusers a, CMPDB_tblroles b " +
                "where(a.role_id = b.role_id) " +
                "and user_id = '" + UserID + "';"

        GetUserRole = 0

        Try
            cn.Open()
            cmd = New SqlCommand(sql, cn)

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()
            While sqlReader.Read()
                GetUserRole = sqlReader.Item("role_id")
            End While

            sqlReader.Close()
            cmd.Dispose()
            cn.Close()

        Catch ex As Exception

            GetUserRole = 0

        End Try


    End Function

    Function GetUserRoleDesc(ByVal UserID As String) As String

        Dim cn As SqlConnection
        Dim cmd As SqlCommand
        Dim sql As String
        cn = New SqlConnection(strConnectionString)

        sql = "SELECT a.role_id,b.role_desc " +
                "FROM CMPDB_tblUsers a, CMPDB_tblroles b " +
                "where(a.role_id = b.role_id) " +
                "and user_id = '" + UserID + "';"

        GetUserRoleDesc = ""

        Try
            cn.Open()
            cmd = New SqlCommand(sql, cn)

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()
            While sqlReader.Read()
                GetUserRoleDesc = sqlReader.Item("role_desc")
            End While

            sqlReader.Close()
            cmd.Dispose()
            cn.Close()

        Catch ex As Exception

            GetUserRoleDesc = ""

        End Try


    End Function

    Public Function GetFullName(ByVal UserID As String) As String
        Dim cn As SqlConnection
        Dim cmd As SqlCommand
        Dim sql As String
        cn = New SqlConnection(strConnectionString)

        sql = "SELECT firstname, lastname FROM CMPDB_tblUsers WHERE user_id = '" & UserID + "';"

        GetFullName = "Unknown"

        Try
            cn.Open()
            cmd = New SqlCommand(sql, cn)

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()
            While sqlReader.Read()
                GetFullName = sqlReader.Item("firstname").ToString() & " " & sqlReader.Item("lastname").ToString()
            End While

            sqlReader.Close()
            cmd.Dispose()
            cn.Close()

        Catch ex As Exception

            MsgBox("Can not open connection!")

        End Try
    End Function

    Public Function RunSQLQuery(query As String)
        Dim con As New SqlConnection(strConnectionString)
        Dim ret = String.Empty
        Try

            Dim cmd = New SqlCommand()
            cmd.Connection = con
            cmd.CommandText = query
            cmd.CommandTimeout = 1500
            con.Open()
            ret = cmd.ExecuteNonQuery().ToString()
            Return ret

        Catch exp As Exception

            ret = exp.Message
            Return ret
        Finally
            con.Close()
        End Try
    End Function

    Public Sub ExecuteProc(mStoredProcName As String)

        Dim con As SqlConnection = New SqlConnection(strConnectionString)
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        cmd.CommandText = "exec " + mStoredProcName
        cmd.CommandTimeout = 1500
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    Public Sub UpdateParamsTable(mParamName As String, mParamValue As String)
        Dim str = "UPDATE [dbo].[system_parameters] " +
                "SET [ParameterValue] = '" + mParamValue + "' " +
                "WHERE [ParameterName]='" + mParamName + "';"
        RunSQLQuery(str)
    End Sub

    Public Function GetParamsValue(mParamName As String)
        Return GetSingleValue("select ParameterValue from system_parameters where ParameterName='" + mParamName + "'")
    End Function

    Public Function GetSingleValue(mQry As String)
        Dim con As New SqlConnection(strConnectionString)
        Dim ret = String.Empty
        Try
            Dim cmd = New SqlCommand()
            cmd.Connection = con
            cmd.CommandText = mQry
            cmd.CommandTimeout = 1500
            con.Open()
            ret = cmd.ExecuteScalar().ToString()
            Return ret
        Catch exp As Exception
            ret = exp.Message
            Return Nothing
        Finally
            con.Close()
        End Try
    End Function

    Public Function AddQuotesToDelimitedString(str As String)
        Dim strFinal = ""
        If str.IndexOf(";") <> 0 Then
            For Each str1 As String In str.Split(";")
                str1 = Chr(39) + str1 + Chr(39)
                strFinal += str1 + ","
            Next
            AddQuotesToDelimitedString = strFinal.TrimEnd(",")
        Else
            AddQuotesToDelimitedString = Chr(39) + str + Chr(39)
        End If

    End Function

    Public Function GetDataTableFromSQL(mqry As String)
        Dim con = New SqlConnection(strConnectionString)
        Dim cmd = New SqlCommand(mqry, con)
        Dim da = New SqlDataAdapter(cmd)
        Dim dt = New DataTable
        da.Fill(dt)
        Return dt
    End Function

    Public Sub SendEmail(ToEmailID As String, CCEmailID As String, Subject As String, Body As String)

        Dim smtp_server As New SmtpClient
        Dim e_mail As New MailMessage
        smtp_server.UseDefaultCredentials = Convert.ToBoolean(GetParamsValue("pUseDefaultCredentials"))
        smtp_server.Port = GetParamsValue("pPortNumber")
        'smtp_server.Host = "smtpgw.pg.com"
        'e_mail.From = New MailAddress("msoforecast.im@pg.com")
        smtp_server.Host = GetParamsValue("pHostName")
        e_mail.From = New MailAddress(GetParamsValue("pFromEmail"))

        If CCEmailID <> "" Then
            For Each a In CCEmailID.Split(New Char() {";"})
                e_mail.CC.Add(a)
            Next
        End If

        For Each a In ToEmailID.Split(New Char() {";"})
            e_mail.To.Add(a)
        Next

        e_mail.Subject = Subject
        e_mail.IsBodyHtml = Convert.ToBoolean(GetParamsValue("pIsHTMLBody"))
        e_mail.Body = Body
        smtp_server.Send(e_mail)
    End Sub

    'Public Function AppendToExcel(excel As MemoryStream, mDataTable As DataTable, excelTabName As String)
    '    Using pck As New ExcelPackage(excel)
    '        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(excelTabName)
    '        If ws IsNot Nothing Then

    '            ws.Cells("A2").LoadFromDataTable(mDataTable, False)
    '            Dim ms = New System.IO.MemoryStream()
    '            pck.SaveAs(ms)
    '            Return ms

    '        Else
    '            HttpContext.Current.Response.Write("Cannot find the specified sheet.")
    '        End If
    '    End Using
    'End Function

    Public Sub DownloadFileFromMemoryStream(excel As MemoryStream, filename As String)
        If excel IsNot Nothing Then
            If filename.Split(".")(1) = "xlsx" Then
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Else
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel.sheet.macroEnabled.12"
            End If
            HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8))
            HttpContext.Current.Response.Clear()
            excel.WriteTo(HttpContext.Current.Response.OutputStream)
            HttpContext.Current.Response.End()
        End If
    End Sub

    Public Class TemplateData

        Public mExcelTabName As String
        Public mExcelTabName2 As String
        Public mFileExtention As String
        Public mExcelFile As MemoryStream
        Public Sub New(_mExcelTabName As String,
           _mFileExtention As String, _mExcelFile As MemoryStream)
            mExcelTabName = _mExcelTabName
            mFileExtention = _mFileExtention
            mExcelFile = _mExcelFile
        End Sub

        Public Sub New(_mExcelTabName As String,
          _mFileExtention As String, _mExcelTabName2 As String, _mExcelFile As MemoryStream)
            mExcelTabName = _mExcelTabName
            mExcelTabName2 = _mExcelTabName2
            mFileExtention = _mFileExtention
            mExcelFile = _mExcelFile
        End Sub
    End Class

    Public Sub PopulateDD(dd As ListControl, TableName As String, ID As String, Value As String)
        Try
            Dim dt = GetDataTableFromSQL("exec CMPDB_sp_FillWithIDnText " + TableName + "," + ID + "," + Value)
            BindDDL(dd, dt)

        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Public Sub PopulateDD(dd As ListControl, TableName As String, ID As String, Value As String, WhereColName As String, WhereValue As String)
        Try
            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@TableName", TableName))
            params.Add(New SqlParameter("@Key", ID))
            params.Add(New SqlParameter("@Value", Value))
            params.Add(New SqlParameter("@WhereCol", WhereColName))
            params.Add(New SqlParameter("@WhereValue", WhereValue))
            Dim dt = ExecuteProcedureForDataTable("sp_FillWithIDnTextFromDependent", params)
            BindDDL(dd, dt)
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Public Sub PopulateDDWithSQL(dd As ListControl, str As String)
        Try
            Dim dt As DataTable = GetDataTableFromSQL(str)
            BindDDL(dd, dt)
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Public Sub PopulateDDFromDataTable(ByRef dd As ListControl, ByRef dt As DataTable)
        Try
            BindDDL(dd, dt)
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Private Sub BindDDL(dd As ListControl, dt As DataTable)
        If dd.GetType.Name = "DropDownList" Then
            Dim firstitem = dd.Items(0)
            dd.Items.Clear()
            dd.Items.Add(firstitem)
        End If

        dd.DataSource = dt
        dd.DataTextField = dt.Columns(0).ColumnName
        dd.DataValueField = dt.Columns(1).ColumnName
        If dt.Rows.Count <= 0 Then
            dt = Nothing
        End If
        dd.DataBind()
    End Sub

    Public Sub AddUpdateRecordsListControls(str As String)
        Try
            ExecuteProc("CMPDB_sp_InsertNUpdateSingleValue " + str)
            MessageBox("Records Successfully Inserted/Updated !")
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Public Sub AddUpdateRecordsDependentListControls(str As String)
        Try
            ExecuteProc("CMPDB_sp_InsertNUpdateMultiple " + str)
            MessageBox("Records Successfully Inserted/Updated !")
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Public Sub DeleteRecordsListControls(str As String)
        Try
            ExecuteProc("CMPDB_sp_DeleteDropdownValues " + str)
            MessageBox("Successfully Deleted !!")
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Public Sub ResetControls(ByVal ParamArray txt As TextBox())
        For Each t As TextBox In txt
            t.Text = String.Empty
        Next
    End Sub

    Public Sub ResetControls(ByVal ParamArray txt As Label())
        For Each t As Label In txt
            t.Text = String.Empty
        Next
    End Sub

    Public Sub ResetControls(ByVal ParamArray txt As ListControl())
        For Each t As ListControl In txt
            t.SelectedIndex = -1
        Next
    End Sub

    Public Sub ExecuteProcedure(spName As String, spParams As List(Of SqlParameter))
        Dim dbCmd As SqlCommand = New SqlCommand()

        Dim con As New SqlConnection(strConnectionString)
        con.Open()
        dbCmd.Connection = con
        dbCmd.CommandText = spName
        dbCmd.CommandType = CommandType.StoredProcedure
        dbCmd.CommandTimeout = 6000
        Dim sqlDa As SqlDataAdapter = New SqlDataAdapter(dbCmd)
        Try
            'If dbConn.State <> ConnectionState.Open Then
            '    dbConn.Open()
            'End If
            For Each param As SqlParameter In spParams
                dbCmd.Parameters.Add(param)
            Next
            Dim dsQuery As New DataSet()
            dbCmd.ExecuteNonQuery()
            'sqlDa.Fill(dsQuery)
            'Return dsQuery
        Finally
            'dbCmd.Connection.Close()
            dbCmd.Dispose()
        End Try
    End Sub

    Public Function ExecuteProcedureForDataTable(spName As String, spParams As List(Of SqlParameter))
        Dim dbCmd As SqlCommand = New SqlCommand()
        Dim con As New SqlConnection(strConnectionString)
        con.Open()
        dbCmd.Connection = con
        dbCmd.CommandText = spName
        dbCmd.CommandType = CommandType.StoredProcedure
        dbCmd.CommandTimeout = 6000
        Dim sqlDa As SqlDataAdapter = New SqlDataAdapter(dbCmd)
        Try
            For Each param As SqlParameter In spParams
                dbCmd.Parameters.Add(param)
            Next
            Dim dsQuery As New DataSet()
            sqlDa.Fill(dsQuery)
            Return dsQuery.Tables(0)
        Finally
            dbCmd.Connection.Close()
            dbCmd.Dispose()
            spParams = Nothing
        End Try
    End Function

    Public Function UploadFileToServer(pathOnServer As String, ParamArray fu As FileUpload()) As Boolean
        Try
            For Each file In fu
                If file.HasFile Then
                    Dim filename As String = Path.GetFileName(file.FileName)
                    file.SaveAs(HttpContext.Current.Request.MapPath("~/") + filename)
                    'StatusLabel.Text = "Upload status: File uploaded!";
                    'StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub download(ByVal bytes As Byte(), mfileName As String)
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & mfileName)
        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding(1252)

        HttpContext.Current.Response.BinaryWrite(bytes)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Sub

    Public Function ShowHideControls(var As Boolean, ParamArray fu As WebControl()) As Boolean
        Try
            For Each con In fu
                con.Style.Add("display", If(var, "block", "none"))
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ShowHideControls(var As String, ParamArray fu As HtmlControl()) As Boolean
        Try
            For Each con In fu
                con.Style.Add("display", var)
                con.Style.Add("display", var)
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Excel Library"

    <System.Runtime.CompilerServices.Extension>
    Public Function ToDataTable(package As ExcelPackage) As DataTable
        Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets.First()
        Dim table As New DataTable()
        For Each firstRowCell In workSheet.Cells(1, 1, 1, workSheet.Dimension.[End].Column)
            table.Columns.Add(firstRowCell.Text)
        Next
        For rowNumber = 2 To workSheet.Dimension.[End].Row
            Dim row = workSheet.Cells(rowNumber, 1, rowNumber, workSheet.Dimension.[End].Column)
            Dim newRow = table.NewRow()
            For Each cell In row
                newRow(cell.Start.Column - 1) = cell.Text
            Next
            table.Rows.Add(newRow)
        Next
        Return table
    End Function

    <System.Runtime.CompilerServices.Extension>
    Public Function ToDataTable(package As ExcelPackage, mSheetName As String) As DataTable
        Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets(mSheetName)
        Dim table As New DataTable()
        For Each firstRowCell In workSheet.Cells(1, 1, 1, workSheet.Dimension.[End].Column)
            table.Columns.Add(firstRowCell.Text)
        Next
        For rowNumber = 2 To workSheet.Dimension.[End].Row
            Dim row = workSheet.Cells(rowNumber, 1, rowNumber, workSheet.Dimension.[End].Column)
            Dim newRow = table.NewRow()
            For Each cell In row
                newRow(cell.Start.Column - 1) = cell.Text
            Next
            table.Rows.Add(newRow)
        Next
        Return table
    End Function
    Public Function FromExcelToTable(mTable As String, fu As FileUpload)
        Try
            If fu.HasFile Then
                If Path.GetExtension(fu.FileName).ToLower.Equals(".xlsx") Or Path.GetExtension(fu.FileName).ToLower.Equals(".xlsm") Then
                    Dim excel = New ExcelPackage(fu.FileContent)

                    Dim dt = excel.ToDataTable()
                    Dim table = mTable
                    Using conn = New SqlConnection(strConnectionString)
                        Dim bulkCopy = New SqlBulkCopy(conn)
                        bulkCopy.BulkCopyTimeout = 1500
                        bulkCopy.BatchSize = 1000

                        bulkCopy.DestinationTableName = table
                        conn.Open()
                        Dim schema = conn.GetSchema("Columns", {Nothing, Nothing, table, Nothing})
                        For Each sourceColumn As DataColumn In dt.Columns
                            For Each row As DataRow In schema.Rows
                                If String.Equals(sourceColumn.ColumnName, DirectCast(row("COLUMN_NAME"), String), StringComparison.OrdinalIgnoreCase) Then
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, DirectCast(row("COLUMN_NAME"), String).Replace("'", ""))
                                    Exit For
                                End If
                            Next
                        Next
                        bulkCopy.WriteToServer(dt)
                    End Using
                End If
            End If
            Return "Done"
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    ''' <summary>
    ''' From Bytes to Table with a table name and Byte array Object
    ''' </summary>
    ''' <param name="mTable">Table Name</param>
    ''' <param name="fu">Byte Array</param>
    ''' <returns></returns>
    Public Function FromExcelToTable(mTable As String, fu As Byte(), TruncateTable As Boolean)
        Try
            If TruncateTable Then
                RunSQLQuery("truncate table " + mTable)
            End If
            'If fu.HasFile Then

            '    If Path.GetExtension(fu.FileName).ToLower.Equals(".xlsx") Or Path.GetExtension(fu.FileName).ToLower.Equals(".xlsm") Then
            Dim ms As New MemoryStream(fu)
            Dim excel As New ExcelPackage(ms)
            Dim dt = excel.ToDataTable(mTable)
            Dim table = mTable
            Using conn = New SqlConnection(strConnectionString)
                Dim bulkCopy = New SqlBulkCopy(conn)
                bulkCopy.BulkCopyTimeout = 1500
                bulkCopy.BatchSize = 1000

                bulkCopy.DestinationTableName = table
                conn.Open()
                Dim schema = conn.GetSchema("Columns", {Nothing, Nothing, table, Nothing})
                For Each sourceColumn As DataColumn In dt.Columns
                    For Each row As DataRow In schema.Rows
                        If String.Equals(sourceColumn.ColumnName, DirectCast(row("COLUMN_NAME"), String), StringComparison.OrdinalIgnoreCase) Then
                            bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, DirectCast(row("COLUMN_NAME"), String).Replace("'", ""))
                            Exit For
                        End If
                    Next
                Next
                bulkCopy.WriteToServer(dt)
            End Using
            '    End If
            'End If
            Return "Done"
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function CreateExcelFromDataTable(table As DataTable, fileName As String)
        Using wb As New XLWorkbook()
            wb.Worksheets.Add(table, "Sheet1")

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream)
                HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.End()
            End Using
        End Using
    End Function

    Public Function AppendToExcel(excel As MemoryStream, mDataTable As DataTable, excelTabName As String)
        Using pck As New ExcelPackage(excel)
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(excelTabName)
            If ws IsNot Nothing Then
                ws.Cells("A2").LoadFromDataTable(mDataTable, False)
                Dim ms = New System.IO.MemoryStream()
                pck.SaveAs(ms)
                Return ms
            Else
                Return Nothing
                HttpContext.Current.Response.Write("Cannot find the specified sheet.")
            End If
        End Using
    End Function

    Public Function ReadCellValueFromWorkSheetName(excel As Byte(), excelTabName As String, excelCellAddress As String)
        Dim buffer() As Byte = excel
        Dim excelStream As New MemoryStream()
        excelStream.Write(buffer, 0, buffer.Length)
        Using pck As New ExcelPackage(excelStream)
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(excelTabName)
            If ws IsNot Nothing Then
                Dim val = ws.Cells(excelCellAddress).Text

                Return val
            Else
                Return Nothing
                HttpContext.Current.Response.Write("Cannot find the specified sheet.")
            End If
        End Using
    End Function

    Public Function ReadCellRangeValueFromWorkSheetName(excel As Byte(), excelTabName As String, excelCellAddress As String)
        Dim buffer() As Byte = excel
        Dim RangeElements As New List(Of String)
        Dim excelStream As New MemoryStream()
        excelStream.Write(buffer, 0, buffer.Length)
        Using pck As New ExcelPackage(excelStream)
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(excelTabName)
            If ws IsNot Nothing Then
                Dim a As String = ""
                Dim val As ExcelRange = ws.Cells(3, 18, 3, 35)
                For Each ran In val
                    RangeElements.Add(ran.Text)
                Next
                Return RangeElements
            Else
                Return Nothing
                HttpContext.Current.Response.Write("Cannot find the specified sheet.")
            End If
        End Using
    End Function

    Public Function WriteToExcelCell(excel As Byte(), cellContent As String, cellExcelTabName As String, contentAddress As String)
        Dim consh As ExcelWorksheet
        Dim excelStream As New MemoryStream()
        excelStream.Write(excel, 0, excel.Length)
        Dim exlpck As New ExcelPackage(excelStream)
        If exlpck.Workbook.Worksheets(cellExcelTabName) Is Nothing Then
            consh = exlpck.Workbook.Worksheets.Add(cellExcelTabName)
        Else
            consh = exlpck.Workbook.Worksheets(cellExcelTabName)
        End If
        consh.Cells(contentAddress).Value = cellContent
        exlpck.Save()
        Dim s = New MemoryStream(exlpck.GetAsByteArray())
        Return s
    End Function



    Public Function WriteToExcelCell1(excel As Byte(), cellContent As String, cellExcelTabName As String, contentAddress As String)
        Dim consh As ExcelWorksheet
        Dim excelStream As New MemoryStream()
        excelStream.Write(excel, 0, excel.Length)
        Dim exlpck As New ExcelPackage(excelStream)
        If exlpck.Workbook.Worksheets(cellExcelTabName) Is Nothing Then
            consh = exlpck.Workbook.Worksheets.Add(cellExcelTabName)
        Else
            consh = exlpck.Workbook.Worksheets(cellExcelTabName)
        End If
        consh.Cells(contentAddress).Value = cellContent
        exlpck.Save()
        Return exlpck.GetAsByteArray()

    End Function

    Public Function ReadCellValueFromWorkSheetNameAndAddContentToCell(excel As Byte(), dataAppendSheet As String, dataAppendCellAddress As String, excelTabName As String, excelCellAddress As String, cellContent As String)
        ' excelCellAddress = ""
        Dim buffer() As Byte = excel
        Dim lst As New List(Of Object)
        Dim excelStream As New MemoryStream()
        excelStream.Write(buffer, 0, buffer.Length)
        Using pck As New ExcelPackage(excelStream)
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(excelTabName)

            If ws IsNot Nothing Then
                Dim val = ws.Cells(excelCellAddress).Text
                lst.Add(val)
                'Write to connect sheet
                lst.Add(WriteToExcelCell(excel, cellContent, dataAppendSheet, dataAppendCellAddress))
                Return lst
            Else
                Return Nothing
                HttpContext.Current.Response.Write("Cannot find the specified sheet.")
            End If
        End Using
    End Function



    ''' <summary>
    ''' Get total sheets 
    ''' </summary>
    ''' <param name="excel"></param>
    ''' <returns></returns>
    Public Function GetSheets(excel As Byte())
        Dim ms As New MemoryStream(excel)
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Value")
        Using pck As New ExcelPackage(ms)
            For Each ws As ExcelWorksheet In pck.Workbook.Worksheets
                dt.Rows.Add(ws.Name, ws.Index.ToString)
            Next
        End Using
        dt.AcceptChanges()
        Return dt
    End Function

#End Region


    Public Function ExecuteProcedure(spName As String, spParams As List(Of SqlParameter), ByRef out As SqlParameter)
        Dim dbCmd As SqlCommand = New SqlCommand()

        Dim con As New SqlConnection(strConnectionString)
        con.Open()
        dbCmd.Connection = con
        dbCmd.CommandText = spName
        dbCmd.CommandType = CommandType.StoredProcedure
        dbCmd.CommandTimeout = 6000
        out.Direction = ParameterDirection.Output
        Dim sqlDa As SqlDataAdapter = New SqlDataAdapter(dbCmd)
        Try
            'If dbConn.State <> ConnectionState.Open Then
            '    dbConn.Open()
            'End If
            For Each param As SqlParameter In spParams
                dbCmd.Parameters.Add(param)
            Next
            dbCmd.Parameters.Add(out)
            Dim dsQuery As New DataSet()
            dbCmd.ExecuteNonQuery()
            Return out.Value

            'sqlDa.Fill(dsQuery)
            'Return dsQuery
        Finally
            'dbCmd.Connection.Close()
            dbCmd.Dispose()
        End Try
    End Function

    Public Sub ResetControls(ByVal ParamArray chk As CheckBoxList())
        For Each t As CheckBoxList In chk
            t.ClearSelection()
        Next
    End Sub

    Public Class EditMeasuresData

        Public mtxtBox As TextBox
        Public mDateLable As Label
        Public mlnkbtnFileName As LinkButton
        Public mlblFileName As Label
        Public mWorkSheetName As TextBox
        Public mCellAddress As TextBox
        Public Sub New(_mtxtBox As TextBox, _mDateLable As Label, _mlnkbtnFileName As LinkButton, _mlblFileName As Label, _mWorkSheetName As TextBox, _mCellAddress As TextBox)
            mtxtBox = _mtxtBox
            mDateLable = _mDateLable
            mlnkbtnFileName = _mlnkbtnFileName
            mlblFileName = _mlblFileName
            mWorkSheetName = _mWorkSheetName
            mCellAddress = _mCellAddress
        End Sub
    End Class

End Module
