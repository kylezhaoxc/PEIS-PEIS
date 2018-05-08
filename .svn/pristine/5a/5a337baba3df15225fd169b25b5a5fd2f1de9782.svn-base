using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictExamType
	{
		int GetMaxId();

		bool Exists(int ExamTypeID);

		int Add(DictExamType model);

		bool Update(DictExamType model);

		bool Delete(int ExamTypeID);

		bool DeleteList(string ExamTypeIDlist);

        DictExamType GetModel(int ExamTypeID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
