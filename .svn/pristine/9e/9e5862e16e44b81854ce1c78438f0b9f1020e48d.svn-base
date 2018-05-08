using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnArcCust : IOnArcCust
	{
		private int _defaultCount = 200;

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ArcCustomer", "OnArcCust");
		}

		public bool Exists(int ID_ArcCustomer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnArcCust");
			stringBuilder.Append(" where ID_ArcCustomer=@ID_ArcCustomer ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ArcCustomer;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnArcCust model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnArcCust(");
			stringBuilder.Append("ID_Gender,ID_Marriage,NationID,CultrulID,VocationID,CustomerName,IDCard,ExamCard,Photo,BirthDay,GenderName,MarriageName,NationName,Address,MobileNo,Email,CultrulName,VocationName,FinishedNum,UnfinishedNum,FirstDatePE,LatestDatePE)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Gender,@ID_Marriage,@NationID,@CultrulID,@VocationID,@CustomerName,@IDCard,@ExamCard,@Photo,@BirthDay,@GenderName,@MarriageName,@NationName,@Address,@MobileNo,@Email,@CultrulName,@VocationName,@FinishedNum,@UnfinishedNum,@FirstDatePE,@LatestDatePE)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Gender", SqlDbType.Int, 4),
				new SqlParameter("@ID_Marriage", SqlDbType.Int, 4),
				new SqlParameter("@NationID", SqlDbType.Int, 4),
				new SqlParameter("@CultrulID", SqlDbType.Int, 4),
				new SqlParameter("@VocationID", SqlDbType.Int, 4),
				new SqlParameter("@CustomerName", SqlDbType.VarChar, 30),
				new SqlParameter("@IDCard", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamCard", SqlDbType.VarChar, 30),
				new SqlParameter("@Photo", SqlDbType.Image),
				new SqlParameter("@BirthDay", SqlDbType.DateTime),
				new SqlParameter("@GenderName", SqlDbType.VarChar, 8),
				new SqlParameter("@MarriageName", SqlDbType.VarChar, 8),
				new SqlParameter("@NationName", SqlDbType.VarChar, 10),
				new SqlParameter("@Address", SqlDbType.VarChar, 120),
				new SqlParameter("@MobileNo", SqlDbType.VarChar, 30),
				new SqlParameter("@Email", SqlDbType.VarChar, 80),
				new SqlParameter("@CultrulName", SqlDbType.VarChar, 10),
				new SqlParameter("@VocationName", SqlDbType.VarChar, 10),
				new SqlParameter("@FinishedNum", SqlDbType.Int, 4),
				new SqlParameter("@UnfinishedNum", SqlDbType.Int, 4),
				new SqlParameter("@FirstDatePE", SqlDbType.DateTime),
				new SqlParameter("@LatestDatePE", SqlDbType.DateTime)
			};
			array[0].Value = model.ID_Gender;
			array[1].Value = model.ID_Marriage;
			array[2].Value = model.NationID;
			array[3].Value = model.CultrulID;
			array[4].Value = model.VocationID;
			array[5].Value = model.CustomerName;
			array[6].Value = model.IDCard;
			array[7].Value = model.ExamCard;
			array[8].Value = model.Photo;
			array[9].Value = model.BirthDay;
			array[10].Value = model.GenderName;
			array[11].Value = model.MarriageName;
			array[12].Value = model.NationName;
			array[13].Value = model.Address;
			array[14].Value = model.MobileNo;
			array[15].Value = model.Email;
			array[16].Value = model.CultrulName;
			array[17].Value = model.VocationName;
			array[18].Value = model.FinishedNum;
			array[19].Value = model.UnfinishedNum;
			array[20].Value = model.FirstDatePE;
			array[21].Value = model.LatestDatePE;
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

		public bool Update(PEIS.Model.OnArcCust model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnArcCust set ");
			stringBuilder.Append("ID_Gender=@ID_Gender,");
			stringBuilder.Append("ID_Marriage=@ID_Marriage,");
			stringBuilder.Append("NationID=@NationID,");
			stringBuilder.Append("CultrulID=@CultrulID,");
			stringBuilder.Append("VocationID=@VocationID,");
			stringBuilder.Append("CustomerName=@CustomerName,");
			stringBuilder.Append("IDCard=@IDCard,");
			stringBuilder.Append("ExamCard=@ExamCard,");
			stringBuilder.Append("Photo=@Photo,");
			stringBuilder.Append("BirthDay=@BirthDay,");
			stringBuilder.Append("GenderName=@GenderName,");
			stringBuilder.Append("MarriageName=@MarriageName,");
			stringBuilder.Append("NationName=@NationName,");
			stringBuilder.Append("Address=@Address,");
			stringBuilder.Append("MobileNo=@MobileNo,");
			stringBuilder.Append("Email=@Email,");
			stringBuilder.Append("CultrulName=@CultrulName,");
			stringBuilder.Append("VocationName=@VocationName,");
			stringBuilder.Append("FinishedNum=@FinishedNum,");
			stringBuilder.Append("UnfinishedNum=@UnfinishedNum,");
			stringBuilder.Append("FirstDatePE=@FirstDatePE,");
			stringBuilder.Append("LatestDatePE=@LatestDatePE");
			stringBuilder.Append(" where ID_ArcCustomer=@ID_ArcCustomer");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Gender", SqlDbType.Int, 4),
				new SqlParameter("@ID_Marriage", SqlDbType.Int, 4),
				new SqlParameter("@NationID", SqlDbType.Int, 4),
				new SqlParameter("@CultrulID", SqlDbType.Int, 4),
				new SqlParameter("@VocationID", SqlDbType.Int, 4),
				new SqlParameter("@CustomerName", SqlDbType.VarChar, 30),
				new SqlParameter("@IDCard", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamCard", SqlDbType.VarChar, 30),
				new SqlParameter("@Photo", SqlDbType.Image),
				new SqlParameter("@BirthDay", SqlDbType.DateTime),
				new SqlParameter("@GenderName", SqlDbType.VarChar, 8),
				new SqlParameter("@MarriageName", SqlDbType.VarChar, 8),
				new SqlParameter("@NationName", SqlDbType.VarChar, 10),
				new SqlParameter("@Address", SqlDbType.VarChar, 120),
				new SqlParameter("@MobileNo", SqlDbType.VarChar, 30),
				new SqlParameter("@Email", SqlDbType.VarChar, 80),
				new SqlParameter("@CultrulName", SqlDbType.VarChar, 10),
				new SqlParameter("@VocationName", SqlDbType.VarChar, 10),
				new SqlParameter("@FinishedNum", SqlDbType.Int, 4),
				new SqlParameter("@UnfinishedNum", SqlDbType.Int, 4),
				new SqlParameter("@FirstDatePE", SqlDbType.DateTime),
				new SqlParameter("@LatestDatePE", SqlDbType.DateTime),
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Gender;
			array[1].Value = model.ID_Marriage;
			array[2].Value = model.NationID;
			array[3].Value = model.CultrulID;
			array[4].Value = model.VocationID;
			array[5].Value = model.CustomerName;
			array[6].Value = model.IDCard;
			array[7].Value = model.ExamCard;
			array[8].Value = model.Photo;
			array[9].Value = model.BirthDay;
			array[10].Value = model.GenderName;
			array[11].Value = model.MarriageName;
			array[12].Value = model.NationName;
			array[13].Value = model.Address;
			array[14].Value = model.MobileNo;
			array[15].Value = model.Email;
			array[16].Value = model.CultrulName;
			array[17].Value = model.VocationName;
			array[18].Value = model.FinishedNum;
			array[19].Value = model.UnfinishedNum;
			array[20].Value = model.FirstDatePE;
			array[21].Value = model.LatestDatePE;
			array[22].Value = model.ID_ArcCustomer;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ArcCustomer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnArcCust ");
			stringBuilder.Append(" where ID_ArcCustomer=@ID_ArcCustomer");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ArcCustomer;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ArcCustomerlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnArcCust ");
			stringBuilder.Append(" where ID_ArcCustomer in (" + ID_ArcCustomerlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnArcCust GetModel(int ID_ArcCustomer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ArcCustomer,ID_Gender,ID_Marriage,NationID,CultrulID,VocationID,CustomerName,IDCard,ExamCard,Photo,BirthDay,GenderName,MarriageName,NationName,Address,MobileNo,Email,CultrulName,VocationName,FinishedNum,UnfinishedNum,FirstDatePE,LatestDatePE from OnArcCust ");
			stringBuilder.Append(" where ID_ArcCustomer=@ID_ArcCustomer");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ArcCustomer;
			PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnArcCust result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ArcCustomer"].ToString() != "")
				{
					onArcCust.ID_ArcCustomer = int.Parse(dataSet.Tables[0].Rows[0]["ID_ArcCustomer"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Gender"].ToString() != "")
				{
					onArcCust.ID_Gender = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Gender"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Marriage"].ToString() != "")
				{
					onArcCust.ID_Marriage = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Marriage"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["NationID"].ToString() != "")
				{
					onArcCust.NationID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["NationID"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["CultrulID"].ToString() != "")
				{
					onArcCust.CultrulID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["CultrulID"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["VocationID"].ToString() != "")
				{
					onArcCust.VocationID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["VocationID"].ToString()));
				}
				onArcCust.CustomerName = dataSet.Tables[0].Rows[0]["CustomerName"].ToString();
				onArcCust.IDCard = dataSet.Tables[0].Rows[0]["IDCard"].ToString();
				onArcCust.ExamCard = dataSet.Tables[0].Rows[0]["ExamCard"].ToString();
				if (dataSet.Tables[0].Rows[0]["Photo"].ToString() != "")
				{
					onArcCust.Photo = (byte[])dataSet.Tables[0].Rows[0]["Photo"];
				}
				if (dataSet.Tables[0].Rows[0]["BirthDay"].ToString() != "")
				{
					onArcCust.BirthDay = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["BirthDay"].ToString()));
				}
				onArcCust.GenderName = dataSet.Tables[0].Rows[0]["GenderName"].ToString();
				onArcCust.MarriageName = dataSet.Tables[0].Rows[0]["MarriageName"].ToString();
				onArcCust.NationName = dataSet.Tables[0].Rows[0]["NationName"].ToString();
				onArcCust.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
				onArcCust.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
				onArcCust.Email = dataSet.Tables[0].Rows[0]["Email"].ToString();
				onArcCust.CultrulName = dataSet.Tables[0].Rows[0]["CultrulName"].ToString();
				onArcCust.VocationName = dataSet.Tables[0].Rows[0]["VocationName"].ToString();
				if (dataSet.Tables[0].Rows[0]["FinishedNum"].ToString() != "")
				{
					onArcCust.FinishedNum = int.Parse(dataSet.Tables[0].Rows[0]["FinishedNum"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["UnfinishedNum"].ToString() != "")
				{
					onArcCust.UnfinishedNum = new int?(int.Parse(dataSet.Tables[0].Rows[0]["UnfinishedNum"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["FirstDatePE"].ToString() != "")
				{
					onArcCust.FirstDatePE = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["FirstDatePE"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["LatestDatePE"].ToString() != "")
				{
					onArcCust.LatestDatePE = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["LatestDatePE"].ToString()));
				}
				result = onArcCust;
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
			stringBuilder.Append("select ID_ArcCustomer,ID_Gender,ID_Marriage,NationID,CultrulID,VocationID,CustomerName,IDCard,ExamCard,Photo,BirthDay,GenderName,MarriageName,NationName,Address,MobileNo,Email,CultrulName,VocationName,FinishedNum,UnfinishedNum,FirstDatePE,LatestDatePE ");
			stringBuilder.Append(" FROM OnArcCust ");
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
			stringBuilder.Append(" ID_ArcCustomer,ID_Gender,ID_Marriage,NationID,CultrulID,VocationID,CustomerName,IDCard,ExamCard,Photo,BirthDay,GenderName,MarriageName,NationName,Address,MobileNo,Email,CultrulName,VocationName,FinishedNum,UnfinishedNum,FirstDatePE,LatestDatePE ");
			stringBuilder.Append(" FROM OnArcCust ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
