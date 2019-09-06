//‘-----------------------------------------------------------
//‘ [ID] EmployeeTerritories4
//‘ [Name] Pranit Chimurkar
//‘ [Func] EmployeeTerritories4 Class
//‘ [Date] 2019/08/12 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] EmployeeTerritories4
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Display EmployeeTerritories Table on hyperlink
    //‘ [Date] 2019/08/12 by Pranit
    //‘-----------------------------------------------------------
    public partial class EmployeeTerritories4 : Page
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
            if (!Page.IsPostBack)
            {
                BindEmpTerr();
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] BindGridView
        //‘[Name] Pranit Chimurkar
        //‘[Func] Fills the Gridview
        //‘[Date] 2019/08/12 by Pranit
        //‘-----------------------------------------------------------
        protected void BindEmpTerr()
        {
            CommonFunction objComFunc = new CommonFunction();
            StringBuilder strBrQuery;//string builder for query
            DataTable dtEmpTer;
            DataSet dsEmpTer;
            string strQuery;
            try
            {
                string var = Request.QueryString["EmployeeID"];
                int id = Convert.ToInt32(var);
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strSelAll + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strEmployeeID + id);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                //CommonFunction Used
                objComFunc = new CommonFunction();
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
        //‘[Date] 2019/08/12 by Pranit
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