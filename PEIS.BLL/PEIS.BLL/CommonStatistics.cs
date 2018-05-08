using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;

namespace PEIS.BLL
{
	public class CommonStatistics
	{
		private static ICommonStatistics dal = DataAccess.CreateCommonStatistics();

		private static readonly CommonStatistics _instance = new CommonStatistics();

		public static CommonStatistics Instance
		{
			get
			{
				return CommonStatistics._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonStatistics.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet Query(string sql, int? TimeOut = null)
		{
			return CommonStatistics.dal.Query(sql, TimeOut);
		}

		public DataSet Query(string currConnectionString, string sql, int? TimeOut = null)
		{
			return CommonStatistics.dal.Query(currConnectionString, sql, TimeOut);
		}
	}
}
