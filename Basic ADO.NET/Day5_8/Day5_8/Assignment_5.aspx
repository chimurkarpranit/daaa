<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assignment_5.aspx.cs" Inherits="Day5_8.Assignment_5" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment_5</title>
        <style type="text/css">
            .heading
            {
                display: flex;
                flex-direction: column;
                justify-content: center;
                text-align: center;
            }
            table
            {
                width:400px;
                margin-left: auto;
                margin-right: auto;
            }
            table th
            {
                font-weight: bold;
            }
            .auto-style1 {
                width: 105px;
            }
            .auto-style2 {
                height: 202px;
                width: 302px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2 class="heading">Add Territory Record for the Employee Record</h2>
    <table class="auto-style2">
        <tr>
            <td class="auto-style1"><asp:Label ID="LblEmployeeID" runat="server" Text="EmployeeID"></asp:Label></td>
            <td><asp:DropDownList ID="DdlEmpID" runat="server" Height="17px" Width="183px"></asp:DropDownList></td>
        </tr>
        <tr><td class="auto-style1"><asp:Label ID="LblTerritoryID" runat="server" Text="TerritoryID"></asp:Label></td>
            <td>
                <asp:TextBox ID="TxtTerritoryID" runat="server" MaxLength="6" Width="179px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RfvTerritoryID" runat="server" ControlToValidate="TxtTerritoryID" ErrorMessage="Invalid TerritoryID" ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator><br />
                <asp:RequiredFieldValidator ID="RgxvTerritoryID" runat="server" ErrorMessage="Enter TerritoryID" ControlToValidate="TxtTerritoryID" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th colspan="2"><asp:Button ID="BtnAdd" runat="server" Text="Add" onclick="AddButtonClick" Width="178px" Height="41px" /></th>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>