//‘-----------------------------------------------------------
//‘ [ID] Add, Update, Delete for Employees Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] Add, Update, Delete for Employees Table
//‘ [Date] 2019/08/12 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] EmpSample
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Add, Update, Delete for Employees Table
    //‘ [Date] 2019/08/12 by Pranit
    //‘-----------------------------------------------------------
    public partial class EmpSample : System.Web.UI.Page
    {
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/12 by Pranit
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
        //‘[Date] 2019/08/12 by Pranit
        //‘-----------------------------------------------------------
        protected void BindGrid()
        {
            CommonFunction objComFunc = new CommonFunction();
            DataSet dsEmpTer = new DataSet();
            string strQuery;
            try
            {
                strQuery = objComFunc.CreateSelectQuery("*", "Employees");
                dsEmpTer = objComFunc.CommonDataSet(strQuery);
                gvEmployee.DataSource = dsEmpTer;
                gvEmployee.DataBind();
            }
            catch(MySqlException ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
            catch(Exception ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnPageIndexChanging
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Page Indexing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/12 by Pranit
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
                Response.Write(AllMessage.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowCancelingEdit
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Cancelling Edit
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/12 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvEmployee.EditIndex = -1;
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowDeleting
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Deleting
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/12 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CommonFunction objComFunc = new CommonFunction();
            MySqlCommand cmdDel;
            string strDelQuery;
            try
            {
                //Delete query in String
                strDelQuery = objComFunc.DeleteWithoutConstraint("Employees", "EmployeeID", Convert.ToInt32(gvEmployee.DataKeys[e.RowIndex].Value.ToString()));
                cmdDel = objComFunc.CommonExecuteQuery(strDelQuery);
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
        }
            //‘-----------------------------------------------------------
            //‘[ID] OnRowUpdating
            //‘[Name] Pranit Chimurkar
            //‘[Func] For Updating
            //‘[Param]  <1：Sender>
            //‘         <2：e>
            //‘[Date] 2019/08/12 by Pranit
            //‘-----------------------------------------------------------
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CommonFunction objComFunc = new CommonFunction();
            MySqlCommand cmdUpdate;
            string strUpdateQuery;
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
                strUpdateQuery = objComFunc.GetUpdateQuery("Employees", "FirstName, )
                strBrQuery = new StringBuilder(objComFunc.strUpdate + objComFunc.strEmployees + objComFunc.strSet + objComFunc.strFirstName + textFirstName.Text + objComFunc.strLastName + textLastName.Text + objComFunc.strCity + textCity.Text + objComFunc.strSalary + textsalary.Text + objComFunc.strNotes + textnotes.Text + objComFunc.strSingleQuote + objComFunc.strWhere + objComFunc.strEmployeeID + objComFunc.strSingleQuote + EmpID + objComFunc.strSingleQuote);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                cmdUpdate = objComFunc.CommonExecute(strQuery);
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnRowEditing
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Editing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/12 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvEmployee.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
            
        }
        //‘-----------------------------------------------------------
        //‘[ID] Insert
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Adding Data
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/12 by Pranit
        //‘-----------------------------------------------------------
        protected void Insert(object sender, EventArgs e)
        {
            CommonFunction objComFunc = new CommonFunction();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdInsert;            
            string strQuery;
            try
            {
                string FirstName = textFirstName.Text;
                string LastName = textLastName.Text;
                string City = textCity.Text;
                string Salary = textSalary.Text;
                string Notes = textNotes.Text;
                textFirstName.Text = "";
                textLastName.Text = "";
                textCity.Text = "";
                textSalary.Text = "";
                textNotes.Text = "";
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strInsert + objComFunc.strEmployees + objComFunc.strColForEmp + objComFunc.strValues + "'" + FirstName + "'" + "," + "'" + LastName + "'" + "," + "'" + City + "'" + "," + Salary + "," + "'" + Notes + "'" + ")");
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                cmdInsert = objComFunc.CommonExecute(strQuery);
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(AllMessage.strException + ex.Message);
            }
        }
    }
}