/************************该页面为个人登记工作量统计公用脚本******************************/
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
        var templateContent = jQuery("#TemplateRegister tbody").html();
        if (templateContent == undefined) { return false; }
        for (var c = 0; c < curPageSize; c++) {
            if (rowNum + c > dataLength) {
                break;
            }
            item = pagerData[rowNum + c];
            if (item != undefined) {
                if (templateContent != null) {
                    newcontent += templateContent.replace(/@RowNum/gi, rowNum + c + 1)
                            .replace(/@RegisterName/gi, item.RegisterName)
                            .replace(/@FeeItemName/gi, item.FeeItemName)
                            .replace(/@RegistCount/gi, item.RegistCount)
                            .replace(/@SumFactPrice/gi, parseFloat(item.SumFactPrice).toFixed(2))
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
    else {
        jQuery("#Pagination").hide();
    }
}

jQuery(document).ready(function () {
    jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    //jQuery("#slRegister").select2();
    ResetSearchInfo("");
});
/// <summary>
/// 获取个人登记工作量信息
/// </summary>
function GetRegisteWorkLoad() {
    var ID_Register = document.getElementById("slRegister").options[document.getElementById("slRegister").selectedIndex].value; //登记人
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    //判断时间差 Begin xmhuang 2014-01-14
    if (!CheckTime(BeginExamDate, EndExamDate)) {
        return false;
    }
    if (ID_Register == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的登记人!");
        return false;
    }
    ResetSearchInfo("正在查询数据，请稍候...");
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Register: ID_Register,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetRegisteWorkLoad'
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

                //                var newcontent = '';
                //                var templateContent = jQuery("#TemplateRegister tbody").html();
                //                if (templateContent == undefined) { return false; }
                //                var RowNum = 1;
                //                jQuery(msg.dataList0).each(function (i, item) {
                //                    if (templateContent != null) {
                //                        newcontent += templateContent.replace(/@RowNum/gi, RowNum)
                //                            .replace(/@RegisterName/gi, item.RegisterName)
                //                            .replace(/@FeeItemName /gi, item.FeeItemName)
                //                            .replace(/@RegistCount/gi, item.RegistCount)
                //                            .replace(/@SumFactPrice/gi, item.SumFactPrice)
                //                            ;
                //                        RowNum++;
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
        msgInfo = "在您查询的条件内，没有找到任何信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
}