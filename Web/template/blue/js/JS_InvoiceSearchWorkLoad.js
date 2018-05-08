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
function GetInvoiceSearchWorkLoad() {
    jQuery('#Searchresult').html("");
    var Invoice = jQuery.trim(jQuery('#txtInvoice').val()); //客户发票号
    var ID_Customer = jQuery.trim(jQuery('#txtID_Customer').val()); //客户发票号
    //验证是否选择录入了至少一个选项
    if (Invoice == "" && ID_Customer == "") {
        ShowSystemDialog("对不起，请您至少输入一个查询条件!");
        jQuery('#txtInvoice').focus();
        return false;
    }
    //验证体检号是否满足要求
    else if (Invoice == "") {
        if (!isCustomerExamNo(ID_Customer)) {
            ShowSystemDialog("对不起，请您输入正确的体检号!");
            jQuery('#txtID_Customer').focus();
            return false;
        }
    }

    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Customer: ID_Customer,
            Invoice: Invoice,
            action: 'GetInvoiceSearchWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                var rowNum = 1;
                var newcontent = '';
                var templateContent = jQuery("#InvoiceSearchWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        if (item.Invoice != "") {
                            item.Invoice = item.Invoice.substr(1, item.Invoice.length);
                        }
                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@ExamTypeName/gi, item.ExamTypeName)
                            .replace(/@ExamPlaceName/gi, item.ExamPlaceName)
                            .replace(/@GuideNurse/gi, item.GuideNurse)
                            .replace(/@FeeWayName/gi, item.FeeWayName)
                            .replace(/@FeeCharger/gi, item.FeeCharger)
                            .replace(/@Invoice/gi, item.Invoice)
                            .replace(/@FeeChargeDate/gi, item.FeeChargeDate)
                            ;
                        rowNum++;
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