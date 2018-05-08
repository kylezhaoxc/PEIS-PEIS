using PEIS.Common;
using PEIS.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PEIS.SQLServerDAL
{
	public class CommandWebserviceInterface : ICommandWebserviceInterface
	{
		private string connectionString = PEIS.DBUtility.PubConstant.GetConnectionString("ConnectionString_Interface");

		public DataSet ExcuteSql(string SQLString)
		{
			DateTime now = DateTime.Now;
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
			{
				DataSet dataSet = new DataSet();
				try
				{
					sqlConnection.Open();
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SQLString, sqlConnection);
					sqlDataAdapter.Fill(dataSet, "ds");
					string dateDiff = Public.GetDateDiff("执行查询语句，返回DataSet", now, DateTime.Now);
					Log4J.Instance.Debug(dateDiff + " ,SQL语句: " + SQLString);
				}
				catch (SqlException ex)
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

		public int ExecuteSqlTran(List<string> SQLStringList)
		{
			DateTime now = DateTime.Now;
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
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
	}
}
