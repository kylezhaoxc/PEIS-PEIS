using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonStatistics
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet Query(string sql, int? TimeOut = null);

		DataSet Query(string currConnectionString, string sql, int? TimeOut = null);
	}
}
