using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustExamItem
	{
		int GetMaxId();

		bool Exists(int ID_CustExamItem);

		int Add(OnCustExamItem model);

		bool Update(OnCustExamItem model);

		bool Delete(int ID_CustExamItem);

		bool DeleteList(string ID_CustExamItemlist);

		OnCustExamItem GetModel(int ID_CustExamItem);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
