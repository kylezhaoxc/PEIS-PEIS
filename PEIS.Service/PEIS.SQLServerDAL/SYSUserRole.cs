using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SYSUserRole : ISYSUserRole
	{
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("UserRoleID", "SYSUserRole");
        }

		public bool Exists(int UserRoleID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SYSUserRole");
            strSql.Append(" where UserRoleID=@UserRoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserRoleID", SqlDbType.Int,4)            };
            parameters[0].Value = UserRoleID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		public int Add(PEIS.Model.SYSUserRole model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SYSUserRole(");
            strSql.Append("UserRoleID,RoleID,UserID,CreateDate,OperatorID)");
            strSql.Append(" values (");
            strSql.Append("@UserRoleID,@RoleID,@UserID,@CreateDate,@OperatorID)");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserRoleID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserRoleID;
            parameters[1].Value = model.RoleID;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.OperatorID;

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

		public bool Update(PEIS.Model.SYSUserRole model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SYSUserRole set ");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("OperatorID=@OperatorID");
            strSql.Append(" where UserRoleID=@UserRoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@UserRoleID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.OperatorID;
            parameters[4].Value = model.UserRoleID;

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

		public bool Delete(int UserRoleID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSUserRole ");
            strSql.Append(" where UserRoleID=@UserRoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserRoleID", SqlDbType.Int,4)            };
            parameters[0].Value = UserRoleID;

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

		public bool DeleteList(string UserRoleIDlist)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSUserRole ");
            strSql.Append(" where UserRoleID in (" + UserRoleIDlist + ")  ");
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

		public PEIS.Model.SYSUserRole GetModel(int UserRoleID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserRoleID,RoleID,UserID,CreateDate,OperatorID from SYSUserRole ");
            strSql.Append(" where UserRoleID=@UserRoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserRoleID", SqlDbType.Int,4)            };
            parameters[0].Value = UserRoleID;

            PEIS.Model.SYSUserRole model = new PEIS.Model.SYSUserRole();
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
            strSql.Append("select UserRoleID,RoleID,UserID,CreateDate,OperatorID ");
            strSql.Append(" FROM SYSUserRole ");
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
            strSql.Append(" UserRoleID,RoleID,UserID,CreateDate,OperatorID ");
            strSql.Append(" FROM SYSUserRole ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public PEIS.Model.SYSUserRole DataRowToModel(DataRow row)
        {
            PEIS.Model.SYSUserRole model = new PEIS.Model.SYSUserRole();
            if (row != null)
            {
                if (row["UserRoleID"] != null && row["UserRoleID"].ToString() != "")
                {
                    model.UserRoleID = int.Parse(row["UserRoleID"].ToString());
                }
                if (row["RoleID"] != null && row["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(row["RoleID"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["OperatorID"] != null && row["OperatorID"].ToString() != "")
                {
                    model.OperatorID = int.Parse(row["OperatorID"].ToString());
                }
            }
            return model;
        }
    }
}
