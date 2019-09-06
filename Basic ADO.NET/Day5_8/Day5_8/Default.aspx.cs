//‘-----------------------------------------------------------
//‘ [ID] Employees Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] Employees Table
//‘ [Date] 2019/08/16 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day5_8
{
    //‘-----------------------------------------------------------
    //‘ [ID] Employees Table
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Employees Table
    //‘ [Date] 2019/08/16 by Pranit
    //‘-----------------------------------------------------------
    public partial class _Default : Page
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
        protected void BindGrid() // For Populating the GridView with Database
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            DataTable dtEmpTer;
            DataSet dsEmpTer;
            string strQuery;
            try
            {
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strSel + objComFunc.strEmpColumns + objComFunc.strFrom + objComFunc.strEmployees);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                //CommonFunction Used
                objComFunc = new CommonFunctions();
                //DataTable
                dtEmpTer = new DataTable();
                //DataSet
                dsEmpTer = new DataSet();
                dtEmpTer = objComFunc.ConnectionGenerate(strBrQuery.ToString());
                //Fill the DataSet
                dsEmpTer.Tables.Add(dtEmpTer);
                //Store the Grid View
                gvEmployee.DataSource = dtEmpTer;
                gvEmployee.DataBind();
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
                gvEmployee.PageIndex = e.NewPageIndex;
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
                gvEmployee.EditIndex = -1;
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
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdDel;
            string strQuery;
            try
            {
                GridViewRow row = gvEmployee.Rows[e.RowIndex];
                //Delete query in String builder
                strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strDelete + objComFunc.strEmployees + objComFunc.strWhere + objComFunc.strEmployeeID + Convert.ToInt32(gvEmployee.DataKeys[e.RowIndex].Value.ToString()));
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                cmdDel = objComFunc.CommonExecute(strQuery);
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
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
            try
            {
                int EmpID = Convert.ToInt32(gvEmployee.DataKeys[e.RowIndex].Value.ToString());
                GridViewRow row = gvEmployee.Rows[e.RowIndex];
                TextBox textFirstName = (TextBox)row.Cells[1].Controls[0];
                TextBox textLastName = (TextBox)row.Cells[2].Controls[0];
                TextBox textCity = (TextBox)row.Cells[3].Controls[0];
                TextBox textsalary = (TextBox)row.Cells[4].Controls[0];
                TextBox textnotes = (TextBox)row.Cells[5].Controls[0];
                gvEmployee.EditIndex = -1;
                //Update query in String builder
                strBrQuery = new StringBuilder(objComFunc.strUpdate + objComFunc.strEmployees + objComFunc.strSet + objComFunc.strFirstName + textFirstName.Text + objComFunc.strLastName + textLastName.Text + objComFunc.strCity + textCity.Text + objComFunc.strSalary + textsalary.Text + objComFunc.strNotes + textnotes.Text + objComFunc.strSingleQuote + objComFunc.strWhere + objComFunc.strEmployeeID + objComFunc.strSingleQuote + EmpID + objComFunc.strSingleQuote);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                cmdUpdate = objComFunc.CommonExecute(strQuery);
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
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
                gvEmployee.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] LBtnAddPage_Click
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Editing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void LBtnAddPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Form.aspx");
        }
    }
}