using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusFee
	{
		int GetMaxId();

		bool Exists(int ID_Fee);

		int Add(BusFee model);

		bool Update(BusFee model);

		bool Delete(int ID_Fee);

		bool DeleteList(string ID_Feelist);

		BusFee GetModel(int ID_Fee);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
