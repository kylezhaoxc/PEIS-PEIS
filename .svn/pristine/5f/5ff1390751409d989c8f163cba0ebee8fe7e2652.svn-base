using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SysMenu
    {
		private readonly ISysMenu dal = DataAccess.CreateNatMenu();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Menu)
		{
			return this.dal.Exists(ID_Menu);
		}

		public int Add(PEIS.Model.SysMenu model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SysMenu model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Menu)
		{
			return this.dal.Delete(ID_Menu);
		}

		public bool DeleteList(string ID_Menulist)
		{
			return this.dal.DeleteList(ID_Menulist);
		}

		public PEIS.Model.SysMenu GetModel(int ID_Menu)
		{
			return this.dal.GetModel(ID_Menu);
		}

		public PEIS.Model.SysMenu GetModelByCache(int ID_Menu)
		{
			string cacheKey = "SysMenuModel-" + ID_Menu;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Menu);
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
			return (PEIS.Model.SysMenu)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SysMenu> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SysMenu> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.SysMenu> list = new List<PEIS.Model.SysMenu>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.SysMenu sysMenu = new PEIS.Model.SysMenu();
					if (dt.Rows[i]["MenuID"].ToString() != "")
					{
                        sysMenu.MenuID = int.Parse(dt.Rows[i]["MenuID"].ToString());
					}
                    sysMenu.MenuName = dt.Rows[i]["MenuName"].ToString();
					if (dt.Rows[i]["ID_ParentMenu"].ToString() != "")
					{
                        sysMenu.ParentID = new int?(int.Parse(dt.Rows[i]["ParentID"].ToString()));
					}
                    sysMenu.Url = dt.Rows[i]["Url"].ToString();
					//if (dt.Rows[i]["CreateDate"].ToString() != "")
					//{
     //                   sysMenu.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					//}
					//if (dt.Rows[i]["UpdateDate"].ToString() != "")
					//{
     //                   sysMenu.UpdateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["UpdateDate"].ToString()));
					//}
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
                        sysMenu.DispOrder = new int?(int.Parse(dt.Rows[i]["DispOrder"].ToString()));
					}
                    sysMenu.RightCode = dt.Rows[i]["RightCode"].ToString();
					//if (dt.Rows[i]["ID_Operator"].ToString() != "")
					//{
     //                   sysMenu.ID_Operator = new int?(int.Parse(dt.Rows[i]["ID_Operator"].ToString()));
					//}
					if (dt.Rows[i]["Is_CombineWithSection"].ToString() != "")
					{
						if (dt.Rows[i]["Is_CombineWithSection"].ToString() == "1" || dt.Rows[i]["Is_CombineWithSection"].ToString().ToLower() == "true")
						{
                            sysMenu.Is_CombineWithSection =true;
						}
						else
						{
                            sysMenu.Is_CombineWithSection = false;
						}
					}
					list.Add(sysMenu);
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
