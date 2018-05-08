using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictCountry
	{
		int GetMaxId();

		bool Exists(int CountryID);

		int Add(DictCountry model);

		bool Update(DictCountry model);

		bool Delete(int CountryID);

		bool DeleteList(string CountryIDlist);

        DictCountry GetModel(int CountryID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
