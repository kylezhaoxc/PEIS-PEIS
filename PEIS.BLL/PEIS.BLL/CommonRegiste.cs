using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;

namespace PEIS.BLL
{
	public class CommonRegiste
	{
		private static ICommonRegiste dal = DataAccess.CreateCommonRegiste();

		private static readonly CommonRegiste _instance = new CommonRegiste();

		public static CommonRegiste Instance
		{
			get
			{
				return CommonRegiste._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonRegiste.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}
	}
}
