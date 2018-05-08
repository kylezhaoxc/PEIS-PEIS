using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;

namespace PEIS.BLL
{
	public class CommonRight
	{
		private static ICommonRight dal = DataAccess.CreateCommonRight();

		private static readonly CommonRight _instance = new CommonRight();

		public static CommonRight Instance
		{
			get
			{
				return CommonRight._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonRight.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonRight.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public int UpdateUserRoleRightSection(int ID_User, string currRoleIDsStr, string currRightIDsStr, string currSectionIDsStr, int ID_Operator, DateTime OperDate)
		{
			return CommonRight.dal.UpdateUserRoleRightSection(ID_User, currRoleIDsStr, currRightIDsStr, currSectionIDsStr, ID_Operator, OperDate);
		}

		public int OperateRoleRight(int ID_Role, string currRightIDsStr, int ID_Operator, DateTime OperDate)
		{
			return CommonRight.dal.OperateRoleRight(ID_Role, currRightIDsStr, ID_Operator, OperDate);
		}

		public int DeleteRole(int ID_Role)
		{
			return CommonRight.dal.DeleteRole(ID_Role);
		}
	}
}
