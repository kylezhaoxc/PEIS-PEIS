/****************报告预览  Begin***********************/
var IsShowIndicatorDiagnose = 0; //是否显示指标异常及建议
var IsShowOtherDiagnose = 0; //是否显示其它异常及建议 
var FastReport = document.getElementById("FastReport"); //获取报表插件
var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改"; //编辑提示
var modelName = jQuery.trim(jQuery("#modelName").val());
var TemplateName = "TemplateReportView";
var regexEnum =
{
    intege1: "^[1-9]\\d*$"				//正整数
}

/// <summary>
/// 判断是否是体检号 （全数字 + 14位）
/// </summary>
function isCustomerExamNo(s) {
    var patrn = regexEnum.intege1;
    if (!s.match(patrn)) {
        return false;
    }
    if (s.length != 14) {
        return false;
    }
    return true;
}

function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13)//回车事件
    {
        //如果是在搜索中
        if (id == "txtCustomerID") {
            AutoGetCustomerSectionExamInfo();
        }
    }
}
/// <summary>
/// 读取指定模版
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function ReadTemplateTeamTaskGroup(TemplateTeamTaskGroupID) {
    //默认是读取tblTemplateTeamTaskList模版填充到tblTeamTask中显示
    if (TemplateTeamTaskGroupID == "" || TemplateTeamTaskGroupID == undefined) {
        TemplateTeamTaskGroupID = TemplateName;
    }
    var teamTaskGroupListContent = ""; //团体任务分组table内容
    var teamTaskGroupListTheadTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' thead').html(); //团体任务模版Thead部分
    var teamTaskListBodyTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' tbody').html(); //团体任务模版body部分
    this.teamTaskGroupListTheadTempleteContent = teamTaskGroupListTheadTempleteContent;
    this.teamTaskListBodyTempleteContent = teamTaskListBodyTempleteContent;
    return this;
}
/// <summary>
/// 获取科室检查信息
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function GetCustExamSectionItem() {

    var CustomerID = jQuery.trim(jQuery("#txtCustomerID").val());
    if (CustomerID != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Ajax/AjaxConclusion.aspx",
            data: {
                type: "printview",
                CustomerID: CustomerID,
                action: 'GetCustExamSectionItem',
                currenttime: encodeURIComponent(new Date())
            },
            cache: false,
            dataType: "json",
            success: function (jsonmsg) {

                if (jsonmsg != null && jsonmsg != "") {
                    // 显示查询到的分科检查信息
                    ShowCustExamSectionItem_Ajax(jsonmsg);
                }
            }
        });
    }
}
/// <summary>
/// 获取客户总检信息
///修改人：黄兴茂
function GetCustomerFinalCheckInfo() {
    var CustomerID = jQuery.trim(jQuery("#txtCustomerID").val());
    if (CustomerID != "") {
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxReportPreview.aspx",
            data: { CustomerID: CustomerID,
                action: 'GetCustomerFinalCheckInfo',
                currenttime: encodeURIComponent(new Date())
            },
            cache: false,
            dataType: "json",
            success: function (jsonmsg) {
                if (jsonmsg != null && jsonmsg != "") {
                    // 显示查询到的分科检查信息
                    ShowCustomerFinalCheckInfo_Ajax(jsonmsg);
                }
            }
        });
    }
}
/// <summary>
/// 绑定总检信息
///修改人：黄兴茂
function ShowCustomerFinalCheckInfo_Ajax(jsonmsg) {

    //    vltContext.Put("htmlFinalOverView", OCPEIModel.FinalOverView.Replace("\n", "<br/>"));              // 综述
    //    vltContext.Put("htmlFinalConclusion", OCPEIModel.FinalConclusion.Replace("\n", "<br/>"));          // 总检结论
    //    vltContext.Put("htmlResultCompare", OCPEIModel.ResultCompare.Replace("\n", "<br/>"));              // 结果对比
    //    vltContext.Put("htmlMainDiagnose", OCPEIModel.MainDiagnose.Replace("\n", "<br/>"));        // 总检建议
    //    vltContext.Put("htmlFinalDietGuide", OCPEIModel.FinalDietGuide.Replace("\n", "<br/>"));            // 饮食建议
    //    vltContext.Put("htmlFinalSportGuide", OCPEIModel.FinalSportGuide.Replace("\n", "<br/>"));          // 运动建议
    //    vltContext.Put("htmlFinalHealthKnowlage", OCPEIModel.FinalHealthKnowlage.Replace("\n", "<br/>"));  // 健康建议
}
/// <summary>
/// 绑定科室信息
///修改人：黄兴茂
function ShowCustExamSectionItem_Ajax(json) {

    var CustExamSectionListTempleteContent = jQuery("#CustExamSectionListTemplete").html();
    if (CustExamSectionListTempleteContent == undefined) { return; }
    var examSectionContent = "", Is_EvenLine = true;
    jQuery(json.dataList0).each(function (j, examsectionitem) {

        examSectionContent += CustExamSectionListTempleteContent
                            .replace(/@SectionName/gi, examsectionitem.SectionName)
                            .replace(/@SectionSummaryText/gi, examsectionitem.SectionSummary)
                            .replace(/@SummaryDoctorName/gi, examsectionitem.SummaryDoctorName)
                            .replace(/@SectionSummaryDate/gi, examsectionitem.SummaryDoctorName != '' ? examsectionitem.SectionSummaryDate : "")
                            .replace(/@Examed/gi, examsectionitem.IS_giveup == 'True' ? "<span title='已弃检'><strong>×</strong></span>" : examsectionitem.SummaryDoctorName == '' ? '--' : '√')
                            .replace(/@Checked/gi, examsectionitem.Is_Check == 'True' ? '√' : '--')
                            .replace(/@GiveUp/gi, examsectionitem.IS_giveup == 'True' ? '√' : '--')
                            .replace(/@tr_class/gi, Is_EvenLine == true ? "tr_even" : "tr_odd")
                            .replace(/@TypistName/gi, examsectionitem.TypistName)
                            ;
        if (Is_EvenLine == true) {
            Is_EvenLine = false;
        }
        else {
            Is_EvenLine = true;
        }
    });
    jQuery("#CustExamSectionList").html(examSectionContent);
}

/// <summary>
/// 根据输入情况，自动获取客户的体检报告预览
///修改人：黄兴茂
///修改日期：2013-07-26
///修改内容：功能要求：通过客户体检号首先检索客户基本信息和客户所有科室的检查信息
///          如果客户已经通过总审，则可用进行报表预览，否则报表预览不可见
/// </summary>
function AutoGetCustomerReport() {

    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
    if (isCustomerExamNo(customerid)) {

        //绑定左侧基本信息
        SearchCustomerBaseInfo();
        //绑定分科检查信息和总检信息
        GetCustExamSectionItem();
    }
}

function PreviewReport() {
    var ID_Customer = jQuery.trim(jQuery("#lblID_Customer").text());
    if (isCustomerExamNo(ID_Customer)) {
        var Is_ReportPrinted = jQuery.trim(jQuery("#lblIs_ReportPrinted").text()) == "√" ? 1 : 0;
        FastReport.ReportPreview(ID_Customer, "ExamReport_Common_Caption.frx", "1", Is_ReportPrinted);
    }
}
/****************报告预览  End***********************/
// ===============  文本域切换区域 Start   ========================================================

/// <summary>
/// 显示隐藏结果文本域
/// </summary>
function ShowHideFinalTextArea(index) {

    jQuery("#trConclusionFinalOverView").hide();        // 综述
    jQuery("#trConclusionFinalConclusion").hide();      // 总检结论
    jQuery("#trConclusionIndicatorDiagnose").hide();     //指标异常及建议 xmhuang 2014-01-21     
    jQuery("#trConclusionOtherDiagnose").hide();     //其它异常及建议 xmhuang 2014-01-21     
    jQuery("#trConclusionResultCompare").hide();        // 结果对比
    jQuery("#trConclusionMainDiagnose").hide();     // 总检建议
    jQuery("#trConclusionFinalDietGuide").hide();       // 饮食建议
    jQuery("#trConclusionFinalSportGuide").hide();      // 运动建议
    jQuery("#trConclusionFinalHealthKnowlage").hide();  // 健康建议


    jQuery("#liConclusionFinalOverView").attr("Class", "");        // 综述
    jQuery("#liConclusionFinalConclusion").attr("Class", "");      // 总检结论
    jQuery("#liConclusionIndicatorDiagnose").attr("Class", "");      // 指标异常及建议 xmhuang 2014-01-21  
    jQuery("#liConclusionOtherDiagnose").attr("Class", "");      // 其它异常及建议 xmhuang 2014-01-21       
    jQuery("#liConclusionResultCompare").attr("Class", "");        // 结果对比
    jQuery("#liConclusionMainDiagnose").attr("Class", "");     // 总检建议
    jQuery("#liConclusionFinalDietGuide").attr("Class", "");       // 饮食建议
    jQuery("#liConclusionFinalSportGuide").attr("Class", "");      // 运动建议
    jQuery("#liConclusionFinalHealthKnowlage").attr("Class", "");  // 健康建议

    switch (index) {
        case 1:
            jQuery("#trConclusionFinalOverView").show();                            // 综述
            jQuery("#liConclusionFinalOverView").attr("Class", "selected");         // 综述
            break;
        case 2:
            jQuery("#trConclusionFinalConclusion").show();                          // 总检结论
            jQuery("#liConclusionFinalConclusion").attr("Class", "selected");       // 总检结论
            break;
        case 3:
            jQuery("#trConclusionResultCompare").show();                            // 结果对比
            jQuery("#liConclusionResultCompare").attr("Class", "selected");         // 结果对比
            break;
        case 4:
            jQuery("#trConclusionMainDiagnose").show();                         // 总检建议
            jQuery("#liConclusionMainDiagnose").attr("Class", "selected");      // 总检建议
            break;
        case 5:
            jQuery("#trConclusionFinalDietGuide").show();                           // 饮食建议
            jQuery("#liConclusionFinalDietGuide").attr("Class", "selected");        // 饮食建议
            break;
        case 6:
            jQuery("#trConclusionFinalSportGuide").show();                          // 运动建议
            jQuery("#liConclusionFinalSportGuide").attr("Class", "selected");       // 运动建议
            break;
        case 7:
            jQuery("#trConclusionFinalHealthKnowlage").show();                      // 健康建议
            jQuery("#liConclusionFinalHealthKnowlage").attr("Class", "selected");   // 健康建议
            break;
        case 8:
            jQuery("#trConclusionIndicatorDiagnose").show();                      // 指标异常及建议li标签 xmhuang 2014-01-21       
            jQuery("#liConclusionIndicatorDiagnose").attr("Class", "selected");   // 指标异常及建议显示内容 xmhuang 2014-01-21       
            break;
        case 9:
            jQuery("#trConclusionOtherDiagnose").show();                      // 其它异常及建议li标签 xmhuang 2014-01-21       
            jQuery("#liConclusionOtherDiagnose").attr("Class", "selected");   // 其它异常及建议显示内容 xmhuang 2014-01-21       
            break;
    }

}

// ===============  文本域切换区域 End   ========================================================
var allWidth = jQuery("#tdLeft").width() + jQuery("#tdRight").width();
jQuery("#hidRight").hide();
jQuery("#tdRight").hide();
jQuery("#tdLeft").css("width", allWidth);
function ToggerterCenter() {
    //

    var hidLeft = jQuery.trim(jQuery("#hidLeft").text());
    if (hidLeft == "→") {
        jQuery("#hidRight").text("←");
    }
    else if (hidLeft == "←") {
        jQuery("#hidRight").text("→");
    }
    jQuery("#hidRight").show();
    jQuery("#tdLeft").css("width", "235px");
    jQuery("#tdLeft").show("slow");
    jQuery("#tdRight").css("width", allWidth - 235);
    jQuery("#tdRight").show("slow");
}

function Toggerter(objID) {
    if (jQuery(objID).attr("direction") == "left") {
        jQuery(objID).attr("direction", "right");
        jQuery(objID).attr("src", "/template/blue/images/late_right.gif");
        jQuery("#tdRight").css("width", allWidth);
        jQuery("#tdLeft").hide("slow");
        jQuery("#tdRight").show("slow");

    }
    else if (jQuery(objID).attr("direction") == "right") {
        jQuery(objID).attr("direction", "left");
        jQuery(objID).attr("src", "/template/blue/images/late_left.gif");
        ToggerterCenter();
    }
}
// 读取分科检查（自动调用）
jQuery(document).ready(function () {
    var ID_Customer = jQuery("#ID_Customer"); //页面参数
    if (ID_Customer.length > 0) {
        ID_Customer = jQuery("#ID_Customer").val();
        jQuery("#txtCustomerID").val(ID_Customer);
    }
    jQuery("#txtCustomerID").focus();
    //    jQuery("#trConclusionFinalConclusion").hide();
    //    jQuery("#trConclusionMainDiagnose").hide();
    //    jQuery("#trConclusionResultCompare").hide();
    //    jQuery("#trConclusionFinalDietGuide").hide();
    //    jQuery("#trConclusionFinalSportGuide").hide();
    //    jQuery("#trConclusionFinalHealthKnowlage").hide();
    ToggerterCenter();
    AutoGetCustomerReport();
    /*xmhuang 2014-01-23 判断是否现在指标异常及建议和其它异常及建议*/
    var IsShowIndicatorDiagnose = jQuery("#IsShowIndicatorDiagnose").val(); //是否显示指标异常及建议
    var IsShowOtherDiagnose = jQuery("#IsShowOtherDiagnose").val(); //是否显示其它异常及建议
    if (IsShowIndicatorDiagnose != 1) {
        jQuery("#trConclusionIndicatorDiagnose").hide();
        jQuery("#liConclusionIndicatorDiagnose").hide();
    }
    if (IsShowOtherDiagnose != 1) {
        jQuery("#trConclusionOtherDiagnose").hide();
        jQuery("#liConclusionOtherDiagnose").hide();
    }
    /*xmhuang 2014-01-23 判断是否现在指标异常及建议和其它异常及建议*/
});
/// <summary>
/// 根据输入情况，自动获取客户的个人信息及体检项目 （当输入数据达到体检号的数据长度时，自动读取）
/// </summary>
function AutoGetCustomerSectionExamInfo() {
    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
    if (isCustomerExamNo(customerid)) {
        //通过体检号判断是否具有操作密级
        if (ISCanReadReport(customerid)) {
            var IsShowIndicatorDiagnose = jQuery("#IsShowIndicatorDiagnose").val(); //是否显示指标异常及建议
            var IsShowOtherDiagnose = jQuery("#IsShowOtherDiagnose").val(); //是否显示其它异常及建议
            var ExamUrl = "/System/ReportManage/ReportPreview.aspx?txtCustomerID=" + customerid + "&IsShowIndicatorDiagnose=" + IsShowIndicatorDiagnose + "&IsShowOtherDiagnose=" + IsShowOtherDiagnose;
            DoLoad(ExamUrl, '');
            ShowHideFinalTextArea(1);
        }
        else {
            ShowSystemDialog("对不起，您查询的体检号不存在或者您没有权限查看此体检号的相关信息！");
            return false;
            return false;
        }
    }
}

/// <summary>
/// 获取用户是否有查看报告的权限
/// </summary>
function ISCanReadReport(ID_Customer) {
    var flag = false;
    jQuery.ajax({
        type: "GET",
        async: false,
        url: "/Ajax/AjaxReportPreview.aspx",
        data: {
            action: "ISCanReadReport",
            ID_Customer: ID_Customer
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg.success == 1) {
                flag = true;
            }
            else {
                flag = false;
            }
        }
    });
    return flag;
}

