using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustExamSection : IOnCustExamSection
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_CustExamSection", "OnCustExamSection");
		}

		public bool Exists(int ID_CustExamSection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustExamSection");
			stringBuilder.Append(" where ID_CustExamSection=@ID_CustExamSection ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamSection", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustExamSection;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustExamSection model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustExamSection(");
			stringBuilder.Append("ID_Customer,ID_Section,CustomerName,SectionName,DiseaseLevel,SectionSummaryDate,SectionSummary,PositiveSummary,ID_SummaryDoctor,SummaryDoctorName,ID_Typist,TypistName,TypistDate,Is_Check,CheckerName,CheckDate,ID_Checker,IS_giveup,IS_Refund,ImageUrl)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Customer,@ID_Section,@CustomerName,@SectionName,@DiseaseLevel,@SectionSummaryDate,@SectionSummary,@PositiveSummary,@ID_SummaryDoctor,@SummaryDoctorName,@ID_Typist,@TypistName,@TypistDate,@Is_Check,@CheckerName,@CheckDate,@ID_Checker,@IS_giveup,@IS_Refund,@ImageUrl)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@CustomerName", SqlDbType.VarChar, 30),
				new SqlParameter("@SectionName", SqlDbType.VarChar, 20),
				new SqlParameter("@DiseaseLevel", SqlDbType.Int, 4),
				new SqlParameter("@SectionSummaryDate", SqlDbType.DateTime),
				new SqlParameter("@SectionSummary", SqlDbType.Text),
				new SqlParameter("@PositiveSummary", SqlDbType.Text),
				new SqlParameter("@ID_SummaryDoctor", SqlDbType.Int, 4),
				new SqlParameter("@SummaryDoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_Typist", SqlDbType.Int, 4),
				new SqlParameter("@TypistName", SqlDbType.VarChar, 30),
				new SqlParameter("@TypistDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Check", SqlDbType.Bit, 1),
				new SqlParameter("@CheckerName", SqlDbType.VarChar, 30),
				new SqlParameter("@CheckDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Checker", SqlDbType.Int, 4),
				new SqlParameter("@IS_giveup", SqlDbType.Bit, 1),
				new SqlParameter("@IS_Refund", SqlDbType.Bit, 1),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 200)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ID_Section;
			array[2].Value = model.CustomerName;
			array[3].Value = model.SectionName;
			array[4].Value = model.DiseaseLevel;
			array[5].Value = model.SectionSummaryDate;
			array[6].Value = model.SectionSummary;
			array[7].Value = model.PositiveSummary;
			array[8].Value = model.ID_SummaryDoctor;
			array[9].Value = model.SummaryDoctorName;
			array[10].Value = model.ID_Typist;
			array[11].Value = model.TypistName;
			array[12].Value = model.TypistDate;
			array[13].Value = model.Is_Check;
			array[14].Value = model.CheckerName;
			array[15].Value = model.CheckDate;
			array[16].Value = model.ID_Checker;
			array[17].Value = model.IS_giveup;
			array[18].Value = model.IS_Refund;
			array[19].Value = model.ImageUrl;
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

		public bool Update(PEIS.Model.OnCustExamSection model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustExamSection set ");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("ID_Section=@ID_Section,");
			stringBuilder.Append("CustomerName=@CustomerName,");
			stringBuilder.Append("SectionName=@SectionName,");
			stringBuilder.Append("DiseaseLevel=@DiseaseLevel,");
			stringBuilder.Append("SectionSummaryDate=@SectionSummaryDate,");
			stringBuilder.Append("SectionSummary=@SectionSummary,");
			stringBuilder.Append("PositiveSummary=@PositiveSummary,");
			stringBuilder.Append("ID_SummaryDoctor=@ID_SummaryDoctor,");
			stringBuilder.Append("SummaryDoctorName=@SummaryDoctorName,");
			stringBuilder.Append("ID_Typist=@ID_Typist,");
			stringBuilder.Append("TypistName=@TypistName,");
			stringBuilder.Append("TypistDate=@TypistDate,");
			stringBuilder.Append("Is_Check=@Is_Check,");
			stringBuilder.Append("CheckerName=@CheckerName,");
			stringBuilder.Append("CheckDate=@CheckDate,");
			stringBuilder.Append("ID_Checker=@ID_Checker,");
			stringBuilder.Append("IS_giveup=@IS_giveup,");
			stringBuilder.Append("IS_Refund=@IS_Refund,");
			stringBuilder.Append("ImageUrl=@ImageUrl");
			stringBuilder.Append(" where ID_CustExamSection=@ID_CustExamSection");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@CustomerName", SqlDbType.VarChar, 30),
				new SqlParameter("@SectionName", SqlDbType.VarChar, 20),
				new SqlParameter("@DiseaseLevel", SqlDbType.Int, 4),
				new SqlParameter("@SectionSummaryDate", SqlDbType.DateTime),
				new SqlParameter("@SectionSummary", SqlDbType.Text),
				new SqlParameter("@PositiveSummary", SqlDbType.Text),
				new SqlParameter("@ID_SummaryDoctor", SqlDbType.Int, 4),
				new SqlParameter("@SummaryDoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_Typist", SqlDbType.Int, 4),
				new SqlParameter("@TypistName", SqlDbType.VarChar, 30),
				new SqlParameter("@TypistDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Check", SqlDbType.Bit, 1),
				new SqlParameter("@CheckerName", SqlDbType.VarChar, 30),
				new SqlParameter("@CheckDate", SqlDbType.DateTime),
				new SqlParameter("@ID_Checker", SqlDbType.Int, 4),
				new SqlParameter("@IS_giveup", SqlDbType.Bit, 1),
				new SqlParameter("@IS_Refund", SqlDbType.Bit, 1),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 200),
				new SqlParameter("@ID_CustExamSection", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ID_Section;
			array[2].Value = model.CustomerName;
			array[3].Value = model.SectionName;
			array[4].Value = model.DiseaseLevel;
			array[5].Value = model.SectionSummaryDate;
			array[6].Value = model.SectionSummary;
			array[7].Value = model.PositiveSummary;
			array[8].Value = model.ID_SummaryDoctor;
			array[9].Value = model.SummaryDoctorName;
			array[10].Value = model.ID_Typist;
			array[11].Value = model.TypistName;
			array[12].Value = model.TypistDate;
			array[13].Value = model.Is_Check;
			array[14].Value = model.CheckerName;
			array[15].Value = model.CheckDate;
			array[16].Value = model.ID_Checker;
			array[17].Value = model.IS_giveup;
			array[18].Value = model.IS_Refund;
			array[19].Value = model.ImageUrl;
			array[20].Value = model.ID_CustExamSection;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_CustExamSection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustExamSection ");
			stringBuilder.Append(" where ID_CustExamSection=@ID_CustExamSection");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamSection", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustExamSection;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_CustExamSectionlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustExamSection ");
			stringBuilder.Append(" where ID_CustExamSection in (" + ID_CustExamSectionlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustExamSection GetModel(int ID_CustExamSection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_CustExamSection,ID_Customer,ID_Section,CustomerName,SectionName,DiseaseLevel,SectionSummaryDate,SectionSummary,PositiveSummary,ID_SummaryDoctor,SummaryDoctorName,ID_Typist,TypistName,TypistDate,Is_Check,CheckerName,CheckDate,ID_Checker,IS_giveup,IS_Refund,ImageUrl from OnCustExamSection ");
			stringBuilder.Append(" where ID_CustExamSection=@ID_CustExamSection");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustExamSection", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustExamSection;
			PEIS.Model.OnCustExamSection onCustExamSection = new PEIS.Model.OnCustExamSection();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustExamSection result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_CustExamSection"].ToString() != "")
				{
					onCustExamSection.ID_CustExamSection = int.Parse(dataSet.Tables[0].Rows[0]["ID_CustExamSection"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onCustExamSection.ID_Customer = new long?(long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Section"].ToString() != "")
				{
					onCustExamSection.ID_Section = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Section"].ToString()));
				}
				onCustExamSection.CustomerName = dataSet.Tables[0].Rows[0]["CustomerName"].ToString();
				onCustExamSection.SectionName = dataSet.Tables[0].Rows[0]["SectionName"].ToString();
				if (dataSet.Tables[0].Rows[0]["DiseaseLevel"].ToString() != "")
				{
					onCustExamSection.DiseaseLevel = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DiseaseLevel"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["SectionSummaryDate"].ToString() != "")
				{
					onCustExamSection.SectionSummaryDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["SectionSummaryDate"].ToString()));
				}
				onCustExamSection.SectionSummary = dataSet.Tables[0].Rows[0]["SectionSummary"].ToString();
				onCustExamSection.PositiveSummary = dataSet.Tables[0].Rows[0]["PositiveSummary"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_SummaryDoctor"].ToString() != "")
				{
					onCustExamSection.ID_SummaryDoctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_SummaryDoctor"].ToString()));
				}
				onCustExamSection.SummaryDoctorName = dataSet.Tables[0].Rows[0]["SummaryDoctorName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Typist"].ToString() != "")
				{
					onCustExamSection.ID_Typist = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Typist"].ToString()));
				}
				onCustExamSection.TypistName = dataSet.Tables[0].Rows[0]["TypistName"].ToString();
				if (dataSet.Tables[0].Rows[0]["TypistDate"].ToString() != "")
				{
					onCustExamSection.TypistDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["TypistDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Check"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Check"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Check"].ToString().ToLower() == "true")
					{
						onCustExamSection.Is_Check = new bool?(true);
					}
					else
					{
						onCustExamSection.Is_Check = new bool?(false);
					}
				}
				onCustExamSection.CheckerName = dataSet.Tables[0].Rows[0]["CheckerName"].ToString();
				if (dataSet.Tables[0].Rows[0]["CheckDate"].ToString() != "")
				{
					onCustExamSection.CheckDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CheckDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Checker"].ToString() != "")
				{
					onCustExamSection.ID_Checker = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Checker"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["IS_giveup"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["IS_giveup"].ToString() == "1" || dataSet.Tables[0].Rows[0]["IS_giveup"].ToString().ToLower() == "true")
					{
						onCustExamSection.IS_giveup = new bool?(true);
					}
					else
					{
						onCustExamSection.IS_giveup = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["IS_Refund"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["IS_Refund"].ToString() == "1" || dataSet.Tables[0].Rows[0]["IS_Refund"].ToString().ToLower() == "true")
					{
						onCustExamSection.IS_Refund = new bool?(true);
					}
					else
					{
						onCustExamSection.IS_Refund = new bool?(false);
					}
				}
				onCustExamSection.ImageUrl = dataSet.Tables[0].Rows[0]["ImageUrl"].ToString();
				result = onCustExamSection;
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
			stringBuilder.Append("select ID_CustExamSection,ID_Customer,ID_Section,CustomerName,SectionName,DiseaseLevel,SectionSummaryDate,SectionSummary,PositiveSummary,ID_SummaryDoctor,SummaryDoctorName,ID_Typist,TypistName,TypistDate,Is_Check,CheckerName,CheckDate,ID_Checker,IS_giveup,IS_Refund,ImageUrl ");
			stringBuilder.Append(" FROM OnCustExamSection ");
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
			stringBuilder.Append(" ID_CustExamSection,ID_Customer,ID_Section,CustomerName,SectionName,DiseaseLevel,SectionSummaryDate,SectionSummary,PositiveSummary,ID_SummaryDoctor,SummaryDoctorName,ID_Typist,TypistName,TypistDate,Is_Check,CheckerName,CheckDate,ID_Checker,IS_giveup,IS_Refund,ImageUrl ");
			stringBuilder.Append(" FROM OnCustExamSection ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
