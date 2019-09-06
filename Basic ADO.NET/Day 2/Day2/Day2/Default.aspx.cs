//‘-----------------------------------------------------------
//‘ [ID] Default
//‘ [Name] Pranit Chimurkar
//‘ [Func] Employee Class
//‘ [Date] 2019/08/10 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] _Default
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] store the dataset in session
    //‘ [Date] 2019/08/10 by Pranit
    //‘-----------------------------------------------------------
    public partial class _Default : Page
    {
        public string strSqlQuery1 { get; set; }
        public string strSqlQuery2 { get; set; }
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/10 by Pranit
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
        //‘[Date] 2019/08/10 by Pranit
        //‘-----------------------------------------------------------
        protected void BindGridView()  // For Populating the GridView with Database
        {
            CommonFunction objComFunc = new CommonFunction();
            DataSet dsData = new DataSet();
            try
            {
                strSqlQuery1 = objComFunc.CreateSelectQuery("*", "Employees");
                strSqlQuery2 = objComFunc.CreateSelectQuery("*", "territories");
                dsData = objComFunc.CommonDataSet(strSqlQuery1);
                //dsData = objComFunc.CommonDataSet(strSqlQuery2);
                Session["DataSet"] = dsData;
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
        //‘[Date] 2019/08/10 by Pranit
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