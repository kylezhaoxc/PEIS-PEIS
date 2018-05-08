using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SYSRight
    {
		private static readonly SYSRight _instance = new SYSRight();

		private readonly ISYSRight dal = DataAccess.CreateSYSRight();

		public static SYSRight Instance
		{
			get
			{
				return SYSRight._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Right)
		{
			return this.dal.Exists(ID_Right);
		}

		public int Add(PEIS.Model.SYSRight model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SYSRight model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Right)
		{
			return this.dal.Delete(ID_Right);
		}

		public bool DeleteList(string ID_Rightlist)
		{
			return this.dal.DeleteList(ID_Rightlist);
		}

		public PEIS.Model.SYSRight GetModel(int ID_Right)
		{
			return this.dal.GetModel(ID_Right);
		}

		public PEIS.Model.SYSRight GetModelByCache(int ID_Right)
		{
			string cacheKey = "SYSRightModel-" + ID_Right;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Right);
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
			return (PEIS.Model.SYSRight)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SYSRight> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SYSRight> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.SYSRight> list = new List<PEIS.Model.SYSRight>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.SYSRight sysRight = new PEIS.Model.SYSRight();
					if (dt.Rows[i]["ID_Right"].ToString() != "")
					{
                        sysRight.RightID = int.Parse(dt.Rows[i]["RightID"].ToString());
					}
                    sysRight.RightName = dt.Rows[i]["RightName"].ToString();
                    sysRight.RightCode = dt.Rows[i]["RightCode"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
                        sysRight.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString());
					}
					if (dt.Rows[i]["Is_Locked"].ToString() != "")
					{
                        sysRight.Is_Locked = int.Parse(dt.Rows[i]["Is_Locked"].ToString());
					}
					if (dt.Rows[i]["ID_ParentRight"].ToString() != "")
					{
                        sysRight.ParentID = new int?(int.Parse(dt.Rows[i]["ParentID"].ToString()));
					}
                    sysRight.Remark = dt.Rows[i]["Remark"].ToString();
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
                        sysRight.CreateDate = DateTime.Parse(dt.Rows[i]["CreateDate"].ToString());
					}
					if (dt.Rows[i]["OperatorID"].ToString() != "")
					{
                        sysRight.OperatorID = new int?(int.Parse(dt.Rows[i]["OperatorID"].ToString()));
					}
					//if (dt.Rows[i]["LastSubItemCount"].ToString() != "")
					//{
     //                   sysRight.LastSubItemCount = new int?(int.Parse(dt.Rows[i]["LastSubItemCount"].ToString()));
					//}
					if (dt.Rows[i]["RightLevel"].ToString() != "")
					{
                        sysRight.RightLevel = new int?(int.Parse(dt.Rows[i]["RightLevel"].ToString()));
					}
					list.Add(sysRight);
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
