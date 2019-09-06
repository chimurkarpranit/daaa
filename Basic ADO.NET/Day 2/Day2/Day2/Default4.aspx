<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default4.aspx.cs" Inherits="Day2.Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default4</title>
    <style type="text/css">
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
</head>
<body>
    <div class="Center">
    <form id="form1" runat="server">
        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" Height="439px" style="margin-bottom: 61px" Width="986px">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
        <Columns>            
        <asp:BoundField ItemStyle-Width="150px" DataField="EmployeeID" HeaderText="Employee ID" />
        <asp:HyperLinkField DataTextField="FirstName" DataNavigateUrlFields="EmployeeID" DataNavigateUrlFormatString="~/EmployeeTerritories4.aspx?EmployeeID={0}" HeaderText="FirstName" ItemStyle-Width = "150" />
        <asp:BoundField ItemStyle-Width="150px" DataField="LastName" HeaderText="LastName" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Title" HeaderText="Title" />
        <asp:BoundField ItemStyle-Width="150px" DataField="TitleOfCourtesy" HeaderText="TitleOfCourtesy" />
        <asp:BoundField ItemStyle-Width="150px" DataField="BirthDate" HeaderText="BirthDate" />
        <asp:BoundField ItemStyle-Width="150px" DataField="HireDate" HeaderText="HireDate" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Address" HeaderText="Address" />
        <asp:BoundField ItemStyle-Width="150px" DataField="City" HeaderText="City" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Region" HeaderText="Region" />
        <asp:BoundField ItemStyle-Width="150px" DataField="PostalCode" HeaderText="PostalCode" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Country" HeaderText="Country" />
        <asp:BoundField ItemStyle-Width="150px" DataField="HomePhone" HeaderText="HomePhone" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Extension" HeaderText="Extension" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Photo" HeaderText="Photo" />
    </Columns>
   </asp:GridView>
    </form>
    </div>
</body>
</html>
