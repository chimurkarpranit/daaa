//‘-----------------------------------------------------------
//‘ [ID] Default
//‘ [Name] Pranit Chimurkar
//‘ [Func] Employee Class
//‘ [Date] 2019/08/09 by Pranit
//‘-----------------------------------------------------------

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day1
{
    //‘-----------------------------------------------------------
    //‘ [ID] Default
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Displays Employees Details
    //‘ [Date] 2019/08/09 by Pranit
    //‘-----------------------------------------------------------
    public partial class Default : Page
    {
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/09 by Pranit
        //‘-----------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e) //Page Load Method
        {
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] BindGridView
        //‘[Name] Pranit Chimurkar
        //‘[Func] Fills the Gridview
        //‘[Date] 2019/08/09 by Pranit
        //‘-----------------------------------------------------------
        protected void BindGridView()  // For Populating the GridView with Database
        {
            CommonFunction objComFunc = new CommonFunction();
            DataTable dtEmp = new DataTable();
            string strQuery;
            try
            {
                strQuery = objComFunc.CreateSelectQuery("*", "Employees");
                dtEmp = objComFunc.CommonDataTable(strQuery);
                gvEmployee.DataSource = dtEmp;
                gvEmployee.DataBind();
            }
            catch (MySqlException ex)
            {
                Response.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                strQuery = null;
                objComFunc = null;
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnPageIndexChanging
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Page Indexing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/09 by Pranit
        //‘-----------------------------------------------------------
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)  // For Clicking on Page Number
        {
            try
            {
                gvEmployee.PageIndex = e.NewPageIndex;
                BindGridView();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}