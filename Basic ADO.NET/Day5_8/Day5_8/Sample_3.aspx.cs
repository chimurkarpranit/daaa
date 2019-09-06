//‘-----------------------------------------------------------
//‘ [ID] Customer Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] Customer Table
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
    //‘ [ID] Customer Table
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Customer Table
    //‘ [Date] 2019/08/16 by Pranit
    //‘-----------------------------------------------------------
    public partial class Sample_3 : Page
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
                strBrQuery = new StringBuilder(objComFunc.strSel + objComFunc.strCuColumns + objComFunc.strFrom + objComFunc.strCustomers);
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
                gvCustomer.DataSource = dtEmpTer;
                gvCustomer.DataBind();
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
                gvCustomer.PageIndex = e.NewPageIndex;
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
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvCustomer.EditIndex = -1;
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
                //Delete query in String builder
                strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strDelete + objComFunc.strCustomers + objComFunc.strWhere + objComFunc.strCustomerID +"'"+ gvCustomer.DataKeys[e.RowIndex].Value.ToString() + "'" + objComFunc.strSemiColon);
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
                string CusID = (gvCustomer.DataKeys[e.RowIndex].Value.ToString());
                GridViewRow row = gvCustomer.Rows[e.RowIndex];
                TextBox textCustomerID = (TextBox)row.Cells[0].Controls[0];
                TextBox textCompanyName = (TextBox)row.Cells[1].Controls[0];
                TextBox textCity = (TextBox)row.Cells[2].Controls[0];
                TextBox textCountry = (TextBox)row.Cells[3].Controls[0];
                gvCustomer.EditIndex = -1;
                //Update query in String builder
                strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strUpdate + objComFunc.strCustomers + objComFunc.strSet + objComFunc.strCustomerID + "'" + textCustomerID.Text + objComFunc.strCity + textCity.Text + objComFunc.strCountry + textCountry.Text + "'" + objComFunc.strWhere + objComFunc.strCustomerID + "'" + CusID +"'"+ objComFunc.strSemiColon);
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
                gvCustomer.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] Insert
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Adding Data
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void Insert(object sender, EventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdInsert;
            string strQuery;
            string CustomerID = TxtCustID.Text;
            string CompanyName = TxtCompanyName.Text;
            string City = TxtCity.Text;
            string Country = TxtCountry.Text;
            TxtCustID.Text = "";
            TxtCompanyName.Text = "";
            TxtCity.Text = "";
            TxtCountry.Text = "";
            int cusIDvalue;
            try
            {
                //Check if TerritoryID Present in in Territory Table
                StringBuilder strBrCus = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strCustomers + objComFunc.strWhere + objComFunc.strCustomerID + CustomerID + objComFunc.strSemiColon);
                cusIDvalue = objComFunc.CommonVerify(strBrCus);
                if (cusIDvalue > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Customer already exists in Customers Table');", true);
                }
                else
                {
                    //insert query in String builder
                    strBrQuery = new StringBuilder(objComFunc.strInsert + objComFunc.strCustomers + objComFunc.strOpenPar + objComFunc.strCuColumns + objComFunc.strClosePar + objComFunc.strValues + "'" + CustomerID + "'" + objComFunc.strComma + "'" + CompanyName + "'" + objComFunc.strComma + "'" + City + "'" + objComFunc.strComma + "'" + Country + "'" + objComFunc.strClosePar + objComFunc.strSemiColon);
                    //convertion from string builder to string
                    strQuery = strBrQuery.ToString();
                    cmdInsert = objComFunc.CommonExecute(strQuery);
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Record added successfully');", true);
                    BindGrid();
                }
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
    }
}