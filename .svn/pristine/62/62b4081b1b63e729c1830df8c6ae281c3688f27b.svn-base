
/// <summary>
///分页函数 XMHuang 2013-08-12 添加注释 
///pageIndex:当前页面ID
/// </summary>

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
            jQuery("#Pagination ul").pagination(dataLength, optInit);
        }
        else if (tempOldtotalCount != dataLength) {
            jQuery("#Pagination ul").pagination(dataLength, optInit);
        }
        tempOldtotalCount = dataLength;
        var item;
        var rowNum = curPageSize * pageIndex; //计算
        var newcontent = '';
        var templateContent = jQuery("#TemplateCustFeeDetail tbody").html();
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    newcontent += templateContent.replace(/@FeeItemNum/gi, rowNum + c + 1)
                                .replace(/@FeeItemName/gi, item.FeeItemName)
                                .replace(/@FeeChargeStaute/gi, item.FeeChargeStaute)
                                .replace(/@FactPrice/gi, parseFloat(item.FactPrice).toFixed(2))
                                .replace(/@SectionName/gi, item.SectionName)
                }
            }
        }

        if (newcontent != '') {
            jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            SetTableRowStyle();
        } else {
            ResetSearchInfo("");
        }
        //设置固定表头 xmhuang 2014-04-02
        //$('#tbDiseaseLevel').tablefix({ height: 600, width: 900, fixRows: 1, fixCols: 8 });
       
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
    }
    else {
        jQuery("#Pagination").hide();
    }
}
/// <summary>
///页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    ResetAllInfo();
});
/// <summary>
/// 窗体按键事件，如果在体检号文本框中回车则自动触发检索功能
/// </summary>
function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13)//回车事件
    {
        if (id == "txtCustomerID")//如果光标在身份证号内，则自动通过体检身份证号检索信息
        {
            jQuery("#btnSearchCustFeeDetail").click(); return;
        }
    }
    return false;
}
/// <summary>
/// 点击重发按钮
/// </summary>
function ClickBtnSearchCustFeeDetail() {
    ResetAllInfo();
    var ID_Customer = jQuery.trim(jQuery("#txtCustomerID").val());            // 体检号
    // 查询客户的基本信息，并显示
    if (!isCustomerExamNo(ID_Customer)) {
        ShowSystemDialog("体检号格式错误，请检核对后重新输入！");
        jQuery('#txtCustomerID').select();
        jQuery('#txtCustomerID').focus();
        return false;
    }
    SearchCustomerBaseInfo(0, 1); //原型：SearchCustomerBaseInfo(IsShowMsg, IsLoadCustomerInfo)
    SearchCustFeeDetail();
}
/// <summary>
/// 获取收费项目明细信息
/// </summary>
function SearchCustFeeDetail() {
    var ID_Customer = jQuery.trim(jQuery("#txtCustomerID").val());            // 体检号
    if (!isCustomerExamNo(ID_Customer)) {
        ShowSystemDialog("体检号格式错误，请检核对后重新输入！");
        jQuery('#txtCustomerID').select();
        jQuery('#txtCustomerID').focus();
        return;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Customer: ID_Customer,
            action: 'GetCustFeeDetail'
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
                //                var newContent = '';
                //                var CustFeeDetailContent = jQuery("#TemplateCustFeeDetail tbody").html();
                //                if (msg.dataList0.length > 0) {
                //                    if (CustFeeDetailContent == null || CustFeeDetailContent == undefined) { return false; }
                //                    jQuery(msg.dataList0).each(function (i, item) {
                //                        newContent += CustFeeDetailContent.replace(/@FeeItemNum/gi, item.FeeItemNum)
                //                                    .replace(/@FeeItemName/gi, item.FeeItemName)
                //                                    .replace(/@FeeChargeStaute/gi, item.FeeChargeStaute)
                //                                    .replace(/@FactPrice/gi, item.FactPrice)
                //                                     .replace(/@SectionName/gi, item.SectionName)
                //                                    ;
                //                    });
                //                    if (newContent != '') {
                //                        jQuery('#Searchresult').html(newContent); //将值填充到Searchresult中显示
                //                    } else {
                //                        ResetSearchInfo("");
                //                    }
                //                }

                //                jQuery(msg.dataList1).each(function (i, item) {
                //                    jQuery("#loadExcel").attr("href", item.FilePath);
                //                });

                //                if (jQuery.trim(jQuery('#lblAge').text()) != "") {
                //                    jQuery('#lblAge').text(jQuery('#lblAge').text() + " 岁");
                //                }
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}

/// <summary>
/// 更新重发的标记 
/// </summary>
function PrintCustFeeDetailReport() {

    var ID_Customer = jQuery.trim(jQuery("#txtCustomerID").val());            // 体检号

    if (!isCustomerExamNo(ID_Customer)) {

        ShowSystemDialog("体检号格式错误，请检核对后重新输入！");
        jQuery('#txtCustomerID').select();
        jQuery('#txtCustomerID').select();
        jQuery('#txtCustomerID').focus();
        return;
    }
    //这里执行调用报告功能
    FastReport.GenerateCustFeeDeatil(ID_Customer, "CustFeeDetail.frx");
}
var defalutImagSrc = "/template/blue/images/icons/nohead.gif"; //默认头像
function ResetAllInfo() {
    jQuery("#lblID_Customer").text("");
    jQuery("#lblCustomerName").text("");
    jQuery("#lblSex").text("");
    jQuery("#lblAge").text("");
    jQuery("#lblIDCard").text("");
    jQuery("#lblTel").text("");
    jQuery("#lblRegisterDate").text("");
    jQuery("#lblTeamName").text("");
    jQuery("#lblMarriedName").text("");
    jQuery("#lblExamType").text("");
    jQuery("#HeadImg").attr("src", defalutImagSrc);

    ResetSearchInfo("");
    jQuery('#txtCustomerID').select();
    jQuery('#txtCustomerID').focus();
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "没有找到任何信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
}