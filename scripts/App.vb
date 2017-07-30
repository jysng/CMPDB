Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail

Module App
    Public Function ReadFromDB(mReportTempleteCode As String)
        Dim mSystem_generated_report_processing_configurations = "CMPDB_tblsystem_generated_report_processing_configurations"
        Dim mReport_template_lib = "[CMPDB_tblreport_template_lib]"
        Dim bytes As Byte()
        Dim fileName As String, contentType As String

        Dim con As New SqlConnection(strConnectionString)
        Dim dtconfig As DataTable
        dtconfig = GetDataTableFromSQL($"select * from {mSystem_generated_report_processing_configurations} where report_code='" + mReportTempleteCode + "'")

        con.Open()
        Dim mMaxValue As String = dtconfig(0)("report_template_id").ToString()
        Using cmd As New SqlCommand()
            cmd.CommandText = $"select application, file_data, file_type from  { mReport_template_lib } where [Lib_ref_id]=@Id"
            cmd.Parameters.AddWithValue("@Id", mMaxValue)
            cmd.Connection = con
            'con.Open();
            Dim adapter As New SqlDataAdapter(cmd)
            Dim DS As New DataSet
            adapter.Fill(DS)
            bytes = DirectCast(DS.Tables(0).Rows(0)("file_data"), Byte())
            contentType = DS.Tables(0).Rows(0)("file_type").ToString()
            fileName = DS.Tables(0).Rows(0)("application").ToString()
            'Put excel as bytes array into memory stream
            Dim ms As New MemoryStream(bytes)
            'Put memory stream into in new workbook object so that we can append data into it
            Dim mTemplateData As New TemplateData(dtconfig(0)("Excel_Tab_Name").ToString(), dtconfig(0)("Output_File_Extension").ToString(), ms)
            Return mTemplateData
        End Using
    End Function


    Public Sub GetParentSiteMapNode()
        HttpContext.Current.Session("url") = False
        If Not HttpContext.Current.Session("strSiteMap") Is Nothing Then

            For Each cNode As SiteMapNode In SiteMap.Providers(HttpContext.Current.Session("strSiteMap")).RootNode.ChildNodes
                WalkTree(cNode)
            Next

            If HttpContext.Current.Session("url") = False Then
                HttpContext.Current.Response.Redirect("Dashboard.aspx")
            End If
        End If




    End Sub


    Public Sub WalkTree(ByVal pNode As SiteMapNode)
        For Each child As SiteMapNode In pNode.ChildNodes
            If child.HasChildNodes Then
                WalkTreeChild(child)
            Else
                If ((child.Url.Length > 0) And (child.Title.Length > 0)) Then
                    If child.Url.Contains(HttpContext.Current.Request.Url.AbsolutePath) Then
                        HttpContext.Current.Session("url") = True
                    End If
                End If
            End If
        Next

    End Sub

    Public Sub WalkTreeChild(ByVal pNode As SiteMapNode)
        For Each child As SiteMapNode In pNode.ChildNodes


            If ((child.Url.Length > 0) And (child.Title.Length > 0)) Then
                If child.Url.Contains(HttpContext.Current.Request.Url.AbsolutePath) Then
                    HttpContext.Current.Session("url") = True
                End If
            End If
        Next

    End Sub

    'Public Sub MessageBox(str As String)
    '    Dim page As Page = HttpContext.Current.Handler
    '    page.ClientScript.RegisterClientScriptBlock(HttpContext.Current.[GetType](), "validation", $"<script language='javascript'>alert('{str}')</script>")
    'End Sub


    Public Sub MessageBox(str As String)
        Try
            Dim page As Page = HttpContext.Current.Handler
            Dim upnl = page.Controls(0).Controls(3).FindControl("ContentPlaceHolder1").FindControl("upnl")
            ScriptManager.RegisterStartupScript(page, upnl.[GetType](), "validation", $"ShowPopUp('{str}');", True)
        Catch ex As Exception

        End Try

        'ScriptManager.RegisterStartupScript(page, upnl.[GetType](), "validation", $"<script language='javascript'>alert('{str}')</script>", False)
    End Sub
    Public Sub MessageBox(str As String, up As UpdatePanel, IsError As Boolean)
        Dim page As Page = HttpContext.Current.Handler
        ScriptManager.RegisterStartupScript(page, up.[GetType](), "validation", $"<script language='javascript'>alert('{str}')</script>", False)
    End Sub

    Dim previousSelected As Integer
    Public Sub HighlightRow(gridPractitioner As GridView, e As GridViewCommandEventArgs)
        Dim rows As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        If HttpContext.Current.Session("previousSelected") IsNot Nothing Then
            previousSelected = Convert.ToInt32(HttpContext.Current.Session("previousSelected"))
        End If
        Dim selectedRow As GridViewRow = gridPractitioner.Rows(previousSelected)
        selectedRow.Style.Add("background-color", "#f9f9f9") 'change it back to original color
        selectedRow = gridPractitioner.Rows(rows.RowIndex)
        selectedRow.Style.Add("background-color", "#dddddd") 'change the color of the new row
        HttpContext.Current.Session("previousSelected") = rows.RowIndex

    End Sub


    Public NotInheritable Class Logger
        Private Sub New()
        End Sub
        Private Shared Property Log() As log4net.ILog
            Get
                Return m_Log
            End Get
            Set
                m_Log = Value
            End Set
        End Property
        Private Shared m_Log As log4net.ILog

        Shared Sub New()
            Log = log4net.LogManager.GetLogger(GetType(Logger))
        End Sub

        Public Shared Sub [Error](msg As Object)
            Log.[Error](msg)
        End Sub

        Public Shared Sub [Error](msg As Object, ex As Exception)
            Log.[Error](msg, ex)
        End Sub

        Public Shared Sub [Error](ex As Exception)
            Dim user_id = HttpContext.Current.Session("User_ID")
            Log.[Error](ex.Message, ex)

            Dim ToEmailID = "vaishali.pareek027@gmail.com"
            Dim CCEmailID = "vaishali.pareek027@gmail.com"
            Dim Subject = "this is test"
            Dim Body = "Hi this is test"

            Dim smtp_server As New SmtpClient
            Dim e_mail As New MailMessage
            smtp_server.UseDefaultCredentials = Convert.ToBoolean(GetParamsValue("pUseDefaultCredentials"))
            smtp_server.Port = GetParamsValue("pPortNumber")
            'smtp_server.Host = "smtpgw.pg.com"
            'e_mail.From = New MailAddress("msoforecast.im@pg.com")
            smtp_server.Host = GetParamsValue("pHostName")
            e_mail.From = New MailAddress(GetParamsValue("pFromEmail"))


            If CCEmailID <> "" Then
                For Each a In CCEmailID.Split(New Char() {";"})
                    e_mail.CC.Add(a)
                Next
            End If

            For Each a In ToEmailID.Split(New Char() {";"})
                e_mail.To.Add(a)
            Next

            e_mail.Subject = Subject
            e_mail.IsBodyHtml = Convert.ToBoolean(GetParamsValue("pIsHTMLBody"))
            e_mail.Body = Body
            smtp_server.Send(e_mail)
        End Sub

        Public Shared Sub Info(msg As Object)
            Log.Info(msg)
        End Sub

        Public Sub SendEmail(ToEmailID As String, CCEmailID As String, Subject As String, Body As String)

            Dim smtp_server As New SmtpClient
            Dim e_mail As New MailMessage
            smtp_server.UseDefaultCredentials = Convert.ToBoolean(GetParamsValue("pUseDefaultCredentials"))
            smtp_server.Port = GetParamsValue("pPortNumber")
            'smtp_server.Host = "smtpgw.pg.com"
            'e_mail.From = New MailAddress("msoforecast.im@pg.com")
            smtp_server.Host = GetParamsValue("pHostName")
            e_mail.From = New MailAddress(GetParamsValue("pFromEmail"))

            If CCEmailID <> "" Then
                For Each a In CCEmailID.Split(New Char() {";"})
                    e_mail.CC.Add(a)
                Next
            End If

            For Each a In ToEmailID.Split(New Char() {";"})
                e_mail.To.Add(a)
            Next

            e_mail.Subject = Subject
            e_mail.IsBodyHtml = Convert.ToBoolean(GetParamsValue("pIsHTMLBody"))
            e_mail.Body = Body
            smtp_server.Send(e_mail)
        End Sub
    End Class
End Module

