<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpSample.aspx.cs" Inherits="Day2.EmpSample" %>

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
        .Center
        {
            margin: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            transform: translate(-50%, -50%);
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
            <h1 class="display-4" style="text-align:center">Employee Details</h1>
                <div class="row"  style="width:1000px">
                <div class="col-md-12">
                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"　HorizontalAlign="Center" AllowPaging="true" PageSize="10" DataKeyNames="EmployeeID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="1000px" CssClass="auto-style1">  
                    <Columns>
                         <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" />
                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderText="LastName" />
                        <asp:BoundField DataField="City" HeaderText="City" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" />
                         <asp:BoundField DataField="Notes" HeaderText="Notes" />
                        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true"/>
                    </Columns>
                </asp:GridView>
                </div>
                </div>
                <div class="row">
                <div class="col-md-12">
    <table border="1" style="border-collapse: collapse; width:1000px;" class="table table-responsive-md">
    <tr>
        <td>
            FirstName:<br />
            <asp:TextBox CssClass="form-control" ID="textFirstName" runat="server" />
        </td>
        <td>
            LastName:<br />
            <asp:TextBox CssClass="form-control" ID="textLastName" runat="server" />
        </td>
         <td>
            City:<br />
            <asp:TextBox CssClass="form-control" ID="textCity" runat="server" />
        </td>
         <td>
            Salary:<br />
            <asp:TextBox CssClass="form-control" ID="textSalary" runat="server" />
        </td>
        <td>
            Notes:<br />
            <asp:TextBox CssClass="form-control" ID="textNotes" runat="server" />
        </td>      
        <td style="text-align:center; vertical-align:central">
            <asp:Button ID="BtnAdd" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="Insert" />
        </td>
    </tr>
</table>
</div>
</div>
</div>
</form>
</body>
</html>