using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusPEPackage
    {
		int GetMaxId();

		bool Exists(int PEPackageID);

		int Add(BusPEPackage model);

		bool Update(BusPEPackage model);

		bool Delete(int PEPackageID);

		bool DeleteList(string PEPackageIDlist);

        BusPEPackage GetModel(int PEPackageID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
