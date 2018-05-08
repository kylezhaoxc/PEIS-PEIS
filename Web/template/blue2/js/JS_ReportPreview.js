/****************报告预览  Begin***********************/
var FastReportView = "";         //报告预览插件
var IsShowIndicatorDiagnose = 0; //是否显示指标异常
var IsShowOtherDiagnose = 0; //是否显示其它诊断 
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
// 读取分科检查（自动调用）
jQuery(document).ready(function () {
    // 先隐藏数据元素，显示正在查询等字样
    HideAllReportViewInfo();

    FastReportView = document.getElementById("FastReportView");   
    
    if (FastReportView != null) {
        FastReportView.ClearChirldControl();    //清除所有子控件
    }
    if (jQuery("#showDialog").length > 0) {
        jQuery("#showDialog").drag({ handler: jQuery(".customerListtitle") });        //客户名单列表拖动
    }

    // SwitchHeader(99); // 隐藏头部
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    var ID_Customer = jQuery("#ID_Customer"); //页面参数
    if (ID_Customer.length > 0) {
        ID_Customer = jQuery("#ID_Customer").val();
        jQuery("#txtCustomerID").val(ID_Customer);
    }
    jQuery("#txtCustomerID").focus();
    AutoGetCustomerReport();
    /*xmhuang 2014-01-23 判断是否现在指标异常和其它诊断*/
    var IsShowIndicatorDiagnose = jQuery("#IsShowIndicatorDiagnose").val(); //是否显示指标异常
    var IsShowOtherDiagnose = jQuery("#IsShowOtherDiagnose").val(); //是否显示其它诊断
    if (IsShowIndicatorDiagnose != 1) {
        jQuery("#trConclusionIndicatorDiagnose").hide();
        jQuery("#liConclusionIndicatorDiagnose").hide();
    }
    if (IsShowOtherDiagnose != 1) {
        jQuery("#trConclusionOtherDiagnose").hide();
        jQuery("#liConclusionOtherDiagnose").hide();
    }
    /*xmhuang 2014-01-23 判断是否现在指标异常和其它诊断*/

});
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
            //首先判断体检号是否正确
            var customerid = jQuery.trim(jQuery("#txtCustomerID").val());
            if (isCustomerExamNo(customerid)) {
                AutoGetCustomerSectionExamInfo();
            }
            else {
                ShowCallBackSystemDialog("对不起，请输入正确的体检号！", function () {
                    if (customerid == "") {
                        jQuery("#txtCustomerID").val(" ");
                    }
                    jQuery("#txtCustomerID").focus();
                    jQuery("#txtCustomerID").select();
                });
                ResetSearchInfo("");
                return false;
            }
            return false;
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

                    // 判断表格是否存在滚动条,并设置相应的样式
                    JudgeTableIsExistScroll();
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
    var examSectionContent = "", Is_EvenLine = true; var rowNum = 1;
    jQuery(json.dataList0).each(function (j, examsectionitem) {

        examSectionContent += CustExamSectionListTempleteContent
                            .replace(/@RowNum/gi, rowNum)
                            .replace(/@SectionName/gi, examsectionitem.SectionName)
                            .replace(/@SectionSummaryText/gi, htmlEncode(examsectionitem.SectionSummary.replace(/-{15}/gi, '')))
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
        rowNum++;
    });
    jQuery("#CustExamSectionList").html(examSectionContent);
    jQuery("#divShowDetail").show();
    SeeMore();
    SetTableRowStyle();
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

        /*
        // 是否是总审
        var Is_FinalCheck = jQuery("#Is_FinalCheck").val();
        // 总审通过
        var Is_Checked = jQuery("#Is_Checked").val();

        // 完成总检 （审核不通过时，将完成总检标记置为False）
        var Is_FinalFinished = jQuery("#Is_FinalFinished").val();

        var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
        if (TipsMessageTempleteContent == undefined) { return; }
        var ExamState = jQuery('#ExamState').val();                         //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
        //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
        if (ExamState == "") {
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入客户的体检号！"));   //显示空信息
        jQuery('#TipsArea').show();
        return;
        }

        if (ExamState != "0") {
        var showmsg = "该客户已归档，所以不能进行报告的预览！";
        if (Is_FinalCheck == "True") {
        showmsg = "该客户已归档，所以不能进行报告的预览！";
        }
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
        }


        if (Is_FinalCheck == "True" && Is_FinalFinished != "True") {
        var ID_FinalDoctor = jQuery("#ID_FinalDoctor").val();  // 总检医生ID 根据总检医生ID是否为空，判断是否是未通过的总检
        var showmsg = "该客户体检信息还未进行总检，所以不能进行报告的预览！";
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
        }

        var Is_GuideSheetPrinted = jQuery("#Is_GuideSheetPrinted").val();         // 指引单是否打印
        var Is_Paused = jQuery("#Is_Paused").val();                               // 是否禁检
        var Is_FeeSettled = jQuery("#Is_FeeSettled").val();                       // 是否完成缴费

        if (Is_FeeSettled != "True") {
        var showmsg = "该客户还未完成缴费，不能进行报告的预览！";
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
        }

        if (Is_Paused == "True" || Is_Paused == "1") {
        var showmsg = "该客户已经禁检，不能进行报告的预览！";
        if (Is_FinalCheck == "True") {
        showmsg = "该客户已经禁检，不能进行报告的预览！";
        }
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
        }

        if (Is_GuideSheetPrinted != "True") {
        var showmsg = "该客户指引单还未打印，所以不能进行报告的预览！";

        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
        }
        */

        // 显示数据元素
        ShowAllReportViewInfo();

        //绑定左侧基本信息
        SearchCustomerBaseInfo(1, 1); //原型：SearchCustomerBaseInfo(IsShowMsg, IsLoadCustomerInfo)
        //绑定分科检查信息和总检信息
        GetCustExamSectionItem();
    }
}


/// <summary>
/// 隐藏内容区的内容
/// </summary>
function HideAllReportViewInfo() {

    jQuery("#btnCustomerSimpleInfo").hide();  // 客户基本信息
    jQuery("#divCustomerBaseInfo").hide();    // 客户信息
    jQuery("#fkls").hide();    // Tab 切换
    jQuery("#fklscen").hide();    // 中间数据部分

    jQuery('#TipsArea').show(); // 显示页面内部的提示信息 20140408 by wtang 

}

/// <summary>
/// 显示内容区的内容
/// </summary>
function ShowAllReportViewInfo() {

    jQuery('#TipsArea').hide(); // 隐藏页面内部的提示信息 20140408 by wtang 

    jQuery("#btnCustomerSimpleInfo").show();  // 客户基本信息
    jQuery("#divCustomerBaseInfo").hide();    // 客户信息(刚加载时，内容部分默认为隐藏)
    jQuery("#fkls").show();    // Tab 切换
    jQuery("#fklscen").show();    // 中间数据部分


}


//function PreviewReport() {

//    var ID_Customer = jQuery.trim(jQuery("#lblID_Customer").text());
//    if (isCustomerExamNo(ID_Customer)) {
//        var Is_ReportPrinted = jQuery.trim(jQuery("#lblIs_ReportPrinted").text()) == "√" ? 1 : 0;
//        //FastReport.ReportPreview(ID_Customer, "ExamReport_Common_Caption.frx", "1", Is_ReportPrinted);
//        //从配置节点中读取当前报告打印调用的模板 xmhuang 20140424
//        FastReport.ReportPreview(ID_Customer, jQuery("#ExamReport").val(), "1", Is_ReportPrinted);
//        FastReportView.ReportPreview(ID_Customer, jQuery("#ExamReport").val(), "1", Is_ReportPrinted);
//        jQuery(FastReportView).show();

//    }
//}

function CustReportPreview() {
    var customerid = jQuery.trim(jQuery("#txtCustomerID").val());
    if (isCustomerExamNo(customerid)) {
        var ExamPreviewReport = "ExamReport_Category_V1.1.9_Male_NoColorX_CustCOMMON.frx"; //  jQuery("#ExamReport").val();
        if (ExamPreviewReport == "" || ExamPreviewReport == undefined) {
            ShowSystemDialog("报告预览模板未配置，请配置后再预览！");
            return;
        }
        jQuery(".hiddleScroll").hide();
        var Is_ReportPrinted = jQuery.trim(jQuery("#lblIs_ReportPrinted").text()) == "√" ? 1 : 0;
        var Is_Checked = jQuery.trim(jQuery("#lblIs_Checked").text()) == "√" ? 1 : 0;
        //必须满足总审，且打印过报告的客户方能打印 xmhuang 20141023 
        var customerName = jQuery("#lblCustomerName").text();
        var tips = "[" + customerName + "(" + customerid + ")" + "]报告预览";
        if (Is_ReportPrinted == 1 && Is_Checked == 0) {
            tips = "[" + customerName + "(" + customerid + ")" + "]尚未总审!";
        }
        if (Is_ReportPrinted == 1 && Is_Checked == 1) {
            Is_ReportPrinted = 1;
        }
        else {
            Is_ReportPrinted = 0;
        } 
        // var strContent = '<div style="width: 836px; height: 100%;vertical-align:top; min-height:700px; border:1px solid #dcdcdc;"><object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style="top:0px;width: 100%; height: 100%; min-height:700px;"> </object></div>';
        //var strContent = '<object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style=" display:none;width: 1024px; min-height:500px;"> </object>';
        var strContent = '<object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style=" display:none;width: 836px; min-height:480px;"> </object>';
        art.dialog({
            id: 'artDlgReportView',
            title: tips,
            lock: true,
            fixed: true,
            padding: 0,
            width: 836,
            height: '100%',
            opacity: 0.3,
            content: strContent,
            resize: false,
            init: function () {
                var FastReportView = document.getElementById("FastReportView");
                if (FastReportView != null) {
                    FastReportView.ClearChirldControl();    //清除所有子控件 
                    //判断当前客户的体检类型是否包含为彩色打印 Begin
                    var ExamType = jQuery.trim(jQuery("#lblExamType").text());       //客户体检类型
                    var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                    if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                        //获取客户性别
                        var Sex = jQuery("#lblSex").text();
                        if (Sex == "男") {
                            var MaleColorReport = "ExamReport_Category_V1.1.9_Male_NoLisBorderX_CustVIP.frx"; // jQuery("#MaleColorReport").val();
                            FastReportView.CustReportPreview(customerid, MaleColorReport, 1, Is_ReportPrinted);
                        }
                        else if (Sex == "女") {
                            var FeMaleColorReport = "ExamReport_Category_V1.1.9_FeMale_NoLisBorderX_CustVIP.frx"; // jQuery("#FeMaleColorReport").val();
                            FastReportView.CustReportPreview(customerid, FeMaleColorReport, 1, Is_ReportPrinted);
                        }
                        else {
                            //性别不详则默认报告打印
                            FastReportView.CustReportPreview(customerid, ExamPreviewReport, 1, Is_ReportPrinted);
                        }
                    }
                    else {
                        FastReportView.CustReportPreview(customerid, ExamPreviewReport, 1, Is_ReportPrinted);
                    }
                    //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                    jQuery("#FastReportView").css("height", jQuery("#FastReportView").parent().parent().height() + "px");
                    jQuery("#FastReportView").show();
                }
            },
            close: function () {
                jQuery("#FastReportView").hide();
                //jQuery(".hiddleScroll").show();
            }
        });


    } else {

        ShowSystemDialog("体检号错误，不能预览报告！"); ResetSearchInfo("");
    }
}
function PreviewReport() {
    var customerid = jQuery.trim(jQuery("#txtCustomerID").val()); 
    if (isCustomerExamNo(customerid)) {
        var ExamPreviewReport = jQuery("#ExamReport").val();
        if (ExamPreviewReport == "" || ExamPreviewReport == undefined) {
            ShowSystemDialog("报告预览模板未配置，请配置后再预览！");
            return;
        }
        jQuery(".hiddleScroll").hide();
        var Is_ReportPrinted = jQuery.trim(jQuery("#lblIs_ReportPrinted").text()) == "√" ? 1 : 0;
        var Is_Checked = jQuery.trim(jQuery("#lblIs_Checked").text()) == "√" ? 1 : 0;
        //必须满足总审，且打印过报告的客户方能打印 xmhuang 20141023 
        var customerName = jQuery("#lblCustomerName").text();
        var tips = "[" + customerName + "(" + customerid + ")" + "]报告预览";
        if (Is_ReportPrinted == 1 && Is_Checked == 0) {
            tips = "[" + customerName + "(" + customerid + ")" + "]尚未总审!";
        }
        if (Is_ReportPrinted == 1 && Is_Checked == 1) {
            Is_ReportPrinted = 1;
        }
        else {
            Is_ReportPrinted = 0;
        }
        // var strContent = '<div style="width: 836px; height: 100%;vertical-align:top; min-height:700px; border:1px solid #dcdcdc;"><object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style="top:0px;width: 100%; height: 100%; min-height:700px;"> </object></div>';
        //var strContent = '<object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style=" display:none;width: 1024px; min-height:500px;"> </object>';
        var strContent = '<object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style=" display:none;width: 836px; min-height:480px;"> </object>';
        art.dialog({
            id: 'artDlgReportView',
            title: tips,
            lock: true,
            fixed: true,
            padding: 0,
            width: 836,
            height: '100%',
            opacity: 0.3,
            content: strContent,
            resize: false,
            init: function () {
                var FastReportView = document.getElementById("FastReportView");
                if (FastReportView != null) {
                    FastReportView.ClearChirldControl();    //清除所有子控件 
                    //判断当前客户的体检类型是否包含为彩色打印 Begin
                    var ExamType = jQuery.trim(jQuery("#lblExamType").text());       //客户体检类型
                    var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                    var IsUseCustReport = jQuery.trim(jQuery("#IsUseCustReport").val()); 
                    if (IsUseCustReport == 1 || IsUseCustReport == "True") {
                        ExamPreviewReport = jQuery("#CustExamReport").val();
                        if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                            //获取客户性别
                            var Sex = jQuery("#lblSex").text();
                            if (Sex == "男") {
                                var MaleColorReport = jQuery("#CustMaleColorReport").val();
                                FastReportView.CustReportPreview(customerid, MaleColorReport, "1", Is_ReportPrinted);
                            }
                            else if (Sex == "女") {
                                var FeMaleColorReport = jQuery("#CustFeMaleColorReport").val();
                                FastReportView.CustReportPreview(customerid, FeMaleColorReport, "1", Is_ReportPrinted);
                            }
                            else {
                                //性别不详则默认报告打印
                                FastReportView.CustReportPreview(customerid, ExamPreviewReport, "1", Is_ReportPrinted);
                            }
                        }
                        else {
                            FastReportView.CustReportPreview(customerid, ExamPreviewReport, "1", Is_ReportPrinted);
                        }
                    }
                    else {
                        if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                            //获取客户性别
                            var Sex = jQuery("#lblSex").text();
                            if (Sex == "男") {
                                var MaleColorReport = jQuery("#MaleColorReport").val();
                                FastReportView.ReportPreview(customerid, MaleColorReport, "1", Is_ReportPrinted);
                            }
                            else if (Sex == "女") {
                                var FeMaleColorReport = jQuery("#FeMaleColorReport").val();
                                FastReportView.ReportPreview(customerid, FeMaleColorReport, "1", Is_ReportPrinted);
                            }
                            else {
                                //性别不详则默认报告打印
                                FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", Is_ReportPrinted);
                            }
                        }
                        else {
                            FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", Is_ReportPrinted);
                        }
                    }

                    //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                    jQuery("#FastReportView").css("height", jQuery("#FastReportView").parent().parent().height() + "px");
                    jQuery("#FastReportView").show();
                }
            },
            close: function () {
                jQuery("#FastReportView").hide();
                //jQuery(".hiddleScroll").show();
            }
        });


    } else {

        ShowSystemDialog("体检号错误，不能预览报告！"); ResetSearchInfo("");
    }
}

function PreviewReport_Waste() {
    var customerid = jQuery.trim(jQuery("#txtCustomerID").val());
    if (isCustomerExamNo(customerid)) {
        var ExamPreviewReport = jQuery("#ExamReport").val();
        if (ExamPreviewReport == "" || ExamPreviewReport == undefined) {
            ShowSystemDialog("报告预览模板未配置，请配置后再预览！");
            return;
        }
        jQuery(".hiddleScroll").hide();
        var Is_ReportPrinted = jQuery.trim(jQuery("#lblIs_ReportPrinted").text()) == "√" ? 1 : 0;
        var Is_Checked = jQuery.trim(jQuery("#lblIs_Checked").text()) == "√" ? 1 : 0;
        //必须满足总审，且打印过报告的客户方能打印 xmhuang 20141023 
        var customerName = jQuery("#lblCustomerName").text();
        var tips = "[" + customerName + "(" + customerid + ")" + "]报告预览";
        if (Is_ReportPrinted == 1 && Is_Checked == 0) {
            tips = "[" + customerName + "(" + customerid + ")" + "]尚未总审!";
        }
        if (Is_ReportPrinted == 1 && Is_Checked == 1) {
            Is_ReportPrinted = 1;
        }
        else {
            Is_ReportPrinted = 0;
        }
        // var strContent = '<div style="width: 836px; height: 100%;vertical-align:top; min-height:700px; border:1px solid #dcdcdc;"><object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style="top:0px;width: 100%; height: 100%; min-height:700px;"> </object></div>';
        var strContent = '<object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style=" display:none;width: 1024px; min-height:500px;"> </object>';
        art.dialog({
            id: 'artDlgReportView',
            title: tips,
            lock: true,
            fixed: true,
            padding: 0,
            width: 1024,
            height: '98.91%',
            opacity: 0.3,
            content: strContent,
            resize: false,
            init: function () {
                var FastReportView = document.getElementById("FastReportView");
                if (FastReportView != null) {
                    FastReportView.ClearChirldControl();    //清除所有子控件 
                    //判断当前客户的体检类型是否包含为彩色打印 Begin
                    var ExamType = jQuery.trim(jQuery("#lblExamType").text());       //客户体检类型
                    var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                    var IsUseCustReport = jQuery.trim(jQuery("#IsUseCustReport").val());
                    if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                        //获取客户性别
                        var Sex = jQuery("#lblSex").text();
                        if (Sex == "男") {
                            var MaleColorReport = jQuery("#MaleColorReport").val();
                            FastReportView.ReportPreview(customerid, MaleColorReport, "1", Is_ReportPrinted);
                        }
                        else if (Sex == "女") {
                            var FeMaleColorReport = jQuery("#FeMaleColorReport").val();
                            FastReportView.ReportPreview(customerid, FeMaleColorReport, "1", Is_ReportPrinted);
                        }
                        else {
                            //性别不详则默认报告打印
                            FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", Is_ReportPrinted);
                        }
                    }
                    else {
                        FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", Is_ReportPrinted);
                    }
                    //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                    jQuery("#FastReportView").css("height", jQuery("#FastReportView").parent().parent().height() + "px");
                    jQuery("#FastReportView").show();
                }
            },
            close: function () {
                jQuery("#FastReportView").hide();
                //jQuery(".hiddleScroll").show();
            }
        });


    } else {

        ShowSystemDialog("体检号错误，不能预览报告！"); ResetSearchInfo("");
    }
}
/****************报告预览  End***********************/
// ===============  文本域切换区域 Start   ========================================================


/// <summary>
/// 根据输入情况，自动获取客户的个人信息及体检项目 （当输入数据达到体检号的数据长度时，自动读取）
/// </summary>
function AutoGetCustomerSectionExamInfo() {
    HideAllReportViewInfo();
    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
    if (isCustomerExamNo(customerid)) {
        //通过体检号判断是否具有操作密级
        if (ISCanReadReport(customerid)) {
            var IsShowIndicatorDiagnose = jQuery("#IsShowIndicatorDiagnose").val(); //是否显示指标异常
            var IsShowOtherDiagnose = jQuery("#IsShowOtherDiagnose").val(); //是否显示其它诊断
            var ExamUrl = "/System/ReportManage/ReportPreview.aspx?CustomerID=" + customerid + "&IsShowIndicatorDiagnose=" + IsShowIndicatorDiagnose + "&IsShowOtherDiagnose=" + IsShowOtherDiagnose;
            DoLoad(ExamUrl, '');

        }
        else {
            ShowSystemDialog("对不起，您查询的体检号不存在或者您没有权限查看此体检号的相关信息！"); ResetSearchInfo("");
            return false;
        }
    }
    else {
        ShowSystemDialog("对不起，请输入正确的体检号！"); ResetSearchInfo("");
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

//查看详细 xmhuang 2014-03-26
function SeeMore() {
    jQuery("#acefwe .hutia").subLength({ len: 80 }); // 隐藏分科多余的内容，只显示100个字
    //    jQuery("#acefwe .hutia").each(function () {
    //        var o = this;
    //        var s = jQuery(this).text(); // o.innerHTML;
    //        o.title = s;
    //        var p = document.createElement("span");
    //        var n = document.createElement("big");
    //        p.innerHTML = s.substring(0, 80);
    //        n.innerHTML = s.length > 80 ? "...[详细]" : "";
    //        n.href = "###";
    //        n.onclick = function () {
    //            if (n.innerHTML == "...[详细]") {
    //                n.innerHTML = "[收起]";
    //                p.innerHTML = s;
    //            } else {
    //                n.innerHTML = "...[详细]";
    //                p.innerHTML = s.substring(0, 80);
    //            }
    //        }
    //        o.innerHTML = "";
    //        o.appendChild(p);
    //        o.appendChild(n);
    //    });
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "对不起，没有找到任何分科检查信息！";
    }
    var empty = "";
    //重置客户基本信息
    jQuery("label[name='lblCustomerName']").text(empty);
    jQuery("#lblCustomerName").text(empty);
    jQuery("#lblSex").text(empty);
    jQuery("#lblIDCard").text(empty);
    jQuery("#lblTel").text(empty);
    jQuery("#lblMarriedName").text(empty);
    //设置头像

    jQuery("#HeadImg").attr("src", defalutImagSrc);

    //绑定用户基本信息

    jQuery("#lblAge").text(empty);

    //绑定用户基本信息
    jQuery("#lblID_Customer").text(empty);
    jQuery("#lblID_Customer").data("Code128c", empty);
    jQuery("#lblRegisterDate").text(empty);
    jQuery("#lblTeamName").text(empty);
    jQuery("#lblExamType").text(empty);

    jQuery("#lblOperateDate").text(empty);
    jQuery("#lblOperator").text(empty);

    //重置科室检查
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#CustExamSectionList').html(html); //设置无数据检索时显示提示信息

    //重置总检结论
    for (var c = 1; c < 13; c++) {
        jQuery("#c" + c).find("span").empty();
    }

    //重置名称
    jQuery("#lblCustomerName").text(empty);
}