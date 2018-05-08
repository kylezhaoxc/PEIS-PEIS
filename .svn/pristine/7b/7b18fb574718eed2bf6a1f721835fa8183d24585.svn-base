using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface IOnCustRelationCustPEInfo
	{
		int GetMaxId();

		bool Exists(int ID_CustRelation);

		int Add(OnCustRelationCustPEInfo model);

		bool Update(OnCustRelationCustPEInfo model);

		bool Delete(int ID_CustRelation);

		bool DeleteList(string ID_CustRelationlist);

		OnCustRelationCustPEInfo GetModel(int ID_CustRelation);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
