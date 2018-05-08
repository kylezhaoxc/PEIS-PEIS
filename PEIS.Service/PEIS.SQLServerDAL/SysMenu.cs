using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SysMenu : ISysMenu
    {
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("MenuID", "SysMenu");
        }

		public bool Exists(int MenuID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SysMenu");
            strSql.Append(" where MenuID=@MenuID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuID", SqlDbType.Int,4)            };
            parameters[0].Value = MenuID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		public int Add(PEIS.Model.SysMenu model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SysMenu(");
            strSql.Append("MenuID,MenuName,ParentID,Url,DispOrder,MenuLevel,RightCode,Is_CombineWithSection)");
            strSql.Append(" values (");
            strSql.Append("@MenuID,@MenuName,@ParentID,@Url,@DispOrder,@MenuLevel,@RightCode,@Is_CombineWithSection)");
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuID", SqlDbType.Int,4),
                    new SqlParameter("@MenuName", SqlDbType.VarChar,60),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),
                    new SqlParameter("@Url", SqlDbType.VarChar,200),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@MenuLevel", SqlDbType.Int,4),
                    new SqlParameter("@RightCode", SqlDbType.VarChar,80),
                    new SqlParameter("@Is_CombineWithSection", SqlDbType.Bit,1)};
            parameters[0].Value = model.MenuID;
            parameters[1].Value = model.MenuName;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.DispOrder;
            parameters[5].Value = model.MenuLevel;
            parameters[6].Value = model.RightCode;
            parameters[7].Value = model.Is_CombineWithSection;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

		public bool Update(PEIS.Model.SysMenu model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysMenu set ");
            strSql.Append("MenuName=@MenuName,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Url=@Url,");
            strSql.Append("DispOrder=@DispOrder,");
            strSql.Append("MenuLevel=@MenuLevel,");
            strSql.Append("RightCode=@RightCode,");
            strSql.Append("Is_CombineWithSection=@Is_CombineWithSection");
            strSql.Append(" where MenuID=@MenuID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuName", SqlDbType.VarChar,60),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),
                    new SqlParameter("@Url", SqlDbType.VarChar,200),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@MenuLevel", SqlDbType.Int,4),
                    new SqlParameter("@RightCode", SqlDbType.VarChar,80),
                    new SqlParameter("@Is_CombineWithSection", SqlDbType.Bit,1),
                    new SqlParameter("@MenuID", SqlDbType.Int,4)};
            parameters[0].Value = model.MenuName;
            parameters[1].Value = model.ParentID;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.DispOrder;
            parameters[4].Value = model.MenuLevel;
            parameters[5].Value = model.RightCode;
            parameters[6].Value = model.Is_CombineWithSection;
            parameters[7].Value = model.MenuID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool Delete(int MenuID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SysMenu ");
            strSql.Append(" where MenuID=@MenuID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuID", SqlDbType.Int,4)            };
            parameters[0].Value = MenuID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool DeleteList(string MenuIDlist)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SysMenu ");
            strSql.Append(" where MenuID in (" + MenuIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public PEIS.Model.SysMenu GetModel(int MenuID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MenuID,MenuName,ParentID,Url,DispOrder,MenuLevel,RightCode,Is_CombineWithSection from SysMenu ");
            strSql.Append(" where MenuID=@MenuID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuID", SqlDbType.Int,4)            };
            parameters[0].Value = MenuID;

            PEIS.Model.SysMenu model = new PEIS.Model.SysMenu();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

		public DataSet GetList(string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MenuID,MenuName,ParentID,Url,DispOrder,MenuLevel,RightCode,Is_CombineWithSection ");
            strSql.Append(" FROM SysMenu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" MenuID,MenuName,ParentID,Url,DispOrder,MenuLevel,RightCode,Is_CombineWithSection ");
            strSql.Append(" FROM SysMenu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public PEIS.Model.SysMenu DataRowToModel(DataRow row)
        {
            PEIS.Model.SysMenu model = new PEIS.Model.SysMenu();
            if (row != null)
            {
                if (row["MenuID"] != null && row["MenuID"].ToString() != "")
                {
                    model.MenuID = int.Parse(row["MenuID"].ToString());
                }
                if (row["MenuName"] != null)
                {
                    model.MenuName = row["MenuName"].ToString();
                }
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["MenuLevel"] != null && row["MenuLevel"].ToString() != "")
                {
                    model.MenuLevel = int.Parse(row["MenuLevel"].ToString());
                }
                if (row["RightCode"] != null)
                {
                    model.RightCode = row["RightCode"].ToString();
                }
                if (row["Is_CombineWithSection"] != null && row["Is_CombineWithSection"].ToString() != "")
                {
                    if ((row["Is_CombineWithSection"].ToString() == "1") || (row["Is_CombineWithSection"].ToString().ToLower() == "true"))
                    {
                        model.Is_CombineWithSection = true;
                    }
                    else
                    {
                        model.Is_CombineWithSection = false;
                    }
                }
            }
            return model;
        }
    }
}
