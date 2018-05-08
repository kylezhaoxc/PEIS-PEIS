using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictFeeWay : IDictFeeWay
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("FeeWayID", "DictFeeWay");
		}

		public bool Exists(int FeeWayID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictFeeWay");
			stringBuilder.Append(" where FeeWayID=@FeeWayID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FeeWayID", SqlDbType.Int, 4)
			};
			array[0].Value = FeeWayID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictFeeWay model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictFeeWay(");
			stringBuilder.Append("FeeWayName,Default,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@FeeWayName,@Default,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FeeWayName", SqlDbType.VarChar, 10),
				new SqlParameter("@Default", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8)
			};
			array[0].Value = model.FeeWayName;
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

		public bool Update(PEIS.Model.DictFeeWay model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictFeeWay set ");
			stringBuilder.Append("FeeWayName=@FeeWayName,");
			stringBuilder.Append("Default=@Default,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where FeeWayID=@FeeWayID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FeeWayName", SqlDbType.VarChar, 10),
				new SqlParameter("@Default", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8),
				new SqlParameter("@FeeWayID", SqlDbType.Int, 4)
			};
			array[0].Value = model.FeeWayName;
			array[1].Value = model.Default;
			array[2].Value = model.InputCode;
			array[3].Value = model.FeeWayID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_FeeWay)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictFeeWay ");
			stringBuilder.Append(" where FeeWayID=@FeeWayID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FeeWayID", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FeeWay;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string FeeWayIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictFeeWay ");
			stringBuilder.Append(" where FeeWayID in (" + FeeWayIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictFeeWay GetModel(int FeeWayID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 FeeWayID,FeeWayName,Default,InputCode from DictFeeWay ");
			stringBuilder.Append(" where FeeWayID=@FeeWayID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FeeWayID", SqlDbType.Int, 4)
			};
			array[0].Value = FeeWayID;
			PEIS.Model.DictFeeWay dictFeeWay = new PEIS.Model.DictFeeWay();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictFeeWay result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["FeeWayID"].ToString() != "")
				{
                    dictFeeWay.FeeWayID = int.Parse(dataSet.Tables[0].Rows[0]["FeeWayID"].ToString());
				}
                dictFeeWay.FeeWayName = dataSet.Tables[0].Rows[0]["FeeWayName"].ToString();
				if (dataSet.Tables[0].Rows[0]["Default"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Default"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Default"].ToString().ToLower() == "true")
					{
                        dictFeeWay.Default = true;
					}
					else
					{
                        dictFeeWay.Default = false;
					}
				}
                dictFeeWay.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dictFeeWay;
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
			stringBuilder.Append("select FeeWayID,FeeWayName,Default,InputCode ");
			stringBuilder.Append(" FROM DictFeeWay ");
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
			stringBuilder.Append(" FeeWayID,FeeWayName,Default,InputCode ");
			stringBuilder.Append(" FROM DictFeeWay ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
