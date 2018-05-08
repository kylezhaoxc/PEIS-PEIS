using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustRelationCustPEInfo : IOnCustRelationCustPEInfo
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_CustRelation", "OnCustRelationCustPEInfo");
		}

		public bool Exists(int ID_CustRelation)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustRelationCustPEInfo");
			stringBuilder.Append(" where ID_CustRelation=@ID_CustRelation ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustRelation", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustRelation;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustRelationCustPEInfo model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustRelationCustPEInfo(");
			stringBuilder.Append("ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_ArcCustomer,@IDCardNo,@ExamCardNo,@ID_Customer,@Is_CompletePhysical,@ExamState)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int, 4),
				new SqlParameter("@IDCardNo", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamCardNo", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@Is_CompletePhysical", SqlDbType.Bit, 1),
				new SqlParameter("@ExamState", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_ArcCustomer;
			array[1].Value = model.IDCardNo;
			array[2].Value = model.ExamCardNo;
			array[3].Value = model.ID_Customer;
			array[4].Value = model.Is_CompletePhysical;
			array[5].Value = model.ExamState;
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

		public bool Update(PEIS.Model.OnCustRelationCustPEInfo model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustRelationCustPEInfo set ");
			stringBuilder.Append("ID_ArcCustomer=@ID_ArcCustomer,");
			stringBuilder.Append("IDCardNo=@IDCardNo,");
			stringBuilder.Append("ExamCardNo=@ExamCardNo,");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("Is_CompletePhysical=@Is_CompletePhysical,");
			stringBuilder.Append("ExamState=@ExamState");
			stringBuilder.Append(" where ID_CustRelation=@ID_CustRelation");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int, 4),
				new SqlParameter("@IDCardNo", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamCardNo", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@Is_CompletePhysical", SqlDbType.Bit, 1),
				new SqlParameter("@ExamState", SqlDbType.Int, 4),
				new SqlParameter("@ID_CustRelation", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_ArcCustomer;
			array[1].Value = model.IDCardNo;
			array[2].Value = model.ExamCardNo;
			array[3].Value = model.ID_Customer;
			array[4].Value = model.Is_CompletePhysical;
			array[5].Value = model.ExamState;
			array[6].Value = model.ID_CustRelation;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_CustRelation)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustRelationCustPEInfo ");
			stringBuilder.Append(" where ID_CustRelation=@ID_CustRelation");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustRelation", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustRelation;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_CustRelationlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustRelationCustPEInfo ");
			stringBuilder.Append(" where ID_CustRelation in (" + ID_CustRelationlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustRelationCustPEInfo GetModel(int ID_CustRelation)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_CustRelation,ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState from OnCustRelationCustPEInfo ");
			stringBuilder.Append(" where ID_CustRelation=@ID_CustRelation");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustRelation", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustRelation;
			PEIS.Model.OnCustRelationCustPEInfo onCustRelationCustPEInfo = new PEIS.Model.OnCustRelationCustPEInfo();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustRelationCustPEInfo result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_CustRelation"].ToString() != "")
				{
					onCustRelationCustPEInfo.ID_CustRelation = int.Parse(dataSet.Tables[0].Rows[0]["ID_CustRelation"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_ArcCustomer"].ToString() != "")
				{
					onCustRelationCustPEInfo.ID_ArcCustomer = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ArcCustomer"].ToString()));
				}
				onCustRelationCustPEInfo.IDCardNo = dataSet.Tables[0].Rows[0]["IDCardNo"].ToString();
				onCustRelationCustPEInfo.ExamCardNo = dataSet.Tables[0].Rows[0]["ExamCardNo"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onCustRelationCustPEInfo.ID_Customer = new long?(long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_CompletePhysical"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_CompletePhysical"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_CompletePhysical"].ToString().ToLower() == "true")
					{
						onCustRelationCustPEInfo.Is_CompletePhysical = new bool?(true);
					}
					else
					{
						onCustRelationCustPEInfo.Is_CompletePhysical = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ExamState"].ToString() != "")
				{
					onCustRelationCustPEInfo.ExamState = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ExamState"].ToString()));
				}
				result = onCustRelationCustPEInfo;
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
			stringBuilder.Append("select ID_CustRelation,ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState ");
			stringBuilder.Append(" FROM OnCustRelationCustPEInfo ");
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
			stringBuilder.Append(" ID_CustRelation,ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState ");
			stringBuilder.Append(" FROM OnCustRelationCustPEInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
