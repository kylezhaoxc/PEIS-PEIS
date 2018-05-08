using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace PEIS.DBUtility
{
	public abstract class DbHelperMySQL
	{
		public static string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;

		public DbHelperMySQL()
		{
		}

		public static int GetMaxID(string FieldName, string TableName)
		{
			string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
			object single = DbHelperMySQL.GetSingle(sQLString);
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
			object single = DbHelperMySQL.GetSingle(strSql);
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

		public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
		{
			object single = DbHelperMySQL.GetSingle(strSql, cmdParms);
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
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, mySqlConnection))
				{
					try
					{
						mySqlConnection.Open();
						int num = mySqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (MySqlException ex)
					{
						mySqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static int ExecuteSqlByTime(string SQLString, int Times)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, mySqlConnection))
				{
					try
					{
						mySqlConnection.Open();
						mySqlCommand.CommandTimeout = Times;
						int num = mySqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (MySqlException ex)
					{
						mySqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				mySqlConnection.Open();
				MySqlCommand mySqlCommand = new MySqlCommand();
				mySqlCommand.Connection = mySqlConnection;
				MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
				mySqlCommand.Transaction = mySqlTransaction;
				try
				{
					foreach (CommandInfo current in list)
					{
						string commandText = current.CommandText;
						MySqlParameter[] cmdParms = (MySqlParameter[])current.Parameters;
						DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, mySqlTransaction, commandText, cmdParms);
						if (current.EffentNextType == EffentNextType.SolicitationEvent)
						{
							if (current.CommandText.ToLower().IndexOf("count(") == -1)
							{
								mySqlTransaction.Rollback();
								throw new Exception("违背要求" + current.CommandText + "必须符合select count(..的格式");
							}
							object obj = mySqlCommand.ExecuteScalar();
							if (obj == null && obj == DBNull.Value)
							{
							}
							bool flag = Convert.ToInt32(obj) > 0;
							if (flag)
							{
								current.OnSolicitationEvent();
							}
						}
						if (current.EffentNextType == EffentNextType.WhenHaveContine || current.EffentNextType == EffentNextType.WhenNoHaveContine)
						{
							if (current.CommandText.ToLower().IndexOf("count(") == -1)
							{
								mySqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "必须符合select count(..的格式");
							}
							object obj = mySqlCommand.ExecuteScalar();
							if (obj == null && obj == DBNull.Value)
							{
							}
							bool flag = Convert.ToInt32(obj) > 0;
							if (current.EffentNextType == EffentNextType.WhenHaveContine && !flag)
							{
								mySqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "返回值必须大于0");
							}
							if (current.EffentNextType == EffentNextType.WhenNoHaveContine && flag)
							{
								mySqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "返回值必须等于0");
							}
						}
						else
						{
							int num = mySqlCommand.ExecuteNonQuery();
							if (current.EffentNextType == EffentNextType.ExcuteEffectRows && num == 0)
							{
								mySqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "必须有影响行");
							}
							mySqlCommand.Parameters.Clear();
						}
					}
					string conStr = PEIS.DBUtility.PubConstant.GetConnectionString("ConnectionStringPPC");
					if (!OracleHelper.ExecuteSqlTran(conStr, oracleCmdSqlList))
					{
						mySqlTransaction.Rollback();
						throw new Exception("执行失败");
					}
					mySqlTransaction.Commit();
					result = 1;
				}
				catch (MySqlException ex)
				{
					mySqlTransaction.Rollback();
					throw ex;
				}
				catch (Exception ex2)
				{
					mySqlTransaction.Rollback();
					throw ex2;
				}
			}
			return result;
		}

		public static int ExecuteSqlTran(List<string> SQLStringList)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				mySqlConnection.Open();
				MySqlCommand mySqlCommand = new MySqlCommand();
				mySqlCommand.Connection = mySqlConnection;
				MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
				mySqlCommand.Transaction = mySqlTransaction;
				try
				{
					int num = 0;
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = SQLStringList[i];
						if (text.Trim().Length > 1)
						{
							mySqlCommand.CommandText = text;
							num += mySqlCommand.ExecuteNonQuery();
						}
					}
					mySqlTransaction.Commit();
					result = num;
				}
				catch
				{
					mySqlTransaction.Rollback();
					result = 0;
				}
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, string content)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				MySqlCommand mySqlCommand = new MySqlCommand(SQLString, mySqlConnection);
				MySqlParameter mySqlParameter = new MySqlParameter("@content", System.Data.SqlDbType.NText);
				mySqlParameter.Value = content;
				mySqlCommand.Parameters.Add(mySqlParameter);
				try
				{
					mySqlConnection.Open();
					int num = mySqlCommand.ExecuteNonQuery();
					result = num;
				}
				catch (MySqlException ex)
				{
					throw ex;
				}
				finally
				{
					mySqlCommand.Dispose();
					mySqlConnection.Close();
				}
			}
			return result;
		}

		public static object ExecuteSqlGet(string SQLString, string content)
		{
			object result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				MySqlCommand mySqlCommand = new MySqlCommand(SQLString, mySqlConnection);
				MySqlParameter mySqlParameter = new MySqlParameter("@content", System.Data.SqlDbType.NText);
				mySqlParameter.Value = content;
				mySqlCommand.Parameters.Add(mySqlParameter);
				try
				{
					mySqlConnection.Open();
					object obj = mySqlCommand.ExecuteScalar();
					if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
					{
						result = null;
					}
					else
					{
						result = obj;
					}
				}
				catch (MySqlException ex)
				{
					throw ex;
				}
				finally
				{
					mySqlCommand.Dispose();
					mySqlConnection.Close();
				}
			}
			return result;
		}

		public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				MySqlCommand mySqlCommand = new MySqlCommand(strSQL, mySqlConnection);
				MySqlParameter mySqlParameter = new MySqlParameter("@fs", System.Data.SqlDbType.Image);
				mySqlParameter.Value = fs;
				mySqlCommand.Parameters.Add(mySqlParameter);
				try
				{
					mySqlConnection.Open();
					int num = mySqlCommand.ExecuteNonQuery();
					result = num;
				}
				catch (MySqlException ex)
				{
					throw ex;
				}
				finally
				{
					mySqlCommand.Dispose();
					mySqlConnection.Close();
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString)
		{
			object result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, mySqlConnection))
				{
					try
					{
						mySqlConnection.Open();
						object obj = mySqlCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (MySqlException ex)
					{
						mySqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString, int Times)
		{
			object result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, mySqlConnection))
				{
					try
					{
						mySqlConnection.Open();
						mySqlCommand.CommandTimeout = Times;
						object obj = mySqlCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (MySqlException ex)
					{
						mySqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static MySqlDataReader ExecuteReader(string strSQL)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString);
			MySqlCommand mySqlCommand = new MySqlCommand(strSQL, mySqlConnection);
			MySqlDataReader result;
			try
			{
				mySqlConnection.Open();
				MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				result = mySqlDataReader;
			}
			catch (MySqlException ex)
			{
				throw ex;
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString)
		{
			System.Data.DataSet result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					mySqlConnection.Open();
					MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(SQLString, mySqlConnection);
					mySqlDataAdapter.Fill(dataSet, "ds");
				}
				catch (MySqlException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, int Times)
		{
			System.Data.DataSet result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					mySqlConnection.Open();
					new MySqlDataAdapter(SQLString, mySqlConnection)
					{
						SelectCommand = 
						{
							CommandTimeout = Times
						}
					}.Fill(dataSet, "ds");
				}
				catch (MySqlException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				using (MySqlCommand mySqlCommand = new MySqlCommand())
				{
					try
					{
						DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, null, SQLString, cmdParms);
						int num = mySqlCommand.ExecuteNonQuery();
						mySqlCommand.Parameters.Clear();
						result = num;
					}
					catch (MySqlException ex)
					{
						throw ex;
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				mySqlConnection.Open();
				using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction())
				{
					MySqlCommand mySqlCommand = new MySqlCommand();
					try
					{
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							MySqlParameter[] cmdParms = (MySqlParameter[])dictionaryEntry.Value;
							DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, mySqlTransaction, cmdText, cmdParms);
							int num = mySqlCommand.ExecuteNonQuery();
							mySqlCommand.Parameters.Clear();
						}
						mySqlTransaction.Commit();
					}
					catch
					{
						mySqlTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static int ExecuteSqlTran(List<CommandInfo> cmdList)
		{
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				mySqlConnection.Open();
				using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction())
				{
					MySqlCommand mySqlCommand = new MySqlCommand();
					try
					{
						int num = 0;
						foreach (CommandInfo current in cmdList)
						{
							string commandText = current.CommandText;
							MySqlParameter[] cmdParms = (MySqlParameter[])current.Parameters;
							DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, mySqlTransaction, commandText, cmdParms);
							if (current.EffentNextType == EffentNextType.WhenHaveContine || current.EffentNextType == EffentNextType.WhenNoHaveContine)
							{
								if (current.CommandText.ToLower().IndexOf("count(") == -1)
								{
									mySqlTransaction.Rollback();
									result = 0;
									return result;
								}
								object obj = mySqlCommand.ExecuteScalar();
								if (obj == null && obj == DBNull.Value)
								{
								}
								bool flag = Convert.ToInt32(obj) > 0;
								if (current.EffentNextType == EffentNextType.WhenHaveContine && !flag)
								{
									mySqlTransaction.Rollback();
									result = 0;
									return result;
								}
								if (current.EffentNextType == EffentNextType.WhenNoHaveContine && flag)
								{
									mySqlTransaction.Rollback();
									result = 0;
									return result;
								}
							}
							else
							{
								int num2 = mySqlCommand.ExecuteNonQuery();
								num += num2;
								if (current.EffentNextType == EffentNextType.ExcuteEffectRows && num2 == 0)
								{
									mySqlTransaction.Rollback();
									result = 0;
									return result;
								}
								mySqlCommand.Parameters.Clear();
							}
						}
						mySqlTransaction.Commit();
						result = num;
					}
					catch
					{
						mySqlTransaction.Rollback();
						throw;
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
		{
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				mySqlConnection.Open();
				using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction())
				{
					MySqlCommand mySqlCommand = new MySqlCommand();
					try
					{
						int num = 0;
						foreach (CommandInfo current in SQLStringList)
						{
							string commandText = current.CommandText;
							MySqlParameter[] array = (MySqlParameter[])current.Parameters;
							MySqlParameter[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								MySqlParameter mySqlParameter = array2[i];
								if (mySqlParameter.Direction == System.Data.ParameterDirection.InputOutput)
								{
									mySqlParameter.Value = num;
								}
							}
							DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, mySqlTransaction, commandText, array);
							int num2 = mySqlCommand.ExecuteNonQuery();
							array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								MySqlParameter mySqlParameter = array2[i];
								if (mySqlParameter.Direction == System.Data.ParameterDirection.Output)
								{
									num = Convert.ToInt32(mySqlParameter.Value);
								}
							}
							mySqlCommand.Parameters.Clear();
						}
						mySqlTransaction.Commit();
					}
					catch
					{
						mySqlTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
		{
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				mySqlConnection.Open();
				using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction())
				{
					MySqlCommand mySqlCommand = new MySqlCommand();
					try
					{
						int num = 0;
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							MySqlParameter[] array = (MySqlParameter[])dictionaryEntry.Value;
							MySqlParameter[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								MySqlParameter mySqlParameter = array2[i];
								if (mySqlParameter.Direction == System.Data.ParameterDirection.InputOutput)
								{
									mySqlParameter.Value = num;
								}
							}
							DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, mySqlTransaction, cmdText, array);
							int num2 = mySqlCommand.ExecuteNonQuery();
							array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								MySqlParameter mySqlParameter = array2[i];
								if (mySqlParameter.Direction == System.Data.ParameterDirection.Output)
								{
									num = Convert.ToInt32(mySqlParameter.Value);
								}
							}
							mySqlCommand.Parameters.Clear();
						}
						mySqlTransaction.Commit();
					}
					catch
					{
						mySqlTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
		{
			object result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				using (MySqlCommand mySqlCommand = new MySqlCommand())
				{
					try
					{
						DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, null, SQLString, cmdParms);
						object obj = mySqlCommand.ExecuteScalar();
						mySqlCommand.Parameters.Clear();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (MySqlException ex)
					{
						throw ex;
					}
				}
			}
			return result;
		}

		public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms)
		{
			MySqlConnection conn = new MySqlConnection(DbHelperMySQL.connectionString);
			MySqlCommand mySqlCommand = new MySqlCommand();
			MySqlDataReader result;
			try
			{
				DbHelperMySQL.PrepareCommand(mySqlCommand, conn, null, SQLString, cmdParms);
				MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				mySqlCommand.Parameters.Clear();
				result = mySqlDataReader;
			}
			catch (MySqlException ex)
			{
				throw ex;
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(DbHelperMySQL.connectionString))
			{
				MySqlCommand mySqlCommand = new MySqlCommand();
				DbHelperMySQL.PrepareCommand(mySqlCommand, mySqlConnection, null, SQLString, cmdParms);
				using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand))
				{
					System.Data.DataSet dataSet = new System.Data.DataSet();
					try
					{
						mySqlDataAdapter.Fill(dataSet, "ds");
						mySqlCommand.Parameters.Clear();
					}
					catch (MySqlException ex)
					{
						throw new Exception(ex.Message);
					}
					result = dataSet;
				}
			}
			return result;
		}

		private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
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
					MySqlParameter mySqlParameter = cmdParms[i];
					if ((mySqlParameter.Direction == System.Data.ParameterDirection.InputOutput || mySqlParameter.Direction == System.Data.ParameterDirection.Input) && mySqlParameter.Value == null)
					{
						mySqlParameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(mySqlParameter);
				}
			}
		}
	}
}
