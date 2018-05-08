using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustExamItemResult
	{
		int GetMaxId();

		bool Exists(int ID_ExamItemResult);

		int Add(OnCustExamItemResult model);

		bool Update(OnCustExamItemResult model);

		bool Delete(int ID_ExamItemResult);

		bool DeleteList(string ID_ExamItemResultlist);

		OnCustExamItemResult GetModel(int ID_ExamItemResult);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
