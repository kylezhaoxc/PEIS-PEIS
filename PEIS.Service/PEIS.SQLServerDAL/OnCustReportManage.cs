using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class OnCustReportManage : IOnCustReportManage
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ReportManage", "OnCustReportManage");
		}

		public bool Exists(int ID_ReportManage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from OnCustReportManage");
			stringBuilder.Append(" where ID_ReportManage=@ID_ReportManage ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ReportManage", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ReportManage;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.OnCustReportManage model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into OnCustReportManage(");
			stringBuilder.Append("ID_ReportWay,ID_ReprotWay,ReportWay,Is_Informed,Informer,InformedDate,Is_InformReturned,Is_ReportReceipted,Is_SelfReceipted,ReportReceiptor,ReportReceiptedDate,ID_ReportOffer,ReportOffer,Is_ReportPrinted,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,ID_ReportChecker,ReportChecker,ReportCheckDate,ReportPosition)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID_ReportWay,@ID_ReprotWay,@ReportWay,@Is_Informed,@Informer,@InformedDate,@Is_InformReturned,@Is_ReportReceipted,@Is_SelfReceipted,@ReportReceiptor,@ReportReceiptedDate,@ID_ReportOffer,@ReportOffer,@Is_ReportPrinted,@ID_ReportPrinter,@ReportPrinter,@ReportPrintedDate,@ID_ReportChecker,@ReportChecker,@ReportCheckDate,@ReportPosition)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ReportWay", SqlDbType.Int, 4),
				new SqlParameter("@ID_ReprotWay", SqlDbType.Int, 4),
				new SqlParameter("@ReportWay", SqlDbType.VarChar, 50),
				new SqlParameter("@Is_Informed", SqlDbType.Bit, 1),
				new SqlParameter("@Informer", SqlDbType.VarChar, 30),
				new SqlParameter("@InformedDate", SqlDbType.DateTime),
				new SqlParameter("@Is_InformReturned", SqlDbType.Bit, 1),
				new SqlParameter("@Is_ReportReceipted", SqlDbType.Bit, 1),
				new SqlParameter("@Is_SelfReceipted", SqlDbType.Bit, 1),
				new SqlParameter("@ReportReceiptor", SqlDbType.VarChar, 30),
				new SqlParameter("@ReportReceiptedDate", SqlDbType.DateTime),
				new SqlParameter("@ID_ReportOffer", SqlDbType.Int, 4),
				new SqlParameter("@ReportOffer", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_ReportPrinted", SqlDbType.Bit, 1),
				new SqlParameter("@ID_ReportPrinter", SqlDbType.Int, 4),
				new SqlParameter("@ReportPrinter", SqlDbType.VarChar, 30),
				new SqlParameter("@ReportPrintedDate", SqlDbType.DateTime),
				new SqlParameter("@ID_ReportChecker", SqlDbType.Int, 4),
				new SqlParameter("@ReportChecker", SqlDbType.VarChar, 30),
				new SqlParameter("@ReportCheckDate", SqlDbType.DateTime),
				new SqlParameter("@ReportPosition", SqlDbType.VarChar, 30)
			};
			array[0].Value = model.ID_ReportWay;
			array[1].Value = model.ID_ReprotWay;
			array[2].Value = model.ReportWay;
			array[3].Value = model.Is_Informed;
			array[4].Value = model.Informer;
			array[5].Value = model.InformedDate;
			array[6].Value = model.Is_InformReturned;
			array[7].Value = model.Is_ReportReceipted;
			array[8].Value = model.Is_SelfReceipted;
			array[9].Value = model.ReportReceiptor;
			array[10].Value = model.ReportReceiptedDate;
			array[11].Value = model.ID_ReportOffer;
			array[12].Value = model.ReportOffer;
			array[13].Value = model.Is_ReportPrinted;
			array[14].Value = model.ID_ReportPrinter;
			array[15].Value = model.ReportPrinter;
			array[16].Value = model.ReportPrintedDate;
			array[17].Value = model.ID_ReportChecker;
			array[18].Value = model.ReportChecker;
			array[19].Value = model.ReportCheckDate;
			array[20].Value = model.ReportPosition;
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

		public bool Update(PEIS.Model.OnCustReportManage model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnCustReportManage set ");
			stringBuilder.Append("ID_ReportWay=@ID_ReportWay,");
			stringBuilder.Append("ID_ReprotWay=@ID_ReprotWay,");
			stringBuilder.Append("ReportWay=@ReportWay,");
			stringBuilder.Append("Is_Informed=@Is_Informed,");
			stringBuilder.Append("Informer=@Informer,");
			stringBuilder.Append("InformedDate=@InformedDate,");
			stringBuilder.Append("Is_InformReturned=@Is_InformReturned,");
			stringBuilder.Append("Is_ReportReceipted=@Is_ReportReceipted,");
			stringBuilder.Append("Is_SelfReceipted=@Is_SelfReceipted,");
			stringBuilder.Append("ReportReceiptor=@ReportReceiptor,");
			stringBuilder.Append("ReportReceiptedDate=@ReportReceiptedDate,");
			stringBuilder.Append("ID_ReportOffer=@ID_ReportOffer,");
			stringBuilder.Append("ReportOffer=@ReportOffer,");
			stringBuilder.Append("Is_ReportPrinted=@Is_ReportPrinted,");
			stringBuilder.Append("ID_ReportPrinter=@ID_ReportPrinter,");
			stringBuilder.Append("ReportPrinter=@ReportPrinter,");
			stringBuilder.Append("ReportPrintedDate=@ReportPrintedDate,");
			stringBuilder.Append("ID_ReportChecker=@ID_ReportChecker,");
			stringBuilder.Append("ReportChecker=@ReportChecker,");
			stringBuilder.Append("ReportCheckDate=@ReportCheckDate,");
			stringBuilder.Append("ReportPosition=@ReportPosition");
			stringBuilder.Append(" where ID_ReportManage=@ID_ReportManage");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ReportWay", SqlDbType.Int, 4),
				new SqlParameter("@ID_ReprotWay", SqlDbType.Int, 4),
				new SqlParameter("@ReportWay", SqlDbType.VarChar, 50),
				new SqlParameter("@Is_Informed", SqlDbType.Bit, 1),
				new SqlParameter("@Informer", SqlDbType.VarChar, 30),
				new SqlParameter("@InformedDate", SqlDbType.DateTime),
				new SqlParameter("@Is_InformReturned", SqlDbType.Bit, 1),
				new SqlParameter("@Is_ReportReceipted", SqlDbType.Bit, 1),
				new SqlParameter("@Is_SelfReceipted", SqlDbType.Bit, 1),
				new SqlParameter("@ReportReceiptor", SqlDbType.VarChar, 30),
				new SqlParameter("@ReportReceiptedDate", SqlDbType.DateTime),
				new SqlParameter("@ID_ReportOffer", SqlDbType.Int, 4),
				new SqlParameter("@ReportOffer", SqlDbType.VarChar, 30),
				new SqlParameter("@Is_ReportPrinted", SqlDbType.Bit, 1),
				new SqlParameter("@ID_ReportPrinter", SqlDbType.Int, 4),
				new SqlParameter("@ReportPrinter", SqlDbType.VarChar, 30),
				new SqlParameter("@ReportPrintedDate", SqlDbType.DateTime),
				new SqlParameter("@ID_ReportChecker", SqlDbType.Int, 4),
				new SqlParameter("@ReportChecker", SqlDbType.VarChar, 30),
				new SqlParameter("@ReportCheckDate", SqlDbType.DateTime),
				new SqlParameter("@ReportPosition", SqlDbType.VarChar, 30),
				new SqlParameter("@ID_ReportManage", SqlDbType.Int, 4)
			};
			array[0].Value = model.ID_ReportWay;
			array[1].Value = model.ID_ReprotWay;
			array[2].Value = model.ReportWay;
			array[3].Value = model.Is_Informed;
			array[4].Value = model.Informer;
			array[5].Value = model.InformedDate;
			array[6].Value = model.Is_InformReturned;
			array[7].Value = model.Is_ReportReceipted;
			array[8].Value = model.Is_SelfReceipted;
			array[9].Value = model.ReportReceiptor;
			array[10].Value = model.ReportReceiptedDate;
			array[11].Value = model.ID_ReportOffer;
			array[12].Value = model.ReportOffer;
			array[13].Value = model.Is_ReportPrinted;
			array[14].Value = model.ID_ReportPrinter;
			array[15].Value = model.ReportPrinter;
			array[16].Value = model.ReportPrintedDate;
			array[17].Value = model.ID_ReportChecker;
			array[18].Value = model.ReportChecker;
			array[19].Value = model.ReportCheckDate;
			array[20].Value = model.ReportPosition;
			array[21].Value = model.ID_ReportManage;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ReportManage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustReportManage ");
			stringBuilder.Append(" where ID_ReportManage=@ID_ReportManage");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ReportManage", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ReportManage;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ReportManagelist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from OnCustReportManage ");
			stringBuilder.Append(" where ID_ReportManage in (" + ID_ReportManagelist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.OnCustReportManage GetModel(int ID_ReportManage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ReportManage,ID_ReportWay,ID_ReprotWay,ReportWay,Is_Informed,Informer,InformedDate,Is_InformReturned,Is_ReportReceipted,Is_SelfReceipted,ReportReceiptor,ReportReceiptedDate,ID_ReportOffer,ReportOffer,Is_ReportPrinted,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,ID_ReportChecker,ReportChecker,ReportCheckDate,ReportPosition from OnCustReportManage ");
			stringBuilder.Append(" where ID_ReportManage=@ID_ReportManage");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ReportManage", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ReportManage;
			PEIS.Model.OnCustReportManage onCustReportManage = new PEIS.Model.OnCustReportManage();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.OnCustReportManage result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ReportManage"].ToString() != "")
				{
					onCustReportManage.ID_ReportManage = int.Parse(dataSet.Tables[0].Rows[0]["ID_ReportManage"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["ID_ReportWay"].ToString() != "")
				{
					onCustReportManage.ID_ReportWay = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ReportWay"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_ReprotWay"].ToString() != "")
				{
					onCustReportManage.ID_ReprotWay = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ReprotWay"].ToString()));
				}
				onCustReportManage.ReportWay = dataSet.Tables[0].Rows[0]["ReportWay"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_Informed"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Informed"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Informed"].ToString().ToLower() == "true")
					{
						onCustReportManage.Is_Informed = new bool?(true);
					}
					else
					{
						onCustReportManage.Is_Informed = new bool?(false);
					}
				}
				onCustReportManage.Informer = dataSet.Tables[0].Rows[0]["Informer"].ToString();
				if (dataSet.Tables[0].Rows[0]["InformedDate"].ToString() != "")
				{
					onCustReportManage.InformedDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["InformedDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_InformReturned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_InformReturned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_InformReturned"].ToString().ToLower() == "true")
					{
						onCustReportManage.Is_InformReturned = new bool?(true);
					}
					else
					{
						onCustReportManage.Is_InformReturned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["Is_ReportReceipted"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_ReportReceipted"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_ReportReceipted"].ToString().ToLower() == "true")
					{
						onCustReportManage.Is_ReportReceipted = new bool?(true);
					}
					else
					{
						onCustReportManage.Is_ReportReceipted = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["Is_SelfReceipted"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_SelfReceipted"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_SelfReceipted"].ToString().ToLower() == "true")
					{
						onCustReportManage.Is_SelfReceipted = new bool?(true);
					}
					else
					{
						onCustReportManage.Is_SelfReceipted = new bool?(false);
					}
				}
				onCustReportManage.ReportReceiptor = dataSet.Tables[0].Rows[0]["ReportReceiptor"].ToString();
				if (dataSet.Tables[0].Rows[0]["ReportReceiptedDate"].ToString() != "")
				{
					onCustReportManage.ReportReceiptedDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["ReportReceiptedDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_ReportOffer"].ToString() != "")
				{
					onCustReportManage.ID_ReportOffer = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ReportOffer"].ToString()));
				}
				onCustReportManage.ReportOffer = dataSet.Tables[0].Rows[0]["ReportOffer"].ToString();
				if (dataSet.Tables[0].Rows[0]["Is_ReportPrinted"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_ReportPrinted"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_ReportPrinted"].ToString().ToLower() == "true")
					{
						onCustReportManage.Is_ReportPrinted = new bool?(true);
					}
					else
					{
						onCustReportManage.Is_ReportPrinted = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_ReportPrinter"].ToString() != "")
				{
					onCustReportManage.ID_ReportPrinter = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ReportPrinter"].ToString()));
				}
				onCustReportManage.ReportPrinter = dataSet.Tables[0].Rows[0]["ReportPrinter"].ToString();
				if (dataSet.Tables[0].Rows[0]["ReportPrintedDate"].ToString() != "")
				{
					onCustReportManage.ReportPrintedDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["ReportPrintedDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["ID_ReportChecker"].ToString() != "")
				{
					onCustReportManage.ID_ReportChecker = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ReportChecker"].ToString()));
				}
				onCustReportManage.ReportChecker = dataSet.Tables[0].Rows[0]["ReportChecker"].ToString();
				if (dataSet.Tables[0].Rows[0]["ReportCheckDate"].ToString() != "")
				{
					onCustReportManage.ReportCheckDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["ReportCheckDate"].ToString()));
				}
				onCustReportManage.ReportPosition = dataSet.Tables[0].Rows[0]["ReportPosition"].ToString();
				result = onCustReportManage;
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
			stringBuilder.Append("select ID_ReportManage,ID_ReportWay,ID_ReprotWay,ReportWay,Is_Informed,Informer,InformedDate,Is_InformReturned,Is_ReportReceipted,Is_SelfReceipted,ReportReceiptor,ReportReceiptedDate,ID_ReportOffer,ReportOffer,Is_ReportPrinted,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,ID_ReportChecker,ReportChecker,ReportCheckDate,ReportPosition ");
			stringBuilder.Append(" FROM OnCustReportManage ");
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
			stringBuilder.Append(" ID_ReportManage,ID_ReportWay,ID_ReprotWay,ReportWay,Is_Informed,Informer,InformedDate,Is_InformReturned,Is_ReportReceipted,Is_SelfReceipted,ReportReceiptor,ReportReceiptedDate,ID_ReportOffer,ReportOffer,Is_ReportPrinted,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,ID_ReportChecker,ReportChecker,ReportCheckDate,ReportPosition ");
			stringBuilder.Append(" FROM OnCustReportManage ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
