//‘-----------------------------------------------------------
//‘ [ID] EmployeeTerritories Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] EmployeeTerritories Table
//‘ [Date] 2019/08/19 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Web.UI;

namespace Day5_8
{
    //‘-----------------------------------------------------------
    //‘ [ID] EmployeeTerritories Table
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] EmployeeTerritories Table
    //‘ [Date] 2019/08/19 by Pranit
    //‘-----------------------------------------------------------
    public partial class Assignment_5 : Page
    {
        AllMessage objMsg = new AllMessage();
        //‘-----------------------------------------------------------
        //‘[ID] Page_Load
        //‘[Name] Pranit Chimurkar
        //‘[Func] Loads the Page
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Assign();
            }
        }
        protected void Assign()
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            DataTable dtEmpID;
            string strQuery;
            try
            {
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strSel + objComFunc.strEmpID + objComFunc.strFrom + objComFunc.strEmployees);
                //conversion from string builder to string
                strQuery = strBrQuery.ToString();
                dtEmpID = objComFunc.ConnectionGenerate(strQuery);
                DdlEmpID.DataSource = dtEmpID;
                DdlEmpID.DataTextField = "EmployeeID";
                DdlEmpID.DataValueField = "EmployeeID";
                DdlEmpID.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] AddButtonClick
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Adding Data
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/19 by Pranit
        //‘-----------------------------------------------------------
        protected void AddButtonClick(object sender, EventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdInsert;
            string strQuery;
            int terIDvalue;
            int empIDvalue;
            try
            {
                //Check if TerritoryID Present in in Territory Table
                StringBuilder strBrTerr = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strTerritories + objComFunc.strWhere + objComFunc.strTerritoryID + TxtTerritoryID.Text);
                terIDvalue = objComFunc.CommonVerify(strBrTerr);
                //Check if TerritoryID for corresponding EmployeeID are present in Employeeterritories Table
                StringBuilder strBrEmpTerr = new StringBuilder(objComFunc.strSel + objComFunc.strCOUNT + objComFunc.strFrom + objComFunc.strEmployeeTerriTories + objComFunc.strWhere + objComFunc.strEmployeeID + DdlEmpID.SelectedValue + objComFunc.strAND + objComFunc.strTerritoryID + TxtTerritoryID.Text + objComFunc.strSemiColon);
                empIDvalue = objComFunc.CommonVerify(strBrEmpTerr);
                if (terIDvalue > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('TerritoryID already exists in Territories Table');", true);
                }
                else if (empIDvalue > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('EmployeeID and TerritoryID already exists in EmployeeTerritories table');", true);
                }
                else
                {
                    //insert query in String builder
                    strBrQuery = new StringBuilder(objComFunc.strForKey + objComFunc.strInsert + objComFunc.strEmployeeTerriTories + objComFunc.strOpenPar + objComFunc.strEmpTerColumns + objComFunc.strClosePar + objComFunc.strValues + DdlEmpID.SelectedItem.Value.ToString() + objComFunc.strComma + TxtTerritoryID.Text + objComFunc.strClosePar + objComFunc.strSemiColon);
                    //convertion from string builder to string
                    strQuery = strBrQuery.ToString();
                    cmdInsert = objComFunc.CommonExecute(strQuery);
                    ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Records successfully Added');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
    }
}