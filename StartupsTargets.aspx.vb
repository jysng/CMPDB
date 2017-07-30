Imports System.Data.SqlClient

Public Class Startupstargets
    Inherits Page
#Region "Variables"
    Dim xUpdateImagePath = "~/images/Update.png"
    Dim xAddImagePath = "~/images/Add.png"
    Dim xTableNameEODates = "CMPDB_vwStartup_EO_Dates"
    Dim xTableNameConstructionDates = "CMPDB_vwStartup_Construction_Dates"
    Dim xTableNameVatDates = "CMPDB_vwStartup_Vat_Dates"
    Dim xTableProjectName = "CMPDB_tblStartups_New"
    Dim list As KeyValuePair(Of String, Integer)
#End Region
#Region "Common"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()

        If Not Me.IsPostBack Then

            If Session("projectID") IsNot Nothing Then
                Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
                Dim valueproj = list.Key

                Dim dt As DataTable = GetDataTableFromSQL("select startup_Name,Startup_ID from CMPDB_tblStartups_New where Project_Name like'%" + valueproj + "%'")
                If dt.Rows.Count > 0 Then
                    Dim countrow = dt.Rows.Count
                    txtProject.Text = list.Key
                    PopulateDDFromDataTable(ddlStartUpName, dt)
                    Dim ddlvalue = dt.Rows(0)("Startup_ID")
                    ddlStartUpName.SelectedValue = ddlvalue
                    'ddlStartUpName.Items.FindByValue(ddlvalue).Selected = True
                    'txtProject.Text = ""
                End If
                If valueproj IsNot Nothing Then
                    txtProject.Enabled = False
                    'ddlStartUpName.Enabled = False
                End If
            End If

            PopulateDD(lstboxEODate, xTableNameEODates, "EO_Date_ID", "DayDate", "Startup_ID", list.Value)
            PopulateDD(lstboxConstructionDate, xTableNameConstructionDates, "Construction_Date_ID", "DayDate", "Startup_ID", list.Value)
            PopulateDD(lstboxVATDate, xTableNameVatDates, "VAT_Date_ID", "DayDate", "Startup_ID", list.Value)
            showtargets(list.Value)

            chkAdvancedMode_CheckedChanged(sender, EventArgs.Empty)

            If Session("UserRole") = "1" Then
                btnApprovedPCA.Enabled = True
            Else
                btnApprovedPCA.Enabled = False
            End If
        End If
    End Sub
#End Region

#Region "Populates"

#End Region

#Region "Startup Targets Section"
    Private Sub showtargets(Startup_ID As String)
        Dim dt = New DataTable
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@Startup_ID", Startup_ID))

        dt = ExecuteProcedureForDataTable("CMPDB_sp_Search_tblStartup_Targets", params)

        If dt.Rows.Count > 0 Then
            ddlStartUpStatus.SelectedValue = dt.Rows(0)("Startup_Status").ToString()
            chkCharacter.Checked = dt.Rows(0)("Charter_Approved").ToString()
            chkPlantAE.Checked = dt.Rows(0)("Plant_AAE").ToString()
            txtSAPProject.Text = dt.Rows(0)("SAP_Project").ToString()

            txtETCTarget.Text = dt.Rows(0)("ETC_Target").ToString()
            txtPRTarget.Text = dt.Rows(0)("PR_Target_Pct").ToString()


            txtMlstnFeasibility.Text = dt.Rows(0)("Mlstn_Feas_Early_Mfg_Involvement").ToString()
            txtMlstnConceptual.Text = dt.Rows(0)("Mlstn_Conce_Perli_Planning").ToString()
            txtMlstnPCStrategy.Text = dt.Rows(0)("Mlstn_PC_Strategy_Commitment").ToString()
            txtMlstnProjectFSF.Text = dt.Rows(0)("Mlstn_Project_FSF_Planning_Excecution").ToString()
            txtMlstnStartupWorkProcess.Text = dt.Rows(0)("Mlstn_StartUp_Work_Process").ToString()
            txtMlstnStartupProduction.Text = dt.Rows(0)("Mlstn_Start_of_Prod").ToString()

            txtMlstnSustain.Text = dt.Rows(0)("Mlstn_Sustain_Mfg_Verification").ToString()
            txtMlstnPSU.Text = dt.Rows(0)("Mlstn_PSU_Followups").ToString()
            txtMlstnG2TDay1.Text = dt.Rows(0)("Mlstn_G2T_Day_1").ToString()
            txtMlstnG2TConstructionCompletion.Text = dt.Rows(0)("Mlstn_G2T_Construction_Completion").ToString()
            txtMlstnG2TEndofPQPhase.Text = dt.Rows(0)("Mlstn_G2T_End_of_PQ_Phase").ToString()
            txtMlstnG2T_End_of_Ininital_Verification.Text = dt.Rows(0)("Mlstn_G2T_End_of_Ininital_Verification_Completion_Phase").ToString()
            txtcomments.Text = dt.Rows(0)("Comments").ToString()
            txtPCAApprovedDate.Text = Convert.ToDateTime(dt.Rows(0)("PCA_Approved").ToString)
            txtPCAApprovedDate.Enabled = False
        End If
    End Sub

    Protected Sub chkAdvancedMode_CheckedChanged(sender As Object, e As EventArgs)
        If chkAdvancedMode.Checked = True Then
            ResetControls(ddlStartUpStatus)
            ResetControls(txtETCTarget, txtPRTarget, txtMlstnPCStrategy, txtMlstnStartupWorkProcess, txtMlstnStartupProduction)
            ResetControls(chkCharacter)
        End If
        ShowHideControls(chkAdvancedMode.Checked, divStartupStatus,
           divCharacterApproved,
           divEtcTargetStatus,
           diPrTargetStatus,
           divtxtMlstnPCStrategy,
           divtxtMlstnStartupWorkProcess,
           divtxtMlstnStartupProduction,
           divMlstnPCStrategy,
           divMlstnStartupWorkProcess,
           divlblMlstnStartupProduction, hr1)
    End Sub

    Private Sub SaveStartupTargets(StartupType As String)

        If chkAdvancedMode.Checked = False Then
            If ddlStartUpStatus.SelectedValue = 0 Then
                MessageBox("Please Select StartUp Status !!")
                ddlStartUpStatus.Focus()
                Exit Sub
            End If
            If chkCharacter.Checked = False Then
                MessageBox("Please check Charter Approved !!")
                Exit Sub
            End If
            If txtETCTarget.Text = "" Then
                MessageBox("Please Insert ETC Target($M) !!")
                txtETCTarget.Focus()
                Exit Sub
            End If
            If txtPRTarget.Text = "" Then
                MessageBox("Please Insert PR Target % !!")
                txtPRTarget.Focus()
                Exit Sub
            End If

            If txtMlstnPCStrategy.Text = "" Then
                MessageBox("Please Select Date !!")
                Exit Sub
            End If

            If txtMlstnStartupWorkProcess.Text = "" Then
                MessageBox("Please Select Date !!")
                Exit Sub
            End If

            If txtMlstnStartupProduction.Text = "" Then
                MessageBox("Please Select Date !!")
                Exit Sub
            End If

        End If

        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
        Dim Startup_ID = list.Value
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@Startuptype", StartupType))
        params.Add(New SqlParameter("@Startup_ID", Startup_ID))
        params.Add(New SqlParameter("@Startup_Status", IIf(ddlStartUpStatus.SelectedValue = 0, "", ddlStartUpStatus.SelectedValue)))
        params.Add(New SqlParameter("@Charter_Approved", IIf(chkCharacter.Checked = True, True, False)))
        params.Add(New SqlParameter("@Plant_AAE", IIf(chkPlantAE.Checked = True, True, False)))
        params.Add(New SqlParameter("@SAP_Project", txtSAPProject.Text))
        params.Add(New SqlParameter("@ETC_Target", txtETCTarget.Text))
        params.Add(New SqlParameter("@PR_Target_Pct", txtPRTarget.Text))
        params.Add(New SqlParameter("@Mlstn_Feas_Early_Mfg_Involvement", IIf(txtMlstnFeasibility.Text = "", DBNull.Value, txtMlstnFeasibility.Text)))
        params.Add(New SqlParameter("@Mlstn_Conce_Perli_Planning", IIf(txtMlstnConceptual.Text = "", DBNull.Value, txtMlstnConceptual.Text)))

        params.Add(New SqlParameter("@Mlstn_PC_Strategy_Commitment", IIf(txtMlstnPCStrategy.Text = "", DBNull.Value, txtMlstnPCStrategy.Text)))
        params.Add(New SqlParameter("@Mlstn_Project_FSF_Planning_Excecution", IIf(txtMlstnProjectFSF.Text = "", DBNull.Value, txtMlstnProjectFSF.Text)))
        params.Add(New SqlParameter("@Mlstn_StartUp_Work_Process", IIf(txtMlstnStartupWorkProcess.Text = "", DBNull.Value, txtMlstnStartupWorkProcess.Text)))
        params.Add(New SqlParameter("@Mlstn_Start_of_Prod", IIf(txtMlstnStartupProduction.Text = "", DBNull.Value, txtMlstnStartupProduction.Text)))
        params.Add(New SqlParameter("@Mlstn_Sustain_Mfg_Verification", IIf(txtMlstnSustain.Text = "", DBNull.Value, txtMlstnSustain.Text)))
        params.Add(New SqlParameter("@Mlstn_PSU_Followups", IIf(txtMlstnPSU.Text = "", DBNull.Value, txtMlstnPSU.Text)))
        params.Add(New SqlParameter("@Mlstn_G2T_Day_1", IIf(txtMlstnG2TDay1.Text = "", DBNull.Value, txtMlstnG2TDay1.Text)))
        params.Add(New SqlParameter("@Mlstn_G2T_Construction_Completion", IIf(txtMlstnG2TConstructionCompletion.Text = "", DBNull.Value, txtMlstnG2TConstructionCompletion.Text)))
        params.Add(New SqlParameter("@Mlstn_G2T_End_of_PQ_Phase", IIf(txtMlstnG2TEndofPQPhase.Text = "", DBNull.Value, txtMlstnG2TEndofPQPhase.Text)))
        params.Add(New SqlParameter("@Mlstn_G2T_End_of_Ininital_Verification_Completion_Phase", IIf(txtMlstnG2TEndofPQPhase.Text = "", DBNull.Value, txtMlstnG2TEndofPQPhase.Text)))
        params.Add(New SqlParameter("@Comments", txtcomments.Text))
        ExecuteProcedure("CMPDB_sp_InsertUpdate_tblStartupTargets", params)

        '  ResetControls(ddlBu, ddlPlant, ddlRegion, ddlDepartment, ddlSWp, ddlSWPRole)
        ' ResetControls(txtDateAdded, txtInsertEmail, txtQualificationDate, txtQualifier, txtTargetDate, txtClassCompletedDate, txtTechCoachEmail, txtcomments)


    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        'send parameter 0 to save only'
        SaveStartupTargets(0)
        MessageBox("Submit Successfully !!")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        ResetControls(ddlStartUpStatus)
        ResetControls(txtSAPProject, txtETCTarget, txtPRTarget, txtMlstnFeasibility, txtMlstnConceptual, txtMlstnPCStrategy, txtMlstnProjectFSF, txtMlstnStartupWorkProcess, txtMlstnStartupProduction, txtMlstnSustain, txtMlstnPSU, txtMlstnG2TDay1, txtMlstnG2TConstructionCompletion, txtMlstnG2TEndofPQPhase, txtMlstnG2T_End_of_Ininital_Verification, txtcomments)
        chkCharacter.Checked = False
        chkPlantAE.Checked = False

    End Sub

    Protected Sub btnback_Click(sender As Object, e As EventArgs)
        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
        Dim Startup_ID = list.Value
        If (Request.QueryString("pageId") = "1") Then
            Response.Redirect("EditMeasures.aspx?StartupId=" + Session("projectID").ToString())
        End If

        If (Request.QueryString("pageId") = "2") Then
            Response.Redirect("Admin_Project.aspx?StartupId=" + Session("projectID").ToString())
        End If

    End Sub

    Protected Sub btnApprovedPCA_Click(sender As Object, e As EventArgs)
        'Send parameter 1 to ApprovedPCA'
        SaveStartupTargets(1)
    End Sub
#End Region

#Region "EO Date Section"
    Protected Sub btnDeleteEODate_Click(sender As Object, e As ImageClickEventArgs)
        DeleteRecordsListControls(xTableNameEODates + ",'" + lstboxEODate.SelectedItem.Value + "'")
        btnRefreshEODate_Click(sender, e)
    End Sub
    Protected Sub btnAddEODate_Click(sender As Object, e As ImageClickEventArgs)
        If txtDays.Text <> String.Empty Or txtEODate.Text <> String.Empty Then
            Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
            Dim strAddUpdate As String = String.Empty
            If btnAddEODate.ImageUrl = xAddImagePath Then
                strAddUpdate = xTableNameEODates + ",'Duration_Days,EO_Date,Startup_ID',I,'''" + txtDays.Text + "'',''" + txtEODate.Text + "'',''" + list.Value.ToString + "''','',Startup_ID"
            ElseIf btnAddEODate.ImageUrl = xUpdateImagePath Then
                If txtEODate.Text + " Days :" + txtDays.Text <> lstboxEODate.SelectedItem.Text Then
                    strAddUpdate = xTableNameEODates + ",'Duration_Days=''" + txtDays.Text + "''',U,''," + lstboxEODate.SelectedItem.Value + ",EO_Date_ID"
                Else
                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsDependentListControls(strAddUpdate)
            btnRefreshEODate_Click(sender, e)
        End If
    End Sub
    Protected Sub btnRefreshEODate_Click(sender As Object, e As ImageClickEventArgs)
        ResetControls(txtDays, txtEODate)
        ResetControls(lstboxEODate)
        btnAddEODate.ImageUrl = xAddImagePath
        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
        PopulateDD(lstboxEODate, xTableNameEODates, "EO_Date_ID", "DayDate", "Startup_ID", list.Value)
    End Sub
    Protected Sub lstboxEODate_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtEODate.Text = lstboxEODate.SelectedItem.Text.Split("|")(0)
        txtDays.Text = lstboxEODate.SelectedItem.Text.Split("|")(1).Replace("Days :", "").Trim
        btnAddEODate.ImageUrl = xUpdateImagePath
    End Sub


#End Region

#Region "Construction Date Section"
    Protected Sub btnDeleteConstructionDate_Click(sender As Object, e As ImageClickEventArgs)
        DeleteRecordsListControls(xTableNameConstructionDates + ",'" + lstboxConstructionDate.SelectedItem.Value + "'")
        btnRefreshConstructionDate_Click(sender, e)
    End Sub
    Protected Sub btnAddConstructionDate_Click(sender As Object, e As ImageClickEventArgs)
        If txtConstructionDays.Text <> String.Empty Or txtConstructionDate.Text <> String.Empty Then
            Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
            Dim strAddUpdate As String = String.Empty
            If btnAddConstructionDate.ImageUrl = xAddImagePath Then
                strAddUpdate = xTableNameConstructionDates + ",'Duration_Days,Construction_Date,Startup_ID',I,'''" + txtConstructionDays.Text + "'',''" + txtConstructionDate.Text + "'',''" + list.Value.ToString + "''','',Startup_ID"
            ElseIf btnAddConstructionDate.ImageUrl = xUpdateImagePath Then
                If txtConstructionDate.Text + " Days :" + txtConstructionDays.Text <> lstboxConstructionDate.SelectedItem.Text Then
                    strAddUpdate = xTableNameConstructionDates + ",'Duration_Days=''" + txtConstructionDays.Text + "''',U,''," + lstboxConstructionDate.SelectedItem.Value + ",Construction_Date_ID"
                Else
                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsDependentListControls(strAddUpdate)
            btnRefreshConstructionDate_Click(sender, e)
        End If
    End Sub
    Protected Sub btnRefreshConstructionDate_Click(sender As Object, e As ImageClickEventArgs)
        ResetControls(txtConstructionDate, txtConstructionDays)
        ResetControls(lstboxConstructionDate)
        btnAddConstructionDate.ImageUrl = xAddImagePath
        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")

        PopulateDD(lstboxConstructionDate, xTableNameConstructionDates, "Construction_Date_ID", "DayDate", "Startup_ID", list.Value)
    End Sub
    Protected Sub lstboxConstructionDate_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtConstructionDate.Text = lstboxConstructionDate.SelectedItem.Text.Split("|")(0)
        txtConstructionDays.Text = lstboxConstructionDate.SelectedItem.Text.Split("|")(1).Replace("Days :", "").Trim
        btnAddConstructionDate.ImageUrl = xUpdateImagePath
    End Sub

#End Region

#Region "Vat Dates Section"


    Protected Sub btnDeleteVatDate_Click(sender As Object, e As ImageClickEventArgs)
        DeleteRecordsListControls(xTableNameVatDates + ",'" + lstboxVATDate.SelectedItem.Value + "'")
        btnRefreshVatDate_Click(sender, e)
    End Sub
    Protected Sub btnAddVatDate_Click(sender As Object, e As ImageClickEventArgs)
        If txtVatDays.Text <> String.Empty Or txtVatDate.Text <> String.Empty Then
            Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
            Dim strAddUpdate As String = String.Empty
            If btnAddVatDate.ImageUrl = xAddImagePath Then
                strAddUpdate = xTableNameVatDates + ",'Duration_Days,VAT_Date,Startup_ID',I,'''" + txtVatDays.Text + "'',''" + txtVatDate.Text + "'',''" + list.Value.ToString + "''','',Startup_ID"
            ElseIf btnAddVatDate.ImageUrl = xUpdateImagePath Then
                If txtVatDate.Text + " Days :" + txtVatDays.Text <> lstboxVATDate.SelectedItem.Text Then
                    strAddUpdate = xTableNameVatDates + ",'Duration_Days=''" + txtVatDays.Text + "''',U,''," + lstboxVATDate.SelectedItem.Value + ",VAT_Date_ID"
                Else
                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsDependentListControls(strAddUpdate)
            btnRefreshVatDate_Click(sender, e)
        End If
    End Sub

    Protected Sub btnRefreshVatDate_Click(sender As Object, e As ImageClickEventArgs)
        ResetControls(txtVatDate, txtVatDays)
        ResetControls(lstboxVATDate)
        btnAddVatDate.ImageUrl = xAddImagePath
        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")

        PopulateDD(lstboxVATDate, xTableNameVatDates, "VAT_Date_ID", "DayDate", "Startup_ID", list.Value)
    End Sub
    Protected Sub lstboxVATDate_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtVatDate.Text = lstboxVATDate.SelectedItem.Text.Split("|")(0)
        txtVatDays.Text = lstboxVATDate.SelectedItem.Text.Split("|")(1).Replace("Days :", "").Trim
        btnAddVatDate.ImageUrl = xUpdateImagePath
    End Sub
#End Region

#Region "Next Prev Dates"

    Protected Sub btnPrev_Click(sender As Object, e As EventArgs)
        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")
        Dim dt As DataTable = GetDataTableFromSQL("select Top(1) * from [CMPDB_tblStartTargetsComments] where PCA_Approved <'" + txtPCAApprovedDate.Text + "' and Startup_ID=" + list.Value.ToString + " order by PCA_Approved desc")
        If dt.Rows.Count > 0 Then
            txtPCAApprovedDate.Text = Convert.ToDateTime(dt.Rows(0)("PCA_Approved")).ToString("MM-dd-yyyy HH:mm:ss.fff")
            txtcomments.Text = dt.Rows(0)("Comments")
        End If
    End Sub
    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        Dim list As KeyValuePair(Of String, Integer) = Session("projectID")

        If txtPCAApprovedDate.Text = "" Then
            Exit Sub
        End If

        Dim createddate = Convert.ToDateTime(txtPCAApprovedDate.Text).ToString("yyyy-MM-dd HH:mm:ss.fff")
        Dim dt As DataTable = GetDataTableFromSQL("select Top(1) * from [CMPDB_tblStartTargetsComments] where  PCA_Approved  >'" + createddate + "' and Startup_ID=" + list.Value.ToString + " order by PCA_Approved ")

        If dt.Rows.Count > 0 Then
            txtPCAApprovedDate.Text = Convert.ToDateTime(dt.Rows(0)("PCA_Approved")).ToString("MM-dd-yyyy HH:mm:ss.fff")
            txtcomments.Text = dt.Rows(0)("Comments")
        End If
    End Sub

    Protected Sub btnearchProj_Click(sender As Object, e As EventArgs)
        If txtProject.Text = String.Empty Then
            MessageBox("Please Fill The Project Name!!")
            Exit Sub
        Else
            Dim value As String = txtProject.Text.Length()
            If (value < 3) Then
                MessageBox("Minimum 3 Character is must !!")
                Exit Sub
            Else
                Dim valueproj = txtProject.Text
                Dim dt As DataTable = GetDataTableFromSQL("select startup_Name,Startup_ID from CMPDB_tblStartups_New where Project_Name like'%" + valueproj + "%'")
                If dt.Rows.Count > 0 Then
                    Dim countrow = dt.Rows.Count
                    PopulateDDFromDataTable(ddlStartUpName, dt)
                    'txtProject.Text = ""
                End If
            End If
        End If
    End Sub
#End Region
End Class