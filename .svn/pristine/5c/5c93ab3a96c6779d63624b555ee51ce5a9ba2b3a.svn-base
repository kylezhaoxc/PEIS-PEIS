using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictFeeWay
	{
		int GetMaxId();

		bool Exists(int FeeWayID);

		int Add(DictFeeWay model);

		bool Update(DictFeeWay model);

		bool Delete(int FeeWayID);

		bool DeleteList(string FeeWayIDlist);

        DictFeeWay GetModel(int FeeWayID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
