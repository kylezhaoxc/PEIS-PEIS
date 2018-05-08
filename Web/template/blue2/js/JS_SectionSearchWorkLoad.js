/*
*分科明细查询
*用户必须输入时间区间
*/
var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var pagerData = null; //记录当前分页数据源
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
        var templateContent = jQuery("#SectionSearchWorkLoadTemplate tbody").html();
        var oldSectionSummary = "";
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    oldSectionSummary = item.SectionSummary;
                    if (item.SectionSummary.length > 30) {
                        item.SectionSummary = item.SectionSummary.substr(0, 30) + "...";
                    }
                    newcontent += templateContent.replace(/@Index/gi, rowNum + c + 1)
                                            .replace(/@ID_Customer/gi, item.ID_Customer)
                                            .replace(/@CustomerName/gi, item.CustomerName)
                                            .replace(/@GenderName/gi, item.GenderName)
                                            .replace(/@Age/gi, item.Age)
                                            .replace(/@SectionName/gi, item.SectionName)
                                            .replace(/@SectionSummaryDate/gi, item.SectionSummaryDate)
                                            .replace(/@SummaryDoctorName/gi, item.SummaryDoctorName)
                                            .replace(/@TypistName/gi, item.TypistName)
                                            .replace(/@TypistDate/gi, item.TypistDate)
                                            .replace(/@CheckDate/gi, item.CheckDate)
                                            .replace(/@SectionSummary/gi, item.SectionSummary)
                                            .replace(/@SummaryTitle/gi, oldSectionSummary);
                }
            }
        }

        if (newcontent != '') {
            jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            SetTableRowStyle();
        } else {
            ResetSearchInfo("");
        }
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
        //设置固定表头 xmhuang 2014-04-02
        //$('#tbDiseaseLevel').tablefix({ height: 600, width: 900, fixRows: 1, fixCols: 8 });
    }
}
/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    ResetSearchInfo("");
    //    if (jQuery("#slSection") != null) {
    //        jQuery("#slSection").select2();
    //    }
});

/// <summary>
/// 发票查询报表
/// 通过指定发票号、体检号查询发票统计信息
/// </summary>
function GetSectionSearchWorkLoad() {
    ResetSearchInfo("正在查询数据,请稍后...");
    var ID_Section = document.getElementById("slSection").options[document.getElementById("slSection").selectedIndex].value; //获取科室ID
    var BeginExamDate = jQuery.trim(jQuery('#BeginExamDate').val());
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery.trim(jQuery('#EndExamDate').val());
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    //判断时间差 Begin xmhuang 2014-01-14
    if (!CheckTime(BeginExamDate, EndExamDate)) {
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Section: ID_Section,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetSectionSearchWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {

                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList1).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;
                //                 var rowNum = 1;
                //                var newcontent = '';
                //                var templateContent = jQuery("#SectionSearchWorkLoadTemplate tbody").html();
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        if (item.SectionSummary.length > 30) {
                //                            item.SectionSummary = item.SectionSummary.substr(0, 30) + "...";
                //                        }
                //                        newcontent += templateContent.replace(/@Index/gi, rowNum)
                //                            .replace(/@ID_Customer/gi, item.ID_Customer)
                //                            .replace(/@CustomerName/gi, item.CustomerName)
                //                            .replace(/@GenderName/gi, item.GenderName)
                //                            .replace(/@Age/gi, item.Age)
                //                            .replace(/@SectionName/gi, item.SectionName)
                //                            .replace(/@SectionSummaryDate/gi, item.SectionSummaryDate)
                //                            .replace(/@SummaryDoctorName/gi, item.SummaryDoctorName)
                //                            .replace(/@TypistName/gi, item.TypistName)
                //                            .replace(/@TypistDate/gi, item.TypistDate)
                //                            .replace(/@CheckDate/gi, item.CheckDate)
                //                            .replace(/@SectionSummary/gi, item.SectionSummary)
                //                            ;
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

/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的范围内，没有找到任何信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
}

function ShowMeTitle(obj) {
    if (jQuery(obj).attr("title") != "") {
        var title = jQuery(obj).find(" label[name='lblTitle']").text();
        jQuery(obj).attr("title", title);
    }
}