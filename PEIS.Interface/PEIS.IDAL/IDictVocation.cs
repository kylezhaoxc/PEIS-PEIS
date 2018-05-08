using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictVocation
	{
		int GetMaxId();

		bool Exists(int VocationID);

		int Add(DictVocation model);

		bool Update(DictVocation model);

		bool Delete(int VocationID);

		bool DeleteList(string VocationIDlist);

		DictVocation GetModel(int VocationID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
