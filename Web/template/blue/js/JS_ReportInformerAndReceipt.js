/****************报告通知和领取公用脚本模块  Begin***********************/
var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改"; //编辑提示
var modelName = jQuery.trim(jQuery("#modelName").val());
var TemplateName = "";
/*******设置页面元素的显示和隐藏 Begin*******/
if (modelName != null) {
    //报告通知
    if (modelName.toLowerCase() == "informer") {
        jQuery("#btnReceipt").hide();
        jQuery("#btnInformer").show();
        jQuery("#btnSetReceipter").hide(); //xmhuang 2013-11-01 隐藏批量设置领取人
        TemplateName = "TemplateReportView";

    }
    //报告领取
    if (modelName.toLowerCase() == "receipt") {
        jQuery("#btnInformer").hide();
        jQuery("#btnReceipt").show();
        jQuery("#btnSetReceipter").show(); //xmhuang 2013-11-01 显示批量设置领取人 暂时屏蔽此功能
        TemplateName = "TemplateReportViewX";
    }
    var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup(TemplateName);
    var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
    jQuery('#tbReportInformer thead').html(templateContent);
}
/*******设置页面元素的显示和隐藏 End*******/
/// <summary>
///文档加载完毕事件
/// </summary>
jQuery(document).ready(function () {
    jQuery("#txtCustomerID").focus();
    QueryPagesData(0);
});

/****************************已审核列表分页绑定 Begin******************************/
function pageselectCallback(page_index, jq) {

    if (tempOperPageCount > 0) {
        tempOperPageCount++;
        QueryPagesData(page_index);
    }
    tempOperPageCount++;

    return false;
}
/// <summary>
/// 获取查询类型 全部：0 个人：1 团体：2
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function GetSearchType() {
    this.SearchType = 0;
    this.ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //婚姻状况
    var SearchType = 0;
    if (document.getElementById("printReportAll").checked) {
        this.SearchType = 0;
    }
    else if (document.getElementById("printReportRadioSelf").checked) {
        this.SearchType = 1;
    }
    else if (document.getElementById("printReportRadioTeam").checked) {
        this.SearchType = 2;
        //获取团体ID
    }

}
function QueryPagesData(pageIndex) {

    var ID_Customer = jQuery.trim(jQuery("#txtCustomerID").val());
    optInit = getOptionsFromForm05();
    //判断是全部、团体还是个人
    var Search = new GetSearchType();
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { ID_Customer: ID_Customer, SearchType: Search.SearchType, ID_Team: Search.ID_Team, modelName: modelName, pageIndex: pageIndex, pageSize: pagePagination05.items_per_page, action: 'GetInformerOrReceiptReport' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {


                jQuery("#Pagination").show();
                if (tempOperPageCount == 0) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                }
                tempOldtotalCount = msg.totalCount;


                var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup(TemplateName);
                var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
                var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
                var index = 1, Is_Team = 0;
                var newContent = '';
                jQuery(msg.dataList).each(function (i, item) {
                    Is_Team = item.TeamName == "" ? 0 : 1;
                    if (templateContent != null) {
                        newContent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@CustomerName/gi, item.CustomerName)
                                    .replace(/@GenderName/gi, item.GenderName)
                                    .replace(/@date/gi, item.date)
                                    .replace(/@TeamName/gi, item.TeamName)
                                    .replace(/@Checker/gi, item.Checker)
                                    .replace(/@CheckedDate/gi, item.CheckedDate)
                                    .replace(/@ReportPrinter/gi, item.ReportPrinter)
                                    .replace(/@ReportPrintedDate/gi, item.ReportPrintedDate)
                                    .replace(/@Is_Checked/gi, item.Is_Checked)
                                    .replace(/@Is_ReportPrinted/gi, item.Is_ReportPrinted)
                                    .replace(/@Is_Informed/gi, item.Is_Informed)
                                    .replace(/@Is_ReportReceipted/gi, item.Is_ReportReceipted)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDate/gi, item.FinalDate)
                                    .replace(/@Department/gi, item.Department)
                                    .replace(/@Informer/gi, item.Informer)
                                    .replace(/@InformedDate/gi, item.InformedDate)
                                    .replace(/@ReportReceiptor/gi, item.ReportReceiptor)
                                    .replace(/@ReportReceiptedDate/gi, item.ReportReceiptedDate)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@checked/gi, true)
                                     .replace(/@Is_Team/gi, Is_Team)
                                     .replace(/@IDCard/gi, item.IDCard)
                                     .replace(/@MobileNo/gi, item.MobileNo)
                                    ;
                        index++;

                    }
                });
                if (newContent != '') {
                    document.getElementById("chbCanInformedReport").checked = true;
                    jQuery('#tbReportInformer tbody').html(newContent);
                }
            } else {
                jQuery('#tbReportInformer tbody').html("");
                jQuery("#Pagination").hide();
            }
        }
    });
}
/****************************已审核列表分页绑定 end******************************/
/// <summary>
/// 表单KeyUp事件
///e：Event参数
/// </summary>
function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13)//回车事件
    {
        //如果是在搜索中
        if (id == "txtCustomerID") {
            AutoGetCustomerReport();
        }
        else if (id == "txtReceipterX")//xmhuang 2013-11-01 批量设置领取人
        {
            DoSetReceipter();
            CloseDialogWindow();
        }
    }
}

/*******页面事件 End*******/

/// <summary>
/// 读取指定模版
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function ReadTemplateTeamTaskGroup(TemplateTeamTaskGroupID) {
    //默认是读取tblTemplateTeamTaskList模版填充到tblTeamTask中显示
    if (TemplateTeamTaskGroupID == "" || TemplateTeamTaskGroupID == undefined) {
        TemplateTeamTaskGroupID = "TemplateReportView";
    }
    var teamTaskGroupListContent = ""; //团体任务分组table内容
    var teamTaskGroupListTheadTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' thead').html(); //团体任务模版Thead部分
    var teamTaskListBodyTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' tbody').html(); //团体任务模版body部分
    this.teamTaskGroupListTheadTempleteContent = teamTaskGroupListTheadTempleteContent;
    this.teamTaskListBodyTempleteContent = teamTaskListBodyTempleteContent;
    return this;
}

/// <summary>
///移除勾选的元素
///obj:元素ID，这里主要是Table元素的ID值
///key:关键字标识，这里主要是Table元素的ID值
/// </summary>
function SelectPrintReport(obj, key) {
    //点击全部
    if (key == "all") {
        jQuery("#s2id_slTeam").hide();
        jQuery("#slTeam").hide();

    }
    //点击个人
    else if (key == "self") {
        jQuery("#s2id_slTeam").hide();
        jQuery("#slTeam").hide();
    }
    //点击团体
    else if (key == "team") {
        jQuery("#s2id_slTeam").show(); //显示团体下拉框 Is_CanSelect
        jQuery("#slTeam").select2();
    }
    jQuery("#lblErrorMessage").empty();
    tempOperPageCount = 0;

    QueryPagesData(0);
}
function SelectTeamPrintReport(obj) {
    var value = obj.value;
    jQuery("#tbReportInformer tbody tr td label[name='lblTeamName']").each(function () {
        if (jQuery.trim(jQuery(this).text()) == value) {
            jQuery(this).parent().parent().show();
        }
        else {
            jQuery(this).parent().parent().hide();
        }
    });
    tempOperPageCount = 0;

    QueryPagesData(0);
}
/// <summary>
///移除勾选的元素
///obj:元素ID，这里主要是Table元素的ID值
/// </summary>
function ResetRownum() {
    var index = 1;
    jQuery("#tbReportInformer tbody tr td label[name='lblRowNum']").each(function () {
        jQuery(this).text(index);
        index++;

    });
}
/// <summary>
///扫描体检号
///ID_Customer:ID_Customer
///</summary>
function AutoGetCustomerReport() {

    var ID_Customer = jQuery.trim(jQuery("#txtCustomerID").val());
    var IsCustomer = isCustomerExamNo(ID_Customer);
    if (!IsCustomer && (ID_Customer.length != 15 && ID_Customer.length != 18)) {
        ShowSystemDialog("对不起，请您输入正确的体检号/证件号！");
        return false;
    }
    var newContent = "";
    var findObj = "";
    //判断体检号是否相同
    //    jQuery("#tbReportInformer tbody tr td label[name='lblID_Customer']").each(function () {
    //        
    //        if (jQuery.trim(jQuery(this).text()) == ID_Customer || jQuery.trim(jQuery(this).parent().find("lable[name='lblIDCard']").text()) == ID_Customer) {
    //            newContent += jQuery(this).parent().parent()[0].outerHTML;
    //            jQuery(this).parent().parent().remove();
    //        } else {
    //            jQuery(this).parent().parent().removeClass("externSelect");
    //        }
    //    });
    jQuery("#tbReportInformer tbody tr").removeClass("externSelect");
    //判断体检号是否相同
    if (IsCustomer) {
        findObj = jQuery("#tbReportInformer tbody tr[id_customer='" + ID_Customer + "']");
        if (jQuery(findObj).length > 0) {
            newContent += jQuery(findObj)[0].outerHTML;
            jQuery(findObj).remove();
        }
        else {
            jQuery(findObj).removeClass("externSelect");
        }
    }
    //判断证件号是否相同
    else {
        findObj = jQuery("#tbReportInformer tbody tr[idcard='" + ID_Customer + "']");
        if (jQuery(findObj).length > 0) {
            newContent += jQuery(findObj)[0].outerHTML;
            jQuery(findObj).remove();
        }
        else {
            jQuery(findObj).removeClass("externSelect");
        }
    }
    if (newContent != "") {
        jQuery("#tbReportInformer tbody").prepend(newContent);
        jQuery("#tbReportInformer tbody tr").first().addClass("externSelect");
        jQuery("#tbReportInformer tbody tr").first().find("[name='ItemCheckbox']").attr('checked', "checked");
    }
    else {
        LoadCustomer(ID_Customer, IsCustomer); //新增身份证检索功能
    }
    ResetRownum();
    jQuery("#txtCustomerID").val("");
    jQuery("#txtCustomerID").focus();
}
/// <summary>
///从服务器加载体检号对应信息
///ID_Customer:ID_Customer xmhuang 2013-11-09 新增身份证检索功能
///</summary>
function LoadCustomer(ID_Customer, IsCustomer) {

    var IDCard = "";
    if (!IsCustomer) {
        IDCard = ID_Customer;
        ID_Customer = "";
    }
    var modelName = jQuery("#modelName").val();
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { IDCard: IDCard, ID_Customer: ID_Customer, modelName: modelName, pageIndex: 0, pageSize: pagePagination.items_per_page, action: 'GetInformerOrReceiptReport' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {
                var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup(TemplateName);
                var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
                var index = 1, Is_Team = 0;
                var newContent = '';
                jQuery(msg.dataList).each(function (i, item) {
                    if (jQuery('#tbReportInformer tbody tr[id_customer="' + item.ID_Customer + '"]').length == 0) {
                        Is_Team = item.TeamName == "" ? 0 : 1;
                        newContent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@CustomerName/gi, item.CustomerName)
                                    .replace(/@GenderName/gi, item.GenderName)
                                    .replace(/@date/gi, item.date)
                                    .replace(/@TeamName/gi, item.TeamName)
                                    .replace(/@Checker/gi, item.Checker)
                                    .replace(/@CheckedDate/gi, item.CheckedDate)
                                    .replace(/@ReportPrinter/gi, item.ReportPrinter)
                                    .replace(/@ReportPrintedDate/gi, item.ReportPrintedDate)
                                    .replace(/@Is_Checked/gi, item.Is_Checked)
                                    .replace(/@Is_ReportPrinted/gi, item.Is_ReportPrinted)
                                    .replace(/@Is_Informed/gi, item.Is_Informed)
                                    .replace(/@Is_ReportReceipted/gi, item.Is_ReportReceipted)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDate/gi, item.FinalDate)
                                    .replace(/@Department/gi, item.Department)
                                    .replace(/@Informer/gi, item.Informer)
                                    .replace(/@InformedDate/gi, item.InformedDate)
                                    .replace(/@ReportReceiptor/gi, item.ReportReceiptor)
                                    .replace(/@ReportReceiptedDate/gi, item.ReportReceiptedDate)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@checked/gi, true)
                                     .replace(/@Is_Team/gi, Is_Team)
                                        .replace(/@IDCard/gi, item.IDCard)
                                     .replace(/@MobileNo/gi, item.MobileNo)
                                    ;
                        index++;
                    }

                });
                if (newContent != '') {
                    jQuery('#tbReportInformer tbody').prepend(newContent);
                    jQuery("#tbReportInformer tbody tr").first().addClass("externSelect");
                    jQuery("#tbReportInformer tbody tr").first().find("[name='ItemCheckbox']").attr('checked', "checked");
                    ResetRownum();
                }
            }
            else {
                ShowSystemDialog("对不起，您查询的体检号不存在或者您没有权限查看此体检号的相关信息！");
                return false;
            }
        }
    });
}
function SingleInforCustomer(obj) {
    var ID_Customer = "", ID_Customers = "", CustomerName = "";
    if (jQuery(obj).parent().parent().is(":visible") == true) {
        ID_Customer = jQuery.trim(jQuery(obj).parent().parent().find("td label[name='lblID_Customer']").text());
        ID_Customers += "'" + ID_Customer + "',";
        CustomerName = jQuery.trim(jQuery(obj).parent().parent().find("td label[name='lblCustomerName']").text());
        jQuery("#lblErrorMessage").text("(正在通知[" + CustomerName + "]前来领取体检报告...)");

        jQuery("#lblErrorMessage").text("(已完成[" + CustomerName + "]体检报告领取通知。)");
        jQuery(obj).parent().parent().remove();
    }
    if (ID_Customers != "") {
        DoInforCustomer(ID_Customers);
    }
}
/// <summary>
///批量通知
///</summary>
function InforCustomer() {
    if (jQuery("#tbReportInformer tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        //jQuery("#lblErrorMessage").text("(您尚未选择需要通知的客户名单！)");
        ShowSystemDialog("您尚未选择需要通知的客户名单！");
        return false;
    }
    else {
        var ID_Customer = "", ID_Customers = "", CustomerName = "";
        //var lblErrorMessage
        jQuery("#tbReportInformer tbody tr td input[name='ItemCheckbox']:checked").each(function () {
            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
                ID_Customers += "'" + ID_Customer + "',";
                CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCustomerName']").text());
                //                jQuery("#lblErrorMessage").text("(正在通知[" + CustomerName + "]前来领取体检报告...)");

                //                jQuery("#lblErrorMessage").text("(已完成[" + CustomerName + "]体检报告领取通知。)");
                jQuery(this).parent().parent().remove();
            }
        });
        if (ID_Customers != "") {
            DoInforCustomer(ID_Customers);
        }

    }
}
function DoInforCustomer(ID_Customers) {
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { ID_Customers: ID_Customers, modelName: modelName, action: 'UpdateCustomerReportInformFlag' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg.success == 1) {
                //jQuery("#lblErrorMessage").text("(通知完毕。)");
                //判断列表中是否有未通知的项目，没有了则重新绑定数据，有则不做任何处理
                if (jQuery("#tbReportInformer tbody tr").length == 0) {
                    QueryPagesData(0);
                }
            }
        }
    });
}

/// <summary>
///单项领取
///</summary>
function SingleReceiptCustomer(obj) {
    //判断是否是本人
    var ID_Customer = "", ID_Customers = "", CustomerName = "", CustomerNameX = "";
    var AllData = "", IsChecked = 0;
    ID_Customer = jQuery.trim(jQuery(obj).parent().parent().find("td label[name='lblID_Customer']").text());
    CustomerName = jQuery.trim(jQuery(obj).parent().parent().find("td label[name='lblCustomerName']").text());
    CustomerNameX = jQuery.trim(jQuery(obj).parent().parent().find("td input[name='txtSelf']").val());
    ID_Customers += "'" + ID_Customer + "',";
    if (jQuery(obj).parent().parent().find("td input[name='ItemCheckbox_Selft']").attr("checked") == "checked") {
        IsChecked = 1;
        jQuery(obj).parent().parent().find("td input[name='txtSelft']").val(CustomerName);
    }
    else {
        IsChecked = 0;

        if (CustomerNameX == "") {
            jQuery("#lblErrorMessage").text("请您填写[" + CustomerName + "]的领取人！");
            jQuery(obj).parent().parent().find("td input[name='txtSelf']").focus();
            return false;
        }
    }
    CustomerName = jQuery.trim(jQuery(obj).parent().parent().find("td label[name='lblCustomerName']").text());
    AllData += ID_Customer + "-" + IsChecked + "-" + CustomerNameX + "-" + CustomerName + "|";
    //    jQuery("#lblErrorMessage").text("(正在领取[" + CustomerName + "]的体检报告...)");
    //    jQuery("#lblErrorMessage").text("(已完成[" + CustomerName + "]体检报告的领取。)");
    jQuery(obj).parent().parent().remove();
    if (AllData != "") {
        DoReceiptCustomer(AllData);
    }
}
/// <summary>
///批量领取,领取时需要修改体检者存档信息中的已完成体检次数和未完成体检次数
///</summary>
function ReceiptCustomer() {
    if (jQuery("#tbReportInformer tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        //jQuery("#lblErrorMessage").text("(您尚未选择需要通知的客户名单！)");
        ShowSystemDialog("对不起，请您选择需要通知的客户名单！");
        return false;
    }
    else {
        var ID_Customer = "", ID_Customers = "", CustomerName = "", CustomerNameX = "";
        var AllData = "", IsChecked = 0;
        //var lblErrorMessage
        jQuery("#tbReportInformer tbody tr td input[name='ItemCheckbox']:checked").each(function () {
            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCustomerName']").text());
                CustomerNameX = jQuery.trim(jQuery(this).parent().parent().find("td input[name='txtSelf']").val());
                ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
                ID_Customers += "'" + ID_Customer + "',";
                if (jQuery(this).parent().parent().find("td input[name='ItemCheckbox_Selft']").attr("checked") == "checked") {
                    IsChecked = 1;
                }
                else {
                    IsChecked = 0;
                    if (CustomerNameX == "") {
                        jQuery("#lblErrorMessage").text("请您填写[" + CustomerName + "]的报告领取人！");
                        jQuery(this).parent().parent().find("td input[name='txtSelf']").focus();
                        return false;
                    }
                }

                if (IsChecked == 1) {
                    CustomerNameX = CustomerName;
                }
                else {
                    CustomerNameX = jQuery.trim(jQuery(this).parent().parent().find("td input[name='txtSelf']").val());
                }

                AllData += ID_Customer + "-" + IsChecked + "-" + CustomerNameX + "-" + CustomerName + "|";
                //                jQuery("#lblErrorMessage").text("(正在领取[" + CustomerName + "]的体检报告...)");
                //                jQuery("#lblErrorMessage").text("(已完成[" + CustomerName + "]体检报告的领取。)");
                jQuery(this).parent().parent().remove();
            }
        });
        if (AllData != "") {
            DoReceiptCustomer(AllData);
        }
    }
}
function DoReceiptCustomer(AllData) {
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { AllData: AllData, modelName: modelName, action: 'UpdateCustomerReportReceiptFlag' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg.success == 1) {
                //jQuery("#lblErrorMessage").text("(领取完毕。)");
                //判断列表中是否有未通知的项目，没有了则重新绑定数据，有则不做任何处理
                if (jQuery("#tbReportInformer tbody tr").length == 0) {
                    QueryPagesData(0);
                }
            }
        }
    });
}

function ChangeSelf(obj) {
    var ParentTr = jQuery(obj).parent().parent();
    var Checked = obj.checked;
    if (Checked == true) {
        var CustomerName = jQuery.trim(jQuery(ParentTr).find("td label[name='lblCustomerName']").text());
        jQuery(obj).siblings("input[name='txtSelf']").val(CustomerName);
    }
    else {
        jQuery(obj).siblings("input[name='txtSelf']").val("");
        jQuery(obj).siblings("input[name='txtSelf']").focus();
        jQuery(obj).siblings("input[name='txtSelf']").select();
    }
}
/****************报告通知和领取公用脚本模块 End***********************/

/****************批量设置领取人 xmhuang 2013-11-01 Begin***********************/
var OldReceipter = ""; //上一次批量领取人的名称
/// <summary>
/// 批量设置领取人 xmhuang 2013-11-01 应需求，由于团体领取人可能只是团体负责人统一领取，特提供批量设置领取人功能来设置领取人
/// </summary>
function BatchSetReceipter(obj) {
    if (jQuery("#tbReportInformer tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        ShowSystemDialog("对不起，请您选择需要" + obj.value + "的客户名单！");
        return false;
    }
    else {
        //弹出设置界面
        OpenSetReceipter();
    }
}
/// <summary>
/// 生成批量设置领取人界面
/// </summary>
function OpenSetReceipter() {
    var tipscontent = '<table class="ModifyPassword">' +
            '<tbody>' +
            '    <tr><td class="left">领取人：</td><td><input maxlength="30" name="txtReceipterX" id="txtReceipterX" value="' + OldReceipter + '" onkeyup="OnFormKeyUp();"/></td></tr>' +
            '</tbody>' +
            '</table>';
    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '批量设置领取人',
        init: function () {
            jQuery("#txtReceipterX").focus();
            if (OldReceipter != "")
            { jQuery("#txtReceipterX").select(); }

        },
        button: [{
            name: '确定',
            callback: function () {
                var ISSetReceipter = DoSetReceipter(); //生成自定义证件号
                return ISSetReceipter;
            }, focus: true
        }, {
            name: '取消'
        }]
    });
}
/// <summary>
/// 执行批量设置领取人
/// </summary>
function DoSetReceipter() {
    //判断是否填写领取人
    var Receipter = jQuery.trim(jQuery("#txtReceipterX").val());
    if (Receipter == "") {
        //        art.dialog({
        //            content: '对不起，请您输入领取人姓名！',
        //            ok: function () {
        //                jQuery("#txtReceipterX").focus();
        //                return true;
        //            }
        //        });
        //        return false;
    }
    OldReceipter = Receipter;
    //统一设置已勾选的领取人
    jQuery("#tbReportInformer tbody tr td input[name='ItemCheckbox']:checked").each(function () {
        //判断当前元素是否是隐藏的
        if (jQuery(this).parent().parent().is(":visible") == true) {
            jQuery(this).parent().parent().find("td input[name='txtSelf']").val(Receipter);
        }
    });
}
/****************批量设置领取人 xmhuang 2013-11-01 End***********************/