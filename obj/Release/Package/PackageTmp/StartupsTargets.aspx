<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StartupsTargets.aspx.vb" MasterPageFile="~/layout.Master" Inherits="CMPDSB_DEVIN.Startupstargets" %>


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

        .col1button {
            float: left;
            margin-right: 30px;
        }

        .col1 {
            width: 15%;
            float: left;
        }

        .col1, button {
            width: 130px;
            margin-right: 30px;
        }

        .col1new {
            width: 20%;
        }

        .col2small {
            width: 90%;
            float: left;
            line-height: 21px;
        }

        .col1 span, select {
            display: block;
            margin-bottom: 10px;
        }

        .col1 span {
            padding-top: 5px;
            margin-bottom: 15px;
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

        .midfix {
            width: 60%;
            float: left;
        }

        .midfix2 {
            width: 10%;
            float: left;
        }

        .midfix3 {
            width: 40%;
            float: left;
        }


        .add {
            width: 100%;
            float: left;
            margin-bottom: 10px;
        }


        .add1 {
            width: 100%;
            float: left;
            margin-bottom: 10px;
        }

        .add2 {
            width: 100%;
            line-height: 17px;
            float: left;
            margin-bottom: 10px;
        }

        .sapretone {
            width: 10%;
            float: left;
        }

        .sapretwo {
            width: 30%;
            float: left;
        }

        .saprethree {
            width: 50%;
            float: left;
        }


        .sapretoneList {
            width: 30%;
            float: left;
        }

        .sapretwoList {
            width: 30%;
            float: left;
        }

        .saprethreeList {
            width: 30%;
            float: left;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upnl">
        <ContentTemplate>
            <div class="container">
                <div class="ContainerOne">
                    <div class="col1" style="width: 670px;">
                        <h2>CMPDB: Startup Targets(Launch Leaders enters Required and SUL Edits)</h2>
                    </div>

                    <div class="col2" style="margin-top: 10px;">
                        <asp:CheckBox ID="chkAdvancedMode" OnCheckedChanged="chkAdvancedMode_CheckedChanged" AutoPostBack="true" Checked="true" runat="server" Text="Advanced Mode" />
                    </div>
					<div class="ContainerOne">
                        <div class="col1">
                            <asp:Label ID="lbltxtProject" CssClass="lbl" runat="server" Text="Project Name"></asp:Label>
                        </div>
                        <div class="col2">
                            <asp:TextBox ID="txtProject" style="height:25px; width: 311px;" runat="server">                      
                            </asp:TextBox>
                        </div>
						<div class="col3">
							<asp:Button ID="btnSearch" OnClick ="btnearchProj_Click" style="width: 70px;margin-left: 190px; height: 30px;" runat="server" Text="Search" /> &nbsp;&nbsp;
						</div>
                    </div>

                    <div class="ContainerOne">
                        <div class="col1">
                            <asp:Label ID="lblStartupName" CssClass="lbl" runat="server" Text="StartUp Name"></asp:Label>
                        </div>
                        <div class="col2">
							 <asp:DropDownList ID="ddlStartUpName" AppendDataBoundItems="true" AutoPostBack="true" Height="25px" Width="315px" runat="server">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>
                          <%--  <asp:TextBox ID="txtStartupName" Height="25px" Width="500px" runat="server">
                      
                            </asp:TextBox>--%>
                        </div>
                    </div>

                    <div class="ContainerOne" id="divStartupStatus" runat="server">
                        <div class="col1">
                            <asp:Label ID="lblStartUpStatus" CssClass="lbl" runat="server" Text="*StartUp Status"></asp:Label>
                        </div>
                        <div class="col2">
                            <asp:DropDownList ID="ddlStartUpStatus" AppendDataBoundItems="true" Height="25px" Width="200px" runat="server">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="2">Hold/Pending</asp:ListItem>
                                <asp:ListItem Value="3">Complete</asp:ListItem>
                                <asp:ListItem Value="4">Cancelled</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="ContainerOne" id="divCharacterApproved" runat="server">
                        <div class="col1">
                            <asp:Label ID="lblCharacter" CssClass="lbl" runat="server" Text="*Charter Approved"></asp:Label>
                        </div>
                        <div class="col2">

                            <asp:CheckBox ID="chkCharacter" runat="server" />

                          <%--  <asp:DropDownList ID="ddlCharacter" AppendDataBoundItems="true"  Height="25px" Width="200px" runat="server">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                    </div>
                    <hr />
                    <div class="ContainerOne">
                        <div class="col1">
                            <asp:Label ID="lblPlantAE" CssClass="lbl" runat="server" Text="Plant AE"></asp:Label>
                        </div>
                        <div class="col2">
                             <asp:CheckBox ID="chkPlantAE" runat="server" />
                         <%--   <asp:DropDownList ID="ddlPlantAE" AppendDataBoundItems="true" Height="25px" Width="200px" runat="server">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                    </div>

                    <div class="ContainerOne">
                        <div class="col1">
                            <asp:Label ID="lblSAPProject" CssClass="lbl" runat="server" Text="SAP Project #"></asp:Label>
                        </div>
                        <div class="col2">
                            <asp:TextBox ID="txtSAPProject" Height="25px" Width="200px" runat="server"></asp:TextBox>


                        </div>
                    </div>

                    <div class="ContainerOne" id="divEtcTargetStatus" runat="server">
                        <div class="col1">
                            <asp:Label ID="lblETCTarger" CssClass="lbl" runat="server" Text="*ETC Target($M)"></asp:Label>
                        </div>
                        <div class="col2">
                            <asp:TextBox ID="txtETCTarget" Height="25px" Width="200px" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <hr />
                    <div class="ContainerOne" id="diPrTargetStatus"  runat="server">
                        <div class="col1">
                            <asp:Label ID="lblPRTarget" CssClass="lbl" runat="server" Text="*PR Target %"></asp:Label>
                        </div>
                        <div class="col2">
                            <asp:TextBox ID="txtPRTarget" Height="25px" Width="200px" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <hr id="hr1" runat="server" />

                    <div class="ContainerOne">
                        <div class="col1" style="width: 100%;">
                            <asp:Label ID="lblMilestoneTarget" CssClass="lbl" runat="server" Text="Schedule Milestone Targets for Each Phase(Date is the beginning of this phase)"></asp:Label>
                        </div>

                    </div>

                    <div class="ContainerOne">
                        <div class="midfix" style="width: 100%">

                            <div class="sapretone">


                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnFeasibility" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>



                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnConceptual" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>


                                </div>
                                 <div class="add1" runat="server" id="divtxtMlstnPCStrategy" >
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnPCStrategy" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>



                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnProjectFSF" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>



                                </div>
                                <div class="add1" runat="server" id="divtxtMlstnStartupWorkProcess" >
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnStartupWorkProcess" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="add1">
                                    <div class="col1new" runat="server" id="divtxtMlstnStartupProduction" >
                                        <asp:TextBox ID="txtMlstnStartupProduction" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>



                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnSustain" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnPSU" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnG2TDay1" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnG2TConstructionCompletion" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnG2TEndofPQPhase" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="add1">
                                    <div class="col1new">
                                        <asp:TextBox ID="txtMlstnG2T_End_of_Ininital_Verification" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="90px" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                              

                            </div>


                            <div class="sapretwo">
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnFeasibility" Text="Feasibility/Early Manufacturing Involvement" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnConceptual" Text="Conceptual/Perliminary Planning" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2" runat="server" id="divMlstnPCStrategy" >
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnPCStrategy" Text="*PC(Defination)/ Strategy and Commitement" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnProjectFSF" Text="Project FSF(Design Started)/Planning and Execution" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2" runat="server" id="divMlstnStartupWorkProcess" >
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnStartupWorkProcess" Text="*Start Up Work Process" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2" runat="server" id="divlblMlstnStartupProduction" >
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnStartupProduction" Text="*Start of Production" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnSustain" Text="Sustain Mfg/Verification" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnPSU" Text="PSU:Followups" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnG2TDay1" Text="Optional:G2T Day1" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnG2TConstructionCompletion" Text="Optional:G2T Construction Completion" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnG2TEndofPQPhase" Text="Optional:G2T End of PQ Phase" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="add2">
                                    <div class="col2small">
                                        <asp:Label ID="lblMlstnG2T_End_of_Ininital_Verification" Text="Optional:G2T End of Initial Verification Completion Phase" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ContainerOne">
                    <div class="midfix" style="width: 100%">
                        <div class="sapretoneList">
                            <div class="add1">
                                EO Date
                            </div>
                            <div class="add1">
                                <div>
                                    <asp:TextBox ID="txtEODate" Width="94px" CssClass="datepicker" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    <asp:TextBox ID="txtDays" runat="server" Width="75px" placeholder="Days"></asp:TextBox>
                                   
                                </div>
                            </div>
                            <div class="add1">
                                <asp:ListBox AutoPostBack="true" OnSelectedIndexChanged="lstboxEODate_SelectedIndexChanged" ID="lstboxEODate" Width="300px" runat="server"></asp:ListBox>
                            </div>
                            <div class="add1">
                                <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshEODate_Click" runat="server" ID="btnRefreshEODate" />
                                <asp:ImageButton CssClass="btngLOBAL" ID="btnAddEODate" OnClick="btnAddEODate_Click" runat="Server" ImageUrl="~/images/Add.png" />
                                <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteEODate_Click" ID="btnDeleteEODate" />

                                <%--   <div class="col1button">
                                    </div>
                                        <div class="col2">
                                    </div>
                                        <div class="col3">
                                    </div>--%>
                            </div>
                        </div>
                        <div class="sapretwoList">
                            <div class="add1">
                                Construction Date
                               
                            </div>
                              <div class="add1">
                                <div>
                                    <asp:TextBox ID="txtConstructionDate" CssClass="datepicker" Width="94px" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    <asp:TextBox ID="txtConstructionDays" runat="server" Width="75px" placeholder="Days"></asp:TextBox>
                                   
                                </div>
                            </div>
                            <div class="add1">

                                <asp:ListBox  AutoPostBack="true" OnSelectedIndexChanged="lstboxConstructionDate_SelectedIndexChanged" ID="lstboxConstructionDate" Width="300px" runat="server"></asp:ListBox>

                            </div>
                             <div class="add1">
                                <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshConstructionDate_Click" runat="server" ID="btnRefreshConstructionDate" />
                                <asp:ImageButton CssClass="btngLOBAL" ID="btnAddConstructionDate" OnClick="btnAddConstructionDate_Click" runat="Server" ImageUrl="~/images/Add.png" />
                                <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteConstructionDate_Click" ID="btnDeleteConstructionDate" />

                                <%--   <div class="col1button">
                                    </div>
                                        <div class="col2">
                                    </div>
                                        <div class="col3">
                                    </div>--%>
                            </div>
                          
                        </div>
                        <div class="saprethreeList">
                            <div class="add1">
                                Vat Date
                               
                            </div>
                            <div class="add1">
                                <div>
                                    <asp:TextBox ID="txtVatDate" CssClass="datepicker" Width="94px" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    <asp:TextBox ID="txtVatDays" runat="server" Width="75px" placeholder="Days"></asp:TextBox>
                                   
                                </div>
                            </div>
                            <div class="add1">

                                <asp:ListBox AutoPostBack="true" OnSelectedIndexChanged="lstboxVATDate_SelectedIndexChanged" ID="lstboxVATDate" Width="300px" runat="server"></asp:ListBox>

                            </div>
                            <div class="add1">
                                <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshVatDate_Click" runat="server" ID="btnRefreshVatDate" />
                                <asp:ImageButton CssClass="btngLOBAL" ID="btnAddVatDate" OnClick="btnAddVatDate_Click"  runat="Server" ImageUrl="~/images/Add.png" />
                                <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteVatDate_Click"  ID="btnDeleteVatDate" />

                                <%--   <div class="col1button">
                                    </div>
                                        <div class="col2">
                                    </div>
                                        <div class="col3">
                                    </div>--%>
                            </div>
                        </div>
                    </div>

                </div>
                    <hr />
                    <div class="ContainerOne">
                        <div class="midfix" style="width: 100%">
                            Enter Comments if Critical Targets(red Color)have changed.Launch Leader Will Review
                        </div>
                        <br />
                        <br />
                        <div class="midfix" style="width: 100%">
                            <div class="add1">
                                <div class="col1" style="width: 80%;">
                                    <asp:TextBox ID="txtcomments" TextMode="MultiLine" Height="100px" Width="480px" runat="server"></asp:TextBox>&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                     <asp:Button ID="btnback" Width="70px" OnClick="btnback_Click" runat="server" Text="Back" /> &nbsp;&nbsp;
                                     <asp:Button ID="btnCancel" Width="70px" OnClick="btnCancel_Click" runat="server" Text="Cancel" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnSubmit" Width="60px" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                                </div>

                            </div>

                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="ContainerOne">

                        <div class="saprethree" style="margin-left: 525px;">
                            <div class="add1">
                                <asp:Button ID="btnApprovedPCA" Width="229px" OnClick="btnApprovedPCA_Click" runat="server" Text="Launch Leader Approve PCA" />
                            </div>

                        </div>
                    </div>
                    <br />
                    <div class="ContainerOne">
                        <div class="saprethree" style="margin-left: 525px;">
                            <div class="add1">
                                <div class="col1" style="margin-right: 0px; width: 40px;">
                                    <asp:Button OnClick="btnPrev_Click" ID="btnPrev" runat="server" Text="<" />
                                </div>
                                <div class="col2" style="width: 20%;">
                                    <asp:TextBox ID="txtPCAApprovedDate" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="150px" runat="server"></asp:TextBox>
                                </div>
                                <div class="col3" style="margin-left: 47px; width: 40px;">
                                    <asp:Button OnClick="btnNext_Click"  ID="btnNext" runat="server" Text=">" />
                                </div>

                            </div>

                        </div>

                    </div>
                    
                </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
