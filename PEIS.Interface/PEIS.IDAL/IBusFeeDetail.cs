using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusFeeDetail
	{
		int GetMaxId();

		bool Exists(int ID_DtlFee);

		int Add(BusFeeDetail model);

		bool Update(BusFeeDetail model);

		bool Delete(int ID_DtlFee);

		bool DeleteList(string ID_DtlFeelist);

		BusFeeDetail GetModel(int ID_DtlFee);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
