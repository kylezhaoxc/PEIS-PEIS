using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictMarriage
    {
		int GetMaxId();

		bool Exists(int MarriageID);

		int Add(DictMarriage model);

		bool Update(DictMarriage model);

		bool Delete(int ID_Marriage);

		bool DeleteList(string ID_Marriagelist);

        DictMarriage GetModel(int ID_Marriage);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
