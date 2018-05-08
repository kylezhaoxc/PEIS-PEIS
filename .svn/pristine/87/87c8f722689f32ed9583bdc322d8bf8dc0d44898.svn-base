using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDctFinalConclusionType
	{
		int GetMaxId();

		bool Exists(int ID_FinalConclusionType);

		int Add(DctFinalConclusionType model);

		bool Update(DctFinalConclusionType model);

		bool Delete(int ID_FinalConclusionType);

		bool DeleteList(string ID_FinalConclusionTypelist);

		DctFinalConclusionType GetModel(int ID_FinalConclusionType);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
