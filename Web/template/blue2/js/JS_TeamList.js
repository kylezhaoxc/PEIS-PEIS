/************************该页面为团队公用脚本******************************/
// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );

var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击查看当前团体的任务信息"; //编辑提示
function pageselectCallback(page_index, jq) {

    if (tempOperPageCount > 0) {
        QueryPagesData(page_index);
    }
    tempOperPageCount++;

    return false;
}

jQuery(document).ready(function () {
    ResetSearchInfo("");
    jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
    jQuery("#teamAddDiv").drag({ handler: jQuery(".teamaddTitle") });
    jQuery("#teamModifyDiv").drag({ handler: jQuery(".teamModifyTitle") });
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    //QueryPagesData(0);                  //注释默认加载功能 xmhuang 2014-04-06
});

function QueryPagesData(pageIndex) {
    optInit = getOptionsFromForm();
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var TeamID = jQuery('#idSelectTeam').val();
    var TeamName = jQuery('#nameSelectTeam').val();
    //var TeamName = jQuery('#nameSelectTeam').val();
    var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxTeamOper.aspx",
        data: { modelName: modelName, pageIndex: pageIndex, TeamID: TeamID, TeamName: TeamName, pageSize: pagePagination.items_per_page, action: 'GetTeamList' },
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


                var newcontent = '';
                //                var templateContent = '<tr id="@ID_TeaM"><td><input ID_TeaM="@ID_TeaM" id="@ID_TeaM" title="" type="checkbox" name="ItemCheckbox"/></td>' +
                //                        '<td><lable>@ID_TeaM</lable></td>' +
                //                        '<td><a href="javascript:void(0);" onclick="OpenTeam(this.targeturl,"编辑团体[@TeamName]");" targeturl="/System/Customer/TeamListOper.aspx?type=Edit&mark=teamlist&modelName=dialogteamlist&ID_TeaM=@ID_TeaM" title="@editTitle">@TeamName</a></td>' +
                //                        '<td><lable>@Creator</lable></td>' +
                //                        '<td>@CreateDate</td>' +
                //                        '<td>@InputCode</td>' +
                //                        '<td>@Note</td>' +
                //                        '<td><a href="javascript:void(0);" onclick="DoLoadX(this);" targeturl="/System/Customer/TeamTaskList.aspx?type=Edit&modelName=teamlist&ID_TeaM=@ID_TeaM&curTeam=@ID_TeaM" title="@editTitle">查看</a></td>' +
                //                        '</tr>';
                var templateContent = jQuery("#TeamListTeamplate tbody").html();
                var rowNum = 1;
                if (pageIndex > 0) {
                    rowNum = optInit.items_per_page * (pageIndex) + 1;
                }
                jQuery(msg.dataList).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_TeaM/gi, item.ID_TeaM)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@targetTeamName/gi, encodeURIComponent(item.TeamName))
                              .replace(/@ID_Creator/gi, item.ID_Creator)
                            .replace(/@Creator/gi, item.Creator)
                            .replace(/@CreateDate/gi, item.CreateDate)
                            .replace(/@InputCode/gi, item.InputCode)
                            .replace(/@Note/gi, item.Note)
                            .replace(/@modelName/gi, modelName)
                            .replace(/@editTitle/gi, editTitle)
                            .replace(/@RowNum/gi, rowNum);
                        rowNum++;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); SetTableRowStyle();
                }
            } else {
                ResetSearchInfo("");
                jQuery("#Pagination").hide();
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
var allISDeleteDT = ""; //当前团体所有不可用于删除的团体ID，团体任务，团体任务分组，体检号
function ISCanDeleteTeamInfo(ID_Team) {
    if (ID_Team == "") {
        return false;
    }
    //这里发起一个同步请求，直到返回数据后方可操作
    var flag = false;
    jQuery.ajax({
        type: "GET",
        async: false,
        url: "/Ajax/AjaxTeamOper.aspx",
        data: { action: "ISCanDeleteTeamInfo", ID_Team: ID_Team },
        cache: false,
        dataType: "json",
        success: function (msg) {
            allISDeleteDT = msg;
        }
    });
    return allISDeleteDT;
}
/*删除 Begin*/
function DoDel() {

    var IDTeamS = '';
    jQuery("#Searchresult [name='ItemCheckbox']").each(function () {
        if (jQuery(this).attr('checked')) {
            IDTeamS += "'" + jQuery(this).attr("id") + "',";
        }
    });
    var curAllISDeleteDT = ISCanDeleteTeamInfo(IDTeamS);
    /******如果出现异常则不允许执行下一步 xmhuang 2013-12-11*********/
    if (curAllISDeleteDT.success != undefined) {
        if (curAllISDeleteDT.success == -1) {
            ShowSystemDialog(curAllISDeleteDT.Message);
            return false;
        }
    }
    /******如果出现异常则不允许执行下一步 xmhuang 2013-12-11*********/
    IDTeamS = "";
    var CustTeamTaskID = '';
    var IsExist = false;
    var ErrorMessage = "";
    var CurID = "", TeamTaskName = "";
    jQuery("#Searchresult [name='ItemCheckbox']").each(function () {
        if (jQuery(this).attr('checked')) {
            IsExist = false;
            CurID = jQuery(this).parent().parent().attr("id");
            TeamTaskName = jQuery(this).parent().parent().find("td a").first().text();
            jQuery(curAllISDeleteDT.dataList).each(function (i, item) {
                if (CurID == item.ID_Team) {
                    ErrorMessage += TeamTaskName + "<br/>";
                    IsExist = true;
                    return false;
                }
            });
            if (!IsExist) {
                IDTeamS += "'" + jQuery(this).parent().parent().attr('id') + "',";
            }
        }
    });
    if (ErrorMessage != "") {
        ErrorMessage = ErrorMessage + " 已存在任务，不可删除,请重新选择！";
        ShowSystemDialog(ErrorMessage);
        return false;
    }
    if (IDTeamS != "") {
        var msgContent = "删除团体信息将无法修复！您确认要删除吗？";
        var dialog = art.dialog({
            id: 'artDialogIDRegisterDate',
            lock: true,
            fixed: true,
            opacity: 0.3,
            title: '温馨提示',
            content: msgContent,
            button: [{
                name: '取消',
                callback: function () {
                    return true;
                }
            }, {
                name: '确定',
                callback: function () {
                    var qustData = { action: 'DelData',
                        IDTeamS: IDTeamS
                    };
                    if (IDTeamS != '') {
                        //存储大数据请设置Content-length值
                        jQuery.ajax({
                            type: "POST",
                            url: "/Ajax/AjaxTeamOper.aspx",
                            data: qustData,
                            cache: false,
                            contentType: "application/x-www-form-urlencoded;Content-length=1024000",
                            dataType: "json",
                            success: function (msg) {
                                ShowSystemDialog(msg.Message);
                                if (msg.success == "1") {
                                    Search();
                                }
                                else {

                                }

                            }
                        });
                    }
                    return true;

                }, focus: true
            }]

        }).lock();
    }
}
/*删除 End*/

function Search() {
    ResetSearchInfo("正在查询，请稍候...");
    tempOperPageCount = 0;
    QueryPagesData(0); //重新按照新输入的条件进行查询
}
/****************************团体弹出框修改数据  Begin*************************************/

/// <summary>
/// 弹出团体编辑页面
/// </summary>
function OpenTeam(obj, title) {
    var url = jQuery(obj).attr("targeturl");
    if (title == "" || title == undefined) {
        title = "新增团体信息";
    }
    art.dialog.open(url,
    {
        width: 350,
        height: 250,
        drag: true,
        lock: true,
        id: 'OperWindowFrame',
        title: title,
        cache: false,
        init: function () {
            jQuery(".aui_close").hide();
        },
        close: function () {
            Search();
        }
    });
}

//function OpenTeam(url, title) {
//    if (title == "" || title == undefined) {
//        title = "新增团体信息";
//    }
//    url += "&title=" + encodeURIComponent(title) + "&date=" + new Date();

//    var para = new OpenDialogPara("no");
//    window.showModalDialog(url, para, "dialogWidth:320px; dialogHeight:260px; scroll:no; status:no; resizable:no");
//    if (para.ReturnValue == "ok") {
//        Search();
//    }
//    else if (para.ReturnValue == "cancel") {
//        //ShowSystemDialog("用户取消了操作");
//    }
//    else if (para.ReturnValue == "no") {
//        //ShowSystemDialog("用户直接关闭了对话框");
//    }
//}
/****************************团体弹出框修改数据  Begin*************************************/

//团体点击回调函数 xmhuang 2014-04-02
function TeamCallBack() {
    Search();
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的条件内，没有找到任何客户信息！";
    }
    var html = "<tr><td class='msg' colSpan='1000'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    SetTableRowStyle();
    jQuery("#Pagination").hide(); //隐藏分页控件
}