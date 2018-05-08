using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace PEIS.SQLServerDAL
{
	public class CommonReport : CommonBase, ICommonReport
	{
		protected string[] QueryCustomerGuideReport_Param = new string[]
		{
			"SELECT CASE WHEN Is_GuideSheetPrinted=1 THEN FLOOR(DATEDIFF(DY,CONVERT(varchar(10),BirthDay,120),OperateDate)/365.25) ELSE FLOOR(DATEDIFF(DY,CONVERT(varchar(10),BirthDay,120),GETDATE())/365.25) END Age,OnCustPhysicalExamInfo.Note,RoleName,[dbo].StrToCode128(OnCustPhysicalExamInfo.ID_Customer) ID_CustomerCode128\r\n            ,OnCustPhysicalExamInfo.ID_Customer,CustomerName,MarriageName,GenderName,CONVERT(varchar(10),BirthDay,120) date,IDCard,MobileNo,Photo,CONVERT(varchar(120),GETDATE(),120) ServerDate,CASE WHEN ISNULL(Is_GuideSheetPrinted,0)=0 THEN CONVERT(varchar(120),GETDATE(),120) ELSE CONVERT(varchar(120),OperateDate,120) END SubScribDate,OnCustPhysicalExamInfo.ExamTypeName+'指引单' ExamTypeName,ExamPlaceName,Team.TeamName,\r\n            REPLACE(RTRIM(ISNULL(TeamTaskGroupCust.Department,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubA,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubB,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubC,'')),' ','-')Department \r\n            FROM  OnCustPhysicalExamInfo  \r\n            LEFT JOIN Team ON OnCustPhysicalExamInfo.ID_Team=Team.ID_Team\r\n            LEFT JOIN TeamTaskGroupCust ON OnCustPhysicalExamInfo.ID_Customer=TeamTaskGroupCust.ID_Customer\r\n            LEFT JOIN TeamTaskGroup ON TeamTaskGroupCust.ID_TeamTaskGroup=TeamTaskGroup.ID_TeamTaskGroup\r\n            WHERE OnCustPhysicalExamInfo.ID_Customer=@ID_Customer;\r\n\r\n           --指引单收费信息部分，通过体检号查找用户所有收费项目\r\n           --1.将收费项目按照其排序顺序保存到零时表中，以便合并收费项目显示时查询\r\n            SELECT CASE BusFee.BreakfastOrder WHEN 1 THEN '√' ELSE '' END BreakfastOrder,BusFee.ID_Section,BusFee.SectionName,ISNULL(BusFee.ID_Specimen,'') ID_Specimen,BusFee.FeeName\r\n            INTO @TempBusFee FROM(SELECT ID_Fee FROM OnCustFee WHERE ISNULL(CustFeeChargeState,0)!=2 AND ISNULL(Is_Printed,0) IN(@Is_Printed) AND ID_Customer=@ID_Customer) OnCustFee\r\n            LEFT JOIN BusFee ON OnCustFee.ID_Fee=BusFee.ID_Fee ORDER BY BusFee.DispOrder\r\n           --2.从中间表中查询收费项目信息           \r\n            SELECT SYSSection.SectionName,BusSpecimen.SpecimenName,REPLACE(TEMPSYSSection.FeeName,'√','') FeeName,CASE WHEN CHARINDEX('√',TEMPSYSSection.FeeName,0)>0 THEN '√' ELSE '' END BreakfastOrder FROM(\r\n            SELECT ID_Section,ID_Specimen\r\n            ,STUFF((SELECT ','+FeeName+BreakfastOrder FROM @TempBusFee T2\r\n            WHERE T2.ID_SECTION=T1.ID_SECTION AND T2.ID_Specimen=T1.ID_Specimen FOR XML PATH('')),1,1,'') FeeName\r\n            FROM @TempBusFee T1 GROUP BY ID_Section,ID_Specimen) TEMPSYSSection\r\n            LEFT JOIN SYSSection ON TEMPSYSSection.ID_Section=SYSSection.ID_Section\r\n            LEFT JOIN BusSpecimen ON TEMPSYSSection.ID_Specimen=BusSpecimen.ID_Specimen\r\n            ORDER BY SYSSection.DispOrder,BusSpecimen.DispOrder;\r\n            DROP TABLE @TempBusFee\r\n          "
		};

		protected string[] QueryCustomerExamResultReport_Param = new string[]
		{
			"--获取用户的基本信息 CustomerInfo\r\nSELECT OnCustPhysicalExamInfo.ID_Customer,NormalDiagnose,CustomerName,MarriageName,GenderName,CONVERT(varchar(10),BirthDay,120) date,IDCard,MobileNo,Photo,SecondaryDiagnose,IndicatorDiagnose,OtherDiagnose,CASE WHEN Is_GuideSheetPrinted=1 THEN FLOOR(DATEDIFF(DY,date,OperateDate)/365.25) ELSE FLOOR(DATEDIFF(DY,date,GETDATE())/365.25) END Age,CONVERT(varchar(120),GETDATE(),120) ServerDate,Checker,CONVERT(varchar(10),CheckedDate,120) CheckedDate,OnCustPhysicalExamInfo.Note,RoleName,OnCustPhysicalExamInfo.ResultCompare,OnCustPhysicalExamInfo.FinalDietGuide,OnCustPhysicalExamInfo.FinalSportGuide,OnCustPhysicalExamInfo.FinalHealthKnowlage,OnCustPhysicalExamInfo.MainDiagnose,OnCustPhysicalExamInfo.FinalDoctor,ISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.FinalDate,120),'')FinalDate,[dbo].StrToCode128(OnCustPhysicalExamInfo.ID_Customer) ID_CustomerCode128,CONVERT(varchar(10),OnCustPhysicalExamInfo.OperateDate,120) SubScribDate,OnCustPhysicalExamInfo.ExamTypeName ExamTypeName,Team.TeamName,\r\nISNULL(TeamTaskGroupCust.Department,'') DepartmentA,REPLACE(RTRIM(ISNULL(TeamTaskGroupCust.Department,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubA,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubB,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubC,'')),' ','--')Department \r\nFROM (SELECT *,CONVERT(varchar(10),BirthDay,120) date FROM OnCustPhysicalExamInfo WHERE ID_Customer=@ID_Customer)OnCustPhysicalExamInfo \r\nLEFT JOIN Team ON OnCustPhysicalExamInfo.ID_Team=Team.ID_Team\r\nLEFT JOIN TeamTaskGroupCust ON OnCustPhysicalExamInfo.ID_Customer=TeamTaskGroupCust.ID_Customer\r\nLEFT JOIN TeamTaskGroup ON TeamTaskGroupCust.ID_TeamTaskGroup=TeamTaskGroup.ID_TeamTaskGroup\r\nWHERE OnCustPhysicalExamInfo.ID_Customer=@ID_Customer;\r\n\r\n--获取用户检查项目包含的科室信息 SectionExamInfo[检验科：101]\r\nSELECT ROW_NUMBER() over(order by SYSSection.DispOrder ASC) RowNum,OnCustExamSection.ExamProjectName,SYSSection.InterfaceType,SYSSection.Is_NotSamePage,SYSSection.Is_NonPrintInReport,SYSSection.SummaryName,OnCustExamSection.* FROM (SELECT ExamProjectName,TypistName,ImageUrl,Is_Check,ID_Section,ID_Customer,SectionName,SectionSummary,SummaryDoctorName,ISNULL(CONVERT(varchar(10),CheckDate,120),'') CheckDate FROM OnCustExamSection \r\nWHERE ID_Customer=@ID_Customer) OnCustExamSection\r\nINNER JOIN SYSSection ON OnCustExamSection.ID_Section=SYSSection.ID_Section AND ISNULL(SYSSection.Is_NonPrintInReport,0)=0 \r\nWHERE Is_Check=1 ORDER BY SYSSection.DispOrder ASC;\r\n\r\n----获取用户检查项目信息 CustExamInfo\r\nSELECT CONVERT(VARCHAR,A.ID_FeeReportMergerX)+ISNULL(A.ID_CustApply,'') ID_FeeReportMerger,B.SpecimenName,B.SpecimenNo,B.BatchNumber,B.ExamNumber,B.DeptName,ISNULL(CONVERT(varchar(20),B.AcquisitionTime,120),'') AcquisitionTime,ISNULL(CONVERT(varchar(20),B.RecvTime,120),'')RecvTime,B.ApplyDoctorName,ISNULL(CONVERT(varchar(20),B.ReportTime,120),'')ReportTime,B.DetectionDoctorName,B.CheckDoctorName,A.* FROM(SELECT SectionExamData.*,ExamItemData.*,BFMerger.DispOrder MergerDispOrder,BFMerger.WorkGroupCode+'|'+BFMerger.WorkStationCode +'|'+BFMerger.LisSpecimenName BFMergerSendGroupParams FROM \r\n(SELECT SYSSection.DispOrder SectionDispOrder,SYSSection.SectionName,SectionSummary,SYSSection.DispOrder,SYSSection.ID_Section\r\nFROM SYSSection,\r\n(SELECT Is_Check,ID_Section,SectionName,SectionSummary FROM OnCustExamSection WHERE ID_Customer=@ID_Customer) OnCustExamSection\r\nWHERE \r\n OnCustExamSection.ID_Section = SYSSection.ID_Section AND ISNULL(SYSSection.Is_NonPrintInReport,0)=0\r\nAND Is_Check=1 AND SectionSummary IS NOT NULL) SectionExamData\r\nLEFT JOIN \r\n(SELECT [OnCustExamItem].ID_CustApply,FeeCode,DetectionMethod,AbbrExamName,CONVERT(VARCHAR(1),ISNULL(IS_FeeReportMerger,0))IS_FeeReportMergered,BusFee.DispOrder FeeDispOrder,BusExamItem.ID_ExamItem,BusExamItem.DispOrder ExamItemDispOrder,ExamDoctorName,ISNULL(CONVERT(varchar(10),ExamDate,120),'') ExamDate,ResultLabMark,\r\nCASE ISNULL(ResultNumber,0) WHEN 0 THEN ResultLabValues ELSE CONVERT(nvarchar(50),ResultNumber) END ResultValue,\r\nResultLabUnit,ResultLabRange,SCO,OnCustExamItem.ExamItemName,OnCustExamItem.ResultSummary,BusFee.ID_SectionX,\r\nBusFee.ID_FeeReportMerger ID_FeeReportMergerX,BusFee.ID_Fee,OnCustFee.ID_CustFee,OnCustFee.ID_Customer,BusFee.FeeName FeeItemName,OnCustFee.ApplyID,OnCustFee.FeeImageUrl\r\nFROM\r\n[OnCustExamItem],(SELECT ID_ExamItem,DispOrder FROM BusExamItem WHERE ISNULL(Is_ExamItemNonPrintInReport,0)=0)BusExamItem,\r\n(SELECT FeeCode,IS_FeeReportMerger,ISNULL(ID_FeeReportMerger,ID_Fee)ID_FeeReportMerger,ID_Fee,ID_Section ID_SectionX,ReportFeeName FeeName,DispOrder FROM BusFee WHERE ISNULL(Is_FeeNonPrintInReport,0)=0)\r\nAS BusFee,\r\n(SELECT ID_CustFee,ID_Customer,ID_Fee,ExamDoctorName,ExamDate,ApplyID,FeeImageUrl FROM OnCustFee WHERE OnCustFee.ID_Customer=@ID_Customer ) \r\nAS OnCustFee,\r\n(SELECT ID_ExamItem,ID_Fee FROM BusFeeDetail WHERE EXISTS(SELECT ID_Fee FROM BusFee WHERE ISNULL(Is_FeeNonPrintInReport,0)=0 AND \r\nBusFeeDetail.ID_Fee=BusFee.ID_Fee))AS BusFeeDetail\r\nWHERE \r\nBusExamItem.ID_ExamItem = [OnCustExamItem].ID_ExamItem\r\nAND [OnCustExamItem].ID_CustFee = OnCustFee.ID_CustFee\r\nAND BusExamItem.ID_ExamItem = BusFeeDetail.ID_ExamItem\r\nAND OnCustFee.ID_Fee = BusFee.ID_Fee\r\nAND BusFeeDetail.ID_Fee = BusFee.ID_Fee\r\n) ExamItemData \r\nON SectionExamData.ID_Section = ExamItemData.ID_SectionX \r\nLEFT JOIN (SELECT BusFee.*,BusSpecimen.LisSpecimenName FROM BusFee,BusSpecimen WHERE BusFee.ID_Specimen = BusSpecimen.ID_Specimen) BFMerger ON ExamItemData.ID_FeeReportMergerX=BFMerger.ID_Fee)A\r\nLEFT JOIN (SELECT * FROM OnCustApply WHERE ID_Customer=@ID_Customer) B ON A.ID_CustApply = B.ID_Apply OR (A.ID_Customer=B.ID_Customer AND B.SendProjectIDs LIKE '%;'+CONVERT(varchar(20),A.FeeCode)+';%')\r\n--ORDER BY SectionExamData.DispOrder,ExamItemData.FeeDispOrder,ExamItemData.ExamItemDispOrder ASC;--由于在OCX中检索检查项目时进行了前面三项的排序，这里可以省略以提高sql查询效率\r\n          "
		};

		protected string[] QueryCustomerExamResultReportOfCustom_Param = new string[]
		{
			"--获取用户的基本信息 CustomerInfo\r\nSELECT OnCustPhysicalExamInfo.ID_Customer,NormalDiagnose,CustomerName,MarriageName,GenderName,CONVERT(varchar(10),BirthDay,120) date,IDCard,MobileNo,Photo,SecondaryDiagnose,IndicatorDiagnose,OtherDiagnose,CASE WHEN Is_GuideSheetPrinted=1 THEN FLOOR(DATEDIFF(DY,date,OperateDate)/365.25) ELSE FLOOR(DATEDIFF(DY,date,GETDATE())/365.25) END Age,CONVERT(varchar(120),GETDATE(),120) ServerDate,Checker,CONVERT(varchar(10),CheckedDate,120) CheckedDate,OnCustPhysicalExamInfo.Note,RoleName,OnCustPhysicalExamInfo.ResultCompare,OnCustPhysicalExamInfo.FinalDietGuide,OnCustPhysicalExamInfo.FinalSportGuide,OnCustPhysicalExamInfo.FinalHealthKnowlage,OnCustPhysicalExamInfo.MainDiagnose,OnCustPhysicalExamInfo.FinalDoctor,ISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.FinalDate,120),'')FinalDate,[dbo].StrToCode128(OnCustPhysicalExamInfo.ID_Customer) ID_CustomerCode128,CONVERT(varchar(10),OnCustPhysicalExamInfo.OperateDate,120) SubScribDate,OnCustPhysicalExamInfo.ExamTypeName ExamTypeName,Team.TeamName,\r\nISNULL(TeamTaskGroupCust.Department,'') DepartmentA,REPLACE(RTRIM(ISNULL(TeamTaskGroupCust.Department,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubA,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubB,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubC,'')),' ','--')Department \r\nFROM (SELECT *,CONVERT(varchar(10),BirthDay,120) date FROM OnCustPhysicalExamInfo WHERE ID_Customer=@ID_Customer)OnCustPhysicalExamInfo \r\nLEFT JOIN Team ON OnCustPhysicalExamInfo.ID_Team=Team.ID_Team\r\nLEFT JOIN TeamTaskGroupCust ON OnCustPhysicalExamInfo.ID_Customer=TeamTaskGroupCust.ID_Customer\r\nLEFT JOIN TeamTaskGroup ON TeamTaskGroupCust.ID_TeamTaskGroup=TeamTaskGroup.ID_TeamTaskGroup\r\nWHERE OnCustPhysicalExamInfo.ID_Customer=@ID_Customer;\r\n\r\n--获取用户检查项目包含的科室信息 SectionExamInfo[检验科：101]\r\nSELECT ROW_NUMBER() over(order by SYSSection.DispOrder ASC) RowNum,OnCustExamSection.ExamProjectName,SYSSection.InterfaceType,SYSSection.Is_NotSamePage,SYSSection.Is_NonPrintInReport,SYSSection.SummaryName,OnCustExamSection.* FROM (SELECT ExamProjectName,TypistName,ImageUrl,Is_Check,ID_Section,ID_Customer,SectionName,SectionSummary,SummaryDoctorName,ISNULL(CONVERT(varchar(10),CheckDate,120),'') CheckDate FROM OnCustExamSection \r\nWHERE ID_Customer=@ID_Customer) OnCustExamSection\r\nINNER JOIN SYSSection ON OnCustExamSection.ID_Section=SYSSection.ID_Section AND ISNULL(SYSSection.Is_NonPrintInReport,0)=0 \r\nWHERE Is_Check=1 ORDER BY SYSSection.DispOrder ASC;\r\n\r\n----获取用户检查项目信息 CustExamInfo\r\nSELECT CONVERT(VARCHAR,A.ID_FeeReportMergerX)+ISNULL(A.ID_CustApply,'') ID_FeeReportMerger,AbbrExamName,Note,TeampLate,NotePic,GroupName,SCO,B.SpecimenName,B.SpecimenNo,B.BatchNumber,B.ExamNumber\r\n,B.DeptName,ISNULL(CONVERT(varchar(20),B.AcquisitionTime,120),'') AcquisitionTime,ISNULL(CONVERT(varchar(20),B.RecvTime,120),'')RecvTime\r\n,B.ApplyDoctorName,ISNULL(CONVERT(varchar(20),B.ReportTime,120),'')ReportTime,B.DetectionDoctorName,B.CheckDoctorName,B.TypistName\r\n,B.TypistDate,A.* FROM(SELECT SectionExamData.*,ExamItemData.*,BFMerger.DispOrder MergerDispOrder FROM \r\n(SELECT SYSSection.DispOrder SectionDispOrder,SYSSection.SectionName,SectionSummary,SYSSection.DispOrder,SYSSection.ID_Section\r\nFROM SYSSection,\r\n(SELECT Is_Check,ID_Section,SectionName,SectionSummary FROM OnCustExamSection WHERE ID_Customer=@ID_Customer) OnCustExamSection\r\nWHERE \r\n OnCustExamSection.ID_Section = SYSSection.ID_Section AND ISNULL(SYSSection.Is_NonPrintInReport,0)=0\r\nAND Is_Check=1 AND SectionSummary IS NOT NULL) SectionExamData\r\nLEFT JOIN \r\n(SELECT [OnCustExamItem].ID_CustApply,SCO,FeeCode,DetectionMethod,AbbrExamName,CONVERT(VARCHAR(1),ISNULL(IS_FeeReportMerger,0))IS_FeeReportMergered,BusFee.DispOrder FeeDispOrder,BusFee.NotePic,BusFee.Note,BusFee.ReportKey TeampLate,BusExamItem.ID_ExamItem,ISNULL(BusExamItem.ID_ExamItemGroup,-1)ID_ExamItemGroup,BusExamItem.ExamItemGroupName GroupName,BusExamItem.DispOrder ExamItemDispOrder,ExamDoctorName,ISNULL(CONVERT(varchar(10),ExamDate,120),'') ExamDate,ResultLabMark,\r\nCASE ISNULL(ResultNumber,0) WHEN 0 THEN ResultLabValues ELSE CONVERT(nvarchar(50),ResultNumber) END ResultValue,\r\nResultLabUnit,ResultLabRange,OnCustExamItem.ExamItemName,OnCustExamItem.ResultSummary,BusFee.ID_SectionX,\r\nBusFee.ID_FeeReportMerger ID_FeeReportMergerX,BusFee.ID_Fee,OnCustFee.ID_CustFee,OnCustFee.ID_Customer,BusFee.FeeName FeeItemName,OnCustFee.ApplyID,OnCustFee.FeeImageUrl\r\nFROM\r\n[OnCustExamItem],(SELECT BEI.*,BEIG.ExamItemGroupName FROM(SELECT ID_ExamItem,DispOrder,ID_ExamItemGroup FROM BusExamItem WHERE ISNULL(Is_ExamItemNonPrintInReport,0)=0) BEI\r\nLEFT JOIN BusExamItemGroup BEIG ON BEI.ID_ExamItemGroup=BEIG.ID_ExamItemGroup\r\n)BusExamItem,\r\n(SELECT BF.*,BFR.ImageUrl NotePic,BFR.Note,BFR.ReportKey FROM(SELECT FeeCode,IS_FeeReportMerger,ISNULL(ID_FeeReportMerger,ID_Fee)ID_FeeReportMerger,ID_Fee,ID_Section ID_SectionX,ReportFeeName FeeName,DispOrder FROM BusFee WHERE ISNULL(Is_FeeNonPrintInReport,0)=0)BF\r\nLEFT JOIN BusFeeReport BFR ON BF.ID_Fee=BFR.ID_Fee)\r\nAS BusFee,\r\n(SELECT ID_CustFee,ID_Customer,ID_Fee,ExamDoctorName,ExamDate,ApplyID,FeeImageUrl FROM OnCustFee WHERE OnCustFee.ID_Customer=@ID_Customer ) \r\nAS OnCustFee,\r\n(SELECT ID_ExamItem,ID_Fee FROM BusFeeDetail WHERE EXISTS(SELECT ID_Fee FROM BusFee WHERE ISNULL(Is_FeeNonPrintInReport,0)=0 AND \r\nBusFeeDetail.ID_Fee=BusFee.ID_Fee))AS BusFeeDetail\r\nWHERE BusExamItem.ID_ExamItem = [OnCustExamItem].ID_ExamItem\r\nAND [OnCustExamItem].ID_CustFee = OnCustFee.ID_CustFee\r\nAND BusExamItem.ID_ExamItem = BusFeeDetail.ID_ExamItem\r\nAND OnCustFee.ID_Fee = BusFee.ID_Fee\r\nAND BusFeeDetail.ID_Fee = BusFee.ID_Fee\r\n) ExamItemData ON SectionExamData.ID_Section = ExamItemData.ID_SectionX \r\nLEFT JOIN BusFee BFMerger ON ExamItemData.ID_FeeReportMergerX=BFMerger.ID_Fee)A\r\nLEFT JOIN (SELECT * FROM OnCustApply WHERE ID_Customer=@ID_Customer) B ON A.ID_CustApply = B.ID_Apply OR (A.ID_Customer=B.ID_Customer AND B.SendProjectIDs LIKE '%;'+CONVERT(varchar(20),A.FeeCode)+';%')\r\n--ORDER BY SectionExamData.DispOrder,ExamItemData.FeeDispOrder,ExamItemData.ExamItemDispOrder ASC;--由于在OCX中检索检查项目时进行了前面三项的排序，这里可以省略以提高sql查询效率\r\n          "
		};

		protected string[] QueryCustomerChargesAndCredence_Param = new string[]
		{
			"--查询用户预约、登记信息和预约凭证信息 CustomerInfo\r\n SELECT OnArcCust.CustomerName,OnArcCust.GenderName,[dbo].StrToCode128(OnCustPhysicalExamInfo.ID_Customer) ID_CustomerCode128,OnCustPhysicalExamInfo.ID_Customer,CONVERT(varchar(10),OnCustPhysicalExamInfo.OperateDate,120) OperateDate,Operator,CONVERT(varchar(10),OnCustPhysicalExamInfo.SubScribDate,120) SubScribDate FROM(SELECT * FROM OnCustPhysicalExamInfo WHERE ID_Customer=@ID_Customer) OnCustPhysicalExamInfo\r\n      INNER JOIN (SELECT ID_ArcCustomer,ID_Customer FROM OnCustRelationCustPEInfo WHERE ID_Customer=@ID_Customer) OnCustRelationCustPEInfo ON OnCustPhysicalExamInfo.ID_Customer=OnCustRelationCustPEInfo.ID_Customer\r\n      INNER JOIN OnArcCust ON OnCustRelationCustPEInfo.ID_ArcCustomer=OnArcCust.ID_ArcCustomer;\r\n          "
		};

		protected string[] QueryCustFeeDeatil_Param = new string[]
		{
			"--查询用户预约、登记信息和预约凭证信息 CustomerInfo\r\n SELECT ISNULL(ExamState,0) ExamState,OnArcCust.CustomerName,OnArcCust.GenderName,OnArcCust.MobileNo,[dbo].StrToCode128(OnCustPhysicalExamInfo.ID_Customer) ID_CustomerCode128,OnCustPhysicalExamInfo.ID_Customer,CONVERT(varchar(10),OnCustPhysicalExamInfo.OperateDate,120) OperateDate,Operator,CONVERT(varchar(10),OnCustPhysicalExamInfo.SubScribDate,120) SubScribDate FROM(SELECT * FROM OnCustPhysicalExamInfo WHERE ID_Customer=@ID_Customer) OnCustPhysicalExamInfo\r\n      INNER JOIN (SELECT ID_ArcCustomer,ID_Customer,ExamState FROM OnCustRelationCustPEInfo WHERE ID_Customer=@ID_Customer) OnCustRelationCustPEInfo ON OnCustPhysicalExamInfo.ID_Customer=OnCustRelationCustPEInfo.ID_Customer\r\n      INNER JOIN OnArcCust ON OnCustRelationCustPEInfo.ID_ArcCustomer=OnArcCust.ID_ArcCustomer;\r\nSELECT  OCF.FeeItemName,OCF.FeeChargeStaute,CASE OCF.FeeChargeStaute WHEN '已退' THEN -OCF.FactPrice ELSE OCF.FactPrice END FactPrice FROM (SELECT BF.DispOrder,OCF.FeeItemName,CASE WHEN OCF.Is_FeeRefund=1 THEN '已退' WHEN OCF.Is_FeeCharged=1 THEN '已收' ELSE '未收' END FeeChargeStaute,OCF.FactPrice FROM(SELECT * FROM OnCustFee WHERE ID_Customer=@ID_Customer) OCF\r\nINNER JOIN BusFee BF ON OCF.ID_Fee=BF.ID_Fee)OCF ORDER BY OCF.DispOrder ASC;\r\n          "
		};

		protected string[] QuerySectionBarCode_Param = new string[]
		{
			"--查询客户基本信息\r\nSELECT OCRCPE.ID_ArcCustomer,OCPE.ID_Customer,[dbo].StrToCode128(OCPE.ID_Customer)ID_CustomerCode128,CustomerName,FLOOR(DATEDIFF(DY,BirthDay,(SELECT CASE WHEN Is_GuideSheetPrinted=1 THEN OperateDate ELSE GETDATE() END OperateDate))/365.25) Age,Case ID_Gender WHEN 1 THEN '男' ELSE '女' End Sex,ISNULL(TeamName,'')TeamName,ISNULL(Department,'')Department,ISNULL(DepartSubA,'')DepartSubA,ISNULL(DepartSubB,'')DepartSubB,ISNULL(DepartSubC,'')DepartSubC FROM OnCustPhysicalExamInfo OCPE,OnCustRelationCustPEInfo OCRCPE WHERE OCPE.ID_Customer=@ID_Customer AND OCPE.ID_Customer=OCRCPE.ID_Customer;\r\n--查询科室对应的收费项目\r\nSELECT OCES.ID_Customer,NS.ID_Section,NS.SectionName,OCF.FeeItemName,NS.Is_PrintBarCode,OCF.ApplyID,[dbo].StrToCode128(OCF.ApplyID) ApplyIDCode128C FROM(SELECT * FROM OnCustExamSection WHERE ID_Customer=@ID_Customer) OCES\r\nINNER JOIN (SELECT * FROM SYSSection WHERE ID_Section=@ID_Section) NS ON OCES.ID_Section=NS.ID_Section\r\nINNER JOIN BusFee BF ON OCES.ID_Section=BF.ID_Section\r\nINNER JOIN (SELECT ApplyID,ID_Customer,ID_CustFee,ID_Fee,FeeItemName,ExamDoctorName,ExamDate FROM OnCustFee WHERE ISNULL(CustFeeChargeState,0)!=2 AND ID_Customer=@ID_Customer) OCF ON BF.ID_Fee=OCF.ID_Fee ORDER BY NS.DispOrder ASC;"
		};

		protected string[] QueryAllSectionBarCode_Param = new string[]
		{
			"--查询客户基本信息\r\nSELECT OCRCPE.ID_ArcCustomer,OCPE.ID_Customer,[dbo].StrToCode128(OCPE.ID_Customer)ID_CustomerCode128,CustomerName,FLOOR(DATEDIFF(DY,BirthDay,(SELECT CASE WHEN Is_GuideSheetPrinted=1 THEN OperateDate ELSE GETDATE() END OperateDate))/365.25) Age,Case ID_Gender WHEN 1 THEN '男' ELSE '女' End Sex,ISNULL(TeamName,'')TeamName,ISNULL(Department,'')Department,ISNULL(DepartSubA,'')DepartSubA,ISNULL(DepartSubB,'')DepartSubB,ISNULL(DepartSubC,'')DepartSubC FROM OnCustPhysicalExamInfo OCPE,OnCustRelationCustPEInfo OCRCPE WHERE OCPE.ID_Customer=@ID_Customer AND OCPE.ID_Customer=OCRCPE.ID_Customer;\r\n--查询科室对应的收费项目\r\nSELECT OCES.ID_Customer,NS.ID_Section,NS.SectionName,OCF.FeeItemName,NS.Is_PrintBarCode,OCF.ApplyID,[dbo].StrToCode128(OCF.ApplyID) ApplyIDCode128C FROM(SELECT * FROM OnCustExamSection WHERE ID_Customer=@ID_Customer) OCES\r\nINNER JOIN SYSSection NS ON OCES.ID_Section=NS.ID_Section\r\nINNER JOIN BusFee BF ON OCES.ID_Section=BF.ID_Section\r\nINNER JOIN (SELECT ApplyID,ID_Customer,ID_CustFee,ID_Fee,FeeItemName,ExamDoctorName,ExamDate FROM OnCustFee WHERE ISNULL(CustFeeChargeState,0)!=2 AND ID_Customer=@ID_Customer) OCF ON BF.ID_Fee=OCF.ID_Fee ORDER BY NS.DispOrder ASC;"
		};

		protected string[] QueryCustomerReportManage_Param = new string[]
		{
			"SELECT OnCustPhysicalExamInfo.ID_Customer,OnArcCust.CustomerName,OnArcCust.GenderName,OnArcCust.date,Team.TeamName,OnCustPhysicalExamInfo.Checker,ISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.CheckedDate,120),'') CheckedDate\r\n,OnCustReportManage.ReportPrinter,ISNULL(CONVERT(varchar(10),OnCustReportManage.ReportPrintedDate,120),'') ReportPrintedDate,OnCustReportManage.Informer,ISNULL(CONVERT(varchar(10),OnCustReportManage.InformedDate,120),'') InformedDate,OnCustReportManage.ReportReceiptor,ISNULL(CONVERT(varchar(10),OnCustReportManage.ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(OnCustPhysicalExamInfo.Is_Checked,0) Is_Checked,ISNULL(OnCustReportManage.Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(OnCustReportManage.Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,\r\nOnCustPhysicalExamInfo.FinalDoctor,ISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.FinalDate,120),'')FinalDate\r\n,REPLACE(RTRIM(ISNULL(TeamTaskGroupCust.Department,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubA,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubB,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubC,'')),' ','-')Department FROM(SELECT ID_Customer,CustomerName,MarriageName,GenderName,CONVERT(varchar(10),BirthDay,120) date,IDCard,MobileNo,Photo FROM OnArcCust\r\nINNER JOIN OnCustRelationCustPEInfo ON OnArcCust.ID_ArcCustomer=OnCustRelationCustPEInfo.ID_ArcCustomer\r\nWHERE OnCustRelationCustPEInfo.ID_Customer IN(@ID_Customer)) OnArcCust\r\nINNER JOIN OnCustPhysicalExamInfo ON OnArcCust.ID_Customer=OnCustPhysicalExamInfo.ID_Customer\r\nLEFT JOIN Team ON OnCustPhysicalExamInfo.ID_Team=Team.ID_Team\r\nLEFT JOIN TeamTaskGroupCust ON OnCustPhysicalExamInfo.ID_Customer=TeamTaskGroupCust.ID_Customer\r\nLEFT JOIN OnCustReportManage ON OnArcCust.ID_Customer=OnCustReportManage.ID_Customer\r\nWHERE OnCustPhysicalExamInfo.ID_Customer IN(@ID_Customer);\r\n          "
		};

		protected string[] QueryISCanReadReport_Param = new string[]
		{
			"SELECT ISNULL(SecurityLevel,0) SecurityLevel FROM OnCustPhysicalExamInfo WHERE OnCustPhysicalExamInfo.ID_Customer IN(@ID_Customer);"
		};

		protected string[] CustomerReportPrintAllTeamInfoPagesParam = new string[]
		{
			"SELECT DISTINCT Team.ID_Team,Team.TeamName FROM(SELECT ID_Team FROM OnCustPhysicalExamInfo WHERE Is_Checked=1 AND ID_Team IS NOT NULL)OnCustPhysicalExamInfo INNER JOIN Team ON OnCustPhysicalExamInfo.ID_Team=Team.ID_Team"
		};

		protected string[] CustomerReportPrintInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			"(SELECT * FROM(SELECT OnCustPhysicalExamInfo.ID_ExamType,OnCustPhysicalExamInfo.ExamTypeName,OnCustPhysicalExamInfo.ID_Team,IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,TeamName,Checker,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,ReportPrinter,ISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,Informer,ISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,ReportReceiptor,ISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,FinalDoctor,ISNULL(CONVERT(varchar(10),FinalDate,120),'')FinalDate,REPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM (SELECT * FROM OnCustPhysicalExamInfo WHERE Is_Checked=1 AND ISNULL(OnCustPhysicalExamInfo.SecurityLevel,0)<@SecurityLevel)OnCustPhysicalExamInfo LEFT JOIN OnCustReportManage ON OnCustPhysicalExamInfo.ID_Customer=OnCustReportManage.ID_Customer)A WHERE A.Is_ReportPrinted=0)",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] CustomerReportPrintInfoPagesParamNoCheck = new string[]
		{
			"ID_Customer",
			"*",
			"(SELECT * FROM(SELECT OnCustPhysicalExamInfo.ID_ExamType,OnCustPhysicalExamInfo.ExamTypeName,OnCustPhysicalExamInfo.ID_Team,IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,TeamName,Checker,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,ReportPrinter,ISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,Informer,ISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,ReportReceiptor,ISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,FinalDoctor,ISNULL(CONVERT(varchar(10),FinalDate,120),'')FinalDate,REPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM (SELECT * FROM OnCustPhysicalExamInfo WHERE ISNULL(OnCustPhysicalExamInfo.SecurityLevel,0)<@SecurityLevel)OnCustPhysicalExamInfo LEFT JOIN OnCustReportManage ON OnCustPhysicalExamInfo.ID_Customer=OnCustReportManage.ID_Customer)A WHERE A.Is_ReportPrinted=0)",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] CustomerReportPrintSelfInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			"(SELECT * FROM(SELECT OnCustPhysicalExamInfo.ID_ExamType,OnCustPhysicalExamInfo.ExamTypeName,OnCustPhysicalExamInfo.ID_Team,IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,TeamName,Checker,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,ReportPrinter,ISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,Informer,ISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,ReportReceiptor,ISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,FinalDoctor,ISNULL(CONVERT(varchar(10),FinalDate,120),'')FinalDate,REPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM (SELECT * FROM OnCustPhysicalExamInfo WHERE Is_Checked=1 AND ID_Team IS NULL AND ISNULL(OnCustPhysicalExamInfo.SecurityLevel,0)<@SecurityLevel)OnCustPhysicalExamInfo LEFT JOIN OnCustReportManage ON OnCustPhysicalExamInfo.ID_Customer=OnCustReportManage.ID_Customer)A WHERE A.Is_ReportPrinted=0)",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] CustomerReportPrintTeamInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			"(SELECT * FROM(SELECT OnCustPhysicalExamInfo.ID_ExamType,OnCustPhysicalExamInfo.ExamTypeName,OnCustPhysicalExamInfo.ID_Team,IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,TeamName,Checker,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,ReportPrinter,ISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,Informer,ISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,ReportReceiptor,ISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,FinalDoctor,ISNULL(CONVERT(varchar(10),FinalDate,120),'')FinalDate,REPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM (SELECT * FROM OnCustPhysicalExamInfo WHERE Is_Checked=1 AND ID_Team IS NOT NULL AND ISNULL(OnCustPhysicalExamInfo.SecurityLevel,0)<@SecurityLevel)OnCustPhysicalExamInfo LEFT JOIN OnCustReportManage ON OnCustPhysicalExamInfo.ID_Customer=OnCustReportManage.ID_Customer)A WHERE A.Is_ReportPrinted=0)",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] CustomerReportPriViewInfoPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			"(SELECT OnArcCust.IDCard,OnArcCust.MobileNo,OnCustPhysicalExamInfo.ID_Customer,OnArcCust.CustomerName,OnArcCust.GenderName,OnArcCust.date,Team.TeamName,OnCustPhysicalExamInfo.Checker,ISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.CheckedDate,120),'') CheckedDate,OnCustReportManage.ReportPrinter,ISNULL(CONVERT(varchar(10),OnCustReportManage.ReportPrintedDate,120),'') ReportPrintedDate,OnCustReportManage.Informer,ISNULL(CONVERT(varchar(10),OnCustReportManage.InformedDate,120),'') InformedDate,OnCustReportManage.ReportReceiptor,ISNULL(CONVERT(varchar(10),OnCustReportManage.ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(OnCustPhysicalExamInfo.Is_Checked,0) Is_Checked,ISNULL(OnCustReportManage.Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(OnCustReportManage.Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,OnCustPhysicalExamInfo.FinalDoctor,ISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.FinalDate,120),'')FinalDate,REPLACE(RTRIM(ISNULL(TeamTaskGroupCust.Department,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubA,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubB,'')+' '+ISNULL(TeamTaskGroupCust.DepartSubC,'')),' ','-')Department FROM(SELECT ID_Customer,CustomerName,MarriageName,GenderName,CONVERT(varchar(10),BirthDay,120) date,IDCard,MobileNo,Photo FROM OnArcCust INNER JOIN OnCustRelationCustPEInfo ON OnArcCust.ID_ArcCustomer=OnCustRelationCustPEInfo.ID_ArcCustomer) OnArcCust INNER JOIN OnCustPhysicalExamInfo ON OnArcCust.ID_Customer=OnCustPhysicalExamInfo.ID_Customer AND OnCustPhysicalExamInfo.Is_Checked=1 LEFT JOIN Team ON OnCustPhysicalExamInfo.ID_Team=Team.ID_Team LEFT JOIN TeamTaskGroupCust ON OnCustPhysicalExamInfo.ID_Customer=TeamTaskGroupCust.ID_Customer LEFT JOIN OnCustReportManage ON OnArcCust.ID_Customer=OnCustReportManage.ID_Customer)",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] CustomerReportPrintCoverInfoPagesParam = new string[]
		{
			"(SELECT * FROM(SELECT OnCustPhysicalExamInfo.ID_ExamType,OnCustPhysicalExamInfo.ExamTypeName,OnCustPhysicalExamInfo.ID_Team,IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,TeamName,Checker,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,ReportPrinter,ISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,Informer,ISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,ReportReceiptor,ISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,ISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,ISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,FinalDoctor,ISNULL(CONVERT(varchar(10),FinalDate,120),'')FinalDate,REPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM (SELECT * FROM OnCustPhysicalExamInfo WHERE Is_Checked=1 AND ID_Customer=@ID_Customer)OnCustPhysicalExamInfo LEFT JOIN OnCustReportManage ON OnCustPhysicalExamInfo.ID_Customer=OnCustReportManage.ID_Customer)A WHERE A.Is_ReportPrinted=1)"
		};

		protected string[] CustomerInformerPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			" (SELECT IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Team,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,Is_Team,TeamName,Checker\r\n,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,OnCustReportManage.ReportPrinter,\r\nISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,OnCustReportManage.Informer,\r\nISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,OnCustReportManage.ReportReceiptor,\r\nISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,\r\nISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,\r\nISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,OnCustPhysicalExamInfo.FinalDoctor,\r\nISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.FinalDate,120),'')FinalDate,\r\nREPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM \r\n(SELECT * FROM OnCustReportManage WHERE Is_ReportPrinted=1 AND ISNULL(Is_Informed,0)=0)OnCustReportManage \r\nINNER JOIN (SELECT * FROM OnCustPhysicalExamInfo WHERE ISNULL(OnCustPhysicalExamInfo.SecurityLevel,0)<@SecurityLevel)OnCustPhysicalExamInfo ON \r\nOnCustReportManage.ID_Customer=OnCustPhysicalExamInfo.ID_Customer AND OnCustPhysicalExamInfo.Is_Checked=1) ",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] CustomerReceiptPagesParam = new string[]
		{
			"ID_Customer",
			"*",
			" (SELECT IDCard,MobileNo,OnCustPhysicalExamInfo.ID_Team,OnCustPhysicalExamInfo.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) date,Is_Team,TeamName,Checker,ISNULL(CONVERT(varchar(10),CheckedDate,120),'') CheckedDate,OnCustReportManage.ReportPrinter,\r\nISNULL(CONVERT(varchar(10),ReportPrintedDate,120),'') ReportPrintedDate,OnCustReportManage.Informer,\r\nISNULL(CONVERT(varchar(10),InformedDate,120),'') InformedDate,OnCustReportManage.ReportReceiptor,\r\nISNULL(CONVERT(varchar(10),ReportReceiptedDate,120),'') ReportReceiptedDate,\r\nISNULL(Is_Checked,0) Is_Checked,ISNULL(Is_ReportPrinted,0) Is_ReportPrinted,\r\nISNULL(Is_Informed,0) Is_Informed,ISNULL(OnCustReportManage.Is_ReportReceipted,0) Is_ReportReceipted,OnCustPhysicalExamInfo.FinalDoctor,\r\nISNULL(CONVERT(varchar(10),OnCustPhysicalExamInfo.FinalDate,120),'')FinalDate,\r\nREPLACE(RTRIM(ISNULL(Department,'')+' '+ISNULL(DepartSubA,'')+' '+ISNULL(DepartSubB,'')+' '+ISNULL(DepartSubC,'')),' ','-')Department FROM \r\n(SELECT * FROM OnCustReportManage WHERE Is_ReportPrinted=1 AND ISNULL(Is_ReportReceipted,0)=0 AND Is_Informed=1) OnCustReportManage\r\nINNER JOIN(SELECT * FROM OnCustPhysicalExamInfo WHERE ISNULL(OnCustPhysicalExamInfo.SecurityLevel,0)<@SecurityLevel)OnCustPhysicalExamInfo ON OnCustReportManage.ID_Customer=OnCustPhysicalExamInfo.ID_Customer) ",
			"ORDER BY ID_Customer DESC"
		};

		protected string[] QueryAllLisCustFee_Param = new string[]
		{
			"\r\n--查询基本信息\r\nSELECT TOAC.ID_ArcCustomer,CustomerName,Photo,TOAC.SubScribDate,Is_GuideSheetPrinted,OperateDate,CASE WHEN Is_GuideSheetPrinted=1 THEN FLOOR(DATEDIFF(DY,BirthDay,OperateDate)/365.25) ELSE FLOOR(DATEDIFF(DY,BirthDay,GETDATE())/365.25) END Age,TOAC.ID_Customer,NationName,GenderName,MarriageName,IDCard,MobileNo,TOAC.TeamName,TOAC.Department DepartmentX,TOAC.DepartSubA,TOAC.DepartSubB,TOAC.DepartSubC\r\n,TOAC.ExamTypeName,ExamPlaceName,Is_FinalFinished,FinalDoctor,FinalDate,TOAC.Is_Checked,TOAC.Checker,TOAC.CheckedDate\r\n,Is_ReportPrinted,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,Is_Informed,Informer,InformedDate,\r\nIs_InformReturned,Is_ReportReceipted,Is_SelfReceipted,ReportReceiptor,ReportReceiptedDate\r\nFROM(SELECT ORPE.ID_ArcCustomer,OPE.* FROM (SELECT Photo,Is_GuideSheetPrinted,BirthDay,OperateDate,CustomerName,NationName,GenderName,MarriageName,IDCard,MobileNo,ID_Customer,SubScribDate,TeamName,Department,DepartSubA,DepartSubB,DepartSubC,ExamTypeName,ExamPlaceName,Is_FinalFinished,FinalDoctor,FinalDate,Is_Checked,Checker,CheckedDate FROM OnCustPhysicalExamInfo WHERE 1=1 \r\nAND ID_Customer=@ID_Customer)OPE\r\nINNER JOIN OnCustRelationCustPEInfo ORPE ON OPE.ID_Customer=ORPE.ID_Customer) TOAC\r\nLEFT JOIN OnCustReportManage OCRM ON TOAC.ID_Customer=OCRM.ID_Customer;\r\n \r\n--  读取作为父级的收费项目 (分单使用)\r\nSELECT A.ID_Customer,A.ID_Section,A.SectionName,A.ID_Fee,A.FeeName,A.FeeCode,ReportFeeName,A.LisSpecimenName,A.SendGroupParams,B.ApplyID \r\nFROM(SELECT  BusFee.*, OnCustFee.ID_Customer, OnCustFee.ID_CustFee,OnCustFee.ID_ExamDoctor,OnCustFee.ExamDoctorName,BusSpecimen.LisSpecimenName,BusFee.WorkGroupCode +'|'+BusFee.WorkStationCode+'|'+ BusSpecimen.LisSpecimenName SendGroupParams,\r\nISNULL(BusFee.DispOrder,500) as BusFeeDispOrder \r\nFROM \r\n(SELECT  * FROM OnCustFee WHERE OnCustFee.ID_Customer = @ID_Customer ) \r\nAS OnCustFee,(SELECT BusFee.* FROM BusFee INNER JOIN SYSSection ON BusFee.ID_Section=SYSSection.ID_Section WHERE SYSSection.InterfaceType='LAB')BusFee,BusSpecimen\r\nWHERE [OnCustFee].ID_Fee = BusFee.ID_Fee \r\nAND BusSpecimen.ID_Specimen = BusFee.ID_Specimen \r\n--AND BusFee.ID_FeeReportMerger = BusFee.ID_Fee\r\nAND ISNULL(OnCustFee.CustFeeChargeState,0) <> 2\r\nAND ID_Customer = @ID_Customer)A\r\nLEFT JOIN(SELECT ID_Customer,ApplyID,ID_ApplyList,SendGroupParams FROM FYH_Interface.DBO.OnCustSendToInterfaceApplyList WHERE ID_Customer=@ID_Customer AND InterfaceType='LAB')B\r\nON A.SendGroupParams=B.SendGroupParams\r\norder by ID_Section,BusFeeDispOrder ASC, A.ID_Fee ASC;"
		};

		protected string[] QueryAllSectionCustFee_Param = new string[]
		{
			"\r\n--查询基本信息\r\nSELECT TOAC.ID_ArcCustomer,CustomerName,TOAC.SubScribDate,Is_GuideSheetPrinted,OperateDate,CASE WHEN Is_GuideSheetPrinted=1 THEN FLOOR(DATEDIFF(DY,BirthDay,OperateDate)/365.25) ELSE FLOOR(DATEDIFF(DY,BirthDay,GETDATE())/365.25) END Age,TOAC.ID_Customer,NationName,GenderName,MarriageName,IDCard,MobileNo,TOAC.TeamName,TOAC.Department DepartmentX,TOAC.DepartSubA,TOAC.DepartSubB,TOAC.DepartSubC\r\n,TOAC.ExamTypeName,ExamPlaceName,Is_FinalFinished,FinalDoctor,FinalDate,TOAC.Is_Checked,TOAC.Checker,TOAC.CheckedDate\r\n,Is_ReportPrinted,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,Is_Informed,Informer,InformedDate,\r\nIs_InformReturned,Is_ReportReceipted,Is_SelfReceipted,ReportReceiptor,ReportReceiptedDate\r\nFROM(SELECT ORPE.ID_ArcCustomer,OPE.* FROM (SELECT Is_GuideSheetPrinted,BirthDay,OperateDate,CustomerName,NationName,GenderName,MarriageName,IDCard,MobileNo,ID_Customer,SubScribDate,TeamName,Department,DepartSubA,DepartSubB,DepartSubC,ExamTypeName,ExamPlaceName,Is_FinalFinished,FinalDoctor,FinalDate,Is_Checked,Checker,CheckedDate FROM OnCustPhysicalExamInfo WHERE 1=1 \r\nAND ID_Customer=@ID_Customer)OPE\r\nINNER JOIN OnCustRelationCustPEInfo ORPE ON OPE.ID_Customer=ORPE.ID_Customer) TOAC\r\nLEFT JOIN OnCustReportManage OCRM ON TOAC.ID_Customer=OCRM.ID_Customer;\r\n\r\n --  读取作为父级的收费项目 (分单使用)\r\nSELECT A.ID_Customer,A.ID_Fee,A.FeeName,A.FeeCode,ReportFeeName,A.ID_Section,A.SectionName,A.LisSpecimenName,A.SendGroupParams,B.ApplyID \r\nFROM(SELECT  BusFee.*, OnCustFee.ID_Customer, OnCustFee.ID_CustFee,OnCustFee.ID_ExamDoctor,OnCustFee.ExamDoctorName,BusSpecimen.LisSpecimenName,BusFee.WorkGroupCode +'|'+BusFee.WorkStationCode+'|'+ BusSpecimen.LisSpecimenName SendGroupParams,\r\nISNULL(BusFee.DispOrder,500) as BusFeeDispOrder \r\nFROM \r\n(SELECT  * FROM OnCustFee WHERE OnCustFee.ID_Customer = @ID_Customer ) \r\nAS OnCustFee,\r\n(SELECT  BusFee.* FROM BusFee WHERE  CONVERT(VARCHAR,BusFee.ID_Section) IN(@SectonStr) ) \r\nAS BusFee ,BusSpecimen\r\nWHERE [OnCustFee].ID_Fee = BusFee.ID_Fee \r\nAND BusSpecimen.ID_Specimen = BusFee.ID_Specimen \r\n--AND BusFee.ID_FeeReportMerger = BusFee.ID_Fee\r\nAND ISNULL(OnCustFee.CustFeeChargeState,0) <> 2\r\nAND CONVERT(VARCHAR,BusFee.ID_Section) IN(@SectonStr) AND ID_Customer = @ID_Customer)A\r\nLEFT JOIN(SELECT ID_Customer,ApplyID,ID_ApplyList,SendGroupParams FROM FYH_Interface.DBO.OnCustSendToInterfaceApplyList WHERE ID_Customer=@ID_Customer AND InterfaceType='LAB')B\r\nON A.SendGroupParams=B.SendGroupParams\r\norder by ID_Section,BusFeeDispOrder ASC, A.ID_Fee ASC;"
		};

		DataTable ICommonReport.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return base.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public int UpdateCustomerPrintFlag(string ID_Customer, string ID_Operator, string Operator, string UpdateTime)
		{
			string item = string.Format("DECLARE @ID_Customer BIGINT\r\nDECLARE @ID_Operator INT\r\nDECLARE @Is_GuideSheetPrinted INT\r\nSELECT @ID_Customer=ISNULL(ID_Customer,0),@ID_Operator=ISNULL(ID_Operator,0),@Is_GuideSheetPrinted=ISNULL(Is_GuideSheetPrinted,0) FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}'\r\nIF(@ID_Customer!=0)\r\nBEGIN\r\n\tUPDATE OnCustFee SET Is_Printed=1 WHERE ID_Customer='{0}';\r\n\tIF(@Is_GuideSheetPrinted=0)\r\n\t    BEGIN\r\n\t\t    --SELECT 1;\r\n\t\t    --如果没有完成登记，则修改已打印和登记三字段\r\n            IF (@ID_Operator=0)\r\n                BEGIN\r\n\t\t            UPDATE OnCustPhysicalExamInfo SET Is_ExamStarted=1,Is_GuideSheetPrinted=1,ID_Operator='{1}',Operator='{2}',OperateDate='{3}' WHERE ID_Customer='{0}';\r\n                END\r\n            ELSE\r\n                BEGIN\r\n\t\t            UPDATE OnCustPhysicalExamInfo SET Is_ExamStarted=1,Is_GuideSheetPrinted=1,OperateDate='{3}' WHERE ID_Customer='{0}';\r\n                END\r\n\t    END\r\n\t ELSE\r\n\t    BEGIN\r\n\t\t    --SELECT 2;\r\n\t\t    UPDATE OnCustPhysicalExamInfo SET Is_GuideSheetPrinted=1 WHERE ID_Customer='{0}';\r\n\t    END\r\nEND", new object[]
			{
				ID_Customer,
				ID_Operator,
				Operator,
				UpdateTime
			});
			return base.ExecuteSqlTran(new List<string>(1)
			{
				item
			});
		}

		public int UpdateCustomerReportReceiptFlag(List<PEIS.Model.OnCustReportManage> listCustReportManage)
		{
			string text = "";
			int num = 0;
			string text2 = string.Empty;
			foreach (PEIS.Model.OnCustReportManage current in listCustReportManage)
			{
				text2 += string.Format("UPDATE OnCustReportManage SET Is_ReportReceipted=1,ID_ReportOffer='{1}',ReportOffer='{2}',ReportReceiptedDate='{3}',Is_SelfReceipted='{4}',ReportReceiptor='{5}',ReportOffDate='{3}' WHERE ID_Customer='{0}';", new object[]
				{
					current.ID_Customer,
					current.ID_ReportOffer,
					current.ReportOffer,
					current.ReportReceiptedDate,
					current.Is_SelfReceipted,
					current.ReportReceiptor
				});
				if (num == 0)
				{
					text = current.ID_Customer.ToString();
				}
				else
				{
					text = text + "," + current.ID_Customer.ToString();
				}
				num++;
			}
			if (!string.IsNullOrEmpty(text))
			{
				text2 += string.Format("UPDATE OnCustRelationCustPEInfo SET Is_CompletePhysical=1 WHERE ID_Customer in ({0}); UPDATE OnCustPhysicalExamInfo SET Is_ReportReceipted = 1 WHERE ID_Customer in ({0});\r\nUPDATE OnArcCust set FinishedNum=ISNULL(FinishedNum,0)+1,UnfinishedNum=case when ISNULL(UnfinishedNum,0)-1<1 then 0 else ISNULL(UnfinishedNum,0)-1 end WHERE ID_ArcCustomer IN(SELECT ID_ArcCustomer FROM OnCustRelationCustPEInfo WHERE ID_Customer IN({0}));", text);
			}
			return base.ExecuteSqlTran(new List<string>(1)
			{
				text2
			});
		}

		DataSet ICommonReport.ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return base.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public int UpdateCustomerReportFlag(string ID_User, string UserName, string ID_Customer, int Is_Self, string OperType)
		{
			string item = string.Empty;
			string text = DateTime.Now.ToString();
			if (OperType.ToLower() == "printreport")
			{
				item = string.Format("IF NOT EXISTS(select ID_Customer from OnCustReportManage WITH(NOLOCK) where ID_Customer='{0}')\r\n\tBEGIN\r\n\t\tINSERT INTO OnCustReportManage(ID_Customer,ID_ReprotWay,ReportWay,ID_ReportPrinter,ReportPrinter,ReportPrintedDate,Is_ReportPrinted)\r\nSELECT '{0}',ID_ReportWay,ReportWayName,'{1}','{2}','{3}',1  FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';\r\n\tEND;", new object[]
				{
					ID_Customer,
					ID_User,
					UserName,
					text
				});
			}
			else if (OperType.ToLower() == "informer")
			{
				item = string.Format("IF NOT EXISTS(select ID_Customer from OnCustReportManage WITH(NOLOCK) where ID_Customer='{0}')\r\n\tBEGIN\r\n\t\tINSERT INTO OnCustReportManage(ID_Customer,ID_Informer,Informer,InformedDate,Is_Informed)\r\n\t\tvalues('{0}','{1}','{2}','{3}',1);\r\n\tEND;", new object[]
				{
					ID_Customer,
					ID_User,
					UserName,
					text
				});
			}
			else if (OperType.ToLower() == "receipt")
			{
				item = string.Format("IF NOT EXISTS(select ID_Customer from OnCustReportManage WITH(NOLOCK) where ID_Customer='{0}')\r\n\tBEGIN\r\n\t\tINSERT INTO OnCustReportManage(ID_Customer,ID_ReprotWay,ReportWay,ID_ReportReceiptor,ReportReceiptor,ReportReceiptedDate,Is_ReportReceipted,Is_SelfReceipted)\r\nSELECT '{0}',ID_ReportWay,ReportWayName,'{1}','{2}','{3}',1,'{4}' FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';\r\n\tEND;", new object[]
				{
					ID_Customer,
					ID_User,
					UserName,
					text,
					Is_Self
				});
			}
			return base.ExecuteSqlTran(new List<string>(1)
			{
				item
			});
		}

		public int UpdateCustomerReportInformFlag(string ID_User, string UserName, string ID_Customer, int Is_Self, string OperType)
		{
			ID_Customer = ID_Customer.TrimEnd(new char[]
			{
				','
			});
			string item = string.Empty;
			string text = DateTime.Now.ToString();
			if (OperType.ToLower() == "informer")
			{
				item = string.Format("UPDATE OnCustReportManage SET Is_Informed=1,ID_Informer='{1}',Informer='{2}',InformedDate='{3}' WHERE ID_Customer IN({0});", new object[]
				{
					ID_Customer,
					ID_User,
					UserName,
					text
				});
			}
			else if (OperType.ToLower() == "receipt")
			{
				item = string.Format("IF NOT EXISTS(select ID_Customer from OnCustReportManage WITH(NOLOCK) where ID_Customer='{0}')\r\n\tBEGIN\r\n\t\tINSERT INTO OnCustReportManage(ID_Customer,ID_ReprotWay,ReportWay,ID_ReportReceiptor,ReportReceiptor,ReportReceiptedDate,Is_ReportReceipted,Is_SelfReceipted)\r\nSELECT '{0}',ID_ReportWay,ReportWayName,'{1}','{2}','{3}',1,'{4}' FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';\r\n\tEND;", new object[]
				{
					ID_Customer,
					ID_User,
					UserName,
					text,
					Is_Self
				});
			}
			return base.ExecuteSqlTran(new List<string>(1)
			{
				item
			});
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
