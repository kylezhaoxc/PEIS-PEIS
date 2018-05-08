using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusBackLogType : IBusBackLogType
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_BackLogType", "BusBackLogType");
		}

		public bool Exists(int ID_BackLogType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusBackLogType");
			stringBuilder.Append(" where ID_BackLogType=@ID_BackLogType ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_BackLogType;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusBackLogType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusBackLogType(");
			stringBuilder.Append("BackLogTypeName,InputCode,DispOrder,Is_Banned,BanDescribe,ID_Operator,Operator,OperateDate,OperateType)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@BackLogTypeName,@InputCode,@DispOrder,@Is_Banned,@BanDescribe,@ID_Operator,@Operator,@OperateDate,@OperateType)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@BackLogTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateType", SqlDbType.Int, 4)
			};
			array[0].Value = model.BackLogTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.DispOrder;
			array[3].Value = model.Is_Banned;
			array[4].Value = model.BanDescribe;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
			array[7].Value = model.OperateDate;
			array[8].Value = model.OperateType;
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

		public bool Update(PEIS.Model.BusBackLogType model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusBackLogType set ");
			stringBuilder.Append("BackLogTypeName=@BackLogTypeName,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("BanDescribe=@BanDescribe,");
			stringBuilder.Append("ID_Operator=@ID_Operator,");
			stringBuilder.Append("Operator=@Operator,");
			stringBuilder.Append("OperateDate=@OperateDate,");
			stringBuilder.Append("OperateType=@OperateType,");
			stringBuilder.Append(" where ID_BackLogType=@ID_BackLogType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@BackLogTypeName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4)
			};
			array[0].Value = model.BackLogTypeName;
			array[1].Value = model.InputCode;
			array[2].Value = model.DispOrder;
			array[3].Value = model.Is_Banned;
			array[4].Value = model.BanDescribe;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
			array[7].Value = model.OperateDate;
			array[8].Value = model.OperateType;
			array[9].Value = model.ID_BackLogType;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_BackLogType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusBackLogType ");
			stringBuilder.Append(" where ID_BackLogType=@ID_BackLogType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_BackLogType;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_BackLogTypelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusBackLogType ");
			stringBuilder.Append(" where ID_BackLogType in (" + ID_BackLogTypelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusBackLogType GetModel(int ID_BackLogType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_BackLogType,BackLogTypeName,InputCode,DispOrder,Is_Banned,BanDescribe,ID_Operator,Operator,OperateDate,OperateType from BusBackLogType ");
			stringBuilder.Append(" where ID_BackLogType=@ID_BackLogType");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_BackLogType", SqlDbType.Int, 4)
			};
			array[0].Value = ID_BackLogType;
			PEIS.Model.BusBackLogType busBackLogType = new PEIS.Model.BusBackLogType();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusBackLogType result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_BackLogType"].ToString() != "")
				{
					busBackLogType.ID_BackLogType = int.Parse(dataSet.Tables[0].Rows[0]["ID_BackLogType"].ToString());
				}
				busBackLogType.BackLogTypeName = dataSet.Tables[0].Rows[0]["BackLogTypeName"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busBackLogType.Is_Banned = new bool?(true);
					}
					else
					{
						busBackLogType.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_Operator"].ToString() != "")
				{
					busBackLogType.ID_Operator = int.Parse(dataSet.Tables[0].Rows[0]["ID_Operator"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busBackLogType.DispOrder = int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				busBackLogType.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				busBackLogType.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				busBackLogType.Operator = dataSet.Tables[0].Rows[0]["Operator"].ToString();
				if (dataSet.Tables[0].Rows[0]["OperateDate"].ToString() != "")
				{
					busBackLogType.OperateDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["OperateDate"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["OperateType"].ToString() != "")
				{
					busBackLogType.OperateType = int.Parse(dataSet.Tables[0].Rows[0]["OperateDate"].ToString());
				}
				result = busBackLogType;
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
			stringBuilder.Append(" FROM BusBackLogType ");
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
			stringBuilder.Append(" FROM BusBackLogType ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
