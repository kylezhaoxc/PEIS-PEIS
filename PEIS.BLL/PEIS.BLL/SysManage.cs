using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;

namespace PEIS.BLL
{
	public class SysManage
	{
		private ISysManage dal = DataAccess.CreateSysManage();

		public int AddTreeNode(SysNode node)
		{
			return this.dal.AddTreeNode(node);
		}

		public void UpdateNode(SysNode node)
		{
			this.dal.UpdateNode(node);
		}

		public void DelTreeNode(int nodeid)
		{
			this.dal.DelTreeNode(nodeid);
		}

		public DataSet GetTreeList(string strWhere)
		{
			return this.dal.GetTreeList(strWhere);
		}

		public SysNode GetNode(int NodeID)
		{
			return this.dal.GetNode(NodeID);
		}

		public void AddLog(string time, string loginfo, string Particular)
		{
			this.dal.AddLog(time, loginfo, Particular);
		}

		public void DelOverdueLog(int days)
		{
			this.dal.DelOverdueLog(days);
		}

		public void DeleteLog(string Idlist)
		{
			string strWhere = "";
			if (Idlist.Trim() != "")
			{
				strWhere = " ID in (" + Idlist + ")";
			}
			this.dal.DeleteLog(strWhere);
		}

		public void DeleteLog(string timestart, string timeend)
		{
			string strWhere = string.Concat(new string[]
			{
				" datetime>'",
				timestart,
				"' and datetime<'",
				timeend,
				"'"
			});
			this.dal.DeleteLog(strWhere);
		}

		public DataSet GetLogs(string strWhere)
		{
			return this.dal.GetLogs(strWhere);
		}

		public DataRow GetLog(string ID)
		{
			return this.dal.GetLog(ID);
		}
	}
}
