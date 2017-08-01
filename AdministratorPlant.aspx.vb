Imports System.Data.SqlClient

Public Class AdministratorPlant
    Inherits Page

#Region "Variables"

    ReadOnly xTblNameDepartment_Types = "CMPDB_tblSite_Departments"
    ReadOnly xTblNameSiteProductionLines_Types = "CMPDB_tblSite_Production_Lines"
    ReadOnly xTblNameSitePlantDetails = "CMPDB_tblPlantDtls"
    ReadOnly xTblNameCBNS_Types = "CMPDB_tblSite_CBNS"
    ReadOnly xTblNametableSource = "CMPDB_tblSource_Files"
    ReadOnly xUpdateImagePath = "~/images/Update.png"
    ReadOnly xAddImagePath = "~/images/Add.png"
    ReadOnly xSWPToolNameCount = "9"
    Public XaVisibility = "none"
#End Region

#Region "Common"
    Private Sub LoadAllDependents()
        populateDepartments(DropDownListPlant.SelectedItem.Value)
        populateCBNs(DropDownListPlant.SelectedItem.Value)
        populateProductionLines()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParentSiteMapNode()
        If Not IsPostBack Then
            ToBeRemovedInTheEnd()
            SetTools()
            Session("HasFiles") = 0
            PopulateDD(DropDownListPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
            If Session("plant_Set") <> "" Then
                PopulateDD(DropDownListPlant, "CMPDB_tblPlants", "Plant_ID", "Plant")
                DropDownListPlant.SelectedValue = Session("plant_Set")
            End If
            PopulateDD(DropDownListProductionType, "CMPDB_tblProduction_Types", "Production_Type_ID", "Production_Type")
            PopulateDD(DropDownListBusinessUnit, "CMPDB_tblBusiness_Unit", "Business_Unit_ID", "Business_Unit")
            SetDefaultPlant(DropDownListPlant)
            LoadAllDependents()
        End If
    End Sub

    Private Sub ToggleRadioButtons(v As Boolean)
        If v = True Then
            RadioButtonSingleSource.Checked = True
            RadioButtonUseCorporate.Checked = False

        Else
            RadioButtonUseCorporate.Checked = True
            RadioButtonSingleSource.Checked = False
        End If
    End Sub

    Private Sub ShowHideFileUploadControls()

        'ShowHideControls("Block", lnkfuGSUMMTL)
        ''ShowHideControls("Block", lnkfuGsumSmallWS, lnkfuGSUMMTL, lnkfuGSUMPRWS, lnkfuGSUMMFGOps, lnkfuMFGDel, lnkfuEWPTracker, lnkfuGSUM_IAP, lnkfuRE_Implementation, lnkfuGSUM_PRRA, lnkfuGSUM_PRRA)
        ShowHideControls(RadioButtonUseCorporate.Checked, fuEWPTracker, fuGSUMMFGOps, fuGSUMMTL, fuGSUMPRWS, fuGsumSmallWS, fuGSUM_IAP, fuGSUM_PRRA, fuMFGDel, fuRE_Implementation)
        ResetControls(lblfuGsumSmallWS, lbl_file_fuGSUMMTL, lbl_file_fuGSUMPRWS, lbl_file_fuGSUMMFGOps, lbl_file_fuMFGDel, lbl_file_fuEWPTracker, lbl_file_fuGSUM_IAP, lbl_file_fuRE_Implementation, lbl_file_fuGSUM_PRRA)
        ResetControls(txtCellEWPTracker, txtCellGSUMMFGOps, txtCellGSUMMTL, txtCellGSUMPRWS, txtCellGsumSmallWS, txtCellGSUM_IAP, txtCellGSUM_PRRA, txtCellMFGDel, txtCellRE_Implementation, txtEWPTracker, txtGSUMMFGOps, txtGSUMMTL, txtGSUMPRWS, txtGsumSmallWS, txtGSUM_IAP, txtGSUM_PRRA, txtMFGDel, txtRE_Implementation)

        ''ShowHideControls(v1(0), fuEWPTracker, fuGSUMMFGOps, fuGSUMMTL, fuGSUMPRWS, fuGsumSmallWS, fuGSUM_IAP, fuGSUM_PRRA, fuMFGDel, fuRE_Implementation)

    End Sub

    Private Sub ShowHideDependentsDepartments(key As String)
        DivDepartments.Attributes.Add("style", "display:" + key)
    End Sub

    Private Sub ShowHideDependentsCBN(key As String)
        DivCBN.Attributes.Add("style", "display:" + key)
    End Sub

    Private Sub ShowHideDependentsProductionLine(key As String)
        DivProductionLines.Attributes.Add("style", "display:" + key)
    End Sub

    Private Sub ToBeRemovedInTheEnd()
        lbl_file_fuGSUM_PRRA.CssClass = "hide"

    End Sub

    Private Sub SetDefaultPlant(ByRef dropDownListPlant As DropDownList)
        If Session("User_ID") Is Nothing Then
        Else
            dropDownListPlant.SelectedValue = GetSingleValue($"Select Plant_ID from CMPDB_tblDefault_Locations where User_ID='{Session("User_ID").ToString()}'")
            DropDownListPlant_SelectedIndexChanged(New Object(), EventArgs.Empty)
        End If
    End Sub
    Private Sub SetTools()
        Dim dt = New DataTable
        dt = GetDataTableFromSQL("select top " + xSWPToolNameCount + " swp_tool_name name,swp_Tool_Name_id id from CMPDB_tblSWP_Tool_Names")
        If dt.Rows.Count > 0 Then
            Dim list As List(Of KeyValuePair(Of String, Integer)) = New List(Of KeyValuePair(Of String, Integer))
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(0)("name").ToString(), dt.Rows(0)("id").ToString()))
            lblGsumSmallWS.Text = dt.Rows(0)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(1)("name").ToString(), dt.Rows(1)("id").ToString()))
            lblGSUMMTL.Text = dt.Rows(1)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(2)("name").ToString(), dt.Rows(2)("id").ToString()))
            lblGSUMPRWS.Text = dt.Rows(2)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(3)("name").ToString(), dt.Rows(3)("id").ToString()))
            lblGSUMMFGOps.Text = dt.Rows(3)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(4)("name").ToString(), dt.Rows(4)("id").ToString()))
            lblMFGDel.Text = dt.Rows(4)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(5)("name").ToString(), dt.Rows(5)("id").ToString()))
            lblEWPTracker.Text = dt.Rows(5)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(6)("name").ToString(), dt.Rows(6)("id").ToString()))
            lblGSUM_IAP.Text = dt.Rows(6)("name").ToString()
            list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(7)("name").ToString(), dt.Rows(7)("id").ToString()))
            lblRE_Implementation.Text = dt.Rows(7)("name").ToString()
            'list.Add(New KeyValuePair(Of String, Integer)(dt.Rows(8)("name").ToString(), dt.Rows(8)("id").ToString()))
            'lblGSUM_PRRA.Text = dt.Rows(8)("name").ToString()
            Session("ToolList") = list
        End If
    End Sub

    Private Sub FillTextBox(ByRef txt As TextBox, ByRef list As ListBox, ByRef btn As ImageButton)
        txt.Text = list.SelectedItem.Text
        btn.ImageUrl = xUpdateImagePath
    End Sub
#End Region

#Region "Populates"
    Private Sub populateDepartments(key As String)
        PopulateDD(ListBoxDepartments, xTblNameDepartment_Types, "Site_Department_ID", "Site_Department", "Site_ID", key)
    End Sub
    Private Sub populateCBNs(key As String)
        'PopulateDDWithSQL(ListBoxCNB, "select Site_CBN,Site_CBN_ID from " + xTblNameCBNS_Types + " where Site_ID='" + key + "'")
        PopulateDD(ListBoxCNB, xTblNameCBNS_Types, "Site_CBN_ID", "Site_CBN", "Site_ID", key)
    End Sub
    Private Sub populateCStandards()
        Dim dt = New DataTable
        Dim params As New List(Of SqlParameter)
        dt = ExecuteProcedureForDataTable("CMPDB_sp_GetSWP_Tools", params)
        Dim listBlob As List(Of KeyValuePair(Of String, Integer)) =
            New List(Of KeyValuePair(Of String, Integer))

        txtCellGsumSmallWS.Text = dt.Rows(0)("cell").ToString()
        txtCellGSUMMTL.Text = dt.Rows(1)("cell").ToString()
        txtCellGSUMPRWS.Text = dt.Rows(2)("cell").ToString()
        txtCellGSUMMFGOps.Text = dt.Rows(3)("cell").ToString()
        txtCellMFGDel.Text = dt.Rows(4)("cell").ToString()
        txtCellEWPTracker.Text = dt.Rows(5)("cell").ToString()
        txtCellGSUM_IAP.Text = dt.Rows(6)("cell").ToString()
        txtCellRE_Implementation.Text = dt.Rows(7)("cell").ToString()
        txtCellGSUM_PRRA.Text = dt.Rows(8)("cell").ToString()


        txtGsumSmallWS.Text = dt.Rows(0)("cname").ToString()
        txtGSUMMTL.Text = dt.Rows(1)("cname").ToString()
        txtGSUMPRWS.Text = dt.Rows(2)("cname").ToString()
        txtGSUMMFGOps.Text = dt.Rows(3)("cname").ToString()
        txtMFGDel.Text = dt.Rows(4)("cname").ToString()
        txtEWPTracker.Text = dt.Rows(5)("cname").ToString()
        txtGSUM_IAP.Text = dt.Rows(6)("cname").ToString()
        txtRE_Implementation.Text = dt.Rows(7)("cname").ToString()
        txtGSUM_PRRA.Text = dt.Rows(8)("cname").ToString()


        lblfuGsumSmallWS.Text = dt.Rows(0)("fname").ToString()
        'lblfuGsumSmallWS.ID = lblfuGsumSmallWS.ID + "_" + dt.Rows(0)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(0)("fname").ToString(), dt.Rows(0)("fid").ToString()))

        lbl_file_fuGSUMMTL.Text = dt.Rows(1)("fname").ToString()
        ' lbl_file_fuGSUMMTL.ID = lbl_file_fuGSUMMTL.ID + " " + dt.Rows(1)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(1)("fname").ToString(), dt.Rows(1)("fid").ToString()))


        lbl_file_fuGSUMPRWS.Text = dt.Rows(2)("fname").ToString()
        'lbl_file_fuGSUMPRWS.ID = lbl_file_fuGSUMPRWS.ID + " " + dt.Rows(2)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(2)("fname").ToString(), dt.Rows(2)("fid").ToString()))

        lbl_file_fuGSUMMFGOps.Text = dt.Rows(3)("fname").ToString()
        'lbl_file_fuGSUMMFGOps.ID = lbl_file_fuGSUMMFGOps.ID + " " + dt.Rows(3)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(3)("fname").ToString(), dt.Rows(3)("fid").ToString()))

        lbl_file_fuMFGDel.Text = dt.Rows(4)("fname").ToString()
        'lbl_file_fuMFGDel.ID = lbl_file_fuMFGDel.ID + " " + dt.Rows(4)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(4)("fname").ToString(), dt.Rows(4)("fid").ToString()))

        lbl_file_fuEWPTracker.Text = dt.Rows(5)("fname").ToString()
        'lbl_file_fuEWPTracker.ID = lbl_file_fuEWPTracker.ID + " " + dt.Rows(5)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(5)("fname").ToString(), dt.Rows(5)("fid").ToString()))

        lbl_file_fuGSUM_IAP.Text = dt.Rows(6)("fname").ToString()
        'lbl_file_fuGSUM_IAP.ID = lbl_file_fuGSUM_IAP.ID + " " + dt.Rows(6)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(6)("fname").ToString(), dt.Rows(6)("fid").ToString()))

        lbl_file_fuRE_Implementation.Text = dt.Rows(7)("fname").ToString()
        'lbl_file_fuRE_Implementation.ID = lbl_file_fuRE_Implementation.ID + " " + dt.Rows(7)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(7)("fname").ToString(), dt.Rows(7)("fid").ToString()))

        lbl_file_fuGSUM_PRRA.Text = dt.Rows(8)("fname").ToString()
        'lbl_file_fuGSUM_PRRA.ID = lbl_file_fuGSUM_PRRA.ID + " " + dt.Rows(8)("tid").ToString()
        listBlob.Add(New KeyValuePair(Of String, Integer)(dt.Rows(8)("fname").ToString(), dt.Rows(8)("fid").ToString()))

        Session("Blobfile") = listBlob

    End Sub

    Protected Sub DropDownListProductionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        populateProductionLines()
    End Sub

    Protected Sub DropDownListPlatform_SelectedIndexChanged(sender As Object, e As EventArgs)
        populateProductionLines()
    End Sub

    Protected Sub DropDownListBusinessUnit_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateDD(DropDownListPlatform, "CMPDB_tblPlatforms", "Platform_ID", "Platform", "BU_ID", DropDownListBusinessUnit.SelectedItem.Value)
        PopulateDDWithSQL(ListBoxProductionLine, "select Site_Production_Line_Name,Site_Production_Line_ID from CMPDB_tblSite_Production_Lines Where Site_ID=" +
            DropDownListPlant.SelectedItem.Value + " and BU_ID=" + DropDownListBusinessUnit.SelectedItem.Value)
    End Sub

#End Region

#Region "Production Line Section"
    Private Sub populateProductionLines()
        Dim params As New List(Of SqlParameter)
        If DropDownListBusinessUnit.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@BU_ID", DropDownListBusinessUnit.SelectedItem.Value))
        End If
        If DropDownListPlatform.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@Platform_ID", DropDownListPlatform.SelectedItem.Value))
        End If
        If DropDownListProductionType.SelectedIndex > 0 Then
            params.Add(New SqlParameter("@Production_Type_ID", DropDownListProductionType.SelectedItem.Value))
        End If
        params.Add(New SqlParameter("@Site_ID", DropDownListPlant.SelectedItem.Value))
        Dim dt = ExecuteProcedureForDataTable("sp_FillWithProductionLine", params)
        PopulateDDFromDataTable(ListBoxProductionLine, dt)
    End Sub

    Protected Sub btnDeleteProductionLine_Click(sender As Object, e As EventArgs)
        DeleteRecordsListControls(xTblNameSiteProductionLines_Types + ",'" + ListBoxProductionLine.SelectedItem.Value + "'")
        MessageBox("Successfully Deleted !!")
        BtnRefreshProductionLine_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub btnAddProductionLine_Click(sender As Object, e As EventArgs)
        If DropDownListPlant.SelectedValue = "0" Then
            MessageBox("Please select the Plants !!")
            DropDownListPlant.Focus()
            Exit Sub
        End If
        If DropDownListBusinessUnit.SelectedValue = "0" Then
            MessageBox("Please Select Buisness Unit !!")
            DropDownListBusinessUnit.Focus()
            Exit Sub
        End If
        'If DropDownListPlatform.SelectedValue = "0" Then
        '	MessageBox("Plaese Select Platform !!")
        '	DropDownListPlatform.Focus()
        '	Exit Sub
        'End If
        If DropDownListProductionType.SelectedValue = "0" Then
            MessageBox("Please Select Production Types !!")
            DropDownListProductionType.Focus()
            Exit Sub
        End If
        Dim strAddUpdate As String = String.Empty
        Dim mOperation As String = "I"
        If txtProductionLine.Text = "" Then
            Exit Sub
        End If
        If btnAddProductionLine.ImageUrl = xAddImagePath Then
            mOperation = "I"
            'strAddUpdate = xTblNameSiteProductionLines_Types + ",'Site_Production_Line_Name,Site_ID',I,'''" + txtProductionLine.Text + "'',''" + DropDownListPlant.SelectedItem.Value + "''','',Site_Production_Line_ID"
        ElseIf btnAddProductionLine.ImageUrl = xUpdateImagePath Then
            'If txtProductionLine.Text <> ListBoxProductionLine.SelectedItem.Text Then
            mOperation = "U"
            'strAddUpdate = xTblNameSiteProductionLines_Types + ",'Site_Production_Line_Name=''" + txtProductionLine.Text + "''',U,''," + ListBoxProductionLine.SelectedItem.Value + ",Site_Production_Line_ID"
            'Else
            'Response.Write("Duplicate Record.")
            'Exit Sub
            'End If
        End If
        'AddUpdateRecordsDependentListControls(strAddUpdate)
        AddUpdateProductionLine(mOperation)
        MessageBox("Successfully Updated !!")
        BtnRefreshProductionLine_Click(sender, EventArgs.Empty)
    End Sub

    Private Sub AddUpdateProductionLine(mOperation As String)
        Dim params As New List(Of SqlParameter)
        If mOperation = "U" Then
            params.Add(New SqlParameter("@Site_Production_Line_ID", ListBoxProductionLine.SelectedItem.Value))
        End If
        params.Add(New SqlParameter("@Site_Production_Line_Name", txtProductionLine.Text))
        params.Add(New SqlParameter("@Site_ID", DropDownListPlant.SelectedItem.Value))
        params.Add(New SqlParameter("@Platform_ID", DropDownListPlatform.SelectedItem.Value))
        params.Add(New SqlParameter("@BU_ID", DropDownListBusinessUnit.SelectedItem.Value))
        params.Add(New SqlParameter("@Production_Type_ID", DropDownListProductionType.SelectedItem.Value))
        params.Add(New SqlParameter("@Operation", mOperation))
        ExecuteProcedure("CMPDB_sp_InsertUpdate_tblSite_Production_Lines", params)
    End Sub

    Protected Sub BtnRefreshProductionLine_Click(sender As Object, e As EventArgs)
        ResetControls(ListBoxProductionLine)
        ResetControls(txtProductionLine)
        btnAddProductionLine.ImageUrl = xAddImagePath
        populateProductionLines()
    End Sub

    Protected Sub ListBoxProductionLine_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBox(txtProductionLine, ListBoxProductionLine, btnAddProductionLine)
    End Sub

    Protected Sub btnAddProductionLineDetails_Click(sender As Object, e As EventArgs)
        Dim mPlatform = ""
        If DropDownListPlant.SelectedItem.Value = Nothing Then
            Exit Sub
        End If

        If ListBoxDepartments.SelectedIndex < 0 Then
            Exit Sub
        End If

        If ListBoxCNB.SelectedIndex < 0 Then
            Exit Sub
        End If
        If ListBoxProductionLine.SelectedIndex < 0 Then
            Exit Sub
        End If
        If DropDownListPlatform.SelectedIndex < 0 Then
            mPlatform = DropDownListPlatform.SelectedItem.Value
        End If
        Dim query As String
        query = "insert into " + xTblNameSitePlantDetails + " values( " + DropDownListPlant.SelectedItem.Value + "," + DropDownListBusinessUnit.SelectedItem.Value + "," + mPlatform + "," + DropDownListProductionType.SelectedItem.Value + "," + ListBoxDepartments.SelectedItem.Value + "," + ListBoxCNB.SelectedItem.Value + "," + ListBoxProductionLine.SelectedItem.Value + "," + " ' " + DateTime.Now + "'" + ")"
        RunSQLQuery(query)

    End Sub


#End Region

#Region "CBNs Section"

    Protected Sub ButtonDeleteCBN_Click(sender As Object, e As ImageClickEventArgs)
        DeleteRecordsListControls(xTblNameCBNS_Types + ",'" + ListBoxCNB.SelectedItem.Value + "'")
        ButtonRefreshCBN_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub ButtonAddCBN_Click(sender As Object, e As EventArgs)
        If DropDownListPlant.SelectedValue = "0" Then
            MessageBox("Please Select Plant !!")
            Exit Sub
        End If
        Dim strAddUpdate As String = String.Empty
        If ButtonAddCBN.ImageUrl = xAddImagePath Then
            strAddUpdate = xTblNameCBNS_Types + ",'Site_CBN,Site_ID',I,'''" + txtCBN.Text + "'',''" + DropDownListPlant.SelectedItem.Value + "''','',Site_CBN_ID"
        ElseIf ButtonAddCBN.ImageUrl = xUpdateImagePath Then
            If txtCBN.Text <> ListBoxCNB.SelectedItem.Text Then
                strAddUpdate = xTblNameCBNS_Types + ",'Site_CBN=''" + txtCBN.Text + "''',U,''," + ListBoxCNB.SelectedItem.Value + ",Site_CBN_ID"
            Else
                MessageBox("Duplicate Record.")
                Exit Sub
            End If
        End If
        AddUpdateRecordsDependentListControls(strAddUpdate)
        ButtonRefreshCBN_Click(sender, EventArgs.Empty)
    End Sub

    Protected Sub ButtonRefreshCBN_Click(sender As Object, e As EventArgs)
        ResetControls(ListBoxCNB)
        ResetControls(txtCBN)
        ButtonAddCBN.ImageUrl = xAddImagePath
        populateCBNs(DropDownListPlant.SelectedItem.Value)
        '  ShowHideDependentsCBN("none")
    End Sub

    Protected Sub ListBoxCNB_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBox(txtCBN, ListBoxCNB, ButtonAddCBN)
    End Sub
#End Region

#Region "Departments Section"
    Protected Sub ListBoxDepartments_SelectedIndexChanged(sender As Object, e As EventArgs)
        FillTextBox(txtDepartments, ListBoxDepartments, ButtonAddDepartments)
    End Sub
    Protected Sub ButtonDeleteDepartments_Click(sender As Object, e As EventArgs)
        DeleteRecordsListControls(xTblNameDepartment_Types + ",'" + ListBoxDepartments.SelectedItem.Value + "'")
        ButtonRefreshDepartments_Click(sender, EventArgs.Empty)
    End Sub
    Protected Sub ButtonAddDepartments_Click(sender As Object, e As EventArgs)
        If DropDownListPlant.SelectedValue = "0" Then
            MessageBox("Please Select Plant !!")
            Exit Sub
        End If
        Dim strAddUpdate As String = String.Empty
        If ButtonAddDepartments.ImageUrl = xAddImagePath Then
            strAddUpdate = xTblNameDepartment_Types + ",'Site_Department,Site_ID',I,'''" + txtDepartments.Text + "'',''" + DropDownListPlant.SelectedItem.Value + "''','',Site_Department_ID"
        ElseIf ButtonAddDepartments.ImageUrl = xUpdateImagePath Then
            If txtDepartments.Text <> ListBoxDepartments.SelectedItem.Text Then
                strAddUpdate = xTblNameDepartment_Types + ",'Site_Department=''" + txtDepartments.Text + "''',U,''," + ListBoxDepartments.SelectedItem.Value + ",Site_Department_ID"

            Else
                MessageBox("Duplicate Record.")
                Exit Sub
            End If
        End If
        AddUpdateRecordsDependentListControls(strAddUpdate)
        ButtonRefreshDepartments_Click(sender, EventArgs.Empty)
    End Sub
    Protected Sub ButtonRefreshDepartments_Click(sender As Object, e As EventArgs)
        ResetControls(ListBoxDepartments)
        ResetControls(txtDepartments)
        ButtonAddDepartments.ImageUrl = xAddImagePath
        populateDepartments(DropDownListPlant.SelectedItem.Value)
        '   ShowHideDependentsDepartments("none")
    End Sub
#End Region

#Region "Source Files Section (Magic Begins Here)"

    Protected Sub DropDownListPlant_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Session("HasFiles") = "0"
            If DropDownListPlant.SelectedIndex = 0 Then
                Exit Sub
            Else
                LoadAllDependents()
                ResetControls(DropDownListBusinessUnit, DropDownListPlatform, DropDownListProductionType)
                LoadSourceFilesSection()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LoadSourceFilesSection()
        RadioButtonUseCorporate.Checked = True
        RadioButtonSingleSource.Checked = False

        If PopulateWorkSheetCellFileName() = 1 Then
            Session("HasFiles") = "1"

            ShowHideControls(False, fuEWPTracker, fuGSUMMFGOps, fuGSUMMTL, fuGSUMPRWS, fuGsumSmallWS, fuGSUM_IAP, fuGSUM_PRRA, fuMFGDel, fuRE_Implementation)
            ShowHideEditButton()
        Else
            If RadioButtonUseCorporate.Checked Then
                populateCStandards()
                ShowHideControls(False, fuEWPTracker, fuGSUMMFGOps, fuGSUMMTL, fuGSUMPRWS, fuGsumSmallWS, fuGSUM_IAP, fuGSUM_PRRA, fuMFGDel, fuRE_Implementation)
            Else
                ShowHideFileUploadControls()
            End If
        End If
    End Sub

    Private Sub ShowHideEditButton()
        If RadioButtonSingleSource.Checked = True Then
            XaVisibility = "block"
        End If
    End Sub

    Private Function PopulateWorkSheetCellFileName()

        Dim mWorksheetCell = "WorkSheet_Cell_Address"
        Dim mWorksheetName = "WorkSheet_Name"
        Dim mFileName = "FileName"
        Dim mBLOBID = "BLOBID"

        Dim dtBLOBDetails As DataTable
        Dim list As List(Of KeyValuePair(Of String, Integer)) = Session("ToolList")
        Dim list1 As List(Of Integer) = (From ll In list Select ll.Value).ToList()
        Dim str As String = String.Join(",", list1)

        Dim mQuery = "select * from " + xTblNametableSource + " c join CMPDB_tblBLOBFiles b on b.BLOBFile_ID= c.BLOBFile_ID where Site_ID=" +
                      DropDownListPlant.SelectedItem.Value + " and SWP_Tool_Name_ID in(" + str + ") and c.BLOBFile_ID in(select BLOBFile_ID from CMPDB_tblBLOBFiles where IsActive=1)"

        dtBLOBDetails = GetDataTableFromSQL(mQuery)
        If dtBLOBDetails.Rows.Count = 0 Then
            mQuery = $"select * from  {xTblNametableSource}  c join CMPDB_tblCorporateSourcesBLOBFiles b on c.BLOBFile_ID= b.BLOBFile_ID  where Site_ID=
            {DropDownListPlant.SelectedItem.Value}  And SWP_Tool_Name_ID in( {str} ) And c.BLOBFile_ID in(select BLOBFile_ID 
            from CMPDB_tblCorporateSourcesBLOBFiles where IsActive=1)"
            dtBLOBDetails = GetDataTableFromSQL(mQuery)
            If dtBLOBDetails.Rows.Count = 0 Then
                Return 0
            Else
                PopulateWorkSheetCellFileNameInner(mWorksheetCell, mWorksheetName, mFileName, dtBLOBDetails)
                Return 1
            End If
        Else
            PopulateWorkSheetCellFileNameInner(mWorksheetCell, mWorksheetName, mFileName, dtBLOBDetails)
            Return 1
        End If
    End Function

    Private Sub PopulateWorkSheetCellFileNameInner(mWorksheetCell As String, mWorksheetName As String, mFileName As String, dtBLOBDetails As DataTable)
        ToggleRadioButtons(CType(dtBLOBDetails.Rows(0)("IsSingleSourceFile"), Boolean))

        txtCellGsumSmallWS.Text = dtBLOBDetails.Rows(0)(mWorksheetCell).ToString()
        txtCellGSUMMTL.Text = dtBLOBDetails.Rows(1)(mWorksheetCell).ToString()
        txtCellGSUMPRWS.Text = dtBLOBDetails.Rows(2)(mWorksheetCell).ToString()
        txtCellGSUMMFGOps.Text = dtBLOBDetails.Rows(3)(mWorksheetCell).ToString()
        txtCellMFGDel.Text = dtBLOBDetails.Rows(4)(mWorksheetCell).ToString()
        txtCellEWPTracker.Text = dtBLOBDetails.Rows(5)(mWorksheetCell).ToString()
        txtCellGSUM_IAP.Text = dtBLOBDetails.Rows(6)(mWorksheetCell).ToString()
        txtCellRE_Implementation.Text = dtBLOBDetails.Rows(7)(mWorksheetCell).ToString()
        'txtCellGSUM_PRRA.Text = dtBLOBDetails.Rows(8)(mWorksheetCell).ToString()

        txtGsumSmallWS.Text = dtBLOBDetails.Rows(0)(mWorksheetName).ToString()
        txtGSUMMTL.Text = dtBLOBDetails.Rows(1)(mWorksheetName).ToString()
        txtGSUMPRWS.Text = dtBLOBDetails.Rows(2)(mWorksheetName).ToString()
        txtGSUMMFGOps.Text = dtBLOBDetails.Rows(3)(mWorksheetName).ToString()
        txtMFGDel.Text = dtBLOBDetails.Rows(4)(mWorksheetName).ToString()
        txtEWPTracker.Text = dtBLOBDetails.Rows(5)(mWorksheetName).ToString()
        txtGSUM_IAP.Text = dtBLOBDetails.Rows(6)(mWorksheetName).ToString()
        txtRE_Implementation.Text = dtBLOBDetails.Rows(7)(mWorksheetName).ToString()
        'txtGSUM_PRRA.Text = dtBLOBDetails.Rows(8)(mWorksheetName).ToString()

        lblfuGsumSmallWS.Text = dtBLOBDetails.Rows(0)(mFileName).ToString()
        lbl_file_fuGSUMMTL.Text = dtBLOBDetails.Rows(1)(mFileName).ToString()
        lbl_file_fuGSUMPRWS.Text = dtBLOBDetails.Rows(2)(mFileName).ToString()
        lbl_file_fuGSUMMFGOps.Text = dtBLOBDetails.Rows(3)(mFileName).ToString()
        lbl_file_fuMFGDel.Text = dtBLOBDetails.Rows(4)(mFileName).ToString()
        lbl_file_fuEWPTracker.Text = dtBLOBDetails.Rows(5)(mFileName).ToString()
        lbl_file_fuGSUM_IAP.Text = dtBLOBDetails.Rows(6)(mFileName).ToString()
        lbl_file_fuRE_Implementation.Text = dtBLOBDetails.Rows(7)(mFileName).ToString()
        'lbl_file_fuGSUM_PRRA.Text = dtBLOBDetails.Rows(8)(mFileName).ToString()
    End Sub

    Protected Sub btnSaveSource_Click(sender As Object, e As EventArgs)

        If RadioButtonUseCorporate.Checked = True Then
            SaveCorporateSourceFiles()
            MessageBox("Successfully Saved !!")
        Else
            SaveSingleSourceFiles()
            MessageBox("Successfully Saved !!")
        End If
    End Sub

    Private Sub SaveSingleSourceFiles()
        Session("executed") = 0
        If fuGsumSmallWS.HasFile Then
            Dim file As System.IO.Stream = fuGsumSmallWS.PostedFile.InputStream
        End If
        Dim mBLOBIDFirst As Int64
        Dim list As List(Of KeyValuePair(Of String, Integer)) = Session("ToolList")
        If fuGsumSmallWS.HasFile Then
            Session("HasFiles") = 0
            mBLOBIDFirst = AddFilesSiteFilesToPlant(txtGsumSmallWS, txtCellGsumSmallWS, fuGsumSmallWS, list(0).Value, True, 0)
        Else
            If Session("HasFiles") = 1 Then

                AddRemainingFilesToPlant(list, mBLOBIDFirst)
                Exit Sub

            Else
                Exit Sub
            End If
        End If
        AddRemainingFilesToPlant(list, mBLOBIDFirst)
        ResetControls(txtGsumSmallWS, txtCellGsumSmallWS, txtGSUMMTL, txtCellGSUMPRWS, txtGSUMPRWS, txtCellGSUMMFGOps, txtGSUMMFGOps, txtCellMFGDel, txtMFGDel, txtCellEWPTracker, txtEWPTracker, txtCellGSUM_IAP, txtDirectory)
        ShowHideFilesSection()
        Session("executed") = 0
    End Sub

    Private Sub AddRemainingFilesToPlant(ByRef list As List(Of KeyValuePair(Of String, Integer)), ByRef mBLOBIDFirst As String)
        AddFilesSiteFilesToPlant(txtGSUMMTL, txtCellGSUMMTL, fuGSUMMTL, list(1).Value, fuGSUMMTL.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtGSUMPRWS, txtCellGSUMPRWS, fuGSUMPRWS, list(2).Value, fuGSUMPRWS.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtGSUMMFGOps, txtCellGSUMMFGOps, fuGSUMMFGOps, list(3).Value, fuGSUMMFGOps.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtMFGDel, txtCellMFGDel, fuMFGDel, list(4).Value, fuMFGDel.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtEWPTracker, txtCellEWPTracker, fuEWPTracker, list(5).Value, fuEWPTracker.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtGSUM_IAP, txtCellGSUM_IAP, fuGSUM_IAP, list(6).Value, fuGSUM_IAP.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtRE_Implementation, txtCellRE_Implementation, fuRE_Implementation, list(7).Value, fuRE_Implementation.HasFile, mBLOBIDFirst)
        AddFilesSiteFilesToPlant(txtGSUM_PRRA, txtCellGSUM_PRRA, fuGSUM_PRRA, list(8).Value, fuGSUM_PRRA.HasFile, mBLOBIDFirst)
    End Sub

    Private Sub SaveCorporateSourceFiles()
        Dim list As List(Of KeyValuePair(Of String, Integer)) = Session("ToolList")
        AddFilesSiteCorporateFiles(txtGsumSmallWS, txtCellGsumSmallWS, fuGsumSmallWS, list(0).Value, True)
        AddFilesSiteCorporateFiles(txtGSUMMTL, txtCellGSUMMTL, fuGSUMMTL, list(1).Value, fuGSUMMTL.HasFile)
        AddFilesSiteCorporateFiles(txtGSUMPRWS, txtCellGSUMPRWS, fuGSUMPRWS, list(2).Value, fuGSUMPRWS.HasFile)
        AddFilesSiteCorporateFiles(txtGSUMMFGOps, txtCellGSUMMFGOps, fuGSUMMFGOps, list(3).Value, fuGSUMMFGOps.HasFile)
        AddFilesSiteCorporateFiles(txtMFGDel, txtCellMFGDel, fuMFGDel, list(4).Value, fuMFGDel.HasFile)
        AddFilesSiteCorporateFiles(txtEWPTracker, txtCellEWPTracker, fuEWPTracker, list(5).Value, fuEWPTracker.HasFile)
        AddFilesSiteCorporateFiles(txtGSUM_IAP, txtCellGSUM_IAP, fuGSUM_IAP, list(6).Value, fuGSUM_IAP.HasFile)
        AddFilesSiteCorporateFiles(txtRE_Implementation, txtCellRE_Implementation, fuRE_Implementation, list(7).Value, fuRE_Implementation.HasFile)
        AddFilesSiteCorporateFiles(txtGSUM_PRRA, txtCellGSUM_PRRA, fuGSUM_PRRA, list(8).Value, fuGSUM_PRRA.HasFile)
        ResetControls(txtGsumSmallWS, txtEWPTracker, txtRE_Implementation, txtCellGSUM_PRRA, txtGSUM_PRRA, txtCellRE_Implementation, txtCellGSUMMTL, txtCellGsumSmallWS, txtGSUMMTL, txtCellGSUMPRWS, txtGSUMPRWS, txtCellGSUMMFGOps, txtGSUMMFGOps, txtCellMFGDel, txtMFGDel, txtCellEWPTracker, txtEWPTracker, txtCellGSUM_IAP, txtDirectory)
    End Sub

    Private Sub AddFilesSiteCorporateFiles(mtxtSheetName As TextBox, mtxtCellAddress As TextBox, ByRef mFileObject As FileUpload, key As String, hasFile As Boolean)
        Dim dt As DataTable = GetDataTableFromSQL("select * from CMPDB_tblCorporateSourcesBLOBFiles f join CMPDB_tblCorporateSources c on c.BLOBFile_ID=f.BLOBFile_ID where SWP_Tool_Name_ID= " + key)

        Dim mIsSingleSourceFile As Boolean = True
        If RadioButtonSingleSource.Checked Then
            mIsSingleSourceFile = True
        Else
            mIsSingleSourceFile = False
        End If

        Dim mPlantID = DropDownListPlant.SelectedItem.Value
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@Site_ID", Convert.ToInt32(mPlantID)))
        params.Add(New SqlParameter("@SWP_Tool_Name_ID", Convert.ToInt32(key)))
        params.Add(New SqlParameter("@Worksheet_Name", mtxtSheetName.Text))
        params.Add(New SqlParameter("@Worksheet_Cell_Address", mtxtCellAddress.Text))
        params.Add(New SqlParameter("@FileObject", dt.Rows(0)("FileObject")))
        params.Add(New SqlParameter("@FileName", dt.Rows(0)("FileName")))
        params.Add(New SqlParameter("@Operation", "I"))
        params.Add(New SqlParameter("@IsSingleSourceFile", mIsSingleSourceFile))
        params.Add(New SqlParameter("@IsFile", True))

        ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPBD_tblSourceFilesBLOB", params)
        'fuGsumSmallWS.PostedFile.InputStream.Position = 0

    End Sub

    Private Function AddFilesSiteFilesToPlant(mtxtSheetName As ITextControl, mtxtCellAddress As ITextControl, ByRef mFileObject As FileUpload, key As String, hasFile As Boolean, mBLOBID As Int64)
        If Not hasFile And mBLOBID = 0 Then
            Exit Function
        End If
        Dim mOperation = If(Session("HasFiles") = 0, "I", "U")
        Dim mPlantID = DropDownListPlant.SelectedItem.Value
        Dim mOldBLOBID As Int64
        Dim mIsSingleSourceFile As Boolean = RadioButtonSingleSource.Checked

        Dim outparam As New SqlParameter("@FirstBLOBID", mOldBLOBID, ParameterDirection.Output)
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter("@Site_ID", Convert.ToInt32(mPlantID)))
        params.Add(New SqlParameter("@SWP_Tool_Name_ID", Convert.ToInt32(key)))
        params.Add(New SqlParameter("@Worksheet_Name", mtxtSheetName.Text))
        params.Add(New SqlParameter("@Worksheet_Cell_Address", mtxtCellAddress.Text))

        params.Add(New SqlParameter("@Operation", mOperation))
        params.Add(New SqlParameter("@IsSingleSourceFile", mIsSingleSourceFile))
        params.Add(New SqlParameter("@IsFile", hasFile))
        params.Add(New SqlParameter("@BLOBFile_ID", mBLOBID))

        If mFileObject.HasFile Then
            params.Add(New SqlParameter("@FileObject", mFileObject.FileContent))
            params.Add(New SqlParameter("@FileName", mFileObject.FileName))
        End If
        If Session("executed") = 1 Then
            ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPBD_tblSourceFilesBLOB", params)
        Else
            If hasFile And mBLOBID = 0 And Session("executed") = 1 Then
                ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPBD_tblSourceFilesBLOB", params)
                Exit Function
            Else
                ''For Update
                If Session("HasFiles") = "1" Then
                    ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPBD_tblSourceFilesBLOB", params)
                Else
                    mOldBLOBID = ExecuteProcedure("CMPDB_sp_InsertNUpdate_CMPBD_tblSourceFilesBLOB", params, outparam)
                End If
            End If
        End If
        If fuGsumSmallWS.HasFile Then
            fuGsumSmallWS.PostedFile.InputStream.Position = 0
        End If
        Session("executed") = 1

        Return mOldBLOBID
    End Function

    Protected Sub RadioButtonUseCorporate_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonUseCorporate.CheckedChanged
        ShowHideFilesSection()
    End Sub

    Protected Sub RadioButtonSingleSource_CheckedChanged(sender As Object, e As EventArgs)
        ShowHideFilesSection()
    End Sub

    Private Sub ShowHideFilesSection()
        If RadioButtonUseCorporate.Checked Then
            ShowHideControls(False, fuEWPTracker, fuGSUMMFGOps, fuGSUMMTL, fuGSUMPRWS, fuGsumSmallWS, fuGSUM_IAP, fuGSUM_PRRA, fuMFGDel, fuRE_Implementation)
            populateCStandards()
        End If
        If RadioButtonSingleSource.Checked Then
            If Session("HasFiles") = 0 Then
                ShowHideControls(True, fuEWPTracker, fuGSUMMFGOps, fuGSUMMTL, fuGSUMPRWS, fuGsumSmallWS, fuGSUM_IAP, fuGSUM_PRRA, fuMFGDel, fuRE_Implementation)
                fuGsumSmallWS.Attributes.Add("onchange", "return validateFileUpload(this);")
                ResetControls(txtCellEWPTracker, txtCellGSUMMFGOps, txtCellGSUMMTL, txtCellGSUMPRWS, txtCellGsumSmallWS, txtCellGSUM_IAP, txtCellGSUM_PRRA, txtCellMFGDel, txtCellRE_Implementation, txtEWPTracker, txtGSUMMFGOps, txtGSUMMTL, txtGSUMPRWS, txtGsumSmallWS, txtGSUM_IAP, txtGSUM_PRRA, txtMFGDel, txtRE_Implementation)
                ResetControls(lblfuGsumSmallWS, lbl_file_fuGSUMMTL, lbl_file_fuGSUMPRWS, lbl_file_fuGSUMMFGOps, lbl_file_fuMFGDel, lbl_file_fuEWPTracker, lbl_file_fuGSUM_IAP, lbl_file_fuRE_Implementation, lbl_file_fuGSUM_PRRA)
            Else
                PopulateWorkSheetCellFileName()
            End If
        End If
    End Sub

    Protected Sub lblfuGsumSmallWS_Click(sender As Object, e As EventArgs) Handles lbl_file_fuGSUMMTL.Click, lbl_file_fuGSUMPRWS.Click, lbl_file_fuGSUMMFGOps.Click, lbl_file_fuMFGDel.Click, lbl_file_fuEWPTracker.Click, lbl_file_fuGSUM_IAP.Click, lbl_file_fuRE_Implementation.Click, lbl_file_fuGSUM_PRRA.Click
        Dim b = CType(sender, LinkButton)
        Dim id = b.ID

        If id = "lblfuGsumSmallWS" Then
            id = 1
        ElseIf id = "lbl_file_fuGSUMMTL" Then
            id = 2
        ElseIf id = "lbl_file_fuGSUMPRWS" Then
            id = 3
        ElseIf id = "lbl_file_fuGSUMMFGOps" Then
            id = 4

        ElseIf id = "lbl_file_fuMFGDel" Then
            id = 5
        ElseIf id = "lbl_file_fuEWPTracker" Then
            id = 6
        ElseIf id = "lbl_file_fuGSUM_IAP" Then
            id = 7
        ElseIf id = "lbl_file_fuRE_Implementation" Then
            id = 8
        ElseIf id = "lbl_file_fuGSUM_PRRA" Then
            id = 9
        End If
        Dim mQry = ""
        If RadioButtonUseCorporate.Checked Then
            mQry = $"select crtnid.SWP_Tool_Name_ID,tfile.filename,tfile.FileObject,tfile.FileUploaded from  
                    CMPDB_tblCorporateSourcesBLOBFiles tfile inner join CMPDB_tblCorporateSources crtnid 
                    on tfile.BLOBFile_ID=crtnid.SWP_Tool_Name_ID  where crtnid.SWP_Tool_Name_ID='{id}'"
        Else
            mQry = $"SELECT 
                    crtnid.SWP_Tool_Name_ID,tfile.filename,tfile.FileObject,tfile.FileUploaded 
                    FROM 
                    CMPDB_tblBLOBFiles tfile inner join 
                    CMPDB_tblSource_Files crtnid ON 
                    tfile.BLOBFile_ID=crtnid.BLOBFile_ID
                    WHERE crtnid.SWP_Tool_Name_ID={id} AND Site_ID={DropDownListPlant.SelectedItem.Value}"
        End If
        Dim dt As DataTable = GetDataTableFromSQL(mQry)
        If dt.Rows.Count > 0 Then
            Dim bytes() = CType(dt.Rows(0)("FileObject"), Byte())
            download(bytes, dt.Rows(0)("filename").ToString())
        End If
    End Sub
#End Region
End Class