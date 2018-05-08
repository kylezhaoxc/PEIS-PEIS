using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDctICDTen
	{
		int GetMaxId();

		bool Exists(int ID_ICD);

		int Add(DctICDTen model);

		bool Update(DctICDTen model);

		bool Delete(int ID_ICD);

		bool DeleteList(string ID_ICDlist);

		DctICDTen GetModel(int ID_ICD);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
