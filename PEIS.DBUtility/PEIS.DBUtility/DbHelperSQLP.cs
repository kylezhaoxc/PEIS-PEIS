using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PEIS.DBUtility
{
	public class DbHelperSQLP
	{
		public string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;

		public DbHelperSQLP()
		{
		}

		public DbHelperSQLP(string ConnectionString)
		{
			this.connectionString = ConnectionString;
		}

		public bool ColumnExists(string tableName, string columnName)
		{
			string sQLString = string.Concat(new string[]
			{
				"select count(1) from syscolumns where [id]=object_id('",
				tableName,
				"') and [name]='",
				columnName,
				"'"
			});
			object single = this.GetSingle(sQLString);
			return single != null && Convert.ToInt32(single) > 0;
		}

		public int GetMaxID(string FieldName, string TableName)
		{
			string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
			object single = this.GetSingle(sQLString);
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

		public bool Exists(string strSql)
		{
			object single = this.GetSingle(strSql);
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

		public bool TabExists(string TableName)
		{
			string sQLString = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
			object single = this.GetSingle(sQLString);
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

		public bool Exists(string strSql, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			object single = this.GetSingle(strSql, cmdParms);
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

		public int ExecuteSql(string SQLString)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public int ExecuteSqlByTime(string SQLString, int Times)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
						DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, commandText, cmdParms);
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

		public int ExecuteSqlTran(List<string> SQLStringList)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
					result = num;
				}
				catch
				{
					sqlTransaction.Rollback();
					result = 0;
				}
			}
			return result;
		}

		public int ExecuteSql(string SQLString, string content)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public object ExecuteSqlGet(string SQLString, string content)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public object GetSingle(string SQLString)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public object GetSingle(string SQLString, int Times)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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

		public System.Data.SqlClient.SqlDataReader ExecuteReader(string strSQL)
		{
			System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString);
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

		public System.Data.DataSet Query(string SQLString)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					sqlConnection.Open();
					System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(SQLString, sqlConnection);
					sqlDataAdapter.Fill(dataSet, "ds");
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public System.Data.DataSet Query(string SQLString, int Times)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					throw new Exception(ex.Message);
				}
				result = dataSet;
			}
			return result;
		}

		public int ExecuteSql(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand())
				{
					try
					{
						DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
						int num = sqlCommand.ExecuteNonQuery();
						sqlCommand.Parameters.Clear();
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

		public void ExecuteSqlTran(Hashtable SQLStringList)
		{
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
							DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, cmdParms);
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

		public int ExecuteSqlTran(List<CommandInfo> cmdList)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
							DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, commandText, cmdParms);
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

		public void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
		{
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
							DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, commandText, array);
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

		public void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
		{
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
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
							DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, array);
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

		public object GetSingle(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand())
				{
					try
					{
						DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
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

		public System.Data.SqlClient.SqlDataReader ExecuteReader(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(this.connectionString);
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			System.Data.SqlClient.SqlDataReader result;
			try
			{
				DbHelperSQLP.PrepareCommand(sqlCommand, conn, null, SQLString, cmdParms);
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

		public System.Data.DataSet Query(string SQLString, params System.Data.SqlClient.SqlParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
				DbHelperSQLP.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
				using (System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand))
				{
					System.Data.DataSet dataSet = new System.Data.DataSet();
					try
					{
						sqlDataAdapter.Fill(dataSet, "ds");
						sqlCommand.Parameters.Clear();
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

		public System.Data.SqlClient.SqlDataReader RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters)
		{
			System.Data.SqlClient.SqlDataReader result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				sqlConnection.Open();
				System.Data.SqlClient.SqlCommand sqlCommand = DbHelperSQLP.BuildQueryCommand(sqlConnection, storedProcName, parameters);
				sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
				System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				result = sqlDataReader;
			}
			return result;
		}

		public System.Data.DataSet RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, string tableName)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				sqlConnection.Open();
				new System.Data.SqlClient.SqlDataAdapter
				{
					SelectCommand = DbHelperSQLP.BuildQueryCommand(sqlConnection, storedProcName, parameters)
				}.Fill(dataSet, tableName);
				sqlConnection.Close();
				result = dataSet;
			}
			return result;
		}

		public System.Data.DataSet RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, string tableName, int Times)
		{
			System.Data.DataSet result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				sqlConnection.Open();
				new System.Data.SqlClient.SqlDataAdapter
				{
					//SelectCommand = DbHelperSQLP.BuildQueryCommand(sqlConnection, storedProcName, parameters),
					SelectCommand = 
					{
						CommandTimeout = Times
					}
				}.Fill(dataSet, tableName);
				sqlConnection.Close();
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

		public int RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, out int rowsAffected)
		{
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				sqlConnection.Open();
				System.Data.SqlClient.SqlCommand sqlCommand = DbHelperSQLP.BuildIntCommand(sqlConnection, storedProcName, parameters);
				rowsAffected = sqlCommand.ExecuteNonQuery();
				int num = (int)sqlCommand.Parameters["ReturnValue"].Value;
				result = num;
			}
			return result;
		}

		private static System.Data.SqlClient.SqlCommand BuildIntCommand(System.Data.SqlClient.SqlConnection connection, string storedProcName, System.Data.IDataParameter[] parameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = DbHelperSQLP.BuildQueryCommand(connection, storedProcName, parameters);
			sqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("ReturnValue", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null));
			return sqlCommand;
		}
	}
}
