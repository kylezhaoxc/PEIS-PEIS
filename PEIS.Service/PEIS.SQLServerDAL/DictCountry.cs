using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DoctCountry : IDictCountry
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CountryID", "DoctCountry");
		}

		public bool Exists(int CountryID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictCountry");
			stringBuilder.Append(" where CountryID=@CountryID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CountryID", SqlDbType.Int, 4)
			};
			array[0].Value = CountryID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictCountry model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictCountry(");
			stringBuilder.Append("CountryName,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@CountryName,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CountryName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8)
			};
			array[0].Value = model.CountryName;
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

		public bool Update(PEIS.Model.DictCountry model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictCountry set ");
			stringBuilder.Append("CountryName=@CountryName,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where CountryID=@CountryID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CountryName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8),
				new SqlParameter("@CountryID", SqlDbType.Int, 4)
			};
			array[0].Value = model.CountryName;
			array[1].Value = model.InputCode;
			array[2].Value = model.CountryID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int CountryID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictCountry ");
			stringBuilder.Append(" where CountryID=@CountryID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CountryID", SqlDbType.Int, 4)
			};
			array[0].Value = CountryID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string CountryIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictCountry ");
			stringBuilder.Append(" where CountryID in (" + CountryIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictCountry GetModel(int CountryID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 CountryID,CountryName,InputCode from DictCountry ");
			stringBuilder.Append(" where CountryID=@CountryID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CountryID", SqlDbType.Int, 4)
			};
			array[0].Value = CountryID;
			PEIS.Model.DictCountry dctCountry = new PEIS.Model.DictCountry();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictCountry result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["CountryID"].ToString() != "")
				{
					dctCountry.CountryID = int.Parse(dataSet.Tables[0].Rows[0]["CountryID"].ToString());
				}
				dctCountry.CountryName = dataSet.Tables[0].Rows[0]["CountryName"].ToString();
				dctCountry.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dctCountry;
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
			stringBuilder.Append("select CountryID,CountryName,InputCode ");
			stringBuilder.Append(" FROM DictCountry ");
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
			stringBuilder.Append(" CountryID,CountryName,InputCode ");
			stringBuilder.Append(" FROM DictCountry ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
