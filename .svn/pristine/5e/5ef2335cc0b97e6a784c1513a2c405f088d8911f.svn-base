using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DictExamPlace : IDictExamPlace
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ExamPlaceID", "DictExamPlace");
		}

		public bool Exists(int ExamPlaceID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DictExamPlace");
			stringBuilder.Append(" where ExamPlaceID=@ExamPlaceID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamPlaceID", SqlDbType.Int, 4)
			};
			array[0].Value = ExamPlaceID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DictExamPlace model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DictExamPlace(");
			stringBuilder.Append("ExamPlaceName,Is_Default,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ExamPlaceName,@Is_Default,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamPlaceName", SqlDbType.VarChar, 50),
				new SqlParameter("@Is_Default", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10)
			};
			array[0].Value = model.ExamPlaceName;
			array[1].Value = model.Default;
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

		public bool Update(PEIS.Model.DictExamPlace model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DictExamPlace set ");
			stringBuilder.Append("ExamPlaceName=@ExamPlaceName,");
			stringBuilder.Append("Default=@Default,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where ExamPlaceID=@ExamPlaceID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamPlaceName", SqlDbType.VarChar, 50),
				new SqlParameter("@Default", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@ExamPlaceID", SqlDbType.Int, 4)
			};
			array[0].Value = model.ExamPlaceName;
			array[1].Value = model.Default;
			array[2].Value = model.InputCode;
			array[3].Value = model.ExamPlaceID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ExamPlaceID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictExamPlace ");
			stringBuilder.Append(" where ExamPlaceID=@ExamPlaceID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamPlaceID", SqlDbType.Int, 4)
			};
			array[0].Value = ExamPlaceID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ExamPlaceIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DictExamPlace ");
			stringBuilder.Append(" where ExamPlaceID in (" + ExamPlaceIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DictExamPlace GetModel(int ExamPlaceID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ExamPlaceID,ExamPlaceName,Is_Default,InputCode from DictExamPlace ");
			stringBuilder.Append(" where ExamPlaceID=@ExamPlaceID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamPlaceID", SqlDbType.Int, 4)
			};
			array[0].Value = ExamPlaceID;
			PEIS.Model.DictExamPlace busExamPlace = new PEIS.Model.DictExamPlace();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DictExamPlace result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ExamPlaceID"].ToString() != "")
				{
					busExamPlace.ExamPlaceID = int.Parse(dataSet.Tables[0].Rows[0]["ExamPlaceID"].ToString());
				}
				busExamPlace.ExamPlaceName = dataSet.Tables[0].Rows[0]["ExamPlaceName"].ToString();
				if (dataSet.Tables[0].Rows[0]["Default"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Default"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Default"].ToString().ToLower() == "true")
					{
						busExamPlace.Default =true;
					}
					else
					{
                        busExamPlace.Default = false;
					}
				}
				busExamPlace.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = busExamPlace;
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
			stringBuilder.Append("select ExamPlaceID,ExamPlaceName,Is_Default,InputCode ");
			stringBuilder.Append(" FROM DictExamPlace ");
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
			stringBuilder.Append(" ExamPlaceID,ExamPlaceName,Is_Default,InputCode ");
			stringBuilder.Append(" FROM DictExamPlace ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
