using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface INatLog
	{
		int GetMaxId();

		bool Exists(int ID_Log);

		int Add(NatLog model);

		bool Update(NatLog model);

		bool Delete(int ID_Log);

		bool DeleteList(string ID_Loglist);

		NatLog GetModel(int ID_Log);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
