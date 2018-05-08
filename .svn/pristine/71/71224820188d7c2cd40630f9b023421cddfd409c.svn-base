using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusExamItem
	{
		int GetMaxId();

		bool Exists(int ID_ExamItem);

		int Add(BusExamItem model);

		bool Update(BusExamItem model);

		bool Delete(int ID_ExamItem);

		bool DeleteList(string ID_ExamItemlist);

		BusExamItem GetModel(int ID_ExamItem);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
