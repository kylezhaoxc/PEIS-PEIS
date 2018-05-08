using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SysManage1 : ISysManage
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("NodeID", "S_Tree");
		}

		public int AddTreeNode(SysNode model)
		{
			model.NodeID = this.GetMaxId();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into S_Tree(");
			stringBuilder.Append("NodeID,Text,ParentID,Location,OrderID,comment,Url,PermissionID,ImageUrl)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@NodeID,@Text,@ParentID,@Location,@OrderID,@comment,@Url,@PermissionID,@ImageUrl)");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NodeID", SqlDbType.Int, 4),
				new SqlParameter("@Text", SqlDbType.VarChar, 100),
				new SqlParameter("@ParentID", SqlDbType.Int, 4),
				new SqlParameter("@Location", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderID", SqlDbType.Int, 4),
				new SqlParameter("@comment", SqlDbType.VarChar, 50),
				new SqlParameter("@Url", SqlDbType.VarChar, 100),
				new SqlParameter("@PermissionID", SqlDbType.Int, 4),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 100)
			};
			array[0].Value = model.NodeID;
			array[1].Value = model.Text;
			array[2].Value = model.ParentID;
			array[3].Value = model.Location;
			array[4].Value = model.OrderID;
			array[5].Value = model.Comment;
			array[6].Value = model.Url;
			array[7].Value = model.PermissionID;
			array[8].Value = model.ImageUrl;
			DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return model.NodeID;
		}

		public void UpdateNode(SysNode model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update S_Tree set ");
			stringBuilder.Append("Text=@Text,");
			stringBuilder.Append("ParentID=@ParentID,");
			stringBuilder.Append("Location=@Location,");
			stringBuilder.Append("OrderID=@OrderID,");
			stringBuilder.Append("comment=@comment,");
			stringBuilder.Append("Url=@Url,");
			stringBuilder.Append("PermissionID=@PermissionID,");
			stringBuilder.Append("ImageUrl=@ImageUrl");
			stringBuilder.Append(" where NodeID=@NodeID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NodeID", SqlDbType.Int, 4),
				new SqlParameter("@Text", SqlDbType.VarChar, 100),
				new SqlParameter("@ParentID", SqlDbType.Int, 4),
				new SqlParameter("@Location", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderID", SqlDbType.Int, 4),
				new SqlParameter("@comment", SqlDbType.VarChar, 50),
				new SqlParameter("@Url", SqlDbType.VarChar, 100),
				new SqlParameter("@PermissionID", SqlDbType.Int, 4),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 100)
			};
			array[0].Value = model.NodeID;
			array[1].Value = model.Text;
			array[2].Value = model.ParentID;
			array[3].Value = model.Location;
			array[4].Value = model.OrderID;
			array[5].Value = model.Comment;
			array[6].Value = model.Url;
			array[7].Value = model.PermissionID;
			array[8].Value = model.ImageUrl;
			DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
		}

		public void DelTreeNode(int NodeID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete S_Tree ");
			stringBuilder.Append(" where NodeID=@NodeID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NodeID", SqlDbType.Int, 4)
			};
			array[0].Value = NodeID;
			DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
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
			stringBuilder.Append(" where NodeID=@NodeID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@NodeID", SqlDbType.Int, 4)
			};
			array[0].Value = NodeID;
			SysNode sysNode = new SysNode();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
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
