using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusExamItem
	{
		private static readonly BusExamItem _instance = new BusExamItem();

		private readonly IBusExamItem dal = DataAccess.CreateBusExamItem();

		public static BusExamItem Instance
		{
			get
			{
				return BusExamItem._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ExamItem)
		{
			return this.dal.Exists(ID_ExamItem);
		}

		public int Add(PEIS.Model.BusExamItem model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusExamItem model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ExamItem)
		{
			return this.dal.Delete(ID_ExamItem);
		}

		public bool DeleteList(string ID_ExamItemlist)
		{
			return this.dal.DeleteList(ID_ExamItemlist);
		}

		public PEIS.Model.BusExamItem GetModel(int ID_ExamItem)
		{
			return this.dal.GetModel(ID_ExamItem);
		}

		public PEIS.Model.BusExamItem GetModelByCache(int ID_ExamItem)
		{
			string cacheKey = "BusExamItemModel-" + ID_ExamItem;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ExamItem);
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
			return (PEIS.Model.BusExamItem)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusExamItem> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusExamItem> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusExamItem> list = new List<PEIS.Model.BusExamItem>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusExamItem busExamItem = new PEIS.Model.BusExamItem();
					if (dt.Rows[i]["ID_ExamItem"].ToString() != "")
					{
						busExamItem.ID_ExamItem = int.Parse(dt.Rows[i]["ID_ExamItem"].ToString());
					}
					busExamItem.ExamItemName = dt.Rows[i]["ExamItemName"].ToString();
					busExamItem.GetResultWay = dt.Rows[i]["GetResultWay"].ToString();
					busExamItem.ExamItemCode = dt.Rows[i]["ExamItemCode"].ToString();
					if (dt.Rows[i]["ID_Section"].ToString() != "")
					{
						busExamItem.ID_Section = new int?(int.Parse(dt.Rows[i]["ID_Section"].ToString()));
					}
					if (dt.Rows[i]["Is_LisValueNull"].ToString() != "")
					{
						if (dt.Rows[i]["Is_LisValueNull"].ToString() == "1" || dt.Rows[i]["Is_LisValueNull"].ToString().ToLower() == "true")
						{
							busExamItem.Is_LisValueNull = new bool?(true);
						}
						else
						{
							busExamItem.Is_LisValueNull = new bool?(false);
						}
					}
					if (dt.Rows[i]["Is_EntrySectSum"].ToString() != "")
					{
						if (dt.Rows[i]["Is_EntrySectSum"].ToString() == "1" || dt.Rows[i]["Is_EntrySectSum"].ToString().ToLower() == "true")
						{
							busExamItem.Is_EntrySectSum = new bool?(true);
						}
						else
						{
							busExamItem.Is_EntrySectSum = new bool?(false);
						}
					}
					if (dt.Rows[i]["EntrySectSumLevel"].ToString() != "")
					{
						busExamItem.EntrySectSumLevel = new int?(int.Parse(dt.Rows[i]["EntrySectSumLevel"].ToString()));
					}
					if (dt.Rows[i]["Is_AutoCalc"].ToString() != "")
					{
						if (dt.Rows[i]["Is_AutoCalc"].ToString() == "1" || dt.Rows[i]["Is_AutoCalc"].ToString().ToLower() == "true")
						{
							busExamItem.Is_AutoCalc = new bool?(true);
						}
						else
						{
							busExamItem.Is_AutoCalc = new bool?(false);
						}
					}
					busExamItem.CalcExpression = dt.Rows[i]["CalcExpression"].ToString();
					if (dt.Rows[i]["SymCols"].ToString() != "")
					{
						busExamItem.SymCols = new int?(int.Parse(dt.Rows[i]["SymCols"].ToString()));
					}
					if (dt.Rows[i]["TextboxRows"].ToString() != "")
					{
						busExamItem.TextboxRows = new int?(int.Parse(dt.Rows[i]["TextboxRows"].ToString()));
					}
					if (dt.Rows[i]["Is_SameRow"].ToString() != "")
					{
						if (dt.Rows[i]["Is_SameRow"].ToString() == "1" || dt.Rows[i]["Is_SameRow"].ToString().ToLower() == "true")
						{
							busExamItem.Is_SameRow = new bool?(true);
						}
						else
						{
							busExamItem.Is_SameRow = new bool?(false);
						}
					}
					busExamItem.ExamItemUnit = dt.Rows[i]["ExamItemUnit"].ToString();
					if (dt.Rows[i]["MaleHiLimit"].ToString() != "")
					{
						busExamItem.MaleHiLimit = new decimal?(decimal.Parse(dt.Rows[i]["MaleHiLimit"].ToString()));
					}
					if (dt.Rows[i]["MaleLoLimit"].ToString() != "")
					{
						busExamItem.MaleLoLimit = new decimal?(decimal.Parse(dt.Rows[i]["MaleLoLimit"].ToString()));
					}
					if (dt.Rows[i]["FemaleHiLimit"].ToString() != "")
					{
						busExamItem.FemaleHiLimit = new decimal?(decimal.Parse(dt.Rows[i]["FemaleHiLimit"].ToString()));
					}
					if (dt.Rows[i]["FemaleLoLimit"].ToString() != "")
					{
						busExamItem.FemaleLoLimit = new decimal?(decimal.Parse(dt.Rows[i]["FemaleLoLimit"].ToString()));
					}
					if (dt.Rows[i]["Is_SymMultiValue"].ToString() != "")
					{
						if (dt.Rows[i]["Is_SymMultiValue"].ToString() == "1" || dt.Rows[i]["Is_SymMultiValue"].ToString().ToLower() == "true")
						{
							busExamItem.Is_SymMultiValue = new bool?(true);
						}
						else
						{
							busExamItem.Is_SymMultiValue = new bool?(false);
						}
					}
					busExamItem.InputCode = dt.Rows[i]["InputCode"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						busExamItem.DispOrder = new int?(int.Parse(dt.Rows[i]["DispOrder"].ToString()));
					}
					busExamItem.Note = dt.Rows[i]["Note"].ToString();
					if (dt.Rows[i]["Forsex"].ToString() != "")
					{
						busExamItem.Forsex = new int?(int.Parse(dt.Rows[i]["Forsex"].ToString()));
					}
					if (dt.Rows[i]["Is_ExamItemNonPrintInReport"].ToString() != "")
					{
						if (dt.Rows[i]["Is_ExamItemNonPrintInReport"].ToString() == "1" || dt.Rows[i]["Is_ExamItemNonPrintInReport"].ToString().ToLower() == "true")
						{
							busExamItem.Is_ExamItemNonPrintInReport = new bool?(true);
						}
						else
						{
							busExamItem.Is_ExamItemNonPrintInReport = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_ExamItemGroup"].ToString() != "")
					{
						busExamItem.ID_ExamItemGroup = new int?(int.Parse(dt.Rows[i]["ID_ExamItemGroup"].ToString()));
					}
					busExamItem.AbbrExamName = dt.Rows[i]["AbbrExamName"].ToString();
					list.Add(busExamItem);
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
