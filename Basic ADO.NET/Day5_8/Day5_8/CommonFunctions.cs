//‘-----------------------------------------------------------
//‘ [ID] CommonFunctions
//‘ [Name] Pranit Chimurkar
//‘ [Func] Common Functions
//‘ [Date] 2019/08/16 by Pranit
//‘-----------------------------------------------------------

using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace Day5_8
{
    public class CommonFunctions
    {
        //Constant Queries
        public string strForKey = "SET FOREIGN_KEY_CHECKS = 0;";
        public string strSelAll = "Select * from ";
        public string strSel = "Select ";
        public string strDelete = "delete from ";
        public string strUpdate = "Update ";
        public string strInsert = "Insert into ";
        public string strValues = "VALUES (";
        public string strSet = "set ";
        public string strWhere = " where ";
        public string strFrom = "from ";
        public string strAND = " and ";
        public string strOpenPar = " (";
        public string strClosePar = ") ";
        public string strComma = ",";
        public string strSemiColon = "; ";
        public string strCOUNT = " COUNT(*) ";

        //Constant Table Name
        public string strEmployeeTerriTories = "employeeterritories ";
        public string strEmployees = "employees ";
        public string strTerritories = "territories ";
        public string strCustomers = "Customers ";

        //Constant Column Name
        public string strEmpID = "EmployeeID ";
        public string strEmployeeID = "EmployeeID = ";
        public string strTerritoryID = "TerritoryID = ";
        public string strTerrDesc = "TerritoryDescription= ";
        public string strRegID = "RegionID= ";
        public string strFirstName = "FirstName= '";
        public string strLastName = "',LastName='";
        public string strCity = "',City='";
        public string strCountry = "',Country='";
        public string strSalary = "',Salary='";
        public string strNotes = "',Notes='";
        public string strCustomerID = "CustomerID= ";
        public string strSingleQuote = "'";
        public string strColForEmp = "(FirstName, LastName, City, Salary, Notes) ";
        public string strColForTer = "(TerritoryID, TerritoryDescription, RegionID) ";
        public string strEmpColumns = "EmployeeID,FirstName,LastName,City,Salary,Notes ";
        public string strCuColumns = "CustomerID,Companyname,City,Country ";
        public string strEmpTerColumns = "EmployeeID, TerritoryID ";

        DataTable dtData;
        MySqlCommand cmdData;
        int intVerify;
        //‘-----------------------------------------------------------
        //‘[ID] ConnectionGenerate
        //‘[Name] Pranit Chimurkar
        //‘[Func] Used for Common Functions
        //‘[Param]  <1：strQuery>
        //‘[Date] 2019/08/16 by Pranit
        //‘-----------------------------------------------------------
        public DataTable ConnectionGenerate(string strQuery)
        {
            //MySql CONNECTION 
            MySqlConnection connData;
            MySqlCommand cmdData;
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
                    connData.Close();//Close the connection
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
            finally
            {
                if (connData.State == ConnectionState.Open)
                {
                    connData.Close();//open the connection
                }
            }
        }

        public int CommonVerify(StringBuilder strBrVerify)
        {
            MySqlConnection connData = new MySqlConnection();
            try
            {
                //connectionstring
                connData.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                if (connData != null && connData.State == ConnectionState.Closed)
                {
                    connData.Open();//open the connection
                }
                cmdData = new MySqlCommand(strBrVerify.ToString(), connData);
                intVerify = Convert.ToInt32(cmdData.ExecuteScalar());
                return intVerify;
            }
            catch (MySqlException ex)  //Specific Exception
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            catch (Exception ex)  //Generic Exception
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                if (connData.State == ConnectionState.Open)
                {
                    connData.Close(); //open the connection
                }
            }
        }
    }
}