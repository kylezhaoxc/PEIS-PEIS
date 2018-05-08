using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictExamPlace
	{
		private static readonly DictExamPlace _instance = new DictExamPlace();

		private readonly IDictExamPlace dal = DataAccess.CreateDictExamPlace();

		public static DictExamPlace Instance
		{
			get
			{
				return DictExamPlace._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ExamPlaceID)
		{
			return this.dal.Exists(ExamPlaceID);
		}

		public int Add(PEIS.Model.DictExamPlace model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictExamPlace model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ExamPlaceID)
		{
			return this.dal.Delete(ExamPlaceID);
		}

		public bool DeleteList(string ExamPlaceIDlist)
		{
			return this.dal.DeleteList(ExamPlaceIDlist);
		}

		public PEIS.Model.DictExamPlace GetModel(int ExamPlaceID)
		{
			return this.dal.GetModel(ExamPlaceID);
		}

		public PEIS.Model.DictExamPlace GetModelByCache(int ExamPlaceID)
		{
			string cacheKey = "DictExamPlaceModel-" + ExamPlaceID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ExamPlaceID);
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
			return (PEIS.Model.DictExamPlace)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictExamPlace> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictExamPlace> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictExamPlace> list = new List<PEIS.Model.DictExamPlace>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictExamPlace busExamPlace = new PEIS.Model.DictExamPlace();
					if (dt.Rows[i]["ExamPlaceID"].ToString() != "")
					{
						busExamPlace.ExamPlaceID = int.Parse(dt.Rows[i]["ExamPlaceID"].ToString());
					}
					busExamPlace.ExamPlaceName = dt.Rows[i]["ExamPlaceName"].ToString();
					if (dt.Rows[i]["Is_Default"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Default"].ToString() == "1" || dt.Rows[i]["Is_Default"].ToString().ToLower() == "true")
						{
							busExamPlace.Default = true;
						}
						else
						{
							busExamPlace.Default = false;
						}
					}
					busExamPlace.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(busExamPlace);
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
