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
    jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    ResetSearchInfo("");
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
    var OperateLevel = jQuery.trim(jQuery("#OperateLevel").val());
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var BeginExamDate = jQuery('#BeginExamDate').val(); // 开始日期
    BeginExamDate = encodeURIComponent(BeginExamDate);
    var EndExamDate = jQuery('#EndExamDate').val();     // 结束日期
    EndExamDate = encodeURIComponent(EndExamDate);
    var TeamID = jQuery('#idSelectTeam').val();     // document.getElementById("txtTeamNameX").options[document.getElementById("txtTeamNameX").selectedIndex].value; //团体ID
    var TeamTaskID = jQuery('#idSelectTeamTask').val();     //document.getElementById("txtTeamTaskNameX").options[document.getElementById("txtTeamTaskNameX").selectedIndex].value; //任务ID
    var SecurityLevel = document.getElementById("slOperateLevel").options[document.getElementById("slOperateLevel").selectedIndex].value; //操作密级
    //var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxCustomerSecurityLevel.aspx",
        data: {
            OperateLevel: OperateLevel,
            ID_Customer: ID_Customer,
            TeamID: TeamID,
            TeamTaskID: TeamTaskID,
            SecurityLevel: SecurityLevel,
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
                    jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                    tempOldtotalCount = msg.totalCount;
                    tempOperPageCount = 1;
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                    tempOperPageCount = 1;
                }
                var RowNum = 1;
                if (pageIndex > 0) {
                    RowNum = optInit.items_per_page * (pageIndex) + 1;
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
                        item.SecurityLevel = "√";
                    }
                    else {
                        //item.SecurityLevel = "×";
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
                            .replace(/@age/gi, item.Age)
                            .replace(/@IsTeam/gi, IsTeam)
                            .replace(/@SecurityLevel/gi, UpperToChines(item.SecurityLevel))
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@RowNum/gi, RowNum)
                             .replace(/@IDCard/gi, item.IDCard)
                             .replace(/@GenderName/gi, item.GenderName)
                            ;
                    RowNum++;
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent);
                    SetTableRowStyle();
                }
            } else {
                jQuery('#Searchresult').html("");
                jQuery("#Pagination").hide();
                //jQuery("#Pagination").pagination(0, optInit);
            }

            // 判断表格是否存在滚动条,并设置相应的样式
            JudgeTableIsExistScroll();
        }
    });
}
function DoLoadX(obj) {

    if (jQuery(obj).attr("targeturl") != "") {
        DoLoad(jQuery(obj).attr("targeturl"), '');
    }
}

function RegistListSearch() {
    ResetSearchInfo("正在查询，请稍候...");
    //  tempOperPageCount = 0;
    QueryPagesData(curPageIndex); //重新按照新输入的条件进行查询
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
    var SecurityLevel = document.getElementById("slOperateLevel").options[document.getElementById("slOperateLevel").selectedIndex].value; //操作密级
    if (SecurityLevel == -1) {
        ShowSystemDialog("请选择密级！");
        return false;
    }
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
            SecurityLevel: SecurityLevel,
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
/// <summary>
/// 隐藏快速查询框
/// </summary>
function HideAllQuickTable() {
    ShowHideQuickQueryTeamTable(false, "");      // 团体
    ShowHideQuickQueryTeamTaskTable(false, "");  // 团体任务
}

var ChinesNum = ["零级", "一级", "二级", "三级", "四级", "五级", "六级", "七级", "八级", "九级", "十级"];
function UpperToChines(num) {
    if (num < ChinesNum.length) {
        return ChinesNum[num];
    }
    return num;
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
    jQuery("#Pagination").hide(); //隐藏分页控件
}