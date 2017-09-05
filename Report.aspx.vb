Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports OfficeOpenXml

Public Class Report
    Inherits System.Web.UI.Page


#Region "Variables"

    Dim xTblPlants = "CMPDB_tblPlants"
    Dim xTblPLantsAssoBU = "CMPDB_tblBUPlant"
    Dim xTblDepartment = "CMPDB_tblSite_Departments"
    Dim xTblNameProjects = "CMPDB_tblStartups_New"
    Dim xTblSUL = "CMPDB_tblPractitioner"

#End Region

#Region "Common Section"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdReport.DataBind()
            PopulateDD(ddlPlantList, xTblPlants, "Plant_ID", "Plant")
            GetReportDefaults()
        End If
    End Sub

    Private Sub GetReportDefaults()
        Dim report_code = ""
        If Request.QueryString.HasKeys Then
            report_code = Request.QueryString("s")

        End If

        Dim dtConfiguration As DataTable = GetDataTableFromSQL("select * from CMPDB_tblsystem_generated_report_processing_configurations where report_code='" + report_code + "'")




        'Load DataReader into the DataTable.

        If dtConfiguration.Rows.Count > 0 Then
            '  lblReportHeading.Text = dtConfiguration(0)(1)
            ViewState("storedproc") = dtConfiguration(0)("report_storedproc")
            ViewState("startdatefilter") = dtConfiguration(0)(5)
            ViewState("librefid") = dtConfiguration(0)("report_template_id")
            ' ViewState("useridflag") = dtConfiguration(0)(10)
            ViewState("exceltabname") = dtConfiguration(0)("excel_tab_name")
            ViewState("outputfileextension") = dtConfiguration(0)("output_file_extension")

            'If dtConfiguration(0)(3) = "Y" Then
            '    divcustomer.Attributes.Add("class", "disblock")
            'End If

            'If dtConfiguration(0)(4) = "Y" Then
            '    divcategories.Attributes.Add("class", "disblock")
            'End If

            'If dtConfiguration(0)(5) = "Y" Then
            '    divweeks.Attributes.Add("class", "disblock")
            'End If

            'If dtConfiguration(0)(6) = "Y" Then
            '    divfpc.Attributes.Add("class", "disblock")
            'End If

            'If dtConfiguration(0)(7) = "Y" Then
            '    divupc.Attributes.Add("class", "disblock")
            'End If

            'If dtConfiguration(0)(8) = "Y" Then
            '    divrrfp.Attributes.Add("class", "disblock")
            'End If
        End If


    End Sub
#End Region

    Protected Sub ddlPlantList_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateDD(ddlDepartment, xTblDepartment, "Site_Department_ID", "Site_Department", "Site_ID", ddlPlantList.SelectedItem.Value)
        PopulateDD(ddlProject, xTblNameProjects, "Startup_ID", "Startup_Name", "Plant", ddlPlantList.SelectedValue)
        PopulateDD(ddlSUL, xTblSUL, "Practitioner_ID", "Email", "Plant_ID", ddlPlantList.SelectedValue)
    End Sub

    Protected Sub btnGenrateReport_Click(sender As Object, e As EventArgs)
        CreateGrid()
    End Sub

    Private Function CreateGrid()
        Dim params As New List(Of SqlParameter)
        If ddlPlantList.SelectedValue > 0 Then
            params.Add(New SqlParameter("@plant", ddlPlantList.SelectedValue))
        End If

        If ddlProject.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@project", ddlProject.SelectedValue))
        End If

        If ddlDepartment.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@department", ddlDepartment.SelectedValue))
        End If

        If ddlSUL.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@practitioner", ddlSUL.SelectedValue))
        End If

        If ddlStartUpStatus.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@status", ddlStartUpStatus.SelectedValue))
        End If

        params.Add(New SqlParameter("@gridtype", "A"))
        Dim dt As DataTable = ExecuteProcedureForDataTable(ViewState("storedproc"), params)
        grdReport.DataBind()
        If dt.Rows.Count > 0 Then
            grdReport.DataSource = dt
            grdReport.DataBind()
            Return dt
        Else
        End If
    End Function

    Private Function AppendToExcel(excel As MemoryStream, filename As String, excelTabName As String)
        Dim mSheetName = "Change Masterplan and Dashboard"
        Using pck As New ExcelPackage(excel)
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(excelTabName)
            If ws IsNot Nothing Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8))
                ws.Cells("A4").LoadFromDataTable(CreateGrid, False)

                Dim a = ws.Cells("R3:AI3")
                Dim i = 0
                For Each cell As ExcelRangeBase In a
                    cell.Value = InsertDataForDates()(i).ToString
                    i = i + 1
                Next
                Dim ms = New MemoryStream()


                pck.SaveAs(ms)
                Dim colors = New List(Of Color)
                Dim textToFind = New List(Of String)
                colors.Add(Color.FromArgb(204, 192, 218))
                colors.Add(Color.FromArgb(51, 204, 51))
                colors.Add(Color.FromArgb(255, 0, 255))
                colors.Add(Color.FromArgb(255, 255, 0))
                colors.Add(Color.FromArgb(0, 112, 192))

                textToFind.Add("^1")
                textToFind.Add("^2")
                textToFind.Add("^3")
                textToFind.Add("^4")
                textToFind.Add("^5")
                ms = FindTextInSheet(ms, textToFind, mSheetName, colors)
                '  ms = FindTextInSheet(ms, "2", mSheetName, Color.FromArgb(204, 192, 218))
                'ms = FindTextInSheet(ms, "3", mSheetName, Color.FromArgb(204, 192, 218))
                'ms = FindTextInSheet(ms, "4", mSheetName, Color.FromArgb(204, 192, 218))
                'ms = FindTextInSheet(ms, "5", mSheetName, Color.FromArgb(204, 192, 218))
                Response.Clear()
                ms.WriteTo(Response.OutputStream)
                Response.End()
            Else
                Response.Write("Cannot find the specified sheet.")
            End If
        End Using
    End Function
    Private Function ReadFromDB()

        Dim bytes As Byte()
        Dim fileName As String, contentType As String


        Dim con As New SqlConnection(strConnectionString)

        con.Open()
        Dim mMaxValue As String = ViewState("librefid")
        Using cmd As New SqlCommand()
            cmd.CommandText = "select application, file_data, file_type from CMPDB_tblreport_template_lib where [Lib_ref_id]=@Id"
            cmd.Parameters.AddWithValue("@Id", mMaxValue)
            cmd.Connection = con
            'con.Open();
            Dim adapter As New SqlDataAdapter(cmd)
            Dim DS As New DataSet
            adapter.Fill(DS)

            bytes = DirectCast(DS.Tables(0).Rows(0)("file_data"), Byte())
            contentType = DS.Tables(0).Rows(0)("file_type").ToString()
            fileName = DS.Tables(0).Rows(0)("application").ToString()
            'Put excel as bytes array into memory stream
            Dim ms As New MemoryStream(bytes)
            'Put memory stream into in new workbook object so that we can append data into it
            Return ms

        End Using
    End Function

    Private Function ReadFromDBbytes()

        Dim bytes As Byte()
        Dim fileName As String, contentType As String


        Dim con As New SqlConnection(strConnectionString)

        con.Open()
        Dim mMaxValue As String = ViewState("librefid")
        Using cmd As New SqlCommand()
            cmd.CommandText = "select application, file_data, file_type from CMPDB_tblreport_template_lib where [Lib_ref_id]=@Id"
            cmd.Parameters.AddWithValue("@Id", mMaxValue)
            cmd.Connection = con
            'con.Open();
            Dim adapter As New SqlDataAdapter(cmd)
            Dim DS As New DataSet
            adapter.Fill(DS)

            bytes = DirectCast(DS.Tables(0).Rows(0)("file_data"), Byte())
            contentType = DS.Tables(0).Rows(0)("file_type").ToString()
            fileName = DS.Tables(0).Rows(0)("application").ToString()
            'Put excel as bytes array into memory stream

            'Put memory stream into in new workbook object so that we can append data into it
            Return bytes

        End Using
    End Function

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Try



            Dim strExcelTabName As String = ViewState("exceltabname")
            Dim strOutputFileExtension As String = ViewState("outputfileextension")

            Dim strFileName As String = strExcelTabName + "." + strOutputFileExtension

            AppendToExcel(ReadFromDB(), strFileName, strExcelTabName)

            'AppendToExcel(ReadFromDB(), "xx.xlsx", "Portfolio Raw")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function InsertDataForDates()
        'RunSQLQuery("truncate table temp_report_dates")
        'Dim RangeElements As List(Of String) = ReadCellRangeValueFromWorkSheetName(ReadFromDBbytes, "Change Masterplan and Dashboard", "")
        Dim RangeElements As New List(Of String)
        For i As Int16 = -1 To 16
            Dim temp = Now.AddMonths(i)
            RangeElements.Add(temp.ToString("MMM-yy"))
        Next
        For Each item In RangeElements
            AddUpdateRecordsListControls("temp_report_dates" + ",'" + item + "',I")
        Next
        Return RangeElements
    End Function
End Class