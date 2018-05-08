using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SysRoleRight
    {
		private readonly ISysRoleRight dal = DataAccess.CreateNatRoleRight();

		public void Add(PEIS.Model.SysRoleRight model)
		{
			this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SysRoleRight model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int RoleRightID)
		{
			return this.dal.Delete(RoleRightID);
		}

		public PEIS.Model.SysRoleRight GetModel(int RoleRightID)
		{
			return this.dal.GetModel(RoleRightID);
		}

		public PEIS.Model.SysRoleRight GetModelByCache(int RoleRightID)
		{
			string cacheKey = "SysRoleRightModel-";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(RoleRightID);
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			return (PEIS.Model.SysRoleRight)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SysRoleRight> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SysRoleRight> DataTableToList(DataTable dt)
		{
            List<PEIS.Model.SysRoleRight> modelList = new List<PEIS.Model.SysRoleRight>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PEIS.Model.SysRoleRight model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

		public DataSet GetAllList()
		{
			return this.GetList("");
		}
	}
}
