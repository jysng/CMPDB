Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.SqlServer.Management.Sdk.Sfc

Imports Microsoft.SqlServer.Management.Smo

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

    Protected Sub btnID_Click1(sender As Object, e As EventArgs)
        'Dim var = GenrateScripts()
        'File.AppendAllText(Server.MapPath("~/Jai2.sql"), var, Encoding.UTF8)
    End Sub

    'Private Function GenrateScripts()
    '    ' Connect to the local, default instance of SQL Server. 
    '    Dim str = "sitecoresql"
    '    Dim srv As New Server(str)

    '    ' Reference the database.  
    '    Dim db As Database = srv.Databases("cmpdb_dev")

    '    Dim scrp As New Scripter(srv)
    '    scrp.Options.ScriptDrops = True
    '    scrp.Options.WithDependencies = True
    '    scrp.Options.Indexes = True
    '    ' To include indexes
    '    scrp.Options.DriAllConstraints = True
    '    ' to include referential constraints in the script
    '    scrp.Options.Triggers = True
    '    scrp.Options.FullTextIndexes = True
    '    scrp.Options.NoCollation = False
    '    scrp.Options.Bindings = True
    '    scrp.Options.IncludeIfNotExists = False
    '    scrp.Options.ScriptBatchTerminator = True
    '    scrp.Options.ExtendedProperties = True
    '    scrp.Options.DriForeignKeys = False
    '    scrp.Options.ScriptData = True

    '    scrp.PrefetchObjects = True
    '    ' some sources suggest this may speed things up
    '    Dim urns = New List(Of Urn)()

    '    ' Iterate through the tables in database and script each one   
    '    For Each tb As Table In db.Tables
    '        ' check if the table is not a system table
    '        If tb.IsSystemObject = False Then
    '            urns.Add(tb.Urn)
    '        End If
    '    Next

    '    ' Iterate through the views in database and script each one. Display the script.   
    '    For Each view As View In db.Views
    '        ' check if the view is not a system object
    '        If view.IsSystemObject = False Then
    '            urns.Add(view.Urn)
    '        End If
    '    Next

    '    ' Iterate through the stored procedures in database and script each one. Display the script.   
    '    For Each sp As StoredProcedure In db.StoredProcedures
    '        ' check if the procedure is not a system object
    '        If sp.IsSystemObject = False Then
    '            urns.Add(sp.Urn)
    '        End If
    '    Next



    '    Dim builder As New StringBuilder()
    '    Dim sc As System.Collections.Specialized.StringCollection = scrp.Script(urns.ToArray())
    '    For Each st As String In sc
    '        ' It seems each string is a sensible batch, and putting GO after it makes it work in tools like SSMS.
    '        ' Wrapping each string in an 'exec' statement would work better if using SqlCommand to run the script.
    '        builder.AppendLine(st)
    '        builder.AppendLine("GO")
    '    Next




    '    Return builder.ToString()

    'End Function
End Class