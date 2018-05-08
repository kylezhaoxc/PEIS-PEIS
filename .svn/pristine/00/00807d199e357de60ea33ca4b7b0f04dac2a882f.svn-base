using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class SYSOpUser
    {
		private readonly ISYSOpUser dal = DataAccess.CreateSYSOpUser();

		private static readonly SYSOpUser _instance = new SYSOpUser();



        public static SYSOpUser Instance
		{
			get
			{
				return SYSOpUser._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_User)
		{
			return this.dal.Exists(ID_User);
		}

		public int Add(PEIS.Model.SYSOpUser model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.SYSOpUser model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_User)
		{
			return this.dal.Delete(ID_User);
		}

		public bool DeleteList(string ID_Userlist)
		{
			return this.dal.DeleteList(ID_Userlist);
		}

		public PEIS.Model.SYSOpUser GetModel(int ID_User)
		{
			return this.dal.GetModel(ID_User);
		}

		public PEIS.Model.SYSOpUser GetModelByCache(int ID_User)
		{
			string cacheKey = "SYSOpUser-" + ID_User;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_User);
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch (System.Exception ex)
				{
					throw ex;
				}
			}
			return (PEIS.Model.SYSOpUser)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.SYSOpUser> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.SYSOpUser> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.SYSOpUser> list = new List<PEIS.Model.SYSOpUser>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.SYSOpUser sysopUser = new PEIS.Model.SYSOpUser();
					if (dt.Rows[i]["UserID"].ToString() != "")
					{
                        sysopUser.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["SectionID"].ToString() != "")
					{
                        sysopUser.SectionID = new int?(int.Parse(dt.Rows[i]["SectionID"].ToString()));
					}
                    sysopUser.UserName = dt.Rows[i]["UserName"].ToString();
                    sysopUser.LoginName = dt.Rows[i]["LoginName"].ToString();
                    sysopUser.PassWord = dt.Rows[i]["PassWord"].ToString();                   
					if (dt.Rows[i]["CreateTime"].ToString() != "")
					{
                        sysopUser.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
					}
					if (dt.Rows[i]["LastLoginTime"].ToString() != "")
					{
                        sysopUser.LastLoginTime = new DateTime?(DateTime.Parse(dt.Rows[i]["LastLoginTime"].ToString()));
					}
					if (dt.Rows[i]["TotalNum"].ToString() != "")
					{
                        sysopUser.TotalNum = new int?(int.Parse(dt.Rows[i]["TotalNum"].ToString()));
					}
					if (dt.Rows[i]["FailCount"].ToString() != "")
					{
                        sysopUser.FailCount = new int?(int.Parse(dt.Rows[i]["FailCount"].ToString()));
					}
					if (dt.Rows[i]["OperateLevel"].ToString() != "")
					{
                        sysopUser.OperateLevel = new int?(int.Parse(dt.Rows[i]["OperateLevel"].ToString()));
					}
                    sysopUser.Note = dt.Rows[i]["Note"].ToString();
					if (dt.Rows[i]["DisDate"].ToString() != "")
					{
                        sysopUser.DisDate = new DateTime?(DateTime.Parse(dt.Rows[i]["DisDate"].ToString()));
					}
					if (dt.Rows[i]["Signature"].ToString() != "")
					{
                        sysopUser.Signature = (byte[])dt.Rows[i]["Signature"];
					}
					if (dt.Rows[i]["DisCountRate"].ToString() != "")
					{
                        sysopUser.DisCountRate = new int?(int.Parse(dt.Rows[i]["DisCountRate"].ToString()));
					}
					if (dt.Rows[i]["Sex"].ToString() != "")
					{
                        sysopUser.Sex = new int?(int.Parse(dt.Rows[i]["Sex"].ToString()));
					}
					if (dt.Rows[i]["Is_Del"].ToString() != "")
					{
                        sysopUser.Is_Del = new int?(int.Parse(dt.Rows[i]["Is_Del"].ToString()));
					}
					if (dt.Rows[i]["VocationType"].ToString() != "")
					{
                        sysopUser.VocationType = new int?(int.Parse(dt.Rows[i]["VocationType"].ToString()));
					}
					list.Add(sysopUser);
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
