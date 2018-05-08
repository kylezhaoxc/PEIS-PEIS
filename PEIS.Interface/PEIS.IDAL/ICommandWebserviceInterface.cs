using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommandWebserviceInterface
	{
		DataSet ExcuteSql(string sql);

		int ExecuteSqlTran(List<string> SQLStringList);
	}
}
