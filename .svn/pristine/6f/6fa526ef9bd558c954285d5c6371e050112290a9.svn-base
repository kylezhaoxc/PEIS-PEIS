using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictReceiveReportWay : IDictReceiveReportWay
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ReportWayID", "DictReceiveReportWay");
		}

		public bool Exists(int ReportWayID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictReceiveReportWay");
			stringBuilder.Append(" where ReportWayID=@ReportWayID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ReportWayID", SqlDbType.Int, 4)
			};
			array[0].Value = ReportWayID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictReceiveReportWay model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictReceiveReportWay(");
			stringBuilder.Append("ReportWayName,Default,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ReportWayName,@Is_Default,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ReportWayName", SqlDbType.VarChar, 50),
				new SqlParameter("@Default", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10)
			};
			array[0].Value = model.ReportWayName;
			array[1].Value = model.Default;
			array[2].Value = model.InputCode;
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

		public bool Update(PEIS.Model.DictReceiveReportWay model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictReceiveReportWay set ");
			stringBuilder.Append("ReportWayName=@ReportWayName,");
			stringBuilder.Append("Default=@Default,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where ReportWayID=@ReportWayID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ReportWayName", SqlDbType.VarChar, 50),
				new SqlParameter("@Default", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@ReportWayID", SqlDbType.Int, 4)
			};
			array[0].Value = model.ReportWayName;
			array[1].Value = model.Default;
			array[2].Value = model.InputCode;
			array[3].Value = model.ReportWayID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ReportWayID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictReceiveReportWay ");
			stringBuilder.Append(" where ReportWayID=@ReportWayID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ReportWayID", SqlDbType.Int, 4)
			};
			array[0].Value = ReportWayID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ReportWayIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictReceiveReportWay ");
			stringBuilder.Append(" where ReportWayID in (" + ReportWayIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictReceiveReportWay GetModel(int ReportWayID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ReportWayID,ReportWayName,Default,InputCode from DictReceiveReportWay ");
			stringBuilder.Append(" where ReportWayID=@ReportWayID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ReportWayID", SqlDbType.Int, 4)
			};
			array[0].Value = ReportWayID;
			PEIS.Model.DictReceiveReportWay receiveReportWay = new PEIS.Model.DictReceiveReportWay();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictReceiveReportWay result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ReportWayID"].ToString() != "")
				{
                    receiveReportWay.ReportWayID = int.Parse(dataSet.Tables[0].Rows[0]["ReportWayID"].ToString());
				}
                receiveReportWay.ReportWayName = dataSet.Tables[0].Rows[0]["ReportWayName"].ToString();
				if (dataSet.Tables[0].Rows[0]["Default"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Default"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Default"].ToString().ToLower() == "true")
					{
                        receiveReportWay.Default = true;
					}
					else
					{
                        receiveReportWay.Default = false;
					}
				}
                receiveReportWay.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = receiveReportWay;
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
			stringBuilder.Append("select ReportWayID,ReportWayName,Default,InputCode ");
			stringBuilder.Append(" FROM DictReceiveReportWay ");
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
			stringBuilder.Append(" ReportWayID,ReportWayName,Default,InputCode ");
			stringBuilder.Append(" FROM DictReceiveReportWay ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
