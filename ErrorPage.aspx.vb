Public Class ErrorPage
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub btnback_Click(sender As Object, e As EventArgs)
		Response.Redirect("Dashboard.aspx")

	End Sub
End Class