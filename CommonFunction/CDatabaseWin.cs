using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;

namespace FRS
{
    #region CDatabaseWin
    /// <summary>
    /// For making the common database funtionalities for Windows.
    /// </summary>
    /// <!--Date :2007/11/21 By Joshi Jimit-->
    class CDatabaseWin
    {
        #region Variable Declaration
        /// <summary>
        /// string strCon, strSql
        /// </summary>
        private string strCon, strSql;
       
        //MYSQL
        ///  MySqlConnection connCommon
        /// </summary>
        private MySqlConnection connCommon;
        /// <summary>
        /// SqlTransaction trnCommon
        /// </summary>
        private MySqlTransaction trnCommon;
        /// <summary>
        /// SqlCommand cmdCommon
        /// </summary>
        private MySqlCommand cmdCommon;
        /// <summary>
        /// SqlDataAdapter adapCommon
        /// </summary>
        private MySqlDataAdapter adapCommon;
        #endregion

        #region GetConnection
        /// <summary>
        /// For Getting the connection to database by setting the connection string.
        /// </summary>
        /// <!--Date :2007/11/21 By Joshi Jimit-->
        public void GetConnection()
        {
            strCon = ConfigurationManager.ConnectionStrings["VideoGrabber"].ConnectionString;
            //string strFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            //IniFile initobj = new IniFile(strFilePath.Substring(6) + "\\FRS.ini");
            //strCon = initobj.IniReadValue("XP_DB_CON_STR", "ConnectionString");
            this.connCommon = new MySqlConnection(strCon);

        }
        #endregion

        #region OpenConnection
        //--------------------------------------------------------------------------------------------------
        //Method Name       :OpenConnection
        //Description       :For Getting the opening the Connection.
        //Parameters        :None
        //Return Type       :None
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public void OpenConnection()
        {
            try
            {
                if (connCommon.State == ConnectionState.Closed)
                {
                    this.connCommon.Open();
                }
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region CreateCommand
        //--------------------------------------------------------------------------------------------------
        //Method Name       :CreateCommand
        //Descripton        :For creating the command object and setting the connection to it.
        //Parameters        :None
        //Return Type       :None
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public void CreateCommand()
        {
            try
            {

                cmdCommon = new MySqlCommand();
                cmdCommon.Connection = this.connCommon;
            }
            catch (MySqlException exSqlException)
            {

                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }

        }
        #endregion

        #region CreateTransactionCommand
        //--------------------------------------------------------------------------------------------------
        //Method Name     :CreateTransactionCommand
        //Description     :For creating the command object with Transaction and setting the connection to it.
        //Parameters      :None
        //Return Type     :None
        //Date            :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public void CreateTransactionCommand(MySqlTransaction Transaction)
        {
            try
            {
                cmdCommon = new MySqlCommand();
                cmdCommon.Connection = this.connCommon;
                cmdCommon.Transaction = trnCommon;
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }

        }
        #endregion

        #region CloseConnection
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CloseConnection
        //Description       :For Closing the Connection.
        //Parameters        :None
        //Return Type       :None
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public void CloseConnection()
        {
            if (connCommon != null)
            {
                if (connCommon.State == ConnectionState.Open)
                {
                    connCommon.Close();
                }
            }
        }
        #endregion

        #region BeginTransaction
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :BeginTransaction
        //Description       :For Begining the Transaction.
        //Parameters        :None
        //Return Type       :SqlTransaction
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public MySqlTransaction BeginTransaction()
        {
            try
            {
                return trnCommon = connCommon.BeginTransaction();
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }

        #endregion

        # region Commit
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Commit
        //Description       :For Commiting the Transaction.
        //Parameters        :None
        //Return Type       :None
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public void Commit()
        {
            try
            {
                trnCommon.Commit();
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region RollBack
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :RollBack
        //Description       :For RollBack of the Transaction.
        //Parameters        :None
        //Return Type       :None
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public void RollBack()
        {
            try
            {
                trnCommon.Rollback();
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Add
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Add
        //Description       :For inserting the Data Into  the table.
        //Parameters        :TableName,ColumnNames and ColumnValues
        //Return Type       :int
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public int Add(string TableName, string ColumnNames, string ColumnValues)
        {

            try
            {
                CreateCommand();
                StringBuilder strBr = new StringBuilder();
                strBr.Append("INSERT INTO ");
                strBr.Append(TableName);
                strBr.Append("(");
                strBr.Append(ColumnNames);
                strBr.Append(") ");
                strBr.Append("VALUES");
                strBr.Append("(");
                strBr.Append(ColumnValues);
                strBr.Append(")");
                this.cmdCommon.CommandText = strBr.ToString();
                return this.cmdCommon.ExecuteNonQuery();
            }
            catch (SqlException exSqlException)
            {
                throw exSqlException;

            }
            catch (Exception exCommon)
            {
                throw exCommon;

            }

        }
        #endregion

        #region Delete
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Delete
        //Description       :For Deleting the Data from the Transaction.
        //Parameters        :TableName and WhereClause
        //Return Type       :int
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public int Delete(string TableName, string WhereClause)
        {
            try
            {
                CreateCommand();
                StringBuilder strBr = new StringBuilder();
                strBr.Append("DELETE FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                cmdCommon.CommandText = strBr.ToString();

                if (cmdCommon.Connection.State == ConnectionState.Closed)
                {
                    cmdCommon.Connection.Open();
                }

                return this.cmdCommon.ExecuteNonQuery();
          
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Modify

        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Delete
        //Description       :For Modification of the table data Transaction.
        //Parameters        :TableName and WhereClause
        //Return Type       :int
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public int Modify(string TableName, string ColumnNames, string WhereClause)
        {
            GetConnection();
            OpenConnection();
            CreateCommand();
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("UPDATE ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append("SET ");
                strBr.Append(ColumnNames);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                this.cmdCommon.CommandText = strBr.ToString();
                return this.cmdCommon.ExecuteNonQuery();
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Display'Normal'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Display
        //Description       :For retrieving the data from the database table according to the where criteria given Transaction.
        //Parameters        :ColumnNames,TableName and WhereClause
        //Return Type       :DataTable
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public DataTable Display(string ColumnNames, string TableName, string WhereClause)
        {
            GetConnection();
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT ");
                strBr.Append(ColumnNames);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                strSql = strBr.ToString();
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                DataSet ds = new DataSet();
                adapCommon.Fill(ds, TableName);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Display'join'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Display'join'
        //Description       :For retrieving the data from the database table according to the where criteria given Transaction.
        //Parameters        :ColumnNames,TableName and WhereClause
        //Return Type       :DataTable
        //Date              :2015/01/02 by adarsh pandey
        //--------------------------------------------------------------------------------------------------
        public DataTable Display(string ColumnNames, string TableName,string join, string WhereClause)
        {
            GetConnection();
            OpenConnection();
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT ");
                strBr.Append(ColumnNames);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append(join);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                strSql = strBr.ToString();
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                DataSet ds = new DataSet();
                adapCommon.Fill(ds, TableName);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Create Adapter
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateAdapter
        //Description       :Create the data adpter object and its command builder
        //Parameters        :SQLQuery
        //Return Type       :SqlDataAdapter
        //Date              :2007/09/05 by Hasmukh Mandavia
        //--------------------------------------------------------------------------------------------------
        public MySqlDataAdapter CreateAdapter(string SQLQuery)
        {
            MySqlDataAdapter DataAdpter = new MySqlDataAdapter(SQLQuery, this.connCommon);
            MySqlCommandBuilder cmbdCommand = new MySqlCommandBuilder(DataAdpter);
            return DataAdpter;
        }
        #endregion

        #region Create Select Query
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateSelectQuery
        //Description       :Creates the select query
        //Parameters        :ColumnNames, TableName
        //Return Type       :string
        //Date              :2007/09/05 by Hasmukh Mandavia
        //--------------------------------------------------------------------------------------------------        
        public string CreateSelectQuery(string ColumnNames, string TableName)
        {
            string strSql;
            StringBuilder strBr = new StringBuilder();
            strBr.Append("SELECT ");
            strBr.Append(ColumnNames);
            strBr.Append(" FROM ");
            strBr.Append(TableName);
            strBr.Append(" ");
            //strBr.Append(WhereClause);
            strSql = strBr.ToString();
            return strSql;
        }
        #endregion

        #region Create Select Query (OVERLOADED)
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateSelectQuery
        //Description       :Creates the select query with WHERE CLAUSE
        //Parameters        :ColumnNames, TableName, WhereClause
        //Return Type       :string
        //Date              :2007/09/05 by Hasmukh Mandavia
        //--------------------------------------------------------------------------------------------------               
        public string CreateSelectQuery(string ColumnNames, string TableName, string WhereClause)
        {
            string strSql;
            StringBuilder strBr = new StringBuilder();
            strBr.Append("SELECT ");
            strBr.Append(ColumnNames);
            strBr.Append(" FROM ");
            strBr.Append(TableName);
            strBr.Append(" ");
            strBr.Append(WhereClause);
            strSql = strBr.ToString();
            return strSql;
        }
        #endregion

        #region Fill DataSet Objcet
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :FillDataSet
        //Description       :Fills the dataset object using data adpter
        //Parameters        :DataSetObject, DataAdpter, TableName
        //Return Type       :DataSet
        //Date              :2007/09/05 by Hasmukh Mandavia
        //--------------------------------------------------------------------------------------------------               
        public DataSet FillDataSet(DataSet DataSetObject, MySqlDataAdapter DataAdpter, string TableName)
        {
            try
            {
                DataAdpter.Fill(DataSetObject, TableName);
                return DataSetObject;
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Update Records by adpter
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :UpdateByAdpter
        //Description       :Updates the records in SQL Database by adpter object
        //Parameters        :DataTableObject, DataAdpterObject
        //Return Type       :None
        //Date              :2007/09/05 by Hasmukh Mandavia
        //--------------------------------------------------------------------------------------------------               
        public void UpdateByAdpter(DataTable DataTableObject, MySqlDataAdapter DataAdpterObject)
        {
            try
            {
                DataAdpterObject.Update(DataTableObject);
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region Display'OverLoaded'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Display
        //Description       :For retrieving the data from the database table Transaction.
        //Parameters        :ColumnNames and TableName 
        //Return Type       :DataTable
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public DataTable Display(string ColumnName, string TableName)
        {
            GetConnection();
            try
            {
                GetConnection();//To Get the connection String.Vishal 31-05-18
                OpenConnection();//Open the connection if close.Vishal 31-05-18
                CreateCommand();//insert the connection string into connCommon Variable.Vishal 31-05-18
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT ");
                strBr.Append(ColumnName);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                strSql = strBr.ToString();
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                DataSet ds = new DataSet();
                adapCommon.Fill(ds, TableName);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region SelectCommon
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :SelectCommon
        //Description       :For retrieving the data from the database table Transaction.
        //Parameters        :SQL Query
        //Return Type       :DataTable
        //Date              :2010/02/02 by Bharat Prajapati
        //--------------------------------------------------------------------------------------------------
        public DataTable SelectCommon(string strSql)
        {
            try
            {
                GetConnection();
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                DataSet ds = new DataSet();
                adapCommon.Fill(ds);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        public void FillDataSet(DataSet ds, string query, string tblName)
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connCommon);

            // fill the DataSet using our DataAdapter 
            dataAdapter.Fill(ds, tblName);
        }

        #region SelectWithDataSet
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :SelectWithDataSet
        //Description       :For retrieving the data from the database table Transaction.
        //Parameters        :SQL Query
        //Return Type       :DataTable
        //Date              :2010/02/03 by Naved Ahmed
        //--------------------------------------------------------------------------------------------------
        public DataTable SelectWithDataSet(string strSql, DataSet ds)
        {
            try
            {
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                //ds = new DataSet();
                adapCommon.Fill(ds);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region GetSingleValue'Normal'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :GetSinglValue
        //Description       :For retrieving the single result(like max,count(*) etc..) according to the where criteria from the database table 
        //                  according the aggregate function given Transaction.
        //Parameters        :AggregateFunction,TableName and WhereClause
        //Return Type       :object
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public object GetSinglValue(string AggregateFunction, string TableName, string WhereClause)
        {
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT ");
                strBr.Append(AggregateFunction);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                this.cmdCommon.CommandText = strBr.ToString();
               
                if (cmdCommon.Connection.State == ConnectionState.Closed)
                {
                    cmdCommon.Connection.Open();
                }
                return cmdCommon.ExecuteScalar();
                //cmdCommon.Connection.Close();
               
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region GetSingleValue'OverLoaded'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :GetSinglValue
        //Description       :For retrieving the single result(like max,count(*) etc..) from the database table 
        //                  according the aggregate function given Transaction.
        //Parameters        :AggregateFunction and TableName 
        //Return Type       :object
        //Date              :2007/09/24 by Sandeep Sharma
        //--------------------------------------------------------------------------------------------------
        public object GetSinglValue(string AggregateFunction, string TableName)
        {
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT ");
                strBr.Append(AggregateFunction);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                this.cmdCommon.CommandText = strBr.ToString();
                return cmdCommon.ExecuteScalar();
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        public int InsertRecord(string strTableName, Dictionary<string, string> objDictFieldValue)
        {
            StringBuilder strbQuery;         
            MySqlCommand objSqlCommand = null;
            try
            {
                GetConnection();
                strbQuery = new StringBuilder();
                strbQuery.Append(@"INSERT INTO ");
                strbQuery.Append(strTableName);
                StringBuilder Col_Name = new StringBuilder();
                StringBuilder Col_Values = new StringBuilder();
                foreach (KeyValuePair<string, string> Key_Value in objDictFieldValue)
                {
                    if (Key_Value.Value.ToString() != "")
                    {
                        Col_Name.Append(Key_Value.Key + ",");

                        Col_Values.Append("'" + Key_Value.Value + "',");
                    }
                }
                strbQuery.Append("(" + Convert.ToString(Col_Name).TrimEnd(',') + ")");
                strbQuery.Append(" VALUES");
                strbQuery.Append("(" + Convert.ToString(Col_Values).TrimEnd(',') + ")");
                // dont use transaction to create command object if it is NULL
                if (trnCommon == null)
                {
                    objSqlCommand = new MySqlCommand(strbQuery.ToString(), connCommon);
                }
                else
                {
                    //if (objSqlCommand.Connection.State == ConnectionState.Closed)
                    //{
                    //    objSqlCommand.Connection.Open();
                    //}

                    objSqlCommand = new MySqlCommand(strbQuery.ToString(), connCommon, trnCommon);
                }

                //// Execute Insert Sql Statement against connection
                if (objSqlCommand.Connection.State == ConnectionState.Closed)
                {
                    objSqlCommand.Connection.Open();
                }
                int i = objSqlCommand.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex) { throw ex; }
            finally { strbQuery = null; objSqlCommand = null; }

        }

        #region FillUserDetails
        /// <summary>
        /// Set all Properties of CUserST class.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>CUserST</returns>
        /// <!-- Date :2007/10/09 By Nirav Parmar -->
        public CUserST FillUserDetails(string ID)
        {
            CUserST objCUserST = new CUserST();
            DataTable dt = Display("*", Common.TBL_USERINFO, "WHERE UserID = '" + ID + "'");

            objCUserST.UserID = dt.Rows[0][0].ToString();
            objCUserST.Name = dt.Rows[0][1].ToString();
            objCUserST.MName = dt.Rows[0][2].ToString();
            objCUserST.LName = dt.Rows[0][3].ToString();
            objCUserST.Sex = dt.Rows[0][3].ToString();
            objCUserST.dob = String.Format("{0:yyyy/MM/dd}", dt.Rows[0][4].ToString());
            objCUserST.Age = dt.Rows[0][5].ToString();
            objCUserST.Nationality = dt.Rows[0][6].ToString();
            objCUserST.Address = dt.Rows[0][7].ToString();           
            objCUserST.Organization = dt.Rows[0][8].ToString();           
            
            return objCUserST;
        }
        #endregion

        #region CAll spInsertDailyTrack
        /// <summary>
        /// Call store procedure to insert blank data for users
        /// </summary>
        /// <param name="strStartDate"></param>
        /// <param name="intDays"></param>
        /// <returns></returns>
        public int CallSPInsertDailyTrack(DateTime strStartDate, int intDays,string createdBy)
        {
            MySqlCommand cmd = new MySqlCommand(Common.SPINSERTDAILYTRACK, this.connCommon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("startDate", strStartDate));
            cmd.Parameters.Add(new MySqlParameter("Days", intDays));
            cmd.Parameters.Add(new MySqlParameter("createdBy", intDays));
            //cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            //cmd.Connection.Close();
            
            return i;

        }
        #endregion

        public int CallSPUpdateDuration(string strmonth, string stryear,int Days)
        {
            MySqlCommand cmd = new MySqlCommand("sp_UpdateDuration", this.connCommon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("strmonth", strmonth));
            cmd.Parameters.Add(new MySqlParameter("stryear", stryear));
            cmd.Parameters.Add(new MySqlParameter("Days", Days));
  
            int i = cmd.ExecuteNonQuery();
            return i;
        }

        #region CAll UpdateSignIn_Manual
        /// <summary>
        /// Update Sign in time in UserTrack Table
        /// </summary>
        /// <param name="strStartDate"></param>
        /// <param name="intDays"></param>
        /// <returns></returns>
        public int CallSPUpdateSignInOut(DateTime strStartDate, int intDays)
        {
            //Common.SPUPDATESIGNINOUT
            //MySqlCommand cmd = new MySqlCommand("sp_UpdateSignInSignOut_Manually", this.connCommon);
            MySqlCommand cmd = new MySqlCommand("spUpdateSignInOut_Manual", this.connCommon);
            //cmd.CommandTimeout = 600;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("startDate", strStartDate));
            cmd.Parameters.Add(new MySqlParameter("Days", intDays));
            //cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            //cmd.Connection.Close();
            return i;

        }
        #endregion

        #region CAll SPInsertMonthlyAttendance
        /// <summary>
        /// InsertMonthlyAttendance
        /// </summary>
        /// <param name="strStartDate"></param>
        /// <param name="intDays"></param>
        /// <returns></returns>
        public int CallSPInsertMonthlyAttendance(DateTime forTheMonth, string curUser)
        {

            MySqlCommand cmd = new MySqlCommand(Common.SPINSERTMONTHLYATT, this.connCommon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("forTheMonth", forTheMonth));
            cmd.Parameters.Add(new MySqlParameter("curUser", curUser));
            //cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            //cmd.Connection.Close();
            return i;

        }
        #endregion

        #region CAll UpdateAttendance
        /// <summary>
        /// UpdateAttendance
        /// </summary>
        /// <param name="strStartDate"></param>
        /// <param name="intDays"></param>
        /// <returns></returns>
        public int CallSPUpdateMonthlyAttendance(DateTime forTheMonth)
        {

            MySqlCommand cmd = new MySqlCommand(Common.SPUPDATEMONTHLYATT, this.connCommon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("forTheMonth", forTheMonth));            
            //cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            //cmd.Connection.Close();
            return i;

        }
        #endregion        

        public virtual int Execute(string strQry)
        {
            // SqlCommand Object
            MySqlCommand objSqlCommand = null;
            objSqlCommand = new MySqlCommand(strQry, connCommon);
            return objSqlCommand.ExecuteNonQuery();
        }

        #region Insert
        /// <summary>
        /// Execute Insert Query on Table
        /// argument1(Not null): Key,Value pair object. Key- Field Name; Value- Value to insert
        /// </summary>
        /// <param name="objDictFieldValue"></param>
        /// <returns>int: No of rows affected after this query executed</returns>
        public virtual int Insert(string strTableName,Dictionary<string, string> objDictFieldValue)
        {
            // Stores no of (Key, Value) Pair in Dictionary object
            int iPairCount = objDictFieldValue.Count;

            // Counter for loop
            int iLoopCnt = 0;

            // Insert Query Field Name
            StringBuilder sbFields = new StringBuilder();

            // Insert Query Field Value
            StringBuilder sbValues = new StringBuilder();

            // Insert Query
            StringBuilder sbInsertQuery = null;

            // SqlCommand Object
            MySqlCommand objSqlCommand = null;

            try
            {
                // Loop to iterate each (key,value) pair of Dictionary object
                foreach (KeyValuePair<string, string> kvpRecord in objDictFieldValue)
                {
                    // Create insert query field
                    sbFields.Append(kvpRecord.Key);

                    //if (kvpRecord.Value.Trim().Equals("null",
                    //                        StringComparison.OrdinalIgnoreCase))
                    //{
                    //    sbValues.Append(kvpRecord.Value);
                    //}
                    //else if (kvpRecord.Value.Trim().Equals("",
                    //                    StringComparison.OrdinalIgnoreCase))
                    //{
                    //    sbValues.Append("null");
                    //}
                    //else if (kvpRecord.Value.Trim().Equals("False"))
                    //{
                    //    sbValues.Append("0");
                    //}
                    //else if (kvpRecord.Value.Trim().Equals("True"))
                    //{
                    //    sbValues.Append("1");
                    //}
                    //else
                    //{
                        sbValues.Append("'");
                        sbValues.Append(kvpRecord.Value);
                        sbValues.Append("'");
                    //}

                    // do not append "," for last pair
                    if (iLoopCnt != iPairCount - 1)
                    {
                        sbFields.Append(", ");
                        sbValues.Append(", ");
                    }
                    // increment loop counter
                    iLoopCnt++;
                }

                // Insert Query
                sbInsertQuery = new StringBuilder();

                // Insert Query Header
                sbInsertQuery.Append("INSERT INTO ");
                sbInsertQuery.Append(strTableName + " ");
                sbInsertQuery.Append("(");

                // Insert Query Field
                sbInsertQuery.Append(sbFields.ToString());
                sbInsertQuery.Append(") ");

                // Insert Query Value
                sbInsertQuery.Append("VALUES ");
                sbInsertQuery.Append("(");
                sbInsertQuery.Append(sbValues.ToString());
                sbInsertQuery.Append(") ");
               
                // dont use transaction to create command object if it is NULL
                if (trnCommon == null)
                {
                    objSqlCommand = new MySqlCommand(sbInsertQuery.ToString(), connCommon);
                }
                else
                {
                    //if (objSqlCommand.Connection.State == ConnectionState.Closed)
                    //{
                    //    objSqlCommand.Connection.Open();
                    //}
                    
                    objSqlCommand = new MySqlCommand(sbInsertQuery.ToString(), connCommon, trnCommon);
                }

                // Execute Insert Sql Statement against connection
                if (objSqlCommand.Connection.State == ConnectionState.Closed)
                {
                    objSqlCommand.Connection.Open();
                }
                return objSqlCommand.ExecuteNonQuery();
            }
            catch (SqlException exSQL)
            {
                throw exSQL;
            }
            catch (InvalidOperationException exInvalidOperation)
            {
                throw exInvalidOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbInsertQuery = null;
                sbFields = null;
                sbValues = null;
                objSqlCommand.Connection.Close();
                objSqlCommand = null;

            }
        }
        #endregion
        #region DisplayDistinct'Where clause'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :DisplayDistinct
        //Description       :For retrieving the data from the database table according to the where criteria given Transaction.
        //Parameters        :ColumnNames,TableName and WhereClause
        //Return Type       :DataTable
        //Date              :2014/12/29 by Adarsh Pandey
        //--------------------------------------------------------------------------------------------------
        public DataTable DisplayDistinct(string ColumnNames, string TableName, string WhereClause)
        {
            GetConnection();
            OpenConnection();
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT DISTINCT ");
                strBr.Append(ColumnNames);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                if (!String.IsNullOrEmpty(WhereClause))
                 strBr.Append(WhereClause);
                strSql = strBr.ToString();
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                DataSet ds = new DataSet();
                adapCommon.Fill(ds, TableName);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion
        #region DisplayDistinct'Where with between clause'
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :DisplayDistinct
        //Description       :For retrieving the data from the database table according to the where criteria given Transaction.
        //Parameters        :ColumnNames,TableName, WhereClause,between,and
        //Return Type       :DataTable
        //Date              :2014/12/30 by Adarsh Pandey
        //--------------------------------------------------------------------------------------------------
        public DataTable DisplayDistinct(string ColumnNames, string TableName, string WhereClause, string between, string and,string OR )
        { 
            GetConnection();
            try
            {
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SELECT DISTINCT ");
                strBr.Append(ColumnNames);
                strBr.Append(" FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                if (!String.IsNullOrEmpty(WhereClause))
                    strBr.Append(WhereClause);
                strBr.Append(between);
                strBr.Append(and);
                if (and == "AND ''")
                { strBr.Append(OR); }
                strSql = strBr.ToString();
                adapCommon = new MySqlDataAdapter(strSql, connCommon);
                DataSet ds = new DataSet();
                adapCommon.Fill(ds, TableName);
                return ds.Tables[0];
            }
            catch (MySqlException exSqlException)
            {
                throw exSqlException;
            }
            catch (Exception exCommon)
            {
                throw exCommon;
            }
        }
        #endregion

        #region GetUpdateQuery
        /// <summary>
        /// Update Query
        /// </summary>
        /// <param name="htUpdateFieldValue"></param>
        /// <param name="htWhereFieldValue"></param>
        /// <returns>Query</returns>
        protected string GetUpdateQuery(string strTableName,
                                        Dictionary<string, string> htUpdateFieldValue,
                                        Dictionary<string, string> htWhereFieldValue)
        {

            StringBuilder sbUpdateQuery = new StringBuilder();
            sbUpdateQuery.Append("Update");
            sbUpdateQuery.Append(" ");
            sbUpdateQuery.Append(strTableName);
            sbUpdateQuery.Append(" ");
            sbUpdateQuery.Append("SET");
            sbUpdateQuery.Append(" ");

            foreach (KeyValuePair<string, string> UpdateVal in htUpdateFieldValue)
            {
                sbUpdateQuery.Append(UpdateVal.Key);
                sbUpdateQuery.Append(" = ");

                //check for null value for allow NULL fields.
                if (UpdateVal.Value == null)
                {
                    sbUpdateQuery.Append("null");
                }
                else
                {
                    //check for apostrophe
                    string strVal = UpdateVal.Value;
                    if (strVal.Contains("'"))
                    {
                        strVal = strVal.Replace("'", "''");
                    }
                    sbUpdateQuery.Append("'");
                    sbUpdateQuery.Append(strVal);
                    sbUpdateQuery.Append("'");
                }
                sbUpdateQuery.Append(",");
            }

            sbUpdateQuery.Remove(sbUpdateQuery.Length - 1, 1);
            sbUpdateQuery.Append(" ");
            sbUpdateQuery.Append(" WHERE ");
            sbUpdateQuery.Append(" ");

            foreach (KeyValuePair<string, string> WhereVal in htWhereFieldValue)
            {
                sbUpdateQuery.Append(WhereVal.Key);
                sbUpdateQuery.Append(" = ");
                sbUpdateQuery.Append("'");
                sbUpdateQuery.Append(WhereVal.Value);
                sbUpdateQuery.Append("'");
                if (htWhereFieldValue.Count > 1)
                {
                    sbUpdateQuery.Append(" AND ");
                }
            }
            if (htWhereFieldValue.Count > 1)
            {
                sbUpdateQuery.Remove(sbUpdateQuery.Length - 4, 3);
            }
            return sbUpdateQuery.ToString();
        }
        #endregion

        #region Update
        /// <summary>
        /// Execute Update Query on Table
        /// argument1(Not null): Key,Value pair object. Key- Field Name; Value- Value to insert
        /// argument2(Null allow): Where condition
        /// </summary>
        /// <param name="objDictSetFieldValue"></param>
        /// <param name="objWhereCriteria"></param>
        /// <returns>int: No of rows affected after this query executed</returns>
        public virtual int Update(string strTableName, Dictionary<string, string> objDictSetFieldValue, Dictionary<string, string> objWhereCriteria)
        {
            // return value
            int returnValue = 0;

            // SqlCommand Object
            MySqlCommand objSqlCommand = null;

            // Update query
            string strUpdateQuery = null;

            try
            {
                strUpdateQuery = GetUpdateQuery(strTableName, objDictSetFieldValue, objWhereCriteria);


                // Do not use transaction to create command object if it is NULL
                if (trnCommon == null)
                {
                    objSqlCommand = new MySqlCommand(strUpdateQuery, connCommon);
                }
                else
                {
                    objSqlCommand = new MySqlCommand(strUpdateQuery, connCommon, trnCommon);
                }

                // Execute Update Sql Statement against connection
                returnValue = objSqlCommand.ExecuteNonQuery();
            }
            catch (SqlException exSQL)
            {
                throw exSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objSqlCommand = null;
            }
            return returnValue;
        }
        #endregion                       
    }
    #endregion
}
