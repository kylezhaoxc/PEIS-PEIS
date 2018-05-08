
var tempOperPageCount = 0;
var tempOldtotalCount = 0; 
var pagerData = null;
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
        var templateContent = jQuery("#DiseaseLevelWorkLoadTemplate tbody").html();
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
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@FeeItemName/gi, item.FeeItemName)

                            .replace(/@SectionName/gi, item.SectionName)


                            .replace(/@DiseaseLevel/gi, item.DiseaseLevel + '级')

                            .replace(/@Is_Informed/gi, item.Is_Informed)
                            .replace(/@Informer/gi, item.Informer)

                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)

                            .replace(/@ExamItemName/gi, item.ExamItemName)
                            .replace(/@ResultSummary/gi, item.ResultSummary)
                            .replace(/@SummaryDoctorName/gi, item.SummaryDoctorName)
                            .replace(/@InformedDate/gi, item.InformedDate)
                            .replace(/@ExamItemSummaryDate/gi, item.ExamItemSummaryDate)
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
    ResetSearchInfo("正在查询，请稍候...");
    var flag = jQuery.trim(jQuery('#flag').val()); //flag:0：病症级别查询 1：病症级别统计
    var MinLevel = jQuery.trim(jQuery('#txtMinLevel').val());
    var MaxLevel = jQuery.trim(jQuery('#txtMiaxLevel').val());

    if (MinLevel == "") {
        ShowSystemDialog("对不起，请您输入最低病症级别");
        jQuery('#txtMinLevel').focus();
        return false;
    }
    if (MaxLevel == "") {
        ShowSystemDialog("对不起，请您输入最高病症级别");
        jQuery('#txtMiaxLevel').focus();
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

                pagerData = msg.dataList0;
                tempOperPageCount = 0;
                QueryPagesData(0);
                jQuery(msg.dataList1).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
                return false;

                //                var rowNum = 1;
                //                var newcontent = '';
                //                var templateContent = jQuery("#DiseaseLevelWorkLoadTemplate tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        if (item.ResultSummary.length > 30) {
                //                            item.ResultSummary = item.ResultSummary.substr(0, 30) + "...";
                //                        }
                //                        newcontent += templateContent.replace(/@Index/gi, rowNum)
                //                            .replace(/@ID_Customer/gi, item.ID_Customer)
                //                            .replace(/@SectionName/gi, item.SectionName)
                //                            .replace(/@FeeItemName/gi, item.FeeItemName)
                //                            .replace(/@ExamItemName/gi, item.ExamItemName)
                //                            .replace(/@SectionName/gi, item.SectionName)
                //                            .replace(/@ResultSummary/gi, item.ResultSummary)
                //                            .replace(/@ExamItemSummaryDate/gi, item.ExamItemSummaryDate)
                //                            .replace(/@DiseaseLevel/gi, item.DiseaseLevel + '级')
                //                            .replace(/@SummaryDoctorName/gi, item.SummaryDoctorName)
                //                            .replace(/@Is_Informed/gi, item.Is_Informed)
                //                            .replace(/@Informer/gi, item.Informer)
                //                            .replace(/@InformedDate/gi, item.InformedDate)
                //                            .replace(/@CustomerName/gi, item.CustomerName)
                //                            .replace(/@GenderName/gi, item.GenderName)
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

//function ShowContent(obj) {
//    jQuery(obj).find("td label[name='showContent']").each(function () {
//        jQuery(this).parent.attr("title", jQuery(this).text());
//    });
//}