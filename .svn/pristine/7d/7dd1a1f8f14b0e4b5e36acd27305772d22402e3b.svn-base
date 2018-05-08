using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusExamItemGroup
	{
		int GetMaxId();

		bool Exists(int ID_ExamItemGroup);

		int Add(BusExamItemGroup model);

		bool Update(BusExamItemGroup model);

		bool Delete(int ID_ExamItemGroup);

		bool DeleteList(string ID_ExamItemGrouplist);

		BusExamItemGroup GetModel(int ID_ExamItemGroup);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
