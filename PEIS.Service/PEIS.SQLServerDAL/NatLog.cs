using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class NatLog : INatLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_Log", "NatLog");
		}

		public bool Exists(int ID_Log)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from NatLog");
			stringBuilder.Append(" where ID_Log=@ID_Log ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Log", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Log;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.NatLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into NatLog(");
			stringBuilder.Append("Operater,OperateDate,OperateIP,OperateType,OperateContent)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@Operater,@OperateDate,@OperateIP,@OperateType,@OperateContent)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@Operater", SqlDbType.NVarChar, 10),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateIP", SqlDbType.VarChar, 128),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@OperateContent", SqlDbType.NVarChar, 128)
			};
			array[0].Value = model.Operater;
			array[1].Value = model.OperateDate;
			array[2].Value = model.OperateIP;
			array[3].Value = model.OperateType;
			array[4].Value = model.OperateContent;
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

		public bool Update(PEIS.Model.NatLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update NatLog set ");
			stringBuilder.Append("Operater=@Operater,");
			stringBuilder.Append("OperateDate=@OperateDate,");
			stringBuilder.Append("OperateIP=@OperateIP,");
			stringBuilder.Append("OperateType=@OperateType,");
			stringBuilder.Append("OperateContent=@OperateContent");
			stringBuilder.Append(" where ID_Log=@ID_Log");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@Operater", SqlDbType.NVarChar, 10),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateIP", SqlDbType.VarChar, 128),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@OperateContent", SqlDbType.NVarChar, 128),
				new SqlParameter("@ID_Log", SqlDbType.Int, 4)
			};
			array[0].Value = model.Operater;
			array[1].Value = model.OperateDate;
			array[2].Value = model.OperateIP;
			array[3].Value = model.OperateType;
			array[4].Value = model.OperateContent;
			array[5].Value = model.ID_Log;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_Log)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from NatLog ");
			stringBuilder.Append(" where ID_Log=@ID_Log");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Log", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Log;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_Loglist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from NatLog ");
			stringBuilder.Append(" where ID_Log in (" + ID_Loglist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.NatLog GetModel(int ID_Log)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_Log,Operater,OperateDate,OperateIP,OperateType,OperateContent from NatLog ");
			stringBuilder.Append(" where ID_Log=@ID_Log");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Log", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Log;
			PEIS.Model.NatLog natLog = new PEIS.Model.NatLog();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.NatLog result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_Log"].ToString() != "")
				{
					natLog.ID_Log = int.Parse(dataSet.Tables[0].Rows[0]["ID_Log"].ToString());
				}
				natLog.Operater = dataSet.Tables[0].Rows[0]["Operater"].ToString();
				if (dataSet.Tables[0].Rows[0]["OperateDate"].ToString() != "")
				{
					natLog.OperateDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["OperateDate"].ToString());
				}
				natLog.OperateIP = dataSet.Tables[0].Rows[0]["OperateIP"].ToString();
				if (dataSet.Tables[0].Rows[0]["OperateType"].ToString() != "")
				{
					natLog.OperateType = int.Parse(dataSet.Tables[0].Rows[0]["OperateType"].ToString());
				}
				natLog.OperateContent = dataSet.Tables[0].Rows[0]["OperateContent"].ToString();
				result = natLog;
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
			stringBuilder.Append("select ID_Log,Operater,OperateDate,OperateIP,OperateType,OperateContent ");
			stringBuilder.Append(" FROM NatLog ");
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
			stringBuilder.Append(" ID_Log,Operater,OperateDate,OperateIP,OperateType,OperateContent ");
			stringBuilder.Append(" FROM NatLog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
