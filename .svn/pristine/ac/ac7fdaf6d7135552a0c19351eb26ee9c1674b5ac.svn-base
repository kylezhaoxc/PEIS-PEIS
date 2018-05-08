/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("#slTeam").select2();
    ResetSearchInfo("尚无任何数据...");
});

/// <summary>
/// 点击仅个人
/// </summary>
function ChangeOnlyPerson() {
    var OnlyPerson = jQuery("input[name='chbOnlyPerson']:checked").val();   // 仅显示个人的
    if (OnlyPerson == 1) {
        jQuery("#chbOnlyTeam").attr("checked", false);
    }
}

/// <summary>
/// 点击仅团体
/// </summary>
function ChangeOnlyTeam() {
    var OnlyTeam = jQuery("input[name='chbOnlyTeam']:checked").val();   // 仅显示团体的
    if (OnlyTeam == 1) {
        jQuery("#chbOnlyPerson").attr("checked", false);
    }
}
/// <summary>
/// 获取发票统计信息
/// </summary>
function GetInvoiceWorkLoad() {
    jQuery('#Searchresult').html("");
    var IsHasInvoice = 1; //是否查询只包含发票信息的
    var CustomerName = jQuery.trim(jQuery('#txtCustomerName').val()); //客户名称
    var BeginExamDate = jQuery('#BeginExamDate').val(); //体检开始日期
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();    //体检结束日期
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    //判断时间差 Begin xmhuang 2014-01-14
    if (!CheckTime(BeginExamDate, EndExamDate)) {
        return false;
    }    
    var OnlyPerson = jQuery("input[name='chbOnlyPerson']:checked").val();   // 仅显示个人的
    var OnlyTeam = jQuery("input[name='chbOnlyTeam']:checked").val();   // 仅显示团体的
    var CardNo = jQuery.trim(jQuery('#txtSFZ').val()); //体检号或者身份证号码
    var ISCustomerExamNo = 0;
    if (isCustomerExamNo(CardNo)) {
        ISCustomerExamNo = 1;
    }
    var IsTeam = -1; //-1：默认显示全部 0：个人 1：团体
    if (OnlyPerson == 1) {
        IsTeam = 0;
    }
    else if (OnlyTeam == 1) {
        IsTeam = 1;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ISCustomerExamNo: ISCustomerExamNo,
            IsHasInvoice: IsHasInvoice,
            CustomerName: CustomerName,
            IsTeam: IsTeam,
            CardNo: CardNo,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetInvoiceWorkLoad'
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
            //                var rowNum = 1;
            //                var newcontent = '';
            //                var templateContent = jQuery("#InvoiceWorkLoadTemplate tbody").html();
            //                if (templateContent == undefined) { return false; }
            //                jQuery(msg.dataList0).each(function (i, item) {
            //                    if (templateContent != null) {
            //                        if (item.Invoice != "") {
            //                            item.Invoice = item.Invoice.substr(1, item.Invoice.length);
            //                        }
            //                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
            //                            .replace(/@SubScribDate/gi, item.SubScribDate)
            //                            .replace(/@CustomerName/gi, item.CustomerName)
            //                            .replace(/@GenderName/gi, item.GenderName)
            //                            .replace(/@Age/gi, item.Age)
            //                            .replace(/@IDCard/gi, item.IDCard)
            //                            .replace(/@MarriageName/gi, item.MarriageName)
            //                            .replace(/@MobileNo/gi, item.MobileNo)
            //                            .replace(/@TeamName/gi, item.TeamName)
            //                            .replace(/@RowNum/gi, rowNum)
            //                            .replace(/@Invoice/gi, item.Invoice)
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
        var templateContent = jQuery("#InvoiceWorkLoadTemplate tbody").html();
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    if (item.Invoice != "") {
                        item.Invoice = item.Invoice.substr(1, item.Invoice.length);
                    }
                    newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@SubScribDate/gi, item.SubScribDate)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@Age/gi, item.Age)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@MobileNo/gi, item.MobileNo)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@RowNum/gi, rowNum + c + 1)
                            .replace(/@Invoice/gi, item.Invoice)
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