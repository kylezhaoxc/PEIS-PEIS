using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISYSOpUser
    {
		int GetMaxId();

		bool Exists(int ID_User);

		int Add(SYSOpUser model);

		bool Update(SYSOpUser model);

		bool Delete(int ID_User);

		bool DeleteList(string ID_Userlist);

        SYSOpUser GetModel(int ID_User);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
