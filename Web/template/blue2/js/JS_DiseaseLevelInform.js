﻿
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


function QueryPagesData(pageIndex) {
    ResetSearchInfo("正在查询，请稍候...");
    var flag = jQuery.trim(jQuery('#flag').val()); 
    var MinLevel = jQuery.trim(jQuery('#txtMinLevel').val());
    var MaxLevel = jQuery.trim(jQuery('#txtMiaxLevel').val());

    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
    if (customerid != "" && !isCustomerExamNo(customerid)) {

        ShowSystemDialog("对不起，您输入的体检号不正确，请重新输入");
        jQuery('#txtCustomerID').focus();
        return false;
    }

    if (!isCustomerExamNo(customerid)) {

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

    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxCustDiseaseLevel.aspx",
        data: {
            pageIndex: pageIndex,
            pageSize: pagePagination.items_per_page,
            flag: flag,
            CustomerID: customerid,
            IsInformed: IsInformed,
            MinLevel: MinLevel,
            MaxLevel: MaxLevel,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'QueryCustDiseaseLevelList'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {

                tempOperPageCount = 0;

                // 检查Ajax返回数据的状态等  20140430 by wtang 
                msg = CheckAjaxReturnDataInfo(msg);

                if (msg == null || msg == "") {
                    return;
                }

                if (parseInt(msg.totalCount) > 0) {

                    jQuery("#Pagination").show();
                    if (tempOperPageCount == 0) {
                        jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                    }
                    else if (tempOldtotalCount != msg.totalCount) {
                        jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                    }
                    tempOldtotalCount = msg.totalCount;


                    var newcontent = "";
                    // 从模版中读取数据加载列表
                    var templateContent = jQuery('#DiseaseLevelListTemplate tbody').html();

                    var RowNum = 1;
                    if (pageIndex > 0) {
                        RowNum = optInit.items_per_page * (pageIndex) + 1;
                    }
                    jQuery(msg.dataList).each(function (i, item) {

                        if (templateContent == undefined) { return; }

                        newcontent +=
                             templateContent.replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@CustomerID/gi, item.ID_Customer)
                            .replace(/@DiseaseLevel/gi, item.DiseaseLevel + '级')
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)

                            ;

                        RowNum++;
                    });
                    if (newcontent != '') {
                        jQuery('#Searchresult').html(newcontent);

                        SetTableEvenOddRowStyle(); // 奇偶行背景
                    }
                } else {
                    jQuery('#Searchresult').html(TipsInfoTempletecontent.replace(/@TipsInfo/gi, "在您查询的时间段内，没有找到客户的体检信息！"));
                    jQuery("#Pagination").hide(); // 没有数据的时候，隐藏分页信息
                }
            }
            else {
                ResetSearchInfo("");
            }
        }
    });

}


function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的范围内，没有找到任何信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); 
}

//function ShowContent(obj) {
//    jQuery(obj).find("td label[name='showContent']").each(function () {
//        jQuery(this).parent.attr("title", jQuery(this).text());
//    });
//}