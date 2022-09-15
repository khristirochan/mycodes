<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeInfo.aspx.cs" Inherits="Employee_System.EmployeeInfo" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <title>Employee Informations</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="container">
            <div class="row">


                <div class="col-md-4">
                </div>
                <div class="col-md-4">

                    <center>
                        <h3 class="mt-5">EMPLOYEE INFORMATIONS</h3>
                    </center>
                    <div class="mb-3 mt-5">
                        <label class="form-label">Firstname</label>
                        <asp:TextBox ID="EmpFnameTxt" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="v" CssClass="text-danger" ControlToValidate="EmpFnameTxt" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>

                    </div>

                    <div class="mb-3">
                        <label class="form-label">Lastname</label>
                        <asp:TextBox ID="EmpLnameTxt" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>

                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="v" CssClass="text-danger" ControlToValidate="EmpLnameTxt" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email address</label>
                        <asp:TextBox ID="EmpEmailTxt" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="v" CssClass="text-danger" ControlToValidate="EmpEmailTxt" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>

                        <div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" ControlToValidate="EmpEmailTxt" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" CssClass="text-danger">Please Enter Valid Email</asp:RegularExpressionValidator>

                    </div>

                    <div class="mb-3">
                        <label class="form-label">Mobile No</label>
                        <asp:TextBox ID="EmpMobilenoTxt" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>

                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="v" CssClass="text-danger" ControlToValidate="EmpMobilenoTxt" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="v" ControlToValidate="EmpMobilenoTxt" ValidationExpression="^([0-9]{10})" CssClass="text-danger" runat="server">Invalid Mobile No</asp:RegularExpressionValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Gender</label>
                        <asp:RadioButton ID="rbtn_male" CssClass="radio" GroupName="gn" Text="Male" runat="server" />
                        <asp:RadioButton ID="rbtn_female" CssClass="radio" GroupName="gn" Text="Female" runat="server" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Department</label>
                     <asp:DropDownList ID="DepartmentListDDL" runat="server" CssClass="w-100 form-control">
                             <asp:ListItem></asp:ListItem>
                         </asp:DropDownList>       

                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="v" CssClass="text-danger" ControlToValidate="DepartmentListDDL" InitialValue="0" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>

                    </div>

                    <div class="mb-3">
                        <label class="form-label">Profile</label>
                        <%--<input type="file" id="EmpProfile" class="form-control" runat="server" required />--%>
                        
		<asp:FileUpload ID="EmpProfile"  CssClass="form-control"  runat="server"  />
                       
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="v" CssClass="text-danger" ControlToValidate="EmpProfile" ErrorMessage="Required" runat="server"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                            runat="server" ControlToValidate="EmpProfile"
                            ErrorMessage="Only .jpg .png File allowed" CssClass="text-danger"
                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.JPG|.PNG)$"
                            ValidationGroup="v" SetFocusOnError="true"></asp:RegularExpressionValidator>
                        
                        <%--<asp:CustomValidator ID="customValidatorUpload" ValidationGroup="v" runat="server" ErrorMessage="" ControlToValidate="EmpProfile" ClientValidationFunction="setUploadButtonState();" />--%>

                        <asp:Label ID="lbl_uploadMessage" CssClass="text-danger" runat="server" Text="" />

                    </div>
                    <asp:Button ID="btn_sub" runat="server" CssClass="btn btn-success" ValidationGroup="v" Text="Submit" OnClick="btn_sub_Click" />

                    <a href="EmployeeSearch.aspx" class="btn btn-primary">Search</a>
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ValidationGroup="v" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblMessage" CssClass="text-success" Text="" runat="server" Visible="false" />


                </div>
            </div>
            <div class="col-md-4">
            </div>
        </div>

        <div class="row">
            <div class="col-2">
            </div>
            <div class="col-8">
                <div class="row">
                    <div class="col-11">
                         <asp:TextBox ID="searchTxt" OnTextChanged="searchTxt_TextChanged" AutoPostBack="true" CssClass="form-control"  runat="server" ></asp:TextBox>   
                     </div>
                    <div class="col-1">
                         <asp:Button ID="srchBtn" runat="server" CssClass="btn btn-dark" Text="Search" OnClick="srchBtn_Click" />
              
                    </div>
                    

                </div>
                 
                
                <br />
                <asp:GridView ID="empGrid" CssClass="table table-striped table-hover" AutoGenerateColumns="false" runat="server" 
                    OnRowCommand="empGrid_RowCommand" OnRowCancelingEdit="empGrid_RowCancelingEdit" OnRowEditing="empGrid_RowEditing" 
                    OnRowUpdating="empGrid_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Emp_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Profile">
                            <ItemTemplate>
                                <asp:Image ID="Image1" CssClass="zoom" Width="100px" Height="100px" runat="server" ImageUrl='<%#Eval("Emp_Profile") %>' />
                            </ItemTemplate>

                            <EditItemTemplate>
                                
                                <asp:FileUpload ID="fuProfile" runat="server" />
                                <br />
                                <asp:Label ID="lblEditProfile" Text='<%#Eval("Emp_Profile") %>' runat="server" Visible="false" />
                            </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Emp_Firstname") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                    <asp:TextBox ID="txtFName" runat="server" Text='<%#Eval("Emp_Firstname") %>'></asp:TextBox>

                                </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Emp_Lastname") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                    <asp:TextBox ID="txtLName" runat="server" Text='<%#Eval("Emp_Lastname") %>'></asp:TextBox>
                                </EditItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Emp_Email") %>'></asp:Label>
                            </ItemTemplate>

                           <EditItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%#Eval("Emp_Email") %>'></asp:TextBox>
                                </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobile No">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Emp_MobileNo") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                    <asp:TextBox ID="txtMob" runat="server" Text='<%#Eval("Emp_MobileNo") %>'></asp:TextBox>
                                </EditItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Gender">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Emp_Gender") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                    <asp:DropDownList ToolTip='<%#Eval("Emp_Gender")%>' SelectedValue='<%#Eval("Emp_Gender") %>' ID="ddl_gender" runat="server">
                                        <asp:ListItem Text="Male"></asp:ListItem>
                                        <asp:ListItem Text="Female"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Department" ControlStyle-CssClass="w-100">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                            </ItemTemplate>

                          <EditItemTemplate>
                                   
                              <asp:DropDownList ID="ddl_department" runat="server" CssClass="form-control">
                             <asp:ListItem></asp:ListItem>

                         </asp:DropDownList>  
                                <asp:Label ID="lblDepartment" Text='<%#Eval("Department") %>' runat="server" Visible="false" />
                                 </EditItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Delete" ControlStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Button ID="gv_btn_del" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="dlt" CommandArgument='<%#Eval("Emp_Id") %>' />
                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField HeaderText="Edit" ControlStyle-CssClass="btn btn-info " ControlStyle-Width="70px" ShowEditButton="true" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-2">
            </div>

        </div>
    </form>
    <!---->

    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>

