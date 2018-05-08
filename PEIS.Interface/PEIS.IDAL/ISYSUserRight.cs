using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISYSUserRight
    {
		int GetMaxId();

		bool Exists(int ID_UserRight);

		int Add(SYSUserRight model);

		bool Update(SYSUserRight model);

		bool Delete(int ID_UserRight);

		bool DeleteList(string ID_UserRightlist);

        SYSUserRight GetModel(int ID_UserRight);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
