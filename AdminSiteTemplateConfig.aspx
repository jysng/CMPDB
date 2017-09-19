<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="AdminSiteTemplateConfig.aspx.vb" Inherits="CMPDSB_DEVIN.AdminSiteTemplateConfig" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .hide {
            display: none;
        }

        .container {
            width: 90%;
            margin: 0 5%;
            margin-bottom: 50px;
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

        .col1 {
            width: 30%;
            float: left;
        }

            .col1 span {
                margin-bottom: 5px;
                display: block;
            }

        .col2 {
            width: 30%;
            float: left;
            margin: 0 5%;
        }

            .col2 span {
                margin-bottom: 5px;
                display: block;
            }

        .col3 {
            width: 30%;
            float: left;
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
                margin-bottom: 11px;
                display: block;
            }

        .col13 {
            width: 15%;
            float: left;
        }

            .col13 span {
                margin-bottom: 5px;
                display: block;
            }

        .col14 {
            width: 15%;
            float: left;
        }

            .col14 span {
                margin-bottom: 5px;
                display: block;
            }

        .col15 {
            width: 4%;
            float: left;
            margin-bottom: 5px;
        }

            .col15 span {
                margin-bottom: 5px;
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

        .col14 input {
            margin-bottom: 5px;
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

        .ContainerBottom a {
            cursor: pointer;
            font-weight: bold;
        }

        .st1 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }

        .st2 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }


        .st3 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }


        .st4 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }

        .st5 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }


        .st6 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }


        .st7 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }


        .st8 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }

        .st9 {
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }


        .col17 {
            width: 8%;
            float: left;
            margin-bottom: 5px;
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
    <script type="text/javascript">
        window.onload = function () {
            var a = document.getElementById("st9");
            a.className = "hide";
        }

        function validateFileUpload(obj) {

            var v = document.getElementById("<%= fuGsumSmallWS.ClientID %>").value;
            var pos = v.lastIndexOf("\\") + 1;
            v = v.substr(pos, v.length - pos);
            showhideControls(v);

        }

        function showhideControls(v) {
            document.getElementById("<%= lbl_file_fuGSUMMTL.ClientID %>").innerText = v;
            document.getElementById("<%= fuGSUMMTL.ClientID %>").style.display = "none";
            document.getElementById('lnkfuGSUMMTL').style.display = "";

            document.getElementById("<%= lbl_file_fuGSUMPRWS.ClientID %>").innerText = v;
            document.getElementById("<%= fuGSUMPRWS.ClientID %>").style.display = "none";
            document.getElementById('lnkfuGSUMPRWS').style.display = "";

            document.getElementById("<%= lbl_file_fuGSUMMFGOps.ClientID %>").innerText = v;
            document.getElementById("<%= fuGSUMMFGOps.ClientID %>").style.display = "none";
            document.getElementById('lnkfuGSUMMFGOps').style.display = "";

            document.getElementById("<%= lbl_file_fuMFGDel.ClientID %>").innerText = v;
            document.getElementById("<%= fuMFGDel.ClientID %>").style.display = "none";
            document.getElementById('lnkfuMFGDel').style.display = "";

            document.getElementById("<%= lbl_file_fuEWPTracker.ClientID %>").innerText = v;
            document.getElementById("<%= fuEWPTracker.ClientID %>").style.display = "none";
            document.getElementById('lnkfuEWPTracker').style.display = "";

            //document.getElementById("<%= lbl_file_fuGSUM_IAP.ClientID %>").innerText = v;
            //document.getElementById("<%= fuGSUM_IAP.ClientID %>").style.display = "none";
            //document.getElementById('lnkfuGSUM_IAP').style.display = "";

           // document.getElementById("<%= lbl_file_fuRE_Implementation.ClientID %>").innerText = v;
           // document.getElementById("<%= fuRE_Implementation.ClientID %>").style.display = "none";
            // document.getElementById('lnkfuRE_Implementation').style.display = "";

            document.getElementById("<%= lbl_file_fuGSUM_PRRA.ClientID %>").innerText = v;
            document.getElementById("<%= fuGSUM_PRRA.ClientID %>").style.display = "none";
            document.getElementById('lnkfuGSUM_PRRA').style.display = "";


        }



        function showhideControlsFromDDL(v) {
            document.getElementById('lnkfuGSUMMTL').style.display = "block";
            document.getElementById('lnkfuGSUMMTL').style.color = "Red";
            alert(document.getElementById('lnkfuGSUMMTL').style.display);
            alert(document.getElementById('lnkfuGSUMMTL').onclick)
            document.getElementById('lnkfuGSUMPRWS').style.display = "";


            document.getElementById('lnkfuGSUMMFGOps').style.display = "";


            document.getElementById('lnkfuMFGDel').style.display = "";


            document.getElementById('lnkfuEWPTracker').style.display = "";

            //document.getElementById("<%= lbl_file_fuGSUM_IAP.ClientID %>").innerText = v;
            //document.getElementById("<%= fuGSUM_IAP.ClientID %>").style.display = "none";
            //document.getElementById('lnkfuGSUM_IAP').style.display = "";

           // document.getElementById("<%= lbl_file_fuRE_Implementation.ClientID %>").innerText = v;
           // document.getElementById("<%= fuRE_Implementation.ClientID %>").style.display = "none";
            // document.getElementById('lnkfuRE_Implementation').style.display = "";


            document.getElementById('lnkfuGSUM_PRRA').style.display = "";


        }

        function dispChooseFile(obj, browse, file, cancel) {

            document.getElementById(obj.id).style.display = "none";
            document.getElementById(browse.id).style.display = "";
            document.getElementById(file.id).style.display = "none";
            document.getElementById(cancel.id).style.display = "";
        }

        function assignFile(assignFile, browse, cancel, edit) {
            var v = document.getElementById("<%= fuGsumSmallWS.ClientID %>").value;
            var UploadFileName = document.getElementById(assignFile.id)
            var pos = v.lastIndexOf("\\") + 1;
            v = v.substr(pos, v.length - pos);
            //alert(v);
            if (v != "") {
                UploadFileName.innerText = v;
            }
            UploadFileName.style.display = "block";
            document.getElementById(browse.id).style.display = "none";
            document.getElementById(cancel.id).style.display = "none";
            document.getElementById(edit.id).style.display = "";

        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="upnl">
        <ContentTemplate>

            <div class="container">
                <div class="ContainerOne">

                    <h2>Admin for Plant </h2>

                    <div class="col1">
                        <asp:Label ID="lblPlants" CssClass="lbl" runat="server" Text="*Plants"></asp:Label>
                        <asp:DropDownList ID="DropDownListPlant" OnSelectedIndexChanged="DropDownListPlant_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" Width="343px" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <div style="display: none">
                            <asp:Label ID="lblBusinessUnit" CssClass="lbl" runat="server" Text="*Business Unit"></asp:Label>
                            <asp:DropDownList ID="DropDownListBusinessUnit" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListBusinessUnit_SelectedIndexChanged" AutoPostBack="true" Width="343px" runat="server">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="lblPlatform" CssClass="lbl" runat="server" Text="Platform">
                            </asp:Label>
                            <asp:DropDownList ID="DropDownListPlatform" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPlatform_SelectedIndexChanged" AppendDataBoundItems="true" Width="343px" runat="server">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="lblProductionType" CssClass="lbl" runat="server" Text="*Production Type"></asp:Label>
                            <asp:DropDownList ID="DropDownListProductionType" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProductionType_SelectedIndexChanged" AppendDataBoundItems="true" Width="343px" runat="server">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>


                            <div class="img">
                                <asp:Image ID="Image1" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
                            </div>
                        </div>
                    </div>
                    <div class="col2">
                    </div>
                    <div style="display: none" class="col3">
                        <div id="DivProductionLines" runat="server">
                            <asp:Label ID="lblProductionLineOrArea" CssClass="lbl" runat="server" Text="*Production Line^ or Area "></asp:Label>
                            <asp:ListBox ID="ListBoxProductionLine" CssClass="ddl ddlgLOBAL" runat="server" OnSelectedIndexChanged="ListBoxProductionLine_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            <div class="midbox">
                                <asp:TextBox MaxLength="50" ID="txtProductionLine" runat="server" CssClass="txtgLOBAL" />
                                <asp:ImageButton Text="Add" runat="server" ID="btnAddProductionLine" ImageUrl="~/images/Add.png" OnClick="btnAddProductionLine_Click" CssClass="btngLOBAL" />
                                <asp:ImageButton Text="Delete" runat="server" ID="btnDeleteProductionLine" OnClick="btnDeleteProductionLine_Click" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" />
                                <asp:ImageButton Text="Refresh" runat="server" ID="BtnRefreshProductionLine" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="BtnRefreshProductionLine_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div style="display: none" class="ContainerOne">
                    <div class="col1">
                        <div id="DivDepartments" runat="server">
                            <asp:Label ID="lbldepartment" CssClass="lbl" runat="server" Text="Departments"></asp:Label>
                            <asp:ListBox ID="ListBoxDepartments" CssClass="ddl ddlgLOBAL" OnSelectedIndexChanged="ListBoxDepartments_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:ListBox>
                            <div class="midbox">
                                <asp:TextBox MaxLength="50" ID="txtDepartments" runat="server" CssClass="txtgLOBAL" />
                                <asp:ImageButton Text="Add" CssClass="btngLOBAL" OnClick="ButtonAddDepartments_Click" ImageUrl="~/images/Add.png" runat="server" ID="ButtonAddDepartments" />
                                <asp:ImageButton Text="Delete" runat="server" OnClick="ButtonDeleteDepartments_Click" ID="ButtonDeleteDepartments" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" />
                                <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" OnClick="ButtonRefreshDepartments_Click" ImageUrl="~/images/Refresh.png" runat="server" ID="ButtonRefreshDepartments" />
                            </div>
                        </div>
                    </div>

                    <div class="col3">
                        <div class="midbox">
                        </div>
                    </div>
                </div>

                <div style="display: none" class="ContainerOne">
                    <div class="col1">
                        <div id="DivCBN" runat="server">
                            <asp:Label ID="lblCBN" CssClass="lbl" runat="server" Text="CBN"></asp:Label>
                            <asp:ListBox ID="ListBoxCNB" CssClass="ddl ddlgLOBAL" runat="server" OnSelectedIndexChanged="ListBoxCNB_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            <div class="midbox">
                                <asp:TextBox MaxLength="50" ID="txtCBN" CssClass="txtgLOBAL" runat="server" />
                                <asp:ImageButton Text="Add" runat="server" ID="ButtonAddCBN" ImageUrl="~/images/Add.png" OnClick="ButtonAddCBN_Click" CssClass="btngLOBAL" />
                                <asp:ImageButton Text="Delete" runat="server" ID="ButtonDeleteCBN" OnClick="ButtonDeleteCBN_Click" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" />
                                <asp:ImageButton Text="Refresh" runat="server" ID="ButtonRefreshCBN" CssClass="btngLOBAL" OnClick="ButtonRefreshCBN_Click" ImageUrl="~/images/Refresh.png" />
                            </div>
                        </div>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col3"></div>
                </div>
                <asp:CheckBox ID="CheckBoxEnableAuto" Visible="false" AutoPostBack="true" runat="server" Text="Enable Auto Create Folder Structure and Links for New Startups" />
                <br />
                <br />
                <div runat="server" id="divEnableDisable">
                    <div class="ContainerBottom" style="display: none">
                        <div class="col1">
                            <b>Directory (on TCC or Shared Drive, NOT LOCAL)</b>
                        </div>
                        <div class="col2">
                            <asp:TextBox MaxLength="50" ID="txtDirectory" Visible="false" runat="server" Width="653px" />
                        </div>
                        <div class="col3">
                        </div>
                    </div>
                    <div class="ContainerBottom extra_bottom_margin">
                        <div style="display:none;" class="col1">

                            <asp:RadioButton ID="RadioButtonUseCorporate" AutoPostBack="true" GroupName="FileSource" runat="server" Text="Use Corporate Standards" />
                            <br />
                            <br />
                            <asp:RadioButton Style="display: none" ID="RadioButtonSingleSource" OnCheckedChanged="RadioButtonSingleSource_CheckedChanged" AutoPostBack="true" Checked="true" GroupName="FileSource" runat="server" Text="Single Source File" />

                        </div>
                        <div class="col11">
                        </div>
                        <div class="col2">
                            <asp:TextBox MaxLength="50" Visible="false" ID="txtSingleSource" runat="server" Width="655px" />
                        </div>
                    </div>
                    <div class="ContainerBottom">
                        <div class="st1">
                            <div id="files" runat="server" class="col12">
                                <asp:FileUpload ClientIDMode="Static" ID="fuGsumSmallWS" runat="server" />
                            </div>
                            <div id="lbls" runat="server" class="col12">

                                <asp:LinkButton ClientIDMode="Static" ID="lblfuGsumSmallWS" OnClick="lblfuGsumSmallWS_Click" runat="server" Text=""></asp:LinkButton>
                            </div>


                            <div class="col17">
                                <a id="lnkfuGsumSmallWS" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuGsumSmallWS,lblfuGsumSmallWS,lnkfuGsumSmallWSCancel);">Edit</a>
                                <a id="lnkfuGsumSmallWSCancel" style="display: none" onclick="assignFile(lblfuGsumSmallWS,fuGsumSmallWS,lnkfuGsumSmallWSCancel,lnkfuGsumSmallWS)">Cancel</a>
                            </div>

                            <div class="col13">
                                <asp:Label ID="lblGsumSmallWS" runat="server" Text=" GSUM+ Small WS Name"></asp:Label>

                            </div>

                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtGsumSmallWS" runat="server" />

                            </div>

                            <div class="col15">
                                <asp:Label ID="lblCellGsumSmallWS" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellGsumSmallWS" runat="server" />
                            </div>
                        </div>


                        <div class="st2">
                            <div id="Div1" runat="server" class="col12">
                                <asp:FileUpload ID="fuGSUMMTL" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuGSUMMTL" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>
                            <div class="col17">
                                <a id="lnkfuGSUMMTL" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuGSUMMTL,lbl_file_fuGSUMMTL,lnkfuGSUMMTLCancel);">Edit</a>
                                <a id="lnkfuGSUMMTLCancel" style="display: none" onclick="assignFile(lbl_file_fuGSUMMTL,fuGSUMMTL,lnkfuGSUMMTLCancel,lnkfuGSUMMTL)">Cancel</a>
                            </div>

                            <div id="Div9" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuGSUMMTL" runat="server" Text="  GSUM+ Mtl WS Name"></asp:Label>

                            </div>

                            <div class="col13">

                                <asp:Label ID="lblGSUMMTL" runat="server" Text="  GSUM+ Mtl WS Name"></asp:Label>

                            </div>


                            <div class="col14">

                                <asp:TextBox MaxLength="50" ID="txtGSUMMTL" runat="server" />
                            </div>


                            <div class="col15">
                                <asp:Label ID="lblCellGSUMMTL" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellGSUMMTL" runat="server" />
                            </div>



                        </div>


                        <div class="st3">
                            <div id="Div2" runat="server" class="col12">
                                <asp:FileUpload ID="fuGSUMPRWS" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuGSUMPRWS" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>

                            <div class="col17">
                                <a id="lnkfuGSUMPRWS" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuGSUMPRWS,lbl_file_fuGSUMPRWS,lnkfuGSUMPRWSCancel);">Edit</a>
                                <a id="lnkfuGSUMPRWSCancel" style="display: none" onclick="assignFile(lbl_file_fuGSUMPRWS,fuGSUMPRWS,lnkfuGSUMPRWSCancel,lnkfuGSUMPRWS)">Cancel</a>

                            </div>

                            <div id="Div10" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuGSUMPRWS" runat="server" Text="  GSUM+ PR WS Name "></asp:Label>
                            </div>

                            <div class="col13">

                                <asp:Label ID="lblGSUMPRWS" runat="server" Text="  GSUM+ PR WS Name "></asp:Label>

                            </div>
                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtGSUMPRWS" runat="server" />

                            </div>

                            <div class="col15">

                                <asp:Label ID="lblCellGSUMPRWS" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellGSUMPRWS" runat="server" />

                            </div>

                        </div>

                        <div class="st4">
                            <div id="Div3" runat="server" class="col12">
                                <asp:FileUpload ID="fuGSUMMFGOps" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuGSUMMFGOps" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>


                            <div class="col17">
                                <a id="lnkfuGSUMMFGOps" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuGSUMMFGOps,lbl_file_fuGSUMMFGOps,lnkfuGSUMMFGOpsCancel);">Edit</a>
                                <a id="lnkfuGSUMMFGOpsCancel" style="display: none" onclick="assignFile(lbl_file_fuGSUMMFGOps,fuGSUMMFGOps,lnkfuGSUMMFGOpsCancel,lnkfuGSUMMFGOps)">Cancel</a>

                            </div>


                            <div id="Div11" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuGSUMMFGOps" runat="server" Text=" GSUM+ Mfg Ops WS Name "></asp:Label>
                            </div>

                            <div class="col13">

                                <asp:Label ID="lblGSUMMFGOps" runat="server" Text=" GSUM+ Mfg Ops WS Name "></asp:Label>

                            </div>

                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtGSUMMFGOps" runat="server" />

                            </div>

                            <div class="col15">

                                <asp:Label ID="lblCellGSUMMFGOps" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellGSUMMFGOps" runat="server" />

                            </div>



                        </div>

                        <div class="st5">
                            <div id="Div4" runat="server" class="col12">
                                <asp:FileUpload ID="fuMFGDel" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuMFGDel" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>


                            <div class="col17">
                                <a id="lnkfuMFGDel" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuMFGDel,lbl_file_fuMFGDel,lnkfuMFGDelCancel);">Edit</a>
                                <a id="lnkfuMFGDelCancel" style="display: none" onclick="assignFile(lbl_file_fuMFGDel,fuMFGDel,lnkfuMFGDelCancel,lnkfuMFGDel)">Cancel</a>

                            </div>



                            <div id="Div12" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuMFGDel" runat="server" Text="GSUM+ Mfg Del WS Name	"></asp:Label>
                            </div>


                            <div class="col13">

                                <asp:Label ID="lblMFGDel" runat="server" Text="GSUM+ Mfg Del WS Name	"></asp:Label>

                            </div>
                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtMFGDel" runat="server" />

                            </div>
                            <div class="col15">
                                <asp:Label ID="lblCellMFGDel" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellMFGDel" runat="server" />

                            </div>

                        </div>

                        <div class="st6">
                            <div id="Div5" runat="server" class="col12">
                                <asp:FileUpload ID="fuEWPTracker" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuEWPTracker" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>
                            <div class="col17">
                                <a id="lnkfuEWPTracker" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuEWPTracker,lbl_file_fuEWPTracker,lnkfuEWPTrackerCancel);">Edit</a>
                                <a id="lnkfuEWPTrackerCancel" style="display: none" onclick="assignFile(lbl_file_fuEWPTracker,fuEWPTracker,lnkfuEWPTrackerCancel,lnkfuEWPTracker)">Cancel</a>
                            </div>
                            <div id="Div13" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuEWPTracker" runat="server" Text="EWP Tracker"></asp:Label>
                            </div>

                            <div class="col13">

                                <asp:Label ID="lblEWPTracker" runat="server" Text="EWP Tracker"></asp:Label>

                            </div>

                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtEWPTracker" runat="server" />

                            </div>
                            <div class="col15">
                                <asp:Label ID="lblCellEWPTracker" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellEWPTracker" runat="server" />

                            </div>

                        </div>

                        <div class="st7">
                            <div id="Div6" runat="server" class="col12">
                                <asp:FileUpload ID="fuGSUM_IAP" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuGSUM_IAP" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>

                            <div class="col17">
                                <a id="lnkfuGSUM_IAP" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuGSUM_IAP,lbl_file_fuGSUM_IAP,lnkfuGSUM_IAPCancel);">Edit</a>
                                <a id="lnkfuGSUM_IAPCancel" style="display: none" onclick="assignFile(lbl_file_fuGSUM_IAP,fuGSUM_IAP,lnkfuGSUM_IAPCancel,lnkfuGSUM_IAP)">Cancel</a>

                            </div>

                            <div id="Div14" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuGSUM_IAP" runat="server" Text="GSUM IAP"></asp:Label>
                            </div>

                            <div class="col13">

                                <asp:Label ID="lblGSUM_IAP" runat="server" Text="GSUM IAP"></asp:Label>

                            </div>

                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtGSUM_IAP" runat="server" />

                            </div>
                            <div class="col15">
                                <asp:Label ID="lblCellGSUM_IAP" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellGSUM_IAP" runat="server" />

                            </div>


                        </div>


                        <div class="st8">
                            <div id="Div7" runat="server" class="col12">
                                <asp:FileUpload ID="fuRE_Implementation" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuRE_Implementation" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>

                            <div class="col17">
                                <a id="lnkfuRE_Implementation" style="display: <%= XaVisibility%>" onclick="dispChooseFile(this,fuRE_Implementation,lbl_file_fuRE_Implementation,lnkfuRE_ImplementationCancel);">Edit</a>
                                <a id="lnkfuRE_ImplementationCancel" style="display: none" onclick="assignFile(lbl_file_fuRE_Implementation,fuRE_Implementation,lnkfuRE_ImplementationCancel,lnkfuRE_Implementation)">Cancel</a>

                            </div>



                            <div id="Div15" style="display: none;" runat="server" class="col12">
                                <asp:Label ID="lblfuRE_Implementation" runat="server" Text="RE Implementation"></asp:Label>
                            </div>
                            <div class="col13">

                                <asp:Label ID="lblRE_Implementation" runat="server" Text="RE Implementation"></asp:Label>

                            </div>

                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtRE_Implementation" runat="server" />

                            </div>
                            <div class="col15">
                                <asp:Label ID="lblCellRE_Implementation" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellRE_Implementation" runat="server" />

                            </div>


                        </div>



                        <div class="st9" id="st9" style="display: none;">
                            <div id="Div8" runat="server" class="col12">
                                <asp:FileUpload ID="fuGSUM_PRRA" runat="server" ClientIDMode="Static" />
                                <asp:LinkButton ID="lbl_file_fuGSUM_PRRA" runat="server" ClientIDMode="Static"></asp:LinkButton>
                            </div>

                            <div class="col17">
                                <a id="lnkfuGSUM_PRRA" style="display: none" onclick="dispChooseFile(this,fuGSUM_PRRA,lbl_file_fuGSUM_PRRA,lnkfuGSUM_PRRACancel);">Edit</a>
                                <a id="lnkfuGSUM_PRRACancel" style="display: none" onclick="assignFile(lbl_file_fuGSUM_PRRA,fuGSUM_PRRA,lnkfuGSUM_PRRACancel,lnkfuGSUM_PRRA)">Cancel</a>

                            </div>



                            <div id="Div16" runat="server" class="col12">
                                <asp:Label ID="lblfuGSUM_PRRA" runat="server" Text="GSUM PRRA"></asp:Label>
                            </div>

                            <div class="col13">

                                <asp:Label ID="lblGSUM_PRRA" runat="server" Text="GSUM PRRA"></asp:Label>
                            </div>


                            <div class="col14">
                                <asp:TextBox MaxLength="50" ID="txtGSUM_PRRA" runat="server" />

                            </div>

                            <div class="col15">
                                <asp:Label ID="lblcellGSUM_PRRA" runat="server" Text=" Cell"></asp:Label>

                            </div>

                            <div class="col16">
                                <asp:TextBox MaxLength="50" ID="txtCellGSUM_PRRA" runat="server" />

                            </div>
                        </div>


                    </div>
                    <div class="ContainerBottom">

                        <div class="col12">
                        </div>
                        <div class="col13" style="float: right; margin-right: 316px;">
                            <br />
                            <br />

                        </div>
                    </div>
                    <center>
                    <asp:Button runat="server" style="margin-right:39%" ID="btnSaveSource" OnClick="btnSaveSource_Click" Text="Save" />
                    </center>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveSource" />
            <asp:PostBackTrigger ControlID="lbl_file_fuGSUMMTL" />
            <asp:PostBackTrigger ControlID="lblfuGsumSmallWS" />
            <asp:PostBackTrigger ControlID="lbl_file_fuGSUMMFGOps" />
            <asp:PostBackTrigger ControlID="lbl_file_fuMFGDel" />
            <asp:PostBackTrigger ControlID="lbl_file_fuEWPTracker" />
            <asp:PostBackTrigger ControlID="lbl_file_fuGSUM_IAP" />
            <asp:PostBackTrigger ControlID="lbl_file_fuRE_Implementation" />
            <asp:PostBackTrigger ControlID="lbl_file_fuGSUM_PRRA" />
            <asp:PostBackTrigger ControlID="lbl_file_fuGSUMPRWS" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
