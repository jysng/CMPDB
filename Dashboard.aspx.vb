Imports System.Data.SqlClient

Public Class Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("User_ID") = ""



        Session("User_ID") = GetUserName()
        Session("UserRole") = GetUserRole(Session("User_ID"))

        If Session("UserRole") = 2 Then
            trGlobal_Admin.Visible = False
            trBtnGlobal_Admin.Visible = False
        Else
            trGlobal_Admin.Visible = True
            trBtnGlobal_Admin.Visible = True
        End If

        If Not IsPostBack Then
            PopulateDD(DDlplants, " CMPDB_tblPlants", "Plant_ID", "Plant")

            Dim mPlantID = GetSingleValue($"Select Plant_ID from CMPDB_tblDefault_Locations where user_id='{Session("User_ID")}'")
            DDlplants.SelectedValue = mPlantID
        End If

    End Sub

    Protected Sub BtnAdmin_Click(sender As Object, e As EventArgs) Handles BtnAdmin.Click
        Response.Redirect("GlobalAdmin.aspx")
    End Sub
    Protected Sub BtnAdminforPlant_Click(sender As Object, e As EventArgs) Handles BtnAdminforPlant.Click
        Response.Redirect("AdministratorPlant.aspx")
    End Sub

    Protected Sub BtnSet_Click(sender As Object, e As EventArgs) Handles BtnSet.Click
        Dim params As New List(Of SqlParameter)
        Session.Remove("plant_Set")
        Session("plant_Set") = DDlplants.SelectedValue
        params.Add(New SqlParameter("@UserID", Session("User_ID").ToString))
        params.Add(New SqlParameter("@PlantID", Convert.ToInt32(DDlplants.SelectedItem.Value)))

        ExecuteProcedure("CMPDB_sp_InsertUpdate_tblDefualt_Locations", params)
    End Sub

    Protected Sub BtnCreatePorject_Click(sender As Object, e As EventArgs)
        Response.Redirect("Admin_Project.aspx")
    End Sub

    Protected Sub btnID_Click(sender As Object, e As EventArgs)
        ' UploadFileToServer(fuFile, "")
    End Sub

    Protected Sub BtnEditPractitioners_Click(sender As Object, e As EventArgs)
        Response.Redirect("Practitioner.aspx")
    End Sub

    Protected Sub BtnEditMeasures_Click(sender As Object, e As EventArgs)
        Response.Redirect("EditMeasures.aspx")
    End Sub

    Protected Sub BtnReports_Click(sender As Object, e As EventArgs)
        Response.Redirect("Report.aspx")
    End Sub
End Class