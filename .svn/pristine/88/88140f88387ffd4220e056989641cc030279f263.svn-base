using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictReceiveReportWay
    {
		int GetMaxId();

		bool Exists(int ID_ReportWay);

		int Add(DictReceiveReportWay model);

		bool Update(DictReceiveReportWay model);

		bool Delete(int ID_ReportWay);

		bool DeleteList(string ID_ReportWaylist);

        DictReceiveReportWay GetModel(int ID_ReportWay);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
