using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonConclusion
	{
		DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions);

		DataSet ExcuteQuerySqlX(string AppSettingKey, string QuerySqlCode, params SqlConditionInfo[] conditions);

		int SaveFinalConclusionData(OnCustPhysicalExamInfo OCPEIModel, List<OnCustConclusion> OnCustConclusionList);

		int UpdateFinalConclusionSectionLock(OnCustPhysicalExamInfo OCPEIModel);

		int SaveCustomerFinalCheck(OnFianlCheck OFCModel);

		int LockCustomerFinalCheck(OnCustPhysicalExamInfo OCPEIModel);

		OnFianlCheck GetCustomerLastOnFianlCheck(long ID_Customer);

		int SaveCustomerFinaUnCheck(OnFianlCheck OFCModel);

		int UpdateCustomerNotFinalFinished(OnCustPhysicalExamInfo OCPEIModel);

		int SaveConclusion(BusConclusion ConclusionModel);
	}
}
