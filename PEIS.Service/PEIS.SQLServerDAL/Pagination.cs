using PEIS.Common;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PEIS.SQLServerDAL
{
	public class Pagination
	{
		internal static DataTable ProcPage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
		{
			DateTime now = DateTime.Now;
			PageCount = 0;
			RecordCount = 0;
			SqlParameter[] param = Pagination.GetParam(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize);
			DataTable result = DbHelperSQL.ExecuteTable(CommandType.StoredProcedure, "NTP_Page", param);
			RecordCount = (int)param[6].Value;
			PageCount = (int)param[7].Value;
			string dateDiff = Public.GetDateDiff("通用查询语句（分页) 日志", now, DateTime.Now);
			Log4J.Instance.Debug(string.Concat(new object[]
			{
				dateDiff,
				" ,SqlAllFields: ",
				Secret.AES.Encrypt(SqlAllFields),
				",SqlTablesAndWhere:",
				Secret.AES.Encrypt(SqlTablesAndWhere),
				",IndexField:",
				IndexField,
				",OrderFields:",
				OrderFields,
				",PageIndex:",
				PageIndex.ToString(),
				",PageSize:",
				PageSize.ToString(),
				",RecordCount:",
				RecordCount,
				",PageCount:",
				PageCount.ToString()
			}));
			return result;
		}

		internal static DataTable ProcPage_Distinct(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
		{
			PageCount = 0;
			RecordCount = 0;
			SqlParameter[] param = Pagination.GetParam(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize);
			DataTable result = DbHelperSQL.ExecuteTable(CommandType.StoredProcedure, "NTP_Page", param);
			RecordCount = (int)param[6].Value;
			PageCount = (int)param[7].Value;
			return result;
		}

		internal static DataTable ProcPage(SqlConnection cn, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
		{
			PageCount = 0;
			RecordCount = 0;
			SqlParameter[] param = Pagination.GetParam(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize);
			DataTable result = DbHelperSQL.ExecuteTable(cn, CommandType.StoredProcedure, "NTP_Page", param);
			RecordCount = (int)param[6].Value;
			PageCount = (int)param[7].Value;
			return result;
		}

		private static SqlParameter[] GetParam(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize)
		{
			SqlParameter[] array = new SqlParameter[8];
			array[0] = new SqlParameter("@IndexField", SqlDbType.VarChar, 100);
			array[0].Value = IndexField;
			array[1] = new SqlParameter("@AllFields", SqlDbType.VarChar, 8000);
			array[1].Value = SqlAllFields;
			array[2] = new SqlParameter("@TablesAndWhere", SqlDbType.VarChar, 8000);
			array[2].Value = SqlTablesAndWhere;
			array[3] = new SqlParameter("@OrderFields", SqlDbType.VarChar, 255);
			array[3].Value = OrderFields;
			array[4] = new SqlParameter("@PageSize", SqlDbType.Int);
			array[4].Value = PageSize;
			array[5] = new SqlParameter("@PageIndex", SqlDbType.Int);
			array[5].Value = PageIndex;
			array[6] = new SqlParameter("@RecordCount", SqlDbType.Int);
			array[6].Direction = ParameterDirection.Output;
			array[7] = new SqlParameter("@PageCount", SqlDbType.Int);
			array[7].Direction = ParameterDirection.Output;
			return array;
		}
	}
}
