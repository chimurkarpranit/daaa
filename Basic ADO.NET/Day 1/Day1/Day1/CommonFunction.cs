using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;

#region Class for CommonFunction
namespace Day1
{
    public class CommonFunction
    {
        #region Variable Declaration
        /// <summary>
        /// string strCon, strSql
        /// </summary>
        private string strCon, strSql;
        /// <summary>
        ///  MySqlConnection connCommon
        /// </summary>
        private MySqlConnection connCommon;
        /// <summary>
        /// MySqlTransaction trnCommon
        /// </summary>
        private MySqlTransaction trnCommon;
        /// <summary>
        /// MySqlCommand cmdCommon
        /// </summary>
        private MySqlCommand cmdCommon;
        /// <summary>
        /// MySqlDataAdapter adapCommon
        /// </summary>
        private MySqlDataAdapter adapCommon;
        /// <summary>
        /// DataTable dtCommon
        /// </summary>
        private DataTable dtCommon;
        /// <summary>
        /// DataSet dsCommon
        /// </summary>
        private DataSet dsCommon;
        #endregion

        #region GetConnection
        /// <summary>
        /// For Getting the connection to database by setting the connection string.
        /// </summary>
        /// <!--Date :2019/09/04 By Pranit Chimurkar-->
        public void GetConnection()
        {
            strCon = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            this.connCommon = new MySqlConnection(strCon);
        }
        #endregion

        #region OpenConnection
        //--------------------------------------------------------------------------------------------------
        //Method Name       :OpenConnection
        //Description       :For Getting the opening the Connection.
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
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

        #region CloseConnection
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CloseConnection
        //Description       :For Closing the Connection.
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
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

        #region Create Command
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateCommand
        //Description       :For Creating the command
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
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

        #region Create Select SQL Query
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateSelectQuery
        //Description       :Creating the SQL Query
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
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
            strSql = strBr.ToString();
            return strSql;
        }
        #endregion

        #region Create Select SQL Query with Where (Overloaded)
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateSelectQuery
        //Description       :Creating the SQL Query with Where
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
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

        #region Create Select SQL Query with WhereClause and Values (Overloaded)
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CreateSelectQuery
        //Description       :Creating the SQL Query with Where
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public string CreateSelectQuery(string ColumnNames, string TableName, string ClauseColumnName, string Value)
        {
            string strSql;
            StringBuilder strBr = new StringBuilder();
            strBr.Append("SELECT ");
            strBr.Append(ColumnNames);
            strBr.Append(" FROM ");
            strBr.Append(TableName);
            strBr.Append(" WHERE ");
            strBr.Append(ClauseColumnName);
            strBr.Append(" = '");
            strBr.Append(Value);
            strBr.Append("'");
            strSql = strBr.ToString();
            return strSql;
        }
        #endregion

        #region CommonData using DataTable
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :CommonDataTable
        //Description       :For retrieving the data from the database table
        //Parameters        :SQL Query
        //Return Type       :DataTable
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public DataTable CommonDataTable(string strQuery)
        {
            try
            {
                GetConnection();
                OpenConnection();
                cmdCommon = new MySqlCommand(strQuery, connCommon);
                adapCommon = new MySqlDataAdapter(cmdCommon);
                adapCommon.SelectCommand = cmdCommon;
                dtCommon = new DataTable();
                adapCommon.Fill(dtCommon);
                return dtCommon;
            }
            catch (MySqlException exMySqlExc)  //Specific Exception
            {
                throw exMySqlExc;
            }
            catch (Exception exExc)  //Generic Exception
            {
                throw exExc;
            }
            finally
            {
                cmdCommon = null;
            }
        }
        #endregion

        #region CommonData using DataSet
        //--------------------------------------------------------------------------------------------------     
        //Method Name       :CommonDataSet
        //Description       :For retrieving the data from the database table
        //Parameters        :SQL Query
        //Return Type       :DataTable
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public DataSet CommonDataSet(string strQuery)
        {
            try
            {
                GetConnection();
                OpenConnection();
                cmdCommon = new MySqlCommand(strQuery, connCommon);
                adapCommon = new MySqlDataAdapter(cmdCommon);
                adapCommon.SelectCommand = cmdCommon;
                dsCommon = new DataSet();
                adapCommon.Fill(dsCommon);
                return dsCommon;
            }
            catch (MySqlException exMySqlExc)  //Specific Exception
            {
                throw exMySqlExc;
            }
            catch (Exception exExc)  //Generic Exception
            {
                throw exExc;
            }
            finally
            {
                cmdCommon = null;
                adapCommon = null;
            }
        }
        #endregion
    }
}
#endregion