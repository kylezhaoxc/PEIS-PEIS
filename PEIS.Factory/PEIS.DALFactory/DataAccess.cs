using PEIS.IDAL;
using System;
using System.Configuration;
using System.Reflection;

namespace PEIS.DALFactory
{
	public sealed class DataAccess
	{
		private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];

		private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
		{
			object result;
			try
			{
				object obj = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
				result = obj;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static object CreateObject(string AssemblyPath, string classNamespace)
		{
			object obj = DataCache.GetCache(classNamespace);
			if (obj == null)
			{
				try
				{
					obj = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
					DataCache.SetCache(classNamespace, obj);
				}
				catch
				{
				}
			}
			return obj;
		}

		public static ISysManage CreateSysManage()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SysManage";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISysManage)obj;
		}

		public static ICommonConclusion CreateCommonConclusion()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonConclusion";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonConclusion)obj;
		}

		public static IBusFeeReport CreateBusFeeReport()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusFeeReport";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusFeeReport)obj;
		}

		public static ICommonCustExam CreateCommonCustExam()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonCustExam";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonCustExam)obj;
		}

		public static ICommonSubScribe CreateCommonSubScribe()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonSubScribe";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonSubScribe)obj;
		}

		public static ICommonRegiste CreateCommonRegiste()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonRegiste";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonRegiste)obj;
		}

		public static ICommonUser CreateCommonUser()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonUser";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonUser)obj;
		}

		public static ICommonRight CreateCommonRight()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonRight";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonRight)obj;
		}

		public static ICommonReport CreateCommonReport()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonReport";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonReport)obj;
		}

		public static ICommonConfig CreateCommonConfig()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonConfig";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonConfig)obj;
		}

		public static IBusConclusion CreateBusConclusion()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusConclusion";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusConclusion)obj;
		}

		public static IBusConclusionType CreateBusConclusionType()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusConclusionType";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusConclusionType)obj;
		}

		public static IBusExamItem CreateBusExamItem()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusExamItem";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusExamItem)obj;
		}

		public static IBusExamItemGroup CreateBusExamItemGroup()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusExamItemGroup";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusExamItemGroup)obj;
		}

		public static IDictExamPlace CreateDictExamPlace()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictExamPlace";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictExamPlace)obj;
		}

		public static IDictExamType CreateDictExamType()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictExamType";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictExamType)obj;
		}

		public static IBusFee CreateBusFee()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusFee";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusFee)obj;
		}

		public static IBusFeeDetail CreateBusFeeDetail()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusFeeDetail";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusFeeDetail)obj;
		}

		public static IDictReceiveReportWay CreateDictReceiveReportWay()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictReceiveReportWay";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictReceiveReportWay)obj;
		}

		public static IBusPEPackage CreateBusPEPackage()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusPEPackage";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusPEPackage)obj;
		}

		public static IBusSetFeeDetail CreateBusSetFeeDetail()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusSetFeeDetail";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusSetFeeDetail)obj;
		}

		public static IBusSpecimen CreateBusSpecimen()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusSpecimen";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusSpecimen)obj;
		}

		public static IBusSymptom CreateBusSymptom()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusSymptom";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusSymptom)obj;
		}

		public static ICommonSystemInfo CreateCommonSystemInfo()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonSystemInfo";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonSystemInfo)obj;
		}

		public static IDctFinalConclusionType CreateDctFinalConclusionType()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DctFinalConclusionType";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDctFinalConclusionType)obj;
		}

		public static IDictCountry CreateDictCountry()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictCountry";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictCountry)obj;
		}

		public static IDictCultrul CreateDictCultrul()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictCultrul";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictCultrul)obj;
		}

		public static IDictFeeWay CreateDictFeeWay()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictFeeWay";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictFeeWay)obj;
		}

		public static IDictGender CreateDictGender()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictGender";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictGender)obj;
		}

		public static IDctICDTen CreateDctICDTen()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DctICDTen";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDctICDTen)obj;
		}

		public static IDictMarriage CreateDictMarriage()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictMarriage";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictMarriage)obj;
		}

		public static IDictNation CreateDictNation()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictNation";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictNation)obj;
		}

		public static IDictVocation CreateDictVocation()
		{
			string classNamespace = DataAccess.AssemblyPath + ".DictVocation";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IDictVocation)obj;
		}

		public static INatLog CreateNatLog()
		{
			string classNamespace = DataAccess.AssemblyPath + ".NatLog";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (INatLog)obj;
		}

		public static ISysMenu CreateNatMenu()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SysMenu";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISysMenu)obj;
		}

		public static ISYSRight CreateSYSRight()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SYSRight";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISYSRight)obj;
		}

		public static ISysRole CreateNatRole()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SysRole";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISysRole)obj;
		}

		public static ISysRoleRight CreateNatRoleRight()
		{
			string classNamespace = DataAccess.AssemblyPath + ".NatRoleRight";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISysRoleRight)obj;
		}

		public static ISYSUserRight CreateSYSUserRight()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SYSUserRight";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISYSUserRight)obj;
		}

		public static ISYSUserRole CreateSYSUserRole()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SYSUserRole";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISYSUserRole)obj;
		}

		public static ISYSUserSection CreateSYSUserSectionn()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SYSUserSection";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISYSUserSection)obj;
		}

		public static ISYSSection CreateSYSSection()
		{
			string classNamespace = DataAccess.AssemblyPath + ".SYSSection";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ISYSSection)obj;
		}

		public static IOnArcCust CreateOnArcCust()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnArcCust";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnArcCust)obj;
		}

		public static IOnCustApply CreateOnCustApply()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustApply";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustApply)obj;
		}

		public static IOnCustConclusion CreateOnCustConclusion()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustConclusion";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustConclusion)obj;
		}

		public static IOnCustExamItem CreateOnCustExamItem()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustExamItem";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustExamItem)obj;
		}

		public static IOnCustExamItemResult CreateOnCustExamItemResult()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustExamItemResult";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustExamItemResult)obj;
		}

		public static IOnCustExamSection CreateOnCustExamSection()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustExamSection";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustExamSection)obj;
		}

		public static IOnCustFee CreateOnCustFee()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustFee";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustFee)obj;
		}

		public static IOnCustPhysicalExamInfo CreateOnCustPhysicalExamInfo()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustPhysicalExamInfo";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustPhysicalExamInfo)obj;
		}

		public static IOnCustRelationCustPEInfo CreateOnCustRelationCustPEInfo()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustRelationCustPEInfo";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustRelationCustPEInfo)obj;
		}

		public static IOnCustReportManage CreateOnCustReportManage()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustReportManage";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustReportManage)obj;
		}

		public static IOnFianlCheck CreateOnFianlCheck()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnFianlCheck";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnFianlCheck)obj;
		}

		//public static ITeam CreateTeam()
		//{
		//	string classNamespace = DataAccess.AssemblyPath + ".Team";
		//	object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
		//	return (ITeam)obj;
		//}

		//public static ITeamTask CreateTeamTask()
		//{
		//	string classNamespace = DataAccess.AssemblyPath + ".TeamTask";
		//	object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
		//	return (ITeamTask)obj;
		//}

		//public static ITeamTaskGroup CreateTeamTaskGroup()
		//{
		//	string classNamespace = DataAccess.AssemblyPath + ".TeamTaskGroup";
		//	object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
		//	return (ITeamTaskGroup)obj;
		//}

		//public static ITeamTaskGroupCust CreateTeamTaskGroupCust()
		//{
		//	string classNamespace = DataAccess.AssemblyPath + ".TeamTaskGroupCust";
		//	object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
		//	return (ITeamTaskGroupCust)obj;
		//}

		//public static ITeamTaskGroupFee CreateTeamTaskGroupFee()
		//{
		//	string classNamespace = DataAccess.AssemblyPath + ".TeamTaskGroupFee";
		//	object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
		//	return (ITeamTaskGroupFee)obj;
		//}

		public static ICommonExcuteSql CreateCommonExcuteSql()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonExcuteSql";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonExcuteSql)obj;
		}

		public static ICommonStatistics CreateCommonStatistics()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommonStatistics";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommonStatistics)obj;
		}

		public static ICommandWebserviceInterface CreateCommandWebserviceInterface()
		{
			string classNamespace = DataAccess.AssemblyPath + ".CommandWebserviceInterface";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (ICommandWebserviceInterface)obj;
		}

		public static IBusBackLogType CreateBusBackLogType()
		{
			string classNamespace = DataAccess.AssemblyPath + ".BusBackLogType";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IBusBackLogType)obj;
		}

		public static IOnCustBackLog CreateOnCustBackLog()
		{
			string classNamespace = DataAccess.AssemblyPath + ".OnCustBackLog";
			object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
			return (IOnCustBackLog)obj;
		}

        public static ISYSOpUser CreateSYSOpUser()
        {
            string classNamespace = DataAccess.AssemblyPath + ".SYSOpUser";
            object obj = DataAccess.CreateObject(DataAccess.AssemblyPath, classNamespace);
            return (ISYSOpUser)obj;
        }
    }
}
