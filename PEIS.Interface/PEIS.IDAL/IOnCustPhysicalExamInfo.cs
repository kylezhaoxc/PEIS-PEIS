using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustPhysicalExamInfo
	{
		bool Exists(long ID_Customer);

		void Add(OnCustPhysicalExamInfo model);

		bool Update(OnCustPhysicalExamInfo model);

		bool Delete(long ID_Customer);

		bool DeleteList(string ID_Customerlist);

		OnCustPhysicalExamInfo GetModel(long ID_Customer);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
