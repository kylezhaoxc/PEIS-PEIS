using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace PEIS.DBUtility
{
	public abstract class DbHelperSQLite
	{
		public static string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;

		public DbHelperSQLite()
		{
		}

		public static int GetMaxID(string FieldName, string TableName)
		{
			string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
			object single = DbHelperSQLite.GetSingle(sQLString);
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
			object single = DbHelperSQLite.GetSingle(strSql);
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

		public static bool Exists(string strSql, params SQLiteParameter[] cmdParms)
		{
			object single = DbHelperSQLite.GetSingle(strSql, cmdParms);
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
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				using (SQLiteCommand sQLiteCommand = new SQLiteCommand(SQLString, sQLiteConnection))
				{
					try
					{
						sQLiteConnection.Open();
						int num = sQLiteCommand.ExecuteNonQuery();
						result = num;
					}
					catch (SQLiteException ex)
					{
						sQLiteConnection.Close();
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(ArrayList SQLStringList)
		{
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				sQLiteConnection.Open();
				SQLiteCommand sQLiteCommand = new SQLiteCommand();
				sQLiteCommand.Connection = sQLiteConnection;
				SQLiteTransaction sQLiteTransaction = sQLiteConnection.BeginTransaction();
				sQLiteCommand.Transaction = sQLiteTransaction;
				try
				{
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = SQLStringList[i].ToString();
						if (text.Trim().Length > 1)
						{
							sQLiteCommand.CommandText = text;
							sQLiteCommand.ExecuteNonQuery();
						}
					}
					sQLiteTransaction.Commit();
				}
				catch (SQLiteException ex)
				{
					sQLiteTransaction.Rollback();
					throw new Exception(ex.Message);
				}
			}
		}

		public static int ExecuteSql(string SQLString, string content)
		{
			int result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				SQLiteCommand sQLiteCommand = new SQLiteCommand(SQLString, sQLiteConnection);
				SQLiteParameter sQLiteParameter = new SQLiteParameter("@content", System.Data.DbType.String);
				sQLiteParameter.Value = content;
				sQLiteCommand.Parameters.Add(sQLiteParameter);
				try
				{
					sQLiteConnection.Open();
					int num = sQLiteCommand.ExecuteNonQuery();
					result = num;
				}
				catch (SQLiteException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					sQLiteCommand.Dispose();
					sQLiteConnection.Close();
				}
			}
			return result;
		}

		public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			int result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				SQLiteCommand sQLiteCommand = new SQLiteCommand(strSQL, sQLiteConnection);
				SQLiteParameter sQLiteParameter = new SQLiteParameter("@fs", System.Data.DbType.Binary);
				sQLiteParameter.Value = fs;
				sQLiteCommand.Parameters.Add(sQLiteParameter);
				try
				{
					sQLiteConnection.Open();
					int num = sQLiteCommand.ExecuteNonQuery();
					result = num;
				}
				catch (SQLiteException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					sQLiteCommand.Dispose();
					sQLiteConnection.Close();
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString)
		{
			object result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				using (SQLiteCommand sQLiteCommand = new SQLiteCommand(SQLString, sQLiteConnection))
				{
					try
					{
						sQLiteConnection.Open();
						object obj = sQLiteCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (SQLiteException ex)
					{
						sQLiteConnection.Close();
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static SQLiteDataReader ExecuteReader(string strSQL)
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString);
			SQLiteCommand sQLiteCommand = new SQLiteCommand(strSQL, sQLiteConnection);
			SQLiteDataReader result;
			try
			{
				sQLiteConnection.Open();
				SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				result = sQLiteDataReader;
			}
			catch (SQLiteException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString)
		{
			System.Data.DataSet result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					sQLiteConnection.Open();
					SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(SQLString, sQLiteConnection);
					sQLiteDataAdapter.Fill(dataSet, "ds");
				}
				catch (SQLiteException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, params SQLiteParameter[] cmdParms)
		{
			int result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				using (SQLiteCommand sQLiteCommand = new SQLiteCommand())
				{
					try
					{
						DbHelperSQLite.PrepareCommand(sQLiteCommand, sQLiteConnection, null, SQLString, cmdParms);
						int num = sQLiteCommand.ExecuteNonQuery();
						sQLiteCommand.Parameters.Clear();
						result = num;
					}
					catch (SQLiteException ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				sQLiteConnection.Open();
				using (SQLiteTransaction sQLiteTransaction = sQLiteConnection.BeginTransaction())
				{
					SQLiteCommand sQLiteCommand = new SQLiteCommand();
					try
					{
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							SQLiteParameter[] cmdParms = (SQLiteParameter[])dictionaryEntry.Value;
							DbHelperSQLite.PrepareCommand(sQLiteCommand, sQLiteConnection, sQLiteTransaction, cmdText, cmdParms);
							int num = sQLiteCommand.ExecuteNonQuery();
							sQLiteCommand.Parameters.Clear();
							sQLiteTransaction.Commit();
						}
					}
					catch
					{
						sQLiteTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static object GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
		{
			object result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				using (SQLiteCommand sQLiteCommand = new SQLiteCommand())
				{
					try
					{
						DbHelperSQLite.PrepareCommand(sQLiteCommand, sQLiteConnection, null, SQLString, cmdParms);
						object obj = sQLiteCommand.ExecuteScalar();
						sQLiteCommand.Parameters.Clear();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (SQLiteException ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
		{
			SQLiteConnection conn = new SQLiteConnection(DbHelperSQLite.connectionString);
			SQLiteCommand sQLiteCommand = new SQLiteCommand();
			SQLiteDataReader result;
			try
			{
				DbHelperSQLite.PrepareCommand(sQLiteCommand, conn, null, SQLString, cmdParms);
				SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				sQLiteCommand.Parameters.Clear();
				result = sQLiteDataReader;
			}
			catch (SQLiteException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, params SQLiteParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DbHelperSQLite.connectionString))
			{
				SQLiteCommand sQLiteCommand = new SQLiteCommand();
				DbHelperSQLite.PrepareCommand(sQLiteCommand, sQLiteConnection, null, SQLString, cmdParms);
				using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(sQLiteCommand))
				{
					System.Data.DataSet dataSet = new System.Data.DataSet();
					try
					{
						sQLiteDataAdapter.Fill(dataSet, "ds");
						sQLiteCommand.Parameters.Clear();
					}
					catch (SQLiteException ex)
					{
						throw new Exception(ex.Message);
					}
					result = dataSet;
				}
			}
			return result;
		}

		private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
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
					SQLiteParameter parameter = cmdParms[i];
					cmd.Parameters.Add(parameter);
				}
			}
		}
	}
}
