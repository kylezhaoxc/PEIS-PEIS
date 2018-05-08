/*
*分科明细查询
*用户必须输入时间区间
*/

/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    ResetSearchInfo("尚无任何数据...");
    if (jQuery("#slSection") != null) {
        jQuery("#slSection").select2();
    }
});

/// <summary>
/// 发票查询报表
/// 通过指定发票号、体检号查询发票统计信息
/// </summary>
function GetSectionSearchWorkLoad() {
    jQuery('#Searchresult').html("");
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
                var rowNum = 1;
                var newcontent = '';
                var templateContent = jQuery("#SectionSearchWorkLoadTemplate tbody").html();
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        if (item.SectionSummary.length > 30) {
                            item.SectionSummary = item.SectionSummary.substr(0, 30) + "...";
                        }
                        newcontent += templateContent.replace(/@Index/gi, rowNum)
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
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='12'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
}