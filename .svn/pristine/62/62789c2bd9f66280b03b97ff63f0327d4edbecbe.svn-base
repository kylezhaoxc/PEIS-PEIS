using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictGender : IDictGender
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GenderID", "DictGender");
		}

		public bool Exists(int GenderID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictGender");
			stringBuilder.Append(" where GenderID=@GenderID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@GenderID", SqlDbType.Int, 4)
			};
			array[0].Value = GenderID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictGender model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictGender(");
			stringBuilder.Append("GenderName,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@GenderName,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@GenderName", SqlDbType.VarChar, 8),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 4)
			};
			array[0].Value = model.GenderName;
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

		public bool Update(PEIS.Model.DictGender model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictGender set ");
			stringBuilder.Append("GenderName=@GenderName,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where GenderID=@GenderID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@GenderName", SqlDbType.VarChar, 8),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 4),
				new SqlParameter("@GenderID", SqlDbType.Int, 4)
			};
			array[0].Value = model.GenderName;
			array[1].Value = model.InputCode;
			array[2].Value = model.GenderID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int GenderID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictGender ");
			stringBuilder.Append(" where GenderID=@GenderID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@GenderID", SqlDbType.Int, 4)
			};
			array[0].Value = GenderID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string GenderIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictGender ");
			stringBuilder.Append(" where GenderID in (" + GenderIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictGender GetModel(int GenderID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 GenderID,GenderName,InputCode from DictGender ");
			stringBuilder.Append(" where GenderID=@GenderID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@GenderID", SqlDbType.Int, 4)
			};
			array[0].Value = GenderID;
			PEIS.Model.DictGender dictGender = new PEIS.Model.DictGender();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictGender result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["GenderID"].ToString() != "")
				{
                    dictGender.GenderID = int.Parse(dataSet.Tables[0].Rows[0]["GenderID"].ToString());
				}
                dictGender.GenderName = dataSet.Tables[0].Rows[0]["GenderName"].ToString();
                dictGender.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dictGender;
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
			stringBuilder.Append("select GenderID,GenderName,InputCode ");
			stringBuilder.Append(" FROM DictGender ");
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
			stringBuilder.Append(" GenderID,GenderName,InputCode ");
			stringBuilder.Append(" FROM DictGender ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
