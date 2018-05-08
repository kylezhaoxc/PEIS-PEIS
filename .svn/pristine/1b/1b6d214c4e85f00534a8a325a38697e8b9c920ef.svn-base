using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusSpecimen
	{
		private static readonly BusSpecimen _instance = new BusSpecimen();

		private readonly IBusSpecimen dal = DataAccess.CreateBusSpecimen();

		public static BusSpecimen Instance
		{
			get
			{
				return BusSpecimen._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Specimen)
		{
			return this.dal.Exists(ID_Specimen);
		}

		public int Add(PEIS.Model.BusSpecimen model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusSpecimen model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Specimen)
		{
			return this.dal.Delete(ID_Specimen);
		}

		public bool DeleteList(string ID_Specimenlist)
		{
			return this.dal.DeleteList(ID_Specimenlist);
		}

		public PEIS.Model.BusSpecimen GetModel(int ID_Specimen)
		{
			return this.dal.GetModel(ID_Specimen);
		}

		public PEIS.Model.BusSpecimen GetModelByCache(int ID_Specimen)
		{
			string cacheKey = "BusSpecimenModel-" + ID_Specimen;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Specimen);
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
			return (PEIS.Model.BusSpecimen)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusSpecimen> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusSpecimen> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusSpecimen> list = new List<PEIS.Model.BusSpecimen>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusSpecimen busSpecimen = new PEIS.Model.BusSpecimen();
					if (dt.Rows[i]["ID_Specimen"].ToString() != "")
					{
						busSpecimen.ID_Specimen = int.Parse(dt.Rows[i]["ID_Specimen"].ToString());
					}
					busSpecimen.SpecimenName = dt.Rows[i]["SpecimenName"].ToString();
					busSpecimen.InputCode = dt.Rows[i]["InputCode"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						busSpecimen.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString());
					}
					busSpecimen.LisSpecimenName = dt.Rows[i]["LisSpecimenName"].ToString();
					list.Add(busSpecimen);
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
