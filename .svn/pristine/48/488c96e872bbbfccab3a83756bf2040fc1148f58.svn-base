using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusFeeReport
	{
		int GetMaxId();

		bool Exists(int ID_FeeReport);

		int Add(BusFeeReport model);

		bool Update(BusFeeReport model);

		bool Delete(int ID_FeeReport);

		bool DeleteList(string ID_FeeReportlist);

		BusFeeReport GetModel(int ID_FeeReport);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
