using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISYSRight
	{
		int GetMaxId();

		bool Exists(int ID_Right);

		int Add(SYSRight model);

		bool Update(SYSRight model);

		bool Delete(int RightID);

		bool DeleteList(string RightIDlist);

        SYSRight GetModel(int RightID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
