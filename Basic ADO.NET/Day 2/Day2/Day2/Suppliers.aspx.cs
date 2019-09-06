//‘-----------------------------------------------------------
//‘ [ID] Suppliers
//‘ [Name] Pranit Chimurkar
//‘ [Func] Supplier Class
//‘ [Date] 2019/08/13 by Pranit
//‘-----------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] Default
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Displays Suppliers Details
    //‘ [Date] 2019/08/13 by Pranit
    //‘-----------------------------------------------------------
    public partial class Suppliers : Page
    {
        SupplierService ss;
        ArrayList arr;
        string conString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
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
            try
            {
                ss = new SupplierService();
                arr = ss.GetSuppliers();
                gvSupplier.DataSource = arr;
                gvSupplier.DataBind();
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
        //‘[Date] 2019/08/13 by Pranit
        //‘-----------------------------------------------------------
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)  // For Clicking on Page Number
        {
            try
            {
                gvSupplier.PageIndex = e.NewPageIndex;
                BindGridView();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}