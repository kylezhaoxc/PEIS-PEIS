using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictCultrul
	{
		int GetMaxId();

		bool Exists(int CultrulID);

		int Add(DictCultrul model);

		bool Update(DictCultrul model);

		bool Delete(int CultrulID);

		bool DeleteList(string CultrulIDlist);

        DictCultrul GetModel(int CultrulID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
