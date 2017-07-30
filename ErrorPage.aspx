<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ErrorPage.aspx.vb" Inherits="CMPDSB_DEVIN.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; padding:50px; font-size:36px; font-family:Arial;">
			Oops!! Something went wrong.
			<asp:Button runat="server" OnClick ="btnback_Click" ID="btnback" text="Back"/>
        </div>
    </form>
</body>
</html>
