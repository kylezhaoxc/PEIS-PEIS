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
    QueryPagesData(0);
});

function QueryPagesData(pageIndex) {

    optInit = getOptionsFromForm();
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var TeamName = jQuery.trim(jQuery('#txtSearchTeamName').val());
    var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxTeamOper.aspx",
        data: { modelName: modelName, pageIndex: pageIndex, TeamName: TeamName, pageSize: pagePagination.items_per_page, action: 'GetTeamList' },
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
                jQuery(msg.dataList).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_TeaM/gi, item.ID_TeaM)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@Creator/gi, item.Creator)
                            .replace(/@CreateDate/gi, item.CreateDate)
                            .replace(/@InputCode/gi, item.InputCode)
                            .replace(/@Note/gi, item.Note)
                            .replace(/@modelName/gi, modelName)
                            .replace(/@editTitle/gi, editTitle)
                            ;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent);
                }
            } else {
                jQuery('#Searchresult').html("");
                jQuery("#Pagination").hide();
            }
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
    jQuery("[name='ItemCheckbox']").each(function () {
        if (jQuery(this).attr('checked')) {
            IsExist = false;
            CurID = jQuery(this).parent().parent().attr("id");
            TeamTaskName = jQuery(this).parent().parent().find("td a").first().text();
            jQuery(curAllISDeleteDT.dataList).each(function (i, item) {
                if (CurID == item.ID_Team) {
                    ErrorMessage += TeamTaskName + "<br/>,";
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
        ErrorMessage = "团体[" + ErrorMessage.substring(0, ErrorMessage.length - 1) + "]下已存在任务，不可删除,请重新选择！";
        ShowSystemDialog(ErrorMessage);
        return false;
    }
    if (IDTeamS != "") {
        if (confirm("删除团体信息将无法修复！您确认要删除吗？")) {
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
        }
        else {
            return false;
        }
    }
}
/*删除 End*/

function Search() {
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
        drag: false,
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