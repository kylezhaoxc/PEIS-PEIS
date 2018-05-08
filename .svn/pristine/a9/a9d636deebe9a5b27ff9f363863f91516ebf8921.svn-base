using PEIS.DALFactory;
using PEIS.IDAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommandWebserviceInterface
	{
		private readonly ICommandWebserviceInterface dal = DataAccess.CreateCommandWebserviceInterface();

		private static readonly CommandWebserviceInterface _instance = new CommandWebserviceInterface();

		public static CommandWebserviceInterface Instance
		{
			get
			{
				return CommandWebserviceInterface._instance;
			}
		}

		public DataSet ExcuteSql(string SQLString)
		{
			return this.dal.ExcuteSql(SQLString);
		}

		public int ExecuteSqlTran(List<string> SQLStringList)
		{
			return this.dal.ExecuteSqlTran(SQLStringList);
		}
	}
}
