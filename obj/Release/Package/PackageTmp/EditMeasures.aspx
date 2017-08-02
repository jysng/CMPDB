<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false" CodeBehind="EditMeasures.aspx.vb" MasterPageFile="~/layout.Master" Inherits="CMPDSB_DEVIN.EditMeasures" %>


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

          /*.divborder {
            border: 1px solid #CCC;

        }*/

          .ContainerOne {
              width: 100%;
              overflow: hidden;
          }

          .col1 {
              width: 200px;
              float: left;
          }

              .col1 span {
                  /*margin-bottom: 5px;*/
                  display: block;
              }

          .col1new {
              width: 100px;
              float: left;
          }

              .col1new span {
                  /*margin-bottom: 5px;*/
                  display: block;
              }


          .col2 {
              width: 200px;
              float: left;
          }

              .col2 span {
                  margin-bottom: 5px;
                  display: block;
              }

          .col2new {
              float: left;
          }

          .col2small {
              width: 150px;
              float: left;
              margin-left: 10px;
          }

          .col2new span {
              /*margin-bottom: 5px;*/
              display: block;
          }

          /*.add1 >  .col3ne span {
            margin-bottom: 5px;
            display: block;
        }*/

          .col3 {
              width: 200px;
              float: left;
          }

          .col3ne {
              width: 260px;
              float: left;
          }


          .col4 {
              width: 200px;
              float: left;
          }

          .col4new {
              width: 320px;
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

          .midbox {
              width: 100%;
              float: right;
              margin-top: 7px;
              margin-bottom: 5px;
          }

          .merge {
              width: 100px;
              float: left;
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
              /*margin-bottom: 10px;*/

              line-height:25px;
          }

          .add2 {
              width: 100%;
              line-height: 25px;
              float: left;
              /*margin-bottom: 10px;*/
          }

          .sapretone {
              width: 23%;
              float: left;
          }

          .sapretwo {
              width: 22%;
              float: left;
              margin-top: 4px;
          }

          .saprethree {
              width: 52%;
              float: left;
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
	 
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upnl">
        <ContentTemplate>
            <div class="container">
                  <div class="ContainerOne">
                <h2>CMPDB: Edit Startup Status and Measures</h2>
                    <br />
                       <div class="ContainerOne TopHead">
                    <div class="col1">
                        <asp:Button ID="btnSerach" OnClick="btnSerach_Click"  runat="server" Text="Search Exisiting Measures" />

                     
                    </div>
                    </div>
                  <br />
                  <br />
                   <fieldset id="fdiv" runat="server" style="display:none;"  >              
                                <div id="divSrchExist" class="ContainerOne SearchBox" runat="server" style="display:none;">

                    <div class="ContainerOne">
                        <div class="midfix">
                            <div class="add">
                                <div class="col1">
                                    
                                    <asp:Label ID="lblPlant" CssClass="lbl" runat="server" Text="Plant"></asp:Label>
                                </div>
                                <div class="col2">

                                    <asp:DropDownList ID="ddlPlant" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="true" Height="25px" Width="200px" runat="server">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>



                                </div>
                            </div>
                            <div class="add">
                                <div class="col1">

                                    <asp:Label ID="lblSUL" CssClass="lbl" runat="server" Text="SUL"></asp:Label>

                                </div>
                                <div class="col2">

                                    <asp:DropDownList ID="ddlSUL" AppendDataBoundItems="true" Height="25px" Width="200px" runat="server">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>

                            <div class="add">
                                <div class="col1">

                                    <asp:Label ID="lblProjectStatus" CssClass="lbl" runat="server" Text="Project Status"></asp:Label>

                                </div>
                                <div class="col2">

                                    <asp:DropDownList ID="ddlProjectStatus" AppendDataBoundItems="true" Height="25px" Width="200px" runat="server">

                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Hold/Pending</asp:ListItem>
                                        <asp:ListItem Value="0">Complete</asp:ListItem>
                                        <asp:ListItem Value="0">Cancelled</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>


                            <div class="add">

                                <div class="col1">

                                    <asp:Label ID="lblLastUpdate" CssClass="lbl" runat="server" Text="Last Update"></asp:Label>

                                </div>
                                <div class="col2">

                                    <asp:TextBox ID="txtLastUpdate" Height="25px" CssClass="datepicker" placeholder="MM/DD/YYYY" Width="200px" runat="server"></asp:TextBox>
                                                          

                                   
                                </div>
                            </div>

                            <div class="add">

                                <div class="col1">
                                </div>
                                <div class="col2" style="margin-left: 347px;">

                                    <asp:Button ID="btnRefineSearch" Text="Search" OnClick="btnRefineSearch_Click" runat="server" />
                                </div>
                            </div>


                        </div>





                    </div>




                    <br />



                    <div class="ContainerOne">

                        <div class="col1">
                            <h2>Search Results</h2>
                        </div>
                        <br />
                        <br />
                        <asp:GridView ID="gridProjects" Width="100%" AutoGenerateColumns="False" OnRowCommand="gridProjects_RowCommand" runat="server" ShowHeaderWhenEmpty="true">
                            <Columns>



                                <asp:TemplateField HeaderText="Startup Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text='<%#Eval("Startup_Name") %>' CommandName="EditDetails"
                                            CommandArgument='<%# Eval("Startup_ID") %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant">
                                    <ItemTemplate>
                                        <%#Eval("plant") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <%#Eval("project_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%--  <asp:TemplateField HeaderText="Project Name" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("Project_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                   <asp:TemplateField HeaderText="Project Type">
                                    <ItemTemplate>
                                        <%#Eval("Project_Type") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Status">
                                    <ItemTemplate>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Change Type">
                                    <ItemTemplate>
                                        <%#Eval("Change_Type") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Unit">
                                    <ItemTemplate>
                                        <%#Eval("Business_Unit") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUL">
                                    <ItemTemplate>
                                        <%#Eval("email") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Production Line">
                                    <ItemTemplate>
                                        <%#Eval("Production_Line") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="PCA Approved">
                                    <ItemTemplate>
                                        <%#Eval("PCA_Approved") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Startup Id">
                                    <ItemTemplate>
                                        <%#Eval("Startup_ID") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
               </fieldset>
                <br />
                <br />
                <div class="ContainerOne">
                    <h2>Critical Milestones</h2>
                </div>

                <div class="ContainerOne">
                    <div class="col1new">
                        <asp:TextBox ID="txtPCStarategyAndCommitment" Width="90px" ReadOnly="true" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                        

                    </div>
                    <div class="col2new">
                        <asp:Label ID="lblStrategy" runat="server" Text="PC (Defination) /Starategy and Commitment"></asp:Label>
                    </div>
                </div>
                <br />
                <div class="ContainerOne">
                    <div class="col1new">
                        <asp:TextBox ID="txtStartUpWorkProcess" ReadOnly="true" Width="90px" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                    </div>
                    <div class="col2new">
                        <asp:Label ID="lblStartUpWorkProcess" runat="server" Text="Start Up Work Process"></asp:Label>
                    </div>

                </div>
                <br />
                <div class="ContainerOne">
                    <div class="col1new">
                        <asp:TextBox ID="txtStartOfProduction" ReadOnly="true" Width="90px" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                    </div>
                    <div class="col2new">
                        <asp:Label ID="lblStartOfProduction" runat="server" Text="Start Of Production"></asp:Label>
                    </div>

                </div>
                <br />
                <div class="ContainerOne">
                    <div class="col1new">
                        <asp:TextBox ID="txtNextVATDate" ReadOnly="true" Width="90px" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                    </div>
                    <div class="col2new">
                        <asp:Label ID="lblNextVATDate" runat="server" Text="Next VAT Date"></asp:Label>
                    </div>

                </div>
                <br />
                <div class="ContainerOne">
                    <div class="col1new">
                        <asp:TextBox ID="txtNextConstructionDate" ReadOnly="true" Width="90px" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                    </div>
                    <div class="col2new">
                        <asp:Label ID="Label4" runat="server" Text="Next Construction Date"></asp:Label>
                    </div>
                </div>
                <br />
                <div class="ContainerOne">
                    <div class="col1new">
                        <asp:TextBox ID="txtNextEO" Width="90px" ReadOnly="true" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>

                    </div>
                    <div class="col2new">
                        <asp:Label ID="Label5" runat="server" Text="Next EO"></asp:Label>
                    </div>
                    <div class="midfix3" style="float: right;">
                        <div class="merge">
                            <asp:Button ID="btnMileStone" runat="server" Enabled="false" OnClick="btnMileStone_Click" Text="Request Target Or Milestone Change"></asp:Button>
                        </div>
                    </div>
                </div>
                <hr />
            <br />
                <div class="ContainerOne">
                    <h2>In Process Measures</h2>
                </div>

                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1new" style="width:300px;">
                                 Warning: No Startup Update  in last 30 days
                                </div>
                            <br />
                                   <br />
                               <br />  
                             <div class="add">
                            <div class="col1new">
                                <asp:TextBox ID="txtTotalSafetyIncidents" Width="80px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2new">
                                <asp:Label ID="lblTotalSafetyIncidents" runat="server" Text="Total SafetyIncidents or near misses"></asp:Label>

                            </div>



                        </div>
                        <br />
                        <div class="add">
                            <div class="col1new">
                                <asp:TextBox ID="txtETC" Width="80px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2new">
                                <asp:Label ID="lblETC" runat="server" Text="ETC $M"></asp:Label>

                            </div>


                        </div>
                        <br />
                        <div class="add">
                            <div class="col1new">
                                <asp:TextBox ID="txtPRLast" Width="80px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2new">
                                <asp:Label ID="lblPRLast" runat="server" Text="Current PR% Last Month Avg"></asp:Label>
                            </div>

                        </div>
                            </div>
                          </div>
                    <div class="midfix3" style="float: right;">

                        <div class="merge" style="margin-top: 10px; width: 500px;">
                            Comments or major deliverables working on/completed
                        </div>
                    </div>
                    <div class="midfix3" style="float: right; margin-top: 10px;">

                        <div class="merge">

                            <asp:TextBox ID="txtComments" Width="400px" TextMode="MultiLine" runat="server" Style="height: 140px;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="midfix3" style="float: right;">

                        <div class="merge" style="margin-top: 40px; width: 500px; margin-left:485px;">

                            <asp:Button ID="btnhg" Text="<<" runat="server" />
                            <asp:Button ID="Button2" Text="<" runat="server" />
                            <asp:TextBox ID="txtst" runat="server"></asp:TextBox>
                            <asp:Button ID="Button3" Text=">" runat="server" />
                            <asp:Button ID="Button4" Text=">>" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button5" Text="Today" runat="server" />
                        </div>
                    </div>
                    <div class="midfix3" style="float: right;">

                        <div class="merge" style="margin-top: 10px;">
                            <asp:Button ID="btnAcceptUpdate" Enabled="false" OnClick="btnAcceptUpdate_Click" Text="Accept Update" runat="server" />
                             </div>
                        </div>
                    </div>
               </div>
              
                <div class="ContainerOne">
                    <div class="midfix" style="width:100%">
                       
                      <div class="sapretone">

                        <div class="add1">
                              <div style="width: 250px;"" >
                                <asp:CheckBox ID="chk1" runat="server" AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />Work Sheet and Cell Address
                                  <asp:ImageButton runat="server" ID="btnimgRefresh" OnClick="btnimgRefresh_Click" ImageUrl="images/Refresh.png" width="16px" />
                            </div>
                       
                               <div class="col1new" style="margin-left: 10px; width: 80px;">
                                <span style="color: blue">Value</span>

                            </div>
                            <div class="col1new" style="margin-left: 20px; width: 120px;">
                                <span style="color: blue">Last Update Date</span>

                            </div>
                      
                        </div>
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell1" Width="80px" runat="server"></asp:TextBox>
                            </div>
                               <div class="col2small">
                                <asp:Label ID="lblFile1LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                          

                        </div>
                        <br />
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell2" Width="80px" runat="server"></asp:TextBox>
                            </div>
                             <div class="col2small">
                                <asp:Label ID="lblFile2LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                        

                        </div>
                        <br />
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell3" Width="80px" runat="server"></asp:TextBox>

                            </div>
                             <div class="col2small">
                                <asp:Label ID="lblFile3LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                            

                        </div>
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell4" Width="80px" runat="server"></asp:TextBox>
                            </div>
                               <div class="col2small">
                                <asp:Label ID="lblFile4LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                           

                        </div>
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell5" Width="80px" runat="server"></asp:TextBox>
                            </div>
                                <div class="col2small">
                                <asp:Label ID="lblFile5LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                        

                        </div>
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell6" Width="80px" runat="server"></asp:TextBox>
                            </div>
                             <div class="col2small">
                                <asp:Label ID="lblFile6LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                       

                        </div>
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell7" Width="80px" runat="server"></asp:TextBox>
                            </div>
                             <div class="col2small">
                                <asp:Label ID="lblFile7LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                         

                        </div>
                        <div class="add1">
                            <div class="col1new">
                                <asp:TextBox ID="txtCell8" Width="80px" runat="server"></asp:TextBox>
                            </div>
                               <div class="col2small">
                                <asp:Label ID="lblFile8LstUpdDte" Text="Last Update Date" runat="server"></asp:Label>
                            </div>
                           
                        </div>
                          </div>

                        
                        <div class="sapretwo" runat="server" style="display:none" id="dvCellWork">
                       
                      

                     <div class="add1">
                              <div style="width: 250px;"" >
                              &nbsp;

                            </div>

                               <div class="col1new" style="margin-left: 10px; width: 80px;">
                                <span style="color: blue">Worksheet </span>

                            </div>
                            <div class="col1new" style="margin-left: 20px;width: 120px;">
                                <span style="color: blue">Cell Address</span>

                            </div>
                      
                        </div>


                        <div class="add1">
                                <div class="col1new">
                                <asp:TextBox ID="txtws1" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc1" Width="80px" runat="server"></asp:TextBox>
                            </div>
                         
                        
                        </div>


                        <br />
                        <div class="add1">
                              <div class="col1new">
                                <asp:TextBox ID="txtws2" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc2" Width="80px" runat="server"></asp:TextBox>
                            </div>
                           
                         
                        </div>
                        <br />
                        <div class="add1">
                             <div class="col1new">
                                <asp:TextBox ID="txtws3" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc3" Width="80px" runat="server"></asp:TextBox>
                            </div>
                           
                         

                        </div>



                        <div class="add1">
                       <div class="col1new">
                                <asp:TextBox ID="txtws4" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc4" Width="80px" runat="server"></asp:TextBox>
                            </div>
                         
                          

                        </div>
                        <div class="add1">
                          
                                 <div class="col1new">
                                <asp:TextBox ID="txtws5" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc5" Width="80px" runat="server"></asp:TextBox>
                            </div>
                        
                          
                        </div>


                        <div class="add1">
                             <div class="col1new">
                                <asp:TextBox ID="txtws6" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc6" Width="80px" runat="server"></asp:TextBox>
                            </div>
                           
                       

                        </div>

                        <div class="add1">
                         
                                <div class="col1new">
                                <asp:TextBox ID="txtws7" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc7" Width="80px" runat="server"></asp:TextBox>
                            </div>
                           
                          
                        </div>


                        <div class="add1">
                        <div class="col1new">
                                <asp:TextBox ID="txtws8" Width="80px" runat="server"></asp:TextBox>
                            </div>
                              <div class="col1new">
                                <asp:TextBox ID="txtwc8" Width="80px" runat="server"></asp:TextBox>
                            </div>
                         
                       

                        </div>


                        
                        
                        
                     
                 

                </div>

                        <div class="saprethree">
                       
                      

                     <div class="add2">
                              <div style="width: 250px;"" >
                   

                            </div>

                               <%--<div class="col1new" style="margin-left: 10px; width: 80px;">
                               <asp:Label ID="lbjk" runat="server">   </asp:Label>

                            </div>--%>
                            <div class="col1new" style="width: 120px;">
                        &nbsp;
                                                              <span style="color: blue">SWP Tool Name</span>
                            </div>  

                     </div>
                            <br />
                            <br />
                        <div class="add2">
                              <div class="col3ne">
                                <asp:Label ID="lblFile1" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile1" Visible="false" OnClick="DownloadFile_Click" runat="server" ForeColor="Blue"></asp:LinkButton>
                            </div>                              
                            

                        </div>


                       

                        <div class="add2">
                                 <div class="col2new">
                                <asp:Label ID="lblFile2" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile2" Visible="false" CommandArgument="File2" OnClick="DownloadFile_Click" ForeColor="Blue" runat="server"></asp:LinkButton>
                            </div>
                        </div>
                        


                        <div class="add2">
                           
                               <div class="col2new">
                                <asp:Label ID="lblFile3" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile3" Visible="false" CommandArgument="File3" OnClick="DownloadFile_Click" runat="server" ForeColor="Blue"></asp:LinkButton>
                            </div>


                        </div>

                          


                        <div class="add2">
                      
                              <div class="col2new">
                                <asp:Label ID="lblFile4" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile4" Visible="false" CommandArgument="File4" OnClick="DownloadFile_Click" runat="server" ForeColor="Blue"></asp:LinkButton>
                            </div>
                        </div>

                          

                        <div class="add2">
                          
                             <div class="col2new">
                                <asp:Label ID="lblFile5" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile5" Visible="false" CommandArgument="File5" OnClick="DownloadFile_Click" ForeColor="Blue" runat="server"></asp:LinkButton>
                            </div>
                        </div>


                        <div class="add2">
                             <div class="col2new">
                                <asp:Label ID="lblFile6" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile6" Visible="false" CommandArgument="File6" OnClick="DownloadFile_Click" runat="server" ForeColor="Blue"></asp:LinkButton>
                            </div>

                        </div>
                            

                        <div class="add2">
                           <div class="col2new">
                                <asp:Label ID="lblFile7" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile7" Visible="false" CommandArgument="File7" OnClick="DownloadFile_Click" runat="server" ForeColor="Blue"></asp:LinkButton>
                            </div>
                          </div>

                          

                        <div class="add2">
                  <div class="col3ne">
                                <asp:Label ID="lblFile8" s runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkFile8" Visible="false" CommandArgument="File1" OnClick="DownloadFile_Click" runat="server" ForeColor="Blue"></asp:LinkButton>
                            </div>

                        </div>

                </div>
                                    </div>
                    </div>
            <br />
                <div class="ContainerOne">
                    <h2>Output Measures
                    </h2>

                </div>
                <div class="ContainerOne">
                    Warning: No Startup update in last 30 days    
                </div>
                <br />
                <br />
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                TGT
                            </div>
                            <div class="col2" style="width: 70px;">
                                Actual

                            </div>
                            <div class="col3" style="width: 70px;">
                                Criteria Met

                            </div>
                            <div class="col4new">
                                </span>
                             

                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <asp:TextBox ID="TextBox17" Width="60px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2" style="width: 70px;">
                                <asp:TextBox ID="TextBox18" Width="60px" runat="server"></asp:TextBox>

                            </div>
                            <div class="col3" style="width: 70px;">
                                <asp:DropDownList ID="TextBox19" Width="60px" runat="server">
                                    <asp:ListItem>
                                        Yes
                                    </asp:ListItem>
                                    <asp:ListItem>
                                        No
                                    </asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col4new">
                                <span>ETC $M
                                </span>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <asp:TextBox ID="TextBox21" Width="60px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2" style="width: 70px;">
                                <asp:TextBox ID="TextBox22" Width="60px" runat="server"></asp:TextBox>

                            </div>
                            <div class="col3" style="width: 70px;">
                                <asp:DropDownList ID="DropDownList1" Width="60px" runat="server">
                                    <asp:ListItem>
                                        Yes
                                    </asp:ListItem>
                                    <asp:ListItem>
                                        No
                                    </asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col4new">
                                <span>PR%(First 30 days average for small projects)
                                </span>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <asp:TextBox ID="TextBox23" Width="60px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2" style="width: 70px;">
                                <asp:TextBox ID="TextBox24" Width="60px" runat="server"></asp:TextBox>

                            </div>
                            <div class="col3" style="width: 70px;">
                                <asp:DropDownList ID="DropDownList2" Width="60px" runat="server">
                                    <asp:ListItem>
                                        Yes
                                    </asp:ListItem>
                                    <asp:ListItem>
                                        No
                                    </asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col4new">
                                <span>GSUM+ SmallProject Readiness checklist @ closeout
                                </span>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <asp:TextBox ID="TextBox25" Width="60px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2" style="width: 70px;">
                                <asp:TextBox ID="TextBox26" Width="60px" runat="server"></asp:TextBox>

                            </div>
                            <div class="col3" style="width: 70px;">
                                <asp:DropDownList ID="DropDownList3" Width="60px" runat="server">
                                    <asp:ListItem>
                                        Yes
                                    </asp:ListItem>
                                    <asp:ListItem>
                                        No
                                    </asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col4new">
                                <span>SOP Date
                                </span>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <asp:TextBox ID="TextBox27" Width="60px" runat="server"></asp:TextBox>
                            </div>
                            <div class="col2" style="width: 70px;">
                                <asp:TextBox ID="TextBox28" Width="60px" runat="server"></asp:TextBox>

                            </div>
                            <div class="col3" style="width: 70px;">
                                <asp:DropDownList ID="DropDownList4" Width="60px" runat="server">
                                    <asp:ListItem>
                                        Yes
                                    </asp:ListItem>
                                    <asp:ListItem>
                                        No
                                    </asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col4new">
                                <span># of Safety incidents(Near Miss and Up)
                                </span>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <span style="width: 60px;"></span>
                            </div>
                            <div class="col2" style="width: 70px;">

                                <span style="width: 60px;"></span>
                            </div>

                            <div class="col3new">
                                <span>HS_E
                                </span>
                                <div class="col4new" style="width: 70px;">
                                    <asp:DropDownList ID="DropDownList7" Width="60px" runat="server">
                                        <asp:ListItem>
                                        Yes
                                        </asp:ListItem>
                                        <asp:ListItem>
                                        No
                                        </asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1" style="width: 70px;">
                                <span style="width: 60px;"></span>
                            </div>
                            <div class="col2" style="width: 70px;">

                                <span style="width: 60px;"></span>
                            </div>

                            <div class="col3new">
                                <span>Quantitys
                                </span>
                                <div class="col4new" style="width: 70px;">
                                    <asp:DropDownList ID="DropDownList5" Width="60px" runat="server">
                                        <asp:ListItem>
                                        Yes
                                        </asp:ListItem>
                                        <asp:ListItem>
                                        No
                                        </asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="ContainerOne">
                    <div class="midfix">
                        <div class="add">
                            <div class="col1">
                                All  Small Start Up  Criteria Met
                            </div>
                            <div class="col2">
                                <asp:TextBox ID="TextBoxdf20" Text="No" Width="60px" runat="server"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="midfix3" style="float: left;">

                        <div class="merge">

                            <asp:Button ID="Button6" Text="Close Project" runat="server" />
                        </div>
                    </div>
                </div>
                 </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkFile1" />
            <asp:PostBackTrigger ControlID="lnkFile2" />
            <asp:PostBackTrigger ControlID="lnkFile3" />
            <asp:PostBackTrigger ControlID="lnkFile4" />
            <asp:PostBackTrigger ControlID="lnkFile5" />
            <asp:PostBackTrigger ControlID="lnkFile6" />
            <asp:PostBackTrigger ControlID="lnkFile7" />
            <asp:PostBackTrigger ControlID="lnkFile8" />
            <%--<asp:PostBackTrigger ControlID="lnkFile1" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
