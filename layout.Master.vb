﻿Imports System.Data.SqlClient

Public Class layout
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strSiteMap As String
            'look up the current users role and display the appropriate menu
            Session("User_ID") = GetUserName()
            Session("UserRole") = GetUserRole(Session("User_ID"))
            strSiteMap = GetSiteMap()
            Session("strSiteMap") = strSiteMap
            SiteMapDataSource1.SiteMapProvider = strSiteMap

            If Session("plant_Set") Is Nothing Then
                Session("plant_Set") = GetDefaultPlant()
            End If
        End If

    End Sub
    ' Get Default Plant of User
    Private ReadOnly Property GetDefaultPlant As Object
        Get
            Return GetSingleValue($"select plant_id from CMPDB_tblDefault_Locations where User_ID='{ Session("User_ID")}'")
        End Get
    End Property

    Function GetSiteMap() As String

        Dim cn As SqlConnection
        Dim cmd As SqlCommand
        Dim sql As String
        cn = New SqlConnection(strConnectionString)

        ' sql = "SELECT site_map FROM roles WHERE role_id = 1;" '& Session("UserRole") & ";"
        sql = "SELECT site_map FROM [CMPDB_tblroles] WHERE role_id = " & Session("UserRole")

        GetSiteMap = "Unknown"

        Try
            cn.Open()
            cmd = New SqlCommand(sql, cn)

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()
            While sqlReader.Read()
                GetSiteMap = sqlReader.Item("site_map").ToString()
            End While

            sqlReader.Close()
            cmd.Dispose()
            cn.Close()

        Catch ex As Exception

            MsgBox("Can not open connection!")

        End Try

    End Function


    Protected Sub Menu_MenuItemDataBound(sender As Object, e As MenuEventArgs) Handles Menu.MenuItemDataBound
        Dim menu As System.Web.UI.WebControls.Menu = DirectCast(sender, System.Web.UI.WebControls.Menu)
        Dim mapNode As SiteMapNode = DirectCast(e.Item.DataItem, SiteMapNode)
        Dim itemToRemove As MenuItem = menu.FindItem(mapNode.Title)


        If mapNode.Title = "StartUp Targets" Then
            Dim parent As MenuItem = e.Item.Parent
            If parent IsNot Nothing Then

                parent.ChildItems.Remove(e.Item)


            End If
        End If
    End Sub
End Class