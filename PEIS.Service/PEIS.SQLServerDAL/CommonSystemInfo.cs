using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;
using System.Reflection;

namespace PEIS.SQLServerDAL
{
	public class CommonSystemInfo : CommonBase, ICommonSystemInfo
	{
		protected string[] QueryCustomerBaseInfo_Param = new string[]
		{
			"--获取用户的基本信息 CustomerInfo\r\n        SELECT ID_Customer, CustomerName, MarriageName, GenderName,CONVERT(varchar(10),BirthDay,120) BirthDay, IDCard, MobileNo, Photo PhotoBase64Code,SubScribDate ExamDate\r\n        FROM OnCustPhysicalExamInfo \r\n        WHERE ID_Customer=@ID_Customer;"
		};

		protected string[] QueryCustomerInfo_WidthHieght_Param = new string[]
		{
			"--获取用户的基本信息 CustomerInfo\r\n        SELECT ID_Customer, CustomerName, MarriageName, GenderName,CONVERT(varchar(10),BirthDay,120) BirthDay, IDCard, MobileNo, Photo PhotoBase64Code,SubScribDate ExamDate\r\n        FROM OnCustPhysicalExamInfo \r\n        WHERE ID_Customer=@ID_Customer;\r\n\r\n        SELECT [ID_ExamItem]\r\n              ,[ExamItemName]\r\n              ,[ResultNumber]\r\n          FROM [OnCustExamItem]\r\n          where ([ExamItemName] = '身高' or [ExamItemName] = '体重' or [ExamItemName] = '腰围')\r\n          and ID_CustFee \r\n          in ( SELECT [ID_CustFee]\r\n          FROM [OnCustFee]\r\n          where ID_Customer = @ID_Customer ); "
		};

		protected string[] QueryQuickSectionList_Param = new string[]
		{
            "SELECT [SectionID],[SectionName],[FunctionType],[DisplayMenu],[IsDel],[InterfaceType],[IsNotSamePage],[IsNonPrintSectSummary],[IsOwnInterface],[IsAutoApprove],[ImageType],[PicPrintSetup],[PacsInterfaceFlag],[InputCode],[DispOrder],[SummaryName],[DefaultSummary],[SepBetweenExamItems],[SepBetweenSymptoms],[TerminalSymbol],[SepExamAndValue],[NoBetweenExamItems],[NoBetweenSympotms],[Note],[IsNoEntryFinalSummary],[IsNonPrintInReport],[IsPrintBarCode]    FROM [SYSSection]\r\n          WHERE 1=1 ;\r\n          "
        };

		DataTable ICommonSystemInfo.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return base.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		DataSet ICommonSystemInfo.ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return base.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		protected new string[] GetSqlSentence(string PageName)
		{
			FieldInfo field = base.GetType().GetField(PageName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new Exception("没有找到SQL");
			}
			return (string[])field.GetValue(this);
		}
	}
}
