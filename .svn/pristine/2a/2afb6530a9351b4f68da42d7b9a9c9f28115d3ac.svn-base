using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusConclusionType
	{
		int GetMaxId();

		bool Exists(int ID_ConclusionType);

		int Add(BusConclusionType model);

		bool Update(BusConclusionType model);

		bool Delete(int ID_ConclusionType);

		bool DeleteList(string ID_ConclusionTypelist);

		BusConclusionType GetModel(int ID_ConclusionType);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
