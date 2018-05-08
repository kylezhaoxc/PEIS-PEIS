using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SYSUserRight : ISYSUserRight
    {
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("UserRightID", "SYSUserRight");
		}

		public bool Exists(int UserRightID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from SYSUserRight");
			stringBuilder.Append(" where UserRightID=@UserRightID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@UserRightID", SqlDbType.Int, 4)
			};
			array[0].Value = UserRightID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.SYSUserRight model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into SYSUserRight(");
			stringBuilder.Append("RightID,CreateDate,OperatorID,UserID)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@RightID,@CreateDate,@OperatorID,@UserID)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@RightID", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@OperatorID", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			array[0].Value = model.RightID;
			array[1].Value = model.CreateDate;
			array[2].Value = model.OperatorID;
			array[3].Value = model.UserID;
			object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), array);
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

		public bool Update(PEIS.Model.SYSUserRight model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update SYSUserRight set ");
			stringBuilder.Append("RightID=@RightID,");
			stringBuilder.Append("CreateDate=@CreateDate,");
			stringBuilder.Append("OperatorID=@OperatorID,");
			stringBuilder.Append("UserID=@UserID");
			stringBuilder.Append(" where RightID=@RightID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@RightID", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@OperatorID", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@UserRightID", SqlDbType.Int, 4)
			};
			array[0].Value = model.RightID;
			array[1].Value = model.CreateDate;
			array[2].Value = model.OperatorID;
			array[3].Value = model.UserID;
			array[4].Value = model.UserRightID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int UserRightID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from SYSUserRight ");
			stringBuilder.Append(" where UserRightID=@UserRightID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@UserRightID", SqlDbType.Int, 4)
			};
			array[0].Value = UserRightID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string UserRightIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from SYSUserRight ");
			stringBuilder.Append(" where UserRightID in (" + UserRightIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.SYSUserRight GetModel(int UserRightID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 UserRightID,RightID,CreateDate,OperatorID,UserID from SYSUserRight ");
			stringBuilder.Append(" where UserRightID=@UserRightID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@UserRightID", SqlDbType.Int, 4)
			};
			array[0].Value = UserRightID;
			PEIS.Model.SYSUserRight UserRight = new PEIS.Model.SYSUserRight();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.SYSUserRight result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["UserRightID"].ToString() != "")
				{
                    UserRight.UserRightID = int.Parse(dataSet.Tables[0].Rows[0]["UserRightID"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Right"].ToString() != "")
				{
                    UserRight.RightID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["RightID"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["CreateDate"].ToString() != "")
				{
                    UserRight.CreateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["OperatorID"].ToString() != "")
				{
                    UserRight.OperatorID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["OperatorID"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
                    UserRight.UserID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["UserID"].ToString()));
				}
				result = UserRight;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select [UserRightID],[UserID],[RightID],[CreateDate],[OperatorID] ");
			stringBuilder.Append(" FROM SYSUserRight ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ");
			if (Top > 0)
			{
				stringBuilder.Append(" top " + Top.ToString());
			}
			stringBuilder.Append(" [UserRightID],[UserID],[RightID],[CreateDate],[OperatorID] ");
			stringBuilder.Append(" FROM SYSUserRight ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
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
