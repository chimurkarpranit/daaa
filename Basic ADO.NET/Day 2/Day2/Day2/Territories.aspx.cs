//‘-----------------------------------------------------------
//‘ [ID] Territories Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] Territories
//‘ [Date] 2019/08/13 by Pranit
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
    //‘ [ID] Territories Table
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Used for Territories Table
    //‘ [Date] 2019/08/13 by Pranit
    //‘-----------------------------------------------------------
    public partial class Territories : Page
    {
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/13 by Pranit
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
        //‘[Date] 2019/08/13 by Pranit
        //‘-----------------------------------------------------------
        protected void BindGridView()  // For Populating the GridView with Database
        {
            CommonFunction objComFunc = new CommonFunction();
            StringBuilder strBrQuery;//string builder for query
            string strQuery;
            try
            {
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strTerr);
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                objComFunc = new CommonFunction();
                gvTerritory.DataSource = objComFunc.ConnectionGenerate(strQuery);
                gvTerritory.DataBind();
            }
            catch(MySqlException ex)
            {
                Response.Write(ex.Message);
            }
            catch(Exception ex)
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
        //‘[Date] 2019/08/09 by Pranit
        //‘-----------------------------------------------------------
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)  // For Clicking on Page Number
        {
            try
            {
                gvTerritory.PageIndex = e.NewPageIndex;
                BindGridView();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}