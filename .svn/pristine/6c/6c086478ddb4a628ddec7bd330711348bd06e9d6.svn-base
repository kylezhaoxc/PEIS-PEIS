using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISYSSection
	{
		int GetMaxId();

		bool Exists(int ID_Section);

		int Add(SYSSection model);

		bool Update(SYSSection model);

		bool Delete(int ID_Section);

		bool DeleteList(string ID_Sectionlist);

		SYSSection GetModel(int ID_Section);

        SYSSection DataRowToModel(DataRow row);

        DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
