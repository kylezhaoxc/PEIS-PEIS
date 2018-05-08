using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustApply
	{
		private readonly IOnCustApply dal = DataAccess.CreateOnCustApply();

		public bool Exists(string ID_Apply)
		{
			return this.dal.Exists(ID_Apply);
		}

		public void Add(PEIS.Model.OnCustApply model)
		{
			this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustApply model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(string ID_Apply)
		{
			return this.dal.Delete(ID_Apply);
		}

		public bool DeleteList(string ID_Applylist)
		{
			return this.dal.DeleteList(ID_Applylist);
		}

		public PEIS.Model.OnCustApply GetModel(string ID_Apply)
		{
			return this.dal.GetModel(ID_Apply);
		}

		public PEIS.Model.OnCustApply GetModelByCache(string ID_Apply)
		{
			string cacheKey = "OnCustApplyModel-" + ID_Apply;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Apply);
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
			return (PEIS.Model.OnCustApply)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustApply> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustApply> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustApply> list = new List<PEIS.Model.OnCustApply>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustApply onCustApply = new PEIS.Model.OnCustApply();
					onCustApply.ID_Apply = dt.Rows[i]["ID_Apply"].ToString();
					if (dt.Rows[i]["ID_Section"].ToString() != "")
					{
						onCustApply.ID_Section = new int?(int.Parse(dt.Rows[i]["ID_Section"].ToString()));
					}
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustApply.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					onCustApply.ApplyTitle = dt.Rows[i]["ApplyTitle"].ToString();
					onCustApply.SpecimenName = dt.Rows[i]["SpecimenName"].ToString();
					onCustApply.BatchNumber = dt.Rows[i]["BatchNumber"].ToString();
					onCustApply.SectionName = dt.Rows[i]["SectionName"].ToString();
					onCustApply.DeptName = dt.Rows[i]["DeptName"].ToString();
					onCustApply.ExamNumber = dt.Rows[i]["ExamNumber"].ToString();
					if (dt.Rows[i]["AcquisitionTime"].ToString() != "")
					{
						onCustApply.AcquisitionTime = new DateTime?(DateTime.Parse(dt.Rows[i]["AcquisitionTime"].ToString()));
					}
					if (dt.Rows[i]["RecvTime"].ToString() != "")
					{
						onCustApply.RecvTime = new DateTime?(DateTime.Parse(dt.Rows[i]["RecvTime"].ToString()));
					}
					if (dt.Rows[i]["ReportTime"].ToString() != "")
					{
						onCustApply.ReportTime = new DateTime?(DateTime.Parse(dt.Rows[i]["ReportTime"].ToString()));
					}
					onCustApply.ApplyDoctorName = dt.Rows[i]["ApplyDoctorName"].ToString();
					onCustApply.DetectionDoctorName = dt.Rows[i]["DetectionDoctorName"].ToString();
					onCustApply.CheckDoctorName = dt.Rows[i]["CheckDoctorName"].ToString();
					if (dt.Rows[i]["CreateTime"].ToString() != "")
					{
						onCustApply.CreateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateTime"].ToString()));
					}
					onCustApply.ExamItems = dt.Rows[i]["ExamItems"].ToString();
					onCustApply.SendProjectIDs = dt.Rows[i]["SendProjectIDs"].ToString();
					onCustApply.SendGroupParams = dt.Rows[i]["SendGroupParams"].ToString();
					onCustApply.SpecimenNo = dt.Rows[i]["SpecimenNo"].ToString();
					if (dt.Rows[i]["Is_TypistFinish"].ToString() != "")
					{
						if (dt.Rows[i]["Is_TypistFinish"].ToString() == "1" || dt.Rows[i]["Is_TypistFinish"].ToString().ToLower() == "true")
						{
							onCustApply.Is_TypistFinish = new bool?(true);
						}
						else
						{
							onCustApply.Is_TypistFinish = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Typist"].ToString() != "")
					{
						onCustApply.ID_Typist = new int?(int.Parse(dt.Rows[i]["ID_Typist"].ToString()));
					}
					onCustApply.TypistName = dt.Rows[i]["TypistName"].ToString();
					if (dt.Rows[i]["TypistDate"].ToString() != "")
					{
						onCustApply.TypistDate = new DateTime?(DateTime.Parse(dt.Rows[i]["TypistDate"].ToString()));
					}
					if (dt.Rows[i]["ID_DetectionDoctor"].ToString() != "")
					{
						onCustApply.ID_DetectionDoctor = new int?(int.Parse(dt.Rows[i]["ID_DetectionDoctor"].ToString()));
					}
					list.Add(onCustApply);
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
