using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnFianlCheck : IOnFianlCheck
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_FinalCheck", "OnFianlCheck");
		}

		public bool Exists(int ID_FinalCheck)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnFianlCheck");
			stringBuilder.Append(" where ID_FinalCheck=@ID_FinalCheck ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FinalCheck", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FinalCheck;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnFianlCheck model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnFianlCheck(");
			stringBuilder.Append("ID_Customer,CustomerName,ID_FinalDoctor,FinalDoctor,SubmitDate,ID_FinalCheckDoctor,FinalCheckDoctor,FinaleCheckDate,Is_Pass,RefuseReason)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Customer,@CustomerName,@ID_FinalDoctor,@FinalDoctor,@SubmitDate,@ID_FinalCheckDoctor,@FinalCheckDoctor,@FinaleCheckDate,@Is_Pass,@RefuseReason)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@CustomerName", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_FinalDoctor", SqlDbType.Int, 4),
				new SqlParameter("@FinalDoctor", SqlDbType.VarChar, 30),
				new SqlParameter("@SubmitDate", SqlDbType.DateTime),
				new SqlParameter("@ID_FinalCheckDoctor", SqlDbType.Int, 4),
				new SqlParameter("@FinalCheckDoctor", SqlDbType.VarChar, 30),
				new SqlParameter("@FinaleCheckDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Pass", SqlDbType.Bit, 1),
				new SqlParameter("@RefuseReason", SqlDbType.Text)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.CustomerName;
			array[2].Value = model.ID_FinalDoctor;
			array[3].Value = model.FinalDoctor;
			array[4].Value = model.SubmitDate;
			array[5].Value = model.ID_FinalCheckDoctor;
			array[6].Value = model.FinalCheckDoctor;
			array[7].Value = model.FinaleCheckDate;
			array[8].Value = model.Is_Pass;
			array[9].Value = model.RefuseReason;
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

		public bool Update(PEIS.Model.OnFianlCheck model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnFianlCheck set ");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("CustomerName=@CustomerName,");
			stringBuilder.Append("ID_FinalDoctor=@ID_FinalDoctor,");
			stringBuilder.Append("FinalDoctor=@FinalDoctor,");
			stringBuilder.Append("SubmitDate=@SubmitDate,");
			stringBuilder.Append("ID_FinalCheckDoctor=@ID_FinalCheckDoctor,");
			stringBuilder.Append("FinalCheckDoctor=@FinalCheckDoctor,");
			stringBuilder.Append("FinaleCheckDate=@FinaleCheckDate,");
			stringBuilder.Append("Is_Pass=@Is_Pass,");
			stringBuilder.Append("RefuseReason=@RefuseReason");
			stringBuilder.Append(" where ID_FinalCheck=@ID_FinalCheck");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@CustomerName", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_FinalDoctor", SqlDbType.Int, 4),
				new SqlParameter("@FinalDoctor", SqlDbType.VarChar, 30),
				new SqlParameter("@SubmitDate", SqlDbType.DateTime),
				new SqlParameter("@ID_FinalCheckDoctor", SqlDbType.Int, 4),
				new SqlParameter("@FinalCheckDoctor", SqlDbType.VarChar, 30),
				new SqlParameter("@FinaleCheckDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Pass", SqlDbType.Bit, 1),
				new SqlParameter("@RefuseReason", SqlDbType.Text),
				new SqlParameter("@ID_FinalCheck", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.CustomerName;
			array[2].Value = model.ID_FinalDoctor;
			array[3].Value = model.FinalDoctor;
			array[4].Value = model.SubmitDate;
			array[5].Value = model.ID_FinalCheckDoctor;
			array[6].Value = model.FinalCheckDoctor;
			array[7].Value = model.FinaleCheckDate;
			array[8].Value = model.Is_Pass;
			array[9].Value = model.RefuseReason;
			array[10].Value = model.ID_FinalCheck;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_FinalCheck)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnFianlCheck ");
			stringBuilder.Append(" where ID_FinalCheck=@ID_FinalCheck");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FinalCheck", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FinalCheck;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_FinalChecklist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnFianlCheck ");
			stringBuilder.Append(" where ID_FinalCheck in (" + ID_FinalChecklist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnFianlCheck GetModel(int ID_FinalCheck)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_FinalCheck,ID_Customer,CustomerName,ID_FinalDoctor,FinalDoctor,SubmitDate,ID_FinalCheckDoctor,FinalCheckDoctor,FinaleCheckDate,Is_Pass,RefuseReason from OnFianlCheck ");
			stringBuilder.Append(" where ID_FinalCheck=@ID_FinalCheck");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_FinalCheck", SqlDbType.Int, 4)
			};
			array[0].Value = ID_FinalCheck;
			PEIS.Model.OnFianlCheck onFianlCheck = new PEIS.Model.OnFianlCheck();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnFianlCheck result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_FinalCheck"].ToString() != "")
				{
					onFianlCheck.ID_FinalCheck = int.Parse(dataSet.Tables[0].Rows[0]["ID_FinalCheck"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onFianlCheck.ID_Customer = new long?(long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString()));
				}
				onFianlCheck.CustomerName = dataSet.Tables[0].Rows[0]["CustomerName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_FinalDoctor"].ToString() != "")
				{
					onFianlCheck.ID_FinalDoctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FinalDoctor"].ToString()));
				}
				onFianlCheck.FinalDoctor = dataSet.Tables[0].Rows[0]["FinalDoctor"].ToString();
				if (dataSet.Tables[0].Rows[0]["SubmitDate"].ToString() != "")
				{
					onFianlCheck.SubmitDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["SubmitDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_FinalCheckDoctor"].ToString() != "")
				{
					onFianlCheck.ID_FinalCheckDoctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FinalCheckDoctor"].ToString()));
				}
				onFianlCheck.FinalCheckDoctor = dataSet.Tables[0].Rows[0]["FinalCheckDoctor"].ToString();
				if (dataSet.Tables[0].Rows[0]["FinaleCheckDate"].ToString() != "")
				{
					onFianlCheck.FinaleCheckDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["FinaleCheckDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Pass"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Pass"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Pass"].ToString().ToLower() == "true")
					{
						onFianlCheck.Is_Pass = new bool?(true);
					}
					else
					{
						onFianlCheck.Is_Pass = new bool?(false);
					}
				}
				onFianlCheck.RefuseReason = dataSet.Tables[0].Rows[0]["RefuseReason"].ToString();
				result = onFianlCheck;
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
			stringBuilder.Append("select ID_FinalCheck,ID_Customer,CustomerName,ID_FinalDoctor,FinalDoctor,SubmitDate,ID_FinalCheckDoctor,FinalCheckDoctor,FinaleCheckDate,Is_Pass,RefuseReason ");
			stringBuilder.Append(" FROM OnFianlCheck ");
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
			stringBuilder.Append(" ID_FinalCheck,ID_Customer,CustomerName,ID_FinalDoctor,FinalDoctor,SubmitDate,ID_FinalCheckDoctor,FinalCheckDoctor,FinaleCheckDate,Is_Pass,RefuseReason ");
			stringBuilder.Append(" FROM OnFianlCheck ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
