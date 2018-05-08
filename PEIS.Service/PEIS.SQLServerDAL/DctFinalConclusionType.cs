using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DctFinalConclusionType : IDctFinalConclusionType
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_FinalConclusionType", "DctFinalConclusionType");
		}

		public bool Exists(int ID_FinalConclusionType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DctFinalConclusionType");
			stringBuilder.Append(" where ID_FinalConclusionType=@ID_FinalConclusionType ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FinalConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FinalConclusionType;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DctFinalConclusionType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DctFinalConclusionType(");
			stringBuilder.Append("FinalConclusionTypeName,InputCode,Note,ID_Creator,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,BanDate,BanOperator,FinalConclusionSignCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@FinalConclusionTypeName,@InputCode,@Note,@ID_Creator,@CreateDate,@Is_Banned,@ID_BanOpr,@BanDescribe,@DispOrder,@BanDate,@BanOperator,@FinalConclusionSignCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FinalConclusionTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@Note", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Creator", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@FinalConclusionSignCode", SqlDbType.VarChar, 30)
			};
			array[0].Value = model.FinalConclusionTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.Note;
			array[3].Value = model.ID_Creator;
			array[4].Value = model.CreateDate;
			array[5].Value = model.Is_Banned;
			array[6].Value = model.ID_BanOpr;
			array[7].Value = model.BanDescribe;
			array[8].Value = model.DispOrder;
			array[9].Value = model.BanDate;
			array[10].Value = model.BanOperator;
			array[11].Value = model.FinalConclusionSignCode;
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

		public bool Update(PEIS.Model.DctFinalConclusionType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DctFinalConclusionType set ");
			stringBuilder.Append("FinalConclusionTypeName=@FinalConclusionTypeName,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("ID_Creator=@ID_Creator,");
			stringBuilder.Append("CreateDate=@CreateDate,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_BanOpr=@ID_BanOpr,");
			stringBuilder.Append("BanDescribe=@BanDescribe,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("BanDate=@BanDate,");
			stringBuilder.Append("BanOperator=@BanOperator,");
			stringBuilder.Append("FinalConclusionSignCode=@FinalConclusionSignCode");
			stringBuilder.Append(" where ID_FinalConclusionType=@ID_FinalConclusionType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@FinalConclusionTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@Note", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Creator", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@FinalConclusionSignCode", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_FinalConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = model.FinalConclusionTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.Note;
			array[3].Value = model.ID_Creator;
			array[4].Value = model.CreateDate;
			array[5].Value = model.Is_Banned;
			array[6].Value = model.ID_BanOpr;
			array[7].Value = model.BanDescribe;
			array[8].Value = model.DispOrder;
			array[9].Value = model.BanDate;
			array[10].Value = model.BanOperator;
			array[11].Value = model.FinalConclusionSignCode;
			array[12].Value = model.ID_FinalConclusionType;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_FinalConclusionType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DctFinalConclusionType ");
			stringBuilder.Append(" where ID_FinalConclusionType=@ID_FinalConclusionType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FinalConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FinalConclusionType;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_FinalConclusionTypelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DctFinalConclusionType ");
			stringBuilder.Append(" where ID_FinalConclusionType in (" + ID_FinalConclusionTypelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DctFinalConclusionType GetModel(int ID_FinalConclusionType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_FinalConclusionType,FinalConclusionTypeName,InputCode,Note,ID_Creator,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,BanDate,BanOperator,FinalConclusionSignCode from DctFinalConclusionType ");
			stringBuilder.Append(" where ID_FinalConclusionType=@ID_FinalConclusionType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FinalConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FinalConclusionType;
			PEIS.Model.DctFinalConclusionType dctFinalConclusionType = new PEIS.Model.DctFinalConclusionType();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DctFinalConclusionType result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_FinalConclusionType"].ToString() != "")
				{
					dctFinalConclusionType.ID_FinalConclusionType = int.Parse(dataSet.Tables[0].Rows[0]["ID_FinalConclusionType"].ToString());
				}
				dctFinalConclusionType.FinalConclusionTypeName = dataSet.Tables[0].Rows[0]["FinalConclusionTypeName"].ToString();
				dctFinalConclusionType.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				dctFinalConclusionType.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Creator"].ToString() != "")
				{
					dctFinalConclusionType.ID_Creator = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Creator"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["CreateDate"].ToString() != "")
				{
					dctFinalConclusionType.CreateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						dctFinalConclusionType.Is_Banned = new bool?(true);
					}
					else
					{
						dctFinalConclusionType.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString() != "")
				{
					dctFinalConclusionType.ID_BanOpr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString()));
				}
				dctFinalConclusionType.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					dctFinalConclusionType.DispOrder = int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["BanDate"].ToString() != "")
				{
					dctFinalConclusionType.BanDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["BanDate"].ToString()));
				}
				dctFinalConclusionType.BanOperator = dataSet.Tables[0].Rows[0]["BanOperator"].ToString();
				dctFinalConclusionType.FinalConclusionSignCode = dataSet.Tables[0].Rows[0]["FinalConclusionSignCode"].ToString();
				result = dctFinalConclusionType;
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
			stringBuilder.Append("select ID_FinalConclusionType,FinalConclusionTypeName,InputCode,Note,ID_Creator,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,BanDate,BanOperator,FinalConclusionSignCode ");
			stringBuilder.Append(" FROM DctFinalConclusionType ");
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
			stringBuilder.Append(" ID_FinalConclusionType,FinalConclusionTypeName,InputCode,Note,ID_Creator,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,BanDate,BanOperator,FinalConclusionSignCode ");
			stringBuilder.Append(" FROM DctFinalConclusionType ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
