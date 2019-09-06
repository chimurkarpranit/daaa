//‘-----------------------------------------------------------
//‘ [ID] EmployeeTerritories
//‘ [Name] Pranit Chimurkar
//‘ [Func] EmployeeTerritories Class
//‘ [Date] 2019/08/11 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] EmployeeTerritories
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] EmployeeTerritories table
    //‘ [Date] 2019/08/11 by Pranit
    //‘-----------------------------------------------------------
    public partial class EmployeeTerritories : System.Web.UI.Page
    {
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/11 by Pranit
        //‘-----------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindEmpTerr();
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] BindGridView
        //‘[Name] Pranit Chimurkar
        //‘[Func] Fills the Gridview
        //‘[Date] 2019/08/11 by Pranit
        //‘-----------------------------------------------------------
        protected void BindEmpTerr()
        {
            CommonFunction objComFunc = new CommonFunction();
            StringBuilder strBrQuery;//string builder for query
            string strQuery;
            try
            {
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strEmpTer);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                objComFunc = new CommonFunction();
                gvEmpTer.DataSource = objComFunc.ConnectionGenerate(strQuery);
                gvEmpTer.DataBind();
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
                gvEmpTer.PageIndex = e.NewPageIndex;
                BindEmpTerr();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}