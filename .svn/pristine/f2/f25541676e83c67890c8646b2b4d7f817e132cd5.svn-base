/// <summary>
///页面初始化
/// </summary>
jQuery(document).ready(function () {
    if (jQuery("#QueryExamListData").length > 0) {
        jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    }
    if (jQuery("#customerScrollControl").length > 0) {
        jQuery("#customerScrollControl").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    }
    //参检情况
    if (jQuery("#TeamQueryListData").length > 0) {
        jQuery("#TeamQueryListData").attr("data-left", (293 + jQuery("#ShowUserMenuDiv").height()));
    }
    //检出统计，人员构成，日期分布
    if (jQuery("#CheckOutTeamQueryListData").length > 0) {
        jQuery("#CheckOutTeamQueryListData").attr("data-left", (298 + jQuery("#ShowUserMenuDiv").height()));
    }
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    TableScrollByID("customerScrollTitle", "customerScrollControl");
    ResetSearchInfo("");
});
/// <summary>
/// 设置表横向滚动 
/// </summary>
function TableScrollByID(titleID, scrollID) {
    var $scrollControl = $("#" + scrollID);
    if ($scrollControl.length > 0) {
        var widthLeft = $scrollControl.width() - $scrollControl[0].clientWidth;
        if (widthLeft > 0) {
            var $scrollTitle = $("#" + titleID);
            $scrollTitle.css("width", $scrollTitle.width() + widthLeft);
        }
        $scrollControl.bind("scroll.j-control", function () {
            var left = $(this).scrollLeft();
            $("#" + titleID).css("margin-left", 0 - left);
        });
    }
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的条件内，没有找到任何信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}

function GetTeamWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = ""; // jQuery('#BeginExamDate').val();  //xmhuang 2014-04-11 团体移除所有时间查询
    BeginExamDate = ""; // encodeURIComponent(BeginExamDate);  //开始日期 xmhuang 2014-04-11 团体移除所有时间查询
    var EndExamDate = ""; // jQuery('#EndExamDate').val();
    EndExamDate = ""; // encodeURIComponent(EndExamDate);    //结束日期 xmhuang 2014-04-11 团体移除所有时间查询
    //判断时间差 Begin xmhuang 2014-01-14
    //    if (!CheckTime(BeginExamDate, EndExamDate)) {
    //        return false;
    //    }
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetTeamWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                dataFlag = "GetTeamWorkLoad";
                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList1).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;
                //                var rowNum = 1;
                //                var newcontent = '';
                //                var templateContent = jQuery("#TeamWorkLoadTemplate tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        //                        newcontent += templateContent.replace(/@NotSetionFinishedNum/gi, item.NotSetionFinishedNum)
                //                        //                            .replace(/@SetionFinishedNum/gi, item.SetionFinishedNum)
                //                        //                            .replace(/@FinalFinishedNum/gi, item.FinalFinishedNum)
                //                        //                            .replace(/@CheckedNum/gi, item.CheckedNum)
                //                        //                            .replace(/@ReportPrintedNum/gi, item.ReportPrintedNum)
                //                        //                            .replace(/@InformedNum/gi, item.InformedNum)
                //                        //                            .replace(/@ReportReceiptedNum/gi, item.ReportReceiptedNum)
                //                        newcontent += templateContent.replace(/@TeamWorkLoadType/gi, item.TeamWorkLoadType)
                //                            .replace(/@TeamWorkLoadNum/gi, item.TeamWorkLoadNum)
                //                            .replace(/@RowNum/gi, rowNum);
                //                        rowNum++;
                //                    }
                //                });
                //                if (newcontent != '') {
                //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                //                } else {
                //                    ResetSearchInfo("");
                //                }

                //                jQuery(msg.dataList1).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });

}


/**********************阳性结果 Begin ***************************/
/// <summary>
/// 查询团体任务的阳性结果信息
/// </summary>
function GetPositiveSummaryWorkLoad() {
ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = ""; // jQuery('#BeginExamDate').val();  //xmhuang 2014-04-11 团体移除所有时间查询
    BeginExamDate = ""; // encodeURIComponent(BeginExamDate);  //开始日期 xmhuang 2014-04-11 团体移除所有时间查询
    var EndExamDate = ""; // jQuery('#EndExamDate').val();
    EndExamDate = ""; // encodeURIComponent(EndExamDate);    //结束日期 xmhuang 2014-04-11 团体移除所有时间查询
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetPositiveSummaryWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            dataFlag = "GetPositiveSummaryWorkLoad";
            pagerData = msg.dataList0;
            tempOperPageCount = 0;
            QueryPagesData(0);
            jQuery(msg.dataList1).each(function (i, item) {
                jQuery("#loadExcel").attr("href", item.FilePath);
            });
            return false;
            //            if (msg != undefined) {
            //                var index = 1;
            //                var newcontent = '';
            //                var curPositiveSummary = "";
            //                var curMainDiagnose = "";
            //                var maxLength = 50;
            //                var templateContent = jQuery("#PositiveSummaryWorkLoadTemplate tbody").html();
            //                if (templateContent == undefined) { return false; }
            //                jQuery(msg.dataList0).each(function (i, item) {
            //                    if (templateContent != null) {
            //                        curPositiveSummary = item.PositiveSummary;
            //                        curMainDiagnose = item.MainDiagnose;
            //                        if (curPositiveSummary.length > maxLength) {
            //                            curPositiveSummary = curPositiveSummary.substring(0, maxLength) + "...";
            //                        }
            //                        if (curMainDiagnose.length > maxLength) {
            //                            curMainDiagnose = curMainDiagnose.substring(0, maxLength) + "...";
            //                        }
            //                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
            //                            .replace(/@index/gi, index)
            //                            .replace(/@CustomerName/gi, item.CustomerName)
            //                            .replace(/@GenderName/gi, item.GenderName)
            //                            .replace(/@Age/gi, item.Age)
            //                            .replace(/@MarriageName/gi, item.MarriageName)
            //                            .replace(/@TeamName/gi, item.TeamName)
            //                            .replace(/@Department/gi, item.Department)
            //                            .replace(/@DepartSubA/gi, item.DepartSubA)
            //                            .replace(/@DepartSubB/gi, item.DepartSubB)
            //                            .replace(/@DepartSubC/gi, item.DepartSubC)
            //                            .replace(/@DepartSubC/gi, item.DepartSubC)
            //                            .replace(/@SubScribDate/gi, item.SubScribDate)
            //                            .replace(/@PositiveTitleSummary/gi, item.PositiveSummary)
            //                            .replace(/@FinalTitleExplanation/gi, item.MainDiagnose)
            //                            .replace(/@PositiveSummary/gi, curPositiveSummary)
            //                            .replace(/@MainDiagnose/gi, curMainDiagnose)

            //                            ;
            //                        index++;
            //                    }
            //                });
            //                if (newcontent != '') {
            //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            //                } else {
            //                    ResetSearchInfo("");
            //                }

            //                jQuery(msg.dataList1).each(function (i, item) {
            //                    jQuery("#loadExcel").attr("href", item.FilePath);
            //                });
            //            }
            //            else {
            //                ResetSearchInfo("");
            //            }
        }
    });
}
/**********************阳性结果 End ***************************/

/**********************完成结果 End ***************************/
/// <summary>
/// 查询团体任务的完成结果信息
/// </summary>
function GetCompeleteWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = "";
    if (jQuery('#EndExamDate').length > 0) {
        BeginExamDate = jQuery('#BeginExamDate').val();
        BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    }
    var EndExamDate = "";
    if (jQuery('#EndExamDate').length > 0) {
        EndExamDate = jQuery('#EndExamDate').val();
        EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    }
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetCompeleteWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            dataFlag = "GetCompeleteWorkLoad";
            pagerData = msg.dataList0;
            tempOperPageCount = 0;
            QueryPagesData(0);
            jQuery(msg.dataList1).each(function (i, item) {
                jQuery("#loadExcel").attr("href", item.FilePath);
            });
            return false;
            //            if (msg != undefined) {
            //                var index = 1;
            //                var newcontent = '';
            //                var curNotCompeleteFeeItemName = "";
            //                var curNote = "";
            //                var maxLength = 50;
            //                var ExamState = "";
            //                var templateContent = jQuery("#CompeleteWorkLoadTemplate tbody").html();
            //                if (templateContent == undefined) { return false; }
            //                jQuery(msg.dataList0).each(function (i, item) {
            //                    if (templateContent != null) {

            //                        //如果未检项目和已检项目都不为空，证明为部分检查
            //                        if (item.NotCompeleteFeeItemName != "" && item.CompeleteFeeItemName != "") {
            //                            ExamState = "部分检查";
            //                        }
            //                        //如果已检项目为空，证明未检
            //                        if (item.CompeleteFeeItemName == "" && item.NotCompeleteFeeItemName != "") {
            //                            ExamState = "未检";
            //                        }
            //                        //如果已检项目不为空，未检项目为空，则证明全部检查
            //                        if (item.CompeleteFeeItemName != "" && item.NotCompeleteFeeItemName == "") {
            //                            ExamState = "全部已检";
            //                        }
            //                        curNotCompeleteFeeItemName = item.NotCompeleteFeeItemName;
            //                        curNote = item.Note;
            //                        if (curNotCompeleteFeeItemName.length > maxLength) {
            //                            curNotCompeleteFeeItemName = curNotCompeleteFeeItemName.substring(0, maxLength) + "...";
            //                        }
            //                        if (curNote.length > maxLength) {
            //                            curNote = curNote.substring(0, maxLength) + "...";
            //                        }
            //                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
            //                            .replace(/@index/gi, index)
            //                            .replace(/@CustomerName/gi, item.CustomerName)
            //                            .replace(/@GenderName/gi, item.GenderName)
            //                            .replace(/@Age/gi, item.Age)
            //                            .replace(/@MarriageName/gi, item.MarriageName)
            //                            .replace(/@TeamName/gi, item.TeamName)
            //                            .replace(/@Department/gi, item.Department)
            //                            .replace(/@DepartSubA/gi, item.DepartSubA)
            //                            .replace(/@DepartSubB/gi, item.DepartSubB)
            //                            .replace(/@DepartSubC/gi, item.DepartSubC)
            //                            .replace(/@DepartSubC/gi, item.DepartSubC)
            //                            .replace(/@SubScribDate/gi, item.SubScribDate)
            //                            .replace(/@ExamState/gi, ExamState)
            //                            .replace(/@NotCompeleteFeeItemName/gi, curNotCompeleteFeeItemName)
            //                            .replace(/@Note/gi, curNote)
            //                            ;
            //                        index++;
            //                    }
            //                });
            //                if (newcontent != '') {
            //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            //                } else {
            //                    ResetSearchInfo("");
            //                }

            //                jQuery(msg.dataList1).each(function (i, item) {
            //                    jQuery("#loadExcel").attr("href", item.FilePath);
            //                });
            //            }
            //            else {
            //                ResetSearchInfo("");
            //            }
        }
    });
}
/**********************完成结果 End ***************************/

/**********************日期分布 Begin ***************************/
/// <summary>
/// 查询日期分布
/// </summary>
function GetSubScribDateWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = "";
    if (jQuery('#EndExamDate').length > 0) {
        BeginExamDate = jQuery('#BeginExamDate').val();
        BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    }
    var EndExamDate = "";
    if (jQuery('#EndExamDate').length > 0) {
        EndExamDate = jQuery('#EndExamDate').val();
        EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    }
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetSubScribDateWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                dataFlag = "GetSubScribDateWorkLoad";
                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;
                //                var index = 1;
                //                var newcontent = '';
                //                var curNotCompeleteFeeItemName = "";
                //                var maxLength = 50;
                //                var ExamState = "";
                //                var templateContent = jQuery("#SubScribDateWorkLoadTemplate tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                //                            .replace(/@index/gi, index)
                //                            .replace(/@Male/gi, item.Male)
                //                            .replace(/@FeMale/gi, item.FeMale)
                //                            .replace(/@SumGender/gi, item.SumGender)
                //                            ;
                //                        index++;
                //                    }
                //                });
                //                if (newcontent != '') {
                //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                //                } else {
                //                    ResetSearchInfo("");
                //                }

                //                jQuery(msg.dataList2).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}
/**********************日期分布 End ***************************/


/**********************年龄分布 Begin ***************************/
/// <summary>
/// 查询年龄分布
/// </summary>
function GetAgeWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = ""; // jQuery('#BeginExamDate').val();  //xmhuang 2014-04-11 团体移除所有时间查询
    BeginExamDate = ""; // encodeURIComponent(BeginExamDate);  //开始日期 xmhuang 2014-04-11 团体移除所有时间查询
    var EndExamDate = ""; // jQuery('#EndExamDate').val();
    EndExamDate = ""; // encodeURIComponent(EndExamDate);    //结束日期 xmhuang 2014-04-11 团体移除所有时间查询
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetAgeWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                dataFlag = "GetAgeWorkLoad";
                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;
                //                var index = 1;
                //                var newcontent = '';
                //                var curNotCompeleteFeeItemName = "";
                //                var maxLength = 50;
                //                var ExamState = "";
                //                var templateContent = jQuery("#AgeWorkLoadTemplate tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                //                            .replace(/@index/gi, index)
                //                            .replace(/@AgeName/gi, item.AgeName)
                //                            .replace(/@Male/gi, item.Male)
                //                            .replace(/@FeMale/gi, item.FeMale)
                //                            .replace(/@SumGender/gi, item.SumGender)
                //                            .replace(/@MPer/gi, item.MalePer)
                //                            .replace(/@FPer/gi, item.FeMalePer)
                //                            .replace(/@SumPer/gi, item.SumPer);
                //                        index++;
                //                    }
                //                });
                //                if (newcontent != '') {
                //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                //                } else {
                //                    ResetSearchInfo("");
                //                }

                //                jQuery(msg.dataList2).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}
/**********************年龄分布 End ***************************/

/**********************检出统计(阳性结果) Begin ***************************/
/// <summary>
/// 查询检出统计
/// </summary>
function GetConclusionWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = ""; // jQuery('#BeginExamDate').val();  //xmhuang 2014-04-11 团体移除所有时间查询
    BeginExamDate = ""; // encodeURIComponent(BeginExamDate);  //开始日期 xmhuang 2014-04-11 团体移除所有时间查询
    var EndExamDate = ""; // jQuery('#EndExamDate').val();
    EndExamDate = ""; // encodeURIComponent(EndExamDate);    //结束日期 xmhuang 2014-04-11 团体移除所有时间查询
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetConclusionWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                dataFlag = "GetConclusionWorkLoad";
                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList1).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;

                //                var index = 1;
                //                var newcontent = '';
                //                var curNotCompeleteFeeItemName = "";
                //                var maxLength = 50;
                //                var ExamState = "";
                //                var templateContent = jQuery("#ConclusionWorkLoadTemplate tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {

                //                        newcontent += templateContent.replace(/@TeamConclusionName/gi, item.TeamConclusionName)
                //                            .replace(/@index/gi, index)
                //                            .replace(/@CheckOutMale/gi, item.CheckOutMale)
                //                            .replace(/@CheckOutFMale/gi, item.CheckOutFMale)
                //                            .replace(/@SumCheckOutCount/gi, item.SumCheckOutCount)
                //                            .replace(/@MaleCheckOutPer/gi, item.CheckOutMalePer)
                //                            .replace(/@CheckOFMalePer/gi, item.CheckOutFMalePer)
                //                            .replace(/@SumPer/gi, item.SumPer)
                //                            ;
                //                        index++;

                //                    }
                //                });
                //                if (newcontent != '') {
                //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                //                } else {
                //                    ResetSearchInfo("");
                //                }

                //                jQuery(msg.dataList1).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}
/**********************检出统计(阳性结果) End ***************************/


/**********************项目参检 Begin ***************************/
function GetCustFeeWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = ""; // jQuery('#BeginExamDate').val();  //xmhuang 2014-04-11 团体移除所有时间查询
    BeginExamDate = ""; // encodeURIComponent(BeginExamDate);  //开始日期 xmhuang 2014-04-11 团体移除所有时间查询
    var EndExamDate = ""; // jQuery('#EndExamDate').val();
    EndExamDate = ""; // encodeURIComponent(EndExamDate);    //结束日期 xmhuang 2014-04-11 团体移除所有时间查询
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetCustFeeWorkLoadOfTeam'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                dataFlag = "GetCustFeeWorkLoad";
                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;

                //                var index = 1;
                //                var newcontent = '';
                //                var curNotCompeleteFeeItemName = "";
                //                var maxLength = 50;
                //                var ExamState = "";
                //                var templateContent = jQuery("#CustFeeWorkLoadTemplate tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                //                            .replace(/@index/gi, index)
                //                            .replace(/@SectionName/gi, item.SectionName)
                //                            .replace(/@FeeName/gi, item.FeeName)
                //                            .replace(/@Male/gi, item.MaleCustFee)
                //                            .replace(/@FeMale/gi, item.FeMaleCustFee)
                //                            .replace(/@SumGender/gi, item.SumGenderCustFee)
                //                            .replace(/@MPer/gi, item.MalePerCustFee)
                //                            .replace(/@FPer/gi, item.FeMalePerCustFee)
                //                            .replace(/@SumPer/gi, item.SumPerCustFee)
                //                            ;
                //                        index++;
                //                    }
                //                });
                //                if (newcontent != '') {
                //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                //                } else {
                //                    ResetSearchInfo("");
                //                }

                //                jQuery(msg.dataList2).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}
/**********************项目参检 End ***************************/

/**********************人员构成 End ***************************/
/// <summary>
/// 查询人员构成
/// </summary>
function GetCustomerFormOfTeam() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetCustomerFormOfTeam'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                dataFlag = "GetCustomerFormOfTeam";
                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;
                //                var index = 1;
                //                var newcontent = '';
                //                var curNotCompeleteFeeItemName = "";
                //                var maxLength = 50;
                //                var ExamState = "";
                //                var templateContent = "";
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (jQuery("#ExamType" + item.ExamType)[0] != undefined) {
                //                        templateContent = jQuery("#ExamType" + item.ExamType)[0].outerHTML;
                //                        if (templateContent != null) {
                //                            newcontent += templateContent.replace(/@ExamType/gi, item.ExamType)
                //                            .replace(/@Male/gi, item.Male)
                //                            .replace(/@FMale/gi, item.FMale)
                //                            .replace(/@SumGender/gi, item.SumGender)
                //                            .replace(/@MPer/gi, item.MealePer)
                //                            .replace(/@FPer/gi, item.FMalePer)
                //                            .replace(/@SumPer/gi, item.SumPer)
                //                            ;
                //                            index++;
                //                        }
                //                    }
                //                });
                //                if (newcontent != '') {
                //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                //                } else {
                //                    ResetSearchInfo("");
                //                }

                //                jQuery(msg.dataList2).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}
/**********************人员构成 End ***************************/

/**********************结论汇总 Begin ***************************/
/// <summary>
/// 查询结论汇总
/// </summary>
function GetAllConclusionWorkLoad() {
    ResetSearchInfo("正在查询数据，请稍候...");
    //jQuery('#Searchresult').html("");
    var BeginExamDate = ""; // jQuery('#BeginExamDate').val();  //xmhuang 2014-04-11 团体移除所有时间查询
    BeginExamDate = ""; // encodeURIComponent(BeginExamDate);  //开始日期 xmhuang 2014-04-11 团体移除所有时间查询
    var EndExamDate = ""; // jQuery('#EndExamDate').val();
    EndExamDate = ""; // encodeURIComponent(EndExamDate);    //结束日期 xmhuang 2014-04-11 团体移除所有时间查询
    var ID_Team = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    var TeamTaskName = jQuery('#nameSelectTeamTask').val();
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == "") {
        ShowSystemDialog("对不起，请您选择需要查看的团体任务!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Team: ID_Team,
            TeamName: TeamName,
            ID_TeamTask: ID_TeamTask,
            TeamTaskName: TeamTaskName,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetAllConclusionWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            dataFlag = "GetAllConclusionWorkLoad";
            pagerData = msg.dataList0;
            tempOperPageCount = 0;
            QueryPagesData(0);
            jQuery(msg.dataList1).each(function (i, item) {
                jQuery("#loadExcel").attr("href", item.FilePath);
            });
            return false;
            //            if (msg != undefined) {
            //                var index = 1;
            //                var newcontent = '';
            //                var curNotCompeleteFeeItemName = "";
            //                var maxLength = 50;
            //                var ExamState = "";
            //                var templateContent = jQuery("#AllConclusionWorkLoadTemplate tbody").html();
            //                if (templateContent == undefined) { return false; }
            //                jQuery(msg.dataList0).each(function (i, item) {
            //                    if (templateContent != null) {
            //                        newcontent += templateContent.replace(/@TeamConclusionName/gi, item.TeamConclusionName)
            //                            .replace(/@index/gi, index)
            //                            .replace(/@ConclusionName/gi, item.ConclusionName)
            //                            .replace(/@ID_Customer/gi, item.ID_Customer)
            //                            .replace(/@CustomerName/gi, item.CustomerName)
            //                            .replace(/@GenderName/gi, item.GenderName)
            //                            .replace(/@Age/gi, item.Age)
            //                            .replace(/@Department/gi, item.Department)
            //                            .replace(/@DepartSubA/gi, item.DepartSubA)
            //                            .replace(/@DepartSubB/gi, item.DepartSubB)
            //                            .replace(/@DepartSubC/gi, item.DepartSubC)
            //                            ;
            //                        index++;
            //                    }
            //                });
            //                if (newcontent != '') {
            //                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            //                } else {
            //                    ResetSearchInfo("");
            //                }

            //                jQuery(msg.dataList1).each(function (i, item) {
            //                    jQuery("#loadExcel").attr("href", item.FilePath);
            //                });

            //            }
            //            else {
            //                ResetSearchInfo("");
            //            }
        }
    });
}
/**********************结论汇总 End ***************************/

/*通用客户端分页脚本 xmhuang 2014-01-13  Begin*/
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );
var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var pagerData = null; //记录当前分页数据源
var dataFlag = null;
function pageselectCallback(page_index, jq) {
    if (tempOperPageCount > 0) {
        tempOperPageCount++;
        QueryPagesData(page_index);
    }
    tempOperPageCount++;
    return false;
}
function QueryPagesData(pageIndex) {
    if (pagerData != null) {
        if (pagerData.length == 0) {
            ResetSearchInfo("");
            jQuery("#Pagination").hide();
            FixedTable();
            return false;
        }
        var maxLength = 50;
        var dataLength = pagerData.length;
        var flag = jQuery.trim(jQuery('#flag').val());
        var curPageSize = pagePagination.items_per_page;
        var optInit = getOptionsFromForm();                   //获取分页配置参数
        jQuery("#Pagination").show();
        if (tempOperPageCount == 0) {
            jQuery("#Pagination ul").pagination(dataLength, optInit);
        }
        else if (tempOldtotalCount != dataLength) {
            jQuery("#Pagination ul").pagination(dataLength, optInit);
        }
        tempOldtotalCount = dataLength;
        var item;
        var rowNum = curPageSize * pageIndex; //计算
        var newcontent = '';
        var templateContent = "";
        if (dataFlag == "GetCompeleteWorkLoad")//完成情况 
        {
            templateContent = jQuery("#CompeleteWorkLoadTemplate tbody").html();
        }
        else if (dataFlag == "GetAllConclusionWorkLoad") //结论汇总
        {
            templateContent = jQuery("#AllConclusionWorkLoadTemplate tbody").html();
        }
        else if (dataFlag == "GetPositiveSummaryWorkLoad")//阳性结果
        {
            templateContent = jQuery("#PositiveSummaryWorkLoadTemplate").html();
        }
        else if (dataFlag == "GetConclusionWorkLoad")//阳性结果
        {
            templateContent = jQuery("#ConclusionWorkLoadTemplate tbody").html();
        }
        else if (dataFlag == "GetCustFeeWorkLoad")//参检情况 xmhuang 2014-04-09
        {
            templateContent = jQuery("#CustFeeWorkLoadTemplate tbody").html();
        }
        else if (dataFlag == "GetAgeWorkLoad")//年龄分布 xmhuang 2014-04-09
        {
            templateContent = jQuery("#AgeWorkLoadTemplate tbody").html();
        }
        else if (dataFlag == "GetCustomerFormOfTeam")//人员构成 xmhuang 2014-04-09
        {

        }
        else if (dataFlag == "GetSubScribDateWorkLoad")//日期分布 xmhuang 2014-04-09
        {
            templateContent = jQuery("#SubScribDateWorkLoadTemplate tbody").html();
        }
        else if (dataFlag == "GetTeamWorkLoad")//团体信息统计 xmhuang 2014-04-09
        {
            templateContent = jQuery("#TeamWorkLoadTemplate tbody").html();
        }

        var curTemplateContent = "";                  //用于专门记录人员构成模板变量
        var curPositiveSummary, curMainDiagnose; //阳性结果使用变量
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    //完成情况 Begin
                    if (dataFlag == "GetCompeleteWorkLoad") {
                        //如果未检项目和已检项目都不为空，证明为部分检查
                        if (item.NotCompeleteFeeItemName != "" && item.CompeleteFeeItemName != "") {
                            ExamState = "部分检查";
                        }
                        //如果已检项目为空，证明未检
                        if (item.CompeleteFeeItemName == "" && item.NotCompeleteFeeItemName != "") {
                            ExamState = "未检";
                        }
                        //如果已检项目不为空，未检项目为空，则证明全部检查
                        if (item.CompeleteFeeItemName != "" && item.NotCompeleteFeeItemName == "") {
                            ExamState = "全部已检";
                        }
                        curNotCompeleteFeeItemName = item.NotCompeleteFeeItemName;
                        curNote = item.Note;
                        if (curNotCompeleteFeeItemName.length > maxLength) {
                            curNotCompeleteFeeItemName = curNotCompeleteFeeItemName.substring(0, maxLength) + "...";
                        }
                        if (curNote.length > maxLength) {
                            curNote = curNote.substring(0, maxLength) + "...";
                        }
                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@Age/gi, item.Age)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@Department/gi, item.Department)
                            .replace(/@DepartSubA/gi, item.DepartSubA)
                            .replace(/@DepartSubB/gi, item.DepartSubB)
                            .replace(/@DepartSubC/gi, item.DepartSubC)
                            .replace(/@DepartSubC/gi, item.DepartSubC)
                            .replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@ExamState/gi, ExamState)
                            .replace(/@NotCompeleteFeeItemName/gi, curNotCompeleteFeeItemName)
                            .replace(/@NotCompeleteTitleFeeItemName/gi, item.NotCompeleteFeeItemName)
                            .replace(/@Note/gi, curNote)
                            ;
                    } //完成情况 End
                    else if (dataFlag == "GetAllConclusionWorkLoad") {
                        newcontent += templateContent.replace(/@TeamConclusionName/gi, item.TeamConclusionName)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@ConclusionName/gi, item.ConclusionName)
                            .replace(/@DiagnoseType/gi, item.DiagnoseType)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@Age/gi, item.Age)
                            .replace(/@Department/gi, item.Department)
                            .replace(/@DepartSubA/gi, item.DepartSubA)
                            .replace(/@DepartSubB/gi, item.DepartSubB)
                            .replace(/@DepartSubC/gi, item.DepartSubC)
                    }
                    else if (dataFlag == "GetPositiveSummaryWorkLoad")//阳性结果
                    {
                        curPositiveSummary = item.PositiveSummary;
                        curMainDiagnose = item.MainDiagnose;
                        if (curPositiveSummary.length > maxLength) {
                            curPositiveSummary = curPositiveSummary.substring(0, maxLength) + "...";
                        }
                        if (curMainDiagnose.length > maxLength) {
                            curMainDiagnose = curMainDiagnose.substring(0, maxLength) + "...";
                        }
                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@Age/gi, item.Age)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@Department/gi, item.Department)
                            .replace(/@DepartSubA/gi, item.DepartSubA)
                            .replace(/@DepartSubB/gi, item.DepartSubB)
                            .replace(/@DepartSubC/gi, item.DepartSubC)
                            .replace(/@DepartSubC/gi, item.DepartSubC)
                            .replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@PositiveTitleSummary/gi, item.PositiveSummary)
                            .replace(/@FinalTitleExplanation/gi, item.MainDiagnose)
                            .replace(/@PositiveSummary/gi, curPositiveSummary)
                            .replace(/@MainDiagnose/gi, curMainDiagnose)
                            .replace(/@SecondaryDiagnose/gi, item.SecondaryDiagnose)
                            .replace(/@IndicatorDiagnose/gi, item.IndicatorDiagnose)
                            .replace(/@NormalDiagnose/gi, item.NormalDiagnose)
                            .replace(/@OtherDiagnose/gi, item.OtherDiagnose)
                            .replace(/@ResultCompare/gi, item.ResultCompare)

                    }
                    else if (dataFlag == "GetConclusionWorkLoad") {
                        newcontent += templateContent.replace(/@TeamConclusionName/gi, item.TeamConclusionName)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@CheckOutMale/gi, item.CheckOutMale)
                            .replace(/@CheckOutFMale/gi, item.CheckOutFMale)
                            .replace(/@SumCheckOutCount/gi, item.SumCheckOutCount)
                            .replace(/@MaleCheckOutPer/gi, item.CheckOutMalePer)
                            .replace(/@CheckOFMalePer/gi, item.CheckOutFMalePer)
                            .replace(/@SumPer/gi, item.SumPer)
                    }
                    else if (dataFlag == "GetCustFeeWorkLoad")//参检情况 xmhuang 2014-04-09
                    {
                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@FeeName/gi, item.FeeName)
                            .replace(/@Male/gi, item.MaleCustFee)
                            .replace(/@FeMale/gi, item.FeMaleCustFee)
                            .replace(/@SumGender/gi, item.SumGenderCustFee)
                            .replace(/@MPer/gi, item.MalePerCustFee)
                            .replace(/@FPer/gi, item.FeMalePerCustFee)
                            .replace(/@SumPer/gi, item.SumPerCustFee)
                    }
                    else if (dataFlag == "GetAgeWorkLoad")//年龄分布 xmhuang 2014-04-09
                    {
                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@AgeName/gi, item.AgeName)
                            .replace(/@Male/gi, item.Male)
                            .replace(/@FeMale/gi, item.FeMale)
                            .replace(/@SumGender/gi, item.SumGender)
                            .replace(/@MPer/gi, item.MalePer)
                            .replace(/@FPer/gi, item.FeMalePer)
                            .replace(/@SumPer/gi, item.SumPer)
                    }
                    else if (dataFlag == "GetCustomerFormOfTeam")//人员构成 xmhuang 2014-04-09
                    {
                        var curTemplateContent = jQuery("#ExamType" + item.ExamType)[0].outerHTML;
                        if (curTemplateContent != null) {
                            newcontent += curTemplateContent.replace(/@ExamType/gi, item.ExamType)
                            .replace(/@Male/gi, item.Male)
                            .replace(/@FMale/gi, item.FMale)
                            .replace(/@SumGender/gi, item.SumGender)
                            .replace(/@MPer/gi, item.MealePer)
                            .replace(/@FPer/gi, item.FMalePer)
                            .replace(/@SumPer/gi, item.SumPer)
                        }
                    }
                    else if (dataFlag == "GetSubScribDateWorkLoad")//日期分布 xmhuang 2014-04-09
                    {
                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@Male/gi, item.Male)
                            .replace(/@FeMale/gi, item.FeMale)
                            .replace(/@SumGender/gi, item.SumGender)
                    }
                    else if (dataFlag == "GetTeamWorkLoad")//团体信息统计 xmhuang 2014-04-09
                    {
                        newcontent += templateContent.replace(/@TeamWorkLoadType/gi, item.TeamWorkLoadType)
                            .replace(/@TeamWorkLoadNum/gi, item.TeamWorkLoadNum)
                            .replace(/@RowNum/gi, rowNum + c + 1)
                    }
                }
            }
        }
        if (newcontent != '') {
            //flag:0:折扣查询  1：记账明细查询
            if (flag == 1) {
                jQuery('#tdFeeWayName').show();
            }
            else {
                jQuery('#tdFeeWayName').hide();
            }
            jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            SetTableRowStyle();
        }
        else {
            ResetSearchInfo("");
        }
        FixedTable();
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
    }
}
/*通用客户端分页脚本 xmhuang 2014-01-13  End*/
function FixedTable() {
    //设置固定表头 xmhuang 2014-04-02
    //$('#tbCustomerList').tablefix({ height: 400, width: 940, fixRows: 1, fixCols: 8 });
}
