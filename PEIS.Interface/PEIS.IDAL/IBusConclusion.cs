using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusConclusion
	{
		int GetMaxId();

		bool Exists(int ID_Conclusion);

		int Add(BusConclusion model);

		bool Update(BusConclusion model);

		bool Delete(int ID_Conclusion);

		bool DeleteList(string ID_Conclusionlist);

		BusConclusion GetModel(int ID_Conclusion);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
