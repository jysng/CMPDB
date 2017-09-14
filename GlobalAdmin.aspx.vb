Imports System.Data.SqlClient
Imports System.IO

Public Class GlobalAdmin
    Inherits System.Web.UI.Page
    Dim pages As Page

#Region "Common"

#Region "Variables"
    Dim xUpdateImagePath = "~/images/Update.png"
    Dim xAddImagePath = "~/images/Add.png"


#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            populateRegion("")
            populateRegion()
            populateProduction() 'DONE
            populateProject()
            populateSWP()
            'populatePractice()
            populateBusinessUnit()
            populateChangeType()
            'insert into [tblQualification_Level] values('QRole1')
            populateQualificationRoles()
            populatePSQualificationLevel()
            populatePracticionerRoleID()
            populateComplexity()
            populateSWP_ID()
            populateQualification()
            populatePlants()
            populatePlantAssociatedBusinessUnits()
            populateBusinessUnits()
            Session("Message") = "Yes"
        End If
    End Sub

    Private Sub FillTextBox(ByRef txt As TextBox, ByRef list As ListBox, ByRef btn As ImageButton)
        txt.Text = list.SelectedItem.Text
        btn.ImageUrl = xUpdateImagePath
    End Sub
#End Region

#Region "Populates"

    Private Sub populateRegion()
        PopulateDD(ListBoxRegions, xTblRegion, "Region_ID", "Region")
    End Sub

    Private Sub populatePlatform(key As String)
        PopulateDDWithSQL(ListBoxPlatform, "select  Platform, Platform_ID from " + xTblNamePlatforms + " where BU_ID='" + key + "'  ")
    End Sub

    Private Sub populateRegion(key As String)
        If key = "" Then
            PopulateDD(ddlRegionTypes, xTblRegion, "Region_ID", "Region")
        Else
            PopulateDD(ddlRegionTypes, xTblRegion, "Region_ID", "Region", "Plant_ID", ListBoxPlants.SelectedItem.Value)
        End If
    End Sub

    Private Sub populateChangeType()
        PopulateDD(ListBoxChangType, xTblNameChange_Types, "Change_Type_ID", "Change_Type")
    End Sub

    Private Sub populateBusinessUnit()
        PopulateDD(ListBoxBusinUnit, xTblNameBusiness_Unit, "Business_Unit_ID", "Business_Unit")
    End Sub

    Public Sub populateProject()
        PopulateDD(ListBoxprojType, xTblNameProject_Types, "Project_Type_ID", "Project_Type")
    End Sub

    Public Sub populateProduction()
        PopulateDD(ListBoxProductionType, xTblNameProduction_Types, "Production_Type_ID", "Production_Type")
    End Sub

    Public Sub populateSWP()
        PopulateDD(ListBoxSWP, xTblNameSWP, "SWP_ID", "SWP")
    End Sub

    Public Sub populatePractice(key As String)
        PopulateDDWithSQL(ListBoxPracticionerRole, "select  Practitioner_Role, Practitioner_Role_ID from " + xTblNamePractitioner_Roles + " where SWP_ID='" + key + "'")
    End Sub

    Private Sub populateQualificationRoles()
        'PopulateDD(DropDownListQLevel, xTblNameQualification_Level, "Qualification_Level_ID", "Qualification_Level")
    End Sub

    Private Sub populateComplexity()
        PopulateDD(ListBoxComplexitySituation, xTblcmpdb_ComplexityLevel, "Complexity_Situation_ID", "ComplexitySituation")
    End Sub

    Private Sub populatePSQualificationLevel()
        PopulateDD(ddlPSQualificationLevel, xTblNameQualification_Level, "Qualification_Level_ID", "Qualification_Level")
    End Sub

    Private Sub populateSWP_ID()
        '  PopulateDD(ddlSWP_ID, xTblNameSWP, "SWP_ID", "SWP")
    End Sub

    Private Sub populatePracticionerRoleID()
        PopulateDD(ddlPracticionerRoleID, xTblNamePractitioner_Roles, "Practitioner_Role_ID", "Practitioner_Role")
    End Sub


    Private Sub populateQualification()
        PopulateDD(ListBoxQualificationTypes, xTblQualification, "Qualification_Level_ID", "Qualification_Level")
    End Sub

    Private Sub populatePlants()
        PopulateDD(ListBoxPlants, xTblPlants, "Plant_ID", "Plant")
    End Sub

    Private Sub populatePlantAssociatedBusinessUnits()
        PopulateDD(DropDownlblPlantsAssociateBU, xTblPlants, "Plant_ID", "Plant")
    End Sub

    Private Sub populateBusinessUnits()
        PopulateDD(ddlBU, xTblNameBusiness_Unit, "Business_Unit_ID", "Business_Unit")
    End Sub


#End Region

#Region "Production Types Section"
    Protected Sub btnAddProductionTypes_Click(sender As Object, e As EventArgs)
        If txtProductionTypes.Text <> String.Empty Then
            Dim strAddUpdate As String = String.Empty
            If btnAddProductionTypes.ImageUrl = xAddImagePath Then
                strAddUpdate = xTblNameProduction_Types + ",'" + txtProductionTypes.Text + "',I"
            ElseIf btnAddProductionTypes.ImageUrl = xUpdateImagePath Then
                If txtProductionTypes.Text <> ListBoxProductionType.SelectedItem.Text Then
                    strAddUpdate = xTblNameProduction_Types + ",'" + txtProductionTypes.Text + "',U,'" + ListBoxProductionType.SelectedItem.Value + "'"
                Else
                    MessageBox("Duplicate Record !!")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsListControls(strAddUpdate)
            ResetProductionTypes()
        End If
    End Sub

    Protected Sub ListBoxProductionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBox(ListBoxProductionType)
    End Sub

    Private Sub FillTextBox(listBoxProductionType As ListBox)
        txtProductionTypes.Text = listBoxProductionType.SelectedItem.Text
        btnAddProductionTypes.ImageUrl = xUpdateImagePath
    End Sub

    Protected Sub btnDeleteProductionTypes_Click(sender As Object, e As EventArgs)
        Try
            DeleteRecordsListControls(xTblNameProduction_Types + ",'" + ListBoxProductionType.SelectedItem.Value + "'")
            ResetProductionTypes()
            MessageBox("Records Successfully Deleted !")
        Catch ex As Exception
            MessageBox("Your data is not delete !")
        End Try

    End Sub

    Private Sub ResetProductionTypes()
        populateProduction()
        ResetControls(txtProductionTypes)
        ResetControls(ListBoxProductionType)
        btnAddProductionTypes.ImageUrl = xAddImagePath

    End Sub

    Protected Sub btnRefreshProductionTypes_Click(sender As Object, e As EventArgs)
        ResetProductionTypes()
    End Sub
#End Region

#Region "Business Unit And Platform Section"

    Protected Sub btnAddBusinUnit_Click(sender As Object, e As EventArgs)
        If DropDownlblPlantsAssociateBU.Text = String.Empty Then
            MessageBox("Please select Plant Associated with Business Units")
            Exit Sub
        End If
        If txtBusinUnit.Text <> String.Empty Then
            FormAddUpdate(txtBusinUnit, ListBoxBusinUnit, btnAddBusinUnit, xTblNameBusiness_Unit)
            ResetBusinessUnit()
        End If
    End Sub


    Private Sub FormAddUpdate(ByRef txt As TextBox, ByRef list As ListBox, ByRef btn As ImageButton, tbl As String)
        Dim strAddUpdate As String = String.Empty
        Dim strAddUAssociateBU As String = String.Empty
        If btn.ImageUrl = xAddImagePath Then
            strAddUpdate = tbl + "," + txt.Text + ",I"
            AddPlantAssoBU("I")
        ElseIf btn.ImageUrl = xUpdateImagePath Then
            If txt.Text <> list.SelectedItem.Text Then
                strAddUpdate = tbl + ",'" + txt.Text + "',U,'" + list.SelectedItem.Value + "'"
            Else
                AddPlantAssoBU("U")
                MessageBox("Updated Successfully !!.")
                Exit Sub
            End If
        End If
        AddUpdateRecordsListControls(strAddUpdate)


        'ResetProductionTypes(txt, list, btn, tbl)
    End Sub

    Public Function AddPlantAssoBU(opr As String)
        Try
            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@BU_ID", ListBoxBusinUnit.SelectedItem.Value))
            params.Add(New SqlParameter("@Plant_ID", DropDownlblPlantsAssociateBU.SelectedItem.Value))

            params.Add(New SqlParameter("@Operation", opr))
            ExecuteProcedure("CMPDB_sp_InsertUpdate_AssoPlantBU ", params)
        Catch ex As Exception

        Finally

        End Try
        Return True
    End Function

    Protected Sub ListBoxBusinUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxBusinUnit.SelectedIndexChanged
        FillTextBox(txtBusinUnit, ListBoxBusinUnit, btnAddBusinUnit)
        populatePlatform(ListBoxBusinUnit.SelectedItem.Value)
        populatelantAssociatedBU(ListBoxBusinUnit.SelectedItem.Value)

        'populatePlantAssociatedBusinessUnits()
        'ShowHideDependentsPlatform("block")
        ResetControls(txtPlatform)
        btnRefreshPlatform_Click(sender, EventArgs.Empty)
    End Sub
    Private Sub populatelantAssociatedBU(value As String)
        Dim dt As DataTable = GetDataTableFromSQL("select Plant_ID from CMPDB_tblBUPlant where BU_ID='" + ListBoxBusinUnit.SelectedItem.Value + "'")
        If dt.Rows.Count > 0 Then
            DropDownlblPlantsAssociateBU.SelectedValue = dt.Rows(0)(0).ToString()
        Else
            DropDownlblPlantsAssociateBU.SelectedIndex = 0
        End If

    End Sub

    Protected Sub BtnDeleteBusinUnit_Click(sender As Object, e As EventArgs)
        If Not ListBoxBusinUnit.SelectedItem.Value Is Nothing AndAlso ListBoxPlatform.Items.Count > 0 Then
            MessageBox("Please remove Platform for Business Unit !!")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please remove Platform for Business Unit ');</script>", False)
        Else
            DeleteRecordsListControls("CMPDB_tblBusiness_Unit,'" + ListBoxBusinUnit.SelectedItem.Value + "'")
            MessageBox("Successfully Deleted !!")
            ResetBusinessUnit()
        End If
    End Sub

    Private Sub ShowHideDependentsPlatform(key As String)

    End Sub

    Private Sub ResetBusinessUnit()
        populateBusinessUnit()
        ResetControls(txtBusinUnit)
        ResetControls(ListBoxBusinUnit)
        btnAddBusinUnit.ImageUrl = xAddImagePath
        ListBoxPlatform.Items.Clear()
        'ShowHideDependentsPlatform("none")
    End Sub

    Protected Sub BtnRefreshBusinUnit_Click(sender As Object, e As EventArgs)
        ResetBusinessUnit()
    End Sub

    Protected Sub ListBoxPlatform_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBox(txtPlatform, ListBoxPlatform, BtnAddPlatform)
    End Sub

    Protected Sub BtnDeletePlatform_Click(sender As Object, e As EventArgs)
        DeleteRecordsListControls(xTblNamePlatforms + ",'" + ListBoxPlatform.SelectedItem.Value + "'")
        MessageBox("Your data is not Deleted !! ")
        btnRefreshPlatform_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub BtnAddPlatform_Click(sender As Object, e As EventArgs)
        If txtPlatform.Text = String.Empty Or ListBoxBusinUnit.SelectedIndex < 0 Then
            MessageBox("Please Select Business Units")
            Exit Sub
        End If
        Dim strAddUpdate As String = String.Empty
        If BtnAddPlatform.ImageUrl = xAddImagePath Then
            strAddUpdate = xTblNamePlatforms + ",'Platform,BU_ID',I,'''" + txtPlatform.Text + "'',''" + ListBoxBusinUnit.SelectedItem.Value + "''','',Platform_ID"
        ElseIf BtnAddPlatform.ImageUrl = xUpdateImagePath Then
            If txtPlatform.Text <> ListBoxPlatform.SelectedItem.Text Then

                strAddUpdate = xTblNamePlatforms + ",'Platform=''" + txtPlatform.Text + "''',U,''," + ListBoxPlatform.SelectedItem.Value + ",Platform_ID"
            Else

                MessageBox("Duplicate Record.")
                Exit Sub
            End If
        End If
        AddUpdateRecordsDependentListControls(strAddUpdate)

        btnRefreshPlatform_Click(sender, EventArgs.Empty)

    End Sub

    Protected Sub btnRefreshPlatform_Click(sender As Object, e As EventArgs)
        ResetControls(ListBoxPlatform)
        ResetControls(txtPlatform)
        BtnAddPlatform.ImageUrl = xAddImagePath
        If ListBoxBusinUnit.SelectedIndex > -1 Then
            populatePlatform(ListBoxBusinUnit.SelectedItem.Value)
        End If
    End Sub

#End Region

#Region "Project Types Section"

    Protected Sub btnAddProjectTypes_Click(sender As Object, e As EventArgs) Handles btnaddtxtprojectType.Click
        If txtprojectType.Text <> String.Empty Then
            Dim strAddUpdate As String = String.Empty
            If btnaddtxtprojectType.ImageUrl = xAddImagePath Then
                strAddUpdate = xTblNameProject_Types + ",'" + txtprojectType.Text + "',I"
            ElseIf btnaddtxtprojectType.ImageUrl = xUpdateImagePath Then
                If txtprojectType.Text <> ListBoxprojType.SelectedItem.Text Then
                    strAddUpdate = xTblNameProject_Types + ",'" + txtprojectType.Text + "',U,'" + ListBoxprojType.SelectedItem.Value + "'"
                Else
                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsListControls(strAddUpdate)

            ResetProjectTypes()
        End If
    End Sub


    Protected Sub ListBoxProjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxprojType.SelectedIndexChanged
        FillTextBox(txtprojectType, ListBoxprojType, btnaddtxtprojectType)
    End Sub
    Private Sub FillTextBoxProjectTYpe(ListBoxprojType As ListBox)
        txtprojectType.Text = ListBoxprojType.SelectedItem.Text
        btnaddtxtprojectType.ImageUrl = xUpdateImagePath
    End Sub

    Protected Sub btnDeletetxtprojectType_Click1(sender As Object, e As EventArgs) Handles btnDeletetxtprojectType.Click
        Try
            DeleteRecordsListControls(xTblNameProject_Types + ",'" + ListBoxprojType.SelectedItem.Value + "'")

            ResetProjectTypes()
            MessageBox("Records Successfully deleted !")
        Catch ex As Exception

            MessageBox("Your data has not been deleted !")
        End Try
    End Sub
    Private Sub ResetProjectTypes()
        populateProject()
        ResetControls(txtprojectType)
        ResetControls(ListBoxprojType)
        btnaddtxtprojectType.ImageUrl = xAddImagePath
    End Sub

    Protected Sub btnRefreshtxtprojectType_Click1(sender As Object, e As EventArgs) Handles btnRefreshtxtprojectType.Click
        ResetProjectTypes()
    End Sub
#End Region

#Region "Change Types Section"

    Protected Sub btnAddChangeTypes_Click(sender As Object, e As EventArgs) Handles btnAddChangType.Click
        If txtChangType.Text <> String.Empty Then
            Dim strAddUpdate As String = String.Empty
            If btnAddChangType.ImageUrl = xAddImagePath Then
                strAddUpdate = xTblNameChange_Types + ",'" + txtChangType.Text + "',I"
            ElseIf btnAddChangType.ImageUrl = xUpdateImagePath Then
                If txtChangType.Text <> ListBoxChangType.SelectedItem.Text Then
                    strAddUpdate = xTblNameChange_Types + ",'" + txtChangType.Text + "',U,'" + ListBoxChangType.SelectedItem.Value + "'"
                Else
                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsListControls(strAddUpdate)

            ResetChangeTypes()
        End If
    End Sub
    Protected Sub ListBoxChangType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxChangType.SelectedIndexChanged
        FillTextBox(txtChangType, ListBoxChangType, btnAddChangType)
    End Sub
    Private Sub FillTextBoxChangeTYpe(ListBoxChangType As ListBox)
        txtChangType.Text = ListBoxChangType.SelectedItem.Text
        btnAddChangType.ImageUrl = xUpdateImagePath
    End Sub


    Protected Sub btnDeleteChangType_Click(sender As Object, e As EventArgs) Handles btnDeleteChangType.Click
        Try
            DeleteRecordsListControls(xTblNameChange_Types + ",'" + ListBoxChangType.SelectedItem.Value + "'")
            ResetChangeTypes()
            MessageBox("Records Successfully deleted !")
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ResetChangeTypes()
        populateChangeType()
        ResetControls(txtChangType)
        ResetControls(ListBoxChangType)
        btnAddChangType.ImageUrl = xAddImagePath
    End Sub
    Protected Sub BtnRefreshChangType_Click(sender As Object, e As EventArgs) Handles BtnRefreshChangType.Click
        ResetChangeTypes()
    End Sub

#End Region

#Region "SWP Types and Practicioner Role Section"

    Protected Sub btnAddSWPTypes_Click(sender As Object, e As EventArgs) Handles BtnAddSWP.Click
        If txtSWP.Text <> String.Empty Then
            Dim strAddUpdate As String = String.Empty
            If BtnAddSWP.ImageUrl = xAddImagePath Then
                strAddUpdate = xTblNameSWP + ",'" + txtSWP.Text + "',I"
            ElseIf BtnAddSWP.ImageUrl = xUpdateImagePath Then
                If txtSWP.Text <> ListBoxSWP.SelectedItem.Text Then
                    strAddUpdate = xTblNameSWP + ",'" + txtSWP.Text + "',U,'" + ListBoxSWP.SelectedItem.Value + "'"
                Else

                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsListControls(strAddUpdate)
            ResetSWP()
        End If
    End Sub

    Protected Sub ListBoxSWP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxSWP.SelectedIndexChanged
        FillTextBox(txtSWP, ListBoxSWP, BtnAddSWP)
        populatePractice(ListBoxSWP.SelectedItem.Value)
        populateSWPToolName(ListBoxSWP.SelectedItem.Value)
        ShowHideDependentsSWP("block")
        ResetControls(txtPracticionerRole)
        ' ResetControls(DropDownListQLevel)
        BtnRefreshPracticionerRole_Click(sender, EventArgs.Empty)
    End Sub



    Private Sub ShowHideDependentsSWP(key As String)

    End Sub

    Private Sub FillTextBoxSWP(ListBoxSWP As ListBox)
        txtSWP.Text = ListBoxSWP.SelectedItem.Text
        BtnAddSWP.ImageUrl = xUpdateImagePath
    End Sub

    Protected Sub BtnDeleteSWP_Click1(sender As Object, e As EventArgs) Handles BtnDeleteSWP.Click
        If Not ListBoxSWP.SelectedItem.Value Is Nothing AndAlso ListBoxPracticionerRole.Items.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please remove Practitioner Role for SWP');</script>", False)
        Else
            DeleteRecordsListControls(xTblNameSWP + ",'" + ListBoxSWP.SelectedItem.Value + "'")

            ResetSWP()
        End If

    End Sub

    Private Sub ResetSWP()
        populateSWP()
        ResetControls(txtSWP)
        ResetControls(ListBoxSWP)
        BtnAddSWP.ImageUrl = xAddImagePath
        ListBoxPracticionerRole.Items.Clear()
        lbSWP_Tool_Name.Items.Clear()
        ShowHideDependentsSWP("none")
    End Sub

    Protected Sub BtnRefreshSWP_Click1(sender As Object, e As EventArgs) Handles BtnRefreshSWP.Click
        ResetSWP()
    End Sub

    Protected Sub btnAddPracticionerRole_Click(sender As Object, e As EventArgs)

        If txtPracticionerRole.Text = String.Empty Then
            Exit Sub
        End If
        If ListBoxSWP.SelectedValue = "" Then
            MessageBox("Please Select SWP !!")
            Exit Sub
        End If

        Dim strAddUpdate As String = String.Empty
        If btnAddPracticionerRole.ImageUrl = xAddImagePath Then
            strAddUpdate = xTblNamePractitioner_Roles + ",'Practitioner_Role,SWP_ID,Qualification_Level_ID',I,'''" + txtPracticionerRole.Text + "'',''" + ListBoxSWP.SelectedItem.Value + "'',''" + "0" + "''','',Practitioner_Role_ID"
        ElseIf btnAddPracticionerRole.ImageUrl = xUpdateImagePath Then
            If txtPracticionerRole.Text <> ListBoxPracticionerRole.SelectedItem.Text Then
                '@TableName as varchar(25),
                '@ColNames as varchar(max),
                '@Operation as VARCHAR(1),
                '@ColValues as varchar(max)=null,
                '@key as varchar(max)=null,
                '@keyCol as varchar(max)=null
                strAddUpdate = xTblNamePractitioner_Roles + ",'Practitioner_Role=''" + txtPracticionerRole.Text + "'',Qualification_Level_ID=''" + "0" + "''',U,''," + ListBoxPracticionerRole.SelectedItem.Value + ",Practitioner_Role_ID"
            Else
                MessageBox("Duplicate Record.")
                Exit Sub
            End If
        End If
        AddUpdateRecordsDependentListControls(strAddUpdate)
        BtnRefreshPracticionerRole_Click(sender, EventArgs.Empty)

    End Sub

    Protected Sub ListBoxPracticionerRole_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBox(txtPracticionerRole, ListBoxPracticionerRole, btnAddPracticionerRole)
        SetQualificationLevelDD(ListBoxPracticionerRole.SelectedItem.Value)

    End Sub

    Private Sub SetQualificationLevelDD(key As String)
        'Dim dt As DataTable = GetDataTableFromSQL("select Qualification_Level_ID from " + xTblNamePractitioner_Roles + " where Practitioner_Role_ID='" + key + "'")
        'If dt.Rows.Count > 0 Then
        '    DropDownListQLevel.SelectedValue = dt.Rows(0)(0).ToString()
        'Else
        '    DropDownListQLevel.SelectedIndex = 0
        'End If
    End Sub

    Protected Sub btnDeletePracticionerRole_Click(sender As Object, e As EventArgs)
        DeleteRecordsListControls(xTblNamePractitioner_Roles + ",'" + ListBoxPracticionerRole.SelectedItem.Value + "'")

        BtnRefreshPracticionerRole_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub BtnRefreshPracticionerRole_Click(sender As Object, e As EventArgs)
        ResetControls(ListBoxPracticionerRole)
        ResetControls(txtPracticionerRole)
        btnAddPracticionerRole.ImageUrl = xAddImagePath
        If ListBoxSWP.SelectedIndex > -1 Then
            populatePractice(ListBoxSWP.SelectedItem.Value)
        End If
    End Sub

    Protected Sub btnSaveSWPPracticionerQualificationLevelForRole_Click(sender As Object, e As EventArgs)

        'RunSQLQuery("Insert into [tblSWP_Practicioner_QualificationLevelForRole] values(" + ListBoxSWP.SelectedItem.Value + "," + ListBoxPracticionerRole.SelectedItem.Value + "," + DropDownListQLevel.SelectedItem.Value + ")")
        'BtnRefreshPracticionerRole_Click(sender, EventArgs.Empty)
        'DropDownListQLevel.SelectedIndex = -1
    End Sub

#End Region

#Region "Complexity Level Section"
    Protected Sub BtnAddComplexitySituation_Click(sender As Object, e As ImageClickEventArgs) Handles BtnAddComplexitySituation.Click
        If txtComplexitySituation.Text = String.Empty Then
            Exit Sub
        End If
        If ddlPracticionerRoleID.SelectedIndex = 0 Then
            MessageBox("Please Select the Practicioner Role")
            Exit Sub
        End If
        If ddlPSQualificationLevel.SelectedIndex = 0 Then
            MessageBox("PS Qualification Level")
            Exit Sub
        End If
        Dim strAddUpdate As String = String.Empty
        If BtnAddComplexitySituation.ImageUrl = xAddImagePath Then
            AddUpdateRecordsDependentListComplexitySituation("I")
            MessageBox("Saved Successfully !!")
        ElseIf BtnAddComplexitySituation.ImageUrl = xUpdateImagePath Then
            'If txtComplexitySituation.Text <> ListBoxComplexitySituation.SelectedItem.Text Then
            AddUpdateRecordsDependentListComplexitySituation("U")
            MessageBox("Updated Successfully !!")
            'Else
            'Response.Write("Duplicate Record.")
            'Exit Sub
            'End If
        End If

        BtnRefreshComplexitySituation_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub BtnDeleteComplexitySituation_Click(sender As Object, e As EventArgs) Handles BtnDeleteComplexitySituation.Click
        DeleteRecordsListControls(xTblcmpdb_ComplexityLevel + ",'" + ListBoxComplexitySituation.SelectedItem.Value + "'")

        BtnRefreshComplexitySituation_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub BtnRefreshComplexitySituation_Click(sender As Object, e As EventArgs) Handles BtnRefreshComplexitySituation.Click
        ResetControls(ListBoxComplexitySituation, ddlPracticionerRoleID)
        ResetControls(ListBoxComplexitySituation, ddlPSQualificationLevel)
        ResetControls(txtComplexitySituation)
        BtnAddComplexitySituation.ImageUrl = xAddImagePath
        populateComplexity()
    End Sub

    Protected Sub ListBoxComplexitySituation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxComplexitySituation.SelectedIndexChanged
        FillTextBox(txtComplexitySituation, ListBoxComplexitySituation, BtnAddComplexitySituation)
        SetDD(ListBoxComplexitySituation.SelectedItem.Value)
    End Sub

    Private Sub SetDD(value As String)
        Dim dt As DataTable = GetDataTableFromSQL("select Practicioner_Role_ID from " + xTblcmpdb_ComplexityLevel + " where ComplexitySituation='" + txtComplexitySituation.Text + "'")
        If dt.Rows.Count > 0 Then
            ddlPracticionerRoleID.SelectedValue = dt.Rows(0)(0).ToString()
        Else
            ddlPracticionerRoleID.SelectedIndex = 0
        End If

        dt = GetDataTableFromSQL("select PS_Qualification_Level from " + xTblcmpdb_ComplexityLevel + " where ComplexitySituation='" + txtComplexitySituation.Text + "'")
        If dt.Rows.Count > 0 Then
            ddlPSQualificationLevel.SelectedValue = dt.Rows(0)(0).ToString()
        Else
            ddlPSQualificationLevel.SelectedIndex = 0
        End If
    End Sub

    Public Function AddUpdateRecordsDependentListComplexitySituation(opr As String)
        Try
            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@ComplexitySituation", txtComplexitySituation.Text))
            params.Add(New SqlParameter("@Practicioner_Role_ID", ddlPracticionerRoleID.SelectedItem.Value))
            params.Add(New SqlParameter("@PS_Qualification_Level", ddlPSQualificationLevel.SelectedItem.Value))
            If ListBoxComplexitySituation.SelectedIndex > -1 Then
                params.Add(New SqlParameter("@Complexity_Situation_ID", ListBoxComplexitySituation.SelectedItem.Value))
            End If
            params.Add(New SqlParameter("@Operation", opr))
            ExecuteProcedure("CMPDB_sp_InsertUpdate_tblComplexityLevel ", params)
        Catch ex As Exception

        Finally

        End Try
        Return True
    End Function
#End Region

#Region "SWP Tool Name Section"
    Protected Sub lbSWP_Tool_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbSWP_Tool_Name.SelectedIndexChanged
        FillTextBox(txtSWP_Tool_Name, lbSWP_Tool_Name, btnAddSWP_Tool_Name)
        FillSWPToolNameDependents(lbSWP_Tool_Name.SelectedItem.Value)
    End Sub

    Private Sub FillSWPToolNameDependents(value As String)
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@SWP_Tool_Name_ID", lbSWP_Tool_Name.SelectedItem.Value))
        Dim dt As DataTable = ExecuteProcedureForDataTable("CMPDB_sp_GetBLOBFromToolName_tblBLOBFiles", params)
        If dt.Rows.Count > 0 Then
            txtCorporate_WS_Name.Text = dt.Rows(0)("Corporate_WS_Name").ToString()
            txtCorporate_Cell_Location_For_Score.Text = dt.Rows(0)("Corporate_Cell_Location_For_Score").ToString()
            lnkbtnFileName.Visible = True
            lnkbtnFileName.Text = dt.Rows(0)("FileName").ToString()
            btnEdit.Attributes.Add("style", "display:block")
            'btnCancel.Attributes.Add("style", "display:block")
            fuCorporate_Standard_File.Attributes.Add("style", "display:none")
        Else
            ddlPracticionerRoleID.SelectedIndex = 0
        End If
    End Sub

    Protected Sub btnAddSWP_Tool_Name_Click(sender As Object, e As ImageClickEventArgs)
        If ListBoxSWP.SelectedValue = "" Then
            MessageBox("Please Select SWP !!")
            Exit Sub
        End If

        Dim strAddUpdate As String = String.Empty
        If btnAddSWP_Tool_Name.ImageUrl = xAddImagePath Then
            AddUpdateRecordsSWPToolName("I")
        ElseIf btnAddSWP_Tool_Name.ImageUrl = xUpdateImagePath Then
            ' If txtSWP_Tool_Name.Text <> lbSWP_Tool_Name.SelectedItem.Text Then
            AddUpdateRecordsSWPToolName("U")
            'Else
            ' MessageBox("Duplicate Record.")
            '    Exit Sub
            'End If
        End If
        btnRefreshSWP_Tool_Name_Click(sender, EventArgs.Empty)
    End Sub
    Private Sub AddUpdateRecordsSWPToolName(opr As String)
        Try
            Dim params As New List(Of SqlParameter)
            Dim paramsForFiles As New List(Of SqlParameter)
            Dim paramsSWPToolName As New List(Of SqlParameter)

            paramsSWPToolName.Add(New SqlParameter("@Operation", opr))
            paramsSWPToolName.Add(New SqlParameter("@SWP_Tool_Name", txtSWP_Tool_Name.Text))
            paramsSWPToolName.Add(New SqlParameter("@SWP_ID", ListBoxSWP.SelectedItem.Value))


            If lbSWP_Tool_Name.SelectedIndex > -1 Then
                params.Add(New SqlParameter("@CorporateSources_ID", lbSWP_Tool_Name.SelectedItem.Value))
                Dim paramsUpdate As New List(Of SqlParameter)
                If fuCorporate_Standard_File.HasFile Then
                    paramsUpdate.Add(New SqlParameter("@FileObject", fuCorporate_Standard_File.FileContent))
                    paramsUpdate.Add(New SqlParameter("@FileName", fuCorporate_Standard_File.FileName))
                End If
                paramsUpdate.Add(New SqlParameter("@SWP_Tool_Name_ID", lbSWP_Tool_Name.SelectedItem.Value))
                paramsUpdate.Add(New SqlParameter("@Corporate_WS_Name", txtCorporate_WS_Name.Text))
                paramsUpdate.Add(New SqlParameter("@Corporate_Cell_Location_For_Score", txtCorporate_Cell_Location_For_Score.Text))
                ExecuteProcedure("CMPDB_sp_updateFile_GlobalAdmin", paramsUpdate)
                Exit Sub
            End If

            params.Add(New SqlParameter("@Operation", opr))
            If txtSWP_Tool_Name.Text = String.Empty Then
                Exit Sub
            End If
            ExecuteProcedure("CMPDB_sp_InsertNUpdate_tblSWP_Tool_Names", paramsSWPToolName)
            If txtCorporate_Cell_Location_For_Score.Text = "" Or txtCorporate_WS_Name.Text = "" Or fuCorporate_Standard_File.HasFile = False Then
                Exit Sub
            End If

            paramsForFiles.Add(New SqlParameter("@FileObject", fuCorporate_Standard_File.FileBytes))
            paramsForFiles.Add(New SqlParameter("@FileName", fuCorporate_Standard_File.FileName))
            paramsForFiles.Add(New SqlParameter("@Operation", opr))

            params.Add(New SqlParameter("@SWP_Tool_Name_ID", txtSWP_Tool_Name.Text))
            params.Add(New SqlParameter("@Corporate_WS_Name", txtCorporate_WS_Name.Text))
            params.Add(New SqlParameter("@Corporate_Cell_Location_For_Score", txtCorporate_Cell_Location_For_Score.Text))

            ExecuteProcedure("CMPDB_sp_InsertNUpdate_tblCorporateSourcesBLOBFiles", paramsForFiles)
            ExecuteProcedure("CMPDB_sp_InsertNUpdate_tblCorporateSources ", params)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDeleteSWP_Tool_Name_Click(sender As Object, e As ImageClickEventArgs)
        DeleteRecordsswpToolName(lbSWP_Tool_Name.SelectedItem.Value)
        btnRefreshSWP_Tool_Name_Click(sender, EventArgs.Empty)
    End Sub
    Protected Sub btnRefreshSWP_Tool_Name_Click(sender As Object, e As EventArgs)
        ResetControls(lbSWP_Tool_Name)
        'ResetControls(l, ddlPSQualificationLevel)
        ResetControls(txtSWP_Tool_Name, txtCorporate_WS_Name, txtCorporate_Cell_Location_For_Score)
        btnAddSWP_Tool_Name.ImageUrl = xAddImagePath
        populateSWPToolName(ListBoxSWP.SelectedItem.Value)
        btnCancel.Attributes.Add("style", "display:none")
        btnEdit.Attributes.Add("style", "display:none")
        lnkbtnFileName.Text = ""
        fuCorporate_Standard_File.Attributes.Add("style", "display:block")
    End Sub
    Private Sub populateSWPToolName(value As String)
        PopulateDD(lbSWP_Tool_Name, xtblSWPToolName, "SWP_Tool_Name_ID", "SWP_Tool_Name", "SWP_ID", value)
    End Sub

    Protected Sub lnkbtnFileName_click(sender As Object, e As EventArgs) Handles lnkbtnFileName.Click

        Dim dt As DataTable = GetDataTableFromSQL("Select a.filename, a.FileObject, a.FileUploaded,a.BLOBFile_ID from CMPDB_tblCorporateSourcesBLOBFiles a
							inner join CMPDB_tblCorporateSources b  on a.BLOBFile_ID = b.BLOBFile_ID where a.FileName='" + lnkbtnFileName.Text + "' and b.Corporate_Cell_Location_For_Score ='" + txtCorporate_Cell_Location_For_Score.Text + "'	and b.Corporate_WS_Name='" + txtCorporate_WS_Name.Text + "' ")
        If dt.Rows.Count > 0 Then
            Dim mBLOBFile = dt.Rows(0)("BLOBFile_ID")
            Dim File1 = CType(dt.Rows(0)("FileObject"), Byte())
            Dim ms As New MemoryStream(File1)
            'Append Table name BLOB ID and File Name to the template Excel File
            Dim FileName = dt.Rows(0)("FileName")
            ms = AppendCustomDataToExcel(ms, mBLOBFile, FileName, "CMPDB_tblCorporateSourcesBLOBFiles")
            DownloadFileFromMemoryStream(ms, dt.Rows(0)("FileName"))
        End If
    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        fuCorporate_Standard_File.Attributes.Add("style", "display:block")
        lnkbtnFileName.Visible = False
        'btnupload.Attributes.Add("style", "display:block")
        btnCancel.Attributes.Add("style", "display:block")
        btnEdit.Attributes.Add("style", "display:none")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        fuCorporate_Standard_File.Attributes.Add("style", "display:none")
        lnkbtnFileName.Visible = True
        'btnupload.Attributes.Add("style", "display:none")

        btnCancel.Attributes.Add("style", "display:none")
        btnEdit.Attributes.Add("style", "display:block")
    End Sub

    'Protected Sub btnupload_click(sender As Object, e As EventArgs) Handles btnCancel.Click
    'Dim params As New List(Of SqlParameter)
    '    params.Add(New SqlParameter("@FileObject", fuCorporate_Standard_File.FileContent))
    '    params.Add(New SqlParameter("@FileName", fuCorporate_Standard_File.FileName))
    '    params.Add(New SqlParameter("@SWP_Tool_Name_ID", lbSWP_Tool_Name.SelectedItem.Value))
    '    params.Add(New SqlParameter("@Corporate_WS_Name", txtCorporate_WS_Name.Text))
    '    params.Add(New SqlParameter("@Corporate_Cell_Location_For_Score", txtCorporate_Cell_Location_For_Score.Text))
    '    ExecuteProcedure("CMPDB_sp_updateFile_GlobalAdmin", params)
    'End Sub

    Public Sub DeleteRecordsswpToolName(str As String)
        ExecuteProc("CMPDB_sp_DeleteSWPToolName " + str)
    End Sub
#End Region

#Region "Qualification Level Type"
    Protected Sub btnAddQualificationTypes_Click(sender As Object, e As EventArgs)
        If txtQualificationTypes.Text <> String.Empty Then
            Dim strAddUpdate As String = String.Empty
            If btnAddQualificationTypes.ImageUrl = xAddImagePath Then
                strAddUpdate = xTblQualification + ",'" + txtQualificationTypes.Text + "',I"
            ElseIf btnAddQualificationTypes.ImageUrl = xUpdateImagePath Then
                If txtQualificationTypes.Text <> ListBoxQualificationTypes.SelectedItem.Text Then
                    strAddUpdate = xTblQualification + ",'" + txtQualificationTypes.Text + "',U,'" + ListBoxQualificationTypes.SelectedItem.Value + "'"
                Else

                    MessageBox("Duplicate Record.")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsListControls(strAddUpdate)

            ResetQualificationTypes()
        End If
    End Sub

    Protected Sub ListBoxQualificationType_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBoxQualificatiion(ListBoxQualificationTypes)
    End Sub

    Private Sub FillTextBoxQualificatiion(ListBoxQualificationTypes As ListBox)
        txtQualificationTypes.Text = ListBoxQualificationTypes.SelectedItem.Text
        btnAddQualificationTypes.ImageUrl = xUpdateImagePath
    End Sub

    Protected Sub btnDeleteQualificationTypes_Click(sender As Object, e As EventArgs)
        Try
            DeleteRecordsListControls(xTblQualification + ",'" + ListBoxQualificationTypes.SelectedItem.Value + "'")
            ResetQualificationTypes()
            MessageBox("Records Successfully Deleted !")
        Catch ex As Exception
            MessageBox("Your data is not delete !")
        End Try

    End Sub
    Private Sub ResetQualificationTypes()
        populateQualification()
        ResetControls(txtQualificationTypes)
        ResetControls(ListBoxQualificationTypes)
        btnAddQualificationTypes.ImageUrl = xAddImagePath
    End Sub
    Protected Sub btnRefreshQualificationTypes_Click(sender As Object, e As EventArgs)
        ResetQualificationTypes()
    End Sub
#End Region

#Region "Region Section"
    Private Sub FillTextBoxRegion(listBoxProductionType As ListBox)
        txtRegions.Text = ListBoxRegions.SelectedItem.Text
        btnAddRegions.ImageUrl = xUpdateImagePath
    End Sub

    Private Sub ResetRegion()
        populateRegion()
        ResetControls(txtRegions)
        ResetControls(ListBoxRegions)
        btnAddRegions.ImageUrl = xAddImagePath
    End Sub



    Protected Sub btnAddRegions_Click(sender As Object, e As ImageClickEventArgs)
        If txtRegions.Text <> String.Empty Then
            Dim strAddUpdate As String = String.Empty
            If btnAddRegions.ImageUrl = xAddImagePath Then
                strAddUpdate = xTblRegion + ",'" + txtRegions.Text + "',I"
            ElseIf btnAddRegions.ImageUrl = xUpdateImagePath Then
                If txtRegions.Text <> ListBoxRegions.SelectedItem.Text Then
                    strAddUpdate = xTblRegion + ",'" + txtRegions.Text + "',U,'" + ListBoxRegions.SelectedItem.Value + "'"
                Else
                    MessageBox("Duplicate Record !!")
                    Exit Sub
                End If
            End If
            AddUpdateRecordsListControls(strAddUpdate)
            populateRegion()
        End If
    End Sub

    Protected Sub btnDeleteRegions_Click(sender As Object, e As ImageClickEventArgs)
        Try
            DeleteRecordsListControls(xTblRegion + ",'" + ListBoxRegions.SelectedItem.Value + "'")
            ResetRegion()
            MessageBox("Records Successfully Deleted !")
        Catch ex As Exception
            MessageBox("Your data is not delete !")
        End Try
    End Sub

    Protected Sub btnRefreshRegions_Click(sender As Object, e As ImageClickEventArgs)
        ResetRegion()
    End Sub

    Protected Sub ListBoxRegions_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBoxRegion(ListBoxRegions)
    End Sub
#End Region

#Region "Plants  Section"
    Protected Sub btnAddPlants_Click(sender As Object, e As EventArgs)
        If ddlRegionTypes.SelectedIndex = 0 Then
            MessageBox("Must choose a region to save.")
            Exit Sub
        End If

        If ddlBU.SelectedIndex = 0 Then
            MessageBox("Must choose a BU to save.")
            Exit Sub
        End If
        If txtPalnts.Text <> String.Empty Then
            FormAddUpdates(txtPalnts, ListBoxPlants, btnAddPlants, xTblPlants)
            ResetPlants()
        End If
    End Sub

    Private Sub FormAddUpdates(ByRef txt As TextBox, ByRef list As ListBox, ByRef btn As ImageButton, tbl As String)
        Dim strAddUpdate As String = String.Empty
        Dim strUpdateRegion = String.Empty
        strUpdateRegion = $"Update { xTblPlants}  SET Region_ID = {ddlRegionTypes.SelectedValue} 
                          where Plant_ID={ListBoxPlants.SelectedValue}"
        If btn.ImageUrl = xAddImagePath Then

            ' strAddUpdate = $"{tbl} ,'Plant,BU_ID',I,''{txt.Text}'','{ddlBU.SelectedValue}',null"
            strAddUpdate = tbl + ",'Plant,BU_ID,Region_ID',I,'''" + txt.Text + "'',''" + ddlBU.SelectedItem.Value + "'',''" + ddlRegionTypes.SelectedItem.Value + "''','',BU_ID"

        ElseIf btn.ImageUrl = xUpdateImagePath Then

            If txt.Text <> list.SelectedItem.Text Then
                'ExecuteProcedure("sp_InsertUpdatePlant", params)
                strAddUpdate = tbl + ",'Plant=''" + txt.Text + "'',BU_ID=" + ddlBU.SelectedValue + "'',Region_ID=" + ddlRegionTypes.SelectedValue + "',U,''," + ListBoxPlants.SelectedItem.Value + ",Plant_ID"
            Else
                MessageBox("Duplicate Record.")
                Exit Sub
            End If
        End If

        ' RunSQLQuery(strUpdateRegion)
        AddUpdateRecordsDependentListControls(strAddUpdate)
        'ResetProductionTypes(txt, list, btn, tbl)
    End Sub
    Protected Sub ListBoxPlants_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxPlants.SelectedIndexChanged
        FillTextBox(txtPalnts, ListBoxPlants, btnAddPlants)
        ddlRegionTypes.SelectedValue = GetSingleValue("SELECT Region_ID From " + xTblPlants + " where Plant_ID=" + ListBoxPlants.SelectedItem.Value)
        'populateRegion(ListBoxPlants.SelectedItem.Value)
        Dim BU_ID = GetSingleValue("select BU_ID from " + xTblPlants + " where Plant_ID=" + ListBoxPlants.SelectedValue)
        ddlBU.SelectedValue = IIf(BU_ID IsNot "", BU_ID, 0)
    End Sub

    Protected Sub BtnDeletePlants_Click(sender As Object, e As EventArgs)
        If Not ListBoxPlants.SelectedItem.Value Then
            MessageBox("Please remove Region for Plant !! ")
        Else
            DeleteRecordsListControls("CMPDB_tblPlants,'" + ListBoxPlants.SelectedItem.Value + "'")
            MessageBox("Successfully Deleted !!")
            ResetPlants()
        End If
    End Sub

    Private Sub ResetPlants()
        populatePlants()
        ResetControls(txtPalnts)
        ddlRegionTypes.SelectedIndex = 0
        ddlBU.SelectedIndex = 0
        ResetControls(ListBoxPlants)
        btnAddPlants.ImageUrl = xAddImagePath
        'ResetControls(ListBoxRegionTypes)
        'ResetControls(txtRegionTypes)
        'ListBoxRegionTypes.Items.Clear()
        'btnAddRegionTypes.ImageUrl = xAddImagePath
        'ShowHideDependentsPlatform("none")
    End Sub

    Protected Sub BtnRefreshPlants_Click(sender As Object, e As EventArgs)
        ResetPlants()
    End Sub
#End Region
End Class