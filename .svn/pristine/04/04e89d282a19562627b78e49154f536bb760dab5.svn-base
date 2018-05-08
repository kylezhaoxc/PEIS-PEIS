using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class DctICDTen : IDctICDTen
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_ICD", "DctICDTen");
		}

		public bool Exists(int ID_ICD)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from DctICDTen");
			stringBuilder.Append(" where ID_ICD=@ID_ICD ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ICD", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ICD;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.DctICDTen model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into DctICDTen(");
			stringBuilder.Append("ICDCNName,ICDENName,Code,Codea,ID_Creator,Creator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,LevelA,LevelB,LevelC,LevelD,LevelE,LevelTree,Class,Tag,ICDtoSection,Note,InputCode)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ICDCNName,@ICDENName,@Code,@Codea,@ID_Creator,@Creator,@CreateDate,@Is_Banned,@ID_BanOpr,@BanOperator,@BanDate,@BanDescribe,@LevelA,@LevelB,@LevelC,@LevelD,@LevelE,@LevelTree,@Class,@Tag,@ICDtoSection,@Note,@InputCode)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ICDCNName", SqlDbType.VarChar, 100),
				new SqlParameter("@ICDENName", SqlDbType.VarChar, 100),
				new SqlParameter("@Code", SqlDbType.VarChar, 50),
				new SqlParameter("@Codea", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Creator", SqlDbType.Int, 4),
				new SqlParameter("@Creator", SqlDbType.VarChar, 30),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 100),
				new SqlParameter("@LevelA", SqlDbType.Int, 4),
				new SqlParameter("@LevelB", SqlDbType.Int, 4),
				new SqlParameter("@LevelC", SqlDbType.Int, 4),
				new SqlParameter("@LevelD", SqlDbType.Int, 4),
				new SqlParameter("@LevelE", SqlDbType.Int, 4),
				new SqlParameter("@LevelTree", SqlDbType.Int, 4),
				new SqlParameter("@Class", SqlDbType.VarChar, 50),
				new SqlParameter("@Tag", SqlDbType.VarChar, 50),
				new SqlParameter("@ICDtoSection", SqlDbType.VarChar, 100),
				new SqlParameter("@Note", SqlDbType.VarChar, 100),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 50)
			};
			array[0].Value = model.ICDCNName;
			array[1].Value = model.ICDENName;
			array[2].Value = model.Code;
			array[3].Value = model.Codea;
			array[4].Value = model.ID_Creator;
			array[5].Value = model.Creator;
			array[6].Value = model.CreateDate;
			array[7].Value = model.Is_Banned;
			array[8].Value = model.ID_BanOpr;
			array[9].Value = model.BanOperator;
			array[10].Value = model.BanDate;
			array[11].Value = model.BanDescribe;
			array[12].Value = model.LevelA;
			array[13].Value = model.LevelB;
			array[14].Value = model.LevelC;
			array[15].Value = model.LevelD;
			array[16].Value = model.LevelE;
			array[17].Value = model.LevelTree;
			array[18].Value = model.Class;
			array[19].Value = model.Tag;
			array[20].Value = model.ICDtoSection;
			array[21].Value = model.Note;
			array[22].Value = model.InputCode;
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

		public bool Update(PEIS.Model.DctICDTen model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update DctICDTen set ");
			stringBuilder.Append("ICDCNName=@ICDCNName,");
			stringBuilder.Append("ICDENName=@ICDENName,");
			stringBuilder.Append("Code=@Code,");
			stringBuilder.Append("Codea=@Codea,");
			stringBuilder.Append("ID_Creator=@ID_Creator,");
			stringBuilder.Append("Creator=@Creator,");
			stringBuilder.Append("CreateDate=@CreateDate,");
			stringBuilder.Append("Is_Banned=@Is_Banned,");
			stringBuilder.Append("ID_BanOpr=@ID_BanOpr,");
			stringBuilder.Append("BanOperator=@BanOperator,");
			stringBuilder.Append("BanDate=@BanDate,");
			stringBuilder.Append("BanDescribe=@BanDescribe,");
			stringBuilder.Append("LevelA=@LevelA,");
			stringBuilder.Append("LevelB=@LevelB,");
			stringBuilder.Append("LevelC=@LevelC,");
			stringBuilder.Append("LevelD=@LevelD,");
			stringBuilder.Append("LevelE=@LevelE,");
			stringBuilder.Append("LevelTree=@LevelTree,");
			stringBuilder.Append("Class=@Class,");
			stringBuilder.Append("Tag=@Tag,");
			stringBuilder.Append("ICDtoSection=@ICDtoSection,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("InputCode=@InputCode");
			stringBuilder.Append(" where ID_ICD=@ID_ICD");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ICDCNName", SqlDbType.VarChar, 100),
				new SqlParameter("@ICDENName", SqlDbType.VarChar, 100),
				new SqlParameter("@Code", SqlDbType.VarChar, 50),
				new SqlParameter("@Codea", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Creator", SqlDbType.Int, 4),
				new SqlParameter("@Creator", SqlDbType.VarChar, 30),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@Is_Banned", SqlDbType.Bit, 1),
				new SqlParameter("@ID_BanOpr", SqlDbType.Int, 4),
				new SqlParameter("@BanOperator", SqlDbType.VarChar, 30),
				new SqlParameter("@BanDate", SqlDbType.DateTime),
				new SqlParameter("@BanDescribe", SqlDbType.VarChar, 100),
				new SqlParameter("@LevelA", SqlDbType.Int, 4),
				new SqlParameter("@LevelB", SqlDbType.Int, 4),
				new SqlParameter("@LevelC", SqlDbType.Int, 4),
				new SqlParameter("@LevelD", SqlDbType.Int, 4),
				new SqlParameter("@LevelE", SqlDbType.Int, 4),
				new SqlParameter("@LevelTree", SqlDbType.Int, 4),
				new SqlParameter("@Class", SqlDbType.VarChar, 50),
				new SqlParameter("@Tag", SqlDbType.VarChar, 50),
				new SqlParameter("@ICDtoSection", SqlDbType.VarChar, 100),
				new SqlParameter("@Note", SqlDbType.VarChar, 100),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_ICD", SqlDbType.Int, 4)
			};
			array[0].Value = model.ICDCNName;
			array[1].Value = model.ICDENName;
			array[2].Value = model.Code;
			array[3].Value = model.Codea;
			array[4].Value = model.ID_Creator;
			array[5].Value = model.Creator;
			array[6].Value = model.CreateDate;
			array[7].Value = model.Is_Banned;
			array[8].Value = model.ID_BanOpr;
			array[9].Value = model.BanOperator;
			array[10].Value = model.BanDate;
			array[11].Value = model.BanDescribe;
			array[12].Value = model.LevelA;
			array[13].Value = model.LevelB;
			array[14].Value = model.LevelC;
			array[15].Value = model.LevelD;
			array[16].Value = model.LevelE;
			array[17].Value = model.LevelTree;
			array[18].Value = model.Class;
			array[19].Value = model.Tag;
			array[20].Value = model.ICDtoSection;
			array[21].Value = model.Note;
			array[22].Value = model.InputCode;
			array[23].Value = model.ID_ICD;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_ICD)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DctICDTen ");
			stringBuilder.Append(" where ID_ICD=@ID_ICD");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ICD", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ICD;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_ICDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from DctICDTen ");
			stringBuilder.Append(" where ID_ICD in (" + ID_ICDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.DctICDTen GetModel(int ID_ICD)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_ICD,ICDCNName,ICDENName,Code,Codea,ID_Creator,Creator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,LevelA,LevelB,LevelC,LevelD,LevelE,LevelTree,Class,Tag,ICDtoSection,Note,InputCode from DctICDTen ");
			stringBuilder.Append(" where ID_ICD=@ID_ICD");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ICD", SqlDbType.Int, 4)
			};
			array[0].Value = ID_ICD;
			PEIS.Model.DctICDTen dctICDTen = new PEIS.Model.DctICDTen();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.DctICDTen result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_ICD"].ToString() != "")
				{
					dctICDTen.ID_ICD = int.Parse(dataSet.Tables[0].Rows[0]["ID_ICD"].ToString());
				}
				dctICDTen.ICDCNName = dataSet.Tables[0].Rows[0]["ICDCNName"].ToString();
				dctICDTen.ICDENName = dataSet.Tables[0].Rows[0]["ICDENName"].ToString();
				dctICDTen.Code = dataSet.Tables[0].Rows[0]["Code"].ToString();
				dctICDTen.Codea = dataSet.Tables[0].Rows[0]["Codea"].ToString();
				if (dataSet.Tables[0].Rows[0]["ID_Creator"].ToString() != "")
				{
					dctICDTen.ID_Creator = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_Creator"].ToString()));
				}
				dctICDTen.Creator = dataSet.Tables[0].Rows[0]["Creator"].ToString();
				if (dataSet.Tables[0].Rows[0]["CreateDate"].ToString() != "")
				{
					dctICDTen.CreateDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() != "")
				{
					if (dataSet.Tables[0].Rows[0]["Is_Banned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Banned"].ToString().ToLower() == "true")
					{
						dctICDTen.Is_Banned = new bool?(true);
					}
					else
					{
						dctICDTen.Is_Banned = new bool?(false);
					}
				}
				if (dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString() != "")
				{
					dctICDTen.ID_BanOpr = new int?(int.Parse(dataSet.Tables[0].Rows[0]["ID_BanOpr"].ToString()));
				}
				dctICDTen.BanOperator = dataSet.Tables[0].Rows[0]["BanOperator"].ToString();
				if (dataSet.Tables[0].Rows[0]["BanDate"].ToString() != "")
				{
					dctICDTen.BanDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["BanDate"].ToString()));
				}
				dctICDTen.BanDescribe = dataSet.Tables[0].Rows[0]["BanDescribe"].ToString();
				if (dataSet.Tables[0].Rows[0]["LevelA"].ToString() != "")
				{
					dctICDTen.LevelA = new int?(int.Parse(dataSet.Tables[0].Rows[0]["LevelA"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["LevelB"].ToString() != "")
				{
					dctICDTen.LevelB = new int?(int.Parse(dataSet.Tables[0].Rows[0]["LevelB"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["LevelC"].ToString() != "")
				{
					dctICDTen.LevelC = new int?(int.Parse(dataSet.Tables[0].Rows[0]["LevelC"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["LevelD"].ToString() != "")
				{
					dctICDTen.LevelD = new int?(int.Parse(dataSet.Tables[0].Rows[0]["LevelD"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["LevelE"].ToString() != "")
				{
					dctICDTen.LevelE = new int?(int.Parse(dataSet.Tables[0].Rows[0]["LevelE"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["LevelTree"].ToString() != "")
				{
					dctICDTen.LevelTree = new int?(int.Parse(dataSet.Tables[0].Rows[0]["LevelTree"].ToString()));
				}
				dctICDTen.Class = dataSet.Tables[0].Rows[0]["Class"].ToString();
				dctICDTen.Tag = dataSet.Tables[0].Rows[0]["Tag"].ToString();
				dctICDTen.ICDtoSection = dataSet.Tables[0].Rows[0]["ICDtoSection"].ToString();
				dctICDTen.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				dctICDTen.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				result = dctICDTen;
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
			stringBuilder.Append("select ID_ICD,ICDCNName,ICDENName,Code,Codea,ID_Creator,Creator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,LevelA,LevelB,LevelC,LevelD,LevelE,LevelTree,Class,Tag,ICDtoSection,Note,InputCode ");
			stringBuilder.Append(" FROM DctICDTen ");
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
			stringBuilder.Append(" ID_ICD,ICDCNName,ICDENName,Code,Codea,ID_Creator,Creator,CreateDate,Is_Banned,ID_BanOpr,BanOperator,BanDate,BanDescribe,LevelA,LevelB,LevelC,LevelD,LevelE,LevelTree,Class,Tag,ICDtoSection,Note,InputCode ");
			stringBuilder.Append(" FROM DctICDTen ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
