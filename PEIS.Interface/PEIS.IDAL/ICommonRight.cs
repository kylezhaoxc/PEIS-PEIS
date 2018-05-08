using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonRight
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions);

		int UpdateUserRoleRightSection(int ID_User, string currRoleIDsStr, string currRightIDsStr, string currSectionIDsStr, int ID_Operator, DateTime OperDate);

		int OperateRoleRight(int ID_Role, string currRightIDsStr, int ID_Operator, DateTime OperDate);

		int DeleteRole(int ID_Role);
	}
}
