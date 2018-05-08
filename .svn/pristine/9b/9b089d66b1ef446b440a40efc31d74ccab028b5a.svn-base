using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictCultrul : IDictCultrul
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CultrulID", "DictCultrul");
		}

		public bool Exists(int CultrulID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictCultrul");
			stringBuilder.Append(" where CultrulID=@CultrulID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CultrulID", SqlDbType.Int, 4)
			};
			array[0].Value = CultrulID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictCultrul model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictCultrul(");
			stringBuilder.Append("CultrulName,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@CultrulName,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CultrulName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8)
			};
			array[0].Value = model.CultrulName;
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

		public bool Update(PEIS.Model.DictCultrul model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictCultrul set ");
			stringBuilder.Append("CultrulName=@CultrulName,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where CultrulID=@CultrulID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CultrulName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8),
				new SqlParameter("@CultrulID", SqlDbType.Int, 4)
			};
			array[0].Value = model.CultrulName;
			array[1].Value = model.InputCode;
			array[2].Value = model.CultrulID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int CultrulID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictCultrul ");
			stringBuilder.Append(" where CultrulID=@CultrulID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CultrulID", SqlDbType.Int, 4)
			};
			array[0].Value = CultrulID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string CultrulIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictCultrul ");
			stringBuilder.Append(" where CultrulID in (" + CultrulIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictCultrul GetModel(int CultrulID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 CultrulID,CultrulName,InputCode from DictCultrul ");
			stringBuilder.Append(" where CultrulID=@CultrulID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CultrulID", SqlDbType.Int, 4)
			};
			array[0].Value = CultrulID;
			PEIS.Model.DictCultrul dctCultrul = new PEIS.Model.DictCultrul();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictCultrul result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["CultrulID"].ToString() != "")
				{
					dctCultrul.CultrulID = int.Parse(dataSet.Tables[0].Rows[0]["CultrulID"].ToString());
				}
				dctCultrul.CultrulName = dataSet.Tables[0].Rows[0]["CultrulName"].ToString();
				dctCultrul.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dctCultrul;
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
			stringBuilder.Append("select CultrulID,CultrulName,InputCode ");
			stringBuilder.Append(" FROM DictCultrul ");
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
			stringBuilder.Append(" CultrulID,CultrulName,InputCode ");
			stringBuilder.Append(" FROM DictCultrul ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
