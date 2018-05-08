using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonSubScribe
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);
	}
}
