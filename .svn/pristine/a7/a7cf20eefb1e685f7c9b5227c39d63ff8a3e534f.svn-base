using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISysRole
	{
		int GetMaxId();

		bool Exists(int ID_Role);

		int Add(SysRole model);

		bool Update(SysRole model);

		bool Delete(int ID_Role);

		bool DeleteList(string ID_Rolelist);

        SysRole GetModel(int ID_Role);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
