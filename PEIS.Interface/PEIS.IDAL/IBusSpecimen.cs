using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IBusSpecimen
	{
		int GetMaxId();

		bool Exists(int ID_Specimen);

		int Add(BusSpecimen model);

		bool Update(BusSpecimen model);

		bool Delete(int ID_Specimen);

		bool DeleteList(string ID_Specimenlist);

		BusSpecimen GetModel(int ID_Specimen);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
