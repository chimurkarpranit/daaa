//‘-----------------------------------------------------------
//‘ [ID] Employees Table
//‘ [Name] Pranit Chimurkar
//‘ [Func] Employees Table
//‘ [Date] 2019/08/16 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Text;

namespace Day5_8
{
    public partial class Add_Form : System.Web.UI.Page
    {
        AllMessage objMsg = new AllMessage();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //‘-----------------------------------------------------------
        //‘[ID] Insert
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Adding Data
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void Insert(object sender, EventArgs e)
        {
            CommonFunctions objComFunc = new CommonFunctions();
            StringBuilder strBrQuery;//string builder for query
            MySqlCommand cmdInsert;
            string strQuery;
            try
            {
                string FirstName = TxtFirstName.Text;
                string LastName = TxtLastName.Text;
                string City = TxtCity.Text;
                string Salary = TxtSalary.Text;
                string Notes = TxtNotes.Text;
                TxtFirstName.Text = "";
                TxtLastName.Text = "";
                TxtCity.Text = "";
                TxtSalary.Text = "";
                TxtNotes.Text = "";
                //insert query in String builder
                strBrQuery = new StringBuilder(objComFunc.strInsert + objComFunc.strEmployees + objComFunc.strColForEmp + objComFunc.strValues + "'" + FirstName + "'" + "," + "'" + LastName + "'" + "," + "'" + City + "'" + "," + Salary + "," + "'" + Notes + "'" + ")");
                //convertion from string builder to string
                strQuery = strBrQuery.ToString();
                cmdInsert = objComFunc.CommonExecute(strQuery);
                ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Employee Details is successfully added');", true);
            }
            catch (Exception ex)
            {
                Response.Write(objMsg.strException + ex.Message);
            }
        }
        //‘-----------------------------------------------------------
        //‘[ID] LBtnEmpDetails_Click
        //‘[Name] Pranit Chimurkar
        //‘[Func] For Editing
        //‘[Param]  <1：Sender>
        //‘         <2：e>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        protected void LBtnEmpDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}