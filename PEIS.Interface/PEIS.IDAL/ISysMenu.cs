using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISysMenu
    {
		int GetMaxId();

		bool Exists(int ID_Menu);

		int Add(SysMenu model);

		bool Update(SysMenu model);

		bool Delete(int ID_Menu);

		bool DeleteList(string ID_Menulist);

        SysMenu GetModel(int ID_Menu);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
