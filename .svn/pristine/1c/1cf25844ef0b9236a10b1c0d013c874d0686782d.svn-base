using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusExamItem : IBusExamItem
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ExamItem", "BusExamItem");
		}

		public bool Exists(int ID_ExamItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusExamItem");
			stringBuilder.Append(" where ID_ExamItem=@ID_ExamItem ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItem;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusExamItem model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusExamItem(");
			stringBuilder.Append("ExamItemName,GetResultWay,ExamItemCode,ID_Section,Is_LisValueNull,Is_EntrySectSum,EntrySectSumLevel,Is_AutoCalc,CalcExpression,SymCols,TextboxRows,Is_SameRow,ExamItemUnit,MaleHiLimit,MaleLoLimit,FemaleHiLimit,FemaleLoLimit,Is_SymMultiValue,InputCode,DispOrder,Note,Forsex,Is_ExamItemNonPrintInReport,ID_ExamItemGroup,AbbrExamName)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ExamItemName,@GetResultWay,@ExamItemCode,@ID_Section,@Is_LisValueNull,@Is_EntrySectSum,@EntrySectSumLevel,@Is_AutoCalc,@CalcExpression,@SymCols,@TextboxRows,@Is_SameRow,@ExamItemUnit,@MaleHiLimit,@MaleLoLimit,@FemaleHiLimit,@FemaleLoLimit,@Is_SymMultiValue,@InputCode,@DispOrder,@Note,@Forsex,@Is_ExamItemNonPrintInReport,@ID_ExamItemGroup,@AbbrExamName)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamItemName", SqlDbType.VarChar, 50),
				new SqlParameter("@GetResultWay", SqlDbType.VarChar, 2),
				new SqlParameter("@ExamItemCode", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@Is_LisValueNull", SqlDbType.Bit, 1),
				new SqlParameter("@Is_EntrySectSum", SqlDbType.Bit, 1),
				new SqlParameter("@EntrySectSumLevel", SqlDbType.Int, 4),
				new SqlParameter("@Is_AutoCalc", SqlDbType.Bit, 1),
				new SqlParameter("@CalcExpression", SqlDbType.Text),
				new SqlParameter("@SymCols", SqlDbType.Int, 4),
				new SqlParameter("@TextboxRows", SqlDbType.Int, 4),
				new SqlParameter("@Is_SameRow", SqlDbType.Bit, 1),
				new SqlParameter("@ExamItemUnit", SqlDbType.VarChar, 20),
				new SqlParameter("@MaleHiLimit", SqlDbType.Float, 8),
				new SqlParameter("@MaleLoLimit", SqlDbType.Float, 8),
				new SqlParameter("@FemaleHiLimit", SqlDbType.Float, 8),
				new SqlParameter("@FemaleLoLimit", SqlDbType.Float, 8),
				new SqlParameter("@Is_SymMultiValue", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 30),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 20),
				new SqlParameter("@Forsex", SqlDbType.Int, 4),
				new SqlParameter("@Is_ExamItemNonPrintInReport", SqlDbType.Bit, 1),
				new SqlParameter("@ID_ExamItemGroup", SqlDbType.Int, 4),
				new SqlParameter("@AbbrExamName", SqlDbType.VarChar, 60)
			};
			array[0].Value = model.ExamItemName;
			array[1].Value = model.GetResultWay;
			array[2].Value = model.ExamItemCode;
			array[3].Value = model.ID_Section;
			array[4].Value = model.Is_LisValueNull;
			array[5].Value = model.Is_EntrySectSum;
			array[6].Value = model.EntrySectSumLevel;
			array[7].Value = model.Is_AutoCalc;
			array[8].Value = model.CalcExpression;
			array[9].Value = model.SymCols;
			array[10].Value = model.TextboxRows;
			array[11].Value = model.Is_SameRow;
			array[12].Value = model.ExamItemUnit;
			array[13].Value = model.MaleHiLimit;
			array[14].Value = model.MaleLoLimit;
			array[15].Value = model.FemaleHiLimit;
			array[16].Value = model.FemaleLoLimit;
			array[17].Value = model.Is_SymMultiValue;
			array[18].Value = model.InputCode;
			array[19].Value = model.DispOrder;
			array[20].Value = model.Note;
			array[21].Value = model.Forsex;
			array[22].Value = model.Is_ExamItemNonPrintInReport;
			array[23].Value = model.ID_ExamItemGroup;
			array[24].Value = model.AbbrExamName;
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

		public bool Update(PEIS.Model.BusExamItem model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusExamItem set ");
			stringBuilder.Append("ExamItemName=@ExamItemName,");
			stringBuilder.Append("GetResultWay=@GetResultWay,");
			stringBuilder.Append("ExamItemCode=@ExamItemCode,");
			stringBuilder.Append("ID_Section=@ID_Section,");
			stringBuilder.Append("Is_LisValueNull=@Is_LisValueNull,");
			stringBuilder.Append("Is_EntrySectSum=@Is_EntrySectSum,");
			stringBuilder.Append("EntrySectSumLevel=@EntrySectSumLevel,");
			stringBuilder.Append("Is_AutoCalc=@Is_AutoCalc,");
			stringBuilder.Append("CalcExpression=@CalcExpression,");
			stringBuilder.Append("SymCols=@SymCols,");
			stringBuilder.Append("TextboxRows=@TextboxRows,");
			stringBuilder.Append("Is_SameRow=@Is_SameRow,");
			stringBuilder.Append("ExamItemUnit=@ExamItemUnit,");
			stringBuilder.Append("MaleHiLimit=@MaleHiLimit,");
			stringBuilder.Append("MaleLoLimit=@MaleLoLimit,");
			stringBuilder.Append("FemaleHiLimit=@FemaleHiLimit,");
			stringBuilder.Append("FemaleLoLimit=@FemaleLoLimit,");
			stringBuilder.Append("Is_SymMultiValue=@Is_SymMultiValue,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("Forsex=@Forsex,");
			stringBuilder.Append("Is_ExamItemNonPrintInReport=@Is_ExamItemNonPrintInReport,");
			stringBuilder.Append("ID_ExamItemGroup=@ID_ExamItemGroup,");
			stringBuilder.Append("AbbrExamName=@AbbrExamName");
			stringBuilder.Append(" where ID_ExamItem=@ID_ExamItem");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ExamItemName", SqlDbType.VarChar, 50),
				new SqlParameter("@GetResultWay", SqlDbType.VarChar, 2),
				new SqlParameter("@ExamItemCode", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Section", SqlDbType.Int, 4),
				new SqlParameter("@Is_LisValueNull", SqlDbType.Bit, 1),
				new SqlParameter("@Is_EntrySectSum", SqlDbType.Bit, 1),
				new SqlParameter("@EntrySectSumLevel", SqlDbType.Int, 4),
				new SqlParameter("@Is_AutoCalc", SqlDbType.Bit, 1),
				new SqlParameter("@CalcExpression", SqlDbType.Text),
				new SqlParameter("@SymCols", SqlDbType.Int, 4),
				new SqlParameter("@TextboxRows", SqlDbType.Int, 4),
				new SqlParameter("@Is_SameRow", SqlDbType.Bit, 1),
				new SqlParameter("@ExamItemUnit", SqlDbType.VarChar, 20),
				new SqlParameter("@MaleHiLimit", SqlDbType.Float, 8),
				new SqlParameter("@MaleLoLimit", SqlDbType.Float, 8),
				new SqlParameter("@FemaleHiLimit", SqlDbType.Float, 8),
				new SqlParameter("@FemaleLoLimit", SqlDbType.Float, 8),
				new SqlParameter("@Is_SymMultiValue", SqlDbType.Bit, 1),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 30),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 20),
				new SqlParameter("@Forsex", SqlDbType.Int, 4),
				new SqlParameter("@Is_ExamItemNonPrintInReport", SqlDbType.Bit, 1),
				new SqlParameter("@ID_ExamItemGroup", SqlDbType.Int, 4),
				new SqlParameter("@AbbrExamName", SqlDbType.VarChar, 60),
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = model.ExamItemName;
			array[1].Value = model.GetResultWay;
			array[2].Value = model.ExamItemCode;
			array[3].Value = model.ID_Section;
			array[4].Value = model.Is_LisValueNull;
			array[5].Value = model.Is_EntrySectSum;
			array[6].Value = model.EntrySectSumLevel;
			array[7].Value = model.Is_AutoCalc;
			array[8].Value = model.CalcExpression;
			array[9].Value = model.SymCols;
			array[10].Value = model.TextboxRows;
			array[11].Value = model.Is_SameRow;
			array[12].Value = model.ExamItemUnit;
			array[13].Value = model.MaleHiLimit;
			array[14].Value = model.MaleLoLimit;
			array[15].Value = model.FemaleHiLimit;
			array[16].Value = model.FemaleLoLimit;
			array[17].Value = model.Is_SymMultiValue;
			array[18].Value = model.InputCode;
			array[19].Value = model.DispOrder;
			array[20].Value = model.Note;
			array[21].Value = model.Forsex;
			array[22].Value = model.Is_ExamItemNonPrintInReport;
			array[23].Value = model.ID_ExamItemGroup;
			array[24].Value = model.AbbrExamName;
			array[25].Value = model.ID_ExamItem;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ExamItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusExamItem ");
			stringBuilder.Append(" where ID_ExamItem=@ID_ExamItem");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItem;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ExamItemlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusExamItem ");
			stringBuilder.Append(" where ID_ExamItem in (" + ID_ExamItemlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusExamItem GetModel(int ID_ExamItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ExamItem,ExamItemName,GetResultWay,ExamItemCode,ID_Section,Is_LisValueNull,Is_EntrySectSum,EntrySectSumLevel,Is_AutoCalc,CalcExpression,SymCols,TextboxRows,Is_SameRow,ExamItemUnit,MaleHiLimit,MaleLoLimit,FemaleHiLimit,FemaleLoLimit,Is_SymMultiValue,InputCode,DispOrder,Note,Forsex,Is_ExamItemNonPrintInReport,ID_ExamItemGroup,AbbrExamName from BusExamItem ");
			stringBuilder.Append(" where ID_ExamItem=@ID_ExamItem");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ExamItem", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ExamItem;
			PEIS.Model.BusExamItem busExamItem = new PEIS.Model.BusExamItem();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusExamItem result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString() != "")
				{
					busExamItem.ID_ExamItem = int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItem"].ToString());
				}
				busExamItem.ExamItemName = dataSet.Tables[0].Rows[0]["ExamItemName"].ToString();
				busExamItem.GetResultWay = dataSet.Tables[0].Rows[0]["GetResultWay"].ToString();
				busExamItem.ExamItemCode = dataSet.Tables[0].Rows[0]["ExamItemCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Section"].ToString() != "")
				{
					busExamItem.ID_Section = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Section"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_LisValueNull"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_LisValueNull"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_LisValueNull"].ToString().ToLower() == "true")
					{
						busExamItem.Is_LisValueNull = new bool?(true);
					}
					else
					{
						busExamItem.Is_LisValueNull = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["Is_EntrySectSum"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_EntrySectSum"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_EntrySectSum"].ToString().ToLower() == "true")
					{
						busExamItem.Is_EntrySectSum = new bool?(true);
					}
					else
					{
						busExamItem.Is_EntrySectSum = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["EntrySectSumLevel"].ToString() != "")
				{
					busExamItem.EntrySectSumLevel = new int?(int.Parse(dataSet.Tables[0].Rows[0]["EntrySectSumLevel"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_AutoCalc"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_AutoCalc"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_AutoCalc"].ToString().ToLower() == "true")
					{
						busExamItem.Is_AutoCalc = new bool?(true);
					}
					else
					{
						busExamItem.Is_AutoCalc = new bool?(false);
					}
				}
				busExamItem.CalcExpression = dataSet.Tables[0].Rows[0]["CalcExpression"].ToString();
				if (dataSet.Tables[0].Rows[0]["SymCols"].ToString() != "")
				{
					busExamItem.SymCols = new int?(int.Parse(dataSet.Tables[0].Rows[0]["SymCols"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["TextboxRows"].ToString() != "")
				{
					busExamItem.TextboxRows = new int?(int.Parse(dataSet.Tables[0].Rows[0]["TextboxRows"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_SameRow"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_SameRow"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_SameRow"].ToString().ToLower() == "true")
					{
						busExamItem.Is_SameRow = new bool?(true);
					}
					else
					{
						busExamItem.Is_SameRow = new bool?(false);
					}
				}
				busExamItem.ExamItemUnit = dataSet.Tables[0].Rows[0]["ExamItemUnit"].ToString();
				if (dataSet.Tables[0].Rows[0]["MaleHiLimit"].ToString() != "")
				{
					busExamItem.MaleHiLimit = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["MaleHiLimit"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["MaleLoLimit"].ToString() != "")
				{
					busExamItem.MaleLoLimit = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["MaleLoLimit"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["FemaleHiLimit"].ToString() != "")
				{
					busExamItem.FemaleHiLimit = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["FemaleHiLimit"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["FemaleLoLimit"].ToString() != "")
				{
					busExamItem.FemaleLoLimit = new decimal?(decimal.Parse(dataSet.Tables[0].Rows[0]["FemaleLoLimit"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_SymMultiValue"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_SymMultiValue"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_SymMultiValue"].ToString().ToLower() == "true")
					{
						busExamItem.Is_SymMultiValue = new bool?(true);
					}
					else
					{
						busExamItem.Is_SymMultiValue = new bool?(false);
					}
				}
				busExamItem.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busExamItem.DispOrder = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString()));
				}
				busExamItem.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				if (dataSet.Tables[0].Rows[0]["Forsex"].ToString() != "")
				{
					busExamItem.Forsex = new int?(int.Parse(dataSet.Tables[0].Rows[0]["Forsex"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_ExamItemNonPrintInReport"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_ExamItemNonPrintInReport"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_ExamItemNonPrintInReport"].ToString().ToLower() == "true")
					{
						busExamItem.Is_ExamItemNonPrintInReport = new bool?(true);
					}
					else
					{
						busExamItem.Is_ExamItemNonPrintInReport = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_ExamItemGroup"].ToString() != "")
				{
					busExamItem.ID_ExamItemGroup = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_ExamItemGroup"].ToString()));
				}
				busExamItem.AbbrExamName = dataSet.Tables[0].Rows[0]["AbbrExamName"].ToString();
				result = busExamItem;
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
			stringBuilder.Append("select ID_ExamItem,ExamItemName,GetResultWay,ExamItemCode,ID_Section,Is_LisValueNull,Is_EntrySectSum,EntrySectSumLevel,Is_AutoCalc,CalcExpression,SymCols,TextboxRows,Is_SameRow,ExamItemUnit,MaleHiLimit,MaleLoLimit,FemaleHiLimit,FemaleLoLimit,Is_SymMultiValue,InputCode,DispOrder,Note,Forsex,Is_ExamItemNonPrintInReport,ID_ExamItemGroup,AbbrExamName ");
			stringBuilder.Append(" FROM BusExamItem ");
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
			stringBuilder.Append(" ID_ExamItem,ExamItemName,GetResultWay,ExamItemCode,ID_Section,Is_LisValueNull,Is_EntrySectSum,EntrySectSumLevel,Is_AutoCalc,CalcExpression,SymCols,TextboxRows,Is_SameRow,ExamItemUnit,MaleHiLimit,MaleLoLimit,FemaleHiLimit,FemaleLoLimit,Is_SymMultiValue,InputCode,DispOrder,Note,Forsex,Is_ExamItemNonPrintInReport,ID_ExamItemGroup,AbbrExamName ");
			stringBuilder.Append(" FROM BusExamItem ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
