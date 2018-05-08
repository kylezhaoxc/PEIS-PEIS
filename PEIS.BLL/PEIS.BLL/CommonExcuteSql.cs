using PEIS.DALFactory;
using PEIS.IDAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonExcuteSql
	{
		private readonly ICommonExcuteSql dal = DataAccess.CreateCommonExcuteSql();

		private static readonly CommonExcuteSql _instance = new CommonExcuteSql();

		public static CommonExcuteSql Instance
		{
			get
			{
				return CommonExcuteSql._instance;
			}
		}

		public DataSet ExcuteSql(string sql)
		{
			return this.dal.ExcuteSql(sql);
		}

		public DataSet ExcuteSql(string AppSettingKey, string sql)
		{
			return this.dal.ExcuteSql(AppSettingKey, sql);
		}

		public int ExecuteSqlTran(List<string> SQLStringList)
		{
			return this.dal.ExecuteSqlTran(SQLStringList);
		}

		public int ExecuteSql(string SQLString)
		{
			return this.dal.ExecuteSql(SQLString);
		}
	}
}
