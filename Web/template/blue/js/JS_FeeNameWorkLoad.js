/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("[name='Searchresult']").attr("title", "点击关闭详细");
    jQuery("#slFeeName").select2();
    ResetSearchInfo("尚无任何数据...");
});

/// <summary>
/// 显示或者折叠详细信息
/// </summary>
function ShowMe(obj) {
    var name = jQuery(obj).attr("name");
    var nextObj = jQuery("[name='Searchresult_detail_" + name + "']");
    jQuery(nextObj).each(function () {
        if (jQuery(this).is(":visible")) {
            jQuery(this).hide();
            jQuery(obj).attr("title", "点击查看详细");
        }
        else {
            jQuery(this).show("slow");
            jQuery(obj).attr("title", "点击关闭详细");
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
    jQuery("#Pagination").hide(); //隐藏分页控件
}

/// <summary>
/// 获取收费项目工作量
/// </summary>
function GetCustFeeWorkLoad() {
    jQuery('#Searchresult').html("");
    var ID_Fee = document.getElementById("slFeeName").options[document.getElementById("slFeeName").selectedIndex].value; //获取被查询医生
    var FeeName = document.getElementById("slFeeName").options[document.getElementById("slFeeName").selectedIndex].text; //获取被查询医生
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
            ID_Fee: ID_Fee,
            FeeName: FeeName,
            InputCode: InputCode,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetCustFeeWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {

            if (msg != undefined) {

                var newContent = '';
                var CustFeetemplateContent = jQuery("#SearchresultTeamplate").html();
                var CustFeeDetailtemplateContent = jQuery("#Searchresult_detail").html();
                if (msg.dataList0.length > 0) {
                    if (CustFeetemplateContent == undefined || CustFeeDetailtemplateContent == undefined) { return false; }
                    //由于是单表返回，返回格式为父－子－父－子...父-子形式，其中IsParent=1为父 IsParent=0为其对应的子项
                    jQuery(msg.dataList0).each(function (i, item) {
                        if (CustFeetemplateContent != null && CustFeeDetailtemplateContent != null) {
                            if (item.IsParent == 1) {
                                newContent += CustFeetemplateContent.replace(/@FeeItemName/gi, item.FeeItemName)
                                    .replace(/@FeeItemNum/gi, item.FeeItemNum)
                                    .replace(/@ID_Fee/gi, item.ID_Fee)
                                     .replace(/@RegisterName/gi, item.RegisterName)
                                      .replace(/@SumFactPrice/gi, item.SumFactPrice)
                                     .replace(/@OrderIndex/gi, item.OrderIndex)
                                    ;
                            }
                            //                            else if (item.IsParent == 0) //子项
                            //                            {
                            //                                newContent += CustFeeDetailtemplateContent.replace(/@CustomerName/gi, item.CustomerName)
                            //                                                                .replace(/@ID_Customer/gi, item.ID_Customer)
                            //                                                                .replace(/@ExamState/gi, item.ExamState)
                            //                                                                .replace(/@ID_Fee/gi, item.ID_Fee)
                            //                                                                .replace(/@TeamName/gi, item.TeamName)
                            //                                                                .replace(/@OrderIndex/gi, item.OrderIndex)
                            //                                                                ;
                            //                            }
                        }
                    });
                    if (newContent != '') {
                        jQuery('#Searchresult').html(newContent); //将值填充到Searchresult中显示
                    } else {
                        ResetSearchInfo("");
                    }
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
        msgInfo = "在您查询的时间段内，没有找到任何信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='11'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}