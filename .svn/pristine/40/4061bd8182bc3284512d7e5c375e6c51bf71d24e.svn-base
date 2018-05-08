using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace PEIS.DBUtility
{
	public abstract class OracleHelper
	{
		public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.AppSettings["OraConnString1"];

		public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.AppSettings["OraConnString2"];

		public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.AppSettings["OraConnString3"];

		public static readonly string ConnectionStringProfile = ConfigurationManager.AppSettings["OraProfileConnString"];

		public static readonly string ConnectionStringMembership = ConfigurationManager.AppSettings["OraMembershipConnString"];

		private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

		public static int ExecuteNonQuery(string connectionString, System.Data.CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand oracleCommand = new OracleCommand();
			int result;
			using (OracleConnection oracleConnection = new OracleConnection(connectionString))
			{
				OracleHelper.PrepareCommand(oracleCommand, oracleConnection, null, cmdType, cmdText, commandParameters);
				int num = oracleCommand.ExecuteNonQuery();
				oracleConnection.Close();
				oracleCommand.Parameters.Clear();
				result = num;
			}
			return result;
		}

		public static System.Data.DataSet Query(string connectionString, string SQLString)
		{
			System.Data.DataSet result;
			using (OracleConnection oracleConnection = new OracleConnection(connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				try
				{
					oracleConnection.Open();
					OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(SQLString, oracleConnection);
					oracleDataAdapter.Fill(dataSet, "ds");
				}
				catch (OracleException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					if (oracleConnection.State != System.Data.ConnectionState.Closed)
					{
						oracleConnection.Close();
					}
				}
				result = dataSet;
			}
			return result;
		}

		public static System.Data.DataSet Query(string connectionString, string SQLString, params OracleParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (OracleConnection oracleConnection = new OracleConnection(connectionString))
			{
				OracleCommand oracleCommand = new OracleCommand();
				OracleHelper.PrepareCommand(oracleCommand, oracleConnection, null, SQLString, cmdParms);
				using (OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand))
				{
					System.Data.DataSet dataSet = new System.Data.DataSet();
					try
					{
						oracleDataAdapter.Fill(dataSet, "ds");
						oracleCommand.Parameters.Clear();
					}
					catch (OracleException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						if (oracleConnection.State != System.Data.ConnectionState.Closed)
						{
							oracleConnection.Close();
						}
					}
					result = dataSet;
				}
			}
			return result;
		}

		private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
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
					OracleParameter oracleParameter = cmdParms[i];
					if ((oracleParameter.Direction == System.Data.ParameterDirection.InputOutput || oracleParameter.Direction == System.Data.ParameterDirection.Input) && oracleParameter.Value == null)
					{
						oracleParameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(oracleParameter);
				}
			}
		}

		public static object GetSingle(string connectionString, string SQLString)
		{
			object result;
			using (OracleConnection oracleConnection = new OracleConnection(connectionString))
			{
				using (OracleCommand oracleCommand = new OracleCommand(SQLString, oracleConnection))
				{
					try
					{
						oracleConnection.Open();
						object obj = oracleCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (OracleException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						if (oracleConnection.State != System.Data.ConnectionState.Closed)
						{
							oracleConnection.Close();
						}
					}
				}
			}
			return result;
		}

		public static bool Exists(string connectionString, string strOracle)
		{
			object single = OracleHelper.GetSingle(connectionString, strOracle);
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

		public static int ExecuteNonQuery(OracleTransaction trans, System.Data.CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand oracleCommand = new OracleCommand();
			OracleHelper.PrepareCommand(oracleCommand, trans.Connection, trans, cmdType, cmdText, commandParameters);
			int result = oracleCommand.ExecuteNonQuery();
			oracleCommand.Parameters.Clear();
			return result;
		}

		public static int ExecuteNonQuery(OracleConnection connection, System.Data.CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand oracleCommand = new OracleCommand();
			OracleHelper.PrepareCommand(oracleCommand, connection, null, cmdType, cmdText, commandParameters);
			int result = oracleCommand.ExecuteNonQuery();
			oracleCommand.Parameters.Clear();
			return result;
		}

		public static int ExecuteNonQuery(string connectionString, string cmdText)
		{
			OracleCommand oracleCommand = new OracleCommand();
			OracleConnection conn = new OracleConnection(connectionString);
			OracleHelper.PrepareCommand(oracleCommand, conn, null, System.Data.CommandType.Text, cmdText, null);
			int result = oracleCommand.ExecuteNonQuery();
			oracleCommand.Parameters.Clear();
			return result;
		}

		public static OracleDataReader ExecuteReader(string connectionString, System.Data.CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand oracleCommand = new OracleCommand();
			OracleConnection oracleConnection = new OracleConnection(connectionString);
			OracleDataReader result;
			try
			{
				OracleHelper.PrepareCommand(oracleCommand, oracleConnection, null, cmdType, cmdText, commandParameters);
				OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				oracleCommand.Parameters.Clear();
				result = oracleDataReader;
			}
			catch
			{
				oracleConnection.Close();
				throw;
			}
			return result;
		}

		public static object ExecuteScalar(string connectionString, System.Data.CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand oracleCommand = new OracleCommand();
			object result;
			using (OracleConnection oracleConnection = new OracleConnection(connectionString))
			{
				OracleHelper.PrepareCommand(oracleCommand, oracleConnection, null, cmdType, cmdText, commandParameters);
				object obj = oracleCommand.ExecuteScalar();
				oracleCommand.Parameters.Clear();
				result = obj;
			}
			return result;
		}

		public static object ExecuteScalar(OracleTransaction transaction, System.Data.CommandType commandType, string commandText, params OracleParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked\tor commited, please\tprovide\tan open\ttransaction.", "transaction");
			}
			OracleCommand oracleCommand = new OracleCommand();
			OracleHelper.PrepareCommand(oracleCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
			object result = oracleCommand.ExecuteScalar();
			oracleCommand.Parameters.Clear();
			return result;
		}

		public static object ExecuteScalar(OracleConnection connectionString, System.Data.CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand oracleCommand = new OracleCommand();
			OracleHelper.PrepareCommand(oracleCommand, connectionString, null, cmdType, cmdText, commandParameters);
			object result = oracleCommand.ExecuteScalar();
			oracleCommand.Parameters.Clear();
			return result;
		}

		public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
		{
			OracleHelper.parmCache[cacheKey] = commandParameters;
		}

		public static OracleParameter[] GetCachedParameters(string cacheKey)
		{
			OracleParameter[] array = (OracleParameter[])OracleHelper.parmCache[cacheKey];
			OracleParameter[] result;
			if (array == null)
			{
				result = null;
			}
			else
			{
				OracleParameter[] array2 = new OracleParameter[array.Length];
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					array2[i] = (OracleParameter)((ICloneable)array[i]).Clone();
					i++;
				}
				result = array2;
			}
			return result;
		}

		private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, System.Data.CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
		{
			if (conn.State != System.Data.ConnectionState.Open)
			{
				conn.Open();
			}
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			cmd.CommandType = cmdType;
			if (trans != null)
			{
				cmd.Transaction = trans;
			}
			if (commandParameters != null)
			{
				for (int i = 0; i < commandParameters.Length; i++)
				{
					OracleParameter value = commandParameters[i];
					cmd.Parameters.Add(value);
				}
			}
		}

		public static string OraBit(bool value)
		{
			string result;
			if (value)
			{
				result = "Y";
			}
			else
			{
				result = "N";
			}
			return result;
		}

		public static bool OraBool(string value)
		{
			return value.Equals("Y");
		}

		public static bool ExecuteSqlTran(string conStr, List<CommandInfo> cmdList)
		{
			bool result;
			using (OracleConnection oracleConnection = new OracleConnection(conStr))
			{
				oracleConnection.Open();
				OracleCommand oracleCommand = new OracleCommand();
				oracleCommand.Connection = oracleConnection;
				OracleTransaction oracleTransaction = oracleConnection.BeginTransaction();
				oracleCommand.Transaction = oracleTransaction;
				try
				{
					foreach (CommandInfo current in cmdList)
					{
						if (!string.IsNullOrEmpty(current.CommandText))
						{
							OracleHelper.PrepareCommand(oracleCommand, oracleConnection, oracleTransaction, System.Data.CommandType.Text, current.CommandText, (OracleParameter[])current.Parameters);
							if (current.EffentNextType == EffentNextType.WhenHaveContine || current.EffentNextType == EffentNextType.WhenNoHaveContine)
							{
								if (current.CommandText.ToLower().IndexOf("count(") == -1)
								{
									oracleTransaction.Rollback();
									throw new Exception("Oracle:违背要求" + current.CommandText + "必须符合select count(..的格式");
								}
								object obj = oracleCommand.ExecuteScalar();
								if (obj == null && obj == DBNull.Value)
								{
								}
								bool flag = Convert.ToInt32(obj) > 0;
								if (current.EffentNextType == EffentNextType.WhenHaveContine && !flag)
								{
									oracleTransaction.Rollback();
									throw new Exception("Oracle:违背要求" + current.CommandText + "返回值必须大于0");
								}
								if (current.EffentNextType == EffentNextType.WhenNoHaveContine && flag)
								{
									oracleTransaction.Rollback();
									throw new Exception("Oracle:违背要求" + current.CommandText + "返回值必须等于0");
								}
							}
							else
							{
								int num = oracleCommand.ExecuteNonQuery();
								if (current.EffentNextType == EffentNextType.ExcuteEffectRows && num == 0)
								{
									oracleTransaction.Rollback();
									throw new Exception("Oracle:违背要求" + current.CommandText + "必须有影像行");
								}
							}
						}
					}
					oracleTransaction.Commit();
					result = true;
				}
				catch (OracleException ex)
				{
					oracleTransaction.Rollback();
					throw ex;
				}
				finally
				{
					if (oracleConnection.State != System.Data.ConnectionState.Closed)
					{
						oracleConnection.Close();
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(string conStr, List<string> SQLStringList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(conStr))
			{
				oracleConnection.Open();
				OracleCommand oracleCommand = new OracleCommand();
				oracleCommand.Connection = oracleConnection;
				OracleTransaction oracleTransaction = oracleConnection.BeginTransaction();
				oracleCommand.Transaction = oracleTransaction;
				try
				{
					foreach (string current in SQLStringList)
					{
						if (!string.IsNullOrEmpty(current))
						{
							oracleCommand.CommandText = current;
							oracleCommand.ExecuteNonQuery();
						}
					}
					oracleTransaction.Commit();
				}
				catch (OracleException ex)
				{
					oracleTransaction.Rollback();
					throw new Exception(ex.Message);
				}
				finally
				{
					if (oracleConnection.State != System.Data.ConnectionState.Closed)
					{
						oracleConnection.Close();
					}
				}
			}
		}
	}
}
