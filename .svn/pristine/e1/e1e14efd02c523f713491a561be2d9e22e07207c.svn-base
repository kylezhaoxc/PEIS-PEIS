using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonReport
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySqlX(string AppSettingKey, string QuerySqlCode, params SqlConditionInfo[] conditions);

		int UpdateCustomerPrintFlag(string ID_Customer, string ID_Operator, string Operator, string UpdateTime);

		int UpdateCustomerReportFlag(string ID_User, string UserName, string ID_Customer, int Is_Self, string OperType);

		int UpdateCustomerReportInformFlag(string ID_User, string UserName, string ID_Customer, int Is_Self, string OperType);

		int UpdateCustomerReportReceiptFlag(List<OnCustReportManage> listCustReportManage);
	}
}
