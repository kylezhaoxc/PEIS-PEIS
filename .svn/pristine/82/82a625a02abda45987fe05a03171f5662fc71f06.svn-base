using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace PEIS.DBUtility
{
	public abstract class DbHelperOleDb
	{
		public static string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;

		public DbHelperOleDb()
		{
		}

		public static int GetMaxID(string FieldName, string TableName)
		{
			string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
			object single = DbHelperSQL.GetSingle(sQLString);
			int result;
			if (single == null)
			{
				result = 1;
			}
			else
			{
				result = int.Parse(single.ToString());
			}
			return result;
		}

		public static bool Exists(string strSql)
		{
			object single = DbHelperSQL.GetSingle(strSql);
			int num;
			if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
			{
				num = 0;
			}
			else
			{
				num = int.Parse(single.ToString());
			}
			return num != 0;
		}

		public static bool Exists(string strSql, params System.Data.OleDb.OleDbParameter[] cmdParms)
		{
			object single = DbHelperOleDb.GetSingle(strSql, cmdParms);
			int num;
			if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
			{
				num = 0;
			}
			else
			{
				num = int.Parse(single.ToString());
			}
			return num != 0;
		}

		public static int ExecuteSql(string SQLString)
		{
			int result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				using (System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, oleDbConnection))
				{
					try
					{
						oleDbConnection.Open();
						int num = oleDbCommand.ExecuteNonQuery();
						result = num;
					}
					catch (System.Data.OleDb.OleDbException ex)
					{
						oleDbConnection.Close();
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(ArrayList SQLStringList)
		{
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				oleDbConnection.Open();
				System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand();
				oleDbCommand.Connection = oleDbConnection;
				System.Data.OleDb.OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction();
				oleDbCommand.Transaction = oleDbTransaction;
				try
				{
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = SQLStringList[i].ToString();
						if (text.Trim().Length > 1)
						{
							oleDbCommand.CommandText = text;
							oleDbCommand.ExecuteNonQuery();
						}
					}
					oleDbTransaction.Commit();
				}
				catch (System.Data.OleDb.OleDbException ex)
				{
					oleDbTransaction.Rollback();
					throw new Exception(ex.Message);
				}
			}
		}

		public static int ExecuteSql(string SQLString, string content)
		{
			int result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, oleDbConnection);
				System.Data.OleDb.OleDbParameter oleDbParameter = new System.Data.OleDb.OleDbParameter("@content", System.Data.OleDb.OleDbType.VarChar);
				oleDbParameter.Value = content;
				oleDbCommand.Parameters.Add(oleDbParameter);
				try
				{
					oleDbConnection.Open();
					int num = oleDbCommand.ExecuteNonQuery();
					result = num;
				}
				catch (System.Data.OleDb.OleDbException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					oleDbCommand.Dispose();
					oleDbConnection.Close();
				}
			}
			return result;
		}

		public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			int result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand(strSQL, oleDbConnection);
				System.Data.OleDb.OleDbParameter oleDbParameter = new System.Data.OleDb.OleDbParameter("@fs", System.Data.OleDb.OleDbType.Binary);
				oleDbParameter.Value = fs;
				oleDbCommand.Parameters.Add(oleDbParameter);
				try
				{
					oleDbConnection.Open();
					int num = oleDbCommand.ExecuteNonQuery();
					result = num;
				}
				catch (System.Data.OleDb.OleDbException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					oleDbCommand.Dispose();
					oleDbConnection.Close();
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString)
		{
			object result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				using (System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand(SQLString, oleDbConnection))
				{
					try
					{
						oleDbConnection.Open();
						object obj = oleDbCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (System.Data.OleDb.OleDbException ex)
					{
						oleDbConnection.Close();
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static System.Data.OleDb.OleDbDataReader ExecuteReader(string strSQL)
		{
			System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString);
			System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand(strSQL, oleDbConnection);
			System.Data.OleDb.OleDbDataReader result;
			try
			{
				oleDbConnection.Open();
				System.Data.OleDb.OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				result = oleDbDataReader;
			}
			catch (System.Data.OleDb.OleDbException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString)
		{
			System.Data.DataSet result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					oleDbConnection.Open();
					System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(SQLString, oleDbConnection);
					oleDbDataAdapter.Fill(dataSet, "ds");
				}
				catch (System.Data.OleDb.OleDbException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, params System.Data.OleDb.OleDbParameter[] cmdParms)
		{
			int result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				using (System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand())
				{
					try
					{
						DbHelperOleDb.PrepareCommand(oleDbCommand, oleDbConnection, null, SQLString, cmdParms);
						int num = oleDbCommand.ExecuteNonQuery();
						oleDbCommand.Parameters.Clear();
						result = num;
					}
					catch (System.Data.OleDb.OleDbException ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				oleDbConnection.Open();
				using (System.Data.OleDb.OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction())
				{
					System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand();
					try
					{
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							System.Data.OleDb.OleDbParameter[] cmdParms = (System.Data.OleDb.OleDbParameter[])dictionaryEntry.Value;
							DbHelperOleDb.PrepareCommand(oleDbCommand, oleDbConnection, oleDbTransaction, cmdText, cmdParms);
							int num = oleDbCommand.ExecuteNonQuery();
							oleDbCommand.Parameters.Clear();
							oleDbTransaction.Commit();
						}
					}
					catch
					{
						oleDbTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static object GetSingle(string SQLString, params System.Data.OleDb.OleDbParameter[] cmdParms)
		{
			object result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				using (System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand())
				{
					try
					{
						DbHelperOleDb.PrepareCommand(oleDbCommand, oleDbConnection, null, SQLString, cmdParms);
						object obj = oleDbCommand.ExecuteScalar();
						oleDbCommand.Parameters.Clear();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (System.Data.OleDb.OleDbException ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static System.Data.OleDb.OleDbDataReader ExecuteReader(string SQLString, params System.Data.OleDb.OleDbParameter[] cmdParms)
		{
			System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString);
			System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand();
			System.Data.OleDb.OleDbDataReader result;
			try
			{
				DbHelperOleDb.PrepareCommand(oleDbCommand, conn, null, SQLString, cmdParms);
				System.Data.OleDb.OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				oleDbCommand.Parameters.Clear();
				result = oleDbDataReader;
			}
			catch (System.Data.OleDb.OleDbException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, params System.Data.OleDb.OleDbParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (System.Data.OleDb.OleDbConnection oleDbConnection = new System.Data.OleDb.OleDbConnection(DbHelperOleDb.connectionString))
			{
				System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand();
				DbHelperOleDb.PrepareCommand(oleDbCommand, oleDbConnection, null, SQLString, cmdParms);
				using (System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand))
				{
					System.Data.DataSet dataSet = new System.Data.DataSet();
					try
					{
						oleDbDataAdapter.Fill(dataSet, "ds");
						oleDbCommand.Parameters.Clear();
					}
					catch (System.Data.OleDb.OleDbException ex)
					{
						throw new Exception(ex.Message);
					}
					result = dataSet;
				}
			}
			return result;
		}

		private static void PrepareCommand(System.Data.OleDb.OleDbCommand cmd, System.Data.OleDb.OleDbConnection conn, System.Data.OleDb.OleDbTransaction trans, string cmdText, System.Data.OleDb.OleDbParameter[] cmdParms)
		{
			if (conn.State != System.Data.ConnectionState.Open)
			{
				conn.Open();
			}
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
			{
				cmd.Transaction = trans;
			}
			cmd.CommandType = System.Data.CommandType.Text;
			if (cmdParms != null)
			{
				for (int i = 0; i < cmdParms.Length; i++)
				{
					System.Data.OleDb.OleDbParameter value = cmdParms[i];
					cmd.Parameters.Add(value);
				}
			}
		}
	}
}
