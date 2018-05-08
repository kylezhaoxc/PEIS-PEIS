using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SysManage : ISysManage
	{
		public int GetMaxId()
		{
			string sQLString = "select max(NodeID)+1 from S_Tree";
			object single = DbHelperSQL.GetSingle(sQLString);
			int result;
			if (single == null)
			{
				result = 1;
			}
			else
			{
				result = int.Parse(single.ToString());
			}
			return result;
		}

		public int AddTreeNode(SysNode node)
		{
			node.NodeID = this.GetMaxId();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into S_Tree(");
			stringBuilder.Append("NodeID,Text,ParentID,Location,OrderID,comment,Url,PermissionID,ImageUrl)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("'" + node.NodeID + "',");
			stringBuilder.Append("'" + node.Text + "',");
			stringBuilder.Append(node.ParentID + ",");
			stringBuilder.Append("'" + node.Location + "',");
			stringBuilder.Append(node.OrderID + ",");
			stringBuilder.Append("'" + node.Comment + "',");
			stringBuilder.Append("'" + node.Url + "',");
			stringBuilder.Append(node.PermissionID + ",");
			stringBuilder.Append("'" + node.ImageUrl + "'");
			stringBuilder.Append(")");
			DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return node.NodeID;
		}

		public void UpdateNode(SysNode node)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update S_Tree set ");
			stringBuilder.Append("Text='" + node.Text + "',");
			stringBuilder.Append("ParentID=" + node.ParentID.ToString() + ",");
			stringBuilder.Append("Location='" + node.Location + "',");
			stringBuilder.Append("OrderID=" + node.OrderID + ",");
			stringBuilder.Append("comment='" + node.Comment + "',");
			stringBuilder.Append("Url='" + node.Url + "',");
			stringBuilder.Append("PermissionID=" + node.PermissionID + ",");
			stringBuilder.Append("ImageUrl='" + node.ImageUrl + "'");
			stringBuilder.Append(" where NodeID=" + node.NodeID);
			DbHelperSQL.ExecuteSql(stringBuilder.ToString());
		}

		public void DelTreeNode(int nodeid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete S_Tree ");
			stringBuilder.Append(" where NodeID=" + nodeid);
			DbHelperSQL.ExecuteSql(stringBuilder.ToString());
		}

		public DataSet GetTreeList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from S_Tree ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by parentid,orderid ");
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public SysNode GetNode(int NodeID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from S_Tree ");
			stringBuilder.Append(" where NodeID=" + NodeID);
			SysNode sysNode = new SysNode();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString());
			SysNode result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				sysNode.NodeID = int.Parse(dataSet.Tables[0].Rows[0]["NodeID"].ToString());
				sysNode.Text = dataSet.Tables[0].Rows[0]["text"].ToString();
				if (dataSet.Tables[0].Rows[0]["ParentID"].ToString() != "")
				{
					sysNode.ParentID = int.Parse(dataSet.Tables[0].Rows[0]["ParentID"].ToString());
				}
				sysNode.Location = dataSet.Tables[0].Rows[0]["Location"].ToString();
				if (dataSet.Tables[0].Rows[0]["OrderID"].ToString() != "")
				{
					sysNode.OrderID = int.Parse(dataSet.Tables[0].Rows[0]["OrderID"].ToString());
				}
				sysNode.Comment = dataSet.Tables[0].Rows[0]["comment"].ToString();
				sysNode.Url = dataSet.Tables[0].Rows[0]["url"].ToString();
				if (dataSet.Tables[0].Rows[0]["PermissionID"].ToString() != "")
				{
					sysNode.PermissionID = int.Parse(dataSet.Tables[0].Rows[0]["PermissionID"].ToString());
				}
				sysNode.ImageUrl = dataSet.Tables[0].Rows[0]["ImageUrl"].ToString();
				result = sysNode;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void AddLog(string time, string loginfo, string Particular)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into S_Log(");
			stringBuilder.Append("datetime,loginfo,Particular)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("'" + time + "',");
			stringBuilder.Append("'" + loginfo + "',");
			stringBuilder.Append("'" + Particular + "'");
			stringBuilder.Append(")");
			DbHelperSQL.ExecuteSql(stringBuilder.ToString());
		}

		public void DeleteLog(int ID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete S_Log ");
			stringBuilder.Append(" where ID= " + ID);
			DbHelperSQL.ExecuteSql(stringBuilder.ToString());
		}

		public void DelOverdueLog(int days)
		{
			string strWhere = " DATEDIFF(day,[datetime],getdate())>" + days;
			this.DeleteLog(strWhere);
		}

		public void DeleteLog(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete S_Log ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			DbHelperSQL.ExecuteSql(stringBuilder.ToString());
		}

		public DataSet GetLogs(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from S_Log ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by ID DESC");
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public DataRow GetLog(string ID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from S_Log ");
			stringBuilder.Append(" where ID= " + ID);
			return DbHelperSQL.Query(stringBuilder.ToString()).Tables[0].Rows[0];
		}
	}
}
