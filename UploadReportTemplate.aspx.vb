Imports System.Data.SqlClient
Imports System.IO


Public Class UploadReportTemplate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()
        If Not IsPostBack Then
            FillDropDown()
            CreateGrid()
        End If
    End Sub

    Private Sub FillDropDown()
        Dim dt As DataTable = GetDataTableFromSQL("select report_code,report_desc from CMPDB_tblsystem_generated_report_processing_configurations")
        ddlTemplateType.DataSource = dt
        ddlTemplateType.DataTextField = "report_code"
        ddlTemplateType.DataValueField = "report_desc"
        ddlTemplateType.DataBind()

    End Sub

    Protected Sub btnUploadTemplate_Click(sender As Object, e As EventArgs) Handles btnUploadTemplate.Click
        Try
            UploadToDB(FileUpload1)
            UpdateReportConfigTable()

        Catch ex As Exception
            Response.Write(ex)
        End Try

    End Sub

    Private Sub UpdateReportConfigTable()
        Dim dt As DataTable = GetDataTableFromSQL("select max(lib_ref_id) from CMPDB_tblreport_template_lib")
        Dim strLibID As String = dt.Rows(0)(0)
        Dim strQry = "update CMPDB_tblsystem_generated_report_processing_configurations set report_template_id=" + strLibID + ",Output_File_Extension='" + System.IO.Path.GetExtension(FileUpload1.FileName).Remove(0, 1) + "' where report_code='" + ddlTemplateType.SelectedItem.Text + "'"
        RunSQLQuery(strQry)
        MessageBox("Template Uploaded Successfully.")
    End Sub

    'Upload File to Database
    Public Sub UploadToDB(FileUpload1 As FileUpload)
        Dim filename As String = IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim contentType As String = FileUpload1.PostedFile.ContentType
        Using fs As IO.Stream = FileUpload1.PostedFile.InputStream
            Using br As New IO.BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(fs.Length)

                Using con As New SqlConnection(strConnectionString)
                    Dim query As String = "insert into CMPDB_tblreport_template_lib(application, file_type, file_data,UpdateDate,CreateDate) values(@application, @file_type, @file_data,@UpdateDate,@CreateDate)"
                    Using cmd As New SqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@application", filename)
                        cmd.Parameters.AddWithValue("@file_type", contentType)
                        cmd.Parameters.AddWithValue("@file_data", bytes)
                        cmd.Parameters.AddWithValue("@UpdateDate", Now)
                        cmd.Parameters.AddWithValue("@CreateDate", Now)
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Function HasTemplate() As Boolean
        Dim con As SqlConnection = New SqlConnection(strConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("select count(*) from CMPDB_tblreport_template_lib", con)
        Dim mTemplateCount As String
        con.Open()
        mTemplateCount = cmd.ExecuteScalar().ToString()
        con.Close()
        If mTemplateCount = "0" Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub ddlTemplateType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTemplateType.SelectedIndexChanged
        If Not ddlTemplateType.SelectedValue = "" Then
            lblReportDesc.Text = "Report Description : " + ddlTemplateType.SelectedValue
        Else
            lblReportDesc.Text = ""
        End If

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ddlTemplateType.SelectedIndex = 0
        lblMessage.Text = ""
        lblReportDesc.Text = ""
    End Sub

    Private Function CreateGrid() As DataTable
        Try
            CreateGrid = GetDataTableFromSQL("select Report_code,r.Lib_ref_id,application,report_desc,UpdateDate,CreateDate from CMPDB_tblreport_template_lib r join CMPDB_tblsystem_generated_report_processing_configurations s on r.lib_ref_id=s.report_template_id")
            gvGrid.DataSource = CreateGrid
            gvGrid.DataBind()
        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Function

    Protected Sub gvGrid_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim bytes As Byte()

        Dim dt = GetDataTableFromSQL("select application, file_data, file_type from CMPDB_tblreport_template_lib where [Lib_ref_id]=" + e.CommandArgument)
        bytes = DirectCast(dt.Rows(0)("file_data"), Byte())
        Dim filename = DirectCast(dt.Rows(0)("application"), String)

        'Put excel as bytes array into memory stream
        Dim ms As New MemoryStream(bytes)

        Response.Clear()
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AppendHeader("Content-Disposition", "filename=" + filename)
        ms.WriteTo(Response.OutputStream)
        Response.End()
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
End Class