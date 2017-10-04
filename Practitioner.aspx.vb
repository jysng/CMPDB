Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class Practicioners
    Inherits System.Web.UI.Page

#Region "Common"
    Private Sub LoadAllDependents(type As String)
        If type = 1 Then
            populateDepartments(ddlPlant.SelectedItem.Value, "1")
            populateBusinessUnit(ddlPlant.SelectedItem.Value, "1")
        Else
            populateDepartments(ddlSearchPlants.SelectedItem.Value, "2")
            populateBusinessUnit(ddlSearchPlants.SelectedItem.Value, "2")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()

        If Not IsPostBack Then
            PopulateDD(ddlPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
            PopulateDD(ddlSearchPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")

            PopulateDD(ddlTechCoachEmail, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
            PopulateDD(ddlQualifier, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
            txtDateAdded.Text = Now.ToString("MM/dd/yyyy")

            '  PopulateDD(ddlAddPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
            populateSWP()
            populateQualificationRoles()

            ' PopulateDD(ddlSearchDept, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department")
            ' PopulateDD(ddlBu, "CMPDB_tblBusiness_Unit", "Business_Unit_ID", "Business_Unit")
            ' PopulateDD(ddlSearchBU, "CMPDB_tblBusiness_Unit", "Business_Unit_ID", "Business_Unit")
            PopulateDD(ddlSearchSWP, xTblNameSWP, "SWP_ID", "SWP")
            ShowGridHeader()
            If Session("plant_Set") <> "" Then
                PopulateDD(ddlPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
                ddlPlant.SelectedValue = Session("plant_Set")
                ddlPlant_SelectedIndexChanged(sender, EventArgs.Empty)
            End If
        End If
    End Sub
#End Region

#Region "Populates"
    Private Sub populateDepartments(key As String, type As String)
        If type = "1" Then
            PopulateDD(ddlDepartment, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department", "Site_ID", key)
            ' PopulateDD(ddlDepartment, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department")
        Else
            PopulateDD(ddlSearchDept, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department", "Site_ID", key)
            '  PopulateDD(ddlSearchDept, "CMPDB_tblSite_Departments", "Site_Department_ID", "Site_Department")
        End If
    End Sub

    Private Sub populateBusinessUnit(key As String, type As String)
        If type = "1" Then
            PopulateDD(ddlBu, "CMPDB_vwBu_Dependent_Plants", "Business_Unit_ID", "Business_Unit", "Plant_ID", key)
        Else
            PopulateDD(ddlSearchBU, "CMPDB_vwBu_Dependent_Plants", "Business_Unit_ID", "Business_Unit", "Plant_ID", key)
        End If
    End Sub

#End Region
#Region "Variables"
    Dim xTblNameQualification_Level = "CMPDB_tblQualification_Level"
    Dim xtblSWPToolName = "CMPDB_tblSWP_Tool_Names"
    Dim xTblNameSWP = "CMPDB_tblSWP"
    Dim xTblBLOBFiles = "CMPDB_tblBLOBFiles"
    'Dim xSuffixDomain = "@pg.com"


#End Region


    Private Sub ShowGridHeader()
        gridPractitioner.DataSource = New List(Of String)
        gridPractitioner.DataBind()
        Session("SortTable") = vbNull
        gridPractitioner.ShowHeaderWhenEmpty = True
    End Sub
    Private Sub populateQualificationRoles()
        PopulateDD(ddlQualificationLevel, xTblNameQualification_Level, "Qualification_Level_ID", "Qualification_Level")

    End Sub

    Protected Sub btnSerach_Click(sender As Object, e As EventArgs)
        'divSrchExist.Attributes.Add("class", "ContainerOne SearchBox")
        'divSrchExist.Attributes.Add("style", "display:block")
        If divSrchExist.Attributes("style") = "display: none;" Then
            divSrchExist.Attributes.Add("style", "display: block;")
            If Session("plant_Set") <> "" Then
                PopulateDD(ddlSearchPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")
                ddlSearchPlants.SelectedValue = Session("plant_Set")
            End If
        ElseIf divSrchExist.Attributes("style") = "display: block;" Then
            divSrchExist.Attributes.Add("style", "display: none;")
            If Session("plant_Set") <> "" Then
                PopulateDD(ddlSearchPlants, "CMPDB_tblPlants", "Plant_ID", "Plant")
                ddlSearchPlants.SelectedValue = Session("plant_Set")
            End If
        End If
        ddlSearchPlants_SelectedIndexChanged(sender, EventArgs.Empty)
        UserNameLabel.Text = "Add Practitioner"
    End Sub


    Public Sub populatePractice(key As String)
        PopulateDD(ddlSWPRole, "CMPDB_tblPractitioner_Roles", "Practitioner_Role_ID", "Practitioner_Role", "SWP_ID", ddlSWp.SelectedItem.Value)
    End Sub

    Protected Sub ddlSWp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSWp.SelectedIndexChanged
        populatePractice(ddlSWp.SelectedValue)
        ''FilteredGrid()
    End Sub
    Dim check = 0

    Protected Sub ddlPlant_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadAllDependents("1")
    End Sub

    Protected Sub ddlSearchPlants_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadAllDependents("2")
    End Sub
    Protected Sub ddlBu_SelectedIndexChanged(sender As Object, e As EventArgs)
        FilteredGrid()
    End Sub
    Public Sub populateSWP()
        PopulateDD(ddlSWp, xTblNameSWP, "SWP_ID", "SWP")
    End Sub

    Protected Sub BtnSrchExistingPractitioner_Click(sender As Object, e As EventArgs) Handles BtnSrchExistingStartup.Click
        FilteredGrid()
    End Sub
    Public Sub FilteredGrid()
        Dim dt = New DataTable
        Dim params As New List(Of SqlParameter)

        params.Add(New SqlParameter("@plant", ddlSearchPlants.SelectedValue))
        params.Add(New SqlParameter("@bu", ddlSearchBU.SelectedValue))
        params.Add(New SqlParameter("@region", DBNull.Value))
        params.Add(New SqlParameter("@department", ddlSearchDept.SelectedValue))
        params.Add(New SqlParameter("@swp", ddlSearchSWP.SelectedValue))

        'If (ddlSearchPlants.SelectedValue > 0) Then
        '    params.Add(New SqlParameter("@plant", ddlSearchPlants.SelectedValue))
        '    check = 1

        'Else
        '    params.Add(New SqlParameter("@plant", DBNull.Value))

        'End If
        'If (ddlSearchBU.SelectedValue > 0) Then
        '    params.Add(New SqlParameter("@bu", ddlSearchBU.SelectedValue))
        '    check = 1
        'Else
        '    params.Add(New SqlParameter("@bu", DBNull.Value))
        'End If

        'params.Add(New SqlParameter("@region", DBNull.Value))

        'If (ddlSearchDept.SelectedValue > 0) Then
        '    params.Add(New SqlParameter("@department", ddlSearchDept.SelectedValue))
        '    check = 1
        'Else
        '    params.Add(New SqlParameter("@department", DBNull.Value))
        'End If
        'If (ddlSearchSWP.SelectedValue > 0) Then
        '    params.Add(New SqlParameter("@swp", ddlSearchSWP.SelectedValue))
        '    check = 1
        'Else
        '    params.Add(New SqlParameter("@swp", DBNull.Value))
        'End If



        dt = ExecuteProcedureForDataTable("CMPDB_sp_ShowPractitioner", params)
        If dt.Rows.Count > 0 Then
            Session("SortTable") = dt
            gridPractitioner.DataSource = Session("SortTable")
            gridPractitioner.DataBind()
        Else
            gridPractitioner.DataSource = New List(Of String)
            gridPractitioner.DataBind()
            Session("SortTable") = vbNull
            gridPractitioner.ShowHeaderWhenEmpty = True

        End If


    End Sub

    'Public Sub Practitioner()
    '    Dim dt = New DataTable
    '    Dim params As New List(Of SqlParameter)
    '    params.Add(New SqlParameter("@typegrid", "N"))

    '    params.Add(New SqlParameter("@plant", "0"))
    '    params.Add(New SqlParameter("@bu", "0"))
    '    params.Add(New SqlParameter("@region", "0"))
    '    params.Add(New SqlParameter("@department", "0"))
    '    params.Add(New SqlParameter("@swp", "0"))


    '    dt = ExecuteProcedureForDataTable("CMPDB_sp_ShowPractitioner", params)
    '    If dt.Rows.Count > 0 Then
    '        Session("SortTable") = dt
    '        gridPractitioner.DataSource = Session("SortTable")

    '        gridPractitioner.DataBind()
    '    Else

    '        gridPractitioner.DataSource = New List(Of String)
    '        gridPractitioner.DataBind()
    '        Session("SortTable") = vbNull
    '        gridPractitioner.ShowHeaderWhenEmpty = True
    '    End If


    'End Sub

    Public Sub getdata(Editid As String)
        Dim dt = New DataTable

        dt = GetDataTableFromSQL("select QL.Qualification_Level,Practitioner_ID,plant.plant_id,p.Plant_Department,p.Business_Unit,r.Region,p.Region_ID,p.Department,convert(varchar(10), cast(DateAdded as date), 101)DateAdded,p.Email,swp.swp_ID,p.swp_role,pr.Practitioner_Role,convert(varchar(10), cast(QualificationDate as date), 101)QualificationDate,p.Qualifier,convert(varchar(10), cast(TargetedDate as date), 101)TargetedDate,convert(varchar(10), cast(ClassCompletedDate as date), 101)ClassCompletedDate,p.TechCoachEmail,p.Comments,p.FirstName,p.LastName,QL.Qualification_Level_ID  from CMPDB_tblPractitioner as P left join CMPDB_tblBusiness_Unit bU on bu.Business_Unit_ID=p.Business_Unit left  join CMPDB_tblPlants as plant on plant.Plant_ID=p.Plant_ID left join CMPDB_tblSite_Departments as dept on dept.Site_Department_ID=p.Department inner join CMPDB_tblSWP as swp on swp.SWP_ID=p.SWP_ID left join CMPDB_tblRegion r on r.Region_ID=p.Region_ID inner join CMPDB_tblPractitioner_Roles as pr on pr.Practitioner_Role_ID=p.SWP_Role left join CMPDB_tblQualification_Level QL ON QL.Qualification_Level_ID=P.Qualification_Level where Practitioner_id=" + Editid + "")
        If dt.Rows.Count > 0 Then
            ddlPlant.SelectedValue = dt.Rows(0)("plant_id").ToString()
            populateBusinessUnit(dt.Rows(0)("plant_id").ToString(), "1")
            ddlBu.SelectedValue = dt.Rows(0)("Business_Unit").ToString()
            ' ddlRegion.SelectedValue = dt.Rows(0)("Region_ID").ToString()
            ' ddlAddPlant.SelectedValue = dt.Rows(0)("plant_id").ToString()
            populateDepartments(dt.Rows(0)("plant_id").ToString(), "1")
            ddlDepartment.SelectedValue = dt.Rows(0)("Department").ToString()
            ddlSWp.SelectedValue = dt.Rows(0)("swp_ID").ToString()
            txtDateAdded.Text = dt.Rows(0)("DateAdded").ToString()
            txtInsertEmail.Text = dt.Rows(0)("Email").ToString().Replace("@pg.com", "")
            txtFirstName.Text = dt.Rows(0)("FirstName").ToString()
            txtLastName.Text = dt.Rows(0)("LastName").ToString()
            populatePractice(dt.Rows(0)("swp_ID").ToString())
            ' ddlSWp_SelectedIndexChanged(New Object, EventArgs.Empty)

            '   populatePractice(dt.Rows(0)("swp_ID").ToString())
            ddlSWPRole.SelectedValue = dt.Rows(0)("swp_role").ToString()
            txtQualificationDate.Text = dt.Rows(0)("QualificationDate").ToString()
            PopulateDD(ddlTechCoachEmail, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
            PopulateDD(ddlQualifier, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
            If (dt.Rows(0)("Qualifier") Is DBNull.Value) Then
                ddlQualifier.SelectedValue = 0
            Else
                ddlQualifier.SelectedValue = dt.Rows(0)("Qualifier").ToString()
            End If
            If (dt.Rows(0)("TechCoachEmail") Is DBNull.Value) Then
                ddlTechCoachEmail.SelectedValue = 0
            Else
                ddlTechCoachEmail.SelectedValue = dt.Rows(0)("TechCoachEmail").ToString()
            End If

            ddlQualificationLevel.SelectedValue = IIf(IsDBNull(dt.Rows(0)("Qualification_Level_ID")), "0", dt.Rows(0)("Qualification_Level_ID"))
            ' ddlQualifier.SelectedValue = dt.Rows(0)("Qualifier").ToString()
            txtTargetDate.Text = dt.Rows(0)("TargetedDate").ToString()
            txtClassCompletedDate.Text = dt.Rows(0)("ClassCompletedDate").ToString()
            txtComments.Text = dt.Rows(0)("comments").ToString()
            btnSaveRecords.Text = "Update"
        End If
    End Sub

    Protected Sub gridPractitioner_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        If e.CommandName = "EditDetails" Then
            PractitionerEditid = Integer.Parse(e.CommandArgument.ToString())
            UserNameLabel.Text = "Update Practitioner"
            HighlightRow(gridPractitioner, e)

            getdata(PractitionerEditid)
        End If
        If e.CommandName = "DeleteDetails" Then
            PractitionerEditid = Integer.Parse(e.CommandArgument.ToString())
            ExecuteProc("CMPDB_sp_Delete_Practitioner " + PractitionerEditid.ToString())
            FilteredGrid()
            MessageBox("Successfully Deleted !!")
        End If

    End Sub
    Protected Sub sellectAll(sender As Object, e As EventArgs)
        Dim addToList As New List(Of String)
        Dim ChkBoxHeader As CheckBox = DirectCast(gridPractitioner.HeaderRow.FindControl("chkb1"), CheckBox)
        For Each row As GridViewRow In gridPractitioner.Rows
            Dim ChkBoxRows As CheckBox = DirectCast(row.FindControl("chkSelMail"), CheckBox)
            Dim EmailText As LinkButton = DirectCast(row.FindControl("btnEdit"), LinkButton)
            If ChkBoxHeader.Checked = True Then
                ChkBoxRows.Checked = True

                If txtEmailTo.Text = "" Then
                    addToList.Add(EmailText.Text)
                    Session("MyList") = addToList
                    txtEmailTo.Text = Session("MyList")(0)
                Else
                    addToList.Add(txtEmailTo.Text + ";" + EmailText.Text)
                    Session("MyList") = addToList
                    txtEmailTo.Text = txtEmailTo.Text + ";" + EmailText.Text
                End If

            Else
                ChkBoxRows.Checked = False
                Dim s As String = txtEmailTo.Text
                txtEmailTo.Text = ""
            End If
        Next
    End Sub


    Protected Sub sellectOne(sender As Object, e As EventArgs)
        Session("MyList") = ""
        Dim addToList As New List(Of String)
        Dim chk = DirectCast(sender, CheckBox)
        Dim row = DirectCast(chk.NamingContainer, GridViewRow)
        Dim EmailText = TryCast(row.FindControl("btnEdit"), LinkButton)
        If chk.Checked = True Then


            If txtEmailTo.Text = "" Then
                addToList.Add(EmailText.Text)
                Session("MyList") = addToList
                txtEmailTo.Text = Session("MyList")(0)
            Else
                addToList.Add(txtEmailTo.Text + ";" + EmailText.Text)
                Session("MyList") = addToList
                txtEmailTo.Text = Session("MyList")(0)
            End If
        Else
            Dim s As String = txtEmailTo.Text
            txtEmailTo.Text = ""
            ' Split string based on spaces.
            Dim words As String() = s.Split(New Char() {";"c})

            ' Use For Each loop over words and display them.
            Dim word As String
            For Each word In words
                If EmailText.Text = word Then

                Else
                    Session("MyList") = word
                    txtEmailTo.Text = txtEmailTo.Text + ";" + Session("MyList")
                End If

            Next

            If txtEmailTo.Text.StartsWith(";") Then txtEmailTo.Text = txtEmailTo.Text.Substring(1)
            'Dim lblRequestType = TryCast(row.FindControl("lblRequestType"), Label)
            'Dim lblStatus = TryCast(row.FindControl("lblStatus"), Label)
        End If


    End Sub


    Private Property PractitionerEditid() As Integer
        Get
            Return CInt(ViewState("PractitionerEditid"))
        End Get
        Set
            ViewState("PractitionerEditid") = Value
        End Set
    End Property

    Protected Sub btnSaveRecords_Click(sender As Object, e As EventArgs)
        txtInsertEmail.Text = txtInsertEmail.Text.Replace("@pg.com", "")
        If ddlPlant.SelectedValue = "0" Then
            MessageBox("Please Select Plant !!")
            ddlPlant.Focus()
            Exit Sub
        End If
        If txtInsertEmail.Text = "" Then
            MessageBox("Please Enter Email !!")
            txtInsertEmail.Focus()
            Exit Sub
        End If
        If ddlSWp.SelectedValue = "0" Then
            MessageBox("Please Select SWP !!")
            ddlSWp.Focus()
            Exit Sub
        End If

        If ddlSWPRole.SelectedValue = "0" Then
            MessageBox("Please Select SWP Role !!")
            ddlSWPRole.Focus()
            Exit Sub
        End If


        Dim Str = ""


        Dim params As New List(Of SqlParameter)
        If btnSaveRecords.Text = "Save" Then
            params.Add(New SqlParameter("@Operation", "I"))
            Str = (GetSingleValue("select Email from CMPDB_tblPractitioner where Email='" + txtInsertEmail.Text + "@pg.com" + "'"))
        Else
            params.Add(New SqlParameter("@Operation", "U"))
            Str = (GetSingleValue("select Email from CMPDB_tblPractitioner where Email='" + txtInsertEmail.Text + "@pg.com" + "' and Practitioner_ID<>" + PractitionerEditid.ToString()))
        End If

        If Str <> "" Then
            MessageBox("Practitioner Already Exist !!")
            Exit Sub
        End If
        params.Add(New SqlParameter("@Practitioner_ID", PractitionerEditid))
        params.Add(New SqlParameter("@Plant_ID", ddlPlant.SelectedValue))
        params.Add(New SqlParameter("@Plant_Department", "Making"))
        params.Add(New SqlParameter("@Business_Unit", ddlBu.SelectedItem.Value))
        params.Add(New SqlParameter("@Department", ddlDepartment.SelectedValue))

        params.Add(New SqlParameter("@Email", txtInsertEmail.Text))
        params.Add(New SqlParameter("@SWP_ID", ddlSWp.SelectedValue))
        params.Add(New SqlParameter("@SWP_Role", ddlSWPRole.SelectedValue))
        params.Add(New SqlParameter("@Qualification_Level", ddlQualificationLevel.SelectedValue))
        params.Add(New SqlParameter("@Qualifier", ddlQualifier.SelectedValue))
        params.Add(New SqlParameter("@FirstName", txtFirstName.Text))
        params.Add(New SqlParameter("@LastName", txtLastName.Text))
        params.Add(New SqlParameter("@TechCoachEmail", ddlTechCoachEmail.SelectedValue))
        params.Add(New SqlParameter("@Comments", txtComments.Text))
        If txtDateAdded.Text = "" Then
            params.Add(New SqlParameter("@DateAdded", DBNull.Value))
        Else
            params.Add(New SqlParameter("@DateAdded", txtDateAdded.Text))
        End If
        If txtClassCompletedDate.Text = "" Then
            params.Add(New SqlParameter("@ClassCompletedDate", DBNull.Value))
        Else
            params.Add(New SqlParameter("@ClassCompletedDate", txtClassCompletedDate.Text))
        End If
        If txtQualificationDate.Text = "" Then
            params.Add(New SqlParameter("@QualificationDate", DBNull.Value))
        Else
            params.Add(New SqlParameter("@QualificationDate", txtQualificationDate.Text))
        End If
        If txtTargetDate.Text = "" Then
            params.Add(New SqlParameter("@TargetedDate", DBNull.Value))
        Else
            params.Add(New SqlParameter("@TargetedDate", txtTargetDate.Text))
        End If
        ExecuteProcedure("CMPDB_sp_InsertNUpdate_tblPractitioner", params)
        If btnSaveRecords.Text = "Save" Then
            MessageBox("Successfully Saved !!")
        Else
            MessageBox("Successfully Updated !!")
            btnSaveRecords.Text = "Save"
        End If
        PopulateDD(ddlTechCoachEmail, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
        PopulateDD(ddlQualifier, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
        ResetControls(ddlBu, ddlPlant, ddlDepartment, ddlSWp, ddlSWPRole, ddlQualificationLevel, ddlQualifier, ddlTechCoachEmail)
        ResetControls(txtInsertEmail, txtQualificationDate, txtTargetDate, txtClassCompletedDate, txtComments, txtFirstName, txtLastName, txtDateAdded)
        'Practitioner()
        FilteredGrid()
        UserNameLabel.Text = "Add Practitioner"
        Dim jScript = "<script>checkCookie();</script>"
        ScriptManager.RegisterStartupScript(Page, upnl.GetType(), "keyClientBlock", jScript, False)
    End Sub

    'Protected Sub btnSaveRecords_Click(sender As Object, e As EventArgs)
    '    txtInsertEmail.Text = txtInsertEmail.Text.Replace("@pg.com", "")
    '    If ddlPlant.SelectedValue = "0" Then
    '        MessageBox("Please Select Plant !!")
    '        ddlPlant.Focus()
    '        Exit Sub
    '    End If
    '    If txtInsertEmail.Text = "" Then
    '        MessageBox("Please Enter Email !!")
    '        txtInsertEmail.Focus()
    '        Exit Sub
    '    End If
    '    If ddlSWp.SelectedValue = "0" Then
    '        MessageBox("Please Select SWP !!")
    '        ddlSWp.Focus()
    '        Exit Sub
    '    End If

    '    If ddlSWPRole.SelectedValue = "0" Then
    '        MessageBox("Please Select SWP Role !!")
    '        ddlSWPRole.Focus()
    '        Exit Sub
    '    End If


    '    Dim Str = ""


    '    Dim params As New List(Of SqlParameter)
    '    If btnSaveRecords.Text = "Save" Then
    '        params.Add(New SqlParameter("@Operation", "I"))
    '        Str = (GetSingleValue("select Email from CMPDB_tblPractitioner where Email='" + txtInsertEmail.Text + "@pg.com" + "'"))
    '    Else
    '        params.Add(New SqlParameter("@Operation", "U"))
    '        Str = (GetSingleValue("select Email from CMPDB_tblPractitioner where Email='" + txtInsertEmail.Text + "@pg.com" + "' and Practitioner_ID<>" + PractitionerEditid.ToString()))
    '    End If

    '    If Str <> "" Then
    '        MessageBox("Practitioner Already Exist !!")
    '        Exit Sub

    '    End If


    '    params.Add(New SqlParameter("@Practitioner_ID", PractitionerEditid))
    '    params.Add(New SqlParameter("@Plant_ID", ddlPlant.SelectedValue))
    '    params.Add(New SqlParameter("@Plant_Department", "Making"))
    '    params.Add(New SqlParameter("@Business_Unit", ddlBu.SelectedItem.Value))
    '    params.Add(New SqlParameter("@Department", ddlDepartment.SelectedValue))
    '    If txtDateAdded.Text = "" Then
    '        params.Add(New SqlParameter("@DateAdded", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@DateAdded", txtDateAdded.Text))
    '    End If

    '    params.Add(New SqlParameter("@Email", txtInsertEmail.Text))
    '    params.Add(New SqlParameter("@SWP_ID", ddlSWp.SelectedValue))
    '    params.Add(New SqlParameter("@SWP_Role", ddlSWPRole.SelectedValue))
    '    If ddlQualificationLevel.SelectedValue = "0" Then
    '        params.Add(New SqlParameter("@Qualification_Level", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@Qualification_Level", ddlQualificationLevel.SelectedValue))
    '    End If
    '    If txtQualificationDate.Text = "" Then
    '        params.Add(New SqlParameter("@QualificationDate", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@QualificationDate", txtQualificationDate.Text))
    '    End If
    '    If ddlQualifier.SelectedValue = "0" Then
    '        params.Add(New SqlParameter("@Qualifier", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@Qualifier", ddlQualifier.SelectedValue))
    '    End If
    '    ' params.Add(New SqlParameter("@Qualifier", IIf(String.IsNullOrEmpty(txtQualifier.Text), "", txtQualifier.Text + xSuffixDomain)))
    '    If txtTargetDate.Text = "" Then
    '        params.Add(New SqlParameter("@TargetedDate", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@TargetedDate", txtTargetDate.Text))
    '    End If
    '    If txtClassCompletedDate.Text = "" Then
    '        params.Add(New SqlParameter("@ClassCompletedDate", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@ClassCompletedDate", txtClassCompletedDate.Text))
    '    End If
    '    params.Add(New SqlParameter("@FirstName", txtFirstName.Text))
    '    params.Add(New SqlParameter("@LastName", txtLastName.Text))
    '    If ddlTechCoachEmail.SelectedValue = "0" Then
    '        params.Add(New SqlParameter("@TechCoachEmail", DBNull.Value))
    '    Else
    '        params.Add(New SqlParameter("@TechCoachEmail", ddlTechCoachEmail.SelectedValue))
    '    End If
    '    '  params.Add(New SqlParameter("@TechCoachEmail", IIf(String.IsNullOrEmpty(txtTechCoachEmail.Text), "", txtTechCoachEmail.Text + xSuffixDomain)))
    '    params.Add(New SqlParameter("@Comments", txtComments.Text))
    '    ExecuteProcedure("CMPDB_sp_InsertNUpdate_tblPractitioner", params)
    '    If btnSaveRecords.Text = "Save" Then
    '        MessageBox("Successfully Saved !!")
    '    Else
    '        MessageBox("Successfully Updated !!")
    '        btnSaveRecords.Text = "Save"
    '    End If
    '    PopulateDD(ddlTechCoachEmail, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
    '    PopulateDD(ddlQualifier, "CMPDB_tblPractitioner", "Practitioner_ID", "Email")
    '    ResetControls(ddlBu, ddlPlant, ddlDepartment, ddlSWp, ddlSWPRole, ddlQualificationLevel, ddlQualifier, ddlTechCoachEmail)
    '    ResetControls(txtInsertEmail, txtQualificationDate, txtTargetDate, txtClassCompletedDate, txtComments)
    '    'Practitioner()
    '    FilteredGrid()
    '    UserNameLabel.Text = "Add Practitioner"
    '    Dim jScript = "<script>checkCookie();</script>"
    '    ScriptManager.RegisterStartupScript(Page, upnl.GetType(), "keyClientBlock", jScript, False)
    'End Sub
    Public Sub SendEmail(ToEmailID As String, CCEmailID As String, Subject As String, Body As String)
        Dim smtp_server As New SmtpClient
        Dim e_mail As New MailMessage
        smtp_server.UseDefaultCredentials = Convert.ToBoolean(GetParamsValue("pUseDefaultCredentials"))
        smtp_server.Port = GetParamsValue("pPortNumber")
        'smtp_server.Host = "smtpgw.pg.com"
        'e_mail.From = New MailAddress("msoforecast.im@pg.com")
        smtp_server.Host = GetParamsValue("pHostName")
        e_mail.From = New MailAddress(GetParamsValue("pFromEmail"))

        'If CCEmailID <> "" Then
        '    For Each a In CCEmailID.Split(New Char() {";"})
        '        e_mail.CC.Add(a)
        '    Next
        'End If

        For Each a In ToEmailID.Split(New Char() {";"})
            e_mail.To.Add(a)
        Next

        e_mail.Subject = Subject
        e_mail.IsBodyHtml = Convert.ToBoolean(GetParamsValue("pIsHTMLBody"))
        e_mail.Body = Body
        smtp_server.Send(e_mail)
    End Sub

    Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs)
        Dim list As New List(Of String)
        Dim i = 1
        Dim toEmail As String = ""
        For Each row In gridPractitioner.Rows
            Dim chk As Boolean = TryCast(row.FindControl("chkSelMail"), CheckBox).Checked
            If chk Then
                Dim seleEmail = TryCast(row.FindControl("btnEdit"), LinkButton).Text
                list.Add(seleEmail + ";")
                toEmail += seleEmail + ";"
            End If
        Next
        toEmail = toEmail.Remove(toEmail.Length - 1)
        SendEmail(toEmail, "", txtEmailSubject.Text, txtEmailBody.Text)
        MessageBox("Mail Send Successfully !!")
    End Sub

    'Protected Sub btnAddToList_Click(sender As Object, e As EventArgs)
    '    txtEmailTo.Text = ""
    '    ' Dim ChkBoxHeader As CheckBox = DirectCast(gridPractitioner.HeaderRow.FindControl("chkb1"), CheckBox)
    '    For Each row As GridViewRow In gridPractitioner.Rows
    '        Dim ChkBoxRows As CheckBox = DirectCast(row.FindControl("chkSelMail"), CheckBox)
    '        If ChkBoxRows.Checked = True Then
    '            Dim EmailText = TryCast(row.FindControl("lblGridEmail"), Label)
    '            If txtEmailTo.Text = "" Then
    '                txtEmailTo.Text = EmailText.Text

    '            Else
    '                txtEmailTo.Text += ";" + EmailText.Text
    '            End If
    '        End If
    '    Next
    'End Sub



    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs)
        FilteredGrid()
        ' FilteredGrid(ddlPlant.SelectedValue, ddlBu.SelectedValue, ddlRegion.SelectedValue, ddlDepartment.SelectedValue, ddlSWp.SelectedValue, ddlSWPRole.SelectedValue)
    End Sub

    Protected Sub ddlSWPRole_SelectedIndexChanged(sender As Object, e As EventArgs)
        FilteredGrid()
        '  FilteredGrid(ddlPlant.SelectedValue, ddlBu.SelectedValue, ddlRegion.SelectedValue, ddlDepartment.SelectedValue, ddlSWp.SelectedValue, ddlSWPRole.SelectedValue)
    End Sub

    Protected Sub gridPractitioner_Sorting(sender As Object, e As GridViewSortEventArgs)
        'Retrieve the table from the session object.
        Dim dt = TryCast(Session("SortTable"), DataTable)

        If dt IsNot Nothing Then

            'Sort the data.
            dt.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
            gridPractitioner.DataSource = Session("SortTable")
            gridPractitioner.DataBind()

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
End Class