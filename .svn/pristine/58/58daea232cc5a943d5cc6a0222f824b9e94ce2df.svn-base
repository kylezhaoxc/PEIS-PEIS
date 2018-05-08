using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonCustExam
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions);

		DataSet GetCustRelationCustPEInfo(long ID_Customer, string IDCardNo, string ExamCardNo);

		DataSet GetArcCustomerInfo(string IDCard, string ExamCard);

		DataSet GetOnArcCustomerAndPEInfo(long ID_Customer, string IDCard, string ExamCard);

		int SaveCustomerSectionSummary(List<OnCustExamItem> OnCustExamItemModelList, List<OnCustFee> OnCustFeeModelList, OnCustExamSection OnCustExamSectionModel, OnCustApply OnCustApplyModel, int ID_FeeReportMerger, params SqlConditionInfo[] conditions);

		int UpdateSectionSummaryCheckState(SYSOpUser UserModel, int CheckState, List<int> IDList);

		int UpdateSectionSummaryCheckState(OnCustExamSection OCESModel);

		int UpdateSectionImageUrl(int ID_CustExamSection, string ImageUrl);

		int BandingCustomerSectionExamInfo(OnCustExamSection OCESModel);

		int UpdateLisCustFeeExamItem(long ID_Customer, int ID_Section, SYSOpUser LoginUserModel);

		int DeleteCustomerExamItem(long ID_Customer, int ID_Section, string InterFaceType);

		int DeleteCustomerExamItem_LAB(long ID_Customer, int ID_Section, string ApplyID, int ID_MergerFee, string InterFaceType);

		OnCustPhysicalExamInfo GetOCPEIModel(long ID_Customer, string ConfigName);

		int UpdateGuideSheetReturnState(OnCustPhysicalExamInfo OCPEIModel);

		int UpdateDiseaselLevelInform(OnCustPhysicalExamInfo OCPEIModel);

		int UpdateExamSectionGiveUpState(OnCustExamSection OCESmodel);

		int UpdateAllNotExamedSectionGiveUp(OnCustExamSection OCESmodel);
	}
}
