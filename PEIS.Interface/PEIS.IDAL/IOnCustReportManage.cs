using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustReportManage
	{
		int GetMaxId();

		bool Exists(int ID_ReportManage);

		int Add(OnCustReportManage model);

		bool Update(OnCustReportManage model);

		bool Delete(int ID_ReportManage);

		bool DeleteList(string ID_ReportManagelist);

		OnCustReportManage GetModel(int ID_ReportManage);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
