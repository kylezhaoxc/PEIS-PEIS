using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictGender
    {
		int GetMaxId();

		bool Exists(int GenderID);

		int Add(DictGender model);

		bool Update(DictGender model);

		bool Delete(int GenderID);

		bool DeleteList(string GenderIDlist);

        DictGender GetModel(int GenderID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
