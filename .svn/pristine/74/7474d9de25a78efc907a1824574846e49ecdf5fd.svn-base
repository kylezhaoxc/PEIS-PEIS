using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonCustExam
	{
		private static ICommonCustExam dal = DataAccess.CreateCommonCustExam();

		private static readonly CommonCustExam _instance = new CommonCustExam();

		public static CommonCustExam Instance
		{
			get
			{
				return CommonCustExam._instance;
			}
		}

		public PEIS.Model.OnCustPhysicalExamInfo GetOCPEIModel(long ID_Customer, string ConfigName)
		{
			return CommonCustExam.dal.GetOCPEIModel(ID_Customer, ConfigName);
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonCustExam.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonCustExam.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public int SaveCustomerSectionSummary(List<PEIS.Model.OnCustExamItem> OnCustExamItemModelList, List<PEIS.Model.OnCustFee> OnCustFeeModelList, PEIS.Model.OnCustExamSection OnCustExamSectionModel, PEIS.Model.OnCustApply OnCustApplyModel, int ID_FeeReportMerger, params SqlConditionInfo[] conditions)
		{
			return CommonCustExam.dal.SaveCustomerSectionSummary(OnCustExamItemModelList, OnCustFeeModelList, OnCustExamSectionModel, OnCustApplyModel, ID_FeeReportMerger, conditions);
		}

		public int UpdateSectionSummaryCheckState(PEIS.Model.SYSOpUser UserModel, int CheckState, List<int> IDList)
		{
			return CommonCustExam.dal.UpdateSectionSummaryCheckState(UserModel, CheckState, IDList);
		}

		public int UpdateSectionSummaryCheckState(PEIS.Model.OnCustExamSection OCESModel)
		{
			return CommonCustExam.dal.UpdateSectionSummaryCheckState(OCESModel);
		}

		public int UpdateSectionImageUrl(int ID_CustExamSection, string ImageUrl)
		{
			return CommonCustExam.dal.UpdateSectionImageUrl(ID_CustExamSection, ImageUrl);
		}

		public int BandingCustomerSectionExamInfo(PEIS.Model.OnCustExamSection OCESModel)
		{
			return CommonCustExam.dal.BandingCustomerSectionExamInfo(OCESModel);
		}

		public int UpdateLisCustFeeExamItem(long ID_Customer, int ID_Section, PEIS.Model.SYSOpUser LoginUserModel)
		{
			return CommonCustExam.dal.UpdateLisCustFeeExamItem(ID_Customer, ID_Section, LoginUserModel);
		}

		public int DeleteCustomerExamItem(long ID_Customer, int ID_Section, string InterFaceType)
		{
			return CommonCustExam.dal.DeleteCustomerExamItem(ID_Customer, ID_Section, InterFaceType);
		}

		public int DeleteCustomerExamItem_LAB(long ID_Customer, int ID_Section, string ApplyID, int ID_MergerFee, string InterFaceType)
		{
			return CommonCustExam.dal.DeleteCustomerExamItem_LAB(ID_Customer, ID_Section, ApplyID, ID_MergerFee, InterFaceType);
		}

		public DataSet GetCustRelationCustPEInfo(long ID_Customer, string IDCardNo, string ExamCardNo)
		{
			string cacheKey = "CommonCustExam-CustRelationCustPEInfo-" + ID_Customer.ToString() + IDCardNo.ToString() + ExamCardNo.ToString();
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = CommonCustExam.dal.GetCustRelationCustPEInfo(ID_Customer, IDCardNo, ExamCardNo);
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			return (DataSet)obj;
		}

		public DataSet GetArcCustomerInfo(string IDCard, string ExamCard)
		{
			string cacheKey = "CommonCustExam-ArcCustomerInfo-" + IDCard.ToString() + ExamCard.ToString();
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = CommonCustExam.dal.GetArcCustomerInfo(IDCard, ExamCard);
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			return (DataSet)obj;
		}

		public DataSet GetOnArcCustomerAndPEInfo(long ID_Customer, string IDCard, string ExamCard)
		{
			string cacheKey = "CommonCustExam-ArcCustomerInfo-" + ID_Customer.ToString() + IDCard.ToString() + ExamCard.ToString();
			object obj = null;
			if (obj == null)
			{
				try
				{
					obj = CommonCustExam.dal.GetOnArcCustomerAndPEInfo(ID_Customer, IDCard, ExamCard);
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			return (DataSet)obj;
		}

		public int UpdateGuideSheetReturnState(PEIS.Model.OnCustPhysicalExamInfo OCPEIModel)
		{
			return CommonCustExam.dal.UpdateGuideSheetReturnState(OCPEIModel);
		}

		public int UpdateDiseaselLevelInform(PEIS.Model.OnCustPhysicalExamInfo OCPEIModel)
		{
			return CommonCustExam.dal.UpdateDiseaselLevelInform(OCPEIModel);
		}

		public int UpdateExamSectionGiveUpState(PEIS.Model.OnCustExamSection OCESmodel)
		{
			return CommonCustExam.dal.UpdateExamSectionGiveUpState(OCESmodel);
		}

		public int UpdateAllNotExamedSectionGiveUp(PEIS.Model.OnCustExamSection OCESmodel)
		{
			return CommonCustExam.dal.UpdateAllNotExamedSectionGiveUp(OCESmodel);
		}
	}
}
