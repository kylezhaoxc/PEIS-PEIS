using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusFeeDetail : IBusFeeDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_DtlFee", "BusFeeDetail");
		}

		public bool Exists(int ID_DtlFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusFeeDetail");
			stringBuilder.Append(" where ID_DtlFee=@ID_DtlFee ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_DtlFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_DtlFee;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusFeeDetail model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusFeeDetail(");
			stringBuilder.Append("ID_Fee,ID_ExamItem)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Fee,@ID_ExamItem)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Fee;
			array[1].Value = model.ID_ExamItem;
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

		public bool Update(PEIS.Model.BusFeeDetail model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusFeeDetail set ");
			stringBuilder.Append("ID_Fee=@ID_Fee,");
			stringBuilder.Append("ID_ExamItem=@ID_ExamItem");
			stringBuilder.Append(" where ID_DtlFee=@ID_DtlFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4),
				new SqlParameter("@ID_DtlFee", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Fee;
			array[1].Value = model.ID_ExamItem;
			array[2].Value = model.ID_DtlFee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_DtlFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusFeeDetail ");
			stringBuilder.Append(" where ID_DtlFee=@ID_DtlFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_DtlFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_DtlFee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_DtlFeelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusFeeDetail ");
			stringBuilder.Append(" where ID_DtlFee in (" + ID_DtlFeelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusFeeDetail GetModel(int ID_DtlFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_DtlFee,ID_Fee,ID_ExamItem from BusFeeDetail ");
			stringBuilder.Append(" where ID_DtlFee=@ID_DtlFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_DtlFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_DtlFee;
			PEIS.Model.BusFeeDetail busFeeDetail = new PEIS.Model.BusFeeDetail();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusFeeDetail result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_DtlFee"].ToString() != "")
				{
					busFeeDetail.ID_DtlFee = int.Parse(dataSet.Tables[0].Rows[0]["ID_DtlFee"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Fee"].ToString() != "")
				{
					busFeeDetail.ID_Fee = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Fee"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString() != "")
				{
					busFeeDetail.ID_ExamItem = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString()));
				}
				result = busFeeDetail;
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
			stringBuilder.Append("select ID_DtlFee,ID_Fee,ID_ExamItem ");
			stringBuilder.Append(" FROM BusFeeDetail ");
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
			stringBuilder.Append(" ID_DtlFee,ID_Fee,ID_ExamItem ");
			stringBuilder.Append(" FROM BusFeeDetail ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
