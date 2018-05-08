using PEIS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PEIS.DBUtility
{
	public abstract class DbHelperSQL
	{
		public static string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;

		public static int Timeout = 30;

		public DbHelperSQL()
		{
		}

		public static void SetTimeoutDefault()
		{
			DbHelperSQL.Timeout = 30;
		}

		public static bool ColumnExists(string tableName, string columnName)
		{
			string sQLString = string.Concat(new string[]
			{
				"select count(1) from syscolumns where [id]=object_id('",
				tableName,
				"') and [name]='",
				columnName,
				"'"
			});
			object single = DbHelperSQL.GetSingle(sQLString);
			return single != null && Convert.ToInt32(single) > 0;
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

		public static bool TabExists(string TableName)
		{
			string sQLString = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
			object single = DbHelperSQL.GetSingle(sQLString);
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

		public static bool Exists(string strSql, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			object single = DbHelperSQL.GetSingle(strSql, cmdParms);
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
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						int num = sqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						sqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static int ExecuteSql_Ex(string currConnectionString, string SQLString)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(currConnectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						int num = sqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						sqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static int ExecuteSqlByTime(string SQLString, int Times)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						sqlCommand.CommandTimeout = Times;
						int num = sqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						sqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
				sqlCommand.Connection = sqlConnection;
				System.Data.SqlClient.SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				sqlCommand.Transaction = sqlTransaction;
				try
				{
					foreach (CommandInfo current in list)
					{
						string commandText = current.CommandText;
						System.Data.SqlClient.SqlParameter[] cmdParms = (System.Data.SqlClient.SqlParameter[])current.Parameters;
						DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, commandText, cmdParms);
						if (current.EffentNextType == EffentNextType.SolicitationEvent)
						{
							if (current.CommandText.ToLower().IndexOf("count(") == -1)
							{
								sqlTransaction.Rollback();
								throw new Exception("违背要求" + current.CommandText + "必须符合select count(..的格式");
							}
							object obj = sqlCommand.ExecuteScalar();
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
								sqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "必须符合select count(..的格式");
							}
							object obj = sqlCommand.ExecuteScalar();
							if (obj == null && obj == DBNull.Value)
							{
							}
							bool flag = Convert.ToInt32(obj) > 0;
							if (current.EffentNextType == EffentNextType.WhenHaveContine && !flag)
							{
								sqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "返回值必须大于0");
							}
							if (current.EffentNextType == EffentNextType.WhenNoHaveContine && flag)
							{
								sqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "返回值必须等于0");
							}
						}
						else
						{
							int num = sqlCommand.ExecuteNonQuery();
							if (current.EffentNextType == EffentNextType.ExcuteEffectRows && num == 0)
							{
								sqlTransaction.Rollback();
								throw new Exception("SQL:违背要求" + current.CommandText + "必须有影响行");
							}
							sqlCommand.Parameters.Clear();
						}
					}
					string conStr = PEIS.DBUtility.PubConstant.GetConnectionString("ConnectionStringPPC");
					if (!OracleHelper.ExecuteSqlTran(conStr, oracleCmdSqlList))
					{
						sqlTransaction.Rollback();
						throw new Exception("Oracle执行失败");
					}
					sqlTransaction.Commit();
					result = 1;
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					sqlTransaction.Rollback();
					throw ex;
				}
				catch (Exception ex2)
				{
					sqlTransaction.Rollback();
					throw ex2;
				}
			}
			return result;
		}

		public static int ExecuteSqlTran(List<string> SQLStringList)
		{
			DateTime now = DateTime.Now;
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
				sqlCommand.Connection = sqlConnection;
				System.Data.SqlClient.SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				sqlCommand.Transaction = sqlTransaction;
				try
				{
					int num = 0;
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = SQLStringList[i];
						if (text.Trim().Length > 1)
						{
							sqlCommand.CommandText = text;
							num += sqlCommand.ExecuteNonQuery();
						}
					}
					sqlTransaction.Commit();
					string dateDiff = Public.GetDateDiff("采用事务的方式，执行多条语句", now, DateTime.Now);
					Log4J.Instance.Error(dateDiff);
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = SQLStringList[i];
						if (text.Trim().Length > 1)
						{
							Log4J.Instance.Debug(string.Concat(new string[]
							{
								dateDiff,
								",本次事务执行的语句【",
								(i + 1).ToString(),
								"】：",
								Secret.AES.Encrypt(text)
							}));
						}
					}
					result = num;
				}
				catch (Exception ex)
				{
					sqlTransaction.Rollback();
					throw ex;
				}
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, string content)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection);
				System.Data.SqlClient.SqlParameter sqlParameter = new System.Data.SqlClient.SqlParameter("@content", System.Data.SqlDbType.NText);
				sqlParameter.Value = content;
				sqlCommand.Parameters.Add(sqlParameter);
				try
				{
					sqlConnection.Open();
					int num = sqlCommand.ExecuteNonQuery();
					result = num;
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					throw ex;
				}
				finally
				{
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static object ExecuteSqlGet(string SQLString, string content)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection);
				System.Data.SqlClient.SqlParameter sqlParameter = new System.Data.SqlClient.SqlParameter("@content", System.Data.SqlDbType.NText);
				sqlParameter.Value = content;
				sqlCommand.Parameters.Add(sqlParameter);
				try
				{
					sqlConnection.Open();
					object obj = sqlCommand.ExecuteScalar();
					if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
					{
						result = null;
					}
					else
					{
						result = obj;
					}
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					throw ex;
				}
				finally
				{
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(strSQL, sqlConnection);
				System.Data.SqlClient.SqlParameter sqlParameter = new System.Data.SqlClient.SqlParameter("@fs", System.Data.SqlDbType.Image);
				sqlParameter.Value = fs;
				sqlCommand.Parameters.Add(sqlParameter);
				try
				{
					sqlConnection.Open();
					int num = sqlCommand.ExecuteNonQuery();
					result = num;
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					throw ex;
				}
				finally
				{
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						object obj = sqlCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						sqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString, int Times)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						sqlCommand.CommandTimeout = Times;
						object obj = sqlCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						sqlConnection.Close();
						throw ex;
					}
				}
			}
			return result;
		}

		public static System.Data.SqlClient.SqlDataReader ExecuteReader(string strSQL)
		{
			System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString);
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(strSQL, sqlConnection);
			System.Data.SqlClient.SqlDataReader result;
			try
			{
				sqlConnection.Open();
				System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				result = sqlDataReader;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				throw ex;
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, int? TimeOut = null)
		{
			DateTime now = DateTime.Now;
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					sqlConnection.Open();
					System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(SQLString, sqlConnection);
					if (TimeOut.HasValue)
					{
						sqlDataAdapter.SelectCommand.CommandTimeout = TimeOut.Value;
					}
					sqlDataAdapter.Fill(dataSet, "ds");
					dataSet = Secret.AES.Decrypt(dataSet);
					string dateDiff = Public.GetDateDiff("执行查询语句，返回DataSet", now, DateTime.Now);
					Log4J.Instance.Debug(dateDiff + " ,SQL语句: " + Secret.AES.Encrypt(SQLString));
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					string dateDiff = Public.GetDateDiff("出错了，执行查询语句，返回DataSet", now, DateTime.Now);
					Log4J.Instance.Debug(string.Concat(new string[]
					{
						dateDiff,
						" ,SQL语句: ",
						Secret.AES.Encrypt(SQLString),
						",错误Message:",
						ex.Message
					}));
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static System.Data.DataSet Query(string currConnectionString, string SQLString, int? TimeOut = null)
		{
			DateTime now = DateTime.Now;
			if (string.IsNullOrEmpty(currConnectionString))
			{
				currConnectionString = DbHelperSQL.connectionString;
			}
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(currConnectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					sqlConnection.Open();
					System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(SQLString, sqlConnection);
					if (TimeOut.HasValue)
					{
						sqlDataAdapter.SelectCommand.CommandTimeout = TimeOut.Value;
					}
					sqlDataAdapter.Fill(dataSet, "ds");
					dataSet = Secret.AES.Decrypt(dataSet);
					string dateDiff = Public.GetDateDiff("执行查询语句，返回DataSet", now, DateTime.Now);
					Log4J.Instance.Debug(dateDiff + " ,SQL语句: " + Secret.AES.Encrypt(SQLString));
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					string dateDiff = Public.GetDateDiff("出错了，执行查询语句，返回DataSet", now, DateTime.Now);
					Log4J.Instance.Debug(string.Concat(new string[]
					{
						dateDiff,
						" ,SQL语句: ",
						Secret.AES.Encrypt(SQLString),
						",错误Message:",
						ex.Message
					}));
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, int Times)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					sqlConnection.Open();
					new System.Data.SqlClient.SqlDataAdapter(SQLString, sqlConnection)
					{
						SelectCommand = 
						{
							CommandTimeout = Times
						}
					}.Fill(dataSet, "ds");
					dataSet = Secret.AES.Decrypt(dataSet);
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			DateTime now = DateTime.Now;
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand())
				{
					try
					{
						DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
						int num = sqlCommand.ExecuteNonQuery();
						sqlCommand.Parameters.Clear();
						string text = "";
						if (cmdParms != null)
						{
							for (int i = 0; i < cmdParms.Length; i++)
							{
								System.Data.SqlClient.SqlParameter sqlParameter = cmdParms[i];
								if (sqlParameter.Value != null && sqlParameter.ParameterName != null)
								{
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										sqlParameter.ParameterName,
										":",
										sqlParameter.Value,
										","
									});
								}
							}
						}
						string dateDiff = Public.GetDateDiff("执行SQL语句，返回影响的记录数", now, DateTime.Now);
						Log4J.Instance.Debug(string.Concat(new string[]
						{
							dateDiff,
							" ,SQL语句: ",
							Secret.AES.Encrypt(SQLString),
							",参数：",
							Secret.AES.Encrypt(text)
						}));
						result = num;
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						throw ex;
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (System.Data.SqlClient.SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
					try
					{
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							System.Data.SqlClient.SqlParameter[] cmdParms = (System.Data.SqlClient.SqlParameter[])dictionaryEntry.Value;
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, cmdParms);
							int num = sqlCommand.ExecuteNonQuery();
							sqlCommand.Parameters.Clear();
						}
						sqlTransaction.Commit();
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static int ExecuteSqlTran(List<CommandInfo> cmdList)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (System.Data.SqlClient.SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
					try
					{
						int num = 0;
						foreach (CommandInfo current in cmdList)
						{
							string commandText = current.CommandText;
							System.Data.SqlClient.SqlParameter[] cmdParms = (System.Data.SqlClient.SqlParameter[])current.Parameters;
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, commandText, cmdParms);
							if (current.EffentNextType == EffentNextType.WhenHaveContine || current.EffentNextType == EffentNextType.WhenNoHaveContine)
							{
								if (current.CommandText.ToLower().IndexOf("count(") == -1)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
								object obj = sqlCommand.ExecuteScalar();
								if (obj == null && obj == DBNull.Value)
								{
								}
								bool flag = Convert.ToInt32(obj) > 0;
								if (current.EffentNextType == EffentNextType.WhenHaveContine && !flag)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
								if (current.EffentNextType == EffentNextType.WhenNoHaveContine && flag)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
							}
							else
							{
								int num2 = sqlCommand.ExecuteNonQuery();
								num += num2;
								if (current.EffentNextType == EffentNextType.ExcuteEffectRows && num2 == 0)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
								sqlCommand.Parameters.Clear();
							}
						}
						sqlTransaction.Commit();
						result = num;
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
		{
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (System.Data.SqlClient.SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
					try
					{
						int num = 0;
						foreach (CommandInfo current in SQLStringList)
						{
							string commandText = current.CommandText;
							System.Data.SqlClient.SqlParameter[] array = (System.Data.SqlClient.SqlParameter[])current.Parameters;
							System.Data.SqlClient.SqlParameter[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								System.Data.SqlClient.SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == System.Data.ParameterDirection.InputOutput)
								{
									sqlParameter.Value = num;
								}
							}
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, commandText, array);
							int num2 = sqlCommand.ExecuteNonQuery();
							array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								System.Data.SqlClient.SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == System.Data.ParameterDirection.Output)
								{
									num = Convert.ToInt32(sqlParameter.Value);
								}
							}
							sqlCommand.Parameters.Clear();
						}
						sqlTransaction.Commit();
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
		{
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (System.Data.SqlClient.SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
					try
					{
						int num = 0;
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							System.Data.SqlClient.SqlParameter[] array = (System.Data.SqlClient.SqlParameter[])dictionaryEntry.Value;
							System.Data.SqlClient.SqlParameter[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								System.Data.SqlClient.SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == System.Data.ParameterDirection.InputOutput)
								{
									sqlParameter.Value = num;
								}
							}
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, array);
							int num2 = sqlCommand.ExecuteNonQuery();
							array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								System.Data.SqlClient.SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == System.Data.ParameterDirection.Output)
								{
									num = Convert.ToInt32(sqlParameter.Value);
								}
							}
							sqlCommand.Parameters.Clear();
						}
						sqlTransaction.Commit();
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static object GetSingle(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand())
				{
					try
					{
						DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
						object obj = sqlCommand.ExecuteScalar();
						sqlCommand.Parameters.Clear();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						throw ex;
					}
				}
			}
			return result;
		}

		public static System.Data.SqlClient.SqlDataReader ExecuteReader(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString);
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			System.Data.SqlClient.SqlDataReader result;
			try
			{
				DbHelperSQL.PrepareCommand(sqlCommand, conn, null, SQLString, cmdParms);
				System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				sqlCommand.Parameters.Clear();
				result = sqlDataReader;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				throw ex;
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
				DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
				using (System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand))
				{
					System.Data.DataSet dataSet = new System.Data.DataSet();
					try
					{
						sqlDataAdapter.Fill(dataSet, "ds");
						sqlCommand.Parameters.Clear();
						dataSet = Secret.AES.Decrypt(dataSet);
					}
					catch (System.Data.SqlClient.SqlException ex)
					{
						throw new Exception(ex.Message);
					}
					result = dataSet;
				}
			}
			return result;
		}

		public static System.Data.DataSet Query(string currConnectionString, string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			System.Data.DataSet result;
			if (string.IsNullOrEmpty(currConnectionString))
			{
				result = null;
			}
			else
			{
				using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(currConnectionString))
				{
					System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
					DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
					using (System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand))
					{
						System.Data.DataSet dataSet = new System.Data.DataSet();
						try
						{
							sqlDataAdapter.Fill(dataSet, "ds");
							sqlCommand.Parameters.Clear();
							dataSet = Secret.AES.Decrypt(dataSet);
						}
						catch (System.Data.SqlClient.SqlException ex)
						{
							throw new Exception(ex.Message);
						}
						result = dataSet;
					}
				}
			}
			return result;
		}

		private static void PrepareCommand(System.Data.SqlClient.SqlCommand cmd, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans, string cmdText, System.Data.SqlClient.SqlParameter[] cmdParms)
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
					System.Data.SqlClient.SqlParameter sqlParameter = cmdParms[i];
					if ((sqlParameter.Direction == System.Data.ParameterDirection.InputOutput || sqlParameter.Direction == System.Data.ParameterDirection.Input) && sqlParameter.Value == null)
					{
						sqlParameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(sqlParameter);
				}
			}
		}

		public static System.Data.SqlClient.SqlDataReader RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters)
		{
			System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString);
			sqlConnection.Open();
			System.Data.SqlClient.SqlCommand sqlCommand = DbHelperSQL.BuildQueryCommand(sqlConnection, storedProcName, parameters);
			sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
			return sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}

		public static System.Data.DataSet RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, string tableName)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				sqlConnection.Open();
				new System.Data.SqlClient.SqlDataAdapter
				{
					SelectCommand = DbHelperSQL.BuildQueryCommand(sqlConnection, storedProcName, parameters)
				}.Fill(dataSet, tableName);
				sqlConnection.Close();
				dataSet = Secret.AES.Decrypt(dataSet);
				result = dataSet;
			}
			return result;
		}

		public static System.Data.DataSet RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, string tableName, int Times)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				sqlConnection.Open();
				new System.Data.SqlClient.SqlDataAdapter
				{
					//SelectCommand = DbHelperSQL.BuildQueryCommand(sqlConnection, storedProcName, parameters),
					SelectCommand = 
					{
						CommandTimeout = Times
					}
				}.Fill(dataSet, tableName);
				sqlConnection.Close();
				dataSet = Secret.AES.Decrypt(dataSet);
				result = dataSet;
			}
			return result;
		}

		private static System.Data.SqlClient.SqlCommand BuildQueryCommand(System.Data.SqlClient.SqlConnection connection, string storedProcName, System.Data.IDataParameter[] parameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(storedProcName, connection);
			sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
			for (int i = 0; i < parameters.Length; i++)
			{
				System.Data.SqlClient.SqlParameter sqlParameter = (System.Data.SqlClient.SqlParameter)parameters[i];
				if (sqlParameter != null)
				{
					if ((sqlParameter.Direction == System.Data.ParameterDirection.InputOutput || sqlParameter.Direction == System.Data.ParameterDirection.Input) && sqlParameter.Value == null)
					{
						sqlParameter.Value = DBNull.Value;
					}
					sqlCommand.Parameters.Add(sqlParameter);
				}
			}
			return sqlCommand;
		}

		public static int RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, out int rowsAffected)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				System.Data.SqlClient.SqlCommand sqlCommand = DbHelperSQL.BuildIntCommand(sqlConnection, storedProcName, parameters);
				rowsAffected = sqlCommand.ExecuteNonQuery();
				int num = (int)sqlCommand.Parameters["ReturnValue"].Value;
				result = num;
			}
			return result;
		}

		private static System.Data.SqlClient.SqlCommand BuildIntCommand(System.Data.SqlClient.SqlConnection connection, string storedProcName, System.Data.IDataParameter[] parameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = DbHelperSQL.BuildQueryCommand(connection, storedProcName, parameters);
			sqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("ReturnValue", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null));
			return sqlCommand;
		}

		public static System.Data.DataTable ExecuteTable(System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
		{
			return DbHelperSQL.ExecuteTable(DbHelperSQL.connectionString, cmdType, cmdText, commandParameters);
		}

		public static System.Data.DataTable ExecuteTable(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
		{
			DateTime now = DateTime.Now;
			System.Data.Common.DbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
			System.Data.DataTable result;
			using (System.Data.Common.DbConnection dbConnection = new System.Data.SqlClient.SqlConnection())
			{
				dbConnection.ConnectionString = connectionString;
				DbHelperSQL.PrepareCommand(dbCommand, dbConnection, null, cmdType, cmdText, commandParameters);
				System.Data.Common.DbDataAdapter dbDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
				dbDataAdapter.SelectCommand = dbCommand;
				System.Data.DataSet dataSet = new System.Data.DataSet();
				dbDataAdapter.Fill(dataSet, "Result");
				dbCommand.Parameters.Clear();
				dataSet = Secret.AES.Decrypt(dataSet);
				string dateDiff = Public.GetDateDiff("执行查询语句", now, DateTime.Now);
				Log4J.Instance.Debug(dateDiff + " ,SQL语句: " + Secret.AES.Encrypt(cmdText));
				result = dataSet.Tables["Result"];
			}
			return result;
		}

		public static System.Data.DataTable ExecuteTable(System.Data.Common.DbConnection connection, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
		{
			System.Data.Common.DbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
			DbHelperSQL.PrepareCommand(dbCommand, connection, null, cmdType, cmdText, commandParameters);
			System.Data.Common.DbDataAdapter dbDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCommand;
			System.Data.DataSet dataSet = new System.Data.DataSet();
			dbDataAdapter.Fill(dataSet, "Result");
			dbCommand.Parameters.Clear();
			dataSet = Secret.AES.Decrypt(dataSet);
			return dataSet.Tables["Result"];
		}

		public static System.Data.DataTableCollection ExecuteTables(System.Data.Common.DbConnection connection, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
		{
			System.Data.Common.DbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
			DbHelperSQL.PrepareCommand(dbCommand, connection, null, cmdType, cmdText, commandParameters);
			System.Data.Common.DbDataAdapter dbDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCommand;
			System.Data.DataSet dataSet = new System.Data.DataSet();
			dbDataAdapter.Fill(dataSet, "Result");
			dbCommand.Parameters.Clear();
			dataSet = Secret.AES.Decrypt(dataSet);
			return dataSet.Tables;
		}

		public static System.Data.DataTable ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params System.Data.Common.DbParameter[] commandParameters)
		{
			return DbHelperSQL.ExecutePage(DbHelperSQL.connectionString, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
		}

		public static System.Data.DataTable ExecutePage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params System.Data.Common.DbParameter[] commandParameters)
		{
			System.Data.DataTable result;
			using (System.Data.Common.DbConnection dbConnection = new System.Data.SqlClient.SqlConnection())
			{
				dbConnection.ConnectionString = connectionString;
				dbConnection.Open();
				result = DbHelperSQL.ExecutePage(dbConnection, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
			}
			return result;
		}

		public static System.Data.DataTable ExecutePage(System.Data.Common.DbConnection connection, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params System.Data.Common.DbParameter[] commandParameters)
		{
			System.Data.Common.DbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
			DbHelperSQL.PrepareCommand(dbCommand, connection, null, System.Data.CommandType.Text, "", commandParameters);
			string pageSql = DbHelperSQL.GetPageSql(connection, dbCommand, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount);
			dbCommand.CommandText = pageSql;
			System.Data.Common.DbDataAdapter dbDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCommand;
			System.Data.DataSet dataSet = new System.Data.DataSet();
			dbDataAdapter.Fill(dataSet, "PageResult");
			dbCommand.Parameters.Clear();
			dataSet = Secret.AES.Decrypt(dataSet);
			return dataSet.Tables["PageResult"];
		}

		private static string GetPageSql(System.Data.Common.DbConnection connection, System.Data.Common.DbCommand cmd, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
		{
			RecordCount = 0;
			PageCount = 0;
			if (PageSize <= 0)
			{
				PageSize = 10;
			}
			string commandText = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
			cmd.CommandText = commandText;
			RecordCount = (int)cmd.ExecuteScalar();
			if (RecordCount % PageSize == 0)
			{
				PageCount = RecordCount / PageSize;
			}
			else
			{
				PageCount = RecordCount / PageSize + 1;
			}
			if (PageIndex > PageCount)
			{
				PageIndex = PageCount;
			}
			if (PageIndex < 1)
			{
				PageIndex = 1;
			}
			string text;
			if (PageIndex == 1)
			{
				text = string.Concat(new object[]
				{
					"select top ",
					PageSize,
					" ",
					SqlAllFields,
					" from ",
					SqlTablesAndWhere,
					" ",
					OrderFields
				});
			}
			else
			{
				text = string.Concat(new object[]
				{
					"select top ",
					PageSize,
					" ",
					SqlAllFields,
					" from "
				});
				if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
				{
					string str = Regex.Replace(SqlTablesAndWhere, "\\ where\\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
					text = text + str + ") and (";
				}
				else
				{
					text = text + SqlTablesAndWhere + " where (";
				}
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					IndexField,
					" not in (select top ",
					(PageIndex - 1) * PageSize,
					" ",
					IndexField,
					" from ",
					SqlTablesAndWhere,
					" ",
					OrderFields
				});
				text = text + ")) " + OrderFields;
			}
			return text;
		}

		private static void PrepareCommand(System.Data.Common.DbCommand cmd, System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, System.Data.CommandType cmdType, string cmdText, System.Data.Common.DbParameter[] cmdParms)
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
			cmd.CommandType = cmdType;
			cmd.CommandTimeout = DbHelperSQL.Timeout;
			if (cmdParms != null)
			{
				for (int i = 0; i < cmdParms.Length; i++)
				{
					System.Data.Common.DbParameter dbParameter = cmdParms[i];
					if (dbParameter != null)
					{
						cmd.Parameters.Add(dbParameter);
					}
				}
			}
		}
	}
}
