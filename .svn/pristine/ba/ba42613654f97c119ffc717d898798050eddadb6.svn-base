using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictMarriage : IDictMarriage
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("MarriageID", "DictMarriage");
		}

		public bool Exists(int MarriageID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictMarriage");
			stringBuilder.Append(" where MarriageID=@MarriageID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@MarriageID", SqlDbType.Int, 4)
			};
			array[0].Value = MarriageID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictMarriage model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictMarriage(");
			stringBuilder.Append("MarriageName,IsMarried,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@MarriageName,@Is_Married,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@MarriageName", SqlDbType.VarChar, 8),
				new SqlParameter("@IsMarried", SqlDbType.Int, 4),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8)
			};
			array[0].Value = model.MarriageName;
			array[1].Value = model.IsMarried;
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

		public bool Update(PEIS.Model.DictMarriage model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictMarriage set ");
			stringBuilder.Append("MarriageName=@MarriageName,");
			stringBuilder.Append("IsMarried=@IsMarried,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where MarriageID=@MarriageID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@MarriageName", SqlDbType.VarChar, 8),
				new SqlParameter("@IsMarried", SqlDbType.Int, 4),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8),
				new SqlParameter("@MarriageID", SqlDbType.Int, 4)
			};
			array[0].Value = model.MarriageName;
			array[1].Value = model.IsMarried;
			array[2].Value = model.InputCode;
			array[3].Value = model.MarriageID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int MarriageID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictMarriage ");
			stringBuilder.Append(" where MarriageID=@MarriageID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@MarriageID", SqlDbType.Int, 4)
			};
			array[0].Value = MarriageID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string MarriageIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictMarriage ");
			stringBuilder.Append(" where MarriageID in (" + MarriageIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictMarriage GetModel(int MarriageID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 MarriageID,MarriageName,IsMarried,InputCode from DictMarriage ");
			stringBuilder.Append(" where MarriageID=@MarriageID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@MarriageID", SqlDbType.Int, 4)
			};
			array[0].Value = MarriageID;
			PEIS.Model.DictMarriage marriage = new PEIS.Model.DictMarriage();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictMarriage result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["MarriageID"].ToString() != "")
				{
                    marriage.MarriageID = int.Parse(dataSet.Tables[0].Rows[0]["MarriageID"].ToString());
				}
                marriage.MarriageName = dataSet.Tables[0].Rows[0]["MarriageName"].ToString();
				if (dataSet.Tables[0].Rows[0]["IsMarried"].ToString() != "")
				{
                    marriage.IsMarried = int.Parse(dataSet.Tables[0].Rows[0]["IsMarried"].ToString());
				}
                marriage.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = marriage;
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
			stringBuilder.Append("select MarriageID,MarriageName,IsMarried,InputCode ");
			stringBuilder.Append(" FROM DictMarriage ");
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
			stringBuilder.Append(" MarriageID,MarriageName,IsMarried,InputCode ");
			stringBuilder.Append(" FROM DictMarriage ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
