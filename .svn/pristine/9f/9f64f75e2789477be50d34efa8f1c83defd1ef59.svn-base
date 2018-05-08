using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SYSSection
    {
		private readonly ISYSSection dal = DataAccess.CreateSYSSection();

		private static readonly SYSSection _instance = new SYSSection();

		public static SYSSection Instance
		{
			get
			{
				return SYSSection._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Section)
		{
			return this.dal.Exists(ID_Section);
		}

		public int Add(PEIS.Model.SYSSection model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SYSSection model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Section)
		{
			return this.dal.Delete(ID_Section);
		}

		public bool DeleteList(string ID_Sectionlist)
		{
			return this.dal.DeleteList(ID_Sectionlist);
		}

		public PEIS.Model.SYSSection GetModel(int ID_Section)
		{
			return this.dal.GetModel(ID_Section);
		}

		public PEIS.Model.SYSSection GetModelByCache(int ID_Section)
		{
			string cacheKey = "SectionModel-" + ID_Section;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Section);
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
			return (PEIS.Model.SYSSection)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SYSSection> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SYSSection> DataTableToList(DataTable dt)
		{
            List<PEIS.Model.SYSSection> modelList = new List<PEIS.Model.SYSSection>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PEIS.Model.SYSSection model;
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
