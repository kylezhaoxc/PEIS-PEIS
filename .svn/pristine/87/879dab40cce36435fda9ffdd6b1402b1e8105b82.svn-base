using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IDictExamPlace
	{
		int GetMaxId();

		bool Exists(int ExamPlaceID);

		int Add(DictExamPlace model);

		bool Update(DictExamPlace model);

		bool Delete(int ExamPlaceID);

		bool DeleteList(string ExamPlaceIDlist);

		DictExamPlace GetModel(int ExamPlaceID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
