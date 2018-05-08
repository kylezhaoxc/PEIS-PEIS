using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustFee
	{
		int GetMaxId();

		bool Exists(int ID_CustFee);

		int Add(OnCustFee model);

		bool Update(OnCustFee model);

		bool Delete(int ID_CustFee);

		bool DeleteList(string ID_CustFeelist);

		OnCustFee GetModel(int ID_CustFee);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
