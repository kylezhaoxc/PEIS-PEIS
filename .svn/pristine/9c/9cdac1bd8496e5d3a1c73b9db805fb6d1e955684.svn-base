using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnArcCust
	{
		int GetMaxId();

		bool Exists(int ID_ArcCustomer);

		int Add(OnArcCust model);

		bool Update(OnArcCust model);

		bool Delete(int ID_ArcCustomer);

		bool DeleteList(string ID_ArcCustomerlist);

		OnArcCust GetModel(int ID_ArcCustomer);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
