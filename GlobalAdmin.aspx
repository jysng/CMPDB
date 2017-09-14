<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GlobalAdmin.aspx.vb" MasterPageFile="~/layout.Master" Inherits="CMPDSB_DEVIN.GlobalAdmin" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">

        .container {
            width: 90%;
            margin: 0 5%;
            margin-bottom: 50px;
        }

        .ContainerOne {
            width: 100%;
            overflow: hidden;
        }

        .col1 {
            width: 27%;
            float: left;
        }

            .col1 span {
                margin-bottom: 5px;
                display: block;
            }

        .col2 {
            width: 27%;
            float: left;
        }

            .col2 span {
                margin-bottom: 5px;
                display: block;
            }

        .col3 {
            width: 25%;
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
            .alert-box {
    padding: 15px;
    margin-bottom: 20px;
    border: 1px solid transparent;
    border-radius: 4px;  
}

.success {
    color: #3c763d;
    background-color: #dff0d8;
    border-color: #d6e9c6;
    display: none;
}

    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upnl">
       <ContentTemplate>
      <div class="container">
        <h2>CMPDB: Global Administrator</h2>
        <div class="ContainerOne">      
                 <div class="col1">
                <asp:Label ID="lblProductionTypes" CssClass="lbl" runat="server" Text="Production Types"></asp:Label>
                <asp:ListBox ID="ListBoxProductionType" CssClass="ddl ddlgLOBAL" OnSelectedIndexChanged="ListBoxProductionType_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtProductionTypes" ClientIDMode="Static" runat="server" CssClass="txtgLOBAL" />
                    <asp:ImageButton CssClass="btngLOBAL" ID="btnAddProductionTypes" OnClick="btnAddProductionTypes_Click" runat="Server" ImageUrl="~/images/Add.png" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteProductionTypes_Click" ID="btnDeleteProductionTypes" />
                    <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshProductionTypes_Click" runat="server" ID="btnRefreshProductionTypes" />
                    
                </div>
            </div>
            <div class="col2">
            </div>
            <div class="col3">
            </div>
        </div>
         
        <div class="ContainerOne">
            <div class="col1">
            <asp:Label ID="lblProjectTypes" CssClass="lbl" runat="server" Text="Project Types"></asp:Label><br />
            <asp:ListBox ID="ListBoxprojType" CssClass="ddl ddlgLOBAL" AutoPostBack="true" runat="server" ></asp:ListBox>

            <div class="midbox">

                <asp:TextBox MaxLength="50" ID="txtprojectType" CssClass="txtgLOBAL" runat="server" />

                <asp:ImageButton Text="Add" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" runat="server" ID="btnaddtxtprojectType" />
                <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" ID="btnDeletetxtprojectType" />


                <asp:ImageButton Text="Refresh" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" ID="btnRefreshtxtprojectType" />
            </div>
        </div>
            <div class="col2">


            <div class="col3">
            </div>

        </div>
       </div>
        <div class="ContainerOne">


            <div class="col1">
                <asp:Label ID="lblChangeType" CssClass="lbl" runat="server" Text="Change Type"></asp:Label>
                <asp:ListBox ID="ListBoxChangType" CssClass="ddl ddlgLOBAL" AutoPostBack="true" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtChangType" CssClass="txtgLOBAL" runat="server" />
                    <asp:ImageButton Text="Add" CssClass="btngLOBAL" ImageUrl="~/images/Add.png"  runat="server" ID="btnAddChangType" />
                    <asp:ImageButton Text="Delete" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" ID="btnDeleteChangType" />
                    <asp:ImageButton Text="Refresh" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" ID="BtnRefreshChangType" />
                </div>

            </div>

            <div class="col2">
            </div>

            <div class="col3">
            </div>

        </div>
        <div class="ContainerOne">
            <div class="col1">
                <asp:Label ID="lblBusinessUnits" CssClass="lbl" runat="server" Text="Business Units"></asp:Label>
                <asp:ListBox ID="ListBoxBusinUnit" CssClass="ddl ddlgLOBAL" AutoPostBack="true" OnSelectedIndexChanged="ListBoxBusinUnit_SelectedIndexChanged" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtBusinUnit" CssClass="txtgLOBAL" runat="server" />
                    <asp:ImageButton Text="Add" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" runat="server" ID="btnAddBusinUnit" OnClick="btnAddBusinUnit_Click" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" ID="BtnDeleteBusinUnit" OnClick="BtnDeleteBusinUnit_Click" />
                    <asp:ImageButton Text="Refresh" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" ID="BtnRefreshBusinUnit" OnClick="BtnRefreshBusinUnit_Click" />
                </div>
            </div>
            <div class="img">
                <asp:Image ID="Image1" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
            <div class="col2">
                <asp:Label ID="lblPlatform" CssClass="lbl" runat="server" Text="Platform"></asp:Label>
                <asp:ListBox ID="ListBoxPlatform" CssClass="ddl ddlgLOBAL" OnSelectedIndexChanged="ListBoxPlatform_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtPlatform" CssClass="txtgLOBAL" runat="server" />

                    <asp:ImageButton Text="Add" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" runat="server" ID="BtnAddPlatform" OnClick="BtnAddPlatform_Click" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" ID="BtnDeletePlatform" OnClick="BtnDeletePlatform_Click" />
                    <asp:ImageButton Text="Refresh" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" ID="btnRefreshPlatform" OnClick="btnRefreshPlatform_Click" />
                </div>
            </div>
           
            <div class="col3" style ="margin-left: 113px;margin-top: -156px;">
                <asp:Label Visible="false" ID="PlantsLBL" CssClass="lbl" runat="server" Text="Plant Associated with Business Units"></asp:Label>
                <asp:DropDownList Visible="false" ID="DropDownlblPlantsAssociateBU" AppendDataBoundItems="true" Width="100%" runat="server">
                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                </asp:DropDownList>

                </div>
            </div>
        
        <div class="ContainerOne">
            <div class="col1">
                <asp:Label ID="lblSwp" CssClass="lbl" runat="server" Text="SWP"></asp:Label>
                <asp:ListBox ID="ListBoxSWP" CssClass="ddl ddlgLOBAL" runat="server" AutoPostBack="true" ></asp:ListBox>

                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtSWP" CssClass="txtgLOBAL" runat="server" />

                    <asp:ImageButton ID="BtnAddSWP" runat="server" Text="Add" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" />
                    <asp:ImageButton ID="BtnDeleteSWP" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" Text="Delete" />
                    <asp:ImageButton ID="BtnRefreshSWP" runat="server" Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" />

                </div>
            </div>
            <div class="img">
                <asp:Image ID="Image2" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
            <div class="col2">
                <asp:Label CssClass="lbl" ID="lblPracticionerRole" runat="server" Text="Practitioner Role"></asp:Label>
                <asp:ListBox ID="ListBoxPracticionerRole" CssClass="ddl ddlgLOBAL" OnSelectedIndexChanged="ListBoxPracticionerRole_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtPracticionerRole" CssClass="txtgLOBAL" runat="server" />
                    <asp:ImageButton Text="Add" runat="server" ID="btnAddPracticionerRole" OnClick="btnAddPracticionerRole_Click" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" ID="btnDeletePracticionerRole" OnClick="btnDeletePracticionerRole_Click" />
                    <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" runat="server" ID="BtnRefreshPracticionerRole" OnClick="BtnRefreshPracticionerRole_Click" />
                </div>
            </div>
          
            <div class="col3">
                <%--<asp:Label ID="lblQualificationLevelForRole" CssClass="lbl" runat="server" Text="Qualification Level For Role"></asp:Label>
                <asp:DropDownList ID="DropDownListQLevel" AppendDataBoundItems="true" Width="100%" runat="server">
                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                </asp:DropDownList>--%>

            </div>
        </div>
        <div class="ContainerOne">
            <div class="col1">
                &nbsp
            </div>
            <div class="img">
                <img  id="ContentPlaceHolder1_Image5" src="images/arrow.png" style="height:32px;width:32px;">
            </div>
            <div class="col2">
                <asp:Label CssClass="lbl" ID="lblSWP_Tool_Name"  runat="server" Text="SWP Tool Name"></asp:Label>
                <asp:ListBox ID="lbSWP_Tool_Name"  OnSelectedIndexChanged="lbSWP_Tool_Name_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="ddl ddlgLOBAL" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtSWP_Tool_Name" runat="server" CssClass="txtgLOBAL" />
                    <asp:ImageButton ID="btnAddSWP_Tool_Name" OnClick="btnAddSWP_Tool_Name_Click" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" Text="Add" />
                    <asp:ImageButton ID="btnDeleteSWP_Tool_Name" OnClick="btnDeleteSWP_Tool_Name_Click" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" Text="Delete" />
                    <asp:ImageButton ID="btnRefreshSWP_Tool_Name" OnClick="btnRefreshSWP_Tool_Name_Click" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" Text="Refresh" />
                    <br />
                </div>

            </div>
            <div class="img2">
                <asp:Image ID="Image4" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
            <div class="col3">
                <asp:Label ID="lblCorporate_WS_Name" CssClass="lbl" runat="server" Text="Corporate Work Sheet Name"></asp:Label>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtCorporate_WS_Name" CssClass="txtgLOBAL" runat="server"></asp:TextBox>
                </div>

                <%--  <asp:Label ID="lblSWP_ID" CssClass="lbl" runat="server" Text="SWP ID"></asp:Label>
                <div class="midbox">
            <asp:DropDownList ID="ddlSWP_ID" AppendDataBoundItems="true" Width="338px" runat="server">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
            </asp:DropDownList>
                    </div>--%>
                <asp:Label ID="lblCorporate_Cell_Location_For_Score" CssClass="lbl" runat="server" Text="Corporate Cell Location For Score"></asp:Label>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtCorporate_Cell_Location_For_Score" CssClass="txtgLOBAL" runat="server"></asp:TextBox >
                </div>
                <asp:Label ID="lblCorporate_Tool_Source_LOcation" CssClass="lbl" runat="server" Text="Upload File"></asp:Label>
                <asp:LinkButton ID="lnkbtnFileName" OnClick="lnkbtnFileName_click" runat="server" Text="" ></asp:LinkButton>
                <div>
                     <asp:LinkButton style="display:none" ID="btnEdit"  runat="server" OnClick="btnEdit_Click" Text="Edit" />
                   <%--<asp:ImageButton ID="btnEdit" style="display:none" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Update.png" OnClick="btnEdit_Click" Text="Edit" />--%>
                <asp:LinkButton style="display:none" ID="btnCancel" runat="server" Text="Cancel" />
                     </div>
                    <asp:FileUpload  ID="fuCorporate_Standard_File"  runat="server" />
                    <%--<asp:LinkButton style="display:none" ID="btnupload" OnClick="btnupload_click" runat="server" Text="Upload" />--%>
                    <%--<asp:TextBox MaxLength="50" ID="txtCorporate_Tool_Source_LOcation" CssClass="txtgLOBAL" runat="server"></asp:TextBox MaxLength="50">--%>
            </div>
        </div>
        <div class="ContainerOne">

            <div class="col1">
                <asp:Label CssClass="lbl" ID="lblComplexitySituation" runat="server" Text="Complexity Situation"></asp:Label>
                <asp:ListBox ID="ListBoxComplexitySituation" runat="server" AutoPostBack="true" CssClass="ddl ddlgLOBAL"></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtComplexitySituation" runat="server" CssClass="txtgLOBAL" />
                    <asp:ImageButton ID="BtnAddComplexitySituation" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" Text="Add" />
                    <asp:ImageButton ID="BtnDeleteComplexitySituation" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" Text="Delete" />
                    <asp:ImageButton ID="BtnRefreshComplexitySituation" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" Text="Refresh" />
                    <br />
                </div>
            </div>
            <div class="img">
                <asp:Image ID="Image5" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
            <div class="col2">
                <asp:Label ID="lblPracticionerRoleID" CssClass="lbl" runat="server" Text="Practicioner Role"></asp:Label>
                <asp:DropDownList ID="ddlPracticionerRoleID" AppendDataBoundItems="true" Width="100%" runat="server">
                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="img2">
                <asp:Image ID="Image6" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
            <div class="col3">
                <asp:Label ID="lblPSQualificationLevel" CssClass="lbl" runat="server" Text="Qualification Level"></asp:Label>
                <asp:DropDownList ID="ddlPSQualificationLevel" AppendDataBoundItems="true" Width="100%" runat="server">
                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>	
        <div class="ContainerOne">           
                
            <div class="col1">
                <asp:Label ID="lblqulaificationlevel" CssClass="lbl" runat="server" Text="Qualification Level"></asp:Label>
                <asp:ListBox ID="ListBoxQualificationTypes" CssClass="ddl ddlgLOBAL"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="ListBoxQualificationType_SelectedIndexChanged"></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtQualificationTypes" ClientIDMode="Static" runat="server" CssClass="txtgLOBAL" />

                    <asp:ImageButton CssClass="btngLOBAL" ID="btnAddQualificationTypes"  OnClick="btnAddQualificationTypes_Click" runat="Server" ImageUrl="~/images/Add.png" />

                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteQualificationTypes_Click" ID="btnDeletequalificationTypes" />


                    <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" runat="server" OnClick="btnRefreshQualificationTypes_Click" ID="btnRefreshQualificationTypes" />
                </div>
            </div>
            <div class="col2">
            </div>

            <div class="col3">
            </div>

        </div>

    <%--    <div class="ContainerOne">      
                 <div class="col1">
                <asp:Label ID="Label1" CssClass="lbl" runat="server" Text="Production Types"></asp:Label>
                <asp:ListBox ID="ListBox1" CssClass="ddl ddlgLOBAL" OnSelectedIndexChanged="ListBoxProductionType_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="TextBox1" ClientIDMode="Static" runat="server" CssClass="txtgLOBAL" />
                    <asp:ImageButton CssClass="btngLOBAL" ID="ImageButton1" OnClick="btnAddProductionTypes_Click" runat="Server" ImageUrl="~/images/Add.png" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteProductionTypes_Click" ID="ImageButton2" />
                    <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshProductionTypes_Click" runat="server" ID="ImageButton3" />
                    
                </div>
            </div>
            <div class="col2">
            </div>
            <div class="col3">
            </div>
        </div>--%>

        <div class="ContainerOne">      
                 <div class="col1">
                <asp:Label ID="Label2" CssClass="lbl" runat="server" Text="Regions"></asp:Label>
                <asp:ListBox ID="ListBoxRegions" CssClass="ddl ddlgLOBAL" OnSelectedIndexChanged="ListBoxRegions_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtRegions" ClientIDMode="Static" runat="server" CssClass="txtgLOBAL" />
                    <asp:ImageButton CssClass="btngLOBAL" ID="btnAddRegions" OnClick="btnAddRegions_Click" runat="Server" ImageUrl="~/images/Add.png" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" OnClick="btnDeleteRegions_Click" ID="btnDeleteRegions" />
                    <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshRegions_Click" runat="server" ID="btnRefreshRegions" />
                </div>
            </div>
            <div class="col2">
            </div>
            <div class="col3">
            </div>
        </div>
        <div class="ContainerOne">
            <div class="col1">
                <asp:Label ID="lblPlants" CssClass="lbl" runat="server" Text="Plants"></asp:Label>
                <asp:ListBox ID="ListBoxPlants" CssClass="ddl ddlgLOBAL" AutoPostBack="true" OnSelectedIndexChanged="ListBoxPlants_SelectedIndexChanged" runat="server"></asp:ListBox>
                <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtPalnts" CssClass="txtgLOBAL" runat="server" />
                    <asp:ImageButton Text="Add" CssClass="btngLOBAL" ImageUrl="~/images/Add.png" runat="server" ID="btnAddPlants" OnClick="btnAddPlants_Click" />
                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server" ID="BtnDeletePlants" OnClick="BtnDeletePlants_Click" />
                    <asp:ImageButton Text="Refresh" runat="server" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" ID="BtnRefreshPlants" OnClick="BtnRefreshPlants_Click" />
                </div>
            </div>
            <div class="img">
                <asp:Image ID="Image7" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
             <div class="col2">
                <asp:Label ID="lblRegion" CssClass="lbl"  runat="server" Text="Region"></asp:Label>
                 <asp:DropDownList AppendDataBoundItems="true" runat="server" id="ddlRegionTypes" Width="100%">
                      <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                 </asp:DropDownList>
                <%--<asp:ListBox ID="ListBoxRegionTypes" CssClass="ddl ddlgLOBAL"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="ListBoxRegionType_SelectedIndexChanged"></asp:ListBox>--%>

               <%-- <div class="midbox">
                    <asp:TextBox MaxLength="50" ID="txtRegionTypes" ClientIDMode="Static" runat="server" CssClass="txtgLOBAL" />

                    <asp:ImageButton CssClass="btngLOBAL" ID="btnAddRegionTypes"  OnClick="btnAddRegionTypes_Click" runat="Server" ImageUrl="~/images/Add.png" />

                    <asp:ImageButton Text="Delete" CssClass="btngLOBAL" ImageUrl="~/images/Delete.png" runat="server"  OnClick="btnDeleteRegionTypes_Click" ID="btnDeleteRegionTypes" />


                    <asp:ImageButton Text="Refresh" CssClass="btngLOBAL" ImageUrl="~/images/Refresh.png" OnClick="btnRefreshRegionTypes_Click" runat="server" ID="btnRefreshRegionTypes" />
                </div>--%>
            </div>
           <div class="img">
                <asp:Image ID="Image8" ImageUrl="~/images/arrow.png" runat="server" Height="32px" Width="32px" />
            </div>
          
            <div class="col3" style ="margin-left: 568px;margin-top: -156px;">
                  <asp:Label ID="lblBU" CssClass="lbl" runat="server" Text="Business Unit"></asp:Label>
             <asp:DropDownList ID="ddlBU" AppendDataBoundItems="true" Width="100%" runat="server">
                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <div class="midbox">
                </div>
            </div>
              </div>

    </div>
          </ContentTemplate>
       <Triggers>
           <asp:PostBackTrigger ControlID="btnAddSWP_Tool_Name" />
           <asp:PostBackTrigger ControlID="lnkbtnFileName" />
       </Triggers>
         </asp:UpdatePanel>
</asp:Content>
