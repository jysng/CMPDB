<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Practitioner.aspx.vb" MasterPageFile="~/layout.Master" Inherits="CMPDSB_DEVIN.Practicioners" %>
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
            display: block;
            margin-bottom: 10px;
        }

        .col1 span {
            padding-top: 5px;
            margin-bottom: 15px;
        }

        .col2 {
            width: 25%;
            float: left;
        }

            .col2 span {
                margin-bottom: 5px;
                display: block;
            }

        .col3 {
            width: 15%;
            float: left;
            line-height: 30px;
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
            margin-left: 23px;
            /* margin-left: 1092px; */
            position: absolute;
            right: 30px;
            text-align: center;
            color: black;
        }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/gridviewScroll.min.js"></script>
    <%--<script src="Scripts/jquery.keynavigation.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
            //keynav();
        });
        function gridviewScroll() {
            $('#<%=gridPractitioner.ClientID%>').gridviewScroll({
                width: '99%',
                height: 300,
                startVertical: $("#<%=hfGridView1SV.ClientID%>").val(),
                startHorizontal: $("#<%=hfGridView1SH.ClientID%>").val(),
                onScrollVertical: function (delta) {
                    $("#<%=hfGridView1SV.ClientID%>").val(delta);
                },
                onScrollHorizontal: function (delta) {
                    $("#<%=hfGridView1SH.ClientID%>").val(delta);
                }
            });
        }
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(gridviewScroll);
            //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(keynav);

        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   
        <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upnl">
            <ContentTemplate>
                <asp:HiddenField runat="server" id ="hfGridView1SV"/>
                <asp:HiddenField runat="server" id ="hfGridView1SH"/>
                 <div class="container">
                <div class="ContainerOne">

                    <h2>CMPDB: Edit Practitioner</h2>
                </div>


                <div class="ContainerOne TopHead">
                    <div class="col1">
                        <asp:Button ID="btnSerach" OnClick="btnSerach_Click" runat="server" Text="Search Exisiting Practitioner" />
                    </div>
                </div>
                <br />
                <br />


                <div id="divSrchExist" class="ContainerOne SearchBox" runat="server" style="display: none;">
                    <div class="col1">

                        <span class="lbl">Plant</span>
                        <span class="lbl">Business Unit</span>
                       
                        <span class="lbl">Department</span>
                        <span class="lbl">SWP</span>


                    </div>
                    <div class="col3">
                        <asp:DropDownList ID="ddlSearchPlants" OnSelectedIndexChanged="ddlSearchPlants_SelectedIndexChanged" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" runat="server" Width="250px" Height="25px" Style="display: block">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSearchBU" AppendDataBoundItems="true" runat="server" Width="250px" Style="display: block" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    
                        <asp:DropDownList ID="ddlSearchDept" AppendDataBoundItems="true" AutoPostBack="true" Width="250px" Style="display: block" Height="25px" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSearchSWP" AppendDataBoundItems="true" runat="server" Width="250px" Style="display: block" Height="25px">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                      
                    </div>
                    <div class="col3" style="margin-left: 75px">
                        <asp:Button ID="BtnSrchExistingStartup" OnClick="BtnSrchExistingPractitioner_Click" runat="server" Text="Search" Width="120px" Height="25px" />
                    </div>
                    <div class="col1new">
                        <h2>Search Results</h2>
                    </div>
                    <asp:GridView ID="gridPractitioner" Width="95%" DataKeyNames="Practitioner_ID" AllowSorting="true" OnSorting="gridPractitioner_Sorting" OnRowCommand="gridPractitioner_RowCommand" AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true">
                        <Columns>

                            <asp:TemplateField HeaderText="Check">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkb1" runat="server" Text="Check All" OnCheckedChanged="sellectAll"
                                        AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelMail" AutoPostBack="true" OnCheckedChanged="sellectOne" runat="server" />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" SortExpression="Email" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text='<%#Eval("Email") %>' CommandName="EditDetails"
                                        CommandArgument='<%# Eval("Practitioner_ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plant" SortExpression="Plant" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BU" SortExpression="Business_Unit" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("Business_Unit") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region" SortExpression="Region" Visible="false" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("Region") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department" SortExpression="Site_Department" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("Site_Department") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Added" SortExpression="DateAdded" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("DateAdded", "{0:MM-dd-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SWP" SortExpression="swp" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("swp") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SWP Role" SortExpression="Practitioner_Role" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("Practitioner_Role") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qualification Level" SortExpression="Qualification_Level" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%# Eval("Qualification_Level")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qualification Date" SortExpression="QualificationDate" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("QualificationDate", "{0:MM-dd-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qualifier" SortExpression="Qualifier" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("Qualifier") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Date" SortExpression="TargetedDate" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("TargetedDate", "{0:MM-dd-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class Completed Date" SortExpression="ClassCompletedDate" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("ClassCompletedDate", "{0:MM-dd-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tech Coach Email" SortExpression="TechCoachEmail" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("TechCoachEmail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments" SortExpression="Comments" HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                    <%#Eval("Comments") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete"  HeaderStyle-ForeColor="Gray">
                                <ItemTemplate>
                                     <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteDetails"
                                        CommandArgument='<%# Eval("Practitioner_ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>

                </div>

                <br />
                <br />
                <div>
                    <h2>
                        <asp:Label ID="UserNameLabel" CssClass="lbl" runat="server" Text="Add Practitioner" /></h2>
                </div>
                <br />
                <br />

                <div class="ContainerOne">
                    <div class="col1">

                        <asp:Label ID="lblPlant" CssClass="lbl" runat="server" Text="*Plant"></asp:Label>

                    </div>
                    <div class="col2">

                        <asp:DropDownList ID="ddlPlant" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" Height="25px" Width="200px" runat="server">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>

                    </div>


                </div>
                <br />



                <div class="ContainerOne">

                    <div class="col1">

                        <asp:Label ID="lbllBu" CssClass="lbl" runat="server" Text="Business Unit"></asp:Label>

                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="ddlBu" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBu_SelectedIndexChanged" Width="200px" Height="25px" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="ContainerOne">

                    <div class="col1">

                        <asp:Label ID="lblDepartment" CssClass="lbl" runat="server" Text="Department"></asp:Label>

                    </div>
                    <div class="col2">

                        <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Width="200px" Height="25px" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col3">
                        <asp:Label ID="Label4" CssClass="lbl" runat="server" Text="First Name"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtFirstName" class="txtgLOBAL" Width="200px" Height="25px" CssClass="txtgLOBAL" runat="server">							   	
                        </asp:TextBox>
                    </div>
                </div>

                <br />
                <div class="ContainerOne">

                    <div class="col1">

                        <asp:Label ID="lblEmail" CssClass="lbl" runat="server" Text="*Email"></asp:Label>

                    </div>
                    <div class="col2">

                        <asp:TextBox ID="txtInsertEmail" placeholder="don't add @pg.com" class="txtgLOBAL" Width="200px" Height="25px" CssClass="txtgLOBAL" runat="server">
                            
                        </asp:TextBox>
                    </div>
                    <div class="col3">
                        <asp:Label ID="Label5" CssClass="lbl" runat="server" Text="Last Name"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtLastName" class="txtgLOBAL" Width="200px" Height="25px" CssClass="txtgLOBAL" runat="server">							   	
                        </asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="ContainerOne">

                    <div class="col1">
                        <asp:Label ID="LabelSWP" CssClass="lbl" runat="server" Text="*SWP"></asp:Label>


                    </div>

                    <div class="col2">
                        <asp:DropDownList ID="ddlSWp" Width="200px" Height="25px" AppendDataBoundItems="true" AutoPostBack="true" class="txtgLOBAL" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>



                    </div>
                    <div class="col3">

                        <asp:Label ID="lblSWPRole" CssClass="lbl" runat="server" Text="*SWP Role"></asp:Label>

                    </div>
                    <div class="col4">

                        <asp:DropDownList ID="ddlSWPRole" Height="25px" Width="200px" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSWPRole_SelectedIndexChanged" class="txtgLOBAL" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                        <%--  <asp:TextBox ID="txtSWPRole"  Height="25px" Width="200px"  class="txtgLOBAL" runat="server">
                            
                        </asp:TextBox>--%>
                    </div>
                </div>
                <br />

                <br />
                <div class="ContainerOne">
                    <div class="col1">
                        <asp:Label ID="lblQualificationLevel" CssClass="lbl" runat="server" Text="Qualification Level"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="ddlQualificationLevel" AppendDataBoundItems="true" Height="25px" Width="200px" runat="server" class="txtgLOBAL">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <%--<asp:ListItem Text="PS Basic" Value ="1"></asp:ListItem>
                                <asp:ListItem Text="PS Intermediate" Value="2"></asp:ListItem>
                                <asp:ListItem Text="PS Advanced" Value ="3"></asp:ListItem>
                                <asp:ListItem Text=" PS Expert" Value ="4"></asp:ListItem>--%>
                        </asp:DropDownList>

                         
                    </div>
                    <%--  <cc:CalendarExtender ID="CalendarExtender2" Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtQualificationDate" runat="server"></cc:CalendarExtender>--%>
                </div>

                <br />

                <div class="ContainerOne">
                    <div class="col1">
                        <asp:Label ID="lblDateAdded" CssClass="lbl" runat="server" Text="Date Added"></asp:Label>


                    </div>
                    <div class="col2">

                        <asp:TextBox ID="txtDateAdded" Enabled="false" Width="200px" placeholder="MM/DD/YYYY" Height="25px" CssClass="txtgLOBAL datepicker" runat="server">
                           
                        </asp:TextBox>
                        <%--<cc:CalendarExtender ID="CalendarExtender1" Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtDateAdded" runat="server"></cc:CalendarExtender>--%>
                    </div>

                    <div class="col3">

                        <asp:Label ID="lblClassCompletedDates" CssClass="lbl" runat="server" Text="Class Complete Date"></asp:Label>

                    </div>
                    <div class="col4">

                        <asp:TextBox ID="txtClassCompletedDate" placeholder="MM/DD/YYYY" Width="200px" Height="25px" class="txtgLOBAL datepicker" runat="server">
                        </asp:TextBox>
                        <%--<cc:CalendarExtender ID="CalendarExtender4" Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtClassCompletedDate" runat="server"></cc:CalendarExtender>--%>
                    </div>
                </div>
                <br />

                <div class="ContainerOne">


                    <div class="col1">

                        <asp:Label ID="lblTargetDate" CssClass="lbl" runat="server" Text="Target Date"></asp:Label>

                    </div>
                    <div class="col2">

                        <asp:TextBox ID="txtTargetDate" placeholder="MM/DD/YYYY" Width="200px" Height="25px" class="txtgLOBAL datepicker" runat="server">
                           
                        </asp:TextBox>
                        <%--  <cc:CalendarExtender ID="CalendarExtender3" Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtTargetDate" runat="server"></cc:CalendarExtender>--%>
                    </div>
                    <div class="col3">

                        <asp:Label ID="lblQualificationDate" CssClass="lbl" runat="server" Text="Qualification Date"></asp:Label>

                    </div>
                    <div class="col4">

                        <asp:TextBox ID="txtQualificationDate" Width="200px" placeholder="MM/DD/YYYY" Height="25px" class="txtgLOBAL datepicker" runat="server">
                           
                        </asp:TextBox>
                        <%--  <cc:CalendarExtender ID="CalendarExtender2" Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtQualificationDate" runat="server"></cc:CalendarExtender>--%>
                    </div>
                </div>
                <br />
                <div class="ContainerOne">

                    <div class="col1">

                        <asp:Label ID="lblTechCoachEmail" CssClass="lbl" runat="server" Text="Tech Coach Email"></asp:Label>

                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="ddlTechCoachEmail" AppendDataBoundItems="true" Width="200px" Height="25px" class="txtgLOBAL" runat="server">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <input id="txtClassCompletedDate" type="date" Width="200px"   Height="25px"  class="txtgLOBAL" runat="server" />--%>
                    </div>
                    <div class="col3">

                        <asp:Label ID="lblQualifier" CssClass="lbl" runat="server" Text="Qualifier Email"></asp:Label>

                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlQualifier" Width="200px" AppendDataBoundItems="true" Height="25px" class="txtgLOBAL" runat="server">
                             <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>
                <br />
                <div class="ContainerOne">

                    <div class="col1">
                        <asp:Label ID="lblcomments" CssClass="lbl" runat="server" Text="Comments"></asp:Label>


                    </div>
                    <div class="col2">

                        <asp:TextBox ID="txtComments" Width="685px" Height="25px" class="txtgLOBAL" runat="server">
                            
                        </asp:TextBox>

                    </div>

                </div>
                <br />
                <br />
                <div class="col4" style="text-align: right; margin-left: 635px;">

                    <asp:Button ID="btnSaveRecords"  OnClick="btnSaveRecords_Click" runat="server" Text="Save" />

                </div>

                <br />
                <br />
                <br />


                <div class="ContainerOne">

                    <div class="col1" style="width: 80px;">
                        <asp:Label ID="Label1" CssClass="lbl" runat="server" Text="To"></asp:Label>

                    </div>

                    <div class="col4" style="text-align: right;">
                        <asp:TextBox ID="txtEmailTo" Width="790px" Enabled="false" Height="30px" runat="server"></asp:TextBox>


                    </div>
                </div>
                <br />
                <div class="ContainerOne">
                    <div class="col1" style="width: 80px;">
                        <asp:Label ID="Label2" CssClass="lbl" runat="server" Text="Subject"></asp:Label>

                    </div>

                    <div class="col1" style="text-align: right;">
                        <asp:TextBox ID="txtEmailSubject" Width="790px" Height="30px" runat="server"></asp:TextBox>


                    </div>
                </div>
                <br />
                <div class="ContainerOne">

                    <div class="col1" style="width: 80px;">
                        <asp:Label ID="Label3" CssClass="lbl" runat="server" Text="Body"></asp:Label>
                    </div>

                    <div class="col4" style="text-align: right;">


                        <asp:TextBox ID="txtEmailBody" TextMode="MultiLine" Width="790px" Height="200px" runat="server"></asp:TextBox>



                    </div>
                </div>
                <br />
                <br />

                <div class="ContainerOne">

                    <div class="col1" style="width: 80px;">
                    </div>

                    <div class="col4" style="text-align: right; margin-left: 635px;">


                        <asp:Button ID="btnSendEmail" OnClick="btnSendEmail_Click" Height="35px" runat="server" Text="Send Email To Selected" />
                    </div>
                      </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    <script>

        function checkCookie() {
            var user = getCookie("CFlag");
            if (user != "") {
                window.close();
            } else {
                //user = prompt("Please enter your name:", "");
                //if (user != "" && user != null) {
                //    setCookie("username", user, 365);
                //}
            }
        }

        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }
    </script>
  
</asp:Content>
