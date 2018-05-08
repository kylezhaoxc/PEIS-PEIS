using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusConclusion : IBusConclusion
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_Conclusion", "BusConclusion");
		}

		public bool Exists(int ID_Conclusion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusConclusion");
			stringBuilder.Append(" where ID_Conclusion=@ID_Conclusion ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Conclusion;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusConclusion model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusConclusion(");
			stringBuilder.Append("ID_ConclusionType,ConclusionName,Explanation,Suggestion,DietGuide,SportsGuide,HealthKnowledge,Forsex,InputCode,DispOrder,TeamConclusionName,ID_Createopr,CreateOperator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,ID_ICD,ID_FinalConclusionType)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_ConclusionType,@ConclusionName,@Explanation,@Suggestion,@DietGuide,@SportsGuide,@HealthKnowledge,@Forsex,@InputCode,@DispOrder,@TeamConclusionName,@ID_Createopr,@CreateOperator,@CreateDate,@Is_Banned,@ID_BanOpr,@BanOperator,@BanDate,@BanDescribe,@ID_ICD,@ID_FinalConclusionType)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ConclusionType", SqlDbType.Int, 4),
				new SqlParameter("@ConclusionName", SqlDbType.VarChar, 50),
				new SqlParameter("@Explanation", SqlDbType.Text),
				new SqlParameter("@Suggestion", SqlDbType.Text),
				new SqlParameter("@DietGuide", SqlDbType.Text),
				new SqlParameter("@SportsGuide", SqlDbType.Text),
				new SqlParameter("@HealthKnowledge", SqlDbType.Text),
				new SqlParameter("@Forsex", SqlDbType.Int, 4),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@TeamConclusionName", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Createopr", SqlDbType.Int, 4),
				new SqlParameter("@CreateOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 100),
				new SqlParameter("@ID_ICD", SqlDbType.Int, 4),
				new SqlParameter("@ID_FinalConclusionType", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_ConclusionType;
			array[1].Value = model.ConclusionName;
			array[2].Value = model.Explanation;
			array[3].Value = model.Suggestion;
			array[4].Value = model.DietGuide;
			array[5].Value = model.SportsGuide;
			array[6].Value = model.HealthKnowledge;
			array[7].Value = model.ForGender;
			array[8].Value = model.InputCode;
			array[9].Value = model.DispOrder;
			array[10].Value = model.TeamConclusionName;
			array[11].Value = model.ID_Createopr;
			array[12].Value = model.CreateOperator;
			array[13].Value = model.CreateDate;
			array[14].Value = model.Is_Banned;
			array[15].Value = model.ID_BanOpr;
			array[16].Value = model.BanOperator;
			array[17].Value = model.BanDate;
			array[18].Value = model.BanDescribe;
			array[19].Value = model.ID_ICD;
			array[20].Value = model.ID_FinalConclusionType;
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

		public bool Update(PEIS.Model.BusConclusion model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusConclusion set ");
			stringBuilder.Append("ID_ConclusionType=@ID_ConclusionType,");
			stringBuilder.Append("ConclusionName=@ConclusionName,");
			stringBuilder.Append("Explanation=@Explanation,");
			stringBuilder.Append("Suggestion=@Suggestion,");
			stringBuilder.Append("DietGuide=@DietGuide,");
			stringBuilder.Append("SportsGuide=@SportsGuide,");
			stringBuilder.Append("HealthKnowledge=@HealthKnowledge,");
			stringBuilder.Append("Forsex=@Forsex,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("TeamConclusionName=@TeamConclusionName,");
			stringBuilder.Append("ID_Createopr=@ID_Createopr,");
			stringBuilder.Append("CreateOperator=@CreateOperator,");
			stringBuilder.Append("CreateDate=@CreateDate,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_BanOpr=@ID_BanOpr,");
			stringBuilder.Append("BanOperator=@BanOperator,");
			stringBuilder.Append("BanDate=@BanDate,");
			stringBuilder.Append("BanDescribe=@BanDescribe,");
			stringBuilder.Append("ID_ICD=@ID_ICD,");
			stringBuilder.Append("ID_FinalConclusionType=@ID_FinalConclusionType");
			stringBuilder.Append(" where ID_Conclusion=@ID_Conclusion");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ConclusionType", SqlDbType.Int, 4),
				new SqlParameter("@ConclusionName", SqlDbType.VarChar, 50),
				new SqlParameter("@Explanation", SqlDbType.Text),
				new SqlParameter("@Suggestion", SqlDbType.Text),
				new SqlParameter("@DietGuide", SqlDbType.Text),
				new SqlParameter("@SportsGuide", SqlDbType.Text),
				new SqlParameter("@HealthKnowledge", SqlDbType.Text),
				new SqlParameter("@Forsex", SqlDbType.Int, 4),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@TeamConclusionName", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Createopr", SqlDbType.Int, 4),
				new SqlParameter("@CreateOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 100),
				new SqlParameter("@ID_ICD", SqlDbType.Int, 4),
				new SqlParameter("@ID_FinalConclusionType", SqlDbType.Int, 4),
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_ConclusionType;
			array[1].Value = model.ConclusionName;
			array[2].Value = model.Explanation;
			array[3].Value = model.Suggestion;
			array[4].Value = model.DietGuide;
			array[5].Value = model.SportsGuide;
			array[6].Value = model.HealthKnowledge;
			array[7].Value = model.ForGender;
			array[8].Value = model.InputCode;
			array[9].Value = model.DispOrder;
			array[10].Value = model.TeamConclusionName;
			array[11].Value = model.ID_Createopr;
			array[12].Value = model.CreateOperator;
			array[13].Value = model.CreateDate;
			array[14].Value = model.Is_Banned;
			array[15].Value = model.ID_BanOpr;
			array[16].Value = model.BanOperator;
			array[17].Value = model.BanDate;
			array[18].Value = model.BanDescribe;
			array[19].Value = model.ID_ICD;
			array[20].Value = model.ID_FinalConclusionType;
			array[21].Value = model.ID_Conclusion;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_Conclusion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusConclusion ");
			stringBuilder.Append(" where ID_Conclusion=@ID_Conclusion");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Conclusion;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_Conclusionlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusConclusion ");
			stringBuilder.Append(" where ID_Conclusion in (" + ID_Conclusionlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusConclusion GetModel(int ID_Conclusion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_Conclusion,ID_ConclusionType,ConclusionName,Explanation,Suggestion,DietGuide,SportsGuide,HealthKnowledge,ForGender,InputCode,DispOrder,TeamConclusionName,ID_Createopr,CreateOperator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,ID_ICD,ID_FinalConclusionType from BusConclusion ");
			stringBuilder.Append(" where ID_Conclusion=@ID_Conclusion");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Conclusion;
			PEIS.Model.BusConclusion busConclusion = new PEIS.Model.BusConclusion();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusConclusion result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_Conclusion"].ToString() != "")
				{
					busConclusion.ID_Conclusion = int.Parse(dataSet.Tables[0].Rows[0]["ID_Conclusion"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_ConclusionType"].ToString() != "")
				{
					busConclusion.ID_ConclusionType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ConclusionType"].ToString()));
				}
				busConclusion.ConclusionName = dataSet.Tables[0].Rows[0]["ConclusionName"].ToString();
				busConclusion.Explanation = dataSet.Tables[0].Rows[0]["Explanation"].ToString();
				busConclusion.Suggestion = dataSet.Tables[0].Rows[0]["Suggestion"].ToString();
				busConclusion.DietGuide = dataSet.Tables[0].Rows[0]["DietGuide"].ToString();
				busConclusion.SportsGuide = dataSet.Tables[0].Rows[0]["SportsGuide"].ToString();
				busConclusion.HealthKnowledge = dataSet.Tables[0].Rows[0]["HealthKnowledge"].ToString();
				if (dataSet.Tables[0].Rows[0]["ForGender"].ToString() != "")
				{
					busConclusion.ForGender = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ForGender"].ToString()));
				}
				busConclusion.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busConclusion.DispOrder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString()));
				}
				busConclusion.TeamConclusionName = dataSet.Tables[0].Rows[0]["TeamConclusionName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Createopr"].ToString() != "")
				{
					busConclusion.ID_Createopr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Createopr"].ToString()));
				}
				busConclusion.CreateOperator = dataSet.Tables[0].Rows[0]["CreateOperator"].ToString();
				if (dataSet.Tables[0].Rows[0]["CreateDate"].ToString() != "")
				{
					busConclusion.CreateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busConclusion.Is_Banned = new bool?(true);
					}
					else
					{
						busConclusion.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString() != "")
				{
					busConclusion.ID_BanOpr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString()));
				}
				busConclusion.BanOperator = dataSet.Tables[0].Rows[0]["BanOperator"].ToString();
				if (dataSet.Tables[0].Rows[0]["BanDate"].ToString() != "")
				{
					busConclusion.BanDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["BanDate"].ToString()));
				}
				busConclusion.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_ICD"].ToString() != "")
				{
					busConclusion.ID_ICD = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ICD"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_FinalConclusionType"].ToString() != "")
				{
					busConclusion.ID_FinalConclusionType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FinalConclusionType"].ToString()));
				}
				result = busConclusion;
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
			stringBuilder.Append("select ID_Conclusion,ID_ConclusionType,ConclusionName,Explanation,Suggestion,DietGuide,SportsGuide,HealthKnowledge,ForGender,InputCode,DispOrder,TeamConclusionName,ID_Createopr,CreateOperator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,ID_ICD,ID_FinalConclusionType ");
			stringBuilder.Append(" FROM BusConclusion ");
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
			stringBuilder.Append(" ID_Conclusion,ID_ConclusionType,ConclusionName,Explanation,Suggestion,DietGuide,SportsGuide,HealthKnowledge,ForGender,InputCode,DispOrder,TeamConclusionName,ID_Createopr,CreateOperator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,ID_ICD,ID_FinalConclusionType ");
			stringBuilder.Append(" FROM BusConclusion ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
