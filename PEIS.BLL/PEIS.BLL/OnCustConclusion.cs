using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustConclusion
	{
		private readonly IOnCustConclusion dal = DataAccess.CreateOnCustConclusion();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_CustConclusion)
		{
			return this.dal.Exists(ID_CustConclusion);
		}

		public int Add(PEIS.Model.OnCustConclusion model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustConclusion model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_CustConclusion)
		{
			return this.dal.Delete(ID_CustConclusion);
		}

		public bool DeleteList(string ID_CustConclusionlist)
		{
			return this.dal.DeleteList(ID_CustConclusionlist);
		}

		public PEIS.Model.OnCustConclusion GetModel(int ID_CustConclusion)
		{
			return this.dal.GetModel(ID_CustConclusion);
		}

		public PEIS.Model.OnCustConclusion GetModelByCache(int ID_CustConclusion)
		{
			string cacheKey = "OnCustConclusionModel-" + ID_CustConclusion;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_CustConclusion);
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
			return (PEIS.Model.OnCustConclusion)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustConclusion> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustConclusion> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustConclusion> list = new List<PEIS.Model.OnCustConclusion>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustConclusion onCustConclusion = new PEIS.Model.OnCustConclusion();
					if (dt.Rows[i]["ID_CustConclusion"].ToString() != "")
					{
						onCustConclusion.ID_CustConclusion = int.Parse(dt.Rows[i]["ID_CustConclusion"].ToString());
					}
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustConclusion.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					onCustConclusion.ConclusionName = dt.Rows[i]["ConclusionName"].ToString();
					onCustConclusion.ConclusionTypeName = dt.Rows[i]["ConclusionTypeName"].ToString();
					if (dt.Rows[i]["Is_NoEntrySuggestion"].ToString() != "")
					{
						if (dt.Rows[i]["Is_NoEntrySuggestion"].ToString() == "1" || dt.Rows[i]["Is_NoEntrySuggestion"].ToString().ToLower() == "true")
						{
							onCustConclusion.Is_NoEntrySuggestion = new bool?(true);
						}
						else
						{
							onCustConclusion.Is_NoEntrySuggestion = new bool?(false);
						}
					}
					onCustConclusion.Explanation = dt.Rows[i]["Explanation"].ToString();
					onCustConclusion.Suggestion = dt.Rows[i]["Suggestion"].ToString();
					onCustConclusion.DietGuide = dt.Rows[i]["DietGuide"].ToString();
					onCustConclusion.SportGuide = dt.Rows[i]["SportGuide"].ToString();
					onCustConclusion.HealthKnowledge = dt.Rows[i]["HealthKnowledge"].ToString();
					if (dt.Rows[i]["ID_Doctor"].ToString() != "")
					{
						onCustConclusion.ID_Doctor = new int?(int.Parse(dt.Rows[i]["ID_Doctor"].ToString()));
					}
					onCustConclusion.DoctorName = dt.Rows[i]["DoctorName"].ToString();
					if (dt.Rows[i]["ConclusionDate"].ToString() != "")
					{
						onCustConclusion.ConclusionDate = DateTime.Parse(dt.Rows[i]["ConclusionDate"].ToString());
					}
					if (dt.Rows[i]["ID_Conclusion"].ToString() != "")
					{
						onCustConclusion.ID_Conclusion = new int?(int.Parse(dt.Rows[i]["ID_Conclusion"].ToString()));
					}
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						onCustConclusion.DispOrder = new int?(int.Parse(dt.Rows[i]["DispOrder"].ToString()));
					}
					if (dt.Rows[i]["DiagnoseType"].ToString() != "")
					{
						onCustConclusion.DiagnoseType = new int?(int.Parse(dt.Rows[i]["DiagnoseType"].ToString()));
					}
					list.Add(onCustConclusion);
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
