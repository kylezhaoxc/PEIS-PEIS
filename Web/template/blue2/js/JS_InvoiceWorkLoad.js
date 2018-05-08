
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
            FixedTable();
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
        var templateContent = jQuery("#InvoiceSearchWorkLoadTemplate tbody").html();
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    newcontent += templateContent.replace(/@RowNum/gi, rowNum + c + 1)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MobileNo/gi, item.MobileNo)
                            .replace(/@OperateDate/gi, item.OperateDate)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@ExamTypeName/gi, item.ExamTypeName)
                            .replace(/@ExamPlaceName/gi, item.ExamPlaceName)
                            .replace(/@GuideNurse/gi, item.GuideNurse)
                            .replace(/@FeeWayName/gi, item.FeeWayName)
                            .replace(/@FeeCharger/gi, item.FeeCharger)
                            .replace(/@Invoice/gi, item.Invoice)
                            .replace(/@FeeChargeDate/gi, item.FeeChargeDate);
                }
            }
        }

        if (newcontent != '') {
            jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            SetTableRowStyle();
        } else {
            ResetSearchInfo("");
        }
        FixedTable();
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
    }
}
/// <summary>
/// 页面初始化
/// </summary>
jQuery(document).ready(function () {
    jQuery("#customerScrollControl").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    TableScrollByID("customerScrollTitle", "customerScrollControl");
    ResetSearchInfo("");
    //jQuery("#slTeam").select2();
    //ResetSearchInfo("");
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
    ResetSearchInfo("");

    var IsSearchDate = document.getElementById("chcIsSearchDate").checked ? 1 : 0; //  jQuery("input[name='chcIsSearchDate']:checked").val(); // 是否将日期作为筛选条件
    var CardNo = jQuery.trim(jQuery('#txtSFZ').val()); //体检号或者身份证号码
    var Invoice = jQuery.trim(jQuery('#txtInvoice').val()); //客户发票号
    var IsHasInvoice = 1; //是否查询只包含发票信息的
    var CustomerName = jQuery.trim(jQuery('#txtCustomerName').val()); //客户名称
    var BeginExamDate = jQuery('#BeginExamDate').val(); //体检开始日期
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();    //体检结束日期
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    if (IsSearchDate == 0) {
        if (CustomerName == "" && Invoice == "" && CardNo == "") {
            ShowSystemDialog("对不起，请您输入查询条件！");
            return false;
        }
    }
    else if (IsSearchDate == 1) {
        //判断时间差 Begin xmhuang 2014-01-14
        if (!CheckTime(BeginExamDate, EndExamDate)) {
            return false;
        }
    }
    var OnlyPerson;    // 仅显示个人的
    var OnlyTeam;     // 仅显示团体的

    //获取团体或个人 xmhuang 2014-04-03
    var slOnlyPerson = document.getElementById("slOnlyPerson").options[document.getElementById("slOnlyPerson").selectedIndex].value; //选择的是个人还是团体
    if (slOnlyPerson == 0)//个人
    {
        OnlyPerson = 1;
    }
    else if (slOnlyPerson == 1)//团体
    {
        OnlyTeam = 1;
    }

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
    ResetSearchInfo("正在查询，请稍候...");
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            IsSearchDate: IsSearchDate,
            Invoice: Invoice,
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
            return false;
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
        msgInfo = "在您查询的条件内，没有找到任何信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
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
            FixedTable();
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
                    newcontent += templateContent.replace(/@RowNum/gi, rowNum + c + 1)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@Age/gi, item.Age)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MobileNo/gi, item.MobileNo)
                            .replace(/@OperateDate/gi, item.OperateDate)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@ExamTypeName/gi, item.ExamTypeName)
                            .replace(/@ExamPlaceName/gi, item.ExamPlaceName)
                            .replace(/@GuideNurse/gi, item.GuideNurse)
                            .replace(/@FeeWayName/gi, item.FeeWayName)
                            .replace(/@FeeCharger/gi, item.FeeCharger)
                            .replace(/@Invoice/gi, item.Invoice)

                            .replace(/@FeeChargeDate/gi, item.FeeChargeDate);
                }
            }
        }
        if (newcontent != '') {
            jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
            SetTableRowStyle();
        }
        else {
            ResetSearchInfo("");
        }
        FixedTable(); 
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
    }
}
/*通用客户端分页脚本 xmhuang 2014-01-13  End*/
function FixedTable() {
    //设置固定表头 xmhuang 2014-04-02
    //$('#tbCustomerList').tablefix({ height: 400, width: 940, fixRows: 1, fixCols: 5 });
}
/// <summary>
/// 设置表横向滚动 
/// </summary>
function TableScrollByID(titleID, scrollID) {
    var $scrollControl = $("#" + scrollID);
    if ($scrollControl.length > 0) {
        var widthLeft = $scrollControl.width() - $scrollControl[0].clientWidth;
        if (widthLeft > 0) {
            var $scrollTitle = $("#" + titleID);
            $scrollTitle.css("width", $scrollTitle.width() + widthLeft);
        }
        $scrollControl.bind("scroll.j-control", function () {
            var left = $(this).scrollLeft();
            $("#" + titleID).css("margin-left", 0 - left);
        });
    }
}