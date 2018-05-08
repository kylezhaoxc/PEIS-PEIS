/****************************团体信息搜索 Begin*******************************/
var OldInputCode = ""; //上次输入的输入码
var OldInputCodeElementID = '';
/// <summary>
/// 根据输入码查询团体信息（通过Ajax后台过滤）
///TempleteID:模版ID
///InputCodeElementID：录入文本ID
/// </summary>
function GetTeamInfoByKeyWords_Ajax(QueryAreaElementID, ShowDataElementID, TempleteID, InputCodeElementID) {

    var InputCode = jQuery.trim(jQuery('#' + InputCodeElementID).val()); //jQuery.trim(jQuery('#txtTeamName').val());
    if (OldInputCode != InputCode || OldInputCodeElementID != InputCodeElementID) {
        OldInputCode = InputCode;
        OldInputCodeElementID = InputCodeElementID;
    } else {
        QueryTeamList(InputCodeElementID, QueryAreaElementID, true);
        return;
    }
    var action = "", ID_Team = "";
    //txtTeamName:表示从团体任务编辑页面检索，txtTeamNameX表示从备单中检索
    if (InputCodeElementID == "txtTeamNameX" || InputCodeElementID == "txtTeamName") {
        action = "GetTeamInfoByKeyWords";
    }
    else if (InputCodeElementID == "txtTeamTaskNameX") {
        ID_Team = jQuery('#txtTeamNameX').data("ID_Team");
        if (ID_Team == "" || ID_Team == undefined) {
            ShowSystemDialog("请您选择团体名称！");
            document.getElementById("txtTeamNameX").focus();
            return;
        }
        action = "GetTeamTaskInfoByKeyWords";
    }
    if (InputCode != "") {
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxTeamOper.aspx",
            cache: true,
            data: { action: action,
                InputCode: InputCode,
                ID_Team: ID_Team
            },
            cache: false,
            dataType: "json",
            success: function (jsonmsg) {
                ShowTeamDataList_Ajax(QueryAreaElementID, ShowDataElementID, TempleteID, jsonmsg, InputCodeElementID);
            }
        });
    } else {
        ShowTeamDataList_Ajax(QueryAreaElementID, ShowDataElementID, TempleteID, "", InputCodeElementID);
    }
}

/// <summary>
/// 根据查询结果数据，显示团体信息（通过Ajax过滤）
///TempleteID模版ID
///teamlist:数据源
///InputCode输入码
/// </summary>
function ShowTeamDataList_Ajax(QueryAreaElementID, ShowDataElementID, TempleteID, teamlist, InputCodeElementID) {

    if (QueryAreaElementID == undefined || QueryAreaElementID == "") {
        QueryAreaElementID = "TeamQueryArea";
    }
    if (ShowDataElementID == undefined || ShowDataElementID == "") {
        ShowDataElementID = "tbleShowTeamList";
    }
    jQuery("#" + InputCodeElementID).removeData("ID_Team");
    var InputCode = jQuery.trim(jQuery('#' + InputCodeElementID).val()); //jQuery.trim(jQuery('#txtTeamName').val());
    var teamListContent = ""; //团体检索table内容
    var teamListTheadTempleteContent = jQuery('#' + TempleteID + ' thead').html(); //团体检索标题内容模版
    var teamListBodyTempleteContent = jQuery('#' + TempleteID + ' tbody').html(); //团体检索主体内容模版
    var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
    var currCodeIndex = 0;
    teamListTheadTempleteContent = teamListTheadTempleteContent.replace(/@TeamNameTitle/gi, "团体名称")
    .replace(/@InputCodeTitle/gi, "输入码")
    if (teamlist != "") {
        jQuery(teamlist.dataList).each(function (i, item) {
            // 如果字符串不包含这个输入码，则继续下一条数据的判断
            CurrQueryCount++;
            teamListContent += teamListBodyTempleteContent
                            .replace(/@ID_Team/gi, item.ID_Team)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@InputCode/gi, item.InputCode);
        });
    }
    if (teamListContent != "") {
        //设置目标div
        QueryTeamList(InputCodeElementID, QueryAreaElementID, true);
        var allTeamListHtml = ("<thead>" + teamListTheadTempleteContent + "</thead>") + ("<tbody>" + teamListContent + "</tbody>");
        jQuery("#" + ShowDataElementID).html(allTeamListHtml);
    } else {
        QueryTeamList(InputCodeElementID, QueryAreaElementID, false);
        jQuery("#" + ShowDataElementID).html("");
    }
    BindTeamListClick(InputCodeElementID, QueryAreaElementID);
}

/// <summary>
/// 根据查询结果数据，显示团体信息
/// </summary>
function QueryTeamList(elementID, QueryAreaElementID, flag) {

    if (QueryAreaElementID == undefined) {
        QueryAreaElementID == "TeamQueryArea";
    }
    var InputCode = jQuery('#' + elementID).val();

    if (InputCode != "" && flag == true) {
        jQuery("#" + QueryAreaElementID).removeClass("hideQueryTeamClass");
        jQuery("#" + QueryAreaElementID).addClass("showQueryTeamClass");
    } else {
        jQuery("#" + QueryAreaElementID).removeClass("showQueryTeamClass");
        jQuery("#" + QueryAreaElementID).addClass("hideQueryTeamClass");
    }
}
function BindTeamListClick(elementID, QueryAreaElementID) {

    jQuery(".tblList tbody tr").click(function () {

        jQuery('#' + elementID).val(jQuery.trim(jQuery(this).find("td[name='show']").text()));
        jQuery('#' + elementID).data("ID_Team", jQuery(this).attr("id"));

        QueryTeamList(elementID, QueryAreaElementID, false);
        if (QueryAreaElementID == "TeamQueryArea") {

            GetTeamTaskInfoByKeyWord("tblTeamTask", "ID_Team", jQuery('#txtTeamName').data("ID_Team"), 0);
        }

        //是否是团体批量操作
        if (jQuery("#modelName").val() != "TeamBatchOper") {
            //设置光标位置,
            //如果检索的是团体，则需要设置任务光标
            if (elementID == "txtTeamNameX") {
                jQuery("#txtTeamTaskNameX").focus();
                jQuery("#txtTeamTaskNameX").select();
            }
            //如果是团体任务,需要绑定团体任务分组信息
            else if (elementID == "txtTeamTaskNameX") {
                GetTeamTaskGroupData_Ajax("tblTeamTaskGroupX", 0);
            }
        }
        else {
            if (elementID == "txtTeamNameX") {
                jQuery("#txtTeamTaskNameX").focus();
                jQuery("#txtTeamTaskNameX").select();
            }
            else {
                //这里绑定所有的团体名单信息
                LoadTeamTaskGroupCustomerInfo(jQuery("#txtTeamNameX").data("ID_Team"), jQuery("#txtTeamTaskNameX").data("ID_Team"), "tblTeamTaskGroupCustomerX");
            }
        }
    });
}
/// <summary>
/// 全选子元素
///obj:父元素
///ShowDataElementID: 需要设置元素选中状态的tableID
/// </summary>
function CheckAllCustomer(obj, ShowDataElementID) {
    jQuery("#" + ShowDataElementID + " tbody tr td input[name='ItemCheckbox']").each(function () {
        jQuery(this).attr('checked', obj.checked);
    })
}
/// <summary>
/// 全选子元素
///obj:父元素
///ShowDataElementID: 需要设置元素选中状态的tableID
/// </summary>
function CheckAllTeamTaskGroupData(obj, ShowDataElementID) {
    jQuery("#" + ShowDataElementID + " tbody tr td input[name='ItemCheckbox']").each(function () {
        jQuery(this).attr('checked', obj.checked);
    })
}
/// <summary>
/// 折扣变动事件
/// </summary>
function OnDisCoutChange(obj) {
    var curZK = jQuery.trim(jQuery(obj).val());
    if (curZK != "") {
        if (parseFloat(curZK) < DisCountRate) {
            curZK = DisCountRate;
        }
        if (curZK == 0) {
            curZK = 10;
        }
        if (parseFloat(curZK) > 10) {
            curZK = 10;
        }
        jQuery("#txtXMZK").val(curZK);
        //遍历所有勾选项设置统一折扣
        jQuery("#tblTeamTaskGroupFee tbody tr[id!='loading'] td input[name='ItemCheckbox']").each(function () {
            if (jQuery(this).parent().parent().attr('CustFeeChargeState') != "0") {
                if (jQuery(this).attr("checked")) {
                    id = jQuery(this).parent().parent().attr("id");
                    jQuery(this).parent().parent().find("td label[name='zk']").text(curZK);
                    //jQuery("#zk_" + id).text(curZK);
                }
            }
        });
    }
    SumJG(); //计算总计
    jQuery(obj).select();
}
/// <summary>
/// 读取团体任务模版
///elementID：模版ID
///showDataElementID：填充目标数据ID
/// </summary>
function ReadTeamTaskTemplate(TempleteID, ShowDataElementID) {
    //默认是读取tblTemplateTeamTaskList模版填充到tblTeamTask中显示
    if (TempleteID == undefined) {
        TempleteID = "tblTemplateTeamTaskList";
    }
    if (ShowDataElementID == undefined) {
        ShowDataElementID = "tblTeamTask";
    }
    var teamTaskGroupListTheadTempleteContent = jQuery('#' + TempleteID + ' thead').html(); //团体任务模版Thead部分
    var teamTaskGroupListBodyTempleteContent = jQuery('#' + TempleteID + ' tbody').html(); //团体任务模版body部分
    this.teamTaskListTheadTempleteContent = teamTaskGroupListTheadTempleteContent;
    this.teamTaskListBodyTempleteContent = teamTaskGroupListBodyTempleteContent;
}
/// <summary>
/// 读取团体任务分组模版并返回thread部分和tbody部分html内容
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function ReadTemplateTeamTaskGroup(TemplateTeamTaskGroupID) {
    //默认是读取tblTemplateTeamTaskList模版填充到tblTeamTask中显示
    if (TemplateTeamTaskGroupID == "" || TemplateTeamTaskGroupID == undefined) {
        TemplateTeamTaskGroupID = "tblTemplateTeamTaskGroup";
    }
    var teamTaskGroupListContent = ""; //团体任务分组table内容
    var teamTaskGroupListTheadTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' thead').html(); //团体任务模版Thead部分
    var teamTaskListBodyTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' tbody').html(); //团体任务模版body部分
    this.teamTaskGroupListTheadTempleteContent = teamTaskGroupListTheadTempleteContent;
    this.teamTaskListBodyTempleteContent = teamTaskListBodyTempleteContent;
    return this;
}
/****************************团体信息搜索 End*******************************/



/*********汇总费用 Begin*******************/

/// <summary>
/// 汇总收费项目
///修改日期：2013-06-24 XMHuang 修改了汇总统一收费的总金额
/// </summary>
function SumJG() {
    var xmzj = jQuery("#tblTeamTaskGroupFee tbody tr[id!='loading'] td input[name='ItemCheckbox']").length; //项目总计个数
    var sumYJ = 0; //项目原价总总计
    var sumZK = 0; //项目折扣后总计
    var sumSJ = 0; //项目实际总计
    var curZK = DisCountRate;
    var curSJ = 0;
    var sumXJJE = 0; //现金金额
    var sumJZJE = 0; //记账金额
    var SumTYFF = 0; //统一付费金额
    var feeTypeName = '';
    var RowNum = 1;
    jQuery("#tblTeamTaskGroupFee tbody tr[id!='loading'] td label[name='yj']").each(function () {
        //设置序号 xmhuang 2014-03-26
        jQuery(this).parent().parent().find("[name = 'lblRowNum']").text(RowNum); RowNum++;
        sumYJ += parseFloat(jQuery.trim(jQuery(this).text()));
        curZK = jQuery.trim(jQuery(this).parent().parent().find("[name = 'zk']").text());
        curSJ = parseFloat(jQuery.trim(jQuery(this).text())) * parseFloat(curZK) / 10;
        feeTypeName = jQuery.trim(jQuery(this).parent().parent().find("[name = 'fffs']").text());
        jQuery(this).parent().parent().find("[name = 'sj']").text(parseFloat(curSJ).toFixed(curFixed));
        if (feeTypeName == "现金") {
            sumXJJE += curSJ;
        }
        else if (feeTypeName == "记账") {
            sumJZJE += curSJ;
        }
        //        else if (feeTypeName == "统一收费") {
        SumTYFF += curSJ;
        //}
        sumSJ += curSJ;
    });
    sumXJJE = parseFloat(sumXJJE).toFixed(curFixed);
    sumYJ = parseFloat(sumYJ).toFixed(curFixed);
    sumSJ = parseFloat(sumSJ).toFixed(curFixed);
    SumTYFF = parseFloat(SumTYFF).toFixed(curFixed);
    //设置总体价格汇总
    jQuery("#lblSumHeaderX").html("");
    //var curHtml = "收费项目(<label style='color:red;font-size:13px; text-decoration:underline;'>共：" + xmzj + "个；原价总计：" + sumYJ.toString() + "元；折扣总计：" + sumSJ.toString() + "元；折扣后总计：" + SumTYFF.toString() + "元</label>)";
    //var curHtml = "项目总数：" + xmzj + "个&nbsp;原价总计：￥" + sumYJ.toString() + "&nbsp;折扣总计：￥" + sumSJ.toString() + "&nbsp;折扣后总计：￥" + SumTYFF.toString();
    var data = { xmzj: xmzj, yjzj: sumYJ, zkzj: sumSJ, zkhzj: sumSJ, xjzf: SumTYFF, jzzf: sumJZJE };

    var curHtml = '<span>项目总数：</span><span class="come_loose_center_red">' + xmzj + '个</span>';
    curHtml += '<span>原价总计：</span><span class="come_loose_center_red">￥' + sumYJ.toString() + '</span>';
    curHtml += '<span>折扣总计：</span><span class="come_loose_center_red">￥' + sumSJ.toString() + '</span>';
    curHtml += '<span>折扣后总计：</span><span class="come_loose_center_red">￥' + SumTYFF.toString() + '</span>';

    var waitChargeText = " 待收：￥" + SumTYFF.toString();
    jQuery("#lblWaitCharge").text(waitChargeText);

    jQuery("#divSumHeader").data("sumData", data);
    jQuery("#divSumHeader").html(curHtml);
    //jQuery("#lblSumHeaderX").html("折扣后总计：" + SumTYFF + "元");
}

function ResetSumJG() {
    //设置总体价格汇总
    //    jQuery("#lblSumHeaderX").html("");
    //    var curHtml = "收费项目(<label style='color:red;font-size:13px; text-decoration:underline;'>共：" + 0 + "个；原价总计：0元；折扣总计：0元；折扣后总计：0元</label>)";
    //    var data = { xmzj: 0, yjzj: 0, zkzj: 0, zkhzj: 0, xjzf: 0, jzzf: 0 };
    //    jQuery("#divSumHeader").data("sumData", data);
    //    jQuery("#divSumHeader").html(curHtml);
    //    jQuery("#lblSumHeaderX").html("折扣后总计：0元");

    var curHtml = '<span>项目总数：</span><span class="come_loose_center_red">0个</span>';
    curHtml += '<span>原价总计：</span><span class="come_loose_center_red">￥0元</span>';
    curHtml += '<span>折扣总计：</span><span class="come_loose_center_red">￥0元</span>';
    curHtml += '<span>折扣后总计：</span><span class="come_loose_center_red">￥0元</span>';
    var waitChargeText = " 待收：￥0元";
    jQuery("#lblWaitCharge").text(waitChargeText);
    jQuery("#divSumHeader").html(curHtml);
}


/*********汇总费用 End*******************/

/// <summary>
/// 新增团体信息
/// </summary>
function AddTeam() {
    var myDialog = art.dialog({ lock: true, fixed: true, opacity: 0.3 });
    jQuery.ajax({
        title: "新增",
        cache: false,
        url: '/System/Customer/TeamListOper.aspx?type=add&modelName=Team',
        success: function (data) {
            myDialog.content(data); // 填充对话框内容
        }
    });
}
/// <summary>
/// 在新页面中新增团体任务
/// </summary>
function AddNewTeamTask() {
    var myDialog = art.dialog({ lock: true, fixed: true, opacity: 0.3 });
    jQuery.ajax({
        title: "新增",
        cache: false,
        url: '/System/Customer/TeamTaskOper.aspx?type=add&modelName=Team',
        success: function (data) {
            myDialog.content(data); // 填充对话框内容
        }
    });
}


/*****************所有团体删除操作验证*******************************/
/// <summary>
/// 是否可以删除客户名单（通过体检号获取用户是否已经有项目在进行体检了[判断是否打印指引单]）
/// </summary>
function IsCanDeleteCustomer(ID_Customers) {

}