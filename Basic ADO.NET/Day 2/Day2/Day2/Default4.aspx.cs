//‘-----------------------------------------------------------
//‘ [ID] Default4
//‘ [Name] Pranit Chimurkar
//‘ [Func] hyperlink to the Employee Name column
//‘ [Date] 2019/08/11 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] Default4
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] hyperlink to the Employee Name column
    //‘ [Date] 2019/08/11 by Pranit
    //‘-----------------------------------------------------------
    public partial class Default4 : System.Web.UI.Page
    {
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/11 by Pranit
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
        //‘[Date] 2019/08/11 by Pranit
        //‘-----------------------------------------------------------
        protected void BindGridView()  // For Populating the GridView with Database
        {
            CommonFunction objComFunc = new CommonFunction();
            DataTable dsData = new DataTable();
            string strQuery;
            try
            {
                strQuery = objComFunc.CreateSelectQuery("*", "Employees");
                dsData = objComFunc.CommonDataTable(strQuery);
                gvEmployee.DataSource = dsData;
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
        }
        //‘-----------------------------------------------------------
        //‘[ID] OnPageIndexChanging
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Page Indexing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/11 by Pranit
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