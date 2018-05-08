using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class NatLog
	{
		private readonly INatLog dal = DataAccess.CreateNatLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Log)
		{
			return this.dal.Exists(ID_Log);
		}

		public int Add(PEIS.Model.NatLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.NatLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Log)
		{
			return this.dal.Delete(ID_Log);
		}

		public bool DeleteList(string ID_Loglist)
		{
			return this.dal.DeleteList(ID_Loglist);
		}

		public PEIS.Model.NatLog GetModel(int ID_Log)
		{
			return this.dal.GetModel(ID_Log);
		}

		public PEIS.Model.NatLog GetModelByCache(int ID_Log)
		{
			string cacheKey = "NatLogModel-" + ID_Log;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Log);
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
			return (PEIS.Model.NatLog)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.NatLog> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.NatLog> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.NatLog> list = new List<PEIS.Model.NatLog>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.NatLog natLog = new PEIS.Model.NatLog();
					if (dt.Rows[i]["ID_Log"].ToString() != "")
					{
						natLog.ID_Log = int.Parse(dt.Rows[i]["ID_Log"].ToString());
					}
					natLog.Operater = dt.Rows[i]["Operater"].ToString();
					if (dt.Rows[i]["OperateDate"].ToString() != "")
					{
						natLog.OperateDate = DateTime.Parse(dt.Rows[i]["OperateDate"].ToString());
					}
					natLog.OperateIP = dt.Rows[i]["OperateIP"].ToString();
					if (dt.Rows[i]["OperateType"].ToString() != "")
					{
						natLog.OperateType = int.Parse(dt.Rows[i]["OperateType"].ToString());
					}
					natLog.OperateContent = dt.Rows[i]["OperateContent"].ToString();
					list.Add(natLog);
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
