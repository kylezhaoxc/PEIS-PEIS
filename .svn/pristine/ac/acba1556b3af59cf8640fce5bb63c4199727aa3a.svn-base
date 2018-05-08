using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusExamItemGroup : IBusExamItemGroup
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ExamItemGroup", "BusExamItemGroup");
		}

		public bool Exists(int ID_ExamItemGroup)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusExamItemGroup");
			stringBuilder.Append(" where ID_ExamItemGroup=@ID_ExamItemGroup ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItemGroup", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItemGroup;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusExamItemGroup model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusExamItemGroup(");
			stringBuilder.Append("ExamItemGroupName,InputCode,DispOrder,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ExamItemGroupName,@InputCode,@DispOrder,@Note,@Is_Banned,@ID_Operator,@Operator,@OperateDate,@OperateType,@BanDescribe)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamItemGroupName", SqlDbType.VarChar, 120),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 200),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50)
			};
			array[0].Value = model.ExamItemGroupName;
			array[1].Value = model.InputCode;
			array[2].Value = model.DispOrder;
			array[3].Value = model.Note;
			array[4].Value = model.Is_Banned;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
			array[7].Value = model.OperateDate;
			array[8].Value = model.OperateType;
			array[9].Value = model.BanDescribe;
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

		public bool Update(PEIS.Model.BusExamItemGroup model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusExamItemGroup set ");
			stringBuilder.Append("ExamItemGroupName=@ExamItemGroupName,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_Operator=@ID_Operator,");
			stringBuilder.Append("Operator=@Operator,");
			stringBuilder.Append("OperateDate=@OperateDate,");
			stringBuilder.Append("OperateType=@OperateType,");
			stringBuilder.Append("BanDescribe=@BanDescribe");
			stringBuilder.Append(" where ID_ExamItemGroup=@ID_ExamItemGroup");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamItemGroupName", SqlDbType.VarChar, 120),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 20),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 200),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_Operator", SqlDbType.Int, 4),
				new SqlParameter("@Operator", SqlDbType.VarChar, 30),
				new SqlParameter("@OperateDate", SqlDbType.DateTime),
				new SqlParameter("@OperateType", SqlDbType.Int, 4),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_ExamItemGroup", SqlDbType.Int, 4)
			};
			array[0].Value = model.ExamItemGroupName;
			array[1].Value = model.InputCode;
			array[2].Value = model.DispOrder;
			array[3].Value = model.Note;
			array[4].Value = model.Is_Banned;
			array[5].Value = model.ID_Operator;
			array[6].Value = model.Operator;
			array[7].Value = model.OperateDate;
			array[8].Value = model.OperateType;
			array[9].Value = model.BanDescribe;
			array[10].Value = model.ID_ExamItemGroup;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ExamItemGroup)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusExamItemGroup ");
			stringBuilder.Append(" where ID_ExamItemGroup=@ID_ExamItemGroup");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItemGroup", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItemGroup;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ExamItemGrouplist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusExamItemGroup ");
			stringBuilder.Append(" where ID_ExamItemGroup in (" + ID_ExamItemGrouplist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusExamItemGroup GetModel(int ID_ExamItemGroup)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ExamItemGroup,ExamItemGroupName,InputCode,DispOrder,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe from BusExamItemGroup ");
			stringBuilder.Append(" where ID_ExamItemGroup=@ID_ExamItemGroup");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItemGroup", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItemGroup;
			PEIS.Model.BusExamItemGroup busExamItemGroup = new PEIS.Model.BusExamItemGroup();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusExamItemGroup result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ExamItemGroup"].ToString() != "")
				{
					busExamItemGroup.ID_ExamItemGroup = int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItemGroup"].ToString());
				}
				busExamItemGroup.ExamItemGroupName = dataSet.Tables[0].Rows[0]["ExamItemGroupName"].ToString();
				busExamItemGroup.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busExamItemGroup.DispOrder = int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				busExamItemGroup.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						busExamItemGroup.Is_Banned = new bool?(true);
					}
					else
					{
						busExamItemGroup.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_Operator"].ToString() != "")
				{
					busExamItemGroup.ID_Operator = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Operator"].ToString()));
				}
				busExamItemGroup.Operator = dataSet.Tables[0].Rows[0]["Operator"].ToString();
				if (dataSet.Tables[0].Rows[0]["OperateDate"].ToString() != "")
				{
					busExamItemGroup.OperateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["OperateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["OperateType"].ToString() != "")
				{
					busExamItemGroup.OperateType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["OperateType"].ToString()));
				}
				busExamItemGroup.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				result = busExamItemGroup;
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
			stringBuilder.Append("select ID_ExamItemGroup,ExamItemGroupName,InputCode,DispOrder,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe ");
			stringBuilder.Append(" FROM BusExamItemGroup ");
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
			stringBuilder.Append(" ID_ExamItemGroup,ExamItemGroupName,InputCode,DispOrder,Note,Is_Banned,ID_Operator,Operator,OperateDate,OperateType,BanDescribe ");
			stringBuilder.Append(" FROM BusExamItemGroup ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
