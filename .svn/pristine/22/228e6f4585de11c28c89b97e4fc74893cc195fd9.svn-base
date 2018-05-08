using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustExamSection
	{
		int GetMaxId();

		bool Exists(int ID_CustExamSection);

		int Add(OnCustExamSection model);

		bool Update(OnCustExamSection model);

		bool Delete(int ID_CustExamSection);

		bool DeleteList(string ID_CustExamSectionlist);

		OnCustExamSection GetModel(int ID_CustExamSection);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
