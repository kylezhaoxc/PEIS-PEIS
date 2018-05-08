/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("#slSection").select2();
    ResetSearchInfo("尚无任何数据...");
});


/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的时间段内，没有找到任何信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='3'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}

/// <summary>
/// 获取收费项目工作量
/// </summary>
function GetCustomerWorkLoad() {
    jQuery('#Searchresult').html("");
    var ID_Section = document.getElementById("slSection").options[document.getElementById("slSection").selectedIndex].value; //获取科室ID
    if (ID_Section == -1) {
        ShowSystemDialog("请您选择科室!");
        return false;
    }
    var BeginExamDate = jQuery.trim(jQuery('#BeginExamDate').val());
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery.trim(jQuery('#EndExamDate').val());
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    //判断时间差 Begin xmhuang 2014-01-14
    if (!CheckTime(BeginExamDate, EndExamDate)) {
        return false;
    }    
    var InputCode = jQuery.trim(jQuery("#txtInputCode").val());
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Section: ID_Section,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetSectionOncustFeeWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            pagerData = msg.dataList0;
            tempOperPageCount = 0;
            QueryPagesData(0);
            jQuery(msg.dataList1).each(function (i, item) {
                jQuery("#loadExcel").attr("href", item.FilePath);
            });
            //            if (msg != undefined) {
            //                var index = 1;
            //                var newContent = '';
            //                var CustFeetemplateContent = jQuery("#SearchresultTeamplate").html();
            //                if (msg.dataList0.length > 0) {
            //                    if (CustFeetemplateContent == undefined) { return false; }
            //                    //由于是单表返回，返回格式为父－子－父－子...父-子形式，其中IsParent=1为父 IsParent=0为其对应的子项
            //                    jQuery(msg.dataList0).each(function (i, item) {
            //                        if (CustFeetemplateContent != null) {
            //                            if (item.ExamState == 0) {
            //                                item.ExamState = "在线库";
            //                            }
            //                            else if (item.ExamState == 1) {
            //                                item.ExamState = "离线库";
            //                            }
            //                            else if (item.ExamState > 1) {
            //                                item.ExamState = "归档库" + (item.ExamState - 1);
            //                            }
            //                            newContent += CustFeetemplateContent.replace(/@ID_Customer/gi, item.ID_Customer)
            //                                    .replace(/@index/gi, index)
            //                                    .replace(/@FeeItemName/gi, item.FeeItemName)
            //                                    .replace(/@SectionName/gi, item.SectionName)
            //                                    .replace(/@ExamDoctorName/gi, item.ExamDoctorName)
            //                                    .replace(/@ExamDate/gi, item.ExamDate)
            //                                    ;
            //                            index++;
            //                        }
            //                    });
            //                    if (newContent != '') {
            //                        jQuery('#Searchresult').html(newContent); //将值填充到Searchresult中显示
            //                    } else {
            //                        ResetSearchInfo("");
            //                    }
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

/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的时间段内，没有找到任何信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='25'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}

function LoadCustomerReport(title, ID_Customer) {

}
/*通用客户端分页脚本 xmhuang 2014-01-13  Begin*/
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );
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
            jQuery("#Pagination").pagination(dataLength, optInit);
        }
        else if (tempOldtotalCount != dataLength) {
            jQuery("#Pagination").pagination(dataLength, optInit);
        }
        tempOldtotalCount = dataLength;
        var item;
        var rowNum = curPageSize * pageIndex; //计算
        var newcontent = '';
        var templateContent = jQuery("#SearchresultTeamplate").html();
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    if (item.ExamState == 0) {
                        item.ExamState = "在线库";
                    }
                    else if (item.ExamState == 1) {
                        item.ExamState = "离线库";
                    }
                    else if (item.ExamState > 1) {
                        item.ExamState = "归档库" + (item.ExamState - 1);
                    }
                    newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                                    .replace(/@index/gi, rowNum + c + 1)
                                    .replace(/@FeeItemName/gi, item.FeeItemName)
                                    .replace(/@SectionName/gi, item.SectionName)
                                    .replace(/@ExamDoctorName/gi, item.ExamDoctorName)
                                    .replace(/@ExamDate/gi, item.ExamDate)
                                    ;
                }
            }
        }
        if (newcontent != '') {
            jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
        }
        else {
            ResetSearchInfo("");
        }
    }
}
/*通用客户端分页脚本 xmhuang 2014-01-13  End*/