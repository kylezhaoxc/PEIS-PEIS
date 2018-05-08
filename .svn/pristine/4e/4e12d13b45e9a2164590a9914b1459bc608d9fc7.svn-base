using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustApply
	{
		bool Exists(string ID_Apply);

		void Add(OnCustApply model);

		bool Update(OnCustApply model);

		bool Delete(string ID_Apply);

		bool DeleteList(string ID_Applylist);

		OnCustApply GetModel(string ID_Apply);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
