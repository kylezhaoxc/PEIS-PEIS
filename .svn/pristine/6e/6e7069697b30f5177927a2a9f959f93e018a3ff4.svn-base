using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustExamItemResult
	{
		private readonly IOnCustExamItemResult dal = DataAccess.CreateOnCustExamItemResult();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ExamItemResult)
		{
			return this.dal.Exists(ID_ExamItemResult);
		}

		public int Add(PEIS.Model.OnCustExamItemResult model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustExamItemResult model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ExamItemResult)
		{
			return this.dal.Delete(ID_ExamItemResult);
		}

		public bool DeleteList(string ID_ExamItemResultlist)
		{
			return this.dal.DeleteList(ID_ExamItemResultlist);
		}

		public PEIS.Model.OnCustExamItemResult GetModel(int ID_ExamItemResult)
		{
			return this.dal.GetModel(ID_ExamItemResult);
		}

		public PEIS.Model.OnCustExamItemResult GetModelByCache(int ID_ExamItemResult)
		{
			string cacheKey = "OnCustExamItemResultModel-" + ID_ExamItemResult;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ExamItemResult);
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
			return (PEIS.Model.OnCustExamItemResult)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustExamItemResult> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustExamItemResult> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustExamItemResult> list = new List<PEIS.Model.OnCustExamItemResult>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustExamItemResult onCustExamItemResult = new PEIS.Model.OnCustExamItemResult();
					if (dt.Rows[i]["ID_ExamItemResult"].ToString() != "")
					{
						onCustExamItemResult.ID_ExamItemResult = int.Parse(dt.Rows[i]["ID_ExamItemResult"].ToString());
					}
					if (dt.Rows[i]["ID_CustExamItem"].ToString() != "")
					{
						onCustExamItemResult.ID_CustExamItem = new int?(int.Parse(dt.Rows[i]["ID_CustExamItem"].ToString()));
					}
					if (dt.Rows[i]["ID_Symptom"].ToString() != "")
					{
						onCustExamItemResult.ID_Symptom = new int?(int.Parse(dt.Rows[i]["ID_Symptom"].ToString()));
					}
					list.Add(onCustExamItemResult);
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
