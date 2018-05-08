using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustExamItem : IOnCustExamItem
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_CustExamItem", "OnCustExamItem");
		}

		public bool Exists(int ID_CustExamItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustExamItem");
			stringBuilder.Append(" where ID_CustExamItem=@ID_CustExamItem ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustExamItem;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustExamItem model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustExamItem(");
			stringBuilder.Append("ID_CustFee,ID_Fee,ID_ExamItem,ExamItemName,DiseaseLevel,ResultLabValues,ResultNumber,ResultLabLowLimit,ResultLabHighLimit,ResultLabUnit,ResultLabMark,ResultSummary,ID_SummaryDoctor,SummaryDoctorName,TypistName,ExamItemSummaryDate,ID_Typist,ResultLabRange,ID_Customer,AbbrExamName,DetectionMethod,SCO,ID_CustApply)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_CustFee,@ID_Fee,@ID_ExamItem,@ExamItemName,@DiseaseLevel,@ResultLabValues,@ResultNumber,@ResultLabLowLimit,@ResultLabHighLimit,@ResultLabUnit,@ResultLabMark,@ResultSummary,@ID_SummaryDoctor,@SummaryDoctorName,@TypistName,@ExamItemSummaryDate,@ID_Typist,@ResultLabRange,@ID_Customer,@AbbrExamName,@DetectionMethod,@SCO,@ID_CustApply)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustFee", SqlDbType.Int, 4),
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ExamItemName", SqlDbType.VarChar, 50),
				new SqlParameter("@DiseaseLevel", SqlDbType.Int, 4),
				new SqlParameter("@ResultLabValues", SqlDbType.VarChar, 240),
				new SqlParameter("@ResultNumber", SqlDbType.Float, 8),
				new SqlParameter("@ResultLabLowLimit", SqlDbType.Float, 8),
				new SqlParameter("@ResultLabHighLimit", SqlDbType.Float, 8),
				new SqlParameter("@ResultLabUnit", SqlDbType.VarChar, 10),
				new SqlParameter("@ResultLabMark", SqlDbType.VarChar, 60),
				new SqlParameter("@ResultSummary", SqlDbType.Text),
				new SqlParameter("@ID_SummaryDoctor", SqlDbType.Int, 4),
				new SqlParameter("@SummaryDoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@TypistName", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamItemSummaryDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Typist", SqlDbType.Int, 4),
				new SqlParameter("@ResultLabRange", SqlDbType.VarChar, 100),
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@AbbrExamName", SqlDbType.VarChar, 60),
				new SqlParameter("@DetectionMethod", SqlDbType.VarChar, 60),
				new SqlParameter("@SCO", SqlDbType.VarChar, 20),
				new SqlParameter("@ID_CustApply", SqlDbType.VarChar, 60)
			};
			array[0].Value = model.ID_CustFee;
			array[1].Value = model.ID_Fee;
			array[2].Value = model.ID_ExamItem;
			array[3].Value = model.ExamItemName;
			array[4].Value = model.DiseaseLevel;
			array[5].Value = model.ResultLabValues;
			array[6].Value = model.ResultNumber;
			array[7].Value = model.ResultLabLowLimit;
			array[8].Value = model.ResultLabHighLimit;
			array[9].Value = model.ResultLabUnit;
			array[10].Value = model.ResultLabMark;
			array[11].Value = model.ResultSummary;
			array[12].Value = model.ID_SummaryDoctor;
			array[13].Value = model.SummaryDoctorName;
			array[14].Value = model.TypistName;
			array[15].Value = model.ExamItemSummaryDate;
			array[16].Value = model.ID_Typist;
			array[17].Value = model.ResultLabRange;
			array[18].Value = model.ID_Customer;
			array[19].Value = model.AbbrExamName;
			array[20].Value = model.DetectionMethod;
			array[21].Value = model.SCO;
			array[22].Value = model.ID_CustApply;
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

		public bool Update(PEIS.Model.OnCustExamItem model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustExamItem set ");
			stringBuilder.Append("ID_CustFee=@ID_CustFee,");
			stringBuilder.Append("ID_Fee=@ID_Fee,");
			stringBuilder.Append("ID_ExamItem=@ID_ExamItem,");
			stringBuilder.Append("ExamItemName=@ExamItemName,");
			stringBuilder.Append("DiseaseLevel=@DiseaseLevel,");
			stringBuilder.Append("ResultLabValues=@ResultLabValues,");
			stringBuilder.Append("ResultNumber=@ResultNumber,");
			stringBuilder.Append("ResultLabLowLimit=@ResultLabLowLimit,");
			stringBuilder.Append("ResultLabHighLimit=@ResultLabHighLimit,");
			stringBuilder.Append("ResultLabUnit=@ResultLabUnit,");
			stringBuilder.Append("ResultLabMark=@ResultLabMark,");
			stringBuilder.Append("ResultSummary=@ResultSummary,");
			stringBuilder.Append("ID_SummaryDoctor=@ID_SummaryDoctor,");
			stringBuilder.Append("SummaryDoctorName=@SummaryDoctorName,");
			stringBuilder.Append("TypistName=@TypistName,");
			stringBuilder.Append("ExamItemSummaryDate=@ExamItemSummaryDate,");
			stringBuilder.Append("ID_Typist=@ID_Typist,");
			stringBuilder.Append("ResultLabRange=@ResultLabRange,");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("AbbrExamName=@AbbrExamName,");
			stringBuilder.Append("DetectionMethod=@DetectionMethod,");
			stringBuilder.Append("SCO=@SCO,");
			stringBuilder.Append("ID_CustApply=@ID_CustApply");
			stringBuilder.Append(" where ID_CustExamItem=@ID_CustExamItem");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustFee", SqlDbType.Int, 4),
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ExamItemName", SqlDbType.VarChar, 50),
				new SqlParameter("@DiseaseLevel", SqlDbType.Int, 4),
				new SqlParameter("@ResultLabValues", SqlDbType.VarChar, 240),
				new SqlParameter("@ResultNumber", SqlDbType.Float, 8),
				new SqlParameter("@ResultLabLowLimit", SqlDbType.Float, 8),
				new SqlParameter("@ResultLabHighLimit", SqlDbType.Float, 8),
				new SqlParameter("@ResultLabUnit", SqlDbType.VarChar, 10),
				new SqlParameter("@ResultLabMark", SqlDbType.VarChar, 60),
				new SqlParameter("@ResultSummary", SqlDbType.Text),
				new SqlParameter("@ID_SummaryDoctor", SqlDbType.Int, 4),
				new SqlParameter("@SummaryDoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@TypistName", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamItemSummaryDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Typist", SqlDbType.Int, 4),
				new SqlParameter("@ResultLabRange", SqlDbType.VarChar, 100),
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@AbbrExamName", SqlDbType.VarChar, 60),
				new SqlParameter("@DetectionMethod", SqlDbType.VarChar, 60),
				new SqlParameter("@SCO", SqlDbType.VarChar, 20),
				new SqlParameter("@ID_CustApply", SqlDbType.VarChar, 60),
				new SqlParameter("@ID_CustExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_CustFee;
			array[1].Value = model.ID_Fee;
			array[2].Value = model.ID_ExamItem;
			array[3].Value = model.ExamItemName;
			array[4].Value = model.DiseaseLevel;
			array[5].Value = model.ResultLabValues;
			array[6].Value = model.ResultNumber;
			array[7].Value = model.ResultLabLowLimit;
			array[8].Value = model.ResultLabHighLimit;
			array[9].Value = model.ResultLabUnit;
			array[10].Value = model.ResultLabMark;
			array[11].Value = model.ResultSummary;
			array[12].Value = model.ID_SummaryDoctor;
			array[13].Value = model.SummaryDoctorName;
			array[14].Value = model.TypistName;
			array[15].Value = model.ExamItemSummaryDate;
			array[16].Value = model.ID_Typist;
			array[17].Value = model.ResultLabRange;
			array[18].Value = model.ID_Customer;
			array[19].Value = model.AbbrExamName;
			array[20].Value = model.DetectionMethod;
			array[21].Value = model.SCO;
			array[22].Value = model.ID_CustApply;
			array[23].Value = model.ID_CustExamItem;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_CustExamItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustExamItem ");
			stringBuilder.Append(" where ID_CustExamItem=@ID_CustExamItem");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustExamItem;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_CustExamItemlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustExamItem ");
			stringBuilder.Append(" where ID_CustExamItem in (" + ID_CustExamItemlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustExamItem GetModel(int ID_CustExamItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_CustExamItem,ID_CustFee,ID_Fee,ID_ExamItem,ExamItemName,DiseaseLevel,ResultLabValues,ResultNumber,ResultLabRange,ResultLabLowLimit,ResultLabHighLimit,ResultLabUnit,ResultLabMark,ResultSummary,ID_SummaryDoctor,SummaryDoctorName,TypistName,ExamItemSummaryDate,ID_Typist from OnCustExamItem ");
			stringBuilder.Append(" where ID_CustExamItem=@ID_CustExamItem");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustExamItem;
			PEIS.Model.OnCustExamItem onCustExamItem = new PEIS.Model.OnCustExamItem();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustExamItem result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_CustExamItem"].ToString() != "")
				{
					onCustExamItem.ID_CustExamItem = int.Parse(dataSet.Tables[0].Rows[0]["ID_CustExamItem"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_CustFee"].ToString() != "")
				{
					onCustExamItem.ID_CustFee = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_CustFee"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Fee"].ToString() != "")
				{
					onCustExamItem.ID_Fee = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Fee"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString() != "")
				{
					onCustExamItem.ID_ExamItem = int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString());
				}
				onCustExamItem.ExamItemName = dataSet.Tables[0].Rows[0]["ExamItemName"].ToString();
				if (dataSet.Tables[0].Rows[0]["DiseaseLevel"].ToString() != "")
				{
					onCustExamItem.DiseaseLevel = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DiseaseLevel"].ToString()));
				}
				onCustExamItem.ResultLabValues = dataSet.Tables[0].Rows[0]["ResultLabValues"].ToString();
				if (dataSet.Tables[0].Rows[0]["ResultNumber"].ToString() != "")
				{
					onCustExamItem.ResultNumber = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["ResultNumber"].ToString()));
				}
				onCustExamItem.ResultLabRange = dataSet.Tables[0].Rows[0]["ResultLabRange"].ToString();
				if (dataSet.Tables[0].Rows[0]["ResultLabLowLimit"].ToString() != "")
				{
					onCustExamItem.ResultLabLowLimit = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["ResultLabLowLimit"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ResultLabHighLimit"].ToString() != "")
				{
					onCustExamItem.ResultLabHighLimit = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["ResultLabHighLimit"].ToString()));
				}
				onCustExamItem.ResultLabUnit = dataSet.Tables[0].Rows[0]["ResultLabUnit"].ToString();
				onCustExamItem.ResultLabMark = dataSet.Tables[0].Rows[0]["ResultLabMark"].ToString();
				onCustExamItem.ResultSummary = dataSet.Tables[0].Rows[0]["ResultSummary"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_SummaryDoctor"].ToString() != "")
				{
					onCustExamItem.ID_SummaryDoctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_SummaryDoctor"].ToString()));
				}
				onCustExamItem.SummaryDoctorName = dataSet.Tables[0].Rows[0]["SummaryDoctorName"].ToString();
				onCustExamItem.TypistName = dataSet.Tables[0].Rows[0]["TypistName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ExamItemSummaryDate"].ToString() != "")
				{
					onCustExamItem.ExamItemSummaryDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["ExamItemSummaryDate"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Typist"].ToString() != "")
				{
					onCustExamItem.ID_Typist = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Typist"].ToString()));
				}
				result = onCustExamItem;
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
			stringBuilder.Append("select ID_CustExamItem,ID_CustFee,ID_Fee,ID_ExamItem,ExamItemName,DiseaseLevel,ResultLabValues,ResultNumber,ResultLabRange,ResultLabLowLimit,ResultLabHighLimit,ResultLabUnit,ResultLabMark,ResultSummary,ID_SummaryDoctor,SummaryDoctorName,TypistName,ExamItemSummaryDate,ID_Typist ");
			stringBuilder.Append(" FROM OnCustExamItem ");
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
			stringBuilder.Append(" ID_CustExamItem,ID_CustFee,ID_Fee,ID_ExamItem,ExamItemName,DiseaseLevel,ResultLabValues,ResultNumber,ResultLabRange,ResultLabLowLimit,ResultLabHighLimit,ResultLabUnit,ResultLabMark,ResultSummary,ID_SummaryDoctor,SummaryDoctorName,TypistName,ExamItemSummaryDate,ID_Typist ");
			stringBuilder.Append(" FROM OnCustExamItem ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
