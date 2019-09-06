<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sample.aspx.cs" Inherits="Day5_8.Sample" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment_7</title>
        <script type="text/javascript">
        function DeleteConfirm()
        {
            var Ans = confirm("Do you want to Delete Selected Record?");
            if (Ans)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        </script>
        <style type="text/css">
            .heading
            {
                display: flex;
                flex-direction: column;
                justify-content: center;
                text-align: center;
            }
            td
            {
                text-align:center;
            }
            table
            {
                width:400px;
                margin-left: auto;
                margin-right: auto;
            }
            .td1
            {
                text-align:left;
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
                width: 611px;
            }
            .auto-style3 {
                width: 105px;
                height: 73px;
            }
            .auto-style4 {
                height: 73px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; text-align:center">
        <h2 class="heading">Add Territory Record for the New Employee Record</h2>
    <table class="auto-style2">
        <tr><td class="auto-style1 td1"><asp:Label ID="LblEmployeeID" runat="server" Text="EmployeeID"></asp:Label></td>
            <td>
                <asp:TextBox ID="TxtEmployeeID" runat="server" MaxLength="6" Width="179px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RgxvEmployeeID" runat="server" ControlToValidate="TxtEmployeeID" ErrorMessage="Invalid EmployeeID" ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RfvEmployeeID" runat="server" ErrorMessage="Enter EmployeeID" ControlToValidate="TxtEmployeeID" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td class="auto-style3 td1"><asp:Label ID="LblTerritoryID" runat="server" Text="TerritoryID"></asp:Label></td>
            <td class="auto-style4">
                <asp:TextBox ID="TxtTerritoryID" runat="server" MaxLength="6" Width="179px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RgxvTerritoryID" runat="server" ControlToValidate="TxtTerritoryID" ErrorMessage="Invalid TerritoryID" ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RfvTerritoryID" runat="server" ErrorMessage="Enter TerritoryID" ControlToValidate="TxtTerritoryID" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td class="auto-style1 td1"><asp:Label ID="LblTerritoryDescription" runat="server" Text="TerritoryDescription"></asp:Label></td>
            <td>
                <asp:TextBox ID="TxtTerritoryDescription" runat="server" Width="179px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RgxvTerDesc" runat="server" ControlToValidate="TxtTerritoryDescription" ErrorMessage="Invalid TerritoryDescription" ForeColor="Red" ValidationExpression="^[a-zA-Z ]*$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RfvTerDesc" runat="server" ErrorMessage="Enter TerritoryDescription" ControlToValidate="TxtTerritoryDescription" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td class="auto-style1 td1"><asp:Label ID="LblRegionID" runat="server" Text="RegionID"></asp:Label></td>
            <td>
                <asp:TextBox ID="TxtRegionID" runat="server" MaxLength="1" Width="179px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RgxvRegionID" runat="server" ControlToValidate="TxtRegionID" ErrorMessage="Invalid RegionID" ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RfvRegionID" runat="server" ErrorMessage="Enter RegionID" ControlToValidate="TxtRegionID" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th colspan="2"><asp:Button ID="BtnAdd" runat="server" Text="Add" onclick="AddButtonClick" Width="178px" Height="41px" /></th>
        </tr>
        <tr>
            <th colspan="2"><asp:Button ID="BtnView" runat="server" Text="View Territory Record" onclick="ViewButtonClick" CausesValidation="false" Width="178px" Height="41px" /></th>
        </tr>
    </table>
    </div>
        <div style="width: 100%; text-align:center">
                    <%--Gridview for Territories Table--%>
                    <asp:GridView ID="gvTerritory" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" DataKeyNames="TerritoryID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="80%" CssClass="auto-style1" AllowPaging="True" PageSize="5">  
                    <Columns>
                        <asp:TemplateField HeaderText="TerritoryID">              
                            <ItemTemplate>
                                <asp:Label ID="LblGridTerritoryID" runat="server" Text='<%# Eval("TerritoryID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtGridTerritoryID" runat="server" Text='<%# Eval("TerritoryID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TerritoryDescription">
                            <ItemTemplate>
                                <asp:Label ID="LblGridTerritoryDescription" runat="server" Text='<%# Eval("TerritoryDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtGridTerritoryDescription" runat="server" Text='<%# Eval("TerritoryDescription") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RegionID">
                            <ItemTemplate>
                                <asp:Label ID="LblGridRegionID" runat="server" Text='<%# Eval("RegionID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtGridRegionID" runat="server" Text='<%# Eval("RegionID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LBtnUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LBtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LBtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="CheckBoxDelHeader" Text="Delete" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="CheckBoxDelete"/>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
            </div>
        <br /><br />
        <div style="width: 100%; text-align:center">
            <asp:Button ID="BtnDeleteRecord" runat="server" CausesValidation="false" Text="Delete" OnClick="ButtonDeleteRecord_Click" />
        </div>
    </form>
</body>
</html>