using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictExamType : IDictExamType
    {
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ExamTypeID", "DictExamType");
		}

		public bool Exists(int ExamTypeID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from ExamType");
			stringBuilder.Append(" where ExamTypeID=@ExamTypeID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamTypeID", SqlDbType.Int, 4)
			};
			array[0].Value = ExamTypeID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictExamType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictExamType(");
			stringBuilder.Append("ExamTypeName,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ExamTypeName,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10)
			};
			array[0].Value = model.ExamTypeName;
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

		public bool Update(PEIS.Model.DictExamType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictExamType set ");
			stringBuilder.Append("ExamTypeName=@ExamTypeName,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where ExamTypeID=@ExamTypeID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@ExamTypeID", SqlDbType.Int, 4)
			};
			array[0].Value = model.ExamTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.ExamTypeID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ExamTypeID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictExamType ");
			stringBuilder.Append(" where ExamTypeID=@ExamTypeID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamTypeID", SqlDbType.Int, 4)
			};
			array[0].Value = ExamTypeID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ExamTypeIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictExamType ");
			stringBuilder.Append(" where ExamTypeID in (" + ExamTypeIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictExamType GetModel(int ExamTypeID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ExamTypeID,ExamTypeName,InputCode,Is_Document from DictExamType ");
			stringBuilder.Append(" where ExamTypeID=@ExamTypeID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamTypeID", SqlDbType.Int, 4)
			};
			array[0].Value = ExamTypeID;
			PEIS.Model.DictExamType dictExamType = new PEIS.Model.DictExamType();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictExamType result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ExamTypeID"].ToString() != "")
				{
                    dictExamType.ExamTypeID = int.Parse(dataSet.Tables[0].Rows[0]["ExamTypeID"].ToString());
				}
                dictExamType.ExamTypeName = dataSet.Tables[0].Rows[0]["ExamTypeName"].ToString();
                dictExamType.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["DocumentID"].ToString() != "")
				{
                    dictExamType.DocumentID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DocumentID"].ToString()));
				}
				result = dictExamType;
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
			stringBuilder.Append("select ExamTypeID,ExamTypeName,InputCode ");
			stringBuilder.Append(" FROM DictExamType ");
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
			stringBuilder.Append(" ExamTypeID,ExamTypeName,InputCode ");
			stringBuilder.Append(" FROM DictExamType ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
