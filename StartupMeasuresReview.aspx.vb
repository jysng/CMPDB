Public Class StartupMeasuresReview
    Inherits System.Web.UI.Page

#Region "Common"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()

        If Not IsPostBack Then
            PopulateDD(ddlSearchPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")
            'ShowGridHeader()
            LoadReportControls("CPS_WBW")
        End If
    End Sub

    Private Sub LoadReportControls(report_code As String)
        Dim dtConfiguration As DataTable = GetDataTableFromSQL($"select * from [CMPDB_tblsystem_generated_report_processing_configurations] where report_code='{report_code}'")

        If dtConfiguration.Rows.Count > 0 Then
            Dim config = dtConfiguration.Rows(0)
            lblReportName.Text = config("report_desc")
            ViewState("storedproc") = config("report_storedproc")
            ViewState("plant_filter_flag") = config("plant_filter_flag")
            ViewState("department_filter_flag") = config("department_filter_flag")
            ViewState("SUL_filter_flag") = config("SUL_filter_flag")
            ViewState("project_manager_flag") = config("project_manager_flag")
            ViewState("project_status_flag") = config("project_status_flag")

            If config("plant_filter_flag") = True Then
                dvPlant.Attributes.Add("class", "disblock")
            End If

            If config("department_filter_flag") = True Then
                dvDepartment.Attributes.Add("class", "disblock")
            End If

            If config("SUL_filter_flag") = True Then
                dvSUL.Attributes.Add("class", "disblock")
            End If

            If config("project_manager_flag") = True Then
                dvProjectManager.Attributes.Add("class", "disblock")
            End If

            If config("project_status_flag") = True Then
                dvProjectStatus.Attributes.Add("class", "disblock")
            End If

        End If
    End Sub
#End Region

#Region "Populates"

#End Region
#Region "Variables"
#End Region


    Private Sub LoadAllDependents()
        PopulatePractitioner(ddlSearchPlants.SelectedItem.Value)
    End Sub
    Private Sub PopulatePractitioner(key As String)
        PopulateDD(ddlSearchSUL, "CMPDB_tblPractitioner", "Practitioner_ID", "Email", "Plant_ID", key)
    End Sub
    Protected Sub ddlSearchPlants_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadAllDependents()
    End Sub
    Private Sub ShowGridHeader()
        gridStartupMeasuresReview.DataSource = New List(Of String)
        gridStartupMeasuresReview.DataBind()
        gridStartupMeasuresReview.ShowHeaderWhenEmpty = True
    End Sub
    Protected Sub btnSerach_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = GetDataTableFromSQL("exec " + ViewState("storedproc"))
        gridStartupMeasuresReview.DataSource = dt
        gridStartupMeasuresReview.DataBind()

        'If divSrchExist.Attributes("style") = "display:none;" Then
        '    divSrchExist.Attributes.Add("style", "display:block;")
        'ElseIf divSrchExist.Attributes("style") = "display:block;" Then
        '    divSrchExist.Attributes.Add("style", "display:none;")
        'End If
    End Sub

    Protected Sub chkAdvancedMode_CheckedChanged(sender As Object, e As EventArgs)
        If chkAdvancedMode.Checked = True Then
            divhide.Style.Add("display", "block")
        Else
            divhide.Style.Add("display", "none")
        End If
    End Sub

    Protected Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadReportControls(ddlReportType.SelectedValue)
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = GetDataTableFromSQL("exec " + ViewState("storedproc"))

        Dim t As TemplateData = ReadFromDB(ddlReportType.SelectedValue)
        Dim ms = AppendToExcel(t.mExcelFile, dt, t.mExcelTabName)
        Dim mFileName = $"Reports({ddlReportType.SelectedItem.Text}({Now.ToString("dd-MM-yyyy")})).xlsx"
        DownloadFileFromMemoryStream(ms, mFileName)
    End Sub

    Protected Sub btnAddReportTemplate_Click(sender As Object, e As EventArgs)
        Response.Redirect("UploadReportTemplate.aspx")
    End Sub
End Class