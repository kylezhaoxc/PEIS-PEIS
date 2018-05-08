/*
*发票查询报表
*用户必须输入发票号或者体检号进行查询*
*/

/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    ResetSearchInfo("尚无任何数据...");
});
/// <summary>
/// 发票查询报表
/// 通过指定发票号、体检号查询发票统计信息
/// </summary>
function GetDiscountSearchWorkLoad() {
    jQuery('#Searchresult').html("");
    var flag = jQuery.trim(jQuery('#flag').val());
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
            flag: flag,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetDiscountSearchWorkLoad'
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
            //            return false;
            //            if (msg != undefined) {
            //                var rowNum = 1;
            //                var newcontent = '';
            //                var templateContent = jQuery("#DiscountSearchWorkLoadTemplate" + flag + " tbody").html();
            //                if (templateContent == undefined) { return false; }
            //                jQuery(msg.dataList0).each(function (i, item) {
            //                    if (templateContent != null) {
            //                        newcontent += templateContent.replace(/@Index/gi, rowNum)
            //                            .replace(/@ID_Customer/gi, item.ID_Customer)
            //                            .replace(/@FeeItemName/gi, item.FeeItemName)
            //                            .replace(/@RegisterName/gi, item.RegisterName)
            //                            .replace(/@FeeChargeDate/gi, item.FeeChargeDate)
            //                            .replace(/@OriginalPrice/gi, item.OriginalPrice)
            //                            .replace(/@Discount/gi, item.Discount)
            //                            .replace(/@FactPrice/gi, item.FactPrice)
            //                            .replace(/@DisterName/gi, item.DiscounterName)
            //                            .replace(/@FeeWayName/gi, item.FeeWayName)
            //                            ;
            //                        rowNum++;
            //                    }
            //                });
            //                if (newcontent != '') {
            //                    //flag:0:折扣查询  1：记账明细查询
            //                    if (flag == 1) {
            //                        jQuery('#tdFeeWayName').show();
            //                    }
            //                    else {
            //                        jQuery('#tdFeeWayName').hide();
            //                    }
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

/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的范围内，没有找到任何信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='11'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
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
        var templateContent = jQuery("#DiscountSearchWorkLoadTemplate" + flag + " tbody").html();
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    newcontent += templateContent.replace(/@Index/gi, rowNum + c + 1)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@FeeItemName/gi, item.FeeItemName)
                            .replace(/@RegisterName/gi, item.RegisterName)
                            .replace(/@FeeChargeDate/gi, item.FeeChargeDate)
                            .replace(/@OriginalPrice/gi, item.OriginalPrice)
                            .replace(/@Discount/gi, item.Discount)
                            .replace(/@FactPrice/gi, item.FactPrice)
                            .replace(/@DisterName/gi, item.DiscounterName)
                            .replace(/@FeeWayName/gi, item.FeeWayName)
                            ;
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