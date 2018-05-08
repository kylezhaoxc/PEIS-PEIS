using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnFianlCheck
	{
		int GetMaxId();

		bool Exists(int ID_FinalCheck);

		int Add(OnFianlCheck model);

		bool Update(OnFianlCheck model);

		bool Delete(int ID_FinalCheck);

		bool DeleteList(string ID_FinalChecklist);

		OnFianlCheck GetModel(int ID_FinalCheck);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
