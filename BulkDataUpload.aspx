<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="BulkDataUpload.aspx.vb" Inherits="CMPDSB_DEVIN.BulkDataUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            width: 90%;
            margin: 0 5%;
            margin-bottom: 50px;
        }

        input[type=date]::-webkit-inner-spin-button {
            -webkit-appearance: none;
            display: none;
        }

        .ContainerOne {
            width: 100%;
            overflow: hidden;
        }

        .col1 {
            width: 15%;
            float: left;
            margin-bottom: 20px;
        }

        .col2 {
            width: 27%;
            float: left;
            margin-bottom: 20px;
        }
        a:link{
            color:#666666;
            text-decoration:none;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="ContainerOne">
            <div class="col1" style="width: 670px;">
                <h2> Bulk Data Upload </h2>
            </div>
        </div>
         <div class="ContainerOne">
            <div class="col2">
                <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Choose Master Table:"></asp:Label>
                <asp:DropDownList ID="ddlTemplateType" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ControlToValidate="ddlTemplateType" InitialValue="" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <br />
                <br />
                <asp:Label Font-Bold="true" ID="lblReportDesc" runat="server" Text=""></asp:Label>

            </div>

        </div>
        <div class="ContainerOne">
            <div class="col1">
                <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Choose Template :"></asp:Label>
            </div>
            <div class="col1">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div class="col1">
                <asp:Button runat="server" OnClick="btnLoadTables_Click" ID="btnLoadTables" Text="Load Tables" />
            </div>
        </div>

        <%--<div class="ContainerOne">
            <div class="col1">
                Search BLOBID
            </div>
            <div class="col2">
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"></asp:Button>
            </div>
        </div>--%>
        <div class="ContainerOne">
            <div class="col1">
                <asp:Button runat="server"  Text="Upload Bulk Data" ID="btnUploadTemplate" OnClick="btnUploadTemplate_Click" />

                <asp:Button runat="server" Text="Reset" ID="btnReset" />

                <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div class="ContainerOne">
            <asp:GridView AutoGenerateSelectButton="true" CssClass="table" OnSelectedIndexChanged="gvGrid_SelectedIndexChanged"  AutoGenerateColumns="false" OnRowCommand="gvGrid_RowCommand" ID="gvGrid" runat="server" EmptyDataText="No records found" AllowSorting="true" ShowHeader="true">
                <Columns>
                    <asp:BoundField DataField="BLOBFile_ID" HeaderText="ID" />
                    <asp:TemplateField HeaderText="Download Here">
                        <ItemTemplate>
                            <asp:LinkButton ForeColor="Blue" ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("BLOBFile_ID") %>'
                                CommandName="Download" Text='<%# Eval("FileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:BoundField DataField="DateUploaded" HeaderText="Upload Date" />
                           <%-- <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" />
                            <asp:BoundField DataField="CreateDate" HeaderText="Create Date" />--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
