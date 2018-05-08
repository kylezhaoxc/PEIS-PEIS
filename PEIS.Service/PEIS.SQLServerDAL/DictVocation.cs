using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictVocation : IDictVocation
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("VocationID", "DictVocation");
		}

		public bool Exists(int VocationID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictVocation");
			stringBuilder.Append(" where VocationID=@VocationID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@VocationID", SqlDbType.Int, 4)
			};
			array[0].Value = VocationID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictVocation model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictVocation(");
			stringBuilder.Append("VocationName,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@VocationName,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@VocationName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8)
			};
			array[0].Value = model.VocationName;
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

		public bool Update(PEIS.Model.DictVocation model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictVocation set ");
			stringBuilder.Append("VocationName=@VocationName,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where VocationID=@VocationID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@VocationName", SqlDbType.VarChar, 10),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 8),
				new SqlParameter("@VocationID", SqlDbType.Int, 4)
			};
			array[0].Value = model.VocationName;
			array[1].Value = model.InputCode;
			array[2].Value = model.VocationID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int VocationID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictVocation ");
			stringBuilder.Append(" where VocationID=@VocationID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@VocationID", SqlDbType.Int, 4)
			};
			array[0].Value = VocationID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string VocationIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictVocation ");
			stringBuilder.Append(" where VocationID in (" + VocationIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictVocation GetModel(int VocationID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 VocationID,VocationName,InputCode from DictVocation ");
			stringBuilder.Append(" where VocationID=@VocationID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@VocationID", SqlDbType.Int, 4)
			};
			array[0].Value = VocationID;
			PEIS.Model.DictVocation dctVocation = new PEIS.Model.DictVocation();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictVocation result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["VocationID"].ToString() != "")
				{
					dctVocation.VocationID = int.Parse(dataSet.Tables[0].Rows[0]["VocationID"].ToString());
				}
				dctVocation.VocationName = dataSet.Tables[0].Rows[0]["VocationName"].ToString();
				dctVocation.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dctVocation;
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
			stringBuilder.Append("select VocationID,VocationName,InputCode ");
			stringBuilder.Append(" FROM DictVocation ");
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
			stringBuilder.Append(" VocationID,VocationName,InputCode ");
			stringBuilder.Append(" FROM DictVocation ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
