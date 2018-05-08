using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SysRole : ISysRole
    {
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("RoleID", "SysRole");
		}

		public bool Exists(int RoleID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from SysRole");
			stringBuilder.Append(" where RoleID=@RoleID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@RoleID", SqlDbType.Int, 4)
			};
			array[0].Value = RoleID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.SysRole model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SysRole(");
            strSql.Append("RoleID,RoleName,DispOrder,Is_Locked,Remark,CreateDate,OperatorID,Is_DefaultRole)");
            strSql.Append(" values (");
            strSql.Append("@RoleID,@RoleName,@DispOrder,@Is_Locked,@Remark,@CreateDate,@OperatorID,@Is_DefaultRole)");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@RoleName", SqlDbType.NVarChar,30),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@Is_Locked", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@Is_DefaultRole", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.DispOrder;
            parameters[3].Value = model.Is_Locked;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.OperatorID;
            parameters[7].Value = model.Is_DefaultRole;

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

		public bool Update(PEIS.Model.SysRole model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysRole set ");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("DispOrder=@DispOrder,");
            strSql.Append("Is_Locked=@Is_Locked,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("Is_DefaultRole=@Is_DefaultRole");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleName", SqlDbType.NVarChar,30),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@Is_Locked", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@Is_DefaultRole", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.DispOrder;
            parameters[2].Value = model.Is_Locked;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreateDate;
            parameters[5].Value = model.OperatorID;
            parameters[6].Value = model.Is_DefaultRole;
            parameters[7].Value = model.RoleID;

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

		public bool Delete(int RoleID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SysRole ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleID", SqlDbType.Int,4)            };
            parameters[0].Value = RoleID;

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

		public bool DeleteList(string RoleIDlist)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SysRole ");
            strSql.Append(" where RoleID in (" + RoleIDlist + ")  ");
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

		public PEIS.Model.SysRole GetModel(int RoleID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RoleID,RoleName,DispOrder,Is_Locked,Remark,CreateDate,OperatorID,Is_DefaultRole from SysRole ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleID", SqlDbType.Int,4)            };
            parameters[0].Value = RoleID;

            PEIS.Model.SysRole model = new PEIS.Model.SysRole();
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ID_Role,RoleName,DispOrder,Is_Locked,Remark,CreateDate,ID_Operator,Is_DefaultRole ");
			stringBuilder.Append(" FROM NatRole ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" RoleID,RoleName,DispOrder,Is_Locked,Remark,CreateDate,OperatorID,Is_DefaultRole ");
            strSql.Append(" FROM SysRole ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public PEIS.Model.SysRole DataRowToModel(DataRow row)
        {
            PEIS.Model.SysRole model = new PEIS.Model.SysRole();
            if (row != null)
            {
                if (row["RoleID"] != null && row["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(row["RoleID"].ToString());
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["Is_Locked"] != null && row["Is_Locked"].ToString() != "")
                {
                    model.Is_Locked = int.Parse(row["Is_Locked"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["OperatorID"] != null && row["OperatorID"].ToString() != "")
                {
                    model.OperatorID = int.Parse(row["OperatorID"].ToString());
                }
                if (row["Is_DefaultRole"] != null && row["Is_DefaultRole"].ToString() != "")
                {
                    model.Is_DefaultRole = int.Parse(row["Is_DefaultRole"].ToString());
                }
            }
            return model;
        }
    }
}
