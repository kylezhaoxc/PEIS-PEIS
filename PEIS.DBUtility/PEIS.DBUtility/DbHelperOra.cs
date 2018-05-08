using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;

namespace PEIS.DBUtility
{
	public abstract class DbHelperOra
	{
		public static string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;

		public DbHelperOra()
		{
		}

		public static int GetMaxID(string FieldName, string TableName)
		{
			string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
			object single = DbHelperOra.GetSingle(sQLString);
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
			object single = DbHelperOra.GetSingle(strSql);
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

		public static bool Exists(string strSql, params OracleParameter[] cmdParms)
		{
			object single = DbHelperOra.GetSingle(strSql, cmdParms);
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
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				using (OracleCommand oracleCommand = new OracleCommand(SQLString, oracleConnection))
				{
					try
					{
						oracleConnection.Open();
						int num = oracleCommand.ExecuteNonQuery();
						result = num;
					}
					catch (OracleException ex)
					{
						oracleConnection.Close();
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(ArrayList SQLStringList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				oracleConnection.Open();
				OracleCommand oracleCommand = new OracleCommand();
				oracleCommand.Connection = oracleConnection;
				OracleTransaction oracleTransaction = oracleConnection.BeginTransaction();
				oracleCommand.Transaction = oracleTransaction;
				try
				{
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = SQLStringList[i].ToString();
						if (text.Trim().Length > 1)
						{
							oracleCommand.CommandText = text;
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
			}
		}

		public static int ExecuteSql(string SQLString, string content)
		{
			int result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				OracleCommand oracleCommand = new OracleCommand(SQLString, oracleConnection);
				OracleParameter oracleParameter = new OracleParameter("@content", OracleType.NVarChar);
				oracleParameter.Value = content;
				oracleCommand.Parameters.Add(oracleParameter);
				try
				{
					oracleConnection.Open();
					int num = oracleCommand.ExecuteNonQuery();
					result = num;
				}
				catch (OracleException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					oracleCommand.Dispose();
					oracleConnection.Close();
				}
			}
			return result;
		}

		public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			int result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				OracleCommand oracleCommand = new OracleCommand(strSQL, oracleConnection);
				OracleParameter oracleParameter = new OracleParameter("@fs", OracleType.LongRaw);
				oracleParameter.Value = fs;
				oracleCommand.Parameters.Add(oracleParameter);
				try
				{
					oracleConnection.Open();
					int num = oracleCommand.ExecuteNonQuery();
					result = num;
				}
				catch (OracleException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					oracleCommand.Dispose();
					oracleConnection.Close();
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString)
		{
			object result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
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
						oracleConnection.Close();
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static OracleDataReader ExecuteReader(string strSQL)
		{
			OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString);
			OracleCommand oracleCommand = new OracleCommand(strSQL, oracleConnection);
			OracleDataReader result;
			try
			{
				oracleConnection.Open();
				OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				result = oracleDataReader;
			}
			catch (OracleException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString)
		{
			System.Data.DataSet result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
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
				result = dataSet;
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, params OracleParameter[] cmdParms)
		{
			int result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				using (OracleCommand oracleCommand = new OracleCommand())
				{
					try
					{
						DbHelperOra.PrepareCommand(oracleCommand, oracleConnection, null, SQLString, cmdParms);
						int num = oracleCommand.ExecuteNonQuery();
						oracleCommand.Parameters.Clear();
						result = num;
					}
					catch (OracleException ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				oracleConnection.Open();
				using (OracleTransaction oracleTransaction = oracleConnection.BeginTransaction())
				{
					OracleCommand oracleCommand = new OracleCommand();
					try
					{
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = dictionaryEntry.Key.ToString();
							OracleParameter[] cmdParms = (OracleParameter[])dictionaryEntry.Value;
							DbHelperOra.PrepareCommand(oracleCommand, oracleConnection, oracleTransaction, cmdText, cmdParms);
							int num = oracleCommand.ExecuteNonQuery();
							oracleCommand.Parameters.Clear();
							oracleTransaction.Commit();
						}
					}
					catch
					{
						oracleTransaction.Rollback();
						throw;
					}
				}
			}
		}

		public static object GetSingle(string SQLString, params OracleParameter[] cmdParms)
		{
			object result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				using (OracleCommand oracleCommand = new OracleCommand())
				{
					try
					{
						DbHelperOra.PrepareCommand(oracleCommand, oracleConnection, null, SQLString, cmdParms);
						object obj = oracleCommand.ExecuteScalar();
						oracleCommand.Parameters.Clear();
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
				}
			}
			return result;
		}

		public static OracleDataReader ExecuteReader(string SQLString, params OracleParameter[] cmdParms)
		{
			OracleConnection conn = new OracleConnection(DbHelperOra.connectionString);
			OracleCommand oracleCommand = new OracleCommand();
			OracleDataReader result;
			try
			{
				DbHelperOra.PrepareCommand(oracleCommand, conn, null, SQLString, cmdParms);
				OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				oracleCommand.Parameters.Clear();
				result = oracleDataReader;
			}
			catch (OracleException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		public static System.Data.DataSet Query(string SQLString, params OracleParameter[] cmdParms)
		{
			System.Data.DataSet result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				OracleCommand oracleCommand = new OracleCommand();
				DbHelperOra.PrepareCommand(oracleCommand, oracleConnection, null, SQLString, cmdParms);
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
					OracleParameter value = cmdParms[i];
					cmd.Parameters.Add(value);
				}
			}
		}

		public static OracleDataReader RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters)
		{
			OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString);
			oracleConnection.Open();
			OracleCommand oracleCommand = DbHelperOra.BuildQueryCommand(oracleConnection, storedProcName, parameters);
			oracleCommand.CommandType = System.Data.CommandType.StoredProcedure;
			return oracleCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}

		public static System.Data.DataSet RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, string tableName)
		{
			System.Data.DataSet result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				oracleConnection.Open();
				new OracleDataAdapter
				{
					SelectCommand = DbHelperOra.BuildQueryCommand(oracleConnection, storedProcName, parameters)
				}.Fill(dataSet, tableName);
				oracleConnection.Close();
				result = dataSet;
			}
			return result;
		}

		private static OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, System.Data.IDataParameter[] parameters)
		{
			OracleCommand oracleCommand = new OracleCommand(storedProcName, connection);
			oracleCommand.CommandType = System.Data.CommandType.StoredProcedure;
			for (int i = 0; i < parameters.Length; i++)
			{
				OracleParameter value = (OracleParameter)parameters[i];
				oracleCommand.Parameters.Add(value);
			}
			return oracleCommand;
		}

		public static int RunProcedure(string storedProcName, System.Data.IDataParameter[] parameters, out int rowsAffected)
		{
			int result;
			using (OracleConnection oracleConnection = new OracleConnection(DbHelperOra.connectionString))
			{
				oracleConnection.Open();
				OracleCommand oracleCommand = DbHelperOra.BuildIntCommand(oracleConnection, storedProcName, parameters);
				rowsAffected = oracleCommand.ExecuteNonQuery();
				int num = (int)oracleCommand.Parameters["ReturnValue"].Value;
				result = num;
			}
			return result;
		}

		private static OracleCommand BuildIntCommand(OracleConnection connection, string storedProcName, System.Data.IDataParameter[] parameters)
		{
			OracleCommand oracleCommand = DbHelperOra.BuildQueryCommand(connection, storedProcName, parameters);
			oracleCommand.Parameters.Add(new OracleParameter("ReturnValue", OracleType.Int32, 4, System.Data.ParameterDirection.ReturnValue, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null));
			return oracleCommand;
		}
	}
}
