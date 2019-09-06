//‘-----------------------------------------------------------
//‘ [ID] SupplierService Class
//‘ [Name] Pranit Chimurkar
//‘ [Func] SupplierService Class
//‘ [Date] 2019/08/13 by Pranit
//‘-----------------------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using System.Text;

namespace Day2
{
    //‘-----------------------------------------------------------
    //‘ [ID] SupplierService Class
    //‘ [Name] Pranit Chimurkar
    //‘ [Func] SupplierService Class
    //‘ [Date] 2019/08/13 by Pranit
    //‘-----------------------------------------------------------
    public class SupplierService
    {
        Supplier sup;
        ArrayList arrylist;
        public ArrayList GetSuppliers()
        {
            //objects
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataAdapter sda;
            MySqlDataReader sqlread;
            con = new MySqlConnection();
            StringBuilder stringBuild;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            con.Open();//open the connection
            //insert SQLquery in String builder
            stringBuild = new StringBuilder("Select SupplierID,CompanyName,ContactName,City from Suppliers");
            cmd = new MySqlCommand(stringBuild.ToString(), con);
            sda = new MySqlDataAdapter(cmd);
            sqlread = cmd.ExecuteReader();//Reading data
            arrylist = new ArrayList();
            while (sqlread.Read())
            {
                sup = new Supplier();
                sup.SupplierID = Convert.ToInt32(sqlread[0]);
                sup.CompanyName = sqlread[1].ToString();
                sup.ContactName = sqlread[2].ToString();
                sup.City = sqlread[3].ToString();
                arrylist.Add(sup);
                sup = null;
            }
            return arrylist;
        }
    }
}