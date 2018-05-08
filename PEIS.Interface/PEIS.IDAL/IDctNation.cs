using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictNation
	{
		int GetMaxId();

		bool Exists(int NationID);

		int Add(DictNation model);

		bool Update(DictNation model);

		bool Delete(int NationID);

		bool DeleteList(string NationIDlist);

		DictNation GetModel(int NationID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
