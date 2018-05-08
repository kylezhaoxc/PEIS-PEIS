using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustConclusion : IOnCustConclusion
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_CustConclusion", "OnCustConclusion");
		}

		public bool Exists(int ID_CustConclusion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustConclusion");
			stringBuilder.Append(" where ID_CustConclusion=@ID_CustConclusion ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustConclusion", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustConclusion;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustConclusion model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustConclusion(");
			stringBuilder.Append("ID_Customer,ConclusionName,ConclusionTypeName,Is_NoEntrySuggestion,Explanation,Suggestion,DietGuide,SportGuide,HealthKnowledge,ID_Doctor,DoctorName,ConclusionDate,ID_Conclusion,DispOrder,DiagnoseType)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Customer,@ConclusionName,@ConclusionTypeName,@Is_NoEntrySuggestion,@Explanation,@Suggestion,@DietGuide,@SportGuide,@HealthKnowledge,@ID_Doctor,@DoctorName,@ConclusionDate,@ID_Conclusion,@DispOrder,@DiagnoseType)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@ConclusionName", SqlDbType.VarChar, 200),
				new SqlParameter("@ConclusionTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@Is_NoEntrySuggestion", SqlDbType.Bit, 1),
				new SqlParameter("@Explanation", SqlDbType.Text),
				new SqlParameter("@Suggestion", SqlDbType.Text),
				new SqlParameter("@DietGuide", SqlDbType.Text),
				new SqlParameter("@SportGuide", SqlDbType.Text),
				new SqlParameter("@HealthKnowledge", SqlDbType.Text),
				new SqlParameter("@ID_Doctor", SqlDbType.Int, 4),
				new SqlParameter("@DoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@ConclusionDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@DiagnoseType", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ConclusionName;
			array[2].Value = model.ConclusionTypeName;
			array[3].Value = model.Is_NoEntrySuggestion;
			array[4].Value = model.Explanation;
			array[5].Value = model.Suggestion;
			array[6].Value = model.DietGuide;
			array[7].Value = model.SportGuide;
			array[8].Value = model.HealthKnowledge;
			array[9].Value = model.ID_Doctor;
			array[10].Value = model.DoctorName;
			array[11].Value = model.ConclusionDate;
			array[12].Value = model.ID_Conclusion;
			array[13].Value = model.DispOrder;
			array[14].Value = model.DiagnoseType;
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

		public bool Update(PEIS.Model.OnCustConclusion model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustConclusion set ");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("ConclusionName=@ConclusionName,");
			stringBuilder.Append("ConclusionTypeName=@ConclusionTypeName,");
			stringBuilder.Append("Is_NoEntrySuggestion=@Is_NoEntrySuggestion,");
			stringBuilder.Append("Explanation=@Explanation,");
			stringBuilder.Append("Suggestion=@Suggestion,");
			stringBuilder.Append("DietGuide=@DietGuide,");
			stringBuilder.Append("SportGuide=@SportGuide,");
			stringBuilder.Append("HealthKnowledge=@HealthKnowledge,");
			stringBuilder.Append("ID_Doctor=@ID_Doctor,");
			stringBuilder.Append("DoctorName=@DoctorName,");
			stringBuilder.Append("ConclusionDate=@ConclusionDate,");
			stringBuilder.Append("ID_Conclusion=@ID_Conclusion,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("DiagnoseType=@DiagnoseType");
			stringBuilder.Append(" where ID_CustConclusion=@ID_CustConclusion");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@ConclusionName", SqlDbType.VarChar, 200),
				new SqlParameter("@ConclusionTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@Is_NoEntrySuggestion", SqlDbType.Bit, 1),
				new SqlParameter("@Explanation", SqlDbType.Text),
				new SqlParameter("@Suggestion", SqlDbType.Text),
				new SqlParameter("@DietGuide", SqlDbType.Text),
				new SqlParameter("@SportGuide", SqlDbType.Text),
				new SqlParameter("@HealthKnowledge", SqlDbType.Text),
				new SqlParameter("@ID_Doctor", SqlDbType.Int, 4),
				new SqlParameter("@DoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@ConclusionDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@DiagnoseType", SqlDbType.Int, 4),
				new SqlParameter("@ID_CustConclusion", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ConclusionName;
			array[2].Value = model.ConclusionTypeName;
			array[3].Value = model.Is_NoEntrySuggestion;
			array[4].Value = model.Explanation;
			array[5].Value = model.Suggestion;
			array[6].Value = model.DietGuide;
			array[7].Value = model.SportGuide;
			array[8].Value = model.HealthKnowledge;
			array[9].Value = model.ID_Doctor;
			array[10].Value = model.DoctorName;
			array[11].Value = model.ConclusionDate;
			array[12].Value = model.ID_Conclusion;
			array[13].Value = model.DispOrder;
			array[14].Value = model.DiagnoseType;
			array[15].Value = model.ID_CustConclusion;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_CustConclusion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustConclusion ");
			stringBuilder.Append(" where ID_CustConclusion=@ID_CustConclusion");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustConclusion", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustConclusion;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_CustConclusionlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustConclusion ");
			stringBuilder.Append(" where ID_CustConclusion in (" + ID_CustConclusionlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustConclusion GetModel(int ID_CustConclusion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_CustConclusion,ID_Customer,ConclusionName,ConclusionTypeName,Is_NoEntrySuggestion,Explanation,Suggestion,DietGuide,SportGuide,HealthKnowledge,ID_Doctor,DoctorName,ConclusionDate,ID_Conclusion,DispOrder,DiagnoseType from OnCustConclusion ");
			stringBuilder.Append(" where ID_CustConclusion=@ID_CustConclusion");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustConclusion", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustConclusion;
			PEIS.Model.OnCustConclusion onCustConclusion = new PEIS.Model.OnCustConclusion();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustConclusion result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_CustConclusion"].ToString() != "")
				{
					onCustConclusion.ID_CustConclusion = int.Parse(dataSet.Tables[0].Rows[0]["ID_CustConclusion"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onCustConclusion.ID_Customer = new long?(long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString()));
				}
				onCustConclusion.ConclusionName = dataSet.Tables[0].Rows[0]["ConclusionName"].ToString();
				onCustConclusion.ConclusionTypeName = dataSet.Tables[0].Rows[0]["ConclusionTypeName"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_NoEntrySuggestion"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_NoEntrySuggestion"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_NoEntrySuggestion"].ToString().ToLower() == "true")
					{
						onCustConclusion.Is_NoEntrySuggestion = new bool?(true);
					}
					else
					{
						onCustConclusion.Is_NoEntrySuggestion = new bool?(false);
					}
				}
				onCustConclusion.Explanation = dataSet.Tables[0].Rows[0]["Explanation"].ToString();
				onCustConclusion.Suggestion = dataSet.Tables[0].Rows[0]["Suggestion"].ToString();
				onCustConclusion.DietGuide = dataSet.Tables[0].Rows[0]["DietGuide"].ToString();
				onCustConclusion.SportGuide = dataSet.Tables[0].Rows[0]["SportGuide"].ToString();
				onCustConclusion.HealthKnowledge = dataSet.Tables[0].Rows[0]["HealthKnowledge"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Doctor"].ToString() != "")
				{
					onCustConclusion.ID_Doctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Doctor"].ToString()));
				}
				onCustConclusion.DoctorName = dataSet.Tables[0].Rows[0]["DoctorName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ConclusionDate"].ToString() != "")
				{
					onCustConclusion.ConclusionDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["ConclusionDate"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Conclusion"].ToString() != "")
				{
					onCustConclusion.ID_Conclusion = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Conclusion"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					onCustConclusion.DispOrder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["DiagnoseType"].ToString() != "")
				{
					onCustConclusion.DiagnoseType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DiagnoseType"].ToString()));
				}
				result = onCustConclusion;
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
			stringBuilder.Append("select ID_CustConclusion,ID_Customer,ConclusionName,ConclusionTypeName,Is_NoEntrySuggestion,Explanation,Suggestion,DietGuide,SportGuide,HealthKnowledge,ID_Doctor,DoctorName,ConclusionDate,ID_Conclusion,DispOrder,DiagnoseType ");
			stringBuilder.Append(" FROM OnCustConclusion ");
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
			stringBuilder.Append(" ID_CustConclusion,ID_Customer,ConclusionName,ConclusionTypeName,Is_NoEntrySuggestion,Explanation,Suggestion,DietGuide,SportGuide,HealthKnowledge,ID_Doctor,DoctorName,ConclusionDate,ID_Conclusion,DispOrder,DiagnoseType ");
			stringBuilder.Append(" FROM OnCustConclusion ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
