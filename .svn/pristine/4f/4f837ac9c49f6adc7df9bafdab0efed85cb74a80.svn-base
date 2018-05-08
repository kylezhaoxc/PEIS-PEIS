using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusFeeReport : IBusFeeReport
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_FeeReport", "BusFeeReport");
		}

		public bool Exists(int ID_FeeReport)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusFeeReport");
			stringBuilder.Append(" where ID_FeeReport=@ID_FeeReport ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FeeReport", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FeeReport;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusFeeReport model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusFeeReport(");
			stringBuilder.Append("ID_Fee,FeeName,ReportKey,ImageUrl,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Fee,@FeeName,@ReportKey,@ImageUrl,@Note,@Is_Banned,@ID_Operator,@Operator,@OperateDate,@OperateType,@BanDescribe)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@FeeName", SqlDbType.VarChar, 50),
				new SqlParameter("@ReportKey", SqlDbType.VarChar, 50),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 256),
				new SqlParameter("@Note", SqlDbType.VarChar, 2000),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50)
			};
			array[0].Value = model.ID_Fee;
			array[1].Value = model.FeeName;
			array[2].Value = model.ReportKey;
			array[3].Value = model.ImageUrl;
			array[4].Value = model.Note;
			array[5].Value = model.Is_Banned;
			array[6].Value = model.ID_Operator;
			array[7].Value = model.Operator;
			array[8].Value = model.OperateDate;
			array[9].Value = model.OperateType;
			array[10].Value = model.BanDescribe;
			object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), array);
			int result;
			if (single == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(single);
			}
			return result;
		}

		public bool Update(PEIS.Model.BusFeeReport model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusFeeReport set ");
			stringBuilder.Append("ID_Fee=@ID_Fee,");
			stringBuilder.Append("FeeName=@FeeName,");
			stringBuilder.Append("ReportKey=@ReportKey,");
			stringBuilder.Append("ImageUrl=@ImageUrl,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_Operator=@ID_Operator,");
			stringBuilder.Append("Operator=@Operator,");
			stringBuilder.Append("OperateDate=@OperateDate,");
			stringBuilder.Append("OperateType=@OperateType,");
			stringBuilder.Append("BanDescribe=@BanDescribe");
			stringBuilder.Append(" where ID_FeeReport=@ID_FeeReport");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@FeeName", SqlDbType.VarChar, 50),
				new SqlParameter("@ReportKey", SqlDbType.VarChar, 50),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 256),
				new SqlParameter("@Note", SqlDbType.VarChar, 2000),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_FeeReport", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Fee;
			array[1].Value = model.FeeName;
			array[2].Value = model.ReportKey;
			array[3].Value = model.ImageUrl;
			array[4].Value = model.Note;
			array[5].Value = model.Is_Banned;
			array[6].Value = model.ID_Operator;
			array[7].Value = model.Operator;
			array[8].Value = model.OperateDate;
			array[9].Value = model.OperateType;
			array[10].Value = model.BanDescribe;
			array[11].Value = model.ID_FeeReport;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_FeeReport)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusFeeReport ");
			stringBuilder.Append(" where ID_FeeReport=@ID_FeeReport");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FeeReport", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FeeReport;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_FeeReportlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusFeeReport ");
			stringBuilder.Append(" where ID_FeeReport in (" + ID_FeeReportlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusFeeReport GetModel(int ID_FeeReport)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_FeeReport,ID_Fee,FeeName,ReportKey,ImageUrl,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe from BusFeeReport ");
			stringBuilder.Append(" where ID_FeeReport=@ID_FeeReport");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FeeReport", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FeeReport;
			PEIS.Model.BusFeeReport busFeeReport = new PEIS.Model.BusFeeReport();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusFeeReport result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_FeeReport"].ToString() != "")
				{
					busFeeReport.ID_FeeReport = int.Parse(dataSet.Tables[0].Rows[0]["ID_FeeReport"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Fee"].ToString() != "")
				{
					busFeeReport.ID_Fee = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Fee"].ToString()));
				}
				busFeeReport.FeeName = dataSet.Tables[0].Rows[0]["FeeName"].ToString();
				busFeeReport.ReportKey = dataSet.Tables[0].Rows[0]["ReportKey"].ToString();
				busFeeReport.ImageUrl = dataSet.Tables[0].Rows[0]["ImageUrl"].ToString();
				busFeeReport.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busFeeReport.Is_Banned = new bool?(true);
					}
					else
					{
						busFeeReport.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_Operator"].ToString() != "")
				{
					busFeeReport.ID_Operator = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Operator"].ToString()));
				}
				busFeeReport.Operator = dataSet.Tables[0].Rows[0]["Operator"].ToString();
				if (dataSet.Tables[0].Rows[0]["OperateDate"].ToString() != "")
				{
					busFeeReport.OperateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["OperateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["OperateType"].ToString() != "")
				{
					busFeeReport.OperateType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["OperateType"].ToString()));
				}
				busFeeReport.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				result = busFeeReport;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ID_FeeReport,ID_Fee,FeeName,ReportKey,ImageUrl,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe ");
			stringBuilder.Append(" FROM BusFeeReport ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ");
			if (Top > 0)
			{
				stringBuilder.Append(" top " + Top.ToString());
			}
			stringBuilder.Append(" ID_FeeReport,ID_Fee,FeeName,ReportKey,ImageUrl,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe ");
			stringBuilder.Append(" FROM BusFeeReport ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
