using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusPEPackage : IBusPEPackage
    {
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("PEPackageID", "BusPEPackage");

        }

		public bool Exists(int PEPackageID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BusPEPackage");
            strSql.Append(" where PEPackageID=@PEPackageID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PEPackageID", SqlDbType.Int,4)           };
            parameters[0].Value = PEPackageID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		public int Add(PEIS.Model.BusPEPackage model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BusPEPackage(");
            strSql.Append("PEPackageID,PEPackageName,Forsex,CreatorID,CreateDate,isBanned,IDBanOpr,BanDate,BanDescribe,InputCode,DispOrder,Note)");
            strSql.Append(" values (");
            strSql.Append("@PEPackageID,@PEPackageName,@Forsex,@CreatorID,@CreateDate,@isBanned,@IDBanOpr,@BanDate,@BanDescribe,@InputCode,@DispOrder,@Note)");
            SqlParameter[] parameters = {
                    new SqlParameter("@PEPackageID", SqlDbType.Int,4),
                    new SqlParameter("@PEPackageName", SqlDbType.VarChar,50),
                    new SqlParameter("@Forsex", SqlDbType.Int,4),
                    new SqlParameter("@CreatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@isBanned", SqlDbType.Bit,1),
                    new SqlParameter("@IDBanOpr", SqlDbType.Int,4),
                    new SqlParameter("@BanDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@BanDescribe", SqlDbType.VarChar,50),
                    new SqlParameter("@InputCode", SqlDbType.VarChar,30),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@Note", SqlDbType.VarChar,50)};
            parameters[0].Value = model.PEPackageID;
            parameters[1].Value = model.PEPackageName;
            parameters[2].Value = model.Forsex;
            parameters[3].Value = model.CreatorID;
            parameters[4].Value = model.CreateDate;
            parameters[5].Value = model.isBanned;
            parameters[6].Value = model.IDBanOpr;
            parameters[7].Value = model.BanDate;
            parameters[8].Value = model.BanDescribe;
            parameters[9].Value = model.InputCode;
            parameters[10].Value = model.DispOrder;
            parameters[11].Value = model.Note;

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

        public bool Update(PEIS.Model.BusPEPackage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BusPEPackage set ");
            strSql.Append("PEPackageName=@PEPackageName,");
            strSql.Append("Forsex=@Forsex,");
            strSql.Append("CreatorID=@CreatorID,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("isBanned=@isBanned,");
            strSql.Append("IDBanOpr=@IDBanOpr,");
            strSql.Append("BanDate=@BanDate,");
            strSql.Append("BanDescribe=@BanDescribe,");
            strSql.Append("InputCode=@InputCode,");
            strSql.Append("DispOrder=@DispOrder,");
            strSql.Append("Note=@Note");
            strSql.Append(" where PEPackageID=@PEPackageID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PEPackageName", SqlDbType.VarChar,50),
                    new SqlParameter("@Forsex", SqlDbType.Int,4),
                    new SqlParameter("@CreatorID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@isBanned", SqlDbType.Bit,1),
                    new SqlParameter("@IDBanOpr", SqlDbType.Int,4),
                    new SqlParameter("@BanDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@BanDescribe", SqlDbType.VarChar,50),
                    new SqlParameter("@InputCode", SqlDbType.VarChar,30),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@Note", SqlDbType.VarChar,50),
                    new SqlParameter("@PEPackageID", SqlDbType.Int,4)};
            parameters[0].Value = model.PEPackageName;
            parameters[1].Value = model.Forsex;
            parameters[2].Value = model.CreatorID;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.isBanned;
            parameters[5].Value = model.IDBanOpr;
            parameters[6].Value = model.BanDate;
            parameters[7].Value = model.BanDescribe;
            parameters[8].Value = model.InputCode;
            parameters[9].Value = model.DispOrder;
            parameters[10].Value = model.Note;
            parameters[11].Value = model.PEPackageID;

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

        public bool Delete(int PEPackageID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BusPEPackage ");
            strSql.Append(" where PEPackageID=@PEPackageID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PEPackageID", SqlDbType.Int,4)           };
            parameters[0].Value = PEPackageID;

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

        public bool DeleteList(string PEPackageIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BusPEPackage ");
            strSql.Append(" where PEPackageID in (" + PEPackageIDlist + ")  ");
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

        public PEIS.Model.BusPEPackage GetModel(int PEPackageID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PEPackageID,PEPackageName,Forsex,CreatorID,CreateDate,isBanned,IDBanOpr,BanDate,BanDescribe,InputCode,DispOrder,Note from BusPEPackage ");
            strSql.Append(" where PEPackageID=@PEPackageID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PEPackageID", SqlDbType.Int,4)           };
            parameters[0].Value = PEPackageID;

           PEIS.Model.BusPEPackage model = new PEIS.Model.BusPEPackage();
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

        public PEIS.Model.BusPEPackage DataRowToModel(DataRow row)
        {
           PEIS.Model.BusPEPackage model = new PEIS.Model.BusPEPackage();
            if (row != null)
            {
                if (row["PEPackageID"] != null && row["PEPackageID"].ToString() != "")
                {
                    model.PEPackageID = int.Parse(row["PEPackageID"].ToString());
                }
                if (row["PEPackageName"] != null)
                {
                    model.PEPackageName = row["PEPackageName"].ToString();
                }
                if (row["Forsex"] != null && row["Forsex"].ToString() != "")
                {
                    model.Forsex = int.Parse(row["Forsex"].ToString());
                }
                if (row["CreatorID"] != null && row["CreatorID"].ToString() != "")
                {
                    model.CreatorID = int.Parse(row["CreatorID"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["isBanned"] != null && row["isBanned"].ToString() != "")
                {
                    if ((row["isBanned"].ToString() == "1") || (row["isBanned"].ToString().ToLower() == "true"))
                    {
                        model.isBanned = true;
                    }
                    else
                    {
                        model.isBanned = false;
                    }
                }
                if (row["IDBanOpr"] != null && row["IDBanOpr"].ToString() != "")
                {
                    model.IDBanOpr = int.Parse(row["IDBanOpr"].ToString());
                }
                if (row["BanDate"] != null && row["BanDate"].ToString() != "")
                {
                    model.BanDate = DateTime.Parse(row["BanDate"].ToString());
                }
                if (row["BanDescribe"] != null)
                {
                    model.BanDescribe = row["BanDescribe"].ToString();
                }
                if (row["InputCode"] != null)
                {
                    model.InputCode = row["InputCode"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["Note"] != null)
                {
                    model.Note = row["Note"].ToString();
                }
            }
            return model;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PEPackageID,PEPackageName,Forsex,CreatorID,CreateDate,isBanned,IDBanOpr,BanDate,BanDescribe,InputCode,DispOrder,Note ");
            strSql.Append(" FROM BusPEPackage ");
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
            strSql.Append(" PEPackageID,PEPackageName,Forsex,CreatorID,CreateDate,isBanned,IDBanOpr,BanDate,BanDescribe,InputCode,DispOrder,Note ");
            strSql.Append(" FROM BusPEPackage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
