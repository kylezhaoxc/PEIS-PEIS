using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusSymptom
	{
		private static readonly BusSymptom _instance = new BusSymptom();

		private readonly IBusSymptom dal = DataAccess.CreateBusSymptom();

		public static BusSymptom Instance
		{
			get
			{
				return BusSymptom._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_Symptom)
		{
			return this.dal.Exists(ID_Symptom);
		}

		public int Add(PEIS.Model.BusSymptom model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusSymptom model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_Symptom)
		{
			return this.dal.Delete(ID_Symptom);
		}

		public bool DeleteList(string ID_Symptomlist)
		{
			return this.dal.DeleteList(ID_Symptomlist);
		}

		public PEIS.Model.BusSymptom GetModel(int ID_Symptom)
		{
			return this.dal.GetModel(ID_Symptom);
		}

		public PEIS.Model.BusSymptom GetModelByCache(int ID_Symptom)
		{
			string cacheKey = "BusSymptomModel-" + ID_Symptom;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Symptom);
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
			return (PEIS.Model.BusSymptom)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusSymptom> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusSymptom> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusSymptom> list = new List<PEIS.Model.BusSymptom>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusSymptom busSymptom = new PEIS.Model.BusSymptom();
					if (dt.Rows[i]["ID_Symptom"].ToString() != "")
					{
						busSymptom.ID_Symptom = int.Parse(dt.Rows[i]["ID_Symptom"].ToString());
					}
					if (dt.Rows[i]["ID_ExamItem"].ToString() != "")
					{
						busSymptom.ID_ExamItem = new int?(int.Parse(dt.Rows[i]["ID_ExamItem"].ToString()));
					}
					if (dt.Rows[i]["ID_Conclusion"].ToString() != "")
					{
						busSymptom.ID_Conclusion = new int?(int.Parse(dt.Rows[i]["ID_Conclusion"].ToString()));
					}
					busSymptom.SymptomName = dt.Rows[i]["SymptomName"].ToString();
					busSymptom.SymptomDescribe = dt.Rows[i]["SymptomDescribe"].ToString();
					if (dt.Rows[i]["DiseaseLevel"].ToString() != "")
					{
						busSymptom.DiseaseLevel = new int?(int.Parse(dt.Rows[i]["DiseaseLevel"].ToString()));
					}
					if (dt.Rows[i]["Is_Default"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Default"].ToString() == "1" || dt.Rows[i]["Is_Default"].ToString().ToLower() == "true")
						{
							busSymptom.Is_Default = new bool?(true);
						}
						else
						{
							busSymptom.Is_Default = new bool?(false);
						}
					}
					busSymptom.NumOperSign = dt.Rows[i]["NumOperSign"].ToString();
					if (dt.Rows[i]["NumMale"].ToString() != "")
					{
						busSymptom.NumMale = new decimal?(decimal.Parse(dt.Rows[i]["NumMale"].ToString()));
					}
					if (dt.Rows[i]["NumFemale"].ToString() != "")
					{
						busSymptom.NumFemale = new decimal?(decimal.Parse(dt.Rows[i]["NumFemale"].ToString()));
					}
					busSymptom.InputCode = dt.Rows[i]["InputCode"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						busSymptom.DispOrder = new int?(int.Parse(dt.Rows[i]["DispOrder"].ToString()));
					}
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							busSymptom.Is_Banned = new bool?(true);
						}
						else
						{
							busSymptom.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_BanOpr"].ToString() != "")
					{
						busSymptom.ID_BanOpr = new int?(int.Parse(dt.Rows[i]["ID_BanOpr"].ToString()));
					}
					busSymptom.BanOperator = dt.Rows[i]["BanOperator"].ToString();
					if (dt.Rows[i]["BanDate"].ToString() != "")
					{
						busSymptom.BanDate = new DateTime?(DateTime.Parse(dt.Rows[i]["BanDate"].ToString()));
					}
					list.Add(busSymptom);
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
