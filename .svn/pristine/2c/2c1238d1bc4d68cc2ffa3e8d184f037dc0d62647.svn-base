using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PEIS.DBUtility
{
	public abstract class SqlHelper
	{
		public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.AppSettings["SQLConnString1"];

		public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.AppSettings["SQLConnString2"];

		public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.AppSettings["SQLConnString3"];

		public static readonly string ConnectionStringProfile = ConfigurationManager.AppSettings["SQLProfileConnString"];

		private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

		public static int ExecuteNonQuery(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			int result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
				int num = sqlCommand.ExecuteNonQuery();
				sqlCommand.Parameters.Clear();
				result = num;
			}
			return result;
		}

		public static int ExecuteNonQuery(System.Data.SqlClient.SqlConnection connection, System.Data.CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			SqlHelper.PrepareCommand(sqlCommand, connection, null, cmdType, cmdText, commandParameters);
			int result = sqlCommand.ExecuteNonQuery();
			sqlCommand.Parameters.Clear();
			return result;
		}

		public static int ExecuteNonQuery(System.Data.SqlClient.SqlTransaction trans, System.Data.CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			SqlHelper.PrepareCommand(sqlCommand, trans.Connection, trans, cmdType, cmdText, commandParameters);
			int result = sqlCommand.ExecuteNonQuery();
			sqlCommand.Parameters.Clear();
			return result;
		}

		public static System.Data.SqlClient.SqlDataReader ExecuteReader(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
			System.Data.SqlClient.SqlDataReader result;
			try
			{
				SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
				System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				sqlCommand.Parameters.Clear();
				result = sqlDataReader;
			}
			catch
			{
				sqlConnection.Close();
				throw;
			}
			return result;
		}

		public static object ExecuteScalar(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			object result;
			using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
				object obj = sqlCommand.ExecuteScalar();
				sqlCommand.Parameters.Clear();
				result = obj;
			}
			return result;
		}

		public static object ExecuteScalar(System.Data.SqlClient.SqlConnection connection, System.Data.CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			SqlHelper.PrepareCommand(sqlCommand, connection, null, cmdType, cmdText, commandParameters);
			object result = sqlCommand.ExecuteScalar();
			sqlCommand.Parameters.Clear();
			return result;
		}

		public static void CacheParameters(string cacheKey, params System.Data.SqlClient.SqlParameter[] commandParameters)
		{
			SqlHelper.parmCache[cacheKey] = commandParameters;
		}

		public static System.Data.SqlClient.SqlParameter[] GetCachedParameters(string cacheKey)
		{
			System.Data.SqlClient.SqlParameter[] array = (System.Data.SqlClient.SqlParameter[])SqlHelper.parmCache[cacheKey];
			System.Data.SqlClient.SqlParameter[] result;
			if (array == null)
			{
				result = null;
			}
			else
			{
				System.Data.SqlClient.SqlParameter[] array2 = new System.Data.SqlClient.SqlParameter[array.Length];
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					array2[i] = (System.Data.SqlClient.SqlParameter)((ICloneable)array[i]).Clone();
					i++;
				}
				result = array2;
			}
			return result;
		}

		private static void PrepareCommand(System.Data.SqlClient.SqlCommand cmd, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans, System.Data.CommandType cmdType, string cmdText, System.Data.SqlClient.SqlParameter[] cmdParms)
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
			if (cmdParms != null)
			{
				for (int i = 0; i < cmdParms.Length; i++)
				{
					System.Data.SqlClient.SqlParameter value = cmdParms[i];
					cmd.Parameters.Add(value);
				}
			}
		}
	}
}
