<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="Report.aspx.vb" Inherits="CMPDSB_DEVIN.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="content/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="content/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .row{
            margin-top:20px;
            margin-right:5px;
            margin-left:5px;
        }

        .rowGrid{
            margin-top:50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <div class="container-fluid ">
                <div class="jumbotron">
                    <h3>Create Project Report 
                    </h3>
                </div>
                <div class="gutter"></div>
                <div class="row top-buffer">
                    <div class="col-md-2">
                       <strong> Plant </strong>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList Width="200px" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPlantList_SelectedIndexChanged" runat="server" ID="ddlPlantList">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2 ">
                       <strong> Project </strong>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList AppendDataBoundItems="true" Width="200px" CssClass="form-control" runat="server" ID="ddlProject">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                       <strong> Department </strong>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList AppendDataBoundItems="true" Width="200px" CssClass="form-control" runat="server" ID="ddlDepartment">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row top-buffer">
                    <div class="col-md-2">
                       <strong> SUL </strong>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList AppendDataBoundItems="true" Width="200px" CssClass="form-control" runat="server" ID="ddlSUL">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                       <strong>Project Status </strong> 

                    </div>
                    <div class="col-md-2">
                         <asp:DropDownList ID="ddlStartUpStatus" CssClass="form-control" AppendDataBoundItems="true" Width="200px" runat="server">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="2">Hold/Pending</asp:ListItem>
                                <asp:ListItem Value="3">Complete</asp:ListItem>
                                <asp:ListItem Value="4">Cancelled</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <asp:Button CssClass="btn btn-info" OnClick="btnGenrateReport_Click" Text="Generate" runat="server" ID="btnGenrateReport"></asp:Button>
                    </div>
                </div>
                
                <div class=" row">
                    <h3>Grid Results</h3>
                    <br />
                    <br />
                    <div class="col">
                        <asp:GridView  Width="100%" ID="grdReport" runat="server" AutoGenerateColumns="false" >

                            <Columns>
                                <asp:BoundField DataField="Date" HeaderText="Last Update" />
                                <%--<asp:BoundField DataField="In_All_Change" HeaderText="In All Change" />--%>
                                <asp:BoundField DataField="Startup_Name" HeaderText="Startup Name" />
                                <asp:BoundField DataField="Production_Line" HeaderText="Production Line" />
                                <%--<asp:BoundField DataField="Business_Unit" HeaderText="Business Unit" />--%>
                                <asp:BoundField DataField="Email" HeaderText="Practioner" />
                                <asp:BoundField DataField="Qualification_Level" HeaderText="Qualification Level" />
                                <asp:BoundField DataField="Project_Status" HeaderText="Status" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div  class="well">No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                        <asp:Button CssClass="btn btn-info" OnClick="btnDownload_Click" Text="Download" runat="server" ID="btnDownload"></asp:Button>
                </div>
            </div>

            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
        <Triggers >
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
