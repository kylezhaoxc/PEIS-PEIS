//客户信息模版
//修改人：黄兴茂
//修改日期：2013-07-25
//修改内容：主要修改了团体批量操作默认选择第一个客户名单的功能，批量删除、检索过滤掉重复的收费项目，新增了批量打印指引单功能，该功能目前暂不公开使用


var curFixed = 2; //有效数字
var curOper = "add";
var choiceBusSetText = "-请选择任务--";
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
$(document).ready(function () {
    var $auto = jQuery(".j-autoHeight");
    $auto.data("left", $auto.data("left") + jQuery("#ShowUserMenuDiv").height());
    //jQuery("#QueryExamListData").attr("data-left", );
    jQuery(".j-hiddenAway").hiddenAway();
    jQuery(".j-autoHeight").autoHeight();
    TableScrollByID("customerScrollTitle", "customerScrollControl");
});
function ResetInfo() {
    //    jQuery("#txtTeamTaskNameX").html('<option code="qzz" value="-1" selected="selected">' + choiceBusSetText + '</option>').attr("selected", true);
    //    jQuery("#txtTeamTaskNameX .select2-choice span").text(choiceBusSetText);
    jQuery("#tblTeamTaskGroupX tbody tr[id!='loading']").remove();
    jQuery("#tblTeamTaskGroupX tbody tr[id='loading']").show();
    jQuery("#tblTeamTaskGroupFee tbody tr[id!='loading']").remove();
    jQuery("#tblTeamTaskGroupFee tbody tr[id='loading']").show();
    jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading']").remove();
    jQuery("#tblTeamTaskGroupCustomerX tbody tr[id='loading']").show();
    ResetSumJG();
    BindTeamTaskGroup(null); //重置分组下拉选项
}
/// <summary>
/// 绑定团体任务信息
/// </summary>
function BindTeamTaskInfo(msg) {
    var defaultoptions = '<option code="qzz" value="-1" selected="selected">' + choiceBusSetText + '</option>';
    var options = "";
    jQuery("#txtTeamTaskNameX").html(defaultoptions);
    if (msg.dataList != null) {
        if (msg.dataList.length > 0) {
            jQuery(msg.dataList).each(function (i, item) {
                options += '<option code="' + item.InputCode + '" value="' + item.ID_Team + '">' + item.TeamName + '</option>';
            });
            if (options != "") {
                options = defaultoptions + options;
            }
            else {
                options = defaultoptions;
            }
        }
    }
    else {
        options = defaultoptions;
    }
    jQuery("#txtTeamTaskNameX").html(options);
    jQuery("#txtTeamTaskNameX").find("option[value='-1']").attr("selected", true);
    jQuery("#s2id_txtTeamTaskNameX .select2-choice span").text(choiceBusSetText);
}
/// <summary>
/// 通过团体、团体任务获取团体任务分组的客户名单信息
///action:调用的后台方法名称
///ID_Team:团体ID
///ID_TeamTask:团体任务ID
///ShowDataElementID:需要展示团体任务分组的客户名单信息的元素ID
/// </summary>
function LoadTeamTaskGroupCustomerInfo(ID_Team, ID_TeamTask, ShowDataElementID) {
    //获取第一个选中的客户名单，加载对应的收费项目信息
    var ID_Customer = "";
    jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
        ID_Customer = jQuery(this).parent().parent().attr("id_customer");
        return false;
    });
    if (ID_Customer != "") {
        LoadTeamtaskGroupCustFee(ID_Team, ID_TeamTask, ID_Customer, "tblTeamTaskGroupFee");
    }
    else {
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxTeamOper.aspx",
            cache: true,
            data: { action: "GetTeamTaskGroupCustInfoOfTeamBath",
                ID_Team: ID_Team,
                ID_TeamTask: ID_TeamTask,
                ID_TeamTaskGroupS: ""
            },
            cache: false,
            dataType: "json",
            success: function (msg) {
                BindTeamTaskGroup(msg.dataList0); //此处任务分组 xmhuang 2014-01-09
                //QueryPagesData(0)//此处绑定客户名单 xmhuang 2014-01-09
            }
        });
    }
}
/// <summary>
/// 绑定客户名单
///这里不需要显示验证信息
/// </summary>
function BindCustomerInfo(dataList) {
    var count = 0;
    var ShowDataElementID = "tblTeamTaskGroupCustomerX";
    //读取客户名单模版
    //    var xustomerTask = new ReadTeamTaskTemplate("tblTemplateTeamTaskGroupCustomer", ShowDataElementID);
    //    var customerListTheadTempleteContent = xustomerTask.teamTaskListTheadTempleteContent;
    //    var customerListBodyTempleteContent = xustomerTask.teamTaskListBodyTempleteContent; 
    var customerListBodyTempleteContent = jQuery('#tblTemplateTeamTaskGroupCustomer').html(); //团体任务模版Thead部分 
    var birthDate = new Date();
    var tempContent = "", htmlContent = "";
    var className = "";
    var title = "";
    var a = new Array();
    jQuery(dataList).each(function (i, item) {

        className = "NotExamStarted";
        //设置是否体检标记 Begin xmhuang 2013-10-16
        if (item.Is_ExamStarted == "True") {
            className = "ExamStarted";
            title = "该客户已开始体检";
        }
        //设置是否体检标记 End xmhuang 2013-10-16

        count++;
        //        tempContent = jQuery(customerListBodyTempleteContent.replace(/@type=text/gi, "")
        //                            .replace(/@type="text"/gi, "")
        //                            .replace(/@exist/gi, item.exist)
        //                            .replace(/@ID_TeamTaskGroup/gi, item.ID_TeamTaskGroup)
        //                            .replace(/@TeamTaskGroupName/gi, item.TeamTaskGroupName)
        //                            .replace(/@CustomerName/gi, item.CustomerName)
        //                            .replace(/@CustomerBirthDay/gi, item.CustomerBirthDay)
        //                            .replace(/@CustomerRoleName/gi, item.CustomerRoleName)
        //                            .replace(/@IDCard/gi, item.IDCard)
        //                            .replace(/@CustomerTel/gi, item.CustomerTel)
        //                            .replace(/@DepartmentX/gi, item.DepartmentX)
        //                            .replace(/@DepartmentA/gi, item.DepartmentA)
        //                            .replace(/@DepartmentB/gi, item.DepartmentB)
        //                            .replace(/@DepartmentC/gi, item.DepartmentC)
        //                            .replace(/@ErorrMessage/gi, "√")
        //                            .replace(/@ID_Customer/gi, item.ID_Customer)
        //                            .replace(/@GenderName/gi, item.GenderName)
        //                            .replace(/@MarriageName/gi, item.MarriageName)
        //                            .replace(/@RowNum/gi, count)
        //                            .replace(/@className/gi, className)
        //                            );

        a[count] = customerListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@title/gi, title)
                            .replace(/@exist/gi, item.exist)
                            .replace(/@ID_TeamTaskGroup/gi, item.ID_TeamTaskGroup)
                            .replace(/@TeamTaskGroupName/gi, item.TeamTaskGroupName)

                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@CustomerBirthDay/gi, item.CustomerBirthDay)

                            .replace(/@CustomerRoleName/gi, item.CustomerRoleName)


                            .replace(/@IDCard/gi, item.IDCard)

                            .replace(/@CustomerTel/gi, item.CustomerTel)

                            .replace(/@DepartmentX/gi, item.DepartmentX)


                            .replace(/@DepartmentA/gi, item.DepartmentA)


                            .replace(/@DepartmentB/gi, item.DepartmentB)


                            .replace(/@DepartmentC/gi, item.DepartmentC)

                            .replace(/@ErorrMessage/gi, "√")
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@RowNum/gi, count)
                            .replace(/@className/gi, className);


        //htmlContent += jQuery(tempContent)[0].outerHTML;
    });
    // htmlContent = a.join("");
    jQuery("#" + ShowDataElementID + " tbody tr[id='loading']").hide();
    jQuery("#" + ShowDataElementID + " tbody tr[id!='loading']").remove();
    jQuery("#" + ShowDataElementID + " tbody").prepend(a.join(""));
    jQuery("#" + ShowDataElementID + " tbody tr td input[name='txtCustomerName']").first().focus();
    //设置不可编辑
    jQuery('#' + ShowDataElementID + ' tbody tr[id!="loading"] input[type!="checkbox"]').attr("disabled", true);
    jQuery('#' + ShowDataElementID + ' tbody tr[id!="loading"] select').attr("disabled", true);

    //这里由于直接调用Click事件，其checked不会直接改变，但要绑定收费项目需要当前复选框为选中状态，所有使用了两次赋值
    //第一次是为click事件函数服务，第二次是为了改变其显示状态
    /* 移除默认选中第一个客户的功能
    jQuery("#" + ShowDataElementID + " tbody tr input[name='ItemCheckbox']").first().attr("checked", true);
    jQuery("#" + ShowDataElementID + " tbody tr input[name='ItemCheckbox']").first().click();
    jQuery("#" + ShowDataElementID + " tbody tr input[name='ItemCheckbox']").first().attr("checked", true);
    */
    SetTableRowStyle();
    // 判断表格是否存在滚动条,并设置相应的样式
    JudgeTableIsExistScroll_Custom();
}

/// <summary>
/// 绑定客户收费项目信息,这里值绑定当前选中的客户名单信息
/// </summary>
function ChangeCustomerCustInfo(obj) {
    //选中当前项则加载对应的收费项目，否则查找当前同级元素的选中项
    jQuery('#divSumHeader').text("收费项目");
    jQuery('#lblSumHeaderX').text("");
    jQuery('#tblTeamTaskGroupFee tbody tr[id!="loading"]').remove();
    jQuery('#tblTeamTaskGroupFee tbody tr[id="loading"]').show();
    var AllTeamTaskGroupID = "TaskGroup";
    var ID_Customer = "";
    //如果当前选中，则移除其它选中项
    var checked = obj.checked;
    if (checked) {
        AllTeamTaskGroupID = jQuery(obj).parent().parent().attr("id_teamtaskgroup");
        ID_Customer = jQuery(obj).parent().parent().attr("id_customer");
    }
    else {
        if (jQuery(obj).parent().parent().siblings().find("td input:checked").first().length > 0) {
            var PreVObj = jQuery(obj).parent().parent().siblings().find("td input:checked").first();
            AllTeamTaskGroupID = jQuery(PreVObj).parent().parent().attr("id_teamtaskgroup");
            ID_Customer = jQuery(PreVObj).parent().parent().attr("id_customer");
        }
    }
    //    if (AllTeamTaskGroupID == "TaskGroup" || AllTeamTaskGroupID == undefined || AllTeamTaskGroupID == "") {
    //        jQuery('#divSumHeader').text("收费项目");
    //        jQuery('#lblSumHeaderX').text("");
    //        jQuery('#tblTeamTaskGroupFee tbody tr[id!="loading"]').remove();
    //        jQuery('#tblTeamTaskGroupFee tbody tr[id="loading"]').show();
    //    }
    //    else {
    if (ID_Customer != "") {
        LoadTeamtaskGroupCustFee(jQuery("#txtTeamNameX").data("ID_Team"), jQuery("#txtTeamTaskNameX").data("ID_Team"), ID_Customer, "tblTeamTaskGroupFee");
    }
    //}
}
/// <summary>
/// 通过团体、团体任务获取团体任务分组的客户名单信息
///action:调用的后台方法名称
///ID_Team:团体ID
///ID_TeamTask:团体任务ID
///ID_Customer:客户体检号
///ShowDataElementID:需要展示团体任务分组的客户名单信息的元素ID
/// </summary>
function LoadTeamtaskGroupCustFee(ID_Team, ID_TeamTask, ID_Customer, ShowDataElementID) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxTeamOper.aspx",
        cache: true,
        data: { action: "GetTeamTaskGroupCustomerCustInfo",
            ID_Team: ID_Team,
            ID_TeamTask: ID_TeamTask,
            ID_TeamTaskGroupS: "",
            ID_Customer: ID_Customer
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            BindCustomerCustInfo(msg.dataList0, ShowDataElementID);
        }
    });
}
function BindCustomerCustInfo(dataList, ShowDataElementID) {
    //这里绑定收费项目
    var newContent = "";
    var count = 0;
    var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("tblTemplateTeamTaskGroupFee");
    var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
    var teamTaskGroupFeeListBodyTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
    jQuery(dataList).each(function (i, item) {
        count++;
        newContent += teamTaskGroupFeeListBodyTempleteContent.replace(/@type=text/gi, "")
        .replace(/@type="text"/gi, "")
        .replace(/@exist/gi, item.exist)
        .replace(/@PEPackageID/gi, "")
        .replace(/@class/gi, "")
        .replace(/@ID_TeamTaskGroup/gi, item.ID_TeamTaskGroup)
        .replace(/@TeamTaskGroupName/gi, item.TeamTaskGroupName)
        .replace(/@FeeWayName/gi, item.FeeWayName)
        .replace(/@ID_Fee/gi, item.ID_Fee)
        .replace(/@FeeName/gi, item.FeeName)
        .replace(/@Price/gi, parseFloat(item.Price).toFixed(2))
        .replace(/@Discount/gi, parseFloat(item.Discount).toFixed(2))
        .replace(/@FactPrice/gi, parseFloat(item.FactPrice).toFixed(2))
        .replace(/@userName/gi, item.userName)
        .replace(/@CustFeeChargeState/gi, item.CustFeeChargeState)
        .replace(/@ID_Section/gi, item.ID_Section)
        .replace(/@SectionName/gi, item.SectionName)
        .replace(/@RowNum/gi, count)
        .replace(/@date/gi, item.date);
    });
    if (newContent != '') {
        //存在数据则移除掉客户新增行，重新绑定保存行
        jQuery("#" + ShowDataElementID + " tbody tr[id!='loading']").remove();
        jQuery("#" + ShowDataElementID + " tbody").append(newContent);
        newContent = "";
        //DoScrollToBottom();
        SetTableRowStyle();
    }
    else {
        //隐藏提示tr
        jQuery("#" + ShowDataElementID + " tbody tr[id!='loading']").remove();
        jQuery("#" + ShowDataElementID + " tbody tr[id='loading'] td").show();
        //jQuery("#" + ShowDataElementID + " tbody tr[id='loading'] td").text("没有任何数据，请您维护！...");
    }
    SumJG(); //计算总计
    jQuery("#" + ShowDataElementID + " tbody tr[id='loading']").hide();
    // 判断表格是否存在滚动条,并设置相应的样式
    JudgeTableIsExistScroll();
}

function BusFeeInfo(BusFee, returnValue) {
    this.BusFee = BusFee;
    this.ReturnValue = returnValue;
}
/// <summary>
///批量新增项目
/// </summary>
function AddTeamTaskGroupFee_Batch() {
    jQuery("#showBusFeeItem tr td input:checked").removeAttr("checked");
    //判断是否有勾选项
    var checkedObj = jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked");
    var checkedObjCount = jQuery(checkedObj).length;
    if (checkedObjCount == 0) {
        ShowSystemDialog("对不起，请您勾选需要批量新增项目的客户！");
        return false;
    }
    var dialog = art.dialog({ id: 'N3690', lock: true, fixed: true, opacity: 0.3, content: "正在加载数据，请稍候..." });
    // jQuery ajax   
    jQuery.ajax({
        type: "POST",
        cache: false,
        url: '/System/Customer/TeamBatch_BusFeeOper.aspx?type=addbatch&modelName=TeamBatch_BusFeeOper',
        success: function (data) {
            curOper = "add";
            dialog.content(data);
        }
    });
}
/// <summary>
///批量删除客户共有收费项目
/// </summary>
function DeleteTeamTaskGroupFee_Batch() {
    jQuery("#showBusFeeItem tr td input:checked").removeAttr("checked");
    //判断是否有勾选项
    var checkedObj = jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked");
    var checkedObjCount = jQuery(checkedObj).length;
    if (checkedObjCount == 0) {
        ShowSystemDialog("对不起，请您勾选需要批量新增项目的客户！");
        return false;
    }
    //获取当前选中的客户体检号
    var ID_Customers = "";
    jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
        ID_Customers = "'" + jQuery(this).parent().parent().attr("id_customer") + "',";
    });
    var ID_Team = jQuery("#txtTeamNameX").data("ID_Team");
    var ID_TeamTask = jQuery("#txtTeamTaskNameX").data("ID_Team");
    var dialog = art.dialog({ id: 'N3690', lock: true, fixed: true, opacity: 0.3, content: "正在加载数据，请稍候..." });
    jQuery.ajax({
        type: "POST",
        url: "/System/Customer/TeamBatch_BusFeeOper.aspx",
        cache: false,
        data: {
            type: "delbatch",
            modelName: "TeamBatch_BusFeeOper",
            ID_Team: ID_Team,
            ID_TeamTask: ID_TeamTask,
            ID_TeamTaskGroupS: "",
            ID_Customers: ID_Customers
        },
        success: function (data) {
            curOper = "delete";

            dialog.content(data);

        }
    });
}
/**********全选团体任务分组收费项目***********/
function checkAllChildren(obj) {
    jQuery("#tblExtern tbody tr td input[name='ItemCheckboxX']").each(function () {
        jQuery(this).attr('checked', obj.checked);
    })
}

function BindCutomerBusFee(datalist) {
    var TemplateBusFee = "TemplateTeamBatchSearch";
    var teamTaskListBodyTempleteContent = jQuery("#TemplateTeamBatchSearch tbody").html();
    var NewContent = "";
    var TempContent = "";
    jQuery(datalist).each(function (i, item) {
        TempContent = jQuery(teamTaskListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@CustFeeChargeState/gi, item.CustFeeChargeState)
                            .replace(/@ID_Fee/gi, item.ID_Fee)
                            .replace(/@FeeName/gi, item.FeeName)
                            .replace(/@PEPackageID/gi, item.PEPackageID)
                            .replace(/@userName/gi, item.userName)
                            .replace(/@ID_Section/gi, item.ID_Section)
                            .replace(/@Price/gi, item.Price)
                            .replace(/@InputCode/gi, item.InputCode)
                            .replace(/@InputCode/gi, "")
                            );
        if (item.IsChecked == "1") {
            jQuery(TempContent).find("td input[name='ItemCheckboxX']").attr("checked", true);
        }
        NewContent += jQuery(TempContent)[0].outerHTML;
    });
    if (NewContent != "") {
        jQuery("#teamBatchSearch tbody").html(NewContent);
    }
    else {
        jQuery("#teamBatchSearch tbody").html("<tr><td collspan='5'>对不起，没有找到任何匹配项！</td></tr>");
    }
}
/*************键盘操作事件：主要为上下左右键，适用于table******************/
function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    //    if (curEvent.keyCode == 13)//回车事件
    //    {
    //        //如果是在搜索中
    //        if (id == "txtSearch") {
    //            SureAdd();
    //        }
    //    }
    if (id == "txtSearch" && (curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40)) {
        keyMove(document.getElementById(id), curEvent); return;
    }
    return false;
}
function keyMove(item, event) {
    var elementID = "tblExtern";
    var maxX = document.getElementById(elementID).rows[0].cells.length; //计算表格有列数
    var maxY = document.getElementById(elementID).rows.length; //计算表格行数
    var objTable = document.getElementById(elementID); //获取table
    var c = item.parentNode.cellIndex; //获取当前列的下标，因为列中有文本框，取父级
    var row = item.parentNode; //获取当前行的下标
    while (row.tagName != "TR") row = row.parentNode;
    var r = row.rowIndex; var x = r; var y = c;

    if (event.keyCode == 40) {
        //如果是在搜索文本中，则设置当前r=-1;
        if (item.id == "txtSearch" || item.id == "chbAll1") {
        }
        else {
            if (r < maxY - 1) {
                x = r + 1;
                y = c;
            }
        }
    }
    if (event.keyCode == 38) {
        if (r > 0) {
            x = r - 1;
            y = c;
        }
        else {
            if (c == 0) {
                document.getElementById("chbAll1").focus();
            }
            else {
                document.getElementById("txtSearch").focus();
            }
            return false;
        }
    }
    if (event.keyCode == 39) {

        if (item.id == "chbAll1") {
            document.getElementById("txtSearch").focus();
            return false;
        }
        else {
            if (c <= maxX - 2) {
                x = r;
                y = c + 1;
            }
        }
    }
    if (event.keyCode == 37) {
        if (item.id == "txtSearch") {
            document.getElementById("chbAll1").focus();
            return false;
        }
        else {
            if (c > 0) {
                x = r;
                y = c - 1;
            }
        }

    }
    if (objTable.rows[x].style.display == "none")
        return;
    if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 13) {
        if (objTable.rows[x].cells[y] != undefined) {
            if (objTable.rows[x].cells[y].children[0] != undefined) {

                //回车默认选中当前行
                if (event.keyCode == 13) {
                    objTable.rows[x].cells[0].children[0].checked = !objTable.rows[x].cells[0].children[0].checked;
                    if (objTable.rows[x].cells[0].children[0].name == "checkAllX") {
                        checkAllChildren(objTable.rows[x].cells[0].children[0]);
                    }
                    document.getElementById("txtSearch").focus();
                    document.getElementById("txtSearch").select();
                }
                else {
                    if (objTable.rows[x].cells[y].children[0].type != "button") {
                        objTable.rows[x].cells[y].children[0].blur();
                        objTable.rows[x].cells[y].children[0].focus();
                    }
                }
            }
        }
    }
    //return false;
}
function ResetSearch() {
    jQuery("#chbAll1").attr("checked", false);
    document.getElementById("txtSearch").focus();
    document.getElementById("txtSearch").select();
    document.getElementById("txtSearch").value = " ";
}

/**************套餐外新增功能完善 Begin****************************/

/***********确认新增********************/
function SureAddCurrentRow(obj) {
    var ID_Customers = "", ID_FeeS = "";
    var ID_Team = jQuery("#txtTeamNameX").data("ID_Team");
    var ID_TeamTask = jQuery("#txtTeamTaskNameX").data("ID_Team")
    jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
        ID_Customers += jQuery(this).parent().parent().attr("id_customer") + ",";
    });
    ID_FeeS += jQuery(obj).parent().parent().attr("id");
    //请求后台删除
    //这里提交后台入库处理
    if (ID_Customers != "") {
        if (curOper == "add") {
            //获取当前分组ID()和收费ID
            var Arr = "", AllItem = "", TeampItem = "", ID_Fee = "", FeeItemName = "", PEPackageID = "", ID_Section = "", Price = 0, Discount = jQuery.trim(jQuery("#txtXMZK").val()), FactPrice = 0, ID_FeeType = document.getElementById("slFeeWay").options[document.getElementById("slFeeWay").selectedIndex].value; //付费方式
            Arr = ID_Customers.split(",");
            for (var c = 0; c < Arr.length; c++) {
                if (Arr[c] != "") {
                    ID_Fee = jQuery(obj).parent().parent().attr("id");
                    FeeItemName = jQuery(obj).parent().parent().attr("feename");
                    PEPackageID = jQuery(obj).parent().parent().attr("id_set");
                    ID_Section = jQuery(obj).parent().parent().attr("id_section");
                    Price = jQuery(obj).parent().parent().attr("price");
                    FactPrice = parseFloat(Price) * parseFloat(Discount) / 100;
                    TeampItem = Arr[c] + "_" + ID_Fee + "_" + FeeItemName + "_" + Price + "_" + Discount + "_" + FactPrice + "_" + ID_FeeType + "|";
                    AllItem += TeampItem;
                }
            }
            if (AllItem != "") {
                //这里提交后台入库处理
                jQuery.ajax({
                    type: "POST",
                    url: "/Ajax/AjaxTeamOper.aspx",
                    cache: true,
                    data: { action: "SaveCustomerCustFeeOfBatch",
                        BatchOper: "add",
                        ID_Customers: ID_Customers,
                        ID_Team: ID_Team,
                        ID_TeamTask: ID_TeamTask,
                        ID_TeamTaskGroupS: "",
                        AllItem: AllItem
                    },
                    cache: false,
                    dataType: "json",
                    success: function (msg) {
                        //ShowSystemDialog(msg.Message);
                        //这里绑定所有的团体名单信息
                        LoadTeamTaskGroupCustomerInfo(ID_Team, ID_TeamTask, "tblTeamTaskGroupCustomerX");
                    }
                });
            }
        }
        else if (curOper == "delete") {
            //请求后台删除
            //这里提交后台入库处理
            if (ID_Customers != "" && ID_FeeS != "") {
                jQuery.ajax({
                    type: "POST",
                    url: "/Ajax/AjaxTeamOper.aspx",
                    cache: true,
                    data: { action: "SaveCustomerCustFeeOfBatch",
                        BatchOper: "delete",
                        ID_Team: ID_Team,
                        ID_TeamTask: ID_TeamTask,
                        ID_TeamTaskGroupS: "",
                        ID_Customers: ID_Customers,
                        ID_FeeS: ID_FeeS
                    },
                    cache: false,
                    dataType: "json",
                    success: function (msg) {
                        //ShowSystemDialog(msg.Message);
                        //这里绑定所有的团体名单信息
                        LoadTeamTaskGroupCustomerInfo(ID_Team, ID_TeamTask, "tblTeamTaskGroupCustomerX");
                    }
                });
            }
            else {
            }
        }
    }
    jQuery(obj).parent().parent().remove();
    SumJG(); //计算总计
    ResetSearch();
    DoClose();
}
function SureAdd() {
    if (jQuery("#showBusFeeItem tr td input[type='checkbox'][name='ItemCheckboxX']:checked").length == 0) {
        DoClose();
        return false;
    }

    var InfoTitle = "", Tips = ""; //提示信息
    if (curOper == "add") {
        //判断是否包含已进行体检的客户 xmhuang 20140507
        //获取勾选的体检号
        var ID_Customers = "", errorMsg = "";
        jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
            ID_Customers += "'" + jQuery(this).parent().parent().attr("id_customer") + "',";
        });
        //判断勾选的体检号是否存在已经开始体检的客户，如果存在则不允许新增
        var customerInfo = GetGuideSheetPrintedCustomer(ID_Customers);
        //判断选择的客户是否已进行体检
        jQuery(customerInfo.dataList).each(function (i, item) {
            if (ID_Customers.search("'" + item.ID_Customer + "'") > -1) {
                errorMsg += item.CustomerName + "[" + item.ID_Customer + "]</br>";
            }
        });

        if (errorMsg != "") {
            errorMsg = errorMsg.substr(0, errorMsg.length - 5);
            ShowSystemDialog("以下客户已开始体检,不允许批量新增收费项目</br>" + errorMsg + " ");
            return false;
        }

        InfoTitle = "您确认要将选择的数据添加到客户的收费项目中吗？";
        Tips = "已成功新增收费项目：";
    }
    else if (curOper == "delete") {
        InfoTitle = "您确认要将选择的数据从客户的收费项目中删除吗？";
        Tips = "已成功删除收费项目：";
    }

    var dialog = art.dialog({
        id: 'artDialogIDRegisterDate',
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '温馨提示',
        content: InfoTitle,
        button: [{
            name: '取消',
            callback: function () {
                return true;
            }
        }, {
            name: '确定',
            callback: function () {
                //清除选中项目
                SelectedCustFee = "";
                var ID_Customers = "", ID_FeeS = "";
                jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
                    ID_Customers += jQuery(this).parent().parent().attr("id_customer") + ",";
                });
                var ID_Team = jQuery("#txtTeamNameX").data("ID_Team");
                var ID_TeamTask = jQuery("#txtTeamTaskNameX").data("ID_Team")
                //请求后台删除
                //这里提交后台入库处理
                if (ID_Customers != "") {
                    if (curOper == "add") {
                        //获取当前分组ID()和收费ID
                        var Arr = "", AllItem = "", TeampItem = "", ID_Fee = "", FeeItemName = "", PEPackageID = "", ID_Section = "", Price = 0, Discount = jQuery.trim(jQuery("#txtXMZK").val()), FactPrice = 0, ID_FeeType = document.getElementById("slFeeWay").options[document.getElementById("slFeeWay").selectedIndex].value; //付费方式
                        Arr = ID_Customers.split(",");
                        for (var c = 0; c < Arr.length; c++) {
                            if (Arr[c] != "") {
                                jQuery("#tblExtern tbody tr[id!='loading'] td input:checked").each(function () {
                                    if (jQuery(this).attr("id") != "chbAll1") {
                                        ID_Fee = jQuery(this).parent().parent().attr("id");
                                        FeeItemName = jQuery(this).parent().parent().attr("feename");
                                        PEPackageID = jQuery(this).parent().parent().attr("id_set");
                                        ID_Section = jQuery(this).parent().parent().attr("id_section");
                                        Price = jQuery(this).parent().parent().attr("price");
                                        FactPrice = parseFloat(Price) * parseFloat(Discount) / 100;
                                        TeampItem = Arr[c] + "_" + ID_Fee + "_" + FeeItemName + "_" + Price + "_" + Discount + "_" + FactPrice + "_" + ID_FeeType + "|";
                                        AllItem += TeampItem;
                                        Tips += FeeItemName + ",";
                                        //jQuery(this).parent().parent().remove();
                                    }
                                });
                            }
                        }
                        //移除选中项
                        jQuery("#tblExtern tbody tr[id!='loading'] td input:checked").each(function () {
                            if (jQuery(this).attr("id") != "chbAll1") {
                                jQuery(this).parent().parent().remove();
                            }
                        });
                        if (AllItem != "") {
                            //这里提交后台入库处理
                            jQuery.ajax({
                                type: "POST",
                                url: "/Ajax/AjaxTeamOper.aspx",
                                cache: true,
                                data: { action: "SaveCustomerCustFeeOfBatch",
                                    BatchOper: "add",
                                    ID_Team: ID_Team,
                                    ID_TeamTask: ID_TeamTask,
                                    ID_TeamTaskGroupS: "",
                                    ID_Customers: ID_Customers,
                                    AllItem: AllItem
                                },
                                cache: false,
                                dataType: "json",
                                success: function (msg) {
                                    //ShowSystemDialog(msg.Message);
                                    //这里绑定所有的团体名单信息
                                    LoadTeamTaskGroupCustomerInfo(ID_Team, ID_TeamTask, "tblTeamTaskGroupCustomerX");
                                }
                            });
                        }
                    }
                    else if (curOper == "delete") {
                        var ID_Team = jQuery("#txtTeamNameX").data("ID_Team");
                        var ID_TeamTask = jQuery("#txtTeamTaskNameX").data("ID_Team");
                        jQuery("#tblExtern tbody tr[id!='loading'] td input:checked").each(function () {
                            if (jQuery(this).attr("id") != "chbAll1") {
                                FeeItemName = jQuery(this).parent().parent().attr("feename");
                                ID_FeeS += "'" + jQuery(this).parent().parent().attr("id") + "',";
                                jQuery(this).parent().parent().remove();
                                Tips += FeeItemName + ",";
                            }
                        });

                        //请求后台删除
                        //这里提交后台入库处理
                        if (ID_Customers != "" && ID_FeeS != "") {
                            jQuery.ajax({
                                type: "POST",
                                url: "/Ajax/AjaxTeamOper.aspx",
                                cache: true,
                                data: { action: "SaveCustomerCustFeeOfBatch",
                                    BatchOper: "delete",
                                    ID_Team: ID_Team,
                                    ID_TeamTask: ID_TeamTask,
                                    ID_TeamTaskGroupS: "",
                                    ID_Customers: ID_Customers,
                                    ID_FeeS: ID_FeeS
                                },
                                cache: false,
                                dataType: "json",
                                success: function (msg) {
                                    //ShowSystemDialog(msg.Message);
                                    //这里绑定所有的团体名单信息
                                    LoadTeamTaskGroupCustomerInfo(ID_Team, ID_TeamTask, "tblTeamTaskGroupCustomerX");
                                }
                            });
                        }
                        else {
                        }
                    }
                }
                SumJG(); //计算总计
                ResetSearch();

                //显示提示信息
                if (Tips.length > 0) {
                    art.dialog.tips(Tips.substring(0, Tips.length - 1), 2);
                } DoClose();
                return true;
            }, focus: true
        }]

    }).lock();

}
function OnKeyUp() {
    SearchBusFee();
}
function DoClose() {
    jQuery("#showBusFeeItem").empty();
    jQuery("#showBusFee").hide();
    if (startState == 1) //如果折叠的则需要
    {
        jQuery("#baseInfo").click();
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
        startState = 0;
    }
    DoScrollToBottom();
    // 判断表格是否存在滚动条,并设置相应的样式
    JudgeTableIsExistScroll();
}
function DoSelectAll() {
    jQuery("#txtSearch").select();
}
function DoScrollToBottom() {
    //window.scrollTo(0, document.body.scrollHeight - 100); 
    jQuery("#divtblList").scrollTop(jQuery("#divtblList")[0].scrollHeight);
}
//绑定搜索项获取光标即选中
function BindSelect() {
    jQuery("#showBusFeeItem input[type='text']").focus(function () {
        this.select();
    });

}
var SelectedCustFee = ""; //记录当前选中的收费项目
/***********关键字搜索
备注：将搜索到的内容放到最前面显示 由模糊匹配改成精确匹配
******************/
function SearchBusFee() {
    var data = {};
    var InputCode = jQuery.trim(jQuery("#txtSearch").val());
    var curEvent = window.event || e;
    if (curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40 || curEvent.keyCode == 13) {
        return false;
    }
    //获取所有的选中项目 Begin
    SelectedCustFee = "";
    jQuery("#tblExtern tbody tr[id!='loading'] td input:checked").each(function () {
        SelectedCustFee += jQuery(this).parent().parent().attr("id") + ",";
    });
    //SelectedCustFee = SelectedCustFee.substring(0, SelectedCustFee.length - 1);
    //获取所有的选中项目 End
    //    if (curEvent.keyCode == 13)//确定按钮，回车事件
    //    {

    //        if (jQuery("#tblExtern tbody tr[id!='loading'] td input:checked").length > 0) {
    //            SureAdd();
    //        }
    //    }
    //jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:blue;text-align:center;">正在加载，请稍候...</td></tr>');
    if (curOper == "add") {
        data = { url: "/Ajax/AjaxRegiste.aspx", Gender: "-1", CustFeeID: "", InputCode: InputCode, action: 'SearchBusFeeByCustFeeID', SelectedFee: SelectedCustFee };
    }
    else if (curOper == "delete") {
        var ID_Team = jQuery("#txtTeamNameX").data("ID_Team");
        var ID_TeamTask = jQuery("#txtTeamTaskNameX").data("ID_Team");
        //获取当前选中的客户体检号
        var ID_Customers = "", ID_TeamTaskGroupS = "";
        jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
            ID_Customers += "'" + jQuery(this).parent().parent().attr("id_customer") + "',";
            ID_TeamTaskGroupS += "'" + jQuery(this).parent().parent().attr("id_teamtaskgroup") + "',";
        });
        data = {
            url: "/Ajax/AjaxRegiste.aspx",
            type: "delbatch",
            modelName: "TeamBatch_BusFeeOper",
            ID_Team: ID_Team,
            ID_TeamTask: ID_TeamTask,
            ID_TeamTaskGroupS: "",
            ID_Customers: ID_Customers,
            InputCode: InputCode,
            action: "SearchCustomerUnionBusFee",
            SelectedFee: SelectedCustFee
        };
    }

    //提交后台处理
    jQuery.ajax({
        type: "POST",
        url: data.url,
        cache: false,
        data: data,
        dataType: "json",
        success: function (msg) {
            BindCutomerBusFee(msg, "");
        }
    });
}
function BindCutomerBusFee(msg, GenderName) {
    var newContent = "";
    jQuery("#showBusFeeItem").data('ExternList', msg); //缓存数据项到divExternList
    jQuery("#showBusFeeItem").empty(''); //清除显示项目
    var allItem = ",";
    if (msg.dataList.length > 0) {
        //由于在点击新增的时候已经过滤掉存在的CustFeeID，这里毋须再次过滤
        jQuery(msg.dataList).each(function (i, item) {

            if (allItem.search("," + item.ID_Fee + ",") == -1) {
                allItem += item.ID_Fee + ",";
                if (item.IsChecked == 1)//选中项和全字匹配项
                {
                    newContent += '<tr name="trExternItem" class="externSelect" CustFeeChargeState="0" code="' + item.InputCode + '" id="' + item.ID_Fee + '" userName="' + item.userName + '" Price="' + item.Price + '" Discount="' + DisCountRate + '" FactPrice="' + item.FactPrice + '" date="' + item.date + '" FeeName="' + item.FeeName + '" name="busList" exist="0" ID_Section="' + item.ID_Section + '" SectionName="' + item.SectionName + '">';
                    newContent += '<td><input onkeydown="keyMove(this, event)" id="Checkbox_' + item.ID_Fee + '" name="ItemCheckboxX" type="checkbox" checked="checked"></td>';
                }
                else if (item.IsChecked == 2)//以InputCode开头的
                {
                    newContent += '<tr name="trExternItem" class="externCanFocus" CustFeeChargeState="0" code="' + item.InputCode + '" id="' + item.ID_Fee + '" userName="' + item.userName + '" Price="' + item.Price + '" Discount="' + DisCountRate + '" FactPrice="' + item.FactPrice + '" date="' + item.date + '" FeeName="' + item.FeeName + '" name="busList" exist="0" ID_Section="' + item.ID_Section + '" SectionName="' + item.SectionName + '">';
                    newContent += '<td><input onkeydown="keyMove(this, event)" id="Checkbox_' + item.ID_Fee + '" name="ItemCheckboxX" type="checkbox"></td>';
                }
                else {
                    newContent += '<tr name="trExternItem" class="trExternItem" CustFeeChargeState="0" code="' + item.InputCode + '" id="' + item.ID_Fee + '" userName="' + item.userName + '" Price="' + item.Price + '" Discount="' + DisCountRate + '" FactPrice="' + item.FactPrice + '" date="' + item.date + '" FeeName="' + item.FeeName + '" name="busList" exist="0" ID_Section="' + item.ID_Section + '" SectionName="' + item.SectionName + '">';
                    newContent += '<td><input onkeydown="keyMove(this, event)" id="Checkbox_' + item.ID_Fee + '" name="ItemCheckboxX" type="checkbox"></td>';
                }
                newContent += '<td><input name="textExternItem" onkeydown="keyMove(this, event)" type="text"  readonly="readonly" style="border:0px; width:100%;" value="' + item.FeeName + '"  id="xmmc_' + item.ID_Fee + '"></td>';
                newContent += '<td><input name="textExternItem" onkeydown="keyMove(this, event)" type="text"  readonly="readonly" style="border:0px; width:100%;" value="' + item.InputCode + '"  id="inputCode_' + item.ID_Fee + '"></td>';
                newContent += '</tr>';
            }
        });
    }
    else {
        jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有找到适合的收费项目！</td></tr>');
        jQuery("#showBusFee").show();
    }
    if (newContent == "") {
        jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有找到适合的收费项目！</td></tr>');
        jQuery("#showBusFee").show();
    }
    else {
        jQuery("#showBusFeeItem").html(newContent);
        //        if (jQuery(".externCanFocus").first().length > 0) {
        //            jQuery(".externCanFocus td input[name='textExternItem']").first().focus(); //设置以InputCode开始的项光标
        //            jQuery(".externCanFocus td input[name='textExternItem']").first().select(); //设置以InputCode开始的项为选中项
        //        }
        newContent = "";
        BindSelect();
    }
}


//判断任务分组是否可以禁用 xmhuang 20140507
function GetGuideSheetPrintedCustomer(ID_Customer) {
    var allISDeleteDT = ""; //当前团体所有不可用于删除的团体ID，团体任务，团体任务分组，体检号
    var flag = false;
    jQuery.ajax({
        type: "POST",
        async: false,
        url: "/Ajax/AjaxTeamOper.aspx",
        data: { action: "GetGuideSheetPrintedCustomer", ID_Customer: ID_Customer },
        cache: false,
        dataType: "json",
        success: function (msg) {
            // 检查Ajax返回数据的状态等 20140504 by xmhuang 
            msg = CheckAjaxReturnDataInfo(msg);
            if (msg == null || msg == "") {
                return;
            }
            allISDeleteDT = msg;
        }
    });
    return allISDeleteDT;
}
var startState = 0; //客户名单折叠状态
/***********批量新增
备注：批量新增由于是多用户，不需要过滤项目
******************/
function DoAddX(obj) {
    //判断是否有勾选项
    var checkedObj = jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked");
    var checkedObjCount = jQuery(checkedObj).length;
    if (checkedObjCount == 0) {
        ShowSystemDialog("对不起，请您勾选需要批量操作的客户！");
        return false;
    }
    var data = {};
    var ID_Customers = "", ID_TeamTaskGroupS = ""; var errorMsg = "";
    if (obj.id == "btnBatchAdd") {

        //获取勾选的体检号
        jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
            ID_Customers += "'" + jQuery(this).parent().parent().attr("id_customer") + "',";
        });
        //判断勾选的体检号是否存在已经开始体检的客户，如果存在则不允许新增
        var customerInfo = GetGuideSheetPrintedCustomer(ID_Customers);
        //判断选择的客户是否已进行体检
        jQuery(customerInfo.dataList).each(function (i, item) {
            if (ID_Customers.search("'" + item.ID_Customer + "'") > -1) {
                errorMsg += item.CustomerName + "[" + item.ID_Customer + "]</br>";
            }
        });

        if (errorMsg != "") {
            errorMsg = errorMsg.substr(0, errorMsg.length - 5);
            ShowSystemDialog("以下客户已开始体检,不允许批量新增收费项目</br>" + errorMsg + " ");
            return false;
        }
        //如果是批量新增
        curOper = "add";
        data = { url: "/Ajax/AjaxRegiste.aspx", Gender: "-1", CustFeeID: "", action: 'GetBusFeeNotINCustFeeID' };
    }
    else if (obj.id == "btnBatchDelete") {
        //如果是批量删除
        curOper = "delete";
        var ID_Team = jQuery("#txtTeamNameX").data("ID_Team");
        var ID_TeamTask = jQuery("#txtTeamTaskNameX").data("ID_Team");
        //获取当前选中的客户体检号
        jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
            ID_Customers += "'" + jQuery(this).parent().parent().attr("id_customer") + "',";
            ID_TeamTaskGroupS += "'" + jQuery(this).parent().parent().attr("id_teamtaskgroup") + "',";
        });
        data = {
            url: "/Ajax/AjaxTeamOper.aspx",
            type: "delbatch",
            modelName: "TeamBatch_BusFeeOper",
            ID_Team: ID_Team,
            ID_TeamTask: ID_TeamTask,
            ID_TeamTaskGroupS: "",
            ID_Customers: ID_Customers,
            action: "GetCustomerUnionBusFee"
        };
    }
    jQuery("#txtSearch").val(''); //设置关键字为空
    var HasData = true, newContent = "";
    jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:blue;text-align:center;">正在加载，请稍候...</td></tr>');
    var newContent = ''; //用于存放html
    startState = 0;
    if (jQuery("#box").is(":visible")) {
        startState = 1;
    }
    if (startState == 1) //如果折叠的则需要隐藏
    {
        jQuery("#baseInfo").click();
        // 判断表格是否存在滚动条,并设置相应的样式
        JudgeTableIsExistScroll();
    }
    //ajax请求：由于字符在get请求中超长，这里必须使用post方式提交请求
    jQuery.ajax({
        type: "POST",
        cache: false,
        url: data.url,
        data: data,
        dataType: "json",
        success: function (msg) {
            jQuery("#showBusFeeItem").data('ExternList', msg); //缓存数据项到divExternList
            jQuery("#showBusFeeItem").empty(''); //清除显示项目
            if (msg.dataList.length > 0) {
                //由于在点击新增的时候已经过滤掉存在的CustFeeID，这里毋须再次过滤
                jQuery(msg.dataList).each(function (i, item) {

                    newContent += '<tr name="trExternItem" class="trExternItem" CustFeeChargeState="0" code="' + item.InputCode + '" id="' + item.ID_Fee + '" userName="' + item.userName + '" Price="' + item.Price + '" Discount="10" FactPrice="' + item.FactPrice + '" date="' + item.date + '" FeeName="' + item.FeeName + '" name="busList" exist="0" ID_Section="' + item.ID_Section + '" SectionName="' + item.SectionName + '">' +
                        '<td><input onkeydown="keyMove(this, event)" id="Checkbox_' + item.ID_Fee + '" name="ItemCheckboxX" type="checkbox" parentid="1909"></td>' +
                        '<td><input name="textExternItem" onkeydown="keyMove(this, event)" type="text"  readonly="readonly" style="border:0px; width:100%;" value="' + item.FeeName + '"  id="xmmc_' + item.ID_Fee + '"></td>' +
                        '<td><input name="textExternItem" onkeydown="keyMove(this, event)" type="text"  readonly="readonly" style="border:0px; width:100%;" value="' + item.InputCode + '"  id="inputCode_' + item.ID_Fee + '"></td>' +
                    '</tr>';
                });
            }
            else {
                jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有找到合适的收费项目！</td></tr>');
                jQuery("#showBusFee").show();
            }
        },
        complete: function () {
            if (newContent == "") {
                jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有找到合适的收费项目！</td></tr>');
                jQuery("#showBusFee").show();
            }
            else {
                jQuery("#showBusFeeItem").append(newContent);
                BindSelect();
            }
            newContent = "";
            jQuery("#showBusFee").show(); 
            // 判断表格是否存在滚动条,并设置相应的样式
            JudgeTableIsExistScroll();
            
            DoScrollToBottom();
            jQuery("#txtSearch").focus();
        }
    });

}
/**************套餐外新增功能完善 End****************************/

/****************************批量打印 Begin*****************************/
var IsPrintedCustomer = "";
/// <summary>
///批量打印
/// </summary>
function PrintReport() {
    if (jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").length == 0) {
        jQuery("#lblErrorMessage").text("(您尚未勾选需要打印的报告或列表中不存在待打印报告！)");
        return false;
    }
    else {

        FastReport.SetUserInfo(ID_User, UserName);
        var ID_Customer = "", CustomerName = "";
        //var lblErrorMessage
        jQuery("#tblTeamTaskGroupCustomerX tbody tr[id!='loading'] td input:checked").each(function () {
            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
                CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='txtCustomerName']").text());
                jQuery("#lblErrorMessage").text("(正在打印[" + CustomerName + "]的体检报告...)");
                FastReport.GenerateCustomerGuide(ID_Customer, "guidesheet.frx", 0);
                jQuery("#lblErrorMessage").text("(已完成[" + CustomerName + "]的体检报告打印。)");
                //jQuery(this).parent().parent().remove();
            }
        });
        jQuery("#lblErrorMessage").text("(报告打印完毕。)");
        jQuery("#lblErrorMessage").text("");
    }
}
/****************************批量打印 End*****************************/

/********************分页绑定客户名单 Begin***********************/
var tempOperPageCount = 0;
var tempOldtotalCount = 0;
function pageselectCallback(page_index, jq) {
    if (tempOperPageCount > 0) {
        tempOperPageCount++;
        QueryPagesData(page_index);
    }
    tempOperPageCount++;
    return false;
}
function DoSearch() {
    tempOperPageCount = 0;
    QueryPagesData(0); //重新按照新输入的条件进行查询
}
/// <summary>
///通过团体、团体任务分页查询其对应的客户名单
/// </summary>
function QueryPagesData(pageIndex) {
    var optInit = getOptionsFromForm05();
    var totalCount = 0;
    //    var ID_Team = document.getElementById("txtTeamNameX").options[document.getElementById("txtTeamNameX").selectedIndex].value; //团体ID
    //    var ID_TeamTask = document.getElementById("txtTeamTaskNameX").options[document.getElementById("txtTeamTaskNameX").selectedIndex].value; //任务ID

    var ID_Team = jQuery('#idSelectTeam').val();
    if (ID_Team == -1 || ID_Team == "") {
        ShowSystemDialog("请选择团体!");
        return false;

    }
    var ID_TeamTask = jQuery('#idSelectTeamTask').val();
    if (ID_TeamTask == -1 || ID_TeamTask == "") {
        ShowSystemDialog("请选择团体任务!");
        return false;

    }
    var ID_TeamTaskGroup = document.getElementById("slTeamTaskGroup").selectedIndex == -1 ? "" : document.getElementById("slTeamTaskGroup").options[document.getElementById("slTeamTaskGroup").selectedIndex].value; //分组ID
    var ID_Customer = jQuery.trim(jQuery('#txtSFZ').val());  //身份证或体检号
    var IDCard = ID_Customer;
    var IsCustomerID = 0;


    //判断是否是体检号
    if (isCustomerExamNo(ID_Customer)) {
        IsCustomerID = 1;
        IDCard = "";
    }
    else {
        ID_Customer = "";
    }
    var CustomerName = jQuery.trim(jQuery('#txtCustomerName').val()); //客户姓名
    if (ID_Team == -1) {
        ShowSystemDialog("对不起，请选择团体！");
        return false;
    }
    if (ID_TeamTask == -1) {
        ShowSystemDialog("对不起，请选择任务！");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxTeamOper.aspx",
        data: { IDCard: IDCard, ID_Customer: ID_Customer, CustomerName: CustomerName, ID_Team: ID_Team, ID_TeamTask: ID_TeamTask, ID_TeamTaskGroup: ID_TeamTaskGroup, pageIndex: pageIndex, pageSize: pagePagination05.items_per_page, action: 'GetCustomerPagesInfo' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            jQuery("#Pagination").show();
            if (tempOperPageCount == 0) {
                jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                tempOldtotalCount = msg.totalCount;
            }
            else if (tempOldtotalCount != msg.totalCount) {
                jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
            }
            BindCustomerInfo(msg.dataList);

        }
    });
}
function BindTeamTaskGroup(dataList) {
    var choiceBusSetText = "-请选择分组--";
    var defaultoptions = '<option code="qzz" value="-1" selected="selected">' + choiceBusSetText + '</option>';
    var options = "";
    jQuery("#slTeamTaskGroup").html(defaultoptions);
    if (dataList != null) {
        if (dataList.length > 0) {
            jQuery(dataList).each(function (i, item) {
                options += '<option code="' + item.InputCode + '" value="' + item.ID_TeamTaskGroup + '">' + item.TeamTaskGroupName + '</option>';
            });
            if (options != "") {
                options = defaultoptions + options;
            }
            else {
                options = defaultoptions;
            }
        }
    }
    else {
        options = defaultoptions;
    }
    jQuery("#slTeamTaskGroup").html(options);

    //    jQuery("#slTeamTaskGroup").find("option[value='-1']").attr("selected", true);
    //    jQuery("#s2id_slTeamTaskGroup .select2-choice span").text(choiceBusSetText);
}
/********************分页绑定客户名单 Begin***********************/

/// <summary>
/// 判断表格是否存在滚动条,并设置相应的样式
/// <summary>
function JudgeTableIsExistScroll_Custom() {
    jQuery(".j-Title-mag").each(function () {

        jQuery(this).scrollTop(1);
        if (jQuery(this).scrollTop() > 0) {
            jQuery(this).prev().addClass("TableHeaderBg");
        } else {
            jQuery(this).prev().removeClass("TableHeaderBg");
        }
        jQuery(this).scrollTop(0);

    });


    ResetShowText();

}