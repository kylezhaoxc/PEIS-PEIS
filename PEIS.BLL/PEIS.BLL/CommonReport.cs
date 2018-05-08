using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonReport
	{
		private static ICommonReport dal = DataAccess.CreateCommonReport();

		private static readonly CommonReport _instance = new CommonReport();

		public static CommonReport Instance
		{
			get
			{
				return CommonReport._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonReport.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonReport.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public DataSet ExcuteQuerySqlX(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonReport.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public DataSet ExcuteQuerySqlX(string AppSettingKey, string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonReport.dal.ExcuteQuerySqlX(AppSettingKey, QuerySqlCode, conditions);
		}

		public int UpdateCustomerPrintFlag(string ID_Customer, string ID_Operator, string Operator, string UpdateTime)
		{
			return CommonReport.dal.UpdateCustomerPrintFlag(ID_Customer, ID_Operator, Operator, UpdateTime);
		}

		public int UpdateCustomerReportFlag(string ID_User, string UserName, string ID_Customer, int Is_Self, string OperType)
		{
			return CommonReport.dal.UpdateCustomerReportFlag(ID_User, UserName, ID_Customer, Is_Self, OperType);
		}

		public int UpdateCustomerReportInformFlag(string ID_User, string UserName, string ID_Customer, int Is_Self, string OperType)
		{
			return CommonReport.dal.UpdateCustomerReportInformFlag(ID_User, UserName, ID_Customer, Is_Self, OperType);
		}

		public int UpdateCustomerReportReceiptFlag(List<PEIS.Model.OnCustReportManage> listCustReportManage)
		{
			return CommonReport.dal.UpdateCustomerReportReceiptFlag(listCustReportManage);
		}

		public DataSet GetCustomerManageReport(string ID_Customer)
		{
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", ID_Customer, System.TypeCode.String)
			};
			DataSet result = null;
			string querySqlCode = "QueryCustomerReportManage_Param";
			try
			{
				result = CommonReport.Instance.ExcuteQuerySql(querySqlCode, conditions);
			}
			catch (System.Exception var_3_33)
			{
			}
			return result;
		}

		public int ISCanReadReport(string ID_Customer)
		{
			int result = 0;
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", ID_Customer, System.TypeCode.String)
			};
			DataSet dataSet = null;
			string querySqlCode = "QueryISCanReadReport_Param";
			try
			{
				dataSet = CommonReport.Instance.ExcuteQuerySql(querySqlCode, conditions);
			}
			catch (System.Exception var_4_35)
			{
			}
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				result = int.Parse(dataSet.Tables[0].Rows[0]["SecurityLevel"].ToString());
			}
			return result;
		}

		public int ISCanReadReport(string AppSettingKey, string ID_Customer)
		{
			int result = 0;
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", ID_Customer, System.TypeCode.String)
			};
			DataSet dataSet = null;
			string querySqlCode = "QueryISCanReadReport_Param";
			try
			{
				dataSet = CommonReport.Instance.ExcuteQuerySql(querySqlCode, conditions);
			}
			catch (System.Exception var_4_35)
			{
			}
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				result = int.Parse(dataSet.Tables[0].Rows[0]["SecurityLevel"].ToString());
			}
			return result;
		}
	}
}
