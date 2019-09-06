//‘-----------------------------------------------------------
//‘ [ID] CommonFunctions
//‘ [Name] Pranit Chimurkar
//‘ [Func] Common Functions
//‘ [Date] 2019/08/10 by Pranit
//‘-----------------------------------------------------------

using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] CommonFunctions
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] Used for Common Functions in Project
    //‘ [Date] 2019/08/10 by Pranit
    //‘-----------------------------------------------------------
    public class CommonFunctions
    {
        DataTable dtData;
        MySqlCommand cmdData;
        //‘-----------------------------------------------------------
        //‘[ID] ConnectionGenerate
        //‘[Name] Pranit Chimurkar
        //‘[Func] Used for Common Functions
        //‘[Param]  <1：strQuery>
        //‘[Date] 2019/08/10 by Pranit
        //‘-----------------------------------------------------------
        public string strEmp = "select * from employees";
        public string strTerr = "select * from territories";
        public string strEmpTer = "select * from employeeterritories";
        public string strEmpTerr = "select * from employeeterritories et where et.EmployeeID = ";

        public string strDel = "delete FROM employees where EmployeeID=";
        public string strEmploTerr = "select * from employeeterritories where EmployeeID = ";


        //Constant Queries
        public string strForKey = "SET FOREIGN_KEY_CHECKS = 0;";
        public string strSelAll = "Select * from ";
        public string strDelete = "delete from ";
        public string strUpdate = "Update ";
        public string strInsert = "Insert into ";
        public string strValues = "VALUES (";
        public string strSet = "set ";
        public string strWhere = " where ";

        //Constant Table Name
        public string strEmployeeTerriTories = "employeeterritories ";
        public string strEmployees = "employees ";

        //Constant Column Name
        public string strEmployeeID = "EmployeeID = ";
        public string strFirstName = "FirstName= '";
        public string strLastName = "',LastName='";
        public string strCity = "',City='";
        public string strSalary = "',Salary='";
        public string strNotes = "',Notes='";
        public string strSingleQuote = "'";
        public string strColForEmp = "(FirstName, LastName, City, Salary, Notes) ";

        public DataTable ConnectionGenerate(string strQuery)
        {
            //MySql CONNECTION 
            MySqlConnection connData;
            MySqlDataAdapter adapData;
            MySqlDataReader rdrData;
            connData = new MySqlConnection();            
            try
            {
                //connectionstring
                connData.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                if (connData != null && connData.State == ConnectionState.Closed)
                {
                    connData.Open();//open the connection                        
                }                
                cmdData = new MySqlCommand(strQuery, connData);
                adapData = new MySqlDataAdapter(cmdData);
                rdrData = cmdData.ExecuteReader();//Reading data
                dtData = new DataTable();
                dtData.Load(rdrData);
                return dtData;
            }
            catch (MySqlException ex)  //Specific Exception
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)  //Generic Exception
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (connData.State == ConnectionState.Open)
                {
                    connData.Close();//open the connection
                }
                cmdData = null;
                adapData = null;
                rdrData = null;
            }
        }
        public MySqlCommand CommonExecute(string strQuery)
        {
            MySqlConnection connData = new MySqlConnection();            
            MySqlTransaction trnData = null; //MySqlTransaction
            try
            {                
                //connectionstring
                connData.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                if (connData != null && connData.State == ConnectionState.Closed)
                {
                    connData.Open();//open the connection
                }
                trnData = connData.BeginTransaction(); //MySqlTransaction BeginTransaction
                cmdData = new MySqlCommand(strQuery, connData);
                cmdData.ExecuteNonQuery();
                trnData.Commit();  //MySqlTransaction Commit
                return cmdData;
            }
            catch (MySqlException ex)  //Specific Exception
            {
                trnData.Rollback();   //Transaction Rollback
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)  //Generic Exception
            {
                trnData.Rollback();   //Transaction Rollback
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}