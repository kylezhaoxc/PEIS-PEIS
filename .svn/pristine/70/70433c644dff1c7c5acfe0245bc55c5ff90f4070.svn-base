using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusExamItemGroup
	{
		private readonly IBusExamItemGroup dal = DataAccess.CreateBusExamItemGroup();

		private static readonly BusExamItemGroup _instance = new BusExamItemGroup();

		public static BusExamItemGroup Instance
		{
			get
			{
				return BusExamItemGroup._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ExamItemGroup)
		{
			return this.dal.Exists(ID_ExamItemGroup);
		}

		public int Add(PEIS.Model.BusExamItemGroup model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusExamItemGroup model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ExamItemGroup)
		{
			return this.dal.Delete(ID_ExamItemGroup);
		}

		public bool DeleteList(string ID_ExamItemGrouplist)
		{
			return this.dal.DeleteList(ID_ExamItemGrouplist);
		}

		public PEIS.Model.BusExamItemGroup GetModel(int ID_ExamItemGroup)
		{
			return this.dal.GetModel(ID_ExamItemGroup);
		}

		public PEIS.Model.BusExamItemGroup GetModelByCache(int ID_ExamItemGroup)
		{
			string cacheKey = "BusExamItemGroupModel-" + ID_ExamItemGroup;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ExamItemGroup);
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
			return (PEIS.Model.BusExamItemGroup)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusExamItemGroup> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusExamItemGroup> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusExamItemGroup> list = new List<PEIS.Model.BusExamItemGroup>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusExamItemGroup busExamItemGroup = new PEIS.Model.BusExamItemGroup();
					if (dt.Rows[i]["ID_ExamItemGroup"].ToString() != "")
					{
						busExamItemGroup.ID_ExamItemGroup = int.Parse(dt.Rows[i]["ID_ExamItemGroup"].ToString());
					}
					busExamItemGroup.ExamItemGroupName = dt.Rows[i]["ExamItemGroupName"].ToString();
					busExamItemGroup.InputCode = dt.Rows[i]["InputCode"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						busExamItemGroup.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString());
					}
					busExamItemGroup.Note = dt.Rows[i]["Note"].ToString();
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							busExamItemGroup.Is_Banned = new bool?(true);
						}
						else
						{
							busExamItemGroup.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Operator"].ToString() != "")
					{
						busExamItemGroup.ID_Operator = new int?(int.Parse(dt.Rows[i]["ID_Operator"].ToString()));
					}
					busExamItemGroup.Operator = dt.Rows[i]["Operator"].ToString();
					if (dt.Rows[i]["OperateDate"].ToString() != "")
					{
						busExamItemGroup.OperateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["OperateDate"].ToString()));
					}
					if (dt.Rows[i]["OperateType"].ToString() != "")
					{
						busExamItemGroup.OperateType = new int?(int.Parse(dt.Rows[i]["OperateType"].ToString()));
					}
					busExamItemGroup.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					list.Add(busExamItemGroup);
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
