using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISysRoleRight
	{
		void Add(SysRoleRight model);

		bool Update(SysRoleRight model);

		bool Delete(int RoleRightID);

        SysRoleRight GetModel(int RoleRightID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);

        PEIS.Model.SysRoleRight DataRowToModel(DataRow row);


    }
}
