using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusSymptom
	{
		int GetMaxId();

		bool Exists(int ID_Symptom);

		int Add(BusSymptom model);

		bool Update(BusSymptom model);

		bool Delete(int ID_Symptom);

		bool DeleteList(string ID_Symptomlist);

		BusSymptom GetModel(int ID_Symptom);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
