using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SYSSection : ISYSSection
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_Section", "SYSSection");
		}

		public bool Exists(int SectionID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SYSSection");
            strSql.Append(" where SectionID=@SectionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SectionID", SqlDbType.Int,4)         };
            parameters[0].Value = SectionID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		public int Add(PEIS.Model.SYSSection model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SYSSection(");
            strSql.Append("SectionName,FunctionType,DisplayMenu,IsDel,InterfaceType,IsNotSamePage,IsNonPrintSectSummary,IsOwnInterface,IsAutoApprove,ImageType,PicPrintSetup,PacsInterfaceFlag,InputCode,DispOrder,SummaryName,DefaultSummary,SepBetweenExamItems,SepBetweenSymptoms,TerminalSymbol,SepExamAndValue,NoBetweenExamItems,NoBetweenSympotms,Note,IsNoEntryFinalSummary,IsNonPrintInReport,IsPrintBarCode)");
            strSql.Append(" values (");
            strSql.Append("@SectionName,@FunctionType,@DisplayMenu,@IsDel,@InterfaceType,@IsNotSamePage,@IsNonPrintSectSummary,@IsOwnInterface,@IsAutoApprove,@ImageType,@PicPrintSetup,@PacsInterfaceFlag,@InputCode,@DispOrder,@SummaryName,@DefaultSummary,@SepBetweenExamItems,@SepBetweenSymptoms,@TerminalSymbol,@SepExamAndValue,@NoBetweenExamItems,@NoBetweenSympotms,@Note,@IsNoEntryFinalSummary,@IsNonPrintInReport,@IsPrintBarCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SectionName", SqlDbType.VarChar,20),
                    new SqlParameter("@FunctionType", SqlDbType.Bit,1),
                    new SqlParameter("@DisplayMenu", SqlDbType.VarChar,80),
                    new SqlParameter("@IsDel", SqlDbType.Bit,1),
                    new SqlParameter("@InterfaceType", SqlDbType.VarChar,8),
                    new SqlParameter("@IsNotSamePage", SqlDbType.Bit,1),
                    new SqlParameter("@IsNonPrintSectSummary", SqlDbType.Bit,1),
                    new SqlParameter("@IsOwnInterface", SqlDbType.Bit,1),
                    new SqlParameter("@IsAutoApprove", SqlDbType.Bit,1),
                    new SqlParameter("@ImageType", SqlDbType.VarChar,8),
                    new SqlParameter("@PicPrintSetup", SqlDbType.VarChar,8),
                    new SqlParameter("@PacsInterfaceFlag", SqlDbType.VarChar,8),
                    new SqlParameter("@InputCode", SqlDbType.VarChar,10),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@SummaryName", SqlDbType.VarChar,20),
                    new SqlParameter("@DefaultSummary", SqlDbType.Text),
                    new SqlParameter("@SepBetweenExamItems", SqlDbType.VarChar,20),
                    new SqlParameter("@SepBetweenSymptoms", SqlDbType.VarChar,20),
                    new SqlParameter("@TerminalSymbol", SqlDbType.VarChar,20),
                    new SqlParameter("@SepExamAndValue", SqlDbType.VarChar,20),
                    new SqlParameter("@NoBetweenExamItems", SqlDbType.VarChar,20),
                    new SqlParameter("@NoBetweenSympotms", SqlDbType.VarChar,20),
                    new SqlParameter("@Note", SqlDbType.VarChar,50),
                    new SqlParameter("@IsNoEntryFinalSummary", SqlDbType.Bit,1),
                    new SqlParameter("@IsNonPrintInReport", SqlDbType.Bit,1),
                    new SqlParameter("@IsPrintBarCode", SqlDbType.Int,4)};
            parameters[0].Value = model.SectionName;
            parameters[1].Value = model.FunctionType;
            parameters[2].Value = model.DisplayMenu;
            parameters[3].Value = model.IsDel;
            parameters[4].Value = model.InterfaceType;
            parameters[5].Value = model.IsNotSamePage;
            parameters[6].Value = model.IsNonPrintSectSummary;
            parameters[7].Value = model.IsOwnInterface;
            parameters[8].Value = model.IsAutoApprove;
            parameters[9].Value = model.ImageType;
            parameters[10].Value = model.PicPrintSetup;
            parameters[11].Value = model.PacsInterfaceFlag;
            parameters[12].Value = model.InputCode;
            parameters[13].Value = model.DispOrder;
            parameters[14].Value = model.SummaryName;
            parameters[15].Value = model.DefaultSummary;
            parameters[16].Value = model.SepBetweenExamItems;
            parameters[17].Value = model.SepBetweenSymptoms;
            parameters[18].Value = model.TerminalSymbol;
            parameters[19].Value = model.SepExamAndValue;
            parameters[20].Value = model.NoBetweenExamItems;
            parameters[21].Value = model.NoBetweenSympotms;
            parameters[22].Value = model.Note;
            parameters[23].Value = model.IsNoEntryFinalSummary;
            parameters[24].Value = model.IsNonPrintInReport;
            parameters[25].Value = model.IsPrintBarCode;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

		public bool Update(PEIS.Model.SYSSection model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SYSSection set ");
            strSql.Append("SectionName=@SectionName,");
            strSql.Append("FunctionType=@FunctionType,");
            strSql.Append("DisplayMenu=@DisplayMenu,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("InterfaceType=@InterfaceType,");
            strSql.Append("IsNotSamePage=@IsNotSamePage,");
            strSql.Append("IsNonPrintSectSummary=@IsNonPrintSectSummary,");
            strSql.Append("IsOwnInterface=@IsOwnInterface,");
            strSql.Append("IsAutoApprove=@IsAutoApprove,");
            strSql.Append("ImageType=@ImageType,");
            strSql.Append("PicPrintSetup=@PicPrintSetup,");
            strSql.Append("PacsInterfaceFlag=@PacsInterfaceFlag,");
            strSql.Append("InputCode=@InputCode,");
            strSql.Append("DispOrder=@DispOrder,");
            strSql.Append("SummaryName=@SummaryName,");
            strSql.Append("DefaultSummary=@DefaultSummary,");
            strSql.Append("SepBetweenExamItems=@SepBetweenExamItems,");
            strSql.Append("SepBetweenSymptoms=@SepBetweenSymptoms,");
            strSql.Append("TerminalSymbol=@TerminalSymbol,");
            strSql.Append("SepExamAndValue=@SepExamAndValue,");
            strSql.Append("NoBetweenExamItems=@NoBetweenExamItems,");
            strSql.Append("NoBetweenSympotms=@NoBetweenSympotms,");
            strSql.Append("Note=@Note,");
            strSql.Append("IsNoEntryFinalSummary=@IsNoEntryFinalSummary,");
            strSql.Append("IsNonPrintInReport=@IsNonPrintInReport,");
            strSql.Append("IsPrintBarCode=@IsPrintBarCode");
            strSql.Append(" where SectionID=@SectionID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SectionName", SqlDbType.VarChar,20),
                    new SqlParameter("@FunctionType", SqlDbType.Bit,1),
                    new SqlParameter("@DisplayMenu", SqlDbType.VarChar,80),
                    new SqlParameter("@IsDel", SqlDbType.Bit,1),
                    new SqlParameter("@InterfaceType", SqlDbType.VarChar,8),
                    new SqlParameter("@IsNotSamePage", SqlDbType.Bit,1),
                    new SqlParameter("@IsNonPrintSectSummary", SqlDbType.Bit,1),
                    new SqlParameter("@IsOwnInterface", SqlDbType.Bit,1),
                    new SqlParameter("@IsAutoApprove", SqlDbType.Bit,1),
                    new SqlParameter("@ImageType", SqlDbType.VarChar,8),
                    new SqlParameter("@PicPrintSetup", SqlDbType.VarChar,8),
                    new SqlParameter("@PacsInterfaceFlag", SqlDbType.VarChar,8),
                    new SqlParameter("@InputCode", SqlDbType.VarChar,10),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@SummaryName", SqlDbType.VarChar,20),
                    new SqlParameter("@DefaultSummary", SqlDbType.Text),
                    new SqlParameter("@SepBetweenExamItems", SqlDbType.VarChar,20),
                    new SqlParameter("@SepBetweenSymptoms", SqlDbType.VarChar,20),
                    new SqlParameter("@TerminalSymbol", SqlDbType.VarChar,20),
                    new SqlParameter("@SepExamAndValue", SqlDbType.VarChar,20),
                    new SqlParameter("@NoBetweenExamItems", SqlDbType.VarChar,20),
                    new SqlParameter("@NoBetweenSympotms", SqlDbType.VarChar,20),
                    new SqlParameter("@Note", SqlDbType.VarChar,50),
                    new SqlParameter("@IsNoEntryFinalSummary", SqlDbType.Bit,1),
                    new SqlParameter("@IsNonPrintInReport", SqlDbType.Bit,1),
                    new SqlParameter("@IsPrintBarCode", SqlDbType.Int,4),
                    new SqlParameter("@SectionID", SqlDbType.Int,4)};
            parameters[0].Value = model.SectionName;
            parameters[1].Value = model.FunctionType;
            parameters[2].Value = model.DisplayMenu;
            parameters[3].Value = model.IsDel;
            parameters[4].Value = model.InterfaceType;
            parameters[5].Value = model.IsNotSamePage;
            parameters[6].Value = model.IsNonPrintSectSummary;
            parameters[7].Value = model.IsOwnInterface;
            parameters[8].Value = model.IsAutoApprove;
            parameters[9].Value = model.ImageType;
            parameters[10].Value = model.PicPrintSetup;
            parameters[11].Value = model.PacsInterfaceFlag;
            parameters[12].Value = model.InputCode;
            parameters[13].Value = model.DispOrder;
            parameters[14].Value = model.SummaryName;
            parameters[15].Value = model.DefaultSummary;
            parameters[16].Value = model.SepBetweenExamItems;
            parameters[17].Value = model.SepBetweenSymptoms;
            parameters[18].Value = model.TerminalSymbol;
            parameters[19].Value = model.SepExamAndValue;
            parameters[20].Value = model.NoBetweenExamItems;
            parameters[21].Value = model.NoBetweenSympotms;
            parameters[22].Value = model.Note;
            parameters[23].Value = model.IsNoEntryFinalSummary;
            parameters[24].Value = model.IsNonPrintInReport;
            parameters[25].Value = model.IsPrintBarCode;
            parameters[26].Value = model.SectionID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool Delete(int SectionID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSSection ");
            strSql.Append(" where SectionID=@SectionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SectionID", SqlDbType.Int,4)         };
            parameters[0].Value = SectionID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool DeleteList(string SectionIDlist)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSSection ");
            strSql.Append(" where SectionID in (" + SectionIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public PEIS.Model.SYSSection GetModel(int SectionID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SectionID,SectionName,FunctionType,DisplayMenu,IsDel,InterfaceType,IsNotSamePage,IsNonPrintSectSummary,IsOwnInterface,IsAutoApprove,ImageType,PicPrintSetup,PacsInterfaceFlag,InputCode,DispOrder,SummaryName,DefaultSummary,SepBetweenExamItems,SepBetweenSymptoms,TerminalSymbol,SepExamAndValue,NoBetweenExamItems,NoBetweenSympotms,Note,IsNoEntryFinalSummary,IsNonPrintInReport,IsPrintBarCode from SYSSection ");
            strSql.Append(" where SectionID=@SectionID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SectionID", SqlDbType.Int,4)
            };
            parameters[0].Value = SectionID;

            PEIS.Model.SYSSection model = new PEIS.Model.SYSSection();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public PEIS.Model.SYSSection DataRowToModel(DataRow row)
        {
            PEIS.Model.SYSSection model = new PEIS.Model.SYSSection();
            if (row != null)
            {
                if (row["SectionID"] != null && row["SectionID"].ToString() != "")
                {
                    model.SectionID = int.Parse(row["SectionID"].ToString());
                }
                if (row["SectionName"] != null)
                {
                    model.SectionName = row["SectionName"].ToString();
                }
                if (row["FunctionType"] != null && row["FunctionType"].ToString() != "")
                {
                    if ((row["FunctionType"].ToString() == "1") || (row["FunctionType"].ToString().ToLower() == "true"))
                    {
                        model.FunctionType = true;
                    }
                    else
                    {
                        model.FunctionType = false;
                    }
                }
                if (row["DisplayMenu"] != null)
                {
                    model.DisplayMenu = row["DisplayMenu"].ToString();
                }
                if (row["IsDel"] != null && row["IsDel"].ToString() != "")
                {
                    if ((row["IsDel"].ToString() == "1") || (row["IsDel"].ToString().ToLower() == "true"))
                    {
                        model.IsDel = true;
                    }
                    else
                    {
                        model.IsDel = false;
                    }
                }
                if (row["InterfaceType"] != null)
                {
                    model.InterfaceType = row["InterfaceType"].ToString();
                }
                if (row["IsNotSamePage"] != null && row["IsNotSamePage"].ToString() != "")
                {
                    if ((row["IsNotSamePage"].ToString() == "1") || (row["IsNotSamePage"].ToString().ToLower() == "true"))
                    {
                        model.IsNotSamePage = true;
                    }
                    else
                    {
                        model.IsNotSamePage = false;
                    }
                }
                if (row["IsNonPrintSectSummary"] != null && row["IsNonPrintSectSummary"].ToString() != "")
                {
                    if ((row["IsNonPrintSectSummary"].ToString() == "1") || (row["IsNonPrintSectSummary"].ToString().ToLower() == "true"))
                    {
                        model.IsNonPrintSectSummary = true;
                    }
                    else
                    {
                        model.IsNonPrintSectSummary = false;
                    }
                }
                if (row["IsOwnInterface"] != null && row["IsOwnInterface"].ToString() != "")
                {
                    if ((row["IsOwnInterface"].ToString() == "1") || (row["IsOwnInterface"].ToString().ToLower() == "true"))
                    {
                        model.IsOwnInterface = true;
                    }
                    else
                    {
                        model.IsOwnInterface = false;
                    }
                }
                if (row["IsAutoApprove"] != null && row["IsAutoApprove"].ToString() != "")
                {
                    if ((row["IsAutoApprove"].ToString() == "1") || (row["IsAutoApprove"].ToString().ToLower() == "true"))
                    {
                        model.IsAutoApprove = true;
                    }
                    else
                    {
                        model.IsAutoApprove = false;
                    }
                }
                if (row["ImageType"] != null)
                {
                    model.ImageType = row["ImageType"].ToString();
                }
                if (row["PicPrintSetup"] != null)
                {
                    model.PicPrintSetup = row["PicPrintSetup"].ToString();
                }
                if (row["PacsInterfaceFlag"] != null)
                {
                    model.PacsInterfaceFlag = row["PacsInterfaceFlag"].ToString();
                }
                if (row["InputCode"] != null)
                {
                    model.InputCode = row["InputCode"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["SummaryName"] != null)
                {
                    model.SummaryName = row["SummaryName"].ToString();
                }
                if (row["DefaultSummary"] != null)
                {
                    model.DefaultSummary = row["DefaultSummary"].ToString();
                }
                if (row["SepBetweenExamItems"] != null)
                {
                    model.SepBetweenExamItems = row["SepBetweenExamItems"].ToString();
                }
                if (row["SepBetweenSymptoms"] != null)
                {
                    model.SepBetweenSymptoms = row["SepBetweenSymptoms"].ToString();
                }
                if (row["TerminalSymbol"] != null)
                {
                    model.TerminalSymbol = row["TerminalSymbol"].ToString();
                }
                if (row["SepExamAndValue"] != null)
                {
                    model.SepExamAndValue = row["SepExamAndValue"].ToString();
                }
                if (row["NoBetweenExamItems"] != null)
                {
                    model.NoBetweenExamItems = row["NoBetweenExamItems"].ToString();
                }
                if (row["NoBetweenSympotms"] != null)
                {
                    model.NoBetweenSympotms = row["NoBetweenSympotms"].ToString();
                }
                if (row["Note"] != null)
                {
                    model.Note = row["Note"].ToString();
                }
                if (row["IsNoEntryFinalSummary"] != null && row["IsNoEntryFinalSummary"].ToString() != "")
                {
                    if ((row["IsNoEntryFinalSummary"].ToString() == "1") || (row["IsNoEntryFinalSummary"].ToString().ToLower() == "true"))
                    {
                        model.IsNoEntryFinalSummary = true;
                    }
                    else
                    {
                        model.IsNoEntryFinalSummary = false;
                    }
                }
                if (row["IsNonPrintInReport"] != null && row["IsNonPrintInReport"].ToString() != "")
                {
                    if ((row["IsNonPrintInReport"].ToString() == "1") || (row["IsNonPrintInReport"].ToString().ToLower() == "true"))
                    {
                        model.IsNonPrintInReport = true;
                    }
                    else
                    {
                        model.IsNonPrintInReport = false;
                    }
                }
                if (row["IsPrintBarCode"] != null && row["IsPrintBarCode"].ToString() != "")
                {
                    model.IsPrintBarCode = int.Parse(row["IsPrintBarCode"].ToString());
                }
            }
            return model;
        }

        public DataSet GetList(string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SectionID,SectionName,FunctionType,DisplayMenu,IsDel,InterfaceType,IsNotSamePage,IsNonPrintSectSummary,IsOwnInterface,IsAutoApprove,ImageType,PicPrintSetup,PacsInterfaceFlag,InputCode,DispOrder,SummaryName,DefaultSummary,SepBetweenExamItems,SepBetweenSymptoms,TerminalSymbol,SepExamAndValue,NoBetweenExamItems,NoBetweenSympotms,Note,IsNoEntryFinalSummary,IsNonPrintInReport,IsPrintBarCode ");
            strSql.Append(" FROM SYSSection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SectionID,SectionName,FunctionType,DisplayMenu,IsDel,InterfaceType,IsNotSamePage,IsNonPrintSectSummary,IsOwnInterface,IsAutoApprove,ImageType,PicPrintSetup,PacsInterfaceFlag,InputCode,DispOrder,SummaryName,DefaultSummary,SepBetweenExamItems,SepBetweenSymptoms,TerminalSymbol,SepExamAndValue,NoBetweenExamItems,NoBetweenSympotms,Note,IsNoEntryFinalSummary,IsNonPrintInReport,IsPrintBarCode ");
            strSql.Append(" FROM SYSSection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


       
    }
}
