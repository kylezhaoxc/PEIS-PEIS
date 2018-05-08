/// <summary>
///页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("#slTeam").select2();
    jQuery("#slTeamTask").select2();
    ResetSearchInfo("尚无任何数据...");

    //为团体绑定CLick事件
    jQuery("#slTeam").click(function () {
        var selecteValue = jQuery(this).find("option:selected").val();
        jQuery("#slTeam").data("ID_Team", selecteValue);
        if (selecteValue != -1) {
            jQuery.ajax({

                type: "POST",
                url: "/Ajax/AjaxTeamOper.aspx",
                data: { action: "GetTeamTaskByTeam", ID_Team: selecteValue },
                cache: false,
                dataType: "json",
                success: function (msg) {
                    BindTeamTaskInfo(msg);
                }
            });
        }
    });
});
/// <summary>
/// 绑定团体任务信息
/// </summary>
function BindTeamTaskInfo(msg) {
    var choiceBusSetText = "-请选择任务--";
    var defaultoptions = '<option code="qzz" value="-1" selected="selected">' + choiceBusSetText + '</option>';
    var options = "";
    jQuery("#slTeamTask").html(defaultoptions);
    if (msg.dataList != null) {
        if (msg.dataList.length > 0) {
            jQuery(msg.dataList).each(function (i, item) {
                options += '<option code="' + item.InputCode + '" value="' + item.ID_Team + '">' + item.TeamName + '</option>';
            });
            if (options != "") {
                options = defaultoptions + options;
            }
            else {
                options = defaultoptions;
            }
        }
    }
    jQuery("#slTeamTask").html(options);
    jQuery("#slTeamTask").find("option[value='-1']").attr("selected", true);
    jQuery("#slTeamTask .select2-choice span").text(choiceBusSetText);
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的条件内，没有找到任何信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='14'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}

function GetTeamWorkLoad() {
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    //判断时间差 Begin xmhuang 2014-01-14
    if (!CheckTime(BeginExamDate, EndExamDate)) {
        return false;
    }    
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
                var newcontent = '';
                var templateContent = jQuery("#TeamWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        //                        newcontent += templateContent.replace(/@NotSetionFinishedNum/gi, item.NotSetionFinishedNum)
                        //                            .replace(/@SetionFinishedNum/gi, item.SetionFinishedNum)
                        //                            .replace(/@FinalFinishedNum/gi, item.FinalFinishedNum)
                        //                            .replace(/@CheckedNum/gi, item.CheckedNum)
                        //                            .replace(/@ReportPrintedNum/gi, item.ReportPrintedNum)
                        //                            .replace(/@InformedNum/gi, item.InformedNum)
                        //                            .replace(/@ReportReceiptedNum/gi, item.ReportReceiptedNum)
                        newcontent += templateContent.replace(/@TeamWorkLoadType/gi, item.TeamWorkLoadType)
                            .replace(/@TeamWorkLoadNum/gi, item.TeamWorkLoadNum)
                            ;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }

                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
                var index = 1;
                var newcontent = '';
                var curNotCompeleteFeeItemName = "";
                var maxLength = 50;
                var ExamState = "";
                var templateContent = jQuery("#SubScribDateWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@index/gi, index)
                            .replace(/@Male/gi, item.Male)
                            .replace(/@FeMale/gi, item.FeMale)
                            .replace(/@SumGender/gi, item.SumGender)
                            ;
                        index++;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }

                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
                var index = 1;
                var newcontent = '';
                var curNotCompeleteFeeItemName = "";
                var maxLength = 50;
                var ExamState = "";
                var templateContent = jQuery("#AgeWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@index/gi, index)
                            .replace(/@AgeName/gi, item.AgeName)
                            .replace(/@Male/gi, item.Male)
                            .replace(/@FeMale/gi, item.FeMale)
                            .replace(/@SumGender/gi, item.SumGender)
                            .replace(/@MPer/gi, item.MalePer)
                            .replace(/@FPer/gi, item.FeMalePer)
                            .replace(/@SumPer/gi, item.SumPer)

                            ;
                        index++;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }

                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
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

    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
                var index = 1;
                var newcontent = '';
                var curNotCompeleteFeeItemName = "";
                var maxLength = 50;
                var ExamState = "";
                var templateContent = jQuery("#ConclusionWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {

                        newcontent += templateContent.replace(/@TeamConclusionName/gi, item.TeamConclusionName)
                            .replace(/@index/gi, index)
                            .replace(/@CheckOutMale/gi, item.CheckOutMale)
                            .replace(/@CheckOutFMale/gi, item.CheckOutFMale)
                            .replace(/@SumCheckOutCount/gi, item.SumCheckOutCount)
                            .replace(/@MaleCheckOutPer/gi, item.CheckOutMalePer)
                            .replace(/@CheckOFMalePer/gi, item.CheckOutFMalePer)
                            .replace(/@SumPer/gi, item.SumPer)
                            ;
                        index++;

                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }

                jQuery(msg.dataList1).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
                var index = 1;
                var newcontent = '';
                var curNotCompeleteFeeItemName = "";
                var maxLength = 50;
                var ExamState = "";
                var templateContent = jQuery("#CustFeeWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@index/gi, index)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@FeeName/gi, item.FeeName)
                            .replace(/@Male/gi, item.MaleCustFee)
                            .replace(/@FeMale/gi, item.FeMaleCustFee)
                            .replace(/@SumGender/gi, item.SumGenderCustFee)
                            .replace(/@MPer/gi, item.MalePerCustFee)
                            .replace(/@FPer/gi, item.FeMalePerCustFee)
                            .replace(/@SumPer/gi, item.SumPerCustFee)
                            ;
                        index++;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }

                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });

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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
                var index = 1;
                var newcontent = '';
                var curNotCompeleteFeeItemName = "";
                var maxLength = 50;
                var ExamState = "";
                var templateContent = "";
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (jQuery("#ExamType" + item.ExamType)[0] != undefined) {
                        templateContent = jQuery("#ExamType" + item.ExamType)[0].outerHTML;
                        if (templateContent != null) {
                            newcontent += templateContent.replace(/@ExamType/gi, item.ExamType)
                            .replace(/@Male/gi, item.Male)
                            .replace(/@FMale/gi, item.FMale)
                            .replace(/@SumGender/gi, item.SumGender)
                            .replace(/@MPer/gi, item.MealePer)
                            .replace(/@FPer/gi, item.FMalePer)
                            .replace(/@SumPer/gi, item.SumPer)
                            ;
                            index++;
                        }
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }

                jQuery(msg.dataList2).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
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
    jQuery('#Searchresult').html("");
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //获取被查询医生
    var TeamName = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].text; //获取被查询医生
    var ID_TeamTask = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].value; //获取被查询医生
    var TeamTaskName = document.getElementById("slTeamTask").options[document.getElementById("slTeamTask").selectedIndex].text; //获取被查询医生
    ID_Team = encodeURIComponent(ID_Team);    //编码被查询医生
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的团体!");
        return false;
    }
    if (ID_TeamTask == -1) {
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
            return false;
        }
        var maxLength = 50;
        var dataLength = pagerData.length;
        var flag = jQuery.trim(jQuery('#flag').val());
        var curPageSize = pagePagination.items_per_page;
        var optInit = getOptionsFromForm();                   //获取分页配置参数
        jQuery("#Pagination").show();
        if (tempOperPageCount == 0) {
            jQuery("#Pagination").pagination(dataLength, optInit);
        }
        else if (tempOldtotalCount != dataLength) {
            jQuery("#Pagination").pagination(dataLength, optInit);
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
            templateContent = jQuery("#PositiveSummaryWorkLoadTemplate tbody").html();
        }
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
                            .replace(/@Note/gi, curNote)
                            ;
                    } //完成情况 End
                    else if (dataFlag == "GetAllConclusionWorkLoad") {
                        newcontent += templateContent.replace(/@TeamConclusionName/gi, item.TeamConclusionName)
                            .replace(/@index/gi, rowNum + c + 1)
                            .replace(/@ConclusionName/gi, item.ConclusionName)
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
        }
        else {
            ResetSearchInfo("");
        }
    }
}
/*通用客户端分页脚本 xmhuang 2014-01-13  End*/