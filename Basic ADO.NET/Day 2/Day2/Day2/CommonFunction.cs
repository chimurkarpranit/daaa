using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

#region Class for CommonFunction
namespace Day2
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

        #region CreateTransactionCommand
        //--------------------------------------------------------------------------------------------------
        //Method Name     :CreateTransactionCommand
        //Description     :For creating the command object with Transaction and setting the connection to it.
        //Parameters      :None
        //Return Type     :None
        //Date            :2019/09/04 by Pranit Chimurkar
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

        #region BeginTransaction
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :BeginTransaction
        //Description       :For Begining the Transaction.
        //Parameters        :None
        //Return Type       :MySqlTransaction
        //Date              :2019/09/04 by Pranit Chimurkar
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

        #region Commit
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Commit
        //Description       :For Commiting the Transaction.
        //Parameters        :None
        //Return Type       :None
        //Date              :2019/09/04 by Pranit Chimurkar
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
        //Date              :2019/09/04 by Pranit Chimurkar
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
        
        #region Create Insert Sql Query using Dictionary
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
        #endregion

        #region Insert Sql Query
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Add
        //Description       :For inserting the Data Into  the table.
        //Parameters        :TableName,ColumnNames and ColumnValues
        //Return Type       :int
        //Date              :2019/09/04 by Pranit Chimurkar
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

        #region Create Delete Sql Query
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Delete
        //Description       :For Deleting the Data from the Transaction.
        //Parameters        :TableName and WhereClause
        //Return Type       :int
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public string Delete(string TableName, string WhereClause)
        {
            try
            {
                string strSql;
                StringBuilder strBr = new StringBuilder();
                strBr.Append("DELETE FROM ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                strSql = strBr.ToString();
                return strSql;
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

        #region Create Delete SQL Query for Where Clause Value of type Integer
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Delete
        //Description       :For Deleting the Data from the Transaction.
        //Parameters        :TableName and WhereClause
        //Return Type       :int
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public string DeleteWithoutConstraint(string TableName, string ClauseColumnName, int Value)
        {
            try
            {
                string strSql;
                StringBuilder strBr = new StringBuilder();
                strBr.Append("SET FOREIGN_KEY_CHECKS = 0;");
                strBr.Append("DELETE FROM ");
                strBr.Append(TableName);
                strBr.Append(" WHERE ");
                strBr.Append(ClauseColumnName);
                strBr.Append(" = '");
                strBr.Append(Value);
                strBr.Append("'");
                strSql = strBr.ToString();
                return strSql;
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
        public string GetUpdateQuery(string strTableName,
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
        //--------------------------------------------------------------------------------------------------       
        //Method Name       :Update
        //Description       :For Modification of the table
        //Parameters        :TableName and WhereClause
        //Return Type       :int
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public string Update(string TableName, string ColumnNames, string WhereClause)
        {
            try
            {
                string strSql;
                StringBuilder strBr = new StringBuilder();
                strBr.Append("UPDATE ");
                strBr.Append(TableName);
                strBr.Append(" ");
                strBr.Append("SET ");
                strBr.Append(ColumnNames);
                strBr.Append(" ");
                strBr.Append(WhereClause);
                strSql = strBr.ToString();
                return strSql;
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

        #region CommonExecuteQuery
        //--------------------------------------------------------------------------------------------------     
        //Method Name       :CommonExecuteQuery
        //Description       :For executing the Queries
        //Parameters        :SQL Query
        //Return Type       :MySqlCommand
        //Date              :2019/09/04 by Pranit Chimurkar
        //--------------------------------------------------------------------------------------------------
        public MySqlCommand CommonExecuteQuery(string strQuery)
        {
            try
            {
                GetConnection();
                OpenConnection();
                BeginTransaction();
                cmdCommon = new MySqlCommand(strQuery, connCommon);
                cmdCommon.ExecuteNonQuery();
                Commit();
                return cmdCommon;
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