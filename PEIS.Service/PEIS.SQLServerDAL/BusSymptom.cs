using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusSymptom : IBusSymptom
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_Symptom", "BusSymptom");
		}

		public bool Exists(int ID_Symptom)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusSymptom");
			stringBuilder.Append(" where ID_Symptom=@ID_Symptom ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Symptom", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Symptom;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusSymptom model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusSymptom(");
			stringBuilder.Append("ID_ExamItem,ID_Conclusion,SymptomName,SymptomDescribe,DiseaseLevel,Is_Default,NumOperSign,NumMale,NumFemale,InputCode,DispOrder,Is_Banned,ID_BanOpr,BanOperator,BanDate)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_ExamItem,@ID_Conclusion,@SymptomName,@SymptomDescribe,@DiseaseLevel,@Is_Default,@NumOperSign,@NumMale,@NumFemale,@InputCode,@DispOrder,@Is_Banned,@ID_BanOpr,@BanOperator,@BanDate)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4),
				new SqlParameter("@SymptomName", SqlDbType.VarChar, 100),
				new SqlParameter("@SymptomDescribe", SqlDbType.Text),
				new SqlParameter("@DiseaseLevel", SqlDbType.Int, 4),
				new SqlParameter("@Is_Default", SqlDbType.Bit, 1),
				new SqlParameter("@NumOperSign", SqlDbType.VarChar, 2),
				new SqlParameter("@NumMale", SqlDbType.Float, 8),
				new SqlParameter("@NumFemale", SqlDbType.Float, 8),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime)
			};
			array[0].Value = model.ID_ExamItem;
			array[1].Value = model.ID_Conclusion;
			array[2].Value = model.SymptomName;
			array[3].Value = model.SymptomDescribe;
			array[4].Value = model.DiseaseLevel;
			array[5].Value = model.Is_Default;
			array[6].Value = model.NumOperSign;
			array[7].Value = model.NumMale;
			array[8].Value = model.NumFemale;
			array[9].Value = model.InputCode;
			array[10].Value = model.DispOrder;
			array[11].Value = model.Is_Banned;
			array[12].Value = model.ID_BanOpr;
			array[13].Value = model.BanOperator;
			array[14].Value = model.BanDate;
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

		public bool Update(PEIS.Model.BusSymptom model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusSymptom set ");
			stringBuilder.Append("ID_ExamItem=@ID_ExamItem,");
			stringBuilder.Append("ID_Conclusion=@ID_Conclusion,");
			stringBuilder.Append("SymptomName=@SymptomName,");
			stringBuilder.Append("SymptomDescribe=@SymptomDescribe,");
			stringBuilder.Append("DiseaseLevel=@DiseaseLevel,");
			stringBuilder.Append("Is_Default=@Is_Default,");
			stringBuilder.Append("NumOperSign=@NumOperSign,");
			stringBuilder.Append("NumMale=@NumMale,");
			stringBuilder.Append("NumFemale=@NumFemale,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_BanOpr=@ID_BanOpr,");
			stringBuilder.Append("BanOperator=@BanOperator,");
			stringBuilder.Append("BanDate=@BanDate");
			stringBuilder.Append(" where ID_Symptom=@ID_Symptom");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ID_Conclusion", SqlDbType.Int, 4),
				new SqlParameter("@SymptomName", SqlDbType.VarChar, 100),
				new SqlParameter("@SymptomDescribe", SqlDbType.Text),
				new SqlParameter("@DiseaseLevel", SqlDbType.Int, 4),
				new SqlParameter("@Is_Default", SqlDbType.Bit, 1),
				new SqlParameter("@NumOperSign", SqlDbType.VarChar, 2),
				new SqlParameter("@NumMale", SqlDbType.Float, 8),
				new SqlParameter("@NumFemale", SqlDbType.Float, 8),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Symptom", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_ExamItem;
			array[1].Value = model.ID_Conclusion;
			array[2].Value = model.SymptomName;
			array[3].Value = model.SymptomDescribe;
			array[4].Value = model.DiseaseLevel;
			array[5].Value = model.Is_Default;
			array[6].Value = model.NumOperSign;
			array[7].Value = model.NumMale;
			array[8].Value = model.NumFemale;
			array[9].Value = model.InputCode;
			array[10].Value = model.DispOrder;
			array[11].Value = model.Is_Banned;
			array[12].Value = model.ID_BanOpr;
			array[13].Value = model.BanOperator;
			array[14].Value = model.BanDate;
			array[15].Value = model.ID_Symptom;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_Symptom)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusSymptom ");
			stringBuilder.Append(" where ID_Symptom=@ID_Symptom");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Symptom", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Symptom;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_Symptomlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusSymptom ");
			stringBuilder.Append(" where ID_Symptom in (" + ID_Symptomlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusSymptom GetModel(int ID_Symptom)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_Symptom,ID_ExamItem,ID_Conclusion,SymptomName,SymptomDescribe,DiseaseLevel,Is_Default,NumOperSign,NumMale,NumFemale,InputCode,DispOrder,Is_Banned,ID_BanOpr,BanOperator,BanDate from BusSymptom ");
			stringBuilder.Append(" where ID_Symptom=@ID_Symptom");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Symptom", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Symptom;
			PEIS.Model.BusSymptom busSymptom = new PEIS.Model.BusSymptom();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusSymptom result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_Symptom"].ToString() != "")
				{
					busSymptom.ID_Symptom = int.Parse(dataSet.Tables[0].Rows[0]["ID_Symptom"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString() != "")
				{
					busSymptom.ID_ExamItem = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Conclusion"].ToString() != "")
				{
					busSymptom.ID_Conclusion = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Conclusion"].ToString()));
				}
				busSymptom.SymptomName = dataSet.Tables[0].Rows[0]["SymptomName"].ToString();
				busSymptom.SymptomDescribe = dataSet.Tables[0].Rows[0]["SymptomDescribe"].ToString();
				if (dataSet.Tables[0].Rows[0]["DiseaseLevel"].ToString() != "")
				{
					busSymptom.DiseaseLevel = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DiseaseLevel"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Default"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Default"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Default"].ToString().ToLower() == "true")
					{
						busSymptom.Is_Default = new bool?(true);
					}
					else
					{
						busSymptom.Is_Default = new bool?(false);
					}
				}
				busSymptom.NumOperSign = dataSet.Tables[0].Rows[0]["NumOperSign"].ToString();
				if (dataSet.Tables[0].Rows[0]["NumMale"].ToString() != "")
				{
					busSymptom.NumMale = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["NumMale"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["NumFemale"].ToString() != "")
				{
					busSymptom.NumFemale = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["NumFemale"].ToString()));
				}
				busSymptom.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busSymptom.DispOrder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busSymptom.Is_Banned = new bool?(true);
					}
					else
					{
						busSymptom.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString() != "")
				{
					busSymptom.ID_BanOpr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString()));
				}
				busSymptom.BanOperator = dataSet.Tables[0].Rows[0]["BanOperator"].ToString();
				if (dataSet.Tables[0].Rows[0]["BanDate"].ToString() != "")
				{
					busSymptom.BanDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["BanDate"].ToString()));
				}
				result = busSymptom;
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
			stringBuilder.Append("select ID_Symptom,ID_ExamItem,ID_Conclusion,SymptomName,SymptomDescribe,DiseaseLevel,Is_Default,NumOperSign,NumMale,NumFemale,InputCode,DispOrder,Is_Banned,ID_BanOpr,BanOperator,BanDate ");
			stringBuilder.Append(" FROM BusSymptom ");
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
			stringBuilder.Append(" ID_Symptom,ID_ExamItem,ID_Conclusion,SymptomName,SymptomDescribe,DiseaseLevel,Is_Default,NumOperSign,NumMale,NumFemale,InputCode,DispOrder,Is_Banned,ID_BanOpr,BanOperator,BanDate ");
			stringBuilder.Append(" FROM BusSymptom ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
