using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustReportManage
	{
		private readonly IOnCustReportManage dal = DataAccess.CreateOnCustReportManage();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ReportManage)
		{
			return this.dal.Exists(ID_ReportManage);
		}

		public int Add(PEIS.Model.OnCustReportManage model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustReportManage model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ReportManage)
		{
			return this.dal.Delete(ID_ReportManage);
		}

		public bool DeleteList(string ID_ReportManagelist)
		{
			return this.dal.DeleteList(ID_ReportManagelist);
		}

		public PEIS.Model.OnCustReportManage GetModel(int ID_ReportManage)
		{
			return this.dal.GetModel(ID_ReportManage);
		}

		public PEIS.Model.OnCustReportManage GetModelByCache(int ID_ReportManage)
		{
			string cacheKey = "OnCustReportManageModel-" + ID_ReportManage;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ReportManage);
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
			return (PEIS.Model.OnCustReportManage)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustReportManage> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustReportManage> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustReportManage> list = new List<PEIS.Model.OnCustReportManage>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustReportManage onCustReportManage = new PEIS.Model.OnCustReportManage();
					if (dt.Rows[i]["ID_ReportManage"].ToString() != "")
					{
						onCustReportManage.ID_ReportManage = int.Parse(dt.Rows[i]["ID_ReportManage"].ToString());
					}
					if (dt.Rows[i]["ID_ReportWay"].ToString() != "")
					{
						onCustReportManage.ID_ReportWay = new int?(int.Parse(dt.Rows[i]["ID_ReportWay"].ToString()));
					}
					if (dt.Rows[i]["ID_ReprotWay"].ToString() != "")
					{
						onCustReportManage.ID_ReprotWay = new int?(int.Parse(dt.Rows[i]["ID_ReprotWay"].ToString()));
					}
					onCustReportManage.ReportWay = dt.Rows[i]["ReportWay"].ToString();
					if (dt.Rows[i]["Is_Informed"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Informed"].ToString() == "1" || dt.Rows[i]["Is_Informed"].ToString().ToLower() == "true")
						{
							onCustReportManage.Is_Informed = new bool?(true);
						}
						else
						{
							onCustReportManage.Is_Informed = new bool?(false);
						}
					}
					onCustReportManage.Informer = dt.Rows[i]["Informer"].ToString();
					if (dt.Rows[i]["InformedDate"].ToString() != "")
					{
						onCustReportManage.InformedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["InformedDate"].ToString()));
					}
					if (dt.Rows[i]["Is_InformReturned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_InformReturned"].ToString() == "1" || dt.Rows[i]["Is_InformReturned"].ToString().ToLower() == "true")
						{
							onCustReportManage.Is_InformReturned = new bool?(true);
						}
						else
						{
							onCustReportManage.Is_InformReturned = new bool?(false);
						}
					}
					if (dt.Rows[i]["Is_ReportReceipted"].ToString() != "")
					{
						if (dt.Rows[i]["Is_ReportReceipted"].ToString() == "1" || dt.Rows[i]["Is_ReportReceipted"].ToString().ToLower() == "true")
						{
							onCustReportManage.Is_ReportReceipted = new bool?(true);
						}
						else
						{
							onCustReportManage.Is_ReportReceipted = new bool?(false);
						}
					}
					if (dt.Rows[i]["Is_SelfReceipted"].ToString() != "")
					{
						if (dt.Rows[i]["Is_SelfReceipted"].ToString() == "1" || dt.Rows[i]["Is_SelfReceipted"].ToString().ToLower() == "true")
						{
							onCustReportManage.Is_SelfReceipted = new bool?(true);
						}
						else
						{
							onCustReportManage.Is_SelfReceipted = new bool?(false);
						}
					}
					onCustReportManage.ReportReceiptor = dt.Rows[i]["ReportReceiptor"].ToString();
					if (dt.Rows[i]["ReportReceiptedDate"].ToString() != "")
					{
						onCustReportManage.ReportReceiptedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ReportReceiptedDate"].ToString()));
					}
					if (dt.Rows[i]["ID_ReportOffer"].ToString() != "")
					{
						onCustReportManage.ID_ReportOffer = new int?(int.Parse(dt.Rows[i]["ID_ReportOffer"].ToString()));
					}
					onCustReportManage.ReportOffer = dt.Rows[i]["ReportOffer"].ToString();
					if (dt.Rows[i]["Is_ReportPrinted"].ToString() != "")
					{
						if (dt.Rows[i]["Is_ReportPrinted"].ToString() == "1" || dt.Rows[i]["Is_ReportPrinted"].ToString().ToLower() == "true")
						{
							onCustReportManage.Is_ReportPrinted = new bool?(true);
						}
						else
						{
							onCustReportManage.Is_ReportPrinted = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_ReportPrinter"].ToString() != "")
					{
						onCustReportManage.ID_ReportPrinter = new int?(int.Parse(dt.Rows[i]["ID_ReportPrinter"].ToString()));
					}
					onCustReportManage.ReportPrinter = dt.Rows[i]["ReportPrinter"].ToString();
					if (dt.Rows[i]["ReportPrintedDate"].ToString() != "")
					{
						onCustReportManage.ReportPrintedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ReportPrintedDate"].ToString()));
					}
					if (dt.Rows[i]["ID_ReportChecker"].ToString() != "")
					{
						onCustReportManage.ID_ReportChecker = new int?(int.Parse(dt.Rows[i]["ID_ReportChecker"].ToString()));
					}
					onCustReportManage.ReportChecker = dt.Rows[i]["ReportChecker"].ToString();
					if (dt.Rows[i]["ReportCheckDate"].ToString() != "")
					{
						onCustReportManage.ReportCheckDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ReportCheckDate"].ToString()));
					}
					onCustReportManage.ReportPosition = dt.Rows[i]["ReportPosition"].ToString();
					list.Add(onCustReportManage);
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
