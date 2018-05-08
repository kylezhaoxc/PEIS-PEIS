/*
*导检查询
*用户必须输入时间区间
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
function GetGuideNurseSearchWorkLoad() {
    jQuery('#Searchresult').html("");
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
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetGuideNurseSearchWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                var rowNum = 1;
                var newcontent = '';
                var templateContent = jQuery("#GuideNurseSearchWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@Index/gi, rowNum)
                            .replace(/@GuideNurse/gi, item.GuideNurse)
                            .replace(/@GuideNum/gi, item.GuideNum)
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