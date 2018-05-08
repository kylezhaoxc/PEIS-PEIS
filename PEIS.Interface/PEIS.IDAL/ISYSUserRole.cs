using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISYSUserRole
	{
		int GetMaxId();

		bool Exists(int ID_UserRole);

		int Add(SYSUserRole model);

		bool Update(SYSUserRole model);

		bool Delete(int ID_UserRole);

		bool DeleteList(string ID_UserRolelist);

		SYSUserRole GetModel(int ID_UserRole);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);

        PEIS.Model.SYSUserRole DataRowToModel(DataRow row);
    }
}
