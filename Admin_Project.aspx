<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="Admin_Project.aspx.vb" Inherits="CMPDSB_DEVIN.Admin_Project" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
           .hideGridColumn
            {
                display:none;
            }
        .container {
            width: 90%;
            margin: 0 5%;
            margin-bottom: 50px;
        }

        .TopHead {
            margin-bottom: 14px;
        }

        .ContainerOne {
            width: 100%;
            overflow: hidden;
        }

        .ContainerBottom {
            width: 100%;
            overflow: hidden;
            margin-bottom: 5px;
            margin-top: 5px;
        }
        .colGrid {
               width: 80%;
                float: left;
        
        }
         .col1new {
            width: 20%;
         
        }
        .col1 {
            width: 20%;
            float: left;
        }

            .col1 span, select {
                display: block;
                margin-bottom: 10px;
            }

            .col1 span {
                padding-top: 5px;
                margin-bottom: 15px;
            }

        .col2 input {
            margin-bottom: 10px;
        }
        /*.col1 select{
             display:block;
             margin-bottom:10px;
         }*/

        .col2 {
            width: 40%;
            float: left;
        }

            .col2 span, select {
                display: block;
                margin-bottom: 10px;
            }

        .col3 {
            width: 20%;
            float: left;
            margin-left: 65px;
        }

        .coldd1 {
            width: 30%;
            float: left;
        }

        .coldd2 {
            width: 15%;
            float: left;
        }

        .coldd3 {
            width: 15%;
            float: left;
        }

        .container h2 {
            margin: 10px 0 20px 0;
        }


        .midbox {
            width: 100%;
            float: left;
            margin-top: 7px;
            margin-bottom: 10px;
        }

            .midbox input {
                float: left;
                margin-right: 10px;
            }

        .col11 {
            width: 15%;
            float: left;
        }

            .col11 span {
                margin-bottom: 5px;
                display: block;
            }

        .col12 {
            width: 20%;
            float: left;
        }

            .col12 input {
                margin-bottom: 5px;
            }

            .col12 span {
                margin-bottom: 21px;
                display: block;
            }

        .col13 {
            width: 5%;
            float: left;
        }

            .col13 span {
                margin-bottom: 5px;
                display: block;
            }

        .col14 {
            width: 5%;
            float: left;
        }

            .col14 span {
                margin-bottom: 11px;
                display: block;
            }

        .col15 {
            width: 4%;
            float: left;
            margin-bottom: 5px;
        }

            .col15 span {
                margin-bottom: 11px;
                display: block;
            }

        .col16 {
            width: 15%;
            float: left;
        }

            .col16 span {
                margin-bottom: 5px;
                display: block;
            }

        .col13 input {
            margin-bottom: 15px;
        }

        .col14 input {
            margin-bottom: 15px;
        }

        .col13 span {
            margin-bottom: 11px;
        }

        .col15 span {
            margin-bottom: 11px;
        }

        .col16 input {
            margin-bottom: 5px;
            width: 50px;
            display: block;
        }

        .extra_bottom_margin {
            margin-bottom: 20px;
        }

        .coldd1 ul {
            margin: 0;
            padding: 0;
        }

            .coldd1 ul li {
                list-style: none;
                padding: 3px 0;
                margin: 0;
            }

        #rbg1 {
            margin: 5px 0;
        }

        /*select { margin:10px 0;}
          input { margin:10px 2px;}*/

        .SearchBox {
            border-color: #cfdbe6;
            border-style: ridge;
            border-width: 1px;
            padding: 14px;
            margin-bottom: 10px;
        }
        /* The Modal (background) */
.modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
   
}

/* Modal Content */
.modal-content {
  background-color: #8AE0F2;
    margin: auto;
    padding: 20px;
    border: 1px solid #dff0d8;
    width: 12%;
    margin-top: 71px;
    margin-left :23px;
    /* margin-left: 1092px; */
    position: absolute;
    right: 30px;
    text-align:center;
    color:black;
 
}

.btnRefresh{
    /*margin-left: -106px;*/
    width: 25px;
    margin-bottom: 7px;
}

    </style>
      <%--<script src="jquery.modal.js" type="text/javascript" charset="utf-8"></script>--%>
     
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upnl">
        <ContentTemplate>
            <div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content">
    <%--<span class="close">&times;</span>--%>
     
     <%-- <%=hdn_message.ClientID  %>--%>
      <%-- <p>Some text in the Modal..</p>--%>
  </div>

</div>
            <div class="container">
                <div class="ContainerOne">
                    <div class="col1">
                        &nbsp;
                    </div>
                    <div class="col2">
                        &nbsp;
                    </div>
                 
                </div>
                    <div class="ContainerOne">
                        <div class="col1">
                            <h2>Admin Project </h2>     
                        </div>
                        <div class="col2" style="margin-top:10px;">
                               <asp:CheckBox ID="chkAdvancedMode" OnCheckedChanged="chkAdvancedMode_CheckedChanged" AutoPostBack="true" checked="true" runat="server" Text="Advanced Mode" />
                        </div>
                        </div>

                <div class="ContainerOne TopHead">
                    <div class="col1">
                        <asp:Button ID="btnSerach" OnClick="btnSerach_Click" runat="server" Text="Search Exisiting Startup" />

                        <asp:CheckBox ID="chkSrch" Visible="false" runat="server" AutoPostBack="true" Text="Search Exisiting Startup" />
                    </div>
                </div>
                <div id="divSrchExist" class="ContainerOne SearchBox" runat="server" style="display:none;">
                    <div class="col1">

                        <span class="lbl">*Plant</span>
                        <span class="lbl">*Project Type</span>
                        <span class="lbl">*Change Type</span> 
                        <span class="lbl">*Business Unit</span>
                        <span class="lbl">*Production Line</span>
                       

                    </div>
                    <div class="col3">
                        <asp:DropDownList ID="DdlPlants" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DdlPlants_SelectedIndexChanged" runat="server" Width="250px" Height="25px" Style="display: block">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSrchProjectType" AppendDataBoundItems="true" runat="server" Width="250px" Style="display: block" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSrchChangeType" AppendDataBoundItems="true" runat="server" Height="25px" Style="display: block" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSrchBusinessUnit" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSrchBusinessUnit_SelectedIndexChanged" AutoPostBack="true" Width="250px" Style="display: block" Height="25px" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSrchProductionLine" AppendDataBoundItems="true"  runat="server" Width="250px" Style="display: block" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                          <br /><br />
                    </div>
                    <div class="col3">
                        <asp:Button ID="BtnSrchExistingStartup" OnClick="BtnSrchExistingStartup_Click" runat="server" Text="Search" Width="120px" Height="25px" />
                    </div>
                    <div class="col1new">
                            <h2>Search Results</h2>
                     </div>
                      <asp:GridView ID="gdvSrch" Width="100%" AutoGenerateColumns="False" AllowSorting="true" OnSorting="gdvSrch_Sorting" OnRowCommand="gdvSrch_RowCommand" runat="server" ShowHeaderWhenEmpty="true">
                            <Columns>



                                <asp:TemplateField HeaderText="Project" SortExpression="project_name" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text='<%#Eval("project_name") %>' CommandName="EditDetails" CommandArgument='<%# Eval("Startup_ID") %>'
                                          ></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant" SortExpression="plant" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("plant") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Startup Name" SortExpression="Startup_Name" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Startup_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Project Name" SortExpression="project_name" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Project_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Type" SortExpression="Project_Type" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Project_Type") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project %" SortExpression="Project_Target" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Project_Target") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Status" SortExpression="Project_Status" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Project_Status") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Change Type" SortExpression="Change_Type" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Change_Type") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Unit" SortExpression="Business_Unit" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Business_Unit") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUL" SortExpression="email" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("email") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Production Line" SortExpression="Production_Line" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Production_Line") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Startup Id" SortExpression="Startup_ID" HeaderStyle-ForeColor="Gray">
                                    <ItemTemplate>
                                        <%#Eval("Startup_ID") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                     </div>

              <br /><br />
                <div class="ContainerOne">
                    <div class="col1" style="width:100%;">
                      <%--<asp:GridView ID="gdvSrch" ShowHeaderWhenEmpty="true" EmptyDataText="No Records found " AutoGenerateColumns="False" Width="100%" AutoGenerateSelectButton="true" OnSelectedIndexChanging="gdvSrch_SelectedIndexChanging" runat="server">
                            <Columns>
                                
                                <asp:BoundField DataField="Plant" HeaderText="Plant"></asp:BoundField>
                                  <asp:BoundField DataField="Project_Type" HeaderText="Project Type"></asp:BoundField>
                                <asp:BoundField  DataField="Change_Type" HeaderText="Change Type"></asp:BoundField>
                                 <asp:BoundField  DataField="Business_Unit" HeaderText="Business Unit"></asp:BoundField>
                                <asp:BoundField DataField="Production_Line" HeaderText="Production Line"></asp:BoundField>

                                 <asp:BoundField  DataField="Plant_ID" HeaderText="Plant"></asp:BoundField>
                                <asp:BoundField  DataField="Project_Type_ID" HeaderText="Project_Type"></asp:BoundField>
                                <asp:BoundField  DataField="Change_Type_ID" HeaderText="Change_Type"></asp:BoundField>
                                <asp:BoundField  DataField="B
                          usiness_Unit_ID" HeaderText="Business_Unit_ID"></asp:BoundField>
                                <asp:BoundField  DataField="Production_Line_ID" HeaderText="Production_Line"></asp:BoundField>
                                 <asp:BoundField  DataField="Startup_ID" HeaderText="Startup_ID"  ></asp:BoundField>

                            </Columns>
                        </asp:GridView>--%>

                        


                </div>
                </div>
               <br /><br /><br />
                <div class="ContainerOne">
                    <div class="col1">
                        <span class="lbl">*Plant</span>
                        <span class="lbl">*Startup Name</span>
                        <span class="lbl">Project Name</span>

                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="DdlPlantsInsert" OnSelectedIndexChanged="DdlPlantsInsert_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" runat="server" Width="250px" Height="25px" Style="display: block">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtstartupName" runat="server" Width="516px" Height="25px"></asp:TextBox>
                        <asp:TextBox ID="txtProjectName" runat="server" Width="516px" Height="25px"></asp:TextBox>
                    </div>
                    <div class="col3">
                    </div>

                </div>
                <h2>Step 1: SWP Tools for Startup</h2>
                <div class="ContainerOne">
                    <div class="col1">
                        <span class="lbl">*In "All Change"</span>
                        <span class="lbl">*Complexity of StartUp</span>
                        <span class="lbl">*GSUM+ CBA's</span>
                    </div>
                    <div class="col2">
                        <div id="rbg1">
                            <asp:RadioButton ID="rbYes" AutoPostBack="true" Checked="true" GroupName="rbInallChange" runat="server" Text="Yes" />
                            <asp:RadioButton ID="rbNo" AutoPostBack="true" GroupName="rbInallChange" runat="server" Text="No" />
                        </div>
                        <%-- <asp:DropDownList ID="DdlInallChanges" runat="server" Width="250px" Height="25px">
                </asp:DropDownList>--%>
                        <asp:DropDownList ID="DdlCOmplexityofStartup" AppendDataBoundItems="true" runat="server" Height="25px" Width="515px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:DropDownList ID="DdlGsumCBas" runat="server" Height="25px" Width="515px">
                </asp:DropDownList>--%>
                        <div id="rbg1">
                            <asp:RadioButton ViewStateMode="Enabled" ID="rbCustom" AutoPostBack="true" GroupName="rbGSUMCBA" OnCheckedChanged="rb_CheckedChanged" runat="server" Text="Complex" Checked="true" />
                            <asp:RadioButton ID="rbSmall" AutoPostBack="true" GroupName="rbGSUMCBA" OnCheckedChanged="rb_CheckedChanged" runat="server" Text="Small" />
                            <asp:RadioButton ID="rbLarge" AutoPostBack="true" GroupName="rbGSUMCBA" OnCheckedChanged="rb_CheckedChanged" runat="server" Text="Medium/Large" />
                        </div>
                    </div>
                    <div class="col3">
                    </div>
                </div>
                <div class="ContainerOne">


                    <div class="col1">&nbsp</div>
                    <div class="coldd1">
                        <%-- <ul>
             <li>  <asp:CheckBox ID="chkIAP" runat="server" Checked="true"  /></li>   
             <li>  <asp:CheckBox ID="chkGsumSmall" runat="server"  GroupName="chkgsumEWPRE" Checked="true" /></li> 
             <li>  <asp:CheckBox ID="chkGsumMaterial" runat="server" Checked="true"  /></li> 
             <li>  <asp:CheckBox ID="chkGsumPRRiskAssessment" runat="server"  Checked="true" /> </li> 
             <li>  <asp:CheckBox ID="chkGSUMmfgOperations" runat="server" Checked="true"  /> </li> 
             <li>  <asp:CheckBox ID="chkMfgReadinessDeliverables" runat="server" Checked="true" /> </li> 
             <li>  <asp:CheckBox ID="ChkTest" runat="server" Checked="true" /> </li>    
             </ul>--%>
                        <asp:CheckBoxList ID="cblGSUM" runat="server" DataTextField="language"
                            DataValueField="language"  RepeatLayout="UnOrderedList" Width="432px">
                        </asp:CheckBoxList>

                    </div>
                    <div class="coldd2">
                        <%--<asp:CheckBox ID="chkEWPTracker" runat="server" GroupName="chkgsumEWPRE" Checked="true" />--%>
                        <asp:CheckBoxList ID="cblEWP" runat="server" DataTextField="language"
                            DataValueField="language" AutoPostBack="True" RepeatLayout="OrderedList" Width="432px">
                            
                        </asp:CheckBoxList>

                    </div>
                    <div class="coldd3">
                        <%--<asp:CheckBox  ID="chkREImplementationPlan" runat="server" Text="RE Implementation Plan"  GroupName="chkgsumEWPRE" Checked="true"/>--%>

                        <asp:CheckBoxList ID="cblREImplementationPlan" runat="server" DataTextField="language"
                            DataValueField="language" AutoPostBack="True" RepeatLayout="OrderedList" Width="432px">
                         
                        </asp:CheckBoxList>




                    </div>

                </div>
                <h2>Step 2: General Information</h2>
                <div class="ContainerOne">
                    <div class="col12">
                        <span class="lbl">*Project Type</span>
                        <span class="lbl">*Change Type</span>
                        <span class="lbl" id="spanltoCBN" runat="server">Link to CBN</span>
                        <span class="lbl" id="spanltoPriority" runat="server">Priority</span>
                    </div>
                    <div class="col13">
                        <asp:DropDownList ID="DdlProjectType" Style="display: block" AppendDataBoundItems="true" runat="server" Width="250px" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlChangeType" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlLinktoCBn" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlPriority" Style="display: block" AppendDataBoundItems="true" runat="server" Width="250px" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="High" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Low" Value="3"></asp:ListItem>
                        </asp:DropDownList>


                    </div>

                </div>
                <hr />
                <div class="ContainerOne">
                    <div class="col1">
                        <span class="lbl">*Business Unit</span>
                        <span class="lbl">*Production Line</span>
                        <span class="lbl" id="spanimpactDept" runat="server">Impact Dept.</span>
                        <span class="lbl" id="spanleadingDept" runat="server">Leading Dept.</span>

                    </div>
                    <div class="col2">

                        <asp:DropDownList ID="DdlBusinessUnit" Style="display: block" AppendDataBoundItems="true"  Width="250px" Height="25px" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlProductionLine" Style="display: block" AppendDataBoundItems="true" runat="server" Width="250px" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList ID="DdlImpactDept" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlLeadingDept" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col3">
                        <%-- <asp:Button ID="BtnFilterbyBU"  runat="server" Text="Filter by BU" Height="25px" />--%>
                       <%--  <asp:Button ID="btnMileStone" runat="server" Enabled="false" OnClick="btnMileStone_Click" Text="Request Target Or Milestone Change"></asp:Button>--%>
                    </div>
                </div>
                <hr />
                <div class="ContainerOne">
                    <div class="col1">
                        <span class="lbl">*SUL</span>
                        <span class="lbl" id="spansulCoach" runat="server">SUL Coach</span>
                        <span class="lbl" id="spansnSiel" runat="server">SN SIEL</span>
                        <span class="lbl" id="spanprjMgr" runat="server">Prj Mgr/Leader</span>
                    </div>
                    <div class="coldd1">
                        <asp:DropDownList ID="DdlSUL" ViewStateMode="Enabled" Style="display: block" AppendDataBoundItems="true" runat="server" Width="250px" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlSULCoach" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlSNSIEL" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlPrjMgr" Style="display: block" AppendDataBoundItems="true" runat="server" Height="25px" Width="250px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                  <div class="col13">
                        <%--<asp:Button ID="btnQSUL" Style="display: block" AppendDataBoundItems="true" runat="server" Text="Q" Width="30px" />
                        <asp:Button ID="BtnQSulCoach" Style="display: block" AppendDataBoundItems="true" runat="server" Text="Q" Width="30px" />
                        <asp:Button ID="BTnQSnSiel" Style="display: block" AppendDataBoundItems="true" runat="server" Text="Q" Width="30px" />
                        <asp:Button ID="BtnQPrjMgr" Style="display: block" AppendDataBoundItems="true" runat="server" Text="Q" Width="30px" />--%>
                    </div>
                    <div style="margin-left:-101px">
                       
                        <asp:ImageButton style="display:none;" CssClass="btnRefresh" OnClientClick="document.getElementById('btnRefresh').style.display='none';" OnClick="btnRefresh_Click" runat="server" ClientIDMode="Static" ID="btnRefresh" ImageUrl="~/images/Refresh.png" />
                         <a runat="server" onclick="setCookie('CFlag','1',.01,'btnRefresh');" href="Practitioner.aspx" target="_blank">
                        <img id="imgAdd" src="images/Add.png" class="btnRefresh" />
                        </a>
                        <br />
                        <div style="display:block;float:right;margin-right:551px">
                         <asp:ImageButton style="display:none;" CssClass="btnRefresh" OnClientClick="document.getElementById('btnCoachRefresh').style.display='none';" OnClick="btnCoachRefresh_Click" runat="server" ClientIDMode="Static" ID="btnCoachRefresh" ImageUrl="~/images/Refresh.png" />
                            </div>
                         <a runat="server" onclick="setCookie('CFlag','1',.01,'btnCoachRefresh');" href="Practitioner.aspx" target="_blank">
                        <img id="imgAddCoach" style="display:block" src="images/Add.png" class="btnRefresh" />
                        </a>

                        <div style="display:block;float:right;margin-right:551px">
                          <asp:ImageButton style="display:none;" CssClass="btnRefresh" OnClientClick="document.getElementById('btnSNSIELRefresh').style.display='none';" OnClick="btnSNSIELRefresh_Click" runat="server" ClientIDMode="Static" ID="btnSNSIELRefresh" ImageUrl="~/images/Refresh.png" />
                            </div>
                         <a runat="server" onclick="setCookie('CFlag','1',.01,'btnSNSIELRefresh');" href="Practitioner.aspx" target="_blank">
                        <img id="imgAddSNSIEL" style="display:block" src="images/Add.png" class="btnRefresh" />
                        </a>
                        <div style="display:block;float:right;margin-right:551px">
                          <asp:ImageButton style="display:none;" CssClass="btnRefresh" OnClientClick="document.getElementById('btnPrjMgrRefresh').style.display='none';" OnClick="btnPrjMgrRefresh_Click" runat="server" ClientIDMode="Static" ID="btnPrjMgrRefresh" ImageUrl="~/images/Refresh.png" />
                            </div>
                         <a runat="server" onclick="setCookie('CFlag','1',.01,'btnPrjMgrRefresh');" href="Practitioner.aspx" target="_blank">
                        <img id="imgAddPrjMgr" style="display:block" src="images/Add.png" class="btnRefresh" />
                        </a>


                        <%--<asp:Button ID="btnplusSUL"  OnClientClick="aspnetForm.target ='_blank';" OnClick="btnplusSUL_Click" Style="display: block" AppendDataBoundItems="true" runat="server" Text="+" Width="30px" UseSubmitBehavior="False" />--%>
                        
                      <%--  <asp:Button ID="BtnplusSulCoach" Style="display: block" AppendDataBoundItems="true" runat="server" Text="+" Width="30px" />
                        <asp:Button ID="BTnplusSnSiel" Style="display: block" AppendDataBoundItems="true" runat="server" Text="+" Width="30px" />
                        <asp:Button ID="BtnplusPrjMgr" Style="display: block" AppendDataBoundItems="true" runat="server" Text="+" Width="30px" />--%>
                    </div>
                    <div class="col3">
                        <asp:Button ID="BtnSetTargetsandMilestones" Style="display: none" runat="server" Text="Set Targets and Milestones" Width="180px" Height="25px" />
                    </div>
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="180px" Height="25px" />                     
                </div>
                <br />
                  <div style="text-align: right;">
                 
                      <asp:Button ID="btnMileStone" runat="server" Enabled="false" OnClick="btnMileStone_Click" Text="Request Target Or Milestone Change"></asp:Button>
                 
                      </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>


        function setCookie(cname, cvalue, exdays,ele) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
            document.getElementById(ele).style.display = 'inline';
        }


        function ShowAddPage() {
            document.cookie = "CFlag=1";

            var input = document.createElement("input");

            input.setAttribute("type", "hidden");

            input.setAttribute("name", "hdnFlag");

            input.setAttribute("value", "1");
            //append to form element that you want .
            document.getElementById("imgAdd").appendChild(input);
            document.getElementById('btnRefresh').style.display = 'inline';
        }
    </script>
</asp:Content>
