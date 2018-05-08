using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;
using System.Reflection;

namespace PEIS.SQLServerDAL
{
	public class CommonRegiste : CommonBase, ICommonRegiste
	{
		protected string[] QueryCustomerManageListInfoPagesParam = new string[]
		{
			"ID_ArcCustomer",
			"*",
			"(SELECT NationID,NationName,ID_Marriage,MarriageName,ID_ArcCustomer,YEAR(getdate())-YEAR(BirthDay) age,CustomerName,ID_Gender,GenderName,IDCard,CONVERT(varchar(10),OnArcCust.BirthDay,120) BirthDay,Photo,MobileNo from OnArcCust) ",
			"ORDER BY ID_ArcCustomer DESC"
		};

		protected string[] ArcCustAndPhysicalExamInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			" (SELECT YEAR(getdate())-YEAR(OnArcCust.BirthDay) age,OnCustPhysicalExamInfo.ID_Customer,OnCustPhysicalExamInfo.CustomerName,OnCustPhysicalExamInfo.ID_Operator\r\n                ,OnCustPhysicalExamInfo.ID_Operator ID_SubScriber,OnCustPhysicalExamInfo.ID_Operator ID_Register,OnArcCust.ID_ArcCustomer,GenderName,IDCard,MarriageName,MobileNo,isnull(OnCustPhysicalExamInfo.Is_Team,0) Is_Team,isnull(OnCustPhysicalExamInfo.Is_FeeSettled,0) Is_FeeSettled,OnCustPhysicalExamInfo.Is_Subscribed Is_Subscribed \r\n                FROM OnCustPhysicalExamInfo\r\n                INNER JOIN OnCustRelationCustPEInfo ON OnCustPhysicalExamInfo.ID_Customer=OnCustRelationCustPEInfo.ID_Customer\r\n                INNER JOIN OnArcCust on OnArcCust.ID_ArcCustomer=OnCustRelationCustPEInfo.ID_ArcCustomer where Is_Subscribed is not null) ",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] QueryPhysicalExamInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			"( SELECT OCF.*,T.TeamName FROM(SELECT ID_Customer,\r\n                CustomerName,\r\n                ID_SubScriber,\r\n                SubScriber,\r\n                CONVERT(varchar(10),SubScribDate,120) SubScribDate,\r\n                SubScriberOperDate,\r\n                ID_Operator,\r\n                OperateDate,\r\n                Operator,\r\n               ID_Team,\r\n                isnull(Is_Team,0) Is_Team,\r\n                isnull(Is_FeeSettled,0) Is_FeeSettled,\r\n                OnCustPhysicalExamInfo.Is_Subscribed Is_Subscribed \r\n                FROM OnCustPhysicalExamInfo WHERE SecurityLevel<100) OCF\r\n                LEFT JOIN Team T ON OCF.ID_Team=T.ID_Team\r\n                ) ",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] QueryPhysicalExamInfoPagesParamX = new string[]
		{
			"ID_Customer",
			"*",
			"( SELECT TOP 99999999 OCF.* FROM(SELECT ID_Customer,\r\n                Photo,CustomerName,IDCard,GenderName,MarriageName,MobileNo,FLOOR(DATEDIFF(DY,BirthDay,(SELECT CASE WHEN Is_GuideSheetPrinted=1 THEN OperateDate ELSE GETDATE() END OperateDate ))/365.25) Age,\r\n                ID_SubScriber,\r\n                SubScriber,\r\n                CONVERT(varchar(10),SubScribDate,120) SubScribDate,\r\n                SubScriberOperDate,\r\n                ID_Operator,\r\n                CONVERT(nvarchar(20),OperateDate,120) OperateDate,\r\n                Operator,\r\n                ID_Team,\r\n                TeamName,\r\n                isnull(Is_Team,0) Is_Team,\r\n                isnull(Is_InternatSubscribe,0) Is_InternatSubscribe,\r\n                CustomerOrderNo,\r\n                isnull(Is_GuideSheetPrinted,0) Is_GuideSheetPrinted,\r\n                isnull(Is_FeeSettled,0) Is_FeeSettled,\r\n                OnCustPhysicalExamInfo.Is_Subscribed Is_Subscribed \r\n                FROM OnCustPhysicalExamInfo WHERE SecurityLevel<100) OCF ORDER BY OCF.ID_Customer ASC\r\n                ) ",
			"ORDER BY ID_Customer ASC"
		};

		protected string[] QueryCustomerSecurityLevelListInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			" (SELECT TeamName,ID_Customer,\r\n                IDCard,CustomerName,GenderName,YEAR(getdate())-YEAR(BirthDay)Age,MobileNo,\r\n\t\t\t\tID_Team,\r\n                SecurityLevel,\r\n                ID_Operator,\r\n                OperateDate,\r\n                ID_Operator ID_SubScriber,\r\n                ID_Operator ID_Register,\r\n                isnull(Is_Team,0) Is_Team,\r\n                isnull(Is_FeeSettled,0) Is_FeeSettled,\r\n                Is_Subscribed Is_Subscribed \r\n                FROM OnCustPhysicalExamInfo\r\n\r\n                ) ",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] QueryCustomerPagesInfoParam = new string[]
		{
			"ID_Customer",
			"*",
			"(SELECT 1 exist,ISNULL(Is_Paused,0) Is_Paused,Note,ISNULL(Is_ExamStarted,Is_ExamStarted)Is_ExamStarted,NationID,NationName,MobileNo CustomerTel,IDCard,CustomerName,ID_Gender,GenderName,ID_Marriage,MarriageName,CONVERT(varchar(10),BirthDay,120) CustomerBirthDay,RoleName CustomerRoleName,TeamTaskGroup.ID_Team,TeamTaskGroup.ID_TeamTask,TeamTaskGroup.ID_TeamTaskGroup,TeamTaskGroupName,ID_TeamTaskGroupCustomer,TeamTaskGroupCust.ID_Customer,TeamTaskGroupCust.Department DepartmentX,TeamTaskGroupCust.DepartSubA DepartmentA,TeamTaskGroupCust.DepartSubB DepartmentB,TeamTaskGroupCust.DepartSubC DepartmentC FROM TeamTaskGroupCust\r\nINNER JOIN (SELECT * FROM TeamTaskGroup WHERE ID_Team=@ID_Team AND ID_TeamTask=@ID_TeamTask AND ID_TeamTaskGroup IN(@ID_TeamTaskGroup))TeamTaskGroup ON TeamTaskGroupCust.ID_TeamTaskGroup=TeamTaskGroup.ID_TeamTaskGroup\r\nINNER JOIN (SELECT * FROM OnCustPhysicalExamInfo WHERE ID_Customer=@ID_Customer) OCPEI ON TeamTaskGroupCust.ID_Customer=OCPEI.ID_Customer\r\nWHERE IDCard=@IDCard AND CustomerName LIKE @CustomerName) ",
			"ORDER BY ID_Customer DESC"
		};

		DataTable ICommonRegiste.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return base.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
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
