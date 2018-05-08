using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictExamType
    {
		private static readonly DictExamType _instance = new DictExamType();

		private readonly IDictExamType dal = DataAccess.CreateDictExamType();

		public static DictExamType Instance
		{
			get
			{
				return DictExamType._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ExamType)
		{
			return this.dal.Exists(ID_ExamType);
		}

		public int Add(PEIS.Model.DictExamType model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictExamType model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ExamTypeID)
		{
			return this.dal.Delete(ExamTypeID);
		}

		public bool DeleteList(string ExamTypeIDlist)
		{
			return this.dal.DeleteList(ExamTypeIDlist);
		}

		public PEIS.Model.DictExamType GetModel(int ExamTypeID)
		{
			return this.dal.GetModel(ExamTypeID);
		}

		public PEIS.Model.DictExamType GetModelByCache(int ExamTypeID)
		{
			string cacheKey = "ExamTypeModel-" + ExamTypeID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ExamTypeID);
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
			return (PEIS.Model.DictExamType)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictExamType> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictExamType> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictExamType> list = new List<PEIS.Model.DictExamType>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictExamType examType = new PEIS.Model.DictExamType();
					if (dt.Rows[i]["ExamTypeID"].ToString() != "")
					{
                        examType.ExamTypeID = int.Parse(dt.Rows[i]["ExamTypeID"].ToString());
					}
                    examType.ExamTypeName = dt.Rows[i]["ExamTypeName"].ToString();
                    examType.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(examType);
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
