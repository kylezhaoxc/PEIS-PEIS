using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;

namespace PEIS.BLL
{
	internal class CommonSubScribe
	{
		private static ICommonSubScribe dal = DataAccess.CreateCommonSubScribe();

		private static readonly CommonSubScribe _instance = new CommonSubScribe();

		public static CommonSubScribe Instance
		{
			get
			{
				return CommonSubScribe._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonSubScribe.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}
	}
}
