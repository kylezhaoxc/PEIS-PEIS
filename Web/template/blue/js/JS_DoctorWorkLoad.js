/************************该页面为医生工作量统计公用脚本******************************/
// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );

var tempOperPageCount = 0;                           //总页数
var tempOldtotalCount = 0;                           //初始总页数，用于判断是否更新页码

/// <summary>
///分页回调函数
///page_index:当前页面ID
///jq:jQuery对象
/// </summary>
function pageselectCallback(page_index, jq) {

    if (tempOperPageCount > 0) {
        QueryPagesData(page_index);                  //调用具体的分页方法           XMHuang 2013-08-12 添加注释
    }
    tempOperPageCount++;                             //设置当前分页条数+1           XMHuang 2013-08-12 添加注释
    return false;
}

/// <summary>
///分页函数 XMHuang 2013-08-12 添加注释 
///pageIndex:当前页面ID
/// </summary>
jQuery(document).ready(function () {
    jQuery("#slDoctor").select2();
    ResetSearchInfo("尚无任何数据...");
    //QueryPagesData(0);                                //分页检索预约、登记数据       XMHuang 2013-08-12 添加注释
    //设置默认光标                 XMHuang 2013-08-12 添加注释

});

/// <summary>
///分页函数
///pageIndex:当前页面ID
/// </summary>
function QueryPagesData(pageIndex) {
    jQuery('#Searchresult').html("");
    var totalCount = 0;                               //总条数
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    var ID_Doctor = document.getElementById("slDoctor").options[document.getElementById("slDoctor").selectedIndex].value; //获取被查询医生
    var Doctor = document.getElementById("slDoctor").options[document.getElementById("slDoctor").selectedIndex].text; //获取被查询医生
    ID_Doctor = encodeURIComponent(ID_Doctor);    //编码被查询医生
    //判断统计项目类型divItem是否显示
    if (jQuery("#divItem").is(":visible") == true) {
        flag = document.getElementById("slItem").options[document.getElementById("slItem").selectedIndex].value; //检查项目类型
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            flag: flag,
            ID_Doctor: ID_Doctor,
            Doctor: Doctor,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetDoctorWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                var newcontent = '';
                var templateContent = jQuery("#DoctorTemplate tbody").html();
                if (templateContent == undefined) { return; }

                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@UserName/gi, item.UserName)
                            .replace(/@Num/gi, item.Num)
                            .replace(/@SectionName/gi, item.SectionName);
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



/*删除 End*/

function RegistListSearch() {

    var BeginExamDate = jQuery.trim(jQuery('#BeginExamDate').val());
    var EndExamDate = jQuery.trim(jQuery('#EndExamDate').val());
    if (BeginExamDate == "") {
        jQuery('#BeginExamDate').focus();
        jQuery('#BeginExamDate').select();
        alert("请您输入开始日期!");
        return false;
    }
    else if (EndExamDate == "") {
        jQuery('#EndExamDate').focus();
        jQuery('#EndExamDate').select();
        alert("请您输入结束日期!");
        return false;
    }
    else {
        //判断时间差 Begin xmhuang 2014-01-14
        if (!CheckTime(BeginExamDate, EndExamDate)) {
            return false;
        }
        tempOperPageCount = 0;
        QueryPagesData(0); //重新按照新输入的条件进行查询
    }
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