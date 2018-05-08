using PEIS.Common;
using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class CommonUser : CommonBase, ICommonUser
	{
		protected string[] UserListPagesParam = new string[]
		{
            "UserID",
			"*",
            " (select A.* from SYSOpUser A left join OnCustExamItem B ON A.UserID=B.ID_Typist WHERE A.UserID>10 ) ",
			"order by UserID asc "
		};

		protected string[] TeamPagesParam = new string[]
		{
			"ID_TeaM",
			"*",
			" (SELECT ID_TeaM,TeamName,ID_Creator,Creator,CONVERT(varchar(10),CreateDate,120) CreateDate,InputCode,Note FROM Team) ",
			"ORDER BY ID_TeaM Desc"
		};

		protected string[] QueryPagesUserListParam = new string[]
		{
            "UserID",
			"*",
            " (  SELECT UserID,SectionID,UserName,LoginName,PassWord,LastLoginTime,Note,DisDate,Signature,DisCountRate,Sex,Is_Del,VocationType,OperateLevel,TotalNum,FailCount,CreateTime from SYSOpUser) ",
			" ORDER BY UserName ASC "
		};

		protected string[] QueryPagesUserListParamByName = new string[]
		{
            "UserID",
			"*",
            " ( SELECT UserID,SectionID,UserName,LoginName,PassWord,LastLoginTime,Note,DisDate,Signature,DisCountRate,Sex,Is_Del,VocationType,OperateLevel,TotalNum,FailCount,CreateTime from SYSOpUser WHERE [UserName] LIKE @UserName OR [LoginName] LIKE @UserName \r\n   ) ",
			" ORDER BY UserName ASC "
		};

        protected string[] QueryPagesSectionUserListParam = new string[]
        {
            "UserID",
            "*",
            " (  SELECT  [UserID],[SYSOpUser].[SectionID],[UserName],[LoginName],[PassWord],[LastLoginTime] ,[SYSOpUser].[Note],[DisDate],[Signature],[DisCountRate],[Sex],[SYSOpUser].[Is_Del] ,[VocationType],[OperateLevel],[TotalNum],[FailCount],[CreateTime], SYSSection.SectionName  as SectionName FROM [SYSOpUser] left join SYSSection on [SYSOpUser].SectionID = SYSSection.SectionID ) ",
			" ORDER BY UserName ASC "

            //"ID_User",
            //"*",
            //" (  SELECT [ID_User]\r\n                                                              ,[NatUser].[ID_Section]\r\n                                                              ,[UserName]\r\n                                                              ,[LoginName]\r\n                                                              ,[PassWord]\r\n                                                              ,[RegistIP]\r\n                                                              ,[RegistTime]\r\n                                                              ,[LastLoginTime]\r\n                                                              ,[LoginTotalNum]\r\n                                                              ,[LoginFailCount]\r\n                                                              ,[OperateLevel]\r\n                                                              ,[NatUser].[Note]\r\n                                                              ,[BanDate]\r\n                                                              ,[Signature]\r\n                                                              ,[DisCountRate]\r\n                                                              ,[GenderName]\r\n                                                              ,[Is_Del]\r\n                                                              ,[VocationType]\r\n                                                              ,[SYSSection].SectionName\r\n                                                          FROM [NatUser] \r\n                                                          LEFT JOIN [SYSSection]\r\n                                                          ON [NatUser].ID_Section = [SYSSection].ID_Section\r\n                                                          \r\n                                                          ) ",
            //" ORDER BY UserName ASC "
        };

		protected string[] QueryPagesSectionUserListParamByName = new string[]
		{
           "UserID",
            "*",
            " (  SELECT  [UserID],[SYSOpUser].[SectionID] as SectionID ,[UserName],[LoginName],[PassWord],[LastLoginTime],[SYSOpUser].[Note],[DisDate],[Signature],[DisCountRate],[Sex],[SYSOpUser].[Is_Del],[VocationType],[OperateLevel],[TotalNum],[FailCount],[CreateTime], SYSSection.SectionName as SectionName FROM [SYSOpUser] left join SYSSection on [SYSOpUser].SectionID = SYSSection.SectionID )  WHERE [UserName] LIKE @UserName OR [LoginName] LIKE @UserName ",
            " ORDER BY UserName ASC "
        };

		protected string[] QueryPagesUserListParamBySection = new string[]
		{
            "UserID",
			"*",
            " (  SELECT UserID,SYSOpUser.SectionID,UserName,LoginName,PassWord,LastLoginTime,[SYSOpUser].Note,DisDate,Signature,DisCountRate,Sex,SYSOpUser.Is_Del,VocationType,OperateLevel,TotalNum,FailCount,CreateTime， SYSSection.SectionName from SYSOpUser left join SYSSection on [SYSOpUser].SectionID = SYSSection.SectionID ) ",
			" ORDER BY UserName ASC "
		};

		protected string[] QueryPagesUserListParamByNameAndSection = new string[]
		{
            "UserID",
			"*",
            " ( SELECT UserID,SYSOpUser.SectionID,UserName,LoginName,PassWord,LastLoginTime,[SYSOpUser].Note,DisDate,Signature,DisCountRate,Sex,SYSOpUser.Is_Del,VocationType,OperateLevel,TotalNum,FailCount,CreateTime， SYSSection.SectionName from SYSOpUser left join SYSSection on [SYSOpUser].SectionID = SYSSection.SectionID   WHERE ([UserName] LIKE @UserName OR [LoginName] LIKE @UserName) AND [SYSOpUser].SectionID = @SectionID  \r\n                                                          ) ",
			" ORDER BY UserName ASC "
		};

		protected string[] QuerySectionExamDoctorList_Param = new string[]
		{
            "SELECT NU.*\r\n  FROM [SYSSection] NUS,SYSOpUser NU\r\n     WHERE NUS.[UserID] = NU.[UserID]  AND NUS.SectionID = @SectionID\r\n          AND NU.VocationType = 1\r\n          ORDER BY [UserName] ASC;\r\n          "
        };

		protected string[] QuerySectionExamUserList_Param = new string[]
		{
            "SELECT NU.[UserID]\r\n ,NU.[UserName]\r\n ,NU.[LoginName]\r\n ,NU.[VocationType]\r\n  FROM [SYSUserSection] NUS,SYSOpUser NU\r\n          WHERE NUS.[UserID] = NU.[UserID] \r\n          AND NUS.SectionID = @SectionID\r\n          ORDER BY [UserName] ASC;\r\n          "
        };

		protected string[] QueryQuickUserList_Param = new string[]
		{
            "SELECT [UserID]\r\n              ,[UserName]\r\n              ,[LoginName]\r\n              ,[VocationType]\r\n          FROM [SYSOpUser]\r\n         WHERE Is_Del = 0 OR Is_Del is null \r\n      ORDER BY [UserName] ASC;\r\n          "
        };

		protected string[] QuerySingleSectionUserInfo_Param = new string[]
		{
            " SELECT [UserID]\r\n     ,[SYSOpUser].[SectionID]\r\n                ,[UserName]\r\n                ,[LoginName]\r\n                ,[PassWord]\r\n                ,[CreateTime]\r\n                ,[LastLoginTime]\r\n                ,[TotalNum]\r\n                ,ISNULL(FailCount,0) FailCount\r\n                ,[OperateLevel]\r\n                ,[SYSOpUser].[Note]\r\n                ,[DisDate]\r\n                ,[Signature]\r\n                ,[DisCountRate]\r\n                ,[sex]\r\n                ,[Is_Del]\r\n                ,[VocationType]\r\n                ,[SYSSection].SectionName\r\n            FROM [SYSOpUser] \r\n            LEFT JOIN [SYSSection]\r\n            ON [SYSOpUser].SectionID = [SYSSection].SectionID \r\n         WHERE User = @UserID;\r\n          "
        };

		DataTable ICommonUser.GetUserInfoByLoginName(string LoginName)
		{
			DbParameter[] array = new DbParameter[]
			{
				new SqlParameter("@LoginName", SqlDbType.VarChar)
			};
			array[0].Value = LoginName;
			string text = " select * from " + CommonBase.tableStartLetter + "SYSOpUser where LoginName=@LoginName";
			DataTable result;
			try
			{
				DataTable dataTable = DbHelperSQL.ExecuteTable(CommandType.Text, text, array);
				result = dataTable;
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error("用户登录： Exception：" + ex.Message + " Sql：" + Secret.AES.Encrypt(text));
				throw ex;
			}
			return result;
		}

		DataTable ICommonUser.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return base.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		DataSet ICommonUser.ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return base.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public new DataTable CreateMaxNum(string NumberHeader)
		{
			return base.CreateMaxNum(NumberHeader);
		}

		public int AddCustomerArcInfo(PEIS.Model.OnArcCust model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("if not exists(select CultrulName from OnArcCust where IDCard=@IDCard AND CustomerName=@CustomerName)\r\n            begin\r\n                insert into OnArcCust(IDCard,CustomerName,ID_Gender,GenderName,BirthDay,NationID,NationName,Photo,FirstDatePE,LatestDatePE,FinishedNum,UnfinishedNum)\r\nvalues(@IDCard,@CustomerName,@ID_Gender,@GenderName,@BirthDay,@NationID,@NationName,@Photo,@FirstDatePE,@LatestDatePE,0,1);\r\nselect @@IDENTITY;\r\n            end\r\n            else \r\n            begin\r\n                update OnArcCust set CustomerName=@CustomerName,ID_Gender=@ID_Gender,GenderName=@GenderName,BirthDay=@BirthDay,NationID=@NationID,NationName=@NationName,Photo=@Photo\r\nwhere IDCard=@IDCard AND CustomerName=@CustomerName;\r\nselect 2;\r\n            end;");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@IDCard", SqlDbType.VarChar),
				new SqlParameter("@CustomerName", SqlDbType.VarChar),
				new SqlParameter("@ID_Gender", SqlDbType.Int, 4),
				new SqlParameter("@GenderName", SqlDbType.VarChar),
				new SqlParameter("@BirthDay", SqlDbType.DateTime),
				new SqlParameter("@NationID", SqlDbType.Int, 4),
				new SqlParameter("@NationName", SqlDbType.VarChar),
				new SqlParameter("@Photo", SqlDbType.Image),
				new SqlParameter("@FirstDatePE", SqlDbType.DateTime),
				new SqlParameter("@LatestDatePE", SqlDbType.DateTime)
			};
			DateTime now = DateTime.Now;
			array[0].Value = model.IDCard;
			array[1].Value = model.CustomerName;
			array[2].Value = model.ID_Gender;
			array[3].Value = model.GenderName;
			array[4].Value = model.BirthDay;
			if (model.NationID.Value == -1)
			{
				array[5].Value = DBNull.Value;
			}
			else
			{
				array[5].Value = model.NationID;
			}
			array[6].Value = model.NationName;
			array[7].Value = model.Photo;
			array[8].Value = now;
			array[9].Value = now;
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

		public int AddCustomerManageArcInfo(PEIS.Model.OnArcCust model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("--通过客户IDCard和CustomerName修改客户体检信息 xmhuang 2014-04-21\r\nUPDATE OnCustPhysicalExamInfo SET CustomerName=@CustomerName,ID_Gender=@ID_Gender,GenderName=@GenderName,BirthDay=@BirthDay,ID_Marriage=@ID_Marriage,MarriageName=@MarriageName,Photo=@Photo,NationID=@NationID,NationName=@NationName,IDCard=@IDCard,MobileNo=@MobileNo WHERE \r\nIDCard=(SELECT IDCard FROM OnArcCust WHERE ID_ArcCustomer=@ID_ArcCustomer) AND CustomerName=(SELECT CustomerName FROM OnArcCust WHERE ID_ArcCustomer=@ID_ArcCustomer);\r\nupdate OnArcCust set CustomerName=@CustomerName,ID_Gender=@ID_Gender,GenderName=@GenderName,BirthDay=@BirthDay,ID_Marriage=@ID_Marriage,MarriageName=@MarriageName,Photo=@Photo,NationID=@NationID,NationName=@NationName,IDCard=@IDCard,MobileNo=@MobileNo\r\nwhere ID_ArcCustomer=@ID_ArcCustomer;select 1;");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_ArcCustomer", SqlDbType.Int),
				new SqlParameter("@CustomerName", SqlDbType.VarChar),
				new SqlParameter("@ID_Gender", SqlDbType.Int, 4),
				new SqlParameter("@GenderName", SqlDbType.VarChar),
				new SqlParameter("@BirthDay", SqlDbType.DateTime),
				new SqlParameter("@ID_Marriage", SqlDbType.Int, 4),
				new SqlParameter("@MarriageName", SqlDbType.VarChar),
				new SqlParameter("@Photo", SqlDbType.Image),
				new SqlParameter("@NationID", SqlDbType.Int),
				new SqlParameter("@NationName", SqlDbType.VarChar),
				new SqlParameter("@IDCard", SqlDbType.VarChar),
				new SqlParameter("@MobileNo", SqlDbType.VarChar)
			};
			DateTime now = DateTime.Now;
			array[0].Value = model.ID_ArcCustomer;
			array[1].Value = model.CustomerName;
			array[2].Value = model.ID_Gender;
			array[3].Value = model.GenderName;
			array[4].Value = model.BirthDay;
			int arg_14D_0 = model.ID_Marriage.Value;
			if (model.ID_Marriage.Value == -1)
			{
				array[5].Value = DBNull.Value;
			}
			else
			{
				array[5].Value = model.ID_Marriage;
			}
			array[6].Value = model.MarriageName;
			array[7].Value = model.Photo;
			if (model.NationID.Value == -1)
			{
				array[8].Value = DBNull.Value;
			}
			else
			{
				array[8].Value = model.NationID;
			}
			array[9].Value = model.NationName;
			array[10].Value = model.IDCard;
			array[11].Value = model.MobileNo;
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

		public int UpdateCustomerPicInfo(PEIS.Model.OnArcCust model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update OnArcCust set Photo=@Photo where IDCard=@IDCard AND CustomerName=@CustomerName;select 1;");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@Photo", SqlDbType.Image),
				new SqlParameter("@IDCard", SqlDbType.VarChar),
				new SqlParameter("@CustomerName", SqlDbType.VarChar)
			};
			DateTime now = DateTime.Now;
			if (model.Photo == null || model.Photo.Length == 0)
			{
				array[0].Value = DBNull.Value;
			}
			else
			{
				array[0].Value = model.Photo;
			}
			array[1].Value = model.IDCard;
			array[2].Value = model.CustomerName;
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

		public int UpdateCustomerPicInfo(string ID_Customer, PEIS.Model.OnArcCust model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE OnCustPhysicalExamInfo SET PHOTO=@Photo WHERE ID_Customer=@ID_Customer;\r\nUPDATE OnArcCust SET Photo=@Photo WHERE ID_ArcCustomer=(select ID_ArcCustomer from OnCustRelationCustPEInfo where ID_Customer=@ID_Customer);\r\nselect 1;");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@Photo", SqlDbType.Image),
				new SqlParameter("@IDCard", SqlDbType.VarChar),
				new SqlParameter("@CustomerName", SqlDbType.VarChar),
				new SqlParameter("@ID_Customer", SqlDbType.VarChar)
			};
			DateTime now = DateTime.Now;
			if (model.Photo == null || model.Photo.Length == 0)
			{
				array[0].Value = DBNull.Value;
			}
			else
			{
				array[0].Value = model.Photo;
			}
			array[1].Value = model.IDCard;
			array[2].Value = model.CustomerName;
			array[3].Value = ID_Customer;
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
	}
}
