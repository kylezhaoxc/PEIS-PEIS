using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusConclusionType : IBusConclusionType
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ConclusionType", "BusConclusionType");
		}

		public bool Exists(int ID_ConclusionType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusConclusionType");
			stringBuilder.Append(" where ID_ConclusionType=@ID_ConclusionType ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ConclusionType;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusConclusionType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusConclusionType(");
			stringBuilder.Append("ConclusionTypeName,InputCode,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ConclusionTypeName,@InputCode,@Is_Banned,@ID_BanOpr,@BanOperator,@BanDate,@BanDescribe)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ConclusionTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 100)
			};
			array[0].Value = model.ConclusionTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.Is_Banned;
			array[3].Value = model.ID_BanOpr;
			array[4].Value = model.BanOperator;
			array[5].Value = model.BanDate;
			array[6].Value = model.BanDescribe;
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

		public bool Update(PEIS.Model.BusConclusionType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusConclusionType set ");
			stringBuilder.Append("ConclusionTypeName=@ConclusionTypeName,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_BanOpr=@ID_BanOpr,");
			stringBuilder.Append("BanOperator=@BanOperator,");
			stringBuilder.Append("BanDate=@BanDate,");
			stringBuilder.Append("BanDescribe=@BanDescribe");
			stringBuilder.Append(" where ID_ConclusionType=@ID_ConclusionType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ConclusionTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 100),
				new SqlParameter("@ID_ConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = model.ConclusionTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.Is_Banned;
			array[3].Value = model.ID_BanOpr;
			array[4].Value = model.BanOperator;
			array[5].Value = model.BanDate;
			array[6].Value = model.BanDescribe;
			array[7].Value = model.ID_ConclusionType;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ConclusionType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusConclusionType ");
			stringBuilder.Append(" where ID_ConclusionType=@ID_ConclusionType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ConclusionType;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ConclusionTypelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusConclusionType ");
			stringBuilder.Append(" where ID_ConclusionType in (" + ID_ConclusionTypelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusConclusionType GetModel(int ID_ConclusionType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ConclusionType,ConclusionTypeName,InputCode,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe from BusConclusionType ");
			stringBuilder.Append(" where ID_ConclusionType=@ID_ConclusionType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ConclusionType;
			PEIS.Model.BusConclusionType busConclusionType = new PEIS.Model.BusConclusionType();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusConclusionType result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ConclusionType"].ToString() != "")
				{
					busConclusionType.ID_ConclusionType = int.Parse(dataSet.Tables[0].Rows[0]["ID_ConclusionType"].ToString());
				}
				busConclusionType.ConclusionTypeName = dataSet.Tables[0].Rows[0]["ConclusionTypeName"].ToString();
				busConclusionType.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busConclusionType.Is_Banned = new bool?(true);
					}
					else
					{
						busConclusionType.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString() != "")
				{
					busConclusionType.ID_BanOpr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString()));
				}
				busConclusionType.BanOperator = dataSet.Tables[0].Rows[0]["BanOperator"].ToString();
				if (dataSet.Tables[0].Rows[0]["BanDate"].ToString() != "")
				{
					busConclusionType.BanDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["BanDate"].ToString()));
				}
				busConclusionType.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				result = busConclusionType;
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
			stringBuilder.Append("select ID_ConclusionType,ConclusionTypeName,InputCode,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe ");
			stringBuilder.Append(" FROM BusConclusionType ");
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
			stringBuilder.Append(" ID_ConclusionType,ConclusionTypeName,InputCode,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe ");
			stringBuilder.Append(" FROM BusConclusionType ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
