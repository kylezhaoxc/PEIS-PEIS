using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustBackLog : IOnCustBackLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_BackLog", "OnCustBackLog");
		}

		public bool Exists(int ID_BackLog)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustBackLog");
			stringBuilder.Append(" where ID_BackLog=@ID_BackLog ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_BackLog", SqlDbType.Int, 4)
			};
			array[0].Value = ID_BackLog;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustBackLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustBackLog(");
			stringBuilder.Append("ID_Customer,ID_BackLogType,CreateDate,OperateDate,Is_Finished,ID_Operator,Operator)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Customer,@ID_BackLogType,@CreateDate,@OperateDate,@Is_Finished,@ID_Operator,@Operator)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt),
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Finished", SqlDbType.Bit),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ID_BackLogType;
			array[2].Value = model.CreateDate;
			array[3].Value = model.OperateDate;
			array[4].Value = model.Is_Finished;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
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

		public bool Update(PEIS.Model.OnCustBackLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustBackLog set ");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("ID_BackLogType=@ID_BackLogType,");
			stringBuilder.Append("CreateDate=@CreateDate,");
			stringBuilder.Append("OperateDate=@OperateDate,");
			stringBuilder.Append("Is_Finished=@Is_Finished,");
			stringBuilder.Append("ID_Operator=@ID_Operator,");
			stringBuilder.Append("Operator=@Operator ");
			stringBuilder.Append(" where ID_BackLog=@ID_BackLog");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt),
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Finished", SqlDbType.Bit),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_BackLog", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ID_BackLogType;
			array[2].Value = model.CreateDate;
			array[3].Value = model.OperateDate;
			array[4].Value = model.Is_Finished;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
			array[7].Value = model.ID_BackLog;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_BackLog)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustBackLog ");
			stringBuilder.Append(" where ID_BackLog=@ID_BackLog");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_BackLog", SqlDbType.Int, 4)
			};
			array[0].Value = ID_BackLog;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_BackLoglist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustBackLog ");
			stringBuilder.Append(" where ID_BackLog in (" + ID_BackLoglist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustBackLog GetModel(int ID_BackLog)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_BackLog,ID_Customer,ID_BackLogType,CreateDate,OperateDate,Is_Finished,ID_Operator,Operator  from OnCustBackLog ");
			stringBuilder.Append(" where ID_BackLog=@ID_BackLog");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_BackLog", SqlDbType.Int, 4)
			};
			array[0].Value = ID_BackLog;
			PEIS.Model.OnCustBackLog onCustBackLog = new PEIS.Model.OnCustBackLog();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustBackLog result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_BackLog"].ToString() != "")
				{
					onCustBackLog.ID_BackLog = (long)int.Parse(dataSet.Tables[0].Rows[0]["ID_BackLog"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onCustBackLog.ID_Customer = long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_BackLogType"].ToString() != "")
				{
					onCustBackLog.ID_BackLogType = int.Parse(dataSet.Tables[0].Rows[0]["ID_BackLogType"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["CreateDate"].ToString() != "")
				{
					onCustBackLog.CreateDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateDate"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["OperateDate"].ToString() != "")
				{
					onCustBackLog.OperateDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["OperateDate"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["Is_Finished"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Finished"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Finished"].ToString().ToLower() == "true")
					{
						onCustBackLog.Is_Finished = new bool?(true);
					}
					else
					{
						onCustBackLog.Is_Finished = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_Operator"].ToString() != "")
				{
					onCustBackLog.ID_Operator = int.Parse(dataSet.Tables[0].Rows[0]["ID_Operator"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Operator"].ToString() != "")
				{
					onCustBackLog.ID_Operator = int.Parse(dataSet.Tables[0].Rows[0]["ID_Operator"].ToString());
				}
				onCustBackLog.Operator = dataSet.Tables[0].Rows[0]["Operator"].ToString();
				result = onCustBackLog;
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
			stringBuilder.Append("select ID_BackLogType,BackLogTypeName,InputCode,DispOrder,Is_Banned,BanDescribe,ID_Operator,Operator,OperateDate,OperateType ");
			stringBuilder.Append(" FROM OnCustBackLog ");
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
			stringBuilder.Append(" ID_BackLogType,BackLogTypeName,InputCode,DispOrder,Is_Banned,BanDescribe,ID_Operator,Operator,OperateDate,OperateType ");
			stringBuilder.Append(" FROM OnCustBackLog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public int AddOrUpdateByBackLogType(PEIS.Model.OnCustBackLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("if not exists(select 1 from OnCustBackLog where ID_Customer=@ID_Customer and ID_BackLogType=@ID_BackLogType)");
			stringBuilder.Append("begin insert into OnCustBackLog(");
			stringBuilder.Append("ID_Customer,ID_BackLogType,CreateDate,OperateDate,Is_Finished,ID_Operator,Operator)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Customer,@ID_BackLogType,@CreateDate,@OperateDate,@Is_Finished,@ID_Operator,@Operator) end");
			stringBuilder.Append(" else begin update OnCustBackLog set OperateDate=@OperateDate,ID_Operator=@ID_Operator,Operator=@Operator,Is_Finished=@Is_Finished where ID_Customer=@ID_Customer and ID_BackLogType=@ID_BackLogType end");
			if (model.ExternByUpdateRegisteType != null)
			{
				bool value = model.ExternByUpdateRegisteType.Is_updateRegisteDate.Value;
				if (value)
				{
					ArrayList custBackLogList = model.ExternByUpdateRegisteType.CustBackLogList;
					if (custBackLogList != null)
					{
						if (custBackLogList.Count > 0)
						{
							string text = string.Empty;
							for (int i = 0; i < custBackLogList.Count; i++)
							{
								text = text + "'" + custBackLogList[i].ToString() + "',";
							}
							text = text.TrimEnd(new char[]
							{
								','
							});
							if (text.Length > 0)
							{
								stringBuilder.Append(string.Concat(new object[]
								{
									";update OnCustBackLog set OperateDate='",
									model.ExternByUpdateRegisteType.RegisteDate,
									"' where ID_Customer=@ID_Customer and ID_BackLogType in(",
									text,
									")"
								}));
							}
						}
					}
				}
			}
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt),
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Finished", SqlDbType.Bit),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.ID_BackLogType;
			array[2].Value = model.CreateDate;
			array[3].Value = model.OperateDate;
			array[4].Value = model.Is_Finished;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
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
	}
}
