/*
*模块名称：客户预约时间更改
*
*
*
*/
var flag = jQuery("#flag").val(); //3:预约 6:登记 9：团体登记
var type = jQuery("#type").val(); //为当前操作类型，有Add、Edit值
/// <summary>
/// 文档加载完成，为扫描框设置焦点，并注册鼠标悬停选中事件
/// </summary>
jQuery(document).ready(function () {
    //设置默认选中
    jQuery("#txtID_Customer").focus();
    jQuery("#txtID_Customer").bind("mouseover", function () {
        jQuery(this).select();
    });
});

/// <summary>
/// 为页面注册按键事件，如果光标在扫描框中，且按键为回车时，将自动触发检索客户信息
/// </summary>
function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13)//回车事件
    {
        //如果是在搜索中
        if (id == "txtID_Customer") {
            //这里绑定客户所有的收费项目、费用统计信息
            //判断是否是正常体检号
            DoSearchCustomerInfoAndCustFeeInfo();
        }
    }
    if (id == "txtID_Customer" && (curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40)) {

    }
    return false;
}
/// <summary>
///通过客户编号[体检号]检索客户基本信息和客户收费项目信息
/// </summary>
function DoSearchCustomerInfoAndCustFeeInfo() {
    ResetAllCustomerInfo();
    var objID = "txtID_Customer";
    var Key = "ID_Customer";
    var modelName = jQuery("#modelName").val();
    var KeyValue = jQuery.trim(jQuery("#" + objID).val());
    if (KeyValue == "" || !isCustomerExamNo(KeyValue)) {
        return false;
    }
    //判断是否是预约客户
    var ErrorMsg = "";
    var card = KeyValue;
    var TypeCode = card.substring(1, 2); //TypeCode:3 个人预约 ,6个人登记 ,9团体登记
    //如果不为个人登记或者团体登记，则验证
    if (flag != 6 && flag != 9) {

        if (TypeCode != 3)//个人预约
        {
            ShowSystemDialog("对不起，体检号[" + card + "]不是预约客户，不允许进行更改！");
            return false;
        }
    }
    else if (flag == 6 || flag == 9) {
        if (TypeCode != 6 && TypeCode != 9)//个人预约
        {
            ShowSystemDialog("对不起，体检号[" + card + "]不是登记客户，不允许进行更改！");
            return false;
        }
    }
    //组建请求参数
    var Is_Org = 0;
    var data = { action: "GetCustomerByIDCustomer", ID_Customer: KeyValue, type: type, modelName: modelName };
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: data,
        cache: false,
        dataType: "json",
        success: function (msg) {
            SetCustomerInfo(msg, KeyValue);
            //这里绑定客户基本信息
        }
    });
    jQuery("#" + objID).blur();
    //jQuery("#" + objID).select();
    return false;
}
//设置用户基本信息
function SetCustomerInfo(msg, ID_Customer) {

    if (msg == null || msg == undefined)
        return false;
    var item;
    var dataList0 = msg.dataList0;
    if (dataList0 != undefined) {
        item = dataList0[0];
        jQuery("#lblCustomerName").text(item.CustomerName);
        jQuery("#lblSex").text(item.GenderName);
        jQuery("#lblIDCard").text(item.IDCard);
        jQuery("#lblTel").text(item.MobileNo);
        jQuery("#lblMarriedName").text(item.MarriageName);
        //设置头像
        if (item.Base64Photo == "") {
            jQuery("#HeadImg").attr("src", defalutImagSrc);
        }
        else {
            jQuery("#HeadImg").attr("src", "data:image/gif;base64," + item.Base64Photo + "");
        }
        //绑定用户基本信息
        //jQuery("#lblAge").text(catcAge(item.date));
        jQuery("#lblAge").text(item.Age);
        jQuery("#tblSearch tbody tr[id='loading']").hide();
        jQuery("#tblSearch tbody tr[id!='loading']").show();
        jQuery("#LSave").show(); jQuery("#lblRegisterDate").show();
        jQuery("#showInfo").show();
        //绑定用户基本信息
        jQuery("#lblID_Customer").text(item.ID_Customer);
        jQuery("#lblID_Customer").data("Code128c", item.Code128c);
        jQuery("#lblRegisterDate").val(item.SubScribDate);
        jQuery("#hidlblRegisterDate").text(item.SubScribDate);
        jQuery("#lblTeamName").text(item.TeamName);
        jQuery("#lblExamType").text(item.ExamTypeName);

        jQuery("#lblOperateDate").text(item.OperateDate);
        jQuery("#lblOperator").text(item.Operator);

        var data = { Is_GuideSheetPrinted: item.Is_GuideSheetPrinted, Is_Subscribed: item.Is_Subscribed };
        jQuery("#txtID_Customer").data("data", data);
    }
    if (dataList0.length == 0) {
        jQuery("#tblTeamTaskGroupFee tbody tr[id!='loading']").remove();
        jQuery("#tblTeamTaskGroupFee tbody tr[id='loading']").show();
        jQuery("#tblSearch tbody tr[id='loading']").show();
        jQuery("#tblSearch tbody tr[id!='loading']").hide();
        //设置总体价格汇总
        jQuery("#lblSumHeaderX").html("");
        //var ysjg = 0, ssjg = 0, ysfy = 0, yjfy = 0;
        var curHtml = "收费项目(<label style='color:red;font-size:13px; text-decoration:underline;'>共：0个；原始价格：0元；应收价格：0元；应收费用：0元；应缴费用：0元</label>)";
        var data = { ysjg: 0, ssjg: 0, ysfy: 0, yjfy: 0 };
        jQuery("#divSumHeader").data("sumData", data);
        jQuery("#divSumHeader").html(curHtml);
        jQuery("#lblSumHeaderX").html("应收费用：0元");
    }
    else {
        GetCustomerExamPhysicalInfo(ID_Customer);
    }
    jQuery("#txtID_Customer").focus();
    jQuery("#txtID_Customer").select();
}

/// <summary>
/// 获取用户体检信息和套餐内容
/// </summary>
/// <returns></returns>
function GetCustomerExamPhysicalInfo(ID_Customer) {

    if (ID_Customer != "") {
        var Is_Org = 0;
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxRegiste.aspx",
            data: { action: 'GetCustomerExamPhysicalInfo',
                ID_ArcCustomer: "",
                ID_Customer: ID_Customer
            },
            cache: false,
            dataType: "json",
            success: function (msg) {
                if (msg.dataList0.length > 0) {
                    jQuery(msg.dataList0).each(function (i, item) {
                        if (item.Is_Team == "True") {
                            Is_Org = 1;
                            jQuery("#lblTeamName").text(item.TeamName);
                        }
                        else {
                            Is_Org = 0;
                            jQuery("#lblTeamName").text("");
                        }
                        //设置用户基本信息
                        jQuery("#lblID_Customer").text(item.ID_Customer);
                    });
                }
                if (msg.dataList1.length > 0) {
                    //ReadBustFeeData(Is_Org, msg.dataList1);//这里绑定收费套餐信息
                }
            }
        });
    }
}
/// <summary>
/// 修改客户预约体检时间
/// </summary>
/// <returns></returns>
function DoUpdateCustomerSubScribDate() {
    var CustomerName = jQuery.trim(jQuery("#lblCustomerName").text());
    var ID_Customer = jQuery.trim(jQuery("#lblID_Customer").text());
    var SubScribDate = jQuery.trim(jQuery("#lblRegisterDate").val());
    var OldSubScribDate = jQuery.trim(jQuery("#hidlblRegisterDate").text());
    if (CustomerName != "") {
        if (SubScribDate != OldSubScribDate) {
            if (confirm("您确认要将客户[" + CustomerName + "]体检日期[" + OldSubScribDate + "]修改为[" + SubScribDate + "]吗？")) {
                if (flag != 6 && flag != 9) {
                    DoUpdateCustomerSubScribDate_Ajax(CustomerName, ID_Customer, SubScribDate);
                }
                else {
                    //体检时间必须选择当天
                    if (SubScribDate != CurDate) {
                        ShowSystemDialog("客户[" + CustomerName + "]的体检日期必须选择当天！"); return false;
                    }
                    else {
                        DoUpdateCustomerSubScribDate_Ajax(CustomerName, ID_Customer, SubScribDate);
                    }
                }
            }
            else {
                jQuery("#lblRegisterDate").val(OldSubScribDate);
            }
        }
        else {
            ShowSystemDialog("客户[" + CustomerName + "]的体检日期未做任何更改，勿须保存！");
            return false;
        }
    }
}
/// <summary>
/// 通过Ajax修改客户预约体检时间
/// </summary>
/// <returns></returns>
function DoUpdateCustomerSubScribDate_Ajax(CustomerName, ID_Customer, SubScribDate) {
    if (ID_Customer != "") {
        var data = { action: "UpdateCustomerSubScribDate", CustomerName: CustomerName, ID_Customer: ID_Customer, SubScribDate: SubScribDate };
        jQuery.ajax({
            type: "GET",
            url: "/Ajax/AjaxRegiste.aspx",
            data: data,
            cache: false,
            dataType: "json",
            success: function (msg) {
                if (msg.success == 0) {
                    ShowSystemDialog("对不起，修改客户[" + CustomerName + "]的体检日期失败，请您重试！");
                }
                else if (msg.success == 1) {
                    jQuery("#hidlblRegisterDate").text(SubScribDate);
                    ShowSystemDialog("成功修改客户[" + CustomerName + "]的体检日期！");
                }
                return false;
            }
        });
    }
}

function ResetAllCustomerInfo() {
    jQuery("#lblID_Customer").text("");
    jQuery("#lblCustomerName").text("");
    jQuery("#lblSex").text("");
    jQuery("#lblAge").text("");
    jQuery("#lblIDCard").text("");
    jQuery("#lblTel").text("");
    jQuery("#lblExamType").text("");
    jQuery("#lblOperateDate").text("");
    jQuery("#lblOperator").text("");
    jQuery("#lblTeamName").text("");
    jQuery("#lblMarriedName").text("");
    jQuery("#hidlblRegisterDate").text("");
    jQuery("#lblRegisterDate").val("");
    jQuery("#lblRegisterDate").hide();
    jQuery("#LSave").hide();
    jQuery("#showInfo").hide();
    jQuery("#loading").show();
}