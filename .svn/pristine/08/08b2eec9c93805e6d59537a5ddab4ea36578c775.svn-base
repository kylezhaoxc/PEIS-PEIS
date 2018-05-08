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
    var flag = jQuery.trim(jQuery('#flag').val()); //flag:0：病症级别查询 1：病症级别统计
    if (flag == 1) {
        jQuery("#divInformed").hide();
    }
    else if (flag == 0) {
        jQuery("#divInformed").show();
    }
});

/// <summary>
/// 发票查询报表
/// 通过指定发票号、体检号查询发票统计信息
/// </summary>
function GetDiseaseLevelWorkLoad() {
    jQuery('#Searchresult').html("");
    var flag = jQuery.trim(jQuery('#flag').val()); //flag:0：病症级别查询 1：病症级别统计
    var MinLevel = jQuery.trim(jQuery('#txtMinLevel').val());
    var MaxLevel = jQuery.trim(jQuery('#txtMiaxLevel').val());
    if (MinLevel == "" && MaxLevel == "") {
        ShowSystemDialog("对不起，请您输入病症级别");
        jQuery('#txtMinLevel').focus();
        return false;
    }
    if (parseFloat(MaxLevel) < parseFloat(MinLevel)) {
        ShowSystemDialog("对不起，起始值不得大于终止值");
        jQuery('#txtMinLevel').focus();
        return false;
    }
    MinLevel = MinLevel == "" ? 0 : MinLevel;
    MaxLevel = MaxLevel == "" ? 0 : MaxLevel;
    if (parseFloat(MinLevel) > parseFloat(MaxLevel)) {

        return false;
    }
    var IsInformed = document.getElementById("slInformed").options[document.getElementById("slInformed").selectedIndex].value; //通知状态
    if (flag == 1) {
        IsInformed = -1; //设置为-1即在查询条件中不包含此项
    }
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
            IsInformed: IsInformed,
            MinLevel: MinLevel,
            MaxLevel: MaxLevel,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetDiseaseLevelWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                var rowNum = 1;
                var newcontent = '';
                var templateContent = jQuery("#DiseaseLevelWorkLoadTemplate tbody").html();
                if (templateContent == undefined) { return false; }
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        if (item.ResultSummary.length > 30) {
                            item.ResultSummary = item.ResultSummary.substr(0, 30) + "...";
                        }
                        newcontent += templateContent.replace(/@Index/gi, rowNum)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@FeeItemName/gi, item.FeeItemName)
                            .replace(/@ExamItemName/gi, item.ExamItemName)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@ResultSummary/gi, item.ResultSummary)
                            .replace(/@ExamItemSummaryDate/gi, item.ExamItemSummaryDate)
                            .replace(/@DiseaseLevel/gi, item.DiseaseLevel)
                            .replace(/@SummaryDoctorName/gi, item.SummaryDoctorName)
                            .replace(/@Is_Informed/gi, item.Is_Informed)
                            .replace(/@Informer/gi, item.Informer)
                            .replace(/@InformedDate/gi, item.InformedDate)
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