using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SYSRight : ISYSRight
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("RightID", "SYSRight");
		}

		public bool Exists(int RightID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from SYSRight");
			stringBuilder.Append(" where RightID=@RightID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@RightID", SqlDbType.Int, 4)
			};
			array[0].Value = RightID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.SYSRight model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into SYSRight(");
			stringBuilder.Append("RightID,RightName,RightCode,DispOrder,Is_Locked,ParentID,Remark,RightLevel,OperatorID,CreateDate)");
			stringBuilder.Append(" values (");
            stringBuilder.Append("@RightID,@RightName,@RightCode,@DispOrder,@Is_Locked,@ParentID,@Remark,@RightLevel,@OperatorID,@CreateDate)");
            stringBuilder.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RightID", SqlDbType.Int,4),
                    new SqlParameter("@RightName", SqlDbType.VarChar,50),
                    new SqlParameter("@RightCode", SqlDbType.VarChar,80),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@Is_Locked", SqlDbType.Int,4),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.VarChar,200),
                    new SqlParameter("@RightLevel", SqlDbType.Int,4),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.RightID;
            parameters[1].Value = model.RightName;
            parameters[2].Value = model.RightCode;
            parameters[3].Value = model.DispOrder;
            parameters[4].Value = model.Is_Locked;
            parameters[5].Value = model.ParentID;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.RightLevel;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.CreateDate;
            object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), parameters);
			int result;
			if (single == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(single);
			}
			return result;
		}

		public bool Update(PEIS.Model.SYSRight model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SYSRight set ");
            strSql.Append("RightName=@RightName,");
            strSql.Append("RightCode=@RightCode,");
            strSql.Append("DispOrder=@DispOrder,");
            strSql.Append("Is_Locked=@Is_Locked,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("RightLevel=@RightLevel,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where RightID=@RightID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RightName", SqlDbType.VarChar,50),
                    new SqlParameter("@RightCode", SqlDbType.VarChar,80),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@Is_Locked", SqlDbType.Int,4),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.VarChar,200),
                    new SqlParameter("@RightLevel", SqlDbType.Int,4),
                    new SqlParameter("@OperatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@RightID", SqlDbType.Int,4)};
            parameters[0].Value = model.RightName;
            parameters[1].Value = model.RightCode;
            parameters[2].Value = model.DispOrder;
            parameters[3].Value = model.Is_Locked;
            parameters[4].Value = model.ParentID;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.RightLevel;
            parameters[7].Value = model.OperatorID;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.RightID;

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

		public bool Delete(int RightID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSRight ");
            strSql.Append(" where RightID=@RightID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RightID", SqlDbType.Int,4)           };
            parameters[0].Value = RightID;

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

		public bool DeleteList(string RightIDlist)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSRight ");
            strSql.Append(" where RightID in (" + RightIDlist + ")  ");
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

		public PEIS.Model.SYSRight GetModel(int RightID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RightID,RightName,RightCode,DispOrder,Is_Locked,ParentID,Remark,RightLevel,OperatorID,CreateDate from SYSRight ");
            strSql.Append(" where RightID=@RightID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RightID", SqlDbType.Int,4)           };
            parameters[0].Value = RightID;

            PEIS.Model.SYSRight model = new PEIS.Model.SYSRight();
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
            strSql.Append("select RightID,RightName,RightCode,DispOrder,Is_Locked,ParentID,Remark,RightLevel,OperatorID,CreateDate ");
            strSql.Append(" FROM SYSRight ");
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
            strSql.Append(" RightID,RightName,RightCode,DispOrder,Is_Locked,ParentID,Remark,RightLevel,OperatorID,CreateDate ");
            strSql.Append(" FROM SYSRight ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public PEIS.Model.SYSRight DataRowToModel(DataRow row)
        {
            PEIS.Model.SYSRight model = new PEIS.Model.SYSRight();
            if (row != null)
            {
                if (row["RightID"] != null && row["RightID"].ToString() != "")
                {
                    model.RightID = int.Parse(row["RightID"].ToString());
                }
                if (row["RightName"] != null)
                {
                    model.RightName = row["RightName"].ToString();
                }
                if (row["RightCode"] != null)
                {
                    model.RightCode = row["RightCode"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["Is_Locked"] != null && row["Is_Locked"].ToString() != "")
                {
                    model.Is_Locked = int.Parse(row["Is_Locked"].ToString());
                }
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["RightLevel"] != null && row["RightLevel"].ToString() != "")
                {
                    model.RightLevel = int.Parse(row["RightLevel"].ToString());
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
