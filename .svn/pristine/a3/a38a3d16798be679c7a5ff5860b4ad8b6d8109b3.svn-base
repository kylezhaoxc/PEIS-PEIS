using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustFee : IOnCustFee
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_CustFee", "OnCustFee");
		}

		public bool Exists(int ID_CustFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustFee");
			stringBuilder.Append(" where ID_CustFee=@ID_CustFee ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustFee;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustFee model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustFee(");
			stringBuilder.Append("ID_Customer,FeeItemName,ID_Register,RegisterName,RegistDate,OriginalPrice,Discount,FactPrice,ID_Discounter,DiscounterName,Is_FeeCharged,ID_FeeCharger,FeeCharger,FeeChargeTime,Is_FeeRefund,ID_FeeRefunder,FeeRefunder,Is_Examined,ID_ExamDoctor,ExamDoctorName,ExamDate,Is_Discard,ID_FeeType,ID_Fee)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_Customer,@FeeItemName,@ID_Register,@RegisterName,@RegistDate,@OriginalPrice,@Discount,@FactPrice,@ID_Discounter,@DiscounterName,@Is_FeeCharged,@ID_FeeCharger,@FeeCharger,@FeeChargeTime,@Is_FeeRefund,@ID_FeeRefunder,@FeeRefunder,@Is_Examined,@ID_ExamDoctor,@ExamDoctorName,@ExamDate,@Is_Discard,@ID_FeeType,@ID_Fee)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@FeeItemName", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Register", SqlDbType.Int, 4),
				new SqlParameter("@RegisterName", SqlDbType.VarChar, 30),
				new SqlParameter("@RegistDate", SqlDbType.DateTime),
				new SqlParameter("@OriginalPrice", SqlDbType.Float, 8),
				new SqlParameter("@Discount", SqlDbType.Int, 4),
				new SqlParameter("@FactPrice", SqlDbType.Float, 8),
				new SqlParameter("@ID_Discounter", SqlDbType.Int, 4),
				new SqlParameter("@DiscounterName", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_FeeCharged", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeCharger", SqlDbType.Int, 4),
				new SqlParameter("@FeeCharger", SqlDbType.VarChar, 30),
				new SqlParameter("@FeeChargeTime", SqlDbType.DateTime),
				new SqlParameter("@Is_FeeRefund", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeRefunder", SqlDbType.Int, 4),
				new SqlParameter("@FeeRefunder", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_Examined", SqlDbType.Bit, 1),
				new SqlParameter("@ID_ExamDoctor", SqlDbType.Int, 4),
				new SqlParameter("@ExamDoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Discard", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeType", SqlDbType.Int, 4),
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.FeeItemName;
			array[2].Value = model.ID_Register;
			array[3].Value = model.RegisterName;
			array[4].Value = model.RegistDate;
			array[5].Value = model.OriginalPrice;
			array[6].Value = model.Discount;
			array[7].Value = model.FactPrice;
			array[8].Value = model.ID_Discounter;
			array[9].Value = model.DiscounterName;
			array[10].Value = model.Is_FeeCharged;
			array[11].Value = model.ID_FeeCharger;
			array[12].Value = model.FeeCharger;
			array[13].Value = model.FeeChargeTime;
			array[14].Value = model.Is_FeeRefund;
			array[15].Value = model.ID_FeeRefunder;
			array[16].Value = model.FeeRefunder;
			array[17].Value = model.Is_Examined;
			array[18].Value = model.ID_ExamDoctor;
			array[19].Value = model.ExamDoctorName;
			array[20].Value = model.ExamDate;
			array[21].Value = model.Is_Discard;
			array[22].Value = model.ID_FeeType;
			array[23].Value = model.ID_Fee;
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

		public bool Update(PEIS.Model.OnCustFee model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustFee set ");
			stringBuilder.Append("ID_Customer=@ID_Customer,");
			stringBuilder.Append("FeeItemName=@FeeItemName,");
			stringBuilder.Append("ID_Register=@ID_Register,");
			stringBuilder.Append("RegisterName=@RegisterName,");
			stringBuilder.Append("RegistDate=@RegistDate,");
			stringBuilder.Append("OriginalPrice=@OriginalPrice,");
			stringBuilder.Append("Discount=@Discount,");
			stringBuilder.Append("FactPrice=@FactPrice,");
			stringBuilder.Append("ID_Discounter=@ID_Discounter,");
			stringBuilder.Append("DiscounterName=@DiscounterName,");
			stringBuilder.Append("Is_FeeCharged=@Is_FeeCharged,");
			stringBuilder.Append("ID_FeeCharger=@ID_FeeCharger,");
			stringBuilder.Append("FeeCharger=@FeeCharger,");
			stringBuilder.Append("FeeChargeTime=@FeeChargeTime,");
			stringBuilder.Append("Is_FeeRefund=@Is_FeeRefund,");
			stringBuilder.Append("ID_FeeRefunder=@ID_FeeRefunder,");
			stringBuilder.Append("FeeRefunder=@FeeRefunder,");
			stringBuilder.Append("Is_Examined=@Is_Examined,");
			stringBuilder.Append("ID_ExamDoctor=@ID_ExamDoctor,");
			stringBuilder.Append("ExamDoctorName=@ExamDoctorName,");
			stringBuilder.Append("ExamDate=@ExamDate,");
			stringBuilder.Append("Is_Discard=@Is_Discard,");
			stringBuilder.Append("ID_FeeType=@ID_FeeType,");
			stringBuilder.Append("ID_Fee=@ID_Fee");
			stringBuilder.Append(" where ID_CustFee=@ID_CustFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Customer", SqlDbType.BigInt, 8),
				new SqlParameter("@FeeItemName", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Register", SqlDbType.Int, 4),
				new SqlParameter("@RegisterName", SqlDbType.VarChar, 30),
				new SqlParameter("@RegistDate", SqlDbType.DateTime),
				new SqlParameter("@OriginalPrice", SqlDbType.Float, 8),
				new SqlParameter("@Discount", SqlDbType.Int, 4),
				new SqlParameter("@FactPrice", SqlDbType.Float, 8),
				new SqlParameter("@ID_Discounter", SqlDbType.Int, 4),
				new SqlParameter("@DiscounterName", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_FeeCharged", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeCharger", SqlDbType.Int, 4),
				new SqlParameter("@FeeCharger", SqlDbType.VarChar, 30),
				new SqlParameter("@FeeChargeTime", SqlDbType.DateTime),
				new SqlParameter("@Is_FeeRefund", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeRefunder", SqlDbType.Int, 4),
				new SqlParameter("@FeeRefunder", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_Examined", SqlDbType.Bit, 1),
				new SqlParameter("@ID_ExamDoctor", SqlDbType.Int, 4),
				new SqlParameter("@ExamDoctorName", SqlDbType.VarChar, 30),
				new SqlParameter("@ExamDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Discard", SqlDbType.Bit, 1),
				new SqlParameter("@ID_FeeType", SqlDbType.Int, 4),
				new SqlParameter("@ID_Fee", SqlDbType.Int, 4),
				new SqlParameter("@ID_CustFee", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_Customer;
			array[1].Value = model.FeeItemName;
			array[2].Value = model.ID_Register;
			array[3].Value = model.RegisterName;
			array[4].Value = model.RegistDate;
			array[5].Value = model.OriginalPrice;
			array[6].Value = model.Discount;
			array[7].Value = model.FactPrice;
			array[8].Value = model.ID_Discounter;
			array[9].Value = model.DiscounterName;
			array[10].Value = model.Is_FeeCharged;
			array[11].Value = model.ID_FeeCharger;
			array[12].Value = model.FeeCharger;
			array[13].Value = model.FeeChargeTime;
			array[14].Value = model.Is_FeeRefund;
			array[15].Value = model.ID_FeeRefunder;
			array[16].Value = model.FeeRefunder;
			array[17].Value = model.Is_Examined;
			array[18].Value = model.ID_ExamDoctor;
			array[19].Value = model.ExamDoctorName;
			array[20].Value = model.ExamDate;
			array[21].Value = model.Is_Discard;
			array[22].Value = model.ID_FeeType;
			array[23].Value = model.ID_Fee;
			array[24].Value = model.ID_CustFee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_CustFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustFee ");
			stringBuilder.Append(" where ID_CustFee=@ID_CustFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustFee;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_CustFeelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustFee ");
			stringBuilder.Append(" where ID_CustFee in (" + ID_CustFeelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustFee GetModel(int ID_CustFee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_CustFee,ID_Customer,FeeItemName,ID_Register,RegisterName,RegistDate,OriginalPrice,Discount,FactPrice,ID_Discounter,DiscounterName,Is_FeeCharged,ID_FeeCharger,FeeCharger,FeeChargeTime,Is_FeeRefund,ID_FeeRefunder,FeeRefunder,Is_Examined,ID_ExamDoctor,ExamDoctorName,ExamDate,Is_Discard,ID_FeeType,ID_Fee from OnCustFee ");
			stringBuilder.Append(" where ID_CustFee=@ID_CustFee");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_CustFee", SqlDbType.Int, 4)
			};
			array[0].Value = ID_CustFee;
			PEIS.Model.OnCustFee onCustFee = new PEIS.Model.OnCustFee();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustFee result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_CustFee"].ToString() != "")
				{
					onCustFee.ID_CustFee = int.Parse(dataSet.Tables[0].Rows[0]["ID_CustFee"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Customer"].ToString() != "")
				{
					onCustFee.ID_Customer = new long?(long.Parse(dataSet.Tables[0].Rows[0]["ID_Customer"].ToString()));
				}
				onCustFee.FeeItemName = dataSet.Tables[0].Rows[0]["FeeItemName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Register"].ToString() != "")
				{
					onCustFee.ID_Register = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Register"].ToString()));
				}
				onCustFee.RegisterName = dataSet.Tables[0].Rows[0]["RegisterName"].ToString();
				if (dataSet.Tables[0].Rows[0]["RegistDate"].ToString() != "")
				{
					onCustFee.RegistDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["RegistDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["OriginalPrice"].ToString() != "")
				{
					onCustFee.OriginalPrice = decimal.Parse(dataSet.Tables[0].Rows[0]["OriginalPrice"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["Discount"].ToString() != "")
				{
					onCustFee.Discount = int.Parse(dataSet.Tables[0].Rows[0]["Discount"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["FactPrice"].ToString() != "")
				{
					onCustFee.FactPrice = decimal.Parse(dataSet.Tables[0].Rows[0]["FactPrice"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_Discounter"].ToString() != "")
				{
					onCustFee.ID_Discounter = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Discounter"].ToString()));
				}
				onCustFee.DiscounterName = dataSet.Tables[0].Rows[0]["DiscounterName"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_FeeCharged"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_FeeCharged"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_FeeCharged"].ToString().ToLower() == "true")
					{
						onCustFee.Is_FeeCharged = true;
					}
					else
					{
						onCustFee.Is_FeeCharged = false;
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_FeeCharger"].ToString() != "")
				{
					onCustFee.ID_FeeCharger = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FeeCharger"].ToString()));
				}
				onCustFee.FeeCharger = dataSet.Tables[0].Rows[0]["FeeCharger"].ToString();
				if (dataSet.Tables[0].Rows[0]["FeeChargeTime"].ToString() != "")
				{
					onCustFee.FeeChargeTime = DateTime.Parse(dataSet.Tables[0].Rows[0]["FeeChargeTime"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["Is_FeeRefund"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_FeeRefund"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_FeeRefund"].ToString().ToLower() == "true")
					{
						onCustFee.Is_FeeRefund = new bool?(true);
					}
					else
					{
						onCustFee.Is_FeeRefund = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_FeeRefunder"].ToString() != "")
				{
					onCustFee.ID_FeeRefunder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FeeRefunder"].ToString()));
				}
				onCustFee.FeeRefunder = dataSet.Tables[0].Rows[0]["FeeRefunder"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_Examined"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Examined"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Examined"].ToString().ToLower() == "true")
					{
						onCustFee.Is_Examined = new bool?(true);
					}
					else
					{
						onCustFee.Is_Examined = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_ExamDoctor"].ToString() != "")
				{
					onCustFee.ID_ExamDoctor = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamDoctor"].ToString()));
				}
				onCustFee.ExamDoctorName = dataSet.Tables[0].Rows[0]["ExamDoctorName"].ToString();
				if (dataSet.Tables[0].Rows[0]["ExamDate"].ToString() != "")
				{
					onCustFee.ExamDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["ExamDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Discard"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Discard"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Discard"].ToString().ToLower() == "true")
					{
						onCustFee.Is_Discard = new bool?(true);
					}
					else
					{
						onCustFee.Is_Discard = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_FeeType"].ToString() != "")
				{
					onCustFee.ID_FeeType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_FeeType"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_Fee"].ToString() != "")
				{
					onCustFee.ID_Fee = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Fee"].ToString()));
				}
				result = onCustFee;
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
			stringBuilder.Append("select ID_CustFee,ID_Customer,FeeItemName,ID_Register,RegisterName,RegistDate,OriginalPrice,Discount,FactPrice,ID_Discounter,DiscounterName,Is_FeeCharged,ID_FeeCharger,FeeCharger,FeeChargeTime,Is_FeeRefund,ID_FeeRefunder,FeeRefunder,Is_Examined,ID_ExamDoctor,ExamDoctorName,ExamDate,Is_Discard,ID_FeeType,ID_Fee ");
			stringBuilder.Append(" FROM OnCustFee ");
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
			stringBuilder.Append(" ID_CustFee,ID_Customer,FeeItemName,ID_Register,RegisterName,RegistDate,OriginalPrice,Discount,FactPrice,ID_Discounter,DiscounterName,Is_FeeCharged,ID_FeeCharger,FeeCharger,FeeChargeTime,Is_FeeRefund,ID_FeeRefunder,FeeRefunder,Is_Examined,ID_ExamDoctor,ExamDoctorName,ExamDate,Is_Discard,ID_FeeType,ID_Fee ");
			stringBuilder.Append(" FROM OnCustFee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
