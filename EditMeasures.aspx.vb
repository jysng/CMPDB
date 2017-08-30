Imports System.Data.SqlClient

Public Class EditMeasures
    Inherits System.Web.UI.Page
#Region "Common"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()

        If Not Me.IsPostBack Then
            SetTools()
            PopulateDD(ddlPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
            gridProjects.DataSource = New List(Of String)
            gridProjects.DataBind()
            Session("SortTable") = vbNull
            gridProjects.ShowHeaderWhenEmpty = True

            If Request.QueryString("StartupId") <> "" Then
                FillData(Request.QueryString("StartupId").ToString())
            End If
        End If
    End Sub

    Private Sub SetTools()
        Dim dt = New DataTable
        dt = GetDataTableFromSQL("select top(8) swp_tool_name name from CMPDB_tblSWP_Tool_Names order by SWP_Tool_Name_ID")
        If dt.Rows.Count > 0 Then
            lblFile1.Text = dt.Rows(0)("name").ToString()
            lblFile2.Text = dt.Rows(1)("name").ToString()
            lblFile3.Text = dt.Rows(2)("name").ToString()
            lblFile4.Text = dt.Rows(3)("name").ToString()
            lblFile5.Text = dt.Rows(4)("name").ToString()
            lblFile6.Text = dt.Rows(5)("name").ToString()
            lblFile7.Text = dt.Rows(6)("name").ToString()
            lblFile8.Text = dt.Rows(7)("name").ToString()
        End If
    End Sub
#End Region
#Region "Populates"
    Private Sub LoadAllDependents()
        populatePractitioner(ddlPlant.SelectedValue)
    End Sub
    Private Sub populatePractitioner(key As String)
        PopulateDD(ddlSUL, "CMPDB_tblPractitioner", "Practitioner_ID", "Email", "Plant_ID", key)
    End Sub

    Protected Sub ddlPlant_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadAllDependents()
    End Sub
#End Region
#Region "Variables"

#End Region
#Region "Search Projects Section"
    Protected Sub btnRefineSearch_Click(sender As Object, e As EventArgs)
        SubMeasures()
    End Sub
    Public Sub SubMeasures()
        Dim dt = New DataTable
        Dim params As New List(Of SqlParameter)
        Dim check = 0

        If ddlPlant.SelectedValue > 0 Then
            params.Add(New SqlParameter("@plant", ddlPlant.SelectedValue))
            check = 1

        Else
            params.Add(New SqlParameter("@plant", DBNull.Value))

        End If
        If ddlSUL.SelectedValue > 0 Then

            params.Add(New SqlParameter("@practitioner", ddlSUL.SelectedValue))
            check = 1
        Else
            params.Add(New SqlParameter("@practitioner", DBNull.Value))
        End If

        If ddlProjectStatus.SelectedValue > 0 Then

            params.Add(New SqlParameter("@status", ddlProjectStatus.SelectedValue))
            check = 1
        Else
            params.Add(New SqlParameter("@status", DBNull.Value))
        End If

        If txtLastUpdate.Text <> "" Then
            params.Add(New SqlParameter("@update", txtLastUpdate.Text))
            check = 1
        Else
            params.Add(New SqlParameter("@update", DBNull.Value))
        End If
        params.Add(New SqlParameter("@gridtype", "E"))

        'If check = 1 Then

        'Else
        '    params.Add(New SqlParameter("@typegrid", "A"))
        'End If

        dt = ExecuteProcedureForDataTable("CMPDB_sp_GetSearchProjectAdminnew", params)
        If dt.Rows.Count > 0 Then
            Session("SortTable") = dt
            gridProjects.DataSource = Session("SortTable")
            gridProjects.DataBind()
        Else

            gridProjects.DataSource = New List(Of String)
            gridProjects.DataBind()
            Session("SortTable") = vbNull
            gridProjects.ShowHeaderWhenEmpty = True
        End If
    End Sub
#End Region
#Region "In Process Measures Section"

    Private Property MeasuresEditid() As Integer
        Get
            Return CInt(ViewState("Startup_ID"))
        End Get
        Set
            ViewState("Startup_ID") = Value
        End Set
    End Property

    Protected Sub DownloadFile_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = DirectCast(sender, LinkButton)
        Dim Fileid As [String] = btn.CommandArgument

        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@Startup_ID", ""))
        params.Add(New SqlParameter("@startupsBLOBFilesID", Fileid))
        params.Add(New SqlParameter("@Type", "D"))
        Dim dtMeasures As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_GetProcessMeasures", params)

        If dtMeasures.Rows.Count > 0 Then
            Dim bytes() As Byte = CType(dtMeasures.Rows(0)("FileObject"), Byte())
            download(bytes, dtMeasures.Rows(0)("filename").ToString())
        End If
    End Sub
    Protected Sub gridProjects_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)

        'Retrieve the table from the session object.
        Dim dt = TryCast(Session("SortTable"), DataTable)

        If dt IsNot Nothing Then

            'Sort the data.
            dt.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
            gridProjects.DataSource = Session("SortTable")
            gridProjects.DataBind()

        End If

    End Sub
    Private Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "DESC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing _
          AndAlso lastDirection = "DESC" Then

                    sortDirection = "ASC"

                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function


    Protected Sub gridProjects_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "EditDetails" Then
            MeasuresEditid = Integer.Parse(e.CommandArgument.ToString())
            Session("Startup_ID") = MeasuresEditid
            HighlightRow(gridProjects, e)
            Dim ctrl As Control = TryCast(e.CommandSource, Control)
            If ctrl IsNot Nothing Then
                Dim row As GridViewRow = TryCast(ctrl.Parent.NamingContainer, GridViewRow)
                Dim lblAssaignedto As LinkButton = DirectCast(row.FindControl("btnEdit"), LinkButton)
                Dim list As KeyValuePair(Of String, Integer) = New KeyValuePair(Of String, Integer)(lblAssaignedto.Text, MeasuresEditid)
                Session("projectID") = list
                FillData(MeasuresEditid)
                PopulateOutputMeasuresData(Session("Startup_ID").ToString)
            End If
        End If
    End Sub

    Private Sub PopulateOutputMeasuresData(StartupId As String)
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@Startup_ID", StartupId))
        Dim dt As DataTable = ExecuteProcedureForDataTable("CMPDB_spGetOutputMeasures", params)
        If dt.Rows.Count > 0 Then
            Dim dr = dt.Rows(0)
            txtETCTGT.Text = dr("ETC_TGT").ToString
            txtETCActual.Text = dr("ETC_Actual").ToString
            ddlETC.SelectedValue = dr("ETC_Criteria_Met").ToString
            txtPRTGT.Text = dr("PR_TGT").ToString()
            txtPRActual.Text = dr("PR_Actual").ToString
            ddlPR.SelectedValue = dr("PR_Criteria_Met").ToString
            txtGSUMTGT.Text = dr("GSUMSmallProject_TGT").ToString
            txtGSUMActual.Text = dr("GSUMSmallProject_Actual").ToString
            ddlGSUM.SelectedValue = dr("GSUMSmallProject_Criteria_Met").ToString
            txtSOPTGT.Text = dr("SOPDate_TGT").ToString
            txtSOPActual.Text = dr("SOPDate_Actual").ToString
            ddlSOP.SelectedValue = dr("SOPDate_Criteria_Met").ToString
            txtSafetyOfIncidentsTGT.Text = dr("SafetyOfIncidents_TGT").ToString
            txtSafetyOfIncidentsActual.Text = dr("SafetyOfIncidents_Actual").ToString
            ddlSafetyOfIncidents.SelectedValue = dr("SafetyOfIncidents_Criteria_Met").ToString
            ddlHSE.SelectedValue = dr("HSE").ToString
            ddlQuality.SelectedValue = dr("Quality").ToString
            txtSmallStartupCriteriaMet.Text = IIf(CType(dr("All_Small_Startup_Criteria_Met"), Boolean), "Yes", "No")
        End If

    End Sub

    Protected Sub gridProjects_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridProjects.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _row As DataRowView = e.Row.DataItem
            If _row IsNot Nothing Then

                Dim _field = _row.Row("PCA_Approved")
                If _field Is DBNull.Value Then
                    Exit Sub
                End If
                If _field = "True" Then
                    e.Row.BackColor = System.Drawing.Color.Green
                Else
                    e.Row.BackColor = System.Drawing.Color.FromArgb(0.22, Drawing.Color.Red)
                End If
            End If
        End If
    End Sub
    Private Sub FillData(MeasuresEditid As String)
        Dim row1 = New EditMeasuresData(txtCell1, lblFile1LstUpdDte, lnkFile1, lblFile1, txtws1, txtwc1)
        Dim row2 = New EditMeasuresData(txtCell2, lblFile2LstUpdDte, lnkFile2, lblFile2, txtws2, txtwc2)
        Dim row3 = New EditMeasuresData(txtCell3, lblFile3LstUpdDte, lnkFile3, lblFile3, txtws3, txtwc3)
        Dim row4 = New EditMeasuresData(txtCell4, lblFile4LstUpdDte, lnkFile4, lblFile4, txtws4, txtwc4)
        Dim row5 = New EditMeasuresData(txtCell5, lblFile5LstUpdDte, lnkFile5, lblFile5, txtws5, txtwc5)
        Dim row6 = New EditMeasuresData(txtCell6, lblFile6LstUpdDte, lnkFile6, lblFile6, txtws6, txtwc6)
        Dim row7 = New EditMeasuresData(txtCell7, lblFile7LstUpdDte, lnkFile7, lblFile7, txtws7, txtwc7)
        Dim row8 = New EditMeasuresData(txtCell8, lblFile8LstUpdDte, lnkFile8, lblFile8, txtws8, txtwc8)
        GetMeasuresValues(MeasuresEditid, row1, row2, row3, row4, row5, row6, row7, row8)
        'PopulateCriticalMilestones(MeasuresEditid)
        btnMileStone.Enabled = True
        btnAcceptUpdate.Enabled = True
    End Sub


    Private Sub GetMeasuresValues(mStartupID As String, ParamArray mEditMeasursesData() As EditMeasuresData)
        Try
            Dim paramsCriticalMilestone As New List(Of SqlParameter)
            paramsCriticalMilestone.Add(New SqlParameter("@Startup_ID", mStartupID))
            Dim dtCriticalMilestone As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_Critical_Milestone", paramsCriticalMilestone)
            If dtCriticalMilestone.Rows.Count > 0 Then
                txtPCStarategyAndCommitment.Text = dtCriticalMilestone.Rows(0)("Mlstn_PC_Strategy_Commitment").ToString()
                txtStartUpWorkProcess.Text = dtCriticalMilestone.Rows(0)("Mlstn_StartUp_Work_Process").ToString()
                txtStartOfProduction.Text = dtCriticalMilestone.Rows(0)("Mlstn_Start_of_Prod").ToString()
                txtNextVATDate.Text = dtCriticalMilestone.Rows(0)("vdate").ToString()
                txtNextConstructionDate.Text = dtCriticalMilestone.Rows(0)("cdate").ToString()
                txtNextEO.Text = dtCriticalMilestone.Rows(0)("eodate").ToString()
            Else
                ResetControls(txtPCStarategyAndCommitment, txtStartUpWorkProcess, txtStartOfProduction, txtNextVATDate, txtNextConstructionDate, txtNextEO)
            End If

            Dim paramsInProcess As New List(Of SqlParameter)
            paramsInProcess.Add(New SqlParameter("@Startup_ID", mStartupID))
            Dim dtINProcessMeasures As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_GetINProcessMeasures", paramsInProcess)

            If dtINProcessMeasures.Rows.Count > 0 Then
                txtTotalSafetyIncidents.Text = dtINProcessMeasures.Rows(0)("Total_Safety_Incidents").ToString()
                txtETC.Text = dtINProcessMeasures.Rows(0)("Etc").ToString()
                txtPRLast.Text = dtINProcessMeasures.Rows(0)("PR_Pct").ToString()
                txtComments.Text = dtINProcessMeasures.Rows(0)("Comments").ToString()
            Else
                ResetControls(txtTotalSafetyIncidents, txtETC, txtPRLast, txtComments)
            End If

            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@Startup_ID", mStartupID))
            params.Add(New SqlParameter("@Type", ""))
            params.Add(New SqlParameter("@startupsBLOBFilesID", ""))

            Dim dtMeasures As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_GetProcessMeasures", params)
            Dim i = 0
            Dim list As New List(Of String)
            For Each em In mEditMeasursesData
                Dim paramsNew As New List(Of SqlParameter)

                For Each row In dtMeasures.Rows
                    If (row("SWP_Tool_Name").ToString.Equals(em.mlblFileName.Text)) Then

                        list.Add(row("SWP_Tool_Name_ID"))

                        Session("SaveList") = list
                        ' Append Value in excel and using
                        Dim lst As List(Of Object) = ReadCellValueFromWorkSheetNameAndAddContentToCell(row("FileObject"),
                                                                                                       "ConnectSheet",
                                                                                                       "A2",
                                                                                                       row("Project_WS_Name"),
                                                                                                       row("Project_CellAddress"),
                                                                                                       row("BLOBFile_ID"))
                        em.mtxtBox.Text = lst(0)

                        paramsNew.Add(New SqlParameter("@Table_Name", "CMPDB_tblStartupBLOBFiles"))
                        paramsNew.Add(New SqlParameter("@BLOB_ID", row("BLOBFile_ID")))
                        paramsNew.Add(New SqlParameter("@FileObject", lst(1)))
                        'paramsNew.Add(New SqlParameter("@FileName", "jysng.xlsm"))

                        ExecuteProcedure("CMPDB_sp_InsertBLOBwithID", paramsNew)
                        em.mDateLable.Text = row("FileUploadeddate")
                        em.mDateLable.Visible = True
                        em.mlnkbtnFileName.Text = row("SWP_Tool_Name")
                        em.mWorkSheetName.Text = row("Project_WS_Name")

                        If row("cellvalue") Is DBNull.Value Then
                            row("cellvalue") = ""
                        End If
                        em.mCellAddress.Text = row("Project_CellAddress")
                        em.mtxtBox.Text = row("cellvalue")

                        em.mlnkbtnFileName.Visible = True
                        em.mlblFileName.Visible = False
                        em.mlnkbtnFileName.CommandArgument = row("startupsBLOBFiles_ID")

                        em.mCellAddress.Style.Add("display", "block")

                        em.mDateLable.Style.Add("display", "block")
                        em.mlblFileName.Style.Add("display", "block")
                        em.mlnkbtnFileName.Style.Add("display", "block")
                        em.mtxtBox.Style.Add("display", "block")
                        em.mWorkSheetName.Style.Add("display", "block")


                        lst = Nothing
                        Exit For
                    Else
                        em.mCellAddress.Style.Add("display", "none")
                        em.mDateLable.Style.Add("display", "none")
                        em.mlblFileName.Style.Add("display", "none")
                        em.mlnkbtnFileName.Style.Add("display", "none")
                        em.mtxtBox.Style.Add("display", "none")
                        em.mWorkSheetName.Style.Add("display", "none")

                    End If

                Next

                i = i + 1
            Next
        Catch ex As Exception
            Dim s = ex.Message
            lblFile2LstUpdDte.Text = s
        End Try
    End Sub

    Protected Sub chk1_CheckedChanged(sender As Object, e As EventArgs)
        If chk1.Checked Then
            dvCellWork.Style.Add("display", "block")
            LoadDataForSheetAndCell()
        Else
            dvCellWork.Style.Add("display", "none")
        End If
    End Sub

    Private Sub LoadDataForSheetAndCell()
        If gridProjects.SelectedIndex > -1 Then
            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@Startup_ID", Session("Startup_ID")))
            Dim dt = ExecuteProcedureForDataTable("CMPDB_spGetCellAndWorkSheetName", params)

            txtws1.Text = dt
        End If
    End Sub

    Protected Sub btnMileStone_Click(sender As Object, e As EventArgs)
        If Session("projectID") Is Nothing Then
            Exit Sub
        End If
        Response.Redirect("Startupstargets.aspx?pageId=1")
    End Sub

    Protected Sub btnAcceptUpdate_Click(sender As Object, e As EventArgs)

        SaveMeasuresValues()
        MessageBox("Successfully Saved !!")


    End Sub

    Private Sub SaveMeasuresValues()

        Dim params As New List(Of SqlParameter)
        Dim mb As New StringBuilder
        Dim sb As New StringBuilder

        mb.AppendLine("<?xml version=""1.0"" ?>")
        sb.AppendLine("<?xml version=""1.0"" ?>")

        mb.AppendLine("<InsertRecords>")
        mb.AppendLine("<InsertRecords>")
        mb.AppendLine("             <Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")

        mb.AppendLine("             <Total_Safety_Incidents>" + txtTotalSafetyIncidents.Text + "</Total_Safety_Incidents>")
        mb.AppendLine("             <ETC>" + txtETC.Text + "</ETC>")
        mb.AppendLine("             <PR_Pct>" + txtPRLast.Text + "</PR_Pct>")
        mb.AppendLine("             <Comments>" + txtComments.Text + "</Comments>")
        mb.AppendLine("         </InsertRecords>")


        mb.AppendLine("</InsertRecords>")
        sb.AppendLine("<InsertRecordsQty>")
        Dim list As New List(Of String)
        list = Session("SaveList")
        For Each row In list
            sb.AppendLine("<InsertRecordsQty>")

            If row = 1 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws1.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc1.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell1.Text + "</Project_CellValue>")


            End If
            If row = 2 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws2.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc2.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell2.Text + "</Project_CellValue>")


            End If
            If row = 3 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws3.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc3.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell3.Text + "</Project_CellValue>")


            End If
            If row = 4 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws4.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc4.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell4.Text + "</Project_CellValue>")


            End If
            If row = 5 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws5.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc5.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell5.Text + "</Project_CellValue>")


            End If
            If row = 6 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws6.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc6.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell6.Text + "</Project_CellValue>")


            End If
            If row = 7 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws7.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc7.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell7.Text + "</Project_CellValue>")


            End If
            If row = 8 Then

                sb.AppendLine("<Startup_ID>" + Session("Startup_ID").ToString() + "</Startup_ID>")
                sb.AppendLine("<SWP_Tool_Name_ID>" + row + "</SWP_Tool_Name_ID>")
                sb.AppendLine("<Project_WS_Name>" + txtws8.Text + "</Project_WS_Name>")
                sb.AppendLine("<Project_CellAddress>" + txtwc8.Text + "</Project_CellAddress>")
                sb.AppendLine("<Project_CellValue>" + txtCell8.Text + "</Project_CellValue>")


            End If

            sb.AppendLine("</InsertRecordsQty>")

        Next
        sb.AppendLine("</InsertRecordsQty>")
        Dim encodedXml = mb.Replace("&", "&amp;").ToString()
        Dim encodedXml2 = sb.Replace("&", "&amp;").ToString()


        params.Add(New SqlParameter("@Single", encodedXml))
        params.Add(New SqlParameter("@Multiple", encodedXml2))
        ExecuteProcedure("CMPDB_sp_InsertUpdate_Measures", params)

    End Sub

    Protected Sub btnSerach_Click(sender As Object, e As EventArgs)
        If divSrchExist.Attributes("style") = "display:none;" Then

            divSrchExist.Attributes.Add("style", "display:block;")
            fdiv.Attributes.Add("style", "display:block;")

            If Session("plant_Set") <> "" Then
                PopulateDD(ddlPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
                ddlPlant.SelectedValue = Session("plant_Set")

            End If
        ElseIf divSrchExist.Attributes("style") = "display:block;" Then
            divSrchExist.Attributes.Add("style", "display:none;")
            fdiv.Attributes.Add("style", "display:none;")
            If Session("plant_Set") <> "" Then
                PopulateDD(ddlPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
                ddlPlant.SelectedValue = Session("plant_Set")
                Session.Abandon()

            End If
        End If
        'Session.Abandon()
        LoadAllDependents()
    End Sub

    Protected Sub btnimgRefresh_Click(sender As Object, e As ImageClickEventArgs)
        gridProjects_RowCommand(sender, EventArgs.Empty)
    End Sub

    Protected Sub btnCloseProject_Click(sender As Object, e As EventArgs)
        Dim params As New List(Of SqlParameter)

        params.Add(New SqlParameter("@Startup_ID", Session("Startup_ID").ToString()))
        params.Add(New SqlParameter("@ETC_TGT", txtETCTGT.Text))
        params.Add(New SqlParameter("@ETC_Actual", txtETCActual.Text))
        params.Add(New SqlParameter("@ETC_Criteria_Met", ddlETC.SelectedItem.Value))
        params.Add(New SqlParameter("@PR_TGT", txtPRTGT.Text))
        params.Add(New SqlParameter("@PR_Actual", txtPRActual.Text))
        params.Add(New SqlParameter("@PR_Criteria_Met", ddlPR.SelectedItem.Value))
        params.Add(New SqlParameter("@GSUMSmallProject_TGT", txtGSUMTGT.Text))
        params.Add(New SqlParameter("@GSUMSmallProject_Actual", txtGSUMActual.Text))
        params.Add(New SqlParameter("@GSUMSmallProject_Criteria_Met", ddlGSUM.SelectedItem.Value))
        params.Add(New SqlParameter("@SOPDate_TGT", txtSOPTGT.Text))
        params.Add(New SqlParameter("@SOPDate_Actual", txtSOPActual.Text))
        params.Add(New SqlParameter("@SOPDate_Criteria_Met", ddlSOP.SelectedItem.Value))
        params.Add(New SqlParameter("@SafetyOfIncidents_TGT", txtSafetyOfIncidentsTGT.Text))
        params.Add(New SqlParameter("@SafetyOfIncidents_Actual", txtSafetyOfIncidentsActual.Text))
        params.Add(New SqlParameter("@SafetyOfIncidents_Criteria_Met", ddlSafetyOfIncidents.SelectedItem.Value))
        params.Add(New SqlParameter("@HSE", ddlHSE.SelectedItem.Value))
        params.Add(New SqlParameter("@Quality", ddlQuality.SelectedItem.Value))
        params.Add(New SqlParameter("@All_Small_Startup_Criteria_Met", GetSmallData()))
        If Session("Startup_ID") IsNot Nothing Then
            ExecuteProcedure("CMPDB_spInsertUpdateStartup_Output_Measures", params)
            ResetControls(txtETCTGT, txtETCActual, txtPRTGT, txtPRActual, txtGSUMTGT, txtGSUMActual, txtSOPTGT, txtSOPActual, txtSafetyOfIncidentsTGT, txtSafetyOfIncidentsActual)
            ResetControls(ddlETC, ddlPR, ddlQuality, ddlSOP, ddlHSE, ddlGSUM, ddlSafetyOfIncidents)
            MessageBox("Project Closed.")
        End If

    End Sub

    Private Function GetSmallData() As Int32
        If txtSmallStartupCriteriaMet.Text = "Yes" Then
            Return 1
        Else
            Return 0
        End If
    End Function
#End Region


End Class