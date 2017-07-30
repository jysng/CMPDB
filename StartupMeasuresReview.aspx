<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StartupMeasuresReview.aspx.vb" MasterPageFile="~/layout.Master" Inherits="CMPDSB_DEVIN.StartupMeasuresReview" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
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
        }

        .col1new {
            width: 20%;
        }

        .col1 span, select {
            /*display: block;*/
            margin-bottom: 10px;
        }

        .col1 span {
            padding-top: 5px;
            /*margin-bottom: 15px;*/
        }

        .col2 {
            width: 20%;
            float: left;
        }

            .col2 span {
                margin-bottom: 5px;
                display: block;
            }

        .col3 {
            width: 20%;
            float: left;
        }

        .col4 {
            width: 20%;
            float: left;
        }

        .img {
            float: left;
            margin-top: 120px;
            margin-left: 5%;
            margin-right: 10px;
        }

        .img2 {
            float: left;
            margin-top: 120px;
            margin-left: 5%;
            margin-right: 10px;
        }

        .col3 span {
            margin-bottom: 5px;
            display: block;
        }

        .container h2 {
            margin: 10px 0 20px 0;
        }

        .midbox {
            width: 100%;
            float: left;
            margin-top: 7px;
            margin-bottom: 5px;
        }

            .midbox input {
                float: left;
                margin-right: 5px;
            }

        .SearchBox {
            border-color: #cfdbe6;
            border-style: ridge;
            border-width: 1px;
            padding: 14px;
            margin-bottom: 10px;
        }

        .col51 {
            width: 25%;
            float: left;
        }

        .wfix {
            width: 90%;
            float: left;
            margin-top: 15px;
        }

        .miles {
            width: 22%;
            float: left;
            margin-top: 15px;
            margin-right: 20px;
        }

        .disblock{
            display:block;
        }
        .common{
           display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upnl">
        <ContentTemplate>

            <div class="container">
                <div class="ContainerOne">

                    <div class="col1" style="width: 670px;">
                        <h2>CMPDB:Reporting</h2>
                    </div>

                    <div class="col2">
                        <asp:CheckBox Visible="false" ID="chkAdvancedMode" OnCheckedChanged="chkAdvancedMode_CheckedChanged" AutoPostBack="true" runat="server" Text="Advanced Mode" />
                    </div>

                    <div class="ContainerOne TopHead">
                        <div class="col1">
                        </div>
                    </div>

                    <br />
                    <br />
                    <div id="divSrchExist" class="ContainerOne SearchBox" runat="server">
                        <div class="col1">

                            <div id="dvReportType" >
                            <span class="lbl">Select Report Type</span>
                            <asp:DropDownList ID="ddlReportType" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="250px" Style="display: block" Height="25px">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="CPS Week By Week" Selected="True" Value="CPS_WBW"></asp:ListItem>
                                <asp:ListItem Text="StartUps Underqualified" Value="STP_UNQ"></asp:ListItem>
                                <asp:ListItem Text="Unreported Output Measures" Value="UNR_OPM"></asp:ListItem>
                                <asp:ListItem Text="StartUps Not  Closed Out" Value="STP_NCO"></asp:ListItem>
                                <asp:ListItem Text="PCA of Critical Targets or Dates" Value="PCA_CTD"></asp:ListItem>
                            </asp:DropDownList>
                                </div>
                            <div runat="server" id="dvPlant" class="common">
                                <span class="lbl">Plant</span>
                                <asp:DropDownList ID="ddlSearchPlants" OnSelectedIndexChanged="ddlSearchPlants_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" runat="server" Width="250px" Height="25px" Style="display: block">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div runat="server" id="dvDepartment" class="common">
                                <span class="lbl">Select Impacted Department</span>
                                <asp:DropDownList ID="ddlSearchImpDept" AppendDataBoundItems="true" runat="server" Width="250px" Style="display: block" Height="25px">

                                    <asp:ListItem Text="ALL" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Making" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Converting" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Packing" Value="4"></asp:ListItem>


                                </asp:DropDownList>
                            </div>
                            <div runat="server" id="dvSUL" class="common">
                            <span class="lbl">Select SUL</span>
                            <asp:DropDownList ID="ddlSearchSUL" AppendDataBoundItems="true" runat="server" Height="25px" Style="display: block" Width="250px">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                                </div>
                            <div runat="server" id="dvProjectManager" class="common">
                            <span class="lbl">Select Project Manager</span>
                            <asp:DropDownList ID="ddlSearchProManager" AppendDataBoundItems="true" AutoPostBack="true" Width="250px" Style="display: block" Height="25px" runat="server">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Jeff Colton" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Simon" Value="2"></asp:ListItem>

                            </asp:DropDownList>
                            </div>
                            <div runat="server" id="dvProjectStatus" class="common">
                                <span class="lbl">Select Project Status</span>
                            <asp:DropDownList ID="ddlSearchProStatus" AppendDataBoundItems="true" runat="server" Width="250px" Style="display: block" Height="25px">
                                <asp:ListItem Text="ALL" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Cancelled" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                                </div>
                            
                            <span class="lbl">Last Update > X days</span>
                            <asp:TextBox ID="txtLastUpdate" runat="server" Width="50px" Height="18px">
                            </asp:TextBox>
                        </div>
                        <div class="col51">
                           
                        </div>
                        <div class="midfix3" id="divhide" runat="server" style="display: none;">
                            <div class="merge">
                                <div class="miles">
                                    Select Milestone
                                <fieldset class="wfix">
                                    <asp:CheckBoxList ID="lstMilestone" runat="server">
                                        <asp:ListItem>conceptual Perliminary/Planning</asp:ListItem>
                                        <asp:ListItem>VAT</asp:ListItem>
                                        <asp:ListItem>EO</asp:ListItem>
                                        <asp:ListItem>Construction</asp:ListItem>
                                        <asp:ListItem>Startup work process</asp:ListItem>
                                        <asp:ListItem>SOP</asp:ListItem>
                                    </asp:CheckBoxList>
                                </fieldset>

                                </div>
                                <div class="miles">
                                    Display these columns in GRID
                                <fieldset class="wfix">
                                    <asp:CheckBoxList ID="lstcolgrid" Width="300px" runat="server">
                                        <asp:ListItem>SUL not qualified for Project</asp:ListItem>
                                        <asp:ListItem>Days till SOP</asp:ListItem>
                                        <asp:ListItem>Days till PR measuring Window Starts</asp:ListItem>
                                        <asp:ListItem>Days till PR measuring Window Ends</asp:ListItem>
                                        <asp:ListItem>Days Since Last Update</asp:ListItem>
                                    </asp:CheckBoxList>
                                </fieldset>
                                </div>

                            </div>
                             
                        </div>


                        <div class="col1new">
                             <br />
                            <br />
                            <asp:Button ID="btnSerach"  style="margin-top: 10px;" OnClick="btnSerach_Click" runat="server" Text="Create Grid" />
                             <br />
                            <br />
                            <h2 style="width:222%"><asp:Label Style="font-size:17px;" runat="server" ID="lblReportName"></asp:Label> Report </h2>
                             <br />
                            <br />
                           
                        </div>
                        <asp:GridView ID="gridStartupMeasuresReview" Width="100%" AutoGenerateColumns="true" runat="server" >
                          <%--  <Columns>



                                <asp:TemplateField HeaderText="Business Unit StartUp">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DepartMent"></asp:TemplateField>

                                <asp:TemplateField HeaderText="SUL"></asp:TemplateField>
                                <asp:TemplateField HeaderText="StartUp Name">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Re Implementaion Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGridEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EWP">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days Since Last Update">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GSUM+Small Project Readiness"></asp:TemplateField>



                            </Columns>--%>
                        </asp:GridView>
                        <br />
                        <br />
                        <asp:Button runat="server" OnClick="btnExportToExcel_Click" ID="btnExportToExcel" Text="Export To Excel" />
                        <asp:Button runat="server" OnClick="btnAddReportTemplate_Click" ID="btnAddReportTemplate" Text="Add Report Template" />

                    </div>
                        </div>

                <br />
                <br />
                <br />

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
