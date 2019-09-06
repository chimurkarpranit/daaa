<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTerritories.aspx.cs" Inherits="Day5_8.EmployeeTerritories" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
     <title>Add/Edit/Delete features in the gridview</title>
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
        table
        {
            border: 1px solid #ccc;
            border-collapse: collapse;
            background-color: #fff;
            width:400px;
            align-content:center;
            margin-left: auto;
            margin-right: auto;
        }
        table th
        {
            background-color: #B8DBFD;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border: 1px solid #ccc;
        }
        table, table table td
        {
            border: 0px solid #ccc;
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
           <div class="container Center" style="width:1000px">
               <h2 class="heading display-4">EmployeeTerritories Details</h2>
                <div class="row"  style="width:1000px">
                <div class="col-md-12">
                <asp:GridView ID="gvEmpTer" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" DataKeyNames="EmployeeID,TerritoryID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="80%" CssClass="auto-style1" AllowPaging="True" PageSize="5">  
                    <Columns>
                        <asp:TemplateField HeaderText="EmployeeID">              
                        <ItemTemplate>
                            <asp:Label ID="LblEmployeeID" runat="server" Text='<%# Eval("EmployeeID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TxtEmployeeID" runat="server" Text='<%# Eval("EmployeeID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TerritoryID">
                        <ItemTemplate>
                            <asp:Label ID="LblTerritoryIDD" runat="server" Text='<%# Eval("TerritoryID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TxtTerritoryID" runat="server" Text='<%# Eval("TerritoryID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LBtnUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LBtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LBtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LBtnDelete" runat="server" OnClientClick="return confirm('Are you sure to delete this record?')" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
                </div>
                <div class="row">
                <div class="col-md-12">
  <table border="1" style="border-collapse: collapse; width:1000px;" class="table table-responsive-lg">
    <tr>
        <td>
            EmployeeID:
            <asp:TextBox ID="TxtAddEmployeeID" CssClass="form-control" runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="RfvEmployeeID" ControlToValidate="TxtAddEmployeeID" ErrorMessage="Enter EmployeeID" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="RgxvEmployeeID" ControlToValidate="TxtAddEmployeeID" ValidationExpression="^[0-9]*$" ErrorMessage="Alphabet Not Allowed as EmployeeID" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
        <td>
            TerritoryID:
            <asp:TextBox ID="TxtAddTerritoryID" CssClass="form-control" runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="RfvTerritoryID" ControlToValidate="TxtAddTerritoryID" ErrorMessage="Enter TerritoryID" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="RgxvTerritoryID" ControlToValidate="TxtAddTerritoryID" ValidationExpression="^[0-9]*$" ErrorMessage="Alphabet Not Allowed as TerritoryID" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
        <td >
            <asp:Button ID="BtnAdd" CssClass="form-control btn btn-primary" runat="server" Text="Add" OnClick="Insert" />
        </td>
    </tr>
</table>
</div>
</div>
</div>
</form>
</body>
</html>