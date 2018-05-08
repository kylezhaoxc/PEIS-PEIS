using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.IDAL
{
	public interface ICommonExcuteSql
	{
		DataSet ExcuteSql(string sql);

		DataSet ExcuteSql(string AppSettingKey, string sql);

		int ExecuteSqlTran(List<string> SQLStringList);

		int ExecuteSql(string SQLString);
	}
}
