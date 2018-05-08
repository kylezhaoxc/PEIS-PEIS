using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SYSUserSection
    {
		private readonly ISYSUserSection dal = DataAccess.CreateSYSUserSectionn();

		public void Add(PEIS.Model.SYSUserSection model)
		{
			this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SYSUserSection model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int SectionID)
		{
			return this.dal.Delete(SectionID);
		}

		public PEIS.Model.SYSUserSection GetModel(int SectionID)
		{
			return this.dal.GetModel(SectionID);
		}

		public PEIS.Model.SYSUserSection GetModelByCache(int SectionID)
		{
			string cacheKey = "SYSUserSectionModel-";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(SectionID);
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
			return (PEIS.Model.SYSUserSection)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SYSUserSection> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SYSUserSection> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.SYSUserSection> list = new List<PEIS.Model.SYSUserSection>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.SYSUserSection natUserSection = new PEIS.Model.SYSUserSection();
					if (dt.Rows[i]["UserSectionID"].ToString() != "")
					{
						natUserSection.UserSectionID =Convert.ToInt32(dt.Rows[i]["UserSectionID"].ToString());
					}
					if (dt.Rows[i]["SectionID"].ToString() != "")
					{
						natUserSection.SectionID = new int?(int.Parse(dt.Rows[i]["SectionID"].ToString()));
					}
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
						natUserSection.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					}
					if (dt.Rows[i]["OperatorID"].ToString() != "")
					{
						natUserSection.OperatorID = new int?(int.Parse(dt.Rows[i]["OperatorID"].ToString()));
					}
					if (dt.Rows[i]["UserID"].ToString() != "")
					{
						natUserSection.UserID = new int?(int.Parse(dt.Rows[i]["UserID"].ToString()));
					}
					list.Add(natUserSection);
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
