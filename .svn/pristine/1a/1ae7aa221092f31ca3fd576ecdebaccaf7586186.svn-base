using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustConclusion
	{
		int GetMaxId();

		bool Exists(int ID_CustConclusion);

		int Add(OnCustConclusion model);

		bool Update(OnCustConclusion model);

		bool Delete(int ID_CustConclusion);

		bool DeleteList(string ID_CustConclusionlist);

		OnCustConclusion GetModel(int ID_CustConclusion);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
