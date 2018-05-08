using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusFee : IBusFee
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_Fee", "BusFee");
		}

		public bool Exists(int ID_Fee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusFee");
			stringBuilder.Append(" where ID_Fee=@ID_Fee ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Fee;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusFee model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusFee(");
			stringBuilder.Append("ID_Section,ID_Specimen,FeeName,ForGender,ReportFeeName,FeeCode,Price,InputCode,SectionName,SpecimenName,WorkGroupCode,WorkStationCode,WorkBenchCode,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,Note,BreakfastOrder,Is_FeeNonPrintInReport,InterfaceName,IS_FeeReportMerger,ID_FeeReportMerger)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Section,@ID_Specimen,@FeeName,@ForGender,@ReportFeeName,@FeeCode,@Price,@InputCode,@SectionName,@SpecimenName,@WorkGroupCode,@WorkStationCode,@WorkBenchCode,@CreateDate,@Is_Banned,@ID_BanOpr,@BanDescribe,@DispOrder,@Note,@BreakfastOrder,@Is_FeeNonPrintInReport,@InterfaceName,@IS_FeeReportMerger,@ID_FeeReportMerger)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@ID_Specimen", SqlDbType.Int, 4),
				new SqlParameter("@FeeName", SqlDbType.VarChar, 50),
				new SqlParameter("@ForGender", SqlDbType.Int, 4),
				new SqlParameter("@ReportFeeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FeeCode", SqlDbType.VarChar, 50),
				new SqlParameter("@Price", SqlDbType.Money, 8),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@SectionName", SqlDbType.VarChar, 20),
				new SqlParameter("@SpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@WorkGroupCode", SqlDbType.VarChar, 20),
				new SqlParameter("@WorkStationCode", SqlDbType.VarChar, 20),
				new SqlParameter("@WorkBenchCode", SqlDbType.VarChar, 20),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 50),
				new SqlParameter("@BreakfastOrder", SqlDbType.Int, 4),
				new SqlParameter("@Is_FeeNonPrintInReport", SqlDbType.Bit, 1),
				new SqlParameter("@InterfaceName", SqlDbType.VarChar, 50),
				new SqlParameter("@IS_FeeReportMerger", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeReportMerger", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Section;
			array[1].Value = model.ID_Specimen;
			array[2].Value = model.FeeName;
			array[3].Value = model.ForGender;
			array[4].Value = model.ReportFeeName;
			array[5].Value = model.FeeCode;
			array[6].Value = model.Price;
			array[7].Value = model.InputCode;
			array[8].Value = model.SectionName;
			array[9].Value = model.SpecimenName;
			array[10].Value = model.WorkGroupCode;
			array[11].Value = model.WorkStationCode;
			array[12].Value = model.WorkBenchCode;
			array[13].Value = model.CreateDate;
			array[14].Value = model.Is_Banned;
			array[15].Value = model.ID_BanOpr;
			array[16].Value = model.BanDescribe;
			array[17].Value = model.DispOrder;
			array[18].Value = model.Note;
			array[19].Value = model.BreakfastOrder;
			array[20].Value = model.Is_FeeNonPrintInReport;
			array[21].Value = model.InterfaceName;
			array[22].Value = model.IS_FeeReportMerger;
			array[23].Value = model.ID_FeeReportMerger;
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

		public bool Update(PEIS.Model.BusFee model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusFee set ");
			stringBuilder.Append("ID_Section=@ID_Section,");
			stringBuilder.Append("ID_Specimen=@ID_Specimen,");
			stringBuilder.Append("FeeName=@FeeName,");
			stringBuilder.Append("ForGender=@ForGender,");
			stringBuilder.Append("ReportFeeName=@ReportFeeName,");
			stringBuilder.Append("FeeCode=@FeeCode,");
			stringBuilder.Append("Price=@Price,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("SectionName=@SectionName,");
			stringBuilder.Append("SpecimenName=@SpecimenName,");
			stringBuilder.Append("WorkGroupCode=@WorkGroupCode,");
			stringBuilder.Append("WorkStationCode=@WorkStationCode,");
			stringBuilder.Append("WorkBenchCode=@WorkBenchCode,");
			stringBuilder.Append("CreateDate=@CreateDate,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_BanOpr=@ID_BanOpr,");
			stringBuilder.Append("BanDescribe=@BanDescribe,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("BreakfastOrder=@BreakfastOrder,");
			stringBuilder.Append("Is_FeeNonPrintInReport=@Is_FeeNonPrintInReport,");
			stringBuilder.Append("InterfaceName=@InterfaceName,");
			stringBuilder.Append("IS_FeeReportMerger=@IS_FeeReportMerger,");
			stringBuilder.Append("ID_FeeReportMerger=@ID_FeeReportMerger");
			stringBuilder.Append(" where ID_Fee=@ID_Fee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@ID_Specimen", SqlDbType.Int, 4),
				new SqlParameter("@FeeName", SqlDbType.VarChar, 50),
				new SqlParameter("@ForGender", SqlDbType.Int, 4),
				new SqlParameter("@ReportFeeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FeeCode", SqlDbType.VarChar, 50),
				new SqlParameter("@Price", SqlDbType.Money, 8),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@SectionName", SqlDbType.VarChar, 20),
				new SqlParameter("@SpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@WorkGroupCode", SqlDbType.VarChar, 20),
				new SqlParameter("@WorkStationCode", SqlDbType.VarChar, 20),
				new SqlParameter("@WorkBenchCode", SqlDbType.VarChar, 20),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 50),
				new SqlParameter("@BreakfastOrder", SqlDbType.Int, 4),
				new SqlParameter("@Is_FeeNonPrintInReport", SqlDbType.Bit, 1),
				new SqlParameter("@InterfaceName", SqlDbType.VarChar, 50),
				new SqlParameter("@IS_FeeReportMerger", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeReportMerger", SqlDbType.Int, 4),
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Section;
			array[1].Value = model.ID_Specimen;
			array[2].Value = model.FeeName;
			array[3].Value = model.ForGender;
			array[4].Value = model.ReportFeeName;
			array[5].Value = model.FeeCode;
			array[6].Value = model.Price;
			array[7].Value = model.InputCode;
			array[8].Value = model.SectionName;
			array[9].Value = model.SpecimenName;
			array[10].Value = model.WorkGroupCode;
			array[11].Value = model.WorkStationCode;
			array[12].Value = model.WorkBenchCode;
			array[13].Value = model.CreateDate;
			array[14].Value = model.Is_Banned;
			array[15].Value = model.ID_BanOpr;
			array[16].Value = model.BanDescribe;
			array[17].Value = model.DispOrder;
			array[18].Value = model.Note;
			array[19].Value = model.BreakfastOrder;
			array[20].Value = model.Is_FeeNonPrintInReport;
			array[21].Value = model.InterfaceName;
			array[22].Value = model.IS_FeeReportMerger;
			array[23].Value = model.ID_FeeReportMerger;
			array[24].Value = model.ID_Fee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_Fee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusFee ");
			stringBuilder.Append(" where ID_Fee=@ID_Fee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Fee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_Feelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusFee ");
			stringBuilder.Append(" where ID_Fee in (" + ID_Feelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusFee GetModel(int ID_Fee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_Fee,ID_Section,ID_Specimen,FeeName,ForGender,ReportFeeName,FeeCode,Price,InputCode,SectionName,SpecimenName,WorkGroupCode,WorkStationCode,WorkBenchCode,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,Note,BreakfastOrder,Is_FeeNonPrintInReport,InterfaceName,IS_FeeReportMerger,ID_FeeReportMerger,OperationalDate from BusFee ");
			stringBuilder.Append(" where ID_Fee=@ID_Fee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Fee;
			PEIS.Model.BusFee busFee = new PEIS.Model.BusFee();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusFee result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_Fee"].ToString() != "")
				{
					busFee.ID_Fee = int.Parse(dataSet.Tables[0].Rows[0]["ID_Fee"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Section"].ToString() != "")
				{
					busFee.ID_Section = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Section"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Specimen"].ToString() != "")
				{
					busFee.ID_Specimen = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Specimen"].ToString()));
				}
				busFee.FeeName = dataSet.Tables[0].Rows[0]["FeeName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ForGender"].ToString() != "")
				{
					busFee.ForGender = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ForGender"].ToString()));
				}
				busFee.ReportFeeName = dataSet.Tables[0].Rows[0]["ReportFeeName"].ToString();
				busFee.FeeCode = dataSet.Tables[0].Rows[0]["FeeCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["Price"].ToString() != "")
				{
					busFee.Price = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["Price"].ToString()));
				}
				busFee.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				busFee.SectionName = dataSet.Tables[0].Rows[0]["SectionName"].ToString();
				busFee.SpecimenName = dataSet.Tables[0].Rows[0]["SpecimenName"].ToString();
				busFee.WorkGroupCode = dataSet.Tables[0].Rows[0]["WorkGroupCode"].ToString();
				busFee.WorkStationCode = dataSet.Tables[0].Rows[0]["WorkStationCode"].ToString();
				busFee.WorkBenchCode = dataSet.Tables[0].Rows[0]["WorkBenchCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["CreateDate"].ToString() != "")
				{
					busFee.CreateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busFee.Is_Banned = new bool?(true);
					}
					else
					{
						busFee.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString() != "")
				{
					busFee.ID_BanOpr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString()));
				}
				busFee.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busFee.DispOrder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString()));
				}
				busFee.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				if (dataSet.Tables[0].Rows[0]["BreakfastOrder"].ToString() != "")
				{
					busFee.BreakfastOrder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["BreakfastOrder"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_FeeNonPrintInReport"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_FeeNonPrintInReport"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_FeeNonPrintInReport"].ToString().ToLower() == "true")
					{
						busFee.Is_FeeNonPrintInReport = new bool?(true);
					}
					else
					{
						busFee.Is_FeeNonPrintInReport = new bool?(false);
					}
				}
				busFee.InterfaceName = dataSet.Tables[0].Rows[0]["InterfaceName"].ToString();
				if (dataSet.Tables[0].Rows[0]["IS_FeeReportMerger"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["IS_FeeReportMerger"].ToString() == "1" || dataSet.Tables[0].Rows[0]["IS_FeeReportMerger"].ToString().ToLower() == "true")
					{
						busFee.IS_FeeReportMerger = new bool?(true);
					}
					else
					{
						busFee.IS_FeeReportMerger = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_FeeReportMerger"].ToString() != "")
				{
					busFee.ID_FeeReportMerger = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FeeReportMerger"].ToString()));
				}
				busFee.OperationalDate = dataSet.Tables[0].Rows[0]["OperationalDate"].ToString();
				result = busFee;
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
			stringBuilder.Append("select ID_Fee,ID_Section,ID_Specimen,FeeName,ForGender,ReportFeeName,FeeCode,Price,InputCode,SectionName,SpecimenName,WorkGroupCode,WorkStationCode,WorkBenchCode,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,Note,BreakfastOrder,Is_FeeNonPrintInReport,InterfaceName,IS_FeeReportMerger,ID_FeeReportMerger ");
			stringBuilder.Append(" FROM BusFee ");
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
			stringBuilder.Append(" ID_Fee,ID_Section,ID_Specimen,FeeName,ForGender,ReportFeeName,FeeCode,Price,InputCode,SectionName,SpecimenName,WorkGroupCode,WorkStationCode,WorkBenchCode,CreateDate,Is_Banned,ID_BanOpr,BanDescribe,DispOrder,Note,BreakfastOrder,Is_FeeNonPrintInReport,InterfaceName,IS_FeeReportMerger,ID_FeeReportMerger ");
			stringBuilder.Append(" FROM BusFee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
