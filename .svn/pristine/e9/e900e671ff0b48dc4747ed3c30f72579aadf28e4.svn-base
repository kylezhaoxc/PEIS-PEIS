using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustExamItemResult : IOnCustExamItemResult
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ExamItemResult", "OnCustExamItemResult");
		}

		public bool Exists(int ID_ExamItemResult)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustExamItemResult");
			stringBuilder.Append(" where ID_ExamItemResult=@ID_ExamItemResult ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItemResult", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItemResult;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustExamItemResult model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustExamItemResult(");
			stringBuilder.Append("ID_CustExamItem,ID_Symptom)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_CustExamItem,@ID_Symptom)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ID_Symptom", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_CustExamItem;
			array[1].Value = model.ID_Symptom;
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

		public bool Update(PEIS.Model.OnCustExamItemResult model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustExamItemResult set ");
			stringBuilder.Append("ID_CustExamItem=@ID_CustExamItem,");
			stringBuilder.Append("ID_Symptom=@ID_Symptom");
			stringBuilder.Append(" where ID_ExamItemResult=@ID_ExamItemResult");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ID_Symptom", SqlDbType.Int, 4),
				new SqlParameter("@ID_ExamItemResult", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_CustExamItem;
			array[1].Value = model.ID_Symptom;
			array[2].Value = model.ID_ExamItemResult;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ExamItemResult)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustExamItemResult ");
			stringBuilder.Append(" where ID_ExamItemResult=@ID_ExamItemResult");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItemResult", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItemResult;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ExamItemResultlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustExamItemResult ");
			stringBuilder.Append(" where ID_ExamItemResult in (" + ID_ExamItemResultlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustExamItemResult GetModel(int ID_ExamItemResult)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ExamItemResult,ID_CustExamItem,ID_Symptom from OnCustExamItemResult ");
			stringBuilder.Append(" where ID_ExamItemResult=@ID_ExamItemResult");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItemResult", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItemResult;
			PEIS.Model.OnCustExamItemResult onCustExamItemResult = new PEIS.Model.OnCustExamItemResult();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustExamItemResult result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ExamItemResult"].ToString() != "")
				{
					onCustExamItemResult.ID_ExamItemResult = int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItemResult"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_CustExamItem"].ToString() != "")
				{
					onCustExamItemResult.ID_CustExamItem = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_CustExamItem"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Symptom"].ToString() != "")
				{
					onCustExamItemResult.ID_Symptom = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Symptom"].ToString()));
				}
				result = onCustExamItemResult;
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
			stringBuilder.Append("select ID_ExamItemResult,ID_CustExamItem,ID_Symptom ");
			stringBuilder.Append(" FROM OnCustExamItemResult ");
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
			stringBuilder.Append(" ID_ExamItemResult,ID_CustExamItem,ID_Symptom ");
			stringBuilder.Append(" FROM OnCustExamItemResult ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
