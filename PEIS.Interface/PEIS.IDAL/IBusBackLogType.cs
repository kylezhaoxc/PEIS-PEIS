using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusBackLogType
	{
		int GetMaxId();

		bool Exists(int ID_User);

		int Add(BusBackLogType model);

		bool Update(BusBackLogType model);

		bool Delete(int ID_User);

		bool DeleteList(string ID_Userlist);

		BusBackLogType GetModel(int ID_User);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
