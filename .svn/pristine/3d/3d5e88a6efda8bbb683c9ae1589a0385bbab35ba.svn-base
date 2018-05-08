using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustFee
	{
		private readonly IOnCustFee dal = DataAccess.CreateOnCustFee();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_CustFee)
		{
			return this.dal.Exists(ID_CustFee);
		}

		public int Add(PEIS.Model.OnCustFee model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustFee model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_CustFee)
		{
			return this.dal.Delete(ID_CustFee);
		}

		public bool DeleteList(string ID_CustFeelist)
		{
			return this.dal.DeleteList(ID_CustFeelist);
		}

		public PEIS.Model.OnCustFee GetModel(int ID_CustFee)
		{
			return this.dal.GetModel(ID_CustFee);
		}

		public PEIS.Model.OnCustFee GetModelByCache(int ID_CustFee)
		{
			string cacheKey = "OnCustFeeModel-" + ID_CustFee;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_CustFee);
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
			return (PEIS.Model.OnCustFee)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustFee> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustFee> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustFee> list = new List<PEIS.Model.OnCustFee>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustFee onCustFee = new PEIS.Model.OnCustFee();
					if (dt.Rows[i]["ID_CustFee"].ToString() != "")
					{
						onCustFee.ID_CustFee = int.Parse(dt.Rows[i]["ID_CustFee"].ToString());
					}
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustFee.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					onCustFee.FeeItemName = dt.Rows[i]["FeeItemName"].ToString();
					if (dt.Rows[i]["ID_Register"].ToString() != "")
					{
						onCustFee.ID_Register = new int?(int.Parse(dt.Rows[i]["ID_Register"].ToString()));
					}
					onCustFee.RegisterName = dt.Rows[i]["RegisterName"].ToString();
					if (dt.Rows[i]["RegistDate"].ToString() != "")
					{
						onCustFee.RegistDate = new DateTime?(DateTime.Parse(dt.Rows[i]["RegistDate"].ToString()));
					}
					if (dt.Rows[i]["OriginalPrice"].ToString() != "")
					{
						onCustFee.OriginalPrice = decimal.Parse(dt.Rows[i]["OriginalPrice"].ToString());
					}
					if (dt.Rows[i]["Discount"].ToString() != "")
					{
						onCustFee.Discount = int.Parse(dt.Rows[i]["Discount"].ToString());
					}
					if (dt.Rows[i]["FactPrice"].ToString() != "")
					{
						onCustFee.FactPrice = decimal.Parse(dt.Rows[i]["FactPrice"].ToString());
					}
					if (dt.Rows[i]["ID_Discounter"].ToString() != "")
					{
						onCustFee.ID_Discounter = new int?(int.Parse(dt.Rows[i]["ID_Discounter"].ToString()));
					}
					onCustFee.DiscounterName = dt.Rows[i]["DiscounterName"].ToString();
					if (dt.Rows[i]["Is_FeeCharged"].ToString() != "")
					{
						if (dt.Rows[i]["Is_FeeCharged"].ToString() == "1" || dt.Rows[i]["Is_FeeCharged"].ToString().ToLower() == "true")
						{
							onCustFee.Is_FeeCharged = true;
						}
						else
						{
							onCustFee.Is_FeeCharged = false;
						}
					}
					if (dt.Rows[i]["ID_FeeCharger"].ToString() != "")
					{
						onCustFee.ID_FeeCharger = new int?(int.Parse(dt.Rows[i]["ID_FeeCharger"].ToString()));
					}
					onCustFee.FeeCharger = dt.Rows[i]["FeeCharger"].ToString();
					if (dt.Rows[i]["FeeChargeTime"].ToString() != "")
					{
						onCustFee.FeeChargeTime = DateTime.Parse(dt.Rows[i]["FeeChargeTime"].ToString());
					}
					if (dt.Rows[i]["Is_FeeRefund"].ToString() != "")
					{
						if (dt.Rows[i]["Is_FeeRefund"].ToString() == "1" || dt.Rows[i]["Is_FeeRefund"].ToString().ToLower() == "true")
						{
							onCustFee.Is_FeeRefund = new bool?(true);
						}
						else
						{
							onCustFee.Is_FeeRefund = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_FeeRefunder"].ToString() != "")
					{
						onCustFee.ID_FeeRefunder = new int?(int.Parse(dt.Rows[i]["ID_FeeRefunder"].ToString()));
					}
					onCustFee.FeeRefunder = dt.Rows[i]["FeeRefunder"].ToString();
					if (dt.Rows[i]["Is_Examined"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Examined"].ToString() == "1" || dt.Rows[i]["Is_Examined"].ToString().ToLower() == "true")
						{
							onCustFee.Is_Examined = new bool?(true);
						}
						else
						{
							onCustFee.Is_Examined = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_ExamDoctor"].ToString() != "")
					{
						onCustFee.ID_ExamDoctor = new int?(int.Parse(dt.Rows[i]["ID_ExamDoctor"].ToString()));
					}
					onCustFee.ExamDoctorName = dt.Rows[i]["ExamDoctorName"].ToString();
					if (dt.Rows[i]["ExamDate"].ToString() != "")
					{
						onCustFee.ExamDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ExamDate"].ToString()));
					}
					if (dt.Rows[i]["Is_Discard"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Discard"].ToString() == "1" || dt.Rows[i]["Is_Discard"].ToString().ToLower() == "true")
						{
							onCustFee.Is_Discard = new bool?(true);
						}
						else
						{
							onCustFee.Is_Discard = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_FeeType"].ToString() != "")
					{
						onCustFee.ID_FeeType = new int?(int.Parse(dt.Rows[i]["ID_FeeType"].ToString()));
					}
					if (dt.Rows[i]["ID_Fee"].ToString() != "")
					{
						onCustFee.ID_Fee = new int?(int.Parse(dt.Rows[i]["ID_Fee"].ToString()));
					}
					list.Add(onCustFee);
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
