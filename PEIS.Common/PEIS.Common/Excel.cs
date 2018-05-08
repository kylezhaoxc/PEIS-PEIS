using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace PEIS.Common
{
    public class Excel
    {
        private static Dictionary<string, ExcelModel> _ColumnsDic;

        public static Dictionary<string, ExcelModel> ColumnsDic
        {
            get
            {
                if (Excel._ColumnsDic == null)
                {
                    Excel._ColumnsDic = new Dictionary<string, ExcelModel>();
                    ExcelModel excelModel = new ExcelModel()
                    {
                        Key = "Discount",
                        Value = "折扣率"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "OriginalPrice",
                        Value = "原价"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "DiscounterName",
                        Value = "折扣人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "UserName",
                        Value = "医生姓名"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SectionName",
                        Value = "科室名称"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ID_Checker",
                        Value = "检查人ID",
                        Is_visible = 0
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumNotTeamNum",
                        Value = "个人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumTeamNum",
                        Value = "团体"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Num",
                        Value = "检查总量"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "TeamWorkLoadType",
                        Value = "统计类型"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "TeamWorkLoadNum",
                        Value = "完成量(人)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeItemName",
                        Value = "收费项目名称"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ID_Customer",
                        Value = "体检号"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CustomerName",
                        Value = "客户姓名"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GenderName",
                        Value = "客户性别"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Age",
                        Value = "年龄"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MarriageName",
                        Value = "婚姻状态"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "TeamName",
                        Value = "单位名称"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Department",
                        Value = "部门"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "DepartSubA",
                        Value = "二级部门"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "DepartSubB",
                        Value = "三级部门"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "DepartSubC",
                        Value = "四级部门"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SubScribDate",
                        Value = "体检时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "PositiveSummary",
                        Value = "阳性结果"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MainDiagnose",
                        Value = "主要诊断"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SecondaryDiagnose",
                        Value = "次要诊断"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "IndicatorDiagnose",
                        Value = "指标异常"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "NormalDiagnose",
                        Value = "正常诊断"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "OtherDiagnose",
                        Value = "其它异常"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ResultCompare",
                        Value = "结果对比"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "DiagnoseType",
                        Value = "结论类型"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "NotCompeleteFeeItemName",
                        Value = "未检项目"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamStateName",
                        Value = "体检状态"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Note",
                        Value = "备注"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Male",
                        Value = "男性"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeMale",
                        Value = "女性"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumGender",
                        Value = "合计"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "AgeName",
                        Value = "年龄组"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MalePer",
                        Value = "男性(百分比)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeMalePer",
                        Value = "女性(百分比)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumPer",
                        Value = "合计(百分比)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeName",
                        Value = "收费项目"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MaleCustFee",
                        Value = "男性(参检人数)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeMaleCustFee",
                        Value = "女性(参检人数)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumGenderCustFee",
                        Value = "合计(参检人数)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MalePerCustFee",
                        Value = "男性(参检比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeMalePerCustFee",
                        Value = "女性(参检比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumPerCustFee",
                        Value = "合计(参检比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamType",
                        Value = "类型"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FMale",
                        Value = "女性"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MealePer",
                        Value = "男性(构成比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FMalePer",
                        Value = "女性(构成比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "TeamConclusionName",
                        Value = "团体结论词"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CheckOutMale",
                        Value = "男性(检出数)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CheckOutFMale",
                        Value = "女性(检出数)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumCheckOutCount",
                        Value = "合计(检出数)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CheckOutMalePer",
                        Value = "男性(检出比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CheckOutFMalePer",
                        Value = "女性(检出比例)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ConclusionName",
                        Value = "结论词"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ID_ArcCustomer",
                        Value = "存档号码"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "IDCard",
                        Value = "证件号"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "MobileNo",
                        Value = "联系电话"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamTypeName",
                        Value = "体检类型"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Is_Checked",
                        Value = "是否总审"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Checker",
                        Value = "审核人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CheckedDate",
                        Value = "审核时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Is_ReportPrinted",
                        Value = "是否打印"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ReportPrinter",
                        Value = "打印人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ReportPrintedDate",
                        Value = "打印时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Is_Informed",
                        Value = "是否通知 "
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Informer",
                        Value = "通知人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "InformedDate",
                        Value = "通知时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Is_ReportReceipted",
                        Value = "是否领取"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ReportReceiptor",
                        Value = "领取人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ReportReceiptedDate",
                        Value = "领取时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamPlaceName",
                        Value = "体检地点"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Is_FinalFinished",
                        Value = "是否总检"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FinalDoctor",
                        Value = "总检医生"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FinalDate",
                        Value = "总检时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeItemNum",
                        Value = "序号"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeChargeStaute",
                        Value = "收费状态"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FactPrice",
                        Value = "价格(元)"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamStated",
                        Value = "检查状态"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "RegisterName",
                        Value = "登记人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeWayName",
                        Value = "付费方式"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "RegistCount",
                        Value = "总数"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SumFactPrice",
                        Value = "总金额"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeCharger",
                        Value = "收费人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeChargeDate",
                        Value = "收费时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "FeeChargeState",
                        Value = "收费状态"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamDoctorName",
                        Value = "检查医生"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "PEPackageName",
                        Value = "套餐名称"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SetCount",
                        Value = "使用人数"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamDate",
                        Value = "检查时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GuideNurse",
                        Value = "导检护士"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "Invoice",
                        Value = "发票号"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GuideNum",
                        Value = "导检量"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GuidePrice",
                        Value = "导检金额"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GuideSheetReturnedby",
                        Value = "回收人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GuideSheetReturnedNum",
                        Value = "回收量"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "GuideSheetReturnedDate",
                        Value = "回收时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SectionSummaryDate",
                        Value = "检查时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SummaryDoctorName",
                        Value = "检查人员"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "TypistName",
                        Value = "录入人员"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "TypistDate",
                        Value = "录入时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "CheckDate",
                        Value = "提交时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "SectionSummary",
                        Value = "小结内容"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamItemName",
                        Value = "检查项目"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ResultSummary",
                        Value = "检查结果"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "ExamItemSummaryDate",
                        Value = "检查时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "DepartmentX",
                        Value = "部门"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "预约人",
                        Value = "预约人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "预约时间",
                        Value = "预约时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "登记人",
                        Value = "登记人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "登记时间",
                        Value = "登记时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "收费人",
                        Value = "收费人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "收费时间",
                        Value = "收费时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "回收人",
                        Value = "回收时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "指引单打印人",
                        Value = "指引单打印人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "指引单打印时间",
                        Value = "指引单打印时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "分检医生",
                        Value = "分检医生"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "分检时间",
                        Value = "分检时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "总检医生",
                        Value = "总检医生"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "总检时间",
                        Value = "总检时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "总审医生",
                        Value = "总审医生"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "总审时间",
                        Value = "总审时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "回收人员",
                        Value = "回收时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "报告打印人",
                        Value = "报告打印人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "报告打印时间",
                        Value = "报告打印时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "通知人",
                        Value = "通知人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "通知时间",
                        Value = "通知时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "领取人",
                        Value = "领取人"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "领取时间",
                        Value = "领取时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                    excelModel = new ExcelModel()
                    {
                        Key = "指引单扫描人",
                        Value = "指引单扫描时间"
                    };
                    Excel._ColumnsDic.Add(excelModel.Key, excelModel);
                }
                return Excel._ColumnsDic;
            }
        }

        static Excel()
        {
            Excel._ColumnsDic = null;
        }

        public Excel()
        {
        }

        public static void DataSetToExcel(DataSet ds, string filePath)
        {
            if ((ds == null ? false : ds.Tables.Count > 0))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("<?xml version='1.0'?><?mso-application progid='Excel.Sheet'?>");
                stringBuilder.AppendLine("<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet'\r\n          xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'\r\n          xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>");
                stringBuilder.AppendLine("<DocumentProperties xmlns='urn:schemas-microsoft-com:office:office'>");
                stringBuilder.AppendLine("</DocumentProperties>");
                stringBuilder.AppendLine("<Styles><Style ss:ID='Default' ss:Name='Normal'><Alignment ss:Vertical='Center'/>\r\n          <Borders/><Font ss:FontName='宋体' x:CharSet='134' ss:Size='11'/><Interior/><NumberFormat/><Protection/></Style>");
                stringBuilder.AppendLine("<Style ss:ID='Header'><Borders><Border ss:Position='Bottom' ss:Weight='1'/>\r\n           <Border ss:Position='Left' ss:Weight='1'/>\r\n           <Border ss:Position='Right' ss:Weight='1'/>\r\n           <Border ss:Position='Top'  ss:Weight='1'/></Borders>\r\n           </Style>");
                stringBuilder.AppendLine("<Style ss:ID='border'><NumberFormat ss:Format='@'/><Borders>\r\n          <Border ss:Position='Bottom'/>\r\n          <Border ss:Position='Left' />\r\n          <Border ss:Position='Right'/>\r\n          <Border ss:Position='Top'/></Borders></Style>");
                stringBuilder.AppendLine("</Styles>");
                try
                {
                    ExcelModel item = null;
                    int count = ds.Tables.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataTable dataTable = ds.Tables[i];
                        int num = 0;
                        int count1 = dataTable.Rows.Count;
                        if (count1 > dataTable.Rows.Count)
                        {
                            count1 = dataTable.Rows.Count;
                        }
                        stringBuilder.AppendLine(string.Concat("<Worksheet ss:Name='", dataTable.TableName, "'>"));
                        stringBuilder.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>");
                        stringBuilder.AppendLine("\r\n<Row ss:AutoFitHeight='1'>");
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            if (Excel.ColumnsDic.ContainsKey(column.ColumnName))
                            {
                                item = Excel.ColumnsDic[column.ColumnName];
                                if (item != null)
                                {
                                    if (item.Is_visible == 1)
                                    {
                                        stringBuilder.AppendLine(string.Concat("<Cell ss:StyleID='Header'><Data ss:Type='String'>", item.Value, "</Data></Cell>"));
                                    }
                                }
                            }
                        }
                        stringBuilder.AppendLine("\r\n</Row>");
                        for (int j = num; j < count1; j++)
                        {
                            stringBuilder.AppendLine("<Row>");
                            for (int k = 0; k < dataTable.Columns.Count; k++)
                            {
                                if (Excel.ColumnsDic.ContainsKey(dataTable.Columns[k].ColumnName))
                                {
                                    item = Excel.ColumnsDic[dataTable.Columns[k].ColumnName];
                                    if (item != null)
                                    {
                                        if (item.Is_visible == 1)
                                        {
                                            stringBuilder.AppendLine(string.Concat("<Cell ss:StyleID='border'><Data ss:Type='String'>", Input.HtmlEncode(dataTable.Rows[j][k].ToString()), "</Data></Cell>"));
                                        }
                                    }
                                }
                            }
                            stringBuilder.AppendLine("</Row>");
                        }
                        stringBuilder.AppendLine("</Table>");
                        stringBuilder.AppendLine("</Worksheet>");
                    }
                    stringBuilder.AppendLine("</Workbook>");
                    StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.GetEncoding("UTF-8"));
                    streamWriter.Write(stringBuilder);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                catch (Exception exception)
                {
                }
            }
        }

        public static void DataTableToExcel(DataTable dt, string filePath)
        {
            if (dt != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("<?xml version='1.0'?><?mso-application progid='Excel.Sheet'?>");
                stringBuilder.AppendLine("<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet'\r\n          xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'\r\n          xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>");
                stringBuilder.AppendLine("<DocumentProperties xmlns='urn:schemas-microsoft-com:office:office'>");
                stringBuilder.AppendLine("</DocumentProperties>");
                stringBuilder.AppendLine("<Styles><Style ss:ID='Default' ss:Name='Normal'><Alignment ss:Vertical='Center'/>\r\n          <Borders/><Font ss:FontName='宋体' x:CharSet='134' ss:Size='11'/><Interior/><NumberFormat/><Protection/></Style>");
                stringBuilder.AppendLine("<Style ss:ID='Header'><Borders><Border ss:Position='Bottom' ss:Weight='1'/>\r\n           <Border ss:Position='Left' ss:Weight='1'/>\r\n           <Border ss:Position='Right' ss:Weight='1'/>\r\n           <Border ss:Position='Top'  ss:Weight='1'/></Borders>\r\n           </Style>");
                stringBuilder.AppendLine("<Style ss:ID='border'><NumberFormat ss:Format='@'/><Borders>\r\n          <Border ss:Position='Bottom'/>\r\n          <Border ss:Position='Left' />\r\n          <Border ss:Position='Right'/>\r\n          <Border ss:Position='Top'/></Borders></Style>");
                stringBuilder.AppendLine("</Styles>");
                try
                {
                    ExcelModel item = null;
                    for (int i = 0; i < 1; i++)
                    {
                        int num = 0;
                        int count = dt.Rows.Count;
                        if (count > dt.Rows.Count)
                        {
                            count = dt.Rows.Count;
                        }
                        stringBuilder.AppendLine(string.Concat("<Worksheet ss:Name='", dt.TableName, "'>"));
                        stringBuilder.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>");
                        stringBuilder.AppendLine("\r\n<Row ss:AutoFitHeight='1'>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (Excel.ColumnsDic.ContainsKey(column.ColumnName))
                            {
                                item = Excel.ColumnsDic[column.ColumnName];
                                if (item != null)
                                {
                                    if (item.Is_visible == 1)
                                    {
                                        stringBuilder.AppendLine(string.Concat("<Cell ss:StyleID='Header'><Data ss:Type='String'>", item.Value, "</Data></Cell>"));
                                    }
                                }
                            }
                        }
                        stringBuilder.AppendLine("\r\n</Row>");
                        for (int j = num; j < count; j++)
                        {
                            stringBuilder.AppendLine("<Row>");
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                if (Excel.ColumnsDic.ContainsKey(dt.Columns[k].ColumnName))
                                {
                                    item = Excel.ColumnsDic[dt.Columns[k].ColumnName];
                                    if (item != null)
                                    {
                                        if (item.Is_visible == 1)
                                        {
                                            stringBuilder.AppendLine(string.Concat("<Cell ss:StyleID='border'><Data ss:Type='String'>", dt.Rows[j][k].ToString(), "</Data></Cell>"));
                                        }
                                    }
                                }
                            }
                            stringBuilder.AppendLine("</Row>");
                        }
                        stringBuilder.AppendLine("</Table>");
                        stringBuilder.AppendLine("</Worksheet>");
                    }
                    stringBuilder.AppendLine("</Workbook>");
                    StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.GetEncoding("UTF-8"));
                    streamWriter.Write(stringBuilder);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                catch (Exception exception)
                {
                }
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        public static void ImportToExcel(DataSet ds, string excelname, string path, string type)
        {
            ExcelModel item;
            try
            {
                Application value = (Application)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
                _Workbook variable = value.Workbooks.Add(true);
                object obj = Missing.Value;
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    ((_Worksheet)((dynamic)variable.ActiveSheet)).Name = excelname;
                    for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                    {
                        if (Excel.ColumnsDic.ContainsKey(ds.Tables[i].Columns[j].ColumnName))
                        {
                            item = Excel.ColumnsDic[ds.Tables[i].Columns[j].ColumnName];
                            if (item != null)
                            {
                                if (item.Is_visible == 1)
                                {
                                    value.Cells[1, j + 1] = item.Value;
                                }
                            }
                        }
                    }
                    for (int k = 0; k < ds.Tables[i].Rows.Count; k++)
                    {
                        for (int l = 0; l < ds.Tables[i].Columns.Count; l++)
                        {
                            if (Excel.ColumnsDic.ContainsKey(ds.Tables[i].Columns[l].ColumnName))
                            {
                                item = Excel.ColumnsDic[ds.Tables[i].Columns[l].ColumnName];
                                if (item != null)
                                {
                                    if (item.Is_visible == 1)
                                    {
                                        value.Cells[k + 2, l + 1] = ds.Tables[i].Rows[k][l].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                variable.SaveAs(string.Concat(path, "\\", type), obj, obj, obj, obj, obj, XlSaveAsAccessMode.xlShared, obj, obj, obj, obj, obj);
                variable.Close(obj, obj, obj);
                Excel.killExcel(value);
                value.Quit();
                GC.Collect();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public static void ImportToExcel(DataTable dt, string excelname, string path, string type)
        {
            try
            {
                Application variable = (Application)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
                _Workbook variable1 = variable.Workbooks.Add(true);
                object value = Missing.Value;
                _Worksheet item = (_Worksheet)((dynamic)variable1.Worksheets[1]);
                item.Name = excelname;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    item.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        item.Cells[j + 2, k + 1] = dt.Rows[j][k].ToString();
                    }
                }
                variable1.Saved = true;
                variable1.SaveAs(path, value, value, value, value, value, XlSaveAsAccessMode.xlShared, value, value, value, value, value);
                variable1.Close(value, value, value);
                Excel.killExcel(variable);
                variable.Quit();
                GC.Collect();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public static bool ImportToExcel_New(DataTable dt, string excelname, string path, string type)
        {
            int i;
            Exception exception;
            bool flag;
            bool flag1 = false;
            Application variable = null;
            Workbooks workbooks = null;
            Workbook variable1 = null;
            Worksheet worksheets = null;
            try
            {
                try
                {
                    variable = (Application)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
                    if (variable != null)
                    {
                        workbooks = variable.Workbooks;
                        variable1 = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        worksheets = (Worksheet)((dynamic)variable1.Worksheets[1]);
                        for (i = 0; i < dt.Columns.Count; i++)
                        {
                            worksheets.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                        }
                        try
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                for (i = 0; i < dt.Columns.Count; i++)
                                {
                                    worksheets.Cells[j + 2, i + 1] = dt.Rows[j][i];
                                }
                            }
                            worksheets.Columns.EntireColumn.AutoFit();
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            flag = flag1;
                            return flag;
                        }
                        variable1.Saved = true;
                        object value = Missing.Value;
                        variable1.SaveCopyAs(path);
                        variable1.SaveAs(string.Concat(path, "\\", type), value, value, value, value, value, XlSaveAsAccessMode.xlShared, value, value, value, value, value);
                        flag1 = true;
                        variable1.Close(false, null, null);
                        variable.Quit();
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                }
            }
            finally
            {
                Marshal.ReleaseComObject(variable1);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(worksheets);
                Marshal.ReleaseComObject(variable);
                workbooks = null;
                worksheets = null;
                variable = null;
                GC.Collect();
            }
            flag = flag1;
            return flag;
        }

        private static void killExcel(_Application excel)
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                IntPtr intPtr = new IntPtr(excel.Hwnd);
                int num = 0;
                Excel.GetWindowThreadProcessId(intPtr, out num);
                Process[] processArray = processes;
                for (int i = 0; i < (int)processArray.Length; i++)
                {
                    Process process = processArray[i];
                    if (process.ProcessName.ToLower().Equals("excel"))
                    {
                        if (process.Id == num)
                        {
                            process.Kill();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
    }
}