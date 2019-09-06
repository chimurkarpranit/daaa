//‘-----------------------------------------------------------
//‘ [ID] EmployeeTerritories Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] EmployeeTerritories Table
//‘ [Date] 2019/08/16 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace Day5_8
{
    //‘-----------------------------------------------------------
    //‘ [ID] EmployeeTerritories Table
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] EmployeeTerritories Table
    //‘ [Date] 2019/08/16 by Pranit
    //‘-----------------------------------------------------------
    public partial class EmployeeTerritories : System.Web.UI.Page
    {
        AllMessage objMsg = new AllMessage();
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] BindGridView
        //‘[Name] Pranit Chimurkar
        //‘[Func] Fills the Gridview
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void BindGrid()
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            DataTable dtEmpTer;
            DataSet dsEmpTer;
            string strQuery;
            try
            {
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strSelAll + objComFunc.strEmployeeTerriTories);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                //DataTable
                dtEmpTer = new DataTable();
                //DataSet
                dsEmpTer = new DataSet();
                dtEmpTer = objComFunc.ConnectionGenerate(strBrQuery.ToString());
                //Fill the DataSet
                dsEmpTer.Tables.Add(dtEmpTer);
                //Store the Grid View
                gvEmpTer.DataSource = dtEmpTer;
                gvEmpTer.DataBind();
            }
            catch (MySqlException ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnPageIndexChanging
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Page Indexing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEmpTer.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowCancelingEdit
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Cancelling Edit
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvEmpTer.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowDeleting
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Deleting
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        object EmpDelID, TerrDelID;
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdDel;
            string strQuery;
            try
            {
                EmpDelID = Convert.ToInt32(gvEmpTer.DataKeys[e.RowIndex].Values["EmployeeID"]);
                TerrDelID = Convert.ToInt32(gvEmpTer.DataKeys[e.RowIndex].Values["TerritoryID"]);
                //Delete query in String builder
                strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strDelete + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strEmployeeID + EmpDelID + objComFunc.strAND + objComFunc.strTerritoryID + TerrDelID);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                cmdDel = objComFunc.CommonExecute(strQuery);                        
                ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Record is deleted successfully');", true);
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
            finally
            {
                EmpDelID = null;
                TerrDelID = null;
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowUpdating
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Updating
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdUpdate;
            string strQuery;
            object EmpUpID, TerrUpID; //Objects needed to fetch the current values of gridview rows
            int bothIDValue;
            EmpUpID = gvEmpTer.DataKeys[e.RowIndex].Values["EmployeeID"];
            TerrUpID = gvEmpTer.DataKeys[e.RowIndex].Values["TerritoryID"];
            GridViewRow row = gvEmpTer.Rows[e.RowIndex];
            string em = ((TextBox)row.FindControl("TxtEmployeeID")).Text;
            string ter = ((TextBox)row.FindControl("TxtTerritoryID")).Text;
            try
            {
                //Checking if both ids exists or not in employeeterritories Table
                StringBuilder strBrBoth = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT +  objComFunc.strFrom + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strEmployeeID + em + objComFunc.strAND + objComFunc.strTerritoryID + ter);
                bothIDValue = objComFunc.CommonVerify(strBrBoth);
                if (bothIDValue > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('EmployeeID and TerritoryID already exists in EmployeeTerritories Table');", true);
                }
                else
                {
                    gvEmpTer.EditIndex = -1;
                    //Update query in String builder
                    strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strUpdate + objComFunc.strEmployeeTerriTories + objComFunc.strSet + objComFunc.strEmployeeID + em +objComFunc.strComma + objComFunc.strTerritoryID + ter + objComFunc.strWhere + objComFunc.strEmployeeID + EmpUpID + objComFunc.strAND + objComFunc.strTerritoryID + TerrUpID);
                    //convertion from string builder to string
                    strQuery = strBrQuery.ToString();
                    cmdUpdate = objComFunc.CommonExecute(strQuery);
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
            finally
            {
                EmpUpID = null;
                TerrUpID = null;
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowEditing
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Editing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvEmpTer.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] Insert
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Adding Data
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void Insert(object sender, EventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdInsert;
            string strQuery;
            int bothExistsAdd;
            string EmployeeIDAdd = TxtAddEmployeeID.Text;
            string TerritoryIDAdd = TxtAddTerritoryID.Text;
            TxtAddEmployeeID.Text = "";
            TxtAddTerritoryID.Text = "";
            try
            {
                //Checking if both ids exists or not in employeeterritories Table
                StringBuilder strBrBothAdd = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strEmployeeID + EmployeeIDAdd + objComFunc.strAND + objComFunc.strTerritoryID + TerritoryIDAdd);
                bothExistsAdd = objComFunc.CommonVerify(strBrBothAdd);
                if (bothExistsAdd > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('EmployeeID and TerritoryID already exists in EmployeeTerritories Table');", true);
                }
                else
                {
                    //insert query in String builder
                    strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strInsert + objComFunc.strEmployeeTerriTories + objComFunc.strOpenPar + objComFunc.strEmpTerColumns + objComFunc.strClosePar + objComFunc.strValues + EmployeeIDAdd + objComFunc.strComma + TerritoryIDAdd + objComFunc.strClosePar);
                    //convertion from string builder to string
                    strQuery = strBrQuery.ToString();
                    cmdInsert = objComFunc.CommonExecute(strQuery);
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
            finally
            {
                objComFunc = null;
                cmdInsert = null;
            }
        }
    }
}