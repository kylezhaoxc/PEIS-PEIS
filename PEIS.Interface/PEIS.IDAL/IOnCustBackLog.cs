using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustBackLog
	{
		int GetMaxId();

		bool Exists(int ID_User);

		int Add(OnCustBackLog model);

		bool Update(OnCustBackLog model);

		bool Delete(int ID_User);

		bool DeleteList(string ID_Userlist);

		OnCustBackLog GetModel(int ID_User);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);

		int AddOrUpdateByBackLogType(OnCustBackLog model);
	}
}
