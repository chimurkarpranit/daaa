<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Form.aspx.cs" Inherits="Day5_8.Add_Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Form</title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .heading
        {
            display: flex;
            flex-direction: column;
            justify-content: center;
            text-align: center;
        }
        .Center
        {
            margin: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            transform: translate(-50%, -50%);
        }
    </style>
        <meta charset="utf-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css"/>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"/>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</head>
<body>    
    <form id="form1" runat="server">
        <asp:LinkButton ID="LBtnEmpDetails" runat="server" CausesValidation="false" OnClick="LBtnEmpDetails_Click">Go to Employee Details Page</asp:LinkButton>
        <h2 class="heading display-4">Employee Registration</h2>
        <div class="shadow-lg p-2 mb-5 bg-white Center" style="width:500px">
            <table style="border-collapse: collapse; width:500px" class="table table-responsive-lg table-borderless">
                <tr>
                    <td>
                        <asp:Label ID="LblFirstName" runat="server" Text="FirstName:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtFirstName" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="RfvFirstName" ControlToValidate="TxtFirstName" ErrorMessage="Enter FirstName" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RgxvFirstName" ControlToValidate="TxtFirstName" ValidationExpression="^[^0-9]+$" ErrorMessage="Digit Not Allowed as FirstName" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblLastname" runat="server" Text="LastName:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtLastName" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="RfvLastName" ControlToValidate="TxtLastName" ErrorMessage="Enter LastName" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RgxvLastName" ControlToValidate="TxtLastName" ValidationExpression="^[^0-9]+$" ErrorMessage="Digit Not Allowed as LastName" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                     <td>
                        <asp:Label ID="LblCity" runat="server" Text="City:"></asp:Label>
                     </td>
                     <td>
                        <asp:TextBox ID="TxtCity" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="RfvCity" ControlToValidate="TxtCity" ErrorMessage="Enter City" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RgxvCity" ControlToValidate="TxtCity" ValidationExpression="^[^0-9]+$" ErrorMessage="Digit Not Allowed as City" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                     <td>
                        <asp:Label ID="LblSalary" runat="server" Text="Salary:"></asp:Label>
                     </td>
                     <td>
                        <asp:TextBox ID="TxtSalary" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="RfvSalary" ControlToValidate="TxtSalary" ErrorMessage="Enter Salary" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="RgxvSalary" ControlToValidate="TxtSalary" ValidationExpression="^[0-9]*$" ErrorMessage="Only Integers are allowed" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                     </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblNotes" CssClass="col-form-label" runat="server" Text="Notes:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNotes" CssClass="form-control" runat="server" />
                         <asp:RequiredFieldValidator runat="server" ID="RfvNotes" ControlToValidate="TxtNotes" ErrorMessage="Enter Notes" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button ID="BtnAdd" runat="server" CssClass="btn btn-primary form-control" Text="Add" OnClick="Insert" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
