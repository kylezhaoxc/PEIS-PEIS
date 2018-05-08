using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SysRole
    {
		private static readonly SysRole _instance = new SysRole();

		private readonly ISysRole dal = DataAccess.CreateNatRole();

		public static SysRole Instance
		{
			get
			{
				return SysRole._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Role)
		{
			return this.dal.Exists(ID_Role);
		}

		public int Add(PEIS.Model.SysRole model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SysRole model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Role)
		{
			return this.dal.Delete(ID_Role);
		}

		public bool DeleteList(string ID_Rolelist)
		{
			return this.dal.DeleteList(ID_Rolelist);
		}

		public PEIS.Model.SysRole GetModel(int ID_Role)
		{
			return this.dal.GetModel(ID_Role);
		}

		public PEIS.Model.SysRole GetModelByCache(int RoleID)
		{
			string cacheKey = "SysRoleModel-" + RoleID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(RoleID);
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
			return (PEIS.Model.SysRole)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SysRole> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SysRole> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.SysRole> list = new List<PEIS.Model.SysRole>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.SysRole sysRole = new PEIS.Model.SysRole();
					if (dt.Rows[i]["RoleID"].ToString() != "")
					{
                        sysRole.RoleID = int.Parse(dt.Rows[i]["RoleID"].ToString());
					}
                    sysRole.RoleName = dt.Rows[i]["RoleName"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
                        sysRole.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString());
					}
					if (dt.Rows[i]["Is_Locked"].ToString() != "")
					{
                        sysRole.Is_Locked = int.Parse(dt.Rows[i]["Is_Locked"].ToString());
					}
                    sysRole.Remark = dt.Rows[i]["Remark"].ToString();
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
                        sysRole.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					}
					if (dt.Rows[i]["OperatorID"].ToString() != "")
					{
                        sysRole.OperatorID = new int?(int.Parse(dt.Rows[i]["OperatorID"].ToString()));
					}
					if (dt.Rows[i]["Is_DefaultRole"].ToString() != "")
					{
                        sysRole.Is_DefaultRole = new int?(int.Parse(dt.Rows[i]["Is_DefaultRole"].ToString()));
					}
					list.Add(sysRole);
				}
			}
			return list;
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}
	}
}
