using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusSetFeeDetail : IBusSetFeeDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_DtlSetFee", "BusSetFeeDetail");
		}

		public bool Exists(int ID_DtlSetFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusSetFeeDetail");
			stringBuilder.Append(" where ID_DtlSetFee=@ID_DtlSetFee ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_DtlSetFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_DtlSetFee;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusSetFeeDetail model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusSetFeeDetail(");
			stringBuilder.Append("PEPackageID,ID_FeeItem)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@PEPackageID,@ID_FeeItem)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@PEPackageID", SqlDbType.Int, 4),
				new SqlParameter("@ID_FeeItem", SqlDbType.Int, 4)
			};
			array[0].Value = model.PEPackageID;
			array[1].Value = model.ID_FeeItem;
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

		public bool Update(PEIS.Model.BusSetFeeDetail model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusSetFeeDetail set ");
			stringBuilder.Append("PEPackageID=@PEPackageID,");
			stringBuilder.Append("ID_FeeItem=@ID_FeeItem");
			stringBuilder.Append(" where ID_DtlSetFee=@ID_DtlSetFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@PEPackageID", SqlDbType.Int, 4),
				new SqlParameter("@ID_FeeItem", SqlDbType.Int, 4),
				new SqlParameter("@ID_DtlSetFee", SqlDbType.Int, 4)
			};
			array[0].Value = model.PEPackageID;
			array[1].Value = model.ID_FeeItem;
			array[2].Value = model.ID_DtlSetFee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_DtlSetFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusSetFeeDetail ");
			stringBuilder.Append(" where ID_DtlSetFee=@ID_DtlSetFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_DtlSetFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_DtlSetFee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_DtlSetFeelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusSetFeeDetail ");
			stringBuilder.Append(" where ID_DtlSetFee in (" + ID_DtlSetFeelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusSetFeeDetail GetModel(int ID_DtlSetFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_DtlSetFee,PEPackageID,ID_FeeItem from BusSetFeeDetail ");
			stringBuilder.Append(" where ID_DtlSetFee=@ID_DtlSetFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_DtlSetFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_DtlSetFee;
			PEIS.Model.BusSetFeeDetail busSetFeeDetail = new PEIS.Model.BusSetFeeDetail();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusSetFeeDetail result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_DtlSetFee"].ToString() != "")
				{
					busSetFeeDetail.ID_DtlSetFee = int.Parse(dataSet.Tables[0].Rows[0]["ID_DtlSetFee"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["PEPackageID"].ToString() != "")
				{
					busSetFeeDetail.PEPackageID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["PEPackageID"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_FeeItem"].ToString() != "")
				{
					busSetFeeDetail.ID_FeeItem = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FeeItem"].ToString()));
				}
				result = busSetFeeDetail;
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
			stringBuilder.Append("select ID_DtlSetFee,PEPackageID,ID_FeeItem ");
			stringBuilder.Append(" FROM BusSetFeeDetail ");
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
			stringBuilder.Append(" ID_DtlSetFee,PEPackageID,ID_FeeItem ");
			stringBuilder.Append(" FROM BusSetFeeDetail ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
