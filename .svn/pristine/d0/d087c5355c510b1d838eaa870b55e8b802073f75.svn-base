using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Reflection;

namespace PEIS.SQLServerDAL
{
	public class CommonStatistics : CommonBase, ICommonStatistics
	{
		DataTable ICommonStatistics.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return base.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		protected new string[] GetSqlSentence(string PageName)
		{
			FieldInfo field = base.GetType().GetField(PageName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new Exception("没有找到SQL");
			}
			return (string[])field.GetValue(this);
		}

		DataSet ICommonStatistics.Query(string sql, int? TimeOut = null)
		{
			return DbHelperSQL.Query(sql, TimeOut);
		}

		DataSet ICommonStatistics.Query(string currConnectionString, string sql, int? TimeOut = null)
		{
			return DbHelperSQL.Query(currConnectionString, sql, TimeOut);
		}
	}
}
