//‘-----------------------------------------------------------
//‘ [ID] Territories Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] Territories Table
//‘ [Date] 2019/08/19 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day5_8
{
    //‘-----------------------------------------------------------
    //‘ [ID] Territories Table
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Territories Table
    //‘ [Date] 2019/08/19 by Pranit
    //‘-----------------------------------------------------------
    public partial class Sample : System.Web.UI.Page
    {
        AllMessage objMsg = new AllMessage();
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gvTerritory.Visible = false;
                BtnDeleteRecord.Visible = false;
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] ViewButtonClick
        //‘[Name] Pranit Chimurkar
        //‘[Func] OnCLick of ViewButton
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void ViewButtonClick(object sender, EventArgs e)
        {
            try
            {
                gvTerritory.Visible = true;
                BtnDeleteRecord.Visible = true;
                if (Page.IsPostBack)
                {
                    BindGrid();
                }
                //Adding an Attribute to Server Control(i.e. ButtonDeleteRecord)
                BtnDeleteRecord.Attributes.Add("onclick", "javascript:return DeleteConfirm()");
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] BindGridView
        //‘[Name] Pranit Chimurkar
        //‘[Func] Fills the Gridview
        //‘[Date] 2019/08/19 by Pranit
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
                strBrQuery = new StringBuilder(objComFunc.strSelAll + objComFunc.strTerritories);
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
                gvTerritory.DataSource = dtEmpTer;
                gvTerritory.DataBind();
                gvTerritory.Visible = true;
                BtnDeleteRecord.Visible = true;
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
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvTerritory.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
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
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvTerritory.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] ButtonDeleteRecord_Click
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Deleting
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void ButtonDeleteRecord_Click(object sender, EventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdDel;
            string strQuery;
            try
            {
                foreach (GridViewRow grow in gvTerritory.Rows)
                {
                    //Searching CheckBox("CheckBoxDelete") in an individual row of Grid
                    CheckBox checkdel = (CheckBox)grow.FindControl("CheckBoxDelete");
                    //If CheckBox is checked than delete the record with particular empid and terid
                    if (checkdel.Checked)
                    {
                        int terID = Convert.ToInt32(gvTerritory.DataKeys[grow.RowIndex].Values["TerritoryID"]);
                        //Delete query in String builder
                        strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strDelete + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strTerritoryID + terID + objComFunc.strSemiColon + objComFunc.strDelete +  objComFunc.strTerritories + objComFunc.strWhere + objComFunc.strTerritoryID + terID + objComFunc.strSemiColon);
                        //convertion from string builder to string
                        strQuery = strBrQuery.ToString();
                        cmdDel = objComFunc.CommonExecute(strQuery);                        
                    }
                }                
                ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Record is deleted successfully');", true);
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
            finally
            {
                objComFunc = null;
                cmdDel = null;
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowUpdating
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Updating
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdUpdate;
            string strQuery;
            int terIDUpdate;
            object TerrUpID; //Object needed to fetch the current values of gridview rows
            TerrUpID = gvTerritory.DataKeys[e.RowIndex].Values["TerritoryID"];
            GridViewRow row = gvTerritory.Rows[e.RowIndex];
            string ter = ((TextBox)row.FindControl("TxtGridTerritoryID")).Text;
            string terDesc = ((TextBox)row.FindControl("TxtGridTerritoryDescription")).Text;
            string terRegion = ((TextBox)row.FindControl("TxtGridRegionID")).Text;
            try
            {
                //Check if TerritoryID Present in in EmployeeTerritories Table
                StringBuilder strBrTerCheck = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strTerritories + objComFunc.strWhere + objComFunc.strTerritoryID + ter);
                terIDUpdate = objComFunc.CommonVerify(strBrTerCheck);
                if (terIDUpdate > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('TerritoryID already exists in Territories Table');", true);
                }
                else
                {
                    gvTerritory.EditIndex = -1;
                    //Update query in String builder
                    strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strUpdate + objComFunc.strTerritories + objComFunc.strSet + objComFunc.strTerritoryID + "'"+ter+ "'"+ objComFunc.strComma + objComFunc.strTerrDesc + "'" + terDesc + "'" + objComFunc.strComma + objComFunc.strRegID + "'" + terRegion + "'" + objComFunc.strWhere + objComFunc.strTerritoryID + TerrUpID);
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
                TerrUpID = null;
                objComFunc = null;
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowEditing
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Editing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvTerritory.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] AddButtonClick
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Adding Data
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void AddButtonClick(object sender, EventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdInsert;
            string strQuery;
            int terIDvalue;
            int empTerIDvalue;
            try
            {
                //Check if TerritoryID Present in in Territory Table
                StringBuilder strBrTerr = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strTerritories + objComFunc.strWhere + objComFunc.strTerritoryID + TxtTerritoryID.Text);
                terIDvalue = objComFunc.CommonVerify(strBrTerr);
                //Check if TerritoryID for corresponding EmployeeID are present in Employeeterritories Table
                StringBuilder strBrEmpTerr = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strEmployeeID + TxtEmployeeID.Text + objComFunc.strAND + objComFunc.strTerritoryID + TxtTerritoryID.Text);
                empTerIDvalue = objComFunc.CommonVerify(strBrEmpTerr);
                if (terIDvalue > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('TerritoryID already exists in Territories Table');", true);
                }
                else if (empTerIDvalue > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('EmployeeID and TerritoryID already exists in EmployeeTerritories table');", true);
                }
                else
                {
                    //insert query in String builder
                    strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strInsert + objComFunc.strEmployeeTerriTories + objComFunc.strOpenPar + objComFunc.strEmpTerColumns + objComFunc.strClosePar + objComFunc.strValues + TxtEmployeeID.Text + objComFunc.strComma + TxtTerritoryID.Text + objComFunc.strClosePar + objComFunc.strSemiColon);
                    strBrQuery.Append(objComFunc.strInsert + objComFunc.strTerritories + objComFunc.strColForTer + objComFunc.strValues + "'" + TxtTerritoryID.Text + "','" + TxtTerritoryDescription.Text + "','" + TxtRegionID.Text + "'" + objComFunc.strClosePar + objComFunc.strSemiColon);
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
                cmdInsert = null;
                objComFunc = null;
            }
        }        
    }
}