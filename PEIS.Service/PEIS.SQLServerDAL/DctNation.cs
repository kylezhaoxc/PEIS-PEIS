using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictNation : IDictNation
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("NationID", "DictNation");
		}

		public bool Exists(int NationID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictNation");
			stringBuilder.Append(" where NationID=@NationID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NationID", SqlDbType.Int, 4)
			};
			array[0].Value = NationID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictNation model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictNation(");
			stringBuilder.Append("NationName,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@NationName,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NationName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8)
			};
			array[0].Value = model.NationName;
			array[1].Value = model.InputCode;
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

		public bool Update(PEIS.Model.DictNation model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictNation set ");
			stringBuilder.Append("NationName=@NationName,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where NationID=@NationID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NationName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8),
				new SqlParameter("@NationID", SqlDbType.Int, 4)
			};
			array[0].Value = model.NationName;
			array[1].Value = model.InputCode;
			array[2].Value = model.NationID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int NationID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictNation ");
			stringBuilder.Append(" where NationID=@NationID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NationID", SqlDbType.Int, 4)
			};
			array[0].Value = NationID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string NationIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictNation ");
			stringBuilder.Append(" where NationID in (" + NationIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictNation GetModel(int NationID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 NationID,NationName,InputCode from DictNation ");
			stringBuilder.Append(" where NationID=@NationID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NationID", SqlDbType.Int, 4)
			};
			array[0].Value = NationID;
			PEIS.Model.DictNation dctNation = new PEIS.Model.DictNation();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictNation result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["NationID"].ToString() != "")
				{
					dctNation.NationID = int.Parse(dataSet.Tables[0].Rows[0]["NationID"].ToString());
				}
				dctNation.NationName = dataSet.Tables[0].Rows[0]["NationName"].ToString();
				dctNation.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dctNation;
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
			stringBuilder.Append("select NationID,NationName,InputCode ");
			stringBuilder.Append(" FROM DictNation ");
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
			stringBuilder.Append(" NationID,NationName,InputCode ");
			stringBuilder.Append(" FROM DictNation ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
