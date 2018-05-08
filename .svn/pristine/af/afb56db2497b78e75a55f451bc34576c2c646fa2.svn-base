using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonUser
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions);

		DataTable CreateMaxNum(string NumberHeader);

		DataTable GetUserInfoByLoginName(string userLoginName);

		int AddCustomerArcInfo(OnArcCust model);

		int AddCustomerManageArcInfo(OnArcCust model);

		int UpdateCustomerPicInfo(OnArcCust model);

		int UpdateCustomerPicInfo(string ID_Customer, OnArcCust model);
	}
}
