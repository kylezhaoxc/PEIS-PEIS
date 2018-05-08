/************************该页面为客户档案列表页公用脚本******************************/
// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );
var defalutImagSrc = "/template/blue/images/icons/nohead.gif"; //默认头像
var IsTeam = ""                                      //是否团体的标记，该标记从参数中输出到页面隐藏域
var tempOperPageCount = 0;                           //总页数
var tempOldtotalCount = 0;                           //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改";                      //编辑提示

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
    ResetSearchInfo("");
    //QueryPagesData(0);                                //分页检索预约、登记数据       XMHuang 2013-08-12 添加注释
    //设置默认光标                 XMHuang 2013-08-12 添加注释

});

/// <summary>
///分页函数
///pageIndex:当前页面ID
/// </summary>
function QueryPagesData(pageIndex) {
    ResetSearchInfo("");
    var optInit = getOptionsFromForm();                   //获取分页配置参数
    var totalCount = 0;                               //总条数
    var CustomerName = jQuery.trim(jQuery('#txtCustomerName').val()); //客户名称
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var IDCard = jQuery.trim(jQuery('#txtSFZ').val()); // 证件号
    var Birthday = jQuery.trim(jQuery('#txtBirthday').val()); // 出生日期
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxCustomerManage.aspx",
        data: {
            pageIndex: pageIndex,
            pageSize: pagePagination.items_per_page,
            CustomerName: CustomerName,
            Gender: Gender,
            IDCard: IDCard,
            Birthday: Birthday,
            action: 'GetCustomerManageList'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {
                jQuery("#Pagination").show();
                if (tempOperPageCount == 0) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                    tempOldtotalCount = msg.totalCount;
                    tempOperPageCount = 1;
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                    tempOperPageCount = 1;
                }
            }
            var CustomerManageTempLate = jQuery("#CustomerManageTempLate").html(); //获取模板
            var Content = "";

            var RowNum = 1;
            if (pageIndex > 0) {
                RowNum = optInit.items_per_page * (pageIndex) + 1;
            }
            jQuery(msg.dataList).each(function (i, item) {
                if (item.Base64Photo == "") {
                    item.Base64Photo = defalutImagSrc;
                }
                else {
                    item.Base64Photo = "data:image/gif;base64," + item.Base64Photo;
                }
                Content += CustomerManageTempLate.replace(/@RowNum/gi, RowNum)
                            .replace(/@ID_ArcCustomer/gi, item.ID_ArcCustomer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@age/gi, item.age)
                            .replace(/@Base64Photo/gi, item.Base64Photo)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MobileNo/gi, item.MobileNo)
                            .replace(/@BirthDay/gi, item.BirthDay)
                            .replace(/@Gender/gi, item.ID_Gender)
                            .replace(/@ID_Marriage/gi, item.ID_Marriage)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@NationName/gi, item.NationName)
                            ;
                RowNum++;
            });
            if (Content != "") {
                jQuery("#Pagination").show();
                jQuery("#Searchresult").html(Content);
            }
        }
    });
}

function RegistListSearch() {
    //必须输入一项查询条件方可查询
    var CustomerName = jQuery.trim(jQuery('#txtCustomerName').val()); //客户名称
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var IDCard = jQuery.trim(jQuery('#txtSFZ').val()); // 证件号
    var Birthday = jQuery.trim(jQuery('#txtBirthday').val()); // 出生日期
    if (CustomerName == "" && IDCard == "") {
        ShowSystemDialog("对不起，请您输入客户名称或证件号进行查询!");
        return false;
    }
    //    if (CustomerName == "" && Gender == -1 && IDCard == "" && Birthday == "") {
    //        ShowSystemDialog("请您输入至少一项查询条件!");
    //        return false;
    //    }
    else {
        tempOperPageCount = 0;
        QueryPagesData(0); //重新按照新输入的条件进行查询
    }
}

/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的条件内，没有找到任何客户信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='11'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}

/// <summary>
/// 弹出团体编辑页面
/// </summary>
function OpenCustomerManage(obj, title) {
    var url = jQuery(obj).attr("targeturl");
    if (title == "" || title == undefined) {
        title = "修改客户基本信息";
    }
    art.dialog.open(url,
    {
        width: 400,
        height: 350,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: title,
        cache: false,
        init: function () {
            jQuery(".aui_close").hide();
        },
        close: function () {
            RegistListSearch();
        }
    });
}
