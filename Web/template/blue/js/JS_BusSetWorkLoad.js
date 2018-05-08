/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
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
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetBusSetWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                var index = 1;
                var newContent = '';
                var CustFeetemplateContent = jQuery("#SearchresultTeamplate").html();
                if (msg.dataList0.length > 0) {
                    if (CustFeetemplateContent == undefined) { return false; }
                    //由于是单表返回，返回格式为父－子－父－子...父-子形式，其中IsParent=1为父 IsParent=0为其对应的子项
                    jQuery(msg.dataList0).each(function (i, item) {
                        if (CustFeetemplateContent != null) {
                            if (item.ExamState == 0) {
                                item.ExamState = "在线库";
                            }
                            else if (item.ExamState == 1) {
                                item.ExamState = "离线库";
                            }
                            else if (item.ExamState > 1) {
                                item.ExamState = "归档库" + (item.ExamState - 1);
                            }
                            newContent += CustFeetemplateContent.replace(/@ID_ArcCustomer/gi, item.ID_ArcCustomer)
                                    .replace(/@index/gi, index)
                                    .replace(/@PEPackageName/gi, item.PEPackageName)
                                    .replace(/@SetCount/gi, item.SetCount)
                                    ;
                            index++;
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
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='25'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}

function LoadCustomerReport(title, ID_Customer) {

}