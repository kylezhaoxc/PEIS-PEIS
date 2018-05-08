using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SYSUserSection : ISYSUserSection
	{
		public void Add(PEIS.Model.SYSUserSection model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SYSUserSection(");
            strSql.Append("UserSectionID,SectionID,UserID,OperatorID,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@ID_UserSection,@SectionID,@UserID,@OperatorID,@CreateDate)");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserSectionID", SqlDbType.Int,4),
                    new SqlParameter("@SectionID", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserSectionID;
            parameters[1].Value = model.SectionID;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.OperatorID;
            parameters[4].Value = model.CreateDate;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
           
        }

		public bool Update(PEIS.Model.SYSUserSection model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SYSUserSection set ");
            strSql.Append("SectionID=@SectionID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where UserSectionID=@UserSectionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SectionID", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@UserSectionID", SqlDbType.Int,4)};
            parameters[0].Value = model.SectionID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.OperatorID;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.UserSectionID;

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

		public bool Delete(int UserSectionID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSUserSection ");
            strSql.Append(" where UserSectionID=@UserSectionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID_UserSection", SqlDbType.Int,4)            };
            parameters[0].Value = UserSectionID;

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

		public PEIS.Model.SYSUserSection GetModel(int UserSectionID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserSectionID,SectionID,UserID,OperatorID,CreateDate from SYSUserSection ");
            strSql.Append(" where UserSectionID=@UserSectionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserSectionID", SqlDbType.Int,4)         };
            parameters[0].Value = UserSectionID;

            PEIS.Model.SYSUserSection model = new PEIS.Model.SYSUserSection();
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
            strSql.Append("select UserSectionID,SectionID,UserID,OperatorID,CreateDate ");
            strSql.Append(" FROM SYSUserSection ");
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
            strSql.Append(" UserSectionID,SectionID,UserID,OperatorID,CreateDate ");
            strSql.Append(" FROM SYSUserSection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public PEIS.Model.SYSUserSection DataRowToModel(DataRow row)
        {
            PEIS.Model.SYSUserSection model = new PEIS.Model.SYSUserSection();
            if (row != null)
            {
                if (row["UserSectionID"] != null && row["UserSectionID"].ToString() != "")
                {
                    model.UserSectionID = int.Parse(row["UserSectionID"].ToString());
                }
                if (row["SectionID"] != null && row["SectionID"].ToString() != "")
                {
                    model.SectionID = int.Parse(row["SectionID"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
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
