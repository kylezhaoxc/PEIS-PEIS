using PEIS.Common;
using PEIS.IDAL;
using PEIS.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.SQLServerDAL
{
	public class CommonExcuteSql : ICommonExcuteSql
	{
		DataSet ICommonExcuteSql.ExcuteSql(string sql)
		{
			DataSet result;
			try
			{
				result = DbHelperSQL.Query(sql);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error("执行单条语句返回DataTable Exception：" + ex.Message);
				  throw ex;
			}
			return result;
		}

		DataSet ICommonExcuteSql.ExcuteSql(string ConfigName, string sql)
		{
			string connectionString = PEIS.DBUtility.PubConstant.GetConnectionString(ConfigName);
			DataSet result;
			if (string.IsNullOrEmpty(connectionString))
			{
				result = null;
			}
			else
			{
				try
				{
					result = DbHelperSQL.Query(connectionString, sql);
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error("执行单条语句返回DataTable Exception：" + ex.Message);
					throw ex;
				}
			}
			return result;
		}

		int ICommonExcuteSql.ExecuteSqlTran(List<string> SQLStringList)
		{
			int result;
			try
			{
				result = DbHelperSQL.ExecuteSqlTran(SQLStringList);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error("采用事务的方式，执行多条语句 Exception：" + ex.Message);
				for (int i = 0; i < SQLStringList.Count; i++)
				{
					string text = SQLStringList[i];
					if (text.Trim().Length > 1)
					{
						Log4J.Instance.Error("本次事务执行的语句【" + (i + 1).ToString() + "】：" + Secret.AES.Encrypt(text));
					}
				}
				throw ex;
			}
			return result;
		}

		int ICommonExcuteSql.ExecuteSql(string SQLString)
		{
			int result;
			try
			{
				result = DbHelperSQL.ExecuteSql(SQLString);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error("采用ExecuteSql，执行语句 Exception：" + ex.Message);
				throw ex;
			}
			return result;
		}
	}
}
