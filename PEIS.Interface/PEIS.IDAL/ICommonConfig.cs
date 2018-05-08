using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonConfig
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions);

		int SaveSection(SYSSection SectionModel);

		int SaveRole(SysRole RoleModel);

		int SaveExamPlace(DictExamPlace ExamPlaceModel);

		int SaveSpecimen(BusSpecimen SpecimenModel);

		int SaveDictExamType(DictExamType ExamTypeModel);

		int SaveFeeItem(BusFee busFeeModel);

		int SaveFeeExamRel(int ID_Fee, string newExamItemIDStrs);

		int SaveExamItemGroupExamRel(int ID_ExamItemGroup, string newExamItemIDStrs);

		int SaveExamItem(BusExamItem ExamItemModel);

		int SaveSymptom(BusSymptom BusSymptomModel);

		int SaveUser(SYSOpUser UserModel);

		int ClearUserLoginCount(int ID_User);

		int ResetUserPassword(int ID_User, string DefaultPassword);

		int SaveBusPEPackage(BusPEPackage PEPackageModel);

		int SaveSetFeeRel(int PEPackageID, string newFeeIDStrs);

		int MergeCustExamInfo(string MergerID_01, string MergerID_02, string[] ConnectionStringArray);

		int SaveConclusionType(BusConclusionType ConclusionTypeModel);

		int SaveFinalConclusionType(DctFinalConclusionType FinalConclusionTypeModel);

		int SaveICD(DctICDTen ICDTenModel);

		int SaveFeeReport(BusFeeReport FeeReportModel);

		int SaveExamItemGroup(BusExamItemGroup ExamItemGroupModel);
	}
}
