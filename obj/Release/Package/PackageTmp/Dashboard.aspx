<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="Dashboard.aspx.vb" Inherits="CMPDSB_DEVIN.Dashboard" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 148px;
        }

        .auto-style2 {
            width: 146px;
        }

        .auto-style3 {
            width: 144px;
        }

        .auto-style4 {
            width: 57px;
        }

        .auto-style5 {
            width: 162px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>

                <table class="auto-style1" border="0">
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td colspan="3">
                            <h2>GSUM+ Change Master Plan and Dash Board</h2>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td colspan="3">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td colspan="3">
                            <h3>Practitioners</h3>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style2">
                            <asp:Button CssClass="btn" ID="BtnEditMeasures" OnClick="BtnEditMeasures_Click" runat="server" Text="Edit Measures" />
                        </td>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td colspan="3">
                            <h3>Launch Leaders or GSUM+ SWPO</h3>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style2">
                            <asp:Button CssClass="btn" ID="BtnCreatePorject" runat="server" OnClick="BtnCreatePorject_Click" Text="Create Project" />
                        </td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Button CssClass="btn" ID="BtnEditPractitioners" OnClick="BtnEditPractitioners_Click" runat="server" Text="Edit Practitioners" />
                            &nbsp;</td>
                        <td></td>
                        <td></td>
                        <td class="auto-style4">
                            <asp:Button CssClass="btn" ID="BtnAdminforPlant" runat="server" Text="Admin for Plant" />
                        </td>
                    </tr>
                    <tr id="trGlobal_Admin" runat="server">
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td colspan="3">
                            <h3>Global/Regional SWPO</h3>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trBtnGlobal_Admin" runat="server">
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style2">
                            <asp:Button CssClass="btn" ID="BtnAdmin" runat="server" Text="Admin" />
                        </td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td colspan="3">
                            <h3>Assign your Default Location</h3>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style2">
                            <asp:Button CssClass="btn" ID="BtnSet" runat="server" Text="Set" />
                        </td>
                        <td></td>
                        <td colspan="2">
                            <asp:DropDownList AppendDataBoundItems="true" ID="DDlplants" Width="450px" runat="server">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td class="auto-style4"></td>
                        <td>
                            <asp:Button CssClass="btn" ID="BtnReports" runat="server" OnClick="BtnReports_Click" Text="Reports" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <%--<asp:FileUpload ID="fuFile" runat="server" />--%>
        <asp:Button ID="btnID" Visible="false" runat="server" Text="Genrate Scripts" OnClick="btnID_Click1" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
