<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="UploadReportTemplate.aspx.vb" Inherits="CMPDSB_DEVIN.UploadReportTemplate" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="ContainerOne">


            <div class="col1" style="width: 670px;">
                <h2>Upload Report Template </h2>
            </div>
        </div>
        <div class="ContainerOne">
            <div class="col2">
                <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Choose Template Type:"></asp:Label>
                <asp:DropDownList ID="ddlTemplateType" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                    <%--<asp:ListItem Text="Project Tracking Report" Value="PTR"></asp:ListItem>--%>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="ddlTemplateType" InitialValue="" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
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
           
    </div>
        <div class="ContainerOne">
         <div class="col1">
                <asp:Button runat="server" Text="Upload Template" ID="btnUploadTemplate" OnClick="btnUploadTemplate_Click" />

                <asp:Button runat="server" Text="Reset" ID="btnReset" />

                <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text=""></asp:Label>
            </div>
              </div>

       <div class="ContainerOne">
           <asp:GridView CssClass="table" OnRowCommand="gvGrid_RowCommand" AutoGenerateColumns="false" ID="gvGrid" runat="server" EmptyDataText="No records found."  AllowSorting="true" ShowHeader="true"  >
                        <Columns>
                            <asp:BoundField DataField="Lib_ref_ID" HeaderText="ID" />
                           <asp:TemplateField HeaderText="Download Here">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("lib_ref_id") %>'  
                                            CommandName="Download" Text='<%# Eval("application") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>  
                            <asp:BoundField DataField="Report_code" HeaderText="Report Type" />
                            <asp:BoundField DataField="Report_Desc" HeaderText="Report Desc" />
                            <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" />
                            <asp:BoundField DataField="CreateDate" HeaderText="Create Date" />
                        </Columns>
                    </asp:GridView>
       </div> 
    </div>
</asp:Content>
