Imports System.Data.SqlClient

Public Class Admin_Project
    Inherits System.Web.UI.Page

#Region "Production Line Section Updated"
    Private Sub populateProductionLines()
        Dim params As New List(Of SqlParameter)
        If ddlSrchBusinessUnit.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@BU_ID", ddlSrchBusinessUnit.SelectedItem.Value))
        End If
        'If .SelectedIndex > 0 Then
        '    params.Add(New SqlParameter("@Platform_ID", DropDownListPlatform.SelectedItem.Value))
        'End If
        'If ddlSrchProjectType.SelectedIndex > 0 Then
        '    params.Add(New SqlParameter("@Production_Type_ID", DropDownListProductionType.SelectedItem.Value))
        'End If
        params.Add(New SqlParameter("@Site_ID", DdlPlants.SelectedItem.Value))
        Dim dt = ExecuteProcedureForDataTable("sp_FillWithProductionLine", params)
        PopulateDDFromDataTable(ddlSrchProductionLine, dt)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()
        If Not IsPostBack Then
            SetTools()
            'Throw New Exception()
            PopulateDD(DdlPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")
            PopulateDD(DdlPlantsInsert, "CMPDB_tblPlants", "Plant_ID", "Plant")
            PopulateDD(DdlChangeType, "CMPDB_tblChange_Types", "Change_Type_ID", "Change_Type")
            PopulateDD(DdlProjectType, "CMPDB_tblProject_Types", "Project_Type_ID", "Project_Type")
            PopulateDD(ddlSrchChangeType, "CMPDB_tblChange_Types", "Change_Type_ID", "Change_Type")
            PopulateDD(ddlSrchProjectType, "CMPDB_tblProject_Types", "Project_Type_ID", "Project_Type")
            'PopulateDD(ddlSrchProductionLine, "CMPDB_tblSite_Production_Lines", "Site_Production_Line_ID", "Site_Production_Line_Name")

            PopulateDD(DdlLinktoCBn, "CMPDB_tblSite_CBNs", "Site_CBN_ID", "Site_CBN")
            PopulateDD(DdlBusinessUnit, "CMPDB_tblBusiness_Unit", "Business_Unit_ID", "Business_Unit")
            PopulateDD(ddlSrchBusinessUnit, "CMPDB_tblBusiness_Unit", "Business_Unit_ID", "Business_Unit")
            PopulateDD(DdlCOmplexityofStartup, "CMPDB_tblComplexityLevel", "Complexity_Situation_ID", "ComplexitySituation")
            PopulateDD(DdlImpactDept, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department")
            PopulateDD(DdlLeadingDept, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department")

            InitialRadioButton()
            chkAdvancedMode.Checked = False
            chkAdvancedMode_CheckedChanged(sender, EventArgs.Empty)
            ShowGridHeader()
            If Session("plant_Set") <> "" Then
                PopulateDD(DdlPlantsInsert, "CMPDB_tblPlants", "Plant_ID", "Plant")
                DdlPlantsInsert.SelectedValue = Session("plant_Set")
                DdlPlantsInsert_SelectedIndexChanged(sender, EventArgs.Empty)
            End If
            'If (chkAdvancedMode.Checked ) Then
        End If
    End Sub

    Protected Sub rb_CheckedChanged(sender As Object, e As EventArgs)
        If rbCustom.Checked Then

            cblGSUM.Items(0).Selected = True
            cblGSUM.Items(1).Selected = True
            cblGSUM.Items(2).Selected = True
            cblGSUM.Items(3).Selected = True
            cblGSUM.Items(4).Selected = True
            cblGSUM.Items(5).Selected = True
            cblGSUM.Items(6).Selected = True
            cblGSUM.Items(7).Selected = True
            ' cblGSUM.Items(8).Selected = True

        End If
        If rbSmall.Checked = True Then

            cblGSUM.Items(0).Selected = False
            cblGSUM.Items(1).Selected = True
            cblGSUM.Items(2).Selected = False
            cblGSUM.Items(3).Selected = False
            cblGSUM.Items(4).Selected = False
            cblGSUM.Items(5).Selected = False
            cblGSUM.Items(6).Selected = False
            cblGSUM.Items(7).Selected = False
            'cblGSUM.Items(8).Selected = False
            cblGSUM.Items(0).Attributes.Add("style", "display:none")
            cblGSUM.Items(2).Attributes.Add("style", "display:none")
            cblGSUM.Items(3).Attributes.Add("style", "display:none")
            cblGSUM.Items(4).Attributes.Add("style", "display:none")
            cblGSUM.Items(5).Attributes.Add("style", "display:none")
            cblGSUM.Items(6).Attributes.Add("style", "display:none")
            cblGSUM.Items(7).Attributes.Add("style", "display:none")
            'cblGSUM.Items(8).Attributes.Add("style", "display:none")

        End If
        If rbLarge.Checked = True Then

            cblGSUM.Items(0).Selected = True
            cblGSUM.Items(1).Selected = False
            cblGSUM.Items(2).Selected = True
            cblGSUM.Items(3).Selected = True
            cblGSUM.Items(4).Selected = True
            cblGSUM.Items(5).Selected = True
            cblGSUM.Items(6).Selected = True
            cblGSUM.Items(7).Selected = True
            'cblGSUM.Items(8).Selected = True
            cblGSUM.Items(1).Attributes.Add("style", "display:none")

        End If
    End Sub

    Protected Sub BtnSetTargetsandMilestones_Click(sender As Object, e As EventArgs) Handles BtnSetTargetsandMilestones.Click
        Dim params As New List(Of SqlParameter)

        Dim Str = (GetSingleValue("select Plant from CMPDB_tblStartups where Plant=" + DdlPlantsInsert.SelectedItem.Value))
        If (Str.Equals(DdlPlantsInsert.SelectedItem.Value) = False) Then
            params.Add(New SqlParameter("@Operation", "I"))
        End If
        If (Str.Equals(DdlPlantsInsert.SelectedItem.Value)) Then
            params.Add(New SqlParameter("@Operation", "U"))
        End If

        params.Add(New SqlParameter("@Startup_Name", txtstartupName.Text))
        params.Add(New SqlParameter("@Plant", DdlPlantsInsert.SelectedItem.Value))

        params.Add(New SqlParameter("@Project_Name", txtProjectName.Text))
        If rbYes.Checked Then
            params.Add(New SqlParameter("@In_All_Change", "true"))
        End If
        If rbNo.Checked Then
            params.Add(New SqlParameter("@In_All_Change", "false"))
        End If

        params.Add(New SqlParameter("@Complexity_of_Startup", DdlCOmplexityofStartup.SelectedItem.Value))
        params.Add(New SqlParameter("@Project_Type", DdlProjectType.SelectedItem.Value))
        params.Add(New SqlParameter("@Change_Type", DdlChangeType.SelectedItem.Value))
        params.Add(New SqlParameter("@Link_to_CBN", DdlLinktoCBn.SelectedItem.Value))
        params.Add(New SqlParameter("@Priority", DdlPriority.SelectedItem.Value))
        params.Add(New SqlParameter("@Production_Line", DdlProductionLine.SelectedItem.Value))
        params.Add(New SqlParameter("@Impacted_Dept", DdlImpactDept.SelectedItem.Value))
        params.Add(New SqlParameter("@Leading_Dept", DdlLeadingDept.SelectedItem.Value))
        params.Add(New SqlParameter("@SUL_ID", DdlSUL.SelectedItem.Value))
        params.Add(New SqlParameter("@SUL_Coach_ID", DdlSULCoach.SelectedItem.Value))
        params.Add(New SqlParameter("@SN_SIEL_ID", DdlSNSIEL.SelectedItem.Value))
        params.Add(New SqlParameter("@Proj_Mgr_ID", DdlPrjMgr.SelectedItem.Value))



        ExecuteProcedure("CMPDB_sp_InsertUpdate_tblStartups", params)
    End Sub

    Protected Sub chkSrch_CheckedChanged(sender As Object, e As EventArgs) Handles chkSrch.CheckedChanged
        If chkSrch.Checked Then
            divSrchExist.Attributes.Add("class", "ContainerOne SearchBox")
            divSrchExist.Attributes.Add("style", "display:block")
        End If
        If chkSrch.Checked = False Then
            divSrchExist.Attributes.Add("style", "display:none")
        End If
    End Sub

    Protected Sub BtnSrchExistingStartup_Click(sender As Object, e As EventArgs) Handles BtnSrchExistingStartup.Click

        Dim params As New List(Of SqlParameter)
        If DdlPlants.SelectedValue > 0 Then
            params.Add(New SqlParameter("@plant", DdlPlants.SelectedValue))
        Else
            params.Add(New SqlParameter("@plant", DBNull.Value))
        End If

        If ddlSrchProjectType.SelectedItem.Value <> "0" Then
            params.Add(New SqlParameter("@Project_Type", ddlSrchProjectType.SelectedItem.Value))
        End If
        If ddlSrchChangeType.SelectedItem.Value <> "0" Then
            params.Add(New SqlParameter("@Change_Type", ddlSrchChangeType.SelectedItem.Value))
        End If
        If ddlSrchProductionLine.SelectedItem.Value <> "0" Then
            params.Add(New SqlParameter("@Production_Line", ddlSrchProductionLine.SelectedItem.Value))
        End If
        If ddlSrchBusinessUnit.SelectedItem.Value <> "0" Then
            params.Add(New SqlParameter("@Business_Unit", ddlSrchBusinessUnit.SelectedItem.Value))
        End If
        params.Add(New SqlParameter("@gridtype", "A"))
        Dim dt As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_GetSearchProjectAdminnew", params)
        If dt.Rows.Count > 0 Then
            gdvSrch.DataSource = dt
            gdvSrch.DataBind()
            'gdvSrch.Columns(5).Visible = False
            'gdvSrch.Columns(6).Visible = False
            'gdvSrch.Columns(7).Visible = False
            'gdvSrch.Columns(8).Visible = False
            'gdvSrch.Columns(9).Visible = False
        Else
            ShowGridHeader()

        End If
    End Sub
    Private Sub ShowGridHeader()
        gdvSrch.DataSource = New List(Of String)
        gdvSrch.DataBind()

        gdvSrch.ShowHeaderWhenEmpty = True
    End Sub

    Protected Sub gdvSrch_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "EditDetails" Then
            Dim Startup_ID = Integer.Parse(e.CommandArgument.ToString())
            HighlightRow(gdvSrch, e)
            Dim params As New List(Of SqlParameter)

            params.Add(New SqlParameter("@Startup_ID", Startup_ID))
            Dim dt As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_Search_tblStartups", params)
            If dt.Rows.Count > 0 Then
                ViewState("Startup_ID") = dt.Rows(0)("Startup_ID").ToString
                txtProjectName.Text = dt.Rows(0)("Project_Name").ToString
                txtstartupName.Text = dt.Rows(0)("Startup_Name").ToString
                DdlPlantsInsert.SelectedValue = dt.Rows(0)("Plant").ToString
                DdlProjectType.SelectedValue = dt.Rows(0)("Project_Type").ToString
                DdlChangeType.SelectedValue = dt.Rows(0)("Change_Type").ToString
                DdlCOmplexityofStartup.SelectedValue = dt.Rows(0)("Complexity_of_Startup").ToString

                DdlLinktoCBn.SelectedValue = dt.Rows(0)("Link_to_CBN").ToString
                DdlPriority.SelectedValue = dt.Rows(0)("Priority").ToString
                DdlBusinessUnit.SelectedValue = dt.Rows(0)("Business_Unit_ID").ToString
                DdlProductionLine.SelectedValue = dt.Rows(0)("Production_Line").ToString
                DdlImpactDept.SelectedValue = dt.Rows(0)("Impacted_Dept").ToString
                DdlLeadingDept.SelectedValue = dt.Rows(0)("Leading_Dept").ToString
                DdlSUL.SelectedValue = dt.Rows(0)("SUL_ID").ToString
                DdlSULCoach.SelectedValue = dt.Rows(0)("SUL_Coach_ID").ToString
                DdlSNSIEL.SelectedValue = dt.Rows(0)("SN_SIEL_ID").ToString
                DdlPrjMgr.SelectedValue = dt.Rows(0)("Proj_Mgr_ID").ToString

                cblGSUM.Items(0).Attributes.Add("style", "display:none")
                cblGSUM.Items(1).Attributes.Add("style", "display:none")
                cblGSUM.Items(2).Attributes.Add("style", "display:none")
                cblGSUM.Items(3).Attributes.Add("style", "display:none")
                cblGSUM.Items(4).Attributes.Add("style", "display:none")
                cblGSUM.Items(5).Attributes.Add("style", "display:none")
                cblGSUM.Items(6).Attributes.Add("style", "display:none")
                cblGSUM.Items(7).Attributes.Add("style", "display:none")
                'cblGSUM.Items(8).Attributes.Add("style", "display:none")


                If dt.Rows(0)("In_All_Change").ToString() = "True" Then
                    rbYes.Checked = True
                    rbNo.Checked = False
                Else
                    rbYes.Checked = False
                    rbNo.Checked = True
                End If

                If dt.Rows(0)("GSUM_CBAs").ToString() = "0" Then
                    rbCustom.Checked = True
                    cblGSUM.Items(0).Attributes.Add("style", "display:block")
                    cblGSUM.Items(1).Attributes.Add("style", "display:block")
                    cblGSUM.Items(2).Attributes.Add("style", "display:block")
                    cblGSUM.Items(3).Attributes.Add("style", "display:block")
                    cblGSUM.Items(4).Attributes.Add("style", "display:block")
                    cblGSUM.Items(5).Attributes.Add("style", "display:block")
                    cblGSUM.Items(6).Attributes.Add("style", "display:block")
                    cblGSUM.Items(7).Attributes.Add("style", "display:block")
                    'cblGSUM.Items(8).Attributes.Add("style", "display:block")

                ElseIf dt.Rows(0)("GSUM_CBAs").ToString() = "1" Then
                    rbSmall.Checked = True
                    cblGSUM.Items(1).Attributes.Add("style", "display:block")

                ElseIf dt.Rows(0)("GSUM_CBAs").ToString() = "2" Then
                    rbLarge.Checked = True
                    cblGSUM.Items(0).Attributes.Add("style", "display:block")
                    cblGSUM.Items(1).Attributes.Add("style", "display:none")
                    cblGSUM.Items(2).Attributes.Add("style", "display:block")
                    cblGSUM.Items(3).Attributes.Add("style", "display:block")
                    cblGSUM.Items(4).Attributes.Add("style", "display:block")
                    cblGSUM.Items(5).Attributes.Add("style", "display:block")
                    cblGSUM.Items(6).Attributes.Add("style", "display:block")
                    cblGSUM.Items(7).Attributes.Add("style", "display:block")
                    'cblGSUM.Items(8).Attributes.Add("style", "display:block")

                End If
                cblGSUM.ClearSelection()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim j As Int16 = Convert.ToInt32(dt.Rows(i)("SWP_Tool_Name_ID"))
                    cblGSUM.Items.FindByValue(j).Selected = True
                Next
            End If
            Dim list As KeyValuePair(Of String, Integer) = New KeyValuePair(Of String, Integer)(txtstartupName.Text, Startup_ID)
            Session("projectID") = list
            btnMileStone.Enabled = True
            btnSave.Text = "Update"
        End If
    End Sub

    Protected Sub gdvSrch_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gdvSrch.SelectedIndexChanging
        Dim row As GridViewRow = gdvSrch.Rows(e.NewSelectedIndex)

        Dim Startup_ID As Int32 = row.Cells(11).Text
        Dim params As New List(Of SqlParameter)

        params.Add(New SqlParameter("@Startup_ID", Startup_ID))


    End Sub

    Private Sub SetTools()
        Dim dt = New DataTable

        dt = GetDataTableFromSQL("select top(8) swp_tool_name name,swp_Tool_Name_id id from CMPDB_tblSWP_Tool_Names")
        If dt.Rows.Count > 0 Then

            Dim list As List(Of KeyValuePair(Of String, Integer)) =
            New List(Of KeyValuePair(Of String, Integer))
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(0)("name").ToString(), dt.Rows(0)("id").ToString()))
            'chkIAP.Text = dt.Rows(0)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(0)("name").ToString(), dt.Rows(0)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(1)("name").ToString(), dt.Rows(1)("id").ToString()))
            'chkGsumSmall.Text = dt.Rows(1)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(1)("name").ToString(), dt.Rows(1)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(2)("name").ToString(), dt.Rows(2)("id").ToString()))
            'chkGsumMaterial.Text = dt.Rows(2)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(2)("name").ToString(), dt.Rows(2)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(3)("name").ToString(), dt.Rows(3)("id").ToString()))
            'chkGsumPRRiskAssessment.Text = dt.Rows(3)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(3)("name").ToString(), dt.Rows(3)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(4)("name").ToString(), dt.Rows(4)("id").ToString()))
            'chkGSUMmfgOperations.Text = dt.Rows(4)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(4)("name").ToString(), dt.Rows(4)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(5)("name").ToString(), dt.Rows(5)("id").ToString()))
            'chkMfgReadinessDeliverables.Text = dt.Rows(5)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(5)("name").ToString(), dt.Rows(5)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(6)("name").ToString(), dt.Rows(6)("id").ToString()))
            'chkEWPTracker.Text = dt.Rows(6)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(6)("name").ToString(), dt.Rows(6)("id").ToString()))

            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(7)("name").ToString(), dt.Rows(7)("id").ToString()))
            'chkREImplementationPlan.Text = dt.Rows(7)("name").ToString()
            cblGSUM.Items.Add(New ListItem(dt.Rows(7)("name").ToString(), dt.Rows(7)("id").ToString()))

            'list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(8)("name").ToString(), dt.Rows(8)("id").ToString()))
            'ChkTest.Text = dt.Rows(8)("name").ToString()
            'cblGSUM.Items.Add(New ListItem(dt.Rows(8)("name").ToString(), dt.Rows(8)("id").ToString()))
            Session("ToolList") = list

        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If DdlPlantsInsert.SelectedItem.Value = 0 Then
            MessageBox("Please select Plant !!")
            DdlPlantsInsert.Focus()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select plant');</script>", False)
            Exit Sub
        End If

        If txtstartupName.Text = "" Then
            MessageBox("Please Enter StartupName !!")
            txtstartupName.Focus()
            Exit Sub
        End If

        If DdlCOmplexityofStartup.SelectedValue = "0" Then
            MessageBox("Please select Complexity of StartUp !!")
            DdlCOmplexityofStartup.Focus()
            Exit Sub
        End If

        If DdlProjectType.SelectedValue = "0" Then
            MessageBox("Please Select ProjectType !!")
            DdlProjectType.Focus()
            Exit Sub
        End If

        If DdlChangeType.SelectedValue = "0" Then
            MessageBox("Please Select the Changetype !!")
            DdlChangeType.Focus()
            Exit Sub
        End If

        If DdlBusinessUnit.SelectedValue = "0" Then
            MessageBox("Please Select Business Unit !!")
            DdlBusinessUnit.Focus()
            Exit Sub
        End If

        If DdlProductionLine.SelectedValue = "0" Then
            MessageBox("Please Select Production Line !!")
            DdlProductionLine.Focus()
            Exit Sub
        End If

        If DdlSUL.SelectedValue = "0" Then
            MessageBox("Please Select  SUL !!")
            DdlSUL.Focus()
            Exit Sub
        End If
        If btnSave.Text = "Save" Then
            AddUpdateRecordsStartUpProject("I")
            AddUpdateRecordsGSUMCBAProject("I")
            MessageBox("Successfully Saved !!")

        Else
            AddUpdateRecordsStartUpProject("U")
            AddUpdateRecordsGSUMCBAProject("U")
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            MessageBox("Successfully Updated !!")
        End If
        cblGSUM.ClearSelection()
        ResetControls(DdlPlantsInsert, DdlBusinessUnit, DdlCOmplexityofStartup, DdlProjectType, DdlChangeType, DdlLinktoCBn, DdlPriority, ddlSrchBusinessUnit, DdlProductionLine, DdlImpactDept, DdlLeadingDept, DdlSUL, DdlSULCoach, DdlSNSIEL, DdlPrjMgr)
        ResetControls(txtstartupName, txtProjectName)


    End Sub

    Public Sub AddUpdateRecordsStartUpProject(opr As String)
        Try


            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@Site_ID", DdlPlantsInsert.SelectedValue))
            params.Add(New SqlParameter("@Startup_Name", txtstartupName.Text))
            params.Add(New SqlParameter("@Project_Name", txtProjectName.Text))

            If rbYes.Checked Then
                params.Add(New SqlParameter("@In_All_Change", 1))
            Else
                params.Add(New SqlParameter("@In_All_Change", 0))
            End If

            params.Add(New SqlParameter("@Complexity_of_Startup", DdlCOmplexityofStartup.SelectedValue))

            If rbCustom.Checked Then
                params.Add(New SqlParameter("@GSUM_CBAs", 0))
            ElseIf rbSmall.Checked Then
                params.Add(New SqlParameter("@GSUM_CBAs", 1))
            Else
                params.Add(New SqlParameter("@GSUM_CBAs", 2))
            End If

            params.Add(New SqlParameter("@Project_Type", DdlProjectType.SelectedValue))
            params.Add(New SqlParameter("@Change_Type", DdlChangeType.SelectedValue))
            params.Add(New SqlParameter("@Link_to_CBN", DdlLinktoCBn.SelectedValue))
            params.Add(New SqlParameter("@Priority", DdlPriority.SelectedValue))
            params.Add(New SqlParameter("@Business_Unit_ID", DdlBusinessUnit.SelectedValue))
            params.Add(New SqlParameter("@Production_Line", DdlProductionLine.SelectedValue))
            params.Add(New SqlParameter("@Impacted_Dept", DdlImpactDept.SelectedValue))
            params.Add(New SqlParameter("@Leading_Dept", DdlLeadingDept.SelectedValue))
            params.Add(New SqlParameter("@SUL_ID", DdlSUL.SelectedValue))
            params.Add(New SqlParameter("@SUL_Coach_ID", DdlSULCoach.SelectedValue))
            params.Add(New SqlParameter("@SN_SIEL_ID", DdlSNSIEL.SelectedValue))
            params.Add(New SqlParameter("@Proj_Mgr_ID", DdlPrjMgr.SelectedValue))
            params.Add(New SqlParameter("@Operation", opr))

            If opr.Equals("U") Then
                params.Add(New SqlParameter("@Startup_ID", ViewState("Startup_ID")))
            End If

            ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPDB_tblStartups_New ", params)
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Public Sub AddUpdateRecordsGSUMCBAProject(opr As String)
        Try
            Dim IAPOUT As Int32 = 0
            Dim params As New List(Of SqlParameter)

            Dim dbCmd As SqlCommand = New SqlCommand()

            If opr.Equals("U") Then
                params.Add(New SqlParameter("@Operation", "U"))
                params.Add(New SqlParameter("@UpdateStartup_ID", ViewState("Startup_ID")))
                ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPDB_tblStartupsFiles", params)
                params.Clear()
            End If

            For Each item As ListItem In cblGSUM.Items.Cast(Of ListItem)().Where(Function(x) x.Selected)
                If item.Selected Then
                    Dim con As New SqlConnection(strConnectionString)
                    con.Open()
                    dbCmd.Connection = con
                    dbCmd.CommandText = "CMPDB_sp_InsertNUpdate_CMPDB_tblStartupsFiles"
                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandTimeout = 6000
                    Dim sqlDa As SqlDataAdapter = New SqlDataAdapter(dbCmd)

                    dbCmd.Parameters.Add("@SWP_Tool_Name_ID", SqlDbType.Int, ParameterDirection.Input).Value = item.Value
                    dbCmd.Parameters.Add("@Plant_ID", SqlDbType.Int, ParameterDirection.Input).Value = DdlPlantsInsert.SelectedValue
                    dbCmd.Parameters.Add("@Operation", SqlDbType.VarChar, 1, ParameterDirection.Input).Value = "ID"
                    dbCmd.Parameters.Add("@UpdateStartup_ID", SqlDbType.Int, ParameterDirection.Input).Value = ViewState("Startup_ID")

                    If Not IAPOUT = 0 Then
                        dbCmd.Parameters.Add("@IAPIN", SqlDbType.Int, ParameterDirection.Input).Value = IAPOUT
                    Else
                        dbCmd.Parameters.Add("@IAPIN", SqlDbType.Int, ParameterDirection.Input).Value = 0
                    End If
                    dbCmd.Parameters.Add("@IAPOUT", SqlDbType.Int)
                    dbCmd.Parameters("@IAPOUT").Direction = ParameterDirection.Output

                    Dim dsQuery As New DataSet()
                    dbCmd.ExecuteNonQuery()
                    con.Close()
                    If Not dbCmd.Parameters("@IAPOUT").Value = 0 Then
                        IAPOUT = Convert.ToInt32(dbCmd.Parameters("@IAPOUT").Value)
                    End If

                    dbCmd.Parameters.Clear()

                End If
            Next
        Catch ex As Exception
            Logger.Error(ex)
            HttpContext.Current.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Private Sub InitialRadioButton()
        cblGSUM.Items(0).Selected = True
        cblGSUM.Items(1).Selected = True
        cblGSUM.Items(2).Selected = True
        cblGSUM.Items(3).Selected = True
        cblGSUM.Items(4).Selected = True
        cblGSUM.Items(5).Selected = True
        cblGSUM.Items(6).Selected = True
        cblGSUM.Items(7).Selected = True
        'cblGSUM.Items(8).Selected = True
    End Sub


    Protected Sub chkAdvancedMode_CheckedChanged(sender As Object, e As EventArgs)

        If chkAdvancedMode.Checked = False Then
            DdlLinktoCBn.Visible = False
            DdlPriority.Visible = False
            DdlImpactDept.Visible = False
            DdlLeadingDept.Visible = False
            DdlSULCoach.Visible = False
            DdlSNSIEL.Visible = False
            DdlPrjMgr.Visible = False
            spanltoCBN.Visible = False
            spanltoPriority.Visible = False
            spanimpactDept.Visible = False
            spanleadingDept.Visible = False
            spansulCoach.Visible = False
            spansnSiel.Visible = False
            spanprjMgr.Visible = False
            'BtnQSulCoach.Visible = False
            'BTnQSnSiel.Visible = False
            'BtnQPrjMgr.Visible = False
            'BtnplusSulCoach.Visible = False
            'BTnplusSnSiel.Visible = False
            'BtnplusPrjMgr.Visible = False

        Else
            DdlLinktoCBn.Visible = True
            DdlPriority.Visible = True
            DdlImpactDept.Visible = True
            DdlLeadingDept.Visible = True
            DdlSULCoach.Visible = True
            DdlSNSIEL.Visible = True
            DdlPrjMgr.Visible = True
            spanltoCBN.Visible = True
            spanltoPriority.Visible = True
            spanimpactDept.Visible = True
            spanleadingDept.Visible = True
            spansulCoach.Visible = True
            spansnSiel.Visible = True
            spanprjMgr.Visible = True
            'BtnQSulCoach.Visible = True
            'BTnQSnSiel.Visible = True
            'BtnQPrjMgr.Visible = True
            'BtnplusSulCoach.Visible = True
            'BTnplusSnSiel.Visible = True
            'BtnplusPrjMgr.Visible = True

        End If


    End Sub

    Protected Sub btnSerach_Click(sender As Object, e As EventArgs)
        If divSrchExist.Attributes("style") = "display:none;" Then
            divSrchExist.Attributes.Add("style", "display:block;")
            If Session("plant_Set") <> "" Then
                PopulateDD(DdlPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")
                DdlPlants.SelectedValue = Session("plant_Set")
            End If
        ElseIf divSrchExist.Attributes("style") = "display:block;" Then
            divSrchExist.Attributes.Add("style", "display:none;")
            If Session("plant_Set") <> "" Then
                PopulateDD(DdlPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")
                DdlPlants.SelectedValue = Session("plant_Set")
            End If
        End If
        DdlPlants_SelectedIndexChanged(sender, EventArgs.Empty)
    End Sub

    Protected Sub DdlPlants_SelectedIndexChanged(sender As Object, e As EventArgs)
        populateProductionLines()
    End Sub

    Protected Sub ddlSrchBusinessUnit_SelectedIndexChanged(sender As Object, e As EventArgs)
        populateProductionLines()
    End Sub

    Protected Sub btnMileStone_Click(sender As Object, e As EventArgs)
        If Session("projectID") Is Nothing Then
            Exit Sub
        End If
        Response.Redirect("Startupstargets.aspx?pageId=2")
    End Sub

    Protected Sub DdlPlantsInsert_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim SULConfigValues = {"CMPDB_tblPractitioner", "Practitioner_ID", "Email", "Plant_ID", DdlPlantsInsert.SelectedValue}
        PopulateDD(DdlSUL, SULConfigValues(0), SULConfigValues(1), SULConfigValues(2), SULConfigValues(3), SULConfigValues(4))
        PopulateDD(DdlSULCoach, SULConfigValues(0), SULConfigValues(1), SULConfigValues(2), SULConfigValues(3), SULConfigValues(4))
        PopulateDD(DdlSNSIEL, SULConfigValues(0), SULConfigValues(1), SULConfigValues(2), SULConfigValues(3), SULConfigValues(4))
        PopulateDD(DdlPrjMgr, SULConfigValues(0), SULConfigValues(1), SULConfigValues(2), SULConfigValues(3), SULConfigValues(4))
        PopulateDD(DdlProductionLine, "CMPDB_tblSite_Production_Lines", "Site_Production_Line_ID", "Site_Production_Line_Name", "Site_ID", SULConfigValues(4))

    End Sub

    Protected Sub btnplusSUL_Click(sender As Object, e As EventArgs)
        Response.Redirect("Practitioner.aspx")
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As ImageClickEventArgs)
        Dim SULConfigValues = {"CMPDB_tblPractitioner", "Practitioner_ID", "Email", "Plant_ID", DdlPlantsInsert.SelectedValue}
        PopulateDD(DdlSUL, SULConfigValues(0), SULConfigValues(1), SULConfigValues(2), SULConfigValues(3), SULConfigValues(4))

        Dim SULID = GetSingleValue("select max(Practitioner_ID) from CMPDB_tblPractitioner")
        DdlSUL.SelectedValue = SULID
    End Sub

    Private Sub gdvSrch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gdvSrch.SelectedIndexChanged

    End Sub
End Class