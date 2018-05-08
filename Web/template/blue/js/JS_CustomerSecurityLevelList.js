/************************该页面为客户操作密级加、解密公用脚本******************************/
// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );
var IsTeam = "";
var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改"; //编辑提示
var curPageIndex = 0;

jQuery(document).ready(function () {
    QueryPagesData(0);
});

function pageselectCallback(page_index, jq) {
    curPageIndex = page_index;
    if (tempOperPageCount > 0) {
        QueryPagesData(page_index);
    }
    tempOperPageCount++;

    return false;
}


function QueryPagesData(pageIndex) {
    optInit = getOptionsFromForm();
    var ID_Customer = jQuery.trim(jQuery("#txtSFZ").val());
      var CustomerName = jQuery.trim(jQuery("#txtCustomerName").val());
   
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var BeginExamDate = jQuery('#BeginExamDate').val(); // 开始日期
    BeginExamDate = encodeURIComponent(BeginExamDate);
    var EndExamDate = jQuery('#EndExamDate').val();     // 结束日期
    EndExamDate = encodeURIComponent(EndExamDate);
    //var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxCustomerSecurityLevel.aspx",
        data: {
            ID_Customer: ID_Customer,
            CustomerName: encodeURIComponent(CustomerName),
            modelName: modelName,
            pageIndex: pageIndex,
            pageSize: pagePagination.items_per_page,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetCustomerSecurityLevelList'
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

                var tmpCustomerIDsStr = ""; // 临时记录体检号（逗号分隔的字符串）

                var newcontent = '';
                var templateContent = jQuery("#RegistListTemplate").html();
                if (templateContent == undefined) { return; }

                jQuery(msg.dataList).each(function (i, item) {

                    if (tmpCustomerIDsStr == "") {
                        tmpCustomerIDsStr = item.ID_Customer;
                    } else {
                        tmpCustomerIDsStr = tmpCustomerIDsStr + "," + item.ID_Customer;
                    }

                    //√×
                    if (item.Is_Team == "True") {
                        item.Is_Team = "√";
                    }
                    else {
                        item.Is_Team = "×";
                    }
                    if (item.Is_Subscribed == "1") {
                        item.Is_Subscribed = "√"
                        modelName = "Regist";
                    }
                    else {
                        item.Is_Subscribed = "×";
                        modelName = "Sign";
                    }
                    if (item.Is_FeeSettled == "True") {
                        item.Is_FeeSettled = "√";
                    }
                    else {
                        item.Is_FeeSettled = "×";
                    }
                    if (item.SecurityLevel > 100) {
                        item.SecurityLevel = "√"
                    }
                    else {
                        item.SecurityLevel = "×"
                    }
                    newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_ArcCustomer/gi, item.ID_ArcCustomer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@MobileNo/gi, item.MobileNo)
                            .replace(/@Is_Team/gi, item.Is_Team)
                            .replace(/@Is_FeeSettled/gi, item.Is_FeeSettled)
                            .replace(/@Is_Subscribed/gi, item.Is_Subscribed)
                            .replace(/@modelName/gi, modelName)
                            .replace(/@editTitle/gi, editTitle)
                            .replace(/@age/gi, item.age)
                            .replace(/@IsTeam/gi, IsTeam)
                            .replace(/@SecurityLevel/gi, item.SecurityLevel)
                            .replace(/@TeamName/gi, item.TeamName)
                            ;
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent);

                    // 查询指定体检号的存档信息及体检基本信息 (查询分页列表的补充信息)
                    GetCustomerExamListArcPhysicalInfo(tmpCustomerIDsStr);
                }
            } else {
                jQuery('#Searchresult').html("");
                jQuery("#Pagination").hide();
                //jQuery("#Pagination").pagination(0, optInit);
            }
        }
    });
}
function DoLoadX(obj) {

    if (jQuery(obj).attr("targeturl") != "") {
        DoLoad(jQuery(obj).attr("targeturl"), '');
    }
}

/*删除 Begin*/
function DoDel() {

    if (confirm("您确认要删除吗？")) {
        //判断是否有选中项目
        var ItemExamCard = '';
        var ItemArcCustomer = '';
        jQuery("[name='ItemCheckbox']").each(function () {
            if (jQuery(this).attr('checked')) {
                jQuery(this).parent().parent().remove();
                ItemExamCard += "'" + jQuery(this).attr("id") + "',";
                ItemArcCustomer += "'" + jQuery(this).attr("ArcCustomer") + "',";
            }
        });
        var qustData = { action: 'DelData',
            type: "Registe",
            ItemExamCard: ItemExamCard,
            ItemArcCustomer: ItemArcCustomer
        };
        if (ItemExamCard != '') {
            //存储大数据请设置Content-length值
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxRegiste.aspx",
                data: qustData,
                cache: false,
                contentType: "application/x-www-form-urlencoded;Content-length=1024000",
                dataType: "json",
                success: function (msg) {
                    ShowSystemDialog(msg.Message);
                    if (msg.success == "1") {
                        RegistListSearch();
                    }
                    else {

                    }

                }
            });
        }
    }
    else {
        return false;
    }
}
/*删除 End*/

function RegistListSearch() {
    //  tempOperPageCount = 0;
    QueryPagesData(curPageIndex); //重新按照新输入的条件进行查询
}

function DirectQueryCustomerID() {

    var ID_Customer = jQuery.trim(jQuery("#txtSFZ").val());
  
    if (isCustomerExamNo(ID_Customer)) {
        jQuery.ajax({
            type: "GET",
            url: "/Ajax/AjaxRegiste.aspx",
            data: { action: "GetCustomerNumber", ID_Customer: ID_Customer,  currenttime: encodeURIComponent(new Date()) },
            cache: false,
            dataType: "json",
            success: function (msg) {

                if (parseInt(msg) > 0) { //如果输入的体检号存在

                    var modelName = jQuery("#modelName").val();
                    if (modelName == null || modelName == undefined || modelName == "") {
                        modelName = "Regist";
                    }

                    var IsTeam = jQuery("#IsTeam").val();
                    if (IsTeam == null || IsTeam == undefined || IsTeam == "") {
                        IsTeam = 0;
                    }

                    DoLoad('/System/Customer/RegistOper.aspx?type=Edit&ID_Customer=' + ID_Customer + '&modelName=' + modelName + '&IsTeam=' + IsTeam, '');
                } else {
                    ShowSystemDialog("您输入的体检号不存在，请核对后在查询！");
                }
            }
        });
    }

}


/// <summary>
/// 查询指定体检号的存档信息及体检基本信息 (查询分页列表的补充信息) 20130721 by WTang 
/// </summary>
function GetCustomerExamListArcPhysicalInfo(CustomerIDsStr) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustExam.aspx",
        data: { CustomerIDsStr: CustomerIDsStr,
            action: 'GetCustomerExamListArcPhysicalInfo',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {

            if (jsonmsg == null || jsonmsg == "" || parseInt(jsonmsg.totalCount) <= 0)
            { return false; }

            jQuery(jsonmsg.dataList0).each(function (i, onarccustitem) {
                jQuery("#GenderName_" + onarccustitem.ID_Customer).html(onarccustitem.GenderName);
                jQuery("#Age_" + onarccustitem.ID_Customer).html(onarccustitem.Age);
                jQuery("#IDCard_" + onarccustitem.ID_Customer).html(onarccustitem.IDCard);
                jQuery("#MarriageName_" + onarccustitem.ID_Customer).html(onarccustitem.MarriageName);
                jQuery("#MobileNo_" + onarccustitem.ID_Customer).html(onarccustitem.MobileNo);
            });
        }
    });

}

function RegistListSearch() {
    tempOperPageCount = 0;
    QueryPagesData(0); //重新按照新输入的条件进行查询
}
/// <summary>
/// 是否可用操作用户 XMHuang 2013-08-09
/// </summary>
function IsCanOperCustomerSecurityLevel() {
    var checkedObj = jQuery("#Searchresult tr[id!='loading'] td input:checked");
    var checkedObjCount = jQuery(checkedObj).length;
    if (checkedObjCount == 0) {
        art.dialog({
            lock: true, fixed: true, opacity: 0.3,
            content: '对不起，请您勾选需要操作的客户名单！',
            icon: 'info',
            ok: true
        });
        return false;
    }
    else {
        return true;
    }
}
/// <summary>
/// Ajax执行加解密 XMHuang 2013-08-09
/// </summary>
function EncodeOrDecodeCustomerSecurityLevel_Ajax(action) {
    //获取勾选的客户名单
    var ID_Customer = "";
    jQuery("#Searchresult tr[id!='loading'] td input:checked").each(function () {
        ID_Customer += jQuery(this).parent().parent().attr("id_customer") + ",";
    });
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustomerSecurityLevel.aspx",
        data: {
            action: action,
            ID_Customer: ID_Customer,
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            ShowSystemDialog(jsonmsg.Message);
            RegistListSearch();
        }
    });
}
/// <summary>
/// 用户加密 XMHuang 2013-08-09
/// </summary>
function EncodeCustomerSecurityLevel() {
    //判断是否有选择
    if (IsCanOperCustomerSecurityLevel()) {
        EncodeOrDecodeCustomerSecurityLevel_Ajax("EncodeCustomerSecurityLevel");
    }
}
/// <summary>
/// 用户解密 XMHuang 2013-08-09
/// </summary>
function DecodeCustomerSecurityLevel() {
    //判断是否有选择
    if (IsCanOperCustomerSecurityLevel()) {
        EncodeOrDecodeCustomerSecurityLevel_Ajax("DecodeCustomerSecurityLevel");
    }
}