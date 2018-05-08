/***************该文件为预约、登记界面公用脚本******************************/
//修改人：黄兴茂
//修改日期：2013-07-25
//修改内容：主要修改了团体登记中，不能正常绑定体检类型、套餐类型 、民族的问题。
//          以及修改了收费项目绑定出现多余项目[此问题是由于没有考虑到团体预约可能没有套餐或者选择了套餐而删除了项目，不能简单的调用套餐变动事件进行绑定]
//          还修改了完成登记无法显示条码信息[由于在绑定客户基本信息时，后台语句没有查询出Code128C编码]

var curFixed = 2; //有效数字
var curYJ = 0; //原价
var curZK = 0; //折扣
var curSJ = 0; //实价
var defalutImagSrc = "/template/blue/images/icons/nohead.gif"; //默认头像
var allBusSet = ''; //全部的套餐明细
var allBusFee = ''; //全部的收费项目明细
var allFeeWay = ''; //所有付费方式
var allHiddFeeWay = ''; //所有隐藏的付费方式
var choiceBusSetText = "--请选择套餐--";
var choiceCountryText = "--请选择国家--";
var choiceCultrulText = "--请选择教育背景--";
var choiceNationText = "--请选择民族--";
var choiceGuideNurseText = "--请选择导检护士--";
var choiceMarriedText = "--请选择婚姻状态--";
var type = ""; //  jQuery("#type").val(); //为当前操作类型，有Add、Edit值
var IsTeam = ""; //  jQuery("#IsTeam").val(); //为当前操作类型，有Add、Edit值
var InputRigth = ""; // jQuery("#InputRigth").val(); //获取用户录入权限，该参数用于控制用户是否可用手动录入身份证信息
var TargetID_Customer = ""; //  jQuery("#ID_Customer").val(); //跳转过来的体检号
var TargetID_ArcCustomer = ""; // jQuery("#ID_ArcCustomer").val(); //跳转过来的存档号
var SynCardOcx1 = ""; // document.getElementById("SynCardOcx1"); //获取身份证插件
var CVR_IDCard = ""; //银安身份证读卡插件
var FastReport = ""; // document.getElementById("FastReport"); //获取报表插件
var TakePhoto = ""; // document.getElementById("TakePhoto"); //获取拍照插件
var str = ""; //保存身份证读取信息
var IsSaved = true; //保存是否可用保存登记信息，默认可保存，在新增项目时将此标记变为false，保存成功后重置为true，此标记用于补打、保存
var SecurityLevelHtml = ""; //记录客户所有的操作密级
//定义56个民族
var NationArray = ['汉族', '蒙古族', '回族', '藏族', '维吾尔族', '苗族', '彝族', '壮族', '布依族', '朝鲜族', '满族', '侗族', '瑶族', '白族', '土家族', '哈尼族', '哈萨克族', '傣族', '黎族', '傈僳族', '佤族', '畲族', '高山族', '拉祜族', '水族', '东乡族', '纳西族', '景颇族', '柯尔克孜族', '土族', '达翰尔族', '仫佬族', '羌族', '布朗族', '撒拉族', '毛南族', '仡佬族', '锡伯族', '阿昌族', '普米族', '塔吉克族', '怒族', '乌孜别克族', '俄罗斯族', '鄂温克族', '德昂族', '保安族', '裕固族', '京族', '塔塔尔族', '独龙族', '鄂伦春族', '赫哲族', '门巴族', '珞巴族', '基诺族'];
/// <summary>
/// 读取团体任务分组模版并返回thead部分和tbody部分html内容
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function ReadTemplateTeamTaskGroup(TemplateTeamTaskGroupID) {
    //默认是读取tblTemplateTeamTaskList模版填充到tblTeamTask中显示
    if (TemplateTeamTaskGroupID == "" || TemplateTeamTaskGroupID == undefined) {
        TemplateTeamTaskGroupID = "TemplateRegistCustFee";
    }
    var teamTaskGroupListContent = ""; //团体任务分组table内容
    var teamTaskGroupListTheadTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' thead').html(); //团体任务模版Thead部分
    var teamTaskListBodyTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' tbody').html(); //团体任务模版body部分
    this.teamTaskGroupListTheadTempleteContent = teamTaskGroupListTheadTempleteContent;
    this.teamTaskListBodyTempleteContent = teamTaskListBodyTempleteContent;
    return this;
}
/*************Form表单按键操作 Begin**************/
function DoParentLoad() {
    DoLoad('/System/Customer/RegistList.aspx?type=' + type + '&modelName=' + jQuery('#modelName').val() + "&IsTeam=" + jQuery('#IsTeam').val(), '');
}
function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13)//回车事件
    {
        //如果是在搜索中
        if (id == "txtSearch") {
            //SureAdd();
        }
        else {

            if (id == "txtSFZ")//如果光标在身份证号内，则自动通过体检身份证号检索信息
            {
                jQuery("#btnSearch").click(); return;
                //Search(id, "IDCard");
            }
            else if (id == "txtSearchX") //如果光标在扫描区则自动触发检索事件
            {
                jQuery("#btnSearch").click(); return;
            }
            else if (id == "txtTJH") //如果光标在体检号内，则自动通过体检号检索信息
            {
                Search(id, "ID_Customer"); return;
            }
            else if (id == "txtYKT")//如果光标在一卡通号内，则自动通过一卡通号检索信息
            {
                Search(id, "ExamCard"); return;
            }
            else if (id == "txtExamCard")//如果光标在扫描的体检号内，则将发送信息到HIS去
            {
                jQuery("#txtExamCard").select();
                SendExamInfoToHis();
            }
            else if (id == "txtUserLoginName" || id == "txtUserPassword")//如果更换折扣人
            {
                ChangeUserDiscount();
            }

        }
    }
    if (id == "txtSearch" && (curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40)) {
        keyMove(document.getElementById(id), curEvent); return;
    }
    return false;
}
/*************Form表单按键操作 End**************/

/*************页面初始化 Begin***********************/
jQuery(document).ready(function () {
    CVR_IDCard = document.getElementById("CVR_IDCard"); //获取身份证插件 xmhuang 2013-10-21
    //SynCardOcx1 = document.getElementById("SynCardOcx1"); //获取身份证插件
    FastReport = document.getElementById("FastReport"); //获取报表插件
    TakePhoto = document.getElementById("TakePhoto"); //获取拍照插件

    type = jQuery("#type").val(); //为当前操作类型，有Add、Edit值
    IsTeam = jQuery("#IsTeam").val(); //为当前操作类型，有Add、Edit值
    if (IsTeam == 1) {
        jQuery("#lblSearchTitle").html("&nbsp;&nbsp;证件号码/体检号码(F2)&nbsp;");
    }
    else {
        jQuery("#lblSearchTitle").html("&nbsp;&nbsp;体检号码(F2)&nbsp;");
        jQuery("#divCloneRightFloat").show();      // 显示克隆
    }
    InputRigth = jQuery("#InputRigth").val(); //获取用户录入权限，该参数用于控制用户是否可用手动录入身份证信息
    TargetID_Customer = jQuery("#ID_Customer").val(); //跳转过来的体检号
    TargetID_ArcCustomer = jQuery("#ID_ArcCustomer").val(); //跳转过来的存档号

    SecurityLevelHtml = jQuery("#slOperateLevel").html(); //设置当前所有的用户操作密级

    jQuery("#btnPrintCust1").data("data", data); //设置打印参数和是否预约参数
    jQuery("#txtSearchX").focus(); //设置默认选中扫描文本框

    if (Base64PhtoStr == "") {
        jQuery("#HeadImg").attr("src", defalutImagSrc);
        jQuery("#HeadImg").attr("Base64Photo", "");
    }
    else {
        jQuery("#HeadImg").attr("src", "data:image/gif;base64," + Base64PhtoStr);
        jQuery("#HeadImg").attr("Base64Photo", Base64PhtoStr);
    }
    if (InputRigth != "1") {
        jQuery("#chcDefaultSearch").hide();
    }
    //设置团体显示信息
    if (teamID == "") {
        jQuery("#didTeam").html("");
    }

    //设置预约还是登记显示 Begin
    jQuery("#ckbRegiste").change(function () {
        //是否是预约
        var IsRegiste = document.getElementById("ckbRegiste").checked;
        if (IsRegiste) {
            jQuery("#lblRegiste").text("预约时间");
            jQuery("[name='btnComplete']").val(" 完 成(F9) ");   // 完成预约
            jQuery("[name='btnAdd']").val(" 申 请 ");        // 申请新客户预约
            jQuery("[name='btnComplete']").attr("title", " 完成预约 ");  // 完成预约
            jQuery("[name='btnAdd']").attr("title", " 申请新客户预约 ");       // 申请新客户预约
        }
        else {
            jQuery("#lblRegiste").text("体检时间");
            jQuery("[name='btnComplete']").val(" 完 成(F9) ");   // 完成登记
            jQuery("[name='btnAdd']").val(" 申 请 ");        // 申请新客户登记
            jQuery("[name='btnComplete']").attr("title", " 完成登记 ");  // 完成登记
            jQuery("[name='btnAdd']").attr("title", " 申请新客户登记 ");       // 申请新客户登记
        }

    });
    ShowElementInfo();
    //设置预约还是登记显示 End

    jQuery("#showBusFee").hide();
    jQuery("#txtSearchX").focus();
    //allBusSet = jQuery("#idSelectSet").html(); //获取所有的套餐内容
    allFeeWay = jQuery("#slFeeWay").html(); //获取所有的付费方式内容
    allHiddFeeWay = jQuery("#hidslFeeWay");



    //设置性别变动套餐
    jQuery("#slSex").change(function () {

        RemoveSelectedSet(); // 清空套餐相应的数据

    });
    jQuery("#slSex").change(); //触发slSex change事件
    //体检类型变动事件
    jQuery("#idSelectSet").change(function () {
        if (jQuery("#idSelectSet").val() == "" && (jQuery("#idSelectSet").data("ID_Customer") == undefined || jQuery("#idSelectSet").data("ID_Customer") == "")) { return; } // 如果选择的体检类型为空值 ，则不进行下面的操作

        var checked = document.getElementById("chcAll").checked;
        var checkedStr = checked == true ? 'checked="checked"' : '';
        var curValue = jQuery(this).val();
        var data = {};

        var optionsLength = document.getElementById("slFeeWay").options.length;

        //移除未保存的项目，只显示当前套餐项目和已经保存的项目
        jQuery("#tblList tbody tr[exist='0']").remove(); //移除未保存的项目
        //if (IsSearch == 1) { jQuery("#tblList tbody tr").remove(); IsSearch = 0; } //如果是检索则直接移除当前所有收费项目
        if (curValue != "-1" || jQuery("#idSelectSet").data("ID_Customer") != undefined) {
            var SetID = jQuery("#idSelectSet").val(); //获取套餐
            if (jQuery("#idSelectSet").data("ID_Customer") != undefined && jQuery("#idSelectSet").data("ID_Customer") != "") {
                data = { ID_Customer: jQuery("#idSelectSet").data("ID_Customer"), action: 'GetBusFeeByCustomerID' };
            }
            else {
                data = { ID_Customer: TargetID_Customer, SetID: SetID, action: 'GetBusFeeBySetID' };
            }
            jQuery.ajax({
                type: "GET",
                url: "/Ajax/AjaxRegiste.aspx",
                data: data,
                cache: false,
                dataType: "json",
                success: function (msg) {
                    allBusFee = msg;
                    var FeeWay = "", FeeWayName = "", ID_Customer = "", newContent = "", exist = 0, CustCssStyle = "CustFeeChargeState", ID_Customer = "";
                    var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateRegistCustFee");
                    var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
                    var teamTaskGroupFeeListBodyTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
                    var RowNum = jQuery("#tblList tbody tr[id!='loading']").length;
                    var XFeeChargeStaute = "", ItemCheckbox = "";
                    jQuery(msg.dataList).each(function (i, item) {
                        //如果没有行或者没有当前ID的非退费项目则允许添加到列表中
                        if (jQuery("#tblList tbody tr[id!='loading'][id_fee='" + item.ID_Fee + "'][custfeechargestate!='2']").length == 0)//判断是否包含当前ID的非退费项目，不包含则新增
                        {
                            //today
                            XFeeChargeStaute = "";
                            ItemCheckbox = "ItemCheckbox";
                            RowNum++;
                            //                            if (jQuery("#idSelectSet").data("ID_Customer") != undefined) {
                            if (jQuery.trim(item.ID_Customer) != "") {
                                exist = 1;
                                ID_Customer = jQuery("#idSelectSet").data("ID_Customer");
                                FeeWay = item.ID_FeeType;
                                FeeWayName = item.FeeWayName;
                            }
                            else {
                                CustCssStyle = "NewXM";
                                if (document.getElementById("slFeeWay") != null) {
                                    FeeWay = document.getElementById("slFeeWay").value;
                                    FeeWayName = jQuery(allHiddFeeWay).find("option[value='" + document.getElementById("slFeeWay").value + "']").text();
                                }
                            }
                            //如果是团体项目则不允许删除，隐藏checkbox
                            if (item.ID_TeamTaskGroup != "") {
                                exist = 1;
                                ItemCheckbox = "UnItemCheckbox";
                                CustCssStyle = "Yellow";
                                //判断收费状态
                                if (item.FeeChargeStaute == "2") {
                                    ItemCheckbox = "UnItemCheckbox";
                                    CustCssStyle = "Yellow";
                                    item.FeeChargeStaute = "√";
                                    XFeeChargeStaute = "√";
                                }
                                //判断是否是已检项目，已检项目不允许退费Is_Examined
                                else if (item.FeeChargeStaute == "1") {
                                    ItemCheckbox = "UnItemCheckbox";
                                    //item.FeeChargeStaute = "√";
                                    item.FeeChargeStaute = "√";
                                    CustCssStyle = "Green";
                                }
                                else if (item.FeeChargeStaute == "0") {
                                    ItemCheckbox = "UnItemCheckbox";
                                    //item.FeeChargeStaute = "";
                                    item.FeeChargeStaute = "";
                                    CustCssStyle = "TeamRed";
                                }
                            }
                            else {
                                //判断收费状态
                                if (item.FeeChargeStaute == "2") {
                                    ItemCheckbox = "UnItemCheckbox";
                                    CustCssStyle = "Yellow";
                                    item.FeeChargeStaute = "√";
                                    XFeeChargeStaute = "√";
                                }
                                //判断是否是已检项目，已检项目不允许退费Is_Examined
                                else if (item.FeeChargeStaute == "1") {
                                    item.FeeChargeStaute = "√";
                                    ItemCheckbox = "UnItemCheckbox";
                                    CustCssStyle = "Green";
                                }
                                else if (item.FeeChargeStaute == "0") {
                                    ItemCheckbox = "ItemCheckbox";
                                    item.FeeChargeStaute = "";
                                    CustCssStyle = "Red";
                                }
                            }
                            //由于在从套餐中检索时，是没有折扣人、注册人字段，这里需要判断
                            if (teamTaskGroupFeeListBodyTempleteContent != null) {
                                newContent += teamTaskGroupFeeListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@ItemCheckbox/gi, ItemCheckbox)
                            .replace(/@ID_TeamTaskGroup/gi, item.ID_TeamTaskGroup)
                             .replace(/@exist/gi, exist)
                             .replace(/@Is_Org/gi, 0)
                            .replace(/@ID_Customer/gi, ID_Customer)
                            .replace(/@ID_Fee/gi, item.ID_Fee)
                            .replace(/@FeeName/gi, item.FeeName)
                            .replace(/@FeeChargeStaute/gi, item.FeeChargeStaute)
                            .replace(/@Price/gi, item.Price)
                            .replace(/@FactPrice/gi, item.FactPrice)
                             .replace(/@Is_FeeRefund/gi, item.Is_FeeRefund)
                            .replace(/@Is_FeeCharged/gi, item.Is_FeeCharged)
                            .replace(/@ID_Section/gi, item.ID_Section)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@date/gi, item.date)
                            .replace(/@FeeType/gi, FeeWay)
                            .replace(/@FeeWayName/gi, FeeWayName)
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@CustCssStyle/gi, CustCssStyle)
                            .replace(/@ID_CustFee/gi, item.ID_CustFee)
                            .replace(/@CustFeeChargeState/gi, item.CustFeeChargeState)
                            .replace(/@Is_Printed/gi, item.Is_Printed)

                            .replace(/@Discount/gi, item.Discount)
                            .replace(/@ID_Discounter/gi, item.ID_Discounter)
                            .replace(/@XDiscounterName/gi, item.DiscounterName)
                            .replace(/@ID_Register/gi, item.ID_Register)
                            .replace(/@RegisterName/gi, item.RegisterName)
                            .replace(/@RegistDate/gi, item.RegistDate)
                            .replace(/@XFeeChargeStaute/gi, XFeeChargeStaute)
                            ;
                            }
                        }
                    });
                    if (newContent != '') {
                        jQuery("#tblList thead").html(teamTaskGroupFeeListTheadTempleteContent);
                        //如果套餐为非当前套餐，且未付款和未体检项目则一并移除
                        //如果是新增则默认移除所有套餐显示当前套餐
                        //                        if (type.toLowerCase() == "add") {
                        //                            jQuery("#tblList tbody").html(newContent);
                        //                        }
                        //如果是修改，则只能移除尚未保存的套餐
                        //else {
                        jQuery("#tblList tbody").append(newContent);
                        //}
                        newContent = "";
                        DoScrollToBottom();
                        BindFeeWay();
                        BindKeyup();
                    }
                    jQuery("#idSelectSet").removeData("ID_Customer"); //移除数据项
                    SumJG(); //计算合计

                }
            });
        }
    });

    jQuery("#txtXMZK").keyup(function () {
        SetDisscount();
        SumJG(); //计算合计
    });



    //绑定统一付费方式
    jQuery("#slFeeWay").html(jQuery(allHiddFeeWay).html());
    jQuery("#slFeeWay").change(function () {
        var id = '';
        var curSelectedValue = jQuery(this).val();
        jQuery("[name='ItemCheckbox']").each(function () {
            //            if (jQuery(this).parent().parent().attr('CustFeeChargeState') == "0") {
            if (jQuery(this).attr("checked")) {
                jQuery(this).parent().parent().find("[name = 'sffs']").attr("feetype", curSelectedValue);
                jQuery(this).parent().parent().find("[name = 'sffs']").text(jQuery(allHiddFeeWay).find("option[value='" + curSelectedValue + "']").text());
            }
            //            }
        }); SumJG();
    });
    //SumJG(); //计算合计
    //判断当前操作类型
    BindEditElement();
    DoScrollToBottom();
    SetShowElement();
    HideAllQuickTableEvent(); // 通过注册焦点事件，隐藏弹出框
    ShowMe(); //设置显示或隐藏列表
    //如果是团体则隐藏新增申请按钮 黄兴茂 2013-08-16 
    if (IsTeam == 1) {
        jQuery("[name='btnAdd']").hide();
        jQuery("#btnReadFromMachine").hide();
        jQuery("#btnGenerate").hide();
    }

    //如果是已经总检则隐藏保存按钮 xmhuang 2013-09-10
    if (Is_Checked == 1 || Is_Checked == "True") {
        jQuery("[name='btnComplete']").hide(); //隐藏保存按钮
        jQuery("#divAddCustFee").hide(); //隐藏新增收费项目操作组
        jQuery("#imgCustomer").hide(); //隐藏获取头像按钮
    }
    else {
        jQuery("[name='btnComplete']").show(); //显示保存按钮
        jQuery("#divAddCustFee").show(); //显示新增收费项目操作组
        jQuery("#imgCustomer").show(); //显示获取头像按钮
    }

    DisposeCamera();
});
/*************页面初始化 Begin***********************/

/*************折扣变动 Begin*******************/
function BindKeyup() {
    jQuery("#tblList tr td label[name='zk'][CustFeeChargeState=='0']").keydown(function () {
        return (/[\d.]/.test(String.fromCharCode(event.keyCode)));
    }).keyup(function () {
        var curZK = jQuery.trim(jQuery(this).val());
        if (curZK == '')
            jQuery(this).val(DisCountRate);
        if (parseFloat(curZK) < DisCountRate || parseFloat(curZK) >= 10)
            jQuery(this).val(DisCountRate);
        if (DisCountRate == 0)
            jQuery(this).val(10);
        curYJ = jQuery(this).parent().parent().find('[name="yj"]').val();
        curZK = jQuery(this).val() * 10;
        var FactPrice = parseFloat(curYJ) * parseFloat(curZK) / 100;
        jQuery(this).parent().parent().find('[name="sj"]').val(parseFloat(FactPrice).toFixed(curFixed));
    }).change(function () {
        SumJG();
    });
}
/*************折扣变动 End*******************/

/**********绑定收费方式 Begin**********/
function BindFeeWay() {

    //jQuery("#tblList select[name='fffs']").select2();
    var FeeType = document.getElementById("slFeeWay").value;
    jQuery("#tblList tr td select[name='fffs']").each(function () {
        if (jQuery(this).children("option").length <= 0) {
            if (jQuery(this).attr("FeeType") != undefined) {
                FeeType = jQuery(this).attr("FeeType")
            }
            jQuery(this).empty();
            jQuery(this).append(jQuery("#slFeeWay").html()); //获取所有付费方式;
            jQuery(this).find("option").removeAttr("selected");
            jQuery(this).find("option[value='" + FeeType + "'").attr("selected", "selected");
        }
    });
}
/**********绑定收费方式 End**********/

/**********绑定页面元素设置显示值 Begin***********************/
function BindEditElement() {
    SetControlEnable();
    SetControlDefalut();
}
/**********绑定页面元素设置显示值 End***********************/

/**********设置选中项 Begin******************************/
function SetControlSelect() {

    jQuery("#slSex [forgender='" + selectedGender + "']").attr("selected", true);
    jQuery("#slMarried [value='" + selectedMarriage + "']").attr("selected", true);
    jQuery("#slReportWay [value='" + selectedReportWay + "']").attr("selected", true);

    jQuery("#slFeeWay option[value='" + selectedFeeWay + "']").attr("selected", true);
    //如果用户的操作密级大于了100，则证明该客户已经加密
    if (selectedSecurityLevel > 100) {
        jQuery("#slOperateLevel").html("<option value='" + selectedSecurityLevel + "'>已加密</option>");
    }
    else {
        jQuery("#slOperateLevel [value='" + selectedSecurityLevel + "']").attr("selected", true);
    }
    jQuery("#slCultrul [value='" + selectedCultrul + "']").attr("selected", true);
    jQuery("#slExamPlace [value='" + selectedExamPlace + "']").attr("selected", true);
    jQuery("#slOperateLevel [value='" + selectedOperateLevel + "']").attr("selected", true);

    jQuery("#idSelectSet").data("ID_Customer", jQuery("#txtTJH").val());

    var dddd = jQuery("#idSelectSet").data("ID_Customer");
    ShowQuickSelectUser(selectedGuideNurse, selectedGuideNurseName);    // 设置导检护士的已选人员
    ShowQuickSelectNation(selectedNation, selectedNationName);          // 设置民族的已选项
    ShowQuickSelectExamType(selectedExamType, selectedExamTypeName);    // 设置体检类型的已选项
    ShowQuickSelectSet(selectedSet, selectedPEPackageName, true);                   // 设置套餐的已选项
    //设置用户性别
    jQuery("#lblHidSex").text(document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text);
}
/**********设置选中项 End******************************/
//修改人：黄兴茂 
//修改时间：2013-07-26 
//修改内容：如果是修改客户登记、预约、团体登记预约信息，则体检号、性别、出生日期为label显示
function SetShowElement() {
    //    jQuery(".txtShow").hide();
    //    jQuery(".lblHid").show();
    //    //return false;
    if (type.toLowerCase() == "edit") {
        jQuery(".txtShow").hide();
        jQuery(".lblHid").show();
        //获取性别
        jQuery("[name='lblHidSex']").text(document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text);
        //jQuery("#btnReadFromMachine").hide();  //隐藏从设备读取按钮
    }
    else if (type.toLowerCase() == "add" || IsSearch == 1) {
        jQuery(".txtShow").show();
        jQuery(".lblHid").hide();
        //jQuery("#btnReadFromMachine").show(); //显示从设备读取按钮
    }
}

function ShowElementInfo() {
    /*设置显示值*/
    if (type.toLowerCase() == "edit") {
        //jQuery("#btnReadFromMachine").hide(); //隐藏从设备读取按钮
        if (jQuery('#modelName').val().toLowerCase() == "regist") {
            jQuery("#lblRegiste").text("预约时间");
            jQuery("#ckbRegiste").attr("disabled", "disabled");
            jQuery("[name='btnComplete']").val(" 完 成(F9) ");   // 完成预约
            jQuery("[name='btnAdd']").val(" 申 请 ");        // 申请新客户预约
            jQuery("[name='btnComplete']").attr("title", " 完成预约 ");     // 完成预约
            jQuery("[name='btnAdd']").attr("title", " 申请新客户预约 ");    // 申请新客户预约


        }
        else if (jQuery('#modelName').val().toLowerCase() == "sign") {
            jQuery("#lblRegiste").text("登记时间");
            jQuery("#ckbRegiste").attr("checked", false);
            jQuery("#ckbRegiste").attr("disabled", "disabled");
            jQuery("[name='btnComplete']").val(" 完 成(F9) ");   // 完成登记
            jQuery("[name='btnAdd']").val(" 申 请 ");        // 申请新客户登记
            jQuery("[name='btnComplete']").attr("title", " 完成登记 ");  // 完成登记
            jQuery("[name='btnAdd']").attr("title", " 申请新客户登记 "); // 申请新客户登记
        }
    }
    else {
        if (jQuery('#modelName').val().toLowerCase() == "regist") {
            jQuery("#ckbRegiste").attr("checked", true);
            jQuery("#ckbRegiste").change();
        }
        else if (jQuery('#modelName').val().toLowerCase() == "sign") {
            jQuery("#ckbRegiste").attr("checked", false);
            jQuery("#ckbRegiste").change();
        }
        //jQuery("#btnReadFromMachine").show(); //显示从设备读取按钮，此按钮只能在新增状态下可见
    }

    /*设置显示*/
}
/*********设置页面下拉元素默认选中项 Begin********************/
function SetControlDefalut() {
    if (jQuery("#slCultrul").find("option[value='-1']").length > 0) {

    }
    else {
        jQuery("#slCultrul").prepend('<option code="qxz" value="-1">' + choiceCultrulText + '</option>'); //为select在第一个位置插入option
    }
    if (type.toLowerCase() == "add") {

        // 导检护士（如果是新增，则清空导检护士相应的数据）
        RemoveSelectedUser();

        // 体检类型（如果是新增，则清空体检类型相应的数据）
        RemoveSelectedExamType();

        // 套餐（如果是新增，则清空套餐相应的数据）
        RemoveSelectedSet();

        //        // 新增时，默认为汉族 
        //        ShowQuickSelectNation(1, "汉族");          // 设置民族的已选项

        jQuery("#slCultrul").find("option:selected").attr("selected", false);
        jQuery("#slCultrul").find("option[value='-1']").attr("selected", true);
        jQuery("#s2id_slCultrul .select2-choice span").text(choiceCultrulText);
        ShowQuickSelectExamType(1, "健康体检");    // 设置体检类型的已选项

        jQuery("#btnGenerate").show(); //显示无证件按钮
        jQuery("[name='btnAdd']").hide(); //隐藏新增按钮
        jQuery("#btnGenerate").removeAttr("disabled");
    }
    else if (type.toLowerCase() == "edit") {
        SetControlSelect();
        jQuery("#btnGenerate").hide();
        if (type.toLowerCase() == "edit")//如果是编辑状态不允许修改登记时间
        {
            jQuery("#txtSubScribDate").hide();
            //            jQuery("#txtSubScribDate").addClass("InputNoBorder"); //设置文本无边框显示
            //            jQuery("#txtSubScribDate").removeClass("Wdate"); //移除登记时间样式
        }
    }
}
/*********设置页面下拉元素默认选中项 End********************/

/*********设置不可编辑项 Begin*******************/
function SetControlEnable() {
    //隐藏检索按钮
    jQuery("#btnSearchX").hide();
    if (type.toLowerCase() == "add") {
        //jQuery("#txtSFZ").removeAttr("readOnly");
        jQuery("#txtYKT").removeAttr("readOnly");
        //jQuery("#txtCustomer").removeAttr("readOnly");
    }
    else if (type.toLowerCase() == "edit") {

        //jQuery("#txtSFZ").attr("readOnly", 'true');
        jQuery("#txtYKT").attr("readOnly", 'true');
        jQuery("#txtTJH").attr("readOnly", 'true');
        jQuery("#txtCustomer").attr("readOnly", 'true');
    }
}
function SetControlDisabled() {
    jQuery("#btnSave").attr("disabled", "disabled");
    jQuery("#btnSave1").attr("disabled", "disabled");

    jQuery("#btnRegiste").attr("disabled", "disabled");
    jQuery("#btnRegiste1").attr("disabled", "disabled");

    jQuery("#btnSubScribe").attr("disabled", "disabled");
    jQuery("#btnSubScribe1").attr("disabled", "disabled");


}
/*********设置不可编辑项 End*******************/

/*********汇总费用 Begin*******************/
function SumJG() {
    var xmzj = jQuery("#tblList tbody tr").length; //项目合计个数
    var sumYJ = 0; //项目原价总合计
    var sumZK = 0; //项目实收合计
    var sumSJ = 0; //项目实际合计
    var curZK = DisCountRate;
    var curSJ = 0;
    var sumXJJE = 0; //现金金额
    var sumJZJE = 0; //记账金额
    var sumYJFY = 0; //本次应交费用
    var sumUnPrintyjfy = 0; //本次未打印收费项目的应交费用
    var feeTypeName = '', feeChargeStaute = "";
    var ParentIsPrinted = "";
    jQuery("#tblList tbody tr td [name='yj']").each(function () {
        feeChargeStaute = jQuery(this).parent().parent().attr("feechargestaute");
        sumYJ += parseFloat(jQuery.trim(jQuery(this).text()));
        curZK = jQuery.trim(jQuery(this).parent().parent().find("[name = 'zk']").text());
        curSJ = parseFloat(jQuery.trim(jQuery(this).text())) * parseFloat(curZK) / 10;
        feeTypeName = jQuery.trim(jQuery(this).parent().parent().find("[name = 'sffs']").text());
        jQuery(this).parent().parent().find("[name = 'sj']").text(parseFloat(curSJ).toFixed(curFixed));
        if (feeTypeName == "现金") {
            sumXJJE += curSJ;
            if (feeChargeStaute == "") {
                sumYJFY += curSJ;
            }
        }
        else if (feeTypeName == "记账") {
            sumJZJE += curSJ;
        }
        //sumSJ += curSJ;

        /*xmhuang 2013-10-15 记录未打印、未收费的现金项目的应交费用 Begin*/

        ParentIsPrinted = jQuery(this).parent().parent().attr("is_printed");
        if (ParentIsPrinted == undefined || ParentIsPrinted == 0)//如果是未打印
        {
            if (feeTypeName == "现金")//如果是现金项目
            {
                if (feeChargeStaute == "") //如果是未收费
                {
                    sumUnPrintyjfy += curSJ;
                }
            }
        }
        /*xmhuang 2013-10-15 记录未打印、未收费的现金项目的应交费用 End*/
    });
    sumXJJE = parseFloat(sumXJJE).toFixed(curFixed);
    sumYJ = parseFloat(sumYJ).toFixed(curFixed);
    sumSJ = parseFloat(sumSJ).toFixed(curFixed);
    sumYJFY = parseFloat(sumYJFY).toFixed(curFixed);
    sumSJ = parseFloat(sumYJ - sumXJJE).toFixed(curFixed);
    sumUnPrintyjfy = parseFloat(sumUnPrintyjfy).toFixed(curFixed);

    var curHtml = "<label name='kytjxm'>客户预约项目</label>(<label style='color:red;font-size:13px; text-decoration:underline;'>共：" + xmzj + "个；原价合计：" + sumYJ.toString() + "元；折扣合计：" + sumSJ.toString() + "元；实收合计：" + sumXJJE.toString() + "元</label>)";
    var data = { xmzj: xmzj, yjzj: sumYJ, zkzj: sumSJ, zkhzj: sumSJ, xjzf: sumXJJE, jzzf: sumJZJE, yjfy: sumYJFY, sumUnPrintyjfy: sumUnPrintyjfy };
    jQuery("#divSumHeader").data("sumData", data);
    jQuery("#divSumHeader").html(curHtml);
    jQuery("#lblSumHeaderX").html("本次应交费用：" + sumYJFY + "元");

    /* 修改人：黄兴茂 修改日期：2013-08-13 修改内容：设置客户收费项目显示内容 Begin*/
    if (jQuery('#modelName').val() != undefined) {
        if (jQuery('#modelName').val().toLowerCase() == "regist") {
            jQuery("#divSumHeader label[name='kytjxm'").text("客户预约项目");
        }
        else if (jQuery('#modelName').val().toLowerCase() == "sign") {
            jQuery("#divSumHeader label[name='kytjxm'").text("客户登记项目");
        }
    }
    else {
        jQuery("#divSumHeader label[name='kytjxm'").text("客户体检项目");
    }
    /* 修改人：黄兴茂 修改日期：2013-08-13 修改内容：设置客户收费项目显示内容 End*/
}
/*********汇总费用 End*******************/



/*********通过身份证检索用户基本信息 Begin*********/

/*********通过身份证检索用户基本信息 End*********/
function ReBindCustomerBusFee() {
    var data = { ID_Customer: jQuery.trim(jQuery("#txtTJH").val()), action: 'GetBusFeeByCustomerID' };
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: data,
        cache: false,
        dataType: "json",
        success: function (msg) {
            var FeeWay = "", FeeWayName = "", ID_Customer = "", newContent = "", exist = 0, CustCssStyle = "CustFeeChargeState", ID_Customer = "";
            var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateRegistCustFee");
            var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
            var teamTaskGroupFeeListBodyTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
            var RowNum = 0;
            var XFeeChargeStaute = "";
            jQuery(msg.dataList).each(function (i, item) {
                //如果没有行或者没有指定id的行则新增
                //today
                XFeeChargeStaute = ""
                ItemCheckbox = "ItemCheckbox";
                RowNum++;
                //黄兴茂 2013-07-25 修改内容：在绑定用户收费项目时候，收费ID和收费名称都存放在数据源中，直接获取绑定值即可
                FeeWay = item.ID_FeeType;
                FeeWayName = item.FeeWayName;
                if (jQuery("#idSelectSet").data("ID_Customer") != undefined) {
                    exist = 1;
                    ID_Customer = jQuery("#idSelectSet").data("ID_Customer");
                }
                else {
                    CustCssStyle = "NewXM";
                    //                    FeeWay = document.getElementById("slFeeWay").value;
                    //                    FeeWayName = jQuery(allHiddFeeWay).find("option[value='" + document.getElementById("slFeeWay").value + "']").text();
                }
                //如果是团体项目则不允许删除，隐藏checkbox
                if (item.ID_TeamTaskGroup != "") {
                    exist = 1;
                    ItemCheckbox = "UnItemCheckbox";
                    CustCssStyle = "Yellow";
                    //判断收费状态
                    if (item.FeeChargeStaute == "2") {
                        ItemCheckbox = "UnItemCheckbox";
                        CustCssStyle = "Yellow";
                        item.FeeChargeStaute = "√";
                        XFeeChargeStaute = "√";
                    }
                    //判断是否是已检项目，已检项目不允许退费Is_Examined
                    else if (item.FeeChargeStaute == "1") {
                        ItemCheckbox = "UnItemCheckbox";
                        item.FeeChargeStaute = "√";
                        CustCssStyle = "Green";
                    }
                    else if (item.FeeChargeStaute == "0") {
                        ItemCheckbox = "UnItemCheckbox";
                        item.FeeChargeStaute = "";
                        CustCssStyle = "TeamRed";
                    }
                }
                else {
                    //判断收费状态
                    if (item.FeeChargeStaute == "2") {
                        ItemCheckbox = "UnItemCheckbox";
                        CustCssStyle = "Yellow";
                        item.FeeChargeStaute = "√";
                        XFeeChargeStaute = "√";
                    }
                    //判断是否是已检项目，已检项目不允许退费Is_Examined
                    else if (item.FeeChargeStaute == "1") {
                        item.FeeChargeStaute = "√";
                        ItemCheckbox = "UnItemCheckbox";
                        CustCssStyle = "Green";
                    }
                    else if (item.FeeChargeStaute == "0") {
                        ItemCheckbox = "ItemCheckbox";
                        item.FeeChargeStaute = "";
                        CustCssStyle = "Red";
                    }
                }
                if (teamTaskGroupFeeListBodyTempleteContent != null) {
                    newContent += teamTaskGroupFeeListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@ItemCheckbox/gi, ItemCheckbox)
                            .replace(/@ID_TeamTaskGroup/gi, item.ID_TeamTaskGroup)
                             .replace(/@exist/gi, 1)
                             .replace(/@Is_Org/gi, 0)
                            .replace(/@ID_Customer/gi, ID_Customer)
                            .replace(/@ID_Fee/gi, item.ID_Fee)
                            .replace(/@FeeName/gi, item.FeeName)
                            .replace(/@FeeChargeStaute/gi, item.FeeChargeStaute)
                            .replace(/@Price/gi, item.Price)
                            .replace(/@FactPrice/gi, item.FactPrice)
                             .replace(/@Is_FeeRefund/gi, item.Is_FeeRefund)
                            .replace(/@Is_FeeCharged/gi, item.Is_FeeCharged)
                            .replace(/@ID_Section/gi, item.ID_Section)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@date/gi, item.date)
                            .replace(/@FeeType/gi, FeeWay)
                            .replace(/@FeeWayName/gi, FeeWayName)
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@CustCssStyle/gi, CustCssStyle)
                            .replace(/@ID_CustFee/gi, item.ID_CustFee)
                            .replace(/@CustFeeChargeState/gi, item.CustFeeChargeState)
                            .replace(/@Is_Printed/gi, item.Is_Printed)

                            .replace(/@Discount/gi, item.Discount)
                            .replace(/@ID_Discounter/gi, item.ID_Discounter)
                            .replace(/@XDiscounterName/gi, item.DiscounterName)
                            .replace(/@ID_Register/gi, item.ID_Register)
                            .replace(/@RegisterName/gi, item.RegisterName)
                            .replace(/@RegistDate/gi, item.RegistDate)
                            .replace(/@XFeeChargeStaute/gi, XFeeChargeStaute)
                            ;
                }
            });
            if (newContent != '') {
                jQuery("#tblList thead").html(teamTaskGroupFeeListTheadTempleteContent);
                //如果套餐为非当前套餐，且未付款和未体检项目则一并移除
                //如果是新增则默认移除所有套餐显示当前套餐
                if (type.toLowerCase() == "add") {
                    jQuery("#tblList tbody").html(newContent);
                }
                //如果是修改，则只能移除尚未保存的套餐
                else {
                    jQuery("#tblList tbody").html(newContent);
                }
                newContent = "";
                DoScrollToBottom();
                BindFeeWay();
                BindKeyup();


            }

            SumJG(); //计算合计

        }
    });
}
/*********删除选中项目 Begin*********/
function DoDel() {
    var rowCount = jQuery("[name='ItemCheckbox']:checked").length;
    if (rowCount == 0) {
        return false;
    }
    if (confirm("您确认要删除吗？")) {
        //判断是否有选中项目
        var CustFeeID = '';
        var ID_Customer = jQuery("#txtTJH").val();
        //xmhuang 2013-09-10 Begin
        var Forsex = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
        Forsex = (Forsex == 1 ? Forsex : 0); //0：女性，1男性，2：共用
        //xmhuang 2013-09-10 End

        jQuery("[name='ItemCheckbox']").each(function () {
            if (jQuery(this).attr('checked')) {
                if (jQuery(this).parent().parent().attr('id_custfee') != "") {
                    CustFeeID += "'" + jQuery(this).parent().parent().attr('id_custfee') + "',";
                }
                jQuery(this).parent().parent().remove();
            }
        });

        if (CustFeeID != '') {
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxRegiste.aspx",
                contentType: "application/x-www-form-urlencoded;Content-length=1024000",
                data: { Forsex: Forsex, CustFeeID: CustFeeID, ID_Customer: ID_Customer, action: 'DeleteCustFee' },
                cache: false,
                dataType: "json",
                success: function (msg) {
                    if (msg != null) {
                        ShowSystemDialog(msg.Message);
                    }
                }
            });
        }

    }
    //计算合计
    SumJG();
    ResetOrder();
}
//从新排序
function ResetOrder() {
    var rowNum = 0;
    jQuery("#tblList tbody tr[id!='loading']").each(function () {
        rowNum++;
        jQuery(this).find("td label[name='lblRowNum']").text(rowNum);

    });
}
/*********删除选中项目 End*********/

/*
//验证数据项目
//通过元素已经设置的required属性来进行数据项为空验证，元素必须设置required类、emptyMessage、errorMessage信息
//如果需要验证Email、Phone需要设置元素vailType为Email、Phone Begin*/
function vailData(objElement) {

    var curObj = '';
    for (i = 0; i < document.forms.length; i++) {
        var elements = document.forms[i].elements;
        for (j = 0; j < elements.length; j++) {
            //undefined elements[j].attributes["requi"]
            curObj = elements[j].value;
            if (elements[j].attributes["required"] != null) {
                //判断是否为空
                if (elements[j].type == "text" || elements[j].type == "textarea") {

                    if (elements[j].value == "") {
                        if (elements[j].attributes["emptyMessage"] != null) {
                            if (elements[j].attributes["emptyMessage"].value != "") {
                                ShowSystemDialog(elements[j].attributes["emptyMessage"].value);
                            }
                            if (jQuery(elements[j]).is(":visible") == true) {
                                elements[j].focus();
                                elements[j].select();
                            }
                            return false;
                        }
                    }
                    else {
                        if (elements[j].attributes["vailType"] != null) {
                            var flag = true;
                            var vailType = elements[j].attributes["vailType"].value;
                            if (vailType == "Email") {
                                flag = isEmail(elements[j].value);
                            }
                            else if (vailType == "Mobil") {
                                flag = isMobil(elements[j].value);
                            }
                            if (!flag) {
                                if (elements[j].attributes["errorMessage"] != null) {
                                    ShowSystemDialog(elements[j].attributes["errorMessage"].value);
                                }
                                else {
                                    ShowSystemDialog("请输入正确的格式！");
                                }
                                if (jQuery(elements[j]).is(":visible") == true) {
                                    elements[j].focus();
                                    elements[j].select();
                                }
                                return false;
                            }
                        }
                    }
                }
                else if (elements[j].type == "select-one") {
                    if (jQuery(elements[j]).val() == "" || jQuery(elements[j]).val() == "-1") {
                        ShowSystemDialog(elements[j].attributes["errorMessage"].value);
                        elements[j].focus();
                        return false;
                    }
                    //                    if (jQuery(elements[j]).val() == "") {
                    //                        ShowSystemDialog(elements[j].attributes["errorMessage"].value);
                    //                        if (jQuery(elements[j]).is(":visible") == true) {
                    //                            elements[j].focus();
                    //                        }
                    //                        return false;
                    //                    }
                }
            }
            else {
                if (elements[j].value != "") {
                    if (elements[j].attributes["vailType"] != null) {
                        flag = true;
                        vailType = elements[j].attributes["vailType"].value;
                        if (vailType == "Email") {
                            flag = isEmail(elements[j].value);
                        }
                        else if (vailType == "Mobil") {
                            flag = isMobil(elements[j].value);
                        }
                        if (!flag) {
                            if (elements[j].attributes["errorMessage"] != null) {
                                ShowSystemDialog(elements[j].attributes["errorMessage"].value);
                            }
                            else {
                                ShowSystemDialog("请输入正确的格式！");
                            }
                            if (jQuery(elements[j]).is(":visible") == true) {
                                elements[j].focus();
                                elements[j].select();
                            }
                            return false;
                        }
                    }
                }
            }
        }
    }
    //xmhuang 203-09-28 如果体检号不存在在验证预约时间不得小于或等于当前时间
    var TempID_Customer = document.getElementById("txtTJH").value; //体检号
    if (TempID_Customer == "") {
        var TempDate = jQuery("#txtSubScribDate").val();
        if (jQuery('#modelName').val().toLowerCase() == "regist") {
            if (TempDate <= CurDate) {
                ShowSystemDialog("预约时间不得小于或等于当前时间");
                //jQuery("#txtSubScribDate").select();
                return false;
            }
        }
        else if (jQuery('#modelName').val().toLowerCase() == "sign") {

        }
    }
    SaveData(objElement);
}

function CheckCustomerInfo() {
    var IDCard = jQuery("#txtSFZ").val(); //身份证
    var ExamCard = jQuery("#txtYKT").val(); //一卡通
    var modelName = jQuery("#modelName").val();
    var IsExist = 0;
    //存储大数据请设置Content-length值
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: { action: 'GetCustomerInfo',
            modelName: modelName,
            IDCard: IDCard,
            ExamCard: ExamCard
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            IsExist = msg.Exist;
        }
    });
    return IsExist;
}
/*
//验证数据项目
//通过元素已经设置的required属性来进行数据项为空验证，元素必须设置required类、emptyMessage、errorMessage信息
//如果需要验证Email、Phone需要设置元素vailType为Email、Phone End*/

/*保存数据项 Begin*/
function SaveData(objElement) {

    var obj = jQuery("#tblList tr[name='busList']");
    if (obj.length == 0) {
        ShowSystemDialog("请您添加体检项目！");
        return false;
    }
    var ID_ArcCustomer = jQuery("#ID_ArcCustomer") == undefined ? "" : jQuery("#ID_ArcCustomer").val();
    var IDCard = document.getElementById("txtSFZ").value; //身份证号码
    var CustomerName = document.getElementById("txtCustomer").value; //客户名称
    var ID_Customer = document.getElementById("txtTJH").value; //体检号
    var ExamCard = document.getElementById("txtYKT").value; //一卡通
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var BirthDay = document.getElementById("txtBirthDay").value; //出生年月
    var Married = document.getElementById("slMarried").options[document.getElementById("slMarried").selectedIndex].value; //婚姻状况
    var MarriageName = document.getElementById("slMarried").options[document.getElementById("slMarried").selectedIndex].text; //婚姻状况
    var MobileNo = document.getElementById("txtMobil").value; //手机号码
    var GuideNurse = jQuery("#idSelectUser").val();             //导检护士ID
    var GuideNurseName = jQuery("#nameSelectUser").val();       //导检护士姓名
    var Nation = jQuery("#idSelectNation").val();               //名族ID
    var NationName = jQuery("#nameSelectNation").val();         //名族
    var ExamType = jQuery("#idSelectExamType").val();        //体检类型ID
    var ExamTypeName = jQuery("#nameSelectExamType").val();  //体检类型名称
    var BusSet = jQuery("#idSelectSet").val();                  //套餐类型ID
    var BusPEPackageName = jQuery("#nameSelectSet").val();            //套餐类型名称
    var ReportWay = document.getElementById("slReportWay").options[document.getElementById("slReportWay").selectedIndex].value; //报告领取方式
    var ReportWayName = document.getElementById("slReportWay").options[document.getElementById("slReportWay").selectedIndex].text; //报告领取方式
    var FeeWay = document.getElementById("slFeeWay").options[document.getElementById("slFeeWay").selectedIndex].value; //付费方式
    var FeeWayName = document.getElementById("slFeeWay").options[document.getElementById("slFeeWay").selectedIndex].text; //付费方式
    var OperateLevel = document.getElementById("slOperateLevel").options[document.getElementById("slOperateLevel").selectedIndex].value; //操作密级
    var OperateLevelName = document.getElementById("slOperateLevel").options[document.getElementById("slOperateLevel").selectedIndex].text; //操作密级
    var Email = document.getElementById("txtEmail").value; //Email
    var ExamPlace = document.getElementById("slExamPlace").options[document.getElementById("slExamPlace").selectedIndex].value; //体检地址
    var ExamPlaceName = document.getElementById("slExamPlace").options[document.getElementById("slExamPlace").selectedIndex].text; //体检地址
    var Note = document.getElementById("txtNote").value; //备注
    var Country = document.getElementById("slCountry").options[document.getElementById("slCountry").selectedIndex].value;   //国家ID
    var CountryName = document.getElementById("slCountry").options[document.getElementById("slCountry").selectedIndex].text; //国家名称
    var Cultrul = document.getElementById("slCultrul").selectedIndex == -1 ? -1 : document.getElementById("slCultrul").options[document.getElementById("slCultrul").selectedIndex].value; //教育背景
    var CultrulName = document.getElementById("slCultrul").selectedIndex == -1 ? -1 : document.getElementById("slCultrul").options[document.getElementById("slCultrul").selectedIndex].text; //教育背景
    var SubScribDate = document.getElementById("txtSubScribDate") == null ? "" : document.getElementById("txtSubScribDate").value; //预约时间
    var Base64Photo = jQuery("#HeadImg").attr("Base64Photo");
    var IsRegiste = document.getElementById("ckbRegiste").checked;
    var modelName = IsRegiste ? "Regist" : "Sign";
    var BusFeeItems = ''; //记录所有的套餐
    var AllIDFee = ""; //所有收费项目ID xmhuang 2013-10-30
    jQuery("#tblList tbody tr td [name='yj']").each(function () {
        if (jQuery(this).parent().parent().find("td input[name='ItemCheckbox']").length > 0) {
            var id_custfee = jQuery(this).parent().parent().attr("id_custfee"); //收费项目ID xmhuang 2013-09-25 新增用于规避出现双项目
            var id_customer = jQuery(this).parent().parent().attr("id_customer"); //收费项目所对应的体检号
            var id = jQuery(this).parent().parent().attr("id_fee"); //收费项目ID
            var id_discounter = jQuery(this).parent().parent().attr("id_discounter"); //折扣人ID
            var discountername = jQuery(this).parent().parent().attr("discountername"); //折扣人名称
            var exist = jQuery(this).parent().parent().attr('exist'); //是否存在
            var ID_Section = jQuery(this).parent().parent().attr('id_section'); //科室ID
            var SectionName = jQuery(this).parent().parent().attr('sectionname'); //科室名称
            var xmmc = jQuery.trim(jQuery(this).parent().parent().find("[name = 'xmmc']").text()); //收费项目名称
            var yj = jQuery.trim(jQuery(this).parent().parent().find("[name = 'yj']").text()); //原价
            var zk = jQuery.trim(jQuery(this).parent().parent().find("[name = 'zk']").text()); //折扣
            var sj = jQuery.trim(jQuery(this).parent().parent().find("[name = 'sj']").text()); //实际价格
            var sffs = jQuery.trim(jQuery(this).parent().parent().find("[name = 'sffs']").attr("feetype")); //收费方式ID
            var sffsName = jQuery.trim(jQuery(this).parent().parent().find("[name = 'sffs']").text()); //收费方式名称
            var itemID = id;
            AllIDFee += itemID + ",";
            var curItem = itemID + "_" + xmmc + "_" + yj + "_" + zk + "_" + sj + "_" + sffs + "_" + sffsName + "_" + itemID + "_" + ID_Section + "_" + SectionName + "_" + id_discounter + "_" + discountername + "_" + id_customer + "_" + id_custfee + "_" + exist;
            BusFeeItems += curItem + "|";
        }
    });
    var qustData = { action: 'SaveData',
        modelName: modelName,
        type: type,
        ID_ArcCustomer: ID_ArcCustomer,
        CardNum: IDCard,
        CustomerName: encodeURIComponent(CustomerName),
        ID_Customer: ID_Customer,
        ExamCard: ExamCard,
        Gender: Gender,
        GenderName: encodeURIComponent(GenderName),
        BirthDay: encodeURIComponent(BirthDay),
        Married: Married,
        MarriageName: encodeURIComponent(MarriageName),
        MobileNo: MobileNo,
        RegisteDate: encodeURIComponent(RegisteDate),
        Register: Register,
        BusSet: BusSet,
        BusPEPackageName: encodeURIComponent(BusPEPackageName),
        ReportWay: ReportWay,
        ReportWayName: encodeURIComponent(ReportWayName),
        ExamType: ExamType,
        ExamTypeName: encodeURIComponent(ExamTypeName),
        FeeWay: FeeWay,
        FeeWayName: encodeURIComponent(FeeWayName),
        GuideNurse: GuideNurse,
        GuideNurseName: encodeURIComponent(GuideNurseName),
        OperateLevel: OperateLevel,
        ExamPlace: ExamPlace,
        ExamPlaceName: encodeURIComponent(ExamPlaceName),
        Country: Country,
        CountryName: encodeURIComponent(CountryName),
        Email: encodeURIComponent(Email),
        Note: encodeURIComponent(Note),
        Nation: Nation,
        NationName: encodeURIComponent(NationName),
        Cultrul: Cultrul,
        CultrulName: encodeURIComponent(CultrulName),
        SubScribDate: encodeURIComponent(SubScribDate),
        BusFeeItems: encodeURIComponent(BusFeeItems),
        Base64Photo: Base64Photo
    };
    //存储大数据请设置Content-length值
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRegiste.aspx",
        data: qustData,
        cache: false,
        contentType: "application/x-www-form-urlencoded;Content-length=1024000",
        dataType: "json",
        success: function (jsonMsg) {
            //设置无证按钮可见
            jQuery("#btnGenerate").hide();
            jQuery("[name='btnAdd']").show();
            jQuery("[name='btnAdd']").removeAttr("disabled");
            jQuery(jsonMsg.dataList).each(function (i, msg) {
                if (msg.success == "1") {
                    //设置套餐为存在项exist=1
                    jQuery("#tblList tbody tr").attr("exist", "1");
                    //设置复选框不可编辑
                    jQuery("#ckbRegiste").attr("disabled", "disabled");

                    IsSaved = true;

                    //20130625,按照和xiaoqiu的讨论结果如下方式进行指引单打印
                    //这里判断是否有未完成的非团体的现金项目，如果有需要打印缴费通知单

                    var Is_Subscribed = msg.Is_Subscribed;
                    var Is_GuideSheetPrinted = msg.Is_GuideSheetPrinted;
                    var CustomerSubScribDate = item.SubScribDate;
                    var data = { SubScribDate: CustomerSubScribDate, Is_Subscribed: Is_Subscribed, Is_GuideSheetPrinted: Is_GuideSheetPrinted };
                    jQuery("#btnPrintCust1").data("data", data);

                    jQuery("#lblHidCustomer").text(msg.ID_Customer); jQuery("#txtTJH").val(msg.ID_Customer);  //由于新增时，体检号是从后台生成的，这里需要设置体检号
                    jQuery("#lblCode128c").text(msg.Code128c);
                    //设置成功完成后的提示信息
                    //XMHuang 20130820 如果当前打印报表则不提示此信息
                    var ShowMsg = "";
                    if (Is_Subscribed == 1) {
                        ShowMsg = "客户[" + jQuery("#txtCustomer").val() + "]成功完成预约！";
                        //ShowSystemDialog("客户[" + jQuery("#txtCustomer").val() + "]成功完成预约！");
                    }
                    else {
                        ShowMsg = "客户[" + jQuery("#txtCustomer").val() + "]成功完成登记！";
                        //ShowSystemDialog("客户[" + jQuery("#txtCustomer").val() + "]成功完成登记！");
                    }
                    jQuery("[name='btnAdd']").data("ShowMsg", ShowMsg);
                    //                    var CloneCustomer = {
                    //                        CustomerName: CustomerName,
                    //                        GenderName: GenderName,
                    //                        BirthDay: BirthDay,
                    //                        IDCard: IDCard,
                    //                        ID_Customer: msg.ID_Customer,
                    //                        Base64Photo: jQuery("#HeadImg").attr("Base64Photo")
                    //                    };
                    var Base64Photo = jQuery("#HeadImg").attr("Base64Photo");
                    AddCustomerQueue(msg.ID_Customer, CustomerName, GenderName, BirthDay, IDCard, Base64Photo);
                    //如果全部已经完成缴费，则直接打印指引单
                    PrintCust(1, "", msg, AllIDFee);

                    //重新绑定收费项目
                    //ReBindCustomerBusFee();//xmhuang 2013-09-25 注释，由于业务调整，此处不再绑定客户收费项目
                }
                else {
                    jQuery("#btnSave").attr("disabled", "");
                    ShowSystemDialog(msg.Message);
                }
            });
        },
        complete: function () {
            IsSaved = true;
        }
    });

}
function ReBindData(msg) {
    //保存成功后，这里重新绑定数据，采用调整方式进行 /System/Customer/RegistOper.aspx?type=Edit&modelName=Regist&ID_ArcCustomer=170&ID_Customer=11160620130016&IsPrint=1
    DoLoad('/System/Customer/RegistList.aspx?type=Edit&modelName=' + jQuery('#modelName').val() + 'ID_ArcCustomer=' + msg.ID_ArcCustomer + '&ID_Customer=' + msg.ID_Customer + '&IsPrint=1', '');
}
/**********全选***********/
function checkAllChildren(obj) {
    jQuery("[name='ItemCheckboxX']").each(function () {
        //判断是否隐藏
        jQuery(this).attr('checked', obj.checked);
    })
}
function checkAll(obj) {

    jQuery("[name='ItemCheckbox']").each(function () {
        //判断是否隐藏
        jQuery(this).attr('checked', obj.checked);
        //        if (obj.checked == true) {
        //            jQuery(this).parent().parent().addClass("externSelect");
        //        }
        //        else {
        //            jQuery(this).parent().parent().removeClass("externSelect");
        //        }
    })
}


/**************************用于套餐外项目添加 Begin***********************************/


/*重新绑定套餐内容 Begin*/
function DoAddX(obj) {

    jQuery("#txtSearch").val(''); //设置关键字为空
    var HasData = true;
    //jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:blue;text-align:center;">正在加载，请稍候...</td></tr>');
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    Gender = (Gender == 1 ? Gender : 0); //0：女性，1男性，2：共用
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var CustFeeID = ''; //获取当前所有套餐ID
    //查找非退费项目的收费项目ID
    jQuery("#tblList tr[name='busList'][custfeechargestate!='2']").each(function (i, item) {
        CustFeeID += "'" + jQuery(this).attr("id_fee") + "',";
    });
    var newContent = ''; //用于存放html
    //ajax请求：由于字符在get请求中超长，这里必须使用post方式提交请求
    jQuery.ajax({
        type: "POST",
        cache: false,
        url: "/Ajax/AjaxRegiste.aspx",
        data: { Gender: Gender, CustFeeID: CustFeeID, action: 'GetBusFeeNotINCustFeeID' },
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
                jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有适合[' + GenderName + ']性的套餐可供选择！</td></tr>');
                jQuery("#showBusFee").show();
            }
        },
        complete: function () {
            if (newContent == "") {
                jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有适合[' + GenderName + ']性的套餐可供选择！</td></tr>');
                jQuery("#showBusFee").show();
            }
            else {
                jQuery("#showBusFeeItem").append(newContent);
                BindSelect();

            }
            jQuery("#showBusFee").show(); DoScrollToBottom();
            newContent = "";
            jQuery("#txtSearch").focus();
        }

    });

}
function OnKeyUp() {
    SearchBusFee();
}
/***********关键字搜索
备注：将搜索到的内容放到最前面显示 由模糊匹配改成精确匹配
******************/
function SearchBusFee() {

    var curEvent = window.event || e;
    if (curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40) {
        return false;
    }
    if (curEvent.keyCode == 13) {
        if (jQuery("#externTBlList tbody tr[id!='loading'] td input:checked").length > 0) {
            SureAdd();
        }
    }
    //jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:blue;text-align:center;">正在加载，请稍候...</td></tr>');
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    Gender = (Gender == 1 ? Gender : 0); //0：女性，1男性，2：共用
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var CustFeeID = "", SelectedFee = "", InputCode = jQuery.trim(jQuery("#txtSearch").val()); ; //获取当前所有套餐ID
    //查找非退费项目的收费项目ID
    //    jQuery("#tblList tr[name='busList'][feechargestaute!='已退']").each(function (i, item) {
    //        CustFeeID += "'" + jQuery(this).attr("id_fee") + "',";
    //    });
    jQuery("#tblList tr[name='busList'][custfeechargestate!='2']").each(function (i, item) {
        CustFeeID += "'" + jQuery(this).attr("id_fee") + "',";
    });

    //获取选中的收费项目
    jQuery("#showBusFeeItem tr td input:checked").each(function () {
        SelectedFee += "'" + jQuery(this).parent().parent().attr("id_fee") + "',";
    });
    CustFeeID = CustFeeID.replace(/undefined/gi, "");
    SelectedFee = SelectedFee.replace(/undefined/gi, "");

    //提交后台处理
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRegiste.aspx",
        cache: false,
        data: { action: "SearchBusFeeByCustFeeID",
            Gender: Gender,
            CustFeeID: CustFeeID,
            SelectedFee: SelectedFee,
            InputCode: InputCode,
            SelectedFee: SelectedFee
        },
        dataType: "json",
        success: function (msg) {
            BindCutomerBusFee(msg, GenderName);
        }
    });
}
function BindCutomerBusFee(msg, GenderName) {
    var newContent = "";
    jQuery("#showBusFeeItem").data('ExternList', msg); //缓存数据项到divExternList
    jQuery("#showBusFeeItem").empty(''); //清除显示项目
    if (msg.dataList.length > 0) {
        //由于在点击新增的时候已经过滤掉存在的CustFeeID，这里毋须再次过滤
        jQuery(msg.dataList).each(function (i, item) {
            if (item.IsChecked == 2)//以InputCode开头的
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
        });
    }
    else {
        jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有适合[' + GenderName + ']性的套餐可供选择！</td></tr>');
        jQuery("#showBusFee").show();
    }
    if (newContent == "") {
        jQuery("#showBusFeeItem").html('<tr name="busList" exist="1"><td colspan="3" style="color:red;text-align:center;">对不起，没有适合[' + GenderName + ']性的套餐可供选择！</td></tr>');
        jQuery("#showBusFee").show();
    }
    else {
        jQuery("#showBusFeeItem").html(newContent);
        //xmhuang 2013-09-27 经讨论，屏蔽默认选中第一个的功能
        //        if (jQuery(".externCanFocus").first().length > 0) {
        //            jQuery(".externCanFocus td input[name='textExternItem']").first().focus(); //设置以InputCode开始的项光标
        //            jQuery(".externCanFocus td input[name='textExternItem']").first().select(); //设置以InputCode开始的项为选中项
        //        }
        newContent = "";
        BindSelect();
    }

}
/***********确认新增********************/
function SureAddCurrentRow(obj) {
    var RowNum = jQuery("#tblList tbody tr[id!='loading']").length + 1, newContent = '', checked = false, ID_Fee = '', userName = '', date = '', FeeName = '', Price = 0, Discount = 0, FactPrice = 0, FeeType = document.getElementById("slFeeWay").value, ID_Section = '', SectionName = '';
    ID_Fee = jQuery(obj).parent().parent().attr("id");
    userName = jQuery(obj).parent().parent().attr("userName");
    date = jQuery(obj).parent().parent().attr("date");
    FeeName = jQuery(obj).parent().parent().attr("FeeName");
    Price = jQuery(obj).parent().parent().attr("Price");
    //Discount = jQuery(obj).parent().parent().attr("Discount");
    FactPrice = jQuery(obj).parent().parent().attr("FactPrice");
    CustFeeChargeState = jQuery(obj).parent().parent().attr("CustFeeChargeState");
    ID_Section = jQuery(obj).parent().parent().attr("ID_Section");
    SectionName = jQuery(obj).parent().parent().attr("SectionName");
    TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateRegistCustFee");
    teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
    teamTaskGroupFeeListBodyTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
    newContent = "";
    if (teamTaskGroupFeeListBodyTempleteContent != null) {
        newContent += teamTaskGroupFeeListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@ItemCheckbox/gi, "ItemCheckbox")
                            .replace(/@ID_TeamTaskGroup/gi, "")
                             .replace(/@exist/gi, 0)
                            .replace(/@Is_Org/gi, 0)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_Fee/gi, ID_Fee)
                            .replace(/@FeeName/gi, FeeName)
                            .replace(/@FeeChargeStaute/gi, "")
                            .replace(/@Price/gi, Price)
                            .replace(/@FactPrice/gi, FactPrice)
                            .replace(/@Is_FeeRefund/gi, 0)
                            .replace(/@Is_FeeCharged/gi, 0)
                            .replace(/@Discount/gi, 10)

                            .replace(/@ID_Section/gi, ID_Section)
                            .replace(/@SectionName/gi, SectionName)
                            .replace(/@date/gi, item.date)
                            .replace(/@FeeType/gi, FeeType)
                            .replace(/@FeeWayName/gi, jQuery(allHiddFeeWay).find("option[value='" + FeeType + "']").text())
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@CustCssStyle/gi, "NewXM")
                            .replace(/@ID_Section/gi, ID_Section)
                            .replace(/@SectionName/gi, SectionName)
                            .replace(/@ID_CustFee/gi, "")
                            .replace(/@CustFeeChargeState/gi, "0")
                            .replace(/@Is_Printed/gi, "0")

                            .replace(/@ID_Discounter/gi, UserID)
                            .replace(/@XDiscounterName/gi, UserName)
                            .replace(/@ID_Register/gi, UserID)
                            .replace(/@RegisterName/gi, UserName)
                            .replace(/@RegistDate/gi, CurDate)
                            .replace(/@XFeeChargeStaute/gi, "")
                            ;
    }
    jQuery(obj).parent().parent().remove();
    if (newContent != '') {
        jQuery("#tblList tbody").append(newContent);
        newContent = "";
        DoScrollToBottom();
        SumJG(); //计算合计
    }
    ResetSearch();
}
function SureAdd() {
    var newContent = '', checked = false, ID_Fee = '', userName = '', date = '', FeeName = '', Price = 0, Discount = 0, FactPrice = 0, FeeType = document.getElementById("slFeeWay").value, ID_Section = '', SectionName = '';
    var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateRegistCustFee");
    var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
    var teamTaskGroupFeeListBodyTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
    var RowNum = jQuery("#tblList tbody tr[id!='loading']").length;
    jQuery("#showBusFeeItem tr[id!='loading'] td input[name='ItemCheckboxX']").each(function () {
        checked = jQuery(this).attr("checked");
        if (checked) {
            RowNum++;
            ID_Fee = jQuery(this).parent().parent().attr("id");
            userName = jQuery(this).parent().parent().attr("userName");
            date = jQuery(this).parent().parent().attr("date");
            FeeName = jQuery(this).parent().parent().attr("FeeName");
            Price = jQuery(this).parent().parent().attr("Price");
            //Discount = jQuery(this).parent().parent().attr("Discount");
            FactPrice = jQuery(this).parent().parent().attr("FactPrice");
            CustFeeChargeState = jQuery(this).parent().parent().attr("CustFeeChargeState");
            ID_Section = jQuery(this).parent().parent().attr("ID_Section");
            SectionName = jQuery(this).parent().parent().attr("SectionName");
            if (teamTaskGroupFeeListBodyTempleteContent != null) {
                newContent += teamTaskGroupFeeListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@ItemCheckbox/gi, "ItemCheckbox")
                            .replace(/@ID_TeamTaskGroup/gi, "")
                             .replace(/@exist/gi, 0)
                            .replace(/@Is_Org/gi, 0)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_Fee/gi, ID_Fee)
                            .replace(/@FeeName/gi, FeeName)
                            .replace(/@FeeChargeStaute/gi, "")
                            .replace(/@Price/gi, Price)
                            .replace(/@FactPrice/gi, FactPrice)
                            .replace(/@Is_FeeRefund/gi, 0)
                            .replace(/@Is_FeeCharged/gi, 0)
                            .replace(/@Discount/gi, 10)

                            .replace(/@ID_Section/gi, ID_Section)
                            .replace(/@SectionName/gi, SectionName)
                            .replace(/@date/gi, item.date)
                            .replace(/@FeeType/gi, FeeType)
                            .replace(/@FeeWayName/gi, jQuery(allHiddFeeWay).find("option[value='" + FeeType + "']").text())
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@CustCssStyle/gi, "NewXM")
                            .replace(/@ID_Section/gi, ID_Section)
                            .replace(/@SectionName/gi, SectionName)
                            .replace(/@ID_CustFee/gi, "")
                            .replace(/@CustFeeChargeState/gi, "0")
                            .replace(/@Is_Printed/gi, "0")

                            .replace(/@ID_Discounter/gi, DisUserID)
                            .replace(/@XDiscounterName/gi, DisUserName)
                            .replace(/@ID_Register/gi, UserID)
                            .replace(/@RegisterName/gi, UserName)
                            .replace(/@RegistDate/gi, CurDate)
                            .replace(/@XFeeChargeStaute/gi, "")
                            ;
            }
            jQuery(this).parent().parent().remove();
            //设置下一个元素选中

        }
    });
    if (newContent != '') {
        IsSaved = false;
        jQuery("#tblList tbody").append(newContent);
        newContent = "";
        DoScrollToBottom();
        BindFeeWay();
        BindKeyup();
        ResetSearch();
        DoClose();
    }
    SumJG(); //计算合计

    return false;
}
//绑定搜索项获取光标即选中
function BindSelect() {
    jQuery("#showBusFeeItem input[type='text']").focus(function () {
        this.select();
    });

}

function DoClose() {
    jQuery("#showBusFeeItem").empty();
    jQuery("#showBusFee").hide();
}
function DoSelectAll() {
    jQuery("#txtSearch").select();
}

/**************************用于套餐外项目添加 End***********************************/

function DoScrollToBottom() {
    // window.scrollTo(0, document.body.scrollHeight - 100);
}
/// <summary>
/// 滚动指定高度
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
function DoScrollValueToBottom(scrollHeight) {
    // window.scrollTo(0, document.body.scrollHeight - 100);
    var top = (document.documentElement.scrollTop + document.documentElement.clientHeight + scrollHeight);
    //var left = document.body.scrollWidth; //(document.documentElement.scrollLeft + document.documentElement.clientWidth - 50);
    window.scrollTo(0, top);
}


/*************键盘操作事件：主要为上下左右键，适用于table******************/
function keyMove(item, event) {

    var elementID = "externTBlList";
    var maxX = document.getElementById(elementID).rows[0].cells.length;   //计算表格有列数
    var maxY = document.getElementById(elementID).rows.length;            //计算表格行数
    var objTable = document.getElementById(elementID); 						//获取table
    var c = item.parentNode.cellIndex; 										//获取当前列的下标，因为列中有文本框，取父级
    var row = item.parentNode; 											    //获取当前行的下标
    while (row.tagName != "TR") row = row.parentNode;
    var r = row.rowIndex; var x = r; var y = c;
    if (event.keyCode == 40) {
        if (r < maxY - 1) {
            x = r + 1;
            y = c;
        }
    }
    if (event.keyCode == 38) {
        if (r > 0) {
            x = r - 1;
            y = c;
        }
    }
    if (event.keyCode == 39) {
        if (c <= maxX - 2) {
            x = r;
            y = c + 1;
        }
    }
    if (event.keyCode == 37) {
        if (c > 0) {
            x = r;
            y = c - 1;
        }
    }
    if (objTable.rows[x].style.display == "none")
        return;
    if (objTable.rows[x].cells[y] != undefined) {
        if (objTable.rows[x].cells[y].children[0] != undefined) {

            //回车默认选中当前行
            if (event.keyCode == 13) {
                objTable.rows[x].cells[0].children[0].checked = !objTable.rows[x].cells[0].children[0].checked;
                if (objTable.rows[x].cells[0].children[0].name == "checkAllX") {
                    checkAllChildren(objTable.rows[x].cells[0].children[0]);
                }
            }
            if (objTable.rows[x].cells[y].children[0].type != "button") {
                objTable.rows[x].cells[y].children[0].blur();
                objTable.rows[x].cells[y].children[0].focus();
                if (objTable.rows[x].cells[y].children[0].id != "txtSearch") {
                    objTable.rows[x].cells[y].children[0].select();
                    //这里自动新增选中行数据
                    if (event.keyCode == 13) {
                        //判断是不是全选行
                        if (objTable.rows[x].cells[y].children[0].name != "checkAllX") {
                            SureAddCurrentRow(objTable.rows[x].cells[y].children[0]);
                            //滚动
                            DoScrollValueToBottom(10);
                            if (objTable.rows[x] != undefined) {
                                //                            objTable.rows[x].cells[y].children[0].focus();
                                //                            objTable.rows[x].cells[y].children[0].select();
                                //                        }
                                //                        else {

                            }
                            else {
                                //ResetSearch();
                            }

                        }
                        //ResetSearch();
                    }
                }
            }
            else {

            }
        }
    }
    return;
}

function ResetSearch() {
    jQuery("#chbAll1").attr("checked", false);
    if (jQuery("#txtSearch").is(":visible") == true) {
        document.getElementById("txtSearch").focus();
        document.getElementById("txtSearch").select();
    }
    document.getElementById("txtSearch").value = " ";
}
/*********获取浏览器代理**************/
function GetCLientAgent() {

    if (navigator.userAgent.indexOf("MSIE") > -1) {
        return "MSIE";
    }
    else if (navigator.userAgent.indexOf("Firefox") > -1) {
        return "Firefox";
    }
}


//编辑页面或者新增页面新增预约登记
function AddNewRegistOrSign(IsCheck) {
    //判断是否有没有保存的项
    if (IsCheck == 1) {
        var length = jQuery("#tblList tbody tr[id!='loading'][id_customer='undefined']").length;
        if (length > 0) {
            if (confirm("列表中存在未保存的项，您需要保存吗？")) {
                jQuery("#btnRegiste1").click();
            }
        }
    }

    //重新设置当前操作类型为新增：Add
    type = "add";
    //    if (operType != undefined) {
    //        operType.SetID(type);
    //    }
    SetShowElement(); //设置体检号、出生日期、性别显示方式
    IsSaved = false;
    jQuery("#ckbRegiste").removeAttr("disabled");
    ResetAllCustomerInfo();
    //设置头像
    jQuery("#HeadImg").attr("src", defalutImagSrc);
    jQuery("#HeadImg").attr("Base64Photo", "");
    document.getElementById("txtSFZ").value = "";
    document.getElementById("txtYKT").value = "";
    document.getElementById("txtTJH").value = "";
    document.getElementById("txtCustomer").value = "";
    document.getElementById("txtEmail").value = "";
    document.getElementById("txtBirthDay").value = "";
    document.getElementById("txtMobil").value = "";
    document.getElementById("txtNote").value = "";
    if (document.getElementById("txtSubScribDate") != null) {
        document.getElementById("txtSubScribDate").value = "";
    }
    //清除套餐
    jQuery("#tblList tbody").html("");
    jQuery("#slSex").change(); //触发slSex change事件

    ResetSearch(); //设置光标
    //设置无证按钮可见
    jQuery("#btnGenerate").show();
    jQuery("[name='btnAdd']").hide();

    jQuery("#slOperateLevel").html(SecurityLevelHtml);              //设置用户操作密级
    jQuery("#txtSubScribDate").show();                              //显示体检时间
    jQuery("#txtSubScribDate").val(CurDate);                        //设置体检时间默认为当天
    jQuery("#txtSearchX").val(""); //置空检索值 xmhuang 2013-09-10
    jQuery("#txtSearchX").focus();
    jQuery("#divAddCustFee").show(); //显示新增收费项目操作组

    jQuery("#lblHidCustomer").text(""); //置空体检号的值 xmhuang 2013-09-27
    jQuery("#txtTJH").val(""); //置空体检号的值 xmhuang 2013-09-27
    jQuery("#didTeam").html(""); //置空团体信息值 xmhuang 2013-09-27

    jQuery("#imgCustomer").show(); //显示获取图片按钮 xmhuang 2013-10-28
}
/**************通过身份证号码获取用户基本信息**********************/
function PostImg(Base64Photo, objID, Key, IsLoadCustomer) {
    var modelName = jQuery("#modelName").val();
    var KeyValue = jQuery.trim(jQuery("#" + objID).val());
    var IDCard = jQuery.trim(jQuery("#lblSFZ").text()); // document.getElementById("txtSFZ").value; //身份证号码
    var CustomerName = document.getElementById("txtCustomer").value; //客户名称
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var BirthDay = document.getElementById("txtBirthDay").value; //出生年月
    var Nation = jQuery("#idSelectNation").val();               //名族ID
    var NationName = jQuery("#nameSelectNation").val();
    //    if (Nation == "") {
    //        Nation = 1;
    //        NationName = NationArray[Nation - 1];
    //    }
    var tempBase64Photo = Base64Photo;
    //var IDCard = jQuery.trim(jQuery("#txtSFZ").val());
    if (IDCard != "") {
        var Is_Org = 0;
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxRegiste.aspx",
            contentType: "application/x-www-form-urlencoded;Content-length=1024000",
            data: { action: 'SaveCustomerInfo',
                IDCard: IDCard,
                CustomerName: CustomerName,
                Gender: Gender,
                GenderName: GenderName,
                BirthDay: BirthDay,
                Nation: Nation,
                NationName: NationName,
                Base64Photo: tempBase64Photo,
                Key: Key,
                KeyValue: KeyValue,
                type: type,
                modelName: modelName,
                IsLoadCustomer: IsLoadCustomer
            },
            cache: false,
            dataType: "json",
            success: function (msg) {
                //IsLoadCustomer==0表示不加载用户信息
                if (IsLoadCustomer == 2) {
                    //只检索基本信息
                    if (msg.dataList != undefined) {
                        var item = msg.dataList[0];
                        jQuery("#txtSFZ").val(item.IDCard);
                        jQuery("#lblSFZ").text(item.IDCard);
                        jQuery("#txtCustomer").val(item.CustomerName);
                        jQuery("#txtYKT").val(item.ExamCard);
                        jQuery("#txtBirthDay").val(item.date);
                        jQuery("#lblHidBirthDay").text(item.date); //设置出生日期
                        jQuery("#txtMobil").val(item.MobileNo);
                        jQuery("#txtEmail").val(item.Email);
                        jQuery("#lblHidSex").text(item.GenderName); //设置性别
                        jQuery("#slSex [value='" + parseInt(item.ID_Gender) + "']").attr("selected", true);
                        jQuery("#slSex").change();
                        jQuery("#slNation [value='" + parseInt(item.NationID) + "']").attr("selected", true);
                        jQuery("#slMarried [value='" + item.ID_Marriage + "']").attr("selected", true);
                        jQuery("#slCultrul [value='" + item.CultrulID + "']").attr("selected", true);
                    }
                }
                else if (IsLoadCustomer == 1) {
                    if (msg.dataList != undefined) {
                        //mark:此处验证为什么扫描身份证没有绑定客户名族和身份证信息 2013-08-19
                        var msgLength = msg.dataList.length;

                        //存在用户基本信息
                        //如果客户信息大于两条则提示操作者选择客户信息后绑定其基本信息
                        if (msgLength > 0) {
                            //只存在一个客户信息
                            if (msgLength == 1) {
                                var IsLoadCustomerInfo = 1; //默认设置检索
                                //xmhuang 2013-09-27 经讨论决定，不管新增还是编辑检索身份证都将检索客户收费信息
                                //                                if (type.toLowerCase() == "add")//新增状态下不检索客户体检信息
                                //                                {
                                //                                    IsLoadCustomerInfo = 0; //设置不检索
                                //                                }
                                SetCustomerInfo(msg.dataList, '', IsLoadCustomerInfo); //设置客户基本信息，并设置是否检索可体检信息
                            }
                            //存在相同身份证多个客户的情况
                            else {
                                ChooseCustomerInfo(msg.dataList);
                            }
                        }
                    }
                }
            }
        });
    }
}
function ReadBustFeeData(Is_Org, dataList) {
    //非团队
    //    if (Is_Org == 0 || Is_Org.toLowerCase() == "False".toLowerCase())
    //        return false;
    if (dataList != undefined) {
        if (dataList.length > 0) {

            //这里读取模版
            var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateRegistCustFee");
            var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
            var teamTaskGroupFeeListBodyTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
            //绑定套餐信息
            var newContent = '', CustCssStyle = '', RowNum = 0, exist = 0, ItemCheckbox = "ItemCheckbox";
            var XFeeChargeStaute = "";
            //清除套餐
            jQuery("#tblList tbody").html("");
            jQuery(dataList).each(function (i, item) {
                if (jQuery("#tblList tr[id!='" + item.ID_Fee + "']").length > 0) {
                    RowNum++;
                    XFeeChargeStaute = "";
                    //如果是团体项目则不允许删除，隐藏checkbox
                    if (item.ID_TeamTaskGroup != "") {
                        exist = 1;
                        ItemCheckbox = "UnItemCheckbox";
                        CustCssStyle = "Yellow";

                        //判断收费状态
                        if (item.FeeChargeStaute == "2") {
                            ItemCheckbox = "UnItemCheckbox";
                            CustCssStyle = "Yellow";
                            item.FeeChargeStaute = "√";
                            XFeeChargeStaute = "√";
                        }
                        //判断是否是已检项目，已检项目不允许退费Is_Examined
                        else if (item.FeeChargeStaute == "1") {
                            ItemCheckbox = "UnItemCheckbox";
                            item.FeeChargeStaute = "√";
                            CustCssStyle = "Green";
                        }
                        else if (item.FeeChargeStaute == "0") {
                            ItemCheckbox = "UnItemCheckbox";
                            item.FeeChargeStaute = "";
                            CustCssStyle = "TeamRed";
                        }
                    }
                    else {
                        //判断收费状态
                        if (item.FeeChargeStaute == "2") {
                            ItemCheckbox = "UnItemCheckbox";
                            CustCssStyle = "Yellow";
                            item.FeeChargeStaute = "√";
                            XFeeChargeStaute = "√";
                        }
                        //判断是否是已检项目，已检项目不允许退费Is_Examined
                        else if (item.FeeChargeStaute == "1") {

                            item.FeeChargeStaute = "√";
                            ItemCheckbox = "UnItemCheckbox";
                            CustCssStyle = "Green";
                        }
                        else if (item.FeeChargeStaute == "0") {
                            item.FeeChargeStaute = "";
                            CustCssStyle = "Red";
                        }
                    }

                    //判断是修改还是新增
                    if (type.toLowerCase() == "edit")
                    { exist = 1; }
                    if (item.ID_Customer != "") {
                        exist = 1;
                    }
                    if (teamTaskGroupFeeListBodyTempleteContent != null) {
                        newContent += teamTaskGroupFeeListBodyTempleteContent.replace(/@type=text/gi, "")
                            .replace(/@type="text"/gi, "")
                            .replace(/@ItemCheckbox/gi, ItemCheckbox)
                            .replace(/@ID_TeamTaskGroup/gi, item.ID_TeamTaskGroup)
                             .replace(/@exist/gi, exist)
                            .replace(/@Is_Org/gi, Is_Org)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_Fee/gi, item.ID_Fee)
                            .replace(/@FeeName/gi, item.FeeName)
                            .replace(/@FeeChargeStaute/gi, item.FeeChargeStaute)
                            .replace(/@Price/gi, item.Price)
                            .replace(/@FactPrice/gi, item.FactPrice)
                            .replace(/@Is_FeeRefund/gi, item.Is_FeeRefund)
                            .replace(/@Is_FeeCharged/gi, item.Is_FeeCharged)
                            .replace(/@Discount/gi, item.Discount)
                            .replace(/@XDiscounterName/gi, item.DiscounterName)
                            .replace(/@ID_Section/gi, item.ID_Section)
                            .replace(/@SectionName/gi, item.SectionName)
                            .replace(/@date/gi, item.date)
                            .replace(/@FeeType/gi, item.ID_FeeType)
                            .replace(/@FeeWayName/gi, jQuery(allHiddFeeWay).find("option[value='" + item.ID_FeeType + "']").text())
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@CustCssStyle/gi, CustCssStyle)
                            .replace(/@ID_CustFee/gi, item.ID_CustFee)
                            .replace(/@CustFeeChargeState/gi, item.CustFeeChargeState)
                            .replace(/@Is_Printed/gi, item.Is_Printed)
                            .replace(/@XFeeChargeStaute/gi, XFeeChargeStaute)


                            .replace(/@Discount/gi, item.Discount)
                            .replace(/@ID_Discounter/gi, item.ID_Discounter)
                            .replace(/@XDiscounterName/gi, item.DiscounterName)
                            .replace(/@ID_Register/gi, item.ID_Register)
                            .replace(/@RegisterName/gi, item.RegisterName)
                            .replace(/@RegistDate/gi, item.RegistDate)

                            ;
                    }
                }
            });
            if (newContent != '') {
                jQuery("#tblList tbody").empty();
                jQuery("#tblList tbody").append(newContent);
                newContent = "";
                DoScrollToBottom();
                BindFeeWay();
                BindKeyup();
            }
            jQuery("#idSelectSet").removeData("ID_Customer"); //移除数据项
            SumJG(); //计算合计
            jQuery("#lblErrorMessage").text("");
        }
    }
}
//选择用户基本信息,调用此方法时，用户身份证信息是>1条的
function ChooseCustomerInfo(dataList) {

    //ID_ArcCustomer,IDCard,CustomerName,CustomerName,ExamCard,ID_Gender
    var content = '<table id="tbChoose" class="tblList tblExamItemTable">';
    var imgSrc = "";
    jQuery(dataList).each(function (i, item) {
        if (item.Base64Photo == "") {
            imgSrc = defalutImagSrc;
        }
        else {
            imgSrc = "data:image/gif;base64," + item.Base64Photo;
        }
        content += ' <tr ID_ArcCustomer="' + item.ID_ArcCustomer + '" IDCard="' + item.IDCard + '" CustomerName="' + item.CustomerName + '" onclick="SureChoose(this)"><td rowspan="4"><img id="HeadImg1" name="HeadImg1" class="headPic" src="' + imgSrc + '" /></td><td>' + item.IDCard + '</td></tr>';
        content += '<tr><td>' + item.CustomerName + '</td></tr>';
        content += '<tr><td>' + item.date + '</td></tr>';
        content += '<tr><td>' + item.GenderName + '</td></tr>';
        //content += '<tr id="trChoose_"' + item.ID_ArcCustomer + '" IDCard="trChoose_"' + item.IDCard + '" CustomerName="trChoose_"' + item.CustomerName + '"><td>' + item.IDCard + '</td><td>' + item.CustomerName + '</td><td>' + item.date + '</td><td>' + item.ID_Gender + '</td></tr>';
    });
    content += '</table>';

    var dialog = art.dialog({
        lock: true, fixed: true, opacity: 0.3, width: 320,
        height: 240,
        id: "dialog1",
        title: '请选择客户信息(点击图片即可选择)',
        content: content,
        follow: document.getElementById('txtSFZ')
    }).lock();
    jQuery(window).data("dataList", dataList);
}
function SureChoose(obj, dataList) {
    var ID_ArcCustomer = jQuery(obj).attr("ID_ArcCustomer");
    var CurdataList = jQuery(window).data("dataList");
    SetCustomerInfo(CurdataList, ID_ArcCustomer, 1);
    CloseDialog("dialog1");
}
function CloseDialog(dialogID) {
    var list = art.dialog.list;
    for (var i in list) {
        if (i == dialogID) {
            list[i].close();
        }
    };
}
/// <summary>
/// 设置用户基本信息
/// <param name="dataList">数据集，此数据集包含客户的基本信息</param>
/// <param name="selectedID_ArcCustomer">当前选中的客户存档ID，一般在身份证重合才会为其赋值，默认情况为空</param>
/// <param name="IsLoadCustomerInfo">是否加载客户体检号，本方法一般通过客户身份证号码、体检号、一卡通号码进行检索获取客户基本信息。
///但是在新增状态下，值允许设置客户基本信息，而不允许设置客户体检号，客户收费项目信息等。此标记用于区分新增或修改操作
///</param>
/// </summary>
/// <returns></returns>
function SetCustomerInfo(dataList, selectedID_ArcCustomer, IsLoadCustomerInfo) {

    if (dataList == null || dataList == undefined)
        return false;
    var item;
    for (var c = 0; c < dataList.length; c++) {
        item = dataList[c];
        if (selectedID_ArcCustomer != "" && selectedID_ArcCustomer != item.ID_ArcCustomer) {
            continue;
        }
        //设置头像
        if (item.Base64Photo == "") {
            jQuery("#HeadImg").attr("src", defalutImagSrc);
        }
        else {
            jQuery("#HeadImg").attr("src", "data:image/gif;base64," + item.Base64Photo + "");
            jQuery("#HeadImg").attr("Base64Photo", item.Base64Photo);
        }
        //绑定用户基本信息
        jQuery("#txtSFZ").val(item.IDCard);
        jQuery("#lblSFZ").text(item.IDCard);
        jQuery("#txtCustomer").val(item.CustomerName);
        jQuery("#txtYKT").val(item.ExamCard);
        jQuery("#txtBirthDay").val(item.date);
        jQuery("#lblHidBirthDay").text(item.date); //设置出生日期
        jQuery("#txtMobil").val(item.MobileNo);
        jQuery("#txtEmail").val(item.Email);
        jQuery("#lblHidSex").text(item.GenderName); //设置性别
        jQuery("#slSex [value='" + parseInt(item.ID_Gender) + "']").attr("selected", true);
        jQuery("#slSex").change();
        //jQuery("#slNation [value='" + parseInt(item.NationID) + "']").attr("selected", true);
        jQuery("#slMarried [value='" + item.ID_Marriage + "']").attr("selected", true);
        jQuery("#slCultrul [value='" + item.CultrulID + "']").attr("selected", true);
        if (item.NationID != "") {
            ShowQuickSelectNation(parseInt(item.NationID), NationArray[parseInt(item.NationID) - 1]);          // 设置民族的已选项 xmhuang 2013-10-18 获取客户基本信息，需要使用此方法设置民族
        }
        if (IsLoadCustomerInfo == 0)//不加载客户体检号信息和客户收费项目信息
        {
            return false;
        }
        else {
            //判断ID_Customer是否存在，如果存在，则直接使用体检号进行检索
            if (item.ID_Customer != undefined) {
                jQuery("#txtTJH").val(item.ID_Customer); //设置体检号
                jQuery("#lblHidCustomer").text(item.ID_Customer); //设置体检号
                GetCustomerExamPhysicalInfo("", item.ID_Customer, IsLoadCustomerInfo); //通过存档ID获取用户体检信息
            }
            else {
                GetCustomerExamPhysicalInfo(item.ID_ArcCustomer, "", IsLoadCustomerInfo); //通过存档ID获取用户体检信息
            }
        }
    }
    jQuery("#lblErrorMessage").text("");
}
/// <summary>
/// 获取用户体检信息和设置套餐内容
///xmhuang 2013-09-30 新增IsTeam参数，用于针对团体检索只检索最近一次此客户对应团体的最近一次体检信息
/// </summary>
/// <returns></returns>
function GetCustomerExamPhysicalInfo(ID_ArcCustomer, ID_Customer, IsLoadCustomerInfo) {
    if (ID_ArcCustomer != "" || ID_Customer != "") {
        var Is_Org = 0;
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxRegiste.aspx",
            data: { action: 'GetCustomerExamPhysicalInfo',
                ID_ArcCustomer: ID_ArcCustomer,
                ID_Customer: ID_Customer,
                IsTeam: IsTeam
            },
            cache: false,
            dataType: "json",
            success: function (msg) {
                if (msg.dataList0.length > 0) {
                    //mark
                    jQuery(msg.dataList0).each(function (i, item) {

                        //xmhuang 2013-09-10 判断用户体检号是否已加密
                        if (item.SecurityLevel > 100)//已加密客户不允许查看
                        {
                            jQuery("#divCustomerInfo").parent().next().hide();
                            ResetAllCustomerInfo();
                            ShowSystemDialog("对不起，该客户已加密，不允许查看！");
                            return false;
                        }
                        else if (item.Is_Checked == 1 || item.Is_Checked == "True")//已完成总检不允许修改即保存
                        {
                            jQuery("[name='btnComplete']").hide(); //隐藏保存按钮
                            jQuery("#divAddCustFee").hide(); //隐藏新增收费项目操作组
                            jQuery("#imgCustomer").hide(); //隐藏获取头像按钮
                        }
                        else {
                            jQuery("[name='btnComplete']").show(); //显示保存按钮
                            jQuery("#divAddCustFee").show(); //显示新增收费项目操作组
                            jQuery("#imgCustomer").show(); //显示获取头像按钮
                            jQuery("#divCustomerInfo").parent().next().show();
                        }

                        /************************设置团体和非团体显示信息和体检信息  Begin*****************************/
                        if (item.Is_Team == "True")//如果是团体
                        {
                            Is_Org = "1";
                            item.RoleName = item.RoleName == undefined ? "" : item.RoleName;
                            jQuery("#didTeam").html('团体ID:' + item.ID_Team + '; </br>团体名称:' + item.TeamName + ';</br> 部门名称:' + item.Department + '-' + item.DepartSubA + '-' + item.DepartSubB + '-' + item.DepartSubC + '</br>角色：' + item.RoleName);
                            jQuery("#didTeam").data("ID_Team", item.ID_Team);
                        }
                        else {
                            Is_Org = "0";
                            jQuery("#didTeam").html('');
                        }

                        ShowQuickSelectExamType(item.ID_ExamType, item.ExamTypeName);    // 设置体检类型的已选项
                        /************************设置团体和非团体显示信息和体检信息  End*****************************/
                        if (type.toLowerCase() == "add" && IsSearchIDCustomer == 0 && item.Is_Team != "True")//新增，并且检索的是身份证号
                        {
                            jQuery("#txtSubScribDate").val(CurDate); jQuery("#lblSubScribDate").text(CurDate);
                        }
                        else {

                            SetCustomerSubscribedInfo(item.Is_Subscribed); //设置客户预约还是登记信息
                            jQuery("#lblCode128c").text(item.Code128c); //设置Code128c码，用于单据打印.
                            jQuery("#idSelectSet").data("ID_Customer", item.ID_Customer);
                            //xmhuang 2013-09-28 由于从设备读取身份证信息只检索客户基本信息，不可用设置体检号和收费项目信息
                            var ReadCustomerFromCard = jQuery("#btnReadFromMachine").data("ReadCustomerFromCard");
                            if (ReadCustomerFromCard == 1) {
                                RemoveSelectedUser();    // 设置导检护士的已选人员
                                RemoveSelectedSet();    // 设置体检类型的已选项
                                jQuery("#lblHidCustomer").text(""); //重置体检号
                                jQuery("#txtTJH").val(""); //重置体检号
                                jQuery("#txtSubScribDate").val(CurDate); //设置登记时间为当天
                                jQuery("#lblSubScribDate").text(CurDate); //设置登记时间为当天
                                //jQuery("#btnReadFromMachine").data("ReadCustomerFromCard", 0); //重置来源于设备读取标记

                            }
                            //xmhuang 2013-09-28 由于从设备读取身份证信息只检索客户基本信息，不可用设置体检号和收费项目信息
                            else {
                                ShowQuickSelectUser(item.ID_GuideNurse, item.GuideNurse);    // 设置导检护士的已选人员
                                ShowQuickSelectNation(item.NationID, item.NationName);          // 设置民族的已选项
                                jQuery("#lblHidCustomer").text(item.ID_Customer); jQuery("#txtTJH").val(item.ID_Customer); //设置体检号
                                jQuery("#txtSubScribDate").val(item.SubScribDateX); jQuery("#lblSubScribDate").text(item.SubScribDateX);
                                jQuery("#txtNote").val(item.Note); //xmhuang 2013-10-26 设置备注信息
                                //如果是团体，这里不可用自动触发套餐变动事件，需要直接绑定其对应套餐信息
                                //if (item.Is_Team == "True") {
                                if (IsSearch == 1)//如果是检索身份证获取是体检号或者是一卡通，则需要通过体检号绑定客户收费项目信息
                                {
                                    ShowQuickSelectSet(item.PEPackageID, item.PEPackageName, true); //绑定套餐并绑定其收费项目
                                }
                                else { ShowQuickSelectSet(item.PEPackageID, item.PEPackageName, false); }
                                IsSearch = 0;
                                //设置收费类型 item.ID_FeeWay xmhuang 2013-10-18
                                if (item.ID_FeeWay != "") {
                                    jQuery("#slFeeWay [value='" + item.ID_FeeWay + "']").attr("selected", true);
                                }

                            }
                            //如果是团体新增登记或者查看登记内容都需要绑定其套餐等内容
                            //设置选中套餐
                        }

                        IsSearchIDCustomer = -1;
                    }

                    );
                }
                else {
                    //未检索到此人的团体信息
                    ShowSystemDialog("对不起,此客户为个人用户，请到个人登记处");
                    return false;
                }
                //设置预约、登记时间
                if (jQuery.trim(jQuery("#txtSubScribDate").val()) == "") {
                    jQuery("#txtSubScribDate").val(CurDate);
                }

                //修改人：黄兴茂 修改日期：2013-07-29 修改内容：个人新增登记、预约毋须绑定收费项目
                if (jQuery("#didTeam").data("ID_Team") != undefined) {
                    if (msg.dataList1.length > 0) {
                        return false; ReadBustFeeData(Is_Org, msg.dataList1);
                    }
                }
            }
        });
    }
}
function RequestCustomerInfo(objID, Key, IsLoadCustomerInfo) {

    var CustomerName = jQuery.trim(jQuery("#txtCustomer").val());
    var modelName = jQuery("#modelName").val();
    var KeyValue = jQuery.trim(jQuery("#" + objID).val());

    //组建请求参数
    var Is_Org = 0;
    var data = { action: "GetCustomerByIDCardX", Key: Key, KeyValue: KeyValue, CustomerName: encodeURIComponent(CustomerName), type: type, modelName: modelName };
    jQuery.ajax({
        //async: false,
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: data,
        cache: false,
        dataType: "json",
        success: function (msg) {

            jQuery("#lblErrorMessage").text("获取到用户信息,正在整理数据...");
            //修改人：黄兴茂 修改时间：2013-08-01 修改内容：屏蔽此处，由于检索后是在获取到客户体检信息时才触发套餐变动事件
            //SetControlDefalut();
            //设置头像
            jQuery("#HeadImg").attr("src", defalutImagSrc);
            jQuery("#HeadImg").attr("Base64Photo", "");
            //绑定用户基本信息
            jQuery("#txtCustomer").val("");
            jQuery("#txtYKT").val("");
            jQuery("#slSex").val("");
            jQuery("#slMarried").val("");
            jQuery("#txtBirthDay").val("");
            jQuery("#txtMobil").val("");
            jQuery("#txtEmail").val("");
            var msgLength = msg.dataList.length;
            //存在用户基本信息
            //如果客户信息大于两条则提示操作者选择客户信息后绑定其基本信息
            if (msgLength > 0) {
                //只存在一个客户信息
                if (msgLength == 1) {
                    SetCustomerInfo(msg.dataList, '', IsLoadCustomerInfo);
                }
                //存在相同身份证多个客户的情况
                else {
                    ChooseCustomerInfo(msg.dataList);
                }
            }
            else {
                //mark xmhuang 2013-09-27 ????
                if (IsFromGenerateCustomerCard == 0) {
                    ShowSystemDialog("对不起,未检索到客户体检信息");
                }
                //                jQuery("#lblErrorMessage").text("从服务器获取用户基本信息失败！");
                //                //判断当前检索的是身份证还是体检号，对应设置值
                //                if (isCustomerExamNo(jQuery.trim(jQuery("#txtSearchX").val()))) {
                //                    jQuery("#lblHidCustomer").val(jQuery.trim(jQuery("#txtSearchX").val()));
                //                    jQuery("#txtTJH").text(jQuery.trim(jQuery("#txtSearchX").val()));
                //                }
                //                else {
                //                    jQuery("#txtSFZ").val(jQuery.trim(jQuery("#txtSearchX").val()));
                //                    jQuery("#lblSFZ").text(jQuery.trim(jQuery("#txtSearchX").val()));
                //                }
                //jQuery("#lblErrorMessage").text("");
                SetGenerateCustomerCard();
            }
            //修改人：黄兴茂 修改日期：2013-07-29 修改内容：个人新增或者登记毋须绑定收费项目信息

            if (msg.dataList1 != null) {

                if (msg.dataList1.length > 0) {
                    ReadBustFeeData(Is_Org, msg.dataList1);
                }
            }
            jQuery("#lblErrorMessage").text("");
            return false;
        }
    });

    return false;

}

/****************缴费通知单、记账通知单 Begin********************/

///判断是否可以打印指引单
function IsCanPrint(ID_Customer) {
    //GetCustomerPrint
    if (ID_Customer == "")
        return { "dataList": "" };
    else {
        var allISDeleteDT = ""; //当前团体所有不可用于删除的团体ID，团体任务，团体任务分组，体检号
        var flag = false;
        jQuery.ajax({
            type: "GET",
            async: false,
            url: "/Ajax/AjaxRegiste.aspx",
            data: { action: "GetCustomerPrint", ID_Customer: ID_Customer },
            cache: false,
            dataType: "json",
            success: function (msg) {
                allISDeleteDT = msg;
            }
        });
        return allISDeleteDT;
    }
}

///
///feeTypeName:收费类型：收费通知、记账通知
///operType：操作类型:View或者Edit
///ID_Customer：体检号
///AllIDFee：体检号对应的所有收费项目ID
function PrintCust(feeTypeName, operType, jsonMsg, AllIDFee) {
    //如果没有项目则不允许打印
    var objBusList = jQuery("#tblList tr[name='busList']");
    if (objBusList.length == 0) {
        ShowSystemDialog("请您添加体检项目！");
        return false;
    }
    if (!IsSaved) {
        ShowSystemDialog("收费项目中存在尚未保存的项！");
        return false;
    }
    var Is_FeeSettled = "";
    if (jsonMsg != undefined) {
        if (jsonMsg.Is_FeeSettled != undefined) {
            Is_FeeSettled = jsonMsg.Is_FeeSettled;
        }
    }
    var SubScribDate = jQuery.trim(jQuery("#txtSubScribDate").val()); //客户预约、登记时间
    var ID_Customer = jQuery.trim(jQuery("#txtTJH").val());
    var ID_CustomerCode128 = jQuery.trim(jQuery("#lblCode128c").text());
    var sumData = jQuery("#divSumHeader").data("sumData");
    var customerName = jQuery.trim(jQuery("#txtCustomer").val());
    var totalCustFee = 0;
    //如果是正常打印，则只打印未打印的应交费用 ，补打则直接打印未缴费的所有应交费用
    if (feeTypeName == 0) //补打
    {
        totalCustFee = sumData.yjfy; //sumData.xjzf;

    }
    else//正常打印
    {
        totalCustFee = sumData.sumUnPrintyjfy; //xmhuang 2013-10-15 调整成只打印未打印的应交费用
    }
    var upperWrite = chineseNumber(totalCustFee); //转换为大写
    var departmentName = "健康管理中心";
    var Date = CurDate; //页面公共变量中存放着当前服务器日期
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别 xmhuang 2013-10-12
    //是否是预约
    var TemplateName = "CustomerCharges.frx";
    var IsRegiste = document.getElementById("ckbRegiste").checked;
    var IsCanShowSaveMsg = true; //是否可以弹出保存信息，如果没有打印报表或者没有其它提示的情况下显示保存信息jQuery("[name='btnAdd']").data("ShowMsg", ShowMsg);
    //注释人：黄兴茂
    //注释时间：2013-07-29
    //注释原因：个人预约是在收费成功后打印的收费凭证
    //    if (IsRegiste) {
    //        TemplateName = "CustomerCredence.frx";
    //    }
    var para = {
        TemplateName: TemplateName,
        customerName: customerName,
        totalCustFee: totalCustFee,
        upperWrite: upperWrite,
        departmentName: departmentName,
        Date: Date,
        SubScribDate: SubScribDate
    };

    //获取打印数据
    var Data = IsCanPrint(ID_Customer).dataList[0];
    //判断是否是团体
    var Is_Subscribed = Data.Is_Subscribed;             //是否预约 1：是，0：否
    var ID_Team = Data.ID_Team;                         //团体ID
    var CustomerSubScribDate = Data.SubScribDate;       //客户体检时间
    var TaskExamStartDate = Data.TaskExamStartDate; //团体任务起始时间
    var TaskExamEndDate = Data.TaskExamEndDate;     //团体任务起始时间
    //如果已经完成收费，并且没有打印过指引单，则直接打印 xmhuang 2013-09-29
    if (Is_FeeSettled == "True") {
        if (ID_Team != "")                                  //如果是团体成员，则判断时间是否在时间段内
        {
            if (Date >= TaskExamStartDate && Date <= TaskExamEndDate)// 判断当前时间是否在备单时间段内
            {
                if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                    UpdateCustomerSubscribDateOfTeam_Ajax(ID_Customer, Date); //xmhuang 2013-10-12 如果是团体客户在任务时间范围内完成登记，则需要修改实际体检时间为登记时间
                    IsCanShowSaveMsg = false; 
                    //FastReport.GenerateCustomerGuide(UserID, UserName, ID_Customer, "guidesheet.frx", 0, 1);
                    SendWaitToInterfaceByClient_Ajax(Gender, ID_Customer); //xmhuang 2013-10-12 打印完指引单后向接口发送信息                   
                }
            }
            else {
                IsCanShowSaveMsg = false; ShowSystemDialog("客户所在团体预约体检时间为：[开始：" + TaskExamStartDate + "，结束：" + TaskExamEndDate + "]目前不能打印指引单！");
            }
        }
        else {
            //判断是登记还是预约
            if (Is_Subscribed == 1)                         //如果是预约，则需要判断时间是否为预约体检时间
            {
                if (Date == CustomerSubScribDate)           //判断当前时间是否和预约体检时间相等
                {
                    //该客户已经进行过登记或预约，有为收费项目则需要先打印收费单，没有收费项目则直接打印增项指引单
                    //判断是否存在非团体为收费的增项项目
                    if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                        //ShowSystemDialog("正在打印指引单，请稍候...");
                        IsCanShowSaveMsg = false; 
                        //FastReport.GenerateCustomerGuide(UserID, UserName, ID_Customer, "guidesheet.frx", 0, 1);
                        SendWaitToInterfaceByClient_Ajax(Gender, ID_Customer); //xmhuang 2013-10-12 打印完指引单后向接口发送信息  
                    }
                }
                else {
                    IsCanShowSaveMsg = false; ShowSystemDialog("客户预约体检时间为:[" + CustomerSubScribDate + "]目前不能打印指引单！");
                }
            }
            else if (Is_Subscribed == 0)//如果是登记，则判断登记时间和当前时间是否相同，相同则可用直接打印指引单
            {
                if (Date == CustomerSubScribDate) {
                    //该客户已经进行过登记或预约，有为收费项目则需要先打印收费单，没有收费项目则直接打印增项指引单
                    //判断是否存在非团体为收费的增项项目
                    if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                        IsCanShowSaveMsg = false; 
                        //FastReport.GenerateCustomerGuide(UserID, UserName, ID_Customer, "guidesheet.frx", 0, 1);
                        SendWaitToInterfaceByClient_Ajax(Gender, ID_Customer); //xmhuang 2013-10-12 打印完指引单后向接口发送信息  
                    }
                }
                else {
                    IsCanShowSaveMsg = false; ShowSystemDialog("客户登记体检时间为:[" + CustomerSubScribDate + "]目前不能打印指引单！");
                }
            }
        }
    }
    //如果已经完成收费，并且没有打印过指引单，则直接打印 xmhuang 2013-09-29
    else {
        //打印收费单
        if (feeTypeName == 0) {
            //判断如果未打印指引单，且交费金额为0
            if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                IsCanShowSaveMsg = false;
                //FastReport.GenerateCustomerCharges(ID_Customer, ID_CustomerCode128, para.TemplateName, "收费", para.customerName, para.totalCustFee, para.upperWrite, para.SubScribDate);
                UpdateCustFeePrintFlag(ID_Customer, AllIDFee); //xmhuang 2013-10-30 新增打印完交费通知单后修改收费项目打印标记为已打，已保证分批打印的正确性

            }
            else {
                //            if (para.totalCustFee > 0) {
                IsCanShowSaveMsg = false;
                //FastReport.GenerateCustomerCharges(ID_Customer, ID_CustomerCode128, para.TemplateName, "收费", para.customerName, para.totalCustFee, para.upperWrite, para.SubScribDate);
                UpdateCustFeePrintFlag(ID_Customer, AllIDFee); //xmhuang 2013-10-30 新增打印完交费通知单后修改收费项目打印标记为已打，已保证分批打印的正确性
                // }
            }
            //feeTypeName = "收费";
            //ShowDialogX("/template/blue/System/Report/RP_Registe_CustFee.htm?operType=" + operType + "&feeTypeName=" + feeTypeName + "&customerName=" + customerName + "&totalCustFee=" + totalCustFee + "&upperWrite=" + upperWrite + "&departmentName=" + departmentName + "&ID_Customer=" + ID_Customer + "&Date=" + Date, "我是弹出窗口", 650, 350, '我是弹出窗口');
        }
        else if (feeTypeName == 1) {

            feeTypeName = "收费";
            //var data = jQuery("#btnPrintCust1").data("data");
            //        var Is_GuideSheetPrinted = data.Is_GuideSheetPrinted;
            //        var Is_Subscribed = data.Is_Subscribed;
            //        var yyDate = SubScribDate; //  jQuery.trim(jQuery("#txtSubScribDate").val());
            //        var todayDate = CurDate; //  RegisteDateX; //客户登记是注册的预约、登记时间，只有到了这天方能打印指引单
            if (jQuery("#tblList tbody tr[id_teamtaskgroup=''][feechargestaute='']").length > 0) {
                IsCanShowSaveMsg = false;
                // FastReport.GenerateCustomerCharges(ID_Customer, ID_CustomerCode128, para.TemplateName, "收费", para.customerName, para.totalCustFee, para.upperWrite, para.SubScribDate);
                UpdateCustFeePrintFlag(ID_Customer, AllIDFee); //xmhuang 2013-10-30 新增打印完交费通知单后修改收费项目打印标记为已打，已保证分批打印的正确性
                // ShowDialogX("/template/blue/System/Report/RP_Registe_CustFee.htm?operType=" + operType + "&feeTypeName=" + feeTypeName + "&customerName=" + customerName + "&totalCustFee=" + totalCustFee + "&upperWrite=" + upperWrite + "&departmentName=" + departmentName + "&ID_Customer=" + ID_Customer + "&Date=" + Date, "我是弹出窗口", 650, 350, '我是弹出窗口');
            }
            else {
                if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {

                    if (ID_Team != "")                                  //如果是团体成员，则判断时间是否在时间段内
                    {

                        if (Date >= TaskExamStartDate && Date <= TaskExamEndDate)// 判断当前时间是否在备单时间段内
                        {
                            if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                                UpdateCustomerSubscribDateOfTeam_Ajax(ID_Customer, Date); //如果是团体客户在任务时间范围内完成登记，则需要修改实际体检时间为登记时间
                                IsCanShowSaveMsg = false; 
                                //FastReport.GenerateCustomerGuide(UserID, UserName, ID_Customer, "guidesheet.frx", 0, 1);
                                SendWaitToInterfaceByClient_Ajax(Gender, ID_Customer); //xmhuang 2013-10-12 打印完指引单后向接口发送信息  
                            }
                        }
                        else {
                            IsCanShowSaveMsg = false; ShowSystemDialog("客户所在团体预约体检时间为：[开始：" + TaskExamStartDate + "，结束：" + TaskExamEndDate + "]目前不能打印指引单！");
                        }
                    }
                    else {
                        //判断是登记还是预约
                        if (Is_Subscribed == 1)                         //如果是预约，则需要判断时间是否为预约体检时间
                        {
                            if (Date == CustomerSubScribDate)           //判断当前时间是否和预约体检时间相等
                            {
                                //该客户已经进行过登记或预约，有为收费项目则需要先打印收费单，没有收费项目则直接打印增项指引单
                                //判断是否存在非团体为收费的增项项目
                                if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                                    //ShowSystemDialog("正在打印指引单，请稍候...");
                                    IsCanShowSaveMsg = false; 
                                    //FastReport.GenerateCustomerGuide(UserID, UserName, ID_Customer, "guidesheet.frx", 0, 1);
                                    SendWaitToInterfaceByClient_Ajax(Gender, ID_Customer); //xmhuang 2013-10-12 打印完指引单后向接口发送信息  
                                }
                            }
                            else {
                                IsCanShowSaveMsg = false; ShowSystemDialog("客户预约体检时间为:[" + CustomerSubScribDate + "]目前不能打印指引单！");
                            }
                        }
                        else if (Is_Subscribed == 0)//如果是登记，则判断登记时间和当前时间是否相同，相同则可用直接打印指引单
                        {
                            if (Date == CustomerSubScribDate) {
                                //该客户已经进行过登记或预约，有为收费项目则需要先打印收费单，没有收费项目则直接打印增项指引单
                                //判断是否存在非团体为收费的增项项目
                                if (jQuery("#tblList tbody tr[is_printed!='1']").length > 0) {
                                    IsCanShowSaveMsg = false;
                                    //FastReport.GenerateCustomerGuide(UserID, UserName, ID_Customer, "guidesheet.frx", 0, 1);
                                    SendWaitToInterfaceByClient_Ajax(Gender, ID_Customer); //xmhuang 2013-10-12 打印完指引单后向接口发送信息  
                                }
                            }
                            else {
                                IsCanShowSaveMsg = false; ShowSystemDialog("客户登记体检时间为:[" + CustomerSubScribDate + "]目前不能打印指引单！");
                            }
                        }
                    }
                }
                else {
                    //ShowSystemDialog("对不起，该用户已打印指引单，当前不能打印指引单...");
                }
            }
        }
    }
    if (IsCanShowSaveMsg) {
        ShowSystemDialog(jQuery("[name='btnAdd']").data("ShowMsg"));
    }
    //xmhuang 2013-09-25 由于调整登记操作，这里完成登记后继续进行下一个申请登记操作
    if (type.toLocaleLowerCase() == "add") {
        AddNewRegistOrSign(0);
    }
    else if (type.toLocaleLowerCase() == "edit") {
        ReBindCustomerBusFee();
    }
}

/****************缴费通知单、记账通知单 End********************/

function ChangeReadAndWrite(obj) {
    var checked = obj.checked;
    if (!checked) {
        jQuery("#txtSFZ").removeAttr("readonly");
        jQuery("#txtCustomer").removeAttr("readonly");
        jQuery("#txtTJH").removeAttr("readonly");
        jQuery("#txtYKT").removeAttr("readonly");
    }
    else {
        jQuery("#txtSFZ").attr("readonly", "readonly");
        jQuery("#txtCustomer").attr("readonly", "readonly");
        jQuery("#txtTJH").attr("readonly", "readonly");
        jQuery("#txtYKT").attr("readonly", "readonly");
    }
}
//设置身份证图片保存格式为jepg
try {
    SynCardOcx1.SetPhotoType(1);
} catch (e) { }

/*********************拍照 Begin**************************/
var photoContent = "";
var photoArt = "";
function StartCamera() {

    //    var content = '<div><object id="TakePhoto" classid="clsid:ea33a66e-f937-4d0d-aa91-8f6c917d0748" width="200"' +
    //            'height="220" codebase="http://192.172.0.120/ActiveX/FYHTakePhotoSetup.msi">' +
    //        '</object></div>';
    //    if (photoContent == "") {
    //        photoContent = jQuery("#divPhoto")[0].outerHTML;
    //        if (document.getElementById("TakePhoto") != undefined || document.getElementById("TakePhoto") != null) {
    //            document.getElementById("TakePhoto").CloseCam();
    //        }
    //        jQuery("#divPhoto").remove();
    //    }
    if (photoContent == "") {
        photoContent = '<div><object id="TakePhoto" classid="clsid:ea33a66e-f937-4d0d-aa91-8f6c917d0748" width="200"' +
                    'height="220" codebase="http://192.172.0.120/ActiveX/FYHTakePhotoSetup.msi">' +
                '</object></div>';
    }

    var left = jQuery("#HeadImg").offset().left + jQuery("#HeadImg").width();
    var top = jQuery("#HeadImg").top; //  + document.documentElement.scrollTop;
    //var form = ShowDialogX("/takephoto/index.html", "我是弹出窗口", 650, 350, '我是弹出窗口');
    //photoArt = ""; //设置此值为空，以保证每次能够重新执行init中的代码保证正常启动视频 xmhuang 2013-10-27s
    if (photoArt == "") {
        photoArt = art.dialog({
            lock: true, opacity: 0.3,
            //follow: document.getElementById("takePhoteFrame"),
            title: '视频拍照',
            content: photoContent,
            opacity: 0.3,
            init: function () {
                jQuery("#TakePhoto").hide();
                TakePhoto = document.getElementById("TakePhoto");
                TakePhoto.StartCamera();
                jQuery("#TakePhoto").show();
            },
            ok: function () {
                return DoTakePhoto();

            },

            okVal: "保存",
            cancelVal: '关闭',
            cancel: function () {
                //每次使用视频资源后就释放掉 xmhuang 2013-10-27
                //                if (document.getElementById("TakePhoto") != undefined || document.getElementById("TakePhoto") != null) {
                //                    document.getElementById("TakePhoto").CloseCam();
                //                }
                //每次使用视频资源后就释放掉 xmhuang 2013-10-27
                this.hide(); return false;
            },
            close: function () {
                //每次使用视频资源后就释放掉 xmhuang 2013-10-27
                //                if (document.getElementById("TakePhoto") != undefined || document.getElementById("TakePhoto") != null) {
                //                    document.getElementById("TakePhoto").CloseCam();
                //                }
                //每次使用视频资源后就释放掉 xmhuang 2013-10-27
                this.hide(); return false;
            }
        });
    }
    else {
        photoArt.show();
    }
}

function CloseCam() {
    TakePhoto.Exit();
}

function DoTakePhoto() {

    var Base64Code = TakePhoto.TakePhoto();
    if (document.getElementById("HeadImg") == undefined) {
        return false;
    }
    document.getElementById("HeadImg").src = "data:image/gif;base64," + Base64Code + "";
    //保存到数据库
    //判断是否存在身份在信息
    var objID = "", Key = "";
    var txtSFZ = jQuery.trim(jQuery("#txtSFZ").val());
    var txtTJH = jQuery.trim(jQuery("#txtTJH").val());
    var txtYKT = jQuery.trim(jQuery("#txtYKT").val());
    if (txtTJH != "") {
        objID = "txtTJH";
        Key = "ID_Customer";
    }
    else if (txtSFZ != "") {
        objID = "txtSFZ";
        Key = "IDCard";
    }
    else {
        objID = "txtYKT";
        Key = "ExamCard";
    }
    if (objID != "" && Key != "") {
        PostImg(Base64Code, objID, Key, 0);
    }

    return true;
    //that.close();
}
/*********************拍照 End**************************/



function JudgeIsHideQuickBox() {
    if (document.activeElement.id != "idSelectExamType") {
        ShowHideQuickQuerySetTable(false, "");      // 套餐
        ShowHideQuickQueryExamTypeTable(false, ""); // 体检类型
        ShowHideQuickQueryNationTable(false, "");   // 民族
        ShowHideQuickQueryUserTable(false, "");     // 导检护士
    }
}


// 隐藏所有的弹出窗口(通过注册焦点事件来隐藏弹出框)
function HideAllQuickTableEvent() {

    //input 获取焦点事件
    jQuery(".content input").focus(function () {
        var tempClass = jQuery("#" + document.activeElement.id).attr("class");

        if (tempClass != undefined) {
            if (tempClass != "QuickQueryShowBox" && tempClass.indexOf("btnQuickSure") < 0) {
                HideAllQuickTable();
            }
        } else {
            HideAllQuickTable();
        }
    });

    //select 获取焦点事件
    jQuery(".content select").focus(function () {
        var tempClass = jQuery("#" + document.activeElement.id).attr("class");
        // ShowSystemDialog(tempClass);
        if (tempClass != "QuickQueryShowBox") {
            HideAllQuickTable();
        }
    });

}

function HideAllQuickTable() {
    ShowHideQuickQuerySetTable(false, "");      // 套餐
    ShowHideQuickQueryExamTypeTable(false, ""); // 体检类型
    ShowHideQuickQueryNationTable(false, "");   // 民族
    ShowHideQuickQueryUserTable(false, "");     // 导检护士
}

/***************************扫描区域功能  Begin************************************/
/// <summary>
///重置收费项目汇总信息
/// </summary>
function ResetSumZJInfo() {

    var curHtml = "<label name='kytjxm'>客户预约项目</label>(<label style='color:red;font-size:13px; text-decoration:underline;'>共：0 个；原价合计：0 元；折扣合计：0 元；实收合计：0 元</label>)";
    jQuery("#divSumHeader").html(curHtml);
    jQuery("#lblSumHeaderX").html("实收合计：0 元");

    /* 修改人：黄兴茂 修改日期：2013-08-13 修改内容：设置客户收费项目显示内容 Begin*/
    if (jQuery('#modelName').val().toLowerCase() == "regist") {
        jQuery("#divSumHeader label[name='kytjxm'").text("客户预约项目");
    }
    else if (jQuery('#modelName').val().toLowerCase() == "sign") {
        jQuery("#divSumHeader label[name='kytjxm'").text("客户登记项目");
    }
    /* 修改人：黄兴茂 修改日期：2013-08-13 修改内容：设置客户收费项目显示内容 End*/
}
/// <summary>
///重置所有客户信息
/// </summary>
function ResetAllCustomerInfo() {
    /***************重置基础信息   Begin***************************/


    jQuery("#txtSFZ").val(""); //置空身份证
    jQuery("#lblSFZ").text(""); //置空身份证
    jQuery("#txtCustomer").val(""); //置空姓名


    if (type.toLowerCase() != "add")//如果是新增状态扫描身份证，不能置空体检号
    {
        jQuery("#txtTJH").val(""); //置空体检号
    }
    jQuery("#txtYKT").val(""); //置空一卡通
    jQuery("#lblHidCustomer").text(""); //重置编辑状态下的体检号
    jQuery("#lblHidBirthDay").text(""); //重置编辑状态下出生日期
    jQuery("#lblHidSex").text(""); //重置编辑状态性别
    //    RemoveSelectedUser(); // 重置导检护士（如果是新增，则清空导检护士相应的数据）

    RemoveSelectedSet(); // 重置套餐（如果是新增，则清空套餐相应的数据）
    //ShowQuickSelectNation(1, "汉族"); // 新增时，默认为汉族 设置民族的已选项
    jQuery("#slCultrul").find("option:selected").attr("selected", false);
    jQuery("#slCultrul").find("option[value='-1']").attr("selected", true);
    jQuery("#s2id_slCultrul .select2-choice span").text(choiceCultrulText);
    /***************重置基础信息   End***************************/

    /***************重置收费项目信息   Begin***************************/
    jQuery("#tblList tbody tr[id!='loading']").remove(); //重置收费项目
    ShowQuickSelectExamType(1, "健康体检");    // 设置体检类型的已选项
    ResetSumZJInfo();

    jQuery("#txtMobil").val(""); //重置联系方式
    jQuery("[name='btnComplete']").show(); //已完成总检的数据，不允许保存，这里需要重置

    jQuery("#lblHidCustomer").text(""); //置空体检号的值 xmhuang 2013-09-27
    jQuery("#txtTJH").val(""); //置空体检号的值 xmhuang 2013-09-27
    jQuery("#didTeam").html(""); //置空团体信息值 xmhuang 2013-09-27

    RemoveSelectedUser();    // 设置导检护士的已选人员
    RemoveSelectedNation();          // 设置民族的已选项
    RemoveSelectedSet();    // 设置体检类型的已选项
    RemoveSelectedExamType(); //设置体检类型
    ShowQuickSelectExamType(1, "健康体检");    // 设置体检类型的已选项 xmhuang 2013-10-08
    jQuery("#txtSetInputCode").val(""); //重置输入的套餐名称 xmhuang 2013-10-26
    jQuery("#txtUserInputCode").val(""); //重置输入的导检护士名称 xmhuang 2013-10-26  
    jQuery("#txtExamTypeInputCode").val(""); //重置输入的体检类型名称 xmhuang 2013-10-26  
    jQuery("#txtNationInputCode").val(""); //重置输入的所属民族名称 xmhuang 2013-10-26  

    jQuery("#slMarried [value='-1']").attr("selected", true); //xmhuang 2013-10-30 设置婚姻为请选择
    jQuery("#slSex [value='-1']").attr("selected", true); //xmhuang 2013-10-30 设置性别为请选择
    jQuery("#slFeeWay [value='1']").attr("selected", true); //xmhuang 2013-10-30 设置付费方式为现金
    jQuery("#slOperateLevel [value='1']").attr("selected", true); //xmhuang 2013-10-30 设置用户密级为一级
    /***************重置收费项目信息   End***************************/
}
/// <summary>
///设置客户预约还是登记显示信息
/// </summary>
function SetCustomerSubscribedInfo(Is_Subscribed) {
    if (Is_Subscribed == 1)//预约客户
    {
        jQuery("#lblRegiste").text("预约时间");
        jQuery("#ckbRegiste").attr("disabled", "disabled");
        jQuery("[name='btnComplete']").val(" 完 成(F9) ");   // 完成预约
        jQuery("[name='btnAdd']").val(" 申 请 ");        // 申请新客户预约
        jQuery("[name='btnComplete']").attr("title", " 完成预约 ");     // 完成预约
        jQuery("[name='btnAdd']").attr("title", " 申请新客户预约 ");    // 申请新客户预约
    }
    else if (Is_Subscribed == 0)//登记客户
    {
        jQuery("#lblRegiste").text("登记时间");
        jQuery("#ckbRegiste").attr("checked", false);
        jQuery("#ckbRegiste").attr("disabled", "disabled");
        jQuery("[name='btnComplete']").val(" 完 成(F9) ");   // 完成登记
        jQuery("[name='btnAdd']").val(" 申 请 ");        // 申请新客户登记
        jQuery("[name='btnComplete']").attr("title", " 完成登记 ");  // 完成登记
        jQuery("[name='btnAdd']").attr("title", " 申请新客户登记 "); // 申请新客户登记

    }
}

function GetCustomerInfo(ID_Customer) {
    var allISDeleteDT = "";
    var flag = false;
    jQuery.ajax({
        type: "GET",
        async: false,
        url: "/Ajax/AjaxRegiste.aspx",
        data: { action: "GetCustomerInfo", ID_Customer: ID_Customer },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg.dataList.length > 0) {
                allISDeleteDT = msg.dataList[0];
            }
        }
    });
    return allISDeleteDT;
}

var IsSearch = 0;
var IsSearchIDCustomer = 0;
function SearchCardAndCustomer() {
    //type = "search"; //扫描数据则默认为编辑状态这样才会触发检索收费项目功能
    var card = jQuery.trim(jQuery("#txtSearchX").val());
    IsFromGenerateCustomerCard = 0;
    jQuery("#btnReadFromMachine").data("ReadCustomerFromCard", 0); //重置来源于设备读取标记
    if (isCustomerExamNo(card))//如果是体检号
    {
        var ErrorMsg = "";
        var TypeCode = card.substring(1, 2); //TypeCode:3 个人预约 ,6个人登记 ,9团体登记
        if (TypeCode == 3)//个人预约
        {
            if (IsTeam == 1)//如果当前模块是团体登记模块
            {
                ErrorMsg = "对不起，体检号[" + card + "]为个人预约客户，请到个人登记处";
            }
            else {

            }
        }
        else if (TypeCode == 6)//个人登记
        {
            if (IsTeam == 1)//如果当前模块是团体登记模块
            {
                ErrorMsg = "对不起，体检号[" + card + "]为个人登记客户，请到个人登记处";
            }
            else {
                if (jQuery('#modelName').val().toLowerCase() == "regist") {
                    ErrorMsg = "对不起，体检号[" + card + "]为个人登记客户，请到个人登记处";
                }
                else if (jQuery('#modelName').val().toLowerCase() == "sign") {
                }
            }
        }
        else if (TypeCode == 9)//团体登记
        {
            if (IsTeam == 1)//如果当前模块是团体登记模块
            { }
            else {
                ErrorMsg = "对不起，体检号[" + card + "]为团体客户，请到团体登记处";
            }
        }
        else {
            var allISDeleteDT = GetCustomerInfo(card);
            //判断是否是对应模块的客户(是个人还是团体)
            if (allISDeleteDT == "" || allISDeleteDT.length == 0) {
                ShowSystemDialog("对不起,系统未找到体检号[" + card + "]对应的信息!");
                return false;
            }
            else {
                var ErrorMsg = "";
                //如果是团体客户
                if (allISDeleteDT.Is_Team == "True") {
                    if (IsTeam == 1)//如果当前模块是团体登记模块
                    { }
                    else {
                        ErrorMsg = "对不起，体检号[" + card + "]为团体客户，请到团体登记处";
                    }
                }
                else {
                    if (IsTeam == 1)//如果当前模块是团体登记模块
                    {
                        ErrorMsg = "对不起，体检号[" + card + "]为个人客户，请到个人登记处";
                    }
                    else {

                    }
                }
                if (ErrorMsg != "") {
                    jQuery("#txtSearchX").val(""); //清空身份证号码 xmhuang 2013-09-30
                    jQuery("#txtSearchX").focus();
                    jQuery("#txtSearchX").select();
                    ShowSystemDialog(ErrorMsg);
                    ErrorMsg = "";
                    return false;
                }
                else {
                    IsSearch = 1;
                    IsSearchIDCustomer = 1;
                    ResetAllCustomerInfo();
                    jQuery("#idSelectSet").data("ID_Customer", card); //绑定体检号
                    jQuery("#tblList tbody").empty(); //重置收费项目
                    SearchCustomerInfo("txtSearchX", "ID_Customer");
                }
            }
        }
        if (ErrorMsg != "") {
            jQuery("#txtSearchX").val(""); //清空身份证号码 xmhuang 2013-09-30
            jQuery("#txtSearchX").focus();
            jQuery("#txtSearchX").select();
            ShowSystemDialog(ErrorMsg);
            ErrorMsg = "";
            return false;
        }
        else {
            IsSearch = 1;
            IsSearchIDCustomer = 1;
            ResetAllCustomerInfo();
            jQuery("#idSelectSet").data("ID_Customer", card); //绑定体检号
            jQuery("#tblList tbody").empty(); //重置收费项目
            SearchCustomerInfo("txtSearchX", "ID_Customer");
        }

    }
    else if (isIDCardNo(card)) //如果身份证
    {
        //个人禁用身份证检索功能 xmhuang 2013-09-30 WWang讨论决定个人禁用身份证检索功能，值提供体检号检索功能，如果需要使用身份证检索功能只能在列表中进行检索
        if (IsTeam == 1) {
            IsSearch = 1;
            IsSearchIDCustomer = 1; //xmhuang 2013-09-27 身份证和体检号都要检索最近一次体检信息
            ResetAllCustomerInfo();
            SearchCustomerInfo("txtSearchX", "IDCard");
        }
        else {
            //个人登记、预约禁用身份证检索功能
        }
    }
    else {
        if (card == "") {
            //个人禁用身份证检索功能 xmhuang 2013-09-30 WWang讨论决定个人禁用身份证检索功能，值提供体检号检索功能，如果需要使用身份证检索功能只能在列表中进行检索
            if (IsTeam == 1) {
                //ShowSystemDialog("请您输入需要查询的体检号或证件信息！");
                IsSearch = 1;
                IsSearchIDCustomer = 1; //xmhuang 2013-09-27 身份证和体检号都要检索最近一次体检信息
                ResetAllCustomerInfo();
                SearchCustomerInfo("txtSearchX", "IDCard");
            }
        }
    }
    jQuery("#txtSearchX").val(""); //清空身份证号码 xmhuang 2013-09-30
    jQuery("#txtSearchX").focus();
    jQuery("#txtSearchX").select();
}
//修改人：黄兴茂
//修改时间：2013-08-01
//修改内容：屏蔽当前操作为add时才能检索客户登记信息
function SearchCustomerInfo(objID, Key) {

    jQuery("#lblErrorMessage").text("");
    //判断是身份证检索还是体检号还是一卡通检索
    var KeyValue = jQuery.trim(jQuery("#" + objID).val());
    //    if (KeyValue == "") {
    //        jQuery("#lblErrorMessage").text("请您输入正确的证件号码或体检号！");
    //        return false;
    //    }
    var IsDefaultSearch = document.getElementById("chcDefaultSearch").checked; //读取是否默认从客户证件卡片读取数据,该控件已隐藏
    if (Key == "ID_Customer")//体检号检索
    {
        RequestCustomerInfo(objID, Key, 1);
    }
    else if (Key == "IDCard")//身份证检索
    {
        if (isIDCardNo(KeyValue))//如果身份证号满足要求，则直接通过身份证检索信息，其信息为客户最近一次登记（预约）信息，并且包含体检项目信息，此功能一般只允许在编辑状态下使用
        {
            if (type.toLowerCase() == "edit")//如果是修改状态则直接通过身份证号获取客户体检信息
            {
                RequestCustomerInfo(objID, Key, 1);
            }
            else//如果是新增状态,则直接从客户卡片上读取身份证信息
            {
                //FindReader(objID, Key); //从客户卡片上读取身份证，如果读取不到信息则从数据库中读取客户信息
                if (IsTeam == 1)//如果是团体成员则直接通过身份证检索其最近体检信息
                {
                    RequestCustomerInfo(objID, Key, 1); //由于当前身份证号存在，则不用从客户卡片上读取信息，直接进行检索，检索客户体检项目信息
                }
                else {
                    RequestCustomerInfo(objID, Key, 1); // xmhuang 2013-09-27 调整身份证检索默认都检索最后一次体检信息 由于当前身份证号存在，则不用从客户卡片上读取信息，直接进行检索，但不检索客户体检项目信息
                }
            }
        }
        else {
            if (KeyValue == "") {
                FindReader(objID, Key, 1); //从客户卡片上读取身份证，如果读取不到信息则从数据库中读取客户信息
            }
        }
    }
    else if (Key == "ExamCard") //一卡通检索
    {
        if (KeyValue.length == 19) {
            RequestCustomerInfo(objID, Key, 1);
        }
        else {
            jQuery("#lblErrorMessage").text("请您输入正确的一卡通号！");
            jQuery("#" + objID).focus();
            jQuery("#" + objID).select();
        }
    }
}
var IsFromGenerateCustomerCard = 0;
/// <summary>
/// 生成自定义证件号码
/// </summary>
function OpenUserNoIDCard() {
    ResetAllCustomerInfo();
    var tipscontent = '<table class="ModifyPassword">' +
            '<tbody>' +
            '    <tr><td class="left">客户姓名：</td><td><input name="txtCustomerNameX" id="txtCustomerNameX" /> &nbsp;</td></tr>' +
            '    <tr><td class="left">出生日期：</td><td><input name="txtCustomerBirthdayX" id="txtCustomerBirthdayX" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd\'})"/> &nbsp;</td></tr>' +
            '    <tr><td class="left">客户性别：</td><td><select id="slSexx"><option value="1">男</option><option value="2">女</option></select></td></tr>' +
            '</tbody>' +
            '</table>';
    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '生成自定义证件号码',
        init: function () {
            document.getElementById("txtCustomerNameX").focus();
        },
        button: [{
            name: '确定',
            callback: function () {
                IsFromGenerateCustomerCard = 1;
                var ISGenerateCustomerCard = GenerateCustomerCard(); //生成自定义证件号
                return ISGenerateCustomerCard;
            }, focus: true
        }, {
            name: '取消'
        }]
    });
}
/// <summary>
///生成自定义客户证件号
/// </summary>
function GenerateCustomerCard() {

    var CustomerName = jQuery("#txtCustomerNameX").val();
    var Birthday = jQuery("#txtCustomerBirthdayX").val();
    var Gender = document.getElementById("slSexx").options[document.getElementById("slSexx").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSexx").options[document.getElementById("slSexx").selectedIndex].text; //性别
    if (CustomerName == "") {
        ShowSystemDialog("请您填写客户姓名！");
        jQuery("#txtCustomerNameX").focus();
        return false;
    }
    else if (Birthday == "") {
        ShowSystemDialog("请您填写出生日期！");
        //jQuery("#txtCustomerBirthdayX").select();
        return false;
    }
    //格式：111111(6位)+出生年月(8位)+"11"+性别+"1"
    var twoRandStr = "11";
    var oneRandStr = "1";
    //生成性别补位
    var Sex = 0;
    if (GenderName == "男") { Sex = 1; }
    else { Sex = 2; }
    var cardStr = "111111" + "" + Birthday.replace(/-/gi, "") + "" + twoRandStr + "" + Sex + "" + oneRandStr;
    var data = { CustomerName: CustomerName, IDCard: cardStr, GenderName: GenderName, Sex: Sex, Birthday: Birthday };
    jQuery("#txtSearchX").data("data", data);
    jQuery("#txtSearchX").val(cardStr); jQuery("#lblSFZ").text(cardStr); jQuery("#txtSFZ").val(cardStr); //设置身份信息
    jQuery("#txtCustomer").val(CustomerName); //设置客户姓名
    //    jQuery("#slSex [forgender='" + Sex + "']").attr("selected", true); //设置当前性别
    RequestCustomerInfo("txtSearchX", "IDCard", 0); //从后台检索是否存在同身份证的客户,0：不加载客户体检号信息和客户收费项目信息 1：相反
    jQuery("#txtSearchX").val(""); //清空身份证号码 xmhuang 2013-09-30
    jQuery("#txtSearchX").focus();
    jQuery("#txtSearchX").select();
}
/// <summary>
///设置自定义客户证件信息
/// </summary>
function SetGenerateCustomerCard() {
    if (jQuery("#txtSearchX").data("data") != undefined) {
        var data = jQuery("#txtSearchX").data("data");
        var CustomerName = data.CustomerName;
        var IDCard = data.IDCard;
        var Sex = data.Sex;
        var GenderName = data.GenderName;
        var Birthday = data.Birthday;
        jQuery("#txtCustomer").val(CustomerName);
        jQuery("#txtSearchX").val(IDCard); jQuery("#lblSFZ").text(IDCard); jQuery("#txtSFZ").val(IDCard); //设置身份信息
        jQuery("#slSex [forgender='" + Sex + "']").attr("selected", true); jQuery("#lblSex").text(GenderName); //设置当前性别
        jQuery("#txtBirthDay").val(Birthday); jQuery("#lblHidBirthDay").text(Birthday); //设置出生日期
        jQuery("#txtSearchX").removeData("data"); //移除数据项
    }
}
/// <summary>
///扫描一卡通
/// </summary>
function InputExamCard() {
    var tipscontent = '<table class="ModifyPassword">' +
            '<tbody>' +
            '    <tr><td class="left">一卡通号：</td><td><input name="txtExamCard" id="txtExamCard" onkeyup="OnFormKeyUp();"/> &nbsp;</td></tr>' +
            '</tbody>' +
            '</table>';
    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '扫描一卡通',
        init: function () {
            document.getElementById("txtExamCard").focus();
            document.getElementById("txtExamCard").select();
        },
        button: [{
            id: "btnExamOK",
            name: '确定',
            callback: function () {
                var ISSend = SendExamInfoToHis(); //生成自定义证件号
                return ISSend;
            }, focus: true
        }, {
            name: '取消'
        }]
    });
}
function SetDisscount() {
    var curZK = jQuery.trim(jQuery("#txtXMZK").val());
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
        jQuery("[name='ItemCheckbox']").each(function () {
            //                if (jQuery(this).parent().parent().attr('CustFeeChargeState') != "0") {
            if (jQuery(this).attr("checked")) {
                jQuery(this).parent().parent().find("[name = 'zk']").text(curZK); //设置折扣后价格
                jQuery(this).parent().parent().find("[name = 'zkr']").text(DisUserName); //设置折扣人
                jQuery(this).parent().parent().attr("id_discounter", DisUserID); //为tr保存折扣人ID
                jQuery(this).parent().parent().attr("discountername", DisUserName); //为tr保存折扣人名称
                //                    jQuery(this).parent().parent().find("[name = 'zkr']").text(UserName); //设置折扣人
                //                    jQuery(this).parent().parent().attr("id_discounter", UserID); //为tr保存折扣人ID
                //                    jQuery(this).parent().parent().attr("discountername", UserName); //为tr保存折扣人名称

            }
            //}
        });

    }
}
function OpenChangeUser() {
    var tipscontent = '<table class="ModifyPassword">' +
            '<tbody>' +
            '    <tr><td class="left">用户名：</td><td><input onkeyup="OnFormKeyUp();" style="width:120px;" maxlength="10" name="txtUserLoginName" id="txtUserLoginName"/> &nbsp;</td></tr>' +
            '    <tr><td class="left">密码：</td><td><input type="password" style="width:120px;" onkeyup="OnFormKeyUp();" maxlength="20" name="txtUserPassword" id="txtUserPassword"/> &nbsp;</td></tr>' +
            '<tr><td colspan="2" align="center"><span id="divUserLoginTips" style="color:red;">&nbsp;</span></td></tr>' +
            '</tbody>' +
            '</table>';
    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '获取用户折扣',
        init: function () {
            document.getElementById("txtUserLoginName").value = LastUserLoginName;
            document.getElementById("txtUserPassword").value = LastUserPassword;
            document.getElementById("txtUserLoginName").focus();
            document.getElementById("txtUserLoginName").select();
        },
        button: [{
            name: '确定',
            callback: function () {
                return ChangeUserDiscount();
            }, focus: true
        }, {
            name: '取消'
        }]
    });
}
var LastUserLoginName = "";
var LastUserPassword = "";
/// <summary>
///选择折扣人，设置折扣
/// </summary>
function ChangeUserDiscount() {


    //ChangeUser
    jQuery("#divUserLoginTips").html("");
    var flag = false;
    var UserLoginName = jQuery.trim(jQuery("#txtUserLoginName").val()); //用户名
    var UserPassword = jQuery.trim(jQuery("#txtUserPassword").val()); //密码
    if (UserLoginName == "") {
        jQuery("#divUserLoginTips").html("请输入用户名!");
        jQuery("#txtUserLoginName").focus();
        return false;
    }
    else if (UserPassword == "") {
        jQuery("#divUserLoginTips").html("请输入密码！");
        jQuery("#txtUserPassword").focus();
        return false;
    }
    jQuery("#divUserLoginTips").html("");
    var VerifyCode = ""; //验证码，暂时没有使用 $("#VerifyCode").val();
    jQuery.ajax({
        type: "GET",
        url: "/ajax/AjaxUser.aspx",
        data: { action: "ChangeUser", UserLoginName: UserLoginName, UserPassword: UserPassword, VerifyCode: VerifyCode, param: Math.random() },
        cache: false,
        async: false,
        dataType: "json",
        success: function (msg) {
            if (msg.success == 0) {
                jQuery("#UserLoginName").focus();
                jQuery("#UserLoginName").select();
                jQuery("#UserPassword").val("");
                jQuery("#divUserLoginTips").html(msg.Message);
                jQuery("#txtUserLoginName").focus();
                jQuery("#txtUserLoginName").select();
                flag = false;
            }
            else if (msg.success == 1) {
                LastUserLoginName = UserLoginName;
                LastUserPassword = UserPassword;
                if (msg.DisCountRate == 0) {
                    msg.DisCountRate = 10;
                }
                DisCountRate = msg.DisCountRate;
                DisUserID = msg.UserID;
                DisUserName = msg.UserName;
                CloseDialogWindow();
                SetDisscount();
                flag = true;
            }
        }
    });
    return flag;
}
/// <summary>
///发送消息到HIS
/// </summary>
function SendExamInfoToHis() {
    var ExamCard = jQuery.trim(jQuery("#txtExamCard").val());
    if (ExamCard == "") {
        ShowSystemDialog("请您扫描一卡通！");
        jQuery("#txtExamCard").focus();
        jQuery("#txtExamCard").select();
        return false;
    }
    //判断一卡通是否满足此格式：;6221355009448213650=000010173815389=？
    //;6221355009448213650=000010173815389=?
    var arr = ExamCard.split('=');
    if (arr[0].indexOf(";") == -1) {
        ShowSystemDialog("一卡通格式不正确，请您重新输入！");
        jQuery("#txtExamCard").focus();
        jQuery("#txtExamCard").select();
        return false;
    }
    var cardInfo = arr[0].split(';');
    jQuery("#txtYKT").val(cardInfo[1]); jQuery("#lblYKT").text(cardInfo[1]); //设置一卡通
    return SendExamInfoToHis_Ajax(ExamCard);
}
/// <summary>
///调用Webservice发送消息到HIS
/// </summary>
function SendExamInfoToHis_Ajax(ExamCard) {
    CloseDialogWindow();
    return true;
}

/// <summary>
///新增客户信息--原从设备读取 xmhuang 2013-09-28
/// </summary>
function ReadCustomerFromCard() {
    jQuery("#btnReadFromMachine").data("ReadCustomerFromCard", 2);
    ResetAllCustomerInfo(); //重置
    FindReader('txtSearchX', 'IDCard', 2); //从设备读取并更新客户头像信息 0:不检索客户婚姻状况和联系电话信息 1：检索全部信息，包含体检信息 2：检索客户婚姻状况和联系电话信息
    jQuery("#txtSearchX").val(""); //清空身份证号码 xmhuang 2013-09-30
    jQuery("#txtSearchX").focus();
    jQuery("#txtSearchX").select();
    //RequestCustomerInfo('txtSearchX', 'IDCard', 0); //获取客户基本信息，并设置更新头像
    //    var CustomerName = jQuery.trim(jQuery("#txtCustomer").val());
    //    var modelName = jQuery("#modelName").val();
    //    var KeyValue = jQuery.trim(jQuery("#" + objID).val());
    //    jQuery("#lblHidCustomer").text(""); //重置体检号信息
    //    jQuery("#txtTJH").val(""); //重置体检号信息
    //    jQuery("#lblRegiste").text(CurDate); //重置登记、预约时间
    //    jQuery("#txtSubScribDate").val(CurDate); //重置登记、预约时间
}

/***************************扫描区域功能 xmhuang 2013-09-25  End************************************/

/***************************操作类型监控 xmhuang 2013-09-25  Begin************************************/
//function OperType() {
//    var oldValue = "";
//    var name = "type";
//    var id = "";
//    this.PEPackageName = function (n) {
//        oldValue = name; //保存上一次的name
//        name = n;
//        this.propertyChange('name', oldValue, n);
//    }
//    this.SetID = function (n) {
//        oldValue = id; //保存上一次的ID
//        id = n;
//        this.propertyChange('id', oldValue, n);
//    }
//    this.Display = function () {
//        //alert(name + "'s id is :" + id);
//    }
//}
//OperType.prototype = {
//    propertyChange: function (propertyName, oldValue, newValue) {
//        if (newValue.toLowerCase() == "add") {
//            jQuery("#divShowOperType").html("您处于<label style='color:blue;'>[申请]</label>状态，可扫描<label style='color:green;'>证件号</label>，不允许检索<label style='color:red;'>体检号</label>");
//        }
//        else if (newValue.toLowerCase() == "edit") {
//            jQuery("#divShowOperType").html("您处于<label style='color:blue;'>[编辑]</label>状态，可扫描<label style='color:green;'>证件号</label>和<label style='color:green;'>体检号</label>来检索客户收费项目");
//        }
//        //alert(propertyName + " 's value changed from " + oldValue + " to " + newValue);
//    }
//};

//var operType = new OperType(); //实例化监控类
//if (operType != undefined) {
//    operType.SetID(type);
//}
/***************************操作类型监控  End************************************/


/// <summary>
/// 修改团体登记时间 xmhuang 2013-10-12
/// </summary>
function UpdateCustomerSubscribDateOfTeam_Ajax(ID_Customer, SubScribDate) {
    //ajax请求：由于字符在get请求中超长，这里必须使用post方式提交请求
    jQuery.ajax({
        type: "POST",
        cache: false,
        url: "/Ajax/AjaxRegiste.aspx",
        data: { ID_Customer: ID_Customer, SubScribDate: SubScribDate, action: 'UpdateCustomerSubscribDateOfTeam' },
        dataType: "json",
        success: function (msg) {

        },
        complete: function () {

        }
    });
}
/// <summary>
/// 折扣变更后，超过10的重置为10
/// 修改人：黄兴茂
/// 修改日期：2013-10-20
/// 修改内容：通过指定体检号获取其对应收费项目
function ChangeDiscount() {
    var Discount = document.getElementById("txtXMZK").value
    if (Discount > 10) {
        Discount = 10;
        document.getElementById("txtXMZK").value = Discount;
    }
}


/**************************************通过证件读取客户信息 Begin *************************************/
/// <summary>
/// 银安身份证读卡器程序
/// 修改人：黄兴茂
/// 修改日期：2013-10-21
/// 修改内容：银安身份证读卡器程序读取客户身份证信息
function FindReader(objID, Key, IsUpdateCustomerPhoto) {
    //SetCookie('CustomerQueue', "");
    var KeyValue = jQuery.trim(jQuery("#" + objID).val());
    jQuery("#lblErrorMessage").text("正在检索,请稍候...");
    this.errorMsg = "";
    this.flag = false;
    var ret = CVR_IDCard.ReadCard();
    if (ret == 0) {
        //设置用户信息
        jQuery("#txtCustomer").val(jQuery.trim(CVR_IDCard.Name));
        jQuery("#txtSFZ").val(jQuery.trim(CVR_IDCard.CardNo));
        jQuery("#lblSFZ").text(CVR_IDCard.CardNo);
        jQuery("#slSex").val(jQuery.trim(CVR_IDCard.Sex));
        var Born = jQuery.trim(CVR_IDCard.Born);
        jQuery("#txtBirthDay").val(Born.substr(0, 4) + "-" + Born.substr(4, 2) + "-" + Born.substr(6, 2));
        if (CVR_IDCard.Sex == "男") {
            jQuery("#slSex [value='1']").attr("selected", true);
        }
        else {
            jQuery("#slSex [value='2']").attr("selected", true);
        }
        jQuery("#slNation [value='" + parseInt(CVR_IDCard.NationCode) + "']").attr("selected", true);
        jQuery("#s2id_slSex .select2-choice span").text(jQuery("#slSex option:selected").text()); //清除选中项
        jQuery("#s2id_slNation .select2-choice span").text(jQuery("#slNation option:selected").text()); //清除选中项
        ShowQuickSelectNation(parseInt(CVR_IDCard.NationCode), NationArray[CVR_IDCard.NationCode - 1]);          // 设置民族的已选项
        //jQuery("#txtSFZ").focus(); jQuery("#txtSFZ").select();
        jQuery("#HeadImg").attr("src", "data:image/gif;base64," + CVR_IDCard.Picture + "");
        jQuery("#HeadImg").attr("Base64Photo", CVR_IDCard.Picture);
        this.errorMsg += ",成功读取身份信息";
        this.flag = true;
        jQuery("#lblErrorMessage").text(this.errorMsg);
        if (SynCardOcx1.CardNo != "") {
            jQuery("#" + objID).val(CVR_IDCard.CardNo); //为检索框赋值
            PostImg(CVR_IDCard.Picture, objID, Key, IsUpdateCustomerPhoto);
        }
        jQuery("#lblErrorMessage").text("");
    }
    else {
        this.flag = false;
        this.errorMsg += "读取身份信息失败,请您确认身份证已正确安放！";
        jQuery("#lblErrorMessage").text(this.errorMsg);
        if (KeyValue.length == 15 || KeyValue.length == 18) {
            jQuery("#lblErrorMessage").text("正在和服务器取得联系...");
            RequestCustomerInfo(objID, Key, 1);
        }
    }
    //jQuery("#lblErrorMessage").text("");
    return this.flag;
}

/// <summary>
/// 新中新身份证读卡器程序，废弃此方法
/// 修改人：黄兴茂
/// 修改日期：2013-10-21
/// 修改内容：银安身份证读卡器程序读取客户身份证信息
function FindReader_XZX(objID, Key, IsUpdateCustomerPhoto) {
    var KeyValue = jQuery.trim(jQuery("#" + objID).val());
    jQuery("#lblErrorMessage").text("正在检索,请稍候...");
    this.errorMsg = "";
    this.flag = false;
    if (str == '') {
        str = SynCardOcx1.FindReader();
    }
    if (str > 0) {
        if (str > 1000) {
            this.errorMsg = "读卡器连接在USB " + str;
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
        }
        else {
            this.errorMsg = "读卡器连接在COM " + str;
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
        }
        var nRet = SynCardOcx1.ReadCardMsg();
        if (nRet == 0) {

            //设置用户信息
            jQuery("#txtCustomer").val(jQuery.trim(SynCardOcx1.NameA));
            jQuery("#txtSFZ").val(jQuery.trim(SynCardOcx1.CardNo));
            jQuery("#lblSFZ").text(SynCardOcx1.CardNo);
            jQuery("#slSex").val(jQuery.trim(SynCardOcx1.Sex));
            var Born = jQuery.trim(SynCardOcx1.Born);
            jQuery("#txtBirthDay").val(Born.substr(0, 4) + "-" + Born.substr(4, 2) + "-" + Born.substr(6, 2));
            jQuery("#slSex [value='" + SynCardOcx1.Sex + "']").attr("selected", true);
            jQuery("#slNation [value='" + parseInt(SynCardOcx1.Nation) + "']").attr("selected", true);
            jQuery("#s2id_slSex .select2-choice span").text(jQuery("#slSex option:selected").text()); //清除选中项
            jQuery("#s2id_slNation .select2-choice span").text(jQuery("#slNation option:selected").text()); //清除选中项
            ShowQuickSelectNation(parseInt(SynCardOcx1.Nation), NationArray[SynCardOcx1.Nation - 1]);          // 设置民族的已选项
            //jQuery("#txtSFZ").focus(); jQuery("#txtSFZ").select();
            jQuery("#HeadImg").attr("src", "data:image/gif;base64," + SynCardOcx1.Base64Photo + "");
            jQuery("#HeadImg").attr("Base64Photo", SynCardOcx1.Base64Photo);
            this.errorMsg += ",成功读取身份信息";
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
            if (SynCardOcx1.CardNo != "") {
                jQuery("#" + objID).val(SynCardOcx1.CardNo); //为检索框赋值
                //XMHUANG 只要从设备读取客户信息就更新客户头像
                //                if (IsUpdateCustomerPhoto == 1) {

                PostImg(SynCardOcx1.Base64Photo, objID, Key, IsUpdateCustomerPhoto);
                //}
            }
            jQuery("#lblErrorMessage").text("");
        }
        else {
            this.flag = false;
            this.errorMsg += ",读取身份信息失败,请您确认身份证已正确安放！";
            jQuery("#lblErrorMessage").text(this.errorMsg);
            if (KeyValue.length == 15 || KeyValue.length == 18) {
                jQuery("#lblErrorMessage").text("正在和服务器取得联系...");
                RequestCustomerInfo(objID, Key, 1);
            }
        }
    }
    else {
        this.errorMsg = "没有找到读卡器";
        this.flag = false;
        jQuery("#lblErrorMessage").text(this.errorMsg + ",正在和服务器取得联系...");
        if (KeyValue.length == 15 || KeyValue.length == 18) {

            RequestCustomerInfo(objID, Key, 1);
        }
        jQuery("#lblErrorMessage").text("");
    }
    return this.flag;
}
/**************************************通过证件读取客户信息 Begin *************************************/

/// <summary>
/// 修改收费项目打印标记
/// 修改人：黄兴茂
/// 修改日期：2013-10-30
function UpdateCustFeePrintFlag(ID_Customer, AllIDFee) {
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRegiste.aspx",
        data: { ID_Customer: ID_Customer, AllIDFee: AllIDFee, action: 'UpdateCustFeePrintFlag' },
        cache: false,
        contentType: "application/x-www-form-urlencoded;Content-length=1024000",
        dataType: "json",
        success: function (jsonMsg) {
            //alert(jsonMsg.Message);
        }
    });

}