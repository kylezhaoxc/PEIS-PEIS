using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SYSUserRole
    {
		private readonly ISYSUserRole dal = DataAccess.CreateSYSUserRole();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_UserRole)
		{
			return this.dal.Exists(ID_UserRole);
		}

		public int Add(PEIS.Model.SYSUserRole model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SYSUserRole model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_UserRole)
		{
			return this.dal.Delete(ID_UserRole);
		}

		public bool DeleteList(string ID_UserRolelist)
		{
			return this.dal.DeleteList(ID_UserRolelist);
		}

		public PEIS.Model.SYSUserRole GetModel(int ID_UserRole)
		{
			return this.dal.GetModel(ID_UserRole);
		}

		public PEIS.Model.SYSUserRole GetModelByCache(int ID_UserRole)
		{
			string cacheKey = "sysUserRoleModel-" + ID_UserRole;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_UserRole);
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
			return (PEIS.Model.SYSUserRole)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SYSUserRole> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SYSUserRole> DataTableToList(DataTable dt)
		{
            List<PEIS.Model.SYSUserRole> modelList = new List<PEIS.Model.SYSUserRole>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PEIS.Model.SYSUserRole model;
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
