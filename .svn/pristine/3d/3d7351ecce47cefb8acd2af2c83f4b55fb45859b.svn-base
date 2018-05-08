using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SysRoleRight : ISysRoleRight
    {
		public void Add(PEIS.Model.SysRoleRight model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SysRoleRight(");
            strSql.Append("RoleRightID,RightID,RoleID,OperatorID,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@RoleRightID,@RightID,@RoleID,@OperatorID,@CreateDate)");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleRightID", SqlDbType.Int,4),
                    new SqlParameter("@RightID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.RoleRightID;
            parameters[1].Value = model.RightID;
            parameters[2].Value = model.RoleID;
            parameters[3].Value = model.OperatorID;
            parameters[4].Value = model.CreateDate;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
               
            }
            else
            {
               
            }
        }

		public bool Update(PEIS.Model.SysRoleRight model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysRoleRight set ");
            strSql.Append("RightID=@RightID,");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where RoleRightID=@RoleRightID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RightID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@RoleRightID", SqlDbType.Int,4)};
            parameters[0].Value = model.RightID;
            parameters[1].Value = model.RoleID;
            parameters[2].Value = model.OperatorID;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.RoleRightID;

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

		public bool Delete(int RoleRightID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SysRoleRight ");
            strSql.Append(" where RoleRightID=@RoleRightID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleRightID", SqlDbType.Int,4)           };
            parameters[0].Value = RoleRightID;

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

		public PEIS.Model.SysRoleRight GetModel(int RoleRightID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RoleRightID,RightID,RoleID,OperatorID,CreateDate from SysRoleRight ");
            strSql.Append(" where RoleRightID=@RoleRightID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleRightID", SqlDbType.Int,4)           };
            parameters[0].Value = RoleRightID;

            PEIS.Model.SysRoleRight model = new PEIS.Model.SysRoleRight();
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
            strSql.Append("select RoleRightID,RightID,RoleID,OperatorID,CreateDate ");
            strSql.Append(" FROM SysRoleRight ");
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
            strSql.Append(" RoleRightID,RightID,RoleID,OperatorID,CreateDate ");
            strSql.Append(" FROM SysRoleRight ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public PEIS.Model.SysRoleRight DataRowToModel(DataRow row)
        {
            PEIS.Model.SysRoleRight model = new PEIS.Model.SysRoleRight();
            if (row != null)
            {
                if (row["RoleRightID"] != null && row["RoleRightID"].ToString() != "")
                {
                    model.RoleRightID = int.Parse(row["RoleRightID"].ToString());
                }
                if (row["RightID"] != null && row["RightID"].ToString() != "")
                {
                    model.RightID = int.Parse(row["RightID"].ToString());
                }
                if (row["RoleID"] != null && row["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(row["RoleID"].ToString());
                }
                if (row["OperatorID"] != null && row["OperatorID"].ToString() != "")
                {
                    model.OperatorID = int.Parse(row["OperatorID"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
            }
            return model;
        }
    }
}
