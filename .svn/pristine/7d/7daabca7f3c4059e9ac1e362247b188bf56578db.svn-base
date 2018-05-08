using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SYSUserRight
    {
		private readonly ISYSUserRight dal = DataAccess.CreateSYSUserRight();
        public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_UserRight)
		{
			return this.dal.Exists(ID_UserRight);
		}

		public int Add(PEIS.Model.SYSUserRight model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SYSUserRight model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_UserRight)
		{
			return this.dal.Delete(ID_UserRight);
		}

		public bool DeleteList(string ID_UserRightlist)
		{
			return this.dal.DeleteList(ID_UserRightlist);
		}

		public PEIS.Model.SYSUserRight GetModel(int ID_UserRight)
		{
			return this.dal.GetModel(ID_UserRight);
		}

		public PEIS.Model.SYSUserRight GetModelByCache(int UserRightID)
		{
			string cacheKey = "SYSUserRightModel-" + UserRightID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(UserRightID);
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
			return (PEIS.Model.SYSUserRight)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SYSUserRight> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SYSUserRight> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.SYSUserRight> list = new List<PEIS.Model.SYSUserRight>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.SYSUserRight sysUserRight = new PEIS.Model.SYSUserRight();
					if (dt.Rows[i]["ID_UserRight"].ToString() != "")
					{
                        sysUserRight.UserRightID = int.Parse(dt.Rows[i]["UserRightID"].ToString());
					}
					if (dt.Rows[i]["RightID"].ToString() != "")
					{
                        sysUserRight.RightID = new int?(int.Parse(dt.Rows[i]["RightID"].ToString()));
					}
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
                        sysUserRight.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					}
					if (dt.Rows[i]["OperatorID"].ToString() != "")
					{
                        sysUserRight.OperatorID = new int?(int.Parse(dt.Rows[i]["OperatorID"].ToString()));
					}
					if (dt.Rows[i]["UserID"].ToString() != "")
					{
                        sysUserRight.UserID = new int?(int.Parse(dt.Rows[i]["UserID"].ToString()));
					}
					list.Add(sysUserRight);
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
