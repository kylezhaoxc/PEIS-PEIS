using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustApply : IOnCustApply
	{
		public bool Exists(string ID_Apply)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustApply");
			stringBuilder.Append(" where ID_Apply=@ID_Apply ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Apply", SqlDbType.VarChar, 50)
			};
			array[0].Value = ID_Apply;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public void Add(PEIS.Model.OnCustApply model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustApply(");
			stringBuilder.Append("ID_Apply,ID_Section,ID_Customer,ApplyTitle,SpecimenName,BatchNumber,SectionName,DeptName,ExamNumber,AcquisitionTime,RecvTime,ReportTime,ApplyDoctorName,DetectionDoctorName,CheckDoctorName,CreateTime,ExamItems,SendProjectIDs,SendGroupParams,SpecimenNo,Is_TypistFinish,ID_Typist,TypistName,TypistDate,ID_DetectionDoctor)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Apply,@ID_Section,@ID_Customer,@ApplyTitle,@SpecimenName,@BatchNumber,@SectionName,@DeptName,@ExamNumber,@AcquisitionTime,@RecvTime,@ReportTime,@ApplyDoctorName,@DetectionDoctorName,@CheckDoctorName,@CreateTime,@ExamItems,@SendProjectIDs,@SendGroupParams,@SpecimenNo,@Is_TypistFinish,@ID_Typist,@TypistName,@TypistDate,@ID_DetectionDoctor)");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Apply", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@ApplyTitle", SqlDbType.VarChar, 100),
				new SqlParameter("@SpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@BatchNumber", SqlDbType.VarChar, 20),
				new SqlParameter("@SectionName", SqlDbType.VarChar, 50),
				new SqlParameter("@DeptName", SqlDbType.VarChar, 50),
				new SqlParameter("@ExamNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@AcquisitionTime", SqlDbType.DateTime),
				new SqlParameter("@RecvTime", SqlDbType.DateTime),
				new SqlParameter("@ReportTime", SqlDbType.DateTime),
				new SqlParameter("@ApplyDoctorName", SqlDbType.VarChar, 20),
				new SqlParameter("@DetectionDoctorName", SqlDbType.VarChar, 20),
				new SqlParameter("@CheckDoctorName", SqlDbType.VarChar, 20),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@ExamItems", SqlDbType.VarChar, 200),
				new SqlParameter("@SendProjectIDs", SqlDbType.VarChar, 100),
				new SqlParameter("@SendGroupParams", SqlDbType.VarChar, 100),
				new SqlParameter("@SpecimenNo", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_TypistFinish", SqlDbType.Bit, 1),
				new SqlParameter("@ID_Typist", SqlDbType.Int, 4),
				new SqlParameter("@TypistName", SqlDbType.VarChar, 30),
				new SqlParameter("@TypistDate", SqlDbType.DateTime),
				new SqlParameter("@ID_DetectionDoctor", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Apply;
			array[1].Value = model.ID_Section;
			array[2].Value = model.ID_Customer;
			array[3].Value = model.ApplyTitle;
			array[4].Value = model.SpecimenName;
			array[5].Value = model.BatchNumber;
			array[6].Value = model.SectionName;
			array[7].Value = model.DeptName;
			array[8].Value = model.ExamNumber;
			array[9].Value = model.AcquisitionTime;
			array[10].Value = model.RecvTime;
			array[11].Value = model.ReportTime;
			array[12].Value = model.ApplyDoctorName;
			array[13].Value = model.DetectionDoctorName;
			array[14].Value = model.CheckDoctorName;
			array[15].Value = model.CreateTime;
			array[16].Value = model.ExamItems;
			array[17].Value = model.SendProjectIDs;
			array[18].Value = model.SendGroupParams;
			array[19].Value = model.SpecimenNo;
			array[20].Value = model.Is_TypistFinish;
			array[21].Value = model.ID_Typist;
			array[22].Value = model.TypistName;
			array[23].Value = model.TypistDate;
			array[24].Value = model.ID_DetectionDoctor;
			DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
		}

		public bool Update(PEIS.Model.OnCustApply model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustApply set ");
			stringBuilder.Append("ID_Section=@ID_Section,");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("ApplyTitle=@ApplyTitle,");
			stringBuilder.Append("SpecimenName=@SpecimenName,");
			stringBuilder.Append("BatchNumber=@BatchNumber,");
			stringBuilder.Append("SectionName=@SectionName,");
			stringBuilder.Append("DeptName=@DeptName,");
			stringBuilder.Append("ExamNumber=@ExamNumber,");
			stringBuilder.Append("AcquisitionTime=@AcquisitionTime,");
			stringBuilder.Append("RecvTime=@RecvTime,");
			stringBuilder.Append("ReportTime=@ReportTime,");
			stringBuilder.Append("ApplyDoctorName=@ApplyDoctorName,");
			stringBuilder.Append("DetectionDoctorName=@DetectionDoctorName,");
			stringBuilder.Append("CheckDoctorName=@CheckDoctorName,");
			stringBuilder.Append("CreateTime=@CreateTime,");
			stringBuilder.Append("ExamItems=@ExamItems,");
			stringBuilder.Append("SendProjectIDs=@SendProjectIDs,");
			stringBuilder.Append("SendGroupParams=@SendGroupParams,");
			stringBuilder.Append("SpecimenNo=@SpecimenNo,");
			stringBuilder.Append("Is_TypistFinish=@Is_TypistFinish,");
			stringBuilder.Append("ID_Typist=@ID_Typist,");
			stringBuilder.Append("TypistName=@TypistName,");
			stringBuilder.Append("TypistDate=@TypistDate,");
			stringBuilder.Append("ID_DetectionDoctor=@ID_DetectionDoctor");
			stringBuilder.Append(" where ID_Apply=@ID_Apply ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@ApplyTitle", SqlDbType.VarChar, 100),
				new SqlParameter("@SpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@BatchNumber", SqlDbType.VarChar, 20),
				new SqlParameter("@SectionName", SqlDbType.VarChar, 50),
				new SqlParameter("@DeptName", SqlDbType.VarChar, 50),
				new SqlParameter("@ExamNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@AcquisitionTime", SqlDbType.DateTime),
				new SqlParameter("@RecvTime", SqlDbType.DateTime),
				new SqlParameter("@ReportTime", SqlDbType.DateTime),
				new SqlParameter("@ApplyDoctorName", SqlDbType.VarChar, 20),
				new SqlParameter("@DetectionDoctorName", SqlDbType.VarChar, 20),
				new SqlParameter("@CheckDoctorName", SqlDbType.VarChar, 20),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@ExamItems", SqlDbType.VarChar, 200),
				new SqlParameter("@SendProjectIDs", SqlDbType.VarChar, 100),
				new SqlParameter("@SendGroupParams", SqlDbType.VarChar, 100),
				new SqlParameter("@SpecimenNo", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_TypistFinish", SqlDbType.Bit, 1),
				new SqlParameter("@ID_Typist", SqlDbType.Int, 4),
				new SqlParameter("@TypistName", SqlDbType.VarChar, 30),
				new SqlParameter("@TypistDate", SqlDbType.DateTime),
				new SqlParameter("@ID_DetectionDoctor", SqlDbType.Int, 4),
				new SqlParameter("@ID_Apply", SqlDbType.VarChar, 50)
			};
			array[0].Value = model.ID_Section;
			array[1].Value = model.ID_Customer;
			array[2].Value = model.ApplyTitle;
			array[3].Value = model.SpecimenName;
			array[4].Value = model.BatchNumber;
			array[5].Value = model.SectionName;
			array[6].Value = model.DeptName;
			array[7].Value = model.ExamNumber;
			array[8].Value = model.AcquisitionTime;
			array[9].Value = model.RecvTime;
			array[10].Value = model.ReportTime;
			array[11].Value = model.ApplyDoctorName;
			array[12].Value = model.DetectionDoctorName;
			array[13].Value = model.CheckDoctorName;
			array[14].Value = model.CreateTime;
			array[15].Value = model.ExamItems;
			array[16].Value = model.SendProjectIDs;
			array[17].Value = model.SendGroupParams;
			array[18].Value = model.SpecimenNo;
			array[19].Value = model.Is_TypistFinish;
			array[20].Value = model.ID_Typist;
			array[21].Value = model.TypistName;
			array[22].Value = model.TypistDate;
			array[23].Value = model.ID_DetectionDoctor;
			array[24].Value = model.ID_Apply;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(string ID_Apply)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustApply ");
			stringBuilder.Append(" where ID_Apply=@ID_Apply ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Apply", SqlDbType.VarChar, 50)
			};
			array[0].Value = ID_Apply;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_Applylist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustApply ");
			stringBuilder.Append(" where ID_Apply in (" + ID_Applylist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustApply GetModel(string ID_Apply)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_Apply,ID_Section,ID_Customer,ApplyTitle,SpecimenName,BatchNumber,SectionName,DeptName,ExamNumber,AcquisitionTime,RecvTime,ReportTime,ApplyDoctorName,DetectionDoctorName,CheckDoctorName,CreateTime,ExamItems,SendProjectIDs,SendGroupParams,SpecimenNo,Is_TypistFinish,ID_Typist,TypistName,TypistDate,ID_DetectionDoctor from OnCustApply ");
			stringBuilder.Append(" where ID_Apply=@ID_Apply ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Apply", SqlDbType.VarChar, 50)
			};
			array[0].Value = ID_Apply;
			PEIS.Model.OnCustApply onCustApply = new PEIS.Model.OnCustApply();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustApply result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				onCustApply.ID_Apply = dataSet.Tables[0].Rows[0]["ID_Apply"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Section"].ToString() != "")
				{
					onCustApply.ID_Section = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Section"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onCustApply.ID_Customer = new long?(long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString()));
				}
				onCustApply.ApplyTitle = dataSet.Tables[0].Rows[0]["ApplyTitle"].ToString();
				onCustApply.SpecimenName = dataSet.Tables[0].Rows[0]["SpecimenName"].ToString();
				onCustApply.BatchNumber = dataSet.Tables[0].Rows[0]["BatchNumber"].ToString();
				onCustApply.SectionName = dataSet.Tables[0].Rows[0]["SectionName"].ToString();
				onCustApply.DeptName = dataSet.Tables[0].Rows[0]["DeptName"].ToString();
				onCustApply.ExamNumber = dataSet.Tables[0].Rows[0]["ExamNumber"].ToString();
				if (dataSet.Tables[0].Rows[0]["AcquisitionTime"].ToString() != "")
				{
					onCustApply.AcquisitionTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["AcquisitionTime"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["RecvTime"].ToString() != "")
				{
					onCustApply.RecvTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["RecvTime"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ReportTime"].ToString() != "")
				{
					onCustApply.ReportTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["ReportTime"].ToString()));
				}
				onCustApply.ApplyDoctorName = dataSet.Tables[0].Rows[0]["ApplyDoctorName"].ToString();
				onCustApply.DetectionDoctorName = dataSet.Tables[0].Rows[0]["DetectionDoctorName"].ToString();
				onCustApply.CheckDoctorName = dataSet.Tables[0].Rows[0]["CheckDoctorName"].ToString();
				if (dataSet.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					onCustApply.CreateTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateTime"].ToString()));
				}
				onCustApply.ExamItems = dataSet.Tables[0].Rows[0]["ExamItems"].ToString();
				onCustApply.SendProjectIDs = dataSet.Tables[0].Rows[0]["SendProjectIDs"].ToString();
				onCustApply.SendGroupParams = dataSet.Tables[0].Rows[0]["SendGroupParams"].ToString();
				onCustApply.SpecimenNo = dataSet.Tables[0].Rows[0]["SpecimenNo"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_TypistFinish"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_TypistFinish"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_TypistFinish"].ToString().ToLower() == "true")
					{
						onCustApply.Is_TypistFinish = new bool?(true);
					}
					else
					{
						onCustApply.Is_TypistFinish = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_Typist"].ToString() != "")
				{
					onCustApply.ID_Typist = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Typist"].ToString()));
				}
				onCustApply.TypistName = dataSet.Tables[0].Rows[0]["TypistName"].ToString();
				if (dataSet.Tables[0].Rows[0]["TypistDate"].ToString() != "")
				{
					onCustApply.TypistDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["TypistDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_DetectionDoctor"].ToString() != "")
				{
					onCustApply.ID_DetectionDoctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_DetectionDoctor"].ToString()));
				}
				result = onCustApply;
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
			stringBuilder.Append("select ID_Apply,ID_Section,ID_Customer,ApplyTitle,SpecimenName,BatchNumber,SectionName,DeptName,ExamNumber,AcquisitionTime,RecvTime,ReportTime,ApplyDoctorName,DetectionDoctorName,CheckDoctorName,CreateTime,ExamItems,SendProjectIDs,SendGroupParams,SpecimenNo,Is_TypistFinish,ID_Typist,TypistName,TypistDate,ID_DetectionDoctor ");
			stringBuilder.Append(" FROM OnCustApply ");
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
			stringBuilder.Append(" ID_Apply,ID_Section,ID_Customer,ApplyTitle,SpecimenName,BatchNumber,SectionName,DeptName,ExamNumber,AcquisitionTime,RecvTime,ReportTime,ApplyDoctorName,DetectionDoctorName,CheckDoctorName,CreateTime,ExamItems,SendProjectIDs,SendGroupParams,SpecimenNo,Is_TypistFinish,ID_Typist,TypistName,TypistDate,ID_DetectionDoctor ");
			stringBuilder.Append(" FROM OnCustApply ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
