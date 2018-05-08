using PEIS.Model;
using System;
using System.Data;

namespace PEIS.IDAL
{
	public interface ISYSUserSection
	{
		void Add(SYSUserSection model);

		bool Update(SYSUserSection model);

		bool Delete(int UserSectionID);

		SYSUserSection GetModel(int UserSectionID);

		DataSet GetList(string strWhere);

		DataSet GetList(int Top, string strWhere, string filedOrder);
	}
}
