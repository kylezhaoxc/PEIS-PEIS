using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusSetFeeDetail
	{
		int GetMaxId();

		bool Exists(int ID_DtlSetFee);

		int Add(BusSetFeeDetail model);

		bool Update(BusSetFeeDetail model);

		bool Delete(int ID_DtlSetFee);

		bool DeleteList(string ID_DtlSetFeelist);

		BusSetFeeDetail GetModel(int ID_DtlSetFee);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
